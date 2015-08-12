// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Microsoft.Research.ClousotRegression;

public class Test
{
    [ClousotRegressionTest]
    [RegressionOutcome("Contract.Ensures(Contract.Result<System.Boolean>() == true);")]
#if FIRST
    [RegressionOutcome("No entry found in the cache")]
#else
    [RegressionOutcome("An entry has been found in the cache")]
#endif
    private bool Type_Primitive()
    {
        // Contract.Ensures(Contract.Result<bool>() == true);
        return true;
    }

    [ClousotRegressionTest]
    [RegressionOutcome("Contract.Ensures(Contract.Result<System.Object>() != null);")]
#if FIRST
    [RegressionOutcome("No entry found in the cache")]
#else
    [RegressionOutcome("An entry has been found in the cache")]
#endif
    private object Type_Object()
    {
        // Contract.Ensures(Contract.Result<object>() != null);
        return new Object();
    }

    [ClousotRegressionTest]
    // No inference from Clousot - TODO
#if FIRST
    [RegressionOutcome("No entry found in the cache")]
#else
    [RegressionOutcome("An entry has been found in the cache")]
#endif
    private T Type_FormalMethodParamter<A, C, G, T>()
      where T : new()
    {
        // Contract.Ensures(Contract.Result<T>() != null);
        return new T();
    }

    [ClousotRegressionTest]
    [RegressionOutcome("Contract.Ensures(Contract.Result<System.Int32[]>() != null);")]
    [RegressionOutcome("Contract.Ensures(Contract.Result<System.Int32[]>().Length == 0);")]
#if FIRST
    [RegressionOutcome("No entry found in the cache")]
#else
    [RegressionOutcome("An entry has been found in the cache")]
#endif
    private int[] Type_Array()
    {
        // Contract.Ensures(Contract.Result<int[]>() != null);
        return new int[0];
    }

    [ClousotRegressionTest]
    [RegressionOutcome("Contract.Ensures(Contract.Result<System.Int32[,,]>() != null);")]
#if FIRST
    [RegressionOutcome("No entry found in the cache")]
#else
    [RegressionOutcome("An entry has been found in the cache")]
#endif
    private int[,,] Type_Array3()
    {
        // Contract.Ensures(Contract.Result<int[,,]>() != null);
        return new int[0, 0, 0];
    }

    [ClousotRegressionTest]
    [RegressionOutcome("Contract.Ensures(Contract.Result<System.Collections.Generic.List<System.Int32>>() != null);")]
    [RegressionOutcome("Contract.Ensures(Contract.Result<System.Collections.Generic.List<System.Int32>>().Count == 0);")]
#if FIRST
    [RegressionOutcome("No entry found in the cache")]
#else
    [RegressionOutcome("An entry has been found in the cache")]
#endif
    private List<int> Type_Specialized()
    {
        // Contract.Ensures(Contract.Result<List<int>>() != null);
        return new List<int>();
    }

    public class DummyClass
    {
        public class NestedClass
        {
            public object DummyField;
        }
    }

    [ClousotRegressionTest]
#if !CLOUSOT2
    [RegressionOutcome("Contract.Ensures(Contract.Result<Test+DummyClass+NestedClass>() != null);")]
#else
[RegressionOutcome("Contract.Ensures(Contract.Result<Test.DummyClass.NestedClass>() != null);")]
#endif
#if FIRST
    [RegressionOutcome("No entry found in the cache")]
#else
    [RegressionOutcome("An entry has been found in the cache")]
#endif
    private DummyClass.NestedClass Type_NestedType()
    {
        // Contract.Ensures(Contract.Result<DummyClass.NestedClass>() != null);
        return new DummyClass.NestedClass();
    }

    [ClousotRegressionTest]
    [RegressionOutcome("Contract.Requires(x != null);")]
    [RegressionOutcome("Contract.Ensures(Contract.Result<System.Object>() == x.DummyField);")]
#if FIRST
    [RegressionOutcome("No entry found in the cache")]
#else
    [RegressionOutcome("An entry has been found in the cache")]
#endif
    public object Type_NestedType(DummyClass.NestedClass x)
    {
        // Contract.Requires(x != null);
        // Contract.Ensures(Contract.Result<object>() == x.DummyField);
        return x.DummyField;
    }


