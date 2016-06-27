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
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization;

namespace CodeFixes
{
  class IArrangedElement  {  }

  class Rectangle
  {
    public Rectangle(int x, int y, object W) { }
  }

  class ComboBox
  {
    public object Width { get; set; }
  }
  
  // extracted from bugs in framework libraries
  internal class NonNullSuggestions
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.False,Message="Calling a method on a null reference 'obj'. Maybe the guard obj == null is too weak? ",PrimaryILOffset=11,MethodILOffset=0)]
    [RegressionOutcome("Contract.Requires(obj != null);")]
#if SHORTCODEFIXES
    [RegressionOutcome("obj != null")]
#else
   [RegressionOutcome("Consider replacing obj == null. Fix: obj != null")] // we do not have source context
#endif
   internal IArrangedElement CastToArrangedElement(object obj)
    {
      IArrangedElement element = obj as IArrangedElement;
      if (obj == null)
	{
	  var foo = new object[] { obj.GetType() };
	  throw new NotSupportedException();
	}
      return element;
    }
	
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.False,Message="Calling a method on a null reference 'comboBox'. Maybe the guard comboBox == null is too weak? ",PrimaryILOffset=6,MethodILOffset=0)]
    [RegressionOutcome("Contract.Requires(comboBox != null);")]
#if SHORTCODEFIXES
    [RegressionOutcome("comboBox != null")]
#else
    [RegressionOutcome("Consider replacing comboBox == null. Fix: comboBox != null")]
#endif
    public void ValidateOwnerDrawRegions(ComboBox comboBox, Rectangle updateRegionBox)
    {
      if (comboBox == null)
	{
	  Rectangle r = new Rectangle(0, 0, comboBox.Width);
	}
    }
    
    private string[] tabClassNames;
    private Type[] tabClasses;

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.False,Message="Use of a null array 'tabClasses'. Maybe the guard tabClasses == null is too weak? ",PrimaryILOffset=45,MethodILOffset=0)]
#if SHORTCODEFIXES
    [RegressionOutcome("tabClasses != null")]
#else
    [RegressionOutcome("Consider replacing tabClasses == null. Fix: tabClasses != null")] // f: this is not really correct, as there is no tabClasses == null in the C# code. Hopefully we will point to the right source pc and maybe change the warning string in the future
#endif
    [RegressionOutcome("Contract.Requires((((tabClasses != null || tabClassNames == null) || tabScopes == null) || tabClasses != null));")] // F: to do, improve the simplification
    private void InitializeArrays(string[] tabClassNames, Type[] tabClasses, string[] tabScopes)
    {
      if (tabClasses != null)
      {
        if ((tabScopes != null) && (tabClasses.Length != tabScopes.Length))
        {
          throw new ArgumentException();
        }
        this.tabClasses = (Type[])tabClasses.Clone();
      }
      else if (tabClassNames != null)
      {
        if ((tabScopes != null) && (tabClasses.Length != tabScopes.Length))
        {
          throw new ArgumentException();
        }
        this.tabClassNames = (string[])tabClassNames.Clone();
        this.tabClasses = null;
      }
    }

  }

  public class SimpleType
  {
    public object MaxLength { get; set; }
    
    public string BaseType { get; set; }
    
    public SimpleType BaseSimpleType { get; set; }

    /// This test no longer tests a code fix, but makes sure the last call to .Length is unreachable
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.False,Message="Calling a method on a null reference 'this.BaseSimpleType'. Are you making some assumption on get_BaseSimpleType that the static checker is unaware of? Or, Maybe the guard this.BaseSimpleType == null is too weak? ",PrimaryILOffset=83,MethodILOffset=0)]
    [RegressionOutcome("Contract.Ensures(Contract.Result<System.String>() != null);")]
#if SHORTCODEFIXES
    [RegressionOutcome("Contract.Assume(this.BaseSimpleType != null);")]
    [RegressionOutcome("this.BaseSimpleType != null")]
#else
    [RegressionOutcome("Consider replacing this.BaseSimpleType == null. Fix: this.BaseSimpleType != null")]
//    [RegressionOutcome("This condition should hold: this.BaseSimpleType != null. Add an assume, a postcondition, or consider a different initialization. Fix: Add (after) Contract.Assume(this.BaseSimpleType != null);")]
	[RegressionOutcome("This condition should hold: this.BaseSimpleType != null. Add an assume, a postcondition to method get_BaseSimpleType, or consider a different initialization. Fix: Add (after) Contract.Assume(this.BaseSimpleType != null);")]
#endif  
    internal string HasConflictingDefinition(SimpleType otherSimpleType)
    {
      if (otherSimpleType == null)
      {
        return "otherSimpleType";
      }
      if (this.MaxLength != otherSimpleType.MaxLength)
      {
        return "MaxLength";
      }
      if (string.Compare(this.BaseType, otherSimpleType.BaseType, StringComparison.Ordinal) != 0)
      {
        return "BaseType";
      }
      if (((this.BaseSimpleType == null) && (otherSimpleType.BaseSimpleType != null)) && (this.BaseSimpleType.HasConflictingDefinition(otherSimpleType.BaseSimpleType).Length != 0))
      {
        return "BaseSimpleType";
      }
      return string.Empty;
    }


    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possibly calling a method on a null reference. Do you expect that CodeFixes.SimpleType.HasConflictingDefinition2(CodeFixes.SimpleType) returns non-null? ",PrimaryILOffset=88,MethodILOffset=0)]
    [RegressionOutcome("Contract.Ensures(Contract.Result<System.String>() != null);")]
#if SHORTCODEFIXES
    [RegressionOutcome("<none>")]
#else
    [RegressionOutcome("The value returned by CodeFixes.SimpleType.HasConflictingDefinition2(CodeFixes.SimpleType) should be non-null. Consider adding a postcondition or an assumption. Fix: <none>")]
#endif  
    internal string HasConflictingDefinition2(SimpleType otherSimpleType)
    {
      if (otherSimpleType == null)
      {
        return "otherSimpleType";
      }
      if (this.MaxLength != otherSimpleType.MaxLength)
      {
        return "MaxLength";
      }
      if (string.Compare(this.BaseType, otherSimpleType.BaseType, StringComparison.Ordinal) != 0)
      {
        return "BaseType";
      }
      if (((this.BaseSimpleType != null) && (otherSimpleType.BaseSimpleType != null)) && (this.BaseSimpleType.HasConflictingDefinition2(otherSimpleType.BaseSimpleType).Length != 0))
      {
        return "BaseSimpleType";
      }
      return string.Empty;
    }
  }

  public class OffByOneSamples
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Array access might be above the upper bound. Maybe the guard i <= arr.Length is too weak? ",PrimaryILOffset=7,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible use of a null array 'arr'",PrimaryILOffset=14,MethodILOffset=0)]
    [RegressionOutcome("Contract.Requires(arr != null);")]
    [RegressionOutcome("Contract.Requires(0 < arr.Length);")]  // One fix (necessary but not sufficient)
#if SHORTCODEFIXES    
    [RegressionOutcome("i < arr.Length")]
#else
    [RegressionOutcome("Consider replacing i <= arr.Length. Fix: i < arr.Length")] // One other fix
