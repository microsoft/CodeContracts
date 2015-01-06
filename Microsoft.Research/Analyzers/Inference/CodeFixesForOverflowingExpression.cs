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

namespace Microsoft.Research.CodeAnalysis.Inference
{
  using Provenance = IEnumerable<ProofObligation>;

  public class CodeFixesForOverflowingExpression<Var> : IBoxedExpressionVisitor
  {
    #region State

    private enum State  {CanOverflow, DoesNotOverflow}

    private readonly APC PC;
    private readonly IFactQuery<BoxedExpression, Var> Facts;
    private readonly IFactQueryForOverflow<BoxedExpression> OverflowOracle;
    private readonly Dictionary<BoxedExpression, State> ExpressionTags;
    private readonly LazyEval<BoxedExpression> ZeroExp;

    private BoxedExpression PartialResult;

    #endregion

    public CodeFixesForOverflowingExpression(APC pc, IFactQuery<BoxedExpression, Var> facts, LazyEval<BoxedExpression> ZeroExp)
    {
      Contract.Requires(facts != null);
      Contract.Requires(ZeroExp != null);

      this.PC = pc;
      this.Facts = facts;
      this.OverflowOracle = new FactQueryForOverflow<Var>(facts);
      this.ExpressionTags = new Dictionary<BoxedExpression,State>();
      this.ZeroExp = ZeroExp;
      this.PartialResult = null;
    }

    public bool TryRefactorExpression(BoxedExpression exp, out BoxedExpression refactoredExp)
    {
      Contract.Requires(exp != null);

      ExpressionTags.Clear();

      ExpressionTags[exp] = State.CanOverflow;

      exp.Dispatch(this);

      return ((refactoredExp = this.PartialResult) != null) && (!exp.Equals(refactoredExp));
    }


    #region Helper

    private bool NotifyOkAndSetResult(BoxedExpression exp)
    {
      Contract.Requires(exp != null);
      Contract.Ensures(this.PartialResult == exp);
      Contract.Ensures(Contract.Result<bool>() == true);

      this.PartialResult = exp;
      this.ExpressionTags[exp] = State.DoesNotOverflow;

      return true;
    }

    private bool NotifyNotOk(BoxedExpression exp)
    {
      Contract.Requires(exp != null);
      Contract.Ensures(this.PartialResult == null);
      Contract.Ensures(Contract.Result<bool>() == false);

      this.PartialResult = null;
      this.ExpressionTags[exp] = State.CanOverflow;

      return false;
    }

    #endregion

    #region Cases 

    public void Variable(object var, PathElement[] path, BoxedExpression original)
    {
      NotifyOkAndSetResult(original);
    }

    public void Constant<Type>(Type type, object value, BoxedExpression original)
    {
      NotifyOkAndSetResult(original);
    }

    public void Binary(BinaryOperator bop, BoxedExpression left, BoxedExpression right, BoxedExpression original)
    {
      if (TryComparisonDoNotOverflow(bop, left, right, original))
      {
        return;
      }

      if (TryDividingByConstnatDoNotOverflow(bop, left, right, original))
      {
        return;
      }

      if (TryAdditionOrSubtractionDirectlyNotOverflow(bop, left, right, original))
      {
        return;
      }

      if(TryMovingSubtractionOnComparison(bop, left, right, original))
      {
        return;
      }

      if (TrySemiSumRewriting(bop, left, right, original))
      {
        return;
      }

      if (TryRemovingAddInComparison(bop, left, right, original))
      {
        return;
      }

      if (TryMovingExpressionsAroundComparison(bop, left, right, original))
      {
        return;
      }

      if (TryAlternateAddAndSub(bop, left, right, original))
      {
        return;
      }

      if (TryRemoveWeakInequalities(bop, left, right, original))
      {
        return;
      }

      NotifyNotOk(original);
    }



    public void Unary(UnaryOperator unaryOperator, BoxedExpression argument, BoxedExpression original)
    {
      // TODO: check for negations of minint

      argument.Dispatch(this);
      if (this.PartialResult != null)
      {
        NotifyOkAndSetResult(BoxedExpression.Unary(unaryOperator, this.PartialResult));
      }

      NotifyNotOk(original);
    }

