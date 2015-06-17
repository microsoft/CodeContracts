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
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
namespace System.Runtime.InteropServices {
  // Summary:
  //     Identifies how to marshal parameters or fields to unmanaged code.
  [Serializable]
  [ComVisible(true)]
  public enum UnmanagedType {
    // Summary:
    //     A 4-byte Boolean value (true != 0, false = 0). This is the Win32 BOOL type.
    Bool = 2,
    //
    // Summary:
    //     A 1-byte signed integer. You can use this member to transform a Boolean value
    //     into a 1-byte, C-style bool (true = 1, false = 0).
    I1 = 3,
    //
    // Summary:
    //     A 1-byte unsigned integer.
    U1 = 4,
    //
    // Summary:
    //     A 2-byte signed integer.
    I2 = 5,
    //
    // Summary:
    //     A 2-byte unsigned integer.
    U2 = 6,
    //
    // Summary:
    //     A 4-byte signed integer.
    I4 = 7,
    //
    // Summary:
    //     A 4-byte unsigned integer.
    U4 = 8,
    //
    // Summary:
    //     An 8-byte signed integer.
    I8 = 9,
    //
    // Summary:
    //     An 8-byte unsigned integer.
    U8 = 10,
    //
    // Summary:
    //     A 4-byte floating point number.
    R4 = 11,
    //
    // Summary:
    //     An 8-byte floating point number.
    R8 = 12,
    //
    // Summary:
    //     Used on a System.Decimal to marshal the decimal value as a COM currency type
    //     instead of as a Decimal.
    Currency = 15,
    //
    // Summary:
    //     A Unicode character string that is a length-prefixed double byte. You can
    //     use this member, which is the default string in COM, on the System.String
    //     data type.
    BStr = 19,
    //
    // Summary:
    //     A single byte, null-terminated ANSI character string. You can use this member
    //     on the System.String or System.Text.StringBuilder data types
    LPStr = 20,
    //
    // Summary:
    //     A 2-byte, null-terminated Unicode character string.
    LPWStr = 21,
    //
    // Summary:
    //     A platform-dependent character string: ANSI on Windows 98 and Unicode on
    //     Windows NT and Windows XP. This value is only supported for platform invoke,
    //     and not COM interop, because exporting a string of type LPTStr is not supported.
    LPTStr = 22,
    //
    // Summary:
    //     Used for in-line, fixed-length character arrays that appear within a structure.
    //     The character type used with System.Runtime.InteropServices.UnmanagedType.ByValTStr
    //     is determined by the System.Runtime.InteropServices.CharSet argument of the
    //     System.Runtime.InteropServices.StructLayoutAttribute applied to the containing
    //     structure. Always use the System.Runtime.InteropServices.MarshalAsAttribute.SizeConst
    //     field to indicate the size of the array.
    ByValTStr = 23,
    //
    // Summary:
    //     A COM IUnknown pointer. You can use this member on the System.Object data
    //     type.
    IUnknown = 25,
    //
    // Summary:
    //     A COM IDispatch pointer (Object in Microsoft Visual Basic 6.0).
    IDispatch = 26,
    //
    // Summary:
    //     A VARIANT, which is used to marshal managed formatted classes and value types.
    Struct = 27,
    //
    // Summary:
    //     A COM interface pointer. The System.Guid of the interface is obtained from
    //     the class metadata. Use this member to specify the exact interface type or
    //     the default interface type if you apply it to a class. This member produces
    //     System.Runtime.InteropServices.UnmanagedType.IUnknown behavior when you apply
    //     it to the System.Object data type.
    Interface = 28,
    //
    // Summary:
    //     A SafeArray is a self-describing array that carries the type, rank, and bounds
    //     of the associated array data. You can use this member with the System.Runtime.InteropServices.MarshalAsAttribute.SafeArraySubType
    //     field to override the default element type.
    SafeArray = 29,
    //
    // Summary:
    //     When System.Runtime.InteropServices.MarshalAsAttribute.Value is set to ByValArray,
    //     the System.Runtime.InteropServices.MarshalAsAttribute.SizeConst must be set
    //     to indicate the number of elements in the array. The System.Runtime.InteropServices.MarshalAsAttribute.ArraySubType
    //     field can optionally contain the System.Runtime.InteropServices.UnmanagedType
    //     of the array elements when it is necessary to differentiate among string
    //     types. You can only use this System.Runtime.InteropServices.UnmanagedType
    //     on an array that appear as fields in a structure.
    ByValArray = 30,
    //
    // Summary:
    //     A platform-dependent, signed integer. 4-bytes on 32 bit Windows, 8-bytes
    //     on 64 bit Windows.
    SysInt = 31,
    //
    // Summary:
    //     A platform-dependent, unsigned integer. 4-bytes on 32 bit Windows, 8-bytes
    //     on 64 bit Windows.
    SysUInt = 32,
    //
    // Summary:
    //     Allows Visual Basic 2005 to change a string in unmanaged code, and have the
    //     results reflected in managed code. This value is only supported for platform
    //     invoke.
    VBByRefStr = 34,
    //
    // Summary:
    //     An ANSI character string that is a length prefixed, single byte. You can
    //     use this member on the System.String data type.
    AnsiBStr = 35,
    //
    // Summary:
    //     A length-prefixed, platform-dependent char string. ANSI on Windows 98, Unicode
    //     on Windows NT. You rarely use this BSTR-like member.
    TBStr = 36,
    //
    // Summary:
    //     A 2-byte, OLE-defined VARIANT_BOOL type (true = -1, false = 0).
    VariantBool = 37,
    //
    // Summary:
    //     An integer that can be used as a C-style function pointer. You can use this
    //     member on a System.Delegate data type or a type that inherits from a System.Delegate.
    FunctionPtr = 38,
    //
    // Summary:
    //     A dynamic type that determines the type of an object at run time and marshals
    //     the object as that type. Valid for platform invoke methods only.
    AsAny = 40,
    //
    // Summary:
    //     A pointer to the first element of a C-style array. When marshaling from managed
    //     to unmanaged, the length of the array is determined by the length of the
    //     managed array. When marshaling from unmanaged to managed, the length of the
    //     array is determined from the System.Runtime.InteropServices.MarshalAsAttribute.SizeConst
    //     and the System.Runtime.InteropServices.MarshalAsAttribute.SizeParamIndex
    //     fields, optionally followed by the unmanaged type of the elements within
    //     the array when it is necessary to differentiate among string types.
    LPArray = 42,
    //
    // Summary:
    //     A pointer to a C-style structure that you use to marshal managed formatted
    //     classes. Valid for platform invoke methods only.
    LPStruct = 43,
    //
    // Summary:
    //     Specifies the custom marshaler class when used with System.Runtime.InteropServices.MarshalAsAttribute.MarshalType
    //     or System.Runtime.InteropServices.MarshalAsAttribute.MarshalTypeRef. The
    //     System.Runtime.InteropServices.MarshalAsAttribute.MarshalCookie field can
    //     be used to pass additional information to the custom marshaler. You can use
    //     this member on any reference type.
    CustomMarshaler = 44,
    //
    // Summary:
    //     This native type associated with an System.Runtime.InteropServices.UnmanagedType.I4
    //     or a System.Runtime.InteropServices.UnmanagedType.U4 causes the parameter
    //     to be exported as a HRESULT in the exported type library.
    Error = 45,
  }
}