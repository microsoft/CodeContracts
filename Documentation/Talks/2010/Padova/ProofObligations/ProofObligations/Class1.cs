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
using System.Text;
using System.Diagnostics.Contracts;

namespace ProofObligations
{
  public class Implicit
  {
    public bool IsCiao(string s)
    {
      return s.Contains("ciao!");
    }

    public int[] RandomArray(int len)
    {
      var random = new Random(len);
      var arr = new int[len];
      for (var i=0; i < len; i++)
      {
        arr[i] = random.Next();
      }
      return arr;
    }

    public int Div(int x, int y)
    {
      return x / y;
    }

    public int Abs(int x)
    {
    if (x < 0)
        return -x;
      return x;
    }

  }

  public class Explicit
  {
    public string Concat(string p, string q)
    {
      Contract.Requires(p != null);
      Contract.Requires(q != null);

      var concat = p + q;

      Contract.Assert(concat != null);
      Contract.Assert(concat.Length > 0);

      return concat;
    }

    public string MyConcat()
    {
      return Concat("Ciao", null);
    }

    public double Abs(double x)
    {
      Contract.Ensures(Contract.Result<double>() >= 0);

      if (x < 0)
        return -x;
      return x;
    }

    public double Sqrt(double z)
    {
      return Math.Sqrt(Abs(z));
    }
  }
}
