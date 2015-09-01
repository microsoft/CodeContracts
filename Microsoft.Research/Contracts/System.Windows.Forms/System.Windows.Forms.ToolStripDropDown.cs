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
using System.Diagnostics.Contracts;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
    /// <summary>
    /// Represents a control that allows the user to select a single item from a list that is displayed when the user clicks a <see cref="T:System.Windows.Forms.ToolStripDropDownButton"/>. Although <see cref="T:System.Windows.Forms.ToolStripDropDownMenu"/> and <see cref="T:System.Windows.Forms.ToolStripDropDown"/> replace and add functionality to the <see cref="T:System.Windows.Forms.Menu"/> control of previous versions, <see cref="T:System.Windows.Forms.Menu"/> is retained for both backward compatibility and future use if you choose.
    /// </summary>
    public class ToolStripDropDown : ToolStrip
    {
        
        // <summary>
        // This property is not relevant to this class.
        // </summary>
        // 
        // <returns>
        // true to enable item reordering; otherwise, false.
        // </returns>
        // public new bool AllowItemReorder {get;}
        
        // <summary>
        // Gets or sets a value indicating whether the <see cref="P:System.Windows.Forms.ToolStripDropDown.Opacity"/> of the form can be adjusted.
        // </summary>
        // 
        // <returns>
        // true if the <see cref="P:System.Windows.Forms.ToolStripDropDown.Opacity"/> of the form can be adjusted; otherwise, false.
        // </returns>
        // public bool AllowTransparency {get; set;}
        
        // <summary>
        // This property is not relevant to this class.
        // </summary>
        // 
        // <returns>
        // One of the <see cref="T:System.Windows.Forms.AnchorStyles"/> values.
        // </returns>
        // public override AnchorStyles Anchor {get; set;}
        
        // <summary>
        // Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStripDropDown"/> automatically adjusts its size when the form is resized.
        // </summary>
        // 
        // <returns>
        // true if the <see cref="T:System.Windows.Forms.ToolStripDropDown"/> control automatically resizes; otherwise, false. The default is true.
        // </returns>
        // <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // [DefaultValue(true)]
        // public override bool AutoSize {get; set;}
        
        // <summary>
        // Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStripDropDown"/> control should automatically close when it has lost activation.
        // </summary>
        // 
        // <returns>
        // true if the <see cref="T:System.Windows.Forms.ToolStripDropDown"/> control automatically closes; otherwise, false. The default is true.
        // </returns>
        // <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // public bool AutoClose {get; set;}
        
        // <summary>
        // Gets or sets a value indicating whether the items in a <see cref="T:System.Windows.Forms.ToolStripDropDown"/> can be sent to an overflow menu.
        // </summary>
        // 
        // <returns>
        // true to send <see cref="T:System.Windows.Forms.ToolStripDropDown"/> items to an overflow menu; otherwise, false. The default is false.
        // </returns>
        // 
        // [DefaultValue(false)]
        // public new bool CanOverflow {get; set;}
        
        // <summary>
        // This property is not relevant to this class.
        // </summary>
        // 
        // <returns>
        // The shortcut menu associated with the control.
        // </returns>
        // public new ContextMenu ContextMenu {get; set;}
        
        // <summary>
        // This property is not relevant to this class.
        // </summary>
        // 
        // <returns>
        // The shortcut menu associated with the control.
        // </returns>
        // public new ContextMenuStrip ContextMenuStrip {get; set;}
        
        // <summary>
        // Gets parameters of a new window.
        // </summary>
        // 
        // <returns>
        // An object of type <see cref="T:System.Windows.Forms.CreateParams"/> used when creating a new window.
        // </returns>
        // protected override CreateParams CreateParams {get;}
        

        // protected override Padding DefaultPadding {get;}
        
        // protected override bool DefaultShowItemToolTips {get;}
        
        // protected override DockStyle DefaultDock {get;}
        
        // <summary>
        // Gets or sets the direction in which the <see cref="T:System.Windows.Forms.ToolStripDropDown"/> is displayed relative to the <see cref="T:System.Windows.Forms.ToolStrip"/>.
        // </summary>
        // 
        // <returns>
        // One of the <see cref="T:System.Windows.Forms.ToolStripDropDownDirection"/> values.
        // </returns>
        // public override ToolStripDropDownDirection DefaultDropDownDirection
        
        // <summary>
        // This property is not relevant to this class.
        // </summary>
        // 
        // <returns>
        // One of the <see cref="T:System.Windows.Forms.DockStyle"/> values.
        // </returns>
        // 
        // [DefaultValue(DockStyle.None)]
        // public override DockStyle Dock {get; set;}
        
        // <summary>
        // Gets or sets a value indicating whether a three-dimensional shadow effect appears when the <see cref="T:System.Windows.Forms.ToolStripDropDown"/> is displayed.
        // </summary>
        // 
        // <returns>
        // true to enable the shadow effect; otherwise, false.
        // </returns>
        // <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // public bool DropShadowEnabled {get; set;}
        
        // <summary>
        // Gets or sets the font of the text displayed on the <see cref="T:System.Windows.Forms.ToolStripDropDown"/>.
        // </summary>
        // 
        // <returns>
        // The <see cref="T:System.Drawing.Font"/> to apply to the text displayed by the control.
        // </returns>
        // public override Font Font { get; set; }
        
        // <summary>
        // This property is not relevant to this class.
        // </summary>
        // 
        // <returns>
        // One of <see cref="T:System.Windows.Forms.ToolStripGripDisplayStyle"/> the values.
        // </returns>
        // public new ToolStripGripDisplayStyle GripDisplayStyle {get;}
        
        // <summary>
        // This property is not relevant to this class.
        // </summary>
        // 
        // <returns>
        // The boundaries of the ToolStrip move handle.
        // </returns>
        // public new Rectangle GripRectangle {get;}
        
        // <summary>
        // This property is not relevant to this class.
        // </summary>
        // 
        // <returns>
        // A <see cref="T:System.Windows.Forms.Padding"/> value.
        // </returns>
        // public new Padding GripMargin {get; set;}
        
        // <summary>
        // This property is not relevant to this class.
        // </summary>
        // 
        // <returns>
        // One of the <see cref="T:System.Windows.Forms.ToolStripGripStyle"/> values.
        // </returns>
        // <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // [DefaultValue(ToolStripGripStyle.Hidden)]
        // public new ToolStripGripStyle GripStyle {get; set;}
        
        // <summary>
        // Gets a value indicating whether this <see cref="T:System.Windows.Forms.ToolStripDropDown"/> was automatically generated.
        // </summary>
        // 
        // <returns>
        // true if this <see cref="T:System.Windows.Forms.ToolStripDropDown"/> is generated automatically; otherwise, false.
        // </returns>
        // 
        // public bool IsAutoGenerated {get;}
        
        
        // <summary>
        // This property is not relevant to this class.
        // </summary>
        // 
        // <returns>
        // The coordinates of the upper-left corner of the control relative to the upper-left corner of its container.
        // </returns>
        // public new Point Location {get; set;}
        
        // <summary>
        // Gets the maximum height and width, in pixels, of the <see cref="T:System.Windows.Forms.ToolStripDropDown"/>.
        // </summary>
        // 
        // <returns>
        // A <see cref="T:System.Drawing.Size"/> representing the height and width of the <see cref="T:System.Windows.Forms.ToolStripDropDown"/>, in pixels.
        // </returns>
        // protected internal override Size MaxItemSize {get; set;}
        
        /// <summary>
        /// Determines the opacity of the form.
        /// </summary>
        /// 
        /// <returns>
        /// The level of opacity for the form. The default is 1.00.
        /// </returns>
        [DefaultValue(1.0)]
        public double Opacity
        {
            get
            {
                Contract.Ensures(Contract.Result<double>() >= 0.0 && Contract.Result<double>() <= 1.0);
                return default(double);
            }
            set
            {
            }
        }

        // <summary>
        // This property is not relevant to this class.
        // </summary>
        // 
        // <returns>
        // The ToolStripItem that is the overflow button for a ToolStrip with overflow enabled.
        // </returns>
        // public new ToolStripOverflowButton OverflowButton {get;}
        
        // <summary>
        // Gets or sets the <see cref="T:System.Windows.Forms.ToolStripItem"/> that is the owner of this <see cref="T:System.Windows.Forms.ToolStripDropDown"/>.
        // </summary>
        // 
        // <returns>
        // The <see cref="T:System.Windows.Forms.ToolStripItem"/> that is the owner of this <see cref="T:System.Windows.Forms.ToolStripDropDown"/>. The default value is null.
        // </returns>
        // [DefaultValue(null)]
        // public ToolStripItem OwnerItem {get; set;}
        
        // <summary>
        // Gets or sets the window region associated with the <see cref="T:System.Windows.Forms.ToolStripDropDown"/>.
        // </summary>
        // 
        // <returns>
        // The window <see cref="T:System.Drawing.Region"/> associated with the control.
        // </returns>
        // public new Region Region {get; set;}
        
        // public override RightToLeft RightToLeft {get; set;}
        
        // <summary>
        // This property is not relevant to this class.
        // </summary>
        // 
        // <returns>
        // true to enable stretching; otherwise, false.
        // </returns>
        // public new bool Stretch {get; set;}
        
        // <summary>
        // Specifies the direction in which to draw the text on the item.
        // </summary>
        // 
        // <returns>
        // One of the <see cref="T:System.Windows.Forms.ToolStripTextDirection"/> values. The default is <see cref="F:System.Windows.Forms.ToolStripTextDirection.Horizontal"/>.
        // </returns>
        // public override ToolStripTextDirection TextDirection {get; set;}
        
        // <summary>
        // Gets or sets a value indicating whether the form should be displayed as a topmost form.
        // </summary>
        // 
        // <returns>
        // true in all cases.
        // </returns>
        // protected virtual bool TopMost {get;}
        
        // <summary>
        // Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStripDropDown"/> is a top-level control.
        // </summary>
        // 
        // <returns>
        // true if the <see cref="T:System.Windows.Forms.ToolStripDropDown"/> is a top-level control; otherwise, false.
        // </returns>
        // <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // public bool TopLevel {get; set;}
        

        // <summary>
        // This property is not relevant to this class.
        // </summary>
        // 
        // <returns>
        // The tab order of the control within its container.
        // </returns>
        // public new int TabIndex {get; set;}
        
        // <summary>
        // Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStripDropDown"/> is visible or hidden.
        // </summary>
        // 
        // <returns>
        // true if the <see cref="T:System.Windows.Forms.ToolStripDropDown"/> is visible; otherwise, false. The default is false.
        // </returns>
        // public new bool Visible {get; set;}
        
        // <summary>
        // Occurs when the value of the <see cref="P:System.Windows.Forms.Control.BackgroundImage"/> property changes.
        // </summary>
        // public new event EventHandler BackgroundImageChanged
        
        // <summary>
        // Occurs when the value of the <see cref="P:System.Windows.Forms.Control.BackgroundImage"/> property changes.
        // </summary>
        // public new event EventHandler BackgroundImageLayoutChanged
        
        // <summary>
        // Occurs when the value of the <see cref="P:System.Windows.Forms.ToolStrip.BindingContext"/> property changes.
        // </summary>
        // public new event EventHandler BindingContextChanged
        
        // <summary>
        // Occurs when the focus or keyboard user interface (UI) cues change.
        // </summary>
        // public new event UICuesEventHandler ChangeUICues
        
        // <summary>
        // This event is not relevant to this class.
        // </summary>
        // public new event EventHandler ContextMenuChanged
        
        // <summary>
        // This event is not relevant to this class.
        // </summary>
        // public new event EventHandler ContextMenuStripChanged
        
        // <summary>
        // This event is not relevant to this class.
        // </summary>
        // public new event EventHandler DockChanged
        
        // <summary>
        // Occurs when the <see cref="T:System.Windows.Forms.ToolStripDropDown"/> is closed.
        // </summary>
        // 
        // public event ToolStripDropDownClosedEventHandler Closed
        
        // <summary>
        // Occurs when the <see cref="T:System.Windows.Forms.ToolStripDropDown"/> control is about to close.
        // </summary>
        // public event ToolStripDropDownClosingEventHandler Closing
        
        // <summary>
        // Occurs when the focus enters the <see cref="T:System.Windows.Forms.ToolStripDropDown"/>.
        // </summary>
        // public new event EventHandler Enter
        
        // <summary>
        // Occurs when the value of the <see cref="P:System.Windows.Forms.ToolStripDropDown.Font"/> property changes.
        // </summary>
        // public new event EventHandler FontChanged
        
        // <summary>
        // Occurs when the value of the <see cref="P:System.Windows.Forms.ToolStrip.ForeColor"/> property changes.
        // </summary>
        // public new event EventHandler ForeColorChanged
        
        // <summary>
        // This event is not relevant for this class.
        // </summary>
        // public new event GiveFeedbackEventHandler GiveFeedback
        
        // <summary>
        // Occurs when the user requests help for a control.
        // </summary>
        // public new event HelpEventHandler HelpRequested
        
        // <summary>
        // Occurs when the <see cref="E:System.Windows.Forms.ToolStripDropDown.ImeModeChanged"/> property has changed.
        // </summary>
        // public new event EventHandler ImeModeChanged
        
        // <summary>
        // Occurs when a key is pressed and held down while the <see cref="T:System.Windows.Forms.ToolStripDropDown"/> has focus.
        // </summary>
        // public new event KeyEventHandler KeyDown
        
        // <summary>
        // Occurs when a key is pressed while the <see cref="T:System.Windows.Forms.ToolStripDropDown"/> has focus.
        // </summary>
        // public new event KeyPressEventHandler KeyPress
        
        // <summary>
        // Occurs when a key is released while the control has focus.
        // </summary>
        // public new event KeyEventHandler KeyUp
        
        // <summary>
        // Occurs when the input focus leaves the control.
        // </summary>
        // public new event EventHandler Leave
        
        // <summary>
        // Occurs when the <see cref="T:System.Windows.Forms.ToolStripDropDown"/> control is opening.
        // </summary>
        // public event CancelEventHandler Opening
        
        /// <summary>
        /// Occurs when the <see cref="T:System.Windows.Forms.ToolStripDropDown"/> is opened.
        /// </summary>
        /// 
        // public event EventHandler Opened
        
        // <summary>
        // Occurs when the value of the <see cref="P:System.Windows.Forms.ToolStripDropDown.Region"/> property changes.
        // </summary>
        // public new event EventHandler RegionChanged
        
        // <summary>
        // This event is not relevant for this class.
        // </summary>
        // public new event ScrollEventHandler Scroll
        
        // <summary>
        // Occurs when the <see cref="T:System.Windows.Forms.ToolStripLayoutStyle"/> style changes.
        // </summary>
        // public new event EventHandler StyleChanged
        
        // <summary>
        // This event is not relevant for this class.
        // </summary>
        // public new event EventHandler TabStopChanged
        
        // <summary>
        // This event is not relevant for this class.
        // </summary>
        // public new event EventHandler TextChanged
        
        // <summary>
        // This event is not relevant to this class.
        // </summary>
        // public new event EventHandler TabIndexChanged
        
        // <summary>
        // This event is not relevant for this class.
        // </summary>
        //  public new event EventHandler Validated
        
        // <summary>
        // This event is not relevant for this class.
        // </summary>
        // public new event CancelEventHandler Validating
        
        // <summary>
        // Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripDropDown"/> class.
        // </summary>
        // public ToolStripDropDown()
        
        // <summary>
        // Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.ToolStripDropDown"/> and optionally releases the managed resources.
        // </summary>
        // <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
        // protected override void Dispose(bool disposing)
        
        /// <summary>
        /// Creates a new accessibility object for the <see cref="T:System.Windows.Forms.ToolStripDropDown"/>.
        /// </summary>
        /// 
        /// <returns>
        /// A new <see cref="T:System.Windows.Forms.AccessibleObject"/> for the control.
        /// </returns>
        protected AccessibleObject CreateAccessibilityInstance()
        {
            Contract.Ensures(Contract.Result<AccessibleObject>() != null);
            return default(AccessibleObject);
        }

        // <summary>
        // Applies various layout options to the <see cref="T:System.Windows.Forms.ToolStripDropDown"/>.
        // </summary>
        // 
        // <returns>
        // The <see cref="T:System.Windows.Forms.LayoutSettings"/> for this <see cref="T:System.Windows.Forms.ToolStripDropDown"/>.
        // </returns>
        // <param name="style">One of the <see cref="T:System.Windows.Forms.ToolStripLayoutStyle"/> values. The possibilities are <see cref="F:System.Windows.Forms.ToolStripLayoutStyle.Flow"/>, <see cref="F:System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow"/>, <see cref="F:System.Windows.Forms.ToolStripLayoutStyle.StackWithOverflow"/>, <see cref="F:System.Windows.Forms.ToolStripLayoutStyle.Table"/>, and <see cref="F:System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow"/>.</param>
        // protected override LayoutSettings CreateLayoutSettings(ToolStripLayoutStyle style)
        
        // protected override void CreateHandle()
        
        // <summary>
        // Closes the <see cref="T:System.Windows.Forms.ToolStripDropDown"/> control.
        // </summary>
        // public void Close()
        
        // <summary>
        // Closes the <see cref="T:System.Windows.Forms.ToolStripDropDown"/> control for the specified reason.
        // </summary>
        // <param name="reason">One of the <see cref="T:System.Windows.Forms.ToolStripDropDownCloseReason"/> values.</param>
        // public void Close(ToolStripDropDownCloseReason reason)
        
        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.ToolStripDropDown.Closed"/> event.
        // </summary>
        // <param name="e">A <see cref="T:System.Windows.Forms.ToolStripDropDownClosedEventArgs"/> that contains the event data.</param>
        // protected virtual void OnClosed(ToolStripDropDownClosedEventArgs e)
        
        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.ToolStripDropDown.Closing"/> event.
        // </summary>
        // <param name="e">A <see cref="T:System.Windows.Forms.ToolStripDropDownClosingEventArgs"/> that contains the event data.</param>
        // protected virtual void OnClosing(ToolStripDropDownClosingEventArgs e)
       
        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.Control.HandleCreated"/> event.
        // </summary>
        // <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        // protected override void OnHandleCreated(EventArgs e)
        
        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.ToolStrip.ItemClicked"/> event.
        // </summary>
        // <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemClickedEventArgs"/> that contains the event data.</param>
        // protected override void OnItemClicked(ToolStripItemClickedEventArgs e)
        
        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.Control.Layout"/> event.
        // </summary>
        // <param name="e">A <see cref="T:System.Windows.Forms.LayoutEventArgs"/> that contains the event data.</param>
        // protected override void OnLayout(LayoutEventArgs e)
        
        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.ToolStripDropDown.Opening"/> event.
        // </summary>
        // <param name="e">A <see cref="T:System.ComponentModel.CancelEventArgs"/> that contains the event data.</param>
        // protected virtual void OnOpening(CancelEventArgs e)
        
        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.ToolStripDropDown.Opened"/> event.
        // </summary>
        // <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        // protected virtual void OnOpened(EventArgs e)
        
        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.ToolStripItem.VisibleChanged"/> event.
        // </summary>
        // <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        // protected override void OnVisibleChanged(EventArgs e)
        
        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.Control.ParentChanged"/> event.
        // </summary>
        // <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        // protected override void OnParentChanged(EventArgs e)
        
        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.ToolStripItem.MouseUp"/> event.
        // </summary>
        // <param name="mea">A <see cref="T:System.Windows.Forms.MouseEventArgs"/> that contains the event data.</param>
        // protected override void OnMouseUp(MouseEventArgs mea)
        
        // <summary>
        // Processes a dialog box key.
        // </summary>
        // 
        // <returns>
        // true if the key was processed by the control; otherwise, false.
        // </returns>
        // <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys"/> values that represents the key to process.</param>
        // [UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
        // protected override bool ProcessDialogKey(Keys keyData)
        
        // <summary>
        // Processes a dialog box character.
        // </summary>
        // 
        // <returns>
        // true if the character was processed by the control; otherwise, false.
        // </returns>
        // <param name="charCode">The character to process.</param>
        // [UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
        // protected override bool ProcessDialogChar(char charCode)
        
        // <summary>
        // Processes a mnemonic character.
        // </summary>
        // 
        // <returns>
        // true if the character was processed as a mnemonic by the control; otherwise, false.
        // </returns>
        // <param name="charCode">The character to process.</param>
        // [UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
        // protected internal override bool ProcessMnemonic(char charCode)
        
        // <summary>
        // This method is not relevant to this class.
        // </summary>
        // <param name="dx">The horizontal scaling factor.</param><param name="dy">The vertical scaling factor.</param>
        // protected override void ScaleCore(float dx, float dy)
        
        // <summary>
        // Scales a control's location, size, padding and margin.
        // </summary>
        // <param name="factor">The factor by which the height and width of the control will be scaled.</param><param name="specified">A value that specifies the bounds of the control to use when defining its size and position.</param>
        // protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
        
        // <summary>
        // Performs the work of setting the specified bounds of this control.
        // </summary>
        // <param name="x">The new <see cref="P:System.Windows.Forms.Control.Left"/> property value of the control. </param><param name="y">The new <see cref="P:System.Windows.Forms.Control.Top"/> property value of the control. </param><param name="width">The new <see cref="P:System.Windows.Forms.Control.Width"/> property value of the control. </param><param name="height">The new <see cref="P:System.Windows.Forms.Control.Height"/> property value of the control. </param><param name="specified">A bitwise combination of the <see cref="T:System.Windows.Forms.BoundsSpecified"/> values. </param>
        // protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        
        // <summary>
        // Adjusts the size of the owner <see cref="T:System.Windows.Forms.ToolStrip"/> to accommodate the <see cref="T:System.Windows.Forms.ToolStripDropDown"/> if the owner <see cref="T:System.Windows.Forms.ToolStrip"/> is currently displayed, or clears and resets active <see cref="T:System.Windows.Forms.ToolStripDropDown"/> child controls of the <see cref="T:System.Windows.Forms.ToolStrip"/> if the <see cref="T:System.Windows.Forms.ToolStrip"/> is not currently displayed.
        // </summary>
        // <param name="visible">true if the owner <see cref="T:System.Windows.Forms.ToolStrip"/> is currently displayed; otherwise, false. </param>
        // protected override void SetVisibleCore(bool visible)
        
        // <summary>
        // Displays the <see cref="T:System.Windows.Forms.ToolStripDropDown"/> control in its default position.
        // </summary>
        // public new void Show()
        
        /// <summary>
        /// Positions the <see cref="T:System.Windows.Forms.ToolStripDropDown"/> relative to the specified control location.
        /// </summary>
        /// <param name="control">The control (typically, a <see cref="T:System.Windows.Forms.ToolStripDropDownButton"/>) that is the reference point for the <see cref="T:System.Windows.Forms.ToolStripDropDown"/> position.</param><param name="position">The horizontal and vertical location of the reference control's upper-left corner, in pixels.</param><exception cref="T:System.ArgumentNullException">The control specified by the <paramref name="control"/> parameter is null.</exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public void Show(Control control, Point position)
        {
            Contract.Requires(control != null);
        }

        /// <summary>
        /// Positions the <see cref="T:System.Windows.Forms.ToolStripDropDown"/> relative to the specified control at the specified location and with the specified direction relative to the parent control.
        /// </summary>
        /// <param name="control">The control (typically, a <see cref="T:System.Windows.Forms.ToolStripDropDownButton"/>) that is the reference point for the <see cref="T:System.Windows.Forms.ToolStripDropDown"/> position.</param><param name="position">The horizontal and vertical location of the reference control's upper-left corner, in pixels.</param><param name="direction">One of the <see cref="T:System.Windows.Forms.ToolStripDropDownDirection"/> values.</param><exception cref="T:System.ArgumentNullException">The control specified by the <paramref name="control"/> parameter is null.</exception>
        public void Show(Control control, Point position, ToolStripDropDownDirection direction)
        {
            Contract.Requires(control != null);
        }

        /// <summary>
        /// Positions the <see cref="T:System.Windows.Forms.ToolStripDropDown"/> relative to the specified control's horizontal and vertical screen coordinates.
        /// </summary>
        /// <param name="control">The control (typically, a <see cref="T:System.Windows.Forms.ToolStripDropDownButton"/>) that is the reference point for the <see cref="T:System.Windows.Forms.ToolStripDropDown"/> position.</param><param name="x">The horizontal screen coordinate of the control, in pixels.</param><param name="y">The vertical screen coordinate of the control, in pixels.</param><exception cref="T:System.ArgumentNullException">The control specified by the <paramref name="control"/> parameter is null.</exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public void Show(Control control, int x, int y)
        {
            Contract.Requires(control != null);
        }

        // <summary>
        // Positions the <see cref="T:System.Windows.Forms.ToolStripDropDown"/> relative to the specified screen location.
        // </summary>
        // <param name="screenLocation">The horizontal and vertical location of the screen's upper-left corner, in pixels.</param><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // public void Show(Point screenLocation)
        
        // <summary>
        // Positions the <see cref="T:System.Windows.Forms.ToolStripDropDown"/> relative to the specified control location and with the specified direction relative to the parent control.
        // </summary>
        // <param name="position">The horizontal and vertical location of the reference control's upper-left corner, in pixels.</param><param name="direction">One of the <see cref="T:System.Windows.Forms.ToolStripDropDownDirection"/> values.</param>
        // public void Show(Point position, ToolStripDropDownDirection direction)
        
        // <summary>
        // Positions the <see cref="T:System.Windows.Forms.ToolStripDropDown"/> relative to the specified screen coordinates.
        // </summary>
        // <param name="x">The horizontal screen coordinate, in pixels.</param><param name="y">The vertical screen coordinate, in pixels.</param><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // public void Show(int x, int y)
        
        // <param name="m">The Windows <see cref="T:System.Windows.Forms.Message"/> to process.</param>
        // [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        // protected override void WndProc(ref Message m)

        /// <summary>
        /// Provides information about the <see cref="T:System.Windows.Forms.ToolStripDropDown"/> control to accessibility client applications.
        /// </summary>
        public class ToolStripDropDownAccessibleObject : ToolStrip.ToolStripAccessibleObject
        {

            /// <summary>
            /// Gets or sets the name of the <see cref="T:System.Windows.Forms.ToolStripDropDown.ToolStripDropDownAccessibleObject"/>.
            /// </summary>
            /// 
            /// <returns>
            /// The string representing the name.
            /// </returns>
            // public override string Name {get; set;}

            /// <summary>
            /// Gets the role of the <see cref="T:System.Windows.Forms.ToolStripDropDown.ToolStripDropDownAccessibleObject"/>.
            /// </summary>
            /// 
            /// <returns>
            /// The <see cref="F:System.Windows.Forms.AccessibleRole.Table"/> value.
            /// </returns>
            // public override AccessibleRole Role {get;}

            /// <summary>
            /// Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripDropDown.ToolStripDropDownAccessibleObject"/> class.
            /// </summary>
            /// <param name="owner">The <see cref="T:System.Windows.Forms.ToolStripDropDown"/> that owns the <see cref="T:System.Windows.Forms.ToolStripDropDown.ToolStripDropDownAccessibleObject"/>.</param>
            public ToolStripDropDownAccessibleObject(ToolStripDropDown owner)
             : base((ToolStrip)owner)
            { }
        }
    }
}
