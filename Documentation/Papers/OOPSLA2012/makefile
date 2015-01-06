BASENAME=paper
JOBNAME=paper

$(JOBNAME).pdf: $(BASENAME).tex
	pdflatex $(BASENAME) -jobname $(JOBNAME)
	bibtex $(BASENAME)
	pdflatex $(BASENAME) -jobname $(JOBNAME)
	pdflatex $(BASENAME) -jobname $(JOBNAME)


clean:
	-del $(JOBNAME).pdf
	-del $(JOBNAME).log
	-del $(JOBNAME).aux

show: $(JOBNAME).pdf
	$(JOBNAME).pdf




