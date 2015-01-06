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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using Microsoft.Research.ClousotRegression;
using System.Runtime.InteropServices;
using System.Reflection;

namespace PreInference
{
  class StraightLineTests
  { 
    [ClousotRegressionTest]
    [RegressionOutcome("Contract.Requires(s != null);")]
    public int StraightForwardNotNull(string s)
    {
      return s.Length;
    }
	
    [ClousotRegressionTest]
	[RegressionOutcome("Contract.Requires((x + 2) > 0);")]
	[RegressionOutcome("Consider replacing the expression (x + 2) > 0 with an equivalent, yet not overflowing expression. Fix: x > (0 - 2)")]
	public void Simplification1(int x)
    {
      int z = x + 1;
      Contract.Assert(z + 1 > 0);
    }

    [ClousotRegressionTest]
	[RegressionOutcome("Contract.Requires(x > 0);")]
	public void Simplification2(int x)
    {
      int z = x + 1;
      Contract.Assert(z - 1 > 0);
    }

    [ClousotRegressionTest]
	[RegressionOutcome("Contract.Requires(x > 0);")]
    public void Simplification3(int x)
    {
      int z = x - 1;
      Contract.Assert(z + 1 > 0);
    }

    [ClousotRegressionTest]
	[RegressionOutcome("Contract.Requires((x - 2) > 0);")]	
    public void Simplification4(int x)
    {
      int z = x - 1;
      Contract.Assert(z - 1 > 0);
    }
	
    [RegressionOutcome("Contract.Requires(arr != null);")]
    [RegressionOutcome("Contract.Requires(2 < arr.Length);")]
    [ClousotRegressionTest]
    public void Redundant(byte[] arr)
    {
      arr[0] = 10;
      arr[2] = 123;
    }

    [ClousotRegressionTest]
    [RegressionOutcome("Contract.Requires(arr != null);")]
    [RegressionOutcome("Contract.Requires(0 <= k);")]
    [RegressionOutcome("Contract.Requires((k + 2) < arr.Length);")]
    public void Redundant(byte[] arr, int k)
    {
      arr[k + 0] = 10;
      arr[k + 2] = 123;
    }

    [ClousotRegressionTest]
    [RegressionOutcome("Contract.Requires(arr != null);")]
    [RegressionOutcome("Contract.Requires(0 <= k);")]
    [RegressionOutcome("Contract.Requires((k + 2) < arr.Length);")]
    [RegressionOutcome("Possible off-by one: did you meant indexing with k instead of k + 1?. Fix: k")]
    public void RedundantWithPostfixIncrement(byte[] arr, int k)
    {
      arr[k] = 1;
      var k1 = k + 1;
      arr[k1] = 123;
      var k2 = k1 + 1;
      arr[k2] = 2;
    }

    [ClousotRegressionTest]
    [RegressionOutcome("Contract.Requires((k + 2) < s.Length);")]
    public void RedundantWithPreFixIncrementOfParameter(int k, string[] s)
    {
      Contract.Requires(s != null);
      Contract.Requires(k >= 0);

      s[k] = "1";
      s[++k] = "2;";
      s[++k] = "3";
    }

  }

  public class ControlFlowInference
  {
    [ClousotRegressionTest]
    [RegressionOutcome("Contract.Requires((x <= 0 || s != null));")]
    public int Conditional(int x, string s)
    {
      if (x > 0)
      {
        return s.Length;
      }

      return x;
    }

    [ClousotRegressionTest]
    [RegressionOutcome("Contract.Requires(obj != null);")]
    public void NotNull(int z, object obj)
    {
      if (z > 0)
        obj.GetHashCode();
      obj.GetType();
    }

    // we do not infer the precondition with this system as the propagations stops because of the local 
    // Also no text fix
    // However we have a suggestion
    [ClousotRegressionTest]
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

