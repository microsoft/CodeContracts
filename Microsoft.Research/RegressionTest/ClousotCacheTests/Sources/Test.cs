// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
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
    private void F()
    {
        Console.WriteLine("Hello");
    }

    [ClousotRegressionTest]
#if FIRST
    [RegressionOutcome("No entry found in the cache")]
#else
    [RegressionOutcome("An entry has been found in the cache")]
#endif
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"assert unproven", PrimaryILOffset = 13, MethodILOffset = 0)]
    private void Fx(int x) // With outcome
    {
        F();
        Contract.Assert(x != 0);
        Console.WriteLine("{0}", x);
    }

    [ClousotRegressionTest]
#if FIRST
    [RegressionOutcome("No entry found in the cache")]
#else
    [RegressionOutcome("An entry has been found in the cache")]
#endif
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"ensures unproven: Contract.Result<object>() != null", PrimaryILOffset = 11, MethodILOffset = 17)]
    private object Fo(object o) // With suggestion
    {
        Contract.Ensures(Contract.Result<object>() != null);

        return o;
    }

    [ClousotRegressionTest]
#if FIRST
    [RegressionOutcome("No entry found in the cache")]
#else
    [RegressionOutcome("An entry has been found in the cache")]
#endif
    private void UseFo() // We try to use the non-null post condition inference
    {
        object o = Fo(1);
        Contract.Assert(o != null);
    }

    [ClousotRegressionTest]
#if FIRST
    [RegressionOutcome("No entry found in the cache")]
#else
    [RegressionOutcome("An entry has been found in the cache")]
#endif
    private object G0(object o)
    {
        Contract.Requires(o != null);

        return o;
    }

    [ClousotRegressionTest]
#if FIRST
    [RegressionOutcome("No entry found in the cache")]
#else
    [RegressionOutcome("An entry has been found in the cache")]
#endif
    private void G() // With requires imported from another method, and an imported infered ensure
    {
        object o = G0(0);
        Contract.Assert(o != null);
    }

    // We now know how to cache pre/prost-conditions inferences
    private int P
    {
        [ClousotRegressionTest]
#if FIRST
        [RegressionOutcome("No entry found in the cache")]
#else
        [RegressionOutcome("An entry has been found in the cache")]
#endif
        get
        { return 1; }
    }

    [ClousotRegressionTest]
#if FIRST
    [RegressionOutcome("No entry found in the cache")]
#else
    [RegressionOutcome("An entry has been found in the cache")]
#endif
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"Possibly unboxing a null reference", PrimaryILOffset = 28, MethodILOffset = 0)]
    public static T GetService<T>(IServiceProvider serviceProvider)
    {
        Contract.Requires(serviceProvider != null);
        return (T)serviceProvider.GetService(typeof(T));
    }
}

namespace CacheBugs
{
    public class ExampleWithPure
    {
        public object field;

        [ClousotRegressionTest]
#if FIRST
        [RegressionOutcome("No entry found in the cache")]
#else
        [RegressionOutcome("An entry has been found in the cache")]
#endif
        [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"assert unproven", PrimaryILOffset = 35, MethodILOffset = 0)]
        public void Use()
        {
            Contract.Assume(field != null);

            IAmPure();

            Contract.Assert(field != null);
        }

        public void IAmPure()
        {
        }
    }

    public class ExamplesWithPure
    {
        [ClousotRegressionTest]
#if FIRST
        [RegressionOutcome("No entry found in the cache")]
#else
        [RegressionOutcome("An entry has been found in the cache")]
#endif
        public string PureMethod(object x)
        {
            Contract.Requires(x != null);
            return x.ToString();
        }

        [ClousotRegressionTest]
#if FIRST
        [RegressionOutcome("No entry found in the cache")]
#else
        [RegressionOutcome("An entry has been found in the cache")]
#endif
        public string PureParameter(object[] x)
        {
            Contract.Requires(x != null);
            return x.ToString();
        }

        [ClousotRegressionTest]
#if FIRST
        [RegressionOutcome("No entry found in the cache")]
#else
        [RegressionOutcome("An entry has been found in the cache")]
#endif
        public void CallPureParameter(object[] x)
        {
            Contract.Requires(x != null);
            PureParameter(x);
        }
    }
}

// For the tests below, Clousot2 was seeing the three occurrences of M as the same method, so it analyzed the first one, and read the other two from the cache.
// The reason is that the generics were not in the Full name
namespace CacheBugsWithClousot2
{
    internal class C
    {
        [ClousotRegressionTest]
        [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "assert unproven", PrimaryILOffset = 4, MethodILOffset = 0)]
#if FIRST
        [RegressionOutcome("No entry found in the cache")]
#else
        [RegressionOutcome("An entry has been found in the cache")]
#endif
        private void M(int x)
        {
            Contract.Assert(x > 0);
        }
    }

    internal class C<T>
    {
        [ClousotRegressionTest]
        [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "assert unproven", PrimaryILOffset = 4, MethodILOffset = 0)]
#if FIRST
        [RegressionOutcome("No entry found in the cache")]
#else
        [RegressionOutcome("An entry has been found in the cache")]
#endif
        private void M(int x)
        {
            Contract.Assert(x > 0);
        }
    }
    internal class C<X, Y>
    {
        [ClousotRegressionTest]
        [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "assert unproven", PrimaryILOffset = 4, MethodILOffset = 0)]
#if FIRST
        [RegressionOutcome("No entry found in the cache")]
#else
        [RegressionOutcome("An entry has been found in the cache")]
#endif
        private void M(int x)
        {
            Contract.Assert(x > 0);
        }
    }

    public class TryLambda
    {
        private delegate TResult MyFunc<T, TResult>(T arg);

        [ClousotRegressionTest]
#if FIRST
        [RegressionOutcome("No entry found in the cache")]
#else
        [RegressionOutcome("An entry has been found in the cache")]
#endif
        public int SomeLambda(string mystr)
        {
            MyFunc<string, int> lengthPlusTwo = (string s) => (s.Length + 2);

            return lengthPlusTwo(mystr);
        }
    }
}

namespace WithReadonly
{
    public class ReadonlyTest
    {
        private string field; // not readonly

#if FIRST
        [RegressionOutcome("No entry found in the cache")]
#else
        [RegressionOutcome("An entry has been found in the cache")]
#endif
        public ReadonlyTest(string s)
        {
            this.field = s;
        }

#if FIRST
        [RegressionOutcome("No entry found in the cache")]
#else
        [RegressionOutcome("An entry has been found in the cache")]
#endif
        public string DoSomething()
        {
            return this.field;
        }
    }
}

namespace EnumValues
{
    public static class Helper
    {
        [ContractVerification(false)]
        static public Exception FailedAssertion()
        {
            Contract.Requires(false, "Randy wants you to handle all the enums ;-)");
            throw new Exception();
        }
    }
    public class Foo
    {
        public enum MyEnum { One, Two, Three, Quattro };

#if FIRST
        [RegressionOutcome("No entry found in the cache")]
#else
        [RegressionOutcome("An entry has been found in the cache")]
#endif	
        public int Test(MyEnum e)
        {
            var i = 0;

            switch (e)
            {
                case MyEnum.One:
                    i += 1;
                    break;

                case MyEnum.Two:
                    i += 2;
                    break;

                case MyEnum.Three:
                    i += 3;
                    break;

                default:
                    throw Helper.FailedAssertion();
            }

            return i;
        }
    }
}