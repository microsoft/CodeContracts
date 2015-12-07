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

using Microsoft.Research.DataStructures;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Research.CodeAnalysis
{
  class Parallel<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>
  {

    [ContractInvariantMethod]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Required for code contracts.")]
    private void ObjectInvariant()
    {
      Contract.Invariant(this.metadataDecoder != null);
      Contract.Invariant(this.methodNumbers != null);
    }

    private readonly IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> metadataDecoder;
    private readonly MethodNumbers<Method, Type> methodNumbers;
    private readonly int bucketSize;

    public Parallel(IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> md, MethodNumbers<Method, Type> methodNumbers, int bucketSize)
    {
      Contract.Requires(md != null);
      Contract.Requires(methodNumbers != null);

      this.metadataDecoder = md;
      this.methodNumbers = methodNumbers;
      this.bucketSize = ComputeBucketSize(bucketSize, this.methodNumbers.Count);
    }

    /// <summary>
    /// Buckets output
    /// </summary>
    public void RunWithBuckets(string[] args)
    {
      Contract.Requires(args != null);

      args = RemoveArgs(args); // To make cccheck.exe happy

      var watch = new CustomStopwatch();
      watch.Start();

      var executor = new BucketExecutor();
//      var id = 0;

      var temp = new ConcurrentBag<IEnumerable<int>>();

//      foreach(var bucket in this.GenerateDisjointBuckets())
      Parallel.ForEach(
        this.GenerateDisjointBuckets(),
      bucket =>
      {
        //PrintBucket(watch, bucket);

        temp.Add(bucket);
        // executor.AddJob(() => RunOneAnalysis(id++, args, ToSelectArgument(bucket)));      
      });

      var array = temp.ToArray();

      Console.WriteLine("Eventually we inferred {0} buckets", array.Count());
      foreach(var b in array)
      {
        PrintBucket(watch, b);
      }

      //executor.DoAllTheRemainingJobs();

      //executor.WaitForStillActiveJobs();
    }

    // TODO: this method is an hack, make it better using reflection, or probably pushing all to the GeneralOptions object
    public static string[] RemoveArgs(string[] args)
    {
      Contract.Requires(args != null);
      Contract.Ensures(Contract.Result<string[]>() != null);

      var result = new List<string>(args.Length);

      for (var i = 0; i < args.Length; i++)
      {
        var arg = args[i];
        if (arg.Length > 0)
        {
          var option = arg.Substring(1).ToLower();

          switch (option)
          {
            case "splitanalysis":
              continue;

            case "bucketsize":
              i++;
              continue;
          }
          // Case "bucketsize=3" or case "splitanalysis=true":
          if (option.StartsWith("bucketsize") || option.StartsWith("splitanalysis"))
          {
            continue;
          }

          result.Add(arg);
        }
      }
      return result.ToArray();
    }

    public IEnumerable<IEnumerable<int>> GenerateBucketsBasedOnVisibility()
    {
      var bucketsCounts = new int[this.methodNumbers.Count];
      var methodsInABucket = new List<Method>();

      foreach (var m in this.methodNumbers.OrderedMethods())
      {
        if (metadataDecoder.IsVisibleOutsideAssembly(m)&& metadataDecoder.HasBody(m))
        {
          var currentBucket = new Set<Method>();
          var currentNumbers = new List<int>();
          // Get the bucket containing the cone for this method
          var bucket = methodNumbers.GetMethodBucket(m);

          // Remember we saw those methods
          methodsInABucket.AddRange(bucket);

          // Add the method number to the current numbers
          currentNumbers.Add(this.methodNumbers.GetMethodNumber(m));

          // Add the bucket to the current methods
          currentBucket.AddRange(bucket);

          // If we passed the threshold, then schedule for analysis
          if (currentBucket.Count >= this.bucketSize)
          {
            bucketsCounts[currentBucket.Count]++;

            yield return currentNumbers;
          }
        }
        else
        {
          // skip them for now...
        }
      }

      var methodsNotInABucket = this.methodNumbers.OrderedMethods().Where(m => metadataDecoder.HasBody(m)).Except(methodsInABucket);
      var count = methodsNotInABucket.Count();

      if (count > 0)
      {
        Console.WriteLine("*** There are {0} methods not in a bucket from externally visible surface", count);

        yield return methodsNotInABucket.Select(m => this.methodNumbers.GetMethodNumber(m));
      }
      else
      {
        Console.WriteLine("*** All the methods are reachable from the outside");
      }

    }

    public IEnumerable<IEnumerable<int>> GenerateDisjointBuckets()
    {

      var bucketsCounts = new int[this.methodNumbers.Count];
      var methodsInABucket = new List<Method>();

      var concurrentbuckets = new ConcurrentBag<Tuple<int, Set<int>>>();

      Console.WriteLine("Creating buckets");

      //foreach (var m in this.methodNumbers.OrderedMethods())
      Parallel.ForEach(
        this.methodNumbers.OrderedMethods().Where(m => metadataDecoder.IsVisibleOutsideAssembly(m) && metadataDecoder.HasBody(m)),
        m =>
        {
          //        if (metadataDecoder.IsVisibleOutsideAssembly(m) && metadataDecoder.HasBody(m))
          {
            var methodNumber = this.methodNumbers.GetMethodNumber(m);

            // Get the bucket containing the cone for this method
            var bucket = methodNumbers.GetMethodBucket(m);

            // Remember we saw those methods
            methodsInABucket.AddRange(bucket);

            concurrentbuckets.Add(new Tuple<int, Set<int>>(methodNumber, new Set<int>(bucket.Select(method => this.methodNumbers.GetMethodNumber(method)))));
          }
        });


      var buckets = concurrentbuckets.ToArray();
      var methodsNotInABucket = this.methodNumbers.OrderedMethods().Except(methodsInABucket);
      var methodsWithFewSubsumed = new List<int>();
      var count = methodsNotInABucket.Count();

      if (count > 0)
      {
        Console.WriteLine("*** There are {0} methods not in a bucket from externally visible surface", count);

        yield return methodsNotInABucket.Select(m => this.methodNumbers.GetMethodNumber(m));
      }
      else
      {
        Console.WriteLine("*** All the methods are reachable from the outside");
      }

      Console.WriteLine("Mergin buckets");

      var methodsAlreadyInABucket = new List<int>();
      for(var i = 0; i < buckets.Count(); i++)
      {
        var currentRepresentative = buckets[i].Item1;
        if(methodsAlreadyInABucket.Contains(currentRepresentative))
        {
          continue;
        }
        var allRepresentativesInThisIteration = new List<int>() { currentRepresentative };
        var allSubsumedMethodsInThisIteration = new Set<int>( buckets[i].Item2 );
        for (var j = i + 1; j < buckets.Count(); j++)
        {
          var candidateRepresentative = buckets[j].Item1;
          if (methodsAlreadyInABucket.Contains(candidateRepresentative))
          {
            continue;
          }

          var methodsSubsumedByTheCandidate = buckets[j].Item2;
          if (allSubsumedMethodsInThisIteration.Intersect(methodsSubsumedByTheCandidate).Any())
          {
            {
              Console.WriteLine("  Merging together the methods subsumed by {0} and {1} -- Method {1} subsumes other {2} methods", buckets[i].Item1, candidateRepresentative, buckets[j].Item2.Count);
            }
            // let's merge!
            allRepresentativesInThisIteration.Add(candidateRepresentative);
            allSubsumedMethodsInThisIteration.AddRange(methodsSubsumedByTheCandidate);
            methodsAlreadyInABucket.Add(j);
            methodsAlreadyInABucket.AddRange(methodsSubsumedByTheCandidate);
          }
        }

        if (allSubsumedMethodsInThisIteration.Count == 1)
        {
          Console.WriteLine("  The method {0} has a trivial subsumption set, adding to the todo buckets", currentRepresentative);
          methodsWithFewSubsumed.AddRange(allSubsumedMethodsInThisIteration);
          continue;
        }

        Console.WriteLine("  Top level method(s) = {0}{1}   Subsumed method = {2}", ToString(allRepresentativesInThisIteration), Environment.NewLine, ToString(allSubsumedMethodsInThisIteration));

        yield return allRepresentativesInThisIteration;
      }

      if(methodsWithFewSubsumed.Any())
      {
        Console.WriteLine("  Those are the methods with few subsumed {0}", ToString(methodsWithFewSubsumed));
        yield return methodsWithFewSubsumed;
      }
    }
    #region Private workers

    private int RunOneAnalysis(int id, string[] args, string selectedMethods)
    {
      var watch = new CustomStopwatch(); watch.Start();
      var idStr = string.Format("[{0}] ", id);
      try
      {
        Console.WriteLine(idStr + "Running with option = {0}", selectedMethods);
        var proc = SetupProcessInfo(GetExecutable(), args, selectedMethods);
        using (var exe = Process.Start(proc))
        {
          Task.Factory.StartNew(
                          () =>
                          {
                            while (!exe.HasExited)
                            {
                              var lineErr = exe.StandardError.ReadLine();
                              Console.WriteLine(idStr + lineErr);
                            }

                            var lineOut = exe.StandardError.ReadToEnd();
                            Console.Write(idStr + lineOut);
                          });

          // Reads the standard output
          string line;
          while (!exe.HasExited)
          {
            line = exe.StandardOutput.ReadLine();
            Console.WriteLine(idStr + line);
          }

          line = exe.StandardOutput.ReadToEnd();
          Console.WriteLine(idStr + line);

          return exe.ExitCode;
        }
      }
      catch
      {
        Console.WriteLine(idStr + "failed to start the process");
        return -1;
      }
      finally
      {
        Console.WriteLine(idStr + "Analysis of {0} took {1}", selectedMethods, watch.Elapsed);
        // nothing?
      }
    }

    private static string GetExecutable()
    {
      return System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;

//      return @"C:\Program Files (x86)\Microsoft\Contracts\Bin\cccheck.exe";
    }

    private string ToSelectArgument(IEnumerable<int> bucket)
    {
      var result = new StringBuilder();
      result.Append("-select ");

      foreach(var i in bucket)
      {
        result.AppendFormat("{0};",i);
      }

      return result.ToString();
    }
    static private ProcessStartInfo SetupProcessInfo(string PathEXE, string[] args, string selectedMethods)
    {
      var processInfo = new ProcessStartInfo();

      processInfo.FileName = PathEXE;
      processInfo.CreateNoWindow = false;
      processInfo.UseShellExecute = false;
      processInfo.WindowStyle = ProcessWindowStyle.Hidden;
      processInfo.RedirectStandardOutput = true;
      processInfo.RedirectStandardError = true;
      processInfo.Arguments = String.Join(" ", args) + " " + selectedMethods; 

      return processInfo;
    }

    private int ComputeBucketSize(int bucketSize, int methodsCount)
    {
      Contract.Ensures(Contract.Result<int>() >= 1);

      if (bucketSize > 0)
      {
        return bucketSize;
      }
      else
      {
        if(methodsCount > 50000)
        {
          return 1000;
        }
        if(methodsCount > 10000)
        {
          return 500;
        }

        if(methodsCount > 1000)
        {
          return 50;
        }
//        else
          return 10;
      }
    }


    #endregion

    #region Print stuff


    private void PrintBucket(Stopwatch watch, IEnumerable<int> bucket)
    {
      var count = bucket.Count();
    //  var i = 0;
      Console.WriteLine("  There are {0} top-level methods in this bucket ({1} to get it)", count, watch.Elapsed);
      /*
      foreach(var b in bucket)
      {
        Console.Write("{0}{1} ", b, i != count - 1 ? ";" : "");
        i++;
      }
      Console.WriteLine();
       */ 
      watch.Restart();
    }

    private string ToString(IEnumerable<int> what)
    {
      var str = new StringBuilder();
      foreach(var i in what)
      {
        str.AppendFormat("{0} ", i);
      }

      return str.ToString();
    }

    #endregion


  }


  public class BucketExecutor
  {
    private static int Threshold()
    {
      return Math.Max(4, System.Environment.ProcessorCount/2);
    }

    Set<Task> tasks;
    readonly Queue<Action> todo;

    public BucketExecutor()
    {
      this.tasks = new Set<Task>();
      this.todo = new Queue<Action>();
    }

    public void AddJob(Action job)
    {
      MakeRoom();
      this.todo.Enqueue(job);

      StartTheAnalysisOfAsManyJobsAsPossible();
    }

    public void DoAllTheRemainingJobs()
    {
      do
      {
        MakeRoom();
        StartTheAnalysisOfAsManyJobsAsPossible();
      } while (todo.Count != 0);
    }

    private void StartTheAnalysisOfAsManyJobsAsPossible()
    {
      while (todo.Count != 0 && tasks.Count <= Threshold())
      {
        var nextJob = todo.Dequeue();
        var task = Task.Factory.StartNew(nextJob);

        this.tasks.Add(task);
      }
    }


    private void MakeRoom()
    {
      if (tasks.Count > 0)
      {
        var cleanedUp = tasks.Where(t => !t.IsCompleted);
        this.tasks = new Set<Task>(cleanedUp);
      }
    }

    internal void WaitForStillActiveJobs()
    {
      Console.WriteLine("Waiting for all the active tasks to finish");
      Task.WaitAll(this.tasks.ToArray());
      Console.WriteLine("Done waiting");
    }
  }
}
