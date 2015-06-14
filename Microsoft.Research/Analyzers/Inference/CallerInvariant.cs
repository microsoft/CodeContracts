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
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Microsoft.Research.CodeAnalysis.Inference
{
  public class CallerInvariantDB<Method>
  {
    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(this.db != null);
    }

    private readonly List<CallInvariant> db;

    public CallerInvariantDB()
    {
      this.db = new List<CallInvariant>();
    }

    public void Notify(Method callee, Method caller, int paramPosition, Predicate info)
    {
      this.db.Add(new CallInvariant(callee, caller, paramPosition, info));
    }

    public IEnumerable<IGrouping<int, CallInvariant>> GetCalleeInvariantsForAMethod(Method callee)
    {
      return this.db.Where(cInv => cInv.Callee.Equals(callee)).GroupBy(cInv => cInv.ParamPosition);
    }

    public IEnumerable<CallInvariant> GetCalleeInvariantsForAMethod(Method callee, int pos)
    {
      Contract.Requires(pos >= 0);

      return this.db.Where(cInv => cInv.ParamPosition == pos && cInv.Callee.Equals(callee));
    }

    public IEnumerable<CallInvariant> GetCalleeInvariantsInAMethod(Method caller)
    {
      return this.db.Where(cInv => cInv.Caller.Equals(caller));
    }

    public void SuggestCallInvariants(IOutput output)
    {
      Contract.Requires(output != null);

      var groupedByCallees = this.db.GroupBy(cInv => cInv.Callee);

      output.WriteLine("Inferred Caller invariants");
      foreach (var callee in groupedByCallees)
      {
        output.WriteLine("Method: {0}", callee.Key);
        var groupedByPosition = callee.GroupBy(cInv => cInv.ParamPosition);
        foreach (var inv in groupedByPosition)
        {
          var nTops = inv.Count(cInv => cInv.Predicate.Equals(PredicateTop.Value));
          var nNotNull = inv.Count(cInv => cInv.Predicate.Equals(PredicateNullness.NotNull));
          var nNull = inv.Count(cInv => cInv.Predicate.Equals(PredicateNullness.Null));
          var tot = (double)(nTops + nNotNull + nNull);

          if (tot == 0) continue;

          output.WriteLine("  Position: {0} (Top:{1}-{4:0.00}%, NN:{2}-{5:0.00}%, N:{3}-{6:0.00}%)", inv.Key, nTops, nNotNull, nNull, nTops/tot, nNotNull/tot, nNull/tot);

          /*
          foreach (var value in inv)
          {
            output.WriteLine("    Value: {0}", value.ToString());
          }
           */
        }
      }
    }


    #region CallInvariant

    public class CallInvariant
    {
      public readonly Method Callee;
      public readonly Method Caller;
      public readonly int ParamPosition;
      public readonly Predicate Predicate;

      public CallInvariant(Method callee, Method caller, int paramPosition, Predicate info)
      {
        Contract.Requires(paramPosition >= 0);
        Contract.Requires(info != null);

        this.Callee = callee;
        this.Caller = caller;
        this.ParamPosition = paramPosition;
        this.Predicate = info;
      }

      public override string ToString()
      {
        return string.Format("<{0}, {1}, {2}, {3}>", this.Callee, this.Caller, this.ParamPosition, this.Predicate);
      }

    #endregion
    }

  }

  [ContractClass(typeof(PredicateContracts))]
  public abstract class Predicate
  {
    abstract public Predicate JoinWith(Predicate other);
  }

  #region Contracts

  [ContractClassFor(typeof(Predicate))]
  abstract class PredicateContracts : Predicate
  {
    public override Predicate JoinWith(Predicate other)
    {
      Contract.Requires(other != null);
      Contract.Ensures(Contract.Result<Predicate>() != null);
      return null;
    }
  }
  
  #endregion

  public class PredicateTop : Predicate
  {

    public static PredicateTop Value { get { return new PredicateTop(); } }

    private PredicateTop() { }

    public override Predicate JoinWith(Predicate other)
    {
      return this;
    }

    public override bool Equals(object obj)
    {
      return obj is PredicateTop;
    }

    public override int GetHashCode()
    {
      return 1;
    }

    public override string ToString()
    {
      return "top";
    }
  }

  public class PredicateNullness : Predicate
  {
    public enum Kind {Null, NotNull}

    private readonly Kind value;

    public static PredicateNullness Null { get { return new PredicateNullness(Kind.Null); } }
    public static PredicateNullness NotNull { get { return new PredicateNullness(Kind.NotNull); } }

    private PredicateNullness(Kind kind)
    {
      this.value = kind;
    }

    public override Predicate JoinWith(Predicate other)
    {
      var right = other as PredicateNullness;
      if (other != null)
      {
        if (this.value == right.value)
        {
          return this;
        }
      }

      return PredicateTop.Value;
    }

    public override bool Equals(object obj)
    {
      var other = obj as PredicateNullness;
      if (other != null)
      {
        return this.value == other.value;
      }

      return false;
    }

    public override int GetHashCode()
    {
      return (int)this.value;
    }

    public override string ToString()
    {
      return this.value.ToString();
    }
  }
}
