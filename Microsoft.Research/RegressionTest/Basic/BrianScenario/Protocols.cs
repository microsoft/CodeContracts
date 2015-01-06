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

using System.Diagnostics;
using System.Diagnostics.Contracts;
using Microsoft.Research.ClousotRegression;

namespace APIProtocols
{
  public enum Case
  {
    A,
    B,
    C,
  }

  class APISpecExample
  {

    string name;

    public bool HasName
    {
      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 1, MethodILOffset = 0)]
      get
      {
        // inferred: Contract.Ensures(Contract.Result<bool>() == (name != null));
        return name != null;
      }
    }

    public string Name
    {
      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 1, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 28, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 22, MethodILOffset = 33)]
      get
      {
        Contract.Requires(HasName);
        Contract.Ensures(Contract.Result<string>() != null);

        return name;
      }

      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 22, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 1, MethodILOffset = 27)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 15, MethodILOffset = 27)]
      set
      {
        Contract.Ensures(HasName || value == null);
        this.name = value;
      }
    }

    Case _case;

    public APISpecExample()
    {
      Contract.Ensures(State == Case.A);

      _case = Case.A;

    }

    public Case State
    {
      get
      {
        Contract.Ensures(Contract.Result<Case>() == _case);
        return _case;
      }
    }

    public void Start()
    {
      Contract.Requires(State == Case.A);
      Contract.Ensures(State == Case.B);

      _case = Case.B;
    }

    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 1, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 58, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 64, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 37, MethodILOffset = 72)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 51, MethodILOffset = 72)]
    public bool MayStart()
    {
      Contract.Requires(State == Case.A);
      Contract.Ensures(!Contract.Result<bool>() && (State == Case.A) || Contract.Result<bool>() && (State == Case.B));

      _case = Case.B;
      return (State == Case.B);
    }

    public void Run()
    {
      Contract.Requires(State == Case.B);
      Contract.Ensures(State == Case.B);

    }

    public void Stop()
    {
      Contract.Requires(State == Case.B);
      Contract.Ensures(State == Case.C);

      _case = Case.C;
    }

    public void Reset()
    {
      Contract.Requires(State == Case.C);
      Contract.Ensures(State == Case.A);

      _case = Case.A;
    }

  }


  class TestAPI
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 7, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 16, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 22, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 28, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 34, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 43, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 49, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 55, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 9, MethodILOffset = 7)]
    [RegressionOutcome(Outcome = ProofOutcome.False, Message = @"requires is false: State == Case.A", PrimaryILOffset = 9, MethodILOffset = 16)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 9, MethodILOffset = 22)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 9, MethodILOffset = 28)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 9, MethodILOffset = 34)]
    [RegressionOutcome(Outcome = ProofOutcome.False, Message = @"requires is false: State == Case.B", PrimaryILOffset = 9, MethodILOffset = 43)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 9, MethodILOffset = 49)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 9, MethodILOffset = 55)]
    public static void Test1(bool bug1, bool bug2)
    {
      APISpecExample a = new APISpecExample();

      a.Start();
      if (bug1)
      {
        a.Start();
      }

      a.Run();
      a.Run();

      a.Stop();

      if (bug2)
      {
        a.Run();
      }

      a.Reset();
      a.Start();
    }

    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 7, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 17, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 32, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 38, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 9, MethodILOffset = 7)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "requires unproven: State == Case.B", PrimaryILOffset = 9, MethodILOffset = 17)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 9, MethodILOffset = 32)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 9, MethodILOffset = 38)]
    public static void Test1b(bool bug1, bool bug2)
    {
      APISpecExample a = new APISpecExample();

      bool success = a.MayStart();

      if (bug1)
      {
        a.Run();
        Contract.Assume(false);
      }

      if (!success)
      {
        a.Start();
      }

      a.Run();
    }


    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 13, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"requires unproven: HasName", PrimaryILOffset = 6, MethodILOffset = 13)]
    public static string Test2(APISpecExample a)
    {
      Contract.Requires(a != null);
      return a.Name;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 13, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 21, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 6, MethodILOffset = 21)]
    public static string Test3(APISpecExample a)
    {
      Contract.Requires(a != null);
      if (a.HasName) return a.Name;
      return "default";
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 13, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 24, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 6, MethodILOffset = 24)]
    public static string Test4(APISpecExample a)
    {
      Contract.Requires(a != null);
      Contract.Requires(a.HasName);

      return a.Name;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 18, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 24, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 6, MethodILOffset = 24)]
    public static string Test5(APISpecExample a)
    {
      Contract.Requires(a != null);
      a.Name = "foo";
      return a.Name;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 14, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 20, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"requires unproven: HasName", PrimaryILOffset = 6, MethodILOffset = 20)]
    public static string Test6(APISpecExample a, string s)
    {
      Contract.Requires(a != null);
      a.Name = s;
      return a.Name;
    }
  }
}


