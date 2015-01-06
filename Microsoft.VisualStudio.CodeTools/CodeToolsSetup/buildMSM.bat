@echo off
if "%1" == "" goto default
if "%1" == "export" goto export

msbuild buildMSM.xml /p:CCNetLabel=1.13.1.1 /p:ReleaseConfig=%1 /t:All
goto end

:default
msbuild buildMSM.xml /p:CCNetLabel=1.13.1.1 /p:ReleaseConfig=release /t:All
export release
goto end

:export
regasm /tlb ..\ITaskManager\bin\Release\ITaskManager.dll
sd edit ..\TLB\ITaskManager.tlb
copy /y ..\ITaskManager\bin\Release\ITaskManager.tlb ..\TLB
msbuild buildMSM.xml /p:CCNetLabel=1.13.1.1 /p:ReleaseConfig=release /t:Export
goto end

:end

