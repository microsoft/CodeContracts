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

  public class FileStream: Stream
  {
    internal FileStream() { }

    public override bool CanRead {
        get {return default(bool);}
    }

    public override bool CanWrite {
        get {return default(bool);}
    }

    public override void WriteByte(byte value) {

    }

    public override Int64 Seek(Int64 offset, SeekOrigin origin) {
        Contract.Requires((int)origin >= 0);
        Contract.Requires((int)origin <= 2);
        Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurred.");

        return default(Int64);
    }

#if false
    public virtual SafeFileHandle! SafeFileHandle { get; }

    public bool CanSeek
    {
      get;
    }



    public int Handle
    {
      get;
    }

    public bool IsAsync
    {
      get;
    }

    public Int64 Length
    {
      get;
    }

    public Int64 Position
    {
      get;
      set
      {
        Contract.Requires(value >= 0);
      }
    }

    public string Name
    {
      get;
    }

    public void Unlock(Int64 position, Int64 length)
    {
      Contract.Requires(position >= 0);
      Contract.Requires(length >= 0);

    }
    public void Lock(Int64 position, Int64 length)
    {
      Contract.Requires(position >= 0);
      Contract.Requires(length >= 0);

    }

    public void EndWrite(IAsyncResult asyncResult)
    {
      Contract.Requires(asyncResult != null);

    }
    public IAsyncResult BeginWrite(Byte[] array, int offset, int numBytes, AsyncCallback userCallback, object stateObject)
    {
      Contract.Requires(array != null);
      Contract.Requires(offset >= 0);
      Contract.Requires(numBytes >= 0);
      Contract.Requires((array.Length - offset) >= numBytes);

      return default(IAsyncResult);
    }
    public int ReadByte()
    {

      return default(int);
    }
    public int EndRead(IAsyncResult asyncResult)
    {
      Contract.Requires(asyncResult != null);

      return default(int);
    }
    public IAsyncResult BeginRead(Byte[] array, int offset, int numBytes, AsyncCallback userCallback, object stateObject)
    {
      Contract.Requires(array != null);
      Contract.Requires(offset >= 0);
      Contract.Requires(numBytes >= 0);
      Contract.Requires((array.Length - offset) >= numBytes);

      return default(IAsyncResult);
    }
    public void Write(Byte[] array, int offset, int count)
    {
      Contract.Requires(array != null);
      Contract.Requires(offset >= 0);
      Contract.Requires(count >= 0);
      Contract.Requires((array.Length - offset) >= count);

    }

    public int Read(Byte[] array, int offset, int count)
    {
      Contract.Requires(array != null);
      Contract.Requires(offset >= 0);
      Contract.Requires(count >= 0);
      Contract.Requires((array.Length - offset) >= count);

      return default(int);
    }
    public void SetLength(Int64 value)
    {
      Contract.Requires(value >= 0);

    }
    public void Flush()
    {

    }
    public void Close()
    {

    }

         public FileStream(int handle, FileAccess access, bool ownsHandle, int bufferSize, bool isAsync)
    {
      Contract.Requires((int)access >= 1);
      Contract.Requires((int)access <= 3);
      Contract.Requires(bufferSize > 0);
#if NETFRAMEWORK_4_0 
      Contract.Ensures(this.CanRead == access.HasFlag(FileAccess.Read));
      Contract.Ensures(this.CanWrite == access.HasFlag(FileAccess.Write));
#endif

    }

    public FileStream(int handle, FileAccess access, bool ownsHandle, int bufferSize) {
      Contract.Requires(bufferSize > 0);
#if NETFRAMEWORK_4_0
      Contract.Ensures(this.CanRead == access.HasFlag(FileAccess.Read));
      Contract.Ensures(this.CanWrite == access.HasFlag(FileAccess.Write));
#endif
    }

    public FileStream(int handle, FileAccess access, bool ownsHandle) {
#if NETFRAMEWORK_4_0
      Contract.Ensures(this.CanRead == access.HasFlag(FileAccess.Read));
      Contract.Ensures(this.CanWrite == access.HasFlag(FileAccess.Write));
#endif
    }

    public FileStream(int handle, FileAccess access) {
#if NETFRAMEWORK_4_0 
      Contract.Ensures(this.CanRead == access.HasFlag(FileAccess.Read));
      Contract.Ensures(this.CanWrite == access.HasFlag(FileAccess.Write));
#endif

    }
#endif

#if !SILVERLIGHT
    public FileStream(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize, bool useAsync) {
      Contract.Requires(!String.IsNullOrEmpty(path));
      Contract.Requires(bufferSize > 0);
#if NETFRAMEWORK_4_0 
      Contract.Ensures(this.CanRead == access.HasFlag(FileAccess.Read));
      Contract.Ensures(this.CanWrite == access.HasFlag(FileAccess.Write));
#endif

      Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error, such as a disk error, occurred. -or- The stream has been closed.");
    }
#endif

    public FileStream(string path, FileMode mode, FileAccess access, FileShare share, int bufferSize) {
      Contract.Requires(!String.IsNullOrEmpty(path));
      Contract.Requires(bufferSize > 0);
#if NETFRAMEWORK_4_0 || SILVERLIGHT_4_0 || SILVERLIGHT_5_0
      Contract.Ensures(this.CanRead == access.HasFlag(FileAccess.Read));
      Contract.Ensures(this.CanWrite == access.HasFlag(FileAccess.Write));
#endif

      Contract.EnsuresOnThrow<System.IO.PathTooLongException>(true, @"The specified path, file name, or both exceed the system-defined maximum length.");
      Contract.EnsuresOnThrow<System.IO.DirectoryNotFoundException>(true, @"The specified path is invalid, (for example, it is on an unmapped drive).");
      Contract.EnsuresOnThrow<System.IO.FileNotFoundException>(true, @"The file cannot be found, such as when mode is FileMode.Truncate or FileMode.Open, and the file specified by path does not exist. The file must already exist in these modes.");
      Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error, such as specifying FileMode.CreateNew when the file specified by path already exists, occurred. -or- The stream has been closed.");
    }

    public FileStream(string path, FileMode mode, FileAccess access, FileShare share) {
      Contract.Requires(!String.IsNullOrEmpty(path));
        
#if NETFRAMEWORK_4_0 || SILVERLIGHT_4_0 || SILVERLIGHT_5_0
      Contract.Ensures(this.CanRead == access.HasFlag(FileAccess.Read));
      Contract.Ensures(this.CanWrite == access.HasFlag(FileAccess.Write));
#endif

      Contract.EnsuresOnThrow<System.IO.PathTooLongException>(true, @"The specified path, file name, or both exceed the system-defined maximum length.");
      Contract.EnsuresOnThrow<System.IO.DirectoryNotFoundException>(true, @"The specified path is invalid, (for example, it is on an unmapped drive).");
      Contract.EnsuresOnThrow<System.IO.FileNotFoundException>(true, @"The file cannot be found, such as when mode is FileMode.Truncate or FileMode.Open, and the file specified by path does not exist. The file must already exist in these modes.");
      Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error, such as specifying FileMode.CreateNew when the file specified by path already exists, occurred. -or- The stream has been closed.");
    }

    public FileStream(string path, FileMode mode, FileAccess access) {
      Contract.Requires(!String.IsNullOrEmpty(path));
#if NETFRAMEWORK_4_0 || SILVERLIGHT_4_0 || SILVERLIGHT_5_0
      Contract.Ensures(this.CanRead == access.HasFlag(FileAccess.Read));
      Contract.Ensures(this.CanWrite == access.HasFlag(FileAccess.Write));
#endif
      Contract.EnsuresOnThrow<System.IO.PathTooLongException>(true, @"The specified path, file name, or both exceed the system-defined maximum length.");
      Contract.EnsuresOnThrow<System.IO.DirectoryNotFoundException>(true, @"The specified path is invalid, (for example, it is on an unmapped drive).");
      Contract.EnsuresOnThrow<System.IO.FileNotFoundException>(true, @"The file cannot be found, such as when mode is FileMode.Truncate or FileMode.Open, and the file specified by path does not exist. The file must already exist in these modes.");
      Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error, such as specifying FileMode.CreateNew when the file specified by path already exists, occurred. -or- The stream has been closed.");

    }

   public FileStream(string path, FileMode mode) {
     Contract.Requires(!String.IsNullOrEmpty(path));
     Contract.EnsuresOnThrow<System.IO.PathTooLongException>(true, @"The specified path, file name, or both exceed the system-defined maximum length.");
     Contract.EnsuresOnThrow<System.IO.DirectoryNotFoundException>(true, @"The specified path is invalid, (for example, it is on an unmapped drive).");
     Contract.EnsuresOnThrow<System.IO.FileNotFoundException>(true, @"The file cannot be found, such as when mode is FileMode.Truncate or FileMode.Open, and the file specified by path does not exist. The file must already exist in these modes.");
     Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error, such as specifying FileMode.CreateNew when the file specified by path already exists, occurred. -or- The stream has been closed.");
   }

  }
}
