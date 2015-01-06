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
  //     Represents a Windows control to display a list of items.
 // [ClassInterface(ClassInterfaceType.AutoDispatch)]
 // [Designer("System.Windows.Forms.Design.ListBoxDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
 // [DefaultEvent("SelectedIndexChanged")]
 // [DefaultProperty("Items")]
 // [DefaultBindingProperty("SelectedValue")]
 // [ComVisible(true)]
  public class ListBox// : ListControl
  {
    // Summary:
    //     Specifies the default item height for an owner-drawn System.Windows.Forms.ListBox.
    public const int DefaultItemHeight = 13;
    //
    // Summary:
    //     Specifies that no matches are found during a search.
    public const int NoMatches = -1;

    // Summary:
    //     Initializes a new instance of the System.Windows.Forms.ListBox class.
    //public ListBox();

    // Summary:
    //     Gets a value indicating whether the System.Windows.Forms.ListBox currently
    //     enables selection of list items.
    //
    // Returns:
    //     true if System.Windows.Forms.SelectionMode is not System.Windows.Forms.SelectionMode.None;
    //     otherwise, false.
    //// // protected override bool AllowSelection { get; }
    //
    //
    // Returns:
    //     A System.Drawing.Color that represents the background color of the control.
    //     The default is the value of the System.Windows.Forms.Control.DefaultBackColor
    //     property.
    // public override  System.Drawing.Color BackColor { get; set; }
    //
    // Summary:
    //     This property is not relevant to this class.
    //
    // Returns:
    //     An System.Drawing.Image.
   // [EditorBrowsable(EditorBrowsableState.Never)]
   // [Browsable(false)]
    // public override  System.Drawing.Image BackgroundImage { get; set; }
    //
    // Summary:
    //     Gets or sets the background image layout for a System.Windows.Forms.ListBox
    //     as defined in the System.Windows.Forms.ImageLayout enumeration.
    //
    // Returns:
    //     One of the values of System.Windows.Forms.ImageLayout. The values are Center,
    //     None, Stretch, Tile, or Zoom. Center is the default value.
    //
    // Exceptions:
    //   System.ComponentModel.InvalidEnumArgumentException:
    //     The specified enumeration value does not exist.
   // [Browsable(false)]
   // [EditorBrowsable(EditorBrowsableState.Never)]
    // public override  ImageLayout BackgroundImageLayout { get; set; }
    //
    // Summary:
    //     Gets or sets the type of border that is drawn around the System.Windows.Forms.ListBox.
    //
    // Returns:
    //     One of the System.Windows.Forms.BorderStyle values. The default is System.Windows.Forms.BorderStyle.Fixed3D.
    //
    // Exceptions:
    //   System.ComponentModel.InvalidEnumArgumentException:
    //     The value is not one of the System.Windows.Forms.BorderStyle values.
   // [DispId(-504)]
    //public BorderStyle BorderStyle { get; set; }
    //
    // Summary:
    //     Gets or sets the width of columns in a multicolumn System.Windows.Forms.ListBox.
    //
    // Returns:
    //     The width, in pixels, of each column in the control. The default is 0.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     A value less than zero is assigned to the property.
   // [Localizable(true)]
   // [DefaultValue(0)]
    public int ColumnWidth
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0);

        return default(int);
      }
      set
      {
        Contract.Requires(value >= 0);

      }
    }
    
    //
    //
    // Returns:
    //     A System.Windows.Forms.CreateParams that contains the required creation parameters
    //     when the handle to the control is created.
    // // protected override CreateParams CreateParams { get; }
    //
    // Summary:
    //     Gets the width of the tabs between the items in the System.Windows.Forms.ListBox.
    //
    // Returns:
    //     A collection of integers representing the tab widths.
   // [Browsable(false)]
   // [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public ListBox.IntegerCollection CustomTabOffsets
    {
      get
      {
        Contract.Ensures(Contract.Result<ListBox.IntegerCollection>() != null);
        return default(ListBox.IntegerCollection);
      }
    }

    //
    //
    // Returns:
    //     The default System.Drawing.Size of the control.
    // // protected override System.Drawing.Size DefaultSize { get; }
    //
    // Summary:
    //     Gets or sets the drawing mode for the control.
    //
    // Returns:
    //     One of the System.Windows.Forms.DrawMode values representing the mode for
    //     drawing the items of the control. The default is DrawMode.Normal.
    //
    // Exceptions:
    //   System.ComponentModel.InvalidEnumArgumentException:
    //     The value assigned to the property is not a member of the System.Windows.Forms.DrawMode
    //     enumeration.
    //
    //   System.ArgumentException:
    //     A multicolumn System.Windows.Forms.ListBox cannot have a variable-sized height.
   // [RefreshProperties(RefreshProperties.Repaint)]
    //public virtual DrawMode DrawMode { get; set; }
    //
    //
    // Returns:
    //     The System.Drawing.Font to apply to the text displayed by the control. The
    //     default is the value of the System.Windows.Forms.Control.DefaultFont property.
    // public override  System.Drawing.Font Font { get; set; }
    //
    //
    // Returns:
    //     The foreground System.Drawing.Color of the control. The default is the value
    //     of the System.Windows.Forms.Control.DefaultForeColor property.
    // public override  System.Drawing.Color ForeColor { get; set; }
    //
    // Summary:
    //     Gets or sets the width by which the horizontal scroll bar of a System.Windows.Forms.ListBox
    //     can scroll.
    //
    // Returns:
    //     The width, in pixels, that the horizontal scroll bar can scroll the control.
    //     The default is zero.
   // [DefaultValue(0)]
   // [Localizable(true)]
    //public int HorizontalExtent { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether a horizontal scroll bar is displayed
    //     in the control.
    //
    // Returns:
    //     true to display a horizontal scroll bar in the control; otherwise, false.
    //     The default is false.
   // [DefaultValue(false)]
   // [Localizable(true)]
    //public bool HorizontalScrollbar { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether the control should resize to avoid
    //     showing partial items.
    //
    // Returns:
    //     true if the control resizes so that it does not display partial items; otherwise,
    //     false. The default is true.
   // [RefreshProperties(RefreshProperties.Repaint)]
   // [DefaultValue(true)]
   // [Localizable(true)]
    //public bool IntegralHeight { get; set; }
    //
    // Summary:
    //     Gets or sets the height of an item in the System.Windows.Forms.ListBox.
    //
    // Returns:
    //     The height, in pixels, of an item in the control.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     The System.Windows.Forms.ListBox.ItemHeight property was set to less than
    //     0 or more than 255 pixels.
   // [Localizable(true)]
   // [RefreshProperties(RefreshProperties.Repaint)]
   // [DefaultValue(13)]
    public virtual int ItemHeight
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0);
        Contract.Ensures(Contract.Result<int>() <= 255);

        return default(int);
      }
      set
      {
        Contract.Requires(value >= 0);
        Contract.Requires(value <= 255);
      }
    }
    
      //
    // Summary:
    //     Gets the items of the System.Windows.Forms.ListBox.
    //
    // Returns:
    //     An System.Windows.Forms.ListBox.ObjectCollection representing the items in
    //     the System.Windows.Forms.ListBox.
   // [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
   // [Localizable(true)]
   // [MergableProperty(false)]
    public ListBox.ObjectCollection Items
    {
      get
      {
        Contract.Ensures(Contract.Result<ListBox.ObjectCollection>() != null);
        return default(ListBox.ObjectCollection);
      }
    }
    //
    // Summary:
    //     Gets or sets a value indicating whether the System.Windows.Forms.ListBox
    //     supports multiple columns.
    //
    // Returns:
    //     true if the System.Windows.Forms.ListBox supports multiple columns; otherwise,
    //     false. The default is false.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     A multicolumn System.Windows.Forms.ListBox cannot have a variable-sized height.
   // [DefaultValue(false)]
    //public bool MultiColumn { get; set; }
    //
    // Summary:
    //     This property is not relevant to this class.
    //
    // Returns:
    //     A System.Windows.Forms.Padding value.
   // [EditorBrowsable(EditorBrowsableState.Never)]
   // [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
   // [Browsable(false)]
    //public Padding Padding { get; set; }
    //
    // Summary:
    //     Gets the combined height of all items in the System.Windows.Forms.ListBox.
    //
    // Returns:
    //     The combined height, in pixels, of all items in the control.
   // [Browsable(false)]
   // [EditorBrowsable(EditorBrowsableState.Advanced)]
   // [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //public int PreferredHeight { get; }
    //
    // Summary:
    //     Gets or sets a value indicating whether text displayed by the control is
    //     displayed from right to left.
    //
    // Returns:
    //     One of the System.Windows.Forms.RightToLeft values.
    // public override  RightToLeft RightToLeft { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether the vertical scroll bar is shown
    //     at all times.
    //
    // Returns:
    //     true if the vertical scroll bar should always be displayed; otherwise, false.
    //     The default is false.
   // [Localizable(true)]
   // [DefaultValue(false)]
    //public bool ScrollAlwaysVisible { get; set; }
    //
    // Summary:
    //     Gets or sets the zero-based index of the currently selected item in a System.Windows.Forms.ListBox.
    //
    // Returns:
    //     A zero-based index of the currently selected item. A value of negative one
    //     (-1) is returned if no item is selected.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     The assigned value is less than -1 or greater than or equal to the item count.
    //
    //   System.ArgumentException:
    //     The System.Windows.Forms.ListBox.SelectionMode property is set to None.
   // [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
   // [Browsable(false)]
   // [Bindable(true)]
    // public override  int SelectedIndex { get; set; }
    //
    // Summary:
    //     Gets a collection that contains the zero-based indexes of all currently selected
    //     items in the System.Windows.Forms.ListBox.
    //
    // Returns:
    //     A System.Windows.Forms.ListBox.SelectedIndexCollection containing the indexes
    //     of the currently selected items in the control. If no items are currently
    //     selected, an empty System.Windows.Forms.ListBox.SelectedIndexCollection is
    //     returned.
   // [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
   // [Browsable(false)]
    public ListBox.SelectedIndexCollection SelectedIndices
    {
      get
      {
        Contract.Ensures(Contract.Result<ListBox.SelectedIndexCollection>() != null);
        return default(ListBox.SelectedIndexCollection);
      }
    }
    //
    // Summary:
    //     Gets or sets the currently selected item in the System.Windows.Forms.ListBox.
    //
    // Returns:
    //     An object that represents the current selection in the control.
   // [Browsable(false)]
   // [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
   // [Bindable(true)]
    //public object SelectedItem { get; set; }
    //
    // Summary:
    //     Gets a collection containing the currently selected items in the System.Windows.Forms.ListBox.
    //
    // Returns:
    //     A System.Windows.Forms.ListBox.SelectedObjectCollection containing the currently
    //     selected items in the control.
   // [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
   // [Browsable(false)]
    public ListBox.SelectedObjectCollection SelectedItems
    {
      get
      {
        Contract.Ensures(Contract.Result<ListBox.SelectedObjectCollection>() != null);
        return default(ListBox.SelectedObjectCollection);
      }
    }
    //
    // Summary:
    //     Gets or sets the method in which items are selected in the System.Windows.Forms.ListBox.
    //
    // Returns:
    //     One of the System.Windows.Forms.SelectionMode values. The default is SelectionMode.One.
    //
    // Exceptions:
    //   System.ComponentModel.InvalidEnumArgumentException:
    //     The assigned value is not one of the System.Windows.Forms.SelectionMode values.
    //public virtual SelectionMode SelectionMode { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether the items in the System.Windows.Forms.ListBox
    //     are sorted alphabetically.
    //
    // Returns:
    //     true if items in the control are sorted; otherwise, false. The default is
    //     false.
   // [DefaultValue(false)]
    //public bool Sorted { get; set; }
    //
    // Summary:
    //     Gets or searches for the text of the currently selected item in the System.Windows.Forms.ListBox.
    //
    // Returns:
    //     The text of the currently selected item in the control.
   // [Browsable(false)]
   // [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
   // [Bindable(false)]
   // [EditorBrowsable(EditorBrowsableState.Advanced)]
    // public override  string Text { get; set; }
    //
    // Summary:
    //     Gets or sets the index of the first visible item in the System.Windows.Forms.ListBox.
    //
    // Returns:
    //     The zero-based index of the first visible item in the control.
   // [Browsable(false)]
   // [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    
    // F: From the implementation I am not sure that it returns >= 0, or which is the behavior when value < 0
    //public int TopIndex { get; set; }

    //
    // Summary:
    //     Gets or sets a value indicating whether the System.Windows.Forms.ListBox
    //     recognizes and expands tab characters when it draws its strings by using
    //     the System.Windows.Forms.ListBox.CustomTabOffsets integer array.
    //
    // Returns:
    //     true if the System.Windows.Forms.ListBox recognizes and expands tab characters;
    //     otherwise, false. The default is false.
   // [Browsable(false)]
   // [DefaultValue(false)]
    //public bool UseCustomTabOffsets { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether the System.Windows.Forms.ListBox
    //     can recognize and expand tab characters when drawing its strings.
    //
    // Returns:
    //     true if the control can expand tab characters; otherwise, false. The default
    //     is true.
   // [DefaultValue(true)]
    //public bool UseTabStops { get; set; }

    // Summary:
    //     Occurs when the System.Windows.Forms.ListBox.BackgroundImage property of
    //     the label changes.
   // [EditorBrowsable(EditorBrowsableState.Never)]
   // [Browsable(false)]
    // public event  EventHandler BackgroundImageChanged;
    //
    // Summary:
    //     Occurs when the System.Windows.Forms.ListBox.BackgroundImageLayout property
    //     changes.
   // [EditorBrowsable(EditorBrowsableState.Never)]
   // [Browsable(false)]
    // public event  EventHandler BackgroundImageLayoutChanged;
    //
    // Summary:
    //     Occurs when the System.Windows.Forms.ListBox control is clicked.
   // [EditorBrowsable(EditorBrowsableState.Always)]
   // [Browsable(true)]
    // public event  EventHandler Click;
    //
    // Summary:
    //     Occurs when a visual aspect of an owner-drawn System.Windows.Forms.ListBox
    //     changes.
    // public event  DrawItemEventHandler DrawItem;
    //
    // Summary:
    //     Occurs when an owner-drawn System.Windows.Forms.ListBox is created and the
    //     sizes of the list items are determined.
    // public event  MeasureItemEventHandler MeasureItem;
    //
    // Summary:
    //     Occurs when the user clicks the System.Windows.Forms.ListBox control with
    //     the mouse pointer.
   // [Browsable(true)]
   // [EditorBrowsable(EditorBrowsableState.Always)]
    // public event  MouseEventHandler MouseClick;
    //
    // Summary:
    //     Occurs when the value of the System.Windows.Forms.ListBox.Padding property
    //     changes.
   // [Browsable(false)]
   // [EditorBrowsable(EditorBrowsableState.Never)]
    // public event  EventHandler PaddingChanged;
    //
    // Summary:
    //     Occurs when the System.Windows.Forms.ListBox control is painted.
   // [EditorBrowsable(EditorBrowsableState.Never)]
   // [Browsable(false)]
    // public event  PaintEventHandler Paint;
    //
    // Summary:
    //     Occurs when the System.Windows.Forms.ListBox.SelectedIndex property has changed.
    // public event  EventHandler SelectedIndexChanged;
    //
    // Summary:
    //     Occurs when the System.Windows.Forms.ListBox.Text property is changed.
   // [Browsable(false)]
   // [EditorBrowsable(EditorBrowsableState.Advanced)]
    // public event  EventHandler TextChanged;

    // Summary:
    //     This member is obsolete, and there is no replacement.
    //
    // Parameters:
    //   value:
    //     An array of objects.
   // [Obsolete("This method has been deprecated.  There is no replacement.  http://go.microsoft.com/fwlink/?linkid=14202")]
    // protected virtual void AddItemsCore(object[] value);
    //
    // Summary:
    //     Maintains performance while items are added to the System.Windows.Forms.ListBox
    //     one at a time by preventing the control from drawing until the System.Windows.Forms.ListBox.EndUpdate()
    //     method is called.
    //public void BeginUpdate();
    //
    // Summary:
    //     Unselects all items in the System.Windows.Forms.ListBox.
    //public void ClearSelected();
    //
    // Summary:
    //     Creates a new instance of the item collection.
    //
    // Returns:
    //     A System.Windows.Forms.ListBox.ObjectCollection that represents the new item
    //     collection.
    // protected virtual ListBox.ObjectCollection CreateItemCollection();
    //
    // Summary:
    //     Resumes painting the System.Windows.Forms.ListBox control after painting
    //     is suspended by the System.Windows.Forms.ListBox.BeginUpdate() method.
    //public void EndUpdate();
    //
    // Summary:
    //     Finds the first item in the System.Windows.Forms.ListBox that starts with
    //     the specified string.
    //
    // Parameters:
    //   s:
    //     The text to search for.
    //
    // Returns:
    //     The zero-based index of the first item found; returns ListBox.NoMatches if
    //     no match is found.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     The value of the s parameter is less than -1 or greater than or equal to
    //     the item count.
    // F: The documentation makes no sense
    public int FindString(string s)
    {
      Contract.Ensures(Contract.Result<int>() >= -1);

      return default(int);
    }
    //
    // Summary:
    //     Finds the first item in the System.Windows.Forms.ListBox that starts with
    //     the specified string. The search starts at a specific starting index.
    //
    // Parameters:
    //   s:
    //     The text to search for.
    //
    //   startIndex:
    //     The zero-based index of the item before the first item to be searched. Set
    //     to negative one (-1) to search from the beginning of the control.
    //
    // Returns:
    //     The zero-based index of the first item found; returns ListBox.NoMatches if
    //     no match is found.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     The startIndex parameter is less than zero or greater than or equal to the
    //     value of the System.Windows.Forms.ListBox.ObjectCollection.Count property
    //     of the System.Windows.Forms.ListBox.ObjectCollection class.
    public int FindString(string s, int startIndex)
    {
      Contract.Requires(startIndex >= -1);
      Contract.Requires(startIndex < this.Items.Count);
      
      Contract.Ensures(Contract.Result<int>() >= -1);

      return default(int);
    }
    //
    // Summary:
    //     Finds the first item in the System.Windows.Forms.ListBox that exactly matches
    //     the specified string.
    //
    // Parameters:
    //   s:
    //     The text to search for.
    //
    // Returns:
    //     The zero-based index of the first item found; returns ListBox.NoMatches if
    //     no match is found.
    public int FindStringExact(string s)
    {
      Contract.Ensures(Contract.Result<int>() >= -1);

      return default(int);
    }
    //
    // Summary:
    //     Finds the first item in the System.Windows.Forms.ListBox that exactly matches
    //     the specified string. The search starts at a specific starting index.
    //
    // Parameters:
    //   s:
    //     The text to search for.
    //
    //   startIndex:
    //     The zero-based index of the item before the first item to be searched. Set
    //     to negative one (-1) to search from the beginning of the control.
    //
    // Returns:
    //     The zero-based index of the first item found; returns ListBox.NoMatches if
    //     no match is found.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     The startIndex parameter is less than zero or greater than or equal to the
    //     value of the System.Windows.Forms.ListBox.ObjectCollection.Count property
    //     of the System.Windows.Forms.ListBox.ObjectCollection class.
    public int FindStringExact(string s, int startIndex)
    {
      Contract.Requires(startIndex >= -1);
      Contract.Requires(startIndex < this.Items.Count);

      Contract.Ensures(Contract.Result<int>() >= -1);

      return default(int);
    }
    //
    // Summary:
    //     Returns the height of an item in the System.Windows.Forms.ListBox.
    //
    // Parameters:
    //   index:
    //     The zero-based index of the item to return the height for.
    //
    // Returns:
    //     The height, in pixels, of the specified item.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     The specified value of the index parameter is less than zero or greater than
    //     the item count.
    public int GetItemHeight(int index)
    {
      Contract.Requires(index >= 0);
      Contract.Requires(index <= this.Items.Count); // F: it seems that == is allowed from the documentation above

      Contract.Ensures(Contract.Result<int>() >= 0);

      return default(int);
    }
    //
    // Summary:
    //     Returns the bounding rectangle for an item in the System.Windows.Forms.ListBox.
    //
    // Parameters:
    //   index:
    //     The zero-based index of item whose bounding rectangle you want to return.
    //
    // Returns:
    //     A System.Drawing.Rectangle that represents the bounding rectangle for the
    //     specified item.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     The index parameter is less than zero or greater than or equal to the value
    //     of the System.Windows.Forms.ListBox.ObjectCollection.Count property of the
    //     System.Windows.Forms.ListBox.ObjectCollection class.
    //public System.Drawing.Rectangle GetItemRectangle(int index);
    //
    // Summary:
    //     Retrieves the bounds within which the System.Windows.Forms.ListBox is scaled.
    //
    // Parameters:
    //   bounds:
    //     A System.Drawing.Rectangle that specifies the area for which to retrieve
    //     the display bounds.
    //
    //   factor:
    //     The height and width of the control's bounds.
    //
    //   specified:
    //     One of the values of System.Windows.Forms.BoundsSpecified that specifies
    //     the bounds of the control to use when defining its size and position.
    //
    // Returns:
    //     A System.Drawing.Rectangle representing the bounds within which the control
    //     is scaled.
   // [EditorBrowsable(EditorBrowsableState.Advanced)]
    // // protected override System.Drawing.Rectangle GetScaledBounds(System.Drawing.Rectangle bounds, System.Drawing.SizeF factor, BoundsSpecified specified);
    //
    // Summary:
    //     Returns a value indicating whether the specified item is selected.
    //
    // Parameters:
    //   index:
    //     The zero-based index of the item that determines whether it is selected.
    //
    // Returns:
    //     true if the specified item is currently selected in the System.Windows.Forms.ListBox;
    //     otherwise, false.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     The index parameter is less than zero or greater than or equal to the value
    //     of the System.Windows.Forms.ListBox.ObjectCollection.Count property of the
    //     System.Windows.Forms.ListBox.ObjectCollection class.
    public bool GetSelected(int index)
    {
        Contract.Requires(index >= 0);
        Contract.Requires(index < this.Items.Count);

        return default(bool);
    }
    //
    // Summary:
    //     Returns the zero-based index of the item at the specified coordinates.
    //
    // Parameters:
    //   p:
    //     A System.Drawing.Point object containing the coordinates used to obtain the
    //     item index.
    //
    // Returns:
    //     The zero-based index of the item found at the specified coordinates; returns
    //     ListBox.NoMatches if no match is found.
    public int IndexFromPoint(System.Drawing.Point p)
    {
      Contract.Ensures(Contract.Result<int>() >= -1);

      return default(int);
    }
    //
    // Summary:
    //     Returns the zero-based index of the item at the specified coordinates.
    //
    // Parameters:
    //   x:
    //     The x-coordinate of the location to search.
    //
    //   y:
    //     The y-coordinate of the location to search.
    //
    // Returns:
    //     The zero-based index of the item found at the specified coordinates; returns
    //     ListBox.NoMatches if no match is found.
    public int IndexFromPoint(int x, int y)
    {
      Contract.Ensures(Contract.Result<int>() >= -1);

      return default(int);
    }
    //
    //
    // Parameters:
    //   e:
    //     A System.Windows.Forms.UICuesEventArgs that contains the event data.
    // // protected override void OnChangeUICues(UICuesEventArgs e);
    //
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    // // protected override void OnDataSourceChanged(EventArgs e);
    //
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    // // protected override void OnDisplayMemberChanged(EventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.ListBox.DrawItem event.
    //
    // Parameters:
    //   e:
    //     A System.Windows.Forms.DrawItemEventArgs that contains the event data.
    // protected virtual void OnDrawItem(DrawItemEventArgs e);
    //
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    // // protected override void OnFontChanged(EventArgs e);
    //
    // Summary:
    //     Specifies when the window handle has been created so that column width and
    //     other characteristics can be set. Inheriting classes should call base.OnHandleCreated.
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    // // protected override void OnHandleCreated(EventArgs e);
    //
    // Summary:
    //     Overridden to be sure that items are set up and cleared out correctly. Inheriting
    //     controls should call base.OnHandleDestroyed.
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    // // protected override void OnHandleDestroyed(EventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.ListBox.MeasureItem event.
    //
    // Parameters:
    //   e:
    //     A System.Windows.Forms.MeasureItemEventArgs that contains the event data.
    // protected virtual void OnMeasureItem(MeasureItemEventArgs e);
    //
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    // // protected override void OnParentChanged(EventArgs e);
    //
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    // // protected override void OnResize(EventArgs e);
    //
    //
    // Parameters:
    //   e:
    //     Event object with the details
    // // protected override void OnSelectedIndexChanged(EventArgs e);
    //
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    // // protected override void OnSelectedValueChanged(EventArgs e);
    //
    // Summary:
    //     Forces the control to invalidate its client area and immediately redraw itself
    //     and any child controls.
    // public override  void Refresh();
    //
    // Summary:
    //     Refreshes the item contained at the specified index.
    //
    // Parameters:
    //   index:
    //     The zero-based index of the element to refresh.
    // // protected override void RefreshItem(int index);
    //
    // Summary:
    //     Refreshes all System.Windows.Forms.ListBox items and retrieves new strings
    //     for them.
    // // protected override void RefreshItems();
    //
    // public override  void ResetBackColor();
    //
    // public override  void ResetForeColor();
    //
    //
    // Parameters:
    //   factor:
    //     The factor by which the height and width of the control will be scaled.
    //
    //   specified:
    //     A System.Windows.Forms.BoundsSpecified value that specifies the bounds of
    //     the control to use when defining its size and position.
    // // protected override void ScaleControl(System.Drawing.SizeF factor, BoundsSpecified specified);
    //
    // Summary:
    //     Sets the specified bounds of the System.Windows.Forms.ListBox control.
    //
    // Parameters:
    //   x:
    //     The new System.Windows.Forms.Control.Left property value of the control.
    //
    //   y:
    //     The new System.Windows.Forms.Control.Top property value of the control.
    //
    //   width:
    //     The new System.Windows.Forms.Control.Width property value of the control.
    //
    //   height:
    //     The new System.Windows.Forms.Control.Height property value of the control.
    //
    //   specified:
    //     A bitwise combination of the System.Windows.Forms.BoundsSpecified values.
    // // protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified);
    //
    // Summary:
    //     Sets the object with the specified index in the derived class.
    //
    // Parameters:
    //   index:
    //     The array index of the object.
    //
    //   value:
    //     The object.
    // // protected override void SetItemCore(int index, object value);
    //
    // Summary:
    //     Clears the contents of the System.Windows.Forms.ListBox and adds the specified
    //     items to the control.
    //
    // Parameters:
    //   value:
    //     An array of objects to insert into the control.
    // // protected override void SetItemsCore(IList value);
    //
    // Summary:
    //     Selects or clears the selection for the specified item in a System.Windows.Forms.ListBox.
    //
    // Parameters:
    //   index:
    //     The zero-based index of the item in a System.Windows.Forms.ListBox to select
    //     or clear the selection for.
    //
    //   value:
    //     true to select the specified item; otherwise, false.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     The specified index was outside the range of valid values.
    //
    //   System.InvalidOperationException:
    //     The System.Windows.Forms.ListBox.SelectionMode property was set to None.
    public void SetSelected(int index, bool value)
    {
      Contract.Requires(index >= 0);
      Contract.Requires(index < this.Items.Count);

    }
    //
    // Summary:
    //     Sorts the items in the System.Windows.Forms.ListBox.
    // protected virtual void Sort();
    //
    // Summary:
    //     Returns a string representation of the System.Windows.Forms.ListBox.
    //
    // Returns:
    //     A string that states the control type, the count of items in the System.Windows.Forms.ListBox
    //     control, and the Text property of the first item in the System.Windows.Forms.ListBox,
    //     if the count is not 0.
    // public override  string ToString();
    //
    // Summary:
    //     Processes the command message the System.Windows.Forms.ListView control receives
    //     from the top-level window.
    //
    // Parameters:
    //   m:
    //     The System.Windows.Forms.Message the top-level window sent to the System.Windows.Forms.ListBox
    //     control.
    // protected virtual void WmReflectCommand(ref Message m);
    //
    // Summary:
    //     The list's window procedure.
    //
    // Parameters:
    //   m:
    //     A Windows Message Object.
    // // protected override void WndProc(ref Message m);

    // Summary:
    //     Represents a collection of integers in a System.Windows.Forms.ListBox.
    public class IntegerCollection// : IList, ICollection, IEnumerable
    {
      // Summary:
      //     Initializes a new instance of the System.Windows.Forms.ListBox.IntegerCollection
      //     class.
      //
      // Parameters:
      //   owner:
      //     The System.Windows.Forms.ListBox that owns the collection.
      //public IntegerCollection(ListBox owner);

      // Summary:
      //     Gets the number of selected items in the System.Windows.Forms.ListBox.
      //
      // Returns:
      //     The number of selected items in the System.Windows.Forms.ListBox.
     // [Browsable(false)]
      extern public virtual int Count { get; }

      // Summary:
      //     Gets or sets the System.Windows.Forms.ListBox.IntegerCollection.this[System.Int32]
      //     having the specified index.
      //
      // Parameters:
      //   index:
      //     The position of the System.Windows.Forms.ListBox.IntegerCollection.this[System.Int32]
      //     in the collection.
      //
      // Returns:
      //     The selected System.Windows.Forms.ListBox.IntegerCollection.this[System.Int32]
      //     at the specified position.
      //public int this[int index] { get; set; }

      // Summary:
      //     Adds a unique integer to the collection in sorted order.
      //
      // Parameters:
      //   item:
      //     The integer to add to the collection.
      //
      // Returns:
      //     The index of the added item.
      //
      // Exceptions:
      //   System.SystemException:
      //     There is insufficient space available to store the new item.
      //public int Add(int item);
      //
      // Summary:
      //     Adds an array of integers to the collection.
      //
      // Parameters:
      //   items:
      //     The array of integers to add to the collection.
      //public void AddRange(int[] items);
      //
      // Summary:
      //     Adds the contents of an existing System.Windows.Forms.ListBox.IntegerCollection
      //     to another collection.
      //
      // Parameters:
      //   value:
      //     The System.Windows.Forms.ListBox.IntegerCollection to add to another collection.
      //public void AddRange(ListBox.IntegerCollection value);
      //
      // Summary:
      //     Removes all integers from the System.Windows.Forms.ListBox.IntegerCollection.
      //public void Clear();
      //
      // Summary:
      //     Determines whether the specified integer is in the collection.
      //
      // Parameters:
      //   item:
      //     The integer to search for in the collection.
      //
      // Returns:
      //     true if the specified integer is in the collection; otherwise, false.
      //public bool Contains(int item);
      //
      // Summary:
      //     Copies the entire System.Windows.Forms.ListBox.IntegerCollection into an
      //     existing array of integers at a specified location within the array.
      //
      // Parameters:
      //   destination:
      //     The array into which the System.Windows.Forms.ListBox.IntegerCollection is
      //     copied.
      //
      //   index:
      //     The location within the destination array to which to copy the System.Windows.Forms.ListBox.IntegerCollection.
      //public void CopyTo(Array destination, int index);
      //
      // Summary:
      //     Retrieves the index within the System.Windows.Forms.ListBox.IntegerCollection
      //     of the specified integer.
      //
      // Parameters:
      //   item:
      //     The integer for which to retrieve the index.
      //
      // Returns:
      //     The zero-based index of the integer in the System.Windows.Forms.ListBox.IntegerCollection;
      //     otherwise, negative one (-1).
      //public int IndexOf(int item);
      //
      // Summary:
      //     Removes the specified integer from the System.Windows.Forms.ListBox.IntegerCollection.
      //
      // Parameters:
      //   item:
      //     The integer to remove from the System.Windows.Forms.ListBox.IntegerCollection.
      //public void Remove(int item);
      ////
      // Summary:
      //     Removes the integer at the specified index from the System.Windows.Forms.ListBox.IntegerCollection.
      //
      // Parameters:
      //   index:
      //     The zero-based index of the integer to remove.
      //public void RemoveAt(int index);
    }

    // Summary:
    //     Represents the collection of items in a System.Windows.Forms.ListBox.
   // [ListBindable(false)]
    public class ObjectCollection// : IList, ICollection, IEnumerable
    {
      // Summary:
      //     Initializes a new instance of System.Windows.Forms.ListBox.ObjectCollection.
      //
      // Parameters:
      //   owner:
      //     The System.Windows.Forms.ListBox that owns the collection.
      //public ObjectCollection(ListBox owner);
      //
      // Summary:
      //     Initializes a new instance of System.Windows.Forms.ListBox.ObjectCollection
      //     based on another System.Windows.Forms.ListBox.ObjectCollection.
      //
      // Parameters:
      //   owner:
      //     The System.Windows.Forms.ListBox that owns the collection.
      //
      //   value:
      //     A System.Windows.Forms.ListBox.ObjectCollection from which the contents are
      //     copied to this collection.
      //public ObjectCollection(ListBox owner, ListBox.ObjectCollection value);
      //
      // Summary:
      //     Initializes a new instance of System.Windows.Forms.ListBox.ObjectCollection
      //     containing an array of objects.
      //
      // Parameters:
      //   owner:
      //     The System.Windows.Forms.ListBox that owns the collection.
      //
      //   value:
      //     An array of objects to add to the collection.
      //public ObjectCollection(ListBox owner, object[] value);

      // Summary:
      //     Gets the number of items in the collection.
      //
      // Returns:
      //     The number of items in the collection
      extern public virtual int Count { get; }
      //
      // Summary:
      //     Gets a value indicating whether the collection is read-only.
      //
      // Returns:
      //     true if this collection is read-only; otherwise, false.
      //public bool IsReadOnly { get; }

      // Summary:
      //     Gets or sets the item at the specified index within the collection.
      //
      // Parameters:
      //   index:
      //     The index of the item in the collection to get or set.
      //
      // Returns:
      //     An object representing the item located at the specified index within the
      //     collection.
      //
      // Exceptions:
      //   System.ArgumentOutOfRangeException:
      //     The index parameter is less than zero or greater than or equal to the value
      //     of the System.Windows.Forms.ListBox.ObjectCollection.Count property of the
      //     System.Windows.Forms.ListBox.ObjectCollection class.
     // [Browsable(false)]
     // [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
      //public virtual object this[int index] { get; set; }

      // Summary:
      //     Adds an item to the list of items for a System.Windows.Forms.ListBox.
      //
      // Parameters:
      //   item:
      //     An object representing the item to add to the collection.
      //
      // Returns:
      //     The zero-based index of the item in the collection, or -1 if System.Windows.Forms.ListBox.BeginUpdate()
      //     has been called.
      //
      // Exceptions:
      //   System.SystemException:
      //     There is insufficient space available to add the new item to the list.
      //public int Add(object item);
      //
      // Summary:
      //     Adds the items of an existing System.Windows.Forms.ListBox.ObjectCollection
      //     to the list of items in a System.Windows.Forms.ListBox.
      //
      // Parameters:
      //   value:
      //     A System.Windows.Forms.ListBox.ObjectCollection to load into this collection.
      //public void AddRange(ListBox.ObjectCollection value);
      //
      // Summary:
      //     Adds an array of items to the list of items for a System.Windows.Forms.ListBox.
      //
      // Parameters:
      //   items:
      //     An array of objects to add to the list.
      //public void AddRange(object[] items);
      //
      // Summary:
      //     Removes all items from the collection.
      //public virtual void Clear();
      //
      // Summary:
      //     Determines whether the specified item is located within the collection.
      //
      // Parameters:
      //   value:
      //     An object representing the item to locate in the collection.
      //
      // Returns:
      //     true if the item is located within the collection; otherwise, false.
      //public bool Contains(object value);
      //
      // Summary:
      //     Copies the entire collection into an existing array of objects at a specified
      //     location within the array.
      //
      // Parameters:
      //   destination:
      //     The object array in which the items from the collection are copied to.
      //
      //   arrayIndex:
      //     The location within the destination array to copy the items from the collection
      //     to.
      //public void CopyTo(object[] destination, int arrayIndex);
      //
      // Summary:
      //     Returns an enumerator to use to iterate through the item collection.
      //
      // Returns:
      //     An System.Collections.IEnumerator that represents the item collection.
      //public IEnumerator GetEnumerator();
      //
      // Summary:
      //     Returns the index within the collection of the specified item.
      //
      // Parameters:
      //   value:
      //     An object representing the item to locate in the collection.
      //
      // Returns:
      //     The zero-based index where the item is located within the collection; otherwise,
      //     negative one (-1).
      //
      // Exceptions:
      //   System.ArgumentNullException:
      //     The value parameter is null.
      //public int IndexOf(object value);
      //
      // Summary:
      //     Inserts an item into the list box at the specified index.
      //
      // Parameters:
      //   index:
      //     The zero-based index location where the item is inserted.
      //
      //   item:
      //     An object representing the item to insert.
      //
      // Exceptions:
      //   System.ArgumentOutOfRangeException:
      //     The index parameter is less than zero or greater than value of the System.Windows.Forms.ListBox.ObjectCollection.Count
      //     property of the System.Windows.Forms.ListBox.ObjectCollection class.
      //public void Insert(int index, object item);
      //
      // Summary:
      //     Removes the specified object from the collection.
      //
      // Parameters:
      //   value:
      //     An object representing the item to remove from the collection.
      //public void Remove(object value);
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
      //     The index parameter is less than zero or greater than or equal to the value
      //     of the System.Windows.Forms.ListBox.ObjectCollection.Count property of the
      //     System.Windows.Forms.ListBox.ObjectCollection class.
      //public void RemoveAt(int index);
    }

    // Summary:
    //     Represents the collection containing the indexes to the selected items in
    //     a System.Windows.Forms.ListBox.
    public class SelectedIndexCollection// : IList, ICollection, IEnumerable
    {
      // Summary:
      //     Initializes a new instance of the System.Windows.Forms.ListBox.SelectedIndexCollection
      //     class.
      //
      // Parameters:
      //   owner:
      //     A System.Windows.Forms.ListBox representing the owner of the collection.
      //public SelectedIndexCollection(ListBox owner);

      // Summary:
      //     Gets the number of items in the collection.
      //
      // Returns:
      //     The number of items in the collection.
     // [Browsable(false)]
      extern public virtual int Count { get; }
      //
      // Summary:
      //     Gets a value indicating whether the collection is read-only.
      //
      // Returns:
      //     true if the collection is read-only; otherwise, false.
      //public bool IsReadOnly { get; }

      // Summary:
      //     Gets the index value at the specified index within this collection.
      //
      // Parameters:
      //   index:
      //     The index of the item in the collection to get.
      //
      // Returns:
      //     The index value from the System.Windows.Forms.ListBox.ObjectCollection that
      //     is stored at the specified location.
      //
      // Exceptions:
      //   System.ArgumentOutOfRangeException:
      //     The index parameter is less than zero or greater than or equal to the value
      //     of the System.Windows.Forms.ListBox.SelectedIndexCollection.Count property
      //     of the System.Windows.Forms.ListBox.SelectedIndexCollection class.
      //public int this[int index] { get; }

      // Summary:
      //     Adds the System.Windows.Forms.ListBox at the specified index location.
      //
      // Parameters:
      //   index:
      //     The location in the array at which to add the System.Windows.Forms.ListBox.
      //public void Add(int index);
      //
      // Summary:
      //     Removes all controls from the collection.
      //public void Clear();
      //
      // Summary:
      //     Determines whether the specified index is located within the collection.
      //
      // Parameters:
      //   selectedIndex:
      //     The index to locate in the collection.
      //
      // Returns:
      //     true if the specified index from the System.Windows.Forms.ListBox.ObjectCollection
      //     for the System.Windows.Forms.ListBox is an item in this collection; otherwise,
      //     false.
      //public bool Contains(int selectedIndex);
      //
      // Summary:
      //     Copies the entire collection into an existing array at a specified location
      //     within the array.
      //
      // Parameters:
      //   destination:
      //     The destination array.
      //
      //   index:
      //     The index in the destination array at which storing begins.
      //public void CopyTo(Array destination, int index);
      //
      // Summary:
      //     Returns an enumerator to use to iterate through the selected indexes collection.
      //
      // Returns:
      //     An System.Collections.IEnumerator that represents the selected indexes collection.
      //public IEnumerator GetEnumerator();
      //
      // Summary:
      //     Returns the index within the System.Windows.Forms.ListBox.SelectedIndexCollection
      //     of the specified index from the System.Windows.Forms.ListBox.ObjectCollection
      //     of the System.Windows.Forms.ListBox.
      //
      // Parameters:
      //   selectedIndex:
      //     The zero-based index from the System.Windows.Forms.ListBox.ObjectCollection
      //     to locate in this collection.
      //
      // Returns:
      //     The zero-based index in the collection where the specified index of the System.Windows.Forms.ListBox.ObjectCollection
      //     was located within the System.Windows.Forms.ListBox.SelectedIndexCollection;
      //     otherwise, negative one (-1).
      ////public int IndexOf(int selectedIndex);
      //
      // Summary:
      //     Removes the specified control from the collection.
      //
      // Parameters:
      //   index:
      //     The control to be removed.
      //public void Remove(int index);
    }

    // Summary:
    //     Represents the collection of selected items in the System.Windows.Forms.ListBox.
    public class SelectedObjectCollection // : IList, ICollection, IEnumerable
    {
      // Summary:
      //     Initializes a new instance of the System.Windows.Forms.ListBox.SelectedObjectCollection
      //     class.
      //
      // Parameters:
      //   owner:
      //     A System.Windows.Forms.ListBox representing the owner of the collection.
      //public SelectedObjectCollection(ListBox owner);

      // Summary:
      //     Gets the number of items in the collection.
      //
      // Returns:
      //     The number of items in the collection.
      extern public virtual int Count { get; }
      //
      // Summary:
      //     Gets a value indicating whether the collection is read-only.
      //
      // Returns:
      //     true if the collection is read-only; otherwise, false.
      //public bool IsReadOnly { get; }

      // Summary:
      //     Gets the item at the specified index within the collection.
      //
      // Parameters:
      //   index:
      //     The index of the item in the collection to retrieve.
      //
      // Returns:
      //     An object representing the item located at the specified index within the
      //     collection.
      //
      // Exceptions:
      //   System.ArgumentOutOfRangeException:
      //     The index parameter is less than zero or greater than or equal to the value
      //     of the System.Windows.Forms.ListBox.ObjectCollection.Count property of the
      //     System.Windows.Forms.ListBox.SelectedObjectCollection class.
     // [Browsable(false)]
     // [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
      //public object this[int index] { get; set; }

      // Summary:
      //     Adds an item to the list of selected items for a System.Windows.Forms.ListBox.
      //
      // Parameters:
      //   value:
      //     An object representing the item to add to the collection of selected items.
      //public void Add(object value);
      //
      // Summary:
      //     Removes all items from the collection of selected items.
      //public void Clear();
      //
      // Summary:
      //     Determines whether the specified item is located within the collection.
      //
      // Parameters:
      //   selectedObject:
      //     An object representing the item to locate in the collection.
      //
      // Returns:
      //     true if the specified item is located in the collection; otherwise, false.
      //public bool Contains(object selectedObject);
      //
      // Summary:
      //     Copies the entire collection into an existing array at a specified location
      //     within the array.
      //
      // Parameters:
      //   destination:
      //     An System.Array representing the array to copy the contents of the collection
      //     to.
      //
      //   index:
      //     The location within the destination array to copy the items from the collection
      //     to.
      //public void CopyTo(Array destination, int index);
      //
      // Summary:
      //     Returns an enumerator that can be used to iterate through the selected item
      //     collection.
      //
      // Returns:
      //     An System.Collections.IEnumerator that represents the item collection.
      //public IEnumerator GetEnumerator();
      //
      // Summary:
      //     Returns the index within the collection of the specified item.
      //
      // Parameters:
      //   selectedObject:
      //     An object representing the item to locate in the collection.
      //
      // Returns:
      //     The zero-based index of the item in the collection; otherwise, -1.
      //public int IndexOf(object selectedObject);
      //
      // Summary:
      //     Removes the specified object from the collection of selected items.
      //
      // Parameters:
      //   value:
      //     An object representing the item to remove from the collection.
      //public void Remove(object value);
    }
  }
}
