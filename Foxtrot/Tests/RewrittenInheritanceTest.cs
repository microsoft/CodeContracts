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

#define CONTRACTS_PRECONDITIONS
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Text;
using System.Collections.Generic;

using System.Diagnostics.Contracts;

namespace Tests {
  #region Contracts
  [ContractClass(typeof(ContractForOverriddenInterface))]
  interface IRewrittenInheritanceOverriddenInterface {
    void ImplicitlyOverriddenInterfaceRequires (bool mustBeTrue);
    void ImplicitlyOverriddenInterfaceEnsures (bool mustBeTrue);
    void ExplicitlyOverriddenInterfaceRequires (bool mustBeTrue);
    void ExplicitlyOverriddenInterfaceEnsures (bool mustBeTrue);
  }

  [ContractClassFor(typeof(IRewrittenInheritanceOverriddenInterface))]
  class ContractForOverriddenInterface : IRewrittenInheritanceOverriddenInterface {
    void IRewrittenInheritanceOverriddenInterface.ImplicitlyOverriddenInterfaceRequires (bool mustBeTrue) {
      Contract.Requires(mustBeTrue);
    }

    void IRewrittenInheritanceOverriddenInterface.ImplicitlyOverriddenInterfaceEnsures (bool mustBeTrue) {
      Contract.Ensures(mustBeTrue);
    }

    public void ExplicitlyOverriddenInterfaceRequires (bool mustBeTrue) {
      Contract.Requires(mustBeTrue);
    }

    public void ExplicitlyOverriddenInterfaceEnsures (bool mustBeTrue) {
      Contract.Ensures(mustBeTrue);
    }
  }

  [ContractClass(typeof(ContractForMultipleInheritanceInterface))]
  public interface IRewrittenMultipleInheritanceInterface {
    void MultiplyImplicitlyOverriddenInterfaceRequires (bool mustBeTrue);
    void MultiplyImplicitlyOverriddenInterfaceEnsures (bool mustBeTrue);
    void MultiplyExplicitlyOverriddenInterfaceRequires (bool mustBeTrue);
    void MultiplyExplicitlyOverriddenInterfaceEnsures (bool mustBeTrue);
    void MultiplyDifferentlyOverriddenInterfaceRequires (bool mustBeTrue);
    void MultiplyDifferentlyOverriddenInterfaceEnsures (bool mustBeTrue);
  }

  [ContractClassFor(typeof(IRewrittenMultipleInheritanceInterface))]
  class ContractForMultipleInheritanceInterface : IRewrittenMultipleInheritanceInterface {
    void IRewrittenMultipleInheritanceInterface.MultiplyImplicitlyOverriddenInterfaceRequires(bool mustBeTrue) {
      Contract.Requires(mustBeTrue);
    }

    void IRewrittenMultipleInheritanceInterface.MultiplyImplicitlyOverriddenInterfaceEnsures(bool mustBeTrue) {
      Contract.Ensures(mustBeTrue);
    }

    void IRewrittenMultipleInheritanceInterface.MultiplyExplicitlyOverriddenInterfaceRequires(bool mustBeTrue) {
      Contract.Requires(mustBeTrue);
    }

    void IRewrittenMultipleInheritanceInterface.MultiplyExplicitlyOverriddenInterfaceEnsures(bool mustBeTrue) {
      Contract.Ensures(mustBeTrue);
    }

    void IRewrittenMultipleInheritanceInterface.MultiplyDifferentlyOverriddenInterfaceRequires(bool mustBeTrue) {
      Contract.Requires(mustBeTrue);
    }

    void IRewrittenMultipleInheritanceInterface.MultiplyDifferentlyOverriddenInterfaceEnsures(bool mustBeTrue) {
      Contract.Ensures(mustBeTrue);
    }
  }

  [ContractClass(typeof(ContractForInterfaceBase))]
  public interface IRewrittenInheritanceInterfaceBase {
    void InterfaceBaseRequires (bool mustBeTrue);
    void InterfaceBaseEnsures (bool mustBeTrue);
  }

  [ContractClassFor(typeof(IRewrittenInheritanceInterfaceBase))]
  class ContractForInterfaceBase : IRewrittenInheritanceInterfaceBase {
    void IRewrittenInheritanceInterfaceBase.InterfaceBaseRequires(bool mustBeTrue) {
      Contract.Requires(mustBeTrue);
    }

    void IRewrittenInheritanceInterfaceBase.InterfaceBaseEnsures(bool mustBeTrue) {
      Contract.Ensures(mustBeTrue);
    }
  }

  [ContractClass(typeof(ContractForInterfaceDerived1))]
  public interface IRewrittenInheritanceInterfaceDerived1 : IRewrittenInheritanceInterfaceBase {
    void InterfaceRequires (int value);
    void InterfaceEnsures (int value);
    void InterfaceRequiresMultipleOfTwo (int value);
    void InterfaceEnsuresMultipleOfTwo (int value);
  }

  [ContractClassFor(typeof(IRewrittenInheritanceInterfaceDerived1))]
  class ContractForInterfaceDerived1 : IRewrittenInheritanceInterfaceDerived1 {
    void IRewrittenInheritanceInterfaceDerived1.InterfaceRequires(int value) {
      Contract.Requires(value % 2 == 0);
    }

    void IRewrittenInheritanceInterfaceDerived1.InterfaceEnsures(int value) {
      Contract.Ensures(value % 2 == 0);
    }

    void IRewrittenInheritanceInterfaceDerived1.InterfaceRequiresMultipleOfTwo(int value) {
      Contract.Requires(value % 2 == 0);
    }

    void IRewrittenInheritanceInterfaceDerived1.InterfaceEnsuresMultipleOfTwo(int value) {
      Contract.Ensures(value % 2 == 0);
    }

    void IRewrittenInheritanceInterfaceBase.InterfaceBaseRequires(bool mustBeTrue) { }

    void IRewrittenInheritanceInterfaceBase.InterfaceBaseEnsures(bool mustBeTrue) { }
  }

  [ContractClass(typeof(ContractForInterfaceDerived2))]
  public interface IRewrittenInheritanceInterfaceDerived2 : IRewrittenInheritanceInterfaceBase {
    void InterfaceRequires (int value);
    void InterfaceEnsures (int value);
    void InterfaceRequiresMultipleOfThree (int value);
    void InterfaceEnsuresMultipleOfThree (int value);
  }

