set release=%1
if "%1" == "" set release=release

copy /y %release%\CodeTools.msm ..\..\Microsoft.Research\ImportedCodeTools
if errorlevel 1 exit /b 1

copy /y ..\CodeToolsUpdate\bin\%release%\CodeToolsUpdate.exe ..\..\Microsoft.Research\ImportedCodeTools
if errorlevel 1 exit /b 1

