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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.Contracts;
using System.Linq;
using CodeUnderTest;
using CodeUnderTest.PDC;

namespace UserFeedback {
  namespace Peli {
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class UnitTest1 : Tests.DisableAssertUI {

      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void TestProperlyRewrittenResult()
      {
        new CodeUnderTest.Peli.UnitTest1.Foo().GetValue(0);
      }

      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void TestInvariantStrings1()
      {
        new CodeUnderTest.Peli.UnitTest1.Foo(2);
      }

      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void TestInvariantStrings2()
      {
        try
        {
          new CodeUnderTest.Peli.UnitTest1.Foo(0);
          throw new Exception();
        }
        catch (TestRewriterMethods.InvariantException i)
        {
          Assert.AreEqual("x > 0", i.Condition);
          Assert.AreEqual(null, i.User);
        }
      }

      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void TestInvariantStrings3()
      {
        try
        {
          new CodeUnderTest.Peli.UnitTest1.Foo(10);
          throw new Exception();
        }
        catch (TestRewriterMethods.InvariantException i)
        {
          Assert.AreEqual("x < 10", i.Condition);
          Assert.AreEqual("upper bound", i.User);
        }
      }



      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      [ExpectedException(typeof(ArgumentException))]
      public void TestOnThrowPositive()
      {
        CodeUnderTest.Peli.UnitTest1.Bar b = new CodeUnderTest.Peli.UnitTest1.Bar(0);
        new CodeUnderTest.Peli.UnitTest1.Foo().TestException(b, 0);
      }

      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void TestOnThrowNegative1()
      {
        CodeUnderTest.Peli.UnitTest1.Bar b = new CodeUnderTest.Peli.UnitTest1.Bar(0);
        try
        {
          new CodeUnderTest.Peli.UnitTest1.Foo().TestException(b, 1);
          throw new Exception();
        }
        catch (TestRewriterMethods.PostconditionOnThrowException p)
        {
          Assert.AreEqual("b.ID == 0", p.Condition);
          Assert.AreEqual("Peli3", p.User);
        }
      }

      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void TestOnThrowNegative2()
      {
        CodeUnderTest.Peli.UnitTest1.Bar b = new CodeUnderTest.Peli.UnitTest1.Bar(0);
        try
        {
          new CodeUnderTest.Peli.UnitTest1.Foo().TestException(b, -1);
          throw new Exception();
        }
        catch (TestRewriterMethods.PostconditionOnThrowException p)
        {
          Assert.AreEqual("b.ID >= 0", p.Condition);
          Assert.AreEqual(null, p.User);
        }
      }



      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void TestRequiresConditionStringAndUserString()
      {
        try
        {
          new CodeUnderTest.Peli.UnitTest1.Foo().GetValue(-1);
          throw new Exception();
        }
        catch (TestRewriterMethods.PreconditionException e)
        {
          if (e.Condition != "i >= 0") throw new Exception();
          if (e.User != "peli1") throw new Exception();
          return;
        }
      }

      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void TestRequiresConditionStringOnly()
      {
        try
        {
          new CodeUnderTest.Peli.UnitTest1.Foo().GetValue(10);
          throw new Exception();
        }
        catch (TestRewriterMethods.PreconditionException e)
        {
          if (e.Condition != "i < 10") throw new Exception();
          if (e.User != null) throw new Exception();
          return;
        }
      }

      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void TestEnsuresConditionStringAndUserString()
      {
        try
        {
          new CodeUnderTest.Peli.UnitTest1.Foo().GetValue(1);
        }
        catch (TestRewriterMethods.PostconditionException e)
        {
          Assert.AreEqual("Contract.Result<Bar>() == null || Contract.Result<Bar>().ID == 0", e.Condition);
          Assert.AreEqual("peli2", e.User);
          return;
        }
      }
    }
  }

  namespace WinSharp {
    using System.Linq;

