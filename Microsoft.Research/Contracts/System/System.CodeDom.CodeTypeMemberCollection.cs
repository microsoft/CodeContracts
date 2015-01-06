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

namespace System.CodeDom
{

    public class CodeTypeMemberCollection
    {

        public CodeTypeMember this [int index]
        {
          get;
          set;
        }

        public void Remove (CodeTypeMember value) {

        }
        public void Insert (int index, CodeTypeMember value) {

        }
        public int IndexOf (CodeTypeMember value) {

          return default(int);
        }
        public void CopyTo (CodeTypeMember[] array, int index) {

        }
        public bool Contains (CodeTypeMember value) {

          return default(bool);
        }
        public void AddRange (CodeTypeMemberCollection! value) {
            Contract.Requires(value != null);

        }
        public void AddRange (CodeTypeMember[]! value) {
            Contract.Requires(value != null);

        }
        public int Add (CodeTypeMember value) {

          return default(int);
        }
        public CodeTypeMemberCollection (CodeTypeMember[] value) {

          return default(CodeTypeMemberCollection);
        }
        public CodeTypeMemberCollection (CodeTypeMemberCollection value) {

          return default(CodeTypeMemberCollection);
        }
        public CodeTypeMemberCollection () {
          return default(CodeTypeMemberCollection);
        }
    }
}
