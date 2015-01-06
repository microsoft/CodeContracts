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

// File System.CodeDom.Compiler.CodeCompiler.cs
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


namespace System.CodeDom.Compiler
{
  abstract public partial class CodeCompiler : CodeGenerator, ICodeCompiler
  {
    #region Methods and constructors
    protected abstract string CmdArgsFromParameters(CompilerParameters options);

    protected CodeCompiler()
    {
    }

    protected virtual new CompilerResults FromDom(CompilerParameters options, System.CodeDom.CodeCompileUnit e)
    {
      return default(CompilerResults);
    }

    protected virtual new CompilerResults FromDomBatch(CompilerParameters options, System.CodeDom.CodeCompileUnit[] ea)
    {
      return default(CompilerResults);
    }

    protected virtual new CompilerResults FromFile(CompilerParameters options, string fileName)
    {
      return default(CompilerResults);
    }

    protected virtual new CompilerResults FromFileBatch(CompilerParameters options, string[] fileNames)
    {
      return default(CompilerResults);
    }

    protected virtual new CompilerResults FromSource(CompilerParameters options, string source)
    {
      return default(CompilerResults);
    }

    protected virtual new CompilerResults FromSourceBatch(CompilerParameters options, string[] sources)
    {
      return default(CompilerResults);
    }

    protected virtual new string GetResponseFileCmdArgs(CompilerParameters options, string cmdArgs)
    {
      Contract.Requires(options != null);

      return default(string);
    }

    protected static string JoinStringArray(string[] sa, string separator)
    {
      return default(string);
    }

    protected abstract void ProcessCompilerOutputLine(CompilerResults results, string line);

    CompilerResults System.CodeDom.Compiler.ICodeCompiler.CompileAssemblyFromDom(CompilerParameters options, System.CodeDom.CodeCompileUnit e)
    {
      return default(CompilerResults);
    }

    CompilerResults System.CodeDom.Compiler.ICodeCompiler.CompileAssemblyFromDomBatch(CompilerParameters options, System.CodeDom.CodeCompileUnit[] ea)
    {
      return default(CompilerResults);
    }

    CompilerResults System.CodeDom.Compiler.ICodeCompiler.CompileAssemblyFromFile(CompilerParameters options, string fileName)
    {
      return default(CompilerResults);
    }

    CompilerResults System.CodeDom.Compiler.ICodeCompiler.CompileAssemblyFromFileBatch(CompilerParameters options, string[] fileNames)
    {
      return default(CompilerResults);
    }

    CompilerResults System.CodeDom.Compiler.ICodeCompiler.CompileAssemblyFromSource(CompilerParameters options, string source)
    {
      return default(CompilerResults);
    }

    CompilerResults System.CodeDom.Compiler.ICodeCompiler.CompileAssemblyFromSourceBatch(CompilerParameters options, string[] sources)
    {
      return default(CompilerResults);
    }
    #endregion

    #region Properties and indexers
    protected abstract string CompilerName
    {
      get;
    }

    protected abstract string FileExtension
    {
      get;
    }
    #endregion
  }
}
