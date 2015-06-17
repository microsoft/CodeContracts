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
using System.Runtime.InteropServices;
using System.Diagnostics.Contracts;

namespace System.IO
{
  // Summary:
  //     Creates a stream whose backing store is memory.
  public class MemoryStream : Stream
  {
    public MemoryStream()
    {
      Contract.Ensures(this.CanSeek);
      Contract.Ensures(this.CanWrite);
      Contract.Ensures(this.CanRead);
    }

    // Summary:
    //     Initializes a new instance of the System.IO.MemoryStream class with an expandable
    //     capacity initialized to zero.
    //public MemoryStream();
    //
    // Summary:
    //     Initializes a new non-resizable instance of the System.IO.MemoryStream class
    //     based on the specified byte array.
    //
    // Parameters:
    //   buffer:
    //     The array of unsigned bytes from which to create the current stream.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     buffer is null.
    public MemoryStream(byte[] buffer)
    {
      Contract.Requires(buffer != null);

      Contract.Ensures(this.CanSeek);
      Contract.Ensures(this.CanWrite);
      Contract.Ensures(this.CanRead);
      Contract.Ensures(this.Capacity == buffer.Length);
      Contract.Ensures(this.Length == buffer.Length);
    }
    //
    // Summary:
    //     Initializes a new instance of the System.IO.MemoryStream class with an expandable
    //     capacity initialized as specified.
    //
    // Parameters:
    //   capacity:
    //     The initial size of the internal array in bytes.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     capacity is negative.
    public MemoryStream(int capacity)
    {
      Contract.Requires(capacity >= 0);

      Contract.Ensures(this.CanSeek);
      Contract.Ensures(this.CanWrite);
      Contract.Ensures(this.CanRead);

    }
    //
    // Summary:
    //     Initializes a new non-resizable instance of the System.IO.MemoryStream class
    //     based on the specified byte array with the System.IO.MemoryStream.CanWrite
    //     property set as specified.
    //
    // Parameters:
    //   buffer:
    //     The array of unsigned bytes from which to create this stream.
    //
    //   writable:
    //     The setting of the System.IO.MemoryStream.CanWrite property, which determines
    //     whether the stream supports writing.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     buffer is null.
    public MemoryStream(byte[] buffer, bool writable)
    {
      Contract.Requires(buffer != null);

      Contract.Ensures(this.CanSeek);
      Contract.Ensures(this.CanWrite == writable);
      Contract.Ensures(this.CanRead);
      Contract.Ensures(this.Capacity == buffer.Length);
      Contract.Ensures(this.Length == buffer.Length);
    }
    //
    // Summary:
    //     Initializes a new non-resizable instance of the System.IO.MemoryStream class
    //     based on the specified region (index) of a byte array.
    //
    // Parameters:
    //   buffer:
    //     The array of unsigned bytes from which to create this stream.
    //
    //   index:
    //     The index into buffer at which the stream begins.
    //
    //   count:
    //     The length of the stream in bytes.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     buffer is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     index or count is less than zero.
    //
    //   System.ArgumentException:
    //     The sum of index and count is greater than the length of buffer.
    public MemoryStream(byte[] buffer, int index, int count)
    {
      Contract.Requires(buffer != null);
      Contract.Requires(index >= 0);
      Contract.Requires(count >= 0);
      Contract.Requires(index + count <= buffer.Length);

      Contract.Ensures(this.CanSeek);
      Contract.Ensures(this.CanWrite);
      Contract.Ensures(this.CanRead);
      Contract.Ensures(this.Capacity == count);
      Contract.Ensures(this.Length == count);
    }
    //
    // Summary:
    //     Initializes a new non-resizable instance of the System.IO.MemoryStream class
    //     based on the specified region of a byte array, with the System.IO.MemoryStream.CanWrite
    //     property set as specified.
    //
    // Parameters:
    //   buffer:
    //     The array of unsigned bytes from which to create this stream.
    //
    //   index:
    //     The index in buffer at which the stream begins.
    //
    //   count:
    //     The length of the stream in bytes.
    //
    //   writable:
    //     The setting of the System.IO.MemoryStream.CanWrite property, which determines
    //     whether the stream supports writing.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     buffer is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     index or count are negative.
    //
    //   System.ArgumentException:
    //     The sum of index and count is greater than the length of buffer.
    public MemoryStream(byte[] buffer, int index, int count, bool writable)
    {
      Contract.Requires(buffer != null);
      Contract.Requires(index >= 0);
      Contract.Requires(count >= 0);
      Contract.Requires(index + count <= buffer.Length);

      Contract.Ensures(this.CanSeek);
      Contract.Ensures(this.CanWrite == writable);
      Contract.Ensures(this.CanRead);
      Contract.Ensures(this.Capacity == count);
      Contract.Ensures(this.Length == count);
    }
    //
    // Summary:
    //     Initializes a new instance of the System.IO.MemoryStream class based on the
    //     specified region of a byte array, with the System.IO.MemoryStream.CanWrite
    //     property set as specified, and the ability to call System.IO.MemoryStream.GetBuffer()
    //     set as specified.
    //
    // Parameters:
    //   buffer:
    //     The array of unsigned bytes from which to create this stream.
    //
    //   index:
    //     The index into buffer at which the stream begins.
    //
    //   count:
    //     The length of the stream in bytes.
    //
    //   writable:
    //     The setting of the System.IO.MemoryStream.CanWrite property, which determines
    //     whether the stream supports writing.
    //
    //   publiclyVisible:
    //     true to enable System.IO.MemoryStream.GetBuffer(), which returns the unsigned
    //     byte array from which the stream was created; otherwise, false.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     buffer is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     index or count is negative.
    //
    //   System.ArgumentException:
    //     The buffer length minus index is less than count.
    public MemoryStream(byte[] buffer, int index, int count, bool writable, bool publiclyVisible)
    {
      Contract.Requires(buffer != null);
      Contract.Requires(index >= 0);
      Contract.Requires(count >= 0);
      Contract.Requires(index <= buffer.Length - count);

      Contract.Ensures(this.CanSeek);
      Contract.Ensures(this.CanWrite == writable);
      Contract.Ensures(this.CanRead);
      Contract.Ensures(this.Capacity == count);
      Contract.Ensures(this.Length == count);
    }

