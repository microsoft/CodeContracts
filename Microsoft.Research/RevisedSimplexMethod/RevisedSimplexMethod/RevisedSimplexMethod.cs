// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;

namespace Microsoft.Glee.Optimization
{
    [Serializable]
    public class RevisedSimplexMethod : LinearProgramInterface
    {
        #region Object Invariant
        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(_nVars >= 0);
            Contract.Invariant(_nSlacks >= 0);
            //Contract.Invariant(epsilon > 0.0f);
            Contract.Invariant(_maxForConstraint > 0.0f);
            Contract.Invariant(_constraints != null);
        }
        #endregion

        #region Fields

        private readonly List<Constraint> _constraints = new List<Constraint>();
        private double[] _costs;
        private readonly double _epsilon = 10.0E-8;
        private double _epsilonForArtificials = 0.0001;
        private double _etaEpsilon = 1.0E-7;
        private int[] _forbiddenPairs;
        private Dictionary<int, double> _knownValues;
        private bool _lookForZeroColumns;
        private bool[] _lowBoundIsSet;
        private double[] _lowBounds;

        public RevisedSimplexMethod()
        {
        }

        /// <summary>
        /// each constraint is scaled into maxInterval 
        /// </summary>
        private readonly double _maxForConstraint = 100;

        private readonly double _minForConstraint = 100;
        private int _nArtificials;
        private int _nSlacks;
        private int _nVars;
        [NonSerialized]
        private RevisedSolver _solver;
        private Status _status = Status.Unknown;

        private bool[] _upperBoundIsSet;
        private double[] _upperBounds;
        private bool _useScaling;
        private double[] _varScaleFactors; //we replace each variable x[i] by varScaleFactors[i]*x[i]

        public bool UseScaling
        {
            get { return _useScaling; }
            set { _useScaling = value; }
        }

        internal int NVars
        {
            get
            {
                Contract.Ensures(Contract.Result<int>() >= 0);
                return _nVars;
            }
            set
            {
                Contract.Requires(value >= 0);

                _nVars = value;
                _lowBoundIsSet = new bool[_nVars];
                _lowBounds = new double[_nVars];
                _upperBoundIsSet = new bool[_nVars];
                _upperBounds = new double[_nVars];
            }
        }

        public bool LookForZeroColumns
        {
            get { return _lookForZeroColumns; }
            set { _lookForZeroColumns = value; }
        }

        #endregion


        #region LinearProgramInterface Members

        public double GetMinimalValue()
        {
            if (Status == Status.Unknown || Status == Status.Feasible)
            {
                Minimize();
            }
            if (Status == Status.Optimal)
            {
                double[] sol = MinimalSolution();

                return -LP.DotProduct(sol, _costs, _costs.Length); //minus since we reversed the costs
            }
            return 0;
        }


        public int[] ForbiddenPairs
        {
            get { return _forbiddenPairs; }
            set { _forbiddenPairs = value; }
        }

        public double Epsilon
        {
            get { return _epsilon; }
            //set { epsilon = value; }
        }

        [ContractVerification(false)]
        public double[] FeasibleSolution()
        {
            int reps = 0;
            Status stat = _status;
            do
            {
                if (reps > 0)
                    _status = stat;
                reps++;
                if (_status == Status.Optimal)
                    return MinimalSolution();

                StageOne();
                _etaEpsilon *= 10;
                if (reps > 5)
                    throw new InvalidOperationException();
            } while (_solver.Status == Status.FloatingPointError);

            if (_solver.Status != Status.Optimal || _status == Status.Infeasible)
            {
                _status = Status.Infeasible;
                return null;
            }
            _status = Status.Feasible;
            var ret = new double[_nVars];
            for (int i = 0; i < _nVars; i++)
                ret[i] = _solver.XStar[i];
            return ret;
        }

        public double[] MinimalSolution()
        {
            if (_costs == null)
                throw new InvalidOperationException(
#if DEBUG
"costs are not set"
#endif
);
            Minimize();
            if (Status == Status.Optimal)
            {
                var ret = new double[NVars];
                if (UseScaling)
                    for (int i = 0; i < NVars; i++)
                    {
                        Contract.Assume(_varScaleFactors[i] != 0);
                        ret[i] = _solver.XStar[i] / _varScaleFactors[i];
                    }
                else
                    for (int i = 0; i < NVars; i++)
                        ret[i] = _solver.XStar[i];

                return ret;
            }
            return null;
        }


