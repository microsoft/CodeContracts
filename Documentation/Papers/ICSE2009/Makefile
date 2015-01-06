
icse2009demo.pdf: icse2009demo.tex icse2009demo.bbl
	pdflatex icse2009demo
	pdflatex icse2009demo
	pdflatex icse2009demo

bib: icse2009demo.bbl

icse2009demo.bbl: icse2009demo.aux bib.bib
	bibtex icse2009demo

icse2009demo.aux:
	touch icse2009demo.aux

clean:
	del /q icse2009demo.pdf

view: icse2009demo.pdf
	icse2009demo.pdf
