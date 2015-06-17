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

namespace Tests.Sources
{

  partial class TestMain
  {
    partial void Run()
    {
      foreach (var item in this.TestClosureAndAbbreviator(new[] { 1, 2, 3, 4, 5 }, 5))
      {
        Console.WriteLine("{0}", item);
      }

      if (!behave)
      {
        foreach (var item in this.TestClosureAndAbbreviator(new[] { 1, 2, -3, 4, 5 }, 5))
        {
          Console.WriteLine("{0}", item);
        }
      }
    }

    public ContractFailureKind NegativeExpectedKind = ContractFailureKind.Precondition;
    public string NegativeExpectedCondition = "Contract.ForAll(data, i => i > 0)";

    public IEnumerable<int> TestClosureAndAbbreviator(IEnumerable<int> data, int scale)
    {
      Contract.Requires(data != null);
#if ROSLYN
      AllPositive(null, data);
#else
      AllPositive(new []{"a"}, data);
#endif 
      return data.Select(i => i * scale);
    }

    [ContractAbbreviator]
    public static void AllPositive(string[] values, IEnumerable<int> data) {
      Contract.Requires(Contract.ForAll(data, i => i > 0));
#if !ROSLYN
      Contract.Requires(values.Length > 0);
#endif
      Contract.Ensures(Contract.ForAll(Contract.Result<IEnumerable<int>>(), i => i > 0));
    }
  }
}

#if NETFRAMEWORK_4_0 || NETFRAMEWORK_3_5

namespace System.Diagnostics.Contracts {
  public class ContractAbbreviatorAttribute : Attribute {
  }
}
#endif
