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