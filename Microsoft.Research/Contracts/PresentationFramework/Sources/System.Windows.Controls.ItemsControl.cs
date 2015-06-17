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

// File System.Windows.Controls.ItemsControl.cs
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


namespace System.Windows.Controls
{
  public partial class ItemsControl : Control, System.Windows.Markup.IAddChild, MS.Internal.Controls.IGeneratorHost
  {
    #region Methods and constructors
    protected virtual new void AddChild(Object value)
    {
    }

    protected virtual new void AddText(string text)
    {
    }

    public override void BeginInit()
    {
    }

    protected virtual new void ClearContainerForItemOverride(System.Windows.DependencyObject element, Object item)
    {
    }

    public System.Windows.DependencyObject ContainerFromElement(System.Windows.DependencyObject element)
    {
      return default(System.Windows.DependencyObject);
    }

    public static System.Windows.DependencyObject ContainerFromElement(System.Windows.Controls.ItemsControl itemsControl, System.Windows.DependencyObject element)
    {
      return default(System.Windows.DependencyObject);
    }

    public override void EndInit()
    {
    }

    public static int GetAlternationIndex(System.Windows.DependencyObject element)
    {
      return default(int);
    }

    protected virtual new System.Windows.DependencyObject GetContainerForItemOverride()
    {
      return default(System.Windows.DependencyObject);
    }

    public static System.Windows.Controls.ItemsControl GetItemsOwner(System.Windows.DependencyObject element)
    {
      return default(System.Windows.Controls.ItemsControl);
    }

    protected virtual new bool IsItemItsOwnContainerOverride(Object item)
    {
      return default(bool);
    }

    public ItemsControl()
    {
    }

    public static System.Windows.Controls.ItemsControl ItemsControlFromItemContainer(System.Windows.DependencyObject container)
    {
      return default(System.Windows.Controls.ItemsControl);
    }

    void MS.Internal.Controls.IGeneratorHost.ClearContainerForItem(System.Windows.DependencyObject container, Object item)
    {
    }

    System.Windows.DependencyObject MS.Internal.Controls.IGeneratorHost.GetContainerForItem(Object item)
    {
      return default(System.Windows.DependencyObject);
    }

    GroupStyle MS.Internal.Controls.IGeneratorHost.GetGroupStyle(System.Windows.Data.CollectionViewGroup group, int level)
    {
      return default(GroupStyle);
    }

    bool MS.Internal.Controls.IGeneratorHost.IsHostForItemContainer(System.Windows.DependencyObject container)
    {
      return default(bool);
    }

    bool MS.Internal.Controls.IGeneratorHost.IsItemItsOwnContainer(Object item)
    {
      return default(bool);
    }

    void MS.Internal.Controls.IGeneratorHost.PrepareItemContainer(System.Windows.DependencyObject container, Object item)
    {
    }

    void MS.Internal.Controls.IGeneratorHost.SetIsGrouping(bool isGrouping)
    {
    }

    protected virtual new void OnAlternationCountChanged(int oldAlternationCount, int newAlternationCount)
    {
    }

    protected virtual new void OnDisplayMemberPathChanged(string oldDisplayMemberPath, string newDisplayMemberPath)
    {
    }

    protected virtual new void OnGroupStyleSelectorChanged(GroupStyleSelector oldGroupStyleSelector, GroupStyleSelector newGroupStyleSelector)
    {
    }

    protected virtual new void OnItemBindingGroupChanged(System.Windows.Data.BindingGroup oldItemBindingGroup, System.Windows.Data.BindingGroup newItemBindingGroup)
    {
    }

    protected virtual new void OnItemContainerStyleChanged(System.Windows.Style oldItemContainerStyle, System.Windows.Style newItemContainerStyle)
    {
    }

    protected virtual new void OnItemContainerStyleSelectorChanged(StyleSelector oldItemContainerStyleSelector, StyleSelector newItemContainerStyleSelector)
    {
    }

    protected virtual new void OnItemsChanged(System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
    }

    protected virtual new void OnItemsPanelChanged(ItemsPanelTemplate oldItemsPanel, ItemsPanelTemplate newItemsPanel)
    {
    }

    protected virtual new void OnItemsSourceChanged(System.Collections.IEnumerable oldValue, System.Collections.IEnumerable newValue)
    {
    }

    protected virtual new void OnItemStringFormatChanged(string oldItemStringFormat, string newItemStringFormat)
    {
    }

    protected virtual new void OnItemTemplateChanged(System.Windows.DataTemplate oldItemTemplate, System.Windows.DataTemplate newItemTemplate)
    {
    }

    protected virtual new void OnItemTemplateSelectorChanged(DataTemplateSelector oldItemTemplateSelector, DataTemplateSelector newItemTemplateSelector)
    {
    }

    protected override void OnKeyDown(System.Windows.Input.KeyEventArgs e)
    {
    }

