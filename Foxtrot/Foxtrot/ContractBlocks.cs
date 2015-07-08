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