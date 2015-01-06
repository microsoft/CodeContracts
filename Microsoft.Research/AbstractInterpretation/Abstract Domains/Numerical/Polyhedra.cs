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

#if POLYHEDRA
using System;
using System.Collections.Generic;
using Microsoft.Boogie.AbstractInterpretation;
using AI = Microsoft.AbstractInterpretationFramework;
using Microsoft.Research.AbstractDomains.Expressions;
using System.Collections;
using Microsoft.Research.AbstractDomains;
using Microsoft.Research.AbstractDomains.Numerical;
using Microsoft.Research.DataStructures;

namespace Microsoft.Research.AbstractDomains.Numerical
{
  class UnknownType : AI.AIType
  {
    public UnknownType()
    { }
  }

  static class UnknownFunctionSymbol
  {
    private static AI.FunctionSymbol value;
    static UnknownFunctionSymbol()
    {
      value = new AI.FunctionSymbol(new UnknownType());
    }

    static public AI.FunctionSymbol Value
    {
      get
      {
        return value;
      }
    }
  }

  class Converter
  {
    static public Exp<E> Box<E>(E exp, IExpressionDecoder<E> decoder)
    {
      if (decoder.IsVariable(exp))
        return new Var<E>(exp, decoder);
      else
      {
        switch (decoder.OperatorFor(exp))
        {
          case ExpressionOperator.ConvertToInt32:
          case ExpressionOperator.ConvertToUInt16:
          case ExpressionOperator.ConvertToUInt32:
          case ExpressionOperator.ConvertToUInt8:
            return Box(decoder.LeftExpressionFor(exp), decoder);
        }
        return new Exp<E>(exp, decoder);
      }
    }

    static public Var<E> BoxAsVariable<E>(E exp, IExpressionDecoder<E> decoder)
    {
      return new Var<E>(exp, decoder);
    }

    static public E Unbox<E>(AI.IExpr boxed)
    {
      return ((Exp<E>)boxed).InExp;
    }
  }

  class Exp<Expression> : AI.IFunApp
  {
    protected IExpressionDecoder<Variable, Expression> decoder;
    protected Expression exp;

    internal Expression InExp
    {
      get
      {
        return this.exp;
      }
    }

    public Exp(Expression exp, IExpressionDecoder<Variable, Expression> decoder)
    {
      this.exp = exp;
      this.decoder = decoder;
    }

    #region IFunApp Members

    public IList/*<AI.IExpr>*/ Arguments
    {
      get 
      {
        IList result = new ArrayList();
        if (this.decoder.IsUnaryExpression(exp))
        {
          result.Add(Converter.Box(this.decoder.LeftExpressionFor(exp), this.decoder));
        }
        else if(this.decoder.IsBinaryExpression(exp))
        {
          result.Add(Converter.Box(this.decoder.LeftExpressionFor(exp), this.decoder));
          result.Add(Converter.Box(this.decoder.RightExpressionFor(exp), this.decoder));
        }

        return result;
      }
    }

    public AI.IFunApp CloneWithArguments(System.Collections.IList args)
    {
      return this;
    }

