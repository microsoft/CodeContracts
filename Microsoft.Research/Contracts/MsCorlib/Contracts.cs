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

#define DEBUG // The behavior of this contract library should be consistent regardless of build type.

using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace System.Diagnostics.Contracts {
  #region Attributes

  /// <summary>
  /// Methods and classes marked with this attribute can be used within calls to Contract methods. Such methods not make any visible state changes.
  /// </summary>
  [AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Event | AttributeTargets.Delegate | AttributeTargets.Class | AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
  public sealed class PureAttribute : Attribute {
  }

  /// <summary>
  /// Types marked with this attribute specify that a separate type contains the contracts for this type.
  /// </summary>
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false, Inherited = false)]
  public sealed class ContractClassAttribute : Attribute {
    private Type _typeWithContracts;

    public ContractClassAttribute(Type typeContainingContracts) {
      _typeWithContracts = typeContainingContracts;
    }

    public Type TypeContainingContracts {
      get { return _typeWithContracts; }
    }
  }

  /// <summary>
  /// Types marked with this attribute specify that they are a contract for the type that is the argument of the constructor.
  /// </summary>
  [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
  public sealed class ContractClassForAttribute : Attribute {
    private Type _typeIAmAContractFor;

    public ContractClassForAttribute(Type typeContractsAreFor) {
      _typeIAmAContractFor = typeContractsAreFor;
    }

    public Type TypeContractsAreFor {
      get { return _typeIAmAContractFor; }
    }
  }

  /// <summary>
  /// This attribute is used to mark a method as being the invariant
  /// method for a class. The method can have any name, but it must
  /// return "void" and take no parameters. The body of the method
  /// must consist solely of one or more calls to the method
  /// Contract.Invariant. A suggested name for the method is 
  /// "ObjectInvariant".
  /// </summary>
  [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
  public sealed class ContractInvariantMethodAttribute : Attribute {
  }

  /// <summary>
  /// Attribute that specifies that an assembly has runtime contract checks.
  /// </summary>
  [AttributeUsage(AttributeTargets.Assembly)]
  public sealed class RuntimeContractsAttribute : Attribute {
  }

  /// <summary>
  /// Attribute that specifies that an assembly is a reference assembly.
  /// </summary>
  /// <remarks>
  /// This attribute will be replaced by System.Runtime.CompilerServices.ReferenceAssemblyAttribute once
  /// the next version of the CLR is available.
  /// </remarks>
  [AttributeUsage(AttributeTargets.Assembly)]
  public sealed class ContractReferenceAssemblyAttribute : Attribute {
  }

  /// <summary>
  /// Methods (and properties) marked with this attribute can be used within calls to Contract methods, but have no runtime behavior associated with them.
  /// </summary>
  [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
  public sealed class ContractRuntimeIgnoredAttribute : Attribute {
  }

#if FEATURE_SERIALIZATION
  [Serializable]
#endif
  internal enum Mutability {
    Unspecified,
    Immutable,    // read-only after construction, except for lazy initialization & caches
    // Do we need a "deeply immutable" value?
    Mutable,
    HasInitializationPhase,  // read-only after some point.  
    // Do we need a value for mutable types with read-only wrapper subclasses?
  }
  // Note: This hasn't been thought through in any depth yet.  Consider it experimental.
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = false)]
  internal sealed class MutabilityAttribute : Attribute {
    private Mutability _mutabilityMarker;

    public MutabilityAttribute(Mutability mutabilityMarker) {
      _mutabilityMarker = mutabilityMarker;
    }

    public Mutability Mutability {
      get { return _mutabilityMarker; }
    }
  }

  /// <summary>
  /// Instructs downstream tools whether to assume the correctness of this assembly, type or member without performing any verification or not.
  /// Can use [ContractVerification(false)] to explicitly mark assembly, type or member as one to *not* have verification performed on it.
  /// Most specific element found (member, type, then assembly) takes precedence.
  /// (That is useful if downstream tools allow a user to decide which polarity is the default, unmarked case.)
  /// </summary>
  /// <remarks>
  /// Apply this attribute to a type to apply to all members of the type, including nested types.
  /// Apply this attribute to an assembly to apply to all types and members of the assembly.
  /// Apply this attribute to a property to apply to both the getter and setter.
  /// </remarks>
  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Method | AttributeTargets.Constructor | AttributeTargets.Property)]
  public sealed class ContractVerificationAttribute : Attribute {
    private bool _value;

    public ContractVerificationAttribute(bool value) { _value = value; }

    public bool Value {
      get { return _value; }
    }
  }

  /// <summary>
  /// Allows a field f to be used in the method contracts for a method m when f has less visibility than m.
  /// For instance, if the method is public, but the field is private.
  /// </summary>
  [AttributeUsage(AttributeTargets.Field)]
  public sealed class ContractPublicPropertyNameAttribute : Attribute {
    private String _publicName;

    public ContractPublicPropertyNameAttribute(String name) {
      _publicName = name;
    }

    public String Name {
      get { return _publicName; }
    }
  }

  #endregion Attributes

  /// <summary>
  /// Contains static methods for representing program contracts such as preconditions, postconditions, and invariants.
  /// </summary>
  /// <remarks>
  /// WARNING: A binary rewriter must be used to insert runtime enforcement of these contracts.
  /// Otherwise some contracts like Ensures can only be checked statically and will not throw exceptions during runtime when contracts are violated.
  /// Please note this class uses conditional compilation to help avoid easy mistakes.  Defining the preprocessor
  /// symbol CONTRACTS_PRECONDITIONS will include all preconditions expressed using Contract.Requires in your 
  /// build.  The symbol CONTRACTS_FULL will include postconditions and object invariants, and requires the binary rewriter.
  /// </remarks>
  public static class Contract {

    #region Private Methods
    //[System.Security.SecuritySafeCritical]  // auto-generated
    private static void AssertImpl(String message, String conditionString) {
      // @TODO: Managed Debugging Assistant (MDA).  
      // @TODO: Consider converting expression to a String, then passing that as the second parameter.
    }

    // Rewriter will call this method to report a contract failure.  Must be public (or we expose public 
    // RewriterEnsures methods).
    //[SecuritySafeCritical]
    public static void Failure(ContractFailureKind failureKind, String userProvidedMessage, String condition) {
    }

    private static String GetDisplayMessage(ContractFailureKind failureKind, String userProvidedMessage) {
      return null;
    }

    // Will trigger escalation policy, if hosted.  Otherwise, exits the process.
    //[SecuritySafeCritical]
    private static void TriggerEscalationPolicy(String message, String condition) {
    }

    #endregion Private Methods

    #region User Methods

    #region Assume

    /// <summary>
    /// Instructs code analysis tools to assume the expression <paramref name="condition"/> is true even if it can not be statically proven to always be true.
    /// </summary>
    /// <param name="condition">Expression to assume will always be true.</param>
    /// <remarks>
    /// At runtime this is equivalent to an <seealso cref="System.Diagnostics.Contracts.Contract.Assert(bool)"/>.
    /// </remarks>
    [Pure]
    public static void Assume(bool condition) {
    }
    /// <summary>
    /// Instructs code analysis tools to assume the expression <paramref name="condition"/> is true even if it can not be statically proven to always be true.
    /// </summary>
    /// <param name="condition">Expression to assume will always be true.</param>
    /// <param name="message">If it is not a constant string literal, then the contract may not be understood by tools.</param>
    /// <remarks>
    /// At runtime this is equivalent to an <seealso cref="System.Diagnostics.Contracts.Contract.Assert(bool)"/>.
    /// </remarks>
    [Pure]
    public static void Assume(bool condition, String message) {
    }

    #endregion Assume

    #region Assert

    /// <summary>
    /// In debug builds, perform a runtime check that <paramref name="condition"/> is true.
    /// </summary>
    /// <param name="condition">Expression to check to always be true.</param>
    [Pure]
    public static void Assert(bool condition) {
    }
    /// <summary>
    /// In debug builds, perform a runtime check that <paramref name="condition"/> is true.
    /// </summary>
    /// <param name="condition">Expression to check to always be true.</param>
    /// <param name="message">If it is not a constant string literal, then the contract may not be understood by tools.</param>
    [Pure]
    public static void Assert(bool condition, String message) {
    }

    #endregion Assert

    #region Requires

    /// <summary>
    /// Specifies a contract such that the expression <paramref name="condition"/> must be true before the enclosing method or property is invoked.
    /// </summary>
    /// <param name="condition">Boolean expression representing the contract.</param>
    /// <remarks>
    /// This call must happen at the beginning of a method or property before any other code.
    /// This contract is exposed to clients so must only reference members at least as visible as the enclosing method.
    /// Use this form when backward compatibility does not force you to throw a particular exception.
    /// </remarks>
    [Pure]
    public static void Requires(bool condition) {
    }

    /// <summary>
    /// Specifies a contract such that the expression <paramref name="condition"/> must be true before the enclosing method or property is invoked.
    /// </summary>
    /// <param name="condition">Boolean expression representing the contract.</param>
    /// <param name="message">If it is not a constant string literal, then the contract may not be understood by tools.</param>
    /// <remarks>
    /// This call must happen at the beginning of a method or property before any other code.
    /// This contract is exposed to clients so must only reference members at least as visible as the enclosing method.
    /// Use this form when backward compatibility does not force you to throw a particular exception.
    /// </remarks>
    [Pure]
    public static void Requires(bool condition, String message) {
    }

    [Pure]
    public static void Requires<TException>(bool condition) {
    }
    [Pure]
    public static void Requires<TException>(bool condition, String message) {
    }

    #endregion Requires

    #region Ensures

    /// <summary>
    /// Specifies a public contract such that the expression <paramref name="condition"/> will be true when the enclosing method or property returns normally.
    /// </summary>
    /// <param name="condition">Boolean expression representing the contract.  May include <seealso cref="OldValue"/> and <seealso cref="Result"/>.</param>
    /// <remarks>
    /// This call must happen at the beginning of a method or property before any other code.
    /// This contract is exposed to clients so must only reference members at least as visible as the enclosing method.
    /// The contract rewriter must be used for runtime enforcement of this postcondition.
    /// </remarks>
    [Pure]
    public static void Ensures(bool condition) {
    }
    /// <summary>
    /// Specifies a public contract such that the expression <paramref name="condition"/> will be true when the enclosing method or property returns normally.
    /// </summary>
    /// <param name="condition">Boolean expression representing the contract.  May include <seealso cref="OldValue"/> and <seealso cref="Result"/>.</param>
    /// <param name="message">If it is not a constant string literal, then the contract may not be understood by tools.</param>
    /// <remarks>
    /// This call must happen at the beginning of a method or property before any other code.
    /// This contract is exposed to clients so must only reference members at least as visible as the enclosing method.
    /// The contract rewriter must be used for runtime enforcement of this postcondition.
    /// </remarks>
    [Pure]
    public static void Ensures(bool condition, String message) {
    }

    /// <summary>
    /// Specifies a contract such that if an exception of type <typeparamref name="TException"/> is thrown then the expression <paramref name="condition"/> will be true when the enclosing method or property terminates abnormally.
    /// </summary>
    /// <typeparam name="TException">Type of exception related to this postcondition.</typeparam>
    /// <param name="condition">Boolean expression representing the contract.  May include <seealso cref="OldValue"/> and <seealso cref="Result"/>.</param>
    /// <remarks>
    /// This call must happen at the beginning of a method or property before any other code.
    /// This contract is exposed to clients so must only reference types and members at least as visible as the enclosing method.
    /// The contract rewriter must be used for runtime enforcement of this postcondition.
    /// </remarks>
    [Pure]
    public static void EnsuresOnThrow<TException>(bool condition) where TException : Exception {
    }
    /// <summary>
    /// Specifies a contract such that if an exception of type <typeparamref name="TException"/> is thrown then the expression <paramref name="condition"/> will be true when the enclosing method or property terminates abnormally.
    /// </summary>
    /// <typeparam name="TException">Type of exception related to this postcondition.</typeparam>
    /// <param name="condition">Boolean expression representing the contract.  May include <seealso cref="OldValue"/> and <seealso cref="Result"/>.</param>
    /// <param name="message">If it is not a constant string literal, then the contract may not be understood by tools.</param>
    /// <remarks>
    /// This call must happen at the beginning of a method or property before any other code.
    /// This contract is exposed to clients so must only reference types and members at least as visible as the enclosing method.
    /// The contract rewriter must be used for runtime enforcement of this postcondition.
    /// </remarks>
    [Pure]
    public static void EnsuresOnThrow<TException>(bool condition, String message) where TException : Exception {
    }

    #region Old, Result, and Out Parameters

    /// <summary>
    /// Represents the result (a.k.a. return value) of a method or property.
    /// </summary>
    /// <typeparam name="T">Type of return value of the enclosing method or property.</typeparam>
    /// <returns>Return value of the enclosing method or property.</returns>
    /// <remarks>
    /// This method can only be used within the argument to the <seealso cref="Ensures(bool)"/> contract.
    /// </remarks>
    [Pure]
    public static T Result<T>() { return default(T); }

    /// <summary>
    /// Represents the final (output) value of an out parameter when returning from a method.
    /// </summary>
    /// <typeparam name="T">Type of the out parameter.</typeparam>
    /// <param name="value">The out parameter.</param>
    /// <returns>The output value of the out parameter.</returns>
    /// <remarks>
    /// This method can only be used within the argument to the <seealso cref="Ensures(bool)"/> contract.
    /// </remarks>
    [Pure]
    public static T ValueAtReturn<T>(out T value) { value = default(T); return value; }

    /// <summary>
    /// Represents the value of <paramref name="value"/> as it was at the start of the method or property.
    /// </summary>
    /// <typeparam name="T">Type of <paramref name="value"/>.  This can be inferred.</typeparam>
    /// <param name="value">Value to represent.  This must be a field or parameter.</param>
    /// <returns>Value of <paramref name="value"/> at the start of the method or property.</returns>
    /// <remarks>
    /// This method can only be used within the argument to the <seealso cref="Ensures(bool)"/> contract.
    /// </remarks>
    [Pure]
    public static T OldValue<T>(T value) { return default(T); }

    #endregion Old, Result, and Out Parameters

    #endregion Ensures

    #region Invariant

    /// <summary>
    /// Specifies a contract such that the expression <paramref name="condition"/> will be true after every method or property on the enclosing class.
    /// </summary>
    /// <param name="condition">Boolean expression representing the contract.</param>
    /// <remarks>
    /// This contact can only be specified in a dedicated invariant method declared on a class.
    /// This contract is not exposed to clients so may reference members less visible as the enclosing method.
    /// The contract rewriter must be used for runtime enforcement of this invariant.
    /// </remarks>
    [Pure]
    public static void Invariant(bool condition) {
    }
    /// <summary>
    /// Specifies a contract such that the expression <paramref name="condition"/> will be true after every method or property on the enclosing class.
    /// </summary>
    /// <param name="condition">Boolean expression representing the contract.</param>
    /// <param name="message">If it is not a constant string literal, then the contract may not be understood by tools.</param>
    /// <remarks>
    /// This contact can only be specified in a dedicated invariant method declared on a class.
    /// This contract is not exposed to clients so may reference members less visible as the enclosing method.
    /// The contract rewriter must be used for runtime enforcement of this invariant.
    /// </remarks>
    [Pure]
    public static void Invariant(bool condition, String message) {
    }

    #endregion Invariant

    #region Quantifiers

    #region ForAll

    /// <summary>
    /// Returns whether the <paramref name="predicate"/> returns <c>true</c> 
    /// for all integers starting from <paramref name="inclusiveLowerBound"/> to <paramref name="exclusiveUpperBound"/> - 1.
    /// </summary>
    /// <param name="inclusiveLowerBound">First integer to pass to <paramref name="predicate"/>.</param>
    /// <param name="exclusiveUpperBound">One greater than the last integer to pass to <paramref name="predicate"/>.</param>
    /// <param name="predicate">Function that is evaluated from <paramref name="inclusiveLowerBound"/> to <paramref name="exclusiveUpperBound"/> - 1.</param>
    /// <returns><c>true</c> if <paramref name="predicate"/> returns <c>true</c> for all integers 
    /// starting from <paramref name="inclusiveLowerBound"/> to <paramref name="exclusiveUpperBound"/> - 1.</returns>
    [Pure]
    public static bool ForAll(int inclusiveLowerBound, int exclusiveUpperBound, Predicate<int> predicate) {
      Contract.Requires<ArgumentException>(inclusiveLowerBound <= exclusiveUpperBound);
      Contract.Requires<ArgumentNullException>(predicate != null, "predicate");
      return true;
    }
    /// <summary>
    /// Returns whether the <paramref name="predicate"/> returns <c>true</c> 
    /// for all elements in the <paramref name="collection"/>.
    /// </summary>
    /// <param name="collection">The collection from which elements will be drawn from to pass to <paramref name="predicate"/>.</param>
    /// <param name="predicate">Function that is evaluated on elements from <paramref name="collection"/>.</param>
    /// <returns><c>true</c> if and only if <paramref name="predicate"/> returns <c>true</c> for all elements in
    /// <paramref name="collection"/>.</returns>
    [Pure]
    public static bool ForAll<T>(IEnumerable<T> collection, Predicate<T> predicate) {
      Contract.Requires<ArgumentNullException>(collection != null, "collection");
      Contract.Requires<ArgumentNullException>(predicate != null, "predicate");
      return true;
    }
    /// <summary>
    /// Returns whether the <paramref name="predicate"/> returns <c>true</c> 
    /// for all values of type T.
    /// </summary>
    /// <param name="predicate">Function that is evaluated on elements of type T.</param>
    /// <returns><c>true</c> if and only if <paramref name="predicate"/> returns <c>true</c> for all elements of type T.</returns>
    [Pure]
    public static bool ForAll<T>(Predicate<T> predicate) {
      Contract.Requires<ArgumentNullException>(predicate != null, "predicate");
      return true;
    }

    #endregion ForAll

    #region Exists

    /// <summary>
    /// Returns whether the <paramref name="predicate"/> returns <c>true</c> 
    /// for any integer starting from <paramref name="inclusiveLowerBound"/> to <paramref name="exclusiveUpperBound"/> - 1.
    /// </summary>
    /// <param name="inclusiveLowerBound">First integer to pass to <paramref name="predicate"/>.</param>
    /// <param name="exclusiveUpperBound">One greater than the last integer to pass to <paramref name="predicate"/>.</param>
    /// <param name="predicate">Function that is evaluated from <paramref name="inclusiveLowerBound"/> to <paramref name="exclusiveUpperBound"/> - 1.</param>
    /// <returns><c>true</c> if <paramref name="predicate"/> returns <c>true</c> for any integer
    /// starting from <paramref name="inclusiveLowerBound"/> to <paramref name="exclusiveUpperBound"/> - 1.</returns>
    [Pure]
    public static bool Exists(int inclusiveLowerBound, int exclusiveUpperBound, Predicate<int> predicate) {
      Contract.Requires<ArgumentException>(inclusiveLowerBound <= exclusiveUpperBound);
      Contract.Requires<ArgumentNullException>(predicate != null, "predicate");
      return false;
    }
    /// <summary>
    /// Returns whether the <paramref name="predicate"/> returns <c>true</c> 
    /// for any element in the <paramref name="collection"/>.
    /// </summary>
    /// <param name="collection">The collection from which elements will be drawn from to pass to <paramref name="predicate"/>.</param>
    /// <param name="predicate">Function that is evaluated on elements from <paramref name="collection"/>.</param>
    /// <returns><c>true</c> if and only if <paramref name="predicate"/> returns <c>true</c> for an element in
    /// <paramref name="collection"/>.</returns>
    [Pure]
    public static bool Exists<T>(IEnumerable<T> collection, Predicate<T> predicate) {
      Contract.Requires<ArgumentNullException>(collection != null, "collection");
      Contract.Requires<ArgumentNullException>(predicate != null, "predicate");
      return false;
    }
    /// <summary>
    /// Returns whether the <paramref name="predicate"/> returns <c>true</c> 
    /// for some value of type T.
    /// </summary>
    /// <param name="predicate">Function that is evaluated on elements of type T.</param>
    /// <returns><c>true</c> if and only if <paramref name="predicate"/> returns <c>true</c> for some value of type T.</returns>
    [Pure]
    public static bool Exists<T>(Predicate<T> predicate) {
      Contract.Requires<ArgumentNullException>(predicate != null, "predicate");
      return true;
    }

    #endregion Exists

    #endregion Quantifiers

    #region Pointers

    /// <summary>
    /// Runtime checking for pointer bounds is not currently feasible. Thus, at runtime, we just return
    /// a very long extent for each pointer that is writable. As long as assertions are of the form
    /// WritableBytes(ptr) >= ..., the runtime assertions will not fail.
    /// The runtime value is 2^64 - 1 or 2^32 - 1.
    /// </summary>
    static readonly ulong MaxWritableExtent;

    /// <summary>
    /// Allows specifying a writable extent for a UIntPtr, similar to SAL's writable extent.
    /// NOTE: this is for static checking only. No useful runtime code can be generated for this
    /// at the moment.
    /// </summary>
    /// <param name="startAddress">Start of memory region</param>
    /// <returns>The result is the number of bytes writable starting at <paramref name="startAddress"/></returns>
    [Pure]
    [ContractRuntimeIgnored]
    public static ulong WritableBytes(UIntPtr startAddress) { return MaxWritableExtent; }

    /// <summary>
    /// Allows specifying a writable extent for a UIntPtr, similar to SAL's writable extent.
    /// NOTE: this is for static checking only. No useful runtime code can be generated for this
    /// at the moment.
    /// </summary>
    /// <param name="startAddress">Start of memory region</param>
    /// <returns>The result is the number of bytes writable starting at <paramref name="startAddress"/></returns>
    [Pure]
    [ContractRuntimeIgnored]
    public static ulong WritableBytes(IntPtr startAddress) { return MaxWritableExtent; }

    /// <summary>
    /// Allows specifying a writable extent for a UIntPtr, similar to SAL's writable extent.
    /// NOTE: this is for static checking only. No useful runtime code can be generated for this
    /// at the moment.
    /// </summary>
    /// <param name="startAddress">Start of memory region</param>
    /// <returns>The result is the number of bytes writable starting at <paramref name="startAddress"/></returns>
    [Pure]
    [ContractRuntimeIgnored]
    unsafe public static ulong WritableBytes(void* startAddress) { return MaxWritableExtent; }

    /// <summary>
    /// Allows specifying a readable extent for a UIntPtr, similar to SAL's readable extent.
    /// NOTE: this is for static checking only. No useful runtime code can be generated for this
    /// at the moment.
    /// </summary>
    /// <param name="startAddress">Start of memory region</param>
    /// <returns>The result is the number of bytes readable starting at <paramref name="startAddress"/></returns>
    [Pure]
    [ContractRuntimeIgnored]
    public static ulong ReadableBytes(UIntPtr startAddress) { return MaxWritableExtent; }

    /// <summary>
    /// Allows specifying a readable extent for a UIntPtr, similar to SAL's readable extent.
    /// NOTE: this is for static checking only. No useful runtime code can be generated for this
    /// at the moment.
    /// </summary>
    /// <param name="startAddress">Start of memory region</param>
    /// <returns>The result is the number of bytes readable starting at <paramref name="startAddress"/></returns>
    [Pure]
    [ContractRuntimeIgnored]
    public static ulong ReadableBytes(IntPtr startAddress) { return MaxWritableExtent; }

    /// <summary>
    /// Allows specifying a readable extent for a UIntPtr, similar to SAL's readable extent.
    /// NOTE: this is for static checking only. No useful runtime code can be generated for this
    /// at the moment.
    /// </summary>
    /// <param name="startAddress">Start of memory region</param>
    /// <returns>The result is the number of bytes readable starting at <paramref name="startAddress"/></returns>
    [Pure]
    [ContractRuntimeIgnored]
    unsafe public static ulong ReadableBytes(void* startAddress) { return MaxWritableExtent; }

    #endregion

    #region Misc.

    /// <summary>
    /// Marker to indicate the end of the contract section of a method.
    /// </summary>
    public static void EndContractBlock() { }

    #endregion

    #endregion User Methods

    #region Failure Behavior

    /// <summary>
    /// Allows a managed application environment such as an interactive interpreter (IronPython) or a
    /// web browser host (Jolt hosting Silverlight in IE) to be notified of contract failures and 
    /// potentially "handle" them, either by throwing a particular exception type, etc.  If any of the
    /// event handlers sets the Cancel flag in the ContractFailedEventArgs, then the Contract class will
    /// not pop up an assert dialog box or trigger escalation policy.  Hooking this event requires 
    /// full trust.
    /// </summary>
    public static event EventHandler<ContractFailedEventArgs> ContractFailed {
      add { }
      remove { }
    }

    #endregion FailureBehavior

  }

  public sealed class ContractFailedEventArgs : EventArgs {
    private ContractFailureKind _failureKind;
    private String _debugMessage;
    private String _condition;

    public ContractFailedEventArgs(ContractFailureKind failureKind, String debugMessage, String condition) {
    }

    public String DebugMessage { get { return _debugMessage; } }
    public String Condition { get { return _condition; } }
    public ContractFailureKind FailureKind { get { return _failureKind; } }

    // Whether the event handler "handles" this contract failure, or to fail via escalation policy.
    public bool Handled {
      get;
      set;
    }
  }

  public enum ContractFailureKind {
    Precondition,
    Postcondition,
    Invariant,
    Assert,
    Assume
  }

  [AttributeUsage(AttributeTargets.All)]
  public sealed class ContractModelAttribute : Attribute {
  }

  /// <summary>
  /// Allows setting contract and tool options at assembly, type, or method granularity.
  /// </summary>
  [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = false)]
  internal sealed class ContractOptionAttribute : global::System.Attribute
  {
    public ContractOptionAttribute(string category, string setting, bool toggle) { }
    public ContractOptionAttribute(string category, string setting, string value) { }
  }

}


