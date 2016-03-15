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
#define CODE_ANALYSIS // For the SuppressMessage attribute


using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Research.ClousotRegression;
using System.Diagnostics.Contracts;
using System.Collections.Generic;

namespace TestWarningMasking
{
  public class NotMasked
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"Possibly calling a method on a null reference 'o'", PrimaryILOffset = 2, MethodILOffset = 0)]
      [RegressionOutcome("Contract.Requires(o != null);")]
    public string Nonnull(object o)
    {
      return o.ToString();
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"The length of the array may be negative", PrimaryILOffset = 2, MethodILOffset = 0)]
      [RegressionOutcome("Contract.Requires(0 <= x);")]
    public decimal[] ArrayCreation(int x)
    {
      return new decimal[x];
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"Array access might be below the lower bound", PrimaryILOffset = 3, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"Array access might be above the upper bound", PrimaryILOffset = 3, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"Possible use of a null array 's'", PrimaryILOffset = 3, MethodILOffset = 0)]
    [RegressionOutcome("Contract.Requires(s != null);")]
    [RegressionOutcome("Contract.Requires(0 <= index);")]
    [RegressionOutcome("Contract.Requires(index < s.Length);")]
    public string ArrayAccess(string[] s, int index)
    {
      return s[index];
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"assert unproven", PrimaryILOffset = 8, MethodILOffset = 0)]
    [RegressionOutcome("Contract.Requires(b >= 0);")]   
    public void Assertion(sbyte b)
    {
      Contract.Assert(b >= 0);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"requires unproven: o != null", PrimaryILOffset = 8, MethodILOffset = 3)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as receiver)", PrimaryILOffset = 3, MethodILOffset = 0)]
    [RegressionOutcome("Contract.Requires(o != null);")]
    public void Precondition(object o)
    {
      Precondition_Internal(o);
    }


    private void Precondition_Internal(object o)
    {
      Contract.Requires(o != null);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"ensures unproven: Contract.Result<int>() < -222", PrimaryILOffset = 13, MethodILOffset = 24)]
      [RegressionOutcome("Contract.Requires(k < -222);")]
    public int Postcondition(int k)
    {
      Contract.Ensures(Contract.Result<int>() < -222);

      return k;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"Array access might be below the lower bound", PrimaryILOffset = 10, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"Array access might be above the upper bound", PrimaryILOffset = 10, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"Possibly calling a method on a null reference 'arr'", PrimaryILOffset = 2, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as array)", PrimaryILOffset = 10, MethodILOffset = 0)]
    [RegressionOutcome("Contract.Requires(arr != null);")]
    [RegressionOutcome("Contract.Requires(0 <= z);")]
    [RegressionOutcome("Contract.Requires(z < arr.Length);")]
    public static string MaskFamily(string[] arr, int z)
    {
      var v = arr.ToString();

      return arr[z] + v;
    }
  }

  public class Masked
  {
    [ClousotRegressionTest]
    [SuppressMessage("Microsoft.Contracts", "Nonnull-2-0")]
    public string Nonnull(object o)
    {
      return o.ToString();
    }

    [ClousotRegressionTest]
    [SuppressMessage("Microsoft.Contracts", "ArrayCreation-2-0")]
    public decimal[] ArrayCreation(int x)
    {
      return new decimal[x];
    }

    [ClousotRegressionTest]
    [SuppressMessage("Microsoft.Contracts", "Nonnull-3-0")]
    [SuppressMessage("Microsoft.Contracts", "ArrayLowerBound-3-0")]
    [SuppressMessage("Microsoft.Contracts", "ArrayUpperBound-3-0")]
    public string ArrayAccess(string[] s, int index)
    {
      return s[index];
    }

    [ClousotRegressionTest]
    [SuppressMessage("Microsoft.Contracts", "Assert-8-0")]
    public void Assertion(sbyte b)
    {
      Contract.Assert(b >= 0);
    }

    [ClousotRegressionTest]
    [SuppressMessage("Microsoft.Contracts", "Requires-8-3")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as receiver)", PrimaryILOffset = 3, MethodILOffset = 0)]
    public void Precondition(object o)
    {
      Precondition_Internal(o);
    }

    private void Precondition_Internal(object o)
    {
      Contract.Requires(o != null);
    }

    [ClousotRegressionTest]
    [SuppressMessage("Microsoft.Contracts", "Ensures-13-24")]
    public int Postcondition(int k)
    {
      Contract.Ensures(Contract.Result<int>() < -222);

      return k;
    }

    [ClousotRegressionTest]
    [SuppressMessage("Microsoft.Contracts", "Nonnull")]
    [SuppressMessage("Microsoft.Contracts", "ArrayLowerBound")]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"Array access might be above the upper bound", PrimaryILOffset = 10, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"valid non-null reference (as array)", PrimaryILOffset = 10, MethodILOffset = 0)]
      [RegressionOutcome("Contract.Requires(z < arr.Length);")]
    public static string MaskFamily(string[] arr, int z)
    {
      var v = arr.ToString();

      return arr[z] + v;  // upper bound access is not masked
    }
  }

  [SuppressMessage("Microsoft.Contracts", "Nonnull")]
  [SuppressMessage("Microsoft.Contracts", "ArrayCreation")]
  class MaskedClass
  {
    [ClousotRegressionTest]
    public static string Null(object o)
    {
      return o.ToString();
    }

    [ClousotRegressionTest]
    public static int[] NewArray(int size)
    {
      return new int[size];
    }
  }
}

