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
using System.Diagnostics.Contracts;
using Microsoft.Research.DataStructures;

namespace Microsoft.Research.CodeAnalysis
{
  public class SimpleSatisfyProcedure<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>
  {
    private readonly IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> MetaDataDecoder;

    public BoxedExpression True
    {
      get
      {
        Contract.Ensures(Contract.Result<BoxedExpression>() != null);

        return BoxedExpression.ConstBool(true, this.MetaDataDecoder);
      }
    }

    public BoxedExpression False
    {
      get
      {
        Contract.Ensures(Contract.Result<BoxedExpression>() != null);

        return BoxedExpression.ConstBool(false, this.MetaDataDecoder);
      }
    }

    public SimpleSatisfyProcedure(IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder)
    {
      Contract.Requires(mdDecoder != null);

      this.MetaDataDecoder = mdDecoder;
    }

    public bool TryMostGeneralSatisfaction(BoxedExpression input, BoxedExpression var, out BoxedExpression result)
    {
      BinaryOperator bop;
      BoxedExpression left, right;
      if (input.IsBinaryExpression(out bop, out left, out right) && bop.IsComparisonBinaryOperator())
      {
        var leftVars = left.Variables();
        var rightVars = right.Variables();

        var vIsLeft = (leftVars.Count == 1 && rightVars.Count == 0);
        var vIsRight = (leftVars.Count == 0 && rightVars.Count == 1);
        if (vIsLeft || vIsRight)
        {
          // do the swap
          if (vIsLeft && bop.TryInvert(out bop))
          {
            var tmp = right;
            right = left;
            left = tmp;
          }

          // Invariant: 
          //    + left has no variables
          //    + right has exactly one variable
          
          // Build a new expression
          var newLeft = left.Sub(ConstantsInBoxedExpression.ConstantsIfLinearExpression(right)).EvaluateConstants(this.MetaDataDecoder);
          if (newLeft != null)
          {
            var newRight = rightVars.PickAnElement();
          }

          switch (bop)
          {
            case BinaryOperator.Ceq:
            case BinaryOperator.Cge:
            case BinaryOperator.Cge_Un:
            case BinaryOperator.Cle:
            case BinaryOperator.Cle_Un:
              {
                result = newLeft.EvaluateConstants(this.MetaDataDecoder);
                return true;
              }

            case BinaryOperator.Cgt:
            case BinaryOperator.Cgt_Un:
              {
                // k > x ==> x == k -1
                result = MinusOne(newLeft);
                return true;
              }

            case BinaryOperator.Clt:
            case BinaryOperator.Clt_Un:
              { // k < x ==> x == k+1
                result = PlusOne(newLeft);
                return true;
              }
          }
        }
        if (leftVars.Count == 1 && rightVars.Count == 1)
        {
          left = leftVars.PickAnElement();
          right = rightVars.PickAnElement();

          if (left.Equals(var) && bop.TryInvert(out bop))
          {
            left = right;
            right = var;            
          }

          // invariant the invariant is on the right
          if (right.Equals(var))
          {
            // TODO: merge with the switch above
            switch (bop)
            {
              case BinaryOperator.Ceq:
              case BinaryOperator.Cge:
              case BinaryOperator.Cge_Un:
              case BinaryOperator.Cle:
              case BinaryOperator.Cle_Un:
                {
                  result = left;
                  return true;
                }

              case BinaryOperator.Cgt:
              case BinaryOperator.Cgt_Un:
                {
                  // k > x ==> x == k -1
                  result = MinusOne(left);
                  return true;
                }

              case BinaryOperator.Clt:
              case BinaryOperator.Clt_Un:
                { // k < x ==> x == k+1
                  result = PlusOne(right);
                  return true;
                }
            }
          }
        }
      }

      result = null;
      return false;
    }

    public BoxedExpression ApplySimpleArithmeticRules(BoxedExpression exp)
    {
      Contract.Requires(exp != null);
      Contract.Ensures(Contract.Result<BoxedExpression>() != null);

      BinaryOperator bop;
      BoxedExpression left, right;
      if (exp.IsBinaryExpression(out bop, out left, out right))
      {
        BinaryOperator bopRight;
        BoxedExpression right1, right2;
        if (right.IsBinaryExpression(out bopRight, out right1, out right2))
        {
          // (a  bop  k * a) or (a bop a * k) ==> (0 bop (k-1) * a)
          if (bopRight == BinaryOperator.Mul)
          {
            int k;
            BoxedExpression other;
            if (right1.IsConstantInt(out k))
            {
              other = right2;
            }
            else if (right2.IsConstantInt(out k))
            {
              other = right1;
            }
            else
            {
              goto done;
            }

            right1 = right2 = null; // kill them, to avoid errors

            // other appears on the left and the right
            if (other.Equals(left))
            {
              var newRight = k - 1 != 1 ? BoxedExpression.Binary(BinaryOperator.Mul, BoxedExpression.Const(k - 1, this.MetaDataDecoder.System_Int32, this.MetaDataDecoder), other) : other;

              return BoxedExpression.Binary(bop,
                BoxedExpression.Const(0, this.MetaDataDecoder.System_Int32, this.MetaDataDecoder), newRight);
            }
          }
        }
        BinaryOperator bopLeft;
        BoxedExpression left1, left2;
        if (left.IsBinaryExpression(out bopLeft, out left1, out left2))
        {
          // (a / k bop  a)  ==> (0 bop (k-1) * a)
          if (bopLeft == BinaryOperator.Div)
          {
            int k;
            if (!left2.IsConstantInt(out k))
            {
              goto done;
            }

            if (left1.Equals(right))
            {
              var newRight = k - 1 != 1 ? BoxedExpression.Binary(BinaryOperator.Mul, BoxedExpression.Const(k - 1, this.MetaDataDecoder.System_Int32, this.MetaDataDecoder), right) : right;

              return BoxedExpression.Binary(bop,
                BoxedExpression.Const(0, this.MetaDataDecoder.System_Int32, this.MetaDataDecoder), newRight);
            }
          }
        }
      }

        // TODO: add new cases as needed

    done:
      ;
      return exp;

    }

