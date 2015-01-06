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
using System.Collections.Specialized;
using System.Diagnostics.Contracts;

namespace OOBChecks
{
  /// <summary>
  /// Testing inheritance from system.dll
  /// </summary>
  public class MyDict : StringDictionary
  {
    /// <summary>
    /// Delegates calls to Count
    /// </summary>
    public override int Count
    {
      get
      {
        return base.Count;
      }
    }
  }

  /// <summary>
  /// An interface to Foo
  /// </summary>
  [ContractClass(typeof(IFooContracts))]
  public interface IFoo
  {
    /// <summary>
    /// Foo the value
    /// </summary>
    int Foo(int x, int y);
  }

  [ContractClassFor(typeof(IFoo))]
  abstract class IFooContracts : IFoo
  {
    #region IFoo Members

    int IFoo.Foo(int x, int y)
    {
      Contract.Requires(x > 0 ||
                        x == 0);
      Contract.Requires(y >= 0);
			Contract.Requires(y < 10);
			Contract.Ensures(Contract.Result<int>() >= 0, "better be greater than 0");
			return 0;
    }

    #endregion
  }

}
