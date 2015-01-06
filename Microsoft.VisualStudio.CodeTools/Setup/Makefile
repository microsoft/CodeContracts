#--------------------------------------------------------------------------
# sources
#--------------------------------------------------------------------------
MAIN	= Contracts
SRCS	= Contracts.wxs UI.wxs
CPPS    = AddBuildTargets.cpp
DIRS	= ITaskManager TaskManager IPropertyPane PropertyPage ContractsPropertyPane


#--------------------------------------------------------------------------
# directories
#--------------------------------------------------------------------------
ifeq ($(VARIANT),)
VARIANT=Debug
endif

SRCDIR	=.
BINDIR	=bin
OUTDIR	=$(BINDIR)/$(VARIANT)
CUSTOMDIR=custom
MAINMSI	=$(OUTDIR)/$(MAIN).msi

#--------------------------------------------------------------------------
# Set search directories based on the VARIANT: debug or release
#--------------------------------------------------------------------------
BDIR=bin/$(VARIANT)
BASEDIRS= -b .. $(patsubst %,-b ../%/$(BDIR),$(DIRS))

# set forward slash to windows backward slash
winpath=$(subst /,\,$(1))

# set dependent dll's
DLLS=$(patsubst %.cpp,$(OUTDIR)/%.dll, $(CPPS))

WIX-DLLS= $(wildcard $(patsubst %,../%/$(BDIR)/*.dll,$(DIRS))) $(DLLS)
	  

#--------------------------------------------------------------------------
# Main targets
#--------------------------------------------------------------------------
all: dirs $(MAINMSI) 
	msiexec /i $(call winpath, $(MAINMSI)) /log $(OUTDIR)/install.log

clean: 
	rm -f -r $(BINDIR)/*

dirs: 
	mkdir -p $(OUTDIR)

#--------------------------------------------------------------------------
# Derive object files
#--------------------------------------------------------------------------
WIXS= $(filter %.wxs,$(SRCS))
WIX-SRCS= $(patsubst %,$(SRCDIR)/%,$(WIXS))
WIX-OBJS= $(patsubst %.wxs,$(OUTDIR)/%.wixobj,$(WIXS))

#--------------------------------------------------------------------------
# Dependency rules
#--------------------------------------------------------------------------
$(MAINMSI): $(WIX-OBJS) $(WIX-DLLS)
	light -out $@ $(BASEDIRS) $(WIX-OBJS)

$(WIX-OBJS): $(OUTDIR)/%.wixobj: $(SRCDIR)/%.wxs
	candle -out $@ $<

$(DLLS): $(OUTDIR)/%.dll: $(CUSTOMDIR)/%.cpp 
	cmd /c "cl /Fe$(call winpath,$(OUTDIR))\ /Fo$(call winpath,$(OUTDIR))\ /LD $(call winpath,$<) $(CUSTOMDIR)\$*.def msi.lib user32.lib"


$(OUTDIR)/Contracts.wixobj: $(SRCDIR)/Contracts.wxs $(SRCDIR)/UI.wxs Binary/License.rtf
$(OUTDIR)/UI.wixobj: $(SRCDIR)/UI.wxs

