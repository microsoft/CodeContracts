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

// File System.Runtime.InteropServices._PropertyInfo.cs
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
  public partial interface _PropertyInfo
  {
    #region Methods and constructors
    bool Equals(Object other);

    System.Reflection.MethodInfo[] GetAccessors();

    System.Reflection.MethodInfo[] GetAccessors(bool nonPublic);

    Object[] GetCustomAttributes(bool inherit);

    Object[] GetCustomAttributes(Type attributeType, bool inherit);

    System.Reflection.MethodInfo GetGetMethod(bool nonPublic);

    System.Reflection.MethodInfo GetGetMethod();

    int GetHashCode();

    void GetIDsOfNames(ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

    System.Reflection.ParameterInfo[] GetIndexParameters();

    System.Reflection.MethodInfo GetSetMethod();

    System.Reflection.MethodInfo GetSetMethod(bool nonPublic);

    Type GetType();

    void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

    void GetTypeInfoCount(out uint pcTInfo);

    Object GetValue(Object obj, System.Reflection.BindingFlags invokeAttr, System.Reflection.Binder binder, Object[] index, System.Globalization.CultureInfo culture);

    Object GetValue(Object obj, Object[] index);

    void Invoke(uint dispIdMember, ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);

    bool IsDefined(Type attributeType, bool inherit);

    void SetValue(Object obj, Object value, System.Reflection.BindingFlags invokeAttr, System.Reflection.Binder binder, Object[] index, System.Globalization.CultureInfo culture);

    void SetValue(Object obj, Object value, Object[] index);

    string ToString();
    #endregion

    #region Properties and indexers
    System.Reflection.PropertyAttributes Attributes
    {
      get;
    }

    bool CanRead
    {
      get;
    }

    bool CanWrite
    {
      get;
    }

    Type DeclaringType
    {
      get;
    }

    bool IsSpecialName
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

    Type PropertyType
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
