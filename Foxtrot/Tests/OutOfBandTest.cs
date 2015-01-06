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
using System.Compiler;
using System.Reflection;
using Microsoft.CSharp;
using OutOfBand;

namespace Tests {
  [TestClass()]
  public class OutOfBandTest : DisableAssertUI{
    #region Test Management
#if false
    [ClassInitialize]
    public static void ClassInitialize (TestContext context) {
      Assert.IsTrue(System.IO.File.Exists("OutOfBand.dll"));
      Assert.IsTrue(System.IO.File.Exists("OutOfBand.Contracts.dll"));
      string[] args = new string[]{
        "/rewrite",
        "/rw:RewriterMethods.dll,TestRewriterMethods",
        "/c:OutOfBand.Contracts.dll",
        "OutOfBand.dll"
      };
      int x = Microsoft.Contracts.Foxtrot.Driver.Program.Main(args); // rewrites in place so tests use rewritten assembly
      Assert.AreEqual(0, x);
    }
#endif

    [ClassCleanup]
    public static void ClassCleanup () {
    }

    [TestInitialize]
    public void TestInitialize () {
    }

    [TestCleanup]
    public void TestCleanup () {
    }
    #endregion Test Management

    #region Tests

    public class DerivedOutOfBandC : C
    {
      public void ViolateBaseInvariant(bool behave)
      {
        base.c = behave;
      }
    }

    [TestMethod]
    public void PositiveOutOfBandInc ()
    {
      new OutOfBand.C().Inc(3);
    }
    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void NegativeOutOfBandInc () {
      new OutOfBand.C().Inc(27);
    }
    [TestMethod]
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
    [TestMethod]
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
    [TestMethod]
    public void PositiveOutOfBandInvariant () {
      new OutOfBand.C();
    }
    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.InvariantException))]
    public void NegativeOutOfBandInvariant () {
      new OutOfBand.C(false);
    }
    [TestMethod]
    public void PositiveOutOfBandRequires()
    {
      var c = new OutOfBand.C();
      var foo = new OutOfBand.Foo(1);
      c.FooMethodTakingTypeFromSameAssembly(foo);
    }
    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void NegativeOutOfBandRequires1()
    {
      var c = new OutOfBand.C();
      c.FooMethodTakingTypeFromSameAssembly(null);
    }
    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NegativeOutOfBandRequires2()
    {
      var c = new OutOfBand.C();
      var foo = new OutOfBand.Foo(0);
      c.FooMethodTakingTypeFromSameAssembly(foo);
    }
    [TestMethod]
    public void PositiveOutOfBandRequiresWithException()
    {
      var c = new OutOfBand.C();
      c.TestRequiresWithException(1);
    }
    [TestMethod]
    [ExpectedException(typeof(IndexOutOfRangeException))]
    public void NegativeOutOfBandRequiresWithException1()
    {
      var c = new OutOfBand.C();
      c.TestRequiresWithException(-1);
    }
    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void NegativeOutOfBandRequiresWithException2()
    {
      var c = new OutOfBand.C();
      c.TestRequiresWithException(10);
    }

    [TestMethod]
    public void PositiveInheritedOOBInvariant()
    {
      var c = new DerivedOutOfBandC();
      c.ViolateBaseInvariant(true);
    }

    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.InvariantException))]
    public void NegativeInheritedOOBInvariant()
    {
      var c = new DerivedOutOfBandC();
      c.ViolateBaseInvariant(false);
    }
    #endregion Tests
  }
}
