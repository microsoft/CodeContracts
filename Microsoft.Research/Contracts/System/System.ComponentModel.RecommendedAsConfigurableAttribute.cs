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
  //     Specifies that the property can be used as an application setting.
  [AttributeUsage(AttributeTargets.Property)]
  [Obsolete("Use System.ComponentModel.SettingsBindableAttribute instead to work with the new settings model.")]
  public class RecommendedAsConfigurableAttribute : Attribute {
    // Summary:
    //     Specifies the default value for the System.ComponentModel.RecommendedAsConfigurableAttribute,
    //     which is System.ComponentModel.RecommendedAsConfigurableAttribute.No. This
    //     static field is read-only.
    public static readonly RecommendedAsConfigurableAttribute Default;
    //
    // Summary:
    //     Specifies that a property cannot be used as an application setting. This
    //     static field is read-only.
    public static readonly RecommendedAsConfigurableAttribute No;
    //
    // Summary:
    //     Specifies that a property can be used as an application setting. This static
    //     field is read-only.
    public static readonly RecommendedAsConfigurableAttribute Yes;

    // Summary:
    //     Initializes a new instance of the System.ComponentModel.RecommendedAsConfigurableAttribute
    //     class.
    //
    // Parameters:
    //   recommendedAsConfigurable:
    //     true if the property this attribute is bound to can be used as an application
    //     setting; otherwise, false.
    public RecommendedAsConfigurableAttribute (bool recommendedAsConfigurable) {

    // Summary:
    //     Gets a value indicating whether the property this attribute is bound to can
    //     be used as an application setting.
    //
    // Returns:
    //     true if the property this attribute is bound to can be used as an application
    //     setting; otherwise, false.
      return default(RecommendedAsConfigurableAttribute);
    }
    public bool RecommendedAsConfigurable { get; }

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
    //     A hash code for the current System.ComponentModel.RecommendedAsConfigurableAttribute.
      return default(override);
    }
    [Pure][Reads(ReadsAttribute.Reads.Owned)]
    public override int GetHashCode () {
    //
    // Summary:
    //     Indicates whether the value of this instance is the default value for the
    //     class.
    //
    // Returns:
    //     true if this instance is the default attribute for the class; otherwise,
    //     false.
      return default(override);
    }
    public override bool IsDefaultAttribute () {
      return default(override);
    }
  }
}
