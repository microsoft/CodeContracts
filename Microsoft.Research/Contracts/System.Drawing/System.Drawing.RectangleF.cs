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

#region Assembly System.Drawing.dll, v4.0.30319
// C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0\System.Drawing.dll
#endregion

using System;
using System.ComponentModel;
using System.Runtime;
using System.Diagnostics.Contracts;

namespace System.Drawing {
  // Summary:
  //     Stores a set of four floating-point numbers that represent the location and
  //     size of a rectangle. For more advanced region functions, use a System.Drawing.Region
  //     object.
  [Serializable]
  public struct RectangleF {
    // Summary:
    //     Represents an instance of the System.Drawing.RectangleF class with its members
    //     uninitialized.
    public static readonly RectangleF Empty;

    //
    // Summary:
    //     Initializes a new instance of the System.Drawing.RectangleF class with the
    //     specified location and size.
    //
    // Parameters:
    //   location:
    //     A System.Drawing.PointF that represents the upper-left corner of the rectangular
    //     region.
    //
    //   size:
    //     A System.Drawing.SizeF that represents the width and height of the rectangular
    //     region.
    //public RectangleF(PointF location, SizeF size);
    //
    // Summary:
    //     Initializes a new instance of the System.Drawing.RectangleF class with the
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
    //[TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    //public RectangleF(float x, float y, float width, float height);

    // Summary:
    //     Tests whether two System.Drawing.RectangleF structures differ in location
    //     or size.
    //
    // Parameters:
    //   left:
    //     The System.Drawing.RectangleF structure that is to the left of the inequality
    //     operator.
    //
    //   right:
    //     The System.Drawing.RectangleF structure that is to the right of the inequality
    //     operator.
    //
    // Returns:
    //     This operator returns true if any of the System.Drawing.RectangleF.X , System.Drawing.RectangleF.Y,
    //     System.Drawing.RectangleF.Width, or System.Drawing.RectangleF.Height properties
    //     of the two System.Drawing.Rectangle structures are unequal; otherwise false.
    //public static bool operator !=(RectangleF left, RectangleF right);
    //
    // Summary:
    //     Tests whether two System.Drawing.RectangleF structures have equal location
    //     and size.
    //
    // Parameters:
    //   left:
    //     The System.Drawing.RectangleF structure that is to the left of the equality
    //     operator.
    //
    //   right:
    //     The System.Drawing.RectangleF structure that is to the right of the equality
    //     operator.
    //
    // Returns:
    //     This operator returns true if the two specified System.Drawing.RectangleF
    //     structures have equal System.Drawing.RectangleF.X, System.Drawing.RectangleF.Y,
    //     System.Drawing.RectangleF.Width, and System.Drawing.RectangleF.Height properties.
    //public static bool operator ==(RectangleF left, RectangleF right);
    //
    // Summary:
    //     Converts the specified System.Drawing.Rectangle structure to a System.Drawing.RectangleF
    //     structure.
    //
    // Parameters:
    //   r:
    //     The System.Drawing.Rectangle structure to convert.
    //
    // Returns:
    //     The System.Drawing.RectangleF structure that is converted from the specified
    //     System.Drawing.Rectangle structure.
    //public static implicit operator RectangleF(Rectangle r);

