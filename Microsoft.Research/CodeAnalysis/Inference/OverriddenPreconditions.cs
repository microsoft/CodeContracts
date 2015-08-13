// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace Microsoft.Research.CodeAnalysis
{
    /// <summary>
    /// The manager for candidate postconditions
    /// </summary>
    [ContractClass(typeof(IOverriddenMethodPreconditionsDispatcherContracts))]
    public interface IOverriddenMethodPreconditionsDispatcher
    {
        /// <summary>
        /// Add the assume to the list of current assumptions
        /// </summary>
        void AddPotentialPreconditions(ProofObligation obl, IEnumerable<BoxedExpression> preconditions);

        /// <summary>
        /// Suggest the assumptions.
        /// Returns how many assumptions have been suggested
        /// </summary>
        int SuggestPotentialPreconditions(IOutput output);
    }

    #region Contracts

    [ContractClassFor(typeof(IOverriddenMethodPreconditionsDispatcher))]
    internal abstract class IOverriddenMethodPreconditionsDispatcherContracts : IOverriddenMethodPreconditionsDispatcher
    {
        public void AddPotentialPreconditions(ProofObligation obl, IEnumerable<BoxedExpression> assumes)
        {
            Contract.Requires(obl != null);
            Contract.Requires(assumes != null);
        }

        public int SuggestPotentialPreconditions(IOutput output)
        {
            Contract.Requires(output != null);
            Contract.Ensures(Contract.Result<int>() >= 0);

            return 0;
        }
    }

    #endregion

    [ContractVerification(true)]
    public class OverriddenMethodPreconditionsDispatcherProfiler
      : IOverriddenMethodPreconditionsDispatcher
    {
        #region Object Invariant

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(inner != null);
        }

        #endregion

        #region Statics

        [ThreadStatic]
        static private int generated;
        [ThreadStatic]
        static private int retained;

        #endregion

        #region State

        private readonly IOverriddenMethodPreconditionsDispatcher inner;
        private bool profilingAlreadyCollected;

        #endregion

        #region Constructor

        public OverriddenMethodPreconditionsDispatcherProfiler(IOverriddenMethodPreconditionsDispatcher dispatcher)
        {
            Contract.Requires(dispatcher != null);

            inner = dispatcher;
            profilingAlreadyCollected = false;
        }

        #endregion

        #region Implementation of IPostCondtionDispatcher
        public void AddPotentialPreconditions(ProofObligation obl, IEnumerable<BoxedExpression> assumes)
        {
            generated += assumes.Count();
            inner.AddPotentialPreconditions(obl, assumes);
        }

        public int SuggestPotentialPreconditions(IOutput output)
        {
            return RecordProfilingInformation(inner.SuggestPotentialPreconditions(output));
        }

        #endregion

        #region Dumping
        public static void DumpStatistics(IOutput output)
        {
            Contract.Requires(output != null);

            if (generated != 0)
            {
                output.WriteLine("Detected {0} preconditions for base methods to suggest", generated);
            }
        }

        #endregion

        #region Profiling

        private int RecordProfilingInformation(int howMany)
        {
            Contract.Requires(howMany >= 0);
            Contract.Ensures(retained >= Contract.OldValue(retained));
            Contract.Ensures(Contract.Result<int>() == howMany);

            if (!profilingAlreadyCollected)
            {
                retained += howMany;
                profilingAlreadyCollected = true;
            }

            return howMany;
        }

        #endregion
    }
}
