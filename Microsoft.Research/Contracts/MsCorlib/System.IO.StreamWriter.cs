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

    public class StreamWriter : TextWriter
    {
        public virtual extern bool AutoFlush { get; set; }

        public virtual Stream BaseStream
        {
            get
            {
                Contract.Ensures(Contract.Result<Stream>() != null);
                return default(Stream);
            }
        }

#if false
        public System.Text.Encoding Encoding
        {
          get;
        }
    public void Write(string value)
    {

    }
#endif
#if false

    public void Write(char[] buffer, int index, int count)
    {
      Contract.Requires(buffer != null);
      Contract.Requires(index >= 0);
      Contract.Requires(count >= 0);
      Contract.Requires(index + count <= buffer.Length);

    }
    public void Write(char[] buffer)
    {

    }
    public void Write(char value)
    {

    }
    public void Flush()
    {

    }
    public void Close()
    {

    }
    public StreamWriter(string path, bool append, System.Text.Encoding encoding, int bufferSize)
    {
      Contract.Requires(path != null);
      Contract.Requires(encoding != null);
      Contract.Requires(bufferSize > 0);

      return default(StreamWriter);
    }
    public StreamWriter(string path, bool append, System.Text.Encoding encoding)
    {
      Contract.Requires(path != null);
      Contract.Requires(encoding != null);

      return default(StreamWriter);
    }
    public StreamWriter(string path, bool append)
    {
      Contract.Requires(path != null);

      return default(StreamWriter);
    }
    public StreamWriter(string path)
    {
      Contract.Requires(path != null);

      return default(StreamWriter);
    }
    public StreamWriter(Stream stream, System.Text.Encoding encoding, int bufferSize)
    {
      Contract.Requires(stream != null);
      Contract.Requires(encoding != null);
      Contract.Requires(bufferSize > 0);

      return default(StreamWriter);
    }
    public StreamWriter(Stream stream, System.Text.Encoding encoding)
    {
      Contract.Requires(stream != null);
      Contract.Requires(encoding != null);
    }
#endif

        public StreamWriter(Stream stream)
        {
            Contract.Requires(stream != null);
        }

        public StreamWriter(string path)
        {
            Contract.Requires(!String.IsNullOrEmpty(path));
            Contract.EnsuresOnThrow<System.IO.IOException>(true,@"path includes an incorrect or invalid syntax for file name, directory name, or volume label.");
            Contract.EnsuresOnThrow<System.IO.DirectoryNotFoundException>(true,@"The specified path is invalid (for example, it is on an unmapped drive.");
            Contract.EnsuresOnThrow<System.IO.FileNotFoundException>(true, @"The file cannot be found.");
        }

        public StreamWriter(Stream stream, Encoding encoding)
        {
            Contract.Requires(stream != null);
            Contract.Requires(encoding != null);
        }

        public StreamWriter(string path, bool append)
        {
            Contract.Requires(!String.IsNullOrEmpty(path));
            Contract.EnsuresOnThrow<System.IO.IOException>(true,
                                                           @"path includes an incorrect or invalid syntax for file name, directory name, or volume label.");
            Contract.EnsuresOnThrow<System.IO.DirectoryNotFoundException>(true,@"The specified path is invalid (for example, it is on an unmapped drive.");
            Contract.EnsuresOnThrow<System.IO.PathTooLongException>(true,@"The specified path, file name, or both exceed the system-defined maximum length.");
        }

        public StreamWriter(Stream stream, Encoding encoding, int bufferSize)
        {
            Contract.Requires(stream != null);
            Contract.Requires(encoding != null);
            Contract.Requires(bufferSize >= 0);
        }

        public StreamWriter(string path, bool append, Encoding encoding)
        {
            Contract.Requires(!String.IsNullOrEmpty(path));
            Contract.EnsuresOnThrow<System.IO.IOException>(true,@"path includes an incorrect or invalid syntax for file name, directory name, or volume label.");
            Contract.EnsuresOnThrow<System.IO.DirectoryNotFoundException>(true,@"The specified path is invalid (for example, it is on an unmapped drive.");
            Contract.EnsuresOnThrow<System.IO.PathTooLongException>(true,@"The specified path, file name, or both exceed the system-defined maximum length.");
        }

        public StreamWriter(string path, bool append, Encoding encoding, int bufferSize)
        {
            Contract.Requires(!String.IsNullOrEmpty(path));
            Contract.Requires(encoding != null);
            Contract.Requires(bufferSize >= 0);
            Contract.EnsuresOnThrow<System.IO.IOException>(true, @"path includes an incorrect or invalid syntax for file name, directory name, or volume label.");
            Contract.EnsuresOnThrow<System.IO.DirectoryNotFoundException>(true, @"The specified path is invalid (for example, it is on an unmapped drive.");
            Contract.EnsuresOnThrow<System.IO.PathTooLongException>(true, @"The specified path, file name, or both exceed the system-defined maximum length.");
        }

        public override void Write(char value)
        {
            Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurs.");
        }

        public override void Write(char[] buffer)
        {
            Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurs.");
        }

        public override void Write(string value)
        {
            Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurs.");
        }

        public override void Write(char[] buffer, int index, int count)
        {
            Contract.Requires(buffer != null);
            Contract.Requires(index >= 0);
            Contract.Requires(count >= 0);
            Contract.Requires((buffer.Length - index) >= count);
            Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurs.");
        }

#if false
        public StreamWriter(Stream stream, Encoding encoding, int bufferSize, bool leaveOpen)
        {
            Contract.Requires(stream != null);
            Contract.Requires(encoding != null);
            Contract.Requires(bufferSize >= 0);
        }


#endif
    }
}
