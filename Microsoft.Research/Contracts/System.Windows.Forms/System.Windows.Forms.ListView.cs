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

using System;
using System.Collections;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Diagnostics.Contracts;

namespace System.Windows.Forms
{
  // Summary:
  //     Represents a Windows list view control, which displays a collection of items
  //     that can be displayed using one of four different views.
  //[DefaultProperty("Items")]
  //[Designer("System.Windows.Forms.Design.ListViewDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
  //[DefaultEvent("SelectedIndexChanged")]
  //[ComVisible(true)]
  //[ClassInterface(ClassInterfaceType.AutoDispatch)]
  //[Docking(DockingBehavior.Ask)]
  public class ListView //: Control
  {
    // Summary:
    //     Initializes a new instance of the System.Windows.Forms.ListView class.
    // public ListView();

    // Summary:
    //     Gets or sets the type of action the user must take to activate an item.
    //
    // Returns:
    //     One of the System.Windows.Forms.ItemActivation values. The default is System.Windows.Forms.ItemActivation.Standard.
    //
    // Exceptions:
    //   System.ComponentModel.InvalidEnumArgumentException:
    //     The value specified is not one of the System.Windows.Forms.ItemActivation
    //     members.
    //public ItemActivation Activation { get; set; }
    //
    // Summary:
    //     Gets or sets the alignment of items in the control.
    //
    // Returns:
    //     One of the System.Windows.Forms.ListViewAlignment values. The default is
    //     System.Windows.Forms.ListViewAlignment.Top.
    //
    // Exceptions:
    //   System.ComponentModel.InvalidEnumArgumentException:
    //     The value specified is not one of the System.Windows.Forms.ListViewAlignment
    //     values.
    //[Localizable(true)]
    //public ListViewAlignment Alignment { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether the user can drag column headers
    //     to reorder columns in the control.
    //
    // Returns:
    //     true if drag-and-drop column reordering is allowed; otherwise, false. The
    //     default is false.
    //[DefaultValue(false)]
    //public bool AllowColumnReorder { get; set; }
    //
    // Summary:
    //     Gets or sets whether icons are automatically kept arranged.
    //
    // Returns:
    //     true if icons are automatically kept arranged and snapped to the grid; otherwise,
    //     false. The default is true.
    //[DefaultValue(true)]
    //public bool AutoArrange { get; set; }
    //
    // Summary:
    //     Gets or sets the background color.
    //
    // Returns:
    //     The System.Drawing.Color of the background.
    
    //public override System.Drawing.Color BackColor { get; set; }
    //
    // Summary:
    //     Gets or sets an System.Windows.Forms.ImageLayout value.
    //
    // Returns:
    //     One of the System.Windows.Forms.ImageLayout values.
    //
    // Exceptions:
    //   System.ComponentModel.InvalidEnumArgumentException:
    //     The value specified is not one of the System.Windows.Forms.ImageLayout values.
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //[Browsable(false)]
    //public override ImageLayout BackgroundImageLayout { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether the background image of the System.Windows.Forms.ListView
    //     should be tiled.
    //
    // Returns:
    //     true if the background image of the System.Windows.Forms.ListView should
    //     be tiled; otherwise, false. The default is false.
    //[DefaultValue(false)]
    //public bool BackgroundImageTiled { get; set; }
    //
    // Summary:
    //     Gets or sets the border style of the control.
    //
    // Returns:
    //     One of the System.Windows.Forms.BorderStyle values. The default is System.Windows.Forms.BorderStyle.Fixed3D.
    //
    // Exceptions:
    //   System.ComponentModel.InvalidEnumArgumentException:
    //     The value specified is not one of the System.Windows.Forms.BorderStyle values.
    //[DispId(-504)]
    //public BorderStyle BorderStyle { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether a check box appears next to each
    //     item in the control.
    //
    // Returns:
    //     true if a check box appears next to each item in the System.Windows.Forms.ListView
    //     control; otherwise, false. The default is false.
    //[DefaultValue(false)]
    //public bool CheckBoxes { get; set; }
    //
    // Summary:
    //     Gets the indexes of the currently checked items in the control.
    //
    // Returns:
    //     A System.Windows.Forms.ListView.CheckedIndexCollection that contains the
    //     indexes of the currently checked items. If no items are currently checked,
    //     an empty System.Windows.Forms.ListView.CheckedIndexCollection is returned.
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //[Browsable(false)]
    //public ListView.CheckedIndexCollection CheckedIndices { get; }
    //
    // Summary:
    //     Gets the currently checked items in the control.
    //
    // Returns:
    //     A System.Windows.Forms.ListView.CheckedListViewItemCollection that contains
    //     the currently checked items. If no items are currently checked, an empty
    //     System.Windows.Forms.ListView.CheckedListViewItemCollection is returned.
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //[Browsable(false)]
    //public ListView.CheckedListViewItemCollection CheckedItems { get; }
    //
    // Summary:
    //     Gets the collection of all column headers that appear in the control.
    //
    // Returns:
    //     A System.Windows.Forms.ListView.ColumnHeaderCollection that represents the
    //     column headers that appear when the System.Windows.Forms.ListView.View property
    //     is set to System.Windows.Forms.View.Details.
    //[Localizable(true)]
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    //[MergableProperty(false)]
    public ListView.ColumnHeaderCollection Columns 
    { 
      get 
      {
        Contract.Ensures(Contract.Result<ListView.ColumnHeaderCollection>() != null);

        return default(ListView.ColumnHeaderCollection);
      }
    }
    //
    // Summary:
    //     This property is not relevant for this class.
    //protected override CreateParams CreateParams { get; }
    //
    //
    // Returns:
    //     The default System.Drawing.Size of the control.
    //protected override System.Drawing.Size DefaultSize { get; }
    //
    //
    // Returns:
    //     true if the surface of the control should be drawn using double buffering;
    //     otherwise, false.
    //protected override bool DoubleBuffered { get; set; }
    //
    // Summary:
    //     Gets the item in the control that currently has focus.
    //
    // Returns:
    //     A System.Windows.Forms.ListViewItem that represents the item that has focus,
    //     or null if no item has the focus in the System.Windows.Forms.ListView.
    //[Browsable(false)]
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //public ListViewItem FocusedItem { get; set; }
    //
    // Summary:
    //     Gets or sets the foreground color.
    //
    // Returns:
    //     The System.Drawing.Color that is the foreground color.
    //public override System.Drawing.Color ForeColor { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether clicking an item selects all its
    //     subitems.
    //
    // Returns:
    //     true if clicking an item selects the item and all its subitems; false if
    //     clicking an item selects only the item itself. The default is false.
    //[DefaultValue(false)]
    //public bool FullRowSelect { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether grid lines appear between the rows
    //     and columns containing the items and subitems in the control.
    //
    // Returns:
    //     true if grid lines are drawn around items and subitems; otherwise, false.
    //     The default is false.
    //[DefaultValue(false)]
    //public bool GridLines { get; set; }
    //
    // Summary:
    //     Gets the collection of System.Windows.Forms.ListViewGroup objects assigned
    //     to the control.
    //
    // Returns:
    //     A System.Windows.Forms.ListViewGroupCollection that contains all the groups
    //     in the System.Windows.Forms.ListView control.
    //[Localizable(true)]
    //[MergableProperty(false)]
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    //public ListViewGroupCollection Groups { get; }
    //
    // Summary:
    //     Gets or sets the column header style.
    //
    // Returns:
    //     One of the System.Windows.Forms.ColumnHeaderStyle values. The default is
    //     System.Windows.Forms.ColumnHeaderStyle.Clickable.
    //
    // Exceptions:
    //   System.ComponentModel.InvalidEnumArgumentException:
    //     The value specified is not one of the System.Windows.Forms.ColumnHeaderStyle
    //     values.
    //public ColumnHeaderStyle HeaderStyle { get; set; }
    ////
    // Summary:
    //     Gets or sets a value indicating whether the selected item in the control
    //     remains highlighted when the control loses focus.
    //
    // Returns:
    //     true if the selected item does not appear highlighted when the control loses
    //     focus; false if the selected item still appears highlighted when the control
    //     loses focus. The default is true.
    //[DefaultValue(true)]
    //public bool HideSelection { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether the text of an item or subitem has
    //     the appearance of a hyperlink when the mouse pointer passes over it.
    //
    // Returns:
    //     true if the item text has the appearance of a hyperlink when the mouse passes
    //     over it; otherwise, false. The default is false.
    //[DefaultValue(false)]
    //public bool HotTracking { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether an item is automatically selected
    //     when the mouse pointer remains over the item for a few seconds.
    //
    // Returns:
    //     true if an item is automatically selected when the mouse pointer hovers over
    //     it; otherwise, false. The default is false.
    //[DefaultValue(false)]
    //public bool HoverSelection { get; set; }
    //
    // Summary:
    //     Gets an object used to indicate the expected drop location when an item is
    //     dragged within a System.Windows.Forms.ListView control.
    //
    // Returns:
    //     A System.Windows.Forms.ListViewInsertionMark object representing the insertion
    //     mark.
    //[Browsable(false)]
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //public ListViewInsertionMark InsertionMark { get; }
    //
    // Summary:
    //     Gets a collection containing all items in the control.
    //
    // Returns:
    //     A System.Windows.Forms.ListView.ListViewItemCollection that contains all
    //     the items in the System.Windows.Forms.ListView control.
    //[Localizable(true)]
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    //[MergableProperty(false)]
    public ListView.ListViewItemCollection Items 
    { get
    {
      Contract.Ensures(Contract.Result<ListView.ListViewItemCollection>() != null);

      return default(ListView.ListViewItemCollection);
    }
    }

