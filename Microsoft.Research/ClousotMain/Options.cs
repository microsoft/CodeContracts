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
using System.IO;
using System.Diagnostics;
using Microsoft.Research.DataStructures;
using System.Text;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;

namespace Microsoft.Research.CodeAnalysis
{
  #region Enumerations
  
  public enum WarningLevelOptions { low, mediumlow, medium, full }

  public enum InferenceMode { Normal, Aggressive}

  public enum InferOptions { arrayrequires, arraypurity, methodensures, nonnullreturn, symbolicreturn, propertyensures, requires, objectinvariants, objectinvariantsforward, assumefalse, autopropertiesensures }

  public enum SuggestOptions { requires, propertyensures, methodensures, nonnullreturn, necessaryensures, arrayrequires, arraypurity, objectinvariants, objectinvariantsforward, assumes, codefixes, codefixesshort, readonlyfields, requiresbase, callinvariants, calleeassumes, asserttocontracts }

  public enum SuggestionsAsWarnings { requires, propertyensures, methodensures, nonnullreturn, necessaryensures, arrayrequires, arraypurity, objectinvariants, objectinvariantsforward, assumes, codefixes, codefixesshort, readonlyfields, requiresbase, callinvariants, calleeassumes, redundantassume, unusedsuppress, asserttocontracts }  

  public enum StatOptions { valid, time, mem, perMethod, arithmetic, asserts, methodasserts, slowmethods, abstractdomains, program, egraph, phases, inference, timeperMethod, controller }

  public enum CheckOptions { assertions, exists, assumptions, falseassumptions, inferredrequires, conditionsvalidity, falsepostconditions, entrycontradictions }

  public enum AnalyzeOptions { closures, movenext, compilergenerated }

  public enum TraceOptions { dfa, heap, expressions, egraph, assumptions, partitions, wp, arrays, numerical, timings, memory, cache, checks, inference, loading, cachehashing, warningcontexts, movenext, suspended }

  public enum ShowOptions { progress, il, errors, validations, unreached, progressnum, progressbar, obligations, paths, invariants, warnranks, analysisphases, scores, inferencetrace, externallyvisiblemembersonly, cachemisses }

  public enum AssemblyMode { standard, legacy }

  public enum PreconditionInferenceMode { aggressive, allPaths, backwards, combined}

  public enum BaseLiningOptions { mixed, ilBased, typeBased }

  // Experiment with different implementations for the sparsearrays
  public enum SparseArrayOptions { time, mem, exp }

  #endregion

  /// <summary>
  /// Put any option as a public field. The name is what gets parsed on the command line.
  /// It can be bool, int, string, or List&lt;string&gt;, or List&lt;int&gt; or enum type
  ///
  /// Derived options are public const strings
  /// </summary>
  public class GeneralOptions : OptionParsing, ILogOptions, Caching.IClousotCacheOptions {

    #region Parsing
    public static GeneralOptions ParseCommandLineArguments(string[] args, Dictionary<string, IMethodAnalysis> analyzers, Dictionary<string, IClassAnalysis> classanalyzers)
    {
      Contract.Requires(args != null);
      Contract.Requires(analyzers != null);
      Contract.Requires(classanalyzers != null);

      Contract.Ensures(Contract.Result<GeneralOptions>() != null);

      var options = new GeneralOptions(analyzers, classanalyzers);
      options.Parse(args);
      
#if DEBUG && false
      Console.WriteLine("[Slicer @ {0}] Slice TimeStamp: {1}", DateTime.Now, DateTime.FromBinary(options.sliceTime));
#endif
      
      return options;
    }

    private readonly Dictionary<string, IMethodAnalysis> analyzers;
    private readonly Dictionary<string, IClassAnalysis> classanalyzers;

    protected GeneralOptions(Dictionary<string, IMethodAnalysis> analyzers, Dictionary<string, IClassAnalysis> classanalyzers)
    {
      this.analyzers = analyzers;
      this.classanalyzers = classanalyzers;

      this.sliceTime = DateTime.Now.ToBinary(); // F: setting it here so that we can debug
    }

    protected override bool ParseUnknown(string arg, string[] args, ref int index, string equalArgument)
    {
      // don't care about case
      arg = arg.ToLower();

      // see if it is a break
      if (arg == "break")
      {
        System.Diagnostics.Debugger.Launch();
        return true;
      }

      if (arg == "echo")
      {
        int j = 0;
        foreach (var s in args)
        {
          Console.WriteLine("arg{0} = '{1}'", j, s);
          j++;
        }
        Console.WriteLine("current dir = '{0}'", Environment.CurrentDirectory);
        return true;
      }

      if (arg == "echocmd")
      {
        Console.WriteLine(Environment.CommandLine);

        return true;
      }

      // lookup in analyzers

      // find arguments
      string options = equalArgument ?? "";

      IMethodAnalysis candidate;
      if (analyzers.TryGetValue(arg, out candidate)) {
        // found analyzer
        bool success = candidate.Initialize(this, options.Split(','));
        if (!success)
        {
          this.AddError("Analysis '{0}' failed to initialize", candidate.Name);
        }
        else
        {
          if (!this.Analyses.Contains(candidate))
          {
            this.Analyses.Add(candidate);
          }
        }
        return true;
      }

      // lookup for class analyzer
      IClassAnalysis clcandidate;
      if (classanalyzers.TryGetValue(arg, out clcandidate))
      {
        // found analyzer
        bool success = clcandidate.Initialize(this, options.Split(','));
        if (!success)
        {
          this.errors++;
        }
        else
        {
          if (!this.ClassAnalyses.Contains(clcandidate))
          {
            this.ClassAnalyses.Add(clcandidate);
          }
        }
        return true;
      }
      return false;
    }

