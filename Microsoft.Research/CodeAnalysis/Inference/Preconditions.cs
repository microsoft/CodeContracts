// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics.Contracts;
using Microsoft.Research.DataStructures;
using System.IO;
using System.Diagnostics;

namespace Microsoft.Research.CodeAnalysis
{
    /// <summary>
    /// The precondition discharger
    /// </summary>
    public interface IPreconditionInference
    {
        bool ShouldAddAssumeFalse { get; }
        bool TryInferConditions(ProofObligation obl, ICodeFixesManager codefixesManager, out InferredConditions conditions);
    }

    /// <summary>
    /// The manager for candidate preconditions
    /// </summary>
    [ContractClass(typeof(IPreconditionDispatcherContracts))]
    public interface IPreconditionDispatcher
    {
        /// <summary>
        /// Add the preconditions to the list of current preconditions
        /// </summary>
        ProofOutcome AddPreconditions(ProofObligation obl, IEnumerable<BoxedExpression> preconditions, ProofOutcome originalOutcome);

        /// <summary>
        /// Returns the list of precondition for this method
        /// </summary>
        IEnumerable<KeyValuePair<BoxedExpression, IEnumerable<MinimalProofObligation>>> GeneratePreconditions();

        /// <summary>
        /// Suggest the precondition.
        /// Returns how many preconditions have been suggested
        /// </summary>
        int SuggestPreconditions(bool treatSuggestionsAsWarnings, bool forceSuggestionOutput);

        /// <summary>
        /// Infer the precondition.
        /// Returns how many preconditions have been propagated to the callers
        /// </summary>
        /// <param name="asPrecondition">if true, try to propagate as precondition, otherwise as assume</param>
        int PropagatePreconditions(bool asPrecondition);

        int NumberOfWarnings { get; }
    }

    public class PreconditionInferenceManager
    {
        [ContractInvariantMethod]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Required for code contracts.")]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.Inference != null);
            Contract.Invariant(this.Dispatch != null);
        }


        public readonly IPreconditionInference Inference;
        public readonly IPreconditionDispatcher Dispatch;

        static private PreconditionInferenceManager dummy; // Thread-safe
        static public PreconditionInferenceManager Dummy
        {
            get
            {
                if (dummy == null)
                {
                    dummy = new PreconditionInferenceManager(new DummyIPreconditionInference(), new DummyIPreconditionDispatcher());
                }
                return dummy;
            }
        }

        public PreconditionInferenceManager(IPreconditionInference Inference, IPreconditionDispatcher Dispatcher)
        {
            Contract.Requires(Inference != null);
            Contract.Requires(Dispatcher != null);

            this.Inference = Inference;
            this.Dispatch = Dispatcher;
        }

        #region Dummy implementations of the interfaces
        private class DummyIPreconditionInference : IPreconditionInference
        {
            public bool TryInferConditions(ProofObligation obl, ICodeFixesManager codefixesManager, out InferredConditions preConditions)
            {
                preConditions = null;
                return false;
            }

            public bool ShouldAddAssumeFalse
            {
                get { return false; }
            }
        }

        private class DummyIPreconditionDispatcher : IPreconditionDispatcher
        {
            public ProofOutcome AddPreconditions(ProofObligation obl, IEnumerable<BoxedExpression> preconditions, ProofOutcome originalOutcome)
            {
                return originalOutcome;
            }

            public IEnumerable<KeyValuePair<BoxedExpression, IEnumerable<MinimalProofObligation>>> GeneratePreconditions()
            {
                return Enumerable.Empty<KeyValuePair<BoxedExpression, IEnumerable<MinimalProofObligation>>>();
            }

            public int SuggestPreconditions(bool treatSuggestionsAsWarnings, bool forceSuggestionOutput)
            {
                return 0;
            }

            public int PropagatePreconditions(bool asPrecondition)
            {
                return 0;
            }

