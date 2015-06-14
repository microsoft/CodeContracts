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
using System.Text;
using System.Diagnostics.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tests;
using CodeUnderTest;

namespace ManualInheritanceChain
{
  [TestClass]
  public class TestClass : DisableAssertUI
  {
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void TestBasePositive()
    {
      new BaseOfChain().Test(1);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void TestLevel1Positive()
    {
      new Level1().Test(1);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void TestLevel3Positive1()
    {
      new Level3().Test(1);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void TestLevel3Positive2()
    {
      new Level3().Test("foo");
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void TestLevel3Positive3()
    {
      bool result = new Level3().TestIsNonEmpty("foo");
      Assert.IsTrue(result);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void TestLevel3Positive4()
    {
      bool result = new Level3().TestIsNonEmpty("");
      Assert.IsFalse(result);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void TestLevel4Positive1()
    {
      new Level4().Test(1);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void TestLevel4Positive2()
    {
      new Level4().Test("foo");
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void TestBaseNegative()
    {
      try
      {
        new BaseOfChain().Test(0);
      }
      catch (ArgumentException a)
      {
        Assert.AreEqual("Precondition failed: x > 0: x must be positive\r\nParameter name: x must be positive", a.Message);
        return;
      }
      throw new Exception();
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void TestLevel1Negative()
    {
      try
      {
        new Level1().CallTest(0);
      }
      catch (ArgumentException a)
      {
        Assert.AreEqual("Precondition failed: x > 0: x must be positive\r\nParameter name: x must be positive", a.Message);
        return;
      }
      throw new Exception();
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void TestLevel3Negative1()
    {
      try
      {
        new Level3().CallTest(0);
      }
      catch (ArgumentException a)
      {
        Assert.AreEqual("Precondition failed: x > 0: x must be positive\r\nParameter name: x must be positive", a.Message);
        return;
      }
      throw new Exception();
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void TestLevel3Negative2()
    {
      try
      {
        new Level3().Test(null);
      }
      catch (ArgumentException a)
      {
        Assert.AreEqual("Precondition failed: s != null", a.Message);
        return;
      }
      throw new Exception();
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void TestLevel3Negative3()
    {
      try
      {
        new Level3().TestIsNonEmpty(null);
      }
      catch (TestRewriterMethods.PreconditionException p)
      {
        Assert.AreEqual("s != null", p.Condition);
        return;
      }
      throw new Exception();
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void TestLevel4Negative1()
    {
      try
      {
        new Level4().CallTest(0);
      }
      catch (ArgumentException a)
      {
        Assert.AreEqual("Precondition failed: x > 0: x must be positive\r\nParameter name: x must be positive", a.Message);
        return;
      }
      throw new Exception();
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void TestLevel4Negative2()
    {
      try
      {
        new Level4().CallTest(null);
      }
      catch (ArgumentException a)
      {
        Assert.AreEqual("Precondition failed: s != null", a.Message);
        return;
      }
      throw new Exception();
    }

  }
}


namespace ManualInheritanceChainWithHelpers
{
  [TestClass]
  public class TestClass : DisableAssertUI
  {
    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void TestBasePositive()
    {
      new ManualInheritanceChain.BaseOfChain().Test(1);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void TestLevel1Positive1()
    {
      new Level1().Test(1);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void TestLevel1Positive2()
    {
      new Level1().TestDouble(1);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void TestLevel3Positive1()
    {
      new Level3().Test(1);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void TestLevel3Positive2()
    {
      new Level3().Test("foo");
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void TestLevel3Positive3()
    {
      bool result = new Level3().TestIsNonEmpty("foo");
      Assert.IsTrue(result);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void TestLevel3Positive4()
    {
      bool result = new Level3().TestIsNonEmpty("");
      Assert.IsFalse(result);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void TestLevel3Positive5()
    {
      new Level3().TestDouble(0);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void TestLevel4Positive1()
    {
      new Level4().Test(1);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void TestLevel4Positive2()
    {
      new Level4().Test("foo");
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void TestLevel4Positive3()
    {
      new Level4().TestDouble(0);
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void TestBaseNegative()
    {
      try
      {
        new ManualInheritanceChain.BaseOfChain().Test(0);
      }
      catch (ArgumentException a)
      {
        Assert.AreEqual("Precondition failed: x > 0: x must be positive\r\nParameter name: x must be positive", a.Message);
        return;
      }
      throw new Exception();
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void TestLevel1Negative1()
    {
      try
      {
        new Level1().Test(0);
      }
      catch (ArgumentException a)
      {
        Assert.AreEqual("x must be positive", a.ParamName);
        return;
      }
      throw new Exception();
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void TestLevel1Negative2()
    {
      try
      {
        new Level1().TestDouble(double.NaN);
      }
      catch (ArgumentOutOfRangeException a)
      {
        Assert.AreEqual("d", a.ParamName);
        return;
      }
      throw new Exception();
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void TestLevel3Negative1()
    {
      try
      {
        new Level3().Test(0);
      }
      catch (ArgumentException a)
      {
        Assert.AreEqual("x must be positive", a.ParamName);
        return;
      }
      throw new Exception();
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void TestLevel3Negative2()
    {
      try
      {
        new Level3().Test(null);
      }
      catch (ArgumentException a)
      {
        Assert.AreEqual("s", a.ParamName);
        return;
      }
      throw new Exception();
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void TestLevel3Negative3()
    {
      try
      {
        new Level3().TestIsNonEmpty(null);
      }
      catch (TestRewriterMethods.PreconditionException p)
      {
        Assert.AreEqual("s != null", p.Condition);
        return;
      }
      throw new Exception();
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void TestLevel3Negative4()
    {
      try
      {
        new Level3().TestDouble(Double.NaN);
      }
      catch (ArgumentOutOfRangeException p)
      {
        Assert.AreEqual("d", p.ParamName);
        return;
      }
      throw new Exception();
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void TestLevel4Negative1()
    {
      try
      {
        new Level4().Test(0);
      }
      catch (ArgumentException a)
      {
        Assert.AreEqual("x must be positive", a.ParamName);
        return;
      }
      throw new Exception();
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void TestLevel4Negative2()
    {
      try
      {
        new Level4().Test(null);
      }
      catch (ArgumentNullException a)
      {
        Assert.AreEqual("s", a.ParamName);
        return;
      }
      throw new Exception();
    }

    [TestMethod, TestCategory("Runtime"), TestCategory("V4.0"), TestCategory("CoreTest"), TestCategory("Short")]
    public void TestLevel4Negative3()
    {
      try
      {
        new Level4().TestDouble(Double.NaN);
      }
      catch (ArgumentOutOfRangeException p)
      {
        Assert.AreEqual("d", p.ParamName);
        return;
      }
      throw new Exception();
    }

  }
}

