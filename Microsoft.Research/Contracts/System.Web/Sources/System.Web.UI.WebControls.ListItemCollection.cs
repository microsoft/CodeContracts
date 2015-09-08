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

// File System.Web.UI.WebControls.ListItemCollection.cs
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


namespace System.Web.UI.WebControls
{
  sealed public partial class ListItemCollection : System.Collections.IList, System.Collections.ICollection, System.Collections.IEnumerable, System.Web.UI.IStateManager
  {
    #region Methods and constructors
    public void Add(ListItem item)
    {
    }

    public void Add(string item)
    {
    }

    public void AddRange(ListItem[] items)
    {
    }

    public void Clear()
    {
    }

    public bool Contains(ListItem item)
    {
      return default(bool);
    }

    public void CopyTo(Array array, int index)
    {
    }

    public ListItem FindByText(string text)
    {
      return default(ListItem);
    }

    public ListItem FindByValue(string value)
    {
      return default(ListItem);
    }

    public System.Collections.IEnumerator GetEnumerator()
    {
      return default(System.Collections.IEnumerator);
    }

    public int IndexOf(ListItem item)
    {
      return default(int);
    }

    public void Insert(int index, string item)
    {
    }

    public void Insert(int index, ListItem item)
    {
    }

    public ListItemCollection()
    {
    }

    public void Remove(string item)
    {
    }

    public void Remove(ListItem item)
    {
    }

    public void RemoveAt(int index)
    {
    }

    int System.Collections.IList.Add(Object item)
    {
      return default(int);
    }

    bool System.Collections.IList.Contains(Object item)
    {
      return default(bool);
    }

    int System.Collections.IList.IndexOf(Object item)
    {
      return default(int);
    }

    void System.Collections.IList.Insert(int index, Object item)
    {
    }

    void System.Collections.IList.Remove(Object item)
    {
    }

    void System.Web.UI.IStateManager.LoadViewState(Object state)
    {
    }

    Object System.Web.UI.IStateManager.SaveViewState()
    {
      return default(Object);
    }

    void System.Web.UI.IStateManager.TrackViewState()
    {
    }
    #endregion

    #region Properties and indexers
    public int Capacity
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

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

    public ListItem this [int index]
    {
      get
      {
        return default(ListItem);
      }
    }

    public Object SyncRoot
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

    bool System.Web.UI.IStateManager.IsTrackingViewState
    {
      get
      {
        return default(bool);
      }
    }
    #endregion
  }
}
