using System.Collections.Generic;
using System.Compiler;

namespace Microsoft.Contracts.Foxtrot
{
    /// FIXME! Need to do a sanity check that the argument to the Old method
    /// is a member binding (or at least something that is directly there and
    /// not something sitting on the stack as a result of some code sequence
    /// that this class doesn't capture and store in the OldExpressions list).
    internal sealed class SubstituteClosureClassWithinOldExpressions : StandardVisitor
    {
        private readonly Dictionary<TypeNode, Local> closureLocals;

        public SubstituteClosureClassWithinOldExpressions(Dictionary<TypeNode, Local> closureLocals)
        {
            this.closureLocals = closureLocals;
        }

        public override Expression VisitMemberBinding(MemberBinding memberBinding)
        {
            Local closureLocal;
            if (memberBinding.TargetObject != null &&
                this.closureLocals.TryGetValue(memberBinding.TargetObject.Type, out closureLocal))
            {
                return new MemberBinding(closureLocal, memberBinding.BoundMember);
            }
            
            return base.VisitMemberBinding(memberBinding);
        }
    }
}