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

// File System.Web.UI.WebControls.MenuItemCollection.cs
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


namespace System.Web.UI.WebControls
{
  sealed public partial class MenuItemCollection : System.Collections.ICollection, System.Collections.IEnumerable, System.Web.UI.IStateManager
  {
    #region Methods and constructors
    public void Add (MenuItem child)
    {
    }

    public void AddAt (int index, MenuItem child)
    {
    }

    public void Clear ()
    {
    }

    public bool Contains (MenuItem c)
    {
      return default(bool);
    }

    public void CopyTo (MenuItem[] array, int index)
    {
      Contract.Ensures ((index - array.Length) <= 0);
      Contract.Ensures (array.Length >= 0);
    }

    public void CopyTo (Array array, int index)
    {
    }

    public System.Collections.IEnumerator GetEnumerator ()
    {
      return default(System.Collections.IEnumerator);
    }

    public int IndexOf (MenuItem value)
    {
      Contract.Ensures (Contract.Result<int>() >= -1);

      return default(int);
    }

    public MenuItemCollection ()
    {
    }

    public MenuItemCollection (MenuItem owner)
    {
    }

    public void Remove (MenuItem value)
    {
    }

    public void RemoveAt (int index)
    {
    }

    void System.Web.UI.IStateManager.LoadViewState (Object state)
    {
    }

    Object System.Web.UI.IStateManager.SaveViewState ()
    {
      return default(Object);
    }

    void System.Web.UI.IStateManager.TrackViewState ()
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

    public bool IsSynchronized
    {
      get
      {
        return default(bool);
      }
    }

    public MenuItem this [int index]
    {
      get
      {
        return default(MenuItem);
      }
    }

    public Object SyncRoot
    {
      get
      {
        return default(Object);
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
