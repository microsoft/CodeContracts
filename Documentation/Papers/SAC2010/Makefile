BASENAME=cc

$(BASENAME).pdf: $(BASENAME).tex $(BASENAME).bst
	pdflatex $(BASENAME)
	pdflatex $(BASENAME)
	pdflatex $(BASENAME)

$(BASENAME).bst: $(BASENAME).bib
	bibtex $(BASENAME)


clean:
	-del $(BASENAME).pdf
	-del $(BASENAME).log
	-del $(BASENAME).aux

show: $(BASENAME).pdf
	$(BASENAME).pdf




