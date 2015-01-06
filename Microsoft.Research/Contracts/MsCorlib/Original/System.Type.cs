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

namespace System
{

    [Immutable]
    public class Type
    {

        public string Namespace
        {
          get;
        }

        public bool IsMarshalByRef
        {
          get;
        }

        public System.Reflection.ConstructorInfo TypeInitializer
        {
          get;
        }

        public bool IsExplicitLayout
        {
          get;
        }

        public bool IsValueType
        {
          get;
        }

        public bool IsAutoClass
        {
          get;
        }

        public bool IsNestedPrivate
        {
          get;
        }

        public bool IsSerializable
        {
          get;
        }

        public System.Reflection.Assembly! Assembly
        {
          get;
        }

        public bool IsNestedAssembly
        {
          get;
        }

        public bool IsNotPublic
        {
          get;
        }

        public bool IsSealed
        {
          get;
        }

        public Guid GUID
        {
          get;
        }

        public bool IsLayoutSequential
        {
          get;
        }

        public bool IsNestedFamily
        {
          get;
        }

        public bool IsNestedFamORAssem
        {
          get;
        }

        public string FullName
        {
          get;
        }

        public virtual Type! MakeArrayType() {
        
          return default(virtual);
        }
        public System.Reflection.MemberTypes MemberType
        {
          get;
        }

        public string AssemblyQualifiedName
        {
          get;
        }

        public Type BaseType
        {
          get;
        }

        public RuntimeTypeHandle TypeHandle
        {
          get;
        }

        public bool IsInterface
        {
          get;
        }

        public bool IsAnsiClass
        {
          get;
        }

        public bool IsAutoLayout
        {
          get;
        }

        public bool IsPointer
        {
          get;
        }

        public bool IsEnum
        {
          get;
        }

        public Type ReflectedType
        {
          get;
        }

        public System.Reflection.TypeAttributes Attributes
        {
          get;
        }

        public Type DeclaringType
        {
          get;
        }

        public bool IsNestedFamANDAssem
        {
          get;
        }

        public bool IsContextful
        {
          get;
        }

        public bool IsClass
        {
          get;
        }

        public bool IsPublic
        {
          get;
        }

        public bool IsAbstract
        {
          get;
        }

        public Type UnderlyingSystemType
        {
          get;
        }

        public bool IsPrimitive
        {
          get;
        }

        public System.Reflection.Module! Module
        {
          get;
        }

        public bool IsImport
        {
          get;
        }

        public bool IsArray
        {
          get;
        }

        public bool IsNestedPublic
        {
          get;
        }

        public bool IsByRef
        {
          get;
        }

        public bool IsSpecialName
        {
          get;
        }

        public bool IsUnicodeClass
        {
          get;
        }

        public static System.Reflection.Binder DefaultBinder
        {
          get;
        }

        public bool HasElementType
        {
          get;
        }

        public bool IsCOMObject
        {
          get;
        }