    // no precondition to infer
    [ClousotRegressionTest]
    //[RegressionOutcome("Contract.Assume(tmp == 29);")] // we moved the some assume suggestions into codefixes
	[RegressionOutcome("This condition should hold: tmp == 29. Add an assume, a postcondition to method DaysInMonth, or consider a different initialization. Fix: Add (after) Contract.Assume(tmp == 29);")]
    public int Local(int z)
    {
      var tmp = DateTime.DaysInMonth(2012, 12);

      Contract.Assert(tmp == 29);

      return tmp;
    }

    // infer z == 29: the guard is implied by the precondition
    [ClousotRegressionTest]
    [RegressionOutcome("Contract.Requires(z == 29);")]
    public int Local2(int z, int k)
    {
      Contract.Requires(k >= 0);

      if (k >= -12)
      {
        Contract.Assert(z == 29);
      }
      return z;
    }


    [ClousotRegressionTest]
	[RegressionOutcome("Contract.Requires(!(b));")] // if b, the we get an error
	[RegressionOutcome("Contract.Requires(x > 0);")] // if x <= 0, we get an error no matter what
    [RegressionOutcome("Consider initializing x with a value other than -123 (e.g., satisfying x > 0). Fix: 1;")]
    public int Branch(int x, bool b)
    {
      if (b)
      {
        x = -123;
      }
      Contract.Assert(x > 0);
      
      return x;
    }

    // nothing to infer
    [ClousotRegressionTest]
    [RegressionOutcome("Consider initializing x with a value other than 12 (e.g., satisfying x < 0). Fix: -1;")]
    public void Branch2(int x)
    {
      if (Foo("ciao"))
      {
        x = -12;
      }
      else
      {
        x = 12;
      }

      Contract.Assert(x < 0);
    }

    [ClousotRegressionTest]
    [RegressionOutcome("Contract.Requires(s != null);")]
    public void Branch3(int x, ref int z, string s)
    {
      // we need the join here
      if (x > 0)
      {
        z = 11;
      }
      else
      {
        z = 122;
      }
      Contract.Assert(s != null);
    }

    // We need a join here in which we get rid of the path
    [ClousotRegressionTest]
    [RegressionOutcome("Contract.Requires(s != null);")] 
    public void Branch4(int x, ref int z, string s)
    {
      if (x > 0)
      {
        z = 11;
      }
      else
      {
        z = 122;
      }
      if (x % 2 == 0)
      {
        z += 1;
      }
      else
      {
        z -= 1;
      }
      Contract.Assert(s != null);
    }

    [ClousotRegressionTest]
    // We should not infer "Contract.Requires(((x > 0 || (x % 2) != 0) || s != null));" because this means that simplification failed
    [RegressionOutcome("Contract.Requires(((x % 2) != 0 || s != null));")]
    public void Branch5(int x, ref int z, string s)
    {
      if (x > 0)
      {
        z = 11;
      }
      else
      {
        z = 122;
      }
      if (x % 2 == 0)
      {
        z += 1;
      }
      else
      {
        return;
      }
      Contract.Assert(s != null);
    }

    public bool Foo(string s)
    {
      return s != null;
    }
    

    [ClousotRegressionTest]
	public void Test_RequiresAnInt_Positive()
    {
      RequiresAnInt(12);
    }

    [ClousotRegressionTest]
	[RegressionOutcome("Contract.Requires(!((" + "@\"12\"" +" as System.Int32)));")]
    public void Test_RequiresAnInt_Negative()
    {
      RequiresAnInt("12");
    }
	
    [ClousotRegressionTest]
	// We do not suggest anymore requires from "throw "-> "assert false" transformations
	//    [RegressionOutcome("Contract.Requires(!((s as System.Int32)));")]
    public void RequiresAnInt(object s)
    {
      if ((s is Int32))
      {
        throw new ArgumentException();
      }
    }
	
	[ClousotRegressionTest]
	[RegressionOutcome("Contract.Requires((i < 0 || j > 0));")]
	public int Mixed(int i, int j)
    {
      if (i < 0)
        throw new ArgumentOutOfRangeException("should be >= 0!!!");

      Contract.Assert(j > 0); 

      return i * 2;
    }
  }
  
