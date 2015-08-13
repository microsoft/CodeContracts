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
    /// The manager for candidate postconditions
    /// </summary>
    [ContractClass(typeof(IPostConditionDispatcherContracts))]
    public interface IPostconditionDispatcher
    {
        /// <summary>
        /// Add the postcondition to the list of current preconditions
        /// </summary>
        void AddPostconditions(IEnumerable<BoxedExpression> postconditions);

        /// <summary>
        /// Report, for the given method, the set of non-null fields as inferred
        /// </summary>
        void AddNonNullFields(object method, IEnumerable<object> fields);

        /// <summary>
        /// Get the non-null fields at the method exit point
        /// </summary>
        IEnumerable<object> GetNonNullFields(object method);

        /// <summary>
        /// Returns the list of precondition for this method
        /// </summary>
        List<BoxedExpression> GeneratePostconditions();

        /// <summary>
        /// Suggest the postcondition.
        /// Returns how many preconditions have been suggested
        /// </summary>
        int SuggestPostconditions();

        /// <summary>
        /// Suggests the postconditions for the constructors of the type currently analyzed
        /// </summary>
        int SuggestNonNullFieldsForConstructors();

        /// <summary>
        /// When all the constructors of a given type are analyzed, it gets the != null object invariants
        /// </summary>
        /// <returns></returns>
        IEnumerable<BoxedExpression> SuggestNonNullObjectInvariantsFromConstructorsForward(bool doNotRecord = false);

        /// <summary>
        /// Infer the postcondition.
        /// Returns how many postconditionss have been propagated to the callers
        /// </summary>
        int PropagatePostconditions();

        /// <summary>
        /// Infer postconditions for autoproperties, i.e., if we detect this.F != null, then it will attach the postcondition result != null to the autoproperty F
        /// Returns how many postconditions have been installed
        /// </summary>
        /// <returns></returns>
        int PropagatePostconditionsForProperties();

        /// <summary>
        /// Returns true if the method may return null directly (e.g. "return null" or indirectly (e.g., "return foo()" and we know that foo may return null)
        /// </summary>
        /// <returns></returns>
        bool MayReturnNull(IFact facts, TimeOutChecker timeout);

        /// <summary>
        /// If we infer false for the method, and it contains user-postconditions, warn the user
        /// </summary>
        bool EmitWarningIfFalseIsInferred();
    }

    #region Contracts

    [ContractClassFor(typeof(IPostconditionDispatcher))]
    internal abstract class IPostConditionDispatcherContracts : IPostconditionDispatcher
    {
        public void AddPostconditions(IEnumerable<BoxedExpression> postconditions)
        {
            Contract.Requires(postconditions != null);
        }

        public void AddNonNullFields(object method, IEnumerable<object> fields)
        {
            Contract.Requires(method != null);
            throw new NotImplementedException();
        }
        public IEnumerable<object> GetNonNullFields(object method)
        {
            Contract.Requires(method != null);

            return null;
        }
        public List<BoxedExpression> GeneratePostconditions()
        {
            Contract.Ensures(Contract.Result<List<BoxedExpression>>() != null);

            throw new NotImplementedException();
        }

        public int SuggestPostconditions()
        {
            Contract.Ensures(Contract.Result<int>() >= 0);

            throw new NotImplementedException();
        }

        public IEnumerable<BoxedExpression> SuggestNonNullObjectInvariantsFromConstructorsForward(bool doNotRecord)
        {
            Contract.Ensures(Contract.Result<IEnumerable<BoxedExpression>>() != null);

            return null;
        }

        public int SuggestNonNullFieldsForConstructors()
        {
            Contract.Ensures(Contract.Result<int>() >= 0);
            throw new NotImplementedException();
        }

        public int PropagatePostconditions()
        {
            Contract.Ensures(Contract.Result<int>() >= 0);
            throw new NotImplementedException();
        }

        public int PropagatePostconditionsForProperties()
        {
            Contract.Ensures(Contract.Result<int>() >= 0);
            throw new NotImplementedException();
        }

        public bool MayReturnNull(IFact facts, TimeOutChecker timeout)
        {
            Contract.Requires(facts != null);
            throw new NotImplementedException();
        }

        public bool EmitWarningIfFalseIsInferred()
        {
            throw new NotImplementedException();
        }
    }

    #endregion

    [ContractVerification(true)]
    public class PostconditionDispatcherProfiler
      : IPostconditionDispatcher
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

        private readonly IPostconditionDispatcher inner;
        private bool profilingAlreadyCollected;

        #endregion

        #region Constructor

        public PostconditionDispatcherProfiler(IPostconditionDispatcher dispatcher)
        {
            Contract.Requires(dispatcher != null);

            inner = dispatcher;
            profilingAlreadyCollected = false;
        }

        #endregion

        #region Implementation of IPostConditionDispatcher
        public void AddPostconditions(IEnumerable<BoxedExpression> postconditions)
        {
            generated += postconditions.Count();
            inner.AddPostconditions(postconditions);
        }

        public void AddNonNullFields(object method, IEnumerable<object> fields)
        {
            inner.AddNonNullFields(method, fields);
        }

        public IEnumerable<object> GetNonNullFields(object method)
        {
            return inner.GetNonNullFields(method);
        }

        public List<BoxedExpression> GeneratePostconditions()
        {
            return inner.GeneratePostconditions();
        }

        public int SuggestPostconditions()
        {
            return RecordProfilingInformation(inner.SuggestPostconditions());
        }

        public int SuggestNonNullFieldsForConstructors()
        {
            return inner.SuggestNonNullFieldsForConstructors();
        }

        public IEnumerable<BoxedExpression> SuggestNonNullObjectInvariantsFromConstructorsForward(bool doNotRecord = false)
        {
            return inner.SuggestNonNullObjectInvariantsFromConstructorsForward(doNotRecord);
        }

        public int PropagatePostconditions()
        {
            return RecordProfilingInformation(inner.PropagatePostconditions());
        }

        public int PropagatePostconditionsForProperties()
        {
            return RecordProfilingInformation(inner.PropagatePostconditionsForProperties());
        }

        public bool MayReturnNull(IFact facts, TimeOutChecker timeout)
        {
            return RecordProfilingInformation(inner.MayReturnNull(facts, timeout));
        }

        public bool EmitWarningIfFalseIsInferred()
        {
            // We do not record conditions on emitted false
            return inner.EmitWarningIfFalseIsInferred();
        }

        #endregion

        #region Dumping
        public static void DumpStatistics(IOutput output)
        {
            Contract.Requires(output != null);

            if (generated != 0)
            {
                output.WriteLine("Discovered {0} postconditions to suggest", generated);
                output.WriteLine("Retained {0} postconditions after filtering", retained);
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

        private bool RecordProfilingInformation(bool mayReturnNull)
        {
            // We do not keep statistics for that -- for the moment?

            return mayReturnNull;
        }

        #endregion
    }
}
