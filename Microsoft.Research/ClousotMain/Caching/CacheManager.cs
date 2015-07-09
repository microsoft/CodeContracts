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

//#define CACHE_TRACE
//#define CHECK_REDUNDANT_ENTRIES

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.Research.DataStructures;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using Microsoft.Research.CodeAnalysis.Caching;
using Microsoft.Research.Slicing;

// The caching system must be in clousotMain because it depends on Output !

namespace Microsoft.Research.CodeAnalysis
{
    using Provenance = IEnumerable<ProofObligation>;
    using System.Runtime.Serialization;
    using System.Threading;

    [ContractVerification(true)]
    internal class CacheManager<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, SymbolicValue> 
        : IDisposable where Type : IEquatable<Type>
    {
        // Abstraction over the particular SQL engine 
        private readonly IClousotCache cacheAccessor;

        // Where to write the results
        public IOutputFullResults<Method, Assembly> ReplayOutput { get; set; }

        // To get the options
        private IMethodDriver <Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, SymbolicValue, ILogOptions> mDriver;

        private IClassDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, SymbolicValue, ILogOptions, IMethodResult<SymbolicValue>> cDriver;

        private AnalysisDriver <Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, SymbolicValue, ILogOptions, IMethodResult<SymbolicValue>> driver;

        private FieldsDB<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> fieldsDB;

        private SharedPostConditionManagerInfo<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> globalPostconditions;

        private GeneralOptions options;

        private bool emitErrorOnCacheLookup
        {
            get { return this.options.EmitErrorOnCacheLookup; }
        }

        private bool SaveToCache
        {
            get { return this.options.SaveToCache || this.options.SaveSemanticBaseline; }
        }

        private readonly string VersionString;
        private readonly Lazy<byte[]> OptionsData;

        private Assembly currentAssembly;

        // Should we trace the cache behavior?
        private bool trace
        {
            get { return this.options.TraceCache; }
        }

        // Should we trace the hashing?
        private bool traceHashing
        {
            get { return this.options.TraceCacheHashing; }
        }

        // How many methods we read from the cache?
        private int nbCacheHits = 0;

        public int NbCacheHits
        {
            get { return this.nbCacheHits; }
        }

        // unique ID for a subroutine (used to put warnings into contexts)
        private BijectiveMap<Subroutine, int> subroutineLocalIds;

        // unique ID for types (used for general serialization of boxed expressions)
        private BijectiveMap<Type, int> referencedTypesLocalIds;

        // State
        private bool inAMethod = false;

        // Stats
        private int calleeAssumesRead = 0;
        private int entryAssumesRead = 0;
        private int calleeAssumesSaved = 0;
        private int entryAssumesSaved = 0;

        private int methodsWithBaseline = 0;
        private int methodsWithoutBaseline = 0;
        private int methodsIdenticalToBaseline = 0;

        // Number of filtered messages before the current method
        private SwallowedBuckets swallowedBefore;

        // Hash of the current methods
        private ByteArray currentHash, currentMethodId;

        private DateTime sliceTime;

        // The entry in the cache
        private Caching.Models.Method recordingMethodModel;
        private Caching.Models.AssemblyInfo assemblyInfo;
        private CancellationToken cancellationToken;

        public CacheManager(
            CancellationToken cancellationToken,
            IEnumerable<IClousotCacheFactory> cacheAccessorFactories,
            Dictionary<string, IMethodAnalysis> methodAnalyses,
            Dictionary<string, IClassAnalysis> classAnalyses,
            SharedPostConditionManagerInfo<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> globalPostconditions,
            GeneralOptions options)
        {
#if CACHE_TRACE
      var nbCounters = Enum.GetValues(typeof(Timer)).Length;
      this.timeCounters = new TimeCounter[nbCounters];
      this.indentation = new string[nbCounters+1];
      for (var i = 0; i < nbCounters; i++)
      {
        this.timeCounters[i] = new TimeCounter(true);
        this.indentation[i+1] = this.indentation[i] + ' ';
      }
#endif

            this.StartTimer(Timer.ctor);

            try
            {
                this.VersionString = options.ClousotVersion;
                this.OptionsData = new Lazy<byte[]>(() => OptionsHashing.GetOptionsData(methodAnalyses, classAnalyses, options));

                this.globalPostconditions = globalPostconditions;
                this.cancellationToken = cancellationToken;
                this.options = options;

                this.sliceTime = DateTime.FromBinary(options.sliceTime);

                if (this.trace)
                {
                    Console.WriteLine("[cache] Clousot version string: {0}", this.VersionString);
                    Console.WriteLine("[cache] Slice time stamp : {0}", this.sliceTime);
                }

                this.cacheAccessor = CreateCacheAccessorAsync(cacheAccessorFactories, options).Result;
            }
            finally
            {
                this.StopTimer(Timer.ctor);
            }
        }

        /// <summary>
        /// Creates <see cref="IClousotCache"/> in parallel and returns the first one that succeeds.
        /// </summary>
        /// <remarks>
        /// Resulting task will never fail and if the method would not be able to connect
        /// to the cache, result of the task would be null. 
        /// Implementation could be refined by adding cancellation.
        /// </remarks>
        /// 
        private Task<IClousotCache> CreateCacheAccessorAsync(
            IEnumerable<IClousotCacheFactory> cacheAccessorFactories, GeneralOptions options)
        {
            var tcs = new TaskCompletionSource<IClousotCache>();
            
            var factories = cacheAccessorFactories.ToList();

            var numberOfRemainingTasks = factories.Count;

            if (numberOfRemainingTasks == 0)
            {
                // Factory sequence is empty, can't connect to cache.
                tcs.TrySetResult(null);
                return tcs.Task;
            }

            // Need to materialize all factories first.
            foreach (var factory in factories)
            {
                var task = factory.CreateAndCheckAsync(options);

                task.ContinueWith(t =>
                {
                    var remainingTaskCount = Interlocked.Decrement(ref numberOfRemainingTasks);

                    if (t.Status == TaskStatus.RanToCompletion)
                    {
                        // CreateAndCheckAsync can return null. We should skip this result in this case!
                        if (t.Result != null)
                        {
                            tcs.TrySetResult(t.Result);
                        }
                    }
                    else if (t.Status == TaskStatus.Faulted)
                    {
                        //Observing an exception to avoid application crache.
                        var exception = t.Exception;
                        if (trace)
                        {
                            Console.WriteLine("Failed to connect to cache. Error: " + exception);
                        }
                    }

                    // Don't need to handle (potential) cancelled state.

                    // If remainingTaskCount is 0, then we were unable to connect to any cache!
                    if (remainingTaskCount == 0)
                    {
                        // Always using "Try"-methods, because result could be already set by another task continuation handler
                        tcs.TrySetResult(null);
                    }
                });
            }

            // Resutling task should be finished by the first successful factory call.
            return tcs.Task;
        }

#if false
    public bool TestCache()
    {
      var metaData = this.cacheAccessor.GetMetadataOrNull("Version");
      if (metaData == null || !metaData.Value.SequenceEqual(Encoding.UTF8.GetBytes(this.VersionString)))
      {
        return false;
      }
      return true;
    }
#endif

        // private helpers

        private int GetOrCreateSubroutineLocalId(Subroutine sub)
        {
            int res;
            if (!this.subroutineLocalIds.TryGetValue(sub, out res))
            {
                res = this.subroutineLocalIds.Count;
                this.subroutineLocalIds.Add(sub, res);
            }

            return res;
        }

        private int GetOrCreateReferencedTypeLocalId(Type typ)
        {
            int res;
            if (!this.referencedTypesLocalIds.TryGetValue(typ, out res))
            {
                res = this.referencedTypesLocalIds.Count;
                this.referencedTypesLocalIds.Add(typ, res);
            }

            return res;
        }

        public void SetAnalysisDriver(
            AnalysisDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, SymbolicValue, ILogOptions, IMethodResult<SymbolicValue>> driver)
        {
            Contract.Requires(driver != null);

            Contract.Assume(this.driver == null);

            this.driver = driver;
        }

        public void StartAssembly(Assembly assembly)
        {
            this.assemblyInfo = this.cacheAccessor.AddAssemblyInfo(
                this.driver.MetaDataDecoder.Name(assembly),
                this.driver.MetaDataDecoder.AssemblyGuid(assembly));

            this.currentAssembly = assembly;
        }

        public void EndAssembly()
        {
            this.currentAssembly = default(Assembly);
        }

        public string MethodHashAsString()
        {
            if (this.currentHash == null)
                return "";

            return this.currentHash.ToString();
        }

        public void StartMethod(
            IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, SymbolicValue, ILogOptions> mDriver,
            IClassDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, SymbolicValue, ILogOptions, IMethodResult<SymbolicValue>> cDriver,
            FieldsDB<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> fieldsDB,
            bool trace)
        {
            this.StartTimer(Timer.StartMethod);

            try
            {
                if (this.inAMethod)
                {
                    throw new InvalidOperationException();
                }

                this.inAMethod = true;

                this.swallowedBefore = new SwallowedBuckets(this.ReplayOutput.SwallowedMessagesCount);
                this.subroutineLocalIds = new BijectiveMap<Subroutine, int>();
                this.referencedTypesLocalIds = new BijectiveMap<Type, int>();

                this.cDriver = cDriver;
                this.mDriver = mDriver;
                this.fieldsDB = fieldsDB;

                ComputeMethodHash(mDriver, trace);

                if (this.SaveToCache)
                {
                    this.recordingMethodModel = this.cacheAccessor.NewMethodModel();
                    this.recordingMethodModel.Name = this.mDriver.MetaDataDecoder.Name(mDriver.CurrentMethod);
                    this.recordingMethodModel.FullName = this.mDriver.MetaDataDecoder.FullName(mDriver.CurrentMethod);
                }
            }
            finally
            {
                this.StopTimer(Timer.StartMethod);
            }
        }

        public void ComputeMethodHash(
            IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, SymbolicValue, ILogOptions> mDriver, bool trace)
        {
            var hashIsId = false;
            MethodHashAttribute mhAttr;
            if (mDriver.MetaDataDecoder.TryGetMethodHashAttribute(mDriver.CurrentMethod, out mhAttr))
            {
                hashIsId = mhAttr.Flags.HasFlag(MethodHashAttributeFlags.IdIsHash);
            }

            if (hashIsId)
            {
                this.currentHash = mhAttr.MethodId;
            }
            else
            {
                using (var hasher = new MethodHasher<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly,
                            IValueContext<Local, Parameter, Method, Field, Type, SymbolicValue>, Expression, SymbolicValue>(
                                this.driver, mDriver, trace, this.GetOrCreateSubroutineLocalId,this.GetOrCreateReferencedTypeLocalId))
                {
                    // Here we effectively do the hashing
                    this.currentHash = hasher.GetHash(this.OptionsData.Value);

                    if (this.trace)
                    {
                        Console.WriteLine("[cache] Computed hash : {0} ({1}) in hex) for method : {2}",
                            this.currentHash, this.currentHash.ToStringHex(),
                            this.driver.MetaDataDecoder.FullName(mDriver.CurrentMethod));
                    }
                }
            }

            if (mhAttr != null)
            {
                this.currentMethodId = mhAttr.MethodId;
            }
            else
            {
                this.currentMethodId = this.currentHash;
            }
        }

        // We abort if we cannot serialize some inferred pre/post or if we already found something in the cache, and we do not want to hash the replay
        public void AbortRecording(string reason)
        {
            this.StartTimer(Timer.AbortRecording);

            try
            {
                if (this.recordingMethodModel == null)
                {
                    return;
                }

                if (this.trace)
                {
                    Console.WriteLine("[cache] We won't save anything in the cache for this method. Reason: {0}", reason);
                }

                this.recordingMethodModel = null;
            }
            finally
            {
                this.StopTimer(Timer.AbortRecording);
            }
        }

        // Recording interface functions

        public void SaveOutcome(bool related, Witness witness, string format, params object[] args)
        {
            this.StartTimer(Timer.SaveOutcome);

            try
            {
                if (this.recordingMethodModel == null)
                {
                    return;
                }

                var model = this.cacheAccessor.AddNewOutcome(this.recordingMethodModel, String.Format(format, args), related); // The bi-directional link is automatically created here
                this.cacheAccessor.SetWitness(model, witness, this.subroutineLocalIds);
            }
            finally
            {
                this.StopTimer(Timer.SaveOutcome);
            }
        }

        public void SaveSuggestion(ClousotSuggestion.Kind type, string kind, APC pc, string suggestion, ClousotSuggestion.ExtraSuggestionInfo extraInfo)
        {
            this.StartTimer(Timer.SaveSuggestion);

            try
            {
                if (this.recordingMethodModel == null)
                {
                    return;
                }

                var model = this.cacheAccessor.AddNewSuggestion(this.recordingMethodModel, suggestion, kind, type, extraInfo); // The bi-directional link is automatically created here
                this.cacheAccessor.SetAPC(model, pc, this.subroutineLocalIds);
            }
            finally
            {
                this.StopTimer(Timer.SaveSuggestion);
            }
        }

        public void SaveTimeout()
        {
            this.StartTimer(Timer.SaveTimeout);

            try
            {
                if (this.recordingMethodModel == null)
                {
                    return;
                }

                this.recordingMethodModel.Timeout = true;
            }
            finally
            {
                this.StopTimer(Timer.SaveTimeout);
            }
        }

        public void SaveStatistics(AnalysisStatistics stats)
        {
            this.StartTimer(Timer.SaveStatistics);

            try
            {
                if (this.recordingMethodModel == null)
                {
                    return;
                }

                this.recordingMethodModel.Statistics = stats;
            }
            finally
            {
                this.StopTimer(Timer.SaveStatistics);
            }
        }

        public void SaveContractDensity(ContractDensity contractDensity)
        {
            this.StartTimer(Timer.SaveContractDensity);

            try
            {
                if (this.recordingMethodModel == null)
                {
                    return;
                }

                this.recordingMethodModel.ContractDensity = contractDensity;
            }
            finally
            {
                this.StopTimer(Timer.SaveContractDensity);
            }
        }

        public void SaveInferredContracts(
            IEnumerable<Pair<BoxedExpression, Provenance>> preConditions,
            IEnumerable<Pair<BoxedExpression, Provenance>> postConditions,
            IEnumerable<Pair<BoxedExpression, Provenance>> objectInvariants,
            IEnumerable<Pair<BoxedExpression, Provenance>> entryAssumes,
            IEnumerable<STuple<BoxedExpression, Provenance, Method>> calleeAssumes,
            IEnumerable<Tuple<Field, BoxedExpression>> nonNullFields,
            bool mayReturnNull,
            bool traceHashing)
        {
            Contract.Requires(preConditions != null);
            Contract.Requires(postConditions != null);
            Contract.Requires(objectInvariants != null);
            Contract.Requires(entryAssumes != null);
            Contract.Requires(calleeAssumes != null);
            Contract.Requires(nonNullFields != null);

            this.StartTimer(Timer.SaveInferredConditions);
            try
            {
                if (this.recordingMethodModel == null)
                {
                    return;
                }

                if (!preConditions.Any() && !postConditions.Any() && !objectInvariants.Any() && !entryAssumes.Any() &&
                    !calleeAssumes.Any()) // do not save if no inferred conditions
                {
                    return;
                }

                var inferredExpr = 
                    new InferredExpr<Field, Method>
                    {
                        PreConditions = preConditions.RemoveProvenance().ToArray(),
                        PostConditions = postConditions.RemoveProvenance().ToArray(),
                        ObjectInvariants = objectInvariants.RemoveProvenance().ToArray(),
                        NonNullFields =
                            nonNullFields.Select(tuple => new Pair<Field, BoxedExpression>(tuple.Item1, tuple.Item2))
                                .ToArray(),
                        EntryAssumes = entryAssumes.ToConditionList<Method>(),
                        CalleeAssumes = calleeAssumes.ToConditionList(),
                        MayReturnNull = mayReturnNull
                    };

                if (this.trace && inferredExpr.HasAssumes)
                {
                    Console.WriteLine("[cache] Saving {0} baseline assumes.", inferredExpr.AssumeCount);
                    foreach (var a in inferredExpr.EntryAssumes)
                    {
                        Console.WriteLine("[cache]   {0}", a.ToString());
                    }

                    foreach (var a in inferredExpr.CalleeAssumes)
                    {
                        Console.WriteLine("[cache]   {0}", a.ToString());
                    }
                }

                if (this.trace)
                {
                    foreach (var a in inferredExpr.PreConditions)
                    {
                        Console.WriteLine("[cache] inferred pre:  {0}", a.One.ToString());
                    }

                    foreach (var a in inferredExpr.PostConditions)
                    {
                        Console.WriteLine("[cache] inferred post:  {0}", a.One.ToString());
                    }
                    
                    foreach (var a in inferredExpr.ObjectInvariants)
                    {
                        Console.WriteLine("[cache] inferred inv:  {0}", a.One.ToString());
                    }
                    
                    foreach (var a in inferredExpr.NonNullFields)
                    {
                        Console.WriteLine("[cache] non-null field:  {0}", a.One.ToString());
                    }

                    Console.WriteLine("[cache] MayReturnNull? {0}", mayReturnNull);
                }

                this.recordingMethodModel.SetInferredExpr(inferredExpr, this.mDriver.CurrentMethod,
                    this.mDriver.MetaDataDecoder, this.mDriver.RawLayer.ContractDecoder, trace, traceHashing);

                if (this.options.SaveSemanticBaseline)
                {
                    this.calleeAssumesSaved += inferredExpr.CalleeAssumes.Count();
                    this.entryAssumesSaved += inferredExpr.EntryAssumes.Count();
                }
            }
            catch (SerializationException)
            {
                this.AbortRecording("Some inferred pre/post conditions cannot be serialized");
            }
            finally
            {
                this.StopTimer(Timer.SaveInferredConditions);
            }
        }

        public void SavePureParametersMask(long mask)
        {
            this.StartTimer(Timer.SavePureParametersMask);
            try
            {
                if (this.recordingMethodModel == null)
                {
                    return;
                }

                this.recordingMethodModel.PureParametersMask = mask;
            }
            finally
            {
                this.StopTimer(Timer.SavePureParametersMask);
            }
        }

        // Replay functions

        /// <summary>
        /// We couldn't match method with a hash in the cache; see if we can match its name. If we can, install assumes to suppress warnings from previous run(s)
        /// 
        /// Also, if method is identical to baseline and we skip identical methods, it installs inferred pre/post/inv conditions.
        /// </summary>
        /// <param name="method">Method currently under analysis</param>
        /// <returns></returns>
        public bool TryInstallAssumesFromBaselining(
            IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, SymbolicValue, ILogOptions> mDriver,
            Method method, bool trace, ref MethodClassification methodKind)
        {
            this.StartTimer(Timer.ReadAndInstallAssumesFromBaselining);

            this.mDriver = mDriver;
            try
            {
                Caching.Models.Method methodModel;

                // look up method by name
                if (this.cacheAccessor.TryGetBaselineModel(mDriver.MetaDataDecoder.FullName(method),
                    options.SemanticBaselineReadName, out methodModel))
                {
                    if (this.trace)
                    {
                        Console.WriteLine("[cache] Found baseline for {0}", mDriver.MetaDataDecoder.FullName(method));
                    }

                    methodKind = MethodClassification.ChangedMethod;
                    this.methodsWithBaseline++;

                    InferredExpr<Field, Method> inferredExpr = DeserializeInferredConditions(method, methodModel);

                    if (methodModel.Hash.ContentEquals(this.currentHash.Bytes))
                    {
                        if (this.trace)
                        {
                            Console.WriteLine("[cache] Baseline is identical to current method");
                        }

                        methodKind = MethodClassification.IdenticalMethod;
                        this.methodsIdenticalToBaseline++;

                        if (options.SkipIdenticalMethods)
                        {
                            var stats = new AnalysisStatistics();

                            // in this case, suppress the entire analysis of this method, but install inferred conditions
                            this.InstallPreconditions(inferredExpr, method, stats);
                            this.InstallPostconditions(inferredExpr, method);
                            this.InstallNonNullFields(inferredExpr, method);

                            var extraObjectInvariants =
                                this.globalPostconditions.SuggestNonNullObjectInvariantsFromConstructorsForward(mDriver.MetaDataDecoder.DeclaringType(method), false);

                            this.InstallObjectInvariants(inferredExpr, extraObjectInvariants, method, ref stats);

                            // Replay inferred pure parameters

                            var methodName = this.mDriver.MetaDataDecoder.FullName(method);
                            IDecodeMetadataExtensionsForPurityInfo.SetPureParametersMask(methodName, methodModel.PureParametersMask);

                            return true;
                        }
                    }

                    if (this.trace)
                    {
                        Console.WriteLine("[cache] Baseline differs from current method");
                    }

                    if (inferredExpr.HasAssumes && !options.ignoreBaselineAssumptions)
                    {
                        if (this.emitErrorOnCacheLookup) // Only for the regression test
                        {
                            this.ReplayOutput.EmitError(new System.CodeDom.Compiler.CompilerError(null, 0, 0, "",
                                "Assumes have been found in the cache."));
                        }

                        this.InstallAssumes(inferredExpr, method);

                        // recompute the method hash after installing
                        this.ComputeMethodHash(mDriver, trace);
                    }

                    return true;
                }
                else
                {
                    this.methodsWithoutBaseline++;
                    return false;
                }
            }
            catch (System.Data.EntityCommandExecutionException)
            {
                if (this.trace)
                {
                    Console.WriteLine("[cache] Please update cccheck version number");
                }

                if (this.emitErrorOnCacheLookup) // Only for regression tests
                {
                    this.ReplayOutput.EmitError(new System.CodeDom.Compiler.CompilerError(null, 0, 0, "",
                        "Please update cccheck version number"));
                }

                this.methodsWithoutBaseline++;
                
                return false;
            }
            finally
            {
                this.StopTimer(Timer.ReadAndInstallAssumesFromBaselining);
            }
        }

        private InferredExpr<Field, Method> DeserializeInferredConditions(Method method, Caching.Models.Method methodModel)
        {
            // Check that assumes can be correctly deserialized

            var inferredExpr = new InferredExpr<Field, Method>();

            this.StartTimer(Timer.GetExpression);
            try
            {
                inferredExpr = methodModel.GetInferredExpr(method, this.mDriver.MetaDataDecoder, this.mDriver.RawLayer.ContractDecoder, traceHashing);
                if (this.trace && inferredExpr.HasAssumes)
                {
                    Console.WriteLine("[cache] Found {0} baseline assumes.", inferredExpr.AssumeCount);
                    foreach (var a in inferredExpr.EntryAssumes)
                    {
                        Console.WriteLine("[cache]   {0}", a.ToString());
                    }

                    foreach (var a in inferredExpr.CalleeAssumes)
                    {
                        Console.WriteLine("[cache]   {0}", a.ToString());
                    }
                }
            }
            catch (SerializationException e)
            {
                if (this.trace)
                {
                    Console.WriteLine("[cache] Deserialization of some inferred conditions failed: {0}", e.Message);
                }

                if (this.emitErrorOnCacheLookup) // Only for regression tests
                {
                    this.ReplayOutput.EmitError(new System.CodeDom.Compiler.CompilerError(null, 0, 0, "",
                        "A baseline has been found but deserialization failed: " + e.Message));
                }
            }
            finally
            {
                this.StopTimer(Timer.GetExpression);
            }

            return inferredExpr;
        }

        private void InstallPreconditions(InferredExpr<Field, Method> inferredExpr, Method method, AnalysisStatistics stats)
        {
            // Puts the inferred preconditions at their place

            if (inferredExpr.PreConditions != null && inferredExpr.PreConditions.Any())
            {
                this.StartTimer(Timer.InstallPreConditions);
                try
                {
                    var methodName = this.mDriver.MetaDataDecoder.Name(method);
                    var preConditions = inferredExpr.PreConditions
                        .Select(p =>
                        {
                            var exp = p.One;

                            var expString =
                                exp.ToString<Type>(
                                    type => OutputPrettyCS.TypeHelper.TypeFullName(this.mDriver.MetaDataDecoder, type));
                            
                            return new BoxedExpression.AssumeExpression(exp, "requires", this.mDriver.CFG.Entry,
                                p.Two ?? 
                                new List<ProofObligation>()
                                {
                                    new FakeProofObligationForAssertionFromTheCache(p.One, methodName, method)
                                },
                                expString);
                        })
                        // We use  trick or returning a non-empty proof obligation to signal that the assertion has been inferred, even if we lost the context
                        .ToList();

                    this.driver.InstallPreconditions(preConditions, method);

                    stats.InferredPreconditions = preConditions.Count;
                }
                finally
                {
                    this.StopTimer(Timer.InstallPreConditions);
                }

                PreconditionInferenceProfiler.NotifyMethodWithAPrecondition();
            }
        }

        private void InstallPostconditions(InferredExpr<Field, Method> inferredExpr, Method method)
        {
            if (inferredExpr.PostConditions != null && inferredExpr.PostConditions.Any())
            {
                this.StartTimer(Timer.InstallPostConditions);
                try
                {
                    var methodName = this.mDriver.MetaDataDecoder.Name(method);

                    var postConditions = inferredExpr
                        .PostConditions
                        .Select(p =>
                        {
                            var exp = p.One;
                            var expString =
                                exp.ToString<Type>(type => OutputPrettyCS.TypeHelper.TypeFullName(this.mDriver.MetaDataDecoder, type));
                            
                            return new BoxedExpression.AssertExpression(exp, "ensures", this.mDriver.CFG.NormalExit
                                /* is it the right APC? */,
                                p.Two ??
                                new List<ProofObligation>()
                                {
                                    new FakeProofObligationForAssertionFromTheCache(p.One, methodName, method)
                                },
                                expString);
                        })
                        // We use  trick or returning a non-empty proof obligation to signal that the assertion has been inferred, even if we lost the context
                        .ToList();

                    this.driver.InstallPostconditions(postConditions, method);

                    if (this.options.PropagateInferredEnsuresForProperties)
                    {
                        var candidates = inferredExpr.PostConditions.Select(p => p.One);
                        
                        foreach (var tuple in SimplePostconditionDispatcher.GetCandidatePostconditionsForAutoProperties(candidates,
                                    this.driver.MetaDataDecoder, this.fieldsDB).GroupBy(t => t.Item1))
                        {
                            if (!tuple.Any()) continue;
                            this.driver.InstallPostconditions(tuple.Select(t => t.Item2).ToList(), tuple.First().Item1);
                        }
                    }
                }
                finally
                {
                    this.StopTimer(Timer.InstallPostConditions);
                }
            }
        }

        private void InstallNonNullFields(InferredExpr<Field, Method> inferredExpr, Method method)
        {
            if (inferredExpr.NonNullFields != null && inferredExpr.NonNullFields.Any())
            {
                this.StartTimer(Timer.InstallPostConditions);
                try
                {
                    this.globalPostconditions.AddNonNullFields(method,
                        inferredExpr.NonNullFields.Select(pair => new Tuple<Field, BoxedExpression>(pair.One, pair.Two)));
                }
                finally
                {
                    this.StopTimer(Timer.InstallPostConditions);
                }
            }
        }

        /// <summary>
        /// Install the assumes as postconditions for the calls in the body of <code>method</code>
        /// </summary>
        /// <param name="inferredExpr"></param>
        /// <param name="method">Is the method under analysis</param>
        private void InstallAssumes(InferredExpr<Field, Method> inferredExpr, Method method)
        {
            this.StartTimer(Timer.InstallAssumesFromBaseLining);
            try
            {
                this.calleeAssumesRead += inferredExpr.CalleeAssumes.Count();

                this.driver.InstallCalleeAssumes(this.mDriver.CFG, inferredExpr.CalleeAssumes, method);
                this.entryAssumesRead += inferredExpr.CalleeAssumes.Count();

                this.driver.InstallEntryAssumes(this.mDriver.CFG, inferredExpr.EntryAssumes, method);
            }
            finally
            {
                this.StopTimer(Timer.InstallAssumesFromBaseLining);
            }

            if (this.trace && false)
            {
                this.mDriver.CFG.Print(Console.Out, this.mDriver.RawLayer.Printer, null, null, null);
            }
        }

        // It's important stats is passed by ref, because we need to rememeber the # of inferred invariants in the case we should reschedule some analysis
        [ContractOption("cccheck.exe", "warninglevel", "full")]
        private void InstallObjectInvariants(InferredExpr<Field, Method> inferredExpr,
            IEnumerable<BoxedExpression> extraObjectInvariants, Method method, ref AnalysisStatistics stats)
        {
            if (this.options.disableForwardObjectInvariantInference)
            {
                extraObjectInvariants = null;
            }

            if (this.cDriver == null)
            {
                if (this.options.TraceCache)
                {
                    Console.WriteLine("[cache] We cannot install the object invariants as the class driver is null");
                }

                return;
            }

            if (this.driver == null)
            {
                if (this.options.TraceCache)
                {
                    Console.WriteLine("[cache] We cannot install the object invariants as the analysis driver is not set (it's null)");
                }

                return;
            }

            if (this.mDriver == null)
            {
                if (this.options.TraceCache)
                {
                    Console.WriteLine("[cache] We cannot install the object invariants as the method driver is not set (it's null)");
                }

                return;
            }

            var expressions = Concatenate(inferredExpr.ObjectInvariants, extraObjectInvariants);
            
            Contract.Assert(expressions != null, "Inferred postcondition");

            if (expressions.Any())
            {
                this.StartTimer(Timer.InstallInvariantsAsConstructorPostconditions);
                try
                {
                    Contract.Assert(this.cDriver != null);
                    Contract.Assert(this.mDriver != null);

                    var methodThis = this.mDriver.MetaDataDecoder.This(method);

                    this.cDriver.InstallInvariantsAsConstructorPostconditions(methodThis, expressions, method);

                    var asAssume = expressions
                        .Select(
                            be =>
                                (new BoxedExpression.AssumeExpression(be.One, "invariant",
                                    this.mDriver.CFG.EntryAfterRequires, be.Two,
                                    be.One.ToString<Type>(
                                        type =>
                                            OutputPrettyCS.TypeHelper.TypeFullName(this.driver.MetaDataDecoder, type)))))
                        .Cast<BoxedExpression>();

                    this.driver.InstallObjectInvariants(asAssume.ToList(), this.cDriver.ClassType);

                    stats.InferredInvariants = expressions.Count();
                    // we know it's a list, so this call is constant time
                }
                finally
                {
                    this.StopTimer(Timer.InstallInvariantsAsConstructorPostconditions);
                }
            }
        }

        [Pure]
        private IEnumerable<Pair<BoxedExpression, Provenance>> Concatenate(
            Pair<BoxedExpression, Provenance>[] necessaryObjectInvariants,
            IEnumerable<BoxedExpression> forwardObjectInvariants)
        {
            var result = new List<Pair<BoxedExpression, Provenance>>();

            if (necessaryObjectInvariants != null && necessaryObjectInvariants.Length > 0)
            {
                result.AddRange(necessaryObjectInvariants);
            }

            if (forwardObjectInvariants != null && forwardObjectInvariants.Any())
            {
                result.AddRange(forwardObjectInvariants.Select(be => new Pair<BoxedExpression, Provenance>(be, null)));
            }

            return result;
        }

        /// <summary>
        /// If the method is in the cache, then "pretend" the cached result
        /// </summary>
        public bool TryReplayAnalysis(Method method, bool trace, out AnalysisStatistics stats,
            out ContractDensity contractDensity, out ByteArray hash)
        {
            // Why method is a parameter while we have fields about the "current method"?

            /*
        if (true)
        {
            stats = default(AnalysisStatistics);
            contractDensity = default(ContractDensity);
            return false; // TMP!
        }
         */
            this.StartTimer(Timer.TryReplayAnalysis);
            try
            {
                if (this.TryReplayAnalysisInternal(method, trace, out stats, out contractDensity, out hash))
                {
                    if (this.trace)
                    {
                        Console.WriteLine("[cache] replay: ok");
                    }

                    return true;
                }
                
                if (mDriver.MetaDataDecoder.GetMethodHashAttributeFlags(method)
                        .HasFlag(MethodHashAttributeFlags.OnlyFromCache))
                {
                    this.AbortRecording("Method has OnlyFromCache flag");

                    if (this.trace)
                    {
                        Console.WriteLine("[cache] replay ok -- we pretend success");
                    }

                    return true; // Fake cache success
                }

                if (this.trace)
                {
                    Console.WriteLine("[cache] replay NOT OK");
                }

                return false;
            }
            finally
            {
                this.StopTimer(Timer.TryReplayAnalysis);
            }
        }

        private bool TryReplayAnalysisInternal(Method method, bool trace, out AnalysisStatistics stats,
            out ContractDensity contractDensity, out ByteArray hash)
        {
            hash = null;
            try
            {
                var mhAttrFlags = mDriver.MetaDataDecoder.GetMethodHashAttributeFlags(method);

                if (this.trace)
                {
                    Console.WriteLine("[cache] Searching for an entry in the cache...");
                }

                ByteArray hashToRead = null;

                if (mhAttrFlags.HasFlag(MethodHashAttributeFlags.Reuse))
                {
                    hashToRead = this.cacheAccessor.GetHashForDate(this.currentMethodId, this.sliceTime, true);
                }
                
                // f: Here we pass true, because we want the latest method after the slicing
                if (hashToRead == null)
                {
                    hashToRead = this.currentHash;
                }

                Caching.Models.Method methodModel;
                hash = hashToRead;
                if (!this.cacheAccessor.TryGetMethodModelForHash(hashToRead, out methodModel))
                {
                    if (this.trace)
                    {
                        Console.WriteLine("[cache] No entry found in the cache");
                    }

                    if (this.emitErrorOnCacheLookup) // Only for the regression test
                    {
                        this.ReplayOutput.EmitError(new System.CodeDom.Compiler.CompilerError(null, 0, 0, "",
                            "No entry found in the cache"));
                    }

                    stats = default(AnalysisStatistics);
                    contractDensity = default(ContractDensity);

                    return false;
                }

                // Check if all inferred pre/post conditions can be correctly deserialized

                InferredExpr<Field, Method> inferredExpr;
                
                this.StartTimer(Timer.GetExpression);

                try
                {
                    inferredExpr = methodModel.GetInferredExpr(
                        method, this.mDriver.MetaDataDecoder,
                        this.mDriver.RawLayer.ContractDecoder, traceHashing);

                    if (this.trace)
                    {
                        Console.WriteLine("[cache] Inferred expressions: {0}", inferredExpr);
                    }
                }
                catch (SerializationException e)
                {
                    if (this.trace)
                    {
                        Console.WriteLine("[cache] Deserialization of some inferred pre/post condition failed: {0}",
                            e.Message);
                    }

                    if (this.emitErrorOnCacheLookup) // Only for regression tests
                    {
                        this.ReplayOutput.EmitError(new System.CodeDom.Compiler.CompilerError(null, 0, 0, "",
                            "An entry has been found in the cache but deserialization failed: " + e.Message));
                    }

                    stats = default(AnalysisStatistics);
                    contractDensity = default(ContractDensity);

                    // F: I do not understand this comment, probably it was because of copy&paste
                    // No need to record, we found the data and everything can be correctly read!
                    //this.AbortRecording("Deserialization error");

                    this.ReplayOutput.WriteLine(
                        "Internal error: Method {0} was found in the cache but deserialization failed.",
                        this.mDriver.MetaDataDecoder.FullName(method));

                    this.ReplayOutput.WriteLine("The reason why the deserialization failed is {0}", e.Message);
                    this.ReplayOutput.WriteLine(
                        "We remove the incorrect entry from the cache and we replace it with a new one. If the error persists, please report the tool output to logozzo@microsoft.com");

                    this.cacheAccessor.DeleteMethodModel(methodModel);
                    this.cacheAccessor.SaveChanges(true); // force commit of changes

                    return false;
                }
                finally
                {
                    this.StopTimer(Timer.GetExpression);
                }

                this.nbCacheHits++;

                if (this.trace)
                {
                    Console.WriteLine("[cache] An entry has been found in the cache, replaying it...");
                }

                if (this.emitErrorOnCacheLookup) // Only for the regression test
                {
                    this.ReplayOutput.EmitError(new System.CodeDom.Compiler.CompilerError(null, 0, 0, "",
                        "An entry has been found in the cache"));
                }

                // No need to record, we found the data and everything can be correctly read!
                this.AbortRecording("The data has been read from the cache");

                ReplayMethodAnalysis(method, mhAttrFlags, methodModel, inferredExpr, out stats, out contractDensity);

                // Replay timeout, if any

                if (methodModel.Timeout)
                {
                    throw new TimeoutExceptionFixpointComputation();
                }

                return true;
            }
            catch (System.Data.EntityCommandExecutionException)
            {
                if (this.trace)
                {
                    Console.WriteLine("[cache] Please update cccheck version number");
                }

                if (this.emitErrorOnCacheLookup) // Only for regression tests
                {
                    this.ReplayOutput.EmitError(new System.CodeDom.Compiler.CompilerError(null, 0, 0, "",
                        "Please update cccheck version number"));
                }

                stats = default(AnalysisStatistics);
                contractDensity = default(ContractDensity);

                return false;
            }
        }

        private void ReplayMethodAnalysis(Method method, MethodHashAttributeFlags mhAttrFlags,
            Caching.Models.Method methodModel, InferredExpr<Field, Method> inferredExpr,
            out AnalysisStatistics stats, out ContractDensity contractDensity)
        {
            if (ReplayOutput == null)
            {
                if (this.trace)
                {
                    Console.WriteLine("[cache] The replay output object is not set. We skip the analysis replay");
                }

                stats = default(AnalysisStatistics);
                contractDensity = default(ContractDensity);

                return;
            }

            // record assembly binding
            if (this.assemblyInfo != null)
            {
                this.cacheAccessor.AddAssemblyBinding(methodModel, this.assemblyInfo);
            }

            stats = methodModel.Statistics;
            contractDensity = methodModel.ContractDensity;

            this.InstallPreconditions(inferredExpr, method, stats);
            this.InstallPostconditions(inferredExpr, method);
            this.InstallNonNullFields(inferredExpr, method);

            var extraObjectInvariants = 
                this.globalPostconditions.SuggestNonNullObjectInvariantsFromConstructorsForward(
                    mDriver.MetaDataDecoder.DeclaringType(method), false);

            this.InstallObjectInvariants(inferredExpr, extraObjectInvariants, method, ref stats);

            // Set the witnesses

            this.driver.MethodCache.AddWitness(method, inferredExpr.MayReturnNull);

            // Replay inferred pure parameters

            var methodName = this.mDriver.MetaDataDecoder.FullName(method);
            IDecodeMetadataExtensionsForPurityInfo.SetPureParametersMask(methodName, methodModel.PureParametersMask);

            if (mhAttrFlags.HasFlag(MethodHashAttributeFlags.ReplayOnlyInferredContracts))
            {
                stats = default(AnalysisStatistics);
                contractDensity = default(ContractDensity);
            }
            else
            {
                // Read and replay outcomes

                foreach (var outcomeModel in methodModel.Outcomes)
                {
                    Contract.Assume(outcomeModel != null);
                    Contract.Assume(this.ReplayOutput != null);

                    if (outcomeModel.Related)
                    {
                        this.ReplayOutput.EmitOutcomeAndRelated(outcomeModel.GetWitness(this.subroutineLocalIds), outcomeModel.Message);
                    }
                    else
                    {
                        this.ReplayOutput.EmitOutcome(outcomeModel.GetWitness(this.subroutineLocalIds),
                            outcomeModel.Message);
                    }
                }

                // Read and replay suggestions

                foreach (var suggestionModel in methodModel.Suggestions)
                {
                    Contract.Assume(Enum.IsDefined(typeof (ClousotSuggestion.Kind), suggestionModel.Type));
                    
                    this.ReplayOutput.Suggestion((ClousotSuggestion.Kind) suggestionModel.Type, suggestionModel.Kind,
                        suggestionModel.GetAPC(this.subroutineLocalIds), suggestionModel.Message, null,
                        ClousotSuggestion.ExtraSuggestionInfo.FromStream(suggestionModel.ExtraInfo));
                }
            }
        }

#if CHECK_REDUNDANT_ENTRIES
    [ThreadStatic]
    private static Dictionary<string, Method> savedMethods = new Dictionary<string, Method>();
#endif

        public void EndMethod(Method method)
        {
            this.StartTimer(Timer.EndMethod);
            try
            {
                if (this.cancellationToken.IsCancellationRequested)
                {
                    // don't save, could be partial
                    this.recordingMethodModel = null;
                }

                if (this.recordingMethodModel != null)
                {
                    if (this.trace)
                    {
                        Console.WriteLine("[cache] Saving data in the cache for current method\n");
                    }

#if CHECK_REDUNDANT_ENTRIES
          var key = this.currentHash.ToString();
          Method existing;
          if (savedMethods.TryGetValue(key, out existing))
          {
            Console.WriteLine("[cache] !!!! The hash {0} has already been used for method {1}",
              this.currentHash, this.mDriver.MetaDataDecoder.Name(existing));
          }
          else
          {
            savedMethods[key] = method;
          }
#endif
                    this.recordingMethodModel.Hash = this.currentHash.Bytes;
                    if (this.swallowedBefore != null)
                    {
                        var swallowedAfter = new SwallowedBuckets(this.ReplayOutput.SwallowedMessagesCount);
                        this.recordingMethodModel.Swallowed = swallowedAfter - this.swallowedBefore;
                    }

                    this.recordingMethodModel.Assemblies.Add(this.assemblyInfo);
                    if (this.options.SaveSemanticBaseline)
                    {
                        if (this.trace)
                        {
                            Console.WriteLine("[cache] Saving baseline for current method\n");
                        }

                        this.cacheAccessor.AddOrUpdateBaseline(this.recordingMethodModel.FullName,
                            this.options.SemanticBaselineSaveName, this.recordingMethodModel);
                    }

                    this.cacheAccessor.AddOrUpdateMethod(this.recordingMethodModel, this.currentMethodId);
                    this.cacheAccessor.SaveChanges();
                }

                // Reset all the fields
                this.recordingMethodModel = null;
                this.cDriver = null;
                this.mDriver = null;
                this.subroutineLocalIds = null;
                this.referencedTypesLocalIds = null;
                this.currentHash = null;
                this.inAMethod = false;
            }
            finally
            {
                this.StopTimer(Timer.EndMethod);
            }
        }

        public void TrySaveChanges(bool now = true)
        {
            this.StartTimer(Timer.TrySaveChanges);
            try
            {
                this.cacheAccessor.SaveChanges(now);
            }
            finally
            {
                this.StopTimer(Timer.TrySaveChanges);
            }
        }

        private bool IsValid
        {
            get { return this.cacheAccessor != null && this.cacheAccessor.IsValid; }
        }

        // Cache timings measurments

        private enum Timer
        {
            ctor,
            AbortRecording,
            ClearCache,
            EndMethod,
            GetExpression,
            InstallPreConditions,
            InstallPostConditions,
            SaveContractDensity,
            SaveInferredConditions,
            SaveOutcome,
            SavePureParametersMask,
            SaveStatistics,
            SaveSuggestion,
            SaveTimeout,
            StartMethod,
            TryReplayAnalysis,
            TrySaveChanges,
            InstallInvariantsAsConstructorPostconditions,
            ReadAndInstallAssumesFromBaselining,
            InstallAssumesFromBaseLining
        }

#if CACHE_TRACE
    private readonly TimeCounter[] timeCounters;
    private readonly string[] indentation;
    private int startNb = 0;
#endif

        [Conditional("CACHE_TRACE")]
        private void StartTimer(Timer method)
        {
#if CACHE_TRACE
      this.timeCounters[(int)method].Start();
      Console.WriteLine("---------> {0}CacheManager.{1}", this.indentation[this.startNb++], method);
#endif
        }

        [Conditional("CACHE_TRACE")]
        private void StopTimer(Timer method)
        {
#if CACHE_TRACE
      this.timeCounters[(int)method].Stop();
      Console.WriteLine("<--------- {0}CacheManager.{1} {2}", this.indentation[--this.startNb], method, this.timeCounters[(int)method].ToString());
#endif
        }

        // IDisposable Members

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected void Dispose(bool disposing)
        {
            if (disposing && this.cacheAccessor != null)
            {
                this.cacheAccessor.Dispose();
            }
        }

        ~CacheManager()
        {
            this.Dispose(false);
        }

        internal static CacheManager<
            Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly,
            ExternalExpression<APC, CodeAnalysis.SymbolicValue>, CodeAnalysis.SymbolicValue>
            Create
            (
                IEnumerable<IClousotCacheFactory> cacheAccessorFactories,
                Dictionary<string, IMethodAnalysis> dictionary,
                Dictionary<string, IClassAnalysis> dictionary_2,
                SharedPostConditionManagerInfo<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> globalPostconditions,
                GeneralOptions generalOptions,
                CancellationToken cancellationToken
            )
        {
            var result = 
                new CacheManager<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly,
                    ExternalExpression<APC, CodeAnalysis.SymbolicValue>, CodeAnalysis.SymbolicValue>(
                        cancellationToken, cacheAccessorFactories,
                        dictionary, dictionary_2, globalPostconditions, generalOptions);

            if (result.IsValid) return result;

            result.Dispose();
            
            return null;
        }

        public string CacheName
        {
            get { return this.cacheAccessor.CacheName; }
        }

        internal void DumpStatistics(IOutputFullResults<Method, Assembly> output, GeneralOptions options)
        {
            Contract.Requires(output != null);

            if (options.UseSemanticBaseline)
            {
                output.WriteLine("Baseline entry  assumes read:  {0}", this.entryAssumesRead);
                output.WriteLine("Baseline callee assumes read:  {0}", this.calleeAssumesRead);
                output.WriteLine("Methods with baseline:  {0}", this.methodsWithBaseline);
                output.WriteLine("Methods w/o  baseline:  {0}", this.methodsWithoutBaseline);
                output.WriteLine("Methods with identical baseline:  {0}", this.methodsIdenticalToBaseline);
            }

            if (options.SaveSemanticBaseline)
            {
                output.WriteLine("Baseline entry  assumes saved: {0}", this.entryAssumesSaved);
                output.WriteLine("Baseline callee assumes saved: {0}", this.calleeAssumesSaved);
            }
        }
    }

