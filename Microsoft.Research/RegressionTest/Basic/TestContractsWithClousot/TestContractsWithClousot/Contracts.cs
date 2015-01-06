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
using System.Diagnostics.Contracts;
using Microsoft.Research.ClousotRegression;

namespace UIntOverflowRepro
{
  // Repro for a bug signaled by MAF
  public class Convert
  {
    public static uint ToUInt32(long value)
    {
      if (value < 0 || value > UInt32.MaxValue)
        throw new Exception();

      Contract.EndContractBlock();

      return (uint)value;
    }

    [ClousotRegressionTest]
#if CLOUSOT2
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"requires unproven: value >= 0 && value <= UInt32.MaxValue", PrimaryILOffset = 5, MethodILOffset = 1)]
#else
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"requires unproven: value >= 0 && value <= UInt32.MaxValue", PrimaryILOffset = 15, MethodILOffset = 1)]
#endif
    uint IConvertible_ToUInt32(long m_value)
    {
      return Convert.ToUInt32(m_value);
    }
  }
  
  public class Convert2
  {
    public static uint ToUInt32(long value)
    {
      if (value < 0)
        throw new Exception();
       
      Contract.EndContractBlock();

      return (uint)value;
    }

    [ClousotRegressionTest]
#if CLOUSOT2
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"requires unproven: value >= 0", PrimaryILOffset = 3, MethodILOffset = 1)]
#else
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"requires unproven: value >= 0", PrimaryILOffset = 10, MethodILOffset = 1)]
#endif
    uint IConvertible_ToUInt32(long m_value)
    {
      return Convert2.ToUInt32(m_value);
    }
  }

}

namespace EnsuresRepro
{
  class Program
  {

    int count;

    public int Count
    {
      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 13, MethodILOffset = 24)]
      get
      {
        Contract.Ensures(Contract.Result<int>() == this.count);
        return this.count;
      }
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 35, MethodILOffset = 83)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 61, MethodILOffset = 83)]
    public int Add()
    {
      Contract.Requires(Count >= 0);

      Contract.Ensures(Contract.Result<int>() == Contract.OldValue(Count));
      Contract.Ensures(Count == Contract.OldValue(Count) + 1);

      return count++;
    }

    public int Add2()
    {
      Contract.Ensures(Contract.Result<int>() == Contract.OldValue(Count));

      return count++;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 35, MethodILOffset = 83)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 61, MethodILOffset = 83)]
    private int Add1()
    {
      Contract.Requires(count >= 0);
      
      Contract.Ensures(Contract.Result<int>() == Contract.OldValue(count));
      Contract.Ensures(count == Contract.OldValue(count) + 1);

      return count++;
    }
    
  }
}

namespace RelationsAfterLoops
{
  class Test
  {
    // OK
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 22, MethodILOffset = 0)]
    public static void Equality_AfterLoop_OK(int[] arr)
    {
      int i = 0;
      int k = arr.Length;

      // infer the loop invariant i  <= k 
      for (; i < k; i++)
      {
      }

      Contract.Assert(i == arr.Length);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 28, MethodILOffset = 0)]
    public static void Equality_AfterLoop_OK(int len)
    {
      Contract.Requires(len >= 0);

      int i = 0;

      for (; i < len; i++)
      {
      }

      Contract.Assert(i == len);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 20, MethodILOffset = 0)]
    public static void Equality_AfterLoop_WithArrays(int[] arr)
    {
      int i = 0;

      for (; i < arr.Length; i++)
      {
      }

      Contract.Assert(i == arr.Length);
    }

    // If arr.Length == 1, then the assertion does not hold
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"assert unproven", PrimaryILOffset = 27, MethodILOffset = 0)]
    public static void LEQ_Wrong(int[] arr)
    {
      int i = 0;
      int count = 0;

      for (; i < arr.Length; i++)
      {
        count += i;
      }

      Contract.Assert(i <= count);
    }

    // ok
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 27, MethodILOffset = 0)]
    public static void LEQ_OK(int[] arr)
    {
      int i = 0;
      int count = 1;

      // It's proven by the wp 
      // Otherwise, Subpoly with infoct do the job
      for (; i < arr.Length; i++)
      {
        count += i;
      }

      Contract.Assert(i <= count);
    }
  }
}

