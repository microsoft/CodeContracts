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
using System.Diagnostics.Contracts;

namespace System.Reflection.Emit
{

    public class MethodBuilder
    {

        public Type ReflectedType
        {
          get;
        }

        public System.Reflection.MethodAttributes Attributes
        {
          get;
        }

        public string Signature
        {
          get;
        }

        public RuntimeMethodHandle MethodHandle
        {
          get;
        }

        public Type DeclaringType
        {
          get;
        }

        public Type ReturnType
        {
          get;
        }

        public bool InitLocals
        {
          get;
          set;
        }

        public System.Reflection.ICustomAttributeProvider ReturnTypeCustomAttributes
        {
          get;
        }

        public System.Reflection.CallingConventions CallingConvention
        {
          get;
        }

        public string Name
        {
          get;
        }

        public void SetCustomAttribute (CustomAttributeBuilder! customBuilder) {
            CodeContract.Requires(customBuilder != null);

        }
        public void SetCustomAttribute (System.Reflection.ConstructorInfo! con, Byte[]! binaryAttribute) {
            CodeContract.Requires(con != null);
            CodeContract.Requires(binaryAttribute != null);

        }
        public bool IsDefined (Type attributeType, bool inherit) {

          return default(bool);
        }
        public Object[] GetCustomAttributes (Type attributeType, bool inherit) {

          return default(Object[]);
        }
        public Object[] GetCustomAttributes (bool inherit) {

          return default(Object[]);
        }
        public System.Reflection.MethodImplAttributes GetMethodImplementationFlags () {

          return default(System.Reflection.MethodImplAttributes);
        }
        public System.Reflection.ParameterInfo[] GetParameters () {

          return default(System.Reflection.ParameterInfo[]);
        }
        public object Invoke (object obj, System.Reflection.BindingFlags invokeAttr, System.Reflection.Binder binder, Object[] parameters, System.Globalization.CultureInfo culture) {

          return default(object);
        }
        public System.Reflection.MethodInfo GetBaseDefinition () {

          return default(System.Reflection.MethodInfo);
        }
        public System.Reflection.Module GetModule () {

          return default(System.Reflection.Module);
        }
        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public string ToString () {

          return default(string);
        }
        public ILGenerator GetILGenerator (int size) {

          return default(ILGenerator);
        }
        public ILGenerator GetILGenerator () {

          return default(ILGenerator);
        }
        public void SetImplementationFlags (System.Reflection.MethodImplAttributes attributes) {

        }
        public void CreateMethodBody (Byte[] il, int count) {
            CodeContract.Requires(il == null || count >= 0);
            CodeContract.Requires(count <= il.Length);

        }
        public void AddDeclarativeSecurity (System.Security.Permissions.SecurityAction action, System.Security.PermissionSet! pset) {
            CodeContract.Requires((int)action >= 2);
            CodeContract.Requires((int)action <= 7);
            CodeContract.Requires(pset != null);

        }
        public void SetSymCustomAttribute (string name, Byte[] data) {

        }
        public void SetMarshal (UnmanagedMarshal unmanagedMarshal) {

        }
        public ParameterBuilder DefineParameter (int position, System.Reflection.ParameterAttributes attributes, string strParamName) {
            CodeContract.Requires(position > 0);

          return default(ParameterBuilder);
        }
        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public int GetHashCode () {

          return default(int);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public bool Equals (object obj) {

          return default(bool);
        }
        public MethodToken GetToken () {
          return default(MethodToken);
        }
    }
}
