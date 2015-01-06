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

// File System.Web.UI.DataSourceView.cs
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


namespace System.Web.UI
{
  abstract public partial class DataSourceView
  {
    #region Methods and constructors

#if NETFRAMEWORK_4_0
    [Pure]
    public virtual new bool CanExecute (string commandName)
    {
      return default(bool);
    }
#endif

    protected DataSourceView (IDataSource owner, string viewName)
    {
    }

    public virtual new void Delete (System.Collections.IDictionary keys, System.Collections.IDictionary oldValues, DataSourceViewOperationCallback callback)
    {
    }

#if NETFRAMEWORK_4_0
    public virtual new void ExecuteCommand (string commandName, System.Collections.IDictionary keys, System.Collections.IDictionary values, DataSourceViewOperationCallback callback)
    {
    }

    protected virtual new int ExecuteCommand (string commandName, System.Collections.IDictionary keys, System.Collections.IDictionary values)
    {
      return default(int);
    }
#endif

    protected virtual new int ExecuteDelete (System.Collections.IDictionary keys, System.Collections.IDictionary oldValues)
    {
      return default(int);
    }

    protected virtual new int ExecuteInsert (System.Collections.IDictionary values)
    {
      return default(int);
    }

    protected internal abstract System.Collections.IEnumerable ExecuteSelect (DataSourceSelectArguments arguments);

    protected virtual new int ExecuteUpdate (System.Collections.IDictionary keys, System.Collections.IDictionary values, System.Collections.IDictionary oldValues)
    {
      return default(int);
    }

    public virtual new void Insert (System.Collections.IDictionary values, DataSourceViewOperationCallback callback)
    {
    }

    protected virtual new void OnDataSourceViewChanged (EventArgs e)
    {
    }

    protected internal virtual new void RaiseUnsupportedCapabilityError (DataSourceCapabilities capability)
    {
    }

    public virtual new void Select (DataSourceSelectArguments arguments, DataSourceViewSelectCallback callback)
    {
    }

    public virtual new void Update (System.Collections.IDictionary keys, System.Collections.IDictionary values, System.Collections.IDictionary oldValues, DataSourceViewOperationCallback callback)
    {
    }
    #endregion

    #region Properties and indexers
    public virtual new bool CanDelete
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool CanInsert
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool CanPage
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool CanRetrieveTotalRowCount
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool CanSort
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool CanUpdate
    {
      get
      {
        return default(bool);
      }
    }

    protected System.ComponentModel.EventHandlerList Events
    {
      get
      {
        Contract.Ensures (Contract.Result<System.ComponentModel.EventHandlerList>() != null);

        return default(System.ComponentModel.EventHandlerList);
      }
    }

    public string Name
    {
      get
      {
        return default(string);
      }
    }
    #endregion
  }
}
