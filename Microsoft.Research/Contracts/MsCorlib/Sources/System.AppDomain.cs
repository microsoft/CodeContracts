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

// File System.AppDomain.cs
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
  sealed public partial class AppDomain : MarshalByRefObject, _AppDomain, System.Security.IEvidenceFactory
  {
    #region Methods and constructors
    internal AppDomain()
    {
    }

    public void AppendPrivatePath(string path)
    {
    }

    public string ApplyPolicy(string assemblyName)
    {
      return default(string);
    }

    public void ClearPrivatePath()
    {
    }

    public void ClearShadowCopyPath()
    {
    }

    public System.Runtime.Remoting.ObjectHandle CreateComInstanceFrom(string assemblyName, string typeName)
    {
      return default(System.Runtime.Remoting.ObjectHandle);
    }

    public System.Runtime.Remoting.ObjectHandle CreateComInstanceFrom(string assemblyFile, string typeName, byte[] hashValue, System.Configuration.Assemblies.AssemblyHashAlgorithm hashAlgorithm)
    {
      return default(System.Runtime.Remoting.ObjectHandle);
    }

    public static AppDomain CreateDomain(string friendlyName, System.Security.Policy.Evidence securityInfo)
    {
      return default(AppDomain);
    }

    public static AppDomain CreateDomain(string friendlyName, System.Security.Policy.Evidence securityInfo, string appBasePath, string appRelativeSearchPath, bool shadowCopyFiles, AppDomainInitializer adInit, string[] adInitArgs)
    {
      return default(AppDomain);
    }

    public static AppDomain CreateDomain(string friendlyName)
    {
      return default(AppDomain);
    }

    public static AppDomain CreateDomain(string friendlyName, System.Security.Policy.Evidence securityInfo, string appBasePath, string appRelativeSearchPath, bool shadowCopyFiles)
    {
      return default(AppDomain);
    }

    public static AppDomain CreateDomain(string friendlyName, System.Security.Policy.Evidence securityInfo, AppDomainSetup info, System.Security.PermissionSet grantSet, System.Security.Policy.StrongName[] fullTrustAssemblies)
    {
      Contract.Ensures((fullTrustAssemblies.Length - Contract.OldValue(fullTrustAssemblies.Length)) <= 0);

      return default(AppDomain);
    }

    public static AppDomain CreateDomain(string friendlyName, System.Security.Policy.Evidence securityInfo, AppDomainSetup info)
    {
      return default(AppDomain);
    }

    public System.Runtime.Remoting.ObjectHandle CreateInstance(string assemblyName, string typeName, bool ignoreCase, System.Reflection.BindingFlags bindingAttr, System.Reflection.Binder binder, Object[] args, System.Globalization.CultureInfo culture, Object[] activationAttributes, System.Security.Policy.Evidence securityAttributes)
    {
      return default(System.Runtime.Remoting.ObjectHandle);
    }

    public System.Runtime.Remoting.ObjectHandle CreateInstance(string assemblyName, string typeName, bool ignoreCase, System.Reflection.BindingFlags bindingAttr, System.Reflection.Binder binder, Object[] args, System.Globalization.CultureInfo culture, Object[] activationAttributes)
    {
      return default(System.Runtime.Remoting.ObjectHandle);
    }

    public System.Runtime.Remoting.ObjectHandle CreateInstance(string assemblyName, string typeName, Object[] activationAttributes)
    {
      return default(System.Runtime.Remoting.ObjectHandle);
    }

    public System.Runtime.Remoting.ObjectHandle CreateInstance(string assemblyName, string typeName)
    {
      return default(System.Runtime.Remoting.ObjectHandle);
    }

    public Object CreateInstanceAndUnwrap(string assemblyName, string typeName)
    {
      return default(Object);
    }

    public Object CreateInstanceAndUnwrap(string assemblyName, string typeName, bool ignoreCase, System.Reflection.BindingFlags bindingAttr, System.Reflection.Binder binder, Object[] args, System.Globalization.CultureInfo culture, Object[] activationAttributes, System.Security.Policy.Evidence securityAttributes)
    {
      return default(Object);
    }

    public Object CreateInstanceAndUnwrap(string assemblyName, string typeName, Object[] activationAttributes)
    {
      return default(Object);
    }

    public Object CreateInstanceAndUnwrap(string assemblyName, string typeName, bool ignoreCase, System.Reflection.BindingFlags bindingAttr, System.Reflection.Binder binder, Object[] args, System.Globalization.CultureInfo culture, Object[] activationAttributes)
    {
      return default(Object);
    }

    public System.Runtime.Remoting.ObjectHandle CreateInstanceFrom(string assemblyFile, string typeName, bool ignoreCase, System.Reflection.BindingFlags bindingAttr, System.Reflection.Binder binder, Object[] args, System.Globalization.CultureInfo culture, Object[] activationAttributes)
    {
      return default(System.Runtime.Remoting.ObjectHandle);
    }

    public System.Runtime.Remoting.ObjectHandle CreateInstanceFrom(string assemblyFile, string typeName, bool ignoreCase, System.Reflection.BindingFlags bindingAttr, System.Reflection.Binder binder, Object[] args, System.Globalization.CultureInfo culture, Object[] activationAttributes, System.Security.Policy.Evidence securityAttributes)
    {
      return default(System.Runtime.Remoting.ObjectHandle);
    }

    public System.Runtime.Remoting.ObjectHandle CreateInstanceFrom(string assemblyFile, string typeName, Object[] activationAttributes)
    {
      return default(System.Runtime.Remoting.ObjectHandle);
    }

    public System.Runtime.Remoting.ObjectHandle CreateInstanceFrom(string assemblyFile, string typeName)
    {
      return default(System.Runtime.Remoting.ObjectHandle);
    }

    public Object CreateInstanceFromAndUnwrap(string assemblyFile, string typeName, bool ignoreCase, System.Reflection.BindingFlags bindingAttr, System.Reflection.Binder binder, Object[] args, System.Globalization.CultureInfo culture, Object[] activationAttributes)
    {
      return default(Object);
    }

    public Object CreateInstanceFromAndUnwrap(string assemblyName, string typeName, bool ignoreCase, System.Reflection.BindingFlags bindingAttr, System.Reflection.Binder binder, Object[] args, System.Globalization.CultureInfo culture, Object[] activationAttributes, System.Security.Policy.Evidence securityAttributes)
    {
      return default(Object);
    }

    public Object CreateInstanceFromAndUnwrap(string assemblyName, string typeName)
    {
      return default(Object);
    }

    public Object CreateInstanceFromAndUnwrap(string assemblyName, string typeName, Object[] activationAttributes)
    {
      return default(Object);
    }

    public System.Reflection.Emit.AssemblyBuilder DefineDynamicAssembly(System.Reflection.AssemblyName name, System.Reflection.Emit.AssemblyBuilderAccess access)
    {
      return default(System.Reflection.Emit.AssemblyBuilder);
    }

    public System.Reflection.Emit.AssemblyBuilder DefineDynamicAssembly(System.Reflection.AssemblyName name, System.Reflection.Emit.AssemblyBuilderAccess access, string dir, System.Security.PermissionSet requiredPermissions, System.Security.PermissionSet optionalPermissions, System.Security.PermissionSet refusedPermissions)
    {
      return default(System.Reflection.Emit.AssemblyBuilder);
    }

    public System.Reflection.Emit.AssemblyBuilder DefineDynamicAssembly(System.Reflection.AssemblyName name, System.Reflection.Emit.AssemblyBuilderAccess access, string dir, bool isSynchronized, IEnumerable<System.Reflection.Emit.CustomAttributeBuilder> assemblyAttributes)
    {
      return default(System.Reflection.Emit.AssemblyBuilder);
    }

    public System.Reflection.Emit.AssemblyBuilder DefineDynamicAssembly(System.Reflection.AssemblyName name, System.Reflection.Emit.AssemblyBuilderAccess access, string dir, System.Security.Policy.Evidence evidence)
    {
      return default(System.Reflection.Emit.AssemblyBuilder);
    }

    public System.Reflection.Emit.AssemblyBuilder DefineDynamicAssembly(System.Reflection.AssemblyName name, System.Reflection.Emit.AssemblyBuilderAccess access, System.Security.Policy.Evidence evidence, System.Security.PermissionSet requiredPermissions, System.Security.PermissionSet optionalPermissions, System.Security.PermissionSet refusedPermissions)
    {
      return default(System.Reflection.Emit.AssemblyBuilder);
    }

    public System.Reflection.Emit.AssemblyBuilder DefineDynamicAssembly(System.Reflection.AssemblyName name, System.Reflection.Emit.AssemblyBuilderAccess access, string dir, System.Security.Policy.Evidence evidence, System.Security.PermissionSet requiredPermissions, System.Security.PermissionSet optionalPermissions, System.Security.PermissionSet refusedPermissions)
    {
      return default(System.Reflection.Emit.AssemblyBuilder);
    }

    public System.Reflection.Emit.AssemblyBuilder DefineDynamicAssembly(System.Reflection.AssemblyName name, System.Reflection.Emit.AssemblyBuilderAccess access, string dir, System.Security.Policy.Evidence evidence, System.Security.PermissionSet requiredPermissions, System.Security.PermissionSet optionalPermissions, System.Security.PermissionSet refusedPermissions, bool isSynchronized)
    {
      return default(System.Reflection.Emit.AssemblyBuilder);
    }

    public System.Reflection.Emit.AssemblyBuilder DefineDynamicAssembly(System.Reflection.AssemblyName name, System.Reflection.Emit.AssemblyBuilderAccess access, string dir, System.Security.Policy.Evidence evidence, System.Security.PermissionSet requiredPermissions, System.Security.PermissionSet optionalPermissions, System.Security.PermissionSet refusedPermissions, bool isSynchronized, IEnumerable<System.Reflection.Emit.CustomAttributeBuilder> assemblyAttributes)
    {
      return default(System.Reflection.Emit.AssemblyBuilder);
    }

    public System.Reflection.Emit.AssemblyBuilder DefineDynamicAssembly(System.Reflection.AssemblyName name, System.Reflection.Emit.AssemblyBuilderAccess access, System.Security.PermissionSet requiredPermissions, System.Security.PermissionSet optionalPermissions, System.Security.PermissionSet refusedPermissions)
    {
      return default(System.Reflection.Emit.AssemblyBuilder);
    }

    public System.Reflection.Emit.AssemblyBuilder DefineDynamicAssembly(System.Reflection.AssemblyName name, System.Reflection.Emit.AssemblyBuilderAccess access, string dir)
    {
      return default(System.Reflection.Emit.AssemblyBuilder);
    }

    public System.Reflection.Emit.AssemblyBuilder DefineDynamicAssembly(System.Reflection.AssemblyName name, System.Reflection.Emit.AssemblyBuilderAccess access, IEnumerable<System.Reflection.Emit.CustomAttributeBuilder> assemblyAttributes, System.Security.SecurityContextSource securityContextSource)
    {
      return default(System.Reflection.Emit.AssemblyBuilder);
    }

    public System.Reflection.Emit.AssemblyBuilder DefineDynamicAssembly(System.Reflection.AssemblyName name, System.Reflection.Emit.AssemblyBuilderAccess access, IEnumerable<System.Reflection.Emit.CustomAttributeBuilder> assemblyAttributes)
    {
      return default(System.Reflection.Emit.AssemblyBuilder);
    }

    public System.Reflection.Emit.AssemblyBuilder DefineDynamicAssembly(System.Reflection.AssemblyName name, System.Reflection.Emit.AssemblyBuilderAccess access, System.Security.Policy.Evidence evidence)
    {
      return default(System.Reflection.Emit.AssemblyBuilder);
    }

    public void DoCallBack(CrossAppDomainDelegate callBackDelegate)
    {
    }

    public int ExecuteAssembly(string assemblyFile, System.Security.Policy.Evidence assemblySecurity, string[] args)
    {
      return default(int);
    }

    public int ExecuteAssembly(string assemblyFile, string[] args, byte[] hashValue, System.Configuration.Assemblies.AssemblyHashAlgorithm hashAlgorithm)
    {
      Contract.Ensures((args.Length - Contract.OldValue(args.Length)) <= 0);

      return default(int);
    }

    public int ExecuteAssembly(string assemblyFile, string[] args)
    {
      Contract.Ensures((args.Length - Contract.OldValue(args.Length)) <= 0);

      return default(int);
    }

    public int ExecuteAssembly(string assemblyFile)
    {
      return default(int);
    }

    public int ExecuteAssembly(string assemblyFile, System.Security.Policy.Evidence assemblySecurity, string[] args, byte[] hashValue, System.Configuration.Assemblies.AssemblyHashAlgorithm hashAlgorithm)
    {
      Contract.Ensures((args.Length - Contract.OldValue(args.Length)) <= 0);

      return default(int);
    }

    public int ExecuteAssembly(string assemblyFile, System.Security.Policy.Evidence assemblySecurity)
    {
      return default(int);
    }

    public int ExecuteAssemblyByName(string assemblyName, System.Security.Policy.Evidence assemblySecurity)
    {
      return default(int);
    }

    public int ExecuteAssemblyByName(string assemblyName)
    {
      return default(int);
    }

    public int ExecuteAssemblyByName(System.Reflection.AssemblyName assemblyName, System.Security.Policy.Evidence assemblySecurity, string[] args)
    {
      Contract.Ensures((args.Length - Contract.OldValue(args.Length)) <= 0);

      return default(int);
    }

    public int ExecuteAssemblyByName(System.Reflection.AssemblyName assemblyName, string[] args)
    {
      Contract.Ensures((args.Length - Contract.OldValue(args.Length)) <= 0);

      return default(int);
    }

    public int ExecuteAssemblyByName(string assemblyName, System.Security.Policy.Evidence assemblySecurity, string[] args)
    {
      Contract.Ensures((args.Length - Contract.OldValue(args.Length)) <= 0);

      return default(int);
    }

    public int ExecuteAssemblyByName(string assemblyName, string[] args)
    {
      Contract.Ensures((args.Length - Contract.OldValue(args.Length)) <= 0);

      return default(int);
    }

    public System.Reflection.Assembly[] GetAssemblies()
    {
      return default(System.Reflection.Assembly[]);
    }

    public static int GetCurrentThreadId()
    {
      return default(int);
    }

    public Object GetData(string name)
    {
      return default(Object);
    }

    public Type GetType()
    {
      return default(Type);
    }

    public override Object InitializeLifetimeService()
    {
      return default(Object);
    }

    public Nullable<bool> IsCompatibilitySwitchSet(string value)
    {
      return default(Nullable<bool>);
    }

    public bool IsDefaultAppDomain()
    {
      return default(bool);
    }

    public bool IsFinalizingForUnload()
    {
      return default(bool);
    }

    public System.Reflection.Assembly Load(string assemblyString, System.Security.Policy.Evidence assemblySecurity)
    {
      return default(System.Reflection.Assembly);
    }

    public System.Reflection.Assembly Load(byte[] rawAssembly, byte[] rawSymbolStore)
    {
      return default(System.Reflection.Assembly);
    }

    public System.Reflection.Assembly Load(byte[] rawAssembly, byte[] rawSymbolStore, System.Security.Policy.Evidence securityEvidence)
    {
      return default(System.Reflection.Assembly);
    }

    public System.Reflection.Assembly Load(System.Reflection.AssemblyName assemblyRef, System.Security.Policy.Evidence assemblySecurity)
    {
      return default(System.Reflection.Assembly);
    }

    public System.Reflection.Assembly Load(byte[] rawAssembly)
    {
      return default(System.Reflection.Assembly);
    }

    public System.Reflection.Assembly Load(System.Reflection.AssemblyName assemblyRef)
    {
      return default(System.Reflection.Assembly);
    }

    public System.Reflection.Assembly Load(string assemblyString)
    {
      return default(System.Reflection.Assembly);
    }

    public System.Reflection.Assembly[] ReflectionOnlyGetAssemblies()
    {
      return default(System.Reflection.Assembly[]);
    }

    public void SetAppDomainPolicy(System.Security.Policy.PolicyLevel domainPolicy)
    {
    }

    public void SetCachePath(string path)
    {
    }

    public void SetData(string name, Object data, System.Security.IPermission permission)
    {
    }

    public void SetData(string name, Object data)
    {
    }

    public void SetDynamicBase(string path)
    {
    }

    public void SetPrincipalPolicy(System.Security.Principal.PrincipalPolicy policy)
    {
    }

    public void SetShadowCopyFiles()
    {
    }

    public void SetShadowCopyPath(string path)
    {
    }

    public void SetThreadPrincipal(System.Security.Principal.IPrincipal principal)
    {
    }

    void System._AppDomain.GetIDsOfNames(ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
    {
    }

    void System._AppDomain.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
    {
    }

    void System._AppDomain.GetTypeInfoCount(out uint pcTInfo)
    {
      pcTInfo = default(uint);
    }

    void System._AppDomain.Invoke(uint dispIdMember, ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
    {
    }

    public override string ToString()
    {
      return default(string);
    }

    public static void Unload(AppDomain domain)
    {
    }
    #endregion

    #region Properties and indexers
    public ActivationContext ActivationContext
    {
      get
      {
        return default(ActivationContext);
      }
    }

    public ApplicationIdentity ApplicationIdentity
    {
      get
      {
        return default(ApplicationIdentity);
      }
    }

    public System.Security.Policy.ApplicationTrust ApplicationTrust
    {
      get
      {
        return default(System.Security.Policy.ApplicationTrust);
      }
    }

    public string BaseDirectory
    {
      get
      {
        return default(string);
      }
    }

    public static System.AppDomain CurrentDomain
    {
      get
      {
        return default(System.AppDomain);
      }
    }

    public AppDomainManager DomainManager
    {
      get
      {
        return default(AppDomainManager);
      }
    }

    public string DynamicDirectory
    {
      get
      {
        return default(string);
      }
    }

    public System.Security.Policy.Evidence Evidence
    {
      get
      {
        return default(System.Security.Policy.Evidence);
      }
    }

    public string FriendlyName
    {
      get
      {
        return default(string);
      }
    }

    public int Id
    {
      get
      {
        return default(int);
      }
    }

    public bool IsFullyTrusted
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsHomogenous
    {
      get
      {
        return default(bool);
      }
    }

    public static bool MonitoringIsEnabled
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public long MonitoringSurvivedMemorySize
    {
      get
      {
        return default(long);
      }
    }

    public static long MonitoringSurvivedProcessMemorySize
    {
      get
      {
        return default(long);
      }
    }

    public long MonitoringTotalAllocatedMemorySize
    {
      get
      {
        return default(long);
      }
    }

    public TimeSpan MonitoringTotalProcessorTime
    {
      get
      {
        return default(TimeSpan);
      }
    }

    public System.Security.PermissionSet PermissionSet
    {
      get
      {
        return default(System.Security.PermissionSet);
      }
    }

    public string RelativeSearchPath
    {
      get
      {
        return default(string);
      }
    }

    public AppDomainSetup SetupInformation
    {
      get
      {
        Contract.Ensures(Contract.Result<System.AppDomainSetup>() != null);

        return default(AppDomainSetup);
      }
    }

    public bool ShadowCopyFiles
    {
      get
      {
        return default(bool);
      }
    }
    #endregion

    #region Events
    public event AssemblyLoadEventHandler AssemblyLoad
    {
      add
      {
      }
      remove
      {
      }
    }

    public event ResolveEventHandler AssemblyResolve
    {
      add
      {
      }
      remove
      {
      }
    }

    public event EventHandler DomainUnload
    {
      add
      {
      }
      remove
      {
      }
    }

    public event EventHandler<System.Runtime.ExceptionServices.FirstChanceExceptionEventArgs> FirstChanceException
    {
      add
      {
      }
      remove
      {
      }
    }

    public event EventHandler ProcessExit
    {
      add
      {
      }
      remove
      {
      }
    }

    public event ResolveEventHandler ReflectionOnlyAssemblyResolve
    {
      add
      {
      }
      remove
      {
      }
    }

    public event ResolveEventHandler ResourceResolve
    {
      add
      {
      }
      remove
      {
      }
    }

    public event ResolveEventHandler TypeResolve
    {
      add
      {
      }
      remove
      {
      }
    }

    public event UnhandledExceptionEventHandler UnhandledException
    {
      add
      {
      }
      remove
      {
      }
    }
    #endregion
  }
}
