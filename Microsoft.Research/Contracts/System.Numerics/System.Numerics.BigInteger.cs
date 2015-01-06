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


using System.Globalization;
using System.Runtime;
using System.Diagnostics.Contracts;

namespace System.Numerics
{
#pragma warning disable 0626
  // Summary:
  //     Represents an arbitrarily large signed integer.
  //[Serializable]
  public struct BigInteger //: IFormattable, IComparable, IComparable<BigInteger>, IEquatable<BigInteger>
  {
    //
    // Summary:
    //     Initializes a new instance of the System.Numerics.BigInteger structure using
    //     the values in a byte array.
    //
    // Parameters:
    //   value:
    //     An array of byte values in little-endian order.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     value is null.
    //
    public BigInteger(byte[] value)
    {
      Contract.Requires(value != null);
    }
    //
    // Summary:
    //     Initializes a new instance of the System.Numerics.BigInteger structure using
    //     a System.Decimal value.
    //
    // Parameters:
    //   value:
    //     A decimal number.
    //public BigInteger(decimal value);
    //
    // Summary:
    //     Initializes a new instance of the System.Numerics.BigInteger structure using
    //     a double-precision floating-point value.
    //
    // Parameters:
    //   value:
    //     A double-precision floating-point value.
    //
    // Exceptions:
    //   System.OverflowException:
    //     The value of value is System.Double.NaN.-or-The value of value is System.Double.NegativeInfinity.-or-The
    //     value of value is System.Double.PositiveInfinity.
    public BigInteger(double value)
    {
      Contract.Requires(!Double.IsNaN(value));
      Contract.Requires(!Double.IsNegativeInfinity(value));
      Contract.Requires(!Double.IsPositiveInfinity(value));
    }
    //
    // Summary:
    //     Initializes a new instance of the System.Numerics.BigInteger structure using
    //     a single-precision floating-point value.
    //
    // Parameters:
    //   value:
    //     A single-precision floating-point value.
    //
    // Exceptions:
    //   System.OverflowException:
    //     The value of value is System.Single.NaN.-or-The value of value is System.Single.NegativeInfinity.-or-The
    //     value of value is System.Single.PositiveInfinity.
    public BigInteger(float value)
    {
      Contract.Requires(!Single.IsNaN(value));
      Contract.Requires(!Single.IsNegativeInfinity(value));
      Contract.Requires(!Single.IsPositiveInfinity(value));
    }

    //
    // Summary:
    //     Initializes a new instance of the System.Numerics.BigInteger structure using
    //     a 32-bit signed integer value.
    //
    // Parameters:
    //   value:
    //     A 32-bit signed integer.
    public BigInteger(int value)
    {
      Contract.Ensures(value != 0 || this.IsZero);
      Contract.Ensures(value != 1 || this.IsOne);
    }

    //
    // Summary:
    //     Initializes a new instance of the System.Numerics.BigInteger structure using
    //     a 64-bit signed integer value.
    //
    // Parameters:
    //   value:
    //     A 64-bit signed integer.
    public BigInteger(long value)
     {
      Contract.Ensures(value != 0 || this.IsZero);
      Contract.Ensures(value != 1 || this.IsOne);
    }

    //
    // Summary:
    //     Initializes a new instance of the System.Numerics.BigInteger structure using
    //     an unsigned 32-bit integer value.
    //
    // Parameters:
    //   value:
    //     An unsigned 32-bit integer value.
    //
    public BigInteger(uint value)
    {
      Contract.Ensures(value != 0 || this.IsZero);
      Contract.Ensures(value != 1 || this.IsOne);
    }

    //
    // Summary:
    //     Initializes a new instance of the System.Numerics.BigInteger structure with
    //     an unsigned 64-bit integer value.
    //
    // Parameters:
    //   value:
    //     An unsigned 64-bit integer.
    //
    public BigInteger(ulong value)
    {
      Contract.Ensures(value != 0 || this.IsZero);
      Contract.Ensures(value != 1 || this.IsOne);
    }

    // Summary:
    //     Negates a specified BigInteger value.
    //
    // Parameters:
    //   value:
    //     The value to negate.
    //
    // Returns:
    //     The result of the value parameter multiplied by negative one (-1).
    [Pure]
    public static BigInteger operator -(BigInteger value)
    {
      return default(BigInteger);
    }
    //
    // Summary:
    //     Subtracts a System.Numerics.BigInteger value from another System.Numerics.BigInteger
    //     value.
    //
    // Parameters:
    //   left:
    //     The value to subtract from (the minuend).
    //
    //   right:
    //     The value to subtract (the subtrahend).
    //
    // Returns:
    //     The result of subtracting right from left.
    [Pure]
    public static BigInteger operator -(BigInteger left, BigInteger right)
    {
      return default(BigInteger);
    }

    //
    // Summary:
    //     Decrements a System.Numerics.BigInteger value by 1.
    //
    // Parameters:
    //   value:
    //     The value to decrement.
    //
    // Returns:
    //     The value of the value parameter decremented by 1.
    [Pure]
    public static BigInteger operator --(BigInteger value)
    {
      return default(BigInteger);
    }

    //
    // Summary:
    //     Returns a value that indicates whether two System.Numerics.BigInteger objects
    //     have different values.
    //
    // Parameters:
    //   left:
    //     The first value to compare.
    //
    //   right:
    //     The second value to compare.
    //
    // Returns:
    //     true if left and right are not equal; otherwise, false.
    [Pure]
    public static bool operator !=(BigInteger left, BigInteger right)
    {
      return default(bool);
    }

    //
    // Summary:
    //     Returns a value that indicates whether a System.Numerics.BigInteger value
    //     and a 64-bit signed integer are not equal.
    //
    // Parameters:
    //   left:
    //     The first value to compare.
    //
    //   right:
    //     The second value to compare.
    //
    // Returns:
    //     true if left and right are not equal; otherwise, false.
    [Pure] public static bool operator !=(BigInteger left, long right)
    {
      return default(bool);
    }

    //
    // Summary:
    //     Returns a value that indicates whether a System.Numerics.BigInteger value
    //     and a 64-bit unsigned integer are not equal.
    //
    // Parameters:
    //   left:
    //     The first value to compare.
    //
    //   right:
    //     The second value to compare.
    //
    // Returns:
    //     true if left and right are not equal; otherwise, false.
//    
    [Pure] public static bool operator !=(BigInteger left, ulong right)
    {
      return default(bool);
    }
    //
    // Summary:
    //     Returns a value that indicates whether a 64-bit signed integer and a System.Numerics.BigInteger
    //     value are not equal.
    //
    // Parameters:
    //   left:
    //     The first value to compare.
    //
    //   right:
    //     The second value to compare.
    //
    // Returns:
    //     true if left and right are not equal; otherwise, false.
    [Pure] public static bool operator !=(long left, BigInteger right)
    {
      return default(bool);
    }

    //
    // Summary:
    //     Returns a value that indicates whether a 64-bit unsigned integer and a System.Numerics.BigInteger
    //     value are not equal.
    //
    // Parameters:
    //   left:
    //     The first value to compare.
    //
    //   right:
    //     The second value to compare.
    //
    // Returns:
    //     true if left and right are not equal; otherwise, false.
//    
    [Pure] public static bool operator !=(ulong left, BigInteger right)
    {
      return default(bool);
    }

