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
  internal struct DbiSecCon
  {
    internal DbiSecCon(BitAccess bits)
    {
      bits.ReadInt16(out section);
      bits.ReadInt16(out pad1);
      bits.ReadInt32(out offset);
      bits.ReadInt32(out size);
      bits.ReadUInt32(out flags);
      bits.ReadInt16(out module);
      bits.ReadInt16(out pad2);
      bits.ReadUInt32(out dataCrc);
      bits.ReadUInt32(out relocCrc);
      //if (pad1 != 0 || pad2 != 0) {
      //  throw new PdbException("Invalid DBI section. "+
      //                                 "(pad1={0}, pad2={1})",
      //                         pad1, pad2);
      //}
    }

    readonly internal short section;                    // 0..1
    readonly internal short pad1;                       // 2..3
    readonly internal int offset;                     // 4..7
    readonly internal int size;                       // 8..11
    readonly internal uint flags;                      // 12..15
    readonly internal short module;                     // 16..17
    readonly internal short pad2;                       // 18..19
    readonly internal uint dataCrc;                    // 20..23
    readonly internal uint relocCrc;                   // 24..27
  }
}
