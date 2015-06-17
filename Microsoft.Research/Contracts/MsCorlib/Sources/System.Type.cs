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

// File System.Type.cs
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


namespace System
{
  abstract public partial class Type : System.Reflection.MemberInfo, System.Runtime.InteropServices._Type, System.Reflection.IReflect
  {
    #region Methods and constructors
    public static bool operator != (System.Type left, System.Type right)
    {
      return default(bool);
    }

    public static bool operator == (System.Type left, System.Type right)
    {
      return default(bool);
    }

    public virtual new bool Equals(System.Type o)
    {
      return default(bool);
    }

    public override bool Equals(Object o)
    {
      return default(bool);
    }

    public virtual new System.Type[] FindInterfaces(System.Reflection.TypeFilter filter, Object filterCriteria)
    {
      return default(System.Type[]);
    }

    public virtual new System.Reflection.MemberInfo[] FindMembers(System.Reflection.MemberTypes memberType, System.Reflection.BindingFlags bindingAttr, System.Reflection.MemberFilter filter, Object filterCriteria)
    {
      return default(System.Reflection.MemberInfo[]);
    }

    public virtual new int GetArrayRank()
    {
      return default(int);
    }

    protected abstract System.Reflection.TypeAttributes GetAttributeFlagsImpl();

    public System.Reflection.ConstructorInfo GetConstructor(System.Reflection.BindingFlags bindingAttr, System.Reflection.Binder binder, System.Reflection.CallingConventions callConvention, System.Type[] types, System.Reflection.ParameterModifier[] modifiers)
    {
      return default(System.Reflection.ConstructorInfo);
    }

    public System.Reflection.ConstructorInfo GetConstructor(System.Reflection.BindingFlags bindingAttr, System.Reflection.Binder binder, System.Type[] types, System.Reflection.ParameterModifier[] modifiers)
    {
      return default(System.Reflection.ConstructorInfo);
    }

    public System.Reflection.ConstructorInfo GetConstructor(System.Type[] types)
    {
      return default(System.Reflection.ConstructorInfo);
    }

    protected abstract System.Reflection.ConstructorInfo GetConstructorImpl(System.Reflection.BindingFlags bindingAttr, System.Reflection.Binder binder, System.Reflection.CallingConventions callConvention, System.Type[] types, System.Reflection.ParameterModifier[] modifiers);

    public abstract System.Reflection.ConstructorInfo[] GetConstructors(System.Reflection.BindingFlags bindingAttr);

    public System.Reflection.ConstructorInfo[] GetConstructors()
    {
      return default(System.Reflection.ConstructorInfo[]);
    }

    public virtual new System.Reflection.MemberInfo[] GetDefaultMembers()
    {
      return default(System.Reflection.MemberInfo[]);
    }

    public abstract System.Type GetElementType();

    public virtual new string GetEnumName(Object value)
    {
      return default(string);
    }

    public virtual new string[] GetEnumNames()
    {
      return default(string[]);
    }

    public virtual new System.Type GetEnumUnderlyingType()
    {
      return default(System.Type);
    }

    public virtual new Array GetEnumValues()
    {
      return default(Array);
    }

    public System.Reflection.EventInfo GetEvent(string name)
    {
      return default(System.Reflection.EventInfo);
    }

    public abstract System.Reflection.EventInfo GetEvent(string name, System.Reflection.BindingFlags bindingAttr);

    public abstract System.Reflection.EventInfo[] GetEvents(System.Reflection.BindingFlags bindingAttr);

    public virtual new System.Reflection.EventInfo[] GetEvents()
    {
      return default(System.Reflection.EventInfo[]);
    }

    public System.Reflection.FieldInfo GetField(string name)
    {
      return default(System.Reflection.FieldInfo);
    }

    public abstract System.Reflection.FieldInfo GetField(string name, System.Reflection.BindingFlags bindingAttr);

    public System.Reflection.FieldInfo[] GetFields()
    {
      return default(System.Reflection.FieldInfo[]);
    }

    public abstract System.Reflection.FieldInfo[] GetFields(System.Reflection.BindingFlags bindingAttr);

    public virtual new System.Type[] GetGenericArguments()
    {
      return default(System.Type[]);
    }

    public virtual new System.Type[] GetGenericParameterConstraints()
    {
      return default(System.Type[]);
    }

    public virtual new System.Type GetGenericTypeDefinition()
    {
      return default(System.Type);
    }

    public override int GetHashCode()
    {
      return default(int);
    }

    public abstract System.Type GetInterface(string name, bool ignoreCase);

    public System.Type GetInterface(string name)
    {
      return default(System.Type);
    }

    public virtual new System.Reflection.InterfaceMapping GetInterfaceMap(System.Type interfaceType)
    {
      return default(System.Reflection.InterfaceMapping);
    }

