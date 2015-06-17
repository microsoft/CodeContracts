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

namespace CodeUnderTest {
  #region Contracts
  [ContractClass(typeof(ContractForOverriddenInterface))]
  interface IRewrittenInheritanceOverriddenInterface {
    void ImplicitlyOverriddenInterfaceRequires(bool mustBeTrue);
    void ImplicitlyOverriddenInterfaceEnsures(bool mustBeTrue);
    void ExplicitlyOverriddenInterfaceRequires(bool mustBeTrue);
    void ExplicitlyOverriddenInterfaceEnsures(bool mustBeTrue);
  }

  [ContractClassFor(typeof(IRewrittenInheritanceOverriddenInterface))]
  abstract class ContractForOverriddenInterface : IRewrittenInheritanceOverriddenInterface {
    void IRewrittenInheritanceOverriddenInterface.ImplicitlyOverriddenInterfaceRequires(bool mustBeTrue)
    {
      Contract.Requires(mustBeTrue);
    }

    void IRewrittenInheritanceOverriddenInterface.ImplicitlyOverriddenInterfaceEnsures(bool mustBeTrue)
    {
      Contract.Ensures(mustBeTrue);
    }

    public void ExplicitlyOverriddenInterfaceRequires(bool mustBeTrue)
    {
      Contract.Requires(mustBeTrue);
    }

    public void ExplicitlyOverriddenInterfaceEnsures(bool mustBeTrue)
    {
      Contract.Ensures(mustBeTrue);
    }
  }

  [ContractClass(typeof(ContractForMultipleInheritanceInterface))]
  public interface IRewrittenMultipleInheritanceInterface {
    void MultiplyImplicitlyOverriddenInterfaceRequires(bool mustBeTrue);
    void MultiplyImplicitlyOverriddenInterfaceEnsures(bool mustBeTrue);
    void MultiplyExplicitlyOverriddenInterfaceRequires(bool mustBeTrue);
    void MultiplyExplicitlyOverriddenInterfaceEnsures(bool mustBeTrue);
    void MultiplyDifferentlyOverriddenInterfaceRequires(bool mustBeTrue);
    void MultiplyDifferentlyOverriddenInterfaceEnsures(bool mustBeTrue);
  }

  [ContractClassFor(typeof(IRewrittenMultipleInheritanceInterface))]
  abstract class ContractForMultipleInheritanceInterface : IRewrittenMultipleInheritanceInterface {
    void IRewrittenMultipleInheritanceInterface.MultiplyImplicitlyOverriddenInterfaceRequires(bool mustBeTrue)
    {
      Contract.Requires(mustBeTrue);
    }

    void IRewrittenMultipleInheritanceInterface.MultiplyImplicitlyOverriddenInterfaceEnsures(bool mustBeTrue)
    {
      Contract.Ensures(mustBeTrue);
    }

    void IRewrittenMultipleInheritanceInterface.MultiplyExplicitlyOverriddenInterfaceRequires(bool mustBeTrue)
    {
      Contract.Requires(mustBeTrue);
    }

    void IRewrittenMultipleInheritanceInterface.MultiplyExplicitlyOverriddenInterfaceEnsures(bool mustBeTrue)
    {
      Contract.Ensures(mustBeTrue);
    }

    void IRewrittenMultipleInheritanceInterface.MultiplyDifferentlyOverriddenInterfaceRequires(bool mustBeTrue)
    {
      Contract.Requires(mustBeTrue);
    }

    void IRewrittenMultipleInheritanceInterface.MultiplyDifferentlyOverriddenInterfaceEnsures(bool mustBeTrue)
    {
      Contract.Ensures(mustBeTrue);
    }
  }

  [ContractClass(typeof(ContractForInterfaceBase))]
  public interface IRewrittenInheritanceInterfaceBase {
    void InterfaceBaseRequires(bool mustBeTrue);
    void InterfaceBaseEnsures(bool mustBeTrue);
  }

  [ContractClassFor(typeof(IRewrittenInheritanceInterfaceBase))]
  abstract class ContractForInterfaceBase : IRewrittenInheritanceInterfaceBase {
    void IRewrittenInheritanceInterfaceBase.InterfaceBaseRequires(bool mustBeTrue)
    {
      Contract.Requires(mustBeTrue);
    }

