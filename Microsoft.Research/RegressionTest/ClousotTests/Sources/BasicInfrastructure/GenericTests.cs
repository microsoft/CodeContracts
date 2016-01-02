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

// this outcome is due to AnalysisInfrastructure9 compiling all tests in the same mode. Not necessary in dev10 tests.
#if !NETFRAMEWORK_3_5 && !NETFRAMEWORK_4_0
[assembly:RegressionOutcome("Method 'BrianRepro.MyList`1.get_Item(System.Int32)' has custom parameter validation but assembly mode is not set to support this. It will be treated as Requires<E>.")]
#endif

namespace BrianRepro
{

  class CooperativeEvent
  {
    /// <summary>
    /// Disabled for cci2 because it depends on inference of getCount. Somehow cci2 is not providing correct
    /// attributes on getCount, and thus we fail here.
    /// </summary>
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Array creation : ok", PrimaryILOffset = 7, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Array creation : ok", PrimaryILOffset = 18, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 32, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 32, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as array)", PrimaryILOffset = 48, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as array)", PrimaryILOffset = 32, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 35, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"requires unproven: _size >= 0", PrimaryILOffset = 13, MethodILOffset = 2)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "Possibly calling a method on a null reference \'waitedOnTaskList\'", PrimaryILOffset = 2, MethodILOffset = 0)]
#if CLOUSOT2
//	#if NETFRAMEWORK_4_0
		[RegressionOutcome(Outcome=ProofOutcome.True,Message="requires is valid",PrimaryILOffset=1,MethodILOffset=35)]
//	#else
//		[RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 8, MethodILOffset = 35)]
//	#endif
#else
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 20, MethodILOffset = 35)]
#endif
    public void Foo(MyList<int> waitedOnTaskList)
    {
      CooperativeEvent[] cooperativeEventArray = new CooperativeEvent[waitedOnTaskList.Count];

      int[] arr = new int[MyList<int>.AnInt()];

      for (int index = 0; index < cooperativeEventArray.Length; index++)
      {
        cooperativeEventArray[index] = null;
        int x = waitedOnTaskList[index];
      }
    }


  }

  class MyList<T>
  {
    [ContractPublicPropertyName("Count")]
    private int _size = 0;

    public int Count
    {
      [ClousotRegressionTest("regular")]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 30, MethodILOffset = 46)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 2, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 37, MethodILOffset = 0)]
      get
      {
        Contract.Requires(_size >= 0);
        Contract.Ensures(Contract.Result<int>() >= 0);
        return _size;
      }
    }

    [ClousotRegressionTest("regular")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 12, MethodILOffset = 24)]
    static public int AnInt()
    {
      Contract.Ensures(Contract.Result<int>() >= 0);
      return 23;
    }

    public T this[int index]
    {
      [ClousotRegressionTest("regular")]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as field receiver)", PrimaryILOffset = 3, MethodILOffset = 0)]
      get
      {
        // Following trick can reduce the range check by one
        if ((uint)index >= (uint)_size)
        {
          throw new Exception();
        }
        Contract.EndContractBlock();

        // Do nothing ...
        return default(T);
      }

    }

    public T this[int index, bool dummy]
    {
      [ClousotRegressionTest("regular")]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=3,MethodILOffset=0)]
      get
      {
        Contract.Requires((uint)index >= (uint)_size);

        // Do nothing ...
        return default(T);
      }

    }

  }
}

namespace GenericInstantiatedContracts
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
      #region IFoo Members

    int IFoo.Foo(int x)
    {
      Contract.Requires(x >= 0);
      Contract.Ensures(Contract.Result<int>() >= 0);

      return 0;
 
      throw new NotImplementedException();
    }

    int IFoo.Value
    {
      get { throw new NotImplementedException(); }
    }

    #endregion
  }

  public class FooClass : IFoo
  {
    public int stored;

    [ClousotRegressionTest("generics")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 25, MethodILOffset = 6)]
    public int Foo(int x)
    {
      return x;
    }

    public int Value {
      [ClousotRegressionTest("generics")]
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

    [ClousotRegressionTest("generics")]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"invariant unproven: stored >= 0", PrimaryILOffset = 13, MethodILOffset = 8)]
    public FooStruct(int initial)
    {
      this.stored = initial;
    }

    [ClousotRegressionTest("generics")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 25, MethodILOffset = 13)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"invariant is valid", PrimaryILOffset = 13, MethodILOffset = 13)]
    public int Foo(int x)
    {
      return x + stored;
    }

    public int Value {
      [ClousotRegressionTest("generics")]
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

    // TODO: figure out why some of these requires fail
    // TODO: enable for cci2
    [ClousotRegressionTest("generics")]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"requires unproven: t.Value >= 0", PrimaryILOffset = 20, MethodILOffset = 44)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"requires unproven: t.Value >= 0", PrimaryILOffset = 19, MethodILOffset = 71)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"requires unproven: t.Value >= 0", PrimaryILOffset = 20, MethodILOffset = 97)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"requires unproven: t.Value >= 0", PrimaryILOffset = 19, MethodILOffset = 124)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"requires unproven: t.Value >= 0", PrimaryILOffset = 20, MethodILOffset = 155)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"requires unproven: t.Value >= 0",PrimaryILOffset=19,MethodILOffset=183)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 57, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 84, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 110, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 137, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 170, MethodILOffset = 0)]
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

namespace JPGRKRepro
{
  // Caused a crash in the heap analysis
  public sealed class WeakEvent<T> where T : class
  {
    private readonly List<T> list = new List<T>();

    [ContractInvariantMethod]
    void ObjectInvariant()
    {
      Contract.Invariant(list != null);
    }

       [ClousotRegressionTest("generics")]

    public static implicit operator T(WeakEvent<T> a)
    {
      List<Delegate> list;
      lock (a.list)
      {
        list = new List<Delegate>(a.list.Count);
      }
      Delegate result = Delegate.Combine(list.ToArray());
      return result as T;
    }
  }
}