    public abstract System.Type[] GetInterfaces();

    public virtual new System.Reflection.MemberInfo[] GetMember(string name, System.Reflection.BindingFlags bindingAttr)
    {
      return default(System.Reflection.MemberInfo[]);
    }

    public System.Reflection.MemberInfo[] GetMember(string name)
    {
      return default(System.Reflection.MemberInfo[]);
    }

    public virtual new System.Reflection.MemberInfo[] GetMember(string name, System.Reflection.MemberTypes type, System.Reflection.BindingFlags bindingAttr)
    {
      return default(System.Reflection.MemberInfo[]);
    }

    public abstract System.Reflection.MemberInfo[] GetMembers(System.Reflection.BindingFlags bindingAttr);

    public System.Reflection.MemberInfo[] GetMembers()
    {
      return default(System.Reflection.MemberInfo[]);
    }

    public System.Reflection.MethodInfo GetMethod(string name)
    {
      return default(System.Reflection.MethodInfo);
    }

    public System.Reflection.MethodInfo GetMethod(string name, System.Type[] types)
    {
      return default(System.Reflection.MethodInfo);
    }

    public System.Reflection.MethodInfo GetMethod(string name, System.Reflection.BindingFlags bindingAttr)
    {
      return default(System.Reflection.MethodInfo);
    }

    public System.Reflection.MethodInfo GetMethod(string name, System.Reflection.BindingFlags bindingAttr, System.Reflection.Binder binder, System.Reflection.CallingConventions callConvention, System.Type[] types, System.Reflection.ParameterModifier[] modifiers)
    {
      return default(System.Reflection.MethodInfo);
    }

    public System.Reflection.MethodInfo GetMethod(string name, System.Type[] types, System.Reflection.ParameterModifier[] modifiers)
    {
      return default(System.Reflection.MethodInfo);
    }

    public System.Reflection.MethodInfo GetMethod(string name, System.Reflection.BindingFlags bindingAttr, System.Reflection.Binder binder, System.Type[] types, System.Reflection.ParameterModifier[] modifiers)
    {
      return default(System.Reflection.MethodInfo);
    }

    protected abstract System.Reflection.MethodInfo GetMethodImpl(string name, System.Reflection.BindingFlags bindingAttr, System.Reflection.Binder binder, System.Reflection.CallingConventions callConvention, System.Type[] types, System.Reflection.ParameterModifier[] modifiers);

    public abstract System.Reflection.MethodInfo[] GetMethods(System.Reflection.BindingFlags bindingAttr);

    public System.Reflection.MethodInfo[] GetMethods()
    {
      return default(System.Reflection.MethodInfo[]);
    }

    public abstract System.Type GetNestedType(string name, System.Reflection.BindingFlags bindingAttr);

    public System.Type GetNestedType(string name)
    {
      return default(System.Type);
    }

    public abstract System.Type[] GetNestedTypes(System.Reflection.BindingFlags bindingAttr);

    public System.Type[] GetNestedTypes()
    {
      return default(System.Type[]);
    }

    public System.Reflection.PropertyInfo[] GetProperties()
    {
      return default(System.Reflection.PropertyInfo[]);
    }

    public abstract System.Reflection.PropertyInfo[] GetProperties(System.Reflection.BindingFlags bindingAttr);

    public System.Reflection.PropertyInfo GetProperty(string name, System.Type returnType, System.Type[] types)
    {
      return default(System.Reflection.PropertyInfo);
    }

    public System.Reflection.PropertyInfo GetProperty(string name, System.Type returnType)
    {
      return default(System.Reflection.PropertyInfo);
    }

    public System.Reflection.PropertyInfo GetProperty(string name, System.Type[] types)
    {
      return default(System.Reflection.PropertyInfo);
    }

    public System.Reflection.PropertyInfo GetProperty(string name, System.Type returnType, System.Type[] types, System.Reflection.ParameterModifier[] modifiers)
    {
      return default(System.Reflection.PropertyInfo);
    }

    public System.Reflection.PropertyInfo GetProperty(string name, System.Reflection.BindingFlags bindingAttr)
    {
      return default(System.Reflection.PropertyInfo);
    }

    public System.Reflection.PropertyInfo GetProperty(string name, System.Reflection.BindingFlags bindingAttr, System.Reflection.Binder binder, System.Type returnType, System.Type[] types, System.Reflection.ParameterModifier[] modifiers)
    {
      return default(System.Reflection.PropertyInfo);
    }

    public System.Reflection.PropertyInfo GetProperty(string name)
    {
      return default(System.Reflection.PropertyInfo);
    }

