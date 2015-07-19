// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics.Contracts;
using Microsoft.Research.ClousotRegression;

internal class Obj
{
    public Obj() { }
    public extern int FooBar();
    public static extern Obj MakeObj();
    public int f;
    public int g;
}


internal class Test
{
#if !CLOUSOT2
    // check for persisting inferred assume across versions to maintain suppression
    [ClousotRegressionTest]
#if FIRST
    [RegressionOutcome("No entry found in the cache")]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"Possibly calling a method on a null reference 'b'", PrimaryILOffset = 1, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "assert unproven. Are you making some assumption on FooBar that the static checker is unaware of? ", PrimaryILOffset = 14, MethodILOffset = 0)]
    public int Test1(Obj b)
    {
        int tmp = b.FooBar();
        Contract.Assert(tmp >= 0);
        return 7;
    }

#else
    [RegressionOutcome("No entry found in the cache")] // can't find method by hash...
    [RegressionOutcome("Assumes have been found in the cache.")] // but do find assume's
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 1, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 20, MethodILOffset = 0)]
    public int Test1(Obj b)
    {
        int tmp = b.FooBar();
        int j = 0;
        j++;
        Contract.Assert(tmp >= 0);
        return 7;
    }
#endif

    // check that a new warning of the same type that should be reported is still reported
    [ClousotRegressionTest]
#if FIRST
    [RegressionOutcome("No entry found in the cache")]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "Possibly accessing a field on a null reference 'o2'. Are you making some assumption on MakeObj that the static checker is unaware of? ", PrimaryILOffset = 7, MethodILOffset = 0)]
    public int Test2(Obj o1, int j)
    {
        Obj o2 = Obj.MakeObj();
        return o2.f + j;
    }

#else
    [RegressionOutcome("No entry found in the cache")] // can't find method by hash...
    [RegressionOutcome("Assumes have been found in the cache.")] // but do find assume's
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as field receiver)", PrimaryILOffset = 13, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"Possibly accessing a field on a null reference 'o1'", PrimaryILOffset = 20, MethodILOffset = 0)]
    public int Test2(Obj o1, int j)
    {
        Obj o2 = Obj.MakeObj();
        j++;
        return j + o2.f + o1.g;
    }
#endif

    // check if multiple variables in the assume() are hanlded correctly
    [ClousotRegressionTest]
#if FIRST
    [RegressionOutcome("No entry found in the cache")]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "assert unproven. Are you making some assumption on Obj.FooBar() that the static checker is unaware of? ", PrimaryILOffset = 14, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "Possibly calling a method on a null reference 'b'", PrimaryILOffset = 1, MethodILOffset = 0)]
    public int Test3(Obj b, int i)
    {
        int tmp = b.FooBar();
        Contract.Assert(tmp >= i);
        return 7;
    }

#else
    [RegressionOutcome("No entry found in the cache")] // can't find method by hash...
    [RegressionOutcome("Assumes have been found in the cache.")] // but do find assume's
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 1, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 20, MethodILOffset = 0)]
    public int Test3(Obj b, int i)
    {
        int tmp = b.FooBar();
        int j = 0;
        j++;
        Contract.Assert(tmp >= i);
        return 7;
    }
#endif

    // see if using a parameter as a return value is handled correctly
    [ClousotRegressionTest]
#if FIRST
    [RegressionOutcome("No entry found in the cache")]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"Possibly calling a method on a null reference 'b'", PrimaryILOffset = 3, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "assert unproven. Are you making some assumption on FooBar that the static checker is unaware of? ", PrimaryILOffset = 17, MethodILOffset = 0)]
    public int Test4(Obj b, int i)
    {
        int tmp = 2;
        i = b.FooBar();
        Contract.Assert(tmp >= i);
        return 7;
    }

#else
    [RegressionOutcome("No entry found in the cache")] // can't find method by hash...
    [RegressionOutcome("Assumes have been found in the cache.")] // but do find assume's
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 3, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 23, MethodILOffset = 0)]
    public int Test4(Obj b, int i)
    {
        int tmp = 2;
        i = b.FooBar();
        int j = 0;
        j++;
        Contract.Assert(tmp >= i);
        return 7;
    }
#endif

    // see if using a parameter as a return value is handled correctly
    [ClousotRegressionTest]
    [RegressionOutcome("No entry found in the cache")]
#if FIRST
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "Possibly calling a method on a null reference 'b' (Fixing this warning may solve one additional issue in the code)", PrimaryILOffset = 7, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "Possibly calling a method on a null reference 'b'", PrimaryILOffset = 16, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "assert unproven. Are you making some assumption on FooBar that the static checker is unaware of? ", PrimaryILOffset = 29, MethodILOffset = 0)]
    public int Test5(Obj b, int i)
    {
        int tmp = 2;
        int x;
        if (i > 0)
        {
            x = b.FooBar();
        }
        else
        {
            x = b.FooBar();
        }
        Contract.Assert(tmp >= x);
        return 7;
    }

