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

namespace System {
  // Summary:
  //     Defines methods that convert the value of the implementing reference or value
  //     type to a common language runtime type that has an equivalent value.
  [ContractClass(typeof(IConvertibleContract))]
  public interface IConvertible {
    // Summary:
    //     Returns the System.TypeCode for this instance.
    //
    // Returns:
    //     The enumerated constant that is the System.TypeCode of the class or value
    //     type that implements this interface.
    [Pure]
    TypeCode GetTypeCode();
    //
    // Summary:
    //     Converts the value of this instance to an equivalent Boolean value using
    //     the specified culture-specific formatting information.
    //
    // Parameters:
    //   provider:
    //     An System.IFormatProvider interface implementation that supplies culture-specific
    //     formatting information.
    //
    // Returns:
    //     A Boolean value equivalent to the value of this instance.
    [Pure]
    bool ToBoolean(IFormatProvider provider);
    //
    // Summary:
    //     Converts the value of this instance to an equivalent 8-bit unsigned integer
    //     using the specified culture-specific formatting information.
    //
    // Parameters:
    //   provider:
    //     An System.IFormatProvider interface implementation that supplies culture-specific
    //     formatting information.
    //
    // Returns:
    //     An 8-bit unsigned integer equivalent to the value of this instance.
    [Pure]
    byte ToByte(IFormatProvider provider);
    //
    // Summary:
    //     Converts the value of this instance to an equivalent Unicode character using
    //     the specified culture-specific formatting information.
    //
    // Parameters:
    //   provider:
    //     An System.IFormatProvider interface implementation that supplies culture-specific
    //     formatting information.
    //
    // Returns:
    //     A Unicode character equivalent to the value of this instance.
    [Pure]
    char ToChar(IFormatProvider provider);
    //
    // Summary:
    //     Converts the value of this instance to an equivalent System.DateTime using
    //     the specified culture-specific formatting information.
    //
    // Parameters:
    //   provider:
    //     An System.IFormatProvider interface implementation that supplies culture-specific
    //     formatting information.
    //
    // Returns:
    //     A System.DateTime instance equivalent to the value of this instance.
    [Pure]
    DateTime ToDateTime(IFormatProvider provider);
    //
    // Summary:
    //     Converts the value of this instance to an equivalent System.Decimal number
    //     using the specified culture-specific formatting information.
    //
    // Parameters:
    //   provider:
    //     An System.IFormatProvider interface implementation that supplies culture-specific
    //     formatting information.
    //
    // Returns:
    //     A System.Decimal number equivalent to the value of this instance.
    [Pure]
    decimal ToDecimal(IFormatProvider provider);
    //
    // Summary:
    //     Converts the value of this instance to an equivalent double-precision floating-point
    //     number using the specified culture-specific formatting information.
    //
    // Parameters:
    //   provider:
    //     An System.IFormatProvider interface implementation that supplies culture-specific
    //     formatting information.
    //
    // Returns:
    //     A double-precision floating-point number equivalent to the value of this
    //     instance.
    [Pure]
    double ToDouble(IFormatProvider provider);
    //
    // Summary:
    //     Converts the value of this instance to an equivalent 16-bit signed integer
    //     using the specified culture-specific formatting information.
    //
    // Parameters:
    //   provider:
    //     An System.IFormatProvider interface implementation that supplies culture-specific
    //     formatting information.
    //
    // Returns:
    //     An 16-bit signed integer equivalent to the value of this instance.
    [Pure]
    short ToInt16(IFormatProvider provider);
    //
    // Summary:
    //     Converts the value of this instance to an equivalent 32-bit signed integer
    //     using the specified culture-specific formatting information.
    //
    // Parameters:
    //   provider:
    //     An System.IFormatProvider interface implementation that supplies culture-specific
    //     formatting information.
    //
    // Returns:
    //     An 32-bit signed integer equivalent to the value of this instance.
    [Pure]
    int ToInt32(IFormatProvider provider);
    //
    // Summary:
    //     Converts the value of this instance to an equivalent 64-bit signed integer
    //     using the specified culture-specific formatting information.
    //
    // Parameters:
    //   provider:
    //     An System.IFormatProvider interface implementation that supplies culture-specific
    //     formatting information.
    //
    // Returns:
    //     An 64-bit signed integer equivalent to the value of this instance.
    [Pure]
    long ToInt64(IFormatProvider provider);
    //
    // Summary:
    //     Converts the value of this instance to an equivalent 8-bit signed integer
    //     using the specified culture-specific formatting information.
    //
    // Parameters:
    //   provider:
    //     An System.IFormatProvider interface implementation that supplies culture-specific
    //     formatting information.
    //
    // Returns:
    //     An 8-bit signed integer equivalent to the value of this instance.
    [Pure]
    sbyte ToSByte(IFormatProvider provider);
    //
    // Summary:
    //     Converts the value of this instance to an equivalent single-precision floating-point
    //     number using the specified culture-specific formatting information.
    //
    // Parameters:
    //   provider:
    //     An System.IFormatProvider interface implementation that supplies culture-specific
    //     formatting information.
    //
    // Returns:
    //     A single-precision floating-point number equivalent to the value of this
    //     instance.
    [Pure]
    float ToSingle(IFormatProvider provider);
    //
    // Summary:
    //     Converts the value of this instance to an equivalent System.String using
    //     the specified culture-specific formatting information.
    //
    // Parameters:
    //   provider:
    //     An System.IFormatProvider interface implementation that supplies culture-specific
    //     formatting information.
    //
    // Returns:
    //     A System.String instance equivalent to the value of this instance.
    [Pure]
    string ToString(IFormatProvider provider);
    //
    // Summary:
    //     Converts the value of this instance to an System.Object of the specified
    //     System.Type that has an equivalent value, using the specified culture-specific
    //     formatting information.
    //
    // Parameters:
    //   provider:
    //     An System.IFormatProvider interface implementation that supplies culture-specific
    //     formatting information.
    //
    //   conversionType:
    //     The System.Type to which the value of this instance is converted.
    //
    // Returns:
    //     An System.Object instance of type conversionType whose value is equivalent
    //     to the value of this instance.
    [Pure]
    object ToType(Type conversionType, IFormatProvider provider);
    //
    // Summary:
    //     Converts the value of this instance to an equivalent 16-bit unsigned integer
    //     using the specified culture-specific formatting information.
    //
    // Parameters:
    //   provider:
    //     An System.IFormatProvider interface implementation that supplies culture-specific
    //     formatting information.
    //
    // Returns:
    //     An 16-bit unsigned integer equivalent to the value of this instance.
    [Pure]
    ushort ToUInt16(IFormatProvider provider);
    //
    // Summary:
    //     Converts the value of this instance to an equivalent 32-bit unsigned integer
    //     using the specified culture-specific formatting information.
    //
    // Parameters:
    //   provider:
    //     An System.IFormatProvider interface implementation that supplies culture-specific
    //     formatting information.
    //
    // Returns:
    //     An 32-bit unsigned integer equivalent to the value of this instance.
    [Pure]
    uint ToUInt32(IFormatProvider provider);
    //
    // Summary:
    //     Converts the value of this instance to an equivalent 64-bit unsigned integer
    //     using the specified culture-specific formatting information.
    //
    // Parameters:
    //   provider:
    //     An System.IFormatProvider interface implementation that supplies culture-specific
    //     formatting information.
    //
    // Returns:
    //     An 64-bit unsigned integer equivalent to the value of this instance.
    [Pure]
    ulong ToUInt64(IFormatProvider provider);
  }

