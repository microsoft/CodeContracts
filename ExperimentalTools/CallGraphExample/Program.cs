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
using System.IO;
using System.Collections.Generic;
using Microsoft.Research.CodeAnalysis;
using Microsoft.Research.DataStructures;

namespace Microsoft.Research.CodeAnalysis {


  public static class CallGraphExample {

    /// <summary>
    /// Selects the code provider and then calls the code provider independent code
    /// </summary>
    public static void Main(string[] args)
    {
      System.Compiler.SystemTypes.Initialize(false, true);

      CallGraphMain(args,
        System.Compiler.Analysis.CCIMDDecoder.Value,
        System.Compiler.Analysis.CCIContractDecoder.Value);

    }

    /// <summary>
    /// CodeProvider independent code
    /// </summary>
    public static void CallGraphMain<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly>(
      string[] args,
      IDecodeMetaData<Local,Parameter,Method,Field,Property,Type,Attribute,Assembly> mdDecoder,
      IDecodeContracts<Local, Parameter, Method, Field, Type> contractDecoder
    )
    {
      new TypeBinder<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly>(args, mdDecoder, contractDecoder).Analyze();
    }

    class TypeBinder<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly> {

      #region Private state during entire analyses, mostly for convenience
      GeneralOptions options;
      System.Collections.Hashtable assemblyCache = new System.Collections.Hashtable();
      CallGraph<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly> callgraph;
      IDecodeMetaData<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly> mdDecoder;

      #endregion

      public TypeBinder(
        string[] args,
        IDecodeMetaData<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly> mdDecoder,
        IDecodeContracts<Local, Parameter, Method, Field, Type> contractDecoder
      )
      {
        this.mdDecoder = mdDecoder;
        options = GeneralOptions.ParseCommandLineArguments(args);

        if (options.HasErrors)
        {
          GeneralOptions.PrintUsageAndExit();
        }
      }

      public void Analyze()
      {
        Console.WriteLine("CallGraphAnalyzer");

        TimeCounter OverallAnalysisTime = new TimeCounter();
        OverallAnalysisTime.Start();

        Set<string> beingAnalyzed = new Set<string>();
        foreach (string assembly in options.Assemblies)
        {
          beingAnalyzed.Add(Path.GetFileNameWithoutExtension(assembly));
        }
        callgraph = new CallGraph<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly>(this.mdDecoder, beingAnalyzed);

        foreach (string assembly in options.Assemblies)
        {
          AnalyzeAssembly(assembly);
        }

        callgraph.EmitDotFile(options.OutputFile);

        OverallAnalysisTime.Stop();

        Console.WriteLine("Total time {0}.", OverallAnalysisTime.ToString());
      }


      private void AnalyzeAssembly(string assemblyName)
      {
        Assembly assembly;
        if (this.mdDecoder.TryLoadAssembly(assemblyName, assemblyCache, null, out assembly, true)) {
          foreach (Type t in mdDecoder.GetTypes(assembly)) {
            callgraph.AddType(t);
          }
        }
        else
        {
          Console.WriteLine("Cannot load assembly {0}", assemblyName);
        }
      }
    }

    private class ExitTraceListener : System.Diagnostics.TraceListener {
      public override void Write(string s)
      {
        Console.Write("Debug assert failed: {0}", s);
        Environment.Exit(-1);
      }

      public override void WriteLine(string message)
      {
        Console.WriteLine("Debug assert failed: {0}", message);
        Environment.Exit(-1);
      }
    }
  }
}
