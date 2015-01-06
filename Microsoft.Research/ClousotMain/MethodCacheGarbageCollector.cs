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
using System.Diagnostics.Contracts;

namespace Microsoft.Research.CodeAnalysis
{
  public class MethodCacheGarbageCollector<Method>
  {
    readonly private Dictionary<Method, List<Method>> continuationList;

    public MethodCacheGarbageCollector()
    {
      this.continuationList = new Dictionary<Method, List<Method>>();
    }

    public void NotifyNewMethodToAnalyze(Method m, IEnumerable<Method> successors)
    {
      Contract.Requires(successors != null);
#if false
      Console.Write("Adding the successors of {0}:\n\t", m);
      foreach (var succ in successors)
      {
        if (succ != null)
        {
          Console.Write("{0} ", succ.ToString());
        }
      }
      Console.WriteLine();
#endif
      this.continuationList[m] = successors.ToList();
    }

    public List<Method> NotifyMethodAnalysisDone(Method m)
    {
      var result = new List<Method>();

      foreach (var pair in continuationList)
      {
        if (pair.Value.Contains(m))
        {
          pair.Value.Remove(m);

          if (pair.Value.Count == 0)
          {
            result.Add(pair.Key);
          }
        }
      }

      return result;
    }
  }
}
