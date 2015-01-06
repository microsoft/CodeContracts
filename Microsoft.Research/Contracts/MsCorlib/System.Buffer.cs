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


using System.Runtime;
using System.Runtime.InteropServices;
using System.Security;
using System.Diagnostics.Contracts;

namespace System
{
  public static class Buffer
  {
    public static void BlockCopy(Array src, int srcOffset, Array dst, int dstOffset, int count)
    {
      Contract.Requires(src != null);
      Contract.Requires(dst != null);
      Contract.Requires(srcOffset >= 0);
      Contract.Requires(dstOffset >= 0);
      Contract.Requires(count >= 0);
      // Removed because the size is in *bytes* and we do not have an easy way to express the size of the elements of the arrays
      // Contract.Requires(src.Length - count >= srcOffset);
      // Contract.Requires(dst.Length -count >= dstOffset);
    }

    public static int ByteLength(Array array)
    {
      Contract.Requires(array != null);
      Contract.Ensures(Contract.Result<int>() >= 0);

      return 0;
    }

    public static byte GetByte(Array array, int index)
    {
      Contract.Requires(array != null);
      Contract.Requires(index >= 0);
      Contract.Requires(index < array.Length);

      return 0;
    }

    public static void SetByte(Array array, int index, byte value)
    {
      Contract.Requires(array != null);
      Contract.Requires(index >= 0);
      Contract.Requires(index < array.Length);
    }
  }
}
