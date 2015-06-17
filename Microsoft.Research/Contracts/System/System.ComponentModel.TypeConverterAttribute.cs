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
  //     Specifies what type to use as a converter for the object this attribute is
  //     bound to. This class cannot be inherited.
  [AttributeUsage(AttributeTargets.All)]
  public sealed class TypeConverterAttribute : Attribute {
    // Summary:
    //     Specifies the type to use as a converter for the object this attribute is
    //     bound to. This static field is read-only.
    public static readonly TypeConverterAttribute Default;

    // Summary:
    //     Initializes a new instance of the System.ComponentModel.TypeConverterAttribute
    //     class with the default type converter, which is an empty string ("").
    public TypeConverterAttribute () {
    //
    // Summary:
    //     Initializes a new instance of the System.ComponentModel.TypeConverterAttribute
    //     class, using the specified type name as the data converter for the object
    //     this attribute is bound to.
    //
    // Parameters:
    //   typeName:
    //     The fully qualified name of the class to use for data conversion for the
    //     object this attribute is bound to.
      return default(TypeConverterAttribute);
    }
    public TypeConverterAttribute (string typeName) {
    //
    // Summary:
    //     Initializes a new instance of the System.ComponentModel.TypeConverterAttribute
    //     class, using the specified type as the data converter for the object this
    //     attribute is bound to.
    //
    // Parameters:
    //   type:
    //     A System.Type that represents the type of the converter class to use for
    //     data conversion for the object this attribute is bound to.
      return default(TypeConverterAttribute);
    }
    public TypeConverterAttribute (Type type) {

    // Summary:
    //     Gets the fully qualified type name of the System.Type to use as a converter
    //     for the object this attribute is bound to.
    //
    // Returns:
    //     The fully qualified type name of the System.Type to use as a converter for
    //     the object this attribute is bound to, or an empty string ("") if none exists.
    //     The default value is an empty string ("").
      return default(TypeConverterAttribute);
    }
    public string ConverterTypeName { get; }

    // Summary:
    //     Returns whether the value of the given object is equal to the current System.ComponentModel.TypeConverterAttribute.
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
    //
    // Summary:
    //     Returns the hash code for this instance.
    //
    // Returns:
    //     A hash code for the current System.ComponentModel.TypeConverterAttribute.
      return default(override);
    }
    [Pure][Reads(ReadsAttribute.Reads.Owned)]
    public override int GetHashCode () {
      return default(override);
    }
  }
}
