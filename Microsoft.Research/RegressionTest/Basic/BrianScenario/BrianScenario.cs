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
using System.Diagnostics.Contracts;
using Microsoft.Research.ClousotRegression;


namespace BrianScenarios
{
  public class Assembly
  {
    bool rof;
    string name;

    [Pure]
    [ClousotRegressionTest]
    public bool IsReflectionLoadOnly
    {
      get
      {
        Contract.Ensures(Contract.Result<bool>() == this.rof);
        return this.rof;
      }
    }

    // Ok: verifies the postcondition
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 1, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 27, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 38, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 7, MethodILOffset = 43)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 12, MethodILOffset = 43)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 20, MethodILOffset = 43)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 13, MethodILOffset = 43)]
    private Assembly(int dummyParameter)
    {
      Contract.Ensures(IsReflectionLoadOnly == rof);

      this.rof = true;
      this.name = "foo";
    }

    // Not ok: rof is NOT the field there...
    [ClousotRegressionTest, RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 1, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 22, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 33, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 7, MethodILOffset = 38)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 12, MethodILOffset = 38)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"ensures unproven: IsReflectionLoadOnly == rof", PrimaryILOffset = 15, MethodILOffset = 38)]
    private Assembly(bool rof, int dummy)
    {
      Contract.Ensures(IsReflectionLoadOnly == rof);

      this.rof = true;
      this.name = "foo";
    }

    // Ok
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 1, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 22, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 33, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 7, MethodILOffset = 38)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 12, MethodILOffset = 38)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 15, MethodILOffset = 38)]
    private Assembly(bool rof)
    {
      Contract.Ensures(IsReflectionLoadOnly == rof);

      this.rof = rof;
      this.name = "foo";
    }

    // Ok
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 5, MethodILOffset = 21)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 10, MethodILOffset = 21)]
    public static Assembly ForReflectionLoadOnly()
    {
      Contract.Ensures(Contract.Result<Assembly>().IsReflectionLoadOnly);

      return new Assembly(true);
    }

    // Ok
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 5, MethodILOffset = 24)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 13, MethodILOffset = 24)]
    public static Assembly ForGeneralReflection()
    {
      Contract.Ensures(!Contract.Result<Assembly>().IsReflectionLoadOnly);

      return new Assembly(false);
    }

    // OK
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 1, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 12, MethodILOffset = 11)]
    public void DoSomething()
    {
      Contract.Requires(this.IsReflectionLoadOnly);
    }

    // ok
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 1, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 12, MethodILOffset = 14)]
    public void DoSomethingElse()
    {
      Contract.Requires(!this.IsReflectionLoadOnly);
    }

    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(this.name != null);
    }
  }

  namespace ContractsTest
  {
    class XorInPrecondition
    {
      public static void Foo(Object a, Object b)
      {
        Contract.Requires((a == null) ^ (b == null), "expected exactly one or the other to be null.");
      }

      public static void Foo1(Object a, Object b)
      {
        Contract.Requires((a == null) != (b == null), "expected exactly one or the other to be null.");
      }

      [ClousotRegressionTest("clousot1")]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=14,MethodILOffset=8)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=18,MethodILOffset=15)]
      public static void Bar()
      {
        Object obj = new Object();
        Foo(obj, null);
        Foo1(obj, null);
      }
    }

    class AndInPrecondition
    {
      public static void Foo(Object a, Object b)
      {
        Contract.Requires((a != null) & (b != null));
      }

      public static void Foo1(Object a, Object b)
      {
        Contract.Requires((a != null) && (b != null));
      }

      [ClousotRegressionTest]
      [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"requires unproven: (a != null) & (b != null)",PrimaryILOffset=15,MethodILOffset=8)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=13,MethodILOffset=15)]
      public static void Bar()
      {
        Object obj = new Object();
        Foo(obj, obj);
        Foo1(obj, obj);
      }
    }

    class OrInPrecondition
    {
      public static void Foo(Object a, Object b)
      {
        Contract.Requires((a != null) | (b != null));
      }

      public static void Foo1(Object a, Object b)
      {
        Contract.Requires((a != null) || (b != null));
      }

      [ClousotRegressionTest]
      [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"requires unproven: (a != null) | (b != null)",PrimaryILOffset=15,MethodILOffset=8)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=13,MethodILOffset=15)]
      public static void Bar()
      {
        Object obj = new Object();
        Foo(obj, obj);
        Foo1(obj, obj);
      }
    }
  }
}
