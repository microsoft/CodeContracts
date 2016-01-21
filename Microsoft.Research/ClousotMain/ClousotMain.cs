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

#define USE_FACTQUERY_WITHMEMORY 
// #define EXPERIMENTAL

//#define TRACE

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Research.AbstractDomains;
using Microsoft.Research.AbstractDomains.Numerical;
using Microsoft.Research.CodeAnalysis.Expressions;
using Microsoft.Research.DataStructures;
using Microsoft.Research.Slicing;
using Microsoft.Research.CodeAnalysis.Caching;
using System.Threading;
using System.Diagnostics;
using Microsoft.Research.ClousotPulse.Messages;
using Microsoft.Research.CodeAnalysis.Inference;
using System.IO.Pipes;
using System.Globalization;

// Because these classes and methods can be called from different places (Console, Visual Studio, WCF Service, ...)
// - do not use Console unless guarded with #if DEBUG. Instead use output
// - do not use Environment.Exit. Instead throw an ExitRequestedException

namespace Microsoft.Research.CodeAnalysis
{
  public interface IOutputFullResultsFactory<Method, Assembly>
  {
    IOutputFullResults<Method, Assembly> GetOutputFullResultsProvider(ILogOptions options);
  }

  public static class Clousot
  {

    public static int ClousotMain<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(
      string[] args,
      IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder,
      IDecodeContracts<Local, Parameter, Method, Field, Type> contractDecoder,
      System.Collections.IDictionary assemblyCache,
      IOutputFullResultsFactory<Method, Assembly> outputFactory = null,
      IEnumerable<IClousotCacheFactory> cacheAccessorFactories = null
    )
      where Type : IEquatable<Type>
      where Method : IEquatable<Method>
    {

      // We do it here becase we hope we can trigger some work of the jit 
      args = PipesUtils.WaitForArgsIfNeeded(args);

      using (var binder = new TypeBinder<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(
        args, mdDecoder, contractDecoder, assemblyCache, outputFactory, cacheAccessorFactories
        ))
        return binder.Analyze();
    }

    public static IndividualMethodAnalyzer<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>
      GetIndividualMethodAnalyzer<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(
      IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder,
      IDecodeContracts<Local, Parameter, Method, Field, Type> contractDecoder,
      string[] args,
      IOutputFullResultsFactory<Method, Assembly> outputFactory,
      IEnumerable<IClousotCacheFactory> cacheAccessorFactories,
      CancellationToken cancellationToken
      )
      where Type : IEquatable<Type>
      where Method : IEquatable<Method>
    {
      var assemblyCache = new Dictionary<string, Assembly>();
      return new IndividualMethodAnalyzer<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(
        args, mdDecoder, contractDecoder, assemblyCache, outputFactory, cacheAccessorFactories, cancellationToken
        );
    }

    public class IndividualMethodAnalyzer<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> : IDisposable
      where Type : IEquatable<Type>
      where Method : IEquatable<Method>
    {

      private TypeBinder<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> binder;
      public IndividualMethodAnalyzer(
        string[] args,
        IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder,
        IDecodeContracts<Local, Parameter, Method, Field, Type> contractDecoder,
        IDictionary assemblyCache,
        IOutputFullResultsFactory<Method, Assembly> outputFactory,
        IEnumerable<IClousotCacheFactory> cacheAccessorFactories,
        CancellationToken cancellationToken
      )
      {
        this.binder = new TypeBinder<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(
          args, mdDecoder, contractDecoder, assemblyCache, outputFactory, cacheAccessorFactories
          );

        this.binder.InitializeClousotDataStructuresAndAnalyses(cancellationToken); // MB: what if initialization not OK?
      }

      #region IDisposable Members

      public void Dispose()
      {
        this.Dispose(true);
      }

      protected virtual void Dispose(bool disposing)
      {
        if (this.binder != null)
          this.binder.Dispose();
      }

      #endregion

      ~IndividualMethodAnalyzer()
      {
        this.Dispose(false);
      }

      public GeneralOptions Options { get { return this.binder.options; } }
      /// <summary>
      /// Clients call this entry point in order to analyze a single method.
      /// During that analysis, methods that are called in its body may also need
      /// to be analyzed.
      /// </summary>
      public void Analyze(Method method)
      {
        this.binder.AnalyzeMethod(method);
        this.binder.output.Close();
      }
      public void AnalyzeAssembly(Assembly assembly)
      {
        this.binder.AnalyzeAssembly(assembly, new Set<string>());
        this.binder.output.Close();
      }

    }

