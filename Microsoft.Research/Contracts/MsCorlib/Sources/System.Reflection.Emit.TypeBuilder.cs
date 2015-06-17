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

// File System.Reflection.Emit.TypeBuilder.cs
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


namespace System.Reflection.Emit
{
  sealed public partial class TypeBuilder : Type, System.Runtime.InteropServices._TypeBuilder
  {
    #region Methods and constructors
    public void AddDeclarativeSecurity(System.Security.Permissions.SecurityAction action, System.Security.PermissionSet pset)
    {
    }

    public void AddInterfaceImplementation(Type interfaceType)
    {
    }

    public Type CreateType()
    {
      return default(Type);
    }

    public ConstructorBuilder DefineConstructor(System.Reflection.MethodAttributes attributes, System.Reflection.CallingConventions callingConvention, Type[] parameterTypes, Type[][] requiredCustomModifiers, Type[][] optionalCustomModifiers)
    {
      Contract.Ensures(Contract.Result<System.Reflection.Emit.ConstructorBuilder>() != null);

      return default(ConstructorBuilder);
    }

    public ConstructorBuilder DefineConstructor(System.Reflection.MethodAttributes attributes, System.Reflection.CallingConventions callingConvention, Type[] parameterTypes)
    {
      Contract.Ensures(Contract.Result<System.Reflection.Emit.ConstructorBuilder>() != null);

      return default(ConstructorBuilder);
    }

    public ConstructorBuilder DefineDefaultConstructor(System.Reflection.MethodAttributes attributes)
    {
      Contract.Ensures(Contract.Result<System.Reflection.Emit.ConstructorBuilder>() != null);

      return default(ConstructorBuilder);
    }

    public EventBuilder DefineEvent(string name, System.Reflection.EventAttributes attributes, Type eventtype)
    {
      Contract.Ensures(Contract.Result<System.Reflection.Emit.EventBuilder>() != null);

      return default(EventBuilder);
    }

    public FieldBuilder DefineField(string fieldName, Type type, Type[] requiredCustomModifiers, Type[] optionalCustomModifiers, System.Reflection.FieldAttributes attributes)
    {
      Contract.Ensures(Contract.Result<System.Reflection.Emit.FieldBuilder>() != null);

      return default(FieldBuilder);
    }

    public FieldBuilder DefineField(string fieldName, Type type, System.Reflection.FieldAttributes attributes)
    {
      Contract.Ensures(Contract.Result<System.Reflection.Emit.FieldBuilder>() != null);

      return default(FieldBuilder);
    }

    public GenericTypeParameterBuilder[] DefineGenericParameters(string[] names)
    {
      Contract.Ensures(Contract.Result<System.Reflection.Emit.GenericTypeParameterBuilder[]>() != null);

      return default(GenericTypeParameterBuilder[]);
    }

    public FieldBuilder DefineInitializedData(string name, byte[] data, System.Reflection.FieldAttributes attributes)
    {
      Contract.Ensures(Contract.Result<System.Reflection.Emit.FieldBuilder>() != null);

      return default(FieldBuilder);
    }

    public MethodBuilder DefineMethod(string name, System.Reflection.MethodAttributes attributes, System.Reflection.CallingConventions callingConvention, Type returnType, Type[] parameterTypes)
    {
      Contract.Ensures(Contract.Result<System.Reflection.Emit.MethodBuilder>() != null);

      return default(MethodBuilder);
    }

    public MethodBuilder DefineMethod(string name, System.Reflection.MethodAttributes attributes, System.Reflection.CallingConventions callingConvention, Type returnType, Type[] returnTypeRequiredCustomModifiers, Type[] returnTypeOptionalCustomModifiers, Type[] parameterTypes, Type[][] parameterTypeRequiredCustomModifiers, Type[][] parameterTypeOptionalCustomModifiers)
    {
      Contract.Ensures(Contract.Result<System.Reflection.Emit.MethodBuilder>() != null);

      return default(MethodBuilder);
    }

    public MethodBuilder DefineMethod(string name, System.Reflection.MethodAttributes attributes)
    {
      Contract.Ensures(Contract.Result<System.Reflection.Emit.MethodBuilder>() != null);

      return default(MethodBuilder);
    }

