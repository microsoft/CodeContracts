ACMNAME=res0000081-ferrara

view: unsafe.pdf
	unsafe.pdf


unsafe.pdf: Unsafe.tex DefinizioniFrancesco.tex Packages.tex bib.bib Concrete.png Intervals.png Octagons.png Polyhedra.png Stripes.png ClousotArchitecture.png unsafe.blg
  pdflatex Unsafe.tex

bib: unsafe.blg

unsafe.blg: always
	bibtex unsafe

always:

edit: 
	sd edit Unsafe.tex
	sd edit DefinizioniFrancesco.tex
	sd edit Packages.tex
	sd edit bib.bib
	sd edit	Points.vsd
	sd edit Concrete.png 
	sd edit Intervals.png 
	sd edit Octagons.png 
	sd edit Polyhedra.png 
	sd edit Stripes.png 
  sd edit ClousotStructure.pptx
  sd edit ClousotArchitecture.png


submit:
	copy unsafe.tex $(ACMNAME).tex
	copy unsafe.pdf $(ACMNAME).pdf
	copy unsafe.pdf $(ACMNAME).ps
