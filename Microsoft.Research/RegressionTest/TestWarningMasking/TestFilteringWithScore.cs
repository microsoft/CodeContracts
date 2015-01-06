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

// The regression defines 4 cases as a compiler switch: LOW, MEDIUMLOW, MEDIUM, FULL
// We expect to see less messages with LOW, more with MEDIUMLOW, MEDIUM and all with FULL
// This file is to test the behavior

using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Research.ClousotRegression;
using System.Diagnostics.Contracts;
using System.Collections.Generic;

#pragma warning disable 0649
namespace Full
{
  public class WarningScores
  {
    public string f;
    
    [ClousotRegressionTest]
#if MEDIUM || MEDIUMLOW || LOW
    // masked
#endif
#if FULL
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Possibly accessing a field on a null reference 'w'",PrimaryILOffset=1,MethodILOffset=0)] // (score: 0.0135)
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"assert unproven",PrimaryILOffset=12,MethodILOffset=0)] // (score: 0.006) 
#endif    
    public int OtherObject(WarningScores w)
    {
      Contract.Assert(w.f != null);

      return w.f.Length;
    }

#if MEDIUM || MEDIUMLOW ||LOW
    // masked
#endif
#if FULL
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Possibly calling a method on a null reference 'this.f'",PrimaryILOffset=6,MethodILOffset=0)] // (score: 0.00416666666666667) 
#endif
    [ClousotRegressionTest]    
    public int MissingObjectInvariant()
    {
      return f.Length;
    }
    
    [ClousotRegressionTest]
#if FULL
 [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Possibly calling a method on a null reference 's'",PrimaryILOffset=1,MethodILOffset=0)] // (score: 0.015)
#endif
#if MEDIUM || MEDIUMLOW ||LOW
    // masked
#endif
    public int MissingPrecondition(string s)
    {
      return s.Length;
    }

    [ClousotRegressionTest]

#if LOW|| MEDIUMLOW
    // nothing to show
#endif
    // We want to shot it with mediumlow, as we have all the code, so we should fix it
#if FULL || MEDIUM 
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"requires unproven: x >= 0. Are you making some assumption on MissingPrecondition that the static checker is unaware of? ",PrimaryILOffset=7,MethodILOffset=22)] 
#endif
    public void MissingPostconditionInAPrecondition(string s)
    {
      Contract.Requires(s != null);
      var l = MissingPrecondition(s);
      RequiresNonNegative(l);
    }

    [ClousotRegressionTest] 
#if LOW|| MEDIUMLOW
    // nothing to show
#endif

#if FULL || MEDIUM 
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"assert unproven. Are you making some assumption on MissingPrecondition that the static checker is unaware of? ",PrimaryILOffset=27,MethodILOffset=0)] // (score: 60	
#endif
    public void MissingPostconditionInAnAssert(string s)
    {
      Contract.Requires(s != null);
      var l = MissingPrecondition(s);
      Contract.Assert(l >= 0);
    }

    [ClousotRegressionTest]
