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

    public class CodeCommentStatementCollection
    {

        public CodeCommentStatement this [int index]
        {
          get;
          set;
        }

        public void Remove (CodeCommentStatement value) {

        }
        public void Insert (int index, CodeCommentStatement value) {

        }
        public int IndexOf (CodeCommentStatement value) {

          return default(int);
        }
        public void CopyTo (CodeCommentStatement[] array, int index) {

        }
        public bool Contains (CodeCommentStatement value) {

          return default(bool);
        }
        public void AddRange (CodeCommentStatementCollection! value) {
            Contract.Requires(value != null);

        }
        public void AddRange (CodeCommentStatement[]! value) {
            Contract.Requires(value != null);

        }
        public int Add (CodeCommentStatement value) {

          return default(int);
        }
        public CodeCommentStatementCollection (CodeCommentStatement[] value) {

          return default(CodeCommentStatementCollection);
        }
        public CodeCommentStatementCollection (CodeCommentStatementCollection value) {

          return default(CodeCommentStatementCollection);
        }
        public CodeCommentStatementCollection () {
          return default(CodeCommentStatementCollection);
        }
    }
}
