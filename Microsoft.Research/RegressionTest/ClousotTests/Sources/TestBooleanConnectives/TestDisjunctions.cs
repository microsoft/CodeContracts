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

namespace BasicTests
{
  public class Disjunctions
  {
    public int[] Arr(int[] a, int len)
    {
      Contract.Requires(a != null);
      Contract.Requires(len >= 0);

      Contract.Ensures(a.Length != len || Contract.Result<int[]>().Length == a.Length);
      Contract.Ensures(a.Length == len || Contract.Result<int[]>().Length == len);

      if (a.Length == len)
        return a;
      else
        return new int[len];
    }

    // ok
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"Array creation : ok", PrimaryILOffset = 2, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as receiver)", PrimaryILOffset = 11, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message="valid non-null reference (as array)", PrimaryILOffset=18, MethodILOffset=0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 7, MethodILOffset = 11)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 19, MethodILOffset = 11)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 23, MethodILOffset = 0)]
    public void Use1()
    {
      var a = new int[10];

      var call = Arr(a, 3);
      // Simplify the postconditions to call.Length == 3

      Contract.Assert(call.Length == 3);
    }

    // ok
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"Array creation : ok", PrimaryILOffset = 22, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as receiver)", PrimaryILOffset = 31, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"Possible use of a null array 'call'", PrimaryILOffset = 38, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 7, MethodILOffset = 31)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 19, MethodILOffset = 31)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 43, MethodILOffset = 0)]
    public void Use2(int len, int newSize)
    {
      Contract.Requires(newSize >= 0);
      Contract.Requires(len > newSize);

      var a = new int[len];

      var call = Arr(a, newSize);
      // Simplify the postconditions to call.Length == newSize

      Contract.Assert(call.Length == newSize);
    }

    // ok
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"Array creation : ok", PrimaryILOffset = 22, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as receiver)", PrimaryILOffset = 31, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"Possible use of a null array 'call'", PrimaryILOffset = 38, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 7, MethodILOffset = 31)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 19, MethodILOffset = 31)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 43, MethodILOffset = 0)]
    public void Use3(int len, int newSize)
    {
      Contract.Requires(len >= 0);
      Contract.Requires(newSize > len);

      var a = new int[len];

      var call = Arr(a, newSize);
      // Simplify the postconditions to call.Length == newSize

      Contract.Assert(call.Length == newSize);
    }

    // ok
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"Array creation : ok", PrimaryILOffset = 25, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as receiver)", PrimaryILOffset = 34, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"Possible use of a null array 'call'", PrimaryILOffset = 41, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 7, MethodILOffset = 34)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 19, MethodILOffset = 34)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"assert unproven", PrimaryILOffset = 46, MethodILOffset = 0)]
    public void Use4(int len, int newSize)
    {
      Contract.Requires(len >= 0);
      Contract.Requires(newSize >= 0);

      var a = new int[len];

      var call = Arr(a, newSize);
      // Cannot simplify the postcondition

      Contract.Assert(call.Length == newSize);
    }
  }

  public class StateMachine
  {
    public enum States { A, B, C }

    public States State;

    private void RotateState()
    {
      Contract.Ensures(Contract.OldValue(State) != States.A || State == States.B);
      Contract.Ensures(Contract.OldValue(State) != States.B || State == States.C);
      Contract.Ensures(Contract.OldValue(State) != States.C || State == States.A);

      // bla bla
    }

    // Ok
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as field receiver)", PrimaryILOffset = 2, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as receiver)", PrimaryILOffset = 8, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as field receiver)", PrimaryILOffset = 14, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 22, MethodILOffset = 0)]
    public void TestOK()
    {
      this.State = States.A;
      RotateState();

      Contract.Assert(this.State == States.B);
    }

    // ok
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as field receiver)", PrimaryILOffset = 2, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as receiver)", PrimaryILOffset = 8, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as field receiver)", PrimaryILOffset = 14, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.False, Message = @"assert is false", PrimaryILOffset = 22, MethodILOffset = 0)]
    public void TestNOTOK()
    {
      this.State = States.A;
      RotateState();

      Contract.Assert(this.State == States.C);
    }
  }

  public class LoopExample
  {
    public int RandomValue(int x)
    {
      Contract.Ensures(x <= 123 || Contract.Result<int>() >= 0);
      //Contract.Ensures(x > 123 || Contract.Result<int>() <= 0);

      if (x > 123)
        return 134;
      else
        return -12;
    }

    // ok
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as receiver)",PrimaryILOffset=21,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=46,MethodILOffset=0)]
    public void Loop(int seed, int counts)
    {
      Contract.Requires(seed > 200);

      var sum = 0;
      for (int i = 0; i < counts; i++)
      {
        var x = RandomValue(seed);
        // We refine the postcondition to "seed > 123 ==> x >= 0", hence x >= 0 by modus ponens

        sum += x;
      }

      Contract.Assert(sum >= 0);
    }
  }

  public class String
  {
    [Pure]
    private static bool IsHexDigit(char c)
    {
      Contract.Ensures(Contract.Result<bool>() == ('0' <= c && c <= '9' || 'A' <= c && c <= 'F' || 'a' <= c && c <= 'f'));

      return '0' <= c && c <= '9' || 'A' <= c && c <= 'F' || 'a' <= c && c <= 'f';
    }

    // F: TODO once we will have disjunctions of intervals
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"assert unproven. The static checker determined that the condition 'c != 36' should hold on entry. Nevertheless, the condition may be too strong for the callers. If you think it is ok, add a precondition to document it: Contract.Requires(c != 36);", PrimaryILOffset = 16, MethodILOffset = 0)]
    public static void TestIsHexDigit(char c)
    {
      if(IsHexDigit(c))
      {
        Contract.Assert(c != '$');
      }
    }
  }
 }