    public AI.IFunctionSymbol FunctionSymbol
    {
      get 
      {
        if (this.decoder.IsConstant(this.exp))
        {
          int val; 
          if (this.decoder.TryValueOf<int>(this.exp, ExpressionType.Int32, out val))
          {
            return AI.Int.Const(val);
          }
          else
          {
            return UnknownFunctionSymbol.Value;
          }
        }

        switch (this.decoder.OperatorFor(exp))
        {
          case ExpressionOperator.Addition:
            return AI.Int.Add;

          case ExpressionOperator.And:
            return AI.Prop.And;

          case ExpressionOperator.Constant:
            return UnknownFunctionSymbol.Value;

          case ExpressionOperator.ConvertToInt32:
          case ExpressionOperator.ConvertToUInt16:
          case ExpressionOperator.ConvertToUInt32:
          case ExpressionOperator.ConvertToUInt8:
            return Converter.Box(this.decoder.LeftExpressionFor(exp), this.decoder).FunctionSymbol;

          case ExpressionOperator.Division:
            return AI.Int.Div;

          case ExpressionOperator.Equal:
case ExpressionOperator.Equal_obj:
            return AI.Int.Eq;

          case ExpressionOperator.GreaterEqualThan_Un:
          case ExpressionOperator.GreaterEqualThan:
            return AI.Int.AtLeast;

          case ExpressionOperator.GreaterThan_Un:
          case ExpressionOperator.GreaterThan:
            return AI.Int.Greater;

          case ExpressionOperator.LessEqualThan_Un:
          case ExpressionOperator.LessEqualThan:
            return AI.Int.AtMost;

          case ExpressionOperator.LessThan_Un:
          case ExpressionOperator.LessThan:
            return AI.Int.Less;

          case ExpressionOperator.Modulus:
            return AI.Int.Mod;

          case ExpressionOperator.Multiplication:
            return AI.Int.Mul;

          case ExpressionOperator.Not:
            return AI.Prop.Not;

          case ExpressionOperator.NotEqual:
            return AI.Int.Neq;

          case ExpressionOperator.Or:
            return AI.Prop.Or;

          case ExpressionOperator.ShiftLeft:
            return UnknownFunctionSymbol.Value;

          case ExpressionOperator.ShiftRight:
            return UnknownFunctionSymbol.Value;

          case ExpressionOperator.SizeOf:
            return UnknownFunctionSymbol.Value;

          case ExpressionOperator.Subtraction:
            return AI.Int.Sub;

          case ExpressionOperator.UnaryMinus:
            return AI.Int.Negate;

          case ExpressionOperator.Unknown:
            return UnknownFunctionSymbol.Value;

          case ExpressionOperator.Variable:
            return new AI.NamedSymbol(ExpressionPrinter.ToString(this.exp, this.decoder), AI.Int.Type);

          case ExpressionOperator.WritableBytes: // to improve???
            return UnknownFunctionSymbol.Value;

          default:
            return UnknownFunctionSymbol.Value;
        }
      }
    }

    #endregion

    #region IExpr Members

    public object DoVisit(AI.ExprVisitor visitor)
    {
      if (this.decoder.IsVariable(this.exp))
      {
        return visitor.VisitVariable(Converter.BoxAsVariable(this.exp, this.decoder));
      }
      else 
      {
        return visitor.VisitFunApp(Converter.Box(this.exp, this.decoder));
      }
    }

    #endregion

    public override string ToString()
    {
      return ExpressionPrinter.ToString(this.exp, this.decoder);

    }
  }

  class Var<Expression> : Exp<Expression>, AI.IVariable
  {

    public Var(Expression exp, IExpressionDecoder<Variable, Expression> decoder)
     : base(exp, decoder)
    {
    }

    #region IVariable Members

    public string Name
    {
      get
      {
        return ExpressionPrinter.ToString(this.exp, this.decoder);
      }
    }

    #endregion
  }

  class UninterestingFunctions : AI.IFunApp
  {
    #region IFunApp Members

    public IList Arguments
    {
      get { return new ArrayList(); }
    }

    public AI.IFunApp CloneWithArguments(IList args)
    {
      return this;
    }

    public AI.IFunctionSymbol FunctionSymbol
    {
      get { return UnknownFunctionSymbol.Value; }
    }

    #endregion

    #region IExpr Members

    public object DoVisit(AI.ExprVisitor visitor)
    {
      return visitor.Default(this);
    }

    #endregion
  }

  class PropFactory<Expression> : AI.IQuantPropExprFactory
  {
    internal static AI.IFunApp t = null;
    internal static AI.IFunApp f = null;

    IExpressionDecoder<Variable, Expression> decoder;
    IExpressionEncoder<Variable, Expression> encoder;

    public PropFactory(IExpressionDecoder<Variable, Expression> decoder, IExpressionEncoder<Variable, Expression> encoder)
    {
      this.decoder = decoder;
      this.encoder = encoder;
    }

    #region IQuantPropExprFactory Members

    public AI.IFunApp Exists(AI.AIType paramType, AI.FunctionBody body)
    {
      return new UninterestingFunctions();
    }

    public AI.IFunApp Exists(AI.IFunction p)
    {
      return new UninterestingFunctions();
    }

    public AI.IFunApp Forall(AI.AIType paramType, AI.FunctionBody body)
    {
      return new UninterestingFunctions();
    }

