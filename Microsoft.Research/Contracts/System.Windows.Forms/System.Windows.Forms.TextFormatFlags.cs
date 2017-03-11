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
    /// Specifies the display and layout information for text strings.
    /// </summary>
    public enum TextFormatFlags
    {
        Bottom = 8,
        EndEllipsis = 32768,
        ExpandTabs = 64,
        ExternalLeading = 512,
        Default = 0,
        HidePrefix = 1048576,
        HorizontalCenter = 1,
        Internal = 4096,
        Left = 0,
        ModifyString = 65536,
        NoClipping = 256,
        NoPrefix = 2048,
        NoFullWidthCharacterBreak = 524288,
        PathEllipsis = 16384,
        PrefixOnly = 2097152,
        Right = 2,
        RightToLeft = 131072,
        SingleLine = 32,
        TextBoxControl = 8192,
        Top = 0,
        VerticalCenter = 4,
        WordBreak = 16,
        WordEllipsis = 262144,
        PreserveGraphicsClipping = 16777216,
        PreserveGraphicsTranslateTransform = 33554432,
        GlyphOverhangPadding = 0,
        NoPadding = 268435456,
        LeftAndRightPadding = 536870912,
    }
}