#else
    [RegressionOutcome("Assumes have been found in the cache.")] // but do find assume's
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 7, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 16, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 33, MethodILOffset = 0)]
    public int Test5(Obj b, int i)
    {
        int tmp = 2;
        int x;
        if (i > 0)
        {
            x = b.FooBar();
        }
        else
        {
            x = b.FooBar();
            x--;
        }
        Contract.Assert(tmp >= x);
        return 7;
    }
#endif

    [ContractVerification(false)]
    private static int UnknownFunction(int z)
    {
        return System.Environment.TickCount;
    }

    private int a;

    [ClousotRegressionTest]
    [RegressionOutcome("No entry found in the cache")]
#if FIRST
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "assert unproven. Are you making some assumption on UnknownFunction that the static checker is unaware of? ", PrimaryILOffset = 19, MethodILOffset = 0)]
#else
    [RegressionOutcome("Assumes have been found in the cache.")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 19, MethodILOffset = 0)]
#endif
    public void TestLocal()
    {
        var i = 0;

        var local = UnknownFunction(i);

        if (local < 100)
        {
            Contract.Assert(local > 10);
        }
    }

    [ClousotRegressionTest]
    [RegressionOutcome("No entry found in the cache")]
#if FIRST
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 9, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 15, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "assert unproven. Are you making some assumption on UnknownFunction that the static checker is unaware of? ", PrimaryILOffset = 31, MethodILOffset = 0)]
#else
    [RegressionOutcome("Assumes have been found in the cache.")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 9, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 15, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 31, MethodILOffset = 0)]
#endif
    public void TestField()
    {
        var i = 0;

        a = UnknownFunction(i);

        var local = a;

        if (local < 100)
        {
            Contract.Assert(local > 10);
        }
    }

    private int[] data;

    [ClousotRegressionTest]
    [RegressionOutcome("No entry found in the cache")]
#if FIRST
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 10, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 16, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as array)", PrimaryILOffset = 28, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 30, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as array)", PrimaryILOffset = 36, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Array creation : ok", PrimaryILOffset = 5, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 28, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 28, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 36, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 36, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "assert unproven. Are you making some assumption on UnknownFunction that the static checker is unaware of? ", PrimaryILOffset = 48, MethodILOffset = 0)]
#else
    [RegressionOutcome("Assumes have been found in the cache.")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 10, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 16, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as array)", PrimaryILOffset = 28, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 30, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as array)", PrimaryILOffset = 36, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Array creation : ok", PrimaryILOffset = 5, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 28, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 28, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 36, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 36, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 48, MethodILOffset = 0)]
#endif
    public void TestArray()
    {
        var i = 0;
        data = new int[10];

        data[5] = UnknownFunction(i);

        var local = data[5];

        if (local < 100)
        {
            Contract.Assert(local > 10);
        }
    }

    public void OtherFunc(int x)
    {
        Contract.Requires(x > 10);
    }

    [ClousotRegressionTest]
    [RegressionOutcome("No entry found in the cache")]
#if FIRST
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 9, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "requires unproven: x > 10", PrimaryILOffset = 5, MethodILOffset = 9)]
#else
    [RegressionOutcome("Assumes have been found in the cache.")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 9, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 5, MethodILOffset = 9)]
#endif
    public void TestChain()
    {
        var i = 0;

        OtherFunc(UnknownFunction(i));
    }

    [ContractVerification(false)]
    private static int RelationOnResult(out int[] array)
    {
        array = new int[10];
        return System.Environment.TickCount;
    }

    [ClousotRegressionTest]
    [RegressionOutcome("No entry found in the cache")]
#if FIRST
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "Possible use of a null array 'data'", PrimaryILOffset = 11, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "Array access might be below the lower bound. Are you making some assumption on RelationOnResult that the static checker is unaware of? ", PrimaryILOffset = 11, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "Array access might be above the upper bound. Are you making some assumption on Test.RelationOnResult(System.Int32[]@) that the static checker is unaware of? ", PrimaryILOffset = 11, MethodILOffset = 0)]
#else
    [RegressionOutcome("Assumes have been found in the cache.")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as array)", PrimaryILOffset = 11, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 11, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 11, MethodILOffset = 0)]
#endif
    public void TestRelation()
    {
        int[] data;
        var i = RelationOnResult(out data);

        data[i] = 0;
    }

#else
    // if CCI2
    // dummy test to prevent above tests from failing on CCI2
    [ClousotRegressionTest]
#if FIRST
    [RegressionOutcome("No entry found in the cache")] 
    public void Dummy() {

    }
#else
    [RegressionOutcome("An entry has been found in the cache")] 
    public void Dummy() {
     
    }
#endif
#endif
}