    public void SizeOf<Type>(Type type, int sizeAsConstant, BoxedExpression original)
    {
      NotifyOkAndSetResult(original);
    }

    public void IsInst<Type>(Type type, BoxedExpression argument, BoxedExpression original)
    {
      argument.Dispatch(this);
      if (this.PartialResult != null)
      {
        NotifyOkAndSetResult(ClousotExpression<Type>.MakeIsInst(type, this.PartialResult));
      }
      else
      {
        NotifyNotOk(original);
      }
    }

    public void ArrayIndex<Type>(Type type, BoxedExpression array, BoxedExpression index, BoxedExpression original)
    {
      BoxedExpression newArray, newIndex;
      array.Dispatch(this);
      if (this.PartialResult != null)
      {
        newArray = this.PartialResult;
        index.Dispatch(this);
        if (this.PartialResult != null)
        {
          newIndex = this.PartialResult;
          NotifyOkAndSetResult(new BoxedExpression.ArrayIndexExpression<Type>(newArray, newIndex, type));

          return;
        }
      }

      NotifyNotOk(original);
    }

    public void Result<Type>(Type type, BoxedExpression original)
    {
      NotifyOkAndSetResult(original);
    }

    public void Old<Type>(Type type, BoxedExpression expression, BoxedExpression original)
    {
      original.Dispatch(this);
      if (this.PartialResult != null)
      {
        NotifyOkAndSetResult(ClousotExpression<Type>.Old(this.PartialResult, type));
      }
      else
      {
        NotifyNotOk(original);
      }
    }

    public void ValueAtReturn<Type>(Type type, BoxedExpression expression, BoxedExpression original)
    {
      original.Dispatch(this);
      if (this.PartialResult != null)
      {
        NotifyOkAndSetResult(ClousotExpression<Type>.ValueAtReturn(this.PartialResult, type));
      }
      else
      {
        NotifyNotOk(original);
      }
    }

    public void Assert(BoxedExpression condition, BoxedExpression original)
    {
      original.Dispatch(this);
      if (this.PartialResult != null)
      {
        var contract = (BoxedExpression.ContractExpression)original;
        Provenance provenance = null; // TODO: encode how we got this assertion
        NotifyOkAndSetResult(new BoxedExpression.AssertExpression(this.PartialResult, contract.Tag, contract.Apc, provenance, null));
      }
      else
      {
        NotifyNotOk(original);
      }
    }

    public void Assume(BoxedExpression condition, BoxedExpression original)
    {
      original.Dispatch(this);
      if (this.PartialResult != null)
      {
        var contract = (BoxedExpression.ContractExpression)original;
        Provenance provenance = null; // TODO: encode how we got this assumption
        NotifyOkAndSetResult(new BoxedExpression.AssumeExpression(this.PartialResult, contract.Tag, contract.Apc, provenance, null));
      }
      else
      {
        NotifyNotOk(original);
      }
    }

    public void StatementSequence(DataStructures.IIndexable<BoxedExpression> statements, BoxedExpression original)
    {
      var result = new List<BoxedExpression>();
      for (var i = 0; i < statements.Count; i++)
      {
        statements[i].Dispatch(this);
        if (this.PartialResult == null)
        {
          NotifyNotOk(original);
          return;
        }
        NotifyOkAndSetResult(this.PartialResult);
        result.Add(this.PartialResult);
      }

      NotifyOkAndSetResult(new BoxedExpression.StatementSequence(result));
    }

