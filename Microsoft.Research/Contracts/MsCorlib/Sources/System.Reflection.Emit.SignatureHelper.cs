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

// File System.Reflection.Emit.SignatureHelper.cs
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
  sealed public partial class SignatureHelper : System.Runtime.InteropServices._SignatureHelper
  {
    #region Methods and constructors
    public void AddArgument(Type argument, Type[] requiredCustomModifiers, Type[] optionalCustomModifiers)
    {
    }

    public void AddArgument(Type clsArgument)
    {
    }

    public void AddArgument(Type argument, bool pinned)
    {
    }

    public void AddArguments(Type[] arguments, Type[][] requiredCustomModifiers, Type[][] optionalCustomModifiers)
    {
    }

    public void AddSentinel()
    {
    }

    public override bool Equals(Object obj)
    {
      return default(bool);
    }

    public static SignatureHelper GetFieldSigHelper(System.Reflection.Module mod)
    {
      Contract.Ensures(Contract.Result<System.Reflection.Emit.SignatureHelper>() != null);

      return default(SignatureHelper);
    }

    public override int GetHashCode()
    {
      return default(int);
    }

    public static SignatureHelper GetLocalVarSigHelper(System.Reflection.Module mod)
    {
      Contract.Ensures(Contract.Result<System.Reflection.Emit.SignatureHelper>() != null);

      return default(SignatureHelper);
    }

    public static SignatureHelper GetLocalVarSigHelper()
    {
      Contract.Ensures(Contract.Result<System.Reflection.Emit.SignatureHelper>() != null);

      return default(SignatureHelper);
    }

    public static SignatureHelper GetMethodSigHelper(System.Reflection.Module mod, Type returnType, Type[] parameterTypes)
    {
      Contract.Ensures(Contract.Result<System.Reflection.Emit.SignatureHelper>() != null);

      return default(SignatureHelper);
    }

    public static SignatureHelper GetMethodSigHelper(System.Runtime.InteropServices.CallingConvention unmanagedCallingConvention, Type returnType)
    {
      Contract.Ensures(Contract.Result<System.Reflection.Emit.SignatureHelper>() != null);

      return default(SignatureHelper);
    }

    public static SignatureHelper GetMethodSigHelper(System.Reflection.CallingConventions callingConvention, Type returnType)
    {
      Contract.Ensures(Contract.Result<System.Reflection.Emit.SignatureHelper>() != null);

      return default(SignatureHelper);
    }

    public static SignatureHelper GetMethodSigHelper(System.Reflection.Module mod, System.Runtime.InteropServices.CallingConvention unmanagedCallConv, Type returnType)
    {
      Contract.Ensures(Contract.Result<System.Reflection.Emit.SignatureHelper>() != null);

      return default(SignatureHelper);
    }

    public static SignatureHelper GetMethodSigHelper(System.Reflection.Module mod, System.Reflection.CallingConventions callingConvention, Type returnType)
    {
      Contract.Ensures(Contract.Result<System.Reflection.Emit.SignatureHelper>() != null);

      return default(SignatureHelper);
    }

    public static SignatureHelper GetPropertySigHelper(System.Reflection.Module mod, System.Reflection.CallingConventions callingConvention, Type returnType, Type[] requiredReturnTypeCustomModifiers, Type[] optionalReturnTypeCustomModifiers, Type[] parameterTypes, Type[][] requiredParameterTypeCustomModifiers, Type[][] optionalParameterTypeCustomModifiers)
    {
      Contract.Ensures(Contract.Result<System.Reflection.Emit.SignatureHelper>() != null);

      return default(SignatureHelper);
    }

    public static SignatureHelper GetPropertySigHelper(System.Reflection.Module mod, Type returnType, Type[] requiredReturnTypeCustomModifiers, Type[] optionalReturnTypeCustomModifiers, Type[] parameterTypes, Type[][] requiredParameterTypeCustomModifiers, Type[][] optionalParameterTypeCustomModifiers)
    {
      Contract.Ensures(Contract.Result<System.Reflection.Emit.SignatureHelper>() != null);

      return default(SignatureHelper);
    }

    public static SignatureHelper GetPropertySigHelper(System.Reflection.Module mod, Type returnType, Type[] parameterTypes)
    {
      Contract.Ensures(Contract.Result<System.Reflection.Emit.SignatureHelper>() != null);

      return default(SignatureHelper);
    }

    public byte[] GetSignature()
    {
      Contract.Ensures(Contract.Result<byte[]>() != null);

      return default(byte[]);
    }

    internal SignatureHelper()
    {
    }

    void System.Runtime.InteropServices._SignatureHelper.GetIDsOfNames(ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
    {
    }

    void System.Runtime.InteropServices._SignatureHelper.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
    {
    }

    void System.Runtime.InteropServices._SignatureHelper.GetTypeInfoCount(out uint pcTInfo)
    {
      pcTInfo = default(uint);
    }

    void System.Runtime.InteropServices._SignatureHelper.Invoke(uint dispIdMember, ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
    {
    }

    public override string ToString()
    {
      return default(string);
    }
    #endregion
  }
}
