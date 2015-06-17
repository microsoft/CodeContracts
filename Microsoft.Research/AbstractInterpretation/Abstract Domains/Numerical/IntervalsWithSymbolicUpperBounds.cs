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
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using Microsoft.Research.AbstractDomains.Expressions;

namespace Microsoft.Research.AbstractDomains.Numerical
{
  public class ProductIntervalsWeakUpperBounds<Expression> : 
    ReducedCartesianAbstractDomain<IntervalEnvironment<Rational, Expression>, 
    WeakUpperBounds<Expression, Rational>>, INumericalAbstractDomain<Rational, Expression>
  {

    #region Protected status
    IExpressionDecoder<Expression>/*!*/ decoder;
    #endregion

    #region (protected) Constructor
    /// <summary>
    /// Please note that the decoder MUST be already be set for the <code>left</code> and <code>right</code> abstract domains
    /// </summary>
    public ProductIntervalsWeakUpperBounds(IntervalEnvironment<Rational, Expression>/*!*/ left, WeakUpperBounds<Expression, Rational>/*!*/ right, IExpressionDecoder<Expression> decoder)
      : base(left, right)
    {
      this.decoder = decoder;
    }
    #endregion

    #region (protected) Constructor
    protected IExpressionDecoder<Expression> Decoder
    {
      get
      {
        return this.decoder;
      }
    }
    #endregion

    protected override ReducedCartesianAbstractDomain<IntervalEnvironment<Rational, Expression>, WeakUpperBounds<Expression, Rational>> 
      Factory(IntervalEnvironment<Rational, Expression> left, WeakUpperBounds<Expression, Rational> right)
    {
      return new ProductIntervalsWeakUpperBounds<Expression>(left, right, this.decoder);
    }

    #region Implementation of the abstract methods
    /// <summary>
    /// This reduction considers the pairs (x, y)  of intervals and add to the weak upper bounds all those such that x &lt; y
    /// </summary>
    public override ReducedCartesianAbstractDomain<IntervalEnvironment<Rational, Expression>, WeakUpperBounds<Expression, Rational>>
      Reduce(IntervalEnvironment<Rational, Expression> left, WeakUpperBounds<Expression, Rational> right)
    {
      return this.Factory(left, right);
    }

    override public bool LessEqual(IAbstractDomain/*!*/ a)
    {
      bool result;
      if (AbstractDomainsHelper.TryTrivialLess(this, a, out result))
      {
        return result;
      }

      ProductIntervalsWeakUpperBounds<Expression> r = a as ProductIntervalsWeakUpperBounds<Expression>;

      bool b1 = this.Left.LessEqual(r.Left);
      bool b2 = this.Right.LessEqual(r.Right);

      return b1 & b2;
    }

    /// <summary>
    /// This is a version of the join which causes a partial propagation of the information from Intervals to Symbolic upper bounds
    /// </summary>
    /// <param name="a">The other element</param>
    public override IAbstractDomain Join(IAbstractDomain a)
    {
      if (this.IsBottom)
        return a;
      if (a.IsBottom)
        return this;
      if (this.IsTop)
        return this;
      if (a.IsTop)
        return a;

      //^ assert a is ReducedCartesianAbstractDomain<LeftDomain, RightDomain>;

      ProductIntervalsWeakUpperBounds<Expression> r = a as ProductIntervalsWeakUpperBounds<Expression>;
      if (a == null)
      {
        Debug.Assert(false, "Error cannot compare a cartesian abstract element with a " + a.GetType());
      }

      IntervalEnvironment<Rational, Expression> joinLeftPart;
      WeakUpperBounds<Expression, Rational> joinRightPart;

      joinLeftPart = (IntervalEnvironment<Rational, Expression>)this.Left.Join(r.Left);
      joinRightPart = (WeakUpperBounds<Expression, Rational>)this.right.Join(r.Right);

      return this.Factory(joinLeftPart, joinRightPart);
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

      Debug.Assert(prev is ProductIntervalsWeakUpperBounds<Expression>, "Wrong type of the domain for the widening...");

      ProductIntervalsWeakUpperBounds<Expression> asIntWSUB = (ProductIntervalsWeakUpperBounds<Expression>)prev;

      IntervalEnvironment<Rational, Expression> widenLeft = (IntervalEnvironment<Rational, Expression>)this.Left.Widening(asIntWSUB.Left);
      WeakUpperBounds<Expression, Rational> widenRight = (WeakUpperBounds<Expression, Rational>)this.Right.Widening(asIntWSUB.Right);

      ProductIntervalsWeakUpperBounds<Expression> result = (ProductIntervalsWeakUpperBounds<Expression>)this.Factory(widenLeft, widenRight);

      return result;
    }

    #endregion

    #region INumericalAbstractDomain<Rational,Expression> Members

    /// <summary>
    /// Dispatch the assigment to the underlying abstract domains
    /// </summary>
    /// <param name="sourcesToTargets"></param>
    public void AssignInParallel(IDictionary<Expression, ISet<Expression>> sourcesToTargets)
    {
      this.left.AssignInParallel(sourcesToTargets);
      this.right.AssignInParallel(sourcesToTargets);
    }

    /// <returns>
    /// An interval that contains the upper bounds for <code>v</code>
    /// </returns>
    public Interval<Rational> BoundsFor(Expression v)
    {
      return left.BoundsFor(v);
    }

    public IThreshold<Rational, Rational> Thresholds
    {
      get
      {
        if (left.Thresholds != null)
          return left.Thresholds;
        else
          return right.Thresholds;
      }
      set
      {
        left.Thresholds = value;
        right.Thresholds = value;
      }
    }

    /// <summary>
    /// Check if the expression <code>exp</code> is greater than zero
    /// </summary>
    public FlatAbstractDomain<bool>/*!*/ CheckIfGreaterEqualThanZero(Expression/*!*/ exp)
    {
      FlatAbstractDomain<bool>/*!*/ checkOnLeft = this.Left.CheckIfGreaterEqualThanZero(exp);
      FlatAbstractDomain<bool>/*!*/ checkOnRight = this.Right.CheckIfGreaterEqualThanZero(exp);

      return (FlatAbstractDomain<bool>/*!*/)checkOnLeft.Meet(checkOnRight);
    }

    /// <summary>
    /// Check if the expression <code>e1</code> is strictly smaller than <code>e2</code>
    /// </summary>
    public FlatAbstractDomain<bool>/*!*/ CheckIfLessThan(Expression/*!*/ e1, Expression/*!*/ e2)
    {
      FlatAbstractDomain<bool>/*!*/ checkOnLeft = this.Left.CheckIfLessThan(e1, e2);
      FlatAbstractDomain<bool>/*!*/ checkOnRight = this.Right.CheckIfLessThan(e1, e2);

      return (FlatAbstractDomain<bool>/*!*/)checkOnLeft.Meet(checkOnRight);
    }

    #endregion

    #region IPureExpressionAssignmentsWithForward<Expression> Members

    public void Assign(Expression x, Expression exp)
    {
      this.left.Assign(x, exp);
      this.right.Assign(x, exp);
    }

    #endregion

    #region IPureExpressionAssignments<Expression> Members

    public ISet<Expression> Variables
    {
      get
      {
        ISet<Expression> varInLeft = this.Left.Variables;
        ISet<Expression> varInRight = this.Right.Variables;

        ISet<Expression> retVal = new Set<Expression>(varInLeft);
        retVal.AddRange(varInRight);

        return retVal;
      }
    }

    public void AddVariable(Expression var)
    {
      this.left.AddVariable(var);
      this.right.AddVariable(var);
    }

    public void ProjectVariable(Expression var)
    {
      this.left.ProjectVariable(var);
      this.right.ProjectVariable(var);
    }

    public void RemoveVariable(Expression var)
    {
      this.left.RemoveVariable(var);
      this.right.RemoveVariable(var);
    }

    public void RenameVariable(Expression OldName, Expression NewName)
    {
      this.left.RenameVariable(OldName, NewName);
      this.right.RenameVariable(OldName, NewName);
    }

    #endregion

    #region IPureExpressionTest<Expression> Members

    public IAbstractDomainWithTransferFunctions<Expression> TestTrue(Expression guard)
    {
      IntervalEnvironment<Rational, Expression> resultLeft = (IntervalEnvironment<Rational, Expression>)left.TestTrue(guard);
      WeakUpperBounds<Expression, Rational> resultRight = (WeakUpperBounds<Expression, Rational>)right.TestTrue(guard);

      return (IAbstractDomainWithTransferFunctions<Expression>)Factory(resultLeft, resultRight);
    }

    public IAbstractDomainWithTransferFunctions<Expression> TestFalse(Expression guard)
    {
      IntervalEnvironment<Rational, Expression> resultLeft = (IntervalEnvironment<Rational, Expression>)left.TestFalse(guard);
      WeakUpperBounds<Expression, Rational> resultRight = (WeakUpperBounds<Expression, Rational>)right.TestFalse(guard);

      return (IAbstractDomainWithTransferFunctions<Expression>)Factory(resultLeft, resultRight);
    }

    public FlatAbstractDomain<bool> CheckIfHolds(Expression exp)
    {
      FlatAbstractDomain<bool> checkLeft = this.Left.CheckIfHolds(exp);
      FlatAbstractDomain<bool> checkRight = this.Right.CheckIfHolds(exp);

      FlatAbstractDomain<bool> result = (FlatAbstractDomain<bool>)checkLeft.Meet(checkRight);

      return result;
    }

    #endregion

    #region IPureExpressionAssignmentsWithBackward<Expression> Members

    public void AssignBackward(Expression x, Expression exp)
    {
      throw new Exception("The method or operation is not implemented.");
    }

    public void AssignBackward(Expression x, Expression exp, IAbstractDomain preState, out IAbstractDomain refinedPreState)
    {
      throw new Exception("The method or operation is not implemented.");
    }

    #endregion
  }
}
#if false

  /// <summary>
  /// The main class for intervals (of rationals) with symbolic upper bounds
  /// </summary>
  abstract public class Pentagons<Rational, Expression> 
    : ReducedCartesianAbstractDomain<IntervalEnvironment<Rational, Expression>, WeakUpperBounds<Expression, Rational>>,
      INumericalAbstractDomain<Rational, Expression>
    where Rational : struct
  {
    #region Statistics
    static public string Statistics
    {
      get
      {
        string maxSizeforFunctionalDomain = MaxSizesAnalysisFrom(IntervalEnvironment<Rational, Expression>.MaxSizes);

        return maxSizeforFunctionalDomain.ToString();
      }
    }

    private static string MaxSizesAnalysisFrom(IDictionary<int, int> iDictionary)
    {
      int max = Int32.MinValue;
      int sum = 0;
      IDictionary<int, int> occurrences = new Dictionary<int, int>();

      foreach (int o in iDictionary.Keys)
      {
        if (iDictionary[o] > max)
        {
          max = iDictionary[o];
        }

        sum += iDictionary[o];

        // Update the occurrences count
        if (occurrences.ContainsKey(iDictionary[o]))
        {
          occurrences[iDictionary[o]]++;
        }
        else
        {
          occurrences[iDictionary[o]] = 1;
        }
      }

      double averageSize = ((double)sum) / iDictionary.Count;

      string result = "Max size : " + max + "\n" + "Average size: " + averageSize;

      return result;
    }

    #endregion

    #region Protected status
    IExpressionDecoder<Expression>/*!*/ decoder;    
    #endregion

    #region (protected) Constructor
    /// <summary>
    /// Please note that the decoder MUST be already be set for the <code>left</code> and <code>right</code> abstract domains
    /// </summary>
    protected Pentagons(IntervalEnvironment<Rational,Expression>/*!*/ left, WeakUpperBounds<Expression, Rational>/*!*/ right, IExpressionDecoder<Expression> decoder)
      : base(left, right)
    {
      this.decoder = decoder;
    }
    #endregion

    #region (protected) Constructor
    protected IExpressionDecoder<Expression> Decoder
    {
      get
      {
        return this.decoder;
      }
    }
    #endregion

    #region TO BE OVERRIDDEN
    abstract override protected ReducedCartesianAbstractDomain<IntervalEnvironment<Rational, Expression>, WeakUpperBounds<Expression, Rational>>
      Factory(IntervalEnvironment<Rational, Expression> left, WeakUpperBounds<Expression, Rational> right);
    #endregion 

    #region Implementation of the abstract methods
    /// <summary>
    /// This reduction considers the pairs (x, y)  of intervals and add to the weak upper bounds all those such that x &lt; y
    /// </summary>
    public override ReducedCartesianAbstractDomain<IntervalEnvironment<Rational, Expression>, WeakUpperBounds<Expression, Rational>> 
      Reduce(IntervalEnvironment<Rational, Expression> left, WeakUpperBounds<Expression, Rational> right)
    {
      ALog.BeginReduce("IntervalsWithSymbolicUpperBounds");

      foreach (Expression x in left.Keys)
      {
        foreach (Expression y in left.Keys)
        {
          if (x.Equals(y))
          {
            continue;
          }

          FlatAbstractDomain<bool> b = left.CheckIfLessThan(x, y);

          if (b.IsTop || b.IsBottom)
          {
            continue;
          }

          if (b.BoxedElement == true)
          {
            ALog.Message("Adding " + ExpressionPrinter<Expression>.ToString(x, this.decoder) + " < " + ExpressionPrinter<Expression>.ToString(y, this.decoder)
              + " as " + left.BoundsFor(x) + " < "+ left.BoundsFor(y));

            right.TestTrueLessThan(x, y);     // Add the constraint x < y
          }
        }
      }

      ALog.EndReduce();

      return this.Factory(left, right);
    }

    override public bool LessEqual(IAbstractDomain/*!*/ a)
    {
      if (this == a)
        return true; 
      if (this.IsBottom)
        return true;
      if (this.IsTop)
        return a.IsTop;
      if (a.IsBottom)
        return false;
      if (a.IsTop)
        return true;

      Pentagons<Rational, Expression> r = a as Pentagons<Rational, Expression>;

      bool b1 = this.Left.LessEqual(r.Left);

      if (!b1)
        return false;

      bool b2 = this.Right.LessEqual(r.Right);
      
      return b2;
    }

    /// <summary>
    /// This is a version of the join which causes a partial propagation of the information from Intervals to Symbolic upper bounds
    /// </summary>
    /// <param name="a">The other element</param>
    public override IAbstractDomain Join(IAbstractDomain a)
    {
      if (this.IsBottom)
        return a;
      if (a.IsBottom)
        return this;
      if (this.IsTop)
        return this;
      if (a.IsTop)
        return a;

      //^ assert a is ReducedCartesianAbstractDomain<LeftDomain, RightDomain>;

      Pentagons<Rational, Expression> r = a as Pentagons<Rational, Expression>;
      if (a == null)
      {
        Debug.Assert(false, "Error cannot compare a cartesian abstract element with a " + a.GetType());
      }

      IntervalEnvironment<Rational, Expression> joinLeftPart;
      WeakUpperBounds<Expression, Rational> joinRightPart;
      {
        // These two lines have a weak notion of closure, which essentially avoids dropping "x < y" if it is implied by the intervals abstract domain                
        // It seems that it is as precise as the expensive join

        joinRightPart = this.Right.Join(r.Right, this.Left, r.Left);
        joinLeftPart = (IntervalEnvironment<Rational, Expression>)this.Left.Join(r.Left);
      }

      return this.Factory(joinLeftPart, joinRightPart);
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

      Debug.Assert(prev is Pentagons<Rational, Expression>, "Wrong type of the domain for the widening...");

      Pentagons<Rational, Expression> asIntWSUB = (Pentagons<Rational, Expression>) prev;
 
      IntervalEnvironment<Rational, Expression> widenLeft = (IntervalEnvironment<Rational, Expression>) this.Left.Widening(asIntWSUB.Left);
      WeakUpperBounds<Expression, Rational> widenRight = (WeakUpperBounds<Expression, Rational>) this.Right.Widening(asIntWSUB.Right);

      Pentagons<Rational, Expression> result = (Pentagons<Rational,Expression>) this.Factory(widenLeft, widenRight);

      return result;
    }

    #endregion

    #region INumericalAbstractDomain<Rational,Expression> Members

    /// <summary>
    /// Dispatch the assigment to the underlying abstract domains
    /// </summary>
    /// <param name="sourcesToTargets"></param>
    public void AssignInParallel(IDictionary<Expression, ISet<Expression>> sourcesToTargets)
    {
      // NOTE NOTE that we first assign in the right (so to assign in the pre-state!!!) and then on the left
      this.right.AssignInParallel(sourcesToTargets, this.left);

      this.left.AssignInParallel(sourcesToTargets);
    }

    /// <returns>
    /// An interval that contains the upper bounds for <code>v</code>
    /// </returns>
    public Interval<Rational> BoundsFor(Expression v)
    {
      return left.BoundsFor(v);
    }

    public IThreshold<Rational, Rational> Thresholds
    {
      get
      {
        if (left.Thresholds != null)
          return left.Thresholds;
        else
          return right.Thresholds;
      }
      set
      {
        left.Thresholds = value;
        right.Thresholds = value;
      }
    }

    /// <summary>
    /// Check if the expression <code>exp</code> is greater than zero
    /// </summary>
    public FlatAbstractDomain<bool>/*!*/ CheckIfGreaterEqualThanZero(Expression/*!*/ exp)
    {
      FlatAbstractDomain<bool>/*!*/ checkOnLeft = this.Left.CheckIfGreaterEqualThanZero(exp);
      FlatAbstractDomain<bool>/*!*/ checkOnRight = this.Right.CheckIfGreaterEqualThanZero(exp);

      return (FlatAbstractDomain<bool>/*!*/)checkOnLeft.Meet(checkOnRight);
    }

    /// <summary>
    /// Check if the expression <code>e1</code> is strictly smaller than <code>e2</code>
    /// </summary>
    public FlatAbstractDomain<bool>/*!*/ CheckIfLessThan(Expression/*!*/ e1, Expression/*!*/ e2)
    {
      FlatAbstractDomain<bool>/*!*/ checkOnLeft = this.Left.CheckIfLessThan(e1, e2);
      FlatAbstractDomain<bool>/*!*/ checkOnRight = this.Right.CheckIfLessThan(e1, e2, this.Left);

      return (FlatAbstractDomain<bool>/*!*/)checkOnLeft.Meet(checkOnRight);
    }

    #endregion

    #region IPureExpressionAssignmentsWithForward<Expression> Members

    public void Assign(Expression x, Expression exp)
    {
      this.left.Assign(x, exp);
      this.right.Assign(x, exp);
    }

    #endregion

    #region IPureExpressionAssignments<Expression> Members

    public ISet<Expression> Variables
    {
      get 
      {
        ISet<Expression> varInLeft = this.Left.Variables;
        ISet<Expression> varInRight = this.Right.Variables;

        ISet<Expression> retVal = new Set<Expression>(varInLeft);
        retVal.AddRange(varInRight);

        return retVal;
      }
    }

    public void AddVariable(Expression var)
    {
      this.left.AddVariable(var);
      this.right.AddVariable(var);
    }

    public void ProjectVariable(Expression var)
    {
      this.left.ProjectVariable(var);
      this.right.ProjectVariable(var);
    }

    public void RemoveVariable(Expression var)
    {
      this.left.RemoveVariable(var);
      this.right.RemoveVariable(var);
    }

    public void RenameVariable(Expression OldName, Expression NewName)
    {
      this.left.RenameVariable(OldName, NewName);
      this.right.RenameVariable(OldName, NewName);
    }

    #endregion

    #region IPureExpressionTest<Expression> Members

    public IAbstractDomainWithTransferFunctions<Expression> TestTrue(Expression guard)
    {
      IntervalEnvironment<Rational, Expression> resultLeft = (IntervalEnvironment<Rational, Expression>) left.TestTrue(guard);
      WeakUpperBounds<Expression, Rational> resultRight = (WeakUpperBounds<Expression,Rational>) right.TestTrue(guard);

      return (IAbstractDomainWithTransferFunctions<Expression>) Factory(resultLeft, resultRight);
    }

    public IAbstractDomainWithTransferFunctions<Expression> TestFalse(Expression guard)
    {
      IntervalEnvironment<Rational, Expression> resultLeft = (IntervalEnvironment<Rational, Expression>) left.TestFalse(guard);
      WeakUpperBounds<Expression, Rational> resultRight = (WeakUpperBounds<Expression, Rational>) right.TestFalse(guard);

      return (IAbstractDomainWithTransferFunctions<Expression>) Factory(resultLeft, resultRight); 
    }

    public FlatAbstractDomain<bool> CheckIfHolds(Expression exp)
    {
      FlatAbstractDomain<bool> checkLeft = this.Left.CheckIfHolds(exp);
      FlatAbstractDomain<bool> checkRight = this.Right.CheckIfHolds(exp, this.left);

      FlatAbstractDomain<bool> result = (FlatAbstractDomain<bool>)checkLeft.Meet(checkRight);

      return result;
    }

    #endregion

    #region IPureExpressionAssignmentsWithBackward<Expression> Members

    public void AssignBackward(Expression x, Expression exp)
    {
      throw new Exception("The method or operation is not implemented.");
    }

    public void AssignBackward(Expression x, Expression exp, IAbstractDomain preState, out IAbstractDomain refinedPreState)
    {
      throw new Exception("The method or operation is not implemented.");
    }

    #endregion
  }

  /// <summary>
  /// Instantiation of intervals with symbolic bound with Rationals
  /// </summary>
  public class IntervalsWithSymbolicBoundsOfRational32<Expression> : ProductIntervalsWeakUpperBounds<Expression>
  {
    #region Constructor
    public IntervalsWithSymbolicBoundsOfRational32(IntervalEnvironment<Rational, Expression> left, WeakUpperBounds<Expression, Rational> right, IExpressionDecoder<Expression> decoder)
      : base(left, right, decoder)
    {
    }
    #endregion

    protected override ReducedCartesianAbstractDomain<IntervalEnvironment<Rational, Expression>, WeakUpperBounds<Expression, Rational>> Factory(IntervalEnvironment<Rational, Expression> left, WeakUpperBounds<Expression, Rational> right)
    {
      return new IntervalsWithSymbolicBoundsOfRational32<Expression>(left, right, this.Decoder);      
    }
  }

  public class ProductIntervalsWeakUpperBounds<Expression> : ReducedCartesianAbstractDomain<IntervalEnvironment<Rational, Expression>, WeakUpperBounds<Expression, Rational>>
  {
    public ProductIntervalsWeakUpperBounds(IntervalEnvironment<Rational, Expression> left, WeakUpperBounds<Expression, Rational> right)
      : base (left, right)
    {
    }

    public override ReducedCartesianAbstractDomain<IntervalEnvironment<Rational, Expression>, WeakUpperBounds<Expression, Rational>> Reduce(IntervalEnvironment<Rational, Expression> left, WeakUpperBounds<Expression, Rational> right)
    {
      throw new Exception("The method or operation is not implemented.");
    }

    public override IAbstractDomain Widening(IAbstractDomain prev)
    {
      throw new Exception("The method or operation is not implemented.");
    }

    protected override ReducedCartesianAbstractDomain<IntervalEnvironment<Rational, Expression>, WeakUpperBounds<Expression, Rational>> Factory(IntervalEnvironment<Rational, Expression> left, WeakUpperBounds<Expression, Rational> right)
    {
      throw new Exception("The method or operation is not implemented.");
    }
  }
}

#endif