  [ContractClassFor(typeof(IRewrittenInheritanceInterfaceDerived2))]
  class ContractForInterfaceDerived2 : IRewrittenInheritanceInterfaceDerived2 {
    void IRewrittenInheritanceInterfaceDerived2.InterfaceRequires(int value) {
      Contract.Requires(value % 3 == 0);
    }

    void IRewrittenInheritanceInterfaceDerived2.InterfaceEnsures(int value) {
      Contract.Ensures(value % 3 == 0);
    }

    void IRewrittenInheritanceInterfaceDerived2.InterfaceRequiresMultipleOfThree(int value) {
      Contract.Requires(value % 3 == 0);
    }

    void IRewrittenInheritanceInterfaceDerived2.InterfaceEnsuresMultipleOfThree(int value) {
      Contract.Ensures(value % 3 == 0);
    }

    void IRewrittenInheritanceInterfaceBase.InterfaceBaseRequires(bool mustBeTrue) { }

    void IRewrittenInheritanceInterfaceBase.InterfaceBaseEnsures(bool mustBeTrue) { }
  }

  public class RewrittenInheritanceBase : DisableAssertUI, IRewrittenInheritanceOverriddenInterface, IRewrittenMultipleInheritanceInterface {
    public bool BaseValueMustBeTrue = true;

    [ContractInvariantMethod]
    private void ObjectInvariant () {
      Contract.Invariant(BaseValueMustBeTrue);
    }

    public virtual void LegacyRequires(string mustBeNonNull)
    {
      if (mustBeNonNull == null) throw new ArgumentNullException("mustBeNonNull");
      Contract.EndContractBlock();
    }

    public virtual void RequiresWithException(string mustBeNonNull)
    {
      Contract.Requires<ArgumentNullException>(mustBeNonNull != null);

    }

    public virtual void BareRequires (bool mustBeTrue) {
      Contract.Requires(mustBeTrue);
    }

    public virtual void AdditiveRequires (int mustBePositive) {
      Contract.Requires(mustBePositive > 0);
    }

    public virtual void SubtractiveRequires (int mustBeAbove100) {
      Contract.Requires(mustBeAbove100 > 100);
    }

    public virtual void AdditiveEnsures (int mustBePositive) {
      Contract.Ensures(mustBePositive > 0);
    }

    public virtual void SubtractiveEnsures (int mustBeAbove100) {
      Contract.Ensures(mustBeAbove100 > 100);
    }

    public virtual void ImplicitlyOverriddenInterfaceRequires (bool mustBeTrue) {
    }

    public virtual void ImplicitlyOverriddenInterfaceEnsures (bool mustBeTrue) {
    }

    void IRewrittenInheritanceOverriddenInterface.ExplicitlyOverriddenInterfaceRequires (bool mustBeTrue) {
    }

    void IRewrittenInheritanceOverriddenInterface.ExplicitlyOverriddenInterfaceEnsures (bool mustBeTrue) {
    }

    public virtual void ExplicitlyOverriddenInterfaceRequires (bool mustBeTrue) {
    }

    public virtual void ExplicitlyOverriddenInterfaceEnsures (bool mustBeTrue) {
    }

    public virtual void MultiplyImplicitlyOverriddenInterfaceRequires (bool mustBeTrue) {
    }

    public virtual void MultiplyImplicitlyOverriddenInterfaceEnsures (bool mustBeTrue) {
    }

    void IRewrittenMultipleInheritanceInterface.MultiplyExplicitlyOverriddenInterfaceRequires (bool mustBeTrue) {
    }

    void IRewrittenMultipleInheritanceInterface.MultiplyExplicitlyOverriddenInterfaceEnsures (bool mustBeTrue) {
    }

    public virtual void MultiplyDifferentlyOverriddenInterfaceRequires (bool mustBeTrue) {
    }

    public virtual void MultiplyDifferentlyOverriddenInterfaceEnsures (bool mustBeTrue) {
    }
  }

  public class RewrittenInheritanceDerived : RewrittenInheritanceBase, IRewrittenInheritanceInterfaceDerived1, IRewrittenInheritanceInterfaceDerived2, IRewrittenMultipleInheritanceInterface {
    public bool InheritedValueMustBeTrue = true;

    [ContractInvariantMethod]
    private void ObjectInvariant () {
      Contract.Invariant(InheritedValueMustBeTrue);
    }

    public override void LegacyRequires(string mustBeNonNull)
    {
      
    }

    public override void RequiresWithException(string mustBeNonNull)
    {
      
    }

    public override void BareRequires (bool mustBeTrue) {
    }

    public override void AdditiveRequires (int mustBePositive) {
      // test that preconditions are inherited
    }
    public override void AdditiveEnsures (int mustBeAbove100) {
      Contract.Ensures(mustBeAbove100 > 100);
    }

    public override void SubtractiveEnsures (int mustBePositive) {
      Contract.Ensures(mustBePositive > 0);
    }

    public void InterfaceBaseRequires (bool mustBeTrue) {
    }

    public void InterfaceBaseEnsures (bool mustBeTrue) {
    }

    public void InterfaceRequires (int value) {
    }

    public void InterfaceEnsures (int value) {
    }

    public void InterfaceRequiresMultipleOfTwo (int value) {
    }

    public void InterfaceEnsuresMultipleOfTwo (int value) {
    }

    void IRewrittenInheritanceInterfaceDerived2.InterfaceRequiresMultipleOfThree (int value) {
    }

    void IRewrittenInheritanceInterfaceDerived2.InterfaceEnsuresMultipleOfThree (int value) {
    }

    public void InterfaceRequiresMultipleOfThree (int value) {
    }

    public void InterfaceEnsuresMultipleOfThree (int value) {
    }

    public override void ImplicitlyOverriddenInterfaceRequires (bool mustBeTrue) {
    }

    public override void ImplicitlyOverriddenInterfaceEnsures (bool mustBeTrue) {
    }

    public override void ExplicitlyOverriddenInterfaceRequires (bool mustBeTrue) {
    }

    public override void ExplicitlyOverriddenInterfaceEnsures (bool mustBeTrue) {
    }
#if CrashTheWriterFixups
        public override void MultiplyImplicitlyOverriddenInterfaceRequires(bool mustBeTrue)
        {
        }

        public override void MultiplyImplicitlyOverriddenInterfaceEnsures(bool mustBeTrue)
        {
        }
#endif
    void IRewrittenMultipleInheritanceInterface.MultiplyExplicitlyOverriddenInterfaceRequires (bool mustBeTrue) {
    }

    void IRewrittenMultipleInheritanceInterface.MultiplyExplicitlyOverriddenInterfaceEnsures (bool mustBeTrue) {
    }