    public AI.IFunApp Forall(AI.IFunction p)
    {
      return new UninterestingFunctions();
    }

    #endregion

    #region IPropExprFactory Members

    public AI.IFunApp And(AI.IExpr p, AI.IExpr q)
    {
      Expression e1 = Converter.Unbox<Expression>(p);
      Expression e2 = Converter.Unbox<Expression>(q);

      return Converter.Box(this.encoder.CompoundExpressionFor(ExpressionType.Bool, ExpressionOperator.And, e1, e2), this.decoder);
    }

    public AI.IFunApp False
    {
      get 
      {
        if (f == null)
        {
          Expression f1 = this.encoder.ConstantFor(false);
          f = Converter.Box(this.encoder.CompoundExpressionFor(ExpressionType.Bool, ExpressionOperator.Constant, f1), this.decoder);
        }
        return f;
      }
    }

    public AI.IFunApp Implies(AI.IExpr p, AI.IExpr q)
    {
      return new UninterestingFunctions();
    }

    public AI.IFunApp Not(AI.IExpr p)
    {
      Expression e = Converter.Unbox<Expression>(p);
      return Converter.Box(this.encoder.CompoundExpressionFor(ExpressionType.Bool, ExpressionOperator.Not, e), this.decoder);
    }

    public AI.IFunApp Or(AI.IExpr p, AI.IExpr q)
    {
      Expression e1 = Converter.Unbox<Expression>(p);
      Expression e2 = Converter.Unbox<Expression>(q);

      return Converter.Box(this.encoder.CompoundExpressionFor(ExpressionType.Bool, ExpressionOperator.Or, e1, e2), this.decoder);
    }

    public AI.IFunApp True
    {
      get 
      {
        if (t == null)
        {
          Expression t1 = this.encoder.ConstantFor(true);
          t = Converter.Box(this.encoder.CompoundExpressionFor(ExpressionType.Bool, ExpressionOperator.Constant, t1), this.decoder);
        }
        return t;
      }
    }

    #endregion
  }

  class LinearExpFactory<Expression> : AI.ILinearExprFactory
  {
    IExpressionDecoder<Variable, Expression> decoder;
    IExpressionEncoder<Variable, Expression> encoder;
   
    public LinearExpFactory(IExpressionDecoder<Variable, Expression> decoder, IExpressionEncoder<Variable, Expression> encoder)
    {
      this.decoder = decoder;
      this.encoder = encoder;
    }

    #region ILinearExprFactory Members

    public AI.IFunApp Add(AI.IExpr p, AI.IExpr q)
    {
      Expression e1 = Converter.Unbox<Expression>(p);
      Expression e2 = Converter.Unbox<Expression>(q);

      return Converter.Box(this.encoder.CompoundExpressionFor(ExpressionType.Int32, ExpressionOperator.Addition, e1, e2), this.decoder);
    }

    public AI.IFunApp And(AI.IExpr p, AI.IExpr q)
    {
      Expression e1 = Converter.Unbox<Expression>(p);
      Expression e2 = Converter.Unbox<Expression>(q);

      return Converter.Box(this.encoder.CompoundExpressionFor(ExpressionType.Bool, ExpressionOperator.And, e1, e2), this.decoder);
    }

    public AI.IFunApp AtMost(AI.IExpr p, AI.IExpr q)
    {
      Expression e1 = Converter.Unbox<Expression>(p);
      Expression e2 = Converter.Unbox<Expression>(q);

      return Converter.Box(this.encoder.CompoundExpressionFor(ExpressionType.Bool, ExpressionOperator.LessEqualThan, e1, e2), this.decoder);
    }

    public AI.IFunApp False
    {
      get 
      {
        if (PropFactory<Expression>.f == null)
        {
          Expression f1 = this.encoder.ConstantFor(false);
          PropFactory<Expression>.f = Converter.Box(this.encoder.CompoundExpressionFor(ExpressionType.Bool, ExpressionOperator.Constant, f1), this.decoder);
        }
        return PropFactory<Expression>.f;
      }
    }

