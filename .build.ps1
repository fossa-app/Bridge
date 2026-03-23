<#
.Synopsis
    Build script

.Description
    TASKS AND REQUIREMENTS
    Initialize and Clean repository
    Restore packages, workflows, tools
    Format code
    Build projects and the solution
    Run Tests
    Pack
    Publish
#>

[System.Diagnostics.CodeAnalysis.SuppressMessageAttribute('PSReviewUnusedParameter', '', Justification = 'Parameter is used actually.')]
param(
    # Build Version
    [Parameter()]
    [string]
    $Version,
    # Build Instance
    [Parameter()]
    [string]
    $Instance,
    # Fast mode
    [Parameter()]
    [switch]
    $Fast
)

Set-StrictMode -Version Latest

# Synopsis: Initialize folders and variables
Task Init {
    $trashFolder = Join-Path -Path . -ChildPath '.trash'
    $trashFolder = Join-Path -Path $trashFolder -ChildPath $Instance
    New-Item -Path $trashFolder -ItemType Directory | Out-Null
    $trashFolder = Resolve-Path -Path $trashFolder

    $buildArtifactsFolder = Join-Path -Path $trashFolder -ChildPath 'artifacts'
    New-Item -Path $buildArtifactsFolder -ItemType Directory | Out-Null

    $distArtifactsFolder = Join-Path -Path $buildArtifactsFolder -ChildPath 'dist'
    New-Item -Path $distArtifactsFolder -ItemType Directory | Out-Null

    $fableOutputArtifactsFolder = Join-Path -Path $buildArtifactsFolder -ChildPath 'fable_output'
    New-Item -Path $fableOutputArtifactsFolder -ItemType Directory | Out-Null

    $state = [PSCustomObject]@{
        NextVersion                = $null
        TrashFolder                = $trashFolder
        BuildArtifactsFolder       = $buildArtifactsFolder
        DistArtifactsFolder        = $distArtifactsFolder
        FableOutputArtifactsFolder = $fableOutputArtifactsFolder
        NuGetPackagePath           = $null
        NPMPackagePath             = $null
    }

    $state | Export-Clixml -Path ".\.trash\$Instance\state.clixml"
    Write-Output $state
}

# Synopsis: Clean previous build leftovers
Task Clean Init, {
    Get-ChildItem -Directory
    | Where-Object { -not $_.Name.StartsWith('.') }
    | ForEach-Object { Get-ChildItem -Path $_ -Recurse -Directory }
    | Where-Object { ( $_.Name -eq 'bin') -or ( $_.Name -eq 'obj') }
    | ForEach-Object { Remove-Item -Path $_ -Recurse -Force }
}

