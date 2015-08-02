@echo off

.nuget\nuget.exe restore .nuget\packages.config -PackagesDirectory .\packages
if errorlevel 1 goto FailedNoPop

pushd Microsoft.VisualStudio.CodeTools\CodeToolsSetup

call buildMSM release
if errorlevel 1 goto Failed

call export release
if errorlevel 1 goto Failed

popd
pushd Microsoft.Research\ManagedContract.Setup

call buildmsi %1 devlab9ts
if errorlevel 1 goto Failed

call buildnuget %1 devlab9ts 
if errorlevel 1 goto Failed

popd
echo .
echo ****************************************************
echo Done building CodeContracts version %1
echo ****************************************************
exit /b 0

:Failed
popd
:FailedNoPop
echo .
echo ****************************************************
echo Build FAILED
echo ****************************************************
exit /b 1