    public MethodBuilder DefineMethod(string name, System.Reflection.MethodAttributes attributes, Type returnType, Type[] parameterTypes)
    {
      Contract.Ensures(Contract.Result<System.Reflection.Emit.MethodBuilder>() != null);

      return default(MethodBuilder);
    }

    public MethodBuilder DefineMethod(string name, System.Reflection.MethodAttributes attributes, System.Reflection.CallingConventions callingConvention)
    {
      Contract.Ensures(Contract.Result<System.Reflection.Emit.MethodBuilder>() != null);

      return default(MethodBuilder);
    }

    public void DefineMethodOverride(System.Reflection.MethodInfo methodInfoBody, System.Reflection.MethodInfo methodInfoDeclaration)
    {
    }

    public TypeBuilder DefineNestedType(string name, System.Reflection.TypeAttributes attr, Type parent, Type[] interfaces)
    {
      Contract.Ensures(0 <= name.Length);
      Contract.Ensures(Contract.Result<System.Reflection.Emit.TypeBuilder>() != null);
      Contract.Ensures(name.Length <= 1023);

      return default(TypeBuilder);
    }

    public TypeBuilder DefineNestedType(string name)
    {
      Contract.Ensures(0 <= name.Length);
      Contract.Ensures(Contract.Result<System.Reflection.Emit.TypeBuilder>() != null);
      Contract.Ensures(name.Length <= 1023);

      return default(TypeBuilder);
    }

    public TypeBuilder DefineNestedType(string name, System.Reflection.TypeAttributes attr, Type parent, PackingSize packSize)
    {
      Contract.Ensures(0 <= name.Length);
      Contract.Ensures(Contract.Result<System.Reflection.Emit.TypeBuilder>() != null);
      Contract.Ensures(name.Length <= 1023);

      return default(TypeBuilder);
    }

    public TypeBuilder DefineNestedType(string name, System.Reflection.TypeAttributes attr, Type parent, int typeSize)
    {
      Contract.Ensures(0 <= name.Length);
      Contract.Ensures(Contract.Result<System.Reflection.Emit.TypeBuilder>() != null);
      Contract.Ensures(name.Length <= 1023);

      return default(TypeBuilder);
    }

    public TypeBuilder DefineNestedType(string name, System.Reflection.TypeAttributes attr)
    {
      Contract.Ensures(0 <= name.Length);
      Contract.Ensures(Contract.Result<System.Reflection.Emit.TypeBuilder>() != null);
      Contract.Ensures(name.Length <= 1023);

      return default(TypeBuilder);
    }

    public TypeBuilder DefineNestedType(string name, System.Reflection.TypeAttributes attr, Type parent)
    {
      Contract.Ensures(0 <= name.Length);
      Contract.Ensures(Contract.Result<System.Reflection.Emit.TypeBuilder>() != null);
      Contract.Ensures(name.Length <= 1023);

      return default(TypeBuilder);
    }

    public MethodBuilder DefinePInvokeMethod(string name, string dllName, System.Reflection.MethodAttributes attributes, System.Reflection.CallingConventions callingConvention, Type returnType, Type[] parameterTypes, System.Runtime.InteropServices.CallingConvention nativeCallConv, System.Runtime.InteropServices.CharSet nativeCharSet)
    {
      Contract.Ensures(Contract.Result<System.Reflection.Emit.MethodBuilder>() != null);

      return default(MethodBuilder);
    }

    public MethodBuilder DefinePInvokeMethod(string name, string dllName, string entryName, System.Reflection.MethodAttributes attributes, System.Reflection.CallingConventions callingConvention, Type returnType, Type[] parameterTypes, System.Runtime.InteropServices.CallingConvention nativeCallConv, System.Runtime.InteropServices.CharSet nativeCharSet)
    {
      Contract.Ensures(Contract.Result<System.Reflection.Emit.MethodBuilder>() != null);

      return default(MethodBuilder);
    }

