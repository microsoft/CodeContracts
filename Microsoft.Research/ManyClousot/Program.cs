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
using System.Compiler;
using System.Compiler.Analysis;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Cci;
using Microsoft.Cci.Analysis;
using Microsoft.Research.CodeAnalysis;
using Microsoft.Research.DataStructures;

namespace ManyClousot
{
  class Program
  {
    static int done = 0;
    static TimeSpan[] times;

    static void Main(string[] args)
    {
      var start = DateTime.Now;
      var stopWatch = new Stopwatch();
      stopWatch.Start();

      var tasks = new Task<int>[args.Length];
      var cmds = new string[tasks.Length];
      times = new TimeSpan[tasks.Length];

      for (var i = 0; i < args.Length; i++)
      {
        cmds[i] = args[i];
      }

      Console.WriteLine("We start the analyses");
      for (var i = 0; i < tasks.Length; i++)
      {
        var clousotArgs = new List<string>();
        clousotArgs.Add(cmds[i]);

        Console.WriteLine("call: " + i);
        var local = i; // without it we get the wrong parameter!!!!!!!!
        tasks[i] = new Task<int>(() => CallClousotEXE(local, clousotArgs.ToArray()));
        tasks[i].Start();
      }

      Console.WriteLine("We wait");
      Task.WaitAll(tasks);

      for(var i = 0; i < tasks.Length; i++)
      {
        Console.WriteLine("Time for {0} was : {1}", i, times[i]);
      }

      Console.WriteLine("we are done (in {0})! Press a key", stopWatch.Elapsed);
      Console.ReadKey();

    }

    static string pathNET = "c:\\Windows\\Microsoft.NET\\Framework\\v4.0.30319\\";
    static string path = "d:\\tmp\\ref1\\";
    static string cmd = "-arrays -arithmetic -enum -nonnull -bounds  -suggest requires   -sortwarns=false  -stats slowmethods -scores -warninglevel=full";
//    static string cmdFull = "-sortwarns=false";
    static string[] assemblies = new string[] { "system.data.dll", "mscorlib.dll", "system.dll", "system.core.dll" };
    static string PathEXE = "C:\\Program Files (x86)\\Microsoft\\Contracts\\Bin\\cccheck.exe";

    static void MainOld(string[] args)
    {
      var start = DateTime.Now;
      var tasks = new Task<int>[assemblies.Length];
      var threads = new Thread[tasks.Length];
      var cmds = new string[tasks.Length];
      for (var i = 0; i < assemblies.Length; i++)
      {
        var p = i % 2 == 0 ? path : pathNET;

        cmds[i] = cmd + " " + p + "" + assemblies[i];
      }

      Console.WriteLine("We start the analyses");
#if true
      for (var i = 0; i < tasks.Length; i++)
      {
        var clousotArgs = cmds[i].Split(' ');
        tasks[i] = new Task<int>(() => CallClousotEXE(i, clousotArgs));
        tasks[i].Start();

      }

      Console.WriteLine("We wait");
      Task.WaitAll(tasks);
#else
      for (var i = 0; i < threads.Length; i++)
      {
        var clousotArgs = cmds[i].Split(' ');
        threads[i] = new Thread(new ThreadStart(() => CallClousot2(clousotArgs)));
        threads[i].Start();
      }

      Console.WriteLine("We wait");

      // Stupid way of waiting
      for (var i = 0; i < threads.Length; i++)
      {
        threads[i].Join();
      }

#endif
      Console.WriteLine("we are done (in {0})! Press a key", DateTime.Now - start);
      Console.ReadKey();

    }

    static int CallClousot2(string[] args)
    {
      var cciilProvider = CciILCodeProvider.CreateCodeProvider();
      var metadataDecoder = cciilProvider.MetaDataDecoder;
      var contractDecoder = cciilProvider.ContractDecoder;
      var assemblyCache = new System.Collections.Hashtable();

      var textWriter = Console.Out;
      var outputFactory = new FullTextWriterOutputFactory<MethodReferenceAdaptor, IAssemblyReference>(textWriter, "ServiceOutput");

      try
      {
        // We now call Clousot
        int returnCode = Clousot.ClousotMain(args,
          metadataDecoder,
          contractDecoder,
          assemblyCache,
          outputFactory: outputFactory,
          cacheAccessorFactories: null
        );

        return returnCode;
      }
      catch (ExitRequestedException e)
      {
        return e.ExitCode;
      }
      catch (Exception e)
      {
        // We need to catch all exception, otherwise the service is turned into an faulted state and remains unusable
        textWriter.WriteLine("[{0}]: Many Clousot: caught exception: {1}", DateTime.Now, e);
        if (e.InnerException != null)
        {
          textWriter.WriteLine("Inner exception: {0}", e.InnerException);
        }
        return -4444;
      }
      finally
      {
        Console.WriteLine("Analysis done".PrefixWithCurrentTime());
        textWriter.Close(); // make sure all pending output are flushed
        cciilProvider.Dispose(); // make sure all open files are closed
      }
    }

    static int CallClousot1(string[] args)
    {
      var assemblyCache = new System.Collections.Hashtable();

      // We handle the platform option here as we can't do it from within Clousot
      // as it is very CCI specific.
      //
      //SetTargetPlatform(args, assemblyCache);

      try
      {
        var textWriter = Console.Out;
        var outputFactory = new FullTextWriterOutputFactory<Method, AssemblyNode>(textWriter, "ServiceOutput");

        int returnCode = Clousot.ClousotMain(args,
          CCIMDDecoder.Value,
          CCIContractDecoder.Value,
          assemblyCache,
          outputFactory,
          null
          );

        return returnCode;
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

    static int CallClousotEXE(int index, string[] args)
    {
      Console.WriteLine(" <<< " + index);

      var start = DateTime.Now;
      var startInfo = new ProcessStartInfo();
      startInfo.CreateNoWindow = false;
      startInfo.UseShellExecute = false;
      startInfo.FileName = PathEXE;
      startInfo.WindowStyle = ProcessWindowStyle.Hidden;
      startInfo.Arguments = string.Join(" ", args);

      try
      {
        // Start the process with the info we specified.
        // Call WaitForExit and then the using statement will close.
        using (var exeProcess = Process.Start(startInfo))
        {
          exeProcess.WaitForExit();
          return exeProcess.ExitCode;
        }
      }
      catch
      {
        return -1;
      }
      finally
      {
        done++;
        Console.WriteLine(" >>> " + index);
        times[index] = DateTime.Now - start;
        Console.Title = string.Format("Done {0} analyses", done);
      }
    }

    private static void DisposeAssemblies(System.Collections.IEnumerable assemblies)
    {
      try
      {
        foreach (System.Compiler.Module assembly in assemblies)
        {
          assembly.Dispose();
        }
      }
      catch { }
    }
  }
}