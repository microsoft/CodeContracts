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

// #define PROBLEM_1

#if PROBLEM_1

// F: 0 warnings with Clousot

/*
 * The report is a false positive.
 * First query: Is value of r after the loop in h() always >= 0?
  * Yes => discharged
  * No => wrong classification
 */

using System.Diagnostics.Contracts;

public class Problem1
{
  int f(int a, int b)
  {
    int t;

    if (a <= b)
    {
      t = a;
      a = b;
      b = t;
    }

    //int t;
    while (b != 0)
    {
      t = b;
      b = a % b;
      a = t;
    }
    return a;
  }


  int g(int a, int b)
  {

    int r = a * b;
    int k = f(a, b);
    int p = 0;
    if (k != 0)
    {
      p = r / k;
    }
    return p;


  }

  int h(int a, int b)
  {

    int r = (a < 0 ? -a : a);
    int k = (b < 0 ? -b : b);


    int i = 0;

    while (i < k)
    {
      r *= 2;
      i++;
    }

    return r;

  }

  void foo(int a, int b)
  {

    int x = f(a, b);
    int y = g(a, b);
    int w = h(a, b);

    int z = 0;

    if (x > 0)
    {
      if (y >= 0)
      {
        z = 1;
      }
    }

    if (w >= 0)
    {
      z = w + 1;
    }

    Contract.Assert(z > 0);



  }
}

#endif