namespace BankAccount
{
  public sealed class Account
  {
    [ContractPublicPropertyName("Value")]
    private int value = 0;

    [Pure]
    public int Value { get { return value; } }

    [ContractInvariantMethodAttribute]
    private void ObjectInvariant()
    {
      Contract.Invariant(value >= 0);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"invariant is valid", PrimaryILOffset = 12, MethodILOffset = 52)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 33, MethodILOffset = 52)]
    public void Dispose(int amount)
    {
      Contract.Requires(amount >= 0);
      Contract.Ensures(value == Contract.OldValue<int>(value) + amount);

      value += amount;
    }

    [ClousotRegressionTest("cci1only")] // The reference to "value" in the precondition gets turned into a call on the getter, which for some reason is not inlined?
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"invariant is valid", PrimaryILOffset = 12, MethodILOffset = 71)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 52, MethodILOffset = 71)]
    public void Withdraw(int amount)
    {
      Contract.Requires(amount >= 0);
      Contract.Requires(value - amount >= 0);
      Contract.Ensures(value == Contract.OldValue<int>(value) - amount);

      value -= amount;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 7, MethodILOffset = 157)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 26, MethodILOffset = 157)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 7, MethodILOffset = 164)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"invariant is valid", PrimaryILOffset = 12, MethodILOffset = 169)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset=98,MethodILOffset=169)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 124, MethodILOffset = 169)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 150, MethodILOffset = 169)]
    public void TransferTo(int amount, Account a)
    {
      Contract.Requires(a != null);
      Contract.Requires(a.value >= 0);
      Contract.Requires(amount >= 0);
      Contract.Requires(value - amount >= 0);

      Contract.Ensures((Contract.OldValue<int>(value) + Contract.OldValue<int>(a.value)) == (value + a.value));

      Contract.Ensures(a.value == Contract.OldValue<int>(a.value) + amount);
      Contract.Ensures(value == Contract.OldValue<int>(value) - amount);

      Withdraw(amount);
      a.Dispose(amount);
    }
  }

}

namespace BugsFixedFromUsers
{
  public class NotEqInPostcondition
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 11, MethodILOffset = 19)]
    public static int Mike(int b)
    {
      Contract.Ensures(Contract.Result<int>() != b);

      return b + 1;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 11, MethodILOffset = 25)]
    public static int Adjust(int b, bool direction)
    {
      Contract.Ensures(Contract.Result<int>() != b);

      return b + (direction ? 1 : -1);
    }
  }
}

namespace PreconditionAsExtension
{
  namespace tryExtension
  {
    static class Extension
    {
      [ClousotRegressionTest]
      [Pure]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 11, MethodILOffset = 20)]
      static public bool IsPositive(this int arg)
      {
        Contract.Ensures(Contract.Result<bool>() == (arg > 0));
        return arg > 0;
      }
    }

    class Try
    {
      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 24, MethodILOffset = 0)]
      public Try(int x)
      {
        Contract.Requires(x.IsPositive());

        Contract.Assert(x >= 0);
      }
    }
  }
}

namespace PeliHeap
{
  namespace minirepro
  {
    class PeliHeap
    {
      private int count;

      public int Count
      {
        get { return this.count; }
      }

      public PeliHeap(int c)
      {
        this.count = c;
      }

      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 4, MethodILOffset = 36)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 18, MethodILOffset = 36)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 27, MethodILOffset = 36)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 41, MethodILOffset = 36)]
      private void MinHeapifyDown(int start)
      {
        Contract.Requires(-1 < start);
        Contract.Requires(start < this.Count);

        int i = start;
        int j = (i - 1) / 2;

        while (i > 0)
        {
          this.Less(i, j);

          i = j;
          j = (i - 1) / 2;
        }
      }

      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 42, MethodILOffset = 0)]
      private void MinHeapifyDown_WithAssert(int start)
      {
        Contract.Requires(-1 < start);
        Contract.Requires(start < this.Count);

        int i = start;
        int j = (i - 1) / 2;

        while (i > 0)
        {
          Contract.Assert(i < this.Count);
          
          i = j;
          j = (i - 1) / 2;
        }
      }

      [Pure]
      private bool Less(int i, int j)
      {
        Contract.Requires(-1 < i);
        Contract.Requires(i < this.Count);
        Contract.Requires(-1 < j);
        Contract.Requires(j < this.Count);

        return true;
      }
    }
  }
}

