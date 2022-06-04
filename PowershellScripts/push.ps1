$mypath = $MyInvocation.MyCommand.Path # gets the path to this script
Write-Host "Setting path to: $mypath" # you should see this in 
Set-Location $mypath # cd to this path
Set-Location .. #cd 1 folder up

$comment=Read-Host "Add a comment describing your work since the last time you comitter:" #requests the user to add a comment and stores it in the comment variable
git add . #adds all files 
git commit -m "$comment" #sets the comment and backs up the changes on your pc
git push #pushes the backup to the repo so others can use it 