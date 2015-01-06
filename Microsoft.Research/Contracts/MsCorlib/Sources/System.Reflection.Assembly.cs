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

// File System.Reflection.Assembly.cs
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


namespace System.Reflection
{
  abstract public partial class Assembly : System.Runtime.InteropServices._Assembly, System.Security.IEvidenceFactory, ICustomAttributeProvider, System.Runtime.Serialization.ISerializable
  {
    #region Methods and constructors
    public static bool operator != (System.Reflection.Assembly left, System.Reflection.Assembly right)
    {
      Contract.Ensures(Contract.Result<bool>() == ((left.Equals(right)) == false));

      return default(bool);
    }

    public static bool operator == (System.Reflection.Assembly left, System.Reflection.Assembly right)
    {
      return default(bool);
    }

    protected Assembly()
    {
    }

    public virtual new Object CreateInstance(string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, Object[] args, System.Globalization.CultureInfo culture, Object[] activationAttributes)
    {
      return default(Object);
    }

    public Object CreateInstance(string typeName)
    {
      return default(Object);
    }

    public Object CreateInstance(string typeName, bool ignoreCase)
    {
      return default(Object);
    }

    public static string CreateQualifiedName(string assemblyName, string typeName)
    {
      Contract.Ensures(Contract.Result<string>() != null);

      return default(string);
    }

    public override bool Equals(Object o)
    {
      return default(bool);
    }

    public static System.Reflection.Assembly GetAssembly(Type type)
    {
      return default(System.Reflection.Assembly);
    }

    public static System.Reflection.Assembly GetCallingAssembly()
    {
      return default(System.Reflection.Assembly);
    }

    public virtual new Object[] GetCustomAttributes(bool inherit)
    {
      return default(Object[]);
    }

    public virtual new Object[] GetCustomAttributes(Type attributeType, bool inherit)
    {
      return default(Object[]);
    }

    public virtual new IList<CustomAttributeData> GetCustomAttributesData()
    {
      return default(IList<CustomAttributeData>);
    }

    public static System.Reflection.Assembly GetEntryAssembly()
    {
      return default(System.Reflection.Assembly);
    }

    public static System.Reflection.Assembly GetExecutingAssembly()
    {
      return default(System.Reflection.Assembly);
    }

    public virtual new Type[] GetExportedTypes()
    {
      return default(Type[]);
    }

    public virtual new FileStream GetFile(string name)
    {
      return default(FileStream);
    }

    public virtual new FileStream[] GetFiles(bool getResourceModules)
    {
      return default(FileStream[]);
    }

    public virtual new FileStream[] GetFiles()
    {
      return default(FileStream[]);
    }

    public override int GetHashCode()
    {
      return default(int);
    }

    public Module[] GetLoadedModules()
    {
      return default(Module[]);
    }

    public virtual new Module[] GetLoadedModules(bool getResourceModules)
    {
      return default(Module[]);
    }

    public virtual new ManifestResourceInfo GetManifestResourceInfo(string resourceName)
    {
      return default(ManifestResourceInfo);
    }

    public virtual new string[] GetManifestResourceNames()
    {
      return default(string[]);
    }

    public virtual new Stream GetManifestResourceStream(string name)
    {
      return default(Stream);
    }

    public virtual new Stream GetManifestResourceStream(Type type, string name)
    {
      return default(Stream);
    }

    public virtual new Module GetModule(string name)
    {
      return default(Module);
    }

    public Module[] GetModules()
    {
      return default(Module[]);
    }

    public virtual new Module[] GetModules(bool getResourceModules)
    {
      return default(Module[]);
    }

    public virtual new AssemblyName GetName()
    {
      return default(AssemblyName);
    }

    public virtual new AssemblyName GetName(bool copiedName)
    {
      return default(AssemblyName);
    }

    public virtual new void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
    {
    }

    public virtual new AssemblyName[] GetReferencedAssemblies()
    {
      return default(AssemblyName[]);
    }

    public virtual new System.Reflection.Assembly GetSatelliteAssembly(System.Globalization.CultureInfo culture)
    {
      return default(System.Reflection.Assembly);
    }

    public virtual new System.Reflection.Assembly GetSatelliteAssembly(System.Globalization.CultureInfo culture, Version version)
    {
      return default(System.Reflection.Assembly);
    }

    public virtual new Type GetType(string name)
    {
      return default(Type);
    }

    public virtual new Type GetType(string name, bool throwOnError)
    {
      return default(Type);
    }

    public virtual new Type GetType(string name, bool throwOnError, bool ignoreCase)
    {
      return default(Type);
    }

    public virtual new Type[] GetTypes()
    {
      return default(Type[]);
    }

    public virtual new bool IsDefined(Type attributeType, bool inherit)
    {
      return default(bool);
    }

    public static System.Reflection.Assembly Load(string assemblyString, System.Security.Policy.Evidence assemblySecurity)
    {
      return default(System.Reflection.Assembly);
    }

    public static System.Reflection.Assembly Load(string assemblyString)
    {
      return default(System.Reflection.Assembly);
    }

