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

//#define MYTRACE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using Microsoft.Research.DataStructures;
using System.Diagnostics;

namespace Microsoft.Research.CodeAnalysis
{
  public class ClassDB<Method, Type>
  {
    #region Private state

    private readonly Func<Method, bool> IsConstructor;
    private readonly Func<Type, int> ConstructorCounter;
    private readonly Func<Type, int> MethodCounter;

    private readonly Dictionary<Type, TypeInfo> classdb;
    private readonly Dictionary<Type, int> type2ConstructorCount;
    private readonly Dictionary<Type, int> type2MethodCount;

    private readonly Dictionary<Type, IEnumerable<BoxedExpression>> inferredInvariants;
    private readonly List<Type> topInvariants;

    #endregion

    #region Public state

    public int IterationCounter = 0;

    public int InferredInvariantsCount
    {
      get
      {
        return inferredInvariants.Values.Aggregate(0, (sum, values) => sum + values.Count());
      }
    }
    public int TypesWithInferredInvariantsCount
    {
      get
      {
        return inferredInvariants.Keys.Count;
      }
    }

    public bool HasNewInvariants = false;

    #endregion

    #region Constructor
    
    public ClassDB(Func<Type, int> ConstructorCounter, Func<Type, int> MethodCounter, Func<Method, bool> IsConstructor)
    {
      Contract.Requires(ConstructorCounter != null);
      Contract.Requires(MethodCounter != null);
      Contract.Requires(IsConstructor != null);

      this.ConstructorCounter = ConstructorCounter;
      this.MethodCounter = MethodCounter;
      this.IsConstructor = IsConstructor;
      this.classdb = new Dictionary<Type, TypeInfo>();
      this.type2ConstructorCount = new Dictionary<Type, int>();
      this.type2MethodCount = new Dictionary<Type, int>();
      this.inferredInvariants = new Dictionary<Type, IEnumerable<BoxedExpression>>();
      this.topInvariants = new List<Type>();
      this.HasNewInvariants = false;
    }

    #endregion

    #region Public interface

    public bool NotifyConstructorAnalysisDone(Type t, Method m, FList<BoxedExpression> posts)
    {
      var constructorsForType = UpdatedConstructorCounter(t);
      if (this.IsConstructor(m))
      {
        var ti = this.GetorAdd(t);
        var analyzedCount = ti.NotifyAnalysisDone(m, posts);

        Contract.Assume(analyzedCount <= constructorsForType);

        // We analyzed all the constructors for the type
        if (analyzedCount == constructorsForType)
        {
          return true;
        }
      }

      return false;
    }

    public bool NotifyMethodOrConstructorAnalysisDone(Type t, Method m, FList<BoxedExpression> posts)
    {
      var methodsForType = UpdateMethodCounter(t);
      var ti = this.GetorAdd(t);
      var analyzedCount = ti.NotifyAnalysisDone(m, posts);

      if (analyzedCount == methodsForType)
      {
        return true;
      }

      return false;
    }

    public void StartNewIteration()
    {
      this.HasNewInvariants = false;

      this.classdb.Clear();
    }

    public IEnumerable<BoxedExpression> Join(Type t)
    {
      TypeInfo info;
      IEnumerable<BoxedExpression> result = null;
      if (this.classdb.TryGetValue(t, out info))
      {
        result = info.Join();
        if (result != null)
        {
          result = result.Where(exp => exp != null);
          var count = result.Count();
          if (count != 0)
          {
#if DEBUG
            Console.WriteLine("So far inferred {0} invariants for {1} types", this.InferredInvariantsCount, this.TypesWithInferredInvariantsCount);
#endif
            AddResult(t, result);
          }
        }
      }

      if (result == null || !result.Any(exp => exp != null))
      {
        this.topInvariants.Add(t);
      }

      return result;
    }

    #endregion

    #region Private methods

    private int UpdatedConstructorCounter(Type t)
    {
      int result;
      if (!this.type2ConstructorCount.TryGetValue(t, out result))
      {
        result = this.type2ConstructorCount[t] = this.ConstructorCounter(t);
#if DEBUG
        Console.WriteLine("Type {0} has {1} constructor(s)", t, result);
#endif
      }
      return result;
    }

    private int UpdateMethodCounter(Type t)
    {
      int result;
      if (!this.type2MethodCount.TryGetValue(t, out result))
      {
        result = this.type2MethodCount[t] = this.MethodCounter(t);
#if DEBUG
        Console.WriteLine("Type {0} has {1} methods(s)", t, result);
#endif
      }
      return result;

    }

    private TypeInfo GetorAdd(Type t)
    {
      TypeInfo result;
      if (!this.classdb.TryGetValue(t, out result))
      {
        result = new TypeInfo();
        this.classdb.Add(t, result);
      }

      return result;
    }