    public AI.IExpr Term(AI.Rational r, AI.IVariable var)
    {
      long up = r.Numerator;
      long down = r.Denominator;

      Expression upAsExp = this.encoder.ConstantFor(up);
      Expression downAsExp = this.encoder.ConstantFor(down);

      Expression asFrac = this.encoder.CompoundExpressionFor(ExpressionType.Int32, ExpressionOperator.Division, upAsExp, downAsExp);

      // We assume AI.IVariable being a Var<Exp>
      Var<Expression> v = var as Var<Expression>;

      if(v == null)
        throw new AbstractInterpretationException();

      Expression newVar = Converter.Unbox<Expression>(v);

      return Converter.Box(this.encoder.CompoundExpressionFor(ExpressionType.Int32, ExpressionOperator.Multiplication, asFrac, newVar), this.decoder); 
    }

    public AI.IFunApp True
    {
      get
      {
        if (PropFactory<Expression>.t == null)
        {
          Expression t1 = this.encoder.ConstantFor(false);
          PropFactory<Expression>.t = Converter.Box(t1, this.decoder);
        }
        return PropFactory<Expression>.t;
      }
    }

    #endregion

    #region IIntExprFactory Members

    public AI.IFunApp Const(int i)
    {
      return Converter.Box(this.encoder.ConstantFor(i), this.decoder);
    }

    #endregion

    #region IValueExprFactory Members

    public AI.IFunApp Eq(AI.IExpr p, AI.IExpr q)
    {
      Expression e1 = Converter.Unbox<Expression>(p);
      Expression e2 = Converter.Unbox<Expression>(q);

      return Converter.Box(this.encoder.CompoundExpressionFor(ExpressionType.Bool, ExpressionOperator.Equal, e1, e2), this.decoder);
    }

    public AI.IFunApp Neq(AI.IExpr p, AI.IExpr q)
    {
      Expression e1 = Converter.Unbox<Expression>(p);
      Expression e2 = Converter.Unbox<Expression>(q);

      return Converter.Box(this.encoder.CompoundExpressionFor(ExpressionType.Bool, ExpressionOperator.NotEqual, e1, e2), this.decoder);
    }

    #endregion
  }

  public class PolyhedraEnvironment<Expression> : INumericalAbstractDomain<Variable, Expression>
  {
    #region Statics
    static private AI.Lattice UnderlyingPolyhedra;
    static private LinearExpFactory<Expression> linearfactory;
    static private PropFactory<Expression> propfactory;

    public static void Init(IExpressionDecoder<Variable, Expression> decoder, IExpressionEncoder<Variable, Expression> encoder)
    {
      linearfactory = new LinearExpFactory<Expression>(decoder, encoder);
      propfactory = new PropFactory<Expression>(decoder, encoder);
      UnderlyingPolyhedra = new AI.PolyhedraLattice(linearfactory, propfactory);

      UnderlyingPolyhedra.Validate();
    }
    #endregion

    #region Private State
    private AI.PolyhedraLattice.Element embedded;
    private IntervalEnvironment<Variable, Expression> intv;

    private IExpressionDecoder<Variable, Expression> decoder;
    private IExpressionEncoder<Variable, Expression> encoder;

    private PolyhedraTestTrueVisitor testTrueVisitor;
    private PolyhedraTestFalseVisitor testFalseVisitor;
    #endregion

    public PolyhedraEnvironment(IExpressionDecoder<Variable, Expression> decoder, IExpressionEncoder<Variable, Expression> encoder)
    {
      this.decoder = decoder;
      this.encoder = encoder;
      this.embedded = UnderlyingPolyhedra.Top;
      this.intv = new IntervalEnvironment<Variable, Expression>(decoder, encoder);

      this.testTrueVisitor = new PolyhedraEnvironment<Expression>.PolyhedraTestTrueVisitor(decoder);
      this.testFalseVisitor = new PolyhedraEnvironment<Expression>.PolyhedraTestFalseVisitor(decoder);
      this.testTrueVisitor.FalseVisitor = this.testFalseVisitor;
      this.testFalseVisitor.TrueVisitor = this.testTrueVisitor;
    }

    private PolyhedraEnvironment(PolyhedraEnvironment<Expression> pe, AI.PolyhedraLattice.Element value, IntervalEnvironment<Variable, Expression> intv)
      : this(pe.decoder, pe.encoder, value, intv)
    {
      this.testTrueVisitor = pe.testTrueVisitor;
      this.testFalseVisitor = pe.testFalseVisitor;
    }

