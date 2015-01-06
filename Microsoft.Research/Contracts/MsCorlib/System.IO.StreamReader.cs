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

  public class StreamReader : TextReader
  {
    internal StreamReader() { }

    public StreamReader(Stream stream)
    {
        Contract.Requires(stream != null);
    }

    public StreamReader(Stream stream, bool detectEncodingFromByteOrderMarks)
    {
        Contract.Requires(stream != null);
    }

    public StreamReader(Stream stream, Encoding encoding)
    {
        Contract.Requires(stream != null);
        Contract.Requires(encoding != null);
    }

    public StreamReader(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks)
    {
        Contract.Requires(stream != null);
        Contract.Requires(encoding != null);
    }

    public StreamReader(string path)
    {
        Contract.Requires(!String.IsNullOrEmpty(path));
        Contract.EnsuresOnThrow<System.IO.IOException>(true, @"path includes an incorrect or invalid syntax for file name, directory name, or volume label.");
        Contract.EnsuresOnThrow<System.IO.DirectoryNotFoundException>(true, @"The specified path is invalid (for example, it is on an unmapped drive.");
        Contract.EnsuresOnThrow<System.IO.FileNotFoundException>(true, @"The file cannot be found.");
    }

    public StreamReader(string path, bool detectEncodingFromByteOrderMarks)
    {
        Contract.Requires(!String.IsNullOrEmpty(path));
        Contract.EnsuresOnThrow<System.IO.IOException>(true, @"path includes an incorrect or invalid syntax for file name, directory name, or volume label.");
        Contract.EnsuresOnThrow<System.IO.DirectoryNotFoundException>(true, @"The specified path is invalid (for example, it is on an unmapped drive.");
        Contract.EnsuresOnThrow<System.IO.FileNotFoundException>(true, @"The file cannot be found.");
    }

    public StreamReader(string path, Encoding encoding)
    {
        Contract.Requires(!String.IsNullOrEmpty(path));
        Contract.Requires(encoding != null);
        Contract.EnsuresOnThrow<System.IO.DirectoryNotFoundException>(true, @"The specified path is invalid (for example, it is on an unmapped drive.");
        Contract.EnsuresOnThrow<System.IO.FileNotFoundException>(true, @"The file cannot be found.");
    }

    public StreamReader(string path, Encoding encoding, bool detectEncodingFromByteOrderMarks)
    {
        Contract.Requires(!String.IsNullOrEmpty(path));
        Contract.Requires(encoding != null);
        Contract.EnsuresOnThrow<System.IO.DirectoryNotFoundException>(true, @"The specified path is invalid (for example, it is on an unmapped drive.");
        Contract.EnsuresOnThrow<System.IO.FileNotFoundException>(true, @"The file cannot be found.");
    }

    public StreamReader(string path, Encoding encoding, bool detectEncodingFromByteOrderMarks, int bufferSize)
    {
        Contract.Requires(!String.IsNullOrEmpty(path));
        Contract.Requires(encoding != null);
        Contract.Requires(bufferSize > 0);
        Contract.EnsuresOnThrow<System.IO.DirectoryNotFoundException>(true, @"The specified path is invalid (for example, it is on an unmapped drive.");
        Contract.EnsuresOnThrow<System.IO.FileNotFoundException>(true, @"The file cannot be found.");
    }

    public override int Read()
    {
        Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurred.");
        return default(int);
    }

    public override string ReadLine()
    {
        Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurred.");
        return default(string);
    }
    public virtual Encoding CurrentEncoding
    {
      get
      {
        Contract.Ensures(Contract.Result<Encoding>() != null);
        return default(Encoding);
      }
    }

    public virtual Stream BaseStream
    {
      get
      {
        Contract.Ensures(Contract.Result<Stream>() != null);
        return default(Stream);
      }
    }
#if false
    public string ReadLine()
    {

      Contract.Ensures(Contract.Result<string>() != null);
      return default(string);
    }
    public string ReadToEnd()
    {

      Contract.Ensures(Contract.Result<string>() != null);
      return default(string);
    }
    public int Read(char[] buffer, int index, int count)
    {
      Contract.Requires(buffer != null);
      Contract.Requires(index >= 0);
      Contract.Requires(count >= 0);
      Contract.Requires(index + count <= buffer.Length);

      return default(int);
    }
    public int Read()
    {

      return default(int);
    }
    public int Peek()
    {

      return default(int);
    }
    public void DiscardBufferedData()
    {

    }
    public void Close()
    {

    }

    public StreamReader(string path, System.Text.Encoding encoding, bool detectEncodingFromByteOrderMarks, int bufferSize)
    {
      Contract.Requires(path != null);
      Contract.Requires(encoding != null);
      Contract.Requires(path.Length != 0);
      Contract.Requires(bufferSize > 0);
    }
    public StreamReader(string path, System.Text.Encoding encoding, bool detectEncodingFromByteOrderMarks)
    {
      Contract.Requires(path != null);
      Contract.Requires(encoding != null);
      Contract.Requires(path.Length != 0);
    }
    public StreamReader(string path, System.Text.Encoding encoding)
    {
      Contract.Requires(path != null);
      Contract.Requires(encoding != null);
      Contract.Requires(path.Length != 0);
    }
    public StreamReader(string path, bool detectEncodingFromByteOrderMarks)
    {
      Contract.Requires(path != null);
      Contract.Requires(path.Length != 0);
    }
    public StreamReader(string path)
    {
      Contract.Requires(path != null);
      Contract.Requires(path.Length != 0);
    }
    public StreamReader(Stream stream, System.Text.Encoding encoding, bool detectEncodingFromByteOrderMarks, int bufferSize)
    {
      Contract.Requires(stream != null);
      Contract.Requires(encoding != null);
      Contract.Requires(bufferSize > 0);
    }
    public StreamReader(Stream stream, System.Text.Encoding encoding, bool detectEncodingFromByteOrderMarks)
    {
      Contract.Requires(stream != null);
      Contract.Requires(encoding != null);
    }
    public StreamReader(Stream stream, System.Text.Encoding encoding)
    {
      Contract.Requires(stream != null);
      Contract.Requires(encoding != null);
    }
    public StreamReader(Stream stream, bool detectEncodingFromByteOrderMarks)
    {
      Contract.Requires(stream != null);
    }
    public StreamReader(Stream stream)
    {
      Contract.Requires(stream != null);
    }
#endif
  }
}
