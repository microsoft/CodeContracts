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
using Microsoft.Research.ClousotRegression;

namespace RaphaelSchweizer
{
  class Program
  {
    static void Main()
    {
      new OpA().Do("Item", 3);
    }

    [ContractClass(typeof(OperationConstraint))]
    public interface IOperation
    {
      Type[] Types { get; }
      double Do(params object[] operands);
    }

    [ContractClassFor(typeof(IOperation))]
    public abstract class OperationConstraint : IOperation
    {
      public Type[] Types
      {
        get
        {
          Contract.Ensures(Contract.Result<Type[]>() != null);
          return default(Type[]);
        }
      }

      public double Do(params object[] operands)
      {
        Contract.Requires(operands != null);
        Contract.Requires(operands.Length == Types.Length);
        Contract.Ensures(Contract.Result<double>() >= 0);
        Contract.Ensures(Contract.Result<double>() <= 1);
        return default(double);
      }
    }

    public class OpA : IOperation
    {
      public Type[] Types
      {
        get
        {
          Contract.Ensures(Contract.Result<Type[]>().Length == 2);
          return new[] { typeof(string), typeof(decimal) };
        }
      }

      [ClousotRegressionTest]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="Lower bound access ok",PrimaryILOffset=8,MethodILOffset=0)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="Upper bound access ok",PrimaryILOffset=8,MethodILOffset=0)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="Lower bound access ok",PrimaryILOffset=11,MethodILOffset=0)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="Upper bound access ok",PrimaryILOffset=11,MethodILOffset=0)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="ensures is valid",PrimaryILOffset=52,MethodILOffset=36)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="ensures is valid",PrimaryILOffset=77,MethodILOffset=36)]
      public double Do(params object[] operands)
      {
        Console.Write(string.Format("We have {0} {1}s\n", operands[1], operands[0]));
        return 0;
      }
    }
  }
}
