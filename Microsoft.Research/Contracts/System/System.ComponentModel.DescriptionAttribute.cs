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
  //     Specifies a description for a property or event.
  [AttributeUsage(AttributeTargets.All)]
  public class DescriptionAttribute : Attribute {
    // Summary:
    //     Specifies the default value for the System.ComponentModel.DescriptionAttribute,
    //     which is an empty string (""). This static field is read-only.
    public static readonly DescriptionAttribute Default;

    // Summary:
    //     Initializes a new instance of the System.ComponentModel.DescriptionAttribute
    //     class with no parameters.
    public DescriptionAttribute () {
    //
    // Summary:
    //     Initializes a new instance of the System.ComponentModel.DescriptionAttribute
    //     class with a description.
    //
    // Parameters:
    //   description:
    //     The description text.
      return default(DescriptionAttribute);
    }
    public DescriptionAttribute (string description) {

    // Summary:
    //     Gets the description stored in this attribute.
    //
    // Returns:
    //     The description stored in this attribute.
      return default(DescriptionAttribute);
    }
    public virtual string Description { get; }
    //
    // Summary:
    //     Gets or sets the string stored as the description.
    //
    // Returns:
    //     The string stored as the description. The default value is an empty string
    //     ("").
    protected string DescriptionValue { get; set; }

    // Summary:
    //     Returns whether the value of the given object is equal to the current System.ComponentModel.DescriptionAttribute.
    //
    // Parameters:
    //   obj:
    //     The object to test the value equality of.
    //
    // Returns:
    //     true if the value of the given object is equal to that of the current; otherwise,
    //     false.
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public override bool Equals (object obj) {
      return default(override);
    }
    [Pure][Reads(ReadsAttribute.Reads.Owned)]
    public override int GetHashCode () {
    //
    // Summary:
    //     Returns a value indicating whether this is the default System.ComponentModel.DescriptionAttribute
    //     instance.
    //
    // Returns:
    //     true, if this is the default System.ComponentModel.DescriptionAttribute instance;
    //     otherwise, false.
      return default(override);
    }
    public override bool IsDefaultAttribute () {
      return default(override);
    }
  }
}