namespace RefinementOfRequires
{
  class SimpleTest
{
    [ClousotRegressionTest]
#if CLOUSOT2
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="method Requires (including invariants) are unsatisfiable: RefinementOfRequires.SimpleTest.Foo(System.Int32)",PrimaryILOffset=26,MethodILOffset=0)] 
#else
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"method Requires (including invariants) are unsatisfiable: RefinementOfRequires.SimpleTest.Foo(System.Int32)",PrimaryILOffset=27,MethodILOffset=0)]
#endif
    public void Foo(int state)
    {
      Contract.Requires(state == -3 || state == 1); 
      Contract.Requires(state == 2);
 
     Contract.Assert(false); // Unreachable
    }
}
}

namespace UserRepro
{
  public class AlexeyR
{
  static bool SimpleDisjunction(out int x)
  {
    Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out x) > 100);

    x = 123;
    return true;
  }

  [ClousotRegressionTest]
  [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=14,MethodILOffset=0)]
  static void UseSimpledisjunction(int z)
  {
    if (SimpleDisjunction(out z))
    {
      Contract.Assert(z > 100);
    }
  }

  [ClousotRegressionTest]
  [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as receiver)",PrimaryILOffset=7,MethodILOffset=0)]  
  [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=41,MethodILOffset=0)]
  [RegressionOutcome(Outcome=ProofOutcome.True,Message="requires is valid",PrimaryILOffset=13,MethodILOffset=7)]
  static void Test(/*out*/ int index)
  {
    int count = new Random().Next(100);
    if (count < 2)
      count = 2;

    index = 0;
    if (GetIndex(1, count - 2, out index))
    {
      Contract.Assert(index < count - 1); // proven, needs modus ponens and subpolyhedra
    }
  }

  static bool GetIndex(int start, int count, out int index)
  {
    Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out index) < start + count);
    index = start;
    return false;
  }

  [ClousotRegressionTest]
  [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as receiver)",PrimaryILOffset=7,MethodILOffset=0)]
  [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=7,MethodILOffset=28)]
  [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=19,MethodILOffset=28)]
#if CLOUSOT2
	// In clousot2, we need the -wp=true option to prove the precondition. In Clousot1, we have slightly different IL which allows us to prove it without using WP
   [RegressionOutcome(Outcome=ProofOutcome.Top,Message="requires unproven: count <= 100",PrimaryILOffset=32,MethodILOffset=28)]
#else
   [RegressionOutcome(Outcome=ProofOutcome.True,Message="requires is valid",PrimaryILOffset=32,MethodILOffset=28)]
#endif
  [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=41,MethodILOffset=0)]
  [RegressionOutcome(Outcome=ProofOutcome.True,Message="requires is valid",PrimaryILOffset=13,MethodILOffset=7)]
  static void Test2(/*out*/ int index)
  {
    int count = new Random().Next(100);
    if (count < 2)
      count = 2;

    index = 0;
    if (GetIndex2(1, count - 2, out index))
    {
      Contract.Assert(index < count - 1); // proven: needs modus ponens, simplification and subpolyhedra
    }
  }

  static bool GetIndex2(int start, int count, out int index)
  {
    
    Contract.Requires(start >= 0);
    Contract.Requires(count >= 0);
    Contract.Requires(count <= 100);

    Contract.Ensures(Contract.ValueAtReturn(out index) >= start);
    Contract.Ensures(Contract.ValueAtReturn(out index) <= start + count);
    
    Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out index) < start + count);
    index = start;
    return false;
  }
  }

  public class ForAllTest
  {
	
	[ClousotRegressionTest]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="Lower bound access ok",PrimaryILOffset=77,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="Upper bound access ok",PrimaryILOffset=77,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=8,MethodILOffset=0)]
#if !CLOUSOT2
		[RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=14,MethodILOffset=0)]
		[RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=23,MethodILOffset=0)]
#endif
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as array)",PrimaryILOffset=28,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=58,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=91,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as array)",PrimaryILOffset=96,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=71,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as array)",PrimaryILOffset=77,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=78,MethodILOffset=0)]
	public int SumLengths(string[] input)
    {
      Contract.Requires(input == null || Contract.ForAll(0, input.Length, j => input[j] != null));

      var c = 0;
      if (input != null)
      {
        for (var i = 0; i < input.Length; i++)
        {
          c += input[i].Length;
        }
      }

      return c;
    }
  }
}
