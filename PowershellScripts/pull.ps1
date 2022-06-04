$mypath = $MyInvocation.MyCommand.Path
Set-Location $mypath
Set-Location ..
git pull origin main #gets all files from repo