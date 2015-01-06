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

#if false
using System;

namespace Microsoft.Research.CodeAnalysis
{
  class Program
  {
    static void Main(string[] args)
    {
      var db = new SingletonDB();
      var workerPool = new WorkerPool("Hen");
      var queue = new FIFOQueue(workerPool, db);
      var scheduler = new LazyScheduler(queue);
      var workerFactory = new ClousotWorkerFactory(scheduler, null, db);

      scheduler.FeedQueue(new ISliceId[0]); // TODO: call slicer

      var numberOfWorkers = Environment.ProcessorCount;
      numberOfWorkers = 1;

      for (var i = 0; i < numberOfWorkers; i++)
        workerPool.CreateWorker(workerFactory);

      workerPool.WaitAll();
      workerPool.StopAll();
    }
  }
}
#endif