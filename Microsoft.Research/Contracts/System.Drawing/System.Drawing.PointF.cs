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
  //     Represents an ordered pair of floating-point x- and y-coordinates that defines
  //     a point in a two-dimensional plane.
  [Serializable]
  [ComVisible(true)]
  public struct PointF {
    // Summary:
    //     Represents a new instance of the System.Drawing.PointF class with member
    //     data left uninitialized.
    public static readonly PointF Empty;

    //
    // Summary:
    //     Initializes a new instance of the System.Drawing.PointF class with the specified
    //     coordinates.
    //
    // Parameters:
    //   x:
    //     The horizontal position of the point.
    //
    //   y:
    //     The vertical position of the point.
    //[TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    //public PointF(float x, float y);

    // Summary:
    //     Translates a System.Drawing.PointF by the negative of a given System.Drawing.Size.
    //
    // Parameters:
    //   pt:
    //     A System.Drawing.PointF to compare.
    //
    //   sz:
    //     A System.Drawing.PointF to compare.
    //
    // Returns:
    //     The translated System.Drawing.PointF.
    //[TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    public static PointF operator -(PointF pt, Size sz) {
      return default(PointF);
    }
    //
    // Summary:
    //     Translates a System.Drawing.PointF by the negative of a specified System.Drawing.SizeF.
    //
    // Parameters:
    //   pt:
    //     The System.Drawing.PointF to translate.
    //
    //   sz:
    //     The System.Drawing.SizeF that specifies the numbers to subtract from the
    //     coordinates of pt.
    //
    // Returns:
    //     The translated System.Drawing.PointF.
    //[TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    public static PointF operator -(PointF pt, SizeF sz) {
      return default(PointF);
    }
    //
    // Summary:
    //     Determines whether the coordinates of the specified points are not equal.
    //
    // Parameters:
    //   left:
    //     A System.Drawing.PointF to compare.
    //
    //   right:
    //     A System.Drawing.PointF to compare.
    //
    // Returns:
    //     true to indicate the System.Drawing.PointF.X and System.Drawing.PointF.Y
    //     values of left and right are not equal; otherwise, false.
    public static bool operator !=(PointF left, PointF right) {
      return default(bool);
    }
    //
    // Summary:
    //     Translates a System.Drawing.PointF by a given System.Drawing.Size.
    //
    // Parameters:
    //   pt:
    //     The System.Drawing.PointF to translate.
    //
    //   sz:
    //     A System.Drawing.Size that specifies the pair of numbers to add to the coordinates
    //     of pt.
    //
    // Returns:
    //     Returns the translated System.Drawing.PointF.
    //[TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    public static PointF operator +(PointF pt, Size sz) {
      return default(PointF);
    }
    //
    // Summary:
    //     Translates the System.Drawing.PointF by the specified System.Drawing.SizeF.
    //
    // Parameters:
    //   pt:
    //     The System.Drawing.PointF to translate.
    //
    //   sz:
    //     The System.Drawing.SizeF that specifies the numbers to add to the x- and
    //     y-coordinates of the System.Drawing.PointF.
    //
    // Returns:
    //     The translated System.Drawing.PointF.
    //[TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    public static PointF operator +(PointF pt, SizeF sz) {
      return default(PointF);
    }
    //
    // Summary:
    //     Compares two System.Drawing.PointF structures. The result specifies whether
    //     the values of the System.Drawing.PointF.X and System.Drawing.PointF.Y properties
    //     of the two System.Drawing.PointF structures are equal.
    //
    // Parameters:
    //   left:
    //     A System.Drawing.PointF to compare.
    //
    //   right:
    //     A System.Drawing.PointF to compare.
    //
    // Returns:
    //     true if the System.Drawing.PointF.X and System.Drawing.PointF.Y values of
    //     the left and right System.Drawing.PointF structures are equal; otherwise,
    //     false.
    public static bool operator ==(PointF left, PointF right) {
      return default(bool);
    }

    // Summary:
    //     Gets a value indicating whether this System.Drawing.PointF is empty.
    //
    // Returns:
    //     true if both System.Drawing.PointF.X and System.Drawing.PointF.Y are 0; otherwise,
    //     false.
    [Browsable(false)]
    public bool IsEmpty {
      get {
        return default(bool);
      }
    }
    //
    // Summary:
    //     Gets or sets the x-coordinate of this System.Drawing.PointF.
    //
    // Returns:
    //     The x-coordinate of this System.Drawing.PointF.
    public float X { get { return default(float); } set { } }
    //
    // Summary:
    //     Gets or sets the y-coordinate of this System.Drawing.PointF.
    //
    // Returns:
    //     The y-coordinate of this System.Drawing.PointF.
    public float Y { get { return default(float); } set { } }

    // Summary:
    //     Translates a given System.Drawing.PointF by the specified System.Drawing.Size.
    //
    // Parameters:
    //   pt:
    //     The System.Drawing.PointF to translate.
    //
    //   sz:
    //     The System.Drawing.Size that specifies the numbers to add to the coordinates
    //     of pt.
    //
    // Returns:
    //     The translated System.Drawing.PointF.
    public static PointF Add(PointF pt, Size sz) {
      return default(PointF);
    }
    //
    // Summary:
    //     Translates a given System.Drawing.PointF by a specified System.Drawing.SizeF.
    //
    // Parameters:
    //   pt:
    //     The System.Drawing.PointF to translate.
    //
    //   sz:
    //     The System.Drawing.SizeF that specifies the numbers to add to the coordinates
    //     of pt.
    //
    // Returns:
    //     The translated System.Drawing.PointF.
    public static PointF Add(PointF pt, SizeF sz) {
      return default(PointF);
    }
    //
    // Summary:
    //     Specifies whether this System.Drawing.PointF contains the same coordinates
    //     as the specified System.Object.
    //
    // Parameters:
    //   obj:
    //     The System.Object to test.
    //
    // Returns:
    //     This method returns true if obj is a System.Drawing.PointF and has the same
    //     coordinates as this System.Drawing.Point.
    public override bool Equals(object obj) {
      return default(bool);
    }
    //
    // Summary:
    //     Returns a hash code for this System.Drawing.PointF structure.
    //
    // Returns:
    //     An integer value that specifies a hash value for this System.Drawing.PointF
    //     structure.
    public override int GetHashCode() {
      return default(int);
    }
    //
    // Summary:
    //     Translates a System.Drawing.PointF by the negative of a specified size.
    //
    // Parameters:
    //   pt:
    //     The System.Drawing.PointF to translate.
    //
    //   sz:
    //     The System.Drawing.Size that specifies the numbers to subtract from the coordinates
    //     of pt.
    //
    // Returns:
    //     The translated System.Drawing.PointF.
    public static PointF Subtract(PointF pt, Size sz) {
      return default(PointF);
    }
    //
    // Summary:
    //     Translates a System.Drawing.PointF by the negative of a specified size.
    //
    // Parameters:
    //   pt:
    //     The System.Drawing.PointF to translate.
    //
    //   sz:
    //     The System.Drawing.SizeF that specifies the numbers to subtract from the
    //     coordinates of pt.
    //
    // Returns:
    //     The translated System.Drawing.PointF.
    public static PointF Subtract(PointF pt, SizeF sz) {
      return default(PointF);
    }
    //
    // Summary:
    //     Converts this System.Drawing.PointF to a human readable string.
    //
    // Returns:
    //     A string that represents this System.Drawing.PointF.
  }
}
