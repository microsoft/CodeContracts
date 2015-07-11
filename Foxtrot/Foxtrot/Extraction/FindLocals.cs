// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Compiler;
using Microsoft.Research.DataStructures;

namespace Microsoft.Contracts.Foxtrot
{
    internal class FindLocals : Inspector
    {
        private Set<Local> _locals = new Set<Local>();

        private FindLocals()
        {
        }

        public static Set<Local> Get(Expression e)
        {
            var n = new FindLocals();
            n.Visit(e);
            return n._locals;
        }

        public override void VisitLocal(Local local)
        {
            _locals.Add(local);
        }
    }
}