  public class PreInferenceWithLoops
  {
    // infer input <= 0 ==> input == 0
    // (equivalent to input >= 0)
    // Cannot be inferred with precondition over all the paths
    [ClousotRegressionTest]
    [RegressionOutcome("Contract.Requires(input >= 0);")]
    public void Loop(int input)
    {
      var j = 0;
      for (; j < input; j++)
      {
      }

      Contract.Assert(input == j);
    }

    // infer x != 0 ==> x > 0
    // Cannot be inferred with precondition over all the paths
    [ClousotRegressionTest]
    [RegressionOutcome("Contract.Requires(x >= 0);")]
    public void Loop2(int x)
    {
      while (x != 0)
      {
        Contract.Assert(x > 0);
        x--;
      }
    }

    // infer strings.Length > 0
    // The other precondition is \exists j \in [0, strings.Length). strings[j] == null, but it is way out of reach at the moment
    // Cannot be inferred with precondition over all the paths
    [ClousotRegressionTest]
    [RegressionOutcome("Contract.Requires(0 < strings.Length);")]
    public int Loop3(string[] strings)
    {
      Contract.Requires(strings != null);

      int i = 0;
      while (strings[i] != null)
      {
        i++;
      }

      return i;
    }

    [ClousotRegressionTest]
//    [RegressionOutcome("Contract.Requires((0 >= m1 || 0 < f));")] 	// 3/20/2014: Now we infer a better precondition
	[RegressionOutcome("Contract.Requires((0 >= m1 || m1 <= f));")]
    public void Loop4(int m1, int f)
    {
      int i = 0;
      while (i < m1)
      {
        Contract.Assert(i < f);
        i++;
      }
    }

    [ClousotRegressionTest]
//    [RegressionOutcome("Contract.Requires((0 >= m1 || 0 < f));")] // Fixed
	[RegressionOutcome("Contract.Requires((0 >= m1 || m1 <= f));")]
    public void Loop4WithPreconditionNotStrongEnough(int m1, int f)
    {
      Contract.Requires(m1 >= 0); // as we may have the equality, then we may never execute the loop

      int i = 0;
      while (i < m1)
      {
        Contract.Assert(i < f);
        i++;
      }
    }

    [ClousotRegressionTest]
	[RegressionOutcome("Contract.Requires(m1 <= f);")]
    public void Loop4WithPrecondition(int m1, int f)
    {
      Contract.Requires(m1 > 0); // this simplifies the precondition above, as we know now we always execute the loop at least once

      int i = 0;
      while (i < m1)
      {
        Contract.Assert(i < f);
        i++;
      }
    }

    [ClousotRegressionTest]
    [RegressionOutcome("Contract.Requires(strs != null);")]
    [RegressionOutcome("Contract.Requires(0 <= start);")]
    [RegressionOutcome("Contract.Requires(start < strs.Length);")]
    public int SearchZero(string[] strs, int start)
    {
      while (strs[start] != null)
      {
        start++;
      }

      return start;
    }

    [ClousotRegressionTest]
    [RegressionOutcome("Contract.Requires(x == 12);")] 
    public void AfterWhileLoop_ConditionAlwaysTrue(int x, int z)
    {
      Contract.Requires(z >= 0);

      while (z > 0)
      {
        z--;
      }
      // here z == 0;
      // uses this information to drop the z <= 0 antecendent

      if (z == 0)
      {
        Contract.Assert(x == 12);
      }
    }

    [ClousotRegressionTest]
    [RegressionOutcome("Contract.Requires(x == 12);")] 
    public void AfterWhileLoop_ConditionAlwaysTrueWithStrongerPrecondition(int x, int z)
    {
      Contract.Requires(z > 0);

      while (z > 0)
      {
        z--;
      }

      // here z == 0;
      if (z == 0)
      {
        Contract.Assert(x == 12);
      }
    }


    // infers x <0 || y > 0
    [ClousotRegressionTest]
    [RegressionOutcome("Contract.Requires((x < 0 || y > 0));")]
    public void SrivastavaGulwaniPLDI09(int x, int y)
    {
      while (x < 0)
      {
        x = x + y;
        y++;
      }
      Contract.Assert(y > 0);
    }
  }

