NAME=Readme
COMMON=


all: prepare $(NAME).pdf
	cd ApiProtocols & nmake
	cd BinarySearch & nmake
	cd Chunker & nmake
	cd GCD & nmake
	cd InterfaceContracts & nmake
	cd Invariants & nmake
	cd Rational & nmake
        cd NonNullStack & nmake
	sd revert -a ...

view: $(NAME).pdf
	$(NAME).pdf

prepare:
	sd edit ...

clean:
	del $(NAME).pdf

$(NAME).pdf: prepare $(NAME).tex $(COMMON)
	pdflatex $(NAME)
	pdflatex $(NAME)