#if LOW || MEDIUM || MEDIUMLOW
#endif
#if FULL
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Possibly calling a method on a null reference 'collection'",PrimaryILOffset=3,MethodILOffset=0)] // (score: 0.015) 
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Possibly calling a method on a null reference 'x'. Do you expect that System.Collections.Generic.IEnumerator`1<System.String>.get_Current returns non-null? ",PrimaryILOffset=20,MethodILOffset=0)] // (score: 25) 
#endif
    public void IterNotNull(IEnumerable<string> collection)
    {
      int l = 0;
      foreach (var x in collection)
        {
            l += x.Length;
        }
    }

    [ClousotRegressionTest]
#if FULL
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possibly calling a method on a null reference 's'",PrimaryILOffset=32,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possibly calling a method on a null reference 't'. The static checker determined that the condition 't != null' should hold on entry. Nevertheless, the condition may be too strong for the callers. If you think it is ok, add a precondition to document it: Contract.Requires(t != null);",PrimaryILOffset=21,MethodILOffset=0)]
#endif
    public void Func(string s, List<char> t)
    {
        // We do not infer a precondition for t, but we detect it is unmodified from the entry
      foreach (var ch in s)
      {
        if(ch == 'a')
          t.Add(ch);
      }
    }
    
    [ClousotRegressionTest]
    public void RequiresNonNegative(int x)
    {
      Contract.Requires(x >= 0);
    }
  }

  static public class AlwaysShow
  {
    static public void NonNull(string s)
    {
      Contract.Requires(s != null);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.False,Message="requires is false: s != null",PrimaryILOffset=7,MethodILOffset=1)]
#if CLOUSOT2
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Invoking method 'CallWithNull' will always lead to an error. If this is wanted, consider adding Contract.Requires(false) to document it",PrimaryILOffset=0,MethodILOffset=0)]
#else 
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Invoking method 'CallWithNull' will always lead to an error. If this is wanted, consider adding Contract.Requires(false) to document it",PrimaryILOffset=1,MethodILOffset=0)]
#endif
    static public void CallWithNull()
    {
      NonNull(null);
    }
  
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.False,Message="Calling a method on a null reference 'o'. Maybe the guard o == null is too weak? ",PrimaryILOffset=4,MethodILOffset=0)]
    static public int WrongCheck(object o)
    {
      if (o == null)
      {
        return o.GetHashCode();
      }
      return 1;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.False,Message="Cannot create an array of negative length. Maybe the guard z < 0 is too weak? ",PrimaryILOffset=5,MethodILOffset=0)]
    static public int[] WrongCheckArrayInit(int z)
    {
      if (z < 0)
      {
        return new int[z];
      }

      return null;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"assert unproven. The error may be caused by the initialization of s. ",PrimaryILOffset=26,MethodILOffset=0)]
    static public void ItIsAWarning_Assert()
    {
	  bool b = NonDet();
      string s;
      if(b)
        s = null;
      else
        s = "ciao";

      Contract.Assert(s != null);    // Should never be masked
    }

	[ContractVerification(false)] // Just a dummy call
	static private bool NonDet()
	{
      return false;
	}
	
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Possibly calling a method on a null reference 's'. The error may be caused by the initialization of s. ",PrimaryILOffset=14,MethodILOffset=0)]
    static public int ItIsAWarning_Deref(bool b)
    {
      string s;
      if(b)
        s = null;
      else
        s = "ciao";

      return s.Length;    // Should never be masked
    }
  
  }
  
 
  public class MaskBecauseOfAssumptions
  {

    int foo;
    int[] z;
    
    [ClousotRegressionTest]
#if FULL
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Array access might be below the lower bound",PrimaryILOffset=12,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Array access might be above the upper bound",PrimaryILOffset=12,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible use of a null array 'this.z'",PrimaryILOffset=12,MethodILOffset=0)]
#endif
    public int ExpectFoo()
    {
      return this.z[foo];
    }

  }
  
  public class MaskBecauseOutParam
  {
    public Dictionary<string, string> myDict;

	[ClousotRegressionTest]
#if MEDIUMLOW || LOW
#endif
#if MEDIUM || FULL
#endif
#if FULL
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"ensures unproven: Contract.Result<string>() != null. The variable 'res' flows from an out parameter. Consider adding a postconditon to the callee or an assumption after the call to document it",PrimaryILOffset=23,MethodILOffset=45)]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possibly calling a method on a null reference 'this.myDict'",PrimaryILOffset=37,MethodILOffset=0)]
#endif
    public string Foo(string s)
    {
      Contract.Requires(s != null);
      Contract.Ensures(Contract.Result<string>() != null);

      string res;
      if (myDict.TryGetValue(s, out res))
      {
        return res;
      }

      res = "ciao";

      return res;
    }
  }
}


namespace ReadonlyAccessMasking
{
  public class ReadOnly<T>
    where T : class
  {
    private readonly T p;

    public ReadOnly(T p)
    {
      if (p == null)
      {
        throw new ArgumentNullException("p");
      }

      this.p = p;
    }

    [ClousotRegressionTest]
#if FULL
    // Masked because readonly
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"assert unproven",PrimaryILOffset=17,MethodILOffset=0)]
#endif
    public void SomeMethod()
    {
      Contract.Assert(this.p != null);
    }

    [ClousotRegressionTest]
#if FULL
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Possibly accessing a field on a null reference 'other'",PrimaryILOffset=1,MethodILOffset=0)]
    // Masked because readonly
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"assert unproven",PrimaryILOffset=17,MethodILOffset=0)]
#endif
    public void AssertOther(ReadOnly<T> other)
    {
      Contract.Assert(other.p != null);
    }

    [ClousotRegressionTest]
    public string MyToString()
    {
      return this.p.ToString();
    }
  }
}

namespace ForAllFiltering
{
  public class Iterator
  {
    [ClousotRegressionTest]
#if FULL
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"ensures unproven: Contract.ForAll(Contract.Result<IEnumerable<T>>(), x => x != null)",PrimaryILOffset=22,MethodILOffset=28)]
#endif
    public IEnumerable<T> GetIterator<T>(List<T> myList)
    {
      Contract.Ensures(Contract.ForAll(Contract.Result<IEnumerable<T>>(), x => x != null));

      return myList;
    }
  }
}

namespace UseCodeFixes
{
  public class Ranking
  {
    [ClousotRegressionTest]
    // We promote this warning to be shown with low, as the code fix determines the guard should be less permessive
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Array access might be above the upper bound. Maybe the guard i <= args.Length is too weak? ",PrimaryILOffset=7,MethodILOffset=0)]
#if FULL
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible use of a null array 'args'",PrimaryILOffset=14,MethodILOffset=0)]
#endif
    static void Foo(string[] args)
    {
      for (var i = 0; i <= args.Length; i++)
      {
        args[i] = null;
      }
    }    
  }
  
  static public class OffByByOne
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Array access might be above the upper bound",PrimaryILOffset=59,MethodILOffset=0)]
#if MEDIUM || MEDIUMLOW || LOW
    // nothing
#endif
#if FULL
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible use of a null array 'list'",PrimaryILOffset=16,MethodILOffset=0)]
#endif
    static public string[] Add(string[] list, ref int count, string value)
    {
      Contract.Requires(count >= 0);
      Contract.Requires(count <= list.Length);

      if(count == list.Length)
      {
        var tmp = new string[count *2];
        list = tmp;
      }
      list[count++] = value;

      return list;
    }
  }
}

namespace RoslynCSharpCompiler
{
  public class SyntaxTree { }
  public class SyntaxNode
  {
    internal readonly SyntaxNode Green;

    internal virtual SyntaxNode ToRed(SyntaxNode parent, int position)
    {
      throw new NotImplementedException();
    }

    private SyntaxTree syntaxTree;
    
    [ClousotRegressionTest]
#if LOW || MEDIUMLOW
    // nothing
#endif
#if FULL || MEDIUM 
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possibly accessing a field on a null reference 'clone'. Are you making some assumption on ToRed that the static checker is unaware of? ",PrimaryILOffset=31,MethodILOffset=0)]
#endif
#if FULL
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possibly accessing a field on a null reference 'node'",PrimaryILOffset=6,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possibly calling a method on a null reference '((RoslynCSharpCompiler.SyntaxNode)node).Green'",PrimaryILOffset=13,MethodILOffset=0)]
#endif 
    internal static T CloneNodeAsRoot<T>(T node, SyntaxTree syntaxTree)
      where T : SyntaxNode
    {
      T clone = (T)node.Green.ToRed(null, 0);
      clone.syntaxTree = syntaxTree;
      return clone;
    }
  }
  
  abstract public class Proxy
  {
    public List<object> Sizes;
  }

  public class SmallReproForAnonymousCall
  {
    [ClousotRegressionTest]
#if LOW || MEDIUMLOW || MEDIUM
	// nothing to show
#endif
#if FULL
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possibly calling a method on a null reference. Do you expect that System.Collections.Generic.List`1<System.Object>.get_Item(System.Int32) returns non-null? ",PrimaryILOffset=30,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possibly accessing a field on a null reference 'rankSpec'",PrimaryILOffset=1,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possibly calling a method on a null reference 'rankSpec.Sizes'",PrimaryILOffset=6,MethodILOffset=0)]
#endif
    private static int GetNumberOfNonOmittedArraySizes(Proxy rankSpec)
    {
      int count = rankSpec.Sizes.Count;
      int result = 0;
      for (int i = 0; i < count; i++)
      {
        if (rankSpec.Sizes[i].ToString() != "")
        {
          result++;
        }
      }
      return result;
    }
  }
  
  public abstract class SmallReproForLowerBoundAccess
  {
    protected SyntaxNode CurrentNode;

    int tokenOffset;
    protected SyntaxNode[] blendedTokens; 

    [ClousotRegressionTest]
#if FULL
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Array access might be above the upper bound",PrimaryILOffset=58,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible use of a null array 'this.blendedTokens'",PrimaryILOffset=58,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Array access might be below the lower bound",PrimaryILOffset=58,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possibly accessing a field on a null reference 'this.CurrentNode'",PrimaryILOffset=6,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible use of a null array 'this.blendedTokens'",PrimaryILOffset=24,MethodILOffset=0)]
#endif
    protected void EatNode()
    {
      SyntaxNode result = this.CurrentNode.Green;
      if (this.tokenOffset >= this.blendedTokens.Length)
      {
        this.AddTokenSlot();
      }
      this.blendedTokens[this.tokenOffset++] = null;
    }

    abstract public void AddTokenSlot();
  }

  public class SmallReproForCastFromList
  {
    [ClousotRegressionTest]
#if LOW || MEDIUM || MEDIUMLOW
#endif
#if FULL
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possibly calling a method on a null reference 'asStr'. Are you making some assumption on get_Item that the static checker is unaware of? ",PrimaryILOffset=26,MethodILOffset=0)]
#endif
    public bool Repro(List<object> objects)
    {
      if (objects != null && objects.Count > 0)
      {
        string asStr = (string) objects[0];

        return asStr.GetHashCode() % 34 == 2;
      }
      return false;
    }
  }
  
  public class SyntaxTokenList
  {
    private int count;
    private int[] nodes;

    public SyntaxTokenList() { }

    public SyntaxTokenList(int count, int nodes, int n1, int n2, int n3 )
    {
      this.count = count;
      this.nodes = new int[nodes];
    }

    public SyntaxTokenList(int count)
        : this(count, 1, 0, 0, 0)
    {
    }
    
    public SyntaxTokenList(int count, int nodes)
        : this(count, nodes, 0, 0, 0)
    {

    }
    
    public SyntaxTokenList(int count, int nodes, int n1)
        : this(count, nodes, n1, 0, 0)
    {

    }
    
    public SyntaxTokenList(int count, int nodes, int n1, int n2)
        : this(count, nodes, n1, n2, 0)
    {
    }
    
    // We want to mask the array accesses via fields but keep them in the medium warning
    [ClousotRegressionTest]
#if MEDIUM || FULL
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Array access might be above the upper bound. Did you meant 0 instead of 1? ",PrimaryILOffset=97,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Array access might be above the upper bound. Did you meant 1 instead of 2? ",PrimaryILOffset=105,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Array access might be above the upper bound. Did you meant 0 instead of 1? ",PrimaryILOffset=73,MethodILOffset=0)]
#endif
#if FULL
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Array access might be above the upper bound. The static checker determined that the condition '0 < this.nodes.Length' should hold on entry. Nevertheless, the condition may be too strong for the callers. If you think it is ok, add an explicit assumption at entry to document it: Contract.Assume(0 < this.nodes.Length);",PrimaryILOffset=89,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Array access might be above the upper bound. The static checker determined that the condition '0 < this.nodes.Length' should hold on entry. Nevertheless, the condition may be too strong for the callers. If you think it is ok, add an explicit assumption at entry to document it: Contract.Assume(0 < this.nodes.Length);",PrimaryILOffset=65,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Array access might be above the upper bound. The static checker determined that the condition '0 < this.nodes.Length' should hold on entry. Nevertheless, the condition may be too strong for the callers. If you think it is ok, add an explicit assumption at entry to document it: Contract.Assume(0 < this.nodes.Length);",PrimaryILOffset=51,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible use of a null array 'this.nodes'. The static checker determined that the condition 'this.nodes != null' should hold on entry. Nevertheless, the condition may be too strong for the callers. If you think it is ok, add an explicit assumption at entry to document it: Contract.Assume(this.nodes != null);",PrimaryILOffset=89,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible use of a null array 'this.nodes'. The static checker determined that the condition 'this.nodes != null' should hold on entry. Nevertheless, the condition may be too strong for the callers. If you think it is ok, add an explicit assumption at entry to document it: Contract.Assume(this.nodes != null);",PrimaryILOffset=65,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible use of a null array 'this.nodes'. The static checker determined that the condition 'this.nodes != null' should hold on entry. Nevertheless, the condition may be too strong for the callers. If you think it is ok, add an explicit assumption at entry to document it: Contract.Assume(this.nodes != null);",PrimaryILOffset=51,MethodILOffset=0)]
#endif
    public SyntaxTokenList ToList()
    {
      if (this.count <= 0)
      {
        return new SyntaxTokenList();
      }
      switch (this.count)
      {
        case 1:
          return new SyntaxTokenList(this.nodes[0]);

        case 2:
          return new SyntaxTokenList(this.nodes[0], this.nodes[1], 0, 0);

        case 3:
          return new SyntaxTokenList(this.nodes[0], this.nodes[1], this.nodes[2], 0, 0);
      }
      return null;
    }
  }
}

