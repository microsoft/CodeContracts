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

using Microsoft.Contracts;
using System;

namespace TestTracePartitioning
{
  public class SimplePartitioning
  {
    // #1: ok with trace partitioning
    public void Partitioning0(bool b)
    {
      int x;
      if (b)
        x = 1;
      else
        x = -1;

      Contract.Assert(x != 0);
    }

    // #2: always ok
    public void Partitioning1(int num)
    {
      string[] strArr;

      if (num > 0)
      {
        strArr = new String[num];
      }
    }

    // #3: always ok?
    public void Partitioning2(int param1, int param2)
    {
      string[] strArr;
      int num = 0;

      while (param1 < param2)
      {
        num++;
      }

      if (num > 0)
      {
        strArr = new String[num];
      }
    }
  }
}

namespace FromMscorlib
{
  public class Random
  {
    private int[] SeedArray;

    // Should prove all the accesses correct (even without trace partitioning, but this was a bug)
    public Random(int Seed)
    {
      this.SeedArray = new int[0x38];
      int num2 = 0x9a4ec86 - Math.Abs(Seed);
      this.SeedArray[0x37] = num2;
      int num3 = 1;
      for (int i = 1; i < 0x37; i++)
      {
        int index = (0x15 * i) % 0x37;
        this.SeedArray[index] = num3;
        num3 = num2 - num3;
        if (num3 < 0)
        {
          num3 += 0x7fffffff;
        }
        num2 = this.SeedArray[index];
      }
      for (int j = 1; j < 5; j++)
      {
        for (int k = 1; k < 0x38; k++)
        {
          this.SeedArray[k] -= this.SeedArray[1 + ((k + 30) % 0x37)];
          if (this.SeedArray[k] < 0)
          {
            this.SeedArray[k] += 0x7fffffff;
          }
        }
      }
      Seed = 1;
    }
  }

}
