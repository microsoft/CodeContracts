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

    public class PropertyBuilder
    {

        public Type ReflectedType
        {
          get;
        }

        public bool CanRead
        {
          get;
        }

        public bool CanWrite
        {
          get;
        }

        public Type PropertyType
        {
          get;
        }

        public Type DeclaringType
        {
          get;
        }

        public System.Reflection.PropertyAttributes Attributes
        {
          get;
        }

        public string Name
        {
          get;
        }

#if false
        public PropertyToken PropertyToken
        {
          get;
        }
#endif

        public bool IsDefined (Type attributeType, bool inherit) {

          return default(bool);
        }
        public Object[] GetCustomAttributes (Type attributeType, bool inherit) {

          return default(Object[]);
        }
        public Object[] GetCustomAttributes (bool inherit) {

          return default(Object[]);
        }
        public System.Reflection.ParameterInfo[] GetIndexParameters () {

          return default(System.Reflection.ParameterInfo[]);
        }
        public System.Reflection.MethodInfo GetSetMethod (bool nonPublic) {

          return default(System.Reflection.MethodInfo);
        }
        public System.Reflection.MethodInfo GetGetMethod (bool nonPublic) {

          return default(System.Reflection.MethodInfo);
        }
        public System.Reflection.MethodInfo[] GetAccessors (bool nonPublic) {

          return default(System.Reflection.MethodInfo[]);
        }
        public void SetValue (object obj, object value, System.Reflection.BindingFlags invokeAttr, System.Reflection.Binder binder, Object[] index, System.Globalization.CultureInfo culture) {

        }
        public void SetValue (object obj, object value, Object[] index) {

        }
        public object GetValue (object obj, System.Reflection.BindingFlags invokeAttr, System.Reflection.Binder binder, Object[] index, System.Globalization.CultureInfo culture) {

          return default(object);
        }
        public object GetValue (object obj, Object[] index) {

          return default(object);
        }
        public void SetCustomAttribute (CustomAttributeBuilder customBuilder) {
            Contract.Requires(customBuilder != null);

        }
        public void SetCustomAttribute (System.Reflection.ConstructorInfo con, Byte[] binaryAttribute) {
            Contract.Requires(con != null);
            Contract.Requires(binaryAttribute != null);

        }
        public void AddOtherMethod (MethodBuilder mdBuilder) {
            Contract.Requires(mdBuilder != null);

        }
        public void SetSetMethod (MethodBuilder mdBuilder) {
            Contract.Requires(mdBuilder != null);

        }
        public void SetGetMethod (MethodBuilder mdBuilder) {
            Contract.Requires(mdBuilder != null);

        }
        public void SetConstant (object defaultValue) {
        }
    }
}
