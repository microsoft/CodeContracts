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
using System.Diagnostics.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Research.ClousotRegression;

namespace TestFrameworkOOB.Purity
{
  class Tests
  {
    [ClousotRegressionTest]
    public static void Test(object a, object b)
    {
      Contract.Requires(Object.ReferenceEquals(a, b));

    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "Possibly calling a method on a null reference \'dict\'", PrimaryILOffset = 3, MethodILOffset = 0), RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 16, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 21, MethodILOffset = 0)]
    public static void Test(IDictionary<int, string> dict, int key)
    {
      Contract.Requires(dict.ContainsKey(key));

      Contract.Assert(dict.ContainsKey(key));
    }
  }

  interface J { }

  class TypeMethodPurity : J
  {
    void Get(Type messageType)
    {
      Contract.Requires(messageType != null && typeof(J).IsAssignableFrom(messageType));
    }

    void Foo()
    {
      J message = new TypeMethodPurity();
      Type t = message.GetType();
      Contract.Assert(t != null);
      Contract.Assume(t == typeof(TypeMethodPurity));
      Contract.Assume(typeof(J).IsAssignableFrom(typeof(TypeMethodPurity)));
      Contract.Assume(typeof(J).IsAssignableFrom(t));
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 25, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 47, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 52, MethodILOffset = 0)]
    void Bar(Type t)
    {
      Contract.Requires(t != null);
      Contract.Requires(typeof(J).IsAssignableFrom(t));

      Contract.Assert(typeof(J).IsAssignableFrom(t));

    }
  }
}
