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
SOURCEPANE=CodeContractsVSPropertyPane
TARGET=CodeContracts

MAIN=CodeContracts
OBJS= $(OUTDIR)\ManagedContracts.wixobj $(OUTDIR)\UI.wixobj 

CODETOOLS = ..\..\Microsoft.VisualStudio.CodeTools
CLOUSOT = ..\Clousot\$(BDIR)
FOXTROT = ..\..\Foxtrot\Driver\$(BDIR)

BDIR	= bin\$(VARIANT)
BASEDIRS= -b $(CODETOOLS)\ITaskManager\$(BDIR) \
	  -b $(CODETOOLS)\TaskManager\$(BDIR) \
	  -b $(CODETOOLS)\IPropertyPane\$(BDIR) \
	  -b $(CODETOOLS)\PropertyPage\$(BDIR) \
	  -b ..\ManagedContractsVSPropertyPane\$(BDIR) \
	  -b $(CLOUSOT) \
	  -b $(FOXTROT) \
    -b ..\CodeFixes\$(BDIR) \
    -b ..\ImportedCCI2 \
    -b ..\Z3\$(BDIR) \


FILES	= Microsoft.CodeContracts.targets \
	  $(OUTDIR)\AddBuildTargets.dll \
	  $(CODETOOLS)\ITaskManager\$(BDIR)\ITaskManager.dll \
		$(CODETOOLS)\TaskManager\$(BDIR)\TaskManager.dll \
	  $(CODETOOLS)\TaskManager\$(BDIR)\1033\TaskManagerUI.dll \
	  $(CODETOOLS)\IPropertyPane\$(BDIR)\IPropertyPane.dll \
	  $(CODETOOLS)\PropertyPage\$(BDIR)\PropertyPage.dll \
	  ..\ManagedContractsVSPropertyPane\$(BDIR)\$(SOURCEPANE).dll \
    $(CLOUSOT)\clousot.exe \
    "$(CLOUSOT)\Abstract Domains.dll" \
    $(CLOUSOT)\AIFramework.dll \
    $(CLOUSOT)\Analyzers.dll \
    $(CLOUSOT)\CCI1CodeProvider.dll \
    $(CLOUSOT)\ClousotMain.dll \
    $(CLOUSOT)\CodeAnalysis.dll \
    $(CLOUSOT)\CodeFixes.dll \
    $(CLOUSOT)\DataStructures.dll \
    $(FOXTROT)\Decompiler.dll \
    $(CLOUSOT)\Graphs.dll \
    $(CLOUSOT)\Microsoft.Contracts.Deserializer.dll \
    $(CLOUSOT)\Microsoft.Contracts.dll \
    ..\CodeFixes\$(BDIR)\Microsoft.ExtendedReflection.dll \
    $(CLOUSOT)\System.Compiler.dll \
    $(CLOUSOT)\System.Compiler.Runtime.dll \
	  $(FOXTROT)\foxtrot.exe \
	  $(FOXTROT)\Foxtrot.Extractor.dll \


MAINMSI = $(OUTDIR)\$(MAIN).msi

#--------------------------------------------------------------------------
# Main target
#--------------------------------------------------------------------------
.SUFFIXES: .wxs .wixobj

all: cci2 makedirs $(MAINMSI) drop
	msiexec /i $(MAINMSI) /log $(OUTDIR)\install.log

drop: $(MAINMSI)
	-copy /y $(MAINMSI) \\maf2\drop\Contracts

clean: 
	del /Q /S $(BINDIR)\*

uninstall:
	msiexec /x $(MAINMSI) /log $(OUTDIR)\uninstall.log

overwritetarget:
	copy /y Microsoft.CodeContracts.targets "$(PROGRAMFILES)\Microsoft\Contracts\MsBuild"
	copy /y Microsoft.CodeContractAnalysis.targets "$(PROGRAMFILES)\Microsoft\Contracts\MsBuild"
	xcopy /y CSharpCodeSnippets\* "$(PROGRAMFILES)\Microsoft\Contracts\Languages\CSharp\Code Contract Snippets"
	xcopy /y VBCodeSnippets\* "$(PROGRAMFILES)\Microsoft\Contracts\Languages\VisualBasic\Code Contract Snippets"

#--------------------------------------------------------------------------
# Dependency rules
#--------------------------------------------------------------------------
$(MAINMSI): $(OBJS) $(FILES)
	light -ext WixUtilExtension -ext WixNetFxExtension -ext WixVSExtension -out $@ $(BASEDIRS) $(OBJS)

{$(SRCDIR)}.wxs{$(OUTDIR)}.wixobj:
	candle -ext WixUtilExtension -ext WixNetFxExtension -ext WixVSExtension -out $@ $<

$(OUTDIR)\ManagedContracts.wixobj: $(SRCDIR)\ManagedContracts.wxs $(SRCDIR)\UI.wxs

$(OUTDIR)\UI.wixobj: $(SRCDIR)\UI.wxs


$(OUTDIR)\AddBuildTargets.dll: $(CUSTOM)\AddBuildTargets.cpp $(CUSTOM)\AddBuildTargets.def
	cl /Fe$(OUTDIR)\ /Fo$(OUTDIR)\ /LD $** msi.lib user32.lib

.IGNORE:

makedirs:
	@mkdir  $(OUTDIR)

cci2:
	cd ..\ImportedCCI2 & sd edit * & nmake & sd revert -a ...
