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

// File System.Runtime.InteropServices._Type.cs
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


namespace System.Runtime.InteropServices
{
  public partial interface _Type
  {
    #region Methods and constructors
    bool Equals(Object other);

    bool Equals(Type o);

    Type[] FindInterfaces(System.Reflection.TypeFilter filter, Object filterCriteria);

    System.Reflection.MemberInfo[] FindMembers(System.Reflection.MemberTypes memberType, System.Reflection.BindingFlags bindingAttr, System.Reflection.MemberFilter filter, Object filterCriteria);

    int GetArrayRank();

    System.Reflection.ConstructorInfo GetConstructor(System.Reflection.BindingFlags bindingAttr, System.Reflection.Binder binder, Type[] types, System.Reflection.ParameterModifier[] modifiers);

    System.Reflection.ConstructorInfo GetConstructor(System.Reflection.BindingFlags bindingAttr, System.Reflection.Binder binder, System.Reflection.CallingConventions callConvention, Type[] types, System.Reflection.ParameterModifier[] modifiers);

    System.Reflection.ConstructorInfo GetConstructor(Type[] types);

    System.Reflection.ConstructorInfo[] GetConstructors();

    System.Reflection.ConstructorInfo[] GetConstructors(System.Reflection.BindingFlags bindingAttr);

    Object[] GetCustomAttributes(Type attributeType, bool inherit);

    Object[] GetCustomAttributes(bool inherit);

    System.Reflection.MemberInfo[] GetDefaultMembers();

    Type GetElementType();

    System.Reflection.EventInfo GetEvent(string name, System.Reflection.BindingFlags bindingAttr);

    System.Reflection.EventInfo GetEvent(string name);

    System.Reflection.EventInfo[] GetEvents();

    System.Reflection.EventInfo[] GetEvents(System.Reflection.BindingFlags bindingAttr);

    System.Reflection.FieldInfo GetField(string name, System.Reflection.BindingFlags bindingAttr);

    System.Reflection.FieldInfo GetField(string name);

    System.Reflection.FieldInfo[] GetFields();

    System.Reflection.FieldInfo[] GetFields(System.Reflection.BindingFlags bindingAttr);

    int GetHashCode();

