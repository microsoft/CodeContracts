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
using System.Diagnostics.Contracts;

namespace System {
  // Summary:
  //     Defines a generalized comparison method that a value type or class implements
  //     to create a type-specific comparison method for ordering instances.
  public interface IComparable<T> {
    // Summary:
    //     Compares the current object with another object of the same type.
    //
    // Parameters:
    //   other:
    //     An object to compare with this object.
    //
    // Returns:
    //     A 32-bit signed integer that indicates the relative order of the objects
    //     being compared. The return value has the following meanings: Value Meaning
    //     Less than zero This object is less than the other parameter.Zero This object
    //     is equal to other. Greater than zero This object is greater than other. 
    [Pure][Reads(ReadsAttribute.Reads.Owned)]
    int CompareTo(T other);
  }
}
