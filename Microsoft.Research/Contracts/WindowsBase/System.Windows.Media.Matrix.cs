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

using MS.Internal;
using System;
using System.ComponentModel;
using System.Windows;
using System.Diagnostics.Contracts;


namespace System.Windows.Media
{
  // Summary:
  //     Represents a 3x3 affine transformation matrix used for transformations in
  //     2-D space.
 // [Serializable]
 // [TypeConverter(typeof(MatrixConverter))]
 // [ValueSerializer(typeof(MatrixValueSerializer))]
  public struct Matrix //: IFormattable
  {
    //
    // Summary:
    //     Initializes a new instance of the System.Windows.Media.Matrix structure.
    //
    // Parameters:
    //   m11:
    //     The new System.Windows.Media.Matrix structure's System.Windows.Media.Matrix.M11
    //     coefficient.
    //
    //   m12:
    //     The new System.Windows.Media.Matrix structure's System.Windows.Media.Matrix.M12
    //     coefficient.
    //
    //   m21:
    //     The new System.Windows.Media.Matrix structure's System.Windows.Media.Matrix.M21
    //     coefficient.
    //
    //   m22:
    //     The new System.Windows.Media.Matrix structure's System.Windows.Media.Matrix.M22
    //     coefficient.
    //
    //   offsetX:
    //     The new System.Windows.Media.Matrix structure's System.Windows.Media.Matrix.OffsetX
    //     coefficient.
    //
    //   offsetY:
    //     The new System.Windows.Media.Matrix structure's System.Windows.Media.Matrix.OffsetY
    //     coefficient.
    //public Matrix(double m11, double m12, double m21, double m22, double offsetX, double offsetY);

    // Summary:
    //     Determines whether the two specified System.Windows.Media.Matrix structures
    //     are not identical.
    //
    // Parameters:
    //   matrix1:
    //     The first System.Windows.Media.Matrix structure to compare.
    //
    //   matrix2:
    //     The second System.Windows.Media.Matrix structure to compare.
    //
    // Returns:
    //     true if matrix1 and matrix2 are not identical; otherwise, false.
    [Pure]
    public static bool operator !=(Matrix matrix1, Matrix matrix2)
    {
      return default(bool);
    }
    //
    // Summary:
    //     Multiplies a System.Windows.Media.Matrix structure by another System.Windows.Media.Matrix
    //     structure.
    //
    // Parameters:
    //   trans1:
    //     The first System.Windows.Media.Matrix structure to multiply.
    //
    //   trans2:
    //     The second System.Windows.Media.Matrix structure to multiply.
    //
    // Returns:
    //     The result of multiplying trans1 by trans2.
    // F: Works with side effects!!!
    //public static Matrix operator *(Matrix trans1, Matrix trans2);
    //
    // Summary:
    //     Determines whether the two specified System.Windows.Media.Matrix structures
    //     are identical.
    //
    // Parameters:
    //   matrix1:
    //     The first System.Windows.Media.Matrix structure to compare.
    //
    //   matrix2:
    //     The second System.Windows.Media.Matrix structure to compare.
    //
    // Returns:
    //     true if matrix1 and matrix2 are identical; otherwise, false.
    [Pure]
    public static bool operator ==(Matrix matrix1, Matrix matrix2)
    {
      return default(bool);
    }