    //
    // Summary:
    //     Returns the remainder that results from division with two specified System.Numerics.BigInteger
    //     values.
    //
    // Parameters:
    //   dividend:
    //     The value to be divided.
    //
    //   divisor:
    //     The value to divide by.
    //
    // Returns:
    //     The remainder that results from the division.
    //
    // Exceptions:
    //   System.DivideByZeroException:
    //     divisor is 0 (zero).
    [Pure] public static BigInteger operator %(BigInteger dividend, BigInteger divisor)
    {
      Contract.Requires(!divisor.IsZero);
      return default(BigInteger);
    }

    //
    // Summary:
    //     Performs a bitwise And operation on two System.Numerics.BigInteger values.
    //
    // Parameters:
    //   left:
    //     The first value.
    //
    //   right:
    //     The second value.
    //
    // Returns:
    //     The result of the bitwise And operation.
    [Pure] public static BigInteger operator &(BigInteger left, BigInteger right)
    {
      return default(BigInteger);
    }

    //
    // Summary:
    //     Multiplies two specified System.Numerics.BigInteger values.
    //
    // Parameters:
    //   left:
    //     The first value to multiply.
    //
    //   right:
    //     The second value to multiply.
    //
    // Returns:
    //     The product of left and right.
    [Pure] public static BigInteger operator *(BigInteger left, BigInteger right)
    {
      return default(BigInteger);
    }

    //
    // Summary:
    //     Divides a specified System.Numerics.BigInteger value by another specified
    //     System.Numerics.BigInteger value by using integer division.
    //
    // Parameters:
    //   dividend:
    //     The value to be divided.
    //
    //   divisor:
    //     The value to divide by.
    //
    // Returns:
    //     The integral result of the division.
    //
    // Exceptions:
    //   System.DivideByZeroException:
    //     divisor is 0 (zero).
    [Pure] public static BigInteger operator /(BigInteger dividend, BigInteger divisor)
    {
      Contract.Requires(!divisor.IsZero);
      return default(BigInteger);
    }

    //
    // Summary:
    //     Performs a bitwise exclusive Or (XOr) operation on two System.Numerics.BigInteger
    //     values.
    //
    // Parameters:
    //   left:
    //     The first value.
    //
    //   right:
    //     The second value.
    //
    // Returns:
    //     The result of the bitwise Or operation.
    [Pure] public static BigInteger operator ^(BigInteger left, BigInteger right)
    {
      return default(BigInteger);
    }

    //
    // Summary:
    //     Performs a bitwise Or operation on two System.Numerics.BigInteger values.
    //
    // Parameters:
    //   left:
    //     The first value.
    //
    //   right:
    //     The second value.
    //
    // Returns:
    //     The result of the bitwise Or operation.
    [Pure] public static BigInteger operator |(BigInteger left, BigInteger right)
    {
      return default(BigInteger);
    }

    //
    // Summary:
    //     Returns the bitwise one's complement of a System.Numerics.BigInteger value.
    //
    // Parameters:
    //   value:
    //     An integer value.
    //
    // Returns:
    //     The bitwise one's complement of value.
    [Pure] public static BigInteger operator ~(BigInteger value)
    {
      return default(BigInteger);
    }

    //
    // Summary:
    //     Returns the value of the System.Numerics.BigInteger operand. (The sign of
    //     the operand is unchanged.)
    //
    // Parameters:
    //   value:
    //     An integer value.
    //
    // Returns:
    //     The value of the value operand.
    [Pure] public static BigInteger operator +(BigInteger value)
    {
      return default(BigInteger);
    }

    //
    // Summary:
    //     Adds the values of two specified System.Numerics.BigInteger objects.
    //
    // Parameters:
    //   left:
    //     The first value to add.
    //
    //   right:
    //     The second value to add.
    //
    // Returns:
    //     The sum of left and right.
    [Pure] public static BigInteger operator +(BigInteger left, BigInteger right)
    {
      return default(BigInteger);
    }

    //
    // Summary:
    //     Increments a System.Numerics.BigInteger value by 1.
    //
    // Parameters:
    //   value:
    //     The value to increment.
    //
    // Returns:
    //     The value of the value parameter incremented by 1.
    [Pure] public static BigInteger operator ++(BigInteger value)
    {
      return default(BigInteger);
    }

    //
    // Summary:
    //     Returns a value that indicates whether a System.Numerics.BigInteger value
    //     is less than another System.Numerics.BigInteger value.
    //
    // Parameters:
    //   left:
    //     The first value to compare.
    //
    //   right:
    //     The second value to compare.
    //
    // Returns:
    //     true if left is less than right; otherwise, false.
    [Pure] public static bool operator <(BigInteger left, BigInteger right)
    {
      return default(bool);
    }

    //
    // Summary:
    //     Returns a value that indicates whether a System.Numerics.BigInteger value
    //     is less than a 64-bit signed integer.
    //
    // Parameters:
    //   left:
    //     The first value to compare.
    //
    //   right:
    //     The second value to compare.
    //
    // Returns:
    //     true if left is less than right; otherwise, false.
    [Pure] public static bool operator <(BigInteger left, long right)
    {
      return default(bool);
    }

    //
    // Summary:
    //     Returns a value that indicates whether a System.Numerics.BigInteger value
    //     is less than a 64-bit unsigned integer.
    //
    // Parameters:
    //   left:
    //     The first value to compare.
    //
    //   right:
    //     The second value to compare.
    //
    // Returns:
    //     true if left is less than right; otherwise, false.
    //
    [Pure] public static bool operator <(BigInteger left, ulong right)
    {
      return default(bool);
    }

    //
    // Summary:
    //     Returns a value that indicates whether a 64-bit signed integer is less than
    //     a System.Numerics.BigInteger value.
    //
    // Parameters:
    //   left:
    //     The first value to compare.
    //
    //   right:
    //     The second value to compare.
    //
    // Returns:
    //     true if left is less than right; otherwise, false.
    [Pure] public static bool operator <(long left, BigInteger right)
    {
      return default(bool);
    }

    //
    // Summary:
    //     Returns a value that indicates whether a 64-bit unsigned integer is less
    //     than a System.Numerics.BigInteger value.
    //
    // Parameters:
    //   left:
    //     The first value to compare.
    //
    //   right:
    //     The second value to compare.
    //
    // Returns:
    //     true if left is less than right; otherwise, false.
//    
    [Pure] public static bool operator <(ulong left, BigInteger right)
    {
      return default(bool);
    }

    //
    // Summary:
    //     Shifts a System.Numerics.BigInteger value a specified number of bits to the
    //     left.
    //
    // Parameters:
    //   value:
    //     The value whose bits are to be shifted.
    //
    //   shift:
    //     The number of bits to shift value to the left.
    //
    // Returns:
    //     A value that has been shifted to the left by the specified number of bits.
    [Pure] public static BigInteger operator <<(BigInteger value, int shift)
    {
      return default(BigInteger);
    }

    //
    // Summary:
    //     Returns a value that indicates whether a System.Numerics.BigInteger value
    //     is less than or equal to another System.Numerics.BigInteger value.
    //
    // Parameters:
    //   left:
    //     The first value to compare.
    //
    //   right:
    //     The second value to compare.
    //
    // Returns:
    //     true if left is less than or equal to right; otherwise, false.
    [Pure] public static bool operator <=(BigInteger left, BigInteger right)
    {
      return default(bool);
    }

