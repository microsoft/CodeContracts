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

namespace ObjectInvariantInference
{

  public class ReadonlyTest
  {
    readonly private int[] objs;
    
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="The length of the array may be negative",PrimaryILOffset=8,MethodILOffset=0)]
    [RegressionOutcome("Contract.Requires(0 <= len);")]
    [RegressionOutcome("Contract.Ensures(len == this.objs.Length);")]
    [RegressionOutcome("Contract.Ensures(Contract.ForAll(0, len, __k__ => this.objs[__k__] == 0));")]
	[RegressionReanalysisCount(1)] // Reanalyze the method once, because the first time is not scheduled for analysis...
    public ReadonlyTest(int len)
    {
      this.objs = new int[len];
    }
    
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Array access might be below the lower bound",PrimaryILOffset=7,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Array access might be above the upper bound",PrimaryILOffset=7,MethodILOffset=0)]
    // We infer it. Hence no warning is swown
    //[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible use of a null array 'this.objs'",PrimaryILOffset=7,MethodILOffset=0)]
    [RegressionOutcome("Contract.Assume(z < this.objs.Length);")]
    [RegressionOutcome("Contract.Requires(0 <= z);")]
    [RegressionOutcome("Contract.Ensures(this.objs != null);")]
    [RegressionOutcome("Contract.Ensures((z - this.objs.Length) < 0);")]
    [RegressionOutcome("Contract.Ensures(1 <= this.objs.Length);")]
    [RegressionOutcome("Contract.Invariant(this.objs != null);")]
    [RegressionOutcome("Contract.Ensures(this.objs[z] == Contract.Result<System.Int32>());")]
    public int BadCode(int z)
    {
      return objs[z];
    }
  }

  public class NoReadonlyTest
  {
    private int[] objs;
    
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="The length of the array may be negative",PrimaryILOffset=8,MethodILOffset=0)]
    [RegressionOutcome("Contract.Requires(0 <= len);")]
    [RegressionOutcome("Contract.Ensures(this.objs != null);")] // not suggested in the constructor above because we inferred it as object invariant
    [RegressionOutcome("Contract.Ensures(len == this.objs.Length);")]
    [RegressionOutcome("Contract.Ensures(Contract.ForAll(0, len, __k__ => this.objs[__k__] == 0));")]
	[RegressionOutcome("Consider adding an object invariant Contract.Invariant(objs != null); to the type NoReadonlyTest")]
    public NoReadonlyTest(int len)
    {
      this.objs = new int[len];
    }
    
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Array access might be below the lower bound",PrimaryILOffset=7,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Array access might be above the upper bound",PrimaryILOffset=7,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible use of a null array 'this.objs'",PrimaryILOffset=7,MethodILOffset=0)]
    [RegressionOutcome("Contract.Assume(this.objs != null);")]
    [RegressionOutcome("Contract.Assume(z < this.objs.Length);")]
    [RegressionOutcome("Contract.Requires(0 <= z);")]
    [RegressionOutcome("Contract.Ensures(this.objs != null);")]
    [RegressionOutcome("Contract.Ensures((z - this.objs.Length) < 0);")]
    [RegressionOutcome("Contract.Ensures(1 <= this.objs.Length);")]
    [RegressionOutcome("Contract.Ensures(this.objs[z] == Contract.Result<System.Int32>());")]
    public int BadCode(int z)
    {
      return objs[z];
    }
  }

  public class NoReadonlyTestWithStrings
  {
    private string[] objs;
    
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="The length of the array may be negative",PrimaryILOffset=8,MethodILOffset=0)]
    [RegressionOutcome("Contract.Requires(0 <= len);")]
    [RegressionOutcome("Contract.Ensures(this.objs != null);")] // not suggested in the constructor above because we inferred it as object invariant
    [RegressionOutcome("Contract.Ensures(len == this.objs.Length);")]
    [RegressionOutcome("Contract.Ensures(Contract.ForAll(0, len, __k__ => this.objs[__k__] == null));")]
    [RegressionOutcome("Consider adding an object invariant Contract.Invariant(objs != null); to the type NoReadonlyTestWithStrings")]
	public NoReadonlyTestWithStrings(int len)
    {
      this.objs = new string[len];
    }
    
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Array access might be below the lower bound",PrimaryILOffset=7,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Array access might be above the upper bound",PrimaryILOffset=7,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible use of a null array 'this.objs'",PrimaryILOffset=7,MethodILOffset=0)]
    [RegressionOutcome("Contract.Assume(this.objs != null);")]
    [RegressionOutcome("Contract.Assume(z < this.objs.Length);")]
    [RegressionOutcome("Contract.Requires(0 <= z);")]
    [RegressionOutcome("Contract.Ensures(this.objs != null);")]
    [RegressionOutcome("Contract.Ensures((z - this.objs.Length) < 0);")]
    [RegressionOutcome("Contract.Ensures(1 <= this.objs.Length);")]
    public string BadCode(int z)
    {
      return objs[z];
    }
  }

  public class InstallObjInvariants
  {
    readonly object y;

    [ClousotRegressionTest]
	[RegressionReanalysisCount(1)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="invariant unproven: this.y != null",PrimaryILOffset=0,MethodILOffset=13)]
    [RegressionOutcome("Contract.Requires(x != null);")]
    [RegressionOutcome("Contract.Ensures(x == this.y);")]
    public InstallObjInvariants(object x)
    {
      y = x;
    }

    [ClousotRegressionTest]
    // we infer the invariant, we now it is sufficient, so we do not emit the warning
    //[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possibly calling a method on a null reference 'this.y'",PrimaryILOffset=6,MethodILOffset=0)]
    [RegressionOutcome("Contract.Ensures(this.y != null);")]
    [RegressionOutcome("Contract.Ensures(Contract.Result<System.Int32>() == this.y.GetHashCode());")]
    [RegressionOutcome("Contract.Invariant(this.y != null);")]
    public int Foo()
    {
      return y.GetHashCode();
    }

    [ClousotRegressionTest]
