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
//  File:   DbiHeader.cs
//
using System;

namespace Microsoft.Singularity.PdbInfo.Features
{
    public struct DbiHeader
    {
        public DbiHeader(BitAccess bits)
        {
            bits.ReadInt32(out sig);
            bits.ReadInt32(out ver);
            bits.ReadInt32(out age);
            bits.ReadInt16(out gssymStream);
            bits.ReadUInt16(out vers);
            bits.ReadInt16(out pssymStream);
            bits.ReadUInt16(out pdbver);
            bits.ReadInt16(out symrecStream);
            bits.ReadUInt16(out pdbver2);
            bits.ReadInt32(out gpmodiSize);
            bits.ReadInt32(out secconSize);
            bits.ReadInt32(out secmapSize);
            bits.ReadInt32(out filinfSize);
            bits.ReadInt32(out tsmapSize);
            bits.ReadInt32(out mfcIndex);
            bits.ReadInt32(out dbghdrSize);
            bits.ReadInt32(out ecinfoSize);
            bits.ReadUInt16(out flags);
            bits.ReadUInt16(out machine);
            bits.ReadInt32(out reserved);
        }

        public int      sig;                        // 0..3
        public int      ver;                        // 4..7
        public int      age;                        // 8..11
        public short    gssymStream;                // 12..13
        public ushort   vers;                       // 14..15
        public short    pssymStream;                // 16..17
        public ushort   pdbver;                     // 18..19
        public short    symrecStream;               // 20..21
        public ushort   pdbver2;                    // 22..23
        public int      gpmodiSize;                 // 24..27
        public int      secconSize;                 // 28..31
        public int      secmapSize;                 // 32..35
        public int      filinfSize;                 // 36..39
        public int      tsmapSize;                  // 40..43
        public int      mfcIndex;                   // 44..47
        public int      dbghdrSize;                 // 48..51
        public int      ecinfoSize;                 // 52..55
        public ushort   flags;                      // 56..57
        public ushort   machine;                    // 58..59
        public int      reserved;                   // 60..63
    }
}
#endif