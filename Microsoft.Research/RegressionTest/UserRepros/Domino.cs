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
using Microsoft.Research.ClousotRegression;
using System.Diagnostics.Contracts;

namespace Repros
{
  public struct StringId 
  {
  
    /// <summary>
    /// An invalid string.
    /// </summary>
    public static readonly StringId Invalid = new StringId(0);

    /// <summary>
    /// Identifier of this string as understood by the owning string table.
    /// </summary>
    public readonly int Value;

    public StringId(int x)
    {
      this.Value = x;
    }

    private const int BytesPerBufferBits = 21;
    internal const int BytesPerBuffer = (1 << BytesPerBufferBits); // internal for use by the unit test
    private const int BytesPerBufferMask = BytesPerBuffer - 1;

    // Ok if we are smarter with the simplification from expressions to Polynomials
	[ClousotRegressionTest]
    internal bool Equals(StringId id, byte[] buffer, int length, int index)
    {
      index = id.Value & BytesPerBufferMask;

      length = buffer[index++];

      if (length == 20)
      {
        Contract.Assume(index + 4 <= buffer.Length);

        Contract.Assert(index < buffer.Length);

        var x1 = (buffer[index++] << 24);
        var x2 = (buffer[index++] << 16);
        var x3 = (buffer[index++] << 8);
        var x4 = (buffer[index++] << 0);


        /*length = (buffer[index++] << 24)
                 | (buffer[index++] << 16)
                 | (buffer[index++] << 8)
                 | (buffer[index++] << 0);
         */ 
      }

      return index % 2 == 0;
    }

	[ClousotRegressionTest]
    private void VerySmallRepro(int index, int[] buffer)
    {
      Contract.Assume(index + 4 <= buffer.Length);

      Contract.Assert(index < buffer.Length);
    }

    // Can't prove it anymore because of the change in the way we simplify the expressions
	[ClousotRegressionTest]
	// We are inferring a necesary and sufficiend precondition out of this postcondition, this is why we do not report a warning
	//	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="ensures unproven: Contract.Result<int>() * div >= n",PrimaryILOffset=37,MethodILOffset=71)]
    private static int GetArrayLength(int n, int div)
    {
      Contract.Requires(div > 0);

      // F: mine, to prove the postcondition
      Contract.Requires(n >= 0);
      // F: I've added it, but clousot cannot  check it
      Contract.Ensures(Contract.Result<int>() * div >= n);

      if( n > 0)
      {
        return (((n - 1) / div) + 1);
      }
      else
      {
        return 0;
      }
      // return n > 0 ? (((n - 1) / div) + 1) : 0;
    }
  }

  public class BetterWarningMessagesTest
  {
    readonly string str;
    int counter = 0;

	[ClousotRegressionTest]
	[RegressionReanalysisCount(1)] // Reanalyze the method once
	[RegressionOutcome(Outcome=ProofOutcome.False,Message="invariant is false: this.str != null. This object invariant was inferred, and it should hold in order to avoid an error if the method UseStr (in the same type) is invoked",PrimaryILOffset=1,MethodILOffset=23)]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Invoking constructor 'Repros.BetterWarningMessagesTest.#ctor()' will always lead to a violation of an (inferred) object invariant",PrimaryILOffset=2,MethodILOffset=0)]
	public BetterWarningMessagesTest()
    {
      this.str = null;
    }
	
	[ClousotRegressionTest]
	[RegressionReanalysisCount(1)] // Reanalyze the method once
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Missing precondition in an externally visible method. Consider adding Contract.Requires(inputString != null); for parameter validation. Otherwise invoking UseStr (in the same type) may cause an error",PrimaryILOffset=2,MethodILOffset=0)]
    public BetterWarningMessagesTest(string inputString, int counter)
    {
      this.counter = 0;

      this.str = inputString;
      if(this.str == null)
      {
        this.counter++;
      }
    }

	[ClousotRegressionTest]
    public string UseStr()
    {
      return this.str.ToString() + "hello";
    }

