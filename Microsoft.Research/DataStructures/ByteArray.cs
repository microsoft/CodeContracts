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
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Research.DataStructures
{
  public class ByteArray : IEquatable<ByteArray>, IComparable<ByteArray>
  {
    private static readonly byte[] EmptyByteArray = new byte[0];

    private readonly byte[] bytes;

    public ByteArray(ByteArray byteArray) { this.bytes = byteArray == null ? null : byteArray.bytes; }
    public ByteArray(byte[] bytes) { this.bytes = bytes; }
    public ByteArray(string base64) { this.bytes = base64 == null ? null : Convert.FromBase64String(base64); }

    public ByteArray(params object[] objects)
    {
      if (objects == null)
        this.bytes = null;
      else if (objects.Length == 0)
        this.bytes = EmptyByteArray;
      else
      {
        List<byte> b = new List<byte>();
        foreach (var obj in objects)
        {
          if (obj == null)
            continue;
          byte[] toadd;
          if (obj is ByteArray)
            toadd = ((ByteArray)obj).Bytes;
          else if (obj is byte[])
            toadd = (byte[])obj;
          else if (obj is bool)
            toadd = BitConverter.GetBytes((bool)obj);
          else if (obj is int)
            toadd = BitConverter.GetBytes((int)obj);
          else if (obj is long)
            toadd = BitConverter.GetBytes((long)obj);
          else
            throw new NotImplementedException();
          if (toadd != null)
            b.AddRange(toadd);
        }
        this.bytes = b.ToArray();
      }
    }

    public byte[] Bytes { get { return this.bytes; } }

    public override int GetHashCode() { return StructuralComparisons.StructuralEqualityComparer.GetHashCode(this.bytes); }

    public override bool Equals(object obj) { return this.Equals(obj as ByteArray); }

    public bool Equals(ByteArray other) { return other != null && StructuralComparisons.StructuralEqualityComparer.Equals(this.bytes, other.bytes); }

    public int CompareTo(ByteArray other) { return other == null ? 1 : StructuralComparisons.StructuralComparer.Compare(this.bytes, other.bytes); }

    public override string ToString() { return this.bytes == null ? String.Empty : Convert.ToBase64String(this.bytes); }
    // alternative: BitConverter.ToString(this.bytes); }

    public static implicit operator ByteArray(byte[] bytes)
    {
      return new ByteArray(bytes);
    }

    public string ToStringHex()
    {
      if (this.bytes == null)
        return String.Empty;

      return BitConverter.ToString(this.bytes);
    }
  }
}
