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
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Microsoft.Research.CodeAnalysis.Caching
{
  public static class CacheUtils
  {
    public const int MaxMethodLength = 4000;

    public static bool ContentEquals(this byte[] hash1, byte[] hash2)
    {
      Contract.Requires(hash1 != null);
      Contract.Requires(hash2 != null);

      if (hash1.Length != hash2.Length) return false;
      for (int i = 0; i < hash1.Length; i++)
      {
        if (hash1[i] != hash2[i]) return false;
      }
      return true;
    }

    public static byte[] MD5Encode(this string segment)
    {
      Contract.Requires(segment != null);
      Contract.Ensures(Contract.Result<byte[]>() != null);

      using (MD5 md5Hash = MD5.Create())
      {
        // Convert the input string to a byte array and compute the hash. 
        byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(segment));
        return data;
      }
    }

  }
}
