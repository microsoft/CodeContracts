
REM The entry point for the regression test

if "%1"=="" goto usage

if /i "%1"=="all" goto runall  

REM Run one particular analysis
@echo Run one analysis: %1
cd Analyses
call Run %1 %2 %3 %4 %5 %6 %7 %8 %9
cd ..
goto exit

:runall
shift
for /F "eol=; tokens=1,2* delims=," %%i in (Analyses.txt) do (
 @echo Testing the analysis %%i
 cd Analyses
 call Run %%i %1 %2 %3 %4 %5 %6 %7 %8 %9
 cd ..
)


:usage
@echo "Use: run (<dir> | all) [fast]"

:exit