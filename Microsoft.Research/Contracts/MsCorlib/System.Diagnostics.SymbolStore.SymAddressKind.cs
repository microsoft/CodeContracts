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

namespace System.Diagnostics.SymbolStore {
  // Summary:
  //     Specifies address types for local variables, parameters, and fields in the
  //     methods System.Diagnostics.SymbolStore.ISymbolWriter.DefineLocalVariable(System.String,System.Reflection.FieldAttributes,System.Byte[],System.Diagnostics.SymbolStore.SymAddressKind,System.Int32,System.Int32,System.Int32,System.Int32,System.Int32),
  //     System.Diagnostics.SymbolStore.ISymbolWriter.DefineParameter(System.String,System.Reflection.ParameterAttributes,System.Int32,System.Diagnostics.SymbolStore.SymAddressKind,System.Int32,System.Int32,System.Int32),
  //     and System.Diagnostics.SymbolStore.ISymbolWriter.DefineField(System.Diagnostics.SymbolStore.SymbolToken,System.String,System.Reflection.FieldAttributes,System.Byte[],System.Diagnostics.SymbolStore.SymAddressKind,System.Int32,System.Int32,System.Int32)
  //     of the System.Diagnostics.SymbolStore.ISymbolWriter interface.
  [Serializable]
  [ComVisible(true)]
  public enum SymAddressKind {
    // Summary:
    //     A Microsoft intermediate language (MSIL) offset. The addr1 parameter is the
    //     MSIL local variable or parameter index.
    ILOffset = 1,
    //
    // Summary:
    //     A native Relevant Virtual Address (RVA). The addr1 parameter is the RVA in
    //     the module.
    NativeRVA = 2,
    //
    // Summary:
    //     A native register address. The addr1 parameter is the register in which the
    //     variable is stored.
    NativeRegister = 3,
    //
    // Summary:
    //     A register-relative address. The addr1 parameter is the register, and the
    //     addr2 parameter is the offset.
    NativeRegisterRelative = 4,
    //
    // Summary:
    //     A native offset. The addr1 parameter is the offset from the start of the
    //     parent.
    NativeOffset = 5,
    //
    // Summary:
    //     A register-relative address. The addr1 parameter is the low-order register,
    //     and the addr2 parameter is the high-order register.
    NativeRegisterRegister = 6,
    //
    // Summary:
    //     A register-relative address. The addr1 parameter is the low-order register,
    //     the addr2 parameter is the stack register, and the addr3 parameter is the
    //     offset from the stack pointer to the high-order part of the value.
    NativeRegisterStack = 7,
    //
    // Summary:
    //     A register-relative address. The addr1 parameter is the stack register, the
    //     addr2 parameter is the offset from the stack pointer to the low-order part
    //     of the value, and the addr3 parameter is the high-order register.
    NativeStackRegister = 8,
    //
    // Summary:
    //     A bit field. The addr1 parameter is the position where the field starts,
    //     and the addr2 parameter is the field length.
    BitField = 9,
    //
    // Summary:
    //     A native section offset. The addr1 parameter is the section, and the addr2
    //     parameter is the offset.
    NativeSectionOffset = 10,
  }
}
