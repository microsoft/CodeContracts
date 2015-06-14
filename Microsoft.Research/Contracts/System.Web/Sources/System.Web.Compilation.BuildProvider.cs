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

// File System.Web.Compilation.BuildProvider.cs
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
  abstract public partial class BuildProvider
  {
    #region Methods and constructors
    protected BuildProvider()
    {
    }

    public virtual new void GenerateCode(AssemblyBuilder assemblyBuilder)
    {
    }

    protected internal virtual new System.CodeDom.CodeCompileUnit GetCodeCompileUnit(out System.Collections.IDictionary linePragmasTable)
    {
      linePragmasTable = default(System.Collections.IDictionary);

      return default(System.CodeDom.CodeCompileUnit);
    }

    public virtual new string GetCustomString(System.CodeDom.Compiler.CompilerResults results)
    {
      return default(string);
    }

    protected CompilerType GetDefaultCompilerType()
    {
      return default(CompilerType);
    }

    protected CompilerType GetDefaultCompilerTypeForLanguage(string language)
    {
      return default(CompilerType);
    }

    public virtual new Type GetGeneratedType(System.CodeDom.Compiler.CompilerResults results)
    {
      return default(Type);
    }

    public virtual new BuildProviderResultFlags GetResultFlags(System.CodeDom.Compiler.CompilerResults results)
    {
      return default(BuildProviderResultFlags);
    }

    protected TextReader OpenReader()
    {
      return default(TextReader);
    }

    protected TextReader OpenReader(string virtualPath)
    {
      return default(TextReader);
    }

    protected Stream OpenStream()
    {
      return default(Stream);
    }

    protected Stream OpenStream(string virtualPath)
    {
      return default(Stream);
    }

    public static void RegisterBuildProvider(string extension, Type providerType)
    {
    }
    #endregion

    #region Properties and indexers
    public virtual new CompilerType CodeCompilerType
    {
      get
      {
        return default(CompilerType);
      }
    }

    protected System.Collections.ICollection ReferencedAssemblies
    {
      get
      {
        return default(System.Collections.ICollection);
      }
    }

    internal protected string VirtualPath
    {
      get
      {
        return default(string);
      }
    }

    public virtual new System.Collections.ICollection VirtualPathDependencies
    {
      get
      {
        return default(System.Collections.ICollection);
      }
    }
    #endregion
  }
}
