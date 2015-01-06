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
#if !SILVERLIGHT_4_0_WP
using System.Runtime.Remoting;
#endif

// Disable the "this variable is not used" warning as every field would imply it.
#pragma warning disable 0414
// Disable the "this variable is never assigned to".
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
  sealed public partial class Activator /* : System.Runtime.InteropServices._Activator */
  {
    #region Methods and constructors
    private Activator ()
    {
    }

#if !SILVERLIGHT
    public static System.Runtime.Remoting.ObjectHandle CreateComInstanceFrom(string assemblyName, string typeName)
    {
      Contract.Requires(assemblyName != null);
      Contract.Requires(typeName != null);

      Contract.Ensures (Contract.Result<System.Runtime.Remoting.ObjectHandle>() != null);

      return default(System.Runtime.Remoting.ObjectHandle);
    }

    public static System.Runtime.Remoting.ObjectHandle CreateComInstanceFrom (string assemblyName, string typeName, byte[] hashValue, System.Configuration.Assemblies.AssemblyHashAlgorithm hashAlgorithm)
    {
      Contract.Requires(assemblyName != null);
      Contract.Requires(typeName != null);

      Contract.Ensures (Contract.Result<System.Runtime.Remoting.ObjectHandle>() != null);

      return default(System.Runtime.Remoting.ObjectHandle);
    }
#endif

#if !SILVERLIGHT
    public static System.Runtime.Remoting.ObjectHandle CreateInstance (AppDomain domain, string assemblyName, string typeName, bool ignoreCase, System.Reflection.BindingFlags bindingAttr, System.Reflection.Binder binder, Object[] args, System.Globalization.CultureInfo culture, Object[] activationAttributes, System.Security.Policy.Evidence securityAttributes)
    {
      Contract.Requires(domain != null);
      Contract.Requires(assemblyName != null);
      Contract.Requires(typeName != null);

      Contract.Ensures(Contract.Result<System.Runtime.Remoting.ObjectHandle>() != null);

      return default(System.Runtime.Remoting.ObjectHandle);
    }

    public static System.Runtime.Remoting.ObjectHandle CreateInstance (AppDomain domain, string assemblyName, string typeName)
    {
      Contract.Requires(domain != null);
      Contract.Requires(assemblyName != null);
      Contract.Requires(typeName != null);

      Contract.Ensures(Contract.Result<System.Runtime.Remoting.ObjectHandle>() != null);

      return default(System.Runtime.Remoting.ObjectHandle);
    }
#endif

#if NETFRAMEWORK_4_0
    public static System.Runtime.Remoting.ObjectHandle CreateInstance (AppDomain domain, string assemblyName, string typeName, bool ignoreCase, System.Reflection.BindingFlags bindingAttr, System.Reflection.Binder binder, Object[] args, System.Globalization.CultureInfo culture, Object[] activationAttributes)
    {
      Contract.Ensures (args.Length >= 0);
      Contract.Ensures (activationAttributes.Length >= 0);

      return default(System.Runtime.Remoting.ObjectHandle);
    }
#endif

#if !SILVERLIGHT
    public static Object CreateInstance (Type type, System.Reflection.BindingFlags bindingAttr, System.Reflection.Binder binder, Object[] args, System.Globalization.CultureInfo culture)
    {
      Contract.Requires(type != null);

      Contract.Ensures(Contract.Result<object>() != null);

      return default(Object);
    }

    public static System.Runtime.Remoting.ObjectHandle CreateInstance (ActivationContext activationContext, string[] activationCustomData)
    {
      Contract.Requires(activationContext != null);

      Contract.Ensures(Contract.Result<System.Runtime.Remoting.ObjectHandle>() != null);

      return default(System.Runtime.Remoting.ObjectHandle);
    }

    public static System.Runtime.Remoting.ObjectHandle CreateInstance (ActivationContext activationContext)
    {
      Contract.Requires(activationContext != null);

      Contract.Ensures(Contract.Result<System.Runtime.Remoting.ObjectHandle>() != null);

      return default(System.Runtime.Remoting.ObjectHandle);
    }
#endif

#if NETFRAMEWORK_4_0
    public static System.Runtime.Remoting.ObjectHandle CreateInstance (string assemblyName, string typeName, bool ignoreCase, System.Reflection.BindingFlags bindingAttr, System.Reflection.Binder binder, Object[] args, System.Globalization.CultureInfo culture, Object[] activationAttributes)
    {
      Contract.Requires(assemblyName != null);
      Contract.Requires(typeName != null);

      Contract.Ensures(Contract.Result<ObjectHandle>() != null);

      return default(System.Runtime.Remoting.ObjectHandle);
    }
#endif

#if !SILVERLIGHT
    public static System.Runtime.Remoting.ObjectHandle CreateInstance (string assemblyName, string typeName)
    {
      Contract.Requires(typeName != null);
      Contract.Ensures(Contract.Result<ObjectHandle>() != null);

      return default(System.Runtime.Remoting.ObjectHandle);
    }
#endif

#if !SILVERLIGHT
    public static System.Runtime.Remoting.ObjectHandle CreateInstance (string assemblyName, string typeName, Object[] activationAttributes)
    {
      Contract.Requires(typeName != null);
      Contract.Ensures(Contract.Result<ObjectHandle>() != null);

      return default(System.Runtime.Remoting.ObjectHandle);
    }
#endif

#if !SILVERLIGHT
    public static Object CreateInstance (Type type, bool nonPublic)
    {
      Contract.Requires(type != null);
      Contract.Ensures(Contract.Result<object>() != null);

      return default(Object);
    }
#endif

    public static Object CreateInstance (Type type)
    {
      Contract.Requires(type != null);
      Contract.Ensures(Contract.Result<object>() != null);
//      Contract.Ensures(Contract.Result<object>() != null || type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>));

      return default(Object);
    }

#if !SILVERLIGHT
    public static Object CreateInstance (Type type, System.Reflection.BindingFlags bindingAttr, System.Reflection.Binder binder, Object[] args, System.Globalization.CultureInfo culture, Object[] activationAttributes)
    {
      Contract.Requires(type != null);
      Contract.Ensures(Contract.Result<object>() != null);
//      Contract.Ensures(Contract.Result<object>() != null || type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>));

      return default(Object);
    }
#endif

    public static Object CreateInstance (Type type, Object[] args)
    {
      Contract.Requires(type != null);
      Contract.Ensures(Contract.Result<object>() != null);
      //Contract.Ensures(Contract.Result<object>() != null || type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>));

      return default(Object);
    }

#if !SILVERLIGHT
    public static Object CreateInstance (Type type, Object[] args, Object[] activationAttributes)
    {
      Contract.Requires(type != null);
      Contract.Ensures(Contract.Result<object>() != null);
      //Contract.Ensures(Contract.Result<object>() != null || type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>));

      return default(Object);
    }
#endif


    public static T CreateInstance<T> ()
    {
      Contract.Ensures(Contract.Result<T>() != null);
      //Contract.Ensures(Contract.Result<T>() != null || typeof(T).IsGenericType && typeof(T).GetGenericTypeDefinition() == typeof(Nullable<>));
      return default(T);
    }

#if !SILVERLIGHT
    public static System.Runtime.Remoting.ObjectHandle CreateInstance (string assemblyName, string typeName, bool ignoreCase, System.Reflection.BindingFlags bindingAttr, System.Reflection.Binder binder, Object[] args, System.Globalization.CultureInfo culture, Object[] activationAttributes, System.Security.Policy.Evidence securityInfo)
    {
      Contract.Requires(assemblyName != null);
      Contract.Requires(typeName != null);
      Contract.Ensures(Contract.Result<ObjectHandle>() != null);

      return default(System.Runtime.Remoting.ObjectHandle);
    }
#endif

#if !SILVERLIGHT
    public static System.Runtime.Remoting.ObjectHandle CreateInstanceFrom (AppDomain domain, string assemblyFile, string typeName)
    {
      Contract.Requires(domain != null);
      Contract.Requires(assemblyFile != null);
      Contract.Requires(typeName != null);
      Contract.Ensures(Contract.Result<ObjectHandle>() != null);

      return default(System.Runtime.Remoting.ObjectHandle);
    }

    public static System.Runtime.Remoting.ObjectHandle CreateInstanceFrom (string assemblyFile, string typeName, bool ignoreCase, System.Reflection.BindingFlags bindingAttr, System.Reflection.Binder binder, Object[] args, System.Globalization.CultureInfo culture, Object[] activationAttributes, System.Security.Policy.Evidence securityInfo)
    {
      Contract.Requires(assemblyFile != null);
      Contract.Requires(typeName != null);
      Contract.Ensures(Contract.Result<ObjectHandle>() != null);

      return default(System.Runtime.Remoting.ObjectHandle);
    }
#endif

#if NETFRAMEWORK_4_0
    public static System.Runtime.Remoting.ObjectHandle CreateInstanceFrom (string assemblyFile, string typeName, bool ignoreCase, System.Reflection.BindingFlags bindingAttr, System.Reflection.Binder binder, Object[] args, System.Globalization.CultureInfo culture, Object[] activationAttributes)
    {
      Contract.Requires(assemblyFile != null);
      Contract.Requires(typeName != null);
      Contract.Ensures(Contract.Result<ObjectHandle>() != null);

      return default(System.Runtime.Remoting.ObjectHandle);
    }
#endif

#if !SILVERLIGHT
    public static System.Runtime.Remoting.ObjectHandle CreateInstanceFrom (string assemblyFile, string typeName, Object[] activationAttributes)
    {
      Contract.Requires(assemblyFile != null);
      Contract.Requires(typeName != null);
      Contract.Ensures(Contract.Result<ObjectHandle>() != null);

      return default(System.Runtime.Remoting.ObjectHandle);
    }
#endif

#if NETFRAMEWORK_4_0
    public static System.Runtime.Remoting.ObjectHandle CreateInstanceFrom (AppDomain domain, string assemblyFile, string typeName, bool ignoreCase, System.Reflection.BindingFlags bindingAttr, System.Reflection.Binder binder, Object[] args, System.Globalization.CultureInfo culture, Object[] activationAttributes)
    {
      Contract.Requires(domain != null);
      Contract.Requires(assemblyFile != null);
      Contract.Requires(typeName != null);
      Contract.Ensures(Contract.Result<ObjectHandle>() != null);

      return default(System.Runtime.Remoting.ObjectHandle);
    }
#endif

#if !SILVERLIGHT
    public static System.Runtime.Remoting.ObjectHandle CreateInstanceFrom (AppDomain domain, string assemblyFile, string typeName, bool ignoreCase, System.Reflection.BindingFlags bindingAttr, System.Reflection.Binder binder, Object[] args, System.Globalization.CultureInfo culture, Object[] activationAttributes, System.Security.Policy.Evidence securityAttributes)
    {
      Contract.Requires(domain != null);
      Contract.Requires(assemblyFile != null);
      Contract.Requires(typeName != null);
      Contract.Ensures(Contract.Result<ObjectHandle>() != null);

      return default(System.Runtime.Remoting.ObjectHandle);
    }
#endif

#if !SILVERLIGHT
    public static System.Runtime.Remoting.ObjectHandle CreateInstanceFrom (string assemblyFile, string typeName)
    {
      Contract.Requires(assemblyFile != null);
      Contract.Requires(typeName != null);
      Contract.Ensures(Contract.Result<ObjectHandle>() != null);

      return default(System.Runtime.Remoting.ObjectHandle);
    }
#endif

#if !SILVERLIGHT
    public static Object GetObject (Type type, string url, Object state)
    {
      Contract.Requires(type != null);
      Contract.Requires(url != null);
      Contract.Ensures(Contract.Result<Object>() != null);

      return default(Object);
    }

    public static Object GetObject (Type type, string url)
    {
      Contract.Requires(type != null);
      Contract.Requires(url != null);
      Contract.Ensures(Contract.Result<Object>() != null);

      return default(Object);
    }
#endif

    #endregion

  }
}