#endif
    public void OffByOne(int[] arr)
    {
      for (var i = 0; i <= arr.Length; i++)
        {
          arr[i] = 0;
        }
    }
    
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.False,Message="Array access IS below the lower bound. The error may be caused by the initialization of i. ",PrimaryILOffset=10,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible use of a null array 'a'",PrimaryILOffset=17,MethodILOffset=0)]
    [RegressionOutcome("Contract.Requires(a != null);")]
	[RegressionOutcome("Contract.Requires(0 >= a.Length);")] // if we enter the loop, we crash
#if SHORTCODEFIXES
    [RegressionOutcome("1;")]
#else
    [RegressionOutcome("Consider initializing i with a value other than 0 (e.g., satisfying 0 <= (i - 1)). Fix: 1;")]
#endif
    public void WrongInitialization_ForLoop(int[] a)
    {
      for (var i = 0; i < a.Length; i++)
      {
        a[i - 1] = 110;
      }
    }
    
    [ClousotRegressionTest] 
    [RegressionOutcome(Outcome=ProofOutcome.False,Message="Array access IS below the lower bound. The error may be caused by the initialization of i. ",PrimaryILOffset=10,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible use of a null array 'a'",PrimaryILOffset=17,MethodILOffset=0)]
    [RegressionOutcome("Contract.Requires(a != null);")]
	[RegressionOutcome("Contract.Requires(0 >= a.Length);")] // if we enter the loop, we crash
	#if SHORTCODEFIXES
    [RegressionOutcome("1;")]
#else
    [RegressionOutcome("Consider initializing i with a value other than 0 (e.g., satisfying 0 <= (i - 1)). Fix: 1;")]
#endif
    public void WrongInitialization_WhileLoop(int[] a)
    {
      var i = 0;
      while (i < a.Length)
      {
        a[i-1] = 110;
        i++;
      }
    }
    
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Array access might be above the upper bound. Are you making some assumption on System.Array.get_Length that the static checker is unaware of? ",PrimaryILOffset=66,MethodILOffset=0)]
    [RegressionOutcome("Contract.Requires(count <= list.Length);")]
    [RegressionOutcome("Contract.Ensures(count - Contract.OldValue(count) == 1);")]
    [RegressionOutcome("Contract.Ensures(1 <= count);")]
    [RegressionOutcome("Contract.Ensures((Contract.OldValue(count) - Contract.Result<System.String[]>().Length) < 0);")]
    [RegressionOutcome("Contract.Ensures((Contract.OldValue(list.Length) - Contract.Result<System.String[]>().Length) <= 0);")]
    [RegressionOutcome("Contract.Ensures(1 <= Contract.Result<System.String[]>().Length);")]
    [RegressionOutcome("Contract.Ensures(Contract.Result<System.String[]>() != null);")]
    [RegressionOutcome("Contract.Requires((count != list.Length || 0 < (int)(list.Length)));")]
#if !SHORTCODEFIXES
    [RegressionOutcome("Consider initializing the array with a value larger than list.Length * 2. Fix: list.Length * 2 + 1")]
    #if CLOUSOT2
      [RegressionOutcome("This condition should hold: 0 < (int)(list.Length). Add an assume, a postcondition to method System.Array.Length.get(), or consider a different initialization. Fix: Add (after) Contract.Assume(0 < (int)(list.Length));")]
    #else
      [RegressionOutcome("This condition should hold: 0 < (int)(list.Length). Add an assume, a postcondition to method System.Array.get_Length, or consider a different initialization. Fix: Add (after) Contract.Assume(0 < (int)(list.Length));")]
    #endif
#else
    [RegressionOutcome("list.Length * 2 + 1")]
    [RegressionOutcome("Contract.Assume(0 < (int)(list.Length));")]
#endif
    public string[] InsertElement(string[] list, ref int count, string element)
    {
      Contract.Requires(list != null);
      Contract.Requires(count >=0);
     
      if (count == list.Length)
      {
        var tmp = new string[count * 2];
        Array.Copy(list, tmp, list.Length);
        list = tmp;
      }

      list[count++] = element;

      return list;
    }
    
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Array access might be above the upper bound. Are you making some assumption on System.Array.get_Length that the static checker is unaware of? ",PrimaryILOffset=81,MethodILOffset=0)]
    [RegressionOutcome("Contract.Ensures(count - Contract.OldValue(count) == 1);")]
    [RegressionOutcome("Contract.Ensures((Contract.OldValue(count) - Contract.Result<System.String[]>().Length) < 0);")]
    [RegressionOutcome("Contract.Ensures((Contract.OldValue(list.Length) - Contract.Result<System.String[]>().Length) <= 0);")]
    [RegressionOutcome("Contract.Ensures(1 <= Contract.Result<System.String[]>().Length);")]
    [RegressionOutcome("Contract.Ensures(1 <= count);")]
    [RegressionOutcome("Contract.Ensures(Contract.Result<System.String[]>() != null);")]
    [RegressionOutcome("Contract.Requires((count != list.Length || 0 < (int)(list.Length)));")]
#if !SHORTCODEFIXES
    [RegressionOutcome("Consider initializing the array with a value larger than list.Length * 2. Fix: list.Length * 2 + 1")]
    #if CLOUSOT2
      [RegressionOutcome("This condition should hold: 0 < (int)(list.Length). Add an assume, a postcondition to method System.Array.Length.get(), or consider a different initialization. Fix: Add (after) Contract.Assume(0 < (int)(list.Length));")]
    #else
      [RegressionOutcome("This condition should hold: 0 < (int)(list.Length). Add an assume, a postcondition to method System.Array.get_Length, or consider a different initialization. Fix: Add (after) Contract.Assume(0 < (int)(list.Length));")]
    #endif
#else
    [RegressionOutcome("list.Length * 2 + 1")]
    [RegressionOutcome("Contract.Assume(0 < (int)(list.Length));")]
#endif
    public string[] InsertElementWithPrecondition(string[] list, ref int count, string element)
    {
      Contract.Requires(list != null);
      Contract.Requires(count >=0);
      Contract.Requires(count <= list.Length);
 
      if (count == list.Length)
      {
        var tmp = new string[count * 2];
        Array.Copy(list, tmp, list.Length);
        list = tmp;
      }

      list[count++] = element;

      return list;
    }
  }
  
   public static class OtherInits
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.False,Message="assert is false. The error may be caused by the initialization of x. ",PrimaryILOffset=16,MethodILOffset=0)]
    // We do not emit it, as we added some code to aggressively remove some false ==> false
    //	[RegressionOutcome("Contract.Requires(false);")] // no matter what this program will fail
#if CLOUSOT2
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="The normal exit of the method is unreachable. If this is wanted, consider adding Contract.Ensures(false) to document it",PrimaryILOffset=0,MethodILOffset=0)]
#else
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="The normal exit of the method is unreachable. If this is wanted, consider adding Contract.Ensures(false) to document it",PrimaryILOffset=1,MethodILOffset=0)]
#endif
#if SHORTCODEFIXES
    [RegressionOutcome("1;")]
#else
    [RegressionOutcome("Consider initializing x with a value other than 3 (e.g., satisfying x - 1 == 0). Fix: 1;")]
