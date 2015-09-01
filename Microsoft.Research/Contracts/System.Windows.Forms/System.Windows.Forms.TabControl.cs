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
  //     Specifies the locations of the tabs in a tab control.
  public enum TabAlignment
  {
    // Summary:
    //     The tabs are located across the top of the control.
    Top = 0,
    //
    // Summary:
    //     The tabs are located across the bottom of the control.
    Bottom = 1,
    //
    // Summary:
    //     The tabs are located along the left edge of the control.
    Left = 2,
    //
    // Summary:
    //     The tabs are located along the right edge of the control.
    Right = 3,
  }

  // Summary:
  //     Specifies the appearance of the tabs in a tab control.
  public enum TabAppearance
  {
    // Summary:
    //     The tabs have the standard appearance of tabs.
    Normal = 0,
    //
    // Summary:
    //     The tabs have the appearance of three-dimensional buttons.
    Buttons = 1,
    //
    // Summary:
    //     The tabs have the appearance of flat buttons.
    FlatButtons = 2,
  }

  // Summary:
  //     Specifies whether the tabs in a tab control are owner-drawn (drawn by the
  //     parent window), or drawn by the operating system.
  public enum TabDrawMode
  {
    // Summary:
    //     The tabs are drawn by the operating system, and are of the same size.
    Normal = 0,
    //
    // Summary:
    //     The tabs are drawn by the parent window, and are of the same size.
    OwnerDrawFixed = 1,
  }

  // Summary:
  //     Specifies how tabs in a tab control are sized.
  public enum TabSizeMode
  {
    // Summary:
    //     The width of each tab is sized to accommodate what is displayed on the tab,
    //     and the size of tabs in a row are not adjusted to fill the entire width of
    //     the container control.
    Normal = 0,
    //
    // Summary:
    //     The width of each tab is sized so that each row of tabs fills the entire
    //     width of the container control. This is only applicable to tab controls with
    //     more than one row.
    FillToRight = 1,
    //
    // Summary:
    //     All tabs in a control are the same width.
    Fixed = 2,
  }

  public class TabControl
  {

    // Summary:
    //     Initializes a new instance of the System.Windows.Forms.TabControl class.
    public TabControl()
    {
    }

    // Summary:
    //     Gets or sets the area of the control (for example, along the top) where the
    //     tabs are aligned.
    //
    // Returns:
    //     One of the System.Windows.Forms.TabAlignment values. The default is Top.
    //
    // Exceptions:
    //   System.ComponentModel.InvalidEnumArgumentException:
    //     The property value is not a valid System.Windows.Forms.TabAlignment value.
    public TabAlignment Alignment
    {
      get
      {
        return default(TabAlignment);
      }
      set
      {
        //
      }
    }   
    
    //
    // Summary:
    //     Gets or sets the visual appearance of the control's tabs.
    //
    // Returns:
    //     One of the System.Windows.Forms.TabAppearance values. The default is Normal.
    //
    // Exceptions:
    //   System.ComponentModel.InvalidEnumArgumentException:
    //     The property value is not a valid System.Windows.Forms.TabAppearance value
    public TabAppearance Appearance
    {
      get
      {
        return default(TabAppearance);
      }
      set
      {
        //
      }
    } 

    //
    // Summary:
    //     This member is not meaningful for this control.
    //
    // Returns:
    //     Always System.Drawing.SystemColors.Control.

    // public override System.Drawing.Color BackColor { get; set; }
    

    // Summary:
    //     This member is not meaningful for this control.
    //
    // Returns:
    //     An System.Drawing.Image.
    // public override System.Drawing.Image BackgroundImage { get; set; }
    
    // Summary:
    //     This member is not meaningful for this control.
    //
    // Returns:
    //     An System.Windows.Forms.ImageLayout.

    //public override ImageLayout BackgroundImageLayout { get; set; }
    //
    // Summary:
    //     This member overrides System.Windows.Forms.Control.CreateParams.
    //
    // Returns:
    //     A System.Windows.Forms.CreateParams that contains the required creation parameters
    //     when the handle to the control is created.
    //protected override CreateParams CreateParams { get; }
    //
    //
    // Returns:
    //     The default System.Drawing.Size of the control.
    //protected override System.Drawing.Size DefaultSize { get; }
    //
    // Summary:
    //     Gets the display area of the control's tab pages.
    //
    // Returns:
    //     A System.Drawing.Rectangle that represents the display area of the tab pages.
    //public override System.Drawing.Rectangle DisplayRectangle { get; }
    //
    // Summary:
    //     This member is not meaningful for this control.
    //
    // Returns:
    //     A System.Boolean value.
    //protected override bool DoubleBuffered { get; set; }
    //
    // Summary:
    //     Gets or sets the way that the control's tabs are drawn.
    //
    // Returns:
    //     One of the System.Windows.Forms.TabDrawMode values. The default is Normal.
    //
    // Exceptions:
    //   System.ComponentModel.InvalidEnumArgumentException:
    //     The property value is not a valid System.Windows.Forms.TabDrawMode value.
    public TabDrawMode DrawMode
    {
      get
      {
        return default(TabDrawMode);
      }
      set
      {
        //
      }
    } 
    //
    // Summary:
    //     This member is not meaningful for this control.
    //
    // Returns:
    //     A System.Drawing.Color.

    //public override System.Drawing.Color ForeColor { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether the control's tabs change in appearance
    //     when the mouse passes over them.
    //
    // Returns:
    //     true if the tabs change in appearance when the mouse passes over them; otherwise,
    //     false. The default is false.
    public bool HotTrack
    {
      get
      {
        return default(bool);
      }
      set
      {
        //
      }
    } 
    //
    // Summary:
    //     Gets or sets the images to display on the control's tabs.
    //
    // Returns:
    //     An System.Windows.Forms.ImageList that specifies the images to display on
    //     the tabs.

    public ImageList ImageList
    {
      get
      {
        return default(ImageList);
      }
      set
      {
        //
      }
    } 
    //
    // Summary:
    //     Gets or sets the size of the control's tabs.
    //
    // Returns:
    //     A System.Drawing.Size that represents the size of the tabs. The default automatically
    //     sizes the tabs to fit the icons and labels on the tabs.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     The width or height of the System.Drawing.Size is less than 0.
    
    //public System.Drawing.Size ItemSize
    //{
    //  get
    //  {
    //    return default(System.Drawing.Size);
    //  }
    //  set
    //  {
    //    //
    //  }
    //} 
    //
    // Summary:
    //     Gets or sets a value indicating whether more than one row of tabs can be
    //     displayed.
    //
    // Returns:
    //     true if more than one row of tabs can be displayed; otherwise, false. The
    //     default is false.
    
    public bool Multiline
    {
      get
      {
        return default(bool);
      }
      set
      {
        //
      }
    } 
    //
    // Summary:
    //     Gets or sets the amount of space around each item on the control's tab pages.
    //
    // Returns:
    //     A System.Drawing.Point that specifies the amount of space around each item.
    //     The default is (6,3).
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     The width or height of the System.Drawing.Point is less than 0.
    
    //public System.Drawing.Point Padding 
    // { 
    //  get
    //  {
    //    return default(System.Drawing.Size);
    //  }
    //  set
    //  {
    //    //
    //  }
    //}
    //
    // Summary:
    //     Gets or sets a value indicating whether right-to-left mirror placement is
    //     turned on.
    //
    // Returns:
    //     true if right-to-left mirror placement is turned on; false for standard child
    //     control placement. The default is false.
    
    //public virtual bool RightToLeftLayout { get; set; }
    //
    // Summary:
    //     Gets the number of rows that are currently being displayed in the control's
    //     tab strip.
    //
    // Returns:
    //     The number of rows that are currently being displayed in the tab strip.
    
    public int RowCount 
    {
      get
      {
        return default(int);
      }
    }
    
    //
    // Summary:
    //     Gets or sets the index of the currently selected tab page.
    //
    // Returns:
    //     The zero-based index of the currently selected tab page. The default is -1,
    //     which is also the value if no tab page is selected.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     The value is less than -1
    public int SelectedIndex
    {
      get
      {
        return default(int);
      }
      set
      {
        Contract.Requires(value >= -1);
        //
      }
    }
    //
    // Summary:
    //     Gets or sets the currently selected tab page.
    //
    // Returns:
    //     A System.Windows.Forms.TabPage that represents the selected tab page. If
    //     no tab page is selected, the value is null.
    public TabPage SelectedTab
    {
      get
      {
        return default(TabPage);
      }
      set
      {
        //
      }
    }
    //
    // Summary:
    //     Gets or sets a value indicating whether a tab's ToolTip is shown when the
    //     mouse passes over the tab.
    //
    // Returns:
    //     true if ToolTips are shown for the tabs that have them; otherwise, false.
    //     The default is false.
    public bool ShowToolTips
    {
      get
      {
        return default(bool);
      }
      set
      {
        //
      }
    }
    //
    // Summary:
    //     Gets or sets the way that the control's tabs are sized.
    //
    // Returns:
    //     One of the System.Windows.Forms.TabSizeMode values. The default is Normal.
    //
    // Exceptions:
    //   System.ComponentModel.InvalidEnumArgumentException:
    //     The property value is not a valid System.Windows.Forms.TabSizeMode value.
    
    public TabSizeMode SizeMode 
        {
      get
      {
        return default(TabSizeMode);
      }
      set
      {
        //
      }
    }
    
    //
    // Summary:
    //     Gets the number of tabs in the tab strip.
    //
    // Returns:
    //     The number of tabs in the tab strip.

    public int TabCount
    {
      get
      {
        return default(int);
      }
    }

    //
    // Summary:
    //     Gets the collection of tab pages in this tab control.
    //
    // Returns:
    //     A System.Windows.Forms.TabControl.TabPageCollection that contains the System.Windows.Forms.TabPage
    //     objects in this System.Windows.Forms.TabControl.
    // public TabControl.TabPageCollection TabPages { get; }
    
    //
    // Summary:
    //     This member is not meaningful for this control.
    //
    // Returns:
    //     A System.String.
    //public override string Text { get; set; }

    // Summary:
    //     This event is not meaningful for this control.
    // public event EventHandler BackColorChanged;
    
    // Summary:
    //     Occurs when the value of the System.Windows.Forms.TabControl.BackgroundImage
    //     property changes.

    // public event EventHandler BackgroundImageChanged;
    //
    // Summary:
    //     Occurs when the value of the System.Windows.Forms.TabControl.BackgroundImageLayout
    //     property changes.
    //public event EventHandler BackgroundImageLayoutChanged;
    //
    // Summary:
    //     Occurs when a tab is deselected.
    //public event TabControlEventHandler Deselected;
    //
    // Summary:
    //     Occurs before a tab is deselected, enabling a handler to cancel the tab change.
    //public event TabControlCancelEventHandler Deselecting;
    //
    // Summary:
    //     Occurs when the System.Windows.Forms.TabControl needs to paint each of its
    //     tabs if the System.Windows.Forms.TabControl.DrawMode property is set to System.Windows.Forms.TabDrawMode.OwnerDrawFixed.
    //public event DrawItemEventHandler DrawItem;
    //
    // Summary:
    //     Occurs when the value of the System.Windows.Forms.TabControl.ForeColor property
    //     changes.
    //public event EventHandler ForeColorChanged;
    //
    // Summary:
    //     This event is not meaningful for this control.
    //public event PaintEventHandler Paint;
    //
    // Summary:
    //     Occurs when the value of the System.Windows.Forms.TabControl.RightToLeftLayout
    //     property changes.
    //public event EventHandler RightToLeftLayoutChanged;
    //
    // Summary:
    //     Occurs when a tab is selected.
    //public event TabControlEventHandler Selected;
    //
    // Summary:
    //     Occurs when the System.Windows.Forms.TabControl.SelectedIndex property has
    //     changed.
    //public event EventHandler SelectedIndexChanged;
    //
    // Summary:
    //     Occurs before a tab is selected, enabling a handler to cancel the tab change.
    //public event TabControlCancelEventHandler Selecting;
    //
    // Summary:
    //     Occurs when the value of the System.Windows.Forms.TabControl.Text property
    //     changes.
    //public event EventHandler TextChanged;

    // Summary:
    //     This member overrides System.Windows.Forms.Control.CreateControlsInstance().
    //
    // Returns:
    //     A new instance of System.Windows.Forms.Control.ControlCollection assigned
    //     to the control.
    //protected override Control.ControlCollection CreateControlsInstance();
    //
    // Summary:
    //     This member overrides System.Windows.Forms.Control.CreateHandle().
    //protected override void CreateHandle();
    //
    // Summary:
    //     Makes the tab following the tab with the specified index the current tab.
    //
    // Parameters:
    //   index:
    //     The index in the System.Windows.Forms.TabControl.TabPages collection of the
    //     tab to deselect.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     index is less than 0 or greater than the number of System.Windows.Forms.TabPage
    //     controls in the System.Windows.Forms.TabControl.TabPages collection minus
    //     1.
    public void DeselectTab(int index)
    {
      Contract.Requires(index >= 0);
    }

    //
    // Summary:
    //     Makes the tab following the tab with the specified name the current tab.
    //
    // Parameters:
    //   tabPageName:
    //     The System.Windows.Forms.Control.Name of the tab to deselect.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     tabPageName is null.-or-tabPageName does not match the System.Windows.Forms.Control.Name
    //     property of any System.Windows.Forms.TabPage in the System.Windows.Forms.TabControl.TabPages
    //     collection.
    public void DeselectTab(string tabPageName)
    {
      Contract.Requires(tabPageName != null);
    }
   

    // Summary:
    //     Makes the tab following the specified System.Windows.Forms.TabPage the current
    //     tab.
    //
    // Parameters:
    //   tabPage:
    //     The System.Windows.Forms.TabPage to deselect.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     index is less than 0 or greater than the number of System.Windows.Forms.TabPage
    //     controls in the System.Windows.Forms.TabControl.TabPages collection minus
    //     1.-or-tabPage is not in the System.Windows.Forms.TabControl.TabPages collection.
    //
    //   System.ArgumentNullException:
    //     tabPage is null.
    public void DeselectTab(TabPage tabPage)
    {
      Contract.Requires(tabPage != null);
    }

    //
    //
    // Parameters:
    //   disposing:
    //     true to release both managed and unmanaged resources; false to release only
    //     unmanaged resources.
    //protected override void Dispose(bool disposing);
    //
    // Summary:
    //     Gets the System.Windows.Forms.TabPage control at the specified location.
    //
    // Parameters:
    //   index:
    //     The index of the System.Windows.Forms.TabPage to get.
    //
    // Returns:
    //     The System.Windows.Forms.TabPage at the specified location.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     index is less than 0 or greater than the System.Windows.Forms.TabControl.TabCount.
    public Control GetControl(int index)
    {
      Contract.Requires(index >= 0);

      return default(Control);
    }


    //
    // Summary:
    //     Gets an array of System.Windows.Forms.TabPage controls that belong to the
    //     System.Windows.Forms.TabControl control.
    //
    // Returns:
    //     An array of System.Windows.Forms.TabPage controls that belong to the System.Windows.Forms.TabControl.
    //protected virtual object[] GetItems();
    //
    // Summary:
    //     Copies the System.Windows.Forms.TabPage controls in the System.Windows.Forms.TabControl
    //     to an array of the specified type.
    //
    // Parameters:
    //   baseType:
    //     The System.Type of the array to create.
    //
    // Returns:
    //     The System.Windows.Forms.TabPage controls that belong to the System.Windows.Forms.TabControl
    //     as an array of the specified type.
    //
    // Exceptions:
    //   System.ArrayTypeMismatchException:
    //     The type System.Windows.Forms.TabPage cannot be converted to baseType.
    //protected virtual object[] GetItems(Type baseType);
    //
    // Summary:
    //     Returns the bounding rectangle for a specified tab in this tab control.
    //
    // Parameters:
    //   index:
    //     The zero-based index of the tab you want.
    //
    // Returns:
    //     A System.Drawing.Rectangle that represents the bounds of the specified tab.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     The index is less than zero.-or- The index is greater than or equal to System.Windows.Forms.TabControl.TabPageCollection.Count.
    //public System.Drawing.Rectangle GetTabRect(int index);
    //
   
    // Summary:
    //     Makes the tab with the specified index the current tab.
    //
    // Parameters:
    //   index:
    //     The index in the System.Windows.Forms.TabControl.TabPages collection of the
    //     tab to select.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     index is less than 0 or greater than the number of System.Windows.Forms.TabPage
    //     controls in the System.Windows.Forms.TabControl.TabPages collection minus
    //     1.
    public void SelectTab(int index)
    {
      Contract.Requires(index >= 0);
    }
    
    // Summary:
    //     Makes the tab with the specified name the current tab.
    //
    // Parameters:
    //   tabPageName:
    //     The System.Windows.Forms.Control.Name of the tab to select.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     tabPageName is null.-or-tabPageName does not match the System.Windows.Forms.Control.Name
    //     property of any System.Windows.Forms.TabPage in the System.Windows.Forms.TabControl.TabPages
    //     collection.
    public void SelectTab(string tabPageName)
    {
      Contract.Requires(tabPageName != null);
    }
    
    //
    // Summary:
    //     Makes the specified System.Windows.Forms.TabPage the current tab.
    //
    // Parameters:
    //   tabPage:
    //     The System.Windows.Forms.TabPage to select.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     index is less than 0 or greater than the number of System.Windows.Forms.TabPage
    //     controls in the System.Windows.Forms.TabControl.TabPages collection minus
    //     1.-or-tabPage is not in the System.Windows.Forms.TabControl.TabPages collection.
    //
    //   System.ArgumentNullException:
    //     tabPage is null.
    public void SelectTab(TabPage tabPage)
  {
    Contract.Requires(tabPage != null);
  }
    
    //
    // Summary:
    //     Returns a string that represents the System.Windows.Forms.TabControl control.
    //
    // Returns:
    //     A string that represents the current System.Windows.Forms.TabControl.
    //public override string ToString();


    public TabPageCollection TabPages
    {
      get
      {
        // To add: \forall c in result. c != null
        Contract.Ensures(Contract.Result<TabPageCollection>() != null);

        return default(TabPageCollection);
      }
    }

    public class TabPageCollection
    {
    }
  }

}