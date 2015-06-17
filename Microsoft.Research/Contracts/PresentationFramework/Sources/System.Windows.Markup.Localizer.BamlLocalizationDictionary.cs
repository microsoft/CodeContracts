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

// File System.Windows.Markup.Localizer.BamlLocalizationDictionary.cs
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


namespace System.Windows.Markup.Localizer
{
  sealed public partial class BamlLocalizationDictionary : System.Collections.IDictionary, System.Collections.ICollection, System.Collections.IEnumerable
  {
    #region Methods and constructors
    public void Add(BamlLocalizableResourceKey key, BamlLocalizableResource value)
    {
    }

    public BamlLocalizationDictionary()
    {
    }

    public void Clear()
    {
    }

    public bool Contains(BamlLocalizableResourceKey key)
    {
      return default(bool);
    }

    public void CopyTo(System.Collections.DictionaryEntry[] array, int arrayIndex)
    {
      Contract.Requires(array != null);
      Contract.Ensures((Contract.OldValue(arrayIndex) - array.Length) < 0);
      Contract.Ensures((this.Count - array.Length) <= 0);
      Contract.Ensures(0 <= this.Count);
    }

    public BamlLocalizationDictionaryEnumerator GetEnumerator()
    {
      Contract.Ensures(Contract.Result<System.Windows.Markup.Localizer.BamlLocalizationDictionaryEnumerator>() != null);

      return default(BamlLocalizationDictionaryEnumerator);
    }

    public void Remove(BamlLocalizableResourceKey key)
    {
    }

    void System.Collections.ICollection.CopyTo(Array array, int index)
    {
    }

    void System.Collections.IDictionary.Add(Object key, Object value)
    {
    }

    bool System.Collections.IDictionary.Contains(Object key)
    {
      return default(bool);
    }

    System.Collections.IDictionaryEnumerator System.Collections.IDictionary.GetEnumerator()
    {
      return default(System.Collections.IDictionaryEnumerator);
    }

    void System.Collections.IDictionary.Remove(Object key)
    {
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
      return default(System.Collections.IEnumerator);
    }
    #endregion

    #region Properties and indexers
    public int Count
    {
      get
      {
        Contract.Ensures(0 <= Contract.Result<int>());

        return default(int);
      }
    }

    public bool IsFixedSize
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsReadOnly
    {
      get
      {
        return default(bool);
      }
    }

    public BamlLocalizableResource this [BamlLocalizableResourceKey key]
    {
      get
      {
        return default(BamlLocalizableResource);
      }
      set
      {
      }
    }

    public System.Collections.ICollection Keys
    {
      get
      {
        return default(System.Collections.ICollection);
      }
    }

    public BamlLocalizableResourceKey RootElementKey
    {
      get
      {
        return default(BamlLocalizableResourceKey);
      }
    }

    int System.Collections.ICollection.Count
    {
      get
      {
        return default(int);
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

    Object System.Collections.IDictionary.this [Object key]
    {
      get
      {
        return default(Object);
      }
      set
      {
      }
    }

    public System.Collections.ICollection Values
    {
      get
      {
        return default(System.Collections.ICollection);
      }
    }
    #endregion
  }
}
