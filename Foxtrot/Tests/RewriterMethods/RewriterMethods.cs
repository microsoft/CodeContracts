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
using System.Diagnostics.CodeAnalysis;

public class TestRewriterMethods {
  public static void Requires(bool condition, string userMsg, string conditionText) {
    if (!condition)
    {
      RaiseContractFailedEvent(System.Diagnostics.Contracts.ContractFailureKind.Precondition, userMsg, conditionText, null);
      throw new PreconditionException(userMsg, conditionText);
    }
  }

#if false
  public static void Requires<E>(bool condition, string userMessage, string conditionText)
    where E:Exception
  {
    if (condition) return;

    string msg = RaiseContractFailedEvent(System.Diagnostics.Contracts.ContractFailureKind.Precondition, userMessage, conditionText, null);
    if (conditionText != null)
    {
      if (userMessage != null)
      {
        msg = conditionText + " required: " + userMessage;
      }
      else
      {
        msg = conditionText + " required";
      }
    }
    else
    {
      msg = userMessage;
    }
    var ci = typeof(E).GetConstructor(new []{typeof(String)});
    Exception obj = null;
    if (ci != null)
    {
      obj = ci.Invoke(new []{msg}) as Exception;
    }
    if (obj != null) { throw obj; }
    throw new ArgumentException(msg);
  }
#endif

#if false
  public static void Ensures(bool condition, string userMsg, string conditionText) {
    if (!condition) throw new PostconditionException(userMsg, conditionText);
  }
#endif
  public static void EnsuresOnThrow(bool condition, string userMsg, string conditionText, Exception e) {
    if (!condition)
    {
      RaiseContractFailedEvent(System.Diagnostics.Contracts.ContractFailureKind.PostconditionOnException, userMsg, conditionText, e);
      throw new PostconditionOnThrowException(userMsg, conditionText);
    }
  }
  public static void Invariant (bool condition, string userMsg, string conditionText) {
    if (!condition)
    {
      RaiseContractFailedEvent(System.Diagnostics.Contracts.ContractFailureKind.Invariant, userMsg, conditionText, null);
      throw new InvariantException(userMsg, conditionText);
    }
  }
  public static void Assert(bool condition, string userMsg, string conditionText) {
    if (!condition)
    {
      RaiseContractFailedEvent(System.Diagnostics.Contracts.ContractFailureKind.Assert, userMsg, conditionText, null);
      throw new AssertException(userMsg, conditionText);
    }
  }
  public static void Assume(bool condition, string userMsg, string conditionText) {
    if (!condition)
    {
      RaiseContractFailedEvent(System.Diagnostics.Contracts.ContractFailureKind.Assume, userMsg, conditionText, null);
      throw new AssumeException(userMsg, conditionText);
    }
  }

  public static int FailureCount = 0;
  public static string RaiseContractFailedEvent(System.Diagnostics.Contracts.ContractFailureKind kind, string user, string condition, Exception inner)
  {
    FailureCount++;
    if (condition != null)
    {
      if (user != null)
      {
        return String.Format("{0} failed: {1}: {2}", kind, condition, user);
      }
      else
      {
        return String.Format("{0} failed: {1}", kind, condition);
      }
    }
    else
    {
      if (user != null)
      {
        return String.Format("{0} failed: {1}", kind, user);
      }
      else
      {
        return String.Format("{0} failed.", kind);
      }
    }
  }