namespace Bierhoff
{
  enum RSState { unread, read, end, closed }

  class ResultSet
  {
    RSState state;

    public RSState State
    {
      get {
        // could be inferred, but this way, we don't make it brittle.
        Contract.Ensures(Contract.Result<RSState>() == this.state);

        return this.state;
      }
    }

    public bool Valid
    {
      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 33, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 41, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 6, MethodILOffset = 49)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 14, MethodILOffset = 49)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 6, MethodILOffset = 51)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 27, MethodILOffset = 51)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 27, MethodILOffset = 49)]
      get
      {
        Contract.Ensures(Contract.Result<bool>() == (this.State == RSState.unread || this.State == RSState.read));

        return this.State == RSState.unread || this.State == RSState.read;
      }
    }
    public bool IsOpen
    {
      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 33, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 41, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 6, MethodILOffset = 49)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 6, MethodILOffset = 51)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 14, MethodILOffset = 49)]
      // happens on path where Valid is true, then second reference to this.State never happens. 
      // F: Now we avoid emitting warnings for unreached parts of postconditions
      //[RegressionOutcome(Outcome = ProofOutcome.Bottom, Message = @"reference use unreached", PrimaryILOffset = 14, MethodILOffset = 51)] 
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 27, MethodILOffset = 51)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 27, MethodILOffset = 49)]
      get
      {
        Contract.Ensures(Contract.Result<bool>() == (this.Valid || this.State == RSState.end));

        return Valid || this.State == RSState.end;
      }
    }

    [ClousotRegressionTest("cci1only")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 1, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 55, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 19, MethodILOffset = 61)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 48, MethodILOffset = 61)]
    public bool Next()
    {
      Contract.Requires(IsOpen);
      Contract.Ensures(Contract.Result<bool>() && State == RSState.unread ||
                       !Contract.Result<bool>() && State == RSState.end);

      state = RSState.unread;
      return true;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 1, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 27, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 12, MethodILOffset = 33)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 20, MethodILOffset = 33)]
    public int GetInt(int column)
    {
      Contract.Requires(Valid);
      Contract.Ensures(State == RSState.read);

      state = RSState.read;
      return column;
    }

    public bool WasNull
    {
      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 1, MethodILOffset = 0)]
      get
      {
        Contract.Requires(State == RSState.read);
        return false;
      }
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 16, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 1, MethodILOffset = 21)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 9, MethodILOffset = 21)]
    public void Close()
    {
      Contract.Ensures(State == RSState.closed);
      state = RSState.closed;
    }
  }

  class TestResultSet
  {
    [ClousotRegressionTest("cci1only")]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "Possibly calling a method on a null reference 'rs'", PrimaryILOffset = 1, MethodILOffset = 0)]
    [RegressionOutcome (Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 12, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 23, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 28, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 34, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 53, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 6, MethodILOffset = 12)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 6, MethodILOffset = 23)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 9, MethodILOffset = 34)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "requires unproven: Valid", PrimaryILOffset = 6, MethodILOffset = 53)]
    public static int? GetFirstInt(ResultSet rs)
    {
      int? result;
      Contract.Requires(rs.IsOpen);
      if (rs.Next())
      {
        result = rs.GetInt(1);
        if (rs.WasNull)
        {
          result = null;
        }
        return result;
      }
      else
      {
        return rs.GetInt(1);
      }
    }
  }
}
