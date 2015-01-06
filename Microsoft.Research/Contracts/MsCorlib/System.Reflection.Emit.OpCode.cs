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
using System.Runtime.InteropServices;
using System.Diagnostics.Contracts;

namespace System.Reflection.Emit
{
  // Summary:
  //     Describes a Microsoft intermediate language (MSIL) instruction.
  public struct OpCode
  {

    // Summary:
    //     Indicates whether two System.Reflection.Emit.OpCode structures are not equal.
    //
    // Parameters:
    //   a:
    //     The System.Reflection.Emit.OpCode to compare to b.
    //
    //   b:
    //     The System.Reflection.Emit.OpCode to compare to a.
    //
    // Returns:
    //     true if a is not equal to b; otherwise, false.
    extern public static bool operator !=(OpCode a, OpCode b);
    //
    // Summary:
    //     Indicates whether two System.Reflection.Emit.OpCode structures are equal.
    //
    // Parameters:
    //   a:
    //     The System.Reflection.Emit.OpCode to compare to b.
    //
    //   b:
    //     The System.Reflection.Emit.OpCode to compare to a.
    //
    // Returns:
    //     true if a is equal to b; otherwise, false.
    extern public static bool operator ==(OpCode a, OpCode b);

    // Summary:
    //     The flow control characteristics of the Microsoft intermediate language (MSIL)
    //     instruction.
    //
    // Returns:
    //     Read-only. The type of flow control.
    public FlowControl FlowControl { get; }
    //
    // Summary:
    //     The name of the Microsoft intermediate language (MSIL) instruction.
    //
    // Returns:
    //     Read-only. The name of the MSIL instruction.
    public string Name { get { return default(string); } }
    //
    // Summary:
    //     The type of Microsoft intermediate language (MSIL) instruction.
    //
    // Returns:
    //     Read-only. The type of Microsoft intermediate language (MSIL) instruction.
    public OpCodeType OpCodeType { get { return default(OpCodeType); } }
    //
    // Summary:
    //     The operand type of an Microsoft intermediate language (MSIL) instruction.
    //
    // Returns:
    //     Read-only. The operand type of an MSIL instruction.
    public OperandType OperandType { get { return default(OperandType); } }
    //
    // Summary:
    //     The size of the Microsoft intermediate language (MSIL) instruction.
    //
    // Returns:
    //     Read-only. The size of the MSIL instruction.
    public int Size { get { return default(int); } }
    //
    // Summary:
    //     How the Microsoft intermediate language (MSIL) instruction pops the stack.
    //
    // Returns:
    //     Read-only. The way the MSIL instruction pops the stack.
    public StackBehaviour StackBehaviourPop { get { return default(StackBehaviour); } }
    //
    // Summary:
    //     How the Microsoft intermediate language (MSIL) instruction pushes operand
    //     onto the stack.
    //
    // Returns:
    //     Read-only. The way the MSIL instruction pushes operand onto the stack.
    public StackBehaviour StackBehaviourPush { get { return default(StackBehaviour); } }
    //
    // Summary:
    //     The value of the immediate operand of the Microsoft intermediate language
    //     (MSIL) instruction.
    //
    // Returns:
    //     Read-only. The value of the immediate operand of the MSIL instruction.
    public short Value { get { return default(short); } }

    //
    // Summary:
    //     Indicates whether the current instance is equal to the specified System.Reflection.Emit.OpCode.
    //
    // Parameters:
    //   obj:
    //     The System.Reflection.Emit.OpCode to compare to the current instance.
    //
    // Returns:
    //     true if the value of obj is equal to the value of the current instance; otherwise,
    //     false.
    extern public bool Equals(OpCode obj);
  }
}