    [TestClass]
      public class RecursionChecks : Tests.DisableAssertUI
      {
      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void RecursionTest1()
      {
        var mydict = new CodeUnderTest.WinSharp.MyDict<string, int>();

        var result = mydict.ContainsKey("foo");

        Assert.IsFalse(result);
      }

      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void RecursionTest2()
      {
        var result = CodeUnderTest.WinSharp.ExplicitlyRecursive.Odd(5);

        Assert.IsTrue(result);
      }

      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void RecursionTest3()
      {
        var result = CodeUnderTest.WinSharp.ExplicitlyRecursive.Odd(6);

        Assert.IsFalse(result);
      }

      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void RecursionTest4()
      {
        var result = CodeUnderTest.WinSharp.ExplicitlyRecursive.Even(7);

        Assert.IsFalse(result);
      }

      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void RecursionTest5()
      {
        var result = CodeUnderTest.WinSharp.ExplicitlyRecursive.Even(8);

        Assert.IsTrue(result);
      }


      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void RecursionTest6()
      {
        var result = CodeUnderTest.WinSharp.ExplicitlyRecursive.SubEven(8);

        Assert.AreEqual(0, result);
      }

      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void RecursionTest7()
      {
        var result = CodeUnderTest.WinSharp.ExplicitlyRecursive.SubEven(5);

        Assert.AreEqual(0, result);
      }

      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void RecursionTest8()
      {
        var result = CodeUnderTest.WinSharp.ExplicitlyRecursive.SubOdd(8);

        Assert.AreEqual(0, result);
      }

      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void RecursionTest9()
      {
        var result = CodeUnderTest.WinSharp.ExplicitlyRecursive.SubOdd(5);

        Assert.AreEqual(0, result);
      }

      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void RecursionTest10()
      {
        var result = CodeUnderTest.WinSharp.ExplicitlyRecursive.ThrowOnEven(5);

        Assert.IsFalse(result);
      }

      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      [ExpectedException(typeof(ArgumentException))]
      public void RecursionTest11()
      {
        var result = CodeUnderTest.WinSharp.ExplicitlyRecursive.ThrowOnEven(4);

        Assert.IsFalse(true);
      }

      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void RecursionTest12()
      {
        var result = CodeUnderTest.WinSharp.ExplicitlyRecursive.ThrowOnOdd(4);

        Assert.IsFalse(result);
      }

      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      [ExpectedException(typeof(ArgumentException))]
      public void RecursionTest13()
      {
        var result = CodeUnderTest.WinSharp.ExplicitlyRecursive.ThrowOnOdd(3);

        Assert.IsFalse(true);
      }

    }

  }

  namespace Multani {
    [TestClass]
      public class MultaniTestClass1 : Tests.DisableAssertUI
      {
      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void PositiveMultani1()
      {
        double[] initialValues = new[] { 1.0, 2.0, 3.0 };
        double[] stDevs = new[] { 0.1, 0.2, 0.3 };
        double[] drifts = new[] { 0.1, 0.1, 0.1 };
        double[,] correlations = new double[3, 3] { { 0.1, 0.1, 0.1 }, { 0.1, 0.1, 0.1 }, { 0.1, 0.1, 0.1 } };
        int randomSeed = 44;
        var c = new CodeUnderTest.Multani.CorrelatedGeometricBrownianMotionFuelPriceSimulator(initialValues, stDevs, drifts, correlations, randomSeed);
      }
      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
      public void NegativeMultani1()
      {
        double[] initialValues = null;
        double[] stDevs = new[] { 0.1, -0.2, 0.3 };
        double[] drifts = null;
        double[,] correlations = null;
        int randomSeed = 44;
        var c = new CodeUnderTest.Multani.CorrelatedGeometricBrownianMotionFuelPriceSimulator(initialValues, stDevs, drifts, correlations, randomSeed);
      }
    }
  }

  namespace Somebody {
    [TestClass]
    public class TestResourceStringUserMessage : Tests.DisableAssertUI {

      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void PositiveTestUserMessages()
      {
        var o = new CodeUnderTest.Somebody.TestResourceStringUserMessage();
        o.RequiresWithInternalResourceMessage("hello");
        o.RequiresWithPublicFieldMessage("hello");
        o.RequiresWithPublicGetterMessage("hello");
      }

      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void NegativeTestInternalUserMessageString()
      {
        try
        {
          var o = new CodeUnderTest.Somebody.TestResourceStringUserMessage();
          o.RequiresWithInternalResourceMessage(null);
          throw new Exception();
        }
        catch (TestRewriterMethods.PreconditionException p)
        {
          // resource not as visible as the contract method
          Assert.AreEqual("Can't call me with null", p.User);
        }
      }

      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void NegativeTestPublicFieldUserMessageString()
      {
        try
        {
          var o = new CodeUnderTest.Somebody.TestResourceStringUserMessage();
          o.RequiresWithPublicFieldMessage(null);
          throw new Exception();
        }
        catch (TestRewriterMethods.PreconditionException p)
        {
          // resource not as visible as the contract method
          Assert.AreEqual(CodeUnderTest.Somebody.TestResourceStringUserMessage.Message2, p.User);
        }
      }

      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void NegativeTestPublicGetterUserMessageString()
      {
        try
        {
          var o = new CodeUnderTest.Somebody.TestResourceStringUserMessage();
          o.RequiresWithPublicGetterMessage(null);
          throw new Exception();
        }
        catch (TestRewriterMethods.PreconditionException p)
        {
          // resource not as visible as the contract method
          Assert.AreEqual(CodeUnderTest.Somebody.TestResourceStringUserMessage.Message3, p.User);
        }
      }

      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void NegativeLegacyThrowWithPrivateExceptionArgument()
      {
        try
        {
          var o = new CodeUnderTest.Somebody.TestResourceStringUserMessage();
          o.LegacyRequiresReferencingPrivateStuffInThrow(CodeUnderTest.Somebody.TypeDescriptorPermissionFlags.Other);
          throw new Exception();
        } catch (ArgumentException e)
        {
          // resource not as visible as the contract method
          Assert.AreEqual("Illegal value 2", e.Message);
        }
      }

      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void PositiveLegacyThrowWithPrivateExceptionArgument()
      {
        var o = new CodeUnderTest.Somebody.TestResourceStringUserMessage();
        o.LegacyRequiresReferencingPrivateStuffInThrow(CodeUnderTest.Somebody.TypeDescriptorPermissionFlags.RestrictedRegistrationAccess);
      }
    }
  }

