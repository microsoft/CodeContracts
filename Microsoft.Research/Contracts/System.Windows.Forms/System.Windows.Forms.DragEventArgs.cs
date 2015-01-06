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
  //     Provides data for the System.Windows.Forms.Control.DragDrop, System.Windows.Forms.Control.DragEnter,
  //     or System.Windows.Forms.Control.DragOver event.
  [ComVisible(true)]
  public class DragEventArgs : EventArgs {
    // Summary:
    //     Initializes a new instance of the System.Windows.Forms.DragEventArgs class.
    //
    // Parameters:
    //   data:
    //     The data associated with this event.
    //
    //   keyState:
    //     The current state of the SHIFT, CTRL, and ALT keys.
    //
    //   x:
    //     The x-coordinate of the mouse cursor in pixels.
    //
    //   y:
    //     The y-coordinate of the mouse cursor in pixels.
    //
    //   allowedEffect:
    //     One of the System.Windows.Forms.DragDropEffects values.
    //
    //   effect:
    //     One of the System.Windows.Forms.DragDropEffects values.
    //[TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    //public DragEventArgs(IDataObject data, int keyState, int x, int y, DragDropEffects allowedEffect, DragDropEffects effect);

    // Summary:
    //     Gets which drag-and-drop operations are allowed by the originator (or source)
    //     of the drag event.
    //
    // Returns:
    //     One of the System.Windows.Forms.DragDropEffects values.
    //public DragDropEffects AllowedEffect { get; }
    //
    // Summary:
    //     Gets the System.Windows.Forms.IDataObject that contains the data associated
    //     with this event.
    //
    // Returns:
    //     The data associated with this event.
    //public IDataObject Data { get; }
    //
    // Summary:
    //     Gets or sets the target drop effect in a drag-and-drop operation.
    //
    // Returns:
    //     One of the System.Windows.Forms.DragDropEffects values.
    //public DragDropEffects Effect { get; set; }
    //
    // Summary:
    //     Gets the current state of the SHIFT, CTRL, and ALT keys, as well as the state
    //     of the mouse buttons.
    //
    // Returns:
    //     The current state of the SHIFT, CTRL, and ALT keys and of the mouse buttons.
    //public int KeyState { get; }
    //
    // Summary:
    //     Gets the x-coordinate of the mouse pointer, in screen coordinates.
    //
    // Returns:
    //     The x-coordinate of the mouse pointer in pixels.
    //public int X { get; }
    //
    // Summary:
    //     Gets the y-coordinate of the mouse pointer, in screen coordinates.
    //
    // Returns:
    //     The y-coordinate of the mouse pointer in pixels.
    //public int Y { get; }
  }
}
