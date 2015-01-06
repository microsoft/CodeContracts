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

// File System.IO.TextWriter.cs
// Automatically generated contract file.
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Diagnostics.Contracts;
using System;

// Disable the "this variable is not used" warning as every field would imply it.
#pragma warning disable 0414
// Disable the "this variable is never assigned to".
#pragma warning disable 0067
// Disable the "this event is never assigned to".
#pragma warning disable 0649
// Disable the "this variable is never used".
#pragma warning disable 0169
// Disable the "new keyword not required" warning.
#pragma warning disable 0109
// Disable the "extern without DllImport" warning.
#pragma warning disable 0626
// Disable the "could hide other member" warning, can happen on certain properties.
#pragma warning disable 0108


namespace System.IO
{
  abstract public partial class TextWriter : MarshalByRefObject, IDisposable
  {
    #region Methods and constructors
    public virtual new void Close()
    {
    }

    public void Dispose()
    {
    }

    protected virtual new void Dispose(bool disposing)
    {
    }

    public virtual new void Flush()
    {
    }

    public static System.IO.TextWriter Synchronized(System.IO.TextWriter writer)
    {
      Contract.Ensures(Contract.Result<System.IO.TextWriter>() != null);

      return default(System.IO.TextWriter);
    }

    protected TextWriter()
    {
      Contract.Ensures(this.CoreNewLine != null);
      Contract.Ensures(this.CoreNewLine.Length == 2);
    }

    protected TextWriter(IFormatProvider formatProvider)
    {
      Contract.Ensures(this.CoreNewLine != null);
      Contract.Ensures(this.CoreNewLine.Length == 2);
    }

    public virtual new void Write(string format, Object arg0, Object arg1, Object arg2)
    {
    }

    public virtual new void Write(bool value)
    {
    }

    public virtual new void Write(string format, Object arg0)
    {
    }

    public virtual new void Write(string format, Object arg0, Object arg1)
    {
    }

    public virtual new void Write(char[] buffer)
    {
    }

    public virtual new void Write(char value)
    {
    }

    public virtual new void Write(string format, Object[] arg)
    {
    }

    public virtual new void Write(char[] buffer, int index, int count)
    {
    }

    public virtual new void Write(uint value)
    {
    }

    public virtual new void Write(int value)
    {
    }

    public virtual new void Write(long value)
    {
    }

    public virtual new void Write(ulong value)
    {
    }

    public virtual new void Write(float value)
    {
    }

    public virtual new void Write(Decimal value)
    {
    }

    public virtual new void Write(Object value)
    {
    }

    public virtual new void Write(string value)
    {
    }

    public virtual new void Write(double value)
    {
    }

    public virtual new void WriteLine(string format, Object arg0)
    {
    }

    public virtual new void WriteLine(Object value)
    {
    }

    public virtual new void WriteLine(string value)
    {
    }

    public virtual new void WriteLine(string format, Object arg0, Object arg1)
    {
    }

    public virtual new void WriteLine(bool value)
    {
    }

    public virtual new void WriteLine(string format, Object[] arg)
    {
    }

    public virtual new void WriteLine(string format, Object arg0, Object arg1, Object arg2)
    {
    }

    public virtual new void WriteLine(char[] buffer, int index, int count)
    {
    }

    public virtual new void WriteLine(int value)
    {
    }

    public virtual new void WriteLine(char[] buffer)
    {
    }

    public virtual new void WriteLine()
    {
    }

    public virtual new void WriteLine(char value)
    {
    }

    public virtual new void WriteLine(uint value)
    {
    }

    public virtual new void WriteLine(double value)
    {
    }

    public virtual new void WriteLine(Decimal value)
    {
    }

    public virtual new void WriteLine(float value)
    {
    }

    public virtual new void WriteLine(long value)
    {
    }

    public virtual new void WriteLine(ulong value)
    {
    }
    #endregion

    #region Properties and indexers
    public abstract Encoding Encoding
    {
      get;
    }

    public virtual new IFormatProvider FormatProvider
    {
      get
      {
        return default(IFormatProvider);
      }
    }

    public virtual new string NewLine
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }
    #endregion

    #region Fields
    protected char[] CoreNewLine;
    public readonly static System.IO.TextWriter Null;
    #endregion
  }
}
