main : Completeness.tex DefinizioniFrancesco.tex Packages.tex bib.bib 
	latex Completeness.tex

pdf : Completeness.tex DefinizioniFrancesco.tex Packages.tex bib.bib
	pdflatex Completeness.tex

bib:
	bibtex Completeness

view: pdf
	Completeness.pdf