        /// <summary>
        /// Call this method only in case when the program is infeasible.
        /// The linear program will be transformed to the form Ax=b. 
        /// The corresponding quadratic program is 
        /// minimize||Ax-b||, x>=0
        /// where ||.|| is the Euclidean norm.
        /// The solution always exists.
        /// Null is returned however if the matrix is too big
        /// </summary>
        /// <returns>approximation to solution</returns>
        public double[]
            LeastSquareSolution()
        {
            //calculate matrix A row length

            Contract.Assume(_constraints.Count > 0);

            int l = _constraints[0].coeffs.Length;
            int n = l;
            foreach (Constraint c in _constraints)
                if (c.relation != Relation.Equal)
                    n++;
            //this is code without any optimizations, well, almost any
            int m = _constraints.Count; //number of rows in A

            var A = new double[m * n];
            int offset = 0;
            int slackVarOffset = 0;

            for (int i = 0; i < m; i++)
            {
                double[] coeff = _constraints[i].coeffs;
                //copy coefficients
                for (int j = 0; j < l; j++)
                    A[offset + j] = coeff[j];

                Relation r = _constraints[i].relation;
                if (r == Relation.LessOrEqual)
                    A[offset + l + slackVarOffset++] = 1; //c[i]*x+1*ui=b[i]
                else if (r == Relation.GreaterOrEqual)
                    A[offset + l + slackVarOffset++] = -1; //c[i]*x-1*ui=b[i]
                offset += n;
            }


            var qp = new QP();
            //setting Q
            for (int i = 0; i < n; i++)
                for (int j = i; j < n; j++)
                {
                    double q = 0;
                    for (offset = 0; offset < A.Length; offset += n)
                        q += A[offset + i] * A[offset + j];
                    if (i == j)
                        qp.SetQMember(i, j, q);
                    else
                        qp.SetQMember(i, j, q / 2);
                }

            var linearCost = new double[n];
            for (int k = 0; k < n; k++)
            {
                double c = 0;
                offset = k;
                for (int j = 0; j < m; j++, offset += n)
                    c += _constraints[j].rightSide * A[offset];

                linearCost[k] = c;
            }
            qp.SetLinearCosts(linearCost);

            //we have no constraints in qp except the default: solution has to be not negative
            double[] sol = qp.Solution;
            if (qp.Status != Status.Optimal)
                throw new InvalidOperationException();

            var ret = new double[l];
            for (int i = 0; i < l; i++)
                ret[i] = sol[i];
            return ret;
        }


        public void InitCosts(double[] costsParam)
        {
            Contract.Assume(NVars == 0 || NVars == costsParam.Length);
            if (NVars == 0)
                NVars = costsParam.Length;
            if (_costs == null)
                _costs = new double[costsParam.Length];
            //flip the costs signs since we are maximizing inside of the solver

            Contract.Assume(_costs.Length >= NVars);

            for (int i = 0; i < NVars; i++)
            {
                Contract.Assert(i < _costs.Length);
                Contract.Assume(i < costsParam.Length);

                _costs[i] = -costsParam[i];
            }
            if (_solver != null)
            {
                if (_solver.Costs != null)
                {
                    for (int i = 0; i < costsParam.Length; i++)
                        _solver.Costs[i] = -costsParam[i];
                }
                if (_status == Status.Unbounded || _status == Status.Optimal)
                {
                    _status = Status.Feasible;
                    _solver.Status = _status;
                }
            }
        }

        public void SetVariable(int i, double val)
        {
            if (_knownValues == null)
                _knownValues = new Dictionary<int, double>();

            _knownValues[i] = val;
        }

        /// <summary>
        /// Adds a constraint to the linear program.
        /// </summary>
        /// <param name="coeff"></param>
        /// <param name="relation"></param>
        /// <param name="rightSide"></param>
        public void AddConstraint(double[] coeff, Relation relation, double rightSide)
        {
            Contract.Assume(NVars == 0 || NVars == coeff.Length);
            if (NVars == 0)
                NVars = coeff.Length;

            var c = new Constraint(coeff.Clone() as double[], relation, rightSide);

            Contract.Assume(c.coeffs != null);

            if (UseScaling)
                ScaleConstraint(c);
            _constraints.Add(c);
        }


