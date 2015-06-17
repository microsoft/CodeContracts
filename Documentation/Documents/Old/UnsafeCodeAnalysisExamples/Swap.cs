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

namespace Swap
{
  class Swap
  {


    public unsafe void Swap0(int* x, int* y)
    {
      int tmp;

      tmp = *x;
      *x = *y;
      *y = tmp;
    }

    public unsafe void Swap1(int* x, int* y)
    {
      Contract.Requires(Contract.WritableBytes(x) >= sizeof(int));
      Contract.Requires(Contract.WritableBytes(y) >= sizeof(int));

      Contract.Ensures(Contract.WritableBytes(x) >= sizeof(int));
      Contract.Ensures(Contract.WritableBytes(y) >= sizeof(int));

      int tmp;

      tmp = *x;
      *x = *y;
      *y = tmp;
    }

    public unsafe void Swap2(int* x, int* y)
    {
      Contract.Requires(Contract.WritableBytes(x) >= sizeof(char));
      Contract.Requires(Contract.WritableBytes(y) >= sizeof(char));

      Contract.Ensures(Contract.WritableBytes(x) >= sizeof(char));
      Contract.Ensures(Contract.WritableBytes(y) >= sizeof(char));

      int tmp;

      tmp = *x;
      *x = *y;
      *y = tmp;
    }

    public unsafe void Swap3(int* x, int* y)
    {
      Contract.Requires(Contract.WritableBytes(x) >= sizeof(double));
      Contract.Requires(Contract.WritableBytes(y) >= sizeof(double));

      Contract.Ensures(Contract.WritableBytes(x) >= sizeof(double));
      Contract.Ensures(Contract.WritableBytes(y) >= sizeof(double));

      int tmp;

      tmp = *x;
      *x = *y;
      *y = tmp;
    }

    public unsafe void Swap4(int* x, int* y)
    {
      Contract.Requires(Contract.WritableBytes(x) >= sizeof(int));
      Contract.Requires(Contract.WritableBytes(y) >= sizeof(int));

      Contract.Ensures(Contract.WritableBytes(x) >= sizeof(double));
      Contract.Ensures(Contract.WritableBytes(y) >= sizeof(double));

      int tmp;

      tmp = *x;
      *x = *y;
      *y = tmp;
    }

    public unsafe void Swap5(int* x, int* y)
    {
      Contract.Requires(Contract.WritableBytes(x) > sizeof(int));
      Contract.Requires(Contract.WritableBytes(y) > sizeof(int));

      Contract.Ensures(Contract.WritableBytes(x) > sizeof(int));
      Contract.Ensures(Contract.WritableBytes(y) > sizeof(int));

      int tmp;

      tmp = *x;
      *x = *y;
      *y = tmp;
    }

    public unsafe void trySwap()
    {
      byte* ptr1 = stackalloc byte[2];
      byte* ptr2 = stackalloc byte[2];

      Swap1((int*)ptr1, (int*)ptr2);
    }

    public unsafe void trySwap1()
    {
      int* ptr1 = stackalloc int[1];
      int* ptr2 = stackalloc int[1];

      Swap5(ptr1, ptr2);
    }

    public unsafe void DoTheSwap()
    {
      byte* left = stackalloc byte[4];
      int* right = stackalloc int[1];

      left[2] = (byte)3;
      *(left + 2) = (byte)3;
      // ... Write something inside left and right ...

      Swap1((int*)left, (int*)right);

      *(left + 2) = (byte)3;
    }


  }
}

#endif