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

namespace NonNull
{
  public class NonNull
  {
    public string TrimSuffix(string s, string suffix)
    {
      Contract.Requires(s != null);
      Contract.Requires(suffix != null);

      string res = s;

      while (res.EndsWith(suffix))
      {
        int len = res.Length - suffix.Length;
        res = res.Substring(0, len);
      }

      Contract.Assert(res != null);

      return res;
    }

    public string TrimSuffixWPO(string s, string suffix)
    {
      Contract.Requires(s != null);
      Contract.Requires(suffix != null);

      string res = s;

      Contract.Assert(res != null);
      while (res.EndsWith(suffix))
      {
        Contract.Assert(res != null);
        Contract.Assume(suffix != null);
        int len = res.Length - suffix.Length;
        Contract.Assert(res != null);
        res = res.Substring(0, len);
        Contract.Assume(res != null);   // Postcondition of res
      }

      Contract.Assert(res != null);

      return res;
    }
  }
}
