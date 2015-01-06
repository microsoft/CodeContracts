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
using System.Windows.Forms;

namespace Tests
{
  [TestClass]
  public class RewrittenContractTest : DisableAssertUI
  {
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
      Contract.Requires<TException>(mustBeTrue);
    }

    public void EnsuresTrue(bool mustBeTrue)
    {
      Contract.Ensures(mustBeTrue);
    }

    private void PrivateRequiresTrue(bool mustBeTrue)
    {
      Contract.Requires(mustBeTrue);
    }

    private void PrivateEnsuresTrue(bool mustBeTrue)
    {
      Contract.Ensures(mustBeTrue);
    }

    public void RequiresAndEnsuresTrue(bool mustBeTrue)
    {
      Contract.Requires(mustBeTrue);
      Contract.Ensures(mustBeTrue);
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
      if (!preconditionHolds)
        throw new ArgumentException();
      Contract.EndContractBlock();
    }
    double Sample() { return 0.0; }
    public int IfThenThrowAsPrecondition2(int maxValue)
    {
      if (maxValue < 0)
      {
        throw new ArgumentOutOfRangeException("maxValue",
              string.Format("{0}", new object[] { "maxValue" }));
      }
      Contract.EndContractBlock();
      return (int)(this.Sample() * maxValue);
    }
    public int IfThenThrowAsPrecondition3(int maxValue)
    {
      if (maxValue < 0)
        throw new ArgumentOutOfRangeException();
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

    int AssertArgumentIsPositive(int y)
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
      Contract.Requires<ArgumentNullException>(parameter != null, "parameter");

    }
    public void RequiresArgumentOutOfRangeException(int parameter)
    {
      Contract.Requires<ArgumentOutOfRangeException>(parameter >= 0, "parameter");
    }
    public void RequiresArgumentException(int x, int y)
    {
      Contract.Requires<ArgumentException>(x >= y, "x must be at least y");
    }
    public static int TestForMemberVisibility1(int x) {
      Contract.Requires(0 < x, String.Empty);
      return 3;
    }

    #endregion

    #region Tests

    [ClassInitialize]
    public static void ClassInitialize(TestContext context)
    {
      object[] attrs = typeof(RewrittenInheritanceBase).Assembly.GetCustomAttributes(true);
      bool found = false;
      foreach (var a in attrs)
      {
        Attribute attr = a as Attribute;
        if (attr == null) continue;
        Type ty = attr.TypeId as Type;
        if (ty == null) continue;
        if (ty.FullName == "System.Diagnostics.Contracts.RuntimeContractsAttribute")
        {
          found = true;
          break;
        }
      }
      Assert.IsTrue(found, "This assembly must have been rewritten before running these tests.  This is usually done in the post build script of the test project.");
    }

    [ClassCleanup]
    public static void ClassCleanup()
    {
    }

    [TestInitialize]
    public void TestInitialize()
    {
      TestRewriterMethods.FailureCount = 0;
    }

    /// <summary>
    /// Checks that RaiseContractFailedEvent hook is called even on legacy requires methods.
    /// </summary>
    [TestCleanup]
    public void TestCleanup()
    {
      int expectedFailures = this.TestContext.TestName.StartsWith("Positive") ? 0 : 1;
      Assert.AreEqual(expectedFailures, TestRewriterMethods.FailureCount);
      MustBeTrue = true;
    }

    public TestContext TestContext
    {
      get; set;
    }

    [TestMethod]
    public void PositiveAssertTest()
    {
      AssertsTrue(true);
    }

    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.AssertException))]
    public void NegativeAssertTest()
    {
      AssertsTrue(false);
    }

    [TestMethod]
    public void PositiveAssumeTest()
    {
      AssumesTrue(true);
    }

    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.AssumeException))]
    public void NegativeAssumeTest()
    {
      AssumesTrue(false);
    }

    [TestMethod]
    public void PositiveRequiresTest()
    {
      RequiresTrue(true);
    }

    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void NegativeRequiresTest()
    {
      RequiresTrue(false);
    }

    [TestMethod]
    public void PositiveRequiresWithExceptionTest()
    {
      RequiresTrue<ArgumentException>(true);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void NegativeRequiresWithExceptionTest()
    {
      RequiresTrue<ArgumentException>(false);
    }

    [TestMethod]
    public void NegativeRequiresWithExceptionTest2()
    {
      try
      {
        RequiresTrue<ArgumentException>(false);
      }
      catch (ArgumentException e)
      {
        Assert.AreEqual("Precondition failed: mustBeTrue", e.Message, true);
      }
    }

    [TestMethod]
    public void PositivePrivateRequiresTest()
    {
      PrivateRequiresTrue(true);
    }

    [TestMethod]
    public void PositivePrivateRequiresTest2()
    {
      // Does not trigger due to /publicsurface
      PrivateRequiresTrue(false);
    }

    [TestMethod]
    public void PositiveEnsuresTest()
    {
      EnsuresTrue(true);
    }

    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeEnsuresTest()
    {
      EnsuresTrue(false);
    }

    [TestMethod]
    public void PositivePrivateEnsuresTest()
    {
      PrivateEnsuresTrue(true);
    }

    [TestMethod]
    public void PositivePrivateEnsuresTest2()
    {
      // Does not trigger due to /publicsurface
      PrivateEnsuresTrue(false);
    }

    [TestMethod]
    public void PositiveEnsureReturnTest()
    {
      EnsuresReturnsTrue(true);
    }

    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeEnsuresReturnTest()
    {
      EnsuresReturnsTrue(false);
    }

    [TestMethod]
    public void PositiveEnsuresAnythingTest()
    {
      EnsuresAnything(true);
      EnsuresAnything(false);
    }

    [TestMethod]
    public void PositiveRequiresAndEnsuresTest()
    {
      RequiresAndEnsuresTrue(true);
    }

    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void NegativeRequiresAndEnsuresTest()
    {
      RequiresAndEnsuresTrue(false);
    }

    [TestMethod]
    public void PositiveInvariantTest()
    {
      MustBeTrue = true;
    }

    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.InvariantException))]
    public void NegativeInvariantTest()
    {
      MustBeTrue = false;
    }

    [TestMethod]
    public void PositiveEnsureResultTest()
    {
      bool output;
      EnsuresSetsTrue(true, out output);
    }

    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeEnsureResultTest()
    {
      bool output;
      EnsuresSetsTrue(false, out output);
    }

    [TestMethod]
    public void PositiveOldTest()
    {
      bool anything = true;
      Assert.AreEqual(true, SetsAndReturnsOld(ref anything));
      Assert.IsFalse(anything);
      Assert.AreEqual(false, SetsAndReturnsOld(ref anything));
      Assert.IsTrue(anything);
    }

    [TestMethod]
    public void PositiveThrowTest()
    {
      ThrowsEnsuresTrue(0, false);
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void PositiveThrowTest1()
    {
      ThrowsEnsuresTrue(1, true);
    }

    [TestMethod]
    [ExpectedException(typeof(SystemException))]
    public void PositiveThrowTest2()
    {
      ThrowsEnsuresTrue(2, true);
    }

    [TestMethod]
    //[ExpectedException(typeof(Exception))] VS Unit Tests can't handle this.
    public void PositiveThrowTest3()
    {
      try
      {
        ThrowsEnsuresTrue(3, true);
      }
      catch (Exception e)
      {
        Assert.IsTrue(e.GetType() == typeof(Exception));
        return;
      }
      Assert.Fail();
    }

    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeThrowTest0()
    {
      ThrowsEnsuresTrue(0, true);
    }

    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionOnThrowException))]
    public void NegativeThrowTest1()
    {
      ThrowsEnsuresTrue(1, false);
    }

    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionOnThrowException))]
    public void NegativeThrowTest2()
    {
      ThrowsEnsuresTrue(2, false);
    }

    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionOnThrowException))]
    public void NegativeThrowTest3()
    {
      ThrowsEnsuresTrue(3, false);
    }

    [TestMethod]
    public void PositiveRequiresForAll()
    {
      int[] A = new int[] { 3, 4, 5 };
      RequiresForAll(A);
    }
    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void NegativeRequiresForAll()
    {
      int[] A = new int[] { 3, -4, 5 };
      RequiresForAll(A);
    }
    [TestMethod]
    public void PositiveRequiresExists()
    {
      int[] A = new int[] { -3, -4, 5 };
      RequiresExists(A);
    }
    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void NegativeRequiresExists()
    {
      int[] A = new int[] { -3, -4, -5 };
      RequiresExists(A);
    }

    [TestMethod]
    public void PositiveEnsuresForAll()
    {
      int[] A = new int[] { 3, 4, 5 };
      int[] B = EnsuresForAll(A, true);
    }
    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeEnsuresForAll()
    {
      int[] A = new int[] { 3, -4, 5 };
      int[] B = EnsuresForAll(A, false);
    }
    [TestMethod]
    public void PositiveEnsuresExists()
    {
      int[] A = new int[] { -3, -4, 5 };
      int[] B = EnsuresExists(A, true);
    }
    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeEnsuresExists()
    {
      int[] A = new int[] { -3, -4, -5 };
      int[] B = EnsuresExists(A, false);
    }


    [TestMethod]
    public void PositiveEnsuresForAllWithOld()
    {
      int[] A = new int[] { 3, 4, 5 };
      EnsuresForAllWithOld(A, true);
    }
    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeEnsuresForAllWithOld()
    {
      int[] A = new int[] { 3, -4, 5 };
      EnsuresForAllWithOld(A, false);
    }

    [TestMethod]
    public void PositiveEnsuresDoubleClosure()
    {
      int[] A = new int[] { 3, 4, 5 };
      EnsuresDoubleClosure(A, A, true);
    }
    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeEnsuresDoubleClosure()
    {
      int[] A = new int[] { 3, 4, 5 };
      int[] B = new int[] { 3, 4 };
      EnsuresDoubleClosure(A, B, false);
    }

    [TestMethod]
    public void PositiveEnsuresExistsWithOld()
    {
      int[] A = new int[] { -3, -4, 5 };
      EnsuresExistsWithOld(A, true);
    }
    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeEnsuresExistsWithOld()
    {
      int[] A = new int[] { -3, -4, -5 };
      EnsuresExistsWithOld(A, false);
    }
    [TestMethod]
    public void PositiveEnsuresWithResultAndQuantifiers()
    {
      int[] A = new int[] { -3, -4, -5 };
      EnsuresWithResultAndQuantifiers(A, true);
    }
    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeEnsuresWithResultAndQuantifiers()
    {
      int[] A = new int[] { -3, -4, -5 };
      EnsuresWithResultAndQuantifiers(A, false);
    }
