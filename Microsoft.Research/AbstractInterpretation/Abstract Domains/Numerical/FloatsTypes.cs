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
using Microsoft.Research.AbstractDomains.Expressions;
using Microsoft.Research.DataStructures;
using Microsoft.Research.CodeAnalysis;

namespace Microsoft.Research.AbstractDomains.Numerical
{

  public class FloatTypes<Variable, Expression>
    : FunctionalAbstractDomain<FloatTypes<Variable, Expression>, Variable, FlatAbstractDomain<ConcreteFloat>>,
      INumericalAbstractDomain<Variable, Expression>
  {
    public readonly static FlatAbstractDomain<ConcreteFloat> Unknown = new FlatAbstractDomain<ConcreteFloat>(ConcreteFloat.Float32).Top;

    private readonly ExpressionManager<Variable, Expression> expManager;

    public FloatTypes(ExpressionManager<Variable, Expression> expManager)
      : base()
    {
      this.expManager = expManager;
    }

    private FloatTypes(FloatTypes<Variable, Expression> other)
      : base(other)
    {
      this.expManager = other.expManager;
    }

    public override object Clone()
    {
      return new FloatTypes<Variable, Expression>(this);
    }

    protected override FloatTypes<Variable, Expression> Factory()
    {
      return new FloatTypes<Variable, Expression>(this.expManager);
    }

    protected override string ToLogicalFormula(Variable d, FlatAbstractDomain<ConcreteFloat> c)
    {
      return "";
    }

    protected override T To<T>(Variable d, FlatAbstractDomain<ConcreteFloat> c, IFactory<T> factory)
    {
      if (c.IsBottom)
        return factory.Constant(false);
      else
        return factory.IdentityForAnd;
    }

    #region INumericalAbstractDomain<Variable, Expression> Members

    public void Assign(Expression x, Expression exp, INumericalAbstractDomainQuery<Variable, Expression> preState)
    {
      // does nothing
    }

    public void AssumeInDisInterval(Variable x, DisInterval value)
    {
      // does nothing
    }

    public INumericalAbstractDomain<Variable, Expression> RemoveRedundanciesWith(INumericalAbstractDomain<Variable, Expression> oracle)
    {
      return this;
    }

    public INumericalAbstractDomain<Variable, Expression> TestTrueGeqZero(Expression exp)
    {
      return this;
    }

    public INumericalAbstractDomain<Variable, Expression> TestTrueLessThan(Expression exp1, Expression exp2)
    {
      return this;
    }

    public INumericalAbstractDomain<Variable, Expression> TestTrueLessEqualThan(Expression exp1, Expression exp2)
    {
      return this;
    }

    public INumericalAbstractDomain<Variable, Expression> TestTrueEqual(Expression exp1, Expression exp2)
    {
      return this;
    }

    #endregion

    #region IAbstractDomainForEnvironments<Variable, Expression> Members

    public string ToString(Expression exp)
    {
      throw new NotImplementedException();
    }

    #endregion

    #region IPureExpressionAssignments<Expression> Members

    public List<Variable> Variables
    {
      get { return new List<Variable>(this.Keys); }
    }

    public void AddVariable(Variable var)
    {
      // does nothing
    }

    public void Assign(Expression x, Expression exp)
    {
      // does nothing
    }

    public void ProjectVariable(Variable var)
    {
      if (this.ContainsKey(var))
        this.RemoveElement(var);
    }

    public void RemoveVariable(Variable var)
    {
      if (this.ContainsKey(var))
        this.RemoveElement(var);
    }

    public void RenameVariable(Variable OldName, Variable NewName)
    {
      if (this.ContainsKey(OldName))
      {
        this[NewName] = this[OldName];
        this.RemoveElement(OldName);
      }
    }

    #endregion

    #region IPureExpressionTest<Expression> Members

    public IAbstractDomainForEnvironments<Variable, Expression> TestTrue(Expression guard)
    {
      return this;
    }

    public IAbstractDomainForEnvironments<Variable, Expression> TestFalse(Expression guard)
    {
      return this;
    }

    public FlatAbstractDomain<bool> CheckIfHolds(Expression exp)
    {
      return CheckOutcome.Top;
    }

    void IPureExpressionTest<Variable, Expression>.AssumeDomainSpecificFact(DomainSpecificFact fact)
    {
      this.AssumeDomainSpecificFact(fact);
    }

    #endregion

    #region IAssignInParallel<Expression> Members

    public void AssignInParallel(Dictionary<Variable, FList<Variable>> sourcesToTargets, Converter<Variable, Expression> convert)
    {
      var tmp = this.Factory();

      this.State = AbstractState.Normal;

      if (sourcesToTargets.Count != 0)
      {
        foreach (var exp in sourcesToTargets.Keys)
        {
          FlatAbstractDomain<ConcreteFloat> value;
          if(this.TryGetValue(exp, out value) && !value.IsTop)
          {
            foreach (var target in sourcesToTargets[exp].GetEnumerable())
            {
              tmp[target] = value;
            }
          }
        }

        this.CopyAndTransferOwnership(tmp);
      }
    }

    #endregion

    #region INumericalAbstractDomainQuery<Variable, Expression> Members

    public DisInterval BoundsFor(Variable v)
    {
      return DisInterval.UnknownInterval;
    }

    public IEnumerable<Expression> LowerBoundsFor(Variable v, bool strict)
    {
      return new Set<Expression>();
    }

    public IEnumerable<Expression> UpperBoundsFor(Variable v, bool strict)
    {
      return new Set<Expression>();
    }

    public IEnumerable<Variable> EqualitiesFor(Variable v)
    {
      return new Set<Variable>();
    }

    public FlatAbstractDomain<bool> CheckIfGreaterEqualThanZero(Variable exp)
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

    public FlatAbstractDomain<bool> CheckIfNonZero(Variable e)
    {
      return CheckOutcome.Top;
    }

    public DisInterval BoundsFor(Expression v)
    {
      return DisInterval.UnknownInterval;
    }

    public List<Pair<Variable, Int32>> IntConstants
    {
      get
      {
        return new List<Pair<Variable, Int32>>();
      }
    }

    public IEnumerable<Expression> LowerBoundsFor(Expression v, bool strict)
    {
      yield break;
    }

    public IEnumerable<Expression> UpperBoundsFor(Expression v, bool strict)
    {
      yield break;
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

    public FlatAbstractDomain<bool> CheckIfLessEqualThan(Expression e1, Expression e2)
    {
      return CheckOutcome.Top;
    }

    public FlatAbstractDomain<bool> CheckIfLessThan_Un(Expression e1, Expression e2)
    {
      return CheckOutcome.Top;
    }

    public FlatAbstractDomain<bool> CheckIfLessEqualThan_Un(Expression e1, Expression e2)
    {
      return CheckOutcome.Top;
    }

    public FlatAbstractDomain<bool> CheckIfEqual(Expression e1, Expression e2)
    {
      return CheckOutcome.Top;
    }

    public FlatAbstractDomain<bool> CheckIfEqualIncomplete(Expression e1, Expression e2)
    {
      return CheckOutcome.Top;
    }

    public FlatAbstractDomain<bool> CheckIfNonZero(Expression e)
    {
      return CheckOutcome.Top;
    }

    public Variable ToVariable(Expression exp)
    {
      return this.expManager.Decoder.UnderlyingVariable(exp);
    }

    #endregion

    #region INumericalAbstractDomain<Variable, Expression> Members


    public void SetFloatType(Variable v, ConcreteFloat f)
    {
      this[v] = new FlatAbstractDomain<ConcreteFloat>(f);
    }

    public FlatAbstractDomain<ConcreteFloat> GetFloatType(Variable v)
    {
      FlatAbstractDomain<ConcreteFloat> value;
      if (this.TryGetValue(v, out value))
      {
        return value;
      }

      return Unknown;
    }

    #endregion
  }
}
