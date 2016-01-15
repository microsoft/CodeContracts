// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics.Contracts;

namespace Microsoft.Research.CodeAnalysis
{
    public partial interface IFrameworkLogOptions
    {
        /// <summary>
        /// Print tracing information during DFA steps
        /// </summary>
        bool TraceDFA { get; }

        /// <summary>
        /// Trace the heap analysis phase of the framework
        /// </summary>
        bool TraceHeapAnalysis { get; }

        /// <summary>
        /// Trace the expression analysis phase of the framework
        /// </summary>
        bool TraceExpressionAnalysis { get; }

        /// <summary>
        /// Trace the egraph operations of the framework
        /// </summary>
        bool TraceEGraph { get; }

        /// <summary>
        /// Trace assumptions of the framework
        /// </summary>
        bool TraceAssumptions { get; }

        /// <summary>
        /// Trace weakest precondition paths
        /// </summary>
        bool TraceWP { get; }

        /// <summary>
        /// Emit the WP formula we cannot prove in the SMT-LIB format
        /// </summary>
        bool EmitSMT2Formula { get; }

        bool TraceNumericalAnalysis { get; }

        /// <summary>
        /// Trace the execution of the partition analysis
        /// </summary>
        bool TracePartitionAnalysis { get; }

        bool TraceInference { get; }

        bool TraceChecks { get; }

        /// <summary>
        /// Trace, for each transfer function, its cost
        /// </summary>
        bool TraceTimings { get; }

        /// <summary>
        /// Trace, for each transfer function, its memory usage
        /// </summary>
        bool TraceMemoryConsumption { get; }

        bool TraceMoveNext { get; }

        bool TraceSuspended { get; }

        /// <summary>
        /// True if analysis framework should print IL of CFGs
        /// </summary>
        bool PrintIL { get; }

        /// <summary>
        /// If true, try to prioritize the warning messages
        /// </summary>
        bool PrioritizeWarnings { get; }

        /// <summary>
        /// Controls whether or not to print suggestions for requires
        /// </summary>
        bool SuggestRequires { get; }

        /// <summary>
        /// Suggest turning assertions into contracts
        /// </summary>
        bool SuggestAssertToContracts { get; }

        /// <summary>
        /// Controls whether or not to print suggestions on arrays 
        /// - Eventually this will be merged with SuggestRequires, now we keep it separate for debugging/development
        /// </summary>
        bool SuggestRequiresForArrays { get; }

        /// <summary>
        /// Controls whether or not should output the suggestion for [Pure] for arrays
        /// </summary>
        bool SuggestRequiresPurityForArrays { get; }

        /// <summary>
        /// Controls whether or not to print ensures suggestions
        /// </summary>
        bool SuggestEnsures(bool isProperty);

        /// <summary>
        /// Control whether or not to print return != null suggestions
        /// </summary>
        bool SuggestNonNullReturn { get; }

        bool InferPreconditionsFromPostconditions { get; }

        bool PropagateObjectInvariants { get; }

        bool InferAssumesForBaseLining { get; }

        /// <summary>
        /// True if we should try to propagate inferred pre-conditions for the method <code>m</code>
        /// </summary>
        /// <param name="isGetterOrSetter">Is the current method a getter or a setter</param>
        bool PropagateInferredRequires(bool isCurrentMethodGetterOrSetter);

        /// <summary>
        /// True iff we should try to propagate the inferred postconditions for the method <code>m</code>
        /// </summary>
        /// <param name="isGetterOrSetter">Is the current method a getter or a setter</param>
        bool PropagateInferredEnsures(bool isCurrentMethodGetterOrSetter);

        /// <summary>
        /// True iff we want to propagate the ensures != null inferred for a method
        /// </summary>
        bool PropagateInferredNonNullReturn { get; }

        bool PropagateInferredSymbolicReturn { get; }

        bool PropagateRequiresPurityForArrays { get; }

        bool PropagatedRequiresAreSufficient { get; }

        bool CheckFalsePostconditions { get; }

        /// <summary>
        /// When printing CS files with contracts, limit output to visible items.
        /// </summary>
        bool OutputOnlyExternallyVisibleMembers { get; }

        /// <summary>
        /// The timeout, expressed in minutes, to stop the analysis
        /// </summary>
        int Timeout { get; }

        /// <summary>
        /// The symbolic timeout, expressed in symbolic ticks, to stop the analysis
        /// </summary>
        long SymbolicTimeout { get; }

        /// <summary>
        /// How many joins before widening ?
        /// </summary>
        int IterationsBeforeWidening { get; }

        /// <summary>
        /// How many variables before giving up the inference of Octagons?
        /// </summary>
        int MaxVarsForOctagonInference { get; }

        int MaxVarsInSingleRenaming { get; }

        bool IsAdaptiveAnalysis { get; }

        /// <summary>
        /// Enforce the fair join
        /// </summary>
        bool EnforceFairJoin { get; }

        bool TurnArgumentExceptionThrowsIntoAssertFalse { get; }

        bool IgnoreExplicitAssumptions { get; }

        /// <summary>
        /// True iff we want to show the analysis phases
        /// </summary>
        bool ShowPhases { get; }

