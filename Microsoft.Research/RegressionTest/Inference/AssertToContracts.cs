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


namespace ExamplesAssertToContracts
{
  public class AssertToContracts
  {
    private int myPrivateField;

    public AssertToContracts(int x)
    {
      this.myPrivateField = x;
    }
    
    
    [ClousotRegressionTest]
	[RegressionOutcome("This Assert cannot be statically proven because some information on the entry state is missing. Consider turning it into an Assume (or adding the missing contracts, e.g., object invariants)")]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="assert unproven",PrimaryILOffset=7,MethodILOffset=0)]
	[RegressionOutcome("Contract.Requires(z >= 0);")]
	public void ShouldBeRequires(int z)
    {
      Contract.Assert(z >= 0); // Can't prove it: should turn into requires
      this.myPrivateField += z;
    }


    [ClousotRegressionTest]
	[RegressionOutcome("This Assert cannot be statically proven because some information on the entry state is missing. Consider turning it into an Assume (or adding the missing contracts, e.g., object invariants)")]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="assert unproven",PrimaryILOffset=12,MethodILOffset=0)]
	public void ShouldBeAssume(int z)
    {
      Contract.Assert(this.myPrivateField >= 0); // Can't prove it: should turn into assume
      this.myPrivateField += z;
    }

    
	// ok
    [ClousotRegressionTest]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="assert unproven. The static checker determined that the condition 'z > 12' should hold on entry. Nevertheless, the condition may be too strong for the callers. If you think it is ok, add a precondition to document it: Contract.Requires(z > 12);",PrimaryILOffset=8,MethodILOffset=0)]
	[RegressionOutcome("Contract.Requires((!(b) || z > 12));")]
	public void ShouldNotSuggest(bool b,  int z)
    {
      if(b)
        Contract.Assert(z > 12);
    }

    [ClousotRegressionTest]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="ensures unproven: Contract.Result<int>() > 12",PrimaryILOffset=9,MethodILOffset=15)]
	[RegressionOutcome("Contract.Requires(z > 12);")]
	public int ShouldNotSuggest(int z)
    {
      Contract.Ensures(Contract.Result<int>() > 12);

      return z;
    }


  }

  public class AssertToContract2
  {
    // Should suggest to turn into assume
	[ClousotRegressionTest]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possibly accessing a field on a null reference 'a'",PrimaryILOffset=1,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="assert unproven",PrimaryILOffset=12,MethodILOffset=0)]
	[RegressionOutcome("This Assert cannot be statically proven because some information on the entry state is missing. Consider turning it into an Assume (or adding the missing contracts, e.g., object invariants)")]
	[RegressionOutcome("Contract.Requires(a != null);")]
	[RegressionOutcome("Contract.Requires(a.f != null);")]
    public void F(A a)
    {
      Contract.Assert(a.f != null);
    }
    // Should turn into assume or requires
    [ClousotRegressionTest]
	[RegressionOutcome("This Assert cannot be statically proven because some information on the entry state is missing. Consider turning it into an Assume (or adding the missing contracts, e.g., object invariants)")]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="assert unproven",PrimaryILOffset=4,MethodILOffset=0)]
	[RegressionOutcome("Contract.Requires(x > 0);")]
    private int F(int x)
    {
      Contract.Assert(x > 0);

      return x + 2;
    }

	[ClousotRegressionTest]    
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="assert unproven",PrimaryILOffset=10,MethodILOffset=0)]
    public void G(int x, out int value)
    {
      var b = TryGet(x, out value);
      Contract.Assert(b);
    }

    [ClousotRegressionTest]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="requires unproven: c >= 0. Is it an off-by-one? The static checker can prove index >= (0 - 1) instead",PrimaryILOffset=7,MethodILOffset=22)]
    public void Search(string s, char c)
    {
      Contract.Requires(s != null);

      var index = s.IndexOf(s);
      Requires(index);
    }

    // We want to filter this one
    [ClousotRegressionTest]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="requires unproven: t.Length > 0",PrimaryILOffset=9,MethodILOffset=9)]
    internal int Search2()
    {
      var t = GetString();

      return Create(t);
    }

    // We want to filter this one
    [ClousotRegressionTest]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="requires unproven: t.Length > 0",PrimaryILOffset=6,MethodILOffset=9)]
    internal int Search3()
    {
      var arr = GetArray();
      return Create(arr);
    }

    [ClousotRegressionTest]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possibly calling a method on a null reference 'identifier'",PrimaryILOffset=9,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="requires unproven: 0 <= index",PrimaryILOffset=13,MethodILOffset=16)]
    internal int Search4()
    {
      var identifier = GetId();

      var s = identifier[identifier.Length - 1];

      return 0;
    }

    [ContractVerification(false)]
    private string GetId()
    {
      return "hello;";
    }

    [ContractVerification(false)]
    private AssertToContract2 GetAssertToContract()
    {
      throw new NotImplementedException();
    }

    public int Length { get; set; }

    // We should not suggest this one
    [ClousotRegressionTest]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible use of a null array 'args'",PrimaryILOffset=1,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possibly calling a method on a null reference",PrimaryILOffset=16,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="requires unproven: index < this.Length",PrimaryILOffset=33,MethodILOffset=16)]
	[RegressionOutcome("Contract.Requires(args != null);")]
	private static void Main(string[] args)
    {
      if (args.Length != 1)
      {
        throw new Exception();
      }

      if (args[0][0] == '@')
      {
      }
    }

    private static string[] ExpandResponseFiles(string[] args)
    {
      Contract.Requires(args != null);

      bool found = false;
      foreach (string arg in args)
      {
        if (arg[0] == 'c')
        {
          found = true;
          break;
        }
      }
      
      return found? null: new string[1];
    }

    [ContractVerification(false)]
    private int Requires(int c)
    {
      Contract.Requires(c >= 0);

      return c;
    }

    [ContractVerification(false)]
    private int Create(String t)
    {
      Contract.Requires(t.Length > 0);

      return t.Length;
    }

    [ContractVerification(false)]
    private int Create(int[] t)
    {
      Contract.Requires(t.Length > 0);

      return t.Length;
    }

    [ContractVerification(false)]
    private string GetString()
    {
      throw new NotImplementedException();
    }

    [ContractVerification(false)]
    private Int32[] GetArray()
    {
      throw new NotImplementedException();
    }

    [ContractVerification(false)]
    private bool TryGet(int x, out int v)
    {
      v = 1;
      return true;
    }
  }
  
  public class A
  {
    public readonly string f;
  }
}