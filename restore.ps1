[CmdletBinding()]
param (
    [Parameter()]
    [string]
    $Version,
    [Parameter()]
    [string]
    $Instance,
    [Parameter()]
    [switch]
    $Fast
)

.\trigger.ps1 -Task Restore -Instance $Instance -Version $Version -Fast:$Fast
