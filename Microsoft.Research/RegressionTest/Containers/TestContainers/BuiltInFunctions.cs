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
using System.Linq;
using System.Diagnostics.Contracts;
using Microsoft.Research.ClousotRegression;

namespace Linq
{
  public class FromLinqtutorial
  {
    [ClousotRegressionTest]
    public void Linq78()
    {
      //int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

      int[] numbers = new int[10];
      numbers[0] = 5;
      numbers[1] = 4;
      numbers[2] = 1;
      numbers[3] = 3;
      numbers[4] = 9;
      numbers[5] = 8;
      numbers[6] = 6;
      numbers[7] = 7;
      numbers[8] = 2;
      numbers[9] = 0;

      double numSum = numbers.Sum();

      Contract.Assert(numSum == 45);

      Console.WriteLine("The sum of the numbers is {0}.", numSum);
    }

    [ClousotRegressionTest]
    public void Linq81()
    {
      //int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

      int[] numbers = new int[10];
      numbers[0] = 5;
      numbers[1] = 4;
      numbers[2] = 1;
      numbers[3] = 3;
      numbers[4] = 9;
      numbers[5] = 8;
      numbers[6] = 6;
      numbers[7] = 7;
      numbers[8] = 2;
      numbers[9] = 0;

      int minNum = numbers.Min();

      Contract.Assert(minNum == 0);

      Console.WriteLine("The minimum number is {0}.", minNum);
    }

    [ClousotRegressionTest]
    public void Linq85()
    {
      //int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
      int[] numbers = new int[10];
      numbers[0] = 5;
      numbers[1] = 4;
      numbers[2] = 1;
      numbers[3] = 3;
      numbers[4] = 9;
      numbers[5] = 8;
      numbers[6] = 6;
      numbers[7] = 7;
      numbers[8] = 2;
      numbers[9] = 0;

      int maxNum = numbers.Max();

      Contract.Assert(maxNum == 9);

      Console.WriteLine("The maximum number is {0}.", maxNum);
    }
  }
  
  public class HandleCount
  {
	[ClousotRegressionTest]
	static public  int Foo(int[] a)
    {
		// Here we use the fact that a.Length == a.Count
      if (a == null || a.Count() != 2)
      {
        return -1;
      }

      return a[1];
    }
	
	[ClousotRegressionTest]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Array access might be above the upper bound. The static checker determined that the condition '1 < a.Length' should hold on entry. Nevertheless, the condition may be too strong for the callers. If you think it is ok, add a precondition to document it: Contract.Requires(1 < a.Length);",PrimaryILOffset=16,MethodILOffset=0)]
	static public  int FooWrong(int[] a)
    {
		// Here we use the fact that a.Length == a.Count
      if (a == null || a.Count() == 2)
      {
        return -1;
      }

      return a[1];
    }
  }
}

namespace SystemArray
{
  class ArrayCopyTest
  {
    [ClousotRegressionTest]
    public void CopyNonNull(object[] args)
    {
      Contract.Requires(args != null);
      Contract.Requires(Contract.ForAll(args, o => o != null));

      var result = new object[args.Length];

      Array.Copy(args, result, args.Length);

      Contract.Assert(Contract.ForAll(result, o => o != null)); // true
    }

    [ClousotRegressionTest]
#if !CLOUSOT2
#endif
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"assert unproven",PrimaryILOffset=158,MethodILOffset=0)]
#if NETFRAMEWORK_4_0
#else
	#if CLOUSOT2
	#else
	#endif
#endif
    public void CopyNonNull(object[] args, object[] result)
    {
      Contract.Requires(args != null);
      Contract.Requires(result != null);
      Contract.Requires(result.Length >= args.Length);
      Contract.Requires(Contract.ForAll(args, o => o != null));

      Array.Copy(args, result, args.Length);

      
      Contract.Assert(Contract.ForAll(result, o => o != null)); // Not true
      Contract.Assert(Contract.ForAll(0, args.Length, i => result[i] != null)); // True
    } 
  }
}