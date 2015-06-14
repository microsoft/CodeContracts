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
using Microsoft.Research.AbstractDomains;

namespace Microsoft.Research.CodeAnalysis
{
  class BooleanExpressionsSimplificator
  {
    public static BoxedExpression Simplify<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions>
      (
      BoxedExpression exp,
      INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> oracle,
      IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions> mdriver
      )
      where Expression : IEquatable<Expression>
      where Variable : IEquatable<Variable>
      where LogOptions : IFrameworkLogOptions
      where Type : IEquatable<Type>
    {
      BoxedExpression simplified;
      SimplifyInternal(exp, oracle, mdriver, out simplified);

      return simplified;
    }

    private static ProofOutcome SimplifyInternal<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions>
      (
      BoxedExpression exp,
      INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> oracle,
      IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions> mdriver,
      out BoxedExpression simplified
      )
      where Expression : IEquatable<Expression>
      where Variable : IEquatable<Variable>
      where LogOptions : IFrameworkLogOptions
      where Type : IEquatable<Type>
    {
      BinaryOperator bop;
      BoxedExpression left, right;
      if (exp.IsBinaryExpression(out bop, out left, out right))
      {
        BoxedExpression simplifiedLeft, simplifiedRight;
        switch (bop)
        {
          case BinaryOperator.LogicalAnd:
            {
              var tLeft = SimplifyInternal(left, oracle, mdriver, out simplifiedLeft);
              if (tLeft == ProofOutcome.False)
              {
                simplified = BoxedExpression.Const(0, mdriver.MetaDataDecoder.System_Boolean, mdriver.MetaDataDecoder);
                return ProofOutcome.False;
              }

              var tRight = SimplifyInternal(right, oracle, mdriver, out simplifiedRight);
              if (tRight == ProofOutcome.False)
              {
                simplified = BoxedExpression.Const(0, mdriver.MetaDataDecoder.System_Boolean, mdriver.MetaDataDecoder);
                return ProofOutcome.False;
              }

              if (tLeft == ProofOutcome.True)
              {
                simplified = simplifiedRight;
                return tRight;
              }

              if (tRight == ProofOutcome.True)
              {
                simplified = simplifiedLeft;
                return tLeft;
              }
              
              simplified = BoxedExpression.BinaryLogicalAnd(simplifiedLeft, simplifiedRight);
              return ProofOutcome.Top;
            }

          case BinaryOperator.LogicalOr:
            {
              var tLeft = SimplifyInternal(left, oracle, mdriver, out simplifiedLeft);
              if (tLeft == ProofOutcome.True)
              {
                simplified = BoxedExpression.Const(1, mdriver.MetaDataDecoder.System_Boolean, mdriver.MetaDataDecoder);
                return ProofOutcome.True;
              }

              var tRight = SimplifyInternal(right, oracle, mdriver, out simplifiedRight);
              if (tRight == ProofOutcome.True)
              {
                simplified = BoxedExpression.Const(1, mdriver.MetaDataDecoder.System_Boolean, mdriver.MetaDataDecoder);
                return ProofOutcome.True;
              }

              if (tLeft == ProofOutcome.False)
              {
                simplified = simplifiedRight;
                return tRight;
              }

              if (tRight == ProofOutcome.False)
              {
                simplified = simplifiedLeft;
                return tLeft;
              }

              simplified = BoxedExpression.BinaryLogicalOr(simplifiedLeft, simplifiedRight);
              return ProofOutcome.Top;
            }

          case BinaryOperator.Ceq:
            {
              var r = oracle.CheckIfEqual(left, right);

              return ProcessOutcome(r, exp, mdriver, out simplified);
            }

          case BinaryOperator.Cge:
          case BinaryOperator.Cge_Un:
            {
              var r = oracle.CheckIfLessEqualThan(right, left);

              return ProcessOutcome(r, exp, mdriver, out simplified);
            }

          case BinaryOperator.Cgt:
          case BinaryOperator.Cgt_Un:
            {
              var r = oracle.CheckIfLessThan(right, left);

              return ProcessOutcome(r, exp, mdriver, out simplified);
            }

          case BinaryOperator.Cle:
          case BinaryOperator.Cle_Un:
            {
              var r = oracle.CheckIfLessEqualThan(left, right);

              return ProcessOutcome(r, exp, mdriver, out simplified);
            }

          case BinaryOperator.Clt:
          case BinaryOperator.Clt_Un:
            {
              var r = oracle.CheckIfLessThan(left, right);

              return ProcessOutcome(r, exp, mdriver, out simplified);
            }

          case BinaryOperator.Cne_Un:
            {
              var r1 = oracle.CheckIfLessThan(left, right);

              if (r1.IsNormal())
              {
                simplified = BoxedExpression.Const(1, mdriver.MetaDataDecoder.System_Boolean, mdriver.MetaDataDecoder);
                return ProofOutcome.True;
              }

              var r2 = oracle.CheckIfLessThan(right, left);
              if (r2.IsNormal())
              {
                simplified = BoxedExpression.Const(1, mdriver.MetaDataDecoder.System_Boolean, mdriver.MetaDataDecoder);
                return ProofOutcome.True;
              }

              simplified = exp;
              return ProofOutcome.Top;
            }
        }
      }

      if (exp.IsUnary)
      {
        var r = oracle.CheckIfHolds(exp);
        if (r.IsTrue())
        {
          simplified = BoxedExpression.Const(1, mdriver.MetaDataDecoder.System_Boolean, mdriver.MetaDataDecoder);
          return ProofOutcome.True;
        }
        if (r.IsFalse())
        {
          simplified = BoxedExpression.Const(0, mdriver.MetaDataDecoder.System_Boolean, mdriver.MetaDataDecoder);
          return ProofOutcome.False;
        }
      }

      simplified = exp;
      return ProofOutcome.Top;
    }

    private static ProofOutcome ProcessOutcome<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions>
      (
      FlatAbstractDomain<bool> result,
      BoxedExpression exp,
      IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions> mdriver,
      out BoxedExpression simplified
      )
      where Expression : IEquatable<Expression>
      where Variable : IEquatable<Variable>
      where LogOptions : IFrameworkLogOptions
      where Type : IEquatable<Type>
    {
      if (result.IsTrue())
      {
        simplified = BoxedExpression.Const(1, mdriver.MetaDataDecoder.System_Boolean, mdriver.MetaDataDecoder);
        return ProofOutcome.True;
      }
      if (result.IsFalse())
      {
        simplified = BoxedExpression.Const(0, mdriver.MetaDataDecoder.System_Boolean, mdriver.MetaDataDecoder);
        return ProofOutcome.False;
      }

      simplified = exp;
      return ProofOutcome.Top;
    }

  }
}
