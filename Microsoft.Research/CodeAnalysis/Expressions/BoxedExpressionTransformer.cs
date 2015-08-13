// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Research.DataStructures;
using System.Diagnostics.Contracts;

namespace Microsoft.Research.CodeAnalysis
{
    public struct Void { }

    abstract public class BoxedExpressionTransformer<ExtraInfo>
    {
        protected ExtraInfo Info { get; private set; }
        protected Func<object, FList<PathElement>> PathFetcher;

        virtual public BoxedExpression Visit(BoxedExpression exp, ExtraInfo info, Func<object, FList<PathElement>> PathFetcher = null)
        {
            Contract.Requires(exp != null);

            this.Info = info;
            this.PathFetcher = GetPathFetcher(PathFetcher);
            return this.Visit(exp);
        }

        protected virtual Func<object, FList<PathElement>> GetPathFetcher(Func<object, FList<PathElement>> PathFetcher)
        {
            return PathFetcher;
        }

        protected BoxedExpression Visit(BoxedExpression exp)
        {
            Contract.Requires(exp != null);

            if (exp.IsVariable)
            {
                return this.Variable(exp, exp.UnderlyingVariable, exp.AccessPath);
            }
            if (exp.IsNull)
            {
                return this.Null(exp);
            }
            if (exp.IsConstant)
            {
                return this.Constant(exp, exp.ConstantType, exp.Constant);
            }
            BinaryOperator bop;
            BoxedExpression left, right;
            if (exp.IsBinaryExpression(out bop, out left, out right))
            {
                return this.Binary(exp, bop, left, right);
            }
            UnaryOperator uop;
            if (exp.IsUnaryExpression(out uop, out left))
            {
                return this.Unary(exp, uop, left);
            }
            if (exp.IsSizeOf)
            {
                return this.SizeOf(exp);
            }
            object type;
            if (exp.IsIsInstExpression(out left, out type))
            {
                return this.IsInst(exp, type, left);
            }
            BoxedExpression array, index;
            if (exp.IsArrayIndexExpression(out array, out index, out type))
            {
                return this.ArrayIndex(exp, type, array, index);
            }
            if (exp.IsResult)
            {
                return this.Result(exp);
            }
            // exp.IsOld ??
            // exp.IsValueAtReturn??
            // exp.IsAssert ??
            // exp.IsAssume ??
            // exp.IsStatementSequence ??
            bool isForAll;
            BoxedExpression boundedVar, low, upp, body;
            if (exp.IsQuantifiedExpression(out isForAll, out boundedVar, out low, out upp, out body))
            {
                if (isForAll)
                {
                    return this.ForAll(exp, boundedVar, low, upp, body);
                }
                else
                {
                    return this.Exists(exp, boundedVar, low, upp, body);
                }
            }

            throw new NotImplementedException("Missing case !!!!");
        }

        #region Virtual methods
        protected virtual BoxedExpression Null(BoxedExpression original)
        {
            return original;
        }

        protected virtual BoxedExpression Variable(BoxedExpression original, object var, PathElement[] path)
        {
            Contract.Requires(original != null);
            Contract.Requires(original.UnderlyingVariable == var);

            if (path != null || this.PathFetcher == null)
            {
                return original;
            }
            else
            {
                Contract.Assert(this.PathFetcher != null);
                var newPath = this.PathFetcher(original.UnderlyingVariable);
                return newPath != null ? BoxedExpression.Var(original.UnderlyingVariable, newPath) : null;
            }
        }

        protected virtual BoxedExpression Constant(BoxedExpression original, object type, object value)
        {
            return original;
        }

        protected virtual BoxedExpression Binary(BoxedExpression original, BinaryOperator binaryOperator, BoxedExpression left, BoxedExpression right)
        {
            Contract.Requires(original != null);
            Contract.Requires(left != null);
            Contract.Requires(right != null);

            var recLeft = this.Visit(left);
            if (recLeft == null)
                return recLeft;

            var recRight = this.Visit(right);
            if (recRight == null)
                return recRight;

            return BoxedExpression.Binary(binaryOperator, recLeft, recRight, original.UnderlyingVariable);
        }

        protected virtual BoxedExpression Unary(BoxedExpression original, UnaryOperator unaryOperator, BoxedExpression argument)
        {
            Contract.Requires(original != null);
            Contract.Requires(argument != null);

            var recArgument = this.Visit(argument);
            if (recArgument == null)
                return null;

            return BoxedExpression.Unary(unaryOperator, recArgument);
        }

        protected virtual BoxedExpression SizeOf(BoxedExpression original)
        {
            return original;
        }

        protected virtual BoxedExpression IsInst(BoxedExpression original, object type, BoxedExpression argument)
        {
            return original;
        }

        protected virtual BoxedExpression ArrayIndex<Typ>(BoxedExpression original, Typ type, BoxedExpression array, BoxedExpression index)
        {
            Contract.Requires(original != null);
            Contract.Requires(array != null);
            Contract.Requires(index != null);

            var arrayRec = this.Visit(array);
            if (arrayRec == null)
                return null;

            var indexRec = this.Visit(index);
            if (indexRec == null)
                return null;

            return BoxedExpression.ArrayIndex(arrayRec, indexRec, (Typ)type);
        }

        protected virtual BoxedExpression Result(BoxedExpression original)
        {
            return original;
        }

        protected virtual BoxedExpression ForAll(BoxedExpression original, BoxedExpression boundVariable, BoxedExpression lower, BoxedExpression upper, BoxedExpression body)
        {
            if (boundVariable == null || lower == null || upper == null || body == null)
            {
                return null;
            }

            var recboundVariable = this.Visit(boundVariable);
            if (recboundVariable == null)
                return null;

            var recLower = this.Visit(lower);
            if (recLower == null)
                return null;

            var recUpper = this.Visit(upper);
            if (recUpper == null)
                return null;

            var recBody = this.Visit(body);
            if (recBody == null)
                return null;

            return new ForAllIndexedExpression(null, recboundVariable, recLower, recUpper, recBody);
        }

        protected virtual BoxedExpression Exists(BoxedExpression original, BoxedExpression boundVariable, BoxedExpression lower, BoxedExpression upper, BoxedExpression body)
        {
            if (boundVariable == null || lower == null || upper == null || body == null)
            {
                return null;
            }

            var recboundVariable = this.Visit(boundVariable);
            if (recboundVariable == null)
                return null;

            var recLower = this.Visit(lower);
            if (recLower == null)
                return null;

            var recUpper = this.Visit(upper);
            if (recUpper == null)
                return null;

            var recBody = this.Visit(body);
            if (recBody == null)
                return null;

            return new ExistsIndexedExpression(null, recboundVariable, recLower, recUpper, recBody);
        }
        //abstract BoxedExpression Old(BoxedExpression original, object type, BoxedExpression expression);
        //abstract BoxedExpression ValueAtReturn(BoxedExpression original, object type, BoxedExpression expression);
        //abstract BoxedExpression Assert(BoxedExpression original, BoxedExpression condition);
        //abstract BoxedExpression Assume(BoxedExpression original, BoxedExpression condition);
        //abstract BoxedExpression StatementSequence(BoxedExpression original, IIndexable<BoxedExpression> statements);

        #endregion
    }
}