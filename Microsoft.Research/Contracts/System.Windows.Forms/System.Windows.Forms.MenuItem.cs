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

using System.Diagnostics.Contracts;

namespace System.Windows.Forms
{
  // Summary:
  //     Represents an individual item that is displayed within a System.Windows.Forms.MainMenu
  //     or System.Windows.Forms.ContextMenu. Although System.Windows.Forms.ToolStripMenuItem
  //     replaces and adds functionality to the System.Windows.Forms.MenuItem control
  //     of previous versions, System.Windows.Forms.MenuItem is retained for both
  //     backward compatibility and future use if you choose.
  //[DefaultProperty("Text")]
  //[DefaultEvent("Click")]
  //[ToolboxItem(false)]
  //[DesignTimeVisible(false)]
  public class MenuItem 
  {
    // Summary:
    //     Initializes a System.Windows.Forms.MenuItem with a blank caption.
    public MenuItem() { }
    //
    // Summary:
    //     Initializes a new instance of the System.Windows.Forms.MenuItem class with
    //     a specified caption for the menu item.
    //
    // Parameters:
    //   text:
    //     The caption for the menu item.
    public MenuItem(string text) { }
    //
    // Summary:
    //     Initializes a new instance of the class with a specified caption and event
    //     handler for the System.Windows.Forms.MenuItem.Click event of the menu item.
    //
    // Parameters:
    //   text:
    //     The caption for the menu item.
    //
    //   onClick:
    //     The System.EventHandler that handles the System.Windows.Forms.MenuItem.Click
    //     event for this menu item.
    public MenuItem(string text, EventHandler onClick)
    { }
    //
    // Summary:
    //     Initializes a new instance of the class with a specified caption and an array
    //     of submenu items defined for the menu item.
    //
    // Parameters:
    //   text:
    //     The caption for the menu item.
    //
    //   items:
    //     An array of System.Windows.Forms.MenuItem objects that contains the submenu
    //     items for this menu item.
    public MenuItem(string text, MenuItem[] items) { }
    //
    // Summary:
    //     Initializes a new instance of the class with a specified caption, event handler,
    //     and associated shortcut key for the menu item.
    //
    // Parameters:
    //   text:
    //     The caption for the menu item.
    //
    //   onClick:
    //     The System.EventHandler that handles the System.Windows.Forms.MenuItem.Click
    //     event for this menu item.
    //
    //   shortcut:
    //     One of the System.Windows.Forms.Shortcut values.
    public MenuItem(string text, EventHandler onClick, Shortcut shortcut) { }
    //
    // Summary:
    //     Initializes a new instance of the System.Windows.Forms.MenuItem class with
    //     a specified caption; defined event-handlers for the System.Windows.Forms.MenuItem.Click,
    //     System.Windows.Forms.MenuItem.Select and System.Windows.Forms.MenuItem.Popup
    //     events; a shortcut key; a merge type; and order specified for the menu item.
    //
    // Parameters:
    //   mergeType:
    //     One of the System.Windows.Forms.MenuMerge values.
    //
    //   mergeOrder:
    //     The relative position that this menu item will take in a merged menu.
    //
    //   shortcut:
    //     One of the System.Windows.Forms.Shortcut values.
    //
    //   text:
    //     The caption for the menu item.
    //
    //   onClick:
    //     The System.EventHandler that handles the System.Windows.Forms.MenuItem.Click
    //     event for this menu item.
    //
    //   onPopup:
    //     The System.EventHandler that handles the System.Windows.Forms.MenuItem.Popup
    //     event for this menu item.
    //
    //   onSelect:
    //     The System.EventHandler that handles the System.Windows.Forms.MenuItem.Select
    //     event for this menu item.
    //
    //   items:
    //     An array of System.Windows.Forms.MenuItem objects that contains the submenu
    //     items for this menu item.
    //public MenuItem(MenuMerge mergeType, int mergeOrder, Shortcut shortcut, string text, EventHandler onClick, EventHandler onPopup, EventHandler onSelect, MenuItem//[] items);

