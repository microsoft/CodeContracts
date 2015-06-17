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

#region Assembly System.Windows.Forms.dll, v4.0.30319
// C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0\System.Windows.Forms.dll
#endregion

using System;
using System.Runtime;
using System.Runtime.InteropServices;

namespace System.Windows.Forms {
  // Summary:
  //     Provides data for the System.Windows.Forms.Control.KeyDown or System.Windows.Forms.Control.KeyUp
  //     event.
  [ComVisible(true)]
  public class KeyEventArgs : EventArgs {
    // Summary:
    //     Initializes a new instance of the System.Windows.Forms.KeyEventArgs class.
    //
    // Parameters:
    //   keyData:
    //     A System.Windows.Forms.Keys representing the key that was pressed, combined
    //     with any modifier flags that indicate which CTRL, SHIFT, and ALT keys were
    //     pressed at the same time. Possible values are obtained be applying the bitwise
    //     OR (|) operator to constants from the System.Windows.Forms.Keys enumeration.
    //[TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    //public KeyEventArgs(Keys keyData);

    // Summary:
    //     Gets a value indicating whether the ALT key was pressed.
    //
    // Returns:
    //     true if the ALT key was pressed; otherwise, false.
    //public virtual bool Alt { get { return default(); } }
    //
    // Summary:
    //     Gets a value indicating whether the CTRL key was pressed.
    //
    // Returns:
    //     true if the CTRL key was pressed; otherwise, false.
    //public bool Control { get { return default(); } }
    //
    // Summary:
    //     Gets or sets a value indicating whether the event was handled.
    //
    // Returns:
    //     true to bypass the control's default handling; otherwise, false to also pass
    //     the event along to the default control handler.
    public bool Handled { get { return default(bool); } set { } }
    //
    // Summary:
    //     Gets the keyboard code for a System.Windows.Forms.Control.KeyDown or System.Windows.Forms.Control.KeyUp
    //     event.
    //
    // Returns:
    //     A System.Windows.Forms.Keys value that is the key code for the event.
    //public Keys KeyCode { get { return default(); } }
    //
    // Summary:
    //     Gets the key data for a System.Windows.Forms.Control.KeyDown or System.Windows.Forms.Control.KeyUp
    //     event.
    //
    // Returns:
    //     A System.Windows.Forms.Keys representing the key code for the key that was
    //     pressed, combined with modifier flags that indicate which combination of
    //     CTRL, SHIFT, and ALT keys was pressed at the same time.
    //public Keys KeyData { get { return default(); } }
    //
    // Summary:
    //     Gets the keyboard value for a System.Windows.Forms.Control.KeyDown or System.Windows.Forms.Control.KeyUp
    //     event.
    //
    // Returns:
    //     The integer representation of the System.Windows.Forms.KeyEventArgs.KeyCode
    //     property.
    //public int KeyValue { get { return default(); } }
    //
    // Summary:
    //     Gets the modifier flags for a System.Windows.Forms.Control.KeyDown or System.Windows.Forms.Control.KeyUp
    //     event. The flags indicate which combination of CTRL, SHIFT, and ALT keys
    //     was pressed.
    //
    // Returns:
    //     A System.Windows.Forms.Keys value representing one or more modifier flags.
    //public Keys Modifiers { get { return default(); } }
    //
    // Summary:
    //     Gets a value indicating whether the SHIFT key was pressed.
    //
    // Returns:
    //     true if the SHIFT key was pressed; otherwise, false.
    //public virtual bool Shift { get { return default(); } }
    //
    // Summary:
    //     Gets or sets a value indicating whether the key event should be passed on
    //     to the underlying control.
    //
    // Returns:
    //     true if the key event should not be sent to the control; otherwise, false.
    public bool SuppressKeyPress { get { return default(bool); } set { } }
  }
}
