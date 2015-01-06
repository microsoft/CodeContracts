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

// File System._AppDomain.cs
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
  public partial interface _AppDomain
  {
    #region Methods and constructors
    void AppendPrivatePath(string path);

    void ClearPrivatePath();

    void ClearShadowCopyPath();

    System.Runtime.Remoting.ObjectHandle CreateInstance(string assemblyName, string typeName);

    System.Runtime.Remoting.ObjectHandle CreateInstance(string assemblyName, string typeName, Object[] activationAttributes);

    System.Runtime.Remoting.ObjectHandle CreateInstance(string assemblyName, string typeName, bool ignoreCase, System.Reflection.BindingFlags bindingAttr, System.Reflection.Binder binder, Object[] args, System.Globalization.CultureInfo culture, Object[] activationAttributes, System.Security.Policy.Evidence securityAttributes);

    System.Runtime.Remoting.ObjectHandle CreateInstanceFrom(string assemblyFile, string typeName);

    System.Runtime.Remoting.ObjectHandle CreateInstanceFrom(string assemblyFile, string typeName, Object[] activationAttributes);

    System.Runtime.Remoting.ObjectHandle CreateInstanceFrom(string assemblyFile, string typeName, bool ignoreCase, System.Reflection.BindingFlags bindingAttr, System.Reflection.Binder binder, Object[] args, System.Globalization.CultureInfo culture, Object[] activationAttributes, System.Security.Policy.Evidence securityAttributes);

    System.Reflection.Emit.AssemblyBuilder DefineDynamicAssembly(System.Reflection.AssemblyName name, System.Reflection.Emit.AssemblyBuilderAccess access, System.Security.Policy.Evidence evidence, System.Security.PermissionSet requiredPermissions, System.Security.PermissionSet optionalPermissions, System.Security.PermissionSet refusedPermissions);

    System.Reflection.Emit.AssemblyBuilder DefineDynamicAssembly(System.Reflection.AssemblyName name, System.Reflection.Emit.AssemblyBuilderAccess access, string dir, System.Security.Policy.Evidence evidence, System.Security.PermissionSet requiredPermissions, System.Security.PermissionSet optionalPermissions, System.Security.PermissionSet refusedPermissions);

    System.Reflection.Emit.AssemblyBuilder DefineDynamicAssembly(System.Reflection.AssemblyName name, System.Reflection.Emit.AssemblyBuilderAccess access, string dir, System.Security.Policy.Evidence evidence, System.Security.PermissionSet requiredPermissions, System.Security.PermissionSet optionalPermissions, System.Security.PermissionSet refusedPermissions, bool isSynchronized);

    System.Reflection.Emit.AssemblyBuilder DefineDynamicAssembly(System.Reflection.AssemblyName name, System.Reflection.Emit.AssemblyBuilderAccess access, string dir, System.Security.PermissionSet requiredPermissions, System.Security.PermissionSet optionalPermissions, System.Security.PermissionSet refusedPermissions);

    System.Reflection.Emit.AssemblyBuilder DefineDynamicAssembly(System.Reflection.AssemblyName name, System.Reflection.Emit.AssemblyBuilderAccess access);

    System.Reflection.Emit.AssemblyBuilder DefineDynamicAssembly(System.Reflection.AssemblyName name, System.Reflection.Emit.AssemblyBuilderAccess access, string dir);

    System.Reflection.Emit.AssemblyBuilder DefineDynamicAssembly(System.Reflection.AssemblyName name, System.Reflection.Emit.AssemblyBuilderAccess access, System.Security.Policy.Evidence evidence);

    System.Reflection.Emit.AssemblyBuilder DefineDynamicAssembly(System.Reflection.AssemblyName name, System.Reflection.Emit.AssemblyBuilderAccess access, string dir, System.Security.Policy.Evidence evidence);

    System.Reflection.Emit.AssemblyBuilder DefineDynamicAssembly(System.Reflection.AssemblyName name, System.Reflection.Emit.AssemblyBuilderAccess access, System.Security.PermissionSet requiredPermissions, System.Security.PermissionSet optionalPermissions, System.Security.PermissionSet refusedPermissions);

    void DoCallBack(CrossAppDomainDelegate theDelegate);

    bool Equals(Object other);

    int ExecuteAssembly(string assemblyFile, System.Security.Policy.Evidence assemblySecurity);

    int ExecuteAssembly(string assemblyFile, System.Security.Policy.Evidence assemblySecurity, string[] args);

    int ExecuteAssembly(string assemblyFile);

    System.Reflection.Assembly[] GetAssemblies();

    Object GetData(string name);

    int GetHashCode();

    void GetIDsOfNames(ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

    Object GetLifetimeService();

    Type GetType();

    void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

    void GetTypeInfoCount(out uint pcTInfo);

    Object InitializeLifetimeService();

    void Invoke(uint dispIdMember, ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);

    System.Reflection.Assembly Load(string assemblyString, System.Security.Policy.Evidence assemblySecurity);

    System.Reflection.Assembly Load(string assemblyString);

    System.Reflection.Assembly Load(System.Reflection.AssemblyName assemblyRef, System.Security.Policy.Evidence assemblySecurity);

    System.Reflection.Assembly Load(byte[] rawAssembly);

    System.Reflection.Assembly Load(byte[] rawAssembly, byte[] rawSymbolStore, System.Security.Policy.Evidence securityEvidence);

    System.Reflection.Assembly Load(byte[] rawAssembly, byte[] rawSymbolStore);

    System.Reflection.Assembly Load(System.Reflection.AssemblyName assemblyRef);

    void SetAppDomainPolicy(System.Security.Policy.PolicyLevel domainPolicy);

    void SetCachePath(string s);

    void SetData(string name, Object data);

    void SetPrincipalPolicy(System.Security.Principal.PrincipalPolicy policy);

    void SetShadowCopyPath(string s);

    void SetThreadPrincipal(System.Security.Principal.IPrincipal principal);

    string ToString();
    #endregion

    #region Properties and indexers
    string BaseDirectory
    {
      get;
    }

    string DynamicDirectory
    {
      get;
    }

    System.Security.Policy.Evidence Evidence
    {
      get;
    }

    string FriendlyName
    {
      get;
    }

    string RelativeSearchPath
    {
      get;
    }

    bool ShadowCopyFiles
    {
      get;
    }
    #endregion

    #region Events
    event AssemblyLoadEventHandler AssemblyLoad
    ;

    event ResolveEventHandler AssemblyResolve
    ;

    event EventHandler DomainUnload
    ;

    event EventHandler ProcessExit
    ;

    event ResolveEventHandler ResourceResolve
    ;

    event ResolveEventHandler TypeResolve
    ;

    event UnhandledExceptionEventHandler UnhandledException
    ;
    #endregion
  }
}