	[ClousotRegressionTest]
    public void Add()
    {
      counter++;
    }
  }
  
}

namespace FixesAfterRaisingWarningLevel
{

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

  public class C
  {
    internal int c;
  }

  public class OutParam
  {
	[ClousotRegressionTest]
    public bool TryGetAValue(int x, out C value)
    {
      if (x > 0)
      {
        value = new C() { c = 12 };

        return true;
      }

      value = null;
      return false;
    }

	[ClousotRegressionTest]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="assert unproven. The variable 'value_Out' flows from an out parameter. Consider adding a postconditon to the callee or an assumption after the call to document it",PrimaryILOffset=24,MethodILOffset=0)]
    public void UseTryGetAValue(int z)
    {
      C value_Out;
      if (!TryGetAValue(z, out value_Out))
      {
        return;
      }

      Contract.Assert(value_Out != null); // OK: now we give a message about it
    }

  	[ClousotRegressionTest]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="requires unproven: c != null. The variable 'value_Out' flows from an out parameter. Consider adding a postconditon to the callee or an assumption after the call to document it",PrimaryILOffset=8,MethodILOffset=19)]
	public void UseTryGetAValueWithRequires(int z)
    {
      C value_Out;
      if (!TryGetAValue(z, out value_Out))
      {
        return;
      }

      DummyCallee(value_Out); // OK: now we warn about it
    }

  	[ClousotRegressionTest]
    public void DummyCallee(C c)
    {
      Contract.Requires(c != null);
    }
  }


  public struct HierarchicalNameId : IEquatable<HierarchicalNameId>
  {
    /// <summary>
    /// An invalid entry.
    /// </summary>
    public static readonly HierarchicalNameId Invalid = new HierarchicalNameId(0);

    public readonly int Value;

	[ClousotRegressionTest]
    public HierarchicalNameId(int x)
    {
      this.Value = x;
    }

	[ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="ensures unproven: Contract.Result<bool>() == (left.Value == right.Value)",PrimaryILOffset=24,MethodILOffset=42)]
	public static bool operator ==(HierarchicalNameId left, HierarchicalNameId right)
    {
      Contract.Ensures(Contract.Result<bool>() == (left.Value == right.Value));

      return left.Equals(right);
    }

	[ClousotRegressionTest]
    public static bool operator !=(HierarchicalNameId left, HierarchicalNameId right)
    {
      return !left.Equals(right);
    }

	[ClousotRegressionTest]
    public bool Equals(HierarchicalNameId other)
    {
      return other.Value == Value;
    }

	[ClousotRegressionTest]
    public override bool Equals(object obj)
    {
      return base.Equals(obj);
    }

	[ClousotRegressionTest]
    public override int GetHashCode()
    {
      return base.GetHashCode();
    }

	[ClousotRegressionTest]
    public static bool AssumeValid(HierarchicalNameId id)
    {
      return id.Value != 0;
    }

	[ClousotRegressionTest]
    public static bool AssumeInvalid(HierarchicalNameId id)
    {
      return id.Value == 0;
    }
  }


  public class StructValidInvalid
  {
	[ClousotRegressionTest]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="ensures unproven: Contract.Result<bool>() == (Contract.ValueAtReturn(out hierarchicalNameId) != HierarchicalNameId.Invalid)",PrimaryILOffset=24,MethodILOffset=53)]
    public bool TrySimpleWrong(HierarchicalNameId input, out HierarchicalNameId hierarchicalNameId)
    {
      Contract.Ensures(Contract.Result<bool>() == (Contract.ValueAtReturn(out hierarchicalNameId) != HierarchicalNameId.Invalid)); // should not prove

      hierarchicalNameId = input;

      return hierarchicalNameId == input;
    }

	[ClousotRegressionTest]
    public bool TrySimpleOk(HierarchicalNameId input, out HierarchicalNameId hierarchicalNameId)
    {
      Contract.Ensures(Contract.Result<bool>() == (Contract.ValueAtReturn(out hierarchicalNameId) != HierarchicalNameId.Invalid));

      hierarchicalNameId = input;

      return hierarchicalNameId != HierarchicalNameId.Invalid; // should prove - ok
    }

	[ClousotRegressionTest]
    public bool TrySimpleOk2(HierarchicalNameId input, out HierarchicalNameId hierarchicalNameId)
    {
      Contract.Requires(input != HierarchicalNameId.Invalid);
      Contract.Ensures(Contract.Result<bool>() == (Contract.ValueAtReturn(out hierarchicalNameId) != HierarchicalNameId.Invalid));

      hierarchicalNameId = input;

      return true; // should prove - ok
    }

	[ClousotRegressionTest]
    public bool TrySimpleOk3(HierarchicalNameId input, out HierarchicalNameId hierarchicalNameId)
    {
      Contract.Requires(input != HierarchicalNameId.Invalid);
      Contract.Ensures(Contract.Result<bool>() == (Contract.ValueAtReturn(out hierarchicalNameId) != HierarchicalNameId.Invalid));

      hierarchicalNameId = input;

      return hierarchicalNameId == input; // should prove too
    }

	[ClousotRegressionTest]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="ensures unproven: Contract.Result<bool>() == (Contract.ValueAtReturn(out hierarchicalNameId) != HierarchicalNameId.Invalid)",PrimaryILOffset=24,MethodILOffset=85)]
    public bool TrySimpleOk4(int x, out HierarchicalNameId hierarchicalNameId)
    {
      Contract.Ensures(Contract.Result<bool>() == (Contract.ValueAtReturn(out hierarchicalNameId) != HierarchicalNameId.Invalid));

      HierarchicalNameId.AssumeInvalid(HierarchicalNameId.Invalid);

      if(x > 0)
      {
        hierarchicalNameId = new HierarchicalNameId(x);
        return true;
      }

      hierarchicalNameId = HierarchicalNameId.Invalid;
      return false;
    }

  }

  // ok
  public class PrivateProperties
  {
    public C MyProperty { get; private set; }

	[ClousotRegressionTest]
	public PrivateProperties()
    {
      this.MyProperty = new C();
      // We know MyProperty != null
    }

	// We infer the ensures MyProperty != null, and hence prove the property
	[ClousotRegressionTest]
    public int DoSomething()
    {
      return this.MyProperty.c;
    }
  }

  // ok
  public class PrettyPrintEnum
  {
    public enum Days{ Sun, Mon, Tue, Wed, Thu, Fri, Sat}

	[ClousotRegressionTest]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Missing precondition in an externally visible method. Consider adding Contract.Requires(d != FixesAfterRaisingWarningLevel.PrettyPrintEnum.Days.Sun); for parameter validation",PrimaryILOffset=2,MethodILOffset=0)]
    public int GetNum(Days d)
    {
      // Will infer: Contract.Requires(d != DominoMini.PrettyPrintEnum.Days.Sun);
      switch(d)
      {
        case Days.Mon:
        case Days.Tue:
        case Days.Wed:
        case Days.Thu:
        case Days.Fri:
          return 111;
       
        case Days.Sat:
        default:
          {
            Contract.Assert(d == Days.Sat); // should not prove it! -- ok we generare a precondition
            return -1;
          }
      }
    }
  }
  
  public class StringTest
  {
	// There was a bug in handling string.Empty
	[ClousotRegressionTest]
    public void AddInputArg(string separator, IEnumerable<string> includes)
    {
      Contract.Requires(!string.IsNullOrWhiteSpace(separator));
      Contract.Requires(includes != null);
      AddInputArgCore(string.Empty, separator, includes);
    }

	[ClousotRegressionTest]
    private void AddInputArgCore(
          string prefix,
          string separator,
          IEnumerable<string> includes)
    {
      Contract.Requires(prefix != null);
      Contract.Requires(!prefix.Contains(" "));
      Contract.Requires(!string.IsNullOrEmpty(separator));
      Contract.Requires(includes != null);

    }
  }
  
  public class objInvariant
  {
    private readonly int[] arr;

    [ContractVerification(false)]
	[ClousotRegressionTest]
	
	// We should not analyze this one!!!
	[RegressionOutcome(Outcome=ProofOutcome.False,Message="invariant is false: this.arr != null. This object invariant was inferred, and it should hold in order to avoid an error if the method Dummy (in the same type) is invoked",PrimaryILOffset=1,MethodILOffset=9)]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Invoking constructor 'FixesAfterRaisingWarningLevel.objInvariant.#ctor()' will always lead to a violation of an (inferred) object invariant",PrimaryILOffset=1,MethodILOffset=0)]
    public objInvariant() {  }

	[ClousotRegressionTest]
    public objInvariant(int x)
    {
      Contract.Requires(x >= 0);
      this.arr = new int[x];
    }

	[ClousotRegressionTest]
    public int Dummy()
    {
      return this.arr.Length;
    }
  }
  
  public class GenericTypeViaReflection
  {
#if NETFRAMEWORK_4_5
		// GenericTypeArguments it's not in v4.0
	[ClousotRegressionTest]
    public static string Len(Type type)
    {
      if (type == typeof(MyList<>))
      {
        return string.Format("{0}", type.GenericTypeArguments[0]);
      }

      if (type == typeof(MyList<,>))
      {
        return string.Format("{0}, {1}", type.GenericTypeArguments[0], type.GenericTypeArguments[1]);
      }

      return null;
    }
#endif
  }
  
  
  public class MyList<A> { }

  public class MyList<A, B> { }

}
namespace PostConditionWithIFF
{
 public class A
 {
   public readonly int X;

