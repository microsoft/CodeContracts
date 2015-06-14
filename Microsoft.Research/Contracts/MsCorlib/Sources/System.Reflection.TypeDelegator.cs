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

// File System.Reflection.TypeDelegator.cs
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


namespace System.Reflection
{
  public partial class TypeDelegator : Type
  {
    #region Methods and constructors
    protected override TypeAttributes GetAttributeFlagsImpl()
    {
      return default(TypeAttributes);
    }

    protected override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
    {
      return default(ConstructorInfo);
    }

    public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
    {
      return default(ConstructorInfo[]);
    }

    public override Object[] GetCustomAttributes(Type attributeType, bool inherit)
    {
      return default(Object[]);
    }

    public override Object[] GetCustomAttributes(bool inherit)
    {
      return default(Object[]);
    }

    public override Type GetElementType()
    {
      return default(Type);
    }

    public override EventInfo GetEvent(string name, BindingFlags bindingAttr)
    {
      return default(EventInfo);
    }

    public override EventInfo[] GetEvents()
    {
      return default(EventInfo[]);
    }

    public override EventInfo[] GetEvents(BindingFlags bindingAttr)
    {
      return default(EventInfo[]);
    }

    public override FieldInfo GetField(string name, BindingFlags bindingAttr)
    {
      return default(FieldInfo);
    }

    public override FieldInfo[] GetFields(BindingFlags bindingAttr)
    {
      return default(FieldInfo[]);
    }

    public override Type GetInterface(string name, bool ignoreCase)
    {
      return default(Type);
    }

    public override InterfaceMapping GetInterfaceMap(Type interfaceType)
    {
      return default(InterfaceMapping);
    }

    public override Type[] GetInterfaces()
    {
      return default(Type[]);
    }

    public override MemberInfo[] GetMember(string name, MemberTypes type, BindingFlags bindingAttr)
    {
      return default(MemberInfo[]);
    }

    public override MemberInfo[] GetMembers(BindingFlags bindingAttr)
    {
      return default(MemberInfo[]);
    }

    protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
    {
      return default(MethodInfo);
    }

    public override MethodInfo[] GetMethods(BindingFlags bindingAttr)
    {
      return default(MethodInfo[]);
    }

    public override Type GetNestedType(string name, BindingFlags bindingAttr)
    {
      return default(Type);
    }

    public override Type[] GetNestedTypes(BindingFlags bindingAttr)
    {
      return default(Type[]);
    }

    public override PropertyInfo[] GetProperties(BindingFlags bindingAttr)
    {
      return default(PropertyInfo[]);
    }

    protected override PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
    {
      return default(PropertyInfo);
    }

    protected override bool HasElementTypeImpl()
    {
      return default(bool);
    }

    public override Object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, Object target, Object[] args, ParameterModifier[] modifiers, System.Globalization.CultureInfo culture, string[] namedParameters)
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

    protected TypeDelegator()
    {
    }

    public TypeDelegator(Type delegatingType)
    {
      Contract.Ensures(delegatingType == this.typeImpl);
      Contract.Ensures(this.typeImpl != null);
    }
    #endregion

    #region Properties and indexers
    public override Assembly Assembly
    {
      get
      {
        return default(Assembly);
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

    public override int MetadataToken
    {
      get
      {
        return default(int);
      }
    }

    public override Module Module
    {
      get
      {
        return default(Module);
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

    public override RuntimeTypeHandle TypeHandle
    {
      get
      {
        return default(RuntimeTypeHandle);
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
    protected Type typeImpl;
    #endregion
  }
}
