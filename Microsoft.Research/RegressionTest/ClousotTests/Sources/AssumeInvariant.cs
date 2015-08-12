// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using Microsoft.Research.ClousotRegression;

namespace AssumeInvariant
{
    internal class C
    {
        public int field;

        [ContractInvariantMethod]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Required for code contracts.")]
        private void ObjectInvariant()
        {
            Contract.Invariant(field > 0);
        }

        public C()
        {
            field = 1;
        }
    }

    internal class Test
    {
        [Pure]
        private static void AssumeInvariant<T>(T o) { }

        private static void Main(string[] args)
        {
            var p = new C();

            TestMe1(p);
            TestMe2(p);
        }

        [ClousotRegressionTest]
        [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "assert unproven", PrimaryILOffset = 10, MethodILOffset = 0)]
        private static void TestMe1(C p)
        {
            Contract.Assert(p.field > 0);
        }

        [ClousotRegressionTest]
        private static void TestMe2(C p)
        {
            AssumeInvariant(p);

            Contract.Assert(p.field > 0);
        }
    }
}

namespace AssumeInvariantOldIssue
{
    using System.Collections;

    public class Host
    {
        public string Name = "";

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(Name != null);
        }
    }

    internal class InvariantAtCallAndOldHandling
    {
        public static class ContractHelpers
        {
            [ContractVerification(false)]
            public static void AssumeInvariant<T>(T o)
            {
            }
        }

        [ClousotRegressionTest]
        private static void AssumeInvariantTrue()
        {
            foreach (Host h in new ArrayList())
            {
                Contract.Assume(h != null);

                ContractHelpers.AssumeInvariant(h);

                Contract.Assert(h.Name != null);
            }
        }

        [ClousotRegressionTest]
        [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "assert unproven", PrimaryILOffset = 53, MethodILOffset = 0)]
        private static void AssumeInvariantUnproven()
        {
            foreach (Host h in new ArrayList())
            {
                Contract.Assume(h != null);

                Contract.Assert(h.Name != null);
            }
        }
    }
}