    // Summary:
    //     Gets a value indicating whether the current stream supports reading.
    //
    // Returns:
    //     true if the stream is open.
    //public override bool CanRead { get; }
    //
    // Summary:
    //     Gets a value indicating whether the current stream supports seeking.
    //
    // Returns:
    //     true if the stream is open.
    //public override bool CanSeek { get; }
    //
    // Summary:
    //     Gets a value indicating whether the current stream supports writing.
    //
    // Returns:
    //     true if the stream supports writing; otherwise, false.
    //public override bool CanWrite { get; }
    //
    // Summary:
    //     Gets or sets the number of bytes allocated for this stream.
    //
    // Returns:
    //     The length of the usable portion of the buffer for the stream.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     A capacity is set that is negative or less than the current length of the
    //     stream.
    //
    //   System.ObjectDisposedException:
    //     The current stream is closed.
    //
    //   System.NotSupportedException:
    //     set is invoked on a stream whose capacity cannot be modified.
    public virtual int Capacity
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0);

        return default(int);
      }
      set
      {
        Contract.Requires(value >= 0);
        Contract.Requires(value >= this.Length);
      }
    }
    //
    // Summary:
    //     Gets the length of the stream in bytes.
    //
    // Returns:
    //     The length of the stream in bytes.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The stream is closed.
    // public virtual long Length { get { return default(long); } }
    //
    // Summary:
    //     Gets or sets the current position within the stream.
    //
    // Returns:
    //     The current position within the stream.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     The position is set to a negative value or a value greater than System.Int32.MaxValue.
    //
    //   System.ObjectDisposedException:
    //     The stream is closed.
    //public override long Position { get; set; }

    // Summary:
    //     Releases the unmanaged resources used by the System.IO.MemoryStream class
    //     and optionally releases the managed resources.
    //
    // Parameters:
    //   disposing:
    //     true to release both managed and unmanaged resources; false to release only
    //     unmanaged resources.
    //protected override void Dispose(bool disposing);
    //
    // Summary:
    //     Overrides System.IO.Stream.Flush() so that no action is performed.
    //public override void Flush();
    //
    // Summary:
    //     Returns the array of unsigned bytes from which this stream was created.
    //
    // Returns:
    //     The byte array from which this stream was created, or the underlying array
    //     if a byte array was not provided to the System.IO.MemoryStream constructor
    //     during construction of the current instance.
    //
    // Exceptions:
    //   System.UnauthorizedAccessException:
    //     The MemoryStream instance was not created with a publicly visible buffer.
    public virtual byte[] GetBuffer()
    {
      Contract.Ensures(Contract.Result<byte[]>() != null);
      Contract.Ensures(Contract.Result<byte[]>().Length >= Length);

      return default(byte[]);
    }
    //
    // Summary:
    //     Reads a block of bytes from the current stream and writes the data to buffer.
    //
    // Parameters:
    //   buffer:
    //     When this method returns, contains the specified byte array with the values
    //     between offset and (offset + count - 1) replaced by the characters read from
    //     the current stream.
    //
    //   offset:
    //     The byte offset in buffer at which to begin reading.
    //
    //   count:
    //     The maximum number of bytes to read.
    //
    // Returns:
    //     The total number of bytes written into the buffer. This can be less than
    //     the number of bytes requested if that number of bytes are not currently available,
    //     or zero if the end of the stream is reached before any bytes are read.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     buffer is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     offset or count is negative.
    //
    //   System.ArgumentException:
    //     offset subtracted from the buffer length is less than count.
    //
    //   System.ObjectDisposedException:
    //     The current stream instance is closed.
    //public override int Read(byte[] buffer, int offset, int count);
    //
    // Summary:
    //     Reads a byte from the current stream.
    //
    // Returns:
    //     The byte cast to a System.Int32, or -1 if the end of the stream has been
    //     reached.
    //
    // Exceptions:
    //   System.ObjectDisposedException:
    //     The current stream instance is closed.
    //public override int ReadByte();
    //
    // Summary:
    //     Sets the position within the current stream to the specified value.
    //
    // Parameters:
    //   offset:
    //     The new position within the stream. This is relative to the loc parameter,
    //     and can be positive or negative.
    //
    //   loc:
    //     A value of type System.IO.SeekOrigin, which acts as the seek reference point.
    //
    // Returns:
    //     The new position within the stream, calculated by combining the initial reference
    //     point and the offset.
    //
    // Exceptions:
    //   System.IO.IOException:
    //     Seeking is attempted before the beginning of the stream.
    //
    //   System.ArgumentOutOfRangeException:
    //     offset is greater than System.Int32.MaxValue.
    //
    //   System.ArgumentException:
    //     There is an invalid System.IO.SeekOrigin. -or- offset caused an arithmetic
    //     overflow.
    //
    //   System.ObjectDisposedException:
    //     The current stream instance is closed.
    //public override long Seek(long offset, SeekOrigin loc);
    //
    // Summary:
    //     Sets the length of the current stream to the specified value.
    //
    // Parameters:
    //   value:
    //     The value at which to set the length.
    //
    // Exceptions:
    //   System.NotSupportedException:
    //     The current stream is not resizable and value is larger than the current
    //     capacity.  -or- The current stream does not support writing.
    //
    //   System.ArgumentOutOfRangeException:
    //     value is negative or is greater than the maximum length of the System.IO.MemoryStream,
    //     where the maximum length is(System.Int32.MaxValue - origin), and origin is
    //     the index into the underlying buffer at which the stream starts.
    //public override void SetLength(long value);
    //
    // Summary:
    //     Writes the stream contents to a byte array, regardless of the System.IO.MemoryStream.Position
    //     property.
    //
    // Returns:
    //     A new byte array.
    public virtual byte[] ToArray()
    {
      Contract.Ensures(Contract.Result<byte[]>() != null);
      Contract.Ensures(Contract.Result<byte[]>().Length == Length);

      return default(byte[]);
    }
    //
    // Summary:
    //     Writes a block of bytes to the current stream using data read from buffer.
    //
    // Parameters:
    //   buffer:
    //     The buffer to write data from.
    //
    //   offset:
    //     The byte offset in buffer at which to begin writing from.
    //
    //   count:
    //     The maximum number of bytes to write.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     buffer is null.
    //
    //   System.NotSupportedException:
    //     The stream does not support writing. For additional information see System.IO.Stream.CanWrite.
    //      -or- The current position is closer than count bytes to the end of the stream,
    //     and the capacity cannot be modified.
    //
    //   System.ArgumentException:
    //     offset subtracted from the buffer length is less than count.
    //
    //   System.ArgumentOutOfRangeException:
    //     offset or count are negative.
    //
    //   System.IO.IOException:
    //     An I/O error occurs.
    //
    //   System.ObjectDisposedException:
    //     The current stream instance is closed.
    //public override void Write(byte[] buffer, int offset, int count);
    //
    // Summary:
    //     Writes a byte to the current stream at the current position.
    //
    // Parameters:
    //   value:
    //     The byte to write.
    //
    // Exceptions:
    //   System.NotSupportedException:
    //     The stream does not support writing. For additional information see System.IO.Stream.CanWrite.
    //      -or- The current position is at the end of the stream, and the capacity
    //     cannot be modified.
    //
    //   System.ObjectDisposedException:
    //     The current stream is closed.
    //public override void WriteByte(byte value);
    //
    // Summary:
    //     Writes the entire contents of this memory stream to another stream.
    //
    // Parameters:
    //   stream:
    //     The stream to write this memory stream to.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     stream is null.
    //
    //   System.ObjectDisposedException:
    //     The current or target stream is closed.
    public virtual void WriteTo(Stream stream)
    {
      Contract.Requires(stream != null);
    }

    // dummy
    public override void WriteByte(byte value)
    {
      throw new NotImplementedException();
    }

    // dummy
    public override long Seek(long arg0, SeekOrigin arg1)
    {
      throw new NotImplementedException();
    }
  }
}
