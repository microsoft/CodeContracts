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
using System.Text;
using System.Diagnostics.Contracts;
using Microsoft.Research.ClousotRegression;

namespace ParametersInEnsures
{
  class TestClass
  {
    public int x;

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 1, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 25, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 9, MethodILOffset = 31)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 17, MethodILOffset = 31)]
    public TestClass(int y)
    {
      Contract.Ensures(this.x == y);

      this.x = y;
    }

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 9, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 17, MethodILOffset = 0)]
    public static void Test()
    {
      TestClass c = new TestClass(5);
      Contract.Assert(c.x == 5);

      
    }

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 21, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 7, MethodILOffset = 30)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 14, MethodILOffset = 30)]
    public int HasEnsures0()
    {
      Contract.Ensures(Contract.Result<int>() == this.x);
      return this.x;
    }

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "Possibly calling a method on a null reference \'c\'", PrimaryILOffset = 2, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 9, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 17, MethodILOffset = 0)]
    public void Test0(TestClass c)
    {
      int result = c.HasEnsures0();
      Contract.Assert(c.x == result);
    }

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 9, MethodILOffset = 20)]
    public static int HasEnsures1(int x)
    {
      Contract.Ensures(Contract.Result<int>() == x);
      return x;
    }

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 12, MethodILOffset = 0)]
    public static void Test1(int arg)
    {
      int result = HasEnsures1(arg);
      Contract.Assert(arg == result);
    }

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 15, MethodILOffset = 54)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 34, MethodILOffset = 54)]
    public static int HasEnsures2(ref int x)
    {
      Contract.Ensures(Contract.Result<int>() == Contract.OldValue(x));
      Contract.Ensures(x == Contract.OldValue(x) + 1);
      var oldx = x;
      x++;
      return oldx;
    }

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 15, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 28, MethodILOffset = 0)]
    public static void Test2(ref int arg)
    {
      int old = arg;
      int result = HasEnsures2(ref arg);
      Contract.Assert(old == result);
      Contract.Assert(old + 1 == arg);
    }

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 15, MethodILOffset = 32)]
    public static int HasEnsures3(TestStruct s)
    {
      Contract.Ensures(Contract.Result<int>() == s.x);
      return s.x;
    }

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 18, MethodILOffset = 0)]
    public static void Test3(TestStruct s)
    {
      int result = HasEnsures3(s);
      Contract.Assert(s.x == result);
    }


    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 19, MethodILOffset = 78)]
    [RegressionOutcome (Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 46, MethodILOffset = 78)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 68, MethodILOffset = 0)]
    public static int HasEnsures4(ref TestStruct s)
    {
      Contract.Ensures(Contract.Result<int>() == Contract.OldValue(s.x));
      Contract.Ensures(s.x == Contract.OldValue(s.x) + 1);
      var old = s.x;
      s.x++;
      return old;
    }

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 19, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 36, MethodILOffset = 0)]
    public static void Test4(ref TestStruct s)
    {
      int oldx = s.x;
      int result = HasEnsures4(ref s);
      Contract.Assert(oldx == result);
      Contract.Assert(s.x == oldx + 1);
    }

  }

  struct TestStruct
  {
    public int x;

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 15, MethodILOffset = 28)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 23, MethodILOffset = 0)]
    public TestStruct(int y)
    {
      Contract.Ensures(Contract.ValueAtReturn(out this.x) == y);

      this.x = y;
    }

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 4, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 20, MethodILOffset = 0)]
    public static void Test()
    {
      TestStruct c = new TestStruct(5);

      Contract.Assert(c.x == 5);
    }
  }
}
