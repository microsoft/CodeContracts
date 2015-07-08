using System;

namespace Microsoft.Contracts.Foxtrot
{
    [Flags]
    internal enum RuntimeContractEmitFlags
    {
        None = 0,
        LegacyRequires = 0x0001,
        RequiresWithException = 0x0002,
        Requires = 0x0004,
        Ensures = 0x0008,
        Invariants = 0x0010,
        Asserts = 0x0020,
        Assumes = 0x0040,
        AsyncEnsures = 0x0080,
        ThrowOnFailure = 0x1000,
        StandardMode = 0x2000,
        InheritContracts = 0x4000,

        /// <summary>
        /// Takes precedence over individual bits (like a mask)
        /// </summary>
        NoChecking = 0x8000,
    }

    internal static class FlagExtensions
    {
        public static bool Emit(this RuntimeContractEmitFlags have, RuntimeContractEmitFlags want)
        {
            return ((have & RuntimeContractEmitFlags.NoChecking) == 0) && (have & want) != 0;
        }
    }
}