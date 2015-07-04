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
  internal struct DbiDbgHdr
  {
    internal DbiDbgHdr(BitAccess bits)
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

    internal ushort snFPO;                 // 0..1
    internal ushort snException;           // 2..3 (deprecated)
    internal ushort snFixup;               // 4..5
    internal ushort snOmapToSrc;           // 6..7
    internal ushort snOmapFromSrc;         // 8..9
    internal ushort snSectionHdr;          // 10..11
    internal ushort snTokenRidMap;         // 12..13
    internal ushort snXdata;               // 14..15
    internal ushort snPdata;               // 16..17
    internal ushort snNewFPO;              // 18..19
    internal ushort snSectionHdrOrig;      // 20..21
  }
}
