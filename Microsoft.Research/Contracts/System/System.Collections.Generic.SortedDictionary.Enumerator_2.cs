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

// File System.Collections.Generic.SortedDictionary.Enumerator_2.cs
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
    public partial struct Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>, IDisposable, System.Collections.IDictionaryEnumerator, System.Collections.IEnumerator
    {
      #region Methods and constructors
      public void Dispose ()
      {
      }

      public bool MoveNext ()
      {
        return default(bool);
      }

      internal void Reset ()
      {
      }

      void System.Collections.IEnumerator.Reset ()
      {
      }
      #endregion

      #region Properties and indexers
      public KeyValuePair<TKey, TValue> Current
      {
        get
        {
          return default(KeyValuePair<TKey, TValue>);
        }
      }

      System.Collections.DictionaryEntry System.Collections.IDictionaryEnumerator.Entry
      {
        get
        {
          return default(System.Collections.DictionaryEntry);
        }
      }

      Object System.Collections.IDictionaryEnumerator.Key
      {
        get
        {
          return default(Object);
        }
      }

      Object System.Collections.IDictionaryEnumerator.Value
      {
        get
        {
          return default(Object);
        }
      }

      Object System.Collections.IEnumerator.Current
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