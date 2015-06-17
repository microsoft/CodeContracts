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
  //     Represents the abstract base class that manages events and layout for all
  //     the elements that a System.Windows.Forms.ToolStrip or System.Windows.Forms.ToolStripDropDown
  //     can contain.
  //[Designer("System.Windows.Forms.Design.ToolStripItemDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
  //[DefaultProperty("Text")]
  //[DefaultEvent("Click")]
  //[ToolboxItem(false)]
  //[DesignTimeVisible(false)]
  public abstract class ToolStripItem 
  {
    // Summary:
    //     Initializes a new instance of the System.Windows.Forms.ToolStripItem class.
    //// protected ToolStripItem();
    //
    // Summary:
    //     Initializes a new instance of the System.Windows.Forms.ToolStripItem class
    //     with the specified name, image, and event handler.
    //
    // Parameters:
    //   text:
    //     A System.String representing the name of the System.Windows.Forms.ToolStripItem.
    //
    //   image:
    //     The System.Drawing.Image to display on the System.Windows.Forms.ToolStripItem.
    //
    //   onClick:
    //     Raises the System.Windows.Forms.ToolStripItem.Click event when the user clicks
    //     the System.Windows.Forms.ToolStripItem.
    //// protected ToolStripItem(string text, System.Drawing.Image image, EventHandler onClick);
    //
    // Summary:
    //     Initializes a new instance of the System.Windows.Forms.ToolStripItem class
    //     with the specified display text, image, event handler, and name.
    //
    // Parameters:
    //   text:
    //     The text to display on the System.Windows.Forms.ToolStripItem.
    //
    //   image:
    //     The Image to display on the System.Windows.Forms.ToolStripItem.
    //
    //   onClick:
    //     The event handler for the System.Windows.Forms.ToolStripItem.Click event.
    //
    //   name:
    //     The name of the System.Windows.Forms.ToolStripItem.
    // protected ToolStripItem(string text, System.Drawing.Image image, EventHandler onClick, string name);

    // Summary:
    //     Gets the System.Windows.Forms.AccessibleObject assigned to the control.
    //
    // Returns:
    //     The System.Windows.Forms.AccessibleObject assigned to the control; if no
    //     System.Windows.Forms.AccessibleObject is currently assigned to the control,
    //     a new instance is created when this property is first accessed
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    //[Browsable(false)]
    //public AccessibleObject AccessibilityObject { get; }
    ////
    //// Summary:
    ////     Gets or sets the default action description of the control for use by accessibility
    ////     client applications.
    ////
    //// Returns:
    ////     The default action description of the control, for use by accessibility client
    ////     applications.
    ////[EditorBrowsable(EditorBrowsableState.Advanced)]
    ////[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    ////[Browsable(false)]
    //public string AccessibleDefaultActionDescription { get; set; }
    ////
    //// Summary:
    ////     Gets or sets the description that will be reported to accessibility client
    ////     applications.
    ////
    //// Returns:
    ////     The description of the control used by accessibility client applications.
    ////     The default is null.
    ////[DefaultValue("")]
    ////[Localizable(true)]
    //public string AccessibleDescription { get; set; }
    ////
    //// Summary:
    ////     Gets or sets the name of the control for use by accessibility client applications.
    ////
    //// Returns:
    ////     The name of the control, for use by accessibility client applications. The
    ////     default is null.
    ////[DefaultValue("")]
    ////[Localizable(true)]
    //public string AccessibleName { get; set; }
    ////
    //// Summary:
    ////     Gets or sets the accessible role of the control, which specifies the type
    ////     of user interface element of the control.
    ////
    //// Returns:
    ////     One of the System.Windows.Forms.AccessibleRole values. The default is System.Windows.Forms.AccessibleRole.PushButton.
    ////
    //// Exceptions:
    ////   System.ComponentModel.InvalidEnumArgumentException:
    ////     The value assigned is not one of the System.Windows.Forms.AccessibleRole
    ////     values.
    //public AccessibleRole AccessibleRole { get; set; }
    ////
    //// Summary:
    ////     Gets or sets a value indicating whether the item aligns towards the beginning
    ////     or end of the System.Windows.Forms.ToolStrip.
    ////
    //// Returns:
    ////     One of the System.Windows.Forms.ToolStripItemAlignment values. The default
    ////     is System.Windows.Forms.ToolStripItemAlignment.Left.
    ////
    //// Exceptions:
    ////   System.ComponentModel.InvalidEnumArgumentException:
    ////     The value assigned is not one of the System.Windows.Forms.ToolStripItemAlignment
    ////     values.
    //public ToolStripItemAlignment Alignment { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether drag-and-drop and item reordering
    //     are handled through events that you implement.
    //
    // Returns:
    //     true if drag-and-drop operations are allowed in the control; otherwise, false.
    //     The default is false.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     System.Windows.Forms.ToolStripItem.AllowDrop and System.Windows.Forms.ToolStrip.AllowItemReorder
    //     are both set to true.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    //[DefaultValue(false)]
    //[Browsable(false)]
    // public virtual bool AllowDrop { get; set; }
    //
    // Summary:
    //     Gets or sets the edges of the container to which a System.Windows.Forms.ToolStripItem
    //     is bound and determines how a System.Windows.Forms.ToolStripItem is resized
    //     with its parent.
    //
    // Returns:
    //     One of the System.Windows.Forms.AnchorStyles values.
    //
    // Exceptions:
    //   System.ComponentModel.InvalidEnumArgumentException:
    //     The value is not one of the System.Windows.Forms.AnchorStyles values.
    //[Browsable(false)]
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //public AnchorStyles Anchor { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether the item is automatically sized.
    //
    // Returns:
    //     true if the System.Windows.Forms.ToolStripItem is automatically sized; otherwise,
    //     false. The default value is true.
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    //[RefreshProperties(RefreshProperties.All)]
    //[Localizable(true)]
    //[DefaultValue(true)]
    //public bool AutoSize { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether to use the System.Windows.Forms.ToolStripItem.Text
    //     property or the System.Windows.Forms.ToolStripItem.ToolTipText property for
    //     the System.Windows.Forms.ToolStripItem ToolTip.
    //
    // Returns:
    //     true to use the System.Windows.Forms.ToolStripItem.Text property for the
    //     ToolTip; otherwise, false. The default is true.
    //[DefaultValue(false)]
    //public bool AutoToolTip { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether the System.Windows.Forms.ToolStripItem
    //     should be placed on a System.Windows.Forms.ToolStrip.
    //
    // Returns:
    //     true if the System.Windows.Forms.ToolStripItem is placed on a System.Windows.Forms.ToolStrip;
    //     otherwise, false.
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //[Browsable(false)]
    //public bool Available { get; set; }
    //
    // Summary:
    //     Gets or sets the background color for the item.
    //
    // Returns:
    //     A System.Drawing.Color that represents the background color of the item.
    //     The default is the value of the System.Windows.Forms.Control.DefaultBackColor
    //     property.
    // public virtual System.Drawing.Color BackColor { get; set; }
    //
    //[DefaultValue("")]
    //[Localizable(true)]
    // public virtual System.Drawing.Image BackgroundImage { get; set; }
    //
    // Summary:
    //     Gets or sets the background image layout used for the System.Windows.Forms.ToolStripItem.
    //
    // Returns:
    //     One of the System.Windows.Forms.ImageLayout values. The default value is
    //     System.Windows.Forms.ImageLayout.Tile.
    //[Localizable(true)]
    // public virtual ImageLayout BackgroundImageLayout { get; set; }
    //
    // Summary:
    //     Gets the size and location of the item.
    //
    // Returns:
    //     A System.Drawing.Rectangle that represents the size and location of the System.Windows.Forms.ToolStripItem.
    //[Browsable(false)]
    // public virtual System.Drawing.Rectangle Bounds { get; }
    //
    // Summary:
    //     Gets a value indicating whether the item can be selected.
    //
    // Returns:
    //     true if the System.Windows.Forms.ToolStripItem can be selected; otherwise,
    //     false.
    //[Browsable(false)]
    // public virtual bool CanSelect { get; }
    //
    // Summary:
    //     Gets the area where content, such as text and icons, can be placed within
    //     a System.Windows.Forms.ToolStripItem without overwriting background borders.
    //
    // Returns:
    //     A System.Drawing.Rectangle containing four integers that represent the location
    //     and size of System.Windows.Forms.ToolStripItem contents, excluding its border.
    //[Browsable(false)]
    //public System.Drawing.Rectangle ContentRectangle { get; }
    //
    // Summary:
    //     Gets a value indicating whether to display the System.Windows.Forms.ToolTip
    //     that is defined as the default.
    //
    // Returns:
    //     false in all cases.
    // protected virtual bool DefaultAutoToolTip { get; }
    //
    // Summary:
    //     Gets a value indicating what is displayed on the System.Windows.Forms.ToolStripItem.
    //
    // Returns:
    //     One of the System.Windows.Forms.ToolStripItemDisplayStyle values. The default
    //     is System.Windows.Forms.ToolStripItemDisplayStyle.ImageAndText.
    // protected virtual ToolStripItemDisplayStyle DefaultDisplayStyle { get; }
    //
    // Summary:
    //     Gets the default margin of an item.
    //
    // Returns:
    //     A System.Windows.Forms.Padding representing the margin.
    // protected internal virtual Padding DefaultMargin { get; }
    //
    // Summary:
    //     Gets the internal spacing characteristics of the item.
    //
    // Returns:
    //     One of the System.Windows.Forms.Padding values.
    // protected virtual Padding DefaultPadding { get; }
    //
    // Summary:
    //     Gets the default size of the item.
    //
    // Returns:
    //     The default System.Drawing.Size of the System.Windows.Forms.ToolStripItem.
    // protected virtual System.Drawing.Size DefaultSize { get; }
    //
    // Summary:
    //     Gets a value indicating whether items on a System.Windows.Forms.ToolStripDropDown
    //     are hidden after they are clicked.
    //
    // Returns:
    //     true if the item is hidden after it is clicked; otherwise, false.
    // protected internal virtual bool DismissWhenClicked { get; }
    //
    // Summary:
    //     Gets or sets whether text and images are displayed on a System.Windows.Forms.ToolStripItem.
    //
    // Returns:
    //     One of the System.Windows.Forms.ToolStripItemDisplayStyle values. The default
    //     is System.Windows.Forms.ToolStripItemDisplayStyle.ImageAndText .
    // public virtual ToolStripItemDisplayStyle DisplayStyle { get; set; }
    //
    // Summary:
    //     Gets or sets which System.Windows.Forms.ToolStripItem borders are docked
    //     to its parent control and determines how a System.Windows.Forms.ToolStripItem
    //     is resized with its parent.
    //
    // Returns:
    //     One of the System.Windows.Forms.DockStyle values. The default is System.Windows.Forms.DockStyle.None.
    //
    // Exceptions:
    //   System.ComponentModel.InvalidEnumArgumentException:
    //     The value assigned is not one of the System.Windows.Forms.DockStyle values.
    //[Browsable(false)]
    //public DockStyle Dock { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether the System.Windows.Forms.ToolStripItem
    //     can be activated by double-clicking the mouse.
    //
    // Returns:
    //     true if the System.Windows.Forms.ToolStripItem can be activated by double-clicking
    //     the mouse; otherwise, false. The default is false.
    //[DefaultValue(false)]
    //public bool DoubleClickEnabled { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether the parent control of the System.Windows.Forms.ToolStripItem
    //     is enabled.
    //
    // Returns:
    //     true if the parent control of the System.Windows.Forms.ToolStripItem is enabled;
    //     otherwise, false. The default is true.
    //[DefaultValue(true)]
    //[Localizable(true)]
    // public virtual bool Enabled { get; set; }
    //
    // Summary:
    //     Gets or sets the font of the text displayed by the item.
    //
    // Returns:
    //     The System.Drawing.Font to apply to the text displayed by the System.Windows.Forms.ToolStripItem.
    //     The default is the value of the System.Windows.Forms.Control.DefaultFont
    //     property.
    //[Localizable(true)]
    // public virtual System.Drawing.Font Font { get; set; }
    //
    // Summary:
    //     Gets or sets the foreground color of the item.
    //
    // Returns:
    //     The foreground System.Drawing.Color of the item. The default is the value
    //     of the System.Windows.Forms.Control.DefaultForeColor property.
    // public virtual System.Drawing.Color ForeColor { get; set; }
    //
    // Summary:
    //     Gets or sets the height, in pixels, of a System.Windows.Forms.ToolStripItem.
    //
    // Returns:
    //     An System.Int32 representing the height, in pixels.
    //[Browsable(false)]
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //[EditorBrowsable(EditorBrowsableState.Always)]
    //public int Height { get; set; }
    //
    // Summary:
    //     Gets or sets the image that is displayed on a System.Windows.Forms.ToolStripItem.
    //
    // Returns:
    //     The System.Drawing.Image to be displayed.
    //[Localizable(true)]
    // public virtual System.Drawing.Image Image { get; set; }
    //
    // Summary:
    //     Gets or sets the alignment of the image on a System.Windows.Forms.ToolStripItem.
    //
    // Returns:
    //     One of the System.Drawing.ContentAlignment values. The default is System.Drawing.ContentAlignment.MiddleLeft.
    //
    // Exceptions:
    //   System.ComponentModel.InvalidEnumArgumentException:
    //     The value assigned is not one of the System.Drawing.ContentAlignment values.
    //[Localizable(true)]
    //public System.Drawing.ContentAlignment ImageAlign { get; set; }
    //
    // Summary:
    //     Gets or sets the index value of the image that is displayed on the item.
    //
    // Returns:
    //     The zero-based index of the image in the System.Windows.Forms.ToolStrip.ImageList
    //     that is displayed for the item. The default is -1, signifying that the image
    //     list is empty.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The value specified is less than -1.
    //[RefreshProperties(RefreshProperties.Repaint)]
    //[RelatedImageList("Owner.ImageList")]
    //[Browsable(false)]
    //[Localizable(true)]
    public int ImageIndex
    {
      get
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
    //     Gets or sets the key accessor for the image in the System.Windows.Forms.ToolStrip.ImageList
    //     that is displayed on a System.Windows.Forms.ToolStripItem.
    //
    // Returns:
    //     A string representing the key of the image.
    //[TypeConverter(typeof(ImageKeyConverter))]
    //[RefreshProperties(RefreshProperties.Repaint)]
    //[Browsable(false)]
    //[RelatedImageList("Owner.ImageList")]
    //[Localizable(true)]
    //public string ImageKey { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether an image on a System.Windows.Forms.ToolStripItem
    //     is automatically resized to fit in a container.
    //
    // Returns:
    //     One of the System.Windows.Forms.ToolStripItemImageScaling values. The default
    //     is System.Windows.Forms.ToolStripItemImageScaling.SizeToFit.
    //[Localizable(true)]
    //public ToolStripItemImageScaling ImageScaling { get; set; }
    //
    // Summary:
    //     Gets or sets the color to treat as transparent in a System.Windows.Forms.ToolStripItem
    //     image.
    //
    // Returns:
    //     One of the System.Drawing.Color values.
    //[Localizable(true)]
    //public System.Drawing.Color ImageTransparentColor { get; set; }
 
    //
    // Summary:
    //     Gets a value indicating whether the container of the current System.Windows.Forms.Control
    //     is a System.Windows.Forms.ToolStripDropDown.
    //
    // Returns:
    //     true if the container of the current System.Windows.Forms.Control is a System.Windows.Forms.ToolStripDropDown;
    //     otherwise, false.
    //[Browsable(false)]
    //public bool IsOnDropDown { get; }
    ////
    //// Summary:
    ////     Gets a value indicating whether the System.Windows.Forms.ToolStripItem.Placement
    ////     property is set to System.Windows.Forms.ToolStripItemPlacement.Overflow.
    ////
    //// Returns:
    ////     true if the System.Windows.Forms.ToolStripItem.Placement property is set
    ////     to System.Windows.Forms.ToolStripItemPlacement.Overflow; otherwise, false.
    ////[Browsable(false)]
    //public bool IsOnOverflow { get; }
    ////
    //// Summary:
    ////     Gets or sets the space between the item and adjacent items.
    ////
    //// Returns:
    ////     A System.Windows.Forms.Padding representing the space between the item and
    ////     adjacent items.
    //public Padding Margin { get; set; }
    ////
    //// Summary:
    ////     Gets or sets how child menus are merged with parent menus.
    ////
    //// Returns:
    ////     One of the System.Windows.Forms.MergeAction values. The default is System.Windows.Forms.MergeAction.MatchOnly.
    ////
    //// Exceptions:
    ////   System.ComponentModel.InvalidEnumArgumentException:
    ////     The value assigned is not one of the System.Windows.Forms.MergeAction values.
    //public MergeAction MergeAction { get; set; }
    ////
    //// Summary:
    ////     Gets or sets the position of a merged item within the current System.Windows.Forms.ToolStrip.
    ////
    //// Returns:
    ////     An integer representing the index of the merged item, if a match is found,
    ////     or -1 if a match is not found.
    ////[DefaultValue(-1)]
    //public int MergeIndex { get; set; }
    //
    // Summary:
    //     Gets or sets the name of the item.
    //
    // Returns:
    //     A string representing the name. The default value is null.
    //[DefaultValue("")]
    //[Browsable(false)]
    //public string Name { get; set; }
    ////
    //// Summary:
    ////     Gets or sets whether the item is attached to the System.Windows.Forms.ToolStrip
    ////     or System.Windows.Forms.ToolStripOverflowButton or can float between the
    ////     two.
    ////
    //// Returns:
    ////     One of the System.Windows.Forms.ToolStripItemOverflow values. The default
    ////     is System.Windows.Forms.ToolStripItemOverflow.AsNeeded.
    ////
    //// Exceptions:
    ////   System.ComponentModel.InvalidEnumArgumentException:
    ////     The value assigned is not one of the System.Windows.Forms.ToolStripItemOverflow
    ////     values.
    //public ToolStripItemOverflow Overflow { get; set; }
    ////
    //// Summary:
    ////     Gets or sets the owner of this item.
    ////
    //// Returns:
    ////     The System.Windows.Forms.ToolStrip that owns or is to own the System.Windows.Forms.ToolStripItem.
    ////[Browsable(false)]
    ////[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //public ToolStrip Owner { get; set; }
    ////
    //// Summary:
    ////     Gets the parent System.Windows.Forms.ToolStripItem of this System.Windows.Forms.ToolStripItem.
    ////
    //// Returns:
    ////     The parent System.Windows.Forms.ToolStripItem of this System.Windows.Forms.ToolStripItem.
    ////[Browsable(false)]
    ////[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //public ToolStripItem OwnerItem { get; }
    //
    // Summary:
    //     Gets or sets the internal spacing, in pixels, between the item's contents
    //     and its edges.
    //
    // Returns:
    //     A System.Windows.Forms.Padding representing the item's internal spacing,
    //     in pixels.
    // public virtual Padding Padding { get; set; }
    //
    // Summary:
    //     Gets or sets the parent container of the System.Windows.Forms.ToolStripItem.
    //
    // Returns:
    //     A System.Windows.Forms.ToolStrip that is the parent container of the System.Windows.Forms.ToolStripItem.
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //[Browsable(false)]
    // protected internal ToolStrip Parent { get; set; }
    //
    // Summary:
    //     Gets the current layout of the item.
    //
    // Returns:
    //     One of the System.Windows.Forms.ToolStripItemPlacement values.
    //[Browsable(false)]
    //public ToolStripItemPlacement Placement { get; }
    //
    // Summary:
    //     Gets a value indicating whether the state of the item is pressed.
    //
    // Returns:
    //     true if the state of the item is pressed; otherwise, false.
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //[Browsable(false)]
    // public virtual bool Pressed { get; }
    //
    // Summary:
    //     Gets or sets a value indicating whether items are to be placed from right
    //     to left and text is to be written from right to left.
    //
    // Returns:
    //     true if items are to be placed from right to left and text is to be written
    //     from right to left; otherwise, false.
    //[Localizable(true)]
    // public virtual RightToLeft RightToLeft { get; set; }
    //
    // Summary:
    //     Mirrors automatically the System.Windows.Forms.ToolStripItem image when the
    //     System.Windows.Forms.ToolStripItem.RightToLeft property is set to System.Windows.Forms.RightToLeft.Yes.
    //
    // Returns:
    //     true to automatically mirror the image; otherwise, false. The default is
    //     false.
    //[Localizable(true)]
    //[DefaultValue(false)]
    //public bool RightToLeftAutoMirrorImage { get; set; }
    //
    // Summary:
    //     Gets a value indicating whether the item is selected.
    //
    // Returns:
    //     true if the System.Windows.Forms.ToolStripItem is selected; otherwise, false.
    //[Browsable(false)]
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    // public virtual bool Selected { get; }
    //
    // Summary:
    //     Gets a value indicating whether to show or hide shortcut keys.
    //
    // Returns:
    //     true to show shortcut keys; otherwise, false. The default is true.
    // protected internal virtual bool ShowKeyboardCues { get; }
    //
    // Summary:
    //     Gets or sets the size of the item.
    //
    // Returns:
    //     A System.Drawing.Size, representing the width and height of a rectangle.
    //[Localizable(true)]
    // public virtual System.Drawing.Size Size { get; set; }
    //
    // Summary:
    //     Gets or sets the object that contains data about the item.
    //
    // Returns:
    //     An System.Object that contains data about the control. The default is null.
    //[Localizable(false)]
    //[DefaultValue("")]
    //[TypeConverter(typeof(StringConverter))]
    //[Bindable(true)]
    //public object Tag { get; set; }
    ////
    // Summary:
    //     Gets or sets the text that is to be displayed on the item.
    //
    // Returns:
    //     A string representing the item's text. The default value is the empty string
    //     ("").
    //[Localizable(true)]
    //[DefaultValue("")]
    // public virtual string Text { get; set; }
    //
    // Summary:
    //     Gets or sets the alignment of the text on a System.Windows.Forms.ToolStripLabel.
    //
    // Returns:
    //     One of the System.Drawing.ContentAlignment values. The default is System.Drawing.ContentAlignment.MiddleRight.
    //
    // Exceptions:
    //   System.ComponentModel.InvalidEnumArgumentException:
    //     The value assigned is not one of the System.Drawing.ContentAlignment values.
    //[Localizable(true)]
    // public virtual System.Drawing.ContentAlignment TextAlign { get; set; }
    //
    // Summary:
    //     Gets the orientation of text used on a System.Windows.Forms.ToolStripItem.
    //
    // Returns:
    //     One of the System.Windows.Forms.ToolStripTextDirection values.
    // public virtual ToolStripTextDirection TextDirection { get; set; }
    //
    // Summary:
    //     Gets or sets the position of System.Windows.Forms.ToolStripItem text and
    //     image relative to each other.
    //
    // Returns:
    //     One of the System.Windows.Forms.TextImageRelation values. The default is
    //     System.Windows.Forms.TextImageRelation.ImageBeforeText.
    //[Localizable(true)]
    //public TextImageRelation TextImageRelation { get; set; }
    //
    // Summary:
    //     Gets or sets the text that appears as a System.Windows.Forms.ToolTip for
    //     a control.
    //
    // Returns:
    //     A string representing the ToolTip text.
    //[Localizable(true)]
    //public string ToolTipText { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether the item is displayed.
    //
    // Returns:
    //     true if the item is displayed; otherwise, false.
    //[Localizable(true)]
    //public bool Visible { get; set; }
    //
    // Summary:
    //     Gets or sets the width in pixels of a System.Windows.Forms.ToolStripItem.
    //
    // Returns:
    //     An System.Int32 representing the width in pixels.
    //[EditorBrowsable(EditorBrowsableState.Always)]
    //[Browsable(false)]
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //public int Width { get; set; }

    // Summary:
    //     Occurs when the value of the System.Windows.Forms.ToolStripItem.Available
    //     property changes.
    //[Browsable(false)]
    // public event EventHandler AvailableChanged;
    //
    // Summary:
    //     Occurs when the value of the System.Windows.Forms.ToolStripItem.BackColor
    //     property changes.
    // public event EventHandler BackColorChanged;
    //
    // Summary:
    //     Occurs when the System.Windows.Forms.ToolStripItem is clicked.
    // public event EventHandler Click;
    //
    // Summary:
    //     Occurs when the System.Windows.Forms.ToolStripItem.DisplayStyle has changed.
    // public event EventHandler DisplayStyleChanged;
    //
    // Summary:
    //     Occurs when the item is double-clicked with the mouse.
    // public event EventHandler DoubleClick;
    //
    // Summary:
    //     Occurs when the user drags an item and the user releases the mouse button,
    //     indicating that the item should be dropped into this item.
    //[Browsable(false)]
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    // public event DragEventHandler DragDrop;
    //
    // Summary:
    //     Occurs when the user drags an item into the client area of this item.
    //[Browsable(false)]
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    // public event DragEventHandler DragEnter;
    //
    // Summary:
    //     Occurs when the user drags an item and the mouse pointer is no longer over
    //     the client area of this item.
    //[Browsable(false)]
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    // public event EventHandler DragLeave;
    //
    // Summary:
    //     Occurs when the user drags an item over the client area of this item.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    //[Browsable(false)]
    // public event DragEventHandler DragOver;
    //
    // Summary:
    //     Occurs when the System.Windows.Forms.ToolStripItem.Enabled property value
    //     has changed.
    // public event EventHandler EnabledChanged;
    //
    // Summary:
    //     Occurs when the System.Windows.Forms.ToolStripItem.ForeColor property value
    //     changes.
    // public event EventHandler ForeColorChanged;
    //
    // Summary:
    //     Occurs during a drag operation.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    //[Browsable(false)]
    // public event GiveFeedbackEventHandler GiveFeedback;
    //
    // Summary:
    //     Occurs when the location of a System.Windows.Forms.ToolStripItem is updated.
    // public event EventHandler LocationChanged;
    //
    // Summary:
    //     Occurs when the mouse pointer is over the item and a mouse button is pressed.
    // public event MouseEventHandler MouseDown;
    //
    // Summary:
    //     Occurs when the mouse pointer enters the item.
    // public event EventHandler MouseEnter;
    //
    // Summary:
    //     Occurs when the mouse pointer hovers over the item.
    // public event EventHandler MouseHover;
    //
    // Summary:
    //     Occurs when the mouse pointer leaves the item.
    // public event EventHandler MouseLeave;
    //
    // Summary:
    //     Occurs when the mouse pointer is moved over the item.
    // public event MouseEventHandler MouseMove;
    //
    // Summary:
    //     Occurs when the mouse pointer is over the item and a mouse button is released.
    // public event MouseEventHandler MouseUp;
    //
    // Summary:
    //     Occurs when the System.Windows.Forms.ToolStripItem.Owner property changes.
    // public event EventHandler OwnerChanged;
    //
    // Summary:
    //     Occurs when the item is redrawn.
    // public event PaintEventHandler Paint;
    //
    // Summary:
    //     Occurs when an accessibility client application invokes help for the System.Windows.Forms.ToolStripItem.
    // public event QueryAccessibilityHelpEventHandler QueryAccessibilityHelp;
    //
    // Summary:
    //     Occurs during a drag-and-drop operation and allows the drag source to determine
    //     whether the drag-and-drop operation should be canceled.
    //[Browsable(false)]
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    // public event QueryContinueDragEventHandler QueryContinueDrag;
    //
    // Summary:
    //     Occurs when the System.Windows.Forms.ToolStripItem.RightToLeft property value
    //     changes.
    // public event EventHandler RightToLeftChanged;
    //
    // Summary:
    //     Occurs when the value of the System.Windows.Forms.ToolStripItem.Text property
    //     changes.
    // public event EventHandler TextChanged;
    //
    // Summary:
    //     Occurs when the value of the System.Windows.Forms.ToolStripItem.Visible property
    //     changes.
    // public event EventHandler VisibleChanged;

    // Summary:
    //     Creates a new accessibility object for the System.Windows.Forms.ToolStripItem.
    //
    // Returns:
    //     A new System.Windows.Forms.AccessibleObject for the System.Windows.Forms.ToolStripItem.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    // protected virtual AccessibleObject CreateAccessibilityInstance();
    //
    // Summary:
    //     Releases the unmanaged resources used by the System.Windows.Forms.ToolStripItem
    //     and optionally releases the managed resources.
    //
    // Parameters:
    //   disposing:
    //     true to release both managed and unmanaged resources; false to release only
    //     unmanaged resources.
    // protected override void Dispose(bool disposing);
    //
    // Summary:
    //     Begins a drag-and-drop operation.
    //
    // Parameters:
    //   data:
    //     The object to be dragged.
    //
    //   allowedEffects:
    //     The drag operations that can occur.
    //
    // Returns:
    //     One of the System.Windows.Forms.DragDropEffects values.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    //public DragDropEffects DoDragDrop(object data, DragDropEffects allowedEffects);
    //
    // Summary:
    //     Retrieves the System.Windows.Forms.ToolStrip that is the container of the
    //     current System.Windows.Forms.ToolStripItem.
    //
    // Returns:
    //     A System.Windows.Forms.ToolStrip that is the container of the current System.Windows.Forms.ToolStripItem.
    //public ToolStrip GetCurrentParent();
    //
    // Summary:
    //     Retrieves the size of a rectangular area into which a control can be fit.
    //
    // Parameters:
    //   constrainingSize:
    //     The custom-sized area for a control.
    //
    // Returns:
    //     A System.Drawing.Size ordered pair, representing the width and height of
    //     a rectangle.
    // public virtual System.Drawing.Size GetPreferredSize(System.Drawing.Size constrainingSize);
    //
    // Summary:
    //     Invalidates the entire surface of the System.Windows.Forms.ToolStripItem
    //     and causes it to be redrawn.
    //public void Invalidate();
    //
    // Summary:
    //     Invalidates the specified region of the System.Windows.Forms.ToolStripItem
    //     by adding it to the update region of the System.Windows.Forms.ToolStripItem,
    //     which is the area that will be repainted at the next paint operation, and
    //     causes a paint message to be sent to the System.Windows.Forms.ToolStripItem.
    //
    // Parameters:
    //   r:
    //     A System.Drawing.Rectangle that represents the region to invalidate.
    //public void Invalidate(System.Drawing.Rectangle r);
    //
    // Summary:
    //     Determines whether a character is an input character that the item recognizes.
    //
    // Parameters:
    //   charCode:
    //     The character to test.
    //
    // Returns:
    //     true if the character should be sent directly to the item and not preprocessed;
    //     otherwise, false.
    // protected internal virtual bool IsInputChar(char charCode);
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
    // protected internal virtual bool IsInputKey(Keys keyData);
    //
    // Summary:
    //     Raises the AvailableChanged event.
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    // protected virtual void OnAvailableChanged(EventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.ToolStripItem.BackColorChanged event.
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    // protected virtual void OnBackColorChanged(EventArgs e);
    //
    // Summary:
    //     Occurs when the System.Windows.Forms.ToolStripItem.Bounds property changes.
    // protected virtual void OnBoundsChanged();
    //
    // Summary:
    //     Raises the System.Windows.Forms.ToolStripItem.Click event.
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    // protected virtual void OnClick(EventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.ToolStripItem.DisplayStyleChanged event.
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    // protected virtual void OnDisplayStyleChanged(EventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.ToolStripItem.DoubleClick event.
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    // protected virtual void OnDoubleClick(EventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.ToolStripItem.DragDrop event.
    //
    // Parameters:
    //   dragEvent:
    //     A System.Windows.Forms.DragEventArgs that contains the event data.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    // protected virtual void OnDragDrop(DragEventArgs dragEvent);
    //
    // Summary:
    //     Raises the System.Windows.Forms.ToolStripItem.DragEnter event.
    //
    // Parameters:
    //   dragEvent:
    //     A System.Windows.Forms.DragEventArgs that contains the event data.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    // protected virtual void OnDragEnter(DragEventArgs dragEvent);
    //
    // Summary:
    //     Raises the System.Windows.Forms.ToolStripItem.DragLeave event.
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    // protected virtual void OnDragLeave(EventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.ToolStripItem.DragOver event.
    //
    // Parameters:
    //   dragEvent:
    //     A System.Windows.Forms.DragEventArgs that contains the event data.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    // protected virtual void OnDragOver(DragEventArgs dragEvent);
    //
    // Summary:
    //     Raises the System.Windows.Forms.ToolStripItem.EnabledChanged event.
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    // protected virtual void OnEnabledChanged(EventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.Control.FontChanged event.
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    // protected virtual void OnFontChanged(EventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.ToolStripItem.ForeColorChanged event.
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    // protected virtual void OnForeColorChanged(EventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.ToolStripItem.GiveFeedback event.
    //
    // Parameters:
    //   giveFeedbackEvent:
    //     A System.Windows.Forms.GiveFeedbackEventArgs that contains the event data.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    // protected virtual void OnGiveFeedback(GiveFeedbackEventArgs giveFeedbackEvent);
    //
    // Summary:
    //     Raises the System.Windows.Forms.Control.Layout event.
    //
    // Parameters:
    //   e:
    //     A System.Windows.Forms.LayoutEventArgs that contains the event data.
    // protected internal virtual void OnLayout(LayoutEventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.ToolStripItem.LocationChanged event.
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    // protected virtual void OnLocationChanged(EventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.ToolStripItem.MouseDown event.
    //
    // Parameters:
    //   e:
    //     A System.Windows.Forms.MouseEventArgs that contains the event data.
    // protected virtual void OnMouseDown(MouseEventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.ToolStripItem.MouseEnter event.
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    // protected virtual void OnMouseEnter(EventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.ToolStripItem.MouseHover event.
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    // protected virtual void OnMouseHover(EventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.ToolStripItem.MouseLeave event.
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    // protected virtual void OnMouseLeave(EventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.ToolStripItem.MouseMove event.
    //
    // Parameters:
    //   mea:
    //     A System.Windows.Forms.MouseEventArgs that contains the event data.
    // protected virtual void OnMouseMove(MouseEventArgs mea);
    //
    // Summary:
    //     Raises the System.Windows.Forms.ToolStripItem.MouseUp event.
    //
    // Parameters:
    //   e:
    //     A System.Windows.Forms.MouseEventArgs that contains the event data.
    // protected virtual void OnMouseUp(MouseEventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.ToolStripItem.OwnerChanged event.
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    // protected virtual void OnOwnerChanged(EventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.Control.FontChanged event when the System.Windows.Forms.ToolStripItem.Font
    //     property has changed on the parent of the System.Windows.Forms.ToolStripItem.
    //
    // Parameters:
    //   e:
    //     A System.EventArgs that contains the event data.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    // protected internal virtual void OnOwnerFontChanged(EventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.ToolStripItem.Paint event.
    //
    // Parameters:
    //   e:
    //     A System.Windows.Forms.PaintEventArgs that contains the event data.
    // protected virtual void OnPaint(PaintEventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.ToolStripItem.BackColorChanged event.
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    // protected virtual void OnParentBackColorChanged(EventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.Control.ParentChanged event.
    //
    // Parameters:
    //   oldParent:
    //     The original parent of the item.
    //
    //   newParent:
    //     The new parent of the item.
    // protected virtual void OnParentChanged(ToolStrip oldParent, ToolStrip newParent);
    //
    // Summary:
    //     Raises the System.Windows.Forms.ToolStripItem.EnabledChanged event when the
    //     System.Windows.Forms.ToolStripItem.Enabled property value of the item's container
    //     changes.
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    // protected internal virtual void OnParentEnabledChanged(EventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.ToolStripItem.ForeColorChanged event.
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    // protected virtual void OnParentForeColorChanged(EventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.ToolStripItem.RightToLeftChanged event.
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    // protected internal virtual void OnParentRightToLeftChanged(EventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.ToolStripItem.QueryContinueDrag event.
    //
    // Parameters:
    //   queryContinueDragEvent:
    //     A System.Windows.Forms.QueryContinueDragEventArgs that contains the event
    //     data.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    // protected virtual void OnQueryContinueDrag(QueryContinueDragEventArgs queryContinueDragEvent);
    //
    // Summary:
    //     Raises the System.Windows.Forms.ToolStripItem.RightToLeftChanged event.
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    // protected virtual void OnRightToLeftChanged(EventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.ToolStripItem.TextChanged event.
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    // protected virtual void OnTextChanged(EventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.ToolStripItem.VisibleChanged event.
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    // protected virtual void OnVisibleChanged(EventArgs e);
    //
    // Summary:
    //     Activates the System.Windows.Forms.ToolStripItem when it is clicked with
    //     the mouse.
    //public void PerformClick();
    //
    // protected internal virtual bool ProcessCmdKey(ref Message m, Keys keyData);
    //
    // protected internal virtual bool ProcessDialogKey(Keys keyData);
    //
    // protected internal virtual bool ProcessMnemonic(char charCode);
    //
    // Summary:
    //     This method is not relevant to this class.
    //[EditorBrowsable(EditorBrowsableState.Never)]
    // public virtual void ResetBackColor();
    //
    // Summary:
    //     This method is not relevant to this class.
    //[EditorBrowsable(EditorBrowsableState.Never)]
    // public virtual void ResetDisplayStyle();
    //
    // Summary:
    //     This method is not relevant to this class.
    //[EditorBrowsable(EditorBrowsableState.Never)]
    // public virtual void ResetFont();
    //
    // Summary:
    //     This method is not relevant to this class.
    //[EditorBrowsable(EditorBrowsableState.Never)]
    // public virtual void ResetForeColor();
    //
    // Summary:
    //     This method is not relevant to this class.
    //[EditorBrowsable(EditorBrowsableState.Never)]
    // public virtual void ResetImage();
    //
    // Summary:
    //     This method is not relevant to this class.
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //public void ResetMargin();
    //
    // Summary:
    //     This method is not relevant to this class.
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //public void ResetPadding();
    //
    // Summary:
    //     This method is not relevant to this class.
    //[EditorBrowsable(EditorBrowsableState.Never)]
    // public virtual void ResetRightToLeft();
    //
    // Summary:
    //     This method is not relevant to this class.
    //[EditorBrowsable(EditorBrowsableState.Never)]
    // public virtual void ResetTextDirection();
    //
    // Summary:
    //     Selects the item.
    //public void Select();
    //
    // Summary:
    //     Sets the size and location of the item.
    //
    // Parameters:
    //   bounds:
    //     A System.Drawing.Rectangle that represents the size and location of the System.Windows.Forms.ToolStripItem
    // protected internal virtual void SetBounds(System.Drawing.Rectangle bounds);
    //
    // Summary:
    //     Sets the System.Windows.Forms.ToolStripItem to the specified visible state.
    //
    // Parameters:
    //   visible:
    //     true to make the System.Windows.Forms.ToolStripItem visible; otherwise, false.
    // protected virtual void SetVisibleCore(bool visible);
    //
    //
    // Returns:
    //     A System.String containing the name of the System.ComponentModel.Component,
    //     if any, or null if the System.ComponentModel.Component is unnamed.
    // public override string ToString();

    // Summary:
    //     Provides information that accessibility applications use to adjust the user
    //     interface of a System.Windows.Forms.ToolStripItem for users with impairments.
    //[ComVisible(true)]
    //public class ToolStripItemAccessibleObject : AccessibleObject
    //{
    //  // Summary:
    //  //     Initializes a new instance of the System.Windows.Forms.ToolStripItem.ToolStripItemAccessibleObject
    //  //     class.
    //  //
    //  // Parameters:
    //  //   ownerItem:
    //  //     The System.Windows.Forms.ToolStripItem that owns this System.Windows.Forms.ToolStripItem.ToolStripItemAccessibleObject.
    //  //
    //  // Exceptions:
    //  //   System.ArgumentNullException:
    //  //     The ownerItem parameter is null.
    //  public ToolStripItemAccessibleObject(ToolStripItem ownerItem);

    //  // Summary:
    //  //     Gets the bounds of the accessible object, in screen coordinates.
    //  //
    //  // Returns:
    //  //     An object of type System.Drawing.Rectangle representing the bounds.
    //  // public override System.Drawing.Rectangle Bounds { get; }
    //  //
    //  // Summary:
    //  //     Gets a string that describes the default action of the System.Windows.Forms.ToolStripItem.
    //  //
    //  // Returns:
    //  //     A description of the default action of the System.Windows.Forms.ToolStripItem.
    //  // public override string DefaultAction { get; }
    //  //
    //  // Summary:
    //  //     Gets the description of the System.Windows.Forms.Control.ControlAccessibleObject.
    //  //
    //  // Returns:
    //  //     A string describing the System.Windows.Forms.ToolStripItem.ToolStripItemAccessibleObject.
    //  // public override string Description { get; }
    //  //
    //  // Summary:
    //  //     Gets the description of what the object does or how the object is used.
    //  //
    //  // Returns:
    //  //     A string describing what the object does or how the object is used.
    //  // public override string Help { get; }
    //  //
    //  // Summary:
    //  //     Gets the shortcut key or access key for the accessible object.
    //  //
    //  // Returns:
    //  //     The shortcut key or access key for the accessible object, or null if there
    //  //     is no shortcut key associated with the object.
    //  // public override string KeyboardShortcut { get; }
    //  //
    //  // Summary:
    //  //     Gets or sets the name of the accessible object.
    //  //
    //  // Returns:
    //  //     The object name, or null if the property has not been set.
    //  // public override string Name { get; set; }
    //  //
    //  // Summary:
    //  //     Gets or sets the parent of an accessible object.
    //  //
    //  // Returns:
    //  //     An object of type System.Windows.Forms.AccessibleObject representing the
    //  //     parent.
    //  // public override AccessibleObject Parent { get; }
    //  //
    //  // Summary:
    //  //     Gets the role of this accessible object.
    //  //
    //  // Returns:
    //  //     One of the System.Windows.Forms.AccessibleRole values.
    //  // public override AccessibleRole Role { get; }
    //  //
    //  // Summary:
    //  //     Gets the state of this accessible object.
    //  //
    //  // Returns:
    //  //     One of the System.Windows.Forms.AccessibleStates values, or System.Windows.Forms.AccessibleStates.None
    //  //     if no state has been set.
    //  // public override AccessibleStates State { get; }

    //  // Summary:
    //  //     Adds a System.Windows.Forms.ToolStripItem.ToolStripItemAccessibleObject.State
    //  //     if System.Windows.Forms.AccessibleStates is System.Windows.Forms.AccessibleStates.None.
    //  //
    //  // Parameters:
    //  //   state:
    //  //     One of the System.Windows.Forms.AccessibleStates values other than System.Windows.Forms.AccessibleStates.None.
    //  public void AddState(AccessibleStates state);
    //  //
    //  // Summary:
    //  //     Performs the default action associated with this accessible object.
    //  // public override void DoDefaultAction();
    //  //
    //  // Summary:
    //  //     Gets an identifier for a Help topic and the path to the Help file associated
    //  //     with this accessible object.
    //  //
    //  // Parameters:
    //  //   fileName:
    //  //     When this method returns, contains a string that represents the path to the
    //  //     Help file associated with this accessible object. This parameter is passed
    //  //     without being initialized.
    //  //
    //  // Returns:
    //  //     An identifier for a Help topic, or -1 if there is no Help topic. On return,
    //  //     the fileName parameter will contain the path to the Help file associated
    //  //     with this accessible object, or null if there is no IAccessible interface
    //  //     specified.
    //  // public override int GetHelpTopic(out string fileName);
    //  //
    //  // Summary:
    //  //     Navigates to another accessible object.
    //  //
    //  // Parameters:
    //  //   navigationDirection:
    //  //     One of the System.Windows.Forms.AccessibleNavigation values.
    //  //
    //  // Returns:
    //  //     An System.Windows.Forms.AccessibleObject that represents one of the System.Windows.Forms.AccessibleNavigation
    //  //     values.
    //  // public override AccessibleObject Navigate(AccessibleNavigation navigationDirection);
    //  //
    //  //
    //  // Returns:
    //  //     A System.String that represents the current System.Object.
    //  // public override string ToString();
    //}
  }
}
