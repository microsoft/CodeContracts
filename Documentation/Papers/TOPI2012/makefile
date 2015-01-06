NAME=ccvs
COMMON=

$(NAME).pdf: $(NAME).tex $(COMMON) $(NAME).bib
	pdflatex $(NAME)
  bibtex $(NAME)
	pdflatex $(NAME)
	pdflatex $(NAME)

edit:
	sd edit $(NAME).tex
	sd edit $(NAME).bib
	runemacs $(NAME).tex

show: $(NAME).pdf
	$(NAME).pdf

clean:
	del $(NAME).pdf
	del $(NAME).log
  del $(NAME).aux
  del *~

