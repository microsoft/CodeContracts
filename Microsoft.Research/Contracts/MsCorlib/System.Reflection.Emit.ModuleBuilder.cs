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

    public class ModuleBuilder
    {

        public string FullyQualifiedName
        {
          get;
        }

        public void DefineUnmanagedResource (string resourceFileName) {
            Contract.Requires(resourceFileName != null);

        }
        public void DefineUnmanagedResource (Byte[] resource) {
            Contract.Requires(resource != null);

        }
        public bool IsTransient () {

          return default(bool);
        }
        public void CreateGlobalFunctions () {

        }
        public MethodBuilder DefinePInvokeMethod (string name, string dllName, string entryName, System.Reflection.MethodAttributes attributes, System.Reflection.CallingConventions callingConvention, Type returnType, Type[] parameterTypes, System.Runtime.InteropServices.CallingConvention nativeCallConv, System.Runtime.InteropServices.CharSet nativeCharSet) {

          return default(MethodBuilder);
        }
        public MethodBuilder DefinePInvokeMethod (string name, string dllName, System.Reflection.MethodAttributes attributes, System.Reflection.CallingConventions callingConvention, Type returnType, Type[] parameterTypes, System.Runtime.InteropServices.CallingConvention nativeCallConv, System.Runtime.InteropServices.CharSet nativeCharSet) {

          return default(MethodBuilder);
        }
        public void SetSymCustomAttribute (string name, Byte[] data) {

        }
        public System.Diagnostics.SymbolStore.ISymbolDocumentWriter DefineDocument (string url, Guid language, Guid languageVendor, Guid documentType) {

          return default(System.Diagnostics.SymbolStore.ISymbolDocumentWriter);
        }
        public void SetUserEntryPoint (System.Reflection.MethodInfo entryPoint) {

        }
        public System.Diagnostics.SymbolStore.ISymbolWriter GetSymWriter () {

          return default(System.Diagnostics.SymbolStore.ISymbolWriter);
        }
        public MethodToken GetConstructorToken (System.Reflection.ConstructorInfo con) {

          return default(MethodToken);
        }
        public SignatureToken GetSignatureToken (Byte[] sigBytes, int sigLength) {

          return default(SignatureToken);
        }
        public SignatureToken GetSignatureToken (SignatureHelper sigHelper) {
            Contract.Requires(sigHelper != null);

          return default(SignatureToken);
        }
        public void SetCustomAttribute (CustomAttributeBuilder customBuilder) {
            Contract.Requires(customBuilder != null);

        }
        public void SetCustomAttribute (System.Reflection.ConstructorInfo con, Byte[] binaryAttribute) {
            Contract.Requires(con != null);
            Contract.Requires(binaryAttribute != null);

        }
        public StringToken GetStringConstant (string str) {

          return default(StringToken);
        }
        public FieldToken GetFieldToken (System.Reflection.FieldInfo field) {

          return default(FieldToken);
        }
        public System.Reflection.MethodInfo GetArrayMethod (Type arrayClass, string methodName, System.Reflection.CallingConventions callingConvention, Type returnType, Type[] parameterTypes) {

          return default(System.Reflection.MethodInfo);
        }
        public MethodToken GetArrayMethodToken (Type arrayClass, string methodName, System.Reflection.CallingConventions callingConvention, Type returnType, Type[] parameterTypes) {

          return default(MethodToken);
        }
        public MethodToken GetMethodToken (System.Reflection.MethodInfo method) {

          return default(MethodToken);
        }
        public TypeToken GetTypeToken (string name) {

          return default(TypeToken);
        }
        public TypeToken GetTypeToken (Type type) {

          return default(TypeToken);
        }
        public FieldBuilder DefineUninitializedData (string name, int size, System.Reflection.FieldAttributes attributes) {

          return default(FieldBuilder);
        }
        public FieldBuilder DefineInitializedData (string name, Byte[] data, System.Reflection.FieldAttributes attributes) {

          return default(FieldBuilder);
        }
        public MethodBuilder DefineGlobalMethod (string name, System.Reflection.MethodAttributes attributes, System.Reflection.CallingConventions callingConvention, Type returnType, Type[] parameterTypes) {

          return default(MethodBuilder);
        }
        public MethodBuilder DefineGlobalMethod (string name, System.Reflection.MethodAttributes attributes, Type returnType, Type[] parameterTypes) {

          return default(MethodBuilder);
        }
        public System.Resources.IResourceWriter DefineResource (string name, string description, System.Reflection.ResourceAttributes attribute) {

          return default(System.Resources.IResourceWriter);
        }
        public System.Resources.IResourceWriter DefineResource (string name, string description) {

          return default(System.Resources.IResourceWriter);
        }
        public EnumBuilder DefineEnum (string name, System.Reflection.TypeAttributes visibility, Type underlyingType) {

          return default(EnumBuilder);
        }
        public TypeBuilder DefineType (string name, System.Reflection.TypeAttributes attr, Type parent, PackingSize packsize) {

          return default(TypeBuilder);
        }
        public TypeBuilder DefineType (string name, System.Reflection.TypeAttributes attr, Type parent, int typesize) {

          return default(TypeBuilder);
        }
        public TypeBuilder DefineType (string name, System.Reflection.TypeAttributes attr, Type parent, PackingSize packingSize, int typesize) {

          return default(TypeBuilder);
        }
        public TypeBuilder DefineType (string name, System.Reflection.TypeAttributes attr) {

          return default(TypeBuilder);
        }
        public TypeBuilder DefineType (string name, System.Reflection.TypeAttributes attr, Type parent) {

          return default(TypeBuilder);
        }
        public TypeBuilder DefineType (string name, System.Reflection.TypeAttributes attr, Type parent, Type[] interfaces) {

          return default(TypeBuilder);
        }
        public TypeBuilder DefineType (string name) {

          return default(TypeBuilder);
        }
        public Type[] GetTypes () {

          return default(Type[]);
        }
        public Type GetType (string className, bool throwOnError, bool ignoreCase) {

          return default(Type);
        }
        public Type GetType (string className, bool ignoreCase) {

          return default(Type);
        }
        public Type GetType (string className) {
          return default(Type);
        }
    }
}