    public void PrintUsage(TextWriter output)
    {
      output.WriteLine("usage: <general-option>* [<analysis> <analysis-options>]+ <assembly>+");
      output.WriteLine(Environment.NewLine + "where <general-option> is one of");
      this.PrintOptions("", output);
      output.WriteLine(Environment.NewLine + "where derived options are of");
      this.PrintDerivedOptions("", output);
      output.WriteLine(Environment.NewLine + "where <analysis> is one of");
      foreach (string key in analyzers.Keys)
      {
        output.WriteLine("   -{0}[:<comma-separated-options>]", key);
        IMethodAnalysis analysis = analyzers[key];
        analysis.PrintOptions("  ", output);
      }
      foreach (string key in classanalyzers.Keys)
      {
        output.WriteLine("   -{0}[:<comma-separated-options>]", key);
        IClassAnalysis analysis = classanalyzers[key];
        analysis.PrintOptions("  ", output);
      }
    }

    #endregion 
      
    #region Atomic Options, i.e. actual state of options

    [OptionDescription("Filters the warnings according to their score")]
    [DoNotHashInCache]
    public WarningLevelOptions warninglevel = WarningLevelOptions.full;

    [OptionDescription("Optional Checks")]
    public List<CheckOptions> check = new List<CheckOptions>() { CheckOptions.assertions, CheckOptions.exists, CheckOptions.entrycontradictions };

    [OptionDescription("Special methods")]
    [DoNotHashInCache]
    public List<AnalyzeOptions> analyze = new List<AnalyzeOptions>() { };

    [DoNotHashInCache]
    public List<TraceOptions> trace = new List<TraceOptions>();

    [DoNotHashInCache]
    public List<ShowOptions> show = new List<ShowOptions>(new ShowOptions[] { ShowOptions.errors });

    [DoNotHashInCache]
    public List<StatOptions> stats = new List<StatOptions>(new StatOptions[]{StatOptions.valid, StatOptions.time, StatOptions.inference});

    #region Inference options

    [OptionDescription("Infer preconditions from exit states")]
    public bool prefrompost = false;

    public InferenceMode inferencemode = InferenceMode.Normal; 

    public List<InferOptions> infer = new List<InferOptions>() { InferOptions.propertyensures,  InferOptions.nonnullreturn, InferOptions.symbolicreturn, InferOptions.arraypurity };

    public List<SuggestOptions> suggest = new List<SuggestOptions>() {};

    [OptionDescription("Disable the inference of object invariants from constructors. Only useful to recover from bugs in the analyzer")]
    public bool disableForwardObjectInvariantInference = false;

    [OptionDescription("Emit a warning instead of a suggestion")]
    [DoNotHashInCache]
    public List<SuggestionsAsWarnings> warnIfSuggest = new List<SuggestionsAsWarnings>();

    [OptionDescription("Allow inference of disjunctive preconditions")]
    public bool infdisjunctions = true;

    [OptionDescription("When -suggest callee assumes, show also disjunctions")]
    public bool suggestcalleeassumeswithdisjunctions = false;

    [OptionDescription("Generate object invariants only for readonly fields")]
    public bool infreadonlyonly = true;

    [OptionDescription("Allow inference of requires from throws of ArgumentException")]
    public bool throwArgExceptionAsAssert = false;

    [OptionDescription("Missing Requires for public surface methods generate warnings")]
    public bool missingPublicRequiresAreErrors = false;

    [OptionDescription("Suggest ensures for externally visible members only")]
    public bool suggestionsForExternalVisibleOnly = false;

    #endregion

    #region Abstract domains options

    [OptionDescription("Select the precondition inference algorithm")]
    public PreconditionInferenceMode premode = PreconditionInferenceMode.allPaths;

    [OptionDescription("Hints to infer bounds for the analysis")]
    public List<Int32> thresholds = new List<int>() { -1, 1 };

    [OptionDescription("Optimized representation")]
    public SparseArrayOptions rep = SparseArrayOptions.time;

    [OptionDescription("Caching of expressions during fixpoint computation")]
    public ExpressionCacheMode expcache = ExpressionCacheMode.Time;

    [OptionDescription("Enable decompilation of disjunctions")]
    public bool refinedisjunctions = true;

    [OptionDescription("Run in the extract method refactoring mode to discover (Ps, Qs)")]
    public bool extractmethodmode = false;

    [OptionDescription("Run in the extract method mode to refine (Pm, Qm)")]
    public string extractmethodmodeRefine = null;

    [OptionDescription("Run in the suggest invariant at mode")]
    public bool invariantsuggestmode = false;

    [OptionDescription("Run some abstract domains algorithms in parallel")]
    public bool adpar = false;

    [OptionDescription("Internal cache size for fact queries")]
    public int cachesize = 10000;

    [OptionDescription("Number of joins before applying the widening")]
    public int joinsBeforeWiden = 1;

    [OptionDescription("Enforce the at lease one join for each loop path")]
    public bool enforceFairJoin = false;

    [OptionDescription("Threshold to for Octagonal constraints")]
    public int maxVarsForOctagon = 8;

    [OptionDescription("Threshold for the renamings")]
    public int maxVarsInRenaming = 20; 

    [OptionWitness]
    [OptionDescription("Number of closure steps while checking assertions")]
    public int steps=0;

    #endregion

    #region WPs

    [OptionDescription("Use weakest preconditions")]
    public bool wp = true;

    [OptionDescription("Limit backward WP computation length")]
    public int maxPathSize = 50;

    [OptionDescription("Emit the path condition we cannot prove in the SMT-LIB format")]
    public bool emitSMT2Formula = false;

