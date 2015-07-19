// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

#define CONTRACTS_FULL

using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using Microsoft.Research.ClousotRegression;

namespace EnumerablesNonNull
{
    public class EnumerablesBasic
    {
        // can't deal with assumption aggregation yet
        [ClousotRegressionTest]
        [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "Possibly calling a method on a null reference 'args'", PrimaryILOffset = 1, MethodILOffset = 0)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 29, MethodILOffset = 0)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 10, MethodILOffset = 0)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 49, MethodILOffset = 55)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 56, MethodILOffset = 0)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 87, MethodILOffset = 0)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 67, MethodILOffset = 0)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 111, MethodILOffset = 117)]
        [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "assert unproven", PrimaryILOffset = 80, MethodILOffset = 0)]
        public static void Start(IEnumerable args)
        {
            foreach (var arg in args)
            {
                Contract.Assume(arg != null);
            }

            foreach (var arg in args)
            {
                Contract.Assert(arg != null);
            }
        }
    }

    public class AssumeForAll
    {
        [ClousotRegressionTest]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 53, MethodILOffset = 0)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 81, MethodILOffset = 0)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 62, MethodILOffset = 0)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 94, MethodILOffset = 100)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 75, MethodILOffset = 0)]
        public static void NonNull_OK(System.Collections.Generic.IEnumerable<object> s)
        {
            Contract.Requires(s != null);
            Contract.Requires(Contract.ForAll(s, arg => arg != null));

            foreach (var arg in s)
            {
                Contract.Assert(arg != null);
            }
        }

        [ClousotRegressionTest]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 53, MethodILOffset = 0)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 83, MethodILOffset = 0)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 63, MethodILOffset = 0)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 76, MethodILOffset = 0)]
        public void NonNullList(System.Collections.Generic.List<string> xs)
        {
            Contract.Requires(xs != null);
            Contract.Requires(Contract.ForAll(xs, i => i != null));

            foreach (var x in xs)
            {
                Contract.Assert(x != null);
            }
        }

        [ClousotRegressionTest]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 53, MethodILOffset = 0)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 81, MethodILOffset = 0)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 62, MethodILOffset = 0)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 94, MethodILOffset = 100)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 75, MethodILOffset = 0)]
        public void NonNullCollection(System.Collections.Generic.ICollection<string> xs)
        {
            Contract.Requires(xs != null);
            Contract.Requires(Contract.ForAll(xs, i => i != null));

            foreach (var x in xs)
            {
                Contract.Assert(x != null);
            }
        }

        [ClousotRegressionTest]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 53, MethodILOffset = 0)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 69, MethodILOffset = 0)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 76, MethodILOffset = 0)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 13, MethodILOffset = 69)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 33, MethodILOffset = 69)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 13, MethodILOffset = 76)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 33, MethodILOffset = 76)]
        [RegressionOutcome(Outcome = ProofOutcome.False, Message = "assert is false", PrimaryILOffset = 87, MethodILOffset = 0)]
        public void NonNullListAssigned1(System.Collections.Generic.List<string> xs)
        {
            Contract.Requires(xs != null);
            Contract.Requires(Contract.ForAll(xs, i => i != null));
            Contract.Requires(xs.Count > 5);

            xs[3] = null;

            Contract.Assert(xs[3] != null); // must be false
        }

        [ClousotRegressionTest]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 53, MethodILOffset = 0)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 69, MethodILOffset = 0)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 13, MethodILOffset = 69)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 33, MethodILOffset = 69)]
#if NETFRAMEWORK_4_0
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="requires is valid",PrimaryILOffset=17,MethodILOffset=104)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="requires is valid",PrimaryILOffset=39,MethodILOffset=104)]
#else
#if CLOUSOT2
		[RegressionOutcome(Outcome=ProofOutcome.True,Message="requires is valid",PrimaryILOffset=3,MethodILOffset=104)]
		[RegressionOutcome(Outcome=ProofOutcome.True,Message="requires is valid",PrimaryILOffset=25,MethodILOffset=104)]
#else
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 22, MethodILOffset = 104)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 44, MethodILOffset = 104)]
#endif
#endif
        [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "assert unproven", PrimaryILOffset = 109, MethodILOffset = 0)]
        public void NonNullListAssigned2(System.Collections.Generic.List<string> xs)
        {
            Contract.Requires(xs != null);
            Contract.Requires(Contract.ForAll(xs, i => i != null));
            Contract.Requires(xs.Count > 5);

            xs[3] = null;

            Contract.Assert(Contract.ForAll(xs, i => i != null)); // must be unproven
        }

        [ClousotRegressionTest]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 53, MethodILOffset = 0)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 69, MethodILOffset = 0)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 75, MethodILOffset = 0)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 105, MethodILOffset = 0)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 85, MethodILOffset = 0)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 13, MethodILOffset = 69)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 33, MethodILOffset = 69)]
        [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "assert unproven", PrimaryILOffset = 98, MethodILOffset = 0)]
        public void NonNullListAssigned3(System.Collections.Generic.List<string> xs)
        {
            Contract.Requires(xs != null);
            Contract.Requires(Contract.ForAll(xs, i => i != null));
            Contract.Requires(xs.Count > 5);

            xs[3] = null;

            foreach (var x in xs)
            {
                Contract.Assert(x != null); // must be unproven
            }
        }

        [ClousotRegressionTest]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 36, MethodILOffset = 0)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 69, MethodILOffset = 0)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 45, MethodILOffset = 0)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 82, MethodILOffset = 88)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 63, MethodILOffset = 0)]
        public static void NonNullGeneric_OK<T>(System.Collections.Generic.IEnumerable<T> s) where T : class
        {
            Contract.Requires(s != null);
            Contract.Requires(Contract.ForAll(s, arg => arg != null));

            foreach (var arg in s)
            {
                Contract.Assert(arg != null);
            }
        }

        [ClousotRegressionTest]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 53, MethodILOffset = 0)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 78, MethodILOffset = 0)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 62, MethodILOffset = 0)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 91, MethodILOffset = 97)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 72, MethodILOffset = 0)]
        // Now it passes, thanks to the improved handling of box instructions
        public static void Positive_OK(System.Collections.Generic.IEnumerable<int> s)
        {
            Contract.Requires(s != null);
            Contract.Requires(Contract.ForAll(s, arg => arg > 0));

            foreach (var arg in s)
            {
                Contract.Assert(arg > 0);
            }
        }
    }

    public class AssertForAll
    {
        [ClousotRegressionTest]
#if NETFRAMEWORK_4_0
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="requires unproven: collection != null (collection)",PrimaryILOffset=17,MethodILOffset=36)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="requires is valid",PrimaryILOffset=39,MethodILOffset=36)]
#else
#if CLOUSOT2
		[RegressionOutcome(Outcome=ProofOutcome.Top,Message="requires unproven",PrimaryILOffset=3,MethodILOffset=36)]
		[RegressionOutcome(Outcome=ProofOutcome.True,Message="requires is valid",PrimaryILOffset=25,MethodILOffset=36)]
