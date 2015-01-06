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

namespace Tests
{	
  public class IsValidTest
  {
    [Pure]
	[ClousotRegressionTest]
	[RegressionOutcome("Contract.Ensures(Contract.Result<System.Boolean>() == ((k < 0 || (!(k < 0) && k > 0))));")]
    public bool IsNotZeroSilly(int k)
    {
      return k < 0 || k > 0;
    }

	[ClousotRegressionTest]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="assert is valid",PrimaryILOffset=8,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.False,Message="assert is false",PrimaryILOffset=20,MethodILOffset=0)]
    public void TestIsNotZero()
    {
      Contract.Assert(IsNotZeroSilly(12)); // true
      Contract.Assert(IsNotZeroSilly(0)); // false
    }
  }

  public class CloudDev
  {
    protected internal int[ /*NodeInt*/] First;
    protected internal const int LargeNode = Int32.MaxValue - (int.MaxValue / 2);

	[ClousotRegressionTest]
    [Pure]
	[RegressionOutcome("Contract.Ensures(Contract.Result<System.Boolean>() == ((this.First != null && (!(0 >= node.Id) && (!(node.Id >= this.First.Length) && this.First[node.Id] < 1073741824)))));")]
    public bool IsValid(Node node)
    {
      int i = node.Id;
      return this.First != null && 0 < i && i < First.Length && First[i] < LargeNode;
    }

	[ClousotRegressionTest]
	[RegressionOutcome(Outcome=ProofOutcome.False,Message="assert is false",PrimaryILOffset=24,MethodILOffset=0)]
    public void TestIsValid()
    {
      var node = new Node();
      node.Id = -2;
      Contract.Assert(IsValid(node)); // it is not...
    }	
  }
  
  public struct Node
  {
    public int Id;
  }
}

namespace InferenceBugs
{
  abstract public class OutParam
  {
	[ClousotRegressionTest]
	[RegressionOutcome("Contract.Ensures(Contract.Result<System.Boolean>() == ((Contract.ValueAtReturn(out user) == null) == false));")]
    private bool TryGetUserFromActiveDirectory(string inputName, out string user)
    {
      user = null;

      try
      {
        user = GetUser();
      }
      finally
      {

      }

      return user != null;
    }

    abstract public string GetUser();
		
  }

  public class Container
  {
    public string s;
  }
  public class Demo
  {
	[ClousotRegressionTest]
	// Should not suggest c.s != null
	[RegressionOutcome("Contract.Ensures(Contract.Result<System.Boolean>() == true);")]
	[RegressionOutcome("Contract.Ensures(Contract.ValueAtReturn(out c).s != null);")]
	[RegressionOutcome("Contract.Ensures(Contract.ValueAtReturn(out c).s.Length == 10);")]
	[RegressionOutcome("Contract.Ensures(!string.IsNullOrEmpty(Contract.ValueAtReturn(out c).s));")]
    public bool TryGet(out Container c)
    {
      c = new Container();
      c.s = "ciao mondo";

	  if (string.IsNullOrEmpty(c.s))
      {
        throw new Exception();
      }

	  
      return true;
    }
  }
}