    // GetOptionsIncompatibilityReason

    internal static class CacheManager
    {
#if false
    [ThreadStatic]
    private static readonly Set<string> emitedIncompatibilityWarnings = new Set<string>();
#endif

        public static List<string> GetOptionsIncompatibilityReason(GeneralOptions options)
        {
            // TODO : What about outputPrettycs & outputPrettyfull ?
            var res = new List<string>();

            if (!options.UseCache)
                return res;

#if DEBUG
            if (options.trace.Except(new TraceOptions[] {TraceOptions.cache, TraceOptions.egraph, TraceOptions.heap}).Any())
            {
                res.Add("The -trace option(s) you have specified is(are) incompatible with the cache.");
            }

            if (options.show.Except(new ShowOptions[]
                {
                    ShowOptions.il,
                    ShowOptions.unreached, ShowOptions.validations, ShowOptions.errors,
                    ShowOptions.progress, ShowOptions.progressbar,
                    ShowOptions.warnranks
                }).Any())
            {
                res.Add("The -show option(s) you have specified is(are) incompatible with the cache.");
            }

            if (options.stats.Except(new StatOptions[]
                {
                    StatOptions.mem, StatOptions.time, StatOptions.perMethod, StatOptions.valid
                }).Any())
            {
                res.Add("The -stats option(s) you have specified is(are) incompatible with the cache.");
            }

            if (options.outputPrettycs)
            {
                res.Add("The -outputPrettycs option is incompatible with the cache."); // TODO : is it really ?
            }

            if (options.CheckInferredRequires)
            {
                res.Add("The -check inferredrequires option is incompatible with the cache.");
            }

#endif
#if false
      res = res.Except(emitedIncompatibilityWarnings).ToList();
      this.emitedIncompatibilityWarnings.AddRange(res);
#endif
            return res;
        }
    }