    // Summary:
    //     Gets or sets a value indicating whether the System.Windows.Forms.MenuItem
    //     is placed on a new line (for a menu item added to a System.Windows.Forms.MainMenu
    //     object) or in a new column (for a submenu item or menu item displayed in
    //     a System.Windows.Forms.ContextMenu).
    //
    // Returns:
    //     true if the menu item is placed on a new line or in a new column; false if
    //     the menu item is left in its default placement. The default is false.
    //[Browsable(false)]
    //[DefaultValue(false)]
    //public bool BarBreak { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether the item is placed on a new line
    //     (for a menu item added to a System.Windows.Forms.MainMenu object) or in a
    //     new column (for a menu item or submenu item displayed in a System.Windows.Forms.ContextMenu).
    //
    // Returns:
    //     true if the menu item is placed on a new line or in a new column; false if
    //     the menu item is left in its default placement. The default is false.
    //[DefaultValue(false)]
    //[Browsable(false)]
    //public bool Break { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether a check mark appears next to the
    //     text of the menu item.
    //
    // Returns:
    //     true if there is a check mark next to the menu item; otherwise, false. The
    //     default is false.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The System.Windows.Forms.MenuItem is a top-level menu or has children.
    //[DefaultValue(false)]
    //public bool Checked { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether the menu item is the default menu
    //     item.
    //
    // Returns:
    //     true if the menu item is the default item in a menu; otherwise, false. The
    //     default is false.
    //[DefaultValue(false)]
    //public bool DefaultItem { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether the menu item is enabled.
    //
    // Returns:
    //     true if the menu item is enabled; otherwise, false. The default is true.
    //[Localizable(true)]
    //[DefaultValue(true)]
    //public bool Enabled { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating the position of the menu item in its parent
    //     menu.
    //
    // Returns:
    //     The zero-based index representing the position of the menu item in its parent
    //     menu.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     The assigned value is less than zero or greater than the item count.
    //[Browsable(false)]
    //public int Index { get; set; }
    //
    // Summary:
    //     Gets a value indicating whether the menu item contains child menu items.
    //
    // Returns:
    //     true if the menu item contains child menu items; false if the menu is a standalone
    //     menu item.
    //[Browsable(false)]
    // public override bool IsParent { get; }
    //
    // Summary:
    //     Gets or sets a value indicating whether the menu item will be populated with
    //     a list of the Multiple Document Interface (MDI) child windows that are displayed
    //     within the associated form.
    //
    // Returns:
    //     true if a list of the MDI child windows is displayed in this menu item; otherwise,
    //     false. The default is false.
    //[DefaultValue(false)]
    //public bool MdiList { get; set; }
    //
    // Summary:
    //     Gets a value indicating the Windows identifier for this menu item.
    //
    // Returns:
    //     The Windows identifier for this menu item.
    //// protected int MenuID { get; }
    //
    // Summary:
    //     Gets or sets a value indicating the relative position of the menu item when
    //     it is merged with another.
    //
    // Returns:
    //     A zero-based index representing the merge order position for this menu item.
    //     The default is 0.
    //[DefaultValue(0)]
    //public int MergeOrder { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating the behavior of this menu item when its menu
    //     is merged with another.
    //
    // Returns:
    //     A System.Windows.Forms.MenuMerge value that represents the menu item's merge
    //     type.
    //
    // Exceptions:
    //   System.ComponentModel.InvalidEnumArgumentException:
    //     The assigned value is not one of the System.Windows.Forms.MenuMerge values.
    //public MenuMerge MergeType { get; set; }
    //
    // Summary:
    //     Gets a value indicating the mnemonic character that is associated with this
    //     menu item.
    //
    // Returns:
    //     A character that represents the mnemonic character associated with this menu
    //     item. Returns the NUL character (ASCII value 0) if no mnemonic character
    //     is specified in the text of the System.Windows.Forms.MenuItem.
    //[Browsable(false)]
    //public char Mnemonic { get; }
    //
    // Summary:
    //     Gets or sets a value indicating whether the code that you provide draws the
    //     menu item or Windows draws the menu item.
    //
    // Returns:
    //     true if the menu item is to be drawn using code; false if the menu item is
    //     to be drawn by Windows. The default is false.
    //[DefaultValue(false)]
    //public bool OwnerDraw { get; set; }
    //
    // Summary:
    //     Gets a value indicating the menu that contains this menu item.
    //
    // Returns:
    //     A System.Windows.Forms.Menu that represents the menu that contains this menu
    //     item.
    //[Browsable(false)]
    //public Menu Parent { get; }
    //
    // Summary:
    //     Gets or sets a value indicating whether the System.Windows.Forms.MenuItem,
    //     if checked, displays a radio-button instead of a check mark.
    //
    // Returns:
    //     true if a radio-button is to be used instead of a check mark; false if the
    //     standard check mark is to be displayed when the menu item is checked. The
    //     default is false.
    //[DefaultValue(false)]
    //public bool RadioCheck { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating the shortcut key associated with the menu
    //     item.
    //
    // Returns:
    //     One of the System.Windows.Forms.Shortcut values. The default is Shortcut.None.
    //
    // Exceptions:
    //   System.ComponentModel.InvalidEnumArgumentException:
    //     The assigned value is not one of the System.Windows.Forms.Shortcut values.
    //[Localizable(true)]
    //public Shortcut Shortcut { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether the shortcut key that is associated
    //     with the menu item is displayed next to the menu item caption.
    //
    // Returns:
    //     true if the shortcut key combination is displayed next to the menu item caption;
    //     false if the shortcut key combination is not to be displayed. The default
    //     is true.
    //[Localizable(true)]
    //[DefaultValue(true)]
    //public bool ShowShortcut { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating the caption of the menu item.
    //
    // Returns:
    //     The text caption of the menu item.
    //[Localizable(true)]
    //public string Text { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether the menu item is visible.
    //
    // Returns:
    //     true if the menu item will be made visible on the menu; otherwise, false.
    //     The default is true.
    //[Localizable(true)]
    //[DefaultValue(true)]
    //public bool Visible { get; set; }

