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
using System.Collections.Generic;
using System.Compiler.Analysis;
using System.Diagnostics.Contracts;
using System.IO;
using Microsoft.Research.DataStructures;
using System.Linq;
using System.Collections;
using Microsoft.Research.ClousotPulse.Messages;
using System.IO.Pipes;

namespace Microsoft.Research.CodeAnalysis 
{
  public class CCI1Driver 
  {
    public static int Main(string[] args)
    {
      return Main(args, null, null);
    }

    public static int Main(string[] args, ISimpleLineWriter lineWriter)
    {
      Contract.Requires(args != null);

      var textWriter = lineWriter.AsTextWriter();
      var outputFactory = new FullTextWriterOutputFactory<System.Compiler.Method, System.Compiler.AssemblyNode>(textWriter);

      return Main(args, outputFactory, null);
    }

    public static int Main(string[] args, IOutputFullResultsFactory<System.Compiler.Method, System.Compiler.AssemblyNode> outputFactory, IEnumerable<Caching.IClousotCacheFactory> cacheAccessorFactories)
    {
      Contract.Requires(args != null);

      var assemblyCache = new Hashtable();
      try
      {
        return Clousot.ClousotMain(args, CCIMDDecoder.Value, CCIContractDecoder.Value, assemblyCache, outputFactory, cacheAccessorFactories);
      }
      catch (ExitRequestedException e)
      {
        return e.ExitCode;
      }
      finally
      {
        DisposeAssemblies(assemblyCache.Values);
      }
    }

    private static void DisposeAssemblies(System.Collections.IEnumerable assemblies)
    {
      Contract.Requires(assemblies != null);
      try
      {
        foreach (System.Compiler.Module assembly in assemblies)
        {
          Contract.Assume(assembly != null);
          assembly.Dispose();
        }
      }
      catch { }
    }

  }
}
