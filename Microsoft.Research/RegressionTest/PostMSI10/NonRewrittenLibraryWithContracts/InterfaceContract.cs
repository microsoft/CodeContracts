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
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace PostMSI
{
  [ContractClass(typeof(ITestInheritedClosureContract))]
  public interface ITestInheritedClosure
  {
    byte[] Test(int x);

    [Pure]
    bool IsZero(int arg);

    ITestInheritedClosure[] Prop { get; }

    bool IsInterface { get; }
  }

  [ContractClassFor(typeof(ITestInheritedClosure))]
  abstract class ITestInheritedClosureContract : ITestInheritedClosure
  {

    #region ITestInheritedClosure Members

    byte[] ITestInheritedClosure.Test(int x)
    {
      ITestInheritedClosure @this = this;
      Contract.Ensures(Contract.Result<byte[]>() != null);
      Contract.Ensures(Contract.ForAll(Contract.Result<byte[]>(), n => @this.IsZero(n)));

      #region Dummy body
      throw new NotImplementedException();
      #endregion
    }

    #endregion

    #region ITestInheritedClosure Members


    bool ITestInheritedClosure.IsZero(int arg)
    {
      #region Dummy body
      throw new NotImplementedException();
      #endregion
    }

    ITestInheritedClosure[] ITestInheritedClosure.Prop
    {
      get
      {
        Contract.Ensures(Contract.Result<ITestInheritedClosure[]>() != null);
        Contract.Ensures(Contract.ForAll(HelperMethods.Helper(Contract.Result<ITestInheritedClosure[]>()), t => t != null && t.IsInterface));

        #region Dummy body
        throw new NotImplementedException();
        #endregion
      }
    }

    #endregion

    #region ITestInheritedClosure Members


    bool ITestInheritedClosure.IsInterface
    {
      get
      {
        #region Dummy body
        throw new NotImplementedException();
        #endregion
      }
    }

    #endregion
  }

  public class HelperMethods {
    [Pure]
    public static IEnumerable<T> Helper<T>(T[] array)
    {
      return array;
    }
  }
}
