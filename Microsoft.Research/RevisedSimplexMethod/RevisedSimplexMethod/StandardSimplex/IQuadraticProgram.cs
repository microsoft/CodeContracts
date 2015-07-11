// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Glee.Optimization
{
    /// <summary>
    /// Interface of a quadratic program solver.
    /// Solves: maximize c*x-0.5xt*Q*x; where xt is transpose of x
    /// and Q is a symmetric matrix, Q is also positive semidefinite: that is xt*Qx*x>=0 for any x>=0.
    /// Unknown x is not-negative and is a subject to linear constraints added by AddConstraint method.
    /// Vector costs,c, is set by method SetLinearCosts.
    /// </summary>
    public interface IQuadraticProgram
    {
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

        Status Status { get; }
    }
}
