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

namespace ManualInheritanceChain
{
  public class Level1 : BaseOfChain
  {
    public override void Test(int x)
    {
      if (x <= 0) throw new ArgumentException("Level 1: x must be positive");
      Contract.EndContractBlock();
    }
  }


  [ContractClass(typeof(Level2Contract))]
  public abstract class Level2 : Level1
  {
    public override void Test(int x)
    {
      if (x <= 0) throw new ArgumentException("Level 2: x must be positive");
      Contract.EndContractBlock();
    }

    public abstract void Test(string s);

    [Pure]
    protected abstract bool IsNonEmpty(string s);

  }

  [ContractClassFor(typeof(Level2))]
  abstract class Level2Contract : Level2
  {
    public override void Test(string s)
    {
      if (s == null) throw new ArgumentNullException("s");
      Contract.EndContractBlock();
    }

    protected override bool IsNonEmpty(string s)
    {
      Contract.Requires(s != null);

      throw new NotImplementedException();
    }
  }
  public class Level3 : Level2
  {
    public override void Test(int x)
    {
      if (x <= 0) throw new ArgumentException("Level 3: x must be positive");
      Contract.EndContractBlock();
    }

    public override void Test(string s)
    {
      // test if we inherit abstract oob class contract
    }

    protected override bool IsNonEmpty(string s)
    {
      return !String.IsNullOrEmpty(s);
    }

    public bool TestIsNonEmpty(string s)
    {
      return IsNonEmpty(s);
    }
  }

  public class Level4 : Level3
  {
    public override void Test(int x)
    {
      if (x <= 0) throw new ArgumentException("Level 4: x must be positive");
      Contract.EndContractBlock();
    }

    public override void Test(string s)
    {
      if (s == null) throw new ArgumentNullException("Level 4: s");
      Contract.EndContractBlock();
    }
  }


  [TestClass]
  public class TestClass : DisableAssertUI
  {
    [TestMethod]
    public void TestBasePositive()
    {
      new BaseOfChain().Test(1);
    }

    [TestMethod]
    public void TestLevel1Positive()
    {
      new Level1().Test(1);
    }

    [TestMethod]
    public void TestLevel3Positive1()
    {
      new Level3().Test(1);
    }

    [TestMethod]
    public void TestLevel3Positive2()
    {
      new Level3().Test("foo");
    }

    [TestMethod]
    public void TestLevel3Positive3()
    {
      bool result = new Level3().TestIsNonEmpty("foo");
      Assert.IsTrue(result);
    }

    [TestMethod]
    public void TestLevel3Positive4()
    {
      bool result = new Level3().TestIsNonEmpty("");
      Assert.IsFalse(result);
    }

    [TestMethod]
    public void TestLevel4Positive1()
    {
      new Level4().Test(1);
    }

    [TestMethod]
    public void TestLevel4Positive2()
    {
      new Level4().Test("foo");
    }

    [TestMethod]
    public void TestBaseNegative()
    {
      try
      {
        new BaseOfChain().Test(0);
      }
      catch (ArgumentException a)
      {
        Assert.AreEqual("Precondition failed: x must be positive", a.Message);
        return;
      }
      throw new Exception();
    }

    [TestMethod]
    public void TestLevel1Negative()
    {
      try
      {
        new Level1().Test(0);
      }
      catch (ArgumentException a)
      {
        Assert.AreEqual("Precondition failed: x must be positive", a.Message);
        return;
      }
      throw new Exception();
    }

    [TestMethod]
    public void TestLevel3Negative1()
    {
      try
      {
        new Level3().Test(0);
      }
      catch (ArgumentException a)
      {
        Assert.AreEqual("Precondition failed: x must be positive", a.Message);
        return;
      }
      throw new Exception();
    }

    [TestMethod]
    public void TestLevel3Negative2()
    {
      try
      {
        new Level3().Test(null);
      }
      catch (ArgumentException a)
      {
        Assert.AreEqual("Value cannot be null.\r\nParameter name: s", a.Message);
        return;
      }
      throw new Exception();
    }

    [TestMethod]
    public void TestLevel3Negative3()
    {
      try {
        new Level3().TestIsNonEmpty(null);
      }
      catch (TestRewriterMethods.PreconditionException p) {
        Assert.AreEqual("s != null", p.Condition);
        return;
      }
      throw new Exception();
    }

    [TestMethod]
    public void TestLevel4Negative1()
    {
      try
      {
        new Level4().Test(0);
      }
      catch (ArgumentException a)
      {
        Assert.AreEqual("Precondition failed: x must be positive", a.Message);
        return;
      }
      throw new Exception();
    }

    [TestMethod]
    public void TestLevel4Negative2()
    {
      try
      {
        new Level4().Test(null);
      }
      catch (ArgumentException a)
      {
        Assert.IsTrue(a.Message.EndsWith(": s"));
        return;
      }
      throw new Exception();
    }

  }

}
