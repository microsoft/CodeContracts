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

namespace Tests.Sources
{

  [ContractClass(typeof(IContract<>))]
  public interface I<T> {
    T M(T x, string y);
    T N<Q>(T x, string y, Q z);
  }

  [ContractClassFor(typeof(I<>))]
  abstract class IContract<T> : I<T> {
    public T M(T x, string y) {
      Contract.Requires(y != null);
      return default(T);
    }
    public T N<Q>(T x, string y, Q z) {
      Contract.Requires(z != null);
      return default(T);
    }
  }

  public class Base {

    public int M(int x, string y) {
      return x;
    }
  }

  public class Intermediate<T> : Base {
    public T N<Q>(T x, string y, Q z) {
      return x;
    }
  }

  public class Derived : Intermediate<int>, I<int> {
  }

}