    //
    // Summary:
    //     Gets or sets a value indicating whether the user can edit the labels of items
    //     in the control.
    //
    // Returns:
    //     true if the user can edit the labels of items at run time; otherwise, false.
    //     The default is false.
    //[DefaultValue(false)]
    //public bool LabelEdit { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether item labels wrap when items are displayed
    //     in the control as icons.
    //
    // Returns:
    //     true if item labels wrap when items are displayed as icons; otherwise, false.
    //     The default is true.
    //[DefaultValue(true)]
    //[Localizable(true)]
    //public bool LabelWrap { get; set; }
    //
    // Summary:
    //     Gets or sets the System.Windows.Forms.ImageList to use when displaying items
    //     as large icons in the control.
    //
    // Returns:
    //     An System.Windows.Forms.ImageList that contains the icons to use when the
    //     System.Windows.Forms.ListView.View property is set to System.Windows.Forms.View.LargeIcon.
    //     The default is null.
    //[DefaultValue("")]
    //public ImageList LargeImageList { get; set; }
    //
    // Summary:
    //     Gets or sets the sorting comparer for the control.
    //
    // Returns:
    //     An System.Collections.IComparer that represents the sorting comparer for
    //     the control.
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //[Browsable(false)]
    //public IComparer ListViewItemSorter { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether multiple items can be selected.
    //
    // Returns:
    //     true if multiple items in the control can be selected at one time; otherwise,
    //     false. The default is true.
    //[DefaultValue(true)]
    //public bool MultiSelect { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether the System.Windows.Forms.ListView
    //     control is drawn by the operating system or by code that you provide.
    //
    // Returns:
    //     true if the System.Windows.Forms.ListView control is drawn by code that you
    //     provide; false if the System.Windows.Forms.ListView control is drawn by the
    //     operating system. The default is false.
    //[DefaultValue(false)]
    //public bool OwnerDraw { get; set; }
    //
    // Summary:
    //     Gets or sets the space between the System.Windows.Forms.ListView control
    //     and its contents.
    //
    // Returns:
    //     The System.Windows.Forms.Padding that specifies the space between the System.Windows.Forms.ListView
    //     control and its contents.
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //[Browsable(false)]
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //public Padding Padding { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether the control is laid out from right
    //     to left.
    //
    // Returns:
    //     true to indicate the System.Windows.Forms.ListView control is laid out from
    //     right to left; otherwise, false.
    //[DefaultValue(false)]
    //[Localizable(true)]
    //public virtual bool RightToLeftLayout { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether a scroll bar is added to the control
    //     when there is not enough room to display all items.
    //
    // Returns:
    //     true if scroll bars are added to the control when necessary to allow the
    //     user to see all the items; otherwise, false. The default is true.
    //[DefaultValue(true)]
    //public bool Scrollable { get; set; }
    //
    // Summary:
    //     Gets the indexes of the selected items in the control.
    //
    // Returns:
    //     A System.Windows.Forms.ListView.SelectedIndexCollection that contains the
    //     indexes of the selected items. If no items are currently selected, an empty
    //     System.Windows.Forms.ListView.SelectedIndexCollection is returned.
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //[Browsable(false)]
    //public ListView.SelectedIndexCollection SelectedIndices { get; }
    //
    // Summary:
    //     Gets the items that are selected in the control.
    //
    // Returns:
    //     A System.Windows.Forms.ListView.SelectedListViewItemCollection that contains
    //     the items that are selected in the control. If no items are currently selected,
    //     an empty System.Windows.Forms.ListView.SelectedListViewItemCollection is
    //     returned.
    //[Browsable(false)]
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //public ListView.SelectedListViewItemCollection SelectedItems { get; }
    //
    // Summary:
    //     Gets or sets a value indicating whether items are displayed in groups.
    //
    // Returns:
    //     true to display items in groups; otherwise, false. The default value is true.
    //[DefaultValue(true)]
    //public bool ShowGroups { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether ToolTips are shown for the System.Windows.Forms.ListViewItem
    //     objects contained in the System.Windows.Forms.ListView.
    //
    // Returns:
    //     true if System.Windows.Forms.ListViewItem ToolTips should be shown; otherwise,
    //     false. The default is true.
    //[DefaultValue(false)]
    //public bool ShowItemToolTips { get; set; }
    //
    // Summary:
    //     Gets or sets the System.Windows.Forms.ImageList to use when displaying items
    //     as small icons in the control.
    //
    // Returns:
    //     An System.Windows.Forms.ImageList that contains the icons to use when the
    //     System.Windows.Forms.ListView.View property is set to System.Windows.Forms.View.SmallIcon.
    //     The default is null.
    //[DefaultValue("")]
    //public ImageList SmallImageList { get; set; }
    //
    // Summary:
    //     Gets or sets the sort order for items in the control.
    //
    // Returns:
    //     One of the System.Windows.Forms.SortOrder values. The default is System.Windows.Forms.SortOrder.None.
    //
    // Exceptions:
    //   System.ComponentModel.InvalidEnumArgumentException:
    //     The value specified is not one of the System.Windows.Forms.SortOrder values.
    //public SortOrder Sortcing { get; set; }
    //
    // Summary:
    //     Gets or sets the System.Windows.Forms.ImageList associated with application-defined
    //     states in the control.
    //
    // Returns:
    //     An System.Windows.Forms.ImageList that contains a set of state images that
    //     can be used to indicate an application-defined state of an item. The default
    //     is null.
    //[DefaultValue("")]
    //public ImageList StateImageList { get; set; }
    //
    // Summary:
    //     This property is not relevant for this class.
    //
    // Returns:
    //     The text to display in the System.Windows.Forms.ListView control.
    //[Browsable(false)]
    //[Bindable(false)]
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //public override string Text { get; set; }
    //
    // Summary:
    //     Gets or sets the size of the tiles shown in tile view.
    //
    // Returns:
    //     A System.Drawing.Size that contains the new tile size.
    //[Browsable(true)]
    //public System.Drawing.Size TileSize { get; set; }
    //
    // Summary:
    //     Gets or sets the first visible item in the control.
    //
    // Returns:
    //     A System.Windows.Forms.ListViewItem that represents the first visible item
    //     in the control.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     The System.Windows.Forms.ListView.View property is set to System.Windows.Forms.View.LargeIcon,
    //     System.Windows.Forms.View.SmallIcon, or System.Windows.Forms.View.Tile.
    //[Browsable(false)]
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //public ListViewItem TopItem { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether the System.Windows.Forms.ListView
    //     uses state image behavior that is compatible with the .NET Framework 1.1
    //     or the .NET Framework 2.0.
    //
    // Returns:
    //     true if the state image behavior is compatible with the .NET Framework 1.1;
    //     false if the behavior is compatible with the .NET Framework 2.0. The default
    //     is true.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    //[Browsable(false)]
    //[DefaultValue(true)]
    //public bool UseCompatibleStateImageBehavior { get; set; }
    //
    // Summary:
    //     Gets or sets how items are displayed in the control.
    //
    // Returns:
    //     One of the System.Windows.Forms.View values. The default is System.Windows.Forms.View.LargeIcon.
    //
    // Exceptions:
    //   System.ComponentModel.InvalidEnumArgumentException:
    //     The value specified is not one of the System.Windows.Forms.View values.
    //public View View { get; set; }
    //
    // Summary:
    //     Gets or sets the number of System.Windows.Forms.ListViewItem objects contained
    //     in the list when in virtual mode.
    //
    // Returns:
    //     The number of System.Windows.Forms.ListViewItem objects contained in the
    //     System.Windows.Forms.ListView when in virtual mode.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     System.Windows.Forms.ListView.VirtualListSize is set to a value less than
    //     0.
    //
    //   System.InvalidOperationException:
    //     System.Windows.Forms.ListView.VirtualMode is set to true, System.Windows.Forms.ListView.VirtualListSize
    //     is greater than 0, and System.Windows.Forms.ListView.RetrieveVirtualItem
    //     is not handled.
    //[RefreshProperties(RefreshProperties.Repaint)]
    //[DefaultValue(0)]
    //public int VirtualListSize { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether you have provided your own data-management
    //     operations for the System.Windows.Forms.ListView control.
    //
    // Returns:
    //     true if System.Windows.Forms.ListView uses data-management operations that
    //     you provide; otherwise, false. The default is false.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     System.Windows.Forms.ListView.VirtualMode is set to true and one of the following
    //     conditions exist:System.Windows.Forms.ListView.VirtualListSize is greater
    //     than 0 and System.Windows.Forms.ListView.RetrieveVirtualItem is not handled.-or-System.Windows.Forms.ListView.Items,
    //     System.Windows.Forms.ListView.CheckedItems, or System.Windows.Forms.ListView.SelectedItems
    //     contains items.-or-Edits are made to System.Windows.Forms.ListView.Items.
    //[RefreshProperties(RefreshProperties.Repaint)]
    //[DefaultValue(false)]
    //public bool VirtualMode { get; set; }

