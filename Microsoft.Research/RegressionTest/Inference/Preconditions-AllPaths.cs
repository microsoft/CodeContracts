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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using Microsoft.Research.ClousotRegression;

namespace PreInference
{
  class SimpleTests
  { 
    [ClousotRegressionTest]
    [RegressionOutcome("Contract.Requires(0 <= x);")]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"The length of the array may be negative",PrimaryILOffset=1,MethodILOffset=0)]
    public int[] GTZero(int x)
    {
      // Should infer x >= 0

      return new int[x];
    }

    [ClousotRegressionTest]
    [RegressionOutcome("Contract.Requires(s != null);")]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possibly calling a method on a null reference 's'",PrimaryILOffset=1,MethodILOffset=0)]
    public int NotNull(string s)
    {
      // Should infer s != null
      
      return s.Length;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Possibly calling a method on a null reference 's'. The static checker determined that the condition 's != null' should hold on entry. Nevertheless, the condition may be too strong for the callers. If you think it is ok, add a precondition to document it: Contract.Requires(s != null);",PrimaryILOffset=4,MethodILOffset=0)]
    public int NotNull_NoPre(bool cond, string s)
    {
      // should not infer s != null
      // It may infer !(cond) || s with a different inference algorithm

      if (cond)
      {
        return s.Length;
      }
      return -1;
    }

    // F: TODO - it's printing twice the precondition
    [ClousotRegressionTest]
    [RegressionOutcome("Contract.Requires(s != null);")]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Possibly calling a method on a null reference 's' (Fixing this warning may solve one additional issue in the code)",PrimaryILOffset=4,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Possibly calling a method on a null reference 's'",PrimaryILOffset=11,MethodILOffset=0)]
    public int NotNull_WithPre(bool cond, string s)
    {
      // Should infer s != null
      if (cond)
      {
        return s.Length;
      }
      return s.Length - 2; 
    }


    [ClousotRegressionTest]
    [RegressionOutcome("Contract.Requires(arr != null);")]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Possible use of a null array 'arr'",PrimaryILOffset=4,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=14,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Lower bound access ok",PrimaryILOffset=14,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Upper bound access ok",PrimaryILOffset=14,MethodILOffset=0)]
    public int CountNonZero(int[] arr)
    {
      // should infer arr != null
      var i = 0;
      while (i < arr.Length && arr[i++] != 0)
      {
      }
      return i;
    }

    [ClousotRegressionTest]
    [RegressionOutcome("Contract.Requires(arr != null);")]
    [RegressionOutcome("Contract.Requires(2 < arr.Length);")]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Possible use of a null array 'arr'",PrimaryILOffset=3,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=7,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=11,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Lower bound access ok",PrimaryILOffset=3,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Array access might be above the upper bound",PrimaryILOffset=3,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Lower bound access ok",PrimaryILOffset=7,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Array access might be above the upper bound",PrimaryILOffset=7,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Lower bound access ok",PrimaryILOffset=11,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Array access might be above the upper bound",PrimaryILOffset=11,MethodILOffset=0)]
    public void SetMany(byte[] arr)
    {
      // Should infer arr != null
      // Should infer 2 < arr.Length
      // and nothing else 
      arr[0] = 0;
      arr[1] = 1;
      arr[2] = 2;
    }

    [ClousotRegressionTest]
    [RegressionOutcome("Contract.Requires(arr != null);")]
    [RegressionOutcome("Contract.Requires(0 <= offset);")]
    [RegressionOutcome("Contract.Requires((offset + 2) < arr.Length);")] 
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Possible use of a null array 'arr'",PrimaryILOffset=3,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=9,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=15,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Array access might be below the lower bound",PrimaryILOffset=3,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Array access might be above the upper bound",PrimaryILOffset=3,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Lower bound access ok",PrimaryILOffset=9,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Array access might be above the upper bound",PrimaryILOffset=9,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Lower bound access ok",PrimaryILOffset=15,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Array access might be above the upper bound",PrimaryILOffset=15,MethodILOffset=0)]
    public void SetManySymbolic(byte[] arr, int offset)
    {
      // Should infer arr != null
      // Should infer offset >= 0
      // Should infer offset +2 < arr.Length
      // and nothing else
      arr[offset] = 0;
      arr[offset + 1] = 1;
      arr[offset + 2] = 2;
    }

    public int z; // dummy field to make the C# compiler happy

    [ClousotRegressionTest]
    [RegressionOutcome("Contract.Requires(0 <= x);")]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=6,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=16,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"The length of the array may be negative",PrimaryILOffset=22,MethodILOffset=0)]
    public int[] GtZeroAfterCondition(bool b, int x)
    {
      // Should infer x >= 0

      // With Forward propagation we get x >= 0, and prove it is checked in each path

      if (b)
      {
        z = 122;
      }
      else
      {
        z = -123;
      }
      return new int[x];
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"The length of the array may be negative. The static checker determined that the condition '0 <= x' should hold on entry. Nevertheless, the condition may be too strong for the callers. If you think it is ok, add a precondition to document it: Contract.Requires(0 <= x);",PrimaryILOffset=4,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"The length of the array may be negative. The static checker determined that the condition '0 <= (x + 1)' should hold on entry. Nevertheless, the condition may be too strong for the callers. If you think it is ok, add a precondition to document it: Contract.Requires(0 <= (x + 1));",PrimaryILOffset=13,MethodILOffset=0)]
    public int[] GTZeroInConditional(bool b, int x)
    {
      // Admissible preconditions are  x >= -1 or (b ==> x >= 0 && !b ==> x >= -1)
      // However, the preconditions all over the paths do not infer it (because it does syntactic propagation)
      if (b)
      {
        return new int[x];
      }
      else
      {
        return new int[x + 1];
      }
    }

    [ClousotRegressionTest]
    [RegressionOutcome("Contract.Requires(objs != null);")]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Possible use of a null array 'objs'",PrimaryILOffset=9,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Possibly calling a method on a null reference",PrimaryILOffset=10,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=28,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Lower bound access ok",PrimaryILOffset=9,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Array access might be above the upper bound",PrimaryILOffset=9,MethodILOffset=0)]
    public int NotNullCheckedInLoop(object[] objs)
    {
      // Should infer objs != null

      var len = 0;
      for (var i = 0; i < 100; i++)
      {
        len += objs[i].GetHashCode();
      }
      len += objs.Length;

      return len;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"assert unproven",PrimaryILOffset=6,MethodILOffset=0)]
    public void AssertInsideWhileLoop(int x)
    {
      // precondition should be Contract.Requires(x == 0 || x >= 0);
      // but this is out of scope of the preconditions all over the paths
      while (x != 0)
      {
        Contract.Assert(x > 0);
        x--;
      }
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"assert unproven",PrimaryILOffset=21,MethodILOffset=0)]
    public int[] InsideWhileLoop(int x)
    {
      Contract.Requires(x >= 0);

      // Should infer x <= 100, but this is out of the capabilities of the preconditions all over the paths

      int i;
      for (i = 0; i < x; i++)
      {
        Contract.Assert(i < 100);
      }

      return null;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"assert unproven. The static checker determined that the condition 'x == 12' should hold on entry. Nevertheless, the condition may be too strong for the callers. If you think it is ok, add a precondition to document it: Contract.Requires(x == 12);",PrimaryILOffset=19,MethodILOffset=0)]
    public void AfterWhileLoop_ConditionAlwaysTrue(int x, int z)
    {
      // should infer x == 12, but this is out of the reach of the preconditions all over the paths
      // as it does not use the information from the numerical state that z != 0 is unreached

      while (z > 0) z--;
      // here z == 0;

      if (z == 0)
      {
        Contract.Assert(x == 12);
      }
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"The length of the array may be negative",PrimaryILOffset=36,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=28,MethodILOffset=0)]
    public int[] AfterWhileLoop_Symbolic(int x)
    {
      Contract.Requires(x >= 0);

      // should infer x >= 1, but we do not use the numerical information

      int i;
      for (i = 0; i < x; i++)
      {
        // empty
      }
      // here we know that i == x

      Contract.Assert(i == x);

      return new int[i - 1]; // cannot be read in pre-state immediately
    }

    [ClousotRegressionTest]
    [RegressionOutcome("Contract.Requires(strs != null);")]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Possible use of a null array 'strs'",PrimaryILOffset=9,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Array access might be below the lower bound",PrimaryILOffset=9,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Array access might be above the upper bound",PrimaryILOffset=9,MethodILOffset=0)]
    public int SearchZero(string[] strs, int start)
   {
     // should infer strs != null
     // Those are out of the reach of the algorithm
      // should infer start >= 0
      // should infer start < strs.Length
      // should not infer any more as we are not doing WPs

      while (strs[start] != null)
      {
        start++;
      }

      return start;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=38,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=18,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Lower bound access ok",PrimaryILOffset=18,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Upper bound access ok",PrimaryILOffset=18,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"assert unproven",PrimaryILOffset=27,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"assert unproven",PrimaryILOffset=77,MethodILOffset=0)]
