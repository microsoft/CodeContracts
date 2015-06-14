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
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace Example
{
  public class POPL_Talk_Example
  {
    public void PartialInit(object[] a, object[] b)
    {
      Contract.Requires(a.Length <= b.Length);

      var j = 0;
      for (var i = 0; i < a.Length; i++)
      {
        if (a[i] != null)
        {
          b[j] = a[i];
          j++;
        } 
      }

      Contract.Assert(Contract.ForAll(0, j, x => b[x] != null));
    }

    bool P(char c) { return c == 'a'; }

    public void EvenOdd(int n)
    {
      var a = new int[n];

      var i = 0;
      while (i < n)
      {
        a[i++] = 1;
        a[i++] = -1;
      }
    }

    public void PartialInit(char[] a, char[] b)
    {
      Contract.Requires(a.Length <= b.Length);

      var j = 0;
      for (var i = 0; i < a.Length; i++)
      {
        var ch = a[i];
        if ('a' <= ch && ch <= 'b')
        {
          b[j] = ch;
          j++;
        }
      }
    }


    public void PartialInit_TestEverythingIsOk(char[] a, char[] b)
    {
      Contract.Requires(a.Length <= b.Length);

      var j = 0;
      for (var i = 0; i < a.Length; i++)
      {
        var ch = a[i];
        if ('a' <= ch && ch <= 'b')
        {
          b[j] = ch;
          j++;
        }
      }

      Contract.Assert(Contract.ForAll(0, j, ind => 'a' <= b[ind]));
      Contract.Assert(Contract.ForAll(0, j, ind => b[ind] <= 'z'));
 
    }
  }
}
