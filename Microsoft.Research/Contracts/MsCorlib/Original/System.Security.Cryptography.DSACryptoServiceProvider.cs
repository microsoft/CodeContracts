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

    public class DSACryptoServiceProvider
    {

        public static bool UseMachineKeyStore
        {
          get;
          set;
        }

        public bool PersistKeyInCsp
        {
          get;
          set;
        }

        public string SignatureAlgorithm
        {
          get;
        }

        public KeySizes[] LegalKeySizes
        {
          get;
        }

        public string KeyExchangeAlgorithm
        {
          get;
        }

        public int KeySize
        {
          get;
        }

        public bool VerifyHash (Byte[]! rgbHash, string str, Byte[]! rgbSignature) {
            CodeContract.Requires(rgbHash != null);
            CodeContract.Requires(rgbSignature != null);

          return default(bool);
        }
        public Byte[] SignHash (Byte[]! rgbHash, string str) {
            CodeContract.Requires(rgbHash != null);

          return default(Byte[]);
        }
        public bool VerifySignature (Byte[] rgbHash, Byte[] rgbSignature) {

          return default(bool);
        }
        public Byte[] CreateSignature (Byte[] rgbHash) {

          return default(Byte[]);
        }
        public bool VerifyData (Byte[] rgbData, Byte[] rgbSignature) {

          return default(bool);
        }
        public Byte[] SignData (Byte[] buffer, int offset, int count) {

          return default(Byte[]);
        }
        public Byte[] SignData (Byte[] buffer) {

          return default(Byte[]);
        }
        public Byte[] SignData (System.IO.Stream inputStream) {

          return default(Byte[]);
        }
        public void ImportParameters (DSAParameters parameters) {

        }
        public DSAParameters ExportParameters (bool includePrivateParameters) {

          return default(DSAParameters);
        }
        public DSACryptoServiceProvider (int dwKeySize, CspParameters parameters) {

          return default(DSACryptoServiceProvider);
        }
        public DSACryptoServiceProvider (CspParameters parameters) {

          return default(DSACryptoServiceProvider);
        }
        public DSACryptoServiceProvider (int dwKeySize) {

          return default(DSACryptoServiceProvider);
        }
        public DSACryptoServiceProvider () {
          return default(DSACryptoServiceProvider);
        }
    }
}
