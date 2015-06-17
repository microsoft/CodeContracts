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
using System.Diagnostics.Contracts;
using Microsoft.Research.ClousotRegression;

namespace TestPostconditionInference
{
  public class TestRangesAreFiltered
  {
    [ClousotRegressionTest]
    [RegressionOutcome("Contract.Ensures(Contract.Result<System.Int32>() == x);")]
    public static int IntPostcondition(int x)
    {
      return x;
    }

    [ClousotRegressionTest]
    [RegressionOutcome("Contract.Ensures(Contract.Result<System.Int64>() == x);")]
    public static long LongPostcondition(long x)
    {
      return x;
    }

    [ClousotRegressionTest]
    [RegressionOutcome("Contract.Ensures(Contract.Result<System.UInt32>() == u);")]
    public static UInt32 UInt32Postcondition(uint u)
    {
      return u;
    }

    [ClousotRegressionTest]
    [RegressionOutcome("Contract.Ensures(Contract.Result<System.UInt64>() == u);")]
    public static UInt64 UInt64Postcondition(UInt64 u)
    {
      return u;
    }

    [ClousotRegressionTest]
    [RegressionOutcome("Contract.Ensures(Contract.Result<System.SByte>() == s);")]
    public static SByte SBytePostcondition(SByte s)
    {
      return s;
    }
	
    public long _field;

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=2,MethodILOffset=0)]
    [RegressionOutcome("Contract.Ensures(z == this._field);")]
    public void SetField(long z)
    {
      this._field = z;
    }		
  }

   public class FilterRedundantPostcondition
   {
    public string applicationVersion;

    public string ApplicationVersion
    {
      [ClousotRegressionTest]
	  [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=22,MethodILOffset=0)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=6,MethodILOffset=27)]
	  [RegressionOutcome(Outcome=ProofOutcome.True,Message="ensures is valid",PrimaryILOffset=16,MethodILOffset=27)]
      get
      {
        Contract.Ensures(Contract.Result<string>() == applicationVersion);
        return applicationVersion;
      }
    }

    extern string GetApplicationVersion();

  }
  
  public  class TimeSpan
  {
    public long _ticks;
 
    public double TotalDays
    {
      // We should emit a range, as this implies that the result is not NaN
      // Also the range is the one of longs, which is included in double
      [ClousotRegressionTest]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=1,MethodILOffset=0)]
      [RegressionOutcome("Contract.Ensures(Contract.Result<System.Double>() == (double)(this._ticks) * 1.15740740740741E-12);")]
      [RegressionOutcome("Contract.Ensures(-9223372036854775808 <= Contract.Result<System.Double>());")]
      [RegressionOutcome("Contract.Ensures(Contract.Result<System.Double>() <= 9223372036854775807);")]
      get
      {
        return (this._ticks * 1.1574074074074074E-12);
      }
    }

    // Here we should not emit a range, as d may be NaN or +00 or -oo
    [ClousotRegressionTest]
    [RegressionOutcome("Contract.Ensures(Contract.Result<System.Double>() == d);")]
    public static double DoubleInPostcondition(double d)
    {
      return d;
    }
  }

#pragma warning disable 0626
  public class Array
  {
    extern int Length { get; }

    // We cast from an Int32, so we should get a smaller interval than the range of long
    public long LongLength
    {
      [ClousotRegressionTest]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as receiver)",PrimaryILOffset=1,MethodILOffset=0)]
      [RegressionOutcome("Contract.Ensures(Contract.Result<System.Int64>() == (long)(this.Length));")]
      [RegressionOutcome("Contract.Ensures(Contract.Result<System.Int64>() == this.Length);")]
      [RegressionOutcome("Contract.Ensures(-2147483648 <= Contract.Result<System.Int64>());")]
      [RegressionOutcome("Contract.Ensures(Contract.Result<System.Int64>() <= 2147483647);")]
      get
      {
        return (long)this.Length;
      }
    }
  }

  public struct TmpStruct
  {
    public int[] arr;
  }

  public class Tmp
  {
    public int[] arr;

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="Lower bound access ok",PrimaryILOffset=38,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="Upper bound access ok",PrimaryILOffset=38,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="Lower bound access ok",PrimaryILOffset=69,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="Upper bound access ok",PrimaryILOffset=69,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="Lower bound access ok",PrimaryILOffset=95,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="Upper bound access ok",PrimaryILOffset=95,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=47,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible use of a null array 't.arr'",PrimaryILOffset=52,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=32,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as array)",PrimaryILOffset=38,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible use of a null array 'tstruct.arr'. The static checker determined that the condition 'tstruct.arr != null' should hold on entry. Nevertheless, the condition may be too strong for the callers. If you think it is ok, add a precondition to document it: Contract.Requires(tstruct.arr != null);",PrimaryILOffset=84,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as array)",PrimaryILOffset=69,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as array)",PrimaryILOffset=104,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as array)",PrimaryILOffset=95,MethodILOffset=0)]
    // Should not suggest t.arr.Length >= 0 and tstruct.arr.Length >= 0
    [RegressionOutcome("Contract.Requires(t.arr != null);")]
    [RegressionOutcome("Contract.Ensures(t.arr != null);")]
    [RegressionOutcome("Contract.Ensures(tstruct.arr != null);")]
    public static int RemoveSimpleAccess(Tmp t, TmpStruct tstruct, int[] p)
    {
      Contract.Requires(t != null);
      Contract.Requires(p != null);

      int sum = 0;
      for (var i = 0; i < t.arr.Length; i++)
      {
        sum += t.arr[i];
      }

      for (var i = 0; i < tstruct.arr.Length; i++)
      {
        sum += tstruct.arr[i];
      }

      for (var i = 0; i < p.Length; i++)
      {
        sum += p[i];
      }

      return sum;
    }
  }

  public class Expression { }
  
  public class ExpandSegment
  {
    private readonly Expression filter;
 
    public ExpandSegment(Expression filter) 
    { 
     this.filter = filter;
    }

    public Expression Filter
    { 
      [ClousotRegressionTest]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=1,MethodILOffset=0)]
      [RegressionOutcome("Contract.Ensures(Contract.Result<TestPostconditionInference.Expression>() == this.filter);")]
      get
      {
        return this.filter;
      }
    }

    public bool HasFilter
    {
      // We used to infer Contract.Result<bool>() == ((this.filter == null) == 0))
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=1,MethodILOffset=0)]
      [RegressionOutcome("Contract.Ensures(Contract.Result<System.Boolean>() == ((this.Filter == null) == false));")]
      [RegressionOutcome("Contract.Ensures(this.Filter == this.filter);")]
      [ClousotRegressionTest]
      get
      {
        return (this.Filter != null);
      }
    }
  }
}