	[ClousotRegressionTest]
   public  A()
   {
     this.X = 12;
   }
 }

  public class B
  {
	[ClousotRegressionTest]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="You are making an assumption on the value of the readonly field a.X in an externally visible method. Consider adding Contract.Assume(0 <= a.X); to document it, or turning the field into a (auto)property",PrimaryILOffset=15,MethodILOffset=0)]
    public int[] Foo(A a)
    {
      Contract.Requires(a != null);

      return new int[a.X];
    }
  }


  class Repro
  {
	[ClousotRegressionTest]
    public void Example(bool isOutput)
    {
      object outputExpression = null;

      if (isOutput)
      {

        outputExpression = SubstitutionOutputExpression();
      }

      Contract.Assert(isOutput == (outputExpression != null));
    }

	[ClousotRegressionTest]
    public object SubstitutionOutputExpression()
    {
      return "ciao";
    }
  }
}


namespace AutoProperties
{
  public class A : IDisposable
  {
    public string Property { get; private set; }

    public bool IsDone { 
		[ClousotRegressionTest]
		get { return this.Property == null; } }

	[ClousotRegressionTest]
    public  A(string s)
    {
      Contract.Requires(s != null);

      this.Property = s;
    } // we do not infer here this.Property != null, because of dispose later

    // Should warn because we are not inferring the property
    [ClousotRegressionTest]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Missing precondition in an externally visible method. Consider adding Contract.Requires(this.Property != null); for parameter validation",PrimaryILOffset=2,MethodILOffset=0)]
	public void DoSomething()
    {
      Contract.Assert(this.Property != null);
    }