    void IRewrittenInheritanceInterfaceBase.InterfaceBaseEnsures(bool mustBeTrue)
    {
      Contract.Ensures(mustBeTrue);
    }
  }

  [ContractClass(typeof(ContractForInterfaceDerived1))]
  public interface IRewrittenInheritanceInterfaceDerived1 : IRewrittenInheritanceInterfaceBase {
    void InterfaceRequires(int value);
    void InterfaceEnsures(int value);
    void InterfaceRequiresMultipleOfTwo(int value);
    void InterfaceEnsuresMultipleOfTwo(int value);
  }

  [ContractClassFor(typeof(IRewrittenInheritanceInterfaceDerived1))]
  abstract class ContractForInterfaceDerived1 : IRewrittenInheritanceInterfaceDerived1 {
    void IRewrittenInheritanceInterfaceDerived1.InterfaceRequires(int value)
    {
      Contract.Requires(value % 2 == 0);
    }

    void IRewrittenInheritanceInterfaceDerived1.InterfaceEnsures(int value)
    {
      Contract.Ensures(value % 2 == 0);
    }

    void IRewrittenInheritanceInterfaceDerived1.InterfaceRequiresMultipleOfTwo(int value)
    {
      Contract.Requires(value % 2 == 0);
    }

    void IRewrittenInheritanceInterfaceDerived1.InterfaceEnsuresMultipleOfTwo(int value)
    {
      Contract.Ensures(value % 2 == 0);
    }

    void IRewrittenInheritanceInterfaceBase.InterfaceBaseRequires(bool mustBeTrue) { }

    void IRewrittenInheritanceInterfaceBase.InterfaceBaseEnsures(bool mustBeTrue) { }
  }

  [ContractClass(typeof(ContractForInterfaceDerived2))]
  public interface IRewrittenInheritanceInterfaceDerived2 : IRewrittenInheritanceInterfaceBase {
    void InterfaceRequires(int value);
    void InterfaceEnsures(int value);
    void InterfaceRequiresMultipleOfThree(int value);
    void InterfaceEnsuresMultipleOfThree(int value);
  }

  [ContractClassFor(typeof(IRewrittenInheritanceInterfaceDerived2))]
  abstract class ContractForInterfaceDerived2 : IRewrittenInheritanceInterfaceDerived2 {
    void IRewrittenInheritanceInterfaceDerived2.InterfaceRequires(int value)
    {
      Contract.Requires(value % 3 == 0);
    }

    void IRewrittenInheritanceInterfaceDerived2.InterfaceEnsures(int value)
    {
      Contract.Ensures(value % 3 == 0);
    }

    void IRewrittenInheritanceInterfaceDerived2.InterfaceRequiresMultipleOfThree(int value)
    {
      Contract.Requires(value % 3 == 0);
    }

    void IRewrittenInheritanceInterfaceDerived2.InterfaceEnsuresMultipleOfThree(int value)
    {
      Contract.Ensures(value % 3 == 0);
    }

    void IRewrittenInheritanceInterfaceBase.InterfaceBaseRequires(bool mustBeTrue) { }

    void IRewrittenInheritanceInterfaceBase.InterfaceBaseEnsures(bool mustBeTrue) { }
  }

  public class RewrittenInheritanceBase : IRewrittenInheritanceOverriddenInterface, IRewrittenMultipleInheritanceInterface {
    public bool BaseValueMustBeTrue = true;

    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(BaseValueMustBeTrue);
    }

    public virtual void LegacyRequires(string mustBeNonNull)
    {
#if LEGACY
      if (mustBeNonNull == null) throw new ArgumentNullException("mustBeNonNull");
      Contract.EndContractBlock();
#else
      Contract.Requires<ArgumentNullException>(mustBeNonNull != null, "mustBeNonNull");
#endif
    }

    public virtual void RequiresWithException(string mustBeNonNull)
    {
#if LEGACY
      if (mustBeNonNull == null) throw new ArgumentNullException("mustBeNonNull != null");
      Contract.EndContractBlock();
#else
      Contract.Requires<ArgumentNullException>(mustBeNonNull != null);
#endif
    }

