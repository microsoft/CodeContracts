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
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;
using Microsoft.Research.ClousotRegression;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ClassLibrary120
{
  public struct MyStruct
  {
    public int x;
  }

  public class WithRO
  {
     int x;
     int z;

    [ClousotRegressionTest]
     public void main(int x, int y, int z)
     {
       Contract.Assume(z <= y);
       while (x < y)
       {
         x++;
       }
       // x >= y >= z
       Contract.Assert(z <= x);
     }
  

    [ClousotRegressionTest]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Field x, declared in type WithRO, is only updated in constructors. Consider marking it as readonly",PrimaryILOffset=1,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Field z, declared in type WithRO, is only updated in constructors. Consider marking it as readonly",PrimaryILOffset=1,MethodILOffset=0)]
    public WithRO(int z)
    {
      this.x = z;
      this.z = z + 2;
    }

     [ClousotRegressionTest]
	 [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Assumption can be proven: Consider changing it into an assert.",PrimaryILOffset=20,MethodILOffset=0)]
	 public void MyStruct(MyStruct str)
    {
      if(str.x > 2)
      {
        Contract.Assume(str.x > 0);
      }
    }

   [ClousotRegressionTest]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="assert unproven",PrimaryILOffset=9,MethodILOffset=0)]
   public void Foo()
    {
      Contract.Assert(this.x > 0);
    }

   [ClousotRegressionTest]
    public void Bar()
    {
      if(this.x > 0)
      {
        Contract.Assert(this.x > -1);
      }
    }

   [ClousotRegressionTest]
   [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Assumption can be proven: Consider changing it into an assert.",PrimaryILOffset=20,MethodILOffset=0)]
    private int Foo(int[] a)
    {
      //Contract.Requires(a.Count() > 2);

      if (a == null || a.Count() != 2)
      {
        return -1;
      }

	  Contract.Assume(a.Length == 2);
      return a[1];
    }
  }
}
