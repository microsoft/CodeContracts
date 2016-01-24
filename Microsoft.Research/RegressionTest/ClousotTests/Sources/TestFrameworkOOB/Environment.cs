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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Research.ClousotRegression;

namespace TestFrameworkOOB
{
  public class EnvironmentTest
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 20, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 13, MethodILOffset = 20)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 33, MethodILOffset = 20)]
    public static void Test1()
    {
      string str = "Test";
      string newLine = Environment.NewLine;

      str = str.Replace(newLine, ";");  

    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 40, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 13, MethodILOffset = 40)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 33, MethodILOffset = 40)]
    public static void Test2(string str, string find)
    {
      if (string.IsNullOrEmpty(find))
      {
        return;
      }
      if (string.IsNullOrEmpty(str))
      {
        return;
      }

      str = str.Replace(find, ";");  

    }
  }

  public class TypeTest
  {
    /// <summary>
    /// The non-null warning is a bug. Should be fixed by computed proper PC prior to requires, rather than
    /// grabbing predecessor block.
    /// </summary>
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 24, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 11, MethodILOffset = 0)]
    public static void Test()
    {
      string s = typeof(TypeTest).Name;
      Contract.Assert(s != null);
    }
  }

  // Tests a non-generic method within a generic class for the oob forwarding
  public class ListTest<T>
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as receiver)", PrimaryILOffset = 8, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 16, MethodILOffset = 0)]
    public static void Test()
    {
      var l = new List<T>();
      Contract.Assert(l.Count == 0);
    }
    
  }

  public class NullableTests
  {
    public int? optInt;
    public int? OptInt
    {
      get {
        Contract.Ensures(Contract.Result<int?>() == optInt);

        return optInt;
      }
      set { this.optInt = value; }
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 25, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 2, MethodILOffset = 30)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 7, MethodILOffset = 30)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 12, MethodILOffset = 30)]
    public void Set(int i)
    {
      Contract.Ensures(optInt.HasValue);

      optInt = i;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 28, MethodILOffset = 0)]
#if CLOUSOT2 
	[RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset=1, MethodILOffset=34)]
#else
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 2, MethodILOffset = 34)]
#endif
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 10, MethodILOffset = 34)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 15, MethodILOffset = 34)]
    public void Set2(int i)
    {
      Contract.Ensures(OptInt.HasValue);

      OptInt = i;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 28, MethodILOffset = 0)]
#if CLOUSOT2 
	[RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset=1, MethodILOffset=33)]
#else
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 2, MethodILOffset = 33)]
#endif
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 10, MethodILOffset = 33)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 15, MethodILOffset = 33)]
    public void Set3(int i)
    {
      // Should pass, but WP reasoning is too weak.
      // F: Now passes thanks to the improved handling of booleans in Intervals
      Contract.Ensures(OptInt.HasValue);

      optInt = i;
    }
  }


    public class NullableTest2
    {
      private int? _Value1;
      private int? _Value2;

      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as receiver)", PrimaryILOffset = 1, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as field receiver)", PrimaryILOffset = 15, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as field receiver)", PrimaryILOffset = 27, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as field receiver)", PrimaryILOffset = 33, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as receiver)", PrimaryILOffset = 38, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as field receiver)", PrimaryILOffset = 50, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as receiver)", PrimaryILOffset = 55, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 43, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 60, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"invariant is valid", PrimaryILOffset = 25, MethodILOffset = 67)]
      public NullableTest2()
      {
        _Value1 = 0;
        _Value2 = 1;
        Contract.Assert(_Value1.HasValue);
        Contract.Assert(_Value2.HasValue);
        // F: invariant proven thanks to the  handling of "true" == 1 in intervals
      }

      [ContractInvariantMethod]
      private void ObjectInvariant()
      {
        Contract.Invariant(_Value1.HasValue == _Value2.HasValue);
      }
    }


    public class NullableTest3
    {
      private int? _Value1;
      private int? _Value2;

      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as receiver)", PrimaryILOffset = 1, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as field receiver)", PrimaryILOffset = 15, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as field receiver)", PrimaryILOffset = 27, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"invariant is valid", PrimaryILOffset = 25, MethodILOffset = 33)]
      public NullableTest3()
      {
        _Value1 = 0;
        _Value2 = 1;
        // f: We do not need assertions here, unlike the original repro above (NullableTest2)
      }

      [ContractInvariantMethod]
      private void ObjectInvariant()
      {
        Contract.Invariant(_Value1.HasValue == _Value2.HasValue);
      }
    }

  class TestPurityIssue
  {
    public bool Value { get; set; }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 28, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 40, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 61, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 13, MethodILOffset = 54)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 66, MethodILOffset = 0)]
    public void TestMe(TestPurityIssue that, Array array)
    {
      Contract.Requires(that != null);
      Contract.Requires(array != null);
      Contract.Requires(this.Value);
      Contract.Requires(that.Value);

      Array.BinarySearch(array, that, null);

      Contract.Assert(that.Value);
      
    }
  }
}