#if SLICING
	// We do not re-use the inferred invariant, as on deserialization we only install it to the constructor
    [RegressionOutcome("Contract.Ensures(this.y != null);")]
    [RegressionOutcome("Contract.Invariant(this.y != null);")]
#else
    // We infer the object invariant y != null in the method above. As a consequence, no warning is generated here
#endif
    [RegressionOutcome("Contract.Ensures(Contract.Result<System.String>() != null);")]
    [RegressionOutcome("Contract.Ensures(this.y.ToString() != null);")]
    [RegressionOutcome("Contract.Ensures(Contract.Result<System.String>() == this.y.ToString());")]
    public string FooString()
    {
      return y.ToString();
    }
  }
  
  public class SegmentInfo{ }
  
  public class RequestDescription
  {
     private readonly SegmentInfo[] segmentInfos;
  
    [ClousotRegressionTest]
#if CLOUSOT2
//    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="invariant unproven: 0 <= (this.segmentInfos.Length - 1)",PrimaryILOffset=0,MethodILOffset=13)]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="invariant unproven: 0 <= (this.segmentInfos.Length - 1). Is it an off-by-one? The static checker can prove (0 - 1) <= (segmentInfos.Length - 1) instead",PrimaryILOffset=0,MethodILOffset=13)]
#else
//    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="invariant unproven: 0 <= (this.segmentInfos.Length - 1)",PrimaryILOffset=6,MethodILOffset=13)]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="invariant unproven: 0 <= (this.segmentInfos.Length - 1). Is it an off-by-one? The static checker can prove (0 - 1) <= (segmentInfos.Length - 1) instead",PrimaryILOffset=6,MethodILOffset=13)]
#endif
    [RegressionOutcome("Contract.Requires(0 <= (segmentInfos.Length - 1));")]
    [RegressionOutcome("Contract.Ensures(segmentInfos == this.segmentInfos);")]
    public RequestDescription(SegmentInfo[] segmentInfos)
    {
      this.segmentInfos = segmentInfos;
    }

    public SegmentInfo LastSegmentInfo
    {
      // We were not deducing that IsReadonly(this.segmentInfos)  ==> IsReadonly(this.segmentInfos.Length)
      [ClousotRegressionTest]
      // We infer the object invariants, so we do not emit the warning
      //[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Array access might be below the lower bound",PrimaryILOffset=16,MethodILOffset=0)]
      //[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible use of a null array 'this.segmentInfos'",PrimaryILOffset=12,MethodILOffset=0)]
      [RegressionOutcome("Contract.Ensures(this.segmentInfos != null);")]
      [RegressionOutcome("Contract.Ensures(1 <= this.segmentInfos.Length);")]
      [RegressionOutcome("Contract.Invariant(this.segmentInfos != null);")]
      [RegressionOutcome("Contract.Invariant(0 <= (this.segmentInfos.Length - 1));")]
      get
      {
        return this.segmentInfos[this.segmentInfos.Length - 1];
      }
    }
  } 

  public class ReadOnly_FilterFalseRequires
  {
    readonly string str;
    int counter = 0;

	[ClousotRegressionTest]
	[RegressionReanalysisCount(1)]
	[RegressionOutcome(Outcome=ProofOutcome.False,Message="invariant is false: this.str != null. This object invariant was inferred, and it should hold in order to avoid an error if the method UseStr (in the same type) is invoked",PrimaryILOffset=0,MethodILOffset=13)]
    public ReadOnly_FilterFalseRequires(int z)
    {
      this.counter = 0;
    }

	[ClousotRegressionTest]
	[RegressionOutcome("Contract.Ensures(Contract.Result<System.String>() != null);")]
	[RegressionOutcome("Contract.Ensures(this.str != null);")]
	[RegressionOutcome("Contract.Ensures(this.str.ToString() != null);")]
	[RegressionOutcome("Contract.Ensures(0 <= Contract.Result<System.String>().Length);")]
	[RegressionOutcome("Contract.Ensures(0 <= this.str.ToString().Length);")]
	[RegressionOutcome("Contract.Invariant(this.str != null);")]
    public string UseStr()
    {
      return this.str.ToString() + "hello";
    }

	[ClousotRegressionTest]
	[RegressionOutcome("Contract.Ensures(this.counter - Contract.OldValue(this.counter) == 1);")]
	[RegressionOutcome("Contract.Ensures(-2147483647 <= this.counter);")]
    public void Add()
    {
      counter++;
    }
  }
  
  public class UseReadonly
  {
	[ClousotRegressionTest]
    public static void Act()
    {
      var z = new ReadOnly_FilterFalseRequires(12);
      z.Add();
    }
  }
  
  public class Bits
  {
	[ClousotRegressionTest]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="assert unproven. Is it an off-by-one? The static checker can prove (((x >> 28) & 15)) < (15 + 1) instead",PrimaryILOffset=11,MethodILOffset=0)] // ???? Why can it prove < 16 ???
	[RegressionOutcome("Contract.Requires((((x >> 28) & 15)) < 15);")]
    static public void BitManipulation(int x)
    {
      Contract.Assert((x >> 28 & 15) < 15); // not true
    }

	[ClousotRegressionTest]
    static public void BitManipulation_Fixed(int x)
    {
      if (x >= 0)
      {
        Contract.Assert((x >> 28 & 15) < 15); // Should prove it
      }
    }
  }
  
  public class EnumFiltering
  {
    public enum MyEnum { A, B}

	[ClousotRegressionTest] 
	[RegressionOutcome("Contract.Ensures(97 <= Contract.Result<System.Char>());")]
	[RegressionOutcome("Contract.Ensures(Contract.Result<System.Char>() <= 98);")]
    public static char EnumCase(MyEnum me)
    {
      if(me == MyEnum.A)
      {
        return 'a';
      } else if(me == MyEnum.B) // Should not warn
      {
        return 'b';
      }
      else
      {
        Contract.Assert(false);
        throw new Exception();
      }
    }

	[ClousotRegressionTest]
	[RegressionOutcome("Contract.Ensures(97 <= Contract.Result<System.Char>());")]
	[RegressionOutcome("Contract.Ensures(Contract.Result<System.Char>() <= 98);")]
    public static char IntCase(int me)
    {
      Contract.Requires(me >= 0);
      Contract.Requires(me <= 1);
      if (me == 0)
      {
        return 'a';
      }
      else if (me == 1) // Should warn
      {
        return 'b';
      }
      else
      {
        Contract.Assert(false);
        throw new Exception();
      }
    }
  }
}

