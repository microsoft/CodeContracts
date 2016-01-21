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

namespace Herman
{
  public class UIntRepro
  {    
    public uint Length;
    private uint capacity;

    [ContractVerification(false)]
    public UIntRepro(uint capacity)
    {
      this.capacity = capacity;
    }
    
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=1,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=15,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=33,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"assert unproven",PrimaryILOffset=27,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=50,MethodILOffset=0)]
    private void AddCapacity()
    {
      Contract.Requires(this.capacity < uint.MaxValue); 
      
      Contract.Assert(this.capacity > 0x1000000);
      var n = this.capacity >> 24;
      Contract.Assert(n < 0x100);   // Clousot (wrongly) reported this assertion to be unreached
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=2,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=28,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=34,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=48,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=78,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=64,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=21,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=56,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=72,MethodILOffset=0)]
    public void Grow(uint newLength)
    {
      Contract.Requires(newLength > this.Length);

      Contract.Assert(newLength <= uint.MaxValue);  // True

     
      this.Length = newLength;

      Contract.Assume(this.capacity < newLength);  
      Contract.Assert(this.capacity < uint.MaxValue); // Clousot (wrongly) reported this assertion to be unreached
                                                      // The assertion is true because newLength <= uint.MaxValue
      // Dummy loop, but we want to force testing Clousot to prove the assertion
      while (this.capacity < newLength)
      {  
        Contract.Assert(this.capacity < uint.MaxValue); 
      }
    }
    
    protected uint StackHeight
    {
      get { return this.stackHeight; }
      
      [ClousotRegressionTest]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=14,MethodILOffset=0)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=21,MethodILOffset=0)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=30,MethodILOffset=0)]
      [RegressionOutcome(Outcome=ProofOutcome.Top,Message="assert unproven",PrimaryILOffset=7,MethodILOffset=0)]
      set
      {
        Contract.Assert(value != uint.MaxValue); // We should not prove it, but we were doing because uint.MaxValue is "-1"
        this.stackHeight = value;
        if (value >= this.maxStack) this.maxStack = value;
      }
    }
    private uint stackHeight;
    private uint maxStack;

  }

  public class Repro<T>
  {
    public uint Length;
    private uint capacity;

    [ContractVerification(false)]
    public Repro(uint capacity)
    {
      this.capacity = capacity;
    }

    public T[] elements = new T[0x100];
    public T[][][] index2;
    public T[][][][] index3;

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=1,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=15,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=25,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Possible use of a null array 'this.index3'",PrimaryILOffset=32,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=35,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"assert unproven",PrimaryILOffset=72,MethodILOffset=0)]
    private void AddCapacity()
    {
      Contract.Requires(this.capacity < uint.MaxValue);

      var n = this.capacity >> 24;
      var index2 = this.index3[n];

      var m = (this.capacity & 0xFF0000) >> 16;
      if (m == 0)
      {
        index2 = new T[0x100][][];
      }
      else
      {  // An overflow in the abstract semantics of "&" caused Clousot to infer m == 0 and then flag this assertion as unreached. Which is false.
        Contract.Assert(index2 != null);
      }
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=1,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=48,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=66,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=89,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Possible use of a null array 'this.index3'",PrimaryILOffset=94,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=109,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=116,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=122,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=15,MethodILOffset=139)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"assert unproven",PrimaryILOffset=60,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=83,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"assert unproven",PrimaryILOffset=103,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"assert unproven",PrimaryILOffset=134,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.False,Message=@"ensures is false: this.capacity == Contract.OldValue<ulong>(this.capacity) + 0x100",PrimaryILOffset=42,MethodILOffset=139)]
    private void AddCapacity_BugOnEdgeConversion()
    {
      Contract.Requires(this.capacity < uint.MaxValue); 
      Contract.Ensures(this.capacity == Contract.OldValue<ulong>(this.capacity) + 0x100);
      
      Contract.Assert(this.capacity > 0x1000000);
      var n = this.capacity >> 24;
      Contract.Assert(n < 0x100);
      Contract.Assert(this.index3.Length == 0x100);
      var index2 = this.index3[n];
      Contract.Assert(index2 == null || index2.Length == 0x100); // This assertion was unreached because the join returned bottom. this was wrong because a wrong interpretation of the edge conversion
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=1,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=24,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Possible use of a null array 'this.index3'",PrimaryILOffset=31,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=34,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=64,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=72,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=18,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"assert unproven",PrimaryILOffset=81,MethodILOffset=0)]
    private void AddCapacity_BugSomewhere()
    {
      var n = this.capacity >> 24;
      Contract.Assert(n < 0x100);
      var index2 = this.index3[n];
      var m = (this.capacity & 0xFF0000) >> 16;
      if (m == 0)
      {
        index2 = new T[0x100][][];
        this.index3[n] = index2;
      }
      else
      {
        Contract.Assert(index2 != null);
      }
    }
  }

  public class Repro_BackwardsDisjunctiveRefinementBug<T>
  {
    [ContractVerification(false)]
    public Repro_BackwardsDisjunctiveRefinementBug(uint capacity)
    {
      this.capacity = capacity;
    }

    public T[] elements;
    public T[][] index1;
    public T[][][] index2;
    public T[][][][] index3;

    public uint Length;
    public uint capacity;

    [ContractInvariantMethod]
    void ObjectInvariant()
    {      
      Contract.Invariant(
        (this.elements != null && this.index1 == null && this.index2 == null && this.index3 == null && this.capacity == 0x100) ||
        (this.elements == null && this.index1 != null && this.index2 == null && this.index3 == null && this.capacity > 0x100 && this.capacity <= 0x10000) ||
        (this.elements == null && this.index1 == null && this.index2 != null && this.index3 == null && this.capacity > 0x10000 && this.capacity <= 0x1000000) ||
        (this.elements == null && this.index1 == null && this.index2 == null && this.index3 != null && this.capacity > 0x1000000));
    }

    [ClousotRegressionTest("cci1only")]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=1,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"assert unproven",PrimaryILOffset=13,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"invariant is valid",PrimaryILOffset=215,MethodILOffset=18)]
    private void AddCapacity()
    {
      Contract.Assert(this.capacity > 0x1000000); // Should be unproven. It was unreached because of a bug in the backwards expression reconstruction when the formula was too big
    }
  }

  public class BugInWPNormalization
  {
    public int[] elements;
 
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=1,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=9,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as array)",PrimaryILOffset=14,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"assert unproven",PrimaryILOffset=35,MethodILOffset=0)]
    public  void Test(uint index)
    {
      Contract.Requires(this.elements == null || this.elements.Length == 0x100);
      
      Contract.Assert(index > 0);  // the normalization of expressions in the WP was wrong for CLE_UN and caused this assertion to be declared true
    }  
  }



  public class BugInBitwiseAndWithUInt64<T>
  {
    public ulong capacity = 0x100;
    T[] elements = new T[0x100];

    [ContractInvariantMethod]
    void ObjectInvariant()
    {
      Contract.Invariant(this.capacity >= 0x100);
      Contract.Invariant((this.capacity & 0xFF) == 0);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=1,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"assert unproven",PrimaryILOffset=12,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"invariant is valid",PrimaryILOffset=17,MethodILOffset=18)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"invariant is valid",PrimaryILOffset=39,MethodILOffset=18)]
    private T[] GetElements(uint index)
    {
      var elements = this.elements;
      Contract.Assert(index > 123); // unproven, was unreached
      return elements;
    }
  }

   public class BugWithULong<T>
  {
    public uint Length;

    ulong capacity = 0x100;
    public  T[][][][] index3;

    // F: TODO
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=1,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=24,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=36,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=42,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"assert unproven",PrimaryILOffset=58,MethodILOffset=0)]
    private void AddCapacity()
    {
      Contract.Requires((this.capacity & 0xFF) == 0);

      this.capacity += 0xFF;

      Contract.Assert((this.capacity & 0x100) == 0);
    }

    
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=2,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=64,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=43,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as receiver)",PrimaryILOffset=58,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=75,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as field receiver)",PrimaryILOffset=15,MethodILOffset=80)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=35,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=52,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"requires unproven: (this.capacity & 0xFF) == 0",PrimaryILOffset=17,MethodILOffset=58)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"ensures is valid",PrimaryILOffset=23,MethodILOffset=80)]
    public void Grow(uint newLength)
    {
      Contract.Requires(newLength > this.Length);
      Contract.Ensures(this.Length == newLength);

      Contract.Assert(newLength <= uint.MaxValue);  

      while (this.capacity < newLength)
      {
        Contract.Assert(this.capacity < uint.MaxValue); //because newLength <= uint.MaxValue
        this.AddCapacity(); // Need a method call there, otherwise the bug did not show up
      }

      this.Length = newLength;
    }
  }

  public class Shorts
  {
    // we use to say the assert was unrechable
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="assert unproven. The static checker determined that the condition 'i <= 32767' should hold on entry. Nevertheless, the condition may be too strong for the callers. If you think it is ok, add a precondition to document it: Contract.Requires(i <= 32767);",PrimaryILOffset=17,MethodILOffset=0)]
    static void Foo(int i)
    {
      if (i == (short)i) return;
      Contract.Assert(i <= short.MaxValue);
    }
  }
  
  public class FalseUnreachedRepro
  {
    public Func<ushort, int> foo;
    public Func<int[], int> foo1;

      // The  array analysis was deducing, wrongly, that pkToken.Length == 8
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=13,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=30,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as array)",PrimaryILOffset=47,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as array)",PrimaryILOffset=53,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as array)",PrimaryILOffset=67,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as field receiver)",PrimaryILOffset=94,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=100,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Bottom,Message="reference use unreached",PrimaryILOffset=108,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Bottom,Message="reference use unreached",PrimaryILOffset=114,MethodILOffset=0)]
//    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="assert unproven",PrimaryILOffset=84,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="assert unproven. Is it an off-by-one? The static checker can prove cbPublicKeyToken > (0 - 1) instead",PrimaryILOffset=84,MethodILOffset=0)]
    public void Foo(int[] pkToken)
    {
      Contract.Requires(pkToken != null);
      Contract.Assume(foo != null);
      Contract.Assume(foo1 != null);
      Contract.Assume(pkToken.Length == 8 || pkToken.Length == 0);
      ushort cbPublicKeyToken = (ushort)pkToken.Length;
      if (cbPublicKeyToken > 0)
      {
        cbPublicKeyToken = (ushort)(cbPublicKeyToken << 1 + 1); //the 1 is a flag indicating that there is a public key token
      }

      Contract.Assert(cbPublicKeyToken > 0); // Should be unproven!
      
      //this.foo(cbPublicKeyToken);
      if (cbPublicKeyToken > 0)
        this.foo1(pkToken);
      else
        this.foo(0); // we were reporting this to be unreached
    }
  }
}

