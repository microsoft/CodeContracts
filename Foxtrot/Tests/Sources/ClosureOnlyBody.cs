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
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;
using System.Threading;

namespace Tests.Sources
{

  partial class TestMain
  {
    partial void Run()
    {
      if (behave)
      {
        Schedule(this.DoWork, 10);
      }
      else
      {
        Schedule(this.DoWork, 10);
      }
    }

    public ContractFailureKind NegativeExpectedKind = ContractFailureKind.Precondition;
    public string NegativeExpectedCondition = "this.behave";

    WaitHandle _waitHandle = new AutoResetEvent(false);

    void Schedule(Action action, int dueTimeMs)
    {
      //Contract.Ensures(Contract.ForAll(0, 5, i => action!= null));

      WaitHandle handle = null;
      handle = RegisterWaitForSingleObject(
          _waitHandle,
          () =>
          {
            action();
            if (handle != null)
              handle.GetHashCode();
          },
          dueTimeMs);
    }

    static WaitHandle RegisterWaitForSingleObject(WaitHandle wh, Action a, int ms)
    {
      a();
      return wh;
    }

    void DoWork()
    {
      Contract.Requires(this.behave);
    }
  }

}
