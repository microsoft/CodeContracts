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
using System.Windows.Forms;

namespace CodeUnderTest {
  public class RewrittenContractTest {
    #region Contracts

    public bool MustBeTrue = true;

    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(MustBeTrue);
    }

    public void AssertsTrue(bool mustBeTrue)
    {
      Contract.Assert(mustBeTrue);
    }

    public void AssumesTrue(bool mustBeTrue)
    {
      Contract.Assume(mustBeTrue);
    }

    public void RequiresTrue(bool mustBeTrue)
    {
      Contract.Requires(mustBeTrue);
    }

    public void RequiresTrue<TException>(bool mustBeTrue)
      where TException : Exception
    {
#if LEGACY
      if (!mustBeTrue) throw new ArgumentException("Precondition failed: mustBeTrue");
      Contract.EndContractBlock();
#else
      Contract.Requires<TException>(mustBeTrue);
#endif
    }

    public void EnsuresTrue(bool mustBeTrue)
    {
      Contract.Ensures(mustBeTrue);
    }

    private void PrivateRequiresTrue(bool mustBeTrue)
    {
      Contract.Requires(mustBeTrue);
    }

    public void CallPrivateRequiresTrue(bool mustBeTrue)
    {
      this.PrivateRequiresTrue(mustBeTrue);
    }

    private void PrivateEnsuresTrue(bool mustBeTrue)
    {
      Contract.Ensures(mustBeTrue);
    }

    public void CallPrivateEnsuresTrue(bool mustBeTrue)
    {
      this.PrivateEnsuresTrue(mustBeTrue);
    }

    public void RequiresAndEnsuresTrue(bool mustBeTrue)
    {
      Contract.Requires(mustBeTrue);
      Contract.Ensures(mustBeTrue);
    }

    public void MakeInvariantTrue()
    {
      this.MustBeTrue = true;
    }

    public void MakeInvariantFalse()
    {
      this.MustBeTrue = false;
    }

    public bool EnsuresReturnsTrue(bool mustBeTrue)
    {
      Contract.Ensures(Contract.Result<bool>());
      return mustBeTrue;
    }

    public void EnsuresSetsTrue(bool mustBeTrue, out bool output)
    {
      Contract.Ensures(Contract.ValueAtReturn(out output) == true);
      output = mustBeTrue;
    }

    public void RequiresAnything(bool anything)
    {
      Contract.Requires(anything || !anything);
    }

    public void EnsuresAnything(bool anything)
    {
      Contract.Ensures(anything == Contract.OldValue(anything));
    }

    public bool SetsAndReturnsOld(ref bool anything)
    {
      Contract.Ensures(Contract.Result<bool>() == Contract.OldValue(anything));
      anything = !anything;
      return !anything;
    }

    public void ThrowsEnsuresTrue(int exception, bool trueIfThrow)
    {
      Contract.EnsuresOnThrow<ApplicationException>(trueIfThrow);
      Contract.EnsuresOnThrow<SystemException>(trueIfThrow);
      Contract.EnsuresOnThrow<Exception>(trueIfThrow);
      Contract.Ensures(!trueIfThrow);

      switch (exception)
      {
        case 1:
          throw new ApplicationException();
        case 2:
          throw new SystemException();
        case 3:
          throw new Exception();
      }
    }
    public void RequiresForAll(int[] xs)
    {
      Contract.Requires(Contract.ForAll(0, xs.Length, delegate(int i) { return xs[i] > 0; }));
    }
    public void RequiresExists(int[] xs)
    {
      Contract.Requires(Contract.Exists(0, xs.Length, delegate(int i) { return xs[i] > 0; }));
    }
    public int[] EnsuresForAll(int[] xs, bool ShouldBeCorrect)
    {
      Contract.Ensures(
        Contract.ForAll(0, xs.Length, delegate(int i) { return xs[i] == Contract.Result<int[]>()[i]; })
      );
      int[] ys = new int[xs.Length];
      for (int i = 0; i < xs.Length; i++)
        ys[i] = xs[i];
      if (!ShouldBeCorrect && xs.Length > 0) ys[xs.Length - 1]++;
      return ys;
    }
    public int[] EnsuresExists(int[] xs, bool ShouldBeCorrect)
    {
      Contract.Ensures(
        Contract.Exists(0, xs.Length, delegate(int i) { return xs[i] == Contract.Result<int[]>()[i]; })
      );
      int[] ys = new int[xs.Length];
      for (int i = 0; i < xs.Length; i++)
        ys[i] = xs[i] + 1;
      if (ShouldBeCorrect && xs.Length > 0) ys[xs.Length - 1] = xs[xs.Length - 1];
      return ys;
    }
    public void EnsuresForAllWithOld(int[] xs, bool ShouldBeCorrect)
    {
      Contract.Requires(xs != null && xs.Length > 0);
      Contract.Ensures(
        Contract.ForAll(0, xs.Length, delegate(int i) { return xs[i] == Contract.OldValue(xs[i]); })
      );
      if (!ShouldBeCorrect) xs[xs.Length - 1]++;
    }
    public void EnsuresDoubleClosure(int[] xs, int[] ys, bool ShouldBeCorrect)
    {
      Contract.Ensures(
        Contract.ForAll(0, xs.Length, i => Contract.Exists(0, ys.Length, j => xs[i] == ys[j]))
      );
      if (!ShouldBeCorrect) xs[xs.Length - 1]++;
    }