    // Summary:
    //     Occurs when the label for an item is edited by the user.
    //public event LabelEditEventHandler AfterLabelEdit;
    //
    // Summary:
    //     Occurs when the System.Windows.Forms.ListView.BackgroundImageLayout property
    //     changes.
    //[Browsable(false)]
    //[EditorBrowsable(EditorBrowsableState.Never)]
    ////public event EventHandler BackgroundImageLayoutChanged;
    //
    // Summary:
    //     Occurs when the user starts editing the label of an item.
    //public event LabelEditEventHandler BeforeLabelEdit;
    //
    // Summary:
    //     Occurs when the contents of the display area for a System.Windows.Forms.ListView
    //     in virtual mode has changed, and the System.Windows.Forms.ListView determines
    //     that a new range of items is needed.
    //public event CacheVirtualItemsEventHandler CacheVirtualItems;
    //
    // Summary:
    //     Occurs when the user clicks a column header within the list view control.
    //public event ColumnClickEventHandler ColumnClick;
    //
    // Summary:
    //     Occurs when the column header order is changed.
    //public event ColumnReorderedEventHandler ColumnReordered;
    //
    // Summary:
    //     Occurs after the width of a column is successfully changed.
    //public event ColumnWidthChangedEventHandler ColumnWidthChanged;
    //
    // Summary:
    //     Occurs when the width of a column is changing.
    //public event ColumnWidthChangingEventHandler ColumnWidthChanging;
    //
    // Summary:
    //     Occurs when the details view of a System.Windows.Forms.ListView is drawn
    //     and the System.Windows.Forms.ListView.OwnerDraw property is set to true.
    //public event DrawListViewColumnHeaderEventHandler DrawColumnHeader;
    //
    // Summary:
    //     Occurs when a System.Windows.Forms.ListView is drawn and the System.Windows.Forms.ListView.OwnerDraw
    //     property is set to true.
    //public event DrawListViewItemEventHandler DrawItem;
    //
    // Summary:
    //     Occurs when the details view of a System.Windows.Forms.ListView is drawn
    //     and the System.Windows.Forms.ListView.OwnerDraw property is set to true.
    //public event DrawListViewSubItemEventHandler DrawSubItem;
    //
    // Summary:
    //     Occurs when an item is activated.
    //public event EventHandler ItemActivate;
    //
    // Summary:
    //     Occurs when the check state of an item changes.
    //public event ItemCheckEventHandler ItemCheck;
    //
    // Summary:
    //     Occurs when the checked state of an item changes.
    //public event ItemCheckedEventHandler ItemChecked;
    //
    // Summary:
    //     Occurs when the user begins dragging an item.
    //public event ItemDragEventHandler ItemDrag;
    //
    // Summary:
    //     Occurs when the mouse hovers over an item.
    //public event ListViewItemMouseHoverEventHandler ItemMouseHover;
    //
    // Summary:
    //     Occurs when the selection state of an item changes.
    //public event ListViewItemSelectionChangedEventHandler ItemSelectionChanged;
    //
    // Summary:
    //     Occurs when the value of the System.Windows.Forms.ListView.Padding property
    //     changes.
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //[Browsable(false)]
    //public event EventHandler PaddingChanged;
    //
    // Summary:
    //     Occurs when the System.Windows.Forms.ListView control is painted.
    //[Browsable(false)]
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //public event PaintEventHandler Paint;
    //
    // Summary:
    //     Occurs when the System.Windows.Forms.ListView is in virtual mode and requires
    //     a System.Windows.Forms.ListViewItem.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     The System.Windows.Forms.RetrieveVirtualItemEventArgs.Item property is not
    //     set to an item when the System.Windows.Forms.ListView.RetrieveVirtualItem
    //     event is handled.
    //public event RetrieveVirtualItemEventHandler RetrieveVirtualItem;
    //
    // Summary:
    //     Occurs when the value of the System.Windows.Forms.ListView.RightToLeftLayout
    //     property changes.
    //public event EventHandler RightToLeftLayoutChanged;
    //
    // Summary:
    //     Occurs when the System.Windows.Forms.ListView is in virtual mode and a search
    //     is taking place.
    //public event SearchForVirtualItemEventHandler SearchForVirtualItem;
    //
    // Summary:
    //     Occurs when the System.Windows.Forms.ListView.SelectedIndices collection
    //     changes.
    //public event EventHandler SelectedIndexChanged;
    //
    // Summary:
    //     Occurs when the System.Windows.Forms.ListView.Text property changes.
    //[Browsable(false)]
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //public event EventHandler TextChanged;
    //
    // Summary:
    //     Occurs when a System.Windows.Forms.ListView is in virtual mode and the selection
    //     state of a range of items has changed.
    //public event ListViewVirtualItemsSelectionRangeChangedEventHandler VirtualItemsSelectionRangeChanged;

