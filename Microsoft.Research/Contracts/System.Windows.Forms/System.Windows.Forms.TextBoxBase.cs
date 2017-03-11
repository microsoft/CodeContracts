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
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
    /// <summary>
    /// Implements the basic functionality required by text controls.
    /// </summary>
    public abstract class TextBoxBase : Control
    {
        // <summary>
        // Gets or sets a value indicating whether pressing the TAB key in a multiline text box control types a TAB character in the control instead of moving the focus to the next control in the tab order.
        // </summary>
        // <returns>
        // true if users can enter tabs in a multiline text box using the TAB key; false if pressing the TAB key moves the focus. The default is false.
        // </returns>
        // public bool AcceptsTab {get; set;}
        
        // <summary>
        // Gets or sets a value indicating whether the defined shortcuts are enabled.
        // </summary>
        // <returns>
        // true to enable the shortcuts; otherwise, false.
        // </returns>
        // public virtual bool ShortcutsEnabled {get; set;}
        
        // <summary>
        // Gets or sets a value indicating whether the height of the control automatically adjusts when the font assigned to the control is changed.
        // </summary>
        // <returns>
        // true if the height of the control automatically adjusts when the font is changed; otherwise, false. The default is true.
        // </returns>
        // <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // public override bool AutoSize {get; set;}
        
        // <summary>
        // Gets or sets the background color of the control.
        // </summary>
        // <returns>
        // A <see cref="T:System.Drawing.Color"/> that represents the background of the control.
        // </returns>
        // <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // public override Color BackColor {get; set;}
        
        // <summary>
        // This property is not relevant for this class.
        // </summary>
        // <returns>
        // The background image for the object.
        // </returns>
        // <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // public override Image BackgroundImage {get; set;}
        
        // <summary>
        // This property is not relevant for this class.
        // </summary>
        // <returns>
        // One of the <see cref="T:System.Windows.Forms.ImageLayout"/> values.
        // </returns>
        // <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // public override ImageLayout BackgroundImageLayout {get; set;}
        
        // <summary>
        // Gets or sets the border type of the text box control.
        // </summary>
        // <returns>
        // A <see cref="T:System.Windows.Forms.BorderStyle"/> that represents the border type of the text box control. The default is Fixed3D.
        // </returns>
        // <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">A value that is not within the range of valid values for the enumeration was assigned to the property. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // public BorderStyle BorderStyle {get; set;}
        
        // <summary>
        // Gets a value indicating whether the <see cref="P:System.Windows.Forms.Control.ImeMode"/> property can be set to an active value, to enable IME support.
        // </summary>
        // <returns>
        // false if the <see cref="P:System.Windows.Forms.TextBoxBase.ReadOnly"/> property is true or if this <see cref="T:System.Windows.Forms.TextBoxBase"/> class is set to use a password mask character; otherwise, true.
        // </returns>
        // protected override bool CanEnableIme {get;}
        
        // <summary>
        // Gets a value indicating whether the user can undo the previous operation in a text box control.
        // </summary>
        // <returns>
        // true if the user can undo the previous operation performed in a text box control; otherwise, false.
        // </returns>
        // <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // public bool CanUndo {get;}
        
        /// <returns>
        /// A <see cref="T:System.Windows.Forms.CreateParams"/> representing the information needed when creating a control.
        /// </returns>
        protected virtual CreateParams CreateParams
        {
            // [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
            get
            {
                Contract.Ensures(Contract.Result<CreateParams>() != null);
                return default(CreateParams);
            }
        }

        // <summary>
        // Gets or sets a value indicating whether control drawing is done in a buffer before the control is displayed. This property is not relevant for this class.
        // </summary>
        // <returns>
        // true to implement double buffering on the control; otherwise, false.
        // </returns>
        // protected override bool DoubleBuffered {get; set;}

        // <summary>
        // Gets or sets the default cursor for the control.
        // </summary>
        // <returns>
        // An object of type <see cref="T:System.Windows.Forms.Cursor"/> representing the current default cursor.
        // </returns>
        // protected override Cursor DefaultCursor { get; }
        
        // <returns>
        // A <see cref="T:System.Drawing.Size"/> value.
        // </returns>
        // protected override Size DefaultSize { get; }
        
        // <summary>
        // Gets or sets the foreground color of the control.
        // </summary>
        // <returns>
        // A <see cref="T:System.Drawing.Color"/> that represents the control's foreground color.
        // </returns>
        // <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // public override Color ForeColor {get; set;}
        
        // <summary>
        // Gets or sets a value indicating whether the selected text in the text box control remains highlighted when the control loses focus.
        // </summary>
        // <returns>
        // true if the selected text does not appear highlighted when the text box control loses focus; false, if the selected text remains highlighted when the text box control loses focus. The default is true.
        // </returns>
        // <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // [DefaultValue(true)]
        // public bool HideSelection {get; set;}
        
        // <summary>
        // Gets or sets the Input Method Editor (IME) mode of a control.
        // </summary>
        // <returns>
        // The IME mode of the control.
        // </returns>
        // protected override ImeMode ImeModeBase {get; set;}
        
        /// <summary>
        /// Gets or sets the lines of text in a text box control.
        /// </summary>
        /// <returns>
        /// An array of strings that contains the text in a text box control.
        /// </returns>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public string[] Lines
        {
            get
            {
                Contract.Ensures(Contract.Result<string[]>() != null);
                return default(string[]);
            }
            set
            {
            }
        }

        // <summary>
        // Gets or sets the maximum number of characters the user can type or paste into the text box control.
        // </summary>
        // <returns>
        // The number of characters that can be entered into the control. The default is 32767.
        // </returns>
        // <exception cref="T:System.ArgumentOutOfRangeException">The value assigned to the property is less than 0. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // public virtual int MaxLength {get; set;}
        
        // <summary>
        // Gets or sets a value that indicates that the text box control has been modified by the user since the control was created or its contents were last set.
        // </summary>
        // <returns>
        // true if the control's contents have been modified; otherwise, false. The default is false.
        // </returns>
        // <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // public bool Modified {get; set;}
        
        // <summary>
        // Gets or sets a value indicating whether this is a multiline text box control.
        // </summary>
        // <returns>
        // true if the control is a multiline text box control; otherwise, false. The default is false.
        // </returns>
        // <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // public virtual bool Multiline {get; set;}
        
        // <summary>
        // This property is not relevant for this class.
        // </summary>
        // <returns>
        // A <see cref="T:System.Windows.Forms.Padding"/> value.
        // </returns>
        // <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode"/></PermissionSet>
        // public new Padding Padding {get; set;}
        
        // <summary>
        // Gets the preferred height for a text box.
        // </summary>
        // <returns>
        // The preferred height of a text box.
        // </returns>
        // public int PreferredHeight {get;}
        
        // <summary>
        // Gets or sets a value indicating whether text in the text box is read-only.
        // </summary>
        // <returns>
        // true if the text box is read-only; otherwise, false. The default is false.
        // </returns>
        // <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // [DefaultValue(false)]
        // public bool ReadOnly {get; set;}
        
        /// <summary>
        /// Gets or sets a value indicating the currently selected text in the control.
        /// </summary>
        /// <returns>
        /// A string that represents the currently selected text in the text box.
        /// </returns>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public virtual string SelectedText
        {
            get
            {
               Contract.Ensures(Contract.Result<string>() != null);
                return default(string);
            }
            set
            {
            }
        }

        /// <summary>
        /// Gets or sets the number of characters selected in the text box.
        /// </summary>
        /// <returns>
        /// The number of characters selected in the text box.
        /// </returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">The assigned value is less than zero.</exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public virtual int SelectionLength
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

        /// <summary>
        /// Gets or sets the starting point of text selected in the text box.
        /// </summary>
        /// <returns>
        /// The starting position of text selected in the text box.
        /// </returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">The assigned value is less than zero.</exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
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
        
        // <summary>
        // Gets or sets the current text in the text box.
        // </summary>
        // <returns>
        // The text displayed in the control.
        // </returns>
        // 
        // public override string Text {get; set;}
        
        /// <summary>
        /// Gets the length of text in the control.
        /// </summary>
        /// <returns>
        /// The number of characters contained in the text of the control.
        /// </returns>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public virtual int TextLength
        {
            get
            {
                Contract.Ensures(Contract.Result<int>() >= 0);
                return default(int);
            }
        }

        // <summary>
        // Indicates whether a multiline text box control automatically wraps words to the beginning of the next line when necessary.
        // </summary>
        // <returns>
        // true if the multiline text box control wraps words; false if the text box control automatically scrolls horizontally when the user types past the right edge of the control. The default is true.
        // </returns>
        // <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // public bool WordWrap {get; set;}

        // <summary>
        // Occurs when the value of the <see cref="P:System.Windows.Forms.TextBoxBase.AcceptsTab"/> property has changed.
        // </summary>
        // public event EventHandler AcceptsTabChanged

        // <summary>
        // This event is not relevant for this class.
        // </summary>
        // public new event EventHandler AutoSizeChanged

        // <summary>
        // Occurs when the value of the <see cref="P:System.Windows.Forms.TextBoxBase.BackgroundImage"/> property changes. This event is not relevant for this class.
        // </summary>
        // public new event EventHandler BackgroundImageChanged

        // <summary>
        // Occurs when the value of the <see cref="P:System.Windows.Forms.TextBoxBase.BackgroundImageLayout"/> property changes. This event is not relevant for this class.
        // </summary>
        // public new event EventHandler BackgroundImageLayoutChanged

        // <summary>
        // Occurs when the value of the <see cref="P:System.Windows.Forms.TextBoxBase.BorderStyle"/> property has changed.
        // </summary>
        // public event EventHandler BorderStyleChanged

        // <summary>
        // Occurs when the text box is clicked.
        // </summary>
        // public new event EventHandler Click

        // <summary>
        // Occurs when the control is clicked by the mouse.
        // </summary>
        // public new event MouseEventHandler MouseClick

        // <summary>
        // Occurs when the value of the <see cref="P:System.Windows.Forms.TextBoxBase.HideSelection"/> property has changed.
        // </summary>
        // public event EventHandler HideSelectionChanged

        // <summary>
        // Occurs when the value of the <see cref="P:System.Windows.Forms.TextBoxBase.Modified"/> property has changed.
        // </summary>
        // public event EventHandler ModifiedChanged

        // <summary>
        // Occurs when the value of the <see cref="P:System.Windows.Forms.TextBoxBase.Multiline"/> property has changed.
        // </summary>
        // public event EventHandler MultilineChanged

        // <summary>
        // This event is not relevant for this class.
        // </summary>
        // public new event EventHandler PaddingChanged

        // <summary>
        // Occurs when the value of the <see cref="P:System.Windows.Forms.TextBoxBase.ReadOnly"/> property has changed.
        // </summary>
        // public event EventHandler ReadOnlyChanged

        // <summary>
        // Occurs when the control is redrawn. This event is not relevant for this class.
        // </summary>
        // public new event PaintEventHandler Paint

        // <summary>
        // Processes a command key.
        // </summary>
        // <returns>
        // true if the command key was processed by the control; otherwise, false.
        // </returns>
        // <param name="msg">A <see cref="T:System.Windows.Forms.Message"/>, passed by reference that represents the window message to process. </param><param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys"/> values that represents the shortcut key to process. </param>
        // [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        // protected override bool ProcessCmdKey(ref Message msg, Keys keyData)

        // <summary>
        // Appends text to the current text of a text box.
        // </summary>
        // <param name="text">The text to append to the current contents of the text box. </param><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // public void AppendText(string text)

        // <summary>
        // Clears all text from the text box control.
        // </summary>
        // <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // public void Clear()

        // <summary>
        // Clears information about the most recent operation from the undo buffer of the text box.
        // </summary>
        // <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // public void ClearUndo()

        // <summary>
        // Copies the current selection in the text box to the Clipboard.
        // </summary>
        // <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // [UIPermission(SecurityAction.Demand, Clipboard = UIPermissionClipboard.OwnClipboard)]
        // public void Copy()

        // protected override void CreateHandle()

        // <summary>
        // Moves the current selection in the text box to the Clipboard.
        // </summary>
        // <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // public void Cut()

        // <summary>
        // Determines whether the specified key is an input key or a special key that requires preprocessing.
        // </summary>
        // <returns>
        // true if the specified key is an input key; otherwise, false.
        // </returns>
        // <param name="keyData">One of the Keys value.</param>
        // protected override bool IsInputKey(Keys keyData)

        // <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data. </param>
        // protected override void OnHandleCreated(EventArgs e)

        // <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data. </param>
        // protected override void OnHandleDestroyed(EventArgs e)

        // <summary>
        // Replaces the current selection in the text box with the contents of the Clipboard.
        // </summary>
        // <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // [UIPermission(SecurityAction.Demand, Clipboard = UIPermissionClipboard.OwnClipboard)]
        // public void Paste()

        // <returns>
        // true if the key was processed by the control; otherwise, false.
        // </returns>
        // <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys"/> values that represents the key to process. </param>
        // [UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
        // protected override bool ProcessDialogKey(Keys keyData)

        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.TextBoxBase.AcceptsTabChanged"/> event.
        // </summary>
        // <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data. </param>
        // protected virtual void OnAcceptsTabChanged(EventArgs e)

        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.TextBoxBase.BorderStyleChanged"/> event.
        // </summary>
        // <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data. </param>
        // protected virtual void OnBorderStyleChanged(EventArgs e)

        // <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data. </param>
        // protected override void OnFontChanged(EventArgs e)

        // <summary>
        // Raise the <see cref="E:System.Windows.Forms.TextBoxBase.HideSelectionChanged"/> event.
        // </summary>
        // <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data. </param>
        // protected virtual void OnHideSelectionChanged(EventArgs e)

        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.TextBoxBase.ModifiedChanged"/> event.
        // </summary>
        // <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data. </param>
        // protected virtual void OnModifiedChanged(EventArgs e)

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseUp"/> event.
        /// </summary>
        /// <param name="mevent">The event data.</param>
        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            Contract.Requires(mevent != null);
        }
        
        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.TextBoxBase.MultilineChanged"/> event.
        // </summary>
        // <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data. </param>
        // protected virtual void OnMultilineChanged(EventArgs e)
        
        // <summary>
        // This method is not relevant for this class.
        // </summary>
        // <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        // protected override void OnPaddingChanged(EventArgs e)
        
        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.TextBoxBase.ReadOnlyChanged"/> event.
        // </summary>
        // <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data. </param>
        // protected virtual void OnReadOnlyChanged(EventArgs e)
        
        // <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data. </param>
        // protected override void OnTextChanged(EventArgs e)
        
        // <summary>
        // Retrieves the character that is closest to the specified location within the control.
        // </summary>
        // <returns>
        // The character at the specified location.
        // </returns>
        // <param name="pt">The location from which to seek the nearest character. </param><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // public virtual char GetCharFromPosition(Point pt)
        
        /// <summary>
        /// Retrieves the index of the character nearest to the specified location.
        /// </summary>
        /// <returns>
        /// The zero-based character index at the specified location.
        /// </returns>
        /// <param name="pt">The location to search. </param><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public virtual int GetCharIndexFromPosition(Point pt)
        {
            Contract.Ensures(Contract.Result<int>() >= 0);
            return default(int);
        }

        /// <summary>
        /// Retrieves the line number from the specified character position within the text of the control.
        /// </summary>
        /// <returns>
        /// The zero-based line number in which the character index is located.
        /// </returns>
        /// <param name="index">The character index position to search. </param><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public virtual int GetLineFromCharIndex(int index)
        {
            Contract.Ensures(Contract.Result<int>() >= 0);
            return default(int);
        }

        // <summary>
        // Retrieves the location within the control at the specified character index.
        // </summary>
        // <returns>
        // The location of the specified character within the client rectangle of the control.
        // </returns>
        // <param name="index">The index of the character for which to retrieve the location. </param><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // public virtual Point GetPositionFromCharIndex(int index)
        
        /// <summary>
        /// Retrieves the index of the first character of a given line.
        /// </summary>
        /// <returns>
        /// The zero-based index of the first character in the specified line.
        /// </returns>
        /// <param name="lineNumber">The line for which to get the index of its first character. </param><exception cref="T:System.ArgumentOutOfRangeException">The value of the <paramref name="lineNumber"/> parameter is less than zero.</exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public int GetFirstCharIndexFromLine(int lineNumber)
        {
            Contract.Requires(lineNumber >= 0);
            return default(int);
        }

        /// <summary>
        /// Retrieves the index of the first character of the current line.
        /// </summary>
        /// <returns>
        /// The zero-based character index in the current line.
        /// </returns>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public int GetFirstCharIndexOfCurrentLine()
        {
            Contract.Ensures(Contract.Result<int>() >= 0);
            return default(int);
        }

        // <summary>
        // Scrolls the contents of the control to the current caret position.
        // </summary>
        // <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // public void ScrollToCaret()
        
        /// <summary>
        /// Specifies that the value of the <see cref="P:System.Windows.Forms.TextBoxBase.SelectionLength"/> property is zero so that no characters are selected in the control.
        /// </summary>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public void DeselectAll()
        {
            Contract.Ensures(SelectionLength == 0);
        }

        /// <summary>
        /// Selects a range of text in the text box.
        /// </summary>
        /// <param name="start">The position of the first character in the current text selection within the text box. </param><param name="length">The number of characters to select. </param><exception cref="T:System.ArgumentOutOfRangeException">The value of the <paramref name="start"/> parameter is less than zero.</exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public void Select(int start, int length)
        {
            Contract.Requires(start >= 0);
        }
        
        // <summary>
        // Selects all text in the text box.
        // </summary>
        // <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // public void SelectAll()
        
        // <summary>
        // Sets the specified bounds of the <see cref="T:System.Windows.Forms.TextBoxBase"/> control.
        // </summary>
        // <param name="x">The new <see cref="P:System.Windows.Forms.Control.Left"/> property value of the control.</param><param name="y">The new <see cref="P:System.Windows.Forms.Control.Top"/> property value of the control.</param><param name="width">The new <see cref="P:System.Windows.Forms.Control.Width"/> property value of the control.</param><param name="height">Not used.</param><param name="specified">A bitwise combination of the <see cref="T:System.Windows.Forms.BoundsSpecified"/> values.</param>
        // protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        
        // <summary>
        // Returns a string that represents the <see cref="T:System.Windows.Forms.TextBoxBase"/> control.
        // </summary>
        // <returns>
        // A string that represents the current <see cref="T:System.Windows.Forms.TextBoxBase"/>. The string includes the type and the <see cref="T:System.Windows.Forms.TextBoxBase"/> property of the control.
        // </returns>
        
        // public override string ToString()
        
        // <summary>
        // Undoes the last edit operation in the text box.
        // </summary>
        // <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // public void Undo()
        
        // <param name="m">A Windows Message Object. </param>
        // [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        // protected override void WndProc(ref Message m)
    }
}
