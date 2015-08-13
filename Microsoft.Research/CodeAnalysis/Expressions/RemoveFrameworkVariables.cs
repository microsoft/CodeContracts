// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Research.DataStructures;

namespace Microsoft.Research.CodeAnalysis.Expressions
{
    public class RemoveFrameworkVariables<Var> : BoxedExpressionTransformer<Void>
    {
        private readonly Func<Var, FList<PathElement>> AccessPath;
        public RemoveFrameworkVariables(Func<Var, FList<PathElement>> AccessPath)
        {
            this.AccessPath = AccessPath;
        }


        protected override BoxedExpression Variable(BoxedExpression original, object var, PathElement[] path)
        {
            // Let's remove the variable
            return new BoxedExpression.VariableExpression(null, path);
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
