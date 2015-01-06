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
  public class VMCAI11
  {
    static void Francesco(char[] str)
    {
      if (str == null)
        return;

      int x=0, y= 12;

      var tmp = Sanitize(str, ref x, ref y);
    }

    static public char[] Sanitize(char[] str, ref int lower, ref int upper)
    {
      Contract.Requires(str != null);
      Contract.Ensures(lower + upper == Contract.Result<char[]>().Length);

      lower = upper = 0;

      var tmp = new char[str.Length];
      int j = 0;

      foreach (var ch in str)
      {
        if ('a' <= ch && ch <= 'z')
        {
          lower++; tmp[j++] = ch;
        }
        else if ('A' <= ch && ch <= 'Z')
        {
          upper++; tmp[j++] = (char)(ch | ' ');
        }
      }

      var result = new char[lower + upper];

      for (var i = 0; i < result.Length; i++)
      {
        result[i] = tmp[i];
      }
      return result;
    }
  }
}

#if false
      Contract.Requires(str != null);
      Contract.Ensures(upper >= 0);
      Contract.Ensures(lower >= 0);
      Contract.Ensures(lower + upper <= str.Length);
      Contract.Ensures(lower + upper == Contract.Result<char[]>().Length);
      Contract.Ensures(Contract.ForAll(0, lower + upper, index => 'a' <= Contract.Result<char[]>()[index]));
      Contract.Ensures(Contract.ForAll(0, lower + upper, index => Contract.Result<char[]>()[index] <= 'z'));
#endif