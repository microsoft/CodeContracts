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

// File System.Web.UI.WebControls.Repeater.cs
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
  public partial class Repeater : System.Web.UI.Control, System.Web.UI.INamingContainer
  {
    #region Methods and constructors
    protected internal override void CreateChildControls()
    {
    }

    protected virtual new void CreateControlHierarchy(bool useDataSource)
    {
    }

    protected virtual new System.Web.UI.DataSourceSelectArguments CreateDataSourceSelectArguments()
    {
      return default(System.Web.UI.DataSourceSelectArguments);
    }

    protected virtual new RepeaterItem CreateItem(int itemIndex, ListItemType itemType)
    {
      return default(RepeaterItem);
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

    protected virtual new void InitializeItem(RepeaterItem item)
    {
    }

    protected override bool OnBubbleEvent(Object sender, EventArgs e)
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

    protected virtual new void OnItemCommand(RepeaterCommandEventArgs e)
    {
    }

    protected virtual new void OnItemCreated(RepeaterItemEventArgs e)
    {
    }

    protected virtual new void OnItemDataBound(RepeaterItemEventArgs e)
    {
    }

    protected internal override void OnLoad(EventArgs e)
    {
    }

    protected internal override void OnPreRender(EventArgs e)
    {
    }

    public Repeater()
    {
    }
    #endregion

    #region Properties and indexers
    public virtual new System.Web.UI.ITemplate AlternatingItemTemplate
    {
      get
      {
        return default(System.Web.UI.ITemplate);
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

    public virtual new string DataMember
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

    public override bool EnableTheming
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public virtual new System.Web.UI.ITemplate FooterTemplate
    {
      get
      {
        return default(System.Web.UI.ITemplate);
      }
      set
      {
      }
    }

    public virtual new System.Web.UI.ITemplate HeaderTemplate
    {
      get
      {
        return default(System.Web.UI.ITemplate);
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

    public virtual new RepeaterItemCollection Items
    {
      get
      {
        return default(RepeaterItemCollection);
      }
    }

    public virtual new System.Web.UI.ITemplate ItemTemplate
    {
      get
      {
        return default(System.Web.UI.ITemplate);
      }
      set
      {
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

    public virtual new System.Web.UI.ITemplate SeparatorTemplate
    {
      get
      {
        return default(System.Web.UI.ITemplate);
      }
      set
      {
      }
    }
    #endregion

    #region Events
    public event RepeaterCommandEventHandler ItemCommand
    {
      add
      {
      }
      remove
      {
      }
    }

    public event RepeaterItemEventHandler ItemCreated
    {
      add
      {
      }
      remove
      {
      }
    }

    public event RepeaterItemEventHandler ItemDataBound
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