    // Should not warn because of the precondition
    [ClousotRegressionTest]
	public void DoSomethingWithContract()
    {
      Contract.Requires(!this.IsDone);

      Contract.Assert(this.Property != null);
    }

	[ClousotRegressionTest]
    public void Dispose()
    {
      if(Property != null)
      {
        // bla bla
        Property = null;
      }
    }
  }

    public class B
  {
    public string Property { get; private set; }

	[ClousotRegressionTest]
    public B()
    {
      this.Property = null;
    } // We do not infer this.Property == null, this is wrong, we should fix it

	[ClousotRegressionTest]
    public B(string value)
    {
      Contract.Requires(value != null);
      this.Property = value;
    } // We infer this.Property != null

	[ClousotRegressionTest]
    public void DoSomething()
    {
		// As we do not consider the path from B(), then we can prove the assertion, but this is wrong and we should fix it
      Contract.Assert(this.Property != null);
    }
  }
  }
  
namespace Nikolai
{
  public static class Range
  {
    [Pure]
    [ContractVerification(false)]
    public static bool IsValid(int index, int count, int available)
    {
      Contract.Requires(available >= 0);
      Contract.Ensures(!Contract.Result<bool>() || index >= 0);
      Contract.Ensures(!Contract.Result<bool>() || count >= 0);
      Contract.Ensures(!Contract.Result<bool>() || index <= available);
      Contract.Ensures(!Contract.Result<bool>() || count <= available - index);

      unchecked
      {
        return ((uint)index <= (uint)available) && ((uint)count <= (uint)(available - index));
      }

      // The above is equivalent to 
      // return index >= 0 && index <= available && count >= 0 && count <= available - index;
    }

  }

