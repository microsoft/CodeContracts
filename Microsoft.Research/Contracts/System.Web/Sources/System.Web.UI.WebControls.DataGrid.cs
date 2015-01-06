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

// File System.Web.UI.WebControls.DataGrid.cs
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
  public partial class DataGrid : BaseDataList, System.Web.UI.INamingContainer
  {
    #region Methods and constructors
    protected virtual new System.Collections.ArrayList CreateColumnSet(PagedDataSource dataSource, bool useDataSource)
    {
      return default(System.Collections.ArrayList);
    }

    protected override void CreateControlHierarchy(bool useDataSource)
    {
    }

    protected override Style CreateControlStyle()
    {
      return default(Style);
    }

    protected virtual new DataGridItem CreateItem(int itemIndex, int dataSourceIndex, ListItemType itemType)
    {
      return default(DataGridItem);
    }

    public DataGrid()
    {
    }

    protected virtual new void InitializeItem(DataGridItem item, DataGridColumn[] columns)
    {
    }

    protected virtual new void InitializePager(DataGridItem item, int columnSpan, PagedDataSource pagedDataSource)
    {
    }

    protected override void LoadViewState(Object savedState)
    {
    }

    protected override bool OnBubbleEvent(Object source, EventArgs e)
    {
      return default(bool);
    }

    protected virtual new void OnCancelCommand(DataGridCommandEventArgs e)
    {
    }

    protected virtual new void OnDeleteCommand(DataGridCommandEventArgs e)
    {
    }

    protected virtual new void OnEditCommand(DataGridCommandEventArgs e)
    {
    }

    protected virtual new void OnItemCommand(DataGridCommandEventArgs e)
    {
    }

    protected virtual new void OnItemCreated(DataGridItemEventArgs e)
    {
    }

    protected virtual new void OnItemDataBound(DataGridItemEventArgs e)
    {
    }

    protected virtual new void OnPageIndexChanged(DataGridPageChangedEventArgs e)
    {
    }

    protected virtual new void OnSortCommand(DataGridSortCommandEventArgs e)
    {
    }

    protected virtual new void OnUpdateCommand(DataGridCommandEventArgs e)
    {
    }

    protected internal override void PrepareControlHierarchy()
    {
    }

    protected override Object SaveViewState()
    {
      return default(Object);
    }

    protected override void TrackViewState()
    {
    }
    #endregion

    #region Properties and indexers
    public virtual new bool AllowCustomPaging
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

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

    public virtual new bool AllowSorting
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public virtual new TableItemStyle AlternatingItemStyle
    {
      get
      {
        return default(TableItemStyle);
      }
    }

    public virtual new bool AutoGenerateColumns
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

    public virtual new DataGridColumnCollection Columns
    {
      get
      {
        return default(DataGridColumnCollection);
      }
    }

    public int CurrentPageIndex
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public virtual new int EditItemIndex
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public virtual new TableItemStyle EditItemStyle
    {
      get
      {
        return default(TableItemStyle);
      }
    }

    public virtual new TableItemStyle FooterStyle
    {
      get
      {
        return default(TableItemStyle);
      }
    }

    public virtual new TableItemStyle HeaderStyle
    {
      get
      {
        return default(TableItemStyle);
      }
    }

    public virtual new DataGridItemCollection Items
    {
      get
      {
        return default(DataGridItemCollection);
      }
    }

    public virtual new TableItemStyle ItemStyle
    {
      get
      {
        return default(TableItemStyle);
      }
    }

    public int PageCount
    {
      get
      {
        return default(int);
      }
    }

    public virtual new DataGridPagerStyle PagerStyle
    {
      get
      {
        return default(DataGridPagerStyle);
      }
    }

    public virtual new int PageSize
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public virtual new int SelectedIndex
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public virtual new DataGridItem SelectedItem
    {
      get
      {
        return default(DataGridItem);
      }
    }

    public virtual new TableItemStyle SelectedItemStyle
    {
      get
      {
        return default(TableItemStyle);
      }
    }

    public virtual new bool ShowFooter
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public virtual new bool ShowHeader
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    protected override System.Web.UI.HtmlTextWriterTag TagKey
    {
      get
      {
        return default(System.Web.UI.HtmlTextWriterTag);
      }
    }

    public virtual new int VirtualItemCount
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }
    #endregion

    #region Events
    public event DataGridCommandEventHandler CancelCommand
    {
      add
      {
      }
      remove
      {
      }
    }

    public event DataGridCommandEventHandler DeleteCommand
    {
      add
      {
      }
      remove
      {
      }
    }

    public event DataGridCommandEventHandler EditCommand
    {
      add
      {
      }
      remove
      {
      }
    }

    public event DataGridCommandEventHandler ItemCommand
    {
      add
      {
      }
      remove
      {
      }
    }

    public event DataGridItemEventHandler ItemCreated
    {
      add
      {
      }
      remove
      {
      }
    }

    public event DataGridItemEventHandler ItemDataBound
    {
      add
      {
      }
      remove
      {
      }
    }

    public event DataGridPageChangedEventHandler PageIndexChanged
    {
      add
      {
      }
      remove
      {
      }
    }

    public event DataGridSortCommandEventHandler SortCommand
    {
      add
      {
      }
      remove
      {
      }
    }

    public event DataGridCommandEventHandler UpdateCommand
    {
      add
      {
      }
      remove
      {
      }
    }
    #endregion

    #region Fields
    public static string CancelCommandName;
    public static string DeleteCommandName;
    public static string EditCommandName;
    public static string NextPageCommandArgument;
    public static string PageCommandName;
    public static string PrevPageCommandArgument;
    public static string SelectCommandName;
    public static string SortCommandName;
    public static string UpdateCommandName;
    #endregion
  }
}
