// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.IO;

using Microsoft.Research.DataStructures;

namespace Microsoft.Research.CodeAnalysis
{
#if SUBPOLY_ONLY
    public enum Precision { Light, Normal, Strong }
    public enum ProofOutcome { Top, False, True, Bottom }
#endif

    public enum ReductionAlgorithm { Fast, Complete, Simplex, SimplexFast, SimplexOptima, None }

    public interface IAIOptions
    {
        ReductionAlgorithm Algorithm { get; }

        bool Use2DConvexHull { get; }
        bool InferOctagonConstraints { get; }

        bool UseMorePreciseWidening { get; }
        bool UseTracePartitioning { get; }
        bool TrackDisequalities { get; }

        bool TracePartitionAnalysis { get; }

        bool TraceNumericalAnalysis { get; }
    }
}
