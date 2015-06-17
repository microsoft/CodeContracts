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

// File System.Runtime.InteropServices._FieldInfo.cs
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
  public partial interface _FieldInfo
  {
    #region Methods and constructors
    bool Equals(Object other);

    Object[] GetCustomAttributes(Type attributeType, bool inherit);

    Object[] GetCustomAttributes(bool inherit);

    int GetHashCode();

    void GetIDsOfNames(ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

    Type GetType();

    void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

    void GetTypeInfoCount(out uint pcTInfo);

    Object GetValue(Object obj);

    Object GetValueDirect(TypedReference obj);

    void Invoke(uint dispIdMember, ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);

    bool IsDefined(Type attributeType, bool inherit);

    void SetValue(Object obj, Object value);

    void SetValue(Object obj, Object value, System.Reflection.BindingFlags invokeAttr, System.Reflection.Binder binder, System.Globalization.CultureInfo culture);

    void SetValueDirect(TypedReference obj, Object value);

    string ToString();
    #endregion

    #region Properties and indexers
    System.Reflection.FieldAttributes Attributes
    {
      get;
    }

    Type DeclaringType
    {
      get;
    }

    RuntimeFieldHandle FieldHandle
    {
      get;
    }

    Type FieldType
    {
      get;
    }

    bool IsAssembly
    {
      get;
    }

    bool IsFamily
    {
      get;
    }

    bool IsFamilyAndAssembly
    {
      get;
    }

    bool IsFamilyOrAssembly
    {
      get;
    }

    bool IsInitOnly
    {
      get;
    }

    bool IsLiteral
    {
      get;
    }

    bool IsNotSerialized
    {
      get;
    }

    bool IsPinvokeImpl
    {
      get;
    }

    bool IsPrivate
    {
      get;
    }

    bool IsPublic
    {
      get;
    }

    bool IsSpecialName
    {
      get;
    }

    bool IsStatic
    {
      get;
    }

    System.Reflection.MemberTypes MemberType
    {
      get;
    }

    string Name
    {
      get;
    }

    Type ReflectedType
    {
      get;
    }
    #endregion
  }
}