    protected abstract System.Reflection.PropertyInfo GetPropertyImpl(string name, System.Reflection.BindingFlags bindingAttr, System.Reflection.Binder binder, System.Type returnType, System.Type[] types, System.Reflection.ParameterModifier[] modifiers);

    public static System.Type GetType(string typeName, bool throwOnError)
    {
      return default(System.Type);
    }

    public System.Type GetType()
    {
      return default(System.Type);
    }

    public static System.Type GetType(string typeName, bool throwOnError, bool ignoreCase)
    {
      return default(System.Type);
    }

    public static System.Type GetType(string typeName)
    {
      return default(System.Type);
    }

    public static System.Type GetType(string typeName, Func<System.Reflection.AssemblyName, System.Reflection.Assembly> assemblyResolver, Func<System.Reflection.Assembly, string, bool, System.Type> typeResolver, bool throwOnError)
    {
      return default(System.Type);
    }

    public static System.Type GetType(string typeName, Func<System.Reflection.AssemblyName, System.Reflection.Assembly> assemblyResolver, Func<System.Reflection.Assembly, string, bool, System.Type> typeResolver, bool throwOnError, bool ignoreCase)
    {
      return default(System.Type);
    }

    public static System.Type GetType(string typeName, Func<System.Reflection.AssemblyName, System.Reflection.Assembly> assemblyResolver, Func<System.Reflection.Assembly, string, bool, System.Type> typeResolver)
    {
      return default(System.Type);
    }

    public static System.Type[] GetTypeArray(Object[] args)
    {
      Contract.Ensures(Contract.Result<System.Type[]>() != null);

      return default(System.Type[]);
    }

    public static TypeCode GetTypeCode(System.Type type)
    {
      return default(TypeCode);
    }

    protected virtual new TypeCode GetTypeCodeImpl()
    {
      return default(TypeCode);
    }

    public static System.Type GetTypeFromCLSID(Guid clsid, bool throwOnError)
    {
      return default(System.Type);
    }

    public static System.Type GetTypeFromCLSID(Guid clsid)
    {
      return default(System.Type);
    }

    public static System.Type GetTypeFromCLSID(Guid clsid, string server)
    {
      return default(System.Type);
    }

    public static System.Type GetTypeFromCLSID(Guid clsid, string server, bool throwOnError)
    {
      return default(System.Type);
    }

    public static System.Type GetTypeFromHandle(RuntimeTypeHandle handle)
    {
      return default(System.Type);
    }

    public static System.Type GetTypeFromProgID(string progID)
    {
      return default(System.Type);
    }

    public static System.Type GetTypeFromProgID(string progID, string server, bool throwOnError)
    {
      return default(System.Type);
    }

    public static System.Type GetTypeFromProgID(string progID, string server)
    {
      return default(System.Type);
    }

    public static System.Type GetTypeFromProgID(string progID, bool throwOnError)
    {
      return default(System.Type);
    }

    public static RuntimeTypeHandle GetTypeHandle(Object o)
    {
      return default(RuntimeTypeHandle);
    }

    protected abstract bool HasElementTypeImpl();

    public abstract Object InvokeMember(string name, System.Reflection.BindingFlags invokeAttr, System.Reflection.Binder binder, Object target, Object[] args, System.Reflection.ParameterModifier[] modifiers, System.Globalization.CultureInfo culture, string[] namedParameters);

    public Object InvokeMember(string name, System.Reflection.BindingFlags invokeAttr, System.Reflection.Binder binder, Object target, Object[] args, System.Globalization.CultureInfo culture)
    {
      return default(Object);
    }

    public Object InvokeMember(string name, System.Reflection.BindingFlags invokeAttr, System.Reflection.Binder binder, Object target, Object[] args)
    {
      return default(Object);
    }

    protected abstract bool IsArrayImpl();

    public virtual new bool IsAssignableFrom(System.Type c)
    {
      return default(bool);
    }

    protected abstract bool IsByRefImpl();

    protected abstract bool IsCOMObjectImpl();

    protected virtual new bool IsContextfulImpl()
    {
      return default(bool);
    }

    public virtual new bool IsEnumDefined(Object value)
    {
      return default(bool);
    }

    public virtual new bool IsEquivalentTo(System.Type other)
    {
      return default(bool);
    }

    public virtual new bool IsInstanceOfType(Object o)
    {
      return default(bool);
    }

    protected virtual new bool IsMarshalByRefImpl()
    {
      return default(bool);
    }

    protected abstract bool IsPointerImpl();

    protected abstract bool IsPrimitiveImpl();

    public virtual new bool IsSubclassOf(System.Type c)
    {
      return default(bool);
    }

    protected virtual new bool IsValueTypeImpl()
    {
      return default(bool);
    }

