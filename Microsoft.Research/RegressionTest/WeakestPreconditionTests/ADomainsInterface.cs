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
using Microsoft.Research.ClousotRegression;

// *** NOTE ***
// Peli's Trim Example is in the TestFrameworkOOB directory, as it uses mscorlib.contracts.dll
// ************

namespace ExamplesRequiringReasoningFromAbstractDomains
{
  class PowerExample
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = (int)19, MethodILOffset = (int)93)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = (int)44, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 73, MethodILOffset = 0)]
    public int Power(int baseVal, int exponent)
    {
      Contract.Ensures(baseVal < 0 || Contract.Result<int>() >= 0);

      int result = 1;
      while (exponent > 0)
      {
        Contract.Assert(baseVal < 0 || result >= 0);
        result *= baseVal;
        exponent--;
        // can't prove this yet, and the assert isn't helping the post condition either.
        // need to compute fixpoints on the backward pass as well to prove this.
        Contract.Assert(baseVal < 0 || result >= 0);
      }
      return result;
    }
  }

  class BrianExample
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as array)", PrimaryILOffset = 20, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as array)", PrimaryILOffset = 35, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"Lower bound access ok", PrimaryILOffset = 35, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"Upper bound access ok", PrimaryILOffset = 35, MethodILOffset = 0)]
    public static void IncrementIndex(int[] array, int index)
    {
      Contract.Requires(array != null);
      Contract.Requires(index >= 0 && index < array.Length);

      array[index]++;
    }
  }

  class MafRiSEExample
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 72, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"assert unproven", PrimaryILOffset = 168, MethodILOffset = 0)]
    //[RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 168, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "ensures unproven: y >= 0 && Contract.Result<int>() >= 0 || y < 0 && Contract.Result<int>() < 0", PrimaryILOffset = 31, MethodILOffset = 194)]
    public int Loop_Wrong(int y)
    {
      Contract.Ensures(y >= 0 && Contract.Result<int>() >= 0 || y < 0 && Contract.Result<int>() < 0);

      int oldy = y;
      int x = 0;
      while (y != 0)
      {
        Contract.Assert(oldy < 0 && x <= 0 || oldy > 0 && x >= 0);
        if (y > 0)
        {
          if (y % 3 == 0)
          {
            x++;
          }
          y--;
        }
        else
        {
          if (y % 4 == 0)
          {
            x--;
          }
          y++;
        }
        Contract.Assert(oldy < 0 && x <= 0 || oldy > 0 && x >= 0);
      }
      return x;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 75, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"assert unproven", PrimaryILOffset = 171, MethodILOffset = 0)]
    //[RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 171, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 34, MethodILOffset = 197)]
    public int Loop_Ok(int y)
    {
      Contract.Ensures(y >= 0 && Contract.Result<int>() >= 0 || y < 0 && Contract.Result<int>() <= 0);

      int oldy = y;
      int x = 0;
      while (y != 0)
      {
        Contract.Assert(oldy < 0 && x <= 0 || oldy > 0 && x >= 0);
        if (y > 0)
        {
          if (y % 3 == 0)
          {
            x++;
          }
          y--;
        }
        else
        {
          if (y % 4 == 0)
          {
            x--;
          }
          y++;
        }
        Contract.Assert(oldy < 0 && x <= 0 || oldy > 0 && x >= 0);
      }
      return x;
    }
  }
}

namespace ExamplesFromPapers
{
  class SriramSankaEtAl
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 103, MethodILOffset = 0)]
    public void SriramPOPL10(int[] p, int pLen, bool mode)
    {
      int o = 1, L = 1, bLen = 0;
      if (pLen < 1)
        return;
      if (p == null)
	{
	  pLen = -1;
	}
      if (mode)
	{
	  o = 1;
	}
      else
	{
	  o = 0;
	}
      while (L <= pLen)
	{
	  if (o > 0)
	    {
	      bLen = L - o;
	    }
	  L = 2 * L;
	}
      Contract.Assert(p == null || bLen <= pLen);
    }
    
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"ensures is valid",PrimaryILOffset=20,MethodILOffset=87)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"ensures is valid",PrimaryILOffset=43,MethodILOffset=87)]
    public static int F90(int n)
    {
      Contract.Ensures(n <= 100 || Contract.Result<int> () == n - 10);
      Contract.Ensures(n > 100 || Contract.Result<int>() == 91);
      if(n > 100)
	{
	  return n-10;
	}
      else
	{
	  return F90(F90(n+1));
	}
    }
  }
}

#pragma warning disable 0184 

namespace Types
{
  interface A
  {
    void foo();
  }

  [ContractClass(typeof(BContracts))]
  interface B
  {
    object moo();
  }

  [ContractClassFor(typeof(B))]
  abstract class BContracts : B 
  {

    #region B Members

    object B.moo()
    {
      Contract.Ensures(!(this is A) || Contract.Result<object>() == this);

      throw new NotImplementedException();
    }

    #endregion
  }

  // The numerical abstract domain should reason about "!(this is A)" to prove the assertions
  sealed class myClass : B
  {
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"ensures is valid",PrimaryILOffset=20,MethodILOffset=10)]
    public object moo()
    {
      return "ciao"; // True as the class is sealed
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=5,MethodILOffset=0)]
    public void AssertType()
    {
      Contract.Assert(!(this is A));  // True as the class is sealed
    }
  }

  class myClassNotSealed : B
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"ensures unproven: !(this is A) || Contract.Result<object>() == this",PrimaryILOffset=20,MethodILOffset=10)]
    public object moo()
    {
      return "Hello";
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"assert unproven",PrimaryILOffset=13,MethodILOffset=0)]
    public void AssertType()
    {
      Contract.Assert(!(this is A)); // Top
    }
  }
}