        bool SufficientConditions { get; }
    }

    #region IFrameworkLogOptions contract binding
    [ContractClass(typeof(IFrameworkLogOptionsContract))]
    public partial interface IFrameworkLogOptions
    {
    }

    [ContractClassFor(typeof(IFrameworkLogOptions))]
    internal abstract class IFrameworkLogOptionsContract : IFrameworkLogOptions
    {
        #region IFrameworkLogOptions Members

        public bool TraceDFA
        {
            get { throw new NotImplementedException(); }
        }

        public bool TraceHeapAnalysis
        {
            get { throw new NotImplementedException(); }
        }

        public bool TraceExpressionAnalysis
        {
            get { throw new NotImplementedException(); }
        }

        public bool TraceEGraph
        {
            get { throw new NotImplementedException(); }
        }

        public bool TraceAssumptions
        {
            get { throw new NotImplementedException(); }
        }

        public bool TraceWP
        {
            get { throw new NotImplementedException(); }
        }

        public bool TraceNumericalAnalysis
        {
            get { throw new NotImplementedException(); }
        }

        public bool TracePartitionAnalysis
        {
            get { throw new NotImplementedException(); }
        }

        public bool TraceTimings
        {
            get { throw new NotImplementedException(); }
        }

        public bool PrintIL
        {
            get { throw new NotImplementedException(); }
        }

        public bool PrioritizeWarnings
        {
            get { throw new NotImplementedException(); }
        }

        public bool SuggestRequires
        {
            get { throw new NotImplementedException(); }
        }

        public bool SuggestAssertToContracts
        {
            get { throw new NotImplementedException(); }
        }

        public bool SuggestRequiresForArrays
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool SuggestRequiresPurityForArrays { get { throw new NotImplementedException(); } }


        public bool SuggestEnsures(bool isProperty)
        {
            throw new NotImplementedException();
        }

        public bool SuggestNonNullReturn
        {
            get { throw new NotImplementedException(); }
        }

        public bool OutputOnlyExternallyVisibleMembers
        {
            get { throw new NotImplementedException(); }
        }

        public bool InferPreconditionsFromPostconditions
        {
            get { throw new NotImplementedException(); }
        }

        public int Timeout
        {
            get
            {
                Contract.Ensures(Contract.Result<int>() >= 0);

                throw new NotImplementedException();
            }
        }

        public long SymbolicTimeout
        {
            get
            {
                Contract.Ensures(Contract.Result<long>() >= 0);

                throw new NotImplementedException();
            }
        }

        public int IterationsBeforeWidening
        {
            get { throw new NotImplementedException(); }
        }

        public int MaxVarsForOctagonInference
        {
            get { throw new NotImplementedException(); }
        }

        public bool EnforceFairJoin
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region IFrameworkLogOptions Members

        public bool TraceChecks
        {
            get { throw new NotImplementedException(); }
        }


        public bool InferRequiresPurityForArrays
        {
            get { throw new NotImplementedException(); }
        }

        public bool TraceInference
        {
            get { throw new NotImplementedException(); }
        }

        public bool TraceSuspended
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region No interesting contract

        public bool PropagateInferredRequires(bool isCurrentMethodGetterOrSetter)
        {
            throw new NotImplementedException();
        }

        public bool PropagateSimpleRequires
        {
            get { throw new NotImplementedException(); }
        }

        public bool PropagateInferredEnsures(bool isCurrentMethodGetterOrSetter)
        {
            throw new NotImplementedException();
        }

        public bool PropagateInferredNonNullReturn
        {
            get { throw new NotImplementedException(); }
        }

        public bool PropagateRequiresPurityForArrays
        {
            get { throw new NotImplementedException(); }
        }

        public bool PropagatedRequiresAreSufficient
        {
            get { throw new NotImplementedException(); }
        }


        public bool EmitSMT2Formula
        {
            get { throw new NotImplementedException(); }
        }


        public bool IgnoreExplicitAssumptions
        {
            get { throw new NotImplementedException(); }
        }


        public bool ShowPhases
        {
            get { throw new NotImplementedException(); }
        }


        public bool TraceMemoryConsumption
        {
            get { throw new NotImplementedException(); }
        }


        public bool CheckFalsePostconditions
        {
            get { throw new NotImplementedException(); }
        }

        public bool PropagateObjectInvariants
        {
            get { throw new NotImplementedException(); }
        }

        public bool InferAssumesForBaseLining
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region IFrameworkLogOptions Members


        public bool TurnArgumentExceptionThrowsIntoAssertFalse
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        public bool PropagateInferredSymbolicReturn
        {
            get { throw new NotImplementedException(); }
        }

        public bool SufficientConditions
        {
            get { throw new NotImplementedException(); }
        }

        public bool TraceMoveNext { get { return default(bool); } }

        public int MaxVarsInSingleRenaming
        {
            get { Contract.Ensures(Contract.Result<int>() >= 0); return 0; }
        }


        public bool IsAdaptiveAnalysis
        {
            get { throw new NotImplementedException(); }
        }
    }
    #endregion
}
