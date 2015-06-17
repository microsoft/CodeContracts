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
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Linq;
using System.IO;
using System.Threading;
using System.Globalization;

[assembly: RegressionOutcome("This/Me cannot be used in Requires of a constructor")]

namespace UserFeedback
{
  namespace SteveDunn
  {
    class SteveDunn
    {
      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 5, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 7, MethodILOffset = 5)]
      public void Test()
      {
        this.Divide(100, 1);
      }

      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 17, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 7, MethodILOffset = 17)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 30, MethodILOffset = 0)]
      public void Test2(int divisor)
      {
        Contract.Requires(numberGreaterThanZero(divisor));

        var result = this.Divide(100, divisor);
        Contract.Assert(result >= 0);
      }

      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 41, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 31, MethodILOffset = 54)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"Division by zero ok", PrimaryILOffset = 49, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"No overflow", PrimaryILOffset = 49, MethodILOffset = 0)]
      public int Divide(int number, int divisor)
      {
        Contract.Requires(numberGreaterThanZero(divisor));
        Contract.Ensures(number < 0 || Contract.Result<int>() >= 0);

        Contract.Assert(divisor > 0);
        return number / divisor;
      }

      [ClousotRegressionTest] // CCI2 decompiler doesn't decompile disjunctions correctly
      [Pure]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 32, MethodILOffset = 46)]
      public static bool numberGreaterThanZero(int divisor)
      {
        Contract.Ensures(Contract.Result<bool>() && divisor > 0 || !Contract.Result<bool>() && divisor <= 0);

        return divisor > 0;
      }

    }
  }

  namespace AndrewArnott
  {
    class AndrewArnott
    {
      [ClousotRegressionTest] // CCI2 is not seeing some mememory deref (value.Length)
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 15, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 31, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 10, MethodILOffset = 31)]
      public void Foo(string value)
      {
        Contract.Requires(value != null);
        Contract.Requires(value.Length > 0);
        Bar(value);
      }

      public void Bar(string value)
      {
        Contract.Requires(!string.IsNullOrEmpty(value));
      }


      byte[] SecretKey { get; set; }

      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 2, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 20, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as array)", PrimaryILOffset = 25, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 34, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as array)", PrimaryILOffset = 39, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 53, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 60, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Array creation : ok", PrimaryILOffset = 27, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 13, MethodILOffset = 60)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 33, MethodILOffset = 60)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 51, MethodILOffset = 60)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 81, MethodILOffset = 60)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"No overflow (caused by a negative array size)",PrimaryILOffset=27,MethodILOffset=0)]
      byte[] CopySecretKey()
      {
        Contract.Assume(this.SecretKey != null);
        byte[] secretKeyCopy = new byte[this.SecretKey.Length];
        if (this.SecretKey.Length > 0)
        {
          this.SecretKey.CopyTo(secretKeyCopy, 0);
        }
        return secretKeyCopy;
      }

      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as receiver)", PrimaryILOffset = 96, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as receiver)", PrimaryILOffset = 103, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as receiver)", PrimaryILOffset = 110, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 13, MethodILOffset = 63)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 13, MethodILOffset = 71)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 121, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as receiver)", PrimaryILOffset = 13, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as receiver)", PrimaryILOffset = 21, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as receiver)", PrimaryILOffset = 28, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as receiver)", PrimaryILOffset = 36, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as receiver)", PrimaryILOffset = 48, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as receiver)", PrimaryILOffset = 56, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as receiver)", PrimaryILOffset = 63, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as receiver)", PrimaryILOffset = 71, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as receiver)", PrimaryILOffset = 88, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 13, MethodILOffset = 28)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 13, MethodILOffset = 36)]
      [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"requires unproven: value >= 0", PrimaryILOffset = 13, MethodILOffset = 103)]
      public static void TestStringBuilder()
      {
        StringBuilder sb = new StringBuilder();

        //Contract.Assert(sb.Length == 0);
        // Test one: add one character, remove one character
        sb.Append("a");
        //Contract.Assert(sb.Length == 1);

        sb.Length -= 1;
        sb.Length = 0; // reset test

        // Test two: add 3 or 4 characters (newline length varies), remove 3.
        sb.AppendLine("ab");
        sb.Length -= 3;

        sb.Length = 0; // reset test
        // Test three: add 3 characters (although it sort of looks like 5), and remove 5.
        // Since this could expand to anywhere from 2 characters long to very long,
        // I'd be willing to settle for no ensures here... But this specific one SHOULD
        // generate a warning since I'm definitely going to hit a runtime error on this one.
        sb.AppendFormat("a{0}c", "b");
        //Contract.Assert(sb.Length >= 2500);

        sb.Length -= 5;
        Contract.Assert(sb.Length >= 0);
      }

      [ClousotRegressionTest]
      [ClousotRegressionTest("cci2only")]
      [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"Possibly calling a method on a null reference 'req'", PrimaryILOffset = 2, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as receiver)", PrimaryILOffset = 20, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 13, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 31, MethodILOffset = 0)]
      public static void TestHttpRequest(HttpRequest req)
      {
        Contract.Assert(req.Url != null);
        Contract.Assert(req.RawUrl != null);
      }

      class Rebinding
      {
        [ClousotRegressionTest]
        [ClousotRegressionTest("cci2only")]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 26, MethodILOffset = 0)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 77, MethodILOffset = 0)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 89, MethodILOffset = 0)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Negation ok (no MinValue) of type Int32", PrimaryILOffset = 61, MethodILOffset = 0)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Negation ok (no MinValue) of type Int32", PrimaryILOffset = 73, MethodILOffset = 0)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Negation ok (no MinValue) of type Int32", PrimaryILOffset = 84, MethodILOffset = 0)]
        [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "requires unproven: 0 <= index", PrimaryILOffset = 13, MethodILOffset = 26)]
        [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "requires unproven: index < this.Length", PrimaryILOffset = 33, MethodILOffset = 26)]
        [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "requires unproven: capacity >= 0. The static checker determined that the condition '((2 - exp + 1)) >= 0' should hold on entry. Nevertheless, the condition may be too strong for the callers. If you think it is ok, add a precondition to document it: Contract.Requires(((2 - exp + 1)) >= 0);", PrimaryILOffset = 19, MethodILOffset = 66)]
        internal static string ToString(double d, string result, int k, int exp)
        {
          Contract.Requires(result != null);

          {
            int res = 0;

            while (result[k] == '0') k--; //at the end of the loop, k == the number of significant digits

            int n = exp + 1;
            if (-6 < n /*&& n <= 0*/)
            {
              res = -n;

              StringBuilder r = new StringBuilder(2 - n);

              res = -n;

              r.Append(false);

              res = -n;         // Warning point

            }

            return res.ToString();
          }
        }
      }
    }
  }

  namespace Alexey
  {
    namespace Locking {
      class Some
      {
        int count = 0;

        [ClousotRegressionTest]
        [ClousotRegressionTest("cci2only")]
        [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=2,MethodILOffset=0)]
#if NETFRAMEWORK_4_0
        [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=33,MethodILOffset=0)]
        [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=57,MethodILOffset=0)]
        [RegressionOutcome(Outcome=ProofOutcome.True,Message="assert is valid",PrimaryILOffset=72,MethodILOffset=0)]
#else
        [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=29,MethodILOffset=0)]
        [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=53,MethodILOffset=0)]
        [RegressionOutcome(Outcome=ProofOutcome.True,Message="assert is valid",PrimaryILOffset=68,MethodILOffset=0)]
#endif
        public void WaitFor0()
        {
            if (this.count > 0)
            {
                lock (this)
                {
                    if (this.count > 0)
                    {
                        Monitor.Wait(this);
                        Contract.Assume(this.count == 0); // <-- "Assumption is false" is not desirable here
                        // Maybe add some special handling for Monitor.Wait and for Wait method of other threading primitives?
                        Contract.Assert(true); // make sure assume above is not false
                    }
                }
            }
        }

        object lockObject = new Object();

        [ClousotRegressionTest]
        [ClousotRegressionTest("cci2only")]
        [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=2,MethodILOffset=0)]
#if NETFRAMEWORK_4_0
        [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=33,MethodILOffset=0)]
        [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=50,MethodILOffset=0)]
        [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=62,MethodILOffset=0)]
        [RegressionOutcome(Outcome=ProofOutcome.True,Message="assert is valid",PrimaryILOffset=77,MethodILOffset=0)]