    void GetIDsOfNames(ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

    Type GetInterface(string name, bool ignoreCase);

    Type GetInterface(string name);

    System.Reflection.InterfaceMapping GetInterfaceMap(Type interfaceType);

    Type[] GetInterfaces();

    System.Reflection.MemberInfo[] GetMember(string name, System.Reflection.BindingFlags bindingAttr);

    System.Reflection.MemberInfo[] GetMember(string name, System.Reflection.MemberTypes type, System.Reflection.BindingFlags bindingAttr);

    System.Reflection.MemberInfo[] GetMember(string name);

    System.Reflection.MemberInfo[] GetMembers();

    System.Reflection.MemberInfo[] GetMembers(System.Reflection.BindingFlags bindingAttr);

    System.Reflection.MethodInfo GetMethod(string name, Type[] types, System.Reflection.ParameterModifier[] modifiers);

    System.Reflection.MethodInfo GetMethod(string name, System.Reflection.BindingFlags bindingAttr, System.Reflection.Binder binder, System.Reflection.CallingConventions callConvention, Type[] types, System.Reflection.ParameterModifier[] modifiers);

    System.Reflection.MethodInfo GetMethod(string name, Type[] types);

    System.Reflection.MethodInfo GetMethod(string name, System.Reflection.BindingFlags bindingAttr, System.Reflection.Binder binder, Type[] types, System.Reflection.ParameterModifier[] modifiers);

    System.Reflection.MethodInfo GetMethod(string name, System.Reflection.BindingFlags bindingAttr);

    System.Reflection.MethodInfo GetMethod(string name);

    System.Reflection.MethodInfo[] GetMethods(System.Reflection.BindingFlags bindingAttr);

    System.Reflection.MethodInfo[] GetMethods();

    Type GetNestedType(string name);

    Type GetNestedType(string name, System.Reflection.BindingFlags bindingAttr);

    Type[] GetNestedTypes(System.Reflection.BindingFlags bindingAttr);

    Type[] GetNestedTypes();

    System.Reflection.PropertyInfo[] GetProperties(System.Reflection.BindingFlags bindingAttr);

    System.Reflection.PropertyInfo[] GetProperties();

    System.Reflection.PropertyInfo GetProperty(string name, Type[] types);

    System.Reflection.PropertyInfo GetProperty(string name, Type returnType);

    System.Reflection.PropertyInfo GetProperty(string name);

    System.Reflection.PropertyInfo GetProperty(string name, Type returnType, Type[] types);

    System.Reflection.PropertyInfo GetProperty(string name, System.Reflection.BindingFlags bindingAttr);

    System.Reflection.PropertyInfo GetProperty(string name, System.Reflection.BindingFlags bindingAttr, System.Reflection.Binder binder, Type returnType, Type[] types, System.Reflection.ParameterModifier[] modifiers);

    System.Reflection.PropertyInfo GetProperty(string name, Type returnType, Type[] types, System.Reflection.ParameterModifier[] modifiers);

    Type GetType();

    void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

    void GetTypeInfoCount(out uint pcTInfo);

    void Invoke(uint dispIdMember, ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);

    Object InvokeMember(string name, System.Reflection.BindingFlags invokeAttr, System.Reflection.Binder binder, Object target, Object[] args);

    Object InvokeMember(string name, System.Reflection.BindingFlags invokeAttr, System.Reflection.Binder binder, Object target, Object[] args, System.Globalization.CultureInfo culture);

    Object InvokeMember(string name, System.Reflection.BindingFlags invokeAttr, System.Reflection.Binder binder, Object target, Object[] args, System.Reflection.ParameterModifier[] modifiers, System.Globalization.CultureInfo culture, string[] namedParameters);

    bool IsAssignableFrom(Type c);

    bool IsDefined(Type attributeType, bool inherit);

    bool IsInstanceOfType(Object o);

    bool IsSubclassOf(Type c);

    string ToString();
    #endregion

    #region Properties and indexers
    System.Reflection.Assembly Assembly
    {
      get;
    }

    string AssemblyQualifiedName
    {
      get;
    }

    System.Reflection.TypeAttributes Attributes
    {
      get;
    }

    Type BaseType
    {
      get;
    }

    Type DeclaringType
    {
      get;
    }

    string FullName
    {
      get;
    }

    Guid GUID
    {
      get;
    }

    bool HasElementType
    {
      get;
    }

    bool IsAbstract
    {
      get;
    }

    bool IsAnsiClass
    {
      get;
    }

    bool IsArray
    {
      get;
    }

    bool IsAutoClass
    {
      get;
    }

    bool IsAutoLayout
    {
      get;
    }

    bool IsByRef
    {
      get;
    }

    bool IsClass
    {
      get;
    }

    bool IsCOMObject
    {
      get;
    }

    bool IsContextful
    {
      get;
    }

    bool IsEnum
    {
      get;
    }

    bool IsExplicitLayout
    {
      get;
    }

    bool IsImport
    {
      get;
    }

    bool IsInterface
    {
      get;
    }

    bool IsLayoutSequential
    {
      get;
    }

    bool IsMarshalByRef
    {
      get;
    }

    bool IsNestedAssembly
    {
      get;
    }

    bool IsNestedFamANDAssem
    {
      get;
    }

    bool IsNestedFamily
    {
      get;
    }

    bool IsNestedFamORAssem
    {
      get;
    }

    bool IsNestedPrivate
    {
      get;
    }

    bool IsNestedPublic
    {
      get;
    }

    bool IsNotPublic
    {
      get;
    }

    bool IsPointer
    {
      get;
    }

    bool IsPrimitive
    {
      get;
    }

    bool IsPublic
    {
      get;
    }

    bool IsSealed
    {
      get;
    }

    bool IsSerializable
    {
      get;
    }

    bool IsSpecialName
    {
      get;
    }

    bool IsUnicodeClass
    {
      get;
    }

    bool IsValueType
    {
      get;
    }

    System.Reflection.MemberTypes MemberType
    {
      get;
    }

    System.Reflection.Module Module
    {
      get;
    }

    string Name
    {
      get;
    }

    string Namespace
    {
      get;
    }

    Type ReflectedType
    {
      get;
    }

    RuntimeTypeHandle TypeHandle
    {
      get;
    }

    System.Reflection.ConstructorInfo TypeInitializer
    {
      get;
    }

    Type UnderlyingSystemType
    {
      get;
    }
    #endregion
  }
}
