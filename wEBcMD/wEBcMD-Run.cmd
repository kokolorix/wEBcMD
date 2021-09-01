@echo off
cd /d "%~dp0"

tasklist -fi "imagename  eq wEBcMD.exe" | findstr "wEBcMD.exe"
if errorlevel 1 goto:run

:kill
call taskkill -im wEBcMD.exe -f

:run
call dotnet run