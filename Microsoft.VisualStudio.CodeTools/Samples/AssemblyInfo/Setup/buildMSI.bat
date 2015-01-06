@ECHO OFF
REM Copyright 2010 Microsoft Research
REM
REM This batch file builds an installer for the sample program
REM You need to install the Wix tool to create the installer
REM and set the "WIXDIR" variable in this batch file appropiately.
REM
REM Note: The two warnings by light.exe can be safely ignored.

@set MAIN=AssemblyInfo
@set RELEASECONFIG=Debug

@set WIXDIR="c:\users\daan\dev\cci\Microsoft.Research\ManagedContract.Setup\WixBinaries"
@set CODETOOLSSDK="c:\program files\Microsoft\CodeToolsSDK"

ECHO Building %MAIN% %RELEASECONFIG% version

IF NOT EXIST %WIXDIR%\candle.exe GOTO nowix

%WIXDIR%\candle.exe %MAIN%.wxs -out %MAIN%.wixobj -dReleaseConfig=%RELEASECONFIG% 
if errorlevel 1 goto end

%WIXDIR%\candle.exe UI.wxs -out UI.wixobj -dLicense=License.rtf -dReleaseConfig=%RELEASECONFIG% 
if errorlevel 1 goto end

ECHO Building msi.
ECHO Note: you can ignore the two warnings LGHT1056, and LGHT1076:ICE61
%WIXDIR%\light.exe -out %MAIN%.msi -cultures:en-us %MAIN%.wixobj UI.wixobj -dReleaseConfig=%RELEASECONFIG% -b ..\Program\bin\%RELEASECONFIG% -b ..\PropertyPane\bin\%RELEASECONFIG% -b ..\Targets -b %CODETOOLSSDK%\bin

GOTO end

:nowix
ECHO Please install the Wix binaries and set the path inside this batch file
ECHO.
GOTO end

:end