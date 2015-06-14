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
  abstract class IPostConditionDispatcherContracts : IPostconditionDispatcher
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
      Contract.Invariant(this.inner != null);
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

      this.inner = dispatcher;
      this.profilingAlreadyCollected = false;
    }
    
    #endregion

    #region Implementation of IPostConditionDispatcher
    public void AddPostconditions(IEnumerable<BoxedExpression> postconditions)
    {
      generated += postconditions.Count();
      this.inner.AddPostconditions(postconditions);
    }

    public void AddNonNullFields(object method, IEnumerable<object> fields)
    {
      this.inner.AddNonNullFields(method, fields);
    }

    public IEnumerable<object> GetNonNullFields(object method)
    {
      return this.inner.GetNonNullFields(method);
    }

    public List<BoxedExpression> GeneratePostconditions()
    {
      return this.inner.GeneratePostconditions();
    }

    public int SuggestPostconditions()
    {
      return RecordProfilingInformation(this.inner.SuggestPostconditions());
    }

    public int SuggestNonNullFieldsForConstructors()
    {
      return this.inner.SuggestNonNullFieldsForConstructors();
    }

    public IEnumerable<BoxedExpression> SuggestNonNullObjectInvariantsFromConstructorsForward(bool doNotRecord = false)
    {
      return this.inner.SuggestNonNullObjectInvariantsFromConstructorsForward(doNotRecord);
    }

    public int PropagatePostconditions()
    {
      return RecordProfilingInformation(this.inner.PropagatePostconditions());
    }

    public int PropagatePostconditionsForProperties()
    {
      return RecordProfilingInformation(this.inner.PropagatePostconditionsForProperties());
    }

    public bool MayReturnNull(IFact facts, TimeOutChecker timeout)
    {
      return RecordProfilingInformation(this.inner.MayReturnNull(facts, timeout));
    }

    public bool EmitWarningIfFalseIsInferred()
    {
      // We do not record conditions on emitted false
      return this.inner.EmitWarningIfFalseIsInferred();
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

      if (!this.profilingAlreadyCollected)
      {
        retained += howMany;
        this.profilingAlreadyCollected = true;
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
