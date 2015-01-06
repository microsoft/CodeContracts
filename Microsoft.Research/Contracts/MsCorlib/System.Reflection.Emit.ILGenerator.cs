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

using System.Diagnostics.Contracts;
using System;

namespace System.Reflection.Emit
{

    public class ILGenerator
    {

        public void EndScope () {

        }
        public void BeginScope () {

        }
        public void MarkSequencePoint (System.Diagnostics.SymbolStore.ISymbolDocumentWriter document, int startLine, int startColumn, int endLine, int endColumn) {
            Contract.Requires(startLine != 0);
            Contract.Requires(startLine >= 0);
            Contract.Requires(endLine != 0);
            Contract.Requires(endLine >= 0);

        }
        public void UsingNamespace (string usingNamespace) {
            Contract.Requires(usingNamespace != null);
            Contract.Requires(usingNamespace.Length != 0);

        }
        public LocalBuilder DeclareLocal (Type localType) {
            Contract.Requires(localType != null);

          Contract.Ensures(Contract.Result<LocalBuilder>() != null);
          return default(LocalBuilder);
        }
        public virtual LocalBuilder DeclareLocal(Type localType, bool pinned) {
            Contract.Requires(localType != null);
            Contract.Ensures(Contract.Result<Type>() != null);
          return default(LocalBuilder);
        }
        public void EmitWriteLine (System.Reflection.FieldInfo fld) {
            Contract.Requires(fld != null);

        }
        public void EmitWriteLine (LocalBuilder localBuilder) {

        }
        public void EmitWriteLine (string value) {

        }
        public void ThrowException (Type excType) {
            Contract.Requires(excType != null);

        }
        public void MarkLabel (Label loc) {

        }
        public Label DefineLabel () {

          return default(Label);
        }
        public void BeginFinallyBlock () {

        }
        public void BeginFaultBlock () {

        }
        public void BeginCatchBlock (Type exceptionType) {
            Contract.Requires(exceptionType == null);
            Contract.Requires(exceptionType != null);

        }
        public void BeginExceptFilterBlock () {

        }
        public void EndExceptionBlock () {

        }
        public Label BeginExceptionBlock () {

          return default(Label);
        }
        public void Emit (OpCode opcode, LocalBuilder local) {
            Contract.Requires(local != null);

        }
        public void Emit (OpCode opcode, string str) {

        }
        public void Emit (OpCode opcode, System.Reflection.FieldInfo field) {

        }
        public void Emit (OpCode opcode, Label[] labels) {

        }
        public void Emit (OpCode opcode, Label label) {

        }
        public void Emit (OpCode opcode, double arg) {

        }
        public void Emit (OpCode opcode, Single arg) {

        }
        public void Emit (OpCode opcode, Int64 arg) {

        }
        public void Emit (OpCode opcode, Type cls) {

        }
        public void Emit (OpCode opcode, System.Reflection.ConstructorInfo con) {

        }
        public void Emit (OpCode opcode, SignatureHelper signature) {
            Contract.Requires(signature != null);

        }
        public void EmitCall (OpCode opcode, System.Reflection.MethodInfo methodInfo, Type[] optionalParameterTypes) {
            Contract.Requires(methodInfo != null);

        }
        public void EmitCalli (OpCode opcode, System.Runtime.InteropServices.CallingConvention unmanagedCallConv, Type returnType, Type[] parameterTypes) {

        }
        public void EmitCalli (OpCode opcode, System.Reflection.CallingConventions callingConvention, Type returnType, Type[] parameterTypes, Type[] optionalParameterTypes) {
            Contract.Requires(optionalParameterTypes == null || (int)((int)callingConvention & 2) != 0);

        }
        public void Emit (OpCode opcode, System.Reflection.MethodInfo meth) {
            Contract.Requires(meth != null);

        }
        public void Emit (OpCode opcode, int arg) {

        }
        public void Emit (OpCode opcode, Int16 arg) {

        }
        public void Emit (OpCode opcode, SByte arg) {

        }
        public void Emit (OpCode opcode, byte arg) {

        }
        public void Emit (OpCode opcode) {
        }
    }
}
