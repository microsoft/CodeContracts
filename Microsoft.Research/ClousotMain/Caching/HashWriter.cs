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

//#define DEBUG_HASHER

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Microsoft.Research.CodeAnalysis
{
  public class HashWriter : StreamWriter
  {
    // Protocol:
    // 1. Encode the string with the StreamWriter this
    // 2. Send the output to the CryptoStream
    // 3. The cryptoStream is simply a proxy for the hashAlgorithm
    private readonly CryptoStream cryptoStream;
    private readonly HashAlgorithm hashAlgorithm;

    private delegate Encoding DefaultEncoding();
    private delegate HashAlgorithm DefaultHashAlgorithm();
    private delegate Stream DefaultStream();

    private static readonly DefaultHashAlgorithm defaultHashAlgorithm = SHA1.Create; // MD5 too costly

    private static readonly DefaultEncoding traceEncoding = () => new UTF8Encoding();
    private static readonly DefaultStream traceStream = Console.OpenStandardOutput;

    // We use Unicode encoding because sometimes there are invalid unicode strings that cannot be encoded as utf8...
    private static readonly DefaultEncoding defaultEncoding = () => new UnicodeEncoding();
    private static readonly DefaultStream defaultStream = () => Stream.Null;

    public HashWriter(bool trace)
      : this(defaultHashAlgorithm(), trace? traceEncoding(): defaultEncoding(), trace)
    { }

    public HashWriter(HashAlgorithm hashAlgorithm, Encoding encoding, bool trace)
      : this(hashAlgorithm, encoding, new CryptoStream(trace? traceStream() : defaultStream(), hashAlgorithm, CryptoStreamMode.Write))
    { }

    private HashWriter(HashAlgorithm hashAlgorithm, Encoding encoding, CryptoStream cryptoStream)
      : base(cryptoStream, encoding)
    {
      this.cryptoStream = cryptoStream;
      this.hashAlgorithm = hashAlgorithm;
    }

    public byte[] GetHash()
    {
      Contract.Ensures(Contract.Result<byte[]>() != null);

      // We are done! Let's do some flushing
      this.Flush();
      this.cryptoStream.FlushFinalBlock(); // At the end we have to flush and say that we are done!
      return hashAlgorithm.Hash;
    }

    protected override void Dispose(bool disposing)
    {
      base.Dispose(disposing);
      this.cryptoStream.Dispose();
      var disposableHashAlgorithm = this.hashAlgorithm as IDisposable;
      if (disposableHashAlgorithm != null)
      {
        disposableHashAlgorithm.Dispose();
      }
    }
  }
}
