// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Compiler;

namespace Microsoft.Contracts.Foxtrot
{
    internal sealed class LookForBadStuff : Inspector
    {
        private ContractNodes _contractNodes;
        internal Statement ReturnStatement;
        internal bool BadStuffFound = false;

        internal LookForBadStuff(ContractNodes contractNodes)
        {
            _contractNodes = contractNodes;
        }

        public override void VisitReturn(Return Return)
        {
            this.ReturnStatement = Return;
            base.VisitReturn(Return);
        }

        public override void VisitExpressionStatement(ExpressionStatement statement)
        {
            Method methodCall = HelperMethods.IsMethodCall(statement);

            if (methodCall == _contractNodes.AssertMethod ||
                methodCall == _contractNodes.AssertWithMsgMethod ||
                methodCall == _contractNodes.AssumeMethod ||
                methodCall == _contractNodes.AssumeWithMsgMethod)
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