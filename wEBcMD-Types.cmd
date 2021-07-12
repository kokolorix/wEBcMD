
@echo off
cd /d "%~dp0"
set wait=3

if "%*" NEQ "" echo %*
:: empty call => call with test argument
@REM if "%*" EQU "" call dir /B Types\*.xml>tools\tmp && call %0<tools\tmp && del tools\tmp
if "%*" EQU "" for /r %%f in (Types\*.xml) do call %0 %%f && goto:eof
if "%~1"=="" goto:eof

:loop
:: if no more arguments break the loop
if "%~1"=="" (
  :: Wenn mit Coderunner in VS-Code gestartet wurde, sofort beeenden
  if "%VSCODE_PID%" NEQ "" goto:eof

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
set xslt-cs=%cd%\tools\types-cs.xslt

@REM if exist "%~1" AltovaXML.exe -xslt2 "%xslt-cs%" -in "%~1" -param outFile='file:///%outFile:\=/%' -out "%outFile%" 
if exist "%~1" AltovaXML.exe -xslt2 "%xslt-cs%" -in "%~1"
@REM echo Gespeichert in: %~dpn1.cs 
goto:eof