namespace Arithmetic
{
  class Bugrepro
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 26, MethodILOffset = 0)]
    internal static void TestBitwiseOr_NoLoop(int min)
    {
      Contract.Requires((min >= 0), "(min >= 0)");

      Contract.Assert((min | 1) >= min);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 42, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 28, MethodILOffset = 71)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 28, MethodILOffset = 57)]
    internal static int TestBitwiseOr(int min, bool b)
    {
      Contract.Requires((min >= 0), "(min >= 0)");
      Contract.Ensures(Contract.Result<int>() >= min);

      Contract.Assert((min | 1) >= min);

      for (int i = (min | 1); i < Int32.MaxValue; i += 2)
      {
        if (b)
          return i;
      }

      return min;
    }
  }
}

namespace Benigni
{
  public class Benigni
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"ensures is valid",PrimaryILOffset=32,MethodILOffset=61)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"ensures is valid",PrimaryILOffset=49,MethodILOffset=61)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"ensures unproven: Contract.Result<int>() <= x", PrimaryILOffset = 32, MethodILOffset = 108)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 49, MethodILOffset = 108)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"assert unproven",PrimaryILOffset=75,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=95,MethodILOffset=0)]
    public int DivRem(int x, int y, out int rem)
    {
      Contract.Requires(y > 0);
      Contract.Requires(x >= 0);

      // Cannot prove this postcondition, as it depends on arithmetic properties 
      Contract.Ensures(Contract.Result<int>() <= x);
      Contract.Ensures(Contract.ValueAtReturn(out rem) <= x);

      if (x == 0)
      {
        rem = 0;
        return 0;
      }

      int z = 0;
      while (x >= y)
      {
        // Cannot prove this assertion, as it involves a quadratic invariant
        Contract.Assert(x * z <= y);
        var oldX = x;
        x = x - y;
        z++;

        Contract.Assert(x < oldX);
      }

      rem = x;
      return z;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"ensures is valid",PrimaryILOffset=21,MethodILOffset=48)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"ensures is valid",PrimaryILOffset=35,MethodILOffset=48)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"ensures is valid",PrimaryILOffset=21,MethodILOffset=93)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"ensures is valid",PrimaryILOffset=35,MethodILOffset=93)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=76,MethodILOffset=0)]
    public int DivRem2(int x, int y, out int rem)
    {
      Contract.Requires(y > 0);
      Contract.Ensures(Contract.ValueAtReturn(out rem) <= x);
      Contract.Ensures(Contract.ValueAtReturn(out rem) < y);

      if (x <= 0)
      {
        rem = x;
        return 0;
      }
      int z = 0;

      while (x >= y)
      {
        var oldX = x;
        int u = y;

        while (u > 0)
        {
          x--;
          u--;
        }

        Contract.Assert(x < oldX);
        z++;
      }

      rem = x;
      return z;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"ensures is valid",PrimaryILOffset=21,MethodILOffset=48)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"ensures is valid",PrimaryILOffset=35,MethodILOffset=48)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"ensures is valid",PrimaryILOffset=21,MethodILOffset=93)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"ensures is valid",PrimaryILOffset=35,MethodILOffset=93)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=76,MethodILOffset=0)]
    public int DivRem3(int x, int y, out int rem)
    {
      Contract.Requires(y > 0);
      Contract.Ensures(Contract.ValueAtReturn(out rem) <= x);
      Contract.Ensures(Contract.ValueAtReturn(out rem) < y);

      if (x <= 0)
      {
        rem = x;
        return 0;
      }

      int z = 0;
      while (x >= y)
      {
        var oldX = x;
        for (int u = 0; u < y; u++)
          x--;

        Contract.Assert(x < oldX);
        z++;
      }

      rem = x;
      return z;
    }
  }
}