#endif
    static public void CookCAV(int x)
    {
        x = 3;
        do
        {
            x = x - 1;
        } while (x > 1);

        Contract.Assert(x == 0);
    }
  
    // From Jobstmann, Griesmayer & Bloem CAV'05 paper
    [ClousotRegressionTest]    
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="assert unproven. The error may be caused by the initialization of most. ",PrimaryILOffset=35,MethodILOffset=0)]
    //[RegressionOutcome("Contract.Requires(((((input1 < input2 || input1 < input3) || input1 <= input2) || input1 > input3) || input1 <= input2));")]
    [RegressionOutcome("Contract.Requires((input1 < input2 || input1 <= input2));")]
    [RegressionOutcome("Contract.Requires((((input1 < input2 || input1 <= input2) || input1 <= input3) || input3 <= input2));")]
	[RegressionOutcome("Contract.Requires((((input1 < input2 || input1 < input3) || input1 > input3) || input1 <= input2));")]

#if SHORTCODEFIXES
    [RegressionOutcome("input1;")]
 //   [RegressionOutcome("input1 <= input2")]
#else
    [RegressionOutcome("Consider initializing most with a value other than input2 (e.g., satisfying input1 <= most). Fix: input1;")]
//    [RegressionOutcome("Consider replacing input1 > input2. Fix: input1 <= input2")]
#endif
    static public void MinMax(int input1, int input2, int input3)
    { 
        var least = input1;
        var most = input1;

        if (most < input2)  
            most = input2;
        if (most < input3)
            most = input3;

        if (least > input2)
            most = input2;     // wrong initialization
        if (least > input3)
            least = input3;

        Contract.Assert(least <= most);
    }
  
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Array access might be above the upper bound. The error may be caused by the initialization of i. ",PrimaryILOffset=18,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible use of a null array 'd'",PrimaryILOffset=1,MethodILOffset=0)]
    [RegressionOutcome("Contract.Requires(d != null);")]
#if CLOUSOT2
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Invoking method 'BackIteration_WrongInitialization' will always lead to an error. If this is wanted, consider adding Contract.Requires(false) to document it",PrimaryILOffset=0,MethodILOffset=0)]
#else
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Invoking method 'BackIteration_WrongInitialization' will always lead to an error. If this is wanted, consider adding Contract.Requires(false) to document it",PrimaryILOffset=1,MethodILOffset=0)]
#endif
#if SHORTCODEFIXES
    [RegressionOutcome("d.Length - 1;")]
#else
	// we should filter those, but in Clousot2 we get 2 different PCs
	//[RegressionOutcome("Consider initializing i with a value other than d.Length . Fix: d.Length - 1;")] 
    [RegressionOutcome("Consider initializing i with a value other than d.Length (e.g., satisfying i < d.Length). Fix: d.Length - 1;")]
#endif
    static public void BackIteration_WrongInitialization(object[] d)
    {
      for (var i = d.Length; i >= 0; i--)
      {
        d[i] = 123456;
      }
    }

    // ok
    // Decimal generates a stind instruction
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Array access might be above the upper bound. The error may be caused by the initialization of i. ",PrimaryILOffset=8,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible use of a null array 'd'",PrimaryILOffset=1,MethodILOffset=0)]
    [RegressionOutcome("Contract.Requires(d != null);")]
#if CLOUSOT2
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Invoking method 'BackIteration_WrongInitialization' will always lead to an error. If this is wanted, consider adding Contract.Requires(false) to document it",PrimaryILOffset=0,MethodILOffset=0)]
#else
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Invoking method 'BackIteration_WrongInitialization' will always lead to an error. If this is wanted, consider adding Contract.Requires(false) to document it",PrimaryILOffset=1,MethodILOffset=0)]
#endif
#if SHORTCODEFIXES
    [RegressionOutcome("d.Length - 1;")]
#else
	// we should filter those, but in Clousot2 we get 2 different PCs
	//[RegressionOutcome("Consider initializing i with a value other than d.Length . Fix: d.Length - 1;")] 
    [RegressionOutcome("Consider initializing i with a value other than d.Length (e.g., satisfying i < d.Length). Fix: d.Length - 1;")]
#endif
    static public void BackIteration_WrongInitialization(Decimal[] d)
    {
      for (var i = d.Length; i >= 0; i--)
      {
        d[i] = 123456;
      }
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Array access might be above the upper bound. Consider strengthening some guard? ",PrimaryILOffset=20,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible use of a null array 'd'",PrimaryILOffset=1,MethodILOffset=0)]
    [RegressionOutcome("Contract.Requires(d != null);")]
#if SHORTCODEFIXES
    [RegressionOutcome("&& i < d.Length")]
#else
    [RegressionOutcome("Consider strengthening the guard. Fix: Add && i < d.Length")]
#endif
    static public void BackIteration_WrongIncrement(object[] d, bool b)
    {
      for (var i = d.Length-1; i >= 0; i++)
      {
        // The error may or may not appear
        d[i] = 123456;
        if(b)
          break;
      }
    }

    // Decimal generates a stind instruction
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Array access might be above the upper bound. Consider strengthening some guard? ",PrimaryILOffset=10,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible use of a null array 'd'",PrimaryILOffset=1,MethodILOffset=0)]
    [RegressionOutcome("Contract.Requires(d != null);")]
#if SHORTCODEFIXES
    [RegressionOutcome("&& i < d.Length")]
#else
    [RegressionOutcome("Consider strengthening the guard. Fix: Add && i < d.Length")]
#endif
    static public void BackIteration_WrongIncrement(Decimal[] d)
    {
      for (var i = d.Length - 1; i >= 0; i++)
      {
        // infer i >= d.Length -1, i>= 0 
        d[i] = 123456;
      }
    }
  }
  public class AllocateMem
  {    

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.False,Message="Array access IS above the upper bound. Did you mean 0 instead of 1? ",PrimaryILOffset=21,MethodILOffset=0)]
    [RegressionOutcome("Contract.Ensures(Contract.Result<System.String>() != null);")]
#if SHORTCODEFIXES
    [RegressionOutcome("0")]
    [RegressionOutcome("2")]
#else
    [RegressionOutcome("Consider initializing the array with a value larger than 1. Fix: 2")]
    [RegressionOutcome("Possible off-by one: did you mean indexing with 0 instead of 1?. Fix: 0")]
 #endif
    public static string GetString(string key)    
    {
      string str = GetString(key, null);
      if (str == null)
      {
        object[] args = new object[1];
        args[1] = key;
        throw new ApplicationException();
      }
      return str;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="The length of the array may be negative",PrimaryILOffset=1,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Array access might be below the lower bound",PrimaryILOffset=14,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Array access might be above the upper bound",PrimaryILOffset=14,MethodILOffset=0)]
    [RegressionOutcome("Contract.Requires(0 <= length);")]
    [RegressionOutcome("Contract.Requires(index < length);")]
    [RegressionOutcome("Contract.Requires(0 <= index);")]
#if SHORTCODEFIXES
    [RegressionOutcome("index + 1")]
#else
    [RegressionOutcome("Consider initializing the array with a value larger than index. Fix: index + 1")] // we have a problem with getting an access path for arrays in newArr instructions, so for the moment we omit the name   
