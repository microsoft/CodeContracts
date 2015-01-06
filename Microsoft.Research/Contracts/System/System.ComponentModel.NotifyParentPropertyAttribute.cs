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
  //     Indicates that the parent property is notified when the value of the property
  //     that this attribute is applied to is modified. This class cannot be inherited.
  [AttributeUsage(AttributeTargets.Property)]
  public sealed class NotifyParentPropertyAttribute : Attribute {
    // Summary:
    //     Indicates the default attribute state, that the property should not notify
    //     the parent property of changes to its value. This field is read-only.
    public static readonly NotifyParentPropertyAttribute Default;
    //
    // Summary:
    //     Indicates that the parent property is not be notified of changes to the value
    //     of the property. This field is read-only.
    public static readonly NotifyParentPropertyAttribute No;
    //
    // Summary:
    //     Indicates that the parent property is notified of changes to the value of
    //     the property. This field is read-only.
    public static readonly NotifyParentPropertyAttribute Yes;

    // Summary:
    //     Initializes a new instance of the System.ComponentModel.NotifyParentPropertyAttribute
    //     class, using the specified value to determine whether the parent property
    //     is notified of changes to the value of the property.
    //
    // Parameters:
    //   notifyParent:
    //     true if the parent should be notified of changes; otherwise, false.
    public NotifyParentPropertyAttribute (bool notifyParent) {

    // Summary:
    //     Gets or sets a value indicating whether the parent property should be notified
    //     of changes to the value of the property.
    //
    // Returns:
    //     true if the parent property should be notified of changes; otherwise, false.
      return default(NotifyParentPropertyAttribute);
    }
    public bool NotifyParent { get; }

    // Summary:
    //     Gets a value indicating whether the specified object is the same as the current
    //     object.
    //
    // Parameters:
    //   obj:
    //     The object to test for equality.
    //
    // Returns:
    //     true if the object is the same as this object; otherwise, false.
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public override bool Equals (object obj) {
    //
    // Summary:
    //     Gets the hash code for this object.
    //
    // Returns:
    //     The hash code for the object the attribute belongs to.
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
    //     true if the current value of the attribute is the default value of the attribute;
    //     otherwise, false.
      return default(override);
    }
    public override bool IsDefaultAttribute () {
      return default(override);
    }
  }
}