        public void Minimize()
        {
            int reps = 0;
            Status stat = Status;
            do
            {
                Status = stat;
                if (Status == Status.Infeasible || Status == Status.Optimal)
                    return;
                if (Status != Status.Feasible)
                    StageOne();

                if (Status == Status.Feasible)
                    StageTwo();
                _etaEpsilon *= 10;
                reps++;
                if (reps > 5)
                    throw new InvalidOperationException();
            } while (Status == Status.FloatingPointError);
        }


        public Status Status
        {
            get { return _status; }
            set { _status = value; }
        }

        public void LimitVariableFromBelow(int var, double l)
        {
            Contract.Assume(_upperBoundIsSet[var] == false || _upperBounds[var] >= l);

            _lowBoundIsSet[var] = true;
            _lowBounds[var] = l;
        }

        public void LimitVariableFromAbove(int var, double l)
        {
            Contract.Assume(_lowBoundIsSet[var] == false || _lowBounds[var] <= l);

            _upperBoundIsSet[var] = true;
            _upperBounds[var] = l;
        }


        public double EpsilonForArtificials
        {
            get { return _epsilonForArtificials; }
            set { _epsilonForArtificials = value; }
        }

        public double EpsilonForReducedCosts
        {
            get { return 0; }
            set { }
        }

        #endregion

        private T[] DupArray<T>(T[] source)
        {
            if (source == null)
                return null;

            var result = new T[source.Length];
            Array.Copy(source, result, source.Length);

            return result;
        }


        private void StageOne()
        {
            #region  Preparing the first stage solver

            if (UseScaling)
                IntroduceVariableScaleFactors();


            var xStar = new double[NVars];
            FillAcceptableValues(xStar);
            CountSlacksAndArtificials(xStar);
            int artificialsStart = NVars + _nSlacks;
            int totalVars = NVars + _nSlacks + _nArtificials;

            Contract.Assume(totalVars > 0); // F: it seems to be some property of the implementation

            var nxstar = new double[totalVars];
            xStar.CopyTo(nxstar, 0);
            xStar = nxstar;

            var basis = new int[_constraints.Count];

            var solverLowBoundIsSet = new bool[totalVars];
            var solverLowBounds = new double[totalVars];
            var solverUpperBoundIsSet = new bool[totalVars];
            var solverUpperBounds = new double[totalVars];
            var solverCosts = new double[totalVars];

            _lowBoundIsSet.CopyTo(solverLowBoundIsSet, 0);
            _lowBounds.CopyTo(solverLowBounds, 0);
            _upperBoundIsSet.CopyTo(solverUpperBoundIsSet, 0);
            _upperBounds.CopyTo(solverUpperBounds, 0);

            Contract.Assume(_nSlacks + _nArtificials >= 0);  // F: It seems to be some property of the implementation
            var AOfSolver = new ExtendedConstraintMatrix(_constraints, _nSlacks + _nArtificials);

            FillFirstStageSolverAndCosts(solverLowBoundIsSet, solverLowBounds,
                                         solverUpperBoundIsSet, solverUpperBounds, solverCosts, AOfSolver, basis, xStar);


            _solver = new RevisedSolver(basis,
                                       xStar,
                                       artificialsStart,
                                       AOfSolver,
                                       solverCosts,
                                       solverLowBoundIsSet, solverLowBounds,
                                       solverUpperBoundIsSet, solverUpperBounds,
                                       ForbiddenPairs,
                                       _constraints,
                                       _etaEpsilon
                );

            #endregion

            ProcessKnownValues();

            _solver.Solve();
            if (_solver.Status == Status.FloatingPointError)
                return;
            HandleArtificialVariablesAndDecideOnStatus(artificialsStart, ref basis,
                                                       AOfSolver as ExtendedConstraintMatrix);
        }

