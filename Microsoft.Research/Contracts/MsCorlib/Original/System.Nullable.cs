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

using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;

using Microsoft.Contracts;

namespace System {
  // Summary:
  //     Represents an object whose underlying type is a value type, but can also
  //     be assigned null like a reference type.
  public struct Nullable<T> where T : struct {
    //
    // Summary:
    //     Initializes a new instance of the System.Nullable<T> structure to the specified
    //     value.
    //
    // Parameters:
    //   value:
    //     A value type.
    public Nullable(T value) {

      return default(Nullable(T);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static explicit operator T(T? value) {
      return default(explicit);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static implicit operator T?(T value) {

    // Summary:
    //     Gets a value indicating whether the current System.Nullable<T> object has
    //     a value.
    //
    // Returns:
    //     true if the current System.Nullable<T> object has a value; false if the current
    //     System.Nullable<T> object has no value.
      return default(implicit);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public bool HasValue { get; }
    //
    // Summary:
    //     Gets the value of the current System.Nullable<T> value.
    //
    // Returns:
    //     The value of the current System.Nullable<T> object if the System.Nullable<T>.HasValue
    //     property is true. An exception is thrown if the System.Nullable<T>.HasValue
    //     property is false.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     The System.Nullable<T>.HasValue property is false.
    public T Value { get; }

    // Summary:
    //     Indicates whether the current System.Nullable<T> value is equal to a specified
    //     object.
    //
    // Parameters:
    //   other:
    //     An object.
    //
    // Returns:
    //     true if the other parameter is equal to the current System.Nullable<T> value;
    //     otherwise, false.This table describes how equality is defined for the compared
    //     values: Return ValueDescriptiontrueThe other parameter is a System.Nullable<T>
    //     value, and the System.Nullable<T>.HasValue properties for the compared values
    //     are false. -or-The other parameter is a System.Nullable<T> value, the System.Nullable<T>.HasValue
    //     properties for the compared values are true, and the System.Nullable<T>.Value
    //     properties of the compared values are equal.falseThe other parameter is not
    //     a System.Nullable<T> value. -or-The other parameter is a System.Nullable<T>
    //     value, and the System.Nullable<T>.HasValue property is true for one compared
    //     value and false for the other compared value. -or-The other parameter is
    //     a System.Nullable<T> value, the System.Nullable<T>.HasValue properties for
    //     the two compared values are true, and the System.Nullable<T>.Value properties
    //     of the compared values are unequal.
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public override bool Equals(object other) {
    //
    // Summary:
    //     Retrieves the hash code of the object returned by the System.Nullable<T>.Value
    //     property.
    //
    // Returns:
    //     The hash code of the object returned by the System.Nullable<T>.Value property
    //     if the System.Nullable<T>.HasValue property is true, or zero if the System.Nullable<T>.HasValue
    //     property is false.
      return default(override);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public override int GetHashCode() {
    //
    // Summary:
    //     Retrieves the value of the current System.Nullable<T> object, or the object's
    //     default value.
    //
    // Returns:
    //     The value of the System.Nullable<T>.Value property if the System.Nullable<T>.HasValue
    //     property is true; otherwise, the default value of the current System.Nullable<T>
    //     object. The type of the default value is the type argument of the current
    //     System.Nullable<T> object, and the value of the default value consists solely
    //     of binary zeroes.
      return default(override);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public T GetValueOrDefault() {
    //
    // Summary:
    //     Retrieves the value of the current System.Nullable<T> object, or the specified
    //     default value.
    //
    // Parameters:
    //   defaultValue:
    //     A value to return if the System.Nullable<T>.HasValue property is false.
    //
    // Returns:
    //     The value of the System.Nullable<T>.Value property if the System.Nullable<T>.HasValue
    //     property is true; otherwise, the defaultValue parameter.
      return default(T);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public T GetValueOrDefault(T defaultValue) {
    //
    // Summary:
    //     Returns the text representation of the value of the current System.Nullable<T>
    //     object.
    //
    // Returns:
    //     The text representation of the value of the current System.Nullable<T> object
    //     if the System.Nullable<T>.HasValue property is true, or an empty string ("")
    //     if the System.Nullable<T>.HasValue property is false.
      return default(T);
    }
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public override string ToString() {
      return default(override);
    }
  }
}
