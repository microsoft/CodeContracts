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
using System.Drawing;
using System.Diagnostics.Contracts;

namespace System.Drawing.Imaging
{
  // Summary:
  //     Defines an array of colors that make up a color palette. The colors are 32-bit
  //     ARGB colors. Not inheritable.
  public sealed class ColorPalette
  {
    internal ColorPalette() { }

    // Summary:
    //     Gets an array of System.Drawing.Color structures.
    //
    // Returns:
    //     The array of System.Drawing.Color structure that make up this System.Drawing.Imaging.ColorPalette.
    public Color[] Entries 
    {
      get
      {
        Contract.Ensures(Contract.Result<Color[]>() != null);

        return default(Color[]);
      }
    }
    //
    // Summary:
    //     Gets a value that specifies how to interpret the color information in the
    //     array of colors.
    //
    // Returns:
    //     The following flag values are valid: 0x00000001The color values in the array
    //     contain alpha information. 0x00000002The colors in the array are grayscale
    //     values. 0x00000004The colors in the array are halftone values.
    //public int Flags { get; }
  }
}
