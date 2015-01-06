@Echo off

setlocal

set HasErrors=0

for %%f in (*.dll) do call RunOne.bat %%f %1 %2 %3 %4 %5 %6 %7 %8 %9

if %HasErrors% NEQ 0 goto :errorExit

title Done!
exit /b 0

:errorExit
title Errors!
exit /b 1

