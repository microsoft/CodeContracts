// CodeContracts
// 
// Copyright (c) Microsoft Corporation
// 
// All rights reserved. 
// 
// MIT License
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED *AS IS*, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Microsoft.Research.DataStructures;

namespace Microsoft.Contracts.Foxtrot.Driver
{
    internal class FoxtrotOptions : OptionParsing
    {
        [OptionDescription("Automatically load out-of-band contracts.", ShortForm = "autoRef")]
        public bool automaticallyLookForOOBs = true;

        [OptionDescription("Silently do nothing if the input assembly has already been rewritten.")]
        public bool allowRewritten = false;

        [OptionDescription("Causes the debugger to be invoked.", ShortForm = "break")]
        public bool breakIntoDebugger = false;

        [OptionDescription("Out-of-band contract assemblies.")]
        public List<string> contracts = null;

        [OptionDescription("Use debug information.")] 
        public bool debug = true;

        [OptionDescription("produce non-zero return code when warnings are found")] 
        public bool failOnWarnings = false;

        [OptionDescription("Hide rewritten contract methods from debugger.")] 
        public bool hideFromDebugger = true;

        [OptionDescription("Full paths to candidate dlls to load for resolution.")] 
        public List<string> resolvedPaths =
            null;

        [OptionDescription("Additional paths to search for referenced assemblies.")] 
        public List<string> libpaths = null;

        [OptionDescription("Instrumentation level: 0=no contracts, 1=ReleaseRequires, 2=Requires, 3=Ensures, 4=Invariants. (Each increase includes the previous)")] 
        public int level = 4; // default: all contracts

        [OptionDescription("Contract recursion level at which to stop evaluating contracts")]
        public int recursionGuard= 4;

        [OptionDescription("Throw ContractException on contract failure", ShortForm = "throw")] 
        public bool throwOnFailure = false;

        [OptionDescription("Remove all contracts except those visible from outside assembly", ShortForm = "publicSurface")]
        public bool publicSurfaceOnly = false; // default everywhere

        [OptionDescription("Instrument call sites with requires checks where necessary", ShortForm = "csr")]
        public bool callSiteRequires = false;

        [OptionDescription("Output path for the rewritten assembly.", ShortForm = "out")] 
        public string output = "same";

        [OptionDescription("Write PDB file. Cannot be specified unless /debug is also specified")]
        public bool writePDBFile = true;

        [OptionDescription("Copy original files (using .original extension)", ShortForm = "originalFiles")] 
        public bool keepOriginalFiles = false;

        [OptionDescription("Don't actually use the rewriter, but pass the assembly through CCI.")] 
        public bool passthrough = false;

        [OptionDescription("Rewrites the given assembly to insert runtime contract checks.")] 
        public bool rewrite = true;

        [OptionDescription("Alternative methods to use for checking contracts at runtime. Syntax: /rw:<assembly>,<class name> or /rw:<assembly>,<namespace name>,<class name>.", ShortForm = "rw")] 
        public string rewriterMethods = null;

        [OptionDescription("Preserve short branches in the rewritten assembly.")] 
        public bool shortBranches = false;

        [OptionDescription("Extract the source text for the contracts. (Requires /debug.)")] 
        public bool extractSourceText = true;

        [OptionDescription("Path to alternate core library (and set of framework assemblies).", ShortForm = "platform")] 
        public string targetplatform = "";

        [OptionDescription("Target .NET framework")] 
        public string framework = "";

        [OptionDescription("Print out extra information.")] 
        public int verbose = 0;

        [OptionDescription("Don't throw up assert listener boxes.")] 
        public bool nobox = false;

        [OptionDescription("Don't print version.")] 
        public bool nologo = false;

        [OptionDescription("Verify the output of rewriting and fail if it verified before but not after.")]
        public bool verify = true;

        [OptionDescription("Set warning level (0-4)")] 
        public uint warn = 1;

        [OptionDescription("Dll/Exe name containing shared contract class")]
        public string contractLibrary = null;

        [OptionDescription("Assembly to process is F#")] 
        public bool fSharp = false;

        public enum AssemblyMode
        {
            standard,
            legacy
        }

        [OptionDescription("Set to legacy if assembly uses if-then-throw parameter validation")]
        public AssemblyMode assemblyMode = AssemblyMode.legacy;

        [OptionDescription("Write repro.bat for debugging")] 
        public bool repro = false;

        [OptionDescription("Inherit invariants across assemblies", ShortForm = "ii")] 
        public bool inheritInvariants = true;

        [OptionDescription("Skip contracts containing quantifiers", ShortForm = "sq")]
        public bool skipQuantifiers =
            false;

        [OptionDescription("Assembly to process.")] 
        public string assembly = null;

        [OptionDescription("Add interface wrappers to hold inherited contracts when a method used for interface implementation is inherited but does not implement the interface method", ShortForm = "iw")]
        public bool addInterfaceWrappersWhenNeeded = true;

        [OptionDescription("Use global assembly cache")] 
        public bool useGAC = true;

        [OptionDescription("Suppress warnings")]
        public List<int> nowarn = new List<int>();

        [OptionDescription("Don't abort due to metadata errors")] 
        public bool ignoreMetadataErrors = false;

        private void HACK_FORCLOUSOT()
        {
            // set the fields to avoid the warning from clousot

            automaticallyLookForOOBs = true;
            allowRewritten = false;
            breakIntoDebugger = false;
            contracts = null;
            debug = true;
            failOnWarnings = false;
            hideFromDebugger = true;
            resolvedPaths = null;
            libpaths = null;
            level = 4;
            recursionGuard = 4;
            throwOnFailure = false;
            publicSurfaceOnly = false;
            callSiteRequires = false;
            output = "same";
            writePDBFile = true;
            keepOriginalFiles = false;
            passthrough = false;
            rewrite = true;
            rewriterMethods = null;
            shortBranches = false;
            extractSourceText = true;
            targetplatform = "";
            framework = "";
            verbose = 0;
            nobox = false;
            nologo = false;
            verify = true;
            warn = 1;
            contractLibrary = null;
            fSharp = false;
            assemblyMode = AssemblyMode.legacy;
            repro = false;
            inheritInvariants = true;
            skipQuantifiers = false;
            assembly = null;
            addInterfaceWrappersWhenNeeded = true;
            useGAC = true;
            nowarn = new List<int>();
            ignoreMetadataErrors = false;
        }

        private int ErrorCount;

        internal int GetErrorCount()
        {
            return this.ErrorCount;
        }

        internal void EmitError(System.CodeDom.Compiler.CompilerError error)
        {
            Contract.Requires(error != null);

            if (!error.IsWarning || this.failOnWarnings)
            {
                ErrorCount++;
            }

            if (ShouldEmitErrorWarning(error))
            {
                if (error.IsWarning && this.failOnWarnings)
                    error.IsWarning = false;

                Console.WriteLine(error.ToString());
            }
        }

        private bool ShouldEmitErrorWarning(System.CodeDom.Compiler.CompilerError error)
        {
            Contract.Requires(error != null);

            if (!error.IsWarning || this.failOnWarnings) return true;

            if (warn <= 0) return false;

            var eName = error.ErrorNumber;
            if (eName != null && eName.StartsWith("CC") && eName.Length >= 3)
            {
                int num;
                if (Int32.TryParse(eName.Substring(2), out num))
                {
                    if (nowarn.Contains(num)) return false;
                }
            }

            return true;
        }

        public bool IsLegacyModeAssembly
        {
            get { return this.assemblyMode == AssemblyMode.legacy; }
        }
    }
}