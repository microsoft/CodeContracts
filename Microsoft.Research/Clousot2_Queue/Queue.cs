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
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.Research.DataStructures;
using System.Diagnostics.Contracts;

namespace Microsoft.Research.CodeAnalysis
{
  class WorkToDo<TPriority> : IWorkId
    where TPriority : IComparable<TPriority>
  {
    public readonly TPriority Priority;
    private readonly ISliceId sliceId;
    private readonly DateTime time;
    private readonly ISliceHash sliceHash;

    public WorkToDo(ISliceId sliceId, DateTime time, ISliceHash sliceHash, TPriority priority)
    {
      this.sliceId = sliceId;
      this.time = time;
      this.sliceHash = sliceHash;
      this.Priority = priority;
    }

    public ISliceId SliceId { get { return this.sliceId; } }
    public DateTime Time { get { return this.time; } }
    public ISliceHash SliceHash { get { return this.sliceHash; } }

    public override int GetHashCode() { return HashHelpers.GetStructuralHashCode(this.Priority, this.sliceId, this.time, this.sliceHash); }
    public override bool Equals(object obj) { return this.Equals(obj as IWorkId); }
    public bool Equals(IWorkId other) { return other != null && this.sliceId.Equals(other.SliceId) && this.time.Equals(other.Time) && this.sliceHash.Equals(other.SliceHash); }

    #region Comparer

    public static readonly Comparer<WorkToDo<TPriority>> PriorityComparer = new PriorityComparerInternal();

    private class PriorityComparerInternal : Comparer<WorkToDo<TPriority>>
    {
      public override int Compare(WorkToDo<TPriority> x, WorkToDo<TPriority> y)
      {
        return x.Priority.CompareTo(y.Priority);
      }
    }

    #endregion

    #region Dictionary

    public class SliceIdProjectedDictionary : ProjectedDictionary<WorkToDo<TPriority>, ISliceId, int>
    {
      public SliceIdProjectedDictionary() : base(w => w.SliceId) { }
    }

    #endregion
  }

