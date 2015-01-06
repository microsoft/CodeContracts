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
  public class FactQueryForOverflow<Variable>
     : IFactQueryForOverflow<BoxedExpression>
  {
    #region Private state

    private readonly IFactQuery<BoxedExpression, Variable> FactQuery;

    #endregion

    #region Constructor

    public FactQueryForOverflow(IFactQuery<BoxedExpression, Variable> facts)
    {
      Contract.Requires(facts != null);

      this.FactQuery = facts;
    }

    #endregion

    #region Implementation of the interface

    public bool CanOverflow(APC pc, BoxedExpression exp)
    {
      var visitor = new CanOverflowVisitor(pc, this.FactQuery);
      exp.Dispatch(visitor);

      return visitor.CanOverflow;
    }

    public bool CanUnderflow(APC pc, BoxedExpression exp)
    {
      var visitor = new CanUndeflowVisitor(pc, this.FactQuery);
      exp.Dispatch(visitor);

      return visitor.CanUnderflow;
    }

    #endregion

    #region Visitors

    abstract class OverflowVisitorBase : IBoxedExpressionVisitor
    {
      #region State
      protected bool Overflow { get; set; }

      protected readonly APC pc;
      protected readonly IFactQuery<BoxedExpression, Variable> Facts;
      #endregion

      #region Constructor
      public OverflowVisitorBase(APC pc, IFactQuery<BoxedExpression, Variable> facts)
      {
        this.Overflow = true;
        this.pc = pc;
        this.Facts = facts;
      }
      #endregion

      #region Special cases

      abstract public void Binary(BinaryOperator binaryOperator, BoxedExpression left, BoxedExpression right, BoxedExpression parent);

      abstract public void Unary(UnaryOperator unaryOperator, BoxedExpression argument, BoxedExpression parent);

      #endregion

      #region Common cases

      public void Variable(object var, PathElement[] path, BoxedExpression parent)
      {
        this.Overflow = false;
      }

      public void Constant<Type>(Type type, object value, BoxedExpression parent)
      {
        this.Overflow = false;
      }

      public void SizeOf<Type>(Type type, int sizeAsConstant, BoxedExpression parent)
      {
        this.Overflow = false;
      }

      public void IsInst<Type>(Type type, BoxedExpression argument, BoxedExpression parent)
      {
        argument.Dispatch(this);
      }

      public void ArrayIndex<Type>(Type type, BoxedExpression array, BoxedExpression index, BoxedExpression parent)
      {
        index.Dispatch(this);
      }

      public void Result<Type>(Type type, BoxedExpression parent)
      {
        this.Overflow = false;
      }

      public void Old<Type>(Type type, BoxedExpression expression, BoxedExpression parent)
      {
        // should we use the entry point PC???
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
        for (var i = 0; i < statements.Count; i++)
        {
          statements[i].Dispatch(this);
          if (this.Overflow == true)
            return;
        }
      }

      public void ForAll(BoxedExpression boundVariable, BoxedExpression lower, BoxedExpression upper, BoxedExpression body, BoxedExpression parent)
      {
        boundVariable.Dispatch(this);
        if (this.Overflow == true)
        {
          return;
        }

        lower.Dispatch(this);
        if (this.Overflow == true)
        {
          return;
        }

        upper.Dispatch(this);
        if (this.Overflow == true)
        {
          return;
        }

        body.Dispatch(this);
        if (this.Overflow == true)
        {
          return;
        }
      }
      #endregion
    }

    class CanOverflowVisitor : OverflowVisitorBase
    {
      public bool CanOverflow { get { return this.Overflow; } }

      public CanOverflowVisitor(APC pc, IFactQuery<BoxedExpression, Variable> facts)
        : base(pc, facts)
      {
      }


      override public void Binary(BinaryOperator binaryOperator, BoxedExpression left, BoxedExpression right, BoxedExpression parent)
      {
        left.Dispatch(this);
        if (this.CanOverflow)
          return;
        right.Dispatch(this);
        if (this.CanOverflow)
          return;

        switch (binaryOperator)
        {
          case BinaryOperator.Add:
          case BinaryOperator.Add_Ovf:
          case BinaryOperator.Add_Ovf_Un:
            {
              // different signs or not negative ==> no overflow
              int leftSign, rightSign;
              if (Facts.TrySign(pc, left, out leftSign) && Facts.TrySign(pc, right, out rightSign))
              {
                if ((leftSign <= 0 && rightSign <= 0) || leftSign * rightSign == -1)
                {
                  this.Overflow = false;
                  return;
                }
              }

              // TODO: improve using upper bounds

              this.Overflow = true;
              return;
            }

          case BinaryOperator.Div:
          case BinaryOperator.Div_Un:
          case BinaryOperator.Rem:
          case BinaryOperator.Rem_Un:
            {
              // TODO: improve using upper bounds

              if (Facts.IsNonZero(pc, right) == ProofOutcome.True)
              {
                this.Overflow = false;
              }

              this.Overflow = true;
              return;
            }
          case BinaryOperator.Mul:
          case BinaryOperator.Mul_Ovf:
          case BinaryOperator.Mul_Ovf_Un:
            {
              this.Overflow = true;
              return;
            }

          case BinaryOperator.Sub:
          case BinaryOperator.Sub_Ovf:
          case BinaryOperator.Sub_Ovf_Un:
            {
              int rightSign;
              if (Facts.TrySign(pc, right, out rightSign) && rightSign >= 0)
              {
                this.Overflow = false;
                return;
              }

              this.Overflow = true;
              return;
            }

          case BinaryOperator.And:
          case BinaryOperator.Ceq:
          case BinaryOperator.Cge:
          case BinaryOperator.Cge_Un:
          case BinaryOperator.Cgt:
          case BinaryOperator.Cgt_Un:
          case BinaryOperator.Cle:
          case BinaryOperator.Cle_Un:
          case BinaryOperator.Clt:
          case BinaryOperator.Clt_Un:
          case BinaryOperator.Cne_Un:
          case BinaryOperator.Cobjeq:
          case BinaryOperator.LogicalAnd:
          case BinaryOperator.LogicalOr:
          case BinaryOperator.Or:
          case BinaryOperator.Shl:
          case BinaryOperator.Shr:
          case BinaryOperator.Shr_Un:
          case BinaryOperator.Xor:
            {
              this.Overflow = false;
              return;
            }

          default:
            {
              this.Overflow = true;
              return;
            }
        }
      }

      override public void Unary(UnaryOperator unaryOperator, BoxedExpression argument, BoxedExpression parent)
      {
        argument.Dispatch(this);
      }

    }

    class CanUndeflowVisitor : OverflowVisitorBase
    {
      public bool CanUnderflow { get { return this.Overflow; } }

      public CanUndeflowVisitor(APC pc, IFactQuery<BoxedExpression, Variable> facts)
        : base(pc, facts)
      {
      }

      public override void Binary(BinaryOperator binaryOperator, BoxedExpression left, BoxedExpression right, BoxedExpression parent)
      {
        left.Dispatch(this);
        if (this.Overflow)
          return;
        right.Dispatch(this);
        if (this.Overflow)
          return;

        switch (binaryOperator)
        {
          case BinaryOperator.Add:
          case BinaryOperator.Add_Ovf:
          case BinaryOperator.Add_Ovf_Un:
            {
              // different signs, or both positive ==> no overflow
              int leftSign, rightSign;
              if (Facts.TrySign(pc, left, out leftSign) && Facts.TrySign(pc, right, out rightSign))
              {
                if (leftSign >= 0 || rightSign >= 0)
                {
                  this.Overflow = false;
                  return;
                }
              }

              // TODO: improve using upper bounds
              this.Overflow = true;
              return;
            }

          case BinaryOperator.Div:
          case BinaryOperator.Div_Un:
          case BinaryOperator.Rem:
          case BinaryOperator.Rem_Un:
            {
              // TODO: improve using upper bounds

              if (Facts.IsNonZero(pc, right) == ProofOutcome.True)
              {
                this.Overflow = false;
              }

              this.Overflow = true;
              return;
            }
          case BinaryOperator.Mul:
          case BinaryOperator.Mul_Ovf:
          case BinaryOperator.Mul_Ovf_Un:
            {
              // TODO: improve using upper bounds

              this.Overflow = true;
              return;
            }

          case BinaryOperator.Sub:
          case BinaryOperator.Sub_Ovf:
          case BinaryOperator.Sub_Ovf_Un:
            {
              int leftSign, rightSign;
              // if left is non-negative or right is non-positive ==> no undeflow
              if (Facts.TrySign(pc, left, out leftSign) && Facts.TrySign(pc, right, out rightSign))
              {
                if (leftSign >= 0 || rightSign <= 0)
                {
                  this.Overflow = false;
                  return;
                }
              }

              this.Overflow = true;
              return;
            }

          case BinaryOperator.And:
          case BinaryOperator.Ceq:
          case BinaryOperator.Cge:
          case BinaryOperator.Cge_Un:
          case BinaryOperator.Cgt:
          case BinaryOperator.Cgt_Un:
          case BinaryOperator.Cle:
          case BinaryOperator.Cle_Un:
          case BinaryOperator.Clt:
          case BinaryOperator.Clt_Un:
          case BinaryOperator.Cne_Un:
          case BinaryOperator.Cobjeq:
          case BinaryOperator.LogicalAnd:
          case BinaryOperator.LogicalOr:
          case BinaryOperator.Or:
          case BinaryOperator.Shl:
          case BinaryOperator.Shr:
          case BinaryOperator.Shr_Un:
          case BinaryOperator.Xor:
            {
              this.Overflow = false;
              return;
            }

          default:
            {
              this.Overflow = true;
              return;
            }
        }
      }

      public override void Unary(UnaryOperator unaryOperator, BoxedExpression argument, BoxedExpression parent)
      {
        // TODO: check for negation of minValue

        argument.Dispatch(this);
      }

    }

      #endregion
  }
}
