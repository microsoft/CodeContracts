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
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;
using System.Threading;

namespace Tests.Sources
{

#if !NETFRAMEWORK_4_5 
  public class ContractAbbreviatorAttribute : Attribute
  {
  }
#endif

  static class ValidationHelper
  {
    [ContractAbbreviator]
    public static void IsGreaterOrEqualThan<T>(T value, T reference)
      where T : IComparable
    {
      Contract.Requires<ArgumentOutOfRangeException>(value.CompareTo(reference) >= 0);
    }
    
    [ContractAbbreviator]
    public static void IsNotNull(object value)
    {
      Contract.Requires<ArgumentNullException>(value != null);
    }
    
    [ContractAbbreviator]
    public static void HasStringLength(string @string, int minimum, int maximum)
    {
      ValidationHelper.IsNotNull(@string);
      ValidationHelper.IsGreaterOrEqualThan(minimum, 0);
      ValidationHelper.IsGreaterOrEqualThan(maximum, 0);
      ValidationHelper.IsGreaterOrEqualThan(maximum, minimum);
      Contract.Requires<ArgumentOutOfRangeException>(minimum <= @string.Length && @string.Length <= maximum);
    }
    
  }



  partial class TestMain
  {
    partial void Run()
    {
      if (behave) {
        MethodBug("Test");
      }
      else {
        MethodBug("Way too long this is a string");
      }
    }

    static void MethodBug(string s)
    {
      ValidationHelper.HasStringLength(s, 0, 10);
      
      Console.WriteLine(s);
    }
    
    public ContractFailureKind NegativeExpectedKind = ContractFailureKind.Precondition;
    public string NegativeExpectedCondition = "Precondition failed: minimum <= @string.Length && @string.Length <= maximum";

  }

}
