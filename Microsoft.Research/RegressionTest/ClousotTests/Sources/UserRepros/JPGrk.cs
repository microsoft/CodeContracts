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
using Microsoft.Research.ClousotRegression;
using System.Diagnostics.Contracts;


namespace Repro
{
  public struct Second
  {
    private double value;

    [ClousotRegressionTest]
 [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=24,MethodILOffset=0)]
 [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as receiver)",PrimaryILOffset=9,MethodILOffset=29)]
 [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"The arguments of the comparison have a compatible precision",PrimaryILOffset=15,MethodILOffset=29)]
 [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"ensures is valid",PrimaryILOffset=17,MethodILOffset=29)]
    public Second(double value)
    {
      Contract.Ensures(Contract.ValueAtReturn(out this).Value == value);
      this.value = value;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as receiver)",PrimaryILOffset=2,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as receiver)",PrimaryILOffset=37,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"The arguments of the comparison have a compatible precision",PrimaryILOffset=16,MethodILOffset=0)]
    public static implicit operator Hertz(Second a)
    {
      Contract.Requires(a.Value != 0d);
      return new Hertz(1d / a.Value);
    }

    [ClousotRegressionTest]
    public double Value
    {
      get
      {
        Contract.Ensures(Contract.Result<double>() == this.value);
        return this.value;
      }
    }
  }

  public struct Hertz
  {
    private double value;

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=24,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as receiver)",PrimaryILOffset=9,MethodILOffset=29)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"The arguments of the comparison have a compatible precision",PrimaryILOffset=15,MethodILOffset=29)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"ensures is valid",PrimaryILOffset=17,MethodILOffset=29)]
    public Hertz(double value)
    {
      Contract.Ensures(Contract.ValueAtReturn(out this).Value == value); // <- unproven
      this.value = value;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as receiver)",PrimaryILOffset=2,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as receiver)",PrimaryILOffset=37,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"The arguments of the comparison have a compatible precision",PrimaryILOffset=16,MethodILOffset=0)]
    public static implicit operator Second(Hertz a)
    {
      Contract.Requires(a.Value != 0d);
      return new Second(1d / a.Value);
    }

    [ClousotRegressionTest]
    public double Value
    {
      get
      {
        Contract.Ensures(Contract.Result<double>() == this.value);
        return value;
      }
    }
  }
}