    //
    // Summary:
    //     Returns a value that indicates whether a System.Numerics.BigInteger value
    //     is less than or equal to a 64-bit signed integer.
    //
    // Parameters:
    //   left:
    //     The first value to compare.
    //
    //   right:
    //     The second value to compare.
    //
    // Returns:
    //     true if left is less than or equal to right; otherwise, false.
    [Pure]
    public static bool operator <=(BigInteger left, long right)
    {
      return default(bool);
    }
    //
    // Summary:
    //     Returns a value that indicates whether a System.Numerics.BigInteger value
    //     is less than or equal to a 64-bit unsigned integer.
    //
    // Parameters:
    //   left:
    //     The first value to compare.
    //
    //   right:
    //     The second value to compare.
    //
    // Returns:
    //     true if left is less than or equal to right; otherwise, false.
    
    [Pure] public static bool operator <=(BigInteger left, ulong right)
    {
      return default(bool);
    }

    //
    // Summary:
    //     Returns a value that indicates whether a 64-bit signed integer is less than
    //     or equal to a System.Numerics.BigInteger value.
    //
    // Parameters:
    //   left:
    //     The first value to compare.
    //
    //   right:
    //     The second value to compare.
    //
    // Returns:
    //     true if left is less than or equal to right; otherwise, false.
    [Pure] public static bool operator <=(long left, BigInteger right)
    {
      return default(bool);
    }

    //
    // Summary:
    //     Returns a value that indicates whether a 64-bit unsigned integer is less
    //     than or equal to a System.Numerics.BigInteger value.
    //
    // Parameters:
    //   left:
    //     The first value to compare.
    //
    //   right:
    //     The second value to compare.
    //
    // Returns:
    //     true if left is less than or equal to right; otherwise, false.
    
    [Pure] public static bool operator <=(ulong left, BigInteger right)
    {
      return default(bool);
    }

    //
    // Summary:
    //     Returns a value that indicates whether the values of two System.Numerics.BigInteger
    //     objects are equal.
    //
    // Parameters:
    //   left:
    //     The first value to compare.
    //
    //   right:
    //     The second value to compare.
    //
    // Returns:
    //     true if the left and right parameters have the same value; otherwise, false.
    [Pure] public static bool operator ==(BigInteger left, BigInteger right)
    {
      return default(bool);
    }

    //
    // Summary:
    //     Returns a value that indicates whether a System.Numerics.BigInteger value
    //     and a signed long integer value are equal.
    //
    // Parameters:
    //   left:
    //     The first value to compare.
    //
    //   right:
    //     The second value to compare.
    //
    // Returns:
    //     true if the left and right parameters have the same value; otherwise, false.
    [Pure] public static bool operator ==(BigInteger left, long right)
    {
      return default(bool);
    }

    //
    // Summary:
    //     Returns a value that indicates whether a System.Numerics.BigInteger value
    //     and an unsigned long integer value are equal.
    //
    // Parameters:
    //   left:
    //     The first value to compare.
    //
    //   right:
    //     The second value to compare.
    //
    // Returns:
    //     true if the left and right parameters have the same value; otherwise, false.
    
    [Pure] public static bool operator ==(BigInteger left, ulong right)
    {
      return default(bool);
    }

    //
    // Summary:
    //     Returns a value that indicates whether a signed long integer value and a
    //     System.Numerics.BigInteger value are equal.
    //
    // Parameters:
    //   left:
    //     The first value to compare.
    //
    //   right:
    //     The second value to compare.
    //
    // Returns:
    //     true if the left and right parameters have the same value; otherwise, false.
    [Pure] public static bool operator ==(long left, BigInteger right)
    {
      return default(bool);
    }

    //
    // Summary:
    //     Returns a value that indicates whether an unsigned long integer value and
    //     a System.Numerics.BigInteger value are equal.
    //
    // Parameters:
    //   left:
    //     The first value to compare.
    //
    //   right:
    //     The second value to compare.
    //
    // Returns:
    //     true if the left and right parameters have the same value; otherwise, false.
    
    [Pure] public static bool operator ==(ulong left, BigInteger right)
    {
      return default(bool);
    }

    //
    // Summary:
    //     Returns a value that indicates whether a System.Numerics.BigInteger value
    //     is greater than another System.Numerics.BigInteger value.
    //
    // Parameters:
    //   left:
    //     The first value to compare.
    //
    //   right:
    //     The second value to compare.
    //
    // Returns:
    //     true if left is greater than right; otherwise, false.
    [Pure] public static bool operator >(BigInteger left, BigInteger right)
    {
      return default(bool);
    }

    //
    // Summary:
    //     Returns a value that indicates whether a System.Numerics.BigInteger is greater
    //     than a 64-bit signed integer value.
    //
    // Parameters:
    //   left:
    //     The first value to compare.
    //
    //   right:
    //     The second value to compare.
    //
    // Returns:
    //     true if left is greater than right; otherwise, false.
    [Pure] public static bool operator >(BigInteger left, long right)
    {
      return default(bool);
    }

    //
    // Summary:
    //     Returns a value that indicates whether a System.Numerics.BigInteger value
    //     is greater than a 64-bit unsigned integer.
    //
    // Parameters:
    //   left:
    //     The first value to compare.
    //
    //   right:
    //     The second value to compare.
    //
    // Returns:
    //     true if left is greater than right; otherwise, false.
    
    [Pure] public static bool operator >(BigInteger left, ulong right)
    {
      return default(bool);
    }

    //
    // Summary:
    //     Returns a value that indicates whether a 64-bit signed integer is greater
    //     than a System.Numerics.BigInteger value.
    //
    // Parameters:
    //   left:
    //     The first value to compare.
    //
    //   right:
    //     The second value to compare.
    //
    // Returns:
    //     true if left is greater than right; otherwise, false.
    [Pure] public static bool operator >(long left, BigInteger right)
    {
      return default(bool);
    }

    //
    // Summary:
    //     Returns a value that indicates whether a System.Numerics.BigInteger value
    //     is greater than a 64-bit unsigned integer.
    //
    // Parameters:
    //   left:
    //     The first value to compare.
    //
    //   right:
    //     The second value to compare.
    //
    // Returns:
    //     true if left is greater than right; otherwise, false.
    
    [Pure] public static bool operator >(ulong left, BigInteger right)
    {
      return default(bool);
    }

    //
    // Summary:
    //     Returns a value that indicates whether a System.Numerics.BigInteger value
    //     is greater than or equal to another System.Numerics.BigInteger value.
    //
    // Parameters:
    //   left:
    //     The first value to compare.
    //
    //   right:
    //     The second value to compare.
    //
    // Returns:
    //     true if left is greater than right; otherwise, false.
    [Pure] public static bool operator >=(BigInteger left, BigInteger right)
    {
      return default(bool);
    }

    //
    // Summary:
    //     Returns a value that indicates whether a System.Numerics.BigInteger value
    //     is greater than or equal to a 64-bit signed integer value.
    //
    // Parameters:
    //   left:
    //     The first value to compare.
    //
    //   right:
    //     The second value to compare.
    //
    // Returns:
    //     true if left is greater than right; otherwise, false.
    [Pure] public static bool operator >=(BigInteger left, long right)
    {
      return default(bool);
    }

    //
    // Summary:
    //     Returns a value that indicates whether a System.Numerics.BigInteger value
    //     is greater than or equal to a 64-bit unsigned integer value.
    //
    // Parameters:
    //   left:
    //     The first value to compare.
    //
    //   right:
    //     The second value to compare.
    //
    // Returns:
    //     true if left is greater than right; otherwise, false.
    
    [Pure] public static bool operator >=(BigInteger left, ulong right)
    {
      return default(bool);
    }