    private PolyhedraEnvironment(IExpressionDecoder<Variable, Expression> decoder, IExpressionEncoder<Variable, Expression> encoder, AI.PolyhedraLattice.Element value, IntervalEnvironment<Variable, Expression> intv)
    {
      this.decoder = decoder;
      this.encoder = encoder;
      this.embedded = value;
      this.intv = intv;

      this.testTrueVisitor = new PolyhedraEnvironment<Expression>.PolyhedraTestTrueVisitor(decoder);
      this.testFalseVisitor = new PolyhedraEnvironment<Expression>.PolyhedraTestFalseVisitor(decoder);
      this.testTrueVisitor.FalseVisitor = this.testFalseVisitor;
      this.testFalseVisitor.TrueVisitor = this.testTrueVisitor;
    }

    #region INumericalAbstractDomain<Rational,Expression> Members

    /// <summary>
    /// Return top
    /// </summary>
    public Interval BoundsFor(Expression v)
    {
      return this.intv.BoundsFor(v); 
    }

    // Does nothing for the moment
    public void AssignInterval(Expression x, Interval value)
    {
      this.AssignIntervalJustInPolyhedra(x, value);
      this.intv.AssignInterval(x, value);
    }

    private void AssignIntervalJustInPolyhedra(Expression x, Interval value)
    {
      if (!value.IsBottom)
      {
        if (!value.LowerBound.IsInfinity)
        {
          AI.IExpr lowerBound = linearfactory.AtMost(linearfactory.Const((Int32)value.LowerBound.PreviousInteger), Converter.BoxAsVariable(x, this.decoder));
          this.embedded = UnderlyingPolyhedra.Constrain(this.embedded, lowerBound);
        }
        if (!value.UpperBound.IsInfinity)
        {
          AI.IExpr upperBound = linearfactory.AtMost(Converter.BoxAsVariable(x, this.decoder), linearfactory.Const((Int32)value.UpperBound.NextInteger));
          this.embedded = UnderlyingPolyhedra.Constrain(this.embedded, upperBound);
        }
      }
    }

    public INumericalAbstractDomain<Variable, Expression> TestTrueGeqZero(Expression exp)
    {
      // 0 <= exp
      AI.IExpr toAssume = linearfactory.AtMost(linearfactory.Const(0), Converter.Box<Expression>(exp, this.decoder));

      return Factory(UnderlyingPolyhedra.Constrain(this.embedded, toAssume), this.intv.TestTrueGeqZero(exp));
    }

    public INumericalAbstractDomain<Variable, Expression> TestTrueLessThan(Expression exp1, Expression exp2)
    {
      // exp1 <= exp2 -1
      AI.IExpr toAssume = linearfactory.AtMost(Converter.Box(exp1, this.decoder), linearfactory.Add(Converter.Box(exp2, this.decoder), linearfactory.Const(-1)));

      return Factory(UnderlyingPolyhedra.Constrain(this.embedded, toAssume), this.intv.TestTrueLessThan(exp1, exp2));
    }

    public INumericalAbstractDomain<Variable, Expression> TestTrueLessEqualThan(Expression exp1, Expression exp2)
    {
      // exp1 <= exp2 
      AI.IExpr toAssume = linearfactory.AtMost(Converter.Box(exp1, this.decoder), Converter.Box(exp2, this.decoder));

      return Factory(UnderlyingPolyhedra.Constrain(this.embedded, toAssume), this.intv.TestTrueLessEqualThan(exp1, exp2));
    }

    public FlatAbstractDomain<bool> CheckIfGreaterEqualThanZero(Expression exp)
    {
      // exp >= 0 ?
      AI.IExpr toCheck = linearfactory.AtMost(linearfactory.Const(0), Converter.Box<Expression>(exp, this.decoder));

      return ToFlatAbstractDomain(UnderlyingPolyhedra.CheckPredicate(this.embedded, toCheck)).Meet(this.intv.CheckIfGreaterEqualThanZero(exp));
    }
    
