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

namespace Existential
{
  public class BasicTests
  {
    [ClousotRegressionTest] 
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"ensures is valid",PrimaryILOffset=58,MethodILOffset=66)]
    public int ExistsInSimpleReturn(int[] arr)
    {
      Contract.Requires(arr != null);
      Contract.Requires(arr.Length > 0);
	
      Contract.Ensures(Contract.Exists(arr, el => el == Contract.Result<int>()));

      return arr[0];
    }

    [ClousotRegressionTest] 
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"ensures is valid",PrimaryILOffset=58,MethodILOffset=66)]
    public int ExistsInSimpleReturn2(int[] arr)
    {
      Contract.Requires(arr != null);
      Contract.Requires(arr.Length > 0);

      Contract.Ensures(Contract.Exists(arr, el => Contract.Result<int>() == el ));

      return arr[0];
    }

    [ClousotRegressionTest] 
#if NETFRAMEWORK_4_0
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=17,MethodILOffset=101)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=39,MethodILOffset=101)]
#else
	#if CLOUSOT2
		[RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=3,MethodILOffset=101)]
		[RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=25,MethodILOffset=101)]
	#else
		[RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=22,MethodILOffset=101)]
		[RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=44,MethodILOffset=101)]
	#endif
#endif
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=106,MethodILOffset=0)]
    public int ExistsConditionalWithAssert(int[] arr, int index1, int index2, bool branch)
    {
      Contract.Requires(arr != null);
      Contract.Requires(index1 >= 0);
      Contract.Requires(index1 < arr.Length);
      Contract.Requires(index2 >= 0);
      Contract.Requires(index2 < arr.Length);

      int value;
     
      if (branch)
      {
        value = arr[index1];
      }
      else
      {
        value = arr[index2];
      }

      Contract.Assert(Contract.Exists(arr, el => value == el));

      return value;
    }

    [ClousotRegressionTest] 
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"ensures is valid",PrimaryILOffset=93,MethodILOffset=109)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"ensures is valid",PrimaryILOffset=93,MethodILOffset=105)]
    public int ExistsConditionalWithPostCondition(int[] arr, int index1, int index2, bool branch)
    {
      Contract.Requires(arr != null);
      Contract.Requires(index1 >= 0);
      Contract.Requires(index1 < arr.Length);
      Contract.Requires(index2 >= 0);
      Contract.Requires(index2 < arr.Length);

      Contract.Ensures(Contract.Exists(arr, el => Contract.Result<int>() == el));

      if (branch)
      {
        return arr[index1];
      }
      else
      {
        return arr[index2];
      }

    }

    [ClousotRegressionTest] 
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"ensures unproven: Contract.Exists(arr, el => Contract.Result<int>() == el)",PrimaryILOffset=58,MethodILOffset=68)]
    public int ExistsInSimpleReturnWrong(int[] arr)
    {
      Contract.Requires(arr != null);
      Contract.Requires(arr.Length > 0);

      Contract.Ensures(Contract.Exists(arr, el => Contract.Result<int>() == el));

      return -369;
    }

    [ClousotRegressionTest] 
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"ensures unproven: Contract.Exists(arr, el => Contract.Result<int>() == el)",PrimaryILOffset=58,MethodILOffset=76)]
    public int ExistsInSimpleReturnWrong2(int[] arr)
    {
      Contract.Requires(arr != null);
      Contract.Requires(arr.Length > 0);

      Contract.Ensures(Contract.Exists(arr, el => Contract.Result<int>() == el)); // We modified the value of arr[0], so it does not hold

      var result = arr[0];

      arr[0] = 1234;

      return result;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=76,MethodILOffset=0)]
    public int LinearSearch(int[] a, int value)
    {
      Contract.Requires(a != null);
      Contract.Requires(Contract.Exists(a, el => el == value));

      for (var i = 0; i < a.Length; i++)
      {
        if (a[i] == value)
          return i;
      }

      // should be unreached
      // infers ForAll(xs, x => x != value)
      Contract.Assert(false);
      return -1;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=74,MethodILOffset=0)]
    public int FirstNotNull(object[] xs)
    {
      Contract.Requires(xs != null);

      Contract.Requires(Contract.Exists(xs, el => el != null));

      for (var i = 0; i < xs.Length; i++)
      {
        if (xs[i] != null)
        {
          return i;
        }
      }

      // should be unreached
      // infers ForAll(xs, x => x == null)
      Contract.Assert(false); // Report as valid, as it is "assert false"
      return -1;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=74,MethodILOffset=0)]    
    public int FirstNull(object[] xs)
    {
      Contract.Requires(xs != null);

      Contract.Requires(Contract.Exists(xs, el => el == null));

      for (var i = 0; i < xs.Length; i++)
      {
        if (xs[i] == null)
        {
          return i;
        }
      }
      // should be unreached
      // infers ForAll(xs, x => x != null)
      Contract.Assert(false);
      return -1;
    }


    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"ensures is valid",PrimaryILOffset=50,MethodILOffset=88)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"ensures is valid",PrimaryILOffset=50,MethodILOffset=73)]
    public bool TryLinearSearch(int[] elements, int value, out int index)
    {
      Contract.Requires(elements != null);

      Contract.Ensures(Contract.Result<bool>() == Contract.Exists(elements, el => value == el));

      for (index = 0; index < elements.Length; index++)
      {
        if (elements[index] == value)
        {
	  // here infers: Contract.Exists(elements, el => value == el)
          return true;
        }
      }

      // here infers: Contract.Assert(Contract.ForAll(elements, el => value != el));

      return false;
    }

    // Tests the WP backwards propagation of exists
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"ensures is valid",PrimaryILOffset=53,MethodILOffset=76)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"ensures is valid",PrimaryILOffset=53,MethodILOffset=91)]
    public bool TryLinearSearchWithImplication(int[] elements, int value, out int index)
    {
      Contract.Requires(elements != null);

      Contract.Ensures(!Contract.Result<bool>() || Contract.Exists(elements, el => value == el));

      for (index = 0; index < elements.Length; index++)
      {
        if (elements[index] == value)
        {
          return true;
        }
      }

      // Contract.Assert(Contract.ForAll(elements, el => value != el));

      return false;
    }

    // TODO : understanding old
    [ClousotRegressionTest] 
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"ensures unproven: Contract.Exists(0, arr.Length, i => Contract.Result<int>() == Contract.OldValue(arr[i]))",PrimaryILOffset=72,MethodILOffset=100)]
    public int ExistsWithNotPure(int[] arr)
    {
      Contract.Requires(arr != null);
      Contract.Requires(arr.Length > 0);

      Contract.Ensures(Contract.Exists(0, arr.Length, i => Contract.Result<int>() == Contract.OldValue(arr[i])));

      var result = arr[0];

      arr[0] = 1234;

      return result;
    }

    // TODO: missing decompilation of nested quantifiers
    [ClousotRegressionTest] 
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"ensures unproven: Contract.ForAll(Contract.Result<int[]>(), el => Contract.Exists(original, or => el == or))",PrimaryILOffset=52,MethodILOffset=102)]
    public int[] ExistsWithForAll(int[] original)
    {
      Contract.Requires(original != null);
      Contract.Ensures(Contract.ForAll(Contract.Result<int[]>(), el => Contract.Exists(original, or => el == or)));

      var result = new int[original.Length];
      for (var i = 0; i < original.Length; i++)
      {
        result[i] = original[i];
      }

      return result;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"ensures is valid",PrimaryILOffset=58,MethodILOffset=132)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"ensures is valid",PrimaryILOffset=98,MethodILOffset=132)]
    public int Max(int[] array)
    {
      Contract.Requires(array != null);
      Contract.Requires(array.Length > 0);

      Contract.Ensures(Contract.Exists(array, el => el == Contract.Result<int>()));
      Contract.Ensures(Contract.ForAll(array, el => el <= Contract.Result<int>()));

      var max = array[0];
      for (var i = 1; i < array.Length; i++)
      {
        if (array[i] > max)
        {
          max = array[i];
        }
      }
      return max;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"ensures is valid",PrimaryILOffset=77,MethodILOffset=118)]
    public static int ReturnValueIsAnArrayElement(int[] a, int max, int i)
    {
      Contract.Requires(Contract.Exists(0, a.Length, index => a[index] == max));
      Contract.Ensures(Contract.Exists(0, a.Length, index => a[index] == Contract.Result<int>()));       

      if (a[i] > max)
      {
        max = a[i];
      }
      return max;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=46,MethodILOffset=129)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"ensures is valid",PrimaryILOffset=72,MethodILOffset=151)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"ensures is valid",PrimaryILOffset=103,MethodILOffset=151)]
    int Max_Refactored(int[] a)
    {
      Contract.Requires(a != null);
      Contract.Requires(0 < a.Length);

      Contract.Ensures(Contract.ForAll(0, a.Length, i => a[i] <= Contract.Result<int>()));
      Contract.Ensures(Contract.Exists(0, a.Length, i => a[i] == Contract.Result<int>()));

      var max = a[0];
      for (var i = 0; i < a.Length; i++)
      {

        max = NewMethod1(a, max, i);
      }

      return max;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"ensures is valid",PrimaryILOffset=77,MethodILOffset=150)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"ensures is valid",PrimaryILOffset=100,MethodILOffset=150)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"ensures is valid",PrimaryILOffset=121,MethodILOffset=150)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"ensures is valid",PrimaryILOffset=77,MethodILOffset=157)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"ensures is valid",PrimaryILOffset=100,MethodILOffset=157)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"ensures is valid",PrimaryILOffset=121,MethodILOffset=157)]
    private static int NewMethod1(int[] a, int max, int i)
    {
      Contract.Requires(Contract.Exists(0, a.Length, index => a[index] == max));

      Contract.Ensures(Contract.Exists(0, a.Length, index => a[index] == Contract.Result<int>()));  

      Contract.Ensures(Contract.Result<int>() >= a[i]);
      Contract.Ensures(Contract.Result<int>() >= max);

      if (a[i] > max)
      {
        return a[i];
      }
      return max;
    }


  }

  public class Tests
  {
    [ClousotRegressionTest]
#if NETFRAMEWORK_4_0
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=17,MethodILOffset=62)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=39,MethodILOffset=62)]
#else
	#if CLOUSOT2
		[RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=3,MethodILOffset=62)]
		[RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=25,MethodILOffset=62)]
	#else
		[RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=22,MethodILOffset=62)]
		[RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=44,MethodILOffset=62)]
	#endif
