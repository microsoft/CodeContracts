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
  //     Provides a container for Windows toolbar objects.
  //[DefaultEvent("ItemClicked")]
  //[ComVisible(true)]
  //[DesignerSerializer("System.Windows.Forms.Design.ToolStripCodeDomSerializer, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.Serialization.CodeDomSerializer, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
  //[DefaultProperty("Items")]
  //[ClassInterface(ClassInterfaceType.AutoDispatch)]
  //[Designer("System.Windows.Forms.Design.ToolStripDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
  public class ToolStrip 
  {
    // Summary:
    //     Initializes a new instance of the System.Windows.Forms.ToolStrip class.
    public ToolStrip() { }
    //
    // Summary:
    //     Initializes a new instance of the System.Windows.Forms.ToolStrip class with
    //     the specified array of System.Windows.Forms.ToolStripItems.
    //
    // Parameters:
    //   items:
    //     An array of System.Windows.Forms.ToolStripItem objects.
    public ToolStrip(params ToolStripItem[] items) { }

    // Summary:
    //     Gets or sets a value indicating whether drag-and-drop and item reordering
    //     are handled through events that you implement.
    //
    // Returns:
    //     true to control drag-and-drop and item reordering through events that you
    //     implement; otherwise, false.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     System.Windows.Forms.ToolStrip.AllowDrop and System.Windows.Forms.ToolStrip.AllowItemReorder
    //     are both set to true.
    // public override bool AllowDrop { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether drag-and-drop and item reordering
    //     are handled privately by the System.Windows.Forms.ToolStrip class.
    //
    // Returns:
    //     true to cause the System.Windows.Forms.ToolStrip class to handle drag-and-drop
    //     and item reordering automatically; otherwise, false. The default value is
    //     false.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     System.Windows.Forms.ToolStrip.AllowDrop and System.Windows.Forms.ToolStrip.AllowItemReorder
    //     are both set to true.
    //[DefaultValue(false)]
    //public bool AllowItemReorder { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether multiple System.Windows.Forms.MenuStrip,
    //     System.Windows.Forms.ToolStripDropDownMenu, System.Windows.Forms.ToolStripMenuItem,
    //     and other types can be combined.
    //
    // Returns:
    //     true if combining of types is allowed; otherwise, false. The default is false.
    //[DefaultValue(true)]
    //public bool AllowMerge { get; set; }
    //
    // Summary:
    //     Gets or sets the edges of the container to which a System.Windows.Forms.ToolStrip
    //     is bound and determines how a System.Windows.Forms.ToolStrip is resized with
    //     its parent.
    //
    // Returns:
    //     One of the System.Windows.Forms.AnchorStyles values.
    // public override AnchorStyles Anchor { get; set; }
    //
    // Summary:
    //     This property is not relevant for this class.
    //
    // Returns:
    //     true to automatically scroll; otherwise, false.
    //
    // Exceptions:
    //   System.NotSupportedException:
    //     Automatic scrolling is not supported by System.Windows.Forms.ToolStrip controls.
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //[Browsable(false)]
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    // public override bool AutoScroll { get; set; }
    //
    // Summary:
    //     This property is not relevant for this class.
    //
    // Returns:
    //     A System.Drawing.Size value.
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //[Browsable(false)]
    //public System.Drawing.Size AutoScrollMargin { get; set; }
    //
    // Summary:
    //     This property is not relevant for this class.
    //
    // Returns:
    //     A System.Drawing.Size value.
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //[Browsable(false)]
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //public System.Drawing.Size AutoScrollMinSize { get; set; }
    //
    // Summary:
    //     This property is not relevant for this class.
    //
    // Returns:
    //     A System.Drawing.Point value.
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //[Browsable(false)]
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //public System.Drawing.Point AutoScrollPosition { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether the control is automatically resized
    //     to display its entire contents.
    //
    // Returns:
    //     true if the control adjusts its width to closely fit its contents; otherwise,
    //     false. The default is true.
    //[DefaultValue(true)]
    //[Browsable(true)]
    //[EditorBrowsable(EditorBrowsableState.Always)]
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    // public override bool AutoSize { get; set; }
    //
    // Summary:
    //     Gets or sets the background color for the System.Windows.Forms.ToolStrip.
    //
    // Returns:
    //     A System.Drawing.Color that represents the background color of the System.Windows.Forms.ToolStrip.
    //     The default is the value of the System.Windows.Forms.Control.DefaultBackColor
    //     property.
    //public System.Drawing.Color BackColor { get; set; }
    //
    // Summary:
    //     Gets or sets the binding context for the System.Windows.Forms.ToolStrip.
    //
    // Returns:
    //     A System.Windows.Forms.BindingContext for the System.Windows.Forms.ToolStrip.
    // public override BindingContext BindingContext { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether items in the System.Windows.Forms.ToolStrip
    //     can be sent to an overflow menu.
    //
    // Returns:
    //     true to send System.Windows.Forms.ToolStrip items to an overflow menu; otherwise,
    //     false. The default value is true.
    //[DefaultValue(true)]
    //public bool CanOverflow { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether the System.Windows.Forms.ToolStrip
    //     causes validation to be performed on any controls that require validation
    //     when it receives focus.
    //
    // Returns:
    //     false in all cases.
    //[DefaultValue(false)]
    //[Browsable(false)]
    //public bool CausesValidation { get; set; }
    //
    // Summary:
    //     This property is not relevant for this class.
    //
    // Returns:
    //     A System.Windows.Forms.Control.ControlCollection representing the collection
    //     of controls contained within the System.Windows.Forms.ToolStrip.
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //public Control.ControlCollection Controls { get; }
    //
    // Summary:
    //     Gets or sets the cursor that is displayed when the mouse pointer is over
    //     the System.Windows.Forms.ToolStrip.
    //
    // Returns:
    //     A System.Windows.Forms.Cursor that represents the cursor to display when
    //     the mouse pointer is over the System.Windows.Forms.ToolStrip.
    //[Browsable(false)]
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    // public override Cursor Cursor { get; set; }
    //
    // Summary:
    //     Gets the docking location of the System.Windows.Forms.ToolStrip, indicating
    //     which borders are docked to the container.
    //
    // Returns:
    //     One of the System.Windows.Forms.DockStyle values. The default is System.Windows.Forms.DockStyle.Top.
    // protected virtual DockStyle DefaultDock { get; }
    //
    // Summary:
    //     Gets or sets a value representing the default direction in which a System.Windows.Forms.ToolStripDropDown
    //     control is displayed relative to the System.Windows.Forms.ToolStrip.
    //
    // Returns:
    //     One of the System.Windows.Forms.ToolStripDropDownDirection values.
    //
    // Exceptions:
    //   System.ComponentModel.InvalidEnumArgumentException:
    //     The specified value is not one of the System.Windows.Forms.ToolStripDropDownDirection
    //     values.
    //[Browsable(false)]
    //// public virtual ToolStripDropDownDirection DefaultDropDownDirection { get; set; }
    //
    // Summary:
    //     Gets the default spacing, in pixels, between the sizing grip and the edges
    //     of the System.Windows.Forms.ToolStrip.
    //
    // Returns:
    //     System.Windows.Forms.Padding values representing the spacing, in pixels.
    // protected virtual Padding DefaultGripMargin { get; }
    //
    // Summary:
    //     Gets the spacing, in pixels, between the System.Windows.Forms.ToolStrip and
    //     the System.Windows.Forms.ToolStripContainer.
    //
    // Returns:
    //     One of the System.Windows.Forms.Padding values. The default is System.Windows.Forms.Padding.Empty.
    // protected override Padding DefaultMargin { get; }
    //
    // Summary:
    //     Gets the internal spacing, in pixels, of the contents of a System.Windows.Forms.ToolStrip.
    //
    // Returns:
    //     A System.Windows.Forms.Padding value of (0, 0, 1, 0).
    // protected override Padding DefaultPadding { get; }
    //
    // Summary:
    //     Gets a value indicating whether ToolTips are shown for the System.Windows.Forms.ToolStrip
    //     by default.
    //
    // Returns:
    //     true in all cases.
    // protected virtual bool DefaultShowItemToolTips { get; }
    //
    // Summary:
    //     Gets the default size of the System.Windows.Forms.ToolStrip.
    //
    // Returns:
    //     The default System.Drawing.Size of the System.Windows.Forms.ToolStrip.
    // protected override System.Drawing.Size DefaultSize { get; }
    //
    // Summary:
    //     Gets the subset of items that are currently displayed on the System.Windows.Forms.ToolStrip,
    //     including items that are automatically added into the System.Windows.Forms.ToolStrip.
    //
    // Returns:
    //     A System.Windows.Forms.ToolStripItemCollection representing the items that
    //     are currently displayed on the System.Windows.Forms.ToolStrip.
    // protected internal virtual ToolStripItemCollection DisplayedItems { get; }
    //
    // Summary:
    //     Retrieves the current display rectangle.
    //
    // Returns:
    //     A System.Drawing.Rectangle representing the System.Windows.Forms.ToolStrip
    //     area for item layout.
    // public override System.Drawing.Rectangle DisplayRectangle { get; }
    //
    // Summary:
    //     Gets or sets which System.Windows.Forms.ToolStrip borders are docked to its
    //     parent control and determines how a System.Windows.Forms.ToolStrip is resized
    //     with its parent.
    //
    // Returns:
    //     One of the System.Windows.Forms.DockStyle values. The default value is System.Windows.Forms.DockStyle.Top.
    // public override DockStyle Dock { get; set; }
    //
    // Summary:
    //     Gets or sets the font used to display text in the control.
    //
    // Returns:
    //     The current default font.
    // public override System.Drawing.Font Font { get; set; }
    //
    // Summary:
    //     Gets or sets the foreground color of the System.Windows.Forms.ToolStrip.
    //
    // Returns:
    //     A System.Drawing.Color representing the foreground color.
    //[Browsable(false)]
    //public System.Drawing.Color ForeColor { get; set; }
    //
    // Summary:
    //     Gets the orientation of the System.Windows.Forms.ToolStrip move handle.
    //
    // Returns:
    //     One of the System.Windows.Forms.ToolStripGripDisplayStyle values. Possible
    //     values are System.Windows.Forms.ToolStripGripDisplayStyle.Horizontal and
    //     System.Windows.Forms.ToolStripGripDisplayStyle.Vertical.
    //[Browsable(false)]
    //public ToolStripGripDisplayStyle GripDisplayStyle { get; }
    //
    // Summary:
    //     Gets or sets the space around the System.Windows.Forms.ToolStrip move handle.
    //
    // Returns:
    //     A System.Windows.Forms.Padding, which represents the spacing.
    //public Padding GripMargin { get; set; }
    //
    // Summary:
    //     Gets the boundaries of the System.Windows.Forms.ToolStrip move handle.
    //
    // Returns:
    //     An object of type System.Drawing.Rectangle, representing the move handle
    //     boundaries. If the boundaries are not visible, the System.Windows.Forms.ToolStrip.GripRectangle
    //     property returns System.Drawing.Rectangle.Empty.
    //[Browsable(false)]
    //public System.Drawing.Rectangle GripRectangle { get; }
    //
    // Summary:
    //     Gets or sets whether the System.Windows.Forms.ToolStrip move handle is visible
    //     or hidden.
    //
    // Returns:
    //     One of the System.Windows.Forms.ToolStripGripStyle values. The default value
    //     is System.Windows.Forms.ToolStripGripStyle.Visible.
    //
    // Exceptions:
    //   System.ComponentModel.InvalidEnumArgumentException:
    //     The specified value is not one of the System.Windows.Forms.ToolStripGripStyle
    //     values.
    //public ToolStripGripStyle GripStyle { get; set; }
    //
    // Summary:
    //     This property is not relevant for this class.
    //
    // Returns:
    //     true if the System.Windows.Forms.ToolStrip has children; otherwise, false.
    //[Browsable(false)]
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //public bool HasChildren { get; }
    //
    // Summary:
    //     This property is not relevant for this class.
    //
    // Returns:
    //     An instance of the System.Windows.Forms.HScrollProperties class, which provides
    //     basic properties for an System.Windows.Forms.HScrollBar.
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //[Browsable(false)]
    //public HScrollProperties HorizontalScroll { get; }
    //
    // Summary:
    //     Gets or sets the image list that contains the image displayed on a System.Windows.Forms.ToolStrip
    //     item.
    //
    // Returns:
    //     An object of type System.Windows.Forms.ImageList.
    //[Browsable(false)]
    //[DefaultValue("")]
    //public ImageList ImageList { get; set; }
    //
    // Summary:
    //     Gets or sets the size, in pixels, of an image used on a System.Windows.Forms.ToolStrip.
    //
    // Returns:
    //     A System.Drawing.Size value representing the size of the image, in pixels.
    //     The default is 16 x 16 pixels.
    //public System.Drawing.Size ImageScalingSize { get; set; }
    //
    // Summary:
    //     Gets a value indicating whether the user is currently moving the System.Windows.Forms.ToolStrip
    //     from one System.Windows.Forms.ToolStripContainer to another.
    //
    // Returns:
    //     true if the user is currently moving the System.Windows.Forms.ToolStrip from
    //     one System.Windows.Forms.ToolStripContainer to another; otherwise, false.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    //[Browsable(false)]
    //public bool IsCurrentlyDragging { get; }
    //
    // Summary:
    //     Gets a value indicating whether a System.Windows.Forms.ToolStrip is a System.Windows.Forms.ToolStripDropDown
    //     control.
    //
    // Returns:
    //     true if the System.Windows.Forms.ToolStrip is a System.Windows.Forms.ToolStripDropDown
    //     control; otherwise, false.
    //[Browsable(false)]
    //public bool IsDropDown { get; }
    //
    // Summary:
    //     Gets all the items that belong to a System.Windows.Forms.ToolStrip.
    //
    // Returns:
    //     An object of type System.Windows.Forms.ToolStripItemCollection, representing
    //     all the elements contained by a System.Windows.Forms.ToolStrip.
    //[MergableProperty(false)]
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
     public virtual ToolStripItemCollection Items 
     {
       get
       {
         // To add: \forall c in result. c != null
         Contract.Ensures(Contract.Result<ToolStripItemCollection>() != null);

         return default(ToolStripItemCollection);
       }
     }
    //
    // Summary:
    //     Passes a reference to the cached System.Windows.Forms.Control.LayoutEngine
    //     returned by the layout engine interface.
    //
    // Returns:
    //     A System.Windows.Forms.Layout.LayoutEngine that represents the cached layout
    //     engine returned by the layout engine interface.
    // public override LayoutEngine LayoutEngine { get; }
    //
    // Summary:
    //     Gets or sets layout scheme characteristics.
    //
    // Returns:
    //     A System.Windows.Forms.LayoutSettings representing the layout scheme characteristics.
    //[Browsable(false)]
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //[DefaultValue("")]
    //public LayoutSettings LayoutSettings { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating how the System.Windows.Forms.ToolStrip lays
    //     out the items collection.
    //
    // Returns:
    //     One of the System.Windows.Forms.ToolStripLayoutStyle values. The possible
    //     values are System.Windows.Forms.ToolStripLayoutStyle.Table, System.Windows.Forms.ToolStripLayoutStyle.Flow,
    //     System.Windows.Forms.ToolStripLayoutStyle.StackWithOverflow, System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow,
    //     and System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow.
    //
    // Exceptions:
    //   System.ComponentModel.InvalidEnumArgumentException:
    //     The value of System.Windows.Forms.ToolStrip.LayoutStyle is not one of the
    //     System.Windows.Forms.ToolStripLayoutStyle values.
    //public ToolStripLayoutStyle LayoutStyle { get; set; }
    //
    // Summary:
    //     Gets the maximum height and width, in pixels, of the System.Windows.Forms.ToolStrip.
    //
    // Returns:
    //     A System.Drawing.Size representing the height and width of the control, in
    //     pixels.
    // protected internal virtual System.Drawing.Size MaxItemSize { get; }
    //
    // Summary:
    //     Gets the orientation of the System.Windows.Forms.ToolStripPanel.
    //
    // Returns:
    //     One of the System.Windows.Forms.Orientation values. The default is System.Windows.Forms.Orientation.Horizontal.
    //[Browsable(false)]
    //public Orientation Orientation { get; }
    //
    // Summary:
    //     Gets the System.Windows.Forms.ToolStripItem that is the overflow button for
    //     a System.Windows.Forms.ToolStrip with overflow enabled.
    //
    // Returns:
    //     An object of type System.Windows.Forms.ToolStripOverflowButton with its System.Windows.Forms.ToolStripItemAlignment
    //     set to System.Windows.Forms.ToolStripItemAlignment.Right and its System.Windows.Forms.ToolStripItemOverflow
    //     value set to System.Windows.Forms.ToolStripItemOverflow.Never.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    //[Browsable(false)]
    //public ToolStripOverflowButton OverflowButton { get; }
    //
    // Summary:
    //     Gets or sets a System.Windows.Forms.ToolStripRenderer used to customize the
    //     look and feel of a System.Windows.Forms.ToolStrip.
    //
    // Returns:
    //     A System.Windows.Forms.ToolStripRenderer used to customize the look and feel
    //     of a System.Windows.Forms.ToolStrip.
    //[Browsable(false)]
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //public ToolStripRenderer Renderer { get; set; }
    //
    // Summary:
    //     Gets or sets the painting styles to be applied to the System.Windows.Forms.ToolStrip.
    //
    // Returns:
    //     One of the System.Windows.Forms.ToolStripRenderMode values. The default is
    //     System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode.
    //
    // Exceptions:
    //   System.ComponentModel.InvalidEnumArgumentException:
    //     The value being set is not one of the System.Windows.Forms.ToolStripRenderMode
    //     values.
    //
    //   System.NotSupportedException:
    //     System.Windows.Forms.ToolStripRenderMode is set to System.Windows.Forms.ToolStripRenderMode.Custom
    //     without the System.Windows.Forms.ToolStrip.Renderer property being assigned
    //     to a new instance of System.Windows.Forms.ToolStripRenderer.
    //public ToolStripRenderMode RenderMode { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether ToolTips are to be displayed on System.Windows.Forms.ToolStrip
    //     items.
    //
    // Returns:
    //     true if ToolTips are to be displayed; otherwise, false. The default is true.
    //[DefaultValue(true)]
    //public bool ShowItemToolTips { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether the System.Windows.Forms.ToolStrip
    //     stretches from end to end in the System.Windows.Forms.ToolStripContainer.
    //
    // Returns:
    //     true if the System.Windows.Forms.ToolStrip stretches from end to end in its
    //     System.Windows.Forms.ToolStripContainer; otherwise, false. The default is
    //     false.
    //[DefaultValue(false)]
    //public bool Stretch { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether the user can give the focus to an
    //     item in the System.Windows.Forms.ToolStrip using the TAB key.
    //
    // Returns:
    //     true if the user can give the focus to an item in the System.Windows.Forms.ToolStrip
    //     using the TAB key; otherwise, false. The default is false.
    //[DispId(-516)]
    //[DefaultValue(false)]
    //public bool TabStop { get; set; }
    //
    // Summary:
    //     Gets or sets the direction in which to draw text on a System.Windows.Forms.ToolStrip.
    //
    // Returns:
    //     One of the System.Windows.Forms.ToolStripTextDirection values. The default
    //     is System.Windows.Forms.ToolStripTextDirection.Horizontal.
    //
    // Exceptions:
    //   System.ComponentModel.InvalidEnumArgumentException:
    //     The specified value is not one of the System.Windows.Forms.ToolStripTextDirection
    //     values.
    // public virtual ToolStripTextDirection TextDirection { get; set; }
    //
    // Summary:
    //     This property is not relevant for this class.
    //
    // Returns:
    //     An instance of the System.Windows.Forms.VScrollProperties class, which provides
    //     basic properties for a System.Windows.Forms.VScrollBar.
    //[Browsable(false)]
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //public VScrollProperties VerticalScroll { get; }

    // Summary:
    //     Occurs when the System.Windows.Forms.ToolStrip.AutoSize property has changed.
    //[Browsable(true)]
    //[EditorBrowsable(EditorBrowsableState.Always)]
    // public event EventHandler AutoSizeChanged;
    //
    // Summary:
    //     Occurs when the user begins to drag the System.Windows.Forms.ToolStrip control.
    // public event EventHandler BeginDrag;
    //
    // Summary:
    //     Occurs when the System.Windows.Forms.ToolStrip.CausesValidation property
    //     changes.
    //[Browsable(false)]
    // public event EventHandler CausesValidationChanged;
    //
    // Summary:
    //     This event is not relevant for this class.
    //
    // Returns:
    //     A System.Windows.Forms.ControlEventHandler.
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //[Browsable(false)]
    // public event ControlEventHandler ControlAdded;
    //
    // Summary:
    //     This event is not relevant for this class.
    //
    // Returns:
    //     A System.Windows.Forms.ControlEventHandler.
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //[Browsable(false)]
    // public event ControlEventHandler ControlRemoved;
    //
    // Summary:
    //     Occurs when the value of the System.Windows.Forms.Cursor property changes.
    //[Browsable(false)]
    // public event EventHandler CursorChanged;
    //
    // Summary:
    //     Occurs when the user stops dragging the System.Windows.Forms.ToolStrip control.
    // public event EventHandler EndDrag;
    //
    // Summary:
    //     Occurs when the value of the System.Windows.Forms.ToolStrip.ForeColor property
    //     changes.
    //[Browsable(false)]
    // public event EventHandler ForeColorChanged;
    //
    // Summary:
    //     Occurs when a new System.Windows.Forms.ToolStripItem is added to the System.Windows.Forms.ToolStripItemCollection.
    // public event ToolStripItemEventHandler ItemAdded;
    //
    // Summary:
    //     Occurs when the System.Windows.Forms.ToolStripItem is clicked.
    // public event ToolStripItemClickedEventHandler ItemClicked;
    //
    // Summary:
    //     Occurs when a System.Windows.Forms.ToolStripItem is removed from the System.Windows.Forms.ToolStripItemCollection.
    // public event ToolStripItemEventHandler ItemRemoved;
    //
    // Summary:
    //     Occurs when the layout of the System.Windows.Forms.ToolStrip is complete.
    // public event EventHandler LayoutCompleted;
    //
    // Summary:
    //     Occurs when the value of the System.Windows.Forms.ToolStrip.LayoutStyle property
    //     changes.
    // public event EventHandler LayoutStyleChanged;
    //
    // Summary:
    //     Occurs when the System.Windows.Forms.ToolStrip move handle is painted.
    // public event PaintEventHandler PaintGrip;
    //
    // Summary:
    //     Occurs when the value of the System.Windows.Forms.ToolStrip.Renderer property
    //     changes.
    // public event EventHandler RendererChanged;

    // Summary:
    //     Creates a new accessibility object for the System.Windows.Forms.ToolStrip
    //     item.
    //
    // Returns:
    //     A new System.Windows.Forms.AccessibleObject for the System.Windows.Forms.ToolStrip
    //     item.
    // protected override AccessibleObject CreateAccessibilityInstance();
    //
    // protected override Control.ControlCollection CreateControlsInstance();
    //
    // Summary:
    //     Creates a default System.Windows.Forms.ToolStripItem with the specified text,
    //     image, and event handler on a new System.Windows.Forms.ToolStrip instance.
    //
    // Parameters:
    //   text:
    //     The text to use for the System.Windows.Forms.ToolStripItem. If the text parameter
    //     is a hyphen (-), this method creates a System.Windows.Forms.ToolStripSeparator.
    //
    //   image:
    //     The System.Drawing.Image to display on the System.Windows.Forms.ToolStripItem.
    //
    //   onClick:
    //     An event handler that raises the System.Windows.Forms.Control.Click event
    //     when the System.Windows.Forms.ToolStripItem is clicked.
    //
    // Returns:
    //     A System.Windows.Forms.ToolStripButton.#ctor(System.String,System.Drawing.Image,System.EventHandler),
    //     or a System.Windows.Forms.ToolStripSeparator if the text parameter is a hyphen
    //     (-).
    // protected internal virtual ToolStripItem CreateDefaultItem(string text, System.Drawing.Image image, EventHandler onClick);
    //
    // Summary:
    //     Specifies the visual arrangement for the System.Windows.Forms.ToolStrip.
    //
    // Parameters:
    //   layoutStyle:
    //     The visual arrangement to be applied to the System.Windows.Forms.ToolStrip.
    //
    // Returns:
    //     One of the System.Windows.Forms.ToolStripLayoutStyle values. The default
    //     is null.
    // protected virtual LayoutSettings CreateLayoutSettings(ToolStripLayoutStyle layoutStyle);
    //
    // Summary:
    //     Releases the unmanaged resources used by the System.Windows.Forms.ToolStrip
    //     and optionally releases the managed resources.
    //
    // Parameters:
    //   disposing:
    //     true to release both managed and unmanaged resources; false to release only
    //     unmanaged resources.
    // protected override void Dispose(bool disposing);
    //
    // Summary:
    //     This method is not relevant for this class.
    //
    // Parameters:
    //   point:
    //     A System.Drawing.Point.
    //
    // Returns:
    //     A System.Windows.Forms.Control.
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //public Control GetChildAtPoint(System.Drawing.Point point);
    //
    // Summary:
    //     This method is not relevant for this class.
    //
    // Parameters:
    //   pt:
    //     A System.Drawing.Point value.
    //
    //   skipValue:
    //     A System.Windows.Forms.GetChildAtPointSkip value.
    //
    // Returns:
    //     A System.Windows.Forms.Control.
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //public Control GetChildAtPoint(System.Drawing.Point pt, GetChildAtPointSkip skipValue);
    //
    // Summary:
    //     Returns the item located at the specified point in the client area of the
    //     System.Windows.Forms.ToolStrip.
    //
    // Parameters:
    //   point:
    //     The System.Drawing.Point at which to search for the System.Windows.Forms.ToolStripItem.
    //
    // Returns:
    //     The System.Windows.Forms.ToolStripItem at the specified location, or null
    //     if the System.Windows.Forms.ToolStripItem is not found.
    //public ToolStripItem GetItemAt(System.Drawing.Point point);
    //
    // Summary:
    //     Returns the item located at the specified x- and y-coordinates of the System.Windows.Forms.ToolStrip
    //     client area.
    //
    // Parameters:
    //   x:
    //     The horizontal coordinate, in pixels, from the left edge of the client area.
    //
    //   y:
    //     The vertical coordinate, in pixels, from the top edge of the client area.
    //
    // Returns:
    //     The System.Windows.Forms.ToolStripItem located at the specified location,
    //     or null if the System.Windows.Forms.ToolStripItem is not found.
    //public ToolStripItem GetItemAt(int x, int y);
    //
    // Summary:
    //     Retrieves the next System.Windows.Forms.ToolStripItem from the specified
    //     reference point and moving in the specified direction.
    //
    // Parameters:
    //   start:
    //     The System.Windows.Forms.ToolStripItem that is the reference point from which
    //     to begin the retrieval of the next item.
    //
    //   direction:
    //     One of the values of System.Windows.Forms.ArrowDirection that specifies the
    //     direction to move.
    //
    // Returns:
    //     A System.Windows.Forms.ToolStripItem that is specified by the start parameter
    //     and is next in the order as specified by the direction parameter.
    //
    // Exceptions:
    //   System.ComponentModel.InvalidEnumArgumentException:
    //     The specified value of the direction parameter is not one of the values of
    //     System.Windows.Forms.ArrowDirection.
    // public virtual ToolStripItem GetNextItem(ToolStripItem start, ArrowDirection direction);
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
    // protected override bool IsInputChar(char charCode);
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
    // Summary:
    //     Raises the System.Windows.Forms.ToolStrip.BeginDrag event.
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    // protected virtual void OnBeginDrag(EventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.Control.DockChanged event.
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    // protected override void OnDockChanged(EventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.Control.Enabled event.
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    // protected override void OnEnabledChanged(EventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.ToolStrip.EndDrag event.
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    // protected virtual void OnEndDrag(EventArgs e);
    //
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    // protected override void OnFontChanged(EventArgs e);
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
    //
    // Parameters:
    //   e:
    //     An System.Windows.Forms.InvalidateEventArgs that contains the event data.
    // protected override void OnInvalidated(InvalidateEventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.ToolStrip.ItemAdded event.
    //
    // Parameters:
    //   e:
    //     A System.Windows.Forms.ToolStripItemEventArgs that contains the event data.
    // protected internal virtual void OnItemAdded(ToolStripItemEventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.ToolStrip.ItemClicked event.
    //
    // Parameters:
    //   e:
    //     A System.Windows.Forms.ToolStripItemClickedEventArgs that contains the event
    //     data.
    // protected virtual void OnItemClicked(ToolStripItemClickedEventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.ToolStrip.ItemRemoved event.
    //
    // Parameters:
    //   e:
    //     A System.Windows.Forms.ToolStripItemEventArgs that contains the event data.
    // protected internal virtual void OnItemRemoved(ToolStripItemEventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.Control.Layout event.
    //
    // Parameters:
    //   e:
    //     A System.Windows.Forms.LayoutEventArgs that contains the event data.
    // protected override void OnLayout(LayoutEventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.ToolStrip.LayoutCompleted event.
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    // protected virtual void OnLayoutCompleted(EventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.ToolStrip.LayoutStyleChanged event.
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    // protected virtual void OnLayoutStyleChanged(EventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.Control.Leave event.
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    // protected override void OnLeave(EventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.Control.LostFocus event.
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    // protected override void OnLostFocus(EventArgs e);
    //
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    // protected override void OnMouseCaptureChanged(EventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.Control.MouseDown event.
    //
    // Parameters:
    //   mea:
    //     A System.Windows.Forms.MouseEventArgs that contains the event data.
    // protected override void OnMouseDown(MouseEventArgs mea);
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
    //     Raises the System.Windows.Forms.Control.MouseMove event.
    //
    // Parameters:
    //   mea:
    //     A System.Windows.Forms.MouseEventArgs that contains the event data.
    // protected override void OnMouseMove(MouseEventArgs mea);
    //
    // Summary:
    //     Raises the System.Windows.Forms.Control.MouseUp event.
    //
    // Parameters:
    //   mea:
    //     A System.Windows.Forms.MouseEventArgs that contains the event data.
    // protected override void OnMouseUp(MouseEventArgs mea);
    //
    // Summary:
    //     Raises the System.Windows.Forms.Control.Paint event.
    //
    // Parameters:
    //   e:
    //     A System.Windows.Forms.PaintEventArgs that contains the event data.
    // protected override void OnPaint(PaintEventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.Control.Paint event for the System.Windows.Forms.ToolStrip
    //     background.
    //
    // Parameters:
    //   e:
    //     A System.Windows.Forms.PaintEventArgs that contains information about the
    //     control to paint.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    // protected override void OnPaintBackground(PaintEventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.ToolStrip.PaintGrip event.
    //
    // Parameters:
    //   e:
    //     A System.Windows.Forms.PaintEventArgs that contains the event data.
    // protected internal virtual void OnPaintGrip(PaintEventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.ToolStrip.RendererChanged event.
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    // protected virtual void OnRendererChanged(EventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.Control.RightToLeftChanged event.
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    // protected override void OnRightToLeftChanged(EventArgs e);
    //
    //
    // Parameters:
    //   se:
    //     A System.Windows.Forms.ScrollEventArgs that contains the event data.
    // protected override void OnScroll(ScrollEventArgs se);
    //
    // Summary:
    //     Raises the System.Windows.Forms.Control.TabStopChanged event.
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    // protected override void OnTabStopChanged(EventArgs e);
    //
    // Summary:
    //     Raises the System.Windows.Forms.ToolStripItem.VisibleChanged event.
    //
    // Parameters:
    //   e:
    //     An System.EventArgs that contains the event data.
    // protected override void OnVisibleChanged(EventArgs e);
    //
    //
    // Parameters:
    //   msg:
    //     A System.Windows.Forms.Message, passed by reference, that represents the
    //     window message to process.
    //
    //   keyData:
    //     One of the System.Windows.Forms.Keys values that represents the key to process.
    // protected override bool ProcessCmdKey(ref Message m, Keys keyData);
    //
    // Summary:
    //     Processes a dialog box key.
    //
    // Parameters:
    //   keyData:
    //     One of the System.Windows.Forms.Keys values that represents the key to process.
    //
    // Returns:
    //     true if the key was processed by the control; otherwise, false.
    // protected override bool ProcessDialogKey(Keys keyData);
    //
    // Summary:
    //     Processes a mnemonic character.
    //
    // Parameters:
    //   charCode:
    //     The character to process.
    //
    // Returns:
    //     true if the character was processed as a mnemonic by the control; otherwise,
    //     false.
    // protected internal override bool ProcessMnemonic(char charCode);
    //
    // Summary:
    //     This method is not relevant for this class.
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //public void ResetMinimumSize();
    //
    // Summary:
    //     Controls the return location of the focus.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    // protected virtual void RestoreFocus();
    //
    // Summary:
    //     Activates a child control. Optionally specifies the direction in the tab
    //     order to select the control from.
    //
    // Parameters:
    //   directed:
    //     true to specify the direction of the control to select; otherwise, false.
    //
    //   forward:
    //     true to move forward in the tab order; false to move backward in the tab
    //     order.
    // protected override void Select(bool directed, bool forward);
    //
    // Summary:
    //     This method is not relevant for this class.
    //
    // Parameters:
    //   x:
    //     An System.Int32.
    //
    //   y:
    //     An System.Int32.
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //public void SetAutoScrollMargin(int x, int y);
    //
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
    // protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified);
    //
    // Summary:
    //     Resets the collection of displayed and overflow items after a layout is done.
    // protected virtual void SetDisplayedItems();
    //
    // Summary:
    //     Anchors a System.Windows.Forms.ToolStripItem to a particular place on a System.Windows.Forms.ToolStrip.
    //
    // Parameters:
    //   item:
    //     The System.Windows.Forms.ToolStripItem to anchor.
    //
    //   location:
    //     A System.Drawing.Point representing the x and y client coordinates of the
    //     System.Windows.Forms.ToolStripItem location, in pixels.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The value of the item parameter is null.
    //
    //   System.NotSupportedException:
    //     The current System.Windows.Forms.ToolStrip is not the owner of the System.Windows.Forms.ToolStripItem
    //     referred to by the item parameter.
    // protected internal void SetItemLocation(ToolStripItem item, System.Drawing.Point location);
    //
    // Summary:
    //     Enables you to change the parent System.Windows.Forms.ToolStrip of a System.Windows.Forms.ToolStripItem.
    //
    // Parameters:
    //   item:
    //     The System.Windows.Forms.ToolStripItem whose System.Windows.Forms.Control.Parent
    //     property is to be changed.
    //
    //   parent:
    //     The System.Windows.Forms.ToolStrip that is the parent of the System.Windows.Forms.ToolStripItem
    //     referred to by the item parameter.
    // protected static void SetItemParent(ToolStripItem item, ToolStrip parent);
    //
    // Summary:
    //     Retrieves a value that sets the System.Windows.Forms.ToolStripItem to the
    //     specified visibility state.
    //
    // Parameters:
    //   visible:
    //     true if the System.Windows.Forms.ToolStripItem is visible; otherwise, false.
    // protected override void SetVisibleCore(bool visible);
    //
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
    //     Provides information that accessibility applications use to adjust the user
    //     interface of a System.Windows.Forms.ToolStrip for users with impairments.
    //[ComVisible(true)]
    public class ToolStripAccessibleObject 
    {
      // Summary:
      //     Initializes a new instance of the System.Windows.Forms.ToolStrip.ToolStripAccessibleObject
      //     class.
      //
      // Parameters:
      //   owner:
      //     The System.Windows.Forms.ToolStrip that owns this System.Windows.Forms.ToolStrip.ToolStripAccessibleObject.
      public ToolStripAccessibleObject(ToolStrip owner) { }

      // public override AccessibleRole Role { get; }

      //
      // Parameters:
      //   index:
      //     The zero-based index of the accessible child.
      //
      // Returns:
      //     An System.Windows.Forms.AccessibleObject that represents the accessible child
      //     corresponding to the specified index.
      // public override AccessibleObject GetChild(int index);
      //
      //
      // Returns:
      //     The number of children belonging to an accessible object.
      // public override int GetChildCount();
      //
      //
      // Parameters:
      //   x:
      //     The horizontal screen coordinate.
      //
      //   y:
      //     The vertical screen coordinate.
      //
      // Returns:
      //     An System.Windows.Forms.AccessibleObject that represents the child object
      //     at the given screen coordinates. This method returns the calling object if
      //     the object itself is at the location specified. Returns null if no object
      //     is at the tested location.
      // public override AccessibleObject HitTest(int x, int y);
    }
  }
}
