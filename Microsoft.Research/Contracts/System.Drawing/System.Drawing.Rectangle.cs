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
using System.Diagnostics.Contracts;

namespace System.Drawing
{
  // Summary:
  //     Stores a set of four integers that represent the location and size of a rectangle.
  //     For more advanced region functions, use a System.Drawing.Region object.
  //[Serializable]
  //[TypeConverter(typeof(RectangleConverter))]
  //[ComVisible(true)]
  public struct Rectangle
  {
    // Summary:
    //     Represents a System.Drawing.Rectangle structure with its properties left
    //     uninitialized.
    ////public static readonly Rectangle Empty;

    //
    // Summary:
    //     Initializes a new instance of the System.Drawing.Rectangle class with the
    //     specified location and size.
    //
    // Parameters:
    //   location:
    //     A System.Drawing.Point that represents the upper-left corner of the rectangular
    //     region.
    //
    //   size:
    //     A System.Drawing.Size that represents the width and height of the rectangular
    //     region.
    ////public Rectangle(Point location, Size size);
    //
    // Summary:
    //     Initializes a new instance of the System.Drawing.Rectangle class with the
    //     specified location and size.
    //
    // Parameters:
    //   x:
    //     The x-coordinate of the upper-left corner of the rectangle.
    //
    //   y:
    //     The y-coordinate of the upper-left corner of the rectangle.
    //
    //   width:
    //     The width of the rectangle.
    //
    //   height:
    //     The height of the rectangle.
    ////public Rectangle(int x, int y, int width, int height);

    // Summary:
    //     Tests whether two System.Drawing.Rectangle structures differ in location
    //     or size.
    //
    // Parameters:
    //   left:
    //     The System.Drawing.Rectangle structure that is to the left of the inequality
    //     operator.
    //
    //   right:
    //     The System.Drawing.Rectangle structure that is to the right of the inequality
    //     operator.
    //
    // Returns:
    //     This operator returns true if any of the System.Drawing.Rectangle.X, System.Drawing.Rectangle.Y,
    //     System.Drawing.Rectangle.Width or System.Drawing.Rectangle.Height properties
    //     of the two System.Drawing.Rectangle structures are unequal; otherwise false.
    ////public static bool operator !=(Rectangle left, Rectangle right);
    //
    // Summary:
    //     Tests whether two System.Drawing.Rectangle structures have equal location
    //     and size.
    //
    // Parameters:
    //   left:
    //     The System.Drawing.Rectangle structure that is to the left of the equality
    //     operator.
    //
    //   right:
    //     The System.Drawing.Rectangle structure that is to the right of the equality
    //     operator.
    //
    // Returns:
    //     This operator returns true if the two System.Drawing.Rectangle structures
    //     have equal System.Drawing.Rectangle.X, System.Drawing.Rectangle.Y, System.Drawing.Rectangle.Width,
    //     and System.Drawing.Rectangle.Height properties.
    ////public static bool operator ==(Rectangle left, Rectangle right);

    // Summary:
    //     Gets the y-coordinate that is the sum of the System.Drawing.Rectangle.Y and
    //     System.Drawing.Rectangle.Height property values of this System.Drawing.Rectangle
    //     structure.
    //
    // Returns:
    //     The y-coordinate that is the sum of System.Drawing.Rectangle.Y and System.Drawing.Rectangle.Height
    //     of this System.Drawing.Rectangle.
    //[Browsable(false)]
    ////public int Bottom { get; }
    //
    // Summary:
    //     Gets or sets the height of this System.Drawing.Rectangle structure.
    //
    // Returns:
    //     The height of this System.Drawing.Rectangle structure.
    ////public int Height { get; set; }
    //
    // Summary:
    //     Tests whether all numeric properties of this System.Drawing.Rectangle have
    //     values of zero.
    //
    // Returns:
    //     This property returns true if the System.Drawing.Rectangle.Width, System.Drawing.Rectangle.Height,
    //     System.Drawing.Rectangle.X, and System.Drawing.Rectangle.Y properties of
    //     this System.Drawing.Rectangle all have values of zero; otherwise, false.
    //[Browsable(false)]
    ////public bool IsEmpty { get; }
    //
    // Summary:
    //     Gets the x-coordinate of the left edge of this System.Drawing.Rectangle structure.
    //
    // Returns:
    //     The x-coordinate of the left edge of this System.Drawing.Rectangle structure.
    //[Browsable(false)]
    ////public int Left { get; }
    //
    // Summary:
    //     Gets or sets the coordinates of the upper-left corner of this System.Drawing.Rectangle
    //     structure.
    //
    // Returns:
    //     A System.Drawing.Point that represents the upper-left corner of this System.Drawing.Rectangle
    //     structure.
    //[Browsable(false)]
    ////public Point Location { get; set; }
    //
    // Summary:
    //     Gets the x-coordinate that is the sum of System.Drawing.Rectangle.X and System.Drawing.Rectangle.Width
    //     property values of this System.Drawing.Rectangle structure.
    //
    // Returns:
    //     The x-coordinate that is the sum of System.Drawing.Rectangle.X and System.Drawing.Rectangle.Width
    //     of this System.Drawing.Rectangle.
    //[Browsable(false)]
    ////public int Right { get; }
    //
    // Summary:
    //     Gets or sets the size of this System.Drawing.Rectangle.
    //
    // Returns:
    //     A System.Drawing.Size that represents the width and height of this System.Drawing.Rectangle
    //     structure.
    //[Browsable(false)]
    ////public Size Size { get; set; }
    //
    // Summary:
    //     Gets the y-coordinate of the top edge of this System.Drawing.Rectangle structure.
    //
    // Returns:
    //     The y-coordinate of the top edge of this System.Drawing.Rectangle structure.
    //[Browsable(false)]
    ////public int Top { get; }
    //
    // Summary:
    //     Gets or sets the width of this System.Drawing.Rectangle structure.
    //
    // Returns:
    //     The width of this System.Drawing.Rectangle structure.
    ////public int Width { get; set; }
    //
    // Summary:
    //     Gets or sets the x-coordinate of the upper-left corner of this System.Drawing.Rectangle
    //     structure.
    //
    // Returns:
    //     The x-coordinate of the upper-left corner of this System.Drawing.Rectangle
    //     structure.
    ////public int X { get; set; }
    //
    // Summary:
    //     Gets or sets the y-coordinate of the upper-left corner of this System.Drawing.Rectangle
    //     structure.
    //
    // Returns:
    //     The y-coordinate of the upper-left corner of this System.Drawing.Rectangle
    //     structure.
    ////public int Y { get; set; }

