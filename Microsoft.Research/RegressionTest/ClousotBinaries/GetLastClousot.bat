REM @echo off
@echo Getting last Clousot version

set CLOUSOTROOT=..\..\Clousot

echo Getting the last version of Clousot from %CLOUSOTROOT%\bin\Debug
copy /y %CLOUSOTROOT%\bin\Debug\clousot.exe
copy /y %CLOUSOTROOT%\bin\Debug\CodeFixes.dll
copy /y %CLOUSOTROOT%\bin\Debug\"Abstract Domains.dll"
copy /y %CLOUSOTROOT%\bin\Debug\Analyzers.dll
copy /y %CLOUSOTROOT%\bin\Debug\CodeAnalysis.dll
copy /y %CLOUSOTROOT%\bin\Debug\ClousotMain.dll
copy /y %CLOUSOTROOT%\bin\Debug\DataStructures.dll
copy /y %CLOUSOTROOT%\bin\Debug\Graphs.dll
copy /y %CLOUSOTROOT%\bin\Debug\System.Compiler.dll
copy /y %CLOUSOTROOT%\bin\Debug\System.Compiler.Runtime.dll
copy /y %CLOUSOTROOT%\bin\Debug\CCI1CodeProvider.dll
copy /y %CLOUSOTROOT%\bin\Debug\Foxtrot.Extractor.dll
copy /y %CLOUSOTROOT%\bin\Debug\Microsoft.Contracts.Deserializer.dll
copy /y %CLOUSOTROOT%\bin\Debug\Microsoft.contracts.dll