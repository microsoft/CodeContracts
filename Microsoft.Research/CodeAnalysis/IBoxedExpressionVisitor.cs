// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Research.DataStructures;

namespace Microsoft.Research.CodeAnalysis
{
    public interface IBoxedExpressionVisitor
    {
        void Variable(object var, PathElement[] path, BoxedExpression original);
        void Constant<Type>(Type type, object value, BoxedExpression original);
        void Binary(BinaryOperator binaryOperator, BoxedExpression left, BoxedExpression right, BoxedExpression original);
        void Unary(UnaryOperator unaryOperator, BoxedExpression argument, BoxedExpression original);
        void SizeOf<Type>(Type type, int sizeAsConstant, BoxedExpression original);
        void IsInst<Type>(Type type, BoxedExpression argument, BoxedExpression original);
        void ArrayIndex<Type>(Type type, BoxedExpression array, BoxedExpression index, BoxedExpression original);

        void Result<Type>(Type type, BoxedExpression original);
        void Old<Type>(Type type, BoxedExpression expression, BoxedExpression original);
        void ValueAtReturn<Type>(Type type, BoxedExpression expression, BoxedExpression original);


        void Assert(BoxedExpression condition, BoxedExpression original);

        void Assume(BoxedExpression condition, BoxedExpression original);

        void StatementSequence(IIndexable<BoxedExpression> statements, BoxedExpression original);

        void ForAll(BoxedExpression boundVariable, BoxedExpression lower, BoxedExpression upper, BoxedExpression body, BoxedExpression original);
    }
}
