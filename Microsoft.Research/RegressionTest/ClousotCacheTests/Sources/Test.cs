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
  void F()
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
  void Fx(int x) // With outcome
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
  object Fo(object o) // With suggestion
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
  void UseFo() // We try to use the non-null post condition inference
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
  object G0(object o)
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
  void G() // With requires imported from another method, and an imported infered ensure
  {
    object o = G0(0);
    Contract.Assert(o != null);
  }

  // We now know how to cache pre/prost-conditions inferences
  int P
  {
    [ClousotRegressionTest]
#if FIRST
    [RegressionOutcome("No entry found in the cache")]
#else
    [RegressionOutcome("An entry has been found in the cache")]
#endif
    get { return 1; }
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
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"assert unproven",PrimaryILOffset=35,MethodILOffset=0)]
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
  class C
  {
    [ClousotRegressionTest]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="assert unproven",PrimaryILOffset=4,MethodILOffset=0)]
#if FIRST 
    [RegressionOutcome("No entry found in the cache")]
#else
    [RegressionOutcome("An entry has been found in the cache")]
#endif
    void M(int x)
    {
		Contract.Assert(x > 0);
    }
  }
 
 class C<T>
  {
    [ClousotRegressionTest]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="assert unproven",PrimaryILOffset=4,MethodILOffset=0)]
#if FIRST 
    [RegressionOutcome("No entry found in the cache")]
#else
    [RegressionOutcome("An entry has been found in the cache")]
#endif
    void M(int x) 
	{ 
		Contract.Assert(x > 0); 
	}
  }
  class C<X, Y>
  {
    [ClousotRegressionTest]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="assert unproven",PrimaryILOffset=4,MethodILOffset=0)]
#if FIRST 
    [RegressionOutcome("No entry found in the cache")]
#else
    [RegressionOutcome("An entry has been found in the cache")]
#endif
    void M(int x) 
	{
		Contract.Assert(x > 0); 
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
		string field; // not readonly
		
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
        public enum MyEnum { One, Two, Three, Quattro};

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