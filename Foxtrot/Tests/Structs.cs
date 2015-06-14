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
using Microsoft.Win32;
using System.Diagnostics.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
  [TestClass]
  public class StructTests : DisableAssertUI
  {
    #region Test Management
    [ClassInitialize]
    public static void ClassInitialize(TestContext context)
    {
    }

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

    [ContractClass(typeof(IIndexableContract<>))]
    public partial interface IIndexable<T>
    {
      int Count { get; }
      T this[int index] { get; }
    }

    [ContractClassFor(typeof(IIndexable<>))]
    public class IIndexableContract<T> : IIndexable<T>
    {
      #region IIndexable<T> Members

      public int Count
      {
        get
        {
          Contract.Ensures(Contract.Result<int>() >= 0);
          throw new NotImplementedException();
        }
      }

      public T this[int index]
      {
        get
        {
          Contract.Requires(index >= 0);
          Contract.Requires(index < this.Count);

          throw new NotImplementedException();
        }
      }

      #endregion
    }

    public struct EmptyIndexable<T> : IIndexable<T>
    {
      #region IIndexable<T> Members

      public int Count
      {
        get { return 0; }
      }

      public T this[int index]
      {
        get { throw new IndexOutOfRangeException(); }
      }

      #endregion

    }

    public struct EmptyIntIndexable : IIndexable<int>
    {
      #region IIndexable<T> Members

      public int Count
      {
        get { return 0; }
      }

      public int this[int index]
      {
        get { return -1; }
      }

      #endregion

    }

    [TestMethod]
    public void StructInheritingFromInterfacePositive1()
    {
      EmptyIndexable<int> empty = new EmptyIndexable<int>();
      int x = empty.Count;
      Assert.AreEqual(0, x);
    }

    [TestMethod]
    public void StructInheritingFromInterfaceNegative1()
    {
      EmptyIndexable<int> empty = new EmptyIndexable<int>();
      try
      {
        int x = empty[0];
        throw new Exception();
      }
      catch (TestRewriterMethods.PreconditionException p)
      {
        Assert.AreEqual("index < this.Count", p.Condition);
      }
    }

    [TestMethod]
    public void StructInheritingFromInterfacePositive2()
    {
      var empty = new EmptyIntIndexable();
      int x = empty.Count;
      Assert.AreEqual(0, x);
    }

    [TestMethod]
    public void StructInheritingFromInterfaceNegative2()
    {
      var empty = new EmptyIntIndexable();
      try
      {
        int x = empty[0];
        throw new Exception();
      }
      catch (TestRewriterMethods.PreconditionException p)
      {
        Assert.AreEqual("index < this.Count", p.Condition);
      }
    }


    #endregion
  }
}