    private BoxedExpression MinusOne(BoxedExpression exp)
    {
      Contract.Requires(exp != null);

      return BoxedExpression.Binary(BinaryOperator.Sub, exp, BoxedExpression.Const(1, this.MetaDataDecoder.System_Int32, this.MetaDataDecoder)).EvaluateConstants(this.MetaDataDecoder);
    }

    private BoxedExpression PlusOne(BoxedExpression exp)
    {
      Contract.Requires(exp != null);

      return BoxedExpression.Binary(BinaryOperator.Add, exp, BoxedExpression.Const(1, this.MetaDataDecoder.System_Int32, this.MetaDataDecoder)).EvaluateConstants(this.MetaDataDecoder);
    }

#pragma warning disable 0693

    class ConstantsInBoxedExpression : IBoxedExpressionVisitor
    {

      public static Set<BoxedExpression> ConstantsIfLinearExpression(BoxedExpression exp)
      {
        Contract.Requires(exp != null);
        var visitor = new ConstantsInBoxedExpression();

        exp.Dispatch(visitor);

        Set<BoxedExpression> result;

        if (!visitor.TryGetConstants(out result))
        {
          result = null;
        }

        return result;
      }

      [ContractInvariantMethod]
      private void ObjectInvariant()
      {
        Contract.Invariant(this.Constants != null);
      }

      public bool TryGetConstants(out Set<BoxedExpression> constants)
      {
        constants = this.Constants;
        return !this.fail;
      }

      readonly private Set<BoxedExpression> Constants;
      private bool fail;
      private bool isPositive;

      public ConstantsInBoxedExpression()
      {
        this.Constants = new Set<BoxedExpression>();
        this.fail = false;
        this.isPositive = true;
      }

      #region IBoxedExpressionVisitor Members

      public void Variable(object var, PathElement[] path, BoxedExpression parent)
      {
      }

      public void Constant<Type>(Type type, object value, BoxedExpression parent)
      {
        if (this.isPositive)
        {
          this.Constants.Add(parent);
        }
        else
        {
          this.Constants.Add(BoxedExpression.Unary(UnaryOperator.Neg, parent));
        }
      }

      public void Binary(BinaryOperator binaryOperator, BoxedExpression left, BoxedExpression right, BoxedExpression parent)
      {
        switch (binaryOperator)
        {
          case BinaryOperator.Add:
            {
              left.Dispatch(this);
              right.Dispatch(this);
            }
            break;
          case BinaryOperator.Sub:
            {
              left.Dispatch(this);

              this.isPositive = !this.isPositive;
              right.Dispatch(this);
              this.isPositive = !this.isPositive;
            }
            break;

          default:
            {
              this.fail = true;
              return;
            }
        }
      }

      public void Unary(UnaryOperator unaryOperator, BoxedExpression argument, BoxedExpression parent)
      {
        argument.Dispatch(this);
      }

      public void SizeOf<Type>(Type type, int sizeAsConstant, BoxedExpression parent)
      {
      }

      public void IsInst<Type>(Type type, BoxedExpression argument, BoxedExpression parent)
      {
        argument.Dispatch(this);
      }

      public void ArrayIndex<Type>(Type type, BoxedExpression array, BoxedExpression index, BoxedExpression parent)
      {
        array.Dispatch(this);
        index.Dispatch(this);
      }

      public void Result<Type>(Type type, BoxedExpression parent)
      {
      }

      public void Old<Type>(Type type, BoxedExpression expression, BoxedExpression parent)
      {
        expression.Dispatch(this);
      }

      public void ValueAtReturn<Type>(Type type, BoxedExpression expression, BoxedExpression parent)
      {
        expression.Dispatch(this);
      }

      public void Assert(BoxedExpression condition, BoxedExpression parent)
      {
        condition.Dispatch(this);
      }

      public void Assume(BoxedExpression condition, BoxedExpression parent)
      {
        condition.Dispatch(this);
      }

      public void StatementSequence(IIndexable<BoxedExpression> statements, BoxedExpression parent)
      {
        foreach (var statement in statements.Enumerate())
        {
          statement.Dispatch(this);
        }
      }

      public void ForAll(BoxedExpression boundVariable, BoxedExpression lower, BoxedExpression upper, BoxedExpression body, BoxedExpression parent)
      {
        boundVariable.Dispatch(this);
        lower.Dispatch(this);
        upper.Dispatch(this);
        body.Dispatch(this);
      }

      #endregion
    }

#pragma warning restore 0693


  }
}
