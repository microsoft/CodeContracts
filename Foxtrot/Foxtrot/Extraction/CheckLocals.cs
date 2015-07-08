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

using System.Compiler;

namespace Microsoft.Contracts.Foxtrot
{
    /// <summary>
    /// Scans code and flags an error if any local is found that
    /// is already in the static hashtable GatherLocals.Locals
    /// Each instance of the class starts out with the flag off
    /// and turns it on if a local is found that is in the
    /// hashtable. Create a new instance for each check.
    /// </summary>
    internal class CheckLocals : Inspector
    {
        internal Local reUseOfExistingLocal = null;
        private GatherLocals gatherLocals;

        public CheckLocals(GatherLocals gatherLocals)
        {
            this.gatherLocals = gatherLocals;
        }

        /// <summary>
        /// check for reused local.
        /// HACK: ignore VB cached boolean locals used in tests. VB does this seemingly for debuggability.
        /// These are always assigned before use in the same scope, so we ignore them.
        /// </summary>
        /// <param name="local"></param>
        /// <returns></returns>
        public override void VisitLocal(Local local)
        {
            if (gatherLocals.Locals[local.UniqueKey] != null)
            {
                if (local.Name != null && local.Name.Name.StartsWith("VB$CG$"))
                {
                    // ignore VB compiler generated locals.
                }
                else
                {
                    reUseOfExistingLocal = local;
                }
            }

            base.VisitLocal(local);
        }
    }
}