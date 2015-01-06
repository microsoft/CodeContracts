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


namespace System.Data.SqlTypes
{
  // Summary:
  //     Represents an 8-bit unsigned integer, in the range of 0 through 255, to be
  //     stored in or retrieved from a database.
  //[Serializable]
  //[XmlSchemaProvider("GetXsdType")]
  public struct SqlByte //: INullable, IComparable, IXmlSerializable
  {
    //// Summary:
    ////     A constant representing the largest possible value of a System.Data.SqlTypes.SqlByte.
    //public static readonly SqlByte MaxValue;
    ////
    //// Summary:
    ////     A constant representing the smallest possible value of a System.Data.SqlTypes.SqlByte.
    //public static readonly SqlByte MinValue;
    ////
    //// Summary:
    ////     Represents a null value that can be assigned to the System.Data.SqlTypes.SqlByte.Value
    ////     property of an instance of the System.Data.SqlTypes.SqlByte structure.
    //public static readonly SqlByte Null;
    ////
    //// Summary:
    ////     Represents a zero value that can be assigned to the System.Data.SqlTypes.SqlByte.Value
    ////     property of an instance of the System.Data.SqlTypes.SqlByte structure.
    //public static readonly SqlByte Zero;

    ////
    //// Summary:
    ////     Initializes a new instance of the System.Data.SqlTypes.SqlByte structure
    ////     using the specified byte value.
    ////
    //// Parameters:
    ////   value:
    ////     A byte value to be stored in the System.Data.SqlTypes.SqlByte.Value property
    ////     of the new System.Data.SqlTypes.SqlByte structure.
    //public SqlByte(byte value);

