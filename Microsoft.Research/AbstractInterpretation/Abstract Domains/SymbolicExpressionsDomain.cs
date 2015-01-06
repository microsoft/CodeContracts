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

// The implementation for the abstract domain of symbolic expressions

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Research.AbstractDomains.Expressions;
using System.Diagnostics;
using Microsoft.Research.DataStructures;

//^ using Microsoft.Contracts;

namespace Microsoft.Research.AbstractDomains
{
  /// <summary>
  /// The abstract domain of symbolic expressions.
  /// It keeps track of the bindings between variables and the expression that is associated with them.
  /// </summary>
  /// <typeparam name="Expression">The type of the expressions in this symbolic abstract domain</typeparam>
  public class SymbolicExpressionsAbstractDomain<Expression> : 
    ISymbolicExpressionsAbstractDomain<Expression>
  {
    #region Cached values

    private static FunctionalAbstractDomain<Expression, FlatAbstractDomainWithComparer<Expression>> cachedTopMap;         // They are initialized by the static constructor
    private static FlatAbstractDomainWithComparer<Expression> cachedBottomExpression;
    private static FlatAbstractDomainWithComparer<Expression> cachedTopExpression;

    #endregion

    #region Private fields
    // TODO5: keep the types for the expressions in the symbolic domain
    // TODO4: make var2exp lazy 

    // private FunctionalAbstractDomain<Expression, FlatAbstractDomain<Expression>>/*^!*/ var2exp;        // var2exp: Variable -> Expression \cup {bottom, top } 

    private FunctionalAbstractDomain<Expression, FlatAbstractDomainWithComparer<Expression>>/*!*/ var2exp;        // var2exp: Variable -> Expression \cup {bottom, top } 

    #endregion

    #region Protected fields (Decoder and Encoder)

    protected IExpressionDecoder<Expression> /*!*/ decoder;
    protected IExpressionEncoder<Expression> /*!*/ encoder;

    #endregion

    #region Constructors
    /// <summary>
    /// Construct an empty abstract domain of symbolic expressions
    /// </summary>
    public SymbolicExpressionsAbstractDomain(IExpressionDecoder<Expression> decoder, IExpressionEncoder<Expression> encoder)
    {
      this.decoder = decoder;
      this.encoder = encoder;
      this.var2exp = new SimpleFunctional<Expression, FlatAbstractDomainWithComparer<Expression>>();

      InitStaticFields();
    }

    private SymbolicExpressionsAbstractDomain(SymbolicExpressionsAbstractDomain<Expression> s)
    {
      this.decoder = s.decoder;
      this.encoder = s.encoder;
      this.var2exp = s.var2exp;
    }

    private SymbolicExpressionsAbstractDomain(FunctionalAbstractDomain<Expression, FlatAbstractDomainWithComparer<Expression>> var2exp, IExpressionDecoder<Expression> decoder, IExpressionEncoder<Expression> encoder)
    {
      this.decoder = decoder;
      this.encoder = encoder;
      this.var2exp = var2exp;
    }
    #endregion

    #region IAbstractDomain Members

    public bool IsBottom
    {
      get
      {
        return this.var2exp.IsBottom;
      }
    }

    public bool IsTop
    {
      get
      {
        return this.var2exp.IsTop;
      }
    }

    /// <summary>
    /// Pointwise order
    /// </summary>
    public bool LessEqual(IAbstractDomain/*!*/ a)
    {
      bool result;
      if (AbstractDomainsHelper.TryTrivialLessEqual(this, a, out result))
      {
        return result;
      }

      Debug.Assert(a is SymbolicExpressionsAbstractDomain<Expression>
          , "Expecting an instance of SymbolicExpressionsAbstractDomain. Found " + a); //^ assert a is SymbolicExpressionsAbstractDomain<Expression>;

      SymbolicExpressionsAbstractDomain<Expression> right = a as SymbolicExpressionsAbstractDomain<Expression>;

      return this.var2exp.LessEqual(right.var2exp);           // We do not need to compare the free variables, as they are a consequence
    }

    public IAbstractDomain Bottom
    {
      get
      {
        return new SymbolicExpressionsAbstractDomain<Expression>(this.decoder, this.encoder);
      }
    }

    public IAbstractDomain Top
    {
      get
      {
        return new SymbolicExpressionsAbstractDomain<Expression>(cachedTopMap, this.decoder, this.encoder);
      }
    }

    public IAbstractDomain/*!*/ Join(IAbstractDomain/*!*/ a)
    {
      IAbstractDomain/*!*/ result;
      if (AbstractDomainsHelper.TryTrivialJoin(this, a, out result))
      {
        return result;
      }

      Debug.Assert(a is SymbolicExpressionsAbstractDomain<Expression>, "Expecting an instance of SymbolicExpressionsAbstractDomain. Found " + a); 
      //^ assert a is SymbolicExpressionsAbstractDomain<Expression>;

      SymbolicExpressionsAbstractDomain<Expression> right = a as SymbolicExpressionsAbstractDomain<Expression>;

      FunctionalAbstractDomain<Expression, FlatAbstractDomainWithComparer<Expression>> joinSymbolicExpressions =
              (FunctionalAbstractDomain<Expression, FlatAbstractDomainWithComparer<Expression>>)this.var2exp.Join(right.var2exp);    // Join the symbolic expression bindings

      Debug.Assert(!joinSymbolicExpressions.IsBottom, "The join of two non-bottom expression environments cannot be bottom");  
      //^ assert !joinSymbolicExpressions.IsBottom;

      /*
      if (joinSymbolicExpressions.IsTop)
      {
          return this.Top;
      }
      else
      {*/
      return new SymbolicExpressionsAbstractDomain<Expression>(joinSymbolicExpressions, this.decoder, this.encoder);
      //}
    }

    public IAbstractDomain/*!*/ Meet(IAbstractDomain/*!*/ a)
    {
      IAbstractDomain trivialMeet;
      if (AbstractDomainsHelper.TryTrivialMeet(this, a, out trivialMeet))
      {
        return trivialMeet;
      }

      Debug.Assert(a is SymbolicExpressionsAbstractDomain<Expression>,
              "Expecting an instance of SymbolicExpressionsAbstractDomain. Found " + a); //^ assert a is SymbolicExpressionsAbstractDomain<Expression>;

      SymbolicExpressionsAbstractDomain<Expression> right = a as SymbolicExpressionsAbstractDomain<Expression>;

      FunctionalAbstractDomain<Expression, FlatAbstractDomainWithComparer<Expression>> meetSymbolicExpressions =
          (FunctionalAbstractDomain<Expression, FlatAbstractDomainWithComparer<Expression>>)this.var2exp.Meet(right.var2exp);    // Meet the symbolic expression bindings

      Debug.Assert(!meetSymbolicExpressions.IsTop, "The meet of two non-top expression environments cannot be top ");  
      // ^ assert !joinSymbolicExpressions.IsTop;

      if (meetSymbolicExpressions.IsBottom)
      {
        return this.Bottom;
      }
      else
      {
        return new SymbolicExpressionsAbstractDomain<Expression>(meetSymbolicExpressions, this.decoder, this.encoder);
      }
    }

    /// <summary>
    /// This abstract domain satisfies the ACC condition, so the Widening is just the join
    /// </summary>
    public IAbstractDomain/*!*/ Widening(IAbstractDomain/*!*/ prev)
    {
      return this.Join(prev);
    }

    #endregion

    #region IPureExpressionAssignments Members

    /// <summary>
    /// The variables defined in this domain
    /// </summary>
    public ISet<Expression> Variables
    {
      get
      {
        return new Set<Expression>(this.var2exp.Keys);
      }
    }

    /// <summary>
    /// The assigment <code>x := exp</code>.
    /// If the assigment is invertible, <code>isInvertible</code> will be set to true. Otherwise to false
    /// </summary>
    public void Assign(Expression/*!*/ x, Expression/*!*/ exp)
    {
      Debug.Assert(this.decoder.IsVariable(x), "I was expecting a variable, I found " + x);

      #region 1. update the binding for x

      Expression newExp = exp;
      bool newExpMustBeUnknown = false;
      bool newExpMustBeBottom = false;

      // We replace each occurrence of var with the known expression
      foreach (Expression var in this.decoder.VariablesIn(newExp))
      {
        Debug.Assert(this.decoder.IsVariable(var), "Error: " + var + " is not a variable");
        if (this.var2exp.ContainsKey(var))      // We have a binding for the variable?
        {
          if (this.var2exp[var].IsTop)       // Is it an unknwon value? then all the expression is unknown
          {
            // newExpMustBeUnknown = true;
          }
          else if (this.var2exp[var].IsBottom) // Is it bottom? Then all the expression evaluates to bottom
          {
            newExpMustBeBottom = true;
          }
          else                                 // We do the update in loco
          {
            // newExp = newExp.Substitute(var, this.var2exp[var].BoxedElement);
            newExp = this.encoder.Substitute(newExp, var, this.var2exp[var].BoxedElement);
          }
        }
      }

      // If a subexpression is unknown or unreached, all the binding for x is. Otherwise we update the binding for x 
      if (newExpMustBeBottom)
      {
        this.var2exp[x] = cachedBottomExpression;
      }
      else if (newExpMustBeUnknown)
      {
        this.var2exp[x] = cachedTopExpression;
      }
      else
      {
        if (this.decoder.VariablesIn(newExp).Contains(x))
        {
          this.var2exp[x] = cachedTopExpression;
        }
        else
        {
          this.var2exp[x] = new FlatAbstractDomainWithComparer<Expression>(newExp, this.decoder);
        }
      }

      #endregion

      #region 2. update the bindings that depend from (the old value of) x, and so on transitively..

      ISet<Expression> noMoreValidBindings = new Set<Expression>();
      FindBindingsToBeRemoved(x, noMoreValidBindings);

      foreach (Expression var in noMoreValidBindings)
      {
        if (!var.Equals(x))
        {
          this.var2exp[var] = cachedTopExpression;
        }
      }

      #endregion
    }

    #endregion

    #region ICloneable Members
    public object/*!*/ Clone()
    {
      FunctionalAbstractDomain<Expression, FlatAbstractDomainWithComparer<Expression>> clonedVar2exp = (FunctionalAbstractDomain<Expression, FlatAbstractDomainWithComparer<Expression>>)this.var2exp.Clone();

      return new SymbolicExpressionsAbstractDomain<Expression>(clonedVar2exp, this.decoder, this.encoder);
    }

    #endregion

    #region IPureExpressionAssignments Members

    /// <summary>
    /// Add the variable <code>var</code> and set its value to top
    /// </summary>
    public void AddVariable(Expression/*!*/ var)
    // ^ requires this.var2exp[var] == null;
    {
      Debug.Assert(!this.var2exp.ContainsKey(var));

      this.var2exp[var] = cachedTopExpression;
    }

    public void ProjectVariable(Expression/*!*/ var)
    // ^ requires this.decoder.IsVariable(var);
    // ^ requires this.var2exp[var] != null;
    {
      Debug.Assert(this.var2exp.ContainsKey(var));

      this.var2exp[var] = cachedTopExpression;
    }

    public void RemoveVariable(Expression/*!*/ var)
    // ^ requires this.var2exp.ContainsKey(var);
    {
      this.var2exp.Remove(var);

      ISet<Expression> toProject = new Set<Expression>();
      foreach (Expression v in this.var2exp.Keys)
      {
        FlatAbstractDomainWithComparer<Expression> liftedExp = this.var2exp[v];

        if (liftedExp.IsBottom || liftedExp.IsTop)
          continue;

        Expression exp = liftedExp.BoxedElement;

        if (this.decoder.VariablesIn(exp).Contains(var))
        {
          toProject.Add(v);
        }
      }

      foreach (Expression v in toProject)
      {
        this.ProjectVariable(v);
      }
    }

    /// <summary>
    /// Renames all the occurrencies of <code>oldName</code> with <code>newName</code>
    /// </summary>
    public void RenameVariable(Expression/*!*/ oldName, Expression/*!*/ newName)
    {
      if (!this.var2exp.ContainsKey(oldName))
        return;

      this.var2exp[newName] = this.var2exp[oldName];

      foreach (Expression var in this.var2exp.Keys)
      {
        if (!var.Equals(oldName))
        {
          FlatAbstractDomainWithComparer<Expression> element = this.var2exp[var];

          if (element.IsBottom || element.IsTop)
            continue;

          this.var2exp[var] = new FlatAbstractDomainWithComparer<Expression>(this.encoder.Substitute(element.BoxedElement, oldName, newName), this.decoder);
        }
      }

      this.var2exp.Remove(oldName);
    }

    #endregion

    #region IPureExpressionTest Members

    /// <summary>
    /// For the moment, we handle just the case with equality
    /// </summary>
    public IAbstractDomainForEnvironments<Expression>/*!*/ TestTrue(Expression/*!*/ guard)
    {
      switch (this.decoder.OperatorFor(guard))
      {
        case ExpressionOperator.Equal:
          return HelperForTestTrueEqual(guard);

        default:
          return this;
      }
    }

    /// <summary>
    /// We do nothing
    /// </summary>
    /// <param name="guard"></param>
    /// <returns>this</returns>
    public IAbstractDomainForEnvironments<Expression>/*!*/ TestFalse(Expression/*!*/ guard)
    {
      return this;
    }


    public FlatAbstractDomain<bool> CheckIfHolds(Expression exp)
    {
      throw new AbstractInterpretationException("For this domain, you were not supposed to call CheckIfHolds");
    }

    #endregion

    #region ISymbolicExpressionsAbstractDomain Members
    /// <summary>
    /// Get the (lifted) expression associated with the variable <code>x</code>
    /// </summary>
    public FlatAbstractDomain<Expression>/*!*/ ExpressionFor(Expression/*!*/ x)
    // ^ requires this.var2exp.ContainsKey(x);
    {
      Debug.Assert(this.var2exp.ContainsKey(x), "The variable " + x + " is not defined in this domain");
      return this.var2exp[x];
    }

    /// <summary>
    /// Substitutes all the variables in <code>tobeRefined</code> with the expressions we've (if they are different from top)
    /// </summary>
    public Expression/*!*/ Refine(Expression/*!*/ tobeRefined)
    {
      Expression refined = tobeRefined;
      foreach (Expression /* is variable */ x in this.decoder.VariablesIn(tobeRefined))
      {
        Debug.Assert(this.decoder.IsVariable(x)); 
        // ^ assert this.decoder.IsVariable(x);

        FlatAbstractDomain<Expression> expForX = this.var2exp[x];
        if (!expForX.IsTop)
        {
          if (expForX.IsBottom)
          {   // If it is bottom, we just return the input
            return tobeRefined;
          }
          else
          {
            // refined = refined.Substitute(x, expForX.BoxedElement);
            refined = this.encoder.Substitute(refined, x, expForX.BoxedElement);
          }
        }
      }

      return refined;
    }

    #endregion

    #region Overridden

    public string ToRewritingRule()
    {
      return null;
    }

    //^ [Confined]
    public override string/*!*/ ToString()
    {
      if (this.IsBottom)
      {
        return "_|_(se)";
      }
      else if (this.IsTop)
      {
        return "Top(se)";
      }
      else
      {
        StringBuilder str = new StringBuilder();
        foreach (Expression var in this.var2exp.Keys)
        {
          string asString =
              this.var2exp[var].IsBottom ? "_|_"
              : this.var2exp[var].IsTop ? "Top"
              : ExpressionPrinter.ToString(this.var2exp[var].BoxedElement, this.decoder);
          str.Append(var + " -> " + asString + ", ");
        }
        string s = str.ToString();
        s = s.Remove(s.LastIndexOf(","));

        return "[" + s + "]";
      }
    }

    #endregion

    #region Private methods

    /// <summary>
    /// The helper method to handle a guard that is in the form of <code>eq(e1, e2)</code>.
    /// 
    /// If e1 is a variable AND we have no information for it, we refine the value with e2
    /// If e2 is a variable AND we have no information for it, we refine the value with e1
    /// </summary>
    /// <param name="guard">We assume that it has already been refined at this point</param>
    private IAbstractDomainForEnvironments<Expression> HelperForTestTrueEqual(Expression guard)
    {
      Debug.Assert(this.decoder.OperatorFor(guard) == ExpressionOperator.Equal, "This helper method can only be called with equality, found instead: " + guard);

      Expression leftExpression = this.decoder.LeftExpressionFor(guard);
      Expression rightExpression = this.decoder.RightExpressionFor(guard);

      if (this.decoder.IsVariable(leftExpression) && this.ExpressionFor(leftExpression).IsTop)
      {   // This is correct as at this point we assume that we have already 
        this.var2exp[leftExpression] = new FlatAbstractDomainWithComparer<Expression>(rightExpression, this.decoder);
      }
      if (this.decoder.IsVariable(rightExpression) && this.ExpressionFor(rightExpression).IsTop)
      {   // This is correct as at this point we assume that we have already 
        this.var2exp[rightExpression] = new FlatAbstractDomainWithComparer<Expression>(leftExpression, this.decoder);
      }

      return this;
    }

    /// <summary>
    /// Initialize the static fields, if they are not already initialized
    /// </summary>
    private void InitStaticFields()
    {
      Debug.Assert(this.encoder != null && this.decoder != null, "At this point I already need a decoder and an encoder");

      // Check if we have already initialized all the maps
      if (cachedTopMap != null && cachedBottomExpression != null && cachedTopExpression != null)
        return;

      Expression dummyForTop = this.encoder.FreshVariable<int>();
      FlatAbstractDomainWithComparer<Expression> topVal = (FlatAbstractDomainWithComparer<Expression>)new FlatAbstractDomainWithComparer<Expression>(dummyForTop, null).Top;

      cachedTopMap = new SimpleFunctional<Expression, FlatAbstractDomainWithComparer<Expression>>();
      cachedTopMap.Add(dummyForTop, topVal);

      cachedBottomExpression = (FlatAbstractDomainWithComparer<Expression>)topVal.Bottom;
      cachedTopExpression = topVal;
    }

    /// <summary>
    /// Find the symbolic expressions containing <code>x</code> that will be set to Top.
    /// The variables corresponding to such expressions will also be propagated backwards;
    /// </summary>
    private void FindBindingsToBeRemoved(Expression x, ISet<Expression> alreadyVisited)
    {
      if (alreadyVisited.Contains(x))
        return;

      alreadyVisited.Add(x);

      foreach (Expression var in this.var2exp.Keys)
      {
        if (var.Equals(x))
          continue;
        else if (!this.var2exp[var].IsBottom && !this.var2exp[var].IsTop)
        {
          Expression tmpExp = this.var2exp[var].BoxedElement;
          if (this.decoder.VariablesIn(tmpExp).Contains(x))
          {
            FindBindingsToBeRemoved(var, alreadyVisited);
          }
        }
      }

    }

    /// <summary>
    /// Removes all the variables whom value depends on <code>varsToRemove</code>
    /// </summary>
    /// <param name="alreadyVisited">the variables we have already visited</param>
    private void ProjectVariablesTransitively(Expression varToRemove, ISet<Expression> alreadyVisited)
    {
      alreadyVisited.Add(varToRemove);

      ISet<Expression> continuation = new Set<Expression>();

      foreach (Expression v in this.var2exp.Keys)
      {
        FlatAbstractDomainWithComparer<Expression> liftedExp = this.var2exp[v];

        if (liftedExp.IsBottom || liftedExp.IsTop)
          continue;

        Expression exp = liftedExp.BoxedElement;

        if (this.decoder.VariablesIn(exp).Contains(varToRemove))
          continuation.Add(v);
      }

      foreach (Expression nextVarToRemove in continuation)
      {
        if (!alreadyVisited.Contains(nextVarToRemove))
          ProjectVariablesTransitively(nextVarToRemove, alreadyVisited);
      }

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