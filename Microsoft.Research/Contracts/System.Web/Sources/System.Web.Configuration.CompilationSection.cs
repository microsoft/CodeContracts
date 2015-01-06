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

// File System.Web.Configuration.CompilationSection.cs
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


namespace System.Web.Configuration
{
  sealed public partial class CompilationSection : System.Configuration.ConfigurationSection
  {
    #region Methods and constructors
    public CompilationSection()
    {
    }

    protected override Object GetRuntimeObject()
    {
      return default(Object);
    }

    protected override void PostDeserialize()
    {
    }
    #endregion

    #region Properties and indexers
    public AssemblyCollection Assemblies
    {
      get
      {
        return default(AssemblyCollection);
      }
    }

    public string AssemblyPostProcessorType
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public bool Batch
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public TimeSpan BatchTimeout
    {
      get
      {
        return default(TimeSpan);
      }
      set
      {
      }
    }

    public BuildProviderCollection BuildProviders
    {
      get
      {
        return default(BuildProviderCollection);
      }
    }

    public CodeSubDirectoriesCollection CodeSubDirectories
    {
      get
      {
        return default(CodeSubDirectoriesCollection);
      }
    }

    public CompilerCollection Compilers
    {
      get
      {
        return default(CompilerCollection);
      }
    }

    public bool Debug
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public string DefaultLanguage
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public bool Explicit
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public ExpressionBuilderCollection ExpressionBuilders
    {
      get
      {
        return default(ExpressionBuilderCollection);
      }
    }

    public FolderLevelBuildProviderCollection FolderLevelBuildProviders
    {
      get
      {
        return default(FolderLevelBuildProviderCollection);
      }
    }

    public int MaxBatchGeneratedFileSize
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public int MaxBatchSize
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public int NumRecompilesBeforeAppRestart
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public bool OptimizeCompilations
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    protected override System.Configuration.ConfigurationPropertyCollection Properties
    {
      get
      {
        return default(System.Configuration.ConfigurationPropertyCollection);
      }
    }

    public bool Strict
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public string TargetFramework
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public string TempDirectory
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public bool UrlLinePragmas
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }
    #endregion
  }
}
