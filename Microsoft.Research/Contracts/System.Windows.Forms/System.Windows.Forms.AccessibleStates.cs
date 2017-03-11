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

namespace System.Windows.Forms
{
    /// <summary>
    /// Specifies values representing possible states for an accessible object.
    /// </summary>
    [Flags]
    public enum AccessibleStates
    {
        None = 0,
        Unavailable = 1,
        Selected = 2,
        Focused = 4,
        Pressed = 8,
        Checked = 16,
        Mixed = 32,
        Indeterminate = Mixed,
        ReadOnly = 64,
        HotTracked = 128,
        Default = 256,
        Expanded = 512,
        Collapsed = 1024,
        Busy = 2048,
        Floating = 4096,
        Marqueed = 8192,
        Animated = 16384,
        Invisible = 32768,
        Offscreen = 65536,
        Sizeable = 131072,
        Moveable = 262144,
        SelfVoicing = 524288,
        Focusable = 1048576,
        Selectable = 2097152,
        Linked = 4194304,
        Traversed = 8388608,
        MultiSelectable = 16777216,
        ExtSelectable = 33554432,
        AlertLow = 67108864,
        AlertMedium = 134217728,
        AlertHigh = 268435456,
        Protected = 536870912,
        HasPopup = 1073741824,
        // [Obsolete("This enumeration value has been deprecated. There is no replacement. http://go.microsoft.com/fwlink/?linkid=14202")]
        Valid = Protected | AlertHigh | AlertMedium | AlertLow | ExtSelectable | MultiSelectable | Traversed | Linked | Selectable | Focusable | SelfVoicing | Moveable | Sizeable | Offscreen | Invisible | Animated | Marqueed | Floating | Busy | Collapsed | Expanded | Default | HotTracked | ReadOnly | Indeterminate | Checked | Pressed | Focused | Selected | Unavailable,
    }
}
