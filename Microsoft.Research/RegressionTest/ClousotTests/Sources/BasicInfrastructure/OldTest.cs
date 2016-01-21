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

[assembly: RegressionOutcome("Detected call to method 'System.Object.Equals(System.Object)' without [Pure] in contracts of method 'OldTests.GenericWithOld`1.Set(type parameter.T)'.")]

namespace OldTests
{
  class FibonacciHeapCell
  {
    FibonacciHeapCell mParent;
    internal FibonacciHeapLinkedList Children { get; set;  }
    internal FibonacciHeapCell Parent
    {
      get
      {
        Contract.Ensures(Contract.Result<FibonacciHeapCell>() == null || Contract.Result<FibonacciHeapCell>().Children != null); //Commented out because nowhere picks up this contract
        return mParent;
      }
      set
      {
        mParent = value;
      }
    }
    internal FibonacciHeapCell Next { get; set; }
    internal FibonacciHeapCell Previous { get; set; }
    internal int Count { get; set; }
  }

  class FibonacciHeapLinkedList
  {
    internal void AddLast(FibonacciHeapCell Node)
    {
      Contract.Requires(Node.Previous == null);
      Contract.Requires(Node.Count <= 0); // Add this to test old in numerical domains

    }
    internal void Remove(FibonacciHeapCell Node)
    {
      Contract.Requires<ArgumentNullException>(Node != null);
      Contract.Ensures(Node.Next == null);
      Contract.Ensures(Node.Previous == null);
      Contract.Ensures(Node.Count <= 0);


      Node.Next = null;
      Node.Previous = null;
    }

  }

  class Roy
  {
    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 41, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 52, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 59, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 82, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 89, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 8, MethodILOffset = 52)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 67, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 10, MethodILOffset = 82)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 28, MethodILOffset = 82)]
    private static void BadTest(FibonacciHeapCell Node, FibonacciHeapLinkedList children, FibonacciHeapLinkedList others)
    {
      Contract.Requires<ArgumentNullException>(Node != null);
      Contract.Requires<ArgumentNullException>(children != null);
      Contract.Requires<ArgumentNullException>(others != null);

      var parentNode = Node.Parent;
      while (parentNode != null)
      {
        children.Remove(parentNode);
        Contract.Assert(parentNode.Previous == null);
        UpdateNodesDegree(parentNode);
        others.AddLast(parentNode);
        parentNode = parentNode.Parent;
      }
    }

    private static void UpdateNodesDegree(FibonacciHeapCell parentNode)
    {
      Contract.Ensures(parentNode.Previous == Contract.OldValue(parentNode.Previous));
      Contract.Ensures(parentNode.Next == Contract.OldValue(parentNode.Next));
      Contract.Ensures(parentNode.Count == Contract.OldValue(parentNode.Count));

    }


  }

  public class NestedOldTest
  {
    public struct A
    {
      public B b;
    }
    public struct B
    {
      public C c;
    }
    public struct C
    {
      public int x;
    }

    [Pure]
    static int GetX(A a)
    {
      Contract.Ensures(Contract.Result<int>() == Contract.OldValue(a.b.c.x));
      
      int x = a.b.c.x;
      return x;
    }

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 24, MethodILOffset = 45)]
    static int Test(ref A a)
    {
      Contract.Ensures(Contract.Result<int>() == Contract.OldValue(GetX(a)));
      return GetX(a);
    }
  }

  public class CallOnStructWithinOldTest {

    public struct T
    {
      public int Y { get; set;  }
    }

    public struct S
    {
      public int X { get; set; }
      private T t;
      public T T
      {
        get { return this.t; }
        set { this.t = value; }
      }
    }

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as receiver)", PrimaryILOffset = 48, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as receiver)", PrimaryILOffset = 56, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as receiver)", PrimaryILOffset = 64, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as receiver)", PrimaryILOffset = 27, MethodILOffset = 73)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"ensures unproven: s.X > 0", PrimaryILOffset = 11, MethodILOffset = 73)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 39, MethodILOffset = 73)]
    static int Test(S s)
    {
      Contract.Ensures(s.X > 0); // wrong and useless, but should work and not crash
      Contract.Ensures(s.T.Y == Contract.Result<int>()); 

      s.X = 5;
      return s.T.Y;
    }
  }


  class AccountExample
  {
    public int Balance { get; private set; }

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as receiver)", PrimaryILOffset = 40, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as receiver)", PrimaryILOffset = 47, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as receiver)", PrimaryILOffset = 12, MethodILOffset = 53)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 32, MethodILOffset = 53)]
    public void Deposit(int amount)
    {
      Contract.Requires(amount > 0);
      Contract.Ensures(Balance == Contract.OldValue(Balance) + amount);

      Balance = Balance + amount;
    }
  }

  class GenericWithOld<T>
  {
    public T Field;

    // TODO: support Equals in contracts
    public void Set(T value)
    {
      Contract.Ensures(this.Field.Equals(value));
      Field = value;
    }
  }

  class TestGenericInstanceWithOld
  {
    // TODO, once we support equals, this should work 
    public static void Test1()
    {
      var v = new GenericWithOld<string>();
      var x = "foo";
      v.Set(x);
      Contract.Assert(v.Field == x);
    }

    struct S
    {
      [ClousotRegressionTest("regular")]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 43, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 50, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 15, MethodILOffset = 55)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 35, MethodILOffset = 55)]
      public S(int a, int b)
      {
        Contract.Ensures(Contract.ValueAtReturn(out this.x) == a);
        Contract.Ensures(Contract.ValueAtReturn(out this.y) == b);
        this.x = a;
        this.y = b;
      }
      public int x;
      public int y;
    }

    // TODO, once we support Equals, this should pass
    public static void Test2()
    {
      var v = new GenericWithOld<S>();
      var s = new S(5,6);
      Contract.Assert(s.x == 5);

      v.Set(s);
      Contract.Assert(v.Field.y == 6);
    }
  }
}

