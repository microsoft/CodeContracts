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

// An implementation for simple disequalities

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;
using Microsoft.Research.AbstractDomains.Expressions;
using Microsoft.Research.CodeAnalysis;
using Microsoft.Research.DataStructures;

namespace Microsoft.Research.AbstractDomains.Numerical
{
  // We keep constraints in the form x != a, b, ... , with x a variable and a, b, ... being a finite set of rationals
  public class SimpleDisequalities<Variable, Expression>
    : FunctionalAbstractDomain<SimpleDisequalities<Variable, Expression>, Variable, SetOfConstraints<Rational>>,
      INumericalAbstractDomain<Variable, Expression>
  {
    #region Private state

    private readonly SimpleDisequalitiesCheckIfHoldsVisitor /*!*/ checkIfHoldsVisitor;
    private readonly IExpressionDecoder<Variable, Expression> /*!*/ decoder;
    private readonly IExpressionEncoder<Variable, Expression> /*!*/ encoder;

    private readonly ConstantVisitor /*!*/ evalConstant;

    private readonly SimpleDisequalitiesTestFalseVisitor /*!*/ testFalseVisitor;
    private readonly SimpleDisequalitiesTestTrueVisitor /*!*/ testTrueVisitor;

    #endregion

    #region Constructors

    /// <summary>
    ///   Construct a new domain
    /// </summary>
    public SimpleDisequalities(IExpressionDecoder<Variable, Expression> /*!*/ decoder,
                               IExpressionEncoder<Variable, Expression> /*!*/ encoder)
    {
      this.decoder = decoder;
      this.encoder = encoder;

      evalConstant = new ConstantVisitor(decoder);

      testTrueVisitor = new SimpleDisequalitiesTestTrueVisitor(this.decoder, evalConstant);
      testFalseVisitor = new SimpleDisequalitiesTestFalseVisitor(this.decoder);

      testTrueVisitor.FalseVisitor = testFalseVisitor;
      testFalseVisitor.TrueVisitor = testTrueVisitor;

      checkIfHoldsVisitor = new SimpleDisequalitiesCheckIfHoldsVisitor(this.decoder, evalConstant);
    }

    private SimpleDisequalities(SimpleDisequalities<Variable, Expression> original)
      : base(original)
    {
      decoder = original.decoder;
      encoder = original.encoder;

      evalConstant = original.evalConstant;

      testTrueVisitor = original.testTrueVisitor;
      testFalseVisitor = original.testFalseVisitor;

      checkIfHoldsVisitor = original.checkIfHoldsVisitor;
    }

    #endregion

    #region Overriding the accessors

    public override SetOfConstraints<Rational> this[Variable index]
    {
      get
      {
        if (ContainsKey(index))
        {
          return base[index];
        }
        else
        {
          return new SetOfConstraints<Rational>(Rational.For(-30)).Top; // Top
        }
      }
      set
      {
        if (decoder.IsSlackOrFrameworkVariable(index))
        {
          base[index] = value;
        }
      }
    }

    #endregion

    #region Implementation of abstract methods

    public override object Clone()
    {
      return new SimpleDisequalities<Variable, Expression>(this);
    }

    protected override SimpleDisequalities<Variable, Expression> Factory()
    {
      return new SimpleDisequalities<Variable, Expression>(decoder, encoder);
    }

    // To be implemented
    protected override string ToLogicalFormula(Variable d, SetOfConstraints<Rational> c)
    {
      return "1";
    }

    protected override T To<T>(Variable d, SetOfConstraints<Rational> c, IFactory<T> factory)
    {
      if (c.IsBottom)
        return factory.Constant(false);

      if (c.IsTop)
        return factory.Constant(true);

      T result = factory.IdentityForAnd;
      T x = factory.Variable(d);

      foreach (Rational r in c.Values)
      {
        result = factory.And(result, factory.NotEqualTo(x, factory.Constant(r)));
      }

      return result;
    }

    #endregion

    #region INumericalAbstractDomain<Variable, Expression> Members

    public void Assign(Expression x, Expression exp, INumericalAbstractDomainQuery<Variable, Expression> preState)
    {
      Interval intvForExp = preState.BoundsFor(exp).AsInterval;

      Variable xVar = decoder.UnderlyingVariable(x);

      AssumeInInterval(xVar, intvForExp);
    }

    public void AssumeInDisInterval(Variable x, DisInterval values)
    {
      AssumeInInterval(x, values.AsInterval);
    }

    INumericalAbstractDomain<Variable, Expression> INumericalAbstractDomain<Variable, Expression>.TestTrueEqual(
      Expression left, Expression right)
    {
      return TestTrueEqual(left, right);
    }

    INumericalAbstractDomain<Variable, Expression> INumericalAbstractDomain<Variable, Expression>.TestTrueGeqZero(
      Expression exp)
    {
      return TestTrueGeqZero(exp);
    }

    INumericalAbstractDomain<Variable, Expression> INumericalAbstractDomain<Variable, Expression>.TestTrueLessThan(
      Expression left, Expression right)
    {
      return TestTrueLessThan(left, right);
    }

    INumericalAbstractDomain<Variable, Expression> INumericalAbstractDomain<Variable, Expression>.TestTrueLessEqualThan(
      Expression left, Expression right)
    {
      return TestTrueLessEqualThan(left, right);
    }

    /// <summary>
    ///   Adds the constraints x != value.LowerBound-1 and x != value.UpperBound+1
    /// </summary>
    public void AssumeInInterval(Variable x, Interval value)
    {
      if (value.IsNormal)
      {
        var strictBounds = new Set<Rational>(2);
        if (!value.LowerBound.IsInfinity)
        {
          try
          {
            strictBounds.Add(value.LowerBound - 1);
          }
          catch (ArithmeticExceptionRational)
          {
            // It may be the case that value.UpperBound is too large, so we catch it
            // and do nothing (i.e. we abstract)
          }
        }

        if (!value.UpperBound.IsInfinity)
        {
          try
          {
            strictBounds.Add(value.UpperBound + 1);
          }

          catch (ArithmeticExceptionRational)
          {
            // It may be the case that value.UpperBound is too large, so we catch it
            // and do nothing (i.e. we abstract)
          }
        }

        // Check if zero is not included. In the case, we want to add the constraint x != 0
        if (value.DoesNotInclude(0))
        {
          strictBounds.Add(Rational.For(0));
        }

        // It may be the case that the interval is e.g. [+oo, +oo], so we cannot blindly add the matrixes
        if (strictBounds.Count > 0)
        {
          this[x] = new SetOfConstraints<Rational>(strictBounds);
        }
      }
    }

    public SimpleDisequalities<Variable, Expression> TestTrueGeqZero(Expression exp)
    {
      // exp >= 0 => exp != -1  (and also -2, ... but we ignore it)

      Variable expVar = decoder.UnderlyingVariable(exp);

      this[expVar] = this[expVar].Meet(new SetOfConstraints<Rational>(Rational.For(-1)));

      return this;
    }


    public SimpleDisequalities<Variable, Expression> TestTrueLessThan(Expression left, Expression right)
    {
      SimpleDisequalities<Variable, Expression> result = this;

      evalConstant.Visit(right);

      if (evalConstant.HasValue)
      {
        Rational v = evalConstant.Result;

        Variable leftVar = decoder.UnderlyingVariable(left);

        // left < k => left != k (also k+1, k+2, k+3, etc. but we do not care ...) 

        SetOfConstraints<Rational> newConstraintsForLeft = this[leftVar].Meet(new SetOfConstraints<Rational>(v));
        result[leftVar] = newConstraintsForLeft;
      }

      evalConstant.Visit(left);

      if (evalConstant.HasValue)
      {
        Rational v = evalConstant.Result;

        Variable rightVar = decoder.UnderlyingVariable(right);

        // k < right => right != k (also k-1, k-2, etc. but we do not care ...)
        SetOfConstraints<Rational> newConstraintsForRight = this[rightVar].Meet(new SetOfConstraints<Rational>(v));
        result[rightVar] = newConstraintsForRight;
      }

      return result;
    }

    public SimpleDisequalities<Variable, Expression> TestTrueLessEqualThan(Expression left, Expression right)
    {
      SimpleDisequalities<Variable, Expression> result = this;

      evalConstant.Visit(right);

      if (evalConstant.HasValue)
      {
        Rational v = evalConstant.Result;

        Variable leftVar = decoder.UnderlyingVariable(left);

        // left <= k => left != k + 1 (also k+2, k+3, etc. but we do not care ...) 
        SetOfConstraints<Rational> newConstraintsForLeft = this[leftVar].Meet(new SetOfConstraints<Rational>(v + 1));
        result[leftVar] = newConstraintsForLeft;
      }

      evalConstant.Visit(left);

      if (evalConstant.HasValue)
      {
        Rational v = evalConstant.Result;

        Variable rightVar = decoder.UnderlyingVariable(right);

        // k <= right => right != k - 1 (also k - 2, k - 3, etc. but we do not care ...)
        SetOfConstraints<Rational> newConstraintsForRight = this[rightVar].Meet(new SetOfConstraints<Rational>(v - 1));
        result[rightVar] = newConstraintsForRight;
      }

      return result;
    }

    public SimpleDisequalities<Variable, Expression> TestTrueEqual(Expression left, Expression right)
    {
      return testTrueVisitor.VisitEqual(left, right, default(Expression), this);
    }

    #endregion

    #region IAbstractDomainForEnvironments<Variable, Expression> Members

    /// <summary>
    ///   Pretty print the expression
    /// </summary>
    public string ToString(Expression exp)
    {
      return ExpressionPrinter.ToString(exp, decoder);
    }

    #endregion

    #region IPureExpressionAssignments<Expression> Members

    public List<Variable> Variables
    {
      get { return new List<Variable>(Keys); }
    }

    public void AddVariable(Variable var)
    {
      // Does nothing ...
    }

    public void Assign(Expression x, Expression exp)
    {
      Assign(x, exp, TopNumericalDomain<Variable, Expression>.Singleton);
    }

    public void ProjectVariable(Variable var)
    {
      RemoveVariable(var);
    }

    public void RemoveVariable(Variable var)
    {
      if (ContainsKey(var))
        RemoveElement(var);
    }

    public void RenameVariable(Variable OldName, Variable NewName)
    {
      if (ContainsKey(OldName))
      {
        this[NewName] = this[OldName];
        RemoveVariable(OldName);
      }
    }

    #endregion

    #region IPureExpressionTest<Expression> Members

    IAbstractDomainForEnvironments<Variable, Expression> IPureExpressionTest<Variable, Expression>.TestTrue(
      Expression guard)
    {
      return TestTrue(guard);
    }

    IAbstractDomainForEnvironments<Variable, Expression> IPureExpressionTest<Variable, Expression>.TestFalse(
      Expression guard)
    {
      return TestFalse(guard);
    }

    public FlatAbstractDomain<bool> CheckIfHolds(Expression exp)
    {
      return checkIfHoldsVisitor.Visit(exp, this);
    }

    void IPureExpressionTest<Variable, Expression>.AssumeDomainSpecificFact(DomainSpecificFact fact)
    {
      AssumeDomainSpecificFact(fact);
    }

    public INumericalAbstractDomain<Variable, Expression> RemoveRedundanciesWith(
      INumericalAbstractDomain<Variable, Expression> oracle)
    {
      var result = new SimpleDisequalities<Variable, Expression>(decoder, encoder);

      if (encoder != null)
      {
        foreach (var x_pair in Elements)
        {
          SetOfConstraints<Rational> k = x_pair.Value;

          if (k.IsBottom || k.IsTop)
          {
            result[x_pair.Key] = k;
          }
          else
          {
            Expression xExp = encoder.VariableFor(x_pair.Key);
            foreach (Rational exp in k.Values)
            {
              Expression notEq = encoder.CompoundExpressionFor(ExpressionType.Bool, ExpressionOperator.NotEqual, xExp,
                                                               exp.ToExpression(encoder));

              FlatAbstractDomain<bool> check = oracle.CheckIfHolds(notEq);

              if (check.IsBottom || check.IsTop || !check.BoxedElement)
              {
                // If it is not implied by the oracle, we give up
                result = result.TestTrue(notEq);
              }
            }
          }
        }
      }

      return result;
    }

    public SimpleDisequalities<Variable, Expression> TestTrue(Expression guard)
    {
      return testTrueVisitor.Visit(guard, this);
    }

    public SimpleDisequalities<Variable, Expression> TestFalse(Expression guard)
    {
      return testFalseVisitor.Visit(guard, this);
    }

    #endregion

    #region IAssignInParallel<Expression> Members

    public void AssignInParallel(Dictionary<Variable, FList<Variable>> sourcesToTargets,
                                 Converter<Variable, Expression> convert)
    {
      var result = new SimpleDisequalities<Variable, Expression>(decoder, encoder);

      foreach (Variable e in Keys)
      {
        if (decoder.IsSlackVariable(e))
        {
          result[e] = this[e];
        }
      }

      State = AbstractState.Normal;

      if (sourcesToTargets.Count == 0)
      {
        // do nothing...
      }
      else
      {
        // Update the values
        foreach (Variable exp in sourcesToTargets.Keys)
        {
          SetOfConstraints<Rational> value = this[exp];

          foreach (Variable target in sourcesToTargets[exp].GetEnumerable())
          {
            if (!value.IsTop)
            {
              result[target] = value;
            }
          }
        }
      }

      ClearElements();
      foreach (Variable e in result.Keys)
      {
        this[e] = result[e];
      }
    }

    #endregion

    #region INumericalAbstractDomainQuery<Variable, Expression> Members

    public DisInterval BoundsFor(Expression v)
    {
      return DisInterval.UnknownInterval;
    }

    public DisInterval BoundsFor(Variable v)
    {
      return DisInterval.UnknownInterval;
    }

    public List<Pair<Variable, Int32>> IntConstants
    {
      get { return new List<Pair<Variable, Int32>>(); }
    }

    public IEnumerable<Expression> LowerBoundsFor(Expression v, bool strict)
    {
      yield break;
    }

    public IEnumerable<Expression> UpperBoundsFor(Expression v, bool strict)
    {
      yield break;
    }

    public IEnumerable<Expression> LowerBoundsFor(Variable v, bool strict)
    {
      yield break;
    }

    public IEnumerable<Expression> UpperBoundsFor(Variable v, bool strict)
    {
      yield break;
    }

    public IEnumerable<Variable> EqualitiesFor(Variable v)
    {
      return new Set<Variable>();
    }

    public FlatAbstractDomain<bool> CheckIfGreaterEqualThanZero(Expression exp)
    {
      return CheckOutcome.Top;
    }

    public FlatAbstractDomain<bool> CheckIfLessThan(Expression e1, Expression e2)
    {
      return CheckOutcome.Top;
    }

    public FlatAbstractDomain<bool> CheckIfLessThanIncomplete(Expression e1, Expression e2)
    {
      return CheckOutcome.Top;
    }

    public FlatAbstractDomain<bool> CheckIfLessThan_Un(Expression e1, Expression e2)
    {
      return CheckIfLessThan(e1, e2);
    }

    public FlatAbstractDomain<bool> CheckIfLessEqualThan(Expression e1, Expression e2)
    {
      return CheckOutcome.Top;
    }

    public FlatAbstractDomain<bool> CheckIfLessEqualThan_Un(Expression e1, Expression e2)
    {
      return CheckIfLessEqualThan(e1, e2);
    }

    public FlatAbstractDomain<bool> CheckIfEqual(Expression e1, Expression e2)
    {
      return CheckOutcome.Top;
    }

    public FlatAbstractDomain<bool> CheckIfEqualIncomplete(Expression e1, Expression e2)
    {
      return CheckOutcome.Top;
    }

    public FlatAbstractDomain<bool> CheckIfLessThan(Variable e1, Variable e2)
    {
      return CheckOutcome.Top;
    }

    public FlatAbstractDomain<bool> CheckIfLessEqualThan(Variable e1, Variable e2)
    {
      return CheckOutcome.Top;
    }

    /// <returns>True iff 0 \in this[e]</returns>
    public FlatAbstractDomain<bool> CheckIfNonZero(Expression e)
    {
      Variable eVar = decoder.UnderlyingVariable(e);
      if (ContainsKey(eVar) && this[eVar].IsNormal())
      {
        return this[eVar].Contains(Rational.For(0)) ? CheckOutcome.True : CheckOutcome.Top;
      }

      return CheckOutcome.Top;
    }

    public Variable ToVariable(Expression exp)
    {
      return decoder.UnderlyingVariable(exp);
    }

    #endregion

    #region Tostring

    public override string ToString()
    {
      if (IsBottom)
        return "_|_";

      if (IsTop)
        return "Top (disequalities)";

      var result = new StringBuilder();

      foreach (Variable x in Keys)
      {
        SetOfConstraints<Rational> value = this[x];
        string toAppend = x + " ";

        if (value.IsBottom)
        {
          toAppend += ": _|_,";
        }
        else if (value.IsTop)
        {
          toAppend += ": {},";
        }
        else
        {
          toAppend += "!= ";
          foreach (Rational r in value.Values)
            toAppend += r + ", ";
        }

        result.AppendLine(toAppend);
      }

      return result.ToString();
    }

    #endregion

    #region Floating point types

    public void SetFloatType(Variable v, ConcreteFloat f)
    {
      // does nothing
    }

    public FlatAbstractDomain<ConcreteFloat> GetFloatType(Variable v)
    {
      return FloatTypes<Variable, Expression>.Unknown;
    }

    #endregion

    #region Visitors

    private class ConstantVisitor
      : GenericTypeExpressionVisitor<Variable, Expression, Unit, bool>
    {
      private bool hasValue;
      private Rational result;

      public ConstantVisitor(IExpressionDecoder<Variable, Expression> decoder)
        : base(decoder)
      {
        hasValue = false;
        result = default(Rational);
      }

      public Rational Result
      {
        get
        {
          Contract.Requires(HasValue);
          return result;
        }
      }

      public bool HasValue
      {
        get { return hasValue; }
      }

      public bool Visit(Expression exp)
      {
        hasValue = false;
        result = default(Rational);

        if (Decoder.IsConstant(exp))
        {
          return base.Visit(exp, Unit.Value);
        }

        return Default(exp);
      }

      public override bool VisitFloat32(Expression exp, Unit input)
      {
        Single value;

        if (Decoder.TryValueOf(exp, ExpressionType.Float32, out value) && Rational.CanRepresentExactly(value))
        {
          result = Rational.For(value);
          hasValue = true;
        }

        return hasValue;
      }

      public override bool VisitFloat64(Expression exp, Unit input)
      {
        Double value;

        if (Decoder.TryValueOf(exp, ExpressionType.Float64, out value) && Rational.CanRepresentExactly(value))
        {
          result = Rational.For(value);
          hasValue = true;
        }

        return hasValue;
      }

      public override bool VisitInt8(Expression exp, Unit input)
      {
        SByte value;

        if (Decoder.TryValueOf(exp, ExpressionType.Int8, out value))
        {
          result = Rational.For(value);
          hasValue = true;
        }

        return hasValue;
      }

      public override bool VisitInt16(Expression exp, Unit input)
      {
        Int16 value;

        if (Decoder.TryValueOf(exp, ExpressionType.Int16, out value))
        {
          result = Rational.For(value);
          hasValue = true;
        }

        return hasValue;
      }

      public override bool VisitInt32(Expression exp, Unit input)
      {
        Int32 value;

        if (Decoder.TryValueOf(exp, ExpressionType.Int32, out value))
        {
          result = Rational.For(value);
          hasValue = true;
        }

        return hasValue;
      }

      public override bool VisitInt64(Expression exp, Unit input)
      {
        Int64 value;

        if (Decoder.TryValueOf(exp, ExpressionType.Int64, out value) && Rational.CanRepresentExactly(value))
        {
          result = Rational.For(value);
          hasValue = true;
        }

        return hasValue;
      }

      public override bool VisitUInt8(Expression exp, Unit input)
      {
        Byte value;

        if (Decoder.TryValueOf(exp, ExpressionType.UInt8, out value))
        {
          result = Rational.For(value);
          hasValue = true;
        }

        return hasValue;
      }

      public override bool VisitUInt16(Expression exp, Unit input)
      {
        UInt16 value;

        if (Decoder.TryValueOf(exp, ExpressionType.UInt16, out value))
        {
          result = Rational.For(value);
          hasValue = true;
        }
        return hasValue;
      }

      public override bool VisitUInt32(Expression exp, Unit input)
      {
        UInt32 value;

        if (Decoder.TryValueOf(exp, ExpressionType.UInt32, out value))
        {
          result = Rational.For(value);
          hasValue = true;
        }
        return hasValue;
      }

      public override bool Default(Expression exp)
      {
        result = default(Rational);
        hasValue = false;

        return false;
      }
    }

    private class SimpleDisequalitiesCheckIfHoldsVisitor :
      CheckIfHoldsVisitor<SimpleDisequalities<Variable, Expression>, Variable, Expression>
    {
      #region Private

      private readonly ConstantVisitor evalConstant;

      #endregion

      #region Constructor

      public SimpleDisequalitiesCheckIfHoldsVisitor(IExpressionDecoder<Variable, Expression> decoder,
                                                    ConstantVisitor evalConstant)
        : base(decoder)
      {
        this.evalConstant = evalConstant;
      }

      #endregion

      public override FlatAbstractDomain<bool> VisitNotEqual(Expression left, Expression right, Expression original,
                                                             FlatAbstractDomain<bool> data)
      {
        if (left.Equals(right) && !Decoder.IsNaN(left) && !Decoder.IsNaN(right))
        {
          return CheckOutcome.False;
        }

        evalConstant.Visit(left);

        bool leftIsConstZero = evalConstant.HasValue && evalConstant.Result.IsZero;

        Variable rightVar = Decoder.UnderlyingVariable(right);

        if (evalConstant.HasValue && Domain.ContainsKey(rightVar) && Domain[rightVar].IsNormal())
        {
          if (Domain[rightVar].Contains(evalConstant.Result))
          {
            return CheckOutcome.True;
          }
        }

        evalConstant.Visit(right);

        bool rightIsConstZero = evalConstant.HasValue && evalConstant.Result.IsZero;

        Variable leftVar = Decoder.UnderlyingVariable(left);

        if (evalConstant.HasValue && Domain.ContainsKey(leftVar) && Domain[leftVar].IsNormal())
        {
          rightIsConstZero = evalConstant.Result.IsZero;

          if (Domain[leftVar].Contains(evalConstant.Result))
          {
            return CheckOutcome.True;
          }
        }

        if (leftIsConstZero)
        {
          return Visit(right, data);
        }
        if (rightIsConstZero)
        {
          return Visit(left, data);
        }

        return CheckOutcome.Top;
      }

      public override FlatAbstractDomain<bool> VisitEqual(Expression left, Expression right, Expression original,
                                                          FlatAbstractDomain<bool> data)
      {
        ExpressionType leftType = Decoder.TypeOf(left);
        ExpressionType rightType = Decoder.TypeOf(right);

        if (!leftType.IsFloatingPointType() && !rightType.IsFloatingPointType())
        {
          if (left.Equals(right) && !Decoder.IsNaN(left) && !Decoder.IsNaN(right))
          {
            return CheckOutcome.True;
          }
        }

        evalConstant.Visit(left);

        Variable rightVar = Decoder.UnderlyingVariable(right);

        if (evalConstant.HasValue && Domain.ContainsKey(rightVar) && Domain[rightVar].IsNormal())
        {
          if (Domain[rightVar].Contains(evalConstant.Result))
          {
            return CheckOutcome.False;
          }
        }

        evalConstant.Visit(right);

        Variable leftVar = Decoder.UnderlyingVariable(right);

        if (evalConstant.HasValue && Domain.ContainsKey(leftVar) && Domain[leftVar].IsNormal())
        {
          if (Domain[leftVar].Contains(evalConstant.Result))
          {
            return CheckOutcome.False;
          }
        }

        return CheckOutcome.Top;
      }

      public override FlatAbstractDomain<bool> VisitEqual_Obj(Expression left, Expression right, Expression original,
                                                              FlatAbstractDomain<bool> data)
      {
        return VisitEqual(left, right, original, data);
      }

      public override FlatAbstractDomain<bool> VisitLessEqualThan(Expression left, Expression right, Expression original,
                                                                  FlatAbstractDomain<bool> data)
      {
        return Domain.CheckIfLessEqualThan(left, right);
      }

      public override FlatAbstractDomain<bool> VisitLessThan(Expression left, Expression right, Expression original,
                                                             FlatAbstractDomain<bool> data)
      {
        return Domain.CheckIfLessThan(left, right);
      }


      /// <summary>
      ///   left * right != 0 ??
      /// </summary>
      public override FlatAbstractDomain<bool> VisitMultiplication(Expression left, Expression right,
                                                                   Expression original, FlatAbstractDomain<bool> data)
      {
        FlatAbstractDomain<bool> leftIsZero = IsNotZero(left);
        FlatAbstractDomain<bool> rightIsZero = IsNotZero(right);

        if (leftIsZero.IsNormal() && rightIsZero.IsNormal())
        {
          // One of the two is zero ...
          if (leftIsZero.IsFalse() || rightIsZero.IsFalse())
          {
            return CheckOutcome.False;
          }
          // Both are non zero
          if (leftIsZero.IsTrue() && rightIsZero.IsTrue())
          {
            return CheckOutcome.True;
          }
        }

        if (leftIsZero.IsBottom || rightIsZero.IsBottom)
        {
          return CheckOutcome.Bottom;
        }


        return CheckOutcome.Top;
      }

      /// <summary>
      ///   left / right != 0 ??
      /// </summary>
      public override FlatAbstractDomain<bool> VisitDivision(Expression left, Expression right, Expression original,
                                                             FlatAbstractDomain<bool> data)
      {
        return IsNotZero(left);
      }

      /// <summary>
      ///   left % right != 0 ??
      /// </summary>
      public override FlatAbstractDomain<bool> VisitModulus(Expression left, Expression right, Expression original,
                                                            FlatAbstractDomain<bool> data)
      {
        // We do not check if left == k * right, for some k
        return IsNotZero(left);
      }

      public override FlatAbstractDomain<bool> VisitUnaryMinus(Expression left, Expression original,
                                                               FlatAbstractDomain<bool> data)
      {
        return IsNotZero(left);
      }

      private FlatAbstractDomain<bool> IsNotZero(Expression x)
      {
        // First try: Is it a constant?
        evalConstant.Visit(x);
        if (evalConstant.HasValue)
        {
          if (evalConstant.Result.IsZero)
          {
            return CheckOutcome.False;
          }
          else
          {
            return CheckOutcome.True;
          }
        }

        // Second try: Do we have some propagated information

        Variable xVar = Decoder.UnderlyingVariable(x);

        SetOfConstraints<Rational> constraints;

        if (Domain.TryGetValue(xVar, out constraints))
        {
          // We know it is != 0 for sure?
          if (constraints.IsNormal() && constraints.Contains(Rational.For(0)))
          {
            return CheckOutcome.True;
          }
          if (constraints.IsBottom)
          {
            return CheckOutcome.Bottom;
          }
        }

        // we do not know...
        return CheckOutcome.Top;
      }
    }

    private class SimpleDisequalitiesTestFalseVisitor :
      TestFalseVisitor<SimpleDisequalities<Variable, Expression>, Variable, Expression>
    {
      #region Constructor

      public SimpleDisequalitiesTestFalseVisitor(IExpressionDecoder<Variable, Expression> decoder)
        : base(decoder)
      {
      }

      #endregion

      #region Implementation of abstract methods

      public override SimpleDisequalities<Variable, Expression> VisitVariable(Variable variable, Expression original,
                                                                              SimpleDisequalities<Variable, Expression>
                                                                                data)
      {
        return data;
      }

      public override SimpleDisequalities<Variable, Expression> VisitEqual_Obj(Expression left, Expression right,
                                                                               Expression original,
                                                                               SimpleDisequalities<Variable, Expression>
                                                                                 data)
      {
        return VisitEqual(left, right, original, data);
      }

      #endregion
    }

    private class SimpleDisequalitiesTestTrueVisitor :
      TestTrueVisitor<SimpleDisequalities<Variable, Expression>, Variable, Expression>
    {
      #region Private state

      private readonly ConstantVisitor evalConstant;

      #endregion

      #region Constructors

      public SimpleDisequalitiesTestTrueVisitor(IExpressionDecoder<Variable, Expression> decoder,
                                                ConstantVisitor evalConstant)
        : base(decoder)
      {
        this.evalConstant = evalConstant;
      }

      #endregion

      #region Overridden

      public override SimpleDisequalities<Variable, Expression> Visit(Expression exp,
                                                                      SimpleDisequalities<Variable, Expression> data)
      {
        SimpleDisequalities<Variable, Expression> result = base.Visit(exp, data);

        // We also know that exp != 0
        var expNotZero = new SetOfConstraints<Rational>(Rational.For(0));
        SetOfConstraints<Rational> prev;

        Variable expVar = Decoder.UnderlyingVariable(exp);

        if (result.TryGetValue(expVar, out prev))
        {
          result[expVar] = prev.Meet(expNotZero);
        }
        else
        {
          result[expVar] = expNotZero;
        }

        return result;
      }

      #endregion

      #region Implementation of abstract mehtods

      public override SimpleDisequalities<Variable, Expression> VisitEqual(Expression left, Expression right,
                                                                           Expression original,
                                                                           SimpleDisequalities<Variable, Expression>
                                                                             data)
      {
        evalConstant.Visit(right);

        Variable leftVar = Decoder.UnderlyingVariable(left);

        if (data.ContainsKey(leftVar) && data[leftVar].IsNormal() && evalConstant.HasValue)
        {
          foreach (Rational r in data[leftVar].Values)
          {
            // we know left != r, so we want that intvForRight != r
            if (r == evalConstant.Result)
              return data.Bottom;
          }
        }

        evalConstant.Visit(left);

        Variable rightVar = Decoder.UnderlyingVariable(right);

        if (data.ContainsKey(rightVar) && data[rightVar].IsNormal() && evalConstant.HasValue)
        {
          foreach (Rational r in data[rightVar].Values)
          {
            // we know right != r, so we want that intvForLeft != r
            if (r == evalConstant.Result)
              return data.Bottom;
          }
        }

        // At this point we know that we do not have simple contraddictions. 
        // Now we can say that left and right have the same inequalities
        SetOfConstraints<Rational> unionOfConstraints = data[leftVar].Meet(data[rightVar]);

        if (!unionOfConstraints.IsTop)
        {
          data[leftVar] = unionOfConstraints;
          data[rightVar] = unionOfConstraints;
        }

        return data;
      }

      public override SimpleDisequalities<Variable, Expression> VisitEqual_Obj(Expression left, Expression right,
                                                                               Expression original,
                                                                               SimpleDisequalities<Variable, Expression>
                                                                                 data)
      {
        return VisitEqual(left, right, original, data);
      }

      public override SimpleDisequalities<Variable, Expression> VisitLessEqualThan(Expression left, Expression right,
                                                                                   Expression original,
                                                                                   SimpleDisequalities
                                                                                     <Variable, Expression> data)
      {
        return data.TestTrueLessEqualThan(left, right);
      }

      public override SimpleDisequalities<Variable, Expression> VisitLessThan(Expression left, Expression right,
                                                                              Expression original,
                                                                              SimpleDisequalities<Variable, Expression>
                                                                                data)
      {
        return data.TestTrueLessThan(left, right);
      }

      public override SimpleDisequalities<Variable, Expression> VisitNotEqual(Expression left, Expression right,
                                                                              Expression original,
                                                                              SimpleDisequalities<Variable, Expression>
                                                                                data)
      {
        SimpleDisequalities<Variable, Expression> result = data;

        evalConstant.Visit(Decoder.Stripped(right));

        if (evalConstant.HasValue)
        {
          // left != k 

          data = Update(left, evalConstant.Result, data);
          data = Update(Decoder.Stripped(left), evalConstant.Result, data);
        }

        evalConstant.Visit(Decoder.Stripped(left));
        if (evalConstant.HasValue)
        {
          // right != k
          var newConstraintsForRight = new SetOfConstraints<Rational>(evalConstant.Result);

          // check if k != 0. If so we add the constraint right != 0
          if (!evalConstant.Result.IsZero)
          {
            newConstraintsForRight = newConstraintsForRight.Meet(new SetOfConstraints<Rational>(Rational.For(0)));
          }

          Variable rightVar = Decoder.UnderlyingVariable(right);
          SetOfConstraints<Rational> prev;

          if (result.TryGetValue(rightVar, out prev))
            newConstraintsForRight = newConstraintsForRight.Meet(prev);

          result[rightVar] = newConstraintsForRight;
        }

        return result;
      }

      public override SimpleDisequalities<Variable, Expression> VisitVariable(Variable var, Expression original,
                                                                              SimpleDisequalities<Variable, Expression>
                                                                                data)
      {
        return data;
      }

      #endregion

      #region Utils

      private SimpleDisequalities<Variable, Expression> Update(Expression exp, Rational k,
                                                               SimpleDisequalities<Variable, Expression> data)
      {
        var newConstraints = new SetOfConstraints<Rational>(k);

        SetOfConstraints<Rational> prev;
        Variable var = Decoder.UnderlyingVariable(exp);

        if (data.TryGetValue(var, out prev))
        {
          newConstraints = newConstraints.Meet(prev);
        }

        data[var] = newConstraints;

        return data;
      }

      #endregion
    }

    #endregion
  }
}