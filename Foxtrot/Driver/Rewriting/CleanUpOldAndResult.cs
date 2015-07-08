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
    /// The Extractor has removed all dependencies on whatever contract class was
    /// used in the assembly. (See the class ExtractAllContractNodesDependencies.)
    /// 
    /// This class exists just to make sure that those pseudo-nodes are replaced with
    /// things the Writer won't barf on.
    /// For instance, don't leave any ReturnValue or OldExpression ASTs in the assembly.
    /// </summary>
    internal sealed class CleanUpOldAndResult : StandardVisitor
    {
        public override Expression VisitOldExpression(OldExpression oldExpression)
        {
            //Debug.Assert(false, "old was not substituted");
            TypeNode returnType = oldExpression.Type;
            if (returnType.IsValueType)
                return new Literal(0, returnType);
            
            return new Literal(null, returnType);
        }

        public override Expression VisitReturnValue(ReturnValue returnValue)
        {
            // return a default value of the same type as the return value
            TypeNode returnType = returnValue.Type;
            ITypeParameter itp = returnType as ITypeParameter;
            if (itp != null)
            {
                Local loc = new Local(returnType);

                UnaryExpression loca = new UnaryExpression(loc, NodeType.AddressOf, loc.Type.GetReferenceType());
                StatementList statements = new StatementList(2);

                statements.Add(new AssignmentStatement(new AddressDereference(loca, returnType, false, 0),
                    new Literal(null, SystemTypes.Object)));

                statements.Add(new ExpressionStatement(loc));

                return new BlockExpression(new Block(statements), returnType);
            }

            if (returnType.IsValueType)
                return new Literal(0, returnType);
            
            return new Literal(null, returnType);
        }
    }
}