    // Summary:
    //     Arranges items in the control when they are displayed as icons based on the
    //     value of the System.Windows.Forms.ListView.Alignment property.
    //public void ArrangeIcons();
    //
    // Summary:
    //     Arranges items in the control when they are displayed as icons with a specified
    //     alignment setting.
    //
    // Parameters:
    //   value:
    //     One of the System.Windows.Forms.ListViewAlignment values.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The value specified in the value parameter is not a member of the System.Windows.Forms.ListViewAlignment
    //     enumeration.
    //public void ArrangeIcons(ListViewAlignment value);
    //
    // Summary:
    //     Resizes the width of the given column as indicated by the resize style.
    //
    // Parameters:
    //   columnIndex:
    //     The zero-based index of the column to resize.
    //
    //   headerAutoResize:
    //     One of the System.Windows.Forms.ColumnHeaderAutoResizeStyle values.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     columnIndex is greater than 0 when System.Windows.Forms.ListView.Columns
    //     is null-or-columnIndex is less than 0 or greater than the number of columns
    //     set.
    //
    //   System.ComponentModel.InvalidEnumArgumentException:
    //     headerAutoResize is not a member of the System.Windows.Forms.ColumnHeaderAutoResizeStyle
    //     enumeration.
    //public void AutoResizeColumn(int columnIndex, ColumnHeaderAutoResizeStyle headerAutoResize);
    //
    // Summary:
    //     Resizes the width of the columns as indicated by the resize style.
    //
    // Parameters:
    //   headerAutoResize:
    //     One of the System.Windows.Forms.ColumnHeaderAutoResizeStyle values.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     System.Windows.Forms.ListView.AutoResizeColumn(System.Int32,System.Windows.Forms.ColumnHeaderAutoResizeStyle)
    //     is called with a value other than System.Windows.Forms.ColumnHeaderAutoResizeStyle.None
    //     when System.Windows.Forms.ListView.View is not set to System.Windows.Forms.View.Details.
    //public void AutoResizeColumns(ColumnHeaderAutoResizeStyle headerAutoResize);
    //
    // Summary:
    //     Prevents the control from drawing until the System.Windows.Forms.ListView.EndUpdate()
    //     method is called.
    //public void BeginUpdate();
    //
    // Summary:
    //     Removes all items and columns from the control.
    //public void Clear();
    //
    // Summary:
    //     Overrides System.Windows.Forms.Control.CreateHandle().
    //protected override void CreateHandle();
    //
    // Summary:
    //     Releases the unmanaged resources used by the System.Windows.Forms.ListView
    //     and optionally releases the managed resources.
    //
    // Parameters:
    //   disposing:
    //     true to release both managed and unmanaged resources; false to release only
    //     unmanaged resources.
    //
    //   disposing:
    //     true to release both managed and unmanaged resources; false to release only
    //     unmanaged resources.
    //protected override void Dispose(bool disposing);
    //
    // Summary:
    //     Resumes drawing of the list view control after drawing is suspended by the
    //     System.Windows.Forms.ListView.BeginUpdate() method.
    //public void EndUpdate();
    //
    // Summary:
    //     Ensures that the specified item is visible within the control, scrolling
    //     the contents of the control if necessary.
    //
    // Parameters:
    //   index:
    //     The zero-based index of the item to scroll into view.
    //public void EnsureVisible(int index);
    //
    // Summary:
    //     Finds the first System.Windows.Forms.ListViewItem that begins with the specified
    //     text value.
    //
    // Parameters:
    //   text:
    //     The text to search for.
    //
    // Returns:
    //     The first System.Windows.Forms.ListViewItem that begins with the specified
    //     text value.
    //public ListViewItem FindItemWithText(string text);
    //
    // Summary:
    //     Finds the first System.Windows.Forms.ListViewItem or System.Windows.Forms.ListViewItem.ListViewSubItem,
    //     if indicated, that begins with the specified text value. The search starts
    //     at the specified index.
    //
    // Parameters:
    //   text:
    //     The text to search for.
    //
    //   includeSubItemsInSearch:
    //     true to include subitems in the search; otherwise, false.
    //
    //   startIndex:
    //     The index of the item at which to start the search.
    //
    // Returns:
    //     The first System.Windows.Forms.ListViewItem that begins with the specified
    //     text value.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     startIndex is less 0 or more than the number items in the System.Windows.Forms.ListView.
    public ListViewItem FindItemWithText(string text, bool includeSubItemsInSearch, int startIndex)
    {
      Contract.Requires(startIndex >= 0);

      return default(ListViewItem);
    }
    //
    // Summary:
    //     Finds the first System.Windows.Forms.ListViewItem or System.Windows.Forms.ListViewItem.ListViewSubItem,
    //     if indicated, that begins with the specified text value. The search starts
    //     at the specified index.
    //
    // Parameters:
    //   text:
    //     The text to search for.
    //
    //   includeSubItemsInSearch:
    //     true to include subitems in the search; otherwise, false.
    //
    //   startIndex:
    //     The index of the item at which to start the search.
    //
    //   isPrefixSearch:
    //     true to match the search text to the prefix of an item; otherwise, false.
    //
    // Returns:
    //     The first System.Windows.Forms.ListViewItem that begins with the specified
    //     text value.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     startIndex is less 0 or more than the number of items in the System.Windows.Forms.ListView.
    public ListViewItem FindItemWithText(string text, bool includeSubItemsInSearch, int startIndex, bool isPrefixSearch)
      {
      Contract.Requires(startIndex >= 0);

      return default(ListViewItem);
    }

