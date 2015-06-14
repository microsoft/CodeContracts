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
using System.Runtime.InteropServices;

namespace System.Reflection {
  // Summary:
  //     Specifies flags that describe the attributes of a field.
  public enum FieldAttributes {
    // Summary:
    //     Specifies that the field cannot be referenced.
    PrivateScope = 0,
    //
    // Summary:
    //     Specifies that the field is accessible only by the parent type.
    Private = 1,
    //
    // Summary:
    //     Specifies that the field is accessible only by subtypes in this assembly.
    FamANDAssem = 2,
    //
    // Summary:
    //     Specifies that the field is accessible throughout the assembly.
    Assembly = 3,
    //
    // Summary:
    //     Specifies that the field is accessible only by type and subtypes.
    Family = 4,
    //
    // Summary:
    //     Specifies that the field is accessible by subtypes anywhere, as well as throughout
    //     this assembly.
    FamORAssem = 5,
    //
    // Summary:
    //     Specifies that the field is accessible by any member for whom this scope
    //     is visible.
    Public = 6,
    //
    // Summary:
    //     Specifies the access level of a given field.
    FieldAccessMask = 7,
    //
    // Summary:
    //     Specifies that the field represents the defined type, or else it is per-instance.
    Static = 16,
    //
    // Summary:
    //     Specifies that the field is initialized only, and cannot be written after
    //     initialization.
    InitOnly = 32,
    //
    // Summary:
    //     Specifies that the field's value is a compile-time (static or early bound)
    //     constant. The field can be set only from a constructor; any other attempt
    //     to set it throws System.FieldAccessException.
    Literal = 64,
    //
    // Summary:
    //     Specifies that the field does not have to be serialized when the type is
    //     remoted.
    NotSerialized = 128,
    //
    // Summary:
    //     Specifies that the field has a relative virtual address (RVA). The RVA is
    //     the location of the method body in the current image, as an address relative
    //     to the start of the image file in which it is located.
    HasFieldRVA = 256,
    //
    // Summary:
    //     Specifies a special method, with the name describing how the method is special.
    SpecialName = 512,
    //
    // Summary:
    //     Specifies that the common language runtime (metadata internal APIs) should
    //     check the name encoding.
    RTSpecialName = 1024,
    //
    // Summary:
    //     Specifies that the field has marshaling information.
    HasFieldMarshal = 4096,
    //
    // Summary:
    //     Reserved for future use.
    PinvokeImpl = 8192,
    //
    // Summary:
    //     Specifies that the field has a default value.
    HasDefault = 32768,
    //
    // Summary:
    //     Reserved.
    ReservedMask = 38144,
  }
}