    //// Summary:
    ////     Subtracts the second System.Data.SqlTypes.SqlByte operand from the first.
    ////
    //// Parameters:
    ////   x:
    ////     A System.Data.SqlTypes.SqlByte structure.
    ////
    ////   y:
    ////     A System.Data.SqlTypes.SqlByte structure.
    ////
    //// Returns:
    ////     The results of subtracting the second System.Data.SqlTypes.SqlByte operand
    ////     from the first.
    //public static SqlByte operator -(SqlByte x, SqlByte y);
    ////
    //// Summary:
    ////     Compares two instances of System.Data.SqlTypes.SqlByte to determine whether
    ////     they are not equal.
    ////
    //// Parameters:
    ////   x:
    ////     A System.Data.SqlTypes.SqlByte structure.
    ////
    ////   y:
    ////     A System.Data.SqlTypes.SqlByte structure.
    ////
    //// Returns:
    ////     A System.Data.SqlTypes.SqlBoolean that is System.Data.SqlTypes.SqlBoolean.True
    ////     if the two instances are not equal or System.Data.SqlTypes.SqlBoolean.False
    ////     if the two instances are equal. If either instance of System.Data.SqlTypes.SqlByte
    ////     is null, the System.Data.SqlTypes.SqlBoolean.Value of the System.Data.SqlTypes.SqlBoolean
    ////     will be System.Data.SqlTypes.SqlBoolean.Null.
    //public static SqlBoolean operator !=(SqlByte x, SqlByte y);
    ////
    //// Summary:
    ////     Computes the remainder after dividing its first System.Data.SqlTypes.SqlByte
    ////     operand by its second.
    ////
    //// Parameters:
    ////   x:
    ////     A System.Data.SqlTypes.SqlByte structure.
    ////
    ////   y:
    ////     A System.Data.SqlTypes.SqlByte structure.
    ////
    //// Returns:
    ////     A System.Data.SqlTypes.SqlByte structure whose System.Data.SqlTypes.SqlByte.Value
    ////     contains the remainder.
    //public static SqlByte operator %(SqlByte x, SqlByte y);
    ////
    //// Summary:
    ////     Computes the bitwise AND of its System.Data.SqlTypes.SqlByte operands.
    ////
    //// Parameters:
    ////   x:
    ////     A System.Data.SqlTypes.SqlByte structure.
    ////
    ////   y:
    ////     A System.Data.SqlTypes.SqlByte structure.
    ////
    //// Returns:
    ////     The results of the bitwise AND operation.
    //public static SqlByte operator &(SqlByte x, SqlByte y);
    ////
    //// Summary:
    ////     Computes the product of the two System.Data.SqlTypes.SqlByte operands.
    ////
    //// Parameters:
    ////   x:
    ////     A System.Data.SqlTypes.SqlByte structure.
    ////
    ////   y:
    ////     A System.Data.SqlTypes.SqlByte structure.
    ////
    //// Returns:
    ////     A new System.Data.SqlTypes.SqlByte structure whose System.Data.SqlTypes.SqlByte.Value
    ////     property contains the product of the multiplication.
    //public static SqlByte operator *(SqlByte x, SqlByte y);
    ////
    //// Summary:
    ////     Divides its first System.Data.SqlTypes.SqlByte operand by its second.
    ////
    //// Parameters:
    ////   x:
    ////     A System.Data.SqlTypes.SqlByte structure.
    ////
    ////   y:
    ////     A System.Data.SqlTypes.SqlByte structure.
    ////
    //// Returns:
    ////     A new System.Data.SqlTypes.SqlByte structure whose System.Data.SqlTypes.SqlByte.Value
    ////     property contains the results of the division.
    //public static SqlByte operator /(SqlByte x, SqlByte y);
    ////
    //// Summary:
    ////     Performs a bitwise exclusive-OR operation on the supplied parameters.
    ////
    //// Parameters:
    ////   x:
    ////     A System.Data.SqlTypes.SqlByte structure.
    ////
    ////   y:
    ////     A System.Data.SqlTypes.SqlByte structure.
    ////
    //// Returns:
    ////     The results of the bitwise XOR operation.
    //public static SqlByte operator ^(SqlByte x, SqlByte y);
    ////
    //// Summary:
    ////     Computes the bitwise OR of its two System.Data.SqlTypes.SqlByte operands.
    ////
    //// Parameters:
    ////   x:
    ////     A System.Data.SqlTypes.SqlByte structure.
    ////
    ////   y:
    ////     A System.Data.SqlTypes.SqlByte structure.
    ////
    //// Returns:
    ////     The results of the bitwise OR operation.
    //public static SqlByte operator |(SqlByte x, SqlByte y);
    ////
    //// Summary:
    ////     The ones complement operator performs a bitwise one's complement operation
    ////     on its System.Data.SqlTypes.SqlByte operand.
    ////
    //// Parameters:
    ////   x:
    ////     A System.Data.SqlTypes.SqlByte structure.
    ////
    //// Returns:
    ////     A System.Data.SqlTypes.SqlByte structure whose System.Data.SqlTypes.SqlByte.Value
    ////     property contains the ones complement of the System.Data.SqlTypes.SqlByte
    ////     parameter.
    //public static SqlByte operator ~(SqlByte x);
    ////
    //// Summary:
    ////     Computes the sum of the two specified System.Data.SqlTypes.SqlByte structures.
    ////
    //// Parameters:
    ////   x:
    ////     A System.Data.SqlTypes.SqlByte structure.
    ////
    ////   y:
    ////     A System.Data.SqlTypes.SqlByte structure.
    ////
    //// Returns:
    ////     A System.Data.SqlTypes.SqlByte whose System.Data.SqlTypes.SqlByte.Value property
    ////     contains the sum of the two operands.
    //public static SqlByte operator +(SqlByte x, SqlByte y);
    ////
    //// Summary:
    ////     Compares two instances of System.Data.SqlTypes.SqlByte to determine whether
    ////     the first is less than the second.
    ////
    //// Parameters:
    ////   x:
    ////     A System.Data.SqlTypes.SqlByte structure.
    ////
    ////   y:
    ////     A System.Data.SqlTypes.SqlByte structure.
    ////
    //// Returns:
    ////     A System.Data.SqlTypes.SqlBoolean that is System.Data.SqlTypes.SqlBoolean.True
    ////     if the first instance is less than the second instance. Otherwise, System.Data.SqlTypes.SqlBoolean.False.
    ////     If either instance of System.Data.SqlTypes.SqlByte is null, the System.Data.SqlTypes.SqlBoolean.Value
    ////     of the System.Data.SqlTypes.SqlBoolean will be System.Data.SqlTypes.SqlBoolean.Null.
    //public static SqlBoolean operator <(SqlByte x, SqlByte y);
    ////
    //// Summary:
    ////     Compares two instances of System.Data.SqlTypes.SqlByte to determine whether
    ////     the first is less than or equal to the second.
    ////
    //// Parameters:
    ////   x:
    ////     A System.Data.SqlTypes.SqlByte structure.
    ////
    ////   y:
    ////     A System.Data.SqlTypes.SqlByte structure.
    ////
    //// Returns:
    ////     A System.Data.SqlTypes.SqlBoolean that is System.Data.SqlTypes.SqlBoolean.True
    ////     if the first instance is less than or equal to the second instance. Otherwise,
    ////     System.Data.SqlTypes.SqlBoolean.False. If either instance of System.Data.SqlTypes.SqlByte
    ////     is null, the System.Data.SqlTypes.SqlBoolean.Value of the System.Data.SqlTypes.SqlBoolean
    ////     will be System.Data.SqlTypes.SqlBoolean.Null.
    //public static SqlBoolean operator <=(SqlByte x, SqlByte y);
    ////
    //// Summary:
    ////     Performs a logical comparison of two System.Data.SqlTypes.SqlByte structures
    ////     to determine whether they are equal.
    ////
    //// Parameters:
    ////   x:
    ////     A System.Data.SqlTypes.SqlByte structure.
    ////
    ////   y:
    ////     A System.Data.SqlTypes.SqlByte structure.
    ////
    //// Returns:
    ////     A System.Data.SqlTypes.SqlBoolean that is System.Data.SqlTypes.SqlBoolean.True
    ////     if the two instances are equal or System.Data.SqlTypes.SqlBoolean.False if
    ////     the two instances are not equal. If either instance of System.Data.SqlTypes.SqlByte
    ////     is null, the System.Data.SqlTypes.SqlBoolean.Value of the System.Data.SqlTypes.SqlBoolean
    ////     will be System.Data.SqlTypes.SqlBoolean.Null.
    //public static SqlBoolean operator ==(SqlByte x, SqlByte y);
    ////
    //// Summary:
    ////     Compares two instances of System.Data.SqlTypes.SqlByte to determine whether
    ////     the first is greater than the second.
    ////
    //// Parameters:
    ////   x:
    ////     A System.Data.SqlTypes.SqlByte structure.
    ////
    ////   y:
    ////     A System.Data.SqlTypes.SqlByte structure.
    ////
    //// Returns:
    ////     A System.Data.SqlTypes.SqlBoolean that is System.Data.SqlTypes.SqlBoolean.True
    ////     if the first instance is greater than the second instance. Otherwise, System.Data.SqlTypes.SqlBoolean.False.
    ////     If either instance of System.Data.SqlTypes.SqlByte is null, the System.Data.SqlTypes.SqlBoolean.Value
    ////     of the System.Data.SqlTypes.SqlBoolean will be System.Data.SqlTypes.SqlBoolean.Null.
    //public static SqlBoolean operator >(SqlByte x, SqlByte y);
    ////
    //// Summary:
    ////     Compares two instances of System.Data.SqlTypes.SqlByte to determine whether
    ////     the first is greater than or equal to the second.
    ////
    //// Parameters:
    ////   x:
    ////     A System.Data.SqlTypes.SqlByte structure.
    ////
    ////   y:
    ////     A System.Data.SqlTypes.SqlByte structure.
    ////
    //// Returns:
    ////     A System.Data.SqlTypes.SqlBoolean that is System.Data.SqlTypes.SqlBoolean.True
    ////     if the first instance is greater than or equal to the second instance. Otherwise,
    ////     System.Data.SqlTypes.SqlBoolean.False. If either instance of System.Data.SqlTypes.SqlByte
    ////     is null, the System.Data.SqlTypes.SqlBoolean.Value of the SqlBoolean will
    ////     be System.Data.SqlTypes.SqlBoolean.Null.
    //public static SqlBoolean operator >=(SqlByte x, SqlByte y);
    ////
    //// Summary:
    ////     Converts the System.Data.SqlTypes.SqlBoolean parameter to a System.Data.SqlTypes.SqlByte.
    ////
    //// Parameters:
    ////   x:
    ////     The System.Data.SqlTypes.SqlBoolean parameter to be converted to a System.Data.SqlTypes.SqlByte.
    ////
    //// Returns:
    ////     A System.Data.SqlTypes.SqlByte whose System.Data.SqlTypes.SqlByte.Value property
    ////     equals the System.Data.SqlTypes.SqlBoolean.ByteValue of the supplied System.Data.SqlTypes.SqlBoolean
    ////     parameter.
    //public static explicit operator SqlByte(SqlBoolean x);
    ////
    //// Summary:
    ////     Converts the supplied System.Data.SqlTypes.SqlByte structure to a byte.
    ////
    //// Parameters:
    ////   x:
    ////     The System.Data.SqlTypes.SqlByte structure to be converted to a byte.
    ////
    //// Returns:
    ////     A byte whose value equals the System.Data.SqlTypes.SqlByte.Value property
    ////     of the System.Data.SqlTypes.SqlByte parameter.
    //public static explicit operator byte(SqlByte x);
    ////
    //// Summary:
    ////     Converts the supplied System.Data.SqlTypes.SqlDecimal to System.Data.SqlTypes.SqlByte.
    ////
    //// Parameters:
    ////   x:
    ////     A System.Data.SqlTypes.SqlDecimal structure.
    ////
    //// Returns:
    ////     A System.Data.SqlTypes.SqlByte structure whose System.Data.SqlTypes.SqlByte.Value
    ////     property is equal to the System.Data.SqlTypes.SqlDecimal.Value of the System.Data.SqlTypes.SqlDecimal
    ////     parameter.
    //public static explicit operator SqlByte(SqlDecimal x);
    ////
    //// Summary:
    ////     Converts the supplied System.Data.SqlTypes.SqlDouble to System.Data.SqlTypes.SqlByte.
    ////
    //// Parameters:
    ////   x:
    ////     A System.Data.SqlTypes.SqlDouble structure.
    ////
    //// Returns:
    ////     A System.Data.SqlTypes.SqlByte structure whose System.Data.SqlTypes.SqlByte.Value
    ////     property is equal to the System.Data.SqlTypes.SqlDouble.Value of the System.Data.SqlTypes.SqlDouble
    ////     parameter.
    //public static explicit operator SqlByte(SqlDouble x);
    ////
    //// Summary:
    ////     Converts the System.Data.SqlTypes.SqlInt16 parameter to a System.Data.SqlTypes.SqlByte.
    ////
    //// Parameters:
    ////   x:
    ////     A System.Data.SqlTypes.SqlInt16 structure.
    ////
    //// Returns:
    ////     A System.Data.SqlTypes.SqlByte structure whose System.Data.SqlTypes.SqlByte.Value
    ////     property is equal to the System.Data.SqlTypes.SqlInt16.Value of the System.Data.SqlTypes.SqlInt16
    ////     parameter.
    //public static explicit operator SqlByte(SqlInt16 x);
    ////
    //// Summary:
    ////     Converts the supplied System.Data.SqlTypes.SqlInt32 to System.Data.SqlTypes.SqlByte.
    ////
    //// Parameters:
    ////   x:
    ////     A System.Data.SqlTypes.SqlInt32 structure.
    ////
    //// Returns:
    ////     A System.Data.SqlTypes.SqlByte structure whose System.Data.SqlTypes.SqlByte.Value
    ////     property is equal to the System.Data.SqlTypes.SqlInt32.Value of the System.Data.SqlTypes.SqlInt32
    ////     parameter.
    //public static explicit operator SqlByte(SqlInt32 x);
    ////
    //// Summary:
    ////     Converts the supplied System.Data.SqlTypes.SqlInt64 to System.Data.SqlTypes.SqlByte.
    ////
    //// Parameters:
    ////   x:
    ////     A System.Data.SqlTypes.SqlInt64 structure.
    ////
    //// Returns:
    ////     A System.Data.SqlTypes.SqlByte structure whose System.Data.SqlTypes.SqlByte.Value
    ////     property is equal to the System.Data.SqlTypes.SqlInt64.Value of the SqlInt64
    ////     parameter.
    //public static explicit operator SqlByte(SqlInt64 x);
    ////
    //// Summary:
    ////     Converts the System.Data.SqlTypes.SqlMoney parameter to a System.Data.SqlTypes.SqlByte.
    ////
    //// Parameters:
    ////   x:
    ////     A SqlMoney structure.
    ////
    //// Returns:
    ////     A System.Data.SqlTypes.SqlByte structure whose System.Data.SqlTypes.SqlByte.Value
    ////     property is equal to the System.Data.SqlTypes.SqlMoney.Value of the System.Data.SqlTypes.SqlMoney
    ////     parameter.
    //public static explicit operator SqlByte(SqlMoney x);
    ////
    //// Summary:
    ////     Converts the supplied System.Data.SqlTypes.SqlSingle structure to System.Data.SqlTypes.SqlByte.
    ////
    //// Parameters:
    ////   x:
    ////     A System.Data.SqlTypes.SqlSingle structure.
    ////
    //// Returns:
    ////     A System.Data.SqlTypes.SqlByte structure whose System.Data.SqlTypes.SqlByte.Value
    ////     property is equal to the System.Data.SqlTypes.SqlSingle.Value of the System.Data.SqlTypes.SqlSingle
    ////     parameter.
    //public static explicit operator SqlByte(SqlSingle x);
    ////
    //// Summary:
    ////     Converts the supplied System.Data.SqlTypes.SqlString to System.Data.SqlTypes.SqlByte.
    ////
    //// Parameters:
    ////   x:
    ////     An instance of the SqlString class.
    ////
    //// Returns:
    ////     A System.Data.SqlTypes.SqlByte structure whose System.Data.SqlTypes.SqlByte.Value
    ////     property is equal to the numeric value represented by the System.Data.SqlTypes.SqlString.
    //public static explicit operator SqlByte(SqlString x);
    ////
    //// Summary:
    ////     Converts the supplied byte value to a System.Data.SqlTypes.SqlByte.
    ////
    //// Parameters:
    ////   x:
    ////     A byte value to be converted to System.Data.SqlTypes.SqlByte.
    ////
    //// Returns:
    ////     A System.Data.SqlTypes.SqlByte structure whose System.Data.SqlTypes.SqlByte.Value
    ////     property is equal to the supplied parameter.
    //public static implicit operator SqlByte(byte x);

