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

// The file contains the following combination of abstract domains
//      + Symbolic and numeric
//      + Numeric and Boolean
//      + Symbolic and "generic"

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Research.DataStructures;
using Microsoft.Research.AbstractDomains.Numerical;
using Microsoft.Research.AbstractDomains.Expressions;
using System.Diagnostics;

namespace Microsoft.Research.AbstractDomains
{
  /// <summary>
  /// The combination, via reduced product, of the symbolic abstract domain and a domain for <code>int32</code>
  /// 
  /// TODO: Make it generic w.r.t. The type of numbers
  /// </summary>
  /// <typeparam name="Expression">Expression is the type of the expression tracked by the symbolic abstract domain</typeparam> 
  public class CombinedSymbolicNumericalDomain<Expression>
      : ReducedCartesianAbstractDomain<ISymbolicExpressionsAbstractDomain<Expression>, INumericalAbstractDomain<Int32, Expression>>,
          IAbstractDomainForEnvironments<Expression>
  {
    #region Protected fields: Decoder and Encoder
    protected IExpressionDecoder<Expression> decoder;
    protected IExpressionEncoder<Expression> encoder;

    #endregion

    #region Constructor
    /// <summary>
    /// Construct the reduced product of symbolic expressions with  a numerical domain (e.g. Intervals or Octagons)
    /// </summary>
    public CombinedSymbolicNumericalDomain(ISymbolicExpressionsAbstractDomain<Expression>/*!*/ symbolic, INumericalAbstractDomain<Int32, Expression>/*!*/ numerical,
        IExpressionDecoder<Expression> decoder, IExpressionEncoder<Expression> encoder)
      : base(symbolic, numerical)
    {
      this.decoder = decoder;
      this.encoder = encoder;
    }
    #endregion

    #region Implementation of abstract methods (Reduce, Widening and Factory)

    /// <summary>
    /// The reduction looks at all the bindings in the style <code>x -> Top</code>, and tries to replace <code>Top</code> with a fixed value, obtained from
    /// the numerical abstract domain.
    /// The reduction is done by allocation a new object...
    /// </summary>
    public override
        ReducedCartesianAbstractDomain<ISymbolicExpressionsAbstractDomain<Expression>, INumericalAbstractDomain<int, Expression>>
            Reduce(ISymbolicExpressionsAbstractDomain<Expression> left, INumericalAbstractDomain<int, Expression> right)
    {
      CombinedSymbolicNumericalDomain<Expression> result = new CombinedSymbolicNumericalDomain<Expression>(left, right, this.decoder, this.encoder);

      foreach (Expression x in result.Left.Variables)
      {
        //^ assert x != null;
        if (result.Left.ExpressionFor(x).IsTop)
        {
          result.ReduceFor(x);
        }
      }
      return result;
    }

    /// <summary>
    /// The widening is just the pointwise widening (without any reduction)
    /// </summary>
    public override IAbstractDomain/*!*/ Widening(IAbstractDomain/*!*/ prev)
    {
      if (this.IsBottom)
        return prev;
      if (this.IsTop)
        return this;
      if (prev.IsBottom)
        return this;
      if (prev.IsTop)
        return prev;
      
      Debug.Assert(prev is CombinedSymbolicNumericalDomain<Expression>,
          "I was expecting an instance of CombinedSymbolicNumericalDomain, I found an instance of " + prev.GetType().ToString());
      //^ assert prev is CombinedSymbolicNumericalDomain<Expression>;

      CombinedSymbolicNumericalDomain<Expression> prevCombined = (CombinedSymbolicNumericalDomain<Expression>)prev;

      SymbolicExpressionsAbstractDomain<Expression> newLeft = (SymbolicExpressionsAbstractDomain<Expression>)this.Left.Widening(prevCombined.Left);
      INumericalAbstractDomain<int, Expression> newRight = (INumericalAbstractDomain<int, Expression>)this.Right.Widening(prevCombined.Right);

      return new CombinedSymbolicNumericalDomain<Expression>(newLeft, newRight, this.decoder, this.encoder);
    }

    /// <returns>
    /// A new instance of CombinedSymbolicNumericalDomain
    /// </returns>
    protected override
        ReducedCartesianAbstractDomain<ISymbolicExpressionsAbstractDomain<Expression>, INumericalAbstractDomain<int, Expression>>
            Factory(ISymbolicExpressionsAbstractDomain<Expression>/*!*/  left, INumericalAbstractDomain<int, Expression>/*!*/  right)
    {
      return new CombinedSymbolicNumericalDomain<Expression>(left, right, this.decoder, this.encoder);
    }



    #endregion

    #region IPureExpressionAssignments Members

    public ISet<Expression>/*!*/ Variables
    {
      get
      {
        return this.Left.Variables;
      }
    }

    /// <summary>
    /// Add the variable <code>var</code> to both abstract domains
    /// </summary>
    public void AddVariable(Expression/*!*/ var)
    {
      this.Left.AddVariable(var);
      this.Right.AddVariable(var);
    }

    /// <summary>
    /// The assignment tries to find a good a more "refined" expression for <code>exp</code> using the Symbolic abstract domain.
    /// The refined expression is then passed to the numerical abstract domain.
    /// If it fails to find a more refined expression, the original <code>exp</code> is passed to the numerical domain
    /// </summary>
    public void Assign(Expression/*!*/ x, Expression/*!*/ exp)
    {
      this.Left.Assign(x, exp);
      FlatAbstractDomain<Expression> refinedExp = this.Left.ExpressionFor(x);

      if (refinedExp.IsBottom)
      {   // It's bottom, nothing to do
        return;
      }
      else if (refinedExp.IsTop)
      {   // We do not have any more refined expression, so we simply give up
        this.Right.Assign(x, exp);
        ReduceFor(x);
      }
      else
      {   // We use the more refined expression
        if (decoder.KindOf(refinedExp.BoxedElement) == ExpressionKind.Boolean)
          Console.WriteLine("Warning: I do not know how to handle the assignment " + x + " = " + refinedExp);
        else
          this.Right.Assign(x, refinedExp.BoxedElement);
      }
    }

    /// <summary>
    /// Project the variables from both abstract domains
    /// </summary>
    public void ProjectVariable(Expression/*!*/ var)
    {
      this.Left.ProjectVariable(var);
      this.Right.ProjectVariable(var);
    }

    /// <summary>
    /// Remove the variable from both abstract domains
    /// </summary>
    public void RemoveVariable(Expression/*!*/ var)
    {
      this.Left.RemoveVariable(var);
      this.Right.RemoveVariable(var);
    }

    /// <summary>
    /// Rename the variable <code>OldName</code> with <code>NewName</code>
    /// </summary>
    /// <param name="OldName">The old of the variable</param>
    /// <param name="NewName">The new name</param>
    public void RenameVariable(Expression/*!*/ OldName, Expression/*!*/ NewName)
    {
      this.Left.RenameVariable(OldName, NewName);
      this.Right.RenameVariable(OldName, NewName);
    }

    #endregion

    #region IPureExpressionTest Members

    /// <summary>
    /// First we try to refine the guard. 
    /// The we pass it to the numerical abstract domain
    /// </summary>
    public IAbstractDomainForEnvironments<Expression>/*!*/ TestTrue(Expression/*!*/ guard)
    {
      Expression refinedGuard = this.Left.Refine(guard);                 // We refine the guard
      Expression normalized = this.decoder.ToBooleanNormalizedForm(refinedGuard);    // then we normalize it

      return ProcessTest(normalized);
    }

    /// <summary>
    /// First we try to refine the guard. 
    /// The we pass it to the numerical abstract domain
    /// </summary>
    public IAbstractDomainForEnvironments<Expression>/*!*/ TestFalse(Expression/*!*/ guard)
    {
      IAbstractDomainForEnvironments<Expression>/*!*/ result;

      if (this.encoder != null)
      {
        Expression notGuard = this.encoder.CompoundExpressionFor(ExpressionKind.Boolean, ExpressionOperator.Not, guard);
        Expression refinedNotGuard = this.Left.Refine(notGuard);                    // We refine the guard
        Expression normalized = this.decoder.ToBooleanNormalizedForm(refinedNotGuard);    // then we normalize it

        result = ProcessTest(normalized);
      }
      else
      {
        result = this;
      }

      return result;
    }

    /// <summary>
    /// Check if the expression holds in this abstract domain.
    /// It does NOT check for the symbolic domain
    /// </summary>
    /// <param name="exp"></param>
    /// <returns></returns>
    public FlatAbstractDomain<bool> CheckIfHolds(Expression exp)
    {
      return this.Right.CheckIfHolds(exp);
    }

    #endregion

    #region Private methods

    /// <summary>
    /// Reduce the information: check if <code>x</code> is a numerical constant, and then it propagates it to the symbolic abstract domain
    /// </summary>
    private void ReduceFor(Expression/*!*/ x)
    {
      Interval<int> boundsForX = this.Right.BoundsFor(x);

      if (boundsForX.LowerBound == boundsForX.UpperBound)     // If it is a singleton value
      {
        //                this.Right.Assign(x, new Constant<int>(boundsForX.LowerBound));
        this.Left.Assign(x, this.encoder.ConstantFor(ExpressionKind.Integer, boundsForX.LowerBound));
      }
    }

    /// <summary>
    /// Process the <code>guard</code>.
    /// It dispatches the terms to the right abstract domains, and it perform the joins and the meet in accordance with "and" and "or"
    /// </summary>
    /// <param name="guard">Must be a normalized guard (i.e. the result of the invocation of a <code>ToBooleanNormalizedForm</code></param>
    private IAbstractDomainForEnvironments<Expression> ProcessTest(Expression/*!*/ guard)
    {
      IAbstractDomainForEnvironments<Expression> result = this;
      IAbstractDomainForEnvironments<Expression> left, right;
      CombinedSymbolicNumericalDomain<Expression> firstClone, secondClone;   // On occurrence clone this state

      bool success; 
      switch (this.decoder.OperatorFor(guard))
      {
        case ExpressionOperator.Constant:
          bool value = this.decoder.TryValueOf<bool>(guard, ExpressionType.Bool, out success);

          result = value ? this : (IAbstractDomainForEnvironments<Expression>)this.Bottom;
          break;

        case ExpressionOperator.ConvertToInt32:
        case ExpressionOperator.ConvertToUInt8:
        case ExpressionOperator.ConvertToUInt16:
        case ExpressionOperator.ConvertToUInt32:
          result = ProcessTest(this.decoder.LeftExpressionFor(guard));
          break;

        case ExpressionOperator.Variable:
          throw new AbstractInterpretationTODOException("TODO");
        case ExpressionOperator.Not:
          throw new AbstractInterpretationTODOException("TODO");

        case ExpressionOperator.And:            // "&" becomes the meet
          firstClone = (CombinedSymbolicNumericalDomain<Expression>)this.Clone();
          secondClone = (CombinedSymbolicNumericalDomain<Expression>)this.Clone();

          //^ assert secondClone.Right != firstClone.Right;
          Debug.Assert(secondClone.Right != firstClone.Right, "the Clone operation is supposed to return fresh instances");

          left = firstClone.ProcessTest(this.decoder.LeftExpressionFor(guard));
          right = secondClone.ProcessTest(this.decoder.RightExpressionFor(guard));

          result = (IAbstractDomainForEnvironments<Expression>)left.Meet(right);
          break;

        case ExpressionOperator.Or:             // "|" becomes the join
          firstClone = (CombinedSymbolicNumericalDomain<Expression>)this.Clone();
          secondClone = (CombinedSymbolicNumericalDomain<Expression>)this.Clone();

          //^ assert secondClone.Right != firstClone.Right;
          Debug.Assert(secondClone.Right != firstClone.Right, "the Clone operation is supposed to return fresh instances");

          left = ProcessTest(this.decoder.LeftExpressionFor(guard));
          right = ProcessTest(this.decoder.RightExpressionFor(guard));

          result = (IAbstractDomainForEnvironments<Expression>)left.Join(right);
          break;

        case ExpressionOperator.Equal:
          throw new AbstractInterpretationTODOException("TODO");
        case ExpressionOperator.NotEqual:
          throw new AbstractInterpretationTODOException("TODO");

        case ExpressionOperator.LessThan:
          Debug.Assert(false, "At this point you must have passed through the normalization of boolean expressions to get rid of " + this.decoder.OperatorFor(guard));
          //^ assert false;
          throw new AbstractInterpretationException("Error!!!");

        case ExpressionOperator.LessEqualThan:
          // bool success;
          IPolynomial<Expression> pol = ArithmeticExpressionsConverter<Expression>.ToPolynomialForm(guard, this.decoder, out success);
          if (success)
          {
            Expression simplifiedGuard = pol.ToCanonicalForm().ToPureExpression(this.encoder);

            firstClone = (CombinedSymbolicNumericalDomain<Expression>)this.Clone();

            result = firstClone.Right.TestTrue(simplifiedGuard);      // pass the guard to the abstract domain
          }
          else
          {
            result = this;
          }
          break;

        case ExpressionOperator.GreaterThan:
        case ExpressionOperator.GreaterEqualThan:
          Debug.Assert(false, "At this point you must have passed through the normalization of boolean expressions to get rid of " + this.decoder.OperatorFor(guard));
          //^ assert false;
          throw new AbstractInterpretationException("Impossible case?");

        case ExpressionOperator.Addition:
        case ExpressionOperator.Subtraction:
        case ExpressionOperator.Multiplication:
        case ExpressionOperator.ShiftLeft:
        case ExpressionOperator.ShiftRight:
        case ExpressionOperator.SizeOf:
        case ExpressionOperator.Division:
        case ExpressionOperator.UnaryMinus:
          Debug.Assert(false, "An arithmetic operator cannot appear in a guard, something is wrong? ( " + guard + " )");
          //^ assert false;
          throw new AbstractInterpretationException("Impossible case?");

        case ExpressionOperator.Unknown:
          result = this;
          break;

        default:
          Debug.Assert(false, "Unknown case in process test : " + guard);
          throw new AbstractInterpretationException("Unknown case...");
      }

      return result;
    }
    #endregion


    #region IAssignInParallel<Expression> Members

    public void AssignInParallel(IDictionary<Expression, FList<Expression>> sourcesToTargets)
    {
      throw new Exception("The method or operation is not implemented.");
    }

    #endregion
  }

