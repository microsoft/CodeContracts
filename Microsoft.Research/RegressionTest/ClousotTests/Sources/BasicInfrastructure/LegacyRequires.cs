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

[assembly: RegressionOutcome("Member 'BasicInfrastructure.VisibilityViolations.priv' has less visibility than the enclosing method 'BasicInfrastructure.VisibilityViolations.Test2'.")]
[assembly: RegressionOutcome("Member 'BasicInfrastructure.VisibilityViolations.intern' has less visibility than the enclosing method 'BasicInfrastructure.VisibilityViolations.Test2'.")]
[assembly: RegressionOutcome("Member 'BasicInfrastructure.VisibilityViolations.priv' has less visibility than the enclosing method 'BasicInfrastructure.VisibilityViolations.Test3'.")]
[assembly: RegressionOutcome("Member 'BasicInfrastructure.VisibilityViolations.prot' has less visibility than the enclosing method 'BasicInfrastructure.VisibilityViolations.Test3'.")]
[assembly: RegressionOutcome("Member 'BasicInfrastructure.VisibilityViolations.priv' has less visibility than the enclosing method 'BasicInfrastructure.VisibilityViolations.Test4'.")]
[assembly: RegressionOutcome("Member 'BasicInfrastructure.VisibilityViolations.prot' has less visibility than the enclosing method 'BasicInfrastructure.VisibilityViolations.Test4'.")]
[assembly: RegressionOutcome("Member 'BasicInfrastructure.VisibilityViolations.intern' has less visibility than the enclosing method 'BasicInfrastructure.VisibilityViolations.Test4'.")]
[assembly: RegressionOutcome("Member 'BasicInfrastructure.VisibilityViolations.priv' has less visibility than the enclosing method 'BasicInfrastructure.VisibilityViolations.Test5'.")]
[assembly: RegressionOutcome("Member 'BasicInfrastructure.VisibilityViolations.prot' has less visibility than the enclosing method 'BasicInfrastructure.VisibilityViolations.Test5'.")]
[assembly: RegressionOutcome("Member 'BasicInfrastructure.VisibilityViolations.intern' has less visibility than the enclosing method 'BasicInfrastructure.VisibilityViolations.Test5'.")]
[assembly: RegressionOutcome("Member 'BasicInfrastructure.VisibilityViolationsInternal.priv' has less visibility than the enclosing method 'BasicInfrastructure.VisibilityViolationsInternal.Test2'.")]
[assembly: RegressionOutcome("Member 'BasicInfrastructure.VisibilityViolationsInternal.priv' has less visibility than the enclosing method 'BasicInfrastructure.VisibilityViolationsInternal.Test3'.")]
[assembly: RegressionOutcome("Member 'BasicInfrastructure.VisibilityViolationsInternal.prot' has less visibility than the enclosing method 'BasicInfrastructure.VisibilityViolationsInternal.Test3'.")]
[assembly: RegressionOutcome("Member 'BasicInfrastructure.VisibilityViolationsInternal.priv' has less visibility than the enclosing method 'BasicInfrastructure.VisibilityViolationsInternal.Test4'.")]
[assembly: RegressionOutcome("Member 'BasicInfrastructure.VisibilityViolationsInternal.prot' has less visibility than the enclosing method 'BasicInfrastructure.VisibilityViolationsInternal.Test4'.")]
[assembly: RegressionOutcome("Member 'BasicInfrastructure.VisibilityViolationsInternal.priv' has less visibility than the enclosing method 'BasicInfrastructure.VisibilityViolationsInternal.Test5'.")]
[assembly: RegressionOutcome("Member 'BasicInfrastructure.VisibilityViolationsInternal.prot' has less visibility than the enclosing method 'BasicInfrastructure.VisibilityViolationsInternal.Test5'.")]
[assembly: RegressionOutcome("Method 'BasicInfrastructure.OverrideViolations.ToString' overrides 'System.Object.ToString', thus cannot add Requires.")]

// This one is for the AnalysisInfrastructure9 tests where all is compiled together rather than per file.
#if !NETFRAMEWORK_3_5 && !NETFRAMEWORK_4_0
[assembly: RegressionOutcome("Method 'BasicInfrastructure.LegacyRequires.UsesLegacyRequires(System.Int32,System.String)' has custom parameter validation but assembly mode is not set to support this. It will be treated as Requires<E>.")]
#endif

namespace BasicInfrastructure
{
  class LegacyRequires
  {
    static void UsesLegacyRequires(int x, string y)
    {
      if (x < 0) throw new Exception();
      if (y == null) throw new Exception();
      Contract.EndContractBlock();


    }

    [ClousotRegressionTest("regular")]
#if CLOUSOT2
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"requires unproven: x >= 0", PrimaryILOffset = 3, MethodILOffset = 3)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"requires unproven: y != null",PrimaryILOffset=20,MethodILOffset=3)]
#else
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"requires unproven: x >= 0", PrimaryILOffset = 17, MethodILOffset = 3)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"requires unproven: y != null", PrimaryILOffset = 34, MethodILOffset = 3)]
#endif
    static void TestCaller1(int x, string y)
    {
      // Expected to fail
      UsesLegacyRequires(x, y);
    }

    [ClousotRegressionTest("regular")]