    //// Summary:
    ////     Indicates whether System.Data.SqlTypes.SqlByte.Value is null.
    ////
    //// Returns:
    ////     true if Value is null. Otherwise, false.
    //public bool IsNull { get; }
    ////
    //// Summary:
    ////     Gets the value of the System.Data.SqlTypes.SqlByte structure. This property
    ////     is read-only
    ////
    //// Returns:
    ////     The value of the System.Data.SqlTypes.SqlByte structure.
    //public byte Value { get; }

    //// Summary:
    ////     Computes the sum of the two specified System.Data.SqlTypes.SqlByte structures.
    ////
    //// Parameters:
    ////   x:
    ////     A System.Data.SqlTypes.SqlByte structure.
    ////
    ////   y:
    ////     A System.Data.SqlTypes.SqlByte structure.
    ////
    //// Returns:
    ////     A System.Data.SqlTypes.SqlByte structure whose Value property contains the
    ////     results of the addition.
    //public static SqlByte Add(SqlByte x, SqlByte y);
    ////
    //// Summary:
    ////     Computes the bitwise AND of its System.Data.SqlTypes.SqlByte operands.
    ////
    //// Parameters:
    ////   x:
    ////     A System.Data.SqlTypes.SqlByte structure.
    ////
    ////   y:
    ////     A System.Data.SqlTypes.SqlByte structure.
    ////
    //// Returns:
    ////     The results of the bitwise AND operation.
    //public static SqlByte BitwiseAnd(SqlByte x, SqlByte y);
    ////
    //// Summary:
    ////     Computes the bitwise OR of its two System.Data.SqlTypes.SqlByte operands.
    ////
    //// Parameters:
    ////   x:
    ////     A System.Data.SqlTypes.SqlByte structure.
    ////
    ////   y:
    ////     A System.Data.SqlTypes.SqlByte structure.
    ////
    //// Returns:
    ////     The results of the bitwise OR operation.
    //public static SqlByte BitwiseOr(SqlByte x, SqlByte y);
    ////
    //// Summary:
    ////     Compares this instance to the supplied System.Object and returns an indication
    ////     of their relative values.
    ////
    //// Parameters:
    ////   value:
    ////     The System.Object to be compared.
    ////
    //// Returns:
    ////     A signed number that indicates the relative values of the instance and the
    ////     object.Return Value Condition Less than zero This instance is less than the
    ////     object. Zero This instance is the same as the object. Greater than zero This
    ////     instance is greater than the object -or- The object is a null reference (Nothing
    ////     in Visual Basic)
    //public int CompareTo(object value);
    ////
    //// Summary:
    ////     Compares this instance to the supplied System.Data.SqlTypes.SqlByte object
    ////     and returns an indication of their relative values.
    ////
    //// Parameters:
    ////   value:
    ////     The System.Data.SqlTypes.SqlByte object to be compared.
    ////
    //// Returns:
    ////     A signed number that indicates the relative values of the instance and the
    ////     object.Return Value Condition Less than zero This instance is less than the
    ////     object. Zero This instance is the same as the object. Greater than zero This
    ////     instance is greater than the object -or- The object is a null reference (Nothing
    ////     in Visual Basic)
    //public int CompareTo(SqlByte value);
    ////
    //// Summary:
    ////     Divides its first System.Data.SqlTypes.SqlByte operand by its second.
    ////
    //// Parameters:
    ////   x:
    ////     A System.Data.SqlTypes.SqlByte structure.
    ////
    ////   y:
    ////     A System.Data.SqlTypes.SqlByte structure.
    ////
    //// Returns:
    ////     A new System.Data.SqlTypes.SqlByte structure whose System.Data.SqlTypes.SqlByte.Value
    ////     property contains the results of the division.
    //public static SqlByte Divide(SqlByte x, SqlByte y);
    ////
    //// Summary:
    ////     Compares the supplied System.Object parameter to the System.Data.SqlTypes.SqlByte.Value
    ////     property of the System.Data.SqlTypes.SqlByte object.
    ////
    //// Parameters:
    ////   value:
    ////     The System.Object to be compared.
    ////
    //// Returns:
    ////     true if object is an instance of System.Data.SqlTypes.SqlByte and the two
    ////     are equal; otherwise false.
    //public override bool Equals(object value);
    ////
    //// Summary:
    ////     Performs a logical comparison of two System.Data.SqlTypes.SqlByte structures
    ////     to determine whether they are equal.
    ////
    //// Parameters:
    ////   x:
    ////     A System.Data.SqlTypes.SqlByte structure.
    ////
    ////   y:
    ////     A System.Data.SqlTypes.SqlByte structure.
    ////
    //// Returns:
    ////     true if the two values are equal. Otherwise, false. If either instance is
    ////     null, then the SqlByte will be null.
    //public static SqlBoolean Equals(SqlByte x, SqlByte y);
    ////
    //// Summary:
    ////     Returns the hash code for this instance.
    ////
    //// Returns:
    ////     A 32-bit signed integer hash code.
    //public override int GetHashCode();
    ////
    //// Summary:
    ////     Returns the XML Schema definition language (XSD) of the specified System.Xml.Schema.XmlSchemaSet.
    ////
    //// Parameters:
    ////   schemaSet:
    ////     A System.Xml.Schema.XmlSchemaSet.
    ////
    //// Returns:
    ////     A string value that indicates the XSD of the specified System.Xml.Schema.XmlSchemaSet.
    //public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet);
    ////
    //// Summary:
    ////     Compares two instances of System.Data.SqlTypes.SqlByte to determine whether
    ////     the first is greater than the second.
    ////
    //// Parameters:
    ////   x:
    ////     A System.Data.SqlTypes.SqlByte structure.
    ////
    ////   y:
    ////     A System.Data.SqlTypes.SqlByte structure.
    ////
    //// Returns:
    ////     A System.Data.SqlTypes.SqlBoolean that is System.Data.SqlTypes.SqlBoolean.True
    ////     if the first instance is greater than the second instance. Otherwise, System.Data.SqlTypes.SqlBoolean.False.
    ////     If either instance of System.Data.SqlTypes.SqlByte is null, the System.Data.SqlTypes.SqlBoolean.Value
    ////     of the System.Data.SqlTypes.SqlBoolean will be System.Data.SqlTypes.SqlBoolean.Null.
    //public static SqlBoolean GreaterThan(SqlByte x, SqlByte y);
    ////
    //// Summary:
    ////     Compares two System.Data.SqlTypes.SqlByte structures to determine whether
    ////     the first is greater than or equal to the second.
    ////
    //// Parameters:
    ////   x:
    ////     A System.Data.SqlTypes.SqlByte structure.
    ////
    ////   y:
    ////     A System.Data.SqlTypes.SqlByte structure.
    ////
    //// Returns:
    ////     A System.Data.SqlTypes.SqlBoolean that is System.Data.SqlTypes.SqlBoolean.True
    ////     if the first instance is greater than or equal to the second instance. Otherwise,
    ////     System.Data.SqlTypes.SqlBoolean.False. If either instance of System.Data.SqlTypes.SqlByte
    ////     is null, the System.Data.SqlTypes.SqlBoolean.Value of the System.Data.SqlTypes.SqlBoolean
    ////     will be System.Data.SqlTypes.SqlBoolean.Null.
    //public static SqlBoolean GreaterThanOrEqual(SqlByte x, SqlByte y);
    ////
    //// Summary:
    ////     Compares two instances of System.Data.SqlTypes.SqlByte to determine whether
    ////     the first is less than the second.
    ////
    //// Parameters:
    ////   x:
    ////     A System.Data.SqlTypes.SqlByte structure.
    ////
    ////   y:
    ////     A System.Data.SqlTypes.SqlByte structure.
    ////
    //// Returns:
    ////     A System.Data.SqlTypes.SqlBoolean that is System.Data.SqlTypes.SqlBoolean.True
    ////     if the first instance is less than the second instance. Otherwise, System.Data.SqlTypes.SqlBoolean.False.
    ////     If either instance of System.Data.SqlTypes.SqlByte is null, the System.Data.SqlTypes.SqlBoolean.Value
    ////     of the System.Data.SqlTypes.SqlBoolean will be System.Data.SqlTypes.SqlBoolean.Null.
    //public static SqlBoolean LessThan(SqlByte x, SqlByte y);
    ////
    //// Summary:
    ////     Compares two instances of System.Data.SqlTypes.SqlByte to determine whether
    ////     the first is less than or equal to the second.
    ////
    //// Parameters:
    ////   x:
    ////     A System.Data.SqlTypes.SqlByte structure.
    ////
    ////   y:
    ////     A System.Data.SqlTypes.SqlByte structure.
    ////
    //// Returns:
    ////     A System.Data.SqlTypes.SqlBoolean that is System.Data.SqlTypes.SqlBoolean.True
    ////     if the first instance is less than or equal to the second instance. Otherwise,
    ////     System.Data.SqlTypes.SqlBoolean.False. If either instance of System.Data.SqlTypes.SqlByte
    ////     is null, the System.Data.SqlTypes.SqlBoolean.Value of the System.Data.SqlTypes.SqlBoolean
    ////     will be System.Data.SqlTypes.SqlBoolean.Null.
    //public static SqlBoolean LessThanOrEqual(SqlByte x, SqlByte y);
    ////
    //// Summary:
    ////     Computes the remainder after dividing its first System.Data.SqlTypes.SqlByte
    ////     operand by its second.
    ////
    //// Parameters:
    ////   x:
    ////     A System.Data.SqlTypes.SqlByte structure.
    ////
    ////   y:
    ////     A System.Data.SqlTypes.SqlByte structure.
    ////
    //// Returns:
    ////     A System.Data.SqlTypes.SqlByte structure whose System.Data.SqlTypes.SqlByte.Value
    ////     contains the remainder.
    //public static SqlByte Mod(SqlByte x, SqlByte y);
    ////
    //// Summary:
    ////     Divides two System.Data.SqlTypes.SqlByte values and returns the remainder.
    ////
    //// Parameters:
    ////   x:
    ////     A System.Data.SqlTypes.SqlByte.
    ////
    ////   y:
    ////     A System.Data.SqlTypes.SqlByte.
    ////
    //// Returns:
    ////     The remainder left after division is performed on x and y.
    //public static SqlByte Modulus(SqlByte x, SqlByte y);
    ////
    //// Summary:
    ////     Computes the product of the two System.Data.SqlTypes.SqlByte operands.
    ////
    //// Parameters:
    ////   x:
    ////     A System.Data.SqlTypes.SqlByte structure.
    ////
    ////   y:
    ////     A System.Data.SqlTypes.SqlByte structure.
    ////
    //// Returns:
    ////     A new System.Data.SqlTypes.SqlByte structure whose System.Data.SqlTypes.SqlByte.Value
    ////     property contains the product of the multiplication.
    //public static SqlByte Multiply(SqlByte x, SqlByte y);
    ////
    //// Summary:
    ////     Compares two instances of System.Data.SqlTypes.SqlByte to determine whether
    ////     they are not equal.
    ////
    //// Parameters:
    ////   x:
    ////     A System.Data.SqlTypes.SqlByte structure.
    ////
    ////   y:
    ////     A System.Data.SqlTypes.SqlByte structure.
    ////
    //// Returns:
    ////     A System.Data.SqlTypes.SqlBoolean that is System.Data.SqlTypes.SqlBoolean.True
    ////     if the two instances are not equal or System.Data.SqlTypes.SqlBoolean.False
    ////     if the two instances are equal. If either instance of System.Data.SqlTypes.SqlByte
    ////     is null, the System.Data.SqlTypes.SqlBoolean.Value of the System.Data.SqlTypes.SqlBoolean
    ////     will be System.Data.SqlTypes.SqlBoolean.Null.
    //public static SqlBoolean NotEquals(SqlByte x, SqlByte y);
    ////
    //// Summary:
    ////     The ones complement operator performs a bitwise one's complement operation
    ////     on its System.Data.SqlTypes.SqlByte operand.
    ////
    //// Parameters:
    ////   x:
    ////     A System.Data.SqlTypes.SqlByte structure.
    ////
    //// Returns:
    ////     A System.Data.SqlTypes.SqlByte structure whose System.Data.SqlTypes.SqlByte.Value
    ////     property contains the ones complement of the System.Data.SqlTypes.SqlByte
    ////     parameter.
    //public static SqlByte OnesComplement(SqlByte x);
    ////
    //// Summary:
    ////     Converts the System.String representation of a number to its 8-bit unsigned
    ////     integer equivalent.
    ////
    //// Parameters:
    ////   s:
    ////     The String to be parsed.
    ////
    //// Returns:
    ////     A System.Data.SqlTypes.SqlByte structure that contains the 8-bit number represented
    ////     by the String parameter.
    //public static SqlByte Parse(string s);
    ////
    //// Summary:
    ////     Subtracts the second System.Data.SqlTypes.SqlByte operand from the first.
    ////
    //// Parameters:
    ////   x:
    ////     A System.Data.SqlTypes.SqlByte structure.
    ////
    ////   y:
    ////     A System.Data.SqlTypes.SqlByte structure.
    ////
    //// Returns:
    ////     The results of subtracting the second System.Data.SqlTypes.SqlByte operand
    ////     from the first.
    //public static SqlByte Subtract(SqlByte x, SqlByte y);
    ////
    //// Summary:
    ////     Converts this System.Data.SqlTypes.SqlByte structure to System.Data.SqlTypes.SqlBoolean.
    ////
    //// Returns:
    ////     true if the System.Data.SqlTypes.SqlByte.Value is non-zero; false if zero;
    ////     otherwise Null.
    //public SqlBoolean ToSqlBoolean();
    ////
    //// Summary:
    ////     Converts this System.Data.SqlTypes.SqlByte structure to System.Data.SqlTypes.SqlDecimal.
    ////
    //// Returns:
    ////     A SqlDecimal structure whose System.Data.SqlTypes.SqlDecimal.Value equals
    ////     the System.Data.SqlTypes.SqlByte.Value of this System.Data.SqlTypes.SqlByte
    ////     structure.
    //public SqlDecimal ToSqlDecimal();
    ////
    //// Summary:
    ////     Converts this System.Data.SqlTypes.SqlByte structure to System.Data.SqlTypes.SqlDouble.
    ////
    //// Returns:
    ////     A SqlDouble structure with the same value as this System.Data.SqlTypes.SqlByte.
    //public SqlDouble ToSqlDouble();
    ////
    //// Summary:
    ////     Converts this System.Data.SqlTypes.SqlByte structure to System.Data.SqlTypes.SqlInt16.
    ////
    //// Returns:
    ////     A SqlInt16 structure with the same value as this System.Data.SqlTypes.SqlByte.
    //public SqlInt16 ToSqlInt16();
    ////
    //// Summary:
    ////     Converts this System.Data.SqlTypes.SqlByte to System.Data.SqlTypes.SqlInt32.
    ////
    //// Returns:
    ////     A SqlInt32 structure with the same value as this System.Data.SqlTypes.SqlByte.
    //public SqlInt32 ToSqlInt32();
    ////
    //// Summary:
    ////     Converts this System.Data.SqlTypes.SqlByte structure to System.Data.SqlTypes.SqlInt64.
    ////
    //// Returns:
    ////     A SqlInt64 structure who System.Data.SqlTypes.SqlInt64.Value equals the System.Data.SqlTypes.SqlByte.Value
    ////     of this System.Data.SqlTypes.SqlByte.
    //public SqlInt64 ToSqlInt64();
    ////
    //// Summary:
    ////     Converts this System.Data.SqlTypes.SqlByte structure to System.Data.SqlTypes.SqlMoney.
    ////
    //// Returns:
    ////     A SqlMoney structure whose System.Data.SqlTypes.SqlMoney.Value equals the
    ////     System.Data.SqlTypes.SqlByte.Value of this System.Data.SqlTypes.SqlByte structure.
    //public SqlMoney ToSqlMoney();
    ////
    //// Summary:
    ////     Converts this System.Data.SqlTypes.SqlByte structure to System.Data.SqlTypes.SqlSingle.
    ////
    //// Returns:
    ////     A SqlSingle structure that has the same System.Data.SqlTypes.SqlSingle.Value
    ////     as this System.Data.SqlTypes.SqlByte structure.
    //public SqlSingle ToSqlSingle();
    ////
    //// Summary:
    ////     Converts this instance of System.Data.SqlTypes.SqlByte to System.Data.SqlTypes.SqlString.
    ////
    //// Returns:
    ////     A SqlString that contains the string representation of the System.Data.SqlTypes.SqlByte
    ////     structure's System.Data.SqlTypes.SqlByte.Value.
    //public SqlString ToSqlString();
    ////
    //// Summary:
    ////     Converts this System.Data.SqlTypes.SqlByte structure to a System.String.
    ////
    //// Returns:
    ////     A string that contains the System.Data.SqlTypes.SqlByte.Value of the System.Data.SqlTypes.SqlByte.
    ////     If the Value is null, the String will be a null string.
    //public override string ToString();
    ////
    //// Summary:
    ////     Performs a bitwise exclusive-OR operation on the supplied parameters.
    ////
    //// Parameters:
    ////   x:
    ////     A System.Data.SqlTypes.SqlByte structure.
    ////
    ////   y:
    ////     A System.Data.SqlTypes.SqlByte structure.
    ////
    //// Returns:
    ////     The results of the XOR operation.
    //public static SqlByte Xor(SqlByte x, SqlByte y);
  }
}
