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
using System.Globalization;

#if !NETFRAMEWORK_3_5
using System.ComponentModel.Composition;
#endif

namespace Microsoft.Research.Clousot
{
  public class NonNullRegressions
  {
    public dynamic Bag { get { return this; } }

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as array)",PrimaryILOffset=27,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=51,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as array)",PrimaryILOffset=87,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as array)",PrimaryILOffset=97,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=121,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=132,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=138,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=143,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as array)",PrimaryILOffset=195,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as array)",PrimaryILOffset=205,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=229,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=240,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=250,MethodILOffset=0)]
    public void DynTest()
    {
      if (Bag != null)
      {
        Bag.Message = "Test";
      }
    }

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 23, MethodILOffset = 0)]
    static public void Basic1(string arg)
    {
      Contract.Requires(!string.IsNullOrEmpty(arg));

      Contract.Assert(arg != null);
    }

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 23, MethodILOffset = 0)]
    static public void Basic2(string arg)
    {
      Contract.Requires(!string.IsNullOrWhiteSpace(arg));

      Contract.Assert(arg != null);
    }

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possibly unboxing a null reference",PrimaryILOffset=7,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=27,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=40,MethodILOffset=0)]
    static void NullableUnboxTest(string[] args)
    {
      int first = (int)Foo(args);  // Warning: Unboxing a null reference
      int second = (int?)Foo(args) ?? 55;  // Warning: Unboxing a null reference
    }

    static object Foo(string[] args)
    {
      if (args != null && args.Length > 5)
      {
        return null;
      }
      return args;
    }

#if !NETFRAMEWORK_3_5
    [Import]
    IFoo AFoo;

    [Import(AllowDefault=true)]
    IFoo BFoo;

    [ImportMany]
    IEnumerable<IFoo> CFoo;


    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=2,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=20,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=38,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="assert is valid",PrimaryILOffset=13,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="assert unproven",PrimaryILOffset=31,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="assert is valid",PrimaryILOffset=49,MethodILOffset=0)]
    void TestImport() {
      Contract.Assert(this.AFoo != null);

      // must warn
      Contract.Assert(this.BFoo != null);

      // no warn
      Contract.Assert(this.CFoo != null);

    }
#endif
  }

  public interface IFoo {
  }
}