#if CLOUSOT2
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=3,MethodILOffset=29)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=20,MethodILOffset=29)]
#else
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 17, MethodILOffset = 29)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 34, MethodILOffset = 29)]
#endif
    static void TestCaller2(int x, string y)
    {
      Contract.Requires(x >= 0);
      Contract.Requires(y != null);

      // Expected to pass
      UsesLegacyRequires(x, y);
    }
  }

  class UnsatisfiableRequires
  {
    [ClousotRegressionTest("regular")]
//    [RegressionOutcome("method Requires (including invariants) are unsatisfiable: BasicInfrastructure.UnsatisfiableRequires.Test1(System.Int32)")]
 [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"method Requires (including invariants) are unsatisfiable: BasicInfrastructure.UnsatisfiableRequires.Test1(System.Int32)",PrimaryILOffset=21,MethodILOffset=0)]
    public static void Test1(int x)
    {
      Contract.Requires(x > 0);
      Contract.Requires(x < 0);
    }

    [ClousotRegressionTest("regular")]
    //[RegressionOutcome("method Requires (including inherited requires and invariants) are unsatisfiable: BasicInfrastructure.UnsatisfiableRequires.Test2(System.String)")]
 [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"method Requires (including inherited requires and invariants) are unsatisfiable: BasicInfrastructure.UnsatisfiableRequires.Test2(System.String)",PrimaryILOffset=24,MethodILOffset=0)]
    public virtual void Test2(string x)
    {
      Contract.Requires(x != null);
      Contract.Requires(x == null);
    }

    int Field = 1;
    string Name = "foo";

    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(Field > 0);
      Contract.Invariant(Name != null);
    }

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 2, MethodILOffset = 0)]
    //[RegressionOutcome("method Requires (including invariants) are unsatisfiable: BasicInfrastructure.UnsatisfiableRequires.TestWithInv()")]
     [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"method Requires (including invariants) are unsatisfiable: BasicInfrastructure.UnsatisfiableRequires.TestWithInv()",PrimaryILOffset=19,MethodILOffset=0)]
    private void TestWithInv()
    {
      Contract.Requires(Field <= 0);
    }

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 2, MethodILOffset = 0)]
    //[RegressionOutcome("method Requires (including invariants) are unsatisfiable: BasicInfrastructure.UnsatisfiableRequires.TestWithInv2()")]
 [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"method Requires (including invariants) are unsatisfiable: BasicInfrastructure.UnsatisfiableRequires.TestWithInv2()",PrimaryILOffset=16,MethodILOffset=0)]
    private void TestWithInv2()
    {
      Contract.Requires(Name == null);
    }

  }

  public class VisibilityViolations
  {
    private int priv = 1;
    protected int prot = 1;
    internal int intern = 1;
    public int pub = 1;

    private void Test1()
    {
      Contract.Requires(priv == 0);
      Contract.Requires(prot == 0);
      Contract.Requires(intern == 0);
      Contract.Requires(pub == 0);
    }

    protected void Test2()
    {
      Contract.Requires(priv == 0);
      Contract.Requires(prot == 0);
      Contract.Requires(intern == 0);
      Contract.Requires(pub == 0);
    }

    internal void Test3()
    {
      Contract.Requires(priv == 0);
      Contract.Requires(prot == 0);
      Contract.Requires(intern == 0);
      Contract.Requires(pub == 0);
    }

    internal protected void Test4()
    {
      Contract.Requires(priv == 0);
      Contract.Requires(prot == 0);
      Contract.Requires(intern == 0);
      Contract.Requires(pub == 0);
    }

    public void Test5()
    {
      Contract.Requires(priv == 0);
      Contract.Requires(prot == 0);
      Contract.Requires(intern == 0);
      Contract.Requires(pub == 0);
    }


  }

  internal class VisibilityViolationsInternal
  {
    private int priv = 1;
    protected int prot = 1;
    internal int intern = 1;
    public int pub = 1;

    private void Test1()
    {
      Contract.Requires(priv == 0);
      Contract.Requires(prot == 0);
      Contract.Requires(intern == 0);
      Contract.Requires(pub == 0);
    }

    protected void Test2()
    {
      Contract.Requires(priv == 0);
      Contract.Requires(prot == 0);
      Contract.Requires(intern == 0);
      Contract.Requires(pub == 0);
    }

    internal void Test3()
    {
      Contract.Requires(priv == 0);
      Contract.Requires(prot == 0);
      Contract.Requires(intern == 0);
      Contract.Requires(pub == 0);
    }

    internal protected void Test4()
    {
      Contract.Requires(priv == 0);
      Contract.Requires(prot == 0);
      Contract.Requires(intern == 0);
      Contract.Requires(pub == 0);
    }

    public void Test5()
    {
      Contract.Requires(priv == 0);
      Contract.Requires(prot == 0);
      Contract.Requires(intern == 0);
      Contract.Requires(pub == 0);
    }


  }

  public class OverrideViolations
  {
    public int x;

    public override string ToString()
    {
      Contract.Requires(x == 0);
      return "";
    }
  }
}
