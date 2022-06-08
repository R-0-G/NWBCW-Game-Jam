
# Getting started with Unity,  git and downloading our project on a Windows PC

## 1. GIT
### Description
This is used to keep our project backed up, easily share files between users, and experiment without needing to worry about losing a nice configuration you had earlier (as well as a variety of other uses for programming) 

### Installation Steps

  <ol>
  <li> Sign up to github (the email you use here will be needed for a later step)
  <li> Download git at https://gitforwindows.org/ and install. When going through the installation wizard, just go with the default setting for every option.
  <li> To test that git is working open powershell (hit start and type in powershell - it should be one of the installed programs on your pc by default). When the blue box appears type in the following text:
</ol>

> git --version

  Then hit return. You should see something like:
  > git version 2.30.2.windows.1

git is now installed and working on your PC - Congrats!

 ## 2. UNITY
 ### Description
A game engine that should speed up development time by having a lot of tools I am familiar with 

### Installation steps
 <ol>
<li> Install "unity hub" from https://unity3d.com/get-unity/download (you may also need to create an account with unity - which might also include creating a free unity license - its been a while since I performed these steps, but I think these steps should be self-explanatory - any issues let me know)
<li> When unity hub opens click on installs -> install editor
<li> Choose 2021.3.4f1 & click install
<li> You shouoldnt need to install any of the sub-components except documentation
</ol>

## 3. Our project
### Installation steps
<ol>
<li> In the meantime while the unity editor is installing & we have git set up, we can download our game jam project and start working with it
<li> Create  a new folder somewhere in your PC (ideally somewhere that is not backed up via onedrive or dropbox or some similar service)
<li> Open that newly created folder, hold down shift & with your mouse over the empty area in the middle, right click - you should see the option "open in terminal" or "open in git bash"
<li> When a screen appears, type in the following command
</ol>

> git clone https://github.com/R-0-G/NWBCW-Game-Jam.git

Wait a few minutes and you now should have downloaded the project! Once your Unity Installation has completed installing you should be able to open the project. Go to the Projects tab in unity hub & click open & find the directory where everything got downloaded to. 

A final step is needed so that I can give you permissions to make changes to the project. In the project folder find a folder called PowershellScripts and open it, then double click the file "initialize.form.exe" . Fill out the small form with the email you used in step 1.1. This will generate two files in a folder called Config - a public key and a private key. If you send me the public key I will be able to give you the correct permissions and allow you to make changes to the project (ie start uploading files including textures, or designing levels). If you aren't sure which of the files is the public key, right click on one and select "Copy as path" & paste the path somewhere. It should end in ".pub" . ie:
>C:\Users\[name]\Documents\NWBCW\PowershellScripts\Config\sshkey.pub

The public key will also generally be the file with the smaller size.
