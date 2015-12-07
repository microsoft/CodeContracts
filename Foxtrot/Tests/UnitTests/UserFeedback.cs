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
using CodeUnderTest;
using CodeUnderTest.PDC;
using Xunit;

namespace UserFeedback {
  namespace Peli {
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    public class UnitTest1 : Tests.DisableAssertUI {

      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void TestProperlyRewrittenResult()
      {
        new CodeUnderTest.Peli.UnitTest1.Foo().GetValue(0);
      }

      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void TestInvariantStrings1()
      {
        new CodeUnderTest.Peli.UnitTest1.Foo(2);
      }

      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void TestInvariantStrings2()
      {
        try
        {
          new CodeUnderTest.Peli.UnitTest1.Foo(0);
          throw new Exception();
        }
        catch (TestRewriterMethods.InvariantException i)
        {
          Assert.Equal("x > 0", i.Condition);
          Assert.Equal(null, i.User);
        }
      }

      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void TestInvariantStrings3()
      {
        try
        {
          new CodeUnderTest.Peli.UnitTest1.Foo(10);
          throw new Exception();
        }
        catch (TestRewriterMethods.InvariantException i)
        {
          Assert.Equal("x < 10", i.Condition);
          Assert.Equal("upper bound", i.User);
        }
      }



      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void TestOnThrowPositive()
      {
        CodeUnderTest.Peli.UnitTest1.Bar b = new CodeUnderTest.Peli.UnitTest1.Bar(0);
        Assert.Throws<ArgumentException>(() => new CodeUnderTest.Peli.UnitTest1.Foo().TestException(b, 0));
      }

      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
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
          Assert.Equal("b.ID == 0", p.Condition);
          Assert.Equal("Peli3", p.User);
        }
      }

      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
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
          Assert.Equal("b.ID >= 0", p.Condition);
          Assert.Equal(null, p.User);
        }
      }



      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
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

      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
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

      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void TestEnsuresConditionStringAndUserString()
      {
        try
        {
          new CodeUnderTest.Peli.UnitTest1.Foo().GetValue(1);
        }
        catch (TestRewriterMethods.PostconditionException e)
        {
          Assert.Equal("Contract.Result<Bar>() == null || Contract.Result<Bar>().ID == 0", e.Condition);
          Assert.Equal("peli2", e.User);
          return;
        }
      }
    }
  }

  namespace WinSharp {
    using System.Linq;

      public class RecursionChecks : Tests.DisableAssertUI
      {
      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void RecursionTest1()
      {
        var mydict = new CodeUnderTest.WinSharp.MyDict<string, int>();

        var result = mydict.ContainsKey("foo");

        Assert.False(result);
      }

      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void RecursionTest2()
      {
        var result = CodeUnderTest.WinSharp.ExplicitlyRecursive.Odd(5);

        Assert.True(result);
      }

      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void RecursionTest3()
      {
        var result = CodeUnderTest.WinSharp.ExplicitlyRecursive.Odd(6);

        Assert.False(result);
      }

      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void RecursionTest4()
      {
        var result = CodeUnderTest.WinSharp.ExplicitlyRecursive.Even(7);

        Assert.False(result);
      }

      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void RecursionTest5()
      {
        var result = CodeUnderTest.WinSharp.ExplicitlyRecursive.Even(8);

        Assert.True(result);
      }


      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void RecursionTest6()
      {
        var result = CodeUnderTest.WinSharp.ExplicitlyRecursive.SubEven(8);

        Assert.Equal(0, result);
      }

      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void RecursionTest7()
      {
        var result = CodeUnderTest.WinSharp.ExplicitlyRecursive.SubEven(5);

        Assert.Equal(0, result);
      }

      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void RecursionTest8()
      {
        var result = CodeUnderTest.WinSharp.ExplicitlyRecursive.SubOdd(8);

        Assert.Equal(0, result);
      }

      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void RecursionTest9()
      {
        var result = CodeUnderTest.WinSharp.ExplicitlyRecursive.SubOdd(5);

        Assert.Equal(0, result);
      }

      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void RecursionTest10()
      {
        var result = CodeUnderTest.WinSharp.ExplicitlyRecursive.ThrowOnEven(5);

        Assert.False(result);
      }

      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void RecursionTest11()
      {
        Assert.Throws<ArgumentException>(() => CodeUnderTest.WinSharp.ExplicitlyRecursive.ThrowOnEven(4));
      }

      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void RecursionTest12()
      {
        var result = CodeUnderTest.WinSharp.ExplicitlyRecursive.ThrowOnOdd(4);

        Assert.False(result);
      }

      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void RecursionTest13()
      {
        Assert.Throws<ArgumentException>(() => CodeUnderTest.WinSharp.ExplicitlyRecursive.ThrowOnOdd(3));
      }

    }

  }

  namespace Multani {
      public class MultaniTestClass1 : Tests.DisableAssertUI
      {
      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void PositiveMultani1()
      {
        double[] initialValues = new[] { 1.0, 2.0, 3.0 };
        double[] stDevs = new[] { 0.1, 0.2, 0.3 };
        double[] drifts = new[] { 0.1, 0.1, 0.1 };
        double[,] correlations = new double[3, 3] { { 0.1, 0.1, 0.1 }, { 0.1, 0.1, 0.1 }, { 0.1, 0.1, 0.1 } };
        int randomSeed = 44;
        var c = new CodeUnderTest.Multani.CorrelatedGeometricBrownianMotionFuelPriceSimulator(initialValues, stDevs, drifts, correlations, randomSeed);
      }
      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void NegativeMultani1()
      {
        double[] initialValues = null;
        double[] stDevs = new[] { 0.1, -0.2, 0.3 };
        double[] drifts = null;
        double[,] correlations = null;
        int randomSeed = 44;
        Assert.Throws<TestRewriterMethods.PreconditionException>(() => new CodeUnderTest.Multani.CorrelatedGeometricBrownianMotionFuelPriceSimulator(initialValues, stDevs, drifts, correlations, randomSeed));
      }
    }
  }

  namespace Somebody {
    public class TestResourceStringUserMessage : Tests.DisableAssertUI {

      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void PositiveTestUserMessages()
      {
        var o = new CodeUnderTest.Somebody.TestResourceStringUserMessage();
        o.RequiresWithInternalResourceMessage("hello");
        o.RequiresWithPublicFieldMessage("hello");
        o.RequiresWithPublicGetterMessage("hello");
      }

      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
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
          Assert.Equal("Can't call me with null", p.User);
        }
      }

      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
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
          Assert.Equal(CodeUnderTest.Somebody.TestResourceStringUserMessage.Message2, p.User);
        }
      }

      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
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
          Assert.Equal(CodeUnderTest.Somebody.TestResourceStringUserMessage.Message3, p.User);
        }
      }

      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
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
          Assert.Equal("Illegal value 2", e.Message);
        }
      }

      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void PositiveLegacyThrowWithPrivateExceptionArgument()
      {
        var o = new CodeUnderTest.Somebody.TestResourceStringUserMessage();
        o.LegacyRequiresReferencingPrivateStuffInThrow(CodeUnderTest.Somebody.TypeDescriptorPermissionFlags.RestrictedRegistrationAccess);
      }
    }
  }

  namespace PDC {

      public class DontCheckInvariantsDuringConstructors : Tests.DisableAssertUI
      {
      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void PositiveTrickyAutoProp1()
      {
        var tricky = new TrickyAutoPropInvariants<Valid>(new Valid(true), 0, false);
        tricky.Change(true, 5);
      }
      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
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
      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
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
      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
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
          Assert.Equal("Flag ? Age > 0 : Age == 0 && Value.IsValid", i.Condition);
        }
      }
      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
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
          Assert.Equal("Flag ? Age > 0 : Age == 0 && Value.IsValid", i.Condition);
        }
      }
      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
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
          Assert.Equal("Flag ? Age > 0 : Age == 0 && Value.IsValid", i.Condition);
        }
      }
      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void NegativeTrickyAutoProp3b() {
        try {
          var tricky = new TrickyAutoPropInvariants<Valid>(new Valid(true), 0, false);
          tricky.LeaveObjectConsistentWrong(); // will throw invariant
          throw new Exception();
        } catch (TestRewriterMethods.InvariantException i) {
          Assert.Equal("Flag ? Age > 0 : Age == 0 && Value.IsValid", i.Condition);
        }
      }
      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
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
          Assert.Equal("Flag ? Age > 0 : Age == 0 && Value.IsValid", i.Condition);
        }
      }

      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void PositiveInvariantOffDuringConstruction1()
      {
        var p = new Invariants("Joe", 42);

      }


      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void NegativeInvariantOffDuringConstruction1()
      {
        try
        {
          var p = new Invariants(null, 1);
          throw new Exception();
        }
        catch (TestRewriterMethods.InvariantException i)
        {
          Assert.Equal("name != null", i.Condition);
        }
      }

      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void PositiveInvariantOffDuringConstruction2()
      {
        var p = new Invariants<string>("Joe", 42, 2, 1);

      }


      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void NegativeInvariantOffDuringConstruction2()
      {
        try
        {
          var p = new Invariants<string>("Joe", 0, 1, 2);
          throw new Exception();
        }
        catch (TestRewriterMethods.InvariantException i)
        {
          Assert.Equal("age > 0", i.Condition);
        }
      }

      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void NegativeInvariantOfAutoPropIntoRequires1()
      {
        try
        {
          var p = new Invariants<string>(null, 0, 1, 2);
          throw new Exception();
        }
        catch (TestRewriterMethods.InvariantException p)
        {
          Assert.Equal("Name != null", p.Condition);
        }
      }

      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void NegativeInvariantOfAutoPropIntoRequires2()
      {
        try
        {
          var p = new Invariants<string>("Joe", 0, 1, 2);
          throw new Exception();
        }
        catch (TestRewriterMethods.InvariantException p)
        {
          Assert.Equal("age > 0", p.Condition);
        }
      }

      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void NegativeInvariantOfAutoPropIntoRequires3()
      {
        try
        {
          var p = new Invariants<string>("Joe", 1, 1, 2);
          throw new Exception();
        }
        catch (TestRewriterMethods.InvariantException p)
        {
          Assert.Equal("X > Y", p.Condition);
        }
      }

      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
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
          Assert.Equal("Name != null", p.Condition);
        }
      }

      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
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
          Assert.Equal("X > Y", p.Condition);
        }
      }

      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
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
          Assert.Equal("X > Y", p.Condition);
        }
      }

    }
  }

  namespace Arnott {
      public class C : Tests.DisableAssertUI
      {
 
      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void PositiveRequiresWithException()
      {
        new CodeUnderTest.Arnott.C().M(this);
      }
      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void NegativeRequiresWithException()
      {
        Assert.Throws<ArgumentNullException>(() => new CodeUnderTest.Arnott.C().M(null));
      }

      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void WorksCorrectlyWithThisOrder()
      {
        new CodeUnderTest.Arnott.C().OkayOrder(new int[20]);
      }
      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void WorksInCorrectlyWithThisOrder()
      {
        Assert.Throws<ArgumentOutOfRangeException>(() => new CodeUnderTest.Arnott.C().BadOrder(new int[10]));
      }

    }

  }

  namespace DavidAllen {
      public class AutoPropProblem : Tests.DisableAssertUI
      {
      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void PositiveNoAutoPropTreatmentForImplsAndOverrides0(){
        new CodeUnderTest.DavidAllen.AutoPropProblem.Impl1().P = 1;
      }
      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void PositiveNoAutoPropTreatmentForImplsAndOverrides1() {
        CodeUnderTest.DavidAllen.AutoPropProblem.J j = new CodeUnderTest.DavidAllen.AutoPropProblem.Impl2();
        j.P = 1;
      }
      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void PositiveNoAutoPropTreatmentForImplsAndOverrides2() {
        new CodeUnderTest.DavidAllen.AutoPropProblem.Sub().P = 1;
      }
      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void NegativeNoAutoPropTreatmentForImplsAndOverrides0() {
        // gets invariant exception but *not* precondition exception which
        // would indicate that the autoprop
        Assert.Throws<TestRewriterMethods.InvariantException>(() => new CodeUnderTest.DavidAllen.AutoPropProblem.Impl1().P = -1);
      }
      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void PositiveNoAutoPropTreatmentForImplsAndOverrides3() {
        CodeUnderTest.DavidAllen.AutoPropProblem.J j = new CodeUnderTest.DavidAllen.AutoPropProblem.Impl2();
        j.P = 0; // no violation of invariant because explicit interface implementations don't get invariants checked
      }
      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void NegativeNoAutoPropTreatmentForImplsAndOverrides1() {
        Assert.Throws<TestRewriterMethods.InvariantException>(() => new CodeUnderTest.DavidAllen.AutoPropProblem.Sub().P = -1);
      }
    }
  }

  namespace MaF
  {
      public class TestContractCheckingOffOn : Tests.DisableAssertUI
    {
      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void NegativeTest0WhereCheckingIsOn()
      {
        Helpers.CheckEnsures(() =>
          new CodeUnderTest.MaF.TestContractCheckingOffOn().Test0("foo"),
          "Contract.Result<int>() > 0");
      }

      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void PositiveTest1WhereCheckingIsOff()
      {
        new CodeUnderTest.MaF.TestContractCheckingOffOn().Test1(null);
      }

      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void NegativeTest2WhereCheckingIsOn()
      {
        Helpers.CheckRequires(() =>
          new CodeUnderTest.MaF.TestContractCheckingOffOn.Nested().Test2(null),
          "s != null");
      }

      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void PositiveTest3WhereCheckingIsBackOff()
      {
        new CodeUnderTest.MaF.TestContractCheckingOffOn.Nested().Test3(null);
      }
    }

    public class TestContractInheritanceOffOn : Tests.DisableAssertUI 
    {
      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void NegativeTest0WhereInheritanceIsBackOn()
      {
        Helpers.CheckEnsures(() =>
          new CodeUnderTest.MaF.TestContractInheritanceOffOn().Test0("foo"),
          "Contract.Result<int>() > 0");
      }

      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void PositiveTest1WhereInheritanceIsOff()
      {
        new CodeUnderTest.MaF.TestContractInheritanceOffOn().Test1(null);
      }

      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void PositiveTest2WhereInheritanceIsOff()
      {
        new CodeUnderTest.MaF.TestContractInheritanceOffOn.Nested().Test2(null);
      }

      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void NegativeTest3WhereInheritanceIsBackOn()
      {
        Helpers.CheckRequires( () =>
          new CodeUnderTest.MaF.TestContractInheritanceOffOn().Test0(null),
          "s != null");
      }
    }

    public class TestOldArray : Tests.DisableAssertUI
    {
      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void TestOldParameterArrayPositive()
      {
        new CodeUnderTest.MaF.TestOldArrays().OldParameterArray(new[] { 1, 2, 3, 4, 5, 6 }, true);
      }
      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void TestOldParameterArrayNegative()
      {
        Helpers.CheckEnsures(() =>
          new CodeUnderTest.MaF.TestOldArrays().OldParameterArray(new[] { 1, 2, 3, 4, 5, 6 }, false),
          "Contract.ForAll(0, z.Length, i => z[i] == Contract.OldValue(z[i]))");
      }
      //[Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void TestOldThisFieldArrayPositive()
      {
        new CodeUnderTest.MaF.TestOldArrays().OldThisFieldArray(true);
      }
      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void TestOldThisFieldArrayNegative()
      {
        Helpers.CheckEnsures(() =>
          new CodeUnderTest.MaF.TestOldArrays().OldThisFieldArray(false),
          "Contract.ForAll(0, this.field.Length, i => this.field[i] == Contract.OldValue(this.field[i]))");
      }
      //[Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void TestOldThisFieldClosurePositive()
      {
        new CodeUnderTest.MaF.TestOldArrays().OldThisFieldClosure(true);
      }
      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void TestOldThisFieldClosureNegative()
      {
        Helpers.CheckEnsures(() =>
          new CodeUnderTest.MaF.TestOldArrays().OldThisFieldClosure(false),
          "Contract.ForAll(this.field, elem => elem == Contract.OldValue(elem))");
      }
      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void TestOldParameterFieldArrayPositive()
      {
        new CodeUnderTest.MaF.TestOldArrays().OldParameterFieldArray(new CodeUnderTest.MaF.TestOldArrays(), true);
      }
      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void TestOldParameterFieldArrayNegative()
      {
        Helpers.CheckEnsures(() =>
          new CodeUnderTest.MaF.TestOldArrays().OldParameterFieldArray(new CodeUnderTest.MaF.TestOldArrays(), false),
          "Contract.ForAll(0, other.field.Length, i => other.field[i] == Contract.OldValue(other.field[i]))");
      }
    }

    public class ClosuresInValidators : Tests.DisableAssertUI
    {
      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void TestCollectionValidatorPositive()
      {
        new CodeUnderTest.MaF.TestClosureValidators().TestCollectionValidator(new[] { "ab", "c", "d", "efg" }, new[] { "ab", "c", "d", "efg" });
      }
      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void TestCollectionValidatorNegative1()
      {
        Helpers.CheckException<ArgumentException>(() =>
          new CodeUnderTest.MaF.TestClosureValidators().TestCollectionValidator(new[] { "ab", null, "d", "efg" }, new[] { "ab", "c", "d", "efg" }),
          "all elements of coll1 must be non-null");
      }
      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void TestCollectionValidatorNegative2()
      {
        Helpers.CheckException<ArgumentException>(() =>
          new CodeUnderTest.MaF.TestClosureValidators().TestCollectionValidator(new[] { "ab", "c", "d", "efg" }, new[] { "ab", null, "d", "efg" }),
          "all elements of coll2 must be non-null");
      }
      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void TestArrayValidatorPositive()
      {
        new CodeUnderTest.MaF.TestClosureValidators().TestArrayValidator(new[] { "ab", "c", "d", "efg" }, new[] { "ab", "c", "d", "efg" });
      }
      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void TestArrayValidatorNegative1()
      {
        Helpers.CheckException<ArgumentException>(() =>
          new CodeUnderTest.MaF.TestClosureValidators().TestArrayValidator(new[] { "ab", null, "d", "efg" }, new[] { "ab", "c", "d", "efg" }),
          "all elements of array1 must be non-null");
      }
      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void TestArrayValidatorNegative2()
      {
        Helpers.CheckException<ArgumentException>(() =>
          new CodeUnderTest.MaF.TestClosureValidators().TestArrayValidator(new[] { "ab", "c", "d", "efg" }, new[] { "ab", null, "d", "efg" }),
          "all elements of array2 must be non-null");
      }

      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void TestCollectionValidator2Positive() {
        new CodeUnderTest.MaF.TestClosureValidators().TestCollectionValidator2(new[] { "ab", "c", "d", "efg" }, new[] { "ab", "c", "d", "efg" });
      }
      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void TestCollectionValidator2Negative1() {
        Helpers.CheckException<ArgumentException>(() =>
          new CodeUnderTest.MaF.TestClosureValidators().TestCollectionValidator2(new[] { "ab", null, "d", "efg" }, new[] { "ab", "c", "d", "efg" }),
          "all elements of coll1 must be non-null");
      }
      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void TestCollectionValidator2Negative2() {
        Helpers.CheckException<ArgumentException>(() =>
          new CodeUnderTest.MaF.TestClosureValidators().TestCollectionValidator2(new[] { "ab", "c", "d", "efg" }, new[] { "ab", null, "d", "efg" }),
          "all elements of coll2 must be non-null");
      }
      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void TestArrayValidator2Positive() {
        new CodeUnderTest.MaF.TestClosureValidators().TestArrayValidator2(new[] { "ab", "c", "d", "efg" }, new[] { "ab", "c", "d", "efg" });
      }
      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void TestArrayValidator2Negative1() {
        Helpers.CheckException<ArgumentException>(() =>
          new CodeUnderTest.MaF.TestClosureValidators().TestArrayValidator2(new[] { "ab", null, "d", "efg" }, new[] { "ab", "c", "d", "efg" }),
          "all elements of array1 must be non-null");
      }
      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void TestArrayValidator2Negative2() {
        Helpers.CheckException<ArgumentException>(() =>
          new CodeUnderTest.MaF.TestClosureValidators().TestArrayValidator2(new[] { "ab", "c", "d", "efg" }, new[] { "ab", null, "d", "efg" }),
          "all elements of array2 must be non-null");
      }

      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void TestFieldArrayAbbreviatorPositive()
      {
        new CodeUnderTest.MaF.TestClosureValidators(new[] { "ab", "c", "d", "efg" }).TestFieldArrayUnchanged();
      }
      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void TestFieldArrayAbbreviatorNegative()
      {
        Helpers.CheckEnsures(() =>
          new CodeUnderTest.MaF.TestClosureValidators(new[] { "ab", null, "d", "efg" }).TestFieldArrayUnchanged(),
          "Contract.ForAll(0, this.field.Length, i => this.field[i] != null)");
      }
      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void TestFieldAsCollectionAbbreviatorPositive()
      {
        new CodeUnderTest.MaF.TestClosureValidators(new[] { "ab", "c", "d", "efg" }).TestFieldAsCollectionUnchanged();
      }
      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void TestFieldAsCollectionAbbreviatorNegative()
      {
        Helpers.CheckEnsures(() =>
          new CodeUnderTest.MaF.TestClosureValidators(new[] { "ab", null, "d", "efg" }).TestFieldAsCollectionUnchanged(),
          "Contract.ForAll(this.field, elem => elem != null)");
      }


#if false
      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void TestPassedFieldArrayValidatorPositive()
      {
        CodeUnderTest.MaF.CallerClass.TestFieldArrayUnchanged(new CodeUnderTest.MaF.TestClosureValidators(new[] { "ab", "c", "d", "efg" }));
      }
      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void TestPassedFieldArrayValidatorNegative()
      {
        Helpers.CheckException<ArgumentException>(() =>
          CodeUnderTest.MaF.CallerClass.TestFieldArrayUnchanged(new CodeUnderTest.MaF.TestClosureValidators(new[] { "ab", "c", null, "efg" })),
          "all elements of array must be non-null");
      }
      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void TestPassedFieldAsCollectionValidatorPositive()
      {
        CodeUnderTest.MaF.CallerClass.TestFieldAsCollectionUnchanged(new CodeUnderTest.MaF.TestClosureValidators(new[] { "ab", "c", "d", "efg" }));
      }
      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void TestPassedFieldAsCollectionValidatorNegative()
      {
        Helpers.CheckException<ArgumentException>(() =>
          CodeUnderTest.MaF.CallerClass.TestFieldAsCollectionUnchanged(new CodeUnderTest.MaF.TestClosureValidators(new[] { "ab", "c", null, "efg" })),
          "all elements of array must be non-null");
      }
#endif
    }

    public class ClosuresInAbbreviators : Tests.DisableAssertUI
    {
      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void TestCollectionAbbreviatorPositive() {
        new CodeUnderTest.MaF.TestClosureAbbreviators().TestCollectionAbbreviator(new[] { "ab", "c", "d", "efg" }, new[] { "ab", "c", "d", "efg" });
      }
      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void TestCollectionAbbreviatorNegative1() {
        Helpers.CheckRequires(() =>
          new CodeUnderTest.MaF.TestClosureAbbreviators().TestCollectionAbbreviator(new[] { "ab", null, "d", "efg" }, new[] { "ab", "c", "d", "efg" }),
          "Contract.ForAll(collection, element => element != null)", "coll1");
      }
      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void TestCollectionAbbreviatorNegative2() {
        Helpers.CheckRequires(() =>
          new CodeUnderTest.MaF.TestClosureAbbreviators().TestCollectionAbbreviator(new[] { "ab", "c", "d", "efg" }, new[] { "ab", null, "d", "efg" }),
          "Contract.ForAll(collection, element => element != null)", "coll2");
      }
      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void TestArrayAbbreviatorPositive() {
        new CodeUnderTest.MaF.TestClosureAbbreviators().TestArrayAbbreviator(new[] { "ab", "c", "d", "efg" }, new[] { "ab", "c", "d", "efg" });
      }
      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void TestArrayAbbreviatorNegative1() {
        Helpers.CheckRequires(() =>
          new CodeUnderTest.MaF.TestClosureAbbreviators().TestArrayAbbreviator(new[] { "ab", null, "d", "efg" }, new[] { "ab", "c", "d", "efg" }),
          "Contract.ForAll(0, array.Length, i => array[i] != null)", "array1");
      }
      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void TestArrayAbbreviatorNegative2() {
        Helpers.CheckRequires(() =>
          new CodeUnderTest.MaF.TestClosureAbbreviators().TestArrayAbbreviator(new[] { "ab", "c", "d", "efg" }, new[] { "ab", null, "d", "efg" }),
          "Contract.ForAll(0, array.Length, i => array[i] != null)", "array2");
      }

      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void TestCollectionAbbreviator2Positive() {
        new CodeUnderTest.MaF.TestClosureAbbreviators().TestCollectionAbbreviator2(new[] { "ab", "c", "d", "efg" }, new[] { "ab", "c", "d", "efg" });
      }
      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void TestCollectionAbbreviator2Negative1() {
        Helpers.CheckRequires(() =>
          new CodeUnderTest.MaF.TestClosureAbbreviators().TestCollectionAbbreviator2(new[] { "ab", null, "d", "efg" }, new[] { "ab", "c", "d", "efg" }),
          "Contract.ForAll(collection, element => element != null)", "coll1");
      }
      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void TestCollectionAbbreviator2Negative2() {
        Helpers.CheckRequires(() =>
          new CodeUnderTest.MaF.TestClosureAbbreviators().TestCollectionAbbreviator2(new[] { "ab", "c", "d", "efg" }, new[] { "ab", null, "d", "efg" }),
          "Contract.ForAll(collection, element => element != null)", "coll2");
      }
      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void TestArrayAbbreviator2Positive() {
        new CodeUnderTest.MaF.TestClosureAbbreviators().TestArrayAbbreviator2(new[] { "ab", "c", "d", "efg" }, new[] { "ab", "c", "d", "efg" });
      }
      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void TestArrayAbbreviator2Negative1() {
        Helpers.CheckRequires(() =>
          new CodeUnderTest.MaF.TestClosureAbbreviators().TestArrayAbbreviator2(new[] { "ab", null, "d", "efg" }, new[] { "ab", "c", "d", "efg" }),
          "Contract.ForAll(0, array.Length, i => array[i] != null)", "array1");
      }
      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void TestArrayAbbreviator2Negative2() {
        Helpers.CheckRequires(() =>
          new CodeUnderTest.MaF.TestClosureAbbreviators().TestArrayAbbreviator2(new[] { "ab", "c", "d", "efg" }, new[] { "ab", null, "d", "efg" }),
          "Contract.ForAll(0, array.Length, i => array[i] != null)", "array2");
      }

      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void TestFieldArrayAbbreviatorPositive() {
        new CodeUnderTest.MaF.TestClosureAbbreviators(new[] { "ab", "c", "d", "efg" }).TestFieldArrayUnchanged();
      }
      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void TestFieldArrayAbbreviatorNegative() {
        Helpers.CheckEnsures(() =>
          new CodeUnderTest.MaF.TestClosureAbbreviators(new[] { "ab", null, "d", "efg" }).TestFieldArrayUnchanged(),
          "Contract.ForAll(0, this.field.Length, i => this.field[i] != null)");
      }
      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void TestFieldAsCollectionAbbreviatorPositive() {
        new CodeUnderTest.MaF.TestClosureAbbreviators(new[] { "ab", "c", "d", "efg" }).TestFieldAsCollectionUnchanged();
      }
      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void TestFieldAsCollectionAbbreviatorNegative() {
        Helpers.CheckEnsures(() =>
          new CodeUnderTest.MaF.TestClosureAbbreviators(new[] { "ab", null, "d", "efg" }).TestFieldAsCollectionUnchanged(),
          "Contract.ForAll(this.field, elem => elem != null)");
      }


    }
  }
  namespace Mokosh
  {
    public class Tests : global::Tests.DisableAssertUI
    {
      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void TestEnumeratorWithRequiresPositive()
      {
        CodeUnderTest.Mokosh.Enumerate.Test(new CodeUnderTest.Mokosh.Projects());
      }
      [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
      public void TestEnumeratorWithRequiresNegative()
      {
        Helpers.CheckRequires(() =>
          CodeUnderTest.Mokosh.Enumerate.Test(null),
          "me != null");
      }

    }

    namespace BenLerner
    {
      public class Closures : global::Tests.DisableAssertUI
      {
        [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
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
        Assert.Equal(expectedCondition, e.Condition);
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
        Assert.Equal(expectedCondition, e.Condition);
        Assert.Equal(userString, e.User);
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
        Assert.Equal(expectedCondition, e.Condition);
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
        Assert.Equal(expectedMessage, e.Message);
      }
    }
  }

}