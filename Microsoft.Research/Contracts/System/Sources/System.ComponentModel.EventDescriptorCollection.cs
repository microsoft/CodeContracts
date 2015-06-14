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

// File System.ComponentModel.EventDescriptorCollection.cs
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


namespace System.ComponentModel
{
  public partial class EventDescriptorCollection : System.Collections.IList, System.Collections.ICollection, System.Collections.IEnumerable
  {
    #region Methods and constructors
    public int Add(EventDescriptor value)
    {
      Contract.Ensures(0 <= Contract.Result<int>());
      Contract.Ensures(Contract.Result<int>() <= 2147483646);

      return default(int);
    }

    public void Clear()
    {
    }

    public bool Contains(EventDescriptor value)
    {
      return default(bool);
    }

    public EventDescriptorCollection(EventDescriptor[] events)
    {
    }

    public EventDescriptorCollection(EventDescriptor[] events, bool readOnly)
    {
    }

    public virtual new EventDescriptor Find(string name, bool ignoreCase)
    {
      return default(EventDescriptor);
    }

    public System.Collections.IEnumerator GetEnumerator()
    {
      return default(System.Collections.IEnumerator);
    }

    public int IndexOf(EventDescriptor value)
    {
      return default(int);
    }

    public void Insert(int index, EventDescriptor value)
    {
      Contract.Requires(0 <= index);
    }

    protected void InternalSort(string[] names)
    {
    }

    protected void InternalSort(System.Collections.IComparer sorter)
    {
    }

    public void Remove(EventDescriptor value)
    {
    }

    public void RemoveAt(int index)
    {
    }

    public virtual new System.ComponentModel.EventDescriptorCollection Sort(string[] names)
    {
      return default(System.ComponentModel.EventDescriptorCollection);
    }

    public virtual new System.ComponentModel.EventDescriptorCollection Sort()
    {
      return default(System.ComponentModel.EventDescriptorCollection);
    }

    public virtual new System.ComponentModel.EventDescriptorCollection Sort(string[] names, System.Collections.IComparer comparer)
    {
      return default(System.ComponentModel.EventDescriptorCollection);
    }

    public virtual new System.ComponentModel.EventDescriptorCollection Sort(System.Collections.IComparer comparer)
    {
      return default(System.ComponentModel.EventDescriptorCollection);
    }

    void System.Collections.ICollection.CopyTo(Array array, int index)
    {
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
      return default(System.Collections.IEnumerator);
    }

    int System.Collections.IList.Add(Object value)
    {
      return default(int);
    }

    void System.Collections.IList.Clear()
    {
    }

    bool System.Collections.IList.Contains(Object value)
    {
      return default(bool);
    }

    int System.Collections.IList.IndexOf(Object value)
    {
      return default(int);
    }

    void System.Collections.IList.Insert(int index, Object value)
    {
    }

    void System.Collections.IList.Remove(Object value)
    {
    }

    void System.Collections.IList.RemoveAt(int index)
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

    public virtual new EventDescriptor this [int index]
    {
      get
      {
        Contract.Requires(0 <= index);

        return default(EventDescriptor);
      }
    }

    public virtual new EventDescriptor this [string name]
    {
      get
      {
        return default(EventDescriptor);
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

    bool System.Collections.IList.IsFixedSize
    {
      get
      {
        return default(bool);
      }
    }

    bool System.Collections.IList.IsReadOnly
    {
      get
      {
        return default(bool);
      }
    }

    Object System.Collections.IList.this [int index]
    {
      get
      {
        return default(Object);
      }
      set
      {
      }
    }
    #endregion

    #region Fields
    public readonly static System.ComponentModel.EventDescriptorCollection Empty;
    #endregion
  }
}