#if TURNINGSTATICCLOSUREINTOOBJECTCLOSURE
        [TestMethod]
        public void PositiveEnsuresWithStaticClosure()
        {
          EnsuresWithStaticClosure(true);
        }
        [TestMethod]
        [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
        public void NegativeEnsuresWithStaticClosure()
        {
          EnsuresWithStaticClosure(false);
        }
#endif

    #region IfThenThrow
    [TestMethod]
    public void PositiveIfThenThrowAsPrecondition()
    {
      IfThenThrowAsPrecondition(true);
    }
    [TestMethod]
    [ExpectedException(typeof(System.ArgumentException))]
    public void NegativeIfThenThrowAsPrecondition()
    {
      IfThenThrowAsPrecondition(false);
    }

    [TestMethod]
    public void PositiveIfThenThrowAsPrecondition2()
    {
      IfThenThrowAsPrecondition2(1);
    }
    [TestMethod]
    [ExpectedException(typeof(System.ArgumentOutOfRangeException))]
    public void NegativeIfThenThrowAsPrecondition2()
    {
      IfThenThrowAsPrecondition2(-1);
    }
    [TestMethod]
    public void PositiveIfThenThrowAsPrecondition3()
    {
      IfThenThrowAsPrecondition3(3);
    }
    [TestMethod]
    [ExpectedException(typeof(System.ArgumentOutOfRangeException))]
    public void NegativeIfThenThrowAsPrecondition3a()
    {
      IfThenThrowAsPrecondition3(-1);
    }
    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void NegativeIfThenThrowAsPrecondition3b()
    {
      IfThenThrowAsPrecondition3(2);
    }

    #endregion IfThenThrow

    [TestMethod]
    public void PositiveUseAParamsMethodInContract()
    {
      UseAParamsMethodInContract(3);
    }
    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void NegativeUseAParamsMethodInContract()
    {
      UseAParamsMethodInContract(5);
    }

    [TestMethod]
    public void PositiveAssertArgumentIsPositive()
    {
      AssertArgumentIsPositive(3);
    }
    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.AssertException))]
    public void NegativeAssertArgumentIsPositive()
    {
      AssertArgumentIsPositive(-3);
    }

    [TestMethod]
    public void PositiveOldExpressionWithoutGoodNameToUseForLocal()
    {
      int a = 3;
      int b = 5;
      Swap(ref a, ref b, true);
    }
    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void NegativeOldExpressionWithoutGoodNameToUseForLocal1()
    {
      int a = 3;
      int b = 3;
      Swap(ref a, ref b, true);
    }
    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeOldExpressionWithoutGoodNameToUseForLocal2()
    {
      int a = 3;
      int b = 5;
      Swap(ref a, ref b, false);
    }

    [TestMethod]
    public void PositivePostConditionUsingResultAsCallee()
    {
      PostConditionUsingResultAsCallee(true);
    }
    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativePostConditionUsingResultAsCallee()
    {
      PostConditionUsingResultAsCallee(false);
    }

    [TestMethod]
    public void NegativeRequiresExceptionTest1()
    {
      try
      {
        RequiresArgumentNullException(null);
        throw new Exception();
      }
      catch (ArgumentNullException a)
      {
        Assert.AreEqual("parameter", a.ParamName);
      }
    }
    [TestMethod]
    public void NegativeRequiresExceptionTest2()
    {
      try
      {
        RequiresArgumentOutOfRangeException(-1);
        throw new Exception();
      }
      catch (ArgumentOutOfRangeException a)
      {
        Assert.AreEqual("parameter", a.ParamName);
      }
    }
    [TestMethod]
    public void NegativeRequiresExceptionTest3()
    {
      try
      {
        RequiresArgumentException(0, 1);
        throw new Exception();
      }
      catch (ArgumentException a)
      {
        Assert.AreEqual(null, a.ParamName);
        Assert.AreEqual("Precondition failed: x >= y: x must be at least y", a.Message);
      }
    }
    [TestMethod]
    public void PositiveTestForMemberVisibility1() {
      RewrittenContractTest.TestForMemberVisibility1(3); // call is not really needed since the test is to make sure the rewriter doesn't crash.
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
  [TestClass]
  public class ReEntrantInvariantTest : DisableAssertUI
  {
    #region Test Management
    [ClassInitialize]
    public static void ClassInitialize(TestContext context)
    {
      object[] attrs = typeof(RewrittenInheritanceBase).Assembly.GetCustomAttributes(true);
      bool found = false;
      foreach (var a in attrs)
      {
        Attribute attr = a as Attribute;
        if (attr == null) continue;
        Type ty = attr.TypeId as Type;
        if (ty == null) continue;
        if (ty.FullName == "System.Diagnostics.Contracts.RuntimeContractsAttribute")
        {
          found = true;
          break;
        }
      }
      Assert.IsTrue(found, "This assembly must have been rewritten before running these tests.  This is usually done in the post build script of the test project.");
    }

    [ClassCleanup]
    public static void ClassCleanup()
    {
    }

    [TestInitialize]
    public void TestInitialize()
    {
    }

    [TestCleanup]
    public void TestCleanup()
    {
    }
    #endregion Test Management
    #region Contracts

    public class ClassWithInvariant
    {
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
    #region Tests
    [TestMethod]
    public void TestReenterInvariant()
    {
      var c = new ClassWithInvariant();
      c.ReEnter();
    }

    [TestMethod]
    public void FailInvariant1()
    {
      var c = new ClassWithInvariant();
      try
      {
        c.FailInvariant1();
        throw new Exception();
      }
      catch (TestRewriterMethods.InvariantException e)
      {
        Assert.AreEqual("b", e.Condition);
      }
    }

    [TestMethod]
    public void FailInvariant2()
    {
      var c = new ClassWithInvariant();
      try
      {
        c.FailInvariant2();
        throw new Exception();
      }
      catch (TestRewriterMethods.InvariantException e)
      {
        Assert.AreEqual("PureMethod()", e.Condition);
      }
    }
    #endregion Tests
  }

  /// <summary>
  /// Test to make sure that an invariant in a class does not get
  /// "inherited" by any nested classes.
  /// </summary>
  [TestClass]
  public class ClassWithInvariantAndNestedClass
  {
    #region Test Management
    [ClassInitialize]
    public static void ClassInitialize(TestContext context)
    {
      object[] attrs = typeof(RewrittenInheritanceBase).Assembly.GetCustomAttributes(true);
      bool found = false;
      foreach (var a in attrs)
      {
        Attribute attr = a as Attribute;
        if (attr == null) continue;
        Type ty = attr.TypeId as Type;
        if (ty == null) continue;
        if (ty.FullName == "System.Diagnostics.Contracts.RuntimeContractsAttribute")
        {
          found = true;
          break;
        }
      }
      Assert.IsTrue(found, "This assembly must have been rewritten before running these tests.  This is usually done in the post build script of the test project.");
    }

    [ClassCleanup]
    public static void ClassCleanup()
    {
    }

    [TestInitialize]
    public void TestInitialize()
    {
    }

    [TestCleanup]
    public void TestCleanup()
    {
      b = true;
    }
    #endregion Test Management
    #region Contracts
    public bool b = true;
    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(b);
    }
    #endregion Contracts
    #region Tests
    [TestMethod]
    public void ReEnterInvariant()
    {
      // Don't need to call another method because this method
      // must be public and so gets rewritten to call the invariant.
    }

    class Inner { }

    #endregion Tests
  }

  [TestClass]
  public class LocalsOfStructTypeEndingUpInAssignments
  {
    #region Test Management
    [ClassInitialize]
    public static void ClassInitialize(TestContext context)
    {
      object[] attrs = typeof(RewrittenInheritanceBase).Assembly.GetCustomAttributes(true);
      bool found = false;
      foreach (var a in attrs)
      {
        Attribute attr = a as Attribute;
        if (attr == null) continue;
        Type ty = attr.TypeId as Type;
        if (ty == null) continue;
        if (ty.FullName == "System.Diagnostics.Contracts.RuntimeContractsAttribute")
        {
          found = true;
          break;
        }
      }
      Assert.IsTrue(found, "This assembly must have been rewritten before running these tests.  This is usually done in the post build script of the test project.");
    }

    [ClassCleanup]
    public static void ClassCleanup()
    {
    }

    [TestInitialize]
    public void TestInitialize()
    {
    }

    [TestCleanup]
    public void TestCleanup()
    {
    }
    #endregion Test Management
    #region Contracts

    public interface IPredicate<T>
    {
      [Pure]
      bool Holds(T value);
    }
    public struct Positive : IPredicate<int>
    {
      [Pure]
      public bool Holds(int value)
      {
        Contract.Ensures(Contract.Result<bool>() == (value > 0));
        return value > 0;
      }
    }
    public class MyCollection<T, Pred> where Pred : struct, IPredicate<T>
    {
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
    #region Tests
    [TestMethod]
    public void PositiveLocalOfStructType()
    {
      MyCollection<int, Positive> l = new MyCollection<int, Positive>();
      l.Add(5);
    }
    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void NegativeLocalOfStructType1()
    {
      MyCollection<int, Positive> l = new MyCollection<int, Positive>();
      l.Add(0);
    }
    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeLocalOfStructType2()
    {
      MyCollection<int, Positive> l = new MyCollection<int, Positive>();
      l.Pull();
    }
    #endregion Tests
  }

  [TestClass]
  public class LegacyPreconditions
  {
    #region Test Management
    [ClassInitialize]
    public static void ClassInitialize(TestContext context)
    {
      object[] attrs = typeof(RewrittenInheritanceBase).Assembly.GetCustomAttributes(true);
      bool found = false;
      foreach (var a in attrs)
      {
        Attribute attr = a as Attribute;
        if (attr == null) continue;
        Type ty = attr.TypeId as Type;
        if (ty == null) continue;
        if (ty.FullName == "System.Diagnostics.Contracts.RuntimeContractsAttribute")
        {
          found = true;
          break;
        }
      }
      Assert.IsTrue(found, "This assembly must have been rewritten before running these tests.  This is usually done in the post build script of the test project.");
    }

    [ClassCleanup]
    public static void ClassCleanup()
    {
    }

    [TestInitialize]
    public void TestInitialize()
    {
    }

    [TestCleanup]
    public void TestCleanup()
    {
    }
    #endregion Test Management
    #region Contracts
    public class C
    {
      [Pure]
      private void ThrowHelper()
      {
        throw new TestRewriterMethods.PreconditionException();
      }
      public int DirectThrow(int x)
      {
        if (x < 0)
        {
          throw new TestRewriterMethods.PreconditionException();
        }
        Contract.EndContractBlock();
        return x + 1;
      }
      public int IndirectThrow(int x)
      {
        if (x < 0)
        {
          ThrowHelper();
        }
        Contract.EndContractBlock();
        return x + 1;
      }
    }
    #endregion Contracts
    #region Tests
    [TestMethod]
    public void PositiveLegacyRequiresDirectThrow()
    {
      C c = new C();
      c.DirectThrow(3);
    }
    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void NegativeLegacyRequiresDirectThrow()
    {
      C c = new C();
      c.DirectThrow(-3);
    }
    [TestMethod]
    public void PositiveLegacyRequiresIndirectThrow()
    {
      C c = new C();
      c.IndirectThrow(3);
    }
    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void NegativeLegacyRequiresIndirectThrow()
    {
      C c = new C();
      c.IndirectThrow(-3);
    }
    #endregion Tests
  }

  [TestClass]
  public class GenericClassWithDeferringCtor
  {
    #region Test Management
    [ClassInitialize]
    public static void ClassInitialize(TestContext context)
    {
      object[] attrs = typeof(RewrittenInheritanceBase).Assembly.GetCustomAttributes(true);
      bool found = false;
      foreach (var a in attrs)
      {
        Attribute attr = a as Attribute;
        if (attr == null) continue;
        Type ty = attr.TypeId as Type;
        if (ty == null) continue;
        if (ty.FullName == "System.Diagnostics.Contracts.RuntimeContractsAttribute")
        {
          found = true;
          break;
        }
      }
      Assert.IsTrue(found, "This assembly must have been rewritten before running these tests.  This is usually done in the post build script of the test project.");
    }

    [ClassCleanup]
    public static void ClassCleanup()
    {
    }

    [TestInitialize]
    public void TestInitialize()
    {
    }

    [TestCleanup]
    public void TestCleanup()
    {
    }
    #endregion Test Management
    #region Contracts
    /// <summary>
    /// The generic type doesn't matter for the actual contract,
    /// just to check whether the contract gets extracted or not.
    /// </summary>
    public class C<T>
    {
      public C(int x) { }
      public C(char c) :
        this(c == 'a' ? 0 : 1)
      {
        Contract.Requires(c != 'z');
      }
    }
    #endregion Contracts
    #region Tests
    [TestMethod]
    public void PositiveGenericClassWithDeferringCtor()
    {
      C<int> c = new C<int>('a');
    }
    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void NegativeGenericClassWithDeferringCtor()
    {
      C<int> c = new C<int>('z');
    }
    #endregion Tests
  }

  [TestClass]
  public class IfaceContractUsingLocalToHoldSelf
  {
    #region Test Management
    [ClassInitialize]
    public static void ClassInitialize(TestContext context)
    {
      object[] attrs = typeof(RewrittenInheritanceBase).Assembly.GetCustomAttributes(true);
      bool found = false;
      foreach (var a in attrs)
      {
        Attribute attr = a as Attribute;
        if (attr == null) continue;
        Type ty = attr.TypeId as Type;
        if (ty == null) continue;
        if (ty.FullName == "System.Diagnostics.Contracts.RuntimeContractsAttribute")
        {
          found = true;
          break;
        }
      }
      Assert.IsTrue(found, "This assembly must have been rewritten before running these tests.  This is usually done in the post build script of the test project.");
    }

    [ClassCleanup]
    public static void ClassCleanup()
    {
    }

    [TestInitialize]
    public void TestInitialize()
    {
    }

    [TestCleanup]
    public void TestCleanup()
    {
    }
    #endregion Test Management
    #region Contracts
    [ContractClass(typeof(ContractForJ))]
    public interface J
    {
      bool M(int[] xs, bool shouldBehave);
      bool P { get; }
      bool Q { get; }
    }

    [ContractClassFor(typeof(J))]
    class ContractForJ : J
    {
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


    class B : J
    {
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
    #region Tests
    [TestMethod]
    public void PositiveIfaceContractUsingLocalToHoldSelf()
    {
      J j = new B();
      int[] A = new int[] { 3, 4, 5 };
      j.M(A, true);
    }
    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void NegativeIfaceContractUsingLocalToHoldSelf1()
    {
      J j = new B();
      int[] A = new int[] { 3, 0, 5 };
      j.M(A, true);
    }
    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeIfaceContractUsingLocalToHoldSelf2()
    {
      J j = new B();
      int[] A = new int[] { 3, 4, 5 };
      j.M(A, false);
    }
    #endregion Tests
  }

  [TestClass]
  public class EvaluateOldExpressionsAfterPreconditions
  {
    #region Test Management
    [ClassInitialize]
    public static void ClassInitialize(TestContext context)
    {
      object[] attrs = typeof(RewrittenInheritanceBase).Assembly.GetCustomAttributes(true);
      bool found = false;
      foreach (var a in attrs)
      {
        Attribute attr = a as Attribute;
        if (attr == null) continue;
        Type ty = attr.TypeId as Type;
        if (ty == null) continue;
        if (ty.FullName == "System.Diagnostics.Contracts.RuntimeContractsAttribute")
        {
          found = true;
          break;
        }
      }
      Assert.IsTrue(found, "This assembly must have been rewritten before running these tests.  This is usually done in the post build script of the test project.");
    }

    [ClassCleanup]
    public static void ClassCleanup()
    {
    }

    [TestInitialize]
    public void TestInitialize()
    {
    }

    [TestCleanup]
    public void TestCleanup()
    {
    }
    #endregion Test Management
    #region Contracts
    public class C
    {
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
    #region Tests
    [TestMethod]
    public void PositiveEvaluateOldExpressionsAfterPreconditions()
    {
      C c = new C();
      int[] A = new int[] { 3, 4, 5 };
      c.M(A);
    }
    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void NegativeEvaluateOldExpressionsAfterPreconditions()
    {
      C c = new C();
      c.M(null); // if the old value for xs.Length is evaluated before the precondition this results in a null dereference
    }
    [TestMethod]
    public void PositiveEvaluateFailingOldExpression1()
    {
      C c = new C();
      c.ConditionalOld(null);
    }
    [TestMethod]
    public void PositiveEvaluateFailingOldExpression2()
    {
      C c = new C();
      c.ConditionalOld(new int[]{3,4});
    }
    #endregion Tests
  }

  [TestClass]
  public class PostConditionsInStructCtorsMentioningFields
  {
    #region Test Management
    [ClassInitialize]
    public static void ClassInitialize(TestContext context)
    {
      object[] attrs = typeof(RewrittenInheritanceBase).Assembly.GetCustomAttributes(true);
      bool found = false;
      foreach (var a in attrs)
      {
        Attribute attr = a as Attribute;
        if (attr == null) continue;
        Type ty = attr.TypeId as Type;
        if (ty == null) continue;
        if (ty.FullName == "System.Diagnostics.Contracts.RuntimeContractsAttribute")
        {
          found = true;
          break;
        }
      }
      Assert.IsTrue(found, "This assembly must have been rewritten before running these tests.  This is usually done in the post build script of the test project.");
    }

    [ClassCleanup]
    public static void ClassCleanup()
    {
    }

    [TestInitialize]
    public void TestInitialize()
    {
    }

    [TestCleanup]
    public void TestCleanup()
    {
    }
    #endregion Test Management
    #region Contracts
    // Make sure that ValueAtReturn can be used with struct fields
    public struct S
    {
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
    #region Tests
    [TestMethod]
    public void PositiveFieldsInStructCtorEnsures()
    {
      S s = new S(3, 4, true);
    }
    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeFieldsInStructCtorEnsures()
    {
      S s = new S(3, 4, false);
    }
    #endregion Tests
  }

  [TestClass]
  public class PrivateInvariantInSealedType
  {
    #region Test Management
    [ClassInitialize]
    public static void ClassInitialize(TestContext context)
    {
      object[] attrs = typeof(RewrittenInheritanceBase).Assembly.GetCustomAttributes(true);
      bool found = false;
      foreach (var a in attrs)
      {
        Attribute attr = a as Attribute;
        if (attr == null) continue;
        Type ty = attr.TypeId as Type;
        if (ty == null) continue;
        if (ty.FullName == "System.Diagnostics.Contracts.RuntimeContractsAttribute")
        {
          found = true;
          break;
        }
      }
      Assert.IsTrue(found, "This assembly must have been rewritten before running these tests.  This is usually done in the post build script of the test project.");
    }

    [ClassCleanup]
    public static void ClassCleanup()
    {
    }

    [TestInitialize]
    public void TestInitialize()
    {
    }

    [TestCleanup]
    public void TestCleanup()
    {
    }
    #endregion Test Management
    #region Contracts
    // Make sure that ValueAtReturn can be used with struct fields
    public sealed class T
    {
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
    #region Tests
    [TestMethod]
    public void PositivePrivateInvariantMethod()
    {
      T t = new T(3, 4, true);
    }
    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.InvariantException))]
    public void NegativePrivateInvariantMethod()
    {
      T t = new T(3, 4, false);
    }
    #endregion Tests
  }

  [TestClass]
  public class Beta1RequiresAlways
  {
    #region Test Management
    [ClassInitialize]
    public static void ClassInitialize(TestContext context)
    {
      object[] attrs = typeof(RewrittenInheritanceBase).Assembly.GetCustomAttributes(true);
      bool found = false;
      foreach (var a in attrs)
      {
        Attribute attr = a as Attribute;
        if (attr == null) continue;
        Type ty = attr.TypeId as Type;
        if (ty == null) continue;
        if (ty.FullName == "System.Diagnostics.Contracts.RuntimeContractsAttribute")
        {
          found = true;
          break;
        }
      }
      Assert.IsTrue(found, "This assembly must have been rewritten before running these tests.  This is usually done in the post build script of the test project.");
    }

    [ClassCleanup]
    public static void ClassCleanup()
    {
    }

    [TestInitialize]
    public void TestInitialize()
    {
    }

    [TestCleanup]
    public void TestCleanup()
    {
    }
    #endregion Test Management

    #region Contracts
    public static void WithRequiresAlways1(string s)
    {
      Contract.RequiresAlways(s != null);
    }
    public static void WithRequiresAlways2(string s)
    {
      Contract.RequiresAlways(s != null, "s should be non-null");
    }
    #endregion Contracts
    #region Tests
    [TestMethod]
    public void PositiveRequiresAlways1()
    {
      WithRequiresAlways1("hello");
    }
    [TestMethod]
    public void PositiveRequiresAlways2()
    {
      WithRequiresAlways2("hello");
    }
    [TestMethod]
    public void NegativeRequiresAlways1()
    {
      try
      {
        WithRequiresAlways1(null);
        throw new Exception();
      }
      catch (ArgumentException a)
      {
        Assert.AreEqual("Precondition failed: s != null", a.Message);
      }
    }
    [TestMethod]
    public void NegativeRequiresAlways2()
    {
      try
      {
        WithRequiresAlways2(null);
        throw new Exception();
      }
      catch (ArgumentException a)
      {
        Assert.AreEqual("Precondition failed: s != null: s should be non-null", a.Message);
      }
    }
    #endregion Tests
  }


  [TestClass]
  public class ConstructorsWithClosures 
  {
    #region Test Management
    [ClassInitialize]
    public static void ClassInitialize(TestContext context)
    {
      object[] attrs = typeof(RewrittenInheritanceBase).Assembly.GetCustomAttributes(true);
      bool found = false;
      foreach (var a in attrs)
      {
        Attribute attr = a as Attribute;
        if (attr == null) continue;
        Type ty = attr.TypeId as Type;
        if (ty == null) continue;
        if (ty.FullName == "System.Diagnostics.Contracts.RuntimeContractsAttribute")
        {
          found = true;
          break;
        }
      }
      Assert.IsTrue(found, "This assembly must have been rewritten before running these tests.  This is usually done in the post build script of the test project.");
    }

    [ClassCleanup]
    public static void ClassCleanup()
    {
    }

    [TestInitialize]
    public void TestInitialize()
    {
    }

    [TestCleanup]
    public void TestCleanup()
    {
    }
    #endregion Test Management

    #region Contracts

    public class Base
    {
      public Base(ref object byref)
      {
      }
    }

    public class ValueMapping : Base
    {
      internal readonly Func<object, string> ValueToString;
      internal string name = "";

      public ValueMapping(object encoder)
        : base(ref encoder)
      {
        Contract.Requires<ArgumentException>(encoder != null);
        //Contract.Requires(encoder != null);
        var nullEncoder = encoder as string;

        this.ValueToString = obj => (obj != null) ? encoder.ToString() : nullEncoder;
      }

      public ValueMapping(object encoder, bool dummy)
        : base(ref encoder)
      {
        Contract.Requires<ArgumentException>(encoder != null);
        //Contract.Requires(encoder != null);
        var nullEncoder = encoder as string;

        this.ValueToString = obj => (obj != null) ? (dummy) ? encoder.ToString() : nullEncoder : this.name;
      }

      public ValueMapping(object encoder, string dummy)
        : this(encoder)
      {
        Contract.Requires<ArgumentException>(encoder != null);
        //Contract.Requires(encoder != null);
        var nullEncoder = encoder as string;

        this.ValueToString = obj => (obj != null) ? (dummy == null) ? encoder.ToString() : nullEncoder : this.name;
      }

      public ValueMapping(object encoder, string[] data)
        : base(ref encoder)
      {
        Contract.Requires<ArgumentException>(encoder != null);
        Contract.Ensures(Contract.Exists(0, data.Length, i => data[i] == this.name));
        var nullEncoder = encoder as string;
        this.name = nullEncoder;

        this.ValueToString = obj => (obj != null) ? encoder.ToString() : nullEncoder;
      }

    }

    public struct StructCtors
    {
      internal readonly Func<object, string> ValueToString;
      internal string name;

      public StructCtors(object encoder)
      {
        Contract.Requires<ArgumentException>(encoder != null);
        //Contract.Requires(encoder != null);
        this.name = "";
        var nullEncoder = encoder as string;

        this.ValueToString = obj => (obj != null) ? encoder.ToString() : nullEncoder;
      }

      public StructCtors(object encoder, bool dummy) : this(encoder)
      {
        Contract.Requires<ArgumentException>(encoder != null);
        //Contract.Requires(encoder != null);
        var nullEncoder = encoder as string;

        this.ValueToString = obj => (obj != null) ? (dummy) ? encoder.ToString() : nullEncoder : "";
      }

      public StructCtors(object encoder, string dummy)
      {
        Contract.Requires<ArgumentException>(encoder != null);
        //Contract.Requires(encoder != null);
        var nullEncoder = encoder as string;
        this.name = "";
        this.ValueToString = obj => (obj != null) ? (dummy==null) ? encoder.ToString() : nullEncoder : "";
      }

      public StructCtors(object encoder, string[] data)
      {
        Contract.Requires<ArgumentException>(encoder != null);
        Contract.Ensures(Contract.Exists(0, data.Length, i => data[i] == (encoder as string)));
        var nullEncoder = encoder as string;
        this.name = nullEncoder;

        this.ValueToString = obj => (obj != null) ? encoder.ToString() : nullEncoder;
      }

    }

    #endregion Contracts

    #region Tests
    [TestMethod]
    public void PositiveCtorWithClosure1()
    {
      var c = new ValueMapping("hello");
    }
    [TestMethod]
    public void PositiveCtorWithClosure2()
    {
      var c = new ValueMapping(new Object());
    }
    [TestMethod]
    public void PositiveCtorWithClosure3()
    {
      var c = new ValueMapping("hello", true);
    }
    [TestMethod]
    public void PositiveCtorWithClosure4()
    {
      var c = new ValueMapping(new object(), false);
    }
    [TestMethod]
    public void PositiveCtorWithClosure5()
    {
      var c = new ValueMapping("Hello", new[]{"Foo","Bar","Hello"});
    }
    [TestMethod]
    public void PositiveCtorWithClosure6()
    {
      var c = new ValueMapping("Hello", "Hello");
    }

    [TestMethod]
    public void NegativeCtorWithClosure1()
    {
      try
      {
        var c = new ValueMapping(null);
        throw new Exception();
      }
      catch (ArgumentException a)
      {
        Assert.AreEqual("Precondition failed: encoder != null", a.Message);
      }
    }
    [TestMethod]
    public void NegativeCtorWithClosure2()
    {
      try
      {
        var c = new ValueMapping(null, false);
        throw new Exception();
      }
      catch (ArgumentException a)
      {
        Assert.AreEqual("Precondition failed: encoder != null", a.Message);
      }
    }
    [TestMethod]
    public void NegativeCtorWithClosure3()
    {
      try
      {
        var c = new ValueMapping(null, (string[])null);
        throw new Exception();
      }
      catch (ArgumentException a)
      {
        Assert.AreEqual("Precondition failed: encoder != null", a.Message);
      }
    }
    [TestMethod]
    public void NegativeCtorWithClosure4()
    {
      try
      {
        var c = new ValueMapping("Hello", new[]{"A","b","D"});
        throw new Exception();
      }
      catch (TestRewriterMethods.PostconditionException a)
      {
        Assert.AreEqual(": Contract.Exists(0, data.Length, i => data[i] == this.name)", a.Message);
      }
    }
    [TestMethod]
    public void NegativeCtorWithClosure5()
    {
      try
      {
        var c = new ValueMapping(null, (string)null);
        throw new Exception();
      }
      catch (ArgumentException a)
      {
        Assert.AreEqual("Precondition failed: encoder != null", a.Message);
      }
    }

    /// <summary>
    /// Structs
    /// </summary>
    [TestMethod]
    public void PositiveStructCtorWithClosure1()
    {
      var c = new StructCtors("hello");
    }
    [TestMethod]
    public void PositiveStructCtorWithClosure2()
    {
      var c = new StructCtors(new Object());
    }
    [TestMethod]
    public void PositiveStructCtorWithClosure3()
    {
      var c = new StructCtors("hello", true);
    }
    [TestMethod]
    public void PositiveStructCtorWithClosure4()
    {
      var c = new StructCtors(new object(), false);
    }
    [TestMethod]
    public void PositiveStructCtorWithClosure5()
    {
      var c = new StructCtors("Hello", new[] { "Foo", "Bar", "Hello" });
    }
    [TestMethod]
    public void PositiveStructCtorWithClosure6()
    {
      var c = new StructCtors("Hello", "Hello");
    }

    [TestMethod]
    public void NegativeStructCtorWithClosure1()
    {
      try
      {
        var c = new StructCtors(null);
        throw new Exception();
      }
      catch (ArgumentException a)
      {
        Assert.AreEqual("Precondition failed: encoder != null", a.Message);
      }
    }
    [TestMethod]
    public void NegativeStructCtorWithClosure2()
    {
      try
      {
        var c = new StructCtors(null, false);
        throw new Exception();
      }
      catch (ArgumentException a)
      {
        Assert.AreEqual("Precondition failed: encoder != null", a.Message);
      }
    }
    [TestMethod]
    public void NegativeStructCtorWithClosure3()
    {
      try
      {
        var c = new StructCtors(null, (string[])null);
        throw new Exception();
      }
      catch (ArgumentException a)
      {
        Assert.AreEqual("Precondition failed: encoder != null", a.Message);
      }
    }
    [TestMethod]
    public void NegativeStructCtorWithClosure4()
    {
      try
      {
        var c = new StructCtors("Hello", new[] { "A", "b", "D" });
        throw new Exception();
      }
      catch (TestRewriterMethods.PostconditionException a)
      {
        Assert.AreEqual(": Contract.Exists(0, data.Length, i => data[i] == (encoder as string))", a.Message);
      }
    }
    [TestMethod]
    public void NegativeStructCtorWithClosure5()
    {
      try
      {
        var c = new StructCtors(null, (string)null);
        throw new Exception();
      }
      catch (ArgumentException a)
      {
        Assert.AreEqual("Precondition failed: encoder != null", a.Message);
      }
    }

    #endregion Tests
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
  [TestClass]
  public class IteratorSimpleContract {

    #region auxiliary test cases
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IIncrementable<T> 
    {
      T IncrementBy(int i);
      int Value();
    }

    public class A : IIncrementable<A> {
      public A(string s) {
        this.s = s;
      }
      string s;
      public int Value() {
        return s.Length;
      }
      public A IncrementBy(int i) {
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
      public IEnumerable<string> Test1a(string s) {
        Contract.Requires(s != null);
        Contract.Ensures(Contract.Result<IEnumerable<string>>() != null);
        Contract.Ensures(Contract.ForAll(Contract.Result<IEnumerable<string>>(), (string s1) => s1 != null));
        yield return "hello";
      }

      /// <summary>
      /// Same as Test1a, except that the forall postcondition is intentionally violated.
      /// </summary>
      public IEnumerable<string> Test1aa(string s) {
        Contract.Requires(s != null);
        Contract.Ensures(Contract.Result<IEnumerable<string>>() != null);
        Contract.Ensures(Contract.ForAll(Contract.Result<IEnumerable<string>>(), (string s1) => s1 != null));
        yield return null;
      }

      /// <summary>
      /// Contract for iterator method: Legacy precondition, with EndContractBlock().
      /// </summary>
      /// <param name="s"></param>
      /// <returns></returns>
      public IEnumerable<string> Test1b(string s) {
        if (s == null) throw new ArgumentException("");
        Contract.EndContractBlock();
        yield return s;
      }

      public IEnumerable<T> Test1c<T>(T t, IEnumerable<T> ts) {
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
      public IEnumerable<T> Test1d<T>(IEnumerable<T> input) {
        Contract.Requires(input != null);
        Contract.Requires(Contract.ForAll(input, (T s1) => s1 !=null));
        Contract.Ensures(Contract.ForAll(Contract.Result<IEnumerable<T>>(), (T s1) => s1 != null));
        foreach (T t in input)
          yield return t;
      }

      /// <summary>
      /// Contract for iterator method with type parameters and input ienumerable, precondition is forall. 
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="input"></param>
      public IEnumerable<T> Test1e<T>(IEnumerable<T> input) {
        Contract.Requires(Contract.ForAll(input, (T s) => s != null));
        foreach (T t in input) {
          yield return t;
        }
      }

      /// <summary>
      /// Contract for iterator method with closure that captures a parameter. 
      /// </summary>
      /// <param name="inputArray"></param>
      /// <param name="max"></param>
      public IEnumerable<int> Test1f(IEnumerable<int> inputArray, int max) {
        Contract.Requires(Contract.ForAll(inputArray, (int x) => x < max));
        Contract.Ensures(Contract.ForAll(Contract.Result<IEnumerable<int>>(), (int y) => y > 0));
        foreach (int i in inputArray) {
          yield return (max - i -1);
        }
      }

      /// <summary>
      /// Generic method: precondition with instance closure that captures a parameter;
      /// </summary>
      /// <param name="x"></param>
      /// <returns></returns>
      public IEnumerable<T> Test1g<T>(IEnumerable<T> ts, T x) {
        Contract.Requires(Contract.ForAll(ts, (T y) => foo(y,x)));
        yield return x;
      }

      bool foo(object y, object x) {
        return y==x;
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
        where T : IIncrementable<T> {
        Contract.Requires(Contract.ForAll(input, (T t) => t.Value() > x));
        Contract.Ensures(Contract.ForAll(Contract.Result<IEnumerable<T>>(), (T t) => t.Value() > (x+y)));
        foreach (T t in input) {
          yield return t.IncrementBy(y);
        }
      }

      public void Test2h() {
        A a = new A("hello");
        A [] aarray = {a};
        Test1h(aarray, 4, 5);
      }
    }
    #endregion Non-generic Class
    #region Generic Class
   
    public class Test3<T> 
      where T: class, IIncrementable<T>
    {
      public Test3(T t) {
        tfield = t;
      }

      /// <summary>
      /// non-generic method & pre/postcondition & pre positive/negative & static closure)
      /// </summary>
      public IEnumerable<T> Test1a(T t) {
        Contract.Requires(t != null);
        Contract.Ensures(Contract.Result<IEnumerable<T>>() != null);
        Contract.Ensures(Contract.ForAll(Contract.Result<IEnumerable<T>>(), (T s1) => s1 != null));
        yield return t;
      }

      /// <summary>
      /// Same as Test1a, except that the forall postcondition is intentionally violated.
      /// </summary>
      public IEnumerable<T> Test1aa(T s) {
        Contract.Ensures(Contract.Result<IEnumerable<T>>() != null);
        Contract.Ensures(Contract.ForAll(Contract.Result<IEnumerable<T>>(), (T s1) => s1 != null));
        yield return null;
      }

      /// <summary>
      /// Contract for iterator method: Legacy precondition, with EndContractBlock().
      /// </summary>
      /// <param name="s"></param>
      /// <returns></returns>
      public IEnumerable<T> Test1b(T s) {
        if (s == null) throw new ArgumentException("");
        Contract.EndContractBlock();
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
        where T1: IIncrementable<T1>
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
        where T1: IIncrementable<T1>
      {
        Contract.Requires(Contract.ForAll(ts, (T1 y) => foo(y, x)));
        foreach (T1 t1 in ts) yield return t1;
      }

      bool foo<S>(IIncrementable<S> y, T x) {
        return y.Value() == x.Value();
      }

      T tfield;

      public T TField {
        get {
          return tfield;
        }
      }

      public IEnumerable<T> Test1h(IEnumerable<T> input)
      {
        Contract.Ensures(Contract.ForAll(Contract.Result<IEnumerable<T>>(), (T t) => t.Value() > TField.Value()));
        foreach (T t in input) {
          yield return t.IncrementBy(2);
        }
      }
    }
    #endregion 
    #region Generic Struct
    public struct Test4<T>
      where T : class, IIncrementable<T> {
      public Test4(T t) {
        tfield = t;
      }

      /// <summary>
      /// non-generic method & pre/postcondition & pre positive/negative & static closure)
      /// </summary>
      public IEnumerable<T> Test1a(T t) {
        Contract.Requires(t != null);
        Contract.Ensures(Contract.Result<IEnumerable<T>>() != null);
        Contract.Ensures(Contract.ForAll(Contract.Result<IEnumerable<T>>(), (T s1) => s1 != null));
        yield return t;
      }

      /// <summary>
      /// Same as Test1a, except that the forall postcondition is intentionally violated.
      /// </summary>
      public IEnumerable<T> Test1aa(T s) {
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
        where T1 : IIncrementable<T1> {
        Contract.Requires(Contract.ForAll(input, (T s) => s.Value() == t.Value()));
        yield return t;
      }
      T tfield;
    }
    #endregion 
    #region IEnumerator
    public IEnumerator<int> Test5a(int x) {
      Contract.Requires(x > 0);
      // not working -- Contract.Ensures(Contract.ForAll(Contract.Result<IEnumerator<int>>(), (int y) => y > 0));
      for (int i = x; i < 10; i++) {
        yield return i;
      }
    }
    #endregion
    #endregion Contracts
    #region Test Drivers
    #region Non-generic class drivers
    /// <summary>
    /// This driver passes the preconditions of Test1a.
    /// </summary>
    [TestMethod]
    public void IteratorPositiveNN() {
      Test2 test2 = new Test2();
      test2.Test1a("hello"); // Precondition of Test1a holds
    }

    /// <summary>
    /// This driver violates the precondition of Test1a.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void IteratorNegativeNN() {
      Test2 test2 = new Test2();
      test2.Test1a(null); // Precondition of Test1a violated.
    }

    /// <summary>
    /// This driver catches the postcondition violation of Test1aa.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void IteratorNegativePostAllNN() {
      Test2 test2 = new Test2();
      test2.Test1aa("hello"); // Postcondition of Test1aa is invalid. 
    }

    /// <summary>
    /// This driver tests a legacy precondition
    /// Legacy contract throws an exception when violated (and is caught by the driver).
    /// </summary>
    [TestMethod]
    public void IteratorNegativeLegacyArgNN() {
      Test2 test2 = new Test2();
      try {
        test2.Test1b(null);
      } catch (ArgumentException) {
        // An ArgumentException is thrown by the legacy precondition. 
      }
    }

    [TestMethod]
    public void IteratorPositiveLegacyArgNN() {
      Test2 test2 = new Test2();
      test2.Test1b("hello");
    }

    /// <summary>
    /// This driver fails the first precondition of Test1d.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void IteratorNegativeCollectionNN() {
      Test2 test2 = new Test2();
      IEnumerable<string> x = null; 
      test2.Test1d(x);
    }

    /// <summary>
    /// This driver fails the second precondition of Test1d.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void IteratorNegativeElemNN() {
      Test2 test2 = new Test2();
      string[] strs = {null};
      test2.Test1d(strs);
    }

    /// <summary>
    /// This driver passes the preconditions of Test1d.
    /// </summary>
    [TestMethod]
    public void IteratorPositivePostElemNN() {
      Test2 test2 = new Test2();
      string[] strs = { "hello" };
      test2.Test1d(strs);
    }

    /// <summary>
    /// This driver passes preconditions of Test1f.
    /// </summary>
    [TestMethod]
    public void IteratorPositiveElemLTMax() {
      Test2 test2 = new Test2();
      int[] xs = { 2, 3, 4, 8 };
      test2.Test1f(xs, 10);
    }

    /// <summary>
    /// This driver fails the precondition of Test1f.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void IteratorNegativeElemLTMax() {
      Test2 test2 = new Test2();
      int[] xs = { 2, 3, 4, 11 };
      test2.Test1f(xs, 10);
    }

    /// <summary>
    /// This driver catches the postcondition violation of Test1f.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void IteratorNegativePostElemLTMax() {
      Test2 test2 = new Test2();
      int[] xs = { 2, 3, 4, 9 };
      test2.Test1f(xs, 10);
    }

    /// <summary>
    /// This driver passes Test1g.
    /// </summary>
    [TestMethod]
    public void IteratorPositivePostElemEqParam2() {
      Test2 test2 = new Test2();
      string[] strs = { "hello", "hello" };
      test2.Test1g(strs, "hello");
    }

    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void IteratorNegativePostElemEqParam2() {
      Test2 test2 = new Test2();
      string[] strs = { "aaa", "hello" };
      test2.Test1g(strs, "hello");
    }

    /// <summary>
    /// This driver catches the postcondition violation of Test1h. 
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void IteratorNegativePostElemLTMaxConstrained() {
      Test2 test2 = new Test2();
      test2.Test2h();
    }
    #endregion NonGeneric Class Drivers
    #region Generic Class Drivers
    [TestMethod]
    public void IteratorGenClsPosNN() {
      Test3<A> test3 = new Test3<A>(new A("hello"));
      test3.Test1a(new A("hello"));
    }

    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void IteratorGenClsNegNN() {
      Test3<A> test3 = new Test3<A>(new A("hello"));
      test3.Test1a(null);
    }

    /// <summary>
    /// This driver catches the postcondition violation of Test1aa.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void IteratorGenClsNegPostAllNN() {
      Test3<A> test3 = new Test3<A>(new A("hello"));
      test3.Test1aa(new A(""));
    }

    /// <summary>
    /// This driver tests a legacy precondition
    /// Legacy contract throws an exception when violated (and is caught by the driver).
    /// </summary>
    [TestMethod]
    public void IteratorGenClsNegLegacyArgNN() {
      Test3<A> test3 = new Test3<A>(new A("hello"));
      try {
        test3.Test1b(null);
      } catch (ArgumentException) {
        // An ArgumentException is thrown by the legacy precondition. 
      }
    }

    [TestMethod]
    public void IteratorGenClsPosLegacyArgNN() {
      Test3<A> test3 = new Test3<A>(new A("hello"));
      test3.Test1b(new A("hello"));
    }  
    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void IteratorGenClsMethNegCollectionNN() {
      Test3<A> test3 = new Test3<A>(null);
      IEnumerable<A> x = null;
      test3.Test1d(x);
    }

    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void IteratorGenClsMethNegElemNN() {
      Test3<A> test3 = new Test3<A>(null);
      A[] aa = { null };
      test3.Test1d(aa);
    }

    [TestMethod]
    public void IteratorGenClsMethPosPostElemNN() {
      Test3<A> test3 = new Test3<A>(null);
      A[] strs = { new A("hello") };
      test3.Test1d(strs);
    }

    [TestMethod]
    public void IteratorGenClsMethPosElemMethEqParamMeth() {
      Test3<A> test3 = new Test3<A>(null);
      A a = new A("hello");
      A[] aa = { new A("12345"), new A("54321") };
      test3.Test1e(aa, a);
    }

    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void IteratorGenClsMethNegElemMethEqParamMeth() {
      Test3<A> test3 = new Test3<A>(null);
      A a = new A("hello");
      A[] aa = { new A("12345"), new A("5432") };
      test3.Test1e(aa, a);
    }

    [TestMethod]
    public void IteratorGenClsMethInsClPosElemEqParam() {
      Test3<A> test3 = new Test3<A>(null);
      A a = new A("hello");
      A[] aa = { new A("12345"), new A("54321") };
      test3.Test1g(aa, a);
    }

    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void IteratorGenClsMethInsClNegElemEqParam() {
      Test3<A> test3 = new Test3<A>(null);
      A a = new A("hello");
      A[] aa = { new A("12345"), new A("321") };
      test3.Test1g(aa, a);
    }

    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void IteratorGenClsMethInsClNegPostElemLTProp() {
      Test3<A> test3 = new Test3<A>(new A("hello"));
      A[] aa = { new A("45"), new A("321") };
      test3.Test1h(aa);
    }

    [TestMethod]
    public void IteratorGenClsMethPostElemLTProp() {
      Test3<A> test3 = new Test3<A>(new A("hello"));
      A[] aa = { new A("1234567"), new A("213456") };
      test3.Test1h(aa);
    }
    #endregion Generic Class Drivers
    #region Generic Struct Drivers
    [TestMethod]
    public void IteratorGenStrPosNN() {
      Test4<A> test4 = new Test4<A>(new A("hello"));
      test4.Test1a(new A("hello"));
    }

    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void IteratorGenStrNegNN() {
      Test4<A> test4 = new Test4<A>(new A("hello"));
      test4.Test1a(null);
    }

    /// <summary>
    /// This driver catches the postcondition violation of Test1aa.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void IteratorGenStrNegPostAllNN() {
      Test4<A> test4 = new Test4<A>(new A("hello"));
      test4.Test1aa(new A(""));
    }

    [TestMethod]
    public void IteratorGenStrMethPosElemMethEqParamMeth() {
      Test4<A> test4 = new Test4<A>(null);
      A a = new A("hello");
      A[] aa = { new A("12345"), new A("54321") };
      test4.Test1e(aa, a);
    }

    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void IteratorGenStrMethNegElemMethEqParamMeth() {
      Test4<A> test4 = new Test4<A>(null);
      A a = new A("hello");
      A[] aa = { new A("12345"), new A("5432") };
      test4.Test1e(aa, a);
    }

    #endregion Generic Struct Drivers
    #region IEnumerator Drivers
    [TestMethod]
    public void IEnumeratorPos() {
      Test5a(2);
    }

    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void IEnumeratorNeg() {
      Test5a(0);
    }
    #endregion 
    #endregion Test Drivers
  }

  [TestClass]
  public class LocalVariableForResult
  {
    #region Test Management
    [ClassInitialize]
    public static void ClassInitialize(TestContext context)
    {
      object[] attrs = typeof(RewrittenInheritanceBase).Assembly.GetCustomAttributes(true);
      bool found = false;
      foreach (var a in attrs)
      {
        Attribute attr = a as Attribute;
        if (attr == null) continue;
        Type ty = attr.TypeId as Type;
        if (ty == null) continue;
        if (ty.FullName == "System.Diagnostics.Contracts.RuntimeContractsAttribute")
        {
          found = true;
          break;
        }
      }
      Assert.IsTrue(found, "This assembly must have been rewritten before running these tests.  This is usually done in the post build script of the test project.");
    }

    [ClassCleanup]
    public static void ClassCleanup()
    {
    }

    [TestInitialize]
    public void TestInitialize()
    {
    }

    [TestCleanup]
    public void TestCleanup()
    {
    }
    #endregion Test Management

    #region Contracts
    public class C
    {
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

    #region Tests
    [TestMethod]
    public void PositiveLocalAsResult1()
    {
      var c = new C();
      c.Test1(1);
    }
    public void PositiveLocalAsResult2()
    {
      var c = new C();
      c.Test2(1, new bool[] { });
    }
    [TestMethod]
    public void PositiveLocalAsResult3()
    {
      var c = new C();
      c.Test2(1, new bool[] { true, true, true });
    }
    [TestMethod]
    public void NegativeLocalAsResult1()
    {
      try
      {
        var c = new C();
        c.Test1(2);
        throw new Exception();
      }
      catch (TestRewriterMethods.PostconditionException p)
      {
        Assert.AreEqual(": result != false", p.Message);
      }
    }
    [TestMethod]
    public void NegativeLocalAsResult2()
    {
      try
      {
        var c = new C();
        c.Test2(2, new bool[] { });
        throw new Exception();
      }
      catch (TestRewriterMethods.PostconditionException p)
      {
        Assert.AreEqual(": result != false", p.Message);
      }
    }
    [TestMethod]
    public void NegativeLocalAsResult3()
    {
      try
      {
        var c = new C();
        c.Test2(1, new bool[] { true, false });
        throw new Exception();
      }
      catch (TestRewriterMethods.PostconditionException p)
      {
        Assert.AreEqual(": Contract.ForAll(0, arr.Length, i => arr[i] == result)", p.Message);
      }
    }

    #endregion Tests
  }

  [TestClass]
  public class IgnoreRuntimeAttributeTest
  {
    #region Test Management
    [ClassInitialize]
    public static void ClassInitialize(TestContext context)
    {
      object[] attrs = typeof(IgnoreRuntimeAttributeTest).Assembly.GetCustomAttributes(true);
      bool found = false;
      foreach (var a in attrs) {
        Attribute attr = a as Attribute;
        if (attr == null) continue;
        Type ty = attr.TypeId as Type;
        if (ty == null) continue;
        if (ty.FullName == "System.Diagnostics.Contracts.RuntimeContractsAttribute")
        {
          found = true;
          break;
        }
      }
      Assert.IsTrue(found, "This assembly must have been rewritten before running these tests.  This is usually done in the post build script of the test project.");
    }

    [ClassCleanup]
    public static void ClassCleanup()
    {
    }

    [TestInitialize]
    public void TestInitialize()
    {
    }

    [TestCleanup]
    public void TestCleanup()
    {
    }
    #endregion Test Management

    #region Contracts
    public class C
    {
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

    #region Tests
    [TestMethod]
    public void PositiveIgnoreRuntimeAttributeTest()
    {
      var c = new C();
      c.M(3, true);
    }
    [TestMethod]
    public void NegativeIgnoreRuntimeAttributeTest()
    {
      try
      {
        var c = new C();
        c.M(2, false);
        throw new Exception();
      }
      catch (TestRewriterMethods.PostconditionException p)
      {
        Assert.AreEqual(": Contract.Result<bool>()", p.Message);
      }
    }

    #endregion Tests
  }

  /// <summary>
  /// All parameters mentioned in postconditions that are not already
  /// within an old expression should get wrapped in one so that
  /// their initial value is used in the postcondition in case the
  /// parameter is updated in the method body.
  /// </summary>
  [TestClass]
  public class WrapParametersInOld {
    #region Test Management
    [ClassInitialize]
    public static void ClassInitialize(TestContext context) {
      object[] attrs = typeof(WrapParametersInOld).Assembly.GetCustomAttributes(true);
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
      int a;

      public C(int b) {
        Contract.Requires(b == 3);
        Contract.Ensures(this.a == b);
        Contract.Ensures(b == 3);
        a = b;
        b = 27;
      }
      public bool Identity(bool b) {
        Contract.Ensures(Contract.Result<bool>() == b);
        var result = b;
        b = !b;
        return result;
      }

      public struct S {
        public int x;
      }

      public void F(S s) {
        Contract.Requires(s.x == 3);
        Contract.Ensures(s.x == 3);
        s.x++;
      }
    }
    #endregion Contracts

    #region Tests
    [TestMethod]
    public void PositiveWrapParam1() {
      var c = new C(3);
      c.Identity(true);
    }
    [TestMethod]
    public void PositiveWrapParam2() {
      var c = new C(3);
      c.Identity(false);
    }
    [TestMethod]
    public void PositiveWrapParam3() {
      var c = new C(3);
      var s = new C.S();
      s.x = 3;
      c.F(s);
    }
    #endregion Tests
  }

}