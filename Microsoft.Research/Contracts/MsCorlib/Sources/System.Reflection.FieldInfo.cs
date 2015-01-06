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

// File System.Reflection.FieldInfo.cs
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
  abstract public partial class FieldInfo : MemberInfo, System.Runtime.InteropServices._FieldInfo
  {
    #region Methods and constructors
    public static bool operator != (System.Reflection.FieldInfo left, System.Reflection.FieldInfo right)
    {
      Contract.Ensures(Contract.Result<bool>() == ((left.Equals(right)) == false));

      return default(bool);
    }

    public static bool operator == (System.Reflection.FieldInfo left, System.Reflection.FieldInfo right)
    {
      return default(bool);
    }

    public override bool Equals(Object obj)
    {
      return default(bool);
    }

    protected FieldInfo()
    {
    }

    public static FieldInfo GetFieldFromHandle(RuntimeFieldHandle handle, RuntimeTypeHandle declaringType)
    {
      return default(FieldInfo);
    }

    public static FieldInfo GetFieldFromHandle(RuntimeFieldHandle handle)
    {
      Contract.Ensures(Contract.Result<System.Reflection.FieldInfo>() != null);

      return default(FieldInfo);
    }

    public override int GetHashCode()
    {
      return default(int);
    }

    public virtual new Type[] GetOptionalCustomModifiers()
    {
      return default(Type[]);
    }

    public virtual new Object GetRawConstantValue()
    {
      return default(Object);
    }

    public virtual new Type[] GetRequiredCustomModifiers()
    {
      return default(Type[]);
    }

    public abstract Object GetValue(Object obj);

    public virtual new Object GetValueDirect(TypedReference obj)
    {
      return default(Object);
    }

    public void SetValue(Object obj, Object value)
    {
    }

    public abstract void SetValue(Object obj, Object value, BindingFlags invokeAttr, Binder binder, System.Globalization.CultureInfo culture);

    public virtual new void SetValueDirect(TypedReference obj, Object value)
    {
    }

    void System.Runtime.InteropServices._FieldInfo.GetIDsOfNames(ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
    {
    }

    Type System.Runtime.InteropServices._FieldInfo.GetType()
    {
      return default(Type);
    }

    void System.Runtime.InteropServices._FieldInfo.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
    {
    }

    void System.Runtime.InteropServices._FieldInfo.GetTypeInfoCount(out uint pcTInfo)
    {
      pcTInfo = default(uint);
    }

    void System.Runtime.InteropServices._FieldInfo.Invoke(uint dispIdMember, ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
    {
    }
    #endregion

    #region Properties and indexers
    public abstract FieldAttributes Attributes
    {
      get;
    }

    public abstract RuntimeFieldHandle FieldHandle
    {
      get;
    }

    public abstract Type FieldType
    {
      get;
    }

    public bool IsAssembly
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsFamily
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsFamilyAndAssembly
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsFamilyOrAssembly
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsInitOnly
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsLiteral
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsNotSerialized
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsPinvokeImpl
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsPrivate
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

    public bool IsSpecialName
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsStatic
    {
      get
      {
        return default(bool);
      }
    }

    public override MemberTypes MemberType
    {
      get
      {
        return default(MemberTypes);
      }
    }
    #endregion
  }
}
