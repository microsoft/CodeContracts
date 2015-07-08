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