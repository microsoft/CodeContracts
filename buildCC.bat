@echo off

cd Microsoft.VisualStudio.CodeTools\CodeToolsSetup
call buildMSM release
call export release
cd ..\..

cd Microsoft.Research\ManagedContract.Setup
call buildmsi %1 devlab9ts
call buildnuget %1 devlab9ts 
cd ..\..
echo .
echo ****************************************************
echo Done building CodeContracts version %1
echo ****************************************************