  namespace PDC {

    [TestClass]
      public class DontCheckInvariantsDuringConstructors : Tests.DisableAssertUI
      {
      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void PositiveTrickyAutoProp1()
      {
        var tricky = new TrickyAutoPropInvariants<Valid>(new Valid(true), 0, false);
        tricky.Change(true, 5);
      }
      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void PositiveTrickyAutoProp2()
      {
        var tricky = new TrickyAutoPropInvariants<Valid>(new Valid(true), 0, false);
        try
        {
          tricky.LeaveObjectInconsistent();
        }
        catch (ApplicationException) { }
        // now we can violate invariant further without being punished
        tricky.Change(false, 5);
      }
      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void PositiveTrickyAutoProp3()
      {
        var tricky = new TrickyAutoPropInvariants<Valid>(new Valid(true), 0, false);
        try
        {
          tricky.LeaveObjectInconsistent();
        }
        catch (ApplicationException) { }
        // now we can violate invariant further without being punished
        tricky.Age = 5;
      }
      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void NegativeTrickyAutoProp1()
      {
        try
        {
          var tricky = new TrickyAutoPropInvariants<Valid>(new Valid(true), 0, false);
          tricky.Change(false, 5);
          throw new Exception();
        }
        catch (TestRewriterMethods.InvariantException i)
        {
          Assert.AreEqual("Flag ? Age > 0 : Age == 0 && Value.IsValid", i.Condition);
        }
      }
      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void NegativeTrickyAutoProp2()
      {
        try
        {
          var tricky = new TrickyAutoPropInvariants<Valid>(new Valid(true), 0, false);
          tricky.Age = 5;
          throw new Exception();
        }
        catch (TestRewriterMethods.PreconditionException i)
        {
          Assert.AreEqual("Flag ? Age > 0 : Age == 0 && Value.IsValid", i.Condition);
        }
      }
      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void NegativeTrickyAutoProp3()
      {
        try
        {
          var tricky = new TrickyAutoPropInvariants<Valid>(new Valid(true), 0, false);
          tricky.LeaveObjectConsistentOK();
          tricky.Age = 0; // should fail precondition
          throw new Exception();
        }
        catch (TestRewriterMethods.PreconditionException i)
        {
          Assert.AreEqual("Flag ? Age > 0 : Age == 0 && Value.IsValid", i.Condition);
        }
      }
      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void NegativeTrickyAutoProp3b() {
        try {
          var tricky = new TrickyAutoPropInvariants<Valid>(new Valid(true), 0, false);
          tricky.LeaveObjectConsistentWrong(); // will throw invariant
          throw new Exception();
        } catch (TestRewriterMethods.InvariantException i) {
          Assert.AreEqual("Flag ? Age > 0 : Age == 0 && Value.IsValid", i.Condition);
        }
      }
      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void NegativeTrickyAutoProp4()
      {
        try
        {
          var tricky = new TrickyAutoPropInvariants<Valid>(new Valid(true), 0, false);
          try
          {
            tricky.LeaveObjectConsistentViaAdvertisedException();
          }
          catch (ApplicationException) { }
          tricky.Age = 5; // should fail precondition
          throw new Exception();
        }
        catch (TestRewriterMethods.PreconditionException i)
        {
          Assert.AreEqual("Flag ? Age > 0 : Age == 0 && Value.IsValid", i.Condition);
        }
      }

      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void PositiveInvariantOffDuringConstruction1()
      {
        var p = new Invariants("Joe", 42);

      }


      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void NegativeInvariantOffDuringConstruction1()
      {
        try
        {
          var p = new Invariants(null, 1);
          throw new Exception();
        }
        catch (TestRewriterMethods.InvariantException i)
        {
          Assert.AreEqual("name != null", i.Condition);
        }
      }

      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void PositiveInvariantOffDuringConstruction2()
      {
        var p = new Invariants<string>("Joe", 42, 2, 1);

      }


      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void NegativeInvariantOffDuringConstruction2()
      {
        try
        {
          var p = new Invariants<string>("Joe", 0, 1, 2);
          throw new Exception();
        }
        catch (TestRewriterMethods.InvariantException i)
        {
          Assert.AreEqual("age > 0", i.Condition);
        }
      }

      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void NegativeInvariantOfAutoPropIntoRequires1()
      {
        try
        {
          var p = new Invariants<string>(null, 0, 1, 2);
          throw new Exception();
        }
        catch (TestRewriterMethods.InvariantException p)
        {
          Assert.AreEqual("Name != null", p.Condition);
        }
      }

      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void NegativeInvariantOfAutoPropIntoRequires2()
      {
        try
        {
          var p = new Invariants<string>("Joe", 0, 1, 2);
          throw new Exception();
        }
        catch (TestRewriterMethods.InvariantException p)
        {
          Assert.AreEqual("age > 0", p.Condition);
        }
      }

      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void NegativeInvariantOfAutoPropIntoRequires3()
      {
        try
        {
          var p = new Invariants<string>("Joe", 1, 1, 2);
          throw new Exception();
        }
        catch (TestRewriterMethods.InvariantException p)
        {
          Assert.AreEqual("X > Y", p.Condition);
        }
      }

      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void NegativeInvariantOfAutoPropIntoRequires4()
      {
        try
        {
          var p = new Invariants<string>("Joe", 1, 2, 1);
          p.Name = null;
          throw new Exception();
        }
        catch (TestRewriterMethods.PreconditionException p)
        {
          Assert.AreEqual("Name != null", p.Condition);
        }
      }

      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void NegativeInvariantOfAutoPropIntoRequires5()
      {
        try
        {
          var p = new Invariants<string>("Joe", 1, 2, 1);
          p.X = 1;
          throw new Exception();
        }
        catch (TestRewriterMethods.PreconditionException p)
        {
          Assert.AreEqual("X > Y", p.Condition);
        }
      }

      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void NegativeInvariantOfAutoPropIntoRequires6()
      {
        try
        {
          var p = new Invariants<string>("Joe", 1, 2, 1);
          p.Y = 2;
          throw new Exception();
        }
        catch (TestRewriterMethods.PreconditionException p)
        {
          Assert.AreEqual("X > Y", p.Condition);
        }
      }

    }
  }

