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
  //     Marks each type of member that is defined as a derived class of MemberInfo.
  public enum MemberTypes {
    // Summary:
    //     Specifies that the member is a constructor, representing a System.Reflection.ConstructorInfo
    //     member. Hexadecimal value of 0x01.
    Constructor = 1,
    //
    // Summary:
    //     Specifies that the member is an event, representing an System.Reflection.EventInfo
    //     member. Hexadecimal value of 0x02.
    Event = 2,
    //
    // Summary:
    //     Specifies that the member is a field, representing a System.Reflection.FieldInfo
    //     member. Hexadecimal value of 0x04.
    Field = 4,
    //
    // Summary:
    //     Specifies that the member is a method, representing a System.Reflection.MethodInfo
    //     member. Hexadecimal value of 0x08.
    Method = 8,
    //
    // Summary:
    //     Specifies that the member is a property, representing a System.Reflection.PropertyInfo
    //     member. Hexadecimal value of 0x10.
    Property = 16,
    //
    // Summary:
    //     Specifies that the member is a type, representing a System.Reflection.MemberTypes.TypeInfo
    //     member. Hexadecimal value of 0x20.
    TypeInfo = 32,
    //
    // Summary:
    //     Specifies that the member is a custom member type. Hexadecimal value of 0x40.
    Custom = 64,
    //
    // Summary:
    //     Specifies that the member is a nested type, extending System.Reflection.MemberInfo.
    NestedType = 128,
    //
    // Summary:
    //     Specifies all member types.
    All = 191,
  }
}
