REM Run one analysis 
REM Compulsory: the directory to test

@echo off

if "%1"=="" goto parameters

cd %1

shift
call Run.bat %1 %2 %3 %4 %5 %6 %7 %8 %9  
if errorlevel 1 goto error
goto ok

:error
@echo.
@echo *********** FAILED *********
@echo. 
cd ..
exit /b 1

:ok
@echo. The test passed
cd ..
exit /b 0

:parameters
@echo "Use: Run <dir> [params]"
