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
using System.IO;
using System.Linq;
using Microsoft.Research.CodeAnalysis;
using Microsoft.Research.DataStructures;
using System.Diagnostics.Contracts;

namespace Microsoft.Research.CodeAnalysis
{
  public class NewCCI2Driver
  {
    public static int Main(string[] args)
    {
      return Main(args, Console.Out.AsSimpleLineWriter());
    }

    // Entry point
    public static int Main(string[] args, ISimpleLineWriter output)
    {
      var start = DateTime.Now;
      var slicingOptions = new SlicingOptions(args);

      // we use it to parse the clousot options, and to split the work
      var clousot = new NewCCI2Driver(slicingOptions.remainingArgs.ToArray(), output);

      if (!clousot.CheckOptions())
      {
        return -1;
      }

      var errorCode = 0;
      WorkerPool workerPool = null;
      IQueue queue = null;
      IWorkerFactory localWorkerFactory = null;
      List<IWorkerFactory> remoteWorkerFactories = null;
      Func<SliceDefinition, ISliceWorkResult> workOnSlice = null;
      Dictionary<ISliceId, int> failedRegressions = null;

      var localWorkers = slicingOptions.LocalWorkers;
      var remoteWorkers = slicingOptions.RemoteWorkers;
      var workers = localWorkers + remoteWorkers;


      // If we have workers, we create a Worker factory
      if (workers > 0)
      {
        IDB db;
        if (remoteWorkers > 0 || clousot.options.useCache)
        {
          // Use Clousot Database 
          db = new StdDB(clousot.options);
        }
        else
        {
          // In-memory database
          db = new MemorySingletonDB();
        }

        workerPool = new WorkerPool("Worker", canCancel: false);
        queue = new FIFOQueue(workerPool, db);

        // When a job is done, it choses which jobs to put in the queue (e.g., analyze the dependencies)
        var scheduler = 
          clousot.options.InferObjectInvariantsOnlyForReadonlyFields? 
          new LazySchedulerForObjectInvariants(queue, db) : // use LazySchedulerForObjectInvariants for global fixpoint computation including object invariants
          new LazyScheduler(queue, db); // use LazyScheduler for the global fixpoint computation

        //var scheduler = new NoloopScheduler(queue);
        
        // Usual cache
        var clousotDB = slicingOptions.useDB ? db : null;

        var argsForWorker = clousot.argsForWorker.ToFList();

        if (localWorkers > 0)
        {
          if (slicingOptions.cci1)
            localWorkerFactory = new Clousot1WorkerFactory(scheduler, argsForWorker, output, clousotDB);
          else
            localWorkerFactory = new Clousot2WorkerFactory(scheduler, argsForWorker, output, clousotDB);
          // TODO: use a lighter version of ClousotMain since options are already checked here
        }
        if (remoteWorkers > 0) // so far 1 factory per remote worker but we can do better
        {
          // TODO: specifiy, for each address the number of workers

          // We have a list, because we can have several addresses
          remoteWorkerFactories = slicingOptions.serviceAddress.Select(addr => new Clousot2SWorkerFactory(scheduler, argsForWorker, output, addr)).ToList<IWorkerFactory>();
        }

        if (clousot.options.IsRegression)
        {
           failedRegressions = new Dictionary<ISliceId, int>();
        }

        // fail if any work fails
        scheduler.OnWorkDone += (sliceId, returnCode) =>
          {
            if (errorCode >= 0)
            {
              if (returnCode < 0) // special error code, keep only one
              {
                errorCode = returnCode;
              }
              else
              {
                int prevValue;
                if (clousot.options.IsRegression)
                {
                  lock (failedRegressions)
                  {
                    Contract.Assume(failedRegressions != null);
                    if (failedRegressions.TryGetValue(sliceId, out prevValue))
                    {
                      output.WriteLine("[Regression] We already analyzed {0} with outcome {1}. Now we update the outcome to {2}", sliceId.Dll, prevValue, returnCode);
                    }
                    failedRegressions[sliceId] = returnCode;
                  }
                }
                errorCode += returnCode; // regression error count, additive
              }
            }
          };

        // What we do for each slice. Two things:
        // 1. Register the slice in the db
        // 2. Add to the queue (via the scheduler, who decides how to do it)
        workOnSlice = sliceDef =>
        {
          var sliceId = db.RegisterSlice(sliceDef);
          scheduler.FeedQueue(new ISliceId[] { sliceId });
          return null;
        };
      }

      ISlicerResult slicerResult = null;

      if (slicingOptions.sliceFirst)
      {
        slicerResult = clousot.SplitWork(workOnSlice);
        output.WriteLine("Slicing time: {0}", DateTime.Now - start);
      }

      if (workerPool != null)
      {
        if (localWorkerFactory != null)
          for (var i = 0; i < localWorkers; i++)
            workerPool.CreateWorker(localWorkerFactory);
        if (remoteWorkerFactories != null)
          foreach (var factory in remoteWorkerFactories)
            workerPool.CreateWorker(factory);
      }

      if (!slicingOptions.sliceFirst)
      {
        slicerResult = clousot.SplitWork(workOnSlice);
        output.WriteLine("Slicing time and thread creation time : {0}", DateTime.Now - start);
      }

      if (workerPool != null)
      {
        // workerPool != null ==> queue != null
        Contract.Assume(queue != null);
        workerPool.WaitAllAnd(queue.EmptyQueueWaitHandle);
        // Something else can arrive at the queue, so we want to stop all of them
        workerPool.StopAll();
      }

      if (slicerResult != null)
      {
        var errors = slicerResult.GetErrors();
        if (errors.Any())
        {
          foreach (var errMessage in errors)
            output.WriteLine(errMessage);
          errorCode = errors.Count();
        }
      }

      output.WriteLine("Total analysis time: {0}", DateTime.Now - start);

      var returnValue = errorCode;

      if (failedRegressions != null && clousot.options.IsRegression && errorCode >= 0)
      {
        returnValue = failedRegressions.Where(pair => pair.Value != 0).Select(pair => Math.Abs(pair.Value)).Sum();
      }

#if DEBUG
      if (clousot.options.IsRegression)
      {
        Console.WriteLine("[Regression] Returned value {0}", returnValue);
      }
#endif      
      return returnValue;
    }

