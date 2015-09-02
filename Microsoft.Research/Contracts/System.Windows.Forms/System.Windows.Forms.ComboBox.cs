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
  //     Represents a Windows combo box control.
 // [DefaultBindingProperty("Text")]
 // [ClassInterface(ClassInterfaceType.AutoDispatch)]
 // [DefaultEvent("SelectedIndexChanged")]
 // [DefaultProperty("Items")]
 // [ComVisible(true)]
 // [Designer("System.Windows.Forms.Design.ComboBoxDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
  public class ComboBox //: ListControl
  {
    // Summary:
    //     Initializes a new instance of the System.Windows.Forms.ComboBox class.
    //public ComboBox();

    // Summary:
    //     Gets or sets a custom System.Collections.Specialized.StringCollection to
    //     use when the System.Windows.Forms.ComboBox.AutoCompleteSource property is
    //     set to CustomSource.
    //
    // Returns:
    //     A System.Collections.Specialized.StringCollection to use with System.Windows.Forms.ComboBox.AutoCompleteSource.
   // [Browsable(true)]
   // [Localizable(true)]
   // [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
   // [EditorBrowsable(EditorBrowsableState.Always)]
    //public AutoCompleteStringCollection AutoCompleteCustomSource { get; set; }
    //
    // Summary:
    //     Gets or sets an option that controls how automatic completion works for the
    //     System.Windows.Forms.ComboBox.
    //
    // Returns:
    //     One of the values of System.Windows.Forms.AutoCompleteMode. The values are
    //     System.Windows.Forms.AutoCompleteMode.Append, System.Windows.Forms.AutoCompleteMode.None,
    //     System.Windows.Forms.AutoCompleteMode.Suggest, and System.Windows.Forms.AutoCompleteMode.SuggestAppend.
    //     The default is System.Windows.Forms.AutoCompleteMode.None.
    //
    // Exceptions:
    //   System.ComponentModel.InvalidEnumArgumentException:
    //     The specified value is not one of the values of System.Windows.Forms.AutoCompleteMode.
   // [EditorBrowsable(EditorBrowsableState.Always)]
   // [Browsable(true)]
    //public AutoCompleteMode AutoCompleteMode { get; set; }
    //
    // Summary:
    //     Gets or sets a value specifying the source of complete strings used for automatic
    //     completion.
    //
    // Returns:
    //     One of the values of System.Windows.Forms.AutoCompleteSource. The options
    //     are AllSystemSources, AllUrl, FileSystem, HistoryList, RecentlyUsedList,
    //     CustomSource, and None. The default is None.
    //
    // Exceptions:
    //   System.ComponentModel.InvalidEnumArgumentException:
    //     The specified value is not one of the values of System.Windows.Forms.AutoCompleteSource.
   // [EditorBrowsable(EditorBrowsableState.Always)]
   // [Browsable(true)]
    //public AutoCompleteSource AutoCompleteSource { get; set; }
    //
    //
    // Returns:
    //     A System.Drawing.Color that represents the background color of the control.
    //     The default is the value of the System.Windows.Forms.Control.DefaultBackColor
    //     property.
    // public override System.Drawing.Color BackColor { get; set; }
    //
    // Summary:
    //     This property is not relevant to this class.
    //
    // Returns:
    //     An System.Drawing.Image.
   // [EditorBrowsable(EditorBrowsableState.Never)]
   // [Browsable(false)]
    // public override System.Drawing.Image BackgroundImage { get; set; }
    //
    // Summary:
    //     Gets or sets the background image layout as defined in the System.Windows.Forms.ImageLayout
    //     enumeration.
    //
    // Returns:
    //     One of the values of System.Windows.Forms.ImageLayout (Center, None, Stretch,
    //     Tile, or Zoom).
    //
    // Exceptions:
    //   System.ComponentModel.InvalidEnumArgumentException:
    //     The specified value is not one of the values of System.Windows.Forms.ImageLayout.
   // [Browsable(false)]
   // [EditorBrowsable(EditorBrowsableState.Never)]
    // public override ImageLayout BackgroundImageLayout { get; set; }
    //
    // Summary:
    //     Gets the required creation parameters when the control handle is created.
    //
    // Returns:
    //     A System.Windows.Forms.CreateParams that contains the required creation parameters
    //     when the handle to the control is created.
    //// protected override CreateParams CreateParams { get; }
    //
    // Summary:
    //     Gets or sets the data source for this System.Windows.Forms.ComboBox.
    //
    // Returns:
    //     An object that implements the System.Collections.IList interface, such as
    //     a System.Data.DataSet or an System.Array. The default is null.
   // [DefaultValue("")]
   // [RefreshProperties(RefreshProperties.Repaint)]
   // [AttributeProvider(typeof(IListSource))]
    //public object DataSource { get; set; }
    //
    //
    // Returns:
    //     The default System.Drawing.Size of the control.
    // protected override System.Drawing.Size DefaultSize { get; }
    //
    // Summary:
    //     Gets or sets a value indicating whether your code or the operating system
    //     will handle drawing of elements in the list.
    //
    // Returns:
    //     One of the System.Windows.Forms.DrawMode enumeration values. The default
    //     is System.Windows.Forms.DrawMode.Normal.
    //
    // Exceptions:
    //   System.ComponentModel.InvalidEnumArgumentException:
    //     The value is not a valid System.Windows.Forms.DrawMode enumeration value.
   // [RefreshProperties(RefreshProperties.Repaint)]
    //public DrawMode DrawMode { get; set; }
    //
    // Summary:
    //     Gets or sets the height in pixels of the drop-down portion of the System.Windows.Forms.ComboBox.
    //
    // Returns:
    //     The height, in pixels, of the drop-down box.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The specified value is less than one.
   // [EditorBrowsable(EditorBrowsableState.Always)]
   // [DefaultValue(106)]
   // [Browsable(true)]
    public int DropDownHeight 
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 1);

        return default(int);
      }
      set
      {
        Contract.Requires(value >= 1);
      }
    }


    //
    // Summary:
    //     Gets or sets a value specifying the style of the combo box.
    //
    // Returns:
    //     One of the System.Windows.Forms.ComboBoxStyle values. The default is DropDown.
    //
    // Exceptions:
    //   System.ComponentModel.InvalidEnumArgumentException:
    //     The assigned value is not one of the System.Windows.Forms.ComboBoxStyle values.
   // [RefreshProperties(RefreshProperties.Repaint)]
    //public ComboBoxStyle DropDownStyle { get; set; }
    //
    // Summary:
    //     Gets or sets the width of the of the drop-down portion of a combo box.
    //
    // Returns:
    //     The width, in pixels, of the drop-down box.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The specified value is less than one.
    public int DropDownWidth
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 1);

        return default(int);
      }
      set
      {
        Contract.Requires(value >= 1);
      }
    }

    //
    // Summary:
    //     Gets or sets a value indicating whether the combo box is displaying its drop-down
    //     portion.
    //
    // Returns:
    //     true if the drop-down portion is displayed; otherwise, false. The default
    //     is false.
   // [Browsable(false)]
   // [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //public bool DroppedDown { get; set; }
    //
    // Summary:
    //     Gets or sets the appearance of the System.Windows.Forms.ComboBox.
    //
    // Returns:
    //     One of the values of System.Windows.Forms.FlatStyle. The options are Flat,
    //     Popup, Standard, and System. The default is Standard.
    //
    // Exceptions:
    //   System.ComponentModel.InvalidEnumArgumentException:
    //     The specified value is not one of the values of System.Windows.Forms.FlatStyle.
   // [Localizable(true)]
    //public FlatStyle FlatStyle { get; set; }
    //
    // Summary:
    //     Gets a value indicating whether the System.Windows.Forms.ComboBox has focus.
    //
    // Returns:
    //     true if this control has focus; otherwise, false.
    // public override bool Focused { get; }
    //
    //
    // Returns:
    //     The foreground System.Drawing.Color of the control. The default is the value
    //     of the System.Windows.Forms.Control.DefaultForeColor property.
    // public override System.Drawing.Color ForeColor { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether the control should resize to avoid
    //     showing partial items.
    //
    // Returns:
    //     true if the list portion can contain only complete items; otherwise, false.
    //     The default is true.
   // [Localizable(true)]
   // [DefaultValue(true)]
    //public bool IntegralHeight { get; set; }
    //
    // Summary:
    //     Gets or sets the height of an item in the combo box.
    //
    // Returns:
    //     The height, in pixels, of an item in the combo box.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The item height value is less than zero.
   // [Localizable(true)]
    public int ItemHeight
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
    // Summary:
    //     Gets an object representing the collection of the items contained in this
    //     System.Windows.Forms.ComboBox.
    //
    // Returns:
    //     A System.Windows.Forms.ComboBox.ObjectCollection representing the items in
    //     the System.Windows.Forms.ComboBox.
   // [MergableProperty(false)]
   // [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
   // [Localizable(true)]
    public ComboBox.ObjectCollection Items 
    {
      get
      {
        Contract.Ensures(Contract.Result<ComboBox.ObjectCollection>() != null);

        return default(ComboBox.ObjectCollection);
      }
    }
    
    //
    // Summary:
    //     Gets or sets the maximum number of items to be shown in the drop-down portion
    //     of the System.Windows.Forms.ComboBox.
    //
    // Returns:
    //     The maximum number of items of in the drop-down portion. The minimum for
    //     this property is 1 and the maximum is 100.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The maximum number is set less than one or greater than 100.
   // [DefaultValue(8)]
   // [Localizable(true)]
    public int MaxDropDownItems 
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0);
        Contract.Ensures(Contract.Result<int>() <= 100);

        return default(int);
      }
      set
      {
        Contract.Requires(value >= 0);
        Contract.Requires(value <= 100);
      }
    }
    
    
    //
    //
    // Returns:
    //     An ordered pair of type System.Drawing.Size representing the width and height
    //     of a rectangle.
    // public override System.Drawing.Size MaximumSize { get; set; }
    //
    // Summary:
    //     Gets or sets the number of characters a user can type into the System.Windows.Forms.ComboBox.
    //
    // Returns:
    //     The maximum number of characters a user can enter. Values of less than zero
    //     are reset to zero, which is the default value.
   // [Localizable(true)]
   // [DefaultValue(0)]
    //public int MaxLength { get; set; }
    //
    //
    // Returns:
    //     An ordered pair of type System.Drawing.Size representing the width and height
    //     of a rectangle.
    // public override System.Drawing.Size MinimumSize { get; set; }
    //
    // Summary:
    //     This property is not relevant for this class.
    //
    // Returns:
    //     A System.Windows.Forms.Padding value.
   // [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
   // [EditorBrowsable(EditorBrowsableState.Never)]
   // [Browsable(false)]
    //public Padding Padding { get; set; }
    //
    // Summary:
    //     Gets the preferred height of the System.Windows.Forms.ComboBox.
    //
    // Returns:
    //     The preferred height, in pixels, of the item area of the combo box.
   // [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
   // [Browsable(false)]
    //public int PreferredHeight { get; }
    //
    // Summary:
    //     Gets or sets the index specifying the currently selected item.
    //
    // Returns:
    //     A zero-based index of the currently selected item. A value of negative one
    //     (-1) is returned if no item is selected.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     The specified index is less than or equal to -2.  -or- The specified index
    //     is greater than or equal to the number of items in the combo box.
   // [Browsable(false)]
   // [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    // public override int SelectedIndex { get; set; }
    //
    // Summary:
    //     Gets or sets currently selected item in the System.Windows.Forms.ComboBox.
    //
    // Returns:
    //     The object that is the currently selected item or null if there is no currently
    //     selected item.
   // [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
   // [Browsable(false)]
   // [Bindable(true)]
    //public object SelectedItem { get; set; }
    //
    // Summary:
    //     Gets or sets the text that is selected in the editable portion of a System.Windows.Forms.ComboBox.
    //
    // Returns:
    //     A string that represents the currently selected text in the combo box. If
    //     System.Windows.Forms.ComboBox.DropDownStyle is set to System.Windows.Forms.ComboBoxStyle.DropDownList,
    //     the return value is an empty string ("").
   // [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
   // [Browsable(false)]
    //public string SelectedText { get; set; }
    //
    // Summary:
    //     Gets or sets the number of characters selected in the editable portion of
    //     the combo box.
    //
    // Returns:
    //     The number of characters selected in the combo box.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The value was less than zero.
   // [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
   // [Browsable(false)]
    public int SelectionLength
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
    // Summary:
    //     Gets or sets the starting index of text selected in the combo box.
    //
    // Returns:
    //     The zero-based index of the first character in the string of the current
    //     text selection.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The value is less than zero.
   // [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
   // [Browsable(false)]
    public int SelectionStart
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
    // Summary:
    //     Gets or sets a value indicating whether the items in the combo box are sorted.
    //
    // Returns:
    //     true if the combo box is sorted; otherwise, false. The default is false.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     An attempt was made to sort a System.Windows.Forms.ComboBox that is attached
    //     to a data source.
   // [DefaultValue(false)]
    //public bool Sorted { get; set; }
    //
    // Summary:
    //     Gets or sets the text associated with this control.
    //
    // Returns:
    //     The text associated with this control.
   // [Localizable(true)]
   // [Bindable(true)]
    // public override string Text { get; set; }

    // Summary:
    //     Occurs when the value of the System.Windows.Forms.ComboBox.BackgroundImage
    //     property changes.
   // [EditorBrowsable(EditorBrowsableState.Never)]
   // [Browsable(false)]
    // public event EventHandler BackgroundImageChanged;
    //
    // Summary:
    //     Occurs when the System.Windows.Forms.ComboBox.BackgroundImageLayout property
    //     changes.
   // [EditorBrowsable(EditorBrowsableState.Never)]
   // [Browsable(false)]
    // public event EventHandler BackgroundImageLayoutChanged;
    //
    // Summary:
    //     This event is not relevant for this class.
   // [EditorBrowsable(EditorBrowsableState.Never)]
   // [Browsable(false)]
    // public event EventHandler DoubleClick;
    //
    // Summary:
    //     Occurs when a visual aspect of an owner-drawn System.Windows.Forms.ComboBox
    //     changes.
    // public event DrawItemEventHandler DrawItem;
    //
    // Summary:
    //     Occurs when the drop-down portion of a System.Windows.Forms.ComboBox is shown.
    // public event EventHandler DropDown;
    //
    // Summary:
    //     Occurs when the drop-down portion of the System.Windows.Forms.ComboBox is
    //     no longer visible.
    // public event EventHandler DropDownClosed;
    //
    // Summary:
    //     Occurs when the System.Windows.Forms.ComboBox.DropDownStyle property has
    //     changed.
    // public event EventHandler DropDownStyleChanged;
    //
    // Summary:
    //     Occurs each time an owner-drawn System.Windows.Forms.ComboBox item needs
    //     to be drawn and when the sizes of the list items are determined.
    // public event MeasureItemEventHandler MeasureItem;
    //
    // Summary:
    //     This event is not relevant to this class.
   // [Browsable(false)]
   // [EditorBrowsable(EditorBrowsableState.Never)]
    // public event EventHandler PaddingChanged;
    //
    // Summary:
    //     Occurs when the System.Windows.Forms.ComboBox control is redrawn.
   // [EditorBrowsable(EditorBrowsableState.Never)]
   // [Browsable(false)]
    // public event PaintEventHandler Paint;
    //
    // Summary:
    //     Occurs when the System.Windows.Forms.ComboBox.SelectedIndex property has
    //     changed.
    // public event EventHandler SelectedIndexChanged;
    //
    // Summary:
    //     Occurs when the selected item has changed and that change is displayed in
    //     the System.Windows.Forms.ComboBox.
    // public event EventHandler SelectionChangeCommitted;
    //
    // Summary:
    //     Occurs when the control has formatted the text, but before the text is displayed.
    // public event EventHandler TextUpdate;

    // Summary:
    //     Adds the specified items to the combo box.
    //
    // Parameters:
    //   value:
    //     The items to add.
   // [Obsolete("This method has been deprecated.  There is no replacement.  http://go.microsoft.com/fwlink/?linkid=14202")]
    //// protected virtual void AddItemsCore(object[] value);
    //
    // Summary:
    //     Maintains performance when items are added to the System.Windows.Forms.ComboBox
    //     one at a time.
    //public void BeginUpdate();
    //
    //
    // Returns:
    //     A new System.Windows.Forms.AccessibleObject for the control.
    // protected override AccessibleObject CreateAccessibilityInstance();
    //
    // Summary:
    //     Creates a handle for the control.
    // protected override void CreateHandle();
    //
    // Summary:
    //     Releases the unmanaged resources used by the System.Windows.Forms.ComboBox
    //     and optionally releases the managed resources.
    //
    // Parameters:
    //   disposing:
    //     true to release both managed and unmanaged resources; false to release only
    //     unmanaged resources.
    // protected override void Dispose(bool disposing);
    //
    // Summary:
    //     Resumes painting the System.Windows.Forms.ComboBox control after painting
    //     is suspended by the System.Windows.Forms.ComboBox.BeginUpdate() method.
    //public void EndUpdate();
    //
    // Summary:
    //     Finds the first item in the combo box that starts with the specified string.
    //
    // Parameters:
    //   s:
    //     The System.String to search for.
    //
    // Returns:
    //     The zero-based index of the first item found; returns -1 if no match is found.
    public int FindString(string s)
    {
      Contract.Ensures(Contract.Result<int>() >= -1);

      return default(int);
    }
    //
    // Summary:
    //     Finds the first item after the given index which starts with the given string.
    //     The search is not case sensitive.
    //
    // Parameters:
    //   s:
    //     The System.String to search for.
    //
    //   startIndex:
    //     The zero-based index of the item before the first item to be searched. Set
    //     to -1 to search from the beginning of the control.
    //
    // Returns:
    //     The zero-based index of the first item found; returns -1 if no match is found,
    //     or 0 if the s parameter specifies System.String.Empty.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     The startIndex is less than -1.  -or- The startIndex is greater than the
    //     last index in the collection.
    public int FindString(string s, int startIndex)
    {
      Contract.Requires(startIndex >= -1);
      Contract.Requires(startIndex < Items.Count);

      Contract.Ensures(Contract.Result<int>() >= -1);

      return default(int);
    }
    //
    // Summary:
    //     Finds the first item in the combo box that matches the specified string.
    //
    // Parameters:
    //   s:
    //     The System.String to search for.
    //
    // Returns:
    //     The zero-based index of the first item found; returns -1 if no match is found,
    //     or 0 if the s parameter specifies System.String.Empty.
    public int FindStringExact(string s)
    {
      Contract.Ensures(Contract.Result<int>() >= -1);

      return default(int);
    }
    //
    // Summary:
    //     Finds the first item after the specified index that matches the specified
    //     string.
    //
    // Parameters:
    //   s:
    //     The System.String to search for.
    //
    //   startIndex:
    //     The zero-based index of the item before the first item to be searched. Set
    //     to -1 to search from the beginning of the control.
    //
    // Returns:
    //     The zero-based index of the first item found; returns -1 if no match is found,
    //     or 0 if the s parameter specifies System.String.Empty.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     The startIndex is less than -1.  -or- The startIndex is equal to the last
    //     index in the collection.
    public int FindStringExact(string s, int startIndex)
    {
      Contract.Requires(startIndex >= -1);
      Contract.Requires(startIndex < Items.Count);

      Contract.Ensures(Contract.Result<int>() >= -1);

      return default(int);
    }
    //
    // Summary:
    //     Returns the height of an item in the System.Windows.Forms.ComboBox.
    //
    // Parameters:
    //   index:
    //     The index of the item to return the height of.
    //
    // Returns:
    //     The height, in pixels, of the item at the specified index.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     The index is less than zero.  -or- The index is greater than count of items
    //     in the list.
    public int GetItemHeight(int index)
    {
      Contract.Requires(index >= 0);
      Contract.Requires(index < Items.Count);

      Contract.Ensures(Contract.Result<int>() >= 0);

      return default(int);
    }
    //
    // Summary:
    //     Determines whether the specified key is a regular input key or a special
    //     key that requires preprocessing.
    //
    // Parameters:
    //   keyData:
    //     One of the System.Windows.Forms.Keys values.
    //
    // Returns:
    //     true if the specified key is a regular input key; otherwise, false.
    // protected override bool IsInputKey(Keys keyData);
    //
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    // protected override void OnBackColorChanged(EventArgs e);
    //
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    // protected override void OnDataSourceChanged(EventArgs e);
    //
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    // protected override void OnDisplayMemberChanged(EventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.ComboBox.DrawItem event.
    //
    // Parameters:
    //   e:
    //     A System.Windows.Forms.DrawItemEventArgs that contains the event data.
    // protected virtual void OnDrawItem(DrawItemEventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.ComboBox.DropDown event.
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    // protected virtual void OnDropDown(EventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.ComboBox.DropDownClosed event.
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    // protected virtual void OnDropDownClosed(EventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.ComboBox.DropDownStyleChanged event.
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    // protected virtual void OnDropDownStyleChanged(EventArgs e);
    //
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    // protected override void OnFontChanged(EventArgs e);
    //
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    // protected override void OnForeColorChanged(EventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.Control.GotFocus event.
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
   // [EditorBrowsable(EditorBrowsableState.Advanced)]
    // protected override void OnGotFocus(EventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.Control.HandleCreated event.
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    // protected override void OnHandleCreated(EventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.Control.HandleDestroyed event.
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    // protected override void OnHandleDestroyed(EventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.Control.KeyDown event.
    //
    // Parameters:
    //   e:
    //     A System.Windows.Forms.KeyEventArgs that contains the event data.
   // [EditorBrowsable(EditorBrowsableState.Advanced)]
    // protected override void OnKeyDown(KeyEventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.Control.KeyPress event.
    //
    // Parameters:
    //   e:
    //     A System.Windows.Forms.KeyPressEventArgs that contains the event data.
    // protected override void OnKeyPress(KeyPressEventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.Control.LostFocus event.
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
   // [EditorBrowsable(EditorBrowsableState.Advanced)]
    // protected override void OnLostFocus(EventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.ComboBox.MeasureItem event.
    //
    // Parameters:
    //   e:
    //     The System.Windows.Forms.MeasureItemEventArgs that was raised.
    // protected virtual void OnMeasureItem(MeasureItemEventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.Control.MouseEnter event.
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    // protected override void OnMouseEnter(EventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.Control.MouseLeave event.
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    // protected override void OnMouseLeave(EventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.Control.BackColorChanged event.
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    // protected override void OnParentBackColorChanged(EventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.Control.Resize event.
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    // protected override void OnResize(EventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.ComboBox.SelectedIndexChanged event.
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    // protected override void OnSelectedIndexChanged(EventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.DomainUpDown.SelectedItemChanged event.
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    // protected virtual void OnSelectedItemChanged(EventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.ListControl.SelectedValueChanged event.
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    // protected override void OnSelectedValueChanged(EventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.ComboBox.SelectionChangeCommitted event.
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    // protected virtual void OnSelectionChangeCommitted(EventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.Control.TextChanged event.
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
   // [EditorBrowsable(EditorBrowsableState.Advanced)]
    // protected override void OnTextChanged(EventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.ComboBox.TextUpdate event.
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    // protected virtual void OnTextUpdate(EventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.Control.Validating event.
    //
    // Parameters:
    //   e:
    //     A System.ComponentModel.CancelEventArgs that contains the event data.
   // [EditorBrowsable(EditorBrowsableState.Advanced)]
    // protected override void OnValidating(CancelEventArgs e);
    //
    //
    // Parameters:
    //   m:
    //     A System.Windows.Forms.Message, passed by reference, that represents the
    //     window message to process.
    //
    // Returns:
    //     true if the message was processed by the control; otherwise, false.
    // protected override bool ProcessKeyEventArgs(ref Message m);
    //
    // Summary:
    //     Refreshes the item contained at the specified location.
    //
    // Parameters:
    //   index:
    //     The location of the item to refresh.
    // protected override void RefreshItem(int index);
    //
    // Summary:
    //     Refreshes all System.Windows.Forms.ComboBox items.
    // protected override void RefreshItems();
    //
    // public override void ResetText();
    //
    //
    // Parameters:
    //   factor:
    //     The factor by which the height and width of the control will be scaled.
    //
    //   specified:
    //     A System.Windows.Forms.BoundsSpecified value that specifies the bounds of
    //     the control to use when defining its size and position.
    // protected override void ScaleControl(System.Drawing.SizeF factor, BoundsSpecified specified);
    //
    // Summary:
    //     Selects a range of text in the editable portion of the System.Windows.Forms.ComboBox.
    //
    // Parameters:
    //   start:
    //     The position of the first character in the current text selection within
    //     the text box.
    //
    //   length:
    //     The number of characters to select.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The start is less than zero.  -or- start plus length is less than zero.
    public void Select(int start, int length)
    {
      Contract.Requires(start >= 0);
      Contract.Requires(start + length >= 0); // F: can we just have length >= 0 here?

    }
    //
    // Summary:
    //     Selects all the text in the editable portion of the System.Windows.Forms.ComboBox.
    //public void SelectAll();
    //
    // Summary:
    //     Sets the size and location of the System.Windows.Forms.ComboBox.
    //
    // Parameters:
    //   x:
    //     The horizontal location in pixels of the control.
    //
    //   y:
    //     The vertical location in pixels of the control.
    //
    //   width:
    //     The width in pixels of the control.
    //
    //   height:
    //     The height in pixels of the control.
    //
    //   specified:
    //     One of the System.Windows.Forms.BoundsSpecified values.
    // protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified);
    //
    // Summary:
    //     When overridden in a derived class, sets the object with the specified index
    //     in the derived class.
    //
    // Parameters:
    //   index:
    //     The array index of the object.
    //
    //   value:
    //     The object.
    // protected override void SetItemCore(int index, object value);
    //
    // Summary:
    //     When overridden in a derived class, sets the specified array of objects in
    //     a collection in the derived class.
    //
    // Parameters:
    //   value:
    //     An array of items.
    // protected override void SetItemsCore(IList value);
    //
    // Summary:
    //     Returns a string that represents the System.Windows.Forms.ComboBox control.
    //
    // Returns:
    //     A System.String that represents the current System.Windows.Forms.ComboBox.
    //     The string includes the type and the number of items in the System.Windows.Forms.ComboBox
    //     control.
    // public override string ToString();
    //
    // Summary:
    //     Processes Windows messages.
    //
    // Parameters:
    //   m:
    //     The Windows System.Windows.Forms.Message to process.
    // protected override void WndProc(ref Message m);

    // Summary:
    //     Provides information about the System.Windows.Forms.ComboBox control to accessibility
    //     client applications.
   // [ComVisible(true)]
    public class ChildAccessibleObject //: AccessibleObject
    {
      // Summary:
      //     Initializes a new instance of the System.Windows.Forms.ComboBox.ChildAccessibleObject
      //     class.
      //
      // Parameters:
      //   owner:
      //     The System.Windows.Forms.ComboBox control that owns the System.Windows.Forms.ComboBox.ChildAccessibleObject.
      //
      //   handle:
      //     A handle to part of the System.Windows.Forms.ComboBox.
      //public ChildAccessibleObject(ComboBox owner, IntPtr handle);

      // Summary:
      //     Gets the name of the object.
      //
      // Returns:
      //     The value of the System.Windows.Forms.ComboBox.ChildAccessibleObject.Name
      //     property is the same as the System.Windows.Forms.AccessibleObject.Name property
      //     for the System.Windows.Forms.AccessibleObject of the System.Windows.Forms.ComboBox.
      // public override string Name { get; }
    }

    // Summary:
    //     Represents the collection of items in a System.Windows.Forms.ComboBox.
   // [ListBindable(false)]
    public class ObjectCollection //: IList, ICollection, IEnumerable
    {
      // Summary:
      //     Initializes a new instance of System.Windows.Forms.ComboBox.ObjectCollection.
      //
      // Parameters:
      //   owner:
      //     The System.Windows.Forms.ComboBox that owns this object collection.
      //public ObjectCollection(ComboBox owner);

      // Summary:
      //     Gets the number of items in the collection.
      //
      // Returns:
      //     The number of items in the collection.
      extern public virtual int Count { get; }
      //
      // Summary:
      //     Gets a value indicating whether this collection can be modified.
      //
      // Returns:
      //     Always false.
      //public bool IsReadOnly { get; }

      // Summary:
      //     Retrieves the item at the specified index within the collection.
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
      //     The index was less than zero.  -or- The index was greater than the count
      //     of items in the collection.
     // [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
     // [Browsable(false)]
      //public virtual object this[int index] { get; set; }

      // Summary:
      //     Adds an item to the list of items for a System.Windows.Forms.ComboBox.
      //
      // Parameters:
      //   item:
      //     An object representing the item to add to the collection.
      //
      // Returns:
      //     The zero-based index of the item in the collection.
      //
      // Exceptions:
      //   System.ArgumentNullException:
      //     The item parameter was null.
      //public int Add(object item);
      //
      // Summary:
      //     Adds an array of items to the list of items for a System.Windows.Forms.ComboBox.
      //
      // Parameters:
      //   items:
      //     An array of objects to add to the list.
      //
      // Exceptions:
      //   System.ArgumentNullException:
      //     An item in the items parameter was null.
      ////public void AddRange(object[] items);
      //
      // Summary:
      //     Removes all items from the System.Windows.Forms.ComboBox.
      //public void Clear();
      //
      // Summary:
      //     Determines if the specified item is located within the collection.
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
      //     The object array to copy the collection to.
      //
      //   arrayIndex:
      //     The location in the destination array to copy the collection to.
      //public void CopyTo(object[] destination, int arrayIndex);
      //
      // Summary:
      //     Returns an enumerator that can be used to iterate through the item collection.
      //
      // Returns:
      //     An System.Collections.IEnumerator that represents the item collection.
      //public IEnumerator GetEnumerator();
      //
      // Summary:
      //     Retrieves the index within the collection of the specified item.
      //
      // Parameters:
      //   value:
      //     An object representing the item to locate in the collection.
      //
      // Returns:
      //     The zero-based index where the item is located within the collection; otherwise,
      //     -1.
      //
      // Exceptions:
      //   System.ArgumentNullException:
      //     The value parameter was null.
      //public int IndexOf(object value);
      //
      // Summary:
      //     Inserts an item into the collection at the specified index.
      //
      // Parameters:
      //   index:
      //     The zero-based index location where the item is inserted.
      //
      //   item:
      //     An object representing the item to insert.
      //
      // Exceptions:
      //   System.ArgumentNullException:
      //     The item was null.
      //
      //   System.ArgumentOutOfRangeException:
      //     The index was less than zero.  -or- The index was greater than the count
      //     of items in the collection.
      //public void Insert(int index, object item);
      //
      // Summary:
      //     Removes the specified item from the System.Windows.Forms.ComboBox.
      //
      // Parameters:
      //   value:
      //     The System.Object to remove from the list.
      //
      // Exceptions:
      //   System.ArgumentOutOfRangeException:
      //     The value parameter was less than zero.  -or- The value parameter was greater
      //     than or equal to the count of items in the collection.
      //public void Remove(object value);
      //
      // Summary:
      //     Removes an item from the System.Windows.Forms.ComboBox at the specified index.
      //
      // Parameters:
      //   index:
      //     The index of the item to remove.
      //
      // Exceptions:
      //   System.ArgumentOutOfRangeException:
      //     The value parameter was less than zero.  -or- The value parameter was greater
      //     than or equal to the count of items in the collection.
      //public void RemoveAt(int index);
    }
  }
}
