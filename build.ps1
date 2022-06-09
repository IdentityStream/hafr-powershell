$ErrorActionPreference = "Stop"
$outputfolder = "$PSScriptRoot\Hafr\bin"
Remove-item $outputfolder -Recurse -ErrorAction Ignore
dotnet publish $PSScriptRoot -c Release -o $outputfolder