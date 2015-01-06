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

// File System.Runtime.InteropServices._Assembly.cs
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


namespace System.Runtime.InteropServices
{
  public partial interface _Assembly
  {
    #region Methods and constructors
    Object CreateInstance(string typeName, bool ignoreCase, System.Reflection.BindingFlags bindingAttr, System.Reflection.Binder binder, Object[] args, System.Globalization.CultureInfo culture, Object[] activationAttributes);

    Object CreateInstance(string typeName, bool ignoreCase);

    Object CreateInstance(string typeName);

    bool Equals(Object other);

    Object[] GetCustomAttributes(bool inherit);

    Object[] GetCustomAttributes(Type attributeType, bool inherit);

    Type[] GetExportedTypes();

    FileStream GetFile(string name);

    FileStream[] GetFiles();

    FileStream[] GetFiles(bool getResourceModules);

    int GetHashCode();

    System.Reflection.Module[] GetLoadedModules();

    System.Reflection.Module[] GetLoadedModules(bool getResourceModules);

    System.Reflection.ManifestResourceInfo GetManifestResourceInfo(string resourceName);

    string[] GetManifestResourceNames();

    Stream GetManifestResourceStream(Type type, string name);

    Stream GetManifestResourceStream(string name);

    System.Reflection.Module GetModule(string name);

    System.Reflection.Module[] GetModules(bool getResourceModules);

    System.Reflection.Module[] GetModules();

    System.Reflection.AssemblyName GetName();

    System.Reflection.AssemblyName GetName(bool copiedName);

    void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context);

    System.Reflection.AssemblyName[] GetReferencedAssemblies();

    System.Reflection.Assembly GetSatelliteAssembly(System.Globalization.CultureInfo culture, Version version);

    System.Reflection.Assembly GetSatelliteAssembly(System.Globalization.CultureInfo culture);

    Type GetType(string name);

    Type GetType(string name, bool throwOnError, bool ignoreCase);

    Type GetType();

    Type GetType(string name, bool throwOnError);

    Type[] GetTypes();

    bool IsDefined(Type attributeType, bool inherit);

    System.Reflection.Module LoadModule(string moduleName, byte[] rawModule, byte[] rawSymbolStore);

    System.Reflection.Module LoadModule(string moduleName, byte[] rawModule);

    string ToString();
    #endregion

    #region Properties and indexers
    string CodeBase
    {
      get;
    }

    System.Reflection.MethodInfo EntryPoint
    {
      get;
    }

    string EscapedCodeBase
    {
      get;
    }

    System.Security.Policy.Evidence Evidence
    {
      get;
    }

    string FullName
    {
      get;
    }

    bool GlobalAssemblyCache
    {
      get;
    }

    string Location
    {
      get;
    }
    #endregion

    #region Events
    event System.Reflection.ModuleResolveEventHandler ModuleResolve
    ;
    #endregion
  }
}
