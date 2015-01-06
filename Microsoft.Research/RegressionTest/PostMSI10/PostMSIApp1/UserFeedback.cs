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

    static class Program
    {
      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 12, MethodILOffset = 17)]
      static void Main2()
      {
        string s = GetString() + "suffix";
        RequiresNonEmplyString(s);
      }

      static string GetString()
      {
        return null;
      }

      static void RequiresNonEmplyString(string arg)
      {
        Contract.Requires(arg.Length != 0);
        //Contract.Requires(arg.Length > 0);   
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

      private void TestCatch()
      {
        int x = 0;
        try
        {
          Console.WriteLine("");
          x = 1;
        }
        catch
        {
          if (x != 0) throw;
        }
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

  namespace MAF
  {
    public class TestV4Contracts
    {
      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 21, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 35, MethodILOffset = 0)]
      public static void M(string s)
      {
        Contract.Requires(!string.IsNullOrWhiteSpace(s));

        Contract.Assert(s != null);
        Contract.Assert(s.Length > 0);
      }
    }

    class Helper
    {
      [ContractAbbreviator]
      public static void EnsureNotNull<T>()
      {
        Contract.Ensures(Contract.Result<T>() != null);

      }
    }
    public class TestAbbreviations
    {
      public int X { get; set; }
      public int Y { get; set; }
      public int Z { get; set; }

      [ContractAbbreviator]
      void AdvertiseUnchanged()
      {
        Contract.Ensures(this.X == Contract.OldValue(this.X));
        Contract.Ensures(this.Y == Contract.OldValue(this.Y));
        Contract.Ensures(this.Z == Contract.OldValue(this.Z));
      }

      [ClousotRegressionTest]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="ensures is valid",PrimaryILOffset=19,MethodILOffset=6)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="ensures is valid",PrimaryILOffset=43,MethodILOffset=6)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="ensures is valid",PrimaryILOffset=67,MethodILOffset=6)]

      public void Work1()
      {
        AdvertiseUnchanged();
      }

      [ClousotRegressionTest]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="ensures is valid",PrimaryILOffset=19,MethodILOffset=18)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="ensures is valid",PrimaryILOffset=43,MethodILOffset=18)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="ensures is valid",PrimaryILOffset=67,MethodILOffset=18)]
      public void Work2()
      {
        AdvertiseUnchanged();

        X = X;
      }

      [ClousotRegressionTest]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="ensures is valid",PrimaryILOffset=19,MethodILOffset=12)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="ensures is valid",PrimaryILOffset=43,MethodILOffset=12)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="ensures is valid",PrimaryILOffset=67,MethodILOffset=12)]
      public void Work3()
      {
        AdvertiseUnchanged();

        Work2();
      }

      [ClousotRegressionTest]
      [RegressionOutcome(Outcome=ProofOutcome.False,Message="ensures is false: this.X == Contract.OldValue(this.X)",PrimaryILOffset=19,MethodILOffset=20)]
      [RegressionOutcome(Outcome=ProofOutcome.Bottom,Message="ensures unreachable",PrimaryILOffset=43,MethodILOffset=20)]
      [RegressionOutcome(Outcome=ProofOutcome.Bottom,Message="ensures unreachable",PrimaryILOffset=67,MethodILOffset=20)]
      public void Work4()
      {
        AdvertiseUnchanged();

        X++;
      }

      [ClousotRegressionTest]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="ensures is valid",PrimaryILOffset=16,MethodILOffset=10)]
      public string GetTheData0()
      {
        Helper.EnsureNotNull<string>();

        return "";
      }

      [ClousotRegressionTest]
      [RegressionOutcome(Outcome=ProofOutcome.False,Message="ensures is false: Contract.Result<T>() != null",PrimaryILOffset=16,MethodILOffset=6)]
      public string GetTheData1()
      {
        Helper.EnsureNotNull<string>();

        return null;
      }

      [ClousotRegressionTest]
      public string GetTheData2()
      {
        //Helper.EnsureNotNull<float>();

        return null;
      }

    }
  }



  namespace Strilanc
  {
    public interface B
    {
      bool P { get; }
    }

    #region I contract binding
    [ContractClass(typeof(IContract))]
    public partial interface I : B
    {
      void S();
    }

    [ContractClassFor(typeof(I))]
    abstract class IContract : I
    {
      #region I Members

      void I.S()
      {
        Contract.Requires(((B)this).P);
      }
      #endregion

      #region B Members

      bool B.P
      {
        get { return true; }
      }

      #endregion
    }
    #endregion



  }

  namespace Jeannin
  {
    class RandomTest
    {
      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 13, MethodILOffset = 55)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 15, MethodILOffset = 60)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 35, MethodILOffset = 60)]
      void M()
      {
        var random = new Random();
        List<string> list = new List<string> { "One", "Two", "Three" };
        Console.WriteLine(list[random.Next(list.Count)]);
      }
    }
  }
  namespace Sexton
  {
    class Test
    {
      void Foo(object bar = null)
      {
      }
    }

  }

  namespace Vandermotten
  {
    class StringImmutability
    {
      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 13, MethodILOffset = 33)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 33, MethodILOffset = 33)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 7, MethodILOffset = 49)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 21, MethodILOffset = 49)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 44, MethodILOffset = 49)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 13, MethodILOffset = 63)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 33, MethodILOffset = 63)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 72, MethodILOffset = 0)]
      public void MethodA(string input)
      {
        Contract.Requires(input != null);
        string extended = input + " ";
        Contract.Assume(extended[extended.Length - 1] == ' ');
        MethodB(extended);
        // Contract.Assert(extended.Length > 0);
        Contract.Assert(extended[extended.Length - 1] == ' ');
      }
      public void MethodB(string input)
      {
        Contract.Requires(input != null);
        Contract.Requires(input.Length > 0);
        Contract.Requires(input[input.Length - 1] == ' ');
        ////Contract.Ensures(input[input.Length - 1] == ' '); // this line can prove the assert in MethodA ?!?
      }
    }
  }

  namespace Connect
  {
    class TestWhiteSpace
    {
      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 9, MethodILOffset = 16)]
      void Test(string someString)
      {
        Contract.Assume(!string.IsNullOrWhiteSpace(someString));
        Call(someString);
      }
      private void Call(string someString)
      {
        Contract.Requires(!string.IsNullOrWhiteSpace(someString));

      }
    }

    class TestGuids
    {
      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 17, MethodILOffset = 0)]
      void Test()
      {
        var guid = Guid.NewGuid();
        Contract.Assert(guid != Guid.Empty);
      }
    }
  }
  namespace EricZeitler
  {
    class C
    {
      [ContractInvariantMethod]
      private void ObjectInvariants()
      {
        Contract.Invariant(_x.Count == Count);
      }

      readonly IList<object> _x = new List<object>();

      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 60, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 19, MethodILOffset = 65)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 36, MethodILOffset = 65)]
      C()
      {
        Contract.Ensures(_x.Count == Count);
        Contract.Assert(_x.Count == Count);
      }

      int Count
      {
        [ClousotRegressionTest]
        get { return _x.Count; }
      }

      [ClousotRegressionTest]
      event EventHandler MyEvent;
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
        public void Dispose()
        {
            _b = null;
        }
    }

}

