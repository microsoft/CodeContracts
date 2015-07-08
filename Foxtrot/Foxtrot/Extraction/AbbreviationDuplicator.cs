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

using System;
using System.Compiler;

namespace Microsoft.Contracts.Foxtrot
{
    /// <summary>
    /// Used to duplicate contracts from an abbreviation method (validator). It must instantiate 
    /// the contracts to the calling context by duplicating the actual argument expressions into
    /// the contracts where the helper parameters occur.
    /// </summary>
    internal class AbbreviationDuplicator : DuplicatorForContractsAndClosures
    {
        private ExpressionList actuals;
        private Expression targetObject;
        private Method abbreviation;
        private TrivialHashtable localsInActuals;

        public AbbreviationDuplicator(Method sourceMethod, Method targetMethod, ContractNodes contractNodes,
            Method abbreviation, Expression targetObject, ExpressionList actuals)
            : base(targetMethod.DeclaringType.DeclaringModule, sourceMethod, targetMethod, contractNodes, false)
        {
            this.targetObject = targetObject;
            this.abbreviation = abbreviation;
            this.actuals = actuals;

            this.localsInActuals = new TrivialHashtable();

            PopulateLocalsInActuals();
        }

        private void PopulateLocalsInActuals()
        {
            var finder = new FindLocalsInActuals(this.localsInActuals);

            finder.VisitExpression(this.targetObject);
            finder.VisitExpressionList(this.actuals);
        }

        private class FindLocalsInActuals : Inspector
        {
            private TrivialHashtable localsFound;

            public FindLocalsInActuals(TrivialHashtable target)
            {
                this.localsFound = target;
            }

            public override void VisitLocal(Local local)
            {
                if (local == null) return;

                this.localsFound[local.UniqueKey] = local;
            }
        }

        public override Expression VisitThis(This This)
        {
            if (This.DeclaringMethod == abbreviation)
                return (Expression) this.Visit(this.targetObject);

            if (This.DeclaringMethod == this.targetMethod)
                // important lower case. Upper case is Duplicator's target method
                return This;

            return base.VisitThis(This);
        }

        public override Expression VisitParameter(Parameter parameter)
        {
            if (parameter == null) return null;

            if (parameter.DeclaringMethod == abbreviation)
            {
                var argIndex = parameter.ParameterListIndex;

                if (argIndex >= actuals.Count) throw new InvalidOperationException("bad parameter/actual list");

                return (Expression) this.Visit(actuals[argIndex]);
            }

            if (parameter.DeclaringMethod == this.targetMethod)
            {
                // important lower case. Upper case is Duplicator's target method
                return parameter;
            }

            return base.VisitParameter(parameter);
        }

        public override Expression VisitLocal(Local local)
        {
            if (local == null) return null;

            if (this.localsInActuals[local.UniqueKey] != null) return local; // don't copy

            return base.VisitLocal(local);
        }

        public override Expression VisitUnaryExpression(UnaryExpression unaryExpression)
        {
            var temp = base.VisitUnaryExpression(unaryExpression);

            if (temp.NodeType == NodeType.AddressOf)
            {
                var unary = temp as UnaryExpression;
                if (unary != null)
                {
                    if (!IsAddressoffable(unary.Operand.NodeType))
                    {
                        var newVar = new Local(unary.Operand.Type);
                        var sl = new StatementList();
                        sl.Add(new AssignmentStatement(newVar, unary.Operand));

                        unary.Operand = newVar;
                        sl.Add(new ExpressionStatement(unary));

                        return new BlockExpression(new Block(sl));
                    }
                }
            }

            return temp;
        }

        private static bool IsAddressoffable(NodeType kind)
        {
            switch (kind)
            {
                case NodeType.Indexer:
                case NodeType.MemberBinding:
                case NodeType.Local:
                case NodeType.Parameter:
                    return true;
            }

            return false;
        }

        public override void SafeAddMember(TypeNode targetType, Member duplicatedMember, Member originalMember)
        {
            // don't add these contracts to target assembly yet, as we will copy them again and add them then

            //base.SafeAddMember(targetType, duplicatedMember, originalMember);
        }

        // Special case substitution on UserMessage of contracts. Regular visitor does not visit this field.

        public override RequiresPlain VisitRequiresPlain(RequiresPlain plain)
        {
            var result = base.VisitRequiresPlain(plain);

            result.UserMessage = (Expression) Visit(result.UserMessage);

            return result;
        }

        public override EnsuresExceptional VisitEnsuresExceptional(EnsuresExceptional exceptional)
        {
            var result = base.VisitEnsuresExceptional(exceptional);

            result.UserMessage = (Expression) Visit(result.UserMessage);

            return result;
        }

        public override EnsuresNormal VisitEnsuresNormal(EnsuresNormal normal)
        {
            var result = base.VisitEnsuresNormal(normal);

            result.UserMessage = (Expression) Visit(result.UserMessage);

            return result;
        }
    }
}