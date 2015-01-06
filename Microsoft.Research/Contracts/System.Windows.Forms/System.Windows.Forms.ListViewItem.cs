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
using System.Runtime.Serialization;
using System.Diagnostics.Contracts;

namespace System.Windows.Forms
{
  // Summary:
  //     Represents an item in a System.Windows.Forms.ListView control.
  //[Serializable]
  //[DefaultProperty("Text")]
  //[TypeConverter(typeof(ListViewItemConverter))]
  //[DesignTimeVisible(false)]
  //[ToolboxItem(false)]
  public class ListViewItem //: ICloneable, ISerializable
  {

    // Summary:
    //     Initializes a new instance of the System.Windows.Forms.ListViewItem class
    //     with default values.
    //public ListViewItem();
    //
    // Summary:
    //     Initializes a new instance of the System.Windows.Forms.ListViewItem class
    //     and assigns it to the specified group.
    //
    // Parameters:
    //   group:
    //     The System.Windows.Forms.ListViewGroup to assign the item to.
    //public ListViewItem(ListViewGroup group);
    //
    // Summary:
    //     Initializes a new instance of the System.Windows.Forms.ListViewItem class
    //     with the specified item text.
    //
    // Parameters:
    //   text:
    //     The text to display for the item. This should not exceed 259 characters.
    //public ListViewItem(string text);
    //
    // Summary:
    //     Initializes a new instance of the System.Windows.Forms.ListViewItem class
    //     with an array of strings representing subitems.
    //
    // Parameters:
    //   items:
    //     An array of strings that represent the subitems of the new item.
    //public ListViewItem(string//[] items);
    //
    // Summary:
    //     Initializes a new instance of the System.Windows.Forms.ListViewItem class
    //     with the image index position of the item's icon and an array of System.Windows.Forms.ListViewItem.ListViewSubItem
    //     objects.
    //
    // Parameters:
    //   subItems:
    //     An array of type System.Windows.Forms.ListViewItem.ListViewSubItem that represents
    //     the subitems of the item.
    //
    //   imageIndex:
    //     The zero-based index of the image within the System.Windows.Forms.ImageList
    //     associated with the System.Windows.Forms.ListView that contains the item.
    //public ListViewItem(ListViewItem.ListViewSubItem//[] subItems, int imageIndex);
    //
    // Summary:
    //     Initializes a new instance of the System.Windows.Forms.ListViewItem class
    //     with the specified subitems and image.
    //
    // Parameters:
    //   subItems:
    //     An array of System.Windows.Forms.ListViewItem.ListViewSubItem objects.
    //
    //   imageKey:
    //     The name of the image within the System.Windows.Forms.ListViewItem.ImageList
    //     of the owning System.Windows.Forms.ListView to display in the System.Windows.Forms.ListViewItem.
    //public ListViewItem(ListViewItem.ListViewSubItem//[] subItems, string imageKey);
    //
    // Summary:
    //     Initializes a new instance of the System.Windows.Forms.ListViewItem class
    //     with the specified serialization information and streaming context.
    //
    // Parameters:
    //   info:
    //     A System.Runtime.Serialization.SerializationInfo containing information about
    //     the System.Windows.Forms.ListViewItem to be initialized.
    //
    //   context:
    //     A System.Runtime.Serialization.StreamingContext that indicates the source
    //     destination and context information of a serialized stream.
    // protected ListViewItem(SerializationInfo info, StreamingContext context);
    //
    // Summary:
    //     Initializes a new instance of the System.Windows.Forms.ListViewItem class
    //     with the specified item text and the image index position of the item's icon.
    //
    // Parameters:
    //   text:
    //     The text to display for the item. This should not exceed 259 characters.
    //
    //   imageIndex:
    //     The zero-based index of the image within the System.Windows.Forms.ImageList
    //     associated with the System.Windows.Forms.ListView that contains the item.
    //public ListViewItem(string text, int imageIndex);
    //
    // Summary:
    //     Initializes a new instance of the System.Windows.Forms.ListViewItem class
    //     with the specified item text and assigns it to the specified group.
    //
    // Parameters:
    //   text:
    //     The text to display for the item. This should not exceed 259 characters.
    //
    //   group:
    //     The System.Windows.Forms.ListViewGroup to assign the item to.
    //public ListViewItem(string text, ListViewGroup group);
    //
    // Summary:
    //     Initializes a new instance of the System.Windows.Forms.ListViewItem class
    //     with the specified text and image.
    //
    // Parameters:
    //   text:
    //     The text to display for the item. This should not exceed 259 characters.
    //
    //   imageKey:
    //     The name of the image within the System.Windows.Forms.ListViewItem.ImageList
    //     of the owning System.Windows.Forms.ListView to display in the System.Windows.Forms.ListViewItem.
    //public ListViewItem(string text, string imageKey);
    //
    // Summary:
    //     Initializes a new instance of the System.Windows.Forms.ListViewItem class
    //     with the image index position of the item's icon and an array of strings
    //     representing subitems.
    //
    // Parameters:
    //   items:
    //     An array of strings that represent the subitems of the new item.
    //
    //   imageIndex:
    //     The zero-based index of the image within the System.Windows.Forms.ImageList
    //     associated with the System.Windows.Forms.ListView that contains the item.
    //public ListViewItem(string//[] items, int imageIndex);
    //
    // Summary:
    //     Initializes a new instance of the System.Windows.Forms.ListViewItem class
    //     with an array of strings representing subitems, and assigns the item to the
    //     specified group.
    //
    // Parameters:
    //   items:
    //     An array of strings that represent the subitems of the new item.
    //
    //   group:
    //     The System.Windows.Forms.ListViewGroup to assign the item to.
    //public ListViewItem(string//[] items, ListViewGroup group);
    //
    // Summary:
    //     Initializes a new instance of the System.Windows.Forms.ListViewItem class
    //     with the specified item and subitem text and image.
    //
    // Parameters:
    //   items:
    //     An array containing the text of the subitems of the System.Windows.Forms.ListViewItem.
    //
    //   imageKey:
    //     The name of the image within the System.Windows.Forms.ListViewItem.ImageList
    //     of the owning System.Windows.Forms.ListView to display in the System.Windows.Forms.ListViewItem.
    //public ListViewItem(string//[] items, string imageKey);
    //
    // Summary:
    //     Initializes a new instance of the System.Windows.Forms.ListViewItem class
    //     with the image index position of the item's icon and an array of System.Windows.Forms.ListViewItem.ListViewSubItem
    //     objects, and assigns the item to the specified group.
    //
    // Parameters:
    //   subItems:
    //     An array of type System.Windows.Forms.ListViewItem.ListViewSubItem that represents
    //     the subitems of the item.
    //
    //   imageIndex:
    //     The zero-based index of the image within the System.Windows.Forms.ImageList
    //     associated with the System.Windows.Forms.ListView that contains the item.
    //
    //   group:
    //     The System.Windows.Forms.ListViewGroup to assign the item to.
    //public ListViewItem(ListViewItem.ListViewSubItem//[] subItems, int imageIndex, ListViewGroup group);
    //
    // Summary:
    //     Initializes a new instance of the System.Windows.Forms.ListViewItem class
    //     with the specified subitems, image, and group.
    //
    // Parameters:
    //   subItems:
    //     An array of System.Windows.Forms.ListViewItem.ListViewSubItem objects that
    //     represent the subitems of the System.Windows.Forms.ListViewItem.
    //
    //   imageKey:
    //     The name of the image within the System.Windows.Forms.ListViewItem.ImageList
    //     of the owning System.Windows.Forms.ListView to display in the item.
    //
    //   group:
    //     The System.Windows.Forms.ListViewGroup to assign the item to.
    //public ListViewItem(ListViewItem.ListViewSubItem//[] subItems, string imageKey, ListViewGroup group);
    //
    // Summary:
    //     Initializes a new instance of the System.Windows.Forms.ListViewItem class
    //     with the specified item text and the image index position of the item's icon,
    //     and assigns the item to the specified group.
    //
    // Parameters:
    //   text:
    //     The text to display for the item. This should not exceed 259 characters.
    //
    //   imageIndex:
    //     The zero-based index of the image within the System.Windows.Forms.ImageList
    //     associated with the System.Windows.Forms.ListView that contains the item.
    //
    //   group:
    //     The System.Windows.Forms.ListViewGroup to assign the item to.
    //public ListViewItem(string text, int imageIndex, ListViewGroup group);
    //
    // Summary:
    //     Initializes a new instance of the System.Windows.Forms.ListViewItem class
    //     with the specified text, image, and group.
    //
    // Parameters:
    //   text:
    //     The text to display for the item. This should not exceed 259 characters.
    //
    //   imageKey:
    //     The name of the image within the System.Windows.Forms.ListViewItem.ImageList
    //     of the owning System.Windows.Forms.ListView to display in the item.
    //
    //   group:
    //     The System.Windows.Forms.ListViewGroup to assign the item to.
    //public ListViewItem(string text, string imageKey, ListViewGroup group);
    //
    // Summary:
    //     Initializes a new instance of the System.Windows.Forms.ListViewItem class
    //     with the image index position of the item's icon and an array of strings
    //     representing subitems, and assigns the item to the specified group.
    //
    // Parameters:
    //   items:
    //     An array of strings that represent the subitems of the new item.
    //
    //   imageIndex:
    //     The zero-based index of the image within the System.Windows.Forms.ImageList
    //     associated with the System.Windows.Forms.ListView that contains the item.
    //
    //   group:
    //     The System.Windows.Forms.ListViewGroup to assign the item to.
    //public ListViewItem(string[] items, int imageIndex, ListViewGroup group);
    //
    // Summary:
    //     Initializes a new instance of the System.Windows.Forms.ListViewItem class
    //     with subitems containing the specified text, image, and group.
    //
    // Parameters:
    //   items:
    //     An array of strings that represents the text for subitems of the System.Windows.Forms.ListViewItem.
    //
    //   imageKey:
    //     The name of the image within the System.Windows.Forms.ListViewItem.ImageList
    //     of the owning System.Windows.Forms.ListView to display in the item.
    //
    //   group:
    //     The System.Windows.Forms.ListViewGroup to assign the item to.
    //public ListViewItem(string[] items, string imageKey, ListViewGroup group);
    //
    // Summary:
    //     Initializes a new instance of the System.Windows.Forms.ListViewItem class
    //     with the image index position of the item's icon; the foreground color, background
    //     color, and font of the item; and an array of strings representing subitems.
    //
    // Parameters:
    //   items:
    //     An array of strings that represent the subitems of the new item.
    //
    //   imageIndex:
    //     The zero-based index of the image within the System.Windows.Forms.ImageList
    //     associated with the System.Windows.Forms.ListView that contains the item.
    //
    //   foreColor:
    //     A System.Drawing.Color that represents the foreground color of the item.
    //
    //   backColor:
    //     A System.Drawing.Color that represents the background color of the item.
    //
    //   font:
    //     A System.Drawing.Font that represents the font to display the item's text
    //     in.
    //public ListViewItem(string[] items, int imageIndex, System.Drawing.Color foreColor, System.Drawing.Color backColor, System.Drawing.Font font);
    //
    // Summary:
    //     Initializes a new instance of the System.Windows.Forms.ListViewItem class
    //     with the subitems containing the specified text, image, colors, and font.
    //
    // Parameters:
    //   items:
    //     An array of strings that represent the text of the subitems for the System.Windows.Forms.ListViewItem.
    //
    //   imageKey:
    //     The name of the image within the System.Windows.Forms.ListViewItem.ImageList
    //     of the owning System.Windows.Forms.ListView to display in the item.
    //
    //   foreColor:
    //     A System.Drawing.Color that represents the foreground color of the item.
    //
    //   backColor:
    //     A System.Drawing.Color that represents the background color of the item.
    //
    //   font:
    //     A System.Drawing.Font to apply to the item text.
    //public ListViewItem(string[] items, string imageKey, System.Drawing.Color foreColor, System.Drawing.Color backColor, System.Drawing.Font font);
    //
    // Summary:
    //     Initializes a new instance of the System.Windows.Forms.ListViewItem class
    //     with the image index position of the item's icon; the foreground color, background
    //     color, and font of the item; and an array of strings representing subitems.
    //     Assigns the item to the specified group.
    //
    // Parameters:
    //   items:
    //     An array of strings that represent the subitems of the new item.
    //
    //   imageIndex:
    //     The zero-based index of the image within the System.Windows.Forms.ImageList
    //     associated with the System.Windows.Forms.ListView that contains the item.
    //
    //   foreColor:
    //     A System.Drawing.Color that represents the foreground color of the item.
    //
    //   backColor:
    //     A System.Drawing.Color that represents the background color of the item.
    //
    //   font:
    //     A System.Drawing.Font that represents the font to display the item's text
    //     in.
    //
    //   group:
    //     The System.Windows.Forms.ListViewGroup to assign the item to.
    //public ListViewItem(string[] items, int imageIndex, System.Drawing.Color foreColor, System.Drawing.Color backColor, System.Drawing.Font font, ListViewGroup group);
    //
    // Summary:
    //     Initializes a new instance of the System.Windows.Forms.ListViewItem class
    //     with the subitems containing the specified text, image, colors, font, and
    //     group.
    //
    // Parameters:
    //   items:
    //     An array of strings that represents the text of the subitems for the System.Windows.Forms.ListViewItem.
    //
    //   imageKey:
    //     The name of the image within the System.Windows.Forms.ListViewItem.ImageList
    //     of the owning System.Windows.Forms.ListView to display in the item.
    //
    //   foreColor:
    //     A System.Drawing.Color that represents the foreground color of the item.
    //
    //   backColor:
    //     A System.Drawing.Color that represents the background color of the item.
    //
    //   font:
    //     A System.Drawing.Font to apply to the item text.
    //
    //   group:
    //     The System.Windows.Forms.ListViewGroup to assign the item to.
    //public ListViewItem(string[] items, string imageKey, System.Drawing.Color foreColor, System.Drawing.Color backColor, System.Drawing.Font font, ListViewGroup group);