#else
        [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "requires unproven", PrimaryILOffset = 22, MethodILOffset = 36)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 44, MethodILOffset = 36)]
#endif
#endif
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 41, MethodILOffset = 0)]
        public void NotNullGeneric<T>(System.Collections.Generic.IEnumerable<T> s) where T : class
        {
            Contract.Requires(Contract.ForAll(s, arg => arg != null));

            Contract.Assert(Contract.ForAll(s, arg => arg != null));
        }

        [ClousotRegressionTest]
#if NETFRAMEWORK_4_0
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="requires unproven: collection != null (collection)",PrimaryILOffset=17,MethodILOffset=70)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="requires is valid",PrimaryILOffset=39,MethodILOffset=70)]
#else
#if CLOUSOT2
		[RegressionOutcome(Outcome=ProofOutcome.Top,Message="requires unproven",PrimaryILOffset=3,MethodILOffset=70)]
		[RegressionOutcome(Outcome=ProofOutcome.True,Message="requires is valid",PrimaryILOffset=25,MethodILOffset=70)]
#else
        [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "requires unproven", PrimaryILOffset = 22, MethodILOffset = 70)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 44, MethodILOffset = 70)]
#endif
#endif
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 75, MethodILOffset = 0)]
        public void NotNull(System.Collections.Generic.IEnumerable<string> s)
        {
            Contract.Requires(Contract.ForAll(s, arg => arg != null));

            Contract.Assert(Contract.ForAll(s, arg => arg != null));
        }

        [ClousotRegressionTest]
#if NETFRAMEWORK_4_0
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="requires unproven: collection != null (collection)",PrimaryILOffset=17,MethodILOffset=70)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="requires is valid",PrimaryILOffset=39,MethodILOffset=70)]
#else
#if CLOUSOT2
		[RegressionOutcome(Outcome=ProofOutcome.Top,Message="requires unproven",PrimaryILOffset=3,MethodILOffset=70)]
		[RegressionOutcome(Outcome=ProofOutcome.True,Message="requires is valid",PrimaryILOffset=25,MethodILOffset=70)]
#else
        [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "requires unproven", PrimaryILOffset = 22, MethodILOffset = 70)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 44, MethodILOffset = 70)]
#endif
#endif
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 75, MethodILOffset = 0)]
        public void Positive(System.Collections.Generic.IEnumerable<int> s)
        {
            Contract.Requires(Contract.ForAll(s, arg => arg > 0));

            Contract.Assert(Contract.ForAll(s, arg => arg > 0));
        }
    }
}

namespace Repros
{
    public class ArrayLength
    {
        [ClousotRegressionTest]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as field receiver)", PrimaryILOffset = 8, MethodILOffset = 0)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as field receiver)", PrimaryILOffset = 15, MethodILOffset = 0)]
        [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"Possible use of a null array 'xs'", PrimaryILOffset = 20, MethodILOffset = 0)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as field receiver)", PrimaryILOffset = 8, MethodILOffset = 0)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as field receiver)", PrimaryILOffset = 45, MethodILOffset = 0)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as array)", PrimaryILOffset = 50, MethodILOffset = 0)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as field receiver)", PrimaryILOffset = 87, MethodILOffset = 0)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as array)", PrimaryILOffset = 92, MethodILOffset = 0)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as field receiver)", PrimaryILOffset = 68, MethodILOffset = 0)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as array)", PrimaryILOffset = 74, MethodILOffset = 0)]
        [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"assert unproven", PrimaryILOffset = 112, MethodILOffset = 0)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 7, MethodILOffset = 75)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 100, MethodILOffset = 0)]
        [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"assert unproven. Is it an off-by-one? The static checker can prove xs.Length > (0 - 1) instead", PrimaryILOffset = 55, MethodILOffset = 0)]
        public static object JoinAll(object[] xs)
        {
            Contract.Requires(Contract.ForAll(0, xs.Length, j => xs[j] != null));

            Contract.Assert(xs.Length > 0); // It's not always true...

            object result = null;

            int i;
            for (i = 0; i < xs.Length; i++)
            {
                result = Join(result, xs[i]);
            }

            Contract.Assert(i > 0);  // We need wp to prove it

            Contract.Assert(result != null); // Ok

            return result;
        }

        [ContractVerification(false)]
        public static object Join(object x, object y)
        {
            Contract.Requires(y != null);
            Contract.Ensures(Contract.Result<object>() != null);

            return y;
        }
    }
}