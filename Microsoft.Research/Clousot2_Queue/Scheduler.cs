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

namespace Microsoft.Research.CodeAnalysis
{
  class NoloopScheduler : IScheduler
  {
    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(queue != null);
    }


    protected readonly IQueue queue;

    public event Action<ISliceId, int> OnWorkDone;

    public NoloopScheduler(IQueue queue)
    {
      Contract.Requires(queue != null);

      this.queue = queue;
    }

    public void FeedQueue(IEnumerable<ISliceId> sliceIds)
    {
      Contract.Requires(sliceIds != null);

      foreach (var sliceId in sliceIds)
      {
        this.queue.AddTodo(sliceId);
      }
    }

    public bool TryPop(IWorkerId workerId, out IWorkId workId)
    {
      return this.queue.TryPop(workerId, out workId);
    }

    public virtual void ReportDone(IWorkerId workerId, IWorkId workId, int returnCode)
    {
      // TODO: what to do with errors? -- the time being we let OnWorkDone record them
      if (this.OnWorkDone != null)
      {
        this.OnWorkDone(workId.SliceId, returnCode);
      }

      this.queue.ReportDone(workerId, workId);
    }

    protected virtual IEnumerable<ISliceId> SlicesToScheduleForReanalysis(IWorkId workId)
    {
      yield break;
    }
  }

  class LazyScheduler : NoloopScheduler
  {
    protected readonly IDB db;

    public LazyScheduler(IQueue queue, IDB db)
      : base(queue)
    {
      Contract.Requires(queue != null);
      this.db = db;
    }

    public override void ReportDone(IWorkerId workerId, IWorkId workId, int returnCode)
    {
      base.ReportDone(workerId, workId, returnCode);

      this.FeedQueue(this.SlicesToScheduleForReanalysis(workId));
    }

    protected override IEnumerable<ISliceId> SlicesToScheduleForReanalysis(IWorkId workId)
    {
      return this.db.Dependences(workId.SliceId);
    }
  }

  class LazySchedulerForObjectInvariants : LazyScheduler
  {
    public LazySchedulerForObjectInvariants(IQueue queue, IDB db)
      : base(queue, db)
    {
      Contract.Requires(queue != null);
    }

    protected override IEnumerable<ISliceId> SlicesToScheduleForReanalysis(IWorkId workId)
    {
      return base.SlicesToScheduleForReanalysis(workId).Union(this.db.SlicesForMethodsInTheSameType(workId.SliceId)).Distinct();
    }
  }
}