#endif     
     public static void AllocateArray_GeneratePreconditions(int length, int index)
    {
      var arr = new int[length];
      arr[index] = 9876;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="The length of the array may be negative. Are you making some assumption on GetALength that the static checker is unaware of? ",PrimaryILOffset=7,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Array access might be below the lower bound",PrimaryILOffset=20,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Array access might be above the upper bound",PrimaryILOffset=20,MethodILOffset=0)]
    [RegressionOutcome("Contract.Requires(0 <= index);")]
    [RegressionOutcome("Contract.Ensures(Contract.Result<System.Int32[]>() != null);")]
    [RegressionOutcome("Contract.Ensures((index - Contract.Result<System.Int32[]>().Length) < 0);")]
    [RegressionOutcome("Contract.Ensures(1 <= Contract.Result<System.Int32[]>().Length);")]
#if SHORTCODEFIXES
    [RegressionOutcome("Contract.Assume(0 <= length);")]
    [RegressionOutcome("index + 1")]
#else
	[RegressionOutcome("This condition should hold: 0 <= length. Add an assume, a postcondition to method GetALength, or consider a different initialization. Fix: Add (after) Contract.Assume(0 <= length);")]
    //[RegressionOutcome("This condition should hold: 0 <= length. Add an assume, a postcondition, or consider a different initialization. Fix: Add (after) Contract.Assume(0 <= length);")]
    [RegressionOutcome("Consider initializing the array with a value larger than index. Fix: index + 1")] // we have a problem with getting an access path for arrays in newArr instructions, so for the moment we omit the name  
#endif
    // We should not infer a [Pure] annotation for GetALength, as "length" is a local
    public static int[] AllocateArray_GenerateSuggestionForAllocation(int index)
    {
      var length = GetALength();
      var arr = new int[length];
      arr[index] = 9876;

      return arr;
    }

        // dummy
    private static string GetString(string s, object o)
    {
      if (o is int)
        return "";
      return null;
    }

    // dummy
    public static int GetALength()
    {
      return System.Environment.ProcessorCount; // some random value
    }    
  }
  
  public class IncreaseSize
  {
    private int nextFree;
    private int[] elements;
    
    public IncreaseSize(int size)
    {
        this.nextFree = 0;
        this.elements = new int[size];
    }
  
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Array access might be above the upper bound. Are you making some assumption on System.Array.get_Length that the static checker is unaware of? ",PrimaryILOffset=122,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible use of a null array 'this.elements'",PrimaryILOffset=29,MethodILOffset=0)]
    [RegressionOutcome("Contract.Assume(this.elements != null);")]
    [RegressionOutcome("Contract.Ensures(1 <= this.elements.Length);")]
    [RegressionOutcome("Contract.Ensures(1 <= this.nextFree);")]
    [RegressionOutcome("Contract.Ensures(this.elements != null);")]
    [RegressionOutcome("Contract.Assume((this.nextFree != this.elements.Length || 0 < (int)(this.elements.Length)));")]
	[RegressionOutcome("Contract.Ensures(this.nextFree - Contract.OldValue(this.nextFree) == 1);")]
	[RegressionOutcome("Contract.Ensures((Contract.OldValue(this.nextFree) - this.elements.Length) < 0);")]
#if SHORTCODEFIXES
    [RegressionOutcome("this.elements.Length * 2 + 1")]
    [RegressionOutcome("Contract.Assume(0 < (int)(this.elements.Length));")]
#else
    [RegressionOutcome("Consider initializing the array with a value larger than this.elements.Length * 2. Fix: this.elements.Length * 2 + 1")]

    #if CLOUSOT2
        [RegressionOutcome("This condition should hold: 0 < (int)(this.elements.Length). Add an assume, a postcondition to method System.Array.Length.get(), or consider a different initialization. Fix: Add (after) Contract.Assume(0 < (int)(this.elements.Length));")]
    #else
        [RegressionOutcome("This condition should hold: 0 < (int)(this.elements.Length). Add an assume, a postcondition to method System.Array.get_Length, or consider a different initialization. Fix: Add (after) Contract.Assume(0 < (int)(this.elements.Length));")]
    #endif
#endif 
    public void Add(int x)
    {
      Contract.Assume(nextFree >= 0);
      Contract.Assume(nextFree <= elements.Length);
      if (nextFree == elements.Length)
      {
        var tmp = new int[nextFree * 2];
        Array.Copy(elements, tmp, elements.Length);
        elements = tmp;
      }
      elements[nextFree++] = x;
    }
  }
  
  public class FromMscorlib
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="The length of the array may be negative. Are you making some assumption on System.ArgIterator.GetRemainingCount() that the static checker is unaware of? ",PrimaryILOffset=20,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Array access might be above the upper bound",PrimaryILOffset=29,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Array access might be above the upper bound",PrimaryILOffset=33,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Array access might be above the upper bound",PrimaryILOffset=37,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Array access might be above the upper bound",PrimaryILOffset=41,MethodILOffset=0)]
#if SHORTCODEFIXES
    [RegressionOutcome("Contract.Assume(0 <= (iterator.GetRemainingCount() + 4));")]
    [RegressionOutcome("1")]
    [RegressionOutcome("2")]
    [RegressionOutcome("3")]
    [RegressionOutcome("4")]
#else   
   [RegressionOutcome("This condition should hold: 0 <= (iterator.GetRemainingCount() + 4). Add an assume, a postcondition to method System.ArgIterator.GetRemainingCount(), or consider a different initialization. Fix: Add (after) Contract.Assume(0 <= (iterator.GetRemainingCount() + 4));")]
    // F: TODO we should filter those
   [RegressionOutcome("Consider initializing the array with a value larger than 0. Fix: 1")]
   [RegressionOutcome("Consider initializing the array with a value larger than 1. Fix: 2")]
   [RegressionOutcome("Consider initializing the array with a value larger than 2. Fix: 3")]
   [RegressionOutcome("Consider initializing the array with a value larger than 3. Fix: 4")]
