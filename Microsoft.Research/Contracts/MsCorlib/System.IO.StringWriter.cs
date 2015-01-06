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
using System.Text;

namespace System.IO
{

    public class StringWriter : System.IO.TextWriter
  {
    public override void Write(string value)
    {

    }
    public override void Write(Char[] buffer, int index, int count)
    {
      Contract.Requires(buffer != null);
      Contract.Requires(index >= 0);
      Contract.Requires(count >= 0);
      Contract.Requires((buffer.Length - index) >= count);

    }
    public override void Write(Char value)
    {

    }
    public virtual System.Text.StringBuilder GetStringBuilder()
    {
      Contract.Ensures(Contract.Result<System.Text.StringBuilder>() != null);
    
      return default(System.Text.StringBuilder);
    }

    public override void Close()
    {
    }

    public StringWriter(System.Text.StringBuilder sb, IFormatProvider formatProvider)
    {
      Contract.Requires(sb != null);

    }

    public StringWriter(System.Text.StringBuilder sb)
    {
      Contract.Requires(sb != null);
    }
    
    public StringWriter(IFormatProvider formatProvider)
    {
    }

    public StringWriter()
    {
      
    }
  }
}