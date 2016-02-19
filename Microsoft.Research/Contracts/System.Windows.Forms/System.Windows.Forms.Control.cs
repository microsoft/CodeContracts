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

using System.Drawing;
using System.Diagnostics.Contracts;
using System.Windows.Forms.Layout;
using System.ComponentModel;

namespace System.Windows.Forms
{
  // Summary:
  //     Defines the base class for controls, which are components with visual representation.
  public class Control
  {
    // Summary:
    //     Initializes a new instance of the System.Windows.Forms.Control class with
    //     default settings.
    public Control()
    { }
    //
    // Summary:
    //     Initializes a new instance of the System.Windows.Forms.Control class with
    //     specific text.
    //
    // Parameters:
    //   text:
    //     The text displayed by the control.
    public Control(string text) { }
    //
    // Summary:
    //     Initializes a new instance of the System.Windows.Forms.Control class as a
    //     child control, with specific text.
    //
    // Parameters:
    //   parent:
    //     The System.Windows.Forms.Control to be the parent of the control.
    //
    //   text:
    //     The text displayed by the control.
    public Control(Control parent, string text) { }
    //
    // Summary:
    //     Initializes a new instance of the System.Windows.Forms.Control class with
    //     specific text, size, and location.
    //
    // Parameters:
    //   text:
    //     The text displayed by the control.
    //
    //   left:
    //     The System.Drawing.Point.X position of the control, in pixels, from the left
    //     edge of the control's container. The value is assigned to the System.Windows.Forms.Control.Left
    //     property.
    //
    //   top:
    //     The System.Drawing.Point.Y position of the control, in pixels, from the top
    //     edge of the control's container. The value is assigned to the System.Windows.Forms.Control.Top
    //     property.
    //
    //   width:
    //     The width of the control, in pixels. The value is assigned to the System.Windows.Forms.Control.Width
    //     property.
    //
    //   height:
    //     The height of the control, in pixels. The value is assigned to the System.Windows.Forms.Control.Height
    //     property.
    public Control(string text, int left, int top, int width, int height) { }
    //
    // Summary:
    //     Initializes a new instance of the System.Windows.Forms.Control class as a
    //     child control, with specific text, size, and location.
    //
    // Parameters:
    //   parent:
    //     The System.Windows.Forms.Control to be the parent of the control.
    //
    //   text:
    //     The text displayed by the control.
    //
    //   left:
    //     The System.Drawing.Point.X position of the control, in pixels, from the left
    //     edge of the control's container. The value is assigned to the System.Windows.Forms.Control.Left
    //     property.
    //
    //   top:
    //     The System.Drawing.Point.Y position of the control, in pixels, from the top
    //     edge of the control's container. The value is assigned to the System.Windows.Forms.Control.Top
    //     property.
    //
    //   width:
    //     The width of the control, in pixels. The value is assigned to the System.Windows.Forms.Control.Width
    //     property.
    //
    //   height:
    //     The height of the control, in pixels. The value is assigned to the System.Windows.Forms.Control.Height
    //     property.
    public Control(Control parent, string text, int left, int top, int width, int height) { }

    // Summary:
    //     Gets the System.Windows.Forms.AccessibleObject assigned to the control.
    //
    // Returns:
    //     The System.Windows.Forms.AccessibleObject assigned to the control.

    //public AccessibleObject AccessibilityObject { get; }
    //
    // Summary:
    //     Gets or sets the default action description of the control for use by accessibility
    //     client applications.
    //
    // Returns:
    //     The default action description of the control for use by accessibility client
    //     applications.
    //public string AccessibleDefaultActionDescription { get; set; }
    //
    // Summary:
    //     Gets or sets the description of the control used by accessibility client
    //     applications.
    //
    // Returns:
    //     The description of the control used by accessibility client applications.
    //     The default is null.

    //public string AccessibleDescription { get; set; }
    //
    // Summary:
    //     Gets or sets the name of the control used by accessibility client applications.
    //
    // Returns:
    //     The name of the control used by accessibility client applications. The default
    //     is null.

    //public string AccessibleName { get; set; }
    //
    // Summary:
    //     Gets or sets the accessible role of the control
    //
    // Returns:
    //     One of the values of System.Windows.Forms.AccessibleRole. The default is
    //     Default.
    //
    // Exceptions:
    //   System.ComponentModel.InvalidEnumArgumentException:
    //     The value assigned is not one of the System.Windows.Forms.AccessibleRole
    //     values.
    //public AccessibleRole AccessibleRole { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether the control can accept data that
    //     the user drags onto it.
    //
    // Returns:
    //     true if drag-and-drop operations are allowed in the control; otherwise, false.
    //     The default is false.

    //public virtual bool AllowDrop { get; set; }
    ////
    //// Summary:
    ////     Gets or sets the edges of the container to which a control is bound and determines
    ////     how a control is resized with its parent.
    ////
    //// Returns:
    ////     A bitwise combination of the System.Windows.Forms.AnchorStyles values. The
    ////     default is Top and Left.
    //[RefreshProperties(RefreshProperties.Repaint)]
    //[Localizable(true)]
    //public virtual AnchorStyles Anchor { get; set; }
    ////
    //// Summary:
    ////     Gets or sets where this control is scrolled to in System.Windows.Forms.ScrollableControl.ScrollControlIntoView(System.Windows.Forms.Control).
    ////
    //// Returns:
    ////     A System.Drawing.Point specifying the scroll location. The default is the
    ////     upper-left corner of the control.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    //[Browsable(false)]
    //public virtual System.Drawing.Point AutoScrollOffset { get; set; }
    ////
    //// Summary:
    ////     This property is not relevant for this class.
    ////
    //// Returns:
    ////     true if enabled; otherwise, false.
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //[RefreshProperties(RefreshProperties.All)]
    //[DefaultValue(false)]
    //[Browsable(false)]
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //[Localizable(true)]
    //public virtual bool AutoSize { get; set; }
    ////
    //// Summary:
    ////     Gets or sets the background color for the control.
    ////
    //// Returns:
    ////     A System.Drawing.Color that represents the background color of the control.
    ////     The default is the value of the System.Windows.Forms.Control.DefaultBackColor
    ////     property.
    //[DispId(-501)]
    //public virtual System.Drawing.Color BackColor { get; set; }
    ////
    //// Summary:
    ////     Gets or sets the background image displayed in the control.
    ////
    //// Returns:
    ////     An System.Drawing.Image that represents the image to display in the background
    ////     of the control.
    //[DefaultValue("")]
    //[Localizable(true)]
    //public virtual System.Drawing.Image BackgroundImage { get; set; }
    ////
    //// Summary:
    ////     Gets or sets the background image layout as defined in the System.Windows.Forms.ImageLayout
    ////     enumeration.
    ////
    //// Returns:
    ////     One of the values of System.Windows.Forms.ImageLayout (System.Windows.Forms.ImageLayout.Center
    ////     , System.Windows.Forms.ImageLayout.None, System.Windows.Forms.ImageLayout.Stretch,
    ////     System.Windows.Forms.ImageLayout.Tile, or System.Windows.Forms.ImageLayout.Zoom).
    ////     System.Windows.Forms.ImageLayout.Tile is the default value.
    ////
    //// Exceptions:
    ////   System.ComponentModel.InvalidEnumArgumentException:
    ////     The specified enumeration value does not exist.
    //[Localizable(true)]
    //public virtual ImageLayout BackgroundImageLayout { get; set; }
    ////
    //// Summary:
    ////     Gets or sets the System.Windows.Forms.BindingContext for the control.
    ////
    //// Returns:
    ////     A System.Windows.Forms.BindingContext for the control.
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    //[Browsable(false)]
    //public virtual BindingContext BindingContext { get; set; }
    //
    // Summary:
    //     Gets the distance, in pixels, between the bottom edge of the control and
    //     the top edge of its container's client area.
    //
    // Returns:
    //     An System.Int32 representing the distance, in pixels, between the bottom
    //     edge of the control and the top edge of its container's client area.
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //[Browsable(false)]
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    //public int Bottom { get; }
    //
    // Summary:
    //     Gets or sets the size and location of the control including its nonclient
    //     elements, in pixels, relative to the parent control.
    //
    // Returns:
    //     A System.Drawing.Rectangle in pixels relative to the parent control that
    //     represents the size and location of the control including its nonclient elements.
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    //[Browsable(false)]
    //public System.Drawing.Rectangle Bounds { get; set; }
    //
    // Summary:
    //     Gets a value indicating whether the System.Windows.Forms.Control.ImeMode
    //     property can be set to an active value, to enable IME support.
    //
    // Returns:
    //     true in all cases.
    //// protected virtualbool CanEnableIme { get; }
    ////
    //// Summary:
    ////     Gets a value indicating whether the control can receive focus.
    ////
    //// Returns:
    ////     true if the control can receive focus; otherwise, false.
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    //[Browsable(false)]
    //public bool CanFocus { get; }
    ////
    //// Summary:
    ////     Determines if events can be raised on the control.
    ////
    //// Returns:
    ////     true if the control is hosted as an ActiveX control whose events are not
    ////     frozen; otherwise, false.
    //protected override bool CanRaiseEvents { get; }
    //
    // Summary:
    //     Gets a value indicating whether the control can be selected.
    //
    // Returns:
    //     true if the control can be selected; otherwise, false.
    //[Browsable(false)]
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //public bool CanSelect { get; }
    ////
    //// Summary:
    ////     Gets or sets a value indicating whether the control has captured the mouse.
    ////
    //// Returns:
    ////     true if the control has captured the mouse; otherwise, false.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //[Browsable(false)]
    //public bool Capture { get; set; }
    ////
    //// Summary:
    ////     Gets or sets a value indicating whether the control causes validation to
    ////     be performed on any controls that require validation when it receives focus.
    ////
    //// Returns:
    ////     true if the control causes validation to be performed on any controls requiring
    ////     validation when it receives focus; otherwise, false. The default is true.
    //[DefaultValue(true)]
    //public bool CausesValidation { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether to catch calls on the wrong thread
    //     that access a control's System.Windows.Forms.Control.Handle property when
    //     an application is being debugged.
    //
    // Returns:
    //     true if calls on the wrong thread are caught; otherwise, false.
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //[Browsable(false)]
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    //public static bool CheckForIllegalCrossThreadCalls { get; set; }
    ////
    //// Summary:
    ////     Gets the rectangle that represents the client area of the control.
    ////
    //// Returns:
    ////     A System.Drawing.Rectangle that represents the client area of the control.
    //[Browsable(false)]
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //public System.Drawing.Rectangle ClientRectangle { get; }
    ////
    //// Summary:
    ////     Gets or sets the height and width of the client area of the control.
    ////
    //// Returns:
    ////     A System.Drawing.Size that represents the dimensions of the client area of
    ////     the control.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //[Browsable(false)]
    //public System.Drawing.Size ClientSize { get; set; }
    ////
    //// Summary:
    ////     Gets the name of the company or creator of the application containing the
    ////     control.
    ////
    //// Returns:
    ////     The company name or creator of the application containing the control.
    //[Browsable(false)]
    //[Description("ControlCompanyNameDescr")]
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //public string CompanyName { get; }
    ////
    //// Summary:
    ////     Gets a value indicating whether the control, or one of its child controls,
    ////     currently has the input focus.
    ////
    //// Returns:
    ////     true if the control or one of its child controls currently has the input
    ////     focus; otherwise, false.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //[Browsable(false)]
    //public bool ContainsFocus { get; }
    ////
    //// Summary:
    ////     Gets or sets the shortcut menu associated with the control.
    ////
    //// Returns:
    ////     A System.Windows.Forms.ContextMenu that represents the shortcut menu associated
    ////     with the control.
    //[DefaultValue("")]
    //[Browsable(false)]
    //public virtual ContextMenu ContextMenu { get; set; }
    ////
    //// Summary:
    ////     Gets or sets the System.Windows.Forms.ContextMenuStrip associated with this
    ////     control.
    ////
    //// Returns:
    ////     The System.Windows.Forms.ContextMenuStrip for this control, or null if there
    ////     is no System.Windows.Forms.ContextMenuStrip. The default is null.
    //[DefaultValue("")]
    //public virtual ContextMenuStrip ContextMenuStrip { get; set; }
    ////
    //// Summary:
    ////     Gets the collection of controls contained within the control.
    ////
    //// Returns:
    ////     A System.Windows.Forms.Control.ControlCollection representing the collection
    ////     of controls contained within the control.
    //[Browsable(false)]
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public Control.ControlCollection Controls
    {
      get
      {
        Contract.Ensures(Contract.Result<Control.ControlCollection>() != null);

        return default(Control.ControlCollection);
      }
    }

