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
using System.Text;
using System.Diagnostics.Contracts;
using Microsoft.Research.ClousotRegression;
using System.Globalization;

namespace Microsoft.Research.Clousot
{
  public class NonNullRegressions
  {

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 21, MethodILOffset = 0)]
    [RegressionOutcome(Outcome=ProofOutcome.False, Message = "assert is false", PrimaryILOffset=31, MethodILOffset=0)]
    static public void Basic1(string arg)
    {
      Contract.Requires(arg != null);

      Contract.Assert(arg != null);
      Contract.Assert(arg == null);
    }

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 21, MethodILOffset = 0)]
    static public void Path1(string arg)
    {
      if (arg == null) return;

      Contract.Assert(arg != null);
    }

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 25, MethodILOffset = 0)]
    static public void Path2(string arg)
    {
      if (arg == null) throw new Exception();

      Contract.Assert(arg != null);
    }

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"requires unproven: arg != null",PrimaryILOffset=8,MethodILOffset=2)]
#if CLOUSOT2
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Missing precondition in an externally visible method. Consider adding Contract.Requires(arg != null); for parameter validation",PrimaryILOffset=1,MethodILOffset=0)]
#else
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Missing precondition in an externally visible method. Consider adding Contract.Requires(arg != null); for parameter validation",PrimaryILOffset=2,MethodILOffset=0)]
#endif
    static public void Path3(string arg)
    {
      Basic1(arg);
    }

    [ClousotRegressionTest("regular"),
     RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 8, MethodILOffset = 11)]
    static public void Path4(string arg)
    {
      if (arg != null)
      {
        Basic1(arg);
      }
    }

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome=ProofOutcome.False,Message=@"requires is false: arg != null",PrimaryILOffset=8,MethodILOffset=14)]
    static public void Path5(string arg)
    {
      if (arg == null)
      {
        Basic1(arg);
      }
    }

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 28, MethodILOffset = 0)]
    unsafe void BinaryOpOnPointer1(int* ptr)
    {
      Contract.Requires(ptr != null);
      int* ptr2 = ptr + 2;
      Contract.Assert(ptr2 != null);
    }

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 28, MethodILOffset = 0)]
    unsafe void BinaryOpOnPointer2(int* ptr)
    {
      Contract.Requires(ptr != null);
      int* ptr2 = 2 + ptr;
      Contract.Assert(ptr2 != null);
    }

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "assert unproven", PrimaryILOffset = 14, MethodILOffset = 0)]
    unsafe void BinaryOpOnPointer3(int* ptr)
    {
      int* ptr2 = 2 + ptr;
      Contract.Assert(ptr2 != null);
    }

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 37, MethodILOffset = 0)]
    unsafe void UnaryOpOnPointer(int* ptr)
    {
      Contract.Requires(ptr != null);
      IntPtr iptr = (IntPtr)ptr;
      byte* bptr = (byte*)iptr;
      Contract.Assert(bptr != null);
    }

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 15, MethodILOffset = 0)]
    void TestBoxing(int x)
    {
      object o = x;

      Contract.Assert(o != null);
    }

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "assert unproven", PrimaryILOffset = 15, MethodILOffset = 0)]
    void TestCastClass1(object obj)
    {
      string s = (string)obj;

      Contract.Assert(s != null); // should fail.
    }

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 28, MethodILOffset = 0)]
    void TestCastClass2(object obj)
    {
      Contract.Requires(obj != null);

      string s = (string)obj;

      Contract.Assert(s != null);
    }

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 24, MethodILOffset = 0)]
    void TestIsInst(object obj)
    {
      string s = obj as string;
      if (s != null) {
        Contract.Assert(obj != null);
      }
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 25, MethodILOffset = 39)]
    public static int InvertSign2(int x)
    {
      Contract.Requires(x != 0);
      Contract.Ensures(Contract.Result<int>() != 0); //!! 
      int y = -x;
      return y;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=6,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="requires is valid",PrimaryILOffset=12,MethodILOffset=6)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="assert is valid",PrimaryILOffset=23,MethodILOffset=0)]
    int TestNegatedGTHasNoEffectInNonNull(out bool flag)
    {
      flag = false;
      uint val = GetVal(0);
      Contract.Assert(val <= int.MaxValue);
      return (int)val;
    }

    uint GetVal(uint x)
    {
      Contract.Requires(x <= int.MaxValue);     
      
      Contract.Ensures(Contract.Result<uint>() >= 0);
      Contract.Ensures(Contract.Result<uint>() <= int.MaxValue);

      return 1;
    }

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome=ProofOutcome.Top, Message = "assert unproven", PrimaryILOffset=8, MethodILOffset=0)]
#if CLOUSOT2
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Missing precondition in an externally visible method. Consider adding Contract.Requires(args != null); for parameter validation",PrimaryILOffset=1,MethodILOffset=0)]
#else
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Missing precondition in an externally visible method. Consider adding Contract.Requires(args != null); for parameter validation",PrimaryILOffset=3,MethodILOffset=0)]
#endif
    static public void Main(string[] args)
    {
      Contract.Assert(args != null);
    }
  }

  class ConstructorTestBase {
  }

  class ConstructorTestDerived {

    object data;

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 6, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 14, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 15, MethodILOffset = 6)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 25, MethodILOffset = 0)]
    public ConstructorTestDerived()
      : this(new Object())
    {
      Contract.Assert(data != null);
    }

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 1, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 40, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 56, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 22, MethodILOffset = 62)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 48, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 33, MethodILOffset = 62)]
    public ConstructorTestDerived(object obj)
    {
      Contract.Requires(obj != null);
      Contract.Ensures(this.data != null);
      Contract.Assert(data == null);
      data = obj;
    }

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 13, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 15, MethodILOffset = 6)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 24, MethodILOffset = 0)]
    public void TestConstructorEnsures()
    {
      ConstructorTestDerived cd = new ConstructorTestDerived(new Object());
      Contract.Assert(cd.data != null);
    }
  }

  class NonNullInvariantTest {
    object data;

    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(data != null);
    }

    public NonNullInvariantTest(object obj)
    {
      Contract.Requires(obj != null);
      this.data = obj;
    }

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 6, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 15, MethodILOffset = 6)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 19, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 13, MethodILOffset = 39)]
    public NonNullInvariantTest(int arg)
      : this(new Object())
    {
      for (int i = 0; i < arg; i++) {
        Push();
      }
    }

    void Push() { }

  }

  class TypeParameterBox1<T>
  {
    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 19, MethodILOffset = 0)]
    public bool Contains(T item)
    {
      if ((Object)item == null)
      {
        Contract.Assert(true); // reachable
        return false;
      }
      else
      {
        return false;
      }
    }
  }

  class TypeParameterBox2<T> where T:struct
  {
    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 19, MethodILOffset = 0)]
    public bool Contains(T item)
    {
      if ((Object)item == null)
      {
        Contract.Assert(false); // unreachable, but we set as valid as it is "assert false"
        return false;
      }
      else
      {
        return false;
      }
    }
  }

  static class GenericMethodBoxing
  {
    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 8, MethodILOffset = 11)]
    static void TestMain1()
    {
      string s = null;
      s = AssumesNonNullWithConstraint(s);
      RequiresNonNull(s);
    }

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 8, MethodILOffset = 11)]
    static void TestMain2()
    {
      string s = null;
      s = AssumesNonNullWithoutConstraint(s);
      RequiresNonNull(s);
    }

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 11, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 8, MethodILOffset = 18)]
    static void TestMain3()
    {
      var c = new GenericTypeBoxingWithConstraint<string>();
      string s = null;
      s = c.AssumesNonNullWithoutConstraint(s);
      RequiresNonNull(s);
    }

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 11, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 8, MethodILOffset = 18)]
    static void TestMain4()
    {
      var c = new GenericTypeBoxingWithoutConstraint<string>();
      string s = null;
      s = c.AssumesNonNullWithoutConstraint(s);
      RequiresNonNull(s);
    }

    class Foo { }
    class Bar { }

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 17, MethodILOffset = 0)]
    static void TestMain5()
    {
      Foo arg = null;
      arg = GenericNestedInstanceTests<Foo>.Test<Bar>.AssumesNonNull(arg);
      Contract.Assert(arg != null);
    }

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 17, MethodILOffset = 0)]
    static void TestMain6()
    {
      Bar arg = null;
      arg = GenericNestedInstanceTests<Foo>.Test<Bar>.AssumesNonNull(arg);
      Contract.Assert(arg != null);
    }

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 17, MethodILOffset = 0)]
    static void TestMain7()
    {
      string arg = null;
      arg = GenericNestedInstanceTests<Foo>.Test<Bar>.AssumesNonNull(arg);
      Contract.Assert(arg != null);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 17, MethodILOffset = 46)]
    static T AssumesNonNullWithConstraint<T>(T arg) where T:class
    {
      Contract.Ensures(Contract.Result<T>() != null);
      Contract.Assume(arg != null);
      return arg;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 17, MethodILOffset = 46)]
    static T AssumesNonNullWithoutConstraint<T>(T arg)
    {
      Contract.Ensures(Contract.Result<T>() != null);
      Contract.Assume(arg != null);
      return arg;
    }

    static void RequiresNonNull(string arg)
    {
      Contract.Requires(arg != null);
    }
  }

  class GenericTypeBoxingWithConstraint<T> where T : class
  {
    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 17, MethodILOffset = 46)]
    public T AssumesNonNullWithoutConstraint(T arg)
    {
      Contract.Ensures(Contract.Result<T>() != null);
      Contract.Assume(arg != null);
      return arg;
    }
  }
  class GenericTypeBoxingWithoutConstraint<T> 
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 17, MethodILOffset = 46)]
    public T AssumesNonNullWithoutConstraint(T arg)
    {
      Contract.Ensures(Contract.Result<T>() != null);
      Contract.Assume(arg != null);
      return arg;
    }
  }
  class GenericNestedInstanceTests<Outer>
  {
    public class Test<Inner>
    {
      public static Inner AssumesNonNull(Inner arg)
      {
        Contract.Ensures(Contract.Result<Inner>() != null);
        Contract.Assume(arg != null);
        return arg;
      }

      public static Outer AssumesNonNull(Outer arg)
      {
        Contract.Ensures(Contract.Result<Outer>() != null);
        Contract.Assume(arg != null);
        return arg;
      }

      public static MVar AssumesNonNull<MVar>(MVar arg)
      {
        Contract.Ensures(Contract.Result<MVar>() != null);
        Contract.Assume(arg != null);
        return arg;
      }
    }
  }
  class TestUnboxing
  {
    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "Possibly unboxing a null reference 'obj'", PrimaryILOffset = 2, MethodILOffset = 0)]
    public int Test1(object obj)
    {
      int x = (int)obj;
      return x;
    }

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "Possibly unboxing a null reference 'obj'", PrimaryILOffset = 2, MethodILOffset = 0)]
    public T Test2<T>(object obj)
    {
      T v = (T)obj;
      return v;
    }
  }

  class TestArray
  {
    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "Possible use of a null array 'obj'", PrimaryILOffset = 3, MethodILOffset = 0)]
    public int Test1(int[] obj)
    {
      int x = obj[5];
      return x;
    }

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "Possible use of a null array 'obj'", PrimaryILOffset = 4, MethodILOffset = 0)]
    public void Test2(int[] obj)
    {
      obj[5] = 0;
    }

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "Possible use of a null array 'obj'", PrimaryILOffset = 3, MethodILOffset = 0)]
    public void Test3(int[] obj)
    {
      Pass(ref obj[5]);
    }

    static void Pass(ref int x) { }
  }

  /// <summary>
  /// We must treat Object.ReferenceEqual like Ceq
  /// </summary>
  class ReferenceEqual
  {
    [ClousotRegressionTest("regular")]
    public static bool operator ==(ReferenceEqual v1, ReferenceEqual v2)
    {
      if (object.ReferenceEquals(v1, null))
      {
        return object.ReferenceEquals(v2, null);
      }
      return v1.Equals(v2);
    }

    public static bool operator !=(ReferenceEqual v1, ReferenceEqual v2)
    {
      return !(v1 == v2);
    }

    public override bool Equals(object obj)
    {
      return base.Equals(obj);
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }
  }

  struct Version
  {
    int x;

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (in unbox)", PrimaryILOffset = 32, MethodILOffset = 0)]
    static int TestStructOnStack(object obj)
    {
      Contract.Requires(obj != null);

      if (obj is Version)
      {
        int a = ((Version)obj).x;
        return a;
      }
      return 0;
    }

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (in unbox)", PrimaryILOffset = 32, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 40, MethodILOffset = 0)]
    static void TestStructOnStack3(object obj)
    {
      Contract.Requires(obj != null);

      if (obj is Version)
      {
        ((Version)obj).Foo();
      }
    }

    void Foo()
    {
      x = 5;
    }

    Version(int x)
    {
      this.x = x;
    }

    [ClousotRegressionTest("regular")]
    public static Version operator -(Version t)
    {
      if (t.x == MinValue.x)
      {
        throw new OverflowException();
      }
      return new Version(-t.x);
    }

    [ClousotRegressionTest("regular")]
    public static int TestStaticNonReadonly()
    {
      int x = OtherValue.x;
      return x;
    }


    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 15, MethodILOffset = 0)]
    public int TestReadonlyInstanceField(TestReadOnlyStructField c)
    {
      Contract.Requires(c != null);
      return c.x.x;
    }

    public int TestMutableInstanceField(TestReadOnlyStructField c)
    {
      return c.y.x;
    }

    public static readonly Version MinValue = new Version();
    public static Version OtherValue = new Version();
  }

  class TestReadOnlyStructField
  {
    public readonly Version x = new Version();
    public Version y = new Version();

  }

  class NonNullReceiverWithPrecondition
  {
    class MyEnvironment
    {
      [Pure]
      internal static string GetResourceString(string p)
      {
        throw new NotImplementedException();
      }
    }

    public virtual void TestMe(string a, string b)
    {
      if (a != null) throw new ArgumentException();
      if (b != null) throw new ArgumentException();
      Contract.EndContractBlock();

    }

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 35, MethodILOffset = 0)]
#if CLOUSOT2
//    #if !NETFRAMEWORK_4_0
		[RegressionOutcome(Outcome=ProofOutcome.True,Message="requires is valid",PrimaryILOffset=1,MethodILOffset=35)]
		[RegressionOutcome(Outcome=ProofOutcome.True,Message="requires is valid",PrimaryILOffset=15,MethodILOffset=35)]
