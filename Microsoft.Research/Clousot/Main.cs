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

#undef USECLOUDOT

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

    const int CloudotAnalysesRetry = 3;

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

#if USECLOUDOT
      if (OptionsHelper.UseCloudot(args))
      {
        // Just an horrible shortcut to invoke the debugger
        if (args.Contains("-break"))
        {
          System.Diagnostics.Debugger.Launch();
        }

        for (var i = 0; i < CloudotAnalysesRetry; i++)
        {
          string[] clousotArgs = null;
          try
          {
            return ClousotViaService.Main(args, out clousotArgs);
          }
          catch (Exception e)
          {
            Console.WriteLine("Connection to Cloudot terminated with an exception".PrefixWithCurrentTime());
            Console.WriteLine("Exception details: {0}", e.ToString());

            if(i == CloudotAnalysesRetry -1 )
            {
              Console.WriteLine("We gave up with the service. Reverting to local execution");
            }
            else
            {
              Console.WriteLine("Try to use the Cloudot again (remaining {0})", CloudotAnalysesRetry - (i-1));
            }

            args = clousotArgs; // We use the service specific options in the case we revert to local execution
          }
        }
      }
#endif
      var assemblyCache = new Hashtable();
      try
      {
#if DEBUG
#if USECLOUDOT
        if (args != null)
#endif
        {
          args = args.Where(s => s != StringConstants.DoNotUseCloudot).ToArray();
        }
#endif
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
