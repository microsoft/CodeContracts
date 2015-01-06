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

namespace CallInvariants
{
  public class BasicTests
  {
    // ok
	[ClousotRegressionTest]
#if CLOUSOT2	
	[RegressionOutcome("The call invariant for Callee, pos: 1 is NotNull")]
#else
	[RegressionOutcome("The call invariant for Callee, pos: 1 is NotNull")]
#endif
	public void CallWithNotNullViaVariable()
    {
      var s = "Hello";

      Callee(s);
    }

	// ok
	[ClousotRegressionTest]
#if CLOUSOT2	
	[RegressionOutcome("The call invariant for Callee, pos: 1 is Null")]
#else
	[RegressionOutcome("The call invariant for Callee, pos: 1 is Null")]
#endif
    public void CallWithNull()
    {
      Callee(null);
    }

	[ClousotRegressionTest]
#if CLOUSOT2
	[RegressionOutcome("The call invariant for Callee, pos: 1 is Null")]
#else
	[RegressionOutcome("The call invariant for Callee, pos: 1 is Null")]
#endif
	public void CalllWithNullViaVariable()
    {
      string s = null;
      Callee(s);
    }

	// TODO: Join!!!
	[ClousotRegressionTest]
#if CLOUSOT2
	[RegressionOutcome("The call invariant for Callee, pos: 1 is Null")]
	[RegressionOutcome("The call invariant for Callee, pos: 1 is NotNull")]
#else
	[RegressionOutcome("The call invariant for Callee, pos: 1 is Null")]
	[RegressionOutcome("The call invariant for Callee, pos: 1 is NotNull")]
#endif
	public void CallWithNullAndNotNull(int z)
	{
	  if(z > 1234)
	    Callee(null);
	  else
	    Callee("I am not null");
	}
	
	[ClousotRegressionTest]
//	[RegressionOutcome("Calling CallInvariants.BasicTests.Callee(System.Int32) with v == -1")]
    public void CallWithNegativeValueViaAVariable()
    {
      var v = -1;
      Callee(v);
    }

	[ClousotRegressionTest]
//	[RegressionOutcome("Calling CallInvariants.BasicTests.Callee(System.Int32,System.Int32) with x < y")]
//	[RegressionOutcome("Calling CallInvariants.BasicTests.Callee(System.Int32,System.Int32) with -2147483647 <= y")]
//	[RegressionOutcome("Calling CallInvariants.BasicTests.Callee(System.Int32,System.Int32) with x <= 2147483646")]
//	[RegressionOutcome("Calling CallInvariants.BasicTests.Callee(System.Int32,System.Int32) with x <= y")]
    public void CallWithARelation(int x, int y)
    {
      Contract.Requires(x < y);
      Callee(x, y);
    }

	[ClousotRegressionTest]
//	[RegressionOutcome("Calling CallInvariants.BasicTests.Callee(System.Int32) with sv11  == -1")]
    public void CallWithNegativeValue()
    {
      Callee(-1);
    }

	[ClousotRegressionTest]
#if CLOUSOT2	
	[RegressionOutcome("The call invariant for Callee, pos: 1 is NotNull")]
#else
	[RegressionOutcome("The call invariant for Callee, pos: 1 is NotNull")]
#endif

	//[RegressionOutcome("Calling CallInvariants.BasicTests.Callee(System.Object,System.Int32) with 1 <= sv19 ")]
    public void CallMixed(byte[] array)
    {
      Contract.Requires(array != null);
      Contract.Requires(array.Length > 0);

      Callee(array, array.Length);
    }

    private void Callee(string param1)
    {
      // do something
    }

    private void Callee(int param1)
    {
      // do something
    }

    private void Callee(int param1, int param2)
    {
      // do something
    }

    private void Callee(object param1, int value)
    {
      // do something
    }
  }
}
