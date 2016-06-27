// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

// This file contains the classes and the abstract domains for implementing the abstract domain of Linear equalities

//#define CHECKINVARIANTS
#define TRACE_PERFORMANCE
//#define USESOLVERFOUNDATION
//#define LOGINFO

using System.Diagnostics;
using System.Text;
using System.Collections.Generic;
using System;
using Microsoft.Research.AbstractDomains.Expressions;
using Microsoft.Research.DataStructures;
using Microsoft.Research.CodeAnalysis;
using Microsoft.Glee;
using Microsoft.Glee.Optimization;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;

#if USESOLVERFOUNDATION
using Microsoft.SolverFoundation.Solvers;
using SolverRational = Microsoft.SolverFoundation.Common.Rational;
using Microsoft.SolverFoundation.Services;
#endif

namespace Microsoft.Research.AbstractDomains.Numerical
{
    /// <summary>
    /// The abstract numerical domain of linear equalities
    /// </summary> 
    public class LinearEqualitiesForSubpolyhedraEnvironment<Variable, Expression> :
        LinearEqualitiesEnvironment<Variable, Expression>
    {
        private const int MAX_VARS_FOR_COMBINE = 3;

        protected const int MaxCoefficientInEquationsforSimplexReduction = 1024;
        protected const long MaxBoundInIntervalsforSimplexReduction = (1L << 16);
        protected const long MinBoundInIntervalsforSimplexReduction = -(1L << 16);

        #region Constructors

        /// <summary>
        /// How many linear equalities?
        /// </summary>
        /// <param name="decoder">The decoder for expressions</param>
        public LinearEqualitiesForSubpolyhedraEnvironment(ExpressionManagerWithEncoder<Variable, Expression> expManager)
          : this(INITIAL_VARS_FOR_EQUATION_COUNT, INITIAL_EQUATIONS_COUNT, expManager)
        {
            Contract.Requires(expManager != null);
        }

        /// <summary>
        /// Build a Linear equalities environment with <code>n</code> dimensions
        /// </summary>
        /// <param name="var">The initial number of variables</param>
        /// <param name="equations">The initial number of equations</param>
        public LinearEqualitiesForSubpolyhedraEnvironment(int var, int equations,
          ExpressionManagerWithEncoder<Variable, Expression> expManager)
          : base(var, equations, expManager)
        {
            Contract.Requires(expManager != null);
        }

        /// <summary>
        /// A cosntructor used mainly for the internal operations of the abstract domain
        /// </summary>
        private LinearEqualitiesForSubpolyhedraEnvironment(SparseRationalArray[] matrix, int numOfRows, int numOfCols, VarsToDimensions varsToDimensions,
          ExpressionManagerWithEncoder<Variable, Expression> expManager)
          : base(matrix, numOfRows, numOfCols, varsToDimensions, expManager)
        {
            Contract.Requires(matrix != null);
            Contract.Requires(expManager != null);

            LogPerformance("matrix length: {0}; rows: {1}; cols: {2}; vars: {3}",
              matrix.Length.ToString(), numOfRows.ToString(), numOfCols.ToString(), varsToDimensions.Count.ToString());
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="l"></param>
        protected LinearEqualitiesForSubpolyhedraEnvironment(LinearEqualitiesEnvironment<Variable, Expression> l)
          : base(l)
        {
            Contract.Requires(l != null);
        }

        protected override LinearEqualitiesEnvironment<Variable, Expression> New(ExpressionManagerWithEncoder<Variable, Expression> expManager)
        {
            return new LinearEqualitiesForSubpolyhedraEnvironment<Variable, Expression>(expManager);
        }


        protected override LinearEqualitiesEnvironment<Variable, Expression> New(int var, int eqs, ExpressionManagerWithEncoder<Variable, Expression> expManager)
        {
            return new LinearEqualitiesForSubpolyhedraEnvironment<Variable, Expression>(var, eqs, expManager);
        }

        protected override LinearEqualitiesEnvironment<Variable, Expression> New(LinearEqualitiesEnvironment<Variable, Expression> other)
        {
            return new LinearEqualitiesForSubpolyhedraEnvironment<Variable, Expression>(other);
        }

        protected override LinearEqualitiesEnvironment<Variable, Expression> New(SparseRationalArray[] JoinedMatrix, int numberOfRowsInResult, int p, VarsToDimensions freshMap, ExpressionManagerWithEncoder<Variable, Expression> expManager)
        {
            return new LinearEqualitiesForSubpolyhedraEnvironment<Variable, Expression>(JoinedMatrix, numberOfRowsInResult, p, freshMap, expManager);
        }
        #endregion

        #region ICloneable Members

        /// <summary>
        /// A type safe version of <code>Clone</code>
        /// </summary>
        override protected LinearEqualitiesEnvironment<Variable, Expression> Duplicate()
        {
            return DuplicateInternal();
        }

        private LinearEqualitiesForSubpolyhedraEnvironment<Variable, Expression> DuplicateInternal()
        {
            return new LinearEqualitiesForSubpolyhedraEnvironment<Variable, Expression>(this);
        }

        #endregion

        protected IEnumerable<Variable> SlackVariables
        {
            get
            {
                var decoder = this.expManager.Decoder;
                foreach (var x in this.varsToDimensions.Keys)
                {
                    if (decoder.IsSlackVariable(x))
                    {
                        yield return x;
                    }
                }
            }
        }

        /// <summary>
        /// Gets a row equivalent to the first one modulo linear combinations with the equalities in the state, using the mapping from <paramref name="exp"/> to <paramref name="dim"/>
        /// </summary>
        /// <param name="initial">The original row</param>
        /// <param name="final">Will contain the result</param>
        /// <param name="exp">A variable for which we need a mapping</param>
        /// <param name="dim">The dimension for <paramref name="exp"/></param>
        /// <returns>true if we managed to find an equivalent of <paramref name="initial"/> without slack variables, false otherwise</returns>
        private bool GetEquivalentWithoutSlack(Polynomial<Variable, Expression> initial,
          out SparseRationalArray final, Variable exp, int dim, IDictionary<Variable, FList<Variable>> sourcesToTargets)
        {
            final = AsVector(initial);
            if (initial.IsTautology || initial.Variables.IsEmpty)
            {
                return false;
            }

            var source = default(Variable);
            var found = false;
            foreach (var pair in sourcesToTargets)
            {
                if (pair.Value.Head.Equals(exp))
                {
                    source = pair.Key;
                    found = true;

                    Contract.Assert(source != null);

                    break;
                }
            }
            try
            {
                if (initial.Variables.Count == 1)
                {// exp had a constant value, we try to see if some variable "source" with another constant value is renamed to exp and return the relation exp - source == k
                    int dimSource;

                    if (!found || !varsToDimensions.TryGetValue(source, out dimSource))
                    {
                        return false;
                    }

                    int rowSource = -1;

                    for (int i = this.NumberOfRows - 1; i >= 0; i--)
                    {
                        if (!this.equations.IsNullRow(i) && this.equations.At(i, dimSource).IsNotZero)
                        {
                            rowSource = i;

                            break;
                        }
                    }

                    if (rowSource < 0)
                    {
                        return false;
                    }

                    if (this.equations.IsConstantRow(rowSource) && this.equations.At(rowSource, dimSource) == 1)
                    {
                        final = FreshRow(numberOfCols);
                        final[dim] = Rational.For(1);
                        final[dimSource] = Rational.For(-1);
                        final[numberOfCols - 1] = (initial.Right[0].K / initial.Left[0].K) - this.equations.At(rowSource, numberOfCols - 1);

                        Contract.Assert(final[dim].IsNotZero);

                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    if (source == null || source.Equals(default(Expression)) || !varsToDimensions.ContainsKey(source))
                    {
                        this.varsToDimensions[exp] = dim;
                        Variable e;

                        var combinations = Combine(initial, SubPolyhedra<Variable, Expression>.IsBetaDefinition(initial, out e, this.expManager.Decoder), 0, MAX_VARS_FOR_COMBINE);

                        this.varsToDimensions.Remove(exp);
                        if (combinations.IsEmpty)
                        {
                            return false;
                        }
                        final = AsVector(combinations.PickAnElement());

                        Contract.Assert(final[dim].IsNotZero);

                        return true;
                    }
                    else
                    {
                        var copy = this.DuplicateInternal();

                        copy.AddConstraint(copy.AsVector(initial));

                        foreach (var e in this.Variables)
                        {
                            if (e.Equals(exp) || e.Equals(source))
                            {
                                continue;
                            }
                            copy.RemoveVariable(e, true);
                        }

                        var copy_matrix = copy.equations.Unshare(copy, ref copy.equations);

                        Contract.Assert(copy.NumberOfRows <= 2);

                        if (copy.NumberOfRows == 1 && !copy.equations.IsConstantRow(0))
                        {
                            final = copy_matrix[0];
                            Contract.Assert(final[dim] != 0);

                            return true;
                        }
                        else if (copy.NumberOfRows == 2)
                        {
                            final = copy_matrix[0];
                            foreach (var pair in copy.equations.GetElementsForRow(1))
                            {
                                final[pair.Key] -= pair.Value;
                            }

                            Contract.Assert(final[dim].IsNotZero);

                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            catch (ArithmeticExceptionRational)
            {
                return false;
            }
        }

        #region Specific algorithms for SubPolyhedra
        /// <summary>
        /// Tries to infer the best intervals for each variable in the domain 
        /// </summary>
#if SUBPOLY_ONLY
        internal
#else
        public
#endif
    IntervalEnvironment<Variable, Expression> BoundsInference(
              IntervalEnvironment<Variable, Expression> initialBounds,
              SubPolyhedra.ReductionAlgorithm algorithm, int MaxVariablesForSimplex)
        {
            Contract.Requires(initialBounds != null);
            Contract.Ensures(Contract.Result<IntervalEnvironment<Variable, Expression>>() != null);

            CheckStateInvariant();

            if (this.IsTop)
            {
                return initialBounds;
            }

            IntervalEnvironment<Variable, Expression> result;

            if (algorithm == SubPolyhedra.ReductionAlgorithm.Simplex)
            {
                this.expManager.Log("Running Simplex");
                result = RunSimplex(initialBounds, MaxVariablesForSimplex);
            }
            else if (algorithm == SubPolyhedra.ReductionAlgorithm.SimplexOptima)
            {
#if USESOLVERFOUNDATION

#if false
                var s1 = RunSimplex(initialBounds);
                var s2 = RunSimplex_OptimaModel(initialBounds);

                bool b1 = s1.LessEqual(s2);
                bool b2 = s2.LessEqual(s1);
                bool b = b1 && b2;

                if (!b)
                {
                    System.Diagnostics.Debugger.Break();
                }
                return s1;
#else
                var s2 = RunSimplex_OptimaModel(initialBounds);
                return s2;
#endif
#else
                result = RunSimplex(initialBounds, MaxVariablesForSimplex);
#endif
            }
            else
            {
                if (algorithm == SubPolyhedra.ReductionAlgorithm.MixSimplexFast)
                {
                    if (this.Variables.Count < 20)
                    {
                        this.expManager.Log("Running Simplex");
                        result = RunSimplex(initialBounds, MaxVariablesForSimplex);
                    }
                    else
                    {
                        this.expManager.Log("Running Fast");
                        result = RunBasisExploration(initialBounds, SubPolyhedra.ReductionAlgorithm.Fast);
                    }
                }
                else
                {
                    result = RunBasisExploration(initialBounds, algorithm);
                }
            }

            this.expManager.Log("Result of the reduction:\n{0}", result.ToString);

            return result;
        }

        private IntervalEnvironment<Variable, Expression> RunBasisExploration(
          IntervalEnvironment<Variable, Expression> initialBounds, SubPolyhedra.ReductionAlgorithm algorithm)
        {
            var watch = new CustomStopwatch();
            watch.Start();

            SparseRationalArray row;
            Interval interval, finiteInterval;
            int infLower, infUpper;
            int numberOfVariablesInBasis = 0;

            var result = initialBounds.Clone() as IntervalEnvironment<Variable, Expression>;

            Contract.Assert(result != null);

            #region Step 1 : put the less precise variables in the basis

            var matrix = this.equations.Unshare(this, ref this.equations);

            for (int i = 0; i < NumberOfRows; i++)
            {
                row = matrix[i];

                interval = result.EvalArray(row, this.varsToDimensions.InverseMap, this.NumberOfColumns - 1,
                  out finiteInterval, out infLower, out infUpper);

                if (interval.IsBottom)
                {
                    this.state = LinearEqualitiesState.Bottom;
                    return initialBounds;
                }

                var LessPrecise = default(Variable);
                var WorstPrecision = Rational.For(0);

                int infiniteInWorst = 0;
                bool foundLessPrecise = false;

                foreach (var pair in row.GetElements())
                {
                    if (pair.Key == this.NumberOfColumns - 1)
                    { // constant
                        continue;
                    }

                    var var = varsToDimensions[pair.Key];
                    var k = pair.Value;
                    var varInterval = k * result.BoundsFor(var).AsInterval;

                    Rational lowerKX, upperKX;

                    try
                    {
                        lowerKX = varInterval.UpperBound.IsPlusInfinity ?
                          (infUpper > 1 ? Rational.MinusInfinity : -finiteInterval.UpperBound)
                          : varInterval.UpperBound - interval.UpperBound;
                    }
                    catch (ArithmeticExceptionRational)
                    {
                        lowerKX = Rational.MinusInfinity;
                    }
                    try
                    {
                        upperKX = varInterval.LowerBound.IsMinusInfinity ?
                          (infLower > 1 ? Rational.PlusInfinity : -finiteInterval.LowerBound)
                          : varInterval.LowerBound - interval.LowerBound;
                    }
                    catch (ArithmeticExceptionRational)
                    {
                        upperKX = Rational.PlusInfinity;
                    }

                    var intervalForX = Interval.For(lowerKX, upperKX) / k /* (Interval.For(k))*/;

                    if (intervalForX.LessEqual(result.BoundsFor(var).AsInterval))
                    {
                        int eRow = -1;

                        if (!TrySwapColumnsInRowEchelonForm(i, varsToDimensions[var], out eRow))
                        {
                            RemoveRow(eRow);
                            PutIntoRowEchelonForm(); // should not change the rows already treated
                            if (eRow <= i)
                            {
                                i--;
                            }
                        }

                        if (eRow == -1)
                        {
                            int dummy;
                            if (!TrySwapColumnsInRowEchelonForm(numberOfVariablesInBasis, i, out dummy))
                            {
                                throw new AbstractInterpretationException("I was not expecting a failure here! Is it a bug?");
                            }

                            numberOfVariablesInBasis++;

                            Contract.Assert(numberOfVariablesInBasis <= NumberOfRows);
                        }

                        foundLessPrecise = true;
                        break;
                    }

                    var resInt = result.BoundsFor(var).AsInterval.Meet(intervalForX);

                    // Found a contradiction?
                    if (resInt.IsBottom)
                    {
                        return result.Bottom;
                    }

                    result[var] = resInt;

                    Rational diff;

                    try
                    { // F: I've added it
                        diff = resInt.UpperBound - resInt.LowerBound;
                    }
                    catch (ArithmeticExceptionRational)
                    {
                        diff = Rational.PlusInfinity;
                    }

                    int infiniteInDiff = (resInt.UpperBound.IsInfinity ? 1 : 0) + (resInt.LowerBound.IsInfinity ? 1 : 0);

                    if (diff > WorstPrecision || infiniteInDiff > infiniteInWorst)
                    {
                        WorstPrecision = diff;
                        infiniteInWorst = infiniteInDiff;
                        LessPrecise = var;
                    }
                }

                if (!foundLessPrecise && WorstPrecision > 0)
                {
                    int eRow;
                    if (!TrySwapColumnsInRowEchelonForm(i, varsToDimensions[LessPrecise], out eRow))
                    {
                        RemoveRow(eRow);
                        PutIntoRowEchelonForm();
                        if (eRow <= i)
                        {
                            i--;
                        }
                    }
                }
            }
            #endregion

            #region Step 2 : infer bounds for all the variables, trying different combinations for the variables not definitely in basis
            Explorer comb;
            var temp = new LinearEqualitiesForSubpolyhedraEnvironment<Variable, Expression>(this);
            temp.PutVariablesInFront();

            if (NumberOfVariables - numberOfVariablesInBasis < 0)
            {
                return result;
            }

            int[] previous = new int[NumberOfVariables - numberOfVariablesInBasis];
            for (int i = 0; i < previous.Length; i++)
            {
                previous[i] = i;
            }

            // If we abstracted away some row because of Rational overflows, 
            // it may have be the case that the invariant NumberOfRows - numberOfVariablesInBasis >= 0 does not hold anymore
            int range = Math.Max(NumberOfRows - numberOfVariablesInBasis, 0);

            switch (algorithm)
            {
                case SubPolyhedra.ReductionAlgorithm.Complete:
                    comb = new CombinatorialExplorer(NumberOfVariables - numberOfVariablesInBasis, range);
                    break;

                case SubPolyhedra.ReductionAlgorithm.Fast:
                    comb = new LinearExplorer(NumberOfVariables - numberOfVariablesInBasis, range);
                    break;

                default:
                    comb = new LinearExplorer(NumberOfVariables - numberOfVariablesInBasis, range);
                    break;
            }

            while (comb.MoveNext())
            {
                if (watch.Elapsed.Minutes >= 5)
                {
                    throw new TimeoutExceptionFixpointComputation("basis exploration");
                }

                for (int i = 0; i < comb.Current.Length; i++)
                {
                    if (previous[i] == comb.Current[i])
                    {
                        continue;
                    }

                    int j = Array.FindIndex(previous, delegate (int x) { return x == comb.Current[i]; });

                    if (temp.equations.At(i + numberOfVariablesInBasis, j + numberOfVariablesInBasis).IsZero)
                    {
                        for (int u = i + numberOfVariablesInBasis + 1; u < numberOfRows; u++)
                        {
                            if (temp.equations.At(u, j + numberOfVariablesInBasis).IsNotZero)
                            {
                                int dummy;
                                if (!temp.TrySwapColumnsInRowEchelonForm(u, j + numberOfVariablesInBasis, out dummy))
                                {
                                    return result;
                                }

                                if (!temp.TrySwapColumnsInRowEchelonForm(i + numberOfVariablesInBasis, u, out dummy))
                                {
                                    return result;
                                }

                                previous[j] = previous[u - numberOfVariablesInBasis];
                                previous[u - numberOfVariablesInBasis] = previous[i];
                                previous[i] = comb.Current[i];
                                break;
                            }
                        }
                    }
                    else
                    {
                        int dummy;
                        if (!temp.TrySwapColumnsInRowEchelonForm(i + numberOfVariablesInBasis, j + numberOfVariablesInBasis, out dummy))
                        {
                            return result;
                        }

                        previous[j] = previous[i];
                        previous[i] = comb.Current[i];
                    }
                }

                var temp_matrix = temp.equations.Unshare(temp, ref temp.equations);

                for (int i = 0; i < temp.NumberOfRows; i++)
                {
                    row = temp_matrix[i];
                    interval = result.EvalArray(row, temp.varsToDimensions.InverseMap, temp.NumberOfColumns - 1, out finiteInterval, out infLower, out infUpper);

                    if (interval.IsBottom)
                    {
                        this.state = LinearEqualitiesState.Bottom;
                        return initialBounds.Bottom;
                    }

                    foreach (var pair in row.GetElements())
                    {
                        if (pair.Key == temp.NumberOfColumns - 1)
                        {// constant
                            continue;
                        }

                        var var = temp.varsToDimensions[pair.Key];
                        var k = pair.Value;
                        var varInterval = k * result.BoundsFor(var).AsInterval;

                        Rational lowerKX, upperKX;
                        try
                        {
                            lowerKX = varInterval.UpperBound.IsPlusInfinity ?
                              (infUpper > 1 ? Rational.MinusInfinity : -finiteInterval.UpperBound) : varInterval.UpperBound - interval.UpperBound;
                        }
                        catch (ArithmeticExceptionRational)
                        {
                            lowerKX = Rational.MinusInfinity;
                        }

                        try
                        {
                            upperKX = varInterval.LowerBound.IsMinusInfinity ?
                              (infLower > 1 ? Rational.PlusInfinity : -finiteInterval.LowerBound) : varInterval.LowerBound - interval.LowerBound;
                        }
                        catch (ArithmeticExceptionRational)
                        {
                            upperKX = Rational.PlusInfinity;
                        }

                        var intervalForKX = Interval.For(lowerKX, upperKX);
                        var intervalForX = intervalForKX / k;
                        var resInt = result.BoundsFor(var).AsInterval.Meet(intervalForX);

                        if (resInt.IsBottom)
                        {
                            return result.Bottom;
                        }

                        result[var] = resInt;
                    }
                }
            }
            #endregion

            return result;
        }

#if false
        static TimeSpan TimeSpentInSimplex = new TimeSpan(0);
#endif

        private IntervalEnvironment<Variable, Expression> RunSimplex(IntervalEnvironment<Variable, Expression> initialBounds, int MaxVariables)
        {
#if TRACE_PERFORMANCE
            return PerformanceMeasure.Measure(PerformanceMeasure.ActionTags.Simplex, () => RunSimplexInternal(initialBounds, MaxVariables), true);
#else
            return RunSimplexInternal(initialBounds, MaxVariables);
#endif
        }

        /// <summary>
        /// Simplex algorithm : initializing the LP, then asking for the minimal solutions
        /// </summary>

        private IntervalEnvironment<Variable, Expression> RunSimplexInternal(IntervalEnvironment<Variable, Expression> initialBounds, int MaxVariables)
        {
#if TRACE_PERFORMANCE
            var watch = new CustomStopwatch();
            watch.Start();
#endif

            var result = initialBounds.Clone() as IntervalEnvironment<Variable, Expression>;

            if (result == null)
            {
                return initialBounds;
            }

            var allVars = Variables.SetUnion(initialBounds.Variables);

            if (allVars.Count >= MaxVariables)
            {
                return initialBounds;
            }

            var varsToDimsInLp = new List<Pair<Variable, int>>();

            int n = 0;
            foreach (var var in allVars)
            {
                varsToDimsInLp.Add(new Pair<Variable, int>(var, n++));
            }

#if false
            Console.WriteLine("Variables {0} (max {1})", n, UpdateMaxVarsInSimplex(n));
#endif
            LinearProgramInterface lp;
            if (TryInitializeLP(initialBounds, varsToDimsInLp, out lp))
            {
                var costs = new double[n];
                var varToMinimize = -1;

                var lowerBounds = new List<Pair<Variable, Rational>>();

                foreach (var toMinimizePair in varsToDimsInLp)
                {
                    if (varToMinimize != -1)
                    {
                        costs[varToMinimize] = 0;
                    }

                    varToMinimize = toMinimizePair.Two;

                    // Get the minimum
                    costs[varToMinimize] = 1;
                    lp.InitCosts(costs);
                    var min = lp.GetMinimalValue();

                    if (lp.Status == Status.Infeasible)
                    {
                        return initialBounds.Bottom;
                    }

                    if (lp.Status == Status.FloatingPointError)
                    {
                        return initialBounds;
                    }

                    var lower = lp.Status == Status.Optimal ?
                      Rational.ConvertFromDouble(min).LowerBound :
                      Rational.MinusInfinity;

                    lowerBounds.Add(new Pair<Variable, Rational>(toMinimizePair.One, lower));
                }

                var count = 0;
                foreach (var toMinimizePair in varsToDimsInLp)
                {
                    if (varToMinimize != -1)
                    {
                        costs[varToMinimize] = 0;
                    }

                    varToMinimize = toMinimizePair.Two;

                    // Get the maximum
                    costs[varToMinimize] = -1;
                    lp.InitCosts(costs);
                    var max = -lp.GetMinimalValue();
                    var upper = lp.Status == Status.Optimal ?
                        Rational.ConvertFromDouble(max).UpperBound : Rational.PlusInfinity;

                    if (lp.Status == Status.FloatingPointError)
                    {
                        return initialBounds;
                    }

                    var refinedBound = DisInterval.For(lowerBounds[count].Two, upper);

                    // The double-based Simplex may have introduced errors in the computation, and have wrong bounds
                    // If this is the case, we abstract the result of the reduction
                    var newBound = refinedBound.IsBottom
                      ? initialBounds.BoundsFor(toMinimizePair.One)
                      : refinedBound.Meet(initialBounds.BoundsFor(toMinimizePair.One));

                    result[toMinimizePair.One] = newBound.AsInterval;

#if DEBUG
                    if (newBound.IsBottom)
                    {
                        this.expManager.Log("Found a bottom in the reduction of " + toMinimizePair.One);
                    }
#endif

                    count++;
                }
            }
            else
            {
                this.expManager.Log("No reduction as the set of constraints is empty");
            }
#if false
            var span = watch.Elapsed;

            TimeSpentInSimplex += span;

            Console.WriteLine("Time spent in Simplex: {0} (total {1}, max {2})", (span).ToString(),
             TimeSpentInSimplex.ToString(),
             UpdateMaxSimplexTime(span));
#endif
            return result;
        }

#if false
        static TimeSpan maxSimplexTime = new TimeSpan(0);
        static TimeSpan UpdateMaxSimplexTime(TimeSpan s)
        {
            if (maxSimplexTime < s)
            {
                maxSimplexTime = s;
            }

            return maxSimplexTime;
        }

        static int maxVarsInSimplex = 0;
        static int UpdateMaxVarsInSimplex(int v)
        {
            if (v > maxVarsInSimplex)
            {
                maxVarsInSimplex = v;
            }

            return maxVarsInSimplex;
        }
#endif
        override internal LinearEqualitiesEnvironment<Variable, Expression> Join(IAbstractDomain a, ref List<Polynomial<Variable, Expression>> deletedLeft, ref List<Polynomial<Variable, Expression>> deletedRight)
        {
            IAbstractDomain trivialresult;
            if (AbstractDomainsHelper.TryTrivialJoin(this, a, out trivialresult))
            {
                if (trivialresult.IsTop)
                {
                    deletedLeft.AddRange(this.ToPolynomial(false));
                    deletedRight.AddRange((a as LinearEqualitiesEnvironment<Variable, Expression>).ToPolynomial(false));
                }
                return trivialresult as LinearEqualitiesEnvironment<Variable, Expression>;
            }

            var right = a as LinearEqualitiesForSubpolyhedraEnvironment<Variable, Expression>;

            Contract.Assert(a != null);

            // 1. Make the domains the same, by uniforming the maps, and swapping columns

            var t1 = Task.Run(
              () =>
              {
                  this.PutIntoRowEchelonForm();
                  this.RecoverUnusedColumns();
                  this.PutSlackVariablesInBasis();
              });

            var t2 = Task.Run(
              () =>
              {
                  right.PutIntoRowEchelonForm();
                  right.RecoverUnusedColumns();
                  right.PutSlackVariablesInBasis();
              });

            Task.WaitAll(t1, t2);

            UniformEnvironments(this, right);

            this.PutIntoWeakRowEchelonForm();
            right.PutIntoWeakRowEchelonForm();

            Task.WaitAll(t1, t2);

            if (this.state == LinearEqualitiesState.Bottom)
            {
                return new LinearEqualitiesForSubpolyhedraEnvironment<Variable, Expression>(right);
            }

            if (right.state == LinearEqualitiesState.Bottom)
            {
                return new LinearEqualitiesForSubpolyhedraEnvironment<Variable, Expression>(this);
            }

            Contract.Assert(this.NumberOfColumns == right.NumberOfColumns);

            // 2. The join is computed according to Karr 1976 paper
            var A = this.equations.GetClonedEquations();      // We clone the matrixes as Karr works with side effects
            var B = right.equations.GetClonedEquations();

            int numberOfRowsInResult;
            var JoinedMatrix = DisjunctionOfLinearSpaces(A, B, this.NumberOfRows, right.numberOfRows, out numberOfRowsInResult, ref deletedLeft, ref deletedRight);

            // 3. Create a clone of the current private state
            var freshMap = new VarsToDimensions(this.varsToDimensions);

            var result = new LinearEqualitiesForSubpolyhedraEnvironment<Variable, Expression>(JoinedMatrix, numberOfRowsInResult, this.NumberOfColumns, freshMap, this.expManager);

            result.state = LinearEqualitiesState.Normal; // the condition implied by Karr's algorithm is weaker than what we call now row echelon form

            return result;
        }

        internal void RemoveRedundantSlackVariables()
        {
            this.PutIntoRowEchelonForm();

            this.expManager.Log("Matrix before removing equalities var == slackvar:\n {0}", this.ToString);

            var equalities = new List<LinearEqualityRedundancy<Variable>>();

            for (var row = 0; row < this.NumberOfRows; row++)
            {
                if (this.equations.RowCount(row) != 2)
                {
                    continue;
                }

                Variable v1 = default(Variable), v2 = default(Variable);
                Rational k1 = Rational.For(0), k2 = Rational.For(0);

                bool first = true;
                int varCount = 0;

                foreach (var pair in this.equations.GetElementsForRow(row))
                {
                    if (pair.Key == this.NumberOfColumns - 1)
                    {
                        continue;
                    }
                    else if (first)
                    {
                        v1 = this.varsToDimensions[pair.Key];
                        k1 = pair.Value;
                        first = false;
                        varCount++;
                    }
                    else
                    {
                        v2 = this.varsToDimensions[pair.Key];
                        k2 = pair.Value;
                        varCount++;
                    }
                }

                if (varCount != 2 ||
                  ((!this.ExpressionManager.Decoder.IsSlackVariable(v1) && !this.ExpressionManager.Decoder.IsSlackVariable(v2))))
                {
                    continue;
                }

                if (((k1 == 1 && k2 == -1) || (k1 == -1 && k2 == 1)) || (k1 == 1 && k2 == 1))
                {
                    if (this.ExpressionManager.Decoder.IsSlackVariable(v1))
                    {
                        var tmp = v1;
                        v1 = v2;
                        v2 = tmp;
                    }

                    int signK = 1;

                    if (k1 == 1 && k2 == 1)
                    {
                        signK = -1;
                    }

                    // Now remove 
                    int v1Col, v2Col;
                    if (this.varsToDimensions.TryGetValue(v1, out v1Col)  // Should always succeed?
                      && this.varsToDimensions.TryGetValue(v2, out v2Col)) // it may be the case we already removed eq.V2
                    {
                        var matrix = this.equations.Unshare(this, ref this.equations);
                        for (int i = 0; i < this.NumberOfRows; i++)
                        {
                            var currRow = matrix[i];
                            if (currRow != null && i != row)
                            {
                                Rational value;
                                if (currRow.IsIndexOfNonDefaultElement(v2Col, out value))
                                {
                                    value = value * signK;

                                    var oldVal = currRow[v1Col];
                                    currRow[v1Col] = oldVal + value;
                                    currRow[v2Col] = Rational.For(0);
                                }
                            }
                        }

                        Contract.Assert(this.ExpressionManager.Decoder.IsSlackVariable(v2));

                        var newMapping = new VarsToDimensions(this.varsToDimensions);
                        if (newMapping.ContainsKey(v2))
                        {
                            newMapping.Remove(v2);
                        }

                        RemoveRow(matrix, row);
                        row--;

                        // Checking code
                        CheckAreAllZeroAt(matrix, v2, row, true);

                        this.equations = new LinearEquations<Variable, Expression>(this, matrix);
                        this.numberOfRows--;
                    }
                }
            }

            this.expManager.Log("Matrix after removing simple equalities : {0}", this.ToString);

            return;
        }

        /// <summary>
        /// The specific AssignInParallel for SubPolyhedra, which takes care of updating the dependencies
        /// </summary>
        /// <param name="sourcesToTargetsInitial">The renaming map</param>
        /// <param name="betaDep">The dependencies to update</param>
        /// <param name="hints">Keeps track of deleted dependencies, to be used at joins or widenings</param>
        internal void AssignInParallel(Dictionary<Variable, FList<Variable>> sourcesToTargetsInitial, Converter<Variable, Expression> convert,
          ref Dictionary<Variable, Polynomial<Variable, Expression>> betaDep, ref Hints<Variable, Expression> hints)
        {
            Contract.Requires(this.IsConsistent, "LinEq inconstistent @ AssignInParallel entry");

            var tmp = new LinearEqualitiesForSubpolyhedraEnvironment<Variable, Expression>(this);

            // adding the domain-generated variables to the map as identity
            var sourcesToTargets = new Dictionary<Variable, FList<Variable>>(sourcesToTargetsInitial);

            if (!this.IsTop)
            {
                foreach (var e in this.SlackVariables)
                {
                    sourcesToTargets.Add(e, FList<Variable>.Cons(e, FList<Variable>.Empty));
                }
            }

            #region Compute the statistics on the renaming

            var pairs = 0;
            foreach (var pair in sourcesToTargets)
            {
                if (pair.Value.Length() > 1)
                {
                    pairs++;
                }
            }

            #endregion

            #region If statistics are bad, then do a very simple renaming and we return

            if (pairs > AdaptiveRenamingInSubpolyhedraThreshold)
            {
                var variablesToRemove = new List<Variable>();
                var newNames = new Dictionary<Variable, Variable>();

                foreach (var v in this.Variables_Internal)
                {
                    FList<Variable> renamings;
                    if (sourcesToTargets.TryGetValue(v, out renamings))
                    {
                        newNames[v] = renamings.Head;
                    }
                    else
                    {
                        variablesToRemove.Add(v);
                    }
                }

                // now remove all the variables lost in the translation
                foreach (var v in variablesToRemove)
                {
                    this.RemoveVariable(v);
                }

                // and finally rename the survicing variables
                foreach (var pair in newNames)
                {
                    this.RenameVariable(pair.Key, pair.Value);
                }

                // We are done!
                return;
            }

            #endregion
            var toSkip = new Set<Variable>();

            foreach (var x in this.varsToDimensions.Keys)
            {
                if (!sourcesToTargets.ContainsKey(x))
                {
                    if (!toSkip.Contains(x)) // otherwise, it already represents a target variable and should not be removed
                    {
                        tmp.RemoveInAssignInParallel(x, ref betaDep, ref hints, sourcesToTargets);
                    }
                    continue;
                }

                var targets = sourcesToTargets[x];

                // choosing the canonical element
                var target =
                  (targets.Length() > 1 && x.Equals(targets.Head)) ?
                  targets.Tail.Head :
                  targets.Head;

                if (x.Equals(target))
                { // If it is the identity there is nothing to do...
                    continue;
                }

                if (tmp.varsToDimensions.ContainsKey(target))
                {
                    tmp.RemoveInAssignInParallel(target, ref betaDep, ref hints, sourcesToTargets);
                    toSkip.Add(target);
                }

                int dimX = tmp.varsToDimensions[x];

                tmp.varsToDimensions.Remove(x);

                tmp.varsToDimensions[target] = dimX;

                var toRemove = SubPolyhedra<Variable, Expression>.UpdateBetaDeps(x, sourcesToTargets[x].Head, ref betaDep);

                foreach (var beta in toRemove)
                {
                    hints.Add(betaDep[beta]);
                }

                foreach (var beta in toRemove)
                {
                    betaDep.Remove(beta);
                }

                SubPolyhedra<Variable, Expression>.UpdateHints(x, sourcesToTargets[x].Head, ref hints);

                this.expManager.Log("Result : {0}", tmp.ToString);

                Contract.Assert(tmp.IsConsistent);
            }

            // now adding equalities between targets of a same source
            foreach (var pair in sourcesToTargets)
            {
                var length = pair.Value.Length();
                if (length <= 1)
                {
                    continue;
                }

                if (base.PrintWarnsForLargeTargets && length > 10)
                {
                    Console.WriteLine("[SUBPOLYHEDRA] Found more than 10 pairwise equalities!!! (exactly {0})", length);
                }

                var target = pair.Key.Equals(pair.Value.Head) ? pair.Value.Tail.Head : pair.Value.Head;
                var targetExp = convert(target);

                foreach (var otherTarget in pair.Value.GetEnumerable())
                {
                    if (target.Equals(otherTarget))
                    {
                        continue;
                    }

                    var otherTargetExp = convert(otherTarget);

                    tmp = tmp.TestTrueEqual(targetExp, otherTargetExp) as LinearEqualitiesForSubpolyhedraEnvironment<Variable, Expression>;

                    Contract.Assert(tmp != null);
                }
            }

            // We change the state of this, using tmp
            CloneThisFrom(tmp);

            Contract.Assert(this.IsConsistent, "LinEq inconstistent @ AssignInParallel exit");
        }


        private static bool AreSmallNumbers(Int64 up, Int64 down)
        {
            return -MaxCoefficientInEquationsforSimplexReduction <= up && up <= MaxCoefficientInEquationsforSimplexReduction && -MaxCoefficientInEquationsforSimplexReduction <= down && down <= MaxCoefficientInEquationsforSimplexReduction;
        }

        private bool TryInitializeLP(IntervalEnvironment<Variable, Expression> bounds, List<Pair<Variable, int>> varsToDimsInLP, out LinearProgramInterface lp)
        {
            var equations = 0;
            var intervals = 0;

            lp = new RevisedSimplexMethod();

            this.expManager.Log("Equalities for the LP problem:\n");

            for (int i = 0; i < NumberOfRows; i++)
            {
                var equality = new double[varsToDimsInLP.Count];

                foreach (var pair in varsToDimsInLP)
                {
                    var var = pair.One;

                    if (this.ContainsKey(var))
                    {
                        var j = varsToDimensions[var];
                        var jlp = pair.Two;

                        var v = this.equations.At(i, j);

                        if (!AreSmallNumbers(v.Up, v.Down))
                        {
                            this.expManager.Log(string.Format("Skipped the equation because of {0}", v));

                            goto nextEquation;
                        }

                        equality[jlp] = (double)v;
                    }
                }

                var k = this.equations.At(i, NumberOfColumns - 1);

                if (!AreSmallNumbers(k.Up, k.Down))
                {
                    this.expManager.Log(string.Format("Skipped the equation because of the known coefficient {0}", k));

                    goto nextEquation;
                }

                this.expManager.Log(string.Format("{0} = {1}", equality.ToStringInDepth(), k.ToString()));

                lp.AddConstraint(equality, Relation.Equal, (double)k);

                equations++;

            nextEquation:
                ;
            }
            if (NumberOfRows == 0 || equations == 0)
            {
                lp = default(LinearProgramInterface);
                return false;
                //lp.AddConstraint(new double[varsToDimsInLP.Count], Relation.Equal, 0);
            }

            this.expManager.Log(string.Format("Intervals for the LP problem:\n{0}", bounds.ToString()));

            foreach (var pair in varsToDimsInLP)
            {
                var var = pair.One;

                Interval intv;
                if (bounds.TryGetValue(var, out intv))
                {
                    if (!intv.UpperBound.IsPlusInfinity)
                    {
                        if (intv.UpperBound < MaxBoundInIntervalsforSimplexReduction)
                        {
                            lp.LimitVariableFromAbove(pair.Two, (double)intv.UpperBound);
                            intervals++;
                        }
                        else
                        {
                            this.expManager.Log(string.Format("Upper bound skipped {0}\n", intv.UpperBound));
                        }
                    }
                    if (!intv.LowerBound.IsMinusInfinity)
                    {
                        if (intv.LowerBound > MinBoundInIntervalsforSimplexReduction)
                        {
                            lp.LimitVariableFromBelow(pair.Two, (double)intv.LowerBound);
                            intervals++;
                        }
                        else
                        {
                            this.expManager.Log(string.Format("Lower bound skipped {0}\n", intv.LowerBound));
                        }
                    }
                }
            }

            lp.Status = Status.Unknown;

            return true;
        }

        private void PutVariablesInFront()
        {
            int i = 0;
            for (int j = 0; j < NumberOfColumns - 1; j++)
            {
                Variable var;
                if (varsToDimensions.TryGetValue(j, out var))
                {
                    int dummy;
                    if (!TrySwapColumnsInRowEchelonForm(i, j, out dummy))
                    {
                        throw new AbstractInterpretationException("Bug!!!");
                    }

                    i++;
                }
            }

            Contract.Assert(i == NumberOfVariables);
        }

        /// <summary>
        /// Get all the slack variables that are duplicates (up to an affine transformation) of another slack variable.
        /// Returns a dictionary d such that d(e1) = (k, e2, c) iff (k != 0 AND e1 + k * e2 == c) OR (k == 0 AND \exists c e1 == c)
        /// </summary>
        /// <returns>A dictionary indexed by redundant variables, to be removed by the caller</returns>
        internal List<LinearEqualityRedundancy<Variable>> GetRedundancies()
        {
            this.PutSlackVariablesOutOfBasis();

            var toRemove = new List<LinearEqualityRedundancy<Variable>>();
            var remaining = new List<int>(); // will contain row indices corresponding to equalities between slack variables not in the required form

            var zero = Rational.For(0);
            var equations = this.equations;
            var decoder = this.ExpressionManager.Decoder;

            #region Find equalities b1 + k * b2 == c directly
            for (int i = 0; i < this.NumberOfRows; i++)
            {
                var redundant = true;
                var exp = default(Variable);
                var target = default(Variable);
                var coeff1 = zero;
                var coeff2 = zero;

                int found = 0;

                for (int j = 0; j < this.NumberOfColumns - 1; j++)
                {
                    var eq_i_j = equations.At(i, j);

                    if (eq_i_j.IsZero)
                    {
                        continue;
                    }

                    var dim_J = this.varsToDimensions[j];

                    if (!decoder.IsSlackVariable(dim_J))
                    {
                        redundant = false;
                        break;
                    }

                    if (found == 0)
                    {
                        exp = dim_J;
                        coeff1 = eq_i_j;
                    }

                    if (found == 1)
                    {
                        target = dim_J;
                        coeff2 = eq_i_j;
                    }
                    found++;
                }

                if (redundant)
                {
                    if (found == 1)
                    {
                        toRemove.Add(new LinearEqualityRedundancy<Variable>(exp, zero, default(Variable), equations.At(i, NumberOfColumns - 1)));
                    }
                    else if (found == 2)
                    {
                        toRemove.Add(new LinearEqualityRedundancy<Variable>(exp, coeff2 / coeff1, target, equations.At(i, numberOfCols - 1) / coeff1));
                    }
                    else
                    {
                        remaining.Add(i);
                    }
                }
            }
            #endregion

            #region Refinement of "if x + p == 0 and y + p == 0 then x == y"
            var remainingCount = remaining.Count;
            if (remainingCount >= 0)
            {
                var removed = new bool[remainingCount];
                for (int i = 0; i < remainingCount; i++)
                {
                    // if any variable varj is "equivalent" to vari, it is also equivalent to the target of vari and has alredy been marked as such
                    if (removed[i])
                    {
                        continue;
                    }

                    for (int j = i + 1; j < remainingCount; j++)
                    {
                        // we have :
                        // vari + pi == ki
                        // varj + pj == kj
                        // now we are trying to see if pi and pj are colinear

                        // 0 for both rows contain only zeros so far, 1 for rows are colinear and non-zero, 2 for rows are not (strictly) colinear
                        var colinearity = 0;
                        var coeff = zero; // if colinearity != 2 then row j == coeff * row i
                        var remaining_i = remaining[i];
                        var remaining_j = remaining[j];

                        for (int k = this.numberOfRows; k < this.numberOfCols - 1; k++)
                        {
                            if (colinearity == 0)
                            {
                                if (equations.At(remaining_i, k).IsNotZero)
                                {
                                    if (equations.At(remaining_j, k).IsNotZero)
                                    {
                                        colinearity = 1;

                                        if (!Rational.TryDiv(equations.At(remaining_j, k), equations.At(remaining_i, k), out coeff))
                                        {
                                            // If we get an overflow from the division above, we skip this constraint and move to the new one
                                            goto NextIteration;
                                        }
                                    }
                                    else
                                    {
                                        colinearity = 2;
                                        break;
                                    }
                                }
                                else
                                {
                                    if (equations.At(remaining_j, k).IsNotZero)
                                    {
                                        colinearity = 2;
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                try
                                {
                                    if (coeff * equations.At(remaining_i, k) != equations.At(remaining_j, k))
                                    {
                                        colinearity = 2;
                                        break;
                                    }
                                }
                                catch (ArithmeticExceptionRational)
                                {
                                    // We skip the constraint and move to the nexr
                                    goto NextIteration;
                                }
                            }
                        }

                        if (colinearity == 1)
                        {
                            // we have :
                            // ExpForI + p == k1
                            // ExpForJ + coeff * p == k2
                            // for some p, so ExpForJ - coeff * ExpForI == k2 - coeff * k1

                            //var remaining_i = remaining[i];
                            // var remaining_j = remaining[j];

                            Variable ExpForI, ExpForJ;
                            if (this.varsToDimensions.TryGetValue(remaining_i, out ExpForI)
                              && this.varsToDimensions.TryGetValue(remaining_j, out ExpForJ))
                            {
                                Rational value;
                                try
                                {
                                    value = equations.At(remaining_j, numberOfCols - 1)/*k2*/ - coeff * equations.At(remaining_i, numberOfCols - 1)/*k1*/;
                                }
                                catch (ArithmeticExceptionRational)
                                {
                                    // We abstract it away
                                    goto NextIteration;
                                }

                                toRemove.Add(new LinearEqualityRedundancy<Variable>(ExpForJ, -coeff, ExpForI, value));
                                removed[j] = true;
                            }
                            else
                            {
                                goto NextIteration;
                            }
                        }

                    NextIteration:
                        ;
                    }
                }
            }
            #endregion

            foreach (var e in this.Variables)
            {
                if (decoder.IsSlackVariable(e) && equations.IsZeroCol(this.NumberOfRows, varsToDimensions[e]))
                {
                    var dummy = new LinearEqualityRedundancy<Variable>(e, zero, default(Variable), Rational.PlusInfinity);
                    toRemove.Add(dummy);
                }
            }

            return toRemove;
        }

        internal void PropagateValue(Variable exp, ref IntervalEnvironment<Variable, Expression> bounds)
        {
            var j = this.varsToDimensions[exp];
            for (var i = 0; i < this.NumberOfRows; i++)
            {
                if (this.equations.At(i, j).IsZero)
                {
                    continue;
                }

                var pol = this.ToPolynomial(i);

                if (pol.Left.Length != 2)
                {
                    continue;
                }

                var forExp = bounds.BoundsFor(exp);
                Rational kexp, kother;
                Variable other;

                if (pol.Left[0].VariableAt(0).Equals(exp))
                {
                    kexp = pol.Left[0].K;
                    kother = pol.Left[1].K;
                    other = pol.Left[1].VariableAt(0);
                }
                else
                {
                    kexp = pol.Left[1].K;
                    kother = pol.Left[0].K;
                    other = pol.Left[0].VariableAt(0);
                }
                bounds[other] = bounds.BoundsFor(other).AsInterval.Meet(Interval.For(pol.Right[0].K / kother) - Interval.For(kexp / kother) * forExp.AsInterval);
            }
        }

        /// <summary>
        /// Removes all rows that only contain one variable (x == k), and returns a map from the expression removed to the associated constants
        /// </summary>
        /// <returns>A map from variables to their constant value</returns>
        internal Dictionary<Variable, Rational> GetConstants()
        {
            var result = new Dictionary<Variable, Rational>();
            int numberOfVars;
            Variable exp = default(Variable);

            for (int i = 0; i < this.numberOfRows; i++)
            {
                numberOfVars = 0;

                foreach (var pair in this.equations.GetElementsForRow(i))
                {
                    if (pair.Value.IsNotZero && pair.Key < this.numberOfCols - 1)
                    {
                        numberOfVars++;
                        if (numberOfVars == 1)
                        {
                            exp = this.varsToDimensions[pair.Key];
                        }
                    }
                }
                if (numberOfVars == 1)
                {
                    result.Add(exp, this.equations.At(i, this.numberOfCols - 1));
                    RemoveRow(i--);
                }
            }
            return result;
        }

        /// <summary>
        /// In row echelon form, put all slack variables in basis to try to get meaningfull information from dropped constraints at the join
        /// </summary>
        private void PutSlackVariablesInBasis()
        {
            Contract.Assert(this.state == LinearEqualitiesState.StrongRowEchelon);

            foreach (var x in this.Variables)
            {
                if (!this.expManager.Decoder.IsSlackVariable(x))
                {
                    continue;
                }

                // already in basis
                if (varsToDimensions[x] < numberOfRows)
                {
                    continue;
                }

                int j = varsToDimensions[x];
                int row = -1;
                for (int i = 0; i < numberOfRows; i++)
                {
                    if (this.equations.At(i, j).IsNotZero && !expManager.Decoder.IsSlackVariable(varsToDimensions[i]))
                    {
                        row = i;
                        break;
                    }
                }

                if (row != -1)
                {
                    int eRow;

                    if (!TrySwapColumnsInRowEchelonForm(row, j, out eRow))
                    {
                        RemoveRow(eRow);
                        PutIntoRowEchelonForm();
                    }
                }
            }
        }

        /// <summary>
        /// In row echelon form, put all slack variables in basis to try to get as much redundancies as possible
        /// </summary>
        private void PutSlackVariablesOutOfBasis()
        {
            Contract.Requires(this.state == LinearEqualitiesState.StrongRowEchelon);

            foreach (var x in this.Variables)
            {
                if (expManager.Decoder.IsSlackVariable(x) || varsToDimensions[x] < numberOfRows) // Already in the basis
                {
                    continue;
                }

                int j = varsToDimensions[x];
                int row = -1;
                for (int i = 0; i < numberOfRows; i++)
                {
                    if (this.equations.At(i, j).IsNotZero
                      && this.varsToDimensions.ContainsValue(i)  // F : I've added the test, as I cannot convince myself that InverseMap[i] is always there
                      && expManager.Decoder.IsSlackVariable(varsToDimensions[i]))
                    {
                        row = i;
                        break;
                    }
                }

                if (row != -1)
                {
                    int dummy;
                    if (!TrySwapColumnsInRowEchelonForm(row, j, out dummy))
                    {
                        RemoveRow(dummy);
                        PutIntoRowEchelonForm();
                    }
                }
            }
        }

        internal void ProjectSlackVariables()
        {
            var decoder = expManager.Decoder;
            foreach (var e in this.Variables)
            {
                if (decoder.IsSlackVariable(e))
                {
                    this.ProjectVariable(e);
                }
            }
        }

        private void RemoveInAssignInParallel(Variable x,
          ref Dictionary<Variable, Polynomial<Variable, Expression>> betaDep, ref Hints<Variable, Expression> hints,
          Dictionary<Variable, FList<Variable>> sourcesToTargets)
        {
            Polynomial<Variable, Expression> deletedRow;
            int d;

            this.RemoveVariable(x, false, out deletedRow, out d);

            SparseRationalArray withoutSlack;
            if (this.GetEquivalentWithoutSlack(deletedRow, out withoutSlack, x, d, sourcesToTargets))
            { // this part is supposed to update the dependencies by replacing the variable x, which is deleted, by a polynomial p such that x == p is provable in the pre-state
              // the polynomial p is represented by withoutSlack, and has the property that it doesn't contain any slack variable ; withoutSlack corresponds to coeff*x == p, not to p only
                var slackVariables = new List<Variable>(betaDep.Keys);
                foreach (var beta in slackVariables)
                {
                    if (betaDep[beta].Variables.Contains(x))
                    { // here we know that beta depends on x, so we need to replace it
                        var updated = new List<Monomial<Variable>>();
                        var k = betaDep[beta].Relation == null ? Rational.For(0) : betaDep[beta].Right[0].K;
                        foreach (var m in betaDep[beta].Left)
                        {
                            if (m.IsConstant || !m.VariableAt(0).Equals(x))
                            { // keep a copy of the old monomials which don't contain x
                                updated.Add(new Monomial<Variable>(m.K, m.VariableAt(0)));
                            }
                            else
                            { // here we have the monomial m.K * x, so we need to replace it using withoutSlack
                                var coeff = withoutSlack[d]; // the coefficient for x
                                foreach (var pair in withoutSlack.GetElements())
                                {
                                    if (pair.Key == d)
                                        continue;
                                    if (pair.Key == this.NumberOfColumns - 1)
                                    { // coeff*x == -k1*y1 - ... - kn*yn + pair.Value, so we replace x by pair.Value / coeff, multiply by the coefficient m.K of x in the previous polynomial, and take the opposite as we will put the conatant on the other side of the equality
                                        k -= m.K * pair.Value / coeff;
                                    }
                                    else
                                    {
                                        updated.Add(new Monomial<Variable>(-m.K * pair.Value / coeff, this.varsToDimensions[pair.Key]));
                                    }
                                }
                            }
                        }
                        var constant = new Monomial<Variable>[] { new Monomial<Variable>(k) };

                        Polynomial<Variable, Expression> dep;

                        if (Polynomial<Variable, Expression>.TryToPolynomialForm(ExpressionOperator.Equal, updated.ToArray(), constant, out dep))
                        {
                            if (dep.Variables.Count > 2)
                                betaDep[beta] = dep;
                            else
                                betaDep.Remove(beta);
                        }
                        else
                        {
#if DEBUG
                            Contract.Assert(false); // We should never get to this point
#endif
                            return;
                        }
                    }
                }
                var newhints = new List<Polynomial<Variable, Expression>>();
                foreach (var pol in hints.Enumerate())
                {
                    if (pol.Variables.Contains(x))
                    { // here we know that pol depends on x, so we need to replace it
                        var updated = new List<Monomial<Variable>>();
                        var k = pol.Relation == null ? Rational.For(0) : pol.Right[0].K;
                        foreach (var m in pol.Left)
                        {
                            if (m.IsConstant || !m.VariableAt(0).Equals(x))
                            { // keep a copy of the old monomials which don't contain x
                                updated.Add(new Monomial<Variable>(m.K, m.VariableAt(0)));
                            }
                            else
                            { // here we have the monomial m.K * x, so we need to replace it using withoutSlack
                                var coeff = withoutSlack[d]; // the coefficient for x
                                foreach (var pair in withoutSlack.GetElements())
                                {
                                    if (pair.Key == d)
                                        continue;
                                    if (pair.Key == this.NumberOfColumns - 1)
                                    { // coeff*x == -k1*y1 - ... - kn*yn + pair.Value, so we replace x by pair.Value / coeff, multiply by the coefficient m.K of x in the previous polynomial, and take the opposite as we will put the conatant on the other side of the equality
                                        k -= m.K * pair.Value / coeff;
                                    }
                                    else
                                    {
                                        updated.Add(new Monomial<Variable>(-m.K * pair.Value / coeff, this.varsToDimensions[pair.Key]));
                                    }
                                }
                            }
                        }
                        var constant = new Monomial<Variable>[] { new Monomial<Variable>(k) };

                        Polynomial<Variable, Expression> dep;
                        Polynomial<Variable, Expression>.TryToPolynomialForm(ExpressionOperator.Equal, updated.ToArray(), constant, out dep);

                        if (dep.Variables.Count > 2)
                        {
                            newhints.Add(dep);
                        }
                    }
                    else
                    {
                        newhints.Add(pol);
                    }
                }
                hints.Clear();
                hints.Add(newhints);
            }
            else
            { // no equivalent, removing all dependencies and hints on x
                var vars = new List<Variable>(betaDep.Keys);
                foreach (var e in vars)
                {
                    if (betaDep[e].Variables.Contains(x))
                    {
                        betaDep.Remove(e);
                    }
                }
                var nHints = new Hints<Variable, Expression>();
                foreach (var p in hints.Enumerate())
                {
                    if (!p.Variables.Contains(x))
                        nHints.Add(p);
                }
                hints = nHints;
            }
        }
        #endregion
    }


    /// <summary>
    /// A class for exploring combinations of m values between 0 and n - 1
    /// </summary>
    public abstract class Explorer : IEnumerator<int[]>
    {
        protected int n;
        protected int m;
        protected int[] current;

        public Explorer(int n, int m)
        {
            Contract.Requires(m <= n);

            this.n = n;
            this.m = m;
            this.current = null;
        }

        #region IEnumerator<int[]> Members

        public int[] Current
        {
            get { return current; }
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
        }

        #endregion

        #region IEnumerator Members

        object System.Collections.IEnumerator.Current
        {
            get { return current; }
        }

        public abstract bool MoveNext();

        public void Reset()
        {
            current = null;
        }

        #endregion
    }

    public class CombinatorialExplorer : Explorer
    {
        /// <summary>
        /// Explores all possible combinations of m values in 0 ... n - 1
        /// </summary>
        /// <param name="n"></param>
        /// <param name="m"></param>
        public CombinatorialExplorer(int n, int m)
          : base(n, m)
        {
        }

        public override bool MoveNext()
        {
            int i;
            if (current == null)
            {
                current = new int[m];
                for (i = 0; i < m; i++)
                {
                    current[i] = i;
                }
                return true;
            }
            i = m - 1;
            if (i < 0) return false;
            while (current[i] >= i + n - m)
            {
                i--;
                if (i < 0) return false;
            }
            current[i]++;
            int k = current[i];
            for (int j = i + 1; j < m; j++)
            {
                k++;
                current[j] = k;
            }
            return true;
        }
    }

    public class LinearExplorer : Explorer
    {
        private int next;

        public LinearExplorer(int n, int m)
          : base(n, m)
        {
            Contract.Assert(m >= 0);

            next = m;
        }

        public override bool MoveNext()
        {
            if (current == null)
            {
                current = new int[m];
                for (int j = 0; j < m; j++)
                {
                    current[j] = j;
                }
                return true;
            }
            if (m == 0)
            {
                return false;
            }

            if (next >= n)
            {
                return false;
            }
            int i = next % m;
            current[i] = next;
            next++;
            return true;
        }
    }
}