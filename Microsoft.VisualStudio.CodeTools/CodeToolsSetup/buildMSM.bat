@echo off

rem Make sure NuGet packages are restored before proceeding with the build
..\..\.nuget\NuGet.exe restore ..\CodeTools10.sln
if errorlevel 1 exit /b 1

if "%1" == "" goto default
if "%1" == "export" goto export

msbuild buildMSM.xml /p:CCNetLabel=1.13.1.1 /p:ReleaseConfig=%1 /t:All
if errorlevel 1 exit /b 1
goto end

:default
msbuild buildMSM.xml /p:CCNetLabel=1.13.1.1 /p:ReleaseConfig=release /t:All
if errorlevel 1 exit /b 1

call export release
if errorlevel 1 exit /b 1

goto end

:export

regasm /tlb ..\ITaskManager\bin\Release\ITaskManager.dll
if errorlevel 1 exit /b 1

sd edit ..\TLB\ITaskManager.tlb
if errorlevel 1 exit /b 1

copy /y ..\ITaskManager\bin\Release\ITaskManager.tlb ..\TLB
if errorlevel 1 exit /b 1

msbuild buildMSM.xml /p:CCNetLabel=1.13.1.1 /p:ReleaseConfig=release /t:Export
if errorlevel 1 exit /b 1

:end
exit /b 0
