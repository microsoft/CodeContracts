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

namespace Microsoft.Research.AbstractDomains.Numerical
{
  // **** Important *****
  // We have a bunch of invariants in this class, which cannot proven by C# type system but Clousot can do.
  // As a consequence we always want to have 0 warnings on this class
  [ContractVerification(true)]
  public class NonRelationalValueAbstraction<Variable, Expression>
    : IAbstractDomainForArraySegmentationAbstraction<NonRelationalValueAbstraction<Variable, Expression>, Variable>
  {
    #region Object invariant
    [ContractInvariantMethod]
    void ObjectInvariant()
    {
      Contract.Invariant(this.disInterval != null);
      Contract.Invariant(this.symbolicConditions != null);
      Contract.Invariant(weaklyRelationalDomains != null);
      Contract.Invariant(weaklyRelationalDomains.Length == WeaklyRelationalDomainsCount);
      Contract.Invariant(Contract.ForAll(weaklyRelationalDomains, dom => dom != null));
    }
    #endregion

    #region State

    private readonly DisInterval disInterval;
    private readonly SymbolicExpressionTracker<Variable, Expression> symbolicConditions;

    private const int WeaklyRelationalDomainsCount = 5;
    private readonly SetOfConstraints<Variable>[] weaklyRelationalDomains;

    public enum ADomains { Equalities = 0, DisEqualities = 1, WeakUpperBounds = 2, StrictUpperBounds = 3, Existential = 4 }

    // Meaning:
    // weaklyRelationalDomains[0] == Equalities
    // weaklyRelationalDomains[1] == DisEqualities
    // weaklyRelationalDomains[2] == WeakUpperBounds
    // weaklyRelationalDomains[3] == StrictUpperBounds 
    // weaklyRelationalDomains[4] == Existential

    #endregion

    #region Constructor

    public NonRelationalValueAbstraction(DisInterval interval)
      : this(interval, SymbolicExpressionTracker<Variable, Expression>.Unknown,
      SetOfConstraints<Variable>.Unknown, SetOfConstraints<Variable>.Unknown,
      SetOfConstraints<Variable>.Unknown,
      SetOfConstraints<Variable>.Unknown, SetOfConstraints<Variable>.Unknown)
    {
      Contract.Requires(interval != null);
    }

    public NonRelationalValueAbstraction(
      DisInterval interval, SymbolicExpressionTracker<Variable, Expression> symbolicConditions,
      SetOfConstraints<Variable> equalities, SetOfConstraints<Variable> disequalities,
      SetOfConstraints<Variable> weakUpperBounds, SetOfConstraints<Variable> strictUpperBounds,
      SetOfConstraints<Variable> existential)
    {
      Contract.Requires(interval != null);
      Contract.Requires(symbolicConditions != null);
      Contract.Requires(disequalities != null);
      Contract.Requires(equalities != null);
      Contract.Requires(weakUpperBounds != null);
      Contract.Requires(strictUpperBounds != null);
      Contract.Requires(existential != null);

      this.disInterval = interval;
      this.symbolicConditions = symbolicConditions;
      this.weaklyRelationalDomains = new SetOfConstraints<Variable>[WeaklyRelationalDomainsCount]
      {
        equalities, 
        disequalities,
        weakUpperBounds, 
        strictUpperBounds,
        existential
      };
    }

    private NonRelationalValueAbstraction(
      DisInterval interval, SymbolicExpressionTracker<Variable, Expression> symbolicConditions,
      SetOfConstraints<Variable>[] domains)
    {
      Contract.Requires(interval != null);
      Contract.Requires(symbolicConditions != null);
      Contract.Requires(domains != null);

      Contract.Requires(domains.Length == WeaklyRelationalDomainsCount);
      Contract.Requires(Contract.ForAll(domains, el => el != null));

      this.disInterval = interval;
      this.symbolicConditions = symbolicConditions;
      this.weaklyRelationalDomains = domains;
    }

    #endregion

    #region Statics
    static public NonRelationalValueAbstraction<Variable, Expression> Unknown
    {
      get
      {
        Contract.Ensures(Contract.Result<NonRelationalValueAbstraction<Variable, Expression>>() != null);
        var allTop = new SetOfConstraints<Variable>[WeaklyRelationalDomainsCount];

        for (var i = 0; i < WeaklyRelationalDomainsCount; i++)
        {
          allTop[i] = SetOfConstraints<Variable>.Unknown;
        }

        return new NonRelationalValueAbstraction<Variable, Expression>(DisInterval.UnknownInterval, SymbolicExpressionTracker<Variable, Expression>.Unknown, allTop);
      }
    }

    static public NonRelationalValueAbstraction<Variable, Expression> Unreached
    {
      get
      {
        Contract.Ensures(Contract.Result<NonRelationalValueAbstraction<Variable, Expression>>() != null);

        var allBottom = new SetOfConstraints<Variable>[WeaklyRelationalDomainsCount];

        for (var i = 0; i < WeaklyRelationalDomainsCount; i++)
        {
          allBottom[i] = SetOfConstraints<Variable>.Unreached;
        }

        return new NonRelationalValueAbstraction<Variable, Expression>(DisInterval.UnreachedInterval, SymbolicExpressionTracker<Variable, Expression>.Unreached, allBottom);
      }
    }

    #endregion

    #region Properties
    public DisInterval Interval
    {
      get
      {
        Contract.Ensures(Contract.Result<DisInterval>() != null);

        return this.disInterval;
      }
    }

    public SetOfConstraints<Variable> Equalities
    {
      get
      {
        Contract.Ensures(Contract.Result<SetOfConstraints<Variable>>() != null);

        return this.weaklyRelationalDomains[(int)ADomains.Equalities];
      }
    }

    public SetOfConstraints<Variable> DisEqualities
    {
      get
      {
        Contract.Ensures(Contract.Result<SetOfConstraints<Variable>>() != null);

        return this.weaklyRelationalDomains[(int)ADomains.DisEqualities];
      }
    }

    public SetOfConstraints<Variable> StrictUpperBounds
    {
      get
      {
        Contract.Ensures(Contract.Result<SetOfConstraints<Variable>>() != null);

        return this.weaklyRelationalDomains[(int)ADomains.StrictUpperBounds];
      }
    }

    public SetOfConstraints<Variable> WeakUpperBounds
    {
      get
      {
        Contract.Ensures(Contract.Result<SetOfConstraints<Variable>>() != null);

        return this.weaklyRelationalDomains[(int)ADomains.WeakUpperBounds];
      }
    }

    public SetOfConstraints<Variable> Existential
    {
      get
      {
        Contract.Ensures(Contract.Result<SetOfConstraints<Variable>>() != null);

        return this.weaklyRelationalDomains[(int)ADomains.Existential];
      }
    }

    public SymbolicExpressionTracker<Variable, Expression> SymbolicConditions
    {
      get
      {
        Contract.Ensures(Contract.Result<SymbolicExpressionTracker<Variable, Expression>>() != null);

        return this.symbolicConditions;
      }
    }

    #endregion

    #region Check For contraddictions

    /// <summary>
    /// Check for contraddictions.
    /// The understanding here is that:
    ///   - 'this' is interpreted as existential and 'exists' as universal
    ///   - 'that' is interpreted as universal and 'exists' as existential
    /// </summary>
    public bool AreInContraddiction(NonRelationalValueAbstraction<Variable, Expression> other)
    {
      Contract.Requires(other != null);

      if (this.Interval.Meet(other.Interval).IsBottom)
        return true;

      if (!this.DisEqualities.Join(other.Equalities).IsTop)
        return true;

      if (!this.Equalities.Join(other.DisEqualities).IsTop)
        return true;

      if (!this.Equalities.Join(other.StrictUpperBounds).IsTop)
        return true;

      if (!this.StrictUpperBounds.Join(other.Equalities).IsTop)
        return true;

      return false;
    }

    #endregion

    #region Assumptions

    public NonRelationalValueAbstraction<Variable, Expression> TestTrueEqual(Variable v)
    {
      if (this.DisEqualities.Contains(v) || this.StrictUpperBounds.Contains(v))
      {
        return this.Bottom;
      }

      var equalities = this.Equalities.Add(v);
      var weakupperbounds = this.WeakUpperBounds.Add(v);

      return new NonRelationalValueAbstraction<Variable, Expression>(
        this.Interval, this.SymbolicConditions,
        equalities, // updated
        this.DisEqualities,
        weakupperbounds,  // updated
        this.StrictUpperBounds, this.Existential);
    }

    #endregion

    #region IAbstractDomain Members

    public bool IsBottom
    {
      get
      {
        return this.disInterval.IsBottom || this.symbolicConditions.IsBottom || this.weaklyRelationalDomains.Any(x => x.IsBottom);
      }
    }

    public bool IsTop
    {
      get
      {
        return this.disInterval.IsTop && this.symbolicConditions.IsTop && this.weaklyRelationalDomains.All(x => x.IsTop);
      }
    }

    public bool LessEqual(NonRelationalValueAbstraction<Variable, Expression> other)
    {
      Contract.Requires(other != null);

      if (!this.disInterval.LessEqual(other.Interval))
      {
        return false;
      }
      Contract.Assert(other.symbolicConditions != null);
      if (!this.symbolicConditions.LessEqual(other.symbolicConditions))
      {
        return false;
      }

      // F: Assuming the object invariant for other
      Contract.Assert(other.weaklyRelationalDomains != null);
      Contract.Assume(Contract.ForAll(other.weaklyRelationalDomains, dom => dom != null));
      Contract.Assume(this.weaklyRelationalDomains.Length == other.weaklyRelationalDomains.Length, "assuming object invariant");
      for (var i = 0; i < this.weaklyRelationalDomains.Length; i++)
      {
        if (!this.weaklyRelationalDomains[i].LessEqual(other.weaklyRelationalDomains[i]))
          return false;
      }

      return true;
    }

    public NonRelationalValueAbstraction<Variable, Expression> Bottom
    {
      get
      {
        Contract.Ensures(Contract.Result<NonRelationalValueAbstraction<Variable, Expression>>() != null);

        var allBottom = new SetOfConstraints<Variable>[WeaklyRelationalDomainsCount];

        for (var i = 0; i < WeaklyRelationalDomainsCount; i++)
        {
          allBottom[i] = SetOfConstraints<Variable>.Unreached;
        }

        return new NonRelationalValueAbstraction<Variable, Expression>(DisInterval.UnreachedInterval, SymbolicExpressionTracker<Variable, Expression>.Unreached, allBottom);
      }
    }

    public NonRelationalValueAbstraction<Variable, Expression> Top
    {
      get
      {
        Contract.Ensures(Contract.Result<NonRelationalValueAbstraction<Variable, Expression>>() != null);

        var allTop = new SetOfConstraints<Variable>[WeaklyRelationalDomainsCount];

        for (var i = 0; i < WeaklyRelationalDomainsCount; i++)
        {
          allTop[i] = SetOfConstraints<Variable>.Unknown;
        }

        return new NonRelationalValueAbstraction<Variable, Expression>(DisInterval.UnknownInterval, SymbolicExpressionTracker<Variable, Expression>.Unknown, allTop);
      }
    }

    [Pure]
    public NonRelationalValueAbstraction<Variable, Expression> Join(NonRelationalValueAbstraction<Variable, Expression> other)
    {
      Contract.Requires(other != null);
      Contract.Ensures(Contract.Result<NonRelationalValueAbstraction<Variable, Expression>>() != null);

      NonRelationalValueAbstraction<Variable, Expression> result;
      if (AbstractDomainsHelper.TryTrivialJoin(this, other, out result))
      {
        return result;
      }

      var intv = this.disInterval.Join(other.Interval);
      var symbolicConditions = this.symbolicConditions.Join(other.symbolicConditions);

      Contract.Assume(this.weaklyRelationalDomains.Length == other.weaklyRelationalDomains.Length);

      var newWeaklyDomains = ParallelOperation(this.weaklyRelationalDomains, other.weaklyRelationalDomains, (x, y) => x.Join(y));

      return new NonRelationalValueAbstraction<Variable, Expression>(intv, symbolicConditions, newWeaklyDomains);
    }

    [Pure]
    public NonRelationalValueAbstraction<Variable, Expression> Meet(NonRelationalValueAbstraction<Variable, Expression> other)
    {
      Contract.Requires(other != null);
      Contract.Ensures(Contract.Result<NonRelationalValueAbstraction<Variable, Expression>>() != null);

      NonRelationalValueAbstraction<Variable, Expression> result;
      if (AbstractDomainsHelper.TryTrivialMeet(this, other, out result))
      {
        return result;
      }

      var intv = this.disInterval.Meet(other.Interval);

      Contract.Assert(other.symbolicConditions != null);
      var symbolicConditions = this.symbolicConditions.Meet(other.symbolicConditions);

      Contract.Assert(other.weaklyRelationalDomains != null);
      Contract.Assume(this.weaklyRelationalDomains.Length == other.weaklyRelationalDomains.Length);

      var newWeaklyDomains = ParallelOperation(this.weaklyRelationalDomains, other.weaklyRelationalDomains, (x, y) => x.Meet(y));

      return new NonRelationalValueAbstraction<Variable, Expression>(intv, symbolicConditions, newWeaklyDomains);
    }

    [Pure]
    public NonRelationalValueAbstraction<Variable, Expression> Widening(NonRelationalValueAbstraction<Variable, Expression> prev)
    {
      Contract.Requires(prev != null);
      Contract.Ensures(Contract.Result<NonRelationalValueAbstraction<Variable, Expression>>() != null);

      NonRelationalValueAbstraction<Variable, Expression> result;
      if (AbstractDomainsHelper.TryTrivialJoin(this, prev, out result))
      {
        return result;
      }

      var intv = this.disInterval.Widening(prev.Interval);
      var symbolicConditions = this.symbolicConditions.Join(prev.symbolicConditions);

      Contract.Assume(this.weaklyRelationalDomains.Length == prev.weaklyRelationalDomains.Length);

      var newWeaklyDomains = ParallelOperation(this.weaklyRelationalDomains, prev.weaklyRelationalDomains, (x, y) => x.Widening(y));

      return new NonRelationalValueAbstraction<Variable, Expression>(intv, SymbolicConditions, newWeaklyDomains);
    }

    #endregion

    #region To

    public T To<T>(IFactory<T> factory)
    {
      var result = factory.And(this.disInterval.To(factory), this.symbolicConditions.To(factory));

      T name;

      if (factory.TryGetName(out name))
      {
        for (var i = 0; i < this.weaklyRelationalDomains.Length; i++)
        {
          if (this.weaklyRelationalDomains[i].IsNormal())
          {
            result = factory.And(result, To(i, factory, name));
          }
        }
      }
      return result;
    }

    private T To<T>(int i, IFactory<T> factory, T name)
    {
      Contract.Requires(i >= 0);
      Contract.Requires(i < this.weaklyRelationalDomains.Length);
      Contract.Requires(factory != null);

      var result = factory.IdentityForAnd;

      var constraints = this.weaklyRelationalDomains[i];

      switch (i)
      {
        // weaklyRelationalDomains[0] == Equalities
        case 0:
          return MakeFact<T>(name, result, constraints.Values, factory, factory.EqualTo);

        // weaklyRelationalDomains[1] == DisEqualities
        case 1:
          return MakeFact<T>(name, result, constraints.Values, factory, factory.NotEqualTo);

        // weaklyRelationalDomains[2] == WeakUpperBounds
        case 2:
          return MakeFact<T>(name, result, constraints.Values, factory, factory.LessEqualThan);

        // weaklyRelationalDomains[3] == StrictUpperBounds 
        case 3:
          return MakeFact<T>(name, result, constraints.Values, factory, factory.LessThan);

        // weaklyRelationalDomains[4] == Existential
        case 4:
          return result;

        default:
          Contract.Assert(false);
          return factory.IdentityForAnd;
      }
    }

    private static T MakeFact<T>(T name, T result, IEnumerable<Variable> constraints,
      IFactory<T> factory, Func<T, T, T> op)
    {
      Contract.Requires(constraints != null);
      Contract.Requires(factory != null);
      Contract.Requires(op != null);

      foreach (var x in constraints)
      {
        result = factory.And(result, op(name, factory.Variable(x)));
      }
      return result;
    }


    #endregion

    #region ICloneable Members

    public object Clone()
    {
      return this.DuplicateMe();
    }

    public NonRelationalValueAbstraction<Variable, Expression> DuplicateMe()
    {
      Contract.Ensures(Contract.Result<NonRelationalValueAbstraction<Variable, Expression>>() != null);

      var newArr = new SetOfConstraints<Variable>[this.weaklyRelationalDomains.Length];
      Array.Copy(this.weaklyRelationalDomains, newArr, this.weaklyRelationalDomains.Length);
      return new NonRelationalValueAbstraction<Variable, Expression>(this.disInterval, this.symbolicConditions, newArr);
    }

    #endregion

    #region To Please the compiler
    bool IAbstractDomain.LessEqual(IAbstractDomain a)
    {
      var other = a as NonRelationalValueAbstraction<Variable, Expression>;
      Contract.Assume(other != null);

      return this.LessEqual(other);
    }

    IAbstractDomain IAbstractDomain.Bottom
    {
      get { return this.Bottom; }
    }

    IAbstractDomain IAbstractDomain.Top
    {
      get { return this.Top; }
    }

    IAbstractDomain IAbstractDomain.Join(IAbstractDomain a)
    {
      var other = a as NonRelationalValueAbstraction<Variable, Expression>;
      Contract.Assume(other != null);

      return this.Join(other);
    }

    IAbstractDomain IAbstractDomain.Meet(IAbstractDomain a)
    {
      var other = a as NonRelationalValueAbstraction<Variable, Expression>;
      Contract.Assume(other != null);

      return this.Meet(other);
    }

    IAbstractDomain IAbstractDomain.Widening(IAbstractDomain prev)
    {
      var other = prev as NonRelationalValueAbstraction<Variable, Expression>;
      Contract.Assume(other != null);

      return this.Widening(other);
    }

    #endregion

    #region ToString
    public override string ToString()
    {
      if (this.IsTop)
      {
        return "Top";
      }
      if (this.IsBottom)
      {
        return "_|_";
      }

      var result = this.disInterval.ToString();

      result += this.symbolicConditions.IsTop ? "" : string.Format("({0})", this.symbolicConditions.ToString());

      for (var i = 0; i < this.weaklyRelationalDomains.Length; i++)
      {
        var dom = this.weaklyRelationalDomains[i];
        if (!dom.IsTop)
        {
          result = result + " " + OperatorFor((ADomains)i) + "{" + dom.ToString() + "}";
        }
      }

      return result;
    }
    #endregion

    #region IAbstractDomainWithRenaming<NonRelationalValueAbstraction<Variable,Expression>,Variable> Members

    [Pure]
    public NonRelationalValueAbstraction<Variable, Expression> Rename(Dictionary<Variable, FList<Variable>> renaming)
    {
      // Rename the symbolic conditions
      var renamedExpressions = this.symbolicConditions.Rename(renaming);

      // Rename the set of constraints
      var renamedConstraints = new SetOfConstraints<Variable>[weaklyRelationalDomains.Length];
      for (var i = 0; i < this.weaklyRelationalDomains.Length; i++)
      {
        var dom = this.weaklyRelationalDomains[i];
        if (dom.IsNormal())
        {
          var renamedStrict = new Set<Variable>();

          foreach (var prev in dom.Values)
          {
            FList<Variable> targets;
            if (renaming.TryGetValue(prev, out targets))
            {
              foreach (var next in targets.GetEnumerable())
              {
                renamedStrict.Add(next);
              }
            }
          }
          renamedConstraints[i] = new SetOfConstraints<Variable>(renamedStrict, false);
        }
        else
        {
          renamedConstraints[i] = dom;
        }
      }

      return new NonRelationalValueAbstraction<Variable, Expression>(this.Interval, renamedExpressions, renamedConstraints);
    }

    #endregion

    #region Helper functions
    [Pure]
    static private SetOfConstraints<Variable>[] ParallelOperation(
      SetOfConstraints<Variable>[] left, SetOfConstraints<Variable>[] right,
      Func<SetOfConstraints<Variable>, SetOfConstraints<Variable>, SetOfConstraints<Variable>> op)
    {
      Contract.Requires(left != null);
      Contract.Requires(right != null);
      Contract.Requires(left.Length == right.Length);
      Contract.Requires(op != null);

      Contract.Ensures(Contract.Result<SetOfConstraints<Variable>[]>() != null);
      Contract.Ensures(Contract.Result<SetOfConstraints<Variable>[]>().Length == left.Length);
      Contract.Ensures(
        Contract.ForAll(Contract.Result<SetOfConstraints<Variable>[]>(), el => el != null));

      var result = new SetOfConstraints<Variable>[left.Length];

      for (var i = 0; i < left.Length; i++)
      {
        var r = op(left[i], right[i]);
        Contract.Assume(r != null);

        result[i] = r;
      }

      return result;
    }

    [Pure]
    [ContractVerification(true)]
    private static string OperatorFor(ADomains index)
    {
      Contract.Ensures(Contract.Result<string>() != null);

      Contract.Assert(index >= 0);
      Contract.Assert((int)index < WeaklyRelationalDomainsCount);

      switch (index)
      {
        case ADomains.Equalities:
          return "=";
        case ADomains.DisEqualities:
          return "!=";
        case ADomains.StrictUpperBounds:
          return "<";
        case ADomains.WeakUpperBounds:
          return "<=";
        case ADomains.Existential:
          return "E";
        default:
          {
            Contract.Assert(false); // should be unreachable
            return "";
          }
      }
    }
    #endregion

    #region IAbstractDomainForArraySegmentationAbstraction<NonRelationalValueAbstraction<Variable,Expression>,Variable> Members

    /// <summary>
    /// We only add less equals
    /// </summary>
    public NonRelationalValueAbstraction<Variable, Expression> AssumeInformationFrom<Exp>(INumericalAbstractDomainQuery<Variable, Exp> oracle)
    {
      var result = this;

      if (this.IsNormal())
      {
        #region Update <
        if (this.StrictUpperBounds.IsNormal())
        {
          var newConstraints = new Set<Variable>(this.StrictUpperBounds.Values);

          foreach (var x in this.StrictUpperBounds.Values)
          {
            // Add S such that x <= S
            var newBounds = oracle.UpperBoundsFor(x, true).ApplyToAll(oracle.ToVariable);
            Contract.Assert(newBounds != null);
            newConstraints.AddRange(newBounds);
          }

          result = result.Update(ADomains.StrictUpperBounds, new SetOfConstraints<Variable>(newConstraints, false));
        }
        #endregion

        #region Update <=
        if (this.WeakUpperBounds.IsNormal())
        {
          var newConstraints = new Set<Variable>(this.WeakUpperBounds.Values);

          foreach (var x in this.WeakUpperBounds.Values)
          {
            // Add S such that x <= S
            var newBounds = oracle.UpperBoundsFor(x, false).ApplyToAll(oracle.ToVariable);
            Contract.Assert(newBounds != null);
            newConstraints.AddRange(newBounds);
          }

          result = result.Update(ADomains.WeakUpperBounds, new SetOfConstraints<Variable>(newConstraints, false));
        }
        #endregion
      }

      return result;
    }

    #endregion

    #region Update

    public NonRelationalValueAbstraction<Variable, Expression> UpdateDisInterval(DisInterval disIntv)
    {
      #region Contracts

      Contract.Requires(disIntv != null);

      Contract.Ensures(Contract.Result<NonRelationalValueAbstraction<Variable, Expression>>() != null);

      #endregion

      return new NonRelationalValueAbstraction<Variable, Expression>(
        disIntv,
        this.symbolicConditions, this.weaklyRelationalDomains);
    }

    public NonRelationalValueAbstraction<Variable, Expression> UpdateSymbolicCondition(SymbolicExpressionTracker<Variable, Expression> sc)
    {
      #region Contracts

      Contract.Requires(sc != null);

      Contract.Ensures(Contract.Result<NonRelationalValueAbstraction<Variable, Expression>>() != null);

      #endregion

      return new NonRelationalValueAbstraction<Variable, Expression>(this.disInterval,
        sc,
        this.weaklyRelationalDomains);
    }

    public NonRelationalValueAbstraction<Variable, Expression> Update(ADomains what, SetOfConstraints<Variable> value)
    {
      #region Contracts

      Contract.Requires(value != null);
      Contract.Ensures(Contract.Result<NonRelationalValueAbstraction<Variable, Expression>>() != null);

      Contract.Assert(Enum.IsDefined(typeof(ADomains), what));

      #endregion

      var copy = new SetOfConstraints<Variable>[this.weaklyRelationalDomains.Length];
      Array.Copy(this.weaklyRelationalDomains, copy, this.weaklyRelationalDomains.Length);

      copy[(int)what] = value;

      return new NonRelationalValueAbstraction<Variable, Expression>(this.disInterval, this.symbolicConditions, copy);
    }

    #endregion
  }

