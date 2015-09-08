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

/* Those tests simulate the analysis on the  extracted method.
   This is why they require inferring (Pm, Qm) -- i.e. requires and ensures -- and then refining them with Qs, which we explictly wrote down as a postcondition in the method */
  
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Microsoft.Research.ClousotRegression;

namespace ExtractMethod
{
  public class ExtractMethod
  {
    [ClousotRegressionTest("cci1only")]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Array access might be below the lower bound",PrimaryILOffset=18,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Array access might be above the upper bound",PrimaryILOffset=18,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible use of a null array 'a'",PrimaryILOffset=18,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="ensures unproven: Contract.Result<int>() >= 0",PrimaryILOffset=11,MethodILOffset=21)]
    [RegressionOutcome("Contract.Requires(a != null);")]
    [RegressionOutcome("Contract.Requires(0 <= i);")]
    [RegressionOutcome("Contract.Requires(i < a.Length);")]
    [RegressionOutcome("Contract.Ensures(a[i] == Contract.Result<System.Int32>());")]
#if CLOUSOT2
    [RegressionOutcome("Extract method suggestion: Contract.Requires(0 <= a[i]);")]
#else
    [RegressionOutcome("Extract method suggestion: Contract.Requires((a[i] < 0) == false);")]
#endif
    public int TestArray(int[] a, int i)
    {
      Contract.Ensures(Contract.Result<int>() >= 0);
      
      var x = a[i];
      
      return x;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="ensures unproven: Contract.Result<int>() == 2",PrimaryILOffset=8,MethodILOffset=24)]
    //[RegressionOutcome("Contract.Ensures(Contract.Result<System.Int32>() == 2);")]
    //[RegressionOutcome("Extract method suggestion: Contract.Requires(i < j);")] // TODO: take into account guards
    public int TestIf(int i, int j)
    {
      Contract.Ensures(Contract.Result<int>() == 2);

      int x;
      if (i < j)
        x = 2;
      else
        x = 4;

      return x;
    }

    // Even if we prove the postcondition (because we assume the decrement does not cause any underflow), we still have the warning
    // for the checked decrement. The suggest precondition will get rid of it
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible underflow in the arithmetic operation",PrimaryILOffset=17,MethodILOffset=0)]
    //[RegressionOutcome("Contract.Ensures(Contract.Result<System.Int32>() == 0);")]
    [RegressionOutcome("Contract.Ensures((Contract.Result<System.Int32>() - Contract.OldValue(x)) <= 0);")]
    [RegressionOutcome("Extract method suggestion: Contract.Requires(0 <= x);")]
    public int TestLoop(int x)
    {
      Contract.Ensures(Contract.Result<int>() == 0);
      
      while (x != 0)
	  {
	    checked { x--; } // possible underflow
      }
      
      return x;
    }
    
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Array access might be below the lower bound",PrimaryILOffset=112,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Array access might be above the upper bound",PrimaryILOffset=112,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="ensures unproven: Contract.Result<int>() < a.Length",PrimaryILOffset=22,MethodILOffset=121)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="ensures unproven: -1 <= Contract.Result<int>()",PrimaryILOffset=38,MethodILOffset=121)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="ensures unproven: Contract.Result<int>() <= i",PrimaryILOffset=55,MethodILOffset=121)]
    [RegressionOutcome("Contract.Requires(0 <= i);")]
    [RegressionOutcome("Contract.Requires(i < a.Length);")]
    [RegressionOutcome("Extract method suggestion: Contract.Requires(last < a.Length);")]
#if CLOUSOT2
	[RegressionOutcome("Extract method suggestion: Contract.Requires(!(-1 > last));")]
	[RegressionOutcome("Extract method suggestion: Contract.Requires(!(last > i));")]
	[RegressionOutcome("Extract method suggestion: Contract.Requires(!(last > a.Length));")]
	[RegressionOutcome("Extract method suggestion: Contract.Requires(!(last < -1));")]
#else
    [RegressionOutcome("Extract method suggestion: Contract.Requires((-1 > last) == false);")]
    [RegressionOutcome("Extract method suggestion: Contract.Requires((last > i) == false);")]
    [RegressionOutcome("Extract method suggestion: Contract.Requires((last > a.Length) == false);")]
    [RegressionOutcome("Extract method suggestion: Contract.Requires((last < -1) == false);")]
#endif
    int ExtractFromLinearSearch(int[] a, int k, int last, int i)
    { 
      Contract.Requires(a != null);
      Contract.Ensures(Contract.Result<int>() < a.Length);
      Contract.Ensures(-1 <= Contract.Result<int>());
      Contract.Ensures(Contract.Result<int>() <= i);
      Contract.Ensures(Contract.Result<int>() <= a.Length);
      Contract.Ensures(Contract.Result<System.Int32>() < a.Length);
      Contract.Ensures(Contract.Result<System.Int32>() >= -1);
      
      if (a[i] == k)
	last = i;
      return last;
}
 
     [ClousotRegressionTest]
     [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Array access might be below the lower bound",PrimaryILOffset=85,MethodILOffset=0)]
     [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Array access might be above the upper bound",PrimaryILOffset=85,MethodILOffset=0)]
#if CLOUSOT2
     [RegressionOutcome(Outcome=ProofOutcome.Top,Message="ensures unproven: Contract.Exists(0, a.Length, __i__ => Contract.Result<System.Int32>() == a[__i__])",PrimaryILOffset=73,MethodILOffset=95)]
#else
     [RegressionOutcome(Outcome=ProofOutcome.Top,Message="ensures unproven: Contract.Exists(0, a.Length, __i__ =>  Contract.Result<System.Int32>() == a[__i__])",PrimaryILOffset=73,MethodILOffset=95)]
#endif
     [RegressionOutcome("Contract.Requires(0 <= i);")]
     [RegressionOutcome("Contract.Requires(i < a.Length);")]
     [RegressionOutcome("Contract.Ensures((a[i] - Contract.Result<System.Int32>()) <= 0);")]
     [RegressionOutcome("Contract.Ensures((Contract.OldValue(max) - Contract.Result<System.Int32>()) <= 0);")]
     [RegressionOutcome("Extract method suggestion: Contract.Requires(Contract.Exists(0, a.Length, i => max == a[i]));")]
     int ExtractFromMax(int[] a, int i, int max)
     {
       Contract.Requires(a != null);
       Contract.Ensures(a != null);
       Contract.Ensures(Contract.Exists(0, a.Length, __i__ =>  Contract.Result<System.Int32>() == a[__i__]));

            var tmp = a[i];
                if (tmp > max)
                {
                    max = tmp;
                }
      return max;
     }
    }
  }