  public class StringSegment
  {
    [Pure]
    public int Length
    {
	[ClousotRegressionTest]
      get
      {
        return m_length;
      }
    }

    private readonly int m_index;
    private readonly int m_length;
    private readonly string m_value;

    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(m_value != null);
      Contract.Invariant(m_index >= 0);
      Contract.Invariant(m_length >= 0);
      Contract.Invariant(m_index <= m_value.Length);
    }


	[ClousotRegressionTest]
	public StringSegment(string value, int index, int length)
    {
      Contract.Requires(value != null);
      Contract.Requires(Range.IsValid(index, length, value.Length));

      m_value = value;
      m_index = index;
      m_length = length;
    }


	[ClousotRegressionTest]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="requires unproven: index < this.Length. Are you making some assumption on System.String.get_Length that the static checker is unaware of? ",PrimaryILOffset=33,MethodILOffset=91)]
    public bool Equals8Bit(byte[] buffer, int bufferIndex)
    {
      Contract.Requires(buffer != null);
      Contract.Requires(Range.IsValid(bufferIndex, Length, buffer.Length));

      var valueIndex = m_index;
      var value = m_value;
      var length = m_length;
      
      Contract.Assert(length <= buffer.Length - bufferIndex); // Keep this assertion as it helps CC to do the proof - found a bug in cccheck with disjunction and linear inequalities

      for (int i = 0; i < length; i++)
      {
        var storedCh = (char)buffer[bufferIndex + i];

//        Contract.Assert(valueIndex + i < value.Length); // Made it explicit 
        if (storedCh != value[valueIndex + i])
        {
          return false;
        }
      }

      return true;
    }
 }
}

namespace FixesForABetterErrorTrace
{
  public class Ex
  {
	[ClousotRegressionTest]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Missing precondition in an externally visible method. Consider adding Contract.Requires(0 != size); for parameter validation. Otherwise the following sequence of method calls may cause an error. Sequence: API -> CreateArray -> Max",PrimaryILOffset=3,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Missing precondition in an externally visible method. Consider adding Contract.Requires((size >= 0 || size != Int32.MinValue)); for parameter validation",PrimaryILOffset=3,MethodILOffset=0)]
    public void API(int size)
    {
      if (size < 0) size = -size;
      CreateArray(size);
    }

	[ClousotRegressionTest]
    private void CreateArray(int size)
    {
	  // Will infer size > 0
      var array = new int[size];
      // ... fill the array ...
      var max = Max(array);
    }

	[ClousotRegressionTest]
    private int Max(int[] a)
    {
	  // Will infer
      // Contract.Requires(a != null);
      // Contract.Requires(a.Length > 0);
      
      var max = a[0];
      for(var i = 1; i < a.Length; i++)
      {
        var tmp = a[i];
        if (tmp > max) max = tmp;
      }

      return max;
    }
  }
}
