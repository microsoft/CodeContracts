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

namespace System
{

    public class Activator
    {

        public static object GetObject (Type! type, string url, object state) {
            CodeContract.Requires(type != null);

          CodeContract.Ensures(CodeContract.Result<object>() != null);
          return default(object);
        }
        public static object GetObject (Type type, string url) {

          CodeContract.Ensures(CodeContract.Result<object>() != null);
          return default(object);
        }
        public static System.Runtime.Remoting.ObjectHandle CreateComInstanceFrom (string assemblyName, string typeName, Byte[] hashValue, System.Configuration.Assemblies.AssemblyHashAlgorithm hashAlgorithm) {

          CodeContract.Ensures(CodeContract.Result<System.Runtime.Remoting.ObjectHandle>() != null);
          return default(System.Runtime.Remoting.ObjectHandle);
        }
        public static System.Runtime.Remoting.ObjectHandle CreateComInstanceFrom (string assemblyName, string typeName) {

          CodeContract.Ensures(CodeContract.Result<System.Runtime.Remoting.ObjectHandle>() != null);
          return default(System.Runtime.Remoting.ObjectHandle);
        }
        public static System.Runtime.Remoting.ObjectHandle CreateInstanceFrom (string assemblyFile, string typeName, bool ignoreCase, System.Reflection.BindingFlags bindingAttr, System.Reflection.Binder binder, Object[] args, System.Globalization.CultureInfo culture, Object[] activationAttributes, System.Security.Policy.Evidence securityInfo) {

          CodeContract.Ensures(CodeContract.Result<System.Runtime.Remoting.ObjectHandle>() != null);
          return default(System.Runtime.Remoting.ObjectHandle);
        }
        public static System.Runtime.Remoting.ObjectHandle CreateInstance (string assemblyName, string typeName, bool ignoreCase, System.Reflection.BindingFlags bindingAttr, System.Reflection.Binder binder, Object[] args, System.Globalization.CultureInfo culture, Object[] activationAttributes, System.Security.Policy.Evidence securityInfo) {

          CodeContract.Ensures(CodeContract.Result<System.Runtime.Remoting.ObjectHandle>() != null);
          return default(System.Runtime.Remoting.ObjectHandle);
        }
        public static System.Runtime.Remoting.ObjectHandle CreateInstanceFrom (string assemblyFile, string typeName, Object[] activationAttributes) {

          CodeContract.Ensures(CodeContract.Result<System.Runtime.Remoting.ObjectHandle>() != null);
          return default(System.Runtime.Remoting.ObjectHandle);
        }
        public static System.Runtime.Remoting.ObjectHandle CreateInstanceFrom (string assemblyFile, string typeName) {

          CodeContract.Ensures(CodeContract.Result<System.Runtime.Remoting.ObjectHandle>() != null);
          return default(System.Runtime.Remoting.ObjectHandle);
        }
        public static object CreateInstance (Type! type, bool nonPublic) {
            CodeContract.Requires(type != null);

          CodeContract.Ensures(CodeContract.Result<object>() != null);
          return default(object);
        }
        public static System.Runtime.Remoting.ObjectHandle CreateInstance (string assemblyName, string typeName, Object[] activationAttributes) {

          CodeContract.Ensures(CodeContract.Result<System.Runtime.Remoting.ObjectHandle>() != null);
          return default(System.Runtime.Remoting.ObjectHandle);
        }
        public static System.Runtime.Remoting.ObjectHandle CreateInstance (string assemblyName, string typeName) {

          CodeContract.Ensures(CodeContract.Result<System.Runtime.Remoting.ObjectHandle>() != null);
          return default(System.Runtime.Remoting.ObjectHandle);
        }
        public static object CreateInstance (Type type) {

          CodeContract.Ensures(CodeContract.Result<object>() != null);
          return default(object);
        }
        public static object CreateInstance (Type type, Object[] args, Object[] activationAttributes) {

          CodeContract.Ensures(CodeContract.Result<object>() != null);
          return default(object);
        }
        public static object CreateInstance (Type type, Object[] args) {

          CodeContract.Ensures(CodeContract.Result<object>() != null);
          return default(object);
        }
        public static object CreateInstance (Type! type, System.Reflection.BindingFlags bindingAttr, System.Reflection.Binder binder, Object[] args, System.Globalization.CultureInfo culture, Object[] activationAttributes) {
            CodeContract.Requires(type != null);
            CodeContract.Requires(activationAttributes.Length <= 1);

          CodeContract.Ensures(CodeContract.Result<object>() != null);
          return default(object);
        }
          CodeContract.Ensures(CodeContract.Result<object>() != null);
          return default(object);
        }
        public static object CreateInstance (Type type, System.Reflection.BindingFlags bindingAttr, System.Reflection.Binder binder, Object[] args, System.Globalization.CultureInfo culture) {
    }
}
