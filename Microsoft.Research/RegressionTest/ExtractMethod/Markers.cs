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
using System.Diagnostics.Contracts;
using Microsoft.Research.ClousotRegression;

namespace ExtractMethod
{
  
  public class Ints
  {
    [Pure]
    public void __PreconditionMarker()
    {
    }

    [Pure]
    public void __PostconditionMarker(int x, bool returnValue)
    {
    }

    [RegressionOutcome("Contract.Ensures(14 <= Contract.Result<System.Int32>());")]
    public void F(int a, int b)
    {
      Contract.Requires(a >= 0);
      Contract.Requires(b >= 0);

      int a1 = a + 1;
      int b1 = b + 1;
      int z;

      // Project the state on a1, b1
      __PreconditionMarker();

      z = a1 + b1+ 12;

      // result is the variable z, should get >= 14
      __PostconditionMarker(z, true);

    }
  }

  public class Objects
  {
    [Pure]
    public void __PreconditionMarker()
    {
    }

    [Pure]
    public void __PostconditionMarker(int x, bool returnValue)
    {
    }

    [ClousotRegressionTest]
    [RegressionOutcome("Contract.Ensures(0 <= Contract.Result<System.Int32>());")]
    public void F(int[] a, int[] b)
    {
      if (a == null || b == null)
      {
        return;
      }

      int z;

      // Project the state on a, b, should get a != null and b != null
      __PreconditionMarker();

      z = a.Length + b.Length;

      // result is the variable z, should get >= 0
      __PostconditionMarker(z, true);

    }
  }
  
  public class BasicArithmeticTests
  {
    [Pure]
    public void __PreconditionMarker(){}

    [Pure]
    public void __PostconditionMarker<T>(T x, bool returnValue){}

    [ClousotRegressionTest]
    [RegressionOutcome("Contract.Ensures(Contract.Result<System.Int32>() == 0);")]
    int Decrement(int x)
        {
           Contract.Requires(x >= 0);
           Contract.Ensures(Contract.Result<System.Int32>() == 0);

           
          __PreconditionMarker();
          while (x != 0)
            x--;
          __PostconditionMarker(x, true);

            return x;
        }

    }

public class HentzingerPLDI
{

[Pure]
public void __PreconditionMarker(int a, int b){}

[Pure]
public void __PostconditionMarker(int a, int b, bool returnValue){}

[ClousotRegressionTest]
[RegressionOutcome("Contract.Requires(0 <= b);")]
[RegressionOutcome("Contract.Requires(0 <= a);")]
[RegressionOutcome("Contract.Ensures(1 <= b);")]
[RegressionOutcome("Contract.Ensures(1 <= a);")]
public void HentzingerEtAl(int n, bool nonDet)
            {
                Contract.Requires(n >= 0);

                int i ,a, b;

                i = 0; a = 0; b = 0;
                while (i < n)
                {

__PreconditionMarker(a,b);
  if (nonDet)
                    {
                        a = a + 1;
                        b = b + 2;
                    }
                    else
                    {
                        a = a + 2;
                        b = b + 1;
                    }
__PostconditionMarker(a, b, false);

                    i = i + 1;
                }
                Contract.Assert(a + b == 3 * n);
            }
}

public class MaxDemo
{

[Pure]
public void __PreconditionMarker(int[]  a){}

[Pure]
public void __PostconditionMarker<T>(int[]  a,T x,bool returnValue){}

[ClousotRegressionTest]
[RegressionOutcome("Contract.Requires(a != null);")]
[RegressionOutcome("Contract.Ensures(a != null);")]
[RegressionOutcome("Contract.Ensures(2 <= a.Length);")]
[RegressionOutcome("Contract.Ensures(Contract.Exists(0, a.Length, __i__ => Contract.Result<System.Int32>() == a[__i__]));")]
public int Max(int[] a)
        {
            Contract.Requires(a != null);
            Contract.Requires(a.Length > 0);

            Contract.Ensures(Contract.Exists(0, a.Length, j => a[j] == Contract.Result<int>()));

            var max = a[0];
            for (var i = 1; i < a.Length; i++)
            {

__PreconditionMarker(a);
var tmp = a[i];
                if (tmp > max)
                {
                    max = tmp;
                }
__PostconditionMarker(a,max, true);

            }

            return max;
        }
}
}