  [ContractClassFor(typeof(IConvertible))]
  abstract class IConvertibleContract : IConvertible
  {
    #region IConvertible Members

    TypeCode IConvertible.GetTypeCode()
    {
      throw new NotImplementedException();
    }

    bool IConvertible.ToBoolean(IFormatProvider provider)
    {
      throw new NotImplementedException();
    }

    byte IConvertible.ToByte(IFormatProvider provider)
    {
      throw new NotImplementedException();
    }

    char IConvertible.ToChar(IFormatProvider provider)
    {
      throw new NotImplementedException();
    }

    DateTime IConvertible.ToDateTime(IFormatProvider provider)
    {
      throw new NotImplementedException();
    }

    decimal IConvertible.ToDecimal(IFormatProvider provider)
    {
      throw new NotImplementedException();
    }

    double IConvertible.ToDouble(IFormatProvider provider)
    {
      throw new NotImplementedException();
    }

    short IConvertible.ToInt16(IFormatProvider provider)
    {
      throw new NotImplementedException();
    }

    int IConvertible.ToInt32(IFormatProvider provider)
    {
      throw new NotImplementedException();
    }

    long IConvertible.ToInt64(IFormatProvider provider)
    {
      throw new NotImplementedException();
    }

    sbyte IConvertible.ToSByte(IFormatProvider provider)
    {
      throw new NotImplementedException();
    }

    float IConvertible.ToSingle(IFormatProvider provider)
    {
      throw new NotImplementedException();
    }

    [Pure]
    string IConvertible.ToString(IFormatProvider provider)
    {
      Contract.Ensures(Contract.Result<string>() != null);

      return default(string);
    }

    object IConvertible.ToType(Type conversionType, IFormatProvider provider)
    {
      throw new NotImplementedException();
    }

    ushort IConvertible.ToUInt16(IFormatProvider provider)
    {
      throw new NotImplementedException();
    }

    uint IConvertible.ToUInt32(IFormatProvider provider)
    {
      throw new NotImplementedException();
    }

    ulong IConvertible.ToUInt64(IFormatProvider provider)
    {
      throw new NotImplementedException();
    }

    #endregion
  }
}
