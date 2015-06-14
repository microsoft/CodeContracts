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

// File System.Web.UI.WebControls.SqlDataSourceView.cs
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
  public partial class SqlDataSourceView : System.Web.UI.DataSourceView, System.Web.UI.IStateManager
  {
    #region Methods and constructors
    public int Delete(System.Collections.IDictionary keys, System.Collections.IDictionary oldValues)
    {
      return default(int);
    }

    protected override int ExecuteDelete(System.Collections.IDictionary keys, System.Collections.IDictionary oldValues)
    {
      return default(int);
    }

    protected override int ExecuteInsert(System.Collections.IDictionary values)
    {
      return default(int);
    }

    protected internal override System.Collections.IEnumerable ExecuteSelect(System.Web.UI.DataSourceSelectArguments arguments)
    {
      return default(System.Collections.IEnumerable);
    }

    protected override int ExecuteUpdate(System.Collections.IDictionary keys, System.Collections.IDictionary values, System.Collections.IDictionary oldValues)
    {
      return default(int);
    }

    public int Insert(System.Collections.IDictionary values)
    {
      return default(int);
    }

    protected virtual new void LoadViewState(Object savedState)
    {
    }

    protected virtual new void OnDeleted(SqlDataSourceStatusEventArgs e)
    {
    }

    protected virtual new void OnDeleting(SqlDataSourceCommandEventArgs e)
    {
    }

    protected virtual new void OnFiltering(SqlDataSourceFilteringEventArgs e)
    {
    }

    protected virtual new void OnInserted(SqlDataSourceStatusEventArgs e)
    {
    }

    protected virtual new void OnInserting(SqlDataSourceCommandEventArgs e)
    {
    }

    protected virtual new void OnSelected(SqlDataSourceStatusEventArgs e)
    {
    }

    protected virtual new void OnSelecting(SqlDataSourceSelectingEventArgs e)
    {
    }

    protected virtual new void OnUpdated(SqlDataSourceStatusEventArgs e)
    {
    }

    protected virtual new void OnUpdating(SqlDataSourceCommandEventArgs e)
    {
    }

    protected internal override void RaiseUnsupportedCapabilityError(System.Web.UI.DataSourceCapabilities capability)
    {
    }

    protected virtual new Object SaveViewState()
    {
      return default(Object);
    }

    public System.Collections.IEnumerable Select(System.Web.UI.DataSourceSelectArguments arguments)
    {
      return default(System.Collections.IEnumerable);
    }

    public SqlDataSourceView(SqlDataSource owner, string name, System.Web.HttpContext context) : base (default(System.Web.UI.IDataSource), default(string))
    {
    }

    void System.Web.UI.IStateManager.LoadViewState(Object savedState)
    {
    }

    Object System.Web.UI.IStateManager.SaveViewState()
    {
      return default(Object);
    }

    void System.Web.UI.IStateManager.TrackViewState()
    {
    }

    protected virtual new void TrackViewState()
    {
    }

    public int Update(System.Collections.IDictionary keys, System.Collections.IDictionary values, System.Collections.IDictionary oldValues)
    {
      return default(int);
    }
    #endregion

    #region Properties and indexers
    public bool CancelSelectOnNullParameter
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public override bool CanDelete
    {
      get
      {
        return default(bool);
      }
    }

    public override bool CanInsert
    {
      get
      {
        return default(bool);
      }
    }

    public override bool CanPage
    {
      get
      {
        return default(bool);
      }
    }

    public override bool CanRetrieveTotalRowCount
    {
      get
      {
        return default(bool);
      }
    }

    public override bool CanSort
    {
      get
      {
        return default(bool);
      }
    }

    public override bool CanUpdate
    {
      get
      {
        return default(bool);
      }
    }

    public System.Web.UI.ConflictOptions ConflictDetection
    {
      get
      {
        return default(System.Web.UI.ConflictOptions);
      }
      set
      {
      }
    }

    public string DeleteCommand
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public SqlDataSourceCommandType DeleteCommandType
    {
      get
      {
        return default(SqlDataSourceCommandType);
      }
      set
      {
      }
    }

    public ParameterCollection DeleteParameters
    {
      get
      {
        return default(ParameterCollection);
      }
    }

    public string FilterExpression
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public ParameterCollection FilterParameters
    {
      get
      {
        return default(ParameterCollection);
      }
    }

    public string InsertCommand
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public SqlDataSourceCommandType InsertCommandType
    {
      get
      {
        return default(SqlDataSourceCommandType);
      }
      set
      {
      }
    }

    public ParameterCollection InsertParameters
    {
      get
      {
        return default(ParameterCollection);
      }
    }

    protected bool IsTrackingViewState
    {
      get
      {
        return default(bool);
      }
    }

    public string OldValuesParameterFormatString
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    protected virtual new string ParameterPrefix
    {
      get
      {
        return default(string);
      }
    }

    public string SelectCommand
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public SqlDataSourceCommandType SelectCommandType
    {
      get
      {
        return default(SqlDataSourceCommandType);
      }
      set
      {
      }
    }

    public ParameterCollection SelectParameters
    {
      get
      {
        return default(ParameterCollection);
      }
    }

    public string SortParameterName
    {
      get
      {
        return default(string);
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

    public string UpdateCommand
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public SqlDataSourceCommandType UpdateCommandType
    {
      get
      {
        return default(SqlDataSourceCommandType);
      }
      set
      {
      }
    }

    public ParameterCollection UpdateParameters
    {
      get
      {
        return default(ParameterCollection);
      }
    }
    #endregion

    #region Events
    public event SqlDataSourceStatusEventHandler Deleted
    {
      add
      {
      }
      remove
      {
      }
    }

    public event SqlDataSourceCommandEventHandler Deleting
    {
      add
      {
      }
      remove
      {
      }
    }

    public event SqlDataSourceFilteringEventHandler Filtering
    {
      add
      {
      }
      remove
      {
      }
    }

    public event SqlDataSourceStatusEventHandler Inserted
    {
      add
      {
      }
      remove
      {
      }
    }

    public event SqlDataSourceCommandEventHandler Inserting
    {
      add
      {
      }
      remove
      {
      }
    }

    public event SqlDataSourceStatusEventHandler Selected
    {
      add
      {
      }
      remove
      {
      }
    }

    public event SqlDataSourceSelectingEventHandler Selecting
    {
      add
      {
      }
      remove
      {
      }
    }

    public event SqlDataSourceStatusEventHandler Updated
    {
      add
      {
      }
      remove
      {
      }
    }

    public event SqlDataSourceCommandEventHandler Updating
    {
      add
      {
      }
      remove
      {
      }
    }
    #endregion
  }
}