  public class SomeClass
  {
    public string[] SomeString;
  }

  public class PreconditionOrder
  {
    [ClousotRegressionTest]
    [RegressionOutcome("Contract.Requires(((s == null || s.SomeString == null) || 0 <= index));")]
    [RegressionOutcome("Contract.Requires(((s == null || s.SomeString == null) || index < s.SomeString.Length));")]
	[RegressionOutcome("Consider strengthening the null check. Fix: Add && index < s.SomeString.Length")]
    public static string SomeMethod(SomeClass s, int index)
    {
      if (s != null)
      {
        if (s.SomeString != null)
          return s.SomeString[index];
      }

      return null;
    }
  }
}

namespace mscorlib
{
  public class MyAppDomainSetup
  {
    public string PrivateBinPath;
    public object DeveloperPath;
    public string[] Value
    {
      get
	{
	  return new string[0x12];
	}
    }

    [ClousotRegressionTest] 
    [RegressionOutcome("Contract.Requires((blob == null || 0 < blob.Length));")]
	[RegressionOutcome("Consider strengthening the null check. Fix: Add && 0 < blob.Length")]
    private static object Deserialize(byte[] blob)
    {
      if (blob == null)
      {
        return null;
      }
      if (blob[0] == 0)
      {
        return 1;
      }

      return null;
    }

    
    internal void SetupFusionContext(IntPtr fusionContext, MyAppDomainSetup oldInfo)
    {
      throw new NotImplementedException();
    }
  }

  public class AppDomain
  {
    // Here we had 2 bugs in the pretty printing:
    //    1. infer: info != null && info != null && info != null ...
    //    2. infer (info != null && (info.DeveloperPath != null ==> info != null)
    // We should only infer info != null
    // Note: we do not analyze this.Value in the regression run, this is why we get the messages 
    [ClousotRegressionTest]
    [RegressionOutcome("Contract.Requires(info != null);")]
//  [RegressionOutcome("Contract.Requires((oldInfo != null || info.Value != null));")]
//  [RegressionOutcome("Contract.Requires((oldInfo != null || 0 < info.Value.Length));")]
  //  [RegressionOutcome("Contract.Requires((oldInfo != null || 5 < info.Value.Length));")]
//  [RegressionOutcome("Contract.Assume(info.Value != null);")] // We also infer this assume - we moved the inference of assume to the codefixes
//  [RegressionOutcome("This condition should hold: info.Value != null. Add an assume, a postcondition, or consider a different initialization. Fix: Add (after) Contract.Assume(info.Value != null);")]
    [RegressionOutcome("Contract.Requires((oldInfo != null || 5 < ((mscorlib.MyAppDomainSetup)info).Value.Length));")]
	[RegressionOutcome("Contract.Requires((oldInfo != null || 0 < ((mscorlib.MyAppDomainSetup)info).Value.Length));")]
    [RegressionOutcome("Contract.Requires((oldInfo != null || ((mscorlib.MyAppDomainSetup)info).Value != null));")]
    //[RegressionOutcome("This condition should hold: ((mscorlib.MyAppDomainSetup)info).Value != null. Add an assume, a postcondition, or consider a different initialization. Fix: Add (after) Contract.Assume(((mscorlib.MyAppDomainSetup)info).Value != null);")]
	[RegressionOutcome("This condition should hold: ((mscorlib.MyAppDomainSetup)info).Value != null. Add an assume, a postcondition to method get_Value, or consider a different initialization. Fix: Add (after) Contract.Assume(((mscorlib.MyAppDomainSetup)info).Value != null);")]
    // When we infer a test strengthening, we check that at least one of the vars in the fix is in the condition. We do not extend this for accesspaths
    //[RegressionOutcome("Consider strengthening the guard. Fix: Add && 1 < info.Value.Length")]
    [RegressionOutcome("Possible off-by one: did you meant indexing with 0 instead of 1?. Fix: 0")]
    private void SetupFusionStore(MyAppDomainSetup info, MyAppDomainSetup oldInfo)
    {
      if (oldInfo == null)
      {
        if ((info.Value[0] == null) || (info.Value[1] == null))
	  {
	    // dummy
	  }
        if (info.Value[5] == null)
	  {
	    info.PrivateBinPath = RuntimeEnvironment.GetRuntimeDirectory();
	  }
	
        if (info.DeveloperPath == null)
	  {
	    info.DeveloperPath = RuntimeEnvironment.GetRuntimeDirectory();
	  }
      }
      IntPtr fusionContext = this.GetFusionContext();
      info.SetupFusionContext(fusionContext, oldInfo);
    }