namespace ImprovedMessages
{
  public class BetterWarningMessagesTest
  {
    readonly string str;
    int counter = 0;

	[ClousotRegressionTest]
	[RegressionReanalysisCount(1)]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="invariant unproven: this.str != null",PrimaryILOffset=0,MethodILOffset=42)]
	[RegressionOutcome("Contract.Requires(inputString != null);")]
	[RegressionOutcome("Contract.Ensures(inputString == this.str);")]
	[RegressionOutcome("Contract.Ensures((Contract.OldValue(this.counter) - this.counter) <= 0);")]
	[RegressionOutcome("Contract.Ensures(0 <= this.counter);")]
	[RegressionOutcome("Contract.Ensures(this.counter <= 1);")]
    public BetterWarningMessagesTest(string inputString, int counter)
    {
      this.counter = 0;

      this.str = inputString;
      if(this.str == null)
      {
        this.counter++;
      }
    }

	[ClousotRegressionTest]
	[RegressionOutcome("Contract.Ensures(Contract.Result<System.String>() != null);")]
	[RegressionOutcome("Contract.Ensures(this.str != null);")]
	[RegressionOutcome("Contract.Ensures(this.str.ToString() != null);")]
	[RegressionOutcome("Contract.Ensures(0 <= Contract.Result<System.String>().Length);")]
	[RegressionOutcome("Contract.Ensures(0 <= this.str.ToString().Length);")]
	[RegressionOutcome("Contract.Invariant(this.str != null);")]
    public string UseStr()
    {
      return this.str.ToString() + "hello";
    }

	[ClousotRegressionTest]
	[RegressionOutcome("Contract.Ensures(this.counter - Contract.OldValue(this.counter) == 1);")]
	[RegressionOutcome("Contract.Ensures(-2147483647 <= this.counter);")]
    public void Add()
    {
      counter++;
    }

	[ClousotRegressionTest]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Array access might be above the upper bound",PrimaryILOffset=2,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible use of a null array 'a'",PrimaryILOffset=2,MethodILOffset=0)]
	[RegressionOutcome("Contract.Requires(a != null);")]
	[RegressionOutcome("Contract.Requires(0 < a.Length);")]
	[RegressionOutcome("Contract.Ensures(a[0] == Contract.Result<System.Int32>());")]
    public static int Count(int[] a)
    {
      return a[0];
    }

	[ClousotRegressionTest]
    public static int TwoLevelsDown(int[] a)
    {
      return Count(a);
    }
  }
}

namespace ReproWithStructs
{
  internal struct STR
  {
    readonly private string S;

	[ClousotRegressionTest]
    // We should not suggest (does not compiler), but it is ok to infer:
 	//	Contract.Ensures(s == this.S);
	public STR(string s)
    {
      this.S = s;
    }

	// It should not suggest an object invariant!
	[ClousotRegressionTest]
	[RegressionOutcome("Contract.Ensures(this.S != null);")]
	[RegressionOutcome("Contract.Ensures(Contract.Result<System.Int32>() == this.S.Length);")]
	[RegressionOutcome("Contract.Ensures(0 <= Contract.Result<System.Int32>());")]
	// It should not suggest this (does not compile)
	// 		Contract.Ensures(Contract.Result<System.Int32>() == Contract.ValueAtReturn(out this.S.Length));     
    public int Len()
    {
      return this.S.Length;
    }
  }
}