    public void ForAll(BoxedExpression boundVariable, BoxedExpression lower, BoxedExpression upper, BoxedExpression body, BoxedExpression original)
    {
      BoxedExpression newBoundVariable, newLower, newUpper, newBody;
      
      boundVariable.Dispatch(this);
      if ((newBoundVariable = this.PartialResult) == null)
      {
        NotifyNotOk(original);
        return;
      }
      else
      {
        NotifyOkAndSetResult(newBoundVariable);
      }

      lower.Dispatch(this);
      if ((newLower = this.PartialResult) == null)
      {
        NotifyNotOk(original);
        return;
      }
      else
      {
        NotifyOkAndSetResult(newLower);
      }

      upper.Dispatch(this);
      if ((newUpper = this.PartialResult) == null)
      {
        NotifyNotOk(original);
        return;
      }
      else
      {
        NotifyOkAndSetResult(newUpper);
      }

      body.Dispatch(this);
      if ((newBody = this.PartialResult) == null)
      {
        NotifyNotOk(original);
        return;
      }
      else
      {
        NotifyOkAndSetResult(newBody);
      }

      NotifyOkAndSetResult(new ForAllIndexedExpression(null, newBoundVariable, newLower, newUpper, newBody));
    }
    #endregion

    #region Binary cases

    private bool TryComparisonDoNotOverflow(BinaryOperator bop, BoxedExpression left, BoxedExpression right, BoxedExpression original)
    {
      BoxedExpression newLeft, newRight;
      if (bop.IsComparisonBinaryOperator())
      {
        left.Dispatch(this);
        if ((newLeft = this.PartialResult) != null)
        {
          right.Dispatch(this);
          if ((newRight = this.PartialResult) != null)
          {
            // ok no overflow!
            NotifyOkAndSetResult(BoxedExpression.Binary(bop, newLeft, newRight, original.UnderlyingVariable));
            return true;
          }
        }
      }

      this.PartialResult = null;
      return false;
    }

    private bool TryDividingByConstnatDoNotOverflow(BinaryOperator bop, BoxedExpression left, BoxedExpression right, BoxedExpression original)
    {
      if (bop == BinaryOperator.Div || bop == BinaryOperator.Div_Un || bop == BinaryOperator.Rem || bop == BinaryOperator.Rem_Un)
      {
        int value;
        if (right.IsConstantInt(out value) && value != 0)
        {
          BoxedExpression newLeft;
          left.Dispatch(this);
          if ((newLeft = this.PartialResult) != null)
          {
            this.NotifyOkAndSetResult(BoxedExpression.Binary(bop, newLeft, right, original.UnderlyingVariable));
            return true;
          }
        }
      }

      this.PartialResult = null;
      return false;
    }

    private bool TryAdditionOrSubtractionDirectlyNotOverflow(BinaryOperator bop, BoxedExpression left, BoxedExpression right, BoxedExpression original)
    {
      if (bop == BinaryOperator.Add || bop == BinaryOperator.Add_Ovf || bop == BinaryOperator.Add_Ovf_Un ||
        bop == BinaryOperator.Sub || bop == BinaryOperator.Sub_Ovf || bop == BinaryOperator.Sub_Ovf_Un)
      {
        if (!this.OverflowOracle.CanOverflow(this.PC, original))
        {
          NotifyOkAndSetResult(left);
          NotifyOkAndSetResult(right);
          NotifyOkAndSetResult(original);
          return true;
        }
      }

      this.PartialResult = null;
      return false;
    }

    private bool TryMovingSubtractionOnComparison(BinaryOperator bop, BoxedExpression left, BoxedExpression right, BoxedExpression original)
    {
      int value;
      // left bop 0
      if (bop.IsComparisonBinaryOperator() && right.IsConstantInt(out value) && value == 0)
      {
        BinaryOperator bopLeft;
        BoxedExpression leftleft, leftRight;

        // leftLeft - leftRigth bop 0
        if (left.IsBinaryExpression(out bopLeft, out leftleft, out leftRight) && (bopLeft == BinaryOperator.Sub || bopLeft == BinaryOperator.Sub_Ovf || bopLeft == BinaryOperator.Sub_Ovf_Un))
        {
          BoxedExpression newLeft, newRight;
          leftleft.Dispatch(this);
          if ((newLeft = this.PartialResult) != null)
          {
            NotifyOkAndSetResult(newLeft);
            leftRight.Dispatch(this);
            if ((newRight = this.PartialResult) != null)
            {
              NotifyOkAndSetResult(newRight);
              NotifyOkAndSetResult(BoxedExpression.Binary(bop, newLeft, newRight, original.UnderlyingVariable));
              return true;
            }
          }
        }
      }

      this.PartialResult = null;
      return false;
    }