    private IntPtr GetFusionContext()
    {
      throw new NotImplementedException();
    } 
  }

  public class LinkedStructure
  {
    public bool HasElementType;
    public LinkedStructure next;

    private LinkedStructure GetElementType()
    {
      return next;
    }

    // nothing to infer
    [ClousotRegressionTest]
    //    [RegressionOutcome("Contract.Assume((elementType.GetElementType()) != null);")]  // we moved the some assume suggestions into codefixes
    // Here we build the left argument by ourselves, as the heap analysis does not have a name for it
    [RegressionOutcome("This condition should hold: elementType.GetElementType() != null. Add an assume, a postcondition to method mscorlib.LinkedStructure.GetElementType(), or consider a different initialization. Fix: Add (after) Contract.Assume(elementType.GetElementType() != null);")]
    internal string SigToString()
    {
      var elementType = this;
      while (elementType.HasElementType)
      {
        elementType = elementType.GetElementType();
      }

      return elementType.ToString();
    }
  }

  public class Guid
  {
    // Here we do not want to generate the trivial precondition hex || !hex || guidChars.Length > ...
    [ClousotRegressionTest]
    [RegressionOutcome("Contract.Requires((!(hex) || guidChars != null));")]
    [RegressionOutcome("Contract.Requires((!(hex) || 0 <= offset));")]
    [RegressionOutcome("Contract.Requires((!(hex) || offset < guidChars.Length));")]
    [RegressionOutcome("Contract.Requires((!(hex) || (offset + 1) < guidChars.Length));")]
    private static int HexsToCharsReduced(char[] guidChars, int offset, int a, int b, bool hex)
    {
      if (hex)
      {
        guidChars[offset++] = 'x';
      }
      if (hex)
      {
        guidChars[offset++] = ',';
      }
      return offset;
    }

    private static char HexToChar(int a)
    {
      a &= 15;
      return ((a > 9) ? ((char)((a - 10) + 0x61)) : ((char)(a + 0x30)));
    }
  }

  public class ActivationAttributeStack
  {
    public  object[] activationAttributes;
    public object[] activationTypes;
    public int freeIndex; /* made the field public to have the precondition inference working */

    // it is ok not to infer a precondition on this.activationAttributes, as we do not have boxed expressions for array loads (which appears in the test below)
    [ClousotRegressionTest]
#if CLOUSOT2
    [RegressionOutcome("Contract.Requires((this.freeIndex == 0 || this.activationTypes != null));")]
    [RegressionOutcome("Contract.Requires((this.freeIndex == 0 || 0 <= (this.freeIndex - 1)));")]
    [RegressionOutcome("Contract.Requires((this.freeIndex == 0 || (this.freeIndex - 1) < this.activationTypes.Length));")]
#else
    [RegressionOutcome("Contract.Requires((!(this.freeIndex) || this.activationTypes != null));")]
    [RegressionOutcome("Contract.Requires((!(this.freeIndex) || 0 <= (this.freeIndex - 1)));")]
    [RegressionOutcome("Contract.Requires((!(this.freeIndex) || (this.freeIndex - 1) < this.activationTypes.Length));")]
    // we infer !(this.freeIndex) instead of (this.freeIndex == 0) because we have some issues with types
#endif
    [RegressionOutcome("Consider strengthening the guard. Fix: Add && (this.freeIndex - 1) < this.activationAttributes.Length")]
    internal void Pop(Type typ)
    {
#pragma warning disable 252
      if ((this.freeIndex != 0) && (this.activationTypes[this.freeIndex - 1] == typ))
      {
        this.freeIndex--;
        this.activationTypes[this.freeIndex] = null;
        this.activationAttributes[this.freeIndex] = null;
#pragma warning restore 252
      }
    }

  }

