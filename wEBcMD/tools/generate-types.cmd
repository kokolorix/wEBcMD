
@echo off
cd /d "%~dp0"
set wait=3

if not "%*"=="" echo %*
:: empty call => call with test argument
@REM if "%*" EQU "" call dir /B Types\*.xml>tools\tmp && call %0<tools\tmp && del tools\tmp
if "%*" EQU "" for /r %%f in (..\Types\*.xml) do call %0 %%f Q
@REM if "%~1"=="" goto:eof

:loop
@REM echo loop: %*
if "%~1"=="Q" (
   call:diagrams
   goto:eof
)
:: if no more arguments break the loop
if "%~1"=="" (
   call:diagrams
  :: Wenn mit Coderunner in VS-Code gestartet wurde, sofort beeenden
  if not "%VSCODE_PID%"=="" goto:eof

  choice /c wq /t %wait% /d q /m "W=pause, Q=quit(%wait%s)"
  if errorlevel 2 goto:eof
  if errorlevel 1 goto:pause
)
call:transform %1
shift
goto:loop

:pause
pause
goto:eof

:transform
@REM set path=%cd%\tools;%path%
set xslt=%cd%\types.xslt

@REM if exist "%~1" AltovaXML.exe -xslt2 "%xslt-cs%" -in "%~1" -param outFile='file:///%outFile:\=/%' -out "%outFile%"
@REM if exist "%~1" AltovaXML.exe -xslt2 "%xslt-cs%" -in "%~1"
if exist "%~1" Transform.exe  -xsl:"%xslt%" -s:"%~1">nul

goto:eof

:diagrams

echo generate typescript diagrams
pushd ..\Doc
if exist generate-ts-diagrams.cmd call generate-ts-diagrams
popd

echo generate csharp diagrams
call puml-gen ..\Types ..\Doc\Types\cs -dir  -createAssociation -allInOne

echo convert plantuml diagrams to svg
call java -jar plantuml.jar ..\Doc\Types\**\*.puml -svg

goto:eof

