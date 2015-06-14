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

    public class Assembly
    {

        public string! FullName
        {
          get;
        }

        public string EscapedCodeBase
        {
          get;
        }

        public MethodInfo EntryPoint
        {
          get;
        }

        public bool GlobalAssemblyCache
        {
          get;
        }

        public System.Security.Policy.Evidence Evidence
        {
          get;
        }

        public string! CodeBase
        {
          get;
        }

        public string Location
        {
          get;
        }

        public string ImageRuntimeVersion
        {
          get;
        }

        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public string ToString () {

          CodeContract.Ensures(CodeContract.Result<string>() != null);
          return default(string);
        }
        public ManifestResourceInfo GetManifestResourceInfo (string resourceName) {

          return default(ManifestResourceInfo);
        }
        public AssemblyName[] GetReferencedAssemblies () {

          return default(AssemblyName[]);
        }
        public static Assembly GetEntryAssembly () {

          return default(Assembly);
        }
        public static Assembly GetCallingAssembly () {

          return default(Assembly);
        }
        public static Assembly GetExecutingAssembly () {

          CodeContract.Ensures(CodeContract.Result<Assembly>() != null);
          return default(Assembly);
        }
        public String[] GetManifestResourceNames () {

          CodeContract.Ensures(CodeContract.Result<String[]>() != null);
          return default(String[]);
        }
        public System.IO.FileStream[] GetFiles (bool getResourceModules) {

          CodeContract.Ensures(CodeContract.Result<System.IO.FileStream[]>() != null);
          return default(System.IO.FileStream[]);
        }
        public System.IO.FileStream[] GetFiles () {

          CodeContract.Ensures(CodeContract.Result<System.IO.FileStream[]>() != null);
          return default(System.IO.FileStream[]);
        }
        public System.IO.FileStream GetFile (string name) {

          return default(System.IO.FileStream);
        }
        public Module GetModule (string arg0) {

          return default(Module);
        }
        public Module[] GetModules (bool getResourceModules) {

          CodeContract.Ensures(CodeContract.Result<Module[]>() != null);
          return default(Module[]);
        }
        public Module[] GetModules () {

          CodeContract.Ensures(CodeContract.Result<Module[]>() != null);
          return default(Module[]);
        }
        public Module[] GetLoadedModules (bool getResourceModules) {

          CodeContract.Ensures(CodeContract.Result<Module[]>() != null);
          return default(Module[]);
        }
        public Module[] GetLoadedModules () {

          CodeContract.Ensures(CodeContract.Result<Module[]>() != null);
          return default(Module[]);
        }
        public object CreateInstance (string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, Object[] args, System.Globalization.CultureInfo culture, Object[] activationAttributes) {

          return default(object);
        }
        public object CreateInstance (string typeName, bool ignoreCase) {

          return default(object);
        }
        public object CreateInstance (string typeName) {

          return default(object);
        }
        public Module LoadModule (string moduleName, Byte[] rawModule, Byte[] rawSymbolStore) {

          return default(Module);
        }
        public Module LoadModule (string moduleName, Byte[] rawModule) {

          return default(Module);
        }
        public static Assembly LoadFile (string path, System.Security.Policy.Evidence securityEvidence) {

          return default(Assembly);
        }
        public static Assembly LoadFile (string path) {

          return default(Assembly);
        }
        public static Assembly Load (Byte[] rawAssembly, Byte[] rawSymbolStore, System.Security.Policy.Evidence securityEvidence) {

          return default(Assembly);
        }
        public static Assembly Load (Byte[] rawAssembly, Byte[] rawSymbolStore) {

          return default(Assembly);
        }
        public static Assembly Load (Byte[] rawAssembly) {

          return default(Assembly);
        }
        public static Assembly LoadWithPartialName (string partialName, System.Security.Policy.Evidence securityEvidence) {

          return default(Assembly);
        }
        public static Assembly LoadWithPartialName (string partialName) {

          return default(Assembly);
        }
        public static Assembly Load (AssemblyName assemblyRef, System.Security.Policy.Evidence assemblySecurity) {

          return default(Assembly);
        }
        public static Assembly Load (AssemblyName assemblyRef) {

          return default(Assembly);
        }
        public static Assembly Load (string assemblyString, System.Security.Policy.Evidence assemblySecurity) {

          return default(Assembly);
        }
        public static Assembly Load (string assemblyString) {

          return default(Assembly);
        }
        public static Assembly LoadFrom (string! assemblyFile, System.Security.Policy.Evidence securityEvidence, Byte[] hashValue, System.Configuration.Assemblies.AssemblyHashAlgorithm hashAlgorithm) {
            CodeContract.Requires(assemblyFile != null);

          return default(Assembly);
        }
        public static Assembly LoadFrom (string assemblyFile, System.Security.Policy.Evidence securityEvidence) {

          return default(Assembly);
        }
        public static Assembly LoadFrom (string assemblyFile) {

          return default(Assembly);
        }
        public bool IsDefined (Type! attributeType, bool inherit) {
            CodeContract.Requires(attributeType != null);

          return default(bool);
        }
        public Object[] GetCustomAttributes (Type! attributeType, bool inherit) {
            CodeContract.Requires(attributeType != null);

          CodeContract.Ensures(CodeContract.Result<Object[]>() != null);
          return default(Object[]);
        }
        public Object[] GetCustomAttributes (bool inherit) {

          CodeContract.Ensures(CodeContract.Result<Object[]>() != null);
          return default(Object[]);
        }
        public void GetObjectData (System.Runtime.Serialization.SerializationInfo! info, System.Runtime.Serialization.StreamingContext context) {
            CodeContract.Requires(info != null);

        }
        public Assembly GetSatelliteAssembly (System.Globalization.CultureInfo culture, Version version) {

          return default(Assembly);
        }
        public Assembly GetSatelliteAssembly (System.Globalization.CultureInfo culture) {

          return default(Assembly);
        }
        public System.IO.Stream GetManifestResourceStream (string name) {

          return default(System.IO.Stream);
        }
        public System.IO.Stream GetManifestResourceStream (Type type, string name) {

          return default(System.IO.Stream);
        }
        public Type[] GetTypes () {

          CodeContract.Ensures(CodeContract.Result<Type[]>() != null);
          return default(Type[]);
        }
        public Type[] GetExportedTypes () {

          CodeContract.Ensures(CodeContract.Result<Type[]>() != null);
          return default(Type[]);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)][ResultNotNewlyAllocated]
        public Type GetType () {

          CodeContract.Ensures(CodeContract.Result<Type>() != null);
          return default(Type);
        }
        public Type GetType (string arg0, bool arg1, bool arg2) {

          return default(Type);
        }
        public Type GetType (string arg0, bool arg1) {

          return default(Type);
        }
        public Type GetType (string arg0) {

          return default(Type);
        }
        public static Assembly GetAssembly (Type! type) {
            CodeContract.Requires(type != null);

          return default(Assembly);
        }
        public static string CreateQualifiedName (string arg0, string arg1) {

          return default(string);
        }
        public AssemblyName GetName (bool copiedName) {

          CodeContract.Ensures(CodeContract.Result<AssemblyName>() != null);
          return default(AssemblyName);
        }
        public AssemblyName GetName () {

          CodeContract.Ensures(CodeContract.Result<AssemblyName>() != null);
          return default(AssemblyName);
        }
        public void remove_ModuleResolve (ModuleResolveEventHandler value) {

        }
        public void add_ModuleResolve (ModuleResolveEventHandler value) {
        }
    }
}
