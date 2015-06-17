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

using System.Diagnostics.Contracts;
using System;

namespace System.Reflection.Emit
{

    public class AssemblyBuilder
    {

        public string ImageRuntimeVersion
        {
          get;
        }

        public string CodeBase
        {
          get;
        }

        public System.Reflection.MethodInfo EntryPoint
        {
          get;
        }

        public string Location
        {
          get;
        }

        public Type[] GetExportedTypes () {

          return default(Type[]);
        }
        public void Save (string assemblyFileName) {

        }
        public void SetCustomAttribute (CustomAttributeBuilder customBuilder) {
            Contract.Requires(customBuilder != null);

        }
        public void SetCustomAttribute (System.Reflection.ConstructorInfo con, Byte[] binaryAttribute) {
            Contract.Requires(con != null);
            Contract.Requires(binaryAttribute != null);

        }
        public void SetEntryPoint (System.Reflection.MethodInfo entryMethod, PEFileKinds fileKind) {

        }
        public void SetEntryPoint (System.Reflection.MethodInfo entryMethod) {

        }
        public ModuleBuilder GetDynamicModule (string name) {

          return default(ModuleBuilder);
        }
        public void DefineUnmanagedResource (string resourceFileName) {
            Contract.Requires(resourceFileName != null);

        }
        public void DefineUnmanagedResource (Byte[] resource) {
            Contract.Requires(resource != null);

        }
        public void DefineVersionInfoResource () {

        }
        public void DefineVersionInfoResource (string product, string productVersion, string company, string copyright, string trademark) {

        }
        public System.Reflection.ManifestResourceInfo GetManifestResourceInfo (string resourceName) {

          return default(System.Reflection.ManifestResourceInfo);
        }
        public System.IO.Stream GetManifestResourceStream (string name) {

          return default(System.IO.Stream);
        }
        public System.IO.Stream GetManifestResourceStream (Type type, string name) {

          return default(System.IO.Stream);
        }
        public System.IO.FileStream[] GetFiles (bool getResourceModules) {

          return default(System.IO.FileStream[]);
        }
        public System.IO.FileStream GetFile (string name) {

          return default(System.IO.FileStream);
        }
        public String[] GetManifestResourceNames () {

          return default(String[]);
        }
        public void AddResourceFile (string name, string fileName, System.Reflection.ResourceAttributes attribute) {

        }
        public void AddResourceFile (string name, string fileName) {

        }
        public System.Resources.IResourceWriter DefineResource (string name, string description, string fileName, System.Reflection.ResourceAttributes attribute) {

          return default(System.Resources.IResourceWriter);
        }
        public System.Resources.IResourceWriter DefineResource (string name, string description, string fileName) {

          return default(System.Resources.IResourceWriter);
        }
        public ModuleBuilder DefineDynamicModule (string name, string fileName, bool emitSymbolInfo) {

          return default(ModuleBuilder);
        }
        public ModuleBuilder DefineDynamicModule (string name, string fileName) {

          return default(ModuleBuilder);
        }
        public ModuleBuilder DefineDynamicModule (string name, bool emitSymbolInfo) {

          return default(ModuleBuilder);
        }
        public ModuleBuilder DefineDynamicModule (string name) {
          return default(ModuleBuilder);
        }
    }
}
