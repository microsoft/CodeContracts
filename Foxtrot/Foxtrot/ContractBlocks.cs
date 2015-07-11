// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Compiler;

namespace Microsoft.Contracts.Foxtrot
{
    /// <summary>
    /// A wrapper just so preamble code can be recognized at the beginning of a method.
    /// Preamble code is
    /// a) closure class creation/initialization
    /// b) base ctor calls (in instance initializers)
    /// c) field initialization (in instance initializers)
    /// </summary>
    public sealed class PreambleBlock : Block
    {
        public PreambleBlock(StatementList statements) : base(statements)
        {
        }
    }

    /// <summary>
    /// Used in MoveNext methods in place of the original contracts
    /// </summary>
    public sealed class AssumeBlock : Block
    {
        public AssumeBlock(StatementList statements)
            : base(statements)
        {
        }
    }

    /// <summary>
    /// Contains initialization code that needs to go before contracts and thus cannot go into the preamble block
    /// For now it contains the following:
    /// a) anonymous cached method delegate initialization to null.
    /// </summary>
    public sealed class InitBlock : Block
    {
        public InitBlock(StatementList statements)
            : base(statements)
        {
        }
    }
}