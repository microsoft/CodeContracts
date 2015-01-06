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
using System.Text;
using System.Diagnostics.Contracts;

namespace Microsoft.Research.DataStructures {

  public interface IWorkList<T> {
    bool Add(T o);
    bool IsEmpty { get; }
    T Pull();
  }

  [ContractVerification(true)]
  [ContractClass(typeof(AbstrWorkListContracts<>))]
  public abstract class AbstrWorkList<T> : IWorkList<T> {
    [ContractInvariantMethod]
    void ObjectInvariant()
    {
      Contract.Invariant(this.elems != null);
    }

    // set of worklist members - for quick membership testing
    protected Set<T> elems = new Set<T>();
    // collection of worklist elements - this provides the order
    protected abstract IEnumerable<T> coll { get; }

    public bool Add(T o) {
      if (!elems.AddQ(o)) return false;
      this.AddInternal(o);
      return true;
    }

    protected abstract void AddInternal(T o);
    public virtual bool AddAll(IEnumerable<T> objs) {
      Contract.Requires(objs != null);
      
      bool result = false;
      foreach (T obj in objs)
        if (Add(obj)) result = true;
      return result;
    }

    public virtual bool IsEmpty {
      get
      {
        Contract.Ensures(Contract.Result<bool>() == (this.Count == 0));

        return elems.Count == 0;
      }
    }

    public abstract T Pull();
    public virtual IEnumerator<T> GetEnumerator() {
      return coll.GetEnumerator();
    }

    public virtual int Count
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() == elems.Count);
        return elems.Count;
      }
    }
  }


  [ContractClassFor(typeof(AbstrWorkList<>))]
  abstract class AbstrWorkListContracts<T> : AbstrWorkList<T>
  {
    protected override IEnumerable<T> coll
    {
      get
      {
        Contract.Ensures(Contract.Result<IEnumerable<T>>() != null);

        throw new NotImplementedException();
      }
    }

    abstract protected override void AddInternal(T o);

    abstract public override T Pull();
  }

  /// <summary>
  /// Stack-based implementation of IWorkList.
  /// </summary>
  [ContractVerification(true)]
  public sealed class WorkStack<T> : AbstrWorkList<T> {

    [ContractInvariantMethod]
    void ObjectInvariant()
    {
      Contract.Invariant(this.stack != null);
    }

    readonly private Stack<T> stack;

    protected override IEnumerable<T> coll {
      get {
        return stack;
      }
    }

    public WorkStack() {
      this.stack = new Stack<T>();
    }

    protected override void AddInternal(T o) {
      stack.Push(o);
    }

    public override T Pull() {
      // F: I've added it: it is unclear why stack should not be empty at this point
      if (stack.Count == 0)
        throw new InvalidOperationException();

      T result = stack.Pop();
      elems.Remove(result);
      return result;
    }

  }


  /// <summary>
  /// Queue-based implementation of IWorkList.
  /// </summary>
  [ContractVerification(true)]
  public sealed class WorkList<T> : AbstrWorkList<T> {
    [ContractInvariantMethod]
    void ObjectInvariant()
    {
      Contract.Invariant(this.queue != null);
    }

    readonly private Queue<T> queue;

    protected override IEnumerable<T> coll {
      get {
        return queue;
      }
    }

    public WorkList() {
      queue = new Queue<T>();
    }

    protected override void AddInternal(T o) {
      queue.Enqueue(o);
    }

    public override T Pull() {
      // F: I've added it: it is unclear why queue should not be empty at this point
      if (queue.Count == 0)
        throw new InvalidOperationException();

      T o = queue.Dequeue();
      elems.Remove(o);
      return o;
    }
  }
}
