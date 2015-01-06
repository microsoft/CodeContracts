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
using System.Reflection;
using System.Diagnostics.Contracts;

namespace System
{

  public class AppDomain
  {
    private AppDomain() { }

#if !SILVERLIGHT
    public virtual string RelativeSearchPath
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
    }
#endif

#if !SILVERLIGHT
    public AppDomainSetup SetupInformation
    {
      get
      {
        Contract.Ensures(Contract.Result<AppDomainSetup>() != null);
        return default(AppDomainSetup);
      }
    }
#endif

    public virtual string FriendlyName
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
    }

#if !SILVERLIGHT
    public virtual string BaseDirectory
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
    }
#endif

#if false
    extern public System.Security.Policy.Evidence Evidence
    {
      get;
    }

    extern public bool ShadowCopyFiles
    {
      get;
    }
#endif

#if !SILVERLIGHT
    public virtual string DynamicDirectory
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
    }
#endif

    public static AppDomain CurrentDomain
    {
      get
      {
        Contract.Ensures(Contract.Result<AppDomain>() != null);
        return default(AppDomain);
      }
    }

#if false
    public object CreateInstanceFromAndUnwrap(string assemblyName, string typeName, bool ignoreCase, System.Reflection.BindingFlags bindingAttr, System.Reflection.Binder binder, Object[] args, System.Globalization.CultureInfo culture, Object[] activationAttributes, System.Security.Policy.Evidence securityAttributes)
    {
      Contract.Ensures(Contract.Result<object>() != null);

      return default(object);
    }
#endif

#if !SILVERLIGHT
    public object CreateInstanceFromAndUnwrap(string assemblyName, string typeName, Object[] activationAttributes)
    {
      Contract.Ensures(Contract.Result<object>() != null);

      return default(object);
    }

    public object CreateInstanceFromAndUnwrap(string assemblyName, string typeName)
    {
      Contract.Ensures(Contract.Result<object>() != null);

      return default(object);
    }
#endif


#if false
    public object CreateInstanceAndUnwrap(string assemblyName, string typeName, bool ignoreCase, System.Reflection.BindingFlags bindingAttr, System.Reflection.Binder binder, Object[] args, System.Globalization.CultureInfo culture, Object[] activationAttributes, System.Security.Policy.Evidence securityAttributes)
    {
      Contract.Ensures(Contract.Result<object>() != null);

      return default(object);
    }
#endif

#if !SILVERLIGHT
    public object CreateInstanceAndUnwrap(string assemblyName, string typeName, Object[] activationAttributes)
    {
      Contract.Ensures(Contract.Result<object>() != null);

      return default(object);
    }

    public object CreateInstanceAndUnwrap(string assemblyName, string typeName)
    {
      Contract.Ensures(Contract.Result<object>() != null);

      return default(object);
    }
#endif

#if false
    public void SetDynamicBase(string path)
    {

    }

    public void SetShadowCopyFiles()
    {

    }
    public void SetShadowCopyPath(string path)
    {

    }

    public static AppDomain CreateDomain(string friendlyName, System.Security.Policy.Evidence securityInfo, AppDomainSetup info)
    {
      Contract.Requires(friendlyName != null);
      Contract.Ensures(Contract.Result<AppDomain>() != null);

      return default(AppDomain);
    }
#endif

#if !SILVERLIGHT
    public static AppDomain CreateDomain(string friendlyName)
    {
      Contract.Ensures(Contract.Result<AppDomain>() != null);

      return default(AppDomain);
    }
#endif

#if false
    public static AppDomain CreateDomain(string friendlyName, System.Security.Policy.Evidence securityInfo, string appBasePath, string appRelativeSearchPath, bool shadowCopyFiles)
    {
      Contract.Ensures(Contract.Result<AppDomain>() != null);

      return default(AppDomain);
    }
    public static AppDomain CreateDomain(string friendlyName, System.Security.Policy.Evidence securityInfo)
    {
      Contract.Ensures(Contract.Result<AppDomain>() != null);

      return default(AppDomain);
    }
#endif
#if false
        public void DoCallBack (CrossAppDomainDelegate callBackDelegate) {
            Contract.Requires(callBackDelegate != null);
        }
#endif

#if false
    public object InitializeLifetimeService()
    {

      return default(object);
    }
#endif

#if false
    public void SetPrincipalPolicy (System.Security.Principal.PrincipalPolicy policy) {
    }
#endif
#if false
    public void SetThreadPrincipal(System.Security.Principal.IPrincipal principal)
    {
      Contract.Requires(principal != null);
    }
    public void SetAppDomainPolicy(System.Security.Policy.PolicyLevel domainPolicy)
    {
      Contract.Requires(domainPolicy != null);

    }
#endif

#if !SILVERLIGHT
    public static void Unload(AppDomain domain)
    {
      Contract.Requires(domain != null);
    }
#endif

