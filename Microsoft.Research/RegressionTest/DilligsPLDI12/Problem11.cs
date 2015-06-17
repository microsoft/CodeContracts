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

#define PROBLEM_11

/* 
* Query: Is the following always true? 
* getchar() always returns a character other than 'C' 
* no => error validated 
* yes=> wrong classification 
*/

 #if PROBLEM_11

using System;
using System.Diagnostics.Contracts;
public class Problem11
{
  const int OPT_A = 0;
  const int OPT_B = 1;
  const int OPT_C = 2;
  const int DEFAULT_OPT = 3;

  public class s
  {
    public int x;
    public int y;
  };


  //typedef void (*ptFn)(struct s*); 




  static void baz(s a)
  {
    a.x = 3;
    a.y = 1;
  }


  static void foo(s b)
  {
    b.x = 1;
    b.y = 3;
  }

  static void bar(s c)
  {
    c.x = 2;
    c.y = 2;
  }


  // Made it local to main
  // Action<s>[] arr = { foo, bar, baz, null };


  void S([Pure] Action<s>[] a, Action<s>[] b, int n)
  {

    int i;
    for (i = 0; i < n - 1; i++)
    {
      b[n - i - 2] = a[i];
    }

    b[n - 1] = null;
  }


  void call(Action<s> p, s a)
  {
    p(a);
    //(*p)(a); 
  }



  int get_opt()
  {

    //getchar returns the next character from the standard input 

    // printf("Enter a character:\n");
    char c = (char)System.Console.Read(); //getchar(); 
    if (c == 'A')
    {
      return OPT_A;
    }
    else if (c == 'B')
    {
      return OPT_B;
    }
    else if (c == 'C')
    {
      return OPT_C;
    }

    else
    {
      return DEFAULT_OPT;
    }
  }

  void main()
  {

    int opt = get_opt();

    s ss = new s();
    ss.x = 0;
    ss.y = 0;

    // F: moved from fields
    Action<s>[] arr = { foo, bar, baz, null };

    Action<s> p = null;
    Action<s>[] arr2 = { foo, baz, bar, null };

    S(arr, arr2, 4);

    if (opt <= 1)
    {
      p = arr[0];
    }
    else if (opt == 2)
    {
      p = arr2[2];
    }
    else
    {
      p = arr2[1];
    }

    call(p, ss);

    if (ss.x < 2)
    {
      Contract.Assert(opt < 2);
    }


  }
}
#endif