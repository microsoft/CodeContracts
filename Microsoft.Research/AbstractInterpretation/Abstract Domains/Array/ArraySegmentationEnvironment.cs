// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using Microsoft.Research.AbstractDomains.Expressions;
using System.Diagnostics.Contracts;
using Microsoft.Research.DataStructures;

namespace Microsoft.Research.AbstractDomains
{
    [ContractVerification(true)]
    public class ArraySegmentationEnvironment<AbstractDomain, Variable, Expression>
      : FunctionalAbstractDomainEnvironment<ArraySegmentationEnvironment<AbstractDomain, Variable, Expression>, Variable, ArraySegmentation<AbstractDomain, Variable, Expression>, Variable, Expression>
      where AbstractDomain : class, IAbstractDomainForArraySegmentationAbstraction<AbstractDomain, Variable>
    {
        #region Constructor
        public ArraySegmentationEnvironment(ExpressionManager<Variable, Expression> expManager)
          : base(expManager)
        {
            Contract.Requires(expManager != null);
        }

        private ArraySegmentationEnvironment(ExpressionManager<Variable, Expression> expManager, List<List<Variable>> buckets)
          : this(expManager)
        {
            Contract.Requires(expManager != null);
            Contract.Requires(buckets != null);
        }

        private ArraySegmentationEnvironment(ArraySegmentationEnvironment<AbstractDomain, Variable, Expression> other)
          : base(other)
        {
            Contract.Requires(other != null);
        }
        #endregion

        public void Update(Variable v, ArraySegmentation<AbstractDomain, Variable, Expression> newVal)
        {
            Contract.Requires(newVal != null);

            this[v] = newVal;
        }

        public override List<Variable> Variables
        {
            get
            {
                var result = new List<Variable>();

                var bounds = new Set<Variable>();

                foreach (var pair in this.Elements)
                {
                    result.Add(pair.Key);

                    Contract.Assume(pair.Value != null);

                    bounds.AddRange(pair.Value.Variables);
                }

                result.AddRange(bounds);

                return result;
            }
        }

        public override object Clone()
        {
            return DuplicateMe();
        }

        public ArraySegmentationEnvironment<AbstractDomain, Variable, Expression> DuplicateMe()
        {
            return new ArraySegmentationEnvironment<AbstractDomain, Variable, Expression>(this);
        }

        protected override ArraySegmentationEnvironment<AbstractDomain, Variable, Expression> Factory()
        {
            return new ArraySegmentationEnvironment<AbstractDomain, Variable, Expression>(this.ExpressionManager);
        }

        public override void Assign(Expression x, Expression exp)
        {
            foreach (var pair in this.Elements)
            {
                Contract.Assume(pair.Value != null);
                pair.Value.Assign(x, exp);
            }
        }

        public override void ProjectVariable(Variable var)
        {
            if (this.ContainsKey(var))
            {
                this.RemoveElement(var);
            }

            foreach (var pair in this.Elements)
            {
                Contract.Assume(pair.Value != null);
                pair.Value.ProjectVariable(var);
            }
        }

        public override void RemoveVariable(Variable var)
        {
            if (this.ContainsKey(var))
            {
                this.RemoveElement(var);
            }

            foreach (var pair in this.Elements)
            {
                Contract.Assume(pair.Value != null);
                pair.Value.RemoveVariable(var);
            }
        }

        public override void RenameVariable(Variable OldName, Variable NewName)
        {
            ArraySegmentation<AbstractDomain, Variable, Expression> entry;
            if (this.TryGetValue(OldName, out entry))
            {
                this.RemoveElement(OldName);
                this.AddElement(NewName, entry);
            }

            foreach (var pair in this.Elements)
            {
                Contract.Assume(pair.Value != null);
                pair.Value.RenameVariable(OldName, NewName);
            }
        }

        public override ArraySegmentationEnvironment<AbstractDomain, Variable, Expression> TestTrue(Expression guard)
        {
            Contract.Assume(guard != null);

            var result = Factory();

            foreach (var pair in this.Elements)
            {
                Contract.Assume(pair.Value != null);
                var newElem = pair.Value.TestTrue(guard);
                result.AddElement(pair.Key, newElem);
            }

            return result;
        }

        public override ArraySegmentationEnvironment<AbstractDomain, Variable, Expression> TestFalse(Expression guard)
        {
            Contract.Assume(guard != null);

            var result = Factory();

            foreach (var pair in this.Elements)
            {
                Contract.Assume(pair.Value != null);
                var newElem = pair.Value.TestFalse(guard);

                result.AddElement(pair.Key, newElem);
            }

            return result;
        }

        public override FlatAbstractDomain<bool> CheckIfHolds(Expression exp)
        {
            foreach (var pair in this.Elements)
            {
                Contract.Assume(pair.Value != null);
                var outcome = pair.Value.CheckIfHolds(exp);

                if (!outcome.IsTop)
                    return outcome;
            }

            return CheckOutcome.Top;
        }

        public override void AssignInParallel(Dictionary<Variable, FList<Variable>> sourcesToTargets, Converter<Variable, Expression> convert)
        {
            var newMaps = this.Factory();

            foreach (var pair in this.Elements)
            {
                FList<Variable> newNames;
                if (sourcesToTargets.TryGetValue(pair.Key, out newNames))
                {
                    Contract.Assume(pair.Value != null);
                    ArraySegmentation<AbstractDomain, Variable, Expression> renamedElement;
                    if (pair.Value.TryAssignInParallelFunctional(sourcesToTargets, convert, out renamedElement))
                    {
                        renamedElement = renamedElement.Simplify();

                        foreach (var name in newNames.GetEnumerable())
                        {
                            newMaps.AddElement(name, renamedElement);
                        }
                    }
                }
                // else, it is abstracted away...
            }

            // We need constants....
            // So we want to add all the knowledge const == var the possible
            var decoder = this.ExpressionManager.Decoder;
            foreach (var pair in sourcesToTargets)
            {
                int value;
                var sourceExp = convert(pair.Key);
                if (decoder.TryValueOf(sourceExp, ExpressionType.Int32, out value))
                {
                    var k = NormalizedExpression<Variable>.For(value);
                    var eq = new Set<NormalizedExpression<Variable>>();
                    foreach (var v in pair.Value.GetEnumerable())
                    {
                        eq.Add(NormalizedExpression<Variable>.For(v));
                    }

                    newMaps = newMaps.TestTrueEqualAsymmetric(eq, k);
                }
            }

            this.CopyAndTransferOwnership(newMaps);
        }

        public ArraySegmentationEnvironment<AbstractDomain, Variable, Expression>
          TestTrueEqualAsymmetric(NormalizedExpression<Variable> v, NormalizedExpression<Variable> normExpression)
        {
            Contract.Requires(v != null);
            Contract.Ensures(Contract.Result<ArraySegmentationEnvironment<AbstractDomain, Variable, Expression>>() != null);

            var result = Factory();

            foreach (var pair in this.Elements)
            {
                Contract.Assume(pair.Value != null);

                result[pair.Key] = pair.Value.TestTrueEqualAsymmetric(v, normExpression);
            }

            return result;
        }

        public ArraySegmentationEnvironment<AbstractDomain, Variable, Expression> TestTrueEqualAsymmetric(Set<NormalizedExpression<Variable>> vars, NormalizedExpression<Variable> normExpression)
        {
            #region Contract

            Contract.Requires(vars != null);

            Contract.Ensures(Contract.Result<ArraySegmentationEnvironment<AbstractDomain, Variable, Expression>>() != null);

            #endregion

            var result = Factory();

            foreach (var pair in this.Elements)
            {
                Contract.Assume(pair.Value != null);

                result[pair.Key] = pair.Value.TestTrueEqualAsymmetric(vars, normExpression);
            }

            return result;
        }

        public ArraySegmentationEnvironment<AbstractDomain, Variable, Expression> ReduceWith(INumericalAbstractDomainQuery<Variable, Expression> oracle)
        {
            #region Contracts
            Contract.Requires(oracle != null);

            Contract.Ensures(Contract.Result<ArraySegmentationEnvironment<AbstractDomain, Variable, Expression>>() != null);
            #endregion

            var result = Factory();

            foreach (var pair in this.Elements)
            {
                Contract.Assume(pair.Value != null);
                var reduced = pair.Value.ReduceWith(oracle);
                result.AddElement(pair.Key, reduced);
            }

            return result;
        }

        public ArraySegmentationEnvironment<AbstractDomain, Variable, Expression> TestTrueInformationForTheSegments(INumericalAbstractDomainQuery<Variable, Expression> oracle)
        {
            #region Contracts
            Contract.Requires(oracle != null);

            Contract.Ensures(Contract.Result<ArraySegmentationEnvironment<AbstractDomain, Variable, Expression>>() != null);
            #endregion

            var result = Factory();

            foreach (var pair in this.Elements)
            {
                Contract.Assume(pair.Value != null);
                var reduced = pair.Value.TestTrueInformationForTheSegments(oracle);
                result.AddElement(pair.Key, reduced);
            }

            return result;
        }
    }
}
