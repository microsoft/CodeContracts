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

namespace System.Reflection
{

    public class TypeDelegator
    {

        public string FullName
        {
          get;
        }

        public Assembly Assembly
        {
          get;
        }

        public Type UnderlyingSystemType
        {
          get;
        }

        public RuntimeTypeHandle TypeHandle
        {
          get;
        }

        public Type BaseType
        {
          get;
        }

        public string Name
        {
          get;
        }

        public Guid GUID
        {
          get;
        }

        public string Namespace
        {
          get;
        }

        public string AssemblyQualifiedName
        {
          get;
        }

        public Module Module
        {
          get;
        }

        public InterfaceMapping GetInterfaceMap (Type interfaceType) {

          return default(InterfaceMapping);
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
        public Type GetElementType () {

          return default(Type);
        }
        public MemberInfo[] GetMembers (BindingFlags bindingAttr) {

          return default(MemberInfo[]);
        }
        public MemberInfo[] GetMember (string name, MemberTypes type, BindingFlags bindingAttr) {

          return default(MemberInfo[]);
        }
        public Type GetNestedType (string name, BindingFlags bindingAttr) {

          return default(Type);
        }
        public Type[] GetNestedTypes (BindingFlags bindingAttr) {

          return default(Type[]);
        }
        public EventInfo[] GetEvents (BindingFlags bindingAttr) {

          return default(EventInfo[]);
        }
        public PropertyInfo[] GetProperties (BindingFlags bindingAttr) {

          return default(PropertyInfo[]);
        }
        public EventInfo[] GetEvents () {

          return default(EventInfo[]);
        }
        public EventInfo GetEvent (string name, BindingFlags bindingAttr) {

          return default(EventInfo);
        }
        public Type[] GetInterfaces () {

          return default(Type[]);
        }
        public Type GetInterface (string name, bool ignoreCase) {

          return default(Type);
        }
        public FieldInfo[] GetFields (BindingFlags bindingAttr) {

          return default(FieldInfo[]);
        }
        public FieldInfo GetField (string name, BindingFlags bindingAttr) {

          return default(FieldInfo);
        }
        public MethodInfo[] GetMethods (BindingFlags bindingAttr) {

          return default(MethodInfo[]);
        }
        public ConstructorInfo[] GetConstructors (BindingFlags bindingAttr) {

          return default(ConstructorInfo[]);
        }
        public object InvokeMember (string name, BindingFlags invokeAttr, Binder binder, object target, Object[] args, ParameterModifier[] modifiers, System.Globalization.CultureInfo culture, String[] namedParameters) {

          return default(object);
        }
        public TypeDelegator (Type delegatingType) {
            Contract.Requires(delegatingType != null);
          return default(TypeDelegator);
        }
    }
}
