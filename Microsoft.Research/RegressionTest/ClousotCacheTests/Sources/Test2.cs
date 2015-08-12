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
    private void F()  // We want to reanalyse
    {
        Console.WriteLine("Hello2");
    }

    [ClousotRegressionTest]
    [RegressionOutcome("An entry has been found in the cache")]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"assert unproven", PrimaryILOffset = 13, MethodILOffset = 0)]
    private void Fx(int x) // We do not want to reanalyse, even if F has changed. We find Fx in the cache from analyzing the prior test Test.cs
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
    private object Fo(object o) // We change the ensure to a requires (so we test the post condition suggestion)
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
    private object G0(object o)
    {
        return o;
    }

    [ClousotRegressionTest]
#if FIRST
    [RegressionOutcome("No entry found in the cache")]
#else
    [RegressionOutcome("An entry has been found in the cache")]
#endif
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "assert unproven", PrimaryILOffset = 20, MethodILOffset = 0)]
    private void G() // We want to reanalyse as contracts for G0 have changed
    {
        object o = G0(0);
        Contract.Assert(o != null);
    }

    [ClousotRegressionTest]
#if FIRST
    [RegressionOutcome("No entry found in the cache")]
#else
    [RegressionOutcome("An entry has been found in the cache")]
#endif
    public static T GetService<T>(IServiceProvider serviceProvider)
      where T : class
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
        // no warning
        public void Use()
        {
            Contract.Assume(field != null);

            IAmPure();

            Contract.Assert(field != null);
        }

        [Pure]
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
        [Pure] // This time it is pure, so we should not find the one defined in Test1
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
        // this time the parameter is pure, so we should hash again
        public string PureParameter([Pure] object[] x)
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

    public class TryLambda
    {
        private delegate TResult MyFunc<T, TResult>(T arg);

        [ClousotRegressionTest]
#if FIRST
        [RegressionOutcome("No entry found in the cache")]
#else
        [RegressionOutcome("An entry has been found in the cache")]
#endif

        public int SomeLambda2(string mystr)
        {
            MyFunc<string, int> lengthPlusTwoSecond = (string s) => (s.Length + 2);

            return lengthPlusTwoSecond(mystr);
        }

        // We should find it in the cache
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
        private readonly string field; // now we marked it readonly!

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
        public enum MyEnum { One, Two, Three, Quattro }; // Added one case!!!

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