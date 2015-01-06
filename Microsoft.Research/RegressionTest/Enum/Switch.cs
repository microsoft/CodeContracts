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
using System.Diagnostics.Contracts;
using Microsoft.Research.ClousotRegression;

namespace SwitchTests
{
  public static class Helper
  {
    [ContractVerification(false)]
    static public Exception FailedAssertion()
    {
      Contract.Requires(false, "handle all enums");
      throw new Exception();
    }
  }

  public class SomeOtherClass
  {
    [ContractVerification(false)]
    public Foo.MyEnum ReturnAnEnumValue()
    {
      return Foo.MyEnum.Three; // dummy
    }
  }

  public class Foo
  {
    public enum MyEnum { One, Two, Three };

	[ClousotRegressionTest]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="Assignment to an enum value ok",PrimaryILOffset=3,MethodILOffset=0)]
#if CLOUSOT2
	// BUG BUG BUG: It seems Clousot2 does not see the precondition of Helper
#else
	[RegressionOutcome(Outcome=ProofOutcome.False,Message="This requires, always leading to an error, may be reachable. Are you missing an enum case? (handle all enums)",PrimaryILOffset=6,MethodILOffset=32)]
#endif
	public int Switch(MyEnum e)
    {
      var i = 0;

      switch (e)
      {
        case MyEnum.One:
          i += 1;
          break;

        case MyEnum.Two:
          i += 2;
          break;

        default:
          throw Helper.FailedAssertion();
      }

      return i;
    }

	[ClousotRegressionTest]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="Assignment to an enum value ok",PrimaryILOffset=3,MethodILOffset=0)]
#if CLOUSOT2
	[RegressionOutcome(Outcome=ProofOutcome.False,Message="This assert, always leading to an error, may be reachable. Are you missing an enum case?",PrimaryILOffset=33,MethodILOffset=0)]
#else
	[RegressionOutcome(Outcome=ProofOutcome.False,Message="This assert, always leading to an error, may be reachable. Are you missing an enum case?",PrimaryILOffset=33,MethodILOffset=0)]
#endif
    public int SwitchWithAssertFalse(MyEnum e)
    {
      var i = 0;

      switch (e)
      {
        case MyEnum.One:
          i += 1;
          break;

        case MyEnum.Two:
          i += 2;
          break;

        default:
          Contract.Assert(false);
          throw new Exception();
      }

      return i;
    }

	[ClousotRegressionTest]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="Assignment to an enum value ok",PrimaryILOffset=20,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="Assignment to an enum value ok",PrimaryILOffset=22,MethodILOffset=0)]
#if CLOUSOT2
	// BUG BUG? Clousot2 does not see the precondition???
#else
	[RegressionOutcome(Outcome=ProofOutcome.False,Message="This requires, always leading to an error, may be reachable. Are you missing an enum case? (handle all enums)",PrimaryILOffset=6,MethodILOffset=51)]
#endif
	public int Switch_ViaMethodReturnValue(SomeOtherClass so)
    {
      Contract.Requires(so != null);
      var i = 0;

      var e = so.ReturnAnEnumValue();

      switch (e)
      {
        case MyEnum.One:
          i += 1;
          break;

        case MyEnum.Two:
          i += 2;
          break;

        default:
          throw Helper.FailedAssertion();
      }

      return i;
    }

    public enum Days { Sun = 0, Mon = 1, Tue = 2, Wed = 3, Thu = 4, Fri = 5, Sat = 6 }

	[ClousotRegressionTest]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="Assignment to an enum value ok",PrimaryILOffset=1,MethodILOffset=0)]
#if CLOUSOT2
	[RegressionOutcome(Outcome=ProofOutcome.False,Message="This assert, always leading to an error, may be reachable. Are you missing an enum case?",PrimaryILOffset=51,MethodILOffset=0)]
#else
	[RegressionOutcome(Outcome=ProofOutcome.False,Message="This assert, always leading to an error, may be reachable. Are you missing an enum case?",PrimaryILOffset=51,MethodILOffset=0)]
#endif
    public string Translate(Days d)
    {
      switch (d)
      {
        case Days.Sun: // 0
        case Days.Tue: // 2
        case Days.Thu: // 4
        case Days.Fri: // 5
          return "Even";

        case Days.Mon: // 1
        case Days.Sat: // 6
          return "Odd";

        default:
          Contract.Assert(false);     // we forgot Wed!
          return null;
      }
    }
  }
}