#if false
    public static int GetCurrentThreadId()
    {
      return default(int);
    }
#endif

#if !SILVERLIGHT_4_0_WP && !SILVERLIGHT_5_0
    [Pure]
    public virtual object GetData(string name)
    {
      Contract.Requires(name != null);

      return default(object);
    }

    public virtual void SetData(string name, object data)
    {
      Contract.Requires(name != null);
    }
#endif

#if false
    public void SetCachePath(string path)
    {

    }
    public void ClearShadowCopyPath()
    {

    }
    public void ClearPrivatePath()
    {

    }
    public void AppendPrivatePath(string path)
    {

    }
    public bool IsFinalizingForUnload()
    {

      return default(bool);
    }
#endif

#if !SILVERLIGHT
    [Pure]
    public virtual System.Reflection.Assembly[] GetAssemblies()
    {
      Contract.Ensures(Contract.Result<System.Reflection.Assembly[]>() != null);
      Contract.Ensures(Contract.ForAll(Contract.Result<System.Reflection.Assembly[]>(), el => el != null));

      return default(System.Reflection.Assembly[]);
    }
#endif

#if false
    public int ExecuteAssembly(string assemblyFile, System.Security.Policy.Evidence assemblySecurity, String[] args, Byte[] hashValue, System.Configuration.Assemblies.AssemblyHashAlgorithm hashAlgorithm)
    {

      return default(int);
    }
    public int ExecuteAssembly(string assemblyFile, System.Security.Policy.Evidence assemblySecurity, String[] args)
    {

      return default(int);
    }
    public int ExecuteAssembly(string assemblyFile)
    {

      return default(int);
    }
    public int ExecuteAssembly(string assemblyFile, System.Security.Policy.Evidence assemblySecurity)
    {

      return default(int);
    }
#endif
#if false
    public System.Reflection.Assembly Load(string assemblyString, System.Security.Policy.Evidence assemblySecurity)
    {
      Contract.Ensures(Contract.Result<System.Reflection.Assembly>() != null);

      return default(System.Reflection.Assembly);
    }
    public System.Reflection.Assembly Load(System.Reflection.AssemblyName assemblyRef, System.Security.Policy.Evidence assemblySecurity)
    {
      Contract.Ensures(Contract.Result<System.Reflection.Assembly>() != null);

      return default(System.Reflection.Assembly);
    }
    public System.Reflection.Assembly Load(Byte[] rawAssembly, Byte[] rawSymbolStore, System.Security.Policy.Evidence securityEvidence)
    {
      Contract.Ensures(Contract.Result<System.Reflection.Assembly>() != null);

      return default(System.Reflection.Assembly);
    }
#endif
#if !SILVERLIGHT
    public virtual System.Reflection.Assembly Load(Byte[] rawAssembly, Byte[] rawSymbolStore)
    {
      Contract.Ensures(Contract.Result<System.Reflection.Assembly>() != null);

      return default(System.Reflection.Assembly);
    }
    public virtual System.Reflection.Assembly Load(Byte[] rawAssembly)
    {
      Contract.Ensures(Contract.Result<System.Reflection.Assembly>() != null);

      return default(System.Reflection.Assembly);
    }
    public virtual System.Reflection.Assembly Load(string assemblyString)
    {
      Contract.Ensures(Contract.Result<System.Reflection.Assembly>() != null);

      return default(System.Reflection.Assembly);
    }
    public virtual System.Reflection.Assembly Load(System.Reflection.AssemblyName assemblyRef)
    {
      Contract.Ensures(Contract.Result<System.Reflection.Assembly>() != null);

      return default(System.Reflection.Assembly);
    }
#endif