namespace OldScopeInference
{
  struct S
  {
    public int X;
  }

  class OldWithoutEnd
  {
    /// <summary>
    /// Tests an issue with oldscope inference in ensures with ldarga. In this example, there is no actual memory
    /// access happening in the old state. Still, we need to end the old scope. We now do so at any instructions
    /// other than nop and ldflda.
    /// </summary>
    /// <param name="s"></param>
    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 16, MethodILOffset = 22)]
    unsafe public static void Test(S s)
    {
      Contract.Ensures(&s.X != null);
    }

    [Pure]
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 23, MethodILOffset = 42)]
    public static bool Predicate1(int data, ref S s)
    {
      Contract.Ensures(Contract.Result<bool>() || data != s.X);
      return data == s.X;
    }

    [Pure]
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 42, MethodILOffset = 61)]
    public static bool Predicate2(ref S s, int data)
    {
      Contract.Ensures(Contract.Result<bool>() && data == s.X || !Contract.Result<bool>() && data != s.X);
      return data == s.X;
    }

    /// <summary>
    /// This is a weird case, where we want to refer to the pre state of s, in the post condition.
    /// However, s is only dereferenced in WeirdMethod, and we cannot wrap old around that.
    /// What happens is that WeirdMethod gets evaluated in the new state because, and thus we effectively
    /// read the post state of s. Thus the ensures fails, even though it should succeed.
    /// </summary>
    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 38, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 29, MethodILOffset = 48)]
    unsafe public static int WeirdPost1(S s)
    {
      Contract.Requires(s.X == 0);
      Contract.Ensures(Predicate1(Contract.Result<int>(), ref s)); // should be valid

      s.X = 5;
      return 0;
    }

    /// <summary>
    /// Like WeirdPost1, showing that indeed we evaluate s.X in the post state.
    /// The parameter order of Predicate2 should not matter
    /// </summary>
    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 22, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"ensures unproven: Predicate2(ref s, Contract.Result<int>())", PrimaryILOffset = 13, MethodILOffset = 32)]
    unsafe public static int WeirdPost2(S s)
    {
      Contract.Ensures(Predicate2(ref s, Contract.Result<int>())); // should not be valid!

      s.X = 0;
      return 0;
    }

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 18, MethodILOffset = 0)]
    public static void Caller1(S s)
    {
      int result = WeirdPost2(s);
      Contract.Assert(result == s.X); // can't prove it due to our handling of struct copies and the weird by-ref predicate
    }


  }
}