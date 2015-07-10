// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Research.CodeAnalysis
{
    public enum ExpressionCacheMode { None, Mem, Time }


    public enum ProofOutcome : byte { Top = 0, Bottom, True, False }

    public static class ProofOutcomeExtensions
    {
        public static ProofOutcome Meet(this ProofOutcome o1, ProofOutcome o2)
        {
            if (o1 == o2) return o1;
            if (o1 == ProofOutcome.Top || o2 == ProofOutcome.Bottom) return o2;
            if (o2 == ProofOutcome.Top || o1 == ProofOutcome.Bottom) return o1;

            // different and none is top or bottom, so meet of true and false
            return ProofOutcome.Bottom;
        }

        public static ProofOutcome Join(this ProofOutcome o1, ProofOutcome o2)
        {
            if (o1 == o2) return o1;
            if (o1 == ProofOutcome.Top || o2 == ProofOutcome.Bottom) return o1;
            if (o2 == ProofOutcome.Top || o1 == ProofOutcome.Bottom) return o2;

            // different and none is top or bottom, so join of true and false
            return ProofOutcome.Top;
        }

        public static bool IsNormal(this ProofOutcome o)
        {
            return o == ProofOutcome.True || o == ProofOutcome.False;
        }

        public static ProofOutcome Negate(this ProofOutcome o)
        {
            switch (o)
            {
                case ProofOutcome.Bottom:
                case ProofOutcome.Top:
                    return o;

                case ProofOutcome.False:
                    return ProofOutcome.True;

                case ProofOutcome.True:
                    return ProofOutcome.False;

                default:
                    return ProofOutcome.Top;
            }
        }
    }
}
