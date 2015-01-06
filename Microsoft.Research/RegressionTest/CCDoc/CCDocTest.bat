rem @echo off
echo Running regression test

cd %1
cd

copy /Y CCDocTests.xml new.xml
if not errorlevel 0 goto failEnd

rem if not exist "ccdoc.exe" goto failEnd

call ccdoc.exe -assembly CCDocTests.contracts.dll -xmlfile new.xml -debug true
if not errorlevel 0 goto failEnd

fc /W good.xml new.xml > difference.txt
if not errorlevel 1 goto goodEnd

goto failEnd

:goodEnd

echo SUCCEEDED
exit /b 0

:failEnd

echo FAILED

exit /b 1

