// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

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
            if (Filter(var, path))
            {
                return null;
            }

            return base.Variable(original, var, path);
        }

        protected override Func<object, FList<PathElement>> GetPathFetcher(Func<object, DataStructures.FList<PathElement>> PathFetcher)
        {
            return (object obj) =>
            {
                if (obj is Var) return AccessPath((Var)obj);
                else return null;
            };
        }
    }
}
