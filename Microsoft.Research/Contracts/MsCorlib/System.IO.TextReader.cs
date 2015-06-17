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

namespace System.IO
{

  public class TextReader
  {
    protected TextReader() { }

#if !SILVERLIGHT
    public static TextReader Synchronized(TextReader reader)
    {
      Contract.Requires(reader != null);

      return default(TextReader);
    }
#endif

    public virtual string ReadLine()
    {
      return default(string);
    }

    public virtual int ReadBlock(char[] buffer, int index, int count)
    {
        Contract.Requires(buffer != null);
        Contract.Requires(index >= 0);
        Contract.Requires(count >= 0);
        Contract.Requires((buffer.Length - index) >= count);
        Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurs.");
        return default(int);
    }

    public virtual string ReadToEnd()
    {
      Contract.Ensures(Contract.Result<string>() != null);
      Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurs.");

      return default(string);
    }

    public virtual int Read(char[] buffer, int index, int count)
    {
      Contract.Requires(buffer != null);
      Contract.Requires(index >= 0);
      Contract.Requires(count >= 0);
      Contract.Requires((buffer.Length - index) >= count);
      Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurs.");

      return default(int);
    }

    public virtual int Read()
    {
        Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurs.");
        return default(int);
    }

    [Pure]
    extern public virtual int Peek();

    extern public virtual void Close();

  }
}
