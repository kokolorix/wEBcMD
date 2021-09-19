
@echo off
cd /d "%~dp0"
pushd wEBcMD\tools
call generate-types.cmd %*
popd