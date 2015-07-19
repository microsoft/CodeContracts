// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

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
