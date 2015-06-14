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

using System.Diagnostics.Contracts;
using System;
using System.Collections.Generic;

namespace CCDocTests {
  //Interface
  /// <summary>
  /// An interface.
  /// </summary>
  [ContractClass(typeof(ContractsForInterface))]
  public interface IInterfaceWithContracts {
    /// <summary>
    /// An unimplemented method in an interface.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    int MethodFromInterface(object input);

    /// <summary>
    /// A generic method in a non-generic interface.
    /// </summary>
    T GenericMethodInNonGenericInterface<T>(T t);
  }
  /// <summary>
  /// Contracts for an interface.
  /// </summary>
  [ContractClassFor(typeof(IInterfaceWithContracts))]
  abstract class ContractsForInterface : IInterfaceWithContracts {
    /// <summary>
    /// Contracts for a method in an interface.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    int IInterfaceWithContracts.MethodFromInterface(object input) {
      Contract.Requires<ArgumentNullException>(input != null, "Input should be non-null.");
      Contract.EnsuresOnThrow<InvalidCastException>(input != null);
      Contract.Ensures(Contract.Result<int>() > 0, "Result will always be a positive integer.");
      return default(int);
    }

    /// <summary>
    /// A generic method in a non-generic interface.
    /// </summary>
    T IInterfaceWithContracts.GenericMethodInNonGenericInterface<T>(T t) {
      Contract.Ensures(Contract.Result<T>().Equals(t));
      throw new NotImplementedException();
    }
  }
  //Base class
  /// <summary>
  /// A base class.
  /// </summary>
  [ContractClass(typeof(ContractsForBaseClass))]
  public abstract class BaseClassWithContracts {
    /// <summary>
    /// An unimplemented method in a base class.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public abstract int MethodFromBaseClass(object input);
  }
  /// <summary>
  /// Contracts for a base class.
  /// </summary>
  [ContractClassFor(typeof(BaseClassWithContracts))]
  abstract class ContractsForBaseClass : BaseClassWithContracts {
    /// <summary>
    /// Contracts for a method in a base class.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public override int MethodFromBaseClass(object input) {
      Contract.Requires<ArgumentNullException>(input != null, "Input should be non-null.");
      Contract.EnsuresOnThrow<InvalidCastException>(input != null);
      Contract.Ensures(Contract.Result<int>() > 0, "Result will always be a positive integer.");
      return default(int);
    }
  }
  /// <summary>
  /// A intermediate base class.
  /// </summary>
  public class IntermediateBaseClassWithContracts : BaseClassWithContracts {
    /// <summary>
    /// An implimented method that adds postconditions.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public override int MethodFromBaseClass(object input) {
      Contract.Ensures(Contract.Result<int>() > 1, "A contract declared on an intermediate class.");
      return 2;
    }
  }
  //Test class
  /// <summary>
  /// A dummy class with various contracts.
  /// </summary>
  public class TestClass : IntermediateBaseClassWithContracts, IInterfaceWithContracts {
    //Inherited contracts
    /// <summary>
    /// An implicitly implemented method that was inherited from an interface.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public int MethodFromInterface(object input) {
      Contract.Ensures(true, "This precondition is declared on the implemented method, not the base method.");
      return 1;
    }

    /// <summary>
    /// A generic method implementing the interface method.
    /// </summary>
    /// <typeparam name="U"></typeparam>
    /// <param name="u"></param>
    /// <returns></returns>
    public U GenericMethodInNonGenericInterface<U>(U u) {
      Contract.Ensures(true, "An extra postcondition declared on the implemented method.");
      return u;
    }

    /// <summary>
    /// An explicitly implemented method that was inherited from an interface.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public override int MethodFromBaseClass(object input) {
      base.MethodFromBaseClass(input);
      Contract.Ensures(true, "This precondition is declared on the implemented method, not the base method.");
      return 1; 
    }
    //Legacy contracts
    /// <summary>
    /// A method with an if-than-throw statement.
    /// </summary>
    /// <param name="input"></param>
    public void MethodWithLegacyContracts(object input) {
      if (input == null) {
        throw new ArgumentNullException("input", "Input should be non-null.");
      }
      Contract.EndContractBlock();
    }
    //Invariants
    /// <summary>
    /// A dummy field used by object invariants.
    /// </summary>
    public int intForInvariants = 10;
    /// <summary>
    /// Object invariants for TestClass.
    /// </summary>
    [ContractInvariantMethod]
    public void ObjectInvariant() {
      Contract.Invariant(intForInvariants > 0);
      Contract.Invariant(intForInvariants % 2 == 0, "Integer will always be even.");
    }
    //Property contracts
    /// <summary>
    /// A property with a getter and setter and contracts on each.
    /// </summary>
    public int PropertyWithContracts {
      get {
        Contract.Ensures(Contract.Result<int>() % 2 != 0, "Result will never be even.");
        Contract.EnsuresOnThrow<NotSupportedException>(2 == 3);
        return 7;
      }
      set {
        Contract.Requires(value % 2 != 0, "Value should not be even.");
        Contract.Requires<ArgumentOutOfRangeException>(value < 0);
        Contract.EnsuresOnThrow<NotSupportedException>(value == 13, "The NotSupportedException will only be thrown if the value equals 13.");
      }
    }
    //Purity
    /// <summary>
    /// A pure method.
    /// </summary>
    [Pure]
    public void PureMethod() {
      //Do nothing.
    }
    //Method contracts
    /// <summary>
    /// A method with contracts.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="b"></param>
    /// <param name="charArray"></param>
    /// <param name="foo"></param>
    /// <param name="o"></param>
    /// <returns></returns>
    public int MethodWithContracts(int x, bool b, char[] charArray, object foo, out string o) {
      Contract.Requires(Contract.Exists(charArray, index => charArray[index] == 'c'), "One char in the charArray parameter should be 'c'.");
      Contract.Requires(Contract.ForAll(charArray, index => charArray[index] != 'f'));
      Contract.Requires(x > 0, "Input integer must be greater than zero.");
      Contract.Requires<ArgumentNullException>(foo != null);
      Contract.Ensures(!String.IsNullOrEmpty(Contract.ValueAtReturn(out o)), "Output parameter \"o\" will be a non-null, non-empty string.");
      Contract.Ensures(Contract.OldValue<bool>(b) == b);
      Contract.Ensures(Contract.Result<int>() > Contract.OldValue<int>(x), "Return value will always be greater than parameter x.");
      Contract.EnsuresOnThrow<ArgumentException>(Contract.ValueAtReturn(out o) == "Hello World!");
      o = "Hello World!";
      return x + 1;
    }
    /// <summary>
    /// A method with quantifiers.
    /// </summary>
    /// <param name="array"></param>
    /// <returns></returns>
    public void MethodWithQuantifiers(int[] array) {
      Contract.Requires(Contract.ForAll(array, i => array[i] != 666), "None of the numbers should be 666.");
      Contract.Requires(Contract.Exists(array, i => array[i] == 5));
      Contract.Requires(true);
    }
  }

  /// <summary>
  /// A generic interface with contracts
  /// </summary>
  /// <typeparam name="T">must be a reference type and have a null constructor</typeparam>
  [ContractClass(typeof(GJContracts<>))]
  public interface GJ<T> where T : class, new() {
    /// <summary>
    /// A very simple method.
    /// The contract for GJ.M that says that the parameter must
    /// not be null, the return value must be non-null and not
    /// equal to the parameter.
    /// </summary>
    T M(T t);
  }
  [ContractClassFor(typeof(GJ<>))]
  abstract class GJContracts<U> : GJ<U> where U : class, new() {
    U GJ<U>.M(U u){
      Contract.Requires(u != null);
      Contract.Ensures(Contract.Result<U>() != null);
      Contract.Ensures(Contract.Result<U>() != u);
      return default(U);
    }
  }
  /// <summary>
  /// A generic implementation of GJ.
  /// </summary>
  public class GJImpl<V> : GJ<V> where V : class, new() {
    /// <summary>
    /// The implementation for GJ.M.
    /// </summary>
    public V M(V v) {
      return new V();
    }
  }
  /// <summary>
  /// A non-generic implementation of GJ.
  /// </summary>
  public class GJNonGenericImpl : GJ<List<int>> {
    /// <summary>
    /// The implementation for GJ.M.
    /// </summary>
    public List<int> M(List<int> xs) {
      return new List<int>();
    }
  }

  /// <summary>
  /// Bug reported: http://social.msdn.microsoft.com/Forums/en-US/codecontracts/thread/f606114d-7f6b-4ff1-8c66-e32cc050965b
  /// by Hank Beasley. When a contract class implicitly implements the interface methods, then the code
  /// in the contract provider that looked up the method in the contract class wasn't checking the names, just the
  /// signatures.
  /// </summary>
  [ContractClass(typeof(IIMatchingSignaturesContract))]
  public interface IMatchingSignatures {
    /// <summary>
    /// M1s.
    /// </summary>
    void M1(string a);
    /// <summary>
    /// M2s.
    /// </summary>
    void M2(string b);
  }
  /// <summary>
  /// Bug reported where CCI2 found wrong method for implicitly implemented methods.
  /// </summary>
  [ContractClassFor(typeof(IMatchingSignatures))]
  public abstract class IIMatchingSignaturesContract : IMatchingSignatures {
    /// <summary>
    /// Method with same signature as M2
    /// </summary>
    /// <param name="a"></param>
    public void M1(string a) {
      Contract.Requires(a == "h");
    }

    /// <summary>
    /// Method with same signature as M1
    /// </summary>
    /// <param name="b"></param>
    public void M2(string b) {
      Contract.Requires(b == "z");
    }
  }

}

