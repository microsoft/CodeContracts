// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

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
        private Block _branchTargetToReplace;
        private Block _newBranchTarget;

        public ReplaceBranchTarget(Block blockToReplace, Block newTarget)
        {
            _branchTargetToReplace = blockToReplace;
            _newBranchTarget = newTarget;
        }

        public override void VisitBranch(Branch branch)
        {
            if (branch == null || branch.Target == null) return;

            if (branch.Target == _branchTargetToReplace)
            {
                branch.Target = _newBranchTarget;
                return;
            }

            base.VisitBranch(branch);
        }
    }
}