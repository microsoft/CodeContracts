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

// #define PROBLEM_3

#if PROBLEM_3

// F: 0 warnings with Clousot

/*
* Report is false positive.
* Query: Is the value of p after the loop in u2() > -2?
* Yes => discharged
* no/unknown => wrong classification
*/

using System.Diagnostics.Contracts;
public class Problem3
{
  int u1(int x)
  {
    int y = (x & ~(1 << 31));
    int z = (y | (1 << 4));
    return z;
  }


  int u2(int x)
  {

    if (x <= 0) return -1;
    int i = 1;
    int p = 1;
    while (i <= x)
    {
      i++;
      p += i;
    }
    return p;

  }

  int u3(int x)
  {
    int y = x + 1;
    return y % 2;
  }

  int d(int a)
  {
    return 2 * a;
  }


  int h(int a)
  {
    int b = u3(a);
    return d(a) + b;
  }


  int g(int a, int b)
  {
    int r = -1;
    int c = h(a);
    if (c == 0)
    {
      r = u1(b);
    }

    else
    {
      c = u2(b);
      r = c + 2;
    }
    return r;
  }


  void f(int a, int b)
  {

    int k = g(a, b);
    Contract.Assert(k > 0);
  }


  int main()
  {
    int i;
    // srand(time(0));
    for (i = 0; i < 1000; i++)
    {
      /*
      int x = rand() % 1000;
      int y = rand() % 1000;
      if (rand() % 2 == 0) x = -x;
      if (rand() % 2 == 0) y = -y;
      printf("x: %d y:%d\n", x, y);
      */

      int x = System.Environment.TickCount % 1000;
      int y = System.Environment.TickCount % 1000;
      if (System.Environment.TickCount % 2 == 0) x = -x;
      if (System.Environment.TickCount % 2 == 0) y = -y;

      f(x, y);
    }

    return i;
  }
}
#endif