# Synopsis: Ensure Central Package Versions compliance
Task EnsureCentralPackageVersions Clean, {

    $projectFiles = Get-ChildItem -Path . `
        -Recurse `
        -Include *.csproj, *.fsproj, *.vbproj `
        -File

    $violations = @()

    foreach ($projectFile in $projectFiles) {
        try {
            [xml]$xml = Get-Content $projectFile.FullName -Raw
        }
        catch {
            throw "Failed to parse XML: $($projectFile.FullName)"
        }

        $ns = New-Object System.Xml.XmlNamespaceManager($xml.NameTable)
        $ns.AddNamespace('msb', $xml.DocumentElement.NamespaceURI)

        $nodes = $xml.SelectNodes('//*[@VersionOverride]', $ns)

        foreach ($node in $nodes) {
            $violations += [PSCustomObject]@{
                File  = $projectFile.FullName
                Node  = $node.Name
                Value = $node.GetAttribute('VersionOverride')
            }
        }
    }

    if ($violations.Count -gt 0) {
        throw "VersionOverride attributes are not allowed. File: $($violations[0].File) Node: <$($violations[0].Node)>"
    }
}

# Synopsis: Restore workloads
Task RestoreWorkloads -If { -not $Fast } Clean, {
    Exec { dotnet workload restore }
}

# Synopsis: Restore tools
Task RestoreTools Clean, {
    Exec { dotnet tool restore }
}

# Synopsis: Restore NuGet packages
Task RestoreNuGetPackages Clean, EnsureCentralPackageVersions, {
    $solution = Resolve-Path -Path 'Bridge.slnx'
    Exec { dotnet restore $solution }
}

# Synopsis: Restore NPM packages
Task RestoreNPMPackages Clean, {
    Exec { npm ci }
}

# Synopsis: Restore
Task Restore RestoreWorkloads, RestoreTools, RestoreNuGetPackages, RestoreNPMPackages

# Synopsis: Scan with DevSkim for security issues
Task DevSkim Restore, {
    $state = Import-Clixml -Path ".\.trash\$Instance\state.clixml"
    $trashFolder = $state.TrashFolder
    $sarifFile = Join-Path -Path $trashFolder -ChildPath 'DevSkim.sarif'
    Exec { dotnet tool run devskim analyze --source-code . --output-file $sarifFile }
    Exec { dotnet tool run devskim fix --source-code . --sarif-result $sarifFile --all }
}

# Synopsis: Format XML Files
Task FormatXmlFiles Clean, {
    Get-ChildItem -Include *.xml, *.config, *.props, *.targets, *.nuspec, *.resx, *.ruleset, *.vsixmanifest, *.vsct, *.xlf, *.csproj, *.fsproj, *.vbproj, *.slnx -Recurse -File
    | Where-Object { -not (git check-ignore $PSItem) }
    | ForEach-Object {
        Write-Output "Formatting XML File: $PSItem"
        $content = Get-Content -Path $PSItem -Raw
        $xml = [xml]$content
        $xml.Save($PSItem)
    }
}

# Synopsis: Format Fantomas
Task FormatFantomas Restore, {
    $solution = Resolve-Path -Path 'Bridge.slnx'
    Exec { dotnet fantomas . }
}

# Synopsis: Format
Task Format -If { -not $Fast } Restore, FormatXmlFiles, FormatFantomas

# Synopsis: Estimate Next Version
Task EstimateVersion Restore, {
    $state = Import-Clixml -Path ".\.trash\$Instance\state.clixml"
    if ($Version) {
        $state.NextVersion = [System.Management.Automation.SemanticVersion]$Version
    }
    else {
        $gitversion = Exec { dotnet tool run dotnet-gitversion -- /overrideconfig assembly-versioning-format='{Major}.{Minor}.{Patch}-preview.{CommitsSinceVersionSource}' } | ConvertFrom-Json
        $state.NextVersion = [System.Management.Automation.SemanticVersion]::Parse($gitversion.AssemblySemVer)
    }

    $state | Export-Clixml -Path ".\.trash\$Instance\state.clixml"
    Write-Output "Next version estimated to be $($state.NextVersion)"
    Write-Output $state
}

# Synopsis: Build Project
Task BuildProject EstimateVersion, {
    $state = Import-Clixml -Path ".\.trash\$Instance\state.clixml"
    $project = Resolve-Path -Path 'src/Bridge/Bridge.fsproj'
    $nextVersion = $state.NextVersion

    Exec { dotnet build $project /v:m --configuration Release /p:version=$nextVersion }
}

# Synopsis: Build
Task Build Format, BuildProject, {
    $solution = Resolve-Path -Path 'Bridge.slnx'
    Exec { dotnet build $solution --configuration Release }
}

# Synopsis: Unit Test
Task UnitTest Build, {
    $project = Resolve-Path -Path 'tests/Bridge.Tests/Bridge.Tests.fsproj'
    Exec { dotnet run --project $project --configuration Release }
}

# Synopsis: Functional Test
Task FunctionalTest Build, {
}

# Synopsis: Integration Test
Task IntegrationTest Build, {
    if (-not $env:CI) {
    }
}

# Synopsis: Test
Task Test UnitTest, FunctionalTest, IntegrationTest

# Synopsis: Pack NuGet package
Task PackNuGet Build, Test, {
    $state = Import-Clixml -Path ".\.trash\$Instance\state.clixml"
    $buildArtifactsFolder = $state.BuildArtifactsFolder
    $nextVersion = $state.NextVersion
    $projectPath = Resolve-Path -Path 'src/Bridge/Bridge.fsproj'

    Exec { dotnet pack $projectPath /v:m /p:Configuration=Release /p:version=$nextVersion --output $buildArtifactsFolder }

    $nugetPackage = Get-ChildItem -Path $buildArtifactsFolder -Filter '*.nupkg' | Select-Object -First 1

    $state.NuGetPackagePath = $nugetPackage.FullName

    $state | Export-Clixml -Path ".\.trash\$Instance\state.clixml"
    Write-Output $state
}

# Synopsis: Pack NPM package
Task PackNPM Build, Test, {
    $state = Import-Clixml -Path ".\.trash\$Instance\state.clixml"
    $buildArtifactsFolder = $state.BuildArtifactsFolder
    $distArtifactsFolder = $state.DistArtifactsFolder
    $fableOutputArtifactsFolder = $state.FableOutputArtifactsFolder
    $nextVersion = $state.NextVersion
    $projectPath = Resolve-Path -Path 'src/Bridge/Bridge.fsproj'

    Exec { dotnet fable $projectPath --outDir $fableOutputArtifactsFolder --language typescript --fableLib @fable-org/fable-library-ts }
    Exec { npm version $nextVersion --no-git-tag-version --allow-same-version }
    Exec { npm install }

    $baseTsConfig = (Resolve-Path .\tsconfig.json).Path.Replace('\', '/')
    $tempTsConfigPath = Join-Path -Path $buildArtifactsFolder -ChildPath 'tsconfig.build.json'
    $fablePattern = "$($fableOutputArtifactsFolder.Replace('\', '/'))/**/*"
    $distPath = $distArtifactsFolder.Replace('\', '/')
    $fablePath = $fableOutputArtifactsFolder.Replace('\', '/')
    
    $tempTsConfig = @"
{
  `"extends`": `"$baseTsConfig`",
  "compilerOptions": {
    "outDir": "$distPath",
    "rootDir": "$fablePath",
    "noEmit": false,
    "allowImportingTsExtensions": false,
    "rewriteRelativeImportExtensions": true
  },
  `"include`": [
    `"$fablePattern`"
  ],
  `"exclude`": []
}
"@
    Set-Content -Path $tempTsConfigPath -Value $tempTsConfig

    Exec { npm run build -- --project $tempTsConfigPath }

    $packageJsonPath = Resolve-Path -Path 'package.json'
    $packageJsonContent = Get-Content -Path $packageJsonPath -Raw | ConvertFrom-Json
    $packageJsonContent.version = $nextVersion.ToString()
    
    $packageJsonContent | ConvertTo-Json -Depth 10 | Set-Content -Path (Join-Path -Path $distArtifactsFolder -ChildPath 'package.json')
    
    Get-ChildItem -Path 'README.md', 'LICENSE' -File | Copy-Item -Destination $distArtifactsFolder
    
    Exec { npm version '1.0.0' --no-git-tag-version --allow-same-version }

    Exec { npm pack $distArtifactsFolder --pack-destination $buildArtifactsFolder }
    
    $npmPackage = Get-ChildItem -Path $buildArtifactsFolder -Filter '*.tgz' | Select-Object -First 1

    $state.NPMPackagePath = $npmPackage.FullName

    $state | Export-Clixml -Path ".\.trash\$Instance\state.clixml"
    Write-Output $state
}

