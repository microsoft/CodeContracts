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
      if (behave)
      {
        var exprs = new List<Expr>()
          {
          };
        
        exprs.AsInferredPreconditions(true);
      }
      else
      {
        IEnumerable<Expr> exprs = null;
        
        exprs.AsInferredPreconditions(true);
      }
    }

    public ContractFailureKind NegativeExpectedKind = ContractFailureKind.Precondition;
    public string NegativeExpectedCondition = "collection != null";

  }

  public class InferredPreconditions {
    public InferredPreconditions(IEnumerable<Pre> pre) {}
  }
  
  public class Pre {
    public Pre(Expr expr, bool isSufficient) {}
  }
  
  public class Expr {
  }

  /// Tests that we don't delete only part of the closure initialization
  public static class Extensions {
    [Pure]
    public static InferredPreconditions AsInferredPreconditions(this IEnumerable<Expr> collection, bool isSufficient)
    {
      Contract.Requires(collection != null);
      Contract.Ensures(Contract.Result<InferredPreconditions>() != null);

      return new InferredPreconditions(collection.Select(expr => new Pre(expr, isSufficient)));
    }

  }

}
