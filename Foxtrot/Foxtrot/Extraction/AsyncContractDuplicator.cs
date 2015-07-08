using System.Compiler;

namespace Microsoft.Contracts.Foxtrot
{
    internal class AsyncContractDuplicator : Duplicator
    {
        private TypeNode parentType;

        public AsyncContractDuplicator(TypeNode parentType, Module module)
            : base(module, parentType)
        {
            this.parentType = parentType;
        }

        public override Expression VisitLocal(Local local)
        {
            if (HelperMethods.IsClosureType(this.parentType, local.Type))
            {
                // don't copy local as we need to share this closure local
                return local;
            }

            return base.VisitLocal(local);
        }
    }
}