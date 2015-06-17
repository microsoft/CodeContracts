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

namespace System.Reflection.Emit
{

    public class UnmanagedMarshal
    {

        public System.Runtime.InteropServices.UnmanagedType BaseType
        {
          get;
        }

        public int ElementCount
        {
          get;
        }

        public System.Runtime.InteropServices.UnmanagedType GetUnmanagedType
        {
          get;
        }

        public Guid IIDGuid
        {
          get;
        }

        public static UnmanagedMarshal DefineLPArray (System.Runtime.InteropServices.UnmanagedType elemType) {

          return default(UnmanagedMarshal);
        }
        public static UnmanagedMarshal DefineByValArray (int elemCount) {

          return default(UnmanagedMarshal);
        }
        public static UnmanagedMarshal DefineSafeArray (System.Runtime.InteropServices.UnmanagedType elemType) {

          return default(UnmanagedMarshal);
        }
        public static UnmanagedMarshal DefineByValTStr (int elemCount) {

          return default(UnmanagedMarshal);
        }
        public static UnmanagedMarshal DefineUnmanagedMarshal (System.Runtime.InteropServices.UnmanagedType unmanagedType) {
            Contract.Requires((int)unmanagedType != 23);
            Contract.Requires((int)unmanagedType != 29);
            Contract.Requires((int)unmanagedType != 30);
            Contract.Requires((int)unmanagedType != 42);
            Contract.Requires((int)unmanagedType != 44);
          return default(UnmanagedMarshal);
        }
    }
}
