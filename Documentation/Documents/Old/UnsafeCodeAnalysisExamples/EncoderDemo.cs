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

// From System.Text
namespace System.Text
{
  public abstract class EncodingDemo
  {
    public unsafe int GetByteCount(char* chars, int count)
    {
      Contract.Requires(count >= 0);
      Contract.Requires(Contract.WritableBytes(chars) >= (uint)count * sizeof(char));

      char[] arrChar = new char[count];
      for (int index = 0; index < count; index++)
      {
        arrChar[index] = *(chars + index);
      }
      return this.GetByteCount(arrChar, 0, count);
    }

    public abstract int GetByteCount(char[] chars, int index, int count);

    internal unsafe int GetByteCount(char* chars, int count, EncoderNLS encoder)
    {
      Contract.Requires(count >= 0);
      Contract.Requires(Contract.WritableBytes(chars) >= (uint)(sizeof(char) * count));

      return this.GetByteCount(chars, count);
    }
  }

  public class ASCIIEncodingDemo : EncodingDemo
  {
    public override unsafe int GetByteCount(char[] chars, int index, int count)
    {
      // Original
      if (chars == null)
      {
        throw new ArgumentNullException();
      }
      if (index < 0)
      {
        throw new ArgumentOutOfRangeException();
      }
      if (count < 0)
      {
        throw new ArgumentOutOfRangeException();
      }
      if ((chars.Length - index) < count)
      {
        throw new ArgumentOutOfRangeException();
      }
      if (chars.Length == 0)
      {
        return 0;
      }
      fixed (char* pChars = chars)
      {
        return this.GetByteCount(pChars + index, count, null);
      }
    }

  }

  public class EncoderNLS { }
}


 

#endif 