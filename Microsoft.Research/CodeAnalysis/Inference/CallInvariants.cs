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

namespace Microsoft.Research.CodeAnalysis
{
  [ContractVerification(true)]
  public class CallInvariantsInferenceManager<Method>
  {
    #region Object invariant

    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(this.dataBase != null);
    }    
    #endregion

    #region State

    // For each caller (key), the call invariants for the calle (Value.First)
    private readonly Dictionary<Method, List<Tuple<Method, BoxedExpression>>> dataBase;

    private Tuple<int, Dictionary<Method, List<BoxedExpression>>> cached;
    private int version;

    #endregion
    
    public CallInvariantsInferenceManager()
    {
      this.dataBase = new Dictionary<Method, List<Tuple<Method, BoxedExpression>>>();
      this.cached = null;
      this.version = 0;
    }

    public void AddCallInvariant(Method caller, Method callee, BoxedExpression invariant)
    {
      Contract.Requires(caller != null);
      Contract.Requires(invariant != null);

      this.dataBase.AddToValues(caller, new Tuple<Method, BoxedExpression>(callee, invariant));
      this.version++;
    }

    public void SuggestCallInvariants(IOutput output)
    {
      Contract.Requires(output != null);

      var result = ComputeCallerInvariantsForMethods();

      output.WriteLine("Methods with call invariants: {0}", result.Count);

      foreach (var pair in result)
      {
        output.WriteLine("Call invariants for method {0}", pair.Key);
        foreach (var exp in pair.Value)
        {
          Contract.Assume(exp != null);
          output.WriteLine("   {0}", exp); 
        }
      }
    }

    private Dictionary<Method, List<BoxedExpression>> ComputeCallerInvariantsForMethods()
    {
      Dictionary<Method, List<BoxedExpression>> result;
      if (this.cached != null && this.cached.Item1 == this.version)
      {
        result = this.cached.Item2;
      }
      else
      {
        // build summary
        result = new Dictionary<Method, List<BoxedExpression>>();
        foreach (var value in this.dataBase.Values)
        {
          foreach (var entry in value)
          {
            Contract.Assume(entry.Item1 != null);
            result.AddToValues(entry.Item1, entry.Item2);
          }
        }

        this.cached = new Tuple<int, Dictionary<Method, List<BoxedExpression>>>(this.version, result);
      }
      return result;
    }
  }
}