    #endregion

    #region Egraph

    [OptionDescription("Use incremental joins in egraph computation (internal)")]
    public bool incrementalEgraphJoin = false;

    #endregion

    #region Paths and output files

    [DoNotHashInCache]
    [OptionDescription("Set .NET core library")]
    public string platform;

    [DoNotHashInCache]
    [OptionDescription(".NET framework used")]
    public string framework = "v4.0";

    [DoNotHashInCache]
    public List<string> define = new List<string>();

    [OptionDescription("Search paths for reference assemblies")]
    [DoNotHashInCache]
    [OptionWithPaths]
    public List<string> libPaths = new List<string>(new string[] { "." });

    [OptionDescription("Assemblies needed to compile the input if it is a source file", ShortForm="r")]
    [DoNotHashInCache]
    [OptionWithPaths]
    public List<string> reference = new List<string>();

    [OptionDescription("Candidate paths to dlls/exes for resolving references")]
    [DoNotHashInCache]
    [OptionWithPaths]
    public List<string> resolvedPaths = new List<string>();

    [OptionDescription("Shared contract class library")]
    [DoNotHashInCache]
    public string cclib = "Microsoft.Contracts";

    [OptionDescription("Extract the source text for the contracts")]
    public bool extractSourceText = true;

    [OptionDescription("Paths to source files")]
    [DoNotHashInCache]
    public List<string> sourcePaths = new List<string>();

    [OptionDescription("Alternative paths to search for source files")]
    [DoNotHashInCache]
    public List<string> alternativeSourcePaths = new List<string>();

    [OptionDescription("Redirect the output to this output file")]
    [DoNotHashInCache]
    public string outFile;

    [OptionDescription("Send the output also to Console.Out -- to be used with the outfile option")]
    [DoNotHashInCache]
    public bool WriteAlsoOnOutput = true;

    [OptionDescription("use baseline file, or create if absent")]
    [DoNotHashInCache]
    public string baseLine;

    [OptionDescription("clear exisiting baseline file before starting (default: false)")]
    [DoNotHashInCache]
    public bool clearBaseLine = false;

    [OptionDescription("set this analysis as the baseline in the cache")]
    public bool setCacheBaseLine;

    [OptionDescription("Strategy for suppressing warnings")]
    [DoNotHashInCache]
    public BaseLiningOptions baseLineStrategy = BaseLiningOptions.mixed;

    [OptionDescription("If a method is identical to baseline, suppress all its warnings")]
    [DoNotHashInCache]
    public bool skipIdenticalMethods = true;

    [OptionDescription("Skip methods without a baseline")]
    [DoNotHashInCache]
    public bool skipNewMethods = false;

    [OptionDescription("Use semantic baseline from cache")]
    [DoNotHashInCache]
    public string useSemanticBaseline = null;

    [OptionDescription("Use semantic baseline for method classification but don't apply the baseline")]
    [DoNotHashInCache]
    public bool ignoreBaselineAssumptions;

    [OptionDescription("Save semantic baseline to cache")]
    [DoNotHashInCache]
    public string saveSemanticBaseline = null;

    [OptionDescription("For testing automatic suppression inference")]
    public bool ignoreExplicitAssumptions = false;

    [OptionDescription("Write xml output")]
    [DoNotHashInCache]
    public bool xml = false;

    [OptionDescription("Use contract reference assembly")]
    [DoNotHashInCache]
    public List<string> contract = new List<string>();

    [OptionDescription("The filename of the custom basic scores for warnings")]
    [DoNotHashInCache]
    public string customScores = null;

    [OptionDescription("Be optimistic on external API? We will assign proof obligations depending on that a low score")]
    [DoNotHashInCache]
    public bool lowScoreForExternal = true; 

    #endregion

    #region Method selection

    [OptionDescription("Build the call graph, and use it to determine analysis order")]
    [DoNotHashInCache] // We do not hash it, as we want to reuse results already in the cache
    public bool usecallgraph = true;

    [OptionDescription("Analyze only selected methods (adds dependencies).")]
    [DoNotHashInCache]
    public List<int> select;

    [DoNotHashInCache]
    public int analyzeFrom = 0;
 
    [DoNotHashInCache]
    public int analyzeTo = Int32.MaxValue;

    [DoNotHashInCache]
    [OptionDescription("Split the analysis in several processes")]
    public bool splitanalysis = false;

    [DoNotHashInCache]
    [OptionDescription("Bucket size for the parallel analysis (negative ==> let the analyzer pick)")]
    public int bucketSize = -1;

    [OptionDescription("Analyse only the members with this full name (adds dependencies).")]
    [DoNotHashInCache]
    public string memberNameSelect;

    [OptionDescription("Analyse only the methods in this type, given its full name (adds dependencies).")]
    [DoNotHashInCache]
    public string typeNameSelect;

    [OptionDescription("Analyse only the methods in this namespace (adds dependencies).")]
    [DoNotHashInCache]
    public string namespaceSelect;

    [OptionDescription("Break at selected methods")]
    [DoNotHashInCache]
    public List<int> breakAt;

    [OptionDescription("Include (transitively) the callees of the selected methods")]
    [DoNotHashInCache]
    public bool includeCalleesTransitively = true;

    [OptionDescription("Show il for focused methods")]
    [DoNotHashInCache]
    public List<int> focus;

    [OptionDescription("Show the hash only for the focused methods")]
    [DoNotHashInCache]
    public int focusHash = -1;

    #endregion

    #region Analyses selection

    [DoNotHashInCache]
    List<IMethodAnalysis> analyses = new List<IMethodAnalysis>(); // Analyses to run
    
