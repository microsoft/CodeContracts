using System.Compiler;

namespace Microsoft.Contracts.Foxtrot
{
    internal sealed class RemoveShortBranches : Inspector
    {
        public override void VisitBranch(Branch branch)
        {
            branch.ShortOffset = false;
        }
    }
}