namespace BenLerner
{
  using State = UInt32;

  public class Selector
  {
  }
  public class NFA
  {
    public static Selector noSelectorNeeded;
  }
  public enum TransitionType
  {
    ElemTest
  }
  public class ManyForAlls
  {
    public ManyForAlls(Dictionary<State, Dictionary<TransitionType, Dictionary<Selector, HashSet<State>>>> initial)
    {
      this.deltaTable = initial;
    }

    public void DoIt()
    {
    }

    internal Dictionary<State, Dictionary<TransitionType, Dictionary<Selector, HashSet<State>>>> deltaTable;

#if false
    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      //Contract.Invariant(deltaTable != null);
      Contract.Invariant(
        Contract.ForAll(deltaTable, (state) => (
          Contract.ForAll(state.Value, (type) => (
            (type.Key == TransitionType.ElemTest && Contract.ForAll(type.Value, (selector) => (selector.Key == NFA.noSelectorNeeded))) ||
            (type.Key != TransitionType.ElemTest && Contract.ForAll(type.Value, (selector) => (selector.Key != NFA.noSelectorNeeded))))))));
    }

#else
    [ContractInvariantMethod]
    private void ObjectInvariant2()
    {
      Contract.Invariant(deltaTable != null);
      Contract.Invariant(
        Contract.ForAll(deltaTable, (state) => (
          Contract.ForAll(state.Value, (type) => (
            Contract.ForAll(type.Value, (selector) => ((type.Key == TransitionType.ElemTest && selector.Key == NFA.noSelectorNeeded) ||
              (type.Key != TransitionType.ElemTest && selector.Key != NFA.noSelectorNeeded))))))));
    }
#endif
  }
}


namespace GabrielLozano
{
  [ContractClass(typeof(RepositoryFactoryContract<>))]
  public interface IRepositoryFactory<out T> where T : class
  {

    T CreateRepository();

  }



  [ContractClassFor(typeof(IRepositoryFactory<>))]

  public abstract class RepositoryFactoryContract<T> : IRepositoryFactory<T> where T : class
  {

    #region IRepositoryFactory<T> Members



    public T CreateRepository()
    {

      Contract.Ensures(Contract.Result<T>() != null);

      return default(T);

    }



    #endregion
  }

}

namespace System.Diagnostics.Contracts {
  [AttributeUsage(AttributeTargets.Method)]
  public sealed class ContractAbbreviatorAttribute : Attribute
  {
  }
}