    public MethodBuilder DefinePInvokeMethod(string name, string dllName, string entryName, System.Reflection.MethodAttributes attributes, System.Reflection.CallingConventions callingConvention, Type returnType, Type[] returnTypeRequiredCustomModifiers, Type[] returnTypeOptionalCustomModifiers, Type[] parameterTypes, Type[][] parameterTypeRequiredCustomModifiers, Type[][] parameterTypeOptionalCustomModifiers, System.Runtime.InteropServices.CallingConvention nativeCallConv, System.Runtime.InteropServices.CharSet nativeCharSet)
    {
      Contract.Ensures(Contract.Result<System.Reflection.Emit.MethodBuilder>() != null);

      return default(MethodBuilder);
    }

    public PropertyBuilder DefineProperty(string name, System.Reflection.PropertyAttributes attributes, Type returnType, Type[] returnTypeRequiredCustomModifiers, Type[] returnTypeOptionalCustomModifiers, Type[] parameterTypes, Type[][] parameterTypeRequiredCustomModifiers, Type[][] parameterTypeOptionalCustomModifiers)
    {
      Contract.Ensures(Contract.Result<System.Reflection.Emit.PropertyBuilder>() != null);

      return default(PropertyBuilder);
    }

    public PropertyBuilder DefineProperty(string name, System.Reflection.PropertyAttributes attributes, Type returnType, Type[] parameterTypes)
    {
      Contract.Ensures(Contract.Result<System.Reflection.Emit.PropertyBuilder>() != null);

      return default(PropertyBuilder);
    }

    public PropertyBuilder DefineProperty(string name, System.Reflection.PropertyAttributes attributes, System.Reflection.CallingConventions callingConvention, Type returnType, Type[] returnTypeRequiredCustomModifiers, Type[] returnTypeOptionalCustomModifiers, Type[] parameterTypes, Type[][] parameterTypeRequiredCustomModifiers, Type[][] parameterTypeOptionalCustomModifiers)
    {
      Contract.Ensures(Contract.Result<System.Reflection.Emit.PropertyBuilder>() != null);

      return default(PropertyBuilder);
    }

    public PropertyBuilder DefineProperty(string name, System.Reflection.PropertyAttributes attributes, System.Reflection.CallingConventions callingConvention, Type returnType, Type[] parameterTypes)
    {
      Contract.Ensures(Contract.Result<System.Reflection.Emit.PropertyBuilder>() != null);

      return default(PropertyBuilder);
    }

    public ConstructorBuilder DefineTypeInitializer()
    {
      Contract.Ensures(Contract.Result<System.Reflection.Emit.ConstructorBuilder>() != null);

      return default(ConstructorBuilder);
    }

    public FieldBuilder DefineUninitializedData(string name, int size, System.Reflection.FieldAttributes attributes)
    {
      Contract.Ensures(Contract.Result<System.Reflection.Emit.FieldBuilder>() != null);

      return default(FieldBuilder);
    }

    protected override System.Reflection.TypeAttributes GetAttributeFlagsImpl()
    {
      return default(System.Reflection.TypeAttributes);
    }

    public static System.Reflection.ConstructorInfo GetConstructor(Type type, System.Reflection.ConstructorInfo constructor)
    {
      Contract.Requires(constructor != null);
      Contract.Requires(constructor.DeclaringType != null);

      return default(System.Reflection.ConstructorInfo);
    }

    protected override System.Reflection.ConstructorInfo GetConstructorImpl(System.Reflection.BindingFlags bindingAttr, System.Reflection.Binder binder, System.Reflection.CallingConventions callConvention, Type[] types, System.Reflection.ParameterModifier[] modifiers)
    {
      return default(System.Reflection.ConstructorInfo);
    }

    public override System.Reflection.ConstructorInfo[] GetConstructors(System.Reflection.BindingFlags bindingAttr)
    {
      return default(System.Reflection.ConstructorInfo[]);
    }

    public override Object[] GetCustomAttributes(bool inherit)
    {
      return default(Object[]);
    }

    public override Object[] GetCustomAttributes(Type attributeType, bool inherit)
    {
      return default(Object[]);
    }

    public override Type GetElementType()
    {
      return default(Type);
    }