    //
    // Summary:
    //     Finds the next item from the given point, searching in the specified direction
    //
    // Parameters:
    //   dir:
    //     One of the System.Windows.Forms.SearchDirectionHint values.
    //
    //   point:
    //     The point at which to begin searching.
    //
    // Returns:
    //     The System.Windows.Forms.ListViewItem that is closest to the given point,
    //     searching in the specified direction.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     System.Windows.Forms.ListView.View is set to a value other than System.Windows.Forms.View.SmallIcon
    //     or System.Windows.Forms.View.LargeIcon.
//    public ListViewItem FindNearestItem(SearchDirectionHint dir, System.Drawing.Point point);
    //
    // Summary:
    //     Finds the next item from the given x- and y-coordinates, searching in the
    //     specified direction.
    //
    // Parameters:
    //   searchDirection:
    //     One of the System.Windows.Forms.SearchDirectionHint values.
    //
    //   x:
    //     The x-coordinate for the point at which to begin searching.
    //
    //   y:
    //     The y-coordinate for the point at which to begin searching.
    //
    // Returns:
    //     The System.Windows.Forms.ListViewItem that is closest to the given coordinates,
    //     searching in the specified direction.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     System.Windows.Forms.ListView.View is set to a value other than System.Windows.Forms.View.SmallIcon
    //     or System.Windows.Forms.View.LargeIcon.
 //   public ListViewItem FindNearestItem(SearchDirectionHint searchDirection, int x, int y);
    //
    // Summary:
    //     Retrieves the item at the specified location.
    //
    // Parameters:
    //   x:
    //     The x-coordinate of the location to search for an item (expressed in client
    //     coordinates).
    //
    //   y:
    //     The y-coordinate of the location to search for an item (expressed in client
    //     coordinates).
    //
    // Returns:
    //     A System.Windows.Forms.ListViewItem that represents the item at the specified
    //     position. If there is no item at the specified location, the method returns
    //     null.
//    public ListViewItem GetItemAt(int x, int y);
    //
    // Summary:
    //     Retrieves the bounding rectangle for a specific item within the list view
    //     control.
    //
    // Parameters:
    //   index:
    //     The zero-based index of the item within the System.Windows.Forms.ListView.ListViewItemCollection
    //     whose bounding rectangle you want to return.
    //
    // Returns:
    //     A System.Drawing.Rectangle that represents the bounding rectangle of the
    //     specified System.Windows.Forms.ListViewItem.
    //public System.Drawing.Rectangle GetItemRect(int index);
    //
    // Summary:
    //     Retrieves the specified portion of the bounding rectangle for a specific
    //     item within the list view control.
    //
    // Parameters:
    //   index:
    //     The zero-based index of the item within the System.Windows.Forms.ListView.ListViewItemCollection
    //     whose bounding rectangle you want to return.
    //
    //   portion:
    //     One of the System.Windows.Forms.ItemBoundsPortion values that represents
    //     a portion of the System.Windows.Forms.ListViewItem for which to retrieve
    //     the bounding rectangle.
    //
    // Returns:
    //     A System.Drawing.Rectangle that represents the bounding rectangle for the
    //     specified portion of the specified System.Windows.Forms.ListViewItem.
    //public System.Drawing.Rectangle GetItemRect(int index, ItemBoundsPortion portion);
    //
    // Summary:
    //     Provides item information, given a point.
    //
    // Parameters:
    //   point:
    //     The System.Drawing.Point at which to retrieve the item information.
    //
    // Returns:
    //     A System.Windows.Forms.ListViewHitTestInfo.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     The point contains coordinates that are less than 0.
    public ListViewHitTestInfo HitTest(System.Drawing.Point point)
    {
      Contract.Requires(point.X >= 0);
      Contract.Requires(point.Y >= 0);

      return default(ListViewHitTestInfo);
    }
    //
    // Summary:
    //     Provides item information, given x- and y-coordinates.
    //
    // Parameters:
    //   x:
    //     The x-coordinate at which to retrieve the item information.
    //
    //   y:
    //     The y-coordinate at which to retrieve the item information.
    //
    // Returns:
    //     A System.Windows.Forms.ListViewHitTestInfo.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     The x- or y-coordinate is less than 0.
    public ListViewHitTestInfo HitTest(int x, int y)
    {
      Contract.Requires(x >= 0);
      Contract.Requires(y >= 0);

      return default(ListViewHitTestInfo);
    }
    //
    //
    // Parameters:
    //   keyData:
    //     One of the System.Windows.Forms.Keys values.
    //
    // Returns:
    //     true if the specified key is a regular input key; otherwise, false.
    //protected override bool IsInputKey(Keys keyData);
    //
    // Summary:
    //     Raises the System.Windows.Forms.ListView.AfterLabelEdit event.
    //
    // Parameters:
    //   e:
    //     A System.Windows.Forms.LabelEditEventArgs that contains the event data.
    //protected virtual void OnAfterLabelEdit(LabelEditEventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.Control.BackgroundImageChanged event.
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    //protected override void OnBackgroundImageChanged(EventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.ListView.BeforeLabelEdit event.
    //
    // Parameters:
    //   e:
    //     A System.Windows.Forms.LabelEditEventArgs that contains the event data.
    //protected virtual void OnBeforeLabelEdit(LabelEditEventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.ListView.CacheVirtualItems event.
    //
    // Parameters:
    //   e:
    //     A System.Windows.Forms.CacheVirtualItemsEventArgs that contains the event
    //     data.
    //protected virtual void OnCacheVirtualItems(CacheVirtualItemsEventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.ListView.ColumnClick event.
    //
    // Parameters:
    //   e:
    //     A System.Windows.Forms.ColumnClickEventArgs that contains the event data.
    //protected virtual void OnColumnClick(ColumnClickEventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.ListView.ColumnReordered event.
    //
    // Parameters:
    //   e:
    //     The System.Windows.Forms.ColumnReorderedEventArgs that contains the event
    //     data.
    //protected virtual void OnColumnReordered(ColumnReorderedEventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.ListView.ColumnWidthChanged event.
    //
    // Parameters:
    //   e:
    //     A System.Windows.Forms.ColumnWidthChangedEventArgs that contains the event
    //     data.
    //protected virtual void OnColumnWidthChanged(ColumnWidthChangedEventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.ListView.ColumnWidthChanging event.
    //
    // Parameters:
    //   e:
    //     A System.Windows.Forms.ColumnWidthChangingEventArgs that contains the event
    //     data.
    //protected virtual void OnColumnWidthChanging(ColumnWidthChangingEventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.ListView.DrawColumnHeader event.
    //
    // Parameters:
    //   e:
    //     A System.Windows.Forms.DrawListViewColumnHeaderEventArgs that contains the
    //     event data.
    //protected virtual void OnDrawColumnHeader(DrawListViewColumnHeaderEventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.ListView.DrawItem event.
    //
    // Parameters:
    //   e:
    //     A System.Windows.Forms.DrawListViewItemEventArgs that contains the event
    //     data.
    //protected virtual void OnDrawItem(DrawListViewItemEventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.ListView.DrawSubItem event.
    //
    // Parameters:
    //   e:
    //     A System.Windows.Forms.DrawListViewSubItemEventArgs that contains the event
    //     data.
    //protected virtual void OnDrawSubItem(DrawListViewSubItemEventArgs e);
    //
    // Summary:
    //     Raises the FontChanged event.
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    //
    //   e:
    //     An System.EventArgs that contains the event data.
    //protected override void OnFontChanged(EventArgs e);
    //
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    //protected override void OnHandleCreated(EventArgs e);
    //
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    //protected override void OnHandleDestroyed(EventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.ListView.ItemActivate event.
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    //protected virtual void OnItemActivate(EventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.ListView.ItemCheck event.
    //
    // Parameters:
    //   ice:
    //     An System.Windows.Forms.ItemCheckEventArgs that contains the event data.
    //protected virtual void OnItemCheck(ItemCheckEventArgs ice);
    //
    // Summary:
    //     Raises the System.Windows.Forms.ListView.ItemChecked event.
    //
    // Parameters:
    //   e:
    //     An System.Windows.Forms.ItemCheckedEventArgs that contains the event data.
    //protected virtual void OnItemChecked(ItemCheckedEventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.ListView.ItemDrag event.
    //
    // Parameters:
    //   e:
    //     An System.Windows.Forms.ItemDragEventArgs that contains the event data.
    //protected virtual void OnItemDrag(ItemDragEventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.ListView.ItemMouseHover event.
    //
    // Parameters:
    //   e:
    //     A System.Windows.Forms.ListViewItemMouseHoverEventArgs that contains the
    //     event data.
    //protected virtual void OnItemMouseHover(ListViewItemMouseHoverEventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.ListView.ItemSelectionChanged event.
    //
    // Parameters:
    //   e:
    //     A System.Windows.Forms.ListViewItemSelectionChangedEventArgs that contains
    //     the event data.
    //protected virtual void OnItemSelectionChanged(ListViewItemSelectionChangedEventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.Control.MouseHover event.
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    //protected override void OnMouseHover(EventArgs e);
    //
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    //protected override void OnMouseLeave(EventArgs e);
    //
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    //protected override void OnParentChanged(EventArgs e);
    //
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    //protected override void OnResize(EventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.ListView.RetrieveVirtualItem event.
    //
    // Parameters:
    //   e:
    //     A System.Windows.Forms.RetrieveVirtualItemEventArgs that contains the event
    //     data.
    //protected virtual void OnRetrieveVirtualItem(RetrieveVirtualItemEventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.ListView.RightToLeftLayoutChanged event.
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    //protected virtual void OnRightToLeftLayoutChanged(EventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.ListView.SearchForVirtualItem event.
    //
    // Parameters:
    //   e:
    //     A System.Windows.Forms.SearchForVirtualItemEventArgs that contains the event
    //     data.
    //protected virtual void OnSearchForVirtualItem(SearchForVirtualItemEventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.ListView.SelectedIndexChanged event.
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    //protected virtual void OnSelectedIndexChanged(EventArgs e);
    //
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    //protected override void OnSystemColorsChanged(EventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.ListView.VirtualItemsSelectionRangeChanged
    //     event.
    //
    // Parameters:
    //   e:
    //     A System.Windows.Forms.ListViewVirtualItemsSelectionRangeChangedEventArgs
    //     that contains the event data.
    //protected virtual void OnVirtualItemsSelectionRangeChanged(ListViewVirtualItemsSelectionRangeChangedEventArgs e);
    //
    // Summary:
    //     Initializes the properties of the System.Windows.Forms.ListView control that
    //     manage the appearance of the control.
    //protected void RealizeProperties();
    //
    // Summary:
    //     Forces a range of System.Windows.Forms.ListViewItem objects to be redrawn.
    //
    // Parameters:
    //   startIndex:
    //     The index for the first item in the range to be redrawn.
    //
    //   endIndex:
    //     The index for the last item of the range to be redrawn.
    //
    //   invalidateOnly:
    //     true to invalidate the range of items; false to invalidate and repaint the
    //     items.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     startIndex or endIndex is less than 0, greater than or equal to the number
    //     of items in the System.Windows.Forms.ListView or, if in virtual mode, greater
    //     than the value of System.Windows.Forms.ListView.VirtualListSize.-or-The given
    //     startIndex is greater than the endIndex.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    public void RedrawItems(int startIndex, int endIndex, bool invalidateOnly)
    {
      Contract.Requires(startIndex >= 0);
      Contract.Requires(endIndex >= 0);
      Contract.Requires(startIndex <= endIndex);
    }
    //
    // Summary:
    //     Sorts the items of the list view.
    //public void Sort();
    //
    // Summary:
    //     Returns a string representation of the System.Windows.Forms.ListView control.
    //
    // Returns:
    //     A string that states the control type, the count of items in the System.Windows.Forms.ListView
    //     control, and the type of the first item in the System.Windows.Forms.ListView,
    //     if the count is not 0.
    //public override string ToString();
    //
    // Summary:
    //     Updates the extended styles applied to the list view control.
    //protected void UpdateExtendedStyles();
    //
    // Summary:
    //     Overrides System.Windows.Forms.Control.WndProc(System.Windows.Forms.Message@).
    //
    // Parameters:
    //   m:
    //     The Windows System.Windows.Forms.Message to process.
    //protected override void WndProc(ref Message m);

    // Summary:
    //     Represents the collection containing the indexes to the checked items in
    //     a list view control.
    //[ListBindable(false)]
    public class CheckedIndexCollection //: IList, ICollection, IEnumerable
    {
      // Summary:
      //     Initializes a new instance of the System.Windows.Forms.ListView.CheckedIndexCollection
      //     class.
      //
      // Parameters:
      //   owner:
      //     A System.Windows.Forms.ListView control that owns the collection.
      public CheckedIndexCollection(ListView owner) { }

     
    }

    // Summary:
    //     Represents the collection of checked items in a list view control.
    //[ListBindable(false)]
    public class CheckedListViewItemCollection // : IList, ICollection, IEnumerable
    {
      // Summary:
      //     Initializes a new instance of the System.Windows.Forms.ListView.CheckedListViewItemCollection
      //     class.
      //
      // Parameters:
      //   owner:
      //     The System.Windows.Forms.ListView control that owns the collection.
      //public CheckedListViewItemCollection(ListView owner);



      // Summary:
      //     Gets the item at the specified index within the collection.
      //
      // Parameters:
      //   index:
      //     The index of the item in the collection to retrieve.
      //
      // Returns:
      //     A System.Windows.Forms.ListViewItem representing the item located at the
      //     specified index within the collection.
      //
      // Exceptions:
      //   System.ArgumentOutOfRangeException:
      //     The index parameter is less than 0 or greater than or equal to the value
      //     of the System.Windows.Forms.ListView.CheckedListViewItemCollection.Count
      //     property of System.Windows.Forms.ListView.CheckedListViewItemCollection.


 
    }

    // Summary:
    //     Represents the collection of column headers in a System.Windows.Forms.ListView
    //     control.
    //[ListBindable(false)]
    public class ColumnHeaderCollection // : IList, ICollection, IEnumerable
    {
      // Summary:
      //     Initializes a new instance of the System.Windows.Forms.ListView.ColumnHeaderCollection
      //     class.
      //
      // Parameters:
      //   owner:
      //     The System.Windows.Forms.ListView that owns this collection.
      //public ColumnHeaderCollection(ListView owner);

   