  /// <summary>
  /// The combination, via reduced product, of a symbolic domain and a generic abstract domain.
  /// 
  /// It is intended for endowing existing domains with symbolic reasoning
  /// </summary>
  /// <typeparam name="ADomain">The abstract domain of interest</typeparam>
  /// <typeparam name="Expression">The concrete representation for expressions</typeparam>
  public class CombinedSymbolicGenericDomain<ADomain, Expression>
      : ReducedCartesianAbstractDomain<ISymbolicExpressionsAbstractDomain<Expression>, ADomain>,
          IAbstractDomainForEnvironments<Expression>
    where ADomain : IAbstractDomainForEnvironments<Expression>
  {
    #region Private state
    private IExpressionDecoder<Expression> decoder;
    private IExpressionEncoder<Expression> encoder;
    #endregion

    #region Constructor
    public CombinedSymbolicGenericDomain(IExpressionDecoder<Expression>/*!*/  decoder, IExpressionEncoder<Expression>/*!*/  encoder,
        ISymbolicExpressionsAbstractDomain<Expression>/*!*/  symbolic, ADomain/*!*/  generic)
      : base(symbolic, generic)
    {
      this.decoder = decoder;
      this.encoder = encoder;
    }
    #endregion

    #region TO BE OVERRIDDEN

    /// <summary>
    /// The default for the reduction does nothing. If you want a better definition, subclass this class and override this method
    /// </summary>
    public override ReducedCartesianAbstractDomain<ISymbolicExpressionsAbstractDomain<Expression>, ADomain>/*!*/
        Reduce(ISymbolicExpressionsAbstractDomain<Expression>/*!*/ left, ADomain/*!*/ right)
    {
      return Factory(left, right);
    }

    /// <summary>
    /// Default: does nothing
    /// </summary>
    virtual protected void ReduceFor(Expression/*!*/ x)
    {
      ISymbolicExpressionRefiner<Expression>/*?*/ refiner = this.Right as ISymbolicExpressionRefiner<Expression>;

      if (refiner != null)
      {
        Expression refinedExpressionForX;
        bool b = refiner.CanRefine(x, out refinedExpressionForX);
        if (b)
        {
          Debug.Assert(refinedExpressionForX != null);
          //^ assert refinedExpressionForX != null;

          Expression newAssumption = this.encoder.CompoundExpressionFor(ExpressionKind.Boolean, ExpressionOperator.Equal, x, refinedExpressionForX);
          this.Left.TestTrue(newAssumption);
        }
      }
    }
    #endregion

    #region Implementation of the abstract methods

    /// <summary>
    /// Pairwise widening
    /// </summary>
    public override IAbstractDomain/*!*/ Widening(IAbstractDomain/*!*/ prev)
    {
      if (this.IsBottom)
        return prev;
      if (this.IsTop)
        return this;
      if (prev.IsBottom)
        return this;
      if (prev.IsTop)
        return prev;

      //^ assert prev is CombinedSymbolicGenericDomain<ADomain, Expression>;
      Debug.Assert(prev is CombinedSymbolicGenericDomain<ADomain, Expression>,
          "I was expecting an instance of CombinedSymboliGeneric, I found an instance of " + prev.GetType().ToString());

      CombinedSymbolicGenericDomain<ADomain, Expression> prevCombined = (CombinedSymbolicGenericDomain<ADomain, Expression>) prev;

      SymbolicExpressionsAbstractDomain<Expression> newLeft = (SymbolicExpressionsAbstractDomain<Expression>)this.Left.Widening(prevCombined.Left);
      ADomain newRight = (ADomain)this.Right.Widening(prevCombined.Right);

      return Factory(newLeft, newRight);
    }

    /// <returns>
    /// A fresh instance for this abstract domain
    /// </returns>
    protected override ReducedCartesianAbstractDomain<ISymbolicExpressionsAbstractDomain<Expression>, ADomain>
        Factory(ISymbolicExpressionsAbstractDomain<Expression>/*!*/  left, ADomain/*!*/  right)
    {
      return new CombinedSymbolicGenericDomain<ADomain, Expression>(this.decoder, this.encoder, left, right);
    }

    #endregion

    #region IPureExpressionAssignments<Expression> Members

    public ISet<Expression>/*!*/ Variables
    {
      get
      {
        return this.Left.Variables;
      }
    }

    /// <summary>
    /// Add the variable <code>var</code> to both the abstract domains
    /// </summary>
    public void AddVariable(Expression/*!*/ var)
    {
      this.Left.AddVariable(var);
      this.Right.AddVariable(var);
    }

    /// <summary>
    /// The assignment tries to find a good a more "refined" expression for <code>exp</code> using the Symbolic abstract domain.
    /// The refined expression is then passed to the generic abstract domain.
    /// If it fails to find a more refined expression, the original <code>exp</code> is passed to the right
    /// </summary>
    public void Assign(Expression/*!*/ x, Expression/*!*/ exp)
    {
      this.Left.Assign(x, exp);
      FlatAbstractDomain<Expression> refinedExp = this.Left.ExpressionFor(x);

      if (refinedExp.IsBottom)
      {   // It's bottom, nothing to do
        return;
      }
      else if (refinedExp.IsTop)
      {   // We do not have a more refined expression, so we simply give up and we pass the original expression to the generic abstract domain
        this.Right.Assign(x, exp);
        ReduceFor(x);
      }
      else
      {   // We use the more refined expression
        this.Right.Assign(x, refinedExp.BoxedElement);
        ReduceFor(x);
      }
    }

    /// <summary>
    /// Project the variable <code>var</code>, i.e. set the variable of <code>var</code> to be top
    /// </summary>
    public void ProjectVariable(Expression/*!*/ var)
    {
      this.Left.ProjectVariable(var);
      this.Right.ProjectVariable(var);
    }

    /// <summary>
    /// Remove the variable <code>var</code>, i.e. project the value, and remove from the state
    /// </summary>
    public void RemoveVariable(Expression/*!*/ var)
    {
      this.Left.RemoveVariable(var);
      this.Right.RemoveVariable(var);
    }

    /// <summary>
    /// Rename the variable <code>OldName</code> with <code>NewName</code>
    /// </summary>
    /// <param name="OldName">The old of the variable</param>
    /// <param name="NewName">The new name</param>
    public void RenameVariable(Expression/*!*/ OldName, Expression/*!*/ NewName)
    {
      this.Left.RenameVariable(OldName, NewName);
      this.Right.RenameVariable(OldName, NewName);
    }
    #endregion

    #region IPureExpressionTest<Expression> Members

    /// <summary>
    /// First we try to refine the guard using the symbolic abstract domain.
    /// Then we pass it to the underlying domain
    /// </summary>
    /// <param name="guard"></param>
    /// <returns></returns>
    public IAbstractDomainForEnvironments<Expression>/*!*/ TestTrue(Expression/*!*/ guard)
    {
      Expression normalized = HelperForNormalizingBooleanGuard(guard);

      return ProcessTest(normalized);
    }

    public IAbstractDomainForEnvironments<Expression>/*!*/ TestFalse(Expression/*!*/ guard)
    {
      IAbstractDomainForEnvironments<Expression>/*!*/ result;

      if (this.encoder != null)
      {
        Expression notGuard = this.encoder.CompoundExpressionFor(ExpressionKind.Boolean, ExpressionOperator.Not, guard);
        Expression normalized = HelperForNormalizingBooleanGuard(notGuard);

        result = ProcessTest(normalized);
      }
      else
      {
        result = this;
      }
      return result;
    }

    /// <summary>
    /// Check if the expression holds in this abstract domain.
    /// It does NOT check for the symbolic domain
    /// </summary>
    /// <param name="exp"></param>
    /// <returns></returns>
    public FlatAbstractDomain<bool> CheckIfHolds(Expression exp)
    {
      Expression normalized = HelperForNormalizingBooleanGuard(exp);
      
      return this.Right.CheckIfHolds(normalized);
    }


    #endregion

    #region Private methods (Implementation of helper methods in the handling of the guard)

    /// <summary>
    /// Process the <code>guard</code>.
    /// It dispatches the terms to the right abstract domains, and it perform the joins and the meet in accordance with "and" and "or"
    /// </summary>
    /// <param name="guard">Must be a normalized guard (i.e. the result of the invocation of a <code>ToBooleanNormalizedForm</code></param>
    private IAbstractDomainForEnvironments<Expression>/*!*/  ProcessTest(Expression/*!*/ guard)
    {
      CombinedSymbolicGenericDomain<ADomain, Expression> result = this;
      IAbstractDomainForEnvironments<Expression> left, right;
      CombinedSymbolicGenericDomain<ADomain, Expression> firstClone, secondClone;   // On occurrence clone this state
      bool success;

      switch (this.decoder.OperatorFor(guard))
      {
        #region All the cases
        case ExpressionOperator.Constant:       // We just see if it is true or false...
          bool value = true;
          if (this.decoder.KindOf(guard) == ExpressionKind.Boolean)
          {
            value = this.decoder.TryValueOf<bool>(guard, ExpressionType.Bool, out success);
          }
          else if (this.decoder.KindOf(guard) == ExpressionKind.Integer)
          {
            value = this.decoder.TryValueOf<int>(guard, ExpressionType.Int32, out success) == 0 ? false : true;
          }
          else
          {
            Debug.Assert(false, "Unkwon case..." + guard);
            //^ assert false;
          }

          result = value ? this : (CombinedSymbolicGenericDomain<ADomain, Expression>)this.Bottom;
          break;

        case ExpressionOperator.ConvertToInt32:
        case ExpressionOperator.ConvertToUInt8:
        case ExpressionOperator.ConvertToUInt16:
        case ExpressionOperator.ConvertToUInt32:
          result = (CombinedSymbolicGenericDomain<ADomain,Expression>)ProcessTest(this.decoder.LeftExpressionFor(guard));
          break;

        case ExpressionOperator.Variable:   // We just pass it below to the domains
          return HelperForProcessTestForVariable(guard);

        case ExpressionOperator.Not:
          return helperForProcessTestForNot(guard);   // We just pass it below to the other domains

        case ExpressionOperator.And:            // "&" becomes the meet
          firstClone = (CombinedSymbolicGenericDomain<ADomain, Expression>)this.Clone();
          secondClone = (CombinedSymbolicGenericDomain<ADomain, Expression>)this.Clone();

          // ^ assert secondClone.Right != firstClone.Right;
          Debug.Assert(!secondClone.Right.Equals(firstClone.Right), "the Clone operation is supposed to return fresh instances");

          left = firstClone.ProcessTest(this.decoder.LeftExpressionFor(guard));
          right = secondClone.ProcessTest(this.decoder.RightExpressionFor(guard));

          result = (CombinedSymbolicGenericDomain<ADomain, Expression>)left.Meet(right);
          break;

        case ExpressionOperator.Or:             // "|" becomes the join
          firstClone = (CombinedSymbolicGenericDomain<ADomain, Expression>)this.Clone();
          secondClone = (CombinedSymbolicGenericDomain<ADomain, Expression>)this.Clone();

          // ^ assert secondClone.Right != firstClone.Right;
          Debug.Assert(!secondClone.Right.Equals(firstClone.Right), "the Clone operation is supposed to return fresh instances");

          left = ProcessTest(this.decoder.LeftExpressionFor(guard));
          right = ProcessTest(this.decoder.RightExpressionFor(guard));

          result = (CombinedSymbolicGenericDomain<ADomain, Expression>)left.Join(right);
          break;

        case ExpressionOperator.Equal:
          return HelperForProcessTestForEqual(guard);

        case ExpressionOperator.NotEqual:
          return HelperForProcessTestForNotEqual(guard);

        case ExpressionOperator.LessThan:
          Debug.Assert(false, "At this point you must have passed through the normalization of boolean expressions to get rid of " + this.decoder.OperatorFor(guard));
          //^ assert false;
          throw new AbstractInterpretationException("Impossible case?");

        case ExpressionOperator.LessEqualThan:
          // Simplifies the guard, by performing some basic arthmetics, and putting it in a normal form

          // bool success;
          IPolynomial<Expression> polynomialFormForGuard = ArithmeticExpressionsConverter<Expression>.ToPolynomialForm(guard, this.decoder, out success);
          if (success)
          {
            IPolynomial<Expression> canonicalFormForGuard = polynomialFormForGuard.ToCanonicalForm();
            Expression simplifiedGuard = canonicalFormForGuard.ToPureExpression(encoder);

            ADomain firstCloneRight = (ADomain)this.Right.Clone();                  // Clone the right abstract domain

            ADomain resultRight = (ADomain)firstCloneRight.TestTrue(simplifiedGuard);      // Pass the guard to the right abstract domain

            result = (CombinedSymbolicGenericDomain<ADomain, Expression>)Factory(this.Left, resultRight);
          }
          else
          {
            return this;
          }
          break;

        case ExpressionOperator.GreaterThan:
        case ExpressionOperator.GreaterEqualThan:
          Debug.Assert(false, "At this point you must have passed through the normalization of boolean expressions to get rid of " + this.decoder.OperatorFor(guard));
          //^ assert false;
          throw new AbstractInterpretationException("Impossible case?");

        case ExpressionOperator.Addition:
        case ExpressionOperator.Subtraction:
        case ExpressionOperator.Multiplication:
        case ExpressionOperator.ShiftLeft:
        case ExpressionOperator.ShiftRight:
        case ExpressionOperator.SizeOf:
        case ExpressionOperator.Division:
        case ExpressionOperator.UnaryMinus:
          Debug.Assert(false, "An arithmetic operator cannot appear in a guard, something is wrong? ( " + guard + " )");
          //^ assert false;
          throw new AbstractInterpretationException("Impossible case?");

        case ExpressionOperator.Unknown:
          result = this;
          break;

        default:
          Debug.Assert(false, "Unknown case...");
          throw new AbstractInterpretationException("Unknown case...");

        #endregion
      }

      return result;
    }

    /// <summary>
    /// We push the expression below to the two domains
    /// </summary>
    /// <param name="guard">Must be "not" an expression</param>
    private IAbstractDomainForEnvironments<Expression>/*!*/  helperForProcessTestForNot(Expression/*!*/  guard)
    {
      Debug.Assert(this.decoder.OperatorFor(guard) == ExpressionOperator.Not, "Expecting \"not something\", found : " + guard);

      ISymbolicExpressionsAbstractDomain<Expression> leftTrue = (ISymbolicExpressionsAbstractDomain<Expression>)this.Left.TestTrue(guard);

      if (leftTrue.IsBottom)
      {
        return (CombinedSymbolicGenericDomain<ADomain, Expression>)this.Bottom;
      }

      ADomain rightTrue = (ADomain)this.Right.TestTrue(guard);

      //TODO4: use some sharing here 

      return (CombinedSymbolicGenericDomain<ADomain, Expression>)Factory(leftTrue, rightTrue);
    }

    /// <summary>
    /// We push the variable to the two domains.
    /// To do: refine the information of the symbolic domain with the one that comes from below
    /// </summary>
    /// <param name="guard"></param>
    /// <returns></returns>
    private IAbstractDomainForEnvironments<Expression>/*!*/  HelperForProcessTestForVariable(Expression/*!*/  guard)
    {
      Debug.Assert(this.decoder.IsVariable(guard), "Expecting a variable, found " + guard);

      ISymbolicExpressionsAbstractDomain<Expression> leftTrue = (ISymbolicExpressionsAbstractDomain<Expression>)this.Left.TestTrue(guard);

      if (leftTrue.IsBottom)
      {
        return (CombinedSymbolicGenericDomain<ADomain, Expression>)this.Bottom;
      }

      ADomain rightTrue = (ADomain)this.Right.TestTrue(guard);

      // TODO2: recover the information from the right domain, to refine the information in the symbolic domain

      // TODO4: use some sharing here 

      return (CombinedSymbolicGenericDomain<ADomain, Expression>)Factory(leftTrue, rightTrue);
    }

    /// <summary>
    /// We know that the parameter is a eq(left, right) expression, and we try to refine this information
    /// </summary>
    private IAbstractDomainForEnvironments<Expression>/*!*/  HelperForProcessTestForEqual(Expression/*!*/  guard)
    {
      Debug.Assert(this.decoder.OperatorFor(guard) == ExpressionOperator.Equal, "This helper method can be called only with equality!");

      ISymbolicExpressionsAbstractDomain<Expression> leftTrue = (ISymbolicExpressionsAbstractDomain<Expression>)this.Left.TestTrue(guard);

      if (leftTrue.IsBottom)
      {
        return (CombinedSymbolicGenericDomain<ADomain, Expression>)this.Bottom;
      }

      ADomain rightTrue = (ADomain)this.Right.TestTrue(guard);

      //TODO4: use some sharing here 

      return (CombinedSymbolicGenericDomain<ADomain, Expression>)Factory(leftTrue, rightTrue);
    }

    /// <summary>
    /// We know that the parameter is a noteq(left, right) expression, so we try to refine this information
    /// </summary>
    private IAbstractDomainForEnvironments<Expression>/*!*/  HelperForProcessTestForNotEqual(Expression/*!*/  guard)
    {
      Debug.Assert(this.decoder.OperatorFor(guard) == ExpressionOperator.NotEqual, "This helper method can be called only with notequal. Found : " + guard);

      ISymbolicExpressionsAbstractDomain<Expression> leftTrue = (ISymbolicExpressionsAbstractDomain<Expression>)this.Left.TestTrue(guard);

      if (leftTrue.IsBottom)
      {
        return (CombinedSymbolicGenericDomain<ADomain, Expression>)this.Bottom;
      }

      ADomain rightTrue = (ADomain)this.Right.TestTrue(guard);

      //TODO4: use some sharing here

      return (CombinedSymbolicGenericDomain<ADomain, Expression>)Factory(leftTrue, rightTrue);
    }

    /// <summary>
    /// Normalize the boolean guard <code>guard</code>
    /// </summary>
    /// <param name="guard"></param>
    /// <returns></returns>
    private Expression HelperForNormalizingBooleanGuard(Expression guard)
    {
      Expression refinedGuard = this.Left.Refine(guard);                              // We refine the guard
 
      Expression normalized = this.decoder.ToBooleanNormalizedForm(refinedGuard);     // We normalize it

      return normalized;
    }

    #endregion

    #region IAssignInParallel<Expression> Members

    public void AssignInParallel(IDictionary<Expression, FList<Expression>> sourcesToTargets)
    {
      throw new Exception("The method or operation is not implemented.");
    }

    #endregion
  }

