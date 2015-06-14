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

using System.Diagnostics.Contracts;
using System.Collections.Generic;
namespace System.Linq
{
  [ContractClass(typeof(CLookup<,>))]
  public interface ILookup<TKey, TElement> 
  {
    // Methods
    [Pure]
    bool Contains(TKey key);

    // Properties
    int Count { get; }

    IEnumerable<TElement> this[TKey key] { get; }
  }

  [ContractClassFor(typeof(ILookup<,>))]
  abstract class CLookup<TKey, TElement> : ILookup<TKey, TElement>
  {
    #region ILookup<TKey,TElement> Members


    [Pure]
    bool ILookup<TKey, TElement>.Contains(TKey key)
    {
      throw new NotImplementedException();
    }

    int ILookup<TKey, TElement>.Count
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0);
        return default(int);
      }
    }

    IEnumerable<TElement> ILookup<TKey, TElement>.this[TKey key]
    {
      get
      {
        Contract.Ensures(Contract.Result<IEnumerable<TElement>>() != null);
        return default(IEnumerable<TElement>);
      }
    }

    #endregion
  }
}