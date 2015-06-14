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
using System.Diagnostics.Contracts;

namespace CodeContracts.Samples
{
  public class MyMath
  {
    public static int GCD(int x, int y)
    {
      while (true)
      {
        if (x < y)
        {
          y %= x;
          if (y == 0)
          {
            return x;
          }
        }
        else
        {
          x %= y;
          if (x == 0)
          {
            return y;
          }
        }
      }
    }
  }

  public class Rational
  {
    int d, n;
    bool pos;

    static public Rational NormalizedRational(bool pos, int x, int y)
    {
      if (x == 0)
      {
        return new Rational(true, 0, 1);
      }

      int gcd = MyMath.GCD(x, y);
      return new Rational(pos, x / gcd, y / gcd);
    }

    private Rational(bool pos, int x, int y)
    {
      this.pos = pos;
      this.d = x;
      this.n = y;
    }
  }

}
