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
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
// using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
    /// <summary>
    /// Represents a Windows text box control.
    /// </summary>
    public class TextBox : TextBoxBase
    {
        /// <summary>
        /// Gets or sets a value indicating whether pressing ENTER in a multiline <see cref="T:System.Windows.Forms.TextBox"/> control creates a new line of text in the control or activates the default button for the form.
        /// </summary>
        /// 
        /// <returns>
        /// true if the ENTER key creates a new line of text in a multiline version of the control; false if the ENTER key activates the default button for the form. The default is false.
        /// </returns>
        /// 
        // [DefaultValue(false)]
        // public bool AcceptsReturn {get; set;}
        
        /// <summary>
        /// Gets or sets an option that controls how automatic completion works for the <see cref="T:System.Windows.Forms.TextBox"/>.
        /// </summary>
        /// 
        /// <returns>
        /// One of the values of <see cref="T:System.Windows.Forms.AutoCompleteMode"/>. The following are the values. <see cref="F:System.Windows.Forms.AutoCompleteMode.Append"/>Appends the remainder of the most likely candidate string to the existing characters, highlighting the appended characters.<see cref="F:System.Windows.Forms.AutoCompleteMode.Suggest"/>Displays the auxiliary drop-down list associated with the edit control. This drop-down is populated with one or more suggested completion strings.<see cref="F:System.Windows.Forms.AutoCompleteMode.SuggestAppend"/>Appends both Suggest and Append options.<see cref="F:System.Windows.Forms.AutoCompleteMode.None"/>Disables automatic completion. This is the default.
        /// </returns>
        /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value is not one of the values of <see cref="T:System.Windows.Forms.AutoCompleteMode"/>. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // [DefaultValue(AutoCompleteMode.None)]
        // public AutoCompleteMode AutoCompleteMode { get; set; }
        
        /// <summary>
        /// Gets or sets a value specifying the source of complete strings used for automatic completion.
        /// </summary>
        /// 
        /// <returns>
        /// One of the values of <see cref="T:System.Windows.Forms.AutoCompleteSource"/>. The options are AllSystemSources, AllUrl, FileSystem, HistoryList, RecentlyUsedList, CustomSource, and None. The default is None.
        /// </returns>
        /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value is not one of the values of <see cref="T:System.Windows.Forms.AutoCompleteSource"/>. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // public AutoCompleteSource AutoCompleteSource {get; set;}
        
        /// <summary>
        /// Gets or sets a custom <see cref="T:System.Collections.Specialized.StringCollection"/> to use when the <see cref="P:System.Windows.Forms.TextBox.AutoCompleteSource"/> property is set to CustomSource.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.Collections.Specialized.StringCollection"/> to use with <see cref="P:System.Windows.Forms.TextBox.AutoCompleteSource"/>.
        /// </returns>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public AutoCompleteStringCollection AutoCompleteCustomSource
        {
            get
            {
                Contract.Ensures(Contract.Result<AutoCompleteStringCollection>() != null);
                return default(AutoCompleteStringCollection);
            }
            set
            {
               
            }
        }

        /// <summary>
        /// Gets or sets whether the <see cref="T:System.Windows.Forms.TextBox"/> control modifies the case of characters as they are typed.
        /// </summary>
        /// 
        /// <returns>
        /// One of the <see cref="T:System.Windows.Forms.CharacterCasing"/> enumeration values that specifies whether the <see cref="T:System.Windows.Forms.TextBox"/> control modifies the case of characters. The default is CharacterCasing.Normal.
        /// </returns>
        /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">A value that is not within the range of valid values for the enumeration was assigned to the property. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // public CharacterCasing CharacterCasing {get; set;}
        
        /// <summary>
        /// Gets or sets a value indicating whether this is a multiline <see cref="T:System.Windows.Forms.TextBox"/> control.
        /// </summary>
        /// 
        /// <returns>
        /// true if the control is a multiline <see cref="T:System.Windows.Forms.TextBox"/> control; otherwise, false. The default is false.
        /// </returns>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // public override bool Multiline {get; set;}
        
        /// <summary>
        /// Gets the required creation parameters when the control handle is created.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.Windows.Forms.CreateParams"/> that contains the required creation parameters when the handle to the control is created.
        /// </returns>
        // protected override CreateParams CreateParams {get;}
        
        /// <summary>
        /// Gets or sets the character used to mask characters of a password in a single-line <see cref="T:System.Windows.Forms.TextBox"/> control.
        /// </summary>
        /// 
        /// <returns>
        /// The character used to mask characters entered in a single-line <see cref="T:System.Windows.Forms.TextBox"/> control. Set the value of this property to 0 (character value) if you do not want the control to mask characters as they are typed. Equals 0 (character value) by default.
        /// </returns>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // public char PasswordChar {get; set;}
        
        /// <summary>
        /// Gets or sets which scroll bars should appear in a multiline <see cref="T:System.Windows.Forms.TextBox"/> control.
        /// </summary>
        /// 
        /// <returns>
        /// One of the <see cref="T:System.Windows.Forms.ScrollBars"/> enumeration values that indicates whether a multiline <see cref="T:System.Windows.Forms.TextBox"/> control appears with no scroll bars, a horizontal scroll bar, a vertical scroll bar, or both. The default is ScrollBars.None.
        /// </returns>
        /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">A value that is not within the range of valid values for the enumeration was assigned to the property. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // [DefaultValue(ScrollBars.None)]
        // public ScrollBars ScrollBars {get; set;}
        
        /// <summary>
        /// Gets or sets the current text in the <see cref="T:System.Windows.Forms.TextBox"/>.
        /// </summary>
        /// 
        /// <returns>
        /// The text displayed in the control.
        /// </returns>
        // public override string Text {get; set;}
        
        /// <summary>
        /// Gets or sets how text is aligned in a <see cref="T:System.Windows.Forms.TextBox"/> control.
        /// </summary>
        /// 
        /// <returns>
        /// One of the <see cref="T:System.Windows.Forms.HorizontalAlignment"/> enumeration values that specifies how text is aligned in the control. The default is HorizontalAlignment.Left.
        /// </returns>
        /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">A value that is not within the range of valid values for the enumeration was assigned to the property. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // public HorizontalAlignment TextAlign {get; set;}
        
        /// <summary>
        /// Gets or sets a value indicating whether the text in the <see cref="T:System.Windows.Forms.TextBox"/> control should appear as the default password character.
        /// </summary>
        /// 
        /// <returns>
        /// true if the text in the <see cref="T:System.Windows.Forms.TextBox"/> control should appear as the default password character; otherwise, false.
        /// </returns>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // [DefaultValue(false)]
        // public bool UseSystemPasswordChar {get; set;}
        
        /// <summary>
        /// Occurs when the value of the <see cref="P:System.Windows.Forms.TextBox.TextAlign"/> property has changed.
        /// </summary>
        /// 
        // public event EventHandler TextAlignChanged
        
        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.TextBox"/> and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
        // protected override void Dispose(bool disposing)
        
        /// <summary>
        /// Determines whether the specified key is an input key or a special key that requires preprocessing.
        /// </summary>
        /// 
        /// <returns>
        /// true if the specified key is an input key; otherwise, false.
        /// </returns>
        /// <param name="keyData">One of the key's values.</param>
        // protected override bool IsInputKey(Keys keyData)
        
        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.BackColorChanged"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        // protected override void OnBackColorChanged(EventArgs e)
        
        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.FontChanged"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data. </param>
        // protected override void OnFontChanged(EventArgs e)
        
        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.GotFocus"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        // protected override void OnGotFocus(EventArgs e)
        
        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.HandleCreated"/> event.
        /// </summary>
        /// <param name="e">The event data.</param>
        // protected override void OnHandleCreated(EventArgs e)
        
        /// <summary>
        /// Raises the <see cref="M:System.Windows.Forms.Control.OnHandleDestroyed(System.EventArgs)"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data. </param>
        // protected override void OnHandleDestroyed(EventArgs e)
        
        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.TextBox.TextAlignChanged"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data. </param>
        // protected virtual void OnTextAlignChanged(EventArgs e)
        
        /// <summary>
        /// Sets the selected text to the specified text without clearing the undo buffer.
        /// </summary>
        /// <param name="text">The text to replace.</param>
        // public void Paste(string text)
        
        /// <summary>
        /// Processes Windows messages.
        /// </summary>
        /// <param name="m">A Windows Message object. </param>
        // [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        // protected override void WndProc(ref Message m)
    }
}

