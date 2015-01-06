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
using System.Reflection;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Data.SqlTypes
{
  // Summary:
  //     Represents a variable-length stream of binary data to be stored in or retrieved
  //     from a database.
  //[Serializable]
  //[XmlSchemaProvider("GetXsdType")]
  public struct SqlBinary// : INullable, IComparable, IXmlSerializable
  {
    // Summary:
    //     Represents a null value that can be assigned to the System.Data.SqlTypes.SqlBinary.Value
    //     property of a System.Data.SqlTypes.SqlBinary structure.
    ////public static readonly SqlBinary Null;

    //
    // Summary:
    //     Initializes a new instance of the System.Data.SqlTypes.SqlBinary structure,
    //     setting the System.Data.SqlTypes.SqlBinary.Value property to the contents
    //     of the supplied byte array.
    //
    // Parameters:
    //   value:
    //     The byte array to be stored or retrieved.
    //public SqlBinary(byte[] value);

    // Summary:
    //     Compares two System.Data.SqlTypes.SqlBinary structures to determine whether
    //     they are not equal.
    //
    // Parameters:
    //   x:
    //     A System.Data.SqlTypes.SqlBinary object.
    //
    //   y:
    //     A System.Data.SqlTypes.SqlBinary object.
    //
    // Returns:
    //     A System.Data.SqlTypes.SqlBoolean that is System.Data.SqlTypes.SqlBoolean.True
    //     if the two instances are not equal or System.Data.SqlTypes.SqlBoolean.False
    //     if the two instances are equal. If either instance of System.Data.SqlTypes.SqlBinary
    //     is null, the System.Data.SqlTypes.SqlBoolean.Value of the System.Data.SqlTypes.SqlBoolean
    //     will be System.Data.SqlTypes.SqlBoolean.Null.
    //public static SqlBoolean operator !=(SqlBinary x, SqlBinary y);
    //
    // Summary:
    //     Concatenates the two System.Data.SqlTypes.SqlBinary parameters to create
    //     a new System.Data.SqlTypes.SqlBinary structure.
    //
    // Parameters:
    //   x:
    //     A System.Data.SqlTypes.SqlBinary object.
    //
    //   y:
    //     A System.Data.SqlTypes.SqlBinary object.
    //
    // Returns:
    //     The concatenated values of the x and y parameters.
    //public static SqlBinary operator +(SqlBinary x, SqlBinary y);
    //
    // Summary:
    //     Compares two System.Data.SqlTypes.SqlBinary structures to determine whether
    //     the first is less than the second.
    //
    // Parameters:
    //   x:
    //     A System.Data.SqlTypes.SqlBinary object.
    //
    //   y:
    //     A System.Data.SqlTypes.SqlBinary object.
    //
    // Returns:
    //     A System.Data.SqlTypes.SqlBoolean that is System.Data.SqlTypes.SqlBoolean.True
    //     if the first instance is less than the second instance. Otherwise System.Data.SqlTypes.SqlBoolean.False.
    //     If either instance of System.Data.SqlTypes.SqlBinary is null, the System.Data.SqlTypes.SqlBoolean.Value
    //     of the System.Data.SqlTypes.SqlBoolean will be System.Data.SqlTypes.SqlBoolean.Null.
    //public static SqlBoolean operator <(SqlBinary x, SqlBinary y);
    //
    // Summary:
    //     Compares two System.Data.SqlTypes.SqlBinary structures to determine whether
    //     the first is less than or equal to the second.
    //
    // Parameters:
    //   x:
    //     A System.Data.SqlTypes.SqlBinary object.
    //
    //   y:
    //     A System.Data.SqlTypes.SqlBinary object.
    //
    // Returns:
    //     A System.Data.SqlTypes.SqlBoolean that is System.Data.SqlTypes.SqlBoolean.True
    //     if the first instance is less than or equal to the second instance. Otherwise
    //     System.Data.SqlTypes.SqlBoolean.False. If either instance of System.Data.SqlTypes.SqlBinary
    //     is null, the System.Data.SqlTypes.SqlBoolean.Value of the System.Data.SqlTypes.SqlBoolean
    //     will be System.Data.SqlTypes.SqlBoolean.Null.
    //public static SqlBoolean operator <=(SqlBinary x, SqlBinary y);
    //
    // Summary:
    //     Compares two System.Data.SqlTypes.SqlBinary structures to determine whether
    //     they are equal.
    //
    // Parameters:
    //   x:
    //     A System.Data.SqlTypes.SqlBinary object.
    //
    //   y:
    //     A System.Data.SqlTypes.SqlBinary object.
    //
    // Returns:
    //     A System.Data.SqlTypes.SqlBoolean that is System.Data.SqlTypes.SqlBoolean.True
    //     if the two instances are equal or System.Data.SqlTypes.SqlBoolean.False if
    //     the two instances are not equal. If either instance of System.Data.SqlTypes.SqlBinary
    //     is null, the System.Data.SqlTypes.SqlBoolean.Value of the System.Data.SqlTypes.SqlBoolean
    //     will be System.Data.SqlTypes.SqlBoolean.Null.
    //public static SqlBoolean operator ==(SqlBinary x, SqlBinary y);
    //
    // Summary:
    //     Compares two System.Data.SqlTypes.SqlBinary structures to determine whether
    //     the first is greater than the second.
    //
    // Parameters:
    //   x:
    //     A System.Data.SqlTypes.SqlBinary object.
    //
    //   y:
    //     A System.Data.SqlTypes.SqlBinary object.
    //
    // Returns:
    //     A System.Data.SqlTypes.SqlBoolean that is System.Data.SqlTypes.SqlBoolean.True
    //     if the first instance is greater than the second instance. Otherwise System.Data.SqlTypes.SqlBoolean.False.
    //     If either instance of System.Data.SqlTypes.SqlBinary is null, the System.Data.SqlTypes.SqlBoolean.Value
    //     of the System.Data.SqlTypes.SqlBoolean will be System.Data.SqlTypes.SqlBoolean.Null.
    //public static SqlBoolean operator >(SqlBinary x, SqlBinary y);
    //
    // Summary:
    //     Compares two System.Data.SqlTypes.SqlBinary structues to determine whether
    //     the first is greater than or equal to the second.
    //
    // Parameters:
    //   x:
    //     A System.Data.SqlTypes.SqlBinary object.
    //
    //   y:
    //     A System.Data.SqlTypes.SqlBinary object.
    //
    // Returns:
    //     A System.Data.SqlTypes.SqlBoolean that is System.Data.SqlTypes.SqlBoolean.True
    //     if the first instance is greater than or equal to the second instance. Otherwise
    //     System.Data.SqlTypes.SqlBoolean.False. If either instance of System.Data.SqlTypes.SqlBinary
    //     is null, the System.Data.SqlTypes.SqlBoolean.Value of the System.Data.SqlTypes.SqlBoolean
    //     will be System.Data.SqlTypes.SqlBoolean.Null.
    //public static SqlBoolean operator >=(SqlBinary x, SqlBinary y);
    //
    // Summary:
    //     Converts a System.Data.SqlTypes.SqlBinary structure to a System.Byte array.
    //
    // Parameters:
    //   x:
    //     The System.Data.SqlTypes.SqlBinary structure to be converted.
    //
    // Returns:
    //     A System.Byte array.
    //public static explicit operator byte[](SqlBinary x);
    //
    // Summary:
    //     Converts a System.Data.SqlTypes.SqlGuid structure to a System.Data.SqlTypes.SqlBinary
    //     structure.
    //
    // Parameters:
    //   x:
    //     The System.Data.SqlTypes.SqlGuid structure to be converted.
    //
    // Returns:
    //     The System.Data.SqlTypes.SqlGuid structure to be converted.
    //public static explicit operator SqlBinary(SqlGuid x);
    //
    // Summary:
    //     Converts an array of bytes to a System.Data.SqlTypes.SqlBinary structure.
    //
    // Parameters:
    //   x:
    //     The array of bytes to be converted.
    //
    // Returns:
    //     A System.Data.SqlTypes.SqlBinary structure that represents the converted
    //     array of bytes.
    //public static implicit operator SqlBinary(byte//[] x);

    // Summary:
    //     Gets a value that indicates whether the System.Data.SqlTypes.SqlBinary.Value
    //     property of the System.Data.SqlTypes.SqlBinary structure is null. This property
    //     is read-only.
    //
    // Returns:
    //     true if Value is null. Otherwise false.
    //public bool IsNull { get; }
    //
    // Summary:
    //     Gets the length in bytes of the System.Data.SqlTypes.SqlBinary.Value property.
    //     This property is read-only.
    //
    // Returns:
    //     The length of the binary data in the System.Data.SqlTypes.SqlBinary.Value
    //     property.
    //
    // Exceptions:
    //   System.Data.SqlTypes.SqlNullValueException:
    //     The System.Data.SqlTypes.SqlBinary.Length property is read when the System.Data.SqlTypes.SqlBinary.Value
    //     property contains System.Data.SqlTypes.SqlBinary.Null.
    //public int Length { get; }
    //
    // Summary:
    //     Gets the value of the System.Data.SqlTypes.SqlBinary structure. This property
    //     is read-only.
    //
    // Returns:
    //     The value of the System.Data.SqlTypes.SqlBinary structure.
    //
    // Exceptions:
    //   System.Data.SqlTypes.SqlNullValueException:
    //     The System.Data.SqlTypes.SqlBinary.Value property is read when the property
    //     contains System.Data.SqlTypes.SqlBinary.Null.
    //public byte[] Value { get; }

    // Summary:
    //     Gets the single byte from the System.Data.SqlTypes.SqlBinary.Value property
    //     located at the position indicated by the integer parameter, index. If index
    //     indicates a position beyond the end of the byte array, a System.Data.SqlTypes.SqlNullValueException
    //     will be raised. This property is read-only.
    //
    // Parameters:
    //   index:
    //     The position of the byte to be retrieved.
    //
    // Returns:
    //     The byte located at the position indicated by the integer parameter.
    //
    // Exceptions:
    //   System.Data.SqlTypes.SqlNullValueException:
    //     The property is read when the System.Data.SqlTypes.SqlBinary.Value property
    //     contains System.Data.SqlTypes.SqlBinary.Null- or - The index parameter indicates
    //     a position byond the length of the byte array as indicated by the System.Data.SqlTypes.SqlBinary.Length
    //     property.
    //public byte this[int index] { get; }

    // Summary:
    //     Concatenates two specified System.Data.SqlTypes.SqlBinary values to create
    //     a new System.Data.SqlTypes.SqlBinary structure.
    //
    // Parameters:
    //   x:
    //     A System.Data.SqlTypes.SqlBinary.
    //
    //   y:
    //     A System.Data.SqlTypes.SqlBinary.
    //
    // Returns:
    //     A System.Data.SqlTypes.SqlBinary that is the concatenated value of x and
    //     y.
    //public static SqlBinary Add(SqlBinary x, SqlBinary y);
    //
    // Summary:
    //     Compares this System.Data.SqlTypes.SqlBinary object to the supplied object
    //     and returns an indication of their relative values.
    //
    // Parameters:
    //   value:
    //     The object to be compared to this System.Data.SqlTypes.SqlBinary structure.
    //
    // Returns:
    //     A signed number that indicates the relative values of this System.Data.SqlTypes.SqlBinary
    //     structure and the object.Return value Condition Less than zero The value
    //     of this System.Data.SqlTypes.SqlBinary object is less than the object. Zero
    //     This System.Data.SqlTypes.SqlBinary object is the same as object. Greater
    //     than zero This System.Data.SqlTypes.SqlBinary object is greater than object.-or-
    //     The object is a null reference.
    //public int CompareTo(object value);
    //
    // Summary:
    //     Compares this System.Data.SqlTypes.SqlBinary object to the supplied System.Data.SqlTypes.SqlBinary
    //     object and returns an indication of their relative values.
    //
    // Parameters:
    //   value:
    //     The System.Data.SqlTypes.SqlBinary object to be compared to this System.Data.SqlTypes.SqlBinary
    //     structure.
    //
    // Returns:
    //     A signed number that indicates the relative values of this System.Data.SqlTypes.SqlBinary
    //     structure and the object.Return value Condition Less than zero The value
    //     of this System.Data.SqlTypes.SqlBinary object is less than the object. Zero
    //     This System.Data.SqlTypes.SqlBinary object is the same as object. Greater
    //     than zero This System.Data.SqlTypes.SqlBinary object is greater than object.-or-
    //     The object is a null reference.
    //public int CompareTo(SqlBinary value);
    //
    // Summary:
    //     Concatenates two System.Data.SqlTypes.SqlBinary structures to create a new
    //     System.Data.SqlTypes.SqlBinary structure.
    //
    // Parameters:
    //   x:
    //     A System.Data.SqlTypes.SqlBinary structure.
    //
    //   y:
    //     A System.Data.SqlTypes.SqlBinary structure.
    //
    // Returns:
    //     The concatenated values of the x and y parameters.
    //public static SqlBinary Concat(SqlBinary x, SqlBinary y);
    //
    // Summary:
    //     Compares the supplied object parameter to the System.Data.SqlTypes.SqlBinary.Value
    //     property of the System.Data.SqlTypes.SqlBinary object.
    //
    // Parameters:
    //   value:
    //     The object to be compared.
    //
    // Returns:
    //     true if object is an instance of System.Data.SqlTypes.SqlBinary and the two
    //     are equal; otherwise false.
    //public override bool Equals(object value);
    //
    // Summary:
    //     Compares two System.Data.SqlTypes.SqlBinary structures to determine whether
    //     they are equal.
    //
    // Parameters:
    //   x:
    //     A System.Data.SqlTypes.SqlBinary structure.
    //
    //   y:
    //     A System.Data.SqlTypes.SqlBinary structure.
    //
    // Returns:
    //     true if the two values are equal. Otherwise, false. If either instance is
    //     null, then the SqlBinary will be null.
    //public static SqlBoolean Equals(SqlBinary x, SqlBinary y);
    //
    // Summary:
    //     Returns the hash code for this System.Data.SqlTypes.SqlBinary structure.
    //
    // Returns:
    //     A 32-bit signed integer hash code.
    //public override int GetHashCode();
    //
    // Summary:
    //     Returns the XML Schema definition language (XSD) of the specified System.Xml.Schema.XmlSchemaSet.
    //
    // Parameters:
    //   schemaSet:
    //     An System.Xml.Schema.XmlSchemaSet.
    //
    // Returns:
    //     A string that indicates the XSD of the specified System.Xml.Schema.XmlSchemaSet.
    //public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet);
    //
    // Summary:
    //     Compares two System.Data.SqlTypes.SqlBinary structures to determine whether
    //     the first is greater than the second.
    //
    // Parameters:
    //   x:
    //     A System.Data.SqlTypes.SqlBinary structure.
    //
    //   y:
    //     A System.Data.SqlTypes.SqlBinary structure.
    //
    // Returns:
    //     A System.Data.SqlTypes.SqlBoolean that is System.Data.SqlTypes.SqlBoolean.True
    //     if the first instance is greater than the second instance. Otherwise System.Data.SqlTypes.SqlBoolean.False.
    //     If either instance of System.Data.SqlTypes.SqlBinary is null, the System.Data.SqlTypes.SqlBoolean.Value
    //     of the System.Data.SqlTypes.SqlBoolean will be System.Data.SqlTypes.SqlBoolean.Null.
    //public static SqlBoolean GreaterThan(SqlBinary x, SqlBinary y);
    //
    // Summary:
    //     Compares two System.Data.SqlTypes.SqlBinary structures to determine whether
    //     the first is greater than or equal to the second.
    //
    // Parameters:
    //   x:
    //     A System.Data.SqlTypes.SqlBinary structure.
    //
    //   y:
    //     A System.Data.SqlTypes.SqlBinary structure.
    //
    // Returns:
    //     A System.Data.SqlTypes.SqlBoolean that is System.Data.SqlTypes.SqlBoolean.True
    //     if the first instance is greater than or equal to the second instance. Otherwise
    //     System.Data.SqlTypes.SqlBoolean.False. If either instance of System.Data.SqlTypes.SqlBinary
    //     is null, the System.Data.SqlTypes.SqlBoolean.Value of the System.Data.SqlTypes.SqlBoolean
    //     will be System.Data.SqlTypes.SqlBoolean.Null.
    //public static SqlBoolean GreaterThanOrEqual(SqlBinary x, SqlBinary y);
    //
    // Summary:
    //     Compares two System.Data.SqlTypes.SqlBinary structures to determine whether
    //     the first is less than the second.
    //
    // Parameters:
    //   x:
    //     A System.Data.SqlTypes.SqlBinary structure.
    //
    //   y:
    //     A System.Data.SqlTypes.SqlBinary structure.
    //
    // Returns:
    //     A System.Data.SqlTypes.SqlBoolean that is System.Data.SqlTypes.SqlBoolean.True
    //     if the first instance is less than the second instance. Otherwise System.Data.SqlTypes.SqlBoolean.False.
    //     If either instance of System.Data.SqlTypes.SqlBinary is null, the System.Data.SqlTypes.SqlBoolean.Value
    //     of the System.Data.SqlTypes.SqlBoolean will be System.Data.SqlTypes.SqlBoolean.Null.
    //public static SqlBoolean LessThan(SqlBinary x, SqlBinary y);
    //
    // Summary:
    //     Compares two System.Data.SqlTypes.SqlBinary structures to determine whether
    //     the first is less than or equal to the second.
    //
    // Parameters:
    //   x:
    //     A System.Data.SqlTypes.SqlBinary structure.
    //
    //   y:
    //     A System.Data.SqlTypes.SqlBinary structure.
    //
    // Returns:
    //     A System.Data.SqlTypes.SqlBoolean that is System.Data.SqlTypes.SqlBoolean.True
    //     if the first instance is less than or equal to the second instance. Otherwise
    //     System.Data.SqlTypes.SqlBoolean.False. If either instance of System.Data.SqlTypes.SqlBinary
    //     is null, the System.Data.SqlTypes.SqlBoolean.Value of the System.Data.SqlTypes.SqlBoolean
    //     will be System.Data.SqlTypes.SqlBoolean.Null.
    //public static SqlBoolean LessThanOrEqual(SqlBinary x, SqlBinary y);
    //
    // Summary:
    //     Compares two System.Data.SqlTypes.SqlBinary structures to determine whether
    //     they are not equal.
    //
    // Parameters:
    //   x:
    //     A System.Data.SqlTypes.SqlBinary structure.
    //
    //   y:
    //     A System.Data.SqlTypes.SqlBinary structure.
    //
    // Returns:
    //     A System.Data.SqlTypes.SqlBoolean that is System.Data.SqlTypes.SqlBoolean.True
    //     if the two instances are not equal or System.Data.SqlTypes.SqlBoolean.False
    //     if the two instances are equal. If either instance of System.Data.SqlTypes.SqlBinary
    //     is null, the System.Data.SqlTypes.SqlBoolean.Value of the System.Data.SqlTypes.SqlBoolean
    //     will be System.Data.SqlTypes.SqlBoolean.Null.
    //public static SqlBoolean NotEquals(SqlBinary x, SqlBinary y);
    //
    // Summary:
    //     Converts this instance of System.Data.SqlTypes.SqlBinary to System.Data.SqlTypes.SqlGuid.
    //
    // Returns:
    //     A System.Data.SqlTypes.SqlGuid structure.
    //public SqlGuid ToSqlGuid();
    //

  }
}