        private void IntroduceVariableScaleFactors()
        {
            _varScaleFactors = new double[_nVars];
            for (int i = 0; i < _nVars; i++)
                IntroduceVariableScaleFactor(i);

            foreach (Constraint c in _constraints)
                for (int i = 0; i < _nVars; i++)
                {
                    Contract.Assume(_varScaleFactors[i] != 0);
                    c.coeffs[i] /= _varScaleFactors[i];
                }
        }

        private void IntroduceVariableScaleFactor(int i)
        {
            Contract.Requires(i >= 0);

            double max = 0;
            foreach (Constraint c in _constraints)
            {
                double d = Math.Abs(c.coeffs[i]);
                if (d > max)
                    max = d;
            }

            if (max > _maxForConstraint)
            {
                //we need to find s such that max/s<=maxInterval => s>=max/ maxForConstraint
                int s = 2;
                double d = max / _maxForConstraint;
                while (s < d)
                    s *= 2;
                _varScaleFactors[i] = s;
            }
            else if (max > _epsilon && max < _minForConstraint)
            {
                //max*s>=minForConstraint
                //s>=minForConstraint/max
                double d = _minForConstraint / max;
                int s = 2;
                while (s < d)
                    s *= 2;
                _varScaleFactors[i] = 1.0 / s;
            }
            else if (max == 0)
            {
                _varScaleFactors[i] = 1;
                SetVariable(i, GetAcceptableValue(i));
            }
            else
                _varScaleFactors[i] = 1;
        }


        private void ProcessKnownValues()
        {
            if (_knownValues != null)
                foreach (var kv in _knownValues)
                {
                    int var = kv.Key;
                    double val = kv.Value;
                    _solver.index[var] = RevisedSolver.forgottenVar;
                    _solver.XStar[var] = val;
                }
        }

        private void HandleArtificialVariablesAndDecideOnStatus(int artificialsStart, ref int[] basis,
                                                        ExtendedConstraintMatrix AOfSolver)
        {
            Contract.Requires(AOfSolver != null);

            //check that there are no artificial variables having positive values
            //see box 8.2 on page 130
            for (int k = 0; k < basis.Length; k++)
            {
                if (basis[k] >= artificialsStart)
                {
                    if (Math.Abs(_solver.XStar[basis[k]]) > EpsilonForArtificials)
                    {
                        _status = Status.Infeasible;
                        return;
                    }

                    var r = new double[basis.Length];
                    r[k] = 1;
                    _solver.Factorization.Solve_yBEquals_cB(r);

                    var cloned = r.Clone() as double[];
                    Contract.Assume(cloned != null);

                    var vr = new Vector(cloned);

                    foreach (int j in _solver.NBasis())
                    {
                        if (j < artificialsStart)
                        {
                            Contract.Assume(j >= 0);  // F: Need quantified invariants here
                            Contract.Assume(j < AOfSolver.NumberOfColumns);

                            var colVector = new ColumnVector(AOfSolver, j);

                            Contract.Assume(vr.Length == colVector.NumberOfRows);

                            if (Math.Abs(vr * colVector) > _epsilon)
                            {
                                AOfSolver.FillColumn(j, r);
                                _solver.Factorization.Solve_BdEqualsa(r);
                                _solver.Factorization.AddEtaMatrix(new EtaMatrix(k, r));
                                _solver.ReplaceInBasis(j, basis[k]);
                                break;
                            }
                        }
                    }
                }
            }
            //now remove the remaining artificial variables if any
            for (int k = 0; k < basis.Length; k++)
            {
                Contract.Assume(k < _constraints.Count); // F: basis's length is < of the # of constraints

                if (basis[k] >= artificialsStart)
                {
                    //cross out k-th constraint
                    //shrink basis
                    var nBas = new int[basis.Length - 1];
                    int j = 0;
                    for (int i = 0; i < basis.Length; i++)
                    {
                        if (i != k)
                        {
                            Contract.Assume(j < nBas.Length);
                            nBas[j++] = basis[i];
                        }
                    }
                    //taking care of the solver extended matrix
                    _constraints.RemoveAt(k);
                    int[] sa = AOfSolver.slacksAndArtificials;
                    for (int i = 0; i < sa.Length; i++)
                        if (sa[i] > k)
                            sa[i]--;
                        else if (sa[i] == k)
                            sa[i] = -1; //this will take put the column to zero

                    _solver.index[basis[k]] = RevisedSolver.forgottenVar;
                    _solver.basis = basis = nBas;
                    for (int i = k; i < basis.Length; i++)
                        _solver.index[basis[i]] = i;
                    k--;
                    _solver.Factorization = null; // to ensure the refactorization

                    _solver.A = new ExtendedConstraintMatrix(_constraints, sa);
                    _solver.U = new UMatrix(nBas.Length);
                }
            }
            _status = Status.Feasible;
        }