#else
        [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=29,MethodILOffset=0)]
        [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=46,MethodILOffset=0)]
        [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=58,MethodILOffset=0)]
        [RegressionOutcome(Outcome=ProofOutcome.True,Message="assert is valid",PrimaryILOffset=73,MethodILOffset=0)]
#endif
        public void WaitFor0WithLockObject()
        {
            if (this.count > 0)
            {
                lock (this)
                {
                    if (this.count > 0)
                    {
                        Monitor.Wait(this.lockObject);
                        Contract.Assume(this.count == 0); // <-- "Assumption is false" is not desirable here
                        // Maybe add some special handling for Monitor.Wait and for Wait method of other threading primitives?
                        Contract.Assert(true); // make sure assume above is not false
                    }
                }
            }
        }

      }

    }
    static class Program
    {
      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 10, MethodILOffset = 18)]
      static void Main_Syntactic()
      {
        string s = GetString() + "suffix";

        // can prove it as it matches syntactically the postcondition of arg.Length in the WPs
        RequiresNonEmptyString_Syntactic(s);
      }

      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 13, MethodILOffset = 18)]
      static void Main_Semantic()
      {
        string s = GetString() + "suffix";

        // can prove it as it matches semantically the postcondition of arg.Length in the WPs
        RequiresNonEmptyString_Semantic(s);
      }

      static string GetString()
      {
        return null;
      }

      static void RequiresNonEmptyString_Semantic(string arg)
      {
        Contract.Requires(arg.Length != 0);
      }

      static void RequiresNonEmptyString_Syntactic(string arg)
      {
        Contract.Requires(arg.Length > 0);
      }

    }



    class Alexey
    {

      Dictionary<string, object> _dict = new Dictionary<string, object>();

      void AddItemToDict(string key, object value)
      {
        Contract.Requires(!_dict.ContainsKey(key));
        _dict.Add(key, value);

        // do something with a newly added item
      }

      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 2, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "Possibly calling a method on a null reference \'this._dict\'", PrimaryILOffset = 8, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 21, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 16, MethodILOffset = 21)]
      void ProcessItem(string key, object value)
      {
        if (!_dict.ContainsKey(key))
        {
          AddItemToDict(key, value);
          return;
        }

        // do something with existing item
      }
    }


    class AssumeOld
    {
      class SomeClass
      {
        public int PropA { get; set; }
      }

      //[ClousotRegressionTest]
      void Test(SomeClass t)
      {
        Contract.Ensures(t.PropA == Contract.OldValue(t.PropA));
        //Contract.Assume(t.PropA == Contract.OldValue(t.PropA));
      }

    }
  }

  namespace RobTF
  {
    using System.Linq;

    public class ClassA
    {
      public string Field { get; set; }
    }

    public class ClassB : ClassA { }

    public class Test : System.Collections.ObjectModel.Collection<ClassA>
    {
      public IQueryable<T> OfType<T>()
      {
        Contract.Ensures(Contract.Result<IQueryable<T>>() != null);

        throw new NotImplementedException();
      }

      [ClousotRegressionTest] // CCI2 is not seeing requires
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 2, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as array)", PrimaryILOffset = 96, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Array creation : ok", PrimaryILOffset = 87, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 96, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 96, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 13, MethodILOffset = 103)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 13, MethodILOffset = 22)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 13, MethodILOffset = 44)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 13, MethodILOffset = 60)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 13, MethodILOffset = 81)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 31, MethodILOffset = 81)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 13, MethodILOffset = 98)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 31, MethodILOffset = 103)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"No overflow (caused by a negative array size)",PrimaryILOffset=87,MethodILOffset=0)]
      public ClassB Foo()
      {
        return this.OfType<ClassB>().Where(b => b.Field == String.Empty).FirstOrDefault();
      }
    }

  }

  namespace Peli
  {

    class TrimString
    {
      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as receiver)", PrimaryILOffset = 50, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as receiver)", PrimaryILOffset = 66, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as receiver)", PrimaryILOffset = 72, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as receiver)", PrimaryILOffset = 78, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 13, MethodILOffset = 50)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 13, MethodILOffset = 78)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 31, MethodILOffset = 78)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="requires is valid",PrimaryILOffset=54,MethodILOffset=78)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="requires is valid",PrimaryILOffset=79,MethodILOffset=78)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 40, MethodILOffset = 90)]
      public static string TrimEnd_If(string target, string suffix)
      {
        Contract.Requires(target != null);
        Contract.Requires(!String.IsNullOrEmpty(suffix));
        Contract.Ensures(Contract.Result<string>() != null);

        var result = target;

        if (result.EndsWith(suffix))
        {
          // Proved by the interface WP/Abstractdomains
          result = result.Substring(0, result.Length - suffix.Length);
        }

        return result;
      }

      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as receiver)", PrimaryILOffset = 97, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as receiver)", PrimaryILOffset = 52, MethodILOffset = 111)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as receiver)", PrimaryILOffset = 72, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as receiver)", PrimaryILOffset = 78, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as receiver)", PrimaryILOffset = 88, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 13, MethodILOffset = 97)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 40, MethodILOffset = 111)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 13, MethodILOffset = 88)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 31, MethodILOffset = 88)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="requires is valid",PrimaryILOffset=54,MethodILOffset=88)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="requires is valid",PrimaryILOffset=79,MethodILOffset=88)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 60, MethodILOffset = 111)]
      public static string TrimSuffix_Var(string source, string suffix)
      {
        Contract.Requires(source != null);
        Contract.Requires(!String.IsNullOrEmpty(suffix));
        Contract.Ensures(Contract.Result<string>() != null);
        Contract.Ensures(!Contract.Result<string>().EndsWith(suffix));

        var result = source;
        while (result.EndsWith(suffix))
        {
          var remainder = result.Length - suffix.Length;
          result = result.Substring(0, remainder);
        }
        return result;
      }

      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 31, MethodILOffset = 86)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as receiver)", PrimaryILOffset = 95, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as receiver)", PrimaryILOffset = 52, MethodILOffset = 109)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as receiver)", PrimaryILOffset = 74, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as receiver)", PrimaryILOffset = 80, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as receiver)", PrimaryILOffset = 86, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 13, MethodILOffset = 95)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 40, MethodILOffset = 109)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 13, MethodILOffset = 86)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="requires is valid",PrimaryILOffset=54,MethodILOffset=86)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="requires is valid",PrimaryILOffset=79,MethodILOffset=86)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 60, MethodILOffset = 109)]
      public static string TrimSuffix(string source, string suffix)
      {
        Contract.Requires(source != null);
        Contract.Requires(!String.IsNullOrEmpty(suffix));
        Contract.Ensures(Contract.Result<string>() != null);
        Contract.Ensures(!Contract.Result<string>().EndsWith(suffix));

        var result = source;
        while (result.EndsWith(suffix))
        {
          // F: The test is there because even if we've lost the name for the value of result.Length - suffix.Length, but we should be able to prove it anyway
          result = result.Substring(0, result.Length - suffix.Length);
        }
        return result;
      }
    }
  }

  namespace Maf
  {
    class Congruence
    {
      [Pure]
      public static bool Property(int x)
      {
        return false;
      }

      //[ClousotRegressionTest]
      public static void Test(int x, int y)
      {
        Contract.Requires(Property(x));

        if (x == y)
        {
          Contract.Assert(Property(y));
        }
      }
    }
  }

  namespace Multani
  {
    class SumTest
    {
      Dictionary<int, double> GetProbs()
      {
        Contract.Ensures(Contract.Result<Dictionary<int, double>>().Values.Sum() == 1);
        return null;
      }
    }
  }

  namespace Strilanc
  {
    public class MStack<T>
    {
      public int size;
      public MStack<T> next;

      [ClousotRegressionTest]
      [ClousotRegressionTest("cci2only")]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as field receiver)", PrimaryILOffset = 2, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as field receiver)", PrimaryILOffset = 11, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as field receiver)", PrimaryILOffset = 28, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as field receiver)", PrimaryILOffset = 36, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as field receiver)", PrimaryILOffset = 42, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"Possibly accessing a field on a null reference 'this.next'. The static checker determined that the condition 'this.next != null' should hold on entry. Nevertheless, the condition may be too strong for the callers. If you think it is ok, add a precondition to document it: Contract.Requires(this.next != null);", PrimaryILOffset = 47, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as field receiver)", PrimaryILOffset = 66, MethodILOffset = 96)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as field receiver)", PrimaryILOffset = 75, MethodILOffset = 96)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 85, MethodILOffset = 96)]
      public MStack<T> Pushed_RequiryingCheckAfterAssertions(T val)
      {
        // Warning for this.next that can be null
        Contract.Requires((this.size == 0) == (this.next == null));
        Contract.Requires((this.size == 0) || (this.size == (this.next.size + 1)));

        Contract.Ensures((this.size == 0) == (this.next == null));

        return null;
      }

      [ClousotRegressionTest]
      [ClousotRegressionTest("cci2only")]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 2, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 10, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 16, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "Possibly accessing a field on a null reference 'this.next'. The static checker determined that the condition 'this.next != null' should hold on entry. Nevertheless, the condition may be too strong for the callers. If you think it is ok, add a precondition to document it: Contract.Requires(this.next != null);", PrimaryILOffset = 21, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 40, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 49, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 66, MethodILOffset = 96)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 75, MethodILOffset = 96)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 85, MethodILOffset = 96)]
      public MStack<T> Pushed_Working(T val)
      {
        // Warning for this.next that can be null
        Contract.Requires((this.size == 0) || (this.size == (this.next.size + 1)));
        Contract.Requires((this.size == 0) == (this.next == null));

        Contract.Ensures((this.size == 0) == (this.next == null));

        return null;
      }
    }

    public abstract class Base
    {
      public bool IsValid
      {
        get;
        private set;
      }

      public int Value
      {
        get;
        private set;
      }

      protected Base(int value)
      {
        this.Value = value;
        this.IsValid = (value != 0);
      }
    }

    public class Sub : Base
    {
      public Sub(int value)
        : base(value)
      {
        Contract.Requires(this.IsValid); // results in an assembly wide issue of using "this"
      }
    }

  }

  namespace Pieter
  {
    public class Rationaal
    {
      int _noemer;
      public int Noemer
      {
        [ClousotRegressionTest]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as field receiver)", PrimaryILOffset = 2, MethodILOffset = 0)]
        get { return _noemer; }
        [ClousotRegressionTest]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as field receiver)", PrimaryILOffset = 3, MethodILOffset = 0)]
        private set { _noemer = value; }
      }

      int _deler;
      public int Deler
      {
        [ClousotRegressionTest]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as field receiver)", PrimaryILOffset = 2, MethodILOffset = 0)]
        get { return _deler; }
        [ClousotRegressionTest]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as field receiver)", PrimaryILOffset = 3, MethodILOffset = 0)]
        private set { _deler = value; }
      }


      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as receiver)", PrimaryILOffset = 1, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as receiver)", PrimaryILOffset = 70, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as receiver)", PrimaryILOffset = 78, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as receiver)", PrimaryILOffset = 39, MethodILOffset = 85)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as receiver)", PrimaryILOffset = 54, MethodILOffset = 85)]
      [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"ensures unproven: Noemer == noemer", PrimaryILOffset = 47, MethodILOffset = 85)]
      [RegressionOutcome(Outcome = ProofOutcome.False, Message = @"ensures is false: Deler == deler", PrimaryILOffset = 62, MethodILOffset = 85)]
      public Rationaal(int noemer, int deler)
      {
        Contract.Requires(noemer > 0, "noemer must be positive.");
        Contract.Requires(deler > 0, "deler must be positive.");
        Contract.Ensures(Noemer == noemer);
        Contract.Ensures(Deler == deler);

        Noemer = noemer;
        Noemer = deler;
      }

      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as receiver)", PrimaryILOffset = 1, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as receiver)", PrimaryILOffset = 70, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as receiver)", PrimaryILOffset = 78, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as receiver)", PrimaryILOffset = 39, MethodILOffset = 85)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as receiver)", PrimaryILOffset = 54, MethodILOffset = 85)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 47, MethodILOffset = 85)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 62, MethodILOffset = 85)]
      public Rationaal(int noemer, int deler, bool dummy)
      {
        Contract.Requires(noemer > 0, "noemer must be positive.");
        Contract.Requires(deler > 0, "deler must be positive.");
        Contract.Ensures(Noemer == noemer);
        Contract.Ensures(Deler == deler);

        Noemer = noemer;
        Deler = deler;
      }


    }
  }

  namespace WinSharp
  {
    class Program
    {
      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 9, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 16, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 10, MethodILOffset = 16)]
      static void TestEqEq()
      {
        WithEqEq foo = new WithEqEq();
        foo.SetBar(5);
        foo.DoFoo();
      }
      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 9, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 16, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 10, MethodILOffset = 16)]
      static void TestEquals()
      {
        var eqtest = new WithEquals();
        eqtest.SetBar(5);
        eqtest.DoFoo();
      }
      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 9, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 16, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 10, MethodILOffset = 16)]
      static void TestObjectEquals()
      {
        var eqtest = new WithObjectEquals();
        eqtest.SetBar(5);
        eqtest.DoFoo();
      }
      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 9, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 16, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 13, MethodILOffset = 16)]
      static void TestIEquatable()
      {
        var eqtest = new WithIEquatable();
        eqtest.SetBar(5);
        eqtest.DoFoo();
      }
    }

    public sealed class WithEqEq
    {
      public int Bar
      {
        get;
        private set;
      }

      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 18, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 2, MethodILOffset = 24)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 10, MethodILOffset = 24)]
      public void SetBar(int value)
      {
        Contract.Ensures(this.Bar == value);

        this.Bar = value;
      }

      public void DoFoo()
      {
        Contract.Requires(this.Bar > 0);
      }
    }
    public sealed class WithEquals
    {
      public int Bar
      {
        get;
        private set;
      }

      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 24, MethodILOffset = 0)]
