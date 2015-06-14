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
using System.Diagnostics.Contracts;

namespace System.Drawing.Imaging
{
  // Summary:
  //     Provides properties that get the frame dimensions of an image. Not inheritable.
  public sealed class FrameDimension
  {
    // Summary:
    //     Initializes a new instance of the System.Drawing.Imaging.FrameDimension class
    //     using the specified Guid structure.
    //
    // Parameters:
    //   guid:
    //     A Guid structure that contains a GUID for this System.Drawing.Imaging.FrameDimension
    //     object.
    //public FrameDimension(Guid guid);

    // Summary:
    //     Gets a globally unique identifier (GUID) that represents this System.Drawing.Imaging.FrameDimension
    //     object.
    //
    // Returns:
    //     A Guid structure that contains a GUID that represents this System.Drawing.Imaging.FrameDimension
    //     object.
    //public Guid Guid { get; }
    //
    // Summary:
    //     Gets the page dimension.
    //
    // Returns:
    //     The page dimension.
    public static FrameDimension Page
    {
      get
      {
        Contract.Ensures(Contract.Result<FrameDimension>() != null);

        return default(FrameDimension);
      }
    }
    //
    // Summary:
    //     Gets the resolution dimension.
    //
    // Returns:
    //     The resolution dimension.
    public static FrameDimension Resolution
    {
      get
      {
        Contract.Ensures(Contract.Result<FrameDimension>() != null);

        return default(FrameDimension);
      }
    }

    //
    // Summary:
    //     Gets the time dimension.
    //
    // Returns:
    //     The time dimension.
    public static FrameDimension Time
    {
      get
      {
        Contract.Ensures(Contract.Result<FrameDimension>() != null);

        return default(FrameDimension);
      }
    }

    // Summary:
    //     Returns a value that indicates whether the specified object is a System.Drawing.Imaging.FrameDimension
    //     equivalent to this System.Drawing.Imaging.FrameDimension object.
    //
    // Parameters:
    //   o:
    //     The object to test.
    //
    // Returns:
    //     Returns true if o is a System.Drawing.Imaging.FrameDimension equivalent to
    //     this System.Drawing.Imaging.FrameDimension object; otherwise, false.
    //public override bool Equals(object o);
    //
    // Summary:
    //     Returns a hash code for this System.Drawing.Imaging.FrameDimension object.
    //
    // Returns:
    //     Returns an int value that is the hash code of this System.Drawing.Imaging.FrameDimension
    //     object.
    //public override int GetHashCode();
    //
    // Summary:
    //     Converts this System.Drawing.Imaging.FrameDimension object to a human-readable
    //     string.
    //
    // Returns:
    //     A string that represents this System.Drawing.Imaging.FrameDimension object.
    //public override string ToString();
  }
}