    // Summary:
    //     Gets or sets the background color of the item's text.
    //
    // Returns:
    //     A System.Drawing.Color that represents the background color of the item's
    //     text.
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //public System.Drawing.Color BackColor { get; set; }
    //
    // Summary:
    //     Gets the bounding rectangle of the item, including subitems.
    //
    // Returns:
    //     A System.Drawing.Rectangle that represents the bounding rectangle of the
    //     item.
    //[Browsable(false)]
    //public System.Drawing.Rectangle Bounds { get; }
    //
    // Summary:
    //     Gets or sets a value indicating whether the item is checked.
    //
    // Returns:
    //     true if the item is checked; otherwise, false. The default is false.
    //[DefaultValue(false)]
    //[RefreshProperties(RefreshProperties.Repaint)]
    //public bool Checked { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether the item has focus within the System.Windows.Forms.ListView
    //     control.
    //
    // Returns:
    //     true if the item has focus; otherwise, false.
    //[Browsable(false)]
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //public bool Focused { get; set; }
    //
    // Summary:
    //     Gets or sets the font of the text displayed by the item.
    //
    // Returns:
    //     The System.Drawing.Font to apply to the text displayed by the control. The
    //     default is the value of the System.Windows.Forms.Control.DefaultFont property
    //     if the System.Windows.Forms.ListViewItem is not associated with a System.Windows.Forms.ListView
    //     control; otherwise, the font specified in the System.Windows.Forms.Control.Font
    //     property for the System.Windows.Forms.ListView control is used.
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //[Localizable(true)]
    //public System.Drawing.Font Font { get; set; }
    //
    // Summary:
    //     Gets or sets the foreground color of the item's text.
    //
    // Returns:
    //     A System.Drawing.Color that represents the foreground color of the item's
    //     text.
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //public System.Drawing.Color ForeColor { get; set; }
    //
    // Summary:
    //     Gets or sets the group to which the item is assigned.
    //
    // Returns:
    //     The System.Windows.Forms.ListViewGroup to which the item is assigned.
    //[DefaultValue("")]
    //[Localizable(true)]
    //public ListViewGroup Group { get; set; }
    //
    // Summary:
    //     Gets or sets the index of the image that is displayed for the item.
    //
    // Returns:
    //     The zero-based index of the image in the System.Windows.Forms.ImageList that
    //     is displayed for the item. The default is -1.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The value specified is less than -1.
    //[DefaultValue(-1)]
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //[RefreshProperties(RefreshProperties.Repaint)]
    //[Localizable(true)]
    public int ImageIndex 
    { get
    { 
      Contract.Ensures(Contract.Result<int>() >= -1);
      return default(int);
    }
      set 
      {
        Contract.Requires(value >= -1);

      } 
    }
    
    
    //
    // Summary:
    //     Gets or sets the key for the image that is displayed for the item.
    //
    // Returns:
    //     The key for the image that is displayed for the System.Windows.Forms.ListViewItem.
    //[TypeConverter(typeof(ImageKeyConverter))]
    //[RefreshProperties(RefreshProperties.Repaint)]
    //[Localizable(true)]
    //[DefaultValue("")]
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //public string ImageKey { get; set; }
    //
    // Summary:
    //     Gets the System.Windows.Forms.ImageList that contains the image displayed
    //     with the item.
    //
    // Returns:
    //     The System.Windows.Forms.ImageList used by the System.Windows.Forms.ListView
    //     control that contains the image displayed with the item.
    //[Browsable(false)]
    //public ImageList ImageList { get; }
    //
    // Summary:
    //     Gets or sets the number of small image widths by which to indent the System.Windows.Forms.ListViewItem.
    //
    // Returns:
    //     The number of small image widths by which to indent the System.Windows.Forms.ListViewItem.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     When setting System.Windows.Forms.ListViewItem.IndentCount, the number specified
    //     is less than 0.
    //[DefaultValue(0)]
    public int IndentCount { 
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
    //     Gets the zero-based index of the item within the System.Windows.Forms.ListView
    //     control.
    //
    // Returns:
    //     The zero-based index of the item within the System.Windows.Forms.ListView.ListViewItemCollection
    //     of the System.Windows.Forms.ListView control, or -1 if the item is not associated
    //     with a System.Windows.Forms.ListView control.
    //[Browsable(false)]
    //public int Index { get; }
    //
    // Summary:
    //     Gets the System.Windows.Forms.ListView control that contains the item.
    //
    // Returns:
    //     A System.Windows.Forms.ListView that contains the System.Windows.Forms.ListViewItem.
    //[Browsable(false)]
    //public ListView ListView { get; }
    //
    // Summary:
    //     Gets or sets the name associated with this System.Windows.Forms.ListViewItem.
    //
    // Returns:
    //     The name of the System.Windows.Forms.ListViewItem. The default is an empty
    //     string ("").
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //[Localizable(true)]
    //[Browsable(false)]
    //public string Name { get; set; }
    //
    // Summary:
    //     Gets or sets the position of the upper-left corner of the System.Windows.Forms.ListViewItem.
    //
    // Returns:
    //     The System.Drawing.Point at the upper-left corner of the System.Windows.Forms.ListViewItem.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     The System.Windows.Forms.ListViewItem.Position is set when the containing
    //     System.Windows.Forms.ListView is in virtual mode.
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //[Browsable(false)]
    //public System.Drawing.Point Position { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether the item is selected.
    //
    // Returns:
    //     true if the item is selected; otherwise, false.
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //[Browsable(false)]
    //public bool Selected { get; set; }
    //
    // Summary:
    //     Gets or sets the index of the state image (an image such as a selected or
    //     cleared check box that indicates the state of the item) that is displayed
    //     for the item.
    //
    // Returns:
    //     The zero-based index of the state image in the System.Windows.Forms.ImageList
    //     that is displayed for the item.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     The value specified for this property is less than -1.-or- The value specified
    //     for this property is greater than 14.
    //[RelatedImageList("ListView.StateImageList")]
    //[DefaultValue(-1)]
    //[RefreshProperties(RefreshProperties.Repaint)]
    //[Localizable(true)]
    public int StateImageIndex 
    { get
    {
      Contract.Ensures(Contract.Result<int>() >= -1);
Contract.Ensures(Contract.Result<int>() <= 14);

      return default(int);
    }
      set
      {
        Contract.Requires(value >= -1);
        Contract.Requires(value <= 14);


      }
    }
    //
    // Summary:
    //     Gets a collection containing all subitems of the item.
    //
    // Returns:
    //     A System.Windows.Forms.ListViewItem.ListViewSubItemCollection that contains
    //     the subitems.
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ListViewItem.ListViewSubItemCollection SubItems 
    { 
      get
    {
        Contract.Ensures(Contract.Result<ListViewItem.ListViewSubItemCollection>() != null);

        return default(ListViewItem.ListViewSubItemCollection);
    }}
    //
    // Summary:
    //     Gets or sets an object that contains data to associate with the item.
    //
    // Returns:
    //     An object that contains information that is associated with the item.
    //[DefaultValue("")]
    //[Localizable(false)]
    //[TypeConverter(typeof(StringConverter))]
    //[Bindable(true)]
    //public object Tag { get; set; }
    //
    // Summary:
    //     Gets or sets the text of the item.
    //
    // Returns:
    //     The text to display for the item. This should not exceed 259 characters.
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //[Localizable(true)]
    //public string Text { get; set; }
    //
    // Summary:
    //     Gets or sets the text shown when the mouse pointer rests on the System.Windows.Forms.ListViewItem.
    //
    // Returns:
    //     The text shown when the mouse pointer rests on the System.Windows.Forms.ListViewItem.
    //[DefaultValue("")]
    //public string ToolTipText { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether the System.Windows.Forms.ListViewItem.Font,
    //     System.Windows.Forms.ListViewItem.ForeColor, and System.Windows.Forms.ListViewItem.BackColor
    //     properties for the item are used for all its subitems.
    //
    // Returns:
    //     true if all subitems use the font, foreground color, and background color
    //     settings of the item; otherwise, false. The default is true.
    //[DefaultValue(true)]
    //public bool UseItemStyleForSubItems { get; set; }

    // Summary:
    //     Places the item text into edit mode.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     The System.Windows.Forms.ListView.LabelEdit property of the associated System.Windows.Forms.ListView
    //     is not set to true.
    //public void BeginEdit();
    //
    // Summary:
    //     Creates an identical copy of the item.
    //
    // Returns:
    //     An object that represents an item that has the same text, image, and subitems
    //     associated with it as the cloned item.
    //public virtual object Clone();
    //
    // Summary:
    //     Deserializes the item.
    //
    // Parameters:
    //   info:
    //     A System.Runtime.Serialization.SerializationInfo that holds the data needed
    //     to deserialize the item.
    //
    //   context:
    //     A System.Runtime.Serialization.StreamingContext that represents the source
    //     and destination of the stream being deserialized.
    // protected virtual void Deserialize(SerializationInfo info, StreamingContext context);
    //
    // Summary:
    //     Ensures that the item is visible within the control, scrolling the contents
    //     of the control, if necessary.
    //public virtual void EnsureVisible();
    //
    // Summary:
    //     Finds the next item from the System.Windows.Forms.ListViewItem, searching
    //     in the specified direction.
    //
    // Parameters:
    //   searchDirection:
    //     One of the System.Windows.Forms.SearchDirectionHint values.
    //
    // Returns:
    //     The System.Windows.Forms.ListViewItem that is closest to the given coordinates,
    //     searching in the specified direction.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     The System.Windows.Forms.ListView.View property of the containing System.Windows.Forms.ListView
    //     is set to a value other than System.Windows.Forms.View.SmallIcon or System.Windows.Forms.View.LargeIcon.
    //public ListViewItem FindNearestItem(SearchDirectionHint searchDirection);
    //
    // Summary:
    //     Retrieves the specified portion of the bounding rectangle for the item.
    //
    // Parameters:
    //   portion:
    //     One of the System.Windows.Forms.ItemBoundsPortion values that represents
    //     a portion of the item for which to retrieve the bounding rectangle.
    //
    // Returns:
    //     A System.Drawing.Rectangle that represents the bounding rectangle for the
    //     specified portion of the item.
    //public System.Drawing.Rectangle GetBounds(ItemBoundsPortion portion);
    //
    // Summary:
    //     Returns the subitem of the System.Windows.Forms.ListViewItem at the specified
    //     coordinates.
    //
    // Parameters:
    //   x:
    //     The x-coordinate.
    //
    //   y:
    //     The y-coordinate.
    //
    // Returns:
    //     The System.Windows.Forms.ListViewItem.ListViewSubItem at the specified x-
    //     and y-coordinates.
    //public ListViewItem.ListViewSubItem GetSubItemAt(int x, int y);
    //
    // Summary:
    //     Removes the item from its associated System.Windows.Forms.ListView control.
    //public virtual void Remove();
    //
    // Summary:
    //     Serializes the item.
    //
    // Parameters:
    //   info:
    //     A System.Runtime.Serialization.SerializationInfo that holds the data needed
    //     to serialize the item.
    //
    //   context:
    //     A System.Runtime.Serialization.StreamingContext that represents the source
    //     and destination of the stream being serialized.
    // protected virtual void Serialize(SerializationInfo info, StreamingContext context);
    //
    //
    // Returns:
    //     A System.String that represents the current System.Object.
    //public override string ToString();

    // Summary:
    //     Represents a subitem of a System.Windows.Forms.ListViewItem.
    //[Serializable]
    //[ToolboxItem(false)]
    //[DesignTimeVisible(false)]
    //[DefaultProperty("Text")]
    public class ListViewSubItem
    {
      // Summary:
      //     Initializes a new instance of the System.Windows.Forms.ListViewItem.ListViewSubItem
      //     class with default values.
      //public ListViewSubItem() 
      //
      // Summary:
      //     Initializes a new instance of the System.Windows.Forms.ListViewItem.ListViewSubItem
      //     class with the specified owner and text.
      //
      // Parameters:
      //   owner:
      //     A System.Windows.Forms.ListViewItem that represents the item that owns the
      //     subitem.
      //
      //   text:
      //     The text to display for the subitem.
      //public ListViewSubItem(ListViewItem owner, string text);
      //
      // Summary:
      //     Initializes a new instance of the System.Windows.Forms.ListViewItem.ListViewSubItem
      //     class with the specified owner, text, foreground color, background color,
      //     and font values.
      //
      // Parameters:
      //   owner:
      //     A System.Windows.Forms.ListViewItem that represents the item that owns the
      //     subitem.
      //
      //   text:
      //     The text to display for the subitem.
      //
      //   foreColor:
      //     A System.Drawing.Color that represents the foreground color of the subitem.
      //
      //   backColor:
      //     A System.Drawing.Color that represents the background color of the subitem.
      //
      //   font:
      //     A System.Drawing.Font that represents the font to display the subitem's text
      //     in.
      //public ListViewSubItem(ListViewItem owner, string text, System.Drawing.Color foreColor, System.Drawing.Color backColor, System.Drawing.Font font);

      // Summary:
      //     Gets or sets the background color of the subitem's text.
      //
      // Returns:
      //     A System.Drawing.Color that represents the background color of the subitem's
      //     text.
      //public System.Drawing.Color BackColor { get; set; }
      //
      // Summary:
      //     Gets the bounding rectangle of the System.Windows.Forms.ListViewItem.ListViewSubItem.
      //
      // Returns:
      //     The bounding System.Drawing.Rectangle of the System.Windows.Forms.ListViewItem.ListViewSubItem.
      //[Browsable(false)]
      //public System.Drawing.Rectangle Bounds { get; }
      ////
      // Summary:
      //     Gets or sets the font of the text displayed by the subitem.
      //
      // Returns:
      //     The System.Drawing.Font to apply to the text displayed by the control.
      //[Localizable(true)]
      //public System.Drawing.Font Font { get; set; }
      //
      // Summary:
      //     Gets or sets the foreground color of the subitem's text.
      //
      // Returns:
      //     A System.Drawing.Color that represents the foreground color of the subitem's
      //     text.
      //public System.Drawing.Color ForeColor { get; set; }
      //
      // Summary:
      //     Gets or sets the name of the System.Windows.Forms.ListViewItem.ListViewSubItem.
      //
      // Returns:
      //     The name of the System.Windows.Forms.ListViewItem.ListViewSubItem, or an
      //     empty string ("") if a name has not been set.
      //[Localizable(true)]
      //public string Name { get; set; }
      //
      // Summary:
      //     Gets or sets an object that contains data about the System.Windows.Forms.ListViewItem.ListViewSubItem.
      //
      // Returns:
      //     An System.Object that contains data about the System.Windows.Forms.ListViewItem.ListViewSubItem.
      //     The default is null.
      //[TypeConverter(typeof(StringConverter))]
      //[Localizable(false)]
      //[Bindable(true)]
      //[DefaultValue("")]
      //public object Tag { get; set; }
      //
      // Summary:
      //     Gets or sets the text of the subitem.
      //
      // Returns:
      //     The text to display for the subitem.
      //[Localizable(true)]
      //public string Text { get; set; }

      // Summary:
      //     Resets the styles applied to the subitem to the default font and colors.
      //public void ResetStyle();
      //
      //
      // Returns:
      //     A System.String that represents the current System.Object.
      //public override string ToString();
    }

    // Summary:
    //     Represents a collection of System.Windows.Forms.ListViewItem.ListViewSubItem
    //     objects stored in a System.Windows.Forms.ListViewItem.
    public class ListViewSubItemCollection //: IList, ICollection, IEnumerable
    {
      // Summary:
      //     Initializes a new instance of the System.Windows.Forms.ListViewItem.ListViewSubItemCollection
      //     class.
      //
      // Parameters:
      //   owner:
      //     The System.Windows.Forms.ListViewItem that owns the collection.
      //public ListViewSubItemCollection(ListViewItem owner);

      // Summary:
     
      // Summary:
      //     Gets or sets the subitem at the specified index within the collection.
      //
      // Parameters:
      //   index:
      //     The index of the item in the collection to retrieve.
      //
      // Returns:
      //     A System.Windows.Forms.ListViewItem.ListViewSubItem representing the subitem
      //     located at the specified index within the collection.
      //
      // Exceptions:
      //   System.ArgumentOutOfRangeException:
      //     The index parameter is less than zero or greater than or equal to the value
      //     of the System.Windows.Forms.ListViewItem.ListViewSubItemCollection.Count
      //     property of the System.Windows.Forms.ListViewItem.ListViewSubItemCollection.
      public ListViewItem.ListViewSubItem this[int index] 
      { 
        get
      {
        Contract.Requires(index >= 0);
        return default(ListViewItem.ListViewSubItem);
      }
        set 
        { 
          Contract.Requires(index >= 0);
        } 
      }

      //
      // Summary:
      //     Gets an item with the specified key from the collection.
      //
      // Parameters:
      //   key:
      //     The name of the System.Windows.Forms.ListViewItem.ListViewSubItem to retrieve.
      //
      // Returns:
      //     The System.Windows.Forms.ListViewItem.ListViewSubItem with the specified
      //     key.
      //public virtual ListViewItem.ListViewSubItem this//[string key] { get; }

      // Summary:
      //     Adds an existing System.Windows.Forms.ListViewItem.ListViewSubItem to the
      //     collection.
      //
      // Parameters:
      //   item:
      //     The System.Windows.Forms.ListViewItem.ListViewSubItem to add to the collection.
      //
      // Returns:
      //     The System.Windows.Forms.ListViewItem.ListViewSubItem that was added to the
      //     collection.
      public ListViewItem.ListViewSubItem Add(ListViewItem.ListViewSubItem item)
      {
        Contract.Ensures(Contract.Result<ListViewItem.ListViewSubItem>() != null);

        return default(ListViewItem.ListViewSubItem);
      }
      //
      // Summary:
      //     Adds a subitem to the collection with specified text.
      //
      // Parameters:
      //   text:
      //     The text to display for the subitem.
      //
      // Returns:
      //     The System.Windows.Forms.ListViewItem.ListViewSubItem that was added to the
      //     collection.
      //public ListViewItem.ListViewSubItem Add(string text);
      //
      // Summary:
      //     Adds a subitem to the collection with specified text, foreground color, background
      //     color, and font settings.
      //
      // Parameters:
      //   text:
      //     The text to display for the subitem.
      //
      //   foreColor:
      //     A System.Drawing.Color that represents the foreground color of the subitem.
      //
      //   backColor:
      //     A System.Drawing.Color that represents the background color of the subitem.
      //
      //   font:
      //     A System.Drawing.Font that represents the typeface to display the subitem's
      //     text in.
      //
      // Returns:
      //     The System.Windows.Forms.ListViewItem.ListViewSubItem that was added to the
      //     collection.
      //public ListViewItem.ListViewSubItem Add(string text, System.Drawing.Color foreColor, System.Drawing.Color backColor, System.Drawing.Font font);
      //
      // Summary:
      //     Adds an array of System.Windows.Forms.ListViewItem.ListViewSubItem objects
      //     to the collection.
      //
      // Parameters:
      //   items:
      //     An array of System.Windows.Forms.ListViewItem.ListViewSubItem objects to
      //     add to the collection.
      //public void AddRange(ListViewItem.ListViewSubItem//[] items);
      //
      // Summary:
      //     Creates new subitems based on an array and adds them to the collection.
      //
      // Parameters:
      //   items:
      //     An array of strings representing the text of each subitem to add to the collection.
      //public void AddRange(string[] items);
      //
      // Summary:
      //     Creates new subitems based on an array and adds them to the collection with
      //     specified foreground color, background color, and font.
      //
      // Parameters:
      //   items:
      //     An array of strings representing the text of each subitem to add to the collection.
      //
      //   foreColor:
      //     A System.Drawing.Color that represents the foreground color of the subitem.
      //
      //   backColor:
      //     A System.Drawing.Color that represents the background color of the subitem.
      //
      //   font:
      //     A System.Drawing.Font that represents the typeface to display the subitem's
      //     text in.
      //public void AddRange(string//[] items, System.Drawing.Color foreColor, System.Drawing.Color backColor, System.Drawing.Font font);
      //
      // Summary:
      //     Removes all subitems and the parent System.Windows.Forms.ListViewItem from
      //     the collection.
      //public void Clear();
      //
      // Summary:
      //     Determines whether the specified subitem is located in the collection.
      //
      // Parameters:
      //   subItem:
      //     A System.Windows.Forms.ListViewItem.ListViewSubItem representing the subitem
      //     to locate in the collection.
      //
      // Returns:
      //     true if the subitem is contained in the collection; otherwise, false.
      //public bool Contains(ListViewItem.ListViewSubItem subItem);
      //
      // Summary:
      //     Determines if the collection contains an item with the specified key.
      //
      // Parameters:
      //   key:
      //     The name of the System.Windows.Forms.ListViewItem.ListViewSubItem to look
      //     for.
      //
      // Returns:
      //     true to indicate the collection contains an item with the specified key;
      //     otherwise, false.
      //public virtual bool ContainsKey(string key);
      //
      // Summary:
      //     Returns an enumerator to use to iterate through the subitem collection.
      //
      // Returns:
      //     An System.Collections.IEnumerator that represents the subitem collection.
      //public IEnumerator GetEnumerator();
      //
      // Summary:
      //     Returns the index within the collection of the specified subitem.
      //
      // Parameters:
      //   subItem:
      //     A System.Windows.Forms.ListViewItem.ListViewSubItem representing the subitem
      //     to locate in the collection.
      //
      // Returns:
      //     The zero-based index of the subitem's location in the collection. If the
      //     subitem is not located in the collection, the return value is negative one
      //     (-1).
      //public int IndexOf(ListViewItem.ListViewSubItem subItem);
      //
      // Summary:
      //     Returns the index of the first occurrence of an item with the specified key
      //     within the collection.
      //
      // Parameters:
      //   key:
      //     The name of the item to retrieve the index for.
      //
      // Returns:
      //     The zero-based index of the first occurrence of an item with the specified
      //     key.
      //public virtual int IndexOfKey(string key);
      //
      // Summary:
      //     Inserts a subitem into the collection at the specified index.
      //
      // Parameters:
      //   index:
      //     The zero-based index location where the item is inserted.
      //
      //   item:
      //     A System.Windows.Forms.ListViewItem.ListViewSubItem representing the subitem
      //     to insert into the collection.
      //
      // Exceptions:
      //   System.ArgumentOutOfRangeException:
      //     The index parameter is less than zero or greater than or equal to the value
      //     of the System.Windows.Forms.ListViewItem.ListViewSubItemCollection.Count
      //     property of the System.Windows.Forms.ListViewItem.ListViewSubItemCollection.
      //public void Insert(int index, ListViewItem.ListViewSubItem item);
      //
      // Summary:
      //     Removes a specified item from the collection.
      //
      // Parameters:
      //   item:
      //     The item to remove from the collection.
      //public void Remove(ListViewItem.ListViewSubItem item);
      //
      // Summary:
      //     Removes the subitem at the specified index within the collection.
      //
      // Parameters:
      //   index:
      //     The zero-based index of the subitem to remove.
      //
      // Exceptions:
      //   System.ArgumentOutOfRangeException:
      //     The index parameter is less than zero or greater than or equal to the value
      //     of the System.Windows.Forms.ListViewItem.ListViewSubItemCollection.Count
      //     property of the System.Windows.Forms.ListViewItem.ListViewSubItemCollection.
      //public void RemoveAt(int index);
      //
      // Summary:
      //     Removes an item with the specified key from the collection.
      //
      // Parameters:
      //   key:
      //     The name of the item to remove from the collection.
      //public virtual void RemoveByKey(string key);
    }
  }
}
