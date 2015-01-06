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
using System.Runtime.InteropServices;

namespace System.Drawing {
  // Summary:
  //     Represents an ordered pair of integer x- and y-coordinates that defines a
  //     point in a two-dimensional plane.
  [Serializable]
  [ComVisible(true)]
  //[TypeConverter(typeof(PointConverter))]
  public struct Point {
    // Summary:
    //     Represents a System.Drawing.Point that has System.Drawing.Point.X and System.Drawing.Point.Y
    //     values set to zero.
    public static readonly Point Empty;

    //
    // Summary:
    //     Initializes a new instance of the System.Drawing.Point class using coordinates
    //     specified by an integer value.
    //
    // Parameters:
    //   dw:
    //     A 32-bit integer that specifies the coordinates for the new System.Drawing.Point.
    //public Point(int dw);
    //
    // Summary:
    //     Initializes a new instance of the System.Drawing.Point class from a System.Drawing.Size.
    //
    // Parameters:
    //   sz:
    //     A System.Drawing.Size that specifies the coordinates for the new System.Drawing.Point.
    //public Point(Size sz);
    //
    // Summary:
    //     Initializes a new instance of the System.Drawing.Point class with the specified
    //     coordinates.
    //
    // Parameters:
    //   x:
    //     The horizontal position of the point.
    //
    //   y:
    //     The vertical position of the point.
    //[TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    //public Point(int x, int y);

