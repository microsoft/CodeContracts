// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics.Contracts;
using Microsoft.Research.ClousotRegression;

namespace RaphaelSchweizer
{
    internal class Program
    {
        private static void Main()
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
            [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 8, MethodILOffset = 0)]
            [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 8, MethodILOffset = 0)]
            [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 11, MethodILOffset = 0)]
            [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 11, MethodILOffset = 0)]
            [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 52, MethodILOffset = 31)]
            [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 77, MethodILOffset = 31)]
            public double Do(params object[] operands)
            {
                Console.Write(string.Format("We have {0} {1}s\n", operands[1], operands[0]));
                return 0;
            }
        }
    }
}