  public class SymbolicExpressionTracker<Variable, Expression>
    : IAbstractDomainWithRenaming<SymbolicExpressionTracker<Variable, Expression>, Variable>
  {
    #region Object invariant

    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(this.symbolicConditions != null);
    }

    #endregion

    #region State

    readonly Variable slackVar;
    readonly SetOfConstraints<Expression> symbolicConditions;

    public delegate Expression Renaming(Expression exp, Dictionary<Variable, FList<Variable>> renaming);
    public delegate Expression ReplaceVariable(Expression exp, Variable oldVar, Variable newVar);

    // It can be set only once
    [ThreadStatic]
    private static Renaming expressionRenamer;
    public static Renaming ExpressionRenamer
    {
      set
      {
        Contract.Requires(value != null);

        expressionRenamer = value;
      }
    }

    // Removed the ThreadStatic as this should run be set only once, and we run some code below in parallel, which will cause a to have a fresh, thread-local, value for variableReplacer 
    //[ThreadStatic] 
    private static ReplaceVariable variableReplacer;
    public static ReplaceVariable VariableReplacer
    {
      set
      {
        Contract.Requires(value != null);

        variableReplacer = value;
      }
    }


    #endregion

    #region Constructor

    public SymbolicExpressionTracker(Variable slackVar, SetOfConstraints<Expression> symbolicConditions)
    {
      Contract.Requires(symbolicConditions != null);

      this.slackVar = slackVar;
      this.symbolicConditions = symbolicConditions;
    }

