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

namespace BasicInfrastructure
{
  class AssertOverloads
  {
    static void MyPassiveAssert(string s, bool b, string s2) { }

    static void MyActiveAssert(string s, bool b, string s2) {
      Contract.Requires(b);
    }

    static void MyAssumeAssert(string s, bool b, string s2)
    {
      Contract.Ensures(b);

      if (!b) throw new Exception();
    }

    class MyContract
    {
      public static void MyAssume(string s, bool b, string s2) { }
      public static void MyAssert(string s, bool b, string s2) { }
    }

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 31, MethodILOffset = 57)]
    string Test1(string s)
    {
      Contract.Requires(s != null, "test");
      Contract.Ensures(s != null, "yup");

      MyPassiveAssert(s, s != null, s);

      return s;
    }

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 2, MethodILOffset = 46)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 31, MethodILOffset = 57)]
    string Test2(string s)
    {
      Contract.Requires(s != null, "test");
      Contract.Ensures(s != null, "yup");

      MyActiveAssert(s, s != null, s);

      return s;
    }

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 13, MethodILOffset = 39)]
    string Test3(string s)
    {
      Contract.Ensures(s != null, "yup");

      MyAssumeAssert(s, s != null, s);

      return s;
    }

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 31, MethodILOffset = 0)]
    void Test4(string s)
    {
      Contract.Requires(s != null, "yup");

      Contract.Assert(s != null, "test");
    }

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 36, MethodILOffset = 0)]
    void Test5(string s)
    {
      Contract.Requires(s != null, "yup");

      MyContract.MyAssert("hello", s != null, "test");
    }

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="ensures is valid",PrimaryILOffset=17,MethodILOffset=46)]
    string Test6(string s)
    {
      Contract.Ensures(Contract.Result<string>() != null, "test");
      Contract.Assume(s != null, "test");

      return s;
    }

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="ensures is valid",PrimaryILOffset=17,MethodILOffset=47)]
    string Test7(string s)
    {
      Contract.Ensures(Contract.Result<string>() != null, "test");
      MyContract.MyAssume(s, s != null, "hello");

      return s;
    }

  }
}