#if NETFRAMEWORK_4_0
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=17,MethodILOffset=72)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=39,MethodILOffset=72)]
#else
#if CLOUSOT2
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=3,MethodILOffset=72)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=25,MethodILOffset=72)]
#else
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=22,MethodILOffset=72)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=44,MethodILOffset=72)]
#endif
#endif
    public string InferExists(string[] s)
    {
      Contract.Requires(s != null);

      // Should we infer Exists i. s[i] != null ?? Surely not with the preconditions all over the paths algorithm

      for(var i = 0; i < s.Length; i++)
      {
        var x = s[i];

        Contract.Assert(x != null);

       //if (x != null)
       //   return x;
      }

      Contract.Assert(Contract.ForAll(s, el => el != null));

      //Contract.Assert(false);
      return null;
    }

  }

  public static class BoxedExpressionSimplification
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"assert unproven",PrimaryILOffset=7,MethodILOffset=0)]
    [RegressionOutcome("Contract.Requires(x != null);")]
    static public void AssertNotNull(object x)
    {
      Contract.Assert(x != null);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"assert unproven",PrimaryILOffset=4,MethodILOffset=0)]
    [RegressionOutcome("Contract.Requires(x == null);")]
    static public void AssertNull(object x)
    {
      Contract.Assert(x == null);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"assert unproven",PrimaryILOffset=4,MethodILOffset=0)]
    [RegressionOutcome("Contract.Requires(x > 0);")]
    static public void AssertGTZero(int x)
    {
      Contract.Assert(x > 0);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"assert unproven",PrimaryILOffset=4,MethodILOffset=0)]
    [RegressionOutcome("Contract.Requires(x < 0);")]
    static public void AssertLTZero(int x)
    {
      Contract.Assert(x < 0);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"assert unproven",PrimaryILOffset=7,MethodILOffset=0)]
    [RegressionOutcome("Contract.Requires(x >= 0);")]
    static public void AssertGeqZero(int x)
    {
      Contract.Assert(x >= 0);
    }

    [ClousotRegressionTest]
    [RegressionOutcome("Contract.Requires(x >= 0);")]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"assert unproven",PrimaryILOffset=15,MethodILOffset=0)]
    static public void AssertWithDouble(double x)
    {
      Contract.Assert(x >= 0);
    }

    [ClousotRegressionTest]
    [RegressionOutcome("Contract.Requires(x >= 0);")]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"assert unproven",PrimaryILOffset=11,MethodILOffset=0)]
    static public void AssertWithFloat(float x)
    {
      Contract.Assert(x >= 0);
    }

    // Should suggest only one precondition
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"assert unproven",PrimaryILOffset=11,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"assert unproven",PrimaryILOffset=27,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"assert unproven",PrimaryILOffset=40,MethodILOffset=0)]
    [RegressionOutcome("Contract.Requires(z >= k);")]
    static void RepeatedPreconditionInference(int x, int z, int k)
    {
      if (x > 0)
      {
        Contract.Assert(z >= k);
      }
      else if (x == 0)
      {
        Contract.Assert(z >= k);
      }
      else
      {
        Contract.Assert(z >= k);
      }
    }
  }

  public class ControlFlowInference
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as receiver)",PrimaryILOffset=2,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Possibly calling a method on a null reference 's' (Fixing this warning may solve one additional issue in the code)",PrimaryILOffset=12,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Possibly calling a method on a null reference 's'",PrimaryILOffset=19,MethodILOffset=0)]
    [RegressionOutcome("Contract.Requires(s != null);")]
    public int WithMethodCall(string s)
    {
      var b = Foo(s);
      if (b)
      {
        return s.Length;
      }
      else
      {
        return s.Length + 1;
      }
    }

    public bool Foo(string s)
    {
      return s != null;
    }
  }

}


