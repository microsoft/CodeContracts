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
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Tests
{
  public class Output : Microsoft.Research.DataStructures.IVerySimpleLineWriterWithEncoding, Microsoft.Research.DataStructures.ISimpleLineWriterWithEncoding
  {
    private readonly string name;
    private readonly TextWriter textWriter;

    // Do not use a static ConsoleOutput because the Visual Studio test environment
    // uses a different Console for each test case

    public static readonly Output Ignore = new Output("Ignore");

    public static Output ConsoleOutputFor(string name)
    {
      return new Output(String.Format("Console::{0}", name), Console.Out);
    }

    private Output(string name)
    {
      this.name = name;
    }

    public Output(string name, TextWriter textWriter)
      : this(name)
    {
      this.textWriter = textWriter;
    }

    public void WriteLine(string value)
    {
      if (this.textWriter == null)
        return;
      try
      {
        this.textWriter.WriteLine(value);
      }
      catch (Exception e)
      {
        //Console.WriteLine(value);
        Console.WriteLine("[{0}] '{1}' writing '{2}'", name, e.Message, value);
      }
    }

    public void WriteLine(string value, params object[] arg)
    {
      if (this.textWriter == null)
        return;
      try
      {
        this.textWriter.WriteLine(value ?? "", arg);
      }
      catch (Exception e)
      {
        //Console.WriteLine(value ?? "", arg);
        Console.WriteLine("[{0}] '{1}' writing '{2}'", name, e.Message, String.Format(value ?? "", arg));
      }
    }

    public Encoding Encoding
    {
      get
      {
        return this.textWriter == null ? Encoding.Default : this.textWriter.Encoding;
      }
    }

    public void OutputDataReceivedEventHandler(Object sender, DataReceivedEventArgs e)
    {
      this.WriteLine(e.Data);
    }
    public void ErrDataReceivedEventHandler(Object sender, DataReceivedEventArgs e)
    {
      this.WriteLine(e.Data);
    }

    public void Dispose()
    {
      // does nothing
    }
  }
}
