@echo off

set ROSLYNPATH=%1

if "%1" == "" goto errorEnd
if "%1" == "?" goto errorEnd
if "%1" == "/" goto errorEnd
if "%1" == "/?" goto errorEnd
if "%1" == "-?" goto errorEnd
if "%1" == "help" goto errorEnd

copy /Y %ROSLYNPATH%\Microsoft.CodeAnalysis.dll
copy /Y %ROSLYNPATH%\Microsoft.CodeAnalysis.pdb
copy /Y %ROSLYNPATH%\Microsoft.CodeAnalysis.CSharp.dll
copy /Y %ROSLYNPATH%\Microsoft.CodeAnalysis.CSharp.pdb
copy /Y %ROSLYNPATH%\Microsoft.CodeAnalysis.EditorFeatures.dll
copy /Y %ROSLYNPATH%\Microsoft.CodeAnalysis.EditorFeatures.pdb
copy /Y %ROSLYNPATH%\Microsoft.CodeAnalysis.EditorFeatures.Text.dll
copy /Y %ROSLYNPATH%\Microsoft.CodeAnalysis.EditorFeatures.Text.pdb
copy /Y %ROSLYNPATH%\Microsoft.CodeAnalysis.Features.dll
copy /Y %ROSLYNPATH%\Microsoft.CodeAnalysis.Features.pdb
copy /Y %ROSLYNPATH%\Microsoft.CodeAnalysis.Workspaces.dll
copy /Y %ROSLYNPATH%\Microsoft.CodeAnalysis.Workspaces.pdb
copy /Y %ROSLYNPATH%\System.Collections.Immutable.dll
copy /Y %ROSLYNPATH%\System.Collections.Immutable.pdb
copy /Y %ROSLYNPATH%\System.Reflection.Metadata.Ecma335.dll
copy /Y %ROSLYNPATH%\System.Reflection.Metadata.Ecma335.pdb

goto end

:errorEnd
echo Usage: CopyRoslynBinaries.bat ^<path^> (like \\roslyn-build1\Drops\Signed\Signed_20140110.1)
exit /b 1

:end
exit /b 0
