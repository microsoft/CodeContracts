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
using CodeUnderTest;
using Xunit;

namespace Tests {
  /// <summary>
  /// Summary description for GenericsTest
  /// </summary>
  public class GenericsTest : DisableAssertUI, IClassFixture<GenericsTest.GenericsTestFixture> {
    public GenericsTest(GenericsTestFixture testFixture)
    {
    }

    public class GenericsTestFixture
    {
      public GenericsTestFixture()
      {
        object[] attrs = typeof(RewrittenInheritanceBase).Assembly.GetCustomAttributes(true);
        bool found = false;
        foreach (var a in attrs)
        {
          Attribute attr = a as Attribute;
          if (attr == null) continue;
          Type ty = attr.TypeId as Type;
          if (ty == null) continue;
          if (ty.FullName == "System.Diagnostics.Contracts.RuntimeContractsAttribute")
          {
            found = true;
            break;
          }
        }
        Assert.True(found, "This assembly must have been rewritten before running these tests.  This is usually done in the post build script of the test project.");
      }
    }

    #region NonGenericExplicitImplementation
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NonGenericExplicitImplementationPositive()
    {
      IGenericInterface<string> j = new NonGenericExplicitImpl();
      j.M("abcd", true);
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NonGenericExplicitImplementationPreNegative()
    {
      IGenericInterface<string> j = new NonGenericExplicitImpl();
      Assert.Throws<TestRewriterMethods.PreconditionException>(() => j.M(null, true));
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NonGenericExplicitImplementationPostNegative()
    {
      IGenericInterface<string> j = new NonGenericExplicitImpl();
      Assert.Throws<TestRewriterMethods.PostconditionException>(() => j.M("abcd", false));
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NonGenericExplicitImplementationClosurePostPositive()
    {
      IGenericInterface<string> j = new NonGenericExplicitImpl();
      string[] x = new string[] { "abcd", "defg" };
      j.WithStaticClosure(x, true);
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NonGenericExplicitImplementationClosurePostNegative()
    {
      IGenericInterface<string> j = new NonGenericExplicitImpl();
      string[] x = new string[] { "abcd", "defg" };
      Assert.Throws<TestRewriterMethods.PostconditionException>(() => j.WithStaticClosure(x, false));
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NonGenericExplicitImplementationClosure2PostPositive()
    {
      IGenericInterface<string> j = new NonGenericExplicitImpl();
      string[] x = new string[] { "abcd", "defg" };
      j.WithClosureObject(x, true);
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NonGenericExplicitImplementationClosure2PostNegative()
    {
      IGenericInterface<string> j = new NonGenericExplicitImpl();
      string[] x = new string[] { "abcd", "defg" };
      Assert.Throws<TestRewriterMethods.PostconditionException>(() => j.WithClosureObject(x, false));
    }
    #endregion NonGenericExplicitImplementation
    #region NonGenericImplicitImplementation
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NonGenericImplicitImplementationPositive()
    {
      IGenericInterface<string> j = new NonGenericImplicitImpl();
      j.M("abcd", true);
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NonGenericImplicitImplementationPreNegative()
    {
      IGenericInterface<string> j = new NonGenericImplicitImpl();
      Assert.Throws<TestRewriterMethods.PreconditionException>(() => j.M(null, true));
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NonGenericImplicitImplementationPostNegative()
    {
      IGenericInterface<string> j = new NonGenericImplicitImpl();
      Assert.Throws<TestRewriterMethods.PostconditionException>(() => j.M("abcd", false));
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NonGenericImplicitImplementationClosurePostPositive()
    {
      IGenericInterface<string> j = new NonGenericImplicitImpl();
      string[] x = new string[] { "abcd", "defg" };
      j.WithStaticClosure(x, true);
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NonGenericImplicitImplementationClosurePostNegative()
    {
      IGenericInterface<string> j = new NonGenericImplicitImpl();
      string[] x = new string[] { "abcd", "defg" };
      Assert.Throws<TestRewriterMethods.PostconditionException>(() => j.WithStaticClosure(x, false));
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NonGenericImplicitImplementationClosure2PostPositive()
    {
      IGenericInterface<string> j = new NonGenericImplicitImpl();
      string[] x = new string[] { "abcd", "defg" };
      j.WithClosureObject(x, true);
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void NonGenericImplicitImplementationClosure2PostNegative()
    {
      IGenericInterface<string> j = new NonGenericImplicitImpl();
      string[] x = new string[] { "abcd", "defg" };
      Assert.Throws<TestRewriterMethods.PostconditionException>(() => j.WithClosureObject(x, false));
    }
    #endregion NonGenericImplicitImplementation
    #region GenericExplicitImplementation
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void GenericExplicitImplementationPositive()
    {
      IGenericInterface<string> j = new GenericExplicitImpl<string>();
      j.M("abcd", true);
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void GenericExplicitImplementationPreNegative()
    {
      IGenericInterface<string> j = new GenericExplicitImpl<string>();
      Assert.Throws<TestRewriterMethods.PreconditionException>(() => j.M(null, true));
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void GenericExplicitImplementationPostNegative()
    {
      IGenericInterface<string> j = new GenericExplicitImpl<string>();
      Assert.Throws<TestRewriterMethods.PostconditionException>(() => j.M("abcd", false));
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void GenericExplicitImplementationClosurePostPositive()
    {
      IGenericInterface<string> j = new GenericExplicitImpl<string>();
      string[] x = new string[] { "abcd", "defg" };
      j.WithStaticClosure(x, true);
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void GenericExplicitImplementationClosurePostNegative()
    {
      IGenericInterface<string> j = new GenericExplicitImpl<string>();
      string[] x = new string[] { "abcd", "defg" };
      Assert.Throws<TestRewriterMethods.PostconditionException>(() => j.WithStaticClosure(x, false));
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void GenericExplicitImplementationClosure2PostPositive()
    {
      IGenericInterface<string> j = new GenericExplicitImpl<string>();
      string[] x = new string[] { "abcd", "defg" };
      j.WithClosureObject(x, true);
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void GenericExplicitImplementationClosure2PostNegative()
    {
      IGenericInterface<string> j = new GenericExplicitImpl<string>();
      string[] x = new string[] { "abcd", "defg" };
      Assert.Throws<TestRewriterMethods.PostconditionException>(() => j.WithClosureObject(x, false));
    }
    #endregion GenericExplicitImplementation
    #region GenericImplicitImplementation
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void GenericImplicitImplementationPositive()
    {
      IGenericInterface<string> j = new GenericImplicitImpl<string>();
      j.M("abcd", true);
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void GenericImplicitImplementationPreNegative()
    {
      IGenericInterface<string> j = new GenericImplicitImpl<string>();
      Assert.Throws<TestRewriterMethods.PreconditionException>(() => j.M(null, true));
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void GenericImplicitImplementationPostNegative()
    {
      IGenericInterface<string> j = new GenericImplicitImpl<string>();
      Assert.Throws<TestRewriterMethods.PostconditionException>(() => j.M("abcd", false));
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void GenericImplicitImplementationClosurePostPositive()
    {
      IGenericInterface<string> j = new GenericImplicitImpl<string>();
      string[] x = new string[] { "abcd", "defg" };
      j.WithStaticClosure(x, true);
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void GenericImplicitImplementationClosurePostNegative()
    {
      IGenericInterface<string> j = new GenericImplicitImpl<string>();
      string[] x = new string[] { "abcd", "defg" };
      Assert.Throws<TestRewriterMethods.PostconditionException>(() => j.WithStaticClosure(x, false));
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void GenericImplicitImplementationClosure2PostPositive()
    {
      IGenericInterface<string> j = new GenericImplicitImpl<string>();
      string[] x = new string[] { "abcd", "defg" };
      j.WithClosureObject(x, true);
    }
    [Fact, Trait("Category", "Runtime"), Trait("Category", "V4.0"), Trait("Category", "CoreTest"), Trait("Category", "Short")]
    public void GenericImplicitImplementationClosure2PostNegative()
    {
      IGenericInterface<string> j = new GenericImplicitImpl<string>();
      string[] x = new string[] { "abcd", "defg" };
      Assert.Throws<TestRewriterMethods.PostconditionException>(() => j.WithClosureObject(x, false));
    }
    #endregion GenericImplicitImplementation
  }
}
