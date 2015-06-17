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
