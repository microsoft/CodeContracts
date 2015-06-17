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
using System.Diagnostics.Contracts;
using System.Windows.Media;

namespace System.Windows
{
  // Summary:
  //     Represents an x- and y-coordinate pair in two-dimensional space.
  // [Serializable]
  // [TypeConverter(typeof(PointConverter))]
  // [ValueSerializer(typeof(PointValueSerializer))]
  public struct Point //: IFormattable
  {
    //
    // Summary:
    //     Creates a new System.Windows.Point structure that contains the specified
    //     coordinates.
    //
    // Parameters:
    //   x:
    //     The x-coordinate of the new System.Windows.Point structure.
    //
    //   y:
    //     The y-coordinate of the new System.Windows.Point structure.
    public Point(double x, double y)
    {
      Contract.Ensures(Contract.ValueAtReturn(out this).X == x);
      Contract.Ensures(Contract.ValueAtReturn(out this).Y == y);
    }

    // Summary:
    //     Subtracts the specified System.Windows.Point from another specified System.Windows.Point
    //     and returns the difference as a System.Windows.Vector.
    //
    // Parameters:
    //   point1:
    //     The point from which point2 is subtracted.
    //
    //   point2:
    //     The point to subtract from point1.
    //
    // Returns:
    //     The difference between point1 and point2.
    [Pure]
    public static Vector operator -(Point point1, Point point2)
    {
      Contract.Ensures(Contract.Result<Vector>().X == point1.X - point2.X);
      Contract.Ensures(Contract.Result<Vector>().Y == point1.Y - point2.Y);

      return default(Vector);
    }
    //
    // Summary:
    //     Subtracts the specified System.Windows.Vector from the specified System.Windows.Point
    //     and returns the resulting System.Windows.Point.
    //
    // Parameters:
    //   point:
    //     The point from which vector is subtracted.
    //
    //   vector:
    //     The vector to subtract from point1
    //
    // Returns:
    //     The difference between point and vector.
    [Pure]
    public static Point operator -(Point point, Vector vector)
    {
      Contract.Ensures(Contract.Result<Point>().X == point.X - vector.X);
      Contract.Ensures(Contract.Result<Point>().Y == point.Y - vector.Y);

      return default(Point);
    }
    //
    // Summary:
    //     Compares two System.Windows.Point structures for inequality. Note: A point's
    //     System.Windows.Point.X and System.Windows.Point.Y coordinates are described
    //     using System.Double values. Because System.Double values can lose precision
    //     when operated on, a comparison between two values that are logically equal
    //     might fail.
    //
    // Parameters:
    //   point1:
    //     The first point to compare.
    //
    //   point2:
    //     The second point to compare.
    //
    // Returns:
    //     true if point1 and point2 have different System.Windows.Point.X or System.Windows.Point.Y
    //     coordinates; false if point1 and point2 have the same System.Windows.Point.X
    //     and System.Windows.Point.Y coordinates.
    [Pure]
    public static bool operator !=(Point point1, Point point2)
    {
      Contract.Ensures(Contract.Result<bool>() == (((point1.X != point2.X) || (point1.Y != point2.Y))));

      return default(bool);
    }
    //
    // Summary:
    //     Transforms the specified System.Windows.Point by the specified System.Windows.Media.Matrix.
    //
    // Parameters:
    //   point:
    //     The point to transform.
    //
    //   matrix:
    //     The transformation matrix.
    //
    // Returns:
    //     The result of transforming the specified point using the specified matrix.
    [Pure]
    public static Point operator *(Point point, Matrix matrix)
    {
      return default(Point);
    }
    //
    // Summary:
    //     Translates the specified System.Windows.Point by the specified System.Windows.Vector
    //     and returns the result.
    //
    // Parameters:
    //   point:
    //     The point to translate.
    //
    //   vector:
    //     The amount by which to translate point.
    //
    // Returns:
    //     The result of translating the specified point by the specified vector.
    [Pure]
    public static Point operator +(Point point, Vector vector)
    {
      Contract.Ensures(Contract.Result<Point>().X == point.X + vector.X);
      Contract.Ensures(Contract.Result<Point>().Y == point.Y + vector.Y);

      return default(Point);
    }
    //
    // Summary:
    //     Compares two System.Windows.Point structures for equality. Note: A point's
    //     coordinates are described using Doubles values. Because the value of Doubles
    //     can lose precision when arithmetic operations are performed on them, a comparison
    //     between two Doubles that are logically equal might fail.
    //
    // Parameters:
    //   point1:
    //     The first System.Windows.Point structure to compare.
    //
    //   point2:
    //     The second System.Windows.Point structure to compare.
    //
    // Returns:
    //     true if both the System.Windows.Point.X and System.Windows.Point.Y coordinates
    //     of point1 and point2 are equal; otherwise, false.
    [Pure]
    public static bool operator ==(Point point1, Point point2)
    {
      Contract.Ensures(Contract.Result<bool>() == (((point1.X == point2.X) && (point1.Y == point2.Y))));

      return default(bool);
    }
    //
    // Summary:
    //     Creates a System.Windows.Vector structure with an System.Windows.Vector.X
    //     value equal to the point's System.Windows.Point.X value and a System.Windows.Vector.Y
    //     value equal to the point's System.Windows.Point.Y value.
    //
    // Parameters:
    //   point:
    //     The point to convert.
    //
    // Returns:
    //     A vector with an System.Windows.Vector.X value equal to the point's System.Windows.Point.X
    //     value and a System.Windows.Vector.Y value equal to the point's System.Windows.Point.Y
    //     value.
    [Pure]
    public static explicit operator Vector(Point point)
    {
      Contract.Ensures(Contract.Result<Vector>().X == point.X);
      Contract.Ensures(Contract.Result<Vector>().Y == point.Y);

      return default(Vector);
    }
    //
    // Summary:
    //     Creates a System.Windows.Size structure with a System.Windows.Size.Width
    //     equal to this point's System.Windows.Point.X value and a System.Windows.Size.Height
    //     equal to this point's System.Windows.Point.Y value.
    //
    // Parameters:
    //   point:
    //     The point to convert.
    //
    // Returns:
    //     A System.Windows.Size structure with a System.Windows.Size.Width equal to
    //     this point's System.Windows.Point.X value and a System.Windows.Size.Height
    //     equal to this point's System.Windows.Point.Y value.
    [Pure]
    public static explicit operator Size(Point point)
    {
      Contract.Ensures(Contract.Result<Size>().Width == Math.Abs(point.X));
      Contract.Ensures(Contract.Result<Size>().Height == Math.Abs(point.Y));

      return default(Size);

    }

