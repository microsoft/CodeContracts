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

#if !SILVERLIGHT

using System.Diagnostics.Contracts;
using System;

namespace System.Diagnostics
{

  public class TraceListener
  {
    protected TraceListener() { }

    extern public int IndentLevel
    {
      get;
      set;
    }

    extern virtual public string Name
    {
      get;
      set;
    }

    public int IndentSize
    {
      get
      {
        return default(int);
      }
      set
      {
        Contract.Requires(value >= 0);
      }
    }

#if false
        public void WriteLine (object o, string category) {

        }
        public void WriteLine (string message, string category) {

        }
        public void WriteLine (object o) {

        }
        public void WriteLine (string arg0) {

        }
        public void Write (object o, string category) {

        }
        public void Write (string message, string category) {

        }
        public void Write (object o) {

        }
        public void Write (string arg0) {

        }
        public void Fail (string message, string detailMessage) {

        }
        public void Fail (string message) {

        }
        public void Flush () {

        }
        public void Close () {

        }
        public void Dispose () {
        }
#endif
  }
}

#endif