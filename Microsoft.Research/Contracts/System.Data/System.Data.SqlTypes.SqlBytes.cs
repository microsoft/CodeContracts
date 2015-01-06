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
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Data.SqlTypes
{
  // Summary:
  //     Represents a mutable reference type that wraps either a System.Data.SqlTypes.SqlBytes.Buffer
  //     or a System.Data.SqlTypes.SqlBytes.Stream.
  //[Serializable]
  //[XmlSchemaProvider("GetXsdType")]
  public sealed class SqlBytes //: INullable, IXmlSerializable, ISerializable
  {
    //// Summary:
    ////     Initializes a new instance of the System.Data.SqlTypes.SqlBytes class.
    //public SqlBytes();
    ////
    //// Summary:
    ////     Initializes a new instance of the System.Data.SqlTypes.SqlBytes class based
    ////     on the specified byte array.
    ////
    //// Parameters:
    ////   buffer:
    ////     The array of unsigned bytes.
    //public SqlBytes(byte[] buffer);
    ////
    //// Summary:
    ////     Initializes a new instance of the System.Data.SqlTypes.SqlBytes class based
    ////     on the specified System.Data.SqlTypes.SqlBinary value.
    ////
    //// Parameters:
    ////   value:
    ////     A System.Data.SqlTypes.SqlBinary value.
    //public SqlBytes(SqlBinary value);
    ////
    //// Summary:
    ////     Initializes a new instance of the System.Data.SqlTypes.SqlBytes class based
    ////     on the specified System.IO.Stream value.
    ////
    //// Parameters:
    ////   s:
    ////     A System.IO.Stream.
    //public SqlBytes(Stream s);

    //// Summary:
    ////     Converts a System.Data.SqlTypes.SqlBinary structure to a System.Data.SqlTypes.SqlBytes
    ////     structure.
    ////
    //// Parameters:
    ////   value:
    ////     The System.Data.SqlTypes.SqlBinary structure to be converted.
    ////
    //// Returns:
    ////     A System.Data.SqlTypes.SqlBytes structure.
    //public static explicit operator SqlBytes(SqlBinary value);
    ////
    //// Summary:
    ////     Converts a System.Data.SqlTypes.SqlBytes structure to a System.Data.SqlTypes.SqlBinary
    ////     structure.
    ////
    //// Parameters:
    ////   value:
    ////     The System.Data.SqlTypes.SqlBytes structure to be converted.
    ////
    //// Returns:
    ////     A System.Data.SqlTypes.SqlBinary structure.
    //public static explicit operator SqlBinary(SqlBytes value);

    //// Summary:
    ////     Returns a reference to the internal buffer.
    ////
    //// Returns:
    ////     Returns a reference to the internal buffer. For System.Data.SqlTypes.SqlBytes
    ////     instances created on top of unmanaged pointers, it returns a managed copy
    ////     of the internal buffer.
    //public byte[] Buffer { get; }
    ////
    //// Summary:
    ////     Gets a Boolean value that indicates whether this System.Data.SqlTypes.SqlBytes
    ////     is null.
    ////
    //// Returns:
    ////     true if the System.Data.SqlTypes.SqlBytes is null, false otherwise.
    //public bool IsNull { get; }
    ////
    //// Summary:
    ////     Gets the length of the value that is contained in the System.Data.SqlTypes.SqlBytes
    ////     instance.
    ////
    //// Returns:
    ////     A System.Int64 value representing the length of the value that is contained
    ////     in the System.Data.SqlTypes.SqlBytes instance. Returns -1 if no buffer is
    ////     available to the instance or if the value is null. Returns a System.IO.Stream.Length
    ////     for a stream-wrapped instance.
    //public long Length { get; }
    ////
    //// Summary:
    ////     Gets the maximum length of the value of the internal buffer of this System.Data.SqlTypes.SqlBytes.
    ////
    //// Returns:
    ////     A long representing the maximum length of the value of the internal buffer.
    ////     Returns -1 for a stream-wrapped System.Data.SqlTypes.SqlBytes.
    //public long MaxLength { get; }
    ////
    //// Summary:
    ////     Returns a null instance of this System.Data.SqlTypes.SqlBytes.
    ////
    //// Returns:
    ////     Returns an instance in such a way that System.Data.SqlTypes.SqlBytes.IsNull
    ////     returns true.
    //public static SqlBytes Null { get; }
    ////
    //// Summary:
    ////     Returns information about the storage state of this System.Data.SqlTypes.SqlBytes
    ////     instance.
    ////
    //// Returns:
    ////     A System.Data.SqlTypes.StorageState enumeration.
    //public StorageState Storage { get; }
    ////
    //// Summary:
    ////     Gets or sets the data of this System.Data.SqlTypes.SqlBytes as a stream.
    ////
    //// Returns:
    ////     A System.IO.Stream.
    //public Stream Stream { get; set; }
    ////
    //// Summary:
    ////     Returns a managed copy of the value held by this System.Data.SqlTypes.SqlBytes.
    ////
    //// Returns:
    ////     The value of this System.Data.SqlTypes.SqlBytes as an array of bytes.
    //public byte[] Value { get; }

    //// Summary:
    ////     Gets or sets the System.Data.SqlTypes.SqlBytes instance at the specified
    ////     index.
    ////
    //// Parameters:
    ////   offset:
    ////     A System.Int64 value.
    ////
    //// Returns:
    ////     A System.Byte value.
    //public byte this[long offset] { get; set; }

    //// Summary:
    ////     Returns the XML Schema definition language (XSD) of the specified System.Xml.Schema.XmlSchemaSet.
    ////
    //// Parameters:
    ////   schemaSet:
    ////     A System.Xml.Schema.XmlSchemaSet.
    ////
    //// Returns:
    ////     A string that indicates the XSD of the specified XmlSchemaSet.
    //public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet);
    ////
    //// Summary:
    ////     Copies bytes from this System.Data.SqlTypes.SqlBytes instance to the passed-in
    ////     buffer and returns the number of copied bytes.
    ////
    //// Parameters:
    ////   offset:
    ////     An System.Int64 long value offset into the value that is contained in the
    ////     System.Data.SqlTypes.SqlBytes instance.
    ////
    ////   buffer:
    ////     The byte array buffer to copy into.
    ////
    ////   offsetInBuffer:
    ////     An System.Int32 integer offset into the buffer to start copying into.
    ////
    ////   count:
    ////     An System.Int32 integer representing the number of bytes to copy.
    ////
    //// Returns:
    ////     An System.Int64 long value representing the number of copied bytes.
    //public long Read(long offset, byte[] buffer, int offsetInBuffer, int count);
    ////
    //// Summary:
    ////     Sets the length of this System.Data.SqlTypes.SqlBytes instance.
    ////
    //// Parameters:
    ////   value:
    ////     The System.Int64 long value representing the length.
    //public void SetLength(long value);
    ////
    //// Summary:
    ////     Sets this System.Data.SqlTypes.SqlBytes instance to null.
    //public void SetNull();
    ////
    //// Summary:
    ////     Constructs and returns a System.Data.SqlTypes.SqlBinary from this System.Data.SqlTypes.SqlBytes
    ////     instance.
    ////
    //// Returns:
    ////     A System.Data.SqlTypes.SqlBinary.
    //public SqlBinary ToSqlBinary();
    ////
    //// Summary:
    ////     Copies bytes from the passed-in buffer to this System.Data.SqlTypes.SqlBytes
    ////     instance.
    ////
    //// Parameters:
    ////   offset:
    ////     An System.Int64 long value offset into the value that is contained in the
    ////     System.Data.SqlTypes.SqlBytes instance.
    ////
    ////   buffer:
    ////     The byte array buffer to copy into.
    ////
    ////   offsetInBuffer:
    ////     An System.Int32 integer offset into the buffer to start copying into.
    ////
    ////   count:
    ////     An System.Int32 integer representing the number of bytes to copy.
    //public void Write(long offset, byte[] buffer, int offsetInBuffer, int count);
  }
}
