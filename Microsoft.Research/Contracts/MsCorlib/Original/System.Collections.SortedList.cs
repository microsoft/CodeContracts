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

    public class SortedList
    {

        public object this [object key]
        {
          get;
          set
            CodeContract.Requires(key != null);
        }

        public int Capacity
        {
          get;
          set;
        }

        public bool IsFixedSize
        {
          get;
        }

        public ICollection Values
        { [ElementCollection]
          get;
        }

        public object SyncRoot
        {
          get;
        }

        public bool IsSynchronized
        {
          get;
        }

        public ICollection Keys
        { [ElementCollection] 
          get;
        }

        public int Count
        {
          get;
        }

        public bool IsReadOnly
        {
          get;
        }

        public void TrimToSize () {

        }
        public static SortedList Synchronized (SortedList! list) {
            CodeContract.Requires(list != null);

          return default(SortedList);
        }
        public void SetByIndex (int index, object value) {
            CodeContract.Requires(index >= 0);

        }
        public void Remove (object key) {

        }
        public void RemoveAt (int index) {
            CodeContract.Requires(index >= 0);

        }
        public int IndexOfValue (object value) {

          return default(int);
        }
        public int IndexOfKey (object! key) {
            CodeContract.Requires(key != null);

          return default(int);
        }
        public IList GetValueList () {

          return default(IList);
        }
        public IList GetKeyList () {

          return default(IList);
        }
        public object GetKey (int index) {
            CodeContract.Requires(index >= 0);

          return default(object);
        }
        [Pure] [GlobalAccess(false)] [Escapes(true,false)]
        public IDictionaryEnumerator GetEnumerator () {
            CodeContract.Ensures(result.IsNew);

          CodeContract.Ensures(CodeContract.Result<IDictionaryEnumerator>() != null);
          return default(IDictionaryEnumerator);
        }
        public object GetByIndex (int index) {
            CodeContract.Requires(index >= 0);

          return default(object);
        }
        public void CopyTo (Array! array, int arrayIndex) {
            CodeContract.Requires(array != null);
            CodeContract.Requires(arrayIndex >= 0);

        }
        public bool ContainsValue (object value) {

          return default(bool);
        }
        public bool ContainsKey (object key) {

          return default(bool);
        }
        public bool Contains (object key) {

          return default(bool);
        }
        public object Clone () {

          return default(object);
        }
        public void Clear () {

        }
        public void Add (object! key, object value) {
            CodeContract.Requires(key != null);

        }
        public SortedList (IDictionary! d, IComparer comparer) {
            CodeContract.Requires(d != null);

          return default(SortedList);
        }
        public SortedList (IDictionary d) {

          return default(SortedList);
        }
        public SortedList (IComparer comparer, int capacity) {

          return default(SortedList);
        }
        public SortedList (IComparer comparer) {

          return default(SortedList);
        }
        public SortedList (int initialCapacity) {
            CodeContract.Requires(initialCapacity >= 0);

          return default(SortedList);
        }
        public SortedList () {
          return default(SortedList);
        }
    }
}