    // Hashing of options

    public static class OptionsHashing
    {
        public static byte[] GetOptionsData(
            Dictionary<string, IMethodAnalysis> methodAnalyses,
            Dictionary<string, IClassAnalysis> classAnalyses,
            GeneralOptions options)
        {
            var sb = new StringBuilder();

            sb.WriteOptionsString(options);
            sb.AppendFormat("PrintOutcome = {0} {1} {2} {3}\n",
                options.PrintOutcome(ProofOutcome.Bottom), options.PrintOutcome(ProofOutcome.False),
                options.PrintOutcome(ProofOutcome.Top), options.PrintOutcome(ProofOutcome.True));

            foreach (var classAnalysis in classAnalyses.Values)
            {
                sb.AppendLine("--------");
                var opts = classAnalysis.Options.ToList();
                sb.AppendFormat("{0} {1}\n", classAnalysis.Name, opts.Count());
                foreach (var opt in opts)
                    sb.AppendLine("---").WriteOptionsString(opt);
            }

            foreach (var methodAnalysis in methodAnalyses.Values)
            {
                sb.AppendLine("--------");
                
                var opts = methodAnalysis.Options.ToList();
                sb.AppendFormat("{0} {1}\n", methodAnalysis.Name, opts.Count());
                
                foreach (var opt in opts)
                    sb.AppendLine("---").WriteOptionsString(opt);
            }

            return Encoding.Unicode.GetBytes(sb.ToString());
        }

