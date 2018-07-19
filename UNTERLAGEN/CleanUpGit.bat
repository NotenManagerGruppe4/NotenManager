@echo off
echo Bitte alle Aenderungen vor dem Ausfuehren sichern!
timeout /t 5 /nobreak

cd C:
cd C:\Notenmanager\NotenManager

echo Resete GitCache...

git rm -r --cached .
git add .
git commit -m "fixed untracked files"

echo Resetet

echo Starte TortoiseGit neu...

cd "C:\Program Files\TortoiseGit\bin\TGitCache.exe"

taskkill /im TGitCache.exe
start "" "C:\Program Files\TortoiseGit\bin\TGitCache.exe"

echo Neu gestartet

timeout /t 3