    public void EnsuresExistsWithOld(int[] xs, bool ShouldBeCorrect)
    {
      Contract.Requires(xs != null && xs.Length > 0);
      Contract.Ensures(
        Contract.Exists(0, xs.Length, delegate(int i) { return xs[i] == Contract.OldValue(xs[i]); })
      );
      for (int i = 0; i < xs.Length; i++)
      {
        xs[i]++;
      }
      if (ShouldBeCorrect) xs[xs.Length - 1]--;
    }
    public int EnsuresWithResultAndQuantifiers(int[] xs, bool BeCorrect)
    {
      Contract.Requires(xs.Length > 0);
      Contract.Ensures(Contract.Result<int>() > 0);
      Contract.Ensures(
        Contract.ForAll(0, xs.Length,
          delegate(int i)
          {
            return Contract.Result<int>() > 0 &&
                   (i == 0 ||
                   Contract.Exists(0, i,
                     delegate(int j)
                     {
                       return Contract.Result<int>() >= xs[i];
                     }));
          })
      );
      Contract.Ensures(Contract.Result<int>() > 0);
      if (!BeCorrect)
        xs[xs.Length - 1]++;
      int r = Int32.MinValue;
      foreach (int x in xs) if (x > r) r = x;
      if (!BeCorrect) return xs[0] - 1;
      return r < 0 ? 1 : r;
    }
#if TURNINGSTATICCLOSUREINTOOBJECTCLOSURE
        public int[] EnsuresWithStaticClosure(bool beCorrect)
        {
          Contract.Ensures(Contract.ForAll(0, Contract.Result<int[]>().Length, i => Contract.Result<int[]>()[i] > 0));

          var result = new int[] { 1, 2, 3 };
          if (!beCorrect)
          {
            result[2] = 0;
          }
          return result;
        }
#endif
    public void IfThenThrowAsPrecondition(bool preconditionHolds)
    {
#if LEGACY
      if (!preconditionHolds)
        throw new ArgumentException();
      Contract.EndContractBlock();
#else
      Contract.Requires<ArgumentException>(preconditionHolds);
#endif
    }
    double Sample() { return 0.0; }
    public int IfThenThrowAsPrecondition2(int maxValue)
    {
#if LEGACY
      if (maxValue < 0)
      {
        throw new ArgumentOutOfRangeException("maxValue",
              string.Format("{0}", new object[] { "maxValue" }));
      }
      Contract.EndContractBlock();
#else
      Contract.Requires<ArgumentOutOfRangeException>(maxValue >= 0, "maxValue");
#endif

      return (int)(this.Sample() * maxValue);
    }
    public int IfThenThrowAsPrecondition3(int maxValue)
    {
#if LEGACY
      if (maxValue < 0)
        throw new ArgumentOutOfRangeException();
#else
      Contract.Requires<ArgumentOutOfRangeException>(maxValue >= 0);
#endif
      Contract.Requires(maxValue == 3);
      return (int)(this.Sample() * maxValue);
    }

    [Pure]
    public int TakeParamsAsArgument(params int[] xs) { return 3; }
    public int UseAParamsMethodInContract(int x)
    {
      Contract.Requires(TakeParamsAsArgument(x) == x);
      return x + 1;
    }

    public int AssertArgumentIsPositive(int y)
    {
      Contract.Assert(0 < y);
      return y;
    }

    public void Swap(ref int x, ref int y, bool behave)
    {
      Contract.Requires(x != y);
      Contract.Ensures(x == Contract.OldValue(y) && y == Contract.OldValue(x));
      if (behave)
      {
        int t = x;
        x = y;
        y = t;
      }
      else
      {
        x = y;
      }
      return;
    }

