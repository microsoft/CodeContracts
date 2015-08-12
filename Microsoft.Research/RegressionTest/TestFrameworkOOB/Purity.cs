// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Research.ClousotRegression;

namespace TestFrameworkOOB.Purity
{
    internal class Tests
    {
        [ClousotRegressionTest]
        public static void Test(object a, object b)
        {
            Contract.Requires(Object.ReferenceEquals(a, b));
        }

        [ClousotRegressionTest]
        [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "Possibly calling a method on a null reference \'dict\'", PrimaryILOffset = 3, MethodILOffset = 0), RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 16, MethodILOffset = 0)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 21, MethodILOffset = 0)]
        public static void Test(IDictionary<int, string> dict, int key)
        {
            Contract.Requires(dict.ContainsKey(key));

            Contract.Assert(dict.ContainsKey(key));
        }
    }

    internal interface J { }

    internal class TypeMethodPurity : J
    {
        private void Get(Type messageType)
        {
            Contract.Requires(messageType != null && typeof(J).IsAssignableFrom(messageType));
        }

        private void Foo()
        {
            J message = new TypeMethodPurity();
            Type t = message.GetType();
            Contract.Assert(t != null);
            Contract.Assume(t == typeof(TypeMethodPurity));
            Contract.Assume(typeof(J).IsAssignableFrom(typeof(TypeMethodPurity)));
            Contract.Assume(typeof(J).IsAssignableFrom(t));
        }

        [ClousotRegressionTest]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 25, MethodILOffset = 0)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 47, MethodILOffset = 0)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 52, MethodILOffset = 0)]
        private void Bar(Type t)
        {
            Contract.Requires(t != null);
            Contract.Requires(typeof(J).IsAssignableFrom(t));

            Contract.Assert(typeof(J).IsAssignableFrom(t));
        }
    }
}
