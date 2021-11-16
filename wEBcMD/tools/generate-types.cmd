
@echo off
cd /d "%~dp0"
set wait=3

set /a argc=0
for %%a in (%*) do set /a argc+=1
@REM echo %argc% %*
@REM if not "%*"=="" echo %*

if %argc%==0 for /r %%f in (..\Types\*.xml) do call:process %%f
if %argc%==0 call:diagrams
if %argc%==0 goto:eof

:process
:: if no more arguments break the loop
if "%~1"=="" (
   if %argc% gtr 0 call:diagrams
	goto:eof
)
call:transform %1

shift
goto:process %*

:pause
pause
goto:eof





: ==========================================================================================================
: ==========================================================================================================
: ==========================================================================================================
:transform
echo %argc% generate code from %~nx1
@REM goto:eof%

@REM set path=%cd%\tools;%path
set xslt=%cd%\types.xslt

@REM if exist "%~1" AltovaXML.exe -xslt2 "%xslt-cs%" -in "%~1" -param outFile='file:///%outFile:\=/%' -out "%outFile%"
@REM if exist "%~1" AltovaXML.exe -xslt2 "%xslt-cs%" -in "%~1"
if exist "%~1" Transform.exe  -xsl:"%xslt%" -s:"%~1">nul

goto:eof






: ==========================================================================================================
: ==========================================================================================================
: ==========================================================================================================
:diagrams

echo %argc% generate typescript diagrams
@REM goto:eof

pushd ..\Doc
if exist generate-ts-diagrams.cmd call generate-ts-diagrams
popd

echo %argc% generate csharp diagrams
call puml-gen ..\Types ..\Doc\Types\cs -dir  -createAssociation -allInOne

echo %argc% convert plantuml diagrams to svg
call java -jar plantuml.jar ..\Doc\Types\**\*.puml -svg

:: Wenn mit Coderunner in VS-Code gestartet wurde, sofort beeenden
if not "%VSCODE_PID%"=="" goto:eof

choice /c wq /t %wait% /d q /m "W=pause, Q=quit(%wait%s)"
if errorlevel 2 goto:eof
if errorlevel 1 goto:pause

goto:eof

