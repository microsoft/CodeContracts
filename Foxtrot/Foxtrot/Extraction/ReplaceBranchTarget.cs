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
    /// Used to deal with legacy if-then-throw requires.
    /// The original structure looks like this:
    /// 
    ///    blocks evaluating the condition and jumping to "success" when
    ///    the pre-condition is true (due to || and &&)
    ///    
    ///    blocks evaluating and throwing the exception
    ///    
    ///    success:
    ///    
    /// We turn this into the following code:
    /// 
    ///    (unchanged blocks evaluating condition and jumping to success)
    ///    
    ///    preconditionHolds = false;
    ///    goto Common;
    ///    
    ///    success:  preConditionHolds=true;
    ///              
    ///    common: preConditionHolds
    /// </summary>
    internal sealed class ReplaceBranchTarget : Inspector
    {
        private Block branchTargetToReplace;
        private Block newBranchTarget;

        public ReplaceBranchTarget(Block blockToReplace, Block newTarget)
        {
            branchTargetToReplace = blockToReplace;
            newBranchTarget = newTarget;
        }

        public override void VisitBranch(Branch branch)
        {
            if (branch == null || branch.Target == null) return;

            if (branch.Target == branchTargetToReplace)
            {
                branch.Target = this.newBranchTarget;
                return;
            }

            base.VisitBranch(branch);
        }
    }
}