    public FlatAbstractDomain<bool> CheckIfLessThan(Expression e1, Expression e2)
    {
      // exp1 <= exp2 -1 ?

      AI.IExpr toAssume = linearfactory.AtMost(Converter.Box(e1, this.decoder), linearfactory.Add(Converter.Box(e2, this.decoder), linearfactory.Const(-1)));

      return ToFlatAbstractDomain(UnderlyingPolyhedra.CheckPredicate(this.embedded, toAssume)).Meet(this.intv.CheckIfLessThan(e1, e2));
    }

    public FlatAbstractDomain<bool> CheckIfLessEqualThan(Expression e1, Expression e2)
    {
     // exp1 <= exp2 ?
      AI.IExpr toAssume = linearfactory.AtMost(Converter.Box(e1, this.decoder), Converter.Box(e2, this.decoder));

      return ToFlatAbstractDomain(UnderlyingPolyhedra.CheckPredicate(this.embedded, toAssume)).Meet(this.intv.CheckIfLessEqualThan(e1, e2));
    }

    #endregion

    #region IAbstractDomain Members

    public bool IsBottom
    {
      get 
      {
        return UnderlyingPolyhedra.IsBottom(this.embedded) || this.intv.IsBottom;
      }
    }

    public bool IsTop
    {
      get 
      {
        return UnderlyingPolyhedra.IsTop(this.embedded) && this.intv.IsTop;
      }
    }

    bool IAbstractDomain.LessEqual(IAbstractDomain a)
    {
      return this.LessEqual((PolyhedraEnvironment<Expression>)a);
    }

    public bool LessEqual(PolyhedraEnvironment<Expression> right)
    {
      bool result;
      if (AbstractDomainsHelper.TryTrivialLessEqual(this, right, out result))
      {
        return result;
      }
      else
      {
        return UnderlyingPolyhedra.LowerThan(this.embedded, right.embedded) && this.intv.LessEqual(right.intv);
      }
    }

    public IAbstractDomain Bottom
    {
      get 
      {
        return Factory(UnderlyingPolyhedra.Bottom, (INumericalAbstractDomain<Variable, Expression>) this.intv.Bottom);
      }
    }

    public IAbstractDomain Top
    {
      get 
      {
        return Factory(UnderlyingPolyhedra.Top, (INumericalAbstractDomain<Variable, Expression>)this.intv.Top);
      }
    }

    IAbstractDomain IAbstractDomain.Join(IAbstractDomain a)
    {
      return this.Join((PolyhedraEnvironment<Expression>)a); 
    }

    public PolyhedraEnvironment<Expression> Join(PolyhedraEnvironment<Expression> right)
    {
      PolyhedraEnvironment<Expression> result;
      if (AbstractDomainsHelper.TryTrivialJoin(this, right, out result))
      {
        return result;
      }
      else
      {
        return Factory(UnderlyingPolyhedra.Join(this.embedded, right.embedded), this.intv.Join(right.intv));
      }
    }

    IAbstractDomain IAbstractDomain.Meet(IAbstractDomain a)
    {
      return this.Meet((PolyhedraEnvironment<Expression>)a);
    }

    public PolyhedraEnvironment<Expression> Meet(PolyhedraEnvironment<Expression> right)
    {
      PolyhedraEnvironment<Expression> result;
      if (AbstractDomainsHelper.TryTrivialMeet(this, right, out result))
      {
        return result;
      }
      else
      {
        return Factory(UnderlyingPolyhedra.Meet(this.embedded, right.embedded), this.intv.Meet(right.intv));
      }
    }

    IAbstractDomain IAbstractDomain.Widening(IAbstractDomain prev)
    {
      return this.Widening((PolyhedraEnvironment<Expression>)prev);
    }

    public PolyhedraEnvironment<Expression> Widening(PolyhedraEnvironment<Expression> prev)
    {
      PolyhedraEnvironment<Expression> result;
      if (AbstractDomainsHelper.TryTrivialJoin(this, prev, out result))
      {
        return result;
      }
      else
      {
        // Boogie's polyhedra has it in the other order
        return Factory(UnderlyingPolyhedra.Widen(prev.embedded, this.embedded), this.intv.Widening(prev.intv));
      }
    }

    public T To<T>(IFactory<T> factory)
    {
      return factory.Constant(true);
    }

    #endregion

    #region ICloneable Members

    public object Clone()
    {
      return Factory((AI.PolyhedraLattice.Element) this.embedded.Clone(), (IAbstractDomain) this.intv.Clone());
    }

