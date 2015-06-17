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
using System.Text;
using System.Diagnostics;
using Microsoft.Research.CodeAnalysis;
using Microsoft.Research.AbstractDomains.Expressions;
using Microsoft.Research.DataStructures;

namespace Microsoft.Research.AbstractDomains.Numerical
{
  using System.Collections.Generic;

  public class StripeIntervalsKarr<Variable, Expression, MetaDataDecoder>
    : ReducedCartesianAbstractDomain<StripeWithIntervals<Variable, Expression, MetaDataDecoder>, LinearEqualitiesEnvironment<Variable, Expression>>,
      INumericalAbstractDomain<Variable, Expression>
    where Expression : class
  {
    #region Protected status
    protected IExpressionDecoder<Variable, Expression>/*!*/ decoder;
    protected IExpressionEncoder<Variable, Expression>/*!*/ encoder;

    protected Precision precision;
    protected bool light { get { return this.precision == Precision.Light; } }
    protected bool strong { get { return this.precision == Precision.Strong; } }
    #endregion

    #region Constructor
    public StripeIntervalsKarr(StripeWithIntervals<Variable, Expression, MetaDataDecoder>/*!*/ left, LinearEqualitiesEnvironment<Variable, Expression>/*!*/ right, IExpressionDecoder<Variable, Expression> decoder, IExpressionEncoder<Variable, Expression> encoder, Logger Log)
      : base(left, right, Log)
    {
      this.decoder = decoder;
      this.encoder = encoder;
    }

    protected IExpressionDecoder<Variable, Expression> Decoder
    {
      get
      {
        return this.decoder;
      }
    }

    protected IExpressionEncoder<Variable, Expression> Encoder
    {
      get
      {
        return this.encoder;
      }
    }
    #endregion


    #region Implementation of the abstract methods

    override protected ReducedCartesianAbstractDomain<StripeWithIntervals<Variable, Expression, MetaDataDecoder>, LinearEqualitiesEnvironment<Variable, Expression>>
      Factory(StripeWithIntervals<Variable, Expression, MetaDataDecoder> left, LinearEqualitiesEnvironment<Variable, Expression> right)
    {
      return new StripeIntervalsKarr<Variable, Expression, MetaDataDecoder>(left, right, this.Decoder, this.Encoder, this.Log);
    }

    /// <summary>
    /// We propagate the equalities to a constant from LinEq to the intervals
    /// </summary>
    public override ReducedCartesianAbstractDomain<StripeWithIntervals<Variable, Expression, MetaDataDecoder>, LinearEqualitiesEnvironment<Variable, Expression>>
      Reduce(StripeWithIntervals<Variable, Expression, MetaDataDecoder> left, LinearEqualitiesEnvironment<Variable, Expression> right)
    {
     // We want to propagate this information as the join for Stripes and Lineq is very smart, but the one for intervals is not
      foreach (var constant in right.ConstantValues())
      {
        left.AssignInterval(constant.One, Interval.For(constant.Two));
      }
      
      return this.Factory(left, right);
    }

    /// <summary>
    /// The pairwise widening
    /// </summary>
    public override IAbstractDomain Widening(IAbstractDomain prev)
    {
      if (this.IsBottom)
        return prev;
      if (prev.IsBottom)
        return this;

      Debug.Assert(prev is StripeIntervalsKarr<Variable, Expression, MetaDataDecoder>, "Wrong type of the domain for the widening...");
      StripeIntervalsKarr<Variable, Expression, MetaDataDecoder> asIntWSUB = (StripeIntervalsKarr<Variable, Expression, MetaDataDecoder>)prev;

      StripeWithIntervals<Variable, Expression, MetaDataDecoder> widenLeft = (StripeWithIntervals<Variable, Expression, MetaDataDecoder>)this.Left.Widening(asIntWSUB.Left);
      LinearEqualitiesEnvironment<Variable, Expression> widenRight = (LinearEqualitiesEnvironment<Variable, Expression>)this.Right.Widening(asIntWSUB.Right);

      StripeIntervalsKarr<Variable, Expression, MetaDataDecoder> result = (StripeIntervalsKarr<Variable, Expression, MetaDataDecoder>)this.Factory(widenLeft, widenRight);
      return result;
    }

    #endregion

    #region INumericalAbstractDomain<Variable, Expression> Members

    public virtual void AssignInParallel(Dictionary<Variable, FList<Variable>> sourcesToTargets, Converter<Variable, Expression> convert)
    {
      this.Right.AssignInParallel(sourcesToTargets, convert);

      this.Left.AssignInParallel(sourcesToTargets, convert);
    }

    public Interval BoundsFor(Expression v)
    {
      return Left.BoundsFor(v);
    }

    public Interval BoundsFor(Variable v)
    {
      return this.Left.Left.BoundsFor(v);
    }

    public void AssignInterval(Variable/*!*/ x, Interval value)
    {
      // does nothing
    }

    #endregion

    #region IPureExpressionAssignmentsWithForward<Expression> Members

    public void Assign(Expression x, Expression exp)
    {
      this.Left.Assign(x, exp);
      this.Right.Assign(x, exp);
    }

    public void Assign(Expression x, Expression exp, INumericalAbstractDomainQuery<Variable, Expression> preState)
    {
      this.Left.Assign(x, exp, preState);
      this.Right.Assign(x, exp/*, preState*/);
    }

    #endregion

    #region IPureExpressionAssignments<Expression> Members

    public Set<Variable> Variables
    {
      get 
      {
        var varInLeft = this.Left.Variables;
        var varInRight = this.Right.Variables;

        var retVal = new Set<Variable>(varInLeft);
        retVal.AddRange(varInRight);

        return retVal;
      }
    }

    public void AddVariable(Variable var)
    {
      this.Left.AddVariable(var);
      this.Right.AddVariable(var);
    }

    public void ProjectVariable(Variable var)
    {
      this.Left.ProjectVariable(var);
      this.Right.ProjectVariable(var);
    }

    public void RemoveVariable(Variable var)
    {
      this.Left.RemoveVariable(var);
      this.Right.RemoveVariable(var);
    }

    public void RenameVariable(Variable OldName, Variable NewName)
    {
      this.Left.RenameVariable(OldName, NewName);
      this.Right.RenameVariable(OldName, NewName);
    }

    /// <summary>
    /// It does nothing. Should be improved
    /// </summary>
    public INumericalAbstractDomain<Variable, Expression> RemoveRedundanciesWith(INumericalAbstractDomain<Variable, Expression> oracle)
    {
      return (INumericalAbstractDomain<Variable, Expression> ) this.Clone();
    }

    #endregion

    #region IPureExpressionTest<Expression> Members

    public IAbstractDomainForEnvironments<Variable, Expression> TestTrue(Expression guard)
    {
      StripeWithIntervals<Variable, Expression, MetaDataDecoder> resultLeft = (StripeWithIntervals<Variable, Expression, MetaDataDecoder>)Left.TestTrue(guard);
      LinearEqualitiesEnvironment<Variable, Expression> resultRight = (LinearEqualitiesEnvironment<Variable, Expression>)Right.TestTrue(guard);

      return (IAbstractDomainForEnvironments<Variable, Expression>)Factory(resultLeft, resultRight);
    }

    public IAbstractDomainForEnvironments<Variable, Expression> TestFalse(Expression guard)
    {
      StripeWithIntervals<Variable, Expression, MetaDataDecoder> resultLeft = (StripeWithIntervals<Variable, Expression, MetaDataDecoder>)Left.TestFalse(guard);
      LinearEqualitiesEnvironment<Variable, Expression> resultRight = (LinearEqualitiesEnvironment<Variable, Expression>)Right.TestFalse(guard);

      return (IAbstractDomainForEnvironments<Variable, Expression>)Factory(resultLeft, resultRight);
    }

    public INumericalAbstractDomain<Variable, Expression>TestTrueGeqZero(Expression/*!*/ exp)
    {
      StripeWithIntervals<Variable, Expression, MetaDataDecoder> resultLeft = (StripeWithIntervals<Variable, Expression, MetaDataDecoder>)Left.TestTrueGeqZero(exp);
      LinearEqualitiesEnvironment<Variable, Expression> resultRight = (LinearEqualitiesEnvironment<Variable, Expression>)Right.TestTrueGeqZero(exp);

      return (INumericalAbstractDomain<Variable, Expression>)Factory(resultLeft, resultRight);
    }

    public INumericalAbstractDomain<Variable, Expression>TestTrueLessThan(Expression/*!*/ exp1, Expression/*!*/ exp2)
    {
      return this;
    }

    public INumericalAbstractDomain<Variable, Expression>TestTrueLessEqualThan(Expression/*!*/ exp1, Expression/*!*/ exp2)
    {
      return this;
    }

    INumericalAbstractDomain<Variable, Expression> INumericalAbstractDomain<Variable, Expression>.TestTrueEqual(Expression/*!*/ exp1, Expression/*!*/ exp2)
    {
      return this.TestTrueEqual(exp1, exp2);
    }

    public StripeIntervalsKarr<Variable, Expression, MetaDataDecoder> TestTrueEqual(Expression/*!*/ exp1, Expression/*!*/ exp2)
    {
      StripeWithIntervals<Variable, Expression, MetaDataDecoder> resultLeft = (StripeWithIntervals<Variable, Expression, MetaDataDecoder>)Left.TestTrueEqual(exp1, exp2);
      LinearEqualitiesEnvironment<Variable, Expression> resultRight = (LinearEqualitiesEnvironment<Variable, Expression>)Right.TestTrueEqual(exp1, exp2);

      return (StripeIntervalsKarr<Variable, Expression, MetaDataDecoder>)Factory(resultLeft, resultRight);
    }

    public FlatAbstractDomain<bool> CheckIfHolds(Expression exp)
    {
      return CheckIfHolds(exp, false);
    }

    private FlatAbstractDomain<bool> CheckIfHolds(Expression exp, bool refine)
    {
      FlatAbstractDomain<bool> result, checkLeft, checkRight;

      // At the first try, we do not want to refine the conditions
      if (refine)
      // F: Here it seems that Pietro was assuming that all the lineq have been propagated to intervals, 
      // which is not the case in general, so we explicitely call the reduction
      {
        foreach (Expression linEq in this.Right.ToExpressions())
        {
          this.Left.TestTrue(linEq);
        }
      }

      checkLeft = this.Left.CheckIfHolds(exp);

      // Shortcut: Is it is true, we return it immediately
      if (!checkLeft.IsBottom && !checkLeft.IsTop && checkLeft.BoxedElement == true)
      {
        return checkLeft;
      }

      checkRight = ((LinearEqualitiesEnvironment<Variable, Expression>)this.Right.Clone()).CheckIfHolds(exp);

      result = checkLeft.Meet(checkRight);

      // If we could not check the condition, 
      // we try to refine the information in the stripe domain with the state of the Karr domain on the variables of the condition
      // If Karr contains just constants, then there is nothing we can refine, so we save expensive computations
      if (result.IsTop && !this.Right.ContainsOnlyConstants)
      {

        var refined = this.Left.Right;
        var variables = this.ExtractVariable(exp);
        variables.AddRange(refined.Variables);

        foreach (var e in variables)
        {
         // refined = refined.Refine(this.Right, e);
        }

        result = refined.CheckIfHolds(exp);

        // Francesco : Hack - this method should be rewritten totally
        if (result.IsTop)
        {
          foreach (Expression massagedExp in MassageExp(exp))
          {
            result = refined.CheckIfHolds(massagedExp);

            if (!result.IsTop)
            {
              return result;
            }
          }
        }
      }

      if (!refine && result.IsTop)
        return CheckIfHolds(exp, true);
      else
        return result;
    }

    private Set<Expression> MassageExp(Expression exp)
    {
      Expression left, right;
      switch (this.decoder.OperatorFor(exp))
      {
        case ExpressionOperator.LessEqualThan:
        case ExpressionOperator.LessEqualThan_Un:
          left = this.decoder.LeftExpressionFor(exp);
          right = this.decoder.RightExpressionFor(exp);

          // All the polynomials
          var equalsTo = this.Right.EqualsTo(this.decoder.UnderlyingVariable(right));

          var result = new Set<Expression>();
          foreach (var p in equalsTo)
          {
            Expression asExp = p.ToPureExpression(this.Encoder);
            Expression newExp = this.Encoder.CompoundExpressionFor(ExpressionType.Bool, ExpressionOperator.LessEqualThan, left, asExp);

            result.Add(newExp);
          }

          return result;

        case ExpressionOperator.GreaterEqualThan:
        case ExpressionOperator.GreaterEqualThan_Un:
          left = this.decoder.LeftExpressionFor(exp);
          right = this.decoder.RightExpressionFor(exp);

          return MassageExp(this.encoder.CompoundExpressionFor(ExpressionType.Bool, ExpressionOperator.LessEqualThan, right, left));

        case ExpressionOperator.Equal:
        case ExpressionOperator.Equal_Obj:
          // Try to figure out if it is in the form of (e11 relop e12) == 0  
          left = this.decoder.LeftExpressionFor(exp);
          right = this.decoder.RightExpressionFor(exp);

          Expression e11, e12;
          ExpressionOperator relop;
          if (ExpressionHelper.Match_E1relopE2eq0(left, right, out relop, out e11, out e12, this.decoder))
          {
            ExpressionOperator negatedOp = ExpressionHelper.NegateRelationalOperator(relop);
            Expression newExp = this.Encoder.CompoundExpressionFor(ExpressionType.Bool, negatedOp, e11, e12);

            return MassageExp(newExp);
          }
          break;
      }
      
      // If we cannot massage the expression, we return an empty set
      return new Set<Expression>();
    }

    public FlatAbstractDomain<bool>/*!*/ CheckIfGreaterEqualThanZero(Expression/*!*/ exp)
    {
      FlatAbstractDomain<bool>/*!*/ checkLeft = this.Left.CheckIfGreaterEqualThanZero(exp);
      FlatAbstractDomain<bool>/*!*/ checkRight = this.Right.CheckIfGreaterEqualThanZero(exp);

      FlatAbstractDomain<bool> result = checkLeft.Meet(checkRight);
      return result;
    }

    /// <summary>
    /// Check if exp != 0
    /// </summary>
    public FlatAbstractDomain<bool>/*!*/ CheckIfNonZero(Expression/*!*/ exp)
    {
      FlatAbstractDomain<bool>/*!*/ checkLeft = this.Left.CheckIfNonZero(exp);
      FlatAbstractDomain<bool>/*!*/ checkRight = this.Right.CheckIfNonZero(exp);

      return checkLeft.Meet(checkRight);
    }

    public FlatAbstractDomain<bool>/*!*/ CheckIfLessThan(Expression/*!*/ e1, Expression/*!*/ e2)
    {
      FlatAbstractDomain<bool>/*!*/ checkLeft = this.Left.CheckIfLessThan(e1, e2);
      FlatAbstractDomain<bool>/*!*/ checkRight = this.Right.CheckIfLessThan(e1, e2);
      
      FlatAbstractDomain<bool> result = (FlatAbstractDomain<bool>/*!*/)checkLeft.Meet(checkRight);
      if (result.Equals(result.Top) && !light)
      { //If we do not arrive to check the condition, we try to refine the information in the stripe domain with the state of the Karr domain
        LinearEqualitiesEnvironment<Variable, Expression> karrRefined = this.Right;
        var refined = this.Left.Right;
        if (strong)
        { //If we want to perform a precise but slow analysis, we refine the information for all the variables of the state of the Karr Domain
          var variables = this.ExtractVariable(e1);
          variables.AddRange(this.ExtractVariable(e2));

          //foreach (var e in variables)
          //  refined = refined.Refine(karrRefined, e);

          //foreach (var e in karrRefined.Variables)
          //  refined = refined.Refine(karrRefined, e);
        }
        else 
        {//Otherwise we refine only on the variables of the condition
          var variables = this.ExtractVariable(e1);
          variables.AddRange(this.ExtractVariable(e2));
          
          //foreach (var e in variables)
          //  refined = refined.Refine(karrRefined, e);
        }
        checkRight = refined.CheckIfLessThan(e1, e2/*, this.Left.Left*/);
        result =  checkLeft.Meet(checkRight);
      }
      return result;
    }

    public FlatAbstractDomain<bool> CheckIfLessThan(Variable/*!*/ e1, Variable/*!*/ e2)
    {
      return CheckOutcome.Top;
    }

    /// <summary>
    /// We check if <code>e1 \lt e2</code>. If it false, we return unknown.
    /// </summary>
    public FlatAbstractDomain<bool> CheckIfLessEqualThan(Expression/*!*/ e1, Expression/*!*/ e2)
    {
      return AbstractDomainsHelper.HelperForCheckLessEqualThan(this, e1, e2);
    }

    public FlatAbstractDomain<bool> CheckIfEqual(Expression/*!*/ e1, Expression/*!*/ e2)
    {
      return AbstractDomainsHelper.HelperForCheckLessEqualThan(this, e1, e2);
    }

    public FlatAbstractDomain<bool> CheckIfLessEqualThan(Variable/*!*/ e1, Variable/*!*/ e2)
    {
      return CheckOutcome.Top;
    }

    /// <summary>
    /// Given an expression, it returns all the variables (and also the length of the pointers that are in the provided expression) contained by it
    /// </summary>
    private Set<Variable> ExtractVariable(Expression e1)
    {
      return this.Decoder.VariablesIn(e1).Union(LookForWritableBytes(e1));
    }

    private Set<Variable> LookForWritableBytes(Expression exp)
    {
      if (this.decoder.IsBinaryExpression(exp))
      {
        Set<Variable> result = LookForWritableBytes(this.decoder.LeftExpressionFor(exp));
        result.AddRange(LookForWritableBytes(this.decoder.RightExpressionFor(exp)));
        
        return result;
      }
      
      if (this.decoder.IsUnaryExpression(exp))
      {
        return LookForWritableBytes(this.decoder.LeftExpressionFor(exp));
      }

      if (this.decoder.IsVariable(exp))
      {
        Expression length;
        var expVar = this.decoder.UnderlyingVariable(exp);
        
        if (this.decoder.TryGetAssociatedExpression(exp, AssociatedInfo.WritableBytes, out length))
        {
          var lengthVar = this.Decoder.UnderlyingVariable(length);
          return new Set<Variable>(expVar).Union(new Set<Variable>(lengthVar));
        }
        else
        {
          return new Set<Variable>(expVar);
        }
      }
      
      // In all the other cases return the emptyset

      return new Set<Variable>();
    }

    #endregion

    #region INumericalAbstractDomain<Variable, Expression>Members

    public Dictionary<Variable, Int32> IntConstants
    {
      get
      {
        return new Dictionary<Variable, Int32>();
      }
    }

    public Set<Expression> LowerBoundsFor(Expression v, bool strict)
    {
     return new Set<Expression>(); // not implemented
    }

    public Set<Expression> UpperBoundsFor(Expression v, bool strict)
    {
      return new Set<Expression>(); // not implemented
    }

    public Set<Expression> LowerBoundsFor(Variable v, bool strict)
    {
      return new Set<Expression>(); // not implemented
    }

    public Set<Expression> UpperBoundsFor(Variable v, bool strict)
    {
      return new Set<Expression>(); // not implemented
    }

    #endregion

    #region ToString
    public string ToString(Expression exp)
    {
      if (this.Decoder != null)
      {
        return ExpressionPrinter.ToString(exp, this.Decoder);
      }
      else
      {
        return "< missing expression decoder >";
      }
    }
    #endregion

    static bool IsVariableEqConstant(Expression guard, IExpressionDecoder<Variable, Expression> decoder)
    {
      switch (decoder.OperatorFor(guard))
      {
        case ExpressionOperator.Equal:
        case ExpressionOperator.Equal_Obj:
          Expression left = decoder.LeftExpressionFor(guard);
          Expression right = decoder.RightExpressionFor(guard);

          return (decoder.IsVariable(left) && decoder.IsConstant(right)) || (decoder.IsVariable(right) && decoder.IsConstant(right));

        default:
          return false;
      }
    }

    #region Floating point types

    public void SetFloatType(Variable v, Floats f)
    {
      // does nothing
    }

    public FlatAbstractDomain<Floats> GetFloatType(Variable v)
    {
      return FloatTypes<Variable, Expression>.Unknown;
    }

    #endregion
  }
}
