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

// File System.Attribute.cs
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
  abstract public partial class Attribute : System.Runtime.InteropServices._Attribute
  {
    #region Methods and constructors
    protected Attribute()
    {
    }

    public override bool Equals(Object obj)
    {
      return default(bool);
    }

    public static Attribute GetCustomAttribute(System.Reflection.Module element, Type attributeType)
    {
      return default(Attribute);
    }

    public static Attribute GetCustomAttribute(System.Reflection.MemberInfo element, Type attributeType, bool inherit)
    {
      return default(Attribute);
    }

    public static Attribute GetCustomAttribute(System.Reflection.Assembly element, Type attributeType)
    {
      return default(Attribute);
    }

    public static Attribute GetCustomAttribute(System.Reflection.ParameterInfo element, Type attributeType, bool inherit)
    {
      return default(Attribute);
    }

    public static Attribute GetCustomAttribute(System.Reflection.Assembly element, Type attributeType, bool inherit)
    {
      return default(Attribute);
    }

    public static Attribute GetCustomAttribute(System.Reflection.ParameterInfo element, Type attributeType)
    {
      return default(Attribute);
    }

    public static Attribute GetCustomAttribute(System.Reflection.Module element, Type attributeType, bool inherit)
    {
      return default(Attribute);
    }

    public static Attribute GetCustomAttribute(System.Reflection.MemberInfo element, Type attributeType)
    {
      return default(Attribute);
    }

    public static Attribute[] GetCustomAttributes(System.Reflection.Assembly element, bool inherit)
    {
      return default(Attribute[]);
    }

    public static Attribute[] GetCustomAttributes(System.Reflection.Module element, bool inherit)
    {
      return default(Attribute[]);
    }

    public static Attribute[] GetCustomAttributes(System.Reflection.Module element)
    {
      return default(Attribute[]);
    }

    public static Attribute[] GetCustomAttributes(System.Reflection.Assembly element, Type attributeType, bool inherit)
    {
      return default(Attribute[]);
    }

    public static Attribute[] GetCustomAttributes(System.Reflection.Assembly element, Type attributeType)
    {
      return default(Attribute[]);
    }

    public static Attribute[] GetCustomAttributes(System.Reflection.Assembly element)
    {
      return default(Attribute[]);
    }

    public static Attribute[] GetCustomAttributes(System.Reflection.Module element, Type attributeType, bool inherit)
    {
      return default(Attribute[]);
    }

    public static Attribute[] GetCustomAttributes(System.Reflection.MemberInfo element, bool inherit)
    {
      return default(Attribute[]);
    }

    public static Attribute[] GetCustomAttributes(System.Reflection.ParameterInfo element)
    {
      return default(Attribute[]);
    }

    public static Attribute[] GetCustomAttributes(System.Reflection.MemberInfo element, Type type, bool inherit)
    {
      return default(Attribute[]);
    }

    public static Attribute[] GetCustomAttributes(System.Reflection.MemberInfo element)
    {
      return default(Attribute[]);
    }

    public static Attribute[] GetCustomAttributes(System.Reflection.ParameterInfo element, Type attributeType)
    {
      return default(Attribute[]);
    }

    public static Attribute[] GetCustomAttributes(System.Reflection.ParameterInfo element, bool inherit)
    {
      return default(Attribute[]);
    }

    public static Attribute[] GetCustomAttributes(System.Reflection.MemberInfo element, Type type)
    {
      return default(Attribute[]);
    }

    public static Attribute[] GetCustomAttributes(System.Reflection.Module element, Type attributeType)
    {
      return default(Attribute[]);
    }

    public static Attribute[] GetCustomAttributes(System.Reflection.ParameterInfo element, Type attributeType, bool inherit)
    {
      return default(Attribute[]);
    }

    public override int GetHashCode()
    {
      return default(int);
    }

    public virtual new bool IsDefaultAttribute()
    {
      return default(bool);
    }

    public static bool IsDefined(System.Reflection.Assembly element, Type attributeType, bool inherit)
    {
      return default(bool);
    }

    public static bool IsDefined(System.Reflection.ParameterInfo element, Type attributeType)
    {
      Contract.Requires(element.Member != null);

      return default(bool);
    }

    public static bool IsDefined(System.Reflection.MemberInfo element, Type attributeType, bool inherit)
    {
      return default(bool);
    }

    public static bool IsDefined(System.Reflection.MemberInfo element, Type attributeType)
    {
      return default(bool);
    }

    public static bool IsDefined(System.Reflection.ParameterInfo element, Type attributeType, bool inherit)
    {
      Contract.Requires(element.Member != null);

      return default(bool);
    }

    public static bool IsDefined(System.Reflection.Assembly element, Type attributeType)
    {
      return default(bool);
    }

    public static bool IsDefined(System.Reflection.Module element, Type attributeType, bool inherit)
    {
      return default(bool);
    }

    public static bool IsDefined(System.Reflection.Module element, Type attributeType)
    {
      return default(bool);
    }

    public virtual new bool Match(Object obj)
    {
      return default(bool);
    }

    void System.Runtime.InteropServices._Attribute.GetIDsOfNames(ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
    {
    }

    void System.Runtime.InteropServices._Attribute.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
    {
    }

    void System.Runtime.InteropServices._Attribute.GetTypeInfoCount(out uint pcTInfo)
    {
      pcTInfo = default(uint);
    }

    void System.Runtime.InteropServices._Attribute.Invoke(uint dispIdMember, ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
    {
    }
    #endregion

    #region Properties and indexers
    public virtual new Object TypeId
    {
      get
      {
        return default(Object);
      }
    }
    #endregion
  }
}