    ////
    //// Summary:
    ////     Gets a value indicating whether the control has been created.
    ////
    //// Returns:
    ////     true if the control has been created; otherwise, false.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    //[Browsable(false)]
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //public bool Created { get; }
    ////
    //// Summary:
    ////     Gets the required creation parameters when the control handle is created.
    ////
    //// Returns:
    ////     A System.Windows.Forms.CreateParams that contains the required creation parameters
    ////     when the handle to the control is created.
    //// protected virtualCreateParams CreateParams { get; }
    ////
    //// Summary:
    ////     Gets or sets the cursor that is displayed when the mouse pointer is over
    ////     the control.
    ////
    //// Returns:
    ////     A System.Windows.Forms.Cursor that represents the cursor to display when
    ////     the mouse pointer is over the control.
    //[AmbientValue("")]
    //public virtual Cursor Cursor { get; set; }
    //
    // Summary:
    //     Gets the data bindings for the control.
    //
    // Returns:
    //     A System.Windows.Forms.ControlBindingsCollection that contains the System.Windows.Forms.Binding
    //     objects for the control.
    [ParenthesizePropertyName(true)]
    [RefreshProperties(RefreshProperties.All)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public ControlBindingsCollection DataBindings
	{
		get
		{
				Contract.Ensures(Contract.Result<ControlBindingsCollection>() != null);
				return default(ControlBindingsCollection);
		}
	}
    ////
    //// Summary:
    ////     Gets the default background color of the control.
    ////
    //// Returns:
    ////     The default background System.Drawing.Color of the control. The default is
    ////     System.Drawing.SystemColors.Control.
    //public static System.Drawing.Color DefaultBackColor { get; }
    ////
    //// Summary:
    ////     Gets or sets the default cursor for the control.
    ////
    //// Returns:
    ////     An object of type System.Windows.Forms.Cursor representing the current default
    ////     cursor.
    //// protected virtualCursor DefaultCursor { get; }
    //
    // Summary:
    //     Gets the default font of the control.
    //
    // Returns:
    //     The default System.Drawing.Font of the control. The value returned will vary
    //     depending on the user's operating system the local culture setting of their
    //     system.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The default font or the regional alternative fonts are not installed on the
    //     client computer.
    public static Font DefaultFont
    {
      get
      {
        Contract.Ensures(Contract.Result<Font>() != null);
        return default(Font);
      }
    }
    
    
    ////
    //// Summary:
    ////     Gets the default foreground color of the control.
    ////
    //// Returns:
    ////     The default foreground System.Drawing.Color of the control. The default is
    ////     System.Drawing.SystemColors.ControlText.
    //public static System.Drawing.Color DefaultForeColor { get; }
    ////
    //// Summary:
    ////     Gets the default Input Method Editor (IME) mode supported by the control.
    ////
    //// Returns:
    ////     One of the System.Windows.Forms.ImeMode values.
    //// protected virtualImeMode DefaultImeMode { get; }
    ////
    //// Summary:
    ////     Gets the space, in pixels, that is specified by default between controls.
    ////
    //// Returns:
    ////     A System.Windows.Forms.Padding that represents the default space between
    ////     controls.
    //// protected virtualPadding DefaultMargin { get; }
    ////
    //// Summary:
    ////     Gets the length and height, in pixels, that is specified as the default maximum
    ////     size of a control.
    ////
    //// Returns:
    ////     A System.Drawing.Point.#ctor(System.Drawing.Size) representing the size of
    ////     the control.
    //// protected virtualSystem.Drawing.Size DefaultMaximumSize { get; }
    ////
    //// Summary:
    ////     Gets the length and height, in pixels, that is specified as the default minimum
    ////     size of a control.
    ////
    //// Returns:
    ////     A System.Drawing.Point.#ctor(System.Drawing.Size) representing the size of
    ////     the control.
    //// protected virtualSystem.Drawing.Size DefaultMinimumSize { get; }
    ////
    //// Summary:
    ////     Gets the internal spacing, in pixels, of the contents of a control.
    ////
    //// Returns:
    ////     A System.Windows.Forms.Padding that represents the internal spacing of the
    ////     contents of a control.
    //// protected virtualPadding DefaultPadding { get; }
    ////
    //// Summary:
    ////     Gets the default size of the control.
    ////
    //// Returns:
    ////     The default System.Drawing.Size of the control.
    //// protected virtualSystem.Drawing.Size DefaultSize { get; }
    ////
    //// Summary:
    ////     Gets the rectangle that represents the display area of the control.
    ////
    //// Returns:
    ////     A System.Drawing.Rectangle that represents the display area of the control.
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    //[Browsable(false)]
    //public virtual System.Drawing.Rectangle DisplayRectangle { get; }
    ////
    //// Summary:
    ////     Gets a value indicating whether the base System.Windows.Forms.Control class
    ////     is in the process of disposing.
    ////
    //// Returns:
    ////     true if the base System.Windows.Forms.Control class is in the process of
    ////     disposing; otherwise, false.
    //[Browsable(false)]
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    //public bool Disposing { get; }
    //
    // Summary:
    //     Gets or sets which control borders are docked to its parent control and determines
    //     how a control is resized with its parent.
    //
    // Returns:
    //     One of the System.Windows.Forms.DockStyle values. The default is System.Windows.Forms.DockStyle.None.
    //
    // Exceptions:
    //   System.ComponentModel.InvalidEnumArgumentException:
    //     The value assigned is not one of the System.Windows.Forms.DockStyle values.
    //[RefreshProperties(RefreshProperties.Repaint)]
    //[Localizable(true)]
    //public virtual DockStyle Dock { get; set; }
    ////
    //// Summary:
    ////     Gets or sets a value indicating whether this control should redraw its surface
    ////     using a secondary buffer to reduce or prevent flicker.
    ////
    //// Returns:
    ////     true if the surface of the control should be drawn using double buffering;
    ////     otherwise, false.
    //// protected virtualbool DoubleBuffered { get; set; }
    ////
    //// Summary:
    ////     Gets or sets a value indicating whether the control can respond to user interaction.
    ////
    //// Returns:
    ////     true if the control can respond to user interaction; otherwise, false. The
    ////     default is true.
    //[Localizable(true)]
    //[DispId(-514)]
    //public bool Enabled { get; set; }
    ////
    //// Summary:
    ////     Gets a value indicating whether the control has input focus.
    ////
    //// Returns:
    ////     true if the control has focus; otherwise, false.
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    //[Browsable(false)]
    //public virtual bool Focused { get; }
    ////
    //// Summary:
    ////     Gets or sets the font of the text displayed by the control.
    ////
    //// Returns:
    ////     The System.Drawing.Font to apply to the text displayed by the control. The
    ////     default is the value of the System.Windows.Forms.Control.DefaultFont property.
    //[AmbientValue("")]
    //[Localizable(true)]
    //[DispId(-512)]
    public virtual Font Font
    {
      get
      {
        Contract.Ensures(Contract.Result<Font>() != null);
        return default(Font);
      }
      set
      {
        // value == null seems to be allowed
      }
    }

    ////
    //// Summary:
    ////     Gets or sets the height of the font of the control.
    ////
    //// Returns:
    ////     The height of the System.Drawing.Font of the control in pixels.
    //protected int FontHeight { get; set; }
    ////
    //// Summary:
    ////     Gets or sets the foreground color of the control.
    ////
    //// Returns:
    ////     The foreground System.Drawing.Color of the control. The default is the value
    ////     of the System.Windows.Forms.Control.DefaultForeColor property.
    //[DispId(-513)]
    //public virtual System.Drawing.Color ForeColor { get; set; }
    ////
    //// Summary:
    ////     Gets the window handle that the control is bound to.
    ////
    //// Returns:
    ////     An System.IntPtr that contains the window handle (HWND) of the control.
    //[DispId(-515)]
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //[Browsable(false)]
    //public IntPtr Handle { get; }
    ////
    //// Summary:
    ////     Gets a value indicating whether the control contains one or more child controls.
    ////
    //// Returns:
    ////     true if the control contains one or more child controls; otherwise, false.
    //[Browsable(false)]
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //public bool HasChildren { get; }
    ////
    //// Summary:
    ////     Gets or sets the height of the control.
    ////
    //// Returns:
    ////     The height of the control in pixels.
    //[Browsable(false)]
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //[EditorBrowsable(EditorBrowsableState.Always)]
    //public int Height { get; set; }
    ////
    //// Summary:
    ////     Gets or sets the Input Method Editor (IME) mode of the control.
    ////
    //// Returns:
    ////     One of the System.Windows.Forms.ImeMode values. The default is System.Windows.Forms.ImeMode.Inherit.
    ////
    //// Exceptions:
    ////   System.ComponentModel.InvalidEnumArgumentException:
    ////     The assigned value is not one of the System.Windows.Forms.ImeMode enumeration
    ////     values.
    //[Localizable(true)]
    //public ImeMode ImeMode { get; set; }
    //// protected virtualImeMode ImeModeBase { get; set; }
    ////
    //// Summary:
    ////     Gets a value indicating whether the caller must call an invoke method when
    ////     making method calls to the control because the caller is on a different thread
    ////     than the one the control was created on.
    ////
    //// Returns:
    ////     true if the control's System.Windows.Forms.Control.Handle was created on
    ////     a different thread than the calling thread (indicating that you must make
    ////     calls to the control through an invoke method); otherwise, false.
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //[Browsable(false)]
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    //public bool InvokeRequired { get; }
    ////
    //// Summary:
    ////     Gets or sets a value indicating whether the control is visible to accessibility
    ////     applications.
    ////
    //// Returns:
    ////     true if the control is visible to accessibility applications; otherwise,
    ////     false.
    //[Browsable(false)]
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //public bool IsAccessible { get; set; }
    ////
    //// Summary:
    ////     Gets a value indicating whether the control has been disposed of.
    ////
    //// Returns:
    ////     true if the control has been disposed of; otherwise, false.
    //[Browsable(false)]
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //public bool IsDisposed { get; }
    ////
    //// Summary:
    ////     Gets a value indicating whether the control has a handle associated with
    ////     it.
    ////
    //// Returns:
    ////     true if a handle has been assigned to the control; otherwise, false.
    //[Browsable(false)]
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    //public bool IsHandleCreated { get; }
    ////
    //// Summary:
    ////     Gets a value indicating whether the control is mirrored.
    ////
    //// Returns:
    ////     true if the control is mirrored; otherwise, false.
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    //[Browsable(false)]
    //public bool IsMirrored { get; }
    ////
    //// Summary:
    ////     Gets a cached instance of the control's layout engine.
    ////
    //// Returns:
    ////     The System.Windows.Forms.Layout.LayoutEngine for the control's contents.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    //[Browsable(false)]
    public virtual LayoutEngine LayoutEngine
    {
      get
      {
        Contract.Ensures(Contract.Result<LayoutEngine>() != null);

        return default(LayoutEngine);
      }
    }
    ////
    //// Summary:
    ////     Gets or sets the distance, in pixels, between the left edge of the control
    ////     and the left edge of its container's client area.
    ////
    //// Returns:
    ////     An System.Int32 representing the distance, in pixels, between the left edge
    ////     of the control and the left edge of its container's client area.
    //[EditorBrowsable(EditorBrowsableState.Always)]
    //[Browsable(false)]
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //public int Left { get; set; }
    //
    // Summary:
    //     Gets or sets the coordinates of the upper-left corner of the control relative
    //     to the upper-left corner of its container.
    //
    // Returns:
    //     The System.Drawing.Point that represents the upper-left corner of the control
    //     relative to the upper-left corner of its container.
    //[Localizable(true)]
    //public System.Drawing.Point Location { get; set; }
    ////
    //// Summary:
    ////     Gets or sets the space between controls.
    ////
    //// Returns:
    ////     A System.Windows.Forms.Padding representing the space between controls.
    //[Localizable(true)]
    //public Padding Margin { get; set; }
    ////
    //// Summary:
    ////     Gets or sets the size that is the upper limit that System.Windows.Forms.Control.GetPreferredSize(System.Drawing.Size)
    ////     can specify.
    ////
    //// Returns:
    ////     An ordered pair of type System.Drawing.Size representing the width and height
    ////     of a rectangle.
    //public virtual System.Drawing.Size MaximumSize { get; set; }
    ////
    //// Summary:
    ////     Gets or sets the size that is the lower limit that System.Windows.Forms.Control.GetPreferredSize(System.Drawing.Size)
    ////     can specify.
    ////
    //// Returns:
    ////     An ordered pair of type System.Drawing.Size representing the width and height
    ////     of a rectangle.
    //public virtual System.Drawing.Size MinimumSize { get; set; }
    ////
    //// Summary:
    ////     Gets a value indicating which of the modifier keys (SHIFT, CTRL, and ALT)
    ////     is in a pressed state.
    ////
    //// Returns:
    ////     A bitwise combination of the System.Windows.Forms.Keys values. The default
    ////     is System.Windows.Forms.Keys.None.
    //public static Keys ModifierKeys { get; }
    ////
    //// Summary:
    ////     Gets a value indicating which of the mouse buttons is in a pressed state.
    ////
    //// Returns:
    ////     A bitwise combination of the System.Windows.Forms.MouseButtons enumeration
    ////     values. The default is System.Windows.Forms.MouseButtons.None.
    //public static MouseButtons MouseButtons { get; }
    ////
    //// Summary:
    ////     Gets the position of the mouse cursor in screen coordinates.
    ////
    //// Returns:
    ////     A System.Drawing.Point that contains the coordinates of the mouse cursor
    ////     relative to the upper-left corner of the screen.
    //public static System.Drawing.Point MousePosition { get; }
    ////
    //// Summary:
    ////     Gets or sets the name of the control.
    ////
    //// Returns:
    ////     The name of the control. The default is an empty string ("").
    //[Browsable(false)]
    //public string Name { get; set; }
    ////
    //// Summary:
    ////     Gets or sets padding within the control.
    ////
    //// Returns:
    ////     A System.Windows.Forms.Padding representing the control's internal spacing
    ////     characteristics.
    //[Localizable(true)]
    //public Padding Padding { get; set; }
    ////
    //// Summary:
    ////     Gets or sets the parent container of the control.
    ////
    //// Returns:
    ////     A System.Windows.Forms.Control that represents the parent or container control
    ////     of the control.
    //[Browsable(false)]
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //public Control Parent { get; set; }
    ////
    //// Summary:
    ////     Gets the size of a rectangular area into which the control can fit.
    ////
    //// Returns:
    ////     A System.Drawing.Size containing the height and width, in pixels.
    //[Browsable(false)]
    //public System.Drawing.Size PreferredSize { get; }
    ////
    //// Summary:
    ////     Gets the product name of the assembly containing the control.
    ////
    //// Returns:
    ////     The product name of the assembly containing the control.
    //[Browsable(false)]
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    //public string ProductName { get; }
    ////
    //// Summary:
    ////     Gets the version of the assembly containing the control.
    ////
    //// Returns:
    ////     The file version of the assembly containing the control.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    //[Browsable(false)]
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //public string ProductVersion { get; }
    //protected static ImeMode PropagatingImeMode { get; }
    ////
    //// Summary:
    ////     Gets a value indicating whether the control is currently re-creating its
    ////     handle.
    ////
    //// Returns:
    ////     true if the control is currently re-creating its handle; otherwise, false.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    //[Browsable(false)]
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //public bool RecreatingHandle { get; }
    ////
    //// Summary:
    ////     Gets or sets the window region associated with the control.
    ////
    //// Returns:
    ////     The window System.Drawing.Region associated with the control.
    //[Browsable(false)]
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //public System.Drawing.Region Region { get; set; }
    ////
    //// Summary:
    ////     This property is now obsolete.
    ////
    //// Returns:
    ////     true if the control is rendered from right to left; otherwise, false. The
    ////     default is false.
    //[Obsolete("This property has been deprecated. Please use RightToLeft instead. http://go.microsoft.com/fwlink/?linkid=14202")]
    //protected internal bool RenderRightToLeft { get; }
    ////
    //// Summary:
    ////     Gets or sets a value indicating whether the control redraws itself when resized.
    ////
    //// Returns:
    ////     true if the control redraws itself when resized; otherwise, false.
    //protected bool ResizeRedraw { get; set; }
    ////
    //// Summary:
    ////     Gets the distance, in pixels, between the right edge of the control and the
    ////     left edge of its container's client area.
    ////
    //// Returns:
    ////     An System.Int32 representing the distance, in pixels, between the right edge
    ////     of the control and the left edge of its container's client area.
    //[Browsable(false)]
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //public int Right { get; }
    ////
    //// Summary:
    ////     Gets or sets a value indicating whether control's elements are aligned to
    ////     support locales using right-to-left fonts.
    ////
    //// Returns:
    ////     One of the System.Windows.Forms.RightToLeft values. The default is System.Windows.Forms.RightToLeft.Inherit.
    ////
    //// Exceptions:
    ////   System.ComponentModel.InvalidEnumArgumentException:
    ////     The assigned value is not one of the System.Windows.Forms.RightToLeft values.
    //[Localizable(true)]
    //public virtual RightToLeft RightToLeft { get; set; }
    ////
    //// Summary:
    ////     Gets a value that determines the scaling of child controls.
    ////
    //// Returns:
    ////     true if child controls will be scaled when the System.Windows.Forms.Control.Scale(System.Single)
    ////     method on this control is called; otherwise, false. The default is true.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    //// protected virtualbool ScaleChildren { get; }
    ////
    //// Summary:
    ////     Gets a value indicating whether the control should display focus rectangles.
    ////
    //// Returns:
    ////     true if the control should display focus rectangles; otherwise, false.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    //[Browsable(false)]
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //protected internal virtual bool ShowFocusCues { get; }
    //
    // Summary:
    //     Gets a value indicating whether the user interface is in the appropriate
    //     state to show or hide keyboard accelerators.
    //
    // Returns:
    //     true if the keyboard accelerators are visible; otherwise, false.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //[Browsable(false)]
    //protected internal virtual bool ShowKeyboardCues { get; }
    ////
    //// Summary:
    ////     Gets or sets the site of the control.
    ////
    //// Returns:
    ////     The System.ComponentModel.ISite associated with the System.Windows.Forms.Control,
    ////     if any.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    //public override ISite Site { get; set; }
    ////
    //// Summary:
    ////     Gets or sets the height and width of the control.
    ////
    //// Returns:
    ////     The System.Drawing.Size that represents the height and width of the control
    ////     in pixels.
    //[Localizable(true)]
    //public System.Drawing.Size Size { get; set; }
    ////
    //// Summary:
    ////     Gets or sets the tab order of the control within its container.
    ////
    //// Returns:
    ////     The index value of the control within the set of controls within its container.
    ////     The controls in the container are included in the tab order.
    //[Localizable(true)]
    //[MergableProperty(false)]
    //public int TabIndex { get; set; }
    ////
    //// Summary:
    ////     Gets or sets a value indicating whether the user can give the focus to this
    ////     control using the TAB key.
    ////
    //// Returns:
    ////     true if the user can give the focus to the control using the TAB key; otherwise,
    ////     false. The default is true.This property will always return true for an instance
    ////     of the System.Windows.Forms.Form class.
    //[DispId(-516)]
    //[DefaultValue(true)]
    //public bool TabStop { get; set; }
    ////
    //// Summary:
    ////     Gets or sets the object that contains data about the control.
    ////
    //// Returns:
    ////     An System.Object that contains data about the control. The default is null.
    //[Bindable(true)]
    //[DefaultValue("")]
    //[Localizable(false)]
    //[TypeConverter(typeof(StringConverter))]
    //public object Tag { get; set; }
    ////
    //// Summary:
    ////     Gets or sets the text associated with this control.
    ////
    //// Returns:
    ////     The text associated with this control.
    //[Bindable(true)]
    //[Localizable(true)]
    //[DispId(-517)]
    //public virtual string Text { get; set; }
    ////
    //// Summary:
    ////     Gets or sets the distance, in pixels, between the top edge of the control
    ////     and the top edge of its container's client area.
    ////
    //// Returns:
    ////     An System.Int32 representing the distance, in pixels, between the bottom
    ////     edge of the control and the top edge of its container's client area.
    //[Browsable(false)]
    //[EditorBrowsable(EditorBrowsableState.Always)]
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //public int Top { get; set; }
    ////
    //// Summary:
    ////     Gets the parent control that is not parented by another Windows Forms control.
    ////     Typically, this is the outermost System.Windows.Forms.Form that the control
    ////     is contained in.
    ////
    //// Returns:
    ////     The System.Windows.Forms.Control that represents the top-level control that
    ////     contains the current control.
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    //[Browsable(false)]
    //public Control TopLevelControl { get; }
    ////
    //// Summary:
    ////     Gets or sets a value indicating whether to use the wait cursor for the current
    ////     control and all child controls.
    ////
    //// Returns:
    ////     true to use the wait cursor for the current control and all child controls;
    ////     otherwise, false. The default is false.
    //[Browsable(true)]
    //[DefaultValue(false)]
    //[EditorBrowsable(EditorBrowsableState.Always)]
    //public bool UseWaitCursor { get; set; }
    ////
    //// Summary:
    ////     Gets or sets a value indicating whether the control and all its parent controls
    ////     are displayed.
    ////
    //// Returns:
    ////     true if the control and all its parent controls are displayed; otherwise,
    ////     false. The default is true.
    //[Localizable(true)]
    //public bool Visible { get; set; }
    ////
    //// Summary:
    ////     Gets or sets the width of the control.
    ////
    //// Returns:
    ////     The width of the control in pixels.
    //[Browsable(false)]
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //[EditorBrowsable(EditorBrowsableState.Always)]
    //public int Width { get; set; }
    ////
    //// Summary:
    ////     This property is not relevant for this class.
    ////
    //// Returns:
    ////     An System.Windows.Forms.IWindowTarget.
    //[Browsable(false)]
    //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //public IWindowTarget WindowTarget { get; set; }

    //// Summary:
    ////     This event is not relevant for this class.
    //[Browsable(false)]
    //[EditorBrowsable(EditorBrowsableState.Never)]
    //public event EventHandler AutoSizeChanged;
    ////
    //// Summary:
    ////     Occurs when the value of the System.Windows.Forms.Control.BackColor property
    ////     changes.
    //public event EventHandler BackColorChanged;
    ////
    //// Summary:
    ////     Occurs when the value of the System.Windows.Forms.Control.BackgroundImage
    ////     property changes.
    //public event EventHandler BackgroundImageChanged;
    ////
    //// Summary:
    ////     Occurs when the System.Windows.Forms.Control.BackgroundImageLayout property
    ////     changes.
    //public event EventHandler BackgroundImageLayoutChanged;
    ////
    //// Summary:
    ////     Occurs when the value of the System.Windows.Forms.BindingContext property
    ////     changes.
    //public event EventHandler BindingContextChanged;
    ////
    //// Summary:
    ////     Occurs when the value of the System.Windows.Forms.Control.CausesValidation
    ////     property changes.
    //public event EventHandler CausesValidationChanged;
    ////
    //// Summary:
    ////     Occurs when the focus or keyboard user interface (UI) cues change.
    //public event UICuesEventHandler ChangeUICues;
    ////
    //// Summary:
    ////     Occurs when the control is clicked.
    //public event EventHandler Click;
    ////
    //// Summary:
    ////     Occurs when the value of the System.Windows.Forms.Control.ClientSize property
    ////     changes.
    //public event EventHandler ClientSizeChanged;
    ////
    //// Summary:
    ////     Occurs when the value of the System.Windows.Forms.Control.ContextMenu property
    ////     changes.
    //[Browsable(false)]
    //public event EventHandler ContextMenuChanged;
    ////
    //// Summary:
    ////     Occurs when the value of the System.Windows.Forms.Control.ContextMenuStrip
    ////     property changes.
    //public event EventHandler ContextMenuStripChanged;
    ////
    //// Summary:
    ////     Occurs when a new control is added to the System.Windows.Forms.Control.ControlCollection.
    //[Browsable(true)]
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    //public event ControlEventHandler ControlAdded;
    ////
    //// Summary:
    ////     Occurs when a control is removed from the System.Windows.Forms.Control.ControlCollection.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    //[Browsable(true)]
    //public event ControlEventHandler ControlRemoved;
    ////
    //// Summary:
    ////     Occurs when the value of the System.Windows.Forms.Control.Cursor property
    ////     changes.
    //public event EventHandler CursorChanged;
    ////
    //// Summary:
    ////     Occurs when the value of the System.Windows.Forms.Control.Dock property changes.
    //public event EventHandler DockChanged;
    ////
    //// Summary:
    ////     Occurs when the control is double-clicked.
    //public event EventHandler DoubleClick;
    ////
    //// Summary:
    ////     Occurs when a drag-and-drop operation is completed.
    //public event DragEventHandler DragDrop;
    ////
    //// Summary:
    ////     Occurs when an object is dragged into the control's bounds.
    //public event DragEventHandler DragEnter;
    ////
    //// Summary:
    ////     Occurs when an object is dragged out of the control's bounds.
    //public event EventHandler DragLeave;
    ////
    //// Summary:
    ////     Occurs when an object is dragged over the control's bounds.
    //public event DragEventHandler DragOver;
    ////
    //// Summary:
    ////     Occurs when the System.Windows.Forms.Control.Enabled property value has changed.
    //public event EventHandler EnabledChanged;
    ////
    //// Summary:
    ////     Occurs when the control is entered.
    //public event EventHandler Enter;
    ////
    //// Summary:
    ////     Occurs when the System.Windows.Forms.Control.Font property value changes.
    //public event EventHandler FontChanged;
    ////
    //// Summary:
    ////     Occurs when the System.Windows.Forms.Control.ForeColor property value changes.
    //public event EventHandler ForeColorChanged;
    ////
    //// Summary:
    ////     Occurs during a drag operation.
    //public event GiveFeedbackEventHandler GiveFeedback;
    ////
    //// Summary:
    ////     Occurs when the control receives focus.
    //[Browsable(false)]
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    //public event EventHandler GotFocus;
    ////
    //// Summary:
    ////     Occurs when a handle is created for the control.
    //[Browsable(false)]
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    //public event EventHandler HandleCreated;
    ////
    //// Summary:
    ////     Occurs when the control's handle is in the process of being destroyed.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    //[Browsable(false)]
    //public event EventHandler HandleDestroyed;
    ////
    //// Summary:
    ////     Occurs when the user requests help for a control.
    //public event HelpEventHandler HelpRequested;
    ////
    //// Summary:
    ////     Occurs when the System.Windows.Forms.Control.ImeMode property has changed.
    //public event EventHandler ImeModeChanged;
    ////
    //// Summary:
    ////     Occurs when a control's display requires redrawing.
    //[Browsable(false)]
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    //public event InvalidateEventHandler Invalidated;
    ////
    //// Summary:
    ////     Occurs when a key is pressed while the control has focus.
    //public event KeyEventHandler KeyDown;
    ////
    //// Summary:
    ////     Occurs when a key is pressed while the control has focus.
    //public event KeyPressEventHandler KeyPress;
    ////
    //// Summary:
    ////     Occurs when a key is released while the control has focus.
    //public event KeyEventHandler KeyUp;
    ////
    //// Summary:
    ////     Occurs when a control should reposition its child controls.
    //public event LayoutEventHandler Layout;
    ////
    //// Summary:
    ////     Occurs when the input focus leaves the control.
    //public event EventHandler Leave;
    ////
    //// Summary:
    ////     Occurs when the System.Windows.Forms.Control.Location property value has
    ////     changed.
    //public event EventHandler LocationChanged;
    ////
    //// Summary:
    ////     Occurs when the control loses focus.
    //[Browsable(false)]
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    //public event EventHandler LostFocus;
    ////
    //// Summary:
    ////     Occurs when the control's margin changes.
    //public event EventHandler MarginChanged;
    ////
    //// Summary:
    ////     Occurs when the control loses or gains mouse capture.
    //public event EventHandler MouseCaptureChanged;
    ////
    //// Summary:
    ////     Occurs when the control is clicked by the mouse.
    //public event MouseEventHandler MouseClick;
    ////
    //// Summary:
    ////     Occurs when the control is double clicked by the mouse.
    //public event MouseEventHandler MouseDoubleClick;
    ////
    //// Summary:
    ////     Occurs when the mouse pointer is over the control and a mouse button is pressed.
    //public event MouseEventHandler MouseDown;
    ////
    //// Summary:
    ////     Occurs when the mouse pointer enters the control.
    //public event EventHandler MouseEnter;
    ////
    //// Summary:
    ////     Occurs when the mouse pointer rests on the control.
    //public event EventHandler MouseHover;
    ////
    //// Summary:
    ////     Occurs when the mouse pointer leaves the control.
    //public event EventHandler MouseLeave;
    ////
    //// Summary:
    ////     Occurs when the mouse pointer is moved over the control.
    //public event MouseEventHandler MouseMove;
    ////
    //// Summary:
    ////     Occurs when the mouse pointer is over the control and a mouse button is released.
    //public event MouseEventHandler MouseUp;
    ////
    //// Summary:
    ////     Occurs when the mouse wheel moves while the control has focus.
    //[Browsable(false)]
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    //public event MouseEventHandler MouseWheel;
    ////
    //// Summary:
    ////     Occurs when the control is moved.
    //public event EventHandler Move;
    ////
    //// Summary:
    ////     Occurs when the control's padding changes.
    //public event EventHandler PaddingChanged;
    ////
    //// Summary:
    ////     Occurs when the control is redrawn.
    //public event PaintEventHandler Paint;
    ////
    //// Summary:
    ////     Occurs when the System.Windows.Forms.Control.Parent property value changes.
    //public event EventHandler ParentChanged;
    ////
    //// Summary:
    ////     Occurs before the System.Windows.Forms.Control.KeyDown event when a key is
    ////     pressed while focus is on this control.
    //public event PreviewKeyDownEventHandler PreviewKeyDown;
    ////
    //// Summary:
    ////     Occurs when System.Windows.Forms.AccessibleObject is providing help to accessibility
    ////     applications.
    //public event QueryAccessibilityHelpEventHandler QueryAccessibilityHelp;
    ////
    //// Summary:
    ////     Occurs during a drag-and-drop operation and enables the drag source to determine
    ////     whether the drag-and-drop operation should be canceled.
    //public event QueryContinueDragEventHandler QueryContinueDrag;
    ////
    //// Summary:
    ////     Occurs when the value of the System.Windows.Forms.Control.Region property
    ////     changes.
    //public event EventHandler RegionChanged;
    ////
    //// Summary:
    ////     Occurs when the control is resized.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    //public event EventHandler Resize;
    ////
    //// Summary:
    ////     Occurs when the System.Windows.Forms.Control.RightToLeft property value changes.
    //public event EventHandler RightToLeftChanged;
    ////
    //// Summary:
    ////     Occurs when the System.Windows.Forms.Control.Size property value changes.
    //public event EventHandler SizeChanged;
    ////
    //// Summary:
    ////     Occurs when the control style changes.
    //public event EventHandler StyleChanged;
    ////
    //// Summary:
    ////     Occurs when the system colors change.
    //public event EventHandler SystemColorsChanged;
    ////
    //// Summary:
    ////     Occurs when the System.Windows.Forms.Control.TabIndex property value changes.
    //public event EventHandler TabIndexChanged;
    ////
    //// Summary:
    ////     Occurs when the System.Windows.Forms.Control.TabStop property value changes.
    //public event EventHandler TabStopChanged;
    ////
    //// Summary:
    ////     Occurs when the System.Windows.Forms.Control.Text property value changes.
    //public event EventHandler TextChanged;
    ////
    //// Summary:
    ////     Occurs when the control is finished validating.
    //public event EventHandler Validated;
    ////
    //// Summary:
    ////     Occurs when the control is validating.
    //public event CancelEventHandler Validating;
    ////
    //// Summary:
    ////     Occurs when the System.Windows.Forms.Control.Visible property value changes.
    //public event EventHandler VisibleChanged;
    //
    // Summary:
    //     Executes the specified delegate asynchronously on the thread that the control's
    //     underlying handle was created on.
    //
    // Parameters:
    //   method:
    //     A delegate to a method that takes no parameters.
    //
    // Returns:
    //     An System.IAsyncResult that represents the result of the System.Windows.Forms.Control.BeginInvoke(System.Delegate)
    //     operation.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    //public IAsyncResult BeginInvoke(Delegate method);
    ////
    //// Summary:
    ////     Executes the specified delegate asynchronously with the specified arguments,
    ////     on the thread that the control's underlying handle was created on.
    ////
    //// Parameters:
    ////   method:
    ////     A delegate to a method that takes parameters of the same number and type
    ////     that are contained in the args parameter.
    ////
    ////   args:
    ////     An array of objects to pass as arguments to the given method. This can be
    ////     null if no arguments are needed.
    ////
    //// Returns:
    ////     An System.IAsyncResult that represents the result of the System.Windows.Forms.Control.BeginInvoke(System.Delegate)
    ////     operation.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    //public IAsyncResult BeginInvoke(Delegate method, params object[] args);
    ////
    //// Summary:
    ////     Brings the control to the front of the z-order.
    //public void BringToFront();
    ////
    //// Summary:
    ////     Retrieves a value indicating whether the specified control is a child of
    ////     the control.
    ////
    //// Parameters:
    ////   ctl:
    ////     The System.Windows.Forms.Control to evaluate.
    ////
    //// Returns:
    ////     true if the specified control is a child of the control; otherwise, false.
    //public bool Contains(Control ctl);
    ////
    //// Summary:
    ////     Creates a new accessibility object for the control.
    ////
    //// Returns:
    ////     A new System.Windows.Forms.AccessibleObject for the control.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    //// protected virtualAccessibleObject CreateAccessibilityInstance();
    ////

    //
    // Summary:
    //     Creates the System.Drawing.Graphics for the control.
    //
    // Returns:
    //     The System.Drawing.Graphics for the control.
    public Graphics CreateGraphics()
    {
      Contract.Ensures(Contract.Result<Graphics>() != null);

      return default(Graphics);
    }

    //// Summary:
    ////     Begins a drag-and-drop operation.
    ////
    //// Parameters:
    ////   data:
    ////     The data to drag.
    ////
    ////   allowedEffects:
    ////     One of the System.Windows.Forms.DragDropEffects values.
    ////
    //// Returns:
    ////     A value from the System.Windows.Forms.DragDropEffects enumeration that represents
    ////     the final effect that was performed during the drag-and-drop operation.
    ////public DragDropEffects DoDragDrop(object data, DragDropEffects allowedEffects);
    ////
    //// Summary:
    ////     Supports rendering to the specified bitmap.
    ////
    //// Parameters:
    ////   bitmap:
    ////     The bitmap to be drawn to.
    ////
    ////   targetBounds:
    ////     The bounds within which the control is rendered.
    ////public void DrawToBitmap(System.Drawing.Bitmap bitmap, System.Drawing.Rectangle targetBounds);
    ////
    //// Summary:
    ////     Retrieves the return value of the asynchronous operation represented by the
    ////     System.IAsyncResult passed.
    ////
    //// Parameters:
    ////   asyncResult:
    ////     The System.IAsyncResult that represents a specific invoke asynchronous operation,
    ////     returned when calling System.Windows.Forms.Control.BeginInvoke(System.Delegate).
    ////
    //// Returns:
    ////     The System.Object generated by the asynchronous operation.
    ////
    //// Exceptions:
    ////   System.ArgumentNullException:
    ////     The asyncResult parameter value is null.
    ////
    ////   System.ArgumentException:
    ////     The asyncResult object was not created by a preceding call of the System.Windows.Forms.Control.BeginInvoke(System.Delegate)
    ////     method from the same control.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    //public object EndInvoke(IAsyncResult asyncResult);
    ////
    //// Summary:
    ////     Retrieves the form that the control is on.
    ////
    //// Returns:
    ////     The System.Windows.Forms.Form that the control is on.
    //public Form FindForm();
    ////
    //// Summary:
    ////     Sets input focus to the control.
    ////
    //// Returns:
    ////     true if the input focus request was successful; otherwise, false.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    //public bool Focus();
    ////
    //// Summary:
    ////     Retrieves the control that contains the specified handle.
    ////
    //// Parameters:
    ////   handle:
    ////     The window handle (HWND) to search for.
    ////
    //// Returns:
    ////     The System.Windows.Forms.Control that represents the control associated with
    ////     the specified handle; returns null if no control with the specified handle
    ////     is found.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    //public static Control FromChildHandle(IntPtr handle);
    ////
    //// Summary:
    ////     Returns the control that is currently associated with the specified handle.
    ////
    //// Parameters:
    ////   handle:
    ////     The window handle (HWND) to search for.
    ////
    //// Returns:
    ////     A System.Windows.Forms.Control that represents the control associated with
    ////     the specified handle; returns null if no control with the specified handle
    ////     is found.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    //public static Control FromHandle(IntPtr handle);
    ////
    //// Summary:
    ////     Retrieves the specified System.Windows.Forms.AccessibleObject.
    ////
    //// Parameters:
    ////   objectId:
    ////     An Int32 that identifies the System.Windows.Forms.AccessibleObject to retrieve.
    ////
    //// Returns:
    ////     An System.Windows.Forms.AccessibleObject.
    //// protected virtualAccessibleObject GetAccessibilityObjectById(int objectId);
    ////
    //// Summary:
    ////     Retrieves a value indicating how a control will behave when its System.Windows.Forms.Control.AutoSize
    ////     property is enabled.
    ////
    //// Returns:
    ////     One of the System.Windows.Forms.AutoSizeMode values.
    //protected AutoSizeMode GetAutoSizeMode();
    ////
    //// Summary:
    ////     Retrieves the child control that is located at the specified coordinates.
    ////
    //// Parameters:
    ////   pt:
    ////     A System.Drawing.Point that contains the coordinates where you want to look
    ////     for a control. Coordinates are expressed relative to the upper-left corner
    ////     of the control's client area.
    ////
    //// Returns:
    ////     A System.Windows.Forms.Control that represents the control that is located
    ////     at the specified point.
    //public Control GetChildAtPoint(System.Drawing.Point pt);
    ////
    //// Summary:
    ////     Retrieves the child control that is located at the specified coordinates,
    ////     specifying whether to ignore child controls of a certain type.
    ////
    //// Parameters:
    ////   pt:
    ////     A System.Drawing.Point that contains the coordinates where you want to look
    ////     for a control. Coordinates are expressed relative to the upper-left corner
    ////     of the control's client area.
    ////
    ////   skipValue:
    ////     One of the values of System.Windows.Forms.GetChildAtPointSkip, determining
    ////     whether to ignore child controls of a certain type.
    ////
    //// Returns:
    ////     The child System.Windows.Forms.Control at the specified coordinates.
    //public Control GetChildAtPoint(System.Drawing.Point pt, GetChildAtPointSkip skipValue);
    ////
    //// Summary:
    ////     Returns the next System.Windows.Forms.ContainerControl up the control's chain
    ////     of parent controls.
    ////
    //// Returns:
    ////     An System.Windows.Forms.IContainerControl, that represents the parent of
    ////     the System.Windows.Forms.Control.
    //public IContainerControl GetContainerControl();
    ////
    //// Summary:
    ////     Retrieves the next control forward or back in the tab order of child controls.
    ////
    //// Parameters:
    ////   ctl:
    ////     The System.Windows.Forms.Control to start the search with.
    ////
    ////   forward:
    ////     true to search forward in the tab order; false to search backward.
    ////
    //// Returns:
    ////     The next System.Windows.Forms.Control in the tab order.
    //public Control GetNextControl(Control ctl, bool forward);
    ////
    //// Summary:
    ////     Retrieves the size of a rectangular area into which a control can be fitted.
    ////
    //// Parameters:
    ////   proposedSize:
    ////     The custom-sized area for a control.
    ////
    //// Returns:
    ////     An ordered pair of type System.Drawing.Size representing the width and height
    ////     of a rectangle.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    //public virtual System.Drawing.Size GetPreferredSize(System.Drawing.Size proposedSize);
    //
    // Summary:
    //     Retrieves the bounds within which the control is scaled.
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

    // Summary:
    //     Conceals the control from the user.
    //public void Hide();
    //

    // Summary:
    //     Invalidates the entire surface of the control and causes the control to be
    //     redrawn.
    //public void Invalidate();
    //
    // Summary:
    //     Invalidates a specific region of the control and causes a paint message to
    //     be sent to the control. Optionally, invalidates the child controls assigned
    //     to the control.
    //
    // Parameters:
    //   invalidateChildren:
    //     true to invalidate the control's child controls; otherwise, false.
    //public void Invalidate(bool invalidateChildren);
    ////
    //// Summary:
    ////     Invalidates the specified region of the control (adds it to the control's
    ////     update region, which is the area that will be repainted at the next paint
    ////     operation), and causes a paint message to be sent to the control.
    ////
    //// Parameters:
    ////   rc:
    ////     A System.Drawing.Rectangle that represents the region to invalidate.
    //public void Invalidate(System.Drawing.Rectangle rc);
    ////
    //// Summary:
    ////     Invalidates the specified region of the control (adds it to the control's
    ////     update region, which is the area that will be repainted at the next paint
    ////     operation), and causes a paint message to be sent to the control.
    ////
    //// Parameters:
    ////   region:
    ////     The System.Drawing.Region to invalidate.
    //public void Invalidate(System.Drawing.Region region);
    ////
    //// Summary:
    ////     Invalidates the specified region of the control (adds it to the control's
    ////     update region, which is the area that will be repainted at the next paint
    ////     operation), and causes a paint message to be sent to the control. Optionally,
    ////     invalidates the child controls assigned to the control.
    ////
    //// Parameters:
    ////   rc:
    ////     A System.Drawing.Rectangle that represents the region to invalidate.
    ////
    ////   invalidateChildren:
    ////     true to invalidate the control's child controls; otherwise, false.
    //public void Invalidate(System.Drawing.Rectangle rc, bool invalidateChildren);
    ////
    //// Summary:
    ////     Invalidates the specified region of the control (adds it to the control's
    ////     update region, which is the area that will be repainted at the next paint
    ////     operation), and causes a paint message to be sent to the control. Optionally,
    ////     invalidates the child controls assigned to the control.
    ////
    //// Parameters:
    ////   region:
    ////     The System.Drawing.Region to invalidate.
    ////
    ////   invalidateChildren:
    ////     true to invalidate the control's child controls; otherwise, false.
    //public void Invalidate(System.Drawing.Region region, bool invalidateChildren);
    ////
    //// Summary:
    ////     Executes the specified delegate on the thread that owns the control's underlying
    ////     window handle.
    ////
    //// Parameters:
    ////   method:
    ////     A delegate that contains a method to be called in the control's thread context.
    ////
    //// Returns:
    ////     The return value from the delegate being invoked, or null if the delegate
    ////     has no return value.
    //public object Invoke(Delegate method);
    ////
    //// Summary:
    ////     Executes the specified delegate, on the thread that owns the control's underlying
    ////     window handle, with the specified list of arguments.
    ////
    //// Parameters:
    ////   method:
    ////     A delegate to a method that takes parameters of the same number and type
    ////     that are contained in the args parameter.
    ////
    ////   args:
    ////     An array of objects to pass as arguments to the specified method. This parameter
    ////     can be null if the method takes no arguments.
    ////
    //// Returns:
    ////     An System.Object that contains the return value from the delegate being invoked,
    ////     or null if the delegate has no return value.

    //
    // Summary:
    //     Determines whether the CAPS LOCK, NUM LOCK, or SCROLL LOCK key is in effect.
    //
    // Parameters:
    //   keyVal:
    //     The CAPS LOCK, NUM LOCK, or SCROLL LOCK member of the System.Windows.Forms.Keys
    //     enumeration.
    //
    // Returns:
    //     true if the specified key or keys are in effect; otherwise, false.
    //
    // Exceptions:
    //   System.NotSupportedException:
    //     The keyVal parameter refers to a key other than the CAPS LOCK, NUM LOCK,
    //     or SCROLL LOCK key.
    //public static bool IsKeyLocked(Keys keyVal) { }
    
    //
    // Summary:
    //     Determines if the specified character is the mnemonic character assigned
    //     to the control in the specified string.
    //
    // Parameters:
    //   charCode:
    //     The character to test.
    //
    //   text:
    //     The string to search.
    //
    // Returns:
    //     true if the charCode character is the mnemonic character assigned to the
    //     control; otherwise, false.
    //public static bool IsMnemonic(char charCode, string text);
    //
    // Summary:
    //     Raises the System.Windows.Forms.Control.Invalidated event with a specified
    //     region of the control to invalidate.
    //
    // Parameters:
    //   invalidatedArea:
    ////     A System.Drawing.Rectangle representing the area to invalidate.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]

    //
    // Summary:
    //     Raises the System.Windows.Forms.Control.PaddingChanged event.
    //
    // Parameters:
    //   e:
    //     A System.EventArgs that contains the event data.
     protected virtual void OnPaddingChanged(EventArgs e){
        Contract.Requires(e != null);
      }

    //
    // Summary:
    //     Raises the System.Windows.Forms.Control.Paint event.
    //
    // Parameters:
    //   e:
    //     A System.Windows.Forms.PaintEventArgs that contains the event data.
    // [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnPaint(PaintEventArgs e)
    {
      Contract.Requires(e != null);
    }


    // Summary:
    //     Forces the control to apply layout logic to all its child controls.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    //public void PerformLayout();
    ////
    //// Summary:
    ////     Forces the control to apply layout logic to all its child controls.
    ////
    //// Parameters:
    ////   affectedControl:
    ////     A System.Windows.Forms.Control that represents the most recently changed
    ////     control.
    ////
    ////   affectedProperty:
    ////     The name of the most recently changed property on the control.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    //public void PerformLayout(Control affectedControl, string affectedProperty);
    ////
    //// Summary:
    ////     Computes the location of the specified screen point into client coordinates.
    ////
    //// Parameters:
    ////   p:
    ////     The screen coordinate System.Drawing.Point to convert.
    ////
    //// Returns:
    ////     A System.Drawing.Point that represents the converted System.Drawing.Point,
    ////     p, in client coordinates.
    //public System.Drawing.Point PointToClient(System.Drawing.Point p);
    ////
    //// Summary:
    ////     Computes the location of the specified client point into screen coordinates.
    ////
    //// Parameters:
    ////   p:
    ////     The client coordinate System.Drawing.Point to convert.
    ////
    //// Returns:
    ////     A System.Drawing.Point that represents the converted System.Drawing.Point,
    ////     p, in screen coordinates.
    //public System.Drawing.Point PointToScreen(System.Drawing.Point p);
    ////
    //// Summary:
    ////     Preprocesses keyboard or input messages within the message loop before they
    ////     are dispatched.
    ////
    //// Parameters:
    ////   msg:
    ////     A System.Windows.Forms.Message that represents the message to process.
    ////
    //// Returns:
    ////     One of the System.Windows.Forms.PreProcessControlState values, depending
    ////     on whether System.Windows.Forms.Control.PreProcessMessage(System.Windows.Forms.Message@)
    ////     is true or false and whether System.Windows.Forms.Control.IsInputKey(System.Windows.Forms.Keys)
    ////     or System.Windows.Forms.Control.IsInputChar(System.Char) are true or false.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    //public PreProcessControlState PreProcessControlMessage(ref Message msg);
    ////
    //// Summary:
    ////     Preprocesses keyboard or input messages within the message loop before they
    ////     are dispatched.
    ////
    //// Parameters:
    ////   msg:
    ////     A System.Windows.Forms.Message, passed by reference, that represents the
    ////     message to process. The possible values are WM_KEYDOWN, WM_SYSKEYDOWN, WM_CHAR,
    ////     and WM_SYSCHAR.
    ////
    //// Returns:
    ////     true if the message was processed by the control; otherwise, false.
    //public virtual bool PreProcessMessage(ref Message msg);
    ////
    //// Summary:
    ////     Processes a command key.
    ////
    //// Parameters:
    ////   msg:
    ////     A System.Windows.Forms.Message, passed by reference, that represents the
    ////     window message to process.
    ////
    ////   keyData:
    ////     One of the System.Windows.Forms.Keys values that represents the key to process.
    ////
    //// Returns:
    ////     true if the character was processed by the control; otherwise, false.

    // Summary:
    //     Represents a collection of System.Windows.Forms.Control objects.
    //[ComVisible(false)]
    //[ListBindable(false)]
    public class ControlCollection : ArrangedElementCollection //, ICollection, IEnumerable, ICloneable
    {
      // Summary:
      //     Initializes a new instance of the System.Windows.Forms.Control.ControlCollection
      //     class.
      //
      // Parameters:
      //   owner:
      //     A System.Windows.Forms.Control representing the control that owns the control
      //     collection.
      //public ControlCollection(Control owner);

      // Summary:
      //     Gets the control that owns this System.Windows.Forms.Control.ControlCollection.
      //
      // Returns:
      //     The System.Windows.Forms.Control that owns this System.Windows.Forms.Control.ControlCollection.
      //public Control Owner { get; }

      // Summary:
      //     Indicates the System.Windows.Forms.Control at the specified indexed location
      //     in the collection.
      //
      // Parameters:
      //   index:
      //     The index of the control to retrieve from the control collection.
      //
      // Returns:
      //     The System.Windows.Forms.Control located at the specified index location
      //     within the control collection.
      //
      // Exceptions:
      //   System.ArgumentOutOfRangeException:
      //     The index value is less than zero or is greater than or equal to the number
      //     of controls in the collection.
      public virtual Control this[int index]
      {
        get
        {
          Contract.Requires(index >= 0);
          Contract.Requires(index < this.Count);

          return default(Control);
        }
      }

      //
      // Summary:
      //     Indicates a System.Windows.Forms.Control with the specified key in the collection.
      //
      // Parameters:
      //   key:
      //     The name of the control to retrieve from the control collection.
      //
      // Returns:
      //     The System.Windows.Forms.Control with the specified key within the System.Windows.Forms.Control.ControlCollection.
      //public virtual Control this[string key] { get; }

      // Summary:
      //     Adds the specified control to the control collection.
      //
      // Parameters:
      //   value:
      //     The System.Windows.Forms.Control to add to the control collection.
      //
      // Exceptions:
      //   System.Exception:
      //     The specified control is a top-level control, or a circular control reference
      //     would result if this control were added to the control collection.
      //
      //   System.ArgumentException:
      //     The object assigned to the value parameter is not a System.Windows.Forms.Control.
      //public virtual void Add(Control value);
      //
      // Summary:
      //     Adds an array of control objects to the collection.
      //
      // Parameters:
      //   controls:
      //     An array of System.Windows.Forms.Control objects to add to the collection.
      //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
      public virtual void AddRange(Control[] controls)
      {
        Contract.Requires(controls != null);
      }
      //
      // Summary:
      //     Removes all controls from the collection.
      //public virtual void Clear();
      //
      // Summary:
      //     Determines whether the specified control is a member of the collection.
      //
      // Parameters:
      //   control:
      //     The System.Windows.Forms.Control to locate in the collection.
      //
      // Returns:
      //     true if the System.Windows.Forms.Control is a member of the collection; otherwise,
      //     false.
      //public bool Contains(Control control);
      //
      // Summary:
      //     Determines whether the System.Windows.Forms.Control.ControlCollection contains
      //     an item with the specified key.
      //
      // Parameters:
      //   key:
      //     The key to locate in the System.Windows.Forms.Control.ControlCollection.
      //
      // Returns:
      //     true if the System.Windows.Forms.Control.ControlCollection contains an item
      //     with the specified key; otherwise, false.
      //public virtual bool ContainsKey(string key);
      //
      // Summary:
      //     Searches for controls by their System.Windows.Forms.Control.Name property
      //     and builds an array of all the controls that match.
      //
      // Parameters:
      //   key:
      //     The key to locate in the System.Windows.Forms.Control.ControlCollection.
      //
      //   searchAllChildren:
      //     true to search all child controls; otherwise, false.
      //
      // Returns:
      //     An array of type System.Windows.Forms.Control containing the matching controls.
      //
      // Exceptions:
      //   System.ArgumentException:
      //     The key parameter is null or the empty string ("").
     // [Pure]
      public Control[] Find(string key, bool searchAllChildren)
      {
        Contract.Requires(!string.IsNullOrEmpty(key));

        Contract.Ensures(Contract.Result<Control[]>() != null);

        return default(Control[]);
      }
      //
      // Summary:
      //     Retrieves the index of the specified child control within the control collection.
      //
      // Parameters:
      //   child:
      //     The System.Windows.Forms.Control to search for in the control collection.
      //
      // Returns:
      //     A zero-based index value that represents the location of the specified child
      //     control within the control collection.
      //
      // Exceptions:
      //   System.ArgumentException:
      //     The childSystem.Windows.Forms.Control is not in the System.Windows.Forms.Control.ControlCollection.
     // [Pure]
      public int GetChildIndex(Control child)
      {
        Contract.Ensures(Contract.Result<int>() >= 0);

        return default(int);
      }
      //
      // Summary:
      //     Retrieves the index of the specified child control within the control collection,
      //     and optionally raises an exception if the specified control is not within
      //     the control collection.
      //
      // Parameters:
      //   child:
      //     The System.Windows.Forms.Control to search for in the control collection.
      //
      //   throwException:
      //     true to throw an exception if the System.Windows.Forms.Control specified
      //     in the child parameter is not a control in the System.Windows.Forms.Control.ControlCollection;
      //     otherwise, false.
      //
      // Returns:
      //     A zero-based index value that represents the location of the specified child
      //     control within the control collection; otherwise -1 if the specified System.Windows.Forms.Control
      //     is not found in the System.Windows.Forms.Control.ControlCollection.
      //
      // Exceptions:
      //   System.ArgumentException:
      //     The childSystem.Windows.Forms.Control is not in the System.Windows.Forms.Control.ControlCollection,
      //     and the throwException parameter value is true.
      public virtual int GetChildIndex(Control child, bool throwException)
      {
        Contract.Ensures(Contract.Result<int>() >= 0);

        return default(int);
      }
      //
      // Summary:
      //     Retrieves a reference to an enumerator object that is used to iterate over
      //     a System.Windows.Forms.Control.ControlCollection.
      //
      // Returns:
      //     An System.Collections.IEnumerator.
      //public override IEnumerator GetEnumerator();
      //
      // Summary:
      //     Retrieves the index of the specified control in the control collection.
      //
      // Parameters:
      //   control:
      //     The System.Windows.Forms.Control to locate in the collection.
      //
      // Returns:
      //     A zero-based index value that represents the position of the specified System.Windows.Forms.Control
      //     in the System.Windows.Forms.Control.ControlCollection.
     // [Pure]
      public int IndexOf(Control control)
      {
        Contract.Ensures(Contract.Result<int>() >= -1);

        return default(int);
      }

      //
      // Summary:
      //     Retrieves the index of the first occurrence of the specified item within
      //     the collection.
      //
      // Parameters:
      //   key:
      //     The name of the control to search for.
      //
      // Returns:
      //     The zero-based index of the first occurrence of the control with the specified
      //     name in the collection.
     // [Pure]
      public virtual int IndexOfKey(string key)
      {
        Contract.Ensures(Contract.Result<int>() >= -1);

        return default(int);
      }

      //
      // Summary:
      //     Removes the specified control from the control collection.
      //
      // Parameters:
      //   value:
      //     The System.Windows.Forms.Control to remove from the System.Windows.Forms.Control.ControlCollection.
      //public virtual void Remove(Control value);
      //
      // Summary:
      //     Removes a control from the control collection at the specified indexed location.
      //
      // Parameters:
      //   index:
      //     The index value of the System.Windows.Forms.Control to remove.
      //public void RemoveAt(int index)

      //
      // Summary:
      //     Removes the child control with the specified key.
      //
      // Parameters:
      //   key:
      //     The name of the child control to remove.
      //public virtual void RemoveByKey(string key);
      //
      // Summary:
      //     Sets the index of the specified child control in the collection to the specified
      //     index value.
      //
      // Parameters:
      //   child:
      //     The childSystem.Windows.Forms.Control to search for.
      //
      //   newIndex:
      //     The new index value of the control.
      //
      // Exceptions:
      //   System.ArgumentException:
      //     The child control is not in the System.Windows.Forms.Control.ControlCollection.
      //public virtual void SetChildIndex(Control child, int newIndex);


      //// protected virtual void OnAutoSizeChanged(EventArgs e);
      //
      // Summary:
      //     Raises the System.Windows.Forms.Control.BackColorChanged event.
      //
      // Parameters:
      //   e:
      //     An System.EventArgs that contains the event data.
     // [EditorBrowsable(EditorBrowsableState.Advanced)]
      //// protected virtual void OnBackColorChanged(EventArgs e);
      //
      // Summary:
      //     Raises the System.Windows.Forms.Control.BackgroundImageChanged event.
      //
      // Parameters:
      //   e:
      //     An System.EventArgs that contains the event data.
     // [EditorBrowsable(EditorBrowsableState.Advanced)]
      //// protected virtual void OnBackgroundImageChanged(EventArgs e);
      //
      // Summary:
      //     Raises the System.Windows.Forms.Control.BackgroundImageLayoutChanged event.
      //
      // Parameters:
      //   e:
      //     An System.EventArgs that contains the event data.
     // [EditorBrowsable(EditorBrowsableState.Advanced)]
      //// protected virtual void OnBackgroundImageLayoutChanged(EventArgs e);
      //
      // Summary:
      //     Raises the System.Windows.Forms.Control.BindingContextChanged event.
      //
      // Parameters:
      //   e:
      //     An System.EventArgs that contains the event data.
     // [EditorBrowsable(EditorBrowsableState.Advanced)]
      // protected virtual void OnBindingContextChanged(EventArgs e);
      //
      // Summary:
      //     Raises the System.Windows.Forms.Control.CausesValidationChanged event.
      //
      // Parameters:
      //   e:
      //     An System.EventArgs that contains the event data.
     // [EditorBrowsable(EditorBrowsableState.Advanced)]
      // protected virtual void OnCausesValidationChanged(EventArgs e);
      //
      // Summary:
      //     Raises the System.Windows.Forms.Control.ChangeUICues event.
      //
      // Parameters:
      //   e:
      //     A System.Windows.Forms.UICuesEventArgs that contains the event data.
     // [EditorBrowsable(EditorBrowsableState.Advanced)]
      // protected virtual void OnChangeUICues(UICuesEventArgs e);
      //
      // Summary:
      //     Raises the System.Windows.Forms.Control.Click event.
      //
      // Parameters:
      //   e:
      //     An System.EventArgs that contains the event data.
     // [EditorBrowsable(EditorBrowsableState.Advanced)]
      // protected virtual void OnClick(EventArgs e);
      //
      // Summary:
      //     Raises the System.Windows.Forms.Control.ClientSizeChanged event.
      //
      // Parameters:
      //   e:
      //     An System.EventArgs that contains the event data.
     // [EditorBrowsable(EditorBrowsableState.Advanced)]
      // protected virtual void OnClientSizeChanged(EventArgs e);
      //
      // Summary:
      //     Raises the System.Windows.Forms.Control.ContextMenuChanged event.
      //
      // Parameters:
      //   e:
      //     An System.EventArgs that contains the event data.
     // [EditorBrowsable(EditorBrowsableState.Advanced)]
      // protected virtual void OnContextMenuChanged(EventArgs e);
      //
      // Summary:
      //     Raises the System.Windows.Forms.Control.ContextMenuStripChanged event.
      //
      // Parameters:
      //   e:
      //     An System.EventArgs that contains the event data.
     // [EditorBrowsable(EditorBrowsableState.Advanced)]
      // protected virtual void OnContextMenuStripChanged(EventArgs e);
      //
      // Summary:
      //     Raises the System.Windows.Forms.Control.ControlAdded event.
      //
      // Parameters:
      //   e:
      //     A System.Windows.Forms.ControlEventArgs that contains the event data.
     // [EditorBrowsable(EditorBrowsableState.Advanced)]
      // protected virtual void OnControlAdded(ControlEventArgs e);
      //
      // Summary:
      //     Raises the System.Windows.Forms.Control.ControlRemoved event.
      //
      // Parameters:
      //   e:
      //     A System.Windows.Forms.ControlEventArgs that contains the event data.
     // [EditorBrowsable(EditorBrowsableState.Advanced)]
      // protected virtual void OnControlRemoved(ControlEventArgs e);
      //
      // Summary:
      //     Raises the System.Windows.Forms.Control.CreateControl() method.
     // [EditorBrowsable(EditorBrowsableState.Advanced)]
      // protected virtual void OnCreateControl();
      //
      // Summary:
      //     Raises the System.Windows.Forms.Control.CursorChanged event.
      //
      // Parameters:
      //   e:
      //     An System.EventArgs that contains the event data.
     // [EditorBrowsable(EditorBrowsableState.Advanced)]
      // protected virtual void OnCursorChanged(EventArgs e);
      //
      // Summary:
      //     Raises the System.Windows.Forms.Control.DockChanged event.
      //
      // Parameters:
      //   e:
      //     An System.EventArgs that contains the event data.
     // [EditorBrowsable(EditorBrowsableState.Advanced)]
      // protected virtual void OnDockChanged(EventArgs e);
      //
      // Summary:
      //     Raises the System.Windows.Forms.Control.DoubleClick event.
      //
      // Parameters:
      //   e:
      //     An System.EventArgs that contains the event data.
     // [EditorBrowsable(EditorBrowsableState.Advanced)]
      // protected virtual void OnDoubleClick(EventArgs e);
      //
      // Summary:
      //     Raises the System.Windows.Forms.Control.DragDrop event.
      //
      // Parameters:
      //   drgevent:
      //     A System.Windows.Forms.DragEventArgs that contains the event data.
     // [EditorBrowsable(EditorBrowsableState.Advanced)]
      // protected virtual void OnDragDrop(DragEventArgs drgevent);
      //
      // Summary:
      //     Raises the System.Windows.Forms.Control.DragEnter event.
      //
      // Parameters:
      //   drgevent:
      //     A System.Windows.Forms.DragEventArgs that contains the event data.
     // [EditorBrowsable(EditorBrowsableState.Advanced)]
      // protected virtual void OnDragEnter(DragEventArgs drgevent);
      //
      // Summary:
      //     Raises the System.Windows.Forms.Control.DragLeave event.
      //
      // Parameters:
      //   e:
      //     An System.EventArgs that contains the event data.
     // [EditorBrowsable(EditorBrowsableState.Advanced)]
      // protected virtual void OnDragLeave(EventArgs e);
      //
      // Summary:
      //     Raises the System.Windows.Forms.Control.DragOver event.
      //
      // Parameters:
      //   drgevent:
      //     A System.Windows.Forms.DragEventArgs that contains the event data.
     // [EditorBrowsable(EditorBrowsableState.Advanced)]
      // protected virtual void OnDragOver(DragEventArgs drgevent);
      //
      // Summary:
      //     Raises the System.Windows.Forms.Control.EnabledChanged event.
      //
      // Parameters:
      //   e:
      //     An System.EventArgs that contains the event data.
     // [EditorBrowsable(EditorBrowsableState.Advanced)]
      // protected virtual void OnEnabledChanged(EventArgs e);
      //
      // Summary:
      //     Raises the System.Windows.Forms.Control.Enter event.
      //
      // Parameters:
      //   e:
      //     An System.EventArgs that contains the event data.
     // [EditorBrowsable(EditorBrowsableState.Advanced)]
      // protected virtual void OnEnter(EventArgs e);
      //
      // Summary:
      //     Raises the System.Windows.Forms.Control.FontChanged event.
      //
      // Parameters:
      //   e:
      //     An System.EventArgs that contains the event data.
     // [EditorBrowsable(EditorBrowsableState.Advanced)]
      // protected virtual void OnFontChanged(EventArgs e);
      //
      // Summary:
      //     Raises the System.Windows.Forms.Control.ForeColorChanged event.
      //
      // Parameters:
      //   e:
      //     An System.EventArgs that contains the event data.
     // [EditorBrowsable(EditorBrowsableState.Advanced)]
      // protected virtual void OnForeColorChanged(EventArgs e);
      //
      // Summary:
      //     Raises the System.Windows.Forms.Control.GiveFeedback event.
      //
      // Parameters:
      //   gfbevent:
      //     A System.Windows.Forms.GiveFeedbackEventArgs that contains the event data.
     // [EditorBrowsable(EditorBrowsableState.Advanced)]
      // protected virtual void OnGiveFeedback(GiveFeedbackEventArgs gfbevent);
      //
      // Summary:
      //     Raises the System.Windows.Forms.Control.GotFocus event.
      //
      // Parameters:
      //   e:
      //     An System.EventArgs that contains the event data.
     // [EditorBrowsable(EditorBrowsableState.Advanced)]
      // protected virtual void OnGotFocus(EventArgs e);
      //
      // Summary:
      //     Raises the System.Windows.Forms.Control.HandleCreated event.
      //
      // Parameters:
      //   e:
      //     An System.EventArgs that contains the event data.
     // [EditorBrowsable(EditorBrowsableState.Advanced)]
      // protected virtual void OnHandleCreated(EventArgs e);
      //
      // Summary:
      //     Raises the System.Windows.Forms.Control.HandleDestroyed event.
      //
      // Parameters:
      //   e:
      //     An System.EventArgs that contains the event data.
     // [EditorBrowsable(EditorBrowsableState.Advanced)]
      // protected virtual void OnHandleDestroyed(EventArgs e);
      //
      // Summary:
      //     Raises the System.Windows.Forms.Control.HelpRequested event.
      //
      // Parameters:
      //   hevent:
      //     A System.Windows.Forms.HelpEventArgs that contains the event data.
     // [EditorBrowsable(EditorBrowsableState.Advanced)]
      // protected virtual void OnHelpRequested(HelpEventArgs hevent);
      //
      // Summary:
      //     Raises the System.Windows.Forms.Control.ImeModeChanged event.
      //
      // Parameters:
      //   e:
      //     An System.EventArgs that contains the event data.
      // protected virtual void OnImeModeChanged(EventArgs e);
      //
      // Summary:
      //     Raises the System.Windows.Forms.Control.Invalidated event.
      //
      // Parameters:
      //   e:
      //     An System.Windows.Forms.InvalidateEventArgs that contains the event data.
     // [EditorBrowsable(EditorBrowsableState.Advanced)]
      // protected virtual void OnInvalidated(InvalidateEventArgs e);
      //
      // Summary:
      //     Raises the System.Windows.Forms.Control.KeyDown event.
      //
      // Parameters:
      //   e:
      //     A System.Windows.Forms.KeyEventArgs that contains the event data.
     // [EditorBrowsable(EditorBrowsableState.Advanced)]
      // protected virtual void OnKeyDown(KeyEventArgs e);
      //
      // Summary:
      //     Raises the System.Windows.Forms.Control.KeyPress event.
      //
      // Parameters:
      //   e:
      //     A System.Windows.Forms.KeyPressEventArgs that contains the event data.
     // [EditorBrowsable(EditorBrowsableState.Advanced)]
      // protected virtual void OnKeyPress(KeyPressEventArgs e);
      //
      // Summary:
      //     Raises the System.Windows.Forms.Control.KeyUp event.
      //
      // Parameters:
      //   e:
      //     A System.Windows.Forms.KeyEventArgs that contains the event data.
     // [EditorBrowsable(EditorBrowsableState.Advanced)]
      // protected virtual void OnKeyUp(KeyEventArgs e);
      //
      // Summary:
      //     Raises the System.Windows.Forms.Control.Layout event.
      //
      // Parameters:
      //   levent:
      //     A System.Windows.Forms.LayoutEventArgs that contains the event data.
     // [EditorBrowsable(EditorBrowsableState.Advanced)]
      // protected virtual void OnLayout(LayoutEventArgs levent);
      //
      // Summary:
      //     Raises the System.Windows.Forms.Control.Leave event.
      //
      // Parameters:
      //   e:
      //     An System.EventArgs that contains the event data.
     // [EditorBrowsable(EditorBrowsableState.Advanced)]
      // protected virtual void OnLeave(EventArgs e);
      //
      // Summary:
      //     Raises the System.Windows.Forms.Control.LocationChanged event.
      //
      // Parameters:
      //   e:
      //     An System.EventArgs that contains the event data.
     // [EditorBrowsable(EditorBrowsableState.Advanced)]
      // protected virtual void OnLocationChanged(EventArgs e);
      //
      // Summary:
      //     Raises the System.Windows.Forms.Control.LostFocus event.
      //
      // Parameters:
      //   e:
      //     An System.EventArgs that contains the event data.
     // [EditorBrowsable(EditorBrowsableState.Advanced)]
      // protected virtual void OnLostFocus(EventArgs e);
      //
      // Summary:
      //     Raises the System.Windows.Forms.Control.MarginChanged event.
      //
      // Parameters:
      //   e:
      //     A System.EventArgs that contains the event data.
      // protected virtual void OnMarginChanged(EventArgs e);
      //
      // Summary:
      //     Raises the System.Windows.Forms.Control.MouseCaptureChanged event.
      //
      // Parameters:
      //   e:
      //     An System.EventArgs that contains the event data.
     // [EditorBrowsable(EditorBrowsableState.Advanced)]
      // protected virtual void OnMouseCaptureChanged(EventArgs e);
      //
      // Summary:
      //     Raises the System.Windows.Forms.Control.MouseClick event.
      //
      // Parameters:
      //   e:
      //     An System.Windows.Forms.MouseEventArgs that contains the event data.
     // [EditorBrowsable(EditorBrowsableState.Advanced)]
      // protected virtual void OnMouseClick(MouseEventArgs e);
      //
      // Summary:
      //     Raises the System.Windows.Forms.Control.MouseDoubleClick event.
      //
      // Parameters:
      //   e:
      //     An System.Windows.Forms.MouseEventArgs that contains the event data.
     // [EditorBrowsable(EditorBrowsableState.Advanced)]
      // protected virtual void OnMouseDoubleClick(MouseEventArgs e);
      //
      // Summary:
      //     Raises the System.Windows.Forms.Control.MouseDown event.
      //
      // Parameters:
      //   e:
      //     A System.Windows.Forms.MouseEventArgs that contains the event data.
     // [EditorBrowsable(EditorBrowsableState.Advanced)]
      // protected virtual void OnMouseDown(MouseEventArgs e);
      //
      // Summary:
      //     Raises the System.Windows.Forms.Control.MouseEnter event.
      //
      // Parameters:
      //   e:
      //     An System.EventArgs that contains the event data.
     // [EditorBrowsable(EditorBrowsableState.Advanced)]
      // protected virtual void OnMouseEnter(EventArgs e);
      //
      // Summary:
      //     Raises the System.Windows.Forms.Control.MouseHover event.
      //
      // Parameters:
      //   e:
      //     An System.EventArgs that contains the event data.
     // [EditorBrowsable(EditorBrowsableState.Advanced)]
      // protected virtual void OnMouseHover(EventArgs e);
      //
      // Summary:
      //     Raises the System.Windows.Forms.Control.MouseLeave event.
      //
      // Parameters:
      //   e:
      //     An System.EventArgs that contains the event data.
     // [EditorBrowsable(EditorBrowsableState.Advanced)]
      // protected virtual void OnMouseLeave(EventArgs e);
      //
      // Summary:
      //     Raises the System.Windows.Forms.Control.MouseMove event.
      //
      // Parameters:
      //   e:
      //     A System.Windows.Forms.MouseEventArgs that contains the event data.
     // [EditorBrowsable(EditorBrowsableState.Advanced)]
      // protected virtual void OnMouseMove(MouseEventArgs e);
      //
      // Summary:
      //     Raises the System.Windows.Forms.Control.MouseUp event.
      //
      // Parameters:
      //   e:
      //     A System.Windows.Forms.MouseEventArgs that contains the event data.
     // [EditorBrowsable(EditorBrowsableState.Advanced)]
      // protected virtual void OnMouseUp(MouseEventArgs e);
      //
      // Summary:
      //     Raises the System.Windows.Forms.Control.MouseWheel event.
      //
      // Parameters:
      //   e:
      //     A System.Windows.Forms.MouseEventArgs that contains the event data.
     // [EditorBrowsable(EditorBrowsableState.Advanced)]
      // protected virtual void OnMouseWheel(MouseEventArgs e);
      //
      // Summary:
      //     Raises the System.Windows.Forms.Control.Move event.
      //
      // Parameters:
      //   e:
      //     An System.EventArgs that contains the event data.
     // [EditorBrowsable(EditorBrowsableState.Advanced)]
      // protected virtual void OnMove(EventArgs e);
      //
      // Summary:
      //     Notifies the control of Windows messages.
      //
      // Parameters:
      //   m:
      //     A System.Windows.Forms.Message that represents the Windows message.
     // [EditorBrowsable(EditorBrowsableState.Advanced)]
      // protected virtual void OnNotifyMessage(Message m);
      //
      // Summary:
      //     Paints the background of the control.
      //
      // Parameters:
      //   pevent:
      //     A System.Windows.Forms.PaintEventArgs that contains information about the
      //     control to paint.
     // [EditorBrowsable(EditorBrowsableState.Advanced)]
      // protected virtual void OnPaintBackground(PaintEventArgs pevent);
      //
      // Summary:
      //     Raises the System.Windows.Forms.Control.BackColorChanged event when the System.Windows.Forms.Control.BackColor
      //     property value of the control's container changes.
      //
      // Parameters:
      //   e:
      //     An System.EventArgs that contains the event data.
     // [EditorBrowsable(EditorBrowsableState.Advanced)]
      // protected virtual void OnParentBackColorChanged(EventArgs e);
      //
      // Summary:
      //     Raises the System.Windows.Forms.Control.BackgroundImageChanged event when
      //     the System.Windows.Forms.Control.BackgroundImage property value of the control's
      //     container changes.
      //
      // Parameters:
      //   e:
      //     An System.EventArgs that contains the event data.
     // [EditorBrowsable(EditorBrowsableState.Advanced)]
      // protected virtual void OnParentBackgroundImageChanged(EventArgs e);
      //
      // Summary:
      //     Raises the System.Windows.Forms.Control.BindingContextChanged event when
      //     the System.Windows.Forms.Control.BindingContext property value of the control's
      //     container changes.
      //
      // Parameters:
      //   e:
      //     An System.EventArgs that contains the event data.
     // [EditorBrowsable(EditorBrowsableState.Advanced)]
      // protected virtual void OnParentBindingContextChanged(EventArgs e);
      //
      // Summary:
      //     Raises the System.Windows.Forms.Control.ParentChanged event.
      //
      // Parameters:
      //   e:
      //     An System.EventArgs that contains the event data.
     // [EditorBrowsable(EditorBrowsableState.Advanced)]
      // protected virtual void OnParentChanged(EventArgs e);
      //
      // Summary:
      //     Raises the System.Windows.Forms.Control.CursorChanged event.
      //
      // Parameters:
      //   e:
      //     An System.EventArgs that contains the event data.
     // [EditorBrowsable(EditorBrowsableState.Advanced)]
      // protected virtual void OnParentCursorChanged(EventArgs e);
      //
      // Summary:
      //     Raises the System.Windows.Forms.Control.EnabledChanged event when the System.Windows.Forms.Control.Enabled
      //     property value of the control's container changes.
      //
      // Parameters:
      //   e:
      //     An System.EventArgs that contains the event data.
     // [EditorBrowsable(EditorBrowsableState.Advanced)]
      // protected virtual void OnParentEnabledChanged(EventArgs e);
      //
      // Summary:
      //     Raises the System.Windows.Forms.Control.FontChanged event when the System.Windows.Forms.Control.Font
      //     property value of the control's container changes.
      //
      // Parameters:
      //   e:
      //     An System.EventArgs that contains the event data.
     // [EditorBrowsable(EditorBrowsableState.Advanced)]
      // protected virtual void OnParentFontChanged(EventArgs e);
      //
      // Summary:
      //     Raises the System.Windows.Forms.Control.ForeColorChanged event when the System.Windows.Forms.Control.ForeColor
      //     property value of the control's container changes.
      //
      // Parameters:
      //   e:
      //     An System.EventArgs that contains the event data.
     // [EditorBrowsable(EditorBrowsableState.Advanced)]
      // protected virtual void OnParentForeColorChanged(EventArgs e);
      //
      // Summary:
      //     Raises the System.Windows.Forms.Control.RightToLeftChanged event when the
      //     System.Windows.Forms.Control.RightToLeft property value of the control's
      //     container changes.
      //
      // Parameters:
      //   e:
      //     An System.EventArgs that contains the event data.
     // [EditorBrowsable(EditorBrowsableState.Advanced)]
      // protected virtual void OnParentRightToLeftChanged(EventArgs e);
      //
      // Summary:
      //     Raises the System.Windows.Forms.Control.VisibleChanged event when the System.Windows.Forms.Control.Visible
      //     property value of the control's container changes.
      //
      // Parameters:
      //   e:
      //     An System.EventArgs that contains the event data.
     // [EditorBrowsable(EditorBrowsableState.Advanced)]
      // protected virtual void OnParentVisibleChanged(EventArgs e);
      //
      // Summary:
      //     Raises the System.Windows.Forms.Control.PreviewKeyDown event.
      //
      // Parameters:
      //   e:
      //     A System.Windows.Forms.PreviewKeyDownEventArgs that contains the event data.
     // [EditorBrowsable(EditorBrowsableState.Advanced)]
      // protected virtual void OnPreviewKeyDown(PreviewKeyDownEventArgs e);
      //
      // Summary:
      //     Raises the System.Windows.Forms.Control.Paint event.
      //
      // Parameters:
      //   e:
      //     A System.Windows.Forms.PaintEventArgs that contains the event data.
      //
      // Exceptions:
      //   System.ArgumentNullException:
      //     The e parameter is null.
     // [EditorBrowsable(EditorBrowsableState.Advanced)]
      // protected virtual void OnPrint(PaintEventArgs e);
      //
      // Summary:
      //     Raises the System.Windows.Forms.Control.QueryContinueDrag event.
      //
      // Parameters:
      //   qcdevent:
      //     A System.Windows.Forms.QueryContinueDragEventArgs that contains the event
      //     data.
     // [EditorBrowsable(EditorBrowsableState.Advanced)]
      // protected virtual void OnQueryContinueDrag(QueryContinueDragEventArgs qcdevent);
      //
      // Summary:
      //     Raises the System.Windows.Forms.Control.RegionChanged event.
      //
      // Parameters:
      //   e:
      //     An System.EventArgs that contains the event data.
     // [EditorBrowsable(EditorBrowsableState.Advanced)]
      // protected virtual void OnRegionChanged(EventArgs e);
      //
      // Summary:
      //     Raises the System.Windows.Forms.Control.Resize event.
      //
      // Parameters:
      //   e:
      //     An System.EventArgs that contains the event data.
     // [EditorBrowsable(EditorBrowsableState.Advanced)]
      // protected virtual void OnResize(EventArgs e);
      //
      // Summary:
      //     Raises the System.Windows.Forms.Control.RightToLeftChanged event.
      //
      // Parameters:
      //   e:
      //     An System.EventArgs that contains the event data.
     // [EditorBrowsable(EditorBrowsableState.Advanced)]
      // protected virtual void OnRightToLeftChanged(EventArgs e);
      //
      // Summary:
      //     Raises the System.Windows.Forms.Control.SizeChanged event.
      //
      // Parameters:
      //   e:
      //     An System.EventArgs that contains the event data.
     // [EditorBrowsable(EditorBrowsableState.Advanced)]
      // protected virtual void OnSizeChanged(EventArgs e);
      //
      // Summary:
      //     Raises the System.Windows.Forms.Control.StyleChanged event.
      //
      // Parameters:
      //   e:
      //     An System.EventArgs that contains the event data.
     // [EditorBrowsable(EditorBrowsableState.Advanced)]
      // protected virtual void OnStyleChanged(EventArgs e);
      //
      // Summary:
      //     Raises the System.Windows.Forms.Control.SystemColorsChanged event.
      //
      // Parameters:
      //   e:
      //     An System.EventArgs that contains the event data.
     // [EditorBrowsable(EditorBrowsableState.Advanced)]
      // protected virtual void OnSystemColorsChanged(EventArgs e);
      //
      // Summary:
      //     Raises the System.Windows.Forms.Control.TabIndexChanged event.
      //
      // Parameters:
      //   e:
      //     An System.EventArgs that contains the event data.
     // [EditorBrowsable(EditorBrowsableState.Advanced)]
      // protected virtual void OnTabIndexChanged(EventArgs e);
      //
      // Summary:
      //     Raises the System.Windows.Forms.Control.TabStopChanged event.
      //
      // Parameters:
      //   e:
      //     An System.EventArgs that contains the event data.
     // [EditorBrowsable(EditorBrowsableState.Advanced)]
      // protected virtual void OnTabStopChanged(EventArgs e);
      //
      // Summary:
      //     Raises the System.Windows.Forms.Control.TextChanged event.
      //
      // Parameters:
      //   e:
      //     An System.EventArgs that contains the event data.
     // [EditorBrowsable(EditorBrowsableState.Advanced)]
      // protected virtual void OnTextChanged(EventArgs e);
      //
      // Summary:
      //     Raises the System.Windows.Forms.Control.Validated event.
      //
      // Parameters:
      //   e:
      //     An System.EventArgs that contains the event data.
     // [EditorBrowsable(EditorBrowsableState.Advanced)]
      // protected virtual void OnValidated(EventArgs e);
      //
      // Summary:
      //     Raises the System.Windows.Forms.Control.Validating event.
      //
      // Parameters:
      //   e:
      //     A System.ComponentModel.CancelEventArgs that contains the event data.
     // [EditorBrowsable(EditorBrowsableState.Advanced)]
      // protected virtual void OnValidating(CancelEventArgs e);
      //
      // Summary:
      //     Raises the System.Windows.Forms.Control.VisibleChanged event.
      //
      // Parameters:
      //   e:
      //     An System.EventArgs that contains the event data.
     // [EditorBrowsable(EditorBrowsableState.Advanced)]
      // protected virtual void OnVisibleChanged(EventArgs e);

    }
  }
}