namespace Herman.Types
{
public class Use
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="requires is valid",PrimaryILOffset=12,MethodILOffset=18)]
    public static Module GetCodeModelFromMetadataModel(IModule module)
    {
      Contract.Requires(!(module is Dummy));

      return GetCodeModelFromMetadataModelHelper(module);
    }

    [ContractVerification(false)]
    private static Module GetCodeModelFromMetadataModelHelper(IModule module)
    {
      Contract.Requires(!(module is Dummy));

      throw new NotImplementedException();
    }
  }

  public interface IModule
  {
  }

  public class Dummy : IModule
  { 
  }

  public class Module : IModule
  {
  }
}

namespace FoxtrotExtractor
{
 public class FoxTrotSmallRepro
  {

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possibly calling a method on a null reference 'm'",PrimaryILOffset=23,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=5,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=11,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="requires is valid",PrimaryILOffset=9,MethodILOffset=11)]
    public void SmallReproWithWhile(MethodContract m)
    {
      for (var i = 0; i < m.RequiresCount; i++)
      {
        // We can prove all of this asserts, they are here only to make it explicit
        // We commented for *not* helping the analysis 
        /*
        Contract.Assert(m.RequiresCount > 0);
        Contract.Assert(i < m.RequiresCount);

        Contract.Assert(m.Requires.Count > 0);
        */
     
        //Contract.Assert(m.Requires.Count == m.RequiresCount);
        //Contract.Assert(i < m.Requires.Count);

        // to prove it, we should infer the equality m.Requires.Count == m.RequiresCount
        // At this aim, we need the combination of Karr and LT abstract domains
        var foo = m.Requires[i];
      }
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possibly calling a method on a null reference 'm'",PrimaryILOffset=4,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=12,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=18,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="requires is valid",PrimaryILOffset=9,MethodILOffset=18)]    
    public void SmallReproWithIf(MethodContract m)
    {      
      var i = 0; 
      if(i < m.RequiresCount)
      {
        var foo = m.Requires[i];
      }
    }    
  }

  public class MethodContract
  {
    private MyList requires;

    public MethodContract(MyList r)
    {
      this.requires = r;
    }

    public MyList Requires
    {
      get
      {
        Contract.Ensures(Contract.Result<MyList>() == this.requires);
        return this.requires;
      }
    }

    public int RequiresCount
    {
      get
      {        
        Contract.Ensures(Contract.Result<int>() == 0 || this.Requires != null);
        Contract.Ensures(this.Requires == null || Contract.Result<int>() == this.Requires.Count);

        var reqs = this.Requires;
        if (reqs == null) return 0;
        return reqs.Count;
      }
    }
  }

  [ContractVerification(false)]
  public class MyList
  {
    public int Count { get {
      Contract.Ensures(Contract.Result<int>() >= 0);
      
      return 12324; } }
    public object this[int i]
    {
      get
      {
       // Contract.Requires(0 <= i);
        Contract.Requires(i < this.Count);

        return null;
      }
    }
  }
}