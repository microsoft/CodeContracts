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

namespace ConsoleApplication8
{
    public class NecessaryObjectInvariant
  {
    private readonly object m_o;

   	[ClousotRegressionTest]
	[RegressionReanalysisCount(1)] // Reanalyze the method once
	[RegressionOutcome("Contract.Requires(o != null);")]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="invariant unproven: this.m_o != null",PrimaryILOffset=0,MethodILOffset=13)]
	[RegressionOutcome("Contract.Ensures(o == this.m_o);")]
	public NecessaryObjectInvariant(object o)
    {
      this.m_o = o;
    }

	[ClousotRegressionTest]
	[RegressionOutcome("Contract.Ensures(this.m_o != null);")]
	[RegressionOutcome("Contract.Invariant(this.m_o != null);")]
    public void AssertIt()
    {
      Contract.Assert(m_o != null);
    }

    [ClousotRegressionTest]
	[RegressionOutcome("Contract.Ensures(Contract.Result<System.Object>() != null);")]
	[RegressionOutcome("Contract.Ensures(Contract.Result<System.Object>() == this.m_o);")]	
	public object GetObject()
    {
      return m_o;
    }
  }

  public class SomeClassWithOneConstructor
  {
    readonly private string m_MyObject;

	[ClousotRegressionTest]
	[RegressionReanalysisCount(1)] // Reanalyze the method once
	[RegressionOutcome("Contract.Ensures(this.m_MyObject != null);")]
//	[RegressionOutcome("Contract.Ensures(s == this.m_MyObject);")] // somehow this is filtered. Is it a bug?
    public SomeClassWithOneConstructor(string s)
    {
      Contract.Requires(s != null);

      this.m_MyObject = s;
    }


    public string TheString
    {
      [ClousotRegressionTest]
      [RegressionReanalysisCount(1)] // Reanalyze the method once
	  [RegressionOutcome("Contract.Ensures(Contract.Result<System.String>() != null);")]
	  [RegressionOutcome("Contract.Ensures(this.m_MyObject != null);")]
		// TODO: Seems we filter result == this.m_MyObject
	  get
      {
        return this.m_MyObject;
      }
    }

	[ClousotRegressionTest]
	[RegressionOutcome("Contract.Ensures(Contract.Result<System.String>() != null);")]
	[RegressionOutcome("Contract.Ensures(Contract.Result<System.String>() == this.m_MyObject);")]
    public string TheString_AsMethodCall()
    {
      return this.m_MyObject;
    }

  }

  public class SomeClassWithTwoConstructors_ShouldWarn
  {
    readonly private string m_MyObject_ThatCanBeNull;

	[ClousotRegressionTest]
	[RegressionOutcome("Contract.Ensures(this.m_MyObject_ThatCanBeNull != null);")]
	[RegressionOutcome("Contract.Ensures(s == this.m_MyObject_ThatCanBeNull);")]
    public SomeClassWithTwoConstructors_ShouldWarn(string s)
    {
      Contract.Requires(s != null);

      this.m_MyObject_ThatCanBeNull = s;
    }

	[ClousotRegressionTest]
	[RegressionOutcome("Contract.Ensures(this.m_MyObject_ThatCanBeNull == null);")]
    public SomeClassWithTwoConstructors_ShouldWarn()
    {
      this.m_MyObject_ThatCanBeNull = null;
    }


    public string TheString
    {
	   // no re-analysis
      [ClousotRegressionTest]
	  [RegressionOutcome("Contract.Ensures(Contract.Result<System.String>() == this.m_MyObject_ThatCanBeNull);")]
      get
      {
        return this.m_MyObject_ThatCanBeNull;
      }
    }

  }


  public class SomeClassWithTwoConstructors_AndFieldCannotBeNull
  {
    readonly private string m_MyObject_ThatCannot_Be_Null;

	[ClousotRegressionTest]
	[RegressionOutcome("Contract.Ensures(this.m_MyObject_ThatCannot_Be_Null != null);")]
	[RegressionOutcome("Contract.Ensures(s == this.m_MyObject_ThatCannot_Be_Null);")]
    public SomeClassWithTwoConstructors_AndFieldCannotBeNull(string s)
    {
      Contract.Requires(s != null);

      this.m_MyObject_ThatCannot_Be_Null = s;
    }

	[ClousotRegressionTest]
	[RegressionOutcome("Contract.Ensures(this.m_MyObject_ThatCannot_Be_Null != null);")]
    public SomeClassWithTwoConstructors_AndFieldCannotBeNull()
    {
      this.m_MyObject_ThatCannot_Be_Null = "Constant string";
    }


    public string TheString
    {
      [ClousotRegressionTest]
      [RegressionReanalysisCount(1)] // Reanalyze the method once
	  [RegressionOutcome("Contract.Ensures(Contract.Result<System.String>() != null);")]
	  [RegressionOutcome("Contract.Ensures(this.m_MyObject_ThatCannot_Be_Null != null);")]
	 get
      {
        return this.m_MyObject_ThatCannot_Be_Null;
      }
    }

  }
  
  public class SomeClassWithANonReadonlyField
  {
    private string m_NonReadonly_ButOnlyAssignedInConstructor;

    [ClousotRegressionTest]
	[RegressionOutcome("Field m_NonReadonly_ButOnlyAssignedInConstructor, declared in type SomeClassWithANonReadonlyField, is only updated in constructors. Consider marking it as readonly")]
	[RegressionOutcome("Contract.Ensures(this.m_NonReadonly_ButOnlyAssignedInConstructor != null);")]
	[RegressionOutcome("Contract.Ensures(input == this.m_NonReadonly_ButOnlyAssignedInConstructor);")]
    public SomeClassWithANonReadonlyField(string input)
    {
      Contract.Requires(input != null);

      this.m_NonReadonly_ButOnlyAssignedInConstructor = input;
    }

    public string TheString_FromANonReadonlyField
    {
      [ClousotRegressionTest]
      [RegressionReanalysisCount(1)] // Reanalyze the method once
	  [RegressionOutcome("Contract.Ensures(Contract.Result<System.String>() != null);")]
	  [RegressionOutcome("Contract.Ensures(this.m_NonReadonly_ButOnlyAssignedInConstructor != null);")]
      get
      {
        return this.m_NonReadonly_ButOnlyAssignedInConstructor;
      }
    }
  }
 
  public class User
  {
	[ClousotRegressionTest]

    public void Test0(NecessaryObjectInvariant noi)
    {
      Contract.Requires(noi != null);

      var v = noi.GetObject();

      Contract.Assert(v != null);
    }

	[ClousotRegressionTest]
	[RegressionOutcome("Contract.Ensures(obj.TheString != null);")]
    public void Test0(SomeClassWithOneConstructor obj)
    {
      Contract.Requires(obj != null);

      var str = obj.TheString;

      Contract.Assert(str != null); // no warning
    }

	[ClousotRegressionTest]
    public void Test1(SomeClassWithOneConstructor obj)
    {
      Contract.Requires(obj != null);

      var str = obj.TheString_AsMethodCall(); // we should infer the postcondition

      Contract.Assert(str != null); // no warning
    }

	[ClousotRegressionTest]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="assert unproven",PrimaryILOffset=26,MethodILOffset=0)]
	[RegressionOutcome("Contract.Requires(obj.TheString != null);")]
	[RegressionOutcome("Contract.Ensures(obj.TheString != null);")]
    public void Test0(SomeClassWithTwoConstructors_ShouldWarn obj)
    {
      Contract.Requires(obj != null);

      var str = obj.TheString;

      Contract.Assert(str != null); // should warn
    }


	[ClousotRegressionTest]
	[RegressionOutcome("Contract.Ensures(obj.TheString != null);")]
    public void Test0(SomeClassWithTwoConstructors_AndFieldCannotBeNull obj)
    {
      Contract.Requires(obj != null);

      var str = obj.TheString;

      Contract.Assert(str != null);// no warning
    }
	
	[ClousotRegressionTest]
	[RegressionOutcome("Contract.Ensures(obj.TheString_FromANonReadonlyField != null);")]
	public void Test2(SomeClassWithANonReadonlyField obj)
    {
      Contract.Requires(obj != null);

      var str = obj.TheString_FromANonReadonlyField;

      Contract.Assert(str != null); // no warning
    }
  }
}