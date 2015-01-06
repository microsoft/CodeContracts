set TDIR=d:\Public\CodeContracts\%1
set BDIR1=%TDIR%\Unpacked\devlab9ts\Bin
set BDIR2=%TDIR%\Unpacked\msft9\Bin
md %BDIR1% 
md %BDIR2% 
copy devlab9ts\msisigned\* %TDIR%
copy msft9\msisigned\*.msi %TDIR%
copy devlab9ts\tmp\*.exe %BDIR1%
copy devlab9ts\tmp\*.pdb %BDIR1%
copy devlab9ts\tmp\*.exe.config %BDIR1%
copy msft9\tmp\*.exe %BDIR2%
copy msft9\tmp\*.pdb %BDIR2%
copy msft9\tmp\*.exe.config %BDIR2%
