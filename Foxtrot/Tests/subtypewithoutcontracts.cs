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

namespace Tests {
  [TestClass()]
  public class TestSubtypeWithoutContracts : DisableAssertUI {
    #region Test Management
    [ClassInitialize]
    public static void ClassInitialize(TestContext context) {
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

    #region Tests
    [TestMethod]
    public void SubTypeWithoutContractsPositive() {
      new SubtypeWithoutContracts.Subtype1().M(3, true);
    }
    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void SubTypeWithoutContractsNegative1() {
      new SubtypeWithoutContracts.Subtype1().M(-3, true);
    }
    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void SubTypeWithoutContractsNegative2() {
      new SubtypeWithoutContracts.Subtype1().M(3, false);
    }
    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionOnThrowException))]
    public void SubTypeWithoutContractsNegative3()
    {
      new SubtypeWithoutContracts.Subtype1().M(1, false);
    }
    [TestMethod]
    public void SubTypeWithoutContractsTestConditionNegative1()
    {
      try
      {
        new SubtypeWithoutContracts.Subtype1().M(-3, true);
      }
      catch (TestRewriterMethods.PreconditionException p)
      {
        Assert.AreEqual("0 < x", p.Condition);
        Assert.AreEqual(null, p.User);
      }
    }
    [TestMethod]
    public void SubTypeWithoutContractsTestConditionNegative2()
    {
      try
      {
        new SubtypeWithoutContracts.Subtype1().M(3, false);
      }
      catch (TestRewriterMethods.PostconditionException p)
      {
        Assert.AreEqual("Contract.Result<int>() == -x", p.Condition);
        Assert.AreEqual(null, p.User);
      }
    }

    [TestMethod]
    public void SubTypeWithoutContractsTestConditionNegative3()
    {
      try
      {
        new SubtypeWithoutContracts.Subtype1().M(1, false);
      }
      catch (TestRewriterMethods.PostconditionOnThrowException p)
      {
        Assert.AreEqual("x == 0", p.Condition);
        Assert.AreEqual(null, p.User);
      }
    }



    [TestMethod]
    public void SubTypeWithoutContractsUserStringsPositive()
    {
      new SubtypeWithoutContracts.Subtype1().WithUserStrings(3, true);
    }
    [TestMethod]
    public void SubTypeWithoutContractsUserStringsNegative1()
    {
      try
      {
        new SubtypeWithoutContracts.Subtype1().WithUserStrings(-3, true);
      }
      catch (TestRewriterMethods.PreconditionException p)
      {
        Assert.AreEqual("0 < x", p.Condition);
        Assert.AreEqual("x must be positive", p.User);
      }
    }
    [TestMethod]
    public void SubTypeWithoutContractsUserStringsNegative2()
    {
      try
      {
        new SubtypeWithoutContracts.Subtype1().WithUserStrings(3, false);
      }
      catch (TestRewriterMethods.PostconditionException p)
      {
        Assert.AreEqual("Contract.Result<int>() == -x", p.Condition);
        Assert.AreEqual("result is negated x", p.User);
      }
    }

    [TestMethod]
    public void SubTypeWithoutContractsUserStringsNegative3()
    {
      try
      {
        new SubtypeWithoutContracts.Subtype1().WithUserStrings(2, false);
      }
      catch (TestRewriterMethods.PostconditionOnThrowException p)
      {
        Assert.AreEqual("x == 0", p.Condition);
        Assert.AreEqual("Throws only if x == 0", p.User);
      }
    }

    [TestMethod]
    public void SubTypeWithoutContractsUserStringsNegative4()
    {
      try
      {
        new SubtypeWithoutContracts.Subtype1().WithUserStrings(1, false);
      }
      catch (ArgumentException arg)
      {
        Assert.AreEqual("Precondition failed: 1 < x: x must be greater than 1", arg.Message);
      }
    }

    #endregion Tests
  }
}
