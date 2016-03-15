set release=%1
if "%1" == "" set release=release

xcopy /y %release%\CodeTools.msm ..\..\Microsoft.Research\ImportedCodeTools\
if errorlevel 1 exit /b 1

xcopy /y ..\CodeToolsUpdate\bin\%release%\CodeToolsUpdate.exe ..\..\Microsoft.Research\ImportedCodeTools\
if errorlevel 1 exit /b 1

xcopy /y ..\ITaskManager\bin\%release%\ITaskManager.dll ..\..\Microsoft.Research\ImportedCodeTools\
if errorlevel 1 exit /b 1

xcopy /y ..\IPropertyPane\bin\%release%\IPropertyPane.dll ..\..\Microsoft.Research\ImportedCodeTools\
if errorlevel 1 exit /b 1

