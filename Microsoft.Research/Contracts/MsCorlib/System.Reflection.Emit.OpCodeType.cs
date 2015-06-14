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
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
  // Summary:
  //     Describes the types of the Microsoft intermediate language (MSIL) instructions.
  public enum OpCodeType
  {
    // Summary:
    //     This enumerator value is reserved and should not be used.
    Annotation = 0,
    //
    // Summary:
    //     These are Microsoft intermediate language (MSIL) instructions that are used
    //     as a synonym for other MSIL instructions. For example, ldarg.0 represents
    //     the ldarg instruction with an argument of 0.
    Macro = 1,
    //
    // Summary:
    //     Describes a reserved Microsoft intermediate language (MSIL) instruction.
    Nternal = 2,
    //
    // Summary:
    //     Describes a Microsoft intermediate language (MSIL) instruction that applies
    //     to objects.
    Objmodel = 3,
    //
    // Summary:
    //     Describes a prefix instruction that modifies the behavior of the following
    //     instruction.
    Prefix = 4,
    //
    // Summary:
    //     Describes a built-in instruction.
    Primitive = 5,
  }
}
