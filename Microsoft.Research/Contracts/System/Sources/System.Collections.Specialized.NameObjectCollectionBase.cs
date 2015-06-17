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

// File System.Collections.Specialized.NameObjectCollectionBase.cs
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


namespace System.Collections.Specialized
{
  abstract public partial class NameObjectCollectionBase : System.Collections.ICollection, System.Collections.IEnumerable, System.Runtime.Serialization.ISerializable, System.Runtime.Serialization.IDeserializationCallback
  {
    #region Methods and constructors
    protected void BaseAdd(string name, Object value)
    {
    }

    protected void BaseClear()
    {
    }

    protected Object BaseGet(string name)
    {
      return default(Object);
    }

    protected Object BaseGet(int index)
    {
      return default(Object);
    }

    protected string[] BaseGetAllKeys()
    {
      Contract.Ensures(Contract.Result<string[]>() != null);

      return default(string[]);
    }

    protected Object[] BaseGetAllValues()
    {
      Contract.Ensures(Contract.Result<System.Object[]>() != null);

      return default(Object[]);
    }

    protected Object[] BaseGetAllValues(Type type)
    {
      return default(Object[]);
    }

    protected string BaseGetKey(int index)
    {
      return default(string);
    }

    protected bool BaseHasKeys()
    {
      return default(bool);
    }

    protected void BaseRemove(string name)
    {
    }

    protected void BaseRemoveAt(int index)
    {
    }

    protected void BaseSet(int index, Object value)
    {
    }

    protected void BaseSet(string name, Object value)
    {
    }

    public virtual new System.Collections.IEnumerator GetEnumerator()
    {
      return default(System.Collections.IEnumerator);
    }

    public virtual new void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
    {
    }

    protected NameObjectCollectionBase(int capacity, System.Collections.IEqualityComparer equalityComparer)
    {
    }

    protected NameObjectCollectionBase(System.Collections.IHashCodeProvider hashProvider, System.Collections.IComparer comparer)
    {
    }

    protected NameObjectCollectionBase()
    {
    }

    protected NameObjectCollectionBase(System.Collections.IEqualityComparer equalityComparer)
    {
    }

    protected NameObjectCollectionBase(int capacity, System.Collections.IHashCodeProvider hashProvider, System.Collections.IComparer comparer)
    {
    }

    protected NameObjectCollectionBase(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
    {
    }

    protected NameObjectCollectionBase(int capacity)
    {
    }

    public virtual new void OnDeserialization(Object sender)
    {
    }

    void System.Collections.ICollection.CopyTo(Array array, int index)
    {
    }
    #endregion

    #region Properties and indexers
    public virtual new int Count
    {
      get
      {
        return default(int);
      }
    }

    protected bool IsReadOnly
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public virtual new System.Collections.Specialized.NameObjectCollectionBase.KeysCollection Keys
    {
      get
      {
        return default(System.Collections.Specialized.NameObjectCollectionBase.KeysCollection);
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
