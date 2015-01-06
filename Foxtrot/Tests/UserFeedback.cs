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
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.Contracts;
using System.Linq;

namespace UserFeedback
{
  namespace Peli
  {
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class UnitTest1
    {
      public class Bar
      {
        public Bar(int i)
        {
          this.ID = i;
        }
        public int ID { get; set; }
      }

      [ContractClass(typeof(CFoo))]
      public interface IFoo
      {
        Bar GetValue(int i);

        void TestException(Bar b, int i);
      }

      [ContractClassFor(typeof(IFoo))]
      class CFoo : IFoo
      {
        Bar IFoo.GetValue(int i)
        {
          Contract.Requires(i >= 0, "peli1");
          Contract.Requires(i < 10);
          Contract.Ensures(
              Contract.Result<Bar>() == null ||
              Contract.Result<Bar>().ID == 0, "peli2");
          return null;
        }

        void IFoo.TestException(Bar b, int i)
        {
          Contract.EnsuresOnThrow<IndexOutOfRangeException>(b.ID >= 0);
          Contract.EnsuresOnThrow<ArgumentException>(b.ID == 0, "Peli3");

        }
      }

      public class Foo : IFoo
      {
        int x;

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
          Contract.Invariant(x > 0);
          Contract.Invariant(x < 10, "upper bound");
        }

        public Foo()
        {
          x = 1;
        }

        public Foo(int init)
        {
          x = init;
        }

        public Bar GetValue(int i)
        {
          if (i == 1) { return new Bar(1); }
          return new Bar(0);
        }

        public void TestException(Bar b, int i)
        {
          b.ID = i;
          if (i == -1) throw new IndexOutOfRangeException();
          if (i == 1) throw new ArgumentException();
          if (i == 0) throw new ArgumentException();
        }
      }

      [TestMethod]
      public void TestProperlyRewrittenResult()
      {
        new Foo().GetValue(0);
      }

      [TestMethod]
      public void TestInvariantStrings1()
      {
        new Foo(2);
      }

      [TestMethod]
      public void TestInvariantStrings2()
      {
        try
        {
          new Foo(0);
          throw new Exception();
        }
        catch (TestRewriterMethods.InvariantException i)
        {
          Assert.AreEqual("x > 0", i.Condition);
          Assert.AreEqual(null, i.User);
        }
      }

      [TestMethod]
      public void TestInvariantStrings3()
      {
        try
        {
          new Foo(10);
          throw new Exception();
        }
        catch (TestRewriterMethods.InvariantException i)
        {
          Assert.AreEqual("x < 10", i.Condition);
          Assert.AreEqual("upper bound", i.User);
        }
      }



      [TestMethod]
      [ExpectedException(typeof(ArgumentException))]
      public void TestOnThrowPositive()
      {
        Bar b = new Bar(0);
        new Foo().TestException(b, 0);
      }

      [TestMethod]
      public void TestOnThrowNegative1()
      {
        Bar b = new Bar(0);
        try
        {
          new Foo().TestException(b, 1);
          throw new Exception();
        }
        catch (TestRewriterMethods.PostconditionOnThrowException p)
        {
          Assert.AreEqual("b.ID == 0", p.Condition);
          Assert.AreEqual("Peli3", p.User);
        }
      }

      [TestMethod]
      public void TestOnThrowNegative2()
      {
        Bar b = new Bar(0);
        try
        {
          new Foo().TestException(b, -1);
          throw new Exception();
        }
        catch (TestRewriterMethods.PostconditionOnThrowException p)
        {
          Assert.AreEqual("b.ID >= 0", p.Condition);
          Assert.AreEqual(null, p.User);
        }
      }



      [TestMethod]
      public void TestRequiresConditionStringAndUserString()
      {
        try
        {
          new Foo().GetValue(-1);
          throw new Exception();
        }
        catch (TestRewriterMethods.PreconditionException e)
        {
          if (e.Condition != "i >= 0") throw new Exception();
          if (e.User != "peli1") throw new Exception();
          return;
        }
      }

      [TestMethod]
      public void TestRequiresConditionStringOnly()
      {
        try
        {
          new Foo().GetValue(10);
          throw new Exception();
        }
        catch (TestRewriterMethods.PreconditionException e)
        {
          if (e.Condition != "i < 10") throw new Exception();
          if (e.User != null) throw new Exception();
          return;
        }
      }

