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

#define CONTRACTS_FULL

using System;
using System.Diagnostics.Contracts;
using Microsoft.Research.ClousotRegression;

namespace TestPostconditionInference
{
  public class TestRangesAreFiltered
  {
    [ClousotRegressionTest]
    public static int IntPostcondition(int x)
    {
      return x;
    }

    [ClousotRegressionTest]
    public static long LongPostcondition(long x)
    {
      return x;
    }

    [ClousotRegressionTest]
    public static UInt32 UInt32Postcondition(uint u)
    {
      return u;
    }

    [ClousotRegressionTest]
    public static UInt64 UInt32Postcondition(UInt64 u)
    {
      return u;
    }

    [ClousotRegressionTest]
    public static SByte SBytePostcondition(SByte s)
    {
      return s;
    }

    public long _field;

    [ClousotRegressionTest]
    public void SetField(long z)
    {
      this._field = z;
    }

  }
}