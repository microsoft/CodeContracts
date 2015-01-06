@echo off

REM Runs the unsafe analyses

set OPTIONS=-show progress -stats! -stats valid -stats perMethod

if /i "%1" == "fast" goto doFast

@echo complete check
copy Framework\AllDLL.txt Framework\DLLs.txt
goto analyze

:doFast
@echo fast check
shift 
copy Framework\ShortDLL.txt Framework\DLLs.txt

:analyze

@echo Testing Clousot "unsafe" with the Basic tests

for /F "eol=; tokens=1,2* delims=," %%i in (Analyses.txt) do (
 @echo . Testing the abstract domain: %%i
 cd Basic
 call Run.bat %%i %%j  %OPTIONS%
 if not errorlevel 0 set error=1 
 cd .. 
)

@echo Testing Clousot "unsafe" with the .NET framework DLLs

for /F "eol=; tokens=1,2* delims=," %%i in (Analyses.txt) do (
 @echo . Testing the abstract domain: %%i
 cd Framework
 call Run.bat %%i %%j  %OPTIONS%
 if not errorlevel 0 set error=1 
 cd .. 
)

REM Default: set the DLLs to be analyzed to all...
copy Framework\AllDLL.txt DLLs.txt

if defined %error% goto exitWithError

exit /b 0

:exitWithError
@echo failed
exit /b 1 