    public SymbolicExpressionTracker(Variable slackVar, Expression exp)
      : this(slackVar, new SetOfConstraints<Expression>(exp))
    {
    }

    #endregion

    #region Statics

    public static SymbolicExpressionTracker<Variable, Expression> Unknown
    {
      get
      {
        Contract.Ensures(Contract.Result<SymbolicExpressionTracker<Variable, Expression>>() != null);

        return new SymbolicExpressionTracker<Variable, Expression>(default(Variable), SetOfConstraints<Expression>.Unknown);
      }
    }

    public static SymbolicExpressionTracker<Variable, Expression> Unreached
    {
      get
      {
        Contract.Ensures(Contract.Result<SymbolicExpressionTracker<Variable, Expression>>() != null);

        return new SymbolicExpressionTracker<Variable, Expression>(default(Variable), SetOfConstraints<Expression>.Unreached);
      }
    }

    #endregion

    #region Getters

    public Variable SlackVariable
    {
      get
      {
        return this.slackVar;
      }
    }

    public SetOfConstraints<Expression> Conditions
    {
      get
      {
        Contract.Ensures(Contract.Result<SetOfConstraints<Expression>>() != null);

        return this.symbolicConditions;
      }
    }

    #endregion

    #region IAbstractDomainWithRenaming<SymbolicExpressionTracker<Variable,Expression>,Variable> Members