    void IRewrittenMultipleInheritanceInterface.MultiplyDifferentlyOverriddenInterfaceRequires (bool mustBeTrue) {
    }

    void IRewrittenMultipleInheritanceInterface.MultiplyDifferentlyOverriddenInterfaceEnsures (bool mustBeTrue) {
    }
  }

  [ContractClass(typeof(QuantifierTest_Contract_ImplicitImplementations))]
  public interface IQuantifierTest1 {
    bool ModifyArray (int[] xs, int x, bool shouldBeCorrect);
    int[] CopyArray (int[] xs, bool shouldBeCorrect);
  }
  [ContractClassFor(typeof(IQuantifierTest1))]
  public class QuantifierTest_Contract_ImplicitImplementations : IQuantifierTest1 {
    bool IQuantifierTest1.ModifyArray(int[] xs, int x, bool shouldBeCorrect) {
      Contract.Requires(xs != null && 0 < xs.Length);
      Contract.Ensures(Contract.Result<bool>() ==
                          Contract.ForAll<int>(xs, delegate(int i) { return i < x; })
                      );
      return default(bool);
    }
    int[] IQuantifierTest1.CopyArray(int[] xs, bool shouldBeCorrect) {
      Contract.Requires(xs != null && 0 < xs.Length);
      Contract.Ensures(Contract.Exists<int>(xs,
                              delegate(int i) { return xs[i] == Contract.Result<int[]>()[i]; }
          )
       );
      return default(int[]);
    }
  }
  public class QuantifierTest1 : IQuantifierTest1 {
    public bool ModifyArray (int[] ys, int x, bool shouldBeCorrect) {
      for (int i = 0; i < ys.Length; i++)
        ys[i] = x - 1;
      if (!shouldBeCorrect)
        ys[ys.Length - 1] = x;
      return true;
    }
    public int[] CopyArray (int[] xs, bool shouldBeCorrect) {
      // strengthen the postcondition
      Contract.Ensures(
        Contract.ForAll(0, xs.Length,
              delegate(int i) { return xs[i] == Contract.Result<int[]>()[i]; })
      );
      int[] res = new int[xs.Length];
      if (shouldBeCorrect) {
        for (int i = 0; i < xs.Length; i++)
          res[i] = xs[i];
      } else {
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
    bool ModifyArray (int[] xs, int x, bool shouldBeCorrect);
    int[] CopyArray (int[] xs, bool shouldBeCorrect);
  }
  [ContractClassFor(typeof(IQuantifierTest2))]
  public class QuantifierTest_Contract_ExplicitImplementations : IQuantifierTest2 {
    bool IQuantifierTest2.ModifyArray (int[] xs, int x, bool shouldBeCorrect) {
      Contract.Requires(xs != null && 0 < xs.Length);
      Contract.Ensures(Contract.Result<bool>() ==
                          Contract.ForAll<int>(xs, delegate(int i) { return i < x; })
                      );
      return default(bool);
    }
    int[] IQuantifierTest2.CopyArray (int[] xs, bool shouldBeCorrect) {
      Contract.Requires(xs != null && 0 < xs.Length);
      Contract.Ensures(Contract.Exists<int>(xs,
                              delegate(int i) { return xs[i] == Contract.Result<int[]>()[i]; }
          )
       );
      return default(int[]);
    }
  }
  public class QuantifierTest2 : IQuantifierTest2 {
    public bool ModifyArray (int[] ys, int x, bool shouldBeCorrect) {
      for (int i = 0; i < ys.Length; i++)
        ys[i] = x - 1;
      if (!shouldBeCorrect)
        ys[ys.Length - 1] = x;
      return true;
    }
    public int[] CopyArray (int[] xs, bool shouldBeCorrect) {
      // strengthen the postcondition
      Contract.Ensures(
        Contract.ForAll(0, xs.Length,
              delegate(int i) { return xs[i] == Contract.Result<int[]>()[i]; })
      );
      int[] res = new int[xs.Length];
      if (shouldBeCorrect) {
        for (int i = 0; i < xs.Length; i++)
          res[i] = xs[i];
      } else {
        int max = Int32.MinValue;
        foreach (int x in xs) if (max < x) max = x;
        for (int i = 0; i < xs.Length; i++)
          res[i] = max + 1;
      }
      return res;
    }
  }

  [ContractClass(typeof(AbstractClassInterfaceContract))]
  interface AbstractClassInterface {
    string M (bool shouldBeCorrect);
  }
  [ContractClassFor(typeof(AbstractClassInterface))]
  public class AbstractClassInterfaceContract : AbstractClassInterface {
    string AbstractClassInterface.M(bool shouldBeCorrect) {
      Contract.Ensures(!string.IsNullOrEmpty(Contract.Result<string>()));
      return default(string);
    }
  }
  public abstract class AbstractClassInterfaceImplementation : AbstractClassInterface {
    public abstract string M (bool shouldBeCorrect);
  }
  public class AbstractClassInterfaceImplementationSubType : AbstractClassInterfaceImplementation {
    public override string M (bool shouldBeCorrect) {
      return shouldBeCorrect ? "abc" : "";
    }
  }

  public class BaseClassWithMethodCallInContract {
    public virtual int P { get { return 0; } }
    public virtual int M (int x) {
      Contract.Requires(P < x);
      return 3;
    }
  }
  public class DerivedClassForBaseClassWithMethodCallInContract : BaseClassWithMethodCallInContract {
    public override int M (int y) {
      return 27;
    }
  }
  public class BaseClassWithOldAndResult {
    public virtual void InheritOld (ref int x, bool shouldBeCorrect) {
      Contract.Requires(1 <= x);
      Contract.Ensures(Contract.OldValue(x) < x);
      if (shouldBeCorrect) x++;
      return;
    }
    public virtual void InheritOldInQuantifier (int[] xs, bool shouldBeCorrect) {
      Contract.Requires(0 < xs.Length);
      Contract.Requires(Contract.ForAll(xs, delegate(int x) { return 0 < x; }));
      Contract.Ensures(Contract.ForAll(0, xs.Length, delegate(int i) { return xs[i] < Contract.OldValue(xs[i]); }));
      if (shouldBeCorrect)
        for (int i = 0; i < xs.Length; i++) xs[i] = xs[i] - 1;
      return;
    }
    public virtual int InheritResult (int x, bool shouldBeCorrect) {
      Contract.Ensures(Contract.Result<int>() == x);
      return shouldBeCorrect ? x : x + 1;
    }
    public virtual int InheritResultInQuantifier (int[] xs, bool shouldBeCorrect) {
      Contract.Requires(0 < xs.Length);
      Contract.Ensures(Contract.ForAll(0, xs.Length, delegate(int i) { return xs[i] < Contract.Result<int>(); }));
      for (int i = 0; i < xs.Length; i++)
        xs[i] = 3;
      return shouldBeCorrect ? 27 : 3;
    }
  }
  public class DerivedFromBaseClassWithOldAndResult : BaseClassWithOldAndResult {
    public override void InheritOld (ref int y, bool shouldBeCorrect) {
      if (shouldBeCorrect) y = y * 2;
      return;
    }
    public override void InheritOldInQuantifier (int[] ys, bool shouldBeCorrect) {
      if (shouldBeCorrect)
        for (int i = 0; i < ys.Length; i++) ys[i] = ys[i] - 3;
      return;
    }
    public override int InheritResult (int x, bool shouldBeCorrect) {
      return shouldBeCorrect ? x : x + 1;
    }
    public override int InheritResultInQuantifier (int[] ys, bool shouldBeCorrect) {
      for (int i = 0; i < ys.Length; i++)
        ys[i] = 0;
      return shouldBeCorrect ? 27 : 0;
    }
  }

  public class NonGenericBaseWithInvariant
  {
    protected bool succeed = true;
    [ContractInvariantMethod]
    private void Invariant()
    {
      Contract.Invariant(succeed);
    }
  }

  public class GenericWithInheritedInvariant<T> : NonGenericBaseWithInvariant
  {
  }

  public class NonGenericDerivedFromGenericWithInvariant : GenericWithInheritedInvariant<string>
  {
    public NonGenericDerivedFromGenericWithInvariant(bool succeed)
    {
      this.succeed = succeed;
    }
  }

  public class ExplicitGenericInterfaceMethodImplementation : IInterfaceWithGenericMethod
  {
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

  public class ImplicitGenericInterfaceMethodImplementation : IInterfaceWithGenericMethod
  {
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
  public interface IInterfaceWithGenericMethod
  {
    IEnumerable<T> Bar<T>();
  }

  [ContractClassFor(typeof(IInterfaceWithGenericMethod))]
  public abstract class InterfaceWithGenericMethodContract : IInterfaceWithGenericMethod
  {
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
  class ContractForIFaceWithMethodCallInContract : IFaceWithMethodCallInContract {
    bool IFaceWithMethodCallInContract.M(int x) {
      IFaceWithMethodCallInContract iFaceReference = this;
      Contract.Requires(x != 3);
      Contract.Requires(iFaceReference.P);
      Contract.Ensures(x != 5 || !iFaceReference.P);
      return default(bool);
    }
    bool IFaceWithMethodCallInContract.P {
      get {
        Contract.Ensures(Contract.Result<bool>());
        return default(bool);
      }
    }
  }
  class ImplForIFaceWithMethodCallInContract : IFaceWithMethodCallInContract {
    public virtual bool M(int x) {
      return true;
    }
    public virtual bool P {
      get { return true; }
    }
  }

  #endregion Interface with method call in contract

  #region ContractClass for abstract methods
  [ContractClass(typeof(AbstractClassContracts))]
  public abstract class AbstractClass {
    public abstract int ReturnFirst(int[] args, bool behave);
    public virtual int Increment(int x, bool behave) {
      Contract.Requires(0 < x);
      Contract.Ensures(Contract.Result<int>() >= x + 1);
      return x + 1;
    }
  }

  [ContractClassFor(typeof(AbstractClass))]
  internal abstract class AbstractClassContracts : AbstractClass {
    public override int ReturnFirst(int[] args, bool behave) {
      Contract.Requires(args != null);
      Contract.Requires(args.Length > 0);
      Contract.Ensures(Contract.Result<int>() == args[0]);
      return default(int);
    }
  }

  [ContractClass(typeof(GenericAbstractClassContracts<,>))]
  public abstract class GenericAbstractClass<A,B> where A: class,B
  {
    public abstract bool IsMatch(B b, A a);

    public abstract B ReturnFirst(B[] args, A match, bool behave);

    public abstract A[][] Collection(int x, int y);

    public abstract A FirstNonNullMatch(A[] elems);

    public abstract C[] GenericMethod<C>(A[] elems);
  }

  [ContractClassFor(typeof(GenericAbstractClass<,>))]
  internal abstract class GenericAbstractClassContracts<A,B> : GenericAbstractClass<A,B>
    where A : class, B
  {
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

  public class ImplForAbstractMethod : AbstractClass
  {
    public override int ReturnFirst(int[] args, bool behave) {
      if (behave)
        return args[0];
      else
        return args[0] + 1;
    }
    public override int Increment(int x, bool behave) {
      Contract.Ensures(Contract.Result<int>() == x + 2);
      return behave ? (x + 2) : base.Increment(x, behave);
    }
  }

  public class ImplForGenericAbstractClass : GenericAbstractClass<string, string>
  {
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
      for (int i=0; i<result.Length; i++) {
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

      foreach (var elem in elems) {
        if (elem is C) {
          result.Add((C)(object)elem);
        }
      }
      if (typeof(C) == typeof(int)) {
        // behave badly
        result.Add((C)(object)55);
      }
      return result.ToArray();
    }
  }

  #endregion ContractClass for abstract methods

  #region Make sure stelem is properly specialized
  public delegate bool D<T>(T t);

  public class BaseWithClosureWithStelem<T>
  {
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
      Contract.Ensures(ts[i+1].Equals(ts[0]));

      int index = i;
      Foo(delegate(T t) { ts[index++] = t; return true; }, ts[0]);
    }
  }

  public class DerivedOfClosureWithStelem<V> : BaseWithClosureWithStelem<V>
  {
    public override void M(V[] vs, int j)
    {
      base.M(vs, j);
    }
  }

  #endregion 
  #endregion

  #region Tests
  [TestClass]
  public class RewrittenInheritanceTest : RewrittenInheritanceDerived {

    [ClassInitialize]
    public static void ClassInitialize (TestContext context) {
      object[] attrs = typeof(RewrittenInheritanceBase).Assembly.GetCustomAttributes(true);
      bool found = false;
      foreach (var a in attrs) {
        Attribute attr = a as Attribute;
        if (attr == null) continue;
        Type ty = attr.TypeId as Type;
        if (ty == null) continue;
        if (ty.FullName == "System.Diagnostics.Contracts.RuntimeContractsAttribute"){
          found = true;
          break;
        }
      }
      Assert.IsTrue(found, "This assembly must have been rewritten before running these tests.  This is usually done in the post build script of the test project.");
    }

    [ClassCleanup]
    public static void ClassCleanup () {
    }

    [TestInitialize]
    public void TestInitialize () {
    }

    [TestCleanup]
    public void TestCleanup () {
      InheritedValueMustBeTrue = true;
      BaseValueMustBeTrue = true;
    }

    [TestMethod]
    public void PositiveOneLevelInheritanceInvariantTest () {
      InheritedValueMustBeTrue = true;
    }

    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.InvariantException))]
    public void NegativeOneLevelInheritanceInvariantTest () {
      InheritedValueMustBeTrue = false;
    }

    [TestMethod]
    public void PositiveTwoLevelInheritanceInvariantTest () {
      BaseValueMustBeTrue = true;
    }

    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.InvariantException))]
    public void NegativeTwoLevelInheritanceInvariantTest () {
      BaseValueMustBeTrue = false;
    }

