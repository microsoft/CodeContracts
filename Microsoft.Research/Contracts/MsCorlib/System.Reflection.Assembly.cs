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
#if !SILVERLIGHT_4_0_WP
using System.Security.Policy;
#endif
using System.Diagnostics.Contracts;

namespace System.Reflection
{

  public class Assembly
  {
#if NETFRAMEWORK_4_0 || SILVERLIGHT_5_0
    protected
#else
    internal 
#endif
      Assembly() { }

    public virtual string FullName
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
      }
    }

#if !SILVERLIGHT
    public virtual string EscapedCodeBase
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
      }
    }

    // Summary:
    //     Gets the evidence for this assembly.
    //
    // Returns:
    //     An Evidence object for this assembly.
    public virtual Evidence Evidence 
    {
      get
      {
        Contract.Ensures(Contract.Result<Evidence>() != null);

        return default(Evidence);
      }
    }
#endif

#if false
    extern public MethodInfo EntryPoint
    {
      get;
    }
#endif

#if !SILVERLIGHT
    extern public virtual bool GlobalAssemblyCache
    {
      get;
    }
#endif

#if !SILVERLIGHT_4_0_WP
    // Summary:
    //     Gets the location of the assembly as specified originally, for example, in
    //     an System.Reflection.AssemblyName object.
    //
    // Returns:
    //     The location of the assembly as specified originally.
    public virtual string CodeBase
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
      }
    }
#endif

#if !SILVERLIGHT_4_0_WP
    //
    // Summary:
    //     Gets the path or UNC location of the loaded file that contains the manifest.
    //
    // Returns:
    //     The location of the loaded file that contains the manifest. If the loaded
    //     file was shadow-copied, the location is that of the file after being shadow-copied.
    //     If the assembly is loaded from a byte array, such as when using the System.Reflection.Assembly.Load(System.Byte[])
    //     method overload, the value returned is an empty string ("").
    public virtual string Location
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
      }
    }
#endif

    //
    // Summary:
    //     Gets a string representing the version of the common language runtime (CLR)
    //     saved in the file containing the manifest.
    //
    // Returns:
    //     A string representing the CLR version folder name. This is not a full path.
    public virtual string ImageRuntimeVersion
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
      }
    }

    //
    // Summary:
    //     Gets the module that contains the manifest for the current assembly.
    //
    // Returns:
    //     A System.Reflection.Module object representing the module that contains the
    //     manifest for the assembly.
    //[ComVisible(false)]
#if SILVERLIGHT_4_0 || NETFRAMEWORK_4_0 || SILVERLIGHT_5_0
    virtual
#endif
    public Module ManifestModule
    {
      get
      {
        Contract.Ensures(Contract.Result<Module>() != null);

        return default(Module);
      }
    }

#if !SILVERLIGHT
    //
    // Summary:
    //     Gets the System.Reflection.AssemblyName objects for all the assemblies referenced
    //     by this assembly.
    //
    // Returns:
    //     An array of type System.Reflection.AssemblyName containing all the assemblies
    //     referenced by this assembly.
    public virtual AssemblyName[] GetReferencedAssemblies()
    {
      Contract.Ensures(Contract.Result<AssemblyName[]>() != null);

      return default(AssemblyName[]);
    }

    public static Assembly GetEntryAssembly()
    {

      return default(Assembly);
    }
#endif

    //
    // Summary:
    //     Returns the System.Reflection.Assembly of the method that invoked the currently
    //     executing method.
    //
    // Returns:
    //     The Assembly object of the method that invoked the currently executing method.
    public static Assembly GetCallingAssembly()
    {
      Contract.Ensures(Contract.Result<Assembly>() != null);

      return default(Assembly);
    }
    public static Assembly GetExecutingAssembly()
    {

      Contract.Ensures(Contract.Result<Assembly>() != null);
      return default(Assembly);
    }
    public virtual String[] GetManifestResourceNames()
    {

      Contract.Ensures(Contract.Result<String[]>() != null);
      return default(String[]);
    }