#endif
    public static string Concat(object arg0, object arg1, object arg2, object arg3, __arglist)
    {
      ArgIterator iterator = new ArgIterator(__arglist);
      int num = iterator.GetRemainingCount() + 4;
      object[] objArray = new object[num];
      objArray[0] = arg0;
      objArray[1] = arg1;
      objArray[2] = arg2;
      objArray[3] = arg3;
      for (int i = 4; i < num; i++)
      {
        objArray[i] = TypedReference.ToObject(iterator.GetNextArg());
      }
      return Concat(objArray);
    }

	// dummy
    private static string Concat(object[] objArray)
    {
      throw new NotImplementedException();
    }

  }

  public class SomeSerializationBug
  {    
    public int messageEnum;
    private object[] args;
    private Type[] instArgs;
    private object callContext;
    private object properties;

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Array access might be above the upper bound. Maybe the guard callA.Length >= 0 is too weak? ",PrimaryILOffset=64,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Array access might be above the upper bound. Maybe the guard callA.Length >= num is too weak? ",PrimaryILOffset=107,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Array access might be above the upper bound. Maybe the guard callA.Length >= num is too weak? ",PrimaryILOffset=150,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Array access might be above the upper bound. Maybe the guard callA.Length >= num is too weak? ",PrimaryILOffset=188,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Array access might be above the upper bound. Maybe the guard callA.Length >= num is too weak? ",PrimaryILOffset=226,MethodILOffset=0)]
    [RegressionOutcome("Contract.Ensures(Contract.Result<System.Runtime.Remoting.Messaging.IMethodCallMessage>() == null);")]
#if SHORTCODEFIXES
    [RegressionOutcome("0 < callA.Length")]
    [RegressionOutcome("num < callA.Length")]
#else
    [RegressionOutcome("Consider replacing callA.Length >= 0. Fix: 0 < callA.Length")]
    [RegressionOutcome("Consider replacing callA.Length >= num. Fix: num < callA.Length")] // Actually there are plenty of those, but at the moment we do not take them into account
#endif
    internal IMethodCallMessage ReadArray(object[] callA, object handlerObject)
    {
      if (callA == null)
		return null;
      
      if (IOUtil.FlagTest(this.messageEnum))
        {
          this.args = callA;
        }
      else
      {
          int num = 0;
          if (IOUtil.FlagTest(this.messageEnum))
	    {
	      // L < 0  
	      if (callA.Length < num)
		{
		  throw new SerializationException();
		}
	      // L >= 0
	      this.args = (object[])callA[num++];
	    }
          if (IOUtil.FlagTest(this.messageEnum))
	    {
	      if (callA.Length < num)
		{
		  throw new SerializationException();
		}
	      this.instArgs = (Type[])callA[num++];
	    }
          if (IOUtil.FlagTest(this.messageEnum))
	    {
	      if (callA.Length < num)
		{
		  throw new SerializationException();
		}
	      this.methodSignature = callA[num++];
	    }
          if (IOUtil.FlagTest(this.messageEnum))
	    {
	      if (callA.Length < num)
		{
		  throw new SerializationException();
		}
	      this.callContext = callA[num++];
	    }
          if (IOUtil.FlagTest(this.messageEnum))
	    {
	      if (callA.Length < num)
		{
		  throw new SerializationException();
		}
	      this.properties = callA[num++];
	    }
	  }	 
      return null; // F
    }

    private class IOUtil
    {
      internal static bool FlagTest(int p)
      {
	throw new NotImplementedException();
      }
    }
    
    public object methodSignature { get; set; }
 }
 
  class FloatingPoint
  {
    private float f;
    private double d;

	[ClousotRegressionTest]
    public void Set(float f)
    {
      if (f != 0.0) // C# compiler generates the cast, so no warning
        this.f = f;
    }

	[ClousotRegressionTest]
#if CLOUSOT2
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible precision mismatch for the arguments of ==",PrimaryILOffset=10,MethodILOffset=0)]
#else
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible precision mismatch for the arguments of ==",PrimaryILOffset=0,MethodILOffset=0)]
#endif
#if SHORTCODEFIXES
    [RegressionOutcome("0 == (double)d0")]
#else
	[RegressionOutcome("Consider adding an explicit cast to force truncation. Fix: 0 == (double)d0")]
#endif
    public void Set(double d0)
    {
      if (d0 != 0.0) 
        this.d = d0; // d0 can be truncated to 0.0, so we emit a warning
    }
  }
  
  internal class InlineAtomFeed 
  {
    // Fields
    private readonly Factory factory;
    private int feed;

    // we infer bottom, so we suggest to remove this constructor
    [ClousotRegressionTest]
	[RegressionOutcome(Outcome=ProofOutcome.False,Message="invariant is false: this.factory != null. This object invariant was inferred, and it should hold in order to avoid an error if the method WriteXml (in the same type) is invoked",PrimaryILOffset=0,MethodILOffset=6)]
// Old message
//    [RegressionOutcome(Outcome=ProofOutcome.False,Message="invariant is false: this.factory != null",PrimaryILOffset=0,MethodILOffset=6)]
    // no way we can satisfy the postcondition
#if CLOUSOT2
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Invoking constructor 'CodeFixes.InlineAtomFeed..ctor()' will always lead to an error. If this is wanted, consider adding Contract.Requires(false) to document it",PrimaryILOffset=0,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="The normal exit of the method is unreachable. If this is wanted, consider adding Contract.Ensures(false) to document it",PrimaryILOffset=0,MethodILOffset=0)]
#else
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Invoking constructor 'CodeFixes.InlineAtomFeed.#ctor()' will always lead to a violation of an (inferred) object invariant",PrimaryILOffset=1,MethodILOffset=0)]
// Old messages:
//    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Invoking constructor 'CodeFixes.InlineAtomFeed.#ctor()' will always lead to an error. If this is wanted, consider adding Contract.Requires(false) to document it",PrimaryILOffset=1,MethodILOffset=0)]
   [RegressionOutcome(Outcome=ProofOutcome.Top,Message="The normal exit of the method is unreachable. If this is wanted, consider adding Contract.Ensures(false) to document it",PrimaryILOffset=1,MethodILOffset=0)]
#endif
#if SHORTCODEFIXES
  #if CLOUSOT2
    [RegressionOutcome("IsInvoked || this.factory != null")]
  #else
    [RegressionOutcome("IsInlineAtomFeedInvoked || this.factory != null")]
  #endif
#else
    #if CLOUSOT2
    [RegressionOutcome("The constructor CodeFixes.InlineAtomFeed..ctor() violates the inferred object invariant this.factory != null. Consider removing it or weakening the object invariant to IsInvoked || this.factory != null. Fix: Add IsInvoked || this.factory != null")]
    #else
    [RegressionOutcome("The constructor CodeFixes.InlineAtomFeed.#ctor() violates the inferred object invariant this.factory != null. Consider removing it or weakening the object invariant to IsInlineAtomFeedInvoked || this.factory != null. Fix: Add IsInlineAtomFeedInvoked || this.factory != null")]
    #endif
#endif
	[RegressionReanalysisCount(1)]
    internal InlineAtomFeed()
    {
    }

    [ClousotRegressionTest]
    [RegressionReanalysisCount(1)]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="invariant unproven: this.factory != null",PrimaryILOffset=0,MethodILOffset=20)]
    [RegressionOutcome("Contract.Requires(factory != null);")]
    [RegressionOutcome("Contract.Ensures(feed == this.feed);")]
    [RegressionOutcome("Contract.Ensures(factory == this.factory);")]
    internal InlineAtomFeed(int feed, Factory factory)
    {
      this.feed = feed;
      this.factory = factory;
    }

    [ClousotRegressionTest]
    // now we filter it, as we suggest an object invariant for this.factory
    //[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possibly calling a method on a null reference 'this.factory'",PrimaryILOffset=7,MethodILOffset=0)]
    [RegressionOutcome("Contract.Ensures(this.factory != null);")]
    [RegressionOutcome("Contract.Invariant(this.factory != null);")]
    public void WriteXml(object writer)
    {
      this.factory.WriteTo(writer);
    }
  }

  public class Factory
  {
    public void WriteTo(object write)
    {
      // dummy: does nothing  
    }
  }

  public class ExamplesComparingCAnalysysAndClousot
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Array access might be above the upper bound",PrimaryILOffset=11,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible use of a null array 'a'",PrimaryILOffset=11,MethodILOffset=0)]
    [RegressionOutcome("Contract.Ensures(Contract.Result<System.Int32>() == -1);")]
    [RegressionOutcome("Contract.Ensures(Contract.Result<System.Int32>() == -(1));")] // todo: pretty print
    [RegressionOutcome("Contract.Requires(a != null);")]
    [RegressionOutcome("Contract.Requires(0 < a.Length);")] // This precondition is necessary but not sufficient
    static int foo(int[] a)
    {
      for (int i = 0; i <= 123; i++)
      {
        a[i] = 12345;
      }

      return -1;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Array access might be above the upper bound. Consider strengthening some guard? ",PrimaryILOffset=34,MethodILOffset=0)]
    [RegressionOutcome("Contract.Ensures(Contract.Result<System.Int32>() == -1);")]
    [RegressionOutcome("Contract.Ensures(Contract.Result<System.Int32>() == -(1));")] // todo: pretty print
#if SHORTCODEFIXES
    [RegressionOutcome("&& i < a.Length")]
#else
    [RegressionOutcome("Consider strengthening the guard. Fix: Add && i < a.Length")]
#endif
    static int foo_WithPreconditions(int[] a)
    {
      Contract.Requires(a != null);
      Contract.Requires(0 < a.Length);
      for (int i = 0; i <= 123; i++)
      {
        a[i] = 12345;
      }

      return -1;
    }
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Array access might be above the upper bound. Maybe the guard i <= 123 is too weak? ",PrimaryILOffset=22,MethodILOffset=0)]
#if CLOUSOT2
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="The normal exit of the method is unreachable. If this is wanted, consider adding Contract.Ensures(false) to document it",PrimaryILOffset=0,MethodILOffset=0)]
#else
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="The normal exit of the method is unreachable. If this is wanted, consider adding Contract.Ensures(false) to document it",PrimaryILOffset=2,MethodILOffset=0)]
#endif
    [RegressionOutcome("Contract.Ensures(Contract.Result<System.Int32>() == 0);")]
