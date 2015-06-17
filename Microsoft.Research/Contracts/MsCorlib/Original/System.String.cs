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
    [Immutable]
    public class String
    {
        [System.Runtime.CompilerServices.IndexerName("Char")]
        public char this [int index]
        {
          get
                  CodeContract.Requires(0 <= index && index < this.Length);
        }

        public int Length
        {
          [Pure][Reads(ReadsAttribute.Reads.Nothing)]
          get
                  CodeContract.Ensures(result >= 0);
        }

        [Pure] [GlobalAccess(false)] [Escapes(true,false)]
        public CharEnumerator GetEnumerator () {
          CodeContract.Ensures(result.IsNew);

          CodeContract.Ensures(CodeContract.Result<CharEnumerator>() != null);
          return default(CharEnumerator);
        }
        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public TypeCode GetTypeCode () {

          return default(TypeCode);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static String IsInterned (String! str) {
            CodeContract.Requires(str != null);
            CodeContract.Ensures(result != null ==> result.Length == str.Length);
            CodeContract.Ensures(result != null ==> str.Equals(result)); 


          return default(String);
        }
        public static String Intern (String! str) {
            CodeContract.Requires(str != null);
            CodeContract.Ensures(result.Length == str.Length);

          return default(String);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static String Concat (String[]! values) {
            CodeContract.Requires(values != null);
            //CodeContract.Ensures(result.Length == Sum({ String s in values); s.Length }));

          CodeContract.Ensures(CodeContract.Result<String>() != null);
          return default(String);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static String Concat (String str0, String str1, String str2, String str3) {
            CodeContract.Ensures(result.Length == 
                (str0 == null ? 0 : str0.Length) +
                (str1 == null ? 0 : str1.Length) +
                (str2 == null ? 0 : str2.Length) +
                (str3 == null ? 0 : str3.Length);

          CodeContract.Ensures(CodeContract.Result<String>() != null);
          return default(String);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static String Concat (String str0, String str1, String str2) {
            CodeContract.Ensures(result.Length == 
                (str0 == null ? 0 : str0.Length) +
                (str1 == null ? 0 : str1.Length) +
                (str2 == null ? 0 : str2.Length);

          CodeContract.Ensures(CodeContract.Result<String>() != null);
          return default(String);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static String Concat (String str0, String str1) {
            CodeContract.Ensures(result.Length == 
                (str0 == null ? 0 : str0.Length) +
                (str1 == null ? 0 : str1.Length);

          CodeContract.Ensures(CodeContract.Result<String>() != null);
          return default(String);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static String Concat (object[]! args) {
            CodeContract.Requires(args != null);

          CodeContract.Ensures(CodeContract.Result<String>() != null);
          return default(String);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static String Concat (object arg0, object arg1, object arg2, object arg3) {

          CodeContract.Ensures(CodeContract.Result<String>() != null);
          return default(String);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static String Concat (object arg0, object arg1, object arg2) {

          CodeContract.Ensures(CodeContract.Result<String>() != null);
          return default(String);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static String Concat (object arg0, object arg1) {

          CodeContract.Ensures(CodeContract.Result<String>() != null);
          return default(String);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static String Concat (object arg0) {

          CodeContract.Ensures(CodeContract.Result<String>() != null);
          return default(String);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static String Copy (String! str) {
            CodeContract.Requires(str != null);

          return default(String);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static String Format (IFormatProvider provider, String format, object[]! args) {
            CodeContract.Requires(format != null);
            CodeContract.Requires(args != null);

          CodeContract.Ensures(CodeContract.Result<String>() != null);
          return default(String);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static String Format (String format, object[] args) {

          CodeContract.Ensures(CodeContract.Result<String>() != null);
          return default(String);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static String Format (String format, object arg0, object arg1, object arg2) {

          CodeContract.Ensures(CodeContract.Result<String>() != null);
          return default(String);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static String Format (String format, object arg0, object arg1) {

          CodeContract.Ensures(CodeContract.Result<String>() != null);
          return default(String);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static String Format (String format, object arg0) {

          CodeContract.Ensures(CodeContract.Result<String>() != null);
          return default(String);
        }
        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public String Remove (int index, int count) {
            CodeContract.Requires(0 <= index);
            CodeContract.Requires(index + count <= Length);
            CodeContract.Ensures(result.Length == this.Length - count);

          CodeContract.Ensures(CodeContract.Result<String>() != null);
          return default(String);
        }
        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public String Replace (String oldValue, String newValue) {
            CodeContract.Requires(oldValue != null);
            CodeContract.Requires(oldValue.Length > 0);

          CodeContract.Ensures(CodeContract.Result<String>() != null);
          return default(String);
        }
        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public String Replace (char oldChar, char newChar) {

          CodeContract.Ensures(CodeContract.Result<String>() != null);
          return default(String);
        }
        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public String Insert (int startIndex, String value) {
            CodeContract.Requires(value != null);
            CodeContract.Requires(0 <= startIndex);
            CodeContract.Requires(startIndex <= this.Length);
            CodeContract.Ensures(result.Length == this.Length + value.Length);

          CodeContract.Ensures(CodeContract.Result<String>() != null);
          return default(String);
        }
        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public String Trim () {

          CodeContract.Ensures(CodeContract.Result<String>() != null);
          return default(String);
        }
        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public String ToUpper (System.Globalization.CultureInfo! culture) {
            CodeContract.Requires(culture != null);
            CodeContract.Ensures(result.Length == this.Length); // Are there languages for which this isn't true?!?

          CodeContract.Ensures(CodeContract.Result<String>() != null);
          return default(String);
        }
        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public String ToUpper () {
            CodeContract.Ensures(result.Length == this.Length);

          CodeContract.Ensures(CodeContract.Result<String>() != null);
          return default(String);
        }
        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public String ToLower (System.Globalization.CultureInfo! culture) {
            CodeContract.Requires(culture != null);
            CodeContract.Ensures(result.Length == this.Length);

          CodeContract.Ensures(CodeContract.Result<String>() != null);
          return default(String);
        }
        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public String ToLower () {
            CodeContract.Ensures(result.Length == this.Length);
            
          CodeContract.Ensures(CodeContract.Result<String>() != null);
          return default(String);
        }
        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public String ToLowerInvariant() {
            CodeContract.Ensures(result.Length == this.Length);

          CodeContract.Ensures(CodeContract.Result<String>() != null);
          return default(String);
        }
        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public bool StartsWith (String! value) {
            CodeContract.Requires(value != null);
            CodeContract.Ensures(result ==> value.Length <= this.Length);

          return default(bool);
        }
        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public String PadRight (int totalWidth, char paddingChar) {
            CodeContract.Requires(totalWidth >= 0);
            CodeContract.Ensures(result.Length == totalWidth);

          CodeContract.Ensures(CodeContract.Result<String>() != null);
          return default(String);
        }
        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public String PadRight (int totalWidth) {
            CodeContract.Requires(totalWidth >= 0);
            CodeContract.Ensures(result.Length == totalWidth);

          CodeContract.Ensures(CodeContract.Result<String>() != null);
          return default(String);
        }
        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public String PadLeft (int totalWidth, char paddingChar) {
            CodeContract.Requires(totalWidth >= 0);
            CodeContract.Ensures(result.Length == totalWidth);

          CodeContract.Ensures(CodeContract.Result<String>() != null);
          return default(String);
        }
        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public String PadLeft (int totalWidth) {
            CodeContract.Requires(totalWidth >= 0);
            CodeContract.Ensures(result.Length == totalWidth);

          CodeContract.Ensures(CodeContract.Result<String>() != null);
          return default(String);
        }
        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public int LastIndexOf (String! value, int startIndex, int count) {
            CodeContract.Requires(value != null);
            CodeContract.Requires(startIndex >= 0);
            CodeContract.Requires(count >= 0);
            CodeContract.Requires(startIndex + count <= Length);
            CodeContract.Ensures(-1 <= result && result < this.Length);

          return default(int);
        }
        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public int LastIndexOf (String! value, int startIndex) {
            CodeContract.Requires(value != null);
            CodeContract.Requires(startIndex >= 0);
            CodeContract.Requires(startIndex <= Length);
            CodeContract.Ensures(-1 <= result && result < this.Length);

          return default(int);
        }
        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public int LastIndexOf (String! value) {
            CodeContract.Requires(value != null);
            CodeContract.Ensures(-1 <= result && result < this.Length);

          return default(int);
        }
        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public int LastIndexOfAny (char[]! anyOf, int startIndex, int count) {
            CodeContract.Requires(anyOf != null);
            CodeContract.Requires(startIndex >= 0);
            CodeContract.Requires(count >= 0);
            CodeContract.Requires(startIndex + count <= Length);
            CodeContract.Ensures(-1 <= result && result < this.Length);

          return default(int);
        }
        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public int LastIndexOfAny (char[]! anyOf, int startIndex) {
            CodeContract.Requires(anyOf != null);
            CodeContract.Requires(startIndex >= 0);
            CodeContract.Requires(startIndex <= Length);
            CodeContract.Ensures(-1 <= result && result < this.Length);

          return default(int);
        }
        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public int LastIndexOfAny (char[]! anyOf) {
            CodeContract.Requires(anyOf != null);
            CodeContract.Ensures(-1 <= result && result < this.Length);

          return default(int);
        }
        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public int LastIndexOf (char arg0, int startIndex, int count) {
            CodeContract.Requires(startIndex >= 0);
            CodeContract.Requires(count >= 0);
            CodeContract.Requires(startIndex + count <= Length);
            CodeContract.Ensures(-1 <= result && result < this.Length);

          return default(int);
        }
        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public int LastIndexOf (char value, int startIndex) {
            CodeContract.Requires(startIndex >= 0);
            CodeContract.Requires(startIndex <= Length);
            CodeContract.Ensures(-1 <= result && result < this.Length);

          return default(int);
        }
        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public int LastIndexOf (char value) {
            CodeContract.Ensures(-1 <= result && result < this.Length);

          return default(int);
        }
        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public int IndexOf (String! value, int startIndex, int count) {
            CodeContract.Requires(value != null);
            CodeContract.Requires(startIndex >= 0);
            CodeContract.Requires(count >= 0);
            CodeContract.Requires(startIndex + count <= Length);
            CodeContract.Ensures(-1 <= result && result < this.Length);

          return default(int);
        }
        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public int IndexOf (String! value, int startIndex) {
            CodeContract.Requires(value != null);
            CodeContract.Requires(startIndex >= 0);
            CodeContract.Requires(startIndex <= Length);
            CodeContract.Ensures(-1 <= result && result < this.Length);

          return default(int);
        }
        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public int IndexOf (String! value) {
            CodeContract.Requires(value != null);
            CodeContract.Ensures(-1 <= result && result < this.Length);

          return default(int);
        }
        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public int IndexOfAny (char[]! anyOf, int startIndex, int count) {
            CodeContract.Requires(anyOf != null);
            CodeContract.Requires(startIndex >= 0);
            CodeContract.Requires(count >= 0);
            CodeContract.Requires(startIndex + count <= Length);
            CodeContract.Ensures(-1 <= result && result < this.Length);

          return default(int);
        }
        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public int IndexOfAny (char[]! anyOf, int startIndex) {
            CodeContract.Requires(anyOf != null);
            CodeContract.Requires(startIndex >= 0);
            CodeContract.Requires(startIndex <= Length);
            CodeContract.Ensures(-1 <= result && result < this.Length);

          return default(int);
        }
        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public int IndexOfAny (char[]! anyOf) {
            CodeContract.Requires(anyOf != null);
            CodeContract.Ensures(-1 <= result && result < this.Length);

          return default(int);
        }
        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public int IndexOf (char arg0, int startIndex, int count) {
            CodeContract.Requires(startIndex >= 0);
            CodeContract.Requires(count >= 0);
            CodeContract.Requires(startIndex + count <= Length);
            CodeContract.Ensures(-1 <= result && result < this.Length);

          return default(int);
        }
        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public int IndexOf (char value, int startIndex) {
            CodeContract.Requires(startIndex >= 0);
            CodeContract.Requires(startIndex <= Length);
            CodeContract.Ensures(-1 <= result && result < this.Length);

          return default(int);
        }
        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public int IndexOf (char value) {
            CodeContract.Ensures(-1 <= result && result < this.Length);

          return default(int);
        }
        public static readonly string/*!*/ Empty = "";
        
        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public bool EndsWith (String! value) {
            CodeContract.Requires(value != null);

          return default(bool);
        }
        public static int CompareOrdinal (String strA, int indexA, String strB, int indexB, int length) {

          return default(int);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static int CompareOrdinal (String strA, String strB) {

          return default(int);
        }
        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public int CompareTo (String strB) {

          return default(int);
        }
        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public int CompareTo (object value) {

          return default(int);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static int Compare (String strA, int indexA, String strB, int indexB, int length, bool ignoreCase, System.Globalization.CultureInfo! culture) {
            CodeContract.Requires(culture != null);

          return default(int);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static int Compare (String strA, int indexA, String strB, int indexB, int length, bool ignoreCase) {

          return default(int);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static int Compare(string strA, string strB, StringComparison comparisonType) {
        
          return default(int);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static int Compare(string strA, int indexA, string strB, int indexB, int length, StringComparison comparisonType) {
        
          return default(int);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static int Compare (String strA, int indexA, String strB, int indexB, int length) {

          return default(int);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static int Compare (String strA, String strB, bool ignoreCase, System.Globalization.CultureInfo! culture) {
            CodeContract.Requires(culture != null);

          return default(int);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static int Compare (String strA, String strB, bool ignoreCase) {

          return default(int);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static int Compare (String strA, String strB) {

          return default(int);
        }
        public String (char c, int count) {
            CodeContract.Ensures(this.Length == count);

          return default(String);
        }
        public String (char[] array) // maybe null
            CodeContract.Ensures(array == null ==> this.Length == 0);
            CodeContract.Ensures(array != null ==> this.Length == array.Length);

        public String (char[]! value, int startIndex, int length) {
            CodeContract.Requires(value != null);
            CodeContract.Requires(startIndex >= 0);
            CodeContract.Requires(length >= 0);
            CodeContract.Requires(startIndex + length <= value.Length);
            CodeContract.Ensures(this.Length == length);

/* These should all be pointer arguments

          return default(String);
        }
        public String (ref SByte arg0, int arg1, int arg2, System.Text.Encoding arg3) {

          return default(String);
        }
        public String (ref SByte arg0, int arg1, int arg2) {

          return default(String);
        }
        public String (ref SByte arg0) {

          return default(String);
        }
        public String (ref char arg0, int arg1, int arg2) {

          return default(String);
        }
        public String (ref char arg0) {
 */

          return default(String);
        }
        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public String TrimEnd (params char[] trimChars) { // maybe null

          CodeContract.Ensures(CodeContract.Result<String>() != null);
          return default(String);
        }
        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public String TrimStart (params char[] trimChars) { // maybe null

          CodeContract.Ensures(CodeContract.Result<String>() != null);
          return default(String);
        }
        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public String Trim (params char[] trimChars) { // maybe null

          CodeContract.Ensures(CodeContract.Result<String>() != null);
          return default(String);
        }
        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public String Substring (int startIndex, int length) {
            CodeContract.Requires(0 <= startIndex);
            CodeContract.Requires(0 <= length);
            CodeContract.Requires(startIndex + length <= this.Length);
            CodeContract.Ensures(result.Length == length);

          CodeContract.Ensures(CodeContract.Result<String>() != null);
          return default(String);
        }
        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public String Substring (int startIndex) {
            CodeContract.Requires(0 <= startIndex);
            CodeContract.Requires(startIndex <= this.Length);
            CodeContract.Ensures(result.Length == this.Length - startIndex);

          CodeContract.Ensures(CodeContract.Result<String>() != null);
          return default(String);
        }
        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public String![] Split (char[] arg0, int arg1) {
            //CodeContract.Ensures(Forall {int i in (0:result.Length)); result[i] != null});

          CodeContract.Ensures(CodeContract.Result<String![]>() != null);
          return default(String![]);
        }
        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public String![] Split (char[] separator) {
            //CodeContract.Ensures(Forall {int i in (0:result.Length)); result[i] != null});

          CodeContract.Ensures(CodeContract.Result<String![]>() != null);
          return default(String![]);
        }
        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public string![] Split(char[] separator, StringSplitOptions options) {
        
          CodeContract.Ensures(CodeContract.Result<string![]>() != null);
          return default(string![]);
        }
        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public string![] Split(string[] separator, StringSplitOptions options) {
        
          CodeContract.Ensures(CodeContract.Result<string![]>() != null);
          return default(string![]);
        }
        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public string![] Split(char[] separator, int count, StringSplitOptions options) {
        
          CodeContract.Ensures(CodeContract.Result<string![]>() != null);
          return default(string![]);
        }
        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public string![] Split(string[] separator, int count, StringSplitOptions options) {
      
          CodeContract.Ensures(CodeContract.Result<string![]>() != null);
          return default(string![]);
        }
        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public char[] ToCharArray (int startIndex, int length) {
            CodeContract.Requires(startIndex >= 0);
            CodeContract.Requires(startIndex <= this.Length);
            CodeContract.Requires(startIndex + length <= this.Length);
            CodeContract.Requires(length >= 0);

          CodeContract.Ensures(CodeContract.Result<char[]>() != null);
          return default(char[]);
        }
        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public char[] ToCharArray () {

          CodeContract.Ensures(CodeContract.Result<char[]>() != null);
          return default(char[]);
        }
        public void CopyTo (int sourceIndex, char[]! destination, int destinationIndex, int count) {
            CodeContract.Requires(destination != null);
            CodeContract.Requires(count >= 0);
            CodeContract.Requires(sourceIndex >= 0);
            CodeContract.Requires(count <= (this.Length - sourceIndex));
            CodeContract.Requires(destinationIndex <= (destination.Length - count));
            CodeContract.Requires(destinationIndex >= 0);

        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static bool operator != (String a, String b) {

          return default(bool);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static bool operator == (String a, String b) {

          return default(bool);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static bool Equals (String a, String b) {
          CodeContract.Ensures(a != null && (object)a == (object)b ==> result); 

          return default(bool);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)][RecursionTermination(10)]
        public bool Equals (String arg0) {
          CodeContract.Ensures((object)this == (object)arg0 ==> result);
          CodeContract.Ensures(result ==> this.Length == arg0.Length); 

          return default(bool);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)]
        public static String Join (String separator, String[]! value, int startIndex, int count) {
            CodeContract.Requires(value != null);
            CodeContract.Requires(startIndex >= 0);
            CodeContract.Requires(count >= 0);
            CodeContract.Requires(startIndex + count <= value.Length);

          return default(String);
        }
        [Pure][Reads(ReadsAttribute.Reads.Nothing)] 
        public static String Join (String separator, String[]! value) {
            CodeContract.Requires(value != null);
          return default(String);
        }
    }
}
