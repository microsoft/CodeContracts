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

// File System.Web.SiteMapNodeCollection.cs
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


namespace System.Web
{
  public partial class SiteMapNodeCollection : System.Web.UI.IHierarchicalEnumerable, System.Collections.IList, System.Collections.ICollection, System.Collections.IEnumerable
  {
    #region Methods and constructors
    public virtual new int Add (SiteMapNode value)
    {
      return default(int);
    }

    public virtual new void AddRange (SiteMapNode[] value)
    {
    }

    public virtual new void AddRange (System.Web.SiteMapNodeCollection value)
    {
    }

    public virtual new void Clear ()
    {
    }

    public virtual new bool Contains (SiteMapNode value)
    {
      return default(bool);
    }

    public virtual new void CopyTo (SiteMapNode[] array, int index)
    {
    }

    public System.Web.UI.WebControls.SiteMapDataSourceView GetDataSourceView (System.Web.UI.WebControls.SiteMapDataSource owner, string viewName)
    {
      Contract.Ensures (Contract.Result<System.Web.UI.WebControls.SiteMapDataSourceView>() != null);

      return default(System.Web.UI.WebControls.SiteMapDataSourceView);
    }

    public virtual new System.Collections.IEnumerator GetEnumerator ()
    {
      return default(System.Collections.IEnumerator);
    }

    public System.Web.UI.WebControls.SiteMapHierarchicalDataSourceView GetHierarchicalDataSourceView ()
    {
      Contract.Ensures (Contract.Result<System.Web.UI.WebControls.SiteMapHierarchicalDataSourceView>() != null);

      return default(System.Web.UI.WebControls.SiteMapHierarchicalDataSourceView);
    }

    public virtual new System.Web.UI.IHierarchyData GetHierarchyData (Object enumeratedItem)
    {
      return default(System.Web.UI.IHierarchyData);
    }

    public virtual new int IndexOf (SiteMapNode value)
    {
      return default(int);
    }

    public virtual new void Insert (int index, SiteMapNode value)
    {
    }

    protected virtual new void OnValidate (Object value)
    {
    }

    public static System.Web.SiteMapNodeCollection ReadOnly (System.Web.SiteMapNodeCollection collection)
    {
      Contract.Ensures (Contract.Result<System.Web.SiteMapNodeCollection>() != null);

      return default(System.Web.SiteMapNodeCollection);
    }

    public virtual new void Remove (SiteMapNode value)
    {
    }

    public virtual new void RemoveAt (int index)
    {
    }

    public SiteMapNodeCollection ()
    {
    }

    public SiteMapNodeCollection (SiteMapNode[] value)
    {
    }

    public SiteMapNodeCollection (SiteMapNode value)
    {
    }

    public SiteMapNodeCollection (System.Web.SiteMapNodeCollection value)
    {
    }

    public SiteMapNodeCollection (int capacity)
    {
    }

    void System.Collections.ICollection.CopyTo (Array array, int index)
    {
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator ()
    {
      return default(System.Collections.IEnumerator);
    }

    int System.Collections.IList.Add (Object value)
    {
      return default(int);
    }

    void System.Collections.IList.Clear ()
    {
    }

    bool System.Collections.IList.Contains (Object value)
    {
      return default(bool);
    }

    int System.Collections.IList.IndexOf (Object value)
    {
      return default(int);
    }

    void System.Collections.IList.Insert (int index, Object value)
    {
    }

    void System.Collections.IList.Remove (Object value)
    {
    }

    void System.Collections.IList.RemoveAt (int index)
    {
    }

    System.Web.UI.IHierarchyData System.Web.UI.IHierarchicalEnumerable.GetHierarchyData (Object enumeratedItem)
    {
      return default(System.Web.UI.IHierarchyData);
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

    public virtual new bool IsFixedSize
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool IsReadOnly
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool IsSynchronized
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new SiteMapNode this [int index]
    {
      get
      {
        return default(SiteMapNode);
      }
      set
      {
      }
    }

    public virtual new Object SyncRoot
    {
      get
      {
        return default(Object);
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
  }
}