      // Summary:
      //     Gets the column header at the specified index within the collection.
      //
      // Parameters:
      //   index:
      //     The index of the column header to retrieve from the collection.
      //
      // Returns:
      //     A System.Windows.Forms.ColumnHeader representing the column header located
      //     at the specified index within the collection.
      //
      // Exceptions:
      //   System.ArgumentOutOfRangeException:
      //     index is less than 0 or greater than or equal to the value of the System.Windows.Forms.ListView.ColumnHeaderCollection.Count
      //     property of the System.Windows.Forms.ListView.ColumnHeaderCollection.
      //public virtual ColumnHeader this[int index] 
      //{ 
      //  get
      //  {
      //    Contract.Requires(index >= 0);

      //    return default(ColumnHeader);
      //  }
      //}
      //
      // Summary:
      //     Gets the column header with the specified key from the collection.
      //
      // Parameters:
      //   key:
      //     The name of the column header to retrieve from the collection.
      //
      // Returns:
      //     The System.Windows.Forms.ColumnHeader with the specified key.
      //public virtual ColumnHeader this[string key] { get; }

      // Summary:
      //     Adds an existing System.Windows.Forms.ColumnHeader to the collection.
      //
      // Parameters:
      //   value:
      //     The System.Windows.Forms.ColumnHeader to add to the collection.
      //
      // Returns:
      //     The zero-based index into the collection where the item was added.
      //public virtual int Add(ColumnHeader value);
      //
      // Summary:
      //     Creates and adds a column with the specified text to the collection.
      //
      // Parameters:
      //   text:
      //     The text to display in the column header.
      //
      // Returns:
      //     The System.Windows.Forms.ColumnHeader with the specified text that was added
      //     to the System.Windows.Forms.ListView.ColumnHeaderCollection.
      //public virtual ColumnHeader Add(string text);
      //
      // Summary:
      //     Creates and adds a column with the specified text and width to the collection.
      //
      // Parameters:
      //   text:
      //     The text of the System.Windows.Forms.ColumnHeader to add to the collection.
      //
      //   width:
      //     The width of the System.Windows.Forms.ColumnHeader to add to the collection.
      //
      // Returns:
      //     The System.Windows.Forms.ColumnHeader with the specified text and width that
      //     was added to the System.Windows.Forms.ListView.ColumnHeaderCollection.
      //public virtual ColumnHeader Add(string text, int width);
      //
      // Summary:
      //     Creates and adds a column with the specified text and key to the collection.
      //
      // Parameters:
      //   key:
      //     The key of the System.Windows.Forms.ColumnHeader to add to the collection.
      //
      //   text:
      //     The text of the System.Windows.Forms.ColumnHeader to add to the collection.
      //
      // Returns:
      //     The System.Windows.Forms.ColumnHeader with the specified key and text that
      //     was added to the System.Windows.Forms.ListView.ColumnHeaderCollection.
      //public virtual ColumnHeader Add(string key, string text);
      //
      // Summary:
      //     Adds a column header to the collection with specified text, width, and alignment
      //     settings.
      //
      // Parameters:
      //   text:
      //     The text to display in the column header.
      //
      //   width:
      //     The initial width of the column header.
      //
      //   textAlign:
      //     One of the System.Windows.Forms.HorizontalAlignment values.
      //
      // Returns:
      //     The System.Windows.Forms.ColumnHeader that was created and added to the collection.
      //public virtual ColumnHeader Add(string text, int width, HorizontalAlignment textAlign);
      //
      // Summary:
      //     Creates and adds a column with the specified text, key, and width to the
      //     collection.
      //
      // Parameters:
      //   key:
      //     The key of the column header.
      //
      //   text:
      //     The text to display in the column header.
      //
      //   width:
      //     The initial width of the System.Windows.Forms.ColumnHeader.
      //
      // Returns:
      //     The System.Windows.Forms.ColumnHeader with the given text, key, and width
      //     that was added to the collection.
      //public virtual  Add(string key, string text, int width);
      //
      // Summary:
      //     Creates and adds a column with the specified key, aligned text, width, and
      //     image index to the collection.
      //
      // Parameters:
      //   key:
      //     The key of the column header.
      //
      //   text:
      //     The text to display in the column header.
      //
      //   width:
      //     The initial width of the column header.
      //
      //   textAlign:
      //     One of the System.Windows.Forms.HorizontalAlignment values.
      //
      //   imageIndex:
      //     The index value of the image to display in the column.
      //
      // Returns:
      //     The System.Windows.Forms.ColumnHeader with the specified key, aligned text,
      //     width, and image index that has been added to the collection.
      //public virtual ColumnHeader Add(string key, string text, int width, HorizontalAlignment textAlign, int imageIndex);
      //
      // Summary:
      //     Creates and adds a column with the specified key, aligned text, width, and
      //     image key to the collection.
      //
      // Parameters:
      //   key:
      //     The key of the column header.
      //
      //   text:
      //     The text to display in the column header.
      //
      //   width:
      //     The initial width of the column header.
      //
      //   textAlign:
      //     One of the System.Windows.Forms.HorizontalAlignment values.
      //
      //   imageKey:
      //     The key value of the image to display in the column header.
      //
      // Returns:
      //     The System.Windows.Forms.ColumnHeader with the specified key, aligned text,
      //     width, and image key that has been added to the collection.
      //public virtual ColumnHeader Add(string key, string text, int width, HorizontalAlignment textAlign, string imageKey);
      //
      // Summary:
      //     Adds an array of column headers to the collection.
      //
      // Parameters:
      //   values:
      //     An array of System.Windows.Forms.ColumnHeader objects to add to the collection.
      //public virtual void AddRange(ColumnHeader[] values);
      //
      // Summary:
      //     Removes all column headers from the collection.
      //public virtual void Clear();
      //
      // Summary:
      //     Determines whether the specified column header is located in the collection.
      //
      // Parameters:
      //   value:
      //     A System.Windows.Forms.ColumnHeader representing the column header to locate
      //     in the collection.
      //
      // Returns:
      //     true if the column header is contained in the collection; otherwise, false.
      //public bool Contains(ColumnHeader value);
      //
      // Summary:
      //     Determines if a column with the specified key is contained in the collection.
      //
      // Parameters:
      //   key:
      //     The name of the column to search for.
      //
      // Returns:
      //     true if a column with the specified name is contained in the collection;
      //     otherwise, false.
      //public virtual bool ContainsKey(string key);
      //
      // Summary:
      //     Returns an enumerator to use to iterate through the column header collection.
      //
      // Returns:
      //     An System.Collections.IEnumerator that represents the column header collection.
      //public IEnumerator GetEnumerator();
      //
      // Summary:
      //     Returns the index, within the collection, of the specified column header.
      //
      // Parameters:
      //   value:
      //     A System.Windows.Forms.ColumnHeader representing the column header to locate
      //     in the collection.
      //
      // Returns:
      //     The zero-based index of the column header's location in the collection. If
      //     the column header is not located in the collection, the return value is -1.
      //public int IndexOf(ColumnHeader value);
      //
      // Summary:
      //     Determines the index for a column with the specified key.
      //
      // Parameters:
      //   key:
      //     The name of the column to retrieve the index for.
      //
      // Returns:
      //     The zero-based index for the first occurrence of the column with the specified
      //     name, if found; otherwise, -1.
      //public virtual int IndexOfKey(string key);
      //
      // Summary:
      //     Inserts an existing column header into the collection at the specified index.
      //
      // Parameters:
      //   index:
      //     The zero-based index location where the column header is inserted.
      //
      //   value:
      //     The System.Windows.Forms.ColumnHeader to insert into the collection.
      //
      // Exceptions:
      //   System.ArgumentOutOfRangeException:
      //     index is less than 0 or greater than or equal to the value of the System.Windows.Forms.ListView.ColumnHeaderCollection.Count
      //     property of the System.Windows.Forms.ListView.ColumnHeaderCollection.
      //public void Insert(int index, ColumnHeader value);
      //
      // Summary:
      //     Creates a new column header with the specified text, and inserts the header
      //     into the collection at the specified index.
      //
      // Parameters:
      //   index:
      //     The zero-based index location where the column header is inserted.
      //
      //   text:
      //     The text to display in the column header.
      //
      // Exceptions:
      //   System.ArgumentOutOfRangeException:
      //     index is less than 0 or greater than or equal to the value of the System.Windows.Forms.ListView.ColumnHeaderCollection.Count
      //     property of the System.Windows.Forms.ListView.ColumnHeaderCollection.
      //public void Insert(int index, string text);
      //
      // Summary:
      //     Creates a new column header with the specified text and initial width, and
      //     inserts the header into the collection at the specified index.
      //
      // Parameters:
      //   index:
      //     The zero-based index location where the column header is inserted.
      //
      //   text:
      //     The text to display in the column header.
      //
      //   width:
      //     The initial width, in pixels, of the column header.
      //public void Insert(int index, string text, int width);
      //
      // Summary:
      //     Creates a new column header with the specified text and key, and inserts
      //     the header into the collection at the specified index.
      //
      // Parameters:
      //   index:
      //     The zero-based index location where the column header is inserted.
      //
      //   key:
      //     The name of the column header.
      //
      //   text:
      //     The text to display in the column header.
      //public void Insert(int index, string key, string text);
      //
      // Summary:
      //     Creates a new column header and inserts it into the collection at the specified
      //     index.
      //
      // Parameters:
      //   index:
      //     The zero-based index location where the column header is inserted.
      //
      //   text:
      //     The text to display in the column header.
      //
      //   width:
      //     The initial width of the column header. Set to -1 to autosize the column
      //     header to the size of the largest subitem text in the column or -2 to autosize
      //     the column header to the size of the text of the column header.
      //
      //   textAlign:
      //     One of the System.Windows.Forms.HorizontalAlignment values.
      //
      // Exceptions:
      //   System.ArgumentOutOfRangeException:
      //     index is less than 0 or greater than or equal to the value of the System.Windows.Forms.ListView.ColumnHeaderCollection.Count
      //     property of the System.Windows.Forms.ListView.ColumnHeaderCollection.
      //public void Insert(int index, string text, int width, HorizontalAlignment textAlign);
      //
      // Summary:
      //     Creates a new column header with the specified text, key, and width, and
      //     inserts the header into the collection at the specified index.
      //
      // Parameters:
      //   index:
      //     The zero-based index location where the column header is inserted.
      //
      //   key:
      //     The name of the column header.
      //
      //   text:
      //     The text to display in the column header.
      //
      //   width:
      //     The initial width, in pixels, of the column header.
      //public void Insert(int index, string key, string text, int width);
      //
      // Summary:
      //     Creates a new column header with the specified aligned text, key, width,
      //     and image index, and inserts the header into the collection at the specified
      //     index.
      //
      // Parameters:
      //   index:
      //     The zero-based index location where the column header is inserted.
      //
      //   key:
      //     The name of the column header.
      //
      //   text:
      //     The text to display in the column header.
      //
      //   width:
      //     The initial width, in pixels, of the column header.
      //
      //   textAlign:
      //     One of the System.Windows.Forms.HorizontalAlignment values.
      //
      //   imageIndex:
      //     The index of the image to display in the column header.
      //public void Insert(int index, string key, string text, int width, HorizontalAlignment textAlign, int imageIndex);
      //
      // Summary:
      //     Creates a new column header with the specified aligned text, key, width,
      //     and image key, and inserts the header into the collection at the specified
      //     index.
      //
      // Parameters:
      //   index:
      //     The zero-based index location where the column header is inserted.
      //
      //   key:
      //     The name of the column header.
      //
      //   text:
      //     The text to display in the column header.
      //
      //   width:
      //     The initial width, in pixels, of the column header.
      //
      //   textAlign:
      //     One of the System.Windows.Forms.HorizontalAlignment values.
      //
      //   imageKey:
      //     The key of the image to display in the column header.
      //public void Insert(int index, string key, string text, int width, HorizontalAlignment textAlign, string imageKey);
      //
      // Summary:
      //     Removes the specified column header from the collection.
      //
      // Parameters:
      //   column:
      //     A System.Windows.Forms.ColumnHeader representing the column header to remove
      //     from the collection.
      //public virtual void Remove(ColumnHeader column);
      //
      // Summary:
      //     Removes the column header at the specified index within the collection.
      //
      // Parameters:
      //   index:
      //     The zero-based index of the column header to remove.
      //
      // Exceptions:
      //   System.ArgumentOutOfRangeException:
      //     index is less than 0 or greater than or equal to the value of the System.Windows.Forms.ListView.ColumnHeaderCollection.Count
      //     property of the System.Windows.Forms.ListView.ColumnHeaderCollection.
      //public virtual void RemoveAt(int index);
      //
      // Summary:
      //     Removes the column with the specified key from the collection.
      //
      // Parameters:
      //   key:
      //     The name of the column to remove from the collection.
      //public virtual void RemoveByKey(string key);
    }

