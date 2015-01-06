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
using System.Text;
using Microsoft.Research.AbstractDomains.Expressions;
using System.Diagnostics;
using Microsoft.Research.DataStructures;
using Microsoft.Research.CodeAnalysis;

namespace Microsoft.Research.AbstractDomains.Numerical
{
  public class Stripe<Variable, Expression, MetaDataDecoder> :
      FunctionalAbstractDomain<Stripe<Variable, Expression, MetaDataDecoder>, Variable, SetOfConstraints<AtMostTwoExpressions<Variable>>>,
        INumericalAbstractDomain<Variable, Expression>
    where Expression : class
  {
    #region Private state
    public readonly IExpressionDecoder<Variable, Expression>/*!*/ decoder;
    protected IExpressionEncoder<Variable, Expression> encoder;           // The expression encoder can be null
    protected MetaDataDecoder mdDecoder;

    #endregion

    #region Constructor
    public Stripe(IExpressionDecoder<Variable, Expression>/*!*/ decoder, MetaDataDecoder mdDecoder)
    {
      this.decoder = decoder;
      this.encoder = null;
      this.mdDecoder = mdDecoder;
    }

    public Stripe(IExpressionDecoder<Variable, Expression>/*!*/ decoder, MetaDataDecoder mdDecoder, IExpressionEncoder<Variable, Expression>/*!*/ encoder)
    {
      this.decoder = decoder;
      this.encoder = encoder;
      this.mdDecoder = mdDecoder;
    }

    protected Stripe(Stripe<Variable, Expression, MetaDataDecoder> other)
    {
      this.decoder = other.decoder;
      foreach (var x in other.Keys)
      {
        var setval = new Set<AtMostTwoExpressions<Variable>>();
        foreach (var el in other[x].EmbeddedValues_Unsafe)
        {
          setval.Add((AtMostTwoExpressions<Variable>)el.Clone());
        }

        var clonedvalue = new SetOfConstraints<AtMostTwoExpressions<Variable>>(setval);//(SetOfConstraints<AtMostTwoExpressions<Variable>>)other[x].Clone();
        this.AddElement(x, clonedvalue);
      }
      this.encoder = other.encoder;
      this.mdDecoder = other.mdDecoder;
    }
    #endregion

    public MetaDataDecoder MdDecoder { get { return this.mdDecoder; } }

    #region INumericalAbstractDomain<Variable, Expression>Members

    public Dictionary<Variable, Int32> IntConstants
    {
      get
      {
        return new Dictionary<Variable,Int32>();
      }
    }

    public Set<Expression> LowerBoundsFor(Expression v, bool strict)
    {
      return new Set<Expression>(); // not implemented
    }

    public Set<Expression> LowerBoundsFor(Variable v, bool strict)
    {
      return new Set<Expression>(); // not implemented
    }

    public Set<Expression> UpperBoundsFor(Expression v, bool strict)
    {
      return new Set<Expression>(); // not implemented
    }

    public Set<Expression> UpperBoundsFor(Variable v, bool strict)
    {
      return new Set<Expression>(); // not implemented
    }

    public INumericalAbstractDomain<Variable, Expression> RemoveRedundanciesWith(INumericalAbstractDomain<Variable, Expression> oracle)
    {
      return (INumericalAbstractDomain<Variable, Expression>) this.Clone();
    }

    private void RemoveConstraints(FList<Expression> vars, Expression exp)
    {
      //while (vars != null)
      //{
      //  Expression el = vars.Head;
      //  if (el.Equals(exp) == false)
      //    this.RemoveVar(el);
      //  vars = vars.Tail;
      //}
    }

    /// <summary>
    /// Check if the current expression contains the given variables
    /// </summary>
    private bool ContainsVariable(Variable exp)
    {
      if (this.ContainsKey(exp) && this[exp].IsNormal()) 
        return true;
      
      foreach (var e in this.Keys)
        foreach (var val in this[e].EmbeddedValues_Unsafe)
          if (val.Contains(exp)) return true;
      return false;
    }

    public void AssignInParallel(Dictionary<Variable, FList<Variable>> sourcesToTargets, Converter<Variable, Expression> convert)
    {
      var cloned = (Stripe<Variable, Expression, MetaDataDecoder>)this.Clone();

      var oldToNewMap = new Dictionary<Variable, FList<Variable>>(sourcesToTargets);
      // when x has several targets including itself, the canonical element shouldn't be itself
      foreach (var sourceToTargets in sourcesToTargets)
      {
        Variable source = sourceToTargets.Key;
        FList<Variable> targets = sourceToTargets.Value;
        if (targets.Length() > 1 && targets.Head.Equals(source))
        {
          FList<Variable> newTargets = FList<Variable>.Cons(targets.Tail.Head, FList<Variable>.Cons(source, targets.Tail.Tail));
          oldToNewMap[source] = newTargets;
        }
      }

      var newMappings = new Dictionary<Variable, List<AtMostTwoExpressions<Variable>>>(this.Count);

      foreach (var oldLeft in this.Keys)
      {
        #region Apply the renamings

        // Do we have a renaming for it?
        if (!oldToNewMap.ContainsKey(oldLeft))
        {
          continue;
        }

        SetOfConstraints<AtMostTwoExpressions<Variable>> oldConstraints = this[oldLeft];

        if (oldConstraints.IsBottom || oldConstraints.IsTop)
        {
          continue;
        }

        foreach (AtMostTwoExpressions<Variable> oldRight in oldConstraints.EmbeddedValues_Unsafe)
        {
          #region Rename the Right part
          Variable exp1 = oldRight.Exp1;
          List<Variable> newExp1 = new List<Variable>();

          if (!oldToNewMap.ContainsKey(exp1))
          {
            continue;
          }
          else
          {
            // newExp1 = oldToNewMap[exp1].Head; // our canonical element

            FList<Variable> l = oldToNewMap[exp1];
            do
            {
              newExp1.Add(l.Head);

              l = l.Tail;
            } while (l != null);
            
          }

          Variable exp2 = oldRight.Exp2;
          List<Variable> newExp2 = new List<Variable>();
          

          if (exp2 != null && !exp2.Equals(default(Expression)))
          {
            if (!oldToNewMap.ContainsKey(exp2))
            {
              continue;
            }
            else
            {
              // newExp2 = oldToNewMap[exp2].Head; // our canonical element

              FList<Variable> l = oldToNewMap[exp2];
              do
              {
                newExp2.Add(l.Head);

                l = l.Tail;
              } while (l != null);
            }
          }

          // Generate the renamed constraint
          //AtMostTwoExpressions<Variable> newRight = newExp2.Count > 0?
          //  new AtMostTwoExpressions<Variable>(newExp1, newExp2, oldRight.N, oldRight.Constant) :
          //  new AtMostTwoExpressions<Variable>(newExp1, oldRight.N, oldRight.Constant);

          List<AtMostTwoExpressions<Variable>> newRight = new List<AtMostTwoExpressions<Variable>>();

          if (newExp2.Count > 0)
          {
            foreach(var e1 in newExp1)
              foreach(var e2 in newExp2)
              {
                newRight.Add(new AtMostTwoExpressions<Variable>(e1, e2, oldRight.N, oldRight.Constant));
              }
          }
          else
          {
            foreach(var e1 in newExp1)
            {
              newRight.Add( new AtMostTwoExpressions<Variable>(e1, oldRight.N, oldRight.Constant));
            }
          }

          FList<Variable> list = oldToNewMap[oldLeft];

          do
          {
            var newLeft = list.Head; 

            if (!newMappings.ContainsKey(newLeft))
            {
              newMappings[newLeft] = new List<AtMostTwoExpressions<Variable>>();
            }

            foreach (AtMostTwoExpressions<Variable> newConstraint in newRight)
            {
              newMappings[newLeft].Add(newConstraint);
            }

            list = list.Tail;
          } while (list != null);

          #endregion
        }
        #endregion
      }

      this.ClearElements();

      foreach (var x in newMappings.Keys)
      {
        var newConstraints = newMappings[x];
        if (newConstraints.Count > 0)
        {
          this[x] = new SetOfConstraints<AtMostTwoExpressions<Variable>>(newConstraints);
        }
      }
    }
    #endregion

    override public object/*!*/ Clone()
    {
      return new Stripe<Variable, Expression, MetaDataDecoder>(this);
    }

    public Interval BoundsFor(Expression v)
    {
      return Interval.For(0).Top;
    }

    public Interval BoundsFor(Variable v)
    {
      return Interval.For(0).Top;
    }

    public void AssignInterval(Variable/*!*/ x, Interval/*!*/ value)
    {
      // does nothing
    }

    protected override Stripe<Variable, Expression, MetaDataDecoder> Factory()
    {
      return new Stripe<Variable, Expression, MetaDataDecoder>(decoder, this.mdDecoder);
    }

    public Stripe<Variable, Expression, MetaDataDecoder>/*!*/ Refine(IntervalEnvironment<Variable, Expression>/*!*/ intervals)
    {
      return this;
    }

    /// <summary>
    /// Given a variable and a state of the linear equalities domain, it adds some possible constraints that may be inferred by the given equivalences
    /// </summary>
    public Stripe<Variable, Expression, MetaDataDecoder>/*!*/ Refine(LinearEqualitiesEnvironment<Variable, Expression> linearequalities, Variable e)
    {
      // Pietro's
      // if (!light)
      if(true)
      {
        //We extract all the polynomial representing expression that are equal to the one passed as parameter
        var equalsto = linearequalities.EqualsTo(e);
        foreach (var poly in equalsto)
        {
          /// IList<Monomial<Expression>> setmon = poly.Left;

          var setmon = this.RemoveZeroValues(poly.Left); //We remove all the monomes that are equal to 0

          if (setmon.Count == 1)
          {
            //If the polynomial is composed only by a monome, we have that the given expression is equal to it
            Monomial<Variable> mon = setmon[0];
            if (mon.Variables.Count == 1)
            {
              //If it contains only a variable we have that K*var==e
              //So we compute the K, and for each contraint var-a*(x+y)>n we add the constraint e-(K*a)*(x+y)>n*K              

              double constant = (double) mon.K;
              var var = mon.Variables[0];
              if (this.ContainsKey(var))
              {
                SetOfConstraints<AtMostTwoExpressions<Variable>> constraints = this[var];
                foreach (AtMostTwoExpressions<Variable> val in constraints.EmbeddedValues_Unsafe)
                {
                 var constr = (AtMostTwoExpressions<Variable>)val.Clone();
                  constr.N = (int)(((double)constr.N) * constant);
                  constr.Constant = (int)(((double)constr.Constant) * constant);
                  if (constr.N != 0 && constant >= 0)
                  {
                    this.AddConstraint(e, constr);
                  }
                }
              }
              if (constant == 1.0)
              {
                //if we have that e==var...
                Stripe<Variable, Expression, MetaDataDecoder> projected = this.GetConstraintsOfAVariable(var);
                foreach (var key in projected.Keys)
                {
                  if (!key.Equals(var))
                  { //.. for each constraint such that var is "in the right" (otherwise we already deal with this case in the previous loop)...


                    foreach (var constr in projected[key].EmbeddedValues_Unsafe)
                    {
                      //.. we add a constraint that is the one that contains e but in which e is replaced by var
                      var res = (AtMostTwoExpressions<Variable>)constr.Clone();
                      res.Replace(var, e);
                      this.AddConstraint(key, res);
                    }
                  }
                }
              }
            }
          }
        }
      }
      return this;
    }

    /// <summary>
    /// Remove from a list of monomial all the ones that are trivially equal to zero
    /// </summary>
    private List<Monomial<Variable>> RemoveZeroValues(IList<Monomial<Variable>> list)
    {
      var result = new List<Monomial<Variable>>();
      foreach (var el in list)
      {
        if (!el.K.IsZero)
        {
          result.Add(el);
        }
      }

      return result;
    }

    #region CheckIf methods

    private FlatAbstractDomain<bool>/*!*/ CheckIfNotHolds(Expression exp)
    {
      FlatAbstractDomain<bool> result;
      
      var operatorForExp = this.decoder.OperatorFor(exp);
      
      var top = CheckOutcome.Top;
      var bottom = CheckOutcome.Bottom;

      Expression left, right;
      switch (operatorForExp)
      {
        case ExpressionOperator.Equal:
        case ExpressionOperator.Equal_Obj:
          left = this.decoder.LeftExpressionFor(exp);
          right = this.decoder.RightExpressionFor(exp);
          // Try to figure out if it is in the form of (e11 rel e12) == 0
          Expression e11, e12;
          ExpressionOperator op;
          if (ExpressionHelper.Match_E1relopE2eq0(left, right, out op, out e11, out e12, this.decoder))
          {
            result = CheckIfHolds(left);
          }
          else
          {
            result = top;
          }
          break;

        case ExpressionOperator.GreaterThan:
        case ExpressionOperator.GreaterThan_Un:
          //!(left > right) -> left <= right -> left < right + 1
          left = this.decoder.LeftExpressionFor(exp);
          right = this.decoder.RightExpressionFor(exp);
          result = CheckIfLessThan(left, right, 1);
          break;

        case ExpressionOperator.GreaterEqualThan:
        case ExpressionOperator.GreaterEqualThan_Un:
          //!(left >= right) -> left < right
          left = this.decoder.LeftExpressionFor(exp);
          right = this.decoder.RightExpressionFor(exp);
          result = CheckIfLessThan(left, right, 0);
          break;

        case ExpressionOperator.LessThan:
        case ExpressionOperator.LessThan_Un:
          //!(left < right) -> left >= right -> right <= left -> right < left + 1
          left = this.decoder.LeftExpressionFor(exp);
          right = this.decoder.RightExpressionFor(exp);
          result = CheckIfLessThan(right, left, 1);
          break;

        case ExpressionOperator.LessEqualThan:
        case ExpressionOperator.LessEqualThan_Un:
          //!(left <= right) -> left > right -> right < left
          left = this.decoder.LeftExpressionFor(exp);
          right = this.decoder.RightExpressionFor(exp);
          result = CheckIfLessThan(right, left, 0);
          break;

        case ExpressionOperator.Or:
          result = top;
          break;

        case ExpressionOperator.Not:
          left = this.decoder.LeftExpressionFor(exp);
          result = this.CheckIfHolds(left);
          break;

        default:
          result = top;
          break;
      }

      return result;
    }

    public FlatAbstractDomain<bool>/*!*/ CheckIfHolds(Expression exp)
    {
      FlatAbstractDomain<bool> result;
      FlatAbstractDomain<bool> top = CheckOutcome.Top;
      FlatAbstractDomain<bool> bottom = CheckOutcome.Bottom;

      Expression left, right;
      ExpressionOperator operatorForExp = this.decoder.OperatorFor(exp);
      
      switch (operatorForExp)
      {
        case ExpressionOperator.And:
        case ExpressionOperator.LogicalAnd:
          result = top;
          break;

        case ExpressionOperator.Equal:
        case ExpressionOperator.Equal_Obj:
          left = this.decoder.LeftExpressionFor(exp);
          right = this.decoder.RightExpressionFor(exp);
          // Try to figure out if it is in the form of (e11 rel e12) == 0
          Expression e11, e12;
          ExpressionOperator op;
          if (ExpressionHelper.Match_E1relopE2eq0(left, right, out op, out e11, out e12, this.decoder))
          {
            result = CheckIfNotHolds(left);
          }
          else
          {
            result = top;
          }
          break;

        case ExpressionOperator.GreaterThan:
        case ExpressionOperator.GreaterThan_Un:
          //left > right -> right < left
          left = this.decoder.LeftExpressionFor(exp);
          right = this.decoder.RightExpressionFor(exp);
          
          result = CheckIfLessThan(right, left, 0);
          break;

        case ExpressionOperator.GreaterEqualThan:
        case ExpressionOperator.GreaterEqualThan_Un:
          //left >= right -> right <= left -> right < left + 1 
          left = this.decoder.LeftExpressionFor(exp);
          right = this.decoder.RightExpressionFor(exp);
          
          result = CheckIfLessThan(right, left, 1);
          break;

        case ExpressionOperator.LessThan:
        case ExpressionOperator.LessThan_Un:
          left = this.decoder.LeftExpressionFor(exp);
          right = this.decoder.RightExpressionFor(exp);
          
          result = CheckIfLessThan(left, right, 0);
          break;

        case ExpressionOperator.LessEqualThan:
        case ExpressionOperator.LessEqualThan_Un:
          //left <= right -> left < right + 1 
          left = this.decoder.LeftExpressionFor(exp);
          right = this.decoder.RightExpressionFor(exp);

          result = CheckIfLessThan(left, right, 1);
          break;

        case ExpressionOperator.Or:
          result = top;
          break;

        case ExpressionOperator.Not:
          left = this.decoder.LeftExpressionFor(exp);
          result = this.CheckIfNotHolds(left);
          break;

        default:
          result = top;
          break;
      }

      return result;
    }

    public FlatAbstractDomain<bool>/*!*/ CheckIfHolds(Expression exp, INumericalAbstractDomain<Variable, Expression>oracleDomain)
    {
      return this.CheckIfHolds(exp);
    }

    public FlatAbstractDomain<bool>/*!*/ CheckIfGreaterEqualThanZero(Expression/*!*/ exp)
    {
      //if (this.ContainsKey(exp)) 
      //  return CheckOutcome.True;
      //else 
        return CheckOutcome.Top;
    }

    public FlatAbstractDomain<bool>/*!*/ CheckIfLessThan(Expression/*!*/ e1, Expression/*!*/ e2)
    {
      // Francesco: The code below is very suspicious !!!

      //e1 <= e2 -> e1 < e2+1
      return this.CheckIfLessThan(e1, e2, 1);
    }

    public FlatAbstractDomain<bool> CheckIfLessEqualThan(Variable v1, Variable v2)
    {
      return CheckOutcome.Top;
    }


    public FlatAbstractDomain<bool> CheckIfEqual(Expression e1, Expression e2)
    {
      return CheckOutcome.Top;
    }

    public FlatAbstractDomain<bool> CheckIfLessThan(Variable v1, Variable v2)
    {
      return CheckOutcome.Top;
    }

    public FlatAbstractDomain<bool>/*!*/ CheckIfLessThan(Expression/*!*/ e1, Expression/*!*/ e2, int val)
    {
      //Polynomial<Variable, Expression> p1, p2, complete;

      //if (Polynomial<Variable, Expression>.TryToPolynomialForm(e1, this.decoder, out p1)
      //  && Polynomial<Variable, Expression>.TryToPolynomialForm(e2, this.decoder, out p2)
      //  && Polynomial<Variable, Expression>.TryToPolynomialForm(ExpressionOperator.LessThan, p1, p2, out complete))
      //{
      //  bool s1;

      //  //We expect something like -x+ay[+az]<k
      //  if (complete.Relation == ExpressionOperator.LessThan)
      //  {
      //    if (complete.Left.Count > 3 || complete.Left.Count < 2)
      //    {
      //      return CheckOutcome.Top;
      //    }

      //    int mult_pointer;
      //    int constant;
      //    //We compute the constant k and we store -k-val, as we check x-ay[-az]>-k and not -x+ay[+az]<=k, and val is used to manage externally the <= operator
      //    constant = -this.ConstantValue(complete.Right, out s1) - val;

      //    if (!s1)
      //    {
      //      goto top;
      //    }

      //    //We extract pointer and the other part of the expression
      //    Expression pointer = this.FindPointer(complete.Left, out mult_pointer);

      //    if (pointer == null || pointer.Equals(default(Expression)))
      //    {
      //      goto top;
      //    }
      //    else
      //    {
      //      Expression exp1, exp2;
      //      int n;
           
      //      if (!this.FindOtherVariables(complete.Left, out exp1, out exp2, out n)) 
      //        goto top;
            
      //      AtMostTwoExpressions<Variable> value;

      //      if (exp2 == null || exp2.Equals(default(Expression)))
      //      {
      //        value = new AtMostTwoExpressions<Variable>(exp1, n / mult_pointer, constant);
      //      }
      //      else
      //      {
      //        value = new AtMostTwoExpressions<Variable>(exp1, exp2, n / mult_pointer, constant);
      //      }
      //      //We check if the actual state of the domain or its refined version can validate it
      //      if (this.Check(pointer, value) || this.RefineInternally().Check(pointer, value))
      //      {
      //        return CheckOutcome.True;
      //      }
      //      else
      //      {
      //        goto top;
      //      }
      //    }
      //  }
      //}

      //top:

      return CheckOutcome.Top;
    }

    /// <summary>
    /// We check if <code>e1 \lt e2</code>. If it false, we return unknown.
    /// </summary>
    public FlatAbstractDomain<bool> CheckIfLessEqualThan(Expression/*!*/ e1, Expression/*!*/ e2)
    {
      return AbstractDomainsHelper.HelperForCheckLessEqualThan(this, e1, e2);
    }

    /// <summary>
    /// Check if exp != 0
    /// </summary>
    public FlatAbstractDomain<bool>/*!*/ CheckIfNonZero(Expression/*!*/ exp)
    {
      return CheckOutcome.Top;
    }

    /// <summary>
    /// It refines internally the information contained by the current state
    /// </summary>
    public Stripe<Variable, Expression, MetaDataDecoder> RefineInternally()
    {
      //// if (!light)
      //if(true)
      //{
      //  Stripe<Variable, Expression, MetaDataDecoder> result = (Stripe<Variable, Expression, MetaDataDecoder>)this.Clone();
      //  foreach (Expression key in this.Keys)
      //    foreach (var el in this[key].EmbeddedValues_Unsafe)
      //    {
      //      if (el.IsBottom == false && el.IsTop == false)//(el.Exp1 == null || el.Exp1.Equals(default(Expression)) || el.Exp2 == null || el.Exp2.Equals(default(Expression))))
      //      {
      //        foreach (var key1 in this.Keys)
      //          foreach (var el1 in this[key1].EmbeddedValues_Unsafe)
      //          {
      //            if (el1.IsTop == false && el.Exp1 != null && key1.Equals(el.Exp1))
      //              if (el.Exp2 == null)
      //              {
      //                //x-a(y)>k && y-b(z+w)>k' -> x-a(b(z+w)+k'+1)>k ->x-a*b(z+w)>k+a*k'+a
      //                result.AddConstraint(key, new AtMostTwoExpressions<Variable>(el1.Exp1, el1.Exp2, el1.N * el.N, el.N * el1.Constant + el.Constant + el.N));
      //              }
      //              else if (el1.N == 1)
      //              {
      //                //x-a(y+z)>k && y-1*w>k' -> x-a(w+k'+1+z)>k -> x-a(w+z)>k+a*k'+a
      //                result.AddConstraint(key, new AtMostTwoExpressions<Variable>(el1.Exp1, el.Exp2, el.N, el.N * el1.Constant + el.Constant + el.N));
      //              }
      //            if (el1.IsTop == false && el.Exp1 != null && el.Exp2 != null && key1.Equals(el.Exp2))
      //            {
      //              //x-a(y+z)>k && z-1*w>k' -> x-a(y+k'+1+w)>k -> x-a(y+w)>k+a*k'+a
      //              result.AddConstraint(key, new AtMostTwoExpressions<Variable>(el.Exp1, el1.Exp1, el.N, el.N * el1.Constant + el.Constant + el.N));
      //            }

      //          }

      //      }
      //    }
      //  return result;
      //}
      // Pietrpo's
      // else return this;

      return this;
    }


    private bool Check(Expression key, AtMostTwoExpressions<Variable> val)
    {
      //if (this.ContainsKey(key) == false) return false;
      //foreach (AtMostTwoExpressions<Variable> constraint in this[key].EmbeddedValues_Unsafe)
      //  if (val.LessEqual(constraint)) return true;
      return false;
    }

    public FlatAbstractDomain<bool>/*!*/ CheckIfLessThan(Expression/*!*/ e1, Expression/*!*/ e2, INumericalAbstractDomain<Variable, Expression>oracleDomain)
    {
      return CheckIfLessThan(e1, e2);
    }

    #endregion

    #region IPureExpressionAssignmentsWithForward<Expression> Members

    public void Assign(Expression x, Expression exp)
    {
      this.Assign(x, exp, TopNumericalDomain<Variable, Expression>.Singleton);
    }

    public void Assign(Expression x, Expression exp, INumericalAbstractDomainQuery<Variable, Expression> preState)
    {
      //var xVar = this.decoder.UnderlyingVariable(x);
      
      //  if (this.ContainsKey(xVar))
      //    this[xVar] = new SetOfConstraints<AtMostTwoExpressions<Variable>>(this[exp].EmbeddedValues);
      //  else
      //    this.AddElement(xVar, new SetOfConstraints<AtMostTwoExpressions<Variable>>(this[exp].EmbeddedValues));

      
      //foreach (var x2 in this.Keys)
      //{
      //  SetOfConstraints<AtMostTwoExpressions<Variable>> constr = this[x2];
      //  foreach (AtMostTwoExpressions<Variable> el in constr.EmbeddedValues)
      //    if (el.Contains(x)) 
      //      el.Replace(x, exp);
      //}

      //AtMostTwoExpressions<Variable> val = ConvertToAtMostTwoExpressions(exp);

      //if (val != null)
      //  this.AddConstraint(x, val);
    }


    /// <summary>
    /// Given an expressions, ii converts it into the right part of a constraint
    /// </summary>
    private AtMostTwoExpressions<Variable> ConvertToAtMostTwoExpressions(Expression e)
    {
      //Variable var;
      //int constant;
      //if (this.MultConstVar(e, out constant, out var))
      //{
      //  //constant*x
      //  if (decoder.IsVariable(var)) return new AtMostTwoExpressions<Variable>(var, constant, 0);
      //  //constant*(x+y)
      //  if (decoder.OperatorFor(var) == ExpressionOperator.Addition && decoder.IsVariable(decoder.LeftExpressionFor(var)) && decoder.IsVariable(decoder.RightExpressionFor(var)))
      //    return new AtMostTwoExpressions<Variable>(decoder.LeftExpressionFor(var), decoder.RightExpressionFor(var), constant, 0);
      //}

      //if (decoder.OperatorFor(e) == ExpressionOperator.Addition)
      //{
      //  Expression var2;
      //  int constant2;
      //  //constant*x+constant*y
      //  if (MultConstVar(decoder.LeftExpressionFor(e), out constant, out var) && MultConstVar(decoder.RightExpressionFor(e), out constant2, out var2)
      //    && constant == constant2 && decoder.IsVariable(var) && decoder.IsVariable(var2))
      //    // f: As we keep strict ">", then it cannot be 0
      //    // return new AtMostTwoExpressions<Variable>(var, var2, constant, 0);
      //    return new AtMostTwoExpressions<Variable>(var, var2, constant, -1);

      //  //x+y
      //  if (decoder.IsVariable(decoder.LeftExpressionFor(e)) && decoder.IsVariable(decoder.RightExpressionFor(e)))
      //    return new AtMostTwoExpressions<Variable>(decoder.LeftExpressionFor(e), decoder.RightExpressionFor(e), 1, 0);

      //}

      return default(AtMostTwoExpressions<Variable>);

    }

    /// <summary>
    /// Return true iff the expression is in the form constant*variable
    /// It stores the constant in the second parameter and the variable in the third
    /// </summary>
    private bool MultConstVar(Expression e, out int cons, out Variable var)
    {
      cons = -1;
      var = default(Variable);
     
      if (decoder.OperatorFor(e) == ExpressionOperator.Multiplication)
      {
        if (decoder.IsConstant(decoder.LeftExpressionFor(e)))
        {
          if (decoder.TryValueOf<int>(decoder.LeftExpressionFor(e), ExpressionType.Int32, out cons))
          {
            var = decoder.UnderlyingVariable(decoder.RightExpressionFor(e));

            return true;
          }
          else
          {
            return false;
          }
        }
        if (decoder.IsConstant(decoder.RightExpressionFor(e)))
        {
          if (decoder.TryValueOf<int>(decoder.RightExpressionFor(e), ExpressionType.Int32, out cons))
          {
            var = decoder.UnderlyingVariable(decoder.LeftExpressionFor(e));

            return true;
          }
          else
          {
            return false;
          }
        }
      }
      if (decoder.IsVariable(e))
      {
        cons = 1;
        var = decoder.UnderlyingVariable(e);
        return true;
      }
      return false;
    }

    /// <summary>
    /// Given a variable, it returns all the constraints in which that variable appears
    /// </summary>
    public Stripe<Variable, Expression, MetaDataDecoder> GetConstraintsOfAVariable(Variable var)
    {
      var result = new Stripe<Variable, Expression, MetaDataDecoder>(this.decoder, this.mdDecoder, this.encoder);
      if (this.ContainsKey(var)) result.AddElement(var, this[var]);
      foreach (var ind in this.Keys)
      {
        if (!ind.Equals(var))
        {
          foreach (AtMostTwoExpressions<Variable> val in this[ind].EmbeddedValues_Unsafe)
          {
            if (val.Contains(var)) result.AddConstraint(ind, val);
          }
        }
      }
      return result;
    }

    /// <summary>
    /// Add the given constraint to the domain, checking if it is already there or if it specifies another constaint yet in the domain
    /// </summary>
    protected void AddConstraint(Variable key, AtMostTwoExpressions<Variable> expr)
    {
      if (this.ContainsKey(key))
      {
        SetOfConstraints<AtMostTwoExpressions<Variable>> set = this[key];
        foreach (AtMostTwoExpressions<Variable> exp2 in set.EmbeddedValues_Unsafe)
          if (exp2.LessEqual(expr))
            return;
          else if (expr.LessEqual(exp2))
          {
            if ((expr.Exp2 == null && exp2.Exp2 == null) || (expr.Exp2 != null && exp2.Exp2 != null))
            {
              exp2.N = expr.N;
              exp2.Constant = expr.Constant;
              exp2.Exp1 = expr.Exp1;
              exp2.Exp2 = expr.Exp2;
              return;
            }
          }
        Set<AtMostTwoExpressions<Variable>> temp_set = set.EmbeddedValues;
        temp_set.Add(expr);
        this[key] = new SetOfConstraints<AtMostTwoExpressions<Variable>>(temp_set);
      }
      else
        this.AddElement(key, new SetOfConstraints<AtMostTwoExpressions<Variable>>(expr));
    }
    #endregion

    #region IPureExpressionAssignments<Expression> Members

    public Set<Variable> Variables
    {
      get
      {
        var result = new Set<Variable>();
        foreach (var x in this.Keys)
        {
          result.Add(x);
          Set<AtMostTwoExpressions<Variable>> vars = this[x].EmbeddedValues;
          foreach (AtMostTwoExpressions<Variable> el in vars)
          {
            if (el.Exp1 != null && !el.Exp1.Equals(default(Expression))) result.Add(el.Exp1);
            if (el.Exp2 != null && !el.Exp2.Equals(default(Expression))) result.Add(el.Exp2);
          }
        }
        return result;
      }
    }

    public void AddVariable(Variable var)
    {
      // Does nothing, as we assume variables initialized to top
    }

    public void ProjectVariable(Variable var)
    {
      this.RemoveVar(var);
    }

    public void RemoveVariable(Variable var)
    {
      this.RemoveVar(var);
    }

    private void RemoveVar(Variable e)
    {
      //Stripe<Variable, Expression, MetaDataDecoder> temp = (Stripe<Variable, Expression, MetaDataDecoder>)this.Clone();

      //foreach (Variable x in temp.Keys)
      //{
      //  if (x.Equals(e)) base.RemoveElement(e);
      //  else
      //  {
      //    SetOfConstraints<AtMostTwoExpressions<Variable>> set = this[x];
      //    foreach (AtMostTwoExpressions<Variable> el in set.EmbeddedValues)
      //      if (el.Contains(e)) el.Replace(e, default(Expression));
      //  }
      //}
    }


    public void RenameVariable(Variable OldName, Variable NewName)
    {
      //foreach (Expression x in this.Keys)
      //{
      //  if (x.Equals(OldName))
      //  {
      //    Set<AtMostTwoExpressions<Variable>> oldVal = this[x].EmbeddedValues;
      //    base.RemoveElement(x);
      //    this[NewName] = new SetOfConstraints<AtMostTwoExpressions<Variable>>(oldVal);
      //  }
      //  else
      //    if (!this[x].IsTop)
      //    {
      //      Set<AtMostTwoExpressions<Variable>> oldVal = this[x].EmbeddedValues;
      //      Set<AtMostTwoExpressions<Variable>> newVal = new System.Collections.Generic.Set<AtMostTwoExpressions<Variable>>(oldVal);
      //      foreach (AtMostTwoExpressions<Variable> el in oldVal)
      //      {
      //        if (el.Contains(OldName))
      //        {
      //          AtMostTwoExpressions<Variable> newvalue = (AtMostTwoExpressions<Variable>)el.Clone();
      //          newvalue.Replace(OldName, NewName);
      //          newVal.Remove(el);
      //          newVal.Add(newvalue);
      //        }
      //      }
      //      this[x] = new SetOfConstraints<AtMostTwoExpressions<Variable>>(newVal);
      //    }
      //}
    }

    #endregion

    #region IPureExpressionTest<Expression> Members

    public IAbstractDomainForEnvironments<Variable, Expression> TestTrue(Expression guard)
    {
      return this.ProcessTestTrue(guard);
    }

    public INumericalAbstractDomain<Variable, Expression>TestTrueGeqZero(Expression/*!*/ exp)
    {
      return this;
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

    public Stripe<Variable, Expression, MetaDataDecoder> TestTrueEqual(Expression/*!*/ exp1, Expression/*!*/ exp2)
    {
      return this;
    }

    public override Stripe<Variable, Expression, MetaDataDecoder>/*!*/ Meet(Stripe<Variable, Expression, MetaDataDecoder>/*!*/ right)
    {
      // Here we do not have trivial joins as we want to join maps of different cardinality
      if (this.IsBottom)
        return this;
      if (right.IsBottom)
        return right;

      Stripe<Variable, Expression, MetaDataDecoder> result = (Stripe<Variable, Expression, MetaDataDecoder>)this.Clone();

      //foreach (Expression x in right.Keys)       // For all the elements in the intersection do the point-wise join
      //{
      //  foreach (AtMostTwoExpressions<Variable> val in right[x].EmbeddedValues_Unsafe)
      //    result.AddConstraint(x, val);
      //}

      return result;
    }


    public IAbstractDomainForEnvironments<Variable, Expression> TestFalse(Expression guard)
    {
      //ALog.BeginTestFalse(StringClosure.For("Stripes"), StringClosure.For(ExpressionPrinter.ToString(guard, this.decoder)), StringClosure.For(""));

      //IAbstractDomainForEnvironments<Variable, Expression>/*!*/ result;

      //if (this.encoder != null)
      //{
      //  Expression notGuard = this.encoder.CompoundExpressionFor(ExpressionType.Bool, ExpressionOperator.Not, guard);
      //  result = TestTrue(notGuard);
      //}
      //else
      //{
      //  result = ProcessTestFalse(guard);
      //}

      //ALog.EndTestFalse(StringClosure.For(result));

      //return result;

      return this;
    }

    private IAbstractDomainForEnvironments<Variable, Expression> ProcessTestTrue(Expression guard)
    {
      //IAbstractDomainForEnvironments<Variable, Expression>/*!*/ result;

      //Expression left, right;

      //switch (this.decoder.OperatorFor(guard))
      //{
        //#region all the cases
        //case ExpressionOperator.Addition:
        //  result = this;
        //  break;

        //case ExpressionOperator.And:
        //  result = this;
        //  break;

        //case ExpressionOperator.Constant:
        //case ExpressionOperator.ConvertToInt32:
        //case ExpressionOperator.ConvertToUInt8:
        //case ExpressionOperator.ConvertToUInt16:
        //case ExpressionOperator.ConvertToUInt32:
        //case ExpressionOperator.ConvertToFloat32:
        //case ExpressionOperator.Division:
        //  result = this;
        //  break;

        //case ExpressionOperator.Equal:
        //case ExpressionOperator.Equal_Obj:
        //  left = this.decoder.LeftExpressionFor(guard);
        //  right = this.decoder.RightExpressionFor(guard);

        //  //v1==v2
        //  //if (this.ContainsKey(left) && this.ContainsKey(right))
        //  //{
        //  //  SetOfConstraints<AtMostTwoExpressions<Variable>> v1 = this[left];
        //  //  SetOfConstraints<AtMostTwoExpressions<Variable>> v2 = this[right];
        //  //  SetOfConstraints<AtMostTwoExpressions<Variable>> intersection = (SetOfConstraints<AtMostTwoExpressions<Variable>>)v1.Meet(v2);
        //  //  if (v1.IsBottom)
        //  //  {
        //  //    result = (IAbstractDomainForEnvironments<Variable, Expression>)this.Bottom;
        //  //  }
        //  //  else
        //    {
        //      //this[left] = intersection;
        //      //this[right] = intersection;

        //      result = this;
        //    }
        //  //}
        //  //else
        //  //{// Try to figure out if it is in the form of (e11 rel e12) == 0
        //  //  Expression e11, e12;
        //  //  ExpressionOperator op;
        //  //  if (ExpressionHelper.Match_E1relopE2eq0(left, right, out op, out e11, out e12, this.decoder))
        //  //  {
        //  //    result = ProcessTestFalse(left);
        //  //  }
        //  //  else
        //  //  {// Try to figure out if it is in the form of (e11 arit_op e12) == 0
        //  //    if (ExpressionHelper.Match_E1aritopE2eq0(left, right, out op, out e11, out e12, this.decoder))
        //  //    {
        //  //      result = this.TestEquivalence(left);
        //  //    }
        //  //    else
        //  //    {
        //  //      result = this;
        //  //    }
        //  //  }
        //  // }
        //  break;

        //case ExpressionOperator.GreaterEqualThan:
        //case ExpressionOperator.GreaterEqualThan_Un:
        //  // "left >= right", so we add the constraint as left>right+1
        //  left = this.decoder.LeftExpressionFor(guard);
        //  right = this.decoder.RightExpressionFor(guard);
        //  result = HelperForGreaterThan(left, right, 1);
        //  break;

        //case ExpressionOperator.GreaterThan:
        //case ExpressionOperator.GreaterThan_Un:
        //  // "left > right", so we add the constraint as left>right
        //  left = this.decoder.LeftExpressionFor(guard);
        //  right = this.decoder.RightExpressionFor(guard);
        //  result = HelperForGreaterThan(left, right, 0);
        //  break;

        //case ExpressionOperator.LessEqualThan:
        //case ExpressionOperator.LessEqualThan_Un:
        //  // "left <= right", so we add the constraint as right>left+1
        //  left = this.decoder.LeftExpressionFor(guard);
        //  right = this.decoder.RightExpressionFor(guard);
        //  result = HelperForGreaterThan(right, left, 1);
        //  break;

        //case ExpressionOperator.LessThan:
        //case ExpressionOperator.LessThan_Un:
        //  // "left < right", so we add the constraint as right>left
        //  left = this.decoder.LeftExpressionFor(guard);
        //  right = this.decoder.RightExpressionFor(guard);
        //  result = HelperForGreaterThan(right, left, 0);
        //  break;

        //case ExpressionOperator.Or:
        //  result = this;
        //  break;

        //case ExpressionOperator.Modulus:
        //case ExpressionOperator.ShiftLeft:
        //case ExpressionOperator.ShiftRight:
        //case ExpressionOperator.SizeOf:
        //case ExpressionOperator.Multiplication:
        //  // Does nothing ...
        //  result = this;
        //  break;

        //case ExpressionOperator.Not:
        //  left = this.decoder.LeftExpressionFor(guard);
        //  result = this.ProcessTestFalse(left);
        //  break;

        //case ExpressionOperator.NotEqual:
        //  result = this;
        //  break;

        //case ExpressionOperator.Subtraction:
        //  result = this;
        //  break;

        //case ExpressionOperator.UnaryMinus:
        //case ExpressionOperator.Unknown:
        //case ExpressionOperator.Variable:
        //  result = this;
        //  break;

        //default:
        //  throw new AbstractInterpretationException("Unhandled operator : " + this.decoder.OperatorFor(guard));
        //#endregion
      //}
      
      //return result;
      return this;
    }


    //It checks the possible equivalences supposing that the initial expression was e==0
    private Stripe<Variable, Expression, MetaDataDecoder> TestEquivalence(Expression e)
    {
      //Polynomial<Variable, Expression> polynomial;
      //if (Polynomial<Variable, Expression>.TryToPolynomialForm(e, this.decoder, out polynomial))
      //{
      //  if (polynomial.Relation != null) 
      //    return this;

      //  IList<Monomial<Expression>> monom = polynomial.Left;
        
      //  if (monom.Count > 3 || monom.Count == 1) 
      //    return this;
        
      //  if (monom.Count == 2)
      //  {
      //    Monomial<Expression> var1, var2;

      //    var1 = monom[0];
      //    var2 = monom[1];

      //    Rational k1 = var1.K;
      //    Rational k2 = var2.K;

      //    if (var1.Variables.Count != 1 || var2.Variables.Count != 1)
      //    {
      //      return this;
      //    }
      //    else
      //    {
      //      return this.AssumeEquivalence(var1.Variables[0], var2.Variables[0], k1, k2).AssumeEquivalence(var2.Variables[0], var1.Variables[0], k2, k1);
      //    }
      //  }
      //}
      return this;
    }

    //Eventually it infers some information from the expression k1*e1+k2*e2
    private Stripe<Variable, Expression, MetaDataDecoder> AssumeEquivalence(Expression e1, Expression e2, Rational k1, Rational k2)
    {
      //Rational div = (-(k1 / k2));

      //if (this.ContainsKey(e1) &&  div >= 0 && div.IsInteger)
      //{
      //  foreach (AtMostTwoExpressions<Variable> oldConstraints in this[e1].EmbeddedValues_Unsafe)
      //  {
      //    AtMostTwoExpressions<Variable> newConstraints = (AtMostTwoExpressions<Variable>)oldConstraints.Clone();
      //    newConstraints.N = newConstraints.N * ((Int32) div);

      //    this.AddConstraint(e2, newConstraints);
      //  }
      //}
      return this;
    }

    virtual protected IAbstractDomainForEnvironments<Variable, Expression>/*!*/ ProcessTestFalse(Expression/*!*/ guard)
    {
      //IAbstractDomainForEnvironments<Variable, Expression>/*!*/ result;

      //Expression left, right;

      //switch (this.decoder.OperatorFor(guard))
      //{
      //  #region all the cases
      //  case ExpressionOperator.Addition:
      //    result = this;
      //    break;

      //  case ExpressionOperator.And:
      //    result = this;
      //    break;

      //  case ExpressionOperator.Constant:
      //  case ExpressionOperator.ConvertToInt32:
      //  case ExpressionOperator.ConvertToUInt8:
      //  case ExpressionOperator.ConvertToUInt16:
      //  case ExpressionOperator.ConvertToUInt32:
      //  case ExpressionOperator.ConvertToFloat32:
      //  case ExpressionOperator.Division:
      //    result = this;
      //    break;

      //  case ExpressionOperator.Equal:
      //  case ExpressionOperator.Equal_Obj:
      //    left = this.decoder.LeftExpressionFor(guard);
      //    right = this.decoder.RightExpressionFor(guard);
      //    // Try to figure out if it is in the form of (e11 rel e12) == 0
      //    Expression e11, e12;
      //    ExpressionOperator op;
      //    if (ExpressionHelper.Match_E1relopE2eq0(left, right, out op, out e11, out e12, this.decoder))
      //    {
      //      result = ProcessTestTrue(left);
      //    }
      //    else
      //    {
      //      result = this;
      //    }
      //    break;


      //  case ExpressionOperator.GreaterEqualThan:
      //  case ExpressionOperator.GreaterEqualThan_Un:
      //    // !(left >= right) is equivalent to left < right
      //    left = this.decoder.LeftExpressionFor(guard);
      //    right = this.decoder.RightExpressionFor(guard);
      //    result = HelperForGreaterThan(right, left, 0);
      //    break;

      //  case ExpressionOperator.GreaterThan:
      //  case ExpressionOperator.GreaterThan_Un:
      //    // !(left >= right) is equivalent to left + 1 < right
      //    left = this.decoder.LeftExpressionFor(guard);
      //    right = this.decoder.RightExpressionFor(guard);
      //    result = HelperForGreaterThan(right, left, 1);
      //    break;

      //  case ExpressionOperator.LessEqualThan:
      //  case ExpressionOperator.LessEqualThan_Un:
      //    // !(left <= right) => left > right
      //    left = this.decoder.LeftExpressionFor(guard);
      //    right = this.decoder.RightExpressionFor(guard);
      //    result = HelperForGreaterThan(left, right, 0);
      //    break;

      //  case ExpressionOperator.LessThan:
      //  case ExpressionOperator.LessThan_Un:
      //    // !(left < right) => left > right+1
      //    left = this.decoder.LeftExpressionFor(guard);
      //    right = this.decoder.RightExpressionFor(guard);
      //    result = HelperForGreaterThan(left, right, 1);
      //    break;

      //  case ExpressionOperator.Or:
      //    result = this;
      //    break;

      //  case ExpressionOperator.Modulus:
      //  case ExpressionOperator.Multiplication:
      //    // Does nothing ...
      //    result = this;
      //    break;

      //  case ExpressionOperator.Not:
      //    left = this.decoder.LeftExpressionFor(guard);
      //    result = this.ProcessTestTrue(left);
      //    break;

      //  case ExpressionOperator.NotEqual:
      //    result = this;
      //    break;

      //  case ExpressionOperator.ShiftLeft:
      //  case ExpressionOperator.ShiftRight:
      //  case ExpressionOperator.SizeOf:
      //  case ExpressionOperator.Subtraction:
      //    result = this;
      //    break;

      //  case ExpressionOperator.UnaryMinus:
      //  case ExpressionOperator.Unknown:
      //  case ExpressionOperator.Variable:
      //    result = this;
      //    break;

      //  default:
      //    throw new AbstractInterpretationException("Unhandled operator : " + this.decoder.OperatorFor(guard));
      //  #endregion
      //}

      //return result;

      return this;
    }


    private IAbstractDomainForEnvironments<Variable, Expression> HelperForGreaterThan(Expression left, Expression right, int i)
    {
      //AtMostTwoExpressions<Variable> newConstraint = new AtMostTwoExpressions<Variable>(right, 1, -i);
      //this.AddConstraint(left, newConstraint);

      //Polynomial<Variable, Expression> p1, p2;
      //if (Polynomial<Variable, Expression>.TryToPolynomialForm(left, this.decoder, out p1)
      //  && Polynomial<Variable, Expression>.TryToPolynomialForm(right, this.decoder, out p2))
      //{
      //  Polynomial<Variable, Expression> complete;

      //  if (!Polynomial<Variable, Expression>.TryToPolynomialForm(ExpressionOperator.GreaterThan, p1, p2, out complete))
      //  {
      //    return this;
      //  }
 
      //  //We expect something like -x+ay[+az]<k
      //  if (ExpressionHelper.IsLessThan(complete.Relation.Value))
      //  {
      //    if (complete.Left.Count > 3 || complete.Left.Count < 2)
      //    {
      //      return this;
      //    }

      //    int constant;
      //    bool success;
      //    //Note that we have something like -x+ay[+az]<k but our constraints are in the form x-ay[-az]>-k, so we have to compute -k
      //    constant = -this.ConstantValue(complete.Right, out success) - i;
      //    if (!success) return this;
      //    int mult;
      //    Expression pointer = this.FindPointer(complete.Left, out mult);
      //    if (pointer == null || pointer.Equals(default(Expression))) return this;
      //    Expression exp1, exp2;
      //    int n;
      //    if (!this.FindOtherVariables(complete.Left, out exp1, out exp2, out n)) return this;
      //    AtMostTwoExpressions<Variable> value;
      //    if (exp2 == null || exp2.Equals(default(Expression)))
      //    {
      //      value = new AtMostTwoExpressions<Variable>(exp1, n / mult, constant);
      //    }
      //    else value = new AtMostTwoExpressions<Variable>(exp1, exp2, n / mult, constant);
      //    this.AddConstraint(pointer, value);
      //  }
      //}
      return this;
    }

    /// <summary>
    /// It returns the constant values of m_1+m_2+... where m_i is the i_th element of the given list
    /// If a monome contains a variable it fails
    /// </summary>
    private int ConstantValue(IList<Monomial<Variable>> monome, out bool success)
    {
      int result = 0;
      foreach (Monomial<Variable> m in monome)
      {
        if (!m.IsConstant) 
        { 
          success = false; 
          return 0; 
        }
        
        // result += m.K.Numerator / m.K.Denominator;
        result += (Int32)m.K;
      }

      success = true;
      return result;
    }

    /// <summary>
    /// It searches in the given monomes at most two monomes like a*x and b*y suche that a==b and a,b>=0
    /// It returns false if it does not find them, otherwise it passes the informations throught the 2nd, 3rd, and 4th parameters
    /// </summary>
    private bool FindOtherVariables(IList<Monomial<Expression>> monome, out Expression e1, out Expression e2, out int n)
    {
      e1 = default(Expression);
      e2 = default(Expression);
      bool first = true;
      int mult = 0;
      n = 0;
      foreach (Monomial<Expression> m in monome)
      {
        foreach (Expression e in m.Variables)
        {
          // Old:
          // if (m.K.Numerator >= 0)
          if(m.K >= 0)
          {
            if (first)
            {
              // Old:
              // mult = m.K.Numerator;
              mult = (Int32)m.K;
              e1 = e;
              first = false;
            }
            else
            {
              // Old:
              // if (mult != m.K.Numerator)
              if(mult != (Int32) m.K)
              {
                return false;
              }

              e2 = e;
            }
          }
        }
      }

      n = mult;

      if (first)
      {
        return false;
      }
      // else? 
      return true;
    }

    /// <summary>
    /// It searches the monome a*x such that a is less or equal than -1
    /// If there are two monomes in this situation, it fails
    /// It is used to find the pointer after that the monomes have been obtained in a canonical form of a polynomial
    /// </summary>
    private Expression FindPointer(IList<Monomial<Expression>> monome, out int mult)
    {
      mult = 0;
      Expression result = default(Expression);

      foreach (Monomial<Expression> m in monome)
      {
        foreach (Expression e in m.Variables)
        {
          // Old:
          // if (m.K.Numerator <= -1 && m.Variables.Count == 1)
          if(m.K <= -1 && m.Variables.Count == 1)
          {
            // Old:
            //mult = -m.K.Numerator;

            mult = (Int32) (-m.K);

            if (result == null || result.Equals(default(Expression))) 
              result = e;
            else 
              return default(Expression);
          }
        }
      }

      return result;
    }

    #endregion

    public Stripe<Variable, Expression, MetaDataDecoder> Join(Stripe<Variable, Expression, MetaDataDecoder> prev, Set<Expression> pointer)
    {
      return this.Join(prev);
    }

    public override Stripe<Variable, Expression, MetaDataDecoder> Widening(Stripe<Variable, Expression, MetaDataDecoder> right)
    {
      return this.Join(right);
    }

    #region IPureExpressionAssignmentsWithBackward<Expression> Members : TODO

    public void AssignBackward(Expression x, Expression exp)
    {
      // TODO4: Assign backward
      throw new Exception("The method or operation is not implemented.");
    }

    public void AssignBackward(Expression x, Expression exp, IAbstractDomain preState, out IAbstractDomain refinedPreState)
    {
      // TODO4: Assign backwards
      throw new Exception("The method or operation is not implemented.");
    }

    #endregion

    #region ToString

    protected override string ToLogicalFormula(Variable d, SetOfConstraints<AtMostTwoExpressions<Variable>> c)
    {
      return null;
    }

    public override string/*!*/ ToString()
    {
      if (this.IsTop)
        return "Top";

      if (this.IsBottom)
        return "Bottom";

      StringBuilder builder = new StringBuilder();
      foreach (var exp in this.Keys)
      {
        string niceX = ExpressionPrinter.ToString(exp, this.decoder);
        foreach (AtMostTwoExpressions<Variable> x in this[exp].EmbeddedValues_Unsafe)
        {
          string xAsString = "";
          if (x.IsBottom) xAsString = "Bottom";
          else if (x.IsTop) xAsString = "Top";
          else xAsString += niceX + "-" + x.ToString();
          builder.AppendLine(xAsString);
        }
      }
      return builder.ToString();
    }

    private string/*!*/ ToString(Set<AtMostTwoExpressions<Variable>> set)
    {
      StringBuilder result = new StringBuilder();
      result.Append("{");
      int c = 0;

      foreach (AtMostTwoExpressions<Variable> x in set)
      {
        string xAsString = "";
        if (x.IsBottom) xAsString = "Bottom";
        else if (x.IsTop) xAsString = "Top";
        else
        {
          if (x.Exp1 != null) xAsString = "-" + x.N + "*" + ExpressionPrinter.ToString(x.Exp1, this.decoder);
          if (x.Exp2 != null) xAsString += "-" + x.N + "*" + ExpressionPrinter.ToString(x.Exp2, this.decoder);
        }

        result.Append(xAsString);

        if (c < set.Count)
        {
          result.Append(", ");
        }

        c++;
      }

      result.Append("}");

      return result.ToString();
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
    #endregion

    protected override T To<T>(Variable d, SetOfConstraints<AtMostTwoExpressions<Variable>> c, IFactory<T> factory)
    {
      if (c.IsTop)
        return factory.Constant(true);
      if (c.IsBottom)
        return factory.Constant(false);

      T result = factory.IdentityForAnd;

      T x = factory.Variable(d);

      // We know the constraints are x - exp > k
      foreach (AtMostTwoExpressions<Variable> exp in c.EmbeddedValues_Unsafe)
      {
        if (exp.IsTop || exp.IsBottom)
          continue;

        T atomForExp;

        if(exp.CountExpressions == 1)
        {
          atomForExp = factory.Variable(exp.Exp1);
        }
        else if (exp.CountExpressions == 2)
        {
          atomForExp = factory.Add(factory.Variable(exp.Exp1), factory.Variable(exp.Exp2));
        }
        else
        { // unreachable? This should became a postcondition of CountExpressions
          Debug.Assert(false);
          throw new AbstractInterpretationException("Impossible case?");
        }

        atomForExp = factory.Mul(factory.Constant(exp.N), atomForExp); // exp.N * (exp.exp1 + exp.exp2)

        T atom = factory.LessThan(factory.Constant(exp.Constant), atomForExp);

        result = factory.And(result, atom);
      }

      return result;
    }


    #region Floating point types

    public void SetFloatType(Variable exp, Floats f)
    {
      // does nothing
    }

    public FlatAbstractDomain<Floats> GetFloatType(Variable exp)
    {
      return new FlatAbstractDomain<Floats>(Floats.Float32).Top;
    }

    #endregion
  }

  public class AtMostTwoExpressions<Variable> 
    : IAbstractDomain
  {
    #region Private state
    private Variable/*!*/ exp1;
    private Variable/*!*/ exp2;

    private int n = 0;
    
    private int constant;
    
    private enum State { Bottom, Normal }
    
    readonly private State state;
    
    #endregion

    #region Getters and setters
    public Variable Exp1
    {
      get
      {
        if (exp1 == null && exp2 != null)
        {
          exp1 = exp2;
          exp2 = default(Variable);
        }
        return exp1;
      }
      set
      {
        exp1 = value;
      }
    }

    public Variable Exp2
    {
      get
      {
        return exp2;
      }
      set
      {
        exp2 = value;
      }
    }

    internal int CountExpressions
    {
      get
      {
        if (this.exp1 != null)
        {
          return this.exp2 != null ? 2 : 1;
        }
        return 1;
      }
    }

    public Set<Variable> VariablesIn
    {
      get
      {
        Set<Variable> result = new Set<Variable>(2);
        result.Add(this.Exp1);
        if (!this.Exp2.Equals(default(Variable)))
        {
          result.Add(this.Exp2);
        }

        return result;
      }
    }

    public int Constant
    {
      get
      {
        return constant;
      }
      set
      {
        constant = value;
      }
    }
    public bool IsBottom
    {
      get
      {
        if (state == State.Bottom)
          return true;
        else
          return false;
      }
    }
    public bool IsTop
    {
      get
      {
        if ((exp1 == null || exp1.Equals(default(Variable))) && (exp2 == null || exp2.Equals(default(Variable))))
          return true;
        else
          return false;
      }
    }

    public IAbstractDomain Bottom
    {
      get
      {
        return new AtMostTwoExpressions<Variable>();
      }
    }

    public IAbstractDomain Top
    {
      get
      {
        return new AtMostTwoExpressions<Variable>(default(Variable), default(Variable), 0, 0);
      }
    }

    public int N
    {
      get
      {
        return n;
      }
      set
      {
        n = value;
      }
    }
    #endregion

    #region Constructors
    public AtMostTwoExpressions(Variable e, int n, int constant)
    {
      state = State.Normal;
      this.exp1 = e;
      this.n = n;
      this.constant = constant;
    }

    public AtMostTwoExpressions(Variable e1, Variable e2, int n, int constant)
    {
      Debug.Assert(e1 != null || e2 != null);
      
      state = State.Normal;
      this.exp1 = e1;
      this.exp2 = e2;
      this.n = n;
      this.constant = constant;
    }

    public AtMostTwoExpressions()
    {
      state = State.Bottom;
    }
    #endregion

    /// <summary>
    /// Return true iff it contains the variable represented by e
    /// </summary>
    public bool Contains(Variable e)
    {
      if (this.IsBottom) return false;
      if (this.exp1 != null && exp1.Equals(e)) return true;
      if (this.exp2 != null && exp2.Equals(e)) return true;
      return false;
    }

    /// <summary>
    /// Replace e with s
    /// </summary>
    public void Replace(Variable e, Variable s)
    {
      if (this.exp1 != null && exp1.Equals(e)) exp1 = s;
      if (this.exp2 != null && exp2.Equals(e)) exp2 = s;
    }

    public bool LessEqual(IAbstractDomain/*!*/ a)
    {
      Debug.Assert(a is AtMostTwoExpressions<Variable>, "Error: expecting an AtMostTwoExpressions object");
      AtMostTwoExpressions<Variable> other = (AtMostTwoExpressions<Variable>)a;
      if (this.IsBottom) return true;
      if (other.IsBottom) return false;

      if (a.IsTop)
        if (this.IsTop) return true;
        else return false;
      if (this.IsTop) return false;

      bool lessOrEqual = this.N == other.N && this.Constant <= other.Constant && this.Exp1.Equals(other.Exp1);
      if (this.Exp2 == null && other.Exp2 == null) return lessOrEqual;
      else
      {
        if (this.Exp2 == null && other.Exp2 != null) return false;
        else
        {
          if (this.Exp2 != null && other.Exp2 == null) return false;
          else
          {
            if (lessOrEqual && this.Exp2.Equals(other.Exp2))
              return true;
            else
              return this.N == other.N && this.Constant <= other.Constant && this.Exp1.Equals(other.Exp2) & this.Exp2.Equals(other.Exp1);
          }
        }
      }

    }

    public override int GetHashCode()
    {
      if (this.IsBottom || this.IsTop) return 1;
      if (exp1 != null) return exp1.GetHashCode();
      else return exp2.GetHashCode();
    }

    public override bool Equals(object obj)
    {
      AtMostTwoExpressions<Variable> conf = obj as AtMostTwoExpressions<Variable>;
      if (conf == null) return false;
      if (this.IsBottom && conf.IsBottom) return true;
      if (this.IsTop && conf.IsTop) return true;
      var e1 = conf.Exp1;
      var e2 = conf.Exp2;
      if (e1 == null) e1 = e2;
      if (exp1 == null)
      {
        exp1 = exp2;
        exp2 = default(Variable);
      }

      bool result = this.N == conf.N && this.constant == conf.constant;
      result = result && ((this.exp1 == null && e1 == null) || this.exp1.Equals(e1));
      if ((this.exp2 == null && e2 != null) || (e2 == null && this.exp2 != null)) return false;

      result = result && ((this.exp2 == null && e2 == null) || this.exp2.Equals(e2));
      return result;
    }

    public object Clone()
    {
      if (exp1 == null)
      {
        exp1 = exp2;
        exp2 = default(Variable);
      }

      if (this.IsBottom) return new AtMostTwoExpressions<Variable>();

      else if (exp2 == null) return new AtMostTwoExpressions<Variable>(exp1, n, constant);
      else return new AtMostTwoExpressions<Variable>(exp1, exp2, n, constant);
    }

    public IAbstractDomain/*!*/ Join(IAbstractDomain/*!*/ a)
    {
      Debug.Assert(a is AtMostTwoExpressions<Variable>, "Error: expecting an AtMostTwoExpressions object");
      AtMostTwoExpressions<Variable> right = (AtMostTwoExpressions<Variable>)a;

      if (this.LessEqual(right)) return right;
      if (right.LessEqual(this)) return this;
      return Top;
    }

    public IAbstractDomain/*!*/ Meet(IAbstractDomain/*!*/ a)
    {
      Debug.Assert(a is AtMostTwoExpressions<Variable>, "Error: expecting an AtMostTwoExpressions object");
      if (this.LessEqual(a)) return this;
      if (a.LessEqual(this)) return a;
      return Bottom;
    }


    public IAbstractDomain/*!*/ Widening(IAbstractDomain/*!*/ prev)
    {
      //The domain is of finite height, so we can apply the join operator
      return this.Join(prev);
    }

    public string ToRewritingRule()
    {
      // TODO1
      return "TODO";
    }

    public T To<T>(IFactory<T> factory)
    {
      // Because of the way things are implemented, this domain should never be called without stripes, and stripes should never call this method
      throw new AbstractInterpretationException("Not supposed to be called!!!");
    }

    public override string ToString()
    {
      if (exp1 == null && exp2 != null)
      {
        exp1 = exp2;
        exp2 = default(Variable);
      }
      if (this.IsBottom) return "Bottom";
      if (this.IsTop) return "Top";
      if (exp2 != null && !exp2.Equals(default(Variable)))
        return n + "*(" + exp1.ToString() + "+" + exp2.ToString() + ") > " + constant;
      else return n + "*" + exp1.ToString() + " > " + constant;
    }


    #region Floating point types

    public void SetFloatType(Variable v, Floats f)
    {
      // does nothing
    }

    public FlatAbstractDomain<Floats> GetFloatType(Variable v)
    {
      return new FlatAbstractDomain<Floats>(Floats.Float32).Top;
    }

    #endregion
  }
}
