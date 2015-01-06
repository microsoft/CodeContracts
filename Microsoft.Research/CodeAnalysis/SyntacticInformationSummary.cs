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
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using Microsoft.Research.DataStructures;

namespace Microsoft.Research.CodeAnalysis
{

  public struct SyntacticTest
  {
    public enum Polarity { Assert, Assume };

    public readonly Polarity Kind;
    public readonly APC PC;
    public readonly string Tag;

    private readonly LazyEval<BoxedExpression> guard;

    public BoxedExpression Guard
    {
      get
      {
        return this.guard.Value;
      }
    }

    public SyntacticTest(Polarity kind, APC pc, string tag, LazyEval<BoxedExpression> guard)
    {
      Contract.Requires(tag != null);
      Contract.Requires(guard != null);

      this.Kind = kind;
      this.PC = pc;
      this.Tag = tag;
      this.guard = guard;
    }

    public override string ToString()
    {
      return string.Format("{0}: {3}({1}) {2}", this.PC, this.Tag, this.Guard, this.Kind);
    }
  }
  public struct RightExpression
  {
    public readonly APC PC;

    private readonly LazyEval<BoxedExpression> assignment;

    public BoxedExpression RValueExpression
    {
      get
      {
        return this.assignment.Value;
      }
    }

    public RightExpression(APC pc, LazyEval<BoxedExpression> assignment)
    {
      Contract.Requires(assignment != null);

      this.PC = pc;
      this.assignment = assignment;
    }

    public override bool Equals(object obj)
    {
      if (obj is RightExpression)
      {
        var other = (RightExpression)obj;
        if (this.PC.Equals(other.PC))
        {
          var exp = this.RValueExpression;
          if (exp != null)
          {
            return exp.Equals(other.RValueExpression);
          }
        }
      }

      return false;
    }

    public override int GetHashCode()
    {
      return this.PC.GetHashCode();
    }
  }
  public struct Renamings<Variable>
  {

    private readonly List<Tuple<Pair<APC, APC>, int>> RenamingsLength;
    private readonly IEnumerable<Tuple<Pair<APC, APC>, Dictionary<Variable, HashSet<Variable>>>> LiveVariablesInRenamings;

    public Renamings(
      List<Tuple<Pair<APC, APC>, int>> renamingsLength,
      List<Tuple<Pair<APC, APC>, Dictionary<Variable, HashSet<Variable>>>> liveVariablesInRenamings)
    {
      Contract.Requires(renamingsLength != null);
      Contract.Requires(liveVariablesInRenamings != null);

      this.RenamingsLength = renamingsLength;
      this.LiveVariablesInRenamings = liveVariablesInRenamings;

#if DEBUG && false
      Console.WriteLine("[RENAMING] #Renamings: {0}, #Max Length {1}, #Average {2}", this.RenamingsLength.Count, this.GetMaxLengthOfARenaming(), this.GetAverageLengthOfARenaming());
#endif
    }

    public int GetMaxLengthOfARenaming()
    {
      return this.RenamingsLength != null && this.RenamingsLength.Any() ?
        this.RenamingsLength.Max(tuple => tuple.Item2) :
        -1;
    }

    public double GetAverageLengthOfARenaming()
    {
      return this.RenamingsLength != null && this.RenamingsLength.Any()?
        this.RenamingsLength.Average(tuple => tuple.Item2) :
         -1.0;
    }

    /// <summary>
    /// The Boolean skip will force the caller to think to the cmd argument
    /// </summary>
    public bool TooManyRenamingsForRefinedAnalysis(bool skip, out string why)
    {
      why = null;
      if (skip)
      {
        return false;
      }

      if(this.RenamingsLength.Any())
      {
        /*
        if(this.RenamingsLength.Count >= Thresholds.Renamings.TooManyRenamings)
        {
          why = string.Format("Too many renamings ({0})", this.RenamingsLength);
          return true;
        }
         */
        var average = this.GetAverageLengthOfARenaming();
        if(average >= Thresholds.Renamings.TooManyVariableRenamingsonAverage)
        {
          why = string.Format("Average length of renaming too large ({0})", average);
          return true;
        }
      }

      return false;
    }

