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

    public class Overlapped
    {

        public int OffsetHigh
        {
          get;
          set;
        }

        public int EventHandle
        {
          get;
          set;
        }

        public int OffsetLow
        {
          get;
          set;
        }

        public IAsyncResult AsyncResult
        {
          get;
          set;
        }

#if DO_UNSAFE_STUFF_LATER
        public static unsafe void Free (NativeOverlapped* nativeOverlappedPtr) {
            Contract.Requires(nativeOverlappedPtr != null);

        }
        public static unsafe Overlapped Unpack (NativeOverlapped* nativeOverlappedPtr) {
            Contract.Requires(nativeOverlappedPtr != null);
                  return default(Overlapped);
        }
#endif


        public NativeOverlapped UnsafePack (IOCompletionCallback iocb) {

          return default(NativeOverlapped);
        }
        public NativeOverlapped Pack (IOCompletionCallback iocb) {

          return default(NativeOverlapped);
        }
        public Overlapped (int offsetLo, int offsetHi, int hEvent, IAsyncResult ar) {

          return default(Overlapped);
        }
        public Overlapped () {
          return default(Overlapped);
        }
    }
}
