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
//  File:   MsfDirectory.cs
//
using System;

namespace Microsoft.Singularity.PdbInfo.Features
{
    public class MsfDirectory
    {
        public MsfDirectory(PdbReader reader, PdbFileHeader head, BitAccess bits)
        {
            bits.MinCapacity(head.directorySize);
            int pages = reader.PagesFromSize(head.directorySize);

            // 0..n in page of directory pages.
            reader.Seek(head.directoryRoot, 0);
            bits.FillBuffer(reader.reader, pages * 4);

            DataStream stream = new DataStream(head.directorySize, bits, pages);
            bits.MinCapacity(head.directorySize);
            stream.Read(reader, bits);

            // 0..3 in directory pages
            int count;
            bits.ReadInt32(out count);

            // 4..n
            int[] sizes = new int[count];
            bits.ReadInt32(sizes);

            // n..m
            streams = new DataStream[count];
            for (int i = 0; i < count; i++) {
                if (sizes[i] <= 0) {
                    streams[i] = new DataStream();
                }
                else {
                    streams[i] = new DataStream(sizes[i], bits,
                                                reader.PagesFromSize(sizes[i]));
                }
            }
        }

        public DataStream[] streams;
    }

}
#endif