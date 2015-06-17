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
using Contracts.Regression;
using System.Diagnostics.CodeAnalysis;

[assembly:RegressionOutcome(@"Method 'UserFeedback.AndrewArnott.IFaceImpl.Method(System.Object)' implements interface method 'UserFeedback.AndrewArnott.IFace.Method(System.Object)', thus cannot add Requires.")]
//[assembly:RegressionOutcome("Detected call to method 'UserFeedback.MaF.CheckStructInvariant.PropIsTrue' without [Pure] in contracts of method 'UserFeedback.MaF.CheckStructInvariant.ObjectInvariant'.")]

namespace UserFeedback
{
  namespace AndrewArnott
  {
    [ContractClass(typeof(IFaceContract))]
    interface IFace
    {
      void Method(object param);
    }

    [ContractClassFor(typeof(IFace))]
    abstract class IFaceContract : IFace
    {
      #region IFace Members

      void IFace.Method(object param)
      {
        Contract.Requires<ArgumentNullException>(param != null);
      }

      #endregion
    }

    class IFaceImpl : IFace
    {
      #region IFace Members

      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 19, MethodILOffset = 0)]
      public void Method(object param)
      {
        Contract.Requires<ArgumentNullException>(param != null);

        Contract.Assert(param != null);
      }

      #endregion
    }

  }

  namespace AlexeyR
  {
    class AlexeyR1
    {
      Dictionary<string, object> _dict = new Dictionary<string, object>();

      void AddItemToDict(string key, string value)
      {
        Contract.Requires(!_dict.ContainsKey(key));
        _dict.Add(key, value);

        // do something with a newly added item
      }

      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 15, MethodILOffset = 17)]
      void ProcessItem(string key, string value)
      {
        if (!_dict.ContainsKey(key))
        {
          AddItemToDict(key, value);
          return;
        }
      }

      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "requires unproven: !_dict.ContainsKey(key)", PrimaryILOffset = 15, MethodILOffset = 3)]
      void ProcessItemBad(string key, string value)
      {
        AddItemToDict(key, value);
      }
    }
  }

  namespace Mathias
  {
    class Mathias
    {
      [ClousotRegressionTest]
      void Test(int[] array, int index)
      {
        Contract.Requires(array[index] == 0);

      }

      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "assert unproven", PrimaryILOffset = 19, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 6, MethodILOffset = 8)]
      void Test2(int[] array, int index)
      {
        if (array[index] == 0)
        {
          Test(array, index);

          Contract.Assert(array[index] == 0);
        }
      }

      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "assert unproven", PrimaryILOffset = 15, MethodILOffset = 0)]
      void Test3(int[] array, int index)
      {
        if (array[index] == 0)
        {
          array[5] = 5;

          Contract.Assert(array[index] == 0);
        }
      }
    }
  }

  namespace DevInstinct
  {
    class DevInstinct
    {
      public static void TestIsNullOrEmptyCollection(System.Collections.ICollection collection)
      {
        Contract.Requires(collection == null || collection.Count == 0);
      }

      [ClousotRegressionTest]
      // http://social.msdn.microsoft.com/Forums/en-US/codecontracts/thread/a1b922cb-fd7d-4648-afb2-b68c5e3fff4d
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 15, MethodILOffset = 1)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 15, MethodILOffset = 12)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 15, MethodILOffset = 22)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 15, MethodILOffset = 49)]
      public static void Test()
      {
        TestIsNullOrEmptyCollection(null);                // ok
        TestIsNullOrEmptyCollection(new int[] { });       // ok
        TestIsNullOrEmptyCollection(new List<int>() { }); // ok

        System.Collections.ICollection iCol = new int[] { } as System.Collections.ICollection;
        Contract.Assume(iCol.Count == 0);
        TestIsNullOrEmptyCollection(iCol);
      }
    }

    class LazyInit
    {
      string _name;
      public string Name
      {
        [ClousotRegressionTest]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 24, MethodILOffset = 50)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 24, MethodILOffset = 43)]
        //[RegressionOutcome(Outcome = ProofOutcome.True, Message = @"invariant is valid", PrimaryILOffset = 25, MethodILOffset = 50)]
        //[RegressionOutcome(Outcome = ProofOutcome.True, Message = @"invariant is valid", PrimaryILOffset = 25, MethodILOffset = 43)]
        get
        {
          Contract.Ensures(!IsInitialized || !String.IsNullOrEmpty(Contract.Result<string>()));

          if (_initialized) return _name;
          else
          {
            return _name;
          }
        }
        [ClousotRegressionTest]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"invariant is valid", PrimaryILOffset = 25, MethodILOffset = 21)]
        set
        {
          Contract.Requires(!String.IsNullOrEmpty(value));
          _name = value;
        }
      }
      public LazyInit() { }

      bool _initialized;
      public bool IsInitialized
      {
        [ClousotRegressionTest]
        //[RegressionOutcome(Outcome = ProofOutcome.True, Message = @"invariant is valid", PrimaryILOffset = 25, MethodILOffset = 6)]
        get { return _initialized; }
      }

      [ContractInvariantMethod]
      void ObjectInvariant()
      {
        Contract.Invariant(!_initialized || !String.IsNullOrEmpty(_name));
      }

      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 25, MethodILOffset = 38)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 53, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 75, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"invariant is valid", PrimaryILOffset = 25, MethodILOffset = 38)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 25, MethodILOffset = 106)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"invariant is valid", PrimaryILOffset = 25, MethodILOffset = 106)]
      [SuppressMessage("Microsoft.Contracts", "Assert-94-0")]
      public void FinalizeInit()
      {
        Contract.Requires(!string.IsNullOrEmpty(Name));
        Contract.Ensures(IsInitialized);

        if (_initialized)
        {
          return;
        }

        Contract.Assert(!String.IsNullOrEmpty(Name));
        Contract.Assert(Name == _name);
        Contract.Assert(!string.IsNullOrEmpty(_name));
        _initialized = true;
      }

    }

    public class TestLazy
    {
      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 9, MethodILOffset = 30)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 14, MethodILOffset = 36)]
      public static void M()
      {
        var l = new LazyInit();
        Contract.Assume(!String.IsNullOrEmpty("Hello"));
        l.Name = "Hello";
        l.FinalizeInit();
      }
    }
  }

  namespace ManfredSteyer
  {
    public class Account
    {
      [ContractPublicPropertyName("Value")]
      protected int value;

      public int Value
      {
        [ClousotRegressionTest]
        get { return value; }
      }

      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 20, MethodILOffset = 58)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 44, MethodILOffset = 58)]
      public virtual int calcInterest()
      {
        Contract.Ensures(Contract.Result<int>() >= 0 || value < 0);
        Contract.Ensures(value == Contract.OldValue(value));

        return value / 50;
      }

      [ClousotRegressionTest]
      public virtual void Deposit(int amount)
      {
        Contract.Requires(amount > 0);

        value += amount;
      }


      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 30, MethodILOffset = 49)]
      public virtual void Withdraw(int amount)
      {
        Contract.Requires(amount > 0);
        Contract.Ensures(value == Contract.OldValue(value) - amount);

        value -= amount;
      }
    }

    public class SavingsAccount : Account
    {
      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 4, MethodILOffset = 17)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 30, MethodILOffset = 22)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"invariant is valid", PrimaryILOffset = 12, MethodILOffset = 22)]
      public override void Withdraw(int amount)
      {
        if (value < amount) throw new ApplicationException();
        base.Withdraw(amount);
      }
      [ContractInvariantMethod()]
      void InvariantMethod()
      {
        Contract.Invariant(value >= 0);
      }

      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 20, MethodILOffset = 22)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 44, MethodILOffset = 22)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 11, MethodILOffset = 22)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"invariant is valid", PrimaryILOffset = 12, MethodILOffset = 22)]
      public override int calcInterest()
      {
        Contract.Ensures(Contract.Result<int>() >= 0);
        return base.calcInterest();
      }
    }
  }

  namespace Damden
  {
    public class BadReversedCondition
    {
      public long Wakeup
      {
        get
        {
          return _Wakeup;
        }
        protected set
        {
          #region require: At least 25 ms or 0 ms
          if (value < 25 && value != 0)
          {
            throw new InvalidOperationException("The wakeup mechanisme is not suited for smaller periods than 25 ms.");
          }
          Contract.EndContractBlock();
          #endregion
          _Wakeup = value;
        }
      }
      private long _Wakeup;


      public static void Test()
      {
        var b = new BadReversedCondition();
        b.Wakeup = 1234;
      }
    }
  }

  namespace Joost
  {
    class Rationaal
    {
      int Noemer { get; set; }
      int Deler { get; set; }

      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "ensures unproven: Noemer == noemer", PrimaryILOffset = 43, MethodILOffset = 76)]
      [RegressionOutcome(Outcome = ProofOutcome.False, Message = "ensures is false: Deler == deler", PrimaryILOffset = 57, MethodILOffset = 76)]
      public Rationaal(int noemer, int deler)
      {
        Contract.Requires(noemer > 0, "noemer must be positive.");
        Contract.Requires(deler > 0, "deler must be positive.");
        Contract.Ensures(Noemer == noemer);
        Contract.Ensures(Deler == deler); // not satisfied!

        Noemer = noemer;
        Noemer = deler; // typo
      }
    }
  }

  namespace Sgro
  {

    [ContractClass(typeof(ITestClassWithInterfaceContract))]
    interface ITestClassWithInterface
    {
      string Name { get; set; }
    }

    [ContractClassFor(typeof(ITestClassWithInterface))]
    abstract class ITestClassWithInterfaceContract : ITestClassWithInterface
    {
      #region ITestClassWithInterface Members

      string ITestClassWithInterface.Name
      {
        get
        {
          Contract.Ensures(!String.IsNullOrEmpty(Contract.Result<string>()));

          throw new NotImplementedException();
        }
        set
        {
          Contract.Requires(!String.IsNullOrEmpty(value));

          throw new NotImplementedException();
        }
      }
      #endregion
    }

    class TestWithInterface : ITestClassWithInterface
    {
      [ContractInvariantMethod]
      void ObjectInvariant()
      {
        Contract.Invariant(!string.IsNullOrEmpty(this._name));
      }


      #region ITestClassWithInterface Members

      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.False, Message = "invariant is false: !string.IsNullOrEmpty(this._name)", PrimaryILOffset = 14, MethodILOffset = 6)]
      public TestWithInterface()
      {
        // buggy because invariant not established
      }

      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"invariant is valid", PrimaryILOffset = 14, MethodILOffset = 27)]
      public TestWithInterface(string initialName)
      {
        Contract.Requires(!String.IsNullOrEmpty(initialName));
        this._name = initialName;
      }

      private string _name;
      public string Name
      {
        [ClousotRegressionTest]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 13, MethodILOffset = 6)]
        // [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"invariant is valid", PrimaryILOffset = 14, MethodILOffset = 6)]
        get
        {
          return this._name;
        }
        [ClousotRegressionTest]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"invariant is valid", PrimaryILOffset = 14, MethodILOffset = 7)]
        set
        {
          this._name = value;
        }
      }

      #endregion
    }
    class TestSimple
    {
      [ContractInvariantMethod]
      void ObjectInvariant()
      {
        Contract.Invariant(!string.IsNullOrEmpty(this._name));
      }


      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.False, Message = "invariant is false: !string.IsNullOrEmpty(this._name)", PrimaryILOffset = 14, MethodILOffset = 6)]
      public TestSimple()
      {
        // buggy because invariant is not established
      }

      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"invariant is valid", PrimaryILOffset = 14, MethodILOffset = 27)]
      public TestSimple(string initialName)
      {
        Contract.Requires(!String.IsNullOrEmpty(initialName));
        this._name = initialName;
      }

      private string _name;
      public string Name
      {
        [ClousotRegressionTest]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 13, MethodILOffset = 24)]
        // [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"invariant is valid", PrimaryILOffset = 14, MethodILOffset = 24)]
        get
        {
          Contract.Ensures(!String.IsNullOrEmpty(Contract.Result<string>()));

          return this._name;
        }
        [ClousotRegressionTest]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"invariant is valid", PrimaryILOffset = 14, MethodILOffset = 21)]
        set
        {
          Contract.Requires(!String.IsNullOrEmpty(value));

          this._name = value;
        }
      }

    }

  }

  namespace Pelmens
  {
    class SomeClass
    {
      private int? number;

      private int? Prop { get; set; }

      [ClousotRegressionTest]
      public SomeClass(int? value) { number = value; }

      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 12, MethodILOffset = 19)]
      public int SomeMethod()
      {
        if (number.HasValue)
        {
          return number.Value;
        }

        return 0;
      }

      [ClousotRegressionTest]
      // We are not smart enough here. The ldflda accessing number causes us to havoc Prop
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 12, MethodILOffset = 38)]
      public int SomeMethod2()
      {
        if (Prop.HasValue)
        {
          if (number.HasValue)
          {
            return Prop.Value;
          }
        }
        return 0;
      }

      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 12, MethodILOffset = 11)]
      public int SomeMethod(int? num)
      {
        if (num.HasValue)
        {
          return num.Value;
        }

        return 0;
      }
    }

  }
  namespace JonSkeet
  {
    class Arithmetic
    {
      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 13, MethodILOffset = 9)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 13, MethodILOffset = 18)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 81, MethodILOffset = 0)]
      void Test()
      {
        Random rng = new Random();
        int first = rng.Next(1, 7), second = rng.Next(1, 7);
        Contract.Assume(first >= 1 && first <= 6);
        Contract.Assume(second >= 1 && second <= 6);
        int sum = first + second;
        Contract.Assert(sum >= 2 && sum <= 12);
      }
    }
  }

  namespace MaF
  {
    class CheckInvariants
    {
      [ContractInvariantMethod]
      void ObjectInvariant()
      {
        //Contract.Assume(false);
      }

    }
    [SuppressMessage("Microsoft.Contracts", "CC1036")]
    struct CheckStructInvariant
    {
      [ContractInvariantMethod]
      void ObjectInvariant()
      {
        Contract.Invariant(PropIsTrue());
      }

      public bool PropIsTrue() { return true; }

    }

    class Disjuncts
    {
      [Pure]
      static bool DoIt(int x)
      {
        if (x < -100) return false;
        if (x > 100) return false;
        return true;
      }

      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 56, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 37, MethodILOffset = 86)]
      public static int M(int x)
      {
        Contract.Requires(x < -5 || x > 5);
        Contract.Ensures(Contract.Result<int>() < -5 || Contract.Result<int>() > 5);

        // .. paths split

        while (DoIt(x))
        {
          // .. no path splitting

          Contract.Assert(x < -5 || x > 5);
          // .. paths split
          if (x < 0)
          {
            x--;
          }
          else
          {
            x++;
          }
          // .. paths split
        }

        // ... paths split 
        return x;
      }


      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 20, MethodILOffset = 53)]
      public static int M(bool y)
      {
        Contract.Ensures(Contract.Result<int>() < -5 || Contract.Result<int>() > 5);

        var x = 0;

        // ... no path splitting 
        while (x >= -10 && x <= 10) // loop exit condition establishes post state
        {
          // no path splitting
          if (y)
          {
            x--;
          }
          else
          {
            x++;
          }
          // no path splitting
        }

        // ... paths split according to < -5 || > 5

        return x;
      }

      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 49, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 63, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 93, MethodILOffset = 0)]
      public static int SplittingBetter(int x, int y)
      {
        Contract.Ensures(x < 0 && Contract.Result<int>() == x - y ||
                         x >= 0 && Contract.Result<int>() == x + y);
        var oldx = x;
        //while (y > 0)
        {
          if (x < 0)
          {
            Contract.Assert(oldx < 0);
          }
          else
          {
            Contract.Assert(oldx >= 0);
          }
          Contract.Assert((x >= 0 || oldx < 0) && (x < 0 || oldx >= 0));
          Contract.Assume(false);
          Contract.Assert(oldx < 0 && x == oldx - y || oldx >= 0 && x == oldx + y);
          if (x < 0)
          {
            x--;
          }
          else
          {
            x++;
          }
          y--;
        }

        return x;
      }
    }
  }
  namespace Onurg
  {
    public class Foo : IFoo
    {
      IEnumerable<T> IFoo.Bar<T>()
      {
        return null;
      }
    }

    [ContractClass(typeof(FooContract))]
    public interface IFoo
    {
      IEnumerable<T> Bar<T>();
    }

    [ContractClassFor(typeof(IFoo))]
    abstract class FooContract : IFoo
    {
      IEnumerable<T> IFoo.Bar<T>()
      {
        Contract.Ensures(Contract.Result<IEnumerable<T>>() != null);
        Contract.Ensures(Contract.ForAll(Contract.Result<IEnumerable<T>>(), x => x != null));
        return default(IEnumerable<T>);
      }
    }

  }

  namespace MatthijsWoord
  {
  }


  namespace Strilanc
  {
    public interface B
    {
      bool P { get; }
    }

    #region I contract binding
    [ContractClass(typeof(CI<>))]
    public partial interface I<T> : B
    {
      void S();
    }

    [ContractClassFor(typeof(I<>))]
    abstract class CI<T> : I<T>
    {
      #region I Members

      public void S()
      {
        Contract.Requires(this.P);
      }
      #endregion

      #region B Members

      public bool P
      {
        get { return true; }
      }

      #endregion
    }
    #endregion

    public class C : I<bool>
    {
      public bool P { get { return true; } }

      public void S() { }

    }
  }

  namespace Jeannin
  {
    class MyList<T>
    {
      List<T> list = new List<T>();

      [ContractInvariantMethod]
      void ObjectInvariant()
      {
        Contract.Invariant(list != null);
      }

      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 12, MethodILOffset = 32)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 11, MethodILOffset = 32)]
      public IEnumerator<T> GetEnumerator()
      {
        Contract.Ensures(Contract.Result<IEnumerator<T>>() != null);

        return this.list.GetEnumerator();
      }
    }
  }

  namespace Sexton
  {
    class ProgramToTestReadonly
    {

      static void ReadonlyTest()
      {
        const string theName = "TheName";
        var program = Factory(theName);
        Contract.Assert(program.Name == theName);
      }

      public string Name
      {
        get
        {
          Contract.Ensures(Contract.Result<string>() == name);
          return name;
        }
      }

      private readonly string name;

      private ProgramToTestReadonly(string name)
      {
        Contract.Ensures(this.name == name);

        this.name = name;
      }


      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 45, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 62, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "assert unproven", PrimaryILOffset = 79, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 16, MethodILOffset = 85)]
      public static ProgramToTestReadonly Factory(string name)
      {
        Contract.Ensures(Contract.Result<ProgramToTestReadonly>().Name == name);

        var p =  new ProgramToTestReadonly(name);
        Contract.Assert(p.Name == p.name);
        Contract.Assert(p.name == name);
        Contract.Assert(p.Name == name);
        return p;

      }

    }
  }
}
