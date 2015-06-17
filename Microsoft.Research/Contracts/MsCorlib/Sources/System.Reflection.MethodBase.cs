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

// File System.Reflection.MethodBase.cs
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
  abstract public partial class MethodBase : MemberInfo, System.Runtime.InteropServices._MethodBase
  {
    #region Methods and constructors
    public static bool operator != (System.Reflection.MethodBase left, System.Reflection.MethodBase right)
    {
      Contract.Ensures(Contract.Result<bool>() == ((left.Equals(right)) == false));

      return default(bool);
    }

    public static bool operator == (System.Reflection.MethodBase left, System.Reflection.MethodBase right)
    {
      return default(bool);
    }

    public override bool Equals(Object obj)
    {
      return default(bool);
    }

    public static MethodBase GetCurrentMethod()
    {
      return default(MethodBase);
    }

    public virtual new Type[] GetGenericArguments()
    {
      return default(Type[]);
    }

    public override int GetHashCode()
    {
      return default(int);
    }

    public virtual new MethodBody GetMethodBody()
    {
      return default(MethodBody);
    }

    public static MethodBase GetMethodFromHandle(RuntimeMethodHandle handle, RuntimeTypeHandle declaringType)
    {
      return default(MethodBase);
    }

    public static MethodBase GetMethodFromHandle(RuntimeMethodHandle handle)
    {
      Contract.Ensures(Contract.Result<System.Reflection.MethodBase>() != null);

      return default(MethodBase);
    }

    public abstract MethodImplAttributes GetMethodImplementationFlags();

    public abstract ParameterInfo[] GetParameters();

    public abstract Object Invoke(Object obj, BindingFlags invokeAttr, Binder binder, Object[] parameters, System.Globalization.CultureInfo culture);

    public Object Invoke(Object obj, Object[] parameters)
    {
      return default(Object);
    }

    protected MethodBase()
    {
    }

    void System.Runtime.InteropServices._MethodBase.GetIDsOfNames(ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
    {
    }

    Type System.Runtime.InteropServices._MethodBase.GetType()
    {
      return default(Type);
    }

    void System.Runtime.InteropServices._MethodBase.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
    {
    }

    void System.Runtime.InteropServices._MethodBase.GetTypeInfoCount(out uint pcTInfo)
    {
      pcTInfo = default(uint);
    }

    void System.Runtime.InteropServices._MethodBase.Invoke(uint dispIdMember, ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
    {
    }
    #endregion

    #region Properties and indexers
    public abstract MethodAttributes Attributes
    {
      get;
    }

    public virtual new CallingConventions CallingConvention
    {
      get
      {
        return default(CallingConventions);
      }
    }

    public virtual new bool ContainsGenericParameters
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

    public bool IsAssembly
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsConstructor
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

    public bool IsFinal
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool IsGenericMethod
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool IsGenericMethodDefinition
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsHideBySig
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

    public bool IsVirtual
    {
      get
      {
        return default(bool);
      }
    }

    public abstract RuntimeMethodHandle MethodHandle
    {
      get;
    }

    bool System.Runtime.InteropServices._MethodBase.IsAbstract
    {
      get
      {
        return default(bool);
      }
    }

    bool System.Runtime.InteropServices._MethodBase.IsAssembly
    {
      get
      {
        return default(bool);
      }
    }

    bool System.Runtime.InteropServices._MethodBase.IsConstructor
    {
      get
      {
        return default(bool);
      }
    }

    bool System.Runtime.InteropServices._MethodBase.IsFamily
    {
      get
      {
        return default(bool);
      }
    }

    bool System.Runtime.InteropServices._MethodBase.IsFamilyAndAssembly
    {
      get
      {
        return default(bool);
      }
    }

    bool System.Runtime.InteropServices._MethodBase.IsFamilyOrAssembly
    {
      get
      {
        return default(bool);
      }
    }

    bool System.Runtime.InteropServices._MethodBase.IsFinal
    {
      get
      {
        return default(bool);
      }
    }

    bool System.Runtime.InteropServices._MethodBase.IsHideBySig
    {
      get
      {
        return default(bool);
      }
    }

    bool System.Runtime.InteropServices._MethodBase.IsPrivate
    {
      get
      {
        return default(bool);
      }
    }

    bool System.Runtime.InteropServices._MethodBase.IsPublic
    {
      get
      {
        return default(bool);
      }
    }

    bool System.Runtime.InteropServices._MethodBase.IsSpecialName
    {
      get
      {
        return default(bool);
      }
    }

    bool System.Runtime.InteropServices._MethodBase.IsStatic
    {
      get
      {
        return default(bool);
      }
    }

    bool System.Runtime.InteropServices._MethodBase.IsVirtual
    {
      get
      {
        return default(bool);
      }
    }
    #endregion
  }
}
