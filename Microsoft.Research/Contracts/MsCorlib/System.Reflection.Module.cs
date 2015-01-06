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

using System;
using System.Diagnostics.Contracts;

namespace System.Reflection
{

  public class Module
  {
#if NETFRAMEWORK_4_0 || SILVERLIGHT_5_0
    protected
#else
    internal 
#endif
      Module() { }

#if !SILVERLIGHT_5_0
    public virtual string FullyQualifiedName
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
    }
#endif

#if SILVERLIGHT_4_0 || NETFRAMEWORK_4_0 || SILVERLIGHT_5_0
    virtual
#endif
    public string Name
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
    }

#if !SILVERLIGHT
#if NETFRAMEWORK_4_0
    virtual
#endif
    public string ScopeName
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }

    }
#endif

#if SILVERLIGHT_4_0 || NETFRAMEWORK_4_0 || SILVERLIGHT_5_0
    virtual
#endif
    public Assembly Assembly
    {
      get
      {
        Contract.Ensures(Contract.Result<Assembly>() != null);
        return default(Assembly);
      }
    }

#if !SILVERLIGHT
    [Pure]
#if NETFRAMEWORK_4_0
    virtual
#endif
    extern public bool IsResource();
#endif

#if SILVERLIGHT_4_0 || NETFRAMEWORK_4_0 || SILVERLIGHT_5_0
    virtual
#endif
    public FieldInfo GetField(string name, BindingFlags bindingAttr)
    {
      Contract.Requires(name != null);

      return default(FieldInfo);
    }
    public FieldInfo GetField(string name)
    {
      Contract.Requires(name != null);

      return default(FieldInfo);
    }
    public FieldInfo[] GetFields()
    {

      return default(FieldInfo[]);
    }
    public MethodInfo GetMethod(string name)
    {
      Contract.Requires(name != null);

      return default(MethodInfo);
    }
    public MethodInfo GetMethod(string name, Type[] types)
    {
      Contract.Requires(name != null);
      Contract.Requires(types != null);

      return default(MethodInfo);
    }
#if false
    public MethodInfo GetMethod(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
    {
      Contract.Requires(name != null);
      Contract.Requires(types != null);

      return default(MethodInfo);
    }
#endif
    public MethodInfo[] GetMethods()
    {

      return default(MethodInfo[]);
    }
#if false
    public System.Security.Cryptography.X509Certificates.X509Certificate GetSignerCertificate()
    {

      return default(System.Security.Cryptography.X509Certificates.X509Certificate);
    }
#endif

    public virtual Type[] GetTypes()
    {
      Contract.Ensures(Contract.Result<Type[]>() != null);
      return default(Type[]);
    }

#if !SILVERLIGHT
    public virtual Type[] FindTypes(TypeFilter filter, object filterCriteria)
    {
      return default(Type[]);
    }
#endif

    public virtual Type GetType(string className, bool throwOnError, bool ignoreCase)
    {

      return default(Type);
    }
    public virtual Type GetType(string className)
    {

      return default(Type);
    }
    public virtual Type GetType(string className, bool ignoreCase)
    {
      return default(Type);
    }
  }
}
