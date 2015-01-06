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

// File System.Web.UI.WebControls.BaseDataList.cs
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
  abstract public partial class BaseDataList : WebControl
  {
    #region Methods and constructors
    protected override void AddParsedSubObject(Object obj)
    {
    }

    protected BaseDataList()
    {
    }

    protected internal override void CreateChildControls()
    {
    }

    protected abstract void CreateControlHierarchy(bool useDataSource);

    protected virtual new System.Web.UI.DataSourceSelectArguments CreateDataSourceSelectArguments()
    {
      return default(System.Web.UI.DataSourceSelectArguments);
    }

    public override void DataBind()
    {
    }

    protected void EnsureDataBound()
    {
    }

    protected virtual new System.Collections.IEnumerable GetData()
    {
      return default(System.Collections.IEnumerable);
    }

    public static bool IsBindableType(Type type)
    {
      return default(bool);
    }

    protected override void OnDataBinding(EventArgs e)
    {
    }

    protected virtual new void OnDataPropertyChanged()
    {
    }

    protected virtual new void OnDataSourceViewChanged(Object sender, EventArgs e)
    {
    }

    protected internal override void OnInit(EventArgs e)
    {
    }

    protected internal override void OnLoad(EventArgs e)
    {
    }

    protected internal override void OnPreRender(EventArgs e)
    {
    }

    protected virtual new void OnSelectedIndexChanged(EventArgs e)
    {
    }

    protected internal abstract void PrepareControlHierarchy();

    protected internal override void Render(System.Web.UI.HtmlTextWriter writer)
    {
    }
    #endregion

    #region Properties and indexers
    public virtual new string Caption
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new TableCaptionAlign CaptionAlign
    {
      get
      {
        return default(TableCaptionAlign);
      }
      set
      {
      }
    }

    public virtual new int CellPadding
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public virtual new int CellSpacing
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public override System.Web.UI.ControlCollection Controls
    {
      get
      {
        return default(System.Web.UI.ControlCollection);
      }
    }

    public virtual new string DataKeyField
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public DataKeyCollection DataKeys
    {
      get
      {
        return default(DataKeyCollection);
      }
    }

    protected System.Collections.ArrayList DataKeysArray
    {
      get
      {
        return default(System.Collections.ArrayList);
      }
    }

    public string DataMember
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new Object DataSource
    {
      get
      {
        return default(Object);
      }
      set
      {
      }
    }

    public virtual new string DataSourceID
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new GridLines GridLines
    {
      get
      {
        return default(GridLines);
      }
      set
      {
      }
    }

    public virtual new HorizontalAlign HorizontalAlign
    {
      get
      {
        return default(HorizontalAlign);
      }
      set
      {
      }
    }

    protected bool Initialized
    {
      get
      {
        return default(bool);
      }
    }

    protected bool IsBoundUsingDataSourceID
    {
      get
      {
        return default(bool);
      }
    }

    protected bool RequiresDataBinding
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    protected System.Web.UI.DataSourceSelectArguments SelectArguments
    {
      get
      {
        return default(System.Web.UI.DataSourceSelectArguments);
      }
    }

    public override bool SupportsDisabledAttribute
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool UseAccessibleHeader
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }
    #endregion

    #region Events
    public event EventHandler SelectedIndexChanged
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
