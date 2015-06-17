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
  //     Provides data for the System.Windows.Forms.Control.KeyPress event.
  [ComVisible(true)]
  public class KeyPressEventArgs : EventArgs {
    // Summary:
    //     Initializes a new instance of the System.Windows.Forms.KeyPressEventArgs
    //     class.
    //
    // Parameters:
    //   keyChar:
    //     The ASCII character corresponding to the key the user pressed.
    //[TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    //public KeyPressEventArgs(char keyChar);

    // Summary:
    //     Gets or sets a value indicating whether the System.Windows.Forms.Control.KeyPress
    //     event was handled.
    //
    // Returns:
    //     true if the event is handled; otherwise, false.
    public bool Handled { get { return default(bool); } set { } }
    //
    // Summary:
    //     Gets or sets the character corresponding to the key pressed.
    //
    // Returns:
    //     The ASCII character that is composed. For example, if the user presses SHIFT
    //     + K, this property returns an uppercase K.
    public char KeyChar { get { return default(char); } set { } }
  }
}