    #endregion

    #region IPureExpressionAssignments<Expression> Members

    public Set<Expression> Variables
    {
      get 
      {
        Set<Expression> result = new Set<Expression>();

        foreach (AI.IVariable var in this.embedded.FreeVariables())
        {
          Exp<Expression> asExp = var as Exp<Expression>;
          result.Add(asExp.InExp);
        }

        return result.Union(this.intv.Variables);
      }
    }

    public void AddVariable(Expression var)
    {
      // Does nothing
    }

    public void Assign(Expression x, Expression exp)
    {
      this.Assign(x, exp, TopNumericalDomain<Variable, Expression>.Singleton);
    }

    public void Assign(Expression x, Expression exp, INumericalAbstractDomainQuery<Variable, Expression> preState)
    {
      this.embedded = UnderlyingPolyhedra.Constrain(this.embedded, Converter.Box(this.encoder.CompoundExpressionFor(ExpressionType.Bool, ExpressionOperator.Equal, x, exp), this.decoder));
    }

    public void ProjectVariable(Expression var)
    {
      this.embedded = UnderlyingPolyhedra.Eliminate(this.embedded, Converter.BoxAsVariable<Expression>(var, this.decoder));
      this.intv.ProjectVariable(var);
    }

    public void RemoveVariable(Expression var)
    {
      this.embedded = UnderlyingPolyhedra.Eliminate(this.embedded, Converter.BoxAsVariable<Expression>(var, this.decoder));
      this.intv.RemoveVariable(var);
    }

    public void RenameVariable(Expression OldName, Expression NewName)
    {
      this.embedded = UnderlyingPolyhedra.Rename(this.embedded, Converter.BoxAsVariable<Expression>(OldName, this.decoder), Converter.BoxAsVariable<Expression>(NewName, this.decoder));
      this.intv.RenameVariable(OldName, NewName);
    }

    #endregion

    #region IPureExpressionTest<Expression> Members

    IAbstractDomainForEnvironments<Variable, Expression> IPureExpressionTest<Expression>.TestTrue(Expression guard)
    {
      return this.TestTrue(guard);
    }

    PolyhedraEnvironment<Expression> TestTrue(Expression guard)
    {
      return this.testTrueVisitor.Visit(guard, this);
    }

    IAbstractDomainForEnvironments<Variable, Expression> IPureExpressionTest<Expression>.TestFalse(Expression guard)
    {
      return this.TestFalse(guard);
    }

    public IAbstractDomainForEnvironments<Variable, Expression> TestFalse(Expression guard)
    {
      return this.testFalseVisitor.Visit(guard, this);
    }

    public FlatAbstractDomain<bool> CheckIfNonZero(Expression exp)
    {
      return new FlatAbstractDomain<bool>(false).Top;
    }

    public FlatAbstractDomain<bool> CheckIfHolds(Expression exp)
    {
      // exp ?
      return ToFlatAbstractDomain(UnderlyingPolyhedra.CheckPredicate(this.embedded, Converter.Box(exp, this.decoder))).Meet(this.intv.CheckIfHolds(exp));
    }

    #endregion

    #region IAssignInParallel<Expression> Members

    public void AssignInParallel(IDictionary<Expression, Microsoft.Research.DataStructures.FList<Expression>> sourcesToTargets)
    {
      this.intv.AssignInParallel(sourcesToTargets);

      // Do the renamings
      foreach (Expression source in sourcesToTargets.Keys)
      {
        {
          if (sourcesToTargets[source].Length() == 1)
          { // we want to just follow the renamings, all the rest is not interesting and we discard it
            Expression target = sourcesToTargets[source].Head;
            // source -> target ,  i.e. the new name for "source" is "target"

            ALog.Message(StringClosure.For("Renaming {0} to {1}", ExpressionPrinter.ToStringClosure(source, decoder), ExpressionPrinter.ToStringClosure(target, decoder)));

              this.embedded = UnderlyingPolyhedra.Rename(this.embedded,Converter.BoxAsVariable(source, this.decoder), Converter.BoxAsVariable(target, this.decoder));
          }
        }
      }
#if true || MOREPRECISE
        // else we want to keep track of constants

      foreach (Expression x in this.intv.Variables)
      {
        Interval value = this.intv.BoundsFor(x);
        {
          this.AssignIntervalJustInPolyhedra(x, value);
        }
      }

#endif
    }


