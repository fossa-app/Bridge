$ErrorActionPreference = "Stop"

# Define paths
$projectPath = "src/Fossa.Bridge/Fossa.Bridge.fsproj"
$distDir = "dist"
$fableOutputDir = "fable_output"

Write-Host "Cleaning dist and fable_output directories..."
if (Test-Path $distDir) { Remove-Item -Recurse -Force $distDir }
if (Test-Path $fableOutputDir) { Remove-Item -Recurse -Force $fableOutputDir }

New-Item -ItemType Directory -Force -Path $distDir | Out-Null

# 1. Build .NET and create NuGet package
Write-Host "`n==============================================="
Write-Host "Building .NET Project and packing NuGet..."
Write-Host "==============================================="
dotnet build $projectPath -c Release
if ($LASTEXITCODE -ne 0) { throw ".NET Build failed" }

dotnet pack $projectPath -c Release -o $distDir
if ($LASTEXITCODE -ne 0) { throw "NuGet Pack failed" }

# 2. Fable Compilation
Write-Host "`n==============================================="
Write-Host "Compiling F# to Fable TypeScript..."
Write-Host "==============================================="
dotnet fable $projectPath -o $fableOutputDir --lang ts
if ($LASTEXITCODE -ne 0) { throw "Fable compilation failed" }

# 3. NPM Install and TypeScript Compilation
Write-Host "`n==============================================="
Write-Host "Installing NPM dev dependencies..."
Write-Host "==============================================="
npm install
if ($LASTEXITCODE -ne 0) { throw "NPM Install failed" }

Write-Host "`n==============================================="
Write-Host "Compiling TypeScript to JavaScript and Types..."
Write-Host "==============================================="
npm run build
if ($LASTEXITCODE -ne 0) { throw "TypeScript build failed" }

# 4. Pack NPM Package
Write-Host "`n==============================================="
Write-Host "Packing NPM package..."
Write-Host "==============================================="
npm pack --pack-destination $distDir
if ($LASTEXITCODE -ne 0) { throw "NPM Pack failed" }

# 5. Verification
Write-Host "`n==============================================="
Write-Host "Verifying Artifacts..."
Write-Host "==============================================="
$nupkgCount = (Get-ChildItem -Path $distDir -Filter "*.nupkg").Count
$tgzCount = (Get-ChildItem -Path $distDir -Filter "*.tgz").Count

if ($nupkgCount -eq 0) {
    throw "Verification failed: Expected at least one .nupkg file in $distDir"
}
Write-Host "SUCCESS: Found $nupkgCount NuGet package(s)." -ForegroundColor Green

if ($tgzCount -eq 0) {
    throw "Verification failed: Expected at least one .tgz file in $distDir"
}
Write-Host "SUCCESS: Found $tgzCount NPM package(s)." -ForegroundColor Green

Write-Host "`nBuild and Packaging completed successfully!" -ForegroundColor Green
