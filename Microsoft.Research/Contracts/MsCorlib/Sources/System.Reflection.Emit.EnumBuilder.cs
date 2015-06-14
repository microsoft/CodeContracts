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

// File System.Reflection.Emit.EnumBuilder.cs
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
  sealed public partial class EnumBuilder : Type, System.Runtime.InteropServices._EnumBuilder
  {
    #region Methods and constructors
    public Type CreateType()
    {
      return default(Type);
    }

    public FieldBuilder DefineLiteral(string literalName, Object literalValue)
    {
      Contract.Ensures(Contract.Result<System.Reflection.Emit.FieldBuilder>() != null);

      return default(FieldBuilder);
    }

    internal EnumBuilder()
    {
    }

    protected override System.Reflection.TypeAttributes GetAttributeFlagsImpl()
    {
      return default(System.Reflection.TypeAttributes);
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

    public override Type GetEnumUnderlyingType()
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

    public override System.Reflection.FieldInfo[] GetFields(System.Reflection.BindingFlags bindingAttr)
    {
      return default(System.Reflection.FieldInfo[]);
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

    protected override bool IsByRefImpl()
    {
      return default(bool);
    }

    protected override bool IsCOMObjectImpl()
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

    protected override bool IsValueTypeImpl()
    {
      return default(bool);
    }

    public override Type MakeArrayType(int rank)
    {
      return default(Type);
    }

    public override Type MakeArrayType()
    {
      return default(Type);
    }

    public override Type MakeByRefType()
    {
      return default(Type);
    }

    public override Type MakePointerType()
    {
      return default(Type);
    }

    public void SetCustomAttribute(System.Reflection.ConstructorInfo con, byte[] binaryAttribute)
    {
    }

    public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
    {
    }

    void System.Runtime.InteropServices._EnumBuilder.GetIDsOfNames(ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
    {
    }

    void System.Runtime.InteropServices._EnumBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
    {
    }

    void System.Runtime.InteropServices._EnumBuilder.GetTypeInfoCount(out uint pcTInfo)
    {
      pcTInfo = default(uint);
    }

    void System.Runtime.InteropServices._EnumBuilder.Invoke(uint dispIdMember, ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
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

    public override Guid GUID
    {
      get
      {
        return default(Guid);
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

    public override Type ReflectedType
    {
      get
      {
        return default(Type);
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

    public FieldBuilder UnderlyingField
    {
      get
      {
        return default(FieldBuilder);
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
  }
}