  namespace Arnott {
    [TestClass]
      public class C : Tests.DisableAssertUI
      {
 
      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void PositiveRequiresWithException()
      {
        new CodeUnderTest.Arnott.C().M(this);
      }
      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      [ExpectedException(typeof(ArgumentNullException))]
      public void NegativeRequiresWithException()
      {
        new CodeUnderTest.Arnott.C().M(null);
      }

      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void WorksCorrectlyWithThisOrder()
      {
        new CodeUnderTest.Arnott.C().OkayOrder(new int[20]);
      }
      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      [ExpectedException(typeof(ArgumentOutOfRangeException))]
      public void WorksInCorrectlyWithThisOrder()
      {
        new CodeUnderTest.Arnott.C().BadOrder(new int[10]);
      }

    }

  }

  namespace DavidAllen {
    [TestClass]
      public class AutoPropProblem : Tests.DisableAssertUI
      {
      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void PositiveNoAutoPropTreatmentForImplsAndOverrides0(){
        new CodeUnderTest.DavidAllen.AutoPropProblem.Impl1().P = 1;
      }
      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void PositiveNoAutoPropTreatmentForImplsAndOverrides1() {
        CodeUnderTest.DavidAllen.AutoPropProblem.J j = new CodeUnderTest.DavidAllen.AutoPropProblem.Impl2();
        j.P = 1;
      }
      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void PositiveNoAutoPropTreatmentForImplsAndOverrides2() {
        new CodeUnderTest.DavidAllen.AutoPropProblem.Sub().P = 1;
      }
      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      [ExpectedException(typeof(TestRewriterMethods.InvariantException))]
      public void NegativeNoAutoPropTreatmentForImplsAndOverrides0() {
        // gets invariant exception but *not* precondition exception which
        // would indicate that the autoprop
        new CodeUnderTest.DavidAllen.AutoPropProblem.Impl1().P = -1;
      }
      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void PositiveNoAutoPropTreatmentForImplsAndOverrides3() {
        CodeUnderTest.DavidAllen.AutoPropProblem.J j = new CodeUnderTest.DavidAllen.AutoPropProblem.Impl2();
        j.P = 0; // no violation of invariant because explicit interface implementations don't get invariants checked
      }
      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      [ExpectedException(typeof(TestRewriterMethods.InvariantException))]
      public void NegativeNoAutoPropTreatmentForImplsAndOverrides1() {
        new CodeUnderTest.DavidAllen.AutoPropProblem.Sub().P = -1;
      }
    }
  }

