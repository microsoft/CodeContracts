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

//
// Test that access to static struct in old value is properly extracted as a longer block expression 
// 
//
using System;
using System.Diagnostics.Contracts;

namespace Tests.Sources {


  partial class TestMain
  {
    public static DateTime CurrentFirstDayOfWeekInMonth(DateTime now) {
     Contract.Ensures(Contract.Result<DateTime>().Year == Contract.OldValue( DateTime.Today.Year ));

     return now;
    }

    partial void Run()
    {

      if (this.behave) {
        var dt = CurrentFirstDayOfWeekInMonth(DateTime.Today);
      }
      else {
        var dt = CurrentFirstDayOfWeekInMonth(new DateTime());
      }
    }

    public ContractFailureKind NegativeExpectedKind = ContractFailureKind.Postcondition;
    public string NegativeExpectedCondition = "Contract.Result<DateTime>().Year == Contract.OldValue( DateTime.Today.Year )";
  }

}