    #endregion

    #region Private Methods
    private PolyhedraEnvironment<Expression> Factory(AI.PolyhedraLattice.Element element, IAbstractDomain intv)
    {
      return new PolyhedraEnvironment<Expression>(this.decoder, this.encoder, element, (IntervalEnvironment<Variable, Expression>) intv);
    }

    private FlatAbstractDomain<bool> ToFlatAbstractDomain(AI.Answer answer)
    {
      switch (answer)
      {
        case AI.Answer.Maybe:
          return new FlatAbstractDomain<bool>(false).Top;

        case AI.Answer.No:
          return new FlatAbstractDomain<bool>(false);

        case AI.Answer.Yes:
          return new FlatAbstractDomain<bool>(true);
      }

      return null; // Should be unreachable
    }


    #endregion

    public override string ToString()
    {
      return this.embedded.ToString();
    }
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


    class PolyhedraTestTrueVisitor : TestTrueVisitor<PolyhedraEnvironment<Expression>, Expression>
    {
      public PolyhedraTestTrueVisitor(IExpressionDecoder<Variable, Expression> decoder)
        : base(decoder)
      {
      }

      public override PolyhedraEnvironment<Expression> VisitEqual(Expression left, Expression right, PolyhedraEnvironment<Expression> data)
      {
        // left == right
        AI.IExpr notEq = linearfactory.Eq(Converter.Box(left, this.Decoder), Converter.Box(right, this.Decoder));

        return data.Factory(UnderlyingPolyhedra.Constrain(data.embedded, notEq), data.intv); 
      }

      public override PolyhedraEnvironment<Expression> VisitLessEqualThan(Expression left, Expression right, PolyhedraEnvironment<Expression> data)
      {
        return (PolyhedraEnvironment<Expression>) data.TestTrueLessEqualThan(left, right);
      }

      public override PolyhedraEnvironment<Expression> VisitLessThan(Expression left, Expression right, PolyhedraEnvironment<Expression> data)
      {
        return (PolyhedraEnvironment <Expression> ) data.TestTrueLessThan(left, right);
      }

      public override PolyhedraEnvironment<Expression> VisitNotEqual(Expression left, Expression right, PolyhedraEnvironment<Expression> data)
      {
        // left != right
        AI.IExpr notEq = linearfactory.Neq(Converter.Box(left, this.Decoder), Converter.Box(right, this.Decoder));

        return data.Factory(UnderlyingPolyhedra.Constrain(data.embedded, notEq), data.intv); 
      }

      public override PolyhedraEnvironment<Expression> VisitVariable(Expression exp, object variable, PolyhedraEnvironment<Expression> data)
      {
        return data;
      }

    }

    class PolyhedraTestFalseVisitor : TestFalseVisitor<PolyhedraEnvironment<Expression>, Expression>
    {
      public PolyhedraTestFalseVisitor(IExpressionDecoder<Variable, Expression> decoder)
        : base(decoder)
      {
      }

      public override PolyhedraEnvironment<Expression> VisitVariable(Expression left, object variable, PolyhedraEnvironment<Expression> data)
      {
        return data;
      }
    }


    #region INumericalAbstractDomain<Rational,Expression> Members

    public INumericalAbstractDomain<Variable, Expression> RemoveRedundanciesWith(INumericalAbstractDomain<Variable, Expression> oracle)
    {
      return (INumericalAbstractDomain<Variable, Expression>) this.Clone();
    }

    /// <returns>
    /// The empty set
    /// </returns>
    public IEnumerable<Expression> LowerBoundsFor(Expression v)
    {
      return new Set<Expression>();
    }

    #endregion

    #region INumericalAbstractDomain<Rational,Expression> Members


    public IEnumerable<Expression> LowerBoundsFor(Expression v, bool strict)
    {
      return new Set<Expression>();
    }

    #endregion

    #region INumericalAbstractDomain<Rational,Expression> Members


    public IEnumerable<Expression> UpperBoundsFor(Expression v, bool strict)
    {
      return new Set<Expression>();
    }

    #endregion
  }

  public class PolyhedraException : Exception
  {
  }
  
}
#endif