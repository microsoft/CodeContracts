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

// File System.Windows.Input.InputBindingCollection.cs
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


namespace System.Windows.Input
{
  sealed public partial class InputBindingCollection : System.Collections.IList, System.Collections.ICollection, System.Collections.IEnumerable
  {
    #region Methods and constructors
    public int Add(InputBinding inputBinding)
    {
      return default(int);
    }

    public void AddRange(System.Collections.ICollection collection)
    {
    }

    public void Clear()
    {
    }

    public bool Contains(InputBinding key)
    {
      return default(bool);
    }

    public void CopyTo(InputBinding[] inputBindings, int index)
    {
    }

    public System.Collections.IEnumerator GetEnumerator()
    {
      return default(System.Collections.IEnumerator);
    }

    public int IndexOf(InputBinding value)
    {
      return default(int);
    }

    public InputBindingCollection(System.Collections.IList inputBindings)
    {
    }

    public InputBindingCollection()
    {
    }

    public void Insert(int index, InputBinding inputBinding)
    {
    }

    public void Remove(InputBinding inputBinding)
    {
    }

    public void RemoveAt(int index)
    {
    }

    void System.Collections.ICollection.CopyTo(Array array, int index)
    {
    }

    int System.Collections.IList.Add(Object inputBinding)
    {
      return default(int);
    }

    bool System.Collections.IList.Contains(Object key)
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

    void System.Collections.IList.Remove(Object inputBinding)
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

    public bool IsSynchronized
    {
      get
      {
        return default(bool);
      }
    }

    public InputBinding this [int index]
    {
      get
      {
        return default(InputBinding);
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
  }
}
