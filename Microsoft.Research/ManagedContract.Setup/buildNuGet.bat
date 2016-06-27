@echo off
setlocal
set version=%1
set releaseConfig=%2

IF "%version%" EQU "" goto usage
IF "%releaseConfig%" EQU "" goto usage

:exec

set contractFolder=%~dp0

echo ************************************
echo     Validating Msi
echo ************************************
set msiFile=%contractFolder%\%releaseConfig%\msi\Contracts.%releaseConfig%.msi
IF NOT EXIST "%msiFile%" (
	echo Msi "%msiFile%" not found. Please ensure you ran buildMsi correctly.
	goto :error
)

echo ************************************
echo     Extracting Msi
echo ************************************
REM We are extracting the MSI for now to avoid writing duplicate specifications. 
REM When we are more comfortable with Nuget we can revisit and nest the nuget in the vsix in the wix.
REM Note ExplodeMsi requires to be run from its deployed folder
set expandedFolder=%contractFolder%\%releaseConfig%\expandedMsi
pushd %contractFolder%\NugetBinaries
ExplodeMsi.exe -c -t "%msiFile%" "%expandedFolder%"
if errorlevel 1 goto :error
popd


echo ************************************
echo     Building NuGet Package
echo ************************************
set packageRootFolder=%expandedFolder%\layout\[ProgramFilesFolder]\Microsoft\Contracts
copy %contractFolder%\Microsoft.Contracts.ds %packageRootFolder%\Microsoft.Contracts.ds /Y
NugetBinaries\NuGet.exe pack -Verbosity detailed -NoPackageAnalysis -NoDefaultExcludes DotNet.Contracts.nuspec -OutputDirectory %contractFolder%\%releaseConfig% -Version %version% -BasePath "%packageRootFolder%"



goto :end

:usage
	echo buildNuget.cmd ^<Version^> ^<ReleaseConfiguration^>
	echo Version should match the version the MSI was build for. ReleaseConfiguration should be the same as passed to buildMsi i.e. msft9
	exit /b 1

:error
	echo.
	echo !!! Error encountered !!!!
	exit /b 1
:end