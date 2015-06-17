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


using System;
using Microsoft.Contracts;
namespace TestAprove
{
  public class TestAprove
  {
    public int Prova(int z)
    {
      int k;
      if (z <= 0)
        k = -2;
      else
        k = z;

      Contract.Assert(k >= -2);

      return k;
    }

    public int Div(int x, int y)
    {
      if (y <= 0)
        throw new Exception();

      if (x <= 0)
        return 0;

      int z = 0;
      while (x >= y)
      {
        x = x - y;
        z++;      
      }

      return z;
    }

    internal int DivWithHeap(IntVal0 x, IntVal1 y)
    {
      if (y.Val <= 0)
        throw new Exception();

      if (x.Val <= 0)
        return 0;

      int z = 0;
      while (x.Val >= y.Val)
      {
        x.Val = x.Val - y.Val;
        z++;
      }

      return z;
    }
  }

  public class IntVal0
  {
    public int Val;
  }

  public class IntVal1
  {
    public int Val;
  }
}

namespace TestAprove2
{
  public class TestAprove
  {
    // TODO: Still problems with method calls
    internal int DivWithHeaWithCalls(IntVal0 x, IntVal1 y)
    {
      if (y.Val <= 0)
        throw new Exception();

      if (x.Val <= 0)
        return 0;

      int z = 0;
      while (x.Val >= y.Val)
      {
        x.Val = x.Val - y.Val;
        z++;
      }

      return z;
    }
  }

  public class IntVal0
  {
    int val;

    public int Val { get { return val; } set { val = value; } }
  }

  public class IntVal1
  {
    int val;

    public int Val { get { return val; } set { val = value; } }
  }
}
