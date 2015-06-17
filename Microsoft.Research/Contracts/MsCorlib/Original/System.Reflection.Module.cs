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

namespace System.Reflection
{

    public class Module
    {

        public string FullyQualifiedName
        {
          get;
        }

        public string Name
        {
          get;
        }

        public string ScopeName
        {
          get;
        }

        public Assembly Assembly
        {
          get;
        }

        public bool IsResource () {

          return default(bool);
        }
        public bool IsDefined (Type! attributeType, bool inherit) {
            CodeContract.Requires(attributeType != null);

          return default(bool);
        }
        public Object[] GetCustomAttributes (Type! attributeType, bool inherit) {
            CodeContract.Requires(attributeType != null);

          return default(Object[]);
        }
        public Object[] GetCustomAttributes (bool inherit) {

          return default(Object[]);
        }
        public void GetObjectData (System.Runtime.Serialization.SerializationInfo! info, System.Runtime.Serialization.StreamingContext context) {
            CodeContract.Requires(info != null);

        }
        public FieldInfo GetField (string! name, BindingFlags bindingAttr) {
            CodeContract.Requires(name != null);

          return default(FieldInfo);
        }
        public FieldInfo GetField (string! name) {
            CodeContract.Requires(name != null);

          return default(FieldInfo);
        }
        public FieldInfo[] GetFields () {

          return default(FieldInfo[]);
        }
        public MethodInfo GetMethod (string! name) {
            CodeContract.Requires(name != null);

          return default(MethodInfo);
        }
        public MethodInfo GetMethod (string! name, Type[]! types) {
            CodeContract.Requires(name != null);
            CodeContract.Requires(types != null);

          return default(MethodInfo);
        }
        public MethodInfo GetMethod (string! name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[]! types, ParameterModifier[] modifiers) {
            CodeContract.Requires(name != null);
            CodeContract.Requires(types != null);

          return default(MethodInfo);
        }
        public MethodInfo[] GetMethods () {

          return default(MethodInfo[]);
        }
        public System.Security.Cryptography.X509Certificates.X509Certificate GetSignerCertificate () {

          return default(System.Security.Cryptography.X509Certificates.X509Certificate);
        }
        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public string ToString () {

          CodeContract.Ensures(CodeContract.Result<string>() != null);
          return default(string);
        }
        public Type[] GetTypes () {

          return default(Type[]);
        }
        public Type[] FindTypes (TypeFilter filter, object filterCriteria) {

          return default(Type[]);
        }
        public Type GetType (string className, bool throwOnError, bool ignoreCase) {

          return default(Type);
        }
        public Type GetType (string className) {

          return default(Type);
        }
        public Type GetType (string className, bool ignoreCase) {
          return default(Type);
        }
    }
}
