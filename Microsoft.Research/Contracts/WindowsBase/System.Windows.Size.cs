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

namespace System.Windows
{
  // Summary:
  //     Implements a structure that is used to describe the System.Windows.Size of
  //     an object.
  // [Serializable]
  // [ValueSerializer(typeof(SizeValueSerializer))]
  // [TypeConverter(typeof(SizeConverter))]
  public struct Size // : IFormattable
  {
    //
    // Summary:
    //     Initializes a new instance of the System.Windows.Size structure and assigns
    //     it an initial width and height.
    //
    // Parameters:
    //   width:
    //     The initial width of the instance of System.Windows.Size.
    //
    //   height:
    //     The initial height of the instance of System.Windows.Size.
    public Size(double width, double height)
    {
      Contract.Requires((width >= 0.0) || double.IsNaN(width) || double.IsPositiveInfinity(width));
      Contract.Requires((height >= 0.0) || double.IsNaN(height) || double.IsPositiveInfinity(height));

      Contract.Ensures(Contract.ValueAtReturn(out this).Width == width);
      Contract.Ensures(Contract.ValueAtReturn(out this).Height == height);
    }

    // Summary:
    //     Compares two instances of System.Windows.Size for inequality.
    //
    // Parameters:
    //   size1:
    //     The first instance of System.Windows.Size to compare.
    //
    //   size2:
    //     The second instance of System.Windows.Size to compare.
    //
    // Returns:
    //     true if the instances of System.Windows.Size are not equal; otherwise false.
    public static bool operator !=(Size size1, Size size2)
    {
      Contract.Ensures(Contract.Result<bool>() == ((size1.Width != size2.Width) || (size1.Height != size2.Height)));

      return default(bool);
    }
    //
    // Summary:
    //     Compares two instances of System.Windows.Size for equality.
    //
    // Parameters:
    //   size1:
    //     The first instance of System.Windows.Size to compare.
    //
    //   size2:
    //     The second instance of System.Windows.Size to compare.
    //
    // Returns:
    //     true if the two instances of System.Windows.Size are equal; otherwise false.
    public static bool operator ==(Size size1, Size size2)
    {
      Contract.Ensures(Contract.Result<bool>() == ((size1.Width == size2.Width) && (size1.Height == size2.Height)));

      return default(bool);
    }
    //
    // Summary:
    //     Explicitly converts an instance of System.Windows.Size to an instance of
    //     System.Windows.Point.
    //
    // Parameters:
    //   size:
    //     The System.Windows.Size value to be converted.
    //
    // Returns:
    //     A System.Windows.Point equal in value to this instance of System.Windows.Size.
    //public static explicit operator Point(Size size);
    //
    // Summary:
    //     Explicitly converts an instance of System.Windows.Size to an instance of
    //     System.Windows.Vector.
    //
    // Parameters:
    //   size:
    //     The System.Windows.Size value to be converted.
    //
    // Returns:
    //     A System.Windows.Vector equal in value to this instance of System.Windows.Size.
    //public static explicit operator Vector(Size size);

    // Summary:
    //     Gets a value that represents a static empty System.Windows.Size.
    //
    // Returns:
    //     An empty instance of System.Windows.Size.
    public static Size Empty 
    {
      get
      {
        Contract.Ensures(Contract.Result<Size>().Width == Double.NegativeInfinity);
        Contract.Ensures(Contract.Result<Size>().Height == Double.NegativeInfinity);

        return default(Size);
      }
    }
    //
    // Summary:
    //     Gets or sets the System.Windows.Size.Height of this instance of System.Windows.Size.
    //
    // Returns:
    //     The System.Windows.Size.Height of this instance of System.Windows.Size. The
    //     default is 0. The value cannot be negative.
    public double Height
    {
      get
      {
        Contract.Ensures(this.IsEmpty || Contract.Result<double>() >= 0.0 || Double.IsNaN(Contract.Result<double>()));

        return default(double);
      }
      set
      {
        Contract.Requires(!this.IsEmpty);
        Contract.Requires(value >= 0.0 || Double.IsNaN(value));

        Contract.Ensures(this.Height == value || Double.IsNaN(value));
      }
    }
    //
    // Summary:
    //     Gets a value that indicates whether this instance of System.Windows.Size
    //     is System.Windows.Size.Empty.
    //
    // Returns:
    //     true if this instance of size is System.Windows.Size.Empty; otherwise false.
    public bool IsEmpty
    {
      get
      {
        Contract.Ensures(Contract.Result<bool>() == this.Width < 0.0);

        return default(bool);
      }
    }
    //
    // Summary:
    //     Gets or sets the System.Windows.Size.Width of this instance of System.Windows.Size.
    //
    // Returns:
    //     The System.Windows.Size.Width of this instance of System.Windows.Size. The
    //     default value is 0. The value cannot be negative.
    public double Width
    {
      get
      {
        Contract.Ensures(this.IsEmpty || Contract.Result<double>() >= 0.0 || Double.IsNaN(Contract.Result<double>()));

        return default(double);
      }
      set
      {
        Contract.Requires(!this.IsEmpty);
        Contract.Requires(value >= 0.0 || Double.IsNaN(value));

        Contract.Ensures(this.Width == value || Double.IsNaN(value));
      }
    }
    // Summary:
    //     Compares an object to an instance of System.Windows.Size for equality.
    //
    // Parameters:
    //   o:
    //     The System.Object to compare.
    //
    // Returns:
    //     true if the sizes are equal; otherwise, false.
    //public override bool Equals(object o);
    //
    // Summary:
    //     Compares a value to an instance of System.Windows.Size for equality.
    //
    // Parameters:
    //   value:
    //     The size to compare to this current instance of System.Windows.Size.
    //
    // Returns:
    //     true if the instances of System.Windows.Size are equal; otherwise, false.
    //public bool Equals(Size value);
    //
    // Summary:
    //     Compares two instances of System.Windows.Size for equality.
    //
    // Parameters:
    //   size1:
    //     The first instance of System.Windows.Size to compare.
    //
    //   size2:
    //     The second instance of System.Windows.Size to compare.
    //
    // Returns:
    //     true if the instances of System.Windows.Size are equal; otherwise, false.
    //public static bool Equals(Size size1, Size size2);
    //
    // Summary:
    //     Gets the hash code for this instance of System.Windows.Size.
    //
    // Returns:
    //     The hash code for this instance of System.Windows.Size.
    //public override int GetHashCode();
    //
    // Summary:
    //     Returns an instance of System.Windows.Size from a converted System.String.
    //
    // Parameters:
    //   source:
    //     A System.String value to parse to a System.Windows.Size value.
    //
    // Returns:
    //     An instance of System.Windows.Size.
    //public static Size Parse(string source);
    //
    // Summary:
    //     Returns a System.String that represents this System.Windows.Size object.
    //
    // Returns:
    //     A System.String that represents this instance of System.Windows.Size.
    //public override string ToString();
    //
    // Summary:
    //     Returns a System.String that represents this instance of System.Windows.Size.
    //
    // Parameters:
    //   provider:
    //     An object that provides a way to control formatting.
    //
    // Returns:
    //     A System.String that represents this System.Windows.Size object.
    //public string ToString(IFormatProvider provider);
  }
}
