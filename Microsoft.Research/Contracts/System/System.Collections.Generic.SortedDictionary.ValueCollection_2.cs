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

#if !SILVERLIGHT

// File System.Collections.Generic.SortedDictionary.ValueCollection_2.cs
// Automatically generated contract file.
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Diagnostics.Contracts;
using System;

// Disable the "this variable is not used" warning as every field would imply it.
#pragma warning disable 0414
// Disable the "this variable is never assigned to".
#pragma warning disable 0649
// Disable the "this variable is never used".
#pragma warning disable 0169
// Disable the "new keyword not required" warning.
#pragma warning disable 0109
// Disable the "extern without DllImport" warning.
#pragma warning disable 0626
// Disable the "could hide other member" warning, can happen on certain properties.
#pragma warning disable 0108


namespace System.Collections.Generic
{
  public partial class SortedDictionary<TKey, TValue>
  {
    sealed public partial class ValueCollection : ICollection<TValue>, IEnumerable<TValue>, System.Collections.ICollection, System.Collections.IEnumerable
    {
      #region Methods and constructors
      public void CopyTo (TValue[] array, int index)
      {
      }

      public ValueCollection.Enumerator GetEnumerator ()
      {
        return default(ValueCollection.Enumerator);
      }

      void System.Collections.Generic.ICollection<TValue>.Add (TValue item)
      {
      }

      void System.Collections.Generic.ICollection<TValue>.Clear ()
      {
      }

      bool System.Collections.Generic.ICollection<TValue>.Contains (TValue item)
      {
        return default(bool);
      }

      bool System.Collections.Generic.ICollection<TValue>.Remove (TValue item)
      {
        return default(bool);
      }

      IEnumerator<TValue> System.Collections.Generic.IEnumerable<TValue>.GetEnumerator ()
      {
        return default(IEnumerator<TValue>);
      }

      void System.Collections.ICollection.CopyTo (Array array, int index)
      {
      }

      System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator ()
      {
        return default(System.Collections.IEnumerator);
      }

      public ValueCollection (System.Collections.Generic.SortedDictionary<TKey, TValue> dictionary)
      {
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

      bool System.Collections.Generic.ICollection<TValue>.IsReadOnly
      {
        get
        {
          return default(bool);
        }
      }

      bool System.Collections.ICollection.IsSynchronized
      {
        get
        {
          return default(bool);
        }
      }

      Object System.Collections.ICollection.SyncRoot
      {
        get
        {
          return default(Object);
        }
      }
      #endregion
    }
  }
}

#endif