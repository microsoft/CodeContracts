// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Research.DataStructures
{
    public class ByteArray : IEquatable<ByteArray>, IComparable<ByteArray>
    {
        private static readonly byte[] EmptyByteArray = new byte[0];

        private readonly byte[] bytes;

        public ByteArray(ByteArray byteArray) { bytes = byteArray == null ? null : byteArray.bytes; }
        public ByteArray(byte[] bytes) { this.bytes = bytes; }
        public ByteArray(string base64) { bytes = base64 == null ? null : Convert.FromBase64String(base64); }

        public ByteArray(params object[] objects)
        {
            if (objects == null)
                bytes = null;
            else if (objects.Length == 0)
                bytes = EmptyByteArray;
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
                bytes = b.ToArray();
            }
        }

        public byte[] Bytes { get { return bytes; } }

        public override int GetHashCode() { return StructuralComparisons.StructuralEqualityComparer.GetHashCode(bytes); }

        public override bool Equals(object obj) { return this.Equals(obj as ByteArray); }

        public bool Equals(ByteArray other) { return other != null && StructuralComparisons.StructuralEqualityComparer.Equals(bytes, other.bytes); }

        public int CompareTo(ByteArray other) { return other == null ? 1 : StructuralComparisons.StructuralComparer.Compare(bytes, other.bytes); }

        public override string ToString() { return bytes == null ? String.Empty : Convert.ToBase64String(bytes); }
        // alternative: BitConverter.ToString(this.bytes); }

        public static implicit operator ByteArray(byte[] bytes)
        {
            return new ByteArray(bytes);
        }

        public string ToStringHex()
        {
            if (bytes == null)
                return String.Empty;

            return BitConverter.ToString(bytes);
        }
    }
}