    public virtual void BareRequires(bool mustBeTrue)
    {
      Contract.Requires(mustBeTrue);
    }

    public virtual void AdditiveRequires(int mustBePositive)
    {
      Contract.Requires(mustBePositive > 0);
    }

    public virtual void SubtractiveRequires(int mustBeAbove100)
    {
      Contract.Requires(mustBeAbove100 > 100);
    }

    public virtual void AdditiveEnsures(int mustBePositive)
    {
      Contract.Ensures(mustBePositive > 0);
    }

    public virtual void SubtractiveEnsures(int mustBeAbove100)
    {
      Contract.Ensures(mustBeAbove100 > 100);
    }

    public virtual void ImplicitlyOverriddenInterfaceRequires(bool mustBeTrue)
    {
    }

    public virtual void ImplicitlyOverriddenInterfaceEnsures(bool mustBeTrue)
    {
    }

    void IRewrittenInheritanceOverriddenInterface.ExplicitlyOverriddenInterfaceRequires(bool mustBeTrue)
    {
    }

    void IRewrittenInheritanceOverriddenInterface.ExplicitlyOverriddenInterfaceEnsures(bool mustBeTrue)
    {
    }

    public virtual void ExplicitlyOverriddenInterfaceRequires(bool mustBeTrue)
    {
    }

    public virtual void ExplicitlyOverriddenInterfaceEnsures(bool mustBeTrue)
    {
    }

    public virtual void MultiplyImplicitlyOverriddenInterfaceRequires(bool mustBeTrue)
    {
    }

    public virtual void MultiplyImplicitlyOverriddenInterfaceEnsures(bool mustBeTrue)
    {
    }

    void IRewrittenMultipleInheritanceInterface.MultiplyExplicitlyOverriddenInterfaceRequires(bool mustBeTrue)
    {
    }

    void IRewrittenMultipleInheritanceInterface.MultiplyExplicitlyOverriddenInterfaceEnsures(bool mustBeTrue)
    {
    }

    public virtual void MultiplyDifferentlyOverriddenInterfaceRequires(bool mustBeTrue)
    {
    }

    public virtual void MultiplyDifferentlyOverriddenInterfaceEnsures(bool mustBeTrue)
    {
    }
  }

  public class RewrittenInheritanceDerived : RewrittenInheritanceBase, IRewrittenInheritanceInterfaceDerived1, IRewrittenInheritanceInterfaceDerived2, IRewrittenMultipleInheritanceInterface {
    public bool InheritedValueMustBeTrue = true;

    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(InheritedValueMustBeTrue);
    }

    public void SetInheritedValueMustBeTrue(bool value)
    {
      this.InheritedValueMustBeTrue = value;
    }

    public void SetBaseValueMustBeTrue(bool value)
    {
      this.BaseValueMustBeTrue = value;
    }

    public override void LegacyRequires(string mustBeNonNull)
    {
#if LEGACY
      if (mustBeNonNull == null) throw new ArgumentNullException("mustBeNonNull");
      Contract.EndContractBlock();
#endif
    }

    public override void RequiresWithException(string mustBeNonNull)
    {
#if LEGACY
      if (mustBeNonNull == null) throw new ArgumentNullException("mustBeNonNull");
      Contract.EndContractBlock();
#endif
    }

    public override void BareRequires(bool mustBeTrue)
    {
    }

    public override void AdditiveRequires(int mustBePositive)
    {
      // test that preconditions are inherited
    }
    public override void AdditiveEnsures(int mustBeAbove100)
    {
      Contract.Ensures(mustBeAbove100 > 100);
    }

    public override void SubtractiveEnsures(int mustBePositive)
    {
      Contract.Ensures(mustBePositive > 0);
    }

    public void InterfaceBaseRequires(bool mustBeTrue)
    {
    }

    public void InterfaceBaseEnsures(bool mustBeTrue)
    {
    }

    public void InterfaceRequires(int value)
    {
    }

    public void InterfaceEnsures(int value)
    {
    }

    public void InterfaceRequiresMultipleOfTwo(int value)
    {
    }

    public void InterfaceEnsuresMultipleOfTwo(int value)
    {
    }

    void IRewrittenInheritanceInterfaceDerived2.InterfaceRequiresMultipleOfThree(int value)
    {
    }

    void IRewrittenInheritanceInterfaceDerived2.InterfaceEnsuresMultipleOfThree(int value)
    {
    }

    public void InterfaceRequiresMultipleOfThree(int value)
    {
    }

    public void InterfaceEnsuresMultipleOfThree(int value)
    {
    }

    public override void ImplicitlyOverriddenInterfaceRequires(bool mustBeTrue)
    {
    }

    public override void ImplicitlyOverriddenInterfaceEnsures(bool mustBeTrue)
    {
    }

    public override void ExplicitlyOverriddenInterfaceRequires(bool mustBeTrue)
    {
    }

    public override void ExplicitlyOverriddenInterfaceEnsures(bool mustBeTrue)
    {
    }
