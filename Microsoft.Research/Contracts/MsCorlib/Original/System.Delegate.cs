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

namespace System
{

    public class Delegate
    {

        public System.Reflection.MethodInfo Method
        {
          get;
        }

        public object Target
        {
          get;
        }

        public void GetObjectData (System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) {

        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static bool operator != (Delegate d1, Delegate d2) {

          return default(bool);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static bool operator == (Delegate d1, Delegate d2) {

          return default(bool);
        }
        public static Delegate CreateDelegate (Type! type, System.Reflection.MethodInfo! method) {
            CodeContract.Requires(type != null);
            CodeContract.Requires(method != null);

          return default(Delegate);
        }
        public static Delegate CreateDelegate (Type! type, Type! target, string! method) {
            CodeContract.Requires(type != null);
            CodeContract.Requires(target != null);
            CodeContract.Requires(method != null);

          return default(Delegate);
        }
        public static Delegate CreateDelegate (Type! type, object! target, string! method, bool ignoreCase) {
            CodeContract.Requires(type != null);
            CodeContract.Requires(target != null);
            CodeContract.Requires(method != null);

          return default(Delegate);
        }
        public static Delegate CreateDelegate (Type! type, object! target, string! method) {
            CodeContract.Requires(type != null);
            CodeContract.Requires(target != null);
            CodeContract.Requires(method != null);

          return default(Delegate);
        }
        public object Clone () {

          return default(object);
        }
        public static Delegate RemoveAll (Delegate source, Delegate value) {

          return default(Delegate);
        }
        public static Delegate Remove (Delegate a, Delegate b) {
          CodeContract.Ensures(((object)a) == null && ((object)b) == null ==> ((object)result) == null);
          CodeContract.Ensures(((object)a) == null && ((object)b) != null ==> ((object)result) == null);
          CodeContract.Ensures(((object)a) != null && ((object)b) == null ==> ((object)result) == (object)a);
          CodeContract.Ensures(((object)a) != null && ((object)b) != null ==> ((object)result) != null && result.GetType() == a.GetType() && Owner.Same(result, a));

          return default(Delegate);
        }
        public Delegate[] GetInvocationList () {

          return default(Delegate[]);
        }
        public static Delegate Combine (Delegate[] delegates) {

          return default(Delegate);
        }
        public static Delegate Combine (Delegate a, Delegate b) {
          CodeContract.Ensures(((object)a) == null && ((object)b) == null ==> ((object)result) == null);
          CodeContract.Ensures(((object)a) == null && ((object)b) != null ==> ((object)result) == (object)b);
          CodeContract.Ensures(((object)a) != null && ((object)b) == null ==> ((object)result) == (object)a);
          CodeContract.Ensures(((object)a) != null && ((object)b) != null ==> ((object)result) != null && result.GetType() == a.GetType() && Owner.Same(result, a));

          return default(Delegate);
        }
        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public int GetHashCode () {

          return default(int);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public bool Equals (object obj) {

          return default(bool);
        }
          return default(object);
        }
        public object DynamicInvoke (Object[] args) {
    }
}
