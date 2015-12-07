// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

// This file contains the interfaces for the abstract domains.
// It also contain the definition for some generic abstract domains (functional, flat, powerset, etc.)

using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Research.DataStructures;
using Microsoft.Research.AbstractDomains.Numerical;
using Microsoft.Research.AbstractDomains.Expressions;
using System.Diagnostics.Contracts;
using Microsoft.Research.CodeAnalysis;
using System.Linq;

namespace Microsoft.Research.AbstractDomains
{
    public enum AbstractState { Normal, Top, Bottom };

    /// <summary>
    /// Just for performances
    /// </summary>
    public abstract class FunctionalAbstractDomain
    {
        #region Performance counters

#if TRACE_PERFORMANCE
        [ThreadStatic]
        static private TimeSpan timeSpentInIntervalEval;
#endif

        [Conditional("TRACE_PERFORMANCE")]
        static protected void UpdateTimeSpentInIntervalEval(TimeSpan span)
        {
#if TRACE_PERFORMANCE
            timeSpentInIntervalEval += span;
#endif
        }

        static public string Statistics
        {
            get
            {
#if TRACE_PERFORMANCE
                var result = new StringBuilder();

                result.AppendFormat("Overall time spent in Eval of Intervals* : {0} {1}", timeSpentInIntervalEval.ToString(), Environment.NewLine);

                return result.ToString();
#else
                return "Performance tracing is off";
#endif
            }
        }

        #endregion
    }

    /// <summary>
    /// A functional lift for the operations on the abstract domain <code>Codomain</code>.
    /// Essentially, is a map from <code>Domain</code> elements to <code>Codomain</code>
    /// </summary>
    /// <typeparam name="Codomain">Must be an instance of IAbstractDomain</typeparam>
    [ContractVerification(true)]
    [ContractClass(typeof(FunctionalAbstractDomainContracts<,,>))]
    public abstract class FunctionalAbstractDomain<This, Domain, Codomain>
      : FunctionalAbstractDomain,
        IAbstractDomain
      where This : FunctionalAbstractDomain<This, Domain, Codomain>
      where Codomain : IAbstractDomain
    {
        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(elements != null);
        }

        #region Private variables

        private AbstractState state;

        private Dictionary<Domain, Codomain> elements;
        private Int32 version;

        #endregion

        public void SetElements(Dictionary<Domain, Codomain> value)
        {
            Contract.Requires(value != null);

            version++;
            elements = value;
        }

        [ContractVerification(false)]
        public void SetElements(List<Pair<Domain, Codomain>> value)
        {
            Contract.Requires(value != null);

            version++;
            elements = new Dictionary<Domain, Codomain>(value.Count);
            foreach (var pair in value)
            {
                if (pair.Two.IsNormal())
                    elements.Add(pair.One, pair.Two);
            }
        }

        public void CopyAndTransferOwnership(FunctionalAbstractDomain<This, Domain, Codomain> other)
        {
            Contract.Requires(other != null);
            Contract.Assume(other.elements != null);

            version++;
            elements = other.elements;
            other.elements = null;
        }

        [ContractVerification(false)]
        public void RemoveElement(Domain key)
        {
            version++;
            elements.Remove(key);
        }

        //[SuppressMessage("Microsoft.Contracts", "Ensures-27-45")]
        [ContractVerification(false)]
        virtual public bool TryGetValue(Domain key, out Codomain value)
        {
            Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn<Codomain>(out value) != null);