            public int NumberOfWarnings
            {
                get { return 0; }
            }
        }
        #endregion
    }

    #region Contracts for the interfaces

    [ContractClassFor(typeof(IPreconditionDispatcher))]
    internal abstract class IPreconditionDispatcherContracts
      : IPreconditionDispatcher
    {
        public ProofOutcome AddPreconditions(ProofObligation obl, IEnumerable<BoxedExpression> preconditions, ProofOutcome originalOutcome)
        {
            Contract.Requires(obl != null);
            Contract.Requires(preconditions != null);

            return ProofOutcome.Top;
        }

        public IEnumerable<KeyValuePair<BoxedExpression, IEnumerable<MinimalProofObligation>>> GeneratePreconditions()
        {
            Contract.Ensures(Contract.Result<IEnumerable<KeyValuePair<BoxedExpression, IEnumerable<MinimalProofObligation>>>>() != null);
            return null;
        }

        public int SuggestPreconditions(bool treatSuggestionsAsWarnings, bool forceSuggestionOutput)
        {
            Contract.Ensures(Contract.Result<int>() >= 0);

            return 0;
        }

        public int PropagatePreconditions(bool asPrecondition)
        {
            Contract.Ensures(Contract.Result<int>() >= 0);

            return 0;
        }

        public int NumberOfWarnings
        {
            get
            {
                Contract.Ensures(Contract.Result<int>() >= 0);
                return 0;
            }
        }
    }

    #endregion


    #region Aggressive (yet unsound) Precondition Inference: Try to read preconditions in the prestate
    public class AggressivePreconditionInference<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions>
      : IPreconditionInference
      where Variable : IEquatable<Variable>
      where Expression : IEquatable<Expression>
      where Type : IEquatable<Type>
      where LogOptions : IFrameworkLogOptions
    {
        #region Invariant

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.MethodDriver != null);
        }

        #endregion

        #region State

        protected readonly IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions> MethodDriver;

        #endregion

        #region Constructor

        public AggressivePreconditionInference(IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions> MethodDriver)
        {
            Contract.Requires(MethodDriver != null);

            this.MethodDriver = MethodDriver;
        }

        #endregion

        #region IPreconditionDischarger Members

        virtual public bool TryInferConditions(ProofObligation obl, ICodeFixesManager codefixesManager, out InferredConditions preConditions)
        {
            var preConds = PreconditionSuggestion.ExpressionsInPreState(obl.ConditionForPreconditionInference, this.MethodDriver.Context, this.MethodDriver.MetaDataDecoder, obl.PC, allowedKinds: ExpressionInPreStateKind.All);
            preConditions = /*preConds == null ? null : */ preConds.Where(pre => pre.hasVariables).AsInferredPreconditions(isSufficient: true);
            return /*preConditions != null && */ preConditions.Any();
        }

        #endregion

        #region Overridden

        public bool ShouldAddAssumeFalse
        {
            get { return false; }
        }

        public override string ToString()
        {
            return "Aggressive precondition inference";
        }
        #endregion

    }

    #endregion

    #region Profiler for precondition Inference

    [ContractVerification(true)]
    public class PreconditionInferenceProfiler
      : IPreconditionInference
    {
        #region state

        readonly private IPreconditionInference inner;
        [ThreadStatic]
        static private int inferred;
        [ThreadStatic]
        static private TimeSpan inferenceTime;

        [ThreadStatic]
        static private int totalMethodsWithPreconditons;
        [ThreadStatic]
        static private int totalMethodsWithNecessaryPreconditions;

        #endregion

        #region invariant

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(inner != null);
            Contract.Invariant(inferred >= 0);
            Contract.Invariant(totalMethodsWithPreconditons >= 0);
        }

        #endregion

        public PreconditionInferenceProfiler(IPreconditionInference inner)
        {
            Contract.Requires(inner != null);

            Contract.Assume(inferred >= 0, "it's a static variable: We believe at construction that other objects left it >= 0");

            this.inner = inner;

            Contract.Assume(totalMethodsWithPreconditons >= 0, "static invariant");
        }

        public bool TryInferConditions(ProofObligation obl, ICodeFixesManager codefixesManager, out InferredConditions preConditions)
        {
            var watch = new CustomStopwatch();
            watch.Start();
            var result = inner.TryInferConditions(obl, codefixesManager, out preConditions);
            watch.Stop();

            inferenceTime += watch.Elapsed;

            if (preConditions == null)
            {
                return false;
            }

            if (result) inferred += preConditions.Count();

            return result;
        }

        public bool ShouldAddAssumeFalse
        {
            get { return inner.ShouldAddAssumeFalse; }
        }

        static public void NotifyMethodWithAPrecondition()
        {
            totalMethodsWithPreconditons++;
        }

        static public void NotifyCheckInferredRequiresResult(uint tops)
        {
            totalMethodsWithNecessaryPreconditions += tops == 0 ? 1 : 0;
        }

        static public void DumpStatistics(IOutput output)
        {
            Contract.Requires(output != null);

            if (totalMethodsWithPreconditons > 0)
            {
                output.WriteLine("Methods with necessary preconditions: {0}", totalMethodsWithPreconditons);
                if (totalMethodsWithNecessaryPreconditions > 0)
                {
                    output.WriteLine("Methods where preconditions were also sufficient: {0}", totalMethodsWithNecessaryPreconditions);
                }
                if (inferred > 0)
                {
                    output.WriteLine("Discovered {0} new candidate preconditions in {1}", inferred, inferenceTime);
                }
            }
        }
    }

    #endregion

    #region Profiler for precondition Dispatching

    public class PreconditionDispatcherProfiler
      : IPreconditionDispatcher
    {
        #region Invariant

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(inner != null);
            Contract.Invariant(filtered >= 0);
        }

        #endregion

        #region State

        private readonly IPreconditionDispatcher inner;
        private bool statisticsAlreadyCollected;
        [ThreadStatic]
        private static int filtered;

        #endregion

        #region Constructor

        public PreconditionDispatcherProfiler(IPreconditionDispatcher dispatcher)
        {
            Contract.Requires(dispatcher != null);

            Contract.Assume(filtered >= 0, "it's a static variable: We believe at construction that other objects left it >= 0");

            inner = dispatcher;
            statisticsAlreadyCollected = false;
        }

        #endregion

        #region Statistics

        public static void DumpStatistics(IOutput output)
        {
            Contract.Requires(output != null);

            output.WriteLine("Retained {0} preconditions after filtering", filtered);
        }

        #endregion

        #region Implementations

        public ProofOutcome AddPreconditions(ProofObligation obl, IEnumerable<BoxedExpression> preconditions, ProofOutcome originalOutcome)
        {
            return inner.AddPreconditions(obl, preconditions, originalOutcome);
        }

        public IEnumerable<KeyValuePair<BoxedExpression, IEnumerable<MinimalProofObligation>>> GeneratePreconditions()
        {
            var result = inner.GeneratePreconditions();
            AddStatistics(result.Count());

            return result;
        }

        public int SuggestPreconditions(bool treatSuggestionsAsWarnings, bool forceSuggestionOutput)
        {
            return AddStatistics(inner.SuggestPreconditions(treatSuggestionsAsWarnings, forceSuggestionOutput));
        }

        public int PropagatePreconditions(bool asPrecondition)
        {
            return AddStatistics(inner.PropagatePreconditions(asPrecondition));
        }

        public int NumberOfWarnings
        {
            get { return inner.NumberOfWarnings; }
        }


        #endregion

        #region private methods

        private int AddStatistics(int howmany)
        {
            Contract.Requires(howmany >= 0);

            Contract.Ensures(Contract.Result<int>() == howmany);

            if (!statisticsAlreadyCollected)
            {
                filtered += howmany;
                statisticsAlreadyCollected = true;
            }

            return howmany;
        }

        private int AddStatisticsForObjectInvariants(int howmany)
        {
            // TODO: stats for object invariants

            return howmany;
        }
        #endregion
    }

    #endregion
}