      [TestMethod]
      public void TestEnsuresConditionStringAndUserString()
      {
        try
        {
          new Foo().GetValue(1);
        }
        catch (TestRewriterMethods.PostconditionException e)
        {
          Assert.AreEqual("Contract.Result<Bar>() == null ||              Contract.Result<Bar>().ID == 0", e.Condition);
          Assert.AreEqual("peli2", e.User);
          return;
        }
      }
    }


    public static class StringExtensions
    {
      public static string TrimSuffix(string source, string suffix)
      {
        Contract.Requires(source != null);
        Contract.Requires(!String.IsNullOrEmpty(suffix));
        Contract.Ensures(Contract.Result<string>() != null);
        Contract.Ensures(!Contract.Result<string>().EndsWith(suffix));

        #region Body
        var result = source;
        while (result.EndsWith(suffix))
        {
          var oldLength = result.Length;
          Contract.Assert(result.Length >= suffix.Length);
          Contract.Assert(suffix.Length > 0);
          var remainder = result.Length - suffix.Length;
          Contract.Assert(remainder < result.Length);
          result = result.Substring(0, remainder);
          Contract.Assert(result.Length < oldLength);
        }
        return result;
        #endregion
      }
    }
  }

  namespace WinSharp
  {
    using System.Linq;

    class MyDict<TKey, TValue> : IDictionary<TKey, TValue>
    {
      Dictionary<TKey, TValue> innerDictionary = new Dictionary<TKey, TValue>();

      public bool ContainsKey(TKey key)
      {
        TValue local;
        return this.TryGetValue(key, out local);
      }


      public bool TryGetValue(TKey key, out TValue value)
      {
        return this.innerDictionary.TryGetValue(key, out value);
      }

      #region IDictionary<TKey,TValue> Members

      public void Add(TKey key, TValue value)
      {
        throw new NotImplementedException();
      }

      public ICollection<TKey> Keys
      {
        get { throw new NotImplementedException(); }
      }

      public bool Remove(TKey key)
      {
        throw new NotImplementedException();
      }

      public ICollection<TValue> Values
      {
        get { throw new NotImplementedException(); }
      }

      public TValue this[TKey key]
      {
        get
        {
          throw new NotImplementedException();
        }
        set
        {
          throw new NotImplementedException();
        }
      }

      #endregion

      #region ICollection<KeyValuePair<TKey,TValue>> Members

      public void Add(KeyValuePair<TKey, TValue> item)
      {
        throw new NotImplementedException();
      }

      public void Clear()
      {
        throw new NotImplementedException();
      }

      public bool Contains(KeyValuePair<TKey, TValue> item)
      {
        throw new NotImplementedException();
      }

      public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
      {
        throw new NotImplementedException();
      }

      public int Count
      {
        get { throw new NotImplementedException(); }
      }

      public bool IsReadOnly
      {
        get { throw new NotImplementedException(); }
      }

      public bool Remove(KeyValuePair<TKey, TValue> item)
      {
        throw new NotImplementedException();
      }

      #endregion

      #region IEnumerable<KeyValuePair<TKey,TValue>> Members

      public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
      {
        throw new NotImplementedException();
      }

      #endregion

      #region IEnumerable Members

      System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
      {
        throw new NotImplementedException();
      }

      #endregion
    }

    public class ExplicitlyRecursive
    {
      [Pure]
      public static bool Odd(int x)
      {
        Contract.Ensures(Contract.Result<bool>() == !Even(x));

        return Math.Abs(x) % 2 == 1;
      }

      [Pure]
      public static bool Even(int x)
      {
        Contract.Ensures(Contract.Result<bool>() == !Odd(x));

        return Math.Abs(x) % 2 == 0;
      }

      [Pure]
      public static int SubOdd(int x)
      {
        Contract.Requires(SubEven(x - 1) == 0);

        if (x <= 0) return 0;
        if (Odd(x)) return SubEven(x - 1);
        return 0;
      }


      [Pure]
      public static int SubEven(int x)
      {
        Contract.Requires(SubOdd(x - 1) == 0);

        if (x <= 0) return 0;
        if (Even(x)) return SubOdd(x - 1);
        return 0;
      }

      [Pure]
      public static bool ThrowOnOdd(int x)
      {
        Contract.EnsuresOnThrow<ArgumentException>(Odd(x) && !ThrowOnEven(x));
        Contract.Ensures(!ThrowOnEven(x + 1));

        if (Odd(x)) throw new ArgumentException();

        return false;
      }

      [Pure]
      public static bool ThrowOnEven(int x)
      {
        Contract.EnsuresOnThrow<ArgumentException>(Even(x) && !ThrowOnOdd(x));
        Contract.Ensures(!ThrowOnOdd(x + 1));

        if (Even(x)) throw new ArgumentException();

        return false;
      }

    }


    [TestClass]
    public class RecursionChecks
    {
      [TestMethod]
      public void RecursionTest1()
      {
        var mydict = new MyDict<string, int>();

        var result = mydict.ContainsKey("foo");

        Assert.IsFalse(result);
      }

      [TestMethod]
      public void RecursionTest2()
      {
        var result = ExplicitlyRecursive.Odd(5);

        Assert.IsTrue(result);
      }

      [TestMethod]
      public void RecursionTest3()
      {
        var result = ExplicitlyRecursive.Odd(6);

        Assert.IsFalse(result);
      }

      [TestMethod]
      public void RecursionTest4()
      {
        var result = ExplicitlyRecursive.Even(7);

        Assert.IsFalse(result);
      }

      [TestMethod]
      public void RecursionTest5()
      {
        var result = ExplicitlyRecursive.Even(8);

        Assert.IsTrue(result);
      }


      [TestMethod]
      public void RecursionTest6()
      {
        var result = ExplicitlyRecursive.SubEven(8);

        Assert.AreEqual(0, result);
      }

      [TestMethod]
      public void RecursionTest7()
      {
        var result = ExplicitlyRecursive.SubEven(5);

        Assert.AreEqual(0, result);
      }

      [TestMethod]
      public void RecursionTest8()
      {
        var result = ExplicitlyRecursive.SubOdd(8);

        Assert.AreEqual(0, result);
      }

      [TestMethod]
      public void RecursionTest9()
      {
        var result = ExplicitlyRecursive.SubOdd(5);

        Assert.AreEqual(0, result);
      }

      [TestMethod]
      public void RecursionTest10()
      {
        var result = ExplicitlyRecursive.ThrowOnEven(5);

        Assert.IsFalse(result);
      }

      [TestMethod]
      [ExpectedException(typeof(ArgumentException))]
      public void RecursionTest11()
      {
        var result = ExplicitlyRecursive.ThrowOnEven(4);

        Assert.IsFalse(true);
      }

      [TestMethod]
      public void RecursionTest12()
      {
        var result = ExplicitlyRecursive.ThrowOnOdd(4);

        Assert.IsFalse(result);
      }

      [TestMethod]
      [ExpectedException(typeof(ArgumentException))]
      public void RecursionTest13()
      {
        var result = ExplicitlyRecursive.ThrowOnOdd(3);

        Assert.IsFalse(true);
      }

    }


    public class Program
    {
      public static void Main2(string[] args)
      {
        IList<string> items = new Foo().Items;
      }
    }

    public sealed class Foo
        : IFoo
    {
      private readonly IList<string> items;

      public IList<string> Items
      {
        get
        {
          return this.items;
        }
      }

      public Foo()
      {
        this.items = new List<string>();
      }

      public bool ContainsItem(string s)
      {
        return this.items.Contains(s);
      }
    }

    [ContractClass(typeof(IFooContract))]
    public interface IFoo
    {
      IList<string> Items
      {
        get;
      }

      bool ContainsItem(string s);
    }

    [ContractClassFor(typeof(IFoo))]
    public sealed class IFooContract
        : IFoo
    {
      private IFooContract()
      {

      }

      IList<string> IFoo.Items
      {
        get
        {
          Contract.Ensures(Contract.Result<IList<string>>() != null);
          Contract.Ensures(Contract.Result<IList<string>>().All(s => ((IFoo)this).ContainsItem(s)));

          throw new Exception();
        }
      }

      bool IFoo.ContainsItem(string s)
      {
        Contract.Ensures(Contract.Result<bool>() == ((IFoo)this).Items.Contains(s));

        throw new Exception();
      }
    }


  }

  namespace Multani
  {
    [TestClass]
    public class MultaniTestClass1
    {
      [TestMethod]
      public void PositiveMultani1()
      {
        double[] initialValues = new[] { 1.0, 2.0, 3.0 };
        double[] stDevs = new[] { 0.1, 0.2, 0.3 };
        double[] drifts = new[] { 0.1, 0.1, 0.1 };
        double[,] correlations = new double[3,3] { {0.1,0.1,0.1}, {0.1,0.1,0.1}, {0.1,0.1,0.1} };
        int randomSeed = 44;
        var c = new CorrelatedGeometricBrownianMotionFuelPriceSimulator(initialValues, stDevs, drifts, correlations, randomSeed);
      }
      [TestMethod]
      [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
      public void NegativeMultani1()
      {
        double[] initialValues = null;
        double[] stDevs = new[] { 0.1, -0.2, 0.3 };
        double[] drifts = null;
        double[,] correlations = null;
        int randomSeed = 44;
        var c = new CorrelatedGeometricBrownianMotionFuelPriceSimulator(initialValues, stDevs, drifts, correlations, randomSeed);
      }
    }
    public class CorrelatedGeometricBrownianMotionFuelPriceSimulator
    {
      public CorrelatedGeometricBrownianMotionFuelPriceSimulator(double[] initialValues, double[] stDevs, double[] drifts, double[,] correlations, int randomSeed)
      {
        Contract.Requires(Contract.ForAll(0, stDevs.Length, index => stDevs[index] >= 0));
      }

    }
  }

  namespace Somebody
  {
    public static class ContractedLINQMethods
    {

      #region GroupBy with posconditions
      //The following code shows how to put some posconditions to GroupBy method
      public static IEnumerable<IGrouping<K, T>> GroupBy<T, K>(this IEnumerable<T> source, Func<T, K> selector)
      {

        //This poscondition produces an unexpected error during injection of the code in the assembly (see attached image)
        Contract.Ensures(Contract.ForAll<T>(source, (x => source != null && Contract.Result<IEnumerable<IGrouping<K, T>>>().Any(y => y.Contains(x)))),
                         "Ensures that each item from the source belongs to some group");

        //When changing the above poscondition to the following one, then there is no error
        //Contract.Ensures(Contract.ForAll(source, (x => Enumerable.GroupBy(source, selector).Any(y => y.Contains(x)))),
        //                 "Ensures that each item from the source belongs to some group");

        //...Others GroupBy posconditions

        //GroupBy implementation
        return Enumerable.GroupBy(source, selector);

      }
      #endregion

      #region ...Other query methods
      //...
      #endregion
    }

    [TestClass]
    public class TestResourceStringUserMessage
    {
      public static string Message2 = "This is message2";
      public static string Message3 { get { return "This is message3"; } }

      public void RequiresWithInternalResourceMessage(string s)
      {
        Contract.Requires(s != null, Tests.Properties.Resources.UserMessage1);
      }

      public void RequiresWithPublicFieldMessage(string s)
      {
        Contract.Requires(s != null, Message2);
      }

      public void RequiresWithPublicGetterMessage(string s)
      {
        Contract.Requires(s != null, Message3);
      }

      [TestMethod]
      public void PositiveTestUserMessages()
      {
        RequiresWithInternalResourceMessage("hello");
        RequiresWithPublicFieldMessage("hello");
        RequiresWithPublicGetterMessage("hello");
      }

      [TestMethod]
      public void NegativeTestInternalUserMessageString()
      {
        try
        {
          RequiresWithInternalResourceMessage(null);
          throw new Exception();
        }
        catch (TestRewriterMethods.PreconditionException p)
        {
          // resource not as visible as the contract method
          Assert.AreEqual(null, p.User);
        }
      }

      [TestMethod]
      public void NegativeTestPublicFieldUserMessageString()
      {
        try
        {
          RequiresWithPublicFieldMessage(null);
          throw new Exception();
        }
        catch (TestRewriterMethods.PreconditionException p)
        {
          // resource not as visible as the contract method
          Assert.AreEqual(Message2, p.User);
        }
      }

      [TestMethod]
      public void NegativeTestPublicGetterUserMessageString()
      {
        try
        {
          RequiresWithPublicGetterMessage(null);
          throw new Exception();
        }
        catch (TestRewriterMethods.PreconditionException p)
        {
          // resource not as visible as the contract method
          Assert.AreEqual(Message3, p.User);
        }
      }

    }
  }

  namespace PDC
  {
    public class Invariants
    {
      public int Age { get; set;  }
      string name;

      [ContractInvariantMethod]
      private void ObjectInvariant()
      {
        Contract.Invariant(Age > 0);
        Contract.Invariant(name != null);
      }

      public Invariants(string name, int age)
      {
        this.Age = age;
        this.name = name;
      }
    }

    public class Invariants<T> where T:class
    {
      int age;
      public T Name { get; set; }
      public int X { get; set; }
      public int Y { get; set; }

      [ContractInvariantMethod]
      private void ObjectInvariant()
      {
        Contract.Invariant(Name != null);
        Contract.Invariant(age > 0);
        Contract.Invariant(X > Y);
      }

      public Invariants(T name, int age, int x, int y)
      {
        this.Name = name;
        this.age = age;
        this.Y = y;
        this.X = x;
      }

    }

    public interface IValid
    {
      bool IsValid { get; }
    }
    public class TrickyAutoPropInvariants<T> where T:IValid
    {
      public T Value { get; set; }
      public int Age { get; set; }
      public bool Flag { get; set; }

      public TrickyAutoPropInvariants(T value, int age, bool flag)
      {
        this.Value = value;
        this.Age = age;
        this.Flag = flag;
      }
      [ContractInvariantMethod]
      void ObjectInvariant()
      {
        Contract.Invariant(Flag ? Age > 0 : Age == 0 && Value.IsValid);
      }
      public void Change(bool flag, int age)
      {
        this.Flag = flag;
        this.Age = age;
      }
      public void LeaveObjectInconsistent()
      {
        throw new ApplicationException();
      }

      public void LeaveObjectConsistent()
      {
        try
        {
          LeaveObjectInconsistent();
        }
        catch (ApplicationException)
        {
        }
      }

      public void LeaveObjectConsistentViaAdvertisedException()
      {
        Contract.EnsuresOnThrow<ApplicationException>(true);
        throw new ApplicationException();
      }

    }
    class Valid : IValid {
      bool valid;
      public Valid(bool valid)
      {
        this.valid = valid;
      }
      public bool IsValid { get { return this.valid; } }
    }
    [TestClass]
    public class DontCheckInvariantsDuringConstructors
    {
      [TestMethod]
      public void PositiveTrickyAutoProp1()
      {
        var tricky = new TrickyAutoPropInvariants<Valid>(new Valid(true), 0, false);
        tricky.Change(true, 5);
      }
      [TestMethod]
      public void PositiveTrickyAutoProp2()
      {
        var tricky = new TrickyAutoPropInvariants<Valid>(new Valid(true), 0, false);
        try
        {
          tricky.LeaveObjectInconsistent();
        }
        catch (ApplicationException) { }
        // now we can violate invariant further without being punished
        tricky.Change(false, 5);
      }
      [TestMethod]
      public void PositiveTrickyAutoProp3()
      {
        var tricky = new TrickyAutoPropInvariants<Valid>(new Valid(true), 0, false);
        try
        {
          tricky.LeaveObjectInconsistent();
        }
        catch (ApplicationException) { }
        // now we can violate invariant further without being punished
        tricky.Age = 5;
      }
      [TestMethod]
      public void NegativeTrickyAutoProp1()
      {
        try
        {
          var tricky = new TrickyAutoPropInvariants<Valid>(new Valid(true), 0, false);
          tricky.Change(false, 5);
          throw new Exception();
        }
        catch (TestRewriterMethods.InvariantException i)
        {
          Assert.AreEqual("Flag ? Age > 0 : Age == 0 && Value.IsValid", i.Condition);
        }
      }
      [TestMethod]
      public void NegativeTrickyAutoProp2()
      {
        try
        {
          var tricky = new TrickyAutoPropInvariants<Valid>(new Valid(true), 0, false);
          tricky.Age = 5;
          throw new Exception();
        }
        catch (TestRewriterMethods.PreconditionException i)
        {
          Assert.AreEqual("Flag ? Age > 0 : Age == 0 && Value.IsValid", i.Condition);
        }
      }
      [TestMethod]
      public void NegativeTrickyAutoProp3()
      {
        try
        {
          var tricky = new TrickyAutoPropInvariants<Valid>(new Valid(true), 0, false);
          tricky.LeaveObjectConsistent();
          tricky.Age = 5; // should fail precondition
          throw new Exception();
        }
        catch (TestRewriterMethods.PreconditionException i)
        {
          Assert.AreEqual("Flag ? Age > 0 : Age == 0 && Value.IsValid", i.Condition);
        }
      }
      [TestMethod]
      public void NegativeTrickyAutoProp4()
      {
        try
        {
          var tricky = new TrickyAutoPropInvariants<Valid>(new Valid(true), 0, false);
          try
          {
            tricky.LeaveObjectConsistentViaAdvertisedException();
          }
          catch (ApplicationException) { }
          tricky.Age = 5; // should fail precondition
          throw new Exception();
        }
        catch (TestRewriterMethods.PreconditionException i)
        {
          Assert.AreEqual("Flag ? Age > 0 : Age == 0 && Value.IsValid", i.Condition);
        }
      }

      [TestMethod]
      public void PositiveInvariantOffDuringConstruction1()
      {
        var p = new Invariants("Joe", 42);

      }

 
      [TestMethod]
      public void NegativeInvariantOffDuringConstruction1()
      {
        try
        {
          var p = new Invariants(null, 1);
          throw new Exception();
        }
        catch (TestRewriterMethods.InvariantException i)
        {
          Assert.AreEqual("name != null", i.Condition);
        }
      }

      [TestMethod]
      public void PositiveInvariantOffDuringConstruction2()
      {
        var p = new Invariants<string>("Joe", 42, 2, 1);

      }


      [TestMethod]
      public void NegativeInvariantOffDuringConstruction2()
      {
        try
        {
          var p = new Invariants<string>("Joe", 0, 1, 2);
          throw new Exception();
        }
        catch (TestRewriterMethods.InvariantException i)
        {
          Assert.AreEqual("age > 0", i.Condition);
        }
      }

      [TestMethod]
      public void NegativeInvariantOfAutoPropIntoRequires1()
      {
        try
        {
          var p = new Invariants<string>(null, 0, 1, 2);
          throw new Exception();
        }
        catch (TestRewriterMethods.InvariantException p)
        {
          Assert.AreEqual("Name != null", p.Condition);
        }
      }

      [TestMethod]
      public void NegativeInvariantOfAutoPropIntoRequires2()
      {
        try
        {
          var p = new Invariants<string>("Joe", 0, 1, 2);
          throw new Exception();
        }
        catch (TestRewriterMethods.InvariantException p)
        {
          Assert.AreEqual("age > 0", p.Condition);
        }
      }

      [TestMethod]
      public void NegativeInvariantOfAutoPropIntoRequires3()
      {
        try
        {
          var p = new Invariants<string>("Joe", 1, 1, 2);
          throw new Exception();
        }
        catch (TestRewriterMethods.InvariantException p)
        {
          Assert.AreEqual("X > Y", p.Condition);
        }
      }

      [TestMethod]
      public void NegativeInvariantOfAutoPropIntoRequires4()
      {
        try
        {
          var p = new Invariants<string>("Joe", 1, 2, 1);
          p.Name = null;
          throw new Exception();
        }
        catch (TestRewriterMethods.PreconditionException p)
        {
          Assert.AreEqual("Name != null", p.Condition);
        }
      }

      [TestMethod]
      public void NegativeInvariantOfAutoPropIntoRequires5()
      {
        try
        {
          var p = new Invariants<string>("Joe", 1, 2, 1);
          p.X = 1;
          throw new Exception();
        }
        catch (TestRewriterMethods.PreconditionException p)
        {
          Assert.AreEqual("X > Y", p.Condition);
        }
      }

      [TestMethod]
      public void NegativeInvariantOfAutoPropIntoRequires6()
      {
        try
        {
          var p = new Invariants<string>("Joe", 1, 2, 1);
          p.Y = 2;
          throw new Exception();
        }
        catch (TestRewriterMethods.PreconditionException p)
        {
          Assert.AreEqual("X > Y", p.Condition);
        }
      }

    }
  }

  namespace Arnott {
    [TestClass]
    public class C {
      public void M(object x) {
        Contract.Requires<ArgumentNullException>(x != null || x != null, "x");
      }

      [TestMethod]
      public void PositiveRequiresWithException() {
        M(this);
      }
      [TestMethod]
      [ExpectedException(typeof(ArgumentNullException))]
      public void NegativeRequiresWithException() {
        M(null);
      }

      public void OkayOrder(int[] args) {
        Contract.Requires<ArgumentOutOfRangeException>(args.Length > 10, "args");
        if (args == null) throw new Exception();
        Contract.EndContractBlock();
      }
      public void BadOrder(int[] args) {
        if (args == null) throw new Exception();
        Contract.Requires<ArgumentOutOfRangeException>(args.Length > 10, "args");
        Contract.EndContractBlock();
      }

      [TestMethod]
      public void WorksCorrectlyWithThisOrder() {
        OkayOrder(new int[20]);
      }
      [TestMethod]
      [ExpectedException(typeof(ArgumentOutOfRangeException))]
      public void WorksInCorrectlyWithThisOrder() {
        BadOrder(new int[10]);
      }

    }

  }
}