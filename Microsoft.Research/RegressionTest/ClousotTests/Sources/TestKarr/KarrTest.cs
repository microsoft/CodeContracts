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

using Microsoft.Research.ClousotRegression;
using System.Diagnostics.Contracts;
using System;

namespace TestKarr
{
  public class Test
  {
    // #1 One Warning
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 10, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "assert unproven", PrimaryILOffset = 23, MethodILOffset = 0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Missing precondition in an externally visible method. Consider adding Contract.Requires(x == y); for parameter validation",PrimaryILOffset=0,MethodILOffset=0)]
	public int Karr0(int x, int y)
    {
      int k;

      if (x == y)
      {
        k = 1;
        Contract.Assert(x == y);  // ok
      }
      else
        k = 2;

      Contract.Assert(x == y); // Cannot conclude x == y
      return k;
    }

    // #2 ok
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 14, MethodILOffset = 0)]
    public int Karr1(int x, int y, int z)
    {
      int k;
      if (x == y)
        if (y == z)
        {
          k = 1;

          Contract.Assert(x == z); // ok because of transitivity
        }
        else
          k = 2;
      else
        k = 3;

      return k;
    }

    // #3 One warning
    [ClousotRegressionTest("karronly")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 20, MethodILOffset = 0)]
    public int Karr2(int x, int y)
    {
      int k;
      if (x == y)
      {
        k = 2;
      }
      else
      {
        x = 0;
        y = 0;
        k = 2;
      }

      Contract.Assert(x == y); // Now proven with Karr because of renaming 0 -> 0, x, y which gives the symbolic relation x == y

      return k;
    }

    [ClousotRegressionTest("karrothers")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 20, MethodILOffset = 0)]
    public int Karr2Prime(int x, int y)
    {
      int k;
      if (x == y)
      {
        k = 2;
      }
      else
      {
        x = 0;
        y = 0;
        k = 2;
      }

      Contract.Assert(x == y);

      return k;
    }

    // #4 ok
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 34, MethodILOffset = 44)]
    public int Karr3(int x, int y, int z)
    {
      Contract.Requires(x == 2 * y);
      Contract.Requires(2 * y == z);

      Contract.Ensures(Contract.Result<int>() == x + y + z);

      return x + y + z;
    }

    // #5 ok
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 13, MethodILOffset = 38)]
    public void Karr4(int x, int y)
    {
      Contract.Requires(x == y);
      Contract.Ensures(x == y);

      int k = 11;
      for (int i = 0; i < 100; i++)
        k += 2;
    }

    // #6 ok
    [ClousotRegressionTest("karronly")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 48, MethodILOffset = 0)]
    public void Karr5(int x, int y, int z, int k, int u1, int u2)
    {
      Contract.Requires(x == y);
      Contract.Requires(y == z);
      Contract.Requires(2 * z == k);
      Contract.Requires(u1 == u2);

      Contract.Assert(2 * x == k);
    }

    // F.: I fixed a bug in Karr so that "karronly" (i.e. Karr5 above) proves the assertion
    [ClousotRegressionTest("karrothers")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 48, MethodILOffset = 0)]
    public void Karr5Prime(int x, int y, int z, int k, int u1, int u2)
    {
      Contract.Requires(x == y);
      Contract.Requires(y == z);
      Contract.Requires(2 * z == k);
      Contract.Requires(u1 == u2);

      Contract.Assert(2 * x == k);
    }
  }

  public class Loops
  {
    [ClousotRegressionTest("karronly")]
    [RegressionOutcome(Outcome = ProofOutcome.False, Message = "assert is false", PrimaryILOffset = 18, MethodILOffset = 0)]
    public void Loop_Wrong0(int x, int len)
    {
      int y = x;

      for (int i = 0; i < len; i++)
      {
        y = x - 4;        
        Contract.Assert(y == x - 2 * i);    // This is not correct ...
      }
    }

    [ClousotRegressionTest("karronly")]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "assert unproven", PrimaryILOffset = (int)14, MethodILOffset = 0)]
    public void Loop_Wrong1(int x, int len)
    {
      int y = x;

      for (int i = 0; i < len; i++)
      {
        Contract.Assert(y == x - 4 * i);    // This is not correct, as y is a constant! (thanks Vincent)
        y = x - 4;
      }
    }

    [ClousotRegressionTest("karronly")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = (int)14, MethodILOffset = 0)]
    public void Loop_Ok(int x, int len)
    {
      int y = x;

      for (int i = 0; i < len; i++)
      {
        Contract.Assert(y == x - 4 * i);    // ok
        y = y - 4;
      }
    }

    [ClousotRegressionTest("karronly")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = (int)28, MethodILOffset = 0)]
    public void Loop_WithLongs(long input)
    {
      long i = input;
      long count = 0;
      while (i > 0)
      {
        count++;
        i--;
      }
      Contract.Assert(count + i == input);
    }
  }

  public class SimpleEqualities
  {// these examples are proved using non-null analysis. Unless it is a bug and Karr should prove it too, they should be moved to non-null tests.
    public bool state;

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 20, MethodILOffset = 42)]
    public int M(int z)
    {
      Contract.Requires(this.state);
      Contract.Ensures(!this.state);

      this.state = !this.state;

      return -2;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 9, MethodILOffset = 38)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 6, MethodILOffset = 23)]
    public int N(int q)
    {
      Contract.Ensures(!this.state);

      this.state = true;
      M(q);

      this.state = false;

      return -12;
    }

  }

  // From Kovacs & Voronkov TACAS'09 paper
  class KovacsVoronkov
  {
    [ClousotRegressionTest("karronly")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 46, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"Array access might be below the lower bound", PrimaryILOffset = 12, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset=12, MethodILOffset=0)]
    static void F(int[] A)
    {
      int a = 0, b = 0, c = 0;
      for (int i = 0; i < A.Length; i++)
      {
        if (A[a] <= 0)
        {
          b++;
        }
        else
        {
          c++;
        }
        a++;
      }

      Contract.Assert(a == b + c);
    }
  }

  // From Mscorlib.
  // This method may take too much time
  public class UmAlQuraCalendar
  {
    internal static DateMapping[] HijriYearInfo;
    public static short[] gmonth;

    [ClousotRegressionTest("karronly")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Array creation : ok", PrimaryILOffset = 5, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 13, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 13, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 43, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 43, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 73, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 73, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 103, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 103, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 133, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 133, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 163, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 163, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 192, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 192, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 222, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 222, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 252, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 252, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 282, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 282, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 313, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 313, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 344, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 344, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 374, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 374, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 406, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 406, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 438, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 438, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 470, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 470, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 502, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 502, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 533, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 533, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 565, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 565, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 597, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 628, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 660, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 660, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 692, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 692, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 723, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 723, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 754, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 754, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 785, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 785, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 815, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 815, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 846, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 846, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 877, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 877, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 908, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 908, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 939, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 939, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 969, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 969, MethodILOffset = 0)] 
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 1000, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 1000, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 1031, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 1031, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 1061, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 1061, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 1092, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 1092, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 1123, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 1123, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 1153, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 1153, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 1184, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 1184, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 1215, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 1215, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 1245, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 1245, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 1276, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 1276, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 1307, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 1307, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 1338, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 1338, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 1369, MethodILOffset = 0)] 
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 1369, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 1399, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 1399, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 1431, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 1431, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 1463, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 1463, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 1494, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 1494, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 1526, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 1526, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 1558, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 1558, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 1589, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 1653, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 1653, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 1684, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 1684, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 1716, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 1716, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 1748, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 1748, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 1779, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 1779, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 1810, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 1810, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 1840, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 1840, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 1871, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 1871, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 1902, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 1902, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 1932, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 1932, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 1963, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 1963, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 1994, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 1994, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 2024, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 2024, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 2055, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 2055, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 2086, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 2086, MethodILOffset = 0)] 
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 2116, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 2116, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 2147, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 2147, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 2178, MethodILOffset = 0)] 
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 2178, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 2209, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 2209, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 2240, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 2240, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 2270, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 2270, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 1589, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 1621, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 1621, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 2301, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 2301, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 2332, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 2332, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 2362, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 2362, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 2393, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 2393, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 2424, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 2424, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 2454, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 2454, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 2486, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 2486, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 2518, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 2518, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 2549, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 2549, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 2581, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 2581, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 2612, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 2612, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 2644, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 2644, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 2676, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 2676, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 2707, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 2707, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 2739, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 2739, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 2771, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 2771, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 2802, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 2802, MethodILOffset = 0)] 
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 2833, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 2833, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 2864, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 2864, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 2894, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 2894, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 2925, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 2925, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 2956, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 2956, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 2986, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 2986, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 3017, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 3017, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 3048, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 3048, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 3079, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 3079, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 3110, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 3110, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 3140, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 3140, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 3171, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 3171, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 3202, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 3202, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 3232, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 3232, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 3263, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 3263, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 3294, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 3294, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 3324, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 3324, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 3355, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 3355, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 3386, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 3386, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 3417, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 3417, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 3448, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 3448, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 3479, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 3479, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 3511, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 3511, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 3543, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 3543, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 3574, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 3574, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 3606, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 3606, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 3638, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 3638, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 3669, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 3669, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 3701, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 3701, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 3733, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 3733, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 3764, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 3764, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 3796, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 3796, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 3828, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 3828, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 3859, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 3859, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 3890, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 3890, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 3921, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 3921, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 3952, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 3952, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 3986, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 597, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 628, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 3986, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 4019, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 4019, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 4053, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 4053, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 4087, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 4087, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 4120, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 4120, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 4154, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 4154, MethodILOffset = 0)] [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Array creation : ok", PrimaryILOffset = 4186, MethodILOffset = 0)]
    static UmAlQuraCalendar()
    {
      HijriYearInfo = new DateMapping[] { 
        new DateMapping(0x2ea, 0x76c, 4, 30), new DateMapping(0x6e9, 0x76d, 4, 0x13), new DateMapping(0xed2, 0x76e, 4, 9), new DateMapping(0xea4, 0x76f, 3, 30), new DateMapping(0xd4a, 0x770, 3, 0x12), new DateMapping(0xa96, 0x771, 3, 7), new DateMapping(0x536, 0x772, 2, 0x18), new DateMapping(0xab5, 0x773, 2, 13), new DateMapping(0xdaa, 0x774, 2, 3), new DateMapping(0xba4, 0x775, 1, 0x17), new DateMapping(0xb49, 0x776, 1, 12), new DateMapping(0xa93, 0x777, 1, 1), new DateMapping(0x52b, 0x777, 12, 0x15), new DateMapping(0xa57, 0x778, 12, 9), new DateMapping(0x4b6, 0x779, 11, 0x1d), new DateMapping(0xab5, 0x77a, 11, 0x12), 
        new DateMapping(0x5aa, 0x77b, 11, 8), new DateMapping(0xd55, 0x77c, 10, 0x1b), new DateMapping(0xd2a, 0x77d, 10, 0x11), new DateMapping(0xa56, 0x77e, 10, 6), new DateMapping(0x4ae, 0x77f, 9, 0x19), new DateMapping(0x95d, 0x780, 9, 13), new DateMapping(0x2ec, 0x781, 9, 3), new DateMapping(0x6d5, 0x782, 8, 0x17), new DateMapping(0x6aa, 0x783, 8, 13), new DateMapping(0x555, 0x784, 8, 1), new DateMapping(0x4ab, 0x785, 7, 0x15), new DateMapping(0x95b, 0x786, 7, 10), new DateMapping(0x2ba, 0x787, 6, 30), new DateMapping(0x575, 0x788, 6, 0x12), new DateMapping(0xbb2, 0x789, 6, 8), new DateMapping(0x764, 0x78a, 5, 0x1d), 
        new DateMapping(0x749, 0x78b, 5, 0x12), new DateMapping(0x655, 0x78c, 5, 6), new DateMapping(0x2ab, 0x78d, 4, 0x19), new DateMapping(0x55b, 0x78e, 4, 14), new DateMapping(0xada, 0x78f, 4, 4), new DateMapping(0x6d4, 0x790, 3, 0x18), new DateMapping(0xec9, 0x791, 3, 13), new DateMapping(0xd92, 0x792, 3, 3), new DateMapping(0xd25, 0x793, 2, 20), new DateMapping(0xa4d, 0x794, 2, 9), new DateMapping(0x2ad, 0x795, 1, 0x1c), new DateMapping(0x56d, 0x796, 1, 0x11), new DateMapping(0xb6a, 0x797, 1, 7), new DateMapping(0xb52, 0x797, 12, 0x1c), new DateMapping(0xaa5, 0x798, 12, 0x10), new DateMapping(0xa4b, 0x799, 12, 5), 
        new DateMapping(0x497, 0x79a, 11, 0x18), new DateMapping(0x937, 0x79b, 11, 13), new DateMapping(0x2b6, 0x79c, 11, 2), new DateMapping(0x575, 0x79d, 10, 0x16), new DateMapping(0xd6a, 0x79e, 10, 12), new DateMapping(0xd52, 0x79f, 10, 2), new DateMapping(0xa96, 0x7a0, 9, 20), new DateMapping(0x92d, 0x7a1, 9, 9), new DateMapping(0x25d, 0x7a2, 8, 0x1d), new DateMapping(0x4dd, 0x7a3, 8, 0x12), new DateMapping(0xada, 0x7a4, 8, 7), new DateMapping(0x5d4, 0x7a5, 7, 0x1c), new DateMapping(0xda9, 0x7a6, 7, 0x11), new DateMapping(0xd52, 0x7a7, 7, 7), new DateMapping(0xaaa, 0x7a8, 6, 0x19), new DateMapping(0x4d6, 0x7a9, 6, 14), 
        new DateMapping(0x9b6, 0x7aa, 6, 3), new DateMapping(0x374, 0x7ab, 5, 0x18), new DateMapping(0x769, 0x7ac, 5, 12), new DateMapping(0x752, 0x7ad, 5, 2), new DateMapping(0x6a5, 0x7ae, 4, 0x15), new DateMapping(0x54b, 0x7af, 4, 10), new DateMapping(0xaab, 0x7b0, 3, 0x1d), new DateMapping(0x55a, 0x7b1, 3, 0x13), new DateMapping(0xad5, 0x7b2, 3, 8), new DateMapping(0xdd2, 0x7b3, 2, 0x1a), new DateMapping(0xda4, 0x7b4, 2, 0x10), new DateMapping(0xd49, 0x7b5, 2, 4), new DateMapping(0xa95, 0x7b6, 1, 0x18), new DateMapping(0x52d, 0x7b7, 1, 13), new DateMapping(0xa5d, 0x7b8, 1, 2), new DateMapping(0x55a, 0x7b8, 12, 0x16), 
        new DateMapping(0xad5, 0x7b9, 12, 11), new DateMapping(0x6aa, 0x7ba, 12, 1), new DateMapping(0x695, 0x7bb, 11, 20), new DateMapping(0x52b, 0x7bc, 11, 8), new DateMapping(0xa57, 0x7bd, 10, 0x1c), new DateMapping(0x4ae, 0x7be, 10, 0x12), new DateMapping(0x976, 0x7bf, 10, 7), new DateMapping(0x56c, 0x7c0, 9, 0x1a), new DateMapping(0xb55, 0x7c1, 9, 15), new DateMapping(0xaaa, 0x7c2, 9, 5), new DateMapping(0xa55, 0x7c3, 8, 0x19), new DateMapping(0x4ad, 0x7c4, 8, 13), new DateMapping(0x95d, 0x7c5, 8, 2), new DateMapping(730, 0x7c6, 7, 0x17), new DateMapping(0x5d9, 0x7c7, 7, 12), new DateMapping(0xdb2, 0x7c8, 7, 1), 
        new DateMapping(0xba4, 0x7c9, 6, 0x15), new DateMapping(0xb4a, 0x7ca, 6, 10), new DateMapping(0xa55, 0x7cb, 5, 30), new DateMapping(0x2b5, 0x7cc, 5, 0x12), new DateMapping(0x575, 0x7cd, 5, 7), new DateMapping(0xb6a, 0x7ce, 4, 0x1b), new DateMapping(0xbd2, 0x7cf, 4, 0x11), new DateMapping(0xbc4, 0x7d0, 4, 6), new DateMapping(0xb89, 0x7d1, 3, 0x1a), new DateMapping(0xa95, 0x7d2, 3, 15), new DateMapping(0x52d, 0x7d3, 3, 4), new DateMapping(0x5ad, 0x7d4, 2, 0x15), new DateMapping(0xb6a, 0x7d5, 2, 10), new DateMapping(0x6d4, 0x7d6, 1, 0x1f), new DateMapping(0xdc9, 0x7d7, 1, 20), new DateMapping(0xd92, 0x7d8, 1, 10), 
        new DateMapping(0xaa6, 0x7d8, 12, 0x1d), new DateMapping(0x956, 0x7d9, 12, 0x12), new DateMapping(0x2ae, 0x7da, 12, 7), new DateMapping(0x56d, 0x7db, 11, 0x1a), new DateMapping(0x36a, 0x7dc, 11, 15), new DateMapping(0xb55, 0x7dd, 11, 4), new DateMapping(0xaaa, 0x7de, 10, 0x19), new DateMapping(0x94d, 0x7df, 10, 14), new DateMapping(0x49d, 0x7e0, 10, 2), new DateMapping(0x95d, 0x7e1, 9, 0x15), new DateMapping(0x2ba, 0x7e2, 9, 11), new DateMapping(0x5b5, 0x7e3, 8, 0x1f), new DateMapping(0x5aa, 0x7e4, 8, 20), new DateMapping(0xd55, 0x7e5, 8, 9), new DateMapping(0xa9a, 0x7e6, 7, 30), new DateMapping(0x92e, 0x7e7, 7, 0x13), 
        new DateMapping(0x26e, 0x7e8, 7, 7), new DateMapping(0x55d, 0x7e9, 6, 0x1a), new DateMapping(0xada, 0x7ea, 6, 0x10), new DateMapping(0x6d4, 0x7eb, 6, 6), new DateMapping(0x6a5, 0x7ec, 5, 0x19), new DateMapping(0, 0x7ed, 5, 14)
     };
      gmonth = new short[] { 0x1f, 0x1f, 0x1c, 0x1f, 30, 0x1f, 30, 0x1f, 0x1f, 30, 0x1f, 30, 0x1f, 0x1f };
      var minDate = new DateTime(0x76c, 4, 30);
      var time = new DateTime(0x7ed, 5, 13, 0x17, 0x3b, 0x3b, 0x3e7);
      var maxDate = new DateTime(time.Ticks + 0x270fL);
    }

    // F: This struct may be nonsense, I took the template from mscorlib just to make the compiler happy for the regression test above
    internal struct DateMapping
    {
      internal int HijriMonthsLengthFlags;
      internal DateTime GregorianDate;
      internal DateMapping(int MonthsLengthFlags, int GYear, int GMonth, int GDay)
      {
        this.HijriMonthsLengthFlags = MonthsLengthFlags; this.GregorianDate = new DateTime(GDay, GMonth, GDay);  
      }
    }
  }
}


