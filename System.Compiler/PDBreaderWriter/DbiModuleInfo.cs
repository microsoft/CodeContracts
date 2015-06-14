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
//  File:   DbiModuleInfo.cs
//
using System;

namespace Microsoft.Singularity.PdbInfo.Features
{
    public class DbiModuleInfo
    {
        public DbiModuleInfo(BitAccess bits, bool readStrings)
        {
            bits.ReadInt32(out opened);
            section = new DbiSecCon(bits);
            bits.ReadUInt16(out flags);
            bits.ReadInt16(out stream);
            bits.ReadInt32(out cbSyms);
            bits.ReadInt32(out cbOldLines);
            bits.ReadInt32(out cbLines);
            bits.ReadInt16(out files);
            bits.ReadInt16(out pad1);
            bits.ReadUInt32(out offsets);
            bits.ReadInt32(out niSource);
            bits.ReadInt32(out niCompiler);
            if (readStrings) {
                bits.ReadCString(out moduleName);
                bits.ReadCString(out objectName);
            }
            else {
                bits.SkipCString(out moduleName);
                bits.SkipCString(out objectName);
            }
            bits.Align(4);
            if (opened != 0 || pad1 != 0) {
                throw new PdbException("Invalid DBI module. "+
                                       "(opened={0}, pad={1})", opened, pad1);
            }
        }

        public int          opened;                 //  0..3
        public DbiSecCon    section;                //  4..31
        public ushort       flags;                  // 32..33
        public short        stream;                 // 34..35
        public int          cbSyms;                 // 36..39
        public int          cbOldLines;             // 40..43
        public int          cbLines;                // 44..57
        public short        files;                  // 48..49
        public short        pad1;                   // 50..51
        public uint         offsets;
        public int          niSource;
        public int          niCompiler;
        public string       moduleName;
        public string       objectName;
    }
}
#endif