    // Summary:
    //     Represents the collection of items in a System.Windows.Forms.ListView control
    //     or assigned to a System.Windows.Forms.ListViewGroup.
    //[ListBindable(false)]
    public class ListViewItemCollection // : IList, ICollection, IEnumerable
    {
      // Summary:
      //     Initializes a new instance of the System.Windows.Forms.ListView.ListViewItemCollection
      //     class.
      //
      // Parameters:
      //   owner:
      //     The System.Windows.Forms.ListView that owns the collection.
      //public ListViewItemCollection(ListView owner);

     
      // Summary:
      //     Gets or sets the item at the specified index within the collection.
      //
      // Parameters:
      //   index:
      //     The index of the item in the collection to get or set.
      //
      // Returns:
      //     A System.Windows.Forms.ListViewItem representing the item located at the
      //     specified index within the collection.
      //
      // Exceptions:
      //   System.ArgumentOutOfRangeException:
      //     The index parameter is less than 0 or greater than or equal to the value
      //     of the System.Windows.Forms.ListView.ListViewItemCollection.Count property
      //     of the System.Windows.Forms.ListView.ListViewItemCollection.
      //public virtual ListViewItem this[int index] { get; set; }
      //
      // Summary:
      //     Retrieves the item with the specified key.
      //
      // Parameters:
      //   key:
      //     The name of the item to retrieve.
      //
      // Returns:
      //     The System.Windows.Forms.ListViewItem whose System.Windows.Forms.ListViewItem.Name
      //     property matches the specified key.
      //public virtual ListViewItem this[string key] { get; }

      // Summary:
      //     Adds an existing System.Windows.Forms.ListViewItem to the collection.
      //
      // Parameters:
      //   value:
      //     The System.Windows.Forms.ListViewItem to add to the collection.
      //
      // Returns:
      //     The System.Windows.Forms.ListViewItem that was added to the collection.
      //public virtual ListViewItem Add(ListViewItem value);
      //
      // Summary:
      //     Adds an item to the collection with the specified text.
      //
      // Parameters:
      //   text:
      //     The text to display for the item.
      //
      // Returns:
      //     The System.Windows.Forms.ListViewItem that was added to the collection.
      //public virtual ListViewItem Add(string text);
      //
      // Summary:
      //     Adds an item to the collection with the specified text and image.
      //
      // Parameters:
      //   text:
      //     The text of the item.
      //
      //   imageIndex:
      //     The index of the image to display for the item.
      //
      // Returns:
      //     The System.Windows.Forms.ListViewItem that was added to the collection.
      //public virtual ListViewItem Add(string text, int imageIndex);
      //
      // Summary:
      //     Creates an item with the specified text and image and adds it to the collection.
      //
      // Parameters:
      //   text:
      //     The text of the item.
      //
      //   imageKey:
      //     The key of the image to display for the item.
      //
      // Returns:
      //     The System.Windows.Forms.ListViewItem added to the collection.
      //public virtual ListViewItem Add(string text, string imageKey);
      //
      // Summary:
      //     Creates an item with the specified key, text, and image and adds an item
      //     to the collection.
      //
      // Parameters:
      //   key:
      //     The name of the item.
      //
      //   text:
      //     The text of the item.
      //
      //   imageIndex:
      //     The index of the image to display for the item.
      //
      // Returns:
      //     The System.Windows.Forms.ListViewItem added to the collection.
      //
      // Exceptions:
      //   System.InvalidOperationException:
      //     The containing System.Windows.Forms.ListView is in virtual mode.
      //public virtual ListViewItem Add(string key, string text, int imageIndex);
      //
      // Summary:
      //     Creates and item with the specified key, text, and image, and adds it to
      //     the collection.
      //
      // Parameters:
      //   key:
      //     The name of the item.
      //
      //   text:
      //     The text of the item.
      //
      //   imageKey:
      //     The key of the image to display for the item.
      //
      // Returns:
      //     The System.Windows.Forms.ListViewItem added to the collection.
      //public virtual ListViewItem Add(string key, string text, string imageKey);
      //
      // Summary:
      //     Adds a collection of items to the collection.
      //
      // Parameters:
      //   items:
      //     The System.Windows.Forms.ListView.ListViewItemCollection to add to the collection.
      //
      // Exceptions:
      //   System.ArgumentNullException:
      //     items is null.
      //
      //   System.InvalidOperationException:
      //     The containing System.Windows.Forms.ListView is in virtual mode.
      //public void AddRange(ListView.ListViewItemCollection items);
      //
      // Summary:
      //     Adds an array of System.Windows.Forms.ListViewItem objects to the collection.
      //
      // Parameters:
      //   items:
      //     An array of System.Windows.Forms.ListViewItem objects to add to the collection.
      //
      // Exceptions:
      //   System.ArgumentNullException:
      //     items is null.
      //public void AddRange(ListViewItem[] items);
      //