    public SymbolicExpressionTracker<Variable, Expression> Rename(Dictionary<Variable, FList<Variable>> renaming)
    {
      return RenameInternal(renaming, this.slackVar);
    }

    private SymbolicExpressionTracker<Variable, Expression> RenameInternal(Dictionary<Variable, FList<Variable>> renaming, Variable slackVar)
    {
      Contract.Requires(renaming != null);
      Contract.Ensures(Contract.Result<SymbolicExpressionTracker<Variable, Expression>>() != null);
      Contract.Ensures(renaming.Count == Contract.OldValue(renaming.Count));

      if (this.symbolicConditions.IsNormal())
      {
        renaming[slackVar] = FList<Variable>.Cons(slackVar, FList<Variable>.Empty);

        Set<Expression> result = new Set<Expression>();

        foreach (var exp in this.symbolicConditions.Values)
        {
          result.AddIfNotNull(expressionRenamer(exp, renaming));
        }
        var renamedExpressions =
          result.Count != 0 ?
          new SetOfConstraints<Expression>(result, false) :
          SetOfConstraints<Expression>.Unknown;

        renaming.Remove(slackVar);  // We should remove the slack variable!

        return new SymbolicExpressionTracker<Variable, Expression>(this.slackVar, symbolicConditions);
      }
      else
      {
        return this;
      }
    }

