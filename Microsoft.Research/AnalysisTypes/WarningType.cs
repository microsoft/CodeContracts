// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Microsoft.Research.CodeAnalysis
{
    // The warnings that can be emitted by Clousot
    public enum WarningType : byte
    {
        // no explicit proof obligation
        UnreachedCodeAfterPrecondition,         // The program point after the precondition is unreached
        TestAlwaysEvaluatingToAConstant,
        FalseRequires,
        FalseEnsures,

        // Arithmetics
        ArithmeticDivisionByZero,
        ArithmeticDivisionOverflow,
        ArithmeticMinValueNegation,
        ArithmeticOverflow,
        ArithmeticUnderflow,
        ArithmeticFloatEqualityPrecisionMismatch,

        // Arrays
        ArrayCreation,
        ArrayLowerBound,
        ArrayUpperBound,

        // Array Purity
        ArrayPurity,

        // Contracts
        ContractAssert,
        ContractAssume, // this is used by the -checkassumptions option
        ContractEnsures,
        ContractInvariant,
        ContractRequires,

        // Missing validations
        MissingPrecondition,
        MissingPreconditionInvolvingReadonly,  // A missing precondition that involves a readonly field

        // A suggestion turned into a warning
        Suggestion,

        // Enum
        EnumRange,

        // Nonnull
        NonnullArray,
        NonnullCall,
        NonnullField,
        NonnullUnbox,

        // Unsafe memory accesses
        UnsafeCreation,
        UnsafeLowerBound,
        UnsafeUpperBound,

        // Can't connect to the cache
        ClousotCacheNotAvailable,
    }
}