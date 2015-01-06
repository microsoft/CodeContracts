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
using System.IO;
using System.Diagnostics.Contracts;
using Contracts.Regression;

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
      //[RegressionOutcome(Outcome = ProofOutcome.True, Message = @"invariant is valid", PrimaryILOffset = 24, MethodILOffset = 6)]
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
    void ObjectInvariant()
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

    string _data;

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

      this._data = data;
      _state = S.Initialized;
    }

    /// <summary>
    /// Further initializes the protocol instance into its final state.
    /// Now the ComputedData property becomes valid, provided the method returns true.
    /// </summary>
    /// <param name="prefix">Used to initialize the computed data</param>
    /// <returns>true if transition succeeds. Upon a false return, the instance stays in the Initialized state</returns>
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="invariant is valid",PrimaryILOffset=24,MethodILOffset=95)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="ensures is valid",PrimaryILOffset=64,MethodILOffset=95)]
    public bool Compute(string prefix)
    {
      Contract.Requires(prefix != null);
      Contract.Requires(State == S.Initialized);
      Contract.Ensures(Contract.Result<bool>() && State == S.Computed ||
                       !Contract.Result<bool>() && State == S.Initialized);

      this._computedData = prefix + _data;
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


    string _computedData;
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