      // Summary:
      //     Determines whether the collection contains an item with the specified key.
      //
      // Parameters:
      //   key:
      //     The name of the item to search for.
      //
      // Returns:
      //     true to indicate the collection contains an item with the specified key;
      //     otherwise, false.
      //public virtual bool ContainsKey(string key);
    
      // Summary:
      //     Searches for items whose name matches the specified key, optionally searching
      //     subitems.
      //
      // Parameters:
      //   key:
      //     The item name to search for.
      //
      //   searchAllSubItems:
      //     true to search subitems; otherwise, false.
      //
      // Returns:
      //     An array of type System.Windows.Forms.ListViewItem.
      //public ListViewItem[] Find(string key, bool searchAllSubItems);
     
      //
      // Summary:
      //     Returns the index within the collection of the specified item.
      //
      // Parameters:
      //   item:
      //     A System.Windows.Forms.ListViewItem representing the item to locate in the
      //     collection.
      //
      // Returns:
      //     The zero-based index of the item's location in the collection; otherwise,
      //     -1 if the item is not located in the collection.
      //public int IndexOf(ListViewItem item);
      ////
      // Summary:
      //     Retrieves the index of the item with the specified key.
      //
      // Parameters:
      //   key:
      //     The name of the item to find in the collection.
      //
      // Returns:
      //     The zero-based index of the first occurrence of the item with the specified
      //     key, if found; otherwise, -1.
      //public virtual int IndexOfKey(string key);
      //
      // Summary:
      //     Inserts an existing System.Windows.Forms.ListViewItem into the collection
      //     at the specified index.
      //
      // Parameters:
      //   index:
      //     The zero-based index location where the item is inserted.
      //
      //   item:
      //     The System.Windows.Forms.ListViewItem that represents the item to insert.
      //
      // Returns:
      //     The System.Windows.Forms.ListViewItem that was inserted into the collection.
      //
      // Exceptions:
      //   System.ArgumentOutOfRangeException:
      //     The index parameter is less than 0 or greater than the value of the System.Windows.Forms.ListView.ListViewItemCollection.Count
      //     property of the System.Windows.Forms.ListView.ListViewItemCollection.
      //public ListViewItem Insert(int index, ListViewItem item);
      //
      // Summary:
      //     Creates a new item and inserts it into the collection at the specified index.
      //
      // Parameters:
      //   index:
      //     The zero-based index location where the item is inserted.
      //
      //   text:
      //     The text to display for the item.
      //
      // Returns:
      //     The System.Windows.Forms.ListViewItem that was inserted into the collection.
      //
      // Exceptions:
      //   System.ArgumentOutOfRangeException:
      //     The index parameter is less than 0 or greater than the value of the System.Windows.Forms.ListView.ListViewItemCollection.Count
      //     property of the System.Windows.Forms.ListView.ListViewItemCollection.
      //public ListViewItem Insert(int index, string text);
      //
      // Summary:
      //     Creates a new item with the specified image index and inserts it into the
      //     collection at the specified index.
      //
      // Parameters:
      //   index:
      //     The zero-based index location where the item is inserted.
      //
      //   text:
      //     The text to display for the item.
      //
      //   imageIndex:
      //     The index of the image to display for the item.
      //
      // Returns:
      //     The System.Windows.Forms.ListViewItem that was inserted into the collection.
      //
      // Exceptions:
      //   System.ArgumentOutOfRangeException:
      //     The index parameter is less than 0 or greater than the value of the System.Windows.Forms.ListView.ListViewItemCollection.Count
      //     property of the System.Windows.Forms.ListView.ListViewItemCollection.
      //public ListViewItem Insert(int index, string text, int imageIndex);
      //
      // Summary:
      //     Creates a new item with the specified text and image and inserts it in the
      //     collection at the specified index.
      //
      // Parameters:
      //   index:
      //     The zero-based index location where the item is inserted.
      //
      //   text:
      //     The text of the System.Windows.Forms.ListViewItem.
      //
      //   imageKey:
      //     The key of the image to display for the item.
      //
      // Returns:
      //     The System.Windows.Forms.ListViewItem added to the collection.
      //
      // Exceptions:
      //   System.ArgumentOutOfRangeException:
      //     The index parameter is less than 0 or greater than the value of the System.Windows.Forms.ListView.ListViewItemCollection.Count
      //     property of the System.Windows.Forms.ListView.ListViewItemCollection.
      //public ListViewItem Insert(int index, string text, string imageKey);
      //
      // Summary:
      //     Creates a new item with the specified key, text, and image, and inserts it
      //     in the collection at the specified index.
      //
      // Parameters:
      //   index:
      //     The zero-based index location where the item is inserted
      //
      //   key:
      //     The System.Windows.Forms.ListViewItem.Name of the item.
      //
      //   text:
      //     The text of the item.
      //
      //   imageIndex:
      //     The index of the image to display for the item.
      //
      // Returns:
      //     The System.Windows.Forms.ListViewItem added to the collection.
      //
      // Exceptions:
      //   System.ArgumentOutOfRangeException:
      //     The index parameter is less than 0 or greater than the value of the System.Windows.Forms.ListView.ListViewItemCollection.Count
      //     property of the System.Windows.Forms.ListView.ListViewItemCollection.
      //public virtual ListViewItem Insert(int index, string key, string text, int imageIndex);
      //
      // Summary:
      //     Creates a new item with the specified key, text, and image, and adds it to
      //     the collection at the specified index.
      //
      // Parameters:
      //   index:
      //     The zero-based index location where the item is inserted.
      //
      //   key:
      //     The System.Windows.Forms.ListViewItem.Name of the item.
      //
      //   text:
      //     The text of the item.
      //
      //   imageKey:
      //     The key of the image to display for the item.
      //
      // Returns:
      //     The System.Windows.Forms.ListViewItem added to the collection.
      //
      // Exceptions:
      //   System.ArgumentOutOfRangeException:
      //     The index parameter is less than 0 or greater than the value of the System.Windows.Forms.ListView.ListViewItemCollection.Count
      //     property of the System.Windows.Forms.ListView.ListViewItemCollection.
      //public virtual ListViewItem Insert(int index, string key, string text, string imageKey);
      //
      // Summary:
      //     Removes the specified item from the collection.
      //
      // Parameters:
      //   item:
      //     A System.Windows.Forms.ListViewItem representing the item to remove from
      //     the collection.
      //
      // Exceptions:
      //   System.ArgumentException:
      //     The System.Windows.Forms.ListViewItem assigned to the item parameter is null.
      //public virtual void Remove(ListViewItem item);
      //
      // Summary:
      //     Removes the item at the specified index within the collection.
      //
      // Parameters:
      //   index:
      //     The zero-based index of the item to remove.
      //
      // Exceptions:
      //   System.ArgumentOutOfRangeException:
      //     The index parameter is less than 0 or greater than or equal to the value
      //     of the System.Windows.Forms.ListView.ListViewItemCollection.Count property
      //     of the System.Windows.Forms.ListView.ListViewItemCollection.
      //public virtual void RemoveAt(int index);
      //
      // Summary:
      //     Removes the item with the specified key from the collection.
      //
      // Parameters:
      //   key:
      //     The name of the item to remove from the collection.
      //public virtual void RemoveByKey(string key);
    }

    // Summary:
    //     Represents the collection that contains the indexes to the selected items
    //     in a System.Windows.Forms.ListView control.
    //[ListBindable(false)]
    public class SelectedIndexCollection // : IList, ICollection, IEnumerable
    {
      // Summary:
      //     Initializes a new instance of the System.Windows.Forms.ListView.SelectedIndexCollection
      //     class.
      //
      // Parameters:
      //   owner:
      //     A System.Windows.Forms.ListView control that owns the collection.
      //public SelectedIndexCollection(ListView owner);
    }

    // Summary:
    //     Represents the collection of selected items in a list view control.
    //[ListBindable(false)]
    public class SelectedListViewItemCollection //: IList, ICollection, IEnumerable
    {
      // Summary:
      //     Initializes a new instance of the System.Windows.Forms.ListView.SelectedListViewItemCollection
      //     class.
      //
      // Parameters:
      //   owner:
      //     The System.Windows.Forms.ListView control that owns the collection.
      //public SelectedListViewItemCollection(ListView owner);
    }
  }
}