    [ClousotRegressionTest]
    [RegressionOutcome("Contract.Ensures(Contract.Result<System.Boolean>() == (((Test)(x)) == this));")]
    // [RegressionOutcome("Contract.Ensures(Contract.Result<System.Boolean>() == (x == this));")]
#if FIRST
    [RegressionOutcome("No entry found in the cache")]
#else
    [RegressionOutcome("An entry has been found in the cache")]
#endif
    private bool Parameters1(object x, object y)
    {
        // Contract.Ensures(Contract.Result<bool>() == ((x == this)));
        return x == this;
    }

    [ClousotRegressionTest]
    [RegressionOutcome("Contract.Ensures(Contract.Result<System.Boolean>() == (x == y));")]
#if FIRST
    [RegressionOutcome("No entry found in the cache")]
#else
    [RegressionOutcome("An entry has been found in the cache")]
#endif
    private bool Parameters2(object x, object y)
    {
        // Contract.Ensures(Contract.Result<bool>() == ((x == y) ));
        return x == y;
    }
}

public class GenericClass<A, C, G, T>
  where A : class, C
  where C : class
  where T : class
{
    // nothing (default)
    [ClousotRegressionTest]
    [RegressionOutcome("Contract.Ensures(Contract.Result<T>() == null);")]
#if FIRST
    [RegressionOutcome("No entry found in the cache")]
#else
    [RegressionOutcome("An entry has been found in the cache")]
#endif
    private T Type_FormalTypeParameter()
    {
        // Contract.Ensures(Contract.Result<T>() == default(T));
        return default(T);
    }

    // nothing (default)
    [ClousotRegressionTest]
    [RegressionOutcome("Contract.Ensures(Contract.Result<T>() == null);")]
#if FIRST
    [RegressionOutcome("No entry found in the cache")]
#else
    [RegressionOutcome("An entry has been found in the cache")]
#endif
    private T Type_FormalTypeParameterResultNull()
    {
        // Contract.Ensures(Contract.Result<T>() == null);
        return null;
    }

    [Pure]
    private C c(A a) { return a; }

    [Pure]
    private static C sc(A a) { return a; }

    [ClousotRegressionTest]
    // method call
#if FIRST
    [RegressionOutcome("No entry found in the cache")]
#else
    [RegressionOutcome("An entry has been found in the cache")]
#endif
    private bool Method_TypeArguments(A a, C c)
    {
        // Contract.Ensures(Contract.Result<bool>() == (this.c(a) == c));
        return this.c(a) == c;
    }

    [ClousotRegressionTest]
    // TODO: fix the Output of the postcondition, because GenericClass takes 4 arguments
    //[RegressionOutcome("Contract.Ensures(Contract.Result<System.Boolean>() == (GenericClass.sc(a) == c));")]
    [RegressionOutcome("Contract.Ensures(Contract.Result<System.Boolean>() == (GenericClass.sc(a).Equals(c)));")]
#if FIRST
    [RegressionOutcome("No entry found in the cache")]
#else
    [RegressionOutcome("An entry has been found in the cache")]
#endif
    private bool Method_Static(A a, C c)
    {
        // Contract.Ensures(Contract.Result<bool>() == (sc(a) == c));
        return sc(a) == c;
    }

    [Pure]
    private U f<U>(bool b, U x) { return b ? x : default(U); }

    [ClousotRegressionTest]
    // generics
#if FIRST
    [RegressionOutcome("No entry found in the cache")]
#else
    [RegressionOutcome("An entry has been found in the cache")]
#endif
    private A Method_Specialized(bool b, A a)
    {
        // Contract.Ensures(Contract.Result<A>() == this.f<A>(b, a));
        return this.f(b, a);
    }

    [ClousotRegressionTest]
#if FIRST
    [RegressionOutcome("No entry found in the cache")]
#else
    [RegressionOutcome("An entry has been found in the cache")]
#endif
    private U Method_Generic<X, U, Y>(bool b, U u)
      where U : class
    {
        // Contract.Ensures(Contract.Result<U>() == this.f<U>(b, u));
        return this.f(b, u);
    }
}
