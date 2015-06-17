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

class Test
{
  [ClousotRegressionTest]
#if FIRST
  [RegressionOutcome("No entry found in the cache")]
#else
  [RegressionOutcome("An entry has been found in the cache")]
#endif
  void F()  // We want to reanalyse
  {
    Console.WriteLine("Hello2");
  }

  [ClousotRegressionTest]
  [RegressionOutcome("An entry has been found in the cache")]
  [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"assert unproven", PrimaryILOffset = 13, MethodILOffset = 0)]
  void Fx(int x) // We do not want to reanalyse, even if F has changed. We find Fx in the cache from analyzing the prior test Test.cs
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
  object Fo(object o) // We change the ensure to a requires (so we test the post condition suggestion)
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
  object G0(object o) 
  {
    return o;
  }

  [ClousotRegressionTest]
#if FIRST
  [RegressionOutcome("No entry found in the cache")]
#else
  [RegressionOutcome("An entry has been found in the cache")]
#endif
  [RegressionOutcome(Outcome=ProofOutcome.Top,Message="assert unproven",PrimaryILOffset=20,MethodILOffset=0)]
  void G() // We want to reanalyse as contracts for G0 have changed
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
   
    delegate TResult MyFunc<T, TResult>(T arg);
   
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
		readonly string field; // now we marked it readonly!
		
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
        public enum MyEnum { One, Two, Three, Quattro}; // Added one case!!!

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