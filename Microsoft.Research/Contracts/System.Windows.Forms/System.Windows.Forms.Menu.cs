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
  //     Represents the base functionality for all menus. Although System.Windows.Forms.ToolStripDropDown
  //     and System.Windows.Forms.ToolStripDropDownMenu replace and add functionality
  //     to the System.Windows.Forms.Menu control of previous versions, System.Windows.Forms.Menu
  //     is retained for both backward compatibility and future use if you choose.
  //[ListBindable(false)]
  //[ToolboxItemFilter("System.Windows.Forms")]
  public abstract class Menu
  {
    // Summary:
    //     Specifies that the System.Windows.Forms.Menu.FindMenuItem(System.Int32,System.IntPtr)
    //     method should search for a handle.
    public const int FindHandle = 0;
    //
    // Summary:
    //     Specifies that the System.Windows.Forms.Menu.FindMenuItem(System.Int32,System.IntPtr)
    //     method should search for a shortcut.
    public const int FindShortcut = 1;

    // Summary:
    //     Initializes a new instance of the System.Windows.Forms.Menu class.
    //
    // Parameters:
    //   items:
    //     An array of type System.Windows.Forms.MenuItem containing the objects to
    //     add to the menu.
    // protected Menu(MenuItem [] items);

    // Summary:
    //     Gets a value representing the window handle for the menu.
    //
    // Returns:
    //     The HMENU value of the menu.
    //[Browsable(false)]
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //public IntPtr Handle { get; }
    //
    // Summary:
    //     Gets a value indicating whether this menu contains any menu items. This property
    //     is read-only.
    //
    // Returns:
    //     true if this menu contains System.Windows.Forms.MenuItem objects; otherwise,
    //     false. The default is false.
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //[Browsable(false)]
    // public virtual bool IsParent { get; }
    //
    // Summary:
    //     Gets a value indicating the System.Windows.Forms.MenuItem that is used to
    //     display a list of multiple document interface (MDI) child forms.
    //
    // Returns:
    //     A System.Windows.Forms.MenuItem that represents the menu item displaying
    //     a list of MDI child forms that are open in the application.
    //[Browsable(false)]
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //public MenuItem MdiListItem { get; }
    //
    // Summary:
    //     Gets a value indicating the collection of System.Windows.Forms.MenuItem objects
    //     associated with the menu.
    //
    // Returns:
    //     A System.Windows.Forms.Menu.MenuItemCollection that represents the list of
    //     System.Windows.Forms.MenuItem objects stored in the menu.
    //[Browsable(false)]
    //[MergableProperty(false)]
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    //public Menu.MenuItemCollection MenuItems { get; }
    ////
    // Summary:
    //     Gets or sets the name of the System.Windows.Forms.Menu.
    //
    // Returns:
    //     A string representing the name.
    //[Browsable(false)]
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //public string Name { get; set; }
    //
    // Summary:
    //     Gets or sets user-defined data associated with the control.
    //
    // Returns:
    //     An object representing the data.
    //[Bindable(true)]
    //[DefaultValue("")]
    //[Localizable(false)]
    //[TypeConverter(typeof(StringConverter))]
    //public object Tag { get; set; }

    // Summary:
    //     Copies the System.Windows.Forms.Menu that is passed as a parameter to the
    //     current System.Windows.Forms.Menu.
    //
    // Parameters:
    //   menuSrc:
    //     The System.Windows.Forms.Menu to copy.
    // protected internal void CloneMenu(Menu menuSrc);
    //
    // Summary:
    //     Creates a new handle to the System.Windows.Forms.Menu.
    //
    // Returns:
    //     A handle to the menu if the method succeeds; otherwise, null.
    // protected virtual IntPtr CreateMenuHandle();
    //
    // Summary:
    //     Disposes of the resources, other than memory, used by the System.Windows.Forms.Menu.
    //
    // Parameters:
    //   disposing:
    //     true to release both managed and unmanaged resources; false to release only
    //     unmanaged resources.
    // protected override void Dispose(bool disposing);
    //
    // Summary:
    //     Gets the System.Windows.Forms.MenuItem that contains the value specified.
    //
    // Parameters:
    //   type:
    //     The type of item to use to find the System.Windows.Forms.MenuItem.
    //
    //   value:
    //     The item to use to find the System.Windows.Forms.MenuItem.
    //
    // Returns:
    //     The System.Windows.Forms.MenuItem that matches value; otherwise, null.
    //public MenuItem FindMenuItem(int type, IntPtr value);
    //
    // Summary:
    //     Returns the position at which a menu item should be inserted into the menu.
    //
    // Parameters:
    //   mergeOrder:
    //     The merge order position for the menu item to be merged.
    //
    // Returns:
    //     The position at which a menu item should be inserted into the menu.
    // protected int FindMergePosition(int mergeOrder);
    //
    // Summary:
    //     Gets the System.Windows.Forms.ContextMenu that contains this menu.
    //
    // Returns:
    //     The System.Windows.Forms.ContextMenu that contains this menu. The default
    //     is null.
    //public ContextMenu GetContextMenu();
    //
    // Summary:
    //     Gets the System.Windows.Forms.MainMenu that contains this menu.
    //
    // Returns:
    //     The System.Windows.Forms.MainMenu that contains this menu.
    //public MainMenu GetMainMenu();
    //
    // Summary:
    //     Merges the System.Windows.Forms.MenuItem objects of one menu with the current
    //     menu.
    //
    // Parameters:
    //   menuSrc:
    //     The System.Windows.Forms.Menu whose menu items are merged with the menu items
    //     of the current menu.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     It was attempted to merge the menu with itself.
    // public virtual void MergeMenu(Menu menuSrc);
    //
    // Summary:
    //     Processes a command key.
    //
    // Parameters:
    //   msg:
    //     A System.Windows.Forms.Message, passed by reference that represents the window
    //     message to process.
    //
    //   keyData:
    //     One of the System.Windows.Forms.Keys values that represents the key to process.
    //
    // Returns:
    //     true if the character was processed by the control; otherwise, false.
    // protected internal virtual bool ProcessCmdKey(ref Message msg, Keys keyData);
    //
    // Summary:
    //     Returns a System.String that represents the System.Windows.Forms.Menu control.
    //
    // Returns:
    //     A System.String that represents the current System.Windows.Forms.Menu.
    //public override string ToString();

    // Summary:
    //     Represents a collection of System.Windows.Forms.MenuItem objects.
    //[ListBindable(false)]
    public class MenuItemCollection
    {
      // Summary:
      //     Initializes a new instance of the System.Windows.Forms.Menu.MenuItemCollection
      //     class.
      //
      // Parameters:
      //   owner:
      //     The System.Windows.Forms.Menu that owns this collection.
      public MenuItemCollection(Menu owner) { }


      // Summary:
      //     Retrieves the System.Windows.Forms.MenuItem at the specified indexed location
      //     in the collection.
      //
      // Parameters:
      //   index:
      //     The indexed location of the System.Windows.Forms.MenuItem in the collection.
      //
      // Returns:
      //     The System.Windows.Forms.MenuItem at the specified location.
      //
      // Exceptions:
      //   System.ArgumentException:
      //     The value parameter is null.or The index parameter is less than zero.or The
      //     index parameter is greater than the number of menu items in the collection,
      //     and the collection of menu items is not null.
      // public virtual MenuItem this[int index] { get; }
      //
      // Summary:
      //     Gets an item with the specified key from the collection.
      //
      // Parameters:
      //   key:
      //     The name of the item to retrieve from the collection.
      //
      // Returns:
      //     The System.Windows.Forms.MenuItem with the specified key.
      // public virtual MenuItem this[string key] { get; }


      // Summary:
      //     Adds a new System.Windows.Forms.MenuItem, to the end of the current menu,
      //     with a specified caption.
      //
      // Parameters:
      //   caption:
      //     The caption of the menu item.
      //
      // Returns:
      //     A System.Windows.Forms.MenuItem that represents the menu item being added
      //     to the collection.
      public virtual MenuItem Add(string caption)
      {
        Contract.Ensures(Contract.Result<MenuItem>() != null);
        return default(MenuItem);
      }

      // Summary:
      //     Adds a new System.Windows.Forms.MenuItem to the end of the current menu with
      //     a specified caption and a specified event handler for the System.Windows.Forms.MenuItem.Click
      //     event.
      //
      // Parameters:
      //   caption:
      //     The caption of the menu item.
      //
      //   onClick:
      //     An System.EventHandler that represents the event handler that is called when
      //     the item is clicked by the user, or when a user presses an accelerator or
      //     shortcut key for the menu item.
      //
      // Returns:
      //     A System.Windows.Forms.MenuItem that represents the menu item being added
      //     to the collection.
      public virtual MenuItem Add(string caption, EventHandler onClick)
      {
        Contract.Ensures(Contract.Result<MenuItem>() != null);
        return default(MenuItem);
      }
      // Summary:
      //     Adds a new System.Windows.Forms.MenuItem to the end of this menu with the
      //     specified caption, System.Windows.Forms.MenuItem.Click event handler, and
      //     items.
      //
      // Parameters:
      //   caption:
      //     The caption of the menu item.
      //
      //   items:
      //     An array of System.Windows.Forms.MenuItem objects that this System.Windows.Forms.MenuItem
      //     will contain.
      //
      // Returns:
      //     A System.Windows.Forms.MenuItem that represents the menu item being added
      //     to the collection.
      public virtual MenuItem Add(string caption, MenuItem[] items)
      {
        Contract.Ensures(Contract.Result<MenuItem>() != null);
        return default(MenuItem);
      }

      //
      // Summary:
      //     Finds the items with the specified key, optionally searching the submenu
      //     items
      //
      // Parameters:
      //   key:
      //     The name of the menu item to search for.
      //
      //   searchAllChildren:
      //     true to search child menu items; otherwise, false.
      //
      // Returns:
      //     An array of System.Windows.Forms.MenuItem objects whose System.Windows.Forms.Menu.Name
      //     property matches the specified key.
      //
      // Exceptions:
      //   System.ArgumentNullException:
      //     key is null or an empty string.
      public MenuItem[] Find(string key, bool searchAllChildren)
      {
        Contract.Requires(!String.IsNullOrEmpty(key));

        return default(MenuItem[]);
      }
    }
  }
}