  public class CustomAttributeData
  {
    public object m_namedArgs;
    public CustomAttributeEncoding[] m_namedParams;
    public MemberInfo[] m_members;
    public Type m_scope;

    // Nothing should be inferred as the assertion depends on a quantified property over the array m_namedParams
    [ClousotRegressionTest]
    public virtual IList<object> get_NamedArguments()
    {
      if (this.m_namedArgs == null)
      {
        if (this.m_namedParams == null)
        {
          return null;
        }
        int index = 0;
        int num4 = 0;
        while (index < this.m_namedParams.Length)
        {
          if (this.m_namedParams[index] != CustomAttributeEncoding.Undefined)
          {
            Contract.Assert(this.m_members != null); // Cannot prove it
            num4++;
          }
          index++;
        }
      }
      return null;
    }

    public enum CustomAttributeEncoding
    {
      Array = 29,
      Boolean = 2,
      Byte = 5,
      Char = 3,
      Double = 13,
      Enum = 85,
      Field = 83,
      Float = 12,
      Int16 = 6,
      Int32 = 8,
      Int64 = 10,
      Object = 81,
      Property = 84,
      SByte = 4,
      String = 14,
      Type = 80,
      UInt16 = 7,
      UInt32 = 9,
      UInt64 = 11,
      Undefined = 0
    }

  }

  public class SimplificationOfPremises
  {
    public int m_RelocFixupCount;
    
    [ClousotRegressionTest]
    [RegressionOutcome("Contract.Requires(0 <= this.m_RelocFixupCount);")]
    internal int[] GetTokenFixups()
    {
      if (this.m_RelocFixupCount == 0)
        {
            return null;
        }
      int[] destinationArray = new int[this.m_RelocFixupCount];
      return destinationArray;
    }
    
    [ClousotRegressionTest]
    [RegressionOutcome("Contract.Requires(z >= 0);")]
    [RegressionOutcome("Consider replacing z < 0. Fix: 0 <= z")]
    public int[] WrongArrayInit(int z)
    {
    // we were not simplifying the premise
      if (z < 0)
      {
        return new int[z];
      }

      return null;
    }

  }
 
 
   public class InferredDisjunction
  {  
    // Nothing to complain, precondition is proven
    [ClousotRegressionTest]
    public void CallFoo()
    {
      Foo(null, -1);
    }

    [ClousotRegressionTest]
    [RegressionOutcome("Contract.Requires((obj == null || z > 0));")]
    public void Foo(object obj, int z)
    {
      if (obj != null)
      {
        Contract.Assert(z > 0);
      }
    }
  }
  
  public class OverflowTesting
  {
    [ClousotRegressionTest]
    [RegressionOutcome("Contract.Requires(elements != null);")]
    [RegressionOutcome("Contract.Requires((nextFree != elements.Length || 0 <= (elements.Length * 2 + 1)));")]
    public void Add(int[] elements, int nextFree, int x)
    {
      Contract.Requires(0 <= nextFree);
      Contract.Requires(nextFree <= elements.Length);

      if (nextFree == elements.Length)
      {
        var tmp = new int[nextFree * 2 + 1]; // This may overflow and get negative. We suggest a precondition for that
        Array.Copy(elements, tmp, elements.Length);
        elements = tmp;
      }
      elements[nextFree] = x;
      nextFree++;
    }
  }

  public class MoreComplexInference
  {
    [ClousotRegressionTest]
//    [RegressionOutcome("Contract.Requires((length + offset) <= str.Length);")] // Now we have a better requires

    // This is wrong!!! We should not correct the preconditions for the callers
	//[RegressionOutcome("Did you meant i <= str.Length instead of i < str.Length?. Fix: i <= str.Length")]

