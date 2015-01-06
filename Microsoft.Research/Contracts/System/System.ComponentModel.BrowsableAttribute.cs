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

namespace System.ComponentModel {
  // Summary:
  //     Specifies whether a property or event should be displayed in a Properties
  //     window.
  [AttributeUsage(AttributeTargets.All)]
  public sealed class BrowsableAttribute : Attribute {
    // Summary:
    //     Specifies the default value for the System.ComponentModel.BrowsableAttribute,
    //     which is System.ComponentModel.BrowsableAttribute.Yes. This static field
    //     is read-only.
    public static readonly BrowsableAttribute Default;
    //
    // Summary:
    //     Specifies that a property or event cannot be modified at design time. This
    //     static field is read-only.
    public static readonly BrowsableAttribute No;
    //
    // Summary:
    //     Specifies that a property or event can be modified at design time. This static
    //     field is read-only.
    public static readonly BrowsableAttribute Yes;

    // Summary:
    //     Initializes a new instance of the System.ComponentModel.BrowsableAttribute
    //     class.
    //
    // Parameters:
    //   browsable:
    //     true if a property or event can be modified at design time; otherwise, false.
    //     The default is true.
    public BrowsableAttribute (bool browsable) {

    // Summary:
    //     Gets a value indicating whether an object is browsable.
    //
    // Returns:
    //     true if the object is browsable; otherwise, false.
      return default(BrowsableAttribute);
    }
    public bool Browsable { get; }

    // Summary:
    //     Indicates whether this instance and a specified object are equal.
    //
    // Parameters:
    //   obj:
    //     Another object to compare to.
    //
    // Returns:
    //     true if obj is equal to this instance; otherwise, false.
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public override bool Equals (object obj) {
    //
    // Summary:
    //     Returns the hash code for this instance.
    //
    // Returns:
    //     A 32-bit signed integer hash code.
      return default(override);
    }
    [Pure][Reads(ReadsAttribute.Reads.Owned)]
    public override int GetHashCode () {
    //
    // Summary:
    //     Determines if this attribute is the default.
    //
    // Returns:
    //     true if the attribute is the default value for this attribute class; otherwise,
    //     false.
      return default(override);
    }
    public override bool IsDefaultAttribute () {
      return default(override);
    }
  }
}