    [DoNotHashInCache]
    List<IClassAnalysis> classanalyses = new List<IClassAnalysis>(); // Class analyses to run

    #endregion

    #region Trade offs

    [OptionDescription("Analysis timeout per method (in seconds)")]
    public int timeout = 180;

    [OptionDescription("Maximum number of calls per method and analysis")]
    public int maxCalls = Int32.MaxValue;

    [OptionDescription("Maximum number of field reads per method and analysis")]
    public int maxFieldReads = Int32.MaxValue;

    [OptionDescription("Maximum number of joins per method and analysis")]
    public int maxJoins = Int32.MaxValue;

    [OptionDescription("Maximum number of widenings per method and analysis")]
    public int maxWidenings = Int32.MaxValue;

    [OptionDescription("Maximum number of steps per method and analysis")]
    public int maxSteps = Int32.MaxValue;

    [OptionDescription("Adaptive analyses (Use weaker domains for huge methods)")]
    public bool adaptive = false;

    [OptionDescription("Remove a method from the internal method cache when all the method callers have been analyzed")]
    public bool gcMethodCache = false;

    #endregion

    #region Output CSharp

    [OptionDescription("Output inferred contracts as C# code")]
    public bool outputPrettycs;

    [OptionDescription("Output folder for inferred contracts as C# code")]
    [DoNotHashInCache]
    public string outputPrettycsFolder = ".";

    [OptionDescription("Output contracts as C# code, one file per class (default)")]
    [DoNotHashInCache]
    public bool outputPrettyFileClass;

    [OptionDescription("Output contracts as C# code, one file per namespace")]
    [DoNotHashInCache]
    public bool outputPrettyFileNamespace;

    [OptionDescription("Output contracts as C# code, one file per toplevel classes (other classes nested)")]
    [DoNotHashInCache]
    public bool outputPrettyFileToplevelClass;

    [OptionDescription("Output all members as C# code, not just members visible outside assembly")]
    public bool outputPrettyFull;

    #endregion

    #region Caching

    [OptionDescription("Clear the warnings cache")]
    [DoNotHashInCache]
    public bool clearCache = false;

    [OptionDescription("Use the cache to avoid analysis when possible.")]
    [DoNotHashInCache]
    public bool useCache = false;

    [OptionDescription("Write the outcome of the analysis to the cache, so it can be used in a future analysis.")]
    [DoNotHashInCache]
    public bool saveToCache = false;

    [OptionDescription("The name for the cache database (defaults to assembly name)")]
    [DoNotHashInCache]
    public string cacheName = null;

    [OptionDescription("The directory in which the cache database will be written (unless -cacheserver is used)")]
    [DoNotHashInCache]
    public string cacheDirectory = null;

    [OptionDescription("The SQL Server to use for the cache (SQL Server Compact Edition is used locally otherwise, unless forceCacheServer=true)")]
    [DoNotHashInCache]
    public string cacheServer = Environment.GetEnvironmentVariable("CODECONTRACTS_CACHESERVER");

    [OptionDescription("The connection timeout for cache servers")]
    [DoNotHashInCache]
    public int cacheServerTimeout = 5;

    [OptionDescription("Abort the analysis if cannot connect to the cacheserver")]
    [DoNotHashInCache]
    public bool forceCacheServer = false;

    [OptionDescription("Emit an error when we read the cache (for regressions)")]
    [DoNotHashInCache]
    public bool emitErrorOnCacheLookup = false;

    [OptionDescription("The maximum number of methods for which warnings are cached")]
    [DoNotHashInCache]
    public int cacheMaxSize = Int32.MaxValue;

    [OptionDescription("Version identifier for assembly information in database")]
    [DoNotHashInCache]
    public string sourceControlInfo = null;

    [OptionDescription("Name the cache database using a version prefix to guard against version mismatches")]
    [DoNotHashInCache]
    public bool cacheDBVersionPrefix = true;

    [OptionDescription("DateTime.ToBinary() of the slice being analyzed")]
    [DoNotHashInCache]
    public long sliceTime /*= DateTime.Now.ToBinary()*/;
    #endregion

    #region Basic switches

    [DoNotHashInCache]
    public bool nologo;

    [OptionDescription("Don't pop-up IDE boxes")]
    [DoNotHashInCache]
    public bool nobox;

    [OptionDescription("Run regression test on input assemblies")]
    [DoNotHashInCache]
    public bool regression;

    [OptionDescription("Compute scores for warnings")]
    [DoNotHashInCache]
    public bool warnscores = true;

    [OptionDescription("Include suggestions in regression")]
    [DoNotHashInCache]
    public bool includesuggestionsinregression = false;

    [OptionDescription("Prioritize the warnings")]
    [DoNotHashInCache]
    public bool sortwarns = true;

    [OptionDescription("Enable suppression of warnings")]
    [DoNotHashInCache]
    public bool maskwarns = true;

    [OptionDescription("Mask the suggestions from the verified repairs")]
    [DoNotHashInCache]
    public bool maskverifiedrepairs = true;

    [OptionDescription("Outputs the masks to suppress warnings")]
    [DoNotHashInCache]
    public bool outputwarnmasks = false;

    [OptionDescription("Outputs the warnings with the related fixes")]
    [DoNotHashInCache]
    public bool groupactions = false;

    [OptionDescription("Don't try to talk to VS Pex")]
    [DoNotHashInCache]
    public bool nopex;

    [OptionDescription("Limit number of issued warnings overall")]
    [DoNotHashInCache]
    public int maxWarnings = Int32.MaxValue;

    [OptionDescription("Write output formatted for remoting")]
    [DoNotHashInCache]
    public bool remote;