    class TypeBinder<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> : IDisposable
      where Type : IEquatable<Type>
      where Method : IEquatable<Method>
    {

      #region Object invariant

      [ContractInvariantMethod]
      private void ObjectInvariant()
      {
        Contract.Invariant(this.originalArguments != null);
        Contract.Invariant(this.options != null);
        Contract.Invariant(this.clousotStateStatistics != null);
      }

      #endregion

      #region Private state during entire analyses, mostly for convenience

      readonly string[] originalArguments;
      private readonly OldAnalysisDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ILogOptions, IMethodResult<SymbolicValue>> driver;
      internal readonly GeneralOptions options;
      private readonly Dictionary<string, IMethodAnalysis> methodAnalyzers = Analyzers.CreateMethodAnalyzers();
      private readonly Dictionary<string, IClassAnalysis> classAnalyzers = Analyzers.CreateClassAnalyzers();
      private readonly IDictionary assemblyCache;
      private readonly List<IMethodAnalysis> _cheapAnalyses = new List<IMethodAnalysis>();
      private readonly List<IMethodAnalysis> _verycheapAnalyses = new List<IMethodAnalysis>();

      internal IOutputFullResults<Method, Assembly> output;
      private readonly AnalysisStatisticsDB globalStats = new AnalysisStatisticsDB();

      private long maxMemory = -1;

      private TimeSpan CFGConstructionTime; // Used only for statistics

      private readonly CacheManager<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ExternalExpression<APC, SymbolicValue>, SymbolicValue> cacheManager;
      private readonly HashSet<Method> analyzedMethods = new HashSet<Method>();
      private readonly HashSet<IClassDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ExternalExpression<APC, SymbolicValue>, SymbolicValue, ILogOptions, IMethodResult<SymbolicValue>>> classDriversToRemove
                 = new HashSet<IClassDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ExternalExpression<APC, SymbolicValue>, SymbolicValue, ILogOptions, IMethodResult<SymbolicValue>>>();
      private IMethodNumbers<Method, Type> methodNumbers;
      private readonly MethodNumbering methodNumbering = new MethodNumbering();

      private readonly ClassDB<Method, Type> classDB;

      private Func<string> currMethodString = null;
      private Action<APC> ContinuationForFailureToConnectToCache = null;
      
      private int analyzedCount = 0;
      private int methodsAnalyzedWithCheaperDomain = 0;
      private int CacheMisses = 0;

      private readonly AssertionStatistics assertStats;

      private readonly SharedPostConditionManagerInfo<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> globalInfoAtMethodExitPoints;
      private readonly SimpleOverriddenMethodPreconditionDispatcher<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> overriddenMethodsPreconditionsDispatcher;
      private readonly FieldsDB<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> fieldsDB;
      private readonly CallerInvariantDB<Method> callInvariantsInferenceManager;

      #endregion

      #region Clousot statistics to be reported to clients

      private readonly ClousotPulseServer pulseServer;
      private ClousotAnalysisState clousotStateStatistics;

      private readonly ClousotAnalysisResults clousotAnalysysisResults;

      private void UpdateClousotAnalysisStateStatistics(int currMethodNumber, string currAssembly, string currMethodName, double currPerc, string phase)
      {
        this.clousotStateStatistics = new ClousotAnalysisState()
        {
          CurrAssembly = currAssembly,
          CurrMethodName = currMethodName,
          CurrMethodNumber = currMethodNumber,
          CurrPercentage = currPerc,
          CurrPhase = phase,
          CurrMemory = GetMemory()
        };
      }

      public ClousotAnalysisState GetClousotAnalysisState()
      {
        this.clousotStateStatistics.CurrMemory = this.GetMemory();
        return this.clousotStateStatistics;
      }

      /// <returns>The amount of used memory, in Mb</returns>
      private int GetMemory()
      {
        return (int)(GC.GetTotalMemory(false) / (1024 * 1024));
      }

      #endregion

      public TypeBinder(
        string[] args,
        IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder,
        IDecodeContracts<Local, Parameter, Method, Field, Type> contractDecoder,
        IDictionary assemblyCache,
        IOutputFullResultsFactory<Method, Assembly> externalOutputFactory = null,
        IEnumerable<IClousotCacheFactory> cacheAccessorFactories = null
      )
      {
        this.originalArguments = args;
        this.lastPhaseStart.Start(); // Start the stop watch for phases
        this.assemblyCache = assemblyCache;
        this.options = GeneralOptions.ParseCommandLineArguments(args, this.methodAnalyzers, this.classAnalyzers);

        #region As first thing, let's check if options are ok. If they are not, let's just end it here
        if (options.HelpRequested)
        {
          options.PrintUsage(Console.Out);
          throw new ExitRequestedException(-1);
        }

        if (options.HasErrors)
        {
          options.PrintErrors(Console.Out);
          throw new ExitRequestedException(-1);
        }
        #endregion

        this.fieldsDB = new FieldsDB<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(mdDecoder);
        this.globalInfoAtMethodExitPoints = new SharedPostConditionManagerInfo<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(mdDecoder, this.fieldsDB);

        this.clousotStateStatistics = new ClousotAnalysisState();
        this.clousotAnalysysisResults = new ClousotAnalysisResults();

        // Start the thread to answer remote questions on Clousot state
        this.pulseServer = new ClousotPulseServer(this.GetClousotAnalysisState);

        BoxedExpression.ConstFalse = BoxedExpression.ConstBool(false, mdDecoder);
        BoxedExpression.ConstTrue = BoxedExpression.ConstBool(true, mdDecoder);

        // Setting min connection pool size for local db allows to check connections
        // concurrently. This value should be the same (or at least comparable with) 
        // as a number of records in the cacheAccessorFactories variable.
        const int MinLocalConnectionPoolSize = 2;

        var cacheInitTime = new TimeSpan(0);
        if (options.NeedCache)
        {
          var startCache = DateTime.Now;
          if (cacheAccessorFactories == null)
          {
            // Should we revert to local?
            if (!options.forceCacheServer)
            {
              cacheAccessorFactories = new IClousotCacheFactory[]{
                new SQLClousotCacheFactory(options.CacheServer),
                new LocalDbClousotCacheFactory(MinLocalConnectionPoolSize),
              };
            }
            else
            { 
              cacheAccessorFactories = new IClousotCacheFactory[]{ new SQLClousotCacheFactory(options.CacheServer) };
            }
          }

          this.cacheManager = CacheManager<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ExternalExpression<APC, SymbolicValue>, SymbolicValue>
            .Create(cacheAccessorFactories, this.methodAnalyzers, this.classAnalyzers, this.globalInfoAtMethodExitPoints, this.options, this.cancellationToken);

          cacheInitTime = DateTime.Now - startCache;
        }

        var scoreManager = GetScoreManager();
        this.output = SelectOutput(scoreManager, this.cacheManager, options, mdDecoder, true, externalOutputFactory);

        if(options.NeedCache)
        { 
          // We need to do it here as the output and the cache are mutually dependent

          if (this.cacheManager == null)
          {
            this.output.Statistic("Time spent trying to connect to the cache server: {0}", cacheInitTime);
          }
          else
          {
            this.output.Statistic("Time spent connecting to the cache: {0}", cacheInitTime);
          }

        }

        // Set target platform here!
        // set shared contract library
        if (!mdDecoder.IsPlatformInitialized)
        {
          WriteLinePhase(output, "Loading the assembly + contracts");

          mdDecoder.SharedContractClassAssembly = options.cclib;
          mdDecoder.SetTargetPlatform(
            framework: options.framework,
            assemblyCache: assemblyCache,
            platform: options.platform,
            resolved: options.resolvedPaths,
            libPaths: options.libPaths,
            errorHandler: this.output.EmitError,
            trace: options.TraceLoading);
        }

        if (options.InferObjectInvariantsForward || options.SuggestObjectInvariantsForward)
        {
          Func<Type, int> ConstructorCounter = t => mdDecoder.Methods(t).Where(mdDecoder.IsConstructor).Count();
          Func<Type, int> MethodCounter = t => mdDecoder.Methods(t).Where(m => !mdDecoder.IsStatic(m)).Count();
          this.classDB = new ClassDB<Method, Type>(ConstructorCounter, MethodCounter, mdDecoder.IsConstructor);
        }

        if (this.options.show.Contains(ShowOptions.scores))
        {
          scoreManager.DumpScores(output.WriteLine);
        }

        if (!options.NoLogo)
        {
          var version = typeof(GeneralOptions).Assembly.GetName().Version;
          output.WriteLine("Microsoft (R) .NET Contract Checker Version {0}", version.ToString());
          output.WriteLine("Copyright (C) Microsoft Corporation. All rights reserved.");
          output.WriteLine("");
        }
        if (this.cacheManager != null)
        {
          var incompatibilities = CacheManager.GetOptionsIncompatibilityReason(options);
          if (incompatibilities.Any())
          {
            output.WriteLine("Warning: the cache system is not compatible with some options. They might not have the expected behaviour:");
            foreach (var reason in incompatibilities)
              output.WriteLine(reason);
          }
        }

        if (this.cacheManager == null && this.options.NeedCache)
        {
          output.WriteLine("Diagnostic: Failed to connect to any cache.");

          this.ContinuationForFailureToConnectToCache = (APC pc) =>
          {
            var witness = new Witness(null, WarningType.ClousotCacheNotAvailable, ProofOutcome.False, pc);
            this.output.EmitOutcome(witness, "Cannot connect to the cache. The CodeContracts static check will not run", "");
            this.output.Close();

            throw new ExitRequestedException(this.options.failOnWarnings? 1 : 0 );
          };

        }
        if (this.cacheManager != null)
        {
          output.WriteLine("Cache used: {0}", this.cacheManager.CacheName);
        }
        var basicDriver = new BasicAnalysisDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ILogOptions>(mdDecoder, contractDecoder, output, options);

        this.driver = new OldAnalysisDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ILogOptions, IMethodResult<SymbolicValue>>(basicDriver);
        if (cacheManager != null)
        {
          cacheManager.SetAnalysisDriver(driver);
        }

        EGraphStats.IncrementalJoin = options.incrementalEgraphJoin;

        this.overriddenMethodsPreconditionsDispatcher = new SimpleOverriddenMethodPreconditionDispatcher<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(mdDecoder);

        if (this.options.SuggestCallInvariants)
        {
          this.callInvariantsInferenceManager = new CallerInvariantDB<Method>();
        }

      }

      private WarningScoresManager GetScoreManager()
      {
        return new WarningScoresManager(this.options.lowScoreForExternal, this.options.customScores);
      }

      [ContractVerification(true)]
      private PreconditionInferenceManager CreatePreconditionManager<Variable>
        (
        List<IProofObligations<Variable, BoxedExpression>> obligations,
        IFactQuery<BoxedExpression, Variable> facts,
        IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ExternalExpression<APC, Variable>, Variable, ILogOptions> mdriver
        )
       where Variable : IEquatable<Variable>
      {
        #region Contracts
        Contract.Requires(this.output != null, "When calling this method, the output should be already set");

        Contract.Requires(mdriver != null);
        Contract.Requires(obligations != null);
        Contract.Requires(facts != null);

        Contract.Ensures(Contract.Result<PreconditionInferenceManager>() != null);

        #endregion

        IPreconditionInference preInference = null;
        IPreconditionDispatcher preDispatcher = null;

        var moveNextStartState = mdriver.MetaDataDecoder.MoveNextStartState(mdriver.CurrentMethod);
        if (moveNextStartState.HasValue)
        {
          // no pre inference
          return PreconditionInferenceManager.Dummy;
        }

        switch (this.options.PreconditionInferenceAlgorithm)
        {
          #region All the cases
          case PreconditionInferenceMode.aggressive:
            {
              preInference = new AggressivePreconditionInference<
              Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ExternalExpression<APC, Variable>, Variable, ILogOptions>
              (mdriver);

              break;
            }

          case PreconditionInferenceMode.allPaths:
            {
              preInference = new PreconditionInferenceAllOverThePaths<
              Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ExternalExpression<APC, Variable>, Variable, ILogOptions>
              (obligations, mdriver);

              break;
            }

          case PreconditionInferenceMode.backwards:
            {
              preInference = new PreconditionInferenceBackwardSymbolic<
              Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ExternalExpression<APC, Variable>, Variable, ILogOptions>
              (facts, mdriver);

              break;
            }

          case PreconditionInferenceMode.combined:
            {
              var inferencers = new List<IPreconditionInference>()
              { 
                new PreconditionInferenceAllOverThePaths<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ExternalExpression<APC, Variable>, Variable, ILogOptions>(obligations, mdriver),
                new PreconditionInferenceBackwardSymbolic<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ExternalExpression<APC, Variable>, Variable, ILogOptions>(facts, mdriver)
              };

              preInference = new PreconditionInferenceCombined(inferencers);

              break;
            }

          default:
            {
              Contract.Assert(false); // Should be unreachable
              break;
            }
          #endregion
        }

        Contract.Assert(preInference != null);

        preInference = new PreconditionInferenceProfiler(preInference);

        preDispatcher = new SimplePreconditionDispatcher<
          Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ExternalExpression<APC, Variable>, Variable, ILogOptions>
          (mdriver, output, this.options.AllowInferenceOfDisjunctions, FilterPreconditionsAccordingToWarningLevel);

        preDispatcher = new PreconditionDispatcherProfiler(preDispatcher);

        return new PreconditionInferenceManager(preInference, preDispatcher);
      }

      [ContractVerification(true)]
      private IPostconditionDispatcher CreatePostconditionDispatcher<Variable>(
         IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ExternalExpression<APC, Variable>, Variable, ILogOptions> mdriver)
        where Variable : IEquatable<Variable>
      {
        Contract.Requires(mdriver != null);
        Contract.Requires(this.output != null);

        var ensures = ClousotSuggestion.Ensures.All;

        if(this.options.SuggestNonNullReturn && !this.options.SuggestEnsures(mdriver.MetaDataDecoder.IsPropertyGetterOrSetter(mdriver.CurrentMethod)))
        {
          ensures = ClousotSuggestion.Ensures.NonNull;
        }

        var dispatcher = new SimplePostconditionDispatcher<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ExternalExpression<APC, Variable>, Variable, ILogOptions>(this.globalInfoAtMethodExitPoints, mdriver, this.output, ensures, options.suggestionsForExternalVisibleOnly, options.inferencemode == InferenceMode.Aggressive);
        return new PostconditionDispatcherProfiler(dispatcher);
      }

      [ContractVerification(true)]
      private IObjectInvariantDispatcher CreateObjectInvariantDispatcher<Variable>(
       IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ExternalExpression<APC, Variable>, Variable, ILogOptions> mdriver)
         where Variable : IEquatable<Variable>
      {
        Contract.Requires(mdriver != null);
        Contract.Requires(this.output != null);

        var dispatcher = new SimpleObjectInvariantDispatcher<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ExternalExpression<APC, Variable>, Variable, ILogOptions>(mdriver, output, this.options.AllowInferenceOfDisjunctions);
        return new ObjectInvariantDispatcherProfiler(dispatcher);
      }

      [ContractVerification(true)]
      private IAssumeDispatcher CreateAssumeDispatcher<Variable>(
       IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ExternalExpression<APC, Variable>, Variable, ILogOptions> mdriver)
         where Variable : IEquatable<Variable>
      {
        Contract.Requires(mdriver != null);
        Contract.Requires(this.output != null);

        var dispatcher = new SimpleAssumeDispatcher<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ExternalExpression<APC, Variable>, Variable, ILogOptions>(mdriver, output, this.options.AllowInferenceOfDisjunctions, this.options.inferencemode == InferenceMode.Aggressive);
        return new AssumeDispatcherProfiler(dispatcher);
      }

      private ICodeFixesManager CreateCodeFixManager<Variable>
        (IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ExternalExpression<APC, Variable>, Variable, ILogOptions> mdriver)
        where Variable : IEquatable<Variable>
      {
        if (this.options.SuggestCodeFixes)
        {
          var constraintSolver = new SimpleSatisfyProcedure<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(mdriver.MetaDataDecoder);

          Func<BoxedExpression, BoxedExpression> ExpressionSimplifier =
            (BoxedExpression be) =>
            {
              return be.EvaluateConstants(mdriver.MetaDataDecoder).Simplify(mdriver.MetaDataDecoder, replaceIntConstantsByBooleans: false);
            };

          Func<BoxedExpression, BoxedExpression, BoxedExpression> ConstraintSolver =
            (BoxedExpression constraint, BoxedExpression variable) =>
            {
              BoxedExpression result;
              if (constraintSolver.TryMostGeneralSatisfaction(constraint, variable, out result))
              {
                return result;
              }
              return null; // give up...
            };

          return new CodeFixesProfiler(
            new CodeFixesManager(
              this.options.suggest.Contains(SuggestOptions.codefixesshort),
              output,
              this.options.TraceCache,
              ExpressionSimplifier, ConstraintSolver,
              new LazyEval<BoxedExpression>(() => BoxedExpression.Const(0, mdriver.MetaDataDecoder.System_Int32, mdriver.MetaDataDecoder)),
              new LazyEval<BoxedExpression>(() => BoxedExpression.Const(1, mdriver.MetaDataDecoder.System_Int32, mdriver.MetaDataDecoder))),
            this.options.trace.Contains(TraceOptions.inference));
        }
        else
        {
          return new DummyCodeFixesManager();
        }
      }

      // A fresh inference manager for each method
      private ContractInferenceManager CreateContractInferenceManager<Variable>(
        List<IProofObligations<Variable, BoxedExpression>> obligations,
        IFactQuery<BoxedExpression, Variable> facts,
        IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ExternalExpression<APC, Variable>, Variable, ILogOptions> mdriver,
        IOutput output
          )
       where Variable : IEquatable<Variable>
      {

        // setting explictly as there is no better way I can think of at this moment
        this.overriddenMethodsPreconditionsDispatcher.CurrentMethod = mdriver.CurrentMethod;
        this.overriddenMethodsPreconditionsDispatcher.BoxedExpressionTransformer = new AddExplicitCastToTheExpression<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ExternalExpression<APC, Variable>, Variable, ILogOptions>
          (mdriver);

        var precondition = CreatePreconditionManager(obligations, facts, mdriver);
        var objectInvariant = CreateObjectInvariantDispatcher(mdriver);
        var postcondition = CreatePostconditionDispatcher(mdriver);
        var assume = CreateAssumeDispatcher(mdriver);
        var codefix = CreateCodeFixManager(mdriver);

        return new ContractInferenceManager(mdriver.CanAddRequires(), this.overriddenMethodsPreconditionsDispatcher, precondition, objectInvariant, postcondition, assume, codefix, output);
      }

      [Pure]
      private bool FilterPreconditionsAccordingToWarningLevel(BoxedExpression exp)
      {
        if (exp == null)
        {
          return false;
        }

        switch (this.options.WarningLevel)
        {
          case WarningLevelOptions.low:
            {
              // F: HACK HACK HACK HACK -- we rely on the textual representation of the precondition, but we should visit the expression, and see if some access path is too complex
              var expText = exp.ToString();
              if (expText != null  // Should never be null as it is a precondition. It is just a sanity check to make the code more robust
                && !expText.Contains("IsNull") // We want to see the IsNullOrEmpty or IsNullOrWhiteSpace
                && expText.Contains(".") && !expText.Contains(".Length"))
              {
                return true;
              }
              else
              {
                return false;
              }
            }
          case WarningLevelOptions.medium:
          case WarningLevelOptions.mediumlow:
          case WarningLevelOptions.full:
          default:
            {
              return false;
            }
        }
      }


      static IOutputFullResults<Method, Assembly> SelectOutput(
        WarningScoresManager scoresManager,
        CacheManager<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ExternalExpression<APC, SymbolicValue>, SymbolicValue> cacheManager,
        GeneralOptions options,
        IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder,
        bool enableRegression = true,
        IOutputFullResultsFactory<Method, Assembly> externalOutputFactory = null
        )
      {
        Contract.Requires(scoresManager != null);

#if DEBUG
        // test if we avoid debugging boxes
        if (options.NoBox)
        {
          System.Diagnostics.Debug.Listeners.Clear();
          System.Diagnostics.Debug.Listeners.Add(new ExitTraceListener());
        }
#else
        System.Diagnostics.Debug.Listeners.Clear();
#endif

        // select normal output
        var tw = options.OutFile;
        IOutputFullResults<Method, Assembly> result;
        if (externalOutputFactory != null)
        {
          result = externalOutputFactory.GetOutputFullResultsProvider(options);
        }
        else if (options.IsXMLOutput)
        {
          result = new XMLWriterOutput<Method, Assembly>(options.OutFileName, tw, options, mdDecoder.DocumentationId, mdDecoder.Name, scoresManager);
        }
        else if (options.IsRemoteOutput)
        {
          result = new RemoteWriterOutput<Method, Assembly>(options.OutFileName, tw, options);
        }
        else
        {
          result = new FullTextWriterOutput<Method, Assembly>(options.OutFileName, tw, options);
        }

        Contract.Assert(result != null);

        if (enableRegression && options.IsRegression)
        {
          result = new RegressionOutput<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(result, options, mdDecoder);
        }

        if (options.warnscores)
        {
          result = new QuantitativeOutput<Method, Assembly>(
           !options.IsRegression && options.PrioritizeWarnings,  // Should we sort the warnings according to their rank? (but only if not in regression!)         
           options.show.Contains(ShowOptions.warnranks),         // Should we print the score for each outcome?
           options.trace.Contains(TraceOptions.warningcontexts), // Should we print the justifiation for each outcome?
           () => options.WarningLevel,                           // Filter outcomes according to their score
           scoresManager,                                        // The object in charge of of the mapping Warnings --> Scores
           options.MaxWarnings,                                  // Max warnings to be shown
           result);
        }

        if (options.baseLine != null)
        {
          if (options.clearBaseLine && File.Exists(options.baseLine)) File.Delete(options.baseLine);
          if (File.Exists(options.baseLine))
          {
            // comparing
            var extraBaseLine = new XmlBaseLineWriter<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(result, options, mdDecoder, cacheManager, options.baseLine + ".new");
            result = new XmlBaseLineComparer<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(extraBaseLine, options, mdDecoder, cacheManager, options.baseLine);
          }
          else
          {
            // first baseline
            result = new XmlBaseLineWriter<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(result, options, mdDecoder, cacheManager, options.baseLine);
          }
        }

        if (options.MaskedWarnings || options.OutputWarningMasks)
        {
          result = new MaskingOutput<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(result, options, mdDecoder);
        }

        if (options.MaskedVerifiedRepairs)
        {
          result = new MaskVerifiedRepairsSuggestions<Method, Assembly>(result);
        }

        result = new SwallowedCounterOutput<Method, Assembly>(result);

        if (options.WarningsWithSuggestions)
        {
          result = new WarningSuggestionLinkOutput<Method, Assembly>(result);
        }

        if (cacheManager != null)
        {
          cacheManager.ReplayOutput = result;
        }

        // All the output layers below will not benefit from the cache. 
        // This may be a wanted side-effect

        if (options.SaveToCache && cacheManager != null)
        {
          result = new CachingOutput<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ExternalExpression<APC, SymbolicValue>, SymbolicValue>(result, cacheManager);
        }

        if (options.extractmethodmodeRefine != null)
        {
          result = new MaskingSuggestions<Method, Assembly>(options.extractmethodmodeRefine, result);
        }

        if (options.ShowOnlyExternallyVisibleMethods)
        {
          result = new ExternallyVisibleMethodsOnlyOutput<Method, Assembly>(result, mdDecoder.IsVisibleOutsideAssembly);
        }

        if(options.warnIfSuggest.Count > 0)
        {
          result = new TurnSuggestionIntoWarnings<Method, Assembly>(result, options.warnIfSuggest);
        }

        // Last thing: we set Console.Out to our output in our release builds
#if !DEBUG
        Console.SetOut(result.ToTextWriter());
#else
      // Otherwise keep using the console
#endif
        return result;
      }

      public int Analyze()
      {
        // String formatting in the analyzer does not specify a culture, therefore some tests fail on systems with non-English culture with e.g. "expected: 2.0, actual: 2,0"
        // Setting the threads culture should be removed as soon as issue #149 is fixed.
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

        try
        {
          return InternalAnalyze();
        }
        catch(ExitRequestedException)
        {
          throw;
        }
        catch (Exception e)
        {
          this.clousotAnalysysisResults.ClousotCrashed = true;

          if (options.NoBox)
          {
            SendLeaderBoardFailure();
            if (this.currMethodString != null)
            {
              output.WriteLine("Internal error at method {0}", this.currMethodString());
            }
            output.WriteLine("Failed with uncaught exception: {0}", e.Message);
            output.WriteLine("Stack trace: {0}", e.StackTrace);

            return -55555;
          }
          else
          {
            throw;
          }
        }
        finally
        {
          this.clousotAnalysysisResults.NotifyDone();
          this.pulseServer.NotifyListenersWeAreDone(this.clousotAnalysysisResults);
          this.pulseServer.Kill();
        }
      }

      private static void WriteReproFile()
      {
        try
        {
          var file = new StreamWriter("repro.bat");
          file.Write(Environment.CommandLine.Replace("-repro", ""));
          file.WriteLine(" %1 %2 %3 %4 %5 %6 %7 %8 %9");
          file.Close();
        }
        catch { }
      }

      public int InitializeClousotDataStructuresAndAnalyses(CancellationToken cancellationToken)
      {
        this.cancellationToken = cancellationToken;
        //TrySendLeaderboardPing();
        if (options.repro)
        {
          WriteReproFile();
        }
        if (options.Analyses.Count == 0)
        {
          output.WriteLine("Warning: No analyses specified.");
        }

        bool isOk;

        InitAndCheckArrayAnalysisDependences(out isOk);

        if (!isOk)
        {
          return -1;
        }

        InitializeSparseArrayRepresentation();

        InitializeThresholdsForWidening();

        InitializeCache(output);

        WarningScoresManager.Tracing = options.trace.Contains(TraceOptions.warningcontexts);

        return 0;
      }

      /// <summary>
      /// Clients call this entry point in order to analyze a list of assemblies.
      /// </summary>
      /// <returns>
      /// The return value is an error indicator:
      /// -1: an error occurred during the analysis.
      ///  0: no errors
      ///  n: for regressions, the number of errors found in the analysis.
      /// </returns>
      int InternalAnalyze()
      {
        var token = new CancellationToken();
        var init = InitializeClousotDataStructuresAndAnalyses(token);

        if (init != 0)
        {
          return init;
        }

        var OverallAnalysisTime = new TimeCounter();
        OverallAnalysisTime.Start();

        PopulateContractAssemblies(output);

        // Compute the set of assembly names of assemblies under analysis so we know when calls are within the
        // scope of analysis
        var beingAnalyzed = new Set<string>(StringComparer.InvariantCultureIgnoreCase);
        beingAnalyzed.AddRange(this.options.Assemblies.Select(Path.GetFileNameWithoutExtension));

        var lastAssemblyName = ""; // for stats output
        foreach (string assembly in options.Assemblies)
        {
          Assembly assem;
          if (!this.driver.MetaDataDecoder.TryLoadAssembly(assembly, assemblyCache, this.output.EmitError, out assem, options.IsLegacyAssemblyMode, options.reference, options.extractSourceText))
          {
            output.WriteLine("Cannot load assembly '{0}'", assembly);
            this.options.AddError();
            continue;
          }
          AnalyzeAssembly(assem, beingAnalyzed);

          lastAssemblyName = assembly;
        }

#if false
        if (this.options.SuggestRequires)
        {
          // TODO TODO TODO: Add those suggestions to the cache
          this.overriddenMethodsPreconditionsDispatcher.SuggestPotentialPreconditions(this.output);
        }
#endif

        if (this.options.SuggestCallInvariants)
        {
          Contract.Assert(this.callInvariantsInferenceManager != null);

          this.callInvariantsInferenceManager.SuggestCallInvariants(this.output);
        }

        // Make sure everything is written to the cache file
        if (this.SaveToCache)
        {
          this.cacheManager.TrySaveChanges(now: true);
        }

        OverallAnalysisTime.Stop();

        PrintAnalysisStatistics(OverallAnalysisTime, lastAssemblyName);

        this.outputWarnings = output.RegressionErrors();
        output.Close();

        if (this.options.HasErrors)
        {
            return 1;
        }
        if (outputWarnings > 0 && (options.failOnWarnings || options.regression))
        {
          return outputWarnings;
        }
        if (options.regression && this.analyzedCount == 0)
        {
          // not a good regression
          return -1;
        }
        if(!options.IsRegression && output.ErrorsWereEmitted)
        {
          return -1;
        }
        return ComputeReturnCode();
      }

      private void PrintAnalysisStatistics(TimeCounter OverallAnalysisTime, string lastAssemblyName)
      {
        // TODO TODO : Move the logic in this method in the ClousotAnalysisResults, so that we save all the statistics in such an object, and we separate them from the console printing/clousot pulse notification, etc.

        foreach (var analysis in this.options.Analyses)
        {
          analysis.PrintAnalysisSpecificStatistics(output);
        }

        if (options.PrintValidationStats)
        {
          if (lastAssemblyName != "")
          {
            lastAssemblyName = Path.GetFileName(lastAssemblyName).Replace(".decl", "");

            this.clousotAnalysysisResults.AnalyzedAssembly = lastAssemblyName;
          }

          var results = globalStats.OverallStatistics().EmitStats(
            output.SwallowedMessagesCount(ProofOutcome.Top),
            output.SwallowedMessagesCount(ProofOutcome.Bottom),
            output.SwallowedMessagesCount(ProofOutcome.False),
            lastAssemblyName, output);

          this.clousotAnalysysisResults.UpdateAssertionStatisticsFrom(results, this.outputWarnings);

        }

        if (options.PrintMemStats)
        {
          output.WriteLine(GenericCache.Statistics);
          output.WriteLine("max allocated memory: {0}", Convert.ToString(this.maxMemory / (1024 * 1024)));

          output.WriteLine(SparseRationalArray.Statistics);
        }
        
        if (options.PrintProgramStats)
        {
          output.WriteLine("Program statistics:");
          output.Statistic("Total instructions {0}, Max instructions {1}", SyntacticComplexity.TotalInstructions.ToString(), SyntacticComplexity.MaxInstructions.ToString());
          output.Statistic("Total joins {0}, Max joins {0}", SyntacticComplexity.TotalJoins.ToString(), SyntacticComplexity.MaxJoins.ToString());
          output.Statistic("Total loops {0}, Max loops {0}", SyntacticComplexity.TotalLoops.ToString(), SyntacticComplexity.MaxLoops.ToString());
        }

        PrintPhaseStats(output);

        if (options.PrintSlowMethods)
        {
          output.WriteLine("Methods taking the longest:");
          var i = 1;
          foreach (var time in this.globalStats.MethodsByAnalysisTime(10))
          {
            output.WriteLine("\t{0}:\t Method #{1}, {2}", i++, time.MethodNumber, time.OverallTime);
          }
        }

        if (options.PrintPerMethodAnalysisTime)
        {
          var fileName = string.Format("PerMethodAnalysisTime.{0}.txt", lastAssemblyName);

          output.WriteLine("Per method analysis time statistics written in the file {0}", fileName);

          using (var file = new StreamWriter(fileName))
          {
            file.WriteLine("Details of the Analysis time per method (seconds):");
            file.WriteLine("#,\ttime,\tCFG time,\tmethod name");
            foreach (var stat in this.globalStats.MethodsByAnalysisTime(Int32.MaxValue))
            {
              file.WriteLine("#{3},\t{1},\t{2},\t{0}", stat.MethodName, stat.OverallTime.TotalSeconds, stat.CFGConstructionTime.TotalSeconds, stat.MethodNumber);
            }
          }
        }
        if (options.PrintAssertStats)
        {
          output.WriteLine("average assertions");
          this.assertStats.ShowAverage(output, analyzedCount);
        }
        if (options.PrintTimeStats)
        {
          long msPerMethod;
          if (this.analyzedCount == 0)
          {
            msPerMethod = 0;
          }
          else
          {
            msPerMethod = (OverallAnalysisTime.InMilliSeconds / (long)this.analyzedCount);
          }

          output.Statistic("Contract density: {0:0.00}", this.ContractDensity.Density);
          output.Statistic("Total methods analyzed {0}", this.analyzedCount.ToString());
          this.clousotAnalysysisResults.MethodsTotal = this.analyzedCount;
          output.Statistic("Methods analyzed with a faster abstract domain {0}", this.methodsAnalyzedWithCheaperDomain);
          this.clousotAnalysysisResults.MethodsWithCheaperAD = this.methodsAnalyzedWithCheaperDomain;
          if (this.UseCache)
          {
            output.Statistic("Method analyses read from the cache {0}", this.cacheManager.NbCacheHits.ToString());
            this.clousotAnalysysisResults.MethodsFromCache = this.cacheManager.NbCacheHits;
            output.Statistic("Methods not found in the cache {0}", this.CacheMisses);
            this.clousotAnalysysisResults.MethodsMissedInCache = this.CacheMisses;
          }

          output.Statistic("Methods with 0 warnings {0}", this.globalStats.MethodsWithZeroWarnings().ToString());

          var str = PerformanceMeasure.GetStats();
          if (str.Length != 0)
          {
            output.Statistic("Time spent in internal, potentially costly, operations");
            output.Statistic(str.ToString());
          }

          output.Statistic("Total time {0}. {1}ms/method", OverallAnalysisTime.ToString(), msPerMethod.ToString());

          if (TimeoutExceptionFixpointComputation.ThrownExceptions > 0)
          {
            var timeout = new TimeSpan(Int32.MaxValue);
            var analysisTimeOuts = this.globalStats.MethodsByAnalysisTime(Int32.MaxValue).Where(stat => stat.OverallTime >= timeout).Count();

            this.clousotAnalysysisResults.MethodsTimedOut = analysisTimeOuts;
#if DEBUG
            output.WriteLine("Total thrown timeouts {0} (method analysis time outs: {1})", TimeoutExceptionFixpointComputation.ThrownExceptions, analysisTimeOuts);
#else
            if (analysisTimeOuts > 0)
            {
                output.WriteLine("Method analyses that timed out: {0}", analysisTimeOuts);
            }
#endif
          }

#if DEBUG
          output.Statistic("FactQuery statistics: {0}", FactQueryStatistics.Statistics);
#endif
        }
        if (options.PrintArithmeticStats)
        {
          output.WriteLine(Rational.Statistics);
          output.WriteLine("Total number of arithmetic exceptions {0}", ArithmeticExceptionRational.ThrownExceptions.ToString());

          output.WriteLine(Interval.Stats);
        }
        if (options.PrintAbstractDomainsStats)
        {
          PrintStatisticsForTheAbstractDomains(output);
        }

        // Output Inference statistics
        if (options.PrintInferenceStats)
        {
          AssumeDispatcherProfiler.DumpStatistics(output);
          PreconditionInferenceProfiler.DumpStatistics(output);
          PreconditionDispatcherProfiler.DumpStatistics(output);
          ObjectInvariantDispatcherProfiler.DumpStatistics(output);
          PostconditionDispatcherProfiler.DumpStatistics(output);
          CodeFixesProfiler.DumpStatistics(output);
          if (this.classDB != null)
          {
            this.classDB.DumpStatistics(output);
          }
          if (this.cacheManager != null)
          {
            this.cacheManager.DumpStatistics(output, options);
          }
        }
      }

      private int ComputeReturnCode()
      {
        if (!this.options.failOnWarnings) { return 0; }

        var stats = globalStats.OverallStatistics();

        // Use Math.Max(..., 0) to make sure we have a non-negative number. 
        // F: There is a bug somewhere, causing totalWarns < 0 appearing *sometimes* when a method times out. It manifests in CloudDev, reported by Dmtry
        // Probably when we throw the timeout exception, we leave the counters in a inconsistent state

        var totalWarns = Math.Max(stats.Top - output.SwallowedMessagesCount(ProofOutcome.Top) + stats.False - output.SwallowedMessagesCount(ProofOutcome.False), 0); 
        if (totalWarns > Int32.MaxValue) { return Int32.MaxValue; }
        return (int)totalWarns;
      }

      private void TrySendLeaderboardPing()
      {
#if LeaderBoard
        try {
          LeaderBoard.LeaderBoardAPI.SendLeaderBoardFeatureUse(GetLeaderBoardFeatureId());
        }
        catch {}
#endif
      }
      private static void SendLeaderBoardFailure()
      {
#if LeaderBoard
        var version = typeof(GeneralOptions).Assembly.GetName().Version;
        LeaderBoard.LeaderBoardAPI.SendLeaderBoardFailure(LeaderBoard_CCCheckId, version);
#endif
      }

      const int LeaderBoard_CCCheckId = 0x3;
      const int LeaderBoardFeatureMask_CCCheck = LeaderBoard_CCCheckId << 12;
      [Flags]
      enum FeatureIds
      {
        NonNull = 0x01,
        Bounds = 0x02,
        Arithmetic = 0x04,
        Arrays = 0x08,
        Unsafe = 0x10,
        Assumptions = 0x20,
      }
      private int GetLeaderBoardFeatureId()
      {
        int featureMask = LeaderBoardFeatureMask_CCCheck;
        if (options.Analyses != null)
        {
          foreach (var analysis in options.Analyses)
          {
            if (analysis.ObligationsEnabled)
            {
              switch (analysis.Name)
              {
                case "Non-null":
                  featureMask |= (int)FeatureIds.NonNull;
                  break;

                case "Bounds":
                  featureMask |= (int)FeatureIds.Bounds;
                  break;

                case "Arithmetic":
                  featureMask |= (int)FeatureIds.Arithmetic;
                  break;

                case "buffers":
                  featureMask |= (int)FeatureIds.Unsafe;
                  break;

                case "arrays":
                  featureMask |= (int)FeatureIds.Arrays;
                  break;

                default:
                  break;
              }
            }
          }
        }
        if (options.CheckAssumptions)
        {
          featureMask |= (int)FeatureIds.Assumptions;
        }
        return featureMask;
      }

      /// <summary>
      /// The array analysis needs the bounds analysis
      /// </summary>
      private void InitAndCheckArrayAnalysisDependences(out bool isOk)
      {
        IMethodAnalysis boundsAnalysis, arrayAnalysis, nonnullAnalysis, enumAnalysis;
        bool hasBoundsAnalysis, hasNonNullAnalysis, hasEnumAnalysis;

        isOk = true;

        var methodAnalyzers = this.methodAnalyzers;
        var analyses = this.options.Analyses;

        // Remember the cheap analysese for adaptive computation
        if((hasNonNullAnalysis= methodAnalyzers.TryGetValue("nonnull", out nonnullAnalysis)) && analyses.Contains(nonnullAnalysis))
        {
          this._cheapAnalyses.Add(nonnullAnalysis);
          this._verycheapAnalyses.Add(nonnullAnalysis);
        }
        if((hasBoundsAnalysis = methodAnalyzers.TryGetValue("bounds", out boundsAnalysis)) && analyses.Contains(boundsAnalysis))
        {
          this._cheapAnalyses.Add(boundsAnalysis);
        }
        if ((hasEnumAnalysis = methodAnalyzers.TryGetValue("enum", out enumAnalysis)) && analyses.Contains(enumAnalysis))
        {
          this._cheapAnalyses.Add(enumAnalysis);
          this._verycheapAnalyses.Add(enumAnalysis);
        }

        if (methodAnalyzers.TryGetValue("arrays", out arrayAnalysis))
        {
          if (hasBoundsAnalysis && hasNonNullAnalysis)
          {
            arrayAnalysis.SetDependency(boundsAnalysis);

            if (analyses.Contains(arrayAnalysis))
            {
              if (analyses.Contains(boundsAnalysis))
              {
                analyses.Remove(boundsAnalysis); // We do not need bounds, as the Array Analysis is already running it...

                if (analyses.Contains(nonnullAnalysis))
                {
                  arrayAnalysis.SetDependency(nonnullAnalysis);
                  analyses.Remove(nonnullAnalysis);
                }
              }
              else
              {
                isOk = false;
              }

              ArrayOptions.Trace = this.options.TraceArrayAnalysis;
            }

            if (hasEnumAnalysis && this.options.Analyses.Contains(enumAnalysis))
            {
              arrayAnalysis.SetDependency(enumAnalysis);
              analyses.Remove(enumAnalysis);
            }
          }
          else
          {
            isOk = false;
          }
        }

        // The following assert i false because the array analysis smashes together the analyses
        // Contract.Assert(_cheapAnalyses.Count <= analyses.Count); 

        if (!isOk)
        {
          this.output.WriteLine("Warning: -bounds is needed when using the -arrays option");
        }
      }

      private void InitializeThresholdsForWidening()
      {
        SimpleThreshold.UserProvided = options.thresholds;
      }

      private void InitializeCache(IOutput output)
      {
        if (this.options.cachesize > 0)
        {
          GenericCache.DEFAULT_CACHE_SIZE = this.options.cachesize;
        }
        else
        {
          GenericCache.DEFAULT_CACHE_SIZE = 16;
          output.WriteLine("Warning: non-positive cache size. Using {0} instead.", GenericCache.DEFAULT_CACHE_SIZE);
        }
      }

      private void InitializeSparseArrayRepresentation()
      {
        switch (options.rep)
        {
          case SparseArrayOptions.time:
            SparseRationalArray.InternalRepresentation = SparseRationalArray.Representation.Dictionary;
            break;

          case SparseArrayOptions.mem:
            SparseRationalArray.InternalRepresentation = SparseRationalArray.Representation.Array;
            break;

          case SparseArrayOptions.exp:
            SparseRationalArray.InternalRepresentation = SparseRationalArray.Representation.List;
            //SparseRationalArray.InternalRepresentation = SparseRationalArray.Representation.HashTableWithBuckets;
            break;

          default:
            SparseRationalArray.InternalRepresentation = SparseRationalArray.Representation.Dictionary;
            break;
        }
      }

      private void PopulateContractAssemblies(IOutputResults output)
      {
        foreach (string contractAssembly in options.ContractAssemblies)
        {
          Assembly assem;
          if (!driver.MetaDataDecoder.TryLoadAssembly(contractAssembly, assemblyCache, null, out assem, this.options.IsLegacyAssemblyMode, this.options.reference, this.options.extractSourceText))
          {
            output.WriteLine("Cannot load contract assembly '{0}'", contractAssembly);
            options.AddError();
          }
        }
      }

      internal void AnalyzeAssembly(Assembly assembly, Set<string> assembliesUnderAnalysis)
      {
        var md = this.driver.MetaDataDecoder;

        // If we know that we already have analyzed all the methods to analyze, then we stop
        if (this.methodNumbering.LastNumber >= options.AnalyzeTo)
        {
          return;
        }

        #region Order the methods appropriately

        WriteLinePhase(output, "Ordering the methods");

        IMethodOrder<Method, Type> methodOrder;
        if (options.MayPropagateInferredRequiresOrEnsures)
        {
          bool constructorsFirst = true;
          methodOrder = new CallGraphOrder<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(constructorsFirst, md, this.driver.ContractDecoder, assembliesUnderAnalysis, this.fieldsDB, this.cancellationToken);

          //methodOrder = new ConstructorsFirstAndTheCallGraphMethodOrder<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(md, this.driver.ContractDecoder, assembliesUnderAnalysis, this.fieldsDB, this.cancellationToken);          
        }
        else
        {
          methodOrder = new ProtectionBasedMethodOrder<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(md, this.driver.ContractDecoder, this.fieldsDB, this.cancellationToken);
        }

        methodOrder.AddFilter((m, i) => this.options.analyzeFrom <= i && i <= this.options.analyzeTo);
        if (options.select != null && options.select.Count > 0)
          methodOrder.AddFilter((m, i) => options.select.Contains(i));

        if (!String.IsNullOrEmpty(options.NamespaceSelect))
          methodOrder.AddFilter((m, i) => md.Namespace(md.DeclaringType(m)).StartsWith(options.NamespaceSelect));

        if (!String.IsNullOrEmpty(options.TypeNameSelect))
          methodOrder.AddFilter((m, i) => md.FullName(md.DeclaringType(m)) == options.TypeNameSelect);

        if (!String.IsNullOrEmpty(options.memberNameSelect))
          methodOrder.AddFilter((m, i) => md.DeclaringMemberCanonicalName(m) == options.memberNameSelect);

        foreach (var t in this.driver.MetaDataDecoder.GetTypes(assembly))
        {
          methodOrder.AddType(t);
        }

        var localMethodNumbers = new MethodNumbers<Method, Type>(this.methodNumbering, methodOrder, this.options.includeCalleesTransitively);
        this.methodNumbers = localMethodNumbers;

        WriteLinePhase(output, "Done ordering the methods");

        if (options.splitanalysis)
        {
          WriteLinePhase(output, "Launching the parallel analysis");

          var parallel = new Parallel<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(md, localMethodNumbers, options.bucketSize);
          parallel.RunWithBuckets(this.originalArguments);

          // We are done, no need to run clousot
          return;
        }

        this.cancellationToken.ThrowIfCancellationRequested();

        #endregion

        var totalMethods = this.methodNumbers.Count;
        var progressChars = 0;
        var stopWatch = new CustomStopwatch();
        stopWatch.Start();

        output.StartAssembly(assembly);
        if (options.outputPrettycs)
        {
          driver.ContractsHandlerManager.StartAssembly(assembly);
        }

        try
        {
          progressChars = AnalyzeAssemblyInternal(totalMethods, progressChars, stopWatch);
        }
        finally
        {
          if (options.outputPrettycs)
          {
            OutputPrettyCS.OutputHelper.OutputStrategy strat = OutputPrettyCS.OutputHelper.OutputStrategy.ONE_FILE_PER_CLASS;
            if (options.outputPrettyFileNamespace)
              strat = OutputPrettyCS.OutputHelper.OutputStrategy.ONE_FILE_PER_NAMESPACE;
            if (options.outputPrettyFileToplevelClass)
              strat = OutputPrettyCS.OutputHelper.OutputStrategy.ONE_FILE_PER_TOPLEVEL_CLASS;
            if (options.outputPrettyFileClass)
              strat = OutputPrettyCS.OutputHelper.OutputStrategy.ONE_FILE_PER_CLASS;
            driver.ContractsHandlerManager.SetStrategy(strat);

            driver.ContractsHandlerManager.PrettyCSOutput(options.outputPrettycsFolder, options.ShowProgress);

            driver.ContractsHandlerManager.EndAssembly(); // forget the inferred contracts for pretty C# printing
          }

          output.EndAssembly();
          if (options.PrintEGraphStats)
          {
            EGraphStats.PrintStats(output);
          }
          //output.WriteLine("Max Class Drivers count is: {0}", driver.MaxClassDriversCount);
        }
      }


      private int AnalyzeAssemblyInternal(int totalMethods, int progressChars, CustomStopwatch stopWatch)
      {
        // Forward object invariant inference
        if (this.options.InferObjectInvariantsForward)
        {
          this.classDB.IterationCounter = 0;
          do
          {
            this.classDB.StartNewIteration();
            output.WriteLine("** Forward object invariant inference. Step {0}", this.classDB.IterationCounter);
            progressChars = AnalyzeMethodsInAssembly(totalMethods, progressChars, stopWatch);
            this.classDB.IterationCounter++;
          }
          while (this.classDB.HasNewInvariants);
        }
        else
        {
          progressChars = AnalyzeMethodsInAssembly(totalMethods, progressChars, stopWatch);
        }
        return progressChars;
      }

      private int AnalyzeMethodsInAssembly(int totalMethods, int progressChars, CustomStopwatch stopWatch)
      {
        #region Analyze the methods in this assembly

        foreach (var method in this.methodNumbers.OrderedMethods())
        {
          DoWork(totalMethods, ref progressChars, stopWatch, method);
        }

        #endregion

        return progressChars;
      }

      private void DoWork(int totalMethods, ref int progressChars, CustomStopwatch assemblyStopWatch, Method method)
      {
        var stopWatch = new CustomStopwatch();
        stopWatch.Start();
        var orderNumber = this.methodNumbers.GetMethodNumber(method);
        var overallTime = TimeSpan.MaxValue; // Assume by default that the method has timed out
        var p = totalMethods > 0 ? (double)orderNumber / totalMethods : 1.0;

        UpdateClousotAnalysisStateStatistics(
          currAssembly : this.driver.MetaDataDecoder.Name(this.driver.MetaDataDecoder.DeclaringAssembly(method)),
          currMethodNumber: orderNumber,        
          currMethodName: this.driver.MetaDataDecoder.FullName(method),
          currPerc: p,
          phase: "preprocessing IL");

        // We use timeouts to kill analyses that are taking too much time. 
        // In a perfect world, we'd like not to have any of those timeouts          
        try
        {
          AnalyzeMethod(method);

          overallTime = stopWatch.Elapsed;
        }
        catch (TimeoutExceptionFixpointComputation)
        {
          output.WriteLine("Analysis timed out for method {0}", this.driver.MetaDataDecoder.FullName(method));
        }

#if EXPERIMENTAL
        catch (Exception)
        {
          if (this.classDB != null && this.classDB.IterationCounter > 0)
          {
            output.WriteLine("An exception occurred. We are swallowing it");
            // swallow the exception
            return;
          }
        }
#else
        catch (AggregateException ae)
        {
          if (ae.InnerExceptions.All(exc => exc is TimeoutExceptionFixpointComputation))
          {
            output.WriteLine("Analysis timed out for method {0}", this.driver.MetaDataDecoder.FullName(method));
          }
          else
          {
            throw ae;
          }
        }
        finally
        {
          var methodAnalysisTimeStatistics = new MethodAnalysisTimeStatistics(orderNumber, this.driver.MetaDataDecoder.Name(method), overallTime, this.CFGConstructionTime);

          this.globalStats.Add(methodAnalysisTimeStatistics);
        }
#endif

        if (options.ShowProgressBar)
        {
          int chars = 80 * orderNumber / totalMethods;
          if (chars > progressChars)
          {
            progressChars = chars;
            Console.Error.Write("|");
          }
        }

        if (options.ShowProgressNum)
        {
          Console.Title = string.Format("Analyzed {0,6:P1} of the methods ({1} elapsed)", p, assemblyStopWatch.Elapsed);
        }
      }

      internal void AnalyzeMethod(Method method)
      {
        // Very first thing: Let's put the timeout to zero at each new method analysis, and let's share it with the WPs
        WeakestPreconditionProver.Timeout = DFARoot.StartTimeOut(this.options.Timeout > 0 ? this.options.Timeout : DFARoot.DefaultTimeOut, this.cancellationToken);

        if (!driver.MetaDataDecoder.HasBody(method))
        {
          return;
        }

        this.cancellationToken.ThrowIfCancellationRequested();

        var md = this.driver.MetaDataDecoder;
        if (
          !driver.ContractDecoder.VerifyMethod(method, 
            this.options.AnalyzeCompilerGeneratedCode,
            !String.IsNullOrEmpty(options.NamespaceSelect) && md.Namespace(md.DeclaringType(method)).StartsWith(options.NamespaceSelect),
            !String.IsNullOrEmpty(options.TypeNameSelect) && md.FullName(md.DeclaringType(method)) == options.TypeNameSelect,
            !String.IsNullOrEmpty(options.MemberNameSelect) && md.DeclaringMemberCanonicalName(method) == options.memberNameSelect))
        {
          return;
        }

        var analysisFlags = new MethodAnalysisFlags(CheckInferredRequires: this.options.CheckInferredRequires, AllowReAnalysis: this.options.InferObjectInvariantsForward);

        if (this.methodNumbers == null)
        {
          AnalyzeMethodInternal(method, analysisFlags);
          return;
        }

        if (!this.methodNumbers.IsSelected(method))
        {
          return;
        }

        var methodNumber = this.methodNumbers.GetMethodNumber(method);

        if (this.options.WantToBreak(methodNumber))
        {
          System.Diagnostics.Debugger.Break();
        }

        if (this.options.IsRegression && !MethodHasRegressionAttribute(method))
        {
          return;
        }

        var pushed = false;
        string optionValue;
        if(this.driver.ContractDecoder.HasOptionForClousot(method, "warninglevel", out optionValue))
        {
          WarningLevelOptions value;
          if (Enum.TryParse<WarningLevelOptions>(optionValue, out value))
          {
            pushed = true;
            options.Push(value);
          }
        }

        try
        {
          if (this.options.IsFocused(methodNumber))
          {
            try
            {
              options.Save();
              options.Add(ShowOptions.il);
              //options.Add(TraceOptions.heap);
              AnalyzeMethodInternal(method, analysisFlags);
            }
            finally
            {
              options.Restore();
            }
          }
          else
          {
            AnalyzeMethodInternal(method, analysisFlags);
          }
        }
        finally
        {
          if(pushed)
          {
            options.Pop();
          }
        }

        // We are out of recursions, we can now safely remove the class drivers no longer useful
        foreach (var cdriver in this.classDriversToRemove)
        {
          this.driver.RemoveClassDriver(cdriver);
        }

        this.classDriversToRemove.Clear();
      }


      /// <summary>
      /// Call different analyze methods depending on whether the current method involves a state machine. 
      /// </summary>
      /// <param name="method"></param>
      private void AnalyzeMethodInternal(Method method, MethodAnalysisFlags analysisFlags)
      {
        output.StartMethod(method);

        this.currMethodString = () =>
        {
          var methodFullName = this.driver.MetaDataDecoder.FullName(method);
          var mdDecoder = this.driver.MetaDataDecoder;
          return string.Format("Method {0} :{1} {2} {3}",
            this.methodNumbers.GetMethodNumber(method), // methodNumbers can be null
            // mdDecoder.IsVisibleOutsideAssembly(method) ? "<externally visible>" : null,
            String.Empty,
            mdDecoder.GetMethodHashAttributeFlags(method).HasFlag(MethodHashAttributeFlags.OnlyFromCache) ? "<read from cache only>" : null,
            methodFullName);
        };

        if (this.options.ShowProgress && !this.analyzedMethods.Contains(method))
        {
#if DEBUG
          var oldColor = Console.ForegroundColor;
          Console.ForegroundColor = ConsoleColor.Yellow;
#endif
          output.WriteLine("{0}", this.currMethodString());
#if DEBUG
          Console.ForegroundColor = oldColor;
#endif
        }

        IClassDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ExternalExpression<APC, SymbolicValue>, SymbolicValue, ILogOptions, IMethodResult<SymbolicValue>> cdriver;
        AnalysisStatistics methodStats;
        var entryPC = default(APC);
        try
        {
          AnalyzeMethodInternal2(method, analysisFlags, out cdriver, out methodStats, out entryPC);

          foreach (var submethod in SubMethods.Gather(method, this.options, this.driver.MetaDataDecoder, this.driver.ContractDecoder))
          {
            if (this.options.ShowProgress && !this.analyzedMethods.Contains(submethod))
            {
              var methodFullName = this.driver.MetaDataDecoder.FullName(submethod);
              var mdDecoder = this.driver.MetaDataDecoder;
              output.WriteLine("Sub-Method {0} :{1} {2} {3}",
                this.methodNumbers.GetMethodNumber(submethod), // methodNumbers can be null
                mdDecoder.IsVisibleOutsideAssembly(submethod) ? "<externally visible>" : null,
                mdDecoder.GetMethodHashAttributeFlags(submethod).HasFlag(MethodHashAttributeFlags.OnlyFromCache) ? "<read from cache only>" : null,
                methodFullName);
            }
            AnalyzeSubMethod(submethod, analysisFlags);
            // TODO: analyze all derived methods that were generated from method.
          }
        }
        finally
        {
          if (entryPC.Block != null)
          {
            output.EndMethod(entryPC, false);
          }
          else
          {
            // was aborted re-analysis.
            // If we were in XML mode then we need to close the method
            if (this.options.IsXMLOutput)
            {
              output.EndMethod(APC.Dummy, true);
            }
          }
        }

        // Re-analyze methods, if needed because of invariant inference
        if (!this.ReAnalyzing && this.options.PropagateInferredInvariants && methodStats.InferredInvariants > 0)
        {
          this.ReAnalyzing = true;

          var flags = analysisFlags.ForConstructorReAnalysis();
          var mdd = this.driver.MetaDataDecoder;

          // If it is a proper method, and not a constructor, then we schedule for re-analysis all the constructors
          if (!mdd.IsConstructor(method))
          {
            foreach (var constructor in mdd.Methods(mdd.DeclaringType(method)).Where(m => (mdd.IsConstructor(m) && !mdd.IsStatic(m)))) 
            {
              if (driver.ContractDecoder.VerifyMethod(constructor, false, false, false, false) && this.analyzedMethods.Contains(constructor))
              {
                output.WriteLine("Re-Analysis of method {0}. Reason: cccheck inferred a more precise object invariant for the type {1}", mdd.FullName(constructor), mdd.Name(mdd.DeclaringType(method)));

                AnalyzeMethodInternal(constructor, flags);
              }
            }
          }
          else // it is a constructor. The inferred invariant is forward, so we only re-schedule for analysis the getters
          {
            foreach (var getter in mdd.Methods(mdd.DeclaringType(method)).Where(m => mdd.IsPropertyGetter(m)))
            {
              if (driver.ContractDecoder.VerifyMethod(getter, false, false, false, false) && this.analyzedMethods.Contains(getter))
              {
                output.WriteLine("Re-Analysis of method {0}. Reason: cccheck inferred a more precise object invariant for the type {1}", mdd.Name(getter), mdd.Name(mdd.DeclaringType(method)));

                AnalyzeMethodInternal(getter, flags);
              }
            }
          }

          this.ReAnalyzing = false;
        }

        // The method has been analyzed, report to the class driver
        this.MethodAnalysisIsCompleteForClassAnalysis(method, cdriver, options.Analyses);
      }

      private bool ReAnalyzing = false; // we want to avoid chains of re-analysis. As a matter of fact it is a trivial widening to the outmost fixpoint computation

      private uint ReanalyzeMethodInternal(Method method, MethodAnalysisFlags analysisFlags)
      {
        IClassDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ExternalExpression<APC, SymbolicValue>, SymbolicValue, ILogOptions, IMethodResult<SymbolicValue>> cdriver;
        AnalysisStatistics methodStats;
        APC entryPC;
        return AnalyzeMethodInternal2(method, analysisFlags, out cdriver, out methodStats, out entryPC);
      }

      private uint AnalyzeSubMethod(Method method, MethodAnalysisFlags analysisFlags)
      {
        IClassDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ExternalExpression<APC, SymbolicValue>, SymbolicValue, ILogOptions, IMethodResult<SymbolicValue>> cdriver;
        AnalysisStatistics methodStats;
        APC entryPC;
        return AnalyzeMethodInternal2(method, analysisFlags, out cdriver, out methodStats, out entryPC);
      }

      /// <returns>0 iff analysisFlags.validatingInferredPreconditons == true and the inferred preconditions are sufficient too. Otherwise the # of preconditions which are necessary but not sufficient</returns>
      private uint AnalyzeMethodInternal2(Method method, MethodAnalysisFlags analysisFlags, 
        out IClassDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ExternalExpression<APC, SymbolicValue>, SymbolicValue, ILogOptions, IMethodResult<SymbolicValue>> cdriver,
        out AnalysisStatistics methodStats, 
        out APC entryPC)
      {
        cdriver = null;
        methodStats = default(AnalysisStatistics);
        entryPC = default(APC);

        var inferredPreconditionsAlsoSufficient = 0u;
        var methodFullName = this.driver.MetaDataDecoder.FullName(method);

        if (this.analyzedMethods.Contains(method))
        {
          var declaringType = this.driver.MetaDataDecoder.DeclaringType(method);
          if (this.classDB != null && !this.classDB.HasInvariantForType(declaringType))
          {
            output.WriteLine("Re-analysis skipped as we have no object invariant on {0}", declaringType);
            return inferredPreconditionsAlsoSufficient;
          }

          if (!analysisFlags.AllowReAnalysis)
          {
            return inferredPreconditionsAlsoSufficient;
          }
        }
        else
        {
          this.analyzedCount++;
          this.analyzedMethods.Add(method);
        }

        var phasecount = 0;

        // keep max memory stats
        this.maxMemory = Math.Max(this.maxMemory, GC.GetTotalMemory(false));

        WriteLinePhase(output, "{0}: Creating the class and method driver", (phasecount++).ToString());

        cdriver = GetClassDriver(method, false);

        var stopWatch = new CustomStopwatch();
        stopWatch.Start();
        var mdriver = this.driver.MethodDriver(method, cdriver, analysisFlags.RemoveInferredPreconditions);
        this.CFGConstructionTime = stopWatch.Elapsed;
        entryPC = mdriver.CFG.Entry.GetFirstSuccessorWithSourceContext(mdriver.CFG);

        if (this.ContinuationForFailureToConnectToCache != null)
        {
          this.ContinuationForFailureToConnectToCache(entryPC);
          Contract.Assume(false, "We will never reach this point");
        }

        Trace.WriteLine(String.Format("Analyzing {0}", driver.MetaDataDecoder.FullName(method)));

        if (this.options.SuggestReadonlyFields  && mdriver.MetaDataDecoder.IsConstructor(method)) // We want the suggestion to appear only for constructors
        {
          this.fieldsDB.EmitFieldsAssignedOnlyInConstructors(entryPC, method, output);
        }

        var traceCacheHashing = this.options.TraceCacheHashing || this.methodNumbers.GetMethodNumber(method) == this.options.focusHash;

        var methodKind = MethodClassification.NewMethod; // default

        if (this.cacheManager != null)
        {
          // Compute hash before baseline
          this.cacheManager.StartMethod(mdriver, cdriver, fieldsDB, traceCacheHashing);

          if (options.UseSemanticBaseline)
          {
            // If we install the baseline, we hash again
            var foundBaseline = this.cacheManager.TryInstallAssumesFromBaselining(mdriver, method, traceCacheHashing, ref methodKind);
            if (foundBaseline)
            {
              if (methodKind == MethodClassification.IdenticalMethod && options.SkipIdenticalMethods)
              {
                if (options.TraceCache)
                {
                  Console.WriteLine("[cache]   Skipping identical baseline method {0}", driver.MetaDataDecoder.FullName(method));
                }
                this.cacheManager.AbortRecording("skipping identical");
                this.cacheManager.EndMethod(method);
                return 0;
              }
            }
            else
            {
              if (options.skipNewMethods)
              {
                if (options.TraceCache)
                {
                  Console.WriteLine("[cache]   Skipping new (non-baselined) method {0}", driver.MetaDataDecoder.FullName(method));
                }
                this.cacheManager.AbortRecording("skipping non-baseline");
                this.cacheManager.EndMethod(method);
                return 0;
              }
            }
          }
        }
        try
        {
          ContractDensity methodContractDensity;
          ByteArray hashUsed = null;
          if (!this.UseCache || !this.cacheManager.TryReplayAnalysis(method, traceCacheHashing, out methodStats, out methodContractDensity, out hashUsed))
          {
            if(this.UseCache)
            {
              this.CacheMisses++;
            }

            try
            {
              if (this.UseCache && this.options.ShowCacheMisses)
              {
                output.WriteLine("Method {0} (hash:{1}) not found in the cache", mdriver.MetaDataDecoder.FullName(method), hashUsed != null ? hashUsed.ToStringHex() : "<no hash>");
              }

              MethodAnalysisNonCached(method, ref analysisFlags, ref phasecount, methodFullName, cdriver, mdriver, out methodStats, out methodContractDensity);
            }
            catch (TimeoutException)
            {
              throw;
            }
            catch (Exception e)
            {
              if (options.InferObjectInvariantsOnlyForReadonlyFields)
              {
                // There is some bug that appears when readonly inference is on, probably because of a ill-formed expression, but all the repro I got so far are just too complex 
                this.clousotAnalysysisResults.ClousotCrashed = true;
                methodStats = default(AnalysisStatistics);
                methodContractDensity = default(ContractDensity);
                output.WriteLine("Internal error in Clousot/cccheck --- catching it, and continuing{0}Exception Type:{1}{0}Message:{2}, Stack Trace{3}", Environment.NewLine, e.GetType(), e.Message, e);
              }
              else
              {
                throw;
              }
            }
          }
          else
          {
            WriteLinePhase(output, "Found in cache", (phasecount++).ToString());
          }

          // keep density stats
          this.ContractDensity.Add(methodContractDensity);

          if (options.PrintPerMethodStatistics)
          {
            output.WriteLine(methodStats.ToString());
          }

          if (analysisFlags.CountInStats)
          {
            globalStats.Add(method, methodStats, methodKind);
          }


          if (analysisFlags.CheckInferredRequires && methodStats.Total > 0)
          {
            if (methodStats.Top == 0)
            {
              inferredPreconditionsAlsoSufficient = 0;
              output.EmitError(new System.CodeDom.Compiler.CompilerError(null, 0, 0, "", "Inferred preconditions are sufficient too"));
            }
            else
            {
              inferredPreconditionsAlsoSufficient = methodStats.Top;
              var msg = string.Format("Inferred preconditions are not sufficient: {0} assertion(s) left", methodStats.Top);
              output.EmitError(new System.CodeDom.Compiler.CompilerError(null, 0, 0, "", msg));
            }

            PreconditionInferenceProfiler.NotifyCheckInferredRequiresResult(methodStats.Top);
          }
        }
        finally
        {
          if (this.cacheManager != null)
          {
            this.cacheManager.EndMethod(method);
          }

          Trace.WriteLine(String.Format("Done Analyzing {0}. Elapsed{1}", driver.MetaDataDecoder.FullName(method), stopWatch.Elapsed));
        }


        return inferredPreconditionsAlsoSufficient;
      }


      private void MethodAnalysisNonCached(
        Method method,
        ref MethodAnalysisFlags analysisFlags,
        ref int phasecount,
        string methodFullName,
        IClassDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ExternalExpression<APC, SymbolicValue>, SymbolicValue, ILogOptions, IMethodResult<SymbolicValue>> cdriver,
        IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ExternalExpression<APC, SymbolicValue>, SymbolicValue, ILogOptions> mdriver,
        out AnalysisStatistics methodStats,
        out ContractDensity methodContractDensity)
      {
        var results = new List<IMethodResult<SymbolicValue>>(options.Analyses.Count);
        var obligations = new List<IProofObligations<SymbolicValue, BoxedExpression>>();

        // keep density stats
        methodContractDensity = ContractDensityAnalyzer.GetContractDensity(mdriver);

        WriteLinePhase(output, "{0}: Running the heap analysis", (phasecount++).ToString());
        // TODO(wuestholz): Maybe we should pass a fresh controller for the next call.
        mdriver.RunHeapAndExpressionAnalyses(null);

        var inferenceManager = (ContractInferenceManager)null;

        try
        {
          var factQuery = new ComposedFactQuery<SymbolicValue>(mdriver.BasicFacts.IsUnreachable);
          var falseObligations = (IEnumerable<MinimalProofObligation>)null;

          methodStats = new AnalysisStatistics();

          cachedExplicitAssertions = null;  // Invalidate the cache.

          phasecount = RunAdaptiveMethodAnalysis(phasecount, mdriver);

          phasecount = RunSyntacticAnalysis(phasecount, methodFullName, mdriver);

          phasecount = RunFactsDiscoveryAnalyses(method, phasecount, methodFullName, cdriver, mdriver, results, obligations, factQuery);

          phasecount = RunProofObligationsChecking(phasecount, mdriver, ref methodStats, results, obligations, out inferenceManager, out falseObligations);

          phasecount = RunExtractMethodLogic(phasecount, mdriver, factQuery);

          phasecount = RunInvariantAtLogic(phasecount, mdriver, factQuery);

          phasecount = RunContractInference(phasecount, methodFullName, mdriver, ref methodStats, results, inferenceManager, factQuery);

          phasecount = RunCodeFixesSuggestion(phasecount, method, falseObligations, mdriver, factQuery, inferenceManager);
        }
        catch (TimeoutExceptionFixpointComputation)
        {
          if (this.SaveToCache)
          {
            this.cacheManager.SaveTimeout();
          }
          throw;
        }

        if (this.SaveToCache)
        {
          this.cacheManager.SaveStatistics(methodStats);
          this.cacheManager.SaveContractDensity(methodContractDensity);

          long mask;
          if (IDecodeMetadataExtensionsForPurityInfo.TryInferredPureParametersMask(mdriver.MetaDataDecoder.FullName(mdriver.CurrentMethod), out mask))
          {
            this.cacheManager.SavePureParametersMask(mask);
          }

          // We should be careful that we do not want to save the object invariants inferred as a forward propagation, as those are not method-local
          var objectInvariantsToSave = mdriver.InferredObjectInvariants.Where(pair => pair.Two.Any(from => from.ObligationName != MinimalProofObligation.InferredForwardObjectInvariant));
          var nnFields = inferenceManager != null ? inferenceManager.PostCondition.GetNonNullFields(method).OfType<Tuple<Field, BoxedExpression>>() : Enumerable.Empty<Tuple<Field, BoxedExpression>>();

          this.cacheManager.SaveInferredContracts(mdriver.InferredPreConditions, mdriver.InferredPostConditions, objectInvariantsToSave, mdriver.InferredEntryAssumes, mdriver.InferredCalleeAssumes, nnFields, mdriver.MayReturnNull, this.options.TraceCacheHashing);
        }

        if (analysisFlags.CheckInferredRequires && methodStats.InferredPreconditions > 0)
        {
          output.WriteLine("** Checking inferred invariants for being sufficient");

          // If we are validating the inferred preconditions, CheckInferredRequires must be false
          // In that case, call to mdriver.EndAnalysis() up in the stack will do clean up

          // First install the preconditions
          mdriver.EndAnalysis();

          // Then mask the output by creating a new output
          var oldOutput = this.output;
          this.output = SelectOutput(this.GetScoreManager(), null, this.options, mdriver.MetaDataDecoder, false);

          // Then re-analyze the method, now with new flags
          var flags = analysisFlags.ForInferredPreconditionsValidation();
          var preconditionsNotSufficient = ReanalyzeMethodInternal(method, flags);

          // restore the previous output
          this.output = oldOutput;

          if (preconditionsNotSufficient == 0u)
          {
            output.EmitError(new System.CodeDom.Compiler.CompilerError(null, 0, 0, "", "Inferred preconditions are sufficient too"));
          }
          else
          {
            var msg = string.Format("!!! Inferred preconditions are not sufficient: {0} assertion(s) left", preconditionsNotSufficient);
            output.EmitError(new System.CodeDom.Compiler.CompilerError(null, 0, 0, "", msg));
          }
        }

        // Tell driver we are done with it so it can bulk add inferred contracts
        mdriver.EndAnalysis();
      }

      private StreamWriter CreateCSVOutputWriter(out bool fileExists)
      {
        StreamWriter result = null;
        var fn = string.Format("dfa_statistics.{0}.csv", Thread.CurrentThread.ManagedThreadId);
        fileExists = false;
        if (options.PrintControllerStats)
        {
          fileExists = File.Exists(fn);
          result = new StreamWriter(File.Open(fn, FileMode.Append, FileAccess.Write, FileShare.ReadWrite));
          result.AutoFlush = true;
        }
        return result;
      }

      private int RunCodeFixesSuggestion(int phasecount, Method method, IEnumerable<MinimalProofObligation> falseObligations,
        IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ExternalExpression<APC, SymbolicValue>, SymbolicValue, ILogOptions> mdriver,
        IFactQuery<BoxedExpression, SymbolicValue> factQuery,
        ContractInferenceManager inferenceManager)
      {

        if (options.suggest.Contains(SuggestOptions.codefixes) || options.suggest.Contains(SuggestOptions.codefixesshort))
        {
          WriteLinePhase(output, "{0}: Suggesting the code fixes", (phasecount++).ToString());

          if (inferenceManager.CodeFixesManager.TrySuggestFixingConstructor(mdriver.CFG.EntryAfterRequires,
            mdriver.MetaDataDecoder.FullName(method),
            mdriver.MetaDataDecoder.IsConstructor(method),
            falseObligations.Where(obl => obl.Provenance != null)) // just pass the obligations that have a non-empty provenance: if it is a constructor then should be inferred invariants
            )
          //() => inferenceManager.PostCondition.GeneratePostconditions().Contains(BoxedExpression.Const(false, mdriver.MetaDataDecoder.System_Boolean, mdriver.MetaDataDecoder)              )))              
          {
            // Do nothing???
          }

          Func<APC, SymbolicValue, IntervalStruct> typeFetcher =
            (APC pc, SymbolicValue v) =>
            {
              IntervalStruct result;
              if (v.TryGetRange<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ExternalExpression<APC, SymbolicValue>, SymbolicValue>(mdriver.Context.ValueContext.GetType(pc, v), mdriver.MetaDataDecoder, out result))
              {
                return result;
              }
              else
              {
                return IntervalStruct.None;
              }
            };

          // un-necessary?
          Func<APC, BoxedExpression, IntervalStruct> typeFetcherExps =
            (APC pc, BoxedExpression exp) =>
            {
              SymbolicValue v;
              if (exp.TryGetFrameworkVariable(out v))
              {
                return typeFetcher(pc, v);
              }
              else
              {
                return IntervalStruct.None;
              }
            };

          Func<BoxedExpression, BoxedExpression> ExtraSimplification =
            (BoxedExpression expr) =>
            {
              return expr.SimplifyConstantsInDivision(
                (long l) =>
                  BoxedExpression.Const(l, mdriver.MetaDataDecoder.System_Int64, mdriver.MetaDataDecoder));
            };

          Func<APC, BoxedExpression, bool, BoxedExpression> Simplificator =
            (APC pc, BoxedExpression expr, bool extraSimplification) =>
            {
              if (extraSimplification)
                return expr.Simplify(mdriver.MetaDataDecoder, exp => exp.IsArrayLength(pc, mdriver.Context, mdriver.MetaDataDecoder), ExtraSimplification);
              else
                return expr.Simplify(mdriver.MetaDataDecoder, exp => exp.IsArrayLength(pc, mdriver.Context, mdriver.MetaDataDecoder));
            };

          Func<APC, APC> PCWithSourceContext = pc => pc.GetFirstPredecessorWithSourceContext(mdriver.CFG);

          var expressionReader = new ExpressionReader<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ExternalExpression<APC, SymbolicValue>, SymbolicValue>();

          // Should we do asserts too?
          foreach (var guard in mdriver.AdditionalSyntacticInformation.TestsInTheMethod
            .Where(s =>
                 s.Guard != null  // we need to check, because they may be null 
              && s.Kind == SyntacticTest.Polarity.Assume
              && (!s.PC.InsideEnsuresInMethod && !s.PC.InsideInvariantInMethod) // TODO: factor out where we want to fix --- this check should be improved and moved somewhere else
              && ((s.Tag != "false" && s.Tag != "ensures"))
              && (expressionReader.Visit(s.Guard, new ReaderInfo<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ExternalExpression<APC, SymbolicValue>, SymbolicValue>(s.PC, mdriver.Context, mdriver.MetaDataDecoder)) != null)
              ))
          {
            var context = new ParametersSuggestNonOverflowingExpression<SymbolicValue>(guard.PC, PCWithSourceContext, guard.Guard, factQuery, Simplificator, (SymbolicValue v) => typeFetcher(mdriver.CFG.Post(guard.PC), v));

            if (inferenceManager.CodeFixesManager.TrySuggestNonOverflowingExpression(ref context))
            {
              // Do nothing??
            }
          }

          foreach (var exp in mdriver.AdditionalSyntacticInformation.RightExpressions.Distinct().Where(exp => (exp.RValueExpression != null && expressionReader.Visit(exp.RValueExpression, new ReaderInfo<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ExternalExpression<APC, SymbolicValue>, SymbolicValue>(exp.PC, mdriver.Context, mdriver.MetaDataDecoder)) != null)))
          {
            var context = new ParametersSuggestNonOverflowingExpression<SymbolicValue>(exp.PC, PCWithSourceContext, exp.RValueExpression, factQuery, Simplificator, (SymbolicValue v) => typeFetcher(mdriver.CFG.Post(exp.PC), v));
            if (inferenceManager.CodeFixesManager.TrySuggestNonOverflowingExpression(ref context))
            {
              // Do nothing??
            }
          }

          inferenceManager.CodeFixesManager.SuggestCodeFixes<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ExternalExpression<APC, SymbolicValue>, SymbolicValue, ILogOptions>(mdriver);
        }

        return phasecount;
      }

      // Contract inference, suggestion, and refining for the extract method
      private int RunContractInference(int phasecount, string methodFullName, IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ExternalExpression<APC, SymbolicValue>, SymbolicValue, ILogOptions> mdriver, ref AnalysisStatistics methodStats, List<IMethodResult<SymbolicValue>> results, ContractInferenceManager inferenceManager, ComposedFactQuery<SymbolicValue> factQuery)
      {
        var isCurrentMethodAProperty = mdriver.RawLayer.MetaDataDecoder.IsPropertyGetterOrSetter(mdriver.RawLayer.Decoder.Context.MethodContext.CurrentMethod);

        if (options.SuggestRequiresForArrays
            || options.SuggestRequiresPurityForArrays
            || options.PropagateRequiresPurityForArrays
            || options.PropagateInferredArrayRequires)
        {
          WriteLinePhase(output, "{0}: Inferring the analysis specific preconditions", (phasecount++).ToString());

          foreach (var analysis in results)
          {
            analysis.SuggestPrecondition(inferenceManager);
          }
        }

        // We should generate the ensures before emitting requires because of "prefrompost"
        if (options.SuggestEnsures(isCurrentMethodAProperty)
          || options.SuggestNonNullReturn
          || options.PropagateInferredEnsures(isCurrentMethodAProperty)
          || options.PropagateInferredNonNullReturn
          || options.PropagateInferredSymbolicReturn
          || options.CheckFalsePostconditions 
          )
        {
          var shouldSearchForAWitness = false;

          WriteLinePhase(output, "{0}: Inferring the postconditions", (phasecount++).ToString());
          foreach (var analysis in results)
          {
            shouldSearchForAWitness |= analysis.SuggestPostcondition(inferenceManager);
          }

          if (shouldSearchForAWitness)
          {
            var mayReturnNull = mdriver.MayReturnNull = inferenceManager.PostCondition.MayReturnNull(factQuery, new TimeOutChecker(60, this.cancellationToken));
#if DEBUG
            if (mayReturnNull)
            {
              Console.WriteLine("This method may return null");
            }
#endif
          }
        }

        if (options.SuggestAssumes)
        {
          methodStats.InferredEntryAssumes = inferenceManager.Assumptions.SuggestEntryAssumes();
        }

        if (options.SuggestAssumesForCallees || options.SuggestNecessaryPostconditions)
        {
          /* var dummy = */ inferenceManager.Assumptions.SuggestCalleeAssumes(options.SuggestAssumesForCallees, options.SuggestNecessaryPostconditions, options.suggestcalleeassumeswithdisjunctions);
        }

        if (options.SuggestRequires || options.TreatMissingPublicRequiresAsErrors)
        {
          var isCurrentMethodVisible = mdriver.MetaDataDecoder.IsVisibleOutsideAssembly(mdriver.Context.MethodContext.CurrentMethod);

          if (options.SuggestRequires || isCurrentMethodVisible)
          {
            var howManyPreconditions = methodStats.InferredPreconditions = inferenceManager.PreCondition.Dispatch.SuggestPreconditions(isCurrentMethodVisible && options.TreatMissingPublicRequiresAsErrors, options.IsXMLOutput);

            if (howManyPreconditions > 0 && options.TreatMissingPublicRequiresAsErrors)
            {
              methodStats.Add(ProofOutcome.Top, (uint)howManyPreconditions);
            }

            if (methodStats.InferredPreconditions > 0)
            {
              PreconditionInferenceProfiler.NotifyMethodWithAPrecondition();
            }
          }
        }

        if (options.SuggestRequiresBase)
        {
          this.overriddenMethodsPreconditionsDispatcher.SuggestPotentialPreconditionsFor(this.output, mdriver.CFG.EntryAfterRequires, mdriver.CurrentMethod);
        }

        inferenceManager.PreCondition.Dispatch.PropagatePreconditions(options.PropagateInferredRequires(isCurrentMethodAProperty));

        if (options.SuggestEnsures(isCurrentMethodAProperty)
          || options.SuggestNonNullReturn)
        {
          /*dummy =*/ inferenceManager.PostCondition.SuggestPostconditions();
        }

        // Probably we do not need this test, as the code in propagate postconditions makes sure of it
        if (options.PropagateInferredEnsures(isCurrentMethodAProperty)
          || options.PropagateInferredNonNullReturn
          || options.PropagateInferredSymbolicReturn)
        {
          inferenceManager.PostCondition.PropagatePostconditions();
        }

        if(options.PropagateInferredEnsuresForProperties)
        {
          inferenceManager.PostCondition.PropagatePostconditionsForProperties();
        }

        if (options.CheckFalsePostconditions)
        { 
          var outcome = inferenceManager.PostCondition.EmitWarningIfFalseIsInferred();
          if (outcome)
          {
            methodStats.Add(ProofOutcome.Top); // update statistics 
          }
        }

        if (options.SuggestObjectInvariants)
        {
          methodStats.InferredInvariants = inferenceManager.ObjectInvariant.SuggestObjectInvariants();

          if (options.inferencemode == InferenceMode.Aggressive)
          {
            inferenceManager.PostCondition.SuggestNonNullFieldsForConstructors();
          }
        }

        if (options.PropagateInferredInvariants)
        {
          if (!options.disableForwardObjectInvariantInference)
          {
            var expressions = inferenceManager.PostCondition.SuggestNonNullObjectInvariantsFromConstructorsForward();

#if DEBUG 
            foreach (var be in expressions)
            {
              Console.WriteLine("[INFERENCE]: We push the *forward* object invariant {0} to the inference manager", be);
            }
#endif
            if (expressions.Any())
            {
              var dummyProofObligation = MinimalProofObligation.GetFreshObligationForBoxingForwardObjectInvariant(mdriver.CFG.Entry, methodFullName, mdriver.MetaDataDecoder);

              inferenceManager.ObjectInvariant.AddObjectInvariants(dummyProofObligation, expressions, ProofOutcome.True);
            }
          }
          
          methodStats.InferredInvariants = inferenceManager.ObjectInvariant.PropagateObjectInvariants(options.PropagateInferredInvariants);

#if DEBUG 
            Console.WriteLine("[INFERENCE]: We inferred {0} invariants", methodStats.InferredInvariants);
#endif        
        }

        inferenceManager.Assumptions.PropagateAssumes();

        if (options.extractmethodmodeRefine != null
          && mdriver.AdditionalSyntacticInformation.AssertedPostconditions != null
          && methodFullName.Contains(options.extractmethodmodeRefine))
        {
          WriteLinePhase(output, "{0}: Refining the precondition", (phasecount++).ToString());

          var postconditionPropagation = new BackwardsPostconditionPropagation.PreconditionsInferenceBackwardSymbolic<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ExternalExpression<APC, SymbolicValue>, SymbolicValue, ILogOptions>
              (factQuery, mdriver);

          postconditionPropagation.SuggestContractsForExtractedMethod(mdriver.AdditionalSyntacticInformation.AssertedPostconditions.ToList(), this.output);
        }

        if ((options.InferObjectInvariantsForward || options.SuggestObjectInvariantsForward))
        {
          RunForwardObjectInvariantInference(mdriver, factQuery);
        }

        return phasecount;
      }

      private void RunForwardObjectInvariantInference(IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ExternalExpression<APC, SymbolicValue>, SymbolicValue, ILogOptions> mdriver, ComposedFactQuery<SymbolicValue> factQuery)
      {
        var currMethod = mdriver.Context.MethodContext.CurrentMethod;
        var analysisStep = this.classDB.IterationCounter;

        if ((analysisStep == 0 && !mdriver.MetaDataDecoder.IsConstructor(currMethod)) || mdriver.MetaDataDecoder.IsStatic(currMethod))
        {
          return;
        }
        var postState = factQuery.InvariantAt(mdriver.CFG.NormalExit, null /* no filter */, true);

#if DEBUG
        output.WriteLine("Exit state: {0}", postState);
#endif
        var currType = mdriver.MetaDataDecoder.DeclaringType(currMethod);

        Func<SymbolicValue, FList<PathElement>> accessPathFetcher = v => mdriver.Context.ValueContext.VisibleAccessPathListFromPost(mdriver.CFG.NormalExit, v);
        var removeVars = new RemoveFrameworkVariables<SymbolicValue>(accessPathFetcher);
        var readonlyVars = new FilterVars<SymbolicValue>(accessPathFetcher, (v, p) => !mdriver.MetaDataDecoder.IsReadOnly(p.ToFList()));
        var posts = postState.GetEnumerable()
          .Distinct()
          .ApplyToAll<BoxedExpression, BoxedExpression>(exp => exp != null ? readonlyVars.Visit(exp, default(Void)) : null)
          .ApplyToAll<BoxedExpression, BoxedExpression>(exp => exp != null ? removeVars.Visit(exp, default(Void)) : null).ToFList();

#if false
        // first iteration, we are interested only in constructors
        if (analysisStep == 0)
#endif
        {
          var allConstructorsAnalyzed = this.classDB.NotifyConstructorAnalysisDone(currType, currMethod, posts);
          if (allConstructorsAnalyzed)
          {
#if DEBUG
            output.WriteLine("We saw all the constructors for the type {0}, so we perform the join", mdriver.MetaDataDecoder.Name(currType));
#endif
            var join = this.classDB.Join(currType);
            if (join != null && join.Any())
            {
              output.WriteLine("Inferred invariant");
              foreach (var j in join)
              {
                output.WriteLine(j);
                mdriver.AddObjectInvariant(j, null);
              }
            }
            else
            {
              output.WriteLine("No object invariant for {0}", mdriver.MetaDataDecoder.Name(currType));
            }
          }
        }
#if false
        else
        {
          var allMethodsAnalyzed = this.classDB.NotifyMethodOrConstructorAnalysisDone(currType, currMethod, posts);

          if (allMethodsAnalyzed)
          {
#if DEBUG
            output.WriteLine("We saw all the methods for the type {0}, so we perform the join", mdriver.MetaDataDecoder.Name(currType));
#endif
            var join = this.classDB.Join(currType);
            if (join != null && join.Any())
            {
              output.WriteLine("Inferred invariant");
              foreach (var j in join)
              {
                output.WriteLine(j);
                mdriver.AddObjectInvariant(j, null);
              }
            }
            else
            {
              output.WriteLine("No object invariant for {0}", mdriver.MetaDataDecoder.Name(currType));
            }
          }
        }
#endif
      }

      private int RunExtractMethodLogic(int phasecount, IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ExternalExpression<APC, SymbolicValue>, SymbolicValue, ILogOptions> mdriver, ComposedFactQuery<SymbolicValue> factQuery)
      {
        if (this.options.extractmethodmode)
        {
          phasecount = SuggestInvariantsForExtractMethod(phasecount, mdriver, factQuery);
        }

        return phasecount;
      }

      /// <summary>
      /// Gets the invariants at some given program points.
      /// We use it for getting the invariants, but also the call invariants
      /// </summary>
      private int RunInvariantAtLogic(int phasecount, IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ExternalExpression<APC, SymbolicValue>, SymbolicValue, ILogOptions> mdriver, ComposedFactQuery<SymbolicValue> factQuery)
      {
        if (this.options.invariantsuggestmode)
        {
          phasecount = SuggestsAbstractStateAt(phasecount, mdriver, factQuery);
        }

        if (this.options.SuggestCallInvariants)
        {
          phasecount = SuggestCallInvariants(phasecount, mdriver, factQuery);
        }

        return phasecount;
      }

      private AnalysisStatistics FailingObligations(
          IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ExternalExpression<APC, SymbolicValue>, SymbolicValue, ILogOptions> mdriver,
          List<IMethodResult<SymbolicValue>> results,
          List<IProofObligations<SymbolicValue, BoxedExpression>> obligations,
          IOutputFullResults<Method, Assembly> output)
      {
        var result = new AnalysisStatistics();

        AssertionStatistics dummy;
        var explicitAssertions = ExplicitAssertions(mdriver, out dummy, null);

        var facts = CreateFactQuery(mdriver.BasicFacts.IsUnreachable, results);

        // Create the contract inference manager
        var inferenceManager = CreateContractInferenceManager(MergeIntoAFlatList(obligations, explicitAssertions), facts, mdriver, output);

        obligations.ForEach(obl => obl.ResetCachedOutcomes());
        
        // validate implicit obligations
        foreach (var obl in obligations)
        {
          obl.Validate(output, inferenceManager, facts);
          result.Add(obl.Statistics);
        }

        if (options.CheckAssertions)
        {
          var stats = AssertionFinder.ValidateAssertions(explicitAssertions, facts, inferenceManager, mdriver, output);
          result.Add(stats);
        }

        return result;
      }

      // Check the proof obligations
      private int RunProofObligationsChecking(
          int phasecount,
          IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ExternalExpression<APC, SymbolicValue>, SymbolicValue, ILogOptions> mdriver,
          ref AnalysisStatistics methodStats,
          List<IMethodResult<SymbolicValue>> results,
          List<IProofObligations<SymbolicValue, BoxedExpression>> obligations,
        out ContractInferenceManager inferenceManager,
        out IEnumerable<MinimalProofObligation> falseObligations)
      {
        AssertionStatistics localAssertStats;

        falseObligations = null;

        // Collect explicit obligations - we need them for precondition inference
        var explicitAssertions = ExplicitAssertions(mdriver, out localAssertStats, null);

        var facts = CreateFactQuery(mdriver.BasicFacts.IsUnreachable, results);

        // Create the contract inference manager
        inferenceManager = CreateContractInferenceManager(MergeIntoAFlatList(obligations, explicitAssertions), facts, mdriver, output);

        obligations.ForEach(obl => obl.ResetCachedOutcomes());

        // validate implicit obligations
        foreach (var obl in obligations)
        {

          WriteLinePhase(output, "{0}: Validating implicit {1} assertions", (phasecount++).ToString(), obl.Name);

          obl.Validate(this.output, inferenceManager, facts);

          methodStats.Add(obl.Statistics);
        }

        if (options.CheckAssertions)
        {
          WriteLinePhase(output, "{0}: Validating the Explicit assertions", (phasecount++).ToString());

          methodStats.Add(AssertionFinder.ValidateAssertions(explicitAssertions, facts, inferenceManager, mdriver, output));

          if (options.PrintMethodAssertStats)
          {
            localAssertStats.Show(output);
          }
          this.assertStats.Add(localAssertStats);

          falseObligations = explicitAssertions.Where(obl => obl.Outcome == ProofOutcome.False).ApplyToAll(o => o.MinimalProofObligation);
        }

        return phasecount;
      }

      AssertionFinder.AssertionObligations<SymbolicValue> cachedExplicitAssertions;
      AssertionStatistics cachedLocalAssertStats;

      private AssertionFinder.AssertionObligations<SymbolicValue> ExplicitAssertions(IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ExternalExpression<APC, SymbolicValue>, SymbolicValue, ILogOptions> mdriver, out AssertionStatistics localAssertStats, DFAController controller)
      {
        if (cachedExplicitAssertions != null)
        {
          localAssertStats = cachedLocalAssertStats;
          return cachedExplicitAssertions;
        }
        cachedExplicitAssertions = AssertionFinder.GatherAssertions(mdriver, output, out localAssertStats);
        cachedLocalAssertStats = localAssertStats;
        return cachedExplicitAssertions;
      }

      private int RunFactsDiscoveryAnalyses(
          Method method,
          int phasecount,
          string methodFullName,
          IClassDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ExternalExpression<APC, SymbolicValue>, SymbolicValue, ILogOptions, IMethodResult<SymbolicValue>> cdriver,
          IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ExternalExpression<APC, SymbolicValue>, SymbolicValue, ILogOptions> mdriver,
          List<IMethodResult<SymbolicValue>> results,
          List<IProofObligations<SymbolicValue, BoxedExpression>> obligations,
          ComposedFactQuery<SymbolicValue> factQuery,
          Func<object, int> failingObligations = null)
      {
        var moveNextStartState = mdriver.MetaDataDecoder.MoveNextStartState(method);

        IMethodAnalysisClientFactory<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ExternalExpression<APC, SymbolicValue>, SymbolicValue, IIteratorClient<SymbolicValue>> factory = null;
        
        if (moveNextStartState.HasValue)
        {
          factory = IteratorAnalyzer.Create(mdriver, moveNextStartState.Value);
        }

        List<IMethodAnalysis> analysesForThisMethod;

        if(options.adaptive && mdriver.SyntacticComplexity.SuggestACheapAnalysis)
        {
          var forcePrint = true;

#if DEBUG
          forcePrint = true;
#endif

          if(forcePrint || options.TraceDFA || options.ShowPhases)
          {
            output.WriteLine(string.Format("cccheck thinks that this method ({0}) is too complex and it may timeout on it. Therefore it reverts to a less clever analysis.", mdriver.MetaDataDecoder.Name(method)));
            this.methodsAnalyzedWithCheaperDomain++;
          }

          analysesForThisMethod = this._cheapAnalyses;
          // Let's avoid very cheap analyses as they may cause too imprecision...
          //          analysesForThisMethod = mdriver.SyntacticComplexity.WayTooManyJoins ? this._verycheapAnalyses : this._cheapAnalyses;
        }
        else
        {
          analysesForThisMethod = options.Analyses;
        }

        foreach (var analysis in analysesForThisMethod)
        {
          #region Run the analysis

          if (options.TraceDFA)
          {
            output.WriteLine("{0} analysis of {1}", analysis.Name, methodFullName);
          }

          WriteLinePhase(output, "{0}: Running the {1} analysis", (phasecount++).ToString(), analysis.Name);

          try
          {
            {
              var obl = analysis.GetProofObligations(methodFullName, mdriver);
              var cachePCs = obl == null ? null : (Predicate<APC>)obl.PCWithProofObligation;
              if (obl != null) obligations.Add(obl);

              IMethodResult<SymbolicValue> result;
              DFAController controller = null;
              try
              {
                controller = CreateFreshDFAController(analysis.Name, methodFullName, mdriver, results, obligations);

                if (factory != null)
                {
                  var iteratorAnalysis = analysis.Instantiate(methodFullName, mdriver, cachePCs, factory, controller);
                  result = iteratorAnalysis.Analyze();
                }
                else
                {
                  result = analysis.Analyze(methodFullName, mdriver, cachePCs, factQuery, controller);
                }
              }
              finally
              {
                if (controller != null)
                {
                  TraceSuspendedAPCs(methodFullName, analysis, controller);
                }
              }

              results.Add(result);

              factQuery.Add(result.FactQuery);

              RecordMethodAnalysisForClassAnalysis(method, mdriver, cdriver, analysis, result);
            }
          }
          catch (TimeoutExceptionFixpointComputation e)
          {
            output.WriteLine("{2} Analysis timed out for method #{0} {1}. Try increase the timeout using the -timeout n switch",
              this.methodNumbers.GetMethodNumber(method), // methodNumbers can be null
              this.driver.MetaDataDecoder.FullName(method),
              analysis.Name);

            var result = e.Result as IMethodResult<SymbolicValue>;
            if (result != null)
            {
                results.Add(result);
                factQuery.Add(result.FactQuery);
                RecordMethodAnalysisForClassAnalysis(method, mdriver, cdriver, analysis, result);
            }

            break; // we are done
          }

          #endregion
        }
        return phasecount;
      }

      private void TraceSuspendedAPCs(string methodFullName, IMethodAnalysis analysis, DFAController controller)
      {
        if (options.TraceSuspended)
        {
          if (controller.SuspendedAPCs != null)
          {
            if (0 < controller.SuspendedAPCs.Count)
            {
              Console.WriteLine("[SUSPENDED] Finished analysis ({0}) of method '{1}' with the following suspended program points: {2}", analysis.Name, methodFullName, string.Join(", ", controller.SuspendedAPCs));
            }
            else
            {
              Console.WriteLine("[SUSPENDED] Finished analysis ({0}) of method '{1}' with no suspended program points", analysis.Name, methodFullName);
            }
          }
        }
      }

      private DFAController CreateFreshDFAController(string analysisName, string methodName, IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ExternalExpression<APC, SymbolicValue>, SymbolicValue, ILogOptions> mdriver, List<IMethodResult<SymbolicValue>> results, List<IProofObligations<SymbolicValue, BoxedExpression>> obligations)
      {
        bool exists;
        var tw = CreateCSVOutputWriter(out exists);
        var result = new DFAController(analysisName, methodName, options.MaxCalls, options.MaxJoins, options.MaxWidenings, options.MaxSteps, (r) => { var rs = new List<IMethodResult<SymbolicValue>>(results); var mr = r as IMethodResult<SymbolicValue>; if (r != null) { rs.Add(mr); } return FailingObligations(mdriver, rs, obligations, new IgnoreOutputFactory<Method, Assembly>().GetOutputFullResultsProvider(mdriver.Options)); }, tw, mdriver.ModifiedAtCall);
        if (options.PrintControllerStats && !exists)
        {
          result.PrintStatisticsCSVHeader();
        }
        return result;
      }

      private int RunSyntacticAnalysis(int phasecount, string methodFullName, IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ExternalExpression<APC, SymbolicValue>, SymbolicValue, ILogOptions> mdriver)
      {
        if (this.options.refinedisjunctions)
        {
          WriteLinePhase(output, "{0}: Running the Syntactic (Disjunctive expressions, guard fetcher, etc.) analysis", (phasecount++).ToString());

          var result = AnalysisWrapper.RunDisjunctionRecoveryAnalysis(methodFullName, mdriver, pc => true);
          mdriver.DisjunctiveExpressionRefiner = result.DisjunctionRefiner;
          mdriver.AdditionalSyntacticInformation = result;
        }

        return phasecount;
      }

      private int RunAdaptiveMethodAnalysis(int phasecount, IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ExternalExpression<APC, SymbolicValue>, SymbolicValue, ILogOptions> mdriver)
      {
        if (this.options.adaptive)
        {
          WriteLinePhase(output, "{0}: Running the Syntactic Complexity analysis", (phasecount++).ToString());

          mdriver.SyntacticComplexity = SyntacticComplexityInfo.GetSyntacticComplexity(mdriver);

          if (options.PrintProgramStats || options.TraceDFA)
          {
            var complexity = mdriver.SyntacticComplexity;
            output.WriteLine("Method stats: {0}", complexity.ToString());
            output.WriteLine("Clousot thinks that: Too many instructions = {0}; Too many joins = {1}; Too many joins for backwards propagation = {2}; {3} Too many loops = {4}", complexity.TooManyInstructionsPerJoin, complexity.TooManyJoins, complexity.TooManyJoinsForBackwardsChecking, complexity.WayTooManyJoins? "Way TOO many joins = True;" : "", complexity.TooManyLoops);
          }
        }
        else
        {
          mdriver.SyntacticComplexity = SyntacticComplexity.Dummy;
        }
        return phasecount;
      }

      private int SuggestInvariantsForExtractMethod(
        int phasecount,
        IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ExternalExpression<APC, SymbolicValue>, SymbolicValue, ILogOptions> mdriver,
        ComposedFactQuery<SymbolicValue> factQuery)
      {
        Contract.Requires(phasecount >= 0);
        Contract.Requires(mdriver != null);
        Contract.Requires(factQuery != null);

        Contract.Ensures(Contract.Result<int>() == phasecount + 1);

        WriteLinePhase(output, "{0}: Running the analysis to infer the extract method markers", (phasecount++).ToString());

        var markers = MarkerFetcher.FindMethodExtractMarkers(mdriver);

        if (markers.Version == 2)    // sanity check
        {

          var varsToRetain = markers.Precondition.Variables;
          if (varsToRetain != null)
          {
            var preconditions = factQuery.InvariantAt(markers.Precondition.PC, varsToRetain, true);

            foreach (var pre in preconditions.GetEnumerable().SyntacticReductionRemoval())
            {
              if (pre != null)
              {
                var preStr = pre.ToString();
                if (preStr != null)
                {
                  output.Suggestion(ClousotSuggestion.Kind.RequiresExtractedMethod, ClousotSuggestion.Kind.RequiresExtractedMethod.Message(),
                    markers.Precondition.PC, string.Format("Contract.Requires({0});", preStr), null, ClousotSuggestion.ExtraSuggestionInfo.None);
                }
              }
            }
          }

          var allVars = markers.Postcondition.HasReturnVariable
            ? markers.Postcondition.Variables.Cons(markers.Postcondition.ReturnVariable)
            : markers.Postcondition.Variables;

          var postconditions = factQuery.InvariantAt(markers.Postcondition.PC, allVars, true);

          if (markers.Postcondition.HasReturnVariable)
          {
            postconditions = postconditions.Map(exp =>
            {
              var type = mdriver.Context.ValueContext.GetType(markers.Postcondition.PC, markers.Postcondition.ReturnVariable);
              if (type.IsNormal)
              {
                return exp.Substitute(BoxedExpression.Var(markers.Postcondition.ReturnVariable), BoxedExpression.Result<Type>(type.Value));
              }
              else
              {
                return null;
              }
            }
            );
          }

          foreach (var post in postconditions.GetEnumerable().SyntacticReductionRemoval())
          {
            // It may be the case that the substitution above failed in some cases
            if (post != null)
            {
              var postStr = post.ToString();
              if (postStr != null)
              {
                output.Suggestion(ClousotSuggestion.Kind.EnsuresExtractedMethod, ClousotSuggestion.Kind.EnsuresExtractedMethod.Message(),
                  markers.Postcondition.PC, string.Format("Contract.Ensures({0});", postStr), null, ClousotSuggestion.ExtraSuggestionInfo.None);
              }
            }
          }
        }

        return phasecount;
      }

      private int SuggestsAbstractStateAt(int phasecount, IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ExternalExpression<APC, SymbolicValue>, SymbolicValue, ILogOptions> mdriver, ComposedFactQuery<SymbolicValue> factQuery)
      {
        Contract.Requires(phasecount >= 0);
        Contract.Requires(mdriver != null);
        Contract.Requires(factQuery != null);

        Contract.Ensures(Contract.Result<int>() == phasecount + 1);

        var inv = MarkerFetcher.FindMethodExtractMarkers(mdriver).Invariant;

        if (inv.PC.Block != null) // rule out some errors
        {
          var abstractState = factQuery.InvariantAt(inv.PC, inv.Variables, true);

          var buff = new StringBuilder();

          foreach (var aVal in abstractState.GetEnumerable().SyntacticReductionRemoval())
          {
            buff.AppendFormat("{0}, ", aVal);
          }

          if (buff.Length != 0)
          {
            buff.Remove(buff.Length - 2, 2);
          }

          output.Suggestion(ClousotSuggestion.Kind.AbstractState, ClousotSuggestion.Kind.AbstractState.Message(),
            inv.PC, buff.ToString(), null, ClousotSuggestion.ExtraSuggestionInfo.None);
        }
        return phasecount + 1;
      }

      private int SuggestCallInvariants(int phasecount, IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ExternalExpression<APC, SymbolicValue>, SymbolicValue, ILogOptions> mdriver, ComposedFactQuery<SymbolicValue> factQuery)
      {
        Contract.Requires(phasecount >= 0);
        Contract.Requires(mdriver != null);
        Contract.Requires(factQuery != null);

        Contract.Ensures(Contract.Result<int>() == phasecount + 1);

        var mdDecoder = mdriver.MetaDataDecoder;
        var currMethod = mdriver.CurrentMethod;

        foreach (var call in mdriver.AdditionalSyntacticInformation.MethodCalls)
        {
          var pos = 0;
          var isInstanceMethod = !mdDecoder.IsStatic(call.Callee);
          foreach (var v in call.ActualParameters)
          {
            if (pos == 0 && isInstanceMethod)
            {
              // skip this
              goto next;
            }

            if(!mdDecoder.IsReferenceType(mdDecoder.ParameterType(mdDecoder.Parameters(call.Callee)[isInstanceMethod? pos-1 : pos])))
            {
              goto next;
            }
            
            var isNotnull = factQuery.IsNonNull(call.PC, v);
            if (isNotnull == ProofOutcome.True)
            {
              this.callInvariantsInferenceManager.Notify(call.Callee, currMethod, pos, PredicateNullness.NotNull);
              goto next;
            }
            var isNull = factQuery.IsNull(call.PC, v);
            if (isNull == ProofOutcome.True)
            {
              this.callInvariantsInferenceManager.Notify(call.Callee, currMethod, pos, PredicateNullness.Null);
              goto next;
            }

            this.callInvariantsInferenceManager.Notify(call.Callee, currMethod, pos, PredicateTop.Value);

          next:
            pos++;
          }
        }

        // Useful only for regression and debugging
        if (this.options.SuggestCallInvariants
          && (this.options.IsRegression ||
#if DEBUG
          true
#else
          false
#endif
          ))
        {
          var entryPC = mdriver.CFG.Entry;
          foreach (var inv in this.callInvariantsInferenceManager.GetCalleeInvariantsInAMethod(currMethod))
          {
            var str = string.Format("The call invariant for {0}, pos: {1} is {2}", mdDecoder.Name(inv.Callee), inv.ParamPosition, inv.Predicate);
            this.output.Suggestion(ClousotSuggestion.Kind.CalleeInvariant, ClousotSuggestion.Kind.CalleeInvariant.Message(), entryPC, str, null, ClousotSuggestion.ExtraSuggestionInfo.None);
          }
        }

        return phasecount + 1;
      }

      private List<IProofObligations<Variable, BoxedExpression>>
        MergeIntoAFlatList<Variable>(
        List<IProofObligations<Variable, BoxedExpression>> obligations,
        AssertionFinder.AssertionObligations<Variable> explicitAssertions)
      {
        #region Contracts

        Contract.Requires(obligations != null);
        Contract.Requires(explicitAssertions != null);

        Contract.Ensures(Contract.Result<List<IProofObligations<Variable, BoxedExpression>>>() != null);

        #endregion

        var result = new List<IProofObligations<Variable, BoxedExpression>>();
        result.AddRange(obligations);
        result.Add(explicitAssertions);

        return result;
      }

      private static IFactQuery<BoxedExpression, Variable> CreateFactQuery<Variable>(
        Predicate<APC> isUnreachable,
        List<IMethodResult<Variable>> results)
      {
        var composed = new ComposedFactQuery<Variable>(isUnreachable);

        composed.Add(new ConstantPropagationFactQuery<Variable>());

        foreach (var result in results) { composed.Add(result.FactQuery); }

#if USE_FACTQUERY_WITHMEMORY
        return new FactQueryWithMemory<Variable>(composed);
#else
        return composed;
#endif
      }

      #region Class analysis helpers

      private void MethodAnalysisIsCompleteForClassAnalysis(
        Method method,
        IClassDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ExternalExpression<APC, SymbolicValue>, SymbolicValue, ILogOptions, IMethodResult<SymbolicValue>> cdriver,
        IList<IMethodAnalysis> analyses
        )
      {
        if (cdriver == null) return; // no class analysis
        cdriver.MethodHasBeenFullyAnalyzed(method);

        // Then check if it's the end of constructors analysis
        if (cdriver.PendingConstructors == 0)
          this.LaunchEndOfConstructorAnalysis(cdriver, analyses);

        // remove the information from the map, it's useless from now on
        // but wait the end of the analysis of all methods currently on the stack to actually remove it
        if (cdriver.IsClassFullyAnalyzed())
          this.classDriversToRemove.Add(cdriver);
      }

      private static void RecordMethodAnalysisForClassAnalysis(
        Method method,
        IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ExternalExpression<APC, SymbolicValue>, SymbolicValue, ILogOptions> mdriver,
        IClassDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ExternalExpression<APC, SymbolicValue>, SymbolicValue, ILogOptions, IMethodResult<SymbolicValue>> cdriver,
        IMethodAnalysis analysis,
        IMethodResult<SymbolicValue> result
        )
      {
        if (cdriver == null) return; // no class analysis
        cdriver.MethodHasBeenAnalyzed(analysis.Name, result, method);
      }

      private IClassDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ExternalExpression<APC, SymbolicValue>, SymbolicValue, ILogOptions, IMethodResult<SymbolicValue>> GetClassDriver(Method method, bool inferOnlyForReadonlyFields)
      {
        if (this.options.ClassAnalyses.Any() || this.options.PropagateInferredInvariants || this.options.InferObjectInvariantsForward)
        {
          return driver.ClassDriver(driver.MetaDataDecoder.DeclaringType(method), inferOnlyForReadonlyFields);
        }
        else
        {
          return null;
        }
      }

      private void LaunchEndOfConstructorAnalysis(
        IClassDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ExternalExpression<APC, SymbolicValue>, SymbolicValue, ILogOptions, IMethodResult<SymbolicValue>> cdriver,
        IList<IMethodAnalysis> analyses
        )
      {
        string clname = OutputPrettyCS.TypeHelper.TypeFullName(cdriver.MetaDataDecoder, cdriver.ClassType);
        // launch the post-constructors analyses
        foreach (var clanalysis in options.ClassAnalyses)
        {
          // DEBUG: output.WriteLine(clanalysis.Name + " has been found!");
          if (clanalysis.ShouldBeCalledAfterConstructors())
            clanalysis.Analyze(clname, cdriver, analyses);
        }
      }

      #endregion

      private bool MethodHasRegressionAttribute(Method method)
      {
        var mdd = this.driver.MetaDataDecoder;
        foreach (var attr in mdd.GetAttributes(method))
        {
          var typeName = driver.MetaDataDecoder.Name(mdd.AttributeType(attr));
          if (typeName == "ClousotRegressionTestAttribute")
          {
            var anyArgs = false;
            foreach (var arg in mdd.PositionalArguments(attr).Enumerate())
            {
              anyArgs = true;
              string symbol = arg as string;
              if (symbol != null && this.options.define.Contains(symbol))
              {
                return true;
              }
            }
            if (!anyArgs) return true; // default configuration
          }
        }
        return false;
      }

      /// <summary>
      /// Returns true if the method has the attribute of CompilerGenerated or of DebuggerNonUserCode
      /// </summary>
      private bool MethodIsAutomaticallyGenerated(Method method)
      {
        var mdd = driver.MetaDataDecoder;
        // Look at the method
        return (mdd.IsCompilerGenerated(method) || mdd.IsCompilerGenerated(mdd.DeclaringType(method)) || mdd.IsDebuggerNonUserCode(method) || mdd.IsDebuggerNonUserCode(mdd.DeclaringType(method)));
      }

      private IFactQuery<BoxedExpression, SymbolicValue> FactQueryDependentAnalyses(IOutput output, String name,
        Dictionary<String, IFactQuery<BoxedExpression, SymbolicValue>> analysesDone, Dictionary<String, Set<String>> dependences,
        IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ExternalExpression<APC, SymbolicValue>, SymbolicValue, ILogOptions> mdriver
        )
      {
        Set<String> analysesNeeded;
        var result = new ComposedFactQuery<SymbolicValue>(mdriver.BasicFacts.IsUnreachable);

        if (dependences.TryGetValue(name, out analysesNeeded))
        {
          foreach (String needAnalysisName in analysesNeeded)
          {
            IFactQuery<BoxedExpression, SymbolicValue> factQuery;
            if (analysesDone.TryGetValue(needAnalysisName, out factQuery))
            {
              result.Add(factQuery);
            }
            else
            {
              output.WriteLine("The {0} analysis requires the {1} analysis. Try adding the switch -{1}", name, needAnalysisName);
              output.WriteLine("Quitting...");
              throw new ExitRequestedException(-1);
            }
          }
        }

        return result as IFactQuery<BoxedExpression, SymbolicValue>;
      }

      private void PrintStatisticsForTheAbstractDomains(IOutput output)
      {
        var domains = typeof(IAbstractDomain);
        var assembly = domains.Assembly;
        var candidates = assembly.GetTypes();

        foreach (var t in candidates)
        {
          var method = t.GetMethod("get_Statistics");
          if (method != null && !method.ContainsGenericParameters && method.GetParameters().Length == 0 && method.IsStatic)
          {
            string result = (method.Invoke(null, new object[] { })) as string;
            if (result != null)
            {
              output.WriteLine("Statistics for {0}: {1}{2}{3}", t.Name.ToString(), Environment.NewLine, result, Environment.NewLine);
            }
          }
        }
      }

#if false
      private void RunBenigni(
        string methodName,
        IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, ExternalExpression<APC, SymbolicValue>, SymbolicValue, ILogOptions> mdriver,
        GeneralOptions options)
      {
        var myoptions = new Analyzers.Bounds.BoundsOptions(options);

        var intervalGenerator = AnalysisWrapper.RunTheAnalysis(methodName, 
          Analyzers.DomainKind.Intervals, mdriver, myoptions);

        if (!System.IO.Directory.Exists(this.options.BENIGNI_DIR))
        {
          System.IO.Directory.CreateDirectory(this.options.BENIGNI_DIR);
        }
        
        TextWriter outFile = new StreamWriter(String.Format("{0}\\{1}.trs", this.options.BENIGNI_DIR, methodName));
    
        outFile.Close();
      }
#endif

      OnDemandMap<string, long> phaseTimes;
      readonly CustomStopwatch lastPhaseStart = new CustomStopwatch();
      
      string lastPhaseName;
      private ContractDensity ContractDensity;
      private CancellationToken cancellationToken;
      private int outputWarnings;

      private TimeSpan WriteLinePhase(IOutput output, string format, params string[] what)
      {
        var elapsed = lastPhaseStart.Elapsed;
        var phaseName =  String.Format(format, what);

        if (this.options.PrintPhaseStats || this.options.ShowPhases)
        {
          if (this.lastPhaseName != null)
          {
            long phaseTime;
            phaseTimes.TryGetValue(lastPhaseName, out phaseTime);
            phaseTimes[lastPhaseName] = phaseTime + elapsed.Ticks;
          }
          this.lastPhaseName = phaseName;

          if (this.options.ShowPhases)
          {
            var mem = this.GetMemory();
            output.WriteLine("   " + "  (elapsed: {0}, mem: {1}Mb)", elapsed.ToString(), mem.ToString());
            output.WriteLine("== {0}", this.lastPhaseName);
          }
        }

        this.clousotStateStatistics.CurrPhase = phaseName;

        lastPhaseStart.Restart();

        return elapsed;
      }

      private void PrintPhaseStats(IOutput output)
      {
        if (this.options.PrintPhaseStats)
        {
          var outputList = new SortedList<long, string>();
          
          // compute total time for percentages
          var total = Math.Max((double)phaseTimes.Values.Sum(), 0.0001); // so it's not 0
          foreach (var keyvalue in phaseTimes)
          {
            var message = string.Format("Phase {0,-46}{1,6:P1} {2}", keyvalue.Key, keyvalue.Value / total, TimeSpan.FromTicks(keyvalue.Value).ToString());
            outputList.Add(keyvalue.Value, message);
          }
          foreach (var message in outputList.Values)
          {
            output.WriteLine("{0}", message);
          }
        }
      }

      #region IDisposable Members

      public void Dispose()
      {
        this.Dispose(true);
      }

      protected virtual void Dispose(bool disposing)
      {
        if (disposing && this.cacheManager != null)
          this.cacheManager.Dispose();
      }

      #endregion

      ~TypeBinder()
      {
        this.Dispose(false);
      }

      internal class RevertedTimeSpanComparer : IComparer<TimeSpan>
      {
        public RevertedTimeSpanComparer()
        { }

        public int Compare(TimeSpan x, TimeSpan y)
        {
          if (x < y) return 1;
          if (x == y) return 0;
          return -1;
        }

      }

      public bool UseCache
      {
        get
        {
          return this.cacheManager != null &&
            (this.options.UseCache);
        }
      }

      public bool SaveToCache
      {
        get
        {
          return (this.options.SaveToCache || this.options.SaveSemanticBaseline) && this.cacheManager != null;
        }
      }

      private class SubMethods : IMethodCodeConsumer<Local, Parameter, Method, Field, Type, Unit, Unit>
      {
        protected readonly IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> md;
        private readonly IDecodeContracts<Local, Parameter, Method, Field, Type> cd;
        private ContractConsumer contractConsumer;
        private GeneralOptions options;

        Set<Method> methods = new Set<Method>();

        public static Set<Method> Gather(
          Method current,
          GeneralOptions options,
          IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> md,
          IDecodeContracts<Local, Parameter, Method, Field, Type> cd
        )
        {
          var sm = new SubMethods(md, cd, options);
          sm.VisitMethod(current);

          var result = sm.methods;
          result.Remove(current); // don't include starting point
          return result;
        }

        private SubMethods(
          IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> md,
          IDecodeContracts<Local, Parameter, Method, Field, Type> cd,
          GeneralOptions options
          )
        {
          this.md = md;
          this.cd = cd;
          this.options = options;
          this.contractConsumer = new ContractConsumer(this);
        }

        void VisitMethod(Method method)
        {
          if (!methods.Add(method)) return;

          if (this.md.HasBody(method))
          {
            this.md.AccessMethodBody(method, this, Unit.Value);

            if (this.cd.HasRequires(method))
              this.cd.AccessRequires<Method, Unit>(method, this.contractConsumer, method);

            if (this.cd.HasEnsures(method))
              this.cd.AccessEnsures<Method, Unit>(method, this.contractConsumer, method);

          }
        }

        class ContractConsumer : ICodeConsumer<Local, Parameter, Method, Field, Type, Method, Unit>
        {
          private SubMethods parent;

          public ContractConsumer(SubMethods sm)
          {
            this.parent = sm;
          }

          #region ICodeConsumer<Local,Parameter,Method,Field,Type,Method,Unit> Members
          Unit ICodeConsumer<Local, Parameter, Method, Field, Type, Method, Unit>.Accept<Label>(ICodeProvider<Label, Local, Parameter, Method, Field, Type> codeProvider, Label entryPoint, Method current)
          {
            Query<Label> query = new Query<Label>(codeProvider, parent, current);

            query.TraceSequentially(entryPoint);
            return Unit.Value;
          }


          #endregion
        }

        private class Query<Label> : MSILVisitor<Label, Local, Parameter, Method, Field, Type, Unit, Unit, Unit, Unit>, ICodeQuery<Label, Local, Parameter, Method, Field, Type, Unit, Unit>
        {
          private readonly ICodeProvider<Label, Local, Parameter, Method, Field, Type> codeProvider;
          private Method currentMethod;
          private SubMethods parent;

          public Query(
            ICodeProvider<Label, Local, Parameter, Method, Field, Type> codeProvider,
            SubMethods parent,
            Method currentMethod
          )
          {
            this.codeProvider = codeProvider;
            this.parent = parent;
            this.currentMethod = currentMethod;
          }

          protected IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> MD
          {
            get { return this.parent.md; }
          }

          public void TraceSequentially(Label current)
          {
            do
            {
              this.codeProvider.Decode<Query<Label>, Unit, Unit>(current, this, Unit.Value);
            } while (this.codeProvider.Next(current, out current));
          }

          #region ICodeQuery<Label,Type,Method,Unit,Unit> Members

          protected override Unit Default(Label pc, Unit data)
          {
            return data;
          }

          public Unit Aggregate(Label current, Label aggregateStart, bool canBeTargetOfBranch, Unit data)
          {
            this.TraceSequentially(aggregateStart);
            return data;
          }

          public override Unit Ldftn(Label pc, Method method, Unit dest, Unit data)
          {
            if (this.parent.options.AnalyzeClosures)
            {
              var unspec = MD.Unspecialized(method);
              if (IsRecCompilerGenerated(unspec))
              {
                this.parent.VisitMethod(unspec);
              }
            }
            return data;
          }

          private bool IsRecCompilerGenerated(Method unspec)
          {
            if (MD.IsCompilerGenerated(unspec)) return true;
            var parent = MD.DeclaringType(unspec);
            return IsRecCompilerGenerated(parent);
          }

          private bool IsRecCompilerGenerated(Type type)
          {
            if (MD.IsCompilerGenerated(type)) return true;
            Type parent;
            if (MD.IsNested(type, out parent))
            {
              return IsRecCompilerGenerated(parent);
            }
            return false;
          }

          public override Unit Newobj<ArgList>(Label pc, Method ctor, Unit dest, ArgList args, Unit data)
          {
            if (this.parent.options.AnalyzeMoveNext)
            {
              var dt = MD.DeclaringType(MD.Unspecialized(ctor));
              if (MD.IsCompilerGenerated(dt))
              {
                foreach (var item in MD.Methods(dt))
                {
                  if (MD.Name(item) == "MoveNext")
                  {
                    this.parent.VisitMethod(item);
                  }
                }
              }
            }
            return data;
          }
          #endregion
        }

        public Unit Accept<Label, Handler>(IMethodCodeProvider<Label, Local, Parameter, Method, Field, Type, Handler> codeProvider, Label entryPoint, Method method, Unit data)
        {
          var query = new Query<Label>(codeProvider, this, method);

          query.TraceSequentially(entryPoint);
          return Unit.Value;
        }
      }
    }

