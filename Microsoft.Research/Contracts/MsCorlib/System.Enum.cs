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

namespace System
{
  public abstract class Enum
  {
#if !SILVERLIGHT
    [Pure]
    public static string Format(Type enumType, object value, string format)
    {
      Contract.Requires(enumType != null);
      Contract.Requires(value != null);
      Contract.Requires(format != null);
      Contract.Ensures(Contract.Result<string>() != null);
      return default(string);   
    }
#endif
    [Pure]
    public static string GetName(Type enumType, object value)
    {
      Contract.Requires(enumType != null);
      Contract.Requires(value != null);
      Contract.Ensures(Contract.Result<string>() != null);
      return default(string);
    }

#if !SILVERLIGHT
    [Pure]
    public static string[] GetNames(Type enumType)
    {
      Contract.Requires(enumType != null);
      Contract.Ensures(Contract.Result<string[]>() != null);
      return default(string[]);
    }
#endif

#if NETFRAMEWORK_4_0 || SILVERLIGHT_4_0 || SILVERLIGHT_5_0
    [Pure]
    extern public virtual TypeCode GetTypeCode();
    [Pure]
    extern public bool HasFlag(Enum flag);
#endif
    [Pure]
    extern public static bool IsDefined(Type enumType, object value);
    [Pure]
    public string ToString(string format)
    {
      Contract.Ensures(Contract.Result<string>() != null);
      return default(string);
    }

#if NETFRAMEWORK_4_0 || SILVERLIGHT_4_0 || SILVERLIGHT_5_0
    [Pure]
    extern public static bool TryParse<TEnum>(string value, out TEnum result) where TEnum : struct;
    [Pure]
    extern public static bool TryParse<TEnum>(string value, bool ignoreCase, out TEnum result) where TEnum : struct;
#endif

#if !SILVERLIGHT
    [Pure]
    public static object Parse(Type enumType, string value)
    {
      Contract.Requires(enumType != null);
      Contract.Requires(value != null);

      Contract.Ensures(Contract.Result<object>() != null);

      return default(object);
    }
#endif

    [Pure]
    public static object Parse(Type enumType, string value, bool ignoreCase)
    {
      Contract.Requires(enumType != null);
      Contract.Requires(value != null);

      Contract.Ensures(Contract.Result<object>() != null);

      return default(object);
    }

    [Pure]
    public static Type GetUnderlyingType(Type enumType)
    {
      Contract.Requires(enumType != null);
      Contract.Ensures(Contract.Result<Type>() != null);
      return default(Type);
    }

#if !SILVERLIGHT
    //
    // Summary:
    //     Returns an instance of the specified enumeration type set to the specified
    //     8-bit unsigned integer value.
    //
    // Parameters:
    //   enumType:
    //     The enumeration for which to create a value.
    //
    //   value:
    //     The value to set.
    //
    // Returns:
    //     An instance of the enumeration set to value.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     enumType is null.
    //
    //   System.ArgumentException:
    //     enumType is not an System.Enum.
    [Pure]
    public static Array GetValues(Type enumType)
    {
      Contract.Requires(enumType != null);
      Contract.Ensures(Contract.Result<Array>() != null);
      return default(Array);
    }
#endif

#if !SILVERLIGHT
    //
    // Summary:
    //     Returns an instance of the specified enumeration type set to the specified
    //     32-bit signed integer value.
    //
    // Parameters:
    //   enumType:
    //     The enumeration for which to create a value.
    //
    //   value:
    //     The value to set.
    //
    // Returns:
    //     An instance of the enumeration set to value.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     enumType is null.
    //
    //   System.ArgumentException:
    //     enumType is not an System.Enum.
    [Pure]
    public static object ToObject(Type enumType, byte value)
    {
      Contract.Requires(enumType != null);
      Contract.Ensures(Contract.Result<object>() != null);
      return default(object);
    }

    [Pure]
    public static object ToObject(Type enumType, int value)
    {
      //
      // Summary:
      //     Returns an instance of the specified enumeration type set to the specified
      //     64-bit signed integer value.
      //
      // Parameters:
      //   enumType:
      //     The enumeration for which to create a value.
      //
      //   value:
      //     The value to set.
      //
      // Returns:
      //     An instance of the enumeration set to value.
      //
      // Exceptions:
      //   System.ArgumentNullException:
      //     enumType is null.
      //
      //   System.ArgumentException:
      //     enumType is not an System.Enum.
      Contract.Requires(enumType != null);
      Contract.Ensures(Contract.Result<object>() != null);
      return default(object);
    }

    [Pure]
    public static object ToObject(Type enumType, long value)
    {
      //
      // Summary:
      //     Returns an instance of the specified enumeration set to the specified value.
      //
      // Parameters:
      //   enumType:
      //     An enumeration.
      //
      //   value:
      //     The value.
      //
      // Returns:
      //     An enumeration object whose value is value.
      //
      // Exceptions:
      //   System.ArgumentNullException:
      //     enumType is null.
      //
      //   System.ArgumentException:
      //     enumType is not an System.Enum.-or- value is not type System.SByte, System.Int16,
      //     System.Int32, System.Int64, System.Byte, System.UInt16, System.UInt32, or
      //     System.UInt64.
      Contract.Requires(enumType != null);
      Contract.Ensures(Contract.Result<object>() != null);
      return default(object);
    }
#endif
    [Pure]
    public static object ToObject(Type enumType, object value)
    {
      Contract.Requires(enumType != null);
      Contract.Ensures(Contract.Result<object>() != null);
      return default(object);
    }
#if !SILVERLIGHT
    [Pure]
    public static object ToObject(Type enumType, sbyte value)
    {
      Contract.Requires(enumType != null);
      Contract.Ensures(Contract.Result<object>() != null);
      return default(object);
    }
    [Pure]
    public static object ToObject(Type enumType, short value)
    {
      Contract.Requires(enumType != null);
      Contract.Ensures(Contract.Result<object>() != null);
      return default(object);
    }
    [Pure]
    public static object ToObject(Type enumType, uint value)
    {
      Contract.Requires(enumType != null);
      Contract.Ensures(Contract.Result<object>() != null);
      return default(object);
    }
#endif

#if !SILVERLIGHT
    [Pure]
    public static object ToObject(Type enumType, ulong value)
    {
      Contract.Requires(enumType != null);
      Contract.Ensures(Contract.Result<object>() != null);
      return default(object);
    }
#endif
    //
    // Summary:
    //     Returns an instance of the specified enumeration type set to the specified
    //     16-bit unsigned integer value.
    //
    // Parameters:
    //   enumType:
    //     The enumeration for which to create a value.
    //
    //   value:
    //     The value to set.
    //
    // Returns:
    //     An instance of the enumeration set to value.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     enumType is null.
    //
    //   System.ArgumentException:
    //     enumType is not an System.Enum.
#if !SILVERLIGHT
    [Pure]
    public static object ToObject(Type enumType, ushort value)
    {
      Contract.Requires(enumType != null);
      Contract.Ensures(Contract.Result<object>() != null);
      return default(object);
    }
#endif
  }
}
