// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics.Contracts;
using Microsoft.Research.DataStructures;
using System.IO;

namespace Microsoft.Research.CodeAnalysis
{
    /// <summary>
    /// The manager for candidate assume
    /// </summary>
    [ContractClass(typeof(IAssumeDispatcherContracts))]
    public interface IAssumeDispatcher
    {
        /// <summary>
        /// Add the assume to the list of current assumptions
        /// </summary>
        void AddEntryAssumes(ProofObligation obl, IEnumerable<BoxedExpression> entryAssumes);

        /// <summary>
        /// Add the assume to the list of current assumptions
        /// Returns the warning contexts for the methods referring to the warning contexts
        /// </summary>
        IEnumerable<WarningContext> AddCalleeAssumes(ProofObligation obl, IEnumerable<IInferredCondition> calleeAssumes);

        /// <summary>
        /// Suggest the assumptions at entry point.
        /// Returns how many assumptions have been suggested
        /// </summary>
        int SuggestEntryAssumes();

        /// <summary>
        /// Suggest the assumptions for calles
        /// Returns how many assumptions have been suggested
        /// </summary>
        /// <returns></returns>
        int SuggestCalleeAssumes(bool suggestCalleeAssumes, bool suggestNecessaryEnsures, bool includedisjunctions = false);

        /// <summary>
        /// Save assumes for later caching/baselining
        /// </summary>
        int PropagateAssumes();
    }

    #region Contracts

    [ContractClassFor(typeof(IAssumeDispatcher))]
    internal abstract class IAssumeDispatcherContracts : IAssumeDispatcher
    {
        public void AddEntryAssumes(ProofObligation obl, IEnumerable<BoxedExpression> assumes)
        {
            Contract.Requires(obl != null);
            Contract.Requires(assumes != null);
        }

        public IEnumerable<WarningContext> AddCalleeAssumes(ProofObligation obl, IEnumerable<IInferredCondition> assumes)
        {
            Contract.Requires(obl != null);
            Contract.Requires(assumes != null);

            Contract.Ensures(Contract.Result<IEnumerable<WarningContext>>() != null);

            return null;
        }


        public int SuggestEntryAssumes()
        {
            Contract.Ensures(Contract.Result<int>() >= 0);

            return 0;
        }

        public void PropagateAssumes()
        {
        }

        int IAssumeDispatcher.PropagateAssumes()
        {
            Contract.Ensures(Contract.Result<int>() >= 0);

            throw new NotImplementedException();
        }

        public int SuggestCalleeAssumes(bool suggestCalleeAssumes, bool suggestNecessaryEnsures, bool includedisjunctions)
        {
            Contract.Requires(suggestCalleeAssumes || suggestNecessaryEnsures);
            Contract.Ensures(Contract.Result<int>() >= 0);

            return 0;
        }
    }

    #endregion

    [ContractVerification(true)]
    public class AssumeDispatcherProfiler
      : IAssumeDispatcher
    {
        #region Object Invariant

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(inner != null);
            Contract.Invariant(profilingAlreadyCollected.Length == 2);
        }

        #endregion

        #region Statics

        [ThreadStatic]
        static private Statistics entryAssumeStatistics = new Statistics();
        [ThreadStatic]
        static private Statistics calleeAssumeStatistics = new Statistics();

        #endregion

        #region State

        private readonly IAssumeDispatcher inner;
        private readonly bool[] profilingAlreadyCollected;

        #endregion

        #region Constructor

        public AssumeDispatcherProfiler(IAssumeDispatcher dispatcher)
        {
            Contract.Requires(dispatcher != null);

            inner = dispatcher;
            profilingAlreadyCollected = new bool[2];
        }

        #endregion

        #region Implementation of IPostCondtionDispatcher
        public void AddEntryAssumes(ProofObligation obl, IEnumerable<BoxedExpression> assumes)
        {
            entryAssumeStatistics.Generated += assumes.Count();
            inner.AddEntryAssumes(obl, assumes);
        }

        public IEnumerable<WarningContext> AddCalleeAssumes(ProofObligation obl, IEnumerable<IInferredCondition> assumes)
        {
            calleeAssumeStatistics.Generated += assumes.Count();
            return inner.AddCalleeAssumes(obl, assumes);
        }

        public int SuggestEntryAssumes()
        {
            return RecordProfilingInformation(inner.SuggestEntryAssumes(), 0, ref entryAssumeStatistics);
        }

        public int PropagateAssumes()
        {
            return RecordProfilingInformation(inner.PropagateAssumes(), 1, ref calleeAssumeStatistics);
        }

        public int SuggestCalleeAssumes(bool suggestCalleeAssumes, bool suggestNecessaryEnsures, bool includedisjunctions)
        {
            // We do not record the statistics, as we already do it when we propagate the assumptions
            return inner.SuggestCalleeAssumes(suggestCalleeAssumes, suggestNecessaryEnsures, includedisjunctions);
        }

        #endregion

        #region Dumping
        public static void DumpStatistics(IOutput output)
        {
            Contract.Requires(output != null);

            if (entryAssumeStatistics.Generated != 0)
            {
                output.WriteLine("Generated {0} entry assume(s) (suggested {1} after filtering)", entryAssumeStatistics.Generated, entryAssumeStatistics.Retained);
            }
            if (calleeAssumeStatistics.Generated != 0)
            {
                output.WriteLine("Generated {0} callee assume(s) ", calleeAssumeStatistics.Generated,
                  calleeAssumeStatistics.Retained > 0 ? string.Format("suggested {0} after filtering", calleeAssumeStatistics.Retained) : String.Empty);
            }
        }

        #endregion

        #region Profiling

        private int RecordProfilingInformation(int howMany, int who, ref Statistics statistics)
        {
            Contract.Requires(howMany >= 0);
            Contract.Ensures(statistics.Retained >= Contract.OldValue(statistics.Retained));
            Contract.Ensures(Contract.Result<int>() == howMany);

            if (!profilingAlreadyCollected[who])
            {
                statistics.Retained += howMany;
                profilingAlreadyCollected[who] = true;
            }

            return howMany;
        }

        #endregion




        private struct Statistics
        {
            public int Generated;
            public int Retained;
        }
    }
}
