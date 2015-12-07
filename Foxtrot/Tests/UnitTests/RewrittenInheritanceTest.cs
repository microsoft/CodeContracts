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
using CodeUnderTest;
using Xunit;

namespace Tests {

  #region Tests
  public class RewrittenInheritanceTest : DisableAssertUI {

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveOneLevelInheritanceInvariantTest()
    {
      new RewrittenInheritanceDerived().SetInheritedValueMustBeTrue(true);
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeOneLevelInheritanceInvariantTest()
    {
      Assert.Throws<TestRewriterMethods.InvariantException>(() => new RewrittenInheritanceDerived().SetInheritedValueMustBeTrue(false));
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveTwoLevelInheritanceInvariantTest()
    {
      new RewrittenInheritanceDerived().SetBaseValueMustBeTrue(true);
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeTwoLevelInheritanceInvariantTest()
    {
      Assert.Throws<TestRewriterMethods.InvariantException>(() => new RewrittenInheritanceDerived().SetBaseValueMustBeTrue(false));
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveBareInheritanceRequiresTest()
    {
      new RewrittenInheritanceDerived().BareRequires(true);
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeBareInheritanceRequiresTest()
    {
      Assert.Throws<TestRewriterMethods.PreconditionException>(() => new RewrittenInheritanceDerived().BareRequires(false));
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveLegacyInheritanceRequiresTest()
    {
      new RewrittenInheritanceDerived().LegacyRequires("");
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeLegacyInheritanceRequiresTest()
    {
      Assert.Throws<ArgumentNullException>(() => new RewrittenInheritanceDerived().LegacyRequires(null));
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveInheritanceRequiresWithExceptionTest()
    {
      new RewrittenInheritanceDerived().RequiresWithException("");
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeInheritanceRequiresWithExceptionTest()
    {
      Assert.Throws<ArgumentNullException>(() => new RewrittenInheritanceDerived().RequiresWithException(null));
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveAdditiveInheritanceRequiresTest()
    {
      new RewrittenInheritanceDerived().AdditiveRequires(101);
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeAdditiveOneLevelInheritanceRequiresTest()
    {
      Assert.Throws<TestRewriterMethods.PreconditionException>(() => new RewrittenInheritanceDerived().AdditiveRequires(0));
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeAdditiveTwoLevelInheritanceRequiresTest()
    {
      Assert.Throws<TestRewriterMethods.PreconditionException>(() => new RewrittenInheritanceDerived().AdditiveRequires(0));
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveSubtractiveInheritanceRequiresTest()
    {
      new RewrittenInheritanceDerived().SubtractiveRequires(101);
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeSubtractiveOneLevelInheritanceRequiresTest()
    {
      Assert.Throws<TestRewriterMethods.PreconditionException>(() => new RewrittenInheritanceDerived().SubtractiveRequires(0));
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeSubtractiveTwoLevelInheritanceRequiresTest()
    {
      Assert.Throws<TestRewriterMethods.PreconditionException>(() => new RewrittenInheritanceDerived().SubtractiveRequires(1));
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveAdditiveInheritanceEnsuresTest()
    {
      new RewrittenInheritanceDerived().AdditiveEnsures(101);
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeAdditiveOneLevelInheritanceEnsuresTest()
    {
      Assert.Throws<TestRewriterMethods.PostconditionException>(() => new RewrittenInheritanceDerived().AdditiveEnsures(1));
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeAdditiveTwoLevelInheritanceEnsuresTest()
    {
      Assert.Throws<TestRewriterMethods.PostconditionException>(() => new RewrittenInheritanceDerived().AdditiveEnsures(0));
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveSubtractiveInheritanceEnsuresTest()
    {
      new RewrittenInheritanceDerived().SubtractiveEnsures(101);
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeSubtractiveOneLevelInheritanceEnsuresTest()
    {
      Assert.Throws<TestRewriterMethods.PostconditionException>(() => new RewrittenInheritanceDerived().SubtractiveEnsures(0));
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeSubtractiveTwoLevelInheritanceEnsuresTest()
    {
      Assert.Throws<TestRewriterMethods.PostconditionException>(() => new RewrittenInheritanceDerived().SubtractiveEnsures(1));
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveInheritedImplicitRequiresTest()
    {
      new RewrittenInheritanceDerived().InterfaceBaseRequires(true);
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveInheritedImplicitEnsuresTest()
    {
      new RewrittenInheritanceDerived().InterfaceBaseEnsures(true);
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeInheritedImplicitRequiresTest()
    {
      Assert.Throws<TestRewriterMethods.PreconditionException>(() => new RewrittenInheritanceDerived().InterfaceBaseRequires(false));
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeInheritedImplicitEnsuresTest()
    {
      Assert.Throws<TestRewriterMethods.PostconditionException>(() => new RewrittenInheritanceDerived().InterfaceBaseEnsures(false));
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveImplicitlyOverriddenRequiresTest()
    {
      new RewrittenInheritanceDerived().ImplicitlyOverriddenInterfaceRequires(true);
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveImplicitlyOverriddenEnsuresTest()
    {
      new RewrittenInheritanceDerived().ImplicitlyOverriddenInterfaceEnsures(true);
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeImplicitlyOverriddenRequiresTest()
    {
      Assert.Throws<TestRewriterMethods.PreconditionException>(() => new RewrittenInheritanceDerived().ImplicitlyOverriddenInterfaceRequires(false));
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeImplicitlyOverriddenEnsuresTest()
    {
      Assert.Throws<TestRewriterMethods.PostconditionException>(() => new RewrittenInheritanceDerived().ImplicitlyOverriddenInterfaceEnsures(false));
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveExplicitlyOverriddenRequiresTest()
    {
      new RewrittenInheritanceDerived().ExplicitlyOverriddenInterfaceRequires(true);
      new RewrittenInheritanceDerived().ExplicitlyOverriddenInterfaceRequires(false);
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveExplicitlyOverriddenEnsuresTest()
    {
      new RewrittenInheritanceDerived().ExplicitlyOverriddenInterfaceEnsures(true);
      new RewrittenInheritanceDerived().ExplicitlyOverriddenInterfaceEnsures(false);
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveMultiplyImplicitlyOverriddenRequiresTest()
    {
      new RewrittenInheritanceDerived().MultiplyImplicitlyOverriddenInterfaceRequires(true);
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveMultiplyImplicitlyOverriddenEnsuresTest()
    {
      new RewrittenInheritanceDerived().MultiplyImplicitlyOverriddenInterfaceEnsures(true);
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeMultiplyImplicitlyOverriddenRequiresTest()
    {
      Assert.Throws<TestRewriterMethods.PreconditionException>(() => new RewrittenInheritanceDerived().MultiplyImplicitlyOverriddenInterfaceRequires(false));
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeMultiplyImplicitlyOverriddenEnsuresTest()
    {
      Assert.Throws<TestRewriterMethods.PostconditionException>(() => new RewrittenInheritanceDerived().MultiplyImplicitlyOverriddenInterfaceEnsures(false));
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveMultiplyExplicitlyOverriddenRequiresTest()
    {
      ((IRewrittenMultipleInheritanceInterface)new RewrittenInheritanceDerived()).MultiplyExplicitlyOverriddenInterfaceRequires(true);
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveMultiplyExplicitlyOverriddenEnsuresTest()
    {
      ((IRewrittenMultipleInheritanceInterface)new RewrittenInheritanceDerived()).MultiplyExplicitlyOverriddenInterfaceEnsures(true);
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeMultiplyExplicitlyOverriddenRequiresTest()
    {
      Assert.Throws<TestRewriterMethods.PreconditionException>(() => ((IRewrittenMultipleInheritanceInterface)new RewrittenInheritanceDerived()).MultiplyExplicitlyOverriddenInterfaceRequires(false));
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeMultiplyExplicitlyOverriddenEnsuresTest()
    {
      Assert.Throws<TestRewriterMethods.PostconditionException>(() => ((IRewrittenMultipleInheritanceInterface)new RewrittenInheritanceDerived()).MultiplyExplicitlyOverriddenInterfaceEnsures(false));
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveMultiplyDifferentlyImplicitlyOverriddenRequiresTest()
    {
      new RewrittenInheritanceDerived().MultiplyDifferentlyOverriddenInterfaceRequires(true);
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveMultiplyDifferentlyExplicitlyOverriddenRequiresTest()
    {
      ((IRewrittenMultipleInheritanceInterface)new RewrittenInheritanceDerived()).MultiplyDifferentlyOverriddenInterfaceRequires(true);
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveMultiplyDifferentlyImplicitlyOverriddenEnsuresTest()
    {
      new RewrittenInheritanceDerived().MultiplyDifferentlyOverriddenInterfaceEnsures(true);
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveMultiplyDifferentlyExplicitlyOverriddenEnsuresTest()
    {
      ((IRewrittenMultipleInheritanceInterface)new RewrittenInheritanceDerived()).MultiplyDifferentlyOverriddenInterfaceEnsures(true);
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeMultiplyDifferentlyImplicitlyOverriddenRequiresTest()
    {
      Assert.Throws<TestRewriterMethods.PreconditionException>(() => new RewrittenInheritanceDerived().MultiplyDifferentlyOverriddenInterfaceRequires(false));
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeMultiplyDifferentlyExplicitlyOverriddenRequiresTest()
    {
      Assert.Throws<TestRewriterMethods.PreconditionException>(() => ((IRewrittenMultipleInheritanceInterface)new RewrittenInheritanceDerived()).MultiplyDifferentlyOverriddenInterfaceRequires(false));
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeMultiplyDifferentlyImplicitlyOverriddenEnsuresTest()
    {
      Assert.Throws<TestRewriterMethods.PostconditionException>(() => new RewrittenInheritanceDerived().MultiplyDifferentlyOverriddenInterfaceEnsures(false));
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeMultiplyDifferentlyExplicitlyOverriddenEnsuresTest()
    {
      Assert.Throws<TestRewriterMethods.PostconditionException>(() => ((IRewrittenMultipleInheritanceInterface)new RewrittenInheritanceDerived()).MultiplyDifferentlyOverriddenInterfaceEnsures(false));
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveImplicitRequiresTest()
    {
      new RewrittenInheritanceDerived().InterfaceRequiresMultipleOfTwo(2);
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveImplicitEnsuresTest()
    {
      new RewrittenInheritanceDerived().InterfaceEnsuresMultipleOfTwo(2);
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeImplicitRequiresTest()
    {
      Assert.Throws<TestRewriterMethods.PreconditionException>(() => new RewrittenInheritanceDerived().InterfaceRequiresMultipleOfTwo(3));
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeImplicitEnsuresTest()
    {
      Assert.Throws<TestRewriterMethods.PostconditionException>(() => new RewrittenInheritanceDerived().InterfaceEnsuresMultipleOfTwo(3));
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveExplicitRequiresTest()
    {
      ((IRewrittenInheritanceInterfaceDerived2)new RewrittenInheritanceDerived()).InterfaceRequiresMultipleOfThree(3);
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveExplicitEnsuresTest()
    {
      ((IRewrittenInheritanceInterfaceDerived2)new RewrittenInheritanceDerived()).InterfaceEnsuresMultipleOfThree(3);
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeExplicitRequiresTest()
    {
      Assert.Throws<TestRewriterMethods.PreconditionException>(() => ((IRewrittenInheritanceInterfaceDerived2)new RewrittenInheritanceDerived()).InterfaceRequiresMultipleOfThree(2));
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeExplicitEnsuresTest()
    {
      Assert.Throws<TestRewriterMethods.PostconditionException>(() => ((IRewrittenInheritanceInterfaceDerived2)new RewrittenInheritanceDerived()).InterfaceEnsuresMultipleOfThree(2));
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveNonExplicitRequiresTest()
    {
      new RewrittenInheritanceDerived().InterfaceRequiresMultipleOfThree(1);
      new RewrittenInheritanceDerived().InterfaceRequiresMultipleOfThree(2);
      new RewrittenInheritanceDerived().InterfaceRequiresMultipleOfThree(3);
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveNonExplicitEnsuresTest()
    {
      new RewrittenInheritanceDerived().InterfaceEnsuresMultipleOfThree(1);
      new RewrittenInheritanceDerived().InterfaceEnsuresMultipleOfThree(2);
      new RewrittenInheritanceDerived().InterfaceEnsuresMultipleOfThree(3);
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveDoubleRequiresTest()
    {
      new RewrittenInheritanceDerived().InterfaceRequires(6);
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveDoubleEnsuresTest()
    {
      new RewrittenInheritanceDerived().InterfaceEnsures(6);
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeDoubleRequiresTest1()
    {
      Assert.Throws<TestRewriterMethods.PreconditionException>(() => new RewrittenInheritanceDerived().InterfaceRequires(1));
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeDoubleRequiresTest2()
    {
      Assert.Throws<TestRewriterMethods.PreconditionException>(() => new RewrittenInheritanceDerived().InterfaceRequires(2));
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeDoubleRequiresTest3()
    {
      Assert.Throws<TestRewriterMethods.PreconditionException>(() => new RewrittenInheritanceDerived().InterfaceRequires(3));
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeDoubleEnsuresTest1()
    {
      Assert.Throws<TestRewriterMethods.PostconditionException>(() => new RewrittenInheritanceDerived().InterfaceEnsures(1));
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeDoubleEnsuresTest2()
    {
      Assert.Throws<TestRewriterMethods.PostconditionException>(() => new RewrittenInheritanceDerived().InterfaceEnsures(2));
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeDoubleEnsuresTest3()
    {
      Assert.Throws<TestRewriterMethods.PostconditionException>(() => new RewrittenInheritanceDerived().InterfaceEnsures(3));
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveQuantifierTest1()
    {
      IQuantifierTest1 iq = new QuantifierTest1();
      int[] A = new int[] { 3, 4, 5 };
      iq.ModifyArray(A, 3, true);
      iq.CopyArray(A, true);
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeQuantifierTest1a()
    {
      IQuantifierTest1 iq = new QuantifierTest1();
      int[] A = new int[] { 3, 4, 5 };
      Assert.Throws<TestRewriterMethods.PostconditionException>(() => iq.ModifyArray(A, 3, false));
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeQuantifierTest1b()
    {
      IQuantifierTest1 iq = new QuantifierTest1();
      int[] A = new int[] { 3, 4, 5 };
      Assert.Throws<TestRewriterMethods.PostconditionException>(() => iq.CopyArray(A, false));
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveQuantifierTest2()
    {
      IQuantifierTest2 iq = new QuantifierTest2();
      int[] A = new int[] { 3, 4, 5 };
      iq.ModifyArray(A, 3, true);
      iq.CopyArray(A, true);
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeQuantifierTest2a()
    {
      IQuantifierTest2 iq = new QuantifierTest2();
      int[] A = new int[] { 3, 4, 5 };
      Assert.Throws<TestRewriterMethods.PostconditionException>(() => iq.ModifyArray(A, 3, false));
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeQuantifierTest2b()
    {
      IQuantifierTest2 iq = new QuantifierTest2();
      int[] A = new int[] { 3, 4, 5 };
      Assert.Throws<TestRewriterMethods.PostconditionException>(() => iq.CopyArray(A, false));
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveAbstractClass()
    {
      AbstractClassInterface a = new AbstractClassInterfaceImplementationSubType();
      a.M(true);
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeAbstractClass()
    {
      AbstractClassInterface a = new AbstractClassInterfaceImplementationSubType();
      Assert.Throws<TestRewriterMethods.PostconditionException>(() => a.M(false));
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveBaseClassWithMethodCallInContract()
    {
      DerivedClassForBaseClassWithMethodCallInContract o = new DerivedClassForBaseClassWithMethodCallInContract();
      o.M(3);
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeBaseClassWithMethodCallInContract()
    {
      DerivedClassForBaseClassWithMethodCallInContract o = new DerivedClassForBaseClassWithMethodCallInContract();
      Assert.Throws<TestRewriterMethods.PreconditionException>(() => o.M(0));
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveInheritOld()
    {
      DerivedFromBaseClassWithOldAndResult o = new DerivedFromBaseClassWithOldAndResult();
      int z = 3;
      o.InheritOld(ref z, true);
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeInheritOld()
    {
      DerivedFromBaseClassWithOldAndResult o = new DerivedFromBaseClassWithOldAndResult();
      int z = 3;
      Assert.Throws<TestRewriterMethods.PostconditionException>(() => o.InheritOld(ref z, false));
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveInheritOldInQuantifier()
    {
      DerivedFromBaseClassWithOldAndResult o = new DerivedFromBaseClassWithOldAndResult();
      int[] A = new int[] { 3, 4, 5 };
      o.InheritOldInQuantifier(A, true);
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeInheritOldInQuantifier()
    {
      DerivedFromBaseClassWithOldAndResult o = new DerivedFromBaseClassWithOldAndResult();
      int[] A = new int[] { 3, 4, 5 };
      Assert.Throws<TestRewriterMethods.PostconditionException>(() => o.InheritOldInQuantifier(A, false));
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveInheritResultInContract()
    {
      DerivedFromBaseClassWithOldAndResult o = new DerivedFromBaseClassWithOldAndResult();
      o.InheritResult(3, true);
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeInheritResultInContract()
    {
      DerivedFromBaseClassWithOldAndResult o = new DerivedFromBaseClassWithOldAndResult();
      Assert.Throws<TestRewriterMethods.PostconditionException>(() => o.InheritResult(3, false));
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveInheritResultInQuantifier()
    {
      DerivedFromBaseClassWithOldAndResult o = new DerivedFromBaseClassWithOldAndResult();
      int[] A = new int[] { 3, 4, 5 };
      o.InheritResultInQuantifier(A, true);
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeInheritResultInQuantifier()
    {
      DerivedFromBaseClassWithOldAndResult o = new DerivedFromBaseClassWithOldAndResult();
      int[] A = new int[] { 3, 4, 5 };
      Assert.Throws<TestRewriterMethods.PostconditionException>(() => o.InheritResultInQuantifier(A, false));
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeProperlyInstantiateInheritedInvariants()
    {
      Assert.Throws<TestRewriterMethods.InvariantException>(
        () =>
        {
          NonGenericDerivedFromGenericWithInvariant ng = new NonGenericDerivedFromGenericWithInvariant(false);
        });
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveProperlyInstantiateInheritedInvariants()
    {
      NonGenericDerivedFromGenericWithInvariant ng = new NonGenericDerivedFromGenericWithInvariant(true);
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveExplicitGenericInterfaceMethod()
    {
      IInterfaceWithGenericMethod i = new ExplicitGenericInterfaceMethodImplementation(0);
      foreach (var x in i.Bar<string>())
      {
      }

    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
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
        Assert.Equal(": Contract.ForAll(Contract.Result<IEnumerable<T>>(), x => x != null)", e.Message);
      }
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
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
        Assert.Equal(": Contract.Result<IEnumerable<T>>() != null", e.Message);
      }
    }


    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveImplicitGenericInterfaceMethod()
    {
      var i = new ImplicitGenericInterfaceMethodImplementation(0);
      foreach (var x in i.Bar<string>())
      {
      }

    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
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
        Assert.Equal(": Contract.ForAll(Contract.Result<IEnumerable<T>>(), x => x != null)", e.Message);
      }
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
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
        Assert.Equal(": Contract.Result<IEnumerable<T>>() != null", e.Message);
      }
    }


    #region Interface with method call in contract
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveIFaceWithMethodCalInContract()
    {
      IFaceWithMethodCallInContract o = new ImplForIFaceWithMethodCallInContract();
      o.M(1);
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeIFaceWithMethodCalInContract1()
    {
      IFaceWithMethodCallInContract o = new ImplForIFaceWithMethodCallInContract();
      Assert.Throws<TestRewriterMethods.PreconditionException>(() => o.M(3));
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeIFaceWithMethodCalInContract2()
    {
      IFaceWithMethodCallInContract o = new ImplForIFaceWithMethodCallInContract();
      Assert.Throws<TestRewriterMethods.PostconditionException>(() => o.M(5));
    }
    #endregion Interface with method call in contract

    #region ContractClass for abstract methods
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveAbstractMethodWithContract()
    {
      AbstractClass a = new ImplForAbstractMethod();
      a.ReturnFirst(new int[] { 3, 4, 5 }, true);
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveAbstractMethodWithContract2()
    {
      AbstractClass a = new ImplForAbstractMethod();
      a.Increment(3, true);
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeAbstractMethodWithContract1()
    {
      AbstractClass a = new ImplForAbstractMethod();
      Assert.Throws<TestRewriterMethods.PreconditionException>(() => a.ReturnFirst(null, true));
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeAbstractMethodWithContract2()
    {
      AbstractClass a = new ImplForAbstractMethod();
      Assert.Throws<TestRewriterMethods.PreconditionException>(() => a.ReturnFirst(new int[] { }, true));
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeAbstractMethodWithContract3()
    {
      AbstractClass a = new ImplForAbstractMethod();
      Assert.Throws<TestRewriterMethods.PostconditionException>(() => a.ReturnFirst(new int[] { 3, 4, 5 }, false));
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeAbstractMethodWithContract4()
    {
      AbstractClass a = new ImplForAbstractMethod();
      Assert.Throws<TestRewriterMethods.PreconditionException>(() => a.Increment(-3, true));
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeAbstractMethodWithContract5()
    {
      AbstractClass a = new ImplForAbstractMethod();
      Assert.Throws<TestRewriterMethods.PostconditionException>(() => a.Increment(3, false));
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveGenericAbstractClassWithContract()
    {
      GenericAbstractClass<string, string> a = new ImplForGenericAbstractClass();
      a.ReturnFirst(new[] { "a", "B", "c" }, "B", true);
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveGenericAbstractClassWithContract2()
    {
      GenericAbstractClass<string, string> a = new ImplForGenericAbstractClass();
      var result = a.Collection(2, 3);
      Assert.Equal(2, result.Length);
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveGenericAbstractClassWithContract3()
    {
      GenericAbstractClass<string, string> a = new ImplForGenericAbstractClass();
      var result = a.FirstNonNullMatch(new[] { null, null, "a", "b" });
      Assert.Equal("a", result);
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveGenericAbstractClassWithContract5()
    {
      GenericAbstractClass<string, string> a = new ImplForGenericAbstractClass();
      var result = a.GenericMethod<float>(new[] { null, null, "a", "b" });
      Assert.Equal(0, result.Length);
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveGenericAbstractClassWithContract6()
    {
      GenericAbstractClass<string, string> a = new ImplForGenericAbstractClass();
      var result = a.GenericMethod<string>(new[] { "a", "b" });
      Assert.Equal(2, result.Length);
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveGenericAbstractClassWithContract7()
    {
      GenericAbstractClass<string, string> a = new ImplForGenericAbstractClass();
      var result = a.GenericMethod<object>(new[] { "a", "b" });
      Assert.Equal(2, result.Length);
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeGenericAbstractClassWithContract1()
    {
      GenericAbstractClass<string, string> a = new ImplForGenericAbstractClass();
      Assert.Throws<TestRewriterMethods.PreconditionException>(() => a.ReturnFirst(null, null, true));
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeGenericAbstractClassWithContract2()
    {
      GenericAbstractClass<string, string> a = new ImplForGenericAbstractClass();
      Assert.Throws<TestRewriterMethods.PreconditionException>(() => a.ReturnFirst(new string[] { }, null, true));
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeGenericAbstractClassWithContract3()
    {
      GenericAbstractClass<string, string> a = new ImplForGenericAbstractClass();
      Assert.Throws<TestRewriterMethods.PostconditionException>(() => a.ReturnFirst(new[] { "3", "4", "5" }, "", false));
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeGenericAbstractClassWithContract4()
    {
      GenericAbstractClass<string, string> a = new ImplForGenericAbstractClass();
      Assert.Throws<TestRewriterMethods.PostconditionException>(() => a.Collection(5, 5));
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeGenericAbstractClassWithContract5()
    {
      GenericAbstractClass<string, string> a = new ImplForGenericAbstractClass();
      Assert.Throws<TestRewriterMethods.PostconditionException>(() => a.FirstNonNullMatch(new string[] { null, null, null }));
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeGenericAbstractClassWithContract6()
    {
      GenericAbstractClass<string, string> a = new ImplForGenericAbstractClass();
      Assert.Throws<TestRewriterMethods.PreconditionException>(
        () =>
        {
          var result = a.GenericMethod<string>(null);
        });
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeGenericAbstractClassWithContract7()
    {
      GenericAbstractClass<string, string> a = new ImplForGenericAbstractClass();
      Assert.Throws<TestRewriterMethods.PostconditionException>(
        () =>
        {
          var result = a.GenericMethod<int>(new string[0]);
        });
    }
    #endregion ContractClass for abstract methods

    #region Stelem specialization
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveStelemSpecialization1()
    {
      var st = new DerivedOfClosureWithStelem<string>();
      st.M(new string[] { "a", "b", "c" }, 1);
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
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
        Assert.Equal(e.Message, ": i + 1 < ts.Length");
      }
    }
    #endregion

  }

  #endregion

  public class PublicProperty : DisableAssertUI {
    #region Tests
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositivePublicProperty()
    {
      D d = new D();
      d.foo(-3);
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativePublicProperty()
    {
      D d = new D();
      Assert.Throws<TestRewriterMethods.PreconditionException>(() => d.foo(3));
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositivePublicPropertyUsingVirtualProperty()
    {
      SubtypeOfUsingVirtualProperty d = new SubtypeOfUsingVirtualProperty();
      // This will fail if the inherited precondition is checked by calling
      //get_P with a non-virtual call.
      d.foo(3);
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveTestProperRetargettingOfImplicitInterfaceMethod()
    {
      var o = new Strilanc.C(true);
      o.S();
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
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
        Assert.Equal("this.P", p.Condition);
      }
    }
    #endregion Tests
  }

  public class ThomasPani : DisableAssertUI
  {
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void PositiveInheritedInv()
    {
      var u = new CodeUnderTest.ThomasPani.U();
      u.SetToValidValue(true);
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NegativeInheritedInv()
    {
      var u = new CodeUnderTest.ThomasPani.U();
      try
      {
        u.SetToValidValue(false);
        throw new Exception();
      } catch (TestRewriterMethods.InvariantException e)
      {
        Assert.Equal("i > 0", e.Condition);
      }
    }
    
  }
}