namespace TestPreconditionWarningMask
{

  public class RequiresAtCallMaskingTest
  {
 [ClousotRegressionTest]
    [SuppressMessage("Microsoft.Contracts", "RequiresAtCall-Contract.ForAll(xs, x => x != null)")]
 [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Possibly calling a method on a null reference 'xs'",PrimaryILOffset=2,MethodILOffset=0)]  
   [RegressionOutcome("Contract.Requires(xs != null);")]
  static public void ReadList(List<string> xs)
    {
      Contract.Requires(xs.Count > 10);                       // not masked
      Contract.Requires(Contract.ForAll(xs, x => x != null)); // masked

        // does nothing
    }
  }

  public class F
  {
 [ClousotRegressionTest]
 [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"requires unproven: xs.Count > 10",PrimaryILOffset=11,MethodILOffset=15)]
   [RegressionOutcome("Contract.Requires(ys.Count > 10);")]
    public void M(List<string> ys)
    {      
      Contract.Requires(ys != null);

      RequiresAtCallMaskingTest.ReadList(ys);
    }

 [ClousotRegressionTest]
 [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"requires unproven: xs.Count > 10",PrimaryILOffset=11,MethodILOffset=2)]
   [RegressionOutcome("Contract.Requires(lista.Count > 10);")]
    public void K(List<string> lista)
    {
      RequiresAtCallMaskingTest.ReadList(lista);
    }
  }

  [ContractClass(typeof(SomeInterfaceContracts))]
  public interface SomeInterface
  {
    [SuppressMessage("Microsoft.Contracts", "RequiresAtCall-myVarName > 123")]
    void DoSomething(int x);
  }

  [ContractClassFor(typeof(SomeInterface))]
  abstract class SomeInterfaceContracts
    : SomeInterface
  {
    #region SomeInterface Members

    void SomeInterface.DoSomething(int myVarName)
    {
      Contract.Requires(myVarName > 123);   // masked
    }

    #endregion
  }

  public class UseMyInterface
  {
 [ClousotRegressionTest]
 [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Possibly calling a method on a null reference 's'",PrimaryILOffset=3,MethodILOffset=0)]
   [RegressionOutcome("Contract.Requires(s != null);")]
    public void CallDoSomething(SomeInterface s, int value)
    {
      s.DoSomething(value);
    }
  }
}

namespace UnMaskedPostcondition
{
  [ContractClass(typeof(IContracts))]
  public interface I
  {
    int F(int x);
  }

  [ContractClassFor(typeof(I))]
  abstract public class IContracts : I
  {
    int I.F(int x)
    {
      Contract.Ensures(Contract.Result<int>() > 123);

      throw new NotImplementedException();
    }
  }

