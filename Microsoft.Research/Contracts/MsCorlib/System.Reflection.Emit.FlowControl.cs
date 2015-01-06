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
  //     Describes how an instruction alters the flow of control.
  public enum FlowControl
  {
    // Summary:
    //     Branch instruction.
    Branch = 0,
    //
    // Summary:
    //     Break instruction.
    Break = 1,
    //
    // Summary:
    //     Call instruction.
    Call = 2,
    //
    // Summary:
    //     Conditional branch instruction.
    Cond_Branch = 3,
    //
    // Summary:
    //     Provides information about a subsequent instruction. For example, the Unaligned
    //     instruction of Reflection.Emit.Opcodes has FlowControl.Meta and specifies
    //     that the subsequent pointer instruction might be unaligned.
    Meta = 4,
    //
    // Summary:
    //     Normal flow of control.
    Next = 5,
    //
    // Summary:
    //     This enumerator value is reserved and should not be used.
    Phi = 6,
    //
    // Summary:
    //     Return instruction.
    Return = 7,
    //
    // Summary:
    //     Exception throw instruction.
    Throw = 8,
  }
}
