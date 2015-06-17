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

// File System.Web.Compilation.ClientBuildManager.cs
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


namespace System.Web.Compilation
{
  sealed public partial class ClientBuildManager : MarshalByRefObject, IDisposable
  {
    #region Methods and constructors
    public ClientBuildManager(string appVirtualDir, string appPhysicalSourceDir)
    {
    }

    public ClientBuildManager(string appVirtualDir, string appPhysicalSourceDir, string appPhysicalTargetDir, ClientBuildManagerParameter parameter)
    {
    }

    public ClientBuildManager(string appVirtualDir, string appPhysicalSourceDir, string appPhysicalTargetDir)
    {
    }

    public ClientBuildManager(string appVirtualDir, string appPhysicalSourceDir, string appPhysicalTargetDir, ClientBuildManagerParameter parameter, System.ComponentModel.TypeDescriptionProvider typeDescriptionProvider)
    {
    }

    public void CompileApplicationDependencies()
    {
    }

    public void CompileFile(string virtualPath)
    {
    }

    public void CompileFile(string virtualPath, ClientBuildManagerCallback callback)
    {
    }

    public System.Web.Hosting.IRegisteredObject CreateObject(Type type, bool failIfExists)
    {
      return default(System.Web.Hosting.IRegisteredObject);
    }

    public string GenerateCode(string virtualPath, string virtualFileString, out System.Collections.IDictionary linePragmasTable)
    {
      linePragmasTable = default(System.Collections.IDictionary);

      return default(string);
    }

    public System.CodeDom.CodeCompileUnit GenerateCodeCompileUnit(string virtualPath, string virtualFileString, out Type codeDomProviderType, out System.CodeDom.Compiler.CompilerParameters compilerParameters, out System.Collections.IDictionary linePragmasTable)
    {
      codeDomProviderType = default(Type);
      compilerParameters = default(System.CodeDom.Compiler.CompilerParameters);
      linePragmasTable = default(System.Collections.IDictionary);

      return default(System.CodeDom.CodeCompileUnit);
    }

    public System.CodeDom.CodeCompileUnit GenerateCodeCompileUnit(string virtualPath, out Type codeDomProviderType, out System.CodeDom.Compiler.CompilerParameters compilerParameters, out System.Collections.IDictionary linePragmasTable)
    {
      codeDomProviderType = default(Type);
      compilerParameters = default(System.CodeDom.Compiler.CompilerParameters);
      linePragmasTable = default(System.Collections.IDictionary);

      return default(System.CodeDom.CodeCompileUnit);
    }

    public string[] GetAppDomainShutdownDirectories()
    {
      return default(string[]);
    }

    public System.Collections.IDictionary GetBrowserDefinitions()
    {
      return default(System.Collections.IDictionary);
    }

    public void GetCodeDirectoryInformation(string virtualCodeDir, out Type codeDomProviderType, out System.CodeDom.Compiler.CompilerParameters compilerParameters, out string generatedFilesDir)
    {
      codeDomProviderType = default(Type);
      compilerParameters = default(System.CodeDom.Compiler.CompilerParameters);
      generatedFilesDir = default(string);
    }

    public Type GetCompiledType(string virtualPath)
    {
      return default(Type);
    }

    public void GetCompilerParameters(string virtualPath, out Type codeDomProviderType, out System.CodeDom.Compiler.CompilerParameters compilerParameters)
    {
      codeDomProviderType = default(Type);
      compilerParameters = default(System.CodeDom.Compiler.CompilerParameters);
    }

    public string GetGeneratedFileVirtualPath(string filePath)
    {
      return default(string);
    }

    public string GetGeneratedSourceFile(string virtualPath)
    {
      return default(string);
    }

    public string[] GetTopLevelAssemblyReferences(string virtualPath)
    {
      return default(string[]);
    }

    public string[] GetVirtualCodeDirectories()
    {
      return default(string[]);
    }

    public override Object InitializeLifetimeService()
    {
      return default(Object);
    }

    public bool IsCodeAssembly(string assemblyName)
    {
      return default(bool);
    }

    public void PrecompileApplication()
    {
    }

    public void PrecompileApplication(ClientBuildManagerCallback callback)
    {
    }

    public void PrecompileApplication(ClientBuildManagerCallback callback, bool forceCleanBuild)
    {
    }

    void System.IDisposable.Dispose()
    {
    }

    public bool Unload()
    {
      return default(bool);
    }
    #endregion

    #region Properties and indexers
    public string CodeGenDir
    {
      get
      {
        return default(string);
      }
    }

    public bool IsHostCreated
    {
      get
      {
        return default(bool);
      }
    }
    #endregion

    #region Events
    public event BuildManagerHostUnloadEventHandler AppDomainShutdown
    {
      add
      {
      }
      remove
      {
      }
    }

    public event EventHandler AppDomainStarted
    {
      add
      {
      }
      remove
      {
      }
    }

    public event BuildManagerHostUnloadEventHandler AppDomainUnloaded
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
