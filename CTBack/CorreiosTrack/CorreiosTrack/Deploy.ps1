$scriptPath = split-path -parent $MyInvocation.MyCommand.Definition
& (Join-Path "$scriptPath" CorreiosTrack.exe) -buildversion "$OctopusReleaseNumber" | Write-Host
