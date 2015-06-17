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
using Microsoft.SpecSharp.Collections;

namespace System
{

    public class Array
    {

        public int Rank
        {
          [Pure][Reads(ReadsAttribute.Reads.Nothing)]
          get;
        }

        public bool IsReadOnly
        {
          [Pure][Reads(ReadsAttribute.Reads.Nothing)]
          get;
        }

        public object SyncRoot
        {
          get;
        }

        public bool IsSynchronized
        {
          [Pure][Reads(ReadsAttribute.Reads.Nothing)]
          get;
        }

        public bool IsFixedSize
        {
          [Pure][Reads(ReadsAttribute.Reads.Nothing)]
          get;
        }

        public int Length
        {
          [Pure][Reads(ReadsAttribute.Reads.Nothing)]
          get;
            CodeContract.Ensures(0 <= result && result <= int.MaxValue);
        }

        public long LongLength
        {
          [Pure][Reads(ReadsAttribute.Reads.Nothing)]
          get;
            CodeContract.Ensures(0 <= result && result <= long.MaxValue);
        }

        public void Initialize () {

        }
        public static void Sort (Array! keys, Array items, int index, int length, System.Collections.IComparer comparer) {
            CodeContract.Requires(keys != null);
            CodeContract.Requires(keys.Rank == 1);
            CodeContract.Requires(items == null || items.Rank == 1);
            CodeContract.Requires(items == null || keys.GetLowerBound(0) == items.GetLowerBound(0));
            CodeContract.Requires(index >= keys.GetLowerBound(0));
            CodeContract.Requires(length >= 0);
            CodeContract.Requires(keys.GetLowerBound(0) + index + length <= keys.Length);
            CodeContract.Requires(items == null || index + length <= items.Length);

        }
        public static void Sort (Array array, int index, int length, System.Collections.IComparer comparer) {
            CodeContract.Requires(array != null);
            CodeContract.Requires(array.Rank == 1);
            CodeContract.Requires(index >= array.GetLowerBound(0));
            CodeContract.Requires(length >= 0);
            CodeContract.Requires(array.GetLowerBound(0) + index + length <= array.Length);

        }
        public static void Sort (Array! keys, Array items, System.Collections.IComparer comparer) {
            CodeContract.Requires(keys != null);
            CodeContract.Requires(keys.Rank == 1);
            CodeContract.Requires(items == null || items.Rank == 1);
            CodeContract.Requires(items == null || keys.GetLowerBound(0) == items.GetLowerBound(0));

        }
        public static void Sort (Array! array, System.Collections.IComparer comparer) {
            CodeContract.Requires(array != null);
            CodeContract.Requires(array.Rank == 1);

        }
        public static void Sort (Array keys, Array items, int index, int length) {
            CodeContract.Requires(keys != null);
            CodeContract.Requires(keys.Rank == 1);
            CodeContract.Requires(items == null || items.Rank == 1);
            CodeContract.Requires(items == null || keys.GetLowerBound(0) == items.GetLowerBound(0));
            CodeContract.Requires(index >= keys.GetLowerBound(0));
            CodeContract.Requires(length >= 0);
            CodeContract.Requires(keys.GetLowerBound(0) + index + length <= keys.Length);
            CodeContract.Requires(items == null || index + length <= items.Length);

        }
        public static void Sort (Array array, int index, int length) {
            CodeContract.Requires(array != null);
            CodeContract.Requires(array.Rank == 1);
            CodeContract.Requires(index >= array.GetLowerBound(0));
            CodeContract.Requires(length >= 0);
            CodeContract.Requires(array.GetLowerBound(0) + index + length <= array.Length);

        }
        public static void Sort (Array! keys, Array items) {
            CodeContract.Requires(keys != null);
            CodeContract.Requires(keys.Rank == 1);
            CodeContract.Requires(items == null || items.Rank == 1);
            CodeContract.Requires(items == null || keys.GetLowerBound(0) == items.GetLowerBound(0));

        }
        public static void Sort (Array! array) {
            CodeContract.Requires(array != null);
            CodeContract.Requires(array.Rank == 1);

        }
        public static void Reverse (Array! array, int index, int length) {
            CodeContract.Requires(array != null);
            CodeContract.Requires(array.Rank == 1);
            CodeContract.Requires(length >= 0);
            CodeContract.Requires(index >= array.GetLowerBound(0));
            CodeContract.Requires(array.GetLowerBound(0) + index + length <= array.Length);

        }
        public static void Reverse (Array! array) {
            CodeContract.Requires(array != null);
            CodeContract.Requires(array.Rank == 1);

        }
        public static int LastIndexOf (Array! array, object value, int startIndex, int count) {
            CodeContract.Requires(array != null);
            CodeContract.Requires(array.Rank == 1);
            CodeContract.Requires(count >= 0);
            CodeContract.Requires(count <= startIndex - array.GetLowerBound(0) + 1);
            CodeContract.Requires(startIndex >= array.GetLowerBound(0));
            CodeContract.Requires(startIndex < array.Length + array.GetLowerBound(0));
            CodeContract.Ensures(result == array.GetLowerBound(0)-1 || (startIndex+1 - count <= result && result <= startIndex));

          return default(int);
        }
        public static int LastIndexOf (Array! array, object value, int startIndex) {
            CodeContract.Requires(array != null);
            CodeContract.Requires(array.Rank == 1);
            CodeContract.Requires(startIndex >= array.GetLowerBound(0));
            CodeContract.Requires(startIndex < array.Length + array.GetLowerBound(0));
            CodeContract.Ensures(array.GetLowerBound(0)-1 <= result && result <= startIndex);

          return default(int);
        }
        public static int LastIndexOf (Array! array, object value) {
            CodeContract.Requires(array != null);
            CodeContract.Requires(array.Rank == 1);
            CodeContract.Ensures(array.GetLowerBound(0)-1 <= result && result <= array.GetUpperBound(0));

          return default(int);
        }
        public static int IndexOf (Array! array, object value, int startIndex, int count) {
            CodeContract.Requires(array != null);
            CodeContract.Requires(array.Rank == 1);
            CodeContract.Requires(startIndex >= array.GetLowerBound(0));
            CodeContract.Requires(startIndex <= array.GetLowerBound(0) + array.Length);
            CodeContract.Requires(count >= 0);
            CodeContract.Requires(startIndex + count <= array.GetLowerBound(0) + array.Length);
            CodeContract.Ensures(result == array.GetLowerBound(0)-1 || (startIndex <= result && result < startIndex + count));

          return default(int);
        }
        public static int IndexOf (Array! array, object value, int startIndex) {
            CodeContract.Requires(array != null);
            CodeContract.Requires(array.Rank == 1);
            CodeContract.Requires(startIndex >= array.GetLowerBound(0));
            CodeContract.Requires(startIndex <= array.GetLowerBound(0) + array.Length);
            CodeContract.Ensures(result == array.GetLowerBound(0)-1 || (startIndex <= result && result <= array.GetUpperBound(0)));

          return default(int);
        }
        public static int IndexOf (Array! array, object value) {
            CodeContract.Requires(array != null);
            CodeContract.Requires(array.Rank == 1);
            CodeContract.Ensures(array.GetLowerBound(0)-1 <= result && result <= array.GetUpperBound(0));

          return default(int);
        }
        [Pure] [GlobalAccess(false)] [Escapes(true,false)]
        public System.Collections.IEnumerator GetEnumerator () {
            CodeContract.Ensures(result.IsNew);

          CodeContract.Ensures(CodeContract.Result<System.Collections.IEnumerator>() != null);
          return default(System.Collections.IEnumerator);
        }
        public void CopyTo (Array! array, long index) {
            CodeContract.Requires(array != null);
            CodeContract.Requires(array.GetLowerBound(0) <= index);
            CodeContract.Requires(this.Rank == 1);
            CodeContract.Requires(array.Rank == 1);
            CodeContract.Requires(this.Length <= array.GetUpperBound(0) + 1 - index);
            modifies this.0, array.*;

        }
        public void CopyTo (Array! array, int index) {
            CodeContract.Requires(array != null);
            CodeContract.Requires(array.GetLowerBound(0) <= index);
            CodeContract.Requires(this.Rank == 1);
            CodeContract.Requires(array.Rank == 1);
            CodeContract.Requires(this.Length <= array.GetUpperBound(0) + 1 - index);
            modifies this.0, array.*;

        }
        public static int BinarySearch (Array! array, int index, int length, object value, System.Collections.IComparer comparer) {
            CodeContract.Requires(array != null);
            CodeContract.Requires(array.Rank == 1);
            CodeContract.Requires(index >= array.GetLowerBound(0));
            CodeContract.Requires(length >= 0);
            CodeContract.Requires(length <= array.Length - index);
            CodeContract.Ensures(result == array.GetLowerBound(0)-1 || (index <= result && result < index + length));

          return default(int);
        }
        public static int BinarySearch (Array! array, object value, System.Collections.IComparer comparer) {
            CodeContract.Requires(array != null);
            CodeContract.Requires(array.Rank == 1);
            CodeContract.Ensures(array.GetLowerBound(0)-1 <= result && result <= array.GetUpperBound(0));

          return default(int);
        }
        public static int BinarySearch (Array array, int index, int length, object value) {
            CodeContract.Requires(array != null);
            CodeContract.Requires(array.Rank == 1);
            CodeContract.Requires(index >= array.GetLowerBound(0));
            CodeContract.Requires(length >= 0);
            CodeContract.Requires(length <= array.Length - index);
            CodeContract.Ensures(result == array.GetLowerBound(0)-1 || (index <= result && result < index + length));

          return default(int);
        }
        public static int BinarySearch (Array! array, object value) {
            CodeContract.Requires(array != null);
            CodeContract.Requires(array.Rank == 1);
            CodeContract.Ensures(array.GetLowerBound(0)-1 <= result && result <= array.GetUpperBound(0));

          return default(int);
        }
        [Pure]
        public object Clone () {
            CodeContract.Ensures(result.GetType() == this.GetType());
            CodeContract.Ensures(Owner.New(result));