    public string PostConditionUsingResultAsCallee(bool behave)
    {
      Contract.Ensures(Contract.Result<string>().Length == 3);
      return behave ? "abc" : "wrongResult";
    }

    public void RequiresArgumentNullException(string parameter)
    {
#if LEGACY
      if (parameter == null) throw new ArgumentNullException("parameter");
      Contract.EndContractBlock();
#else
      Contract.Requires<ArgumentNullException>(parameter != null, "parameter");
#endif
    }
    public void RequiresArgumentOutOfRangeException(int parameter)
    {
#if LEGACY
      if (parameter < 0) throw new ArgumentOutOfRangeException("parameter");
      Contract.EndContractBlock();
#else
      Contract.Requires<ArgumentOutOfRangeException>(parameter >= 0, "parameter");
#endif
    }
    public void RequiresArgumentException(int x, int y)
    {
#if LEGACY
      if (x < y) throw new ArgumentException("Precondition failed: x >= y: x must be at least y\r\nParameter name: x must be at least y");
      Contract.EndContractBlock();
#else
      Contract.Requires<ArgumentException>(x >= y, "x must be at least y");
#endif
    }
    public static int TestForMemberVisibility1(int x)
    {
      Contract.Requires(0 < x, String.Empty);
      return 3;
    }

