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


namespace Demo
{
  public class ZuneDate
  {
 
    public static int YearSince1980(int daysSince1980, out int dayInYear)
    {
      Contract.Requires(daysSince1980 >= 1);

      Contract.Ensures(Contract.ValueAtReturn(out dayInYear) >= 1);
      Contract.Ensures(Contract.ValueAtReturn(out dayInYear) <= 366);
      Contract.Ensures(DateTime.IsLeapYear(Contract.Result<int>()) || Contract.ValueAtReturn(out dayInYear) <= 365);

      var year = 1980;
      var daysLeft = daysSince1980;

      while (daysLeft > 365)
      {
        var oldDaysLeft = daysLeft;
        if (DateTime.IsLeapYear(year))
        {
          if (daysLeft > 366)
          {
            daysLeft -= 366;
            year += 1;
          }
          else
          {
            dayInYear = daysLeft;
            return year;
          }
        }
        else
        {
          daysLeft -= 365;
          year += 1;
        }
        Contract.Assert(daysLeft < oldDaysLeft);
      }

      dayInYear = daysLeft;
      return year;
    }

  }
}
