@echo off
call buildCC %1 
if errorlevel 1 exit /b 1