    // Summary:
    //     Gets or sets the System.Windows.Point.X-coordinate value of this System.Windows.Point
    //     structure.
    //
    // Returns:
    //     The System.Windows.Point.X-coordinate value of this System.Windows.Point
    //     structure. The default value is 0.
    public double X
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    //
    // Summary:
    //     Gets or sets the System.Windows.Point.Y-coordinate value of this System.Windows.Point.
    //
    // Returns:
    //     The System.Windows.Point.Y-coordinate value of this System.Windows.Point
    //     structure. The default value is 0.
    public double Y 
        {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    // Summary:
    //     Adds a System.Windows.Vector to a System.Windows.Point and returns the result
    //     as a System.Windows.Point structure.
    //
    // Parameters:
    //   point:
    //     The System.Windows.Point structure to add.
    //
    //   vector:
    //     The System.Windows.Vector structure to add.
    //
    // Returns:
    //     Returns the sum of point and vector.
    [Pure]
    public static Point Add(Point point, Vector vector)
    {
      Contract.Ensures(Contract.Result<Point>().X == point.X + vector.X);
      Contract.Ensures(Contract.Result<Point>().Y == point.Y + vector.Y);

      return default(Point);
    }
    //
    // Summary:
    //     Determines whether the specified System.Object is a System.Windows.Point
    //     and whether it contains the same coordinates as this System.Windows.Point.
    //     Note: System.Windows.Point coordinates are described using System.Double
    //     values. Because the value of a System.Double can lose precision when operated
    //     upon, a comparison between two Doubles that are logically equal might fail.
    //
    // Parameters:
    //   o:
    //     The System.Object to compare.
    //
    // Returns:
    //     true if o is a System.Windows.Point and contains the same System.Windows.Point.X
    //     and System.Windows.Point.Y values as this System.Windows.Point; otherwise,
    //     false.
    //public override bool Equals(object o);
    //
    // Summary:
    //     Compares two System.Windows.Point structures for equality. Note: System.Windows.Point
    //     coordinates are expressed using Double values. Because the value of a System.Double
    //     can lose precision when operated on, a comparison between two Doubles that
    //     are logically equal might fail.
    //
    // Parameters:
    //   value:
    //     The point to compare to this instance.
    //
    // Returns:
    //     true if both System.Windows.Point structures contain the same System.Windows.Point.X
    //     and System.Windows.Point.Y values; otherwise, false.
    //public bool Equals(Point value);
    //
    // Summary:
    //     Compares two System.Windows.Point structures for equality. Note: Point coordinates
    //     are described using System.Double values. Because the value of a System.Double
    //     can lose precision when operated upon, a comparison between two System.Double
    //     values that are logically equal might fail.
    //
    // Parameters:
    //   point1:
    //     The first point to compare.
    //
    //   point2:
    //     The second point to compare.
    //
    // Returns:
    //     true if point1 and point2 contain the same System.Windows.Point.X and System.Windows.Point.Y
    //     values; otherwise, false.
    //public static bool Equals(Point point1, Point point2);
    //
    // Summary:
    //     Returns the hash code for this System.Windows.Point.
    //
    // Returns:
    //     The hash code for this System.Windows.Point structure.
    //public override int GetHashCode();
    //
    // Summary:
    //     Transforms the specified System.Windows.Point structure by the specified
    //     System.Windows.Media.Matrix structure.
    //
    // Parameters:
    //   point:
    //     The point to transform.
    //
    //   matrix:
    //     The transformation matrix.
    //
    // Returns:
    //     The transformed point.
    [Pure]
    public static Point Multiply(Point point, Matrix matrix)
    {
      return default(Point);
    }
    //
    // Summary:
    //     Offsets a point's System.Windows.Point.X and System.Windows.Point.Y coordinates
    //     by the specified amounts.
    //
    // Parameters:
    //   offsetX:
    //     The amount to offset the point'sSystem.Windows.Point.X coordinate.
    //
    //   offsetY:
    //     The amount to offset thepoint's System.Windows.Point.Y coordinate.
    //
    // Returns:
    //     The result of offsetting this point by the specified x and y offsets.
    public void Offset(double offsetX, double offsetY)
    {
      Contract.Ensures(this.X == Contract.OldValue(this.X) + offsetX);
      Contract.Ensures(this.Y == Contract.OldValue(this.Y) + offsetY);
    }
    //
    // Summary:
    //     Constructs a System.Windows.Point from the specified System.String.
    //
    // Parameters:
    //   source:
    //     A string representation of a point.
    //
    // Returns:
    //     The equivalent System.Windows.Point structure.
    //
    // Exceptions:
    //   System.FormatException:
    //     source is not composed of two comma- or space-delimited double values.
    //
    //   System.InvalidOperationException:
    //     source does not contain two numbers.-or-source contains too many delimiters.
    //public static Point Parse(string source);
    //
    // Summary:
    //     Subtracts the specified System.Windows.Point from another specified System.Windows.Point
    //     and returns the difference as a System.Windows.Vector.
    //
    // Parameters:
    //   point1:
    //     The point from which point2 is subtracted.
    //
    //   point2:
    //     The point to subtract from point1.
    //
    // Returns:
    //     The difference between point1 and point2.
    [Pure]
    public static Vector Subtract(Point point1, Point point2)
    {
      Contract.Ensures(Contract.Result<Vector>().X == point1.X - point2.X);
      Contract.Ensures(Contract.Result<Vector>().Y == point1.Y - point2.Y);

      return default(Vector);
    }
    //
    // Summary:
    //     Subtracts the specified System.Windows.Vector from the specified System.Windows.Point
    //     and returns the resulting System.Windows.Point.
    //
    // Parameters:
    //   point:
    //     The point from which vector is subtracted.
    //
    //   vector:
    //     The vector to subtract from point.
    //
    // Returns:
    //     The difference between point and vector.
    [Pure]
    public static Point Subtract(Point point, Vector vector)
    {
      Contract.Ensures(Contract.Result<Point>().X == point.X - vector.X);
      Contract.Ensures(Contract.Result<Point>().Y == point.Y - vector.Y);

      return default(Point);
    }
    //
    // Summary:
    //     Creates a System.String representation of this System.Windows.Point.
    //
    // Returns:
    //     A System.String containing the System.Windows.Point.X and System.Windows.Point.Y
    //     values of this System.Windows.Point structure.
    //public override string ToString();
    //
    // Summary:
    //     Creates a System.String representation of this System.Windows.Point.
    //
    // Parameters:
    //   provider:
    //     Culture-specific formatting information.
    //
    // Returns:
    //     A System.String containing the System.Windows.Point.X and System.Windows.Point.Y
    //     values of this System.Windows.Point structure.
    //public string ToString(IFormatProvider provider);
  }
}
