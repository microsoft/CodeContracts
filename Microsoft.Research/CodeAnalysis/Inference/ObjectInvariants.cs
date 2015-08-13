// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace Microsoft.Research.CodeAnalysis
{
    [ContractClass(typeof(IObjectInvarariantDispatcherContracts))]
    public interface IObjectInvariantDispatcher
    {
        /// <summary>
        /// Add the object invariants to the list of current object invariants
        /// </summary>
        ProofOutcome AddObjectInvariants(ProofObligation obl, IEnumerable<BoxedExpression> objectInvariants, ProofOutcome originalOutcome);

        /// <summary>
        /// Returns the list of object invariants for this method
        /// </summary>
        IEnumerable<KeyValuePair<BoxedExpression, IEnumerable<MinimalProofObligation>>> GenerateObjectInvariants();

        /// <summary>
        /// Suggest the object invariants.
        /// Returns how many object invariants have been suggested
        /// </summary>
        int SuggestObjectInvariants();

        /// <summary>
        /// Infer the object invariants.
        /// Returns how many object invariants have been propagated to the callers
        /// </summary>
        /// <param name="asInvariant">if true, install as an invariant, otherwise as an assume</param>
        int PropagateObjectInvariants(bool asInvariant);
    }

    #region Contracts

    [ContractClassFor(typeof(IObjectInvariantDispatcher))]
    internal abstract class IObjectInvarariantDispatcherContracts : IObjectInvariantDispatcher
    {
        public ProofOutcome AddObjectInvariants(ProofObligation obl, IEnumerable<BoxedExpression> objectInvariants, ProofOutcome originalOutcome)
        {
            Contract.Requires(obl != null);
            Contract.Requires(objectInvariants != null);

            return ProofOutcome.Top;
        }

        public IEnumerable<KeyValuePair<BoxedExpression, IEnumerable<MinimalProofObligation>>> GenerateObjectInvariants()
        {
            Contract.Ensures(Contract.Result<IEnumerable<KeyValuePair<BoxedExpression, IEnumerable<MinimalProofObligation>>>>() != null);

            return null;
        }

        public int SuggestObjectInvariants()
        {
            Contract.Ensures(Contract.Result<int>() >= 0);

            return 0;
        }

        public int PropagateObjectInvariants(bool asInvariant)
        {
            Contract.Ensures(Contract.Result<int>() >= 0);

            return 0;
        }
    }

    #endregion

    public class ObjectInvariantDispatcherProfiler : IObjectInvariantDispatcher
    {
        #region statics

        [ThreadStatic]
        private static int retained;
        [ThreadStatic]
        private static int generated;

        #endregion

        #region Private state

        private readonly IObjectInvariantDispatcher inner;
        private bool statsEmitted;

        #endregion

        #region Constructor

        public ObjectInvariantDispatcherProfiler(IObjectInvariantDispatcher inner)
        {
            Contract.Requires(inner != null);

            this.inner = inner;
            statsEmitted = false;
        }

        #endregion

        #region Implementation
        public ProofOutcome AddObjectInvariants(ProofObligation obl, IEnumerable<BoxedExpression> objectInvariants, ProofOutcome originalOutcome)
        {
            generated += objectInvariants.Count();
            return inner.AddObjectInvariants(obl, objectInvariants, originalOutcome);
        }

        public IEnumerable<KeyValuePair<BoxedExpression, IEnumerable<MinimalProofObligation>>> GenerateObjectInvariants()
        {
            var result = inner.GenerateObjectInvariants();
            AddStatisticsForObjectInvariants(result.Count());

            return result;
        }

        public int SuggestObjectInvariants()
        {
            return AddStatisticsForObjectInvariants(inner.SuggestObjectInvariants());
        }

        public int PropagateObjectInvariants(bool asInvariant)
        {
            return AddStatisticsForObjectInvariants(inner.PropagateObjectInvariants(asInvariant));
        }

        #endregion

        #region Dumping
        public static void DumpStatistics(IOutput output)
        {
            Contract.Requires(output != null);

            output.WriteLine("Inferred {0} object invariants", generated);
            output.WriteLine("Retained {0} object invariants after filtering", retained);
        }

        #endregion

        #region Private

        private int AddStatisticsForObjectInvariants(int p)
        {
            if (!statsEmitted)
            {
                retained += p;
                statsEmitted = true;
            }

            return p;
        }
        #endregion
    }
}