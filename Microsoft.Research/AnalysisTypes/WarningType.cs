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