@echo off

color 0a
title GitCommit/Push V0.01 - Ran Crump

set INPUT=
set /P INPUT=Would you like to push a update? (y/n): %=%

If /I "%INPUT%"=="y" goto yes 
If /I "%INPUT%"=="n" goto no

:no
echo "Process Diverted, input 'y' not recived"
pause
Exit /b

:yes
git add .
git commit -m "GitCommand&Push Remote User..." 
git pull
git push
git pull
pause