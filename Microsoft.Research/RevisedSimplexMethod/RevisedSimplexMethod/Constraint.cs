// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Glee.Optimization
{
    [Serializable]
    internal class Constraint
    {
        internal Constraint() { }

        internal Constraint(double[] coeffs, Relation relation, double rightSide)
        {
            this.coeffs = coeffs;
            this.relation = relation;
            this.rightSide = rightSide;
        }

        readonly internal double[] coeffs;
        readonly internal Relation relation;
        internal double rightSide;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Double.ToString"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1818:DoNotConcatenateStringsInsideLoops")]
        public override string ToString()
        {
            string r = "";
            for (int i = 0; i < coeffs.Length; i++)
            {
                r += coeffs[i];
                if (i < coeffs.Length - 1)
                    r += ",";
                else r += " ";
            }

            r += relation.ToString();
            r += " " + rightSide.ToString();
            return r;
        }
    }
}