//	#else
//		[RegressionOutcome(Outcome=ProofOutcome.True,Message="requires is valid",PrimaryILOffset=3,MethodILOffset=35)]
//		[RegressionOutcome(Outcome=ProofOutcome.True,Message="requires is valid",PrimaryILOffset=17,MethodILOffset=35)]
//	#endif
#else
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 14, MethodILOffset = 35)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 28, MethodILOffset = 35)]
#endif
    public static void Test(bool b)
    {
      for (int i = 0; i < 2; i++)
      {
        NonNullReceiverWithPrecondition r;
        if (b)
        {
          r = new NonNullReceiverWithPrecondition();
        }
        else
        {
          r = new NonNullReceiverWithPrecondition();
        }

        r.TestMe(null, null); // use null to make it more likely to cause an error if we grab the wrong offset.
      }
    }

  }

  class FrancescoFromVB
  {
    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 31, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as array)", PrimaryILOffset = 57, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 8, MethodILOffset = 31)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 44, MethodILOffset = 0)]
    void FromVB(bool b)
    {
      String[] a = new string[1];
      int j = 0;
      while (b)
      {
        j = j + 1;

        int tmp = j + 1;
        var old = a;
        a = Copy(a, new string[tmp]);

        Contract.Assert(a != null);

        a[j] = "";

      }
    }

    /// <summary>
    /// Not part of regression, as the equality reasoning w/o bounds isn't up to it.
    /// </summary>
    private string[] Copy(string[] from, string[] to)
    {
      Contract.Requires(to != null);

      Contract.Ensures(Contract.Result<string[]>() == to);

      return to;
    }
  }

  class MyEventArgs : EventArgs
  {
  }

  class EventArgsTests
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 8, MethodILOffset = 0)]
    public void TestEventArgs1(object sender, EventArgs evargs)
    {
      Contract.Assert(evargs != null);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 8, MethodILOffset = 0)]
    public static void TestEventArgs2(object sender, EventArgs evargs)
    {
      Contract.Assert(evargs != null);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 8, MethodILOffset = 0)]
    public void TestEventArgs3(object sender, MyEventArgs evargs)
    {
      Contract.Assert(evargs != null);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 8, MethodILOffset = 0)]
    public static void TestEventArgs4(object sender, MyEventArgs evargs)
    {
      Contract.Assert(evargs != null);
    }
  }

  class FalsePositives
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="assert unproven",PrimaryILOffset=80,MethodILOffset=0)]
    public int Loop(int y)
    {
      //Contract.Ensures(/*y >= 0 && Contract.Result<int>() >= 0 ||*/ y < 0 && Contract.Result<int>() <= 0);

      // Contract.Ensures(y >= 0 || Contract.Result<int>() < 0);

      int oldy = y;
      int x = 0;

      while (y != 0)
      {
        //Contract.Assert(oldy < 0 && x <= 0 || oldy > 0 && x >= 0);

        if (y > 0)
        {
          if (y % 3 == 0)
          {
            x++;
          }
          y--;
        }
        else
        {
          if (y % 4 == 0)
          {
            x--;
          }
          y++;
        }

        Contract.Assert(y != 0);

        //Contract.Assert(oldy < 0 && x <= 0 || oldy > 0 && x >= 0);
      }

      return x;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.False,Message="ensures is false: Contract.Result<Dictionary<object, object>>() != null (Case0)",PrimaryILOffset=17,MethodILOffset=28)]
    public Dictionary<object, object> TestSourceContext0() {
      Contract.Ensures( Contract.Result<Dictionary<object, object>>() != null, "Case0" );
      return null;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.False,Message="ensures is false: Contract.Result<string>() == \",\" (Case1)",PrimaryILOffset=21,MethodILOffset=32)]
    public string TestSourceContext1() {
      Contract.Ensures( Contract.Result<string>() == ",", "Case1" );
      return null;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.False,Message="ensures is false: Contract.Result<string>() == \"\\\",\" (Case2)",PrimaryILOffset=21,MethodILOffset=32)]
    public string TestSourceContext2() {
      Contract.Ensures( Contract.Result<string>() == "\",", "Case2" );
      return null;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.False,Message="ensures is false: Contract.Result<string>() == \"\\\"\\\",\" (Case3)",PrimaryILOffset=21,MethodILOffset=32)]
    public string TestSourceContext3() {
      Contract.Ensures( Contract.Result<string>() == "\"\",", "Case3" );
      return null;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.False,Message="ensures is false: Contract.Result<string>() == \"',\" (Case4)",PrimaryILOffset=21,MethodILOffset=32)]
    public string TestSourceContext4() {
      Contract.Ensures( Contract.Result<string>() == "',", "Case4" );
      return null;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.False,Message="ensures is false: Contract.Result<string>() == \"'',\" (Case5)",PrimaryILOffset=21,MethodILOffset=32)]
    public string TestSourceContext5() {
      Contract.Ensures( Contract.Result<string>() == "'',", "Case5" );
      return null;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.False,Message="ensures is false: Contract.Result<char>() == '\"' (Case6)",PrimaryILOffset=15,MethodILOffset=26)]
    public char TestSourceContext6() {
      Contract.Ensures( Contract.Result<char>() == '"', "Case6" );
      return default(char);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.False,Message="ensures is false: Contract.Result<char>() == '\\'' (Case7)",PrimaryILOffset=15,MethodILOffset=26)]
    public char TestSourceContext7() {
      Contract.Ensures( Contract.Result<char>() == '\'', "Case7" );
      return default(char);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.False,Message="ensures is false: Contract.Result<char>() == ')' (Case8)",PrimaryILOffset=15,MethodILOffset=26)]
    public char TestSourceContext8() {
      Contract.Ensures( Contract.Result<char>() == ')', "Case8" );
      return default(char);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.False,Message="ensures is false: Contract.Result<string>() == \")tadah\" (Case9)",PrimaryILOffset=21,MethodILOffset=32)]
    public string TestSourceContext9() {
      Contract.Ensures( Contract.Result<string>() == ")tadah", "Case9" );
      return null;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.False,Message="ensures is false: Contract.Result<string>() == \">tadah\" (Case10)",PrimaryILOffset=21,MethodILOffset=32)]
    public string TestSourceContext10() {
      Contract.Ensures( Contract.Result<string>() == ">tadah", "Case10" );
      return null;
    }
  }
}