  // Made partial to sepate the "DB" part and the Control part
  abstract partial class Queue<TPriority> : IQueue
    where TPriority : IComparable<TPriority>
  {
      public bool optionA1 = false;
      public bool optionA2 = false;
      public bool optionB1 = false;
      public bool optionB2 = false;

    public event Action<IWorkerId, IWorkId> OnWorkDone;
    
    private readonly Object Lock = new Object();
    private readonly HashSetPriorityQueue<WorkToDo<TPriority>> ToDo = HashSetPriorityQueue<WorkToDo<TPriority>>.Create<WorkToDo<TPriority>.SliceIdProjectedDictionary>(WorkToDo<TPriority>.PriorityComparer);
    private readonly SubKeyDictionary<WorkToDo<TPriority>, ISliceId, ISliceHash, IWorkerId> WorkInProgress = new SubKeyDictionary<WorkToDo<TPriority>, ISliceId, ISliceHash, IWorkerId>(w => w.SliceId, w => w.SliceHash);
    private readonly ManualResetEvent EmptyQueue = new ManualResetEvent(true);

    public WaitHandle EmptyQueueWaitHandle { get { return this.EmptyQueue; } }
    
    public bool AddTodo(ISliceId sliceId)
    {
      DateTime t = DateTime.Now;

      var sliceHash = this.ComputeSliceHash(sliceId, t);

      if (this.AlreadyComputed(sliceId, sliceHash))
        return false;

      if (this.WorkInProgress.ContainsKey(sliceHash))
        return false;

      var priority = this.ComputePriority(sliceId, t); // by default it is based on time, but we can put whatever we want

      var workToDo = new WorkToDo<TPriority>(sliceId, t, sliceHash, priority);

      lock (this.Lock)
      {
        // Notify that the queue is not empty anymore.
        // The main program ends when the queue is empty and all the workers are idle
        this.EmptyQueue.Reset();

        // Remove all the things to do which have the same slice ID
        // This is not true, as our analysis is not monotonic
        this.ToDo.Remove(workToDo);

        // Add to to the queue
        this.ToDo.Add(workToDo);

        // Untested option
        if (this.optionB1)
          foreach (var p in this.WorkInProgress.GetValueOrEmpty(sliceId).AssumeNotNull())
            this.CancelWork(p.Key, p.Value);

        // Untested option
        if (this.optionA1)
          this.WorkInProgress.Remove(sliceId);
      }

      return true;
    }

    public bool TryPop(IWorkerId workerId, out IWorkId workId)
    {
      lock (this.Lock)
      {
        if (this.ToDo.Count == 0)
        {
          workId = default(IWorkId);
          return false;
        }

        var workToDo = this.ToDo.Pop();
        Contract.Assume(workToDo != null);
        // Untested option
        if (this.optionB2)
          foreach (var p in this.WorkInProgress.GetValueOrEmpty(workToDo.SliceId).AssumeNotNull())
            this.CancelWork(p.Key, p.Value);

        // Untested option
        if (this.optionA2)
          this.WorkInProgress.Remove(workToDo.SliceId);

        this.WorkInProgress.Add(workToDo, workerId);

        workId = workToDo;
        return true;
      }
    }

    public void ReportDone(IWorkerId workerId, IWorkId workId)
    {
      lock (this.Lock)
      {
        // This loop is not sound as we are not monotonic
        foreach (var p in this.WorkInProgress.GetValueOrEmpty(workId.SliceId).AssumeNotNull().Where(p => p.Key.Time < workId.Time).ToArray())
          if (this.CancelWork(p.Key, p.Value))
            this.WorkInProgress.Remove(p.Key);

        this.WorkInProgress.Remove(workId.SliceId, workId.SliceHash);
      }

      this.MarkAsComputed(workId.SliceId, workId.SliceHash);

      // Maybe not used, see if we can remove it
      if (this.OnWorkDone != null)
        this.OnWorkDone(workerId, workId);

      lock (this.Lock)
      {
        // If the queue is empty, let's trigger the event
        if (this.InternalIsEmpty())
          this.EmptyQueue.Set();
      }
    }

    private bool InternalIsEmpty()
    {
      return this.ToDo.Count == 0 && !this.WorkInProgress.Any();
    }

    public bool IsEmpty()
    {
      lock (this.Lock)
        return this.InternalIsEmpty();
    }

    protected virtual bool CancelWork(WorkToDo<TPriority> work, IWorkerId workerId)
    {
      return false;
    }

    protected abstract TPriority ComputePriority(ISliceId sliceId, DateTime t);
  }

  abstract partial class Queue<TPriority> : IQueue
    where TPriority : IComparable<TPriority>
  {
    private readonly IDB db;

    public Queue(IDB db)
    {
      this.db = db;
    }

    private ISliceHash ComputeSliceHash(ISliceId sliceId, DateTime t)
    {
      return this.db.ComputeSliceHash(sliceId, t);
    }
    private bool AlreadyComputed(ISliceId sliceId, ISliceHash sliceHash)
    {
      return this.db.AlreadyComputed(sliceId, sliceHash);
    }
    private void MarkAsComputed(ISliceId sliceId, ISliceHash sliceHash)
    {
      this.db.MarkAsComputed(sliceId, sliceHash);
    }
  }

  abstract class CancellableQueue<TPriority> : Queue<TPriority>
    where TPriority : IComparable<TPriority>
  {
    private readonly WorkerPool workerPool;

    public CancellableQueue(WorkerPool workerPool, IDB db)
      : base(db)
    {
      this.workerPool = workerPool;
    }

    protected override bool CancelWork(WorkToDo<TPriority> work, IWorkerId workerId)
    {
      return this.workerPool.CancelWork(workerId);
    }
  }

  class FIFOQueue : CancellableQueue<long>
  {
    public FIFOQueue(WorkerPool workerPool, IDB db)
      : base(workerPool, db)
    { }

    protected override long ComputePriority(ISliceId sliceId, DateTime t)
    {
      return -t.Ticks;
    }
  }
}