    // Summary:
    //     Gets the y-coordinate that is the sum of System.Drawing.RectangleF.Y and
    //     System.Drawing.RectangleF.Height of this System.Drawing.RectangleF structure.
    //
    // Returns:
    //     The y-coordinate that is the sum of System.Drawing.RectangleF.Y and System.Drawing.RectangleF.Height
    //     of this System.Drawing.RectangleF structure.
    //[Browsable(false)]
    //public float Bottom { get; }
    //
    // Summary:
    //     Gets or sets the height of this System.Drawing.RectangleF structure.
    //
    // Returns:
    //     The height of this System.Drawing.RectangleF structure. The default is 0.
    //public float Height { get; set; }
    //
    // Summary:
    //     Tests whether the System.Drawing.RectangleF.Width or System.Drawing.RectangleF.Height
    //     property of this System.Drawing.RectangleF has a value of zero.
    //
    // Returns:
    //     This property returns true if the System.Drawing.RectangleF.Width or System.Drawing.RectangleF.Height
    //     property of this System.Drawing.RectangleF has a value of zero; otherwise,
    //     false.
    //[Browsable(false)]
    //public bool IsEmpty { get; }
    //
    // Summary:
    //     Gets the x-coordinate of the left edge of this System.Drawing.RectangleF
    //     structure.
    //
    // Returns:
    //     The x-coordinate of the left edge of this System.Drawing.RectangleF structure.
    //[Browsable(false)]
    //public float Left { get; }
    //
    // Summary:
    //     Gets or sets the coordinates of the upper-left corner of this System.Drawing.RectangleF
    //     structure.
    //
    // Returns:
    //     A System.Drawing.PointF that represents the upper-left corner of this System.Drawing.RectangleF
    //     structure.
    //[Browsable(false)]
    //public PointF Location { get; set; }
    //
    // Summary:
    //     Gets the x-coordinate that is the sum of System.Drawing.RectangleF.X and
    //     System.Drawing.RectangleF.Width of this System.Drawing.RectangleF structure.
    //
    // Returns:
    //     The x-coordinate that is the sum of System.Drawing.RectangleF.X and System.Drawing.RectangleF.Width
    //     of this System.Drawing.RectangleF structure.
    //[Browsable(false)]
    //public float Right { get; }
    //
    // Summary:
    //     Gets or sets the size of this System.Drawing.RectangleF.
    //
    // Returns:
    //     A System.Drawing.SizeF that represents the width and height of this System.Drawing.RectangleF
    //     structure.
    //[Browsable(false)]
    //public SizeF Size { get; set; }
    //
    // Summary:
    //     Gets the y-coordinate of the top edge of this System.Drawing.RectangleF structure.
    //
    // Returns:
    //     The y-coordinate of the top edge of this System.Drawing.RectangleF structure.
    //[Browsable(false)]
    //public float Top { get; }
    //
    // Summary:
    //     Gets or sets the width of this System.Drawing.RectangleF structure.
    //
    // Returns:
    //     The width of this System.Drawing.RectangleF structure. The default is 0.
    //public float Width { get; set; }
    //
    // Summary:
    //     Gets or sets the x-coordinate of the upper-left corner of this System.Drawing.RectangleF
    //     structure.
    //
    // Returns:
    //     The x-coordinate of the upper-left corner of this System.Drawing.RectangleF
    //     structure. The default is 0.
    //public float X { get; set; }
    //
    // Summary:
    //     Gets or sets the y-coordinate of the upper-left corner of this System.Drawing.RectangleF
    //     structure.
    //
    // Returns:
    //     The y-coordinate of the upper-left corner of this System.Drawing.RectangleF
    //     structure. The default is 0.
    //public float Y { get; set; }

