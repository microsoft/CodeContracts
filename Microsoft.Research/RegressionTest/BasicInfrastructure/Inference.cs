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

using System.Diagnostics.Contracts;
using Microsoft.Research.ClousotRegression;
using System;

class ModifiesInformation
{
  string x;
  string y = null;
  ModifiesInformation next;

  [ClousotRegressionTest("inference")]
  [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 46, MethodILOffset = 0)]
  [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 59, MethodILOffset = 0)]
  void NoModifiesWithJoin(bool b)
  {
    if (b)
    {
    }
    else
    {
    }
    string tmpx = this.next.x; // should be unmodified
    string tmpy = this.next.y; // should be unmodified

    Contract.Assert(tmpx != null); // should result in a pre-condition suggestion
    Contract.Assert(tmpy != null); // should result in a pre-condition suggestion
  }


  [ClousotRegressionTest("inference")]
  [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 46, MethodILOffset = 0)]
  [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 59, MethodILOffset = 0)]
  static void NoModifiesWithJoin(ModifiesInformation a, bool b)
  {
    if (b)
    {
    }
    else
    {
    }
    string tmpx = a.next.x; // should be unmodified
    string tmpy = a.next.y; // should be unmodified

    Contract.Assert(tmpx != null); // should result in a pre-condition suggestion
    Contract.Assert(tmpy != null); // should result in a pre-condition suggestion
  }



  [ClousotRegressionTest("inference")]
  [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "assert unproven", PrimaryILOffset = 48, MethodILOffset = 0)]
  [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 61, MethodILOffset = 0)]
  void ModifiesOnBranch(bool b)
  {
    if (b)
    {
      this.x = null;
    }
    else
    {
    }
    string tmpx = this.x; // should be modified
    string tmpy = this.next.y; // should be unmodified

    Contract.Assert(tmpx != null); // should NOT result in a pre-condition suggestion
    Contract.Assert(tmpy != null); // should result in a pre-condition suggestion
  }


  [ClousotRegressionTest("inference")]
  [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "assert unproven", PrimaryILOffset = 57, MethodILOffset = 0)]
  [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "assert unproven", PrimaryILOffset = 70, MethodILOffset = 0)]
  void ModifiesOnBranch2(bool b)
  {
    if (b)
    {
      this.next = new ModifiesInformation();
    }
    else
    {
    }
    string tmpx = this.next.x; // should be modified
    string tmpy = this.next.y; // should be modified

    Contract.Assert(tmpx != null); // should NOT result in a pre-condition suggestion
    Contract.Assert(tmpy != null); // should NOT result in a pre-condition suggestion
  }


  [ClousotRegressionTest("inference")]
  [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "assert unproven", PrimaryILOffset = 26, MethodILOffset = 0)]
#if CLOUSOT2
	#if SLICING 
		[RegressionOutcome(Outcome=ProofOutcome.True,Message="requires is valid",PrimaryILOffset=0,MethodILOffset=3)]
	#else
		[RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=1,MethodILOffset=3)]
	#endif
#else
  [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 3, MethodILOffset = 3)]
#endif
  void ModifiesByCall()
  {
    ModifiesOnBranch(true);

    Contract.Assert(this.next.x != null); // should NOT result in a pre-condition suggestion
  }

}

class AccessPathTest
{
  public static readonly int Min = 5;
  private static readonly int pMin = 5;

  public static int Prop { get { return 5; } }
  private static int pProp { get { return 5; } }

  public int IProp { get { return 5; } }
  private int iProp { get { return 5; } }

  [ClousotRegressionTest("inference")]
  [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 9, MethodILOffset = 0)]
  public void PosTestStaticField()
  {
    Contract.Assert(Min > 5); // should result in a precondition (thus valid assert)
  }

  [ClousotRegressionTest("inference")]
  [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "assert unproven", PrimaryILOffset = 9, MethodILOffset = 0)]
  public void NegTestStaticField()
  {
    Contract.Assert(pMin > 5); // should NOT result in a precondition
  }

  [ClousotRegressionTest("inference")]
  [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "assert unproven", PrimaryILOffset = 9, MethodILOffset = 0)]
  public void PosTestStaticProperty()
  {
    Contract.Assert(Prop > 5); // should NOT result in a precondition (we don't want preconditions on static props)
  }

  [ClousotRegressionTest("inference")]
  [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "assert unproven", PrimaryILOffset = 9, MethodILOffset = 0)]
  public void NegTestStaticProperty()
  {
    Contract.Assert(pProp > 5); // should NOT result in a precondition
  }


  [ClousotRegressionTest("inference")]
  [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 10, MethodILOffset = 0)]
  public void PosTestInstanceProperty()
  {
    Contract.Assert(IProp > 5); // should result in a precondition (thus valid assert)
  }

  [ClousotRegressionTest("inference")]
  [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "assert unproven", PrimaryILOffset = 10, MethodILOffset = 0)]
  public void NegTestInstanceProperty()
  {
    Contract.Assert(iProp > 5); // should NOT result in a precondition
  }


  class PreconditionInference
  {
    string data = null;

    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(data != null);
    }

    [ClousotRegressionTest("inference")]
    [RegressionOutcome(Outcome=ProofOutcome.False,Message=@"invariant is false: data != null",PrimaryILOffset=13,MethodILOffset=16)]
    public PreconditionInference()
    {
      // invariant not proven, but should not suggest pre-condition
    }

    [Pure]
    public static bool IsNull(string s)
    {
      return s == null;
    }

    public static void TestPreString1(string s)
    {
      Contract.Assert(!IsNull(s));
    }

    [Pure]
    public bool IsFoo()
    {
      return true;
    }

    [Pure]
    public bool IsBar { get { return true; } }

    public static void TestPreString2(PreconditionInference p)
    {
      Contract.Assert(!p.IsFoo());
    }

    public static void TestPreString3(PreconditionInference p)
    {
      Contract.Assert(!p.IsBar);
    }
  }
}