    public virtual new System.Type MakeArrayType()
    {
      return default(System.Type);
    }

    public virtual new System.Type MakeArrayType(int rank)
    {
      return default(System.Type);
    }

    public virtual new System.Type MakeByRefType()
    {
      return default(System.Type);
    }

    public virtual new System.Type MakeGenericType(System.Type[] typeArguments)
    {
      return default(System.Type);
    }

    public virtual new System.Type MakePointerType()
    {
      return default(System.Type);
    }

    public static System.Type ReflectionOnlyGetType(string typeName, bool throwIfNotFound, bool ignoreCase)
    {
      return default(System.Type);
    }

    void System.Runtime.InteropServices._Type.GetIDsOfNames(ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
    {
    }

    void System.Runtime.InteropServices._Type.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
    {
    }

    void System.Runtime.InteropServices._Type.GetTypeInfoCount(out uint pcTInfo)
    {
      pcTInfo = default(uint);
    }

    void System.Runtime.InteropServices._Type.Invoke(uint dispIdMember, ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
    {
    }

    public override string ToString()
    {
      return default(string);
    }

    protected Type()
    {
    }
    #endregion

    #region Properties and indexers
    public abstract System.Reflection.Assembly Assembly
    {
      get;
    }

    public abstract string AssemblyQualifiedName
    {
      get;
    }

    public System.Reflection.TypeAttributes Attributes
    {
      get
      {
        return default(System.Reflection.TypeAttributes);
      }
    }

    public abstract System.Type BaseType
    {
      get;
    }

    public virtual new bool ContainsGenericParameters
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new System.Reflection.MethodBase DeclaringMethod
    {
      get
      {
        return default(System.Reflection.MethodBase);
      }
    }

    public override System.Type DeclaringType
    {
      get
      {
        return default(System.Type);
      }
    }

    public static System.Reflection.Binder DefaultBinder
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Reflection.Binder>() != null);

        return default(System.Reflection.Binder);
      }
    }

    public abstract string FullName
    {
      get;
    }

    public virtual new System.Reflection.GenericParameterAttributes GenericParameterAttributes
    {
      get
      {
        return default(System.Reflection.GenericParameterAttributes);
      }
    }

    public virtual new int GenericParameterPosition
    {
      get
      {
        return default(int);
      }
    }

    public abstract Guid GUID
    {
      get;
    }

    public bool HasElementType
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsAbstract
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsAnsiClass
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsArray
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsAutoClass
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsAutoLayout
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsByRef
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsClass
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsCOMObject
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsContextful
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool IsEnum
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsExplicitLayout
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool IsGenericParameter
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool IsGenericType
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool IsGenericTypeDefinition
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsImport
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsInterface
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsLayoutSequential
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsMarshalByRef
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsNested
    {
      get
      {
        Contract.Ensures(Contract.Result<bool>() == (this.DeclaringType != null));

        return default(bool);
      }
    }

    public bool IsNestedAssembly
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsNestedFamANDAssem
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsNestedFamily
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsNestedFamORAssem
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsNestedPrivate
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsNestedPublic
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsNotPublic
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsPointer
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsPrimitive
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsPublic
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsSealed
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool IsSecurityCritical
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool IsSecuritySafeCritical
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool IsSecurityTransparent
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool IsSerializable
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsSpecialName
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsUnicodeClass
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsValueType
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsVisible
    {
      get
      {
        return default(bool);
      }
    }

    public override System.Reflection.MemberTypes MemberType
    {
      get
      {
        return default(System.Reflection.MemberTypes);
      }
    }

    public abstract System.Reflection.Module Module
    {
      get;
    }

    public abstract string Namespace
    {
      get;
    }

    public override System.Type ReflectedType
    {
      get
      {
        return default(System.Type);
      }
    }

    public virtual new System.Runtime.InteropServices.StructLayoutAttribute StructLayoutAttribute
    {
      get
      {
        return default(System.Runtime.InteropServices.StructLayoutAttribute);
      }
    }

    public virtual new RuntimeTypeHandle TypeHandle
    {
      get
      {
        return default(RuntimeTypeHandle);
      }
    }

    public System.Reflection.ConstructorInfo TypeInitializer
    {
      get
      {
        return default(System.Reflection.ConstructorInfo);
      }
    }

    public abstract System.Type UnderlyingSystemType
    {
      get;
    }
    #endregion

    #region Fields
    public readonly static char Delimiter;
    public readonly static System.Type[] EmptyTypes;
    public readonly static System.Reflection.MemberFilter FilterAttribute;
    public readonly static System.Reflection.MemberFilter FilterName;
    public readonly static System.Reflection.MemberFilter FilterNameIgnoreCase;
    public readonly static Object Missing;
    #endregion
  }
}