    struct MethodAnalysisFlags
    {
      public bool CheckInferredRequires; // this.options.CheckInferredRequires
      public bool CountInStats;
      public bool AllowReAnalysis;
      public bool RemoveInferredPreconditions;
      public bool ValidatingInferredPreconditions;

      public MethodAnalysisFlags(bool CheckInferredRequires = false, bool AllowReAnalysis = false)
      {
        this.CheckInferredRequires = CheckInferredRequires;

        this.CountInStats = true;
        this.AllowReAnalysis = AllowReAnalysis;
        this.RemoveInferredPreconditions = false;
        this.ValidatingInferredPreconditions = false;
      }

      public MethodAnalysisFlags Clone()
      {
        return this; // we are using the fact it is a struct
      }

      public MethodAnalysisFlags ForInferredPreconditionsValidation()
      {
        var flags = this.Clone();
        flags.CheckInferredRequires = false;
        flags.CountInStats = false;
        flags.AllowReAnalysis = true;
        flags.RemoveInferredPreconditions = true;
        flags.ValidatingInferredPreconditions = true;
        return flags;
      }

      public MethodAnalysisFlags ForConstructorReAnalysis()
      {
        var flags = this.Clone();
        flags.CountInStats = true;
        flags.AllowReAnalysis = true;
        return flags;
      }
    }

    class ExitTraceListener : System.Diagnostics.TraceListener
    {
      public override void Fail(string message)
      {
        Console.WriteLine("Debug assert failed: {0}", message);
        var trace = new System.Diagnostics.StackTrace(true);
        Console.WriteLine("{0}", trace);
        // MB: we cannot use ExitRequestedException here because the exception would be thrown from the debugger
        // and would not be caught in Main
        Environment.Exit(-1);
      }

      public override void WriteLine(string message)
      {
      }
      public override void Write(string message)
      {
      }
    }
  }
}