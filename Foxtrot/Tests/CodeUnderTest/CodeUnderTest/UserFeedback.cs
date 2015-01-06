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
using System.Diagnostics.Contracts;
using System.Linq;

namespace CodeUnderTest {
  namespace Peli {
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    public class UnitTest1 {
      public class Bar {
        public Bar(int i)
        {
          this.ID = i;
        }
        public int ID { get; set; }
      }

      [ContractClass(typeof(CFoo))]
      public interface IFoo {
        Bar GetValue(int i);

        void TestException(Bar b, int i);
      }

      [ContractClassFor(typeof(IFoo))]
      abstract class CFoo : IFoo {
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

      public class Foo : IFoo {
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

    }


    public static class StringExtensions {
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

  namespace WinSharp {
    using System.Linq;

    public class MyDict<TKey, TValue> : IDictionary<TKey, TValue> {
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

    public class ExplicitlyRecursive {
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




    public class Program {
      public static void Main2(string[] args)
      {
        IList<string> items = new Foo().Items;
      }
    }

    public sealed class Foo
        : IFoo {
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
    public interface IFoo {
      IList<string> Items
      {
        get;
      }

      [Pure]
      bool ContainsItem(string s);
    }

    [ContractClassFor(typeof(IFoo))]
    abstract class IFooContract
        : IFoo {
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

  namespace Multani {
    public class CorrelatedGeometricBrownianMotionFuelPriceSimulator {
      public CorrelatedGeometricBrownianMotionFuelPriceSimulator(double[] initialValues, double[] stDevs, double[] drifts, double[,] correlations, int randomSeed)
      {
        Contract.Requires(Contract.ForAll(0, stDevs.Length, index => stDevs[index] >= 0));
      }

    }
  }

  namespace Somebody {
    public static class ContractedLINQMethods {

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

    public class TestResourceStringUserMessage {
      public static string Message2 = "This is message2";
      public static string Message3 { get { return "This is message3"; } }

      public void RequiresWithInternalResourceMessage(string s)
      {
        Contract.Requires(s != null, CodeUnderTest.Properties.Resources.UserMessage1);
        //Contract.Requires(s != null, null);
      }

      public void RequiresWithPublicFieldMessage(string s)
      {
        Contract.Requires(s != null, Message2);
      }

      public void RequiresWithPublicGetterMessage(string s)
      {
        Contract.Requires(s != null, Message3);
      }

      public void LegacyRequiresReferencingPrivateStuffInThrow(TypeDescriptorPermissionFlags type)
      {
        if ((type & ~TypeDescriptorPermissionFlags.RestrictedRegistrationAccess) != 0)
          throw new ArgumentException(String.Format(SR.GetString(SR.Arg_EnumIllegalVal), (int)type));
        Contract.EndContractBlock();

      }

     }

    static class SR
    {
      public static string GetString(string key)
      {
        return "Illegal value {0}";
      }

      public static string Arg_EnumIllegalVal = "unimportant";
    }

    [Flags]
    public enum TypeDescriptorPermissionFlags
    {
      RestrictedRegistrationAccess = 1,
      Other = 2
    }
  }

  namespace PDC {
    public class Invariants {
      public int Age { get; set; }
      string name;

      [ContractInvariantMethod]
      private void ObjectInvariant()
      {
        Contract.Invariant(Age > 0, "Age positive");
        Contract.Invariant(name != null);
      }

      public Invariants(string name, int age)
      {
        this.Age = age;
        this.name = name;
      }
    }

    public class Invariants<T> where T : class {
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

    public interface IValid {
      bool IsValid { get; }
    }
    public class TrickyAutoPropInvariants<T> where T : IValid {
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
        Contract.Invariant(Flag ? Age > 0 : Age == 0 && Value.IsValid, "Tricky");
      }
      public void Change(bool flag, int age)
      {
        this.Flag = flag;
        this.Age = age;
      }
      public void LeaveObjectInconsistent()
      {
        this.Flag = true;
        this.Age = 0;
        throw new ApplicationException();
      }

      public void LeaveObjectConsistentWrong()
      {
        try
        {
          LeaveObjectInconsistent();
        }
        catch (ApplicationException)
        {
        }
      }

      public void LeaveObjectConsistentOK() {
        try {
          LeaveObjectInconsistent();
        } catch (ApplicationException) {
          this.Age = 1;
        }
      }

      public void LeaveObjectConsistentViaAdvertisedException()
      {
        Contract.EnsuresOnThrow<ApplicationException>(true);
        throw new ApplicationException();
      }

    }
    public class Valid : IValid {
      bool valid;
      public Valid(bool valid)
      {
        this.valid = valid;
      }
      public bool IsValid { get { return this.valid; } }
    }
  }

  namespace Arnott {
    public class C {
      public void M(object x)
      {
#if LEGACY
        if (!(x != null || x != null)) throw new ArgumentNullException("x");
        Contract.EndContractBlock();
#else
        Contract.Requires<ArgumentNullException>(x != null || x != null, "x");
#endif
      }

      public void OkayOrder(int[] args)
      {
#if LEGACY
        if (args.Length <= 10) throw new ArgumentOutOfRangeException("args", "args.Length > 10");
        if (args == null) throw new Exception();
        if (!CheckSimpleFun(args.Length >= 1000)) throw new Exception(); // cover extra code paths in decompiler
        Contract.EndContractBlock();
#else
        Contract.Requires<ArgumentOutOfRangeException>(args.Length > 10, "args");
        Contract.Requires<Exception>(args != null);
        Contract.Requires<Exception>(CheckSimpleFun(args.Length >= 1000));
#endif
      }
      public void BadOrder(int[] args)
      {
#if LEGACY
        if (args == null) throw new Exception();
        //Contract.Requires<ArgumentOutOfRangeException>(args.Length > 10, "args");
        if (args.Length <= 10) throw new ArgumentOutOfRangeException("args", "args.Length > 10");
        if (args.Length >= 1000) throw new Exception(); // cover some extra code paths in decompiler
        if ((args.Length >= 1000)) throw new Exception(); // cover some extra code paths in decompiler
        if ((args.Length >= 1000) || (args.Length <= -100)) throw new Exception(); // cover paths in decompiler
        Contract.EndContractBlock();
#else
        Contract.Requires<Exception>(args != null);
        Contract.Requires<ArgumentOutOfRangeException>(args.Length > 10, "args");
        Contract.Requires<Exception>(args.Length < 1000);
        Contract.Requires<Exception>((args.Length < 1000));
        Contract.Requires<Exception>((args.Length < 1000) && (args.Length > 100));
#endif
      }
      public void ExtraExtractorCodePaths(int[] args, object foo)
      {
#if LEGACY
        if ((args == null) && (foo != null || CheckSimpleFun(foo.ToString().Length > 100))) throw new Exception();
        Contract.EndContractBlock();
#else
        Contract.Requires<Exception>((args != null) || (foo == null && !CheckSimpleFun(foo.ToString().Length > 100)));
#endif
      }

      [Pure]
      public bool CheckSimpleFun(bool b)
      {
        return !b;
      }

    }

  }

  namespace DavidAllen {
    public class AutoPropProblem {
      // http://social.msdn.microsoft.com/Forums/en-US/codecontracts/thread/c2a8c853-b236-4da8-822d-4e50d51b7347/
      public interface J {
        int P { get; set; }
      }
      public class Impl1 : J {
        // user wants an invariant over their public property
        [ContractInvariantMethod]
        void ObjInv() {
          Contract.Invariant(0 <= P);
        }

        // public means it is used as the implementation for J.P
        // autoprop pre-post generation should *not* be done for
        // P's methods since they will inherit any contracts from
        // J.P's methods and cannot add any contracts
        public int P { get; set; }

      }
      public class Impl2 : J {
        // user wants an invariant over their interface implementation
        // (Seems weird, but who are we to cast stones?)
        [ContractInvariantMethod]
        void ObjInv() {
          J jThis = this as J;
          Contract.Invariant(0 <= jThis.P);
        }

        // autoprop pre-post generation should *not* be done for
        // these methods since they will inherit any contracts from
        // J.P's methods and cannot add any contracts
        int J.P { get; set; }
      }
      public class Base {
        public virtual int P { get; set; }
      }
      public class Sub : Base {
        // user wants an invariant over their public property
        [ContractInvariantMethod]
        void ObjInv() {
          Contract.Invariant(0 <= P);
        }

        // autoprop pre-post generation should *not* be done for
        // these methods since they will inherit any contracts from
        // Base.P's methods and cannot add any contracts
        public override int P { get; set; }
      }
    }
  }

  namespace MaF
  {
    [ContractOption("runtime", "checking", false)]
    public class TestContractCheckingOffOn
    {
      [ContractOption("runtime", "checking", true)]
      public virtual int Test0(string s)
      {
        Contract.Requires(s != null);
        Contract.Ensures(Contract.Result<int>() > 0);
        return 0;
      }

      public virtual int Test1(string s)
      {
        Contract.Requires(s != null);
        Contract.Ensures(Contract.Result<int>() > 0);
        return 1;
      }

      [ContractOption("runtime", "checking", true)]
      public class Nested
      {
        public virtual void Test2(string s)
        {
          Contract.Requires(s != null);
        }

        [ContractOption("runtime", "checking", false)]
        public virtual void Test3(string s)
        {
          Contract.Requires(s != null);
        }
      }
    }

    [ContractOption("contract", "Inheritance", false)]
    public class TestContractInheritanceOffOn : TestContractCheckingOffOn
    {
      [ContractOption("contract", "inheritance", true)] 
      public override int Test0(string s)
      {
        return 0;
      }

      public override int Test1(string s)
      {
        return 0;
      }

      [ContractOption("contract", "Inheritance", true)]
      new public class Nested : TestContractCheckingOffOn.Nested
      {
        [ContractOption("contract", "Inheritance", false)]
        public override void Test2(string s)
        {
        }

        public override void Test3(string s)
        {
        }
      }
    }

    public class TestOldArrays
    {
      int[] field = new[] { 7, 6, 5, 4, 3, 2, 1 };

      public void OldParameterArray(int[] z, bool behave)
      {
        Contract.Ensures(Contract.ForAll(0, z.Length, i => z[i] == Contract.OldValue(z[i])));
        if (!behave)
        {
          z[3]++;
        }
      }

      public void OldThisFieldArray(bool behave)
      {
        Contract.Ensures(Contract.ForAll(0, this.field.Length, i => this.field[i] == Contract.OldValue(this.field[i])));
        if (!behave)
        {
          this.field[3]++;
        }
      }
      public void OldThisFieldClosure(bool behave)
      {
        Contract.Ensures(Contract.ForAll(this.field, elem => elem == Contract.OldValue(elem)));
        if (!behave)
        {
          this.field[3]++;
        }
      }

      public void OldParameterFieldArray(TestOldArrays other, bool behave)
      {
        Contract.Ensures(Contract.ForAll(0, other.field.Length, i => other.field[i] == Contract.OldValue(other.field[i])));
        if (!behave)
        {
          other.field[3]++;
        }

      }

#if TESTING_BAD_CONTRACT_USE || false
      public void TestBadRequires()
      {
        Contract.Requires(Contract.Result<int>() == 0);
        Action x = () => Console.WriteLine(Contract.Result<int>());
      }

      [ContractInvariantMethod]
      void ObjectInvariant()
      {
        Contract.Invariant(Contract.Result<int>() == 0);
      }

      public string BadUseOfCallInContractBlock(string s) {
        Contract.Requires(s != null);
        Contract.ForAll(s, c => c != ' ');
        Contract.Ensures(Contract.Result<string>() != null);
        return s;
      }
      void RandomVoidCode() { }
      bool RandomBoolCode() { return true; }
      void RandomVoidCode(Predicate<int> pred) { }
      bool RandomBoolCode(Predicate<int> pred) { return true;  }

      public string BadUseOfCallInContractBlock2(string s) {
        Contract.Requires(s != null);
        RandomVoidCode();
        Contract.Ensures(Contract.Result<string>() != null);
        return s;
      }
      public string BadUseOfCallInContractBlock3(string s) {
        Contract.Requires(s != null);
        RandomBoolCode();
        Contract.Ensures(Contract.Result<string>() != null);
        return s;
      }

      public string BadUseOfCallInContractBlock4(string s) {
        Contract.Requires(s != null);
        RandomVoidCode(x => { Contract.ForAll(s, c => c != ' '); return true; });
        Contract.Ensures(Contract.Result<string>() != null);
        return s;
      }

      public string BadUseOfCallInContractBlock5(string s) {
        Contract.Requires(s != null);
        RandomBoolCode(x => { Contract.ForAll(s, c => c != ' '); return true; });
        Contract.Ensures(Contract.Result<string>() != null);
        return s;
      }

#endif
    }

    public class ClosureValidators
    {
      [ContractArgumentValidator]
      public static void NotNull(object obj, string name)
      {
        if (obj == null) throw new ArgumentNullException(name);
        Contract.EndContractBlock();
      }
      [ContractArgumentValidator]
      public static void NotNullCollection<T>(IEnumerable<T> collection, string name)
      {
        NotNull(collection, name);
        if (!Contract.ForAll(collection, element => element != null)) throw new ArgumentException("all elements of " + name + " must be non-null");
        Contract.EndContractBlock();
      }

      [ContractArgumentValidator]
      public static void NotNullArray<T>(T[] array, string name)
      {
        NotNull(array, name);
        if (!Contract.ForAll(0, array.Length, i => array[i] != null)) throw new ArgumentException("all elements of " + name + " must be non-null");
        Contract.EndContractBlock();
      }
    }
    public class ClosureAbbreviators {
      [ContractAbbreviator]
      public static void NotNull(object obj, string name) {
        Contract.Requires(obj != null, name);
      }
      [ContractAbbreviator]
      public static void NotNullCollection<T>(IEnumerable<T> collection, string name) {
        NotNull(collection, name);
        Contract.Requires(Contract.ForAll(collection, element => element != null), name);
        Contract.Ensures(Contract.ForAll(collection, element => element != null), name);
      }

      [ContractAbbreviator]
      public static void NotNullArray<T>(T[] array, string name) {
        NotNull(array, name);
        Contract.Requires(Contract.ForAll(0, array.Length, i => array[i] != null), name);
        Contract.Ensures(Contract.ForAll(0, array.Length, i => array[i] != null), name);
      }
    }
    public class TestClosureValidators
    {
      object[] field;

      public TestClosureValidators() { }
      public TestClosureValidators(object[] data)
      {
        this.field = data;
      }

      public void TestCollectionValidator<T>(ICollection<T> coll1, ICollection<T> coll2)
      {
        ClosureValidators.NotNullCollection(coll1, "coll1");
        ClosureValidators.NotNullCollection(coll2, "coll2");
      }

      public void TestArrayValidator<T>(T[] array1, T[] array2)
      {
        ClosureValidators.NotNullArray(array1, "array1");
        ClosureValidators.NotNullArray(array2, "array2");
      }

      public void TestCollectionValidator2<T>(ICollection<T> coll1, ICollection<T> coll2) {
        var tmp1 = coll1;
        ClosureValidators.NotNullCollection(tmp1, "coll1");
        var tmp2 = coll2;
        ClosureValidators.NotNullCollection(tmp2, "coll2");
      }

      public void TestArrayValidator2<T>(T[] array1, T[] array2) {
        var tmp1 = array1;
        ClosureValidators.NotNullArray(array1, "array1");
        var tmp2 = array2;
        ClosureValidators.NotNullArray(array2, "array2");
      }

      public void TestFieldArrayUnchanged()
      {
        EnsuresAllNonNullArray(0);
        EnsuresAllNonNullArray(1);
      }

      public void TestFieldAsCollectionUnchanged()
      {
        EnsuresAllNonNullCollection(0);
        EnsuresAllNonNullCollection(1);
      }

      [ContractArgumentValidator]
      private void RequireNonNullField()
      {
        if (this.field == null) throw new InvalidOperationException();
        Contract.EndContractBlock();
      }

      [ContractAbbreviator]
      private void EnsuresAllNonNullArray(int x)
      {
        Contract.Ensures(Contract.ForAll(0, this.field.Length, i => this.field[i] != null));
      }

      [ContractAbbreviator]
      private void EnsuresAllNonNullCollection(int x)
      {
        Contract.Ensures(Contract.ForAll(this.field, elem => elem != null));
      }

    }
    public class TestClosureAbbreviators {
      object[] field;

      public TestClosureAbbreviators() { }
      public TestClosureAbbreviators(object[] data) {
        this.field = data;
      }

      public void TestCollectionAbbreviator<T>(ICollection<T> coll1, ICollection<T> coll2) {
        ClosureAbbreviators.NotNullCollection(coll1, "coll1");
        ClosureAbbreviators.NotNullCollection(coll2, "coll2");
      }

      public void TestArrayAbbreviator<T>(T[] array1, T[] array2) {
        ClosureAbbreviators.NotNullArray(array1, "array1");
        ClosureAbbreviators.NotNullArray(array2, "array2");
      }

      public void TestCollectionAbbreviator2<T>(ICollection<T> coll1, ICollection<T> coll2) {
        var tmp1 = coll1;
        ClosureAbbreviators.NotNullCollection(tmp1, "coll1");
        var tmp2 = coll2;
        ClosureAbbreviators.NotNullCollection(tmp2, "coll2");
      }

      public void TestArrayAbbreviator2<T>(T[] array1, T[] array2) {
        var tmp1 = array1;
        ClosureAbbreviators.NotNullArray(array1, "array1");
        var tmp2 = array2;
        ClosureAbbreviators.NotNullArray(array2, "array2");
      }

      public void TestFieldArrayUnchanged() {
        EnsuresAllNonNullArray(0);
        EnsuresAllNonNullArray(1);
      }

      public void TestFieldAsCollectionUnchanged() {
        EnsuresAllNonNullCollection(0);
        EnsuresAllNonNullCollection(1);
      }

      [ContractAbbreviator]
      private void RequireNonNullField() {
        Contract.Requires(this.field != null);
      }

      [ContractAbbreviator]
      private void EnsuresAllNonNullArray(int x) {
        Contract.Ensures(Contract.ForAll(0, this.field.Length, i => this.field[i] != null));
      }

      [ContractAbbreviator]
      private void EnsuresAllNonNullCollection(int x) {
        Contract.Ensures(Contract.ForAll(this.field, elem => elem != null));
      }

    }
#if true
    public class CallerClass
    {
      public static void TestFieldArrayUnchanged(TestClosureValidators target)
      {
        //target.EnsuresAllNonNullArray();

      }
      public static void TestFieldAsCollectionUnchanged(TestClosureValidators target)
      {
        //target.EnsuresAllNonNullCollection();

      }
    }
#endif

  }

  namespace SuperBecio
  {

    class WriterBugWhenExceptionHandlerListHits21
    {
      static void Test(IEnumerable<object> children)
      {

        foreach (var item in children)
        {
        }
        foreach (var item in children)
        {
        }
        foreach (var item in children)
        {
        }
        foreach (var item in children)
        {
        }
        foreach (var item in children)
        {
        }
        foreach (var item in children)
        {
        }
        foreach (var item in children)
        {
        }
        foreach (var item in children)
        {
        }
        foreach (var item in children)
        {
        }
        foreach (var item in children)
        {
        }
        foreach (var item in children)
        {
        }
        foreach (var item in children)
        {
        }
        foreach (var item in children)
        {
        }
        foreach (var item in children)
        {
        }
        foreach (var item in children)
        {
        }
        foreach (var item in children)
        {
        }
        foreach (var item in children)
        {
        }
        foreach (var item in children)
        {
        }
        foreach (var item in children)
        {
        }
        foreach (var item in children)
        {
        }
#if true
        foreach (var item in children)
        {
        }
#endif
      }
    }
  }

  namespace Mokosh
  {
    public static class Enumerate
    {
      public static void Test(Projects pj)
      {
        foreach (var p in pj.TreeTraversal())
        {
        }
      }
      public static IEnumerable<ProjectItem> TreeTraversal(this Projects me)
      {
        Contract.Requires/*<ArgumentNullException>*/(me != null);

        foreach (Project p in me)
        {
          foreach (ProjectItem pi in p.ProjectItems)
          {
            yield return pi;
          }
        }
      }
    }
    public class Projects : IEnumerable<Project>
    {
      #region IEnumerable<Project> Members

      public IEnumerator<Project> GetEnumerator()
      {
        yield return new Project();
        yield return new Project();
      }

      #endregion

      #region IEnumerable Members

      System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
      {
        return this.GetEnumerator();
      }

      #endregion
    }
    public class Project
    {
      public IEnumerable<ProjectItem> ProjectItems
      {
        get
        {
          return new ProjectItem[] { new ProjectItem() };
        }
      }
    }
    public class ProjectItem
    {
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
      public static Selector noSelectorNeeded = new Selector();
    }
    public enum TransitionType
    {
      ElemTest,
      NotElemTest,
    }
    public class ManyForAlls
    {
      public ManyForAlls(Dictionary<State, Dictionary<TransitionType, Dictionary<Selector, HashSet<State>>>> initial)
      {
        this.deltaTable = initial;
      }

      public static void DoIt()
      {
        var x = new Dictionary<State, Dictionary<TransitionType, Dictionary<Selector, HashSet<State>>>>();
        var d = new Dictionary<TransitionType,Dictionary<Selector,HashSet<State>>>();
        x.Add(5, d);
        var d2 = new Dictionary<Selector,HashSet<State>>();
        d.Add(TransitionType.ElemTest, d2);
        d2.Add(NFA.noSelectorNeeded, null);
        var d3 = new Dictionary<Selector, HashSet<State>>();
        d.Add(TransitionType.NotElemTest, d3);
        d3.Add(new Selector(), null);
        var m = new ManyForAlls(x);

      }

      internal Dictionary<State, Dictionary<TransitionType, Dictionary<Selector, HashSet<State>>>> deltaTable;


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
    }
  }

}