    [OptionDescription("Select whether legacy if-then-throw or Requires<E> are supported")]
    public AssemblyMode assemblyMode = AssemblyMode.legacy;

    [OptionDescription("Write repro.bat for debugging")]
    [DoNotHashInCache]
    public bool repro = false;

    [OptionDescription("produce non-zero return code when warnings are found")]
    [DoNotHashInCache]
    public bool failOnWarnings = false;

    #endregion

    #endregion

    #region Derived options, define as const strings

    public const string statsOnly = "-show=!! -suggest=!!";
    public const string ide = "-stats=!! -trace=!!";
    public const string silent = "-show=!! -stats=!! -trace=!! -nologo";
    public const string cache = "-useCache -saveToCache";

    public const string repairs = "-suggest codefixes -maskverifiedrepairs=false";

    public const string missingPublicEnsuresAreErrors = "-suggestionsForExternalVisibleOnly=true -suggest nonnullreturn -warnifSuggest nonnullreturn";

    public const string scores = "-show warnranks -trace warningcontexts";

    #endregion

    #region Public accessors

    public WarningLevelOptions WarningLevel { get { return this.warninglevel; } }

    #region Tracing

    public bool TraceChecks { get { return this.trace.Contains(TraceOptions.checks); } }
    public bool TraceDFA { get { return this.trace.Contains(TraceOptions.dfa); } }
    public bool TraceHeapAnalysis { get { return this.trace.Contains(TraceOptions.heap); } }
    public bool TraceExpressionAnalysis { get { return this.trace.Contains(TraceOptions.expressions); } }
    public bool TraceEGraph { get { return this.trace.Contains(TraceOptions.egraph); } }
    public bool TraceAssumptions { get { return this.trace.Contains(TraceOptions.assumptions); } }
    public bool TraceWP { get { return this.trace.Contains(TraceOptions.wp); } }
    public bool TracePartitionAnalysis { get { return this.trace.Contains(TraceOptions.partitions); } }
    public bool TraceTimings { get { return this.trace.Contains(TraceOptions.timings); } }
    public bool TraceMemoryConsumption { get { return this.trace.Contains(TraceOptions.memory); } }
    public bool TraceMoveNext { get { return this.trace.Contains(TraceOptions.movenext); } }
    public bool TraceNumericalAnalysis { get { return this.trace.Contains(TraceOptions.numerical); } }
    public bool TraceArrayAnalysis { get { return this.trace.Contains(TraceOptions.arrays); } }
    public bool TraceCache { get { return this.trace.Contains(TraceOptions.cache); } }
    public bool TraceCacheHashing { get { return this.trace.Contains(TraceOptions.cachehashing); } }
   
    public bool TraceSuspended { get { return this.trace.Contains(TraceOptions.suspended); } }

    //public bool TraceCacheHashing(int methodNumber) { return this.trace.Contains(TraceOptions.cachehashing) || methodNumber == this.focusHash; }
    public bool TraceInference { get { return this.trace.Contains(TraceOptions.inference); } }
    public bool TraceLoading { get { return this.trace.Contains(TraceOptions.loading); } }

    #endregion

    public bool EmitSMT2Formula { get { return this.emitSMT2Formula;} }
    public bool EmitErrorOnCacheLookup { get { return this.emitErrorOnCacheLookup; } }
    public bool PrintIL { get { return this.show.Contains(ShowOptions.il); } }
    public int Timeout { get { return this.timeout; } }
    public int MaxCalls { get { return this.maxCalls; } }
    public int MaxFieldReads { get { return this.maxFieldReads; } }
    public int MaxJoins { get { return this.maxJoins; } }
    public int MaxWidenings { get { return this.maxWidenings; } }
    public int MaxSteps { get { return this.maxSteps; } }
    public int AnalyzeTo { get { return this.analyzeTo; } }
    public int AnalyzeFrom { get { return this.analyzeFrom; } }
    public int IterationsBeforeWidening { get { return this.joinsBeforeWiden; } }
    public bool EnforceFairJoin { get { return this.enforceFairJoin; } }
    public int MaxVarsForOctagonInference { get { return this.maxVarsForOctagon; } }

    public int MaxVarsInSingleRenaming { get { return this.maxVarsInRenaming; } }

    public bool IsAdaptiveAnalysis { get { return this.adaptive; } }

    public int Steps { get { return this.steps; } }
    public List<IMethodAnalysis> Analyses { get { return this.analyses; } }
    public List<IClassAnalysis> ClassAnalyses { get { return this.classanalyses; } }
    public List<string> Assemblies { get { return this.GeneralArguments; } }
    public List<string> ContractAssemblies { get { return this.contract; } }
    public bool TurnArgumentExceptionThrowsIntoAssertFalse { get { return this.throwArgExceptionAsAssert; } }
    public bool IgnoreExplicitAssumptions { get { return this.ignoreExplicitAssumptions; } }
    public bool WantToBreak(int methodNumber)
    {
      if (this.breakAt == null)
        return false;

      return this.breakAt.Contains(methodNumber);
    }

    #region Show*
    public bool ShowProgress 
    { 
      get { return this.show.Contains(ShowOptions.progress); } 
    }

    public bool ShowProgressBar
    {
      get { return this.show.Contains(ShowOptions.progressbar); }
    }

    public bool ShowProgressNum
    {
      get { return this.show.Contains(ShowOptions.progressnum); }
    }

    public bool ShowInvariants
    {
      get { return this.show.Contains(ShowOptions.invariants); }
    }

    public bool ShowInferenceTrace
    {
      get
      {
        return this.show.Contains(ShowOptions.inferencetrace);
      }
    }


    #endregion

    #region Output

    public bool IsXMLOutput
    {
      get { return this.xml; }
    }