    // Summary:
    //     Converts the specified System.Drawing.RectangleF structure to a System.Drawing.Rectangle
    //     structure by rounding the System.Drawing.RectangleF values to the next higher
    //     integer values.
    //
    // Parameters:
    //   value:
    //     The System.Drawing.RectangleF structure to be converted.
    //
    // Returns:
    //     Returns a System.Drawing.Rectangle.
    ////public static Rectangle Ceiling(RectangleF value);
    //
    // Summary:
    //     Determines if the specified point is contained within this System.Drawing.Rectangle
    //     structure.
    //
    // Parameters:
    //   pt:
    //     The System.Drawing.Point to test.
    //
    // Returns:
    //     This method returns true if the point represented by pt is contained within
    //     this System.Drawing.Rectangle structure; otherwise false.
    [Pure]
    public bool Contains(Point pt) {
      return default(bool);
    }
    //
    // Summary:
    //     Determines if the rectangular region represented by rect is entirely contained
    //     within this System.Drawing.Rectangle structure.
    //
    // Parameters:
    //   rect:
    //     The System.Drawing.Rectangle to test.
    //
    // Returns:
    //     This method returns true if the rectangular region represented by rect is
    //     entirely contained within this System.Drawing.Rectangle structure; otherwise
    //     false.
    [Pure]
    public bool Contains(Rectangle rect) {
      return default(bool);
    }
    //
    // Summary:
    //     Determines if the specified point is contained within this System.Drawing.Rectangle
    //     structure.
    //
    // Parameters:
    //   x:
    //     The x-coordinate of the point to test.
    //
    //   y:
    //     The y-coordinate of the point to test.
    //
    // Returns:
    //     This method returns true if the point defined by x and y is contained within
    //     this System.Drawing.Rectangle structure; otherwise false.
    [Pure]
    public bool Contains(int x, int y) {
      return default(bool);
    }
    //
    // Summary:
    //     Tests whether obj is a System.Drawing.Rectangle structure with the same location
    //     and size of this System.Drawing.Rectangle structure.
    //
    // Parameters:
    //   obj:
    //     The System.Object to test.
    //
    // Returns:
    //     This method returns true if obj is a System.Drawing.Rectangle structure and
    //     its System.Drawing.Rectangle.X, System.Drawing.Rectangle.Y, System.Drawing.Rectangle.Width,
    //     and System.Drawing.Rectangle.Height properties are equal to the corresponding
    //     properties of this System.Drawing.Rectangle structure; otherwise, false.
    ////public override bool Equals(object obj);
    //
    // Summary:
    //     Creates a System.Drawing.Rectangle structure with the specified edge locations.
    //
    // Parameters:
    //   left:
    //     The x-coordinate of the upper-left corner of this System.Drawing.Rectangle
    //     structure.
    //
    //   top:
    //     The y-coordinate of the upper-left corner of this System.Drawing.Rectangle
    //     structure.
    //
    //   right:
    //     The x-coordinate of the lower-right corner of this System.Drawing.Rectangle
    //     structure.
    //
    //   bottom:
    //     The y-coordinate of the lower-right corner of this System.Drawing.Rectangle
    //     structure.
    //
    // Returns:
    //     The new System.Drawing.Rectangle that this method creates.
    //public static Rectangle FromLTRB(int left, int top, int right, int bottom);
    //
    // Summary:
    //     Returns the hash code for this System.Drawing.Rectangle structure. For information
    //     about the use of hash codes, see System.Object.GetHashCode() .
    //
    // Returns:
    //     An integer that represents the hash code for this rectangle.
    //public override int GetHashCode();
    //
    // Summary:
    //     Inflates this System.Drawing.Rectangle by the specified amount.
    //
    // Parameters:
    //   size:
    //     The amount to inflate this rectangle.
    //public void Inflate(Size size);
    //
    // Summary:
    //     Inflates this System.Drawing.Rectangle by the specified amount.
    //
    // Parameters:
    //   width:
    //     The amount to inflate this System.Drawing.Rectangle horizontally.
    //
    //   height:
    //     The amount to inflate this System.Drawing.Rectangle vertically.
    //public void Inflate(int width, int height);
    //
    // Summary:
    //     Creates and returns an inflated copy of the specified System.Drawing.Rectangle
    //     structure. The copy is inflated by the specified amount. The original System.Drawing.Rectangle
    //     structure remains unmodified.
    //
    // Parameters:
    //   rect:
    //     The System.Drawing.Rectangle with which to start. This rectangle is not modified.
    //
    //   x:
    //     The amount to inflate this System.Drawing.Rectangle horizontally.
    //
    //   y:
    //     The amount to inflate this System.Drawing.Rectangle vertically.
    //
    // Returns:
    //     The inflated System.Drawing.Rectangle.
    //public static Rectangle Inflate(Rectangle rect, int x, int y);
    //
    // Summary:
    //     Replaces this System.Drawing.Rectangle with the intersection of itself and
    //     the specified System.Drawing.Rectangle.
    //
    // Parameters:
    //   rect:
    //     The System.Drawing.Rectangle with which to intersect.
    //public void Intersect(Rectangle rect);
    //
    // Summary:
    //     Returns a third System.Drawing.Rectangle structure that represents the intersection
    //     of two other System.Drawing.Rectangle structures. If there is no intersection,
    //     an empty System.Drawing.Rectangle is returned.
    //
    // Parameters:
    //   a:
    //     A rectangle to intersect.
    //
    //   b:
    //     A rectangle to intersect.
    //
    // Returns:
    //     A System.Drawing.Rectangle that represents the intersection of a and b.
    //public static Rectangle Intersect(Rectangle a, Rectangle b);
    //
    // Summary:
    //     Determines if this rectangle intersects with rect.
    //
    // Parameters:
    //   rect:
    //     The rectangle to test.
    //
    // Returns:
    //     This method returns true if there is any intersection, otherwise false.
    [Pure]
    public bool IntersectsWith(Rectangle rect) {
      return default(bool);
    }
    //
    // Summary:
    //     Adjusts the location of this rectangle by the specified amount.
    //
    // Parameters:
    //   pos:
    //     Amount to offset the location.
    //public void Offset(Point pos);
    //
    // Summary:
    //     Adjusts the location of this rectangle by the specified amount.
    //
    // Parameters:
    //   x:
    //     The horizontal offset.
    //
    //   y:
    //     The vertical offset.
    //public void Offset(int x, int y);
    //
    // Summary:
    //     Converts the specified System.Drawing.RectangleF to a System.Drawing.Rectangle
    //     by rounding the System.Drawing.RectangleF values to the nearest integer values.
    //
    // Parameters:
    //   value:
    //     The System.Drawing.RectangleF to be converted.
    //
    // Returns:
    //     A System.Drawing.Rectangle.
    //public static Rectangle Round(RectangleF value);
    //
    // Summary:
    //     Converts the attributes of this System.Drawing.Rectangle to a human-readable
    //     string.
    //
    // Returns:
    //     A string that contains the position, width, and height of this System.Drawing.Rectangle
    //     structure ¾ for example, {X=20, Y=20, Width=100, Height=50}
    //public override string ToString();
    //
    // Summary:
    //     Converts the specified System.Drawing.RectangleF to a System.Drawing.Rectangle
    //     by truncating the System.Drawing.RectangleF values.
    //
    // Parameters:
    //   value:
    //     The System.Drawing.RectangleF to be converted.
    //
    // Returns:
    //     A System.Drawing.Rectangle.
    //public static Rectangle Truncate(RectangleF value);
    //
    // Summary:
    //     Gets a System.Drawing.Rectangle structure that contains the union of two
    //     System.Drawing.Rectangle structures.
    //
    // Parameters:
    //   a:
    //     A rectangle to union.
    //
    //   b:
    //     A rectangle to union.
    //
    // Returns:
    //     A System.Drawing.Rectangle structure that bounds the union of the two System.Drawing.Rectangle
    //     structures.
    [Pure]
    public static Rectangle Union(Rectangle a, Rectangle b) {
      return default(Rectangle);
    }
  }
}
