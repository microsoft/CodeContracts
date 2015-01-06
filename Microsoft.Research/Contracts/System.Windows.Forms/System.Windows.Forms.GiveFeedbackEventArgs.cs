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
  //     Provides data for the System.Windows.Forms.Control.GiveFeedback event, which
  //     occurs during a drag operation.
  [ComVisible(true)]
  public class GiveFeedbackEventArgs : EventArgs {
    // Summary:
    //     Initializes a new instance of the System.Windows.Forms.GiveFeedbackEventArgs
    //     class.
    //
    // Parameters:
    //   effect:
    //     The type of drag-and-drop operation. Possible values are obtained by applying
    //     the bitwise OR (|) operation to the constants defined in the System.Windows.Forms.DragDropEffects.
    //
    //   useDefaultCursors:
    //     true if default pointers are used; otherwise, false.
    //[TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    //public GiveFeedbackEventArgs(DragDropEffects effect, bool useDefaultCursors);

    // Summary:
    //     Gets the drag-and-drop operation feedback that is displayed.
    //
    // Returns:
    //     One of the System.Windows.Forms.DragDropEffects values.
    //public DragDropEffects Effect { get; }
    //
    // Summary:
    //     Gets or sets whether drag operation should use the default cursors that are
    //     associated with drag-drop effects.
    //
    // Returns:
    //     true if the default pointers are used; otherwise, false.
    public bool UseDefaultCursors { get { return default(bool); } set { } }
  }
}