#if CrashTheWriterFixups
        public override void MultiplyImplicitlyOverriddenInterfaceRequires(bool mustBeTrue)
        {
        }

        public override void MultiplyImplicitlyOverriddenInterfaceEnsures(bool mustBeTrue)
        {
        }
#endif
    void IRewrittenMultipleInheritanceInterface.MultiplyExplicitlyOverriddenInterfaceRequires(bool mustBeTrue)
    {
    }

    void IRewrittenMultipleInheritanceInterface.MultiplyExplicitlyOverriddenInterfaceEnsures(bool mustBeTrue)
    {
    }

    void IRewrittenMultipleInheritanceInterface.MultiplyDifferentlyOverriddenInterfaceRequires(bool mustBeTrue)
    {
    }

    void IRewrittenMultipleInheritanceInterface.MultiplyDifferentlyOverriddenInterfaceEnsures(bool mustBeTrue)
    {
    }
  }

  [ContractClass(typeof(QuantifierTest_Contract_ImplicitImplementations))]
  public interface IQuantifierTest1 {
    bool ModifyArray(int[] xs, int x, bool shouldBeCorrect);
    int[] CopyArray(int[] xs, bool shouldBeCorrect);
  }
  [ContractClassFor(typeof(IQuantifierTest1))]
  abstract class QuantifierTest_Contract_ImplicitImplementations : IQuantifierTest1 {
    bool IQuantifierTest1.ModifyArray(int[] xs, int x, bool shouldBeCorrect)
    {
      Contract.Requires(xs != null && 0 < xs.Length);
      Contract.Ensures(Contract.Result<bool>() ==
                          Contract.ForAll<int>(xs, delegate(int i) { return i < x; })
                      );
      return default(bool);
    }
    int[] IQuantifierTest1.CopyArray(int[] xs, bool shouldBeCorrect)
    {
      Contract.Requires(xs != null && 0 < xs.Length);
      Contract.Ensures(Contract.Exists<int>(xs,
                              delegate(int i) { return xs[i] == Contract.Result<int[]>()[i]; }
          )
       );
      return default(int[]);
    }
  }
  public class QuantifierTest1 : IQuantifierTest1 {
    public bool ModifyArray(int[] ys, int x, bool shouldBeCorrect)
    {
      for (int i = 0; i < ys.Length; i++)
        ys[i] = x - 1;
      if (!shouldBeCorrect)
        ys[ys.Length - 1] = x;
      return true;
    }
    public int[] CopyArray(int[] xs, bool shouldBeCorrect)
    {
      // strengthen the postcondition
      Contract.Ensures(
        Contract.ForAll(0, xs.Length,
              delegate(int i) { return xs[i] == Contract.Result<int[]>()[i]; })
      );
      int[] res = new int[xs.Length];
      if (shouldBeCorrect)
      {
        for (int i = 0; i < xs.Length; i++)
          res[i] = xs[i];
      }
      else
      {
        int max = Int32.MinValue;
        foreach (int x in xs) if (max < x) max = x;
        for (int i = 0; i < xs.Length; i++)
          res[i] = max + 1;
      }
      return res;
    }
  }

  [ContractClass(typeof(QuantifierTest_Contract_ExplicitImplementations))]
  public interface IQuantifierTest2 {
    bool ModifyArray(int[] xs, int x, bool shouldBeCorrect);
    int[] CopyArray(int[] xs, bool shouldBeCorrect);
  }
  [ContractClassFor(typeof(IQuantifierTest2))]
  abstract class QuantifierTest_Contract_ExplicitImplementations : IQuantifierTest2 {
    bool IQuantifierTest2.ModifyArray(int[] xs, int x, bool shouldBeCorrect)
    {
      Contract.Requires(xs != null && 0 < xs.Length);
      Contract.Ensures(Contract.Result<bool>() ==
                          Contract.ForAll<int>(xs, delegate(int i) { return i < x; })
                      );
      return default(bool);
    }
    int[] IQuantifierTest2.CopyArray(int[] xs, bool shouldBeCorrect)
    {
      Contract.Requires(xs != null && 0 < xs.Length);
      Contract.Ensures(Contract.Exists<int>(xs,
                              delegate(int i) { return xs[i] == Contract.Result<int[]>()[i]; }
          )
       );
      return default(int[]);
    }
  }
  public class QuantifierTest2 : IQuantifierTest2 {
    public bool ModifyArray(int[] ys, int x, bool shouldBeCorrect)
    {
      for (int i = 0; i < ys.Length; i++)
        ys[i] = x - 1;
      if (!shouldBeCorrect)
        ys[ys.Length - 1] = x;
      return true;
    }
    public int[] CopyArray(int[] xs, bool shouldBeCorrect)
    {
      // strengthen the postcondition
      Contract.Ensures(
        Contract.ForAll(0, xs.Length,
              delegate(int i) { return xs[i] == Contract.Result<int[]>()[i]; })
      );
      int[] res = new int[xs.Length];
      if (shouldBeCorrect)
      {
        for (int i = 0; i < xs.Length; i++)
          res[i] = xs[i];
      }
      else
      {
        int max = Int32.MinValue;
        foreach (int x in xs) if (max < x) max = x;
        for (int i = 0; i < xs.Length; i++)
          res[i] = max + 1;
      }
      return res;
    }
  }

  [ContractClass(typeof(AbstractClassInterfaceContract))]
  public interface AbstractClassInterface {
    string M(bool shouldBeCorrect);
  }
  [ContractClassFor(typeof(AbstractClassInterface))]
  abstract class AbstractClassInterfaceContract : AbstractClassInterface {
    string AbstractClassInterface.M(bool shouldBeCorrect)
    {
      Contract.Ensures(!string.IsNullOrEmpty(Contract.Result<string>()));
      return default(string);
    }
  }
  public abstract class AbstractClassInterfaceImplementation : AbstractClassInterface {
    public abstract string M(bool shouldBeCorrect);
  }
  public class AbstractClassInterfaceImplementationSubType : AbstractClassInterfaceImplementation {
    public override string M(bool shouldBeCorrect)
    {
      return shouldBeCorrect ? "abc" : "";
    }
  }

  public class BaseClassWithMethodCallInContract {
    public virtual int P { get { return 0; } }
    public virtual int M(int x)
    {
      Contract.Requires(P < x);
      return 3;
    }
  }
  public class DerivedClassForBaseClassWithMethodCallInContract : BaseClassWithMethodCallInContract {
    public override int M(int y)
    {
      return 27;
    }
  }
  public class BaseClassWithOldAndResult {
    public virtual void InheritOld(ref int x, bool shouldBeCorrect)
    {
      Contract.Requires(1 <= x);
      Contract.Ensures(Contract.OldValue(x) < x);
      if (shouldBeCorrect) x++;
      return;
    }
    public virtual void InheritOldInQuantifier(int[] xs, bool shouldBeCorrect)
    {
      Contract.Requires(0 < xs.Length);
      Contract.Requires(Contract.ForAll(xs, delegate(int x) { return 0 < x; }));
      Contract.Ensures(Contract.ForAll(0, xs.Length, delegate(int i) { return xs[i] < Contract.OldValue(xs[i]); }));
      if (shouldBeCorrect)
        for (int i = 0; i < xs.Length; i++) xs[i] = xs[i] - 1;
      return;
    }
    public virtual int InheritResult(int x, bool shouldBeCorrect)
    {
      Contract.Ensures(Contract.Result<int>() == x);
      return shouldBeCorrect ? x : x + 1;
    }
    public virtual int InheritResultInQuantifier(int[] xs, bool shouldBeCorrect)
    {
      Contract.Requires(0 < xs.Length);
      Contract.Ensures(Contract.ForAll(0, xs.Length, delegate(int i) { return xs[i] < Contract.Result<int>(); }));
      for (int i = 0; i < xs.Length; i++)
        xs[i] = 3;
      return shouldBeCorrect ? 27 : 3;
    }
  }
  public class DerivedFromBaseClassWithOldAndResult : BaseClassWithOldAndResult {
    public override void InheritOld(ref int y, bool shouldBeCorrect)
    {
      if (shouldBeCorrect) y = y * 2;
      return;
    }
    public override void InheritOldInQuantifier(int[] ys, bool shouldBeCorrect)
    {
      if (shouldBeCorrect)
        for (int i = 0; i < ys.Length; i++) ys[i] = ys[i] - 3;
      return;
    }
    public override int InheritResult(int x, bool shouldBeCorrect)
    {
      return shouldBeCorrect ? x : x + 1;
    }
    public override int InheritResultInQuantifier(int[] ys, bool shouldBeCorrect)
    {
      for (int i = 0; i < ys.Length; i++)
        ys[i] = 0;
      return shouldBeCorrect ? 27 : 0;
    }
  }

  public class NonGenericBaseWithInvariant {
    protected bool succeed = true;
    [ContractInvariantMethod]
    private void Invariant()
    {
      Contract.Invariant(succeed);
    }
  }

  public class GenericWithInheritedInvariant<T> : NonGenericBaseWithInvariant {
  }

  public class NonGenericDerivedFromGenericWithInvariant : GenericWithInheritedInvariant<string> {
    public NonGenericDerivedFromGenericWithInvariant(bool succeed)
    {
      this.succeed = succeed;
    }
  }

  public class ExplicitGenericInterfaceMethodImplementation : IInterfaceWithGenericMethod {
    int mode;

    public ExplicitGenericInterfaceMethodImplementation(int mode)
    {
      this.mode = mode;
    }

    IEnumerable<T> IInterfaceWithGenericMethod.Bar<T>()
    {
      switch (mode)
      {
        case 0:
          return new T[0];

        case 1:
          return new T[1];

        default:
          return null;
      }
    }
  }

  public class ImplicitGenericInterfaceMethodImplementation : IInterfaceWithGenericMethod {
    int mode;

    public ImplicitGenericInterfaceMethodImplementation(int mode)
    {
      this.mode = mode;
    }

    public IEnumerable<T> Bar<T>()
    {
      switch (mode)
      {
        case 0:
          return new T[0];

        case 1:
          return new T[1];

        default:
          return null;
      }
    }
  }

  [ContractClass(typeof(InterfaceWithGenericMethodContract))]
  public interface IInterfaceWithGenericMethod {
    IEnumerable<T> Bar<T>();
  }

  [ContractClassFor(typeof(IInterfaceWithGenericMethod))]
  abstract class InterfaceWithGenericMethodContract : IInterfaceWithGenericMethod {
    IEnumerable<T> IInterfaceWithGenericMethod.Bar<T>()
    {
      Contract.Ensures(Contract.Result<IEnumerable<T>>() != null);
      Contract.Ensures(Contract.ForAll(Contract.Result<IEnumerable<T>>(), x => x != null));
      return default(IEnumerable<T>);
    }
  }

  #region Interface with method call in contract
  [ContractClass(typeof(ContractForIFaceWithMethodCallInContract))]
  public interface IFaceWithMethodCallInContract {
    bool M(int x);
    bool P { get; }
  }

  [ContractClassFor(typeof(IFaceWithMethodCallInContract))]
  abstract class ContractForIFaceWithMethodCallInContract : IFaceWithMethodCallInContract {
    bool IFaceWithMethodCallInContract.M(int x)
    {
      IFaceWithMethodCallInContract iFaceReference = this;
      Contract.Requires(x != 3);
      Contract.Requires(iFaceReference.P);
      Contract.Ensures(x != 5 || !iFaceReference.P);
      return default(bool);
    }
    bool IFaceWithMethodCallInContract.P
    {
      get
      {
        Contract.Ensures(Contract.Result<bool>());
        return default(bool);
      }
    }
  }
  public class ImplForIFaceWithMethodCallInContract : IFaceWithMethodCallInContract {
    public virtual bool M(int x)
    {
      return true;
    }
    public virtual bool P
    {
      get { return true; }
    }
  }

  #endregion Interface with method call in contract

  #region ContractClass for abstract methods
  [ContractClass(typeof(AbstractClassContracts))]
  public abstract class AbstractClass {
    public abstract int ReturnFirst(int[] args, bool behave);
    public virtual int Increment(int x, bool behave)
    {
      Contract.Requires(0 < x);
      Contract.Ensures(Contract.Result<int>() >= x + 1);
      return x + 1;
    }
  }

  [ContractClassFor(typeof(AbstractClass))]
  internal abstract class AbstractClassContracts : AbstractClass {
    public override int ReturnFirst(int[] args, bool behave)
    {
      Contract.Requires(args != null);
      Contract.Requires(args.Length > 0);
      Contract.Ensures(Contract.Result<int>() == args[0]);
      return default(int);
    }
  }

  [ContractClass(typeof(GenericAbstractClassContracts<,>))]
  public abstract class GenericAbstractClass<A, B> where A : class,B {
    [Pure]
    public abstract bool IsMatch(B b, A a);

    public abstract B ReturnFirst(B[] args, A match, bool behave);

    public abstract A[][] Collection(int x, int y);

    public abstract A FirstNonNullMatch(A[] elems);

    public abstract C[] GenericMethod<C>(A[] elems);
  }

  [ContractClassFor(typeof(GenericAbstractClass<,>))]
  internal abstract class GenericAbstractClassContracts<A, B> : GenericAbstractClass<A, B>
    where A : class, B {
    public override bool IsMatch(B b, A a)
    {
      throw new NotImplementedException();
    }

    public override B ReturnFirst(B[] args, A match, bool behave)
    {
      Contract.Requires(args != null);
      Contract.Requires(args.Length > 0);
      Contract.Ensures(Contract.Exists(0, args.Length, i => args[i].Equals(Contract.Result<B>()) && IsMatch(args[i], match)));
      return default(B);
    }

    public override A[][] Collection(int x, int y)
    {
      Contract.Ensures(Contract.ForAll(Contract.Result<A[][]>(), nested => nested != null && nested.Length == y && Contract.ForAll(nested, elem => elem != null)));
      Contract.Ensures(Contract.ForAll(0, x, index => Contract.Result<A[][]>()[index] != null));
      throw new NotImplementedException();
    }

    public override A FirstNonNullMatch(A[] elems)
    {
      // meaningless, but testing our closures, in particular inner one with a static closure referring to result.
      Contract.Ensures(Contract.Exists(0, elems.Length, index => elems[index] != null && elems[index] == Contract.Result<A>() &&
                                          Contract.ForAll(0, index, prior => Contract.Result<A>() != null)));
      // See if we are properly sharing fields.
      Contract.Ensures(Contract.Exists(0, elems.Length, index => elems[index] != null && elems[index] == Contract.Result<A>() &&
                                          Contract.ForAll(0, index, prior => Contract.Result<A>() != null)));
      // See if we are properly sharing fields.
      Contract.Ensures(Contract.Exists(0, elems.Length, index => elems[index] != null &&
                                          Contract.ForAll(0, index, prior => Contract.Result<A>() != null)));

      throw new NotImplementedException();
    }

    public override C[] GenericMethod<C>(A[] elems)
    {
      Contract.Requires(elems != null);
      Contract.Ensures(Contract.Result<C[]>() != null);
      Contract.Ensures(Contract.ForAll(Contract.Result<C[]>(), resultElem => Contract.Exists(elems, orig => resultElem.Equals(orig))));


      throw new NotImplementedException();
    }
  }

  public class ImplForAbstractMethod : AbstractClass {
    public override int ReturnFirst(int[] args, bool behave)
    {
      if (behave)
        return args[0];
      else
        return args[0] + 1;
    }
    public override int Increment(int x, bool behave)
    {
      Contract.Ensures(Contract.Result<int>() == x + 2);
      return behave ? (x + 2) : base.Increment(x, behave);
    }
  }

  public class ImplForGenericAbstractClass : GenericAbstractClass<string, string> {
    public override bool IsMatch(string b, string a)
    {
      return b == a;
    }

    public override string ReturnFirst(string[] args, string match, bool behave)
    {
      for (int i = 0; i < args.Length; i++)
      {
        if (IsMatch(args[i], match)) return args[i];
      }
      return default(string);
    }

    public override string[][] Collection(int x, int y)
    {
      var result = new string[x][];
      for (int i = 0; i < result.Length; i++)
      {
        result[i] = new string[y];
        for (int j = 0; j < y; j++)
        {
          if (x == 5 && y == 5 && i == 4 && j == 4)
          {
            // behave badly
            continue;
          }
          result[i][j] = "Foo";
        }
      }
      return result;
    }

    public override string FirstNonNullMatch(string[] elems)
    {
      for (int i = 0; i < elems.Length; i++)
      {
        if (elems[i] != null) return elems[i];
      }
      return null;
    }

    public override C[] GenericMethod<C>(string[] elems)
    {
      List<C> result = new List<C>();

      foreach (var elem in elems)
      {
        if (elem is C)
        {
          result.Add((C)(object)elem);
        }
      }
      if (typeof(C) == typeof(int))
      {
        // behave badly
        result.Add((C)(object)55);
      }
      return result.ToArray();
    }
  }

  #endregion ContractClass for abstract methods

  #region Make sure stelem is properly specialized
  public delegate bool D<T>(T t);

  public class BaseWithClosureWithStelem<T> {
    public bool Foo(D<T> action, T data)
    {
      action(data);
      action(data);
      return true;
    }

    public virtual void M(T[] ts, int i)
    {
      Contract.Requires(ts != null);
      Contract.Requires(i + 1 < ts.Length);
      Contract.Ensures(ts[i].Equals(ts[0]));
      Contract.Ensures(ts[i + 1].Equals(ts[0]));

      int index = i;
      Foo(delegate(T t) { ts[index++] = t; return true; }, ts[0]);
    }
  }

  public class DerivedOfClosureWithStelem<V> : BaseWithClosureWithStelem<V> {
    public override void M(V[] vs, int j)
    {
      base.M(vs, j);
    }
  }

  #endregion
  #endregion

    #region Contracts
    public class C {
      public int P { get { return _p; } }
      [ContractPublicPropertyName("P")]
      private int _p;
      public virtual int foo(int x)
      {
        Contract.Requires(x < _p);
        return 3;
      }
      public C()
      {
        _p = 0;
      }
    }

    public class D : C {
      public override int foo(int x)
      {
        return 27;
      }
    }

    public class UsingVirtualProperty {
      public virtual int P { get { return _p; } }
      [ContractPublicPropertyName("P")]
      private int _p;
      public virtual int foo(int x)
      {
        Contract.Requires(x < _p);
        return 3;
      }
      public UsingVirtualProperty()
      {
        _p = 0;
      }
    }

    public class SubtypeOfUsingVirtualProperty : UsingVirtualProperty {
      public override int P { get { return 100; } }
      public override int foo(int x)
      {
        return 27;
      }
    }
    #endregion Contracts
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
    #region I Membersbu

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
    bool behave;
    public C(bool behave)
    {
      this.behave = behave;
    }

    public bool P { get { return behave; } }

    public void S()
    {

      Contract.Assert(((B)this).P);
    }

  }
}

namespace CodeUnderTest.ThomasPani
{
  public class T
  {
    protected int i = 100;

    [ContractInvariantMethod]
    private void Invariant()
    {
      Contract.Invariant(i >= 0); // i non-negative
    }

    public void SetToValidValue(bool behave)
    {
      if (behave)
      {
        i = 1;
      } else
      {
        i = 0;
      }
    }
  }

  public class U : T
  {
    [ContractInvariantMethod]
    private void Invariant()
    {
      Contract.Invariant(i > 0);  // i positive
    }
  }

}