  namespace MaF
  {
    [TestClass]
      public class TestContractCheckingOffOn : Tests.DisableAssertUI
    {
      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void NegativeTest0WhereCheckingIsOn()
      {
        Helpers.CheckEnsures(() =>
          new CodeUnderTest.MaF.TestContractCheckingOffOn().Test0("foo"),
          "Contract.Result<int>() > 0");
      }

      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void PositiveTest1WhereCheckingIsOff()
      {
        new CodeUnderTest.MaF.TestContractCheckingOffOn().Test1(null);
      }

      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void NegativeTest2WhereCheckingIsOn()
      {
        Helpers.CheckRequires(() =>
          new CodeUnderTest.MaF.TestContractCheckingOffOn.Nested().Test2(null),
          "s != null");
      }

      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void PositiveTest3WhereCheckingIsBackOff()
      {
        new CodeUnderTest.MaF.TestContractCheckingOffOn.Nested().Test3(null);
      }
    }

    [TestClass]
    public class TestContractInheritanceOffOn : Tests.DisableAssertUI 
    {
      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void NegativeTest0WhereInheritanceIsBackOn()
      {
        Helpers.CheckEnsures(() =>
          new CodeUnderTest.MaF.TestContractInheritanceOffOn().Test0("foo"),
          "Contract.Result<int>() > 0");
      }

      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void PositiveTest1WhereInheritanceIsOff()
      {
        new CodeUnderTest.MaF.TestContractInheritanceOffOn().Test1(null);
      }

      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void PositiveTest2WhereInheritanceIsOff()
      {
        new CodeUnderTest.MaF.TestContractInheritanceOffOn.Nested().Test2(null);
      }

      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void NegativeTest3WhereInheritanceIsBackOn()
      {
        Helpers.CheckRequires( () =>
          new CodeUnderTest.MaF.TestContractInheritanceOffOn().Test0(null),
          "s != null");
      }
    }

