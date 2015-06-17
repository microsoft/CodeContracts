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

// File System.Activator.cs
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
  sealed public partial class Activator : System.Runtime.InteropServices._Activator
  {
    #region Methods and constructors
    internal Activator()
    {
    }

    public static System.Runtime.Remoting.ObjectHandle CreateComInstanceFrom(string assemblyName, string typeName)
    {
      return default(System.Runtime.Remoting.ObjectHandle);
    }

    public static System.Runtime.Remoting.ObjectHandle CreateComInstanceFrom(string assemblyName, string typeName, byte[] hashValue, System.Configuration.Assemblies.AssemblyHashAlgorithm hashAlgorithm)
    {
      return default(System.Runtime.Remoting.ObjectHandle);
    }

    public static System.Runtime.Remoting.ObjectHandle CreateInstance(AppDomain domain, string assemblyName, string typeName)
    {
      return default(System.Runtime.Remoting.ObjectHandle);
    }

    public static System.Runtime.Remoting.ObjectHandle CreateInstance(AppDomain domain, string assemblyName, string typeName, bool ignoreCase, System.Reflection.BindingFlags bindingAttr, System.Reflection.Binder binder, Object[] args, System.Globalization.CultureInfo culture, Object[] activationAttributes, System.Security.Policy.Evidence securityAttributes)
    {
      return default(System.Runtime.Remoting.ObjectHandle);
    }

    public static System.Runtime.Remoting.ObjectHandle CreateInstance(string assemblyName, string typeName, bool ignoreCase, System.Reflection.BindingFlags bindingAttr, System.Reflection.Binder binder, Object[] args, System.Globalization.CultureInfo culture, Object[] activationAttributes, System.Security.Policy.Evidence securityInfo)
    {
      return default(System.Runtime.Remoting.ObjectHandle);
    }

    public static System.Runtime.Remoting.ObjectHandle CreateInstance(string assemblyName, string typeName, bool ignoreCase, System.Reflection.BindingFlags bindingAttr, System.Reflection.Binder binder, Object[] args, System.Globalization.CultureInfo culture, Object[] activationAttributes)
    {
      return default(System.Runtime.Remoting.ObjectHandle);
    }

    public static System.Runtime.Remoting.ObjectHandle CreateInstance(ActivationContext activationContext, string[] activationCustomData)
    {
      return default(System.Runtime.Remoting.ObjectHandle);
    }

    public static Object CreateInstance(Type type, System.Reflection.BindingFlags bindingAttr, System.Reflection.Binder binder, Object[] args, System.Globalization.CultureInfo culture)
    {
      return default(Object);
    }

    public static System.Runtime.Remoting.ObjectHandle CreateInstance(AppDomain domain, string assemblyName, string typeName, bool ignoreCase, System.Reflection.BindingFlags bindingAttr, System.Reflection.Binder binder, Object[] args, System.Globalization.CultureInfo culture, Object[] activationAttributes)
    {
      return default(System.Runtime.Remoting.ObjectHandle);
    }

    public static System.Runtime.Remoting.ObjectHandle CreateInstance(ActivationContext activationContext)
    {
      return default(System.Runtime.Remoting.ObjectHandle);
    }

    public static Object CreateInstance(Type type, System.Reflection.BindingFlags bindingAttr, System.Reflection.Binder binder, Object[] args, System.Globalization.CultureInfo culture, Object[] activationAttributes)
    {
      return default(Object);
    }

    public static System.Runtime.Remoting.ObjectHandle CreateInstance(string assemblyName, string typeName, Object[] activationAttributes)
    {
      return default(System.Runtime.Remoting.ObjectHandle);
    }

    public static Object CreateInstance(Type type)
    {
      return default(Object);
    }

    public static Object CreateInstance(Type type, Object[] args)
    {
      return default(Object);
    }

    public static Object CreateInstance(Type type, Object[] args, Object[] activationAttributes)
    {
      return default(Object);
    }

    public static Object CreateInstance(Type type, bool nonPublic)
    {
      return default(Object);
    }

    public static System.Runtime.Remoting.ObjectHandle CreateInstance(string assemblyName, string typeName)
    {
      return default(System.Runtime.Remoting.ObjectHandle);
    }

    public static T CreateInstance<T>()
    {
      Contract.Ensures(Contract.Result<T>() != null);

      return default(T);
    }

    public static System.Runtime.Remoting.ObjectHandle CreateInstanceFrom(string assemblyFile, string typeName)
    {
      return default(System.Runtime.Remoting.ObjectHandle);
    }

    public static System.Runtime.Remoting.ObjectHandle CreateInstanceFrom(AppDomain domain, string assemblyFile, string typeName)
    {
      return default(System.Runtime.Remoting.ObjectHandle);
    }

    public static System.Runtime.Remoting.ObjectHandle CreateInstanceFrom(string assemblyFile, string typeName, Object[] activationAttributes)
    {
      return default(System.Runtime.Remoting.ObjectHandle);
    }

    public static System.Runtime.Remoting.ObjectHandle CreateInstanceFrom(string assemblyFile, string typeName, bool ignoreCase, System.Reflection.BindingFlags bindingAttr, System.Reflection.Binder binder, Object[] args, System.Globalization.CultureInfo culture, Object[] activationAttributes, System.Security.Policy.Evidence securityInfo)
    {
      return default(System.Runtime.Remoting.ObjectHandle);
    }

    public static System.Runtime.Remoting.ObjectHandle CreateInstanceFrom(AppDomain domain, string assemblyFile, string typeName, bool ignoreCase, System.Reflection.BindingFlags bindingAttr, System.Reflection.Binder binder, Object[] args, System.Globalization.CultureInfo culture, Object[] activationAttributes, System.Security.Policy.Evidence securityAttributes)
    {
      return default(System.Runtime.Remoting.ObjectHandle);
    }

    public static System.Runtime.Remoting.ObjectHandle CreateInstanceFrom(AppDomain domain, string assemblyFile, string typeName, bool ignoreCase, System.Reflection.BindingFlags bindingAttr, System.Reflection.Binder binder, Object[] args, System.Globalization.CultureInfo culture, Object[] activationAttributes)
    {
      return default(System.Runtime.Remoting.ObjectHandle);
    }

    public static System.Runtime.Remoting.ObjectHandle CreateInstanceFrom(string assemblyFile, string typeName, bool ignoreCase, System.Reflection.BindingFlags bindingAttr, System.Reflection.Binder binder, Object[] args, System.Globalization.CultureInfo culture, Object[] activationAttributes)
    {
      return default(System.Runtime.Remoting.ObjectHandle);
    }

    public static Object GetObject(Type type, string url, Object state)
    {
      return default(Object);
    }

    public static Object GetObject(Type type, string url)
    {
      return default(Object);
    }

    void System.Runtime.InteropServices._Activator.GetIDsOfNames(ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
    {
    }

    void System.Runtime.InteropServices._Activator.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
    {
    }

    void System.Runtime.InteropServices._Activator.GetTypeInfoCount(out uint pcTInfo)
    {
      pcTInfo = default(uint);
    }

    void System.Runtime.InteropServices._Activator.Invoke(uint dispIdMember, ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
    {
    }
    #endregion
  }
}
