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

// File System.Web.UI.WebControls.FormView.cs
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
  public partial class FormView : CompositeDataBoundControl, System.Web.UI.IDataItemContainer, System.Web.UI.INamingContainer, System.Web.UI.IPostBackEventHandler, IPostBackContainer, IDataBoundItemControl, IDataBoundControl, System.Web.UI.IRenderOuterTableControl
  {
    #region Methods and constructors
    public void ChangeMode(FormViewMode newMode)
    {
    }

    protected override int CreateChildControls(System.Collections.IEnumerable dataSource, bool dataBinding)
    {
      return default(int);
    }

    protected override Style CreateControlStyle()
    {
      return default(Style);
    }

    protected override System.Web.UI.DataSourceSelectArguments CreateDataSourceSelectArguments()
    {
      return default(System.Web.UI.DataSourceSelectArguments);
    }

    protected virtual new FormViewRow CreateRow(int itemIndex, DataControlRowType rowType, DataControlRowState rowState)
    {
      return default(FormViewRow);
    }

    protected virtual new Table CreateTable()
    {
      return default(Table);
    }

    public sealed override void DataBind()
    {
    }

    public virtual new void DeleteItem()
    {
    }

    protected override void EnsureDataBound()
    {
    }

    protected virtual new void ExtractRowValues(System.Collections.Specialized.IOrderedDictionary fieldValues, bool includeKeys)
    {
    }

    public FormView()
    {
    }

    protected virtual new void InitializePager(FormViewRow row, PagedDataSource pagedDataSource)
    {
    }

    protected virtual new void InitializeRow(FormViewRow row)
    {
    }

    public virtual new void InsertItem(bool causesValidation)
    {
    }

    public virtual new bool IsBindableType(Type type)
    {
      return default(bool);
    }

    protected internal override void LoadControlState(Object savedState)
    {
    }

    protected override void LoadViewState(Object savedState)
    {
    }

    protected internal virtual new string ModifiedOuterTableStylePropertyName()
    {
      return default(string);
    }

    protected override bool OnBubbleEvent(Object source, EventArgs e)
    {
      return default(bool);
    }

    protected internal override void OnInit(EventArgs e)
    {
    }

    protected virtual new void OnItemCommand(FormViewCommandEventArgs e)
    {
    }

    protected virtual new void OnItemCreated(EventArgs e)
    {
    }

    protected virtual new void OnItemDeleted(FormViewDeletedEventArgs e)
    {
    }

    protected virtual new void OnItemDeleting(FormViewDeleteEventArgs e)
    {
    }

    protected virtual new void OnItemInserted(FormViewInsertedEventArgs e)
    {
    }

    protected virtual new void OnItemInserting(FormViewInsertEventArgs e)
    {
    }

    protected virtual new void OnItemUpdated(FormViewUpdatedEventArgs e)
    {
    }

    protected virtual new void OnItemUpdating(FormViewUpdateEventArgs e)
    {
    }

    protected virtual new void OnModeChanged(EventArgs e)
    {
    }

    protected virtual new void OnModeChanging(FormViewModeEventArgs e)
    {
    }

    protected virtual new void OnPageIndexChanged(EventArgs e)
    {
    }

    protected virtual new void OnPageIndexChanging(FormViewPageEventArgs e)
    {
    }

    protected internal override void PerformDataBinding(System.Collections.IEnumerable data)
    {
    }

    protected internal virtual new void PrepareControlHierarchy()
    {
    }

    protected virtual new void RaisePostBackEvent(string eventArgument)
    {
    }

    protected internal override void Render(System.Web.UI.HtmlTextWriter writer)
    {
    }

    protected internal override Object SaveControlState()
    {
      return default(Object);
    }

    protected override Object SaveViewState()
    {
      return default(Object);
    }

    public void SetPageIndex(int index)
    {
    }

    void System.Web.UI.IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
    {
    }

    System.Web.UI.PostBackOptions System.Web.UI.WebControls.IPostBackContainer.GetPostBackOptions(IButtonControl buttonControl)
    {
      return default(System.Web.UI.PostBackOptions);
    }

    protected override void TrackViewState()
    {
    }

    public virtual new void UpdateItem(bool causesValidation)
    {
    }
    #endregion

    #region Properties and indexers
    public virtual new bool AllowPaging
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public virtual new string BackImageUrl
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new FormViewRow BottomPagerRow
    {
      get
      {
        return default(FormViewRow);
      }
    }

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

    public FormViewMode CurrentMode
    {
      get
      {
        return default(FormViewMode);
      }
    }

    public virtual new Object DataItem
    {
      get
      {
        return default(Object);
      }
    }

    public int DataItemCount
    {
      get
      {
        return default(int);
      }
    }

    public virtual new int DataItemIndex
    {
      get
      {
        return default(int);
      }
    }

    public virtual new DataKey DataKey
    {
      get
      {
        return default(DataKey);
      }
    }

    public virtual new string[] DataKeyNames
    {
      get
      {
        return default(string[]);
      }
      set
      {
      }
    }

    public virtual new FormViewMode DefaultMode
    {
      get
      {
        return default(FormViewMode);
      }
      set
      {
      }
    }

    public virtual new System.Web.UI.ITemplate EditItemTemplate
    {
      get
      {
        return default(System.Web.UI.ITemplate);
      }
      set
      {
      }
    }

    public TableItemStyle EditRowStyle
    {
      get
      {
        return default(TableItemStyle);
      }
    }

    public TableItemStyle EmptyDataRowStyle
    {
      get
      {
        return default(TableItemStyle);
      }
    }

    public virtual new System.Web.UI.ITemplate EmptyDataTemplate
    {
      get
      {
        return default(System.Web.UI.ITemplate);
      }
      set
      {
      }
    }

    public virtual new string EmptyDataText
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new bool EnableModelValidation
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public virtual new FormViewRow FooterRow
    {
      get
      {
        return default(FormViewRow);
      }
    }

    public TableItemStyle FooterStyle
    {
      get
      {
        return default(TableItemStyle);
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

    public virtual new string FooterText
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

    public virtual new FormViewRow HeaderRow
    {
      get
      {
        return default(FormViewRow);
      }
    }

    public TableItemStyle HeaderStyle
    {
      get
      {
        return default(TableItemStyle);
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

    public virtual new string HeaderText
    {
      get
      {
        return default(string);
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

    public virtual new System.Web.UI.ITemplate InsertItemTemplate
    {
      get
      {
        return default(System.Web.UI.ITemplate);
      }
      set
      {
      }
    }

    public TableItemStyle InsertRowStyle
    {
      get
      {
        return default(TableItemStyle);
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

    public virtual new int PageCount
    {
      get
      {
        return default(int);
      }
    }

    public virtual new int PageIndex
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public virtual new PagerSettings PagerSettings
    {
      get
      {
        return default(PagerSettings);
      }
    }

    public TableItemStyle PagerStyle
    {
      get
      {
        return default(TableItemStyle);
      }
    }

    public virtual new System.Web.UI.ITemplate PagerTemplate
    {
      get
      {
        return default(System.Web.UI.ITemplate);
      }
      set
      {
      }
    }

    public virtual new bool RenderOuterTable
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public virtual new FormViewRow Row
    {
      get
      {
        return default(FormViewRow);
      }
    }

    public TableItemStyle RowStyle
    {
      get
      {
        return default(TableItemStyle);
      }
    }

    public Object SelectedValue
    {
      get
      {
        return default(Object);
      }
    }

    int System.Web.UI.IDataItemContainer.DataItemIndex
    {
      get
      {
        return default(int);
      }
    }

    int System.Web.UI.IDataItemContainer.DisplayIndex
    {
      get
      {
        return default(int);
      }
    }

    string[] System.Web.UI.WebControls.IDataBoundControl.DataKeyNames
    {
      get
      {
        return default(string[]);
      }
      set
      {
      }
    }

    string System.Web.UI.WebControls.IDataBoundControl.DataMember
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    Object System.Web.UI.WebControls.IDataBoundControl.DataSource
    {
      get
      {
        return default(Object);
      }
      set
      {
      }
    }

    string System.Web.UI.WebControls.IDataBoundControl.DataSourceID
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    System.Web.UI.IDataSource System.Web.UI.WebControls.IDataBoundControl.DataSourceObject
    {
      get
      {
        return default(System.Web.UI.IDataSource);
      }
    }

    System.Web.UI.WebControls.DataKey System.Web.UI.WebControls.IDataBoundItemControl.DataKey
    {
      get
      {
        return default(System.Web.UI.WebControls.DataKey);
      }
    }

    DataBoundControlMode System.Web.UI.WebControls.IDataBoundItemControl.Mode
    {
      get
      {
        return default(DataBoundControlMode);
      }
    }

    protected override System.Web.UI.HtmlTextWriterTag TagKey
    {
      get
      {
        return default(System.Web.UI.HtmlTextWriterTag);
      }
    }

    public virtual new FormViewRow TopPagerRow
    {
      get
      {
        return default(FormViewRow);
      }
    }
    #endregion

    #region Events
    public event FormViewCommandEventHandler ItemCommand
    {
      add
      {
      }
      remove
      {
      }
    }

    public event EventHandler ItemCreated
    {
      add
      {
      }
      remove
      {
      }
    }

    public event FormViewDeletedEventHandler ItemDeleted
    {
      add
      {
      }
      remove
      {
      }
    }

    public event FormViewDeleteEventHandler ItemDeleting
    {
      add
      {
      }
      remove
      {
      }
    }

    public event FormViewInsertedEventHandler ItemInserted
    {
      add
      {
      }
      remove
      {
      }
    }

    public event FormViewInsertEventHandler ItemInserting
    {
      add
      {
      }
      remove
      {
      }
    }

    public event FormViewUpdatedEventHandler ItemUpdated
    {
      add
      {
      }
      remove
      {
      }
    }

    public event FormViewUpdateEventHandler ItemUpdating
    {
      add
      {
      }
      remove
      {
      }
    }

    public event EventHandler ModeChanged
    {
      add
      {
      }
      remove
      {
      }
    }

    public event FormViewModeEventHandler ModeChanging
    {
      add
      {
      }
      remove
      {
      }
    }

    public event EventHandler PageIndexChanged
    {
      add
      {
      }
      remove
      {
      }
    }

    public event FormViewPageEventHandler PageIndexChanging
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
