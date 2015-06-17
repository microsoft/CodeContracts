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
using System.Runtime.InteropServices;

namespace CodeUnderTest {

  [ContractClass(typeof(IIndexableContract<>))]
  public partial interface IIndexable<T> {
    int Count { get; }
    T this[int index] { get; }
  }

  [ContractClassFor(typeof(IIndexable<>))]
  abstract class IIndexableContract<T> : IIndexable<T> {
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

  public struct EmptyIndexable<T> : IIndexable<T> {
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

  public struct EmptyIntIndexable : IIndexable<int> {
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

  namespace WithExplicitLayout
  {
    [StructLayout(LayoutKind.Explicit, Size = HandleBitEncoder.kSizeOf)]
    public struct HandleBitEncoder
    {
      public const int kSizeOf = sizeof(ulong) + sizeof(int);

      [FieldOffset(0)]
      ulong m_64;
      [FieldOffset(0)]
      uint m_32;
      [FieldOffset(8)]
      int m_bit_index;

      public int Index { get { return m_bit_index; } }

      [Pure]
      public bool IsNonEmpty() {
        return this.m_64 != 0;
      }

      [ContractInvariantMethod]
      void ObjectInvariant()
      {
        Contract.Invariant(Index >= 0);
        Contract.Invariant(m_bit_index < 64);
        Contract.Invariant(IsNonEmpty() || !IsNonEmpty());

      }

      public HandleBitEncoder(ulong data, int index)
      {
        Contract.Requires(index >= 0);
        Contract.Requires(index < 64);
        this.m_32 = 0; // redundant but shuts up the compiler
        this.m_64 = data;
        this.m_bit_index = index;
      }

      public void DoStuff() { }
    }

    public struct HandleBitEncoderNoLayout
    {
      ulong m_64;
      int m_bit_index;

      public int Index { get { return m_bit_index; } }

      [Pure]
      public bool IsNonEmpty()
      {
        return this.m_64 != 0;
      }

      [ContractInvariantMethod]
      void ObjectInvariant()
      {
        Contract.Invariant(Index >= 0);
        Contract.Invariant(m_bit_index < 64);
        Contract.Invariant(IsNonEmpty() || !IsNonEmpty());
      }

      public HandleBitEncoderNoLayout(ulong data, int index)
      {
        Contract.Requires(index >= 0);
        Contract.Requires(index < 64);
        this.m_64 = data;
        this.m_bit_index = index;
      }

      public void DoStuff() { }
    }


  }
}