  public class FilterI : I
  {
    // unfiltered warning
    [ClousotRegressionTest]
    [RegressionOutcome("Contract.Assume(x > 123);")]
    [RegressionOutcome("Contract.Requires(!(this is UnMaskedPostcondition.FilterI) || x > 123)")] // F: there is some extra info associated that we do not capture in the regression output
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"ensures unproven: Contract.Result<int>() > 123",PrimaryILOffset=10,MethodILOffset=6)]
    public int F(int x)
    {
      return x;
    }
  }
}

namespace MaskedPostcondition
{
  [ContractClass(typeof(IContracts))]
  public interface I
  {
    [SuppressMessage("Microsoft.Contracts", "EnsuresInMethod-Contract.Result<int>() > 123")]
    int F(int x);
  }

  [ContractClassFor(typeof(I))]
  abstract public class IContracts : I
  {
    int I.F(int x)
    {
      Contract.Ensures(Contract.Result<int>() > 123);

      throw new NotImplementedException();
    }
  }

  public class FilterI : I
  {
    // Filtered warning
    [ClousotRegressionTest]
    //[RegressionOutcome("Contract.Requires(!(this is UnMaskedPostcondition.FilterI) || x > 123)")] // F: there is some extra info associated that we do not capture in the regression output
    public int F(int x)
    {
      return x;
    }
  }
}

namespace ObsoleteSuppressWarnings
{
  public class WarningMask
  {
    // Masked Warning
    [ClousotRegressionTest]
    [SuppressMessage("Microsoft.Contracts", "Nonnull-2-0")]
    public int Masked(string s)
    {
      return s.Length;
    }
    
    // Warning not Masked, the suppress warning message is unused, we emit a string to the user
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possibly calling a method on a null reference 's'. The static checker determined that the condition 's != null' should hold on entry. Nevertheless, the condition may be too strong for the callers. If you think it is ok, add a precondition to document it: Contract.Requires(s != null);",PrimaryILOffset=10,MethodILOffset=0)]
    [SuppressMessage("Microsoft.Contracts", "Nonnull-2-0")]
    [RegressionOutcome("Method ObsoleteSuppressWarnings.WarningMask.NotMasked_Warning: Unused suppression of a warning message: [SuppressMessage(\"Microsoft.Contracts\", \"Nonnull-2-0\")]")]
    public int NotMasked_Warning(bool b, string s)
    {
      if (b)
	return s.Length;
      else
	return 0;
    }

    // Error not Masked, the suppress warning message is unused, we emit a string to the user
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.False,Message="Calling a method on a null reference 's'. The static checker determined that the condition 's != null' should hold on entry. Nevertheless, the condition may be too strong for the callers. If you think it is ok, add a precondition to document it: Contract.Requires(s != null);",PrimaryILOffset=13,MethodILOffset=0)]      
    [SuppressMessage("Microsoft.Contracts", "Nonnull-2-0")]
    [RegressionOutcome("Method ObsoleteSuppressWarnings.WarningMask.NotMasked_Error: Unused suppression of a warning message: [SuppressMessage(\"Microsoft.Contracts\", \"Nonnull-2-0\")]")]
    public int NotMasked_Error(string s)
    {
      if (s == null)
	return s.Length;
      else
	return 0;
    }    
  }

  [SuppressMessage("Microsoft.Contracts", "Nonnull")]
  public class WarningMaskWithClassWideAttribute
  {
    // Masked Warning
    [ClousotRegressionTest]
    [SuppressMessage("Microsoft.Contracts", "Nonnull-1-0")]
      [RegressionOutcome("Method ObsoleteSuppressWarnings.WarningMaskWithClassWideAttribute.Masked: Unused suppression of a warning message: [SuppressMessage(\"Microsoft.Contracts\", \"Nonnull-1-0\")]")]
    public int Masked(string s)
    {
      return s.Length;
    }
    
    // Warning masked because of the class attribute, but the suppress warning message is unused so we emit a message to the user
    [ClousotRegressionTest]
    [SuppressMessage("Microsoft.Contracts", "Nonnull-1-0")]
      [RegressionOutcome("Method ObsoleteSuppressWarnings.WarningMaskWithClassWideAttribute.NotMasked_Warning: Unused suppression of a warning message: [SuppressMessage(\"Microsoft.Contracts\", \"Nonnull-1-0\")]")]
    public int NotMasked_Warning(bool b, string s)
    {
      if (b)
	return s.Length;
      else
	return 0;
    }

    // Error masked because of the class attribute, the suppress warning message is unused so we emit a message to the user
    [ClousotRegressionTest]
    [SuppressMessage("Microsoft.Contracts", "Nonnull-1-0")]
      [RegressionOutcome("Method ObsoleteSuppressWarnings.WarningMaskWithClassWideAttribute.NotMasked_Error: Unused suppression of a warning message: [SuppressMessage(\"Microsoft.Contracts\", \"Nonnull-1-0\")]")]
    public int NotMasked_Error(string s)
    {
      if (s == null)
	return s.Length;
      else
	return 0;
    }    
  }

}