namespace BugsFromForum
{
  public class Wurz0
  {
    // Here we add the inference rule that if arr1 == arr2 then arr1.Length == arr2.Length
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"Array creation : ok", PrimaryILOffset = 5, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 27, MethodILOffset = 0)]
    internal void ArrayError(int z)
    {
      int lngCount = 0;

      int[] tmp = new int[lngCount + 1];

      int[] chkAttribute = Id(tmp);

      // Use Id postcondition
      Contract.Assert(chkAttribute.Length == tmp.Length);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 8, MethodILOffset = 14)]
    private int[] Id(int[] dest)
    {
      Contract.Ensures(Contract.Result<int[]>() == dest);

      return dest;
    }

  }
}

namespace Brian
{
  public class Brian0
  {
    public static void SomeMethod(byte displayOrder)
    {
      Contract.Requires(IsValidDisplayOrder(displayOrder));
    }

    [Pure]
    public static bool IsValidDisplayOrder(byte value)
    {
      // Customer reports that the first two of these forms cause the static checker to complain that the
      // precondition is not proven (when you pass in 0).  The third works - if you explicitly test for 0.
      //Contract.Ensures(Contract.Result<bool>() == !(value < 0 || value > 19));

      Contract.Ensures(Contract.Result<bool>() == (value >= 0 && value <= 19));

      //Contract.Ensures(Contract.Result<bool>() == ((value == 0) || (value > 0 && value <= 19));

      return (value >= 0 && value <= 19);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 6, MethodILOffset = 1)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 6, MethodILOffset = 7)]
    static void Main(string[] args)
    {
      // Should be valid.
      SomeMethod(0);
      SomeMethod(1);
    }
  }
}

namespace Doubles
{
  public class MathExamples
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"assert unproven",PrimaryILOffset=50,MethodILOffset=0)]
    public void PositiveInfintySum(bool b)
    {
      var pInfty = Double.PositiveInfinity;
      var z = b? 1.0 : 2.0;

      var sum = pInfty + z;
      
      Contract.Assert(sum == Double.PositiveInfinity); // Cannot prove with -bounds as they are not tracking doubles
    }
    
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"assert unproven",PrimaryILOffset=50,MethodILOffset=0)]
    public void PositiveInfintySub(bool b)
    {
      var pInfty = Double.PositiveInfinity;
      var z = b ? 1.0 : 2.0;
      
      var sum = pInfty - z;

      Contract.Assert(sum == Double.PositiveInfinity);  // Cannot prove with -bounds as they are not tracking doubles
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"assert unproven",PrimaryILOffset=50,MethodILOffset=0)]
    public void NegativeInfintyAdd(bool b)
    {
      var pInfty = Double.NegativeInfinity;
      var z = b ? 1.0 : 2.0;

      var sum = pInfty + z;

      Contract.Assert(sum == Double.NegativeInfinity);  // Cannot prove with -bounds as they are not tracking doubles
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"assert unproven",PrimaryILOffset=50,MethodILOffset=0)]
    public void NegativeInfintySub(bool b)
    {
      var pInfty = Double.NegativeInfinity;
      var z = b ? 1.0 : 2.0;

      var sum = pInfty - z;

      Contract.Assert(sum == Double.NegativeInfinity);  // Cannot prove with -bounds as they are not tracking doubles
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=50,MethodILOffset=0)]
    public void NaNAdd(bool b)
    {
      var nan = Double.NaN;
      var z = b ? 1.0 : 2.0;

      var sum = nan + z;

      Contract.Assert(nan.Equals(Double.NaN));  // prove it thanks to the constant propagation of the heap analysis
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=40,MethodILOffset=0)]
    public void NaNAdd_UsingIsNaN(bool b)
    {
      var nan = Double.NaN;
      var z = b ? 1.0 : 2.0;

      var sum = nan + z;

      Contract.Assert(Double.IsNaN(nan));  // Cannot prove with -bounds as they are not tracking doubles
    }
  }
}