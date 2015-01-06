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
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;

namespace Microsoft.Research.Cloudot.Common
{
  public class TextWriterAsync : TextWriter
  {

    private readonly TextWriter inner;

    public TextWriterAsync(TextWriter inner)
    {
      Contract.Requires(inner != null);

      this.inner = inner;
    }

    public override void Write(bool value)
    {
      this.inner.WriteAsync(value.ToString());
    }

    public override void Write(char value)
    {
      this.inner.WriteAsync(value);
    }

    public override void Write(char[] buffer)
    {
      this.inner.WriteAsync(buffer);
    }

    public override void Write(char[] buffer, int index, int count)
    {
      this.inner.WriteAsync(buffer, index, count);
    }

    public override void Write(decimal value)
    {
      this.inner.WriteAsync(value.ToString());
    }

    public override void Write(double value)
    {
      this.inner.WriteAsync(value.ToString());
    }

    public override void Write(float value)
    {
      this.inner.WriteAsync(value.ToString());
    }

    public override void Write(int value)
    {
      this.inner.WriteAsync(value.ToString());
    }

    public override void Write(long value)
    {
      this.inner.WriteAsync(value.ToString());
    }

    public override void Write(object value)
    {
      this.inner.WriteAsync(value.ToString());
    }

    public override void Write(string format, object arg0)
    {
      var str = string.Format(format, arg0);
      this.inner.WriteAsync(str);
    }

    public override void Write(string format, object arg0, object arg1)
    {
      var str = string.Format(format, arg0, arg1);
      this.inner.WriteAsync(str);
    }

    public override void Write(string format, object arg0, object arg1, object arg2)
    {
      var str = string.Format(format, arg0, arg1, arg2);
      this.inner.WriteAsync(str);
    }

    public override void Write(string format, params object[] arg)
    {
      var str = string.Format(format, arg);
      this.inner.WriteAsync(str);
    }

    public override void Write(string value)
    {
      this.inner.WriteAsync(value);
    }

    public override void Write(uint value)
    {
      this.inner.WriteAsync(value.ToString());
    }

    public override void Write(ulong value)
    {
      this.inner.WriteAsync(value.ToString());
    }

    public override void WriteLine()
    {
      this.inner.WriteLineAsync();
    }

    public override void WriteLine(bool value)
    {
      this.inner.WriteLineAsync(value.ToString());
    }

    public override void WriteLine(char value)
    {
      this.inner.WriteLineAsync(value);
    }

    public override void WriteLine(char[] buffer)
    {
      this.inner.WriteLineAsync(buffer);
    }

    public override void WriteLine(char[] buffer, int index, int count)
    {
      this.inner.WriteLineAsync(buffer, index, count);
    }

    public override void WriteLine(decimal value)
    {
      this.inner.WriteLineAsync(value.ToString());
    }

    public override void WriteLine(float value)
    {
      this.inner.WriteLineAsync(value.ToString());
    }

    public override void WriteLine(double value)
    {
      this.inner.WriteLineAsync(value.ToString());
    }

    public override void WriteLine(int value)
    {
      this.inner.WriteLineAsync(value.ToString());
    }

    public override void WriteLine(long value)
    {
      this.inner.WriteLineAsync(value.ToString());
    }


    public override void WriteLine(object value)
    {
      this.inner.WriteLineAsync(value.ToString());
    }

    public override void WriteLine(string format, object arg0)
    {
      this.inner.WriteLineAsync(string.Format(format, arg0));
    }

    public override void WriteLine(string format, object arg0, object arg1)
    {
      this.inner.WriteLineAsync(string.Format(format, arg0, arg1));
    }

    public override void WriteLine(string format, object arg0, object arg1, object arg2)
    {
      this.inner.WriteLineAsync(string.Format(format, arg0, arg1, arg2));
    }

    public override void WriteLine(string format, params object[] arg)
    {
      this.inner.WriteLineAsync(string.Format(format, arg));
    }

    public override void WriteLine(string value)
    {
      this.inner.WriteLineAsync(value);
    }

    public override void WriteLine(uint value)
    {
      this.inner.WriteLineAsync(value.ToString());
    }

    public override void WriteLine(ulong value)
    {
      this.inner.WriteLineAsync(value.ToString());
    }

    public override void Close()
    {
      this.inner.Close();
    }

    protected override void Dispose(bool disposing)
    {
      base.Dispose(disposing);
    }

    public override void Flush()
    {
      this.inner.Flush();
    }

    public override System.Threading.Tasks.Task FlushAsync()
    {
      return this.inner.FlushAsync();
    }

    public override IFormatProvider FormatProvider
    {
      get
      {
        return this.inner.FormatProvider;
      }
    }

    public override object InitializeLifetimeService()
    {
      return this.inner.InitializeLifetimeService();
    }


    public override string NewLine
    {
      get
      {
        return this.inner.NewLine;
      }
      set
      {
        this.inner.NewLine = value;
      }
    }

    public override string ToString()
    {
      return this.inner.ToString();
    }

    public override System.Text.Encoding Encoding
    {
      get { return inner.Encoding; }
    }
  }
}
