@echo off
REM Run the bounds analysis on the framework DLLs
REM Usage: 
REM 	   The first parameter is the prefix for the text files
REM 	   All the others are passed to Clousot, and are those specific for the analysis

setlocal 

SET CLOUSOTEXEDIR=..\..\..\ClousotBinaries
SET FRWORKDIR=..\..\..\FrameworkBinaries\v2
SET PREFIX=%1
SET LAST=""
    
shift

for /F "eol=; tokens=1,2* delims=," %%i in (DLLs.txt) do (
  SET LAST=%%i
  @echo .. Analyzing %%i 
  %CLOUSOTEXEDIR%\clousot.exe %FRWORKDIR%\%%i %1 %2 %3 %4 %5 %6 %7 %8 %9> %PREFIX%.%%i.txt 
  REM @echo ... Checking the answer 
  fc /W %PREFIX%.%%i.txt %PREFIX%.%%i.ok.txt > nul
  if errorlevel 1 goto error
  @echo ... ok 
  @echo.
)
REM If we reach this point, all the tests succeeded
exit /b 0

:error
@echo ..:: FAILED ::.. 
start windiff %PREFIX%.%LAST%.txt %PREFIX%.%LAST%.ok.txt
exit /b 1


