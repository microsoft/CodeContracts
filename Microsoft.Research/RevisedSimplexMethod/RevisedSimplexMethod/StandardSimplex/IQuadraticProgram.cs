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
    /// <summary>
    /// Interface of a quadratic program solver.
    /// Solves: maximize c*x-0.5xt*Q*x; where xt is transpose of x
    /// and Q is a symmetric matrix, Q is also positive semidefinite: that is xt*Qx*x>=0 for any x>=0.
    /// Unknown x is not-negative and is a subject to linear constraints added by AddConstraint method.
    /// Vector costs,c, is set by method SetLinearCosts.
    /// </summary>
    public interface IQuadraticProgram {
        void AddConstraint(double[] coeffs, Relation relation, double rightSide);

        /// <summary>
        /// sets i,j element of Q
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="qij"></param>
        void SetQMember(int i, int j, double qij);

        /// <summary>
        /// adds d to i,j element of Q
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="qij"></param>
        void AddToQMember(int i, int j, double d);
        void SetLinearCosts(double[] costs);

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
        double[] Solution { get; }

        Status Status { get;  }
    }
}
