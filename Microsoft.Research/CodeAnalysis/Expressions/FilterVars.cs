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
using Microsoft.Research.DataStructures;
using System.Diagnostics.Contracts;

namespace Microsoft.Research.CodeAnalysis.Expressions
{
  public class FilterVars<Var> : BoxedExpressionTransformer<Void>
  {
    private readonly Func<Var, FList<PathElement>> AccessPath;
    private readonly Func<object, PathElement[], bool> Filter;

    public FilterVars(Func<Var, FList<PathElement>> AccessPath, Func<object, PathElement[], bool> Filter)
    {
      Contract.Requires(Filter != null);

      this.AccessPath = AccessPath;
      this.Filter = Filter;
    }

    protected override BoxedExpression Variable(BoxedExpression original, object var, PathElement[] path)
    {
      if (this.Filter(var, path))
      {
        return null;
      }

      return base.Variable(original, var, path);
    }

    protected override Func<object, FList<PathElement>> GetPathFetcher(Func<object, DataStructures.FList<PathElement>> PathFetcher)
    {
      return (object obj) =>
      {
        if (obj is Var) return this.AccessPath((Var)obj);
        else return null;
      };
    }
  }
}
