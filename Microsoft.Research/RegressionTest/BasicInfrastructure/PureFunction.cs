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

[assembly: RegressionOutcome("Detected call to method 'PureFunction.ImpureDelegate.Invoke(System.Int32)' without [Pure] in contracts of method 'PureFunction.DelegateTests.Test1(PureFunction.ImpureDelegate,System.Int32)'.")]
//[assembly: RegressionOutcome("Detected call to method 'PureFunction.ImpureDelegate.Invoke(System.Int32)' without [Pure] in contracts of method 'PureFunction.DelegateTests.Test5(System.Int32)'.")]
[assembly: RegressionOutcome("Detected call to method 'System.Comparison`1<System.Int32>.Invoke(System.Int32,System.Int32)' without [Pure] in contracts of method 'PureFunction.DelegateTests.Test3(System.Comparison`1<System.Int32>,System.Int32)'.")]
namespace PureFunction
{
  class C
  {
    [Pure]
    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 9, MethodILOffset = 20)]
    public int P()
    {
      Contract.Ensures(Contract.Result<int>() == 3);
      return 3;
    }
    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 2, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.False, Message = @"ensures is false: Contract.Result<int>() == 4", PrimaryILOffset = 24, MethodILOffset = 35)]
    public int M(int x)
    {
      Contract.Requires(P() == x);
      Contract.Ensures(Contract.Result<int>() == 4);
      return x;
    }

    [Pure]
    public int Binary(int x, int y)
    {
      return x + y;
    }

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 4, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 21, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 29, MethodILOffset = 0)]
    public void TestBinaryPredicate(int x, int y)
    {
      Contract.Requires(Binary(x, y) > 0);
      Contract.Assert(Binary(x, y) > 0);
    }


    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 4, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 44, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 52, MethodILOffset = 0)]
    public int TestBinaryPredicate2(int x, int y)
    {
      Contract.Requires(Binary(x, y) > 0);
      int z;
      if (x < y)
      {
        z = 55;
      }
      else
      {
        z = 77;
      }
      Contract.Assert(Binary(x, y) > 0);
      return z;
    }
  }
  class Tests
  {
    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 7, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 10, MethodILOffset = 7)]
    static void TestMain()
    {
      new C().M(3);
    }

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 25, MethodILOffset = 0)]
    static void TestBinary1(int x, int y)
    {
      Contract.Requires(x * y >= 0);
      Contract.Assert(x * y >= 0);
    }
    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 25, MethodILOffset = 0)]
    static void TestBinary2(int x, int y)
    {
      Contract.Requires(x / y >= 0);
      Contract.Assert(x / y >= 0);
    }
    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 25, MethodILOffset = 0)]
    static void TestBinary3(int x, int y)
    {
      Contract.Requires(x + y >= 0);
      Contract.Assert(x + y >= 0);
    }
    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 25, MethodILOffset = 0)]
    static void TestBinary4(int x, int y)
    {
      Contract.Requires(x - y >= 0);
      Contract.Assert(x - y >= 0);
    }
    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 25, MethodILOffset = 0)]
    static void TestBinary5(int x, int y)
    {
      Contract.Requires(x % y >= 0);
      Contract.Assert(x % y >= 0);
    }

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 29, MethodILOffset = 0)]
    static void TestBinary6(int x, int y)
    {
      Contract.Requires(-x * -y >= 0);
      Contract.Assert(-x * -y >= 0);
    }
    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 29, MethodILOffset = 0)]
    static void TestBinary7(int x, int y)
    {
      Contract.Requires(-x / -y >= 0);
      Contract.Assert(-x / -y >= 0);
    }
    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 29, MethodILOffset = 0)]
    static void TestBinary8(int x, int y)
    {
      Contract.Requires(-x + -y >= 0);
      Contract.Assert(-x + -y >= 0);
    }
    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 29, MethodILOffset = 0)]
    static void TestBinary9(int x, int y)
    {
      Contract.Requires(-x - -y >= 0);
      Contract.Assert(-x - -y >= 0);
    }
    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 29, MethodILOffset = 0)]
    static void TestBinary10(int x, int y)
    {
      Contract.Requires(-x % -y >= 0);
      Contract.Assert(-x % -y >= 0);
    }

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 33, MethodILOffset = 0)]
    static void TestBinary1(float x, float y)
    {
      Contract.Requires(x * y >= 0);
      Contract.Assert(x * y >= 0);
    }
    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 33, MethodILOffset = 0)]
    static void TestBinary2(float x, float y)
    {
      Contract.Requires(x / y >= 0);
      Contract.Assert(x / y >= 0);
    }
    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 33, MethodILOffset = 0)]
    static void TestBinary3(float x, float y)
    {
      Contract.Requires(x + y >= 0);
      Contract.Assert(x + y >= 0);
    }
    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 33, MethodILOffset = 0)]
    static void TestBinary4(float x, float y)
    {
      Contract.Requires(x - y >= 0);
      Contract.Assert(x - y >= 0);
    }
    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 33, MethodILOffset = 0)]
    static void TestBinary5(float x, float y)
    {
      Contract.Requires(x % y >= 0);
      Contract.Assert(x % y >= 0);
    }

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 39, MethodILOffset = 0)]
    static void TestBinary1(int x, int y, int z)
    {
      Contract.Requires(x * -y - z * (x - y) >= 0);
      Contract.Assert(x * -y - z * (x - y) >= 0);
    }
    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 39, MethodILOffset = 0)]
    static void TestBinary2(int x, int y, int z)
    {
      Contract.Requires(x / -y - z * (x - y) >= 0);
      Contract.Assert(x / -y - z * (x - y) >= 0);
    }
    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 39, MethodILOffset = 0)]
    static void TestBinary3(int x, int y, int z)
    {
      Contract.Requires(x + -y - z * (x - y) >= 0);
      Contract.Assert(x + -y - z * (x - y) >= 0);
    }
    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 39, MethodILOffset = 0)]
    static void TestBinary4(int x, int y, int z)
    {
      Contract.Requires(x - -y - z * (x - y) >= 0);
      Contract.Assert(x - -y - z * (x - y) >= 0);
    }
    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 39, MethodILOffset = 0)]
    static void TestBinary5(int x, int y, int z)
    {
      Contract.Requires(x % -y - z * (x - y) >= 0);
      Contract.Assert(x % -y - z * (x - y) >= 0);
    }

  }

  class PropertySetAndGet
  {
    public int InstanceProp { get; set; }
    public static int StaticProp { get; set; }

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 9, MethodILOffset = 22)]
    public static void TestStatic(int x)
    {
      Contract.Ensures(StaticProp == x);

      StaticProp = x;
      
    }

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 18, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 2, MethodILOffset = 24)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 10, MethodILOffset = 24)]
    public void TestInstance(int x)
    {
      Contract.Ensures(InstanceProp == x);

      InstanceProp = x;
    }
  }

  public delegate bool ImpureDelegate(int x);

  [Pure]
  public delegate bool PureDelegate(int x);

  [Pure]
  public delegate bool PureBinaryDelegate(int x, int y);

  public class DelegateTests
  {
    public PureDelegate pd1;
    public ImpureDelegate ipd1;
    public Comparison<int> comp;
    public Predicate<int> pred;

    public void Test1(ImpureDelegate d, int x)
    {
      Contract.Requires(d(x));
    }

    public void Test2(PureDelegate d, int x)
    {
      Contract.Requires(d(x));
    }

    public void Test3(Comparison<int> d, int x)
    {
      Contract.Requires(d(x,0) == 0);
    }

    public void Test4(Predicate<int> d, int x)
    {
      Contract.Requires(d(x));
    }

    public void Test5(int x)
    {
      Contract.Requires(this.ipd1(x));
    }

    public void Test6(int x)
    {
      Contract.Requires(this.pd1(x));
    }

    public void Test7(int x)
    {
      Contract.Requires(this.comp(x, 0) == 0);
    }

    public void Test8(int x)
    {
      Contract.Requires(this.pred(x));
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "Possibly calling a method on a null reference 'd'", PrimaryILOffset = 4, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 18, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 23, MethodILOffset = 0)]
    public void Test9(PureBinaryDelegate d, int x, int y)
    {
      Contract.Requires(d(x, y));
      Contract.Assert(d(x, y));
    }

  }

  class MultiVariableJoins
  {
    [Pure]
    private static bool EndsWith(string source, string suffix)
    {
      return source.EndsWith(suffix);
    }
    
    [Pure]
    private static string TrimSuffix(string source, string suffix)
    {
      return source;
    }

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 15, MethodILOffset = 50)]
    public string TrimSuffixFully(string source, string suffix)
    {
      Contract.Ensures(!EndsWith(Contract.Result<string>(), suffix));

      while (EndsWith(source, suffix))
      {
        source = TrimSuffix(source, suffix);
      }
      return source;
    }
  }

  class PureFunctionsAndMutation
  {
    [Pure]
    public bool Contains<T>(T x)
    {
      return true;
    }

    public void Add<T>(T x)
    {
      Contract.Ensures(Contains(x));

    }

    public void Remove<T>(T x)
    {
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 16, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 29, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 54, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 62, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 75, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 83, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 101, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 34, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "assert unproven", PrimaryILOffset = 67, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 88, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 106, MethodILOffset = 0)]
    public static void Test(PureFunctionsAndMutation pf, int x)
    {
      Contract.Requires(pf != null);
      Contract.Requires(pf.Contains(x));

      Contract.Assert(pf.Contains(x));

      if (x > 5)
      {
        pf.Remove(x);
        Contract.Assert(pf.Contains(x)); // should be false

        pf.Add(x);

        Contract.Assert(pf.Contains(x)); // should be true
      }
      else
      {
      }
      Contract.Assert(pf.Contains(x)); // should be true
    }
  }

  
  class PureFunctionsAndOutParameters
  {
    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="assert is valid",PrimaryILOffset=34,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="assert is valid",PrimaryILOffset=44,MethodILOffset=0)]
    static void Main2()
    {
      object x;
      int y = Get(out x);
      Contract.Assume(x != null);
      int z;
      object w;
      z = Get(out w);
      Contract.Assert(z == y);
      Contract.Assert(x == w);
    }

    [Pure]
    static int Get(out object x)
    {
      x = new object();
      return 0;
    }
  }
}