            return elements.TryGetValue(key, out value);
        }


        public void ClearElements()
        {
            version++;
            elements.Clear();
        }

        [ContractVerification(false)]
        public void AddElement(Domain key, Codomain value)
        {
            version++;
            elements.Add(key, value);
        }

        public IEnumerable<KeyValuePair<Domain, Codomain>> Elements
        {
            get
            {
                Contract.Ensures(Contract.Result<IEnumerable<KeyValuePair<Domain, Codomain>>>() != null);

                return elements;
            }
        }

        public IEnumerable<Domain> Keys
        {
            get
            {
                Contract.Ensures(Contract.Result<IEnumerable<Domain>>() != null);

                return elements.Keys;
            }
        }

        protected AbstractState State
        {
            get { return state; }
            set { state = value; }
        }

        protected Int32 Version
        {
            get
            {
                return version;
            }
            set
            {
                version = value;
            }
        }

        #region Constructors
        public FunctionalAbstractDomain()
        {
            elements = new Dictionary<Domain, Codomain>();
            state = AbstractState.Normal;
            version = 0;
        }

        protected FunctionalAbstractDomain(FunctionalAbstractDomain<This, Domain, Codomain> fun)
        {
            Contract.Requires(fun != null);

            elements = new Dictionary<Domain, Codomain>(fun.elements);
            state = fun.state;
            version = fun.version;
        }
        #endregion

        #region Implementation for IAbstractDomain

        abstract public object Clone();

        /// <summary>
        /// A functional map is bottom when at least one element is bottom
        /// </summary>
        public bool IsBottom
        {
            get
            {
                if (this.State == AbstractState.Bottom)
                    return true;
                if (elements.Keys.Count == 0)        // !bottom && no variable => it is not bottom
                    return false;

                return false;
            }
        }

        /// <summary>
        /// A functional map is top when all of the values in the codomain are top
        /// </summary>
        public bool IsTop
        {
            get
            {
                foreach (var val in elements)
                {
                    if (!val.Value.IsTop)
                        return false;
                }

                this.State = AbstractState.Top;
                return true;
            }
        }

        bool IAbstractDomain.LessEqual(IAbstractDomain a)
        {
            return this.LessEqual((This)a);
        }

        /// <summary>
        /// The pointwise extension of the order on the elements in the codomain
        /// </summary>
        [ContractVerification(false)]
        virtual public bool LessEqual(This right)
        {
            Contract.Requires(right != null);

            bool result;
            if (AbstractDomainsHelper.TryTrivialLessEqual(this, right, out result))
            {
                return result;
            }

            foreach (var pair in right.Elements)
            {
                var rightVal = pair.Value;

                Contract.Assume(rightVal != null);

                // We do not want to enforce each subclass to have a canonical representation so we explicitely check for it
                if (rightVal.IsTop)
                {
                    continue;
                }

                Codomain leftVal;
                if (!elements.TryGetValue(pair.Key, out leftVal))
                {
                    return false;
                }

                if (!leftVal.LessEqual(rightVal))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Returns an empty function
        /// </summary>
        IAbstractDomain IAbstractDomain.Bottom
        {
            get
            {
                return this.Bottom;
            }
        }

        public This Bottom
        {
            get
            {
                Contract.Ensures(Contract.Result<This>() != null);

                var bot = this.Factory();

                bot.state = AbstractState.Bottom;

                return bot;
            }
        }

        IAbstractDomain IAbstractDomain.Top
        {
            get
            {
                return this.Top;
            }
        }

        public This Top
        {
            get
            {
                Contract.Ensures(Contract.Result<This>() != null);

                var top = this.Factory();

                top.state = AbstractState.Top;

                return top;
            }
        }

        /// <summary>
        /// Join of functional maps
        /// </summary>
        /// <returns> A freshly allocated functional map, with the pointwise join</returns>
        IAbstractDomain IAbstractDomain.Join(IAbstractDomain a)
        {
            return this.Join((This)a);
        }

        [ContractVerification(false)]
        public virtual This Join(This other)
        {
            Contract.Requires(other != null);
            Contract.Ensures(Contract.Result<This>() != null);

            if (object.ReferenceEquals(this, other))
            {
                return other;
            }

            if (this.IsBottom)
                return other;
            if (other.IsBottom)
                return (This)this;

            Dictionary<Domain, Codomain> left, right;

            Contract.Assume(other.elements != null);

            // Select the smaller one
            if (elements.Count <= other.elements.Count)
            {
                left = elements;
                right = other.elements;
            }
            else
            {
                left = other.elements;
                right = elements;
            }

            var result = this.Factory();

            foreach (var pair in left)       // For all the elements in the intersection do the point-wise join
            {
                Codomain right_x;
                if (right.TryGetValue(pair.Key, out right_x))
                {
                    Contract.Assume(right_x != null);

                    var join = pair.Value.Join(right_x);

                    if (join.IsBottom)
                    {
                        return result.Bottom;
                    }

                    if (!join.IsTop)
                    { // We keep in the map only the elements that are != top
                        result[pair.Key] = (Codomain)join;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Meet of functional abstract domains
        /// </summary>
        IAbstractDomain IAbstractDomain.Meet(IAbstractDomain a)
        {
            return this.Meet((This)a);
        }

        [ContractVerification(false)]
        public virtual This Meet(This right)
        {
            Contract.Requires(right != null);
            Contract.Ensures(Contract.Result<This>() != null);

            // Here we do not have trivial joins as we want to join maps of different cardinality
            if (this.IsBottom)
                return (This)this;
            if (right.IsBottom)
                return right;

            var result = this.Factory();

            Contract.Assume(right.elements != null);

            foreach (var pair in this.Elements)       // For all the elements in the intersection do the point-wise join
            {
                Codomain right_x;
                if (right.elements.TryGetValue(pair.Key, out right_x))
                {
                    Contract.Assume(right_x != null);

                    var meet = pair.Value.Meet(right_x);
                    result[pair.Key] = (Codomain)meet;
                }
            }

            return result;
        }

        IAbstractDomain IAbstractDomain.Widening(IAbstractDomain prev)
        {
            return this.Widening((This)prev);
        }

        /// <summary>
        /// Widening of the functional domain.
        /// If the two functions have the same cardinality, then it is a widening.
        /// If not, i.e. the cardinality of the domain increases then it may not terminate.
        /// If you are in this 2nd case, please contact me (logozzo)
        /// </summary>
        [ContractVerification(false)]
        public virtual This Widening(This right)
        {
            Contract.Requires(right != null);
            Contract.Ensures(Contract.Result<This>() != null);

            // Here we do not have trivial widening as we want to join maps of different cardinality
            if (this.IsBottom)
                return right;

            if (right.IsBottom)
                return (This)this;

            var result = this.Factory();

            Contract.Assume(right.elements != null);

            foreach (var pair in this.Elements)       // For all the elements in the intersection do the point-wise join
            {
                Codomain right_x;
                if (right.elements.TryGetValue(pair.Key, out right_x))
                {
                    Contract.Assume(right_x != null);

                    var widening = pair.Value.Widening(right_x);

                    if (!widening.IsTop)
                    {
                        result[pair.Key] = (Codomain)widening;
                    }
                }
            }

            return result;
        }
        #endregion

        #region Redefined indexer

#if TRACEPERFORMANCE
        public static Dictionary<int, int> MaxSizes
        {
            get
            {
                return PerformanceCounter.MaxSizes;
            }
        }

        [ThreadStatic]
        private static int MaxSize;
#endif

        [ContractVerification(false)]
        virtual public Codomain this[Domain index]
        {
            set
            {
                if (value.IsBottom)
                {
                    state = AbstractState.Bottom;
                }

#if TRACEPERFORMANCE

                int oldMaxSize = MaxSize;
                MaxSize = Math.Max(MaxSize, this.Count);

#if TRACEPERFORMANCE && VERBOSEOUTPUT
                if (MaxSize > oldMaxSize)
                {
                    ConsoleColor oldColor = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;

                    Console.WriteLine("New MaxSize for an instance of a FunctionalAbstractDomain: {0}", MaxSize);

                    Console.ForegroundColor = oldColor;
                }
#endif

                if (MaxSizes.ContainsKey(this.GetHashCode()))
                {
                    MaxSizes[this.GetHashCode()] = Math.Max(MaxSizes[this.GetHashCode()], this.Count);
                }
                else
                {
                    MaxSizes[this.GetHashCode()] = this.Count;
                }

#if TRACEPERFORMANCE && VERBOSEOUTPUT
                if (MaxSizes[this.GetHashCode()] >= 50)
                {
                    ConsoleColor oldColor = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;

                    Console.WriteLine(" >50 vars : {0}", MaxSizes[this.GetHashCode()]);

                    Console.ForegroundColor = oldColor;
                }
#endif
#endif
                version++;
                elements[index] = value;
            }

            get
            {
                return elements[index];
            }
        }

        public int Count
        {
            get
            {
                return elements.Count;
            }
        }

        [ContractVerification(false)]
        public bool ContainsKey(Domain d)
        {
            return elements.ContainsKey(d);
        }

        #endregion

        #region TO BE OVERRIDDEN

        abstract protected This Factory();

        abstract protected string ToLogicalFormula(Domain d, Codomain c);

        abstract protected T To<T>(Domain d, Codomain c, IFactory<T> factory);

        #endregion

        #region To<T>

        public T To<T>(IFactory<T> factory)
        {
            if (this.IsBottom)
                return factory.Constant(false);
            if (this.IsTop)
                return factory.Constant(true);

            T result = factory.IdentityForAnd;

            foreach (Domain d in elements.Keys)
            {
                T singleEntry = To(d, this[d], factory);

                result = factory.And(result, singleEntry);
            }

            return result;
        }
        #endregion

        #region Assume Domain specific facts
        virtual public void AssumeDomainSpecificFact(DomainSpecificFact fact)
        {
        }

        #endregion

        #region Overriden
        //^ [Confined]
        public override string ToString()
        {
            if (this.Count == 0)
                return "empty";

            string result = "";
            foreach (var x in elements.Keys)
            {
                if (x != null)
                {
                    result += x.ToString() + " -> " + this[x] + ", ";
                }
            }
            return result;
        }
        #endregion
    }

    [ContractClassFor(typeof(FunctionalAbstractDomain<,,>))]
    internal abstract class FunctionalAbstractDomainContracts<This, Domain, Codomain>
      : FunctionalAbstractDomain<This, Domain, Codomain>
      where This : FunctionalAbstractDomain<This, Domain, Codomain>
      where Codomain : IAbstractDomain
    {
        protected override This Factory()
        {
            Contract.Ensures(Contract.Result<This>() != null);

            return default(This);
        }

        public override object Clone()
        {
            throw new NotImplementedException();
        }

        protected override string ToLogicalFormula(Domain d, Codomain c)
        {
            throw new NotImplementedException();
        }

        protected override T To<T>(Domain d, Codomain c, IFactory<T> factory)
        {
            Contract.Requires(factory != null);

            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// A simple implementation of the functional abstract domain
    /// </summary>
    [ContractVerification(true)]
    public sealed class SimpleImmutableFunctional<Domain, Codomain>
      : FunctionalAbstractDomain<SimpleImmutableFunctional<Domain, Codomain>, Domain, Codomain>
      where Codomain : IAbstractDomain
    {
        #region Constuctor
        public SimpleImmutableFunctional(Dictionary<Domain, Codomain> map)
          : base()
        {
            Contract.Requires(map != null);

            foreach (var pair in map)
            {
                base[pair.Key] = pair.Value;
            }
        }

        #endregion

        #region Add
        [ContractVerification(false)]
        public SimpleImmutableFunctional<Domain, Codomain> Add(Domain x, Codomain y)
        {
            Contract.Ensures(Contract.Result<SimpleImmutableFunctional<Domain, Codomain>>() != null);

            Codomain oldy;
            if (this.TryGetValue(x, out oldy) && oldy.Equals(x))
            {
                return this;
            }
            var newMap = new Dictionary<Domain, Codomain>();
            foreach (var pair in this.Elements)
            {
                newMap.Add(pair.Key, pair.Value);
            }

            newMap[x] = y;

            return new SimpleImmutableFunctional<Domain, Codomain>(newMap);
        }
        #endregion

        #region Overridden

        public override Codomain this[Domain index]
        {
            get
            {
                return base[index];
            }
            set
            {
                base[index] = value;
            }
        }

        public override object Clone()
        {
            return this;
        }

        protected override string ToLogicalFormula(Domain d, Codomain c)
        {
            return null;
        }

        protected override T To<T>(Domain d, Codomain c, IFactory<T> factory)
        {
            return factory.Constant(true);
        }

        protected override SimpleImmutableFunctional<Domain, Codomain> Factory()
        {
            return new SimpleImmutableFunctional<Domain, Codomain>(new Dictionary<Domain, Codomain>());
        }
        #endregion

        #region Statics
        static public SimpleImmutableFunctional<Domain, Codomain> Unknown
        {
            get
            {
                Contract.Ensures(Contract.Result<SimpleImmutableFunctional<Domain, Codomain>>() != null);

                return new SimpleImmutableFunctional<Domain, Codomain>(new Dictionary<Domain, Codomain>());
            }
        }
        #endregion
    }

    /// <summary>
    /// The extension of a functional abstract domain to environemnts
    /// </summary>
    public abstract class FunctionalAbstractDomainEnvironment<This, Domain, Codomain, Variable, Expression>
      : FunctionalAbstractDomain<This, Domain, Codomain>,
      IAbstractDomainForEnvironments<Variable, Expression>
      where This : FunctionalAbstractDomainEnvironment<This, Domain, Codomain, Variable, Expression>
      where Codomain : IAbstractDomain
    {
        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.ExpressionManager != null);
        }

        #region State

        protected readonly ExpressionManager<Variable, Expression> ExpressionManager;

        #endregion

        #region Constructors

        protected FunctionalAbstractDomainEnvironment(ExpressionManager<Variable, Expression> expManager)
        {
            Contract.Requires(expManager != null);

            this.ExpressionManager = expManager;
        }

        protected FunctionalAbstractDomainEnvironment(FunctionalAbstractDomainEnvironment<This, Domain, Codomain, Variable, Expression> other)
          : base(other)
        {
            Contract.Requires(other != null);

            this.ExpressionManager = other.ExpressionManager;
        }

        #endregion

        #region To be OVERRIDDEN

        abstract override public object Clone();

        abstract override protected This Factory();

        abstract public List<Variable> Variables { get; }

        abstract public void Assign(Expression x, Expression exp);

        abstract public void ProjectVariable(Variable var);

        abstract public void RemoveVariable(Variable var);

        abstract public void RenameVariable(Variable OldName, Variable NewName);

        abstract public This TestTrue(Expression guard);

        abstract public This TestFalse(Expression guard);

        abstract public FlatAbstractDomain<bool> CheckIfHolds(Expression exp);

        abstract public void AssignInParallel(Dictionary<Variable, FList<Variable>> sourcesToTargets, Converter<Variable, Expression> convert);

        #endregion

        #region TestTrue Equal

        virtual public This TestTrueEqual(Domain v1, Domain v2)
        {
            Contract.Ensures(Contract.Result<This>() != null);

            Codomain sl, sr;
            var inLeft = this.TryGetValue(v1, out sl);
            var inRight = this.TryGetValue(v2, out sr);

            if (inLeft ^ inRight)
            {
                if (inLeft)
                {
                    this.AddElement(v2, sl);
                }
                else
                {
                    this.AddElement(v1, sr);
                }
            }
            else if (inLeft)
            {
                Contract.Assert(inRight);

                var meet = sl.Meet(sr);

                if (meet is Codomain)
                {
                    this[v1] = (Codomain)meet;
                    this[v2] = (Codomain)meet;
                }
            }

            return (This)this;
        }

        #endregion

        #region Overridden
        protected override string ToLogicalFormula(Domain d, Codomain c)
        {
            return "true";
        }

        protected override T To<T>(Domain d, Codomain c, IFactory<T> factory)
        {
            return factory.IdentityForAnd;
        }
        #endregion

        #region IAbstractDomainForEnvironments<Variable,Expression> Members

        public string ToString(Expression exp)
        {
            return ExpressionPrinter.ToString(exp, this.ExpressionManager.Decoder);
        }

        #endregion

        #region IPureExpressionAssignments<Variable,Expression> Members

        public virtual void AddVariable(Variable var)
        {
        }

        #endregion

        #region IPureExpressionTest<Variable,Expression> Members

        IAbstractDomainForEnvironments<Variable, Expression> IPureExpressionTest<Variable, Expression>.TestTrue(Expression guard)
        {
            return this.TestTrue(guard);
        }

        IAbstractDomainForEnvironments<Variable, Expression> IPureExpressionTest<Variable, Expression>.TestFalse(Expression guard)
        {
            return this.TestFalse(guard);
        }

        void IPureExpressionTest<Variable, Expression>.AssumeDomainSpecificFact(DomainSpecificFact fact)
        {
            this.AssumeDomainSpecificFact(fact);
        }

        #endregion
    }

    /// <summary>
    /// The implementation for a cartesian abstract domain with reduction.
    /// The reduction operation is specific to the domains, so each subclass must implement is
    /// </summary>
    [ContractClass(typeof(ReducedCartesianAbstractDomainContracts<,>))]
    public abstract class ReducedCartesianAbstractDomain<LeftDomain, RightDomain> : IAbstractDomain
      where LeftDomain : class, IAbstractDomain
      where RightDomain : class, IAbstractDomain
    {
        #region Private fields
        readonly private LeftDomain left;
        readonly private RightDomain right;
        #endregion

        #region Object Invariant
        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(left != null);
            Contract.Invariant(right != null);
        }

        #endregion

        #region Getters

        public LeftDomain Left
        {
            get
            {
                Contract.Ensures(Contract.Result<LeftDomain>() != null);
                Contract.Ensures(Contract.Result<LeftDomain>().Equals(left));

                return left;
            }
        }

        public RightDomain Right
        {
            get
            {
                Contract.Ensures(Contract.Result<RightDomain>() != null);
                Contract.Ensures(Contract.Result<RightDomain>().Equals(right));

                return right;
            }
        }
        #endregion

        #region Constructor
        protected ReducedCartesianAbstractDomain(LeftDomain left, RightDomain right)
        {
            Contract.Requires(left != null);
            Contract.Requires(right != null);

            Contract.Ensures(this.left.Equals(left));
            Contract.Ensures(this.right.Equals(right));

            Contract.Ensures(this.left != null);
            Contract.Ensures(this.right != null);

            this.left = left;
            this.right = right;
        }
        #endregion

        #region TO BE OVERRIDDEN

        /// <summary>
        /// Perform the reduction of the elements of the two domains.
        /// The reduction depends by the abstract domains, so each subclass of <code>ReducedCartesianAbstractDomain</code> is responsible for defining and implementing it.
        /// </summary>
        abstract public ReducedCartesianAbstractDomain<LeftDomain, RightDomain> Reduce(LeftDomain left, RightDomain right);

        /// <summary>
        /// The widening is abstract because I want each implementation to think to his widening.
        /// In particular I want to avoid non termination problems consequence of a bad interaction between closure (or reduction) and naive widenings 
        /// </summary>
        abstract public IAbstractDomain Widening(IAbstractDomain prev);

        /// <summary>
        /// The factory method for creating a particular instance of 
        /// </summary>
        abstract protected ReducedCartesianAbstractDomain<LeftDomain, RightDomain> Factory(LeftDomain left, RightDomain right);

        #endregion

        #region Generic assume
        virtual public void AssumeDomainSpecificFact(DomainSpecificFact fact)
        {
            Contract.Requires(fact != null);
        }

        #endregion

        #region IAbstractDomain Members

        public virtual bool IsBottom
        {
            get
            {
                return left.IsBottom || right.IsBottom;       // Please note that we do the reduction here, so the element is bottom if one of the two is
            }
        }

        public bool IsTop
        {
            get
            {
                return left.IsTop && right.IsTop;
            }
        }

        /// <summary>
        /// The pairwise order.
        /// If first perform the closure of the two arguments, and then checks them pairwisely.
        /// It is defined virtual so that a class can redefine it so to make it faster
        /// </summary>
        virtual public bool LessEqual(IAbstractDomain a)
        {
            Contract.Assert(a is ReducedCartesianAbstractDomain<LeftDomain, RightDomain>);

            bool result;
            if (AbstractDomainsHelper.TryTrivialLessEqual(this, a, out result))
            {
                return result;
            }

            var r = a as ReducedCartesianAbstractDomain<LeftDomain, RightDomain>;
            Contract.Assume(r != null);

            var lreduced = this.Reduce(left, right);
            var rreduced = r.Reduce(r.left, r.right);

            bool b1 = lreduced.Left.LessEqual(rreduced.Left);

            if (!b1)
                return b1;

            bool b2 = lreduced.Right.LessEqual(rreduced.Right);

            return b2;
        }

        public IAbstractDomain Bottom
        {
            get
            {
                return Factory((LeftDomain)this.Left.Bottom, (RightDomain)this.Right.Bottom);
            }
        }

        public IAbstractDomain Top
        {
            get
            {
                return Factory((LeftDomain)this.Left.Top, (RightDomain)this.Right.Top);
            }
        }

        /// <summary>
        /// The pointwise join. The results are then reduced through <code>Reduce</code>
        /// </summary>
        virtual public IAbstractDomain Join(IAbstractDomain a)
        {
            IAbstractDomain result;
            if (AbstractDomainsHelper.TryTrivialJoin(this, a, out result))
            {
                return result;
            }

            var r = a as ReducedCartesianAbstractDomain<LeftDomain, RightDomain>;
            Contract.Assume(r != null);

            var joinLeftPart = (LeftDomain)this.Left.Join(r.Left);
            var joinRightPart = (RightDomain)this.Right.Join(r.Right);

            return this.Reduce(joinLeftPart, joinRightPart);
        }

        /// <summary>
        /// The pointwise meet. The results are then reduced through <code>Reduce</code>
        /// </summary>
        public IAbstractDomain Meet(IAbstractDomain a)
        {
            IAbstractDomain trivialMeet;
            if (AbstractDomainsHelper.TryTrivialMeet(this, a, out trivialMeet))
            {
                return trivialMeet;
            }

            var r = a as ReducedCartesianAbstractDomain<LeftDomain, RightDomain>;
            Contract.Assume(r != null);

            var meetLeftPart = (LeftDomain)this.Left.Meet(r.Left);
            var meetRightPart = (RightDomain)this.Right.Meet(r.Right);

            return this.Reduce(meetLeftPart, meetRightPart);
        }

        virtual public T To<T>(IFactory<T> factory)
        {
            var left = this.Left.To(factory);
            var right = this.Right.To(factory);

            return factory.And(left, right);
        }
        #endregion

        #region ICloneable Members

        /// <summary>
        /// Make a deep copy of this abstract element, i.e. it also clones the contained abstract elements
        /// </summary>
        virtual public object Clone()
        {
            var leftCloned = this.Left.Clone() as LeftDomain;
            var rightCloned = this.Right.Clone() as RightDomain;

            Contract.Assume(leftCloned != null);
            Contract.Assume(rightCloned != null);

            return Factory(leftCloned, rightCloned);
        }
        #endregion

        #region Overridden

        public override string ToString()
        {
            var leftStr = format(this.Left.ToString());
            var rightStr = format(this.Right.ToString());

            return "<" + Environment.NewLine + "  " + leftStr + ";" + Environment.NewLine + rightStr + Environment.NewLine + ">";
        }

        #endregion

        #region Pretty printing...
        /// <summary>
        /// Format the string <code>input</code>, and replaces all the "\n" with "\n  "
        /// </summary>
        static private string format(string input)
        {
            Contract.Requires(input != null);

            string s = input.Replace(Environment.NewLine, Environment.NewLine + "  ");

            return s;
        }
        #endregion
    }

    #region Contracts for Reduced Cartesian Abstract Domain

    [ContractClassFor(typeof(ReducedCartesianAbstractDomain<,>))]
    internal abstract class ReducedCartesianAbstractDomainContracts<LeftDomain, RightDomain>
      : ReducedCartesianAbstractDomain<LeftDomain, RightDomain>
      where LeftDomain : class, IAbstractDomain
      where RightDomain : class, IAbstractDomain
    {
        private ReducedCartesianAbstractDomainContracts(LeftDomain left, RightDomain right)
          : base(left, right)
        { }

        public override ReducedCartesianAbstractDomain<LeftDomain, RightDomain> Reduce(LeftDomain left, RightDomain right)
        {
            Contract.Requires(left != null);
            Contract.Requires(right != null);

            Contract.Ensures(Contract.Result<ReducedCartesianAbstractDomain<LeftDomain, RightDomain>>() != null);

            return default(ReducedCartesianAbstractDomain<LeftDomain, RightDomain>);
        }

        protected override ReducedCartesianAbstractDomain<LeftDomain, RightDomain> Factory(LeftDomain left, RightDomain right)
        {
            Contract.Requires(left != null);
            Contract.Requires(right != null);

            Contract.Ensures(Contract.Result<ReducedCartesianAbstractDomain<LeftDomain, RightDomain>>() != null);

            return default(ReducedCartesianAbstractDomain<LeftDomain, RightDomain>);
        }

        public override IAbstractDomain Widening(IAbstractDomain prev)
        {
            throw new NotImplementedException();
        }
    }


    #endregion

    /// <summary>
    /// The base class for a cartesian product of two numerical abstract domains
    /// </summary>
    public abstract class ReducedNumericalDomains<LeftDomain, RightDomain, Variable, Expression>
      : ReducedCartesianAbstractDomain<LeftDomain, RightDomain>,
        INumericalAbstractDomain<Variable, Expression>
      where LeftDomain : class, INumericalAbstractDomain<Variable, Expression>
      where RightDomain : class, INumericalAbstractDomain<Variable, Expression>
    {
        #region Object invariant

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(expManager != null);
        }

        #endregion

        #region State

        private readonly ExpressionManagerWithEncoder<Variable, Expression> expManager;

        protected readonly INumericalAbstractDomain<Variable, Expression>[] arr;

        #endregion

        #region Getters

        public ExpressionManagerWithEncoder<Variable, Expression> ExpressionManager
        {
            get
            {
                Contract.Ensures(Contract.Result<ExpressionManagerWithEncoder<Variable, Expression>>() != null);

                return expManager;
            }
        }

        protected Expression Zero
        {
            get
            {
                return this.ExpressionManager.Encoder.ConstantFor(0);
            }
        }

        #endregion

        #region (Protected) constructor

        protected ReducedNumericalDomains(LeftDomain left, RightDomain right, ExpressionManagerWithEncoder<Variable, Expression> expManager)
          : base(left, right)
        {
            Contract.Requires(left != null);
            Contract.Requires(right != null);

            Contract.Requires(expManager != null);

            Contract.Ensures(this.Left.Equals(left));
            Contract.Ensures(this.Right.Equals(right));

            this.expManager = expManager;
            this.arr = new INumericalAbstractDomain<Variable, Expression>[] { this.Left, this.Right };
        }

        #endregion

        #region To be implemented by subclasses
        abstract public FlatAbstractDomain<bool> CheckIfGreaterEqualThanZero(Expression exp);

        abstract public FlatAbstractDomain<bool> CheckIfLessThan(Expression e1, Expression e2);

        abstract public FlatAbstractDomain<bool> CheckIfLessEqualThan(Expression e1, Expression e2);

        virtual public FlatAbstractDomain<bool> CheckIfLessThanIncomplete(Expression e1, Expression e2)
        {
            return this.CheckIfLessThan(e1, e2);
        }

        #endregion

        #region INumericalAbstractDomain<Variable, Expression> Members

        virtual public FlatAbstractDomain<bool> CheckIfLessThan(Variable v1, Variable v2)
        {
            FlatAbstractDomain<bool> left, right;
            left = right = null;

            left = this.Left.CheckIfLessThan(v1, v2);

            if (left.IsBottom)
            {
                return left;
            }

            right = this.Right.CheckIfLessThan(v1, v2);

            return left.Meet(right);
        }

        virtual public FlatAbstractDomain<bool> CheckIfLessEqualThan(Variable v1, Variable v2)
        {
            FlatAbstractDomain<bool> left, right;
            left = right = null;

            left = this.Left.CheckIfLessEqualThan(v1, v2);

            if (left.IsBottom)
            {
                return left;
            }

            right = this.Right.CheckIfLessEqualThan(v1, v2);

            return left.Meet(right);
            //return this.Left.CheckIfLessEqualThan(v1, v2).Meet(this.Right.CheckIfLessEqualThan(v1, v2));
        }

        virtual public FlatAbstractDomain<bool> CheckIfLessThan_Un(Expression e1, Expression e2)
        {
            FlatAbstractDomain<bool> left, right;
            left = right = null;

            left = this.Left.CheckIfLessThan_Un(e1, e2);

            if (left.IsBottom)
            {
                return left;
            }

            right = this.Right.CheckIfLessThan_Un(e1, e2);

            return left.Meet(right);

            // return this.Left.CheckIfLessThan_Un(e1, e2).Meet(this.Right.CheckIfLessThan_Un(e1, e2));
        }

        virtual public FlatAbstractDomain<bool> CheckIfLessEqualThan_Un(Expression e1, Expression e2)
        {
            FlatAbstractDomain<bool> left, right;
            left = right = null;

            left = this.Left.CheckIfLessEqualThan_Un(e1, e2);

            if (left.IsBottom)
            {
                return left;
            }

            right = this.Right.CheckIfLessEqualThan_Un(e1, e2);

            return left.Meet(right);

            //return this.Left.CheckIfLessEqualThan_Un(e1, e2).Meet(this.Right.CheckIfLessEqualThan_Un(e1, e2));
        }

        virtual public DisInterval BoundsFor(Expression exp)
        {
            DisInterval left, right;
            left = right = null;

            left = this.Left.BoundsFor(exp);

            if (left.IsBottom)
            {
                return left;
            }

            right = this.Right.BoundsFor(exp);

            return left.Meet(right);

            //return this.Left.BoundsFor(exp).Meet(this.Right.BoundsFor(exp));
        }

        virtual public DisInterval BoundsFor(Variable v)
        {
            DisInterval left, right;
            left = right = null;

            left = this.Left.BoundsFor(v);

            if (left.IsBottom)
            {
                return left;
            }
            right = this.Right.BoundsFor(v);

            return left.Meet(right);

            //return this.Left.BoundsFor(v).Meet(this.Right.BoundsFor(v));
        }

        virtual public List<Pair<Variable, Int32>> IntConstants
        {
            get
            {
                var leftConstants = this.Left.IntConstants;
                var rightConstants = this.Right.IntConstants;

                leftConstants.AddRange(rightConstants);

                return leftConstants;
            }
        }

        virtual public IEnumerable<Expression> LowerBoundsFor(Expression v, bool strict)
        {
            return this.Right.LowerBoundsFor(v, strict).Union(this.Left.LowerBoundsFor(v, strict));
        }

        virtual public IEnumerable<Expression> UpperBoundsFor(Expression v, bool strict)
        {
            return this.Right.UpperBoundsFor(v, strict).Union(this.Left.UpperBoundsFor(v, strict));
        }

        virtual public IEnumerable<Expression> LowerBoundsFor(Variable v, bool strict)
        {
            return this.Right.LowerBoundsFor(v, strict).Union(this.Left.LowerBoundsFor(v, strict));
        }

        virtual public IEnumerable<Expression> UpperBoundsFor(Variable v, bool strict)
        {
            return this.Right.UpperBoundsFor(v, strict).Union(this.Left.UpperBoundsFor(v, strict));
        }

        virtual public IEnumerable<Variable> EqualitiesFor(Variable v)
        {
            return this.Right.EqualitiesFor(v).Union(this.Left.EqualitiesFor(v));
        }

        virtual public INumericalAbstractDomain<Variable, Expression> TestTrueGeqZero(Expression exp)
        {
            var leftResult = (LeftDomain)this.Left.TestTrueGeqZero(exp);
            var rightResult = (RightDomain)this.Right.TestTrueGeqZero(exp);

            return this.Factory(leftResult, rightResult) as INumericalAbstractDomain<Variable, Expression>;
        }

        virtual public INumericalAbstractDomain<Variable, Expression> TestTrueLessThan(Expression exp1, Expression exp2)
        {
            var leftResult = (LeftDomain)this.Left.TestTrueLessThan(exp1, exp2);
            var rightResult = (RightDomain)this.Right.TestTrueLessThan(exp1, exp2);

            return this.Factory(leftResult, rightResult) as INumericalAbstractDomain<Variable, Expression>;
        }

        virtual public INumericalAbstractDomain<Variable, Expression> TestTrueLessEqualThan(Expression exp1, Expression exp2)
        {
            var leftResult = (LeftDomain)this.Left.TestTrueLessEqualThan(exp1, exp2);
            var rightResult = (RightDomain)this.Right.TestTrueLessEqualThan(exp1, exp2);

            return this.Factory(leftResult, rightResult) as INumericalAbstractDomain<Variable, Expression>;
        }

        virtual public INumericalAbstractDomain<Variable, Expression> TestTrueEqual(Expression exp1, Expression exp2)
        {
            var leftResult = (LeftDomain)this.Left.TestTrueEqual(exp1, exp2);
            var rightResult = (RightDomain)this.Right.TestTrueEqual(exp1, exp2);

            return this.Factory(leftResult, rightResult) as INumericalAbstractDomain<Variable, Expression>;
        }

        virtual public INumericalAbstractDomain<Variable, Expression> RemoveRedundanciesWith(INumericalAbstractDomain<Variable, Expression> oracle)
        {
            var leftReduced = (LeftDomain)this.Left.RemoveRedundanciesWith(this.Right);
            var rightReduced = (RightDomain)this.Right.RemoveRedundanciesWith(oracle);

            return this.Factory(leftReduced, rightReduced) as INumericalAbstractDomain<Variable, Expression>;
        }

        #endregion

        #region IPureExpressionAssignments<Expression> Members

        public List<Variable> Variables
        {
            get
            {
                return this.Left.Variables.SetUnion(this.Right.Variables);
            }
        }

        public void AddVariable(Variable var)
        {
            this.Left.AddVariable(var);
            this.Right.AddVariable(var);
        }

        virtual public void AssumeInDisInterval(Variable x, DisInterval value)
        {
            //this.Left.AssumeInDisInterval(x, value);
            this.Right.AssumeInDisInterval(x, value);
        }

        void IPureExpressionTest<Variable, Expression>.AssumeDomainSpecificFact(DomainSpecificFact fact)
        {
            this.AssumeDomainSpecificFact(fact);
        }

        override public void AssumeDomainSpecificFact(DomainSpecificFact fact)
        {
            this.Left.AssumeDomainSpecificFact(fact);
            this.Right.AssumeDomainSpecificFact(fact);
        }

        virtual public void Assign(Expression x, Expression exp)
        {
            this.Left.Assign(x, exp);
            this.Right.Assign(x, exp);
        }

        virtual public void Assign(Expression x, Expression exp, INumericalAbstractDomainQuery<Variable, Expression> preState)
        {
            this.Left.Assign(x, exp, preState);
            this.Right.Assign(x, exp, preState);
        }

        virtual public void ProjectVariable(Variable var)
        {
            this.Left.ProjectVariable(var);
            this.Right.ProjectVariable(var);
        }

        virtual public void RemoveVariable(Variable var)
        {
            this.Left.RemoveVariable(var);
            this.Right.RemoveVariable(var);
        }

        virtual public void RenameVariable(Variable OldName, Variable NewName)
        {
            this.Left.RenameVariable(OldName, NewName);
            this.Right.RenameVariable(OldName, NewName);
        }

        #endregion

        #region IPureExpressionTest<Expression> Members
        public virtual IAbstractDomainForEnvironments<Variable, Expression> TestTrue(Expression guard)
        {
            var results = new IAbstractDomain[this.arr.Length];

            for (var i = 0; i < this.arr.Length; i++)
            {
                results[i] = this.arr[i].TestTrue(guard);
            }

            return this.Factory((LeftDomain)results[0], (RightDomain)results[1]) as INumericalAbstractDomain<Variable, Expression>;
        }

        virtual public IAbstractDomainForEnvironments<Variable, Expression> TestFalse(Expression guard)
        {
            var results = new IAbstractDomain[this.arr.Length];

            for (var i = 0; i < this.arr.Length; i++)
            {
                results[i] = this.arr[i].TestFalse(guard);
            }

            return this.Factory((LeftDomain)results[0], (RightDomain)results[1]) as INumericalAbstractDomain<Variable, Expression>;
        }

        virtual public FlatAbstractDomain<bool> CheckIfHolds(Expression exp)
        {
            if (CommonChecks.CheckTrivialEquality(exp, expManager.Decoder))
            {
                return CheckOutcome.True;
            }

            var results = new FlatAbstractDomain<bool>[this.arr.Length];

            for (var i = 0; i < arr.Length; i++)
            {
                results[i] = this.arr[i].CheckIfHolds(exp);
            }

            return results[0].Meet(results[1]);
        }

        public FlatAbstractDomain<bool> CheckIfEqual(Expression e1, Expression e2)
        {
            FlatAbstractDomain<bool> outcome;
            if (CommonChecks.CheckTrivialEquality(e1, e2, expManager.Decoder, out outcome))
            {
                return outcome;
            }

            var leftOutcome = this.Left.CheckIfEqual(e1, e2);
            if (leftOutcome.IsNormal())
            {
                return leftOutcome;
            }

            return this.Right.CheckIfEqual(e1, e2);
        }

        public FlatAbstractDomain<bool> CheckIfEqualIncomplete(Expression e1, Expression e2)
        {
            return this.CheckIfEqual(e1, e2);
        }

        /// <summary>
        /// Check if <code>e != 0</code>
        /// </summary>
        public FlatAbstractDomain<bool> CheckIfNonZero(Expression exp)
        {
            var checks = new FlatAbstractDomain<bool>[this.arr.Length];
            //MyParallel.For(0, 2, i => checks[i] = this.arr[i].CheckIfNonZero(exp));

            for (var i = 0; i < arr.Length; i++)
            {
                checks[i] = this.arr[i].CheckIfNonZero(exp);
            }

            var result = checks[0].Meet(checks[1]);

            if (result.IsTop)
            {
                // We give a try by checking if exp > 0 or exp < 0
                var isGTzero = this.CheckIfLessThan(this.Zero, exp);  // zero < exp ?

                if (isGTzero.IsTrue())
                {
                    return isGTzero;
                }

                var isLTzero = this.CheckIfLessThan(exp, this.Zero);  // exp < zero ?

                if (isLTzero.IsTrue())
                {
                    return isLTzero;
                }
            }

            return result;
        }

        public override IAbstractDomain Widening(IAbstractDomain prev)
        {
            if (this.IsBottom)
                return prev;
            if (prev.IsBottom)
                return this;

            var castedPrev = prev as ReducedNumericalDomains<LeftDomain, RightDomain, Variable, Expression>;

            Contract.Assert(prev != null);

            var widened = new IAbstractDomain[this.arr.Length];

            for (var i = 0; i < arr.Length; i++)
            {
                widened[i] = this.arr[i].Widening(castedPrev.arr[i]);
            }

            return this.Factory((LeftDomain)widened[0], (RightDomain)widened[1]);
        }

        #endregion

        #region IAssignInParallel<Expression> Members

        virtual public void AssignInParallel(Dictionary<Variable, FList<Variable>> sourcesToTargets, Converter<Variable, Expression> convert)
        {
            this.Left.AssignInParallel(sourcesToTargets, convert);
            this.Right.AssignInParallel(sourcesToTargets, convert);
        }

        #endregion

        #region Floating point types

        public void SetFloatType(Variable v, ConcreteFloat f)
        {
            this.Left.SetFloatType(v, f);
            this.Right.SetFloatType(v, f);
        }

        public FlatAbstractDomain<ConcreteFloat> GetFloatType(Variable v)
        {
            return this.Left.GetFloatType(v).Meet(this.Right.GetFloatType(v));
        }

        #endregion

        #region ToString
        public string ToString(Expression exp)
        {
            if (expManager.Decoder != null)
            {
                return ExpressionPrinter.ToString(exp, expManager.Decoder);
            }
            else
            {
                return "< missing expression decoder >";
            }
        }
        #endregion

        #region INumericalAbstractDomainQuery<Variable,Expression> Members

        public Variable ToVariable(Expression exp)
        {
            return expManager.Decoder.UnderlyingVariable(exp);
        }

        #endregion
    }

    /// <summary>
    /// A flat abstract domain, to be used for instance for constant propagation
    /// </summary>
    /// <typeparam name="Elements">The elements in the flat lattice. We perform the comparison of elements through <code>Equals</code></typeparam>
    [ContractVerification(true)]
    public class FlatAbstractDomain<Elements>
      : IAbstractDomain
    {
        public enum State { Top, Bottom, Normal }

        #region Cached values
        static private readonly FlatAbstractDomain<Elements> cachedBottom; // Thread-safe
        static private readonly FlatAbstractDomain<Elements> cachedTop; // Thread-safe

        static FlatAbstractDomain()
        {
            cachedBottom = new FlatAbstractDomain<Elements>(State.Bottom);
            cachedTop = new FlatAbstractDomain<Elements>(State.Top);
        }

        #endregion

        #region Private fields
        protected readonly State state;              // The state of the abstract element
        private readonly Elements val;               // If the state is top or bottom, this value is meaningless
        private readonly string name;                // An optional string, for pretty printing

        #endregion

        #region Getter
        /// <summary>
        /// The element encapsulated inside this element.
        /// It makes sense only if <code>!this.IsBottom</code> and <code>!this.IsTop</code>
        /// </summary>
        public Elements BoxedElement
        {
            get
            {
                Contract.Requires(this.IsNormal(), "BoxedElement makes sense just on non extreme cases");

                return val;
            }
        }
        #endregion

        #region Constructors
        public FlatAbstractDomain(Elements val, string name = "")
        {
            this.state = State.Normal;
            this.val = val;
            this.name = name;
        }

        protected FlatAbstractDomain()
        {
            this.state = State.Normal;
            val = default(Elements);
        }

        protected FlatAbstractDomain(State state)
        {
            this.state = state;
            val = default(Elements);
        }

        #endregion

        #region Implementation of IAbstractDomain

        public virtual object Clone()
        {
            return this;
        }

        public bool IsBottom
        {
            get
            {
                return this.state == State.Bottom;
            }
        }

        public bool IsTop
        {
            get
            {
                return this.state == State.Top;
            }
        }

        public bool LessEqual(IAbstractDomain a)
        {
            var other = a as FlatAbstractDomain<Elements>;

            Contract.Assume(other != null);

            return this.LessEqual(other);
        }

        public bool LessEqual(FlatAbstractDomain<Elements> right)
        {
            Contract.Requires(right != null);

            bool result;
            if (AbstractDomainsHelper.TryTrivialLessEqual(this, right, out result))
            {
                return result;
            }

            return AreEqual(val, right.val);
        }

        IAbstractDomain IAbstractDomain.Bottom
        {
            get
            {
                return BottomFactory();
            }
        }

        public FlatAbstractDomain<Elements> Bottom
        {
            get
            {
                return BottomFactory();
            }
        }

        IAbstractDomain IAbstractDomain.Top
        {
            get
            {
                return TopFactory();
            }
        }

        public FlatAbstractDomain<Elements> Top
        {
            get
            {
                Contract.Ensures(Contract.Result<FlatAbstractDomain<Elements>>() != null);

                return TopFactory();
            }
        }

        public IAbstractDomain Join(IAbstractDomain a)
        {
            var other = a as FlatAbstractDomain<Elements>;

            Contract.Assume(other != null);

            return this.Join(other);
        }

        public FlatAbstractDomain<Elements> Join(FlatAbstractDomain<Elements> right)
        {
            Contract.Requires(right != null);

            Contract.Ensures(Contract.Result<FlatAbstractDomain<Elements>>() != null);

            FlatAbstractDomain<Elements> result;
            if (AbstractDomainsHelper.TryTrivialJoin(this, right, out result))
            {
                return result;
            }

            Contract.Assume(val != null);
            Contract.Assume(right.val != null);

            return AreEqual(val, right.val) ? this : Top;
        }

        public IAbstractDomain Meet(IAbstractDomain a)
        {
            var other = a as FlatAbstractDomain<Elements>;

            Contract.Assume(other != null);

            return this.Meet(other);
        }

        public FlatAbstractDomain<Elements> Meet(FlatAbstractDomain<Elements> right)
        {
            Contract.Requires(right != null);

            Contract.Ensures(Contract.Result<FlatAbstractDomain<Elements>>() != null);

            FlatAbstractDomain<Elements> trivialMeet;
            if (AbstractDomainsHelper.TryTrivialMeet(this, right, out trivialMeet))
            {
                return trivialMeet;
            }

            if (AreEqual(val, right.val))
                return this;
            else
                return Bottom;
        }

        public IAbstractDomain Widening(IAbstractDomain prev)
        {
            return this.Join(prev);
        }
        #endregion

        #region Protected, to be Overridden
        /// <summary>
        /// A Factory method
        /// </summary>
        virtual protected FlatAbstractDomain<Elements> Factory()
        {
            return new FlatAbstractDomain<Elements>();
        }

        /// <summary>
        /// To be overridden if we want to change the meaning of equality.
        /// The default is to use the object.Equals() method
        /// </summary>
        /// <returns>
        /// true iff <code>left</code> and <code>right</code> are the same elements. False otherwise
        /// </returns>
        virtual protected bool AreEqual(Elements left, Elements right)
        {
            if (object.ReferenceEquals(left, right))
            {
                return true;
            }

            return left.Equals(right);
        }

        virtual protected FlatAbstractDomain<Elements> BottomFactory()
        {
            Contract.Ensures(Contract.Result<FlatAbstractDomain<Elements>>() != null);

            Contract.Assert(cachedBottom != null);

            return cachedBottom;
        }

        virtual protected FlatAbstractDomain<Elements> TopFactory()
        {
            Contract.Ensures(Contract.Result<FlatAbstractDomain<Elements>>() != null);

            Contract.Assert(cachedTop != null);

            return cachedTop;
        }

        #endregion

        #region Overridden

        virtual public T To<T>(IFactory<T> factory)
        {
            if (this.IsBottom)
                return factory.Constant(false);
            else
                return factory.Constant(true);
        }

        public override string ToString()
        {
            if (this.IsBottom)
                return "_|_";
            else if (this.IsTop)
                return "Top";
            else
            {
                Contract.Assume(val != null);

                return name != null && name.Length > 0 ? name : val.ToString();
            }
        }
        #endregion

        #region Implicit conversion
        public static implicit operator FlatAbstractDomain<Elements>(Elements el)
        {
            Contract.Ensures(Contract.Result<FlatAbstractDomain<Elements>>() != null);

            return new FlatAbstractDomain<Elements>(el);
        }
        #endregion
    }

    public class FlatAbstractDomainWithRenaming<Elements>
      : FlatAbstractDomain<Elements>,
      IAbstractDomainWithRenaming<FlatAbstractDomainWithRenaming<Elements>, Elements>
    {
        public FlatAbstractDomainWithRenaming(Elements el)
          : base(el)
        {
        }

        public FlatAbstractDomainWithRenaming(State state = State.Top)
          : base(state)
        {
        }

        #region IAbstractDomainWithRenaming<FlatAbstractDomainWithRenaming<Elements>,Elements> Members

        public FlatAbstractDomainWithRenaming<Elements> Rename(Dictionary<Elements, FList<Elements>> renaming)
        {
            if (this.IsNormal())
            {
                FList<Elements> newNames;
                if (renaming.TryGetValue(this.BoxedElement, out newNames) && newNames.Length() == 1)
                {
                    return new FlatAbstractDomainWithRenaming<Elements>(newNames.Head);
                }
                return new FlatAbstractDomainWithRenaming<Elements>();
            }
            else
            {
                return this;
            }
        }

        #endregion

        #region Implicit conversion
        static public implicit operator FlatAbstractDomainWithRenaming<Elements>(Elements el)
        {
            Contract.Ensures(Contract.Result<FlatAbstractDomainWithRenaming<Elements>>() != null);

            return new FlatAbstractDomainWithRenaming<Elements>(el);
        }
        #endregion

        #region Override

        protected override FlatAbstractDomain<Elements> BottomFactory()
        {
            return new FlatAbstractDomainWithRenaming<Elements>(State.Bottom);
        }

        protected override FlatAbstractDomain<Elements> TopFactory()
        {
            return new FlatAbstractDomainWithRenaming<Elements>(State.Top);
        }

        #endregion

        #region ToString

        public override string ToString()
        {
            if (this.IsBottom)
                return "_|_";
            if (this.IsTop)
                return "Top";
            return this.BoxedElement.ToString();
        }
        #endregion
    }

    [ContractVerification(true)]
    public class FlatAbstractDomainOfBoolsWithRenaming<Variable>
      : FlatAbstractDomain<bool>,
      IAbstractDomainForArraySegmentationAbstraction<FlatAbstractDomainOfBoolsWithRenaming<Variable>, Variable>
    {
        public FlatAbstractDomainOfBoolsWithRenaming(bool value, string msg = "")
          : base(value, msg)
        {
        }

        public FlatAbstractDomainOfBoolsWithRenaming(State state)
          : base(state)
        {
        }

        #region IAbstractDomainWithRenaming<FlatAbstractDomainWithRenaming<Elements,Variable>,Variable> Members

        public FlatAbstractDomainOfBoolsWithRenaming<Variable> Rename(Dictionary<Variable, FList<Variable>> renaming)
        {
            return this;
        }

        #endregion

        #region overridden

        protected override FlatAbstractDomain<bool> BottomFactory()
        {
            return new FlatAbstractDomainOfBoolsWithRenaming<Variable>(State.Bottom);
        }

        protected override FlatAbstractDomain<bool> TopFactory()
        {
            return new FlatAbstractDomainOfBoolsWithRenaming<Variable>(State.Top);
        }

        #endregion

        #region To

        public override T To<T>(IFactory<T> factory)
        {
            if (this.IsBottom)
                return factory.IdentityForOr;

            if (this.IsTop)
                return factory.IdentityForAnd;

            T name;
            if (this.BoxedElement && factory.TryGetName(out name))
            {
                // name != null
                return factory.NotEqualTo(name, factory.Null);
            }

            return factory.IdentityForAnd;
        }

        #endregion

        #region IAbstractDomainForArraySegmentationAbstraction<FlatAbstractDomainOfBoolsWithRenaming<Variable>,Variable> Members

        public FlatAbstractDomainOfBoolsWithRenaming<Variable> AssumeInformationFrom<Expression>(INumericalAbstractDomainQuery<Variable, Expression> oracle)
        {
            return this;
        }

        #endregion
    }

    /// <summary>
    /// One day this should be merged with ProofOutcome
    /// </summary>
    static public class CheckOutcome
    {
        readonly public static FlatAbstractDomain<bool> True;   // Thread-safe
        readonly public static FlatAbstractDomain<bool> False;  // Thread-safe
        readonly public static FlatAbstractDomain<bool> Bottom; // Thread-safe
        readonly public static FlatAbstractDomain<bool> Top;    // Thread-safe

        static CheckOutcome()
        {
            True = new FlatAbstractDomain<bool>(true);
            False = new FlatAbstractDomain<bool>(false);
            Bottom = True.Bottom;
            Top = True.Top;
        }

        [Pure]
        static public bool IsNormal(this IAbstractDomain receiver)
        {
            Contract.Ensures(Contract.Result<bool>() == (!receiver.IsBottom && !receiver.IsTop));

            return !receiver.IsBottom && !receiver.IsTop;
        }

        [Pure]
        static public bool IsTrue(this FlatAbstractDomain<bool> receiver)
        {
            return receiver.IsNormal() && receiver.BoxedElement;
        }

        [Pure]
        static public bool IsFalse(this FlatAbstractDomain<bool> receiver)
        {
            return receiver.IsNormal() && !receiver.BoxedElement;
        }
    }

    /// <summary>
    /// A flat abstract domain which uses a Comparer to test if two elements are the same.
    /// A first application is in the symbolic abstract domain.
    /// </summary>
    public class FlatAbstractDomainWithComparer<Elements>
      : FlatAbstractDomain<Elements>
    {
        #region Static constructor

        static private readonly FlatAbstractDomainWithComparer<Elements> cachedBottom; // Thread-safe
        static private readonly FlatAbstractDomainWithComparer<Elements> cachedTop;    // Thread-safe

        static FlatAbstractDomainWithComparer()
        {
            cachedBottom = new FlatAbstractDomainWithComparer<Elements>(State.Bottom);
            cachedTop = new FlatAbstractDomainWithComparer<Elements>(State.Top);
        }
        #endregion

        #region The comparer used to test if two elements are the same
        private readonly IComparer<Elements> comparer;
        #endregion

        #region Constructors
        /// <summary>
        /// Construct an abstract element of the flat lattice and the ability of comparing elements
        /// </summary>
        /// <param name="value">The element embedded in this abstract value</param>
        /// <param name="comparer">The comparer for the elements</param>
        public FlatAbstractDomainWithComparer(Elements value, IComparer<Elements> comparer)
          : base(value)
        {
            this.comparer = comparer;
        }

        protected FlatAbstractDomainWithComparer(IComparer<Elements> comparer)
          : base()
        {
            this.comparer = comparer;
        }

        protected FlatAbstractDomainWithComparer(State state)
          : base(state)
        {
            comparer = null;
        }

        #endregion

        #region Overridden methods
        /// <summary>
        /// We use the comparer passed at object creation time for comparing the elements
        /// </summary>
        protected override bool AreEqual(Elements left, Elements right)
        {
            return comparer.Compare(left, right) == 0;
        }

        /// <returns>A new instance of <code>FlatAbstractDomainWithComparer</code> that has the same comparer than this object </returns>
        protected override FlatAbstractDomain<Elements> Factory()
        {
            var freshValue = new FlatAbstractDomainWithComparer<Elements>(comparer);

            return freshValue;
        }

        protected override FlatAbstractDomain<Elements> BottomFactory()
        {
            return cachedBottom;
        }

        protected override FlatAbstractDomain<Elements> TopFactory()
        {
            return cachedTop;
        }
        #endregion
    }

    ///<summary>
    /// A powerset abstract domain
    ///</summary>
    ///<typeparam name="Elements">The elements of the set</typeparam>
    public class PowerSetAbstractDomain<Elements>
      : IAbstractDomain
    {
        // Improve: Add subclass to handle exactly enums, for precision and speed

        #region Cached values
        static protected readonly PowerSetAbstractDomain<Elements> cachedBottom = new PowerSetAbstractDomain<Elements>(State.Bottom); // Thread-safe
        static protected readonly PowerSetAbstractDomain<Elements> cachedTop = new PowerSetAbstractDomain<Elements>(State.Top);       // Thread-safe
        #endregion

        protected enum State { Top, Bottom, Normal };

        #region Private fields
        private readonly State state;                            // The state of the abstract element
        private readonly IReadonlySet<Elements> elements;        // The elements in the abstract element. This field is not significative if state == Top or state == Bottom
        #endregion

        #region Constructors
        /// <summary>
        /// Constructs the singleton {<code>element</code>}
        /// </summary>
        //^ [NotDelayed]
        public PowerSetAbstractDomain(Elements element)
        {
            // To make happy the Spec# compiler, we rewrite the two lines below by adding  a temp
            //  
            //  elements = new Set<Elements>();
            //  elements.Add(element);
            //
            var tmpElements = new Set<Elements>();

            tmpElements.Add(element);
            elements = tmpElements;
            //^ base();
            state = State.Normal;
        }

        /// <summary>
        /// Constructs the PowerSet containing all and only <code>elements</code>
        /// </summary>
        //^ [NotDelayed]
        public PowerSetAbstractDomain(IReadonlySet<Elements> elements)
        {
            //  To make happy the Spec# compiler, we rewrite the line below by adding a temp
            //      this.elements = new Set<Elements>(elements);

            var tmpElements = elements;
            this.elements = tmpElements;
            state = tmpElements.Count != 0 ? State.Normal : State.Bottom;
            //^ base();
        }

        private PowerSetAbstractDomain(State state)
        {
            this.state = state;
            elements = new Set<Elements>();
        }

        private PowerSetAbstractDomain(PowerSetAbstractDomain<Elements> psAD)
        {
            state = psAD.state;
            elements = new Set<Elements>(psAD.elements);
        }
        #endregion

        #region Implementation of IAbstractDomain

        public object Clone()
        {
            return new PowerSetAbstractDomain<Elements>(this);
        }

        public bool IsBottom
        {
            get
            {
                if (elements != null && elements.Count == 0 && state != State.Top)
                    return true;
                else
                    return state == State.Bottom;
            }
        }

        public bool IsTop
        {
            get
            {
                return state == State.Top;
            }
        }

        /// <summary>
        /// The partial order on the abstract domain
        ///</summary>
        public bool LessEqual(IAbstractDomain a)
        {
            bool result;
            if (AbstractDomainsHelper.TryTrivialLessEqual(this, a, out result))
            {
                return result;
            }

            Contract.Assume(a is PowerSetAbstractDomain<Elements>, "Error: expecting a PowerSetAbstractDomain<Elements> " + a);
            PowerSetAbstractDomain<Elements> right = (PowerSetAbstractDomain<Elements>)a;

            //^ assert this.state == State.Normal && right.state == State.Normal;

            return elements.IsSubset(right.elements);
        }

        /// <summary>
        /// Get the bottom element of the abstract domain
        ///</summary>
        public IAbstractDomain Bottom
        {
            get
            {
                return cachedBottom;
            }
        }

        /// <summary>
        /// Get the top element of the abstract doman
        ///</summary>
        public IAbstractDomain Top
        {
            get
            {
                return cachedTop;
            }
        }

        /// <summary>
        /// The join is roughly the set union
        ///</summary>
        public IAbstractDomain Join(IAbstractDomain a)
        {
            IAbstractDomain trivialresult;
            if (AbstractDomainsHelper.TryTrivialJoin(this, a, out trivialresult))
            {
                return trivialresult;
            }

            var right = a as PowerSetAbstractDomain<Elements>;
            Contract.Assume(right != null);

            return new PowerSetAbstractDomain<Elements>(elements.Union(right.elements));
        }

        /// <summary>
        /// The meet is roughly the set intersection
        ///</summary>
        public IAbstractDomain Meet(IAbstractDomain a)
        {
            IAbstractDomain trivialMeet;
            if (AbstractDomainsHelper.TryTrivialMeet(this, a, out trivialMeet))
            {
                return trivialMeet;
            }

            var right = a as PowerSetAbstractDomain<Elements>;
            Contract.Assume(right != null);

            var result = elements.Intersection(right.elements);

            return new PowerSetAbstractDomain<Elements>(result);
        }

        /// <summary>
        /// The widening is quite rough, in that it approximate the set with top
        /// Convention: <code>this</code> is the value of the new iteration. <code>prev</code> is the value of the previous one
        ///</summary>
        public IAbstractDomain Widening(IAbstractDomain prev)
        {
            if (this.IsTop)
                return this;
            else if (this.IsBottom)
                return prev;
            else if (prev.IsBottom)
                return this;
            else if (prev.IsTop)
                return prev;
            else
                return Top;
        }
        #endregion

        public bool IsSigleton
        {
            get
            {
                if (this.IsBottom || this.IsTop)
                {
                    return true;
                }
                else
                {
                    return elements.Count == 1;
                }
            }
        }

        /// <summary>
        /// The set of elements contained an an element of this domain.
        /// If this element is top, then an exception is thrown
        /// </summary>
        public Set<Elements> EmbeddedValues
        {
            get
            {
                if (!this.IsTop)
                {
                    return new Set<Elements>(elements);
                }
                else
                {
                    throw new AbstractInterpretationException("Cannot have the elements of a top powerset");
                }
            }
        }

        #region Overridden

        public T To<T>(IFactory<T> factory)
        {
            if (this.IsBottom)
                return factory.Constant(false);
            else
                return factory.Constant(true);
        }

        //^ [Confined]
        public override string ToString()
        {
            string result;
            if (this.IsBottom)
            {
                result = "{}";
            }
            else if (this.IsTop)
            {
                result = "Top";
            }
            else
            {
                result = "{ ";
                foreach (Elements e in elements)
                {
                    result += (e != null ? e.ToString() : "null") + ",";
                }

                int indexOfLastComma = result.LastIndexOf(",");
                if (indexOfLastComma > 0)
                {
                    result = result.Remove(indexOfLastComma);
                }

                result += " }";
            }

            return result;
        }
        #endregion

    }

    /// <summary>
    /// The generic class for set of constraints.
    /// Please note that it is different from <code>PowerSetAbstractDomain</code>
    /// </summary>
    [ContractVerification(true)]
    public class SetOfConstraints<Elements>
      : IAbstractDomainWithRenaming<SetOfConstraints<Elements>, Elements>
    {
        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(elements != null);
        }

        #region Cached values
        static private readonly SetOfConstraints<Elements> cachedBottom = new SetOfConstraints<Elements>(State.Bottom); // Thread-safe
        static private readonly SetOfConstraints<Elements> cachedTop = new SetOfConstraints<Elements>(State.Top);       // Thread-safe
        #endregion

        private enum State { Top, Bottom, Normal };

        #region Private fields
        private readonly State state;                      // The state of the abstract element
        private readonly Set<Elements> elements;           // The elements in the abstract element. This field is not significative if state == Top or state == Bottom

        #endregion

        #region Constructors

        public SetOfConstraints(Elements element)
        {
            elements = new Set<Elements>() { element };

            state = State.Normal;
        }

        public SetOfConstraints(IEnumerable<Elements> elements)
          : this(new Set<Elements>(elements))
        {
            Contract.Requires(elements != null);
        }

        public SetOfConstraints(Set<Elements> elements, bool makeCopy)
          : this(makeCopy ? new Set<Elements>(elements) : elements)
        {
            Contract.Requires(elements != null);
        }

        private SetOfConstraints(Set<Elements> elements)
        {
            Contract.Requires(elements != null);

            this.elements = elements;
            state = this.elements.Count != 0 ? State.Normal : State.Top;
        }

        private SetOfConstraints(State state)
        {
            this.state = state;
            elements = new Set<Elements>();
        }

        private SetOfConstraints(SetOfConstraints<Elements> psAD)
        {
            Contract.Requires(psAD != null);

            Contract.Assume(psAD.elements != null);

            state = psAD.state;
            elements = new Set<Elements>(psAD.elements);
        }
        #endregion

        #region Static Properties
        static public SetOfConstraints<Elements> Unknown
        {
            get
            {
                Contract.Ensures(Contract.Result<SetOfConstraints<Elements>>() != null);

                return cachedTop;
            }
        }

        static public SetOfConstraints<Elements> Unreached
        {
            get
            {
                Contract.Ensures(Contract.Result<SetOfConstraints<Elements>>() != null);

                return cachedBottom;
            }
        }

        #endregion

        #region Implementation of IAbstractDomain

        public object Clone()
        {
            return this; // It's functional
        }

        public bool IsBottom
        {
            get
            {
                if (/*this.elements != null && */elements.Count == 0 && state != State.Top)
                    return true;
                else
                    return state == State.Bottom;
            }
        }

        public bool IsTop
        {
            get
            {
                return state == State.Top;
            }
        }

        /// <summary>
        /// Order is superset inclusion
        /// </summary>
        public bool LessEqual(IAbstractDomain a)
        {
            bool result;
            if (AbstractDomainsHelper.TryTrivialLessEqual(this, a, out result))
            {
                return result;
            }

            var right = a as SetOfConstraints<Elements>;

            Contract.Assume(right != null);
            Contract.Assume(right.elements != null);

            return right.elements.IsSubset(elements);
        }

        public IAbstractDomain Bottom
        {
            get
            {
                return cachedBottom;
            }
        }

        /// <summary>
        /// Get the top element of the abstract doman
        ///</summary>
        IAbstractDomain IAbstractDomain.Top
        {
            get
            {
                return cachedTop;
            }
        }

        public SetOfConstraints<Elements> Top
        {
            get
            {
                return cachedTop;
            }
        }

        IAbstractDomain IAbstractDomain.Join(IAbstractDomain a)
        {
            var other = a as SetOfConstraints<Elements>;

            Contract.Assume(other != null);

            return this.Join(other);
        }

        /// <summary>
        /// The join is set intersection
        ///</summary>
        [Pure]
        public SetOfConstraints<Elements> Join(SetOfConstraints<Elements> right)
        {
            Contract.Requires(right != null);
            Contract.Ensures(Contract.Result<SetOfConstraints<Elements>>() != null);

            SetOfConstraints<Elements> trivialresult;
            if (AbstractDomainsHelper.TryTrivialJoin(this, right, out trivialresult))
            {
                return trivialresult;
            }
            Contract.Assume(right.elements != null);

            var result = elements.Intersection(right.elements);

            return new SetOfConstraints<Elements>(result, false);
        }

        IAbstractDomain IAbstractDomain.Meet(IAbstractDomain a)
        {
            var other = a as SetOfConstraints<Elements>;

            Contract.Assume(other != null);

            return this.Meet(other);
        }

        /// <summary>
        /// The meet is set union
        ///</summary>
        [Pure]
        public SetOfConstraints<Elements> Meet(SetOfConstraints<Elements> right)
        {
            Contract.Requires(right != null);

            Contract.Ensures(Contract.Result<SetOfConstraints<Elements>>() != null);

            SetOfConstraints<Elements> trivialMeet;
            if (AbstractDomainsHelper.TryTrivialMeet(this, right, out trivialMeet))
            {
                return trivialMeet;
            }

            Contract.Assume(right.elements != null);

            var result = elements.Union(right.elements);

            return new SetOfConstraints<Elements>(result, false);
        }

        IAbstractDomain IAbstractDomain.Widening(IAbstractDomain a)
        {
            var other = a as SetOfConstraints<Elements>;

            Contract.Assume(other != null);

            return this.Widening(other);
        }

        /// <summary>
        /// The widening is quite rough, in that it approximate the set with top
        /// Convention: <code>this</code> is the value of the new iteration. <code>prev</code> is the value of the previous one
        ///</summary>
        [Pure]
        public SetOfConstraints<Elements> Widening(SetOfConstraints<Elements> prev)
        {
            Contract.Requires(prev != null);

            Contract.Ensures(Contract.Result<SetOfConstraints<Elements>>() != null);

            if (this.IsTop)
                return this;
            if (this.IsBottom)
                return prev;

            if (prev.IsBottom)
                return this;
            if (prev.IsTop)
                return prev;

            return this.Join(prev as SetOfConstraints<Elements>);
        }
        #endregion

        public int Count
        {
            get
            {
                if (!this.IsNormal())
                {
                    return 0;
                }
                return elements.Count;
            }
        }

        public IEnumerable<Elements> Values
        {
            get
            {
                if (this.IsNormal())
                {
                    foreach (var x in elements)
                    {
                        yield return x;
                    }
                }
                else
                {
                    yield break;
                }
            }
        }

        [Pure]
        public SetOfConstraints<Elements> Add(Elements el)
        {
            Contract.Ensures(Contract.Result<SetOfConstraints<Elements>>() != null);

            if (this.IsBottom)
            {
                return this;
            }
            if (this.IsTop)
            {
                return new SetOfConstraints<Elements>(el);
            }
            else
            {
                var newSet = new Set<Elements>(elements);
                newSet.Add(el);

                return new SetOfConstraints<Elements>(newSet, false);
            }
        }

        [Pure]
        public bool Contains(Elements what)
        {
            if (!this.IsNormal())
                return false;

            return elements.Contains(what);
        }

        public T To<T>(IFactory<T> factory)
        {
            if (this.IsBottom)
                return factory.Constant(false);
            else
                return factory.Constant(true);
        }

        #region Overridden

        private Optional<int> hash;
        public override int GetHashCode()
        {
            if (!hash.IsValid)
            {
                hash = elements.GetHashCode();
            }

            return hash.Value;
        }

        public override string ToString()
        {
            if (this.IsBottom)
                return "{}";
            else if (this.IsTop)
                return "Top";
            else
            {
                return elements.ToString();
            }
        }
        #endregion

        #region IAbstractDomainWithRenaming<SetOfConstraints<Elements>,Elements> Members

        [ContractVerification(false)]
        public SetOfConstraints<Elements> Rename(Dictionary<Elements, FList<Elements>> renaming)
        {
            if (this.IsNormal())
            {
                var newSet = new Set<Elements>(elements.Count);
                foreach (var el in elements)
                {
                    FList<Elements> renamed;
                    if (renaming.TryGetValue(el, out renamed))
                    {
                        newSet.AddRange(renamed.GetEnumerable());
                    }
                }

                return new SetOfConstraints<Elements>(newSet, false);
            }

            return this;
        }

        #endregion
    }

    /// <summary>
    /// An abstract element with just two elements, top and bottom
    /// This is mainly for creating dummy top and bottom values and for debugging purposes
    /// </summary>
    [ContractVerification(true)]
    public class Reachability :
      IAbstractDomain
    {
        public enum State { Top, Bottom }

        #region Cached values
        static private readonly Reachability bottomReachbility = new Reachability(State.Bottom); // Thread-safe
        static private readonly Reachability topReachbility = new Reachability(State.Top);       // Thread-safe
        #endregion

        #region Private fields
        readonly private State val;
        #endregion

        #region Getters

        static public Reachability aTopElement
        {
            get
            {
                return topReachbility;
            }
        }

        static public Reachability aBottomElement
        {
            get
            {
                return bottomReachbility;
            }
        }
        #endregion

        #region Constructors

        protected Reachability(State r)
        {
            val = r;
        }

        #endregion

        #region Implementation of IAbstractDomain

        public object Clone()
        {
            return this;
        }

        public bool IsBottom
        {
            get
            {
                return val == State.Bottom;
            }
        }

        public bool IsTop
        {
            get
            {
                return val == State.Top;
            }
        }

        public bool LessEqual(IAbstractDomain a)
        {
            bool result;
            if (AbstractDomainsHelper.TryTrivialLessEqual(this, a, out result))
            {
                return result;
            }

            return false;
        }

        virtual public IAbstractDomain Bottom
        {
            get
            {
                return bottomReachbility;
            }
        }

        virtual public IAbstractDomain Top
        {
            get
            {
                return topReachbility;
            }
        }

        public IAbstractDomain Join(IAbstractDomain a)
        {
            IAbstractDomain result;
            if (AbstractDomainsHelper.TryTrivialJoin(this, a, out result))
            {
                return result;
            }

            Contract.Assume(false, "you were not supposed to be there.... " + a);

            return this.Top;
        }

        public IAbstractDomain Meet(IAbstractDomain a)
        {
            IAbstractDomain trivialMeet;
            if (AbstractDomainsHelper.TryTrivialMeet(this, a, out trivialMeet))
            {
                return trivialMeet;
            }

            Contract.Assume(false, "you were not supposed to be there.... " + a);

            return this.Bottom;
        }

        public IAbstractDomain Widening(IAbstractDomain prev)
        {
            return this.Join(prev);
        }

        public T To<T>(IFactory<T> factory)
        {
            if (this.IsBottom)
                return factory.Constant(false);
            else
                return factory.Constant(true);
        }
        #endregion

        #region ToString
        public override string ToString()
        {
            return val == State.Bottom ? "bottom" : "top";
        }
        #endregion
    }

    [ContractVerification(true)]
    public class TwoValuesLattice<Variable>
      : Reachability, IAbstractDomainForArraySegmentationAbstraction<TwoValuesLattice<Variable>, Variable>
    {
        private readonly string message;

        public TwoValuesLattice(Reachability.State state, string msg = null)
          : base(state)
        {
            message = msg;
        }

        override public IAbstractDomain Bottom
        {
            get
            {
                return new TwoValuesLattice<Variable>(State.Bottom, (this.IsBottom && message != null) ? message : "");
            }
        }

        override public IAbstractDomain Top
        {
            get
            {
                return new TwoValuesLattice<Variable>(State.Top);
            }
        }

        #region IAbstractDomainWithRenaming<Reachability<Variable>,Variable> Members

        public TwoValuesLattice<Variable> Rename(Dictionary<Variable, FList<Variable>> renaming)
        {
            return this;
        }

        #endregion

        #region ToString

        public override string ToString()
        {
            if (message != null)
            {
                return message;
            }

            return base.ToString();
        }

        #endregion

        #region IAbstractDomainForArraySegmentationAbstraction<TwoValuesLattice<Variable>,Variable> Members

        public TwoValuesLattice<Variable> AssumeInformationFrom<Expression>(INumericalAbstractDomainQuery<Variable, Expression> oracle)
        {
            return this;
        }

        #endregion
    }

#if TRACEPERFORMANCE

    internal static class PerformanceCounter
    {
        // For performance counting...
        [ThreadStatic]
        static internal Dictionary<int, int> maxSizes;

        static internal Dictionary<int, int> MaxSizes
        {
            get
            {
                if (maxSizes == null)
                    maxSizes = new Dictionary<int, int>();
                return maxSizes;
            }
        }
    }

#endif
}
