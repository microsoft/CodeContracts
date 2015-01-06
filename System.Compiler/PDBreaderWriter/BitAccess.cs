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

#if UseSingularityPDB

///////////////////////////////////////////////////////////////////////////////
//
//  Microsoft Research Singularity PDB Info Library
//
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//
//  File:   BitAccess.cs
//
using System;
using System.IO;
using System.Text;

namespace Microsoft.Singularity.PdbInfo.Features
{
    public class BitAccess
    {
        private byte[] buffer;
        private int offset;

        public int Position
        {
            get { return offset; }
            set { offset = value; }
        }

        public BitAccess(int capacity)
        {
            this.buffer = new byte[capacity];
            this.offset = 0;
        }

        internal void FillBuffer(Stream stream, int capacity)
        {
            MinCapacity(capacity);
            stream.Read(buffer, 0, capacity);
            offset = 0;
        }

        internal void WriteBuffer(Stream stream, int count)
        {
            stream.Write(buffer, 0, count);
        }

        internal void MinCapacity(int capacity)
        {
            if (buffer.Length < capacity) {
                buffer = new byte[capacity];
            }
            offset = 0;
        }

        public byte[] Buffer    // [GalenH] Just for PdbDump
        {
            get { return buffer; }
        }

        public void Align(int alignment)
        {
            while ((offset % alignment) != 0) {
                offset++;
            }
        }

        public void WriteInt32(int value)
        {
            buffer[offset + 0] = (byte) value;
            buffer[offset + 1] = (byte) (value >> 8);
            buffer[offset + 2] = (byte) (value >> 16);
            buffer[offset + 3] = (byte) (value >> 24);
            offset += 4;
        }

        public void WriteInt32(int[] values)
        {
            for (int i = 0; i < values.Length; i++) {
                WriteInt32(values[i]);
            }
        }

        public void WriteBytes(byte[] bytes)
        {
            for (int i = 0; i < bytes.Length; i++) {
                buffer[offset++] = bytes[i];
            }
        }

        public void ReadInt16(out short value)
        {
            value = (short)((buffer[offset + 0] & 0xFF) |
                            (buffer[offset + 1] << 8));
            offset += 2;
        }

        public void ReadInt32(out int value)
        {
            value = (int)((buffer[offset + 0] & 0xFF) |
                          (buffer[offset + 1] << 8) |
                          (buffer[offset + 2] << 16) |
                          (buffer[offset + 3] << 24));
            offset += 4;
        }

        public void ReadUInt16(out ushort value)
        {
            value = (ushort)((buffer[offset + 0] & 0xFF) |
                             (buffer[offset + 1] << 8));
            offset += 2;
        }

        public void ReadUInt8(out byte value)
        {
            value = (byte)((buffer[offset + 0] & 0xFF));
            offset += 1;
        }

        public void ReadUInt32(out uint value)
        {
            value = (uint)((buffer[offset + 0] & 0xFF) |
                           (buffer[offset + 1] << 8) |
                           (buffer[offset + 2] << 16) |
                           (buffer[offset + 3] << 24));
            offset += 4;
        }

        public void ReadInt32(int[] values)
        {
            for (int i = 0; i < values.Length; i++) {
                ReadInt32(out values[i]);
            }
        }

        public void ReadUInt32(uint[] values)
        {
            for (int i = 0; i < values.Length; i++) {
                ReadUInt32(out values[i]);
            }
        }

        public void ReadBytes(byte[] bytes)
        {
            for (int i = 0; i < bytes.Length; i++) {
                bytes[i] = buffer[offset++];
            }
        }

        public void ReadCString(out string value)
        {
            int len = 0;
            while (offset + len < buffer.Length && buffer[offset + len] != 0) {
                len++;
            }
            value = Encoding.UTF8.GetString(buffer, offset, len);
            offset += len + 1;
        }

        public void SkipCString(out string value)
        {
            int len = 0;
            while (offset + len < buffer.Length && buffer[offset + len] != 0) {
                len++;
            }
            offset += len + 1;
            value= null;
        }

        public void ReadGuid(out Guid guid)
        {
            uint a;
            ushort b;
            ushort c;
            byte d;
            byte e;
            byte f;
            byte g;
            byte h;
            byte i;
            byte j;
            byte k;

            ReadUInt32(out a);
            ReadUInt16(out b);
            ReadUInt16(out c);
            ReadUInt8(out d);
            ReadUInt8(out e);
            ReadUInt8(out f);
            ReadUInt8(out g);
            ReadUInt8(out h);
            ReadUInt8(out i);
            ReadUInt8(out j);
            ReadUInt8(out k);

            guid = new Guid(a, b, c, d, e, f, g, h, i, j, k);
        }
    }
}
#endif