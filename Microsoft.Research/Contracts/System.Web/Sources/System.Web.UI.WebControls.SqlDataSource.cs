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

// File System.Web.UI.WebControls.SqlDataSource.cs
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
  public partial class SqlDataSource : System.Web.UI.DataSourceControl
  {
    #region Methods and constructors
    protected virtual new SqlDataSourceView CreateDataSourceView(string viewName)
    {
      return default(SqlDataSourceView);
    }

    public int Delete()
    {
      return default(int);
    }

    protected virtual new System.Data.Common.DbProviderFactory GetDbProviderFactory()
    {
      return default(System.Data.Common.DbProviderFactory);
    }

    protected override System.Web.UI.DataSourceView GetView(string viewName)
    {
      return default(System.Web.UI.DataSourceView);
    }

    protected override System.Collections.ICollection GetViewNames()
    {
      return default(System.Collections.ICollection);
    }

    public int Insert()
    {
      return default(int);
    }

    protected override void LoadViewState(Object savedState)
    {
    }

    protected internal override void OnInit(EventArgs e)
    {
    }

    protected override Object SaveViewState()
    {
      return default(Object);
    }

    public System.Collections.IEnumerable Select(System.Web.UI.DataSourceSelectArguments arguments)
    {
      return default(System.Collections.IEnumerable);
    }

    public SqlDataSource(string connectionString, string selectCommand)
    {
    }

    public SqlDataSource(string providerName, string connectionString, string selectCommand)
    {
    }

    public SqlDataSource()
    {
    }

    protected override void TrackViewState()
    {
    }

    public int Update()
    {
      return default(int);
    }
    #endregion

    #region Properties and indexers
    public virtual new int CacheDuration
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public virtual new System.Web.UI.DataSourceCacheExpiry CacheExpirationPolicy
    {
      get
      {
        return default(System.Web.UI.DataSourceCacheExpiry);
      }
      set
      {
      }
    }

    public virtual new string CacheKeyDependency
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new bool CancelSelectOnNullParameter
    {
      get
      {
        return default(bool);
      }
      set
      {
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

    public virtual new string ConnectionString
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public SqlDataSourceMode DataSourceMode
    {
      get
      {
        return default(SqlDataSourceMode);
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

    public virtual new bool EnableCaching
    {
      get
      {
        return default(bool);
      }
      set
      {
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

    public virtual new string ProviderName
    {
      get
      {
        return default(string);
      }
      set
      {
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

    public virtual new string SqlCacheDependency
    {
      get
      {
        return default(string);
      }
      set
      {
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
