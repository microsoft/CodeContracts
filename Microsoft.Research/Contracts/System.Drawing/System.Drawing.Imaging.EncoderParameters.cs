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

namespace System.Drawing.Imaging {
  // Summary:
  //     Initializes a new instance of the System.Drawing.Imaging.EncoderParameters class
  //     that can contain one System.Drawing.Imaging.EncoderParameter object..
  public sealed class EncoderParameters : IDisposable {
    // Summary:
    //     Initializes a new instance of the System.Drawing.Imaging.EncoderParameters class
    //     that can contain one System.Drawing.Imaging.EncoderParameter object.
    public EncoderParameters() {
      Contract.Ensures(Param != null);
      Contract.Ensures(Param.Length == 1);
    }
    // Summary:
    //     Initializes a new instance of the System.Drawing.Imaging.EncoderParameters class
    //     that can contain the specified number of System.Drawing.Imaging.EncoderParameter
    //     objects.
    //
    // Parameters:
    //   count:
    //     An integer that specifies the number of System.Drawing.Imaging.EncoderParameter
    //     objects that the System.Drawing.Imaging.EncoderParameters object can contain.
    public EncoderParameters(int count) {
      Contract.Ensures(Param != null);
      Contract.Ensures(Param.Length == count);
    }
    //
    // Summary:
    //     Gets or sets an array of System.Drawing.Imaging.EncoderParameter objects.
    //
    // Returns:
    //     The array of System.Drawing.Imaging.EncoderParameter objects.
    public EncoderParameter[] Param { get; set; }
    //
    // Summary:
    //     Releases all resources used by this System.Drawing.Imaging.EncoderParameters
    //     object.
    public void Dispose() {
      Contract.Ensures(Param == null);
    }
  }
}
