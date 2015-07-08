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