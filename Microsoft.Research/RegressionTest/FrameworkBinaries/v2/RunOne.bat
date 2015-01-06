
echo .
echo Analyzing %1
echo   %2 %3 %4 %5 %6 %7 %8 %9
title Analyzing %1

%2 %3 %4 %5 %6 %7 %8 %9 %1
if %ERRORLEVEL% NEQ 0 goto :errorExit

goto :done

:errorExit
set HasErrors=1

:done