    private readonly Dictionary<string, IMethodAnalysis> methodAnalyzers = Analyzers.CreateMethodAnalyzers();
    private readonly Dictionary<string, IClassAnalysis> classAnalyzers = Analyzers.CreateClassAnalyzers();
    private readonly GeneralOptions options;
    private readonly ISimpleLineWriter output;
    private readonly string[] argsForWorker;

    private NewCCI2Driver(string[] args, ISimpleLineWriter output)
    {
      /* TODO: argsForWorker
       * We currently keep all the arguments except the general arguments
       * But we should also remove arguments like -select, -namespaceSelect, -typenameSelect, -memberNameSelect -analyzeFrom, -analyzeTo (or translate them)
       */

      this.options = FilteringGeneralOptions.ParseCommandLineArguments(args, this.methodAnalyzers, this.classAnalyzers, out this.argsForWorker);

      this.output = output;
    }

    private bool CheckOptions()
    {
      // TODO check options

      if (this.options.HelpRequested)
      {
        this.options.PrintUsage(this.output.AsTextWriter());
        return false;
      }

      if (this.options.HasErrors)
      {
        this.options.PrintErrors(this.output.AsTextWriter());
        return false;
      }

      if (this.options.repro)
        this.WriteReproFile();

      if (this.options.Analyses.Count == 0)
        this.output.WriteLine("Warning: No analyses specified.");

      if (!this.CheckArrayAnalysisDependences())
        return false;

      return true;
    }

    private ISlicerResult SplitWork(Func<SliceDefinition, ISliceWorkResult> workOnSlice)
    {
      // TODO: split in different slicers

      // TODO: here we are suppose to send .dll, .pdb files too?
      using (var slicer = new ClousotSlicer2(this.options, this.output, this.methodAnalyzers, this.classAnalyzers))
      {
        return slicer.DoWork(workOnSlice);
      }
    }

