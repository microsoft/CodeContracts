

UserDoc.pdf: UserDoc.tex VSUI1.png example1.dll
	pdflatex UserDoc.tex
	pdflatex UserDoc.tex
	pdflatex UserDoc.tex

show: UserDoc.pdf
	UserDoc.pdf

clean:
	del /s /q userdoc.pdf
	del /s /q userdoc.aux
	del /s /q userdoc.log
	del /s /q userdoc.out
	del /s /q userdoc.toc
  del /s /q example1.dll

examples: example1.dll

example1.dll: example1.cs
	-csc /d:CONTRACTS_FULL /t:library example1.cs
  -..\driver\bin\debug\foxtrot.exe example1.dll -libpaths ..\..\Microsoft.Research\Contracts\bin\debug\v3.5
