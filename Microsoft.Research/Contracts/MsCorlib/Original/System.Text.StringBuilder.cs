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

namespace System.Text
{

    public class StringBuilder
    {

        public Char this [int index]
        {
          get;
              CodeContract.Requires(0 <= index && index < this.Length);
          set;
        }

        public int MaxCapacity
        {
          get;
        }

        public int Length
        {
          get;
              CodeContract.Ensures(0 <= result);
          set;
        }

        public int Capacity
        {
          get;
          set;
        }

        [WriteConfined]
        public StringBuilder Replace (Char oldChar, Char newChar, int startIndex, int count) {
            CodeContract.Requires(count >= 0);

          CodeContract.Ensures(CodeContract.Result<StringBuilder>() != null);
          return default(StringBuilder);
        }
        [WriteConfined]
        public StringBuilder Replace (Char oldChar, Char newChar) {

          CodeContract.Ensures(CodeContract.Result<StringBuilder>() != null);
          return default(StringBuilder);
        }
        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public bool Equals (StringBuilder sb) {

          return default(bool);
        }
        [WriteConfined]
        public StringBuilder Replace (string arg0, string arg1, int arg2, int arg3) {

          CodeContract.Ensures(CodeContract.Result<StringBuilder>() != null);
          return default(StringBuilder);
        }
        [WriteConfined]
        public StringBuilder Replace (string oldValue, string newValue) {

          CodeContract.Ensures(CodeContract.Result<StringBuilder>() != null);
          return default(StringBuilder);
        }
        [WriteConfined]
        public StringBuilder AppendFormat (IFormatProvider provider, string! format, Object[]! args) {
            CodeContract.Requires(format != null);
            CodeContract.Requires(args != null);

          CodeContract.Ensures(CodeContract.Result<StringBuilder>() != null);
          return default(StringBuilder);
        }
        [WriteConfined]
        public StringBuilder AppendFormat (string format, Object[] args) {

          CodeContract.Ensures(CodeContract.Result<StringBuilder>() != null);
          return default(StringBuilder);
        }
        [WriteConfined]
        public StringBuilder AppendFormat (string format, object arg0, object arg1, object arg2) {

          CodeContract.Ensures(CodeContract.Result<StringBuilder>() != null);
          return default(StringBuilder);
        }
        [WriteConfined]
        public StringBuilder AppendFormat (string format, object arg0, object arg1) {

          CodeContract.Ensures(CodeContract.Result<StringBuilder>() != null);
          return default(StringBuilder);
        }
        [WriteConfined]
        public StringBuilder AppendFormat (string format, object arg0) {

          CodeContract.Ensures(CodeContract.Result<StringBuilder>() != null);
          return default(StringBuilder);
        }
        [WriteConfined]
        public StringBuilder Insert (int index, object value) {

          CodeContract.Ensures(CodeContract.Result<StringBuilder>() != null);
          return default(StringBuilder);
        }
        [WriteConfined]
        public StringBuilder Insert (int index, UInt64 value) {

          CodeContract.Ensures(CodeContract.Result<StringBuilder>() != null);
          return default(StringBuilder);
        }
        [WriteConfined]
        public StringBuilder Insert (int index, UInt32 value) {

          CodeContract.Ensures(CodeContract.Result<StringBuilder>() != null);
          return default(StringBuilder);
        }
        [WriteConfined]
        public StringBuilder Insert (int index, UInt16 value) {

          CodeContract.Ensures(CodeContract.Result<StringBuilder>() != null);
          return default(StringBuilder);
        }
        [WriteConfined]
        public StringBuilder Insert (int index, Decimal value) {

          CodeContract.Ensures(CodeContract.Result<StringBuilder>() != null);
          return default(StringBuilder);
        }
        [WriteConfined]
        public StringBuilder Insert (int index, double value) {

          CodeContract.Ensures(CodeContract.Result<StringBuilder>() != null);
          return default(StringBuilder);
        }
        [WriteConfined]
        public StringBuilder Insert (int index, Single value) {

          CodeContract.Ensures(CodeContract.Result<StringBuilder>() != null);
          return default(StringBuilder);
        }
        [WriteConfined]
        public StringBuilder Insert (int index, Int64 value) {

          CodeContract.Ensures(CodeContract.Result<StringBuilder>() != null);
          return default(StringBuilder);
        }
        [WriteConfined]
        public StringBuilder Insert (int index, int value) {

          CodeContract.Ensures(CodeContract.Result<StringBuilder>() != null);
          return default(StringBuilder);
        }
        [WriteConfined]
        public StringBuilder Insert (int arg0, Char[] arg1, int arg2, int arg3) {

          CodeContract.Ensures(CodeContract.Result<StringBuilder>() != null);
          return default(StringBuilder);
        }
        [WriteConfined]
        public StringBuilder Insert (int index, Char[] value) {

          CodeContract.Ensures(CodeContract.Result<StringBuilder>() != null);
          return default(StringBuilder);
        }
        [WriteConfined]
        public StringBuilder Insert (int index, Char value) {

          CodeContract.Ensures(CodeContract.Result<StringBuilder>() != null);
          return default(StringBuilder);
        }
        [WriteConfined]
        public StringBuilder Insert (int index, Int16 value) {

          CodeContract.Ensures(CodeContract.Result<StringBuilder>() != null);
          return default(StringBuilder);
        }
        [WriteConfined]
        public StringBuilder Insert (int index, byte value) {

          CodeContract.Ensures(CodeContract.Result<StringBuilder>() != null);
          return default(StringBuilder);
        }
        [WriteConfined]
        public StringBuilder Insert (int index, SByte value) {

          CodeContract.Ensures(CodeContract.Result<StringBuilder>() != null);
          return default(StringBuilder);
        }
        [WriteConfined]
        public StringBuilder Insert (int index, bool value) {

          CodeContract.Ensures(CodeContract.Result<StringBuilder>() != null);
          return default(StringBuilder);
        }
        [WriteConfined]
        public StringBuilder Insert (int index, string value) {

          CodeContract.Ensures(CodeContract.Result<StringBuilder>() != null);
          return default(StringBuilder);
        }
        [WriteConfined]
        public StringBuilder Append (Char[] value) {

          CodeContract.Ensures(CodeContract.Result<StringBuilder>() != null);
          return default(StringBuilder);
        }
        [WriteConfined]
        public StringBuilder Append (object value) {

          CodeContract.Ensures(CodeContract.Result<StringBuilder>() != null);
          return default(StringBuilder);
        }
        [WriteConfined]
        public StringBuilder Append (UInt64 value) {

          CodeContract.Ensures(CodeContract.Result<StringBuilder>() != null);
          return default(StringBuilder);
        }
        [WriteConfined]
        public StringBuilder Append (UInt32 value) {

          CodeContract.Ensures(CodeContract.Result<StringBuilder>() != null);
          return default(StringBuilder);
        }
        [WriteConfined]
        public StringBuilder Append (UInt16 value) {

          CodeContract.Ensures(CodeContract.Result<StringBuilder>() != null);
          return default(StringBuilder);
        }
        [WriteConfined]
        public StringBuilder Append (Decimal value) {

          CodeContract.Ensures(CodeContract.Result<StringBuilder>() != null);
          return default(StringBuilder);
        }
        [WriteConfined]
        public StringBuilder Append (double value) {

          CodeContract.Ensures(CodeContract.Result<StringBuilder>() != null);
          return default(StringBuilder);
        }
        [WriteConfined]
        public StringBuilder Append (Single value) {

          CodeContract.Ensures(CodeContract.Result<StringBuilder>() != null);
          return default(StringBuilder);
        }
        [WriteConfined]
        public StringBuilder Append (Int64 value) {

          CodeContract.Ensures(CodeContract.Result<StringBuilder>() != null);
          return default(StringBuilder);
        }
        [WriteConfined]
        public StringBuilder Append (int value) {

          CodeContract.Ensures(CodeContract.Result<StringBuilder>() != null);
          return default(StringBuilder);
        }
        [WriteConfined]
        public StringBuilder Append (Int16 value) {

          CodeContract.Ensures(CodeContract.Result<StringBuilder>() != null);
          return default(StringBuilder);
        }
        [WriteConfined]
        public StringBuilder Append (Char value) {

          CodeContract.Ensures(CodeContract.Result<StringBuilder>() != null);
          return default(StringBuilder);
        }
        [WriteConfined]
        public StringBuilder Append (byte value) {

          CodeContract.Ensures(CodeContract.Result<StringBuilder>() != null);
          return default(StringBuilder);
        }
        [WriteConfined]
        public StringBuilder Append (SByte value) {

          CodeContract.Ensures(CodeContract.Result<StringBuilder>() != null);
          return default(StringBuilder);
        }
        [WriteConfined]
        public StringBuilder Append (bool value) {

          CodeContract.Ensures(CodeContract.Result<StringBuilder>() != null);
          return default(StringBuilder);
        }
        [WriteConfined]
        public StringBuilder Remove (int startIndex, int length) {
            CodeContract.Requires(0 <= startIndex);
            CodeContract.Requires(0 <= length);
            CodeContract.Requires(startIndex + length <= Length);
            modifies this.*;
            CodeContract.Ensures(result == this);
            CodeContract.Ensures(Length == old(Length) - length);

          CodeContract.Ensures(CodeContract.Result<StringBuilder>() != null);
          return default(StringBuilder);
        }
        [WriteConfined]
        public StringBuilder Insert (int index, string value, int count) {
            CodeContract.Requires(value != null || (index == 0 && count == 0));
            CodeContract.Requires(0 <= index && index < Length);
            CodeContract.Requires(1 <= count);
            modifies this.*;
            CodeContract.Ensures(result == this);
            CodeContract.Ensures(Length == old(Length) + count * value.Length);

          CodeContract.Ensures(CodeContract.Result<StringBuilder>() != null);
          return default(StringBuilder);
        }
        [WriteConfined]
        public StringBuilder Append (string! value, int startIndex, int count) {
            CodeContract.Requires(value != null || (startIndex == 0 && count == 0));
            CodeContract.Requires(0 <= count);
            CodeContract.Requires(0 <= startIndex);
            CodeContract.Requires(startIndex <= (value.Length - count));
            modifies this.*;
            CodeContract.Ensures(result == this);
            CodeContract.Ensures(Length == old(Length) + count);

          CodeContract.Ensures(CodeContract.Result<StringBuilder>() != null);
          return default(StringBuilder);
        }
        [WriteConfined]
        public StringBuilder Append (string value) {
            modifies this.*;
            CodeContract.Ensures(result == this);
            CodeContract.Ensures(value == null ==> Length == old(Length));
            CodeContract.Ensures(value != null ==> Length == old(Length) + value.Length);

          CodeContract.Ensures(CodeContract.Result<StringBuilder>() != null);
          return default(StringBuilder);
        }
        [WriteConfined]
        public StringBuilder Append (Char[]! value, int startIndex, int charCount) {
            CodeContract.Requires(startIndex >= 0);
            CodeContract.Requires(charCount >= 0);
            CodeContract.Requires(charCount <= (value.Length - startIndex));

          CodeContract.Ensures(CodeContract.Result<StringBuilder>() != null);
          return default(StringBuilder);
        }
        [WriteConfined]
        public StringBuilder Append (Char value, int repeatCount) {
            CodeContract.Requires(repeatCount == 0 || repeatCount >= 0);

          CodeContract.Ensures(CodeContract.Result<StringBuilder>() != null);
          return default(StringBuilder);
        }
        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public string ToString (int startIndex, int length) {
            CodeContract.Requires(0 <= startIndex);
            CodeContract.Requires(0 <= length);
            CodeContract.Requires(startIndex + length <= Length);
            CodeContract.Ensures(result.Length == length);

          CodeContract.Ensures(CodeContract.Result<string>() != null);
          return default(string);
        }
        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public string ToString () {
            CodeContract.Ensures(result.Length == this.Length);

          CodeContract.Ensures(CodeContract.Result<string>() != null);
          return default(string);
        }
        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public int EnsureCapacity (int capacity) {
            CodeContract.Requires(capacity >= 0);

          return default(int);
        }
        public StringBuilder (int capacity, int maxCapacity) {
            CodeContract.Requires(capacity <= maxCapacity);
            CodeContract.Requires(maxCapacity >= 1);
            CodeContract.Requires(capacity >= 0);
            CodeContract.Ensures(Length == 0);

          return default(StringBuilder);
        }
        public StringBuilder (string value, int startIndex, int length, int capacity) {
            CodeContract.Requires(capacity >= 0);
            CodeContract.Requires(length >= 0);

          return default(StringBuilder);
        }
        public StringBuilder (string value, int capacity) {
            CodeContract.Requires(capacity >= 0);
            CodeContract.Ensures(value == null ==> Length == 0);
            CodeContract.Ensures(value != null ==> Length == value.Length);

          return default(StringBuilder);
        }
        public StringBuilder (string value) {
            CodeContract.Ensures(value == null ==> Length == 0);
            CodeContract.Ensures(value != null ==> Length == value.Length);

          return default(StringBuilder);
        }
        public StringBuilder (int capacity) {
            CodeContract.Requires(capacity >= 0);
            CodeContract.Ensures(Length == 0);

          return default(StringBuilder);
        }
        public StringBuilder () {
            CodeContract.Ensures(Length == 0);
          return default(StringBuilder);
        }
    }
}