  /// <summary>
  /// The combination, via reduced product, of a domain for <code>int32</code> and one for booleans
  /// </summary>
  /// <typeparam name="Expression">Is the type of the expressions considered by the abstract domain</typeparam>
  public class CombinedNumericalBooleanDomain<Expression>
      : ReducedCartesianAbstractDomain<INumericalAbstractDomain<Int32, Expression>, IBooleanAbstractDomain<Expression>>,
          IAbstractDomainForEnvironments<Expression>,
          ISymbolicExpressionRefiner<Expression>
  {
    #region Private state
    private IExpressionDecoder<Expression> decoder;
    private IExpressionEncoder<Expression> encoder;

    // left = numerical abstract domain
    // right = boolean abstract domain

    #endregion

    #region Constructor
    /// <param name="decoder">The decoder for expressions</param>
    /// <param name="numerical">The abstract domain for numerical values</param>
    /// <param name="boolean">The abstract domain for booleans</param>
    public CombinedNumericalBooleanDomain(IExpressionDecoder<Expression> decoder, IExpressionEncoder<Expression> encoder, INumericalAbstractDomain<Int32, Expression> numerical, IBooleanAbstractDomain<Expression> boolean)
      : base(numerical, boolean)
    {
      this.decoder = decoder;
      this.encoder = encoder;
    }
    #endregion

    #region Implementation of abstract methods (Reduce, Widening and Factory)

    /// <summary>
    /// For the moment, the reduction does nothing, it just creates a new instance with the two values
    /// In future, we want the abstract domain for booleans to ask the numerical if a given arithmetic identity holds or not
    /// </summary>
    public override ReducedCartesianAbstractDomain<INumericalAbstractDomain<int, Expression>, IBooleanAbstractDomain<Expression>>
        Reduce(INumericalAbstractDomain<int, Expression> left, IBooleanAbstractDomain<Expression> right)
    {
      return Factory(left, right);
    }

    /// <summary>
    /// Pointwise widening
    /// </summary>
    public override IAbstractDomain/*!*/ Widening(IAbstractDomain/*!*/ prev)
    {
      if (this.IsBottom)
        return prev;
      if (this.IsTop)
        return this;
      if (prev.IsBottom)
        return this;
      if (prev.IsTop)
        return prev;

      Debug.Assert(prev is CombinedNumericalBooleanDomain<Expression>, "Expecting an instance of CombinedNumericalBooleanDomain. Found a " + prev.GetType().ToString());
      //^ assert prev is CombinedNumericalBooleanDomain<Expression>;

      CombinedNumericalBooleanDomain<Expression> prevAsCombined = (CombinedNumericalBooleanDomain<Expression>)prev;

      INumericalAbstractDomain<Int32, Expression> widenNumerical = (INumericalAbstractDomain<Int32, Expression>)this.Left.Widening(prevAsCombined.Left);
      IBooleanAbstractDomain<Expression> widenBoolean = (IBooleanAbstractDomain<Expression>)this.Right.Widening(prevAsCombined.Right);

      return Factory(widenNumerical, widenBoolean);
    }

    /// <returns>
    /// A fresh CombinedNumericalBooleanDomain.
    /// If one of the two is bottom, then both are
    /// </returns>
    protected override ReducedCartesianAbstractDomain<INumericalAbstractDomain<int, Expression>, IBooleanAbstractDomain<Expression>> Factory
        (INumericalAbstractDomain<int, Expression> left, IBooleanAbstractDomain<Expression> right)
    {
      // We have to keep the first case in order to avoid an infinite loop (as this.Bottom is defined in terms of Factory...)
      if (left.IsBottom && right.IsBottom)            // If both are bottom, then we return a new abstract element made up of bottoms 
        return new CombinedNumericalBooleanDomain<Expression>(this.decoder, this.encoder, left, right);
      else if (left.IsBottom || right.IsBottom)        // We reduce, if one of the two domains is bottom, then both are
        return (CombinedNumericalBooleanDomain<Expression>)this.Bottom;
      else
        return new CombinedNumericalBooleanDomain<Expression>(this.decoder, this.encoder, left, right);
    }
    #endregion

    #region IPureExpressionAssignments<Expression> Members

    /// <summary>
    /// The variables defined in the two abstract domains (i.e. the union)
    /// </summary>
    public ISet<Expression>/*!*/ Variables
    {
      get
      {
        ISet<Expression> varOnLeft = this.Left.Variables;
        ISet<Expression> varOnRight = this.Right.Variables;

        ISet<Expression> union = new Set<Expression>(varOnLeft);
        union.AddRange(varOnRight);

        return union;
      }
    }

    /// <summary>
    /// Add the variable <code>var</code> to one of the two abstract domains, according to its type
    /// </summary>
    public void AddVariable(Expression/*!*/ var)
    {
      if (decoder.KindOf(var) == ExpressionKind.Integer)
        this.Left.AddVariable(var);
      else if (decoder.KindOf(var) == ExpressionKind.Boolean)
        this.Right.AddVariable(var);
      else
        Debug.Assert(false, "I do not know the type of " + var);
      //^ assert false;
    }

    /// <summary>
    /// The assignment is done on one of the two domains, depending on the type of <code>x</code>
    /// </summary>
    public void Assign(Expression/*!*/ x, Expression/*!*/ exp)
    {
      if (decoder.KindOf(x) == ExpressionKind.Integer)
        this.Left.Assign(x, exp);
      else if (decoder.KindOf(x) == ExpressionKind.Boolean)
        this.Right.Assign(x, exp);
      else
        Debug.Assert(false, "I do not know the type of " + x); //^ assert false;
    }

    /// <summary>
    /// Project the variable from the state
    /// </summary>
    public void ProjectVariable(Expression/*!*/ var)
    {
      if (decoder.KindOf(var) == ExpressionKind.Integer)
        this.Left.ProjectVariable(var);
      else if (decoder.KindOf(var) == ExpressionKind.Boolean)
        this.Right.ProjectVariable(var);
      else
        Debug.Assert(false, "I do not know the type of " + var); //^ assert false;
    }

    /// <summary>
    /// Remove the variable from the state
    /// </summary>
    public void RemoveVariable(Expression/*!*/ var)
    {
      if (decoder.KindOf(var) == ExpressionKind.Integer)
        this.Left.RemoveVariable(var);
      else if (decoder.KindOf(var) == ExpressionKind.Boolean)
        this.Right.RemoveVariable(var);
      else
        Debug.Assert(false, "I do not know the type of " + var); //^ assert false;
    }

    /// <summary>
    /// Rename the variable <code>OldName</code> with <code>NewName</code>
    /// </summary>
    /// <param name="OldName">The old of the variable</param>
    /// <param name="NewName">The new name</param>
    public void RenameVariable(Expression/*!*/ OldName, Expression/*!*/ NewName)
    {
      if (decoder.KindOf(OldName) == ExpressionKind.Integer)
        this.Left.RenameVariable(OldName, NewName);
      else if (decoder.KindOf(OldName) == ExpressionKind.Boolean)
        this.Right.RenameVariable(OldName, NewName);
      else
        Debug.Assert(false, "I do not know the type of " + OldName); //^ assert false;
    }

    #endregion

    #region IPureExpressionTest<Expression> Members

    /// <summary>
    /// The handling of conditionals
    /// </summary>
    /// <param name="guard">The guard to be checked. !!! At this point we assume that it is normalized !!!</param>
    /// <returns>An overapproximation of the states that makes the <code>guard</code> valid in this abstract state</returns>
    public IAbstractDomainForEnvironments<Expression>/*!*/ TestTrue(Expression/*!*/ guard)
    {
      // At this point we assume that the guard has already been normalized...   
      return ProcessTest(guard);
    }

    public IAbstractDomainForEnvironments<Expression>/*!*/ TestFalse(Expression/*!*/ guard)
    {
      IAbstractDomainForEnvironments<Expression>/*!*/ result;

      if (this.encoder != null)
      {
        Expression notGuard = this.encoder.CompoundExpressionFor(ExpressionKind.Boolean, ExpressionOperator.Not, guard);
        Expression normalized = BooleanExpressionsNormalizer<Expression>.Normalize(notGuard, decoder, encoder);
        result = ProcessTest(normalized);
      }
      else
      {
        result = this;
      }
      return result;
    }

    /// <summary>
    /// Check if the expression <code>exp</code> holds, does not hold, is unreachable or we do not know
    /// </summary>
    public FlatAbstractDomain<bool> CheckIfHolds(Expression exp)
    {
      // At this point we assume that the exp has already been normalized
      return ProcessCheckOfExpression(exp);
    }

    #endregion

    #region Private methods : ProcessTestTrue and its helpers
    IAbstractDomainForEnvironments<Expression>/*!*/ ProcessTest(Expression/*!*/  guard)
    {
      CombinedNumericalBooleanDomain<Expression> result = this;
      IAbstractDomainForEnvironments<Expression> leftResult, rightResult;
      CombinedNumericalBooleanDomain<Expression> firstClone, secondClone;
      bool success;

      switch (this.decoder.OperatorFor(guard))        // the handling of tests is by induction on the structure
      {
        #region All the cases
        case ExpressionOperator.Constant:
          if (this.decoder.KindOf(guard) == ExpressionKind.Boolean)
          {
            result = this.decoder.TryValueOf<bool>(guard, ExpressionType.Bool, out success) ? this : (CombinedNumericalBooleanDomain<Expression>)this.Bottom;
          }
          else if (this.decoder.KindOf(guard) == ExpressionKind.Integer)
          {
            result = this.decoder.TryValueOf<int>(guard, ExpressionType.Int32, out success) == 0 ?
                            (CombinedNumericalBooleanDomain<Expression>)this.Bottom
                            : this;
          }
          else
          {
            Debug.Assert(false, "Unkwnown case... " + guard); //^ assert false;
          }
          break;

        case ExpressionOperator.ConvertToInt32:
        case ExpressionOperator.ConvertToUInt8:
        case ExpressionOperator.ConvertToUInt16:
        case ExpressionOperator.ConvertToUInt32:
          // We ignore the conversion to Int32
          result = this;
          break;

        case ExpressionOperator.Variable:
          // It must be a boolean variable?

          if (this.decoder.KindOf(guard) == ExpressionKind.Boolean)
          {
            IBooleanAbstractDomain<Expression> clonedRight = (IBooleanAbstractDomain<Expression>)this.Right.Clone();
            IBooleanAbstractDomain<Expression> resultRight = (IBooleanAbstractDomain<Expression>)clonedRight.TestTrue(guard);

            result = (CombinedNumericalBooleanDomain<Expression>)Factory(this.Left, resultRight);
          }
          else if (this.decoder.KindOf(guard) == ExpressionKind.Integer)
          {
            Expression guardEqZero = this.encoder.CompoundExpressionFor(ExpressionKind.Boolean, ExpressionOperator.Equal, guard, this.encoder.ConstantFor(ExpressionKind.Integer, 0));  // Build the expression guard == 0

            INumericalAbstractDomain<Int32, Expression> clonedLeftForVar = (INumericalAbstractDomain<Int32, Expression>)this.Left.Clone();
            INumericalAbstractDomain<Int32, Expression> resultLeftForVar = (INumericalAbstractDomain<Int32, Expression>)clonedLeftForVar.TestTrue(guardEqZero);

            result = (CombinedNumericalBooleanDomain<Expression>)Factory(resultLeftForVar, this.Right);
          }
          else
          {
            Debug.Assert(false, "I do not knwo how to handle this variable:" + guard.ToString());
            throw new AbstractInterpretationException("Error in the decoding of the variables...");
          }
          break;

        case ExpressionOperator.Not:
          // At this point I assume that the expression has already been simplified by pushing all the negations inside the expression itself.
          // As a consequence, the only Not(e) that I can have is when e == a variable
          Debug.Assert(this.decoder.IsVariable(this.decoder.LeftExpressionFor(guard)), "I was expecting something as !(var), I found " + guard);

          IBooleanAbstractDomain<Expression> clonedRightForNot = (IBooleanAbstractDomain<Expression>)this.Right.Clone();
          IBooleanAbstractDomain<Expression> resultRightForNot = (IBooleanAbstractDomain<Expression>)clonedRightForNot.TestTrue(guard);

          result = (CombinedNumericalBooleanDomain<Expression>)Factory(this.Left, resultRightForNot);

          break;

        case ExpressionOperator.And:            // "&" becomes the meet
          firstClone = (CombinedNumericalBooleanDomain<Expression>)this.Clone();
          secondClone = (CombinedNumericalBooleanDomain<Expression>)this.Clone();

          //^ assert secondClone.Right != firstClone.Right;
          Debug.Assert(secondClone.Right != firstClone.Right, "the Clone operation is supposed to return fresh instances");

          leftResult = (CombinedNumericalBooleanDomain<Expression>)firstClone.ProcessTest(this.decoder.LeftExpressionFor(guard));
          rightResult = (CombinedNumericalBooleanDomain<Expression>)secondClone.ProcessTest(this.decoder.RightExpressionFor(guard));

          result = (CombinedNumericalBooleanDomain<Expression>)leftResult.Meet(rightResult);
          break;

        case ExpressionOperator.Or:             // "||" becomes the join
          firstClone = (CombinedNumericalBooleanDomain<Expression>)this.Clone();
          secondClone = (CombinedNumericalBooleanDomain<Expression>)this.Clone();

          //^ assert secondClone.Right != firstClone.Right;
          Debug.Assert(secondClone.Right != firstClone.Right, "the Clone operation is supposed to return fresh instances");

          leftResult = ProcessTest(this.decoder.LeftExpressionFor(guard));
          rightResult = ProcessTest(this.decoder.RightExpressionFor(guard));

          result = (CombinedNumericalBooleanDomain<Expression>)leftResult.Join(rightResult);
          break;

        case ExpressionOperator.Equal:
          result = HelperForTestTrueEqual(guard);

          break;

        case ExpressionOperator.NotEqual:
          result = HelperForTestTrueNotEqual(guard);

          break;

        case ExpressionOperator.LessThan:
          Debug.Assert(false, "At this point you must have passed through the normalization of boolean expressions to get rid of " + this.decoder.OperatorFor(guard));
          //^ assert false;
          throw new AbstractInterpretationException();


        case ExpressionOperator.LessEqualThan:
          // It must be handled by the numerical abstract domain

          INumericalAbstractDomain<Int32, Expression> clonedLeft = (INumericalAbstractDomain<Int32, Expression>)this.Left.Clone();
          INumericalAbstractDomain<Int32, Expression> resultLeft = (INumericalAbstractDomain<Int32, Expression>)clonedLeft.TestTrue(guard);            // pass the guard to the numerical abstract domain

          result = (CombinedNumericalBooleanDomain<Expression>)Factory(resultLeft, this.Right);
          break;

        case ExpressionOperator.GreaterThan:
        case ExpressionOperator.GreaterEqualThan:
          Debug.Assert(false, "At this point you must have passed through the normalization of boolean expressions to get rid of " + this.decoder.OperatorFor(guard));
          //^ assert false;
          throw new AbstractInterpretationException();


        case ExpressionOperator.Addition:
        case ExpressionOperator.Subtraction:
        case ExpressionOperator.Multiplication:
        case ExpressionOperator.ShiftLeft:
        case ExpressionOperator.ShiftRight:
        case ExpressionOperator.SizeOf:
        case ExpressionOperator.Division:
        case ExpressionOperator.UnaryMinus:
          Debug.Assert(false, "An arithmetic operator cannot appear in a guard, something is wrong? ( " + guard + " )");
          //^ assert false;
          throw new AbstractInterpretationException();

        case ExpressionOperator.Unknown:
          return this;

        #endregion
      }

      return result;
    }

    private CombinedNumericalBooleanDomain<Expression>/*!*/  HelperForTestTrueNotEqual(Expression/*!*/  guard)
    // ^ requires this.decoder.OperatorFor(guard) == ExpressionOperator.Equal;
    {
      Debug.Assert(this.decoder.OperatorFor(guard) == ExpressionOperator.NotEqual, "Expecting a NOT-equality, found " + guard);

      Expression leftExpression = this.decoder.LeftExpressionFor(guard);
      Expression rightExpression = this.decoder.RightExpressionFor(guard);

      INumericalAbstractDomain<Int32, Expression> returnNumericalDomain = this.Left;
      IBooleanAbstractDomain<Expression> returnBooleanDomain = this.Right;

      if (this.decoder.KindOf(leftExpression) == ExpressionKind.Boolean || this.decoder.KindOf(rightExpression) == ExpressionKind.Boolean)
      {   // pass it to the abstraction for booleans
        returnBooleanDomain = (IBooleanAbstractDomain<Expression>)this.Right.TestTrue(guard);
      }
      else
      {   // pass it to the abstraction for Int32
        returnNumericalDomain = (INumericalAbstractDomain<Int32, Expression>)this.Left.TestTrue(guard);
      }

      return (CombinedNumericalBooleanDomain<Expression>)Factory(returnNumericalDomain, returnBooleanDomain);
    }

    /// <summary>
    /// Determines if the guard is a boolean or a numerical equality and then it passes it to the right abstract domain
    /// </summary>
    private CombinedNumericalBooleanDomain<Expression>/*!*/  HelperForTestTrueEqual(Expression/*!*/ guard)
    // ^ requires this.decoder.OperatorFor(guard) == ExpressionOperator.Equal;
    {
      Debug.Assert(this.decoder.OperatorFor(guard) == ExpressionOperator.Equal, "Expecting an equality, found " + guard);

      Expression leftExpression = this.decoder.LeftExpressionFor(guard);
      Expression rightExpression = this.decoder.RightExpressionFor(guard);

      INumericalAbstractDomain<Int32, Expression> returnNumericalDomain = this.Left;
      IBooleanAbstractDomain<Expression> returnBooleanDomain = this.Right;

      if (this.decoder.KindOf(leftExpression) == ExpressionKind.Boolean || this.decoder.KindOf(rightExpression) == ExpressionKind.Boolean)
      {   // pass it to the abstraction for booleans
        returnBooleanDomain = (IBooleanAbstractDomain<Expression>)this.Right.TestTrue(guard);
      }
      else
      {   // pass it to the abstraction for Int32
        returnNumericalDomain = (INumericalAbstractDomain<Int32, Expression>)this.Left.TestTrue(guard);
      }

      return (CombinedNumericalBooleanDomain<Expression>)Factory(returnNumericalDomain, returnBooleanDomain);
    }

    #endregion

    #region Private methods: ProcessCheckOfExpression and its helpers
    static private readonly FlatAbstractDomain<bool> True = new FlatAbstractDomain<bool>(true);
    static private readonly FlatAbstractDomain<bool> False = new FlatAbstractDomain<bool>(false);
    static private readonly FlatAbstractDomain<bool> Unreached = True.Bottom;
    static private readonly FlatAbstractDomain<bool> Unknown = True.Top;

    /// <summary>
    /// Process the checking of expression, by induction on <code>exp</code>
    /// </summary>
    private FlatAbstractDomain<bool>/*!*/ ProcessCheckOfExpression(Expression/*!*/ exp)
    {
      FlatAbstractDomain<bool> result;
      FlatAbstractDomain<bool> leftResult, rightResult;
      bool success;

      switch (this.decoder.OperatorFor(exp))        // the handling of tests is by induction on the structure
      {
        #region All the cases
        case ExpressionOperator.Constant:
          if (this.decoder.KindOf(exp) == ExpressionKind.Boolean)
          {
            result = this.decoder.TryValueOf<bool>(exp, ExpressionType.Bool, out success) ? True : False;
          }
          else if (this.decoder.KindOf(exp) == ExpressionKind.Integer)
          {
            result = this.decoder.TryValueOf<int>(exp, ExpressionType.Bool, out success) == 0 ? False : True;
          }
          else
          {
            Debug.Assert(false, "Unkwnown case... " + exp); //^ assert false;
            throw new AbstractInterpretationException("Unknown case... ");
          }
          break;

        case ExpressionOperator.ConvertToInt32:
        case ExpressionOperator.ConvertToUInt8:
        case ExpressionOperator.ConvertToUInt16:
        case ExpressionOperator.ConvertToUInt32:
          result = this.Left.CheckIfHolds(exp);
          break;

        case ExpressionOperator.Variable:

          if (this.decoder.KindOf(exp) == ExpressionKind.Boolean)
          {
            result = this.Right.CheckIfHolds(exp);
          }
          else if (this.decoder.KindOf(exp) == ExpressionKind.Integer)
          {
            Expression guardEqZero = this.encoder.CompoundExpressionFor(ExpressionKind.Boolean, ExpressionOperator.Equal, exp, this.encoder.ConstantFor(ExpressionKind.Integer, 0));  // Build the expression guard == 0

            result = this.Left.CheckIfHolds(guardEqZero);
          }
          else
          {
            Debug.Assert(false, "I do not knwo how to handle this variable");
            throw new AbstractInterpretationException("Error in the decoding of the variables...");
          }
          break;

        case ExpressionOperator.Not:
          // At this point I assume that the expression has already been simplified by pushing all the negations inside the expression itself.
          // As a consequence, the only Not(e) that I can have is when e == a variable
          Debug.Assert(this.decoder.IsVariable(this.decoder.LeftExpressionFor(exp)), "I was expecting something as !(var), I found " + exp);

          result = this.Right.CheckIfHolds(exp);
          break;

        case ExpressionOperator.And:            // "&" becomes the meet
          leftResult = this.CheckIfHolds(this.decoder.LeftExpressionFor(exp));
          rightResult = this.CheckIfHolds(this.decoder.RightExpressionFor(exp));

          result = HelperForCheckingAnd(leftResult, rightResult);
          break;

        case ExpressionOperator.Or:             // "|" becomes the join
          leftResult = this.CheckIfHolds(this.decoder.LeftExpressionFor(exp));
          rightResult = this.CheckIfHolds(this.decoder.RightExpressionFor(exp));

          result = HelperForCheckingOr(leftResult, rightResult);
          break;

        case ExpressionOperator.Equal:
          result = HelperForCheckingEqual(exp);
          break;

        case ExpressionOperator.NotEqual:
          result = HelperForCheckingNotEqual(exp);
          break;

        case ExpressionOperator.LessThan:
          Debug.Assert(false, "At this point you must have passed through the normalization of boolean expressions to get rid of " + this.decoder.OperatorFor(exp));
          //^ assert false;
          throw new AbstractInterpretationException();

        case ExpressionOperator.LessEqualThan:
          // It must be handled by the numerical abstract domain

          result = this.Left.CheckIfHolds(exp);
          break;

        case ExpressionOperator.GreaterThan:
        case ExpressionOperator.GreaterEqualThan:
          Debug.Assert(false, "At this point you must have passed through the normalization of boolean expressions to get rid of " + this.decoder.OperatorFor(exp));
          //^ assert false;
          throw new AbstractInterpretationException();

        case ExpressionOperator.Addition:
        case ExpressionOperator.Subtraction:
        case ExpressionOperator.SizeOf:
        case ExpressionOperator.ShiftLeft:
        case ExpressionOperator.ShiftRight:
        case ExpressionOperator.Multiplication:
        case ExpressionOperator.Division:
        case ExpressionOperator.UnaryMinus:
          Debug.Assert(false, "An arithmetic operator cannot appear in a guard, something is wrong? ( " + exp + " )");
          //^ assert false;
          throw new AbstractInterpretationException();

        case ExpressionOperator.Unknown:
          return Unknown;

        default:
          throw new AbstractInterpretationException("Unknown case...");
        #endregion
      }

      return result; 
    }

    private FlatAbstractDomain<bool>/*!*/ HelperForCheckingAnd(FlatAbstractDomain<bool>/*!*/ leftResult, FlatAbstractDomain<bool>/*!*/ rightResult)
    {
      // Top is the "strongest"
      if (leftResult.IsTop)
        return leftResult;
      if (rightResult.IsTop)
        return rightResult;
      
      // Then cames unreachability
      if (leftResult.IsBottom)
        return leftResult;
      if (rightResult.IsBottom)
        return rightResult;

      // At this point we just do ||
      return new FlatAbstractDomain<bool>(leftResult.BoxedElement && rightResult.BoxedElement);    
    }

    private FlatAbstractDomain<bool>/*!*/ HelperForCheckingOr(FlatAbstractDomain<bool>/*!*/ leftResult, FlatAbstractDomain<bool>/*!*/ rightResult)
    {
      // Top is the "strongest"
      if (leftResult.IsTop)
        return leftResult;
      if (rightResult.IsTop)
        return rightResult;

      // Then cames unreachability
      if (leftResult.IsBottom)
        return leftResult;
      if (rightResult.IsBottom)
        return rightResult;

      // At this point we just do ||
      return new FlatAbstractDomain<bool>(leftResult.BoxedElement || rightResult.BoxedElement);
    }   
    
    private FlatAbstractDomain<bool>/*!*/ HelperForCheckingNotEqual(Expression/*!*/ guard)
    {
      Debug.Assert(this.decoder.OperatorFor(guard) == ExpressionOperator.NotEqual, "Expecting a NOT-equality, found " + guard);

      Expression leftExpression = this.decoder.LeftExpressionFor(guard);
      Expression rightExpression = this.decoder.RightExpressionFor(guard);

      FlatAbstractDomain<bool> result;

      if (this.decoder.KindOf(leftExpression) == ExpressionKind.Boolean || this.decoder.KindOf(rightExpression) == ExpressionKind.Boolean)
      {   // pass it to the abstraction for booleans
        result = this.Right.CheckIfHolds(guard);
      }
      else
      {   // pass it to the abstraction for Int32
        result = this.Left.CheckIfHolds(guard);
      }

      return result;
    }

    private FlatAbstractDomain<bool>/*!*/ HelperForCheckingEqual(Expression/*!*/guard)
    {
      Debug.Assert(this.decoder.OperatorFor(guard) == ExpressionOperator.Equal, "Expecting an equality, found " + guard);

      Expression leftExpression = this.decoder.LeftExpressionFor(guard);
      Expression rightExpression = this.decoder.RightExpressionFor(guard);

      FlatAbstractDomain<bool> result;

      if (this.decoder.KindOf(leftExpression) == ExpressionKind.Boolean || this.decoder.KindOf(rightExpression) == ExpressionKind.Boolean)
      {   // pass it to the abstraction for booleans
        result = this.Right.CheckIfHolds(guard);
      }
      else
      {   // pass it to the abstraction for Int32
        result = this.Left.CheckIfHolds(guard);
      }

      return result;
    }
    
    #endregion

    #region ISymbolicExpressionRefiner<Expression> Members

    /// <summary>
    /// If <code>x</code> can evaluate to a singleton value, we use this information to refine the symbolic abstract domain
    /// </summary>
    /// <param name="x">The variable to be refined</param>
    /// <param name="refinedValue">The refined value if any. <code>null</code> otherwise</param>
    /// <returns><code>true</code> iff <code>toBeRefined</code> can be refined</returns>
    public bool CanRefine(Expression  x, out Expression refinedValue)
    // ^ requires this.decoder.IsVariable(x);
    {
      Debug.Assert(this.decoder.IsVariable(x), "I know how to refine just variables...");

      ExpressionKind typeOfX = this.decoder.KindOf(x);
      bool result;

      if (typeOfX == ExpressionKind.Integer)
      {
        Interval<Int32> boundsForX = this.Left.BoundsFor(x);

        if (boundsForX.IsSingleton)
        {
          Int32 v = boundsForX.UpperBound;

          refinedValue = this.encoder.ConstantFor(typeOfX, v);
          result = true;
        }
        else
        {
          refinedValue = default(Expression);
          result = false;
        }
      }
      else if (typeOfX == ExpressionKind.Boolean)
      {
        FlatAbstractDomain<bool> valueOfX = this.Right.ValueFor(x);

        if (!valueOfX.IsBottom && !valueOfX.IsTop)
        {
          bool b = valueOfX.BoxedElement;

          refinedValue = this.encoder.ConstantFor(typeOfX, b);
          result = true;
        }
        else
        {
          refinedValue = default(Expression);
          result = false;
        }
      }
      else
      {
        throw new AbstractInterpretationException("unknown type : " + this.decoder.KindOf(x));
      }

      return result;
    }
    #endregion

    #region IAssignInParallel<Expression> Members

    public void AssignInParallel(IDictionary<Expression, FList<Expression>> sourcesToTargets)
    {
      throw new Exception("The method or operation is not implemented.");
    }

    #endregion
  }
}
