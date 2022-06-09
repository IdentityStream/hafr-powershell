$ErrorActionPreference = "Stop"
$outputfolder = "$PSScriptRoot\out"
Remove-item $outputfolder -Recurse -ErrorAction Ignore
New-Item -Path $outputfolder -ItemType Directory
Copy-Item -Path $PSScriptRoot\Hafr.psd1 -Destination $outputfolder\Hafr.psd1
dotnet publish $PSScriptRoot -c Release -o $outputfolder\bin