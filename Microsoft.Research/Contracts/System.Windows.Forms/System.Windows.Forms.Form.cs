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
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
  // Summary:
  //     Represents a window or dialog box that makes up an application's user interface.
 //[Designer("System.Windows.Forms.Design.FormDocumentDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(IRootDesigner))]
 //[ToolboxItemFilter("System.Windows.Forms.Control.TopLevel")]
 //[DesignerCategory("Form")]
 //[DefaultEvent("Load")]
 //[ToolboxItem(false)]
 //[DesignTimeVisible(false)]
 //[ComVisible(true)]
 //[InitializationEvent("Load")]
 //[ClassInterface(ClassInterfaceType.AutoDispatch)]
  public class Form //: ContainerControl
  {
    // Summary:
    //     Initializes a new instance of the System.Windows.Forms.Form class.
    //public Form();

    // Summary:
    //     Gets or sets the button on the form that is clicked when the user presses
    //     the ENTER key.
    //
    // Returns:
    //     An System.Windows.Forms.IButtonControl that represents the button to use
    //     as the accept button for the form.
   //[DefaultValue("")]
    //public IButtonControl AcceptButton { get; set; }
    //
    // Summary:
    //     Gets the currently active form for this application.
    //
    // Returns:
    //     A System.Windows.Forms.Form that represents the currently active form, or
    //     null if there is no active form.
    //public static Form ActiveForm { get; }
    //
    // Summary:
    //     Gets the currently active multiple-document interface (MDI) child window.
    //
    // Returns:
    //     Returns a System.Windows.Forms.Form that represents the currently active
    //     MDI child window, or null if there are currently no child windows present.
   //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
   //[Browsable(false)]
    //public Form ActiveMdiChild { get; }
    ////
    // Summary:
    //     Gets or sets a value indicating whether the opacity of the form can be adjusted.
    //
    // Returns:
    //     true if the opacity of the form can be changed; otherwise, false.
   //[Browsable(false)]
   //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //public bool AllowTransparency { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether the form adjusts its size to fit
    //     the height of the font used on the form and scales its controls.
    //
    // Returns:
    //     true if the form will automatically scale itself and its controls based on
    //     the current font assigned to the form; otherwise, false. The default is true.
   //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
   //[Obsolete("This property has been deprecated. Use the AutoScaleMode property instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
   //[EditorBrowsable(EditorBrowsableState.Never)]
   //[Browsable(false)]
    //public bool AutoScale { get; set; }
    //
    // Summary:
    //     Gets or sets the base size used for autoscaling of the form.
    //
    // Returns:
    //     A System.Drawing.Size that represents the base size that this form uses for
    //     autoscaling.
   //[Localizable(true)]
   //[EditorBrowsable(EditorBrowsableState.Never)]
   //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
   //[Browsable(false)]
    //public virtual Size AutoScaleBaseSize { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether the form enables autoscrolling.
    //
    // Returns:
    //     true to enable autoscrolling on the form; otherwise, false. The default is
    //     false.
   //[Localizable(true)]
    //public override bool AutoScroll { get; set; }
    //
    // Summary:
    //     Resize the form according to the setting of System.Windows.Forms.Form.AutoSizeMode.
    //
    // Returns:
    //     true if the form will automatically resize; false if it must be manually
    //     resized.
   //[EditorBrowsable(EditorBrowsableState.Always)]
   //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
   //[Browsable(true)]
    //public override bool AutoSize { get; set; }
    //
    // Summary:
    //     Gets or sets the mode by which the form automatically resizes itself.
    //
    // Returns:
    //     An System.Windows.Forms.AutoSizeMode enumerated value. The default is System.Windows.Forms.AutoSizeMode.GrowOnly.
    //
    // Exceptions:
    //   System.ComponentModel.InvalidEnumArgumentException:
    //     The value is not a valid System.Windows.Forms.AutoSizeMode value.
   //[Browsable(true)]
   //[Localizable(true)]
    //public AutoSizeMode AutoSizeMode { get; set; }
    //
    //
    // Returns:
    //     An System.Windows.Forms.AutoValidate enumerated value that indicates whether
    //     contained controls are implicitly validated on focus change. The default
    //     is System.Windows.Forms.AutoValidate.Inherit.
   //[EditorBrowsable(EditorBrowsableState.Always)]
   //[Browsable(true)]
    //public override AutoValidate AutoValidate { get; set; }
    //
    //
    // Returns:
    //     A System.Drawing.Color that represents the background color of the control.
    //     The default is the value of the System.Windows.Forms.Control.DefaultBackColor
    //     property.
    //public override Color BackColor { get; set; }
    //
    // Summary:
    //     Gets or sets the button control that is clicked when the user presses the
    //     ESC key.
    //
    // Returns:
    //     An System.Windows.Forms.IButtonControl that represents the cancel button
    //     for the form.
   //[DefaultValue("")]
    //public IButtonControl CancelButton { get; set; }
    //
    // Summary:
    //     Gets or sets the size of the client area of the form.
    //
    // Returns:
    //     A System.Drawing.Size that represents the size of the form's client area.
   //[Localizable(true)]
   //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    //public Size ClientSize { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether a control box is displayed in the
    //     caption bar of the form.
    //
    // Returns:
    //     true if the form displays a control box in the upper left corner of the form;
    //     otherwise, false. The default is true.
   //[DefaultValue(true)]
    //public bool ControlBox { get; set; }
    //
    //// protected override CreateParams CreateParams { get; }
    //
    // Summary:
    //     Gets the default Input Method Editor (IME) mode supported by the control.
    //
    // Returns:
    //     One of the System.Windows.Forms.ImeMode values.
    // protected override ImeMode DefaultImeMode { get; }
    //
    //
    // Returns:
    //     The default System.Drawing.Size of the control.
    // protected override Size DefaultSize { get; }
    //
    // Summary:
    //     Gets or sets the size and location of the form on the Windows desktop.
    //
    // Returns:
    //     A System.Drawing.Rectangle that represents the bounds of the form on the
    //     Windows desktop using desktop coordinates.
   //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
   //[Browsable(false)]
    //public Rectangle DesktopBounds { get; set; }
    //
    // Summary:
    //     Gets or sets the location of the form on the Windows desktop.
    //
    // Returns:
    //     A System.Drawing.Point that represents the location of the form on the desktop.
   //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
   //[Browsable(false)]
    //public Point DesktopLocation { get; set; }
    //
    // Summary:
    //     Gets or sets the dialog result for the form.
    //
    // Returns:
    //     A System.Windows.Forms.DialogResult that represents the result of the form
    //     when used as a dialog box.
    //
    // Exceptions:
    //   System.ComponentModel.InvalidEnumArgumentException:
    //     The value specified is outside the range of valid values.
   //[Browsable(false)]
   //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //public DialogResult DialogResult { get; set; }
    //
    // Summary:
    //     Gets or sets the border style of the form.
    //
    // Returns:
    //     A System.Windows.Forms.FormBorderStyle that represents the style of border
    //     to display for the form. The default is FormBorderStyle.Sizable.
    //
    // Exceptions:
    //   System.ComponentModel.InvalidEnumArgumentException:
    //     The value specified is outside the range of valid values.
   //[DispId(-504)]
    //public FormBorderStyle FormBorderStyle { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether a Help button should be displayed
    //     in the caption box of the form.
    //
    // Returns:
    //     true to display a Help button in the form's caption bar; otherwise, false.
    //     The default is false.
   //[DefaultValue(false)]
    //public bool HelpButton { get; set; }
    //
    // Summary:
    //     Gets or sets the icon for the form.
    //
    // Returns:
    //     An System.Drawing.Icon that represents the icon for the form.
   //[AmbientValue("")]
   //[Localizable(true)]
    //public Icon Icon { get; set; }
    //
    // Summary:
    //     Gets a value indicating whether the form is a multiple-document interface
    //     (MDI) child form.
    //
    // Returns:
    //     true if the form is an MDI child form; otherwise, false.
   //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
   //[Browsable(false)]
    //public bool IsMdiChild { get; }
    //
    // Summary:
    //     Gets or sets a value indicating whether the form is a container for multiple-document
    //     interface (MDI) child forms.
    //
    // Returns:
    //     true if the form is a container for MDI child forms; otherwise, false. The
    //     default is false.
   //[DefaultValue(false)]
    //public bool IsMdiContainer { get; set; }
    //
    // Summary:
    //     Gets a value indicating whether the form can use all windows and user input
    //     events without restriction.
    //
    // Returns:
    //     true if the form has restrictions; otherwise, false. The default is true.
   //[Browsable(false)]
   //[EditorBrowsable(EditorBrowsableState.Advanced)]
    //public bool IsRestrictedWindow { get; }
    //
    // Summary:
    //     Gets or sets a value indicating whether the form will receive key events
    //     before the event is passed to the control that has focus.
    //
    // Returns:
    //     true if the form will receive all key events; false if the currently selected
    //     control on the form receives key events. The default is false.
   //[DefaultValue(false)]
    //public bool KeyPreview { get; set; }
    //
   //[SettingsBindable(true)]
    //public Point Location { get; set; }
    //
    // Summary:
    //     Gets or sets the primary menu container for the form.
    //
    // Returns:
    //     A System.Windows.Forms.MenuStrip that represents the container for the menu
    //     structure of the form. The default is null.
   //[DefaultValue("")]
   //[TypeConverter(typeof(ReferenceConverter))]
    //public MenuStrip MainMenuStrip { get; set; }
    //
   //[Browsable(false)]
   //[EditorBrowsable(EditorBrowsableState.Never)]
    //public Padding Margin { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether the Maximize button is displayed
    //     in the caption bar of the form.
    //
    // Returns:
    //     true to display a Maximize button for the form; otherwise, false. The default
    //     is true.
   //[DefaultValue(true)]
    //public bool MaximizeBox { get; set; }
    //
    // Summary:
    //     Gets and sets the size of the form when it is maximized.
    //
    // Returns:
    //     A System.Drawing.Rectangle that represents the bounds of the form when it
    //     is maximized.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     The value of the System.Drawing.Rectangle.Top property is greater than the
    //     height of the form.-or- The value of the System.Drawing.Rectangle.Left property
    //     is greater than the width of the form.
    // protected Rectangle MaximizedBounds { get; set; }
    //
    // Summary:
    //     Gets the maximum size the form can be resized to.
    //
    // Returns:
    //     A System.Drawing.Size that represents the maximum size for the form.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     The values of the height or width within the System.Drawing.Size object are
    //     less than zero.
   //[Localizable(true)]
   //[RefreshProperties(RefreshProperties.Repaint)]
   //[DefaultValue(typeof(Size), "0, 0")]
    //public override Size MaximumSize { get; set; }
    //
    // Summary:
    //     Gets an array of forms that represent the multiple-document interface (MDI)
    //     child forms that are parented to this form.
    //
    // Returns:
    //     An array of System.Windows.Forms.Form objects, each of which identifies one
    //     of this form's MDI child forms.
   //[Browsable(false)]
   //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //public Form[] MdiChildren { get; }
    //
    // Summary:
    //     Gets or sets the current multiple-document interface (MDI) parent form of
    //     this form.
    //
    // Returns:
    //     A System.Windows.Forms.Form that represents the MDI parent form.
    //
    // Exceptions:
    //   System.Exception:
    //     The System.Windows.Forms.Form assigned to this property is not marked as
    //     an MDI container.-or- The System.Windows.Forms.Form assigned to this property
    //     is both a child and an MDI container form.-or- The System.Windows.Forms.Form
    //     assigned to this property is located on a different thread.
   //[Browsable(false)]
   //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //public Form MdiParent { get; set; }
    //
    // Summary:
    //     Gets or sets the System.Windows.Forms.MainMenu that is displayed in the form.
    //
    // Returns:
    //     A System.Windows.Forms.MainMenu that represents the menu to display in the
    //     form.
   //[Browsable(false)]
   //[TypeConverter(typeof(ReferenceConverter))]
   //[DefaultValue("")]
    //public MainMenu Menu { get; set; }
    //
    // Summary:
    //     Gets the merged menu for the form.
    //
    // Returns:
    //     A System.Windows.Forms.MainMenu that represents the merged menu of the form.
   //[Browsable(false)]
   //[EditorBrowsable(EditorBrowsableState.Advanced)]
   //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    //public MainMenu MergedMenu { get; }
    //
    // Summary:
    //     Gets or sets a value indicating whether the Minimize button is displayed
    //     in the caption bar of the form.
    //
    // Returns:
    //     true to display a Minimize button for the form; otherwise, false. The default
    //     is true.
   //[DefaultValue(true)]
    //public bool MinimizeBox { get; set; }
    //
    // Summary:
    //     Gets or sets the minimum size the form can be resized to.
    //
    // Returns:
    //     A System.Drawing.Size that represents the minimum size for the form.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     The values of the height or width within the System.Drawing.Size object are
    //     less than zero.
   //[RefreshProperties(RefreshProperties.Repaint)]
   //[Localizable(true)]
   // public override Size MinimumSize { get; set; }
   // //
   // // Summary:
   // //     Gets a value indicating whether this form is displayed modally.
   // //
   // // Returns:
   // //     true if the form is displayed modally; otherwise, false.
   ////[Browsable(false)]
   ////[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
   // public bool Modal { get; }
   // //
   // // Summary:
   // //     Gets or sets the opacity level of the form.
   // //
   // // Returns:
   // //     The level of opacity for the form. The default is 1.00.
   ////[DefaultValue(1)]
   ////[TypeConverter(typeof(OpacityConverter))]
   // public double Opacity { get; set; }
   // //
   // // Summary:
   // //     Gets an array of System.Windows.Forms.Form objects that represent all forms
   // //     that are owned by this form.
   // //
   // // Returns:
   // //     A System.Windows.Forms.Form array that represents the owned forms for this
   // //     form.
   ////[Browsable(false)]
   ////[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
   // public Form[] OwnedForms { get; }
   // //
   // // Summary:
   // //     Gets or sets the form that owns this form.
   // //
   // // Returns:
   // //     A System.Windows.Forms.Form that represents the form that is the owner of
   // //     this form.
   // //
   // // Exceptions:
   // //   System.Exception:
   // //     A top-level window cannot have an owner.
   ////[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
   ////[Browsable(false)]
   // public Form Owner { get; set; }
   // //
   // // Summary:
   // //     Gets the location and size of the form in its normal window state.
   // //
   // // Returns:
   // //     A System.Drawing.Rectangle that contains the location and size of the form
   // //     in the normal window state.
   ////[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
   ////[Browsable(false)]
   // public Rectangle RestoreBounds { get; }
   // //
   // // Summary:
   // //     Gets or sets a value indicating whether right-to-left mirror placement is
   // //     turned on.
   // //
   // // Returns:
   // //     true if right-to-left mirror placement is turned on; otherwise, false for
   // //     standard child control placement. The default is false.
   ////[DefaultValue(false)]
   ////[Localizable(true)]
   // public virtual bool RightToLeftLayout { get; set; }
   // //
   // // Summary:
   // //     Gets or sets a value indicating whether an icon is displayed in the caption
   // //     bar of the form.
   // //
   // // Returns:
   // //     true if the form displays an icon in the caption bar; otherwise, false. The
   // //     default is true.
   ////[DefaultValue(true)]
   // public bool ShowIcon { get; set; }
   // //
   // // Summary:
   // //     Gets or sets a value indicating whether the form is displayed in the Windows
   // //     taskbar.
   // //
   // // Returns:
   // //     true to display the form in the Windows taskbar at run time; otherwise, false.
   // //     The default is true.
   ////[DefaultValue(true)]
   // public bool ShowInTaskbar { get; set; }
   // //
   // // Summary:
   // //     Gets a value indicating whether the window will be activated when it is shown.
   // //
   // // Returns:
   // //     True if the window will not be activated when it is shown; otherwise, false.
   // //     The default is false.
   ////[Browsable(false)]
   // // protected virtual bool ShowWithoutActivation { get; }
   // //
   // // Summary:
   // //     Gets or sets the size of the form.
   // //
   // // Returns:
   // //     A System.Drawing.Size that represents the size of the form.
   ////[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
   ////[Localizable(false)]
   // public Size Size { get; set; }
   // //
   // // Summary:
   // //     Gets or sets the style of the size grip to display in the lower-right corner
   // //     of the form.
   // //
   // // Returns:
   // //     A System.Windows.Forms.SizeGripStyle that represents the style of the size
   // //     grip to display. The default is System.Windows.Forms.SizeGripStyle.Auto
   // //
   // // Exceptions:
   // //   System.ComponentModel.InvalidEnumArgumentException:
   // //     The value specified is outside the range of valid values.
   // public SizeGripStyle SizeGripStyle { get; set; }
   // //
   // // Summary:
   // //     Gets or sets the starting position of the form at run time.
   // //
   // // Returns:
   // //     A System.Windows.Forms.FormStartPosition that represents the starting position
   // //     of the form.
   // //
   // // Exceptions:
   // //   System.ComponentModel.InvalidEnumArgumentException:
   // //     The value specified is outside the range of valid values.
   ////[Localizable(true)]
   // public FormStartPosition StartPosition { get; set; }
   // //
   // // Summary:
   // //     Gets or sets the tab order of the control within its container.
   // //
   // // Returns:
   // //     An System.Int32 containing the index of the control within the set of controls
   // //     within its container that is included in the tab order.
   ////[Browsable(false)]
   ////[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
   ////[EditorBrowsable(EditorBrowsableState.Never)]
   // public int TabIndex { get; set; }
   // //
   ////[DefaultValue(true)]
   ////[Browsable(false)]
   ////[EditorBrowsable(EditorBrowsableState.Never)]
   ////[DispId(-516)]
   // public bool TabStop { get; set; }
   // //
   // //
   // // Returns:
   // //     The text associated with this control.
   ////[SettingsBindable(true)]
   // public override string Text { get; set; }
   // //
   // // Summary:
   // //     Gets or sets a value indicating whether to display the form as a top-level
   // //     window.
   // //
   // // Returns:
   // //     true to display the form as a top-level window; otherwise, false. The default
   // //     is true.
   // //
   // // Exceptions:
   // //   System.Exception:
   // //     A Multiple-document interface (MDI) parent form must be a top-level window.
   ////[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
   ////[Browsable(false)]
   ////[EditorBrowsable(EditorBrowsableState.Advanced)]
   // public bool TopLevel { get; set; }
   // //
   // // Summary:
   // //     Gets or sets a value indicating whether the form should be displayed as a
   // //     topmost form.
   // //
   // // Returns:
   // //     true to display the form as a topmost form; otherwise, false. The default
   // //     is false.
   ////[DefaultValue(false)]
   // public bool TopMost { get; set; }
   // //
   // // Summary:
   // //     Gets or sets the color that will represent transparent areas of the form.
   // //
   // // Returns:
   // //     A System.Drawing.Color that represents the color to display transparently
   // //     on the form.
   // public Color TransparencyKey { get; set; }
   // //
   // // Summary:
   // //     Gets or sets the form's window state.
   // //
   // // Returns:
   // //     A System.Windows.Forms.FormWindowState that represents the window state of
   // //     the form. The default is FormWindowState.Normal.
   // //
   // // Exceptions:
   // //   System.ComponentModel.InvalidEnumArgumentException:
   // //     The value specified is outside the range of valid values.
   // public FormWindowState WindowState { get; set; }

   // // Summary:
   // //     Occurs when the form is activated in code or by the user.
   // public event EventHandler Activated;
   // //
   ////[EditorBrowsable(EditorBrowsableState.Always)]
   ////[Browsable(true)]
   // public event EventHandler AutoSizeChanged;
   // //
   ////[Browsable(true)]
   ////[EditorBrowsable(EditorBrowsableState.Always)]
   // public event EventHandler AutoValidateChanged;
   // //
   // // Summary:
   // //     Occurs when the form is closed.
   ////[EditorBrowsable(EditorBrowsableState.Never)]
   ////[Browsable(false)]
   // public event EventHandler Closed;
   // //
   // // Summary:
   // //     Occurs when the form is closing.
   ////[EditorBrowsable(EditorBrowsableState.Never)]
   ////[Browsable(false)]
   // public event CancelEventHandler Closing;
   // //
   // // Summary:
   // //     Occurs when the form loses focus and is no longer the active form.
   // public event EventHandler Deactivate;
   // //
   // // Summary:
   // //     Occurs after the form is closed.
   // public event FormClosedEventHandler FormClosed;
   // //
   // // Summary:
   // //     Occurs before the form is closed.
   // public event FormClosingEventHandler FormClosing;
   // //
   // // Summary:
   // //     Occurs when the Help button is clicked.
   ////[EditorBrowsable(EditorBrowsableState.Always)]
   ////[Browsable(true)]
   // public event CancelEventHandler HelpButtonClicked;
   // //
   // // Summary:
   // //     Occurs after the input language of the form has changed.
   // public event InputLanguageChangedEventHandler InputLanguageChanged;
   // //
   // // Summary:
   // //     Occurs when the user attempts to change the input language for the form.
   // public event InputLanguageChangingEventHandler InputLanguageChanging;
   // //
   // // Summary:
   // //     Occurs before a form is displayed for the first time.
   // public event EventHandler Load;
   // //
   ////[Browsable(false)]
   ////[EditorBrowsable(EditorBrowsableState.Never)]
   // public event EventHandler MarginChanged;
   // //
   // // Summary:
   // //     Occurs when the value of the System.Windows.Forms.Form.MaximizedBounds property
   // //     has changed.
   // public event EventHandler MaximizedBoundsChanged;
   // //
   // // Summary:
   // //     Occurs when the value of the System.Windows.Forms.Form.MaximumSize property
   // //     has changed.
   // public event EventHandler MaximumSizeChanged;
   // //
   // // Summary:
   // //     Occurs when a multiple-document interface (MDI) child form is activated or
   // //     closed within an MDI application.
   // public event EventHandler MdiChildActivate;
   // //
   // // Summary:
   // //     Occurs when the menu of a form loses focus.
   ////[Browsable(false)]
   // public event EventHandler MenuComplete;
   // //
   // // Summary:
   // //     Occurs when the menu of a form receives focus.
   ////[Browsable(false)]
   // public event EventHandler MenuStart;
   // //
   // // Summary:
   // //     Occurs when the value of the System.Windows.Forms.Form.MinimumSize property
   // //     has changed.
   // public event EventHandler MinimumSizeChanged;
   // //
   // // Summary:
   // //     Occurs when a form enters resizing mode.
   // public event EventHandler ResizeBegin;
   // //
   // // Summary:
   // //     Occurs when a form exits resizing mode.
   // public event EventHandler ResizeEnd;
   // //
   // // Summary:
   // //     Occurs after the value of the System.Windows.Forms.Form.RightToLeftLayout
   // //     property changes.
   // public event EventHandler RightToLeftLayoutChanged;
   // //
   // // Summary:
   // //     Occurs whenever the form is first displayed.
   // public event EventHandler Shown;
   // //
   // // Summary:
   // //     Occurs when the value of the System.Windows.Forms.Form.TabIndex property
   // //     changes.
   ////[Browsable(false)]
   ////[EditorBrowsable(EditorBrowsableState.Never)]
   // public event EventHandler TabIndexChanged;
   // //
   ////[EditorBrowsable(EditorBrowsableState.Never)]
   ////[Browsable(false)]
   // public event EventHandler TabStopChanged;

   // // Summary:
   // //     Activates the form and gives it focus.
   // public void Activate();
   // //
   // // Summary:
   // //     Activates the MDI child of a form.
   // //
   // // Parameters:
   // //   form:
   // //     The child form to activate.
   // // protected void ActivateMdiChild(Form form);
   // //
   // // Summary:
   // //     Adds an owned form to this form.
   // //
   // // Parameters:
   // //   ownedForm:
   // //     The System.Windows.Forms.Form that this form will own.
   // public void AddOwnedForm(Form ownedForm);
   // //
   ////[EditorBrowsable(EditorBrowsableState.Advanced)]
   // // protected override void AdjustFormScrollbars(bool displayScrollbars);
   // //
   // // Summary:
   // //     Resizes the form according to the current value of the System.Windows.Forms.Form.AutoScaleBaseSize
   // //     property and the size of the current font.
   ////[EditorBrowsable(EditorBrowsableState.Never)]
   ////[Obsolete("This method has been deprecated. Use the ApplyAutoScaling method instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
   // // protected void ApplyAutoScaling();
   // //
   // // Summary:
   // //     Centers the position of the form within the bounds of the parent form.
   // // protected void CenterToParent();
   // //
   // // Summary:
   // //     Centers the form on the current screen.
   // // protected void CenterToScreen();
   // //
   // // Summary:
   // //     Closes the form.
   // //
   // // Exceptions:
   // //   System.InvalidOperationException:
   // //     The form was closed while a handle was being created.
   // //
   // //   System.ObjectDisposedException:
   // //     You cannot call this method from the System.Windows.Forms.Form.Activated
   // //     event when System.Windows.Forms.Form.WindowState is set to System.Windows.Forms.FormWindowState.Maximized.
   // public void Close();
   // //
   // //
   // // Returns:
   // //     A new instance of System.Windows.Forms.Control.ControlCollection assigned
   // //     to the control.
   ////[EditorBrowsable(EditorBrowsableState.Advanced)]
   // // protected override Control.ControlCollection CreateControlsInstance();
   // //
   // // Summary:
   // //     Creates the handle for the form. If a derived class overrides this function,
   // //     it must call the base implementation.
   ////[EditorBrowsable(EditorBrowsableState.Advanced)]
   // // protected override void CreateHandle();
   // //
   // //
   // // Parameters:
   // //   m:
   // //     The Windows System.Windows.Forms.Message to process.
   ////[EditorBrowsable(EditorBrowsableState.Advanced)]
   // // protected override void DefWndProc(ref Message m);
   // //
   // // Summary:
   // //     Disposes of the resources (other than memory) used by the System.Windows.Forms.Form.
   // //
   // // Parameters:
   // //   disposing:
   // //     true to release both managed and unmanaged resources; false to release only
   // //     unmanaged resources.
   // // protected override void Dispose(bool disposing);
   // //
   // // Summary:
   // //     Gets the size when autoscaling the form based on a specified font.
   // //
   // // Parameters:
   // //   font:
   // //     A System.Drawing.Font representing the font to determine the autoscaled base
   // //     size of the form.
   // //
   // // Returns:
   // //     A System.Drawing.SizeF representing the autoscaled size of the form.
   ////[Obsolete("This method has been deprecated. Use the AutoScaleDimensions property instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
   ////[EditorBrowsable(EditorBrowsableState.Never)]
   // public static SizeF GetAutoScaleSize(Font font);
   // //
   // //
   // // Parameters:
   // //   bounds:
   // //     A System.Drawing.Rectangle that specifies the area for which to retrieve
   // //     the display bounds.
   // //
   // //   factor:
   // //     The height and width of the control's bounds.
   // //
   // //   specified:
   // //     One of the values of System.Windows.Forms.BoundsSpecified that specifies
   // //     the bounds of the control to use when defining its size and position.
   // //
   // // Returns:
   // //     A System.Drawing.Rectangle representing the bounds within which the control
   // //     is scaled.
   ////[EditorBrowsable(EditorBrowsableState.Advanced)]
   // // protected override Rectangle GetScaledBounds(Rectangle bounds, SizeF factor, BoundsSpecified specified);
   // //
   // // Summary:
   // //     Arranges the multiple-document interface (MDI) child forms within the MDI
   // //     parent form.
   // //
   // // Parameters:
   // //   value:
   // //     One of the System.Windows.Forms.MdiLayout values that defines the layout
   // //     of MDI child forms.
   // public void LayoutMdi(MdiLayout value);
   // //
   // // Summary:
   // //     Raises the System.Windows.Forms.Form.Activated event.
   // //
   // // Parameters:
   // //   e:
   // //     An System.EventArgs that contains the event data.
   ////[EditorBrowsable(EditorBrowsableState.Advanced)]
   // // protected virtual void OnActivated(EventArgs e);
   // //
   // // Summary:
   // //     Raises the System.Windows.Forms.Control.BackgroundImageChanged event.
   // //
   // // Parameters:
   // //   e:
   // //     An System.EventArgs that contains the data.
   // // protected override void OnBackgroundImageChanged(EventArgs e);
   // //
   // // Summary:
   // //     Raises the System.Windows.Forms.Control.BackgroundImageLayoutChanged event.
   // //
   // // Parameters:
   // //   e:
   // //     An System.EventArgs that contains the event data.
   // // protected override void OnBackgroundImageLayoutChanged(EventArgs e);
   // //
   // // Summary:
   // //     Raises the System.Windows.Forms.Form.Closed event.
   // //
   // // Parameters:
   // //   e:
   // //     The System.EventArgs that contains the event data.
   ////[EditorBrowsable(EditorBrowsableState.Advanced)]
   // // protected virtual void OnClosed(EventArgs e);
   // //
   // // Summary:
   // //     Raises the System.Windows.Forms.Form.Closing event.
   // //
   // // Parameters:
   // //   e:
   // //     A System.ComponentModel.CancelEventArgs that contains the event data.
   ////[EditorBrowsable(EditorBrowsableState.Advanced)]
   // // protected virtual void OnClosing(CancelEventArgs e);
   // //
   // // Summary:
   // //     Raises the CreateControl event.
   ////[EditorBrowsable(EditorBrowsableState.Advanced)]
   // // protected override void OnCreateControl();
   // //
   // // Summary:
   // //     Raises the System.Windows.Forms.Form.Deactivate event.
   // //
   // // Parameters:
   // //   e:
   // //     The System.EventArgs that contains the event data.
   ////[EditorBrowsable(EditorBrowsableState.Advanced)]
   // // protected virtual void OnDeactivate(EventArgs e);
   // //
   // //
   // // Parameters:
   // //   e:
   // //     An System.EventArgs that contains the event data.
   ////[EditorBrowsable(EditorBrowsableState.Advanced)]
   // // protected override void OnEnabledChanged(EventArgs e);
   // //
   // // Summary:
   // //     Raises the System.Windows.Forms.Control.Enter event.
   // //
   // // Parameters:
   // //   e:
   // //     An System.EventArgs that contains the event data.
   ////[EditorBrowsable(EditorBrowsableState.Advanced)]
   // // protected override void OnEnter(EventArgs e);
   // //
   // //
   // // Parameters:
   // //   e:
   // //     An System.EventArgs that contains the event data.
   ////[EditorBrowsable(EditorBrowsableState.Advanced)]
   // // protected override void OnFontChanged(EventArgs e);
   // //
   // // Summary:
   // //     Raises the System.Windows.Forms.Form.FormClosed event.
   // //
   // // Parameters:
   // //   e:
   // //     A System.Windows.Forms.FormClosedEventArgs that contains the event data.
   ////[EditorBrowsable(EditorBrowsableState.Advanced)]
   // // protected virtual void OnFormClosed(FormClosedEventArgs e);
   // //
   // // Summary:
   // //     Raises the System.Windows.Forms.Form.FormClosing event.
   // //
   // // Parameters:
   // //   e:
   // //     A System.Windows.Forms.FormClosingEventArgs that contains the event data.
   ////[EditorBrowsable(EditorBrowsableState.Advanced)]
   // // protected virtual void OnFormClosing(FormClosingEventArgs e);
   // //
   // //
   // // Parameters:
   // //   e:
   // //     An System.EventArgs that contains the event data.
   ////[EditorBrowsable(EditorBrowsableState.Advanced)]
   // // protected override void OnHandleCreated(EventArgs e);
   // //
   // //
   // // Parameters:
   // //   e:
   // //     An System.EventArgs that contains the event data.
   ////[EditorBrowsable(EditorBrowsableState.Advanced)]
   // // protected override void OnHandleDestroyed(EventArgs e);
   // //
   // // Summary:
   // //     Raises the System.Windows.Forms.Form.HelpButtonClicked event.
   // //
   // // Parameters:
   // //   e:
   // //     A System.ComponentModel.CancelEventArgs that contains the event data.
   ////[EditorBrowsable(EditorBrowsableState.Advanced)]
   // // protected virtual void OnHelpButtonClicked(CancelEventArgs e);
   // //
   // // Summary:
   // //     Raises the System.Windows.Forms.Form.InputLanguageChanged event.
   // //
   // // Parameters:
   // //   e:
   // //     The System.Windows.Forms.InputLanguageChangedEventArgs that contains the
   // //     event data.
   ////[EditorBrowsable(EditorBrowsableState.Advanced)]
   // // protected virtual void OnInputLanguageChanged(InputLanguageChangedEventArgs e);
   // //
   // // Summary:
   // //     Raises the System.Windows.Forms.Form.InputLanguageChanging event.
   // //
   // // Parameters:
   // //   e:
   // //     The System.Windows.Forms.InputLanguageChangingEventArgs that contains the
   // //     event data.
   ////[EditorBrowsable(EditorBrowsableState.Advanced)]
   // // protected virtual void OnInputLanguageChanging(InputLanguageChangingEventArgs e);
   // //
   // // protected override void OnLayout(LayoutEventArgs levent);
   // //
   // // Summary:
   // //     Raises the System.Windows.Forms.Form.Load event.
   // //
   // // Parameters:
   // //   e:
   // //     An System.EventArgs that contains the event data.
   ////[EditorBrowsable(EditorBrowsableState.Advanced)]
   // // protected virtual void OnLoad(EventArgs e);
   // //
   // // Summary:
   // //     Raises the System.Windows.Forms.Form.MaximizedBoundsChanged event.
   // //
   // // Parameters:
   // //   e:
   // //     The System.EventArgs that contains the event data.
   ////[EditorBrowsable(EditorBrowsableState.Advanced)]
   // // protected virtual void OnMaximizedBoundsChanged(EventArgs e);
   // //
   // // Summary:
   // //     Raises the System.Windows.Forms.Form.MaximumSizeChanged event.
   // //
   // // Parameters:
   // //   e:
   // //     The System.EventArgs that contains the event data.
   ////[EditorBrowsable(EditorBrowsableState.Advanced)]
   // // protected virtual void OnMaximumSizeChanged(EventArgs e);
   // //
   // // Summary:
   // //     Raises the System.Windows.Forms.Form.MdiChildActivate event.
   // //
   // // Parameters:
   // //   e:
   // //     The System.EventArgs that contains the event data.
   ////[EditorBrowsable(EditorBrowsableState.Advanced)]
   // // protected virtual void OnMdiChildActivate(EventArgs e);
   // //
   // // Summary:
   // //     Raises the System.Windows.Forms.Form.MenuComplete event.
   // //
   // // Parameters:
   // //   e:
   // //     The System.EventArgs that contains the event data.
   ////[EditorBrowsable(EditorBrowsableState.Advanced)]
   // // protected virtual void OnMenuComplete(EventArgs e);
   // //
   // // Summary:
   // //     Raises the System.Windows.Forms.Form.MenuStart event.
   // //
   // // Parameters:
   // //   e:
   // //     The System.EventArgs that contains the event data.
   ////[EditorBrowsable(EditorBrowsableState.Advanced)]
   // // protected virtual void OnMenuStart(EventArgs e);
   // //
   // // Summary:
   // //     Raises the System.Windows.Forms.Form.MinimumSizeChanged event.
   // //
   // // Parameters:
   // //   e:
   // //     An System.EventArgs that contains the event data.
   ////[EditorBrowsable(EditorBrowsableState.Advanced)]
   // // protected virtual void OnMinimumSizeChanged(EventArgs e);
   // //
   // //
   // // Parameters:
   // //   e:
   // //     A System.Windows.Forms.PaintEventArgs that contains the event data.
   ////[EditorBrowsable(EditorBrowsableState.Advanced)]
   // // protected override void OnPaint(PaintEventArgs e);
   // //
   // //
   // // Parameters:
   // //   e:
   // //     An System.EventArgs that contains the event data.
   ////[EditorBrowsable(EditorBrowsableState.Advanced)]
   // // protected override void OnResize(EventArgs e);
   // //
   // // Summary:
   // //     Raises the System.Windows.Forms.Form.ResizeBegin event.
   // //
   // // Parameters:
   // //   e:
   // //     A System.EventArgs that contains the event data.
   ////[EditorBrowsable(EditorBrowsableState.Advanced)]
   // // protected virtual void OnResizeBegin(EventArgs e);
   // //
   // // Summary:
   // //     Raises the System.Windows.Forms.Form.ResizeEnd event.
   // //
   // // Parameters:
   // //   e:
   // //     A System.EventArgs that contains the event data.
   ////[EditorBrowsable(EditorBrowsableState.Advanced)]
   // // protected virtual void OnResizeEnd(EventArgs e);
   // //
   // // Summary:
   // //     Raises the System.Windows.Forms.Form.RightToLeftLayoutChanged event.
   // //
   // // Parameters:
   // //   e:
   // //     An System.EventArgs that contains the event data.
   ////[EditorBrowsable(EditorBrowsableState.Advanced)]
   // // protected virtual void OnRightToLeftLayoutChanged(EventArgs e);
   // //
   // // Summary:
   // //     Raises the System.Windows.Forms.Form.Shown event.
   // //
   // // Parameters:
   // //   e:
   // //     A System.EventArgs that contains the event data.
   ////[EditorBrowsable(EditorBrowsableState.Advanced)]
   // // protected virtual void OnShown(EventArgs e);
   // //
   // //
   // // Parameters:
   // //   e:
   // //     An System.EventArgs that contains the event data.
   ////[EditorBrowsable(EditorBrowsableState.Advanced)]
   // // protected override void OnStyleChanged(EventArgs e);
   // //
   // //
   // // Parameters:
   // //   e:
   // //     An System.EventArgs that contains the event data.
   ////[EditorBrowsable(EditorBrowsableState.Advanced)]
   // // protected override void OnTextChanged(EventArgs e);
   // //
   // // Summary:
   // //     Raises the System.Windows.Forms.Control.VisibleChanged event.
   // //
   // // Parameters:
   // //   e:
   // //     The System.EventArgs that contains the event data.
   ////[EditorBrowsable(EditorBrowsableState.Advanced)]
   // // protected override void OnVisibleChanged(EventArgs e);
   // //
   // // Summary:
   // //     Processes a command key.
   // //
   // // Parameters:
   // //   msg:
   // //     A System.Windows.Forms.Message, passed by reference, that represents the
   // //     Win32 message to process.
   // //
   // //   keyData:
   // //     One of the System.Windows.Forms.Keys values that represents the key to process.
   // //
   // // Returns:
   // //     true if the keystroke was processed and consumed by the control; otherwise,
   // //     false to allow further processing.
   // // protected override bool ProcessCmdKey(ref Message msg, Keys keyData);
   // //
   ////[EditorBrowsable(EditorBrowsableState.Advanced)]
   // // protected override bool ProcessDialogChar(char charCode);
   // //
   // // Summary:
   // //     Processes a dialog box key.
   // //
   // // Parameters:
   // //   keyData:
   // //     One of the System.Windows.Forms.Keys values that represents the key to process.
   // //
   // // Returns:
   // //     true if the keystroke was processed and consumed by the control; otherwise,
   // //     false to allow further processing.
   // // protected override bool ProcessDialogKey(Keys keyData);
   // //
   // //
   // // Parameters:
   // //   m:
   // //     A System.Windows.Forms.Message, passed by reference, that represents the
   // //     window message to process.
   // //
   // // Returns:
   // //     true if the message was processed by the control; otherwise, false.
   // // protected override bool ProcessKeyPreview(ref Message m);
   // //
   // // protected internal override bool ProcessMnemonic(char charCode);
   // //
   // //
   // // Parameters:
   // //   forward:
   // //     true to cycle forward through the controls in the System.Windows.Forms.ContainerControl;
   // //     otherwise, false.
   // //
   // // Returns:
   // //     true if a control is selected; otherwise, false.
   // // protected override bool ProcessTabKey(bool forward);
   // //
   // // Summary:
   // //     Removes an owned form from this form.
   // //
   // // Parameters:
   // //   ownedForm:
   // //     A System.Windows.Forms.Form representing the form to remove from the list
   // //     of owned forms for this form.
   // //public void RemoveOwnedForm(Form ownedForm);
   // //
   ////[EditorBrowsable(EditorBrowsableState.Advanced)]
   // // protected override void ScaleControl(SizeF factor, BoundsSpecified specified);
   // //
   // // Summary:
   // //     Performs scaling of the form.
   // //
   // // Parameters:
   // //   x:
   // //     Percentage to scale the form horizontally
   // //
   // //   y:
   // //     Percentage to scale the form vertically
   ////[EditorBrowsable(EditorBrowsableState.Never)]
   // // protected override void ScaleCore(float x, float y);
   // //
   // // Summary:
   // //     Selects this form, and optionally selects the next or previous control.
   // //
   // // Parameters:
   // //   directed:
   // //     If set to true that the active control is changed
   // //
   // //   forward:
   // //     If directed is true, then this controls the direction in which focus is moved.
   // //     If this is true, then the next control is selected; otherwise, the previous
   // //     control is selected.
   // // protected override void Select(bool directed, bool forward);
   // //
   // //
   // // Parameters:
   // //   x:
   // //     The new System.Windows.Forms.Control.Left property value of the control.
   // //
   // //   y:
   // //     The new System.Windows.Forms.Control.Top property value of the control.
   // //
   // //   width:
   // //     The new System.Windows.Forms.Control.Width property value of the control.
   // //
   // //   height:
   // //     The new System.Windows.Forms.Control.Height property value of the control.
   // //
   // //   specified:
   // //     A bitwise combination of the System.Windows.Forms.BoundsSpecified values.
   // //
   // //   x:
   // //     The new System.Windows.Forms.Control.Left property value of the control.
   // //
   // //   y:
   // //     The new System.Windows.Forms.Control.Top property value of the control.
   // //
   // //   width:
   // //     The new System.Windows.Forms.Control.Width property value of the control.
   // //
   // //   height:
   // //     The new System.Windows.Forms.Control.Height property value of the control.
   // //
   // //   specified:
   // //     A bitwise combination of the System.Windows.Forms.BoundsSpecified values.
   ////[EditorBrowsable(EditorBrowsableState.Advanced)]
   // // protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified);
   // //
   // // Summary:
   // //     Sets the client size of the form. This will adjust the bounds of the form
   // //     to make the client size the requested size.
   // //
   // // Parameters:
   // //   x:
   // //     The client area width, in pixels.
   // //
   // //   y:
   // //     The client area height, in pixels.
   // //
   // //   x:
   // //     The client area width, in pixels.
   // //
   // //   y:
   // //     The client area height, in pixels.
   ////[EditorBrowsable(EditorBrowsableState.Advanced)]
   // // protected override void SetClientSizeCore(int x, int y);
   // //
   // // Summary:
   // //     Sets the bounds of the form in desktop coordinates.
   // //
   // // Parameters:
   // //   x:
   // //     The x-coordinate of the form's location.
   // //
   // //   y:
   // //     The y-coordinate of the form's location.
   // //
   // //   width:
   // //     The width of the form.
   // //
   // //   height:
   // //     The height of the form.
   // //public void SetDesktopBounds(int x, int y, int width, int height);
   // //
   // // Summary:
   // //     Sets the location of the form in desktop coordinates.
   // //
   // // Parameters:
   // //   x:
   // //     The x-coordinate of the form's location.
   // //
   // //   y:
   // //     The y-coordinate of the form's location.
   // //public void SetDesktopLocation(int x, int y);
   // //
   // //
   // // Parameters:
   // //   value:
   // //     true to make the control visible; otherwise, false.
   ////[EditorBrowsable(EditorBrowsableState.Advanced)]
   // // protected override void SetVisibleCore(bool value);
   // //
   // // Summary:
   // //     Shows the form with the specified owner to the user.
   // //
   // // Parameters:
   // //   owner:
   // //     Any object that implements System.Windows.Forms.IWin32Window and represents
   // //     the top-level window that will own this form.
   // //
   // // Exceptions:
   // //   System.ArgumentException:
   // //     The form specified in the owner parameter is the same as the form being shown.
   // //public void Show(IWin32Window owner);
   // //
   // // Summary:
   // //     Shows the form as a modal dialog box with the currently active window set
   // //     as its owner.
   // //
   // // Returns:
   // //     One of the System.Windows.Forms.DialogResult values.
   // //
   // // Exceptions:
   // //   System.ArgumentException:
   // //     The form specified in the owner parameter is the same as the form being shown.
   // //
   // //   System.InvalidOperationException:
   // //     The form being shown is already visible.-or- The form being shown is disabled.-or-
   // //     The form being shown is not a top-level window.-or- The form being shown
   // //     as a dialog box is already a modal form.-or-The current process is not running
   // //     in user interactive mode (for more information, see System.Windows.Forms.SystemInformation.UserInteractive).
   // //public DialogResult ShowDialog();
   // //
   // // Summary:
   // //     Shows the form as a modal dialog box with the specified owner.
   // //
   // // Parameters:
   // //   owner:
   // //     Any object that implements System.Windows.Forms.IWin32Window that represents
   // //     the top-level window that will own the modal dialog box.
   // //
   // // Returns:
   // //     One of the System.Windows.Forms.DialogResult values.
   // //
   // // Exceptions:
   // //   System.ArgumentException:
   // //     The form specified in the owner parameter is the same as the form being shown.
   // //
   // //   System.InvalidOperationException:
   // //     The form being shown is already visible.-or- The form being shown is disabled.-or-
   // //     The form being shown is not a top-level window.-or- The form being shown
   // //     as a dialog box is already a modal form.-or-The current process is not running
   // //     in user interactive mode (for more information, see System.Windows.Forms.SystemInformation.UserInteractive).
   // //public DialogResult ShowDialog(IWin32Window owner);
   // //
   // // Summary:
   // //     Gets a string representing the current instance of the form.
   // //
   // // Returns:
   // //     A string consisting of the fully qualified name of the form object's class,
   // //     with the System.Windows.Forms.Form.Text property of the form appended to
   // //     the end. For example, if the form is derived from the class MyForm in the
   // //     MyNamespace namespace, and the System.Windows.Forms.Form.Text property is
   // //     set to Hello, World, this method will return MyNamespace.MyForm, Text: Hello,
   // //     World.
   // //public override string ToString();
   // //
   // // Summary:
   // //     Updates which button is the default button.
   // // protected override void UpdateDefaultButton();
   // //
   // //
   // // Returns:
   // //     true if all of the children validated successfully; otherwise, false. If
   // //     called from the System.Windows.Forms.Control.Validating or System.Windows.Forms.Control.Validated
   // //     event handlers, this method will always return false.
   ////[Browsable(true)]
   ////[EditorBrowsable(EditorBrowsableState.Always)]
   // //public override bool ValidateChildren();
   // //
   // //
   // // Parameters:
   // //   validationConstraints:
   // //     Tells System.Windows.Forms.ContainerControl.ValidateChildren(System.Windows.Forms.ValidationConstraints)
   // //     how deeply to descend the control hierarchy when validating the control's
   // //     children.
   // //
   // // Returns:
   // //     true if all of the children validated successfully; otherwise, false. If
   // //     called from the System.Windows.Forms.Control.Validating or System.Windows.Forms.Control.Validated
   // //     event handlers, this method will always return false.
   ////[EditorBrowsable(EditorBrowsableState.Always)]
   ////[Browsable(true)]
   // //public override bool ValidateChildren(ValidationConstraints validationConstraints);
   // //
   ////[EditorBrowsable(EditorBrowsableState.Advanced)]
   // // protected override void WndProc(ref Message m);

   // // Summary:
   // //     Represents a collection of controls on the form.
   ////[ComVisible(false)]
   // public class ControlCollection //: Control.ControlCollection
   // {
   //   // Summary:
   //   //     Initializes a new instance of the System.Windows.Forms.Form.ControlCollection
   //   //     class.
   //   //
   //   // Parameters:
   //   //   owner:
   //   //     The System.Windows.Forms.Form to contain the controls added to the control
   //   //     collection.
   //   //public ControlCollection(Form owner);

   //   // Summary:
   //   //     Adds a control to the form.
   //   //
   //   // Parameters:
   //   //   value:
   //   //     The System.Windows.Forms.Control to add to the form.
   //   //
   //   // Exceptions:
   //   //   System.Exception:
   //   //     A multiple document interface (MDI) parent form cannot have controls added
   //   //     to it.
   //   //public override void Add(Control value);
   //   //
   //   // Summary:
   //   //     Removes a control from the form.
   //   //
   //   // Parameters:
   //   //   value:
   //   //     A System.Windows.Forms.Control to remove from the form.
   //   //public override void Remove(Control value);
   // }
  }
}
