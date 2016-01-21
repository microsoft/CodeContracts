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

namespace test
{
  public class NNDemo
  {
    [ClousotRegressionTest]
    // We are setting the false to true
    //[RegressionOutcome(Outcome=ProofOutcome.False,Message="Calling a method on a null reference 'o'",PrimaryILOffset=17,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=17,MethodILOffset=0)]
    [RegressionOutcome("Contract.Requires(o != null);")]
    [RegressionOutcome("Consider replacing o == null. Fix: o != null")]
    public int InferNotNull(string o)
    {
      if (o == null && o != "ciao") // here we had a problem with the FactQuery of NonNull, when used with as the array plugin
      {
        return o.GetHashCode();
      }
      return -1;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=2,MethodILOffset=0)]
#if CLOUSOT2
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="requires is valid",PrimaryILOffset=0,MethodILOffset=2)]
#else
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="requires is valid",PrimaryILOffset=1,MethodILOffset=2)]
#endif
    [RegressionOutcome("Contract.Requires(s != null);")]
    public void CallInferNotNull(string s)
    {
      InferNotNull(s);
    }

    [ClousotRegressionTest]
#if CLOUSOT2
    [RegressionOutcome(Outcome=ProofOutcome.False,Message="requires is false: s != null",PrimaryILOffset=0,MethodILOffset=2)]
#else
//    [RegressionOutcome(Outcome=ProofOutcome.False,Message="requires is false: s != null",PrimaryILOffset=2,MethodILOffset=2)]
	[RegressionOutcome(Outcome=ProofOutcome.False,Message="requires is false: s != null. This sequence of invocations will bring to an error CallWithNull -> CallInferNotNull -> InferNotNull, condition o != null",PrimaryILOffset=2,MethodILOffset=2)]
#endif
// We should not show it by default
//	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="The normal exit of the method is unreachable. If this is wanted, consider adding Contract.Ensures(false) to document it",PrimaryILOffset=0,MethodILOffset=0)]
#if CLOUSOT2
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Invoking method 'CallWithNull' will always lead to an error. If this is wanted, consider adding Contract.Requires(false) to document it",PrimaryILOffset=0,MethodILOffset=0)]
#else
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Invoking method 'CallWithNull' will always lead to an error. If this is wanted, consider adding Contract.Requires(false) to document it",PrimaryILOffset=2,MethodILOffset=0)]
#endif
    public void CallWithNull()
    {
      CallInferNotNull(null);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=19,MethodILOffset=0)]
#if CLOUSOT2
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="requires unproven: s != null. This sequence of invocations will bring to an error CallWithTop -> CallInferNotNull -> InferNotNull, condition o != null. The error may be caused by the initialization of s. ",PrimaryILOffset=0,MethodILOffset=19)]
#else
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="requires unproven: s != null. This sequence of invocations will bring to an error CallWithTop -> CallInferNotNull -> InferNotNull, condition o != null. The error may be caused by the initialization of s. ",PrimaryILOffset=2,MethodILOffset=19)]
#endif
    [RegressionOutcome("Consider initializing s with a value other than null (e.g., satisfying s != null). Fix: <none>")]
    public void CallWithTop()
    {    
	  bool b = NonDet(); // use a method call, and not a parameter, to avoid suggesting a precondition
	
     string s = null;
     if(b)
      s = "not-null";
      
      CallInferNotNull(s);
    }
	
	// Just a dummy method
	[ContractVerification(false)]
	static private bool NonDet()
	{
	  return false;
	}
  }
  
    public class JobTitle
  {
    extern public int BaseSalary { get; }
  }

    public class Person
  {
    private readonly JobTitle jobTitle;
    private readonly string Name;

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=1,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=20,MethodILOffset=0)]
// Old message
//  [RegressionOutcome(Outcome=ProofOutcome.False,Message="invariant is false: this.jobTitle != null",PrimaryILOffset=0,MethodILOffset=25)]
	[RegressionOutcome(Outcome=ProofOutcome.False,Message="invariant is false: this.jobTitle != null. This object invariant was inferred, and it should hold in order to avoid an error if the method BaseSalary (in the same type) is invoked",PrimaryILOffset=0,MethodILOffset=25)]

	
#if SLICING
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=0,MethodILOffset=25)]
#endif
#if CLOUSOT2
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Invoking constructor 'test.Person.#ctor(System.String)' will always lead to a violation of an (inferred) object invariant",PrimaryILOffset=0,MethodILOffset=0)]
//    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Invoking constructor 'test.Person..ctor(System.String)' will always lead to an error. If this is wanted, consider adding Contract.Requires(false) to document it",PrimaryILOffset=0,MethodILOffset=0)]
    [RegressionOutcome("The constructor test.Person..ctor(System.String) violates the inferred object invariant this.jobTitle != null. Consider removing it or weakening the object invariant to IsInvoked || this.jobTitle != null. Fix: Add IsInvoked || this.jobTitle != null")]
#else
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Invoking constructor 'test.Person.#ctor(System.String)' will always lead to a violation of an (inferred) object invariant",PrimaryILOffset=1,MethodILOffset=0)]
// Old message
//    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Invoking constructor 'test.Person.#ctor(System.String)' will always lead to an error. If this is wanted, consider adding Contract.Requires(false) to document it",PrimaryILOffset=1,MethodILOffset=0)]
    [RegressionOutcome("The constructor test.Person.#ctor(System.String) violates the inferred object invariant this.jobTitle != null. Consider removing it or weakening the object invariant to IsPersonInvoked || this.jobTitle != null. Fix: Add IsPersonInvoked || this.jobTitle != null")]
#endif
   	[RegressionReanalysisCount(1)] // Reanalyze the method once
	public Person(string name)
    {
      Contract.Requires(name != null);

      this.Name = name;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=1,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=6,MethodILOffset=0)]
    public int BaseSalary()
    {
      return this.jobTitle.BaseSalary;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=1,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=26,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=33,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=0,MethodILOffset=38)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="invariant is valid",PrimaryILOffset=0,MethodILOffset=38)]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="ensures is valid",PrimaryILOffset=0,MethodILOffset=38)]
	[RegressionReanalysisCount(1)] // Reanalyze the method once
	public Person(JobTitle jobTitle, string name)
    {
      Contract.Requires(jobTitle != null && name != null);

      this.jobTitle = jobTitle;
      this.Name = name;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=1,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=14,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=20,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="requires is valid",PrimaryILOffset=13,MethodILOffset=25)]
#if SLICING 
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=32,MethodILOffset=0)]
#else
    [RegressionOutcome(Outcome=ProofOutcome.Bottom,Message="reference use unreached",PrimaryILOffset=32,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="invariant is valid",PrimaryILOffset=0,MethodILOffset=30)]
    [RegressionOutcome(Outcome=ProofOutcome.Bottom,Message="invariant unreachable",PrimaryILOffset=0,MethodILOffset=37)]
#endif
    public string GetFullName()
    {
      if (this.jobTitle != null)
      {
        return string.Format("{0} ({1})", this.Name, this.jobTitle);
      }

      return this.Name;
    }
  }
  

}