    #endregion

    #region Abstract operations
    public bool LessEqual(SymbolicExpressionTracker<Variable, Expression> other)
    {
      Contract.Requires(other != null);

      bool result;
      if (AbstractDomainsHelper.TryTrivialLessEqual(this, other, out result))
      {
        return result;
      }

      other = this.UnifySlackVariableWith(other);

      return this.symbolicConditions.LessEqual(other.symbolicConditions);
    }

    public SymbolicExpressionTracker<Variable, Expression> Bottom
    {
      get { return new SymbolicExpressionTracker<Variable, Expression>(default(Variable), SetOfConstraints<Expression>.Unreached); }
    }

    public SymbolicExpressionTracker<Variable, Expression> Top
    {
      get { return new SymbolicExpressionTracker<Variable, Expression>(default(Variable), SetOfConstraints<Expression>.Unknown); }
    }

    public SymbolicExpressionTracker<Variable, Expression> Join(SymbolicExpressionTracker<Variable, Expression> other)
    {
      Contract.Requires(other != null);
      Contract.Ensures(Contract.Result<SymbolicExpressionTracker<Variable, Expression>>() != null);

      SymbolicExpressionTracker<Variable, Expression> result;
      if (AbstractDomainsHelper.TryTrivialJoin(this, other, out result))
      {
        return result;
      }

      other = UnifySlackVariableWith(other);

      return new SymbolicExpressionTracker<Variable, Expression>(this.slackVar, this.symbolicConditions.Join(other.symbolicConditions));
    }