    public override System.Reflection.EventInfo GetEvent(string name, System.Reflection.BindingFlags bindingAttr)
    {
      return default(System.Reflection.EventInfo);
    }

    public override System.Reflection.EventInfo[] GetEvents(System.Reflection.BindingFlags bindingAttr)
    {
      return default(System.Reflection.EventInfo[]);
    }

    public override System.Reflection.EventInfo[] GetEvents()
    {
      return default(System.Reflection.EventInfo[]);
    }

    public override System.Reflection.FieldInfo GetField(string name, System.Reflection.BindingFlags bindingAttr)
    {
      return default(System.Reflection.FieldInfo);
    }

    public static System.Reflection.FieldInfo GetField(Type type, System.Reflection.FieldInfo field)
    {
      Contract.Requires(field != null);
      Contract.Requires(field.DeclaringType != null);

      return default(System.Reflection.FieldInfo);
    }

    public override System.Reflection.FieldInfo[] GetFields(System.Reflection.BindingFlags bindingAttr)
    {
      return default(System.Reflection.FieldInfo[]);
    }

    public override Type[] GetGenericArguments()
    {
      return default(Type[]);
    }

    public override Type GetGenericTypeDefinition()
    {
      return default(Type);
    }

    public override Type GetInterface(string name, bool ignoreCase)
    {
      return default(Type);
    }

    public override System.Reflection.InterfaceMapping GetInterfaceMap(Type interfaceType)
    {
      return default(System.Reflection.InterfaceMapping);
    }

    public override Type[] GetInterfaces()
    {
      return default(Type[]);
    }

    public override System.Reflection.MemberInfo[] GetMember(string name, System.Reflection.MemberTypes type, System.Reflection.BindingFlags bindingAttr)
    {
      return default(System.Reflection.MemberInfo[]);
    }

    public override System.Reflection.MemberInfo[] GetMembers(System.Reflection.BindingFlags bindingAttr)
    {
      return default(System.Reflection.MemberInfo[]);
    }

    public static System.Reflection.MethodInfo GetMethod(Type type, System.Reflection.MethodInfo method)
    {
      Contract.Requires(method != null);

      return default(System.Reflection.MethodInfo);
    }

    protected override System.Reflection.MethodInfo GetMethodImpl(string name, System.Reflection.BindingFlags bindingAttr, System.Reflection.Binder binder, System.Reflection.CallingConventions callConvention, Type[] types, System.Reflection.ParameterModifier[] modifiers)
    {
      return default(System.Reflection.MethodInfo);
    }

    public override System.Reflection.MethodInfo[] GetMethods(System.Reflection.BindingFlags bindingAttr)
    {
      return default(System.Reflection.MethodInfo[]);
    }

    public override Type GetNestedType(string name, System.Reflection.BindingFlags bindingAttr)
    {
      return default(Type);
    }

    public override Type[] GetNestedTypes(System.Reflection.BindingFlags bindingAttr)
    {
      return default(Type[]);
    }

    public override System.Reflection.PropertyInfo[] GetProperties(System.Reflection.BindingFlags bindingAttr)
    {
      return default(System.Reflection.PropertyInfo[]);
    }

    protected override System.Reflection.PropertyInfo GetPropertyImpl(string name, System.Reflection.BindingFlags bindingAttr, System.Reflection.Binder binder, Type returnType, Type[] types, System.Reflection.ParameterModifier[] modifiers)
    {
      return default(System.Reflection.PropertyInfo);
    }

    protected override bool HasElementTypeImpl()
    {
      return default(bool);
    }

    public override Object InvokeMember(string name, System.Reflection.BindingFlags invokeAttr, System.Reflection.Binder binder, Object target, Object[] args, System.Reflection.ParameterModifier[] modifiers, System.Globalization.CultureInfo culture, string[] namedParameters)
    {
      return default(Object);
    }

    protected override bool IsArrayImpl()
    {
      return default(bool);
    }

    public override bool IsAssignableFrom(Type c)
    {
      return default(bool);
    }

    protected override bool IsByRefImpl()
    {
      return default(bool);
    }

    protected override bool IsCOMObjectImpl()
    {
      return default(bool);
    }

