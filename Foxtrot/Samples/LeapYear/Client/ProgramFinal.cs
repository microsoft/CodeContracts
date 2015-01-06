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
using System.Text;
using System.Diagnostics.Contracts;

namespace Demo
{
  class Program
  {


    static void Main(string[] args)
    {
      if (args.Length < 1) return;
      var argString = args[0];
      Contract.Assume(argString != null);

      int daysSince1980 = Int32.Parse(args[0]);
      if (daysSince1980 <= 0) return;

      int dayInYear;
      int year = ZuneDate.YearSince1980(daysSince1980, out dayInYear);

      string[] days;
      if (DateTime.IsLeapYear(year))
      {
        days = new string[366];
      }
      else
      {
        Contract.Assert(dayInYear <= 365);
        days = new string[365];
      }
      days[dayInYear - 1] = "used";

    }
  }
}
