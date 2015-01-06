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
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using System.CodeDom.Compiler;
using Microsoft.Research.ClousotRegression;

namespace DebuggerNonUserCodeAttributeObservance
{
  [DebuggerNonUserCodeAttribute]
  public class DebuggerNonUserCodeClass
  {
    [ClousotRegressionTest]
    public DebuggerNonUserCodeClass(string s)
    {
      Contract.Assert(s != null);
    }
  }

  public class DebuggerNonUserCodeClass_Ok
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as receiver)",PrimaryILOffset=1,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"assert unproven",PrimaryILOffset=15,MethodILOffset=0)]
#if CLOUSOT2
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Missing precondition in an externally visible method. Consider adding Contract.Requires(s != null); for parameter validation",PrimaryILOffset=0,MethodILOffset=0)]
#else
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Missing precondition in an externally visible method. Consider adding Contract.Requires(s != null); for parameter validation",PrimaryILOffset=1,MethodILOffset=0)]
#endif
    public DebuggerNonUserCodeClass_Ok(string s)
    {
      Contract.Assert(s != null);
    }
  }
}

namespace CompilerGenerateAttributeObservance
{
  [CompilerGenerated]
  public class CompilerGeneratedClass
  {
    [ClousotRegressionTest]
    public CompilerGeneratedClass(string s)
    {
      Contract.Assert(s != null);
    }
  }

  public class CompilerGenerated_OkClass
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as receiver)",PrimaryILOffset=1,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"assert unproven",PrimaryILOffset=15,MethodILOffset=0)]
#if CLOUSOT2
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Missing precondition in an externally visible method. Consider adding Contract.Requires(s != null); for parameter validation",PrimaryILOffset=0,MethodILOffset=0)]
#else
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Missing precondition in an externally visible method. Consider adding Contract.Requires(s != null); for parameter validation",PrimaryILOffset=1,MethodILOffset=0)]
#endif
    public CompilerGenerated_OkClass(string s)
    {
      Contract.Assert(s != null);
    }
  }

  public class CompilerGeneratedMethod
  {
    [CompilerGenerated]
    [ClousotRegressionTest]
    public void CompilerGeneratedMethodTest(string s)
    {
      Contract.Assert(s != null);
    }
  }

  public class CompilerGeneratedMethod_Ok
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"assert unproven", PrimaryILOffset = 8, MethodILOffset = 0)]
#if CLOUSOT2
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Missing precondition in an externally visible method. Consider adding Contract.Requires(s != null); for parameter validation",PrimaryILOffset=1,MethodILOffset=0)]
#else
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Missing precondition in an externally visible method. Consider adding Contract.Requires(s != null); for parameter validation",PrimaryILOffset=3,MethodILOffset=0)]
#endif	
    public void CompilerGeneratedMethodTest_Ok(string s)
    {
      Contract.Assert(s != null);
    }
  }
}

namespace OverrideBecauseOfContractVerification
{
  [CompilerGenerated]
  public static class CompilerGenerated
  {
    [ClousotRegressionTest]
    static public void DoNotAnalyzeMe(string s)
    {
      Contract.Assert(s != null);
    }

    [ContractVerification(true)]
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"assert unproven",PrimaryILOffset=8,MethodILOffset=0)]
#if CLOUSOT2
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Missing precondition in an externally visible method. Consider adding Contract.Requires(s != null); for parameter validation",PrimaryILOffset=1,MethodILOffset=0)]
#else
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Missing precondition in an externally visible method. Consider adding Contract.Requires(s != null); for parameter validation",PrimaryILOffset=3,MethodILOffset=0)]
#endif	
    static public void ForceMyAnalysis(string s)
    {
      Contract.Assert(s != null);
    }
  }  

  // this is the attribute generated by WPF
  [GeneratedCode("Mimick WPF", "1.1.1.1")]
  public static class GeneratedCode
  {
    [ClousotRegressionTest]
    static public void DoNotAnalyzeMe(string s)
    {
      Contract.Assert(s != null);
    }

    [ContractVerification(true)]
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"assert unproven",PrimaryILOffset=8,MethodILOffset=0)]
#if CLOUSOT2
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Missing precondition in an externally visible method. Consider adding Contract.Requires(s != null); for parameter validation",PrimaryILOffset=1,MethodILOffset=0)]
#else
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Missing precondition in an externally visible method. Consider adding Contract.Requires(s != null); for parameter validation",PrimaryILOffset=3,MethodILOffset=0)]
#endif	
    static public void ForceMyAnalysis(string s)
    {
      Contract.Assert(s != null);
    }
  }  

}