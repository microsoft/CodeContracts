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

namespace WeakestPreconditionTests
{
  public class Paths
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = (int)19, MethodILOffset = (int)44)]
    public static int TestDisjunct(bool b)
    {
      Contract.Ensures(Contract.Result<int>() == 0 || Contract.Result<int>() == 5);

      if (b)
      {
        return 0;
      }
      else
      {
        return 5;
      }
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = (int)16, MethodILOffset = (int)27)]
    public static int Implication(int x)
    {
      Contract.Ensures(!(x > 0) || Contract.Result<int>() > 0);

      return x;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = (int)16, MethodILOffset = (int)44)]
    public static int Implication2(int x)
    {
      Contract.Ensures(!(x > 0) || Contract.Result<int>() > 0);

      if (x > 0)
      {
        return x;
      }
      return -5;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"ensures unproven: !(x > 0) || Contract.Result<int>() > 0", PrimaryILOffset = 16, MethodILOffset = 41)]
    public static int Implication3(int x)
    {
      Contract.Ensures(!(x > 0) || Contract.Result<int>() > 0);

      if (x >= 0)
      {
        // not true
        return -x;
      }
      return x;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = (int)18, MethodILOffset = (int)29)]
    public static string Implication(string s)
    {
      Contract.Ensures(s == null || Contract.Result<string>() != null);

      return s;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = (int)21, MethodILOffset = (int)66)]
    public static int TestAnd(int x)
    {
      Contract.Ensures(Contract.Result<int>() > -5 && Contract.Result<int>() < 5);

      if (x < -3)
      {
        x = -3;
      }

      if (x > 3)
      {
        x = 3;
      }

      return x;
    }


    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = (int)32, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = (int)61, MethodILOffset = 0)]
    public static void Correlation()
    {

      string flag = GetFlag2();
      string s = null;

      if (flag != null)
      {
        s = "foo";
      }
      else
      {
        Contract.Assert(flag == null);
      }

      Something();

      if (flag != null)
      {
        Contract.Assert(s != null);
      }
    }

    [ClousotRegressionTest]
    [RegressionOutcome (Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = (int)34, MethodILOffset = 0)]
    [RegressionOutcome (Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = (int)63, MethodILOffset = 0)]
    public static void Correlation2()
    {
      int flag = GetFlag();
      int data = 0;

      if (flag == 1)
      {
        data = 5;
      }
      else
      {
        Contract.Assert(flag != 1);
      }
      Something();

      if (flag == 1)
      {
        Contract.Assert(data > 0);
      }
    }
    public static int flag;
    public static string flag2;
    static int GetFlag() { return flag; }
    static string GetFlag2() { return flag2; }
    static void Something() { }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = (int)21, MethodILOffset = 0)]
    void Correlation3(bool a, bool b)
    {
      Contract.Requires(a || b);

      Contract.Assert(a || b);
    }

    // *** NOTE
    // F: I moved Power to ADomainsInterface.cs, as the interface to the abstract domains helps the proof
    // ***
  }
}

namespace Improvements
{
  public class Tmp
  {
    public readonly Reactor r = new Reactor();

    public Reactor.State state;

	[ClousotRegressionTest]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=2,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=31,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=46,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=62,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=78,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=94,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=101,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="assert is valid",PrimaryILOffset=54,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="assert is valid",PrimaryILOffset=86,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="assert is valid",PrimaryILOffset=109,MethodILOffset=0)]
    public void SimpleTest(bool wt, bool restart)
    {
      Contract.Requires(this.state == Reactor.State.Off);

      Contract.Requires(wt != restart); 

      this.state = Reactor.State.On;

      if (wt)
      {
        Contract.Assert(this.state == Reactor.State.On);
        this.state = Reactor.State.Off;
      }

      if (restart)
      {
        Contract.Assert(this.state == Reactor.State.On);
        this.state = Reactor.State.Off;
      }

      Contract.Assert(this.state== Reactor.State.Off);

    }

	[ClousotRegressionTest]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=2,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=47,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=62,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=78,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=94,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=110,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=117,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="assert is valid",PrimaryILOffset=70,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="assert is valid",PrimaryILOffset=102,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="assert is valid",PrimaryILOffset=125,MethodILOffset=0)]
    public void SimpleTest_DifferentPrecondition(bool wt, bool restart)
    {
      Contract.Requires(this.state == Reactor.State.Off);

      Contract.Requires(wt || restart);
      Contract.Requires(!wt || !restart);

      this.state = Reactor.State.On;

      if (wt)
      {
        Contract.Assert(this.state == Reactor.State.On);
        this.state = Reactor.State.Off;
      }

      if (restart)
      {
        Contract.Assert(this.state == Reactor.State.On);
        this.state = Reactor.State.Off;
      }

      Contract.Assert(this.state == Reactor.State.Off);

    }
  }
	
	public class Reactor
  {
    public enum State { On, Off}

    public State state;

    public Reactor()
    {
      this.state = State.Off;
    }

    public void turnReactorOn()
    {
      Contract.Requires(this.state == State.Off);
      Contract.Ensures(this.state == State.On);

      this.state = State.On;
    }

    public void SCRAM()
    {
      Contract.Requires(this.state == State.On);
      Contract.Ensures(this.state == State.Off);

      this.state = State.Off;
    }
  }
}