    // Summary:
    //     Determines if the specified point is contained within this System.Drawing.RectangleF
    //     structure.
    //
    // Parameters:
    //   pt:
    //     The System.Drawing.PointF to test.
    //
    // Returns:
    //     This method returns true if the point represented by the pt parameter is
    //     contained within this System.Drawing.RectangleF structure; otherwise false.
    [Pure]
    public bool Contains(PointF pt) {
      return default(bool);
    }
    //
    // Summary:
    //     Determines if the rectangular region represented by rect is entirely contained
    //     within this System.Drawing.RectangleF structure.
    //
    // Parameters:
    //   rect:
    //     The System.Drawing.RectangleF to test.
    //
    // Returns:
    //     This method returns true if the rectangular region represented by rect is
    //     entirely contained within the rectangular region represented by this System.Drawing.RectangleF;
    //     otherwise false.
    [Pure]
    public bool Contains(RectangleF rect) {
      return default(bool);
    }
    //
    // Summary:
    //     Determines if the specified point is contained within this System.Drawing.RectangleF
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
    //     this System.Drawing.RectangleF structure; otherwise false.
    [Pure]
    public bool Contains(float x, float y) {
      return default(bool);
    }
    //
    // Summary:
    //     Tests whether obj is a System.Drawing.RectangleF with the same location and
    //     size of this System.Drawing.RectangleF.
    //
    // Parameters:
    //   obj:
    //     The System.Object to test.
    //
    // Returns:
    //     This method returns true if obj is a System.Drawing.RectangleF and its X,
    //     Y, Width, and Height properties are equal to the corresponding properties
    //     of this System.Drawing.RectangleF; otherwise, false.
    //public override bool Equals(object obj);
    //
    // Summary:
    //     Creates a System.Drawing.RectangleF structure with upper-left corner and
    //     lower-right corner at the specified locations.
    //
    // Parameters:
    //   left:
    //     The x-coordinate of the upper-left corner of the rectangular region.
    //
    //   top:
    //     The y-coordinate of the upper-left corner of the rectangular region.
    //
    //   right:
    //     The x-coordinate of the lower-right corner of the rectangular region.
    //
    //   bottom:
    //     The y-coordinate of the lower-right corner of the rectangular region.
    //
    // Returns:
    //     The new System.Drawing.RectangleF that this method creates.
    //[TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
    //public static RectangleF FromLTRB(float left, float top, float right, float bottom);
    //
    // Summary:
    //     Gets the hash code for this System.Drawing.RectangleF structure. For information
    //     about the use of hash codes, see Object.GetHashCode.
    //
    // Returns:
    //     The hash code for this System.Drawing.RectangleF.
    //public override int GetHashCode();
    //
    // Summary:
    //     Enlarges this System.Drawing.RectangleF by the specified amount.
    //
    // Parameters:
    //   size:
    //     The amount to inflate this rectangle.
    //public void Inflate(SizeF size);
    //
    // Summary:
    //     Enlarges this System.Drawing.RectangleF structure by the specified amount.
    //
    // Parameters:
    //   x:
    //     The amount to inflate this System.Drawing.RectangleF structure horizontally.
    //
    //   y:
    //     The amount to inflate this System.Drawing.RectangleF structure vertically.
    //public void Inflate(float x, float y);
    //
    // Summary:
    //     Creates and returns an enlarged copy of the specified System.Drawing.RectangleF
    //     structure. The copy is enlarged by the specified amount and the original
    //     rectangle remains unmodified.
    //
    // Parameters:
    //   rect:
    //     The System.Drawing.RectangleF to be copied. This rectangle is not modified.
    //
    //   x:
    //     The amount to enlarge the copy of the rectangle horizontally.
    //
    //   y:
    //     The amount to enlarge the copy of the rectangle vertically.
    //
    // Returns:
    //     The enlarged System.Drawing.RectangleF.
    //public static RectangleF Inflate(RectangleF rect, float x, float y);
    //
    // Summary:
    //     Replaces this System.Drawing.RectangleF structure with the intersection of
    //     itself and the specified System.Drawing.RectangleF structure.
    //
    // Parameters:
    //   rect:
    //     The rectangle to intersect.
    //public void Intersect(RectangleF rect);
    //
    // Summary:
    //     Returns a System.Drawing.RectangleF structure that represents the intersection
    //     of two rectangles. If there is no intersection, and empty System.Drawing.RectangleF
    //     is returned.
    //
    // Parameters:
    //   a:
    //     A rectangle to intersect.
    //
    //   b:
    //     A rectangle to intersect.
    //
    // Returns:
    //     A third System.Drawing.RectangleF structure the size of which represents
    //     the overlapped area of the two specified rectangles.
    [Pure]
    public static RectangleF Intersect(RectangleF a, RectangleF b) {
      return default(RectangleF);
    }
    //
    // Summary:
    //     Determines if this rectangle intersects with rect.
    //
    // Parameters:
    //   rect:
    //     The rectangle to test.
    //
    // Returns:
    //     This method returns true if there is any intersection.
    [Pure]
    public bool IntersectsWith(RectangleF rect) {
      return default(bool);
    }
    //
    // Summary:
    //     Adjusts the location of this rectangle by the specified amount.
    //
    // Parameters:
    //   pos:
    //     The amount to offset the location.
    //public void Offset(PointF pos);
    //
    // Summary:
    //     Adjusts the location of this rectangle by the specified amount.
    //
    // Parameters:
    //   x:
    //     The amount to offset the location horizontally.
    //
    //   y:
    //     The amount to offset the location vertically.
    //public void Offset(float x, float y);
    //
    // Summary:
    //     Converts the Location and System.Drawing.Size of this System.Drawing.RectangleF
    //     to a human-readable string.
    //
    // Returns:
    //     A string that contains the position, width, and height of this System.Drawing.RectangleF
    //     structure. For example, "{X=20, Y=20, Width=100, Height=50}".
    //public override string ToString();
    //
    // Summary:
    //     Creates the smallest possible third rectangle that can contain both of two
    //     rectangles that form a union.
    //
    // Parameters:
    //   a:
    //     A rectangle to union.
    //
    //   b:
    //     A rectangle to union.
    //
    // Returns:
    //     A third System.Drawing.RectangleF structure that contains both of the two
    //     rectangles that form the union.
    [Pure]
    public static RectangleF Union(RectangleF a, RectangleF b) {
      return default(RectangleF);
    }
  }
}
