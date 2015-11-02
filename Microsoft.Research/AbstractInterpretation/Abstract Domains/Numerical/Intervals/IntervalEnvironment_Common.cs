// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.Research.AbstractDomains.Expressions;
using Microsoft.Research.DataStructures;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System;
using System.Diagnostics.Contracts;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Research.CodeAnalysis;

namespace Microsoft.Research.AbstractDomains.Numerical
{
    [ContractVerification(true)]
    [ContractClass(typeof(IntervalEnvironment_BaseContracts<,,,,>))]
    abstract public class IntervalEnvironment_Base<This, Variable, Expression, IType, NType>
      : FunctionalAbstractDomain<This, Variable, IType>,
          INumericalAbstractDomain<Variable, Expression>,
          ISetOfNumbersAbstraction<Variable, Expression, NType, IType>,
          IIntervalAbstraction<Variable, Expression>
      where IType : IntervalBase<IType, NType>
      where This : IntervalEnvironment_Base<This, Variable, Expression, IType, NType>
    {
        #region Constants and statics

        static protected int NEXT_ID = 0;          // The ID for the domains
        protected const int MAXDEPTH = 10;     // The max depth for evaluating the expressions

        private static int NextID
        {
            get
            {
                return NEXT_ID++;
            }
        }

        #endregion

        #region Object invariant
        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(expManager != null);
            Contract.Invariant(checkIfHoldsVisitor != null);
        }
        #endregion

        #region Protected and Private fields (the encoder and decoder, the visitors, the domain id, ...)

        private readonly ExpressionManager<Variable, Expression> expManager;
        private readonly IntervalsCheckIfHoldsVisitor checkIfHoldsVisitor;

        private int id = -1;   // to be estabilished after the constructor...
        private int parent_id = -1;

        protected readonly Logger Log;

        #endregion

        #region Constructor

        protected IntervalEnvironment_Base(IntervalEnvironment_Base<This, Variable, Expression, IType, NType> original)
          : base(original)
        {
            Contract.Requires(original != null);

            Contract.Assume(original.expManager != null);
            expManager = original.expManager;

            id = NextID;
            parent_id = original.id;

            Contract.Assume(original.checkIfHoldsVisitor != null);

            checkIfHoldsVisitor = original.checkIfHoldsVisitor;

            this.Log = original.Log;
        }

        protected IntervalEnvironment_Base(ExpressionManager<Variable, Expression> expManager)
          : this(expManager, new IntervalsCheckIfHoldsVisitor(expManager.Decoder))
        {
            Contract.Requires(expManager != null);
        }

        protected IntervalEnvironment_Base(ExpressionManager<Variable, Expression> expManager, IntervalsCheckIfHoldsVisitor checker)
          : base()
        {
            Contract.Requires(expManager != null);
            Contract.Requires(checker != null);

            this.expManager = expManager;

            id = NextID;
            parent_id = -1;

            checkIfHoldsVisitor = checker;
        }
        #endregion

        #region Getters and setters

        public ExpressionManager<Variable, Expression> ExpressionManager
        {
            get
            {
                Contract.Ensures(Contract.Result<ExpressionManager<Variable, Expression>>() != null);

                return expManager;
            }
        }

        sealed public override IType this[Variable index]
        {
            get
            {
                return base[index];
            }
            set
            {
                // Avoid keeping dummy variables
                if (this.ExpressionManager.Decoder.IsSlackOrFrameworkVariable(index))
                {
                    base[index] = value;
                }
            }
        }

        #endregion

        #region To be overridden

        abstract protected This DuplicateMe();
        abstract protected This NewInstance(ExpressionManager<Variable, Expression> expManager);
        abstract protected IType ConvertInterval(Interval intv);

        // Assumptions
        abstract public This TestTrueGeqZero(Expression exp);
        abstract public This TestTrueLessThan(Expression exp1, Expression exp2);
        abstract public This TestTrueLessEqualThan(Expression exp1, Expression exp2);

        abstract public This TestTrueLessThan_Un(Expression exp1, Expression exp2);
        abstract public This TestTrueLessEqualThan_Un(Expression exp1, Expression exp2);

        abstract protected This TestNotEqualToZero(Expression guard);
        abstract protected This TestNotEqualToZero(Variable v);

        abstract protected This TestEqualToZero(Variable v);

        abstract protected void AssumeKLessThanRight(IType k, Variable right);
        abstract protected void AssumeLeftLessThanK(Variable left, IType k);

        // BoundsFor
        abstract public DisInterval BoundsFor(Expression exp);
        abstract public DisInterval BoundsFor(Variable v);

        // Assign
        abstract protected void AssumeInDisInterval_Internal(Variable x, DisInterval value);

        // Arithmetics
        abstract public bool IsGreaterEqualThanZero(NType val);
        abstract public bool IsGreaterThanZero(NType val);
        abstract public bool IsLessThanZero(NType val);
        abstract public bool IsLessEqualThanZero(NType val);

        abstract public bool IsLessThan(NType val1, NType val2);
        abstract public bool IsLessEqualThan(NType val1, NType val2);
        abstract public bool IsZero(NType val);
        abstract public bool IsNotZero(NType val);
        abstract public bool IsPlusInfinity(NType val);
        abstract public bool IsMinusInfinity(NType val);

        abstract public bool AreEqual(IType left, IType right);

        abstract public NType PlusInfinity { get; }
        abstract public NType MinusInfinty { get; }
        abstract public bool TryAdd(NType left, NType right, out NType result);

        abstract public IType IntervalUnknown { get; }
        abstract public IType IntervalZero { get; }
        abstract public IType IntervalOne { get; }
        abstract public IType IntervalGreaterEqualThanMinusOne { get; }
        abstract public IType Interval_Positive { get; }
        abstract public IType Interval_StrictlyPositive { get; }
        abstract public IType IntervalSingleton(NType val);
        abstract public IType IntervalRightOpen(NType inf);
        abstract public IType IntervalLeftOpen(NType sup);

        abstract public bool IsMaxInt32(IType intv);
        abstract public bool IsMinInt32(IType intv);

        abstract public IType Interval_Add(IType left, IType right);
        abstract public IType Interval_Div(IType left, IType right);
        abstract public IType Interval_Sub(IType left, IType right);
        abstract public IType Interval_Mul(IType left, IType right);
        abstract public IType Interval_Not(IType left);
        abstract public IType Interval_UnaryMinus(IType left);
        abstract public IType Interval_Rem(IType left, IType right);

        abstract public IType Interval_BitwiseAnd(IType left, IType right);
        abstract public IType Interval_BitwiseOr(IType left, IType right);
        abstract public IType Interval_BitwiseXor(IType left, IType right);

        abstract public IType Interval_ShiftLeft(IType left, IType right);
        abstract public IType Interval_ShiftRight(IType left, IType right);

        abstract public IType For(Byte v);

        abstract public IType For(Double d);
        abstract public IType For(Int16 v);
        abstract public IType For(Int32 v);
        abstract public IType For(Int64 v);
        abstract public IType For(SByte s);
        abstract public IType For(UInt16 u);
        abstract public IType For(UInt32 u);
        abstract public IType For(Rational r);
        abstract public IType For(NType inf, NType sup);

        abstract protected IType ApplyConversion(ExpressionOperator conversionType, IType val);

        abstract protected T To<T>(NType n, IFactory<T> factory);

        #endregion

        #region Helpers

        virtual public FlatAbstractDomain<bool> IsNotZero(IType intv)
        {
            Contract.Ensures(Contract.Result<FlatAbstractDomain<bool>>() != null);

            if (intv.IsNormal)
            {
                if (this.IsGreaterThanZero(intv.LowerBound) || this.IsLessThanZero(intv.UpperBound))
                {
                    return CheckOutcome.True;
                }
                if (this.IsZero(intv.LowerBound) && this.IsZero(intv.UpperBound))
                {
                    return CheckOutcome.False;
                }
            }

            return CheckOutcome.Top;
        }

        public bool IsGreaterEqualThanZero(IType intv)
        {
            return intv.IsNormal && this.IsGreaterEqualThanZero(intv.LowerBound);
        }

        public bool IsLessEqualThanZero(IType intv)
        {
            return intv.IsNormal && this.IsLessEqualThanZero(intv.UpperBound);
        }
        #endregion

        #region Common Assumptions
        /// <summary>
        /// Assume that <code>k \leq right</code>
        /// </summary>
        protected void AssumeKLessEqualThanRight(IType k, Variable right)
        {
            IType refinedIntv;
            if (IntervalInference.TryRefine_KLessEqualThanRight(true, k, right, this, out refinedIntv))
            {
                this[right] = refinedIntv;
            }
        }

        /// <summary>
        /// Assume that <code>left \leq k</code>
        /// </summary>
        protected void AssumeLeftLessEqualThanK(Variable left, IType k)
        {
            IType refinedIntv;
            if (IntervalInference.TryRefine_LeftLessEqualThanK(true, left, k, this, out refinedIntv))
            {
                this[left] = refinedIntv;
            }
        }
        #endregion

        #region IPureAssignment

        /// <summary>
        /// Evaluates the expression on the left (that must be an arithmetic expression) and assigns it to the variable <code>x</code>
        /// </summary>
        public void Assign(Expression x, Expression exp)
        {
            State = AbstractState.Normal;

            var val = Eval(exp);

            var xVar = expManager.Decoder.UnderlyingVariable(x);

            if (val.IsBottom)
            {
                this.State = AbstractState.Bottom;
                this.ClearElements();
            }
            else if (val.IsTop)
            {
                return;
            }
            else
            {
                IType prev;
                if (this.TryGetValue(xVar, out prev))
                {
                    val = prev.Meet(val);
                }

                this[xVar] = val;
            }
        }

        public void Assign(Expression x, Expression exp, INumericalAbstractDomainQuery<Variable, Expression> preState)
        {
            this.Assign(x, exp);

            this.RefineWithPrestate(x, exp, preState);
        }

        /// <summary>
        /// Assign the expression 
        /// </summary>
        public void AssumeInDisInterval(Variable x, DisInterval value)
        {
            if (value.IsTop)
            {
                return;
            }

            AssumeInDisInterval_Internal(x, value);
        }

        /// <summary>
        /// Perform the parallel assignment.
        /// First, it evaluates all the expressions in the current state (evaluation is side effect free).
        /// Then, it updates the values
        /// </summary>
        public virtual void AssignInParallel(Dictionary<Variable, FList<Variable>> sourcesToTargets, Converter<Variable, Expression> convert)
        {
            var result = NewInstance(expManager);

            Contract.Assert(result != null);

            foreach (var e_pair in this.Elements)
            {
                if (expManager.Decoder.IsSlackVariable(e_pair.Key))
                {
                    result.AddElement(e_pair.Key, e_pair.Value);
                }
                Contract.Assert(result != null); // F: It was a bug in Clousot that Manuel fixed. I keep the assertion for regression purposes
            }

            Contract.Assert(result != null);

            this.State = AbstractState.Normal;

            if (sourcesToTargets.Count == 0)
            {
                // do nothing...
            }
            else
            {
#if true
                foreach (var pair in this.Elements)
                {
                    FList<Variable> values;
                    if (sourcesToTargets.TryGetValue(pair.Key, out values))
                    {
                        foreach (var target in values.GetEnumerable())
                        {
                            result[target] = pair.Value;
                        }
                    }
                }
#else
        // Update the values
        foreach (var pair in sourcesToTargets)
        {
          var value = Eval(convert(pair.Key));

          if (value.IsTop)
          {           
            continue;
          }

          foreach (var target in pair.Value.GetEnumerable())
          {
            result[target] = value;
          }

          Contract.Assert(result != null);

        }
#endif
            }

            // Update the state
            this.CopyAndTransferOwnership(result);
        }

        protected void RefineWith(Variable var, IType intv)
        {
            Contract.Requires(intv != null);

            if (intv.IsTop)
            {
                return;
            }

            IType prev;
            if (this.TryGetValue(var, out prev))
            {
                this[var] = prev.Meet(intv);
            }
            else
            {
                this[var] = intv;
            }
        }