// http://social.msdn.microsoft.com/Forums/en/codecontracts/thread/f46cf6dd-e03a-4831-8b9a-1446c6a4732b
namespace DaveSexton1 {

  /// <summary>
  /// A repro from the forum
  /// </summary>
  [ContractClass(typeof(CollectionModificationContract<>))]
  public abstract partial class CollectionModification<T> {
    /// <summary>
    /// 1
    /// </summary>
    /// <param name="collection">Collection.</param>
    public abstract void Accept(ICollection<T> collection);

    /// <summary>
    /// 2
    /// </summary>
    /// <param name="add">Delegate.</param>
    /// <param name="remove">Delegate.</param>
    /// <param name="clear">Delegate.</param>
    public abstract void Accept(Action<T> add, Action<T> remove, Action clear);

    /// <summary>
    /// 3
    /// </summary>
    /// <typeparam name="TResult">Type.</typeparam>
    /// <param name="add">Delegate.</param>
    /// <param name="remove">Delegate.</param>
    /// <param name="clear">Delegate.</param>
    /// <returns>Result.</returns>
    public abstract TResult Accept<TResult>(Func<T, TResult> add, Func<T, TResult> remove, Func<TResult> clear);
  }

  [ContractClassFor(typeof(CollectionModification<>))]
  internal abstract class CollectionModificationContract<T> : CollectionModification<T> {
    public override void Accept(ICollection<T> collection) {
      Contract.Requires(collection != null);
    }

