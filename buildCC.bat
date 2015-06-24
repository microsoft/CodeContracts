@echo off
cd Microsoft.Research\ManagedContract.Setup
call buildmsi %1 devlab9ts
call buildnuget %1 devlab9ts 
cd ..\..
echo .
echo ****************************************************
echo Done building CodeContracts version %1
echo ****************************************************
