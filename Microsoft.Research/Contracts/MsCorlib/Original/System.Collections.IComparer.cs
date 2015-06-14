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
using System.Runtime.InteropServices;

namespace System.Collections {
  // Summary:
  //     Exposes a method that compares two objects.
  [ComVisible(true)]
  public interface IComparer {
    // Summary:
    //     Compares two objects and returns a value indicating whether one is less than,
    //     equal to, or greater than the other.
    //
    // Parameters:
    //   y:
    //     The second object to compare.
    //
    //   x:
    //     The first object to compare.
    //
    // Returns:
    //     Value Condition Less than zero x is less than y. Zero x equals y. Greater
    //     than zero x is greater than y.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     Neither x nor y implements the System.IComparable interface.-or- x and y
    //     are of different types and neither one can handle comparisons with the other.
    int Compare(object x, object y);
  }
}