#if SHORTCODEFIXES
    [RegressionOutcome("i < 123")]
#else
    [RegressionOutcome("Consider replacing i <= 123. Fix: i < 123")] // F: should we figure out that len == 123, that len appears in the source, and then improve the message?
#endif
    static int tmain(int argc, string[] argv)
    {
      int[] a = new int[123];

      int len = 123;
      for (int i = 0; i <= len; i++)
      {
        a[i] = 12345;
      }

      return 0;
    }
  }
  
    public class OveflowsFixes
  {
    // nothing to suggest, -b or -a may underflow
    [ClousotRegressionTest]
    public void Ex1_NOT_OK(int a, int b)
    {
      Contract.Requires(a + b >= 0);
    }

    // suggests a >= -b
    [ClousotRegressionTest("cci1only")]
#if SHORTCODEFIXES
    [RegressionOutcome("a >= (0 - b)")]
#else
    [RegressionOutcome("Consider replacing the expression (a + b) >= 0 with an equivalent, yet not overflowing expression. Fix: a >= (0 - b)")]
#endif
    public void Ex1_OK(int a, int b)
    {
      Contract.Requires(a >= 0);
      Contract.Requires(b >= 0);
      Contract.Requires(a + b >= 0);
    }

    // should suggest b >= -a but NOT a >= -b, as -b may overflow
    [ClousotRegressionTest("cci1only")]
#if SHORTCODEFIXES
    [RegressionOutcome("b >= (0 - a)")]
#else
    [RegressionOutcome("Consider replacing the expression (a + b) >= 0 with an equivalent, yet not overflowing expression. Fix: b >= (0 - a)")]   
#endif
    public void Ex1_OnlyOnePre_OK(int a, int b)
    {
      Contract.Requires(a >= 0);
      Contract.Requires(a + b >= 0);
    }

    // nothing to suggest
    [ClousotRegressionTest]   
   public void Ex2_NOT_OK(int a, int b, int c)
    {
      Contract.Requires(a >= 0);
      Contract.Requires(b >= 0);
      Contract.Requires(a + b < c);
    }

    // should suggest a < c- b
    [ClousotRegressionTest]
#if SHORTCODEFIXES
    [RegressionOutcome("a < (c - b)")]
#else
    [RegressionOutcome("Consider replacing the expression (a + b) < c with an equivalent, yet not overflowing expression. Fix: a < (c - b)")]
#endif
    public void Ex2_OK(int a, int b, int c)
    {
      Contract.Requires(a >= 0);
      Contract.Requires(b >= 0);
      Contract.Requires(c >= 0);
      Contract.Requires(a + b < c);
    }

    // nothing to suggest
    [ClousotRegressionTest]
    public void Ex3_NOT_OK(int a, int b, int c, int d)
    {
      Contract.Requires(a >= 0);
      Contract.Requires(b >= 0);
      Contract.Requires(c >= 0);
      Contract.Requires(d >= 0);
      Contract.Requires(a+b+c < d);
    }

    // TODO TODO
    [ClousotRegressionTest]
    public void Ex3_OK(int a, int b, int c, int d)
    {
      Contract.Requires(a >= 0);
      Contract.Requires(b >= 0);
      Contract.Requires(c >= 0);
      Contract.Requires(d >= 0);
      Contract.Requires(d >= c);
      Contract.Requires(a + b + c < d);
    }


    // nothing to suggest, b-a may underflow
    [ClousotRegressionTest]    
    public void BinarySearch_NOT_OK(int a, int b)
    {
      Contract.Requires((a + b) / 2 >= 0); 
    }

    // suggest a + (b-a)/2
    // TODO: fix pretty printing
    [ClousotRegressionTest("cci1only")]
#if SHORTCODEFIXES
    [RegressionOutcome("(a + (b - a) / 2) >= 0")]
    [RegressionOutcome("a + (b - a) / 2")]
#else
    [RegressionOutcome("Consider replacing the expression ((a + b) / 2) >= 0 with an equivalent, yet not overflowing expression. Fix: (a + (b - a) / 2) >= 0")]
    [RegressionOutcome("Consider replacing the expression (a + b) / 2 with an equivalent, yet not overflowing expression. Fix: a + (b - a) / 2")]
#endif
    public void BinarySearch_OK(int a, int b)
    {
      Contract.Requires(a >= 0);
      Contract.Requires(b >= 0);
      Contract.Requires((a + b) / 2 >= 0); 
    }

    [ClousotRegressionTest]
    [RegressionOutcome("Contract.Ensures(Contract.Result<System.Int32>() < array.Length);")]
    [RegressionOutcome("Contract.Ensures(-1 <= Contract.Result<System.Int32>());")]
    [RegressionOutcome("Contract.Ensures(Contract.Result<System.Int32>() <= 2147483646);")]
#if SHORTCODEFIXES
    [RegressionOutcome("inf + (sup - inf) / 2")]
#else
    [RegressionOutcome("Consider replacing the expression (inf + sup) / 2 with an equivalent, yet not overflowing expression. Fix: inf + (sup - inf) / 2")]
#endif
    public static int BinarySearch_PossibleUnderflow(int[] array, int value)
    {
      Contract.Requires(array != null);
      int inf = 0;
      int sup = array.Length - 1; 

      while (inf <= sup)
      {
        int index = (inf + sup) /2 ;

        int mid = array[index];

        if (value == mid)
          return index;
        if (mid < value)
          inf = index + 1;
        else
          sup = index - 1;
      }
      return -1;
    }
    
        [ClousotRegressionTest]
    [RegressionOutcome("Contract.Ensures(Contract.Result<System.Int32>() < array.Length);")]
    [RegressionOutcome("Contract.Ensures(-1 <= Contract.Result<System.Int32>());")]
    [RegressionOutcome("Contract.Ensures(Contract.Result<System.Int32>() <= 2147483646);")]
#if SHORTCODEFIXES
    [RegressionOutcome("inf + (sup - inf) / 2")]
#else
    [RegressionOutcome("Consider replacing the expression (2 * (inf + sup)) / 4 with an equivalent, yet not overflowing expression. Fix: inf + (sup - inf) / 2")]
#endif
    public static int BinarySearch_PossibleUnderflowWithSimplification(int[] array, int value)
    {
      Contract.Requires(array != null);
      int inf = 0;
      int sup = array.Length - 1; 

      while (inf <= sup)
      {
        int index = 2*(inf + sup) /4 ;

        int mid = array[index];

        if (value == mid)
          return index;
        if (mid < value)
          inf = index + 1;
        else
          sup = index - 1;
      }
      return -1;
    }
    
    [ClousotRegressionTest]
    [RegressionOutcome("Contract.Ensures(Contract.Result<System.Int32>() < array.Length);")]
    [RegressionOutcome("Contract.Ensures(-1 <= Contract.Result<System.Int32>());")]
    [RegressionOutcome("Contract.Ensures(Contract.Result<System.Int32>() <= 998);")]
    public static int BinarySearch_NoProblem(int[] array, int value)
    {
      Contract.Requires(array != null);
      Contract.Requires(array.Length < 1000);

      int inf = 0;
      int sup = array.Length - 1; 

      while (inf <= sup)
      {
        int index = (inf + sup) /2 ;

        int mid = array[index];

        if (value == mid)
          return index;
        if (mid < value)
          inf = index + 1;
        else
          sup = index - 1;
      }
      return -1;
    }
    

   
    [ClousotRegressionTest]
	[RegressionOutcome("Contract.Requires(array.Length != 0);")]
    [RegressionOutcome("Contract.Ensures(Contract.Result<System.Int32>() < array.Length);")]
    [RegressionOutcome("Contract.Ensures(-1 <= Contract.Result<System.Int32>());")]
    [RegressionOutcome("Contract.Ensures(Contract.Result<System.Int32>() <= 2147483646);")]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Array access might be above the upper bound. The error may be caused by the initialization of sup. ",PrimaryILOffset=28,MethodILOffset=0)]
#if SHORTCODEFIXES
    [RegressionOutcome("inf + (sup - inf) / 2")]
    [RegressionOutcome("array.Length - 1;")]
#else
	[RegressionOutcome("Consider initializing sup with a value other than array.Length (e.g., satisfying array.Length != 0). Fix: array.Length - 1;")]
    [RegressionOutcome("Consider replacing the expression (inf + sup) / 2 with an equivalent, yet not overflowing expression. Fix: inf + (sup - inf) / 2")]
#endif
    public static int BinarySearch_PossibleWrongSupInitialization(int[] array, int value)
    {
      Contract.Requires(array != null);
      int inf = 0;
      int sup = array.Length;  // should suggest the initialization!

      while (inf <= sup)
      {
        int index = (inf + sup) /2 ;

        int mid = array[index];

        if (value == mid)
          return index;
        if (mid < value)
          inf = index + 1;
        else
          sup = index - 1;
      }
      return -1;
    }
  }

  
  public class OffByOneFixes
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Array access might be above the upper bound",PrimaryILOffset=28,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible use of a null array 'b'",PrimaryILOffset=14,MethodILOffset=0)]
    [RegressionOutcome("Contract.Requires(b != null);")]
    [RegressionOutcome("Contract.Requires(index < b.Length);")]
	[RegressionOutcome("Contract.Ensures(Contract.Result<System.Boolean>() == b[index]);")]
	public bool Simple_NotOk(bool[] b, int index)
    {
      Contract.Requires(index >= 0);
      Contract.Requires(index <= b.Length);

      return b[index]; // possible off by one, but we suggest nothing as index - 1 may cause an error
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Array access might be above the upper bound. Did you mean index - 1 instead of index? ",PrimaryILOffset=28,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible use of a null array 'b'",PrimaryILOffset=14,MethodILOffset=0)]
    [RegressionOutcome("Contract.Requires(b != null);")]
    [RegressionOutcome("Contract.Requires(index < b.Length);")]
	[RegressionOutcome("Contract.Ensures(Contract.Result<System.Boolean>() == b[index]);")]
