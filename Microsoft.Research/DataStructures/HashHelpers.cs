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

namespace Microsoft.Research.DataStructures {

    public static class HashHelpers
    {
        // Table of prime numbers to use as hash table sizes. 
        // The entry used for capacity is the smallest prime number in this aaray
        // that is larger than twice the previous capacity. 

        internal static readonly int[] primes = {
            3, 7, 11, 17, 23, 29, 37, 47, 59, 71, 89, 107, 131, 163, 197, 239, 293, 353, 431, 521, 631, 761, 919,
            1103, 1327, 1597, 1931, 2333, 2801, 3371, 4049, 4861, 5839, 7013, 8419, 10103, 12143, 14591,
            17519, 21023, 25229, 30293, 36353, 43627, 52361, 62851, 75431, 90523, 108631, 130363, 156437,
            187751, 225307, 270371, 324449, 389357, 467237, 560689, 672827, 807403, 968897, 1162687, 1395263,
            1674319, 2009191, 2411033, 2893249, 3471899, 4166287, 4999559, 5999471, 7199369};

        internal static bool IsPrime(int candidate)
        {
            if ((candidate & 1) != 0)
            {
                int limit = (int)Math.Sqrt(candidate);
                for (int divisor = 3; divisor <= limit; divisor += 2)
                {
                    if ((candidate % divisor) == 0)
                        return false;
                }
                return true;
            }
            return (candidate == 2);
        }

        internal static int GetPrime(int min)
        {
            Contract.Requires((min >= 0), "(min >= 0)");

            for (int i = 0; i < primes.Length; i++)
            {
                int prime = primes[i];
                if (prime >= min) return prime;
            }

            //outside of our predefined table. 
            //compute the hard way. 
            for (int i = (min | 1); i < Int32.MaxValue; i += 2)
            {
                if (IsPrime(i))
                    return i;
            }
            return min;
        }

        public static int CombineHashCodes(int h1, int h2)
        {
          return ((h1 << 5) + h1) ^ h2; // taken from Syste.Tuple.CombineHashCodes
        }

        public static int CombineHashCodes(params int[] h)
        {
          return CombineHashCodes((IEnumerable<int>)h);
        }

        public static int CombineHashCodes(IEnumerable<int> h)
        {
          return h == null ? 0 : h.Aggregate(0, CombineHashCodes);
        }

        public static int GetStructuralHashCode<T1, T2>(T1 x1, T2 x2)
        {
          return CombineHashCodes(x1.GetHashCode(), x2.GetHashCode());
        }

        public static int GetStructuralHashCode<T1, T2, T3>(T1 x1, T2 x2, T3 x3)
        {
          return CombineHashCodes(x1.GetHashCode(), x2.GetHashCode(), x3.GetHashCode());
        }

        public static int GetStructuralHashCode<T1, T2, T3, T4>(T1 x1, T2 x2, T3 x3, T4 x4)
        {
          return CombineHashCodes(x1.GetHashCode(), x2.GetHashCode(), x3.GetHashCode(), x4.GetHashCode());
        }
    }
}
