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
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace System.Drawing
{
  // Summary:
  //     Stores an ordered pair of integers, typically the width and height of a rectangle.
  //[Serializable]
  //[TypeConverter(typeof(SizeConverter))]
  //[ComVisible(true)]
  public struct Size
  {
    // Summary:
    //     Initializes a new instance of the System.Drawing.Size class.
    //public static readonly Size Empty;

    //
    // Summary:
    //     Initializes a new instance of the System.Drawing.Size class from the specified
    //     System.Drawing.Point.
    //
    // Parameters:
    //   pt:
    //     The System.Drawing.Point from which to initialize this System.Drawing.Size.
    //public Size(Point pt);
    //
    // Summary:
    //     Initializes a new instance of the System.Drawing.Size class from the specified
    //     dimensions.
    //
    // Parameters:
    //   width:
    //     The width component of the new System.Drawing.Size.
    //
    //   height:
    //     The height component of the new System.Drawing.Size.
    //public Size(int width, int height);

    // Summary:
    //     Subtracts the width and height of one System.Drawing.Size structure from
    //     the width and height of another System.Drawing.Size structure.
    //
    // Parameters:
    //   sz1:
    //     The System.Drawing.Size structure on the left side of the subtraction operator.
    //
    //   sz2:
    //     The System.Drawing.Size structure on the right side of the subtraction operator.
    //
    // Returns:
    //     A System.Drawing.Size structure that is the result of the subtraction operation.
    //public static Size operator -(Size sz1, Size sz2);
    //
    // Summary:
    //     Tests whether two System.Drawing.Size structures are different.
    //
    // Parameters:
    //   sz1:
    //     The System.Drawing.Size structure on the left of the inequality operator.
    //
    //   sz2:
    //     The System.Drawing.Size structure on the right of the inequality operator.
    //
    // Returns:
    //     true if sz1 and sz2 differ either in width or height; false if sz1 and sz2
    //     are equal.
    //public static bool operator !=(Size sz1, Size sz2);
    //
    // Summary:
    //     Adds the width and height of one System.Drawing.Size structure to the width
    //     and height of another System.Drawing.Size structure.
    //
    // Parameters:
    //   sz1:
    //     The first System.Drawing.Size to add.
    //
    //   sz2:
    //     The second System.Drawing.Size to add.
    //
    // Returns:
    //     A System.Drawing.Size structure that is the result of the addition operation.
    //public static Size operator +(Size sz1, Size sz2);
    //
    // Summary:
    //     Tests whether two System.Drawing.Size structures are equal.
    //
    // Parameters:
    //   sz1:
    //     The System.Drawing.Size structure on the left side of the equality operator.
    //
    //   sz2:
    //     The System.Drawing.Size structure on the right of the equality operator.
    //
    // Returns:
    //     true if sz1 and sz2 have equal width and height; otherwise, false.
    //public static bool operator ==(Size sz1, Size sz2);
    //
    // Summary:
    //     Converts the specified System.Drawing.Size to a System.Drawing.Point.
    //
    // Parameters:
    //   size:
    //     The System.Drawing.Size to convert.
    //
    // Returns:
    //     The System.Drawing.Point structure to which this operator converts.
    //public static explicit operator Point(Size size);
    //
    // Summary:
    //     Converts the specified System.Drawing.Size to a System.Drawing.SizeF.
    //
    // Parameters:
    //   p:
    //     The System.Drawing.Size to convert.
    //
    // Returns:
    //     The System.Drawing.SizeF structure to which this operator converts.
    //public static implicit operator SizeF(Size p);

    // Summary:
    //     Gets or sets the vertical component of this System.Drawing.Size.
    //
    // Returns:
    //     The vertical component of this System.Drawing.Size, typically measured in
    //     pixels.
    //public int Height { get; set; }
    //
    // Summary:
    //     Tests whether this System.Drawing.Size has width and height of 0.
    //
    // Returns:
    //     This property returns true when this System.Drawing.Size has both a width
    //     and height of 0; otherwise, false.
    //[Browsable(false)]
    //public bool IsEmpty { get; }
    //
    // Summary:
    //     Gets or sets the horizontal component of this System.Drawing.Size.
    //
    // Returns:
    //     The horizontal component of this System.Drawing.Size, typically measured
    //     in pixels.
    //public int Width { get; set; }

    // Summary:
    //     Adds the width and height of one System.Drawing.Size structure to the width
    //     and height of another System.Drawing.Size structure.
    //
    // Parameters:
    //   sz1:
    //     The first System.Drawing.Size to add.
    //
    //   sz2:
    //     The second System.Drawing.Size to add.
    //
    // Returns:
    //     A System.Drawing.Size structure that is the result of the addition operation.
    //public static Size Add(Size sz1, Size sz2);
    //
    // Summary:
    //     Converts the specified System.Drawing.SizeF structure to a System.Drawing.Size
    //     structure by rounding the values of the System.Drawing.Size structure to
    //     the next higher integer values.
    //
    // Parameters:
    //   value:
    //     The System.Drawing.SizeF structure to convert.
    //
    // Returns:
    //     The System.Drawing.Size structure this method converts to.
    //public static Size Ceiling(SizeF value);
    //
    // Summary:
    //     Tests to see whether the specified object is a System.Drawing.Size with the
    //     same dimensions as this System.Drawing.Size.
    //
    // Parameters:
    //   obj:
    //     The System.Object to test.
    //
    // Returns:
    //     true if obj is a System.Drawing.Size and has the same width and height as
    //     this System.Drawing.Size; otherwise, false.
    //public override bool Equals(object obj);
    //
    // Summary:
    //     Returns a hash code for this System.Drawing.Size structure.
    //
    // Returns:
    //     An integer value that specifies a hash value for this System.Drawing.Size
    //     structure.
    //public override int GetHashCode();
    //
    // Summary:
    //     Converts the specified System.Drawing.SizeF structure to a System.Drawing.Size
    //     structure by rounding the values of the System.Drawing.SizeF structure to
    //     the nearest integer values.
    //
    // Parameters:
    //   value:
    //     The System.Drawing.SizeF structure to convert.
    //
    // Returns:
    //     The System.Drawing.Size structure this method converts to.
    //public static Size Round(SizeF value);
    //
    // Summary:
    //     Subtracts the width and height of one System.Drawing.Size structure from
    //     the width and height of another System.Drawing.Size structure.
    //
    // Parameters:
    //   sz1:
    //     The System.Drawing.Size structure on the left side of the subtraction operator.
    //
    //   sz2:
    //     The System.Drawing.Size structure on the right side of the subtraction operator.
    //
    // Returns:
    //     The System.Drawing.Size that is a result of the subtraction operation.
    //public static Size Subtract(Size sz1, Size sz2);
    //
    // Summary:
    //     Creates a human-readable string that represents this System.Drawing.Size.
    //
    // Returns:
    //     A string that represents this System.Drawing.Size.
    //public override string ToString();
    //
    // Summary:
    //     Converts the specified System.Drawing.SizeF structure to a System.Drawing.Size
    //     structure by truncating the values of the System.Drawing.SizeF structure
    //     to the next lower integer values.
    //
    // Parameters:
    //   value:
    //     The System.Drawing.SizeF structure to convert.
    //
    // Returns:
    //     The System.Drawing.Size structure this method converts to.
    //public static Size Truncate(SizeF value);
  }
}
