Param(
	[string] $nugetApiKey,
	[switch] $whatIf
)
$ErrorActionPreference = "Stop"
& $PSScriptRoot\build.ps1
Publish-Module -Path $PSScriptRoot\Hafr\ -NuGetApiKey $nugetApiKey -WhatIf:$whatIf -Verbose