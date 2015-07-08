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