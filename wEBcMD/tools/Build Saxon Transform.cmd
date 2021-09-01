@echo off

if not exist "%ProgramFiles%\Saxonica\SaxonHEC1.2.1" echo SaxonHEC1.2.1 not installed & goto:eof
if not exist "%ProgramFiles(x86)%\Microsoft Visual Studio\2019\Community\VC\Auxiliary\Build\vcvars64.bat" echo Visual Studio 2019 not installed & goto:eof

call "%ProgramFiles(x86)%\Microsoft Visual Studio\2019\Community\VC\Auxiliary\Build\vcvars64.bat"

pushd "%ProgramFiles%\Saxonica\SaxonHEC1.2.1\command"
call cl /EHsc "-I%jdkdir%" "-I%jdkdir%\win32" "-Ijni" "%ProgramFiles%\Saxonica\SaxonHEC1.2.1\command\Transform.c"
call copy /Y "%ProgramFiles%\Saxonica\SaxonHEC1.2.1\command\Transform.exe" "C:\Projects\wEBcMD\tools\Transform.exe"
call copy /Y "%ProgramFiles%\Saxonica\SaxonHEC1.2.1\libsaxonhec.dll" "C:\Projects\wEBcMD\tools\libsaxonhec.dll"
popd

pause
