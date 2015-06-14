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
  internal struct BitSet
  {
    internal BitSet(BitAccess bits)
    {
      bits.ReadInt32(out size);    // 0..3 : Number of words
      words = new uint[size];
      bits.ReadUInt32(words);
    }

    //internal BitSet(int size) {
    //  this.size = size;
    //  words = new uint[size];
    //}

    internal bool IsSet(int index)
    {
      int word = index / 32;
      if (word >= this.size) return false;
      return ((words[word] & GetBit(index)) != 0);
    }

    //internal void Set(int index) {
    //  int word = index / 32;
    //  if (word >= this.size) return;
    //  words[word] |= GetBit(index);
    //}

    //internal void Clear(int index) {
    //  int word = index / 32;
    //  if (word >= this.size) return;
    //  words[word] &= ~GetBit(index);
    //}

    private static uint GetBit(int index)
    {
      return ((uint)1 << (index % 32));
    }

    //private static uint ReverseBits(uint value) {
    //  uint o = 0;
    //  for (int i = 0; i < 32; i++) {
    //    o = (o << 1) | (value & 1);
    //    value >>= 1;
    //  }
    //  return o;
    //}

    internal bool IsEmpty
    {
      get { return size == 0; }
    }

    //internal bool GetWord(int index, out uint word) {
    //  if (index < size) {
    //    word = ReverseBits(words[index]);
    //    return true;
    //  }
    //  word = 0;
    //  return false;
    //}

    private int size;
    private uint[] words;
  }

}