    // Summary:
    //     Occurs when the menu item is clicked or selected using a shortcut key or
    //     access key defined for the menu item.
    //// public event EventHandler Click;
    //
    // Summary:
    //     Occurs when the System.Windows.Forms.MenuItem.OwnerDraw property of a menu
    //     item is set to true and a request is made to draw the menu item.
    // public event DrawItemEventHandler DrawItem;
    //
    // Summary:
    //     Occurs when the menu needs to know the size of a menu item before drawing
    //     it.
    // public event MeasureItemEventHandler MeasureItem;
    //
    // Summary:
    //     Occurs before a menu item's list of menu items is displayed.
    // public event EventHandler Popup;
    //
    // Summary:
    //     Occurs when the user places the pointer over a menu item.
    // public event EventHandler Select;

    // Summary:
    //     Creates a copy of the current System.Windows.Forms.MenuItem.
    //
    // Returns:
    //     A System.Windows.Forms.MenuItem that represents the duplicated menu item.
    // // protected MenuItem CloneMenu();
    //
    // Summary:
    //     Creates a copy of the specified System.Windows.Forms.MenuItem.
    //
    // Parameters:
    //   itemSrc:
    //     The System.Windows.Forms.MenuItem that represents the menu item to copy.
    //
    // Returns:
    //     A System.Windows.Forms.MenuItem that represents the duplicated menu item.
    // protected void CloneMenu(MenuItem itemSrc);
    //
    // Summary:
    //     Disposes of the resources (other than memory) used by the System.Windows.Forms.MenuItem.
    // protected override void Dispose(bool disposing);
    //
    // Summary:
    //     Merges this System.Windows.Forms.MenuItem with another System.Windows.Forms.MenuItem
    //     and returns the resulting merged System.Windows.Forms.MenuItem.
    //
    // Returns:
    //     A System.Windows.Forms.MenuItem that represents the merged menu item.
    // // protected MenuItem MergeMenu();
    //
    // Summary:
    //     Merges another menu item with this menu item.
    //
    // Parameters:
    //   itemSrc:
    //     A System.Windows.Forms.MenuItem that specifies the menu item to merge with
    //     this one.
    //public void MergeMenu(MenuItem itemSrc);
    //
    // Summary:
    //     Raises the System.Windows.Forms.MenuItem.Click event.
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    // protected virtual void OnClick(EventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.MenuItem.DrawItem event.
    //
    // Parameters:
    //   e:
    //     A System.Windows.Forms.DrawItemEventArgs that contains the event data.
    // protected virtual void OnDrawItem(DrawItemEventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.MenuItem.Popup event.
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    // protected virtual void OnInitMenuPopup(EventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.MenuItem.MeasureItem event.
    //
    // Parameters:
    //   e:
    //     A System.Windows.Forms.MeasureItemEventArgs that contains the event data.
    // protected virtual void OnMeasureItem(MeasureItemEventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.MenuItem.Popup event.
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    // protected virtual void OnPopup(EventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.MenuItem.Select event.
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    // protected virtual void OnSelect(EventArgs e);
    //
    // Summary:
    //     Generates a System.Windows.Forms.Control.Click event for the System.Windows.Forms.MenuItem,
    //     simulating a click by a user.
    //public void PerformClick();
    //
    // Summary:
    //     Raises the System.Windows.Forms.MenuItem.Select event for this menu item.
    // // protected void PerformSelect();
    //
    // Summary:
    //     Returns a string that represents the System.Windows.Forms.MenuItem.
    //
    // Returns:
    //     A string that represents the current System.Windows.Forms.MenuItem. The string
    //     includes the type and the System.Windows.Forms.MenuItem.Text property of
    //     the control.
    // public override string ToString();
  }
}
