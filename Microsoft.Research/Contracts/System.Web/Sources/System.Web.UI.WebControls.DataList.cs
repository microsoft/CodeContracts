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

// File System.Web.UI.WebControls.DataList.cs
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
  public partial class DataList : BaseDataList, System.Web.UI.INamingContainer, IRepeatInfoUser, IWizardSideBarListControl
  {
    #region Methods and constructors
    protected override void CreateControlHierarchy(bool useDataSource)
    {
    }

    protected override Style CreateControlStyle()
    {
      return default(Style);
    }

    protected virtual new DataListItem CreateItem(int itemIndex, ListItemType itemType)
    {
      return default(DataListItem);
    }

    public DataList()
    {
    }

    protected virtual new void InitializeItem(DataListItem item)
    {
    }

    protected override void LoadViewState(Object savedState)
    {
    }

    protected override bool OnBubbleEvent(Object source, EventArgs e)
    {
      return default(bool);
    }

    protected virtual new void OnCancelCommand(DataListCommandEventArgs e)
    {
    }

    protected virtual new void OnDeleteCommand(DataListCommandEventArgs e)
    {
    }

    protected virtual new void OnEditCommand(DataListCommandEventArgs e)
    {
    }

    protected internal override void OnInit(EventArgs e)
    {
    }

    protected virtual new void OnItemCommand(DataListCommandEventArgs e)
    {
    }

    protected virtual new void OnItemCreated(DataListItemEventArgs e)
    {
    }

    protected virtual new void OnItemDataBound(DataListItemEventArgs e)
    {
    }

    protected virtual new void OnUpdateCommand(DataListCommandEventArgs e)
    {
    }

    protected internal override void PrepareControlHierarchy()
    {
    }

    protected internal override void RenderContents(System.Web.UI.HtmlTextWriter writer)
    {
    }

    protected override Object SaveViewState()
    {
      return default(Object);
    }

    Style System.Web.UI.WebControls.IRepeatInfoUser.GetItemStyle(ListItemType itemType, int repeatIndex)
    {
      return default(Style);
    }

    void System.Web.UI.WebControls.IRepeatInfoUser.RenderItem(ListItemType itemType, int repeatIndex, RepeatInfo repeatInfo, System.Web.UI.HtmlTextWriter writer)
    {
    }

    protected override void TrackViewState()
    {
    }
    #endregion

    #region Properties and indexers
    public virtual new TableItemStyle AlternatingItemStyle
    {
      get
      {
        return default(TableItemStyle);
      }
    }

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

    public virtual new bool ExtractTemplateRows
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public virtual new TableItemStyle FooterStyle
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

    public override GridLines GridLines
    {
      get
      {
        return default(GridLines);
      }
      set
      {
      }
    }

    public virtual new TableItemStyle HeaderStyle
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

    public virtual new DataListItemCollection Items
    {
      get
      {
        return default(DataListItemCollection);
      }
    }

    public virtual new TableItemStyle ItemStyle
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

    public virtual new int RepeatColumns
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public virtual new RepeatDirection RepeatDirection
    {
      get
      {
        return default(RepeatDirection);
      }
      set
      {
      }
    }

    public virtual new RepeatLayout RepeatLayout
    {
      get
      {
        return default(RepeatLayout);
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

    public virtual new DataListItem SelectedItem
    {
      get
      {
        return default(DataListItem);
      }
    }

    public virtual new TableItemStyle SelectedItemStyle
    {
      get
      {
        return default(TableItemStyle);
      }
    }

    public virtual new System.Web.UI.ITemplate SelectedItemTemplate
    {
      get
      {
        return default(System.Web.UI.ITemplate);
      }
      set
      {
      }
    }

    public Object SelectedValue
    {
      get
      {
        return default(Object);
      }
    }

    public virtual new TableItemStyle SeparatorStyle
    {
      get
      {
        return default(TableItemStyle);
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

    bool System.Web.UI.WebControls.IRepeatInfoUser.HasFooter
    {
      get
      {
        return default(bool);
      }
    }

    bool System.Web.UI.WebControls.IRepeatInfoUser.HasHeader
    {
      get
      {
        return default(bool);
      }
    }

    bool System.Web.UI.WebControls.IRepeatInfoUser.HasSeparators
    {
      get
      {
        return default(bool);
      }
    }

    int System.Web.UI.WebControls.IRepeatInfoUser.RepeatedItemCount
    {
      get
      {
        return default(int);
      }
    }

    System.Collections.IEnumerable System.Web.UI.WebControls.IWizardSideBarListControl.Items
    {
      get
      {
        return default(System.Collections.IEnumerable);
      }
    }

    protected override System.Web.UI.HtmlTextWriterTag TagKey
    {
      get
      {
        return default(System.Web.UI.HtmlTextWriterTag);
      }
    }
    #endregion

    #region Events
    public event DataListCommandEventHandler CancelCommand
    {
      add
      {
      }
      remove
      {
      }
    }

    public event DataListCommandEventHandler DeleteCommand
    {
      add
      {
      }
      remove
      {
      }
    }

    public event DataListCommandEventHandler EditCommand
    {
      add
      {
      }
      remove
      {
      }
    }

    public event CommandEventHandler ItemCommand
    {
      add
      {
      }
      remove
      {
      }
    }

    public event DataListCommandEventHandler ItemCommand
    {
      add
      {
      }
      remove
      {
      }
    }

    public event DataListItemEventHandler ItemCreated
    {
      add
      {
      }
      remove
      {
      }
    }

    public event DataListItemEventHandler ItemDataBound
    {
      add
      {
      }
      remove
      {
      }
    }

    public event DataListCommandEventHandler UpdateCommand
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
    public static string SelectCommandName;
    public static string UpdateCommandName;
    #endregion
  }
}
