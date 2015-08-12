// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Microsoft.Research.DataStructures;

namespace Microsoft.Research.CodeAnalysis
{
    [Flags]
    public enum MethodHashAttributeFlags
    {
        Default = 0,
        OnlyFromCache = 1, // means that the result of the analysis can only be read from cache but shouldn't be computed by clousot (e.g. method with empty body)
        Reuse = 2, // means that we should try to get the latest available result for the id of this method (ignoring its hash)
        IdIsHash = 4, // means that the method id should also be considered is hash (e.g. method with empty body)
        IgnoreRegression = 8, // means that we should ignore the regression attribute for this method
        ReplayOnlyInferredContracts = 16, // means that we read from the cache, outcomes, suggestions, stats, etc. should not be replayed (e.g. APCs coming from other assemblies)
        FirstInOrder = 32, // means that the methodOrder will put these methods first

        ForDependenceMethod = OnlyFromCache | Reuse | IdIsHash | IgnoreRegression | ReplayOnlyInferredContracts | FirstInOrder,
    }

    public class MethodHashAttribute : Attribute
    {
        private readonly ByteArray methodId;
        private readonly MethodHashAttributeFlags flags;

        public MethodHashAttribute(ByteArray methodId, MethodHashAttributeFlags flags)
        {
            this.methodId = methodId;
            this.flags = flags;
        }

        public static MethodHashAttribute FromDecoder(IIndexable<object> args)
        {
            if (args == null || args.Count != 2)
                return null;

            var hashObj = args[0]; // it can be null, when generated at the Slicer level
            var flagsObj = args[1];

            if ((hashObj == null || hashObj is string) && (flagsObj is int))
            {
                var flags = (MethodHashAttributeFlags)(int)flagsObj;
                var methodHash = new ByteArray(hashObj as string);

                return new MethodHashAttribute(methodHash, flags);
            }
            else
            {
                return null;
            }
        }

        public ByteArray MethodId { get { return methodId; } }
        public MethodHashAttributeFlags Flags { get { return flags; } }

        // For the code writer
        public string Arg1 { get { return methodId == null ? "" : methodId.ToString(); } }
        public int Arg2 { get { return (int)flags; } }
    }
}