    public bool IsRemoteOutput
    {
      get { return remote; }
    }

    TextWriter outWriter = null;
    public TextWriter OutFile
    {
      get
      {
        if (outWriter == null)
        {
          if (outFile != null)
          {
            outWriter = new StreamWriter(outFile);

            if (this.WriteAlsoOnOutput)
            {
              outWriter = new TextWriterWithDoubleWrite<TextWriter, TextWriter>(Console.Out, outWriter);
            }
          }
          else
          {
            outWriter = Console.Out;
          }
        }
        return outWriter;
      }
    }

    public const string DefaultOutFileName = "Console.Out";

    public string OutFileName
    {
      get
      {
        if (outFile != null)
        {
          return outFile;
        }
        else
        {
          return DefaultOutFileName;
        }
      }
    }

    #endregion

    #region Regression

    public bool IsRegression { get { return this.regression; } }

    public bool IncludeSuggestionMessagesInRegression { get { return this.IsRegression && this.includesuggestionsinregression; } }

    #endregion

    #region Warnings

    public bool PrioritizeWarnings { get { return this.sortwarns; } }

    public bool MaskedWarnings { get { return this.maskwarns; } }

    public bool MaskedVerifiedRepairs { get { return this.maskverifiedrepairs; } }

    public bool OutputWarningMasks { get { return this.outputwarnmasks; } }

    public bool WarningsWithSuggestions { get { return this.groupactions; } }


    #endregion

    public bool PrintOutcome(ProofOutcome outcome)
    {
      switch (outcome) {
        case ProofOutcome.True:
          return this.show.Contains(ShowOptions.validations);

        case ProofOutcome.Bottom:
          return
            this.show.Contains(ShowOptions.validations) ||
            this.show.Contains(ShowOptions.unreached);

        case ProofOutcome.False:
        case ProofOutcome.Top:
          return this.show.Contains(ShowOptions.errors);

        default:
          return true;
      }
    }

    #region Inference and suggestions

    public bool AllowInferenceOfDisjunctions
    {
      get
      {
        return this.infdisjunctions;
      }
    }

    public bool InferObjectInvariantsOnlyForReadonlyFields
    {
      get
      {
        return this.infreadonlyonly;
      }
    }

    public bool InferObjectInvariantsForward
    {
      get
      {
        return this.infer.Contains(InferOptions.objectinvariantsforward);
      }
    }

    public bool SuggestAssumes
    {
      get
      {
        return this.suggest.Contains(SuggestOptions.assumes);
      }
    }

    public bool SuggestAssumesForCallees
    {
      get
      {
        return this.suggest.Contains(SuggestOptions.calleeassumes);
      }
    }

    public bool SuggestNecessaryPostconditions
    {
      get
      {
        return this.suggest.Contains(SuggestOptions.necessaryensures);
      }
    }

    public bool SuggestCodeFixes
    {
      get
      {
        return this.suggest.Contains(SuggestOptions.codefixes) || this.suggest.Contains(SuggestOptions.codefixesshort);
      }
    }

    public bool SuggestRequires
    {
      get
      {
        return this.suggest.Contains(SuggestOptions.requires);
      }
    }

    public bool SuggestRequiresBase
    {
      get
      {
        return this.suggest.Contains(SuggestOptions.requiresbase);
      }
    }

    public bool SuggestAssertToContracts
    {
      get 
      { 
        return this.suggest.Contains(SuggestOptions.asserttocontracts); 
      }
    }

    public bool SuggestRequiresForArrays
    {
      get
      {
        return this.suggest.Contains(SuggestOptions.arrayrequires);
      }
    }

    public bool SuggestRequiresPurityForArrays
    {
      get
      {
        return this.suggest.Contains(SuggestOptions.arraypurity);
      }
    }

    public bool SuggestObjectInvariants
    {
      get
      {
        return this.suggest.Contains(SuggestOptions.objectinvariants);
      }
    }

    public bool SuggestObjectInvariantsForward
    {
      get
      {
        return this.suggest.Contains(SuggestOptions.objectinvariantsforward);
      }
    }

    public bool SuggestCallInvariants
    {
      get
      {
        return this.suggest.Contains(SuggestOptions.callinvariants);
      }
    }

    public bool SuggestEnsures(bool isProperty)
    {
      return this.suggest.Contains(SuggestOptions.methodensures) ||
        (isProperty ? this.suggest.Contains(SuggestOptions.propertyensures) : false);
    }

    public bool SuggestNonNullReturn
    {
      get
      {
        return this.suggest.Contains(SuggestOptions.nonnullreturn);
      }
    }

    public bool SuggestReadonlyFields
    {
      get
      {
        return this.suggest.Contains(SuggestOptions.readonlyfields);
      }
    }

    public bool CheckInferredRequires
    {
      get
      {
        return this.check.Contains(CheckOptions.inferredrequires); 
      }
    }

    public bool TreatMissingPublicRequiresAsErrors
    {
      get
      {
        return this.missingPublicRequiresAreErrors;
      }
    }

    public PreconditionInferenceMode PreconditionInferenceAlgorithm
    {
      get
      {
        return this.premode;
      }
    }

    public bool MayPropagateInferredRequiresOrEnsures
    {
      get
      {
        return
          this.infer.Contains(InferOptions.requires) ||
          this.infer.Contains(InferOptions.propertyensures) ||
          this.infer.Contains(InferOptions.methodensures) ||
          this.infer.Contains(InferOptions.nonnullreturn) ||
          this.infer.Contains(InferOptions.symbolicreturn) ||
          this.infer.Contains(InferOptions.arraypurity) ||
          this.infer.Contains(InferOptions.arrayrequires) ||
          this.infer.Contains(InferOptions.objectinvariants);
      }
    }

