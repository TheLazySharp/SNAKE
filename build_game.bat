@echo off
REM === Script pour compiler et générer un .exe de ton jeu Raylib ===

REM Nom du projet (remplace si besoin)
set PROJECT=MonJeuRaylib

REM Dossier de sortie
set OUTPUT=bin\Release\net6.0\win-x64\publish

echo ===============================
echo 🔨 Compilation du projet %PROJECT%
echo ===============================

dotnet publish -c Release -r win-x64 --self-contained true /p:PublishSingleFile=true

echo.
echo ===============================
echo ✅ Build terminé !
echo ===============================
echo Ton exe se trouve ici :
echo %OUTPUT%
echo.

pause
