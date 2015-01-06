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

namespace System.Windows.Forms {
  // Summary:
  //     Provides data for the System.Windows.Forms.Control.PreviewKeyDown event.
  public class PreviewKeyDownEventArgs : EventArgs {
    // Summary:
    //     Initializes a new instance of the System.Windows.Forms.PreviewKeyDownEventArgs
    //     class with the specified key.
    //
    // Parameters:
    //   keyData:
    //     One of the System.Windows.Forms.Keys values.
    //[TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    //public PreviewKeyDownEventArgs(Keys keyData);

    // Summary:
    //     Gets a value indicating whether the ALT key was pressed.
    //
    // Returns:
    //     true if the ALT key was pressed; otherwise, false.
    //public bool Alt { get; }
    //
    // Summary:
    //     Gets a value indicating whether the CTRL key was pressed.
    //
    // Returns:
    //     true if the CTRL key was pressed; otherwise, false.
    //public bool Control { get; }
    //
    // Summary:
    //     Gets or sets a value indicating whether a key is a regular input key.
    //
    // Returns:
    //     true if the key is a regular input key; otherwise, false.
    public bool IsInputKey { get { return default(bool); } set { } }
    //
    // Summary:
    //     Gets the keyboard code for a System.Windows.Forms.Control.KeyDown or System.Windows.Forms.Control.KeyUp
    //     event.
    //
    // Returns:
    //     One of the System.Windows.Forms.Keys values.
    //public Keys KeyCode { get; }
    //
    // Summary:
    //     Gets the key data for a System.Windows.Forms.Control.KeyDown or System.Windows.Forms.Control.KeyUp
    //     event.
    //
    // Returns:
    //     One of the System.Windows.Forms.Keys values.
    //public Keys KeyData { get; }
    //
    // Summary:
    //     Gets the keyboard value for a System.Windows.Forms.Control.KeyDown or System.Windows.Forms.Control.KeyUp
    //     event.
    //
    // Returns:
    //     An System.Int32 representing the keyboard value.
    //public int KeyValue { get; }
    //
    // Summary:
    //     Gets the modifier flags for a System.Windows.Forms.Control.KeyDown or System.Windows.Forms.Control.KeyUp
    //     event.
    //
    // Returns:
    //     One of the System.Windows.Forms.Keys values.
    //public Keys Modifiers { get; }
    //
    // Summary:
    //     Gets a value indicating whether the SHIFT key was pressed.
    //
    // Returns:
    //     true if the SHIFT key was pressed; otherwise, false.
    //public bool Shift { get; }
  }
}