    // Summary:
    //     Gets the determinant of this System.Windows.Media.Matrix structure.
    //
    // Returns:
    //     The determinant of this System.Windows.Media.Matrix.
    //public double Determinant { get; }
    //
    // Summary:
    //     Gets a value that indicates whether this System.Windows.Media.Matrix structure
    //     is invertible.
    //
    // Returns:
    //     true if the System.Windows.Media.Matrix has an inverse; otherwise, false.
    //     The default is true.
    //public bool HasInverse { get; }
    //
    // Summary:
    //     Gets an identity System.Windows.Media.Matrix.
    //
    // Returns:
    //     An identity matrix.
    //public static Matrix Identity { get; }
    //
    // Summary:
    //     Gets a value that indicates whether this System.Windows.Media.Matrix structure
    //     is an identity matrix.
    //
    // Returns:
    //     true if the System.Windows.Media.Matrix structure is an identity matrix;
    //     otherwise, false. The default is true.
    //public bool IsIdentity { get; }
    //
    // Summary:
    //     Gets or sets the value of the first row and first column of this System.Windows.Media.Matrix
    //     structure.
    //
    // Returns:
    //     The value of the first row and first column of this System.Windows.Media.Matrix.
    //     The default value is 1.
    //public double M11 { get; set; }
    //
    // Summary:
    //     Gets or sets the value of the first row and second column of this System.Windows.Media.Matrix
    //     structure.
    //
    // Returns:
    //     The value of the first row and second column of this System.Windows.Media.Matrix.
    //     The default value is 0.
    //public double M12 { get; set; }
    //
    // Summary:
    //     Gets or sets the value of the second row and first column of this System.Windows.Media.Matrix
    //     structure.
    //
    // Returns:
    //     The value of the second row and first column of this System.Windows.Media.Matrix.
    //     The default value is 0.
    //public double M21 { get; set; }
    //
    // Summary:
    //     Gets or sets the value of the second row and second column of this System.Windows.Media.Matrix
    //     structure.
    //
    // Returns:
    //     The value of the second row and second column of this System.Windows.Media.Matrix
    //     structure. The default value is 1.
    //public double M22 { get; set; }
    //
    // Summary:
    //     Gets or sets the value of the third row and first column of this System.Windows.Media.Matrix
    //     structure.
    //
    // Returns:
    //     The value of the third row and first column of this System.Windows.Media.Matrix
    //     structure. The default value is 0.
    //public double OffsetX { get; set; }
    //
    // Summary:
    //     Gets or sets the value of the third row and second column of this System.Windows.Media.Matrix
    //     structure.
    //
    // Returns:
    //     The value of the third row and second column of this System.Windows.Media.Matrix
    //     structure. The default value is 0.
    //public double OffsetY { get; set; }

