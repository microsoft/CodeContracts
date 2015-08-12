// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.IO;
using System.Diagnostics.Contracts;
using Microsoft.Research.ClousotRegression;

namespace Protocols
{
    /// <summary>
    /// Example class with a protocol.
    /// </summary>
    public class ClassWithProtocol
    {
        /// <summary>
        /// The possible states of the protocol instance.
        /// </summary>
        public enum S
        {
            /// <summary>
            /// Object has not been initialized
            /// </summary>
            NotReady,
            /// <summary>
            /// Object is initialized and Data is available
            /// </summary>
            Initialized,
            /// <summary>
            /// Computed data is now available.
            /// </summary>
            Computed
        }

        private S _state;

        /// <summary>
        /// The current state of the protocol instance.
        /// </summary>
        public S State
        {
            [ClousotRegressionTest]
            get
            {
                //Contract.Ensures(Contract.Result<S>() == _state);

                return _state;
            }
        }

        /// <summary>
        /// Object invariant method.
        /// </summary>
        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(_state != S.Computed || _computedData != null);
        }

        /// <summary>
        /// Create a new protocol class
        /// </summary>
        [ClousotRegressionTest]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 15, MethodILOffset = 27)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"invariant is valid", PrimaryILOffset = 24, MethodILOffset = 27)]
        public ClassWithProtocol()
        {
            Contract.Ensures(this.State == S.NotReady);
            _state = S.NotReady;
        }

        private string _data;

        /// <summary>
        /// Initializes the protocol instance so that the Compute method becomes valid.
        /// Furthermore, the Data property becomes accessible as well.
        /// </summary>
        /// <param name="data">string value used to initialize Data property</param>
        [ClousotRegressionTest]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 23, MethodILOffset = 42)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"invariant is valid", PrimaryILOffset = 24, MethodILOffset = 42)]
        public void Initialize(string data)
        {
            Contract.Requires(State == S.NotReady);
            Contract.Ensures(State == S.Initialized);

            _data = data;
            _state = S.Initialized;
        }

        /// <summary>
        /// Further initializes the protocol instance into its final state.
        /// Now the ComputedData property becomes valid, provided the method returns true.
        /// </summary>
        /// <param name="prefix">Used to initialize the computed data</param>
        /// <returns>true if transition succeeds. Upon a false return, the instance stays in the Initialized state</returns>
        [ClousotRegressionTest]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 64, MethodILOffset = 95)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"invariant is valid", PrimaryILOffset = 24, MethodILOffset = 95)]
        public bool Compute(string prefix)
        {
            Contract.Requires(prefix != null);
            Contract.Requires(State == S.Initialized);
            Contract.Ensures(Contract.Result<bool>() && State == S.Computed ||
                             !Contract.Result<bool>() && State == S.Initialized);

            _computedData = prefix + _data;
            _state = S.Computed;

            return true;
        }

        /// <summary>
        /// The data value of the protocol instance.
        /// </summary>
        public string Data
        {
            get
            {
                Contract.Requires(State != S.NotReady);

                return _data;
            }
        }


        private string _computedData;
        /// <summary>
        /// The computed data value. Available when state is Computed.
        /// </summary>
        public string ComputedData
        {
            [ClousotRegressionTest]
            [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 35, MethodILOffset = 46)]
            get
            {
                Contract.Requires<InvalidOperationException>(State == S.Computed, "object must be in Computed state");
                Contract.Ensures(Contract.Result<string>() != null, "result is non-null");

                return _computedData;
            }
        }
    }
}
