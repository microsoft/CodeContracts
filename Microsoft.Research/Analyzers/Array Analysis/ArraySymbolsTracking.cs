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
using System.Diagnostics.Contracts;
using Microsoft.Research.AbstractDomains;
using Microsoft.Research.AbstractDomains.Numerical;
using Microsoft.Research.CodeAnalysis;
using System.Diagnostics;
using Microsoft.Research.DataStructures;
using Microsoft.Research.AbstractDomains.Expressions;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Research.CodeAnalysis
{
  public static partial class AnalysisWrapper
  {
    public static partial class TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>
      where Variable : IEquatable<Variable>
      where Expression : IEquatable<Expression>
      where Type : IEquatable<Type>
    {
      [ContractVerification(true)]      
      public class ScalarFromArrayTracking
      : IAbstractDomainWithRenaming<ScalarFromArrayTracking, BoxedVariable<Variable>>
      {
        #region Object invariant

        [ContractInvariantMethod]
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Required for code contracts.")]
        private void ObjectInvariant()
        {
          Contract.Invariant(this.left != null);
          Contract.Invariant(this.right != null);
          Contract.Invariant(this.conditions != null);
          Contract.Invariant(this.isUnmodifiedFromEntry != null);
        }

        #endregion

        #region State

        private readonly SetOfConstraints<BoxedVariable<Variable>> left;   // meaning: all the elements are ==
        private readonly SetOfConstraints<BoxedVariable<Variable>> right;  // meaning: all the elements are ==
        private readonly FlatAbstractDomain<bool> isUnmodifiedFromEntry;
        private readonly SymbolicExpressionTracker<BoxedVariable<Variable>, BoxedExpression> conditions;

        #endregion

        #region Constructor

        public ScalarFromArrayTracking(
          BoxedVariable<Variable> left, BoxedVariable<Variable> right, 
          FlatAbstractDomain<bool> isUnmodifiedFromEntry,
          SymbolicExpressionTracker<BoxedVariable<Variable>, BoxedExpression> conditions)
          : this(new SetOfConstraints<BoxedVariable<Variable>>(left), new SetOfConstraints<BoxedVariable<Variable>>(right),
          isUnmodifiedFromEntry, conditions)
        {
          Contract.Requires(isUnmodifiedFromEntry != null);
          Contract.Requires(conditions != null);
        }

        public ScalarFromArrayTracking(
          SetOfConstraints<BoxedVariable<Variable>> left,
          SetOfConstraints<BoxedVariable<Variable>> right, 
          FlatAbstractDomain<bool> isUnmodifiedFromEntry,
          SymbolicExpressionTracker<BoxedVariable<Variable>, BoxedExpression> conditions)
        {
          Contract.Requires(left != null);
          Contract.Requires(right != null);
          Contract.Requires(isUnmodifiedFromEntry != null);
          Contract.Requires(conditions != null);

          this.left = left;
          this.right = right;
          this.isUnmodifiedFromEntry = isUnmodifiedFromEntry;
          this.conditions = conditions;
        }
        #endregion

        #region Getters

        public SetOfConstraints<BoxedVariable<Variable>> Left { get { return this.left; } }
        public SetOfConstraints<BoxedVariable<Variable>> Right { get { return this.right; } }
        public FlatAbstractDomain<bool> IsUnmodifiedFromEntry { get { return this.isUnmodifiedFromEntry; } }
        public SymbolicExpressionTracker<BoxedVariable<Variable>, BoxedExpression> Conditions { get { return this.conditions; } }

        #endregion

        #region IAbstractDomainWithRenaming<CartestianAbstractDomain<LeftDomain,RightDomain,Variable>,Variable> Members

        ScalarFromArrayTracking IAbstractDomainWithRenaming<ScalarFromArrayTracking, BoxedVariable<Variable>>.Rename(
          Dictionary<BoxedVariable<Variable>, FList<BoxedVariable<Variable>>> renaming)
        {
          return new ScalarFromArrayTracking(this.left.Rename(renaming), this.right.Rename(renaming), this.isUnmodifiedFromEntry, this.conditions);
        }

        #endregion

        #region Abstract Domain operations

        public bool LessEqual(ScalarFromArrayTracking a)
        {
          Contract.Requires(a != null);

          bool result;
          if (AbstractDomainsHelper.TryTrivialLessEqual(this, a, out result))
          {
            return result;
          }

          return this.left.LessEqual(a.left) && this.right.LessEqual(a.right) && this.isUnmodifiedFromEntry.LessEqual(a.isUnmodifiedFromEntry) && this.conditions.LessEqual(a.conditions);
        }

        public ScalarFromArrayTracking Bottom
        {
          get 
          {
            Contract.Ensures(Contract.Result<ScalarFromArrayTracking>() != null);
            
            return new ScalarFromArrayTracking(
              (SetOfConstraints<BoxedVariable<Variable>>)this.left.Bottom,
              (SetOfConstraints<BoxedVariable<Variable>>)this.right.Bottom, 
              this.isUnmodifiedFromEntry.Bottom,
              SymbolicExpressionTracker<BoxedVariable<Variable>, BoxedExpression>.Unreached); 
          }
        }

        public ScalarFromArrayTracking Top
        {
          get 
          {
            Contract.Ensures(Contract.Result<ScalarFromArrayTracking>() != null);
          
            return new ScalarFromArrayTracking(
              (SetOfConstraints<BoxedVariable<Variable>>)this.left.Top,
              (SetOfConstraints<BoxedVariable<Variable>>)this.right.Top,
              this.isUnmodifiedFromEntry.Top, SymbolicExpressionTracker<BoxedVariable<Variable>, BoxedExpression>.Unknown); 
          }
        }

        public ScalarFromArrayTracking Join(ScalarFromArrayTracking a)
        {
          Contract.Requires(a != null);
          Contract.Ensures(Contract.Result<ScalarFromArrayTracking>() != null);

          ScalarFromArrayTracking result;
          if (AbstractDomainsHelper.TryTrivialJoin(this, a, out result))
          {
            return result;
          }

          return new ScalarFromArrayTracking(
            (SetOfConstraints<BoxedVariable<Variable>>)this.left.Join(a.left),
            (SetOfConstraints<BoxedVariable<Variable>>)this.right.Join(a.right), 
            this.isUnmodifiedFromEntry.Join(a.isUnmodifiedFromEntry), this.conditions.Join(a.conditions));
        }

        public ScalarFromArrayTracking Meet(ScalarFromArrayTracking a)
        {
          Contract.Requires(a != null);
          Contract.Ensures(Contract.Result<ScalarFromArrayTracking>() != null);

          ScalarFromArrayTracking result;
          if (AbstractDomainsHelper.TryTrivialMeet(this, a, out result))
          {
            return result;
          }

          var meetWrite = 
            this.isUnmodifiedFromEntry.IsTop? a.isUnmodifiedFromEntry :
            a.isUnmodifiedFromEntry.IsTop ? this.isUnmodifiedFromEntry:
            a.isUnmodifiedFromEntry.BoxedElement == this.isUnmodifiedFromEntry.BoxedElement? a.isUnmodifiedFromEntry :
            CheckOutcome.Top;

          Contract.Assume(meetWrite != null);

          return new ScalarFromArrayTracking(
            (SetOfConstraints<BoxedVariable<Variable>>)this.left.Meet(a.left),
            (SetOfConstraints<BoxedVariable<Variable>>)this.right.Meet(a.right),
            meetWrite, this.conditions.Meet(a.conditions));
        }

        public ScalarFromArrayTracking Widening(ScalarFromArrayTracking prev)
        {
          Contract.Requires(prev != null);
          Contract.Ensures(Contract.Result<ScalarFromArrayTracking>() != null);

          ScalarFromArrayTracking result;
          if (AbstractDomainsHelper.TryTrivialJoin(this, prev, out result))
          {
            return result;
          }

          return new ScalarFromArrayTracking(
            (SetOfConstraints<BoxedVariable<Variable>>)this.left.Widening(prev.left),
            (SetOfConstraints<BoxedVariable<Variable>>)this.right.Widening(prev.right),
            this.isUnmodifiedFromEntry.Join(prev.isUnmodifiedFromEntry), this.conditions.Join(prev.conditions));
        }

        #endregion

        #region IAbstractDomain Members

        public bool IsBottom
        {
          get { return this.left.IsBottom || this.right.IsBottom || this.isUnmodifiedFromEntry.IsBottom || this.conditions.IsBottom; }
        }

        public bool IsTop
        {
          get { return this.left.IsTop && this.right.IsTop && this.isUnmodifiedFromEntry.IsTop && this.conditions.IsTop; }
        }

        bool IAbstractDomain.LessEqual(IAbstractDomain a)
        {
          var other = a as ScalarFromArrayTracking;
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
          var other = a as ScalarFromArrayTracking;
          Contract.Assume(other != null);

          return this.Join(other);
        }

        IAbstractDomain IAbstractDomain.Meet(IAbstractDomain a)
        {
          var other = a as ScalarFromArrayTracking;
          Contract.Assume(other != null);

          return this.Meet(other);
        }

        IAbstractDomain IAbstractDomain.Widening(IAbstractDomain prev)
        {
          var other = prev as ScalarFromArrayTracking;
          Contract.Assume(other != null);

          return this.Widening(other);
        }

        public T To<T>(IFactory<T> factory)
        {
          return factory.And(this.left.To(factory), this.right.To(factory));
        }

        #endregion

        #region ICloneable Members

        object ICloneable.Clone()
        {
          return new ScalarFromArrayTracking(
            (SetOfConstraints<BoxedVariable<Variable>>)this.left.Clone(),
            (SetOfConstraints<BoxedVariable<Variable>>)this.right.Clone(), 
            this.isUnmodifiedFromEntry,
            this.conditions);
        }

        #endregion

        #region ToString
        public override string ToString()
        {
          if (this.IsBottom)
            return "_|_";

          if (this.IsTop)
            return "Top";

          var purity = "";
          if (this.isUnmodifiedFromEntry.IsNormal())
          {
            purity = this.isUnmodifiedFromEntry.IsTrue() ? "pure" : "not-pure?";
          }

          return string.Format("({0}, {1}){2}{3}",
            this.left.ToString(),
            this.right.ToString(),
            purity,
            this.conditions.IsNormal()? this.conditions.ToString() : "");
        }
        #endregion

        public ScalarFromArrayTracking AddCondition(BoxedVariable<Variable> var, BoxedExpression sourceExp)
        {
          Contract.Requires(sourceExp != null);
          Contract.Ensures(Contract.Result<ScalarFromArrayTracking>() != null);

          var conditions = this.conditions.Meet(new SymbolicExpressionTracker<BoxedVariable<Variable>, BoxedExpression>(var, sourceExp));

          return new ScalarFromArrayTracking(this.left, this.right, this.isUnmodifiedFromEntry, conditions);
        }
      }

      /// <summary>
      /// A map var -> (array, index, unmodified), i.e. we track for a scalar variable 'var', if it can be refined to an array 
      /// (in which case index != top) or if its value flows from an element of the array 'array'. 
      /// If unmodified == true, it means that the value is the same as in the entry state (i.e. the array index has not been written meanwhile)
      /// </summary>
      /// 
      [ContractVerification(false)]
      public class ArrayTracking : 
        FunctionalAbstractDomainEnvironment<ArrayTracking, BoxedVariable<Variable>, ScalarFromArrayTracking,
          BoxedVariable<Variable>, BoxedExpression>
      {
        #region Constructor

        public ArrayTracking(
          ExpressionManager<BoxedVariable<Variable>, BoxedExpression> expManager)
          : base(expManager)
        {

          Contract.Requires(expManager != null);
        }

        private ArrayTracking(ArrayTracking other)
          : base(other)
        {
          Contract.Requires(other != null);
        }

        #endregion

        public override object Clone()
        {
          return new ArrayTracking(this);
        }

        protected override ArrayTracking Factory()
        {
          return new ArrayTracking(this.ExpressionManager);
        }

        public override List<BoxedVariable<Variable>> Variables
        {
          get
          {
            var result = new List<BoxedVariable<Variable>>();

            if (this.IsNormal())
            {
              foreach (var element in this.Elements)
              {
                result.Add(element.Key);
                if (element.Value.Left.IsNormal())
                {
                  result.AddRange(element.Value.Left.Values);
                }
                if (element.Value.Right.IsNormal())
                {
                  result.AddRange(element.Value.Right.Values);
                }
              }
            }
            return result;
          }
        }

        public override void Assign(BoxedExpression x, BoxedExpression exp)
        {
          //
        }

        public override void ProjectVariable(BoxedVariable<Variable> var)
        {
          if (this.ContainsKey(var))
          {
            this.RemoveElement(var);
          }
        }

        public override void RemoveVariable(BoxedVariable<Variable> var)
        {
          if (this.ContainsKey(var))
          {
            this.RemoveElement(var);
          }
        }

        public override ArrayTracking TestTrue(BoxedExpression guard)
        {
          return this;
        }

        public override ArrayTracking TestFalse(BoxedExpression guard)
        {
          return this;
        }

        public override FlatAbstractDomain<bool> CheckIfHolds(BoxedExpression exp)
        {
          return CheckOutcome.Top;
        }

        public override void RenameVariable(BoxedVariable<Variable> OldName, BoxedVariable<Variable> NewName)
        {
          // do nothing?
        }

        public override void AssignInParallel(
          Dictionary<BoxedVariable<Variable>, FList<BoxedVariable<Variable>>> sourcesToTargets,
          Converter<BoxedVariable<Variable>, BoxedExpression> convert)
        {
          if(!this.IsNormal())
          {
            return;
          }

          var result = new
          List<Pair<BoxedVariable<Variable>, ScalarFromArrayTracking>>();

          foreach (var element in this.Elements)
          {
            FList<BoxedVariable<Variable>> newNames;
            if (sourcesToTargets.TryGetValue(element.Key, out newNames))
            {
              var newLeft = element.Value.Left.Rename(sourcesToTargets);
              var newRight = element.Value.Right.Rename(sourcesToTargets);


              if (newLeft.IsNormal() || newRight.IsNormal())
              {
                var renamedPair = new ScalarFromArrayTracking(
                  newLeft, newRight, element.Value.IsUnmodifiedFromEntry, element.Value.Conditions);

                foreach (var newKey in newNames.GetEnumerable())
                {
                  result.Add(newKey, renamedPair);
                }
              }
            }
          }

          this.ClearElements();
          foreach (var el in result)
          {
            this[el.One] = el.Two;
          }
        }

        #region To

        protected override T To<T>(BoxedVariable<Variable> d, ScalarFromArrayTracking c, IFactory<T> factory)
        {
          var result = factory.IdentityForAnd;

          if (c.Left.IsNormal())
          {
            T name;
            if (factory.TryGetBoundVariable(out name))
            {
              var first = true;

              foreach (var x in c.Left.Values)
              {
                T arrayLength;
                if (factory.TryArrayLengthName(factory.Variable(x), out arrayLength))
                {
                  var body = factory.EqualTo(factory.Variable(d), factory.ArrayIndex(factory.Variable(x), name));
                  var exists = factory.Exists(factory.Constant(0), arrayLength, body);

                  if (first)
                  {
                    result = exists;
                    first = false;
                  }
                  else
                  {
                    result = factory.And(result, exists);
                  }
                }
              }
            }
          }

          return result;
        }
        
        #endregion
      }
    }
  }
}