    [TestClass]
    public class TestOldArray : Tests.DisableAssertUI
    {
      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void TestOldParameterArrayPositive()
      {
        new CodeUnderTest.MaF.TestOldArrays().OldParameterArray(new[] { 1, 2, 3, 4, 5, 6 }, true);
      }
      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void TestOldParameterArrayNegative()
      {
        Helpers.CheckEnsures(() =>
          new CodeUnderTest.MaF.TestOldArrays().OldParameterArray(new[] { 1, 2, 3, 4, 5, 6 }, false),
          "Contract.ForAll(0, z.Length, i => z[i] == Contract.OldValue(z[i]))");
      }
      //[TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void TestOldThisFieldArrayPositive()
      {
        new CodeUnderTest.MaF.TestOldArrays().OldThisFieldArray(true);
      }
      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void TestOldThisFieldArrayNegative()
      {
        Helpers.CheckEnsures(() =>
          new CodeUnderTest.MaF.TestOldArrays().OldThisFieldArray(false),
          "Contract.ForAll(0, this.field.Length, i => this.field[i] == Contract.OldValue(this.field[i]))");
      }
      //[TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void TestOldThisFieldClosurePositive()
      {
        new CodeUnderTest.MaF.TestOldArrays().OldThisFieldClosure(true);
      }
      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void TestOldThisFieldClosureNegative()
      {
        Helpers.CheckEnsures(() =>
          new CodeUnderTest.MaF.TestOldArrays().OldThisFieldClosure(false),
          "Contract.ForAll(this.field, elem => elem == Contract.OldValue(elem))");
      }
      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void TestOldParameterFieldArrayPositive()
      {
        new CodeUnderTest.MaF.TestOldArrays().OldParameterFieldArray(new CodeUnderTest.MaF.TestOldArrays(), true);
      }
      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void TestOldParameterFieldArrayNegative()
      {
        Helpers.CheckEnsures(() =>
          new CodeUnderTest.MaF.TestOldArrays().OldParameterFieldArray(new CodeUnderTest.MaF.TestOldArrays(), false),
          "Contract.ForAll(0, other.field.Length, i => other.field[i] == Contract.OldValue(other.field[i]))");
      }
    }
    [TestClass]
    public class ClosuresInValidators : Tests.DisableAssertUI
    {
      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void TestCollectionValidatorPositive()
      {
        new CodeUnderTest.MaF.TestClosureValidators().TestCollectionValidator(new[] { "ab", "c", "d", "efg" }, new[] { "ab", "c", "d", "efg" });
      }
      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void TestCollectionValidatorNegative1()
      {
        Helpers.CheckException<ArgumentException>(() =>
          new CodeUnderTest.MaF.TestClosureValidators().TestCollectionValidator(new[] { "ab", null, "d", "efg" }, new[] { "ab", "c", "d", "efg" }),
          "all elements of coll1 must be non-null");
      }
      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void TestCollectionValidatorNegative2()
      {
        Helpers.CheckException<ArgumentException>(() =>
          new CodeUnderTest.MaF.TestClosureValidators().TestCollectionValidator(new[] { "ab", "c", "d", "efg" }, new[] { "ab", null, "d", "efg" }),
          "all elements of coll2 must be non-null");
      }
      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void TestArrayValidatorPositive()
      {
        new CodeUnderTest.MaF.TestClosureValidators().TestArrayValidator(new[] { "ab", "c", "d", "efg" }, new[] { "ab", "c", "d", "efg" });
      }
      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void TestArrayValidatorNegative1()
      {
        Helpers.CheckException<ArgumentException>(() =>
          new CodeUnderTest.MaF.TestClosureValidators().TestArrayValidator(new[] { "ab", null, "d", "efg" }, new[] { "ab", "c", "d", "efg" }),
          "all elements of array1 must be non-null");
      }
      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void TestArrayValidatorNegative2()
      {
        Helpers.CheckException<ArgumentException>(() =>
          new CodeUnderTest.MaF.TestClosureValidators().TestArrayValidator(new[] { "ab", "c", "d", "efg" }, new[] { "ab", null, "d", "efg" }),
          "all elements of array2 must be non-null");
      }

      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void TestCollectionValidator2Positive() {
        new CodeUnderTest.MaF.TestClosureValidators().TestCollectionValidator2(new[] { "ab", "c", "d", "efg" }, new[] { "ab", "c", "d", "efg" });
      }
      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void TestCollectionValidator2Negative1() {
        Helpers.CheckException<ArgumentException>(() =>
          new CodeUnderTest.MaF.TestClosureValidators().TestCollectionValidator2(new[] { "ab", null, "d", "efg" }, new[] { "ab", "c", "d", "efg" }),
          "all elements of coll1 must be non-null");
      }
      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void TestCollectionValidator2Negative2() {
        Helpers.CheckException<ArgumentException>(() =>
          new CodeUnderTest.MaF.TestClosureValidators().TestCollectionValidator2(new[] { "ab", "c", "d", "efg" }, new[] { "ab", null, "d", "efg" }),
          "all elements of coll2 must be non-null");
      }
      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void TestArrayValidator2Positive() {
        new CodeUnderTest.MaF.TestClosureValidators().TestArrayValidator2(new[] { "ab", "c", "d", "efg" }, new[] { "ab", "c", "d", "efg" });
      }
      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void TestArrayValidator2Negative1() {
        Helpers.CheckException<ArgumentException>(() =>
          new CodeUnderTest.MaF.TestClosureValidators().TestArrayValidator2(new[] { "ab", null, "d", "efg" }, new[] { "ab", "c", "d", "efg" }),
          "all elements of array1 must be non-null");
      }
      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void TestArrayValidator2Negative2() {
        Helpers.CheckException<ArgumentException>(() =>
          new CodeUnderTest.MaF.TestClosureValidators().TestArrayValidator2(new[] { "ab", "c", "d", "efg" }, new[] { "ab", null, "d", "efg" }),
          "all elements of array2 must be non-null");
      }

      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void TestFieldArrayAbbreviatorPositive()
      {
        new CodeUnderTest.MaF.TestClosureValidators(new[] { "ab", "c", "d", "efg" }).TestFieldArrayUnchanged();
      }
      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void TestFieldArrayAbbreviatorNegative()
      {
        Helpers.CheckEnsures(() =>
          new CodeUnderTest.MaF.TestClosureValidators(new[] { "ab", null, "d", "efg" }).TestFieldArrayUnchanged(),
          "Contract.ForAll(0, this.field.Length, i => this.field[i] != null)");
      }
      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void TestFieldAsCollectionAbbreviatorPositive()
      {
        new CodeUnderTest.MaF.TestClosureValidators(new[] { "ab", "c", "d", "efg" }).TestFieldAsCollectionUnchanged();
      }
      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void TestFieldAsCollectionAbbreviatorNegative()
      {
        Helpers.CheckEnsures(() =>
          new CodeUnderTest.MaF.TestClosureValidators(new[] { "ab", null, "d", "efg" }).TestFieldAsCollectionUnchanged(),
          "Contract.ForAll(this.field, elem => elem != null)");
      }


#if false
      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void TestPassedFieldArrayValidatorPositive()
      {
        CodeUnderTest.MaF.CallerClass.TestFieldArrayUnchanged(new CodeUnderTest.MaF.TestClosureValidators(new[] { "ab", "c", "d", "efg" }));
      }
      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void TestPassedFieldArrayValidatorNegative()
      {
        Helpers.CheckException<ArgumentException>(() =>
          CodeUnderTest.MaF.CallerClass.TestFieldArrayUnchanged(new CodeUnderTest.MaF.TestClosureValidators(new[] { "ab", "c", null, "efg" })),
          "all elements of array must be non-null");
      }
      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void TestPassedFieldAsCollectionValidatorPositive()
      {
        CodeUnderTest.MaF.CallerClass.TestFieldAsCollectionUnchanged(new CodeUnderTest.MaF.TestClosureValidators(new[] { "ab", "c", "d", "efg" }));
      }
      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void TestPassedFieldAsCollectionValidatorNegative()
      {
        Helpers.CheckException<ArgumentException>(() =>
          CodeUnderTest.MaF.CallerClass.TestFieldAsCollectionUnchanged(new CodeUnderTest.MaF.TestClosureValidators(new[] { "ab", "c", null, "efg" })),
          "all elements of array must be non-null");
      }
#endif
    }