    public bool PropagateInferredRequires(bool isCurrentMethodAProperty)
    {
      return this.infer.Contains(InferOptions.requires);
    }

    public bool PropagateInferredEnsures(bool isCurrentMethodAProperty)
    {
      return this.infer.Contains(InferOptions.methodensures) ||
        (isCurrentMethodAProperty ? this.infer.Contains(InferOptions.propertyensures) : false);
    }

    public bool PropagateInferredInvariants
    {
      get
      {
        return this.infer.Contains(InferOptions.objectinvariants);
      }
    }

    public bool PropagateInferredNonNullReturn
    {
      get
      {
        return this.infer.Contains(InferOptions.nonnullreturn);
      }
    }

    public bool PropagateInferredSymbolicReturn
    {
      get
      {
        return this.infer.Contains(InferOptions.symbolicreturn);
      }
    }

    public bool PropagateInferredEnsuresForProperties
    {
      get
      {
        return this.infer.Contains(InferOptions.autopropertiesensures);
      }
    }

    public bool PropagateInferredArrayRequires
    {
      get
      {
        return this.infer.Contains(InferOptions.arrayrequires);
      }
    }

    public bool PropagateRequiresPurityForArrays
    {
      get
      {
        return this.infer.Contains(InferOptions.arraypurity);
      }
    }

    public bool PropagateObjectInvariants
    {
      get { return this.infer.Contains(InferOptions.objectinvariants); }
    }

    public bool PropagatedRequiresAreSufficient
    {
      get
      {
        // The AllPaths is the only precondition inference analysis to be guaranteed to be sufficient
        return this.premode == PreconditionInferenceMode.allPaths;
      }
    }


    public bool InferPreconditionsFromPostconditions { get { return this.prefrompost; } }
    #endregion

    public bool NoLogo { get { return this.nologo; } }

    public bool NoBox { get { return this.nobox || this.regression; } }

    #region Checking 

    public bool CheckAssertions { get { return this.check.Contains(CheckOptions.assertions); } }

    public bool CheckExistentials { get { return this.check.Contains(CheckOptions.exists); } }

    public bool CheckAssumptions
    {
      get { return this.check.Contains(CheckOptions.assumptions) || this.check.Contains(CheckOptions.falseassumptions);  }
    }

    public bool CheckConditions
    {
      get { return this.check.Contains(CheckOptions.conditionsvalidity); }
    }

    public bool CheckAssumptionsAndContradictions
    {
      get
      {
        return this.check.Contains(CheckOptions.falseassumptions);
      }
    }

    public bool CheckFalsePostconditions
    {
      get
      {
        return this.check.Contains(CheckOptions.falsepostconditions);
      }
    }

    public bool CheckEntryContradictions
    {
      get
      {
        return !this.UseSemanticBaseline && this.check.Contains(CheckOptions.entrycontradictions);
      }
    }

    public bool InferAssumesForBaseLining
    {
        get
        {
            return this.UseSemanticBaseline || this.SaveSemanticBaseline;
        }
    }

  #endregion

    public ExpressionCacheMode ExpCaching
    {
      get
      {
        return this.expcache;
      }
    }

    #region Print
    
    public bool PrintPerMethodStatistics { get { return this.stats.Contains(StatOptions.perMethod); } }
    public bool PrintValidationStats { get { return this.stats.Contains(StatOptions.valid); } }
    public bool PrintTimeStats { get { return this.stats.Contains(StatOptions.time); } }
    public bool PrintProgramStats { get { return this.stats.Contains(StatOptions.program); } }
    public bool PrintSlowMethods { get { return this.stats.Contains(StatOptions.slowmethods); } }
    public bool PrintPerMethodAnalysisTime { get { return this.stats.Contains(StatOptions.timeperMethod); } }
    public bool PrintMemStats { get { return this.stats.Contains(StatOptions.mem); } }
    public bool PrintArithmeticStats { get { return this.stats.Contains(StatOptions.arithmetic); } }
    public bool PrintAssertStats { get { return this.stats.Contains(StatOptions.asserts); } }
    public bool PrintMethodAssertStats { get { return this.stats.Contains(StatOptions.methodasserts); } }
    public bool PrintAbstractDomainsStats { get { return this.stats.Contains(StatOptions.abstractdomains); } }
    public bool PrintEGraphStats { get { return this.stats.Contains(StatOptions.egraph); } }
    public bool PrintPhaseStats { get { return this.stats.Contains(StatOptions.phases); } }
    public bool PrintInferenceStats { get { return this.stats.Contains(StatOptions.inference); } }
    public bool PrintControllerStats { get { return this.stats.Contains(StatOptions.controller); } }

    #endregion

    public bool OutputOnlyExternallyVisibleMembers { get { return !this.outputPrettyFull; } }
    public bool ShowOnlyExternallyVisibleMethods { get { return this.show.Contains(ShowOptions.externallyvisiblemembersonly); } }

    public bool UseWeakestPreconditions { get { return this.wp; } }
    public bool ShowUnprovenObligations { get { return this.show.Contains(ShowOptions.obligations); } }
    public bool ShowPaths { get { return this.show.Contains(ShowOptions.paths); } }
    public bool ShowPhases { get { return this.show.Contains(ShowOptions.analysisphases); } }
    public bool ShowCacheMisses { get { return this.show.Contains(ShowOptions.cachemisses); } }    
    public int MaxPathSize { get { return this.maxPathSize; } }
    public int MaxWarnings { get { return this.maxWarnings; } }

    #region Cache

