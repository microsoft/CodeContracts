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

namespace System {

  // Summary:
  //     Represents an object whose underlying type is a value type, but can also
  //     be assigned null like a reference type.
  public struct Nullable<T> where T : struct {

    /// <summary>
    /// We represent this bool in order to have the connection between the default constructor/initobj
    /// and the outcome of IsValid.
    /// </summary>
    private bool hasValue;

    //
    // Summary:
    //     Initializes a new instance of the System.Nullable<T> structure to the specified
    //     value.
    //
    // Parameters:
    //   value:
    //     A value type.
    public Nullable(T value) {
      Contract.Ensures(Contract.ValueAtReturn(out this).HasValue);
    }

    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static explicit operator T(T? value) {
      return default(T);
    }

    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public static implicit operator T?(T value) {
      Contract.Ensures(Contract.Result<T?>().HasValue);
      return default(T);
    }

    // Summary:
    //     Gets a value indicating whether the current System.Nullable<T> object has
    //     a value.
    //
    // Returns:
    //     true if the current System.Nullable<T> object has a value; false if the current
    //     System.Nullable<T> object has no value.
    [Pure]
    [Reads(ReadsAttribute.Reads.Nothing)]
    public bool HasValue
    {
      get
      {
        Contract.Ensures(Contract.Result<bool>() == this.hasValue);
        return default(bool);
      }
    }

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
    public T Value
    {
      get
      {
        Contract.Requires(HasValue);
        return default(T);
      }
    }

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
    [Pure][Reads(ReadsAttribute.Reads.Nothing)]
    public T GetValueOrDefault()
    {
      return default(T);
    }
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
    [Pure]
    [Reads(ReadsAttribute.Reads.Nothing)]
    public T GetValueOrDefault(T defaultValue) {
      return default(T);
    }
  }

  // Summary:
  //     Supports a value type that can be assigned null like a reference type. This
  //     class cannot be inherited.
  public static class Nullable
  {
    // Summary:
    //     Compares the relative values of two System.Nullable<T> objects.
    //
    // Parameters:
    //   n1:
    //     A System.Nullable<T> object.
    //
    //   n2:
    //     A System.Nullable<T> object.
    //
    // Type parameters:
    //   T:
    //     The underlying value type of the n1 and n2 parameters.
    //
    // Returns:
    //     An integer that indicates the relative values of the n1 and n2 parameters.
    //      Return Value Description Less than zero The System.Nullable<T>.HasValue
    //     property for n1 is false, and the System.Nullable<T>.HasValue property for
    //     n2 is true.  -or- The System.Nullable<T>.HasValue properties for n1 and n2
    //     are true, and the value of the System.Nullable<T>.Value property for n1 is
    //     less than the value of the System.Nullable<T>.Value property for n2.  Zero
    //     The System.Nullable<T>.HasValue properties for n1 and n2 are false.  -or-
    //     The System.Nullable<T>.HasValue properties for n1 and n2 are true, and the
    //     value of the System.Nullable<T>.Value property for n1 is equal to the value
    //     of the System.Nullable<T>.Value property for n2.  Greater than zero The System.Nullable<T>.HasValue
    //     property for n1 is true, and the System.Nullable<T>.HasValue property for
    //     n2 is false.  -or- The System.Nullable<T>.HasValue properties for n1 and
    //     n2 are true, and the value of the System.Nullable<T>.Value property for n1
    //     is greater than the value of the System.Nullable<T>.Value property for n2.
    //public static int Compare<T>(T? n1, T? n2) where T : struct;
    //
    // Summary:
    //     Indicates whether two specified System.Nullable<T> objects are equal.
    //
    // Parameters:
    //   n1:
    //     A System.Nullable<T> object.
    //
    //   n2:
    //     A System.Nullable<T> object.
    //
    // Type parameters:
    //   T:
    //     The underlying value type of the n1 and n2 parameters.
    //
    // Returns:
    //     true if the n1 parameter is equal to the n2 parameter; otherwise, false.
    //     The return value depends on the System.Nullable<T>.HasValue and System.Nullable<T>.Value
    //     properties of the two parameters that are compared.  Return Value Description
    //     true The System.Nullable<T>.HasValue properties for n1 and n2 are false.
    //     -or- The System.Nullable<T>.HasValue properties for n1 and n2 are true, and
    //     the System.Nullable<T>.Value properties of the parameters are equal.  false
    //     The System.Nullable<T>.HasValue property is true for one parameter and false
    //     for the other parameter.  -or- The System.Nullable<T>.HasValue properties
    //     for n1 and n2 are true, and the System.Nullable<T>.Value properties of the
    //     parameters are unequal.
    //public static bool Equals<T>(T? n1, T? n2) where T : struct;
    //
    // Summary:
    //     Returns the underlying type argument of the specified nullable type.
    //
    // Parameters:
    //   nullableType:
    //     A System.Type object that describes a closed generic nullable type.
    //
    // Returns:
    //     The type argument of the nullableType parameter, if the nullableType parameter
    //     is a closed generic nullable type; otherwise, null.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     nullableType is null.
    [Pure]
    public static Type GetUnderlyingType(Type nullableType)
    {
      Contract.Requires(nullableType != null);

      return (default(Type));
    }
  }
}
