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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace Numerical
{
  public class GCDEx
  {
    static public int GCD(int x, int y)
    {
      Contract.Requires(x > 0);
      Contract.Requires(y > 0);

      Contract.Ensures(Contract.Result<int>() > 0);

      while (true)
      {
        if (x < y)
        {
          y %= x;
          if (y == 0)
            return x;
        }
        else
        {
          x %= y;
          if (x == 0)
            return y;
        }
      }
    }

    static public int GCD_Termination(int x, int y)
    {
      Contract.Requires(x > 0);
      Contract.Requires(y > 0);

      Contract.Ensures(Contract.Result<int>() > 0);

      while (true)
      {
        int diff;

        if (x < y)
        {
          diff = y - x;
          y %= x;

          Contract.Assert(diff > (y - x));
          if (y == 0)
            return x;
        }
        else
        {
          diff = x - y;
          x %= y;
          if (x == 0)
            return y;

          Contract.Assert(diff > (x - y));
        }


        Contract.Assert(diff >= 0);

      }
    }

  }

  public class BoundsChecking
  {
    public void AllToZero(int[] a)
    {
      for (int i = 0; i < a.Length; i++)
      {
        Contract.Assert(i >= 0);
        Contract.Assert(i < a.Length);
        a[i] = 0;
      }
    }

    public void CopyTo(object[] from, object[] to, int start)
    {
      Contract.Requires(start >= 0);
      Contract.Requires(start < to.Length);
      Contract.Requires(from.Length <= to.Length - start);
      for (int i = 0; i < from.Length; i++)
        to[start + i] = from[i];
    }
  }

  public class Karr
  {
    public void Loop()
    {
      int x = 0, y = 2;
   
      while (NonDet())
      {
        x++; y++;
      }
      Contract.Assert(y - x == 2);
    }

    public bool NonDet()
    {
      return System.Environment.TickCount % 23 == 0;
    }
  }

  public class SubPoly
  {
    public int ChunkLen;
    public char[] ChunkChars;

    void Append(int wb, int count)
    {
      Contract.Requires(wb >= 2 * count);
      if (count + ChunkLen > ChunkChars.Length)
        CopyChars(wb, ChunkChars.Length - ChunkLen);
    }

    private void CopyChars(int wb, int len)
    {
      Contract.Requires(wb >= 2 * len);

    }
  }

  public class Disjunction
  {
    public int Simple(bool b)
    {
      int z;
      if (b)
        z = 12;
      else
        z = -12;
      return 1 / z;
    }

    public string Simple2(bool b)
    {
      Contract.Ensures(
        Contract.Result<string>() == null 
        || Contract.Result<string>().Length > 0);

      if (b)
        return null;
      else
        return "Ciao!";
    }
  }
}
