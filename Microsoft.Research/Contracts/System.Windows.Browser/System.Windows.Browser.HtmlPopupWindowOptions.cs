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
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace System.Windows.Browser
{
  // Summary:
  //     Provides options to control pop-up windows.
  public sealed class HtmlPopupWindowOptions
  {
    // Summary:
    //     Creates a new instance of the System.Windows.Browser.HtmlPopupWindowOptions
    //     class.
    extern public HtmlPopupWindowOptions();

    // Summary:
    //     Determines whether the Internet Explorer links toolbar or Firefox personal/bookmarks
    //     toolbar is shown in the pop-up window.
    //
    // Returns:
    //     true if Internet Explorer links toolbar or Firefox personal/bookmarks toolbar
    //     is shown in the pop-up window; otherwise, false.
    extern public bool Directories { get; set; }
    //
    // Summary:
    //     Sets the height of a pop-up window.
    //
    // Returns:
    //     Height of the pop-up window, in pixels.
    extern public int Height { get; set; }
    //
    // Summary:
    //     Sets the position of the left edge of a pop-up window.
    //
    // Returns:
    //     Horizontal distance from the left edge of the browser's document space to
    //     the left edge of the pop-up window.
    extern public int Left { get; set; }
    //
    // Summary:
    //     Sets the URL of a pop-up window.
    //
    // Returns:
    //     The URL of the pop-up window.
    extern public bool Location { get; set; }
    //
    // Summary:
    //     Controls the visibility of the menu bar in a pop-up window.
    //
    // Returns:
    //     true if the menu bar is visible in the pop-up window; otherwise, false.
    extern public bool Menubar { get; set; }
    //
    // Summary:
    //     Determines whether a pop-up window can be resized.
    //
    // Returns:
    //     true if the pop-up window can be resized; otherwise, false.
    extern public bool Resizeable { get; set; }
    //
    // Summary:
    //     Controls the visibility of scroll bars in a pop-up window.
    //
    // Returns:
    //     true if scroll bars may appear in the pop-up window; otherwise, false.
    extern public bool Scrollbars { get; set; }
    //
    // Summary:
    //     Controls the visibility of the status bar in a pop-up window.
    //
    // Returns:
    //     true if the status bar is visible in the pop-up window; otherwise, false.
    extern public bool Status { get; set; }
    //
    // Summary:
    //     Controls the visibility of the toolbar in a pop-up window.
    //
    // Returns:
    //     true if the toolbar is visible in the pop-up window; otherwise, false.
    extern public bool Toolbar { get; set; }
    //
    // Summary:
    //     Sets the position of the top edge of a pop-up window.
    //
    // Returns:
    //     Vertical distance from the top edge of the browser's document space to the
    //     top edge of the pop-up window.
    extern public int Top { get; set; }
    //
    // Summary:
    //     Sets the width of a pop-up window.
    //
    // Returns:
    //     Width of the pop-up window, in pixels.
    extern public int Width { get; set; }
  }
}

