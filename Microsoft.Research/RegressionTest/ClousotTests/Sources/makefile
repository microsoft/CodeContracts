CLOUSOT = \users\maf\cci1\microsoft.research\Clousot\bin\debug\clousot.exe
CLOUSOTOPTIONS = /nologo -repro -wp=false /libpaths:..\bin\debug -regression -show:validations;unreached -useZ3 -nonnull
CSHARPREFS = /r:..\..\..\imported\ReferenceAssemblies\v3.5\mscorlib.dll /r:..\..\..\Contracts\bin\Debug\v3.5\Microsoft.Contracts.dll /r:..\bin\debug\ClousotTestHarness.dll
CSHARPOPTIONS = /nologo /d:CONTRACTS_FULL

z3test1.clt: z3test1.dll
z3test2.clt: z3test2.dll
z3test3.clt: z3test3.dll
z3test4.clt: z3test4.dll
z3test5.clt: z3test5.dll
z3test6.clt: z3test6.dll
z3test7.clt: z3test7.dll
z3test8.clt: z3test8.dll
z3test9.clt: z3test9.dll
z3test10.clt: z3test10.dll
z3test11.clt: z3test11.dll
z3test12.clt: z3test12.dll
z3test13.clt: z3test13.dll
z3test14.clt: z3test14.dll
z3test15.clt: z3test15.dll
z3test16.clt: z3test16.dll
z3test17.clt: z3test17.dll
z3test18.clt: z3test18.dll
z3test19.clt: z3test19.dll
z3test20.clt: z3test20.dll
z3test21.clt: z3test21.dll
z3test22.clt: z3test22.dll
z3test23.clt: z3test23.dll
z3test24.clt: z3test24.dll

all: \
	z3test1.clt \
	z3test2.clt \
	z3test3.clt \
	z3test4.clt \
	z3test5.clt \
	z3test6.clt \
	z3test7.clt \
	z3test8.clt \
	z3test9.clt \
	z3test10.clt \
	z3test11.clt \
	z3test12.clt \
	z3test13.clt \
	z3test14.clt \
	z3test15.clt \
	z3test16.clt \
	z3test17.clt \
	z3test18.clt \
	z3test19.clt \
	z3test20.clt \
	z3test21.clt \
	z3test22.clt \
	z3test23.clt \
	z3test24.clt \



clean:
	del z3test*.dll
	del z3test*.pdb
	del z3test*.clt
  del *~

.SUFFIXES: .cs .dll .clt

.dll.clt:
	$(CLOUSOT) -libpaths "..\..\..\imported\ReferenceAssemblies\v3.5" -libpaths "..\..\..\Contracts\bin\Debug\v3.5" $(CLOUSOTOPTIONS) $< >$@
	type $@

.cs.dll:
	csc /debug /t:library /nostdlib $(CSHARPREFS) $(CSHARPOPTIONS) /out:$@ $<
