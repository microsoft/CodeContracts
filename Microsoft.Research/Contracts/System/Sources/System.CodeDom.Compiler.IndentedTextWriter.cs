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

// File System.CodeDom.Compiler.IndentedTextWriter.cs
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


namespace System.CodeDom.Compiler
{
  public partial class IndentedTextWriter : TextWriter
  {
    #region Methods and constructors
    public override void Close()
    {
    }

    public override void Flush()
    {
    }

    public IndentedTextWriter(TextWriter writer)
    {
    }

    public IndentedTextWriter(TextWriter writer, string tabString)
    {
    }

    protected virtual new void OutputTabs()
    {
    }

    public override void Write(string format, Object arg0, Object arg1)
    {
    }

    public override void Write(string format, Object arg0)
    {
    }

    public override void Write(string s)
    {
    }

    public override void Write(string format, Object[] arg)
    {
    }

    public override void Write(Object value)
    {
    }

    public override void Write(char[] buffer)
    {
    }

    public override void Write(char[] buffer, int index, int count)
    {
    }

    public override void Write(bool value)
    {
    }

    public override void Write(char value)
    {
    }

    public override void Write(double value)
    {
    }

    public override void Write(long value)
    {
    }

    public override void Write(float value)
    {
    }

    public override void Write(int value)
    {
    }

    public override void WriteLine(string format, Object arg0, Object arg1)
    {
    }

    public override void WriteLine(string format, Object arg0)
    {
    }

    public override void WriteLine(Object value)
    {
    }

    public override void WriteLine(string s)
    {
    }

    public override void WriteLine(uint value)
    {
    }

    public override void WriteLine(string format, Object[] arg)
    {
    }

    public override void WriteLine(char value)
    {
    }

    public override void WriteLine(char[] buffer)
    {
    }

    public override void WriteLine()
    {
    }

    public override void WriteLine(bool value)
    {
    }

    public override void WriteLine(char[] buffer, int index, int count)
    {
    }

    public override void WriteLine(int value)
    {
    }

    public override void WriteLine(long value)
    {
    }

    public override void WriteLine(double value)
    {
    }

    public override void WriteLine(float value)
    {
    }

    public void WriteLineNoTabs(string s)
    {
    }
    #endregion

    #region Properties and indexers
    public override Encoding Encoding
    {
      get
      {
        return default(Encoding);
      }
    }

    public int Indent
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public TextWriter InnerWriter
    {
      get
      {
        return default(TextWriter);
      }
    }

    public override string NewLine
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
    public static string DefaultTabString;
    #endregion
  }
}
