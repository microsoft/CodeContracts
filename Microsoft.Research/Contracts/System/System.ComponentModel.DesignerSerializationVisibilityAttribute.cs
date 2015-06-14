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
  //     Specifies the type of persistence to use when serializing a property on a
  //     component at design time.
  [AttributeUsage(AttributeTargets.Method|AttributeTargets.Property)]
  public sealed class DesignerSerializationVisibilityAttribute : Attribute {
    // Summary:
    //     Specifies that a serializer should serialize the contents of the property,
    //     rather than the property itself. This field is read-only.
    public static readonly DesignerSerializationVisibilityAttribute Content;
    //
    // Summary:
    //     Specifies the default value, which is System.ComponentModel.DesignerSerializationVisibilityAttribute.Visible,
    //     that is, a visual designer uses default rules to generate the value of a
    //     property. This static field is read-only.
    public static readonly DesignerSerializationVisibilityAttribute Default;
    //
    // Summary:
    //     Specifies that a serializer should not serialize the value of the property.
    //     This static field is read-only.
    public static readonly DesignerSerializationVisibilityAttribute Hidden;
    //
    // Summary:
    //     Specifies that a serializer should be allowed to serialize the value of the
    //     property. This static field is read-only.
    public static readonly DesignerSerializationVisibilityAttribute Visible;

    // Summary:
    //     Initializes a new instance of the System.ComponentModel.DesignerSerializationVisibilityAttribute
    //     class using the specified System.ComponentModel.DesignerSerializationVisibility
    //     value.
    //
    // Parameters:
    //   visibility:
    //     One of the System.ComponentModel.DesignerSerializationVisibility values.
    public DesignerSerializationVisibilityAttribute (DesignerSerializationVisibility visibility) {

    // Summary:
    //     Gets a value indicating the basic serialization mode a serializer should
    //     use when determining whether and how to persist the value of a property.
    //
    // Returns:
    //     One of the System.ComponentModel.DesignerSerializationVisibility values.
    //     The default is System.ComponentModel.DesignerSerializationVisibility.Visible.
      return default(DesignerSerializationVisibilityAttribute);
    }
    public DesignerSerializationVisibility Visibility { get; }

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
    //     Returns the hash code for this object.
    //
    // Returns:
    //     A 32-bit signed integer hash code.
      return default(override);
    }
    [Pure][Reads(ReadsAttribute.Reads.Owned)]
    public override int GetHashCode () {
    //
    // Summary:
    //     Gets a value indicating whether the current value of the attribute is the
    //     default value for the attribute.
    //
    // Returns:
    //     true if the attribute is set to the default value; otherwise, false.
      return default(override);
    }
    public override bool IsDefaultAttribute () {
      return default(override);
    }
  }
}
