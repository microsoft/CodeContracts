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
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Diagnostics.Contracts;
using Microsoft.Research.ClousotRegression;
using System.Runtime.InteropServices;


namespace Pasta
{
    public class Class1
    {
	  //                         0          1        2        3     4      5
      public enum CookPasta { BoilWater, AddSalt, AddPasta, Stir, Strain, Eat }

      [Pure]
      [ClousotRegressionTest]
      static public bool CanTransition(CookPasta from, CookPasta to)
      {
        switch (from)
        {
          case CookPasta.BoilWater:
            return to == CookPasta.AddPasta || to == CookPasta.AddSalt;

          case CookPasta.AddPasta:
            return to == CookPasta.AddSalt || to == CookPasta.Stir;

          case CookPasta.AddSalt:
            return to == CookPasta.AddPasta;

          case CookPasta.Stir:
            return to == CookPasta.Strain;

          case CookPasta.Strain:
            return to == CookPasta.Eat;

          case CookPasta.Eat:
            return false;

          default:
            throw CodeContractsHelpers.ThrowIfReached<WrongWayOfDoingPasta>("missing case");
        }
      }

      [ClousotRegressionTest]
      static public void BoilWaterIsInitial(CookPasta someState)
      {
        var next = CanTransition(someState, CookPasta.BoilWater);
        Contract.Assert(!next, "there is no way to transition to boiling water");
      }

      [ClousotRegressionTest]
      static public void EatingIsFinal(CookPasta someOtherState)
      {
        var next = CanTransition(CookPasta.Eat, someOtherState);
        Contract.Assert(!next, "Once you eat your pasta, nothing more left to do");
      }

      [ClousotRegressionTest]
	  [RegressionOutcome("This condition involving the return value of CanTransition should hold to avoid an error later (obligation next == 0): Contract.Result<System.Boolean>() == false")]
	  [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Missing precondition in an externally visible method. Consider adding Contract.Requires(someOtherState != Pasta.Class1.CookPasta.Eat); for parameter validation",PrimaryILOffset=2,MethodILOffset=0)]
	  static public void StrainIsFinal(CookPasta someOtherState)
      {
        var next = CanTransition(CookPasta.Strain, someOtherState);
        Contract.Assert(!next, "We cannot prove it, as it is false");
      }

      [ClousotRegressionTest]
	  // The code that was inferring the precondition below was buggy
	  // [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Missing precondition in an externally visible method. Consider adding Contract.Requires(unknownStep == Pasta.Class1.CookPasta.Strain); for parameter validation",PrimaryILOffset=1,MethodILOffset=0)]
	  [RegressionOutcome(Outcome=ProofOutcome.Top,Message="assert unproven",PrimaryILOffset=11,MethodILOffset=0)]
	  static public void EatAfterStir(CookPasta unknownStep)
      {
        var init = CookPasta.Stir;

        var next = CanTransition(init, unknownStep);

        Contract.Assert(next); // note in this example we ask for "next" instead of "not next" as in the example above
      }

      [Pure]
      [ClousotRegressionTest]
      static public bool IsInitial(CookPasta state)
      {
        return state == CookPasta.BoilWater;
      }

      [Pure]
      [ClousotRegressionTest]
      static public bool IsTerminal(CookPasta state)
      {
        return state == CookPasta.Eat;
      }

    }

    public class WrongWayOfDoingPasta : Exception { }

    public static class CodeContractsHelpers
    {
      [ContractVerification(false)]

      public static Exception ThrowIfReached<TException>(string s)

      where TException : Exception
      {

        Contract.Requires(false);

        var constructor = typeof(TException).GetConstructor(new Type[] { typeof(string) });

        if (constructor != null)
        {

          return (TException)constructor.Invoke(new object[] { s });

        }

        return new Exception(s);

      }
    }
}
