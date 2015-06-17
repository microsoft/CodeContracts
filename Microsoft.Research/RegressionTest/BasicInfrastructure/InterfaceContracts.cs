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
using Microsoft.Research.ClousotRegression;
using System.Diagnostics.Contracts;

namespace InterfaceContracts
{

  [ContractClass(typeof(IFooContract))]
  public interface IFoo
  {
    int Foo(int x);

    int Value { get; }
  }

  [ContractClassFor(typeof(IFoo))]
  abstract class IFooContract : IFoo
  {
    int IFoo.Foo(int x) {
      Contract.Requires(x >= 0);
      Contract.Ensures(Contract.Result<int>() >= 0);
      
      return 0;
    }

    int IFoo.Value {
      get {
        Contract.Ensures(Contract.Result<int>() >= 0);
        return 0;
      }
    }
  }

  public class FooClass : IFoo
  {
    public int stored;

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 25, MethodILOffset = 6)]
    public int Foo(int x)
    {
      return x;
    }

    public int Value {
      [ClousotRegressionTest("regular")]
	  [RegressionOutcome(Outcome=ProofOutcome.Top,Message="ensures unproven: Contract.Result<int>() >= 0. Are you missing an object invariant on field stored?",PrimaryILOffset=12,MethodILOffset=11)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 2, MethodILOffset = 0)]
      get { return this.stored; }
    }

  }

  public struct FooStruct : IFoo 
  {
    public int stored;

    [ContractInvariantMethod]
    void Invariant() {
      Contract.Invariant(stored >= 0);
    }

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"invariant is valid", PrimaryILOffset = 13, MethodILOffset = 13)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 25, MethodILOffset = 13)]
    public int Foo(int x)
    {
      return x + stored;
    }

    public int Value {
      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 12, MethodILOffset = 11)]
      get { return this.stored; }
    }
  }


  public class GenericFooUse<T> where T : IFoo
  {

    [ClousotRegressionTest("generics")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 8, MethodILOffset = 64)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 37, MethodILOffset = 73)]
    public int Test1(T t)
    {
      Contract.Requires(t.Value >= 0);
      Contract.Ensures(Contract.Result<int>() >= 0);

      return t.Foo(t.Value);
    }

    [ClousotRegressionTest("generics")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 8, MethodILOffset = 61)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 36, MethodILOffset = 70)]
    public int Test2(ref T t)
    {
      Contract.Requires(t.Value >= 0);
      Contract.Ensures(Contract.Result<int>() >= 0);

      return t.Foo(t.Value);
    }
  }

  public class InstantiatedFooUse
  {
    public GenericFooUse<FooClass> fooClass;
    public GenericFooUse<FooStruct> fooStruct;

    [ClousotRegressionTest("generics")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 20, MethodILOffset = 44)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 57, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 19, MethodILOffset = 71)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 84, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 20, MethodILOffset = 97)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 110, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 19, MethodILOffset = 124)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 137, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 20, MethodILOffset = 155)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 170, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 19, MethodILOffset = 183)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 198, MethodILOffset = 0)]
    public void TestMain(FooClass fc, FooStruct fs1, ref FooStruct fs2)
    {
      Contract.Assume(this.fooClass != null);
      Contract.Assume(this.fooStruct != null);

      int x1 = fooClass.Test1(fc);
      Contract.Assert(x1 >= 0);

      int x2 = fooClass.Test2(ref fc);
      Contract.Assert(x2 >= 0);

      int y1 = fooStruct.Test1(fs1);
      Contract.Assert(y1 >= 0);

      int y2 = fooStruct.Test2(ref fs1);
      Contract.Assert(y2 >= 0);

      int z1 = fooStruct.Test1(fs2);
      Contract.Assert(z1 >= 0);
      
      int z2 = fooStruct.Test2(ref fs2);
      Contract.Assert(z2 >= 0);
    }
  }

}

namespace GenericInterfaceContracts
{
  [ContractClass(typeof(ContractForJ<>))]
  public interface J<T>
  {
    bool FooBar(T[] xs);
  }

  [ContractClass(typeof(ContractForK))]
  public interface K
  {
    bool FooBar(int[] xs);
  }

  [ContractClassFor(typeof(J<>))]
  abstract class ContractForJ<T> : J<T>
  {
    bool J<T>.FooBar(T[] xs)
    {
      Contract.Requires(xs != null);
      return default(bool);
    }
  }

  [ContractClassFor(typeof(K))]
  abstract class ContractForK : K
  {
    bool K.FooBar(int[] xs)
    {
      Contract.Requires(xs != null);
      return default(bool);
    }
  }

  class B : J<int>
  {
    public virtual bool FooBar(int[] xs)
    {
      return true;
    }
  }
  class GenericB<T> : J<T>
  {
    public virtual bool FooBar(T[] xs)
    {
      return true;
    }
  }

  class C : K
  {
    public virtual bool FooBar(int[] xs)
    {
      return true;
    }
  }

  class M
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.False, Message = @"requires is false: xs != null", PrimaryILOffset = 8, MethodILOffset = 9)]
    public void Test1()
    {
      J<int> j = new B();
      j.FooBar(null);
    }
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.False, Message = @"requires is false: xs != null", PrimaryILOffset = 8, MethodILOffset = 3)]
    public void Test2(K k)
    {
      k.FooBar(null);
    }
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.False, Message = @"requires is false: xs != null", PrimaryILOffset = 8, MethodILOffset = 9)]
    public void Test3()
    {
      J<int> j2 = new GenericB<int>();
      j2.FooBar(null);
    }
  }

}


namespace AbstractClassContracts
{
    [ContractClass(typeof(AContract))]
    abstract class A
    {
      internal abstract void DoSmth(string s);
    }

    [ContractClassFor(typeof(A))]
    abstract class AContract : A
    {
      internal override void DoSmth(string s)
      {
        Contract.Requires(s != null);
      }
    }

    class B : A
    {
      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as receiver)", PrimaryILOffset = 3, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 8, MethodILOffset = 3)]
      internal override void DoSmth(string s)
      {
        this.DoSmthElse(s);
      }

      internal void DoSmthElse(string x)
      {
        Contract.Requires(x != null);
      }
    }
}