    public bool NeedCache
    {
      get
      {
        return this.UseCache || this.SaveToCache || this.ClearCache || this.UseSemanticBaseline || this.SaveSemanticBaseline;
      }
    }
    public bool ClearCache { get { return this.clearCache; } }
    public bool UseCache { get { return this.useCache; } }
    public bool SaveToCache { get { return this.saveToCache; } }
    bool Caching.IClousotCacheOptions.SaveToCache { get { return this.SaveToCache || this.SaveSemanticBaseline; } }
    public int CacheMaxSize { get { return this.cacheMaxSize; } }
    public string SourceControlInfo { get { return this.sourceControlInfo; } }
    public string CacheServer { get { return this.cacheServer; } }
    public int CacheServerTimeout { get { return this.cacheServerTimeout; } }
    public bool SetCacheBaseLine { get { return this.setCacheBaseLine; } }

    public string TypeNameSelect { get { return this.typeNameSelect; } }
    public string NamespaceSelect { get { return this.namespaceSelect; } }
    public string MemberNameSelect { get { return this.memberNameSelect; } }

    #endregion

    public bool IsFocused(int methodNumber)
    {
      return (this.focus != null && this.focus.Contains(methodNumber));
    }

    private List<ShowOptions> savedShow;
    private List<TraceOptions> savedTrace;
    private Stack<WarningLevelOptions> savedWarningLevel;

    public void Save()
    {
      savedShow = this.show;
      savedTrace = this.trace;
      
      this.show = new List<ShowOptions>(this.show);
      this.trace = new List<TraceOptions>(this.trace);
    }
    public void Restore()
    {
      this.show = savedShow;
      this.trace = savedTrace;
    }
    public void Add(ShowOptions option)
    {
      this.show.Add(option);
    }

    public void Add(TraceOptions option)
    {
      this.trace.Add(option);
    }

    public void Push(WarningLevelOptions level)
    {
      EnsureWarningLevel();

#if DEBUG
      Console.WriteLine("Changing the warning level to {0}", level);
#endif

      this.savedWarningLevel.Push(this.warninglevel);
      this.warninglevel = level;
    }

    public void Pop()
    {
      Contract.Assume(this.savedWarningLevel != null);


      this.warninglevel = this.savedWarningLevel.Pop();

#if DEBUG
      Console.WriteLine("Warning level restored to {0}", this.warninglevel);
#endif

    }

    private void EnsureWarningLevel()
    {
      Contract.Ensures(this.savedWarningLevel != null);

      if(this.savedWarningLevel == null)
      {
        this.savedWarningLevel = new Stack<WarningLevelOptions>();
      }
    }

    public bool IsLegacyAssemblyMode { get { return this.assemblyMode == AssemblyMode.legacy; } }
    #endregion

    #region CacheFileName

    private readonly string clousotVersion = typeof(GeneralOptions).Assembly.GetName().Version.ToString();
    public string ClousotVersion { get { return this.clousotVersion; } }

    public string CacheDirectory
    {
      get
      {
        if (!string.IsNullOrWhiteSpace(this.cacheDirectory))
        {
          return this.cacheDirectory;
        }
        try
        {
          var folder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData, Environment.SpecialFolderOption.Create);
          folder = Path.Combine(folder, "CodeContracts");
          if (!Directory.Exists(folder))
          {
            Directory.CreateDirectory(folder);
          }
          return folder;
        }
        catch
        {
          return null;
        }
      }
    }

    [ContractVerification(true)]
    public string GetCacheDBName()
    {
      Contract.Ensures(Contract.Result<string>() != null);

      var prefix = this.cacheDBVersionPrefix ? "cccheck" + clousotVersion + "Cache." : "";
      // A different name was set by a command line option
      if (this.cacheName != null)
      {
          return prefix + this.cacheName;
      }

      var result = prefix;

      Contract.Assume(this.Assemblies != null, "Missing postcondition");
      foreach (var assembly in this.Assemblies)
      {
        try
        {
          var name = Path.GetFileNameWithoutExtension(assembly);
          result += name;
        }
        catch
        {
        }
      }

      return result;
    }

    bool Caching.IClousotCacheOptions.Trace { get { return this.TraceCache; } }
    #endregion

    public bool UseSemanticBaseline
    {
      get
      {
        return this.useSemanticBaseline != null;
      }
    }

    public bool SaveSemanticBaseline
    {
      get
      {
        return this.saveSemanticBaseline != null;
      }
    }

    public string SemanticBaselineReadName
    {
      get
      {
        return this.useSemanticBaseline;
      }
    }

    public string SemanticBaselineSaveName
    {
      get
      {
        return this.saveSemanticBaseline;
      }
    }

    public bool SkipIdenticalMethods { get { return this.skipIdenticalMethods; } }


    public bool SufficientConditions
    {
      get
      {
        return this.infer.Contains(InferOptions.assumefalse);
      }
    }

    public bool AnalyzeClosures
    {
      get
      {
        return this.analyze.Contains(AnalyzeOptions.closures);
      }
    }

    public bool AnalyzeMoveNext
    {
      get
      {
        return this.analyze.Contains(AnalyzeOptions.movenext);
      }
    }

    public bool AnalyzeCompilerGeneratedCode
    {
      get
      {
        return this.analyze.Contains(AnalyzeOptions.compilergenerated);
      }
    }

    public bool WarningsAsErrors { get { return failOnWarnings; } }

    public static List<string> GetClousotOptionsWithPaths()
    {
      var result = new List<string>();
      foreach (var field in typeof(GeneralOptions).GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
      {
        if (field.IsDefined(typeof(OptionWithPathsAttribute), false))
        {
          result.Add(field.Name);
        }
      }

      return result;
    }
  }
}
