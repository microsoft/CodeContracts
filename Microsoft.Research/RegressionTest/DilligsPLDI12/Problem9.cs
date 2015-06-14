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

// F: added a bunch of preconditions to mimick the top-down analysis
//  : added the postconditions to explain what f{1,2,3} do
//  : we get the warning as the 3 swaps do not ensure the ordering

//#define PROBLEM_9

#if PROBLEM_9
/*
 * Is the following possible?
 * The return value of rand at line 3 is less than the result of rand() at line 2 *AND* 
 * the result of rand() at line 2 is less than the result of rand() at line 1.
*/



using System.Diagnostics.Contracts;
class Problem9
{
  class s
  {
    public int x;
    public int y;
    public int z;
  };

  s h(int a, int b, int c)
  {
    // struct s* n = malloc(sizeof(struct s));

    s n = new s();

    n.x = a;
    n.y = b;
    n.z = c;

    return n;
  }


  void f1(s ss)
  {
    Contract.Ensures(ss.x <= ss.y);
    Contract.Ensures(Contract.OldValue(ss.z) == ss.z);

    int t;
    if (ss.y < ss.x)
    {
      t = ss.y;
      ss.y = ss.x;
      ss.x = t;
    }
  }

  void f2(s ss)
  {
    Contract.Ensures(ss.y <= ss.z);
    Contract.Ensures(Contract.OldValue(ss.x) == ss.x);

    int z;
    if (ss.z < ss.y)
    {
      z = ss.z;
      ss.z = ss.y;
      ss.y = z;
    }
  }

  void f3(s ss)
  {
    Contract.Ensures(ss.x <= ss.z);
    Contract.Ensures(Contract.OldValue(ss.y) == ss.y);

    int x;
    if (ss.z < ss.x)
    {
      x = ss.z;
      ss.z = ss.x;
      ss.x = x;
    }

  }

  int c1(int a, int b, s ss)
  {
    if (a > b)
    {
      if (a == ss.x) return 1;
    }
    return 0;

  }

  int c2(int a, int b, s ss)
  {
    if (a > b)
    {
      if (b == ss.z) return 1;
    }
    return 0;
  }




  int d(int[] a, s ss)
  {
    Contract.Requires(ss != null);
    Contract.Requires(2 < a.Length);

    if (c1(a[0], a[1], ss) != 0) return -1;
    if (c1(a[0], a[2], ss) != 0) return -2;
    if (c1(a[1], a[2], ss) != 0) return -3;
    if (c2(a[0], a[1], ss) != 0) return -4;
    if (c2(a[0], a[2], ss) != 0) return -5;
    if (c2(a[1], a[2], ss) != 0) return -6;

    return 1;
  }





  void g(out int a, out int b, out int c)
  {
    a = System.Environment.TickCount;
    b = System.Environment.TickCount;
    c = System.Environment.TickCount;
  }




  void foo()
  {
    int a;
    int b;
    int c;
    s ss;

    g(out  a, out b, out c); // a,b, c get random values
    ss = h(a, b, c);         // allocate a new structure with those values
    int[] arr = new int[3];
    arr[0] = a;
    arr[1] = b;
    arr[2] = c;

    f1(ss); // do the comparisons
    Contract.Assert(ss.x <= ss.y);

    f2(ss);
    Contract.Assert(ss.y <= ss.z);

    f3(ss);
    Contract.Assert(ss.x <= ss.z);

    int r = d(arr, ss);
    Contract.Assert(r > 0);



  }
}
#endif