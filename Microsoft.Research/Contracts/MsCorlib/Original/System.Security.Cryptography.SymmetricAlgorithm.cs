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

    public class SymmetricAlgorithm
    {

        public int FeedbackSize
        {
          get;
          set;
        }

        public KeySizes[] LegalBlockSizes
        {
          get;
        }

        public int BlockSize
        {
          get;
          set;
        }

        public CipherMode Mode
        {
          get;
          set
            CodeContract.Requires((int)value >= 1);
            CodeContract.Requires(4 >= (int)value);
        }

        public int KeySize
        {
          get;
          set;
        }

        public Byte[]! IV
        {
          get;
          set
            CodeContract.Requires(value != null);
        }

        public Byte[]! Key
        {
          get;
          set
            CodeContract.Requires(value != null);
        }

        public KeySizes[] LegalKeySizes
        {
          get;
        }

        public PaddingMode Padding
        {
          get;
          set
            CodeContract.Requires((int)value >= 1);
            CodeContract.Requires(3 >= (int)value);
        }

        public void GenerateIV () {

        }
        public void GenerateKey () {

        }
        public ICryptoTransform CreateDecryptor (Byte[] arg0, Byte[] arg1) {

          return default(ICryptoTransform);
        }
        public ICryptoTransform CreateDecryptor () {

          return default(ICryptoTransform);
        }
        public ICryptoTransform CreateEncryptor (Byte[] arg0, Byte[] arg1) {

          return default(ICryptoTransform);
        }
        public ICryptoTransform CreateEncryptor () {

          return default(ICryptoTransform);
        }
        public static SymmetricAlgorithm Create (string algName) {

          return default(SymmetricAlgorithm);
        }
        public static SymmetricAlgorithm Create () {

          return default(SymmetricAlgorithm);
        }
        public bool ValidKeySize (int bitLength) {

          return default(bool);
        }
        public void Clear () {

        }
        public SymmetricAlgorithm () {
          return default(SymmetricAlgorithm);
        }
    }
}