        private void FillFirstStageSolverAndCosts(
            bool[] solverLowBoundIsSet,
            double[] solverLowBounds,
            bool[] solverUpperBoundIsSet,
            double[] solverUpperBounds,
            double[] solverCosts,
            Matrix A,
            int[] basis,
            double[] xStar)
        {
            Contract.Requires(solverUpperBounds != null);
            Contract.Requires(solverLowBounds != null);
            Contract.Requires(solverLowBoundIsSet != null);
            Contract.Requires(solverUpperBoundIsSet != null);
            Contract.Requires(solverCosts != null);
            Contract.Requires(A != null);
            Contract.Requires(xStar != null);

            int slackVar = NVars;
            int artificialVar = NVars + _nSlacks;
            int row = 0;

            foreach (Constraint c in _constraints)
            {
                Contract.Assume(row < basis.Length);

                //we need to bring the program to the form Ax=b
                double rs = c.rightSide - LP.DotProduct(xStar, c.coeffs, _nVars);
                switch (c.relation)
                {
                    case Relation.Equal: //no slack variable here
                        if (rs >= 0)
                        {
                            SetZeroBound(solverLowBoundIsSet, solverLowBounds, artificialVar);
                            Contract.Assume(artificialVar < solverCosts.Length);
                            solverCosts[artificialVar] = -1;
                            //we are maximizing, so the artificial, which is non-negatiive, will be pushed to zero
                        }
                        else
                        {
                            SetZeroBound(solverUpperBoundIsSet, solverUpperBounds, artificialVar);
                            Contract.Assume(artificialVar < solverCosts.Length);
                            solverCosts[artificialVar] = 1;
                        }
                        basis[row] = artificialVar;
                        A[row, artificialVar++] = 1;
                        break;

                    case Relation.GreaterOrEqual:
                        //introduce a non-positive slack variable,
                        Contract.Assume(slackVar < solverUpperBoundIsSet.Length);
                        Contract.Assume(slackVar < solverUpperBounds.Length);
                        SetZeroBound(solverUpperBoundIsSet, solverUpperBounds, slackVar);
                        A[row, slackVar] = 1;
                        if (rs > 0)
                        {
                            //adding one artificial which is non-negative
                            SetZeroBound(solverLowBoundIsSet, solverLowBounds, artificialVar);
                            A[row, artificialVar] = 1;
                            solverCosts[artificialVar] = -1;
                            basis[row] = artificialVar++;
                        }
                        else
                        {
                            //we can put slackVar into basis, and avoid adding an artificial variable
                            //We will have an equality c.coefficients*acceptableCosts+x[slackVar]=c.rightSide, or x[slackVar]=rs<=0.
                            basis[row] = slackVar;
                        }
                        slackVar++;

                        break;

                    case Relation.LessOrEqual:
                        //introduce a non-negative slack variable,
                        Contract.Assume(slackVar < solverLowBoundIsSet.Length);
                        Contract.Assume(slackVar < solverLowBounds.Length);
                        SetZeroBound(solverLowBoundIsSet, solverLowBounds, slackVar);
                        A[row, slackVar] = 1;
                        if (rs < 0)
                        {
                            //adding one artificial which is non-positive
                            SetZeroBound(solverUpperBoundIsSet, solverUpperBounds, artificialVar);
                            A[row, artificialVar] = 1;
                            Contract.Assume(artificialVar < solverCosts.Length);
                            solverCosts[artificialVar] = 1;
                            basis[row] = artificialVar++;
                        }
                        else
                        {
                            //we can put slackVar into basis, and avoid adding an artificial variable
                            //We will have an equality c.coefficients*acceptableCosts+x[slackVar]=c.rightSide, or x[slackVar]=rs<=0.
                            basis[row] = slackVar;
                        }
                        slackVar++;
                        break;
                }

                xStar[basis[row]] = rs;

                row++;
            }
        }