        protected void TestTrueListOfFacts(List<Pair<Variable, IType>> list)
        {
            Contract.Requires(list != null);

            foreach (var pair in list)
            {
                Contract.Assume(pair.Two != null);

                this.RefineWith(pair.One, pair.Two);
            }
        }

        protected List<Pair<Variable, IType>> JoinConstraints(
          List<Pair<Variable, IType>> constraintsLT,
          List<Pair<Variable, IType>> constraintsGT)
        {
            Contract.Requires(constraintsLT != null);
            Contract.Requires(constraintsGT != null);
            Contract.Ensures(Contract.Result<List<Pair<Variable, IType>>>() != null);

            var result = new List<Pair<Variable, IType>>();

            if (constraintsGT.Count == 0 || constraintsLT.Count == 0)
            {
                return result;
            }

            foreach (var pair in constraintsLT)
            {
                IType intv;
                if (constraintsGT.SearchAndRemoveIfFound(pair.One, out intv))
                {
                    Contract.Assume(intv != null);
                    intv = pair.Two.Join(intv);
                    if (!intv.IsTop)
                    {
                        result.Add(pair.One, intv);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// The variables defined in this environment
        /// </summary>
        public List<Variable> Variables
        {
            get
            {
                return new List<Variable>(this.Keys);
            }
        }

        public List<Variable> VariablesNonSlack
        {
            get
            {
                var result = new List<Variable>();
                foreach (var k in this.Keys)
                {
                    if (!expManager.Decoder.IsSlackVariable(k))
                    {
                        result.Add(k);
                    }
                }

                return result;
            }
        }

        /// <summary>
        /// Add the variable <code>var</code> to the environment
        /// </summary>
        /// <param name="var">Must not be already there</param>
        public void AddVariable(Variable var)
        {
            this.State = AbstractState.Normal;

            this.ProjectVariable(var);
        }

        /// <summary>
        /// Projects out the variable <code>var</code>.
        /// Its value is set to top.
        /// </summary>
        public void ProjectVariable(Variable var)
        {
            if (this.ContainsKey(var))
            {
                this.RemoveElement(var);
            }
        }

        /// <summary>
        /// Removes the variable <code>var</code>
        /// </summary>
        /// <param name="var"></param>
        public void RemoveVariable(Variable var)
        {
            this.RemoveElement(var);
        }

        /// <summary>
        /// Renames the variable <code>oldName</code> to <code>newName</code>
        /// </summary>
        public void RenameVariable(Variable oldName, Variable newName)
        {
            if (this.ContainsKey(oldName))
            {
                this[newName] = this[oldName];
                this.RemoveVariable(oldName);
            }
        }

        #endregion

        #region Eval
        protected internal IType EvalPolynomial(Polynomial<Variable, Expression> pol)
        {
            Contract.Requires(pol.Relation == null);

            IType i;
            int l, u;
            return this.EvalPolynomial(pol, out i, out l, out u);
        }

        protected internal IType EvalPolynomial(Polynomial<Variable, Expression> pol, out IType finiteBounds, out int infLower, out int infUpper)
        {
            Contract.Requires(pol.Relation == null);

            infLower = 0;
            infUpper = 0;

            var tmpValue = this.IntervalZero;
            finiteBounds = this.IntervalZero;

            foreach (var m in pol.Left)
            {
                var tmpInt = EvalMonomial(m);
                tmpValue = Interval_Add(tmpValue, tmpInt);

                var newLower = finiteBounds.LowerBound;
                var newUpper = finiteBounds.UpperBound;

                if (this.IsPlusInfinity(tmpInt.UpperBound))
                {
                    infUpper++;
                }
                else
                {
                    if (!TryAdd(newUpper, tmpInt.UpperBound, out newUpper))
                    {
                        newUpper = this.PlusInfinity;
                    }
                    Contract.Assume(!object.Equals(newUpper, null));
                }
                if (this.IsMinusInfinity(tmpInt.LowerBound))
                {
                    infLower++;
                }
                else
                {
                    if (!TryAdd(newLower, tmpInt.LowerBound, out newLower))
                    {
                        newLower = this.MinusInfinty;
                    }
                    Contract.Assume(!object.Equals(newLower, null));
                }
                finiteBounds = For(newLower, newUpper);
            }

            return tmpValue;
        }

        protected IType EvalMonomial(Monomial<Variable> m)
        {
            Contract.Ensures(Contract.Result<IType>() != null);

            var tmpValue = For(m.K);
            IType valueForX;

            foreach (var x in m.Variables)
            {
                if (this.TryGetValue(x, out valueForX))
                {
                    tmpValue = Interval_Mul(tmpValue, valueForX);
                }
                else
                {
                    return IntervalUnknown;
                }
            }

            return tmpValue;
        }

        public IType Eval(Variable v)
        {
            Contract.Ensures(Contract.Result<IType>() != null);

            IType val;
            if (this.TryGetValue(v, out val))
            {
                return val;
            }

            return this.IntervalUnknown;
        }

        /// <summary>
        /// The difference with BoundsFor is in the return type, which is more specific for Eval 
        /// </summary>
        public IType Eval(Expression exp)
        {
            Contract.Ensures(Contract.Result<IType>() != null);

            Contract.Assume(exp != null);

#if TRACE_PERFORMANCE
            var watch = new CustomStopwatch();
            watch.Start();
#endif
            // Such a common situation that we want to short cut it
            int value;
            if (expManager.Decoder.IsConstantInt(exp, out value))
            {
                return For(value);
            }

            // F: funny enough, making evalVisitor a field makes the analysis slower!
            var evalVisitor = new EvalExpressionVisitor(expManager.Decoder);

            var result = evalVisitor.Visit(exp, new Pair<This, int>((This)this, 0));

            Contract.Assert(result != null);

            if (evalVisitor.DuplicatedOccurrences.Count >= 1)
            {
                var intv = default(IType);
                var isFirst = true;

                foreach (var v in evalVisitor.DuplicatedOccurrences)
                {
                    IType intvForV;
                    if (this.TryGetValue(v, out intvForV)
                      && intvForV.IsFinite
                      && this.IsGreaterEqualThanZero(intvForV.LowerBound) // the precision improvement below holds when the interval is non-negative
                      )
                    {
                        var r = EvalWithExtremes(exp, v, intvForV);
                        if (isFirst)
                        {
                            intv = r;
                            isFirst = false;
                        }
                        else
                        {
                            Contract.Assert(intv != null);
                            intv = r.Join(intv);
                        }
                    }
                    Contract.Assert(isFirst || intv != null);
                }

                result = isFirst ? result : result.Meet(intv);
                Contract.Assert(result != null);
            }

#if TRACE_PERFORMANCE
            UpdateTimeSpentInIntervalEval(watch.Elapsed);
#endif
            return result;
        }

        private IType EvalWithExtremes(Expression exp, Variable v, IType intv)
        {
            Contract.Requires(exp != null);
            Contract.Ensures(Contract.Result<IType>() != null);

            var evalVisitor = new EvalExpressionVisitor(expManager.Decoder);

            this[v] = IntervalSingleton(intv.LowerBound);

            var low = evalVisitor.Visit(exp, new Pair<This, int>((This)this, 0));

            this[v] = IntervalSingleton(intv.UpperBound);

            var upp = evalVisitor.Visit(exp, new Pair<This, int>((This)this, 0));

            Contract.Assert(upp != null);

            this[v] = intv;

            return low.Join(upp);
        }

        protected IType EvalConstant(Expression exp)
        {
            Contract.Requires(exp != null);

            return new EvalConstantVisitor(this.ExpressionManager.Decoder).Visit(exp, (This)this);
        }
        #endregion

        #region Checks
        public FlatAbstractDomain<bool> CheckIfHolds(Expression exp)
        {
            Contract.Ensures(Contract.Result<FlatAbstractDomain<bool>>() != null);

            var result = checkIfHoldsVisitor.Visit(exp, (This)this);

            return result;
        }

        public FlatAbstractDomain<bool> CheckIfGreaterEqualThanZero(Expression exp)
        {
            Contract.Ensures(Contract.Result<FlatAbstractDomain<bool>>() != null);

            var boundsForIndex = this.Eval(exp);

            if (boundsForIndex.IsBottom)
            {
                return CheckOutcome.Bottom;
            }
            else if (boundsForIndex.IsNormal)
            {
                if (this.IsGreaterEqualThanZero(boundsForIndex.LowerBound))
                {
                    return CheckOutcome.True;
                }
                else if (this.IsLessThanZero(boundsForIndex.UpperBound))
                {
                    return CheckOutcome.False;
                }
                else
                {
                    return CheckOutcome.Top;
                }
            }

            // It may be the case that the naive evaluation of the expression may cause a lose of precision (we had this case in mscorlib)
            // As a consequence, we want to simplify the expression to a polynomial, and reasoning on it
            Polynomial<Variable, Expression> pol;
            if (Polynomial<Variable, Expression>.TryToPolynomialForm(exp, expManager.Decoder, out pol))
            {
                return this.CheckIfGreaterEqualThanZero(pol);
            }
            return CheckOutcome.Top;
        }

        public FlatAbstractDomain<bool> CheckIfLessThan(Expression e1, Expression e2)
        {
            var v1 = Eval(e1);
            var v2 = Eval(e2);

            return this.CheckIfLessThan(v1, v2);
        }

        public FlatAbstractDomain<bool> CheckIfLessThanIncomplete(Expression e1, Expression e2)
        {
            return this.CheckIfLessThan(e1, e2);
        }

        public FlatAbstractDomain<bool> CheckIfLessThan(Variable e1, Variable e2)
        {
            var v1 = EvalFast(e1);
            var v2 = EvalFast(e2);

            return this.CheckIfLessThan(v1, v2);
        }

        public FlatAbstractDomain<bool> CheckIfLessThan_Un(Expression e1, Expression e2)
        {
            var v1 = Eval(e1);
            var v2 = Eval(e2);

            if (!expManager.Decoder.TypeOf(e1).IsFloatingPointType() && !expManager.Decoder.TypeOf(e2).IsFloatingPointType())
            {
                ToUInt(ref v1, ref v2);
            }
            return this.CheckIfLessThan(v1, v2);
        }

        public FlatAbstractDomain<bool> CheckIfLessThan_Un(Variable e1, Variable e2)
        {
            var v1 = EvalFast(e1);
            var v2 = EvalFast(e2);

            ToUInt(ref v1, ref v2);

            return this.CheckIfLessThan(v1, v2);
        }

        public FlatAbstractDomain<bool> CheckIfLessEqualThan(Expression e1, Expression e2)
        {
            var v1 = Eval(e1);
            var v2 = Eval(e2);

            return CheckIfLessEqualThan(v1, v2);
        }

        public FlatAbstractDomain<bool> CheckIfLessEqualThan(Variable e1, Variable e2)
        {
            var v1 = EvalFast(e1);
            var v2 = EvalFast(e2);

            return this.CheckIfLessEqualThan(v1, v2);
        }

        public FlatAbstractDomain<bool> CheckIfLessEqualThan_Un(Expression e1, Expression e2)
        {
            var v1 = Eval(e1);
            var v2 = Eval(e2);

            if (!expManager.Decoder.TypeOf(e1).IsFloatingPointType() && !expManager.Decoder.TypeOf(e2).IsFloatingPointType())
            {
                ToUInt(ref v1, ref v2);
            }

            return CheckIfLessEqualThan(v1, v2);
        }

        public FlatAbstractDomain<bool> CheckIfLessEqualThan_Un(Variable e1, Variable e2)
        {
            var v1 = EvalFast(e1);
            var v2 = EvalFast(e2);

            ToUInt(ref v1, ref v2);

            return this.CheckIfLessEqualThan(v1, v2);
        }

        public FlatAbstractDomain<bool> CheckIfEqual(Expression e1, Expression e2)
        {
            IType v1, v2;

            if ((v1 = Eval(e1)).IsSingleton && (v2 = Eval(e2)).IsSingleton)
            {
                return CheckIfEqual(v1, v2);
            }
            return CheckOutcome.Top;
        }

        public FlatAbstractDomain<bool> CheckIfEqualIncomplete(Expression e1, Expression e2)
        {
            return this.CheckIfEqual(e1, e2);
        }

        virtual public FlatAbstractDomain<bool> CheckIfNonZero(Expression e)
        {
            var r = this.Eval(e);

            return this.IsNotZero(r);
        }

        /// <summary>
        /// Checks if all the elements of the interval <code>v1</code> are smaller than those in <code>v2</code>
        /// </summary>
        protected FlatAbstractDomain<bool> CheckIfLessThan(IType v1, IType v2)
        {
            Contract.Requires(v1 != null);
            Contract.Requires(v2 != null);

            Contract.Ensures(Contract.Result<FlatAbstractDomain<bool>>() != null);

            if (!v1.IsNormal || !v2.IsNormal)
            {
                return CheckOutcome.Top;
            }

            var lowerBoundForV1 = v1.LowerBound;
            var upperBoundForV1 = v1.UpperBound;
            var lowerBoundForV2 = v2.LowerBound;
            var upperBoundForV2 = v2.UpperBound;

            if (IsLessThan(upperBoundForV1, lowerBoundForV2))
            {
                return CheckOutcome.True;
            }
            else if (IsLessEqualThan(upperBoundForV2, lowerBoundForV1))
            {
                return CheckOutcome.False;
            }
            else
            {
                return CheckOutcome.Top;
            }
        }

        /// <summary>
        /// Checks if all the elements of the interval <code>v1</code> are smaller or equal of those in <code>v2</code>
        /// </summary>
        protected FlatAbstractDomain<bool> CheckIfLessEqualThan(IType v1, IType v2)
        {
            Contract.Requires(v1 != null);
            Contract.Requires(v2 != null);
            Contract.Ensures(Contract.Result<FlatAbstractDomain<bool>>() != null);

            if (!v1.IsNormal || !v2.IsNormal)
            {
                return CheckOutcome.Top;
            }

            var lowerBoundForV1 = v1.LowerBound;
            var upperBoundForV1 = v1.UpperBound;
            var lowerBoundForV2 = v2.LowerBound;
            var upperBoundForV2 = v2.UpperBound;

            if (IsLessEqualThan(upperBoundForV1, lowerBoundForV2))
            {
                return CheckOutcome.True;
            }
            else if (IsLessThan(upperBoundForV2, lowerBoundForV1))
            {
                return CheckOutcome.False;
            }
            else
            {
                return CheckOutcome.Top;
            }
        }

        protected FlatAbstractDomain<bool> CheckIfEqual(IType v1, IType v2)
        {
            Contract.Requires(v1 != null);
            Contract.Requires(v2 != null);
            Contract.Requires(v1.IsSingleton);
            Contract.Requires(v2.IsSingleton);

            //if (v1.IsSingleton && v2.IsSingleton)
            {
                return v1.LowerBound.Equals(v2.LowerBound) ? CheckOutcome.True : CheckOutcome.False;
            }

            // return CheckOutcome.Top;
        }

        #endregion

        #region INumericalAbstractDomain
        public This RemoveRedundanciesWith(INumericalAbstractDomainQuery<Variable, Expression> oracle)
        {
            Contract.Ensures(Contract.Result<This>() != null);

            return this.DuplicateMe();
        }

        protected IType EvalFast(Variable v)
        {
            Contract.Ensures(Contract.Result<IType>() != null);

            IType result;
            if (this.TryGetValue(v, out result))
            {
                return result;
            }
            else
            {
                return IntervalUnknown;
            }
        }

        public virtual List<Pair<Variable, Int32>> IntConstants
        {
            get
            {
                return new List<Pair<Variable, int>>();
            }
        }

        public IEnumerable<Expression> LowerBoundsFor(Expression v, bool strict)
        {
            var res = new Set<Expression>();
            var val = this.BoundsFor(v);

            IExpressionEncoder<Variable, Expression> encoder;
            if (this.ExpressionManager.TryGetEncoder(out encoder) && val.IsNormal && !val.LowerBound.IsInfinity)
            {
                if (strict)
                {
                    Rational bound; // = strict ? val.LowerBound - 1 : val.LowerBound;
                    if (Rational.TrySub(val.LowerBound, Rational.For(1), out bound))
                    {
                        res.Add(bound.ToExpression(encoder));
                    }
                }
                else
                {
                    res.Add(val.LowerBound.ToExpression(encoder));
                }
            }

            return res;
        }
        public IEnumerable<Expression> LowerBoundsFor(Variable v, bool strict)
        {
            var res = new Set<Expression>();
            var val = this.BoundsFor(v);

            IExpressionEncoder<Variable, Expression> encoder;
            if (this.ExpressionManager.TryGetEncoder(out encoder) && val.IsNormal && !val.LowerBound.IsInfinity)
            {
                if (strict)
                {
                    Rational bound; // = strict ? val.LowerBound - 1 : val.LowerBound;
                    if (Rational.TrySub(val.LowerBound, Rational.For(1), out bound))
                    {
                        res.Add(bound.ToExpression(encoder));
                    }
                }
                else
                {
                    res.Add(val.LowerBound.ToExpression(encoder));
                }
            }

            return res;
        }

        public IEnumerable<Expression> UpperBoundsFor(Expression v, bool strict)
        {
            var val = this.BoundsFor(v);

            IExpressionEncoder<Variable, Expression> encoder;
            if (this.ExpressionManager.TryGetEncoder(out encoder) && val.IsNormal && !val.UpperBound.IsInfinity)
            {
                Rational bound; // = strict ? val.UpperBound + 1 : val.UpperBound;
                if (strict)
                {
                    if (Rational.TryAdd(1, val.UpperBound, out bound))
                    {
                        yield return bound.ToExpression(encoder);
                    }
                }
                else
                {
                    yield return val.UpperBound.ToExpression(encoder);
                }
            }
        }

        public IEnumerable<Expression> UpperBoundsFor(Variable v, bool strict)
        {
            var val = this.BoundsFor(v);

            IExpressionEncoder<Variable, Expression> encoder;
            if (this.ExpressionManager.TryGetEncoder(out encoder) && val.IsNormal && !val.UpperBound.IsInfinity)
            {
                Rational bound; // = strict ? val.UpperBound + 1 : val.UpperBound;
                if (strict)
                {
                    if (Rational.TryAdd(1, val.UpperBound, out bound))
                    {
                        yield return bound.ToExpression(encoder);
                    }
                }
                else
                {
                    yield return val.UpperBound.ToExpression(encoder);
                }
            }
        }

        public IEnumerable<Variable> EqualitiesFor(Variable v)
        {
            return new Set<Variable>();
        }

        virtual public This TestTrue(Expression guard)
        {
            Contract.Requires(guard != null);

            Contract.Ensures(Contract.Result<This>() != null);

            var res = this.TestNotEqualToZero(expManager.Decoder.UnderlyingVariable(guard));
            var visitor = new IntervalTestVisitor(expManager.Decoder);

            return visitor.VisitTrue(guard, (This)this);
        }

        virtual public This TestFalse(Expression guard)
        {
            Contract.Requires(guard != null);

            Contract.Ensures(Contract.Result<This>() != null);

            var res = this.TestEqualToZero(expManager.Decoder.UnderlyingVariable(guard));
            var visitor = new IntervalTestVisitor(expManager.Decoder);

            return visitor.VisitFalse(guard, (This)this);
        }

        public This TestTrueLessThan(Variable left, Variable right)
        {
            Contract.Ensures(Contract.Result<This>() != null);

            var kLeft = EvalFast(left);
            var kRight = EvalFast(right);

            this.AssumeKLessThanRight(kLeft, right);
            this.AssumeLeftLessThanK(left, kRight);

            return (This)this;
        }

        /// <summary>
        /// The handler for the expressions in the form of <code>e1 == e2</code>.
        /// The default is built invoking by transforming the code into <code>e1 &lt;= e2 &amp;&amp; e2 &lt;= e1</code>
        /// However, if no encoder is provided, then it evaluates e1, and e2, and then it tests that they are the same interval...
        /// </summary>
        public This TestTrueEqual(Expression e1, Expression e2)
        {
            Contract.Requires(e1 != null);
            Contract.Requires(e2 != null);

            Contract.Ensures(Contract.Result<This>() != null);

            var result = (This)this;

            #region 1. Try to see if it is a relational expression
            // If it is in the for "op(e11, e12) relSym k" where relSym is a relational symbol, and k a constant
            // Then it is a boolean expression, so we handle it as it deserves
            Int32 value;
            if (this.ExpressionManager.Decoder.OperatorFor(e1).IsRelationalOperator() && this.ExpressionManager.Decoder.IsConstantInt(e2, out value))
            {
                if (value == 0)
                { // that is !(e1)
                    result = this.TestFalse(e1);
                }
                else
                { // that is e1
                    result = this.TestTrue(e1);
                }
            }
            #endregion

            #region 2. Otherwise, handle the equality

            IExpressionEncoder<Variable, Expression> encoder;

            if (this.ExpressionManager.TryGetEncoder(out encoder))
            {
                // assume e1 <= e2; assume e2 <= e1
                return result.TestTrueLessEqualThan(e1, e2).TestTrueLessEqualThan(e2, e1);
            }
            else
            {
                var varFore1 = expManager.Decoder.UnderlyingVariable(e1);
                var varFore2 = expManager.Decoder.UnderlyingVariable(e2);

                if (this.ContainsKey(varFore1))
                {
                    var resultTmp = this.DuplicateMe();

                    var newVal = Eval(e1).Meet(Eval(e2));
                    resultTmp[varFore1] = newVal;
                    resultTmp[varFore2] = newVal;

                    result = resultTmp;
                }
                else if (this.ExpressionManager.Decoder.IsVariable(e1) || this.ExpressionManager.Decoder.IsWritableBytes(e1)) // it is in the form of "x == exp"
                {
                    var intervalForE2 = Eval(e2);

                    if (!intervalForE2.IsTop)
                    { // Avoid putting top in the environment
                        this[varFore1] = intervalForE2;
                    }
                }
                else if (this.ExpressionManager.Decoder.OperatorFor(e1).IsConversionOperator())
                { // if it is in the form of "(cast) e1 == e2"
                    result = TestTrueEqual(this.ExpressionManager.Decoder.LeftExpressionFor(e1), e2);
                }
                else if (this.ExpressionManager.Decoder.IsConstant(e1) && this.ExpressionManager.Decoder.IsConstant(e2))
                {
                    var tmpRes = Eval(e1).Meet(Eval(e2));
                    if (tmpRes.IsBottom)
                    {
                        return Bottom;
                    }
                }

                return result;
            }
            #endregion
        }

        /// <summary>
        /// Handler for expression in the form of <code>e1 != e2</code>
        /// </summary>
        /// <returns>by default, it does <code>e1 &lt; e2 ||  e2 &lt; e1</code></returns>
        virtual public This TestNotEqual(Expression e1, Expression e2)
        {
            Contract.Requires(e1 != null);
            Contract.Requires(e2 != null);

            #region 1. Try to see if it is a relational operator
            int value;
            // If it is in the for "op(e11, e12) relSym k" where relSym is a relational symbol, and k a constant 
            if (this.ExpressionManager.Decoder.OperatorFor(e1).IsRelationalOperator() && this.ExpressionManager.Decoder.IsConstantInt(e2, out value))
            {
                if (value == 0)
                { // that is !!(e1)
                    return this.TestTrue(e1);
                }
                else
                { // that is !e1
                    return this.TestFalse(e1);
                }
            }
            #endregion

            #region 2. Otherwise handle the inequality by two LT operations
            var adom1 = this.DuplicateMe();
            var adom2 = this.DuplicateMe();

            var l1 = adom1.TestTrueLessThan(e1, e2);
            var l2 = adom2.TestTrueLessThan(e2, e1);

            return l1.Join(l2);
            #endregion
        }

        [ContractVerification(false)]
        protected override string ToLogicalFormula(Variable d, IType c)
        {
            if (!c.IsNormal())
            {
                return null;
            }

            var result = new List<string>();

            if (c.IsSingleton)
            {
                result.Add(ExpressionPrinter.ToStringBinary(ExpressionOperator.Equal, ExpressionPrinter.ToString(d, expManager.Decoder), c.LowerBound.ToString()));
            }
            else if (!c.IsLowerBoundMinusInfinity)
            {
                result.Add(ExpressionPrinter.ToStringBinary(ExpressionOperator.LessEqualThan, c.LowerBound.ToString(), ExpressionPrinter.ToString(d, expManager.Decoder)));
            }
            else if (!c.IsUpperBoundPlusInfinity)
            {
                result.Add(ExpressionPrinter.ToStringBinary(ExpressionOperator.LessEqualThan, ExpressionPrinter.ToString(d, expManager.Decoder), c.UpperBound.ToString()));
            }

            return ExpressionPrinter.ToLogicalFormula(ExpressionOperator.LogicalAnd, result);
        }

        protected override T To<T>(Variable d, IType c, IFactory<T> factory)
        {
            if (c.IsTop)
                return factory.IdentityForAnd;
            if (c.IsBottom)
                return factory.Constant(false);

            NType r;
            if (c.TryGetSingletonValue(out r))
            {
                return factory.EqualTo(factory.Variable(d), To(r, factory));
            }

            T left = c.IsLowerBoundMinusInfinity
                ? factory.IdentityForAnd
                : factory.LessEqualThan(To(c.LowerBound, factory), factory.Variable(d));


            T right = c.IsUpperBoundPlusInfinity
                ? factory.IdentityForAnd
                : factory.LessEqualThan(factory.Variable(d), To(c.UpperBound, factory));

            return factory.And(left, right);
        }
        #endregion

        #region Proxies to please the compiler
        INumericalAbstractDomain<Variable, Expression> INumericalAbstractDomain<Variable, Expression>.RemoveRedundanciesWith(INumericalAbstractDomain<Variable, Expression> oracle)
        {
            return this.RemoveRedundanciesWith(oracle);
        }

        IAbstractDomainForEnvironments<Variable, Expression> IPureExpressionTest<Variable, Expression>.TestTrue(Expression guard)
        {
            return this.TestTrue(guard);
        }

        INumericalAbstractDomain<Variable, Expression> INumericalAbstractDomain<Variable, Expression>.TestTrueGeqZero(Expression exp)
        {
            return this.TestTrueGeqZero(exp);
        }

        INumericalAbstractDomain<Variable, Expression> INumericalAbstractDomain<Variable, Expression>.TestTrueLessThan(Expression exp1, Expression exp2)
        {
            return this.TestTrueLessThan(exp1, exp2);
        }

        INumericalAbstractDomain<Variable, Expression> INumericalAbstractDomain<Variable, Expression>.TestTrueLessEqualThan(Expression exp1, Expression exp2)
        {
            return this.TestTrueLessEqualThan(exp1, exp2);
        }

        INumericalAbstractDomain<Variable, Expression> INumericalAbstractDomain<Variable, Expression>.TestTrueEqual(Expression exp1, Expression exp2)
        {
            return this.TestTrueEqual(exp1, exp2);
        }

        IAbstractDomainForEnvironments<Variable, Expression> IPureExpressionTest<Variable, Expression>.TestFalse(Expression guard)
        {
            return this.TestFalse(guard);
        }

        public override object Clone()
        {
            return this.DuplicateMe();
        }

        #endregion

        #region TryGetValue
        public bool TryGetValue(Variable var, bool isSigned, out IType intv)
        {
            if (this.TryGetValue(var, out intv))
            {
                Contract.Assert(intv != null);
                intv = isSigned ? intv : intv.ToUnsigned();

                return true;
            }

            return false;
        }

        #endregion

        #region Privates

        private void RefineWithPrestate(Expression x, Expression exp, INumericalAbstractDomainQuery<Variable, Expression> preState)
        {
            Contract.Requires(preState != null);

            var refinedInterval = ConvertInterval(preState.BoundsFor(exp).AsInterval);

            if (refinedInterval.IsNormal())
            {
                var xExp = expManager.Decoder.UnderlyingVariable(x);

                this.RefineWith(xExp, refinedInterval);
            }
        }

        private FlatAbstractDomain<bool> CheckIfGreaterEqualThanZero(Polynomial<Variable, Expression> pol)
        {
            if (pol.Relation != null)
            {
                return CheckOutcome.Top;
            }

            var result = this.EvalPolynomial(pol);

            if (result.IsBottom || result.IsTop)
            {
                return CheckOutcome.Top;
            }
            if (this.IsGreaterEqualThanZero(result.LowerBound))
            {
                return CheckOutcome.True;
            }
            else if (this.IsLessThanZero(result.UpperBound))
            {
                return CheckOutcome.False;
            }
            else
            {
                return CheckOutcome.Top;
            }
        }

        protected void ToUInt(ref IType v)
        {
            Contract.Requires(v != null);

            v = ApplyConversion(ExpressionOperator.ConvertToUInt32, v);
        }

        protected void ToUInt(ref IType v1, ref IType v2)
        {
            Contract.Requires(v1 != null);
            Contract.Requires(v2 != null);

            Contract.Ensures(Contract.ValueAtReturn(out v1) != null);
            Contract.Ensures(Contract.ValueAtReturn(out v2) != null);

            ToUInt(ref v1);
            ToUInt(ref v2);
            Contract.Assume(v1 != null);
            Contract.Assume(v2 != null);
        }

        #endregion

        #region ToString
        override public string ToString()
        {
            string result;

            if (this.IsBottom)
            {
                result = "_|_";
            }
            else if (this.IsTop)
            {
                result = "Top";
            }
            else
            {
                var lines = new List<string>();

                foreach (var x in this.Keys)
                {
                    // REDUNDANT inferred by CC
                    /*          var xAsString = this.expManager.Decoder != null ? this.expManager.Decoder.NameOf(x) : x.ToString(); */

                    var xAsString = expManager.Decoder.NameOf(x);

                    if (!this[x].IsTop)
                    {
                        lines.Add(xAsString + ": " + this[x]);
                    }
                }

                lines.Sort();

                var buff = new StringBuilder();

                if (lines.Count >= 2)
                {
                    for (var i = 0; i < lines.Count - 1; i++)
                    {
                        buff.AppendFormat("{0}, ", lines[i]);
                    }
                }

                if (lines.Count >= 1)
                    buff.AppendFormat(lines[lines.Count - 1]);

                result = buff.ToString();
            }

            return result;
        }

        public string ToString(Expression exp)
        {
            //if (this.expManager.Decoder != null)
            {
                return ExpressionPrinter.ToString(exp, expManager.Decoder);
            }
            /*      else
                  {
                    return "< missing expression decoder >";
                  }
             */
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
        protected class IntervalTestVisitor
        {
            [ContractInvariantMethod]
            private void ObjectInvariant()
            {
                Contract.Invariant(ttVisitor != null);
                Contract.Invariant(tfVisitor != null);
            }

            readonly private IntervalsTrueTestVisitor ttVisitor;
            readonly private IntervalsFalseTestVisitor tfVisitor;

            public IntervalTestVisitor(IExpressionDecoder<Variable, Expression> decoder)
            {
                ttVisitor = new IntervalsTrueTestVisitor(decoder);
                tfVisitor = new IntervalsFalseTestVisitor(decoder);

                ttVisitor.FalseVisitor = tfVisitor;
                tfVisitor.TrueVisitor = ttVisitor;
            }

            internal This VisitTrue(Expression guard, This domain)
            {
                Contract.Requires(guard != null);
                Contract.Ensures(Contract.Result<This>() != null);

                return ttVisitor.Visit(guard, domain);
            }

            internal This VisitFalse(Expression guard, This domain)
            {
                Contract.Requires(guard != null);
                Contract.Ensures(Contract.Result<This>() != null);

                return tfVisitor.Visit(guard, domain);
            }

            private class IntervalsTrueTestVisitor :
              TestTrueVisitor<This, Variable, Expression>
            {
                public IntervalsTrueTestVisitor(IExpressionDecoder<Variable, Expression> decoder)
                  : base(decoder)
                { }


                /// <summary>
                /// We override the visitor as in some cases we want to capture information at an higher level.
                /// For instance for
                ///   \gt( -( +( sv7 (7), sv10 (10) ), sv48 (107) ), 0 )
                /// we want to associate to the information ( -( +( sv7 (7), sv10 (10)), sv48 (107)) \in [1, +oo]
                /// </summary>
                protected override This DispatchCompare(GenericExpressionVisitor<This, This, Variable, Expression>.CompareVisitor cmp, Expression left, Expression right, Expression original, This data)
                {
                    data = cmp(left, right, original, data);
                    return base.DispatchCompare(cmp, left, right, original, data);
                }

                public override This VisitEqual(Expression left, Expression right, Expression original, This data)
                {
                    return data.TestTrueEqual(left, right);
                }

                public override This VisitEqual_Obj(Expression left, Expression right, Expression original, This data)
                {
                    return this.VisitEqual(left, right, original, data);
                }

                public override This VisitLessEqualThan_Un(Expression left, Expression right, Expression original, This data)
                {
                    return data.TestTrueLessEqualThan_Un(left, right);
                }

                private bool AreFloat(Expression left, Expression right)
                {
                    var ltype = this.Decoder.TypeOf(left);
                    if (ltype == ExpressionType.Float32 || ltype == ExpressionType.Float64)
                        return true;

                    var rtype = this.Decoder.TypeOf(right);

                    return rtype == ExpressionType.Float32 || rtype == ExpressionType.Float64;
                }

                public override This VisitLessEqualThan(Expression left, Expression right, Expression original, This data)
                {
                    return data.TestTrueLessEqualThan(left, right);
                }

                public override This VisitLessThan(Expression left, Expression right, Expression original, This data)
                {
                    return data.TestTrueLessThan(left, right);
                }

                public override This VisitLessThan_Un(Expression left, Expression right, Expression original, This data)
                {
                    return data.TestTrueLessThan_Un(left, right);
                }

                public override This VisitAddition(Expression left, Expression right, Expression original, This data)
                {
                    data = base.VisitAddition(left, right, original, data);
                    return data.TestNotEqualToZero(original);
                }

                public override This VisitAnd(Expression left, Expression right, Expression original, This data)
                {
                    data = base.VisitAnd(left, right, original, data);
                    return data.TestNotEqualToZero(original);
                }

                public override This VisitDivision(Expression left, Expression right, Expression original, This data)
                {
                    data = base.VisitDivision(left, right, original, data);
                    return data.TestNotEqualToZero(original);
                }

                public override This VisitMultiplication(Expression left, Expression right, Expression original, This data)
                {
                    data = base.VisitMultiplication(left, right, original, data);
                    return data.TestNotEqualToZero(original);
                }

                public override This VisitShiftLeft(Expression left, Expression right, Expression original, This data)
                {
                    data = base.VisitShiftLeft(left, right, original, data);
                    return data.TestNotEqualToZero(original);
                }

                public override This VisitUnknown(Expression left, This data)
                {
                    data = base.VisitUnknown(left, data);
                    return data.TestNotEqualToZero(left);
                }

                public override This VisitUnaryMinus(Expression left, Expression original, This data)
                {
                    data = base.VisitUnaryMinus(left, original, data);
                    return data.TestNotEqualToZero(original);
                }

                public override This VisitOr(Expression left, Expression right, Expression original, This data)
                {
                    data = base.VisitOr(left, right, original, data);
                    return data.TestNotEqualToZero(original);
                }

                public override This VisitNot(Expression left, This data)
                {
                    var ttVisitor = new IntervalsTrueTestVisitor(data.ExpressionManager.Decoder);
                    var tfVisitor = new IntervalsFalseTestVisitor(data.ExpressionManager.Decoder);

                    ttVisitor.FalseVisitor = tfVisitor;
                    tfVisitor.TrueVisitor = ttVisitor;

                    return tfVisitor.Visit(left, data);
                }

                public override This VisitNotEqual(Expression left, Expression right, Expression original, This data)
                {
                    return data.TestNotEqual(left, right);
                }

                public override This VisitVariable(Variable var, Expression original, This data)
                {
                    return data.TestNotEqualToZero(original);
                }

                // left - right != 0
                public override This VisitSubtraction(Expression left, Expression right, Expression original, This data)
                {
                    data = data.TestNotEqualToZero(original);
                    return data.TestNotEqual(left, right);
                }

                // left % right != 0
                public override This VisitModulus(Expression left, Expression right, Expression original, This data)
                {
                    // refine the interval by removing zero
                    if (data.CheckIfGreaterEqualThanZero(left).IsTrue())
                    {
                        data = data.TestNotEqualToZero(left);
                    }
                    data = base.VisitModulus(left, right, original, data);

                    return data.TestNotEqualToZero(original);
                }

                public override This VisitXor(Expression left, Expression right, Expression original, This data)
                {
                    data = base.VisitXor(left, right, original, data);

                    return data.TestNotEqualToZero(original);
                }
            }

            private class IntervalsFalseTestVisitor :
              TestFalseVisitor<This, Variable, Expression>
            {
                public IntervalsFalseTestVisitor(IExpressionDecoder<Variable, Expression> decoder)
                  : base(decoder)
                {
                }

                public override This Visit(Expression exp, This data)
                {
                    Contract.Ensures(Contract.Result<This>() != null);

                    var result = base.Visit(exp, data);

                    Contract.Assert(result != null);

                    if (this.Decoder.IsBinaryExpression(exp))
                    {
                        var leftExp = this.Decoder.LeftExpressionFor(exp);
                        var rightExp = this.Decoder.RightExpressionFor(exp);

                        var k = data.Eval(rightExp);

                        if (k.IsBottom)
                        {
                            return result.Bottom;
                        }

                        if (!k.IsSingleton)
                        {
                            return result;    // No more work to be done ...
                        }

                        switch (this.Decoder.OperatorFor(exp))
                        {
                            case ExpressionOperator.Multiplication:
                            case ExpressionOperator.Division:
                                // ignored
                                break;

                            case ExpressionOperator.GreaterEqualThan:
                            case ExpressionOperator.GreaterEqualThan_Un:
                                {
                                    // !(left >= [a,b]) -> left < [a,b]
                                    var leftExpVar = this.Decoder.UnderlyingVariable(leftExp);
                                    result.AssumeLeftLessThanK(leftExpVar, k);
                                }
                                break;

                            case ExpressionOperator.GreaterThan:
                            case ExpressionOperator.GreaterThan_Un:
                                {
                                    // !(left > [a,b]) -> left <= [a,b]
                                    var leftExpVar = this.Decoder.UnderlyingVariable(leftExp);
                                    result.AssumeLeftLessEqualThanK(leftExpVar, k);
                                }
                                break;

                            case ExpressionOperator.LessEqualThan:
                            case ExpressionOperator.LessEqualThan_Un:
                                {
                                    // !(left <= [a,b]) -> left > [a, b]
                                    var leftExpVar = this.Decoder.UnderlyingVariable(leftExp);
                                    result.AssumeKLessThanRight(k, leftExpVar);
                                }
                                break;

                            case ExpressionOperator.LessThan:
                            case ExpressionOperator.LessThan_Un:
                                {
                                    // !(left < [a,b]) -> left >= [a, b]
                                    var leftExpVar = this.Decoder.UnderlyingVariable(leftExp);
                                    result.AssumeKLessEqualThanRight(k, leftExpVar);
                                }
                                break;
                        }
                    }

                    return result;
                }

                public override This VisitEqual_Obj(Expression left, Expression right, Expression original, This data)
                {
                    return this.VisitEqual(left, right, original, data);
                }

                public override This VisitLessEqualThan(Expression left, Expression right, Expression original, This data)
                {
                    return data.TestTrueLessThan(right, left);
                }

                public override This VisitLessThan(Expression left, Expression right, Expression original, This data)
                {
                    return data.TestTrueLessEqualThan(right, left);
                }

                public override This VisitNot(Expression left, This data)
                {
                    return this.TrueVisitor.Visit(left, data);
                }

                public override This VisitNotEqual(Expression left, Expression right, Expression original, This data)
                {
                    return this.TrueVisitor.VisitEqual(left, right, original, data);
                }

                public override This VisitVariable(Variable var, Expression original, This data)
                {
                    data[var] = data.Eval(original).Meet(data.IntervalZero);      // guard : [0, 0]
                    return data;
                }
            }
        }

        #region Visitor for CheckIfHolds
        protected class IntervalsCheckIfHoldsVisitor :
          CheckIfHoldsVisitor<This, Variable, Expression>
        {
            public IntervalsCheckIfHoldsVisitor(IExpressionDecoder<Variable, Expression> decoder)
              : base(decoder)
            {
                Contract.Requires(decoder != null);
            }

            public override FlatAbstractDomain<bool> VisitConstant(Expression left, FlatAbstractDomain<bool> data)
            {
                var valForLeft = this.Domain.Eval(left);
                NType v;

                if (valForLeft.TryGetSingletonValue(out v))
                {
                    Contract.Assume(v != null);
                    return this.Domain.IsNotZero(v) ? CheckOutcome.True : CheckOutcome.False;
                }
                else
                {
                    return CheckOutcome.Top;
                }
            }

            public override FlatAbstractDomain<bool> VisitEqual(Expression left, Expression right, Expression original, FlatAbstractDomain<bool> data)
            {
                if (this.Decoder.IsNaN(left) || this.Decoder.IsNaN(right))
                {
                    return CheckOutcome.False;
                }

                return this.VisitEqual_Internal(left, right);
            }

            public override FlatAbstractDomain<bool> VisitEqual_Obj(Expression left, Expression right, Expression original, FlatAbstractDomain<bool> data)
            {
                if (this.Decoder.IsNaN(left) && this.Decoder.IsNaN(right))
                {
                    return CheckOutcome.True;
                }

                return this.VisitEqual_Internal(left, right);
            }

            public override FlatAbstractDomain<bool> VisitLessEqualThan(Expression left, Expression right, Expression original, FlatAbstractDomain<bool> data)
            {
                return this.Domain.CheckIfLessEqualThan(left, right);
            }

            public override FlatAbstractDomain<bool> VisitLessEqualThan_Un(Expression left, Expression right, Expression original, FlatAbstractDomain<bool> data)
            {
                return this.Domain.CheckIfLessEqualThan_Un(left, right);
            }

            public override FlatAbstractDomain<bool> VisitLessThan(Expression left, Expression right, Expression original, FlatAbstractDomain<bool> data)
            {
                return this.Domain.CheckIfLessThan(left, right);
            }

            public override FlatAbstractDomain<bool> VisitLessThan_Un(Expression left, Expression right, Expression original, FlatAbstractDomain<bool> data)
            {
                return this.Domain.CheckIfLessThan_Un(left, right);
            }

            public override FlatAbstractDomain<bool> VisitNot(Expression left, FlatAbstractDomain<bool> data)
            {
                FlatAbstractDomain<bool> leftHolds = this.Visit(left, data);

                if (leftHolds.IsNormal())
                {
                    return new FlatAbstractDomain<bool>(!leftHolds.BoxedElement);
                }
                else
                {
                    return leftHolds;
                }
            }

            public override FlatAbstractDomain<bool> VisitNotEqual(Expression left, Expression right, Expression original, FlatAbstractDomain<bool> data)
            {
                long value; // We use long because we do not want Int32.MinValue to be wrapped to zero

                if (this.Decoder.TryValueOf<long>(right, ExpressionType.Int64, out value) && value == 0)
                {
                    // left != 0 => we should check if left holds
                    var direct = this.Visit(left, data);

                    if (!direct.IsTop)
                    {
                        return direct;
                    }
                }

                var areEqual = this.VisitEqual(left, right, original, data);

                if (areEqual.IsNormal())
                {
                    return new FlatAbstractDomain<bool>(!areEqual.BoxedElement);
                }
                else
                {
                    return areEqual;
                }
            }

            public override FlatAbstractDomain<bool> VisitVariable(Variable var, Expression original, FlatAbstractDomain<bool> data)
            {
                var intv = this.Domain.Eval(original);

                if (intv.IsTop)
                    return CheckOutcome.Top;

                if (intv.IsBottom)
                    return CheckOutcome.Bottom;

                if (!intv.IsNormal)
                    return CheckOutcome.Top;

                if (this.Domain.IsGreaterThanZero(intv.LowerBound) || this.Domain.IsLessThanZero(intv.UpperBound))
                    return CheckOutcome.True;

                if (intv.IsSingleton && this.Domain.IsZero(intv.LowerBound))
                    return CheckOutcome.False;

                return CheckOutcome.Top;
            }

            private FlatAbstractDomain<bool> VisitEqual_Internal(Expression left, Expression right)
            {
                var leftType = this.Decoder.TypeOf(left);
                var rightType = this.Decoder.TypeOf(right);

                if (!leftType.IsFloatingPointType() && !rightType.IsFloatingPointType() && left.Equals(right))
                {
                    return CheckOutcome.True;
                }

                var leftInt = this.Domain.Eval(left);
                var rightInt = this.Domain.Eval(right);

                if (leftInt.IsSingleton && rightInt.IsSingleton && leftInt.LessEqual(rightInt))
                {
                    return CheckOutcome.True;
                }

                if (!leftInt.IsLowerBoundMinusInfinity && !leftInt.IsUpperBoundPlusInfinity && !rightInt.IsLowerBoundMinusInfinity && !rightInt.IsUpperBoundPlusInfinity &&
                  leftInt.LowerBound.Equals(rightInt.LowerBound) && leftInt.UpperBound.Equals(rightInt.UpperBound) && leftInt.LowerBound.Equals(leftInt.UpperBound))
                {
                    return CheckOutcome.True;
                }

                if ((!leftInt.IsUpperBoundPlusInfinity && !rightInt.IsLowerBoundMinusInfinity && this.Domain.IsLessThan(leftInt.UpperBound, rightInt.LowerBound))
                  || (!rightInt.IsUpperBoundPlusInfinity && !leftInt.IsLowerBoundMinusInfinity && this.Domain.IsLessThan(rightInt.UpperBound, leftInt.LowerBound)))
                {
                    return CheckOutcome.False;
                }


                var op = this.Decoder.OperatorFor(left);

                int value;

                if (this.Decoder.IsConstantInt(right, out value) && value == 0)
                {
                    if (op.IsLessThan())
                    {
                        // it is (a < b) == 0, that is !(a < b) that is b <= a
                        var valForA = this.Domain.Eval(this.Decoder.LeftExpressionFor(left));
                        var valForB = this.Domain.Eval(this.Decoder.RightExpressionFor(left));

                        if (this.Domain.IsLessEqualThan(valForB.UpperBound, valForA.LowerBound))
                        {
                            return CheckOutcome.True;
                        }
                    }

                    if (op.IsLessEqualThan())
                    {
                        // it is (a <= b) == 0, that is !(a <= b) that is b > a
                        var valForA = this.Domain.Eval(this.Decoder.LeftExpressionFor(left));
                        var valForB = this.Domain.Eval(this.Decoder.RightExpressionFor(left));

                        if (this.Domain.IsLessThan(valForA.UpperBound, valForB.LowerBound))
                        {
                            return CheckOutcome.True;
                        }
                    }

                    if (op.IsGreaterThan())
                    {
                        // it is (a > b) == 0, that is !(a > b) that is b >= a
                        var valForA = this.Domain.Eval(this.Decoder.LeftExpressionFor(left));
                        var valForB = this.Domain.Eval(this.Decoder.RightExpressionFor(left));

                        if (this.Domain.IsLessEqualThan(valForA.UpperBound, valForB.LowerBound))
                        {
                            return CheckOutcome.True;
                        }
                    }

                    if (op.IsGreaterEqualThan())
                    {
                        // it is (a >= b) == 0, that is !(a >= b) that is b < a
                        var valForA = this.Domain.Eval(this.Decoder.LeftExpressionFor(left));
                        var valForB = this.Domain.Eval(this.Decoder.RightExpressionFor(left));

                        if (this.Domain.IsLessThan(valForB.UpperBound, valForA.LowerBound))
                        {
                            return CheckOutcome.True;
                        }
                    }

                    if ((op == ExpressionOperator.Equal || op == ExpressionOperator.Equal_Obj))
                    {
                        // it is (a == b) == 0, that is !(a == b) that is b != a
                        var valForA = this.Domain.Eval(this.Decoder.LeftExpressionFor(left));
                        var valForB = this.Domain.Eval(this.Decoder.RightExpressionFor(left));

                        Contract.Assert(valForA != null);
                        Contract.Assert(valForB != null);

                        if ((valForA.Meet(valForB)).IsBottom)
                        { // a \sqcap b == _|_  implies that they are different
                            return CheckOutcome.True;
                        }

                        if (valForA.IsNormal() && valForB.IsNormal() && valForA.LessEqual(valForB) && valForB.LessEqual(valForA))
                        {
                            return CheckOutcome.True;
                        }
                    }
                }

                return CheckOutcome.Top;
            }

            protected override FlatAbstractDomain<bool> Default(FlatAbstractDomain<bool> data)
            {
                return CheckOutcome.Top;
            }
        }
        #endregion

        private class EvalExpressionVisitor
          : GenericExpressionVisitor<Pair<This, Int32>, IType, Variable, Expression>
        {
            [ContractInvariantMethod]
            private void ObjectInvariant()
            {
                Contract.Invariant(evalConstantVisitor != null);
                Contract.Invariant(occurrences != null);
            }

            #region Private state

            private readonly EvalConstantVisitor evalConstantVisitor;
            private readonly VariableOccurrences occurrences;

            #endregion

            #region Constructor
            public EvalExpressionVisitor(IExpressionDecoder<Variable, Expression> decoder)
              : base(decoder)
            {
                Contract.Requires(decoder != null);

                evalConstantVisitor = new EvalConstantVisitor(decoder);
                occurrences = new VariableOccurrences(decoder);
            }
            #endregion

            public List<Variable> DuplicatedOccurrences
            {
                get
                {
                    Contract.Ensures(Contract.Result<List<Variable>>() != null);

                    return occurrences.DuplicatedOccurrences;
                }
            }

            #region Entrypoint for the visit
            public override IType Visit(Expression exp, Pair<This, Int32> data)
            {
                Contract.Ensures(Contract.Result<IType>() != null);

                if (data.Two >= IntervalEnvironment_Base<This, Variable, Expression, IType, NType>.MAXDEPTH)
                {
                    return data.One.IntervalUnknown;
                }

                var result = base.Visit(exp, PlusOne(data));

                /*
                if (result == null)
                {
                  return data.One.IntervalUnknown;
                }
                */
                result = RefineWithTypeRange(result, exp, data.One);

                var expVar = this.Decoder.UnderlyingVariable(exp);
                IType prev_result;
                if (data.One.TryGetValue(expVar, out prev_result))
                {
                    result = result.Meet(prev_result);
                    Contract.Assert(result != null);
                }

                return result;
            }
            #endregion

            #region Visits
            public override IType VisitAddition(Expression left, Expression right, Expression original, Pair<This, Int32> data)
            {
                occurrences.Add(left, right);

                var plusOne = PlusOne(data);
                var leftIntv = Visit(left, plusOne);
                var rightIntv = Visit(right, plusOne);

                var sum = data.One.Interval_Add(leftIntv, rightIntv);

                // If we know the type, we can refine the result
                if (Decoder.TypeOf(left) == ExpressionType.Int32 || Decoder.TypeOf(right) == ExpressionType.Int32)
                {
                    // <0 + MaxInt32 = [-1, +oo]
                    if (data.One.IsLessEqualThanZero(leftIntv) && data.One.IsMaxInt32(rightIntv))
                    {
                        sum = sum.Meet(data.One.IntervalGreaterEqualThanMinusOne);
                    }
                }

                return sum;
            }

            public override IType VisitAnd(Expression left, Expression right, Expression original, Pair<This, Int32> data)
            {
                occurrences.Add(left, right);

                var plusOne = PlusOne(data);
                var leftIntv = Visit(left, plusOne);
                var rightIntv = Visit(right, plusOne);

                if (data.One.IsGreaterThanZero(rightIntv.LowerBound))
                {
                    return data.One.Interval_BitwiseAnd(leftIntv, rightIntv);
                }
                else
                {
                    return data.One.IntervalUnknown;
                }
            }

            public override IType VisitConstant(Expression left, Pair<This, Int32> data)
            {
                return evalConstantVisitor.Visit(left, data.One);
            }

            public override IType VisitConvertToInt8(Expression left, Expression original, Pair<This, int> data)
            {
                occurrences.Add(left);

                return data.One.ApplyConversion(ExpressionOperator.ConvertToInt8, Visit(left, PlusOne(data)));
            }

            public override IType VisitConvertToInt32(Expression left, Expression original, Pair<This, Int32> data)
            {
                occurrences.Add(left);

                return data.One.ApplyConversion(ExpressionOperator.ConvertToInt32, Visit(left, PlusOne(data)));
            }

            public override IType VisitConvertToUInt16(Expression left, Expression original, Pair<This, Int32> data)
            {
                occurrences.Add(left);

                return data.One.ApplyConversion(ExpressionOperator.ConvertToUInt16, Visit(left, PlusOne(data)));
            }

            public override IType VisitConvertToUInt32(Expression left, Expression original, Pair<This, Int32> data)
            {
                occurrences.Add(left);

                return data.One.ApplyConversion(ExpressionOperator.ConvertToUInt32, Visit(left, PlusOne(data)));
            }

            public override IType VisitConvertToFloat64(Expression p, Expression original, Pair<This, int> data)
            {
                occurrences.Add(p);

                return data.One.ApplyConversion(ExpressionOperator.ConvertToFloat64, Visit(p, PlusOne(data)));
            }

            public override IType VisitConvertToUInt8(Expression left, Expression original, Pair<This, Int32> data)
            {
                occurrences.Add(left);

                return data.One.ApplyConversion(ExpressionOperator.ConvertToUInt8, Visit(left, PlusOne(data)));
            }

            public override IType VisitConvertToFloat32(Expression left, Expression original, Pair<This, int> data)
            {
                occurrences.Add(left);

                return data.One.ApplyConversion(ExpressionOperator.ConvertToFloat32, Visit(left, PlusOne(data)));
            }

            public override IType VisitDivision(Expression left, Expression right, Expression original, Pair<This, Int32> data)
            {
                occurrences.Add(left, right);

                var plusOne = PlusOne(data);

                var leftIntv = Visit(left, plusOne);
                var rightIntv = Visit(right, plusOne);

                return data.One.Interval_Div(leftIntv, rightIntv);
            }

            public override IType VisitEqual(Expression left, Expression right, Expression original, Pair<This, Int32> data)
            {
                occurrences.Add(left, right);

                return data.One.IntervalUnknown;
            }

            public override IType VisitGreaterEqualThan(Expression left, Expression right, Expression original, Pair<This, Int32> data)
            {
                occurrences.Add(left, right);

                return data.One.IntervalUnknown;
            }

            public override IType VisitGreaterThan(Expression left, Expression right, Expression original, Pair<This, Int32> data)
            {
                occurrences.Add(left, right);

                return data.One.IntervalUnknown;
            }

            public override IType VisitLessEqualThan(Expression left, Expression right, Expression original, Pair<This, Int32> data)
            {
                occurrences.Add(left, right);

                return data.One.IntervalUnknown;
            }

            public override IType VisitLessThan(Expression left, Expression right, Expression original, Pair<This, Int32> data)
            {
                occurrences.Add(left, right);

                return data.One.IntervalUnknown;
            }

            public override IType VisitModulus(Expression left, Expression right, Expression original, Pair<This, Int32> data)
            {
                occurrences.Add(left, right);

                var plusOne = PlusOne(data);

                var leftIntv = Visit(left, plusOne);
                var rightIntv = Visit(right, plusOne);

                var result = data.One.Interval_Rem(leftIntv, rightIntv);

                if (AreCoPrimes(left, right))
                {
                    result = result.Meet(data.One.Interval_StrictlyPositive);
                }

                return result;
            }

            // [SuppressMessage("Microsoft.Contracts", "TestAlwaysEvaluatingToAConstant")]
            private bool AreCoPrimes(Expression l, Expression r)
            {
                int rVal;
                // Try to match (x * k) % r
                if (this.Decoder.IsConstantInt(r, out rVal) && rVal != 0 &&
                  this.Decoder.IsBinaryExpression(l) && this.Decoder.OperatorFor(l) == ExpressionOperator.Multiplication)
                {
                    var ll = this.Decoder.LeftExpressionFor(l);
                    var lr = this.Decoder.RightExpressionFor(l);

                    int llVal, lrVal;

                    var bll = this.Decoder.IsConstantInt(ll, out llVal);
                    var blr = this.Decoder.IsConstantInt(lr, out lrVal);

                    // Review: Why blr && blr ??? cccheck complains about blr==false being constant
                    if ((!bll && !blr) || (bll && blr) || llVal <= 0 || rVal <= 0)
                    {
                        return false;
                    }

                    if (bll)
                    {
                        return Rational.GCD(llVal, rVal) == 1;
                    }
                    else
                    {
                        return Rational.GCD(lrVal, rVal) == 1;
                    }
                }

                return false;
            }

            public override IType VisitMultiplication(Expression left, Expression right, Expression original, Pair<This, Int32> data)
            {
                occurrences.Add(left, right);

                var plusOne = PlusOne(data);

                var leftIntv = Visit(left, plusOne);
                var rightIntv = Visit(right, plusOne);

                return data.One.Interval_Mul(leftIntv, rightIntv);
            }

            public override IType VisitNot(Expression left, Pair<This, Int32> data)
            {
                occurrences.Add(left);

                return data.One.Interval_Not(Visit(left, PlusOne(data)));
            }

            public override IType VisitNotEqual(Expression left, Expression right, Expression original, Pair<This, Int32> data)
            {
                occurrences.Add(left, right);

                return data.One.IntervalUnknown;
            }

            public override IType VisitOr(Expression left, Expression right, Expression original, Pair<This, Int32> data)
            {
                occurrences.Add(left, right);

                var plusOne = PlusOne(data);

                var leftIntv = Visit(left, plusOne);
                var rightIntv = Visit(right, plusOne);

                return data.One.Interval_BitwiseOr(leftIntv, rightIntv);
            }

            public override IType VisitShiftLeft(Expression left, Expression right, Expression original, Pair<This, Int32> data)
            {
                occurrences.Add(left, right);

                var plusOne = PlusOne(data);

                var leftIntv = Visit(left, plusOne);
                var rightIntv = Visit(right, plusOne);

                return data.One.Interval_ShiftLeft(leftIntv, rightIntv);
            }

            public override IType VisitShiftRight(Expression left, Expression right, Expression original, Pair<This, Int32> data)
            {
                occurrences.Add(left, right);

                var plusOne = PlusOne(data);

                var leftIntv = Visit(left, plusOne);
                var rightIntv = Visit(right, plusOne);

                return data.One.Interval_ShiftRight(leftIntv, rightIntv);
            }

            public override IType VisitSizeOf(Expression sizeofExp, Pair<This, Int32> data)
            {
                occurrences.Add(sizeofExp);

                int i;
                if (this.Decoder.TrySizeOf(sizeofExp, out i))
                    return data.One.For(i);
                else
                    return data.One.Interval_Positive; // This is an upper approximation as we do not consider the size of the actual type 
            }

            public override IType VisitSubtraction(Expression left, Expression right, Expression original, Pair<This, Int32> data)
            {
                occurrences.Add(left, right);

                var plusOne = PlusOne(data);

                var leftIntv = Visit(left, plusOne);
                var rightIntv = Visit(right, plusOne);

                return data.One.Interval_Sub(leftIntv, rightIntv);
            }

            public override IType VisitUnaryMinus(Expression left, Expression original, Pair<This, Int32> data)
            {
                occurrences.Add(left);

                return data.One.Interval_UnaryMinus(Visit(left, PlusOne(data)));
            }

            public override IType VisitUnknown(Expression left, Pair<This, Int32> data)
            {
                occurrences.Add(left);

                return data.One.IntervalUnknown;
            }

            public override IType VisitVariable(Variable var, Expression original, Pair<This, Int32> data)
            {
                // this.occurrences.Add(var);

                if (data.One.ContainsKey(var))
                    return data.One[var];
                else
                    return data.One.IntervalUnknown;
            }

            public override IType VisitWritableBytes(Expression left, Expression wholeExpression, Pair<This, Int32> data)
            {
                occurrences.Add(left);

                var v = this.Decoder.UnderlyingVariable(wholeExpression);
                if (data.One.ContainsKey(v))
                    return data.One[v];
                else
                    return data.One.IntervalUnknown;
            }

            public override IType VisitXor(Expression left, Expression right, Expression original, Pair<This, int> data)
            {
                occurrences.Add(left, right);

                var plusOne = PlusOne(data);

                var leftIntv = Visit(left, plusOne);
                var rightIntv = Visit(right, plusOne);

                return data.One.Interval_BitwiseXor(leftIntv, rightIntv);
            }

            protected override IType Default(Pair<This, Int32> data)
            {
                return data.One.IntervalUnknown;
            }

            #endregion

            #region Privates
            static private Pair<This, Int32> PlusOne(Pair<This, Int32> data)
            {
                return new Pair<This, int>(data.One, data.Two + 1);
            }

            private IType RefineWithTypeRange(IType interval, Expression exp, This converter)
            {
                Contract.Requires(interval != null);
                Contract.Ensures(Contract.Result<IType>() != null);

                var type = this.Decoder.TypeOf(exp);
                switch (type)
                {
                    case ExpressionType.Bool:
                        return converter.ApplyConversion(ExpressionOperator.ConvertToInt32, interval);

                    case ExpressionType.Int8:
                        return converter.ApplyConversion(ExpressionOperator.ConvertToInt8, interval);

                    case ExpressionType.Int16:
                        return converter.ApplyConversion(ExpressionOperator.ConvertToInt16, interval);

                    case ExpressionType.Int32:
                        return converter.ApplyConversion(ExpressionOperator.ConvertToInt32, interval);

                    case ExpressionType.Int64:
                        return converter.ApplyConversion(ExpressionOperator.ConvertToInt64, interval);

                    case ExpressionType.UInt8:
                        return converter.ApplyConversion(ExpressionOperator.ConvertToUInt8, interval);

                    case ExpressionType.UInt16:
                        return converter.ApplyConversion(ExpressionOperator.ConvertToUInt16, interval);

                    case ExpressionType.UInt32:
                        return converter.ApplyConversion(ExpressionOperator.ConvertToUInt32, interval);

                    case ExpressionType.UInt64:
                        return converter.ApplyConversion(ExpressionOperator.ConvertToUInt64, interval);

                    default:
                        return interval;
                }
            }

            #endregion

            #region Occurrences counter
            private class VariableOccurrences
            {
                #region Object Invariant
                [ContractInvariantMethod]
                private void ObjectInvariant()
                {
                    Contract.Invariant(duplicated != null);
                    Contract.Invariant(occurrences != null);
                    Contract.Invariant(decoder != null);
                }
                #endregion

                readonly private Dictionary<Variable, int> occurrences;
                private readonly IExpressionDecoder<Variable, Expression> decoder;
                private readonly List<Variable> duplicated;

                public VariableOccurrences(IExpressionDecoder<Variable, Expression> decoder)
                {
                    Contract.Requires(decoder != null);

                    occurrences = new Dictionary<Variable, int>();
                    this.decoder = decoder;
                    duplicated = new List<Variable>();
                }

                public void Add(Variable v)
                {
                    int count;
                    if (occurrences.TryGetValue(v, out count))
                    {
                        occurrences[v] = count + 1;

                        if (count == 1)
                        {
                            duplicated.Add(v);
                        }
                    }
                    else
                    {
                        occurrences[v] = 1;
                    }
                }

                public void Add(Expression e)
                {
                    this.Add(decoder.UnderlyingVariable(e));
                }

                public void Add(params Expression[] exps)
                {
                    Contract.Requires(exps != null);

                    foreach (var e in exps)
                    {
                        this.Add(e);
                    }
                }

                public List<Variable> DuplicatedOccurrences
                {
                    get
                    {
                        Contract.Ensures(Contract.Result<List<Variable>>() != null);

                        return duplicated;
                    }
                }

                public override string ToString()
                {
                    return occurrences.ToString();
                }
            }
            #endregion

            #region Logic for caching 

            public class Cache
            {
                [ContractInvariantMethod]
                private void ObjectInvariant()
                {
                    Contract.Invariant(cache != null);
                }


                private Dictionary<Variable, IType> cache;
                private int currversion;

                public Cache(int version)
                {
                    currversion = version;
                    cache = new Dictionary<Variable, IType>();
                }

                public bool TryGetFromCache(int version, Variable v, out IType result)
                {
                    if (version == currversion && cache.TryGetValue(v, out result))
                    {
                        return true;
                    }

                    result = default(IType);
                    return false;
                }

                public void Set(int version, Variable v, IType value)
                {
                    if (currversion == version)
                    {
                        cache[v] = value;
                    }
                    else
                    {
                        currversion = version;
                        cache = new Dictionary<Variable, IType>();
                    }
                }
            }

            #endregion
        }

        /// <summary>
        /// Get an interval containing the constant.
        /// It can be an overapproximation as some values may not be exactly represented as for instance Int64 when the underlying Rational
        /// are at 32 bits
        /// </summary>
        public class EvalConstantVisitor
          : GenericTypeExpressionVisitor<Variable, Expression, This, IType>
        {
            private IType IntervalUnknown;

            public EvalConstantVisitor(IExpressionDecoder<Variable, Expression> decoder)
              : base(decoder)
            {
                Contract.Requires(decoder != null);
            }

            override public IType Visit(Expression exp, This state)
            {
                IntervalUnknown = state.IntervalUnknown;

                if (exp != null && this.Decoder.IsConstant(exp))
                {
                    // F: WARNING BELOW TO BE FIXED
                    return base.Visit(exp, state);
                }
                else
                {
                    return Default(exp);
                }
            }

            public override IType VisitFloat32(Expression exp, This input)
            {
                Single value;
                IType result;

                if (this.Decoder.TryValueOf<Single>(exp, ExpressionType.Float32, out value))
                {
                    result = input.For(value);

                    return result;
                }
                else
                {
                    return input.IntervalUnknown;
                }
            }

            public override IType VisitFloat64(Expression exp, This input)
            {
                Double value;
                IType result;

                if (this.Decoder.TryValueOf<Double>(exp, ExpressionType.Float64, out value))
                {
                    result = input.For(value);

                    return result;
                }
                else
                {
                    return input.IntervalUnknown;
                }
            }

            public override IType VisitInt8(Expression exp, This input)
            {
                SByte value;
                IType result;

                if (this.Decoder.TryValueOf<SByte>(exp, ExpressionType.Int8, out value))
                {
                    result = input.For(value);

                    return result;
                }
                else
                {
                    return input.IntervalUnknown;
                }
            }

            public override IType VisitInt16(Expression exp, This input)
            {
                Int16 value;

                if (this.Decoder.TryValueOf<Int16>(exp, ExpressionType.Int16, out value))
                {
                    return input.For(value);
                }
                else
                {
                    return input.IntervalUnknown;
                }
            }

            public override IType VisitInt32(Expression exp, This input)
            {
                Int32 value;
                IType result;

                if (this.Decoder.TryValueOf<Int32>(exp, ExpressionType.Int32, out value))
                {
                    result = input.For(value);

                    return result;
                }
                else
                {
                    return input.IntervalUnknown;
                }
            }

            public override IType VisitInt64(Expression exp, This input)
            {
                Int64 value;

                if (this.Decoder.TryValueOf<Int64>(exp, ExpressionType.Int64, out value))
                {
                    return input.For(value);
                }
                else
                {
                    return input.IntervalUnknown;
                }
            }

            public override IType VisitUInt8(Expression exp, This input)
            {
                Byte value;

                if (this.Decoder.TryValueOf<Byte>(exp, ExpressionType.UInt8, out value))
                {
                    return input.For(value);
                }
                else
                {
                    return input.IntervalUnknown;
                }
            }

            public override IType VisitUInt16(Expression exp, This input)
            {
                UInt16 value;

                if (this.Decoder.TryValueOf<UInt16>(exp, ExpressionType.UInt16, out value))
                {
                    return input.For(value);
                }
                else
                {
                    return input.IntervalUnknown;
                }
            }

            public override IType VisitUInt32(Expression exp, This input)
            {
                UInt32 value;

                if (this.Decoder.TryValueOf<UInt32>(exp, ExpressionType.UInt32, out value))
                {
                    return input.For(value);
                }
                else
                {
                    return input.IntervalUnknown;
                }
            }

            public override IType VisitBool(Expression exp, This input)
            {
                bool b;
                if (this.Decoder.TryValueOf<Boolean>(exp, ExpressionType.Bool, out b))
                {
                    return b ? input.For(1) : input.For(0);
                }

                return input.IntervalUnknown;
            }

            public override IType Default(Expression exp)
            {
                return IntervalUnknown;
            }
        }
        #endregion

        #region IIntervalAbstraction<Variable,Expression> Members (To make the type system happy)

        bool IIntervalAbstraction<Variable, Expression>.LessEqual(IIntervalAbstraction<Variable, Expression> other)
        {
            return this.LessEqual((This)other);
        }

        IIntervalAbstraction<Variable, Expression> IIntervalAbstraction<Variable, Expression>.Join(IIntervalAbstraction<Variable, Expression> other)
        {
            return this.Join((This)other);
        }

        IIntervalAbstraction<Variable, Expression> IIntervalAbstraction<Variable, Expression>.Widening(IIntervalAbstraction<Variable, Expression> other)
        {
            return this.Widening((This)other);
        }

        List<Pair<Variable, Int32>> IIntervalAbstraction<Variable, Expression>.IntConstants
        {
            get { return this.IntConstants; }
        }

        void IIntervalAbstraction<Variable, Expression>.AssumeInDisInterval(Variable x, DisInterval value)
        {
            this.AssumeInDisInterval(x, value);
        }

        void IIntervalAbstraction<Variable, Expression>.Assign(Expression x, Expression exp, INumericalAbstractDomainQuery<Variable, Expression> preState)
        {
            this.Assign(x, exp, preState);
        }

        IIntervalAbstraction<Variable, Expression> IIntervalAbstraction<Variable, Expression>.RemoveRedundanciesWith(INumericalAbstractDomain<Variable, Expression> oracle)
        {
            return this.RemoveRedundanciesWith(oracle);
        }

        IIntervalAbstraction<Variable, Expression> IIntervalAbstraction<Variable, Expression>.TestTrue(Expression exp)
        {
            return this.TestTrue(exp);
        }

        IIntervalAbstraction<Variable, Expression> IIntervalAbstraction<Variable, Expression>.TestFalse(Expression exp)
        {
            return this.TestFalse(exp);
        }

        IIntervalAbstraction<Variable, Expression> IIntervalAbstraction<Variable, Expression>.TestTrueLessThan(Expression exp1, Expression exp2)
        {
            return this.TestTrueLessThan(exp1, exp2);
        }

        IIntervalAbstraction<Variable, Expression> IIntervalAbstraction<Variable, Expression>.TestTrueLessThan(Variable v1, Variable v2)
        {
            return this.TestTrueLessThan(v1, v2);
        }

        IIntervalAbstraction<Variable, Expression> IIntervalAbstraction<Variable, Expression>.TestTrueLessEqualThan(Expression exp1, Expression exp2)
        {
            return this.TestTrueLessEqualThan(exp1, exp2);
        }

        IIntervalAbstraction<Variable, Expression> IIntervalAbstraction<Variable, Expression>.TestTrueLessEqualThan(Variable v1, Variable v2)
        {
#if DEBUG
            System.Diagnostics.Debugger.Break();
#endif
            return this;
        }

        IIntervalAbstraction<Variable, Expression> IIntervalAbstraction<Variable, Expression>.TestTrueEqual(Expression exp1, Expression exp2)
        {
            return this.TestTrueEqual(exp1, exp2);
        }

        IIntervalAbstraction<Variable, Expression> IIntervalAbstraction<Variable, Expression>.TestTrueGeqZero(Expression exp)
        {
            return this.TestTrueGeqZero(exp);
        }

        #endregion

        #region IPureExpressionTest

        void IPureExpressionTest<Variable, Expression>.AssumeDomainSpecificFact(DomainSpecificFact fact)
        {
            this.AssumeDomainSpecificFact(fact);
        }

        #endregion

        #region INumericalAbstractDomainQuery<Variable,Expression> Members

        public Variable ToVariable(Expression exp)
        {
            return expManager.Decoder.UnderlyingVariable(exp);
        }

        #endregion
    }

    #region Contracts for IntervalEnvironment_Base
    [ContractClassFor(typeof(IntervalEnvironment_Base<,,,,>))]
    abstract public class IntervalEnvironment_BaseContracts<This, Variable, Expression, IType, NType>
      : IntervalEnvironment_Base<This, Variable, Expression, IType, NType>
      where IType : IntervalBase<IType, NType>
      where This : IntervalEnvironment_Base<This, Variable, Expression, IType, NType>
    {
        protected IntervalEnvironment_BaseContracts() : base((ExpressionManager<Variable, Expression>)null) { }

        protected override This DuplicateMe()
        {
            Contract.Ensures(Contract.Result<This>() != null);
            return default(This);
        }

        protected override This NewInstance(ExpressionManager<Variable, Expression> expManager)
        {
            Contract.Requires(expManager != null);
            Contract.Ensures(Contract.Result<This>() != null);

            return default(This);
        }

        protected override IType ConvertInterval(Interval intv)
        {
            Contract.Requires(intv != null);
            Contract.Ensures(Contract.Result<IType>() != null);

            return default(IType);
        }

        public override This TestTrueGeqZero(Expression exp)
        {
            Contract.Ensures(Contract.Result<This>() != null);

            return default(This);
        }

        public override This TestTrueLessThan(Expression exp1, Expression exp2)
        {
            Contract.Ensures(Contract.Result<This>() != null);

            return default(This);
        }

        public override This TestTrueLessEqualThan(Expression exp1, Expression exp2)
        {
            Contract.Ensures(Contract.Result<This>() != null);

            return default(This);
        }

        public override This TestTrueLessThan_Un(Expression exp1, Expression exp2)
        {
            Contract.Ensures(Contract.Result<This>() != null);

            return default(This);
        }

        public override This TestTrueLessEqualThan_Un(Expression exp1, Expression exp2)
        {
            Contract.Ensures(Contract.Result<This>() != null);

            return default(This);
        }

        protected override This TestNotEqualToZero(Expression guard)
        {
            Contract.Ensures(Contract.Result<This>() != null);

            return default(This);
        }

        protected override This TestNotEqualToZero(Variable v)
        {
            Contract.Ensures(Contract.Result<This>() != null);

            return default(This);
        }

        protected override This TestEqualToZero(Variable v)
        {
            Contract.Ensures(Contract.Result<This>() != null);

            return default(This);
        }

        protected override void AssumeKLessThanRight(IType k, Variable right)
        {
            Contract.Requires(k != null);
        }

        protected override void AssumeLeftLessThanK(Variable left, IType k)
        {
            Contract.Requires(k != null);
        }

        public override DisInterval BoundsFor(Expression exp)
        {
            Contract.Ensures(Contract.Result<DisInterval>() != null);

            return default(DisInterval);
        }

        public override DisInterval BoundsFor(Variable v)
        {
            Contract.Ensures(Contract.Result<DisInterval>() != null);

            return default(DisInterval);
        }

        protected override void AssumeInDisInterval_Internal(Variable x, DisInterval value)
        {
            Contract.Requires(value != null);
        }

        public override bool IsGreaterEqualThanZero(NType val)
        {
            Contract.Requires(!object.Equals(val, null));

            return default(bool);
        }

        public override bool IsGreaterThanZero(NType val)
        {
            Contract.Requires(!object.Equals(val, null));

            return default(bool);
        }

        public override bool IsLessThanZero(NType val)
        {
            Contract.Requires(!object.Equals(val, null));

            return default(bool);
        }

        public override bool IsLessEqualThanZero(NType val)
        {
            Contract.Requires(!object.Equals(val, null));

            return default(bool);
        }

        public override bool IsLessThan(NType val1, NType val2)
        {
            Contract.Requires(!object.Equals(val1, null));
            Contract.Requires(!object.Equals(val2, null));

            return default(bool);
        }

        public override bool IsLessEqualThan(NType val1, NType val2)
        {
            Contract.Requires(!object.Equals(val1, null));
            Contract.Requires(!object.Equals(val2, null));

            return default(bool);
        }

        public override bool IsZero(NType val)
        {
            Contract.Requires(!object.Equals(val, null));

            return default(bool);
        }

        public override bool IsNotZero(NType val)
        {
            Contract.Requires(!object.Equals(val, null));

            return default(bool);
        }

        public override bool IsPlusInfinity(NType val)
        {
            Contract.Requires(!object.Equals(val, null));

            return default(bool);
        }

        public override bool IsMinusInfinity(NType val)
        {
            Contract.Requires(!object.Equals(val, null));

            return default(bool);
        }

        public override bool AreEqual(IType left, IType right)
        {
            Contract.Requires(left != null);
            Contract.Requires(right != null);

            throw new NotImplementedException();
        }

        public override NType PlusInfinity
        {
            get { throw new NotImplementedException(); }
        }

        public override NType MinusInfinty
        {
            get { throw new NotImplementedException(); }
        }

        public override bool TryAdd(NType left, NType right, out NType result)
        {
            Contract.Requires(!object.Equals(left, null));
            Contract.Requires(!object.Equals(right, null));
            Contract.Ensures(!Contract.Result<bool>() || !object.Equals(Contract.ValueAtReturn(out result), null));

            result = default(NType);

            return default(bool);
        }

        public override IType IntervalUnknown
        {
            get
            {
                Contract.Ensures(Contract.Result<IType>() != null);
                return default(IType);
            }
        }

        public override IType IntervalZero
        {
            get
            {
                Contract.Ensures(Contract.Result<IType>() != null);
                return default(IType);
            }
        }

        public override IType IntervalOne
        {
            get
            {
                Contract.Ensures(Contract.Result<IType>() != null);
                return default(IType);
            }
        }

        public override IType IntervalGreaterEqualThanMinusOne
        {
            get
            {
                Contract.Ensures(Contract.Result<IType>() != null);
                return default(IType);
            }
        }

        public override IType Interval_Positive
        {
            get
            {
                Contract.Ensures(Contract.Result<IType>() != null);
                return default(IType);
            }
        }

        public override IType Interval_StrictlyPositive
        {
            get
            {
                Contract.Ensures(Contract.Result<IType>() != null);
                return default(IType);
            }
        }

        public override IType IntervalSingleton(NType val)
        {
            Contract.Requires(!object.Equals(val, null));

            Contract.Ensures(Contract.Result<IType>() != null);

            return default(IType);
        }

        public override IType IntervalRightOpen(NType inf)
        {
            Contract.Requires(!object.Equals(inf, null));

            Contract.Ensures(Contract.Result<IType>() != null);

            return default(IType);
        }

        public override IType IntervalLeftOpen(NType sup)
        {
            Contract.Requires(!object.Equals(sup, null));

            Contract.Ensures(Contract.Result<IType>() != null);

            return default(IType);
        }

        public override bool IsMaxInt32(IType intv)
        {
            Contract.Requires(intv != null);

            return default(bool);
        }

        public override bool IsMinInt32(IType intv)
        {
            Contract.Requires(intv != null);

            return default(bool);
        }

        public override IType Interval_Add(IType left, IType right)
        {
            Contract.Requires(left != null);
            Contract.Requires(right != null);

            Contract.Ensures(Contract.Result<IType>() != null);

            return default(IType);
        }

        public override IType Interval_Div(IType left, IType right)
        {
            Contract.Requires(left != null);
            Contract.Requires(right != null);

            Contract.Ensures(Contract.Result<IType>() != null);

            return default(IType);
        }

        public override IType Interval_Sub(IType left, IType right)
        {
            Contract.Requires(left != null);
            Contract.Requires(right != null);

            Contract.Ensures(Contract.Result<IType>() != null);

            return default(IType);
        }

        public override IType Interval_Mul(IType left, IType right)
        {
            Contract.Requires(left != null);
            Contract.Requires(right != null);

            Contract.Ensures(Contract.Result<IType>() != null);

            return default(IType);
        }

        public override IType Interval_Not(IType left)
        {
            Contract.Requires(left != null);

            Contract.Ensures(Contract.Result<IType>() != null);

            return default(IType);
        }

        public override IType Interval_UnaryMinus(IType left)
        {
            Contract.Requires(left != null);

            Contract.Ensures(Contract.Result<IType>() != null);

            return default(IType);
        }

        public override IType Interval_Rem(IType left, IType right)
        {
            Contract.Requires(left != null);
            Contract.Requires(right != null);

            Contract.Ensures(Contract.Result<IType>() != null);

            return default(IType);
        }

        public override IType Interval_BitwiseAnd(IType left, IType right)
        {
            Contract.Requires(left != null);
            Contract.Requires(right != null);

            Contract.Ensures(Contract.Result<IType>() != null);

            return default(IType);
        }

        public override IType Interval_BitwiseOr(IType left, IType right)
        {
            Contract.Requires(left != null);
            Contract.Requires(right != null);

            Contract.Ensures(Contract.Result<IType>() != null);

            return default(IType);
        }

        public override IType Interval_BitwiseXor(IType left, IType right)
        {
            Contract.Requires(left != null);
            Contract.Requires(right != null);

            Contract.Ensures(Contract.Result<IType>() != null);

            return default(IType);
        }

        public override IType Interval_ShiftLeft(IType left, IType right)
        {
            Contract.Requires(left != null);
            Contract.Requires(right != null);

            Contract.Ensures(Contract.Result<IType>() != null);

            return default(IType);
        }

        public override IType Interval_ShiftRight(IType left, IType right)
        {
            Contract.Requires(left != null);
            Contract.Requires(right != null);

            Contract.Ensures(Contract.Result<IType>() != null);

            return default(IType);
        }

        public override IType For(byte v)
        {
            Contract.Ensures(Contract.Result<IType>() != null);
            return default(IType);
        }

        public override IType For(double d)
        {
            Contract.Ensures(Contract.Result<IType>() != null);
            return default(IType);
        }

        public override IType For(short v)
        {
            Contract.Ensures(Contract.Result<IType>() != null);
            return default(IType);
        }

        public override IType For(int v)
        {
            Contract.Ensures(Contract.Result<IType>() != null);
            return default(IType);
        }

        public override IType For(long v)
        {
            Contract.Ensures(Contract.Result<IType>() != null);
            return default(IType);
        }

        public override IType For(sbyte s)
        {
            Contract.Ensures(Contract.Result<IType>() != null);
            return default(IType);
        }

        public override IType For(ushort u)
        {
            Contract.Ensures(Contract.Result<IType>() != null);
            return default(IType);
        }

        public override IType For(uint u)
        {
            Contract.Ensures(Contract.Result<IType>() != null);

            return default(IType);
        }

        public override IType For(Rational r)
        {
            Contract.Requires(!object.Equals(r, null));

            Contract.Ensures(Contract.Result<IType>() != null);

            return default(IType);
        }

        public override IType For(NType inf, NType sup)
        {
            Contract.Requires(!object.Equals(inf, null));
            Contract.Requires(!object.Equals(sup, null));

            Contract.Ensures(Contract.Result<IType>() != null);

            return default(IType);
        }

        protected override IType ApplyConversion(ExpressionOperator conversionType, IType val)
        {
            Contract.Requires(val != null);

            Contract.Ensures(Contract.Result<IType>() != null);

            return default(IType);
        }

        protected override T To<T>(NType n, IFactory<T> factory)
        {
            Contract.Requires(factory != null);

            throw new NotImplementedException();
        }

        protected override This Factory()
        {
            return default(This);
        }
    }
    #endregion
}