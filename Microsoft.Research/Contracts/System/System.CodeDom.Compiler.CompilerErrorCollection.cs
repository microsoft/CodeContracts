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

namespace System.CodeDom.Compiler
{

    public class CompilerErrorCollection
    {

        public CompilerError this [int index]
        {
          get;
          set;
        }

        public bool HasErrors
        {
          get;
        }

        public bool HasWarnings
        {
          get;
        }

        public void Remove (CompilerError value) {

        }
        public void Insert (int index, CompilerError value) {

        }
        public int IndexOf (CompilerError value) {

          return default(int);
        }
        public void CopyTo (CompilerError[] array, int index) {

        }
        public bool Contains (CompilerError value) {

          return default(bool);
        }
        public void AddRange (CompilerErrorCollection! value) {
            Contract.Requires(value != null);

        }
        public void AddRange (CompilerError[]! value) {
            Contract.Requires(value != null);

        }
        public int Add (CompilerError value) {

          return default(int);
        }
        public CompilerErrorCollection (CompilerError[] value) {

          return default(CompilerErrorCollection);
        }
        public CompilerErrorCollection (CompilerErrorCollection value) {

          return default(CompilerErrorCollection);
        }
        public CompilerErrorCollection () {
          return default(CompilerErrorCollection);
        }
    }
}
