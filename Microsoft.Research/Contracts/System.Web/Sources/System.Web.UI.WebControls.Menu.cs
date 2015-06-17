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

// File System.Web.UI.WebControls.Menu.cs
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
  public partial class Menu : HierarchicalDataBoundControl, System.Web.UI.IPostBackEventHandler, System.Web.UI.INamingContainer
  {
    #region Methods and constructors
    protected override void AddAttributesToRender(System.Web.UI.HtmlTextWriter writer)
    {
    }

    protected internal override void CreateChildControls()
    {
    }

    public sealed override void DataBind()
    {
    }

    protected override void EnsureDataBound()
    {
    }

    public MenuItem FindItem(string valuePath)
    {
      return default(MenuItem);
    }

    protected override System.Collections.IDictionary GetDesignModeState()
    {
      return default(System.Collections.IDictionary);
    }

    protected internal override void LoadControlState(Object savedState)
    {
    }

    protected override void LoadViewState(Object state)
    {
    }

    public Menu()
    {
    }

    protected override bool OnBubbleEvent(Object source, EventArgs e)
    {
      return default(bool);
    }

    protected override void OnDataBinding(EventArgs e)
    {
    }

    protected internal override void OnInit(EventArgs e)
    {
    }

    protected virtual new void OnMenuItemClick(MenuEventArgs e)
    {
    }

    protected virtual new void OnMenuItemDataBound(MenuEventArgs e)
    {
    }

    protected internal override void OnPreRender(EventArgs e)
    {
    }

    protected internal override void PerformDataBinding()
    {
    }

    protected internal virtual new void RaisePostBackEvent(string eventArgument)
    {
    }

    protected internal override void Render(System.Web.UI.HtmlTextWriter writer)
    {
    }

    public override void RenderBeginTag(System.Web.UI.HtmlTextWriter writer)
    {
    }

    protected internal override void RenderContents(System.Web.UI.HtmlTextWriter writer)
    {
    }

    public override void RenderEndTag(System.Web.UI.HtmlTextWriter writer)
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

    protected override void SetDesignModeState(System.Collections.IDictionary data)
    {
    }

    protected void SetItemDataBound(MenuItem node, bool dataBound)
    {
    }

    protected void SetItemDataItem(MenuItem node, Object dataItem)
    {
    }

    protected void SetItemDataPath(MenuItem node, string dataPath)
    {
    }

    void System.Web.UI.IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
    {
    }

    protected override void TrackViewState()
    {
    }
    #endregion

    #region Properties and indexers
    public override System.Web.UI.ControlCollection Controls
    {
      get
      {
        return default(System.Web.UI.ControlCollection);
      }
    }

    public MenuItemBindingCollection DataBindings
    {
      get
      {
        return default(MenuItemBindingCollection);
      }
    }

    public int DisappearAfter
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public string DynamicBottomSeparatorImageUrl
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public bool DynamicEnableDefaultPopOutImage
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public int DynamicHorizontalOffset
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public Style DynamicHoverStyle
    {
      get
      {
        return default(Style);
      }
    }

    public string DynamicItemFormatString
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public System.Web.UI.ITemplate DynamicItemTemplate
    {
      get
      {
        return default(System.Web.UI.ITemplate);
      }
      set
      {
      }
    }

    public MenuItemStyle DynamicMenuItemStyle
    {
      get
      {
        return default(MenuItemStyle);
      }
    }

    public SubMenuStyle DynamicMenuStyle
    {
      get
      {
        return default(SubMenuStyle);
      }
    }

    public string DynamicPopOutImageTextFormatString
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public string DynamicPopOutImageUrl
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public MenuItemStyle DynamicSelectedStyle
    {
      get
      {
        return default(MenuItemStyle);
      }
    }

    public string DynamicTopSeparatorImageUrl
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public int DynamicVerticalOffset
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public bool IncludeStyleBlock
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public MenuItemCollection Items
    {
      get
      {
        return default(MenuItemCollection);
      }
    }

    public bool ItemWrap
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public MenuItemStyleCollection LevelMenuItemStyles
    {
      get
      {
        return default(MenuItemStyleCollection);
      }
    }

    public MenuItemStyleCollection LevelSelectedStyles
    {
      get
      {
        return default(MenuItemStyleCollection);
      }
    }

    public SubMenuStyleCollection LevelSubMenuStyles
    {
      get
      {
        return default(SubMenuStyleCollection);
      }
    }

    public int MaximumDynamicDisplayLevels
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public Orientation Orientation
    {
      get
      {
        return default(Orientation);
      }
      set
      {
      }
    }

    public char PathSeparator
    {
      get
      {
        return default(char);
      }
      set
      {
      }
    }

    public MenuRenderingMode RenderingMode
    {
      get
      {
        return default(MenuRenderingMode);
      }
      set
      {
      }
    }

    public string ScrollDownImageUrl
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public string ScrollDownText
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public string ScrollUpImageUrl
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public string ScrollUpText
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public MenuItem SelectedItem
    {
      get
      {
        return default(MenuItem);
      }
    }

    public string SelectedValue
    {
      get
      {
        return default(string);
      }
    }

    public string SkipLinkText
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public string StaticBottomSeparatorImageUrl
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public int StaticDisplayLevels
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public bool StaticEnableDefaultPopOutImage
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public Style StaticHoverStyle
    {
      get
      {
        return default(Style);
      }
    }

    public string StaticItemFormatString
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public System.Web.UI.ITemplate StaticItemTemplate
    {
      get
      {
        return default(System.Web.UI.ITemplate);
      }
      set
      {
      }
    }

    public MenuItemStyle StaticMenuItemStyle
    {
      get
      {
        return default(MenuItemStyle);
      }
    }

    public SubMenuStyle StaticMenuStyle
    {
      get
      {
        return default(SubMenuStyle);
      }
    }

    public string StaticPopOutImageTextFormatString
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public string StaticPopOutImageUrl
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public MenuItemStyle StaticSelectedStyle
    {
      get
      {
        return default(MenuItemStyle);
      }
    }

    public Unit StaticSubMenuIndent
    {
      get
      {
        return default(Unit);
      }
      set
      {
      }
    }

    public string StaticTopSeparatorImageUrl
    {
      get
      {
        return default(string);
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

    public string Target
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }
    #endregion

    #region Events
    public event MenuEventHandler MenuItemClick
    {
      add
      {
      }
      remove
      {
      }
    }

    public event MenuEventHandler MenuItemDataBound
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
    public readonly static string MenuItemClickCommandName;
    #endregion
  }
}
