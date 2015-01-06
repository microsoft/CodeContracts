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

// File System.Reflection.Emit.ConstructorBuilder.cs
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
  sealed public partial class ConstructorBuilder : System.Reflection.ConstructorInfo, System.Runtime.InteropServices._ConstructorBuilder
  {
    #region Methods and constructors
    public void AddDeclarativeSecurity(System.Security.Permissions.SecurityAction action, System.Security.PermissionSet pset)
    {
    }

    internal ConstructorBuilder()
    {
    }

    public ParameterBuilder DefineParameter(int iSequence, System.Reflection.ParameterAttributes attributes, string strParamName)
    {
      Contract.Ensures(Contract.Result<System.Reflection.Emit.ParameterBuilder>() != null);

      return default(ParameterBuilder);
    }

    public override Object[] GetCustomAttributes(Type attributeType, bool inherit)
    {
      return default(Object[]);
    }

    public override Object[] GetCustomAttributes(bool inherit)
    {
      return default(Object[]);
    }

    public ILGenerator GetILGenerator(int streamSize)
    {
      Contract.Ensures(Contract.Result<System.Reflection.Emit.ILGenerator>() != null);

      return default(ILGenerator);
    }

    public ILGenerator GetILGenerator()
    {
      Contract.Ensures(Contract.Result<System.Reflection.Emit.ILGenerator>() != null);

      return default(ILGenerator);
    }

    public override System.Reflection.MethodImplAttributes GetMethodImplementationFlags()
    {
      return default(System.Reflection.MethodImplAttributes);
    }

    public System.Reflection.Module GetModule()
    {
      return default(System.Reflection.Module);
    }

    public override System.Reflection.ParameterInfo[] GetParameters()
    {
      return default(System.Reflection.ParameterInfo[]);
    }

    public MethodToken GetToken()
    {
      return default(MethodToken);
    }

    public override Object Invoke(Object obj, System.Reflection.BindingFlags invokeAttr, System.Reflection.Binder binder, Object[] parameters, System.Globalization.CultureInfo culture)
    {
      return default(Object);
    }

    public override Object Invoke(System.Reflection.BindingFlags invokeAttr, System.Reflection.Binder binder, Object[] parameters, System.Globalization.CultureInfo culture)
    {
      return default(Object);
    }

    public override bool IsDefined(Type attributeType, bool inherit)
    {
      return default(bool);
    }

    public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
    {
    }

    public void SetCustomAttribute(System.Reflection.ConstructorInfo con, byte[] binaryAttribute)
    {
    }

    public void SetImplementationFlags(System.Reflection.MethodImplAttributes attributes)
    {
    }

    public void SetSymCustomAttribute(string name, byte[] data)
    {
    }

    void System.Runtime.InteropServices._ConstructorBuilder.GetIDsOfNames(ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
    {
    }

    void System.Runtime.InteropServices._ConstructorBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
    {
    }

    void System.Runtime.InteropServices._ConstructorBuilder.GetTypeInfoCount(out uint pcTInfo)
    {
      pcTInfo = default(uint);
    }

    void System.Runtime.InteropServices._ConstructorBuilder.Invoke(uint dispIdMember, ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
    {
    }

    public override string ToString()
    {
      return default(string);
    }
    #endregion

    #region Properties and indexers
    public override System.Reflection.MethodAttributes Attributes
    {
      get
      {
        return default(System.Reflection.MethodAttributes);
      }
    }

    public override System.Reflection.CallingConventions CallingConvention
    {
      get
      {
        return default(System.Reflection.CallingConventions);
      }
    }

    public override Type DeclaringType
    {
      get
      {
        return default(Type);
      }
    }

    public bool InitLocals
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public override RuntimeMethodHandle MethodHandle
    {
      get
      {
        return default(RuntimeMethodHandle);
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

    public override Type ReflectedType
    {
      get
      {
        return default(Type);
      }
    }

    public Type ReturnType
    {
      get
      {
        return default(Type);
      }
    }

    public string Signature
    {
      get
      {
        return default(string);
      }
    }
    #endregion
  }
}