    public SymbolicExpressionTracker<Variable, Expression> Meet(SymbolicExpressionTracker<Variable, Expression> other)
    {
      Contract.Requires(other != null);
      Contract.Ensures(Contract.Result<SymbolicExpressionTracker<Variable, Expression>>() != null);

      SymbolicExpressionTracker<Variable, Expression> result;
      if (AbstractDomainsHelper.TryTrivialMeet(this, other, out result))
      {
        return result;
      }

      other = UnifySlackVariableWith(other);

      return new SymbolicExpressionTracker<Variable, Expression>(this.slackVar, this.symbolicConditions.Meet(other.symbolicConditions));

    }

    public SymbolicExpressionTracker<Variable, Expression> Widening(SymbolicExpressionTracker<Variable, Expression> other)
    {
      Contract.Requires(other != null);
      Contract.Ensures(Contract.Result<SymbolicExpressionTracker<Variable, Expression>>() != null);

      return this.Join(other);
    }

    #endregion

    #region IAbstractDomain Members

    public bool IsBottom
    {
      get { return this.symbolicConditions.IsBottom; }
    }

    public bool IsTop
    {
      get { return this.symbolicConditions.IsTop; }
    }

    bool IAbstractDomain.LessEqual(IAbstractDomain a)
    {
      var other = a as SymbolicExpressionTracker<Variable, Expression>;
      Contract.Assume(a != null);

      return this.LessEqual(other);
    }