#if false
    public System.Runtime.Remoting.ObjectHandle CreateInstanceFrom(string assemblyFile, string typeName, bool ignoreCase, System.Reflection.BindingFlags bindingAttr, System.Reflection.Binder binder, Object[] args, System.Globalization.CultureInfo culture, Object[] activationAttributes, System.Security.Policy.Evidence securityAttributes)
    {

      return default(System.Runtime.Remoting.ObjectHandle);
    }

    public System.Runtime.Remoting.ObjectHandle CreateInstance(string assemblyName, string typeName, bool ignoreCase, System.Reflection.BindingFlags bindingAttr, System.Reflection.Binder binder, Object[] args, System.Globalization.CultureInfo culture, Object[] activationAttributes, System.Security.Policy.Evidence securityAttributes)
    {
      Contract.Requires(this != null);
      Contract.Requires(assemblyName != null);

      return default(System.Runtime.Remoting.ObjectHandle);
    }
    public System.Runtime.Remoting.ObjectHandle CreateInstanceFrom(string assemblyFile, string typeName, Object[] activationAttributes)
    {
      Contract.Requires(this != null);

      return default(System.Runtime.Remoting.ObjectHandle);
    }
    public System.Runtime.Remoting.ObjectHandle CreateInstance(string assemblyName, string typeName, Object[] activationAttributes)
    {
      Contract.Requires(this != null);
      Contract.Requires(assemblyName != null);

      return default(System.Runtime.Remoting.ObjectHandle);
    }
    public System.Runtime.Remoting.ObjectHandle CreateComInstanceFrom(string assemblyFile, string typeName, Byte[] hashValue, System.Configuration.Assemblies.AssemblyHashAlgorithm hashAlgorithm)
    {
      Contract.Requires(this != null);
      Contract.Requires(assemblyFile != null);

      return default(System.Runtime.Remoting.ObjectHandle);
    }
    public System.Runtime.Remoting.ObjectHandle CreateComInstanceFrom(string assemblyName, string typeName)
    {
      Contract.Requires(this != null);
      Contract.Requires(assemblyName != null);

      return default(System.Runtime.Remoting.ObjectHandle);
    }
    public System.Runtime.Remoting.ObjectHandle CreateInstanceFrom(string assemblyFile, string typeName)
    {
      Contract.Requires(this != null);

      return default(System.Runtime.Remoting.ObjectHandle);
    }
    public System.Runtime.Remoting.ObjectHandle CreateInstance(string assemblyName, string typeName)
    {
      Contract.Requires(this != null);
      Contract.Requires(assemblyName != null);

      return default(System.Runtime.Remoting.ObjectHandle);
    }
    public System.Reflection.Emit.AssemblyBuilder DefineDynamicAssembly(System.Reflection.AssemblyName name, System.Reflection.Emit.AssemblyBuilderAccess access, string dir, System.Security.Policy.Evidence evidence, System.Security.PermissionSet requiredPermissions, System.Security.PermissionSet optionalPermissions, System.Security.PermissionSet refusedPermissions, bool isSynchronized)
    {

      return default(System.Reflection.Emit.AssemblyBuilder);
    }
    public System.Reflection.Emit.AssemblyBuilder DefineDynamicAssembly(System.Reflection.AssemblyName name, System.Reflection.Emit.AssemblyBuilderAccess access, string dir, System.Security.Policy.Evidence evidence, System.Security.PermissionSet requiredPermissions, System.Security.PermissionSet optionalPermissions, System.Security.PermissionSet refusedPermissions)
    {

      return default(System.Reflection.Emit.AssemblyBuilder);
    }
    public System.Reflection.Emit.AssemblyBuilder DefineDynamicAssembly(System.Reflection.AssemblyName name, System.Reflection.Emit.AssemblyBuilderAccess access, System.Security.Policy.Evidence evidence, System.Security.PermissionSet requiredPermissions, System.Security.PermissionSet optionalPermissions, System.Security.PermissionSet refusedPermissions)
    {

      return default(System.Reflection.Emit.AssemblyBuilder);
    }
    public System.Reflection.Emit.AssemblyBuilder DefineDynamicAssembly(System.Reflection.AssemblyName name, System.Reflection.Emit.AssemblyBuilderAccess access, string dir, System.Security.PermissionSet requiredPermissions, System.Security.PermissionSet optionalPermissions, System.Security.PermissionSet refusedPermissions)
    {

      return default(System.Reflection.Emit.AssemblyBuilder);
    }
    public System.Reflection.Emit.AssemblyBuilder DefineDynamicAssembly(System.Reflection.AssemblyName name, System.Reflection.Emit.AssemblyBuilderAccess access, string dir, System.Security.Policy.Evidence evidence)
    {

      return default(System.Reflection.Emit.AssemblyBuilder);
    }
    public System.Reflection.Emit.AssemblyBuilder DefineDynamicAssembly(System.Reflection.AssemblyName name, System.Reflection.Emit.AssemblyBuilderAccess access, System.Security.PermissionSet requiredPermissions, System.Security.PermissionSet optionalPermissions, System.Security.PermissionSet refusedPermissions)
    {

      return default(System.Reflection.Emit.AssemblyBuilder);
    }
    public System.Reflection.Emit.AssemblyBuilder DefineDynamicAssembly(System.Reflection.AssemblyName name, System.Reflection.Emit.AssemblyBuilderAccess access, System.Security.Policy.Evidence evidence)
    {

      return default(System.Reflection.Emit.AssemblyBuilder);
    }
    public System.Reflection.Emit.AssemblyBuilder DefineDynamicAssembly(System.Reflection.AssemblyName name, System.Reflection.Emit.AssemblyBuilderAccess access, string dir)
    {

      return default(System.Reflection.Emit.AssemblyBuilder);
    }
    public System.Reflection.Emit.AssemblyBuilder DefineDynamicAssembly(System.Reflection.AssemblyName name, System.Reflection.Emit.AssemblyBuilderAccess access)
    {

      return default(System.Reflection.Emit.AssemblyBuilder);
    }
#endif
  }
}