#endif
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=67,MethodILOffset=0)]
    static public int Max([Pure] int[] arr)
    {
      //Contract.Ensures(Contract.Exists(arr, el => el == Contract.Result<int>()));
      var max = arr[0];
      for (var i = 1; i < arr.Length; i++)
      {
        if (max < arr[i])
        {
          max = arr[i];
        }
      }

      Contract.Assert(Contract.Exists(arr, el => el == max)); 

      return max;
    }

    [ClousotRegressionTest]
#if NETFRAMEWORK_4_0
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=17,MethodILOffset=70)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=39,MethodILOffset=70)]
#else
	#if CLOUSOT2
		[RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=3,MethodILOffset=70)]
		[RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=25,MethodILOffset=70)]
	#else
		[RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=22,MethodILOffset=70)]
		[RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=44,MethodILOffset=70)]
	#endif
#endif
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"assert unproven",PrimaryILOffset=75,MethodILOffset=0)]
    static public int[] ArrayCopy([Pure] int[] arr)
    {
      var result = new int[arr.Length];
      for (var i = 0; i < arr.Length; i++)
      {
        result[i] = arr[i];
      }

      // We infer the right invariant, but we cannot prove the condition yet as we do not decompile Contract.Exists
      Contract.Assert(Contract.ForAll(result, el => Contract.Exists(arr, origin => el == origin)));

      return result;
    }
    


    [ClousotRegressionTest]
