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

.\trigger.ps1 -Task Build -Instance $Instance -Version $Version -Fast:$Fast
