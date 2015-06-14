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

// #define PROBLEM_8

// F: we do not ask the question, as we infer it.
//    However, we have other warnings that all originates from the fact that we do not do a top-down analysis, so we lose the information that N == 3, etc.

#if PROBLEM_8


/*
* False positive:
* Query: In function c, is the value of r after the loop always >= 0?
* Yes: discharged
* No: wrong classification
*/


using System.Diagnostics.Contracts;
public class Problem8
{
  const int N = 3;
  const int A = 0;
  const int B = 1;
  const int C = 2;


  /*
  int a(int* arr, int n);
  int b(int* arr, int n);
  int f(int d, int* ar, int n);
    */
  void foo(int p)
  {
    //  int* ar = malloc((N+1)*sizeof(int));
    int[] ar = new int[(N + 1)];

    g(ar, N + 1);

    Contract.Assert(ar[N] == 0); // F: Clousot weakness we do not get that ar[N+1-1] == ar[N]
    Contract.Assume(ar[N-1] == 0); // F: we need loop unrolling to prove this one

    // F: those two lines stand for the original "k = rand()%N;"
    int k = System.Environment.TickCount % N; 
    if (k < 0) k = -k;

    Contract.Assert(k < N);

    int v = ar[k];

    int t = f(v, ar, N);
    //  printf("t is: %d\n", t);
    Contract.Assert(t >= 0);

    // F: should we do something with that?
    //free(ar);  

  }
  void g(int[] ar, int n)
  {
    Contract.Requires(0 < ar.Length);
    Contract.Requires(n <= ar.Length);
    Contract.Requires(n > 0);
    Contract.Ensures(Contract.ForAll(0, n - 1, k => ar[k] >= 0));
    Contract.Ensures(ar[n - 1] == 0);

    int i;
    for (i = 0; i < (n - 1); i++)
    {
      Contract.Assert(n - i -2 >= 0); // F: added as hint to clousot: this is proven by Subpolyhedra, but subpolyhedra run after the array analysis
      ar[i] = (n - i - 2);
    }
    Contract.Assert(i == n-1); // F: added as hint for the array analysis, which does not deduce here that  i == n- 1

    ar[i] = 0;
  }

  int a(int[] arr, int n)
  {
    Contract.Ensures(arr[n-1] + n > n || Contract.Result<int>() == 1);
    int s = arr[n - 1];
    int i;
    for (i = 0; i < n; i++)
    {
      s++;
    }
    Contract.Assert(s == arr[n-1] + n); // F: we should add the assert to help Clousot
    if (s > n) return -1;
    return 1;

  }

  int b(int[] arr, int n)
  {
    Contract.Ensures(arr[n] != 0 || Contract.Result<int>() == 2);
    if (arr[n] != 0) return -2;
    return 2;

  }

  int c(int[] arr, int n)
  {
    Contract.Requires(arr != null);
    Contract.Requires(n <= arr.Length);
    int r = 0;
    int i;
    for (i = 0; i < n; i++)
    {
      int t = arr[i] < 0 ? -arr[i] : arr[i];
      r += t;
    }

    return r;
  }


  int f(int d, int[] ar, int n)
  {
    Contract.Requires(ar[n - 1] == 0);
    Contract.Requires(ar[n] == 0); // this is a consequence of the caller context, probably should be an assume
    Contract.Ensures(d< 0 || d > 2 || Contract.Result<int>() >= 0); // F: added to track where we lose precision

    int r = 0;
    switch (d)
    {
      case A:
        {
          r = a(ar, n);
          Contract.Assert(r >= 0); // F: added to track where we lose precision
          break;
        }
      case B:
        {
          r = b(ar, n);
          Contract.Assert(r >= 0); // F: added to track where we lose precision
          break;
        }
      case C:
        {
          r = c(ar, n); 
          Contract.Assert(r >= 0); // F: added to track where we lose precision
          break;
        }
      default:
        {
          r = -1;
          break;
        }

    }

    return r;
  }
}
#endif