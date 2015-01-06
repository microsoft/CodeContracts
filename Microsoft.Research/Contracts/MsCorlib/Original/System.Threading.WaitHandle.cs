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

namespace System.Threading
{

    public class WaitHandle
    {

        public int Handle
        {
          get;
          set;
        }

        public void Close () {

        }
        public static int WaitAny (WaitHandle[] waitHandles) {

          return default(int);
        }
        public static int WaitAny (WaitHandle[] waitHandles, TimeSpan timeout, bool exitContext) {

          return default(int);
        }
        public static int WaitAny (WaitHandle[]! waitHandles, int millisecondsTimeout, bool exitContext) {
            CodeContract.Requires(waitHandles != null);
            // NOT LEGAL BECAUSE MAX_WAITHANDLES IS PRIVATE
            //CodeContract.Requires(waitHandles.Length <= System.Threading.WaitHandle.MAX_WAITHANDLES);
            CodeContract.Requires(millisecondsTimeout >= -1);

          return default(int);
        }
        public static bool WaitAll (WaitHandle[] waitHandles) {

          return default(bool);
        }
        public static bool WaitAll (WaitHandle[] waitHandles, TimeSpan timeout, bool exitContext) {

          return default(bool);
        }
        public static bool WaitAll (WaitHandle[]! waitHandles, int millisecondsTimeout, bool exitContext) {
            CodeContract.Requires(waitHandles != null);
            // INVALID BECAUSE MAX_WAITHANDLES IS PRIVATE
            //CodeContract.Requires(waitHandles.Length <= System.Threading.WaitHandle.MAX_WAITHANDLES);
            CodeContract.Requires(millisecondsTimeout >= -1);

          return default(bool);
        }
        public bool WaitOne () {

          return default(bool);
        }
        public bool WaitOne (TimeSpan timeout, bool exitContext) {

          return default(bool);
        }
        public bool WaitOne (int millisecondsTimeout, bool exitContext) {

          return default(bool);
        }
        public WaitHandle () {
          return default(WaitHandle);
        }
    }
}
