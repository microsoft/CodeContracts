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

namespace System
{

  public class Random
  {
    virtual public void NextBytes(Byte[] buffer)
    {
      Contract.Requires(buffer != null);
    }

    virtual public double NextDouble()
    {
      Contract.Ensures(Contract.Result<double>() >= 0.0);
      Contract.Ensures(Contract.Result<double>() < 1.0);

      return default(double);
    }

    virtual public int Next(int maxValue)
    {
      Contract.Requires(maxValue >= 0);

      Contract.Ensures(Contract.Result<int>() >= 0);
      Contract.Ensures(Contract.Result<int>() < maxValue || maxValue == 0 && Contract.Result<int>() == 0);

      return default(int);
    }

    virtual public int Next(int minValue, int maxValue)
    {
      Contract.Requires(minValue <= maxValue);

      Contract.Ensures(Contract.Result<int>() >= minValue);
      Contract.Ensures(Contract.Result<int>() < maxValue || maxValue == minValue && Contract.Result<int>() == minValue);

      return default(int);
    }

    virtual public int Next()
    {
      Contract.Ensures(Contract.Result<int>() >= 0);

      return default(int);
    }

  }
}
