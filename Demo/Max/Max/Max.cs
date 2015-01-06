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

using System.Diagnostics.Contracts;
using System;

namespace RiSE
{
  public static class Utils
  {
    public static int Max(int[] elements)
    {
      Contract.Requires(elements != null);

      Contract.Ensures(Contract.ForAll(elements, el => el <= Contract.Result<int>()));
      Contract.Ensures(Contract.Exists(elements, el => el == Contract.Result<int>()));

      var max = int.MinValue;
      for (var i = 0; i < elements.Length; i++)
      {
        if (max < elements[i])
          max = elements[i];
      }

      return max;
    }

    public static int[] ParseToInts(string[] original)
    {
      Contract.Requires(original != null);

      var result = new int[original.Length];
      var position = 0;
      foreach (var s in original)
      {
        int parsed;
        if (Int32.TryParse(s, out parsed))
        {
          result[position++] = parsed;
        }
      }

      return position == 0 ? null : result;
    }

  }
}