# Synopsis: Pack
Task Pack PackNuGet, PackNPM

# Synopsis: Publish NuGet package
Task PublishNuGet PackNuGet, {
    $state = Import-Clixml -Path ".\.trash\$Instance\state.clixml"
    $nugetPackagePath = $state.NuGetPackagePath

    if ($null -eq $env:NUGET_API_KEY) {
        Import-Module -Name Microsoft.PowerShell.SecretManagement
        $apiKey = Get-Secret -Name 'Fossa-Bridge-NuGet-API-Key' -AsPlainText
    }
    else {
        $apiKey = $env:NUGET_API_KEY
    }

    Exec { dotnet nuget push $nugetPackagePath --source https://api.nuget.org/v3/index.json --api-key $apiKey }
}

# Synopsis: Publish NPM package
Task PublishNPM PackNPM, {
    $state = Import-Clixml -Path ".\.trash\$Instance\state.clixml"
    $npmPackagePath = $state.NPMPackagePath

    if ($null -eq $env:NPM_TOKEN) {
        Import-Module -Name Microsoft.PowerShell.SecretManagement
        $token = Get-Secret -Name 'Fossa-Bridge-NPM-Token' -AsPlainText
    }
    else {
        $token = $env:NPM_TOKEN
    }

    Exec { npm config set //registry.npmjs.org/:_authToken $token }
    Exec { npm publish $npmPackagePath --access public }
}

# Synopsis: Publish
Task Publish PublishNuGet, PublishNPM
