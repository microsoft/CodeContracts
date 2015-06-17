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
  //     Describes the operand type of Microsoft intermediate language (MSIL) instruction.
  public enum OperandType
  {
    // Summary:
    //     The operand is a 32-bit integer branch target.
    InlineBrTarget = 0,
    //
    // Summary:
    //     The operand is a 32-bit metadata token.
    InlineField = 1,
    //
    // Summary:
    //     The operand is a 32-bit integer.
    InlineI = 2,
    //
    // Summary:
    //     The operand is a 64-bit integer.
    InlineI8 = 3,
    //
    // Summary:
    //     The operand is a 32-bit metadata token.
    InlineMethod = 4,
    //
    // Summary:
    //     No operand.
    InlineNone = 5,
    //
    // Summary:
    //     The operand is reserved and should not be used.
    InlinePhi = 6,
    //
    // Summary:
    //     The operand is a 64-bit IEEE floating point number.
    InlineR = 7,
    //
    // Summary:
    //     The operand is a 32-bit metadata signature token.
    InlineSig = 9,
    //
    // Summary:
    //     The operand is a 32-bit metadata string token.
    InlineString = 10,
    //
    // Summary:
    //     The operand is the 32-bit integer argument to a switch instruction.
    InlineSwitch = 11,
    //
    // Summary:
    //     The operand is a FieldRef, MethodRef, or TypeRef token.
    InlineTok = 12,
    //
    // Summary:
    //     The operand is a 32-bit metadata token.
    InlineType = 13,
    //
    // Summary:
    //     The operand is 16-bit integer containing the ordinal of a local variable
    //     or an argument.
    InlineVar = 14,
    //
    // Summary:
    //     The operand is an 8-bit integer branch target.
    ShortInlineBrTarget = 15,
    //
    // Summary:
    //     The operand is an 8-bit integer.
    ShortInlineI = 16,
    //
    // Summary:
    //     The operand is a 32-bit IEEE floating point number.
    ShortInlineR = 17,
    //
    // Summary:
    //     The operand is an 8-bit integer containing the ordinal of a local variable
    //     or an argumenta.
    ShortInlineVar = 18,
  }
}
