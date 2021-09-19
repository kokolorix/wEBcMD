@echo off
:: from tools to main dir
cd /d "%~dp0"

:: show arguments, if any
if not "%*"=="" echo %*

set xsl=%~1
echo xsl: %xsl%
echo.

for /f "tokens=*" %%F in ('dir/b/s %2') do call:doIt %%F
goto:eof

:doIt
@REM echo %1
if exist "%~1" %~dp0\Transform.exe  -xsl:"%xsl%" -s:"%~1">nul
goto:eof



:loop
if "%~1"=="Q" goto:eof
:: if no more arguments break the loop
if "%~1"=="" (
  :: Wenn mit Coderunner in VS-Code gestartet wurde, sofort beeenden
  if not "%VSCODE_PID%"=="" goto:eof

  choice /c wq /t 3 /d q /m "W=pause, Q=quit(3s)"
  if errorlevel 2 goto:eof
  if errorlevel 1 goto:pause
)

call:doIt1 %1
shift
goto:loop

:pause
pause
goto:eof

:doIt1
set path=%cd%\tools;%path%
set xslt=%cd%\tools\types.xslt

@REM if exist "%~1" AltovaXML.exe -xslt2 "%xslt-cs%" -in "%~1" -param outFile='file:///%outFile:\=/%' -out "%outFile%"
@REM if exist "%~1" AltovaXML.exe -xslt2 "%xslt-cs%" -in "%~1"
if exist "%~1" Transform.exe  -xsl:"%xslt%" -s:"%~1">nul
goto:eof
