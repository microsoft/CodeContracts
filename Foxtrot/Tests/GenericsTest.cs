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

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.Contracts;

namespace Tests {
  #region Contracts
  [ContractClass(typeof(ContractClassForAnInterface<>))]
  public interface IGenericInterface<T> where T : class {
    T M(T x, bool behave);

    void WithStaticClosure(T[] x, bool behave);

    void WithClosureObject(T[] x, bool behave);
  }

  [ContractClassFor(typeof(IGenericInterface<>))]
  class ContractClassForAnInterface<W> : IGenericInterface<W> where W : class
  {
    W IGenericInterface<W>.M(W y, bool b)
    {
      Contract.Requires(y != null);
      Contract.Ensures(Contract.Result<W>() != null && Contract.Result<W>().Equals(y));
      return default(W);
    }

    void IGenericInterface<W>.WithStaticClosure(W[] x, bool behave)
    {
      Contract.Ensures(Contract.ForAll(x, e => e != null));
    }

    void IGenericInterface<W>.WithClosureObject(W[] x, bool behave)
    {
      Contract.Ensures(Contract.ForAll(0, x.Length, i => x[i] != null));
    }

  }


  class NonGenericExplicitImpl : IGenericInterface<string> {
    string IGenericInterface<string>.M(string z, bool behave) {
      return behave ? z : String.Concat(z, z);
    }

    void IGenericInterface<string>.WithStaticClosure(string[] x, bool behave)
    {
      if (behave) return;
      x[0] = null;
    }

    void IGenericInterface<string>.WithClosureObject(string[] x, bool behave)
    {
      if (behave) return;
      x[0] = null;
    }
  }
  class NonGenericImplicitImpl : IGenericInterface<string> {
    public string M(string z, bool behave) {
      return behave ? z : String.Concat(z, z);
    }

    public void WithStaticClosure(string[] x, bool behave)
    {
      if (behave) return;
      x[0] = null;
    }

    public void WithClosureObject(string[] x, bool behave)
    {
      if (behave) return;
      x[0] = null;
    }
  }

  class GenericExplicitImpl<U> : IGenericInterface<U> where U : class {
    U IGenericInterface<U>.M(U z, bool behave) {
      return behave ? z : null;
    }

    void IGenericInterface<U>.WithStaticClosure(U[] x, bool behave)
    {
      if (behave) return;
      x[0] = null;
    }

    void IGenericInterface<U>.WithClosureObject(U[] x, bool behave)
    {
      if (behave) return;
      x[0] = null;
    }
  }

  class GenericImplicitImpl<U> : IGenericInterface<U> where U : class {
    public U M(U z, bool behave) {
      return behave ? z : null;
    }

    public void WithStaticClosure(U[] x, bool behave)
    {
      if (behave) return;
      x[0] = null;
    }

    public void WithClosureObject(U[] x, bool behave)
    {
      if (behave) return;
      x[0] = null;
    }
  }

  #endregion Contracts
  #region Tests
  /// <summary>
  /// Summary description for GenericsTest
  /// </summary>
  [TestClass]
  public class GenericsTest {
    public GenericsTest() {
      //
      // TODO: Add constructor logic here
      //
    }

    private TestContext testContextInstance;

    /// <summary>
    ///Gets or sets the test context which provides
    ///information about and functionality for the current test run.
    ///</summary>
    public TestContext TestContext {
      get {
        return testContextInstance;
      }
      set {
        testContextInstance = value;
      }
    }

    #region Additional test attributes
    //
    // You can use the following additional attributes as you write your tests:
    //
    // Use ClassInitialize to run code before running the first test in the class
    [ClassInitialize()]
    public static void MyClassInitialize(TestContext testContext) {
      object[] attrs = typeof(RewrittenInheritanceBase).Assembly.GetCustomAttributes(true);
      bool found = false;
      foreach (var a in attrs) {
        Attribute attr = a as Attribute;
        if (attr == null) continue;
        Type ty = attr.TypeId as Type;
        if (ty == null) continue;
        if (ty.FullName == "System.Diagnostics.Contracts.RuntimeContractsAttribute") {
          found = true;
          break;
        }
      }
      Assert.IsTrue(found, "This assembly must have been rewritten before running these tests.  This is usually done in the post build script of the test project.");
    }
    //
    // Use ClassCleanup to run code after all tests in a class have run
    // [ClassCleanup()]
    // public static void MyClassCleanup() { }
    //
    // Use TestInitialize to run code before running each test 
    // [TestInitialize()]
    // public void MyTestInitialize() { }
    //
    // Use TestCleanup to run code after each test has run
    // [TestCleanup()]
    // public void MyTestCleanup() { }
    //
    #endregion

