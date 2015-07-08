using System.Compiler;

namespace Microsoft.Contracts.Foxtrot
{
    internal sealed class WrapParametersInOldExpressions : StandardVisitor
    {
        public override Expression VisitOldExpression(OldExpression oldExpression)
        {
            // no point descending down into any old expressions
            return oldExpression;
        }

        public override Expression VisitParameter(Parameter parameter)
        {
            if (parameter == null) return null;

            var oe = new OldExpression(parameter);
            oe.Type = parameter.Type;

            return oe;
        }
    }
}