    //
    // Summary:
    //     Returns a value that indicates whether a 64-bit signed integer is greater
    //     than or equal to a System.Numerics.BigInteger value.
    //
    // Parameters:
    //   left:
    //     The first value to compare.
    //
    //   right:
    //     The second value to compare.
    //
    // Returns:
    //     true if left is greater than right; otherwise, false.
    [Pure] public static bool operator >=(long left, BigInteger right)
    {
      return default(bool);
    }

    //
    // Summary:
    //     Returns a value that indicates whether a 64-bit unsigned integer is greater
    //     than or equal to a System.Numerics.BigInteger value.
    //
    // Parameters:
    //   left:
    //     The first value to compare.
    //
    //   right:
    //     The second value to compare.
    //
    // Returns:
    //     true if left is greater than right; otherwise, false.
    
    [Pure] public static bool operator >=(ulong left, BigInteger right)
    {
      return default(bool);
    }

    //
    // Summary:
    //     Shifts a System.Numerics.BigInteger value a specified number of bits to the
    //     right.
    //
    // Parameters:
    //   value:
    //     The value whose bits are to be shifted.
    //
    //   shift:
    //     The number of bits to shift value to the right.
    //
    // Returns:
    //     A value that has been shifted to the right by the specified number of bits.
    [Pure] public static BigInteger operator >>(BigInteger value, int shift)
    {
      return default(BigInteger);
    }

    //
    // Summary:
    //     Defines an explicit conversion of a System.Numerics.BigInteger object to
    //     a signed 8-bit value.
    //
    // Parameters:
    //   value:
    //     The value to convert to a signed 8-bit value.
    //
    // Returns:
    //     An object that contains the value of the value parameter.
    //
    // Exceptions:
    //   System.OverflowException:
    //     value is less than System.SByte.MinValue.-or-value is greater than System.SByte.MaxValue.
    
    [Pure] public static explicit operator sbyte(BigInteger value)
    {
      Contract.Requires(value >= System.SByte.MinValue);
      Contract.Requires(value <= System.SByte.MaxValue);

      return default(sbyte);
    }

    //
    // Summary:
    //     Defines an explicit conversion of a System.Numerics.BigInteger object to
    //     a System.Decimal value.
    //
    // Parameters:
    //   value:
    //     The value to convert to a System.Decimal.
    //
    // Returns:
    //     An object that contains the value of the value parameter.
    //
    // Exceptions:
    //   System.OverflowException:
    //     value is less than System.Decimal.MinValue.-or-value is greater than System.Decimal.MaxValue.
    [Pure] public static explicit operator decimal(BigInteger value)
    {
      return default(decimal);
    }

    //
    // Summary:
    //     Defines an explicit conversion of a System.Numerics.BigInteger object to
    //     a System.Double value.
    //
    // Parameters:
    //   value:
    //     The value to convert to a System.Double.
    //
    // Returns:
    //     An object that contains the value of the value parameter.
    [Pure] public static explicit operator double(BigInteger value)
    {
      return default(double);
    }
    //
    // Summary:
    //     Defines an explicit conversion of a System.Numerics.BigInteger object to
    //     a single-precision floating-point value.
    //
    // Parameters:
    //   value:
    //     The value to convert to a single-precision floating-point value.
    //
    // Returns:
    //     An object that contains the closest possible representation of the value
    //     parameter.
    [Pure] public static explicit operator float(BigInteger value)
    {
      return default(float);
    }

    //
    // Summary:
    //     Defines an explicit conversion of a System.Numerics.BigInteger object to
    //     an unsigned 64-bit integer value.
    //
    // Parameters:
    //   value:
    //     The value to convert to an unsigned 64-bit integer.
    //
    // Returns:
    //     An object that contains the value of the value parameter.
    //
    // Exceptions:
    //   System.OverflowException:
    //     value is less than System.UInt64.MinValue.-or-value is greater than System.UInt64.MaxValue.
    
    [Pure] public static explicit operator ulong(BigInteger value)
    {
      Contract.Requires(value >= System.UInt64.MinValue);
      Contract.Requires(value <= System.UInt64.MaxValue);

      return default(ulong);
    }
    //
    // Summary:
    //     Defines an explicit conversion of a System.Numerics.BigInteger object to
    //     a 64-bit signed integer value.
    //
    // Parameters:
    //   value:
    //     The value to convert to a 64-bit signed integer.
    //
    // Returns:
    //     An object that contains the value of the value parameter.
    //
    // Exceptions:
    //   System.OverflowException:
    //     value is less than System.Int64.MinValue.-or-value is greater than System.Int64.MaxValue.
    [Pure] public static explicit operator long(BigInteger value)
    {
      Contract.Requires(value >= System.Int64.MinValue);
      Contract.Requires(value <= System.Int64.MaxValue);

      return default(long);
    }

    //
    // Summary:
    //     Defines an explicit conversion of a System.Numerics.BigInteger object to
    //     an unsigned 32-bit integer value.
    //
    // Parameters:
    //   value:
    //     The value to convert to an unsigned 32-bit integer.
    //
    // Returns:
    //     An object that contains the value of the value parameter.
    //
    // Exceptions:
    //   System.OverflowException:
    //     value is less than System.UInt32.MinValue.-or-value is greater than System.UInt32.MaxValue.
    
    [Pure] public static explicit operator uint(BigInteger value)
    {
      Contract.Requires(value >= System.UInt32.MinValue);
      Contract.Requires(value <= System.UInt32.MaxValue);

      return default(uint);
    }

    //
    // Summary:
    //     Defines an explicit conversion of a System.Numerics.BigInteger object to
    //     a 32-bit signed integer value.
    //
    // Parameters:
    //   value:
    //     The value to convert to a 32-bit signed integer.
    //
    // Returns:
    //     An object that contains the value of the value parameter.
    //
    // Exceptions:
    //   System.OverflowException:
    //     value is less than System.Int32.MinValue.-or-value is greater than System.Int32.MaxValue.
    [Pure] public static explicit operator int(BigInteger value)
    {
      Contract.Requires(value >= System.Int32.MinValue);
      Contract.Requires(value <= System.Int32.MaxValue);

      return default(int);
    }

    //
    // Summary:
    //     Defines an explicit conversion of a System.Numerics.BigInteger object to
    //     a 16-bit signed integer value.
    //
    // Parameters:
    //   value:
    //     The value to convert to a 16-bit signed integer.
    //
    // Returns:
    //     An object that contains the value of the value parameter.
    //
    // Exceptions:
    //   System.OverflowException:
    //     value is less than System.Int16.MinValue.-or-value is greater than System.Int16.MaxValue.
    [Pure] public static explicit operator short(BigInteger value)
    {
      Contract.Requires(value >= System.Int16.MinValue);
      Contract.Requires(value <= System.Int16.MaxValue);

      return default(Int16);
    }



    //
    // Summary:
    //     Defines an explicit conversion of a System.Numerics.BigInteger object to
    //     an unsigned 16-bit integer value.
    //
    // Parameters:
    //   value:
    //     The value to convert to an unsigned 16-bit integer.
    //
    // Returns:
    //     An object that contains the value of the value parameter
    //
    // Exceptions:
    //   System.OverflowException:
    //     value is less than System.UInt16.MinValue.-or-value is greater than System.UInt16.MaxValue.
    
    [Pure] public static explicit operator ushort(BigInteger value)
    {
      Contract.Requires(value >= System.UInt16.MinValue);
      Contract.Requires(value <= System.UInt16.MaxValue);

      return default(UInt16);
    }

