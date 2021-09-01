
@echo off
cd /d "%~dp0"
set wait=3

if not "%*"=="" echo %*
:: empty call => call with test argument
@REM if "%*" EQU "" call dir /B Types\*.xml>tools\tmp && call %0<tools\tmp && del tools\tmp
if "%*" EQU "" for /r %%f in (Types\*.xml) do call %0 %%f Q
@REM if "%~1"=="" goto:eof

:loop
@REM echo loop: %*
if "%~1"=="Q" goto:eof
:: if no more arguments break the loop
if "%~1"=="" (
  :: Wenn mit Coderunner in VS-Code gestartet wurde, sofort beeenden
  if not "%VSCODE_PID%"=="" goto:eof

  choice /c wq /t %wait% /d q /m "W=pause, Q=quit(%wait%s)"
  if errorlevel 2 goto:eof
  if errorlevel 1 goto:pause
)
call:doIt %1
shift
goto:loop

:pause
pause
goto:eof

:doIt
set path=tools;%path%
set xslt=%cd%\tools\types.xslt

@REM if exist "%~1" AltovaXML.exe -xslt2 "%xslt-cs%" -in "%~1" -param outFile='file:///%outFile:\=/%' -out "%outFile%"
@REM if exist "%~1" AltovaXML.exe -xslt2 "%xslt-cs%" -in "%~1"
if exist "%~1" Transform.exe  -xsl:"%xslt%" -s:"%~1">nul
goto:eof

