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

#define CONTRACTS_FULL

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Research.ClousotRegression;
using System.Diagnostics.Contracts;

namespace IteratorSimpleContract {

  public class Test2 {
    #region Drivers
    [ClousotRegressionTestAttribute]
    [RegressionOutcome(Outcome = ProofOutcome.False, Message = @"requires is false: s != null", PrimaryILOffset = 55, MethodILOffset = 49)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 55, MethodILOffset = 20)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 55, MethodILOffset = 28)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 41, MethodILOffset = 0)]
    public string Test2a(string s) {
      Contract.Requires(s != null);
      var strs = Test1a("hello"); // Positive: precondtions of Test1a (s != null)
      strs = Test1a(s);           // Positive: precondition of Test1a (s != null)
      Contract.Assert(strs != null); // assertion is valid from the postcondition of Test1a
      strs = Test1a(null); // Negative: postcondition of Test1a (s!= null)
      return s;
    }

    [ClousotRegressionTestAttribute]
#if !CLOUSOT2
		[RegressionOutcome(Outcome = ProofOutcome.False, Message = @"requires is false: s != null", PrimaryILOffset = 63, MethodILOffset = 3)]
#else
		[RegressionOutcome(Outcome=ProofOutcome.False,Message="requires is false: s != null",PrimaryILOffset=44,MethodILOffset=3)]
#endif	
    public void Test2b(string s) {
      Test1b(null); // Negative: legacy precondition of Test1b not satisified: (s!= null)
    }

    [ClousotRegressionTestAttribute]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"assert unproven", PrimaryILOffset = 59, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.False, Message = @"requires is false: input != null", PrimaryILOffset = 59, MethodILOffset = 70)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 59, MethodILOffset = 16)]
#if !NETFRAMEWORK_4_0
	#if !CLOUSOT2
		[RegressionOutcome(Outcome=ProofOutcome.Top,Message="requires unproven",PrimaryILOffset=22,MethodILOffset=54)]
		[RegressionOutcome(Outcome=ProofOutcome.True,Message="requires is valid",PrimaryILOffset=44,MethodILOffset=54)]
	#else // CLOUSOT2
		[RegressionOutcome(Outcome=ProofOutcome.Top,Message="requires unproven",PrimaryILOffset=3,MethodILOffset=54)]
		[RegressionOutcome(Outcome=ProofOutcome.True,Message="requires is valid",PrimaryILOffset=25,MethodILOffset=54)]	
	#endif
#else // .net v4.0
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="requires unproven: collection != null (collection)",PrimaryILOffset=17,MethodILOffset=54)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="requires is valid",PrimaryILOffset=39,MethodILOffset=54)]
#endif
    public void Test2d(IEnumerable<string> s) {
      Contract.Requires(s != null);
      IEnumerable<string> resultEnum = Test1d(s); // Precondition is validated
      Contract.Assert(Contract.ForAll(resultEnum, (string s1) => s1 != null)); // From postcondition, but cannot prove for now.
      s = null;
      Test1d(s); // This one fails
    }

    // Forall(...) precondition cannot be proven for now in the two cases below. 
    [ClousotRegressionTestAttribute]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"requires unproven: Contract.ForAll(input, (T s) => s != null)", PrimaryILOffset = 67, MethodILOffset = 46)]
    public void Test2e(IEnumerable<string> ss) {
      Contract.Requires(Contract.ForAll(ss, (string s) => s != null));
      Test1e(ss);  // Even we have a precondition in the caller, the precondition of callee is not proven.
    }

    [ClousotRegressionTestAttribute]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"requires unproven: Contract.ForAll(inputArray, (int x) => x < max)", PrimaryILOffset = 103, MethodILOffset = 24)]
    public void Test2f() {
      int[] arr = { 1, 2, 3, 4 };
      int max = 5;
      Test1f(arr, max); // Forall precondition of the callee is not proven.
    }

    [ClousotRegressionTestAttribute]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 16, MethodILOffset = 0)]
    public void Test2g() {
      IEnumerable<int> singleton = Test1g(2);
      Contract.Assert(singleton != null); // Postcondition is proven. 
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.False,Message="assert is false",PrimaryILOffset=277,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="assert is valid",PrimaryILOffset=237,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="assert unproven",PrimaryILOffset=178,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="assert is valid",PrimaryILOffset=131,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="assert is valid",PrimaryILOffset=88,MethodILOffset=0)]
    public IEnumerable<int> TestIteratorPaths(int x) {
      Contract.Requires(x > 0);

      Contract.Assert(x > 0);
      yield return 1;

      Contract.Assert(x > 0);
      x = 2;
      yield return 2;

      Contract.Assert(x == 2);
      x = x * x;
      yield return 2;

      Contract.Assert(x > 0);
      yield return 3;

      Contract.Assert(x < 0);
    }
    #endregion Drivers

    #region Iterator methods with contracts
    /// <summary>
    /// Contract for iterator method: Requires, Ensures, Ensures with forall.
    /// Iterator class with closure classes, both in forall and in the code. 
    /// </summary>
    public IEnumerable<string> Test1a(string s) {
      Contract.Requires(s != null);
      Contract.Ensures(Contract.Result<IEnumerable<string>>() != null);
      Contract.Ensures(Contract.ForAll(Contract.Result<IEnumerable<string>>(), (string s1) => s1 != null));
      int[] x = { 1, 2 };
      x.First(((int y) => y > 0));
      yield return s;
    }
    /// <summary>
    /// Contract for iterator method: Legacy precondition, with EndContractBlock().
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public IEnumerable<string> Test1b(string s) {
      if (s == null) throw new Exception("");
      Contract.EndContractBlock();
      yield return s;
    }

    /// <summary>
    /// Contract for iterator method with type parameter. 
    /// Post condition is forall
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="input"></param>
    /// <returns></returns>
    public IEnumerable<T> Test1d<T>(IEnumerable<T> input) {
      Contract.Requires(input != null);
      Contract.Ensures(Contract.ForAll(Contract.Result<IEnumerable<T>>(), (T s1) => s1 != null));
      foreach (T t in input)
        yield return t;
    }

    /// <summary>
    /// Contract for iterator method with type parameters and input ienumerable, precondition is forall. 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="input"></param>
    public IEnumerable<T> Test1e<T>(IEnumerable<T> input) {
      Contract.Requires(Contract.ForAll(input, (T s) => s != null));
      foreach (T t in input) {
        yield return t;
      }
    }

    /// <summary>
    /// Contract for iterator method with closure that captures a parameter. 
    /// </summary>
    /// <param name="inputArray"></param>
    /// <param name="max"></param>
    public IEnumerable<int> Test1f(IEnumerable<int> inputArray, int max) 
    {
      Contract.Requires(Contract.ForAll(inputArray, (int x) => x < max));
      Contract.Ensures(Contract.ForAll(Contract.Result<IEnumerable<int>>(), (int y) => y >0));
      foreach (int i in inputArray) {
        yield return (max -i);
      }
    }

    /// <summary>
    /// Contract for iterator method: simplest post condition: result is non-null;
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    public IEnumerable<int> Test1g(int x) {
      Contract.Ensures(Contract.Result<IEnumerable<int>>() != null);
      yield return x;
    }
    #endregion Iterator methods with contracts
  }
}