    [TestClass]
    public class ClosuresInAbbreviators : Tests.DisableAssertUI
    {
      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void TestCollectionAbbreviatorPositive() {
        new CodeUnderTest.MaF.TestClosureAbbreviators().TestCollectionAbbreviator(new[] { "ab", "c", "d", "efg" }, new[] { "ab", "c", "d", "efg" });
      }
      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void TestCollectionAbbreviatorNegative1() {
        Helpers.CheckRequires(() =>
          new CodeUnderTest.MaF.TestClosureAbbreviators().TestCollectionAbbreviator(new[] { "ab", null, "d", "efg" }, new[] { "ab", "c", "d", "efg" }),
          "Contract.ForAll(collection, element => element != null)", "coll1");
      }
      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void TestCollectionAbbreviatorNegative2() {
        Helpers.CheckRequires(() =>
          new CodeUnderTest.MaF.TestClosureAbbreviators().TestCollectionAbbreviator(new[] { "ab", "c", "d", "efg" }, new[] { "ab", null, "d", "efg" }),
          "Contract.ForAll(collection, element => element != null)", "coll2");
      }
      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void TestArrayAbbreviatorPositive() {
        new CodeUnderTest.MaF.TestClosureAbbreviators().TestArrayAbbreviator(new[] { "ab", "c", "d", "efg" }, new[] { "ab", "c", "d", "efg" });
      }
      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void TestArrayAbbreviatorNegative1() {
        Helpers.CheckRequires(() =>
          new CodeUnderTest.MaF.TestClosureAbbreviators().TestArrayAbbreviator(new[] { "ab", null, "d", "efg" }, new[] { "ab", "c", "d", "efg" }),
          "Contract.ForAll(0, array.Length, i => array[i] != null)", "array1");
      }
      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void TestArrayAbbreviatorNegative2() {
        Helpers.CheckRequires(() =>
          new CodeUnderTest.MaF.TestClosureAbbreviators().TestArrayAbbreviator(new[] { "ab", "c", "d", "efg" }, new[] { "ab", null, "d", "efg" }),
          "Contract.ForAll(0, array.Length, i => array[i] != null)", "array2");
      }

      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void TestCollectionAbbreviator2Positive() {
        new CodeUnderTest.MaF.TestClosureAbbreviators().TestCollectionAbbreviator2(new[] { "ab", "c", "d", "efg" }, new[] { "ab", "c", "d", "efg" });
      }
      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void TestCollectionAbbreviator2Negative1() {
        Helpers.CheckRequires(() =>
          new CodeUnderTest.MaF.TestClosureAbbreviators().TestCollectionAbbreviator2(new[] { "ab", null, "d", "efg" }, new[] { "ab", "c", "d", "efg" }),
          "Contract.ForAll(collection, element => element != null)", "coll1");
      }
      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void TestCollectionAbbreviator2Negative2() {
        Helpers.CheckRequires(() =>
          new CodeUnderTest.MaF.TestClosureAbbreviators().TestCollectionAbbreviator2(new[] { "ab", "c", "d", "efg" }, new[] { "ab", null, "d", "efg" }),
          "Contract.ForAll(collection, element => element != null)", "coll2");
      }
      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void TestArrayAbbreviator2Positive() {
        new CodeUnderTest.MaF.TestClosureAbbreviators().TestArrayAbbreviator2(new[] { "ab", "c", "d", "efg" }, new[] { "ab", "c", "d", "efg" });
      }
      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void TestArrayAbbreviator2Negative1() {
        Helpers.CheckRequires(() =>
          new CodeUnderTest.MaF.TestClosureAbbreviators().TestArrayAbbreviator2(new[] { "ab", null, "d", "efg" }, new[] { "ab", "c", "d", "efg" }),
          "Contract.ForAll(0, array.Length, i => array[i] != null)", "array1");
      }
      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void TestArrayAbbreviator2Negative2() {
        Helpers.CheckRequires(() =>
          new CodeUnderTest.MaF.TestClosureAbbreviators().TestArrayAbbreviator2(new[] { "ab", "c", "d", "efg" }, new[] { "ab", null, "d", "efg" }),
          "Contract.ForAll(0, array.Length, i => array[i] != null)", "array2");
      }

      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void TestFieldArrayAbbreviatorPositive() {
        new CodeUnderTest.MaF.TestClosureAbbreviators(new[] { "ab", "c", "d", "efg" }).TestFieldArrayUnchanged();
      }
      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void TestFieldArrayAbbreviatorNegative() {
        Helpers.CheckEnsures(() =>
          new CodeUnderTest.MaF.TestClosureAbbreviators(new[] { "ab", null, "d", "efg" }).TestFieldArrayUnchanged(),
          "Contract.ForAll(0, this.field.Length, i => this.field[i] != null)");
      }
      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void TestFieldAsCollectionAbbreviatorPositive() {
        new CodeUnderTest.MaF.TestClosureAbbreviators(new[] { "ab", "c", "d", "efg" }).TestFieldAsCollectionUnchanged();
      }
      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void TestFieldAsCollectionAbbreviatorNegative() {
        Helpers.CheckEnsures(() =>
          new CodeUnderTest.MaF.TestClosureAbbreviators(new[] { "ab", null, "d", "efg" }).TestFieldAsCollectionUnchanged(),
          "Contract.ForAll(this.field, elem => elem != null)");
      }


    }
  }
  namespace Mokosh
  {
    [TestClass]
    public class Tests : global::Tests.DisableAssertUI
    {
      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void TestEnumeratorWithRequiresPositive()
      {
        CodeUnderTest.Mokosh.Enumerate.Test(new CodeUnderTest.Mokosh.Projects());
      }
      [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
      public void TestEnumeratorWithRequiresNegative()
      {
        Helpers.CheckRequires(() =>
          CodeUnderTest.Mokosh.Enumerate.Test(null),
          "me != null");
      }

    }

