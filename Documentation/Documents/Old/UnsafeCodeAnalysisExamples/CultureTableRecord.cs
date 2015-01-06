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

#if true

#define FEATURE_FULL_CONTRACTS

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Contracts;

namespace ConsoleApplication1.Test
{
  class CultureTableRecord
  {
    private unsafe ushort* m_pPool;
 
    private unsafe int[] GetWordArray(uint iData)
    {
      Contract.Requires(iData >= 0);
      Contract.Requires(Contract.WritableBytes(m_pPool) >= (uint)(sizeof(ushort) * (iData + 1)));
      Contract.Requires(m_pPool[iData] >= 0);
      Contract.Requires(Contract.WritableBytes(m_pPool) >= (uint)(sizeof(ushort) * (iData+m_pPool[iData] + 1)));

      if (iData == 0)
      {
        return new int[0];
      }
      ushort* pWord = this.m_pPool + iData;
      int count = pWord[0];
      int[] values = new int[count];
      for (int i = 0; i < count; i++)
      {
        values[i] = pWord[i+1];
      }
      return values;
    }
  }
}

#endif