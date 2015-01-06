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
using System.Diagnostics.Contracts;

namespace System.Reflection.Emit
{

    public class SignatureHelper
    {

        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public string ToString () {

          return default(string);
        }
        public Byte[] GetSignature () {

          return default(Byte[]);
        }
        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public int GetHashCode () {

          return default(int);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public bool Equals (object obj) {

          return default(bool);
        }
        public void AddSentinel () {

        }
        public void AddArgument (Type clsArgument) {

        }
        public static SignatureHelper GetPropertySigHelper (System.Reflection.Module mod, Type returnType, Type[] parameterTypes) {

          return default(SignatureHelper);
        }
        public static SignatureHelper GetMethodSigHelper (System.Reflection.Module mod, Type returnType, Type[] parameterTypes) {

          return default(SignatureHelper);
        }
        public static SignatureHelper GetMethodSigHelper (System.Reflection.Module mod, System.Reflection.CallingConventions callingConvention, Type returnType) {

          return default(SignatureHelper);
        }
        public static SignatureHelper GetFieldSigHelper (System.Reflection.Module mod) {

          return default(SignatureHelper);
        }
        public static SignatureHelper GetLocalVarSigHelper (System.Reflection.Module mod) {

          return default(SignatureHelper);
        }
        public static SignatureHelper GetMethodSigHelper (System.Reflection.Module mod, System.Runtime.InteropServices.CallingConvention unmanagedCallConv, Type returnType) {
            CodeContract.Requires((int)unmanagedCallConv == 2 || (int)unmanagedCallConv == 3 || (int)unmanagedCallConv == 4 || (int)unmanagedCallConv == 5);
          return default(SignatureHelper);
        }
    }
}