#if !SILVERLIGHT_4_0_WP
    public virtual System.IO.FileStream[] GetFiles(bool getResourceModules)
    {

      Contract.Ensures(Contract.Result<System.IO.FileStream[]>() != null);
      return default(System.IO.FileStream[]);
    }
#endif

#if !SILVERLIGHT
    public virtual System.IO.FileStream[] GetFiles()
    {
      Contract.Ensures(Contract.Result<System.IO.FileStream[]>() != null);
      return default(System.IO.FileStream[]);
    }
#endif

#if !SILVERLIGHT_4_0_WP
    public virtual System.IO.FileStream GetFile(string name)
    {

      return default(System.IO.FileStream);
    }
#endif

#if !SILVERLIGHT
    public virtual Module GetModule(string arg0)
    {

      return default(Module);
    }

    public virtual Module[] GetModules(bool getResourceModules)
    {

      Contract.Ensures(Contract.Result<Module[]>() != null);
      return default(Module[]);
    }
#endif

    public virtual Module[] GetModules()
    {

      Contract.Ensures(Contract.Result<Module[]>() != null);
      return default(Module[]);
    }

#if !SILVERLIGHT
    [Pure]
    public virtual Module[] GetLoadedModules(bool getResourceModules)
    {
      Contract.Ensures(Contract.Result<Module[]>() != null);
    
      return default(Module[]);
    }

    public virtual Module[] GetLoadedModules()
    {
      Contract.Ensures(Contract.Result<Module[]>() != null);
     
      return default(Module[]);
    }
#endif

    //public object CreateInstance(string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, Object[] args, System.Globalization.CultureInfo culture, Object[] activationAttributes)
    //{

    //  return default(object);
    //}

#if !SILVERLIGHT
    public virtual object CreateInstance(string typeName, bool ignoreCase)
    {

      return default(object);
    }
#endif

    public virtual object CreateInstance(string typeName)
    {

      return default(object);
    }
#if false
    public Module LoadModule(string moduleName, Byte[] rawModule, Byte[] rawSymbolStore)
    {

      return default(Module);
    }
    public Module LoadModule(string moduleName, Byte[] rawModule)
    {

      return default(Module);
    }
    public static Assembly LoadFile(string path, System.Security.Policy.Evidence securityEvidence)
    {

      return default(Assembly);
    }
#endif

#if !SILVERLIGHT
    public static Assembly LoadFile(string path)
    {
      Contract.Requires(!String.IsNullOrEmpty(path));

      Contract.Ensures(Contract.Result<Assembly>() != null);

      return default(Assembly);
    }

#endif

#if false
    public static Assembly Load(Byte[] rawAssembly, Byte[] rawSymbolStore, System.Security.Policy.Evidence securityEvidence)
    {
      Contract.Ensures(Contract.Result<Assembly>() != null);

      return default(Assembly);
    }
#endif

#if !SILVERLIGHT
    public static Assembly Load(Byte[] rawAssembly, Byte[] rawSymbolStore)
    {
      Contract.Ensures(Contract.Result<Assembly>() != null);

      return default(Assembly);
    }

    public static Assembly Load(Byte[] rawAssembly)
    {
      Contract.Ensures(Contract.Result<Assembly>() != null);

      return default(Assembly);
    }
#endif

#if false
    public static Assembly LoadWithPartialName(string partialName, System.Security.Policy.Evidence securityEvidence)
    {

      return default(Assembly);
    }
#endif

#if !SILVERLIGHT
    public static Assembly LoadWithPartialName(string partialName)
    {
      return default(Assembly);
    }
#endif

#if false
    public static Assembly Load(AssemblyName assemblyRef, System.Security.Policy.Evidence assemblySecurity)
    {

      return default(Assembly);
    }
#endif

