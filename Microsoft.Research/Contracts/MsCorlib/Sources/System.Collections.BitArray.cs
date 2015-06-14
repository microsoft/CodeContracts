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

// File System.Collections.BitArray.cs
// Automatically generated contract file.
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Diagnostics.Contracts;
using System;

// Disable the "this variable is not used" warning as every field would imply it.
#pragma warning disable 0414
// Disable the "this variable is never assigned to".
#pragma warning disable 0067
// Disable the "this event is never assigned to".
#pragma warning disable 0649
// Disable the "this variable is never used".
#pragma warning disable 0169
// Disable the "new keyword not required" warning.
#pragma warning disable 0109
// Disable the "extern without DllImport" warning.
#pragma warning disable 0626
// Disable the "could hide other member" warning, can happen on certain properties.
#pragma warning disable 0108


namespace System.Collections
{
  sealed public partial class BitArray : ICollection, IEnumerable, ICloneable
  {
    #region Methods and constructors
    public System.Collections.BitArray And(System.Collections.BitArray value)
    {
      Contract.Ensures((Contract.OldValue(this.Length) - value.Length) <= 0);
      Contract.Ensures((value.Length - Contract.OldValue(this.Length)) <= 0);
      Contract.Ensures(Contract.Result<System.Collections.BitArray>() != null);
      Contract.Ensures(Contract.Result<System.Collections.BitArray>() == this);

      return default(System.Collections.BitArray);
    }

    public BitArray(int[] values)
    {
    }

    public BitArray(System.Collections.BitArray bits)
    {
    }

    public BitArray(int length)
    {
    }

    public BitArray(int length, bool defaultValue)
    {
    }

    public BitArray(byte[] bytes)
    {
    }

    public BitArray(bool[] values)
    {
    }

    public Object Clone()
    {
      return default(Object);
    }

    public void CopyTo(Array array, int index)
    {
    }

    public bool Get(int index)
    {
      Contract.Ensures((index - this.Length) < 0);

      return default(bool);
    }

    public IEnumerator GetEnumerator()
    {
      return default(IEnumerator);
    }

    public System.Collections.BitArray Not()
    {
      Contract.Ensures(Contract.Result<System.Collections.BitArray>() != null);
      Contract.Ensures(Contract.Result<System.Collections.BitArray>() == this);

      return default(System.Collections.BitArray);
    }

    public System.Collections.BitArray Or(System.Collections.BitArray value)
    {
      Contract.Ensures((Contract.OldValue(this.Length) - value.Length) <= 0);
      Contract.Ensures((value.Length - Contract.OldValue(this.Length)) <= 0);
      Contract.Ensures(Contract.Result<System.Collections.BitArray>() != null);
      Contract.Ensures(Contract.Result<System.Collections.BitArray>() == this);

      return default(System.Collections.BitArray);
    }

    public void Set(int index, bool value)
    {
      Contract.Ensures((index - Contract.OldValue(this.Length)) < 0);
    }

    public void SetAll(bool value)
    {
    }

    public System.Collections.BitArray Xor(System.Collections.BitArray value)
    {
      Contract.Ensures((Contract.OldValue(this.Length) - value.Length) <= 0);
      Contract.Ensures((value.Length - Contract.OldValue(this.Length)) <= 0);
      Contract.Ensures(Contract.Result<System.Collections.BitArray>() != null);
      Contract.Ensures(Contract.Result<System.Collections.BitArray>() == this);

      return default(System.Collections.BitArray);
    }
    #endregion

    #region Properties and indexers
    public int Count
    {
      get
      {
        return default(int);
      }
    }

    public bool IsReadOnly
    {
      get
      {
        Contract.Ensures(Contract.Result<bool>() == false);

        return default(bool);
      }
    }

    public bool IsSynchronized
    {
      get
      {
        return default(bool);
      }
    }

    public bool this [int index]
    {
      get
      {
        Contract.Ensures((index - this.Length) < 0);
        Contract.Ensures(1 <= this.Length);

        return default(bool);
      }
      set
      {
        Contract.Ensures((index - Contract.OldValue(this.Length)) < 0);
      }
    }

    public int Length
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public Object SyncRoot
    {
      get
      {
        return default(Object);
      }
    }
    #endregion
  }
}