    // Summary:
    //     Translates a System.Drawing.Point by the negative of a given System.Drawing.Size.
    //
    // Parameters:
    //   pt:
    //     The System.Drawing.Point to translate.
    //
    //   sz:
    //     A System.Drawing.Size that specifies the pair of numbers to subtract from
    //     the coordinates of pt.
    //
    // Returns:
    //     A System.Drawing.Point structure that is translated by the negative of a
    //     given System.Drawing.Size structure.
    //[TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    public static Point operator -(Point pt, Size sz) {
      return default(Point);
    }
    //
    // Summary:
    //     Compares two System.Drawing.Point objects. The result specifies whether the
    //     values of the System.Drawing.Point.X or System.Drawing.Point.Y properties
    //     of the two System.Drawing.Point objects are unequal.
    //
    // Parameters:
    //   left:
    //     A System.Drawing.Point to compare.
    //
    //   right:
    //     A System.Drawing.Point to compare.
    //
    // Returns:
    //     true if the values of either the System.Drawing.Point.X properties or the
    //     System.Drawing.Point.Y properties of left and right differ; otherwise, false.
    //[TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
    public static bool operator !=(Point left, Point right) {
      return default(bool);
    }
    //
    // Summary:
    //     Translates a System.Drawing.Point by a given System.Drawing.Size.
    //
    // Parameters:
    //   pt:
    //     The System.Drawing.Point to translate.
    //
    //   sz:
    //     A System.Drawing.Size that specifies the pair of numbers to add to the coordinates
    //     of pt.
    //
    // Returns:
    //     The translated System.Drawing.Point.
    //[TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    public static Point operator +(Point pt, Size sz) {
      return default(Point);
    }
    //
    // Summary:
    //     Compares two System.Drawing.Point objects. The result specifies whether the
    //     values of the System.Drawing.Point.X and System.Drawing.Point.Y properties
    //     of the two System.Drawing.Point objects are equal.
    //
    // Parameters:
    //   left:
    //     A System.Drawing.Point to compare.
    //
    //   right:
    //     A System.Drawing.Point to compare.
    //
    // Returns:
    //     true if the System.Drawing.Point.X and System.Drawing.Point.Y values of left
    //     and right are equal; otherwise, false.
    public static bool operator ==(Point left, Point right) {
      return default(bool);
    }
    //
    // Summary:
    //     Converts the specified System.Drawing.Point structure to a System.Drawing.Size
    //     structure.
    //
    // Parameters:
    //   p:
    //     The System.Drawing.Point to be converted.
    //
    // Returns:
    //     The System.Drawing.Size that results from the conversion.
    public static explicit operator Size(Point p) {
      return default(Size);
    }
    //
    // Summary:
    //     Converts the specified System.Drawing.Point structure to a System.Drawing.PointF
    //     structure.
    //
    // Parameters:
    //   p:
    //     The System.Drawing.Point to be converted.
    //
    // Returns:
    //     The System.Drawing.PointF that results from the conversion.
    public static implicit operator PointF(Point p) {
      return default(PointF);
    }

    // Summary:
    //     Gets a value indicating whether this System.Drawing.Point is empty.
    //
    // Returns:
    //     true if both System.Drawing.Point.X and System.Drawing.Point.Y are 0; otherwise,
    //     false.
    [Browsable(false)]
    public bool IsEmpty {
      get {
        return default(bool);
      }
    }
    //
    // Summary:
    //     Gets or sets the x-coordinate of this System.Drawing.Point.
    //
    // Returns:
    //     The x-coordinate of this System.Drawing.Point.
    public int X { get { return default(int); } set { } }
    //
    // Summary:
    //     Gets or sets the y-coordinate of this System.Drawing.Point.
    //
    // Returns:
    //     The y-coordinate of this System.Drawing.Point.
    public int Y { get { return default(int); } set { } }

    // Summary:
    //     Adds the specified System.Drawing.Size to the specified System.Drawing.Point.
    //
    // Parameters:
    //   pt:
    //     The System.Drawing.Point to add.
    //
    //   sz:
    //     The System.Drawing.Size to add
    //
    // Returns:
    //     The System.Drawing.Point that is the result of the addition operation.
    public static Point Add(Point pt, Size sz) {
      return default(Point);
    }
    //
    // Summary:
    //     Converts the specified System.Drawing.PointF to a System.Drawing.Point by
    //     rounding the values of the System.Drawing.PointF to the next higher integer
    //     values.
    //
    // Parameters:
    //   value:
    //     The System.Drawing.PointF to convert.
    //
    // Returns:
    //     The System.Drawing.Point this method converts to.
    public static Point Ceiling(PointF value) {
      return default(Point);
    }
    //
    // Summary:
    //     Specifies whether this System.Drawing.Point contains the same coordinates
    //     as the specified System.Object.
    //
    // Parameters:
    //   obj:
    //     The System.Object to test.
    //
    // Returns:
    //     true if obj is a System.Drawing.Point and has the same coordinates as this
    //     System.Drawing.Point.
    public override bool Equals(object obj) {
      return default(bool);
    }
    //
    // Summary:
    //     Returns a hash code for this System.Drawing.Point.
    //
    // Returns:
    //     An integer value that specifies a hash value for this System.Drawing.Point.
    public override int GetHashCode() {
      return default(int);
    }
    //
    // Summary:
    //     Translates this System.Drawing.Point by the specified System.Drawing.Point.
    //
    // Parameters:
    //   p:
    //     The System.Drawing.Point used offset this System.Drawing.Point.
    public void Offset(Point p) {
    }
    //
    // Summary:
    //     Translates this System.Drawing.Point by the specified amount.
    //
    // Parameters:
    //   dx:
    //     The amount to offset the x-coordinate.
    //
    //   dy:
    //     The amount to offset the y-coordinate.
    //[TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
    public void Offset(int dx, int dy) { }
    //
    // Summary:
    //     Converts the specified System.Drawing.PointF to a System.Drawing.Point object
    //     by rounding the System.Drawing.Point values to the nearest integer.
    //
    // Parameters:
    //   value:
    //     The System.Drawing.PointF to convert.
    //
    // Returns:
    //     The System.Drawing.Point this method converts to.
    public static Point Round(PointF value) {
      return default(Point);
    }
    //
    // Summary:
    //     Returns the result of subtracting specified System.Drawing.Size from the
    //     specified System.Drawing.Point.
    //
    // Parameters:
    //   pt:
    //     The System.Drawing.Point to be subtracted from.
    //
    //   sz:
    //     The System.Drawing.Size to subtract from the System.Drawing.Point.
    //
    // Returns:
    //     The System.Drawing.Point that is the result of the subtraction operation.
    public static Point Subtract(Point pt, Size sz) {
      return default(Point);
    }
    //
    // Summary:
    //     Converts this System.Drawing.Point to a human-readable string.
    //
    // Returns:
    //     A string that represents this System.Drawing.Point.
    //
    // Summary:
    //     Converts the specified System.Drawing.PointF to a System.Drawing.Point by
    //     truncating the values of the System.Drawing.Point.
    //
    // Parameters:
    //   value:
    //     The System.Drawing.PointF to convert.
    //
    // Returns:
    //     The System.Drawing.Point this method converts to.
    public static Point Truncate(PointF value) {
      return default(Point);
    }
  }
}