    public bool IsCreated()
    {
      return default(bool);
    }

    public override bool IsDefined(Type attributeType, bool inherit)
    {
      return default(bool);
    }

    protected override bool IsPointerImpl()
    {
      return default(bool);
    }

    protected override bool IsPrimitiveImpl()
    {
      return default(bool);
    }

    public override bool IsSubclassOf(Type c)
    {
      return default(bool);
    }

    public override Type MakeArrayType()
    {
      return default(Type);
    }

    public override Type MakeArrayType(int rank)
    {
      return default(Type);
    }

    public override Type MakeByRefType()
    {
      return default(Type);
    }

    public override Type MakeGenericType(Type[] typeArguments)
    {
      return default(Type);
    }

    public override Type MakePointerType()
    {
      return default(Type);
    }

    public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
    {
    }

    public void SetCustomAttribute(System.Reflection.ConstructorInfo con, byte[] binaryAttribute)
    {
    }

    public void SetParent(Type parent)
    {
    }

    void System.Runtime.InteropServices._TypeBuilder.GetIDsOfNames(ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
    {
    }

    void System.Runtime.InteropServices._TypeBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
    {
    }

    void System.Runtime.InteropServices._TypeBuilder.GetTypeInfoCount(out uint pcTInfo)
    {
      pcTInfo = default(uint);
    }

    void System.Runtime.InteropServices._TypeBuilder.Invoke(uint dispIdMember, ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
    {
    }

    public override string ToString()
    {
      return default(string);
    }

    internal TypeBuilder()
    {
    }
    #endregion

    #region Properties and indexers
    public override System.Reflection.Assembly Assembly
    {
      get
      {
        return default(System.Reflection.Assembly);
      }
    }

    public override string AssemblyQualifiedName
    {
      get
      {
        return default(string);
      }
    }

    public override Type BaseType
    {
      get
      {
        return default(Type);
      }
    }

    public override System.Reflection.MethodBase DeclaringMethod
    {
      get
      {
        return default(System.Reflection.MethodBase);
      }
    }

    public override Type DeclaringType
    {
      get
      {
        return default(Type);
      }
    }

    public override string FullName
    {
      get
      {
        return default(string);
      }
    }

    public override System.Reflection.GenericParameterAttributes GenericParameterAttributes
    {
      get
      {
        return default(System.Reflection.GenericParameterAttributes);
      }
    }

    public override int GenericParameterPosition
    {
      get
      {
        return default(int);
      }
    }

    public override Guid GUID
    {
      get
      {
        return default(Guid);
      }
    }

    public override bool IsGenericParameter
    {
      get
      {
        return default(bool);
      }
    }

    public override bool IsGenericType
    {
      get
      {
        return default(bool);
      }
    }

    public override bool IsGenericTypeDefinition
    {
      get
      {
        return default(bool);
      }
    }

    public override bool IsSecurityCritical
    {
      get
      {
        return default(bool);
      }
    }

    public override bool IsSecuritySafeCritical
    {
      get
      {
        return default(bool);
      }
    }

    public override bool IsSecurityTransparent
    {
      get
      {
        return default(bool);
      }
    }

    public override System.Reflection.Module Module
    {
      get
      {
        return default(System.Reflection.Module);
      }
    }

    public override string Name
    {
      get
      {
        return default(string);
      }
    }

    public override string Namespace
    {
      get
      {
        return default(string);
      }
    }

    public PackingSize PackingSize
    {
      get
      {
        return default(PackingSize);
      }
    }

    public override Type ReflectedType
    {
      get
      {
        return default(Type);
      }
    }

    public int Size
    {
      get
      {
        return default(int);
      }
    }

    public override RuntimeTypeHandle TypeHandle
    {
      get
      {
        return default(RuntimeTypeHandle);
      }
    }

    public TypeToken TypeToken
    {
      get
      {
        return default(TypeToken);
      }
    }

    public override Type UnderlyingSystemType
    {
      get
      {
        return default(Type);
      }
    }
    #endregion

    #region Fields
    public static int UnspecifiedTypeSize;
    #endregion
  }
}
