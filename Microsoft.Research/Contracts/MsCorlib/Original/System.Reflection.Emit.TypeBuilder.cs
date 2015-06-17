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

    public class TypeBuilder
    {

        public string FullName
        {
          get;
        }

        public Type ReflectedType
        {
          get;
        }

        public System.Reflection.Assembly Assembly
        {
          get;
        }

        public PackingSize PackingSize
        {
          get;
        }

        public Type UnderlyingSystemType
        {
          get;
        }

        public int Size
        {
          get;
        }

        public Type DeclaringType
        {
          get;
        }

        public RuntimeTypeHandle TypeHandle
        {
          get;
        }

        public TypeToken TypeToken
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

        public System.Reflection.Module Module
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
        public Object[] GetCustomAttributes (Type! attributeType, bool inherit) {
            CodeContract.Requires(attributeType != null);

          return default(Object[]);
        }
        public Object[] GetCustomAttributes (bool inherit) {

          return default(Object[]);
        }
        public bool IsSubclassOf (Type c) {

          return default(bool);
        }
        public Type GetElementType () {

          return default(Type);
        }
        public bool IsAssignableFrom (Type c) {

          return default(bool);
        }
        public System.Reflection.MemberInfo[] GetMembers (System.Reflection.BindingFlags bindingAttr) {

          return default(System.Reflection.MemberInfo[]);
        }
        public System.Reflection.EventInfo[] GetEvents (System.Reflection.BindingFlags bindingAttr) {

          return default(System.Reflection.EventInfo[]);
        }
        public System.Reflection.InterfaceMapping GetInterfaceMap (Type interfaceType) {

          return default(System.Reflection.InterfaceMapping);
        }
        public System.Reflection.MemberInfo[] GetMember (string name, System.Reflection.MemberTypes type, System.Reflection.BindingFlags bindingAttr) {

          return default(System.Reflection.MemberInfo[]);
        }
        public Type GetNestedType (string name, System.Reflection.BindingFlags bindingAttr) {

          return default(Type);
        }
        public Type[] GetNestedTypes (System.Reflection.BindingFlags bindingAttr) {

          return default(Type[]);
        }
        public System.Reflection.PropertyInfo[] GetProperties (System.Reflection.BindingFlags bindingAttr) {

          return default(System.Reflection.PropertyInfo[]);
        }
        public System.Reflection.EventInfo[] GetEvents () {

          return default(System.Reflection.EventInfo[]);
        }
        public System.Reflection.EventInfo GetEvent (string name, System.Reflection.BindingFlags bindingAttr) {

          return default(System.Reflection.EventInfo);
        }
        public Type[] GetInterfaces () {

          return default(Type[]);
        }
        public Type GetInterface (string name, bool ignoreCase) {

          return default(Type);
        }
        public System.Reflection.FieldInfo[] GetFields (System.Reflection.BindingFlags bindingAttr) {

          return default(System.Reflection.FieldInfo[]);
        }
        public System.Reflection.FieldInfo GetField (string name, System.Reflection.BindingFlags bindingAttr) {

          return default(System.Reflection.FieldInfo);
        }
        public System.Reflection.MethodInfo[] GetMethods (System.Reflection.BindingFlags bindingAttr) {

          return default(System.Reflection.MethodInfo[]);
        }
        public System.Reflection.ConstructorInfo[] GetConstructors (System.Reflection.BindingFlags bindingAttr) {

          return default(System.Reflection.ConstructorInfo[]);
        }
        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public string ToString () {

          return default(string);
        }
        public object InvokeMember (string name, System.Reflection.BindingFlags invokeAttr, System.Reflection.Binder binder, object target, Object[] args, System.Reflection.ParameterModifier[] modifiers, System.Globalization.CultureInfo culture, String[] namedParameters) {

          return default(object);
        }
        public void AddDeclarativeSecurity (System.Security.Permissions.SecurityAction action, System.Security.PermissionSet pset) {

        }
        public TypeBuilder DefineNestedType (string name, System.Reflection.TypeAttributes attr, Type parent, PackingSize packSize) {

          return default(TypeBuilder);
        }
        public TypeBuilder DefineNestedType (string name, System.Reflection.TypeAttributes attr, Type parent, int typeSize) {

          return default(TypeBuilder);
        }
        public TypeBuilder DefineNestedType (string name, System.Reflection.TypeAttributes attr) {

          return default(TypeBuilder);
        }
        public TypeBuilder DefineNestedType (string name, System.Reflection.TypeAttributes attr, Type parent) {

          return default(TypeBuilder);
        }
        public TypeBuilder DefineNestedType (string name, System.Reflection.TypeAttributes attr, Type parent, Type[] interfaces) {

          return default(TypeBuilder);
        }
        public TypeBuilder DefineNestedType (string name) {

          return default(TypeBuilder);
        }
        public FieldBuilder DefineUninitializedData (string name, int size, System.Reflection.FieldAttributes attributes) {

          return default(FieldBuilder);
        }
        public FieldBuilder DefineInitializedData (string name, Byte[] data, System.Reflection.FieldAttributes attributes) {

          return default(FieldBuilder);
        }
        public FieldBuilder DefineField (string fieldName, Type type, System.Reflection.FieldAttributes attributes) {

          return default(FieldBuilder);
        }
        public void DefineMethodOverride (System.Reflection.MethodInfo methodInfoBody, System.Reflection.MethodInfo methodInfoDeclaration) {

        }
        public Type CreateType () {

          return default(Type);
        }
        public ConstructorBuilder DefineDefaultConstructor (System.Reflection.MethodAttributes attributes) {

          return default(ConstructorBuilder);
        }
        public ConstructorBuilder DefineConstructor (System.Reflection.MethodAttributes attributes, System.Reflection.CallingConventions callingConvention, Type[] parameterTypes) {

          return default(ConstructorBuilder);
        }
        public ConstructorBuilder DefineTypeInitializer () {

          return default(ConstructorBuilder);
        }
        public MethodBuilder DefinePInvokeMethod (string name, string dllName, string entryName, System.Reflection.MethodAttributes attributes, System.Reflection.CallingConventions callingConvention, Type returnType, Type[] parameterTypes, System.Runtime.InteropServices.CallingConvention nativeCallConv, System.Runtime.InteropServices.CharSet nativeCharSet) {

          return default(MethodBuilder);
        }
        public MethodBuilder DefinePInvokeMethod (string name, string dllName, System.Reflection.MethodAttributes attributes, System.Reflection.CallingConventions callingConvention, Type returnType, Type[] parameterTypes, System.Runtime.InteropServices.CallingConvention nativeCallConv, System.Runtime.InteropServices.CharSet nativeCharSet) {

          return default(MethodBuilder);
        }
        public EventBuilder DefineEvent (string name, System.Reflection.EventAttributes attributes, Type eventtype) {

          return default(EventBuilder);
        }
        public PropertyBuilder DefineProperty (string name, System.Reflection.PropertyAttributes attributes, Type returnType, Type[] parameterTypes) {

          return default(PropertyBuilder);
        }
        public MethodBuilder DefineMethod (string name, System.Reflection.MethodAttributes attributes, System.Reflection.CallingConventions callingConvention, Type returnType, Type[] parameterTypes) {

          return default(MethodBuilder);
        }
        public MethodBuilder DefineMethod (string name, System.Reflection.MethodAttributes attributes, Type returnType, Type[] parameterTypes) {

          return default(MethodBuilder);
        }
        public void AddInterfaceImplementation (Type! interfaceType) {
            CodeContract.Requires(interfaceType != null);

        }
        public void SetParent (Type! parent) {
            CodeContract.Requires(parent != null);
        }
    }
}
