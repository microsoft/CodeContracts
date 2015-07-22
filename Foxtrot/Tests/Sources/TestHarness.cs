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

namespace Tests.Sources
{
  partial class TestMain
  {
    bool behave;
    public TestMain(bool behave)
    {
      this.behave = behave;
    }

    public static int Main()
    {
      try
      {
        PositiveTest();
        NegativeTest();
        return 0;
      }
      catch (Exception e)
      {
        Console.WriteLine("{0} {1}", e.Message, e.StackTrace);
        return -1;
      }
    }

    partial void Run();

    static void PositiveTest()
    {
      try
      {
        var t = new TestMain(true);
        t.Run();
      }
      catch(Exception e)
      {
        Console.WriteLine();
        Console.WriteLine("Positive test failed.");
        Console.WriteLine("Error: " + e);

        Console.WriteLine();
        throw;
      }
    }

    static void NegativeTest()
    {
      var t = new TestMain(false);
      try
      {
#if NETFRAMEWORK_4_5 || NETFRAMEWORK_4_0
        try {
          t.Run();
        }
        catch (AggregateException ae) {
          throw ae.Flatten().InnerException;
        }
#else
        t.Run();
#endif
        throw new Exception("Expected failure did not happen");
      }
      catch (TestInfrastructure.RewriterMethods.ContractException e)
      {
        TestInfrastructure.Assert.AreEqual(t.NegativeExpectedKind, e.Kind);
        TestInfrastructure.Assert.AreEqual(t.NegativeExpectedCondition, e.Condition);
      }
      catch (ArgumentException e)
      {
        TestInfrastructure.Assert.AreEqual(t.NegativeExpectedKind, ContractFailureKind.Precondition);
        TestInfrastructure.Assert.AreEqual(t.NegativeExpectedCondition, e.Message);
      }
    }
  }

}

namespace TestInfrastructure
{
  class Assert
  {
    public static void AreEqual(object expected, object actual)
    {
      if (object.Equals(expected, actual)) return;
      var result = String.Format("Expected: '{0}', Actual: '{1}'", expected, actual);
      Console.WriteLine(result);
      throw new Exception(result);
    }
  }

  public class RewriterMethods
  {
    public static void Requires(bool condition, string userMsg, string conditionText)
    {
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


    public static void Ensures(bool condition, string userMsg, string conditionText) {
      if (!condition) throw new PostconditionException(userMsg, conditionText);
    }

    public static void EnsuresOnThrow(bool condition, string userMsg, string conditionText, Exception e)
    {
      if (!condition)
      {
        RaiseContractFailedEvent(System.Diagnostics.Contracts.ContractFailureKind.PostconditionOnException, userMsg, conditionText, e);
        throw new PostconditionOnThrowException(userMsg, conditionText);
      }
    }
    public static void Invariant(bool condition, string userMsg, string conditionText)
    {
      if (!condition)
      {
        RaiseContractFailedEvent(System.Diagnostics.Contracts.ContractFailureKind.Invariant, userMsg, conditionText, null);
        throw new InvariantException(userMsg, conditionText);
      }
    }
    public static void Assert(bool condition, string userMsg, string conditionText)
    {
      if (!condition)
      {
        RaiseContractFailedEvent(System.Diagnostics.Contracts.ContractFailureKind.Assert, userMsg, conditionText, null);
        throw new AssertException(userMsg, conditionText);
      }
    }
    public static void Assume(bool condition, string userMsg, string conditionText)
    {
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
    public abstract class ContractException : Exception
    {
      public readonly System.Diagnostics.Contracts.ContractFailureKind Kind;
      public readonly string User;
      public readonly string Condition;

      //protected ContractException() : this("Contract failed.") { }
      //protected ContractException(string message) : base(message) { }
      //protected ContractException(string message, Exception inner) : base(message, inner) { }
      public ContractException(ContractFailureKind kind, string user, string cond)
        : base(user + ": " + cond)
      {
        this.Kind = kind;
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
    public sealed class AssertException : ContractException
    {
#if false
      public AssertException() : this("Assertion failed.") { }
      public AssertException(string message) : base(message) { }
      public AssertException(string message, Exception inner) : base(message, inner) { }
#endif
      public AssertException(string user, string cond) : base(ContractFailureKind.Assert, user, cond) { }
#if FEATURE_SERIALIZATION
      private AssertException (SerializationInfo info, StreamingContext context) : base(info, context) { }
#endif
    }

#if FEATURE_SERIALIZATION
    [Serializable]
#endif
    public sealed class AssumeException : ContractException
    {
#if falsen
      public AssumeException() : this("Assumption failed.") { }
      public AssumeException(string message) : base(message) { }
      public AssumeException(string message, Exception inner) : base(message, inner) { }
#endif
      public AssumeException(string user, string cond) : base(ContractFailureKind.Assume, user, cond) { }
#if FEATURE_SERIALIZATION
      private AssertException (SerializationInfo info, StreamingContext context) : base(info, context) { }
#endif
    }

#if FEATURE_SERIALIZATION
    [Serializable]
#endif
    public sealed class PreconditionException : ContractException
    {
#if false
      public PreconditionException() : this("Precondition failed.") { }
      public PreconditionException(string message) : base(message) { }
      public PreconditionException(string message, Exception inner) : base(message, inner) { }
#endif
      public PreconditionException(string user, string cond) : base(ContractFailureKind.Precondition, user, cond) { }
#if FEATURE_SERIALIZATION
      private PreconditionException (SerializationInfo info, StreamingContext context) : base(info, context) { }
#endif
    }

#if FEATURE_SERIALIZATION
    [Serializable]
#endif
    public sealed class PostconditionException : ContractException
    {
#if false
      public PostconditionException() : this("Postcondition failed.") { }
      public PostconditionException(string message) : base(message) { }
      public PostconditionException(string message, Exception inner) : base(message, inner) { }
#endif
      public PostconditionException(string user, string cond) : base(ContractFailureKind.Postcondition, user, cond) { }
#if FEATURE_SERIALIZATION
      private PostconditionException (SerializationInfo info, StreamingContext context) : base(info, context) { }
#endif
    }


#if FEATURE_SERIALIZATION
    [Serializable]
#endif
    public sealed class PostconditionOnThrowException : ContractException
    {
#if false
      public PostconditionOnThrowException() : this("Postcondition on Exception failed.") { }
      public PostconditionOnThrowException(string message) : base(message) { }
      public PostconditionOnThrowException(string message, Exception inner) : base(message, inner) { }
#endif
      public PostconditionOnThrowException(string user, string cond) : base(ContractFailureKind.PostconditionOnException, user, cond) { }
#if FEATURE_SERIALIZATION
      private PostconditionOnThrowException (SerializationInfo info, StreamingContext context) : base(info, context) { }
#endif
    }

#if FEATURE_SERIALIZATION
    [Serializable]
#endif
    public sealed class InvariantException : ContractException
    {
#if false
      public InvariantException() { }
      public InvariantException(string message) : base(message) { }
      public InvariantException(string message, Exception inner) : base(message, inner) { }
#endif
      public InvariantException(string user, string cond) : base(ContractFailureKind.Invariant, user, cond) { }
#if FEATURE_SERIALIZATION
      private InvariantException (SerializationInfo info, StreamingContext context) : base(info, context) { }
#endif
    }

    #endregion Exceptions
  }
}
