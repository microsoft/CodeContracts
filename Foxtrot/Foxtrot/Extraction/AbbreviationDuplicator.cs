// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

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
        private ExpressionList _actuals;
        private Expression _targetObject;
        private Method _abbreviation;
        private TrivialHashtable _localsInActuals;

        public AbbreviationDuplicator(Method sourceMethod, Method targetMethod, ContractNodes contractNodes,
            Method abbreviation, Expression targetObject, ExpressionList actuals)
            : base(targetMethod.DeclaringType.DeclaringModule, sourceMethod, targetMethod, contractNodes, false)
        {
            _targetObject = targetObject;
            _abbreviation = abbreviation;
            _actuals = actuals;

            _localsInActuals = new TrivialHashtable();

            PopulateLocalsInActuals();
        }

        private void PopulateLocalsInActuals()
        {
            var finder = new FindLocalsInActuals(_localsInActuals);

            finder.VisitExpression(_targetObject);
            finder.VisitExpressionList(_actuals);
        }

        private class FindLocalsInActuals : Inspector
        {
            private TrivialHashtable _localsFound;

            public FindLocalsInActuals(TrivialHashtable target)
            {
                _localsFound = target;
            }

            public override void VisitLocal(Local local)
            {
                if (local == null) return;

                _localsFound[local.UniqueKey] = local;
            }
        }

        public override Expression VisitThis(This This)
        {
            if (This.DeclaringMethod == _abbreviation)
                return (Expression)this.Visit(_targetObject);

            if (This.DeclaringMethod == this.targetMethod)
                // important lower case. Upper case is Duplicator's target method
                return This;

            return base.VisitThis(This);
        }

        public override Expression VisitParameter(Parameter parameter)
        {
            if (parameter == null) return null;

            if (parameter.DeclaringMethod == _abbreviation)
            {
                var argIndex = parameter.ParameterListIndex;

                if (argIndex >= _actuals.Count) throw new InvalidOperationException("bad parameter/actual list");

                return (Expression)this.Visit(_actuals[argIndex]);
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

            if (_localsInActuals[local.UniqueKey] != null) return local; // don't copy

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

            result.UserMessage = (Expression)Visit(result.UserMessage);

            return result;
        }

        public override EnsuresExceptional VisitEnsuresExceptional(EnsuresExceptional exceptional)
        {
            var result = base.VisitEnsuresExceptional(exceptional);

            result.UserMessage = (Expression)Visit(result.UserMessage);

            return result;
        }

        public override EnsuresNormal VisitEnsuresNormal(EnsuresNormal normal)
        {
            var result = base.VisitEnsuresNormal(normal);

            result.UserMessage = (Expression)Visit(result.UserMessage);

            return result;
        }
    }
}