	[RegressionOutcome("Contract.Requires((offset >= (length + offset) || (length + offset) <= str.Length));")]
	[RegressionOutcome("Consider adding the assumption (length + offset) <= str.Length. Fix: Add Contract.Assume((length + offset) <= str.Length);")]
#if CLOUSOT2
    [RegressionOutcome("This condition should hold: i < str.Length. Add an assume, a postcondition to method System.String.Length.get(), or consider a different initialization. Fix: Add (after) Contract.Assume(i < str.Length);")]
    [RegressionOutcome("This condition should hold: (length + offset) <= str.Length. Add an assume, a postcondition to method System.String.Length.get(), or consider a different initialization. Fix: Add (after) Contract.Assume((length + offset) <= str.Length);")]
#else
	[RegressionOutcome("This condition should hold: i < str.Length. Add an assume, a postcondition to method System.String.get_Length, or consider a different initialization. Fix: Add (after) Contract.Assume(i < str.Length);")]
  [RegressionOutcome("This condition should hold: (length + offset) <= str.Length. Add an assume, a postcondition to method System.String.get_Length, or consider a different initialization. Fix: Add (after) Contract.Assume((length + offset) <= str.Length);")]
#endif
    public static int GetChar(string str, int offset, int length)
    {
      Contract.Requires(str != null);
      Contract.Requires(offset >= 0);
      Contract.Requires(length >= 0);
      Contract.Requires(length < str.Length);
      Contract.Requires(offset < str.Length);

      int val = 0;
      for (int i = offset; i < length + offset; i++)
      {      
        char c = str[i]; // Method call, not removed by the compiler
      }

      return val;
    }

  }
  
   public class False
  {
    [ClousotRegressionTest]
    [RegressionOutcome("Contract.Requires(x != null);")]
    [RegressionOutcome("Contract.Requires(y >= 0);")]
    [RegressionOutcome("Contract.Requires(z > y);")]
    public void Foo(object x, int y, int z)
    {
      if(x == null) // x != null
        Contract.Assert(false);

      if(y < 0)     // infer: x == null || y >= 0 simplified to  y >= 0
        Contract.Assert(false);

      if(z <= y)    // infer: x == null || y < 0 || z > y  simplified to z > y
        Contract.Assert(false);
    }
    
    [ClousotRegressionTest]
    [RegressionOutcome("Contract.Requires(capacity >= 0);")]
    [RegressionOutcome("Contract.Requires(length >= 0);")]
    [RegressionOutcome("Contract.Requires(startIndex >= 0);")]
    [RegressionOutcome("Contract.Requires((value == null || startIndex <= (value.Length - length)));")]
    [RegressionOutcome("Consider adding the assumption startIndex <= (value.Length - length). Fix: Add Contract.Assume(startIndex <= (value.Length - length));")]
#if CLOUSOT2
    [RegressionOutcome("This condition should hold: startIndex <= (value.Length - length). Add an assume, a postcondition to method System.String.Length.get(), or consider a different initialization. Fix: Add (after) Contract.Assume(startIndex <= (value.Length - length));")]
#else
    [RegressionOutcome("This condition should hold: startIndex <= (value.Length - length). Add an assume, a postcondition to method System.String.get_Length, or consider a different initialization. Fix: Add (after) Contract.Assume(startIndex <= (value.Length - length));")] 
#endif
	public static void StringBuilderSmallRepro(string value, int startIndex, int length, int capacity)
    {
      if (capacity < 0)
      {
        Contract.Assert(false);
      }
      if (length < 0)
      {
        Contract.Assert(false);
      }
      if (startIndex < 0)
      {
        Contract.Assert(false);
      }
      if (value == null)
      {
        value = string.Empty;
      }
      if (startIndex > (value.Length - length))
      {
        Contract.Assert(false);
      }
    }
  }
}

namespace ReplaceExpressionsByMoreVisibleGetters
{
  public class PublicGetterRepro
  {
    private int a, b;

    public PublicGetterRepro(int a, int b)
    {
      this.a = a;
      this.b = b;
    }

