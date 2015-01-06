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
using System.Diagnostics.Contracts;

namespace Microsoft.VisualBasic
{
  public enum CompareMethod
  {
    Binary,
    Text
  }


  public static class Strings
  {

    public static int Len(string str)
    {
      Contract.Ensures(Contract.Result<int>() == str.Length);
      return default(int);
    }

    public static int InStr(string string1, string string2, CompareMethod cmp)
    {
      Contract.Ensures(Contract.Result<int>() < 0 || string1 != null);
      return default(int);
    }

    public static string Mid(string str, int Start, int Length)
    {
      Contract.Ensures(Contract.Result<string>() != null);

      return default(string);
    }

    public static string[] Split(string Expression, string Delimiter, int Limit, CompareMethod Compare)
    {
      Contract.Ensures(Contract.Result<string[]>() != null);

      return default(string[]);
    }
  }
}