        public System.Reflection.InterfaceMapping GetInterfaceMap (Type interfaceType) {

          return default(System.Reflection.InterfaceMapping);
        }
        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public int GetHashCode () {

          return default(int);
        }
        public bool Equals (Type o) {

          return default(bool);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public bool Equals (object o) {

          return default(bool);
        }
        public static Type[] GetTypeArray (Object[]! args) {
            CodeContract.Requires(args != null);

          return default(Type[]);
        }
        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public string ToString () {

          CodeContract.Ensures(CodeContract.Result<string>() != null);
          return default(string);
        }
        public bool IsAssignableFrom (Type c) {

          return default(bool);
        }
        public bool IsInstanceOfType (object o) {

          return default(bool);
        }
        public bool IsSubclassOf (Type c) {

          return default(bool);
        }
        public Type GetElementType () {

          return default(Type);
        }
        public System.Reflection.MemberInfo[] FindMembers (System.Reflection.MemberTypes memberType, System.Reflection.BindingFlags bindingAttr, System.Reflection.MemberFilter filter, object filterCriteria) {

          return default(System.Reflection.MemberInfo[]);
        }
        public System.Reflection.MemberInfo[] GetDefaultMembers () {

          return default(System.Reflection.MemberInfo[]);
        }
        public System.Reflection.MemberInfo[] GetMembers (System.Reflection.BindingFlags arg0) {

          return default(System.Reflection.MemberInfo[]);
        }
        public System.Reflection.MemberInfo[] GetMembers () {

          return default(System.Reflection.MemberInfo[]);
        }
        public System.Reflection.MemberInfo[] GetMember (string name, System.Reflection.MemberTypes type, System.Reflection.BindingFlags bindingAttr) {

          return default(System.Reflection.MemberInfo[]);
        }
        public System.Reflection.MemberInfo[] GetMember (string name, System.Reflection.BindingFlags bindingAttr) {

          return default(System.Reflection.MemberInfo[]);
        }
        public System.Reflection.MemberInfo[] GetMember (string name) {

          return default(System.Reflection.MemberInfo[]);
        }
        public Type GetNestedType (string arg0, System.Reflection.BindingFlags arg1) {

          return default(Type);
        }
        public Type GetNestedType (string name) {

          return default(Type);
        }
        public Type[] GetNestedTypes (System.Reflection.BindingFlags arg0) {

          return default(Type[]);
        }
        public Type[] GetNestedTypes () {

          return default(Type[]);
        }
        public System.Reflection.PropertyInfo[] GetProperties () {

          return default(System.Reflection.PropertyInfo[]);
        }
        public System.Reflection.PropertyInfo[] GetProperties (System.Reflection.BindingFlags arg0) {

          return default(System.Reflection.PropertyInfo[]);
        }
        public System.Reflection.PropertyInfo GetProperty (string! name) {
            CodeContract.Requires(name != null);

          return default(System.Reflection.PropertyInfo);
        }
        public System.Reflection.PropertyInfo GetProperty (string! name, Type! returnType) {
            CodeContract.Requires(name != null);
            CodeContract.Requires(returnType != null);

          return default(System.Reflection.PropertyInfo);
        }
        public System.Reflection.PropertyInfo GetProperty (string! name, Type[]! types) {
            CodeContract.Requires(name != null);
            CodeContract.Requires(types != null);

          return default(System.Reflection.PropertyInfo);
        }
        public System.Reflection.PropertyInfo GetProperty (string! name, Type returnType, Type[]! types) {
            CodeContract.Requires(name != null);
            CodeContract.Requires(types != null);

          return default(System.Reflection.PropertyInfo);
        }
        public System.Reflection.PropertyInfo GetProperty (string! name, System.Reflection.BindingFlags bindingAttr) {
            CodeContract.Requires(name != null);

          return default(System.Reflection.PropertyInfo);
        }
        public System.Reflection.PropertyInfo GetProperty (string! name, Type returnType, Type[]! types, System.Reflection.ParameterModifier[] modifiers) {
            CodeContract.Requires(name != null);
            CodeContract.Requires(types != null);

          return default(System.Reflection.PropertyInfo);
        }
        public System.Reflection.PropertyInfo GetProperty (string! name, System.Reflection.BindingFlags bindingAttr, System.Reflection.Binder binder, Type returnType, Type[]! types, System.Reflection.ParameterModifier[] modifiers) {
            CodeContract.Requires(name != null);
            CodeContract.Requires(types != null);

          return default(System.Reflection.PropertyInfo);
        }
        public System.Reflection.EventInfo[] GetEvents (System.Reflection.BindingFlags arg0) {

          return default(System.Reflection.EventInfo[]);
        }
        public System.Reflection.EventInfo[] GetEvents () {

          return default(System.Reflection.EventInfo[]);
        }
        public System.Reflection.EventInfo GetEvent (string arg0, System.Reflection.BindingFlags arg1) {

          return default(System.Reflection.EventInfo);
        }
        public System.Reflection.EventInfo GetEvent (string name) {

          return default(System.Reflection.EventInfo);
        }
        public Type[] FindInterfaces (System.Reflection.TypeFilter! filter, object filterCriteria) {
            CodeContract.Requires(filter != null);

          return default(Type[]);
        }
        public Type[] GetInterfaces () {

          return default(Type[]);
        }
        public Type GetInterface (string arg0, bool arg1) {

          return default(Type);
        }
        public Type GetInterface (string name) {

          return default(Type);
        }
        public System.Reflection.FieldInfo[] GetFields (System.Reflection.BindingFlags arg0) {

          return default(System.Reflection.FieldInfo[]);
        }
        public System.Reflection.FieldInfo[] GetFields () {

          return default(System.Reflection.FieldInfo[]);
        }
        public System.Reflection.FieldInfo GetField (string name) {

          return default(System.Reflection.FieldInfo);
        }
        public System.Reflection.FieldInfo GetField (string arg0, System.Reflection.BindingFlags arg1) {

          return default(System.Reflection.FieldInfo);
        }
        public System.Reflection.MethodInfo[] GetMethods (System.Reflection.BindingFlags arg0) {

          return default(System.Reflection.MethodInfo[]);
        }
        public System.Reflection.MethodInfo[] GetMethods () {

          return default(System.Reflection.MethodInfo[]);
        }
        public System.Reflection.MethodInfo GetMethod (string! name) {
            CodeContract.Requires(name != null);

          return default(System.Reflection.MethodInfo);
        }
        public System.Reflection.MethodInfo GetMethod (string! name, System.Reflection.BindingFlags bindingAttr) {
            CodeContract.Requires(name != null);

          return default(System.Reflection.MethodInfo);
        }
        public System.Reflection.MethodInfo GetMethod (string! name, Type[]! types) {
            CodeContract.Requires(name != null);
            CodeContract.Requires(types != null);

          return default(System.Reflection.MethodInfo);
        }
        public System.Reflection.MethodInfo GetMethod (string! name, Type[]! types, System.Reflection.ParameterModifier[] modifiers) {
            CodeContract.Requires(name != null);
            CodeContract.Requires(types != null);

          return default(System.Reflection.MethodInfo);
        }
        public System.Reflection.MethodInfo GetMethod (string! name, System.Reflection.BindingFlags bindingAttr, System.Reflection.Binder binder, Type[]! types, System.Reflection.ParameterModifier[] modifiers) {
            CodeContract.Requires(name != null);
            CodeContract.Requires(types != null);

          return default(System.Reflection.MethodInfo);
        }
        public System.Reflection.MethodInfo GetMethod (string! name, System.Reflection.BindingFlags bindingAttr, System.Reflection.Binder binder, System.Reflection.CallingConventions callConvention, Type[]! types, System.Reflection.ParameterModifier[] modifiers) {
            CodeContract.Requires(name != null);
            CodeContract.Requires(types != null);

          return default(System.Reflection.MethodInfo);
        }
        public System.Reflection.ConstructorInfo[] GetConstructors (System.Reflection.BindingFlags arg0) {

          return default(System.Reflection.ConstructorInfo[]);
        }
        public System.Reflection.ConstructorInfo[] GetConstructors () {

          return default(System.Reflection.ConstructorInfo[]);
        }
        public System.Reflection.ConstructorInfo GetConstructor (Type[] types) {

          return default(System.Reflection.ConstructorInfo);
        }
        public System.Reflection.ConstructorInfo GetConstructor (System.Reflection.BindingFlags bindingAttr, System.Reflection.Binder binder, Type[]! types, System.Reflection.ParameterModifier[] modifiers) {
            CodeContract.Requires(types != null);

          return default(System.Reflection.ConstructorInfo);
        }
        public System.Reflection.ConstructorInfo GetConstructor (System.Reflection.BindingFlags bindingAttr, System.Reflection.Binder binder, System.Reflection.CallingConventions callConvention, Type[]! types, System.Reflection.ParameterModifier[] modifiers) {
            CodeContract.Requires(types != null);

          return default(System.Reflection.ConstructorInfo);
        }
        public int GetArrayRank () {

          return default(int);
        }
        public static Type GetTypeFromHandle (RuntimeTypeHandle handle) {

          return default(Type);
        }
        public static RuntimeTypeHandle GetTypeHandle (object! o) {
            CodeContract.Requires(o != null);

          return default(RuntimeTypeHandle);
        }
        public object InvokeMember (string name, System.Reflection.BindingFlags invokeAttr, System.Reflection.Binder binder, object target, Object[] args) {

          return default(object);
        }
        public object InvokeMember (string name, System.Reflection.BindingFlags invokeAttr, System.Reflection.Binder binder, object target, Object[] args, System.Globalization.CultureInfo culture) {

          return default(object);
        }
        public object InvokeMember (string arg0, System.Reflection.BindingFlags arg1, System.Reflection.Binder arg2, object arg3, Object[] arg4, System.Reflection.ParameterModifier[] arg5, System.Globalization.CultureInfo arg6, String[] arg7) {

          return default(object);
        }
        public static TypeCode GetTypeCode (Type type) {

          return default(TypeCode);
        }
        public static Type GetTypeFromCLSID (Guid clsid, string server, bool throwOnError) {

          return default(Type);
        }
        public static Type GetTypeFromCLSID (Guid clsid, string server) {

          return default(Type);
        }
        public static Type GetTypeFromCLSID (Guid clsid, bool throwOnError) {

          return default(Type);
        }
        public static Type GetTypeFromCLSID (Guid clsid) {

          return default(Type);
        }
        public static Type GetTypeFromProgID (string progID, string server, bool throwOnError) {

          return default(Type);
        }
        public static Type GetTypeFromProgID (string progID, string server) {

          return default(Type);
        }
        public static Type GetTypeFromProgID (string progID, bool throwOnError) {

          return default(Type);
        }
        public static Type GetTypeFromProgID (string progID) {

          return default(Type);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)][ResultNotNewlyAllocated]
        public Type GetType () {

          CodeContract.Ensures(CodeContract.Result<Type>() != null);
          return default(Type);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)] 
        public static Type GetType (string typeName) {

          CodeContract.Ensures(CodeContract.Result<Type>() != null);
          return default(Type);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)] 
        public static Type GetType (string typeName, bool throwOnError) {
          CodeContract.Ensures(throwOnError ==> result != null);

          return default(Type);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)] 
        public static Type GetType (string typeName, bool throwOnError, bool ignoreCase) {
          CodeContract.Ensures(throwOnError ==> result != null);
          return default(Type);
        }
    }
}