#if SHORTCODEFIXES
    [RegressionOutcome("index - 1")]
#else         
    [RegressionOutcome("Possible off-by one: did you mean indexing with index - 1 instead of index?. Fix: index - 1")]
#endif
    public bool Simple_Ok(bool[] b, int index)
    {
      Contract.Requires(index >= 1);
      Contract.Requires(index <= b.Length);

      return b[index]; // possible off by one, we suggest index -1
    }    
  }

  
  public class StrongerTestWithConstants
  {
    public int x, y;

    [ClousotRegressionTest]
#if SHORTCODEFIXES
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Array access might be above the upper bound. Did you mean 0 instead of 1? Or, Maybe the guard a.Length > 0 is too weak? ",PrimaryILOffset=21,MethodILOffset=0)]
    [RegressionOutcome("Contract.Requires((a.Length <= 0 || 1 < a.Length));")]
    [RegressionOutcome("0")]
    [RegressionOutcome("a.Length > 1")]
#else
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Array access might be above the upper bound. Did you mean 0 instead of 1? Or, Maybe the guard a.Length > 0 is too weak? ",PrimaryILOffset=21,MethodILOffset=0)]
    [RegressionOutcome("Contract.Requires((a.Length <= 0 || 1 < a.Length));")] // makes sense: if a.Length > 0 ==> a.Length > 1
    [RegressionOutcome("Possible off-by one: did you mean indexing with 0 instead of 1?. Fix: 0")]
    [RegressionOutcome("Consider replacing a.Length > 0. Fix: a.Length > 1")]
#endif    
    public void Arrays(int[] a)
    {
      Contract.Requires(a != null);

      if (a.Length > 0)
      {
        x = a[1];
        y = a[0];
      }
    }
  }

  public class TestPureSuggestion
  {
    public SomeObj s;

    // We should suggest GetInt to be marked as [Pure]
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="assert unproven",PrimaryILOffset=37,MethodILOffset=0)]
    [RegressionOutcome("Contract.Ensures(this.s != null);")]
#if SHORTCODEFIXES
    [RegressionOutcome("Add attribute [Pure] to CodeFixes.TestPureSuggestion.GetInt(System.Int32)")]
