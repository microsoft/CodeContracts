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

// File System.Runtime.InteropServices._MethodBase.cs
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
  public partial interface _MethodBase
  {
    #region Methods and constructors
    bool Equals(Object other);

    Object[] GetCustomAttributes(Type attributeType, bool inherit);

    Object[] GetCustomAttributes(bool inherit);

    int GetHashCode();

    void GetIDsOfNames(ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

    System.Reflection.MethodImplAttributes GetMethodImplementationFlags();

    System.Reflection.ParameterInfo[] GetParameters();

    Type GetType();

    void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

    void GetTypeInfoCount(out uint pcTInfo);

    void Invoke(uint dispIdMember, ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);

    Object Invoke(Object obj, Object[] parameters);

    Object Invoke(Object obj, System.Reflection.BindingFlags invokeAttr, System.Reflection.Binder binder, Object[] parameters, System.Globalization.CultureInfo culture);

    bool IsDefined(Type attributeType, bool inherit);

    string ToString();
    #endregion

    #region Properties and indexers
    System.Reflection.MethodAttributes Attributes
    {
      get;
    }

    System.Reflection.CallingConventions CallingConvention
    {
      get;
    }

    Type DeclaringType
    {
      get;
    }

    bool IsAbstract
    {
      get;
    }

    bool IsAssembly
    {
      get;
    }

    bool IsConstructor
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

    bool IsFinal
    {
      get;
    }

    bool IsHideBySig
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

    bool IsVirtual
    {
      get;
    }

    System.Reflection.MemberTypes MemberType
    {
      get;
    }

    RuntimeMethodHandle MethodHandle
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
