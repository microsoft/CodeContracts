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

namespace StructsAndProperties
{
  public enum Kind { A, B }

  public struct S
  {
    Kind kind;
    public int Foo;

    [Pure]
    public static Kind GetKind(S s)
    {
      Contract.Ensures(Contract.Result<Kind>() == s.kind);
      return s.kind;
    }

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 23, MethodILOffset = 0)]
    [RegressionOutcome (Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 30, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 15, MethodILOffset = 35)]
    public S(Kind kind)
    {
      Contract.Ensures(GetKind(Contract.ValueAtReturn(out this)) == kind);
      this.kind = kind;
      this.Foo = 0;
    }

    public void Impure() { }

    [Pure]
    public void Pure() { }
  }

  class StructProperties
  {
    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 18, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 10, MethodILOffset = 18)]
    public void Test1(S s)
    {
      Contract.Requires(S.GetKind(s) == Kind.A);

      Test2(s);
    }

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 25, MethodILOffset = 0)]
    public void Test2(S s)
    {
      Contract.Requires(S.GetKind(s) == Kind.A);

      Contract.Assert(S.GetKind(s) == Kind.A);
    }

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 27, MethodILOffset = 0)]
    public void Test3(S s)
    {
      Contract.Requires(S.GetKind(s) == Kind.A);
      S t = s;
      Contract.Assert(S.GetKind(t) == Kind.A);
    }

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 17, MethodILOffset = 0)]
    public void Test4(S s)
    {
      S t = s;
      Contract.Assert(S.GetKind(t) == S.GetKind(s));
    }

    [ClousotRegressionTest("regular"), RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 14, MethodILOffset = 30)]
    public S Test5(Kind kind)
    {
      Contract.Ensures(S.GetKind(Contract.Result<S>()) == kind);

      return new S(kind);
    }

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 3, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 18, MethodILOffset = 0)]
    public void Test6()
    {
      S s = Test5(Kind.B);

      Contract.Assert(S.GetKind(s) == Kind.B);
    }

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 3, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 18, MethodILOffset = 0)]
    public void Test7(Kind kind)
    {
      S s = Test5(kind);

      Contract.Assert(S.GetKind(s) == kind);
    }

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 18, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 33, MethodILOffset = 0)]
    public void Test8(S s)
    {
      Contract.Requires(S.GetKind(s) == Kind.A);

      s.Pure();

      Contract.Assert(S.GetKind(s) == Kind.A);
    }

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 20, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 35, MethodILOffset = 0)]
    public void Test9(S s)
    {
      Contract.Requires(S.GetKind(s) == Kind.A);
      S t = s;
      s.Impure();
      Contract.Assert(S.GetKind(t) == Kind.A); // okay
    }

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 18, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 26, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"requires unproven: S.GetKind(s) == Kind.A", PrimaryILOffset = 10, MethodILOffset = 26)]
    public void Test1N(S s)
    {
      Contract.Requires(S.GetKind(s) == Kind.A);

      s.Impure();

      Test2(s); // should not be proven
    }

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 19, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "assert unproven", PrimaryILOffset = 33, MethodILOffset = 0)]
    public void Test2N(S s)
    {
      Contract.Requires(S.GetKind(s) == Kind.A);

      s.Foo = 5;

      Contract.Assert(S.GetKind(s) == Kind.A); // should not be proven
    }

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 5, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "assert unproven", PrimaryILOffset = 25, MethodILOffset = 0)]
    public void Test3N(S s)
    {
      S t = s;
      s.Impure();
      Contract.Assert(S.GetKind(t) == S.GetKind(s)); // should not be proven
    }

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 23, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 31, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"ensures unproven: S.GetKind(Contract.Result<S>()) == kind", PrimaryILOffset = 14, MethodILOffset = 42)]
    public S Test4N(Kind kind)
    {
      Contract.Ensures(S.GetKind(Contract.Result<S>()) == kind);

      S s = new S(kind);
      s.Impure();
      return s; // post should fail.
    }

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 3, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 11, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "assert unproven", PrimaryILOffset = 26, MethodILOffset = 0)]
    public void Test5N()
    {
      S s = Test5(Kind.B);

      s.Impure();
      Contract.Assert(S.GetKind(s) == Kind.B); // should fail
    }

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 3, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 11, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "assert unproven", PrimaryILOffset = 26, MethodILOffset = 0)]
    public void Test6N(Kind kind)
    {
      S s = Test5(kind);

      s.Impure();

      Contract.Assert(S.GetKind(s) == kind); // should fail
    }

  }


  public struct MyNullable<T>
  {
    T value;
    bool hasValue;

    public bool HasValue
    {
      get
      {
        Contract.Ensures(Contract.Result<bool>() == this.hasValue);
        return this.hasValue;
      }
    }

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 23, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 30, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 10, MethodILOffset = 35)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 15, MethodILOffset = 35)]
    public MyNullable(T value)
    {
      Contract.Ensures(Contract.ValueAtReturn(out this).HasValue);
      this.value = value;
      this.hasValue = true;
    }

    public T Value
    {
      get
      {
        Contract.Requires(this.HasValue);
        return value;
      }
    }

    [ClousotRegressionTest("cci1only")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 9, MethodILOffset = 30)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 14, MethodILOffset = 30)]
    public static implicit operator MyNullable<T>(T value)
    {
      Contract.Ensures(Contract.Result<MyNullable<T>>().HasValue);

      return new MyNullable<T>(value);
    }

  }

  public class MyNullableTest
  {
    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 3, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"requires unproven: this.HasValue", PrimaryILOffset = 7, MethodILOffset = 3)]
    public static void Test1(MyNullable<int> optInt)
    {
      int x = optInt.Value;
    }

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 3, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 18, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 7, MethodILOffset = 18)]
    public static int UseNullable(MyNullable<int> optY)
    {
      if (optY.HasValue)
      {
        return optY.Value;
      }
      return 0;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 4, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 12, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 7, MethodILOffset = 12)]
    public static int UseNullable1(int x)
    {
      MyNullable<int> y = new MyNullable<int>(x);

      return y.Value;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 10, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 7, MethodILOffset = 10)]
    public static int UseNullable2(int x)
    {
      MyNullable<int> y = x;

      return y.Value;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 8, MethodILOffset = 7)]
    public static void UseNullable3(int x)
    {
      Pass(x);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 8, MethodILOffset = 7)]
    public static void UseNullable4(int x)
    {
      Pass(new MyNullable<int>(x));
    }

    public static void Pass(MyNullable<int> x)
    {
      Contract.Requires(x.HasValue);

    }

  }
}