    //
    // Summary:
    //     Defines an explicit conversion of a System.Numerics.BigInteger object to
    //     an unsigned byte value.
    //
    // Parameters:
    //   value:
    //     The value to convert to a System.Byte.
    //
    // Returns:
    //     An object that contains the value of the value parameter.
    //
    // Exceptions:
    //   System.OverflowException:
    //     value is less than System.Byte.MinValue. -or-value is greater than System.Byte.MaxValue.
    [Pure] public static explicit operator byte(BigInteger value)
    {
      Contract.Requires(value >= System.Byte.MinValue);
      Contract.Requires(value <= System.Byte.MaxValue);

      return default(byte);
    }

    //
    // Summary:
    //     Defines an explicit conversion of a System.Decimal object to a System.Numerics.BigInteger
    //     value.
    //
    // Parameters:
    //   value:
    //     The value to convert to a System.Numerics.BigInteger.
    //
    // Returns:
    //     An object that contains the value of the value parameter.
    [Pure] public static explicit operator BigInteger(decimal value)
    {
      return default(BigInteger);
    }
    //
    // Summary:
    //     Defines an explicit conversion of a System.Double value to a System.Numerics.BigInteger
    //     value.
    //
    // Parameters:
    //   value:
    //     The value to convert to a System.Numerics.BigInteger.
    //
    // Returns:
    //     An object that contains the value of the value parameter.
    //
    // Exceptions:
    //   System.OverflowException:
    //     value is System.Double.NaN.-or-value is System.Double.PositiveInfinity.-or-value
    //     is System.Double.NegativeInfinity.
    [Pure]
    public static explicit operator BigInteger(double value)
    {
      Contract.Requires(!Double.IsNaN(value));
      Contract.Requires(!Double.IsNegativeInfinity(value));
      Contract.Requires(!Double.IsPositiveInfinity(value));

      return default(BigInteger);
    }

    //
    // Summary:
    //     Defines an explicit conversion of a System.Single object to a System.Numerics.BigInteger
    //     value.
    //
    // Parameters:
    //   value:
    //     The value to convert to a System.Numerics.BigInteger.
    //
    // Returns:
    //     An object that contains the value of the value parameter.
    //
    // Exceptions:
    //   System.OverflowException:
    //     value is System.Single.NaN.-or-value is System.Single.PositiveInfinity.-or-value
    //     is System.Single.NegativeInfinity.
    [Pure] public static explicit operator BigInteger(float value)
    {
      Contract.Requires(!Single.IsNaN(value));
      Contract.Requires(!Single.IsNegativeInfinity(value));
      Contract.Requires(!Single.IsPositiveInfinity(value));

      return default(BigInteger);
    }

    //
    // Summary:
    //     Defines an implicit conversion of an unsigned byte to a System.Numerics.BigInteger
    //     value.
    //
    // Parameters:
    //   value:
    //     The value to convert to a System.Numerics.BigInteger.
    //
    // Returns:
    //     An object that contains the value of the value parameter.
    [Pure] public static implicit operator BigInteger(byte value)
    {
      return default(BigInteger);
    }

    //
    // Summary:
    //     Defines an implicit conversion of a signed 32-bit integer to a System.Numerics.BigInteger
    //     value.
    //
    // Parameters:
    //   value:
    //     The value to convert to a System.Numerics.BigInteger.
    //
    // Returns:
    //     An object that contains the value of the value parameter.
    [Pure] public static implicit operator BigInteger(int value)
    {
      return default(BigInteger);
    }

    //
    // Summary:
    //     Defines an implicit conversion of a signed 64-bit integer to a System.Numerics.BigInteger
    //     value.
    //
    // Parameters:
    //   value:
    //     The value to convert to a System.Numerics.BigInteger.
    //
    // Returns:
    //     An object that contains the value of the value parameter.
    [Pure] public static implicit operator BigInteger(long value)
    {
      return default(BigInteger);
    }

    //
    // Summary:
    //     Defines an implicit conversion of an 8-bit signed integer to a System.Numerics.BigInteger
    //     value.
    //
    // Parameters:
    //   value:
    //     The value to convert to a System.Numerics.BigInteger.
    //
    // Returns:
    //     An object that contains the value of the value parameter.
    [Pure] public static implicit operator BigInteger(sbyte value)
    {
      return default(BigInteger);
    }

    //
    // Summary:
    //     Defines an implicit conversion of a signed 16-bit integer to a System.Numerics.BigInteger
    //     value.
    //
    // Parameters:
    //   value:
    //     The value to convert to a System.Numerics.BigInteger.
    //
    // Returns:
    //     An object that contains the value of the value parameter.
    [Pure] public static implicit operator BigInteger(short value)
    {
      return default(BigInteger);
    }

    //
    // Summary:
    //     Defines an implicit conversion of a 32-bit unsigned integer to a System.Numerics.BigInteger
    //     value.
    //
    // Parameters:
    //   value:
    //     The value to convert to a System.Numerics.BigInteger.
    //
    // Returns:
    //     An object that contains the value of the value parameter.
    
    [Pure] public static implicit operator BigInteger(uint value)
    {
      return default(BigInteger);
    }

    //
    // Summary:
    //     Defines an implicit conversion of a 64-bit unsigned integer to a System.Numerics.BigInteger
    //     value.
    //
    // Parameters:
    //   value:
    //     The value to convert to a System.Numerics.BigInteger.
    //
    // Returns:
    //     An object that contains the value of the value parameter.
    
    [Pure] public static implicit operator BigInteger(ulong value)
    {
      return default(BigInteger);
    }

    //
    // Summary:
    //     Defines an implicit conversion of a 16-bit unsigned integer to a System.Numerics.BigInteger
    //     value.
    //
    // Parameters:
    //   value:
    //     The value to convert to a System.Numerics.BigInteger.
    //
    // Returns:
    //     An object that contains the value of the value parameter.
    //
    [Pure] public static implicit operator BigInteger(ushort value)
    {
      return default(BigInteger);
    }


    // Summary:
    //     Indicates whether the value of the current System.Numerics.BigInteger object
    //     is an even number.
    //
    // Returns:
    //     true if the value of the System.Numerics.BigInteger object is an even number;
    //     otherwise, false.
    //[Pure] public bool IsEven { get; }
    //
    // Summary:
    //     Indicates whether the value of the current System.Numerics.BigInteger object
    //     is System.Numerics.BigInteger.One.
    //
    // Returns:
    //     true if the value of the System.Numerics.BigInteger object is System.Numerics.BigInteger.One;
    //     otherwise, false.
    [Pure] public bool IsOne 
    { 
      get { return default(bool); } 
    }
    //
    // Summary:
    //     Indicates whether the value of the current System.Numerics.BigInteger object
    //     is a power of two.
    //
    // Returns:
    //     true if the value of the System.Numerics.BigInteger object is a power of
    //     two; otherwise, false.
    //public bool IsPowerOfTwo { get; }
    //
    // Summary:
    //     Indicates whether the value of the current System.Numerics.BigInteger object
    //     is System.Numerics.BigInteger.Zero.
    //
    // Returns:
    //     true if the value of the System.Numerics.BigInteger object is System.Numerics.BigInteger.Zero;
    //     otherwise, false.
    public bool IsZero
    {
      get { return default(bool); }
    }
    //
    // Summary:
    //     Gets a value that represents the number negative one (-1).
    //
    // Returns:
    //     An integer whose value is negative one (-1).
    //public static BigInteger MinusOne { get; }
    //
    // Summary:
    //     Gets a value that represents the number one (1).
    //
    // Returns:
    //     An object whose value is one (1).
    public static BigInteger One
    {
      get
      {
        Contract.Ensures(Contract.Result<BigInteger>().IsOne);
        return default(BigInteger);
      }
    }

