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
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using Microsoft.Research.CodeAnalysis.Caching;

namespace Microsoft.Research.CodeAnalysis
{
  static class DummyExpressionHasher
  {
    public static byte[] Hash(BoxedExpression expr, bool trace)
    {
      Contract.Ensures(Contract.Result<byte[]>() != null);

      using (var tw = new HashWriter(trace))
      {
        tw.WriteLine(expr.ToString());
        return tw.GetHash();
      }
    }

    public static byte[] Hash(IEnumerable<BoxedExpression> exprs, bool trace)
    {
      Contract.Ensures(Contract.Result<byte[]>() != null);

      using (var tw = new HashWriter(trace))
      {
        foreach (var expr in exprs)
          tw.WriteLine(expr.ToString());
        return tw.GetHash();
      }
    }

    public static byte[] Hash<Field, Method>(InferredExpr<Field, Method> expr, bool trace)
    {
      Contract.Ensures(Contract.Result<byte[]>() != null);

      using (var tw = new HashWriter(trace))
      {
        tw.Write(expr.ToHashString());
        return tw.GetHash();
      }
    }
  }
}
