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
using System.Threading;

namespace Tests.Sources
{

#if NETFRAMEWORK_4_5

  using System.Threading.Tasks;

  class BlobStoreClient
  {
    private readonly Dictionary<int, bool> m_localBlobStore;

    public async Task<bool> BlobExists(int contentId, CancellationToken cancellationToken)
    {
      Contract.Requires(contentId > 0, "Argument contentId cannot be null");
      Contract.Requires(cancellationToken != null, "Argument cancellationToken cannot be null");

      //using (new LogPerfOperation())
      {
        bool exists = false;

        if (true || m_localBlobStore.ContainsKey(contentId))
        {
          exists = true;
        }
        else
        {
          exists = await InvokeWithTableUpdate(async () =>
          {
            return GetBlobManager(contentId);
          }, cancellationToken);
        }

        return exists;
      }
    }
    private bool GetBlobManager(int contentId)
    {
      return true;
    }

    private async Task<T> InvokeWithTableUpdate<T>(Func<Task<T>> method, CancellationToken cancellation)
    {
      return default(T);
    }

  }
  partial class TestMain
  {
    void TestMe(BlobStoreClient c, bool behave) {
      {
        CancellationToken token = new CancellationToken();
        var z = c.BlobExists(5, token);
        if (!behave) {
          c.BlobExists(0, token);
        }
      }
    }

    void Test()
    {
      var g = new BlobStoreClient();
      TestMe(g, behave);

    }

    partial void Run()
    {
      this.Test();
    }

    public ContractFailureKind NegativeExpectedKind = ContractFailureKind.Precondition;
    public string NegativeExpectedCondition = "contentId > 0";
  }
#else
  // Dummy for Pre 4.5
  partial class TestMain 
  {

    void Test() {
      Contract.Requires(false);
    }

    partial void Run()
    {
      if (!behave) {
        this.Test();
      }
    }

    public ContractFailureKind NegativeExpectedKind = ContractFailureKind.Precondition;
    public string NegativeExpectedCondition = "false";
  }
#endif
}
