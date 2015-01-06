set ILMERGEDIR=\cci\ilmerge
%ILMERGEDIR%\ilmerge\bin\debug\ilmerge.exe /out:Foxtrot.exe /log /wildcards /allowDup bin\Debug\Foxtrot.exe bin\Debug\System.Compiler.dll bin\Debug\System.Compiler.Runtime.dll bin\Debug\Microsoft.Contracts.dll
copy /y Foxtrot.exe Foxtrot.ex_
del Foxtrot.exe

