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
    public struct Enum
    {

        public static object Parse(Type! enumType, string! value) {
            CodeContract.Requires(enumType != null);
            CodeContract.Requires(value != null);
            
          return default(object);
        }
        public static object Parse(Type! enumType, string! value, bool ignoreCase) {
            CodeContract.Requires(enumType != null);
            CodeContract.Requires(value != null);
            
          return default(object);
        }
        public static Type GetUnderlyingType(Type enumType) {
        
      //
      // Summary:
      //     Retrieves an array of the values of the constants in a specified enumeration.
      //
      // Parameters:
      //   enumType:
      //     An enumeration type.
      //
      // Returns:
      //     An System.Array of the values of the constants in enumType. The elements
      //     of the array are sorted by the values of the enumeration constants.
      //
      // Exceptions:
      //   System.ArgumentNullException:
      //     enumType is null.
      //
      //   System.ArgumentException:
      //     enumType is not an System.Enum.
          CodeContract.Ensures(CodeContract.Result<Type>() != null);
          return default(Type);
        }
      [ComVisible(true)]
      public static Array GetValues(Type enumType) {
          
            
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
        CodeContract.Ensures(CodeContract.Result<Array>() != null);
        return default(Array);
      }
    public static object ToObject(Type! enumType, byte value) {
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
      CodeContract.Ensures(CodeContract.Result<object>() != null);
      return default(object);
    }
    public static object ToObject(Type! enumType, int value) {
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
      CodeContract.Ensures(CodeContract.Result<object>() != null);
      return default(object);
    }
    public static object ToObject(Type! enumType, long value) {
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
      CodeContract.Ensures(CodeContract.Result<object>() != null);
      return default(object);
    }
    public static object ToObject(Type! enumType, object value) {
    //
    // Summary:
    //     Returns an instance of the specified enumeration type set to the specified
    //     8-bit signed integer value.
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
    [ComVisible(true)]
      CodeContract.Ensures(CodeContract.Result<object>() != null);
      return default(object);
    }
    [CLSCompliant(false)]
    public static object ToObject(Type enumType, sbyte value) {
    //
    // Summary:
    //     Returns an instance of the specified enumeration type set to the specified
    //     16-bit signed integer value.
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
      return default(object);
    }
    public static object ToObject(Type! enumType, short value) {
    //
    // Summary:
    //     Returns an instance of the specified enumeration type set to the specified
    //     32-bit unsigned integer value.
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
      CodeContract.Ensures(CodeContract.Result<object>() != null);
      return default(object);
    }
    public static object ToObject(Type! enumType, uint value) {
    //
    // Summary:
    //     Returns an instance of the specified enumeration type set to the specified
    //     64-bit unsigned integer value.
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
    [CLSCompliant(false)]
      CodeContract.Ensures(CodeContract.Result<object>() != null);
      return default(object);
    }
    [ComVisible(true)]
    public static object ToObject(Type! enumType, ulong value) {
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
    [CLSCompliant(false)]
      CodeContract.Ensures(CodeContract.Result<object>() != null);
      return default(object);
    }
    [ComVisible(true)]
    public static object ToObject(Type! enumType, ushort value) {
    
      CodeContract.Ensures(CodeContract.Result<object>() != null);
      return default(object);
    }
    public string ToString() {

      CodeContract.Ensures(CodeContract.Result<string>() != null);
      return default(string);
    }
    }
}
