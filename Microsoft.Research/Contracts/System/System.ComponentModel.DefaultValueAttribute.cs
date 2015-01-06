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
  //     Specifies the default value for a property.
  [AttributeUsage(AttributeTargets.All)]
  public class DefaultValueAttribute : Attribute {
    // Summary:
    //     Initializes a new instance of the System.ComponentModel.DefaultValueAttribute
    //     class using a System.Boolean value.
    //
    // Parameters:
    //   value:
    //     A System.Boolean that is the default value.
    public DefaultValueAttribute (bool value) {
    //
    // Summary:
    //     Initializes a new instance of the System.ComponentModel.DefaultValueAttribute
    //     class using an 8-bit unsigned integer.
    //
    // Parameters:
    //   value:
    //     An 8-bit unsigned integer that is the default value.
      return default(DefaultValueAttribute);
    }
    public DefaultValueAttribute (byte value) {
    //
    // Summary:
    //     Initializes a new instance of the System.ComponentModel.DefaultValueAttribute
    //     class using a Unicode character.
    //
    // Parameters:
    //   value:
    //     A Unicode character that is the default value.
      return default(DefaultValueAttribute);
    }
    public DefaultValueAttribute (char value) {
    //
    // Summary:
    //     Initializes a new instance of the System.ComponentModel.DefaultValueAttribute
    //     class using a double-precision floating point number.
    //
    // Parameters:
    //   value:
    //     A double-precision floating point number that is the default value.
      return default(DefaultValueAttribute);
    }
    public DefaultValueAttribute (double value) {
    //
    // Summary:
    //     Initializes a new instance of the System.ComponentModel.DefaultValueAttribute
    //     class using a single-precision floating point number.
    //
    // Parameters:
    //   value:
    //     A single-precision floating point number that is the default value.
      return default(DefaultValueAttribute);
    }
    public DefaultValueAttribute (float value) {
    //
    // Summary:
    //     Initializes a new instance of the System.ComponentModel.DefaultValueAttribute
    //     class using a 32-bit signed integer.
    //
    // Parameters:
    //   value:
    //     A 32-bit signed integer that is the default value.
      return default(DefaultValueAttribute);
    }
    public DefaultValueAttribute (int value) {
    //
    // Summary:
    //     Initializes a new instance of the System.ComponentModel.DefaultValueAttribute
    //     class using a 64-bit signed integer.
    //
    // Parameters:
    //   value:
    //     A 64-bit signed integer that is the default value.
      return default(DefaultValueAttribute);
    }
    public DefaultValueAttribute (long value) {
    //
    // Summary:
    //     Initializes a new instance of the System.ComponentModel.DefaultValueAttribute
    //     class.
    //
    // Parameters:
    //   value:
    //     An System.Object that represents the default value.
      return default(DefaultValueAttribute);
    }
    public DefaultValueAttribute (object value) {
    //
    // Summary:
    //     Initializes a new instance of the System.ComponentModel.DefaultValueAttribute
    //     class using a 16-bit signed integer.
    //
    // Parameters:
    //   value:
    //     A 16-bit signed integer that is the default value.
      return default(DefaultValueAttribute);
    }
    public DefaultValueAttribute (short value) {
    //
    // Summary:
    //     Initializes a new instance of the System.ComponentModel.DefaultValueAttribute
    //     class using a System.String.
    //
    // Parameters:
    //   value:
    //     A System.String that is the default value.
      return default(DefaultValueAttribute);
    }
    public DefaultValueAttribute (string value) {
    //
    // Summary:
    //     Initializes a new instance of the System.ComponentModel.DefaultValueAttribute
    //     class, converting the specified value to the specified type, and using an
    //     invariant culture as the translation context.
    //
    // Parameters:
    //   type:
    //     A System.Type that represents the type to convert the value to.
    //
    //   value:
    //     A System.String that can be converted to the type using the System.ComponentModel.TypeConverter
    //     for the type and the U.S. English culture.
      return default(DefaultValueAttribute);
    }
    public DefaultValueAttribute (Type type, string value) {

    // Summary:
    //     Gets the default value of the property this attribute is bound to.
    //
    // Returns:
    //     An System.Object that represents the default value of the property this attribute
    //     is bound to.
      return default(DefaultValueAttribute);
    }
    public virtual object Value { get; }

    // Summary:
    //     Returns whether the value of the given object is equal to the current System.ComponentModel.DefaultValueAttribute.
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
    //     Sets the default value for the property to which this attribute is bound.
    //
    // Parameters:
    //   value:
    //     The default value.
    protected void SetValue (object value);
      return default(override);
    }
  }
}
