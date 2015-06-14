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
using System.IO;
using Microsoft.Research.CodeAnalysis;
using Microsoft.Research.DataStructures;

namespace Microsoft.Research.CodeAnalysis
{
  delegate int CallLocalClousotMainFunc<Method, Assembly>(string[] args,
    IOutputFullResultsFactory<Method, Assembly> outputFactory,
    IEnumerable<Caching.IClousotCacheFactory> cacheAccessorFactories);

  delegate int CallAnyClousotMainFunc(string[] args, TextWriter tw);

  // Create the workers
  // A worker is roughly a thread. If it is run locally, then it is just a thread. If it is run remotely, then it just invokes the remote service
  abstract class ClousotWorkerFactory<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> : IWorkerFactory
    where Type : IEquatable<Type>
    where Method : IEquatable<Method>
  {
    protected readonly IScheduler scheduler;
    protected readonly FList<string> args;

    protected ClousotWorkerFactory(IScheduler scheduler, FList<string> args)
    {
      this.scheduler = scheduler;
      this.args = args;
    }

    public abstract Worker NewWorker(IWorkerId workerId);

    protected abstract class BaseClousotWorker : Worker
    {
      private readonly FList<string> baseArgs;

      public BaseClousotWorker(IWorkerId id, FList<string> baseArgs, IScheduler scheduler)
        : base(id, scheduler)
      {
        Contract.Requires(id != null);

        this.baseArgs = baseArgs;
      }

      protected virtual FList<string> MakeArgs(IWorkId work)
      {
        Contract.Requires(work != null);

        var args = this.baseArgs;

        args = args.Cons(work.SliceId.Dll); // the dll to analyze

        args = args.Cons("-sliceTime:" + work.Time.ToBinary()); // The sliceTime is the "universal" time -- to see: can we replace it with a job id?

        return args;
      }
    }
  }

  abstract class LocalClousotWorkerFactory<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> : ClousotWorkerFactory<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>
    where Type : IEquatable<Type>
    where Method : IEquatable<Method>
  {
    private readonly ISimpleLineWriter output;
    private readonly IDB db;

    public LocalClousotWorkerFactory(IScheduler scheduler, FList<string> args, ISimpleLineWriter output, IDB db)
      : base(scheduler, args)
    {
      this.db = db;
      this.output = output;
    }

    public override Worker NewWorker(IWorkerId workerId)
    {
      return new ClousotWorker(this.CallClousotMain, workerId, this.args, this.scheduler, output, this.db);
    }

    protected abstract int CallClousotMain(string[] args,
      IOutputFullResultsFactory<Method, Assembly> outputFactory,
      IEnumerable<Caching.IClousotCacheFactory> cacheAccessorFactories);


    class ClousotWorker : BaseClousotWorker
    {
      private readonly CallLocalClousotMainFunc<Method, Assembly> callClousotMain;
      private readonly ISimpleLineWriter output;
      private readonly IDB db;

      public ClousotWorker(CallLocalClousotMainFunc<Method, Assembly> callClousotMain, IWorkerId id, FList<string> baseArgs, IScheduler scheduler, ISimpleLineWriter output, IDB db)
        : base(id, baseArgs, scheduler)
      {
        Contract.Requires(id != null);


        this.callClousotMain = callClousotMain;
        this.output = output;
        this.db = db;
      }

      protected override FList<string> MakeArgs(IWorkId work)
      {
        var args = base.MakeArgs(work);

        if (this.db != null)
          args = args.Cons("-cache");

        return args;
      }

      protected override int Do(IWorkId work)
      {
        var args = this.MakeArgs(work).ToArray();

        var bufferedOutput = BufferedTextWriter.Create();
        try
        {
          var outputFactory = new FullTextWriterOutputFactory<Method, Assembly>(bufferedOutput);

          this.output.WriteLine("[SlicerWorker: {0}] Analysis of dll (Time stamp {1}) {2}", DateTime.Now, work.Time, work.SliceId.Dll);

          return this.callClousotMain(args, outputFactory, this.db.AsEnumerable());
        }
        finally
        {
          this.output.WriteLine(bufferedOutput.ToString());
        }
      }
    }
  }

  abstract class RemoteClousotWorkerFactory<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> : ClousotWorkerFactory<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>
    where Type : IEquatable<Type>
    where Method : IEquatable<Method>
  {
    private readonly ISimpleLineWriter output;

    public RemoteClousotWorkerFactory(IScheduler scheduler, FList<string> args, ISimpleLineWriter output)
      : base(scheduler, args)
    {
      this.output = output;
    }

    public override Worker NewWorker(IWorkerId workerId)
    {
      return new ClousotWorker(this.CallClousotMain, workerId, this.args, this.scheduler, this.output);
    }

    protected abstract int CallClousotMain(string[] args, TextWriter tw);

    // It cannot have a DB factory, as we cannot pass this object via the WCF (just too complex and not interesting, better to use the global, shared DB)
    class ClousotWorker : BaseClousotWorker
    {
      private readonly CallAnyClousotMainFunc callClousotMain;
      private readonly ISimpleLineWriter output;

      public ClousotWorker(CallAnyClousotMainFunc callClousotMain, IWorkerId id, FList<string> baseArgs, IScheduler scheduler, ISimpleLineWriter output)
        : base(id, baseArgs, scheduler)
      {
        Contract.Requires(id != null);

        this.callClousotMain = callClousotMain;
        this.output = output;
      }

      protected override int Do(IWorkId work)
      {
        var args = this.MakeArgs(work).ToArray();

        var bufferedOutput = BufferedTextWriter.Create();
        try
        {
          bufferedOutput.WriteLine("Analysis of dll: {0}", work.SliceId.Dll);

          return this.callClousotMain(args, bufferedOutput);
        }
        finally
        {
          this.output.WriteLine(bufferedOutput.ToString());
        }
      }
    }
  }
}