#else
    [RegressionOutcome("The value of this.s may be modified by the invocation to CodeFixes.TestPureSuggestion.GetInt(System.Int32). Consider adding the attribute [Pure] to CodeFixes.TestPureSuggestion.GetInt(System.Int32) or a postcondition. Fix: Add attribute [Pure] to CodeFixes.TestPureSuggestion.GetInt(System.Int32)")]
#endif
    public int RequiresPureFunction(int x)
    {
      Contract.Requires(s != null);

      var y = GetInt(x);

      Contract.Assert(s != null);

      return y;
    }

    // Sanity check that adding [Pure] proves the assertion
    [ClousotRegressionTest]
    [RegressionOutcome("Contract.Ensures(this.s != null);")]
    public int AddedPureFunction(int x)
    {
      Contract.Requires(s != null);

      var y = GetIntPure(x);

      Contract.Assert(s != null);

      return y;
    }

    [ContractVerification(false)]
    private  int GetInt(int i)
    {
      throw new NotImplementedException();
    }

    [ContractVerification(false)]
    [Pure]
    private int GetIntPure(int i)
    {
      throw new NotImplementedException();
    }

    public class SomeObj
    {
      public int f;

      public SomeObj(int x)
      {
        this.f = x;
      }
    }

  }
  public class Dilligs 
  {

  [ClousotRegressionTest]
  [RegressionOutcome(Outcome=ProofOutcome.Top,Message="assert unproven. Consider strengthening some guard? ",PrimaryILOffset=63,MethodILOffset=0)] // We can prove it with subpolyhedra, the test is here only because we want to test code fixes, and in particular not emit wrong ones
#if SHORTCODEFIXES
  [RegressionOutcome("&& (k + i + j) > (2 * n)")]
#else
  [RegressionOutcome("Consider strengthening the guard. Fix: Add && (k + i + j) > (2 * n)")]
#endif
  void Foo(bool flag, int n)
  {
    Contract.Requires(n >= 0);

    int k = 1;
    if (flag) k = n * n;

    //Contract.Assume(k >= 0); // With this assumption we can prove the asser

    int i = 0, j = 0;
    while (i <= n)
    {
      i++;
      j += i;
    }

    Contract.Assume(j >= n); // asked by their tool to the user

    int z = k + i + j;
    Contract.Assert(z > 2 * n);
  }
  
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="assert unproven. Are you making some assumption on some callee that it is unknown to the static analyzer? ",PrimaryILOffset=67,MethodILOffset=0)] // We can prove it with subpolyhedra, and so we added them in the regression
    [RegressionOutcome("Contract.Requires(((!(flag) || 1 <= n) || (n * n + 2) > (2 * n)));")] // this is equivalent to true, but clousot cannot simplify it
    [RegressionOutcome("Contract.Requires(((flag || 1 <= n) || (1 + 2) > (2 * n)));")] // this is equivalent to true, but Clousot cannot simplify it
#if SHORTCODEFIXES
    [RegressionOutcome("Contract.Assume((n * n + 2) > (2 * n));")] // this is equivalent to true, but Clousot cannot simplify it
#else
    [RegressionOutcome("Consider adding the assumption (n * n + 2) > (2 * n). Fix: Add Contract.Assume((n * n + 2) > (2 * n));")]
#endif
    void Foo_WithManualUnrolling(bool flag, int n)
  {
    Contract.Requires(n >= 0);

    int k = 1;
    if (flag)
    {
      k = n * n;
      Contract.Assume(k >= 0);
    }

    int i = 0, j = 0;
    {
      i = 1;
      j = 1;
      while (i <= n)
      {
        i++;
        j += i;
      }
    }
    
    int z = k + i + j;
    Contract.Assert(z > 2 * n);
  }
	
	[ClousotRegressionTest]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="assert unproven. Are you making some assumption on CodeFixes.Dilligs.h(System.Int32@) that the static checker is unaware of? ",PrimaryILOffset=36,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="assert unproven",PrimaryILOffset=47,MethodILOffset=0)]
#if SHORTCODEFIXES
	[RegressionOutcome("Contract.Assume(i != j);")]
#else
    #if CLOUSOT2
        [RegressionOutcome("This condition should hold: i != j. Add an assume, a postcondition to method CodeFixes.Dilligs.h(System.Int32), or consider a different initialization. Fix: Add (after) Contract.Assume(i != j);")]
    #else
        [RegressionOutcome("This condition should hold: i != j. Add an assume, a postcondition to method CodeFixes.Dilligs.h(System.Int32@), or consider a different initialization. Fix: Add (after) Contract.Assume(i != j);")]
    #endif
#endif
	void main_Prime()
    {
      int[] x = new int[] { 1, 2, 3, 4, 5 };

      int i = 0, j;
      j = h(ref i);
      //printf("Value of i: %d j: %d\n", i, j);

      Contract.Assert(i != j);

      //g(x, i, j);

      Contract.Assert(x[j] == 1);

    }

    [ContractVerification(false)]
    int h(ref int i)
    {
      Contract.Ensures(i >= 0);
      Contract.Ensures(i <= 4);
      Contract.Ensures(Contract.Result<int>() >= 0);
      Contract.Ensures(Contract.Result<int>() <= 4);

	  return 0;
    }  
  }
}

namespace ShouldGetStrongerTests
{
  public class StrengthenPre
  {
	[ClousotRegressionTest]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="requires unproven: f.Length > 4. Should you strengthen a check for null? ",PrimaryILOffset=12,MethodILOffset=25)]
#if SHORTCODEFIXES
	[RegressionOutcome("&& floats.Length > 4")]
#else
	[RegressionOutcome("Consider strengthening the null check. Fix: Add && floats.Length > 4")]
#endif
	public static bool GetVectorNoCast(DomNode domNode, AttributeInfo attribute, out Vec3F result)
    {
      Contract.Requires(domNode != null);
      float[] floats = domNode.GetAttributeFloat(attribute);
      if (floats != null)
      {
        result = new Vec3F(floats);
        return true;
      }

      result = null;
      return false;
    }
 
	[ClousotRegressionTest]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="requires unproven: f.Length > 4. Should you strengthen a check for null? ",PrimaryILOffset=12,MethodILOffset=30)]
#if SHORTCODEFIXES
	[RegressionOutcome("&& floats.Length > 4")]
#else
	[RegressionOutcome("Consider strengthening the null check. Fix: Add && floats.Length > 4")]
#endif
	public static bool GetVector(DomNode domNode, AttributeInfo attribute, out Vec3F result)
    {
      Contract.Requires(domNode != null);
      float[] floats = domNode.GetAttribute(attribute) as float[];
      if (floats != null)
      {
        result = new Vec3F(floats);
        return true;
      }

      result = null;
      return false;
    }
  }
  public class Vec3F
  {
    public Vec3F(float[] f)
    {
      Contract.Requires(f.Length > 4);
    }
  }

  public class DomNode
  {

    [ContractVerification(false)]
    internal float[] GetAttributeFloat(AttributeInfo attribute)
    {
      throw new System.NotImplementedException();
    }
    [ContractVerification(false)]
    internal object GetAttribute(AttributeInfo attribute)
    {
      throw new System.NotImplementedException();
    }

  }

  public class AttributeInfo
  {

  }

}
