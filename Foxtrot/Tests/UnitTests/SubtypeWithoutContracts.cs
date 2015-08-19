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
using System.Diagnostics;

using System.Diagnostics.Contracts;
using Microsoft.Contracts.Foxtrot;
using System.Reflection;
using Microsoft.CSharp;
using SubtypeWithoutContracts;
using Xunit;

namespace Tests {
  public class TestSubtypeWithoutContracts : DisableAssertUI {
    #region Tests
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    //[DeploymentItem("Foxtrot\\Tests\\AssemblyWithContracts\\bin\\Debug\\v3.5\\AssemblyWithContracts.Contracts.dll")]
    public void SubTypeWithoutContractsPositive()
    {
      new SubtypeWithoutContracts.Subtype1().M(3, true);
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void SubTypeWithoutContractsNegative1()
    {
      Assert.Throws<TestRewriterMethods.PreconditionException>(() => new SubtypeWithoutContracts.Subtype1().M(-3, true));
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void SubTypeWithoutContractsNegative2()
    {
      Assert.Throws<TestRewriterMethods.PostconditionException>(() => new SubtypeWithoutContracts.Subtype1().M(3, false));
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void SubTypeWithoutContractsNegative3()
    {
      Assert.Throws<TestRewriterMethods.PostconditionOnThrowException>(() => new SubtypeWithoutContracts.Subtype1().M(1, false));
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void SubTypeWithoutContractsTestConditionNegative1()
    {
      try
      {
        new SubtypeWithoutContracts.Subtype1().M(-3, true);
      }
      catch (TestRewriterMethods.PreconditionException p)
      {
        Assert.Equal("0 < x", p.Condition);
        Assert.Equal(null, p.User);
      }
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void SubTypeWithoutContractsTestConditionNegative2()
    {
      try
      {
        new SubtypeWithoutContracts.Subtype1().M(3, false);
      }
      catch (TestRewriterMethods.PostconditionException p)
      {
        Assert.Equal("Contract.Result<int>() == -x", p.Condition);
        Assert.Equal(null, p.User);
      }
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void SubTypeWithoutContractsTestConditionNegative3()
    {
      try
      {
        new SubtypeWithoutContracts.Subtype1().M(1, false);
      }
      catch (TestRewriterMethods.PostconditionOnThrowException p)
      {
        Assert.Equal("x == 0", p.Condition);
        Assert.Equal(null, p.User);
      }
    }



    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void SubTypeWithoutContractsUserStringsPositive()
    {
      new SubtypeWithoutContracts.Subtype1().WithUserStrings(3, true);
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void SubTypeWithoutContractsUserStringsNegative1()
    {
      try
      {
        new SubtypeWithoutContracts.Subtype1().WithUserStrings(-3, true);
      }
      catch (TestRewriterMethods.PreconditionException p)
      {
        Assert.Equal("0 < x", p.Condition);
        Assert.Equal(null, p.User); // user string must be masked here as it is not accessible
      }
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void SubTypeWithoutContractsUserStringsNegative2()
    {
      try
      {
        new SubtypeWithoutContracts.Subtype1().WithUserStrings(3, false);
      }
      catch (TestRewriterMethods.PostconditionException p)
      {
        Assert.Equal("Contract.Result<int>() == -x", p.Condition);
        Assert.Equal("result is negated x", p.User);
      }
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void SubTypeWithoutContractsUserStringsNegative3()
    {
      try
      {
        new SubtypeWithoutContracts.Subtype1().WithUserStrings(2, false);
      }
      catch (TestRewriterMethods.PostconditionOnThrowException p)
      {
        Assert.Equal("x == 0", p.Condition);
        Assert.Equal("Throws only if x == 0", p.User);
      }
    }

    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void SubTypeWithoutContractsUserStringsNegative4()
    {
      try
      {
        new SubtypeWithoutContracts.Subtype1().WithUserStrings(1, false);
      }
      catch (ArgumentException arg)
      {
        Assert.Equal("Precondition failed: 1 < x: x must be greater than 1", arg.Message);
      }
    }

    #endregion Tests
  }
}
