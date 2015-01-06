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
using System.Diagnostics;

using System.Diagnostics.Contracts;
using Microsoft.Contracts.Foxtrot;
using System.Reflection;
using Microsoft.CSharp;
using OutOfBand;
using SubtypeWithoutContracts;

namespace Tests {
  [TestClass()]
  public class OutOfBandTest : DisableAssertUI {

    #region Tests


    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveOutOfBandInc()
    {
      new OutOfBand.C().Inc(3);
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void NegativeOutOfBandInc()
    {
      new OutOfBand.C().Inc(27);
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void NegativeOutOfBandInc2()
    {
      try
      {
        new OutOfBand.C().Inc(-1);
      }
      catch (TestRewriterMethods.PreconditionException p)
      {
        // Since we are not running asmmeta on OutOfBand.Contracts and we use call-site-requires
        // we don't get the string here.
        // Assert.AreEqual("y >= 0", p.Condition);
        Assert.AreEqual("y must be non-negative", p.User);
      }
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void NegativeOutOfBandInc3()
    {
      try
      {
        new OutOfBand.C().Inc(27);
      }
      catch (TestRewriterMethods.PreconditionException p)
      {
        // Since we are not running asmmeta on OutOfBand.Contracts and we use call-site-requires
        // we don't get the string here.
        // Assert.AreEqual("y == 3", p.Condition);
        Assert.AreEqual(null, p.User);
      }
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveOutOfBandInvariant()
    {
      new OutOfBand.C();
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.InvariantException))]
    public void NegativeOutOfBandInvariant()
    {
      new OutOfBand.C(false);
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveOutOfBandRequires()
    {
      var c = new OutOfBand.C();
      var foo = new OutOfBand.Foo(1);
      c.FooMethodTakingTypeFromSameAssembly(foo);
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void NegativeOutOfBandRequires1()
    {
      var c = new OutOfBand.C();
      c.FooMethodTakingTypeFromSameAssembly(null);
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeOutOfBandRequires2()
    {
      var c = new OutOfBand.C();
      var foo = new OutOfBand.Foo(-1);
      c.FooMethodTakingTypeFromSameAssembly(foo);
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveOutOfBandRequiresWithException()
    {
      var c = new OutOfBand.C();
      c.TestRequiresWithException(1);
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(IndexOutOfRangeException))]
    public void NegativeOutOfBandRequiresWithException1()
    {
      var c = new OutOfBand.C();
      c.TestRequiresWithException(-1);
    }
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void NegativeOutOfBandRequiresWithException2()
    {
      var c = new OutOfBand.C();
      c.TestRequiresWithException(10);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void PositiveInheritedOOBInvariant()
    {
      var c = new DerivedOutOfBandC();
      c.ViolateBaseInvariant(true);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    [ExpectedException(typeof(TestRewriterMethods.InvariantException))]
    public void NegativeInheritedOOBInvariant()
    {
      var c = new DerivedOutOfBandC();
      c.ViolateBaseInvariant(false);
    }
    #endregion Tests
  }
}
