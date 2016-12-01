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
using System.Diagnostics.Contracts;

namespace System.Collections
{

  public class BitArray : ICollection
  {
    public int Length
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() == this.Count);
        return default(int);
      }
      set
      {
        Contract.Requires(value >= 0);
      }
    }

    public bool this[int index]
    {
      get
      {
        Contract.Requires(index >= 0);
        Contract.Requires(index < this.Count);
        return default(bool);
      }
      set
      {
        Contract.Requires(index >= 0);
        Contract.Requires(index < this.Count);
      }
    }

    // ICollection.Count implementation
    extern public virtual int Count
    {
      get;
    }

    public BitArray Xor(BitArray value)
    {
      Contract.Requires(value != null);
      Contract.Ensures(Contract.Result<BitArray>() != null);

      return default(BitArray);
    }

    public BitArray Or(BitArray value)
    {
      Contract.Requires(value != null);
      Contract.Ensures(Contract.Result<BitArray>() != null);

      return default(BitArray);
    }

    public BitArray And(BitArray value)
    {
      Contract.Requires(value != null);
      Contract.Ensures(Contract.Result<BitArray>() != null);

      return default(BitArray);
    }

    public void Set(int index, bool value)
    {
      Contract.Requires(index >= 0);
      Contract.Requires(index < this.Count);

    }
    public bool Get(int index)
    {
      Contract.Requires(index >= 0);
      Contract.Requires(index < this.Count);

      return default(bool);
    }

    public BitArray(BitArray bits)
    {
      Contract.Requires(bits != null);
      Contract.Ensures(this.Count == bits.Count);
    }

    public BitArray(Int32[] values)
    {
      Contract.Requires(values != null);
      Contract.Ensures(this.Count == values.Length * 32);
    }

    public BitArray(Boolean[] values)
    {
      Contract.Requires(values != null);
      Contract.Ensures(this.Count == values.Length);
    }

    public BitArray(Byte[] bytes)
    {
      Contract.Requires(bytes != null);
      Contract.Ensures(this.Count == bytes.Length * 8);
    }

    public BitArray(int length, bool defaultValue)
    {
      Contract.Requires(length >= 0);
      Contract.Ensures(this.Count == length);
    }

    public BitArray(int length)
    {
      Contract.Requires(length >= 0);
      Contract.Ensures(this.Count == length);
    }

    #region ICollection Members


    public bool IsSynchronized
    {
      get { throw new NotImplementedException(); }
    }

    public object SyncRoot
    {
      get { throw new NotImplementedException(); }
    }

    public void CopyTo(Array array, int index)
    {
      throw new NotImplementedException();
    }

    #endregion
  }
}
