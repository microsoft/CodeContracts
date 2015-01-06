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
using System.Diagnostics.Contracts;
using Microsoft.Cci;
using Microsoft.Cci.Analysis;
using Microsoft.Research.DataStructures;

namespace Microsoft.Research.CodeAnalysis
{
  public class CCI2Driver
  {

    /// <summary>
    /// Entry point for Clousot
    /// </summary>
    public static int Main(string[] args)
    {
      return Main(args, null, null);
    }

    public static int Main(string[] args, ISimpleLineWriter lineWriter)
    {
      Contract.Requires(args != null);

      var textWriter = lineWriter.AsTextWriter();
      var outputFactory = new FullTextWriterOutputFactory<MethodReferenceAdaptor, IAssemblyReference>(textWriter);

      return Main(args, outputFactory, null);
    }

    public static int Main(string[] args, IOutputFullResultsFactory<MethodReferenceAdaptor, IAssemblyReference> outputFactory, IEnumerable<Caching.IClousotCacheFactory> cacheAccessorFactories)
    {
      Contract.Requires(args != null);

      if (args.Length > 0 && args[0] == "-cloudot")
      {
        try
        {
          return ClousotViaService.Main(args);
        }
        catch (Exception)
        {
          Console.WriteLine("Process terminated with an exception".PrefixWithCurrentTime());
          return -1;
        }
      }
      else
      {
        var cciilProvider = CciILCodeProvider.CreateCodeProvider();
        var metadataDecoder = cciilProvider.MetaDataDecoder;
        var contractDecoder = cciilProvider.ContractDecoder;
        var assemblyCache = new System.Collections.Hashtable();

        try
        {
          int returnCode = Clousot.ClousotMain(args,
            metadataDecoder,
            contractDecoder,
            assemblyCache,
            outputFactory: outputFactory,
            cacheAccessorFactories: cacheAccessorFactories
          );

          return returnCode; // important for regressions
        }
        catch (ExitRequestedException e)
        {
          return e.ExitCode;
        }
        finally
        {
          cciilProvider.Dispose();
        }
      }
    }
  }


}