        private static string EscapeOptionsString(this string str)
        {
            return str.Replace(" ", "\\ ").Replace("\n", "\\n").Replace("\\", "\\\\");
        }

        private static StringBuilder WriteValue(this StringBuilder sb, object value)
        {
            if (value == null)
            {
                return sb.Append("\\null");
            }

            var type = value.GetType();

            if (type.IsPrimitive || type.IsEnum || type == typeof (string))
            {
                return sb.Append(value.ToString().EscapeOptionsString());
            }

            if (value is IEnumerable)
            {
                return ((IEnumerable) value).Cast<object>()
                    .Aggregate(sb, (sb2, elmt) => sb2.WriteValue(elmt).Append(' '));
            }

            throw new InvalidOperationException();
        }

        private static StringBuilder WriteOptionsString(this StringBuilder sb, OptionParsing options)
        {
            return options.GetType().GetFields(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public)
                .Where(field => !field.GetCustomAttributes(typeof (DoNotHashInCacheAttribute), true).Any())
                .Aggregate(sb, 
                    (sb2, field) => sb2.AppendFormat("{0} = ", field.Name.EscapeOptionsString())
                        .WriteValue(field.GetValue(options))
                        .AppendLine());
        }
    }

    // CachingOutput

    internal class CachingOutput<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression,
        SymbolicValue> : DerivableOutputFullResults<Method, Assembly>
        where Type : IEquatable<Type>
    {
        private CacheManager<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, SymbolicValue> cacheManager;

        internal CachingOutput(IOutputFullResults<Method, Assembly> underlying,
            CacheManager<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, SymbolicValue> cacheManager)
            : base(underlying)
        {
            this.cacheManager = cacheManager;
        }

        // IOutputFullResults<Method,Assembly> Members

        public override void StartAssembly(Assembly assembly)
        {
            this.cacheManager.StartAssembly(assembly);
            base.StartAssembly(assembly);
        }

        public override void EndAssembly()
        {
            this.cacheManager.EndAssembly();
            base.EndAssembly();
        }

        // IOutputResults Members

        public override bool EmitOutcome(Witness witness, string format, params object[] args)
        {
            this.cacheManager.SaveOutcome(false, witness, format, args);
            return base.EmitOutcome(witness, format, args);
        }

        public override bool EmitOutcomeAndRelated(Witness witness, string format, params object[] args)
        {
            this.cacheManager.SaveOutcome(true, witness, format, args);
            return base.EmitOutcomeAndRelated(witness, format, args);
        }

        // IOutput Members

        public override void Suggestion(ClousotSuggestion.Kind type, string kind, APC pc, string suggestion,
            List<uint> causes, ClousotSuggestion.ExtraSuggestionInfo extraInfo)
        {
            this.cacheManager.SaveSuggestion(type, kind, pc, suggestion, extraInfo);
            base.Suggestion(type, kind, pc, suggestion, causes, extraInfo);
        }
    }

