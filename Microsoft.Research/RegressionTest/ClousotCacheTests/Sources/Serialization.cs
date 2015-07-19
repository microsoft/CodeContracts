// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Microsoft.Research.ClousotRegression;

internal class Test
{
    [ClousotRegressionTest]
#if FIRST
    [RegressionOutcome("No entry found in the cache")]
#else
    [RegressionOutcome("An entry has been found in the cache")]
#endif
    private bool Type_Primitive()
    {
        Contract.Ensures(Contract.Result<bool>() == true);
        return true;
    }

    [ClousotRegressionTest]
#if FIRST
    [RegressionOutcome("No entry found in the cache")]
#else
    [RegressionOutcome("An entry has been found in the cache")]
#endif
    private object Type_Object()
    {
        Contract.Ensures(Contract.Result<object>() != null);
        return new Object();
    }

    [ClousotRegressionTest]
#if FIRST
    [RegressionOutcome("No entry found in the cache")]
#else
    [RegressionOutcome("An entry has been found in the cache")]
#endif
    private T Type_FormalMethodParamter<A, C, G, T>()
      where T : new()
    {
        Contract.Ensures(Contract.Result<T>() != null);
        return new T();
    }

    [ClousotRegressionTest]
#if FIRST
    [RegressionOutcome("No entry found in the cache")]
#else
    [RegressionOutcome("An entry has been found in the cache")]
#endif
    private int[] Type_Array()
    {
        Contract.Ensures(Contract.Result<int[]>() != null);
        return new int[0];
    }

    [ClousotRegressionTest]
#if FIRST
    [RegressionOutcome("No entry found in the cache")]
#else
    [RegressionOutcome("An entry has been found in the cache")]
#endif
    private int[,,] Type_Array3()
    {
        Contract.Ensures(Contract.Result<int[,,]>() != null);
        return new int[0, 0, 0];
    }

    [ClousotRegressionTest]
#if FIRST
    [RegressionOutcome("No entry found in the cache")]
#else
    [RegressionOutcome("An entry has been found in the cache")]
#endif
    private List<int> Type_Specialized()
    {
        Contract.Ensures(Contract.Result<List<int>>() != null);
        return new List<int>();
    }

    private class DummyClass
    {
        public class NestedClass
        {
            public object DummyField;
        }
    }

    [ClousotRegressionTest]
#if FIRST
    [RegressionOutcome("No entry found in the cache")]
#else
    [RegressionOutcome("An entry has been found in the cache")]
#endif
    private DummyClass.NestedClass Type_NestedType()
    {
        Contract.Ensures(Contract.Result<DummyClass.NestedClass>() != null);
        return new DummyClass.NestedClass();
    }

    [ClousotRegressionTest]
#if FIRST
    [RegressionOutcome("No entry found in the cache")]
#else
    [RegressionOutcome("An entry has been found in the cache")]
#endif
    private object Type_NestedType(DummyClass.NestedClass x)
    {
        Contract.Requires(x != null);
        Contract.Ensures(Contract.Result<object>() == x.DummyField);
        return x.DummyField;
    }

    [ClousotRegressionTest]
#if FIRST
    [RegressionOutcome("No entry found in the cache")]
#else
    [RegressionOutcome("An entry has been found in the cache")]
#endif
    private bool Parameters(object x, object y)
    {
        Contract.Ensures(Contract.Result<bool>() == ((x == this) || (x == y)));
        return x == this || x == y;
    }
}

public class GenericClass<A, C, G, T>
  where A : class, C
  where C : class
  where T : class
{
    [ClousotRegressionTest]
#if FIRST
    [RegressionOutcome("No entry found in the cache")]
#else
    [RegressionOutcome("An entry has been found in the cache")]
#endif
    private T Type_FormalTypeParameter()
    {
        Contract.Ensures(Contract.Result<T>() == default(T));
        return default(T);
    }

    [Pure]
    private C c(A a) { return a; }

    [Pure]
    private static C sc(A a) { return a; }

    [ClousotRegressionTest]
#if FIRST
    [RegressionOutcome("No entry found in the cache")]
#else
    [RegressionOutcome("An entry has been found in the cache")]
#endif
    private bool Method_TypeArguments(A a, C c)
    {
        Contract.Ensures(Contract.Result<bool>() == (this.c(a) == c));
        return this.c(a) == c;
    }

    [ClousotRegressionTest]
#if FIRST
    [RegressionOutcome("No entry found in the cache")]
#else
    [RegressionOutcome("An entry has been found in the cache")]
#endif
    private bool Method_Static(A a, C c)
    {
        Contract.Ensures(Contract.Result<bool>() == (sc(a) == c));
        return sc(a) == c;
    }

    [Pure]
    private U f<U>(bool b, U x) { return b ? x : default(U); }

    [ClousotRegressionTest]
#if FIRST
    [RegressionOutcome("No entry found in the cache")]
#else
    [RegressionOutcome("An entry has been found in the cache")]
#endif
    private A Method_Specialized(bool b, A a)
    {
        Contract.Ensures(Contract.Result<A>() == this.f<A>(b, a));
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
        Contract.Ensures(Contract.Result<U>() == this.f<U>(b, u));
        return this.f(b, u);
    }
}
