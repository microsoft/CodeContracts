using System.Compiler;

namespace Microsoft.Contracts.Foxtrot
{
    internal sealed class ReplaceReturns : StandardVisitor
    {
        private readonly Local result;
        private readonly Block newExit;

        private int returnCount;

        private readonly bool leaveExceptionBody;
        private SourceContext lastReturnSourceContext;

        public ReplaceReturns(Local r, Block b, bool leaveExceptionBody = false)
        {
            result = r;
            newExit = b;
            this.leaveExceptionBody = leaveExceptionBody;
        }

        //public override Block VisitBlock(Block block) {
        //  if(block.Statements != null && block.Statements.Count == 1) {
        //    Return r = block.Statements[0] as Return;
        //    if(r != null) {
        //      Statement s = this.VisitReturn(r);
        //      Block retBlock = s as Block;
        //      if(retBlock != null) {
        //        block.Statements = retBlock.Statements;
        //        return block;
        //      } else {
        //        return base.VisitBlock(block);
        //      }
        //    } else {
        //      return base.VisitBlock(block);
        //    }
        //  } else {
        //    return base.VisitBlock(block);
        //  }
        //}

        public override Statement VisitReturn(Return Return)
        {
            if (Return == null)
            {
                return null;
            }

            returnCount++;
            this.lastReturnSourceContext = Return.SourceContext;

            StatementList stmts = new StatementList();

            Return.Expression = this.VisitExpression(Return.Expression);

            if (Return.Expression != null)
            {
                MethodCall mc = Return.Expression as MethodCall;
                if (mc != null && mc.IsTailCall)
                {
                    mc.IsTailCall = false;
                }

                var assgnmt = new AssignmentStatement(result, Return.Expression);

                assgnmt.SourceContext = Return.SourceContext;
                stmts.Add(assgnmt);
            }

            // the branch is a "leave" out of the try block that the body will be
            // in.
            var branch = new Branch(null, newExit, false, false, this.leaveExceptionBody);
            branch.SourceContext = Return.SourceContext;

            stmts.Add(branch);

            return new Block(stmts);
        }

        public SourceContext LastReturnSourceContext
        {
            get { return this.lastReturnSourceContext; }
        }
    }
}