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

// <copyright file="ChunkerTest.cs" company="Microsoft">Copyright © Microsoft 2009</copyright>
using System;
using ChunkerTest;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChunkerTest
{
    /// <summary>This class contains parameterized unit tests for Chunker</summary>
    [TestClass]
    [PexClass(typeof(Chunker))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class ChunkerTest
    {
        /// <summary>Test stub for ChunkSize</summary>
        [PexMethod]
        public void ChunkSizeGet([PexAssumeUnderTest]Chunker target)
        {
            // TODO: add assertions to method ChunkerTest.ChunkSizeGet(Chunker)
            int result = target.ChunkSize;
        }

        /// <summary>Test stub for .ctor(String, Int32)</summary>
        [PexMethod]
        public Chunker Constructor(string source, int chunkSize)
        {
            // TODO: add assertions to method ChunkerTest.Constructor(String, Int32)
            Chunker target = new Chunker(source, chunkSize);
            return target;
        }

        /// <summary>Test stub for NextChunk()</summary>
        [PexMethod]
        public string NextChunk([PexAssumeUnderTest]Chunker target)
        {
            // TODO: add assertions to method ChunkerTest.NextChunk(Chunker)
            string result = target.NextChunk();
            return result;
        }
    }
}
