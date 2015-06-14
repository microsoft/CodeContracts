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

// File System.Collections.DictionaryBase.cs
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
  abstract public partial class DictionaryBase : IDictionary, ICollection, IEnumerable
  {
    #region Methods and constructors
    public void Clear()
    {
    }

    public void CopyTo(Array array, int index)
    {
    }

    protected DictionaryBase()
    {
    }

    public IDictionaryEnumerator GetEnumerator()
    {
      return default(IDictionaryEnumerator);
    }

    protected virtual new void OnClear()
    {
    }

    protected virtual new void OnClearComplete()
    {
    }

    protected virtual new Object OnGet(Object key, Object currentValue)
    {
      return default(Object);
    }

    protected virtual new void OnInsert(Object key, Object value)
    {
    }

    protected virtual new void OnInsertComplete(Object key, Object value)
    {
    }

    protected virtual new void OnRemove(Object key, Object value)
    {
    }

    protected virtual new void OnRemoveComplete(Object key, Object value)
    {
    }

    protected virtual new void OnSet(Object key, Object oldValue, Object newValue)
    {
    }

    protected virtual new void OnSetComplete(Object key, Object oldValue, Object newValue)
    {
    }

    protected virtual new void OnValidate(Object key, Object value)
    {
    }

    void System.Collections.IDictionary.Add(Object key, Object value)
    {
    }

    bool System.Collections.IDictionary.Contains(Object key)
    {
      return default(bool);
    }

    void System.Collections.IDictionary.Remove(Object key)
    {
    }

    IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
      return default(IEnumerator);
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

    protected IDictionary Dictionary
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Collections.IDictionary>() != null);
        Contract.Ensures(Contract.Result<System.Collections.IDictionary>() == this);

        return default(IDictionary);
      }
    }

    protected Hashtable InnerHashtable
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Collections.Hashtable>() != null);

        return default(Hashtable);
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

    bool System.Collections.IDictionary.IsFixedSize
    {
      get
      {
        return default(bool);
      }
    }

    bool System.Collections.IDictionary.IsReadOnly
    {
      get
      {
        return default(bool);
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

    ICollection System.Collections.IDictionary.Keys
    {
      get
      {
        return default(ICollection);
      }
    }

    ICollection System.Collections.IDictionary.Values
    {
      get
      {
        return default(ICollection);
      }
    }
    #endregion
  }
}
