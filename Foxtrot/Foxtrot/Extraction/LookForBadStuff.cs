using System.Compiler;

namespace Microsoft.Contracts.Foxtrot
{
    internal sealed class LookForBadStuff : Inspector
    {
        private ContractNodes contractNodes;
        internal Statement ReturnStatement;
        internal bool BadStuffFound = false;

        internal LookForBadStuff(ContractNodes contractNodes)
        {
            this.contractNodes = contractNodes;
        }

        public override void VisitReturn(Return Return)
        {
            this.ReturnStatement = Return;
            base.VisitReturn(Return);
        }

        public override void VisitExpressionStatement(ExpressionStatement statement)
        {
            Method methodCall = HelperMethods.IsMethodCall(statement);

            if (methodCall == this.contractNodes.AssertMethod ||
                methodCall == this.contractNodes.AssertWithMsgMethod ||
                methodCall == this.contractNodes.AssumeMethod ||
                methodCall == this.contractNodes.AssumeWithMsgMethod)
            {
                this.BadStuffFound = true;
            }

            base.VisitExpressionStatement(statement);
        }

        internal void CheckForBadStuff(StatementList contractClump)
        {
            this.VisitStatementList(contractClump);
        }
    }
}