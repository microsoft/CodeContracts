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
using System.Diagnostics.Contracts;
using Microsoft.Research.ClousotRegression;


namespace MichaelRepros
{
  public class MichaelRepro0
  {
    static int m;

    [ClousotRegressionTest("postonly")]
    public void f0()
    {
      m = 0;
    }

    [ClousotRegressionTest("postonly")]
    public void f12()
    {
      m = 12;
    }

    [ClousotRegressionTest("postonly")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 14, MethodILOffset = 0)]
    public void Testf0()
    {
      f0();
      Contract.Assert(m == 0);
    }

    [ClousotRegressionTest("postonly")]
    [RegressionOutcome(Outcome = ProofOutcome.False, Message = @"assert is false", PrimaryILOffset = 18, MethodILOffset = 0)]
    public void Testf0_False()
    {
      f0();
      Contract.Assert(m == 999);
    }

    [ClousotRegressionTest("postonly")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 15, MethodILOffset = 0)]
    public void Testf12()
    {
      f12();
      Contract.Assert(m == 12);
    }
  }

  unsafe public class MichaelRepro1
  {
    //[ClousotRegressionTest("postonly")]
    public void SetToNull(out int* ptr)
    {
      ptr = null;
    }

    //[ClousotRegressionTest("postonly")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 16, MethodILOffset = 0)]
    public void TestSetToNull()
    {
      int* p = null;
      SetToNull(out p);
      Contract.Assert(p == null);
    }
  }

  public class MichaelRepro2
  {
    public bool b;

    // this.b == true
    [ClousotRegressionTest("postonly")]
    public MichaelRepro2()
    {
      this.b = true;
    }

    // ok (nothing)
    [ClousotRegressionTest("postonly")]
    public MichaelRepro2(int x)
    {
      if (x > 10)
        this.b = true;
      else
        this.b = false;
    }

    // this.b == b
    [ClousotRegressionTest("postonly")]
    public MichaelRepro2(bool b)
    {
      this.b = b;
    }

    // nothing... Because of the control flow
    [ClousotRegressionTest("postonly")]
    public MichaelRepro2(bool b1, bool b2)
    {
      this.b = b1 && b2;
    }

    // return == true
    [ClousotRegressionTest("postonly")]
    public bool AlwaysTrue()
    {
      return true;
    }

    // return == false
    [ClousotRegressionTest("postonly")]
    public bool AlwaysFalse()
    {
      return false;
    }

    [ClousotRegressionTest("postonly")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 12, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"assert unproven", PrimaryILOffset = 31, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 52, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"assert unproven", PrimaryILOffset = 74, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 93, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 108, MethodILOffset = 0)]
    public void Test()
    {
      var m1 = new MichaelRepro2();
      Contract.Assert(m1.b == true);      // true 

      var m2 = new MichaelRepro2(29);
      Contract.Assert(m2.b);      // true, but we cannot prove it

      var m3 = new MichaelRepro2(false);
      Contract.Assert(!m3.b);   // true

      var m4 = new MichaelRepro2(true, false);
      Contract.Assert(!m4.b);     // true, but we cannot prove it

      var m5 = new MichaelRepro2();

      Contract.Assert(m5.AlwaysTrue()); // true;

      Contract.Assert(!m5.AlwaysFalse());  // true
    }
  }

  public class MichaelRepro3
  {
    public enum SomeEnum { A = 2, B, C }

    [ClousotRegressionTest("postonly")]
    public SomeEnum ReturnEnum(int x)
    {
      if (x == 2)
        return SomeEnum.A;
      else
        return SomeEnum.B;

    }

    // ok
    [ClousotRegressionTest("postonly")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 22, MethodILOffset = 0)]
    public void Test()
    {
      var x = new MichaelRepro3();
      var r = x.ReturnEnum(12);
      Contract.Assert(r != SomeEnum.C);
    }
  }

  public class MichaelRepro4
  {
    public string Value;
    public MichaelRepro4 mRep;

    [ClousotRegressionTest("postonly")]
    public MichaelRepro4(string val, MichaelRepro4 rep)
    {
      this.Value = val;
      this.mRep = rep;
    }

    [ClousotRegressionTest("postonly")]
    public MichaelRepro4()
      : this(null, null)
    {
    }

    [ClousotRegressionTest("postonly")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 15, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.False, Message = @"assert is false", PrimaryILOffset = 32, MethodILOffset = 0)]
    public void Test()
    {
      var obj = new MichaelRepro4();
      Contract.Assert(obj.Value == null); // true
      Contract.Assert(obj.mRep != null);  // false
    }
  }

  public class MichaelRepro5
  {
    public bool mBool;
    public bool mIndic;

    [ClousotRegressionTest("postonly")]
    public MichaelRepro5()
    { // Ensures: this.mIndic == true
      if (!mBool)
        mIndic = true;    // Unreachable!
    }

    [ClousotRegressionTest("postonly")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 12, MethodILOffset = 0)]
    public void Test()
    {
      var m1 = new MichaelRepro5();
      Contract.Assert(m1.mIndic == true);
    }
  }
}

namespace MafRepros
{
  public class FilterNonNull
  {
    string s;

    [ContractInvariantMethod]
    void ObjectInvariant()
    {
      Contract.Invariant(s != null);
    }

    [ClousotRegressionTest("postonly")]
    public string S
    {
      set
      {
        this.s = value;
      }
      get
      {
        return this.s;
      }
    }

    [ClousotRegressionTest("postonly")]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"assert unproven",PrimaryILOffset=25,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"invariant unproven: s != null",PrimaryILOffset=12,MethodILOffset=41)]
    public FilterNonNull(string s)
    {
      this.S = s;
      Contract.Assert(this.s != null);  // should be top!

      this.S = "ciao";
    }
  }

}

namespace ForumRepros {
  public static class UrlValidator
  {
    /// <summary>
    /// Validates an optional deviceclass.
    /// </summary>
    /// <returns><c>null</c> if no deviceclass is specified, otherwise the deviceclass.</returns>
    /// <exception cref="WebFaultException{String}">Bad request.</exception>
    [ClousotRegressionTest]
    public static byte? ValidateDeviceClass(string deviceclass)
    {
      var result = ValidateDeviceClassInternal(deviceclass);
      byte dummy;
      //Contract.Assert(byte.TryParse(deviceclass, out dummy));

      return result;
    }
 
    /// <summary>Validates the deviceclass.</summary>
    /// <param name="deviceClass">The deviceclass.</param>
    /// <returns>The validated deviceclass.</returns>
    /// <exception cref="WebFaultException{String}">Bad request.</exception>
    [ClousotRegressionTest]
    public static byte ValidateDeviceClassInternal(string deviceClass)
    {
      
      byte dc;
      if (!byte.TryParse(deviceClass, out dc))
      {
        throw new Exception();
      }
      
      return dc;
    }
  }
}
