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

namespace System.Security
{

    public class SecurityElement
    {

        public System.Collections.Hashtable Attributes
        {
          get;
          set;
        }

        public System.Collections.ArrayList Children
        {
          get;
          set;
        }

        public string! Tag
        {
          get;
          set
            CodeContract.Requires(value != null);
        }

        public string Text
        {
          get;
          set;
        }

        public string SearchForTextOfTag (string! tag) {
            CodeContract.Requires(tag != null);

          return default(string);
        }
        public SecurityElement SearchForChildByTag (string! tag) {
            CodeContract.Requires(tag != null);

          return default(SecurityElement);
        }
        public string Attribute (string! name) {
            CodeContract.Requires(name != null);

          return default(string);
        }
        [Pure][Reads(ReadsAttribute.Reads.Owned)]
        public string ToString () {

          return default(string);
        }
        public static string Escape (string str) {

          return default(string);
        }
        public static bool IsValidAttributeValue (string value) {

          return default(bool);
        }
        public static bool IsValidAttributeName (string name) {

          return default(bool);
        }
        public static bool IsValidText (string text) {

          return default(bool);
        }
        public static bool IsValidTag (string tag) {

          return default(bool);
        }
        public bool Equal (SecurityElement other) {

          return default(bool);
        }
        public void AddChild (SecurityElement! child) {
            CodeContract.Requires(child != null);

        }
        public void AddAttribute (string! name, string! value) {
            CodeContract.Requires(name != null);
            CodeContract.Requires(value != null);

        }
        public SecurityElement (string! tag, string text) {
            CodeContract.Requires(tag != null);

          return default(SecurityElement);
        }
        public SecurityElement (string! tag) {
            CodeContract.Requires(tag != null);
          return default(SecurityElement);
        }
    }
}