    #region NonGenericExplicitImplementation
    [TestMethod]
    public void NonGenericExplicitImplementationPositive() {
      IGenericInterface<string> j = new NonGenericExplicitImpl();
      j.M("abcd", true);
    }
    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void NonGenericExplicitImplementationPreNegative() {
      IGenericInterface<string> j = new NonGenericExplicitImpl();
      j.M(null, true);
    }
    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NonGenericExplicitImplementationPostNegative() {
      IGenericInterface<string> j = new NonGenericExplicitImpl();
      j.M("abcd", false);
    }
    [TestMethod]
    public void NonGenericExplicitImplementationClosurePostPositive()
    {
      IGenericInterface<string> j = new NonGenericExplicitImpl();
      string[] x = new string[] { "abcd", "defg" };
      j.WithStaticClosure(x, true);
    }
    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NonGenericExplicitImplementationClosurePostNegative()
    {
      IGenericInterface<string> j = new NonGenericExplicitImpl();
      string[] x = new string[] { "abcd", "defg" };
      j.WithStaticClosure(x, false);
    }
    [TestMethod]
    public void NonGenericExplicitImplementationClosure2PostPositive()
    {
      IGenericInterface<string> j = new NonGenericExplicitImpl();
      string[] x = new string[] { "abcd", "defg" };
      j.WithClosureObject(x, true);
    }
    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NonGenericExplicitImplementationClosure2PostNegative()
    {
      IGenericInterface<string> j = new NonGenericExplicitImpl();
      string[] x = new string[] { "abcd", "defg" };
      j.WithClosureObject(x, false);
    }
    #endregion NonGenericExplicitImplementation
    #region NonGenericImplicitImplementation
    [TestMethod]
    public void NonGenericImplicitImplementationPositive() {
      IGenericInterface<string> j = new NonGenericImplicitImpl();
      j.M("abcd", true);
    }
    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void NonGenericImplicitImplementationPreNegative() {
      IGenericInterface<string> j = new NonGenericImplicitImpl();
      j.M(null, true);
    }
    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NonGenericImplicitImplementationPostNegative() {
      IGenericInterface<string> j = new NonGenericImplicitImpl();
      j.M("abcd", false);
    }
    [TestMethod]
    public void NonGenericImplicitImplementationClosurePostPositive()
    {
      IGenericInterface<string> j = new NonGenericImplicitImpl();
      string[] x = new string[] { "abcd", "defg" };
      j.WithStaticClosure(x, true);
    }
    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NonGenericImplicitImplementationClosurePostNegative()
    {
      IGenericInterface<string> j = new NonGenericImplicitImpl();
      string[] x = new string[] { "abcd", "defg" };
      j.WithStaticClosure(x, false);
    }
    [TestMethod]
    public void NonGenericImplicitImplementationClosure2PostPositive()
    {
      IGenericInterface<string> j = new NonGenericImplicitImpl();
      string[] x = new string[] { "abcd", "defg" };
      j.WithClosureObject(x, true);
    }
    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void NonGenericImplicitImplementationClosure2PostNegative()
    {
      IGenericInterface<string> j = new NonGenericImplicitImpl();
      string[] x = new string[] { "abcd", "defg" };
      j.WithClosureObject(x, false);
    }
    #endregion NonGenericImplicitImplementation
    #region GenericExplicitImplementation
    [TestMethod]
    public void GenericExplicitImplementationPositive() {
      IGenericInterface<string> j = new GenericExplicitImpl<string>();
      j.M("abcd", true);
    }
    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void GenericExplicitImplementationPreNegative() {
      IGenericInterface<string> j = new GenericExplicitImpl<string>();
      j.M(null, true);
    }
    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void GenericExplicitImplementationPostNegative() {
      IGenericInterface<string> j = new GenericExplicitImpl<string>();
      j.M("abcd", false);
    }
    [TestMethod]
    public void GenericExplicitImplementationClosurePostPositive()
    {
      IGenericInterface<string> j = new GenericExplicitImpl<string>();
      string[] x = new string[] { "abcd", "defg" };
      j.WithStaticClosure(x, true);
    }
    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void GenericExplicitImplementationClosurePostNegative()
    {
      IGenericInterface<string> j = new GenericExplicitImpl<string>();
      string[] x = new string[] { "abcd", "defg" };
      j.WithStaticClosure(x, false);
    }
    [TestMethod]
    public void GenericExplicitImplementationClosure2PostPositive()
    {
      IGenericInterface<string> j = new GenericExplicitImpl<string>();
      string[] x = new string[] { "abcd", "defg" };
      j.WithClosureObject(x, true);
    }
    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void GenericExplicitImplementationClosure2PostNegative()
    {
      IGenericInterface<string> j = new GenericExplicitImpl<string>();
      string[] x = new string[] { "abcd", "defg" };
      j.WithClosureObject(x, false);
    }
    #endregion GenericExplicitImplementation
    #region GenericImplicitImplementation
    [TestMethod]
    public void GenericImplicitImplementationPositive() {
      IGenericInterface<string> j = new GenericImplicitImpl<string>();
      j.M("abcd", true);
    }
    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PreconditionException))]
    public void GenericImplicitImplementationPreNegative() {
      IGenericInterface<string> j = new GenericImplicitImpl<string>();
      j.M(null, true);
    }
    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void GenericImplicitImplementationPostNegative() {
      IGenericInterface<string> j = new GenericImplicitImpl<string>();
      j.M("abcd", false);
    }
    [TestMethod]
    public void GenericImplicitImplementationClosurePostPositive()
    {
      IGenericInterface<string> j = new GenericImplicitImpl<string>();
      string[] x = new string[] { "abcd", "defg" };
      j.WithStaticClosure(x, true);
    }
    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void GenericImplicitImplementationClosurePostNegative()
    {
      IGenericInterface<string> j = new GenericImplicitImpl<string>();
      string[] x = new string[] { "abcd", "defg" };
      j.WithStaticClosure(x, false);
    }
    [TestMethod]
    public void GenericImplicitImplementationClosure2PostPositive()
    {
      IGenericInterface<string> j = new GenericImplicitImpl<string>();
      string[] x = new string[] { "abcd", "defg" };
      j.WithClosureObject(x, true);
    }
    [TestMethod]
    [ExpectedException(typeof(TestRewriterMethods.PostconditionException))]
    public void GenericImplicitImplementationClosure2PostNegative()
    {
      IGenericInterface<string> j = new GenericImplicitImpl<string>();
      string[] x = new string[] { "abcd", "defg" };
      j.WithClosureObject(x, false);
    }
    #endregion GenericImplicitImplementation
  }
  #endregion Tests
}