namespace ObjectInvariantMasking
{
  public class NotMasked
  {
    private object field;
    
    [ContractInvariantMethod]
    void ObjectInvariant() 
    {
      Contract.Invariant(this.field != null);
    }
 
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as receiver)",PrimaryILOffset=1,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=10,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"invariant unproven: this.field != null",PrimaryILOffset=13,MethodILOffset=16)]
    [RegressionOutcome("Contract.Requires(s != null);")]
    public NotMasked(string s)
    {
      this.field = s;
    }

    [ClousotRegressionTest]    
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=3,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"invariant unproven: this.field != null",PrimaryILOffset=13,MethodILOffset=8)]
    [RegressionOutcome("Contract.Requires(z != null);")]
    public void Set(int[] z)
    {
      this.field = z;
    }
  }


  [SuppressMessage("Microsoft.Contracts", "InvariantInMethod-this.field != null")]
  public class Masked
  {
    private object field;
    
    [ContractInvariantMethod]
    void ObjectInvariant() 
    {
      Contract.Invariant(this.field != null);
    }
 
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as receiver)",PrimaryILOffset=1,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=10,MethodILOffset=0)]    
    public Masked(string s)
    {
      this.field = s;
    }

    [ClousotRegressionTest]    
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=3,MethodILOffset=0)]
    public void Set(int[] z)
    {
      this.field = z;
    }
  }
}

namespace ShortCutForEnsuresMasking
{
  public class MaskEnsures
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"ensures unproven: Contract.Result<int>() >= z * 3 + 12 * z * z",PrimaryILOffset=21,MethodILOffset=32)]
    [RegressionOutcome("Contract.Requires(z >= (z * 3 + (12 * z) * z));")]
    public int MaskMe(int z)
    {
      Contract.Ensures(Contract.Result<int>() >= z * 3 + 12 * z * z);

      return z;
    }

    [ClousotRegressionTest]
    [SuppressMessage("Microsoft.Contracts", "Ensures-Contract.Result<int>() >= z * 3 + 12 * z * z")]
    public int Masked(int z)
    {
      Contract.Ensures(Contract.Result<int>() >= z * 3 + 12 * z * z);

      return z;      
    }
  }
}

namespace AlexeyR
{
  public class ShouldMaskPreconditionSuggestion
  {
    [ClousotRegressionTest]
    static void Caller(object arg)
    {
      // static checker suggests to add precondition here
      Callee(arg);
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Contracts", "RequiresAtCall-HardToProveCond(arg)")]
    static void Callee(object arg)
    {
      Contract.Requires(HardToProveCond(arg));
    }

    [Pure]
    static bool HardToProveCond(object arg)
    {
      return true;
    }

  }
}

namespace DaveSexton
{
  public class Class1
  {
    [SuppressMessage("Microsoft.Contracts", "Contract.Requires(o != null);")]
    public int Precondition(object o)
    {
      if (o.GetHashCode() > 123)
      {
        return -1;
      }

      return 4;
    }
  }
}

/*

// We do not sort warnings in regression ==> we cannot test it

namespace GroupBottoms
{
  public class TooManyUnreached
  {
    public int x;
    public int y;
    public string s;

    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(x > 0);
      Contract.Invariant(y > 0);
      Contract.Invariant(s.Length > 0);
    }

	[ClousotRegressionTest]
	public TooManyUnreached()
	{
	}
	
	[ClousotRegressionTest]	
    public int foo()
    {
      if (this.x == 0)
      {
        return 0;
      }

      return this.x + this.y;
    }
  }

}
*/