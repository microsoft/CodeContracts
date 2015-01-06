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

    public class ArrayList
    {

        public object this [int index]
        {
          get
            CodeContract.Requires(index >= 0);
            CodeContract.Requires(index < this.Count);
          set
            CodeContract.Requires(index >= 0);
            CodeContract.Requires(index < this.Count);
        }

        public int Capacity
        {
          get
            CodeContract.Ensures(result >= 0);
          set
            CodeContract.Requires(value >= this.Count);
        }

        public int Count
        {
          get
            CodeContract.Ensures(result >= 0);
        }

        public object SyncRoot
        {
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

        public bool IsReadOnly
        {
          get;
        }

        public void TrimToSize () {
            CodeContract.Requires(! this.IsReadOnly);
            CodeContract.Requires(! this.IsFixedSize);

        }
        public Array ToArray (Type! type) {
            CodeContract.Requires(type != null);

          CodeContract.Ensures(CodeContract.Result<Array>() != null);
          return default(Array);
        }
        public object[] ToArray () {

          CodeContract.Ensures(CodeContract.Result<object[]>() != null);
          return default(object[]);
        }
        public static ArrayList Synchronized (ArrayList! list) {
            CodeContract.Requires(list != null);
            CodeContract.Ensures(result.IsSynchronized);

          return default(ArrayList);
        }
        public static IList Synchronized (IList! list) {
            CodeContract.Requires(list != null);
            CodeContract.Ensures(result.IsSynchronized);

          return default(IList);
        }
        public void Sort (int index, int count, IComparer comparer) {
            CodeContract.Requires(index >= 0);
            CodeContract.Requires(count >= 0);
            CodeContract.Requires(index + count <= Count);
            CodeContract.Requires(! this.IsReadOnly);

        }
        public void Sort (IComparer comparer) {
            CodeContract.Requires(! this.IsReadOnly);

        }
        public void Sort () {
            CodeContract.Requires(! this.IsReadOnly);

        }
        public ArrayList GetRange (int index, int count) {
            CodeContract.Requires(index >= 0);
            CodeContract.Requires(count >= 0);
            CodeContract.Requires(index + count <= Count);

          return default(ArrayList);
        }
        public void SetRange (int index, ICollection! c) {
            CodeContract.Requires(c != null);
            CodeContract.Requires(index >= 0);
            CodeContract.Requires(index + c.Count <= Count);
            CodeContract.Requires(! this.IsReadOnly);

        }
        public void Reverse (int index, int count) {
            CodeContract.Requires(index >= 0);
            CodeContract.Requires(count >= 0);
            CodeContract.Requires(index + count <= Count);
            CodeContract.Requires(! this.IsReadOnly);

        }
        public void Reverse () {
            CodeContract.Requires(! this.IsReadOnly);

        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static ArrayList Repeat (object value, int count) {
            CodeContract.Requires(count >= 0);

          return default(ArrayList);
        }
        public void RemoveRange (int index, int count) {
            CodeContract.Requires(index >= 0);
            CodeContract.Requires(count >= 0);
            CodeContract.Requires(index + count <= Count);
            CodeContract.Requires(! this.IsReadOnly);
            CodeContract.Requires(! this.IsFixedSize);

        }
        public void RemoveAt (int index) {
            CodeContract.Requires(index >= 0);
            CodeContract.Requires(index < Count);
            CodeContract.Requires(! this.IsReadOnly);
            CodeContract.Requires(! this.IsFixedSize);

        }
        public void Remove (object obj) {
            CodeContract.Requires(! this.IsReadOnly);
            CodeContract.Requires(! this.IsFixedSize);

        }
        public static ArrayList ReadOnly (ArrayList list) {
            CodeContract.Requires(list != null);
            CodeContract.Ensures(result.IsReadOnly);

          CodeContract.Ensures(CodeContract.Result<ArrayList>() != null);
          return default(ArrayList);
        }
        public static IList ReadOnly (IList list) {
            CodeContract.Requires(list != null);
            CodeContract.Ensures(result.IsReadOnly);

          CodeContract.Ensures(CodeContract.Result<IList>() != null);
          return default(IList);
        }
        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public int LastIndexOf (object value, int startIndex, int count) {
            CodeContract.Requires(0 <= count);
            CodeContract.Requires(0 <= startIndex);
            CodeContract.Requires(startIndex + count <= Count);
            CodeContract.Ensures(-1 <= result && result < this.Count);

          return default(int);
        }
        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public int LastIndexOf (object value, int startIndex) {
            CodeContract.Requires(startIndex >= 0);
            CodeContract.Requires(startIndex < this.Count);
            CodeContract.Ensures(-1 <= result && result < this.Count);

          return default(int);
        }
        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public int LastIndexOf (object value) {
            CodeContract.Ensures(-1 <= result && result < this.Count);

          return default(int);
        }
        public void InsertRange (int index, ICollection! c) {
            CodeContract.Requires(c != null);
            CodeContract.Requires(index >= 0);
            CodeContract.Requires(index <= Count);
            CodeContract.Requires(! this.IsReadOnly);
            CodeContract.Requires(! this.IsFixedSize);

        }
        public void Insert (int index, object value) {
            CodeContract.Requires(index >= 0);
            CodeContract.Requires(index <= Count);
            CodeContract.Requires(! this.IsReadOnly);
            CodeContract.Requires(! this.IsFixedSize);

        }
        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public int IndexOf (object value, int startIndex, int count) {
            CodeContract.Requires(0 <= count);
            CodeContract.Requires(0 <= startIndex);
            CodeContract.Requires(startIndex + count <= Count);
            CodeContract.Ensures(-1 <= result && result < this.Count);

          return default(int);
        }
        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public int IndexOf (object value, int startIndex) {
            CodeContract.Requires(startIndex >= 0);
            CodeContract.Requires(startIndex < this.Count);
            CodeContract.Ensures(-1 <= result && result < this.Count);

          return default(int);
        }
        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public int IndexOf (object value) {
            CodeContract.Ensures(-1 <= result && result < this.Count);

          return default(int);
        }
        [Pure] [GlobalAccess(false)] [Escapes(true,false)]
        public IEnumerator GetEnumerator (int index, int count) {
            CodeContract.Requires(index >= 0);
            CodeContract.Requires(count >= 0);
            CodeContract.Ensures(result.IsNew);

          CodeContract.Ensures(CodeContract.Result<IEnumerator>() != null);
          return default(IEnumerator);
        }
        [Pure] [GlobalAccess(false)] [Escapes(true,false)]
        public IEnumerator GetEnumerator () {
            CodeContract.Ensures(result.IsNew);

          CodeContract.Ensures(CodeContract.Result<IEnumerator>() != null);
          return default(IEnumerator);
        }
        public static ArrayList FixedSize (ArrayList! list) {
            CodeContract.Requires(list != null);
            CodeContract.Ensures(result.IsFixedSize);

          return default(ArrayList);
        }
        public static IList FixedSize (IList! list) {
            CodeContract.Requires(list != null);
            CodeContract.Ensures(result.IsFixedSize);

          return default(IList);
        }
        public void CopyTo (int index, Array! array, int arrayIndex, int count) {
            CodeContract.Requires(array != null);
            CodeContract.Requires(array.Rank == 1);
            CodeContract.Requires(index >= 0);
            CodeContract.Requires(index < this.Count);
            CodeContract.Requires(arrayIndex >= 0);
            CodeContract.Requires(arrayIndex < array.Length);
            CodeContract.Requires(count >= 0);
            CodeContract.Requires(index + count <= Count);
            CodeContract.Requires( arrayIndex + count <= array.Length);

        }
        public void CopyTo (Array! array, int arrayIndex) {
            CodeContract.Requires(array != null);
            CodeContract.Requires(array.Rank == 1);
            CodeContract.Requires(arrayIndex >= 0);
            CodeContract.Requires(arrayIndex < this.Count);

        }
        public void CopyTo (Array! array) {
            CodeContract.Requires(array != null);
            CodeContract.Requires(array.Rank == 1);

        }
        public bool Contains (object item) {

          return default(bool);
        }
        public object Clone () {

          return default(object);
        }
        public void Clear () {
            CodeContract.Requires(! this.IsReadOnly);
            CodeContract.Requires(! this.IsFixedSize);

        }
        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public int BinarySearch (object value, IComparer comparer) {
            CodeContract.Ensures(-1 <= result && result < this.Count);

          return default(int);
        }
        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public int BinarySearch (object value) {
            CodeContract.Ensures(-1 <= result && result < this.Count);

          return default(int);
        }
        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public int BinarySearch (int index, int count, object value, IComparer comparer) {
            CodeContract.Requires(index >= 0);
            CodeContract.Requires(count >= 0);
            CodeContract.Requires(index + count <= Count);
            CodeContract.Ensures(-1 <= result && result < this.Count);

          return default(int);
        }
        public void AddRange (ICollection! c) {
            CodeContract.Requires(c != null);
            CodeContract.Requires(! this.IsReadOnly);
            CodeContract.Requires(! this.IsFixedSize);

        }
        public int Add (object value) {
            CodeContract.Requires(! this.IsReadOnly);
            CodeContract.Requires(! this.IsFixedSize);
            CodeContract.Ensures(this.Count == old(this.Count) + 1);

          return default(int);
        }
        public static ArrayList Adapter (IList! list) {
            CodeContract.Requires(list != null);

          return default(ArrayList);
        }
        public ArrayList (ICollection! c) {
            CodeContract.Requires(c != null);
            CodeContract.Ensures(this.Capacity == c.Count);
            CodeContract.Ensures(this.Count == c.Count);
            CodeContract.Ensures(!this.IsReadOnly && !this.IsFixedSize);

          return default(ArrayList);
        }
        public ArrayList (int capacity) {
            CodeContract.Requires(capacity >= 0);
            CodeContract.Ensures(capacity == 0 ==> this.Capacity == 16);
            CodeContract.Ensures(capacity > 0 ==> this.Capacity == capacity);
            CodeContract.Ensures(!this.IsReadOnly && !this.IsFixedSize);

          return default(ArrayList);
        }
        public ArrayList () {
            CodeContract.Ensures(this.Count == 0);
            CodeContract.Ensures(this.Capacity == 16); // stated in the documentation
            CodeContract.Ensures(!this.IsReadOnly && !this.IsFixedSize);
          return default(ArrayList);
        }
    }
}