    // Summary:
    //     Appends the specified System.Windows.Media.Matrix structure to this System.Windows.Media.Matrix
    //     structure.
    //
    // Parameters:
    //   matrix:
    //     The System.Windows.Media.Matrix structure to append to this System.Windows.Media.Matrix
    //     structure.
    //public void Append(Matrix matrix);
    //
    // Summary:
    //     Determines whether the specified System.Windows.Media.Matrix structure is
    //     identical to this instance.
    //
    // Parameters:
    //   value:
    //     The instance of System.Windows.Media.Matrix to compare to this instance.
    //
    // Returns:
    //     true if instances are equal; otherwise, false.
    //public bool Equals(Matrix value);
    //
    // Summary:
    //     Determines whether the specified System.Object is a System.Windows.Media.Matrix
    //     structure that is identical to this System.Windows.Media.Matrix.
    //
    // Parameters:
    //   o:
    //     The System.Object to compare.
    //
    // Returns:
    //     true if o is a System.Windows.Media.Matrix structure that is identical to
    //     this System.Windows.Media.Matrix structure; otherwise, false.
    //public override bool Equals(object o);
    //
    // Summary:
    //     Determines whether the two specified System.Windows.Media.Matrix structures
    //     are identical.
    //
    // Parameters:
    //   matrix1:
    //     The first System.Windows.Media.Matrix structure to compare.
    //
    //   matrix2:
    //     The second System.Windows.Media.Matrix structure to compare.
    //
    // Returns:
    //     true if matrix1 and matrix2 are identical; otherwise, false.
    //public static bool Equals(Matrix matrix1, Matrix matrix2);
    //
    // Summary:
    //     Returns the hash code for this System.Windows.Media.Matrix structure.
    //
    // Returns:
    //     The hash code for this instance.
    //public override int GetHashCode();
    //
    // Summary:
    //     Inverts this System.Windows.Media.Matrix structure.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     The System.Windows.Media.Matrix structure is not invertible.
    //public void Invert();
    //
    // Summary:
    //     Multiplies a System.Windows.Media.Matrix structure by another System.Windows.Media.Matrix
    //     structure.
    //
    // Parameters:
    //   trans1:
    //     The first System.Windows.Media.Matrix structure to multiply.
    //
    //   trans2:
    //     The second System.Windows.Media.Matrix structure to multiply.
    //
    // Returns:
    //     The result of multiplying trans1 by trans2.
    //public static Matrix Multiply(Matrix trans1, Matrix trans2);
    //
    // Summary:
    //     Converts a System.String representation of a matrix into the equivalent System.Windows.Media.Matrix
    //     structure.
    //
    // Parameters:
    //   source:
    //     The System.String representation of the matrix.
    //
    // Returns:
    //     The equivalent System.Windows.Media.Matrix structure.
    //public static Matrix Parse(string source);
    //
    // Summary:
    //     Prepends the specified System.Windows.Media.Matrix structure onto this System.Windows.Media.Matrix
    //     structure.
    //
    // Parameters:
    //   matrix:
    //     The System.Windows.Media.Matrix structure to prepend to this System.Windows.Media.Matrix
    //     structure.
    //public void Prepend(Matrix matrix);
    //
    // Summary:
    //     Applies a rotation of the specified angle about the origin of this System.Windows.Media.Matrix
    //     structure.
    //
    // Parameters:
    //   angle:
    //     The angle of rotation.
    //public void Rotate(double angle);
    //
    // Summary:
    //     Rotates this matrix about the specified point.
    //
    // Parameters:
    //   angle:
    //     The angle, in degrees, by which to rotate this matrix.
    //
    //   centerX:
    //     The x-coordinate of the point about which to rotate this matrix.
    //
    //   centerY:
    //     The y-coordinate of the point about which to rotate this matrix.
    //public void RotateAt(double angle, double centerX, double centerY);
    //
    // Summary:
    //     Prepends a rotation of the specified angle at the specified point to this
    //     System.Windows.Media.Matrix structure.
    //
    // Parameters:
    //   angle:
    //     The rotation angle, in degrees.
    //
    //   centerX:
    //     The x-coordinate of the rotation center.
    //
    //   centerY:
    //     The y-coordinate of the rotation center.
    //public void RotateAtPrepend(double angle, double centerX, double centerY);
    //
    // Summary:
    //     Prepends a rotation of the specified angle to this System.Windows.Media.Matrix
    //     structure.
    //
    // Parameters:
    //   angle:
    //     The angle of rotation to prepend.
    //public void RotatePrepend(double angle);
    //
    // Summary:
    //     Appends the specified scale vector to this System.Windows.Media.Matrix structure.
    //
    // Parameters:
    //   scaleX:
    //     The value by which to scale this System.Windows.Media.Matrix along the x-axis.
    //
    //   scaleY:
    //     The value by which to scale this System.Windows.Media.Matrix along the y-axis.
    //public void Scale(double scaleX, double scaleY);
    //
    // Summary:
    //     Scales this System.Windows.Media.Matrix by the specified amount about the
    //     specified point.
    //
    // Parameters:
    //   scaleX:
    //     The amount by which to scale this System.Windows.Media.Matrix along the x-axis.
    //
    //   scaleY:
    //     The amount by which to scale this System.Windows.Media.Matrix along the y-axis.
    //
    //   centerX:
    //     The x-coordinate of the scale operation's center point.
    //
    //   centerY:
    //     The y-coordinate of the scale operation's center point.
    //public void ScaleAt(double scaleX, double scaleY, double centerX, double centerY);
    //
    // Summary:
    //     Prepends the specified scale about the specified point of this System.Windows.Media.Matrix.
    //
    // Parameters:
    //   scaleX:
    //     The x-axis scale factor.
    //
    //   scaleY:
    //     The y-axis scale factor.
    //
    //   centerX:
    //     The x-coordinate of the point about which the scale operation is performed.
    //
    //   centerY:
    //     The y-coordinate of the point about which the scale operation is performed.
    //public void ScaleAtPrepend(double scaleX, double scaleY, double centerX, double centerY);
    //
    // Summary:
    //     Prepends the specified scale vector to this System.Windows.Media.Matrix structure.
    //
    // Parameters:
    //   scaleX:
    //     The value by which to scale this System.Windows.Media.Matrix structure along
    //     the x-axis.
    //
    //   scaleY:
    //     The value by which to scale this System.Windows.Media.Matrix structure along
    //     the y-axis.
    //public void ScalePrepend(double scaleX, double scaleY);
    //
    // Summary:
    //     Changes this System.Windows.Media.Matrix structure into an identity matrix.
    //public void SetIdentity();
    //
    // Summary:
    //     Appends a skew of the specified degrees in the x and y dimensions to this
    //     System.Windows.Media.Matrix structure.
    //
    // Parameters:
    //   skewX:
    //     The angle in the x dimension by which to skew this System.Windows.Media.Matrix.
    //
    //   skewY:
    //     The angle in the y dimension by which to skew this System.Windows.Media.Matrix.
    //public void Skew(double skewcX, double skewY);
    //
    // Summary:
    //     Prepends a skew of the specified degrees in the x and y dimensions to this
    //     System.Windows.Media.Matrix structure.
    //
    // Parameters:
    //   skewX:
    //     The angle in the x dimension by which to skew this System.Windows.Media.Matrix.
    //
    //   skewY:
    //     The angle in the y dimension by which to skew this System.Windows.Media.Matrix.
    //public void SkewPrepend(double skewX, double skewY);
    //
    // Summary:
    //     Creates a System.String representation of this System.Windows.Media.Matrix
    //     structure.
    //
    // Returns:
    //     A System.String containing the System.Windows.Media.Matrix.M11, System.Windows.Media.Matrix.M12,
    //     System.Windows.Media.Matrix.M21, System.Windows.Media.Matrix.M22, System.Windows.Media.Matrix.OffsetX,
    //     and System.Windows.Media.Matrix.OffsetY values of this System.Windows.Media.Matrix.
    //public override string ToString();
    //
    // Summary:
    //     Creates a System.String representation of this System.Windows.Media.Matrix
    //     structure with culture-specific formatting information.
    //
    // Parameters:
    //   provider:
    //     The culture-specific formatting information.
    //
    // Returns:
    //     A System.String containing the System.Windows.Media.Matrix.M11, System.Windows.Media.Matrix.M12,
    //     System.Windows.Media.Matrix.M21, System.Windows.Media.Matrix.M22, System.Windows.Media.Matrix.OffsetX,
    //     and System.Windows.Media.Matrix.OffsetY values of this System.Windows.Media.Matrix.
    //public string ToString(IFormatProvider provider);
    //
    // Summary:
    //     Transforms the specified point by the System.Windows.Media.Matrix and returns
    //     the result.
    //
    // Parameters:
    //   point:
    //     The point to transform.
    //
    // Returns:
    //     The result of transforming point by this System.Windows.Media.Matrix.
    //public Point Transform(Point point);
    //
    // Summary:
    //     Transforms the specified points by this System.Windows.Media.Matrix.
    //
    // Parameters:
    //   points:
    //     The points to transform. The original points in the array are replaced by
    //     their transformed values.
    //public void Transform(Point[] points);
    //
    // Summary:
    //     Transforms the specified vector by this System.Windows.Media.Matrix.
    //
    // Parameters:
    //   vector:
    //     The vector to transform.
    //
    // Returns:
    //     The result of transforming vector by this System.Windows.Media.Matrix.
    //public Vector Transform(Vector vector);
    //
    // Summary:
    //     Transforms the specified vectors by this System.Windows.Media.Matrix.
    //
    // Parameters:
    //   vectors:
    //     The vectors to transform. The original vectors in the array are replaced
    //     by their transformed values.
    //public void Transform(Vector[] vectors);
    //
    // Summary:
    //     Appends a translation of the specified offsets to this System.Windows.Media.Matrix
    //     structure.
    //
    // Parameters:
    //   offsetX:
    //     The amount to offset this System.Windows.Media.Matrix along the x-axis.
    //
    //   offsetY:
    //     The amount to offset this System.Windows.Media.Matrix along the y-axis.
    //public void Translate(double offsetX, double offsetY);
    //
    // Summary:
    //     Prepends a translation of the specified offsets to this System.Windows.Media.Matrix
    //     structure.
    //
    // Parameters:
    //   offsetX:
    //     The amount to offset this System.Windows.Media.Matrix along the x-axis.
    //
    //   offsetY:
    //     The amount to offset this System.Windows.Media.Matrix along the y-axis.
    //public void TranslatePrepend(double offsetX, double offsetY);
  }
}