    public override void Accept(Action<T> add, Action<T> remove, Action clear) {
      Contract.Requires(add != null);
      Contract.Requires(remove != null);
      Contract.Requires(clear != null);
    }

    public override TResult Accept<TResult>(Func<T, TResult> add, Func<T, TResult> remove, Func<TResult> clear) {
      Contract.Requires(remove != null);
      Contract.Requires(add != null);
      Contract.Requires(clear != null);
      return default(TResult);
    }
  }

  /// <summary>
  /// An implementation for a repro from the forum.
  /// </summary>
  public class Foo : CollectionModification<int> {
    /// <summary>
    /// 1
    /// </summary>
    /// <param name="collection">Collection.</param>
    public override void Accept(ICollection<int> collection) { }

    /// <summary>
    /// 2
    /// </summary>
    /// <param name="add">Delegate.</param>
    /// <param name="remove">Delegate.</param>
    /// <param name="clear">Delegate.</param>
    public override void Accept(Action<int> add, Action<int> remove, Action clear) { }

    /// <summary>
    /// 3
    /// </summary>
    /// <typeparam name="TResult">Type.</typeparam>
    /// <param name="add">Delegate.</param>
    /// <param name="remove">Delegate.</param>
    /// <param name="clear">Delegate.</param>
    /// <returns>Result.</returns>
    public override TResult Accept<TResult>(Func<int, TResult> add, Func<int, TResult> remove, Func<TResult> clear) { return default(TResult); }
  }

}