    [TestMethod]
    public void PositiveBareInheritanceRequiresTest () {
      BareRequires(true);
    }

    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void NegativeBareInheritanceRequiresTest () {
      BareRequires(false);
    }

    [TestMethod]
    public void PositiveLegacyInheritanceRequiresTest()
    {
      LegacyRequires("");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void NegativeLegacyInheritanceRequiresTest()
    {
      LegacyRequires(null);
    }

    [TestMethod]
    public void PositiveInheritanceRequiresWithExceptionTest()
    {
      RequiresWithException("");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void NegativeInheritanceRequiresWithExceptionTest()
    {
      RequiresWithException(null);
    }

    [TestMethod]
    public void PositiveAdditiveInheritanceRequiresTest () {
      AdditiveRequires(101);
    }

    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void NegativeAdditiveOneLevelInheritanceRequiresTest () {
      AdditiveRequires(0);
    }

    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void NegativeAdditiveTwoLevelInheritanceRequiresTest () {
      AdditiveRequires(0);
    }

    [TestMethod]
    public void PositiveSubtractiveInheritanceRequiresTest () {
      SubtractiveRequires(101);
    }

    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void NegativeSubtractiveOneLevelInheritanceRequiresTest () {
      SubtractiveRequires(0);
    }

    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void NegativeSubtractiveTwoLevelInheritanceRequiresTest () {
      SubtractiveRequires(1);
    }

    [TestMethod]
    public void PositiveAdditiveInheritanceEnsuresTest () {
      AdditiveEnsures(101);
    }

    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeAdditiveOneLevelInheritanceEnsuresTest () {
      AdditiveEnsures(1);
    }

    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeAdditiveTwoLevelInheritanceEnsuresTest () {
      AdditiveEnsures(0);
    }

    [TestMethod]
    public void PositiveSubtractiveInheritanceEnsuresTest () {
      SubtractiveEnsures(101);
    }

    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeSubtractiveOneLevelInheritanceEnsuresTest () {
      SubtractiveEnsures(0);
    }

    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeSubtractiveTwoLevelInheritanceEnsuresTest () {
      SubtractiveEnsures(1);
    }

    [TestMethod]
    public void PositiveInheritedImplicitRequiresTest () {
      InterfaceBaseRequires(true);
    }

    [TestMethod]
    public void PositiveInheritedImplicitEnsuresTest () {
      InterfaceBaseEnsures(true);
    }

    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void NegativeInheritedImplicitRequiresTest () {
      InterfaceBaseRequires(false);
    }

    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeInheritedImplicitEnsuresTest () {
      InterfaceBaseEnsures(false);
    }

    [TestMethod]
    public void PositiveImplicitlyOverriddenRequiresTest () {
      ImplicitlyOverriddenInterfaceRequires(true);
    }

    [TestMethod]
    public void PositiveImplicitlyOverriddenEnsuresTest () {
      ImplicitlyOverriddenInterfaceEnsures(true);
    }

    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void NegativeImplicitlyOverriddenRequiresTest () {
      ImplicitlyOverriddenInterfaceRequires(false);
    }

    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeImplicitlyOverriddenEnsuresTest () {
      ImplicitlyOverriddenInterfaceEnsures(false);
    }

    [TestMethod]
    public void PositiveExplicitlyOverriddenRequiresTest () {
      ExplicitlyOverriddenInterfaceRequires(true);
      ExplicitlyOverriddenInterfaceRequires(false);
    }

    [TestMethod]
    public void PositiveExplicitlyOverriddenEnsuresTest () {
      ExplicitlyOverriddenInterfaceEnsures(true);
      ExplicitlyOverriddenInterfaceEnsures(false);
    }

    [TestMethod]
    public void PositiveMultiplyImplicitlyOverriddenRequiresTest () {
      MultiplyImplicitlyOverriddenInterfaceRequires(true);
    }

    [TestMethod]
    public void PositiveMultiplyImplicitlyOverriddenEnsuresTest () {
      MultiplyImplicitlyOverriddenInterfaceEnsures(true);
    }

    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void NegativeMultiplyImplicitlyOverriddenRequiresTest () {
      MultiplyImplicitlyOverriddenInterfaceRequires(false);
    }

    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeMultiplyImplicitlyOverriddenEnsuresTest () {
      MultiplyImplicitlyOverriddenInterfaceEnsures(false);
    }

    [TestMethod]
    public void PositiveMultiplyExplicitlyOverriddenRequiresTest () {
      ((IRewrittenMultipleInheritanceInterface)this).MultiplyExplicitlyOverriddenInterfaceRequires(true);
    }

    [TestMethod]
    public void PositiveMultiplyExplicitlyOverriddenEnsuresTest () {
      ((IRewrittenMultipleInheritanceInterface)this).MultiplyExplicitlyOverriddenInterfaceEnsures(true);
    }

    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void NegativeMultiplyExplicitlyOverriddenRequiresTest () {
      ((IRewrittenMultipleInheritanceInterface)this).MultiplyExplicitlyOverriddenInterfaceRequires(false);
    }

    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeMultiplyExplicitlyOverriddenEnsuresTest () {
      ((IRewrittenMultipleInheritanceInterface)this).MultiplyExplicitlyOverriddenInterfaceEnsures(false);
    }

    [TestMethod]
    public void PositiveMultiplyDifferentlyImplicitlyOverriddenRequiresTest () {
      MultiplyDifferentlyOverriddenInterfaceRequires(true);
    }

    [TestMethod]
    public void PositiveMultiplyDifferentlyExplicitlyOverriddenRequiresTest () {
      ((IRewrittenMultipleInheritanceInterface)this).MultiplyDifferentlyOverriddenInterfaceRequires(true);
    }

    [TestMethod]
    public void PositiveMultiplyDifferentlyImplicitlyOverriddenEnsuresTest () {
      MultiplyDifferentlyOverriddenInterfaceEnsures(true);
    }

    [TestMethod]
    public void PositiveMultiplyDifferentlyExplicitlyOverriddenEnsuresTest () {
      ((IRewrittenMultipleInheritanceInterface)this).MultiplyDifferentlyOverriddenInterfaceEnsures(true);
    }

    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void NegativeMultiplyDifferentlyImplicitlyOverriddenRequiresTest () {
      MultiplyDifferentlyOverriddenInterfaceRequires(false);
    }

    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void NegativeMultiplyDifferentlyExplicitlyOverriddenRequiresTest () {
      ((IRewrittenMultipleInheritanceInterface)this).MultiplyDifferentlyOverriddenInterfaceRequires(false);
    }

    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeMultiplyDifferentlyImplicitlyOverriddenEnsuresTest () {
      MultiplyDifferentlyOverriddenInterfaceEnsures(false);
    }

    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeMultiplyDifferentlyExplicitlyOverriddenEnsuresTest () {
      ((IRewrittenMultipleInheritanceInterface)this).MultiplyDifferentlyOverriddenInterfaceEnsures(false);
    }

    [TestMethod]
    public void PositiveImplicitRequiresTest () {
      InterfaceRequiresMultipleOfTwo(2);
    }

    [TestMethod]
    public void PositiveImplicitEnsuresTest () {
      InterfaceEnsuresMultipleOfTwo(2);
    }

    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void NegativeImplicitRequiresTest () {
      InterfaceRequiresMultipleOfTwo(3);
    }

    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeImplicitEnsuresTest () {
      InterfaceEnsuresMultipleOfTwo(3);
    }

    [TestMethod]
    public void PositiveExplicitRequiresTest () {
      ((IRewrittenInheritanceInterfaceDerived2)this).InterfaceRequiresMultipleOfThree(3);
    }

    [TestMethod]
    public void PositiveExplicitEnsuresTest () {
      ((IRewrittenInheritanceInterfaceDerived2)this).InterfaceEnsuresMultipleOfThree(3);
    }

    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void NegativeExplicitRequiresTest () {
      ((IRewrittenInheritanceInterfaceDerived2)this).InterfaceRequiresMultipleOfThree(2);
    }

    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeExplicitEnsuresTest () {
      ((IRewrittenInheritanceInterfaceDerived2)this).InterfaceEnsuresMultipleOfThree(2);
    }

    [TestMethod]
    public void PositiveNonExplicitRequiresTest () {
      InterfaceRequiresMultipleOfThree(1);
      InterfaceRequiresMultipleOfThree(2);
      InterfaceRequiresMultipleOfThree(3);
    }

    [TestMethod]
    public void PositiveNonExplicitEnsuresTest () {
      InterfaceEnsuresMultipleOfThree(1);
      InterfaceEnsuresMultipleOfThree(2);
      InterfaceEnsuresMultipleOfThree(3);
    }

    [TestMethod]
    public void PositiveDoubleRequiresTest () {
      InterfaceRequires(6);
    }

    [TestMethod]
    public void PositiveDoubleEnsuresTest () {
      InterfaceEnsures(6);
    }

    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void NegativeDoubleRequiresTest1 () {
      InterfaceRequires(1);
    }

    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void NegativeDoubleRequiresTest2 () {
      InterfaceRequires(2);
    }

    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void NegativeDoubleRequiresTest3 () {
      InterfaceRequires(3);
    }

    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeDoubleEnsuresTest1 () {
      InterfaceEnsures(1);
    }

    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeDoubleEnsuresTest2 () {
      InterfaceEnsures(2);
    }

    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeDoubleEnsuresTest3 () {
      InterfaceEnsures(3);
    }

    [TestMethod]
    public void PositiveQuantifierTest1 () {
      IQuantifierTest1 iq = new QuantifierTest1();
      int[] A = new int[] { 3, 4, 5 };
      iq.ModifyArray(A, 3, true);
      iq.CopyArray(A, true);
    }
    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeQuantifierTest1a () {
      IQuantifierTest1 iq = new QuantifierTest1();
      int[] A = new int[] { 3, 4, 5 };
      iq.ModifyArray(A, 3, false);
    }
    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeQuantifierTest1b () {
      IQuantifierTest1 iq = new QuantifierTest1();
      int[] A = new int[] { 3, 4, 5 };
      iq.CopyArray(A, false);
    }

    [TestMethod]
    public void PositiveQuantifierTest2 () {
      IQuantifierTest2 iq = new QuantifierTest2();
      int[] A = new int[] { 3, 4, 5 };
      iq.ModifyArray(A, 3, true);
      iq.CopyArray(A, true);
    }
    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeQuantifierTest2a () {
      IQuantifierTest2 iq = new QuantifierTest2();
      int[] A = new int[] { 3, 4, 5 };
      iq.ModifyArray(A, 3, false);
    }
    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeQuantifierTest2b () {
      IQuantifierTest2 iq = new QuantifierTest2();
      int[] A = new int[] { 3, 4, 5 };
      iq.CopyArray(A, false);
    }

    [TestMethod]
    public void PositiveAbstractClass () {
      AbstractClassInterface a = new AbstractClassInterfaceImplementationSubType();
      a.M(true);
    }
    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeAbstractClass () {
      AbstractClassInterface a = new AbstractClassInterfaceImplementationSubType();
      a.M(false);
    }

    [TestMethod]
    public void PositiveBaseClassWithMethodCallInContract () {
      DerivedClassForBaseClassWithMethodCallInContract o = new DerivedClassForBaseClassWithMethodCallInContract();
      o.M(3);
    }
    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void NegativeBaseClassWithMethodCallInContract () {
      DerivedClassForBaseClassWithMethodCallInContract o = new DerivedClassForBaseClassWithMethodCallInContract();
      o.M(0);
    }

    [TestMethod]
    public void PositiveInheritOld () {
      DerivedFromBaseClassWithOldAndResult o = new DerivedFromBaseClassWithOldAndResult();
      int z = 3;
      o.InheritOld(ref z, true);
    }
    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeInheritOld () {
      DerivedFromBaseClassWithOldAndResult o = new DerivedFromBaseClassWithOldAndResult();
      int z = 3;
      o.InheritOld(ref z, false);
    }
    [TestMethod]
    public void PositiveInheritOldInQuantifier () {
      DerivedFromBaseClassWithOldAndResult o = new DerivedFromBaseClassWithOldAndResult();
      int[] A = new int[] { 3, 4, 5 };
      o.InheritOldInQuantifier(A, true);
    }
    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeInheritOldInQuantifier () {
      DerivedFromBaseClassWithOldAndResult o = new DerivedFromBaseClassWithOldAndResult();
      int[] A = new int[] { 3, 4, 5 };
      o.InheritOldInQuantifier(A, false);
    }
    [TestMethod]
    public void PositiveInheritResultInContract () {
      DerivedFromBaseClassWithOldAndResult o = new DerivedFromBaseClassWithOldAndResult();
      o.InheritResult(3, true);
    }
    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeInheritResultInContract () {
      DerivedFromBaseClassWithOldAndResult o = new DerivedFromBaseClassWithOldAndResult();
      o.InheritResult(3, false);
    }
    [TestMethod]
    public void PositiveInheritResultInQuantifier () {
      DerivedFromBaseClassWithOldAndResult o = new DerivedFromBaseClassWithOldAndResult();
      int[] A = new int[] { 3, 4, 5 };
      o.InheritResultInQuantifier(A, true);
    }
    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeInheritResultInQuantifier () {
      DerivedFromBaseClassWithOldAndResult o = new DerivedFromBaseClassWithOldAndResult();
      int[] A = new int[] { 3, 4, 5 };
      o.InheritResultInQuantifier(A, false);
    }

    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.InvariantException))]
    public void NegativeProperlyInstantiateInheritedInvariants()
    {
      NonGenericDerivedFromGenericWithInvariant ng = new NonGenericDerivedFromGenericWithInvariant(false);
    }

    [TestMethod]
    public void PositiveProperlyInstantiateInheritedInvariants()
    {
      NonGenericDerivedFromGenericWithInvariant ng = new NonGenericDerivedFromGenericWithInvariant(true);
    }

    [TestMethod]
    public void PositiveExplicitGenericInterfaceMethod()
    {
      IInterfaceWithGenericMethod i = new ExplicitGenericInterfaceMethodImplementation(0);
      foreach (var x in i.Bar<string>())
      {
      }

    }

    [TestMethod]
    public void NegativeExplicitGenericInterfaceMethod1()
    {
      try
      {
        IInterfaceWithGenericMethod i = new ExplicitGenericInterfaceMethodImplementation(1);
        foreach (var x in i.Bar<string>())
        {
        }
        throw new Exception();
      }
      catch (TestRewriterMethods.PostconditionException e)
      {
        Assert.AreEqual(": Contract.ForAll(Contract.Result<IEnumerable<T>>(), x => x != null)", e.Message);
      }
    }
    [TestMethod]
    public void NegativeExplicitGenericInterfaceMethod2()
    {
      try
      {
        IInterfaceWithGenericMethod i = new ExplicitGenericInterfaceMethodImplementation(2);
        foreach (var x in i.Bar<string>())
        {
        }
        throw new Exception();
      }
      catch (TestRewriterMethods.PostconditionException e)
      {
        Assert.AreEqual(": Contract.Result<IEnumerable<T>>() != null", e.Message);
      }
    }


    [TestMethod]
    public void PositiveImplicitGenericInterfaceMethod()
    {
      var i = new ImplicitGenericInterfaceMethodImplementation(0);
      foreach (var x in i.Bar<string>())
      {
      }

    }

    [TestMethod]
    public void NegativeImplicitGenericInterfaceMethod1()
    {
      try
      {
        var i = new ImplicitGenericInterfaceMethodImplementation(1);
        foreach (var x in i.Bar<string>())
        {
        }
        throw new Exception();
      }
      catch (TestRewriterMethods.PostconditionException e)
      {
        Assert.AreEqual(": Contract.ForAll(Contract.Result<IEnumerable<T>>(), x => x != null)", e.Message);
      }
    }
    [TestMethod]
    public void NegativeImplicitGenericInterfaceMethod2()
    {
      try
      {
        var i = new ImplicitGenericInterfaceMethodImplementation(2);
        foreach (var x in i.Bar<string>())
        {
        }
        throw new Exception();
      }
      catch (TestRewriterMethods.PostconditionException e)
      {
        Assert.AreEqual(": Contract.Result<IEnumerable<T>>() != null", e.Message);
      }
    }


    #region Interface with method call in contract
    [TestMethod]
    public void PositiveIFaceWithMethodCalInContract() {
      IFaceWithMethodCallInContract o = new ImplForIFaceWithMethodCallInContract();
      o.M(1);
    }
    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void NegativeIFaceWithMethodCalInContract1() {
      IFaceWithMethodCallInContract o = new ImplForIFaceWithMethodCallInContract();
      o.M(3);
    }
    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeIFaceWithMethodCalInContract2() {
      IFaceWithMethodCallInContract o = new ImplForIFaceWithMethodCallInContract();
      o.M(5);
    }
    #endregion Interface with method call in contract

    #region ContractClass for abstract methods
    [TestMethod]
    public void PositiveAbstractMethodWithContract() {
      AbstractClass a = new ImplForAbstractMethod();
      a.ReturnFirst(new int[] { 3, 4, 5 }, true);
    }
    [TestMethod]
    public void PositiveAbstractMethodWithContract2() {
      AbstractClass a = new ImplForAbstractMethod();
      a.Increment(3, true);
    }
    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void NegativeAbstractMethodWithContract1() {
      AbstractClass a = new ImplForAbstractMethod();
      a.ReturnFirst(null, true);
    }
    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void NegativeAbstractMethodWithContract2() {
      AbstractClass a = new ImplForAbstractMethod();
      a.ReturnFirst(new int[] {}, true);
    }
    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeAbstractMethodWithContract3() {
      AbstractClass a = new ImplForAbstractMethod();
      a.ReturnFirst(new int[] { 3, 4, 5 }, false);
    }
    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void NegativeAbstractMethodWithContract4() {
      AbstractClass a = new ImplForAbstractMethod();
      a.Increment(-3, true);
    }
    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeAbstractMethodWithContract5() {
      AbstractClass a = new ImplForAbstractMethod();
      a.Increment(3, false);
    }

    [TestMethod]
    public void PositiveGenericAbstractClassWithContract()
    {
      GenericAbstractClass<string, string> a = new ImplForGenericAbstractClass();
      a.ReturnFirst(new[] { "a", "B", "c" }, "B", true);
    }
    [TestMethod]
    public void PositiveGenericAbstractClassWithContract2()
    {
      GenericAbstractClass<string, string> a = new ImplForGenericAbstractClass();
      var result = a.Collection(2,3);
      Assert.AreEqual(2, result.Length);
    }
    [TestMethod]
    public void PositiveGenericAbstractClassWithContract3()
    {
      GenericAbstractClass<string, string> a = new ImplForGenericAbstractClass();
      var result = a.FirstNonNullMatch(new[]{null, null, "a", "b"});
      Assert.AreEqual("a", result);
    }
    [TestMethod]
    public void PositiveGenericAbstractClassWithContract5()
    {
      GenericAbstractClass<string, string> a = new ImplForGenericAbstractClass();
      var result = a.GenericMethod<float>(new[] { null, null, "a", "b" });
      Assert.AreEqual(0, result.Length);
    }
    [TestMethod]
    public void PositiveGenericAbstractClassWithContract6()
    {
      GenericAbstractClass<string, string> a = new ImplForGenericAbstractClass();
      var result = a.GenericMethod<string>(new[] { "a", "b" });
      Assert.AreEqual(2, result.Length);
    }
    [TestMethod]
    public void PositiveGenericAbstractClassWithContract7()
    {
      GenericAbstractClass<string, string> a = new ImplForGenericAbstractClass();
      var result = a.GenericMethod<object>(new[] { "a", "b" });
      Assert.AreEqual(2, result.Length);
    }
    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void NegativeGenericAbstractClassWithContract1()
    {
      GenericAbstractClass<string,string> a = new ImplForGenericAbstractClass();
      a.ReturnFirst(null, null, true);
    }
    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void NegativeGenericAbstractClassWithContract2()
    {
      GenericAbstractClass<string,string> a = new ImplForGenericAbstractClass();
      a.ReturnFirst(new string[] { }, null, true);
    }
    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeGenericAbstractClassWithContract3()
    {
      GenericAbstractClass<string,string> a = new ImplForGenericAbstractClass();
      a.ReturnFirst(new [] { "3", "4", "5" }, "", false);
    }
    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeGenericAbstractClassWithContract4()
    {
      GenericAbstractClass<string, string> a = new ImplForGenericAbstractClass();
      a.Collection(5, 5);
    }
    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeGenericAbstractClassWithContract5()
    {
      GenericAbstractClass<string, string> a = new ImplForGenericAbstractClass();
      a.FirstNonNullMatch(new string[] { null, null, null });
    }
    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void NegativeGenericAbstractClassWithContract6()
    {
      GenericAbstractClass<string, string> a = new ImplForGenericAbstractClass();
      var result = a.GenericMethod<string>(null);
    }
    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeGenericAbstractClassWithContract7()
    {
      GenericAbstractClass<string, string> a = new ImplForGenericAbstractClass();
      var result = a.GenericMethod<int>(new string[0]);
    }
    #endregion ContractClass for abstract methods

    #region Stelem specialization
    [TestMethod]
    public void PositiveStelemSpecialization1()
    {
      var st = new DerivedOfClosureWithStelem<string>();
      st.M(new string[] { "a", "b", "c" }, 1);
    }
    [TestMethod]
    public void NegativeStelemSpecialization1()
    {
      var st = new DerivedOfClosureWithStelem<string>();
      try
      {
        st.M(new string[] { "a", "b", "c" }, 2);
        throw new Exception();
      }
      catch (TestRewriterMethods.PreconditionException e)
      {
        Assert.AreEqual(e.Message, ": i + 1 < ts.Length");
      }
    }
    #endregion

  }

  #endregion

  [TestClass]
  public class PublicProperty {
    #region Test Management
    [ClassInitialize]
    public static void ClassInitialize(TestContext context) {
      object[] attrs = typeof(RewrittenInheritanceBase).Assembly.GetCustomAttributes(true);
      bool found = false;
      foreach (var a in attrs) {
        Attribute attr = a as Attribute;
        if (attr == null) continue;
        Type ty = attr.TypeId as Type;
        if (ty == null) continue;
        if (ty.FullName == "System.Diagnostics.Contracts.RuntimeContractsAttribute") {
          found = true;
          break;
        }
      }
      Assert.IsTrue(found, "This assembly must have been rewritten before running these tests.  This is usually done in the post build script of the test project.");
    }

    [ClassCleanup]
    public static void ClassCleanup() {
    }

    [TestInitialize]
    public void TestInitialize() {
    }

    [TestCleanup]
    public void TestCleanup() {
    }
    #endregion Test Management
    #region Contracts
    public class C {
      public int P { get { return _p; } }
      [ContractPublicPropertyName("P")]
      private int _p;
      public virtual int foo(int x) {
        Contract.Requires(x < _p);
        return 3;
      }
      public C()
      {
        _p = 0;
      }
    }

    public class D : C {
      public override int foo(int x) {
        return 27;
      }
    }

    class UsingVirtualProperty {
      public virtual int P { get { return _p; } }
      [ContractPublicPropertyName("P")]
      private int _p;
      public virtual int foo(int x) {
        Contract.Requires(x < _p);
        return 3;
      }
      public UsingVirtualProperty() {
        _p = 0;
      }
    }

    class SubtypeOfUsingVirtualProperty : UsingVirtualProperty {
      public override int P { get { return 100; } }
      public override int foo(int x) {
        return 27;
      }
    }
    #endregion Contracts
    #region Tests
    [TestMethod]
    public void PositivePublicProperty() {
      D d = new D();
      d.foo(-3);
    }
    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void NegativePublicProperty() {
      D d = new D();
      d.foo(3);
    }
    [TestMethod]
    public void PositivePublicPropertyUsingVirtualProperty() {
      SubtypeOfUsingVirtualProperty d = new SubtypeOfUsingVirtualProperty();
      // This will fail if the inherited precondition is checked by calling
      //get_P with a non-virtual call.
      d.foo(3);
    }

    [TestMethod]
    public void PositiveTestProperRetargettingOfImplicitInterfaceMethod() {
      var o = new Strilanc.C(true);
      o.S();
    }

    [TestMethod]
    public void NegativeTestProperRetargettingOfImplicitInterfaceMethod() {
      var o = new Strilanc.C(false);
      try {
        o.S();
        throw new Exception();
      }
      catch (TestRewriterMethods.PreconditionException p) {
        Assert.AreEqual("this.P", p.Condition);
      }
    }
    #endregion Tests
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
    public class CI<T> : I<T>
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
      public C(bool behave) {
        this.behave = behave;
      }

      public bool P { get { return behave; } }

      public void S() { 

        Contract.Assert(((B)this).P);
      }

    }
  }

}
