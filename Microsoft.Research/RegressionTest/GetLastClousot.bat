@echo off
if "%1"=="" goto default

set CLOUSOTROOT="%1"
goto getit

:default
REM the default behavior
set CLOUSOTROOT=..\..\..\System.Compiler.Analysis
goto getit

:getit

cd ClousotBinaries
call GetLastClousot.bat
cd..

set CLOUSOTROOT=