    // SQLServer & SQLServerCompact CacheDataAccessorFactory

#if false
  public class SQLServerCacheDataAccessorFactory : ICacheDataAccessorFactory
  {
    private readonly GeneralOptions options;

    public SQLServerCacheDataAccessorFactory(GeneralOptions options)
    {
      this.options = options;
    }

    public ICacheDataAccessor Create(string DBName, Dictionary<string, byte[]> metadataIfCreation)
    {
      if (String.IsNullOrWhiteSpace(this.options.CacheServer))
        return null;

      var cacheVersionParameters = new CacheVersionParameters
      {
        Version = this.options.CacheVersion,
        SetBaseLine = this.options.SetCacheBaseLine,
      };

      return new SQLServerCacheDataAccessor(this.options.CacheServer, DBName, metadataIfCreation, this.options.CacheMaxSize, cacheVersionParameters);
    }
  }

  public class SQLServerCompactCacheDataAccessorFactory : ICacheDataAccessorFactory
  {
    private readonly GeneralOptions options;

    public SQLServerCompactCacheDataAccessorFactory(GeneralOptions options)
    {
      this.options = options;
    }

    public ICacheDataAccessor Create(string DBName, Dictionary<string, byte[]> metadataIfCreation)
    {
      var cacheVersionParameters = new CacheVersionParameters
      {
        Version = this.options.CacheVersion,
        SetBaseLine = this.options.SetCacheBaseLine,
      };

      return new SQLServerCompactCacheDataAccessor(this.options.cacheDirectory, DBName, metadataIfCreation, this.options.CacheMaxSize, cacheVersionParameters);
    }
  }
#endif
}
