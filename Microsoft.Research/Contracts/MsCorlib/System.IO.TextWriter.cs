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
using System.Text;

namespace System.IO
{

  public class TextWriter
  {
    protected TextWriter() { }

    public virtual IFormatProvider FormatProvider
    {
      get
      {
        Contract.Ensures(Contract.Result<IFormatProvider>() != null);
        return default(IFormatProvider);
      }
    }

    public virtual string NewLine
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
      set
      {
        Contract.Requires(value != null);
      }
    }

    public virtual System.Text.Encoding Encoding
    {
      get
      {
        Contract.Ensures(Contract.Result<Encoding>() != null);
        return default(Encoding);
      }
    }

    public virtual void WriteLine()
    {
        Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurs.");
    }
    public virtual void WriteLine(bool value)
    {
        Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurs.");
    }
    public virtual void WriteLine(char value)
    {
        Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurs.");
    }
    public virtual void WriteLine(string format, params object[] args)
    {
      Contract.Requires(format != null);
      Contract.Requires(args != null);
      Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurs.");
    }

    public virtual  void WriteLine(string format, object arg0, object arg1)
    {
      Contract.Requires(format != null);
      Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurs.");
    }
    public virtual void WriteLine(string format, object arg0)
    {
      Contract.Requires(format != null);
      Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurs.");
    }
    public virtual void WriteLine(object value)
    {
        Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurs.");
    }
    public virtual void WriteLine(string value)
    {
        Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurs.");
    }
    public virtual void WriteLine(Decimal value)
    {
        Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurs.");
    }
    public virtual void WriteLine(double value)
    {
        Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurs.");
    }
    public virtual void WriteLine(float value)
    {
        Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurs.");
    }
    public virtual void WriteLine(ulong value)
    {
        Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurs.");
    }
    public virtual void WriteLine(long value)
    {
        Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurs.");
    }
    public virtual void WriteLine(uint value)
    {
        Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurs.");
    }
    public virtual void WriteLine(int value)
    {
        Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurs.");
    }
    public virtual void WriteLine(char[] buffer, int index, int count)
    {
        Contract.Requires(buffer != null);
        Contract.Requires(index >= 0);
        Contract.Requires(count >= 0);
        Contract.Requires((buffer.Length - index) >= count);
        Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurs.");
    }
    public virtual void WriteLine(char[] buffer)
    {
        Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurs.");
    }
    
      public virtual void Write(string format, params object[] args)
    {
      Contract.Requires(format != null);
      Contract.Requires(args != null);
      Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurs.");
    }
    public virtual void Write(string format, object arg0, object arg1)
    {
      Contract.Requires(format != null);
      Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurs.");
    }
    public virtual void Write(string format, object arg0)
    {
      Contract.Requires(format != null);
      Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurs.");
    }
    public virtual void Write(object value)
    {
        Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurs.");
    }
    public virtual void Write(string value)
    {
        Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurs.");
    }
    public virtual void Write(Decimal value)
    {
        Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurs.");
    }
    public virtual void Write(double value)
    {
        Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurs.");
    }
    public virtual void Write(float value)
    {
        Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurs.");
    }
    public virtual void Write(ulong value)
    {
        Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurs.");
    }
    public virtual void Write(long value)
    {
        Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurs.");
    }
    public virtual void Write(uint value)
    {
        Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurs.");
    }
    public virtual void Write(int value)
    {
        Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurs.");
    }
    public virtual void Write(bool value)
    {
        Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurs.");
    }
    public virtual void Write(char[] buffer, int index, int count)
    {
      Contract.Requires(buffer != null);
      Contract.Requires(index >= 0);
      Contract.Requires(count >= 0);
      Contract.Requires((buffer.Length - index) >= count);
      Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurs.");
    }
    public virtual void Write(char[] buffer)
    {
        Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurs.");
    }
    public virtual void Write(char value)
    {
        Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurs.");
    }
    public virtual void Flush()
    {

    }
    public virtual void Close()
    {
    }

#if false
    public static TextWriter Synchronized(TextWriter writer)
    {
      Contract.Requires(writer != null);

      return default(TextWriter);
    }
    public virtual void Write(string format, object arg0, object arg1, object arg2)
    {
      Contract.Requires(format != null);

    }
    public virtual  void WriteLine(string format, object arg0, object arg1, object arg2)
    {
      Contract.Requires(format != null);

    }
#endif
  }
}
