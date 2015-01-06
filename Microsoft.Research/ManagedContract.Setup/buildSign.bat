call deleteSignedBits.bat %1
msbuild Setup10.proj /p:CCNetLabel=%1 /p:ContinuousBuild=true /p:IgnoreCpx=true /t:All
if %ERRORLEVEL% NEQ 0 goto :errorExit
call saveSignedBits.bat %1
:errorExit
