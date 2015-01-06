BASE=main
TEXFILES = $(addsuffix .tex, $(BASE))
PDFFILES = $(addsuffix .pdf, $(BASE)) 
ALLPDFFILES = $(PDFFILES)
ABSTRACT = abstract

LATEX = pdflatex --file-line-error --shell-escape --recorder --synctex=1

BIBTEX = bibtex

.PHONY:all
all: delete $(PDFFILES)

.PHONY:help
help:
	@echo "make [all]  : typeeset $(PDFFILES)"
	@echo "make help   : this help"
	@echo "make clean  : delete auxiliary files"
	@echo "make clean  : delete non-tex files"
	@echo "make up     : update svn"
	@echo "make ci     : commit svn"
    

%.pdf: %.tex biblio.bib
	echo "\\longfalse" > short-long.tex
	${LATEX} $<
	${LATEX} $<
	-${BIBTEX} $(basename $<)
	${LATEX} $<"




.PHONY:clean
clean:
	/bin/rm -f $(addsuffix .aux, $(BASE)) $(addsuffix .bbl, $(BASE)) $(addsuffix .blg, $(BASE)) $(addsuffix .fld, $(BASE)) $(addsuffix .fls, $(BASE)) $(addsuffix .log, $(BASE)) $(addsuffix .out, $(BASE)) $(addsuffix .synctex.gz, $(BASE)) $(BASE)*.*~ $(addsuffix .thm, $(BASE))
	/bin/rm -f ${ABSTRACT}*.aux ${ABSTRACT}*.bbl ${ABSTRACT}*.blg ${ABSTRACT}*.fld ${ABSTRACT}*.fls ${ABSTRACT}*.log ${ABSTRACT}*.out ${ABSTRACT}*.synctex.gz *.*~ makefile~ 

.PHONY:delete
delete:clean
	/bin/rm -f $(PDFFILES)
	/bin/rm -f ${ABSTRACT}*.pdf

#.PHONY:zip
#zip: typeset
#	/bin/rm -f ${FILE}.zip; /bin/rm -f *~; zip -r  ${FILE}.zip *
#	mv ${FILE}-`date +"%Y-%m-%d-%Mm%Ss"`.zip ..

.PHONY:update
update: 
	svn update

.PHONY:commit
commit: update
	@echo `date` > msg.txt
	@echo `whoami` >> msg.txt
	svn commit --non-interactive --force-log --file ./msg.txt
	@/bin/rm -f msg.txt

.PHONY:ci
ci: commit

.PHONY:up
up: update
