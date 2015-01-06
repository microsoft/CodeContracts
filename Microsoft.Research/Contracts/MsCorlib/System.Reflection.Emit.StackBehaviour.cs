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

namespace System.Reflection.Emit {
  // Summary:
  //     Describes how values are pushed onto a stack or popped off a stack.
  [Serializable]
  [ComVisible(true)]
  public enum StackBehaviour {
    // Summary:
    //     No values are popped off the stack.
    Pop0 = 0,
    //
    // Summary:
    //     Pops one value off the stack.
    Pop1 = 1,
    //
    // Summary:
    //     Pops 1 value off the stack for the first operand, and 1 value of the stack
    //     for the second operand.
    Pop1_pop1 = 2,
    //
    // Summary:
    //     Pops a 32-bit integer off the stack.
    Popi = 3,
    //
    // Summary:
    //     Pops a 32-bit integer off the stack for the first operand, and a value off
    //     the stack for the second operand.
    Popi_pop1 = 4,
    //
    // Summary:
    //     Pops a 32-bit integer off the stack for the first operand, and a 32-bit integer
    //     off the stack for the second operand.
    Popi_popi = 5,
    //
    // Summary:
    //     Pops a 32-bit integer off the stack for the first operand, and a 64-bit integer
    //     off the stack for the second operand.
    Popi_popi8 = 6,
    //
    // Summary:
    //     Pops a 32-bit integer off the stack for the first operand, a 32-bit integer
    //     off the stack for the second operand, and a 32-bit integer off the stack
    //     for the third operand.
    Popi_popi_popi = 7,
    //
    // Summary:
    //     Pops a 32-bit integer off the stack for the first operand, and a 32-bit floating
    //     point number off the stack for the second operand.
    Popi_popr4 = 8,
    //
    // Summary:
    //     Pops a 32-bit integer off the stack for the first operand, and a 64-bit floating
    //     point number off the stack for the second operand.
    Popi_popr8 = 9,
    //
    // Summary:
    //     Pops a reference off the stack.
    Popref = 10,
    //
    // Summary:
    //     Pops a reference off the stack for the first operand, and a value off the
    //     stack for the second operand.
    Popref_pop1 = 11,
    //
    // Summary:
    //     Pops a reference off the stack for the first operand, and a 32-bit integer
    //     off the stack for the second operand.
    Popref_popi = 12,
    //
    // Summary:
    //     Pops a reference off the stack for the first operand, a value off the stack
    //     for the second operand, and a value off the stack for the third operand.
    Popref_popi_popi = 13,
    //
    // Summary:
    //     Pops a reference off the stack for the first operand, a value off the stack
    //     for the second operand, and a 64-bit integer off the stack for the third
    //     operand.
    Popref_popi_popi8 = 14,
    //
    // Summary:
    //     Pops a reference off the stack for the first operand, a value off the stack
    //     for the second operand, and a 32-bit integer off the stack for the third
    //     operand.
    Popref_popi_popr4 = 15,
    //
    // Summary:
    //     Pops a reference off the stack for the first operand, a value off the stack
    //     for the second operand, and a 64-bit floating point number off the stack
    //     for the third operand.
    Popref_popi_popr8 = 16,
    //
    // Summary:
    //     Pops a reference off the stack for the first operand, a value off the stack
    //     for the second operand, and a reference off the stack for the third operand.
    Popref_popi_popref = 17,
    //
    // Summary:
    //     No values are pushed onto the stack.
    Push0 = 18,
    //
    // Summary:
    //     Pushes one value onto the stack.
    Push1 = 19,
    //
    // Summary:
    //     Pushes 1 value onto the stack for the first operand, and 1 value onto the
    //     stack for the second operand.
    Push1_push1 = 20,
    //
    // Summary:
    //     Pushes a 32-bit integer onto the stack.
    Pushi = 21,
    //
    // Summary:
    //     Pushes a 64-bit integer onto the stack.
    Pushi8 = 22,
    //
    // Summary:
    //     Pushes a 32-bit floating point number onto the stack.
    Pushr4 = 23,
    //
    // Summary:
    //     Pushes a 64-bit floating point number onto the stack.
    Pushr8 = 24,
    //
    // Summary:
    //     Pushes a reference onto the stack.
    Pushref = 25,
    //
    // Summary:
    //     Pops a variable off the stack.
    Varpop = 26,
    //
    // Summary:
    //     Pushes a variable onto the stack.
    Varpush = 27,
    //
    // Summary:
    //     Pops a reference off the stack for the first operand, a value off the stack
    //     for the second operand, and a 32-bit integer off the stack for the third
    //     operand.
    Popref_popi_pop1 = 28,
  }
}
