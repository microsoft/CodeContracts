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
//  File:   DbiDbgHdr.cs
//
using System;

namespace Microsoft.Singularity.PdbInfo.Features
{
    public struct DbiDbgHdr
    {
        public DbiDbgHdr(BitAccess bits)
        {
            bits.ReadUInt16(out snFPO);
            bits.ReadUInt16(out snException);
            bits.ReadUInt16(out snFixup);
            bits.ReadUInt16(out snOmapToSrc);
            bits.ReadUInt16(out snOmapFromSrc);
            bits.ReadUInt16(out snSectionHdr);
            bits.ReadUInt16(out snTokenRidMap);
            bits.ReadUInt16(out snXdata);
            bits.ReadUInt16(out snPdata);
            bits.ReadUInt16(out snNewFPO);
            bits.ReadUInt16(out snSectionHdrOrig);
        }

        public ushort   snFPO;                 // 0..1
        public ushort   snException;           // 2..3 (deprecated)
        public ushort   snFixup;               // 4..5
        public ushort   snOmapToSrc;           // 6..7
        public ushort   snOmapFromSrc;         // 8..9
        public ushort   snSectionHdr;          // 10..11
        public ushort   snTokenRidMap;         // 12..13
        public ushort   snXdata;               // 14..15
        public ushort   snPdata;               // 16..17
        public ushort   snNewFPO;              // 18..19
        public ushort   snSectionHdrOrig;      // 20..21
    }
}
#endif