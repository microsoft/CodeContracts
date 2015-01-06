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

using System.Diagnostics.Contracts;
using System;

namespace System.Security.Cryptography
{

    public class CryptoAPITransform
    {

        public int InputBlockSize
        {
          get;
        }

        public int KeyHandle
        {
          get;
        }

        public bool CanTransformMultipleBlocks
        {
          get;
        }

        public int OutputBlockSize
        {
          get;
        }

        public bool CanReuseTransform
        {
          get;
        }

        public Byte[] TransformFinalBlock (Byte[]! inputBuffer, int inputOffset, int inputCount) {
            CodeContract.Requires(inputBuffer != null);
            CodeContract.Requires(inputOffset >= 0);
            CodeContract.Requires(inputCount >= 0);
            CodeContract.Requires(inputCount <= inputBuffer.Length);
            CodeContract.Requires((inputBuffer.Length - inputCount) >= inputOffset);

          return default(Byte[]);
        }
        public int TransformBlock (Byte[]! inputBuffer, int inputOffset, int inputCount, Byte[]! outputBuffer, int outputOffset) {
            CodeContract.Requires(inputBuffer != null);
            CodeContract.Requires(outputBuffer != null);
            CodeContract.Requires(inputOffset >= 0);
            CodeContract.Requires(inputCount > 0);
            CodeContract.Requires(inputCount <= inputBuffer.Length);
            CodeContract.Requires((inputBuffer.Length - inputCount) >= inputOffset);

          return default(int);
        }
        public void Clear () {
        }
    }
}