        /// <summary>
        /// fill the original values with some values from their [lowBound, upperBound] intervals
        /// </summary>
        /// <returns></returns>
        private void FillAcceptableValues(double[] xStar)
        {
            Contract.Assume(xStar.Length >= _nVars);

            double val;
            for (int i = 0; i < _nVars; i++)
            {
                if (_knownValues == null || _knownValues.TryGetValue(i, out val) == false)
                {
                    xStar[i] = GetAcceptableValue(i);
                }
                else
                {
                    xStar[i] = val;
                }
            }
        }

        /// <summary>
        /// return a number from the variable domain
        /// </summary>
        /// <param name="x"></param>
        /// <param name="i"></param>
        [Pure]
        private double GetAcceptableValue(int i)
        {
            if (_lowBoundIsSet[i])
                return _lowBounds[i];
            if (_upperBoundIsSet[i])
                return _upperBounds[i];
            return 0;
        }

        private static void SetZeroBound(bool[] boundIsSet, double[] bounds, int var)
        {
            Contract.Assume(var < boundIsSet.Length);
            Contract.Assume(var < bounds.Length);

            boundIsSet[var] = true;
            bounds[var] = 0;
        }

        private void CountSlacksAndArtificials(double[] xStar)
        {
            foreach (Constraint constraint in _constraints)
                CountSlacksAndArtificialsForConstraint(constraint, xStar);
        }

        private void CountSlacksAndArtificialsForConstraint(Constraint constraint, double[] xStar)
        {
            double rightSide = constraint.rightSide - LP.DotProduct(constraint.coeffs, xStar);
            switch (constraint.relation)
            {
                case Relation.Equal:
                    _nArtificials++;
                    break;
                case Relation.GreaterOrEqual:
                    _nSlacks++;
                    if (rightSide > 0)
                        _nArtificials++;
                    break;
                case Relation.LessOrEqual:
                    _nSlacks++;
                    if (rightSide < 0)
                        _nArtificials++;
                    break;
            }
        }


        //static int calls;

        private void StageTwo()
        {
            PrepareStageTwo();
            _solver.Solve();
            _status = _solver.Status;
        }

        private void PrepareStageTwo()
        {
            int i;
            for (i = NVars + _nSlacks; i < NVars + _nSlacks + _nArtificials; i++)
                _solver.ForgetVariable(i);

            var solverMatrix = _solver.A;
            Contract.Assume(solverMatrix != null);
            solverMatrix.NumberOfColumns = NVars + _nSlacks;
            if (_solver.y.Length != _constraints.Count)
                _solver.y = new double[_constraints.Count];
            //this should be refactored somehow, y is the variable for keeping solutions of the linear systems
            for (i = 0; i < _costs.Length; i++)
                _solver.Costs[i] = _costs[i];

            for (; i < _solver.Costs.Length; i++)
                _solver.Costs[i] = 0;

            _solver.Status = Status.Feasible;
        }


        private void ScaleConstraint(Constraint c)
        {
            Contract.Requires(c != null);

            double max = 0;
            for (int i = 0; i < c.coeffs.Length; i++)
            {
                double d = Math.Abs(c.coeffs[i]);
                if (d > max)
                    max = d;
            }

            if (max > _maxForConstraint)
            {
                //find first power of two k, such that max/k<=maxForConstraint,
                //that is  k >= max / maxForConstraint;
                int k = 2;
                double d = max / _maxForConstraint;
                while (k < d)
                    k *= 2;
                for (int i = 0; i < c.coeffs.Length; i++)
                    c.coeffs[i] /= k;
                c.rightSide /= k;
            }
            else if (max < _minForConstraint && max > _epsilon)
            {
                //find first power of two, k , such that max*k>=minForConstraint,
                // or k>=minForConstrant/max
                double d = _minForConstraint / max;
                int k = 2;
                while (k < d)
                    k *= 2;

                for (int i = 0; i < c.coeffs.Length; i++)
                    c.coeffs[i] *= k;
                c.rightSide *= k;
            }
        }
    }
}