    IAbstractDomain IAbstractDomain.Bottom
    {
      get { return this.Bottom; }
    }

    IAbstractDomain IAbstractDomain.Top
    {
      get { return this.Top; }
    }

    IAbstractDomain IAbstractDomain.Join(IAbstractDomain a)
    {
      var other = a as SymbolicExpressionTracker<Variable, Expression>;
      Contract.Assume(a != null);

      return this.Join(other);
    }

    IAbstractDomain IAbstractDomain.Meet(IAbstractDomain a)
    {
      var other = a as SymbolicExpressionTracker<Variable, Expression>;
      Contract.Assume(a != null);

      return this.Meet(other);
    }

    IAbstractDomain IAbstractDomain.Widening(IAbstractDomain prev)
    {
      var other = prev as SymbolicExpressionTracker<Variable, Expression>;
      Contract.Assume(prev != null);

      return this.Widening(other);
    }

    public T To<T>(IFactory<T> factory)
    {
      return this.symbolicConditions.To(factory);
    }

    #endregion

    #region ICloneable Members

    object ICloneable.Clone()
    {
      return this;
    }

    #endregion

    #region Utils

    private SymbolicExpressionTracker<Variable, Expression> UnifySlackVariableWith(SymbolicExpressionTracker<Variable, Expression> other)
    {
      Contract.Requires(other != null);
      Contract.Ensures(Contract.Result<SymbolicExpressionTracker<Variable, Expression>>() != null);

      if (!other.IsNormal() || object.Equals(other.slackVar, this.slackVar))
      {
        return other;
      }

      var result = new Set<Expression>();
      foreach (var exp in other.symbolicConditions.Values)
      {
        result.AddIfNotNull(variableReplacer(exp, other.slackVar, this.slackVar));
      }

      return result.Count != 0 ?
        new SymbolicExpressionTracker<Variable, Expression>(this.slackVar, new SetOfConstraints<Expression>(result, false)) :
        this.Top;

    }

    #endregion

    #region ToString

    public override string ToString()
    {
      if (this.IsBottom)
        return "_|_";

      if (this.IsTop)
        return "Top";

      return string.Format("({0} -> {1})",
        this.slackVar != null ? this.slackVar.ToString() : "null", this.symbolicConditions.ToString());
    }

    #endregion
  }
}