    private void WriteReproFile()
    {
      try
      {
        var file = new StreamWriter("repro.bat");
        file.WriteLine("{0} %1 %2 %3 %4 %5 %6 %7 %8 %9", Environment.CommandLine.Replace("-repro", ""));
        file.Close();
      }
      catch { }
    }

    private bool CheckArrayAnalysisDependences()
    {
      IMethodAnalysis boundsAnalysis;
      IMethodAnalysis arrayAnalysis;

      var bounds = this.methodAnalyzers.TryGetValue("bounds", out boundsAnalysis);
      var arrays = this.methodAnalyzers.TryGetValue("arrays", out arrayAnalysis);

      if (!arrays)
        return true;

      if (bounds && (!this.options.Analyses.Contains(arrayAnalysis) || this.options.Analyses.Contains(boundsAnalysis)))
        return true;

      this.output.WriteLine("Warning: -bounds is needed when using the -arrays option");

      return false;
    }

    /// <summary>
    /// Get the options for the slicer and Cloudot.
    /// Remember the ones that it does not understand
    /// </summary>
    class SlicingOptions : OptionParsing
    {
      public SlicingOptions(string[] args)
      {
        this.Parse(args);
      }

      protected override bool TreatGeneralArgumentsAsUnknown { get { return true; } }
      protected override bool TreatHelpArgumentAsUnknown { get { return true; } }

      public readonly List<string> remainingArgs = new List<string>();

      protected override bool ParseUnknown(string option, string[] args, ref int index, string optionEqualsArgument)
      {
        this.remainingArgs.Add(args[index]);

        return true;
      }

      [OptionDescription("Number of local workers")]
      public int workers = -1;

      [OptionDescription("Wait the end of slicing before running the workers")]
      public bool sliceFirst = false;

      [OptionDescription("Make Clousot use the common database")]
      public bool useDB = true;

      [OptionDescription("Make Clousot analyzers use CCI1 instead of CCI2")]
      public bool cci1 = false;

      [OptionDescription("Service addresses of remote workers")]
      public List<string> serviceAddress = new List<string>();

      public int RemoteWorkers { get { return this.serviceAddress.Count; } }
      public int LocalWorkers
      {
        get
        {
          if (this.workers >= 0) return this.workers;
          if (this.RemoteWorkers > 0) return 0;
          return Environment.ProcessorCount;
        }
      }
    }

    class FilteringGeneralOptions : GeneralOptions
    {
      public static GeneralOptions ParseCommandLineArguments(string[] args, Dictionary<string, IMethodAnalysis> analyzers, Dictionary<string, IClassAnalysis> classanalyzers, out string[] nonGeneralArguments)
      {
        var options = new FilteringGeneralOptions(args, analyzers, classanalyzers);
        nonGeneralArguments = options.GetNonGeneralArguments();
        return options;
      }

      private readonly string[] originalArgs;
      private readonly Set<int> generalArgIndexes = new Set<int>();

      private FilteringGeneralOptions(string[] originalArgs, Dictionary<string, IMethodAnalysis> analyzers, Dictionary<string, IClassAnalysis> classanalyzers) :
        base(analyzers, classanalyzers)
      {
        this.originalArgs = originalArgs;
        this.Parse(originalArgs);
      }

      protected override bool ParseGeneralArgument(string arg, string[] args, ref int index)
      {
        if (Object.ReferenceEquals(args, this.originalArgs)) // otherwise, it means we are parsing a macro argument (TODO?)
          this.generalArgIndexes.Add(index);

        return base.ParseGeneralArgument(arg, args, ref index); // index should not be updated
      }

      public string[] GetNonGeneralArguments()
      {
        return this.originalArgs.Where((_, index) => !this.generalArgIndexes.Contains(index)).ToArray();
      }
    }
  }
}
