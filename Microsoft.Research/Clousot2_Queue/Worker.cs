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

using System.Threading;
using System;
using System.Diagnostics.Contracts;

namespace Microsoft.Research.CodeAnalysis
{
  interface IWorkerFactory
  {
    Worker NewWorker(IWorkerId workerId);
  }

  interface IWorkerId
  {

  }

  class WorkerId : IWorkerId
  {
    public string Name;

    public override string ToString()
    {
      return this.Name;
    }
  }

  abstract class Worker
  {
    private readonly IWorkerId id;
    private readonly IScheduler scheduler;
    private Thread thread;
    private ManualResetEvent stopRequested = new ManualResetEvent(false); // To stop the worker
    private ManualResetEvent idle = new ManualResetEvent(false); // To notify I am idle

    public Worker(IWorkerId id, IScheduler scheduler)
    {
      Contract.Requires(id != null);

      this.id = id;
      this.scheduler = scheduler;
      this.thread = CreateThread();
    }

    private Thread CreateThread()
    {
      var thread = new Thread(this.ThreadProc);
      thread.Name = this.id.ToString();
      return thread;
    }

    public IWorkerId Id { get { return this.id; } }

    public WaitHandle IdleWaitHandle { get { return this.idle; } }

    public void Start()
    {
      this.thread.Start();
    }

    public virtual void Stop()
    {
      this.stopRequested.Set();
      this.thread.Join();
      this.stopRequested.Reset();
    }

    public virtual void Cancel()
    {
      this.thread.Abort("Cancel requested"); // not very good, TODO: find another way to cancel (e.g. pass a CancellationToken to Clousot)
      this.thread = CreateThread(); // an aborted thread cannot be restarted
      this.thread.Start();
    }

#if CAN_PAUSE
    public virtual void Pause()
    {
      this.thread.Suspend();
    }

    public virtual void Resume()
    {
      this.thread.Resume();
    }
#endif

    private void ThreadProc()
    {
      try
      {
        while (!this.stopRequested.WaitOne(0))
        {
          IWorkId work;
          if (!this.TryGetWorkToDo(out work))
          {
            this.idle.Set();
            var timeToWait = 1000;
            if (this.stopRequested.WaitOne(timeToWait))
              break;
            continue;
          }
          this.idle.Reset();
          int returnCode = -1;
          try
          {
            returnCode = this.Do(work);
          }
          catch(Exception 
#if DEBUG
            e
#endif
            )
          {
#if DEBUG
            Console.WriteLine("Something went wrong when doing the analysis inside a worker {0}{1}", Environment.NewLine, e);
#endif
            returnCode = -1; // TODO: better error reporting
          }
          this.OnWorkDone(work, returnCode);
        }
      }
      finally
      {
        this.idle.Set();
      }
    }

    private bool TryGetWorkToDo(out IWorkId workId)
    {
      return this.scheduler.TryPop(this.id, out workId);
    }

    private void OnWorkDone(IWorkId work, int returnCode)
    {
      this.scheduler.ReportDone(this.id, work, returnCode);
    }

    protected abstract int Do(IWorkId work);
  }
}
