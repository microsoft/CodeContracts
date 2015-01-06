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

// File System.Reflection.Emit.ILGenerator.cs
// Automatically generated contract file.
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Diagnostics.Contracts;
using System;

// Disable the "this variable is not used" warning as every field would imply it.
#pragma warning disable 0414
// Disable the "this variable is never assigned to".
#pragma warning disable 0067
// Disable the "this event is never assigned to".
#pragma warning disable 0649
// Disable the "this variable is never used".
#pragma warning disable 0169
// Disable the "new keyword not required" warning.
#pragma warning disable 0109
// Disable the "extern without DllImport" warning.
#pragma warning disable 0626
// Disable the "could hide other member" warning, can happen on certain properties.
#pragma warning disable 0108


namespace System.Reflection.Emit
{
  public partial class ILGenerator : System.Runtime.InteropServices._ILGenerator
  {
    #region Methods and constructors
    public virtual new void BeginCatchBlock(Type exceptionType)
    {
    }

    public virtual new void BeginExceptFilterBlock()
    {
    }

    public virtual new Label BeginExceptionBlock()
    {
      return default(Label);
    }

    public virtual new void BeginFaultBlock()
    {
    }

    public virtual new void BeginFinallyBlock()
    {
    }

    public virtual new void BeginScope()
    {
    }

    public virtual new LocalBuilder DeclareLocal(Type localType)
    {
      return default(LocalBuilder);
    }

    public virtual new LocalBuilder DeclareLocal(Type localType, bool pinned)
    {
      return default(LocalBuilder);
    }

    public virtual new Label DefineLabel()
    {
      return default(Label);
    }

    public virtual new void Emit(OpCode opcode, Label[] labels)
    {
    }

    public virtual new void Emit(OpCode opcode, Label label)
    {
    }

    public virtual new void Emit(OpCode opcode, System.Reflection.FieldInfo field)
    {
    }

    public virtual new void Emit(OpCode opcode, LocalBuilder local)
    {
    }

    public virtual new void Emit(OpCode opcode, string str)
    {
    }

    public virtual new void Emit(OpCode opcode, double arg)
    {
    }

    public virtual new void Emit(OpCode opcode, short arg)
    {
    }

    public virtual new void Emit(OpCode opcode, int arg)
    {
    }

    public virtual new void Emit(OpCode opcode, System.Reflection.MethodInfo meth)
    {
    }

    public virtual new void Emit(OpCode opcode)
    {
    }

    public virtual new void Emit(OpCode opcode, byte arg)
    {
    }

    public void Emit(OpCode opcode, sbyte arg)
    {
    }

    public virtual new void Emit(OpCode opcode, SignatureHelper signature)
    {
    }

    public virtual new void Emit(OpCode opcode, System.Reflection.ConstructorInfo con)
    {
    }

    public virtual new void Emit(OpCode opcode, long arg)
    {
    }

    public virtual new void Emit(OpCode opcode, Type cls)
    {
    }

    public virtual new void Emit(OpCode opcode, float arg)
    {
    }

    public virtual new void EmitCall(OpCode opcode, System.Reflection.MethodInfo methodInfo, Type[] optionalParameterTypes)
    {
    }

    public virtual new void EmitCalli(OpCode opcode, System.Reflection.CallingConventions callingConvention, Type returnType, Type[] parameterTypes, Type[] optionalParameterTypes)
    {
    }

    public virtual new void EmitCalli(OpCode opcode, System.Runtime.InteropServices.CallingConvention unmanagedCallConv, Type returnType, Type[] parameterTypes)
    {
    }

    public virtual new void EmitWriteLine(System.Reflection.FieldInfo fld)
    {
    }

    public virtual new void EmitWriteLine(LocalBuilder localBuilder)
    {
      Contract.Requires(localBuilder != null);
    }

    public virtual new void EmitWriteLine(string value)
    {
    }

    public virtual new void EndExceptionBlock()
    {
    }

    public virtual new void EndScope()
    {
    }

    internal ILGenerator()
    {
    }

    public virtual new void MarkLabel(Label loc)
    {
    }

    public virtual new void MarkSequencePoint(System.Diagnostics.SymbolStore.ISymbolDocumentWriter document, int startLine, int startColumn, int endLine, int endColumn)
    {
    }

    void System.Runtime.InteropServices._ILGenerator.GetIDsOfNames(ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
    {
    }

    void System.Runtime.InteropServices._ILGenerator.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
    {
    }

    void System.Runtime.InteropServices._ILGenerator.GetTypeInfoCount(out uint pcTInfo)
    {
      pcTInfo = default(uint);
    }

    void System.Runtime.InteropServices._ILGenerator.Invoke(uint dispIdMember, ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
    {
    }

    public virtual new void ThrowException(Type excType)
    {
    }

    public virtual new void UsingNamespace(string usingNamespace)
    {
    }
    #endregion

    #region Properties and indexers
    public virtual new int ILOffset
    {
      get
      {
        return default(int);
      }
    }
    #endregion
  }
}