    [ClousotRegressionTest]
    public int Length 
    { 
        get 
        { 
          Contract.Ensures(Contract.Result<int>() == a + b);
          return a + b; 
        } 
    }

    [ClousotRegressionTest]
    
	// We do not infer it anymore
	//[RegressionOutcome("Contract.Requires(i <= this.Length);")]
#if CLOUSOT2
    [RegressionOutcome("This condition should hold: i <= this.Length. Add an assume, a postcondition to method ReplaceExpressionsByMoreVisibleGetters.PublicGetterRepro.Length.get(), or consider a different initialization. Fix: Add (after) Contract.Assume(i <= this.Length);")]
#else
    [RegressionOutcome("This condition should hold: i <= this.Length. Add an assume, a postcondition to method ReplaceExpressionsByMoreVisibleGetters.PublicGetterRepro.get_Length, or consider a different initialization. Fix: Add (after) Contract.Assume(i <= this.Length);")]
#endif
    public int Precondition(int i)
    {
      // we should infer i <= this.Length, instead of i <= a + b
      if (i > this.Length)
      {
        throw new ArgumentOutOfRangeException();
      }

      return i + 1;
    }
  }
}

namespace UserRepro
{
   public class RobAshton
   {
     [ClousotRegressionTest]
     [RegressionOutcome("Contract.Requires(((a % b) == 0 || a != 0));")]
     private static bool AreBothWaysDivisibleBy2(int a, int b)
     {
       Contract.Requires(b != 0);
       return a % b == 0 || b % a == 0;
     }
   }
   
   public class Maf
   {
     [ClousotRegressionTest]
     [RegressionOutcome("Contract.Requires(((t as System.Collections.IEnumerable) != null || tabs >= 0));")]
     [RegressionOutcome("Contract.Requires((!((t as System.String)) || tabs >= 0));")]
     public void TestInference(object t, int tabs)
     {
       var asEnumerable = t as IEnumerable;
       if (asEnumerable != null && !(t is string))
       {
         // do nothing
       }
       else // asNumerable == null || t is string
       {
         Contract.Assert(tabs >= 0);
       }
     }
   }
}

namespace Shuvendu
{
  class Program
  {
    [ClousotRegressionTest]
    [RegressionOutcome("Contract.Requires(0 <= x);")]
    [RegressionOutcome("Contract.Requires(((x <= 0 || x - 1 != t) || 1 == x));")]
    public void Foo(int x, int t)
    {
      var i = x;
      var j = 0;
      while (i > 0)
      {
        //Contract.Assert(i + j == x);
        i--; j++;
        if (i == t) 
          break;
      }
      Contract.Assert(j == x);
    }
  }
}

namespace BugRepro
{
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

  public class Tmp
  {
    public readonly Reactor r = new Reactor();

    public Reactor.State state;

    [ClousotRegressionTest]
	[RegressionOutcome("Contract.Requires((!(restart) || !(wt)));")]
	[RegressionOutcome("Contract.Requires((restart || wt));")]
	[RegressionOutcome("Consider initializing this.state with a value other than 1 (e.g., satisfying this.state == 0). Fix: 0;")]
	[RegressionOutcome("Consider initializing this.state with a value other than 0 (e.g., satisfying this.state == 1). Fix: 1;")]
    public void SimpleTest(bool wt, bool restart)
    {
      Contract.Requires(this.state == Reactor.State.Off);

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
	[RegressionOutcome("Contract.Requires(this.r != null);")]
	[RegressionOutcome("Contract.Requires((!(restart) || !(wt)));")]
	[RegressionOutcome("Contract.Requires((restart || wt));")]
	[RegressionOutcome("Consider initializing this.r.state with a value other than 0 (e.g., satisfying this.r.state == 1). Fix: 1;")]
    public void MoreComplexTest(bool wt, bool restart)
    {
      Contract.Requires(this.r.state == Reactor.State.Off);

      this.r.turnReactorOn();

      if(wt)
      {
        this.r.SCRAM();
      }

      if(restart)
      {
        this.r.SCRAM();
      }

      this.r.turnReactorOn();
      this.r.SCRAM();
    }
  }
}