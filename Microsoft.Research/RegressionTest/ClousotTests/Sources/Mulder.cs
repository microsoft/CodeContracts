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

using Microsoft.Research.ClousotRegression;

namespace ContractTest
{
  #region Usings

  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.Threading;

  #endregion

  /// <summary>
  /// enumerator joiner base.
  /// </summary>

  internal class EnumeratorJoinerBase : IEnumerator
  {
    #region Attributes

    /// <summary>
    /// ma x_ loc k_ wait.
    /// </summary>
    internal const int MAXLOCKWAIT = 10000; // milliseconds

    /// <summary>
    /// if null, the state of the enumeratorjoiner is invalid (ie it 
    /// points to before the first item or after the last item).
    /// current enumerator.
    /// </summary>
    protected int? _currentEnumerator;

    /// <summary>
    /// current object.
    /// </summary>
    protected object _currentObject;

    /// <summary>
    /// rw lock.
    /// </summary>
    protected ReaderWriterLock _rwLock;

    /// <summary>
    /// enumerators.
    /// </summary>
    private readonly IList<IEnumerator> _enumerators;

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="EnumeratorJoinerBase"/> class.
    /// </summary>
    /// <param name="rwLock">
    /// The rw lock.
    /// </param>
    /// <param name="enumerators">
    /// The enumerators.
    /// </param>
    [ClousotRegressionTest]
    internal EnumeratorJoinerBase(ReaderWriterLock rwLock, params IEnumerator[] enumerators)
    {
      this._rwLock = rwLock;
      this._enumerators = new List<IEnumerator>(enumerators);
    }

    #endregion

    #region Properties

    /// <summary>
    /// Gets Current.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// </exception>
    public virtual object Current
    {
      [ClousotRegressionTest]
      get
      {
        // TODO: detect modification of the collection
        switch (this._currentEnumerator)
        {
          case null:
            throw new InvalidOperationException("Current object accessed before MoveNext() was called.");
          case -1:
            throw new InvalidOperationException("Enumerator is past the end of the collection.");
          default:
            return this._currentObject;
        }
      }
    }

    #endregion

    #region Methods

    /// <summary>
    /// move next.
    /// </summary>
    /// <returns>
    /// The move next.
    /// </returns>
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possibly calling a method on a null reference 'this._rwLock'",PrimaryILOffset=11,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possibly calling a method on a null reference 'this._enumerators'. The static checker determined that the condition 'this._enumerators != null' should hold on entry. Nevertheless, the condition may be too strong for the callers. If you think it is ok, add an object invariant or an assumption at entry to document it: Contract.Invariant(this._enumerators != null);",PrimaryILOffset=64,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possibly calling a method on a null reference",PrimaryILOffset=69,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possibly calling a method on a null reference 'this._enumerators'. The static checker determined that the condition 'this._enumerators != null' should hold on entry. Nevertheless, the condition may be too strong for the callers. If you think it is ok, add an object invariant or an assumption at entry to document it: Contract.Invariant(this._enumerators != null);",PrimaryILOffset=100,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possibly calling a method on a null reference",PrimaryILOffset=105,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possibly calling a method on a null reference 'this._rwLock'",PrimaryILOffset=284,MethodILOffset=290)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="requires unproven: index < this.Count. The static checker determined that the condition '0 < this._enumerators.Count' should hold on entry. Nevertheless, the condition may be too strong for the callers. If you think it is ok, add an explicit assumption at entry to document it: Contract.Assume(0 < this._enumerators.Count);",PrimaryILOffset=33,MethodILOffset=64)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="requires unproven: index >= 0",PrimaryILOffset=13,MethodILOffset=100)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="requires unproven: index < this.Count",PrimaryILOffset=33,MethodILOffset=100)]
    public virtual bool MoveNext()
    {
      this._rwLock.AcquireReaderLock(MAXLOCKWAIT);
      try
      {
        switch (this._currentEnumerator)
        {
          case null:
            this._currentEnumerator = 0;
            this._enumerators[0].Reset();
            break;
          case -1:
            return false;
        }

        if (this._enumerators[this._currentEnumerator.Value].MoveNext())
        {
          this._currentObject = this._enumerators[this._currentEnumerator.Value].Current;
          return true;
        }
        else
        {
          // We've hit the last item of the current enumerator;
          if (this._currentEnumerator == this._enumerators.Count - 1)
          {
            // We're also on the last enumerator. State is now invalid.
            this._currentEnumerator = -1;
            this._currentObject = null;
            return false;
          }
          else
          {
            this._currentEnumerator++;
            return this.MoveNext();
          }
        }
      }
      finally
      {
        this._rwLock.ReleaseReaderLock();
      }
    }

    /// <summary>
    /// reset.
    /// </summary>
    [ClousotRegressionTest]
    public virtual void Reset()
    {
      this._currentEnumerator = null;
      this._currentObject = null;
    }

    #endregion
  }
}
