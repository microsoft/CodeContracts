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

//-----------------------------------------------------------------------------
//
// Copyright (c) Microsoft. All rights reserved.
// This code is licensed under the Microsoft Public License.
// THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
// IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
// PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
//
//-----------------------------------------------------------------------------
using System;

namespace Microsoft.Cci.Pdb
{
  internal struct DbiHeader
  {
    internal DbiHeader(BitAccess bits)
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

    internal int sig;                        // 0..3
    internal int ver;                        // 4..7
    internal int age;                        // 8..11
    internal short gssymStream;                // 12..13
    internal ushort vers;                       // 14..15
    internal short pssymStream;                // 16..17
    internal ushort pdbver;                     // 18..19
    internal short symrecStream;               // 20..21
    internal ushort pdbver2;                    // 22..23
    internal int gpmodiSize;                 // 24..27
    internal int secconSize;                 // 28..31
    internal int secmapSize;                 // 32..35
    internal int filinfSize;                 // 36..39
    internal int tsmapSize;                  // 40..43
    internal int mfcIndex;                   // 44..47
    internal int dbghdrSize;                 // 48..51
    internal int ecinfoSize;                 // 52..55
    internal ushort flags;                      // 56..57
    internal ushort machine;                    // 58..59
    internal int reserved;                   // 60..63
  }
}