    protected override void OnTextInput(System.Windows.Input.TextCompositionEventArgs e)
    {
    }

    protected virtual new void PrepareContainerForItemOverride(System.Windows.DependencyObject element, Object item)
    {
    }

    protected virtual new bool ShouldApplyItemContainerStyle(System.Windows.DependencyObject container, Object item)
    {
      return default(bool);
    }

    public bool ShouldSerializeGroupStyle()
    {
      Contract.Requires(this.GroupStyle != null);
      Contract.Ensures(Contract.Result<bool>() == (this.GroupStyle.Count > 0));

      return default(bool);
    }

    public bool ShouldSerializeItems()
    {
      Contract.Ensures(Contract.Result<bool>() == this.HasItems);

      return default(bool);
    }

    void System.Windows.Markup.IAddChild.AddChild(Object value)
    {
    }

    void System.Windows.Markup.IAddChild.AddText(string text)
    {
    }

    #endregion

    #region Properties and indexers
    public int AlternationCount
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0);
        return default(int);
      }
      set
      {
      }
    }

    public string DisplayMemberPath
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public System.Collections.ObjectModel.ObservableCollection<GroupStyle> GroupStyle
    {
      get
      {
        return default(System.Collections.ObjectModel.ObservableCollection<GroupStyle>);
      }
    }

    public GroupStyleSelector GroupStyleSelector
    {
      get
      {
        return default(GroupStyleSelector);
      }
      set
      {
      }
    }

    public bool HasItems
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsGrouping
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsTextSearchCaseSensitive
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool IsTextSearchEnabled
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public System.Windows.Data.BindingGroup ItemBindingGroup
    {
      get
      {
        return default(System.Windows.Data.BindingGroup);
      }
      set
      {
      }
    }

    public ItemContainerGenerator ItemContainerGenerator
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Controls.ItemContainerGenerator>() != null);

        return default(ItemContainerGenerator);
      }
    }

    public System.Windows.Style ItemContainerStyle
    {
      get
      {
        return default(System.Windows.Style);
      }
      set
      {
      }
    }

    public StyleSelector ItemContainerStyleSelector
    {
      get
      {
        return default(StyleSelector);
      }
      set
      {
      }
    }

    public ItemCollection Items
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Controls.ItemCollection>() != null);

        return default(ItemCollection);
      }
    }

    public ItemsPanelTemplate ItemsPanel
    {
      get
      {
        return default(ItemsPanelTemplate);
      }
      set
      {
      }
    }

    public System.Collections.IEnumerable ItemsSource
    {
      get
      {
        return default(System.Collections.IEnumerable);
      }
      set
      {
      }
    }

    public string ItemStringFormat
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public System.Windows.DataTemplate ItemTemplate
    {
      get
      {
        return default(System.Windows.DataTemplate);
      }
      set
      {
      }
    }

    public DataTemplateSelector ItemTemplateSelector
    {
      get
      {
        return default(DataTemplateSelector);
      }
      set
      {
      }
    }

    internal protected override System.Collections.IEnumerator LogicalChildren
    {
      get
      {
        return default(System.Collections.IEnumerator);
      }
    }

    int MS.Internal.Controls.IGeneratorHost.AlternationCount
    {
      get
      {
        return default(int);
      }
    }

    ItemCollection MS.Internal.Controls.IGeneratorHost.View
    {
      get
      {
        return default(ItemCollection);
      }
    }
    #endregion

    #region Fields
    public readonly static System.Windows.DependencyProperty AlternationCountProperty;
    public readonly static System.Windows.DependencyProperty AlternationIndexProperty;
    public readonly static System.Windows.DependencyProperty DisplayMemberPathProperty;
    public readonly static System.Windows.DependencyProperty GroupStyleSelectorProperty;
    public readonly static System.Windows.DependencyProperty HasItemsProperty;
    public readonly static System.Windows.DependencyProperty IsGroupingProperty;
    public readonly static System.Windows.DependencyProperty IsTextSearchCaseSensitiveProperty;
    public readonly static System.Windows.DependencyProperty IsTextSearchEnabledProperty;
    public readonly static System.Windows.DependencyProperty ItemBindingGroupProperty;
    public readonly static System.Windows.DependencyProperty ItemContainerStyleProperty;
    public readonly static System.Windows.DependencyProperty ItemContainerStyleSelectorProperty;
    public readonly static System.Windows.DependencyProperty ItemsPanelProperty;
    public readonly static System.Windows.DependencyProperty ItemsSourceProperty;
    public readonly static System.Windows.DependencyProperty ItemStringFormatProperty;
    public readonly static System.Windows.DependencyProperty ItemTemplateProperty;
    public readonly static System.Windows.DependencyProperty ItemTemplateSelectorProperty;
    #endregion
  }
}