namespace HermanAI
{
 
  public abstract class Instruction
  {
    public Instruction Operand1;
   
  }
 
  public sealed class Interval
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=25,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="assert unproven",PrimaryILOffset=81,MethodILOffset=0)]
    internal static Interval/*?*/ TryToGetAsInterval<MyInstruction>(MyInstruction expression)
    where MyInstruction : Instruction, new()
    {
      Contract.Requires(expression != null);
 
 
      var operand1 = expression.Operand1 as MyInstruction;
      if (operand1 == null)
      {
        Contract.Assert(expression.ToString() == "a"); // reported unreached
        return null;
      }
 
      return null;
    }
  }
}

namespace ContractLabs
{
    class NonNullImplicationLab
    {
      [ClousotRegressionTest]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="ensures is valid",PrimaryILOffset=14,MethodILOffset=33)]
      public T Foo<T>()
        where T : class
      {
        Contract.Ensures(Contract.Result<T>() == null);

        return null; // ensures unproven: Contract.Result<T>() == null
      }

      [ClousotRegressionTest]
      [RegressionOutcome(Outcome=ProofOutcome.Top,Message="ensures unproven: Contract.Result<T>() != null",PrimaryILOffset=17,MethodILOffset=36)]
      public T Bar<T>()
      {
        Contract.Ensures(Contract.Result<T>() != null);

        return default(T);
      }
    }
}

namespace SourceExtraction {

  class SameLineRequires {

    public void M(string s) { Contract.Requires(s != null); }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.False,Message="requires is false: s != null",PrimaryILOffset=8,MethodILOffset=3)]
    public void Test() {
      M(null);
    }
  }
}


