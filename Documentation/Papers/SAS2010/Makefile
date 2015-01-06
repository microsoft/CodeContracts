NAME=SAS2010
COMMON=

$(NAME).pdf: $(NAME).tex $(NAME).bib $(COMMON)
	pdflatex $(NAME)
  bibtex $(NAME)
	pdflatex $(NAME)
	pdflatex $(NAME)

view: $(NAME).pdf
	$(NAME).pdf

clean:
	del $(NAME).pdf
	del $(NAME).log
  del $(NAME).aux
  del *~

