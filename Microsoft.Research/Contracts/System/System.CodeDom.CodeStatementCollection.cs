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

    public class CodeStatementCollection
    {

        public CodeStatement this [int index]
        {
          get;
          set;
        }

        public void Remove (CodeStatement value) {

        }
        public void Insert (int index, CodeStatement value) {

        }
        public int IndexOf (CodeStatement value) {

          return default(int);
        }
        public void CopyTo (CodeStatement[] array, int index) {

        }
        public bool Contains (CodeStatement value) {

          return default(bool);
        }
        public void AddRange (CodeStatementCollection! value) {
            Contract.Requires(value != null);

        }
        public void AddRange (CodeStatement[]! value) {
            Contract.Requires(value != null);

        }
        public int Add (CodeExpression value) {

          return default(int);
        }
        public int Add (CodeStatement value) {

          return default(int);
        }
        public CodeStatementCollection (CodeStatement[] value) {

          return default(CodeStatementCollection);
        }
        public CodeStatementCollection (CodeStatementCollection value) {

          return default(CodeStatementCollection);
        }
        public CodeStatementCollection () {
          return default(CodeStatementCollection);
        }
    }
}