#if  CLOUSOT2
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 1, MethodILOffset = 30)]
#else
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 2, MethodILOffset = 30)]
#endif
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 16, MethodILOffset = 30)]
      public void SetBar(int value)
      {
        Contract.Ensures(this.Bar.Equals(value));

        this.Bar = value;
      }

      public void DoFoo()
      {
        Contract.Requires(this.Bar > 0);
      }
    }
    public sealed class WithObjectEquals
    {
      public int Bar
      {
        get;
        private set;
      }

      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 31, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 2, MethodILOffset = 37)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 23, MethodILOffset = 37)]
      public void SetBar(int value)
      {
        Contract.Ensures(Object.Equals(this.Bar, value));

        this.Bar = value;
      }

      public void DoFoo()
      {
        Contract.Requires(this.Bar > 0);
      }
    }
    public sealed class WithIEquatable
    {
      public IEquatable<int> Bar
      {
        get;
        private set;
      }

      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 26, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 2, MethodILOffset = 32)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 13, MethodILOffset = 32)]
      public void SetBar(int value)
      {
        Contract.Ensures(this.Bar.Equals(value));

        this.Bar = value;
      }

      public void DoFoo()
      {
        Contract.Requires(this.Bar.Equals(5));
      }
    }

    public class TestPropModifies
    {

      public sealed class Foo
      {
        public int Prop1
        {
          get;
          set;
        }
        public int Prop2
        {
          get;
          set;
        }

        public void Bar()
        {
          Contract.Ensures(this.Prop1 == Contract.OldValue(this.Prop1) + 1);
          this.Prop1++;
        }

        public void Baz()
        {
          Contract.Requires(this.Prop2 != 0);

          Console.WriteLine("Something");
        }
      }

      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as receiver)", PrimaryILOffset = 8, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as receiver)", PrimaryILOffset = 15, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"requires unproven: this.Prop2 != 0", PrimaryILOffset = 13, MethodILOffset = 15)]
      static void Test()
      {
        Foo foo = new Foo();

        foo.Bar();

        foo.Baz();
      }


    }
  }

  namespace Pelmens
  {
    class SomeClass
    {
      private int? number;

      public SomeClass(int? value) { number = value; }

      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 2, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 7, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 21, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 26, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 12, MethodILOffset = 26)]
      public int SomeMethod()
      {
        if (number.HasValue)
        {
          return number.Value;
        }

        return 0;
      }
    }

  }

  namespace Somebody
  {
    class TestResourceString
    {
      internal void Test(string s)
      {
        Contract.Requires(s != null, TestFrameworkOOB.Properties.Resources.UserMessage1);

      }

      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.False, Message = @"requires is false: s != null", PrimaryILOffset = 13, MethodILOffset = 3)]
      void Test()
      {
        Test(null);
      }
    }
  }

  namespace Jauernig
  {
    [ContractClass(typeof(ContractForISet<>))]
    public interface ISet<T>
    {
      // Queries
      [Pure]
      int Count { get; }
      [Pure]
      bool IsEmpty { get; }
      [Pure]
      IEnumerator<T> GetEnumerator();
      [Pure]
      bool Contains(T item);

      // Commands
      void Add(T item);
      void Remove(T item);
      void Clear();
    }

    [ContractClassFor(typeof(ISet<>))]
    abstract class ContractForISet<T> : ISet<T>
    {
      int ISet<T>.Count
      {
        get
        {
          Contract.Ensures(Contract.Result<int>() >= 0);
          return default(int);
        }
      }

      bool ISet<T>.IsEmpty
      {
        get
        {
          Contract.Ensures(Contract.Result<bool>() == (((ISet<T>)this).Count == 0));
          return default(bool);
        }
      }

      IEnumerator<T> ISet<T>.GetEnumerator()
      {
        Contract.Ensures(Contract.Result<IEnumerator<T>>() != null);
        return default(IEnumerator<T>);
      }

      bool ISet<T>.Contains(T item)
      {
        Contract.Requires(item != null);
        return default(bool);
      }


      void ISet<T>.Add(T item)
      {
        Contract.Requires(item != null);
        Contract.Requires(!((ISet<T>)this).Contains(item));
        Contract.Ensures(((ISet<T>)this).Contains(item));
      }

      void ISet<T>.Remove(T item)
      {
        Contract.Requires(item != null);
        Contract.Requires(((ISet<T>)this).Contains(item));
        Contract.Ensures(!((ISet<T>)this).Contains(item));
      }

      void ISet<T>.Clear()
      {
        Contract.Ensures(((ISet<T>)this).Count == 0);
      }
    }

    public class ListSet<T> : ISet<T>
    {
      private readonly List<T> _baseList;

      public ListSet()
      {
        _baseList = new List<T>();
      }

      [ContractInvariantMethod]
      private void ClassInvariants()
      {
        Contract.Invariant(_baseList != null);
        // Contract.Invariant(IsEmpty == (Count == 0));
      }

      #region ISet<T> Members

      public int Count
      {
        [ClousotRegressionTest] // CCI2 is not inheriting contracts
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 26, MethodILOffset = 0)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 31, MethodILOffset = 0)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 7, MethodILOffset = 40)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 12, MethodILOffset = 40)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 12, MethodILOffset = 40)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 19, MethodILOffset = 40)]
        get
        {
          Contract.Ensures(Contract.Result<int>() == _baseList.Count);
          return _baseList.Count;
        }
      }

      public bool IsEmpty
      {
        [ClousotRegressionTest]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 2, MethodILOffset = 0)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 7, MethodILOffset = 14)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 17, MethodILOffset = 14)]
        get { return (Count == 0); }
      }

      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 2, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 7, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 12, MethodILOffset = 21)]
      public IEnumerator<T> GetEnumerator()
      {
        return _baseList.GetEnumerator();
      }

      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 2, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 8, MethodILOffset = 0)]
      public bool Contains(T item)
      {
        return _baseList.Contains(item);
      }

      //[ClousotRegressionTest]
      public void Add(T item)
      {
        _baseList.Add(item);
      }

      //[ClousotRegressionTest]
      public void Remove(T item)
      {
        _baseList.Remove(item);
      }

      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 2, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 7, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 2, MethodILOffset = 13)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 10, MethodILOffset = 13)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 13, MethodILOffset = 13)]
      public void Clear()
      {
        _baseList.Clear();
      }

      #endregion
    }

  }

  namespace AlexeyR
  {
    public class MyReadOnlyCollection<T>
    {
      private IList<T> x;

      public int Count
      {
        get
        {
          return x.Count;
        }
      }

      public MyReadOnlyCollection(IList<T> arr)
      {
        Contract.Requires(arr != null);
        Contract.Ensures(this.Count == arr.Count);

        this.x = arr;
      }
    }

    static class Program
    {
      [ClousotRegressionTest("cc1only")]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as receiver)", PrimaryILOffset = 16, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"Array creation : ok", PrimaryILOffset = 2, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 15, MethodILOffset = 9)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 27, MethodILOffset = 0)]
      static void Main()
      {
        int[] arr = new int[1];

        var coll = new MyReadOnlyCollection<int>(arr);
        Contract.Assert(coll.Count != 0);
      }

      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 18, MethodILOffset = 45)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 34, MethodILOffset = 45)]
      static bool Test(int x)
      {
        Contract.Requires(x > 0);
        bool result = Contract.Result<bool>();
        Contract.Ensures(result != false);
        Contract.Ensures(result || !result);
        return true;
      }

      [ClousotRegressionTest("cci1only")] // See below test for the equivalent test in CCI2.
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 8, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 25, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 8, MethodILOffset = 128)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 48, MethodILOffset = 128)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 54, MethodILOffset = 128)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 66, MethodILOffset = 128)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 93, MethodILOffset = 128)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as array)", PrimaryILOffset = 98, MethodILOffset = 128)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 59, MethodILOffset = 128)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 85, MethodILOffset = 128)]
      [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "ensures unproven: Contract.ForAll(0, arr.Length, i => arr[i] == result)", PrimaryILOffset = 117, MethodILOffset = 128)]
      static bool Test_UsingCCI1(int x, bool[] arr)
      {
        Contract.Requires(x > 0);
        Contract.Requires(arr != null);
        bool result = Contract.Result<bool>();
        Contract.Ensures(result != false);
        Contract.Ensures(result || !result);
        Contract.Ensures(Contract.ForAll(0, arr.Length, i => arr[i] == result));
        return true;
      }
      // CCI2 does a better job (although not perfect) of decompiling the anonymous delegate
      // That ends up with the contract not having any references to the closure class (display class)
      // so there are fewer dereferences.
      [ClousotRegressionTest("cci2only")] // see the above test for the equivalent CCI1 test.
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as field receiver)", PrimaryILOffset = 8, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as array)", PrimaryILOffset = 98, MethodILOffset = 128)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 59, MethodILOffset = 128)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 85, MethodILOffset = 128)]
      [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "ensures unproven: Contract.ForAll(0, arr.Length, i => arr[i] == result)", PrimaryILOffset = 117, MethodILOffset = 128)]
      static bool Test_UsingCCI2(int x, bool[] arr) {
        Contract.Requires(x > 0);
        Contract.Requires(arr != null);
        bool result = Contract.Result<bool>();
        Contract.Ensures(result != false);
        Contract.Ensures(result || !result);
        Contract.Ensures(Contract.ForAll(0, arr.Length, i => arr[i] == result));
        return true;
      }

    }
  }

  namespace RosenHaus
  {
    interface IBar<T>
    {
      [Pure]
      bool IsValid(T outBuf);
      void TryGet(T outBuf, int timeOut);
    }

    /// <summary>
    /// Checks for infinite recursion in specialization (due to self-instantiation types)
    /// </summary>
    class Foo<T>
    {
      IBar<T> source = null;
      T curBuffer = default(T);
      int TimeOut { get; set; }

      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as field receiver)", PrimaryILOffset = 2, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as field receiver)", PrimaryILOffset = 8, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as receiver)", PrimaryILOffset = 14, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as receiver)", PrimaryILOffset = 19, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"invariant unproven: source.IsValid(curBuffer)", PrimaryILOffset = 18, MethodILOffset = 25)]
      void FooMethod()
      {
        // replacing TimeOut with a constant prevents the crash
        source.TryGet(curBuffer, TimeOut);
      }

      [ContractInvariantMethod]
      private void ObjectInvariant()
      {
        // commenting out this line prevents the crash
        Contract.Invariant(source.IsValid(curBuffer));
      }
    }

    // Check that in reference contexts, we don't loose nullness if we go into a generic
    // context and box.
    namespace BoxingAndimplicitInterfaceContractImplementations
    {
      using System;
      using System.Diagnostics.Contracts;

      [ContractClass(typeof(IFooContract<>))]
      public interface IFoo<T> //where T:class
      {
        void FooMethod(T x);
      }

      [ContractClassFor(typeof(IFoo<>))]
      abstract class IFooContract<T> : IFoo<T>
      {
        // Check that implicit interface contracts like this are picked up by Clousot
        public void FooMethod(T x)
        {
          Contract.Requires(x != null);
        }
      }

      public class Foo0 : IFoo<Random>
      {
        public void FooMethod(Random x) { }

        [ClousotRegressionTest]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 27, MethodILOffset = 0)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 13, MethodILOffset = 27)]
        public void Bar(object x)
        {
          var r = x as Random;
          if (r == null)
            throw new ArgumentException();
          FooMethod(r); // should succeed
        }
      }

      public class Foo1 : IFoo<Random>
      {
        public void FooMethod(Random x) { }

        [ClousotRegressionTest]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 27, MethodILOffset = 0)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 13, MethodILOffset = 27)]
        public void Bar(object x)
        {
          var r = x as Random;
          if (r == null)
            throw new ArgumentException();

          FooMethod(r); // should succeed
        }
      }

      public class Foo2 : IFoo<Random>
      {
        public void FooMethod(Random x) { }

        [ClousotRegressionTest]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 10, MethodILOffset = 0)]
        [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"requires unproven: x != null", PrimaryILOffset = 13, MethodILOffset = 10)]
        public void Bar(object x)
        {
          var r = x as Random;
          FooMethod(r);  // should fail
        }
      }

    }
  }

  namespace JonathanAllen
  {
    class VBStringCompare
    {
      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 10, MethodILOffset = 28)]
      public static int Ciccio(string s)
      {
        if (MyCompare(s, "") == 0)
        {
          return 0;
        }

        Foo(s);

        return 1;
      }

      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as receiver)", PrimaryILOffset = 18, MethodILOffset = 140)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as receiver)", PrimaryILOffset = 29, MethodILOffset = 140)]
      [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"Possibly calling a method on a null reference 'Left'. The static checker determined that the condition 'Left != null' should hold on entry. Nevertheless, the condition may be too strong for the callers. If you think it is ok, add a precondition to document it: Contract.Requires(Left != null);", PrimaryILOffset = 37, MethodILOffset = 140)]
      [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"Possibly calling a method on a null reference 'Right'. The static checker determined that the condition 'Right != null' should hold on entry. Nevertheless, the condition may be too strong for the callers. If you think it is ok, add a precondition to document it: Contract.Requires(Right != null);", PrimaryILOffset = 43, MethodILOffset = 140)]
      [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "ensures unproven: Contract.Result<int>() != 0 || ((Left == null && Right == null) || (Left == null && Right.Length == 0) || (Right == null && Left.Length == 0) || (Left.Length == Right.Length))", PrimaryILOffset = 56, MethodILOffset = 140)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 129, MethodILOffset = 140)]
      public static int MyCompare(string Left, string Right)
      {
        Contract.Ensures(Contract.Result<int>() != 0 ||
          ((Left == null && Right == null) ||
          (Left == null && Right.Length == 0) ||
          (Right == null && Left.Length == 0) ||
          (Left.Length == Right.Length)));

        Contract.Ensures(Contract.Result<int>() == 0 ||
          ((Right == null && Left.Length > 0) ||
          (Left == null && Right.Length > 0) ||
          (Left != null && Right != null && (Left.Length > 0 || Right.Length > 0))));


        return default(int);
      }

      [ClousotRegressionTest]
      public static void Foo(string s)
      {
        Contract.Requires(!string.IsNullOrEmpty(s));
      }
    }
  }

  namespace Sexton
  {
    class Test
    {
      private int value;
      private Settings settings;

      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 1, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 23, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 28, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 39, MethodILOffset = 0)]
      public Test(string foo)
      {
        Contract.Requires(foo != null);

        this.value = foo.Length;
        this.settings = new Settings();
      }
    }

    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    class Settings
    {
    }

    struct CheckExtraManifestation
    {
      public IList<int> List
      {
        [ClousotRegressionTest]
        [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=15,MethodILOffset=39)]
        [RegressionOutcome(Outcome=ProofOutcome.True,Message="ensures is valid",PrimaryILOffset=23,MethodILOffset=39)]
        get
        {
          Contract.Ensures(!initialized || list.IsReadOnly);
          
          return list;
        }
      }

      private readonly IList<int> list;
      private readonly bool initialized;

      [ClousotRegressionTest]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=21,MethodILOffset=0)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=26,MethodILOffset=0)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=37,MethodILOffset=0)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=50,MethodILOffset=0)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="invariant is valid",PrimaryILOffset=24,MethodILOffset=55)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="invariant is valid",PrimaryILOffset=52,MethodILOffset=55)]
      public CheckExtraManifestation(IList<int> items)
      {
        Contract.Requires(items != null);

        list = new List<int>(items).AsReadOnly();

        Contract.Assume(list.IsReadOnly);

        initialized = true;
      }
      
      [ContractInvariantMethod]
      void ObjectInvariant()
      {
        Contract.Invariant(!initialized || list != null);
        Contract.Invariant(!initialized || list.IsReadOnly);
      }
    }

  }
  namespace PeterGolde
  {
    class C
    {
      public int Data { get; private set; }

      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 1, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 20, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 10, MethodILOffset = 27)]
      public C(int data)
      {
        Contract.Requires(data > 0);
        this.Data = data;
      }

      [ContractInvariantMethod]
      private void ObjectInvariant()
      {
        Contract.Invariant(Data > 0);
      }

      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "Possibly calling a method on a null reference 'c'", PrimaryILOffset = 2, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 10, MethodILOffset = 0)]
      public static void T(C c)
      {
        Contract.Assert(c.Data > 0);
      }
    }
  }
  namespace Eugene
  {
    public class Window
    {
      [ClousotRegressionTest]
      [ClousotRegressionTest("cci2only")]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 2, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 24, MethodILOffset = 38)]
      private void TestPos()
      {
        IntPtr hwnd = GetForegroundWindow();

        if (hwnd == IntPtr.Zero)
          throw new ApplicationException("Hwnd cannot be zero");

        var window = new Window(hwnd);
      }

      [ClousotRegressionTest]
      [ClousotRegressionTest("cci2only")]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 2, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"requires unproven: hwnd != IntPtr.Zero (hwnd)", PrimaryILOffset = 24, MethodILOffset = 9)]
      private void TestNeg()
      {
        IntPtr hwnd = GetForegroundWindow();

        var window = new Window(hwnd);
      }

      public Window(IntPtr hwnd)
      {
        Contract.Requires<ArgumentNullException>(hwnd != IntPtr.Zero, "hwnd");
        //some other code
      }

      IntPtr GetForegroundWindow()
      {
        return new IntPtr();
      }
    }
  }

  namespace JoelBaranick
  {
    public class StoreValue<TKey, TStatus, TValue> : IEquatable<StoreValue<TKey, TStatus, TValue>> where TStatus : IComparable<TStatus>
    {
      #region IEquatable<StoreValue<TKey,TStatus,TValue>> Members

      public bool Equals(StoreValue<TKey, TStatus, TValue> other)
      {
        throw new NotImplementedException();
      }

      #endregion
    }

    /// <summary>
    /// Check that we pickup abstract method contracts when the class is generic.
    /// </summary>
    [ContractClass(typeof(StoreBaseContract<,,>))]
    public abstract class StoreBase<TKey, TStatus, TValue> where TStatus : IComparable<TStatus>
    {
      [ClousotRegressionTest]
      //[ClousotRegressionTest("cci2only")] cci2 is not picking up the abstract method contract
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 21, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 12, MethodILOffset = 35)]
      internal virtual StoreValue<TKey, TStatus, TValue> ReadFromStorage(TKey key)
      {
        Contract.Ensures(Contract.Result<StoreValue<TKey, TStatus, TValue>>() != null);

        try
        {
          return this.ReadFromStorageInternal(key);
        }
        catch (Exception)
        {
          throw;
        }
      }

      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 10, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 27, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 43, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 50, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 70, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 75, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 82, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 102, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 107, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 114, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 134, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 139, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 146, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 157, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 162, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 167, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 13, MethodILOffset = 43)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 13, MethodILOffset = 75)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 13, MethodILOffset = 107)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 13, MethodILOffset = 139)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 13, MethodILOffset = 167)]
      // check that exception.Data.Add does not modify exception.Data.
      protected Exception GetStoreException(string message, TKey key, Exception e)
      {
        var exception = new Exception(message, e);
        if (exception.Data != null)
        {
          exception.Data.Add("Key", key);
          exception.Data.Add("KeyType", typeof(TKey).FullName);
          exception.Data.Add("StatusType", typeof(TStatus).FullName);
          exception.Data.Add("ValueType", typeof(TValue).FullName);
          exception.Data.Add("StoreType", this.GetType().FullName);
        }

        return exception;
      }

      /// <summary>
      /// Reads the status from storage.
      /// </summary>
      /// <param name="key">The store key.</param>
      /// <returns>The status.</returns>
      protected abstract StoreValue<TKey, TStatus, TValue> ReadFromStorageInternal(TKey key);
    }

    [ContractClassFor(typeof(StoreBase<,,>))]
    internal abstract class StoreBaseContract<TKey, TStatus, TValue> : StoreBase<TKey, TStatus, TValue>
      where TStatus : IComparable<TStatus>
    {

      protected override StoreValue<TKey, TStatus, TValue> ReadFromStorageInternal(TKey key)
      {
        Contract.Ensures(Contract.Result<StoreValue<TKey, TStatus, TValue>>() != null);

        throw new NotImplementedException();
      }
    }

    [ContractVerification(true)]
    class Paths<TKey>
    {
      string storeDirectory;

      public Paths(string s)
      {
        storeDirectory = s;
      }
      /// <summary>
      /// Gets the store filename.
      /// </summary>
      /// <param name="key">The store key.</param>
      /// <returns>The path to the store file.</returns>
      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as array)", PrimaryILOffset = 100, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as field receiver)", PrimaryILOffset = 109, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as receiver)", PrimaryILOffset = 114, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as field receiver)", PrimaryILOffset = 129, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"Array creation : ok", PrimaryILOffset = 86, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"Lower bound access ok", PrimaryILOffset = 100, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"Upper bound access ok", PrimaryILOffset = 100, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 13, MethodILOffset = 102)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 31, MethodILOffset = 102)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 122, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 13, MethodILOffset = 135)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 31, MethodILOffset = 135)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"invariant is valid", PrimaryILOffset = 15, MethodILOffset = 166)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 40, MethodILOffset = 166)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 69, MethodILOffset = 166)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"No overflow (caused by a negative array size)",PrimaryILOffset=86,MethodILOffset=0)]
      internal string GetStoreFilename(TKey key)
      {
        Contract.Requires(!Equals(null, key));
        Contract.Ensures(!String.IsNullOrEmpty(Contract.Result<string>()), "result non-empty");
        Contract.Ensures(!String.IsNullOrEmpty(Path.GetDirectoryName(Contract.Result<string>())), "directory of result non-empty");

        string fileName = string.Format(CultureInfo.InvariantCulture, "{0}.xml", key);
        Contract.Assert(this.storeDirectory.Length > 0);

        var result = Path.Combine(this.storeDirectory, fileName);
        Contract.Assume(!String.IsNullOrEmpty(Path.GetDirectoryName(result)));
        return result;
      }

      [ContractInvariantMethod]
      private void ObjectInvariant()
      {
        Contract.Invariant(!String.IsNullOrEmpty(this.storeDirectory));
      }
    }
  }


  class Paths<TKey>
  {
    string storeDirectory;

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 1, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 25, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 15, MethodILOffset = 31)]
    public Paths(string s)
    {
      Contract.Requires(!String.IsNullOrEmpty(s));
      storeDirectory = s;
    }
    /// <summary>
    /// Gets the store filename.
    /// </summary>
    /// <param name="key">The store key.</param>
    /// <returns>The path to the store file.</returns>
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as array)", PrimaryILOffset = 100, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 109, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 114, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 129, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Array creation : ok", PrimaryILOffset = 86, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 100, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 100, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 13, MethodILOffset = 102)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 31, MethodILOffset = 102)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 122, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 13, MethodILOffset = 135)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 31, MethodILOffset = 135)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 15, MethodILOffset = 166)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 40, MethodILOffset = 166)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 69, MethodILOffset = 166)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"No overflow (caused by a negative array size)",PrimaryILOffset=86,MethodILOffset=0)]
    internal string GetStoreFilename(TKey key)
    {
      Contract.Requires(!Equals(null, key));
      Contract.Ensures(!String.IsNullOrEmpty(Contract.Result<string>()), "result non-empty");
      Contract.Ensures(!String.IsNullOrEmpty(System.IO.Path.GetDirectoryName(Contract.Result<string>())), "directory of result non-empty");

      string fileName = string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0}.xml", key);
      Contract.Assert(this.storeDirectory.Length > 0);

      var result = System.IO.Path.Combine(this.storeDirectory, fileName);
      Contract.Assume(!String.IsNullOrEmpty(System.IO.Path.GetDirectoryName(result)));
      return result;
    }

    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(!String.IsNullOrEmpty(this.storeDirectory));
    }
  }

}

