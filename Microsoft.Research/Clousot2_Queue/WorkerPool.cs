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
using System.Linq;
using System.Threading;

namespace Microsoft.Research.CodeAnalysis
{
  class WorkerPool
  {
    private readonly string name;
    private readonly bool canCancel;

    private readonly Dictionary<IWorkerId, Worker> workers = new Dictionary<IWorkerId, Worker>();
    private WaitHandle[] waitHandles;
    private Object waitHandlesLock = new Object();
    private int idgen = 0;

    public WorkerPool(string name, bool canCancel)
    {
      this.name = name;
      this.canCancel = canCancel;
    }

    public Worker CreateWorker(IWorkerFactory workerFactory)
    {
      Contract.Requires(workerFactory != null);

      var workerId = new WorkerId { Name = String.Format("{0}.{1}", this.name, this.idgen++) };

      var worker = workerFactory.NewWorker(workerId);

      worker.Start();

      lock (this.waitHandlesLock)
      {
        this.waitHandles = null;

        this.workers.Add(workerId, worker);
      }

      return worker;
    }

    public int Count { get { return this.workers.Count; } }

    public bool CancelWork(IWorkerId workerId)
    {
      if (!this.canCancel)
        return false;

      Worker worker;
      if (!this.workers.TryGetValue(workerId, out worker))
        return false;

      worker.Cancel();
      worker.Start();

      return true;
    }

    public void WaitAll()
    {
      WaitAllAnd(additionalWaitHandles: null);
    }

    public void WaitAllAnd(WaitHandle additionalWaitHandle)
    {
      WaitAllAnd(new WaitHandle[] { additionalWaitHandle });
    }

    public void WaitAllAnd(IEnumerable<WaitHandle> additionalWaitHandles)
    {
      WaitHandle[] wh;

      lock (this.waitHandlesLock)
      {
        if (this.waitHandles == null)
          this.waitHandles = this.workers.Select(p => p.Value.IdleWaitHandle).ToArray();

        wh = this.waitHandles;
      }

      if (additionalWaitHandles != null && additionalWaitHandles.Any())
        wh = wh.Concat(additionalWaitHandles).ToArray();

      if (Thread.CurrentThread.GetApartmentState() == ApartmentState.STA)
      {
        // This is only for the regression test
        // No WaitAll with STA threads, hack to simulate it
        // This will only happen in test mode, because we cannot control the ApartmentState in this case

        var allEventSignaled = false;
        while (!allEventSignaled) // only works with ManuelResetEvents
        {
          // only ok if the second .All is atomic
          allEventSignaled = wh.All(w => w.WaitOne()) && wh.All(w => w.WaitOne(0));
        }
      }
      else
      {
        // only works with up to 64 wait handles
        // TODO: investigate on using CountdownEvent
        
        WaitHandle.WaitAll(wh);
      }
    }

    public void StopAll()
    {
      foreach (var p in this.workers)
        p.Value.Stop();
    }
  }
}