#if NETFRAMEWORK_4_0
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=13,MethodILOffset=87)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=35,MethodILOffset=87)]
#else
 	#if CLOUSOT2
		[RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=3,MethodILOffset=87)]
		[RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=25,MethodILOffset=87)]
	#else
		[RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=22,MethodILOffset=87)]
		[RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=44,MethodILOffset=87)]
	#endif
#endif
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"assert unproven",PrimaryILOffset=92,MethodILOffset=0)]
    static public string[] ConditionWithMethodCall([Pure] string[] origin)
    {
      Contract.Requires(origin != null);

      var result = new string[origin.Length];
      int j = 0;
      for (int i = 0; i < origin.Length; i++)
      {
        if (!String.IsNullOrEmpty(origin[i]))
        {
          result[j] = origin[i];
          j++;
        }
      }

      // We do not decompile IsNullOrEmpty in the boxed expression
      Contract.Assert(Contract.ForAll(0, j, indx => !String.IsNullOrEmpty(result[indx])));

      return result;

    }
  }
}

namespace SymbolicPropagationOfConditions
{
  public class Tests
  {
    [ClousotRegressionTest]
#if NETFRAMEWORK_4_0
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=13,MethodILOffset=78)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=35,MethodILOffset=78)]
#else
	#if CLOUSOT2
		[RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=3,MethodILOffset=78)]
		[RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=25,MethodILOffset=78)]
	#else
		[RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=22,MethodILOffset=78)]
		[RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=44,MethodILOffset=78)]
	#endif