    private bool TrySemiSumRewriting(BinaryOperator bop, BoxedExpression left, BoxedExpression right, BoxedExpression original)
    {
        
      // TODO: case when bop is negative
      if (bop == BinaryOperator.Div || bop == BinaryOperator.Div_Un) 
      {
        BinaryOperator bopLeft;
        BoxedExpression a, b;
        int value;
        // match (leftleft + leftRight) / 2
        // TODO: make it for even numbers
        if (left.IsBinaryExpression(out bopLeft, out a, out b) &&
         (bopLeft == BinaryOperator.Add || bopLeft == BinaryOperator.Add_Ovf || bopLeft == BinaryOperator.Add_Ovf_Un) &&
          right.IsConstantInt(out value) && value == 2
          )
        {
          BoxedExpression aPrime, bPrime;
          a.Dispatch(this);
          if ((aPrime = this.PartialResult) != null)
          {
            b.Dispatch(this);
            if ((bPrime = this.PartialResult) != null)
            {
              a = null; b = null;
              var RightSubLeft = BoxedExpression.Binary(BinaryOperator.Sub, bPrime, aPrime);
              if (!this.OverflowOracle.CanUnderflow(this.PC, RightSubLeft))
              {
                return NotifyOkAndSetResult(BoxedExpression.Binary(BinaryOperator.Add, aPrime, BoxedExpression.Binary(bop, RightSubLeft, right)));
              }

              var LeftSubRight = BoxedExpression.Binary(BinaryOperator.Sub, aPrime, bPrime);
              if (!this.OverflowOracle.CanUnderflow(this.PC, LeftSubRight))
              {
                return NotifyOkAndSetResult(BoxedExpression.Binary(BinaryOperator.Add, bPrime, BoxedExpression.Binary(bop, LeftSubRight, right)));
              }
            }
          }
        }
      }

      return NotifyNotOk(original);
    }


    private bool TryRemovingAddInComparison(BinaryOperator bop, BoxedExpression left, BoxedExpression c, BoxedExpression original)
    {
      if (bop.IsComparisonBinaryOperator())
      {
        BinaryOperator bopLeft;
        BoxedExpression a, b;
        // match a + b
        if (left.IsBinaryExpression(out bopLeft, out a, out b) && (bopLeft == BinaryOperator.Add || bopLeft == BinaryOperator.Add_Ovf || bopLeft == BinaryOperator.Add_Ovf_Un))
        {
          BoxedExpression aPrime, bPrime, cPrime;
          a.Dispatch(this);
          if ((aPrime = this.PartialResult) != null)
          {
            b.Dispatch(this);
            if ((bPrime = this.PartialResult) != null)
            {
              c.Dispatch(this);
              if ((cPrime = this.PartialResult) != null)
              {
                a = null; b = null; c = null;
                var RightSubB = BoxedExpression.Binary(BinaryOperator.Sub, cPrime, bPrime);
                if (!this.OverflowOracle.CanUnderflow(this.PC, RightSubB))
                {
                  return NotifyOkAndSetResult(BoxedExpression.Binary(bop, aPrime, RightSubB));
                }

                var RightSubA = BoxedExpression.Binary(BinaryOperator.Sub, cPrime, aPrime);
                if (!this.OverflowOracle.CanUnderflow(this.PC, RightSubA))
                {
                  return NotifyOkAndSetResult(BoxedExpression.Binary(bop, bPrime, RightSubA));
                }
              }
            }
          }
        }
      }

      return NotifyNotOk(original);
    }