namespace TestFrameworkOOB.Properties
{
  using System;


  /// <summary>
  ///   A strongly-typed resource class, for looking up localized strings, etc.
  /// </summary>
  // This class was auto-generated by the StronglyTypedResourceBuilder
  // class via a tool like ResGen or Visual Studio.
  // To add or remove a member, edit your .ResX file then rerun ResGen
  // with the /str option, or rebuild your VS project.
  [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0")]
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
  [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
  internal class Resources
  {

    private static global::System.Resources.ResourceManager resourceMan;

    private static global::System.Globalization.CultureInfo resourceCulture;

    [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
    internal Resources()
    {
    }

    /// <summary>
    ///   Returns the cached ResourceManager instance used by this class.
    /// </summary>
    [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
    internal static global::System.Resources.ResourceManager ResourceManager
    {
      get
      {
        if (object.ReferenceEquals(resourceMan, null))
        {
          global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("TestFrameworkOOB.Properties.Resources", typeof(Resources).Assembly);
          resourceMan = temp;
        }
        return resourceMan;
      }
    }

    /// <summary>
    ///   Overrides the current thread's CurrentUICulture property for all
    ///   resource lookups using this strongly typed resource class.
    /// </summary>
    [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
    internal static global::System.Globalization.CultureInfo Culture
    {
      get
      {
        return resourceCulture;
      }
      set
      {
        resourceCulture = value;
      }
    }

    /// <summary>
    ///   Looks up a localized string similar to Argument cannot be null.
    /// </summary>
    internal static string UserMessage1
    {
      get
      {
        return ResourceManager.GetString("UserMessage1", resourceCulture);
      }
    }
  }

  namespace KenMuse {
    using System;
    class C{
      [ClousotRegressionTest]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as array)",PrimaryILOffset=21,MethodILOffset=0)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=31,MethodILOffset=0)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="Array creation : ok",PrimaryILOffset=7,MethodILOffset=0)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="Lower bound access ok",PrimaryILOffset=21,MethodILOffset=0)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="Upper bound access ok",PrimaryILOffset=21,MethodILOffset=0)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="requires is valid",PrimaryILOffset=13,MethodILOffset=23)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="requires is valid",PrimaryILOffset=31,MethodILOffset=23)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="requires is valid",PrimaryILOffset=13,MethodILOffset=31)]
      [RegressionOutcome(Outcome=ProofOutcome.Top,Message="requires unproven: index < this.Length",PrimaryILOffset=33,MethodILOffset=31)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"No overflow (caused by a negative array size)",PrimaryILOffset=7,MethodILOffset=0)]
      public char M(int a){
        string s = String.Format("{0}", new object[]{ a });
        return s[0];
      }
    }
    
  }

  namespace EriZeitler {
    class A: IDisposable
    {
        object _a;

        [ContractInvariantMethod]
        private void ObjectInvariants()
        {
            Contract.Invariant(_a != null);
        }

        [ClousotRegressionTest]
        [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=3,MethodILOffset=0)]
        void IDisposable.Dispose()
        {
            _a = null;
        }
    }

    class B: IDisposable
    {
        object _b;

        [ContractInvariantMethod]
        private void ObjectInvariants()
        {
            Contract.Invariant(_b != null);
        }

        [ClousotRegressionTest]
        [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=3,MethodILOffset=0)]
        public void Dispose()
        {
            _b = null;
        }
    }

  }

  namespace AndreyTitov
  {
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using System.Text;


    public sealed class PropertyState
    {
        private readonly int m_index;
        private readonly bool m_isValid;
        private readonly bool m_valueWillChangedWhenRecall;
        private readonly bool m_recallIsCostly;
        private static readonly PropertyState[] s_allStates;
        private static readonly int[,] s_transitions;

        private const int PredefinedStatesCount = 9;

        [ClousotRegressionTest]
        [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as array)",PrimaryILOffset=16,MethodILOffset=0)]
        [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as array)",PrimaryILOffset=24,MethodILOffset=0)]
        [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as array)",PrimaryILOffset=32,MethodILOffset=0)]
        [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as array)",PrimaryILOffset=40,MethodILOffset=0)]
        [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as array)",PrimaryILOffset=48,MethodILOffset=0)]
        [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as array)",PrimaryILOffset=56,MethodILOffset=0)]
        [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as array)",PrimaryILOffset=64,MethodILOffset=0)]
        [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as array)",PrimaryILOffset=72,MethodILOffset=0)]
        [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as array)",PrimaryILOffset=80,MethodILOffset=0)]
        [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as array)",PrimaryILOffset=92,MethodILOffset=0)]
        [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=134,MethodILOffset=0)]
        [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=154,MethodILOffset=0)]
        [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=175,MethodILOffset=0)]
        [RegressionOutcome(Outcome=ProofOutcome.True,Message="Array creation : ok",PrimaryILOffset=3,MethodILOffset=0)]
        [RegressionOutcome(Outcome=ProofOutcome.True,Message="Lower bound access ok",PrimaryILOffset=16,MethodILOffset=0)]
        [RegressionOutcome(Outcome=ProofOutcome.True,Message="Upper bound access ok",PrimaryILOffset=16,MethodILOffset=0)]
        [RegressionOutcome(Outcome=ProofOutcome.True,Message="Lower bound access ok",PrimaryILOffset=24,MethodILOffset=0)]
        [RegressionOutcome(Outcome=ProofOutcome.True,Message="Upper bound access ok",PrimaryILOffset=24,MethodILOffset=0)]
        [RegressionOutcome(Outcome=ProofOutcome.True,Message="Lower bound access ok",PrimaryILOffset=32,MethodILOffset=0)]
        [RegressionOutcome(Outcome=ProofOutcome.True,Message="Upper bound access ok",PrimaryILOffset=32,MethodILOffset=0)]
        [RegressionOutcome(Outcome=ProofOutcome.True,Message="Lower bound access ok",PrimaryILOffset=40,MethodILOffset=0)]
        [RegressionOutcome(Outcome=ProofOutcome.True,Message="Upper bound access ok",PrimaryILOffset=40,MethodILOffset=0)]
        [RegressionOutcome(Outcome=ProofOutcome.True,Message="Lower bound access ok",PrimaryILOffset=48,MethodILOffset=0)]
        [RegressionOutcome(Outcome=ProofOutcome.True,Message="Upper bound access ok",PrimaryILOffset=48,MethodILOffset=0)]
        [RegressionOutcome(Outcome=ProofOutcome.True,Message="Lower bound access ok",PrimaryILOffset=56,MethodILOffset=0)]
        [RegressionOutcome(Outcome=ProofOutcome.True,Message="Upper bound access ok",PrimaryILOffset=56,MethodILOffset=0)]
        [RegressionOutcome(Outcome=ProofOutcome.True,Message="Lower bound access ok",PrimaryILOffset=64,MethodILOffset=0)]
        [RegressionOutcome(Outcome=ProofOutcome.True,Message="Upper bound access ok",PrimaryILOffset=64,MethodILOffset=0)]
        [RegressionOutcome(Outcome=ProofOutcome.True,Message="Lower bound access ok",PrimaryILOffset=72,MethodILOffset=0)]
        [RegressionOutcome(Outcome=ProofOutcome.True,Message="Upper bound access ok",PrimaryILOffset=72,MethodILOffset=0)]
        [RegressionOutcome(Outcome=ProofOutcome.True,Message="Lower bound access ok",PrimaryILOffset=80,MethodILOffset=0)]
        [RegressionOutcome(Outcome=ProofOutcome.True,Message="Upper bound access ok",PrimaryILOffset=80,MethodILOffset=0)]
        [RegressionOutcome(Outcome=ProofOutcome.True,Message="assert is valid",PrimaryILOffset=98,MethodILOffset=0)]
        [RegressionOutcome(Outcome=ProofOutcome.Top,Message="assert unproven",PrimaryILOffset=142,MethodILOffset=0)]
        [RegressionOutcome(Outcome=ProofOutcome.Top,Message="assert unproven",PrimaryILOffset=163,MethodILOffset=0)]
        [RegressionOutcome(Outcome=ProofOutcome.Top,Message="assert unproven",PrimaryILOffset=184,MethodILOffset=0)]
        [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"No overflow (caused by a negative array size)",PrimaryILOffset=3,MethodILOffset=0)]
        [RegressionOutcome(Outcome=ProofOutcome.True,Message="Array creation : ok (dimension 0)",PrimaryILOffset=108,MethodILOffset=0)]
        [RegressionOutcome(Outcome=ProofOutcome.True,Message="Array creation : ok (dimension 1)",PrimaryILOffset=108,MethodILOffset=0)]
        static PropertyState()
        {
            s_allStates = new[]
            {
                Constant,
                Calculating,
                LongCalculated,
                CalculationPended,
                DynamicalyChanging,
                Disposable,
                Action,
                Invalid,
                Unsupported,
            };

            Contract.Assert(s_allStates.Length == PredefinedStatesCount);

            s_transitions = new[,]
            {
                /*    0  1  2  3  4  5  6  7  8*/
                /*0*/{0, 1, 2, 3, 4, 0, 7, 7, 8},
                /*1*/{1, 1, 2, 3, 4, 1, 7, 7, 8},
                /*2*/{2, 2, 2, 3, 4, 2, 7, 7, 8},
                /*3*/{3, 3, 3, 3, 4, 3, 7, 7, 8},
                /*4*/{4, 4, 4, 4, 4, 4, 7, 7, 8},
                /*5*/{0, 1, 2, 3, 4, 5, 7, 7, 8},
                /*6*/{7, 7, 7, 7, 7, 7, 7, 7, 8},
                /*7*/{7, 7, 7, 7, 7, 7, 7, 7, 8},
                /*8*/{8, 8, 8, 8, 8, 8, 8, 8, 8},
            };

            // Next line crashes Code Clontracts
            Contract.Assert(s_transitions.Rank == 2);
            Contract.Assert(s_transitions.GetLength(0) == PredefinedStatesCount);
            Contract.Assert(s_transitions.GetLength(1) == PredefinedStatesCount);
        }

        [ClousotRegressionTest]
        [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=1,MethodILOffset=0)]
        [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=95,MethodILOffset=0)]
        [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=102,MethodILOffset=0)]
        [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=109,MethodILOffset=0)]
        [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=117,MethodILOffset=0)]
        [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=33,MethodILOffset=123)]
        [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=48,MethodILOffset=123)]
        [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=63,MethodILOffset=123)]
        [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=78,MethodILOffset=123)]
        [RegressionOutcome(Outcome=ProofOutcome.True,Message="ensures is valid",PrimaryILOffset=41,MethodILOffset=123)]
        [RegressionOutcome(Outcome=ProofOutcome.True,Message="ensures is valid",PrimaryILOffset=56,MethodILOffset=123)]
        [RegressionOutcome(Outcome=ProofOutcome.True,Message="ensures is valid",PrimaryILOffset=71,MethodILOffset=123)]
        [RegressionOutcome(Outcome=ProofOutcome.True,Message="ensures is valid",PrimaryILOffset=87,MethodILOffset=123)]
        private PropertyState(
            int index,
            bool isValid,
            bool valueWillChangedWhenRecall,
            bool recallIsCostly
        )
        {
            Contract.Requires(index < PredefinedStatesCount);
            Contract.Requires(index >= 0);

            Contract.Ensures(m_index == index);
            Contract.Ensures(m_isValid == isValid);
            Contract.Ensures(m_valueWillChangedWhenRecall == valueWillChangedWhenRecall);
            Contract.Ensures(m_recallIsCostly == recallIsCostly);

            m_index = index;
            m_isValid = isValid;
            m_valueWillChangedWhenRecall = valueWillChangedWhenRecall;
            m_recallIsCostly = recallIsCostly;
        }

        #region Properties

        private int Index
        {
            get
            {
                Contract.Ensures(Contract.Result<int>() >= 0);
                Contract.Ensures(Contract.Result<int>() < PredefinedStatesCount);

                return m_index;
            }
        }

        public bool IsValid
        {
            get
            {
                return m_isValid;
            }
        }

        public bool ValueWillChangedWhenRecall
        {
            get
            {
                return m_valueWillChangedWhenRecall;
            }
        }

        public bool RecallIsCostly
        {
            get
            {
                return m_recallIsCostly;
            }
        }

        #endregion

        #region Values

        public static readonly PropertyState Constant;
        public static readonly PropertyState Calculating;
        public static readonly PropertyState LongCalculated;
        public static readonly PropertyState CalculationPended;
        public static readonly PropertyState DynamicalyChanging;
        public static readonly PropertyState Disposable;
        public static readonly PropertyState Action;
        public static readonly PropertyState Invalid;
        public static readonly PropertyState Unsupported;

        #endregion
    }

  }

  namespace MikeBarry {
    public class A
    {
      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 1, MethodILOffset = 0)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=10,MethodILOffset=0)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="requires is valid",PrimaryILOffset=13,MethodILOffset=10)]
      public A()
      {
        Test(this);
      }        
      
      private void Test(object o)
      {
        Contract.Requires<ArgumentException>(o as A != null);
      }
    }

  }

  namespace AndrewAnderson
  {
    class MyClass
    {
      [ClousotRegressionTest]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=20,MethodILOffset=0)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="requires is valid",PrimaryILOffset=17,MethodILOffset=20)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="requires is valid",PrimaryILOffset=125,MethodILOffset=20)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="requires is valid",PrimaryILOffset=233,MethodILOffset=20)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="requires is valid",PrimaryILOffset=373,MethodILOffset=20)]
      public MyClass(string id)
        : this(id, null, null)
      {
        Contract.Requires(!string.IsNullOrEmpty(id));
      }

      [ClousotRegressionTest]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=27,MethodILOffset=0)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=47,MethodILOffset=0)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=60,MethodILOffset=0)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=71,MethodILOffset=0)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=81,MethodILOffset=0)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=95,MethodILOffset=0)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=106,MethodILOffset=0)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=135,MethodILOffset=0)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=155,MethodILOffset=0)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=168,MethodILOffset=0)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=179,MethodILOffset=0)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=189,MethodILOffset=0)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=203,MethodILOffset=0)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=214,MethodILOffset=0)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=243,MethodILOffset=0)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=263,MethodILOffset=0)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=276,MethodILOffset=0)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=287,MethodILOffset=0)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=307,MethodILOffset=0)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=320,MethodILOffset=0)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=333,MethodILOffset=0)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=340,MethodILOffset=0)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=349,MethodILOffset=0)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=356,MethodILOffset=0)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=1,MethodILOffset=0)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=381,MethodILOffset=0)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=392,MethodILOffset=0)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=404,MethodILOffset=0)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=409,MethodILOffset=0)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=420,MethodILOffset=0)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=433,MethodILOffset=0)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=438,MethodILOffset=0)]
      public MyClass(string id, short? startHour, short? endHour)
      {
        Contract.Requires(!string.IsNullOrEmpty(id));
        Contract.Requires((startHour == null) || (startHour >= 0 && startHour <= 23), "startHour must be between 0 and 23");
        Contract.Requires((endHour == null) || (endHour >= 0 && endHour <= 23), "endHour must be between 0 and 23");
        Contract.Requires((startHour == null || endHour == null) || (startHour <= endHour), "Parameter startHour cannot exceed parameter endHour");
        
        Id = id;
        StartHour = (startHour ?? 0);
        EndHour = (endHour ?? 23);
      }
      
      public string Id { get; set; }
      
      public short StartHour { get; set; }
      
      public short EndHour { get; set; }
    }
  }

  namespace Jamie {
    class TestOperators
    {
      [ClousotRegressionTest]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="requires is valid",PrimaryILOffset=8,MethodILOffset=11)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="requires is valid",PrimaryILOffset=8,MethodILOffset=27)]
      static void TestOps()
      {
        Work((string)new Class());
        Work(new Class());
      }
      
      private static void Work(string p)
      {
        Contract.Requires(p != null);
      }
      private static void Work(int[] p)
      {
        Contract.Requires(p != null);
      }
    }

    public sealed class Class
    {
      public static explicit operator bool(Class c)
      {
        return false;
      }

      public static explicit operator string(Class c)
      {
        Contract.Ensures(Contract.Result<string>() != null);
        
        return string.Empty;
      }
      public static implicit operator int[](Class c)
      {
        Contract.Ensures(Contract.Result<int[]>() != null);
        
        return new int[0];
      }
    }
  }

  namespace Porges
  {
    public abstract class MemoryEncoder
    {
      protected MemoryEncoder()
      {
        buffer = new byte[512];
        Length = 0;
        CurrentIndex = 0;
      }

      private byte[] buffer;

      public int CurrentIndex { get; set; }
      public int Length { get; private set; }

      [ClousotRegressionTest("cci1only")] // See below test for the equivalent test in CCI2.
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=15,MethodILOffset=0)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=25,MethodILOffset=0)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as array)",PrimaryILOffset=30,MethodILOffset=0)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=48,MethodILOffset=0)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as array)",PrimaryILOffset=53,MethodILOffset=0)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=68,MethodILOffset=0)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="assert is valid",PrimaryILOffset=60,MethodILOffset=0)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="invariant is valid",PrimaryILOffset=13,MethodILOffset=75)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="invariant is valid",PrimaryILOffset=38,MethodILOffset=75)]
      public void ReserveSpace_CCI1(int extra)
      {
        Contract.Requires(extra >= 0);

        var newLen = Length + extra;

        if (newLen > buffer.Length)
        {
          // ignore...
        } else
        {

          Contract.Assert(newLen <= buffer.Length);
          Length = newLen;
        }
      }

      // CCI2 uses the invariant as a precondition for the auto-property's setter because the setter
      // is private (and the invariant mentions a private field). So that precondition is checked
      // (and validated) in the call to the setter in this method.
      [ClousotRegressionTest("cci2only")] // see the above test for the equivalent CCI1 test.
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 15, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 25, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as array)", PrimaryILOffset = 30, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 48, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as array)", PrimaryILOffset = 53, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 68, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 60, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 13, MethodILOffset = 75)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 38, MethodILOffset = 75)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 38, MethodILOffset = 68)]
      public void ReserveSpace_CCI2(int extra) {
        Contract.Requires(extra >= 0);

        var newLen = Length + extra;

        if (newLen > buffer.Length) {
          // ignore...
        } else {

          Contract.Assert(newLen <= buffer.Length);
          Length = newLen;
        }
      }

      [ContractInvariantMethod]
      private void Invariants()
      {
        Contract.Invariant(buffer != null);
        Contract.Invariant(Length <= buffer.Length);
      }
    }
  }

  namespace DaveSexton {
    [ContractClass(typeof(IFooContract))]
    interface IFoo
    {
      bool Initialized { get; }
      object Value { get; }
    }

    [ContractClassFor(typeof(IFoo))]
    abstract class IFooContract : IFoo
    {
      public bool Initialized
      {
        get
        {
          // Contract.Ensures(true);
          
          return false;
        }

      }
      public object Value
      {
        get
        {
          Contract.Ensures(Initialized);
          return null;
        }
      }
    }

    class Foo : IFoo
    {
      public bool Initialized
      {
        get
        {
          Contract.Ensures(Contract.Result<bool>() || value == null);

          return value != null;
        }
      }

      private object value;
      public object Value
      {
        [ClousotRegressionTest]
          [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=20,MethodILOffset=0)]
          [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=41,MethodILOffset=0)]
          [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=47,MethodILOffset=0)]
          [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=2,MethodILOffset=56)]
          [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=2,MethodILOffset=56)]
          [RegressionOutcome(Outcome=ProofOutcome.True,Message="ensures is valid",PrimaryILOffset=7,MethodILOffset=56)]
          [RegressionOutcome(Outcome=ProofOutcome.True,Message="ensures is valid",PrimaryILOffset=13,MethodILOffset=56)]
        get {
          Contract.Ensures(value != null);

          if (value == null)
            value = new object();

          // inherited ensures should be proven : Initialized
          return value;
        }
      }
    }
  }
}