    //
    // Summary:
    //     Gets a number that indicates the sign (negative, positive, or zero) of the
    //     current System.Numerics.BigInteger object.
    //
    // Returns:
    //     A number that indicates the sign of the System.Numerics.BigInteger object,
    //     as shown in the following table.NumberDescription-1The value of this object
    //     is negative.0The value of this object is 0 (zero).1The value of this object
    //     is positive.
    public int Sign
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= -1);
        Contract.Ensures(Contract.Result<int>() <= 1);
        Contract.Ensures((Contract.Result<int>() == 0) == this.IsZero);

        return default(int);
      }
    }
    //
    // Summary:
    //     Gets a value that represents the number 0 (zero).
    //
    // Returns:
    //     An integer whose value is 0 (zero).
    public static BigInteger Zero 
    {
      get
      {
        Contract.Ensures(Contract.Result<BigInteger>().IsZero);
        return default(BigInteger);
      }
    }

    // Summary:
    //     Gets the absolute value of a System.Numerics.BigInteger object.
    //
    // Parameters:
    //   value:
    //     A number.
    //
    // Returns:
    //     The absolute value of value.
    [Pure] public static BigInteger Abs(BigInteger value)
    {
      Contract.Ensures(Contract.Result<BigInteger>().Sign >= 0);

      return default(BigInteger);
    }
    //
    // Summary:
    //     Adds two System.Numerics.BigInteger values and returns the result.
    //
    // Parameters:
    //   left:
    //     The first value to add.
    //
    //   right:
    //     The second value to add.
    //
    // Returns:
    //     The sum of left and right.
    //[TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    [Pure] public static BigInteger Add(BigInteger left, BigInteger right)
    {
      return default(BigInteger);
    }

    //
    // Summary:
    //     Compares two System.Numerics.BigInteger values and returns an integer that
    //     indicates whether the first value is less than, equal to, or greater than
    //     the second value.
    //
    // Parameters:
    //   left:
    //     The first value to compare.
    //
    //   right:
    //     The second value to compare.
    //
    // Returns:
    //     A signed integer that indicates the relative values of left and right, as
    //     shown in the following table.ValueConditionLess than zeroleft is less than
    //     right.Zeroleft equals right.Greater than zeroleft is greater than right.
    [Pure] public static int Compare(BigInteger left, BigInteger right)
    {
      return default(int);
    }

    //
    // Summary:
    //     Compares this instance to a second System.Numerics.BigInteger and returns
    //     an integer that indicates whether the value of this instance is less than,
    //     equal to, or greater than the value of the specified object.
    //
    // Parameters:
    //   other:
    //     The object to compare.
    //
    // Returns:
    //     A signed integer value that indicates the relationship of this instance to
    //     other, as shown in the following table.Return valueDescriptionLess than zeroThe
    //     current instance is less than other.ZeroThe current instance equals other.Greater
    //     than zeroThe current instance is greater than other.
    //[Pure] public int CompareTo(BigInteger other)
    //{
//      return default(int);
 //   }

    //
    // Summary:
    //     Compares this instance to a signed 64-bit integer and returns an integer
    //     that indicates whether the value of this instance is less than, equal to,
    //     or greater than the value of the signed 64-bit integer.
    //
    // Parameters:
    //   other:
    //     The signed 64-bit integer to compare.
    //
    // Returns:
    //     A signed integer value that indicates the relationship of this instance to
    //     other, as shown in the following table.Return valueDescriptionLess than zeroThe
    //     current instance is less than other.ZeroThe current instance equals other.Greater
    //     than zeroThe current instance is greater than other.
    [Pure] public int CompareTo(long other)
    {
      return default(int);
    }

    //
    // Summary:
    //     Compares this instance to a specified object and returns an integer that
    //     indicates whether the value of this instance is less than, equal to, or greater
    //     than the value of the specified object.
    //
    // Parameters:
    //   obj:
    //     The object to compare.
    //
    // Returns:
    //     A signed integer that indicates the relationship of the current instance
    //     to the obj parameter, as shown in the following table.Return valueDescriptionLess
    //     than zeroThe current instance is less than obj.ZeroThe current instance equals
    //     obj.Greater than zeroThe current instance is greater than obj, or the obj
    //     parameter is null.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     obj is not a System.Numerics.BigInteger.
    //[Pure] public int CompareTo(object obj)
    //{
     // Contract.Requires(obj != null);
      //Contract.Requires(obj is BigInteger);

