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
using System.IO;
using System.Diagnostics.Contracts;

namespace Microsoft.Glee.Optimization {

    public enum Relation {
        LessOrEqual,
        Equal,
        GreaterOrEqual
    }

    public enum Status {
        Unknown,
        Infeasible,
        Unbounded,
        Optimal,
        Feasible, //this status is assigned after successfull stage one
        FloatingPointError
    }

    /// <summary>
    /// Solves the general linear program but always looking for the minimum
    /// </summary>
    
  [ContractClass(typeof(LinearProgramInterfaceContracts))]
    public interface LinearProgramInterface {
        /// <summary>
        /// When looking for the Quadratical Program minimum both variables of some variable pairs cannot appear in the basis together
        /// </summary>
        int[] ForbiddenPairs { set;            get;        }

        double Epsilon {
          get; /*set;*/
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1819:PropertiesShouldNotReturnArrays")]
        double[] FeasibleSolution();

        /// <summary>
        /// returns an optimal solution: that is a solution where cx is minimal
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1819:PropertiesShouldNotReturnArrays")]
        double[] MinimalSolution();

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
        double[] LeastSquareSolution();

        ///<summary>
        /// set the cost function
        /// </summary>
        /// <param name="costs">the objective vector</param>
        void InitCosts(double[] costsParam);

        /// <summary>
        /// If one just happens to know the i-th variable value it can be set
        /// </summary>
        /// <param name="i"></param>
        /// <param name="val"></param>
        void SetVariable(int i, double val);

        /// <summary>
        /// adds a constraint: the coeffiecents should not be very close to zero or too huge.
        /// If it is the case, as one can scale for example the whole programm to zero,
        /// then such coefficient will be treated az zeros. We are talking here about the numbers
        /// with absolute values less than 1.0E-8
        /// </summary>
        /// <param name="coeff">the constraint coefficents</param>
        /// <param name="relation">could be 'less or equal', equal or 'greater or equal'</param>
        /// <param name="rightSide">right side of the constraint</param>
        void AddConstraint(double[] coeff, Relation relation, double rightSide);

        /// <summary>
        /// Solves the linear program, minimizing, by going through stages one and two
        /// </summary>
        void Minimize();

        /// <summary>
        /// calculate the cost of the solution
        /// </summary>
        double GetMinimalValue();

        Status Status {
            get;
            set;
        }

        /// <summary>
        /// Add a constraint from below on the variable 
        /// </summary>
        /// <param name="var"></param>
        /// <param name="l"></param>
        void LimitVariableFromBelow(int var, double l);

        /// <summary>
        /// Add a constraint from above on the variable 
        /// </summary>
        /// <param name="var"></param>
        /// <param name="l"></param>
        void LimitVariableFromAbove(int var, double l);
         
        double EpsilonForArtificials{get;set;}
        double EpsilonForReducedCosts { get;set;}
    }


    [ContractClassFor(typeof(LinearProgramInterface))]
    abstract class LinearProgramInterfaceContracts :
      LinearProgramInterface
    {
      #region LinearProgramInterface Members

      int[] LinearProgramInterface.ForbiddenPairs
      {
        get
        {
          throw new NotImplementedException();
        }
        set
        {
          throw new NotImplementedException();
        }
      }

      double LinearProgramInterface.Epsilon
      {
        get
        {
          throw new NotImplementedException();
        }
      }

      double[] LinearProgramInterface.FeasibleSolution()
      {
        throw new NotImplementedException();
      }

      double[] LinearProgramInterface.MinimalSolution()
      {
        throw new NotImplementedException();
      }

      double[] LinearProgramInterface.LeastSquareSolution()
      {
        Contract.Requires(((LinearProgramInterface)this).Status == Status.Infeasible);

        return default(double[]);
      }

      void LinearProgramInterface.InitCosts(double[] costsParam)
      {
        throw new NotImplementedException();
      }

      void LinearProgramInterface.SetVariable(int i, double val)
      {
        throw new NotImplementedException();
      }

      void LinearProgramInterface.AddConstraint(double[] coeff, Relation relation, double rightSide)
      {
        throw new NotImplementedException();
      }

      void LinearProgramInterface.Minimize()
      {
        throw new NotImplementedException();
      }

      double LinearProgramInterface.GetMinimalValue()
      {
        throw new NotImplementedException();
      }

      Status LinearProgramInterface.Status
      {
        get
        {
          throw new NotImplementedException();
        }
        set
        {
          throw new NotImplementedException();
        }
      }

      void LinearProgramInterface.LimitVariableFromBelow(int var, double l)
      {
        throw new NotImplementedException();
      }

      void LinearProgramInterface.LimitVariableFromAbove(int var, double l)
      {
        throw new NotImplementedException();
      }

      double LinearProgramInterface.EpsilonForArtificials
      {
        get
        {
          throw new NotImplementedException();
        }
        set
        {
          throw new NotImplementedException();
        }
      }

      double LinearProgramInterface.EpsilonForReducedCosts
      {
        get
        {
          throw new NotImplementedException();
        }
        set
        {
          throw new NotImplementedException();
        }
      }

      #endregion
    }
}
