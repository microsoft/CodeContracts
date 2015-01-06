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
  //     Stores an ordered pair of floating-point numbers, typically the width and
  //     height of a rectangle.
  [Serializable]
  [ComVisible(true)]
  //[TypeConverter(typeof(SizeFConverter))]
  public struct SizeF {
    // Summary:
    //     Gets a System.Drawing.SizeF structure that has a System.Drawing.SizeF.Height
    //     and System.Drawing.SizeF.Width value of 0.
    //
    // Returns:
    //     A System.Drawing.SizeF structure that has a System.Drawing.SizeF.Height and
    //     System.Drawing.SizeF.Width value of 0.
    public static readonly SizeF Empty;

    //
    // Summary:
    //     Initializes a new instance of the System.Drawing.SizeF structure from the
    //     specified System.Drawing.PointF structure.
    //
    // Parameters:
    //   pt:
    //     The System.Drawing.PointF structure from which to initialize this System.Drawing.SizeF
    //     structure.
    //public SizeF(PointF pt);
    //
    // Summary:
    //     Initializes a new instance of the System.Drawing.SizeF structure from the
    //     specified existing System.Drawing.SizeF structure.
    //
    // Parameters:
    //   size:
    //     The System.Drawing.SizeF structure from which to create the new System.Drawing.SizeF
    //     structure.
    //public SizeF(SizeF size);
    //
    // Summary:
    //     Initializes a new instance of the System.Drawing.SizeF structure from the
    //     specified dimensions.
    //
    // Parameters:
    //   width:
    //     The width component of the new System.Drawing.SizeF structure.
    //
    //   height:
    //     The height component of the new System.Drawing.SizeF structure.
    //[TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    //public SizeF(float width, float height);

    // Summary:
    //     Subtracts the width and height of one System.Drawing.SizeF structure from
    //     the width and height of another System.Drawing.SizeF structure.
    //
    // Parameters:
    //   sz1:
    //     The System.Drawing.SizeF structure on the left side of the subtraction operator.
    //
    //   sz2:
    //     The System.Drawing.SizeF structure on the right side of the subtraction operator.
    //
    // Returns:
    //     A System.Drawing.SizeF that is the result of the subtraction operation.
    //[TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    public static SizeF operator -(SizeF sz1, SizeF sz2) {
      return default(SizeF);
    }
    //
    // Summary:
    //     Tests whether two System.Drawing.SizeF structures are different.
    //
    // Parameters:
    //   sz1:
    //     The System.Drawing.SizeF structure on the left of the inequality operator.
    //
    //   sz2:
    //     The System.Drawing.SizeF structure on the right of the inequality operator.
    //
    // Returns:
    //     This operator returns true if sz1 and sz2 differ either in width or height;
    //     false if sz1 and sz2 are equal.
    public static bool operator !=(SizeF sz1, SizeF sz2) {
      return default(bool);
    }
    //
    // Summary:
    //     Adds the width and height of one System.Drawing.SizeF structure to the width
    //     and height of another System.Drawing.SizeF structure.
    //
    // Parameters:
    //   sz1:
    //     The first System.Drawing.SizeF structure to add.
    //
    //   sz2:
    //     The second System.Drawing.SizeF structure to add.
    //
    // Returns:
    //     A System.Drawing.Size structure that is the result of the addition operation.
    //[TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    public static SizeF operator +(SizeF sz1, SizeF sz2) {
      return default(SizeF);
    }
    //
    // Summary:
    //     Tests whether two System.Drawing.SizeF structures are equal.
    //
    // Parameters:
    //   sz1:
    //     The System.Drawing.SizeF structure on the left side of the equality operator.
    //
    //   sz2:
    //     The System.Drawing.SizeF structure on the right of the equality operator.
    //
    // Returns:
    //     This operator returns true if sz1 and sz2 have equal width and height; otherwise,
    //     false.
    public static bool operator ==(SizeF sz1, SizeF sz2) {
      return default(bool);
    }
    //
    // Summary:
    //     Converts the specified System.Drawing.SizeF structure to a System.Drawing.PointF
    //     structure.
    //
    // Parameters:
    //   size:
    //     The System.Drawing.SizeF structure to be converted
    //
    // Returns:
    //     The System.Drawing.PointF structure to which this operator converts.
    public static explicit operator PointF(SizeF size) {
      return default(PointF);
    }

    // Summary:
    //     Gets or sets the vertical component of this System.Drawing.SizeF structure.
    //
    // Returns:
    //     The vertical component of this System.Drawing.SizeF structure, typically
    //     measured in pixels.
    public float Height { get { return default(float); } set { } }
    //
    // Summary:
    //     Gets a value that indicates whether this System.Drawing.SizeF structure has
    //     zero width and height.
    //
    // Returns:
    //     This property returns true when this System.Drawing.SizeF structure has both
    //     a width and height of zero; otherwise, false.
    [Browsable(false)]
    public bool IsEmpty {
      get {
        return default(bool);
      }
    }
    //
    // Summary:
    //     Gets or sets the horizontal component of this System.Drawing.SizeF structure.
    //
    // Returns:
    //     The horizontal component of this System.Drawing.SizeF structure, typically
    //     measured in pixels.
    public float Width { get { return default(float); } set { } }

    // Summary:
    //     Adds the width and height of one System.Drawing.SizeF structure to the width
    //     and height of another System.Drawing.SizeF structure.
    //
    // Parameters:
    //   sz1:
    //     The first System.Drawing.SizeF structure to add.
    //
    //   sz2:
    //     The second System.Drawing.SizeF structure to add.
    //
    // Returns:
    //     A System.Drawing.SizeF structure that is the result of the addition operation.
    public static SizeF Add(SizeF sz1, SizeF sz2) {
      return default(SizeF);
    }
    //
    // Summary:
    //     Tests to see whether the specified object is a System.Drawing.SizeF structure
    //     with the same dimensions as this System.Drawing.SizeF structure.
    //
    // Parameters:
    //   obj:
    //     The System.Object to test.
    //
    // Returns:
    //     This method returns true if obj is a System.Drawing.SizeF and has the same
    //     width and height as this System.Drawing.SizeF; otherwise, false.
    public override bool Equals(object obj) {
      return default(bool);
    }
    //
    // Summary:
    //     Returns a hash code for this System.Drawing.Size structure.
    //
    // Returns:
    //     An integer value that specifies a hash value for this System.Drawing.Size
    //     structure.
    public override int GetHashCode() {
      return default(int);
    }
    //
    // Summary:
    //     Subtracts the width and height of one System.Drawing.SizeF structure from
    //     the width and height of another System.Drawing.SizeF structure.
    //
    // Parameters:
    //   sz1:
    //     The System.Drawing.SizeF structure on the left side of the subtraction operator.
    //
    //   sz2:
    //     The System.Drawing.SizeF structure on the right side of the subtraction operator.
    //
    // Returns:
    //     A System.Drawing.SizeF structure that is a result of the subtraction operation.
    public static SizeF Subtract(SizeF sz1, SizeF sz2) {
      return default(SizeF);
    }
    //
    // Summary:
    //     Converts a System.Drawing.SizeF structure to a System.Drawing.PointF structure.
    //
    // Returns:
    //     Returns a System.Drawing.PointF structure.
    public PointF ToPointF() {
      return default(PointF);
    }
    //
    // Summary:
    //     Converts a System.Drawing.SizeF structure to a System.Drawing.Size structure.
    //
    // Returns:
    //     Returns a System.Drawing.Size structure.
    public Size ToSize() {
      return default(Size);
    }
    //
    // Summary:
    //     Creates a human-readable string that represents this System.Drawing.SizeF
    //     structure.
    //
    // Returns:
    //     A string that represents this System.Drawing.SizeF structure.
  }
}