#if !SILVERLIGHT_5_0
    public static Assembly Load(AssemblyName assemblyRef)
    {

      return default(Assembly);
    }
#endif

#if false
    public static Assembly Load(string assemblyString, System.Security.Policy.Evidence assemblySecurity)
    {

      return default(Assembly);
    }
#endif
    public static Assembly Load(string assemblyString)
    {

      return default(Assembly);
    }

#if false
    public static Assembly LoadFrom(string assemblyFile, System.Security.Policy.Evidence securityEvidence, Byte[] hashValue, System.Configuration.Assemblies.AssemblyHashAlgorithm hashAlgorithm)
    {

      return default(Assembly);
    }

    public static Assembly LoadFrom(string assemblyFile, System.Security.Policy.Evidence securityEvidence)
    {

      return default(Assembly);
    }
#endif
#if NETFRAMEWORK_4_0
    public static Assembly LoadFrom(string assemblyFile, Byte[] hashValue, System.Configuration.Assemblies.AssemblyHashAlgorithm hashAlgorithm)
    {
      Contract.Requires(assemblyFile != null);
      Contract.Ensures(Contract.Result<Assembly>() != null);
      return default(Assembly);
    }
#endif

#if !SILVERLIGHT_5_0
    public static Assembly LoadFrom(string assemblyFile)
    {
      Contract.Requires(assemblyFile != null);
      Contract.Ensures(Contract.Result<Assembly>() != null);
      return default(Assembly);
    }
#endif

#if false
    public void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
    {
      Contract.Requires(info != null);

    }
    public Assembly GetSatelliteAssembly(System.Globalization.CultureInfo culture, Version version)
    {

      return default(Assembly);
    }
    public Assembly GetSatelliteAssembly(System.Globalization.CultureInfo culture)
    {

      return default(Assembly);
    }
    public System.IO.Stream GetManifestResourceStream(string name)
    {

      return default(System.IO.Stream);
    }
    public System.IO.Stream GetManifestResourceStream(Type type, string name)
    {

      return default(System.IO.Stream);
    }
#endif
    public virtual Type[] GetTypes()
    {
      Contract.Ensures(Contract.Result<Type[]>() != null);
      Contract.Ensures(Contract.ForAll(Contract.Result<Type[]>(), el => el != null));

      return default(Type[]);
    }
    public virtual Type[] GetExportedTypes()
    {

      Contract.Ensures(Contract.Result<Type[]>() != null);
      return default(Type[]);
    }

#if !SILVERLIGHT
    public virtual Type GetType(string name, bool throwOnError, bool ignoreCase)
    {
      Contract.Requires(name != null);
      Contract.Ensures(Contract.Result<Type>() != null || !throwOnError);
      return default(Type);
    }
#endif

    public virtual Type GetType(string name, bool throwOnError)
    {
      Contract.Requires(name != null);
      Contract.Ensures(Contract.Result<Type>() != null || !throwOnError);
      return default(Type);
    }
    public virtual Type GetType(string arg0)
    {

      return default(Type);
    }
#if !SILVERLIGHT
    public static Assembly GetAssembly(Type type)
    {
      Contract.Requires(type != null);

      return default(Assembly);
    }

    public static string CreateQualifiedName(string arg0, string arg1)
    {

      return default(string);
    }
#endif

#if !SILVERLIGHT_5_0
    [Pure]
    public virtual AssemblyName GetName(bool copiedName)
    {

      Contract.Ensures(Contract.Result<AssemblyName>() != null);
      return default(AssemblyName);
    }
#endif

#if !SILVERLIGHT_5_0
    [Pure]
    public virtual AssemblyName GetName()
    {
      Contract.Ensures(Contract.Result<AssemblyName>() != null);
      return default(AssemblyName);
    }
#endif

#if false
    public void remove_ModuleResolve(ModuleResolveEventHandler value)
    {

    }
    public void add_ModuleResolve(ModuleResolveEventHandler value)
    {
    }
#endif

  }
}
