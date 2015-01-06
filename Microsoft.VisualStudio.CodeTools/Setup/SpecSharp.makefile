!MESSAGE Make sure both the CSharp plugin DLLs and the SpecSharp DLLs are version $(VERSION)!

!IF "$(VERSION)" == ""
!  ERROR Usage: nmake -f SpecSharp.makefile VERSION=1.0.5301
!ENDIF

!IF !DEFINED(VARIANT)
VARIANT=Debug
!ENDIF

#--------------------------------------------------------------------------
# sources
#--------------------------------------------------------------------------
SRCDIR=.
BINDIR=bin
OUTDIR=$(BINDIR)\$(VARIANT)
CUSTOM=Custom

MAIN= SpecSharp
OBJS= $(OUTDIR)\SpecSharp.wixobj $(OUTDIR)\UI.wixobj


BDIR	= bin\$(VARIANT)
BASEDIRS= -b ..\ITaskManager\$(BDIR) \
	  -b ..\TaskManager\$(BDIR) \
	  -b ..\IPropertyPane\$(BDIR) \
	  -b ..\PropertyPage\$(BDIR) \
	  -b ..\ContractsPropertyPane\$(BDIR) \
	  -b ..\..\Microsoft.SpecSharp\Wix \


FILES	= Microsoft.Contracts.targets \
	  $(OUTDIR)\AddBuildTargets.dll \
	  ..\ITaskManager\$(BDIR)\ITaskManager.dll \
	  ..\TaskManager\$(BDIR)\TaskManager.dll \
	  ..\TaskManager\$(BDIR)\1033\TaskManagerUI.dll \
	  ..\IPropertyPane\$(BDIR)\IPropertyPane.dll \
	  ..\PropertyPage\$(BDIR)\PropertyPage.dll \
	  ..\ContractsPropertyPane\$(BDIR)\ContractsPropertyPage.dll

MAINMSI = $(OUTDIR)\$(MAIN).msi

#--------------------------------------------------------------------------
# Main target
#--------------------------------------------------------------------------
.SUFFIXES: .wxs .wixobj

all: makedirs $(MAINMSI)
	msiexec /i $(MAINMSI) /log $(OUTDIR)\install.log

clean: 
	del /Q /S $(BINDIR)\*

#--------------------------------------------------------------------------
# Dependency rules
#--------------------------------------------------------------------------
$(MAINMSI): $(OBJS) $(FILES)
	light -out $@ $(BASEDIRS) $(OBJS)

{$(SRCDIR)}.wxs{$(OUTDIR)}.wixobj:
	candle -sw37 -sw58 -dVersion=$(VERSION) -out $@ $<

$(OUTDIR)\SpecSharp.wixobj: $(SRCDIR)\SpecSharp.wxs $(SRCDIR)\UI.wxs
$(OUTDIR)\UI.wixobj: $(SRCDIR)\UI.wxs

$(OUTDIR)\AddBuildTargets.dll: $(CUSTOM)\AddBuildTargets.cpp $(CUSTOM)\AddBuildTargets.def
	cl /Fe$(OUTDIR)\ /Fo$(OUTDIR)\ /LD $** msi.lib user32.lib

.IGNORE:

makedirs:
	@mkdir  $(OUTDIR)

