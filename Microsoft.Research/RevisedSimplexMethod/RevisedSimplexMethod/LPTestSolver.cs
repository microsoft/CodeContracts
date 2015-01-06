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

namespace Microsoft.Glee.Optimization {
#if DEBUGGLEE
    public class LPTestSolver: LinearProgramInterface {
        LP lp;
        RevisedSimplexMethod rsm;
        static int calls;
        public LPTestSolver(bool lookForZeroColumns) {
            calls++;
            Console.WriteLine(calls);
            lp = new LP(lookForZeroColumns);
            rsm = new RevisedSimplexMethod();           
        }
        #region LinearProgramInterface Members
       
        public int[] ForbiddenPairs {
            get {
               return lp.ForbiddenPairs;
            }
            set {
                lp.ForbiddenPairs = rsm.ForbiddenPairs = value;
            }
        }

        public double Epsilon {
            get {
                return lp.Epsilon;
            }
            set {
                lp.Epsilon = rsm.Epsilon = value;
            }
        }

        public double[] FeasibleSolution {
            get {
                double[] ret = lp.FeasibleSolution;
                double[] ttt = rsm.FeasibleSolution;
                CheckStatuses();
                return ret;
            }
        }

        private void CheckStatuses() {
            if (lp.Status != rsm.Status) {
                Console.WriteLine("different statuses lp {0} rsm {1}", lp.Status, rsm.Status);
            }
        }

        public double[] MinimalSolution {
            get {
                double[] ret = lp.MinimalSolution;
                double[] ttt = rsm.MinimalSolution;
                CheckStatuses();
                return ttt;
            }
        }

        public double[] LeastSquareSolution() {
            double[] ret = lp.LeastSquareSolution();
            double[] ttt = rsm.LeastSquareSolution();
            CheckStatuses();
            return ret;
        }

        public void InitCosts(double[] costsParam) {
            lp.InitCosts(costsParam);
            rsm.InitCosts(costsParam);
        }

        public void SetVariable(int i, double val) {
            lp.SetVariable(i, val);
            rsm.SetVariable(i, val);
        }

        public void AddConstraint(double[] coeff, Relation relation, double rightSide) {
            lp.AddConstraint(coeff, relation, rightSide);
            rsm.AddConstraint(coeff, relation, rightSide);
        }

        public void Minimize() {
            lp.Minimize();
            rsm.Minimize();
            CheckStatuses();
        }

        public double MinimalValue {
            get {
                double ret = lp.MinimalValue;
                double f = rsm.MinimalValue;
                CheckStatuses();
                return ret;
            }
        }

        public Status Status {
            get {
                CheckStatuses();
                return lp.Status;
            }
            set {
                lp.Status = rsm.Status = value;
            }
        }

        public void LimitVariableFromBelow(int var, double l) {
            lp.LimitVariableFromBelow(var, l);
            rsm.LimitVariableFromBelow(var, l);
            CheckStatuses();
        }

        public void LimitVariableFromAbove(int var, double l) {
            lp.LimitVariableFromAbove(var, l);
            rsm.LimitVariableFromAbove(var, l);
            CheckStatuses();
        }

        public double EpsilonForArtificials {
            get {
                return lp.EpsilonForArtificials;
            }
            set {
                lp.EpsilonForArtificials = rsm.EpsilonForArtificials = value;
            }
        }

        public double EpsilonForReducedCosts {
            get {
                return lp.EpsilonForReducedCosts;
            }
            set {
                lp.EpsilonForReducedCosts = rsm.EpsilonForReducedCosts = value;
            }
        }

        #endregion
    }
#endif
}