    public bool TryLiveVariablesAtJoinPoint(APC pc, out Set<Variable> liveVars, out Set<Variable> deadVars)
    {
      Contract.Ensures(Contract.Result<bool>() || Contract.ValueAtReturn(out liveVars) == null);
      Contract.Ensures(Contract.Result<bool>() || Contract.ValueAtReturn(out deadVars) == null);

      liveVars = null;
      deadVars = null;

      if(this.LiveVariablesInRenamings == null)
      {
        return false;
      }

      var renamingsAtPC = this.LiveVariablesInRenamings.Where(tuple => tuple.Item1.Two.Equals(pc));
      if(renamingsAtPC.Any() && renamingsAtPC.All(tuple => tuple.Item2 != null))
      {
        var allVariables = new List<Set<Variable>>();
        foreach(var renaming in renamingsAtPC)
        {
          var allVars = new Set<Variable>();
          foreach (var pair in renaming.Item2)
          {
            allVars.AddRange(pair.Value);
          }
          allVariables.Add(allVars);
        }
        // At this point, we have for each renaming the variables
        // Now we compute the intersection to see all the variables live at the join point

        var intersection = new Set<Variable>();
        var union = new Set<Variable>();
        var isFirst = true;
        foreach(var vs in allVariables)
        {
          if(isFirst)
          {
            intersection = new Set<Variable>(vs);
            union = new Set<Variable>(vs);
            isFirst = false;
          }
          else
          {
            intersection = intersection.Intersection(vs);
            union = union.Union(vs);
          }
        }

        liveVars = intersection;
        deadVars = union.Difference(intersection);
        return true;
      }

      return false;
    }

  }

  #region Method Call info

  public struct MethodCallInfo<Method, Variable>
  {
    public readonly APC PC;
    public readonly Method Callee;
    public readonly List<Variable> ActualParameters;

    public MethodCallInfo(APC pc, Method method, List<Variable> actualParameters)
    {
      this.PC = pc;
      this.Callee = method;
      this.ActualParameters = actualParameters;
    }

    public override string ToString()
    {
      return string.Format("{0} : {1}({2} parameters)", this.PC, this.Callee, this.ActualParameters.Count);
    }
  }

  #endregion

  public struct SyntacticInformation<Method, Field, Variable>
  {
    public readonly IDisjunctiveExpressionRefiner<Variable, BoxedExpression> DisjunctionRefiner;
    public readonly IEnumerable<SyntacticTest> TestsInTheMethod;
    public readonly IEnumerable<RightExpression> RightExpressions;
    public readonly Dictionary<Variable, APC> VariableDefinitions;
    public readonly IEnumerable<Field> MayUpdatedFields;
    public readonly IEnumerable<MethodCallInfo<Method, Variable>> MethodCalls;
    public readonly Renamings<Variable> Renamings;
    public readonly bool HasThrow;
    public readonly bool HasExceptionHandlers;

    public IEnumerable<BoxedExpression> AssertedPostconditions
    {
      get
      {
        Contract.Assume(this.TestsInTheMethod != null);

        return this.TestsInTheMethod.Where(t => t.Kind == SyntacticTest.Polarity.Assert && t.Tag == "ensures").Select(t => t.Guard);
      }
    }

    public SyntacticInformation(IDisjunctiveExpressionRefiner<Variable, BoxedExpression> refiner,
      IEnumerable<SyntacticTest> codeTests, IEnumerable<RightExpression> codeExpressions, Dictionary<Variable, APC> variableDefinitions,
      IEnumerable<Field> mayUpdateFields,
      IEnumerable<MethodCallInfo<Method, Variable>> methodCalls,
      List<Tuple<Pair<APC, APC>, Dictionary<Variable, HashSet<Variable>>>> renamingsInfo,
      List<Tuple<Pair<APC, APC>, int>> renamingsLength,
      bool HasThrow, bool HasExceptionHandlers)
    {
      Contract.Requires(refiner != null);
      Contract.Requires(codeTests != null);
      Contract.Requires(codeExpressions != null);
      Contract.Requires(variableDefinitions != null);
      Contract.Requires(mayUpdateFields != null);
      Contract.Requires(methodCalls != null);
      Contract.Requires(renamingsInfo != null);
      Contract.Requires(renamingsLength != null);

      this.DisjunctionRefiner = refiner;
      this.TestsInTheMethod = codeTests.Where(t => t.Guard != null);
      this.RightExpressions = codeExpressions.Where(t => t.RValueExpression != null);
      this.VariableDefinitions = variableDefinitions;
      this.MayUpdatedFields = mayUpdateFields;
      this.MethodCalls = methodCalls;
      this.HasThrow = HasThrow;
      this.HasExceptionHandlers = HasExceptionHandlers;
      this.Renamings = new Renamings<Variable>(renamingsLength, renamingsInfo);
    }
  }
}
