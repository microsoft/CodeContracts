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

        public string RelativeSearchPath
        {
          get;
        }

        public AppDomainSetup SetupInformation
        {
          get;
        }

        public string FriendlyName
        {
          get;
        }

        public string BaseDirectory
        {
          get;
        }

        public System.Security.Policy.Evidence Evidence
        {
          get;
        }

        public bool ShadowCopyFiles
        {
          get;
        }

        public string DynamicDirectory
        {
          get;
        }

        public static AppDomain! CurrentDomain
        {
          get;
        }

        public object CreateInstanceFromAndUnwrap (string assemblyName, string typeName, bool ignoreCase, System.Reflection.BindingFlags bindingAttr, System.Reflection.Binder binder, Object[] args, System.Globalization.CultureInfo culture, Object[] activationAttributes, System.Security.Policy.Evidence securityAttributes) {

          return default(object);
        }
        public object CreateInstanceFromAndUnwrap (string assemblyName, string typeName, Object[] activationAttributes) {

          return default(object);
        }
        public object CreateInstanceFromAndUnwrap (string assemblyName, string typeName) {

          return default(object);
        }
        public object CreateInstanceAndUnwrap (string assemblyName, string typeName, bool ignoreCase, System.Reflection.BindingFlags bindingAttr, System.Reflection.Binder binder, Object[] args, System.Globalization.CultureInfo culture, Object[] activationAttributes, System.Security.Policy.Evidence securityAttributes) {

          return default(object);
        }
        public object CreateInstanceAndUnwrap (string assemblyName, string typeName, Object[] activationAttributes) {

          return default(object);
        }
        public object CreateInstanceAndUnwrap (string assemblyName, string typeName) {

          return default(object);
        }
        public void SetDynamicBase (string path) {

        }
        public void SetShadowCopyFiles () {

        }
        public void SetShadowCopyPath (string path) {

        }
        public static AppDomain CreateDomain (string! friendlyName, System.Security.Policy.Evidence securityInfo, AppDomainSetup info) {
            CodeContract.Requires(friendlyName != null);

          return default(AppDomain);
        }
        public static AppDomain CreateDomain (string friendlyName) {

          return default(AppDomain);
        }
        public static AppDomain CreateDomain (string friendlyName, System.Security.Policy.Evidence securityInfo, string appBasePath, string appRelativeSearchPath, bool shadowCopyFiles) {

          return default(AppDomain);
        }
        public static AppDomain CreateDomain (string friendlyName, System.Security.Policy.Evidence securityInfo) {

          return default(AppDomain);
        }
        public void DoCallBack (CrossAppDomainDelegate! callBackDelegate) {
            CodeContract.Requires(callBackDelegate != null);

        }
        public object InitializeLifetimeService () {

          return default(object);
        }
        public void SetPrincipalPolicy (System.Security.Principal.PrincipalPolicy policy) {

        }
        public void SetThreadPrincipal (System.Security.Principal.IPrincipal! principal) {
            CodeContract.Requires(principal != null);

        }
        public void SetAppDomainPolicy (System.Security.Policy.PolicyLevel! domainPolicy) {
            CodeContract.Requires(domainPolicy != null);

        }
        public static void Unload (AppDomain! domain) {
            CodeContract.Requires(domain != null);

        }
        public static int GetCurrentThreadId () {

          return default(int);
        }
        public object GetData (string! name) {
            CodeContract.Requires(name != null);

          return default(object);
        }
        public void SetData (string! name, object data) {
            CodeContract.Requires(name != null);

        }
        public void SetCachePath (string path) {

        }
        public void ClearShadowCopyPath () {

        }
        public void ClearPrivatePath () {

        }
        public void AppendPrivatePath (string path) {

        }
        public bool IsFinalizingForUnload () {

          return default(bool);
        }
        public System.Reflection.Assembly[] GetAssemblies () {

          CodeContract.Ensures(CodeContract.Result<System.Reflection.Assembly[]>() != null);
          return default(System.Reflection.Assembly[]);
        }
        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public string ToString () {

          CodeContract.Ensures(CodeContract.Result<string>() != null);
          return default(string);
        }
        public int ExecuteAssembly (string assemblyFile, System.Security.Policy.Evidence assemblySecurity, String[] args, Byte[] hashValue, System.Configuration.Assemblies.AssemblyHashAlgorithm hashAlgorithm) {

          return default(int);
        }
        public int ExecuteAssembly (string assemblyFile, System.Security.Policy.Evidence assemblySecurity, String[] args) {

          return default(int);
        }
        public int ExecuteAssembly (string assemblyFile) {

          return default(int);
        }
        public int ExecuteAssembly (string assemblyFile, System.Security.Policy.Evidence assemblySecurity) {

          return default(int);
        }
        public System.Reflection.Assembly Load (string assemblyString, System.Security.Policy.Evidence assemblySecurity) {

          return default(System.Reflection.Assembly);
        }
        public System.Reflection.Assembly Load (System.Reflection.AssemblyName assemblyRef, System.Security.Policy.Evidence assemblySecurity) {

          return default(System.Reflection.Assembly);
        }
        public System.Reflection.Assembly Load (Byte[] rawAssembly, Byte[] rawSymbolStore, System.Security.Policy.Evidence securityEvidence) {

          return default(System.Reflection.Assembly);
        }
        public System.Reflection.Assembly Load (Byte[] rawAssembly, Byte[] rawSymbolStore) {

          return default(System.Reflection.Assembly);
        }
        public System.Reflection.Assembly Load (Byte[] rawAssembly) {

          return default(System.Reflection.Assembly);
        }
        public System.Reflection.Assembly Load (string assemblyString) {

          return default(System.Reflection.Assembly);
        }
        public System.Reflection.Assembly Load (System.Reflection.AssemblyName assemblyRef) {

          return default(System.Reflection.Assembly);
        }
        public System.Runtime.Remoting.ObjectHandle CreateInstanceFrom (string assemblyFile, string typeName, bool ignoreCase, System.Reflection.BindingFlags bindingAttr, System.Reflection.Binder binder, Object[] args, System.Globalization.CultureInfo culture, Object[] activationAttributes, System.Security.Policy.Evidence securityAttributes) {
            CodeContract.Requires(this != null);

          return default(System.Runtime.Remoting.ObjectHandle);
        }
        public System.Runtime.Remoting.ObjectHandle CreateInstance (string! assemblyName, string typeName, bool ignoreCase, System.Reflection.BindingFlags bindingAttr, System.Reflection.Binder binder, Object[] args, System.Globalization.CultureInfo culture, Object[] activationAttributes, System.Security.Policy.Evidence securityAttributes) {
            CodeContract.Requires(this != null);
            CodeContract.Requires(assemblyName != null);

          return default(System.Runtime.Remoting.ObjectHandle);
        }
        public System.Runtime.Remoting.ObjectHandle CreateInstanceFrom (string assemblyFile, string typeName, Object[] activationAttributes) {
            CodeContract.Requires(this != null);

          return default(System.Runtime.Remoting.ObjectHandle);
        }
        public System.Runtime.Remoting.ObjectHandle CreateInstance (string! assemblyName, string typeName, Object[] activationAttributes) {
            CodeContract.Requires(this != null);
            CodeContract.Requires(assemblyName != null);

          return default(System.Runtime.Remoting.ObjectHandle);
        }
        public System.Runtime.Remoting.ObjectHandle CreateComInstanceFrom (string! assemblyFile, string typeName, Byte[] hashValue, System.Configuration.Assemblies.AssemblyHashAlgorithm hashAlgorithm) {
            CodeContract.Requires(this != null);
            CodeContract.Requires(assemblyFile != null);

          return default(System.Runtime.Remoting.ObjectHandle);
        }
        public System.Runtime.Remoting.ObjectHandle CreateComInstanceFrom (string! assemblyName, string typeName) {
            CodeContract.Requires(this != null);
            CodeContract.Requires(assemblyName != null);

          return default(System.Runtime.Remoting.ObjectHandle);
        }
        public System.Runtime.Remoting.ObjectHandle CreateInstanceFrom (string assemblyFile, string typeName) {
            CodeContract.Requires(this != null);

          return default(System.Runtime.Remoting.ObjectHandle);
        }
        public System.Runtime.Remoting.ObjectHandle CreateInstance (string! assemblyName, string typeName) {
            CodeContract.Requires(this != null);
            CodeContract.Requires(assemblyName != null);

          return default(System.Runtime.Remoting.ObjectHandle);
        }
        public System.Reflection.Emit.AssemblyBuilder DefineDynamicAssembly (System.Reflection.AssemblyName name, System.Reflection.Emit.AssemblyBuilderAccess access, string dir, System.Security.Policy.Evidence evidence, System.Security.PermissionSet requiredPermissions, System.Security.PermissionSet optionalPermissions, System.Security.PermissionSet refusedPermissions, bool isSynchronized) {

          return default(System.Reflection.Emit.AssemblyBuilder);
        }
        public System.Reflection.Emit.AssemblyBuilder DefineDynamicAssembly (System.Reflection.AssemblyName name, System.Reflection.Emit.AssemblyBuilderAccess access, string dir, System.Security.Policy.Evidence evidence, System.Security.PermissionSet requiredPermissions, System.Security.PermissionSet optionalPermissions, System.Security.PermissionSet refusedPermissions) {

          return default(System.Reflection.Emit.AssemblyBuilder);
        }
        public System.Reflection.Emit.AssemblyBuilder DefineDynamicAssembly (System.Reflection.AssemblyName name, System.Reflection.Emit.AssemblyBuilderAccess access, System.Security.Policy.Evidence evidence, System.Security.PermissionSet requiredPermissions, System.Security.PermissionSet optionalPermissions, System.Security.PermissionSet refusedPermissions) {

          return default(System.Reflection.Emit.AssemblyBuilder);
        }
        public System.Reflection.Emit.AssemblyBuilder DefineDynamicAssembly (System.Reflection.AssemblyName name, System.Reflection.Emit.AssemblyBuilderAccess access, string dir, System.Security.PermissionSet requiredPermissions, System.Security.PermissionSet optionalPermissions, System.Security.PermissionSet refusedPermissions) {

          return default(System.Reflection.Emit.AssemblyBuilder);
        }
        public System.Reflection.Emit.AssemblyBuilder DefineDynamicAssembly (System.Reflection.AssemblyName name, System.Reflection.Emit.AssemblyBuilderAccess access, string dir, System.Security.Policy.Evidence evidence) {

          return default(System.Reflection.Emit.AssemblyBuilder);
        }
        public System.Reflection.Emit.AssemblyBuilder DefineDynamicAssembly (System.Reflection.AssemblyName name, System.Reflection.Emit.AssemblyBuilderAccess access, System.Security.PermissionSet requiredPermissions, System.Security.PermissionSet optionalPermissions, System.Security.PermissionSet refusedPermissions) {

          return default(System.Reflection.Emit.AssemblyBuilder);
        }
        public System.Reflection.Emit.AssemblyBuilder DefineDynamicAssembly (System.Reflection.AssemblyName name, System.Reflection.Emit.AssemblyBuilderAccess access, System.Security.Policy.Evidence evidence) {

          return default(System.Reflection.Emit.AssemblyBuilder);
        }
        public System.Reflection.Emit.AssemblyBuilder DefineDynamicAssembly (System.Reflection.AssemblyName name, System.Reflection.Emit.AssemblyBuilderAccess access, string dir) {

          return default(System.Reflection.Emit.AssemblyBuilder);
        }
        public System.Reflection.Emit.AssemblyBuilder DefineDynamicAssembly (System.Reflection.AssemblyName name, System.Reflection.Emit.AssemblyBuilderAccess access) {

          return default(System.Reflection.Emit.AssemblyBuilder);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)][ResultNotNewlyAllocated]
        public Type GetType () {

          return default(Type);
        }
        public void remove_UnhandledException (UnhandledExceptionEventHandler value) {

        }
        public void add_UnhandledException (UnhandledExceptionEventHandler value) {

        }
        public void remove_AssemblyResolve (ResolveEventHandler value) {

        }
        public void add_AssemblyResolve (ResolveEventHandler value) {

        }
        public void remove_ResourceResolve (ResolveEventHandler value) {

        }
        public void add_ResourceResolve (ResolveEventHandler value) {

        }
        public void remove_TypeResolve (ResolveEventHandler value) {

        }
        public void add_TypeResolve (ResolveEventHandler value) {

        }
        public void remove_ProcessExit (EventHandler value) {

        }
        public void add_ProcessExit (EventHandler value) {

        }
        public void remove_AssemblyLoad (AssemblyLoadEventHandler value) {

        }
        public void add_AssemblyLoad (AssemblyLoadEventHandler value) {

        }
        public void remove_DomainUnload (EventHandler value) {

        }
        public void add_DomainUnload (EventHandler value) {
        }
    }
}
