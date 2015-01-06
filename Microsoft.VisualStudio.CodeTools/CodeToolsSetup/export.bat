set release=%1
if "%1" == "" set release=release

copy /y %release%\CodeTools.msm ..\..\Microsoft.Research\ImportedCodeTools
copy /y ..\CodeToolsUpdate\bin\%release%\CodeToolsUpdate.exe ..\..\Microsoft.Research\ImportedCodeTools