          CodeContract.Ensures(CodeContract.Result<object>() != null);
          return default(object);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public int GetLowerBound (int arg0) {

          return default(int);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public int GetUpperBound (int arg0) {

          return default(int);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public long GetLongLength (int dimension) {

          return default(long);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public int GetLength (int arg0) {

          return default(int);
        }
        public void SetValue (object value, long[] indices) {

        }
        public void SetValue (object value, long index1, long index2, long index3) {
            CodeContract.Requires(index1 <= 2147483647);
            CodeContract.Requires(index1 >= -2147483648);
            CodeContract.Requires(index2 <= 2147483647);
            CodeContract.Requires(index2 >= -2147483648);
            CodeContract.Requires(index3 <= 2147483647);
            CodeContract.Requires(index3 >= -2147483648);
            CodeContract.Requires(this.Rank == 3);
            CodeContract.Requires(index1 >= this.GetLowerBound(0));
            CodeContract.Requires(index1 <= this.GetUpperBound(0));
            CodeContract.Requires(index2 >= this.GetLowerBound(0));
            CodeContract.Requires(index2 <= this.GetUpperBound(0));
            CodeContract.Requires(index3 >= this.GetLowerBound(0));
            CodeContract.Requires(index3 <= this.GetUpperBound(0));

        }
        public void SetValue (object value, long index1, long index2) {
            CodeContract.Requires(index1 <= 2147483647);
            CodeContract.Requires(index1 >= -2147483648);
            CodeContract.Requires(index2 <= 2147483647);
            CodeContract.Requires(index2 >= -2147483648);
            CodeContract.Requires(this.Rank == 2);
            CodeContract.Requires(index1 >= this.GetLowerBound(0));
            CodeContract.Requires(index1 <= this.GetUpperBound(0));
            CodeContract.Requires(index2 >= this.GetLowerBound(0));
            CodeContract.Requires(index2 <= this.GetUpperBound(0));

        }
        public void SetValue (object value, long index) {
            CodeContract.Requires(index <= 2147483647);
            CodeContract.Requires(index >= -2147483648);
            CodeContract.Requires(this.Rank == 1);
            CodeContract.Requires(index >= this.GetLowerBound(0));
            CodeContract.Requires(index <= this.GetUpperBound(0));

        }
        public void SetValue (object value, int[]! indices) {
            CodeContract.Requires(indices != null);
            //CodeContract.Requires(Forall { int i in indices; this.GetLowerBound(i) <= indices[i] && indices[i] <= this.GetUpperBound(i) });

        }
        public void SetValue (object value, int index1, int index2, int index3) {
            CodeContract.Requires(this.Rank == 3);
            CodeContract.Requires(index1 >= this.GetLowerBound(0));
            CodeContract.Requires(index1 <= this.GetUpperBound(0));
            CodeContract.Requires(index2 >= this.GetLowerBound(0));
            CodeContract.Requires(index2 <= this.GetUpperBound(0));
            CodeContract.Requires(index3 >= this.GetLowerBound(0));
            CodeContract.Requires(index3 <= this.GetUpperBound(0));

        }
        public void SetValue (object value, int index1, int index2) {
            CodeContract.Requires(this.Rank == 2);
            CodeContract.Requires(index1 >= this.GetLowerBound(0));
            CodeContract.Requires(index1 <= this.GetUpperBound(0));
            CodeContract.Requires(index2 >= this.GetLowerBound(0));
            CodeContract.Requires(index2 <= this.GetUpperBound(0));

        }
        public void SetValue (object value, int index) {
            CodeContract.Requires(this.Rank == 1);
            CodeContract.Requires(index >= this.GetLowerBound(0));
            CodeContract.Requires(index <= this.GetUpperBound(0));

        }
        public object GetValue (long[] indices) {

          return default(object);
        }
        public object GetValue (long index1, long index2, long index3) {
            CodeContract.Requires(index1 <= 2147483647);
            CodeContract.Requires(index1 >= -2147483648);
            CodeContract.Requires(index2 <= 2147483647);
            CodeContract.Requires(index2 >= -2147483648);
            CodeContract.Requires(index3 <= 2147483647);
            CodeContract.Requires(index3 >= -2147483648);
            CodeContract.Requires(this.Rank == 3);
            CodeContract.Requires(index1 >= this.GetLowerBound(0));
            CodeContract.Requires(index1 <= this.GetUpperBound(0));
            CodeContract.Requires(index2 >= this.GetLowerBound(0));
            CodeContract.Requires(index2 <= this.GetUpperBound(0));
            CodeContract.Requires(index3 >= this.GetLowerBound(0));
            CodeContract.Requires(index3 <= this.GetUpperBound(0));

          return default(object);
        }
        public object GetValue (long index1, long index2) {
            CodeContract.Requires(index1 <= 2147483647);
            CodeContract.Requires(index1 >= -2147483648);
            CodeContract.Requires(index2 <= 2147483647);
            CodeContract.Requires(index2 >= -2147483648);
            CodeContract.Requires(this.Rank == 2);
            CodeContract.Requires(index1 >= this.GetLowerBound(0));
            CodeContract.Requires(index1 <= this.GetUpperBound(0));
            CodeContract.Requires(index2 >= this.GetLowerBound(0));
            CodeContract.Requires(index2 <= this.GetUpperBound(0));

          return default(object);
        }
        public object GetValue (long index) {
            CodeContract.Requires(index <= 2147483647);
            CodeContract.Requires(index >= -2147483648);
            CodeContract.Requires(this.Rank == 1);
            CodeContract.Requires(index >= this.GetLowerBound(0));
            CodeContract.Requires(index <= this.GetUpperBound(0));

          return default(object);
        }
        public object GetValue (int index1, int index2, int index3) {
            CodeContract.Requires(this.Rank == 3);
            CodeContract.Requires(index1 >= this.GetLowerBound(0));
            CodeContract.Requires(index1 <= this.GetUpperBound(0));
            CodeContract.Requires(index2 >= this.GetLowerBound(0));
            CodeContract.Requires(index2 <= this.GetUpperBound(0));
            CodeContract.Requires(index3 >= this.GetLowerBound(0));
            CodeContract.Requires(index3 <= this.GetUpperBound(0));

          return default(object);
        }
        public object GetValue (int index1, int index2) {
            CodeContract.Requires(this.Rank == 2);
            CodeContract.Requires(index1 >= this.GetLowerBound(0));
            CodeContract.Requires(index1 <= this.GetUpperBound(0));
            CodeContract.Requires(index2 >= this.GetLowerBound(0));
            CodeContract.Requires(index2 <= this.GetUpperBound(0));

          return default(object);
        }
        public object GetValue (int index) {
            CodeContract.Requires(this.Rank == 1);
            CodeContract.Requires(index >= this.GetLowerBound(0));
            CodeContract.Requires(index <= this.GetUpperBound(0));


          return default(object);
        }
        public object GetValue (int[]! indices) {
            CodeContract.Requires(indices != null);

          return default(object);
        }
        public static void Clear (Array! array, int index, int length) {
            CodeContract.Requires(array != null);
            CodeContract.Requires(array.Rank == 1);
            CodeContract.Requires(index >= array.GetLowerBound(0));
            CodeContract.Requires(length >= 0);
            CodeContract.Requires(array.Length - (index + array.GetLowerBound(0)) >= length);

        }
        public static void Copy (Array sourceArray, long sourceIndex, Array destinationArray, long destinationIndex, long length) {
            CodeContract.Requires(sourceArray != null);
            CodeContract.Requires(destinationArray != null);
            CodeContract.Requires(sourceArray.Rank == destinationArray.Rank);
            CodeContract.Requires(sourceIndex >= sourceArray.GetLowerBound(0));
            CodeContract.Requires(destinationIndex >= destinationArray.GetLowerBound(0));
            CodeContract.Requires(length >= 0);
            CodeContract.Requires(sourceIndex + length <= sourceArray.GetLowerBound(0) + sourceArray.Length);
            CodeContract.Requires(destinationIndex + length <= destinationArray.GetLowerBound(0) + destinationArray.Length);
            CodeContract.Requires(sourceIndex <= 2147483647);
            CodeContract.Requires(sourceIndex >= -2147483648);
            CodeContract.Requires(destinationIndex <= 2147483647);
            CodeContract.Requires(destinationIndex >= -2147483648);
            CodeContract.Requires(length <= 2147483647);
            CodeContract.Requires(length >= -2147483648);
            modifies destinationArray.*;

        }
        public static void Copy (Array sourceArray, Array destinationArray, long length) {
            CodeContract.Requires(sourceArray != null);
            CodeContract.Requires(destinationArray != null);
            CodeContract.Requires(sourceArray.Rank == destinationArray.Rank);
            CodeContract.Requires(sourceArray.Rank == destinationArray.Rank);
            CodeContract.Requires(length >= 0);
            CodeContract.Requires(length <= sourceArray.GetLowerBound(0) + sourceArray.Length);
            CodeContract.Requires(length <= destinationArray.GetLowerBound(0) + destinationArray.Length);
            CodeContract.Requires(length <= 2147483647);
            CodeContract.Requires(length >= -2147483648);
            modifies destinationArray.*;

        }
        public static void Copy (Array! sourceArray, int sourceIndex, Array! destinationArray, int destinationIndex, int length) {
            CodeContract.Requires(sourceArray != null);
            CodeContract.Requires(destinationArray != null);
            CodeContract.Requires(sourceArray.Rank == destinationArray.Rank);
            CodeContract.Requires(sourceIndex >= sourceArray.GetLowerBound(0));
            CodeContract.Requires(destinationIndex >= destinationArray.GetLowerBound(0));
            CodeContract.Requires(length >= 0);
            CodeContract.Requires(sourceIndex + length <= sourceArray.GetLowerBound(0) + sourceArray.Length);
            CodeContract.Requires(destinationIndex + length <= destinationArray.GetLowerBound(0) + destinationArray.Length);
            modifies destinationArray.*;

        }
        public static void Copy (Array! sourceArray, Array! destinationArray, int length) {
            CodeContract.Requires(sourceArray != null);
            CodeContract.Requires(destinationArray != null);
            CodeContract.Requires(sourceArray.Rank == destinationArray.Rank);
            CodeContract.Requires(sourceArray.Rank == destinationArray.Rank);
            CodeContract.Requires(length >= 0);
            CodeContract.Requires(length <= sourceArray.GetLowerBound(0) + sourceArray.Length);
            CodeContract.Requires(length <= destinationArray.GetLowerBound(0) + destinationArray.Length);
            modifies destinationArray.*;

        }
        public static Array CreateInstance (Type! elementType, int[]! lengths, int[]! lowerBounds) {
            CodeContract.Requires(elementType != null);
            CodeContract.Requires(lengths != null);
            CodeContract.Requires(lowerBounds != null);
            CodeContract.Requires(lengths.Length == lowerBounds.Length);
            CodeContract.Requires(lengths.Length != 0);

          CodeContract.Ensures(CodeContract.Result<Array>() != null);
          return default(Array);
        }
        public static Array CreateInstance (Type! elementType, long[]! lengths) {
            CodeContract.Requires(elementType != null);
            CodeContract.Requires(lengths != null);
            CodeContract.Requires(lengths.Length > 0);
            // CodeContract.Requires(Forall { int i in lengths; lengths[i] >= 0; });
            CodeContract.Ensures(result.Rank == lengths.Length);
            // CodeContract.Ensures(Forall { int i in lengths); result.GetLength(i) == length[i]); }

          CodeContract.Ensures(CodeContract.Result<Array>() != null);
          return default(Array);
        }
        public static Array CreateInstance (Type! elementType, int[]! lengths) {
            CodeContract.Requires(elementType != null);
            CodeContract.Requires(lengths != null);
            CodeContract.Requires(lengths.Length > 0);
            // CodeContract.Requires(Forall { int i in lengths; lengths[i] >= 0; });
            CodeContract.Ensures(result.Rank == lengths.Length);
            // CodeContract.Ensures(Forall { int i in lengths); result.GetLength(i) == length[i]); }

          CodeContract.Ensures(CodeContract.Result<Array>() != null);
          return default(Array);
        }
        public static Array CreateInstance (Type! elementType, int length1, int length2, int length3) {
            CodeContract.Requires(elementType != null);
            CodeContract.Requires(length1 >= 0);
            CodeContract.Requires(length2 >= 0);
            CodeContract.Requires(length3 >= 0);
            CodeContract.Ensures(result.Rank == 3);
            CodeContract.Ensures(result.GetLength(0) == length1 && result.GetLength(1) == length2 && result.GetLength(2) == length3);

          CodeContract.Ensures(CodeContract.Result<Array>() != null);
          return default(Array);
        }
        public static Array CreateInstance (Type! elementType, int length1, int length2) {
            CodeContract.Requires(elementType != null);
            CodeContract.Requires(length1 >= 0);
            CodeContract.Requires(length2 >= 0);
            CodeContract.Ensures(result.Rank == 2);
            CodeContract.Ensures(result.GetLength(0) == length1 && result.GetLength(1) == length2);

          CodeContract.Ensures(CodeContract.Result<Array>() != null);
          return default(Array);
        }
        public static Array CreateInstance (Type! elementType, int length) {
            CodeContract.Requires(elementType != null);
            CodeContract.Requires(length >= 0);
            CodeContract.Ensures(result.Rank == 1);
            CodeContract.Ensures(result.GetLength(0) == length);
          CodeContract.Ensures(CodeContract.Result<Array>() != null);
          return default(Array);
        }
    }
}
