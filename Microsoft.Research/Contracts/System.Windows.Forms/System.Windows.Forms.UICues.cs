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

namespace System.Windows.Forms {
  // Summary:
  //     Specifies the state of the user interface.
  [Flags]
  public enum UICues {
    // Summary:
    //     No change was made.
    None = 0,
    //
    // Summary:
    //     Focus rectangles are displayed after the change.
    ShowFocus = 1,
    //
    // Summary:
    //     Keyboard cues are underlined after the change.
    ShowKeyboard = 2,
    //
    // Summary:
    //     Focus rectangles are displayed and keyboard cues are underlined after the
    //     change.
    Shown = 3,
    //
    // Summary:
    //     The state of the focus cues has changed.
    ChangeFocus = 4,
    //
    // Summary:
    //     The state of the keyboard cues has changed.
    ChangeKeyboard = 8,
    //
    // Summary:
    //     The state of the focus cues and keyboard cues has changed.
    Changed = 12,
  }
}
