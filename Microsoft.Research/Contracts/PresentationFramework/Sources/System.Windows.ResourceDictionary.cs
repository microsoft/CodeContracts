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

// File System.Windows.ResourceDictionary.cs
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


namespace System.Windows
{
  public partial class ResourceDictionary : System.Collections.IDictionary, System.Collections.ICollection, System.Collections.IEnumerable, System.ComponentModel.ISupportInitialize, System.Windows.Markup.IUriContext, System.Windows.Markup.INameScope
  {
    #region Methods and constructors
    public void Add(Object key, Object value)
    {
    }

    public void BeginInit()
    {
    }

    public void Clear()
    {
    }

    public bool Contains(Object key)
    {
      return default(bool);
    }

    public void CopyTo(System.Collections.DictionaryEntry[] array, int arrayIndex)
    {
      Contract.Requires(arrayIndex >= 0);
    }

    public void EndInit()
    {
    }

    public Object FindName(string name)
    {
      return default(Object);
    }

    public System.Collections.IDictionaryEnumerator GetEnumerator()
    {
      return default(System.Collections.IDictionaryEnumerator);
    }

    public void RegisterName(string name, Object scopedElement)
    {
    }

    public void Remove(Object key)
    {
    }

    public ResourceDictionary()
    {
    }

    void System.Collections.ICollection.CopyTo(Array array, int arrayIndex)
    {
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
      return default(System.Collections.IEnumerator);
    }

    public void UnregisterName(string name)
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

    public DeferrableContent DeferrableContent
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.DeferrableContent>() == null);

        return default(DeferrableContent);
      }
      set
      {
        Contract.Requires(value != null);
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
      internal set
      {
      }
    }

    public Object this [Object key]
    {
      get
      {
        return default(Object);
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

    public System.Collections.ObjectModel.Collection<System.Windows.ResourceDictionary> MergedDictionaries
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Collections.ObjectModel.Collection<System.Windows.ResourceDictionary>>() != null);

        return default(System.Collections.ObjectModel.Collection<System.Windows.ResourceDictionary>);
      }
    }

    public Uri Source
    {
      get
      {
        return default(Uri);
      }
      set
      {
        Contract.Ensures(!string.IsNullOrEmpty(value.OriginalString));
        Contract.Ensures(0 <= value.OriginalString.Length);
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

    Uri System.Windows.Markup.IUriContext.BaseUri
    {
      get
      {
        return default(Uri);
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
