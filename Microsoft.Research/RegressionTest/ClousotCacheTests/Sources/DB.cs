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
using System.Diagnostics.Contracts;
using Microsoft.Research.ClousotRegression;

// Stressing the function name maximum length that can be stored in the DB
namespace Caching
{
  using A = IAmANamespaceWithARelativelyLongNameContainingAGenericClassWithARelativelyLongName.IAmAGenericClassWithARelativelyLongName<object, object>;
  namespace IAmANamespaceWithARelativelyLongNameContainingAGenericClassWithARelativelyLongName
  {
    class IAmAGenericClassWithARelativelyLongName<T1, T2> { }
  }
  namespace IAmANamespaceWithARelativelyLongNameContainingAGenericClassWithARelativelyLongName
  {
    using B = IAmAGenericClassWithARelativelyLongName<A, A>;
    namespace IAmANamespaceWithARelativelyLongNameContainingAGenericClassWithARelativelyLongName2
    {
      using C = IAmAGenericClassWithARelativelyLongName<B, B>;
      namespace IAmANamespaceWithARelativelyLongNameContainingAGenericClassWithARelativelyLongName3
      {
        using D = IAmAGenericClassWithARelativelyLongName<C, C>;
        namespace IAmANamespaceWithARelativelyLongNameContainingAGenericClassWithARelativelyLongName4
        {
          using E = IAmAGenericClassWithARelativelyLongName<D, D>;
          namespace IAmANamespaceWithARelativelyLongNameContainingAGenericClassWithARelativelyLongName5
          {
            using F = IAmAGenericClassWithARelativelyLongName<E, E>;
            namespace IAmANamespaceWithARelativelyLongNameContainingAGenericClassWithARelativelyLongName6
            {
              using G = IAmAGenericClassWithARelativelyLongName<F, F>;

              class IAmAStaticClassWithARelativelyLongName
              {
                [ClousotRegressionTest]
#if FIRST
                [RegressionOutcome("No entry found in the cache")]
#else
                [RegressionOutcome("No entry found in the cache")] // we won't save this as it is too long
                //                [RegressionOutcome("An entry has been found in the cache")]
#endif
                public static void IAmAFunctionWithARelativelyLongNameUsingTheGenericClassWithARelativelyLongName(G x)
                {
                  Contract.Requires(x != null);
                  Console.WriteLine(x);
                }
              }
            }
          }
        }
      }
    }
  }
}
