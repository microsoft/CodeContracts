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

// File System.Web.UI.WebControls.TableCellCollection.cs
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
  sealed public partial class TableCellCollection : System.Collections.IList, System.Collections.ICollection, System.Collections.IEnumerable
  {
    #region Methods and constructors
    public int Add (TableCell cell)
    {
      return default(int);
    }

    public void AddAt (int index, TableCell cell)
    {
    }

    public void AddRange (TableCell[] cells)
    {
    }

    public void Clear ()
    {
    }

    public void CopyTo (Array array, int index)
    {
    }

    public int GetCellIndex (TableCell cell)
    {
      Contract.Ensures (Contract.Result<int>() >= -1);

      return default(int);
    }

    public System.Collections.IEnumerator GetEnumerator ()
    {
      return default(System.Collections.IEnumerator);
    }

    public void Remove (TableCell cell)
    {
    }

    public void RemoveAt (int index)
    {
    }

    int System.Collections.IList.Add (Object o)
    {
      return default(int);
    }

    bool System.Collections.IList.Contains (Object o)
    {
      return default(bool);
    }

    int System.Collections.IList.IndexOf (Object o)
    {
      return default(int);
    }

    void System.Collections.IList.Insert (int index, Object o)
    {
    }

    void System.Collections.IList.Remove (Object o)
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

    public TableCell this [int index]
    {
      get
      {
        Contract.Ensures (Contract.Result<System.Web.UI.WebControls.TableCell>() != null);

        return default(TableCell);
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
    #endregion
  }
}
