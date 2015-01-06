@echo off
if "%2" == "" goto nonetlabel

echo Build %2 version (%1)
msbuild buildMSI10.xml /p:CCNetLabel=%1 /p:ReleaseConfig=%2 /t:All %3
goto end

:nonetlabel
if "%1" == "" goto default

echo build %1 version (1.1.1.1)
msbuild buildMSI10.xml /p:CCNetLabel=1.1.1.1 /p:ReleaseConfig=%1 /t:All 
goto end

:default
echo build debug version (1.1.1.1)
msbuild buildMSI10.xml /p:CCNetLabel=1.1.1.1 /p:ReleaseConfig=debug /t:All
goto end


:end