namespace UseWitnesses
{  
  public class TestMayReturnNull
  {
	[ClousotRegressionTest]
    public int[] CanReturnNullOrNotNull(int b)
    {
      if (b > 100)
        return new int[b];
      else
        return null;
    }

	[ClousotRegressionTest]
    public int[] ReturnSomethingThatCanReturnNullOrNotNull(int b)
    {
      if (b > 200)
        return new int[b];
      else
        return CanReturnNullOrNotNull(b);
    }

	[ClousotRegressionTest]
// We always want to see this warning	
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="assert unproven. Are you making some assumption on CanReturnNullOrNotNull that the static checker is unaware of? ",PrimaryILOffset=15,MethodILOffset=0)]
    public void TestThrowNullPointer()
    {
      var res = CanReturnNullOrNotNull(0);
      // here res == null
      Contract.Assert(res != null); // should be false;
    }

	[ClousotRegressionTest]
#if LOW
	// Nothing
#else
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="Possible use of a null array 'res'. Are you making some assumption on ReturnSomethingThatCanReturnNullOrNotNull that the static checker is unaware of? ",PrimaryILOffset=30,MethodILOffset=0)]
#endif
    public int TestThrowNullPointer(int b)
    {
      var res = ReturnSomethingThatCanReturnNullOrNotNull(b);

      int i = 0;
      foreach (var x in res)
      {
        i++;
      }

      return i;
    }
  }

}
