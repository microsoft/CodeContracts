// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using Microsoft.Research.DataStructures;

namespace Microsoft.Research.CodeAnalysis.Expressions
{
    public class SourceLevelReadableExpression<Variable> : BoxedExpressionTransformer<Void>
    {
        private readonly Func<Variable, FList<PathElement>> AccessPath;

        public SourceLevelReadableExpression(Func<Variable, FList<PathElement>> AccessPath)
        {
            this.AccessPath = AccessPath;
        }

        protected override Func<object, FList<PathElement>> GetPathFetcher(Func<object, DataStructures.FList<PathElement>> PathFetcher)
        {
            if (AccessPath != null)
            {
                return (object obj) =>
                {
                    if (obj is Variable) return AccessPath((Variable)obj);
                    else return null;
                };
            }
            else // return the function always returning null
            {
                return (object obj) => { return null; };
            }
        }
    }
}
