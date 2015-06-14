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

// #define PROBLEM_10
// F: added some contracts and made the field a local variable, and then Clousot proves it  
//  : the result is unsound as we hit, in g(int&, int&), an unsound hypothesysis of Clousot, i.e. a != b
//  : then, I've added an extra level of inderection to make our assumption explicit, and it a bug in the precondition inference in Clousot
//  : once the bug is fixed, then Clousot suggests the condition i != j
#if PROBLEM_10
/*
 * Query: Can *i and j after the loop in h() have the same value?
 */

using System.Diagnostics.Contracts;
class Problem10
{
  //private readonly int[] x = new int[] { 1, 2, 3, 4, 5 };

  void swap(ref int a, ref int b)
  {
    Contract.Ensures(Contract.OldValue(a) == b);
    Contract.Ensures(Contract.OldValue(b) == a);
    int t = a;
    a = b;
    b = t;
  }

  void g(ref int a, ref int b)
  {
    Contract.Ensures(a == 2);
    Contract.Ensures(b == 1);

    a = 1;
    b = 2;

    swap(ref a, ref b);
  }

  // F: added
  void g(int[] arr, int i, int j)
  {
    Contract.Requires(i != j);
    Contract.Ensures(arr[i] == 2);
    Contract.Ensures(arr[j] == 1);

    g(ref arr[i], ref arr[j]);
  }


  int c(int i)
  {
    Contract.Ensures((i >= 0 && i <= 4) || Contract.Result<int>() == 0);
    Contract.Ensures(!(i >= 0 && i <= 4) || Contract.Result<int>() == 1);

    if (i < 0) return 0;
    else if (i > 4) return 0;
    return 1;
  }


  int h(ref int i)
  {
    Contract.Ensures(i >= 0); 
    Contract.Ensures(i <= 4);
    Contract.Ensures(Contract.Result<int>() >= 0);
    Contract.Ensures(Contract.Result<int>() <= 4);

    int j;
    while (true)
    {
      //printf("Please enter an integer in range [0-4]\n");
      //scanf("%d", i);
      i = System.Environment.TickCount;

      if (c(i) == 0)
      {
        // printf("Integer out of range.");
        continue;
      }

      Contract.Assert(i >= 0);

       
      //printf("Please enter another integer in range [0-4]\n");
      //scanf("%d", &j);

      j = System.Environment.TickCount;

      if (c(j) == 0)
      {
        //printf("Integer out of range.");
        continue;
      }

      break;
    }
    return j;
  }

  void main()
  {
    int i = 0, j;
    j = h(ref i);
    //printf("Value of i: %d j: %d\n", i, j);

    int[] x = new int[] { 1, 2, 3, 4, 5 };

    //int a = x[i];
    //int b = x[j];

    g(ref x[i], ref x[j]);

    Contract.Assert(x[j] == 1);

  }

  void main_Prime()
  {
    int[] x = new int[] { 1, 2, 3, 4, 5 };

    int i = 0, j;
    j = h(ref i);
    //printf("Value of i: %d j: %d\n", i, j);

    Contract.Assert(i != j);

    g(x, i, j);

    Contract.Assert(x[j] == 1);

  }

}
#endif