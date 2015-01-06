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
using System.Diagnostics.Contracts;
using Microsoft.Research.ClousotRegression;

namespace AssumeSuggestion
{
  public abstract class AssumePre
  {
    public virtual void F(int x) { }

    public abstract int Len(object y);
  }

  public class AssumePreSub : AssumePre
  {
   
    [ClousotRegressionTest]
    [RegressionOutcome("Contract.Assume(x > 0);")]
    [RegressionOutcome("Contract.Requires(!(this is AssumeSuggestion.AssumePreSub) || x > 0)")] // F: there is some extra info associated that we do not capture in the regression output
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"assert unproven",PrimaryILOffset=4,MethodILOffset=0)]
    public override void F(int x)
    {
      Contract.Assert( x > 0);
    }

    [ClousotRegressionTest]
    [RegressionOutcome("Contract.Assume(y != null);")]
    [RegressionOutcome("Contract.Requires(!(this is AssumeSuggestion.AssumePreSub) || y != null)")] // F: there is some extra info associated that we do not capture in the regression output
    [RegressionOutcome("Contract.Ensures(Contract.Result<System.Int32>() == y.GetHashCode() + 1);")]
    [RegressionOutcome("Contract.Ensures(Contract.Result<System.Int32>() - y.GetHashCode() == 1);")]
    [RegressionOutcome("Contract.Ensures((y.GetHashCode() - Contract.Result<System.Int32>()) < 0);")]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Possibly calling a method on a null reference 'y'",PrimaryILOffset=1,MethodILOffset=0)]
    public override int Len(object y)
    {
      return y.GetHashCode() + 1;
    }
  }


  public interface I
  {
    int[] MyMethod(int z);
  }

  public class IImplementation : I
  { 
    [ClousotRegressionTest]
    [RegressionOutcome("Contract.Assume(0 <= z);")]
    [RegressionOutcome("Contract.Requires(!(this is AssumeSuggestion.IImplementation) || 0 <= z)")] // F: there is some extra info associated that we do not capture in the regression output
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"The length of the array may be negative",PrimaryILOffset=1,MethodILOffset=0)]
    [RegressionOutcome("Contract.Ensures(Contract.ForAll(0, Contract.Result<System.Int32[]>().Length, __k__ => Contract.Result<System.Int32[]>()[__k__] == 0));")]
    public int[] MyMethod(int z)
    {
      return new int[z];
    }
  }

  public class NoPre
  {
    private int x;

    public NoPre(int x)
    {
      this.x = x;
    }
 
    [ClousotRegressionTest]
    [RegressionOutcome("Contract.Assume(z < this.x);")] // This cannot be an object invariant, as it relates the parameter with the private state
    [RegressionOutcome("Contract.Ensures((z - this.x) < 0);")]
    [RegressionOutcome("Contract.Ensures(-2147483647 <= this.x);")]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"assert unproven",PrimaryILOffset=9,MethodILOffset=0)]    
     public void BadCode(int z)
    {
      Contract.Assert(z < this.x);
    }
  }

  public class AssumeReturnValue
  {
    [ContractVerification(false)]
    public string AssumeNotNull(string s)
    {
      return s;
    }

    [ClousotRegressionTest]
#if CODEFIXES
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Possibly calling a method on a null reference 's'. Are you making some assumption on AssumeNotNull that the static checker is unaware of? ",PrimaryILOffset=9,MethodILOffset=0)]
    // [RegressionOutcome("Contract.Assume(s != null);")]
 //   [RegressionOutcome("This condition should hold: s != null. Add an assume, a postcondition, or consider a different initialization. Fix: Add (after) Contract.Assume(s != null);")]
	[RegressionOutcome("This condition should hold: s != null. Add an assume, a postcondition to method AssumeNotNull, or consider a different initialization. Fix: Add (after) Contract.Assume(s != null);")]
#else
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Possibly calling a method on a null reference 's'",PrimaryILOffset=9,MethodILOffset=0)]
#endif
    [RegressionOutcome("Contract.Ensures(0 <= Contract.Result<System.Int32>());")]
    public int AssumingItIsNotNullImplicit(string someStr)
    {
      var s = AssumeNotNull(someStr);
      // I am assuming s it is not null!
      return s.Length;
    }

    [ClousotRegressionTest]
#if CODEFIXES
 	[RegressionOutcome("This condition should hold: s != null. Add an assume, a postcondition to method AssumeNotNull, or consider a different initialization. Fix: Add (after) Contract.Assume(s != null);")]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"assert unproven. Are you making some assumption on AssumeNotNull that the static checker is unaware of? ",PrimaryILOffset=15,MethodILOffset=0)]
#else
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"assert unproven",PrimaryILOffset=15,MethodILOffset=0)]
#endif
    [RegressionOutcome("Contract.Ensures(0 <= Contract.Result<System.Int32>());")]    
    public int AssumingItIsNotNullExplicit(string someStr)
    {
      var s = AssumeNotNull(someStr);

      Contract.Assert(s != null);

      return s.Length;
    }

    [ContractVerification(false)]
    public int AssumeLargerThanZero()
    {
      return System.Environment.TickCount;
    }

    [ClousotRegressionTest]
#if !CODEFIXES
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"The length of the array may be negative",PrimaryILOffset=29,MethodILOffset=0)]
#else
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"The length of the array may be negative. Are you making some assumption on AssumeLargerThanZero that the static checker is unaware of? ",PrimaryILOffset=29,MethodILOffset=0)]
#endif
#if CODEFIXES
	[RegressionOutcome("This condition should hold: 0 <= returnValue. Add an assume, a postcondition to method AssumeLargerThanZero, or consider a different initialization. Fix: Add (after) Contract.Assume(0 <= returnValue);")]
#endif
    [RegressionOutcome("Contract.Ensures(Contract.Result<System.Int64[]>() != null);")]
    [RegressionOutcome("Contract.Ensures(z + Contract.OldValue(y) - y == 0);")]
    [RegressionOutcome("Contract.Ensures(0 <= Contract.Result<System.Int64[]>().Length);")]
    public long[] AssumingItIsLargerThanZero(int z, ref long y, ref double d)
    {
      y = z + y; // only yo avoid straight line code

      var returnValue = AssumeLargerThanZero();

      d = d / 2; // only yo avoid straight line code

      return new long[returnValue];
    }

  }
}
