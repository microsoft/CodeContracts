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


namespace System.Collections
{

    public class Hashtable
    {

        public object this [object! key]
        {
          get
            CodeContract.Requires(key != null);
          set;
        }

        public int Count
        {
          get;
        }

        public object! SyncRoot
        {
          get;
        }

        public ICollection! Keys
        {
          [ElementCollection]
          get;
        }

        public bool IsSynchronized
        {
          get;
        }

        public bool IsFixedSize
        {
          get;
        }

        public ICollection! Values
        {
          [ElementCollection]
          get;
        }

        public bool IsReadOnly
        {
          get;
        }

        public void OnDeserialization (object sender) {

        }
        public void GetObjectData (System.Runtime.Serialization.SerializationInfo! info, System.Runtime.Serialization.StreamingContext context) {
            CodeContract.Requires(info != null);

        }
        public static Hashtable Synchronized (Hashtable table) {
            CodeContract.Requires(table != null);

          CodeContract.Ensures(CodeContract.Result<Hashtable>() != null);
          return default(Hashtable);
        }
        public void Remove (object! key) {
            CodeContract.Requires(key != null);

        }
        [Pure] [GlobalAccess(false)] [Escapes(true,false)]
        public IDictionaryEnumerator GetEnumerator () {
            CodeContract.Ensures(result.IsNew);

          CodeContract.Ensures(CodeContract.Result<IDictionaryEnumerator>() != null);
          return default(IDictionaryEnumerator);
        }
        public void CopyTo (Array! array, int arrayIndex) {
            CodeContract.Requires(array != null);
            CodeContract.Requires(arrayIndex >= 0);

        }
        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public bool ContainsValue (object value) {

          return default(bool);
        }
        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public bool ContainsKey (object! key) {
            CodeContract.Requires(key != null);

          return default(bool);
        }
        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public bool Contains (object! key) {

          return default(bool);
        }
        public object Clone () {

          CodeContract.Ensures(CodeContract.Result<object>() != null);
          return default(object);
        }
        public void Clear () {

        }
        public void Add (object! key, object value) {

        }
        public Hashtable (IDictionary! d, Single loadFactor, IHashCodeProvider hcp, IComparer comparer) {
            CodeContract.Requires(d != null);

          return default(Hashtable);
        }
        public Hashtable (IDictionary d, IHashCodeProvider hcp, IComparer comparer) {

          return default(Hashtable);
        }
        public Hashtable (IDictionary d, Single loadFactor) {

          return default(Hashtable);
        }
        public Hashtable (IDictionary d) {

          return default(Hashtable);
        }
        public Hashtable (int capacity, IHashCodeProvider hcp, IComparer comparer) {

          return default(Hashtable);
        }
        public Hashtable (IHashCodeProvider hcp, IComparer comparer) {

          return default(Hashtable);
        }
        public Hashtable (int capacity, Single loadFactor, IHashCodeProvider hcp, IComparer comparer) {
            CodeContract.Requires(capacity >= 0);
            CodeContract.Requires(loadFactor >= 0);
            CodeContract.Requires(loadFactor <= 0);

          return default(Hashtable);
        }
        public Hashtable (int capacity, Single loadFactor) {

          return default(Hashtable);
        }
        public Hashtable (int capacity) {

          return default(Hashtable);
        }
        public Hashtable () {
          return default(Hashtable);
        }
    }
}
