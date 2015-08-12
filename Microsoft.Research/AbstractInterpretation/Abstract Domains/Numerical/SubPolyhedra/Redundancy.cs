// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Microsoft.Research.AbstractDomains.Numerical
{
    public struct LinearEqualityRedundancy<Variable>
    {
        readonly public Variable V1;
        readonly public Variable V2;
        readonly public Rational Coeff2;
        readonly public Rational Offset;

        readonly public int Row;

        public LinearEqualityRedundancy(Variable v1, Rational coeff2, Variable v2, Rational offset)
          : this(v1, coeff2, v2, offset, -23456)
        {
        }

        public LinearEqualityRedundancy(Variable v1, Rational coeff2, Variable v2, Rational offset, int row)
        {
            this.V1 = v1;
            this.Coeff2 = coeff2;
            this.V2 = v2;
            this.Offset = offset;
            this.Row = row;
        }

        public override string ToString()
        {
            return string.Format("<{0}, {1}, {2}, {3}, row: {4}>", V1, Coeff2, V2, Offset, Row);
        }
    }
}