    private void AddResult(Type t, IEnumerable<BoxedExpression> newInvariants)
    {
      IEnumerable<BoxedExpression> prevExpressions;

      // We already saw the type, and got top for it
      if (this.topInvariants.Contains(t))
      {
        return;
      }
      
      if (this.inferredInvariants.TryGetValue(t, out prevExpressions))
      {
        if (ExtensionsForComparingSequencesOfBoxedExpression.IncludedIn(newInvariants, prevExpressions))
        {

          if (newInvariants.Count() == prevExpressions.Count())
          {
#if DEBUG
            Console.WriteLine("Found the same invariant for {0}", t);
#endif
          }
          else
          {
#if DEBUG
            Console.WriteLine("Found a new invariant for {0}", t);
            Console.WriteLine("Previous invariant: {0}", ToString(prevExpressions));
#endif
            this.HasNewInvariants = true;
            this.inferredInvariants[t] = newInvariants;
          }
        }
        else
        {
#if DEBUG
          Console.WriteLine("Removed invariant for {0}", t);
#endif
          this.inferredInvariants.Remove(t);
          this.topInvariants.Add(t);
        }
      }
      else
      {
#if DEBUG
        Console.WriteLine("Found new invariant for {0}", t);
#endif
        this.HasNewInvariants = true;
        this.inferredInvariants[t] = newInvariants;
      }
    }

    public bool HasInvariantForType(Type t)
    {
      return this.inferredInvariants.ContainsKey(t);
    }
    #endregion

    class TypeInfo
    {
      #region Implementation

      private readonly Dictionary<Method, FList<BoxedExpression>> post;

      public TypeInfo()
      {
        this.post = new Dictionary<Method, FList<BoxedExpression>>();
      }

      public int NotifyAnalysisDone(Method m, FList<BoxedExpression> posts)
      {
        this.post[m] = posts;

        return this.post.Keys.Count;
      }

      public IEnumerable<BoxedExpression> Join()
      {
        IEnumerable<BoxedExpression> result = null;
        foreach (var c in this.post.Values)
        {
          if (c == null)
          {
            continue;
          }

          if (result != null)
          {
            var cEnumerable = c.GetEnumerable();
            Print("new value to intersect with", cEnumerable);
            // result = result.Intersect(cEnumerable);

            if (cEnumerable == null)
            {
              Print("Giving up, intersecting with null", null);
              return null;
            }

            result = ExtensionsForComparingSequencesOfBoxedExpression.Intersection(result, cEnumerable);

            Print("result of the intersection", result);
          }
          else
          {
            result = c.GetEnumerable();
            Print("First", result);
          }
          if (result == null || !result.Any())
          {
            Print("Giving up", null);
            return result;
          }
        }

        return result;
      }


      //[Conditional("DEBUG")]
      [Conditional("MYTRACE")]
      private void Print(string str, IEnumerable<BoxedExpression> exps)
      {
        Console.WriteLine(str);
        if (exps == null)
        {
          Console.WriteLine("<null>");
        }
        else
        {
          foreach (var be in exps)
          {
            Console.WriteLine(be);
          }
        }
      }
      #endregion
    }

    public void DumpStatistics(IOutput output)
    {
      output.WriteLine("Inferred object invariants for {0} types. {1} conditions overall",
        this.TypesWithInferredInvariantsCount, this.InferredInvariantsCount);
    }

    private static string ToString(IEnumerable<BoxedExpression> exps)
    {
      if (exps == null)
        return "<null>";

      var result = "";

      foreach (var e in exps)
      {
        result += e.ToString() + " ";
      }

      return result;
    }


  }

  static public class ExtensionsForComparingSequencesOfBoxedExpression
  {

    static public IEnumerable<BoxedExpression> Intersection(IEnumerable<BoxedExpression> prev, IEnumerable<BoxedExpression> next)
    {
      var result = new List<BoxedExpression>();
      if (prev == null || !prev.Any())
      {
        return result;
      }

      foreach (var p in prev)
      {
        if (p == null)
        {
          continue;
        }

        foreach (var n in next)
        {
          if (p.Equals(n))
          {
            result.Add(p);
          }
        }
      }

      return result;
    }

    internal static bool IncludedIn(IEnumerable<BoxedExpression> newInvariants, IEnumerable<BoxedExpression> prevExpressions)
    {

      foreach (var inv in newInvariants)
      {
        foreach (var p in prevExpressions)
        {
          if (inv.Equals(p))
          {
            goto found;
          }
        }
        return false; // found one expression in newInvariants which is not in prevExpressions

        found: 
        ;
      }

      return true;
    }

  }
}