//      return default(int);
  //  }

    //
    // Summary:
    //     Compares this instance to an unsigned 64-bit integer and returns an integer
    //     that indicates whether the value of this instance is less than, equal to,
    //     or greater than the value of the unsigned 64-bit integer.
    //
    // Parameters:
    //   other:
    //     The unsigned 64-bit integer to compare.
    //
    // Returns:
    //     A signed integer that indicates the relative value of this instance and other,
    //     as shown in the following table.Return valueDescriptionLess than zeroThe
    //     current instance is less than other.ZeroThe current instance equals other.Greater
    //     than zeroThe current instance is greater than other.
    
    [Pure] public int CompareTo(ulong other)
    {
      return default(int);
    }

    //
    // Summary:
    //     Divides one System.Numerics.BigInteger value by another and returns the result.
    //
    // Parameters:
    //   dividend:
    //     The value to be divided.
    //
    //   divisor:
    //     The value to divide by.
    //
    // Returns:
    //     The quotient of the division.
    //
    // Exceptions:
    //   System.DivideByZeroException:
    //     divisor is 0 (zero).
    [Pure] public static BigInteger Divide(BigInteger dividend, BigInteger divisor)
    {
      Contract.Requires(!divisor.IsZero);
      
      return default(BigInteger);
    }

    //
    // Summary:
    //     Divides one System.Numerics.BigInteger value by another, returns the result,
    //     and returns the remainder in an output parameter.
    //
    // Parameters:
    //   dividend:
    //     The value to be divided.
    //
    //   divisor:
    //     The value to divide by.
    //
    //   remainder:
    //     When this method returns, contains a System.Numerics.BigInteger value that
    //     represents the remainder from the division. This parameter is passed uninitialized.
    //
    // Returns:
    //     The quotient of the division.
    //
    // Exceptions:
    //   System.DivideByZeroException:
    //     divisor is 0 (zero).
    [Pure] public static BigInteger DivRem(BigInteger dividend, BigInteger divisor, out BigInteger remainder)
    {
      Contract.Requires(!divisor.IsZero);

      return default(BigInteger);
    }

    //
    // Summary:
    //     Returns a value that indicates whether the current instance and a specified
    //     System.Numerics.BigInteger object have the same value.
    //
    // Parameters:
    //   other:
    //     The object to compare.
    //
    // Returns:
    //     true if this System.Numerics.BigInteger object and other have the same value;
    //     otherwise, false.
    //public bool Equals(BigInteger other);
    //
    // Summary:
    //     Returns a value that indicates whether the current instance and a signed
    //     64-bit integer have the same value.
    //
    // Parameters:
    //   other:
    //     The signed 64-bit integer value to compare.
    //
    // Returns:
    //     true if the signed 64-bit integer and the current instance have the same
    //     value; otherwise, false.
    [Pure] public bool Equals(long other)
    {
      return default(bool);
    }
    //
    // Summary:
    //     Returns a value that indicates whether the current instance and a specified
    //     object have the same value.
    //
    // Parameters:
    //   obj:
    //     The object to compare.
    //
    // Returns:
    //     true if the obj parameter is a System.Numerics.BigInteger object or a type
    //     capable of implicit conversion to a System.Numerics.BigInteger value, and
    //     its value is equal to the value of the current System.Numerics.BigInteger
    //     object; otherwise, false.
    extern public override bool Equals(object obj);
    //
    // Summary:
    //     Returns a value that indicates whether the current instance and an unsigned
    //     64-bit integer have the same value.
    //
    // Parameters:
    //   other:
    //     The unsigned 64-bit integer to compare.
    //
    // Returns:
    //     true if the current instance and the unsigned 64-bit integer have the same
    //     value; otherwise, false.
    [Pure] public bool Equals(ulong other)
    {
      return default(bool);
    }

    //
    // Summary:
    //     Returns the hash code for the current System.Numerics.BigInteger object.
    //
    // Returns:
    //     A 32-bit signed integer hash code.
    extern public override int GetHashCode();
    //
    // Summary:
    //     Finds the greatest common divisor of two System.Numerics.BigInteger values.
    //
    // Parameters:
    //   left:
    //     The first value.
    //
    //   right:
    //     The second value.
    //
    // Returns:
    //     The greatest common divisor of left and right.
    [Pure]
    public static BigInteger GreatestCommonDivisor(BigInteger left, BigInteger right)
    {
      return default(BigInteger);
    }

    //
    // Summary:
    //     Returns the natural (base e) logarithm of a specified number.
    //
    // Parameters:
    //   value:
    //     The number whose logarithm is to be found.
    //
    // Returns:
    //     The natural (base e) logarithm of value, as shown in the table in the Remarks
    //     section.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     The natural log of value is out of range of the System.Double data type.
    [Pure]
    public static double Log(BigInteger value)
    {
      return default(double);
    }
    //
    // Summary:
    //     Returns the logarithm of a specified number in a specified base.
    //
    // Parameters:
    //   value:
    //     A number whose logarithm is to be found.
    //
    //   baseValue:
    //     The base of the logarithm.
    //
    // Returns:
    //     The base baseValue logarithm of value, as shown in the table in the Remarks
    //     section.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     The log of value is out of range of the System.Double data type.
    [Pure] public static double Log(BigInteger value, double baseValue)
    {
      return default(double);    
    }

    //
    // Summary:
    //     Returns the base 10 logarithm of a specified number.
    //
    // Parameters:
    //   value:
    //     A number whose logarithm is to be found.
    //
    // Returns:
    //     The base 10 logarithm of value, as shown in the table in the Remarks section.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     The base 10 log of value is out of range of the System.Double data type.
    [Pure] public static double Log10(BigInteger value)
    {
      return default(double);
    }

    //
    // Summary:
    //     Returns the larger of two System.Numerics.BigInteger values.
    //
    // Parameters:
    //   left:
    //     The first value to compare.
    //
    //   right:
    //     The second value to compare.
    //
    // Returns:
    //     The left or right parameter, whichever is larger.
    [Pure] public static BigInteger Max(BigInteger left, BigInteger right)
    {
      Contract.Ensures(Contract.Result<BigInteger>() >= left);
      Contract.Ensures(Contract.Result<BigInteger>() >= right);
      Contract.Ensures(Contract.Result<BigInteger>() == (left >= right? left : right));

      return default(BigInteger);
    }

    //
    // Summary:
    //     Returns the smaller of two System.Numerics.BigInteger values.
    //
    // Parameters:
    //   left:
    //     The first value to compare.
    //
    //   right:
    //     The second value to compare.
    //
    // Returns:
    //     The left or right parameter, whichever is smaller.
    [Pure] public static BigInteger Min(BigInteger left, BigInteger right)
    {
      Contract.Ensures(Contract.Result<BigInteger>() <= left);
      Contract.Ensures(Contract.Result<BigInteger>() <= right);
      Contract.Ensures(Contract.Result<BigInteger>() == (left <= right ? left : right));

      return default(BigInteger);
    }
    //
    // Summary:
    //     Performs modulus division on a number raised to the power of another number.
    //
    // Parameters:
    //   value:
    //     The number to raise to the exponent power.
    //
    //   exponent:
    //     The exponent to raise value by.
    //
    //   modulus:
    //     The value to divide valueexponent by.
    //
    // Returns:
    //     The remainder after dividing valueexponent by modulus.
    //
    // Exceptions:
    //   System.DivideByZeroException:
    //     modulus is zero.
    //
    //   System.ArgumentOutOfRangeException:
    //     exponent is negative.
    [Pure] public static BigInteger ModPow(BigInteger value, BigInteger exponent, BigInteger modulus)
    {
      Contract.Requires(!modulus.IsZero);
      Contract.Requires(exponent >= 0);

      return default(BigInteger);
    }
    //
    // Summary:
    //     Returns the product of two System.Numerics.BigInteger values.
    //
    // Parameters:
    //   left:
    //     The first number to multiply.
    //
    //   right:
    //     The second number to multiply.
    //
    // Returns:
    //     The product of the left and right parameters.
    [Pure] public static BigInteger Multiply(BigInteger left, BigInteger right)
    {
      return default(BigInteger);
    }

    //
    // Summary:
    //     Negates a specified System.Numerics.BigInteger value.
    //
    // Parameters:
    //   value:
    //     The value to negate.
    //
    // Returns:
    //     The result of the value parameter multiplied by negative one (-1).
    [Pure] public static BigInteger Negate(BigInteger value)
    {
      return default(BigInteger);
    }

    //
    // Summary:
    //     Converts the string representation of a number to its System.Numerics.BigInteger
    //     equivalent.
    //
    // Parameters:
    //   value:
    //     A string that contains the number to convert.
    //
    // Returns:
    //     A value that is equivalent to the number specified in the value parameter.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     value is null.
    //
    //   System.FormatException:
    //     value is not in the correct format.
    [Pure] public static BigInteger Parse(string value)
    {
      Contract.Requires(value != null);

      return default(BigInteger);
    }

    //
    // Summary:
    //     Converts the string representation of a number in a specified culture-specific
    //     format to its System.Numerics.BigInteger equivalent.
    //
    // Parameters:
    //   value:
    //     A string that contains a number to convert.
    //
    //   provider:
    //     An object that provides culture-specific formatting information about value.
    //
    // Returns:
    //     A value that is equivalent to the number specified in the value parameter.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     value is null.
    //
    //   System.FormatException:
    //     value is not in the correct format.
    [Pure] public static BigInteger Parse(string value, IFormatProvider provider)
    {
      Contract.Requires(value != null);
      return default(BigInteger);
    }
    //
    // Summary:
    //     Converts the string representation of a number in a specified style to its
    //     System.Numerics.BigInteger equivalent.
    //
    // Parameters:
    //   value:
    //     A string that contains a number to convert.
    //
    //   style:
    //     A bitwise combination of the enumeration values that specify the permitted
    //     format of value.
    //
    // Returns:
    //     A value that is equivalent to the number specified in the value parameter.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     style is not a System.Globalization.NumberStyles value.-or-style includes
    //     the System.Globalization.NumberStyles.AllowHexSpecifier or System.Globalization.NumberStyles.HexNumber
    //     flag along with another value.
    //
    //   System.ArgumentNullException:
    //     value is null.
    //
    //   System.FormatException:
    //     value does not comply with the input pattern specified by System.Globalization.NumberStyles.
    [Pure] public static BigInteger Parse(string value, NumberStyles style)
    {
      Contract.Requires(value != null);
      Contract.Requires(Enum.IsDefined(typeof(NumberStyles), style));

      return default(BigInteger);
    }
    //
    // Summary:
    //     Converts the string representation of a number in a specified style and culture-specific
    //     format to its System.Numerics.BigInteger equivalent.
    //
    // Parameters:
    //   value:
    //     A string that contains a number to convert.
    //
    //   style:
    //     A bitwise combination of the enumeration values that specify the permitted
    //     format of value.
    //
    //   provider:
    //     An object that provides culture-specific formatting information about value.
    //
    // Returns:
    //     A value that is equivalent to the number specified in the value parameter.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     style is not a System.Globalization.NumberStyles value.-or-style includes
    //     the System.Globalization.NumberStyles.AllowHexSpecifier or System.Globalization.NumberStyles.HexNumber
    //     flag along with another value.
    //
    //   System.ArgumentNullException:
    //     value is null.
    //
    //   System.FormatException:
    //     value does not comply with the input pattern specified by style.
    [Pure] public static BigInteger Parse(string value, NumberStyles style, IFormatProvider provider)
    {
      Contract.Requires(value != null);
      Contract.Requires(Enum.IsDefined(typeof(NumberStyles), style));

      return default(BigInteger);
    }
    //
    // Summary:
    //     Raises a System.Numerics.BigInteger value to the power of a specified value.
    //
    // Parameters:
    //   value:
    //     The number to raise to the exponent power.
    //
    //   exponent:
    //     The exponent to raise value by.
    //
    // Returns:
    //     The result of raising value to the exponent power.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     The value of the exponent parameter is negative.
    [Pure] public static BigInteger Pow(BigInteger value, int exponent)
    {
      Contract.Requires(exponent >= 0);

      return default(BigInteger);
    }
 
    //
    // Summary:
    //     Performs integer division on two System.Numerics.BigInteger values and returns
    //     the remainder.
    //
    // Parameters:
    //   dividend:
    //     The value to be divided.
    //
    //   divisor:
    //     The value to divide by.
    //
    // Returns:
    //     The remainder after dividing dividend by divisor.
    //
    // Exceptions:
    //   System.DivideByZeroException:
    //     divisor is 0 (zero).
    [Pure] public static BigInteger Remainder(BigInteger dividend, BigInteger divisor)
    {
      Contract.Requires(!divisor.IsZero);

      return default(BigInteger);
    }
    //
    // Summary:
    //     Subtracts one System.Numerics.BigInteger value from another and returns the
    //     result.
    //
    // Parameters:
    //   left:
    //     The value to subtract from (the minuend).
    //
    //   right:
    //     The value to subtract (the subtrahend).
    //
    // Returns:
    //     The result of subtracting right from left.
    [Pure] public static BigInteger Subtract(BigInteger left, BigInteger right)
    {
      return default(BigInteger);
    }
    //
    // Summary:
    //     Converts a System.Numerics.BigInteger value to a byte array.
    //
    // Returns:
    //     The value of the current System.Numerics.BigInteger object converted to an
    //     array of bytes.
    [Pure] public byte[] ToByteArray()
    {
      Contract.Ensures(Contract.Result<byte[]>() != null);

      return default(byte[]);
    }
    //
    // Summary:
    //     Converts the numeric value of the current System.Numerics.BigInteger object
    //     to its equivalent string representation.
    //
    // Returns:
    //     The string representation of the current System.Numerics.BigInteger value.
    //public override string ToString();
    //
    // Summary:
    //     Converts the numeric value of the current System.Numerics.BigInteger object
    //     to its equivalent string representation by using the specified culture-specific
    //     formatting information.
    //
    // Parameters:
    //   provider:
    //     An object that supplies culture-specific formatting information.
    //
    // Returns:
    //     The string representation of the current System.Numerics.BigInteger value
    //     in the format specified by the provider parameter.
    //public string ToString(IFormatProvider provider);
    //
    // Summary:
    //     Converts the numeric value of the current System.Numerics.BigInteger object
    //     to its equivalent string representation by using the specified format.
    //
    // Parameters:
    //   format:
    //     A standard or custom numeric format string.
    //
    // Returns:
    //     The string representation of the current System.Numerics.BigInteger value
    //     in the format specified by the format parameter.
    //
    // Exceptions:
    //   System.FormatException:
    //     format is not a valid format string.
    //public string ToString(string format);
    //
    // Summary:
    //     Converts the numeric value of the current System.Numerics.BigInteger object
    //     to its equivalent string representation by using the specified format and
    //     culture-specific format information.
    //
    // Parameters:
    //   format:
    //     A standard or custom numeric format string.
    //
    //   provider:
    //     An object that supplies culture-specific formatting information.
    //
    // Returns:
    //     The string representation of the current System.Numerics.BigInteger value
    //     as specified by the format and provider parameters.
    //
    // Exceptions:
    //   System.FormatException:
    //     format is not a valid format string.
    //public string ToString(string format, IFormatProvider provider);
    //
    // Summary:
    //     Tries to convert the string representation of a number to its System.Numerics.BigInteger
    //     equivalent, and returns a value that indicates whether the conversion succeeded.
    //
    // Parameters:
    //   value:
    //     The string representation of a number.
    //
    //   result:
    //     When this method returns, contains the System.Numerics.BigInteger equivalent
    //     to the number that is contained in value, or zero (0) if the conversion fails.
    //     The conversion fails if the value parameter is null or is not of the correct
    //     format. This parameter is passed uninitialized.
    //
    // Returns:
    //     true if value was converted successfully; otherwise, false.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     value is null.
    [Pure] public static bool TryParse(string value, out BigInteger result)
    {
      Contract.Requires(value != null);

      result = default(BigInteger);
      return default(bool);
    }
    //
    // Summary:
    //     Tries to convert the string representation of a number in a specified style
    //     and culture-specific format to its System.Numerics.BigInteger equivalent,
    //     and returns a value that indicates whether the conversion succeeded.
    //
    // Parameters:
    //   value:
    //     The string representation of a number. The string is interpreted using the
    //     style specified by style.
    //
    //   style:
    //     A bitwise combination of enumeration values that indicates the style elements
    //     that can be present in value. A typical value to specify is System.Globalization.NumberStyles.Integer.
    //
    //   provider:
    //     An object that supplies culture-specific formatting information about value.
    //
    //   result:
    //     When this method returns, contains the System.Numerics.BigInteger equivalent
    //     to the number that is contained in value, or System.Numerics.BigInteger.Zero
    //     if the conversion failed. The conversion fails if the value parameter is
    //     null or is not in a format that is compliant with style. This parameter is
    //     passed uninitialized.
    //
    // Returns:
    //     true if the value parameter was converted successfully; otherwise, false.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     style is not a System.Globalization.NumberStyles value.-or-style includes
    //     the System.Globalization.NumberStyles.AllowHexSpecifier or System.Globalization.NumberStyles.HexNumber
    //     flag along with another value.
    [Pure] public static bool TryParse(string value, NumberStyles style, IFormatProvider provider, out BigInteger result)
    {
      Contract.Requires(Enum.IsDefined(typeof(NumberStyles), style));

      result = default(BigInteger);
      return default(bool);
    }
  }
}