    private bool TryMovingExpressionsAroundComparison(BinaryOperator bop, BoxedExpression left, BoxedExpression c, BoxedExpression original)
    {
      if (bop.IsComparisonBinaryOperator())
      {
        BinaryOperator bopLeft;
        BoxedExpression a, b;
        if (left.IsBinaryExpression(out bopLeft, out a, out b) && (bopLeft == BinaryOperator.Add || bopLeft == BinaryOperator.Add_Ovf || bopLeft == BinaryOperator.Add_Ovf_Un))
        {
          var aAddbSubc = BoxedExpression.Binary(BinaryOperator.Sub, left, c);
          aAddbSubc.Dispatch(this);
          if (this.PartialResult != null)
          {
            return NotifyOkAndSetResult(BoxedExpression.Binary(bop, aAddbSubc, ZeroExp.Value));
          }
        }
      }

      return NotifyNotOk(original);
    }

    private bool TryAlternateAddAndSub(BinaryOperator bop, BoxedExpression left, BoxedExpression c, BoxedExpression original)
    {
      if (bop == BinaryOperator.Sub || bop == BinaryOperator.Sub_Ovf || bop == BinaryOperator.Sub_Ovf_Un)
      {
        BinaryOperator bopLeft;
        BoxedExpression a, b;
        if (left.IsBinaryExpression(out bopLeft, out a, out b) && (bop == BinaryOperator.Add || bop == BinaryOperator.Add_Ovf || bop == BinaryOperator.Add_Ovf_Un))
        {
          BoxedExpression aPrime, bPrime, cPrime;
          a.Dispatch(this);
          if ((aPrime = this.PartialResult) != null)
          {
            b.Dispatch(this);
            if ((bPrime = this.PartialResult) != null)
            {
              c.Dispatch(this);
              if((cPrime = this.PartialResult) != null)
              {
                // Here we implement a Prolog-like search
                var aSubc = BoxedExpression.Binary(BinaryOperator.Sub, a, c);
                if (!this.OverflowOracle.CanUnderflow(this.PC, aSubc))
                {
                  var candidate = BoxedExpression.Binary(BinaryOperator.Add, aSubc, b);

                  candidate.Dispatch(this);
                  if (this.PartialResult != null)
                  {
                    return NotifyOkAndSetResult(this.PartialResult);
                  }
                }

                aSubc = null; // kill the var to avoid errors

                var bSubc = BoxedExpression.Binary(BinaryOperator.Sub, b, c);
                if(!this.OverflowOracle.CanUnderflow(this.PC, bSubc))
                {
                  var candidate = BoxedExpression.Binary(BinaryOperator.Add, bSubc, a);
                  candidate.Dispatch(this);
                  if (this.PartialResult != null)
                  {
                    return NotifyOkAndSetResult(this.PartialResult);
                  }
                }
              }
            }
          }
        }
      }

      return NotifyNotOk(original);
    }

    /// <summary>
    /// a + 1 \leq b ==> a \lt b
    /// </summary>
    private bool TryRemoveWeakInequalities(BinaryOperator bop, BoxedExpression left, BoxedExpression right, BoxedExpression original)
    {
      if (bop.IsComparisonBinaryOperator())
      {
        if (bop == BinaryOperator.Cle)
        {
          return TryRemoveWeakInequalitiesInternal(bop, left, right);
        }
        if (bop == BinaryOperator.Cge)
        {
          return TryRemoveWeakInequalitiesInternal(BinaryOperator.Cle, right, left);
        }
      }

      return NotifyNotOk(original);
    }

    private bool TryRemoveWeakInequalitiesInternal(BinaryOperator bop, BoxedExpression left, BoxedExpression right)
    {
      if (bop == BinaryOperator.Cle)
      {
        BinaryOperator bopLeft;
        BoxedExpression a, b;
        int value;
        left.Dispatch(this);
        if (left.IsBinaryExpression(out bopLeft, out a, out b) && (bopLeft == BinaryOperator.Add || bopLeft == BinaryOperator.Add_Ovf || bopLeft == BinaryOperator.Add_Ovf_Un)
          && b.IsConstantInt(out value))
        {
          a.Dispatch(this);
          if (this.PartialResult != null)
          {
            var candidate = BoxedExpression.Binary(BinaryOperator.Clt, this.PartialResult, right);
            candidate.Dispatch(this);
            if (this.PartialResult != null)
            {
              return this.NotifyOkAndSetResult(this.PartialResult);
            }
          }
        }
      }

      return false;
    }

    #endregion
  }
}