    namespace BenLerner
    {
      [TestClass]
      public class Closures : global::Tests.DisableAssertUI
      {
        [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
        public void TestPositiveDeepClosures()
        {
          CodeUnderTest.BenLerner.ManyForAlls.DoIt();
        }
      }
    }
  }
  class Helpers
  {
    public static void CheckRequires(Action action, string expectedCondition)
    {
      try
      {
        action();
        throw new Exception();
      }
      catch (TestRewriterMethods.PreconditionException e)
      {
        Assert.AreEqual(expectedCondition, e.Condition);
      }
    }
    public static void CheckRequires(Action action, string expectedCondition, string userString)
    {
      try
      {
        action();
        throw new Exception();
      }
      catch (TestRewriterMethods.PreconditionException e)
      {
        Assert.AreEqual(expectedCondition, e.Condition);
        Assert.AreEqual(userString, e.User);
      }
    }
    public static void CheckEnsures(Action action, string expectedCondition)
    {
      try
      {
        action();
        throw new Exception();
      }
      catch (TestRewriterMethods.PostconditionException e)
      {
        Assert.AreEqual(expectedCondition, e.Condition);
      }
    }
    public static void CheckException<E>(Action action, string expectedMessage)
      where E : Exception
    {
      try
      {
        action();
        throw new Exception();
      }
      catch (E e)
      {
        Assert.AreEqual(expectedMessage, e.Message);
      }
    }
  }

}