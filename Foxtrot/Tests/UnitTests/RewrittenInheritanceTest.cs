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

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Text;
using System.Collections.Generic;
using CodeUnderTest;

namespace Tests {

  #region Tests
  [TestClass]
  public class RewrittenInheritanceTest : DisableAssertUI {

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

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveOneLevelInheritanceInvariantTest()
    {
      new RewrittenInheritanceDerived().SetInheritedValueMustBeTrue(true);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.InvariantException))]
    public void NegativeOneLevelInheritanceInvariantTest()
    {
      new RewrittenInheritanceDerived().SetInheritedValueMustBeTrue(false);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveTwoLevelInheritanceInvariantTest()
    {
      new RewrittenInheritanceDerived().SetBaseValueMustBeTrue(true);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.InvariantException))]
    public void NegativeTwoLevelInheritanceInvariantTest()
    {
      new RewrittenInheritanceDerived().SetBaseValueMustBeTrue(false);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveBareInheritanceRequiresTest()
    {
      new RewrittenInheritanceDerived().BareRequires(true);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void NegativeBareInheritanceRequiresTest()
    {
      new RewrittenInheritanceDerived().BareRequires(false);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveLegacyInheritanceRequiresTest()
    {
      new RewrittenInheritanceDerived().LegacyRequires("");
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(ArgumentNullException))]
    public void NegativeLegacyInheritanceRequiresTest()
    {
      new RewrittenInheritanceDerived().LegacyRequires(null);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveInheritanceRequiresWithExceptionTest()
    {
      new RewrittenInheritanceDerived().RequiresWithException("");
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(ArgumentNullException))]
    public void NegativeInheritanceRequiresWithExceptionTest()
    {
      new RewrittenInheritanceDerived().RequiresWithException(null);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveAdditiveInheritanceRequiresTest()
    {
      new RewrittenInheritanceDerived().AdditiveRequires(101);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void NegativeAdditiveOneLevelInheritanceRequiresTest()
    {
      new RewrittenInheritanceDerived().AdditiveRequires(0);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void NegativeAdditiveTwoLevelInheritanceRequiresTest()
    {
      new RewrittenInheritanceDerived().AdditiveRequires(0);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveSubtractiveInheritanceRequiresTest()
    {
      new RewrittenInheritanceDerived().SubtractiveRequires(101);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void NegativeSubtractiveOneLevelInheritanceRequiresTest()
    {
      new RewrittenInheritanceDerived().SubtractiveRequires(0);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void NegativeSubtractiveTwoLevelInheritanceRequiresTest()
    {
      new RewrittenInheritanceDerived().SubtractiveRequires(1);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveAdditiveInheritanceEnsuresTest()
    {
      new RewrittenInheritanceDerived().AdditiveEnsures(101);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeAdditiveOneLevelInheritanceEnsuresTest()
    {
      new RewrittenInheritanceDerived().AdditiveEnsures(1);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeAdditiveTwoLevelInheritanceEnsuresTest()
    {
      new RewrittenInheritanceDerived().AdditiveEnsures(0);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveSubtractiveInheritanceEnsuresTest()
    {
      new RewrittenInheritanceDerived().SubtractiveEnsures(101);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeSubtractiveOneLevelInheritanceEnsuresTest()
    {
      new RewrittenInheritanceDerived().SubtractiveEnsures(0);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeSubtractiveTwoLevelInheritanceEnsuresTest()
    {
      new RewrittenInheritanceDerived().SubtractiveEnsures(1);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveInheritedImplicitRequiresTest()
    {
      new RewrittenInheritanceDerived().InterfaceBaseRequires(true);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveInheritedImplicitEnsuresTest()
    {
      new RewrittenInheritanceDerived().InterfaceBaseEnsures(true);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void NegativeInheritedImplicitRequiresTest()
    {
      new RewrittenInheritanceDerived().InterfaceBaseRequires(false);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeInheritedImplicitEnsuresTest()
    {
      new RewrittenInheritanceDerived().InterfaceBaseEnsures(false);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveImplicitlyOverriddenRequiresTest()
    {
      new RewrittenInheritanceDerived().ImplicitlyOverriddenInterfaceRequires(true);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveImplicitlyOverriddenEnsuresTest()
    {
      new RewrittenInheritanceDerived().ImplicitlyOverriddenInterfaceEnsures(true);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void NegativeImplicitlyOverriddenRequiresTest()
    {
      new RewrittenInheritanceDerived().ImplicitlyOverriddenInterfaceRequires(false);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeImplicitlyOverriddenEnsuresTest()
    {
      new RewrittenInheritanceDerived().ImplicitlyOverriddenInterfaceEnsures(false);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveExplicitlyOverriddenRequiresTest()
    {
      new RewrittenInheritanceDerived().ExplicitlyOverriddenInterfaceRequires(true);
      new RewrittenInheritanceDerived().ExplicitlyOverriddenInterfaceRequires(false);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveExplicitlyOverriddenEnsuresTest()
    {
      new RewrittenInheritanceDerived().ExplicitlyOverriddenInterfaceEnsures(true);
      new RewrittenInheritanceDerived().ExplicitlyOverriddenInterfaceEnsures(false);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveMultiplyImplicitlyOverriddenRequiresTest()
    {
      new RewrittenInheritanceDerived().MultiplyImplicitlyOverriddenInterfaceRequires(true);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveMultiplyImplicitlyOverriddenEnsuresTest()
    {
      new RewrittenInheritanceDerived().MultiplyImplicitlyOverriddenInterfaceEnsures(true);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void NegativeMultiplyImplicitlyOverriddenRequiresTest()
    {
      new RewrittenInheritanceDerived().MultiplyImplicitlyOverriddenInterfaceRequires(false);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeMultiplyImplicitlyOverriddenEnsuresTest()
    {
      new RewrittenInheritanceDerived().MultiplyImplicitlyOverriddenInterfaceEnsures(false);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveMultiplyExplicitlyOverriddenRequiresTest()
    {
      ((IRewrittenMultipleInheritanceInterface)new RewrittenInheritanceDerived()).MultiplyExplicitlyOverriddenInterfaceRequires(true);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveMultiplyExplicitlyOverriddenEnsuresTest()
    {
      ((IRewrittenMultipleInheritanceInterface)new RewrittenInheritanceDerived()).MultiplyExplicitlyOverriddenInterfaceEnsures(true);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void NegativeMultiplyExplicitlyOverriddenRequiresTest()
    {
      ((IRewrittenMultipleInheritanceInterface)new RewrittenInheritanceDerived()).MultiplyExplicitlyOverriddenInterfaceRequires(false);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeMultiplyExplicitlyOverriddenEnsuresTest()
    {
      ((IRewrittenMultipleInheritanceInterface)new RewrittenInheritanceDerived()).MultiplyExplicitlyOverriddenInterfaceEnsures(false);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveMultiplyDifferentlyImplicitlyOverriddenRequiresTest()
    {
      new RewrittenInheritanceDerived().MultiplyDifferentlyOverriddenInterfaceRequires(true);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveMultiplyDifferentlyExplicitlyOverriddenRequiresTest()
    {
      ((IRewrittenMultipleInheritanceInterface)new RewrittenInheritanceDerived()).MultiplyDifferentlyOverriddenInterfaceRequires(true);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveMultiplyDifferentlyImplicitlyOverriddenEnsuresTest()
    {
      new RewrittenInheritanceDerived().MultiplyDifferentlyOverriddenInterfaceEnsures(true);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveMultiplyDifferentlyExplicitlyOverriddenEnsuresTest()
    {
      ((IRewrittenMultipleInheritanceInterface)new RewrittenInheritanceDerived()).MultiplyDifferentlyOverriddenInterfaceEnsures(true);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void NegativeMultiplyDifferentlyImplicitlyOverriddenRequiresTest()
    {
      new RewrittenInheritanceDerived().MultiplyDifferentlyOverriddenInterfaceRequires(false);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void NegativeMultiplyDifferentlyExplicitlyOverriddenRequiresTest()
    {
      ((IRewrittenMultipleInheritanceInterface)new RewrittenInheritanceDerived()).MultiplyDifferentlyOverriddenInterfaceRequires(false);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeMultiplyDifferentlyImplicitlyOverriddenEnsuresTest()
    {
      new RewrittenInheritanceDerived().MultiplyDifferentlyOverriddenInterfaceEnsures(false);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeMultiplyDifferentlyExplicitlyOverriddenEnsuresTest()
    {
      ((IRewrittenMultipleInheritanceInterface)new RewrittenInheritanceDerived()).MultiplyDifferentlyOverriddenInterfaceEnsures(false);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveImplicitRequiresTest()
    {
      new RewrittenInheritanceDerived().InterfaceRequiresMultipleOfTwo(2);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveImplicitEnsuresTest()
    {
      new RewrittenInheritanceDerived().InterfaceEnsuresMultipleOfTwo(2);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void NegativeImplicitRequiresTest()
    {
      new RewrittenInheritanceDerived().InterfaceRequiresMultipleOfTwo(3);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeImplicitEnsuresTest()
    {
      new RewrittenInheritanceDerived().InterfaceEnsuresMultipleOfTwo(3);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveExplicitRequiresTest()
    {
      ((IRewrittenInheritanceInterfaceDerived2)new RewrittenInheritanceDerived()).InterfaceRequiresMultipleOfThree(3);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveExplicitEnsuresTest()
    {
      ((IRewrittenInheritanceInterfaceDerived2)new RewrittenInheritanceDerived()).InterfaceEnsuresMultipleOfThree(3);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void NegativeExplicitRequiresTest()
    {
      ((IRewrittenInheritanceInterfaceDerived2)new RewrittenInheritanceDerived()).InterfaceRequiresMultipleOfThree(2);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeExplicitEnsuresTest()
    {
      ((IRewrittenInheritanceInterfaceDerived2)new RewrittenInheritanceDerived()).InterfaceEnsuresMultipleOfThree(2);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveNonExplicitRequiresTest()
    {
      new RewrittenInheritanceDerived().InterfaceRequiresMultipleOfThree(1);
      new RewrittenInheritanceDerived().InterfaceRequiresMultipleOfThree(2);
      new RewrittenInheritanceDerived().InterfaceRequiresMultipleOfThree(3);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveNonExplicitEnsuresTest()
    {
      new RewrittenInheritanceDerived().InterfaceEnsuresMultipleOfThree(1);
      new RewrittenInheritanceDerived().InterfaceEnsuresMultipleOfThree(2);
      new RewrittenInheritanceDerived().InterfaceEnsuresMultipleOfThree(3);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveDoubleRequiresTest()
    {
      new RewrittenInheritanceDerived().InterfaceRequires(6);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveDoubleEnsuresTest()
    {
      new RewrittenInheritanceDerived().InterfaceEnsures(6);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void NegativeDoubleRequiresTest1()
    {
      new RewrittenInheritanceDerived().InterfaceRequires(1);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void NegativeDoubleRequiresTest2()
    {
      new RewrittenInheritanceDerived().InterfaceRequires(2);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void NegativeDoubleRequiresTest3()
    {
      new RewrittenInheritanceDerived().InterfaceRequires(3);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeDoubleEnsuresTest1()
    {
      new RewrittenInheritanceDerived().InterfaceEnsures(1);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeDoubleEnsuresTest2()
    {
      new RewrittenInheritanceDerived().InterfaceEnsures(2);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeDoubleEnsuresTest3()
    {
      new RewrittenInheritanceDerived().InterfaceEnsures(3);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveQuantifierTest1()
    {
      IQuantifierTest1 iq = new QuantifierTest1();
      int[] A = new int[] { 3, 4, 5 };
      iq.ModifyArray(A, 3, true);
      iq.CopyArray(A, true);
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeQuantifierTest1a()
    {
      IQuantifierTest1 iq = new QuantifierTest1();
      int[] A = new int[] { 3, 4, 5 };
      iq.ModifyArray(A, 3, false);
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeQuantifierTest1b()
    {
      IQuantifierTest1 iq = new QuantifierTest1();
      int[] A = new int[] { 3, 4, 5 };
      iq.CopyArray(A, false);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveQuantifierTest2()
    {
      IQuantifierTest2 iq = new QuantifierTest2();
      int[] A = new int[] { 3, 4, 5 };
      iq.ModifyArray(A, 3, true);
      iq.CopyArray(A, true);
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeQuantifierTest2a()
    {
      IQuantifierTest2 iq = new QuantifierTest2();
      int[] A = new int[] { 3, 4, 5 };
      iq.ModifyArray(A, 3, false);
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeQuantifierTest2b()
    {
      IQuantifierTest2 iq = new QuantifierTest2();
      int[] A = new int[] { 3, 4, 5 };
      iq.CopyArray(A, false);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveAbstractClass()
    {
      AbstractClassInterface a = new AbstractClassInterfaceImplementationSubType();
      a.M(true);
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeAbstractClass()
    {
      AbstractClassInterface a = new AbstractClassInterfaceImplementationSubType();
      a.M(false);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveBaseClassWithMethodCallInContract()
    {
      DerivedClassForBaseClassWithMethodCallInContract o = new DerivedClassForBaseClassWithMethodCallInContract();
      o.M(3);
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void NegativeBaseClassWithMethodCallInContract()
    {
      DerivedClassForBaseClassWithMethodCallInContract o = new DerivedClassForBaseClassWithMethodCallInContract();
      o.M(0);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveInheritOld()
    {
      DerivedFromBaseClassWithOldAndResult o = new DerivedFromBaseClassWithOldAndResult();
      int z = 3;
      o.InheritOld(ref z, true);
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeInheritOld()
    {
      DerivedFromBaseClassWithOldAndResult o = new DerivedFromBaseClassWithOldAndResult();
      int z = 3;
      o.InheritOld(ref z, false);
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveInheritOldInQuantifier()
    {
      DerivedFromBaseClassWithOldAndResult o = new DerivedFromBaseClassWithOldAndResult();
      int[] A = new int[] { 3, 4, 5 };
      o.InheritOldInQuantifier(A, true);
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeInheritOldInQuantifier()
    {
      DerivedFromBaseClassWithOldAndResult o = new DerivedFromBaseClassWithOldAndResult();
      int[] A = new int[] { 3, 4, 5 };
      o.InheritOldInQuantifier(A, false);
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveInheritResultInContract()
    {
      DerivedFromBaseClassWithOldAndResult o = new DerivedFromBaseClassWithOldAndResult();
      o.InheritResult(3, true);
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeInheritResultInContract()
    {
      DerivedFromBaseClassWithOldAndResult o = new DerivedFromBaseClassWithOldAndResult();
      o.InheritResult(3, false);
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveInheritResultInQuantifier()
    {
      DerivedFromBaseClassWithOldAndResult o = new DerivedFromBaseClassWithOldAndResult();
      int[] A = new int[] { 3, 4, 5 };
      o.InheritResultInQuantifier(A, true);
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeInheritResultInQuantifier()
    {
      DerivedFromBaseClassWithOldAndResult o = new DerivedFromBaseClassWithOldAndResult();
      int[] A = new int[] { 3, 4, 5 };
      o.InheritResultInQuantifier(A, false);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.InvariantException))]
    public void NegativeProperlyInstantiateInheritedInvariants()
    {
      NonGenericDerivedFromGenericWithInvariant ng = new NonGenericDerivedFromGenericWithInvariant(false);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveProperlyInstantiateInheritedInvariants()
    {
      NonGenericDerivedFromGenericWithInvariant ng = new NonGenericDerivedFromGenericWithInvariant(true);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveExplicitGenericInterfaceMethod()
    {
      IInterfaceWithGenericMethod i = new ExplicitGenericInterfaceMethodImplementation(0);
      foreach (var x in i.Bar<string>())
      {
      }

    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
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
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
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


    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveImplicitGenericInterfaceMethod()
    {
      var i = new ImplicitGenericInterfaceMethodImplementation(0);
      foreach (var x in i.Bar<string>())
      {
      }

    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
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
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
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
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveIFaceWithMethodCalInContract()
    {
      IFaceWithMethodCallInContract o = new ImplForIFaceWithMethodCallInContract();
      o.M(1);
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void NegativeIFaceWithMethodCalInContract1()
    {
      IFaceWithMethodCallInContract o = new ImplForIFaceWithMethodCallInContract();
      o.M(3);
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeIFaceWithMethodCalInContract2()
    {
      IFaceWithMethodCallInContract o = new ImplForIFaceWithMethodCallInContract();
      o.M(5);
    }
    #endregion Interface with method call in contract

    #region ContractClass for abstract methods
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveAbstractMethodWithContract()
    {
      AbstractClass a = new ImplForAbstractMethod();
      a.ReturnFirst(new int[] { 3, 4, 5 }, true);
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveAbstractMethodWithContract2()
    {
      AbstractClass a = new ImplForAbstractMethod();
      a.Increment(3, true);
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void NegativeAbstractMethodWithContract1()
    {
      AbstractClass a = new ImplForAbstractMethod();
      a.ReturnFirst(null, true);
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void NegativeAbstractMethodWithContract2()
    {
      AbstractClass a = new ImplForAbstractMethod();
      a.ReturnFirst(new int[] { }, true);
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeAbstractMethodWithContract3()
    {
      AbstractClass a = new ImplForAbstractMethod();
      a.ReturnFirst(new int[] { 3, 4, 5 }, false);
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void NegativeAbstractMethodWithContract4()
    {
      AbstractClass a = new ImplForAbstractMethod();
      a.Increment(-3, true);
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeAbstractMethodWithContract5()
    {
      AbstractClass a = new ImplForAbstractMethod();
      a.Increment(3, false);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveGenericAbstractClassWithContract()
    {
      GenericAbstractClass<string, string> a = new ImplForGenericAbstractClass();
      a.ReturnFirst(new[] { "a", "B", "c" }, "B", true);
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveGenericAbstractClassWithContract2()
    {
      GenericAbstractClass<string, string> a = new ImplForGenericAbstractClass();
      var result = a.Collection(2, 3);
      Assert.AreEqual(2, result.Length);
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveGenericAbstractClassWithContract3()
    {
      GenericAbstractClass<string, string> a = new ImplForGenericAbstractClass();
      var result = a.FirstNonNullMatch(new[] { null, null, "a", "b" });
      Assert.AreEqual("a", result);
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveGenericAbstractClassWithContract5()
    {
      GenericAbstractClass<string, string> a = new ImplForGenericAbstractClass();
      var result = a.GenericMethod<float>(new[] { null, null, "a", "b" });
      Assert.AreEqual(0, result.Length);
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveGenericAbstractClassWithContract6()
    {
      GenericAbstractClass<string, string> a = new ImplForGenericAbstractClass();
      var result = a.GenericMethod<string>(new[] { "a", "b" });
      Assert.AreEqual(2, result.Length);
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveGenericAbstractClassWithContract7()
    {
      GenericAbstractClass<string, string> a = new ImplForGenericAbstractClass();
      var result = a.GenericMethod<object>(new[] { "a", "b" });
      Assert.AreEqual(2, result.Length);
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void NegativeGenericAbstractClassWithContract1()
    {
      GenericAbstractClass<string, string> a = new ImplForGenericAbstractClass();
      a.ReturnFirst(null, null, true);
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void NegativeGenericAbstractClassWithContract2()
    {
      GenericAbstractClass<string, string> a = new ImplForGenericAbstractClass();
      a.ReturnFirst(new string[] { }, null, true);
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeGenericAbstractClassWithContract3()
    {
      GenericAbstractClass<string, string> a = new ImplForGenericAbstractClass();
      a.ReturnFirst(new[] { "3", "4", "5" }, "", false);
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeGenericAbstractClassWithContract4()
    {
      GenericAbstractClass<string, string> a = new ImplForGenericAbstractClass();
      a.Collection(5, 5);
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeGenericAbstractClassWithContract5()
    {
      GenericAbstractClass<string, string> a = new ImplForGenericAbstractClass();
      a.FirstNonNullMatch(new string[] { null, null, null });
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void NegativeGenericAbstractClassWithContract6()
    {
      GenericAbstractClass<string, string> a = new ImplForGenericAbstractClass();
      var result = a.GenericMethod<string>(null);
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeGenericAbstractClassWithContract7()
    {
      GenericAbstractClass<string, string> a = new ImplForGenericAbstractClass();
      var result = a.GenericMethod<int>(new string[0]);
    }
    #endregion ContractClass for abstract methods

    #region Stelem specialization
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveStelemSpecialization1()
    {
      var st = new DerivedOfClosureWithStelem<string>();
      st.M(new string[] { "a", "b", "c" }, 1);
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
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
  public class PublicProperty : DisableAssertUI {
    #region Test Management

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
    #region Tests
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositivePublicProperty()
    {
      D d = new D();
      d.foo(-3);
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void NegativePublicProperty()
    {
      D d = new D();
      d.foo(3);
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositivePublicPropertyUsingVirtualProperty()
    {
      SubtypeOfUsingVirtualProperty d = new SubtypeOfUsingVirtualProperty();
      // This will fail if the inherited precondition is checked by calling
      //get_P with a non-virtual call.
      d.foo(3);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveTestProperRetargettingOfImplicitInterfaceMethod()
    {
      var o = new Strilanc.C(true);
      o.S();
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void NegativeTestProperRetargettingOfImplicitInterfaceMethod()
    {
      var o = new Strilanc.C(false);
      try
      {
        o.S();
        throw new Exception();
      }
      catch (TestRewriterMethods.PreconditionException p)
      {
        Assert.AreEqual("this.P", p.Condition);
      }
    }
    #endregion Tests
  }

  [TestClass]
  public class ThomasPani : DisableAssertUI
  {
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveInheritedInv()
    {
      var u = new CodeUnderTest.ThomasPani.U();
      u.SetToValidValue(true);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void NegativeInheritedInv()
    {
      var u = new CodeUnderTest.ThomasPani.U();
      try
      {
        u.SetToValidValue(false);
        throw new Exception();
      } catch (TestRewriterMethods.InvariantException e)
      {
        Assert.AreEqual("i > 0", e.Condition);
      }
    }
    
  }
}
