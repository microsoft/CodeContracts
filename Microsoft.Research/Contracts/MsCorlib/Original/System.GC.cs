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

namespace System
{

    public class GC
    {

        public static int MaxGeneration
        {
          get;
        }

        public static Int64 GetTotalMemory (bool forceFullCollection) {

          return default(Int64);
        }
        public static void ReRegisterForFinalize (object! obj) {
            CodeContract.Requires(obj != null);

        }
        public static void SuppressFinalize (object! obj) {
            CodeContract.Requires(obj != null);

        }
        public static void WaitForPendingFinalizers () {

        }
        public static int GetGeneration (WeakReference wo) {

          return default(int);
        }
        public static void KeepAlive (object arg0) {

        }
        public static void Collect () {

        }
        public static void Collect (int generation) {
            CodeContract.Requires(generation >= 0);

        }
        public static int GetGeneration (object arg0) {
          return default(int);
        }
    }
}
