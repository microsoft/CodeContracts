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
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using Microsoft.Research.ClousotRegression;
using System.Xml.Linq;
using System.Diagnostics;
using System.Collections.Specialized;

namespace TestFrameworkOOB
{
  class NonNull
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 32, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 37, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 25, MethodILOffset = 46)]
    string CheckBackToBackNonNullness(StringBuilder s)
    {
      Contract.Requires(s != null);
      Contract.Ensures(Contract.Result<string>() != null);

      return s.ToString().Trim();
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 12, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 29, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 46, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 74, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 82, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 106, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 111, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 63, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 95, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 124, MethodILOffset = 0)]
    void CheckCurrentXXXNullness()
    {
      Contract.Assert(System.Threading.Thread.CurrentThread != null);

      Contract.Assert(System.AppDomain.CurrentDomain != null);

      Contract.Assert(System.Globalization.CultureInfo.CurrentCulture != null);

      Contract.Assert(Process.GetCurrentProcess() != null);

      ProcessThreadCollection ptc = Process.GetCurrentProcess().Threads;

      ProcessThread pt = ptc[0];
      Contract.Assert(ptc != null);

      StringDictionary env = Process.GetCurrentProcess().StartInfo.EnvironmentVariables;

      Contract.Assert(env != null);
    }

    // Tests a generic method within a non-generic class for the oob forwarding
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 63, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 13, MethodILOffset = 15)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 28, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 13, MethodILOffset = 35)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 13, MethodILOffset = 41)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 54, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 76, MethodILOffset = 0)]
    void CheckLinqNullness(int[] arg)
    {
      Contract.Requires(arg != null);

      var arr = System.Linq.Enumerable.ToArray(arg);

      Contract.Assert(arr != null);

      IQueryable<int> query = System.Linq.Queryable.Skip(System.Linq.Queryable.AsQueryable(arr), 0);
      Contract.Assert(query != null);
      IQueryable q2 = query;
      IQueryProvider qp = q2.Provider;
      Contract.Assert(qp != null);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 15, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 33, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 38, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 56, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 61, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 66, MethodILOffset = 0)]
    void GeoffTest(XDocument calendar)
    {
      Contract.Requires(calendar != null);
      Contract.Requires(calendar.Root != null);
      Contract.Requires(calendar.Root.Name != null);

      if (calendar.Root.Name.Namespace != "foobar")
      {
        throw new ArgumentException();
      }
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "Possibly calling a method on a null reference 'dict'", PrimaryILOffset = 19, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 12, MethodILOffset = 28)]
    public ICollection<Key> GetKeys<Key, Value>(IDictionary<Key, Value> dict)
    {
      Contract.Ensures(Contract.Result<ICollection<Key>>() != null);

      return dict.Keys;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 8, MethodILOffset = 40)]
    static void GenericCreation<T>() where T : new()
    {
      T t = new T();
      RequireNonNullarg(t);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 8, MethodILOffset = 13)]
    static void GenericCreation2<T>() where T : class, new()
    {
      T t = new T();
      RequireNonNullarg(t);
    }

    [ClousotRegressionTest]
#if NETFRAMEWORK_4_0
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="requires is valid",PrimaryILOffset=8,MethodILOffset=15)]
#else
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 8, MethodILOffset = 17)]
#endif
    static void GenericCreation3<T>() where T : struct
    {
      T t = new T();
      RequireNonNullarg(t);
    }

    static void RequireNonNullarg(object value)
    {
      Contract.Requires(value != null);
    }


    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (in unbox)",PrimaryILOffset=20,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="requires is valid",PrimaryILOffset=13,MethodILOffset=15)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="assert is valid",PrimaryILOffset=38,MethodILOffset=0)]
    public void TryCreate<T>(Type t)
    {
      Contract.Requires(t != null);
      var newInstance = (T)Activator.CreateInstance(t);
      Contract.Assert(newInstance != null);
    }
  }

  public class Bar { }
  
  public static class BoolExt {
    [Pure]
    public static bool Implies(this bool antecedant, bool consequent)
    {
      Contract.Ensures(Contract.Result<bool>() && (!antecedant || consequent) || !Contract.Result<bool>() && antecedant && !consequent);
      return !antecedant || consequent;
    }
  }
  public class UserJauernigFeedback
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 68, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 15, MethodILOffset = 106)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 40, MethodILOffset = 106)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 33, MethodILOffset = 106)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 61, MethodILOffset = 106)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=13,MethodILOffset=88)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="requires is valid",PrimaryILOffset=30,MethodILOffset=88)]
    public Bar GetFirst(IList<Bar> list)
    {
      Contract.Requires(list != null);
      // implication: [] => null  
      Contract.Ensures(!(list.Count == 0) || (Contract.Result<Bar>() == null));
      // implication: ![] => !null  
      Contract.Ensures((list.Count == 0) || (Contract.Result<Bar>() != null));

      if (list.Count == 0)
        return null;
      return list.First() ?? new Bar();
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 120, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 58, MethodILOffset = 170)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 86, MethodILOffset = 170)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 79, MethodILOffset = 170)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 113, MethodILOffset = 170)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=13,MethodILOffset=140)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="requires is valid",PrimaryILOffset=30,MethodILOffset=140)]
    public Bar GetFirst2(IList<Bar> list)
    {
      Contract.Requires(list != null);
      Contract.Requires(Contract.ForAll(list, elem => elem != null));
      Contract.Ensures((list.Count == 0).Implies(Contract.Result<Bar>() == null));
      Contract.Ensures((list.Count != 0).Implies(Contract.Result<Bar>() != null));

      if (list.Count == 0)
        return null;

      var result = list.First();
      if (result == null) return new Bar();
      return result;
    }
  }

  class UserArnottFeedback
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 18, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 8, MethodILOffset = 18)]
    public void Foo1(Uri uri)
    {
      if (uri != null)
      {
        Bar(uri);
      }
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 20, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 8, MethodILOffset = 20)]
    public void Foo2(Uri uri)
    {
      if (uri == null)
      {
        return;
      }

      Bar(uri);
    }

    [ClousotRegressionTest]
    public void Bar(Uri uri)
    {
      Contract.Requires(uri != null);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "requires unproven: input != null", PrimaryILOffset = 13, MethodILOffset = 4)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "requires unproven: pattern != null", PrimaryILOffset = 31, MethodILOffset = 4)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "requires unproven: replacement != null", PrimaryILOffset = 49, MethodILOffset = 4)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 17, MethodILOffset = 0)]
    public void TestRegex(string input, string pattern, string replacement)
    {
      var result = System.Text.RegularExpressions.Regex.Replace(input, pattern, replacement);
      Contract.Assert(result != null);
    }
  }
}