#endif
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=83,MethodILOffset=0)]
    static public int[] Filter([Pure] int[] origin)
    {
      var result = new int[origin.Length];
      int j = 0;
      for (int i = 0; i < origin.Length; i++)
      {
	// This expression is outside the expressivity of Clousot numerical domains, but we propagate it symbolically anyway
        if (origin[i] % 2345 + 2== 0)
        {
          result[j] = origin[i];
          j++;
        }
      }

      Contract.Assert(Contract.ForAll(0, j, indx => result[indx] % 2345 + 2 == 0));

      return result;
    }
  }
}

namespace ForAllExists
{
  public class Tests
  {
    [ClousotRegressionTest]
#if NETFRAMEWORK_4_0
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=13,MethodILOffset=148)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=35,MethodILOffset=148)]
#else
	#if CLOUSOT2
		[RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=3,MethodILOffset=148)]
		[RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=25,MethodILOffset=148)]
	#else
		[RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=22,MethodILOffset=148)]
		[RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=44,MethodILOffset=148)]
	#endif
#endif
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=153,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"ensures unproven: Contract.ForAll( 0, Contract.ValueAtReturn(out j), indx => Contract.Exists(strings, someValue => Contract.Result<string[]>()[indx] == someValue))",PrimaryILOffset=54,MethodILOffset=164)]
    static public string[] FilterNonNullStrings(string[] strings, out int j)
    {
      Contract.Requires(strings != null);

      // True
      Contract.Ensures(
        Contract.ForAll(
          0, Contract.ValueAtReturn(out j),
          indx => Contract.Exists(strings,
            someValue => Contract.Result<string[]>()[indx] == someValue)));

      var result = new string[strings.Length];
      j = 0;
      for (var i = 0; i < strings.Length; i++)
      {
        if (strings[i] != null)
        {
          result[j] = strings[i];
          j++;
        }
      }

      Contract.Assert(Contract.ForAll(0, j, indx => result[indx] != null));
      
      return result;
    }
    
    [ClousotRegressionTest]
#if NETFRAMEWORK_4_0
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=13,MethodILOffset=157)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=35,MethodILOffset=157)]
#else
	#if CLOUSOT2
		[RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=3,MethodILOffset=157)]
		[RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=25,MethodILOffset=157)]
	#else
		[RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=22,MethodILOffset=157)]
		[RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=44,MethodILOffset=157)]
	#endif
#endif
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=162,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"ensures unproven: Contract.ForAll( 0, Contract.ValueAtReturn(out j), indx => Contract.Exists(strings, someValue => Contract.Result<string[]>()[indx] == someValue))",PrimaryILOffset=54,MethodILOffset=173)]
    static public string[] FilterNonNullStrings_WithUpdate_WrongPostcondition(string[] strings, out int j)
    {
      Contract.Requires(strings != null);
      
      // Top
      Contract.Ensures(
        Contract.ForAll(
          0, Contract.ValueAtReturn(out j), 
          indx => Contract.Exists(strings, 
            someValue => Contract.Result<string[]>()[indx] == someValue)));

      var result = new string[strings.Length];

      j = 0;
      for (var i = 0; i < strings.Length; i++)
      {
        if (strings[i] != null)
        {
          result[j] = strings[i];
          strings[i] = null;
          j++;
        }
      }

      // True
      Contract.Assert(Contract.ForAll(0, j, indx => result[indx] != null));
      
      return result;
    }

    [ClousotRegressionTest]
#if NETFRAMEWORK_4_0
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=13,MethodILOffset=157)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=35,MethodILOffset=157)]
#else
	#if CLOUSOT2
		[RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=3,MethodILOffset=157)]
		[RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=25,MethodILOffset=157)]
	#else
		[RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=22,MethodILOffset=157)]
		[RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=44,MethodILOffset=157)]
	#endif
#endif
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=162,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"ensures unproven: Contract.ForAll( 0, Contract.ValueAtReturn(out j), indx => Contract.Exists(strings, someValue => Contract.Result<string[]>()[indx] == Contract.OldValue(someValue)))",PrimaryILOffset=54,MethodILOffset=173)]
    static public string[] FilterNonNullStrings_WithUpdate_RightPostcondition(string[] strings, out int j)
    {
      Contract.Requires(strings != null);

      // true (we use old)
      Contract.Ensures(
        Contract.ForAll(
          0, Contract.ValueAtReturn(out j),
          indx => Contract.Exists(strings,
            someValue => Contract.Result<string[]>()[indx] == Contract.OldValue(someValue))));

      var result = new string[strings.Length];

      j = 0;
      for (var i = 0; i < strings.Length; i++)
      {
        if (strings[i] != null)
        {
          result[j] = strings[i];
          strings[i] = null;
          j++;
        }
      }

      Contract.Assert(Contract.ForAll(0, j, indx => result[indx] != null));

      return result;
    }
  }
}