    #endregion
  }

  /// <summary>
  /// Test to make sure that if an invariant mentions a public method
  /// (which itself then checks the invariant) an infinite loop does
  /// not result. When re-entered, an invariant is supposed to just
  /// "return true" (i.e., it is a void method so it just returns
  /// without throwing an exception).
  /// </summary>
  public class ReEntrantInvariantTest {
    #region Contracts

    public class ClassWithInvariant {
      public bool b = true;
      public bool y = true;
      [ContractInvariantMethod]
      private void ObjectInvariant1()
      {
        Contract.Invariant(b);
      }

      [ContractInvariantMethod]
      private void ObjectInvariant2()
      {
        Contract.Invariant(PureMethod());
      }

      [Pure]
      public bool PureMethod() { return y; }
      public int M(int x) { return x; }

      public void ReEnter()
      {
        // Don't really need to call another method because this method
        // must be public and so gets rewritten to call the invariant.
        // But this keeps it consistent with the rest of the tests.
        M(3);
      }

      public void FailInvariant1()
      {
        this.b = false;
      }

      public void FailInvariant2()
      {
        this.y = false;
      }

    }
    #endregion Contracts
  }

  /// <summary>
  /// Test to make sure that an invariant in a class does not get
  /// "inherited" by any nested classes.
  /// </summary>
  public class ClassWithInvariantAndNestedClass {
    #region Contracts
    public bool b = true;
    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(b);
    }
    #endregion Contracts
  }

  public class LocalsOfStructTypeEndingUpInAssignments {
    #region Contracts

    public interface IPredicate<T> {
      [Pure]
      bool Holds(T value);
    }
    public struct Positive : IPredicate<int> {
      [Pure]
      public bool Holds(int value)
      {
        Contract.Ensures(Contract.Result<bool>() == (value > 0));
        return value > 0;
      }
    }
    public class MyCollection<T, Pred> where Pred : struct, IPredicate<T> {
      public void Add(T value)
      {
        Contract.Requires(new Pred().Holds(value));
      }
      public T Pull()
      {
        Contract.Ensures(new Pred().Holds(Contract.Result<T>()));
        return default(T);
      }
    }
    #endregion Contracts
  }

  public class LegacyPreconditions {
    #region Contracts
    public class C {
      [Pure]
      private void ThrowHelper()
      {
        throw new TestRewriterMethods.PreconditionException();
      }
      public int DirectThrow(int x)
      {
#if LEGACY
        if (x < 0)
        {
          throw new TestRewriterMethods.PreconditionException();
        }
        Contract.EndContractBlock();
#else
        Contract.Requires<TestRewriterMethods.PreconditionException>(x >= 0);
#endif
        return x + 1;
      }
      public int IndirectThrow(int x)
      {
#if LEGACY
        if (x < 0)
        {
          ThrowHelper();
        }
        Contract.EndContractBlock();
#else
        Contract.Requires<TestRewriterMethods.PreconditionException>(x >= 0);
#endif
        return x + 1;
      }
    }
    #endregion Contracts
  }

  public class GenericClassWithDeferringCtor {
    #region Contracts
    /// <summary>
    /// The generic type doesn't matter for the actual contract,
    /// just to check whether the contract gets extracted or not.
    /// </summary>
    public class C<T> {
      public C(int x) { }
      public C(char c) :
        this(c == 'a' ? 0 : 1)
      {
        Contract.Requires(c != 'z');
      }
    }
    #endregion Contracts
  }

  public class IfaceContractUsingLocalToHoldSelf {
    #region Contracts
    [ContractClass(typeof(ContractForJ))]
    public interface J {
      bool M(int[] xs, bool shouldBehave);
      bool P { get; }
      bool Q { get; }
    }

    [ContractClassFor(typeof(J))]
    abstract class ContractForJ : J {
      public bool M(int[] xs, bool shouldBehave)
      {
        J jThis = this;
        Contract.Requires(jThis.P);
        Contract.Requires(jThis.Q);
        Contract.Requires(this.Q); // reference non interface method
        Contract.Requires(xs != null);
        Contract.Requires(Contract.ForAll(0, xs.Length, i => xs[i] != 0));
        Contract.Ensures(Contract.Result<bool>() == Contract.OldValue(jThis.P));
        return default(bool);
      }
      bool J.P
      {
        get
        {
          Contract.Ensures(Contract.Result<bool>());
          return default(bool);
        }
      }
      public bool Q
      {
        get
        {
          return default(bool);
        }
      }
    }


    public class B : J {
      public virtual bool M(int[] xs, bool shouldBehave)
      {
        return shouldBehave;
      }
      public virtual bool P
      {
        get { return true; }
      }

      public bool Q { get { return true; } }
    }
    #endregion Contracts
  }

  public class EvaluateOldExpressionsAfterPreconditions {
    #region Contracts
    public class C {
      public void M(int[] xs)
      {
        Contract.Requires(xs != null);
        Contract.Ensures(0 < Contract.OldValue(xs.Length));
      }

      public int ConditionalOld(int[] xs)
      {
        Contract.Ensures(xs == null || Contract.Result<int>() == Contract.OldValue(xs.Length));

        if (xs == null) return 0;
        return xs.Length;
      }
    }
    #endregion Contracts
  }

  public class PostConditionsInStructCtorsMentioningFields {
    #region Contracts
    // Make sure that ValueAtReturn can be used with struct fields
    public struct S {
      int x;
      int y;
      public S(int a, int b, bool behave)
      {
        Contract.Ensures(Contract.ValueAtReturn(out this.x) == a);
        Contract.Ensures(Contract.ValueAtReturn(out this.y) == b);
        if (behave)
        {
          this.x = a;
          this.y = b;
        }
        else
        {
          this.x = a + 1;
          this.y = b + 1;
        }
      }
    }
    #endregion Contracts
  }

  public class PrivateInvariantInSealedType {
    #region Contracts
    // Make sure that ValueAtReturn can be used with struct fields
    public sealed class T {
      int x;
      int y;

      [ContractInvariantMethod]
      private void ObjectInvariant()
      {
        Contract.Invariant(x >= y);
      }
      public T(int a, int b, bool behave)
      {
        if (a < b)
        {
          var tmp = b;
          b = a;
          a = tmp;
        }
        if (behave)
        {
          this.x = a;
          this.y = b;
        }
        else
        {
          this.x = b;
          this.y = a;
        }
      }
    }
    #endregion Contracts
  }

  public class ConstructorsWithClosures {
    #region Contracts

    public class Base {
      public Base(ref object byref)
      {
      }
    }

    public class ValueMapping : Base {
      internal readonly Func<object, string> ValueToString;
      internal string name = "";

      public ValueMapping(object encoder)
        : base(ref encoder)
      {
#if LEGACY
        if (encoder == null) throw new ArgumentException("Precondition failed: encoder != null");
        Contract.EndContractBlock();
#else
        Contract.Requires<ArgumentException>(encoder != null);
#endif
        var nullEncoder = encoder as string;

        this.ValueToString = obj => (obj != null) ? encoder.ToString() : nullEncoder;
      }

      public ValueMapping(object encoder, bool dummy)
        : base(ref encoder)
      {
#if LEGACY
        if (encoder == null) throw new ArgumentException("Precondition failed: encoder != null");
        Contract.EndContractBlock();
#else
        Contract.Requires<ArgumentException>(encoder != null);
#endif
        var nullEncoder = encoder as string;

        this.ValueToString = obj => (obj != null) ? (dummy) ? encoder.ToString() : nullEncoder : this.name;
      }

      public ValueMapping(object encoder, string dummy)
        : this(encoder)
      {
#if LEGACY
        if (encoder == null) throw new ArgumentException("Precondition failed: encoder != null");
        Contract.EndContractBlock();
#else
        Contract.Requires<ArgumentException>(encoder != null);
#endif
        var nullEncoder = encoder as string;

        this.ValueToString = obj => (obj != null) ? (dummy == null) ? encoder.ToString() : nullEncoder : this.name;
      }

      public ValueMapping(object encoder, string[] data)
        : base(ref encoder)
      {
#if LEGACY
        if (encoder == null) throw new ArgumentException("Precondition failed: encoder != null");
#else
        Contract.Requires<ArgumentException>(encoder != null);
#endif
        Contract.Ensures(Contract.Exists(0, data.Length, i => data[i] == this.name));

        var nullEncoder = encoder as string;
        this.name = nullEncoder;

        this.ValueToString = obj => (obj != null) ? encoder.ToString() : nullEncoder;
      }

    }

    public struct StructCtors {
      internal readonly Func<object, string> ValueToString;
      internal string name;

      public StructCtors(object encoder)
      {
#if LEGACY
        if (encoder == null) throw new ArgumentException("Precondition failed: encoder != null");
        Contract.EndContractBlock();
#else
        Contract.Requires<ArgumentException>(encoder != null);
#endif

        this.name = "";
        var nullEncoder = encoder as string;

        this.ValueToString = obj => (obj != null) ? encoder.ToString() : nullEncoder;
      }

      public StructCtors(object encoder, bool dummy)
        : this(encoder)
      {
#if LEGACY
        if (encoder == null) throw new ArgumentException("Precondition failed: encoder != null");
        Contract.EndContractBlock();
#else
        Contract.Requires<ArgumentException>(encoder != null);
#endif

        var nullEncoder = encoder as string;

        this.ValueToString = obj => (obj != null) ? (dummy) ? encoder.ToString() : nullEncoder : "";
      }

      public StructCtors(object encoder, string dummy)
      {
#if LEGACY
        if (encoder == null) throw new ArgumentException("Precondition failed: encoder != null");
        Contract.EndContractBlock();
#else
        Contract.Requires<ArgumentException>(encoder != null);
#endif

        var nullEncoder = encoder as string;
        this.name = "";
        this.ValueToString = obj => (obj != null) ? (dummy == null) ? encoder.ToString() : nullEncoder : "";
      }

      public StructCtors(object encoder, string[] data)
      {
#if LEGACY
        if (encoder == null) throw new ArgumentException("Precondition failed: encoder != null");
#else
        Contract.Requires<ArgumentException>(encoder != null);
#endif
        Contract.Ensures(Contract.Exists(0, data.Length, i => data[i] == (encoder as string)));

        var nullEncoder = encoder as string;
        this.name = nullEncoder;

        this.ValueToString = obj => (obj != null) ? encoder.ToString() : nullEncoder;
      }

    }

    #endregion Contracts
  }

  /// <summary>
  /// Test Matrix: 
  /// Variable 1: Is it class/struct/generic? (non-generic class, generic class, generic struct)
  /// Variable 2: Is Method generic? (generic/non-generic)
  /// Variable 3: Does contract involve (static| instance) closure? (no closure/static closure/instance closure with capture/static closure with capture)
  /// 
  /// In Test2 and Test3: we further test:
  /// Variable 4: (legacy precondition| precondition) + postcondition + p|n
  /// 
  /// Test2.Test1a: non-generic method static closure requires-ensures positive | requires negative (2 calls)
  /// Test2.Test1aa: non-genric method static closure requires positive ensures negative (1 call)
  /// Test2.Test1b: non-generic method legacy requires no clousre: (2 calls p|n)
  /// Test2.Test1c: generic method legacy requires static closure with capture
  /// Test2.Test1d: generic method static closure precondition failable (3 calls, 2n | 1p)
  /// Test2.Test1f: generic method static closure with capture (3 calls, 2n-precondition fails different ways| 1p)
  /// Test2.Test1g: generic method instance closure with capture (3 calls, 2n-precondition fails different ways |1p)
  /// Test2.Test1h: generic method static closure with capture/generic with constraints
  /// 
  /// Test3.Test1a: generic class, non-generic method, static closure
  /// Test3.Test1aa: generic class, non generic method, static closure, requires positive, ensures negative
  /// Test3.Test1b: generic class, non-generic method, legacy requires, no clousre
  /// Test3.Test1c: generic class, non-generic method, mixed requires, static closure
  /// Test3.Test1d: generic class, generic method, static closure
  /// Test3.Test1e: generic class, generic method, static closure
  /// Test3.Test1g: generic class, generic method, instance closure
  /// Test3.Test1h: a comprehensive test case
  /// 
  /// Test4.Test1a: generic struct, non-generic method static closure
  /// Test4.Test1c: generic struct, generic method static closure
  ///
  /// In addtion: Test5x tests when the return type is IEnumerator.
  /// 
  /// Drivers' naming convention:
  /// 
  /// Iterator + ("" (non-generic type non-generic method) | GenCls | GenMeth | GenClsMeth | GenStr |GenStrMth) + (Pos | Neg) + ( "" (meaning precondition) | Post) + ( "" (meaning parameter) | collection | elem) + "NN | Eq ... | LT |...".
  /// </summary>
  public class IteratorSimpleContract {

    #region auxiliary test cases
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IIncrementable<T> {
      T IncrementBy(int i);
      [Pure]
      int Value();
    }

    public class A : IIncrementable<A> {
      public A(string s)
      {
        this.s = s;
      }
      string s;
      public int Value()
      {
        return s.Length;
      }
      public A IncrementBy(int i)
      {
        // bug: doesnt increase the size
        return this;
      }
    }
    #endregion auxiliary test cases

    #region Contracts
    #region Non-generic Class
    public class Test2 {

      /// <summary>
      /// non-generic method & pre/postcondition & pre positive/negative & static closure)
      /// </summary>
      public IEnumerable<string> Test1a(string s)
      {
        Contract.Requires(s != null);
        Contract.Ensures(Contract.Result<IEnumerable<string>>() != null);
        Contract.Ensures(Contract.ForAll(Contract.Result<IEnumerable<string>>(), (string s1) => s1 != null));
        yield return "hello";
      }

      /// <summary>
      /// Same as Test1a, except that the forall postcondition is intentionally violated.
      /// </summary>
      public IEnumerable<string> Test1aa(string s)
      {
        Contract.Requires(s != null);
        Contract.Ensures(Contract.Result<IEnumerable<string>>() != null);
        Contract.Ensures(Contract.ForAll(Contract.Result<IEnumerable<string>>(), (string s1) => s1 != null));
        yield return null;
      }

      /// <summary>
      /// Contract for iterator method
      /// </summary>
      /// <param name="s"></param>
      /// <returns></returns>
      public IEnumerable<string> Test1b(string s)
      {
#if LEGACY
        if (s == null) throw new ArgumentException("");
        Contract.EndContractBlock();
#else
        Contract.Requires<ArgumentException>(s != null, "");
#endif
        yield return s;
      }

      public IEnumerable<T> Test1c<T>(T t, IEnumerable<T> ts)
      {
        if (t.Equals(default(T))) throw new ArgumentException("");
        Contract.Requires(Contract.ForAll(ts, (T t1) => t1.Equals(t)));
        yield return t;
      }

      /// <summary>
      /// Contract for iterator method with type parameter. 
      /// Post condition is forall
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="input"></param>
      /// <returns></returns>
      public IEnumerable<T> Test1d<T>(IEnumerable<T> input)
      {
        Contract.Requires(input != null);
        Contract.Requires(Contract.ForAll(input, (T s1) => s1 != null));
        Contract.Ensures(Contract.ForAll(Contract.Result<IEnumerable<T>>(), (T s1) => s1 != null));
        foreach (T t in input)
          yield return t;
      }

      /// <summary>
      /// Contract for iterator method with type parameters and input ienumerable, precondition is forall. 
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="input"></param>
      public IEnumerable<T> Test1e<T>(IEnumerable<T> input)
      {
        Contract.Requires(Contract.ForAll(input, (T s) => s != null));
        foreach (T t in input)
        {
          yield return t;
        }
      }

      /// <summary>
      /// Contract for iterator method with closure that captures a parameter. 
      /// </summary>
      /// <param name="inputArray"></param>
      /// <param name="max"></param>
      public IEnumerable<int> Test1f(IEnumerable<int> inputArray, int max)
      {
        Contract.Requires(Contract.ForAll(inputArray, (int x) => x < max));
        Contract.Ensures(Contract.ForAll(Contract.Result<IEnumerable<int>>(), (int y) => y > 0));
        foreach (int i in inputArray)
        {
          yield return (max - i - 1);
        }
      }

      /// <summary>
      /// Generic method: precondition with instance closure that captures a parameter;
      /// </summary>
      /// <param name="x"></param>
      /// <returns></returns>
      public IEnumerable<T> Test1g<T>(IEnumerable<T> ts, T x)
      {
        Contract.Requires(Contract.ForAll(ts, (T y) => foo(y, x)));
        yield return x;
      }

      [Pure]
      public bool foo(object y, object x)
      {
        return y == x;
      }


      /// <summary>
      /// Contract for iterator method: Type parameter has a contraint. 
      /// closure used in ensures captures a parameter.
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="input"></param>
      /// <param name="x"></param>
      /// <returns></returns>
      public IEnumerable<T> Test1h<T>(IEnumerable<T> input, int x, int y)
        where T : IIncrementable<T>
      {
        Contract.Requires(Contract.ForAll(input, (T t) => t.Value() > x));
        Contract.Ensures(Contract.ForAll(Contract.Result<IEnumerable<T>>(), (T t) => t.Value() > (x + y)));
        foreach (T t in input)
        {
          yield return t.IncrementBy(y);
        }
      }

      public void Test2h()
      {
        A a = new A("hello");
        A[] aarray = { a };
        Test1h(aarray, 4, 5);
      }
    }
    #endregion Non-generic Class
    #region Generic Class

    public class Test3<T>
      where T : class, IIncrementable<T> {
      public Test3(T t)
      {
        tfield = t;
      }

      /// <summary>
      /// non-generic method & pre/postcondition & pre positive/negative & static closure)
      /// </summary>
      public IEnumerable<T> Test1a(T t)
      {
        Contract.Requires(t != null);
        Contract.Ensures(Contract.Result<IEnumerable<T>>() != null);
        Contract.Ensures(Contract.ForAll(Contract.Result<IEnumerable<T>>(), (T s1) => s1 != null));
        yield return t;
      }

      /// <summary>
      /// Same as Test1a, except that the forall postcondition is intentionally violated.
      /// </summary>
      public IEnumerable<T> Test1aa(T s)
      {
        Contract.Ensures(Contract.Result<IEnumerable<T>>() != null);
        Contract.Ensures(Contract.ForAll(Contract.Result<IEnumerable<T>>(), (T s1) => s1 != null));
        yield return null;
      }

      /// <summary>
      /// Contract for iterator method: Legacy precondition, with EndContractBlock().
      /// </summary>
      /// <param name="s"></param>
      /// <returns></returns>
      public IEnumerable<T> Test1b(T s)
      {
#if LEGACY
        if (s == null) throw new ArgumentException("");
        Contract.EndContractBlock();
#else
        Contract.Requires<ArgumentException>(s != null, "");
#endif
        yield return s;
      }

      /// <summary>
      /// Contract for iterator method with type parameter. 
      /// Type parameter of the method masks that of the class
      /// </summary>
      public IEnumerable<T> Test1d(IEnumerable<T> input)
      {
        Contract.Requires(input != null);
        Contract.Requires(Contract.ForAll(input, (T s1) => s1 != null));
        Contract.Ensures(Contract.ForAll(Contract.Result<IEnumerable<T>>(), (T s1) => s1 != null));
        foreach (T t in input)
          yield return t;
      }

      /// <summary>
      /// Contract for iterator method with type parameters and input ienumerable, precondition is forall. 
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="input"></param>
      public IEnumerable<T1> Test1e<T1>(IEnumerable<T> input, T1 t)
        where T1 : IIncrementable<T1>
      {
        Contract.Requires(Contract.ForAll(input, (T s) => s.Value() == t.Value()));
        yield return t;
      }


      /// <summary>
      /// Generic method: precondition with instance closure that captures parameters;
      /// </summary>
      /// <param name="x"></param>
      /// <returns></returns>
      public IEnumerable<T1> Test1g<T1>(IEnumerable<T1> ts, T x)
        where T1 : IIncrementable<T1>
      {
        Contract.Requires(Contract.ForAll(ts, (T1 y) => foo(y, x)));
        foreach (T1 t1 in ts) yield return t1;
      }

      [Pure]
      public bool foo<S>(IIncrementable<S> y, T x)
      {
        return y.Value() == x.Value();
      }

      T tfield;

      public T TField
      {
        get
        {
          return tfield;
        }
      }

      public IEnumerable<T> Test1h(IEnumerable<T> input)
      {
        Contract.Ensures(Contract.ForAll(Contract.Result<IEnumerable<T>>(), (T t) => t.Value() > TField.Value()));
        foreach (T t in input)
        {
          yield return t.IncrementBy(2);
        }
      }
    }
    #endregion
    #region Generic Struct
    public struct Test4<T>
      where T : class, IIncrementable<T> {
      public Test4(T t)
      {
        tfield = t;
      }

      /// <summary>
      /// non-generic method & pre/postcondition & pre positive/negative & static closure)
      /// </summary>
      public IEnumerable<T> Test1a(T t)
      {
        Contract.Requires(t != null);
        Contract.Ensures(Contract.Result<IEnumerable<T>>() != null);
        Contract.Ensures(Contract.ForAll(Contract.Result<IEnumerable<T>>(), (T s1) => s1 != null));
        yield return t;
      }

      /// <summary>
      /// Same as Test1a, except that the forall postcondition is intentionally violated.
      /// </summary>
      public IEnumerable<T> Test1aa(T s)
      {
        Contract.Ensures(Contract.Result<IEnumerable<T>>() != null);
        Contract.Ensures(Contract.ForAll(Contract.Result<IEnumerable<T>>(), (T s1) => s1 != null));
        yield return null;
      }

      /// <summary>
      /// Contract for iterator method with type parameters and input ienumerable, precondition is forall. 
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="input"></param>
      public IEnumerable<T1> Test1e<T1>(IEnumerable<T> input, T1 t)
        where T1 : IIncrementable<T1>
      {
        Contract.Requires(Contract.ForAll(input, (T s) => s.Value() == t.Value()));
        yield return t;
      }
      T tfield;
    }
    #endregion
    #region IEnumerator
    public IEnumerator<int> Test5a(int x)
    {
      Contract.Requires(x > 0);
      // not working -- Contract.Ensures(Contract.ForAll(Contract.Result<IEnumerator<int>>(), (int y) => y > 0));
      for (int i = x; i < 10; i++)
      {
        yield return i;
      }
    }
    #endregion
    #endregion Contracts
  }

  public class LocalVariableForResult {
    #region Contracts
    public class C {
      public bool Test1(int x)
      {
        Contract.Requires(x > 0);
        bool result = Contract.Result<bool>();
        Contract.Ensures(result != false);
        Contract.Ensures(result || !result);

        return x == 1;
      }

      public bool Test2(int x, bool[] arr)
      {
        Contract.Requires(x > 0);
        Contract.Requires(arr != null);
        bool result = Contract.Result<bool>();
        Contract.Ensures(result != false);
        Contract.Ensures(result || !result);
        Contract.Ensures(Contract.ForAll(0, arr.Length, i => arr[i] == result));

        return x == 1;
      }
    }
    #endregion Contracts
  }

  public class IgnoreRuntimeAttributeTest {
    #region Contracts
    public class C {
      public int a = 27;

      [ContractInvariantMethod]
      private void MyInv()
      {
        Contract.Invariant(a == 27);
        Contract.Invariant(P(a));
        Contract.Invariant(G(a) != 27);
      }

      [ContractRuntimeIgnored]
      [Pure]
      public bool P(int x)
      {
        throw new InvalidOperationException("P should not be executed!");
      }
      [ContractRuntimeIgnored]
      [Pure]
      public T G<T>(T t)
      {
        throw new InvalidOperationException("G<T> should not be executed!");
      }
      [ContractRuntimeIgnored]
      public bool Property
      {
        get
        {
          throw new InvalidOperationException("Property should not be executed!");
        }
      }

      [Pure]
      public bool Foo(bool a, bool b) { return a || b; }

      public bool M(int x, bool behave)
      {
        Contract.Requires(a == 5 || Property && Foo(a == 5 ? Property : true, Property ? true : false));
        Contract.Requires(P(x));
        Contract.Requires(G(x) != 0);
        Contract.Requires(Property);
        Contract.Ensures(Contract.Result<bool>());
        Contract.Ensures(P(3));
        Contract.Ensures(G(3) == 27);
        Contract.Ensures(Property);

        return behave;
      }


    }
    #endregion Contracts
  }

  /// <summary>
  /// All parameters mentioned in postconditions that are not already
  /// within an old expression should get wrapped in one so that
  /// their initial value is used in the postcondition in case the
  /// parameter is updated in the method body.
  /// </summary>
  public class WrapParametersInOld {
    #region Contracts
    public class C {
      int a;

      public C(int b)
      {
        Contract.Requires(b == 3);
        Contract.Ensures(this.a == b);
        Contract.Ensures(b == 3);
        a = b;
        b = 27;
      }
      public bool Identity(bool b)
      {
        Contract.Ensures(Contract.Result<bool>() == b);
        var result = b;
        b = !b;
        return result;
      }

      public struct S {
        public int x;
      }

      public void F(S s)
      {
        Contract.Requires(s.x == 3);
        Contract.Ensures(s.x == 3);
        s.x++;
      }
    }
    #endregion Contracts
  }
}
