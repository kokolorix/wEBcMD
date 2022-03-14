@echo off
pushd %~dp0

@REM echo current dir is: %cd%

call powershell -file "%~dpn0.ps1"

@REM echo args: %*
@REM echo.

popd
exit /B 0


