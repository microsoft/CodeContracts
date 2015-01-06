@echo off
copy /y %1 %temp% > nul
pushd %temp%
%~dp0\Rewriter\bin\Debug\rewriter %~nx1 /out:%~nx1.rewritten > nul
ildasm %~nx1 /out:%~nx1.il
ildasm %~nx1.rewritten /out:%~nx1.rewritten.il
%~dp0\compareil.pl %~nx1.il > %~nx1.processed.il
%~dp0\compareil.pl %~nx1.rewritten.il > %~nx1.rewritten.processed.il
start windiff %~nx1.processed.il %~nx1.rewritten.processed.il
popd
