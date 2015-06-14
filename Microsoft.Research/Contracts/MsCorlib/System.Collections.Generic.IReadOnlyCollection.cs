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

#if !SILVERLIGHT_4_0 && !SILVERLIGHT_4_0_WP && !SILVERLIGHT_3_0 && !SILVERLIGHT_5_0 && !NETFRAMEWORK_3_5 && !NETFRAMEWORK_4_0

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;

namespace System.Collections.Generic
{
  #region IReadOnlyCollection contract binding
  [ContractClassFor(typeof(IReadOnlyCollection<>))]
  abstract class IReadOnlyCollectionContract<T> : IReadOnlyCollection<T>
  {
    #region IReadOnlyCollection<T> Members

    public int Count
    {
      get {
        Contract.Ensures(Contract.Result<int>() >= 0);
        throw new NotImplementedException(); 
      }
    }

    #endregion

    #region IEnumerable<T> Members

    public IEnumerator<T> GetEnumerator()
    {
      throw new NotImplementedException();
    }

    #endregion

    #region IEnumerable Members

    IEnumerator IEnumerable.GetEnumerator()
    {
      throw new NotImplementedException();
    }

    public object[] Model
    {
      get { throw new NotImplementedException(); }
    }

    #endregion
  }
  #endregion

  [ContractClass(typeof(IReadOnlyCollectionContract<>))]
  public interface IReadOnlyCollection<T> : IEnumerable<T>
  {
    int Count { get; }
  }
}

#endif