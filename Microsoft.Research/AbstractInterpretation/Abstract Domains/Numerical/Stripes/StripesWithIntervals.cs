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

// The implementation for the reduced product of intervals and symbolic weak upper bounds
// This has got the official name of "Pentagons"

using System;
using System.Text;
using System.Diagnostics;
using Microsoft.Research.AbstractDomains.Expressions;
using Microsoft.Research.DataStructures;
using Microsoft.Research.CodeAnalysis;

namespace Microsoft.Research.AbstractDomains.Numerical
{
  using System.Collections.Generic;

  public class StripeWithIntervals<Variable, Expression, MetaDataDecoder> 
    : ReducedCartesianAbstractDomain<IntervalEnvironment<Variable, Expression>, StripeWithIntervals<Variable, Expression, MetaDataDecoder>>,
      INumericalAbstractDomain<Variable, Expression>
    where Expression : class
  {
    #region Protected status
    protected readonly IExpressionDecoder<Variable, Expression>/*!*/ decoder;

    #endregion

    #region Constructor
    public StripeWithIntervals(IntervalEnvironment<Variable, Expression>/*!*/ left, StripeWithIntervals<Variable, Expression, MetaDataDecoder>/*!*/ right, IExpressionDecoder<Variable, Expression> decoder, Logger Log)
      : base(left, right, Log)
    {      
      this.decoder = decoder;
    }

    protected IExpressionDecoder<Variable, Expression> Decoder
    {
      get
      {
        return this.decoder;
      }
    }
    #endregion

    #region Implementation of the abstract methods

    override protected ReducedCartesianAbstractDomain<IntervalEnvironment<Variable, Expression>, StripeWithIntervals<Variable, Expression, MetaDataDecoder>>
      Factory(IntervalEnvironment<Variable, Expression> left, StripeWithIntervals<Variable, Expression, MetaDataDecoder> right)
    {
      return new StripeWithIntervals<Variable, Expression, MetaDataDecoder>(left, right, this.decoder, this.Log);
    }

    public override ReducedCartesianAbstractDomain<IntervalEnvironment<Variable, Expression>, StripeWithIntervals<Variable, Expression, MetaDataDecoder>> 
      Reduce(IntervalEnvironment<Variable, Expression>left, StripeWithIntervals<Variable, Expression, MetaDataDecoder> right)
    {
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

      Debug.Assert(prev is StripeWithIntervals<Variable, Expression, MetaDataDecoder>, "Wrong type of the domain for the widening...");
      StripeWithIntervals<Variable, Expression, MetaDataDecoder> asIntWSUB = (StripeWithIntervals<Variable, Expression, MetaDataDecoder>)prev;
 
      var widenLeft = (IntervalEnvironment<Variable, Expression>) this.Left.Widening(asIntWSUB.Left);
      StripeWithIntervals<Variable, Expression, MetaDataDecoder> widenRight = (StripeWithIntervals<Variable, Expression, MetaDataDecoder>)this.Right.Widening(asIntWSUB.Right);

      StripeWithIntervals<Variable, Expression, MetaDataDecoder> result = (StripeWithIntervals<Variable, Expression, MetaDataDecoder>)this.Factory(widenLeft, widenRight);
     return result;
    }

    #endregion

    new IntervalEnvironment<Variable, Expression> Left
    {
      get
      {
        return base.Left;
      }
    }

    new StripeWithIntervals<Variable, Expression, MetaDataDecoder> Right
    {
      get
      {
        return base.Right;
      }
    }

    #region INumericalAbstractDomain<Variable, Expression>Members

    public virtual void AssignInParallel(Dictionary<Variable, FList<Variable>> sourcesToTargets, Converter<Variable, Expression> convert)
    {
      this.Right.AssignInParallel(sourcesToTargets, convert);

      this.Left.AssignInParallel(sourcesToTargets, convert);
    }

    public Interval BoundsFor(Expression v)
    {
      return this.Left.BoundsFor(v);
    }

    public Interval BoundsFor(Variable v)
    {
      return this.Left.BoundsFor(v);
    }

    public Dictionary<Variable, Int32> IntConstants
    {
      get
      {
        return new Dictionary<Variable, Int32>();
      }
    }

    public Set<Expression> LowerBoundsFor(Expression v, bool strict)
    {
      return new Set<Expression>(); //not implemented
    }

    public Set<Expression> UpperBoundsFor(Expression v, bool strict)
    {
      return new Set<Expression>(); //not implemented
    }

    public Set<Expression> LowerBoundsFor(Variable v, bool strict)
    {
      return new Set<Expression>(); //not implemented
    }

    public Set<Expression> UpperBoundsFor(Variable v, bool strict)
    {
      return new Set<Expression>(); //not implemented
    }

    #endregion

    #region IPureExpressionAssignmentsWithForward<Expression> Members

    public void Assign(Expression x, Expression exp)
    {
//      this.Left.Assign(x, exp);
//      this.Right.Assign(x, exp);
      this.Assign(x, exp, TopNumericalDomain<Variable, Expression>.Singleton);
    }

    public void Assign(Expression/*!*/ x, Expression/*!*/ exp, INumericalAbstractDomainQuery<Variable, Expression> preState)
    {
      this.Left.Assign(x, exp, preState);
      this.Right.Assign(x, exp, preState);
    }

    public void AssignInterval(Variable  x, Interval value)
    {
      this.Left.AssignInterval(x, value);
      this.Right.AssignInterval(x, value);
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

    #endregion

    #region IPureExpressionTest<Expression> Members

    public INumericalAbstractDomain<Variable, Expression> TestTrueGeqZero(Expression/*!*/ exp)
    {
      IntervalEnvironment<Variable, Expression> resultLeft = this.Left.TestTrueGeqZero(exp);
      StripeWithIntervals<Variable, Expression, MetaDataDecoder> resultRight = (StripeWithIntervals<Variable, Expression, MetaDataDecoder>)this.Right.TestTrueGeqZero(exp);

      return (INumericalAbstractDomain<Variable, Expression>)Factory(resultLeft, resultRight);
    }

    public INumericalAbstractDomain<Variable, Expression>TestTrueLessThan(Expression/*!*/ exp1, Expression/*!*/ exp2)
    {
      IntervalEnvironment<Variable, Expression> resultLeft = this.Left.TestTrueLessThan(exp1, exp2);
      StripeWithIntervals<Variable, Expression, MetaDataDecoder> resultRight = (StripeWithIntervals<Variable, Expression, MetaDataDecoder>)this.Right.TestTrueLessThan(exp1, exp2);

      return (INumericalAbstractDomain<Variable, Expression>)Factory(resultLeft, resultRight);
    }

    public INumericalAbstractDomain<Variable, Expression>TestTrueLessEqualThan(Expression/*!*/ exp1, Expression/*!*/ exp2)
    {
      IntervalEnvironment<Variable, Expression> resultLeft = this.Left.TestTrueLessEqualThan(exp1, exp2);
      StripeWithIntervals<Variable, Expression, MetaDataDecoder> resultRight = (StripeWithIntervals<Variable, Expression, MetaDataDecoder>)this.Right.TestTrueLessEqualThan(exp1, exp2);

      return (INumericalAbstractDomain<Variable, Expression>)Factory(resultLeft, resultRight);
    }

    public INumericalAbstractDomain<Variable, Expression> TestTrueEqual(Expression/*!*/ exp1, Expression/*!*/ exp2)
    {
      IntervalEnvironment<Variable, Expression> resultLeft = this.Left.TestTrueEqual(exp1, exp2);
      StripeWithIntervals<Variable, Expression, MetaDataDecoder> resultRight = (StripeWithIntervals<Variable, Expression, MetaDataDecoder>) this.Right.TestTrueEqual(exp1, exp2);

      return (INumericalAbstractDomain<Variable, Expression>)Factory(resultLeft, resultRight);
    }

    public virtual IAbstractDomainForEnvironments<Variable, Expression> TestTrue(Expression guard)
    {
      IntervalEnvironment<Variable, Expression> resultLeft = this.Left.TestTrue(guard);
      StripeWithIntervals<Variable, Expression, MetaDataDecoder> resultRight = (StripeWithIntervals<Variable, Expression, MetaDataDecoder>)this.Right.TestTrue(guard);

      return (IAbstractDomainForEnvironments<Variable, Expression>)Factory(resultLeft, resultRight);
    }

    public virtual IAbstractDomainForEnvironments<Variable, Expression> TestFalse(Expression guard)
    {
      IntervalEnvironment<Variable, Expression> resultLeft = this.Left.TestFalse(guard);
      StripeWithIntervals<Variable, Expression, MetaDataDecoder> resultRight = (StripeWithIntervals<Variable, Expression, MetaDataDecoder>)this.Right.TestFalse(guard);

      return (IAbstractDomainForEnvironments<Variable, Expression>)Factory(resultLeft, resultRight);
    }

    public INumericalAbstractDomain<Variable, Expression> RemoveRedundanciesWith(INumericalAbstractDomain<Variable, Expression> oracle)
    {
      IntervalEnvironment<Variable, Expression> resultLeft = (IntervalEnvironment<Variable, Expression>) this.Left.RemoveRedundanciesWith(oracle);
      StripeWithIntervals<Variable, Expression, MetaDataDecoder> resultRight = (StripeWithIntervals<Variable, Expression, MetaDataDecoder>)this.Right.RemoveRedundanciesWith(oracle);

      return (INumericalAbstractDomain<Variable, Expression>)Factory(resultLeft, resultRight);
    }
      
      public FlatAbstractDomain<bool> CheckIfHolds(Expression exp)
    {
      FlatAbstractDomain<bool> checkLeft = this.Left.CheckIfHolds(exp);
      FlatAbstractDomain<bool> checkRight = this.Right.CheckIfHolds(exp/*, this.Left*/);
      
      FlatAbstractDomain<bool> result =  checkLeft.Meet(checkRight);
      if (result.Equals(result.Top))
      { //If we do not arrive to check the condition, we try to refine the information in the stripe domain with the state of Interval domain and also internally
        StripeWithIntervals<Variable, Expression, MetaDataDecoder> refined = this.Right/*.Refine(this.Left).RefineInternally()*/;
        checkRight = refined.CheckIfHolds(exp/*, this.Left*/);
        result = checkLeft.Meet(checkRight);
      }
      return result;
    }

    public FlatAbstractDomain<bool>/*!*/ CheckIfGreaterEqualThanZero(Expression/*!*/ exp)
    {
      FlatAbstractDomain<bool>/*!*/ checkLeft = this.Left.CheckIfGreaterEqualThanZero(exp);
      FlatAbstractDomain<bool>/*!*/ checkRight = this.Right.CheckIfGreaterEqualThanZero(exp);

      FlatAbstractDomain<bool> result = checkLeft.Meet(checkRight);
      if (result.Equals(result.Top))
      { //If we do not arrive to check the condition, we try to refine the information in the stripe domain with the state of Interval domain and also internally
        StripeWithIntervals<Variable, Expression, MetaDataDecoder> refined = this.Right/*.Refine(this.Left).RefineInternally()*/;
        checkRight = refined.CheckIfGreaterEqualThanZero(exp);
        result = checkLeft.Meet(checkRight);
      }
      return result;
    }

    public FlatAbstractDomain<bool>/*!*/ CheckIfLessThan(Expression/*!*/ e1, Expression/*!*/ e2)
    {
      FlatAbstractDomain<bool>/*!*/ checkLeft = this.Left.CheckIfLessThan(e1, e2);
      FlatAbstractDomain<bool>/*!*/ checkRight = this.Right.CheckIfLessThan(e1, e2/*, this.Left*/);
      FlatAbstractDomain<bool> result = checkLeft.Meet(checkRight);
      if (result.Equals(result.Top))
      { //If we do not arrive to check the condition, we try to refine the information in the stripe domain with the state of Interval domain and also internally
        StripeWithIntervals<Variable, Expression, MetaDataDecoder> refined = this.Right/*.Refine(this.Left).RefineInternally()*/;
        checkRight = refined.CheckIfLessThan(e1, e2/*, this.Left*/);
        result = checkLeft.Meet(checkRight);
      }
      return result;
    }


    public FlatAbstractDomain<bool>/*!*/ CheckIfLessThan(Variable/*!*/ v1, Variable/*!*/ v2)
    {
      return CheckOutcome.Top;
    }


    public FlatAbstractDomain<bool>/*!*/ CheckIfNonZero(Expression/*!*/ e)
    {
      var checkLeft = this.Left.CheckIfNonZero(e);
      var checkRight = this.Right.CheckIfNonZero(e);

      return checkLeft.Meet(checkRight);
    }

    public FlatAbstractDomain<bool>/*!*/ CheckIfLessEqualThan(Expression/*!*/ e1, Expression/*!*/ e2)
    {
      return AbstractDomainsHelper.HelperForCheckLessEqualThan(this, e1, e2);
    }

    public FlatAbstractDomain<bool> CheckIfEqual(Expression e1, Expression e2)
    {
      var c1 = CheckIfLessEqualThan(e1, e2);
      var c2 = CheckIfLessEqualThan(e2, e1);

      if (c1.IsNormal() && c2.IsNormal())
      {
        return new FlatAbstractDomain<bool>(c1.BoxedElement && c2.BoxedElement);
      }

      return CheckOutcome.Top;
    }

    public FlatAbstractDomain<bool>/*!*/ CheckIfLessEqualThan(Variable/*!*/ v1, Variable/*!*/ v2)
    {
      return CheckOutcome.Top;
    }

    #endregion

    #region ToString
    public string ToString(Expression exp)
    {
      if (this.decoder != null)
      {
        return ExpressionPrinter.ToString(exp, this.decoder);
      }
      else
      {
        return "< missing expression decoder >";
      }
    }
    #endregion

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
