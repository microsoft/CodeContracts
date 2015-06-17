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
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeContracts.Samples
{
  [ContractVerification(false)]
  public class NormalizedRational : Rational
  {
    public NormalizedRational(int n, int d)
      : base(n, d)
    {
      this.Normalize();
    }
    [Pure]
    private static int GCD(int x, int y)
    {
      // find greatest common divisor of x and y
      int ans;
      int z;
      if (x < y)
        ans = NormalizedRational.GCD(y, x);
      else if (x % y == 0)
        ans = y;
      else
      {
        z = x % y;
        ans = NormalizedRational.GCD(y, z);
      }
      return ans;
    }
    private void Normalize()
    {
      if (this.Numerator == 0)
      {
        this.Denominator = 1;
      }
      else
      {
        var g = NormalizedRational.GCD(this.Numerator, this.Denominator);
        this.Numerator = this.Numerator / g;
        this.Denominator = this.Denominator / g;
      }
    }

    public override void Divide(int divisor)
    {
      base.Divide(divisor);
      Normalize();
    }

    [ContractInvariantMethod]
    private void NormalizedInvariant()
    {
      Contract.Invariant(NormalizedRational.GCD(this.Numerator, this.Denominator) == 1);
    }

  }

}
 
