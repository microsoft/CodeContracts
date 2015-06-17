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

// File System.Reflection.EventInfo.cs
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
  abstract public partial class EventInfo : MemberInfo, System.Runtime.InteropServices._EventInfo
  {
    #region Methods and constructors
    public static bool operator != (System.Reflection.EventInfo left, System.Reflection.EventInfo right)
    {
      Contract.Ensures(Contract.Result<bool>() == ((left.Equals(right)) == false));

      return default(bool);
    }

    public static bool operator == (System.Reflection.EventInfo left, System.Reflection.EventInfo right)
    {
      return default(bool);
    }

    public virtual new void AddEventHandler(Object target, Delegate handler)
    {
    }

    public override bool Equals(Object obj)
    {
      return default(bool);
    }

    protected EventInfo()
    {
    }

    public MethodInfo GetAddMethod()
    {
      return default(MethodInfo);
    }

    public abstract MethodInfo GetAddMethod(bool nonPublic);

    public override int GetHashCode()
    {
      return default(int);
    }

    public virtual new MethodInfo[] GetOtherMethods(bool nonPublic)
    {
      return default(MethodInfo[]);
    }

    public MethodInfo[] GetOtherMethods()
    {
      return default(MethodInfo[]);
    }

    public MethodInfo GetRaiseMethod()
    {
      return default(MethodInfo);
    }

    public abstract MethodInfo GetRaiseMethod(bool nonPublic);

    public abstract MethodInfo GetRemoveMethod(bool nonPublic);

    public MethodInfo GetRemoveMethod()
    {
      return default(MethodInfo);
    }

    public virtual new void RemoveEventHandler(Object target, Delegate handler)
    {
    }

    void System.Runtime.InteropServices._EventInfo.GetIDsOfNames(ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
    {
    }

    Type System.Runtime.InteropServices._EventInfo.GetType()
    {
      return default(Type);
    }

    void System.Runtime.InteropServices._EventInfo.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
    {
    }

    void System.Runtime.InteropServices._EventInfo.GetTypeInfoCount(out uint pcTInfo)
    {
      pcTInfo = default(uint);
    }

    void System.Runtime.InteropServices._EventInfo.Invoke(uint dispIdMember, ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
    {
    }
    #endregion

    #region Properties and indexers
    public abstract EventAttributes Attributes
    {
      get;
    }

    public virtual new Type EventHandlerType
    {
      get
      {
        return default(Type);
      }
    }

    public virtual new bool IsMulticast
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