  public static void TriggerFailure(System.Diagnostics.Contracts.ContractFailureKind kind, string message, string user, string condition, Exception inner)
  {
    switch (kind)
    {
      case System.Diagnostics.Contracts.ContractFailureKind.Assert:
        throw new AssertException(user, condition);
      case System.Diagnostics.Contracts.ContractFailureKind.Assume:
        throw new AssumeException(user, condition);
      case System.Diagnostics.Contracts.ContractFailureKind.Invariant:
        throw new InvariantException(user, condition);
      case System.Diagnostics.Contracts.ContractFailureKind.Postcondition:
        throw new PostconditionException(user, condition);
      case System.Diagnostics.Contracts.ContractFailureKind.PostconditionOnException:
        throw new PostconditionOnThrowException(user, condition);
      case System.Diagnostics.Contracts.ContractFailureKind.Precondition:
        throw new PreconditionException(user, condition);
    }
  }
  #region Exceptions

#if FEATURE_SERIALIZATION
    [Serializable]
#endif
  [SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible", Justification = "Not intended to be called at runtime.")]
  public abstract class ContractException : Exception {

    public readonly string User;
    public readonly string Condition;

    protected ContractException () : this("Contract failed.") { }
    protected ContractException (string message) : base(message) { }
    protected ContractException (string message, Exception inner) : base(message, inner) { }
    public ContractException(string user, string cond) : base(user + ": " + cond) {
      this.User = user;
      this.Condition = cond;
    }
#if FEATURE_SERIALIZATION
#if !MIDORI
      [SecurityPermissionAttribute(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
#endif
      protected ContractException (SerializationInfo info, StreamingContext context) : base(info, context) { }
#endif
  }

#if FEATURE_SERIALIZATION
    [Serializable]
#endif
  [SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible", Justification = "Not intended to be called at runtime.")]
  public sealed class AssertException : ContractException {
    public AssertException () : this("Assertion failed.") { }
    public AssertException (string message) : base(message) { }
    public AssertException (string message, Exception inner) : base(message, inner) { }
    public AssertException(string user, string cond) : base(user, cond) { }
#if FEATURE_SERIALIZATION
      private AssertException (SerializationInfo info, StreamingContext context) : base(info, context) { }
#endif
  }

  #if FEATURE_SERIALIZATION
    [Serializable]
#endif
  [SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible", Justification = "Not intended to be called at runtime.")]
  public sealed class AssumeException : ContractException {
    public AssumeException () : this("Assumption failed.") { }
    public AssumeException (string message) : base(message) { }
    public AssumeException (string message, Exception inner) : base(message, inner) { }
    public AssumeException(string user, string cond) : base(user, cond) { }
#if FEATURE_SERIALIZATION
      private AssertException (SerializationInfo info, StreamingContext context) : base(info, context) { }
#endif
  }

#if FEATURE_SERIALIZATION
    [Serializable]
#endif
  [SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible", Justification = "Not intended to be called at runtime.")]
  public sealed class PreconditionException : ContractException {
    public PreconditionException () : this("Precondition failed.") { }
    public PreconditionException (string message) : base(message) { }
    public PreconditionException (string message, Exception inner) : base(message, inner) { }
    public PreconditionException(string user, string cond) : base(user, cond) { }
#if FEATURE_SERIALIZATION
      private PreconditionException (SerializationInfo info, StreamingContext context) : base(info, context) { }
#endif
  }

#if FEATURE_SERIALIZATION
    [Serializable]
#endif
  [SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible", Justification = "Not intended to be called at runtime.")]
  [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Don't tell me how to spell, damnit.")]
  public sealed class PostconditionException : ContractException {
    public PostconditionException () : this("Postcondition failed.") { }
    public PostconditionException (string message) : base(message) { }
    public PostconditionException (string message, Exception inner) : base(message, inner) { }
    public PostconditionException(string user, string cond) : base(user, cond) { }
#if FEATURE_SERIALIZATION
      private PostconditionException (SerializationInfo info, StreamingContext context) : base(info, context) { }
#endif
  }


#if FEATURE_SERIALIZATION
    [Serializable]
#endif
  [SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible", Justification = "Not intended to be called at runtime.")]
  [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Don't tell me how to spell, damnit.")]
  public sealed class PostconditionOnThrowException : ContractException {
    public PostconditionOnThrowException() : this("Postcondition on Exception failed.") { }
    public PostconditionOnThrowException(string message) : base(message) { }
    public PostconditionOnThrowException(string message, Exception inner) : base(message, inner) { }
    public PostconditionOnThrowException(string user, string cond) : base(user, cond) { }
#if FEATURE_SERIALIZATION
      private PostconditionOnThrowException (SerializationInfo info, StreamingContext context) : base(info, context) { }
#endif
  }

#if FEATURE_SERIALIZATION
    [Serializable]
#endif
  [SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible", Justification = "Not intended to be called at runtime.")]
  public sealed class InvariantException : ContractException {
    public InvariantException () { }
    public InvariantException (string message) : base(message) { }
    public InvariantException (string message, Exception inner) : base(message, inner) { }
    public InvariantException(string user, string cond) : base(user, cond) { }
#if FEATURE_SERIALIZATION
      private InvariantException (SerializationInfo info, StreamingContext context) : base(info, context) { }
#endif
  }

  #endregion Exceptions
}