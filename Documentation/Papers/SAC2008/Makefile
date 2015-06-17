
main: pentagons.tex DefinizioniFrancesco.tex Packages.tex pentagons.bbl Intervals.eps Octagons.eps Original.eps Pentagons.eps  
  latex pentagons.tex

ps: main
  dvips pentagons.dvi

pdf: main
  dvipdfm pentagons.dvi

pentagons.bbl: bib.bib
  bibtex pentagons

bib:
  bibtex pentagons

Intervals.eps: Intervals.png
  bmeps -c -p 2 -e r  Intervals.png Intervals.eps

Octagons.eps: Octagons.png
  bmeps -c -p 2 -e r  Octagons.png Octagons.eps

Original.eps: Original.png
  bmeps -c -p 2 -e r Original.png Original.eps

Pentagons.eps: Pentagons.png
  bmeps -c -p 2 -e r Pentagons.png Pentagons.eps

show: pdf
	pentagons.pdf


edit:
	sd edit pentagons.tex
	sd edit definizioniFrancesco.tex
	sd edit Packages.tex
	sd edit bib.bib
	sd edit Intervals.png
	sd edit Octagons.png
	sd edit Pentagons.png
	sd edit Original.png
