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

// File System.Reflection.Emit.AssemblyBuilder.cs
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


namespace System.Reflection.Emit
{
  sealed public partial class AssemblyBuilder : System.Reflection.Assembly, System.Runtime.InteropServices._AssemblyBuilder
  {
    #region Methods and constructors
    public void AddResourceFile(string name, string fileName, System.Reflection.ResourceAttributes attribute)
    {
    }

    public void AddResourceFile(string name, string fileName)
    {
    }

    internal AssemblyBuilder()
    {
    }

    public ModuleBuilder DefineDynamicModule(string name)
    {
      Contract.Ensures(Contract.Result<System.Reflection.Emit.ModuleBuilder>() != null);

      return default(ModuleBuilder);
    }

    public ModuleBuilder DefineDynamicModule(string name, string fileName)
    {
      Contract.Ensures(Contract.Result<System.Reflection.Emit.ModuleBuilder>() != null);

      return default(ModuleBuilder);
    }

    public ModuleBuilder DefineDynamicModule(string name, bool emitSymbolInfo)
    {
      Contract.Ensures(Contract.Result<System.Reflection.Emit.ModuleBuilder>() != null);

      return default(ModuleBuilder);
    }

    public ModuleBuilder DefineDynamicModule(string name, string fileName, bool emitSymbolInfo)
    {
      Contract.Ensures(Contract.Result<System.Reflection.Emit.ModuleBuilder>() != null);

      return default(ModuleBuilder);
    }

    public System.Resources.IResourceWriter DefineResource(string name, string description, string fileName, System.Reflection.ResourceAttributes attribute)
    {
      Contract.Ensures(Contract.Result<System.Resources.IResourceWriter>() != null);

      return default(System.Resources.IResourceWriter);
    }

    public System.Resources.IResourceWriter DefineResource(string name, string description, string fileName)
    {
      Contract.Ensures(Contract.Result<System.Resources.IResourceWriter>() != null);

      return default(System.Resources.IResourceWriter);
    }

    public void DefineUnmanagedResource(byte[] resource)
    {
    }

    public void DefineUnmanagedResource(string resourceFileName)
    {
    }

    public void DefineVersionInfoResource(string product, string productVersion, string company, string copyright, string trademark)
    {
    }

    public void DefineVersionInfoResource()
    {
    }

    public override bool Equals(Object obj)
    {
      return default(bool);
    }

    public override Object[] GetCustomAttributes(bool inherit)
    {
      return default(Object[]);
    }

    public override Object[] GetCustomAttributes(Type attributeType, bool inherit)
    {
      return default(Object[]);
    }

    public override IList<System.Reflection.CustomAttributeData> GetCustomAttributesData()
    {
      return default(IList<System.Reflection.CustomAttributeData>);
    }

    public ModuleBuilder GetDynamicModule(string name)
    {
      return default(ModuleBuilder);
    }

    public override Type[] GetExportedTypes()
    {
      return default(Type[]);
    }

    public override FileStream GetFile(string name)
    {
      return default(FileStream);
    }

    public override FileStream[] GetFiles(bool getResourceModules)
    {
      return default(FileStream[]);
    }

    public override int GetHashCode()
    {
      return default(int);
    }

    public override System.Reflection.Module[] GetLoadedModules(bool getResourceModules)
    {
      return default(System.Reflection.Module[]);
    }

    public override System.Reflection.ManifestResourceInfo GetManifestResourceInfo(string resourceName)
    {
      return default(System.Reflection.ManifestResourceInfo);
    }

    public override string[] GetManifestResourceNames()
    {
      return default(string[]);
    }

    public override Stream GetManifestResourceStream(string name)
    {
      return default(Stream);
    }

    public override Stream GetManifestResourceStream(Type type, string name)
    {
      return default(Stream);
    }

    public override System.Reflection.Module GetModule(string name)
    {
      return default(System.Reflection.Module);
    }

    public override System.Reflection.Module[] GetModules(bool getResourceModules)
    {
      return default(System.Reflection.Module[]);
    }

    public override System.Reflection.AssemblyName GetName(bool copiedName)
    {
      return default(System.Reflection.AssemblyName);
    }

    public override System.Reflection.AssemblyName[] GetReferencedAssemblies()
    {
      return default(System.Reflection.AssemblyName[]);
    }

    public override System.Reflection.Assembly GetSatelliteAssembly(System.Globalization.CultureInfo culture, Version version)
    {
      return default(System.Reflection.Assembly);
    }

    public override System.Reflection.Assembly GetSatelliteAssembly(System.Globalization.CultureInfo culture)
    {
      return default(System.Reflection.Assembly);
    }

    public override Type GetType(string name, bool throwOnError, bool ignoreCase)
    {
      return default(Type);
    }

    public override bool IsDefined(Type attributeType, bool inherit)
    {
      return default(bool);
    }

    public void Save(string assemblyFileName)
    {
    }

    public void Save(string assemblyFileName, System.Reflection.PortableExecutableKinds portableExecutableKind, System.Reflection.ImageFileMachine imageFileMachine)
    {
    }

    public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
    {
    }

    public void SetCustomAttribute(System.Reflection.ConstructorInfo con, byte[] binaryAttribute)
    {
    }

    public void SetEntryPoint(System.Reflection.MethodInfo entryMethod)
    {
    }

    public void SetEntryPoint(System.Reflection.MethodInfo entryMethod, PEFileKinds fileKind)
    {
    }

    void System.Runtime.InteropServices._AssemblyBuilder.GetIDsOfNames(ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
    {
    }

    void System.Runtime.InteropServices._AssemblyBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
    {
    }

    void System.Runtime.InteropServices._AssemblyBuilder.GetTypeInfoCount(out uint pcTInfo)
    {
      pcTInfo = default(uint);
    }

    void System.Runtime.InteropServices._AssemblyBuilder.Invoke(uint dispIdMember, ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
    {
    }
    #endregion

    #region Properties and indexers
    public override string CodeBase
    {
      get
      {
        return default(string);
      }
    }

    public override System.Reflection.MethodInfo EntryPoint
    {
      get
      {
        return default(System.Reflection.MethodInfo);
      }
    }

    public override System.Security.Policy.Evidence Evidence
    {
      get
      {
        return default(System.Security.Policy.Evidence);
      }
    }

    public override string FullName
    {
      get
      {
        return default(string);
      }
    }

    public override bool GlobalAssemblyCache
    {
      get
      {
        return default(bool);
      }
    }

    public override long HostContext
    {
      get
      {
        return default(long);
      }
    }

    public override string ImageRuntimeVersion
    {
      get
      {
        return default(string);
      }
    }

    public override bool IsDynamic
    {
      get
      {
        return default(bool);
      }
    }

    public override string Location
    {
      get
      {
        return default(string);
      }
    }

    public override System.Reflection.Module ManifestModule
    {
      get
      {
        return default(System.Reflection.Module);
      }
    }

    public override System.Security.PermissionSet PermissionSet
    {
      get
      {
        return default(System.Security.PermissionSet);
      }
    }

    public override bool ReflectionOnly
    {
      get
      {
        return default(bool);
      }
    }

    public override System.Security.SecurityRuleSet SecurityRuleSet
    {
      get
      {
        return default(System.Security.SecurityRuleSet);
      }
    }
    #endregion
  }
}