    public static System.Reflection.Assembly Load(AssemblyName assemblyRef)
    {
      return default(System.Reflection.Assembly);
    }

    public static System.Reflection.Assembly Load(AssemblyName assemblyRef, System.Security.Policy.Evidence assemblySecurity)
    {
      return default(System.Reflection.Assembly);
    }

    public static System.Reflection.Assembly Load(byte[] rawAssembly)
    {
      return default(System.Reflection.Assembly);
    }

    public static System.Reflection.Assembly Load(byte[] rawAssembly, byte[] rawSymbolStore, System.Security.Policy.Evidence securityEvidence)
    {
      return default(System.Reflection.Assembly);
    }

    public static System.Reflection.Assembly Load(byte[] rawAssembly, byte[] rawSymbolStore)
    {
      return default(System.Reflection.Assembly);
    }

    public static System.Reflection.Assembly Load(byte[] rawAssembly, byte[] rawSymbolStore, System.Security.SecurityContextSource securityContextSource)
    {
      return default(System.Reflection.Assembly);
    }

    public static System.Reflection.Assembly LoadFile(string path)
    {
      return default(System.Reflection.Assembly);
    }

    public static System.Reflection.Assembly LoadFile(string path, System.Security.Policy.Evidence securityEvidence)
    {
      return default(System.Reflection.Assembly);
    }

    public static System.Reflection.Assembly LoadFrom(string assemblyFile, System.Security.Policy.Evidence securityEvidence, byte[] hashValue, System.Configuration.Assemblies.AssemblyHashAlgorithm hashAlgorithm)
    {
      return default(System.Reflection.Assembly);
    }

    public static System.Reflection.Assembly LoadFrom(string assemblyFile)
    {
      return default(System.Reflection.Assembly);
    }

    public static System.Reflection.Assembly LoadFrom(string assemblyFile, byte[] hashValue, System.Configuration.Assemblies.AssemblyHashAlgorithm hashAlgorithm)
    {
      return default(System.Reflection.Assembly);
    }

    public static System.Reflection.Assembly LoadFrom(string assemblyFile, System.Security.Policy.Evidence securityEvidence)
    {
      return default(System.Reflection.Assembly);
    }

    public Module LoadModule(string moduleName, byte[] rawModule)
    {
      return default(Module);
    }

    public virtual new Module LoadModule(string moduleName, byte[] rawModule, byte[] rawSymbolStore)
    {
      return default(Module);
    }

    public static System.Reflection.Assembly LoadWithPartialName(string partialName)
    {
      return default(System.Reflection.Assembly);
    }

    public static System.Reflection.Assembly LoadWithPartialName(string partialName, System.Security.Policy.Evidence securityEvidence)
    {
      return default(System.Reflection.Assembly);
    }

    public static System.Reflection.Assembly ReflectionOnlyLoad(string assemblyString)
    {
      return default(System.Reflection.Assembly);
    }

    public static System.Reflection.Assembly ReflectionOnlyLoad(byte[] rawAssembly)
    {
      return default(System.Reflection.Assembly);
    }

    public static System.Reflection.Assembly ReflectionOnlyLoadFrom(string assemblyFile)
    {
      return default(System.Reflection.Assembly);
    }

    Type System.Runtime.InteropServices._Assembly.GetType()
    {
      return default(Type);
    }

    public override string ToString()
    {
      return default(string);
    }

    public static System.Reflection.Assembly UnsafeLoadFrom(string assemblyFile)
    {
      return default(System.Reflection.Assembly);
    }
    #endregion

    #region Properties and indexers
    public virtual new string CodeBase
    {
      get
      {
        return default(string);
      }
    }

    public virtual new MethodInfo EntryPoint
    {
      get
      {
        return default(MethodInfo);
      }
    }

    public virtual new string EscapedCodeBase
    {
      get
      {
        return default(string);
      }
    }

    public virtual new System.Security.Policy.Evidence Evidence
    {
      get
      {
        return default(System.Security.Policy.Evidence);
      }
    }

    public virtual new string FullName
    {
      get
      {
        return default(string);
      }
    }

    public virtual new bool GlobalAssemblyCache
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new long HostContext
    {
      get
      {
        return default(long);
      }
    }

    public virtual new string ImageRuntimeVersion
    {
      get
      {
        return default(string);
      }
    }

    public virtual new bool IsDynamic
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsFullyTrusted
    {
      get
      {
        Contract.Requires(this.PermissionSet != null);

        return default(bool);
      }
    }

    public virtual new string Location
    {
      get
      {
        return default(string);
      }
    }

    public virtual new Module ManifestModule
    {
      get
      {
        return default(Module);
      }
    }

    public virtual new System.Security.PermissionSet PermissionSet
    {
      get
      {
        return default(System.Security.PermissionSet);
      }
    }

    public virtual new bool ReflectionOnly
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new System.Security.SecurityRuleSet SecurityRuleSet
    {
      get
      {
        return default(System.Security.SecurityRuleSet);
      }
    }
    #endregion

    #region Events
    public virtual new event ModuleResolveEventHandler ModuleResolve
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
