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

// #define TRACE_PERFORMANCE

#if DEBUG
// F: use the two forms, as I never rember which one
//#define TRACE_JOIN
//#define TRACEJOIN
//#define TRACE_PERFORMANCE
#endif


// #define TRACECLEANDEPS

using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using Microsoft.Research.AbstractDomains.Expressions;
using Microsoft.Research.DataStructures;
using System.Diagnostics.Contracts;
using Microsoft.Research.CodeAnalysis;
using System.Threading.Tasks;
using System.Linq;

namespace Microsoft.Research.AbstractDomains.Numerical
{
  static public class SubPolyhedra
  {
    public enum JoinConstraintInference { Standard, ConvexHull2D, Octagons, CHOct }
    public enum ReductionAlgorithm { Simplex, SimplexOptima, MixSimplexFast, Fast, Complete }

    #region Precision parameters

    [ThreadStatic]
    private static ReductionAlgorithm algorithm; // the algorithm for inferring tighter intervals
    public static ReductionAlgorithm Algorithm
    {
      set
      {
        algorithm = value;
      }
      internal get
      {
        return algorithm;
      }
    }

    [ThreadStatic]
    private static JoinConstraintInference inference; // what constraints we guess at the join
    public static JoinConstraintInference Inference
    {
      set
      {
        inference = value;
      }
      internal get
      {
        return inference;
      }
    }

    #endregion

    [ThreadStatic]
    public static int MaxVariablesInOctagonsConstraintInference;
    public const int MaxHintsToRetainInWidening = 128;

    public const int MaxVariablesToRunSimplex = 500;

    [ThreadStatic]
    public static TimeSpan TimeSpentInReduction = new TimeSpan(0);
  }

  public class SubPolyhedra<Variable, Expression> 
    : ReducedNumericalDomains<LinearEqualitiesForSubpolyhedraEnvironment<Variable, Expression>, IntervalEnvironment<Variable, Expression>, Variable, Expression>
  {
    #region State

    private SPTestTrueVisitor testTrueVisitor;
    private SPTestFalseVisitor testFalseVisitor;
    private SPCheckIfHoldsVisitor checkIfHoldsVisitor;

    private int numberOfWidenings;
    private bool WideningOnHints = false;

    #endregion

    #region Dependencies for slack variables
    private Dictionary<Variable, Polynomial<Variable, Expression>> betaDep; // a map from slack variables to the linear equality that introduced it. Useful since the row echelon form tends to lose this kind of information
    private Hints<Variable, Expression> hints = new Hints<Variable, Expression>(); // linear functionals that we want to bound (i.e.candidates for slack variables). Each one contains excatly one slack variable, for convenience.
    #endregion

    #region Constructors

    public SubPolyhedra(
      LinearEqualitiesForSubpolyhedraEnvironment<Variable, Expression> linEq, 
      IntervalEnvironment<Variable, Expression> intEnv,
      ExpressionManagerWithEncoder<Variable, Expression> expManager)
      : base(linEq, intEnv, expManager)
    {
      Contract.Requires(expManager != null);

      this.betaDep = new Dictionary<Variable, Polynomial<Variable, Expression>>();
      this.hints = new Hints<Variable, Expression>();
      this.numberOfWidenings = 0;
      this.testTrueVisitor = new SubPolyhedra<Variable, Expression>.SPTestTrueVisitor(expManager);
      this.testFalseVisitor = new SubPolyhedra<Variable, Expression>.SPTestFalseVisitor(expManager.Decoder);
      this.checkIfHoldsVisitor = new SubPolyhedra<Variable, Expression>.SPCheckIfHoldsVisitor(expManager);
      this.testTrueVisitor.FalseVisitor = this.testFalseVisitor;
      this.testFalseVisitor.TrueVisitor = this.testTrueVisitor;
    }

    /// <summary>
    /// Construct a subpolyhedra which is the same as <code>prev</code> but for the interval environment, which is <code>intEnv</code>
    /// </summary>
    /// <param name="prev"></param>
    /// <param name="intvEnv"></param>
    private SubPolyhedra(SubPolyhedra<Variable, Expression> prev, IntervalEnvironment<Variable, Expression> intvEnv)
      : base(prev.Left, intvEnv, prev.ExpressionManager)
    {
      this.betaDep = new Dictionary<Variable,Polynomial<Variable,Expression>>(prev.betaDep);
      this.hints = new Hints<Variable, Expression>(prev.hints);
      this.numberOfWidenings = prev.numberOfWidenings;
      this.testFalseVisitor = prev.testFalseVisitor;
      this.testTrueVisitor = prev.testTrueVisitor;          // Mutual recursive calls already set
      this.checkIfHoldsVisitor = prev.checkIfHoldsVisitor;
    }

    public override object Clone()
    {
      return this.DuplicateMe();
    }

    public SubPolyhedra<Variable, Expression> DuplicateMe()
    {
      var result = base.Clone() as SubPolyhedra<Variable, Expression>;

      result.betaDep = new Dictionary<Variable, Polynomial<Variable, Expression>>(this.betaDep);
      result.hints = new Hints<Variable, Expression>(this.hints);
      result.numberOfWidenings = 0;
      result.WideningOnHints = this.WideningOnHints;
      return result;
    }
    #endregion

#if TRACEJOIN
    public override void Assign(Expression x, Expression exp, INumericalAbstractDomainQuery<Variable, Expression> preState)
    {
      this.DebugCheckLessEq("sv27", "sv9", "before assign");
      base.Assign(x, exp, preState);
      this.DebugCheckLessEq("sv27", "sv9", "after assign");
    }
#endif

    #region Test* methods
    public SubPolyhedra<Variable, Expression> Assume(Expression guard)
    {
      return this.TestTrue(guard) as SubPolyhedra<Variable, Expression>;
    }

    public override IAbstractDomainForEnvironments<Variable, Expression> TestTrue(Expression guard)
    {
      var result = this.testTrueVisitor.Visit(guard, this);

      var deps = result.betaDep;
      var hintSet = result.hints;
      
      result = result.ReduceInternal(result.Left, result.Right, false);
      result.betaDep = deps;
      result.hints = hintSet;

      if (!result.IsBottom)
      {
        result.CleanUpBetaVariables();
      }

      return result;
    }

    public override IAbstractDomainForEnvironments<Variable, Expression> TestFalse(Expression guard)
    {
      var result = this.testFalseVisitor.Visit(guard, this);

      // the reduction, if precise enough, will tell us if this branch is unreachable
      var deps = result.betaDep;
      var hintSet = result.hints;

      result = result.ReduceInternal(result.Left, result.Right, false);

      result.betaDep = deps;
      result.hints = hintSet;

      if (!result.IsBottom)
      {
        result.CleanUpBetaVariables();
      }

      return result;
    }

    public override INumericalAbstractDomain<Variable, Expression> TestTrueGeqZero(Expression exp)
    {
      return TestTrue(this.ExpressionManager.Encoder.CompoundExpressionFor(ExpressionType.Bool, ExpressionOperator.GreaterEqualThan, exp, 
        this.ExpressionManager.Encoder.ConstantFor(0))) as INumericalAbstractDomain<Variable, Expression>;
    }

    public override INumericalAbstractDomain<Variable, Expression> TestTrueLessEqualThan(Expression exp1, Expression exp2)
    {
      return TestTrue(this.ExpressionManager.Encoder.CompoundExpressionFor(ExpressionType.Bool, ExpressionOperator.LessEqualThan, exp1, exp2)) as INumericalAbstractDomain<Variable, Expression>;
    }

    public override INumericalAbstractDomain<Variable, Expression> TestTrueLessThan(Expression exp1, Expression exp2)
    {
      return TestTrue(this.ExpressionManager.Encoder.CompoundExpressionFor(ExpressionType.Bool, ExpressionOperator.LessThan, exp1, exp2)) as INumericalAbstractDomain<Variable, Expression>;
    }

    public override INumericalAbstractDomain<Variable, Expression> TestTrueEqual(Expression exp1, Expression exp2)
    {
      return TestTrue(this.ExpressionManager.Encoder.CompoundExpressionFor(ExpressionType.Bool, ExpressionOperator.Equal, exp1, exp2)) as INumericalAbstractDomain<Variable, Expression>;
    }

    public INumericalAbstractDomain<Variable, Expression> TestTrueEqual(Variable v1, Variable v2)
    {
      var v1Exp = this.ExpressionManager.Encoder.VariableFor(v1);
      var v2Exp = this.ExpressionManager.Encoder.VariableFor(v2);

      return this.TestTrueEqual(v1Exp, v2Exp);
    }
    #endregion

    #region Abstract Domain Operations

    #region !!!!Debugging ONLY!!!!

#if DEBUG
    private bool TrySearchVariable(string varstr, ref Variable v)
    {
        foreach (var x in this.Variables)
        {
          if (x.ToString().Contains(varstr))
          {
            v = x;
            return true;
          }
        }

        return false;
    }

    public bool DebugCheckSv18LessThanSv21(string where)
    {
      var b = this.DebugCheckLessEq("sv18", "sv21", where);
/*      if (!b)
      {       
        return this.DebugCheckLessEq("sv19", "sv11", where);
      }
      else*/
      {
        return b;
      }
    }

    public bool DebugCheckLessEq(string left, string right, string where)
    {
    // We want to be sure it does not go in the release bits
#if DEBUG
      Variable l,r;
      l = r = default(Variable);
      if (TrySearchVariable(left, ref l) && TrySearchVariable(right, ref r))
      {
        return DebugCheckLessEq(l, r, where);
      }
#endif
      return false;
    }

    public bool DebugCheckLessEq(Variable l, Variable r, string where)
    {
      var isTrue = this.CheckIfLessThan(this.ExpressionManager.Encoder.VariableFor(l), this.ExpressionManager.Encoder.VariableFor(r));

      //this.ExpressionManager.Log("{3}: {0} < {1} is {2}", () =>left, ()=>right, ()=>isTrue.ToString(), () =>where); 

      Console.WriteLine("{3}: {0} < {1} is {2}", l.ToString(), r.ToString(), isTrue.ToString(), where);
      if (!isTrue.IsTrue())
      {
        isTrue = this.CheckIfLessEqualThan(this.ExpressionManager.Encoder.VariableFor(l), this.ExpressionManager.Encoder.VariableFor(r));

        Console.WriteLine("{3}: {0} <= {1} is {2}", l.ToString(), r.ToString(), isTrue.ToString(), where);
      }

      return isTrue.IsTrue();
    }

#endif

    #endregion

#if TRACEJOIN
    private static int joincount = 0;
#endif

    public override IAbstractDomain Join(IAbstractDomain a)
    {
      return PerformanceMeasure.Measure(PerformanceMeasure.ActionTags.SubPolyJoin, () => JoinInternal(a), true);
    }

    private IAbstractDomain JoinInternal(IAbstractDomain a)
    {
      var watch = new Stopwatch();
      watch.Start();

#if TRACEJOIN
      Console.WriteLine("join #{0}", joincount++);
#endif

      IAbstractDomain result;
      SubPolyhedra<Variable, Expression> resultAsSubPolyhedra;
      SubPolyhedra<Variable, Expression> other = a as SubPolyhedra<Variable, Expression>;

      Contract.Assert(other != null);

#if TRACEJOIN
      this.DebugCheckLessEq("sv18", "sv9", "this");
      other.DebugCheckLessEq("sv18", "sv9", "other");

      this.DebugCheckLessEq("sv21", "sv9", "this");
      other.DebugCheckLessEq("sv21", "sv9", "other");

      this.DebugCheckLessEq("sv26", "sv9", "this");
      other.DebugCheckLessEq("sv26", "sv9", "other");
#endif

      PrintStatisticsForJoin(this);
      PrintStatisticsForJoin(other);

      SubPolyhedra<Variable, Expression> simpleResult;
      if (this.TrySimpleJoin(other, out simpleResult))
      {
        return simpleResult;
      }

      List<Polynomial<Variable, Expression>> deletedLeft;
      List<Polynomial<Variable, Expression>> deletedRight;
      SubPolyhedra<Variable, Expression> left;
      SubPolyhedra<Variable, Expression> right;

      NormalizeOperands(other, out deletedLeft, out deletedRight, out left, out right);

      // Try the trivial join
      if (AbstractDomainsHelper.TryTrivialJoin(left, right, out result))
      {
        return result;
      }

      // Do the pairwise join
      resultAsSubPolyhedra = PairwiseJoin(other, ref deletedLeft, ref deletedRight, left, right);

      // Using hints      

      var newHints = ApplyHints(resultAsSubPolyhedra, other, left, right);

      newHints = DoTheCleanup(watch, resultAsSubPolyhedra, newHints);

#if TRACEJOIN
      resultAsSubPolyhedra.DebugCheckLessEq("sv18", "sv9", "result");
      resultAsSubPolyhedra.DebugCheckLessEq("sv21", "sv9", "this");
      resultAsSubPolyhedra.DebugCheckLessEq("sv26", "sv9", "this");
#endif

      if (resultAsSubPolyhedra.hints.Count > SubPolyhedra.MaxHintsToRetainInWidening)
      {
        resultAsSubPolyhedra.hints = new Hints<Variable, Expression>();
        resultAsSubPolyhedra.WideningOnHints = true;
      }

      resultAsSubPolyhedra.PrintStatistics("@ join point");

      return resultAsSubPolyhedra;
    }
   
    #region Join helpers
    private bool TrySimpleJoin(SubPolyhedra<Variable, Expression> other, out SubPolyhedra<Variable, Expression> result)
    {
      if (Object.ReferenceEquals(this, other))
      {
        result = this;
        return true;
      }

      this.Left.RemoveUnusedVariables();
      other.Left.RemoveUnusedVariables();

      Contract.Assert(other != null);

      if (AbstractDomainsHelper.TryTrivialJoin(this, other, out result))
      {
        return true;
      }

      return false;
    }

    private void NormalizeOperands(SubPolyhedra<Variable, Expression> other, out List<Polynomial<Variable, Expression>> deletedLeft, out List<Polynomial<Variable, Expression>> deletedRight, out SubPolyhedra<Variable, Expression> left, out SubPolyhedra<Variable, Expression> right)
    {
      deletedLeft = new List<Polynomial<Variable, Expression>>();
      deletedRight = new List<Polynomial<Variable, Expression>>();

      // simplify
      this.CleanUpBetaVariables();
      other.CleanUpBetaVariables();

      // Transfer the definitions for the slack variables : Step 1

      this.HarmonizeSlackVariables(this, other);
      this.HarmonizeSlackVariables(other, this);

      // Do the reduction (infer the tightest bounds) 
      
      left = ReduceAndPutConstants(this.Left, this.Right);
      right = ReduceAndPutConstants(other.Left, other.Right);

      //MyParallel.Invoke(leftAct, rightAct);

      this.ExpressionManager.Log("** Left state after harmonization and reduction :\n{0}", left.ToString);
      this.ExpressionManager.Log("** Right state after harmonization and reduction :\n{0}", right.ToString);

      // Reduction may have removed some variables for efficiency, we need to put them back
      ReintroduceDroppedVariables(left, right, this, other);
    }

    private SubPolyhedra<Variable, Expression> PairwiseJoin(SubPolyhedra<Variable, Expression> other, ref List<Polynomial<Variable, Expression>> deletedLeft, ref List<Polynomial<Variable, Expression>> deletedRight, SubPolyhedra<Variable, Expression> left, SubPolyhedra<Variable, Expression> right)
    {
      SubPolyhedra<Variable, Expression> resultAsSubPolyhedra;
      this.ExpressionManager.Log("** Karr Join Left\n{0}", left.Left.ToString);
      this.ExpressionManager.Log("** Karr Join Right\n{0}", right.Left.ToString);

      var leftKarr = left.Left.DuplicateMe();
      var rightKarr = right.Left.DuplicateMe();
      var resultLeft = leftKarr.Join(rightKarr) as LinearEqualitiesForSubpolyhedraEnvironment<Variable, Expression>;

      this.ExpressionManager.Log("** Karr Join Result\n{0}", resultLeft.ToString);

      var resultRight = left.Right.Join(right.Right) as IntervalEnvironment<Variable, Expression>;
      resultAsSubPolyhedra = Factory(resultLeft, resultRight) as SubPolyhedra<Variable, Expression>;

      this.ExpressionManager.Log("** Temporay Result after pairwise join: \n{0}", resultAsSubPolyhedra.ToString);

      // Set the dependencies for the result
      SetDependencies(resultAsSubPolyhedra, this.betaDep);
      SetDependencies(resultAsSubPolyhedra, other.betaDep);

      // Do some clean up (mainly for performances)
      resultAsSubPolyhedra.CleanUpBetaVariables();

      // here we want to get any equality between program variables that may have been lost
      var simplifiedLeft = left.Left.DuplicateMe() as LinearEqualitiesForSubpolyhedraEnvironment<Variable, Expression>;
      var simplifiedRight = right.Left.DuplicateMe() as LinearEqualitiesForSubpolyhedraEnvironment<Variable, Expression>;

      simplifiedLeft.ProjectSlackVariables();
      simplifiedRight.ProjectSlackVariables();

      simplifiedLeft.Join(simplifiedRight, ref deletedLeft, ref deletedRight);

      // Try to recover relations using dropped equalities
      ReintroduceDroppedConstraints(resultAsSubPolyhedra, right, deletedLeft);
      ReintroduceDroppedConstraints(resultAsSubPolyhedra, left, deletedRight);

      this.ExpressionManager.Log("** Temporay Result after reintroducing dropped equalities: \n{0}", resultAsSubPolyhedra.ToString);
      return resultAsSubPolyhedra;
    }

    private Dictionary<Variable, Polynomial<Variable, Expression>> ApplyHints(SubPolyhedra<Variable, Expression> resultAsSubPolyhedra, SubPolyhedra<Variable, Expression> other, SubPolyhedra<Variable, Expression> left, SubPolyhedra<Variable, Expression> right)
    {
      this.ExpressionManager.Log("** Left state before using hints \n{0}", left.ToString);
      this.ExpressionManager.Log("** Right state before using hints \n{0}", right.ToString);

      var betaList = new Dictionary<Variable, Polynomial<Variable, Expression>>();
      var intvResult = new Dictionary<Variable, Interval>();

      var allHints = new Hints<Variable, Expression>(this.hints);
      allHints.Add(other.hints);

      HintsFromBetaDeps(allHints, left.betaDep);
      HintsFromBetaDeps(allHints, right.betaDep);

      // we add the same slack variable to all polynomials, so renaming will be necessary
      var beta = this.ExpressionManager.Encoder.FreshVariable<int>();

      // Infer the semantic hints
      var semanticHints = left.Right.ConvexHullHelper(right.Right, beta, SubPolyhedra.Inference);

      this.ExpressionManager.Log("**Computed hints \n{0}", semanticHints.ToString);

      // Begin

#if false
      // F: This code does not quite work for some reasons that are unclear to me
      var octagonsHints = InferOctagonHints(left, right, semanticHints, beta);

      foreach (var triple in octagonsHints)
      {
        resultAsSubPolyhedra.Left.AddConstraint(triple.Two);
        resultAsSubPolyhedra.Right.AssumeInInterval(triple.One, triple.Three);

        Console.WriteLine("Inferred {0} {1}", triple.Two, triple.Three);
      }
      Console.WriteLine("----");
#else
      allHints.Add(semanticHints);
#endif
      // End


      CleanHints(ref allHints, this.ExpressionManager);

      this.ExpressionManager.Log("**All hints \n{0}", Print(allHints));

      // build list of polynomials to check
      foreach (var pol in allHints.Enumerate())
      {
        var k = IsBetaDefinition(pol, out beta, this.ExpressionManager.Decoder);
        if (k != 1)
        {
          continue;
        }

        var betaTwo = this.ExpressionManager.Encoder.FreshVariable<int>();
        var polBeta = pol.Rename(false, beta, betaTwo);
        betaList[betaTwo] = polBeta;
      }

      // simplify list
      CleanDeps(ref betaList, this.ExpressionManager);

      var clonedLeft = left.DuplicateMe();

      // get the ranges for the slack variables proved by the left element
      foreach (var pair in betaList)
      {
        clonedLeft.Left.AddConstraint(clonedLeft.Left.AsVector(pair.Value));
      }

      this.ExpressionManager.Log("** Left state before reduction\n{0}", clonedLeft.ToString);

      clonedLeft = clonedLeft.ReduceInternal(clonedLeft.Left, clonedLeft.Right, false);

      this.ExpressionManager.Log("** Left state after reduction\n{0}", clonedLeft.ToString);

      foreach (var alpha in betaList.Keys)
      {
        var bounds = clonedLeft.Right.BoundsFor(alpha);
        if (bounds.IsNormal)
        {
          intvResult[alpha] = bounds.AsInterval;
        }
      }

      // get the ranges for the slack variables proved by the right element and join them with the previous intervals
      var clonedRight = right.DuplicateMe();
      foreach (var alpha_pair in betaList)
      {
        clonedRight.Left.AddConstraint(clonedRight.Left.AsVector(alpha_pair.Value));
      }

      this.ExpressionManager.Log("Right state before reduction\n{0}", clonedRight.ToString);

      clonedRight = clonedRight.ReduceInternal(clonedRight.Left, clonedRight.Right, false);

      this.ExpressionManager.Log("Rigth state after reduction\n{0}", clonedRight.ToString);

      foreach (var alpha in betaList.Keys)
      {
        Interval bounds;
        if (intvResult.TryGetValue(alpha, out bounds))
        {
          intvResult[alpha] = bounds.Join(clonedRight.Right.BoundsFor(alpha).AsInterval);
        }
      }

      // keep non-top results only
      foreach (var pair in intvResult)
      {
        if (!pair.Value.IsTop)
        {
          this.ExpressionManager.Log("Adding hint {0} == {1}", betaList[pair.Key].ToString, pair.Value.ToString);

          //Console.WriteLine("Inferred* {0} {1}", betaList[pair.Key], pair.Value);

          var linearEq = betaList[pair.Key];
          resultAsSubPolyhedra.Left.AddConstraint(resultAsSubPolyhedra.Left.AsVector(linearEq));
          resultAsSubPolyhedra.Right[pair.Key] = pair.Value;
          resultAsSubPolyhedra.betaDep[pair.Key] = linearEq;
        }
      }

      var newHints = new Dictionary<Variable, Polynomial<Variable, Expression>>();

#if false
      foreach (var triple in octagonsHints)
      {
        newHints[triple.One] = triple.Two;
        }
#endif

      foreach (var pair in this.betaDep)
      {
        if (!resultAsSubPolyhedra.betaDep.ContainsKey(pair.Key))
        {
          newHints.Add(pair.Key, pair.Value);
        }
      }

      foreach (var pair in other.betaDep)
      {
        if (!resultAsSubPolyhedra.betaDep.ContainsKey(pair.Key) && !newHints.ContainsKey(pair.Key))
        {
          newHints.Add(pair.Key, pair.Value);
        }
      }
      return newHints;
    }

    private Dictionary<Variable, Polynomial<Variable, Expression>> DoTheCleanup(Stopwatch watch, SubPolyhedra<Variable, Expression> resultAsSubPolyhedra, Dictionary<Variable, Polynomial<Variable, Expression>> newHints)
    {
      CleanDeps(ref newHints, this.ExpressionManager);

      resultAsSubPolyhedra.hints = new Hints<Variable, Expression>(newHints.Values);

      resultAsSubPolyhedra.Left.PutIntoRowEchelonForm();

      resultAsSubPolyhedra.CleanUpBetaVariables();

      CleanDeps(ref resultAsSubPolyhedra.betaDep, this.ExpressionManager);

      CleanHints(ref resultAsSubPolyhedra.hints, this.ExpressionManager);

      this.ExpressionManager.Log("Elapsed: {0}", watch.Elapsed.ToString);

      PrintStatisticsForJoin(resultAsSubPolyhedra);

      resultAsSubPolyhedra.Left.RemoveUnusedVariables();

      resultAsSubPolyhedra.Left.RemoveRedundantSlackVariables();

      this.ExpressionManager.Log("Join result: {0}", resultAsSubPolyhedra.ToString);
      
      return newHints;
    }
    #endregion

    private List<STuple<Variable, Polynomial<Variable, Expression>, DisInterval>> InferOctagonHints(
      SubPolyhedra<Variable, Expression> left, SubPolyhedra<Variable, Expression> right,
      Set<Polynomial<Variable, Expression>> semanticHints, Variable beta)
    {
      var leftCloned = left.DuplicateMe();
      var rightCloned = right.DuplicateMe();

      var myBetaList = new Dictionary<Variable, Polynomial<Variable, Expression>>();
      foreach (var pol in semanticHints)
      {
        var k = IsBetaDefinition(pol, out beta, this.ExpressionManager.Decoder);
        if (k != 1)
        {
          continue;
        }

        var betaTwo = this.ExpressionManager.Encoder.FreshVariable<int>();
        var polBeta = pol.Rename(false, beta, betaTwo);
        myBetaList[betaTwo] = polBeta;

        leftCloned.Left.AddConstraint(leftCloned.Left.AsVector(polBeta));
        rightCloned.Left.AddConstraint(rightCloned.Left.AsVector(polBeta));
      }

      leftCloned = leftCloned.ReduceInternal(leftCloned.Left, leftCloned.Right, true);
      rightCloned = rightCloned.ReduceInternal(rightCloned.Left, rightCloned.Right, true);

      var result = new List<STuple<Variable, Polynomial<Variable, Expression>, DisInterval>>();
      foreach (var pair in myBetaList)
      {
        var intvLeft = leftCloned.BoundsFor(pair.Key);
        var intvRight = rightCloned.BoundsFor(pair.Key);
        var join = intvLeft.Join(intvRight);

        if (join.IsNormal)
        {
          var newConstraint = new STuple<Variable, Polynomial<Variable, Expression>, DisInterval>(pair.Key, pair.Value, join);
          result.Add(newConstraint);
        }
      }

      return result;
    }

    /// <summary>
    /// Works with side effects in place
    /// </summary>
    private void HarmonizeSlackVariables(SubPolyhedra<Variable, Expression> from, SubPolyhedra<Variable, Expression> to)
    {
      var vars = new List<Variable>(from.betaDep.Keys);

      var cached_to_Variables = to.Variables.ToSet();
      foreach (var alpha in vars)
      {
        if (cached_to_Variables.Contains(alpha))
        {
          // Most likely, the definitions are not the same so we need to rename
          var newName = this.ExpressionManager.Encoder.FreshVariable<int>();
          from.RenameVariable(alpha, newName);
          to.Left.AddConstraint(to.Left.AsVector(from.betaDep[newName]));
        }
        else
        {
          to.Left.AddConstraint(to.Left.AsVector(from.betaDep[alpha]));
        }
      }
    }

    private SubPolyhedra<Variable, Expression> ReduceAndPutConstants(LinearEqualitiesForSubpolyhedraEnvironment<Variable, Expression> lineq, IntervalEnvironment<Variable, Expression> intv)
    {
      var res = ReduceInternal(lineq, intv, false);
      res.PutConstantsInKarr(); // the join in Karr is more precise when constants are present

      return res;
    }

    /// <summary>
    /// Works with in place updates
    /// </summary>
    static private void ReintroduceDroppedVariables(SubPolyhedra<Variable, Expression> left, SubPolyhedra<Variable, Expression> right, SubPolyhedra<Variable, Expression> originalLeft, SubPolyhedra<Variable, Expression> originalRight)
    {
      var vars = new List<Variable>(originalLeft.betaDep.Keys);
      foreach (var alpha in vars)
      {
        if (left.Left.Variables.Contains(alpha))
        {
          // ok, reduction kept it
        }
        else
        {
          left.Left.AddConstraint(left.Left.AsVector(originalLeft.betaDep[alpha]));
        }

        if (right.Left.Variables.Contains(alpha))
        {
          // ok, reduction kept it
        }
        else
        {
          right.Left.AddConstraint(right.Left.AsVector(originalLeft.betaDep[alpha]));
        }
      }

      vars = new List<Variable>(originalRight.betaDep.Keys);
      foreach (var alpha in vars)
      {
        if (left.Left.Variables.Contains(alpha))
        {
          // ok, reduction kept it
        }
        else
        {
          left.Left.AddConstraint(left.Left.AsVector(originalRight.betaDep[alpha]));
        }

        if (right.Left.Variables.Contains(alpha))
        {
          // ok, reduction kept it
        }
        else
        {
          right.Left.AddConstraint(right.Left.AsVector(originalRight.betaDep[alpha]));
        }
      }
    }

    /// <summary>
    /// Works with in place updates
    /// </summary>
    static private void SetDependencies(SubPolyhedra<Variable, Expression> result, Dictionary<Variable, Polynomial<Variable, Expression>> betaDep)
    {
      foreach (var alpha_pair in betaDep)
      {
        if (result.Variables.Contains(alpha_pair.Key))
        {
          result.betaDep[alpha_pair.Key] = alpha_pair.Value;
        }
      }
    }

    /// <summary>
    /// Works with in place updates to result
    /// </summary>
    private void ReintroduceDroppedConstraints(
      SubPolyhedra<Variable, Expression> result, SubPolyhedra<Variable, Expression> original, List<Polynomial<Variable, Expression>> dropped)
    {
      var betaList = new Dictionary<Variable, Polynomial<Variable, Expression>>();
      var intvResult = new Dictionary<Variable, DisInterval>();
      var cloned = original.DuplicateMe();

      foreach (var pol in dropped)
      {
        if (pol.Variables.IsSingleton)
        {// should be handled by intervals
          continue;
        }

        Polynomial<Variable, Expression> polBeta;
        Variable beta, betaTwo;

        int k = IsBetaDefinition(pol, out betaTwo, this.ExpressionManager.Decoder);

        if (k == 0 && pol.Variables.Count > 1)
        { // equality between program variables that doesn't hold in right : 
          // trying to get some bounds for the corresponding linear functional
          beta = this.ExpressionManager.Encoder.FreshVariable<int>();
          polBeta = pol.AddMonomialToTheLeft(new Monomial<Variable>(-1, beta));
          cloned.Left.AddConstraint(cloned.Left.AsVector(polBeta));
          betaList[beta] = polBeta;
          intvResult[beta] = DisInterval.For(0);
        }
        else if (k == 1 && pol.Variables.Count > 2)
        { // linear functional bounded on the left : 
          // as it may not correspond to the actual definition of the slack variable, we will try to bound it also on the right
          beta = this.ExpressionManager.Encoder.FreshVariable<int>();
          polBeta = pol.Rename(false, betaTwo, beta);
          cloned.Left.AddConstraint(cloned.Left.AsVector(polBeta));
          betaList[beta] = polBeta;
          intvResult[beta] = original.BoundsFor(betaTwo);
        }
        else
        {
          // in all the other cases, we don't try to get any information,
          // either because it should be kept by intervals or because it involves several slack variables
        }
      }

      cloned = cloned.ReduceInternal(cloned.Left, cloned.Right, false);

      foreach (var alpha_pair in betaList)
      {
        if (!cloned.Right.BoundsFor(alpha_pair.Key).IsTop)
        {
          result.Left.AddConstraint(result.Left.AsVector(alpha_pair.Value));
          var disinterval = cloned.Right.BoundsFor(alpha_pair.Key).Join(intvResult[alpha_pair.Key]);
          result.Right[alpha_pair.Key] = disinterval.AsInterval;
          result.betaDep[alpha_pair.Key] = alpha_pair.Value;
        }
      }
    }

    private void HintsFromBetaDeps(Hints<Variable, Expression> result, Dictionary<Variable, Polynomial<Variable, Expression>> betaDeps)
    {
      foreach (var e_pair in betaDeps)
      {
        Variable beta;
        if (IsBetaDefinition(e_pair.Value, out beta, this.ExpressionManager.Decoder) == 1)
        {
          result.Add(e_pair.Value);
        }
      }
    }
    

    [Conditional("TRACE_JOIN")]
    private static void PrintStatisticsForJoin(SubPolyhedra<Variable, Expression> sp)
    {
    }

    public override bool LessEqual(IAbstractDomain a)
    { // here we assume that we are called after a widening, so the slack variables in this are renamings of slack variables in a
      if (this.IsBottom)
        return true;

      if (a.IsTop)
        return true;

      var other = a as SubPolyhedra<Variable, Expression>;

#if false
      // First, direct try -- it breaks regression test. Probably of some aliasing?
      if (this.Left.LessEqual(other.Left) && this.Right.LessEqual(other.Right))
      {
        return true;
      }
#endif


      Contract.Assert(other != null);

      #region Quick Checks

      var decoder = this.ExpressionManager.Decoder;
      var intvRight = this.Right;
      var intvLeft = other.Right;

      foreach (var var in other.Right.Variables)
      {
        if (!decoder.IsSlackVariable(var) && !intvRight.BoundsFor(var).LessEqual(intvLeft.BoundsFor(var)))
        {
          return false;
        }
      }

      #endregion

      #region Implementation using the fact that slack variables in the two states are related
      var cloned = this.DuplicateMe();
      foreach (var pair in this.betaDep)
      {
        var e = pair.Key;
        var def = pair.Value;

        Variable equivalent;
        Rational coeff, k;
        if (other.TryGetEquivalent(e, def, out equivalent, out coeff, out k))
        {
          // Avoid trivial equalities
          if (e.Equals(equivalent))
          {
            continue;
          }

          if (coeff == 1 && k.IsZero )
          {
            if (cloned.Variables.Contains(equivalent))
            {
              // If the equivalent is in cloned, then they should have the same value
              // So we assume e == cloned
              cloned = cloned.TestTrueEqual(equivalent, e) as SubPolyhedra<Variable, Expression>;
              if (cloned.IsBottom)
              {
                return false;
              }
            }
            else
            {
              cloned.RenameVariable(e, equivalent);
            }
          }
          else
          {
            var monos = new Monomial<Variable> []
            {
              new Monomial<Variable>(e),
              new Monomial<Variable>(-coeff, equivalent),
              new Monomial<Variable>(-k) 
            };
            
            Polynomial<Variable, Expression> pol;
            Polynomial<Variable, Expression>.TryToPolynomialForm(monos, out pol);

            var karr = cloned.Left;
            var intervals = cloned.Right;

            karr.AddConstraint(karr.AsVector(pol));

            var diff = (intervals.BoundsFor(e) - DisInterval.For(k));
            intervals[equivalent] =  diff.AsInterval / coeff;
            
            cloned.RemoveVariable(e);
          }
        }
      }

      var lreduced = cloned.ReduceInternal(cloned.Left, cloned.Right, false);
      var rreduced = other.ReduceInternal(other.Left, other.Right, false);
      #endregion

      if (lreduced.Left.LessEqual(rreduced.Left) && lreduced.Right.LessEqual(rreduced.Right))
      {
        return this.WideningOnHints == other.WideningOnHints;
      }

      return false;
    }

    public override IAbstractDomain Widening(IAbstractDomain prev)
    {
      var other = prev as SubPolyhedra<Variable, Expression>;
      Contract.Assert(other != null);

      this.CleanUpBetaVariables(true);

      #region Transfer the definitions for the slack variables
      // from other to this ; not the other way since we are widening
      var vars = new List<Variable>(other.betaDep.Keys);
      foreach (var alpha in vars)
      {
        if (this.Variables.Contains(alpha))
        {
          Variable newName = this.ExpressionManager.Encoder.FreshVariable<int>();
          other.RenameVariable(alpha, newName);
          this.Left.AddConstraint(this.Left.AsVector(other.betaDep[newName]));
        }
        else
          this.Left.AddConstraint(this.Left.AsVector(other.betaDep[alpha]));
      }
      #endregion

      var newState = this.Reduce(this.Left, this.Right) as SubPolyhedra<Variable, Expression>;

      foreach (var alpha in newState.Variables.SetDifference(newState.Left.Variables))
      { // some slack variables may have been removed from Karr to speed up reduction, we need to put them back
        if (this.betaDep.ContainsKey(alpha))
        {
          newState.Left.AddConstraint(newState.Left.AsVector(this.betaDep[alpha]));
        }
        else if (other.betaDep.ContainsKey(alpha))
        {
          newState.Left.AddConstraint(newState.Left.AsVector(other.betaDep[alpha]));
        }
      }

      var deletedLeft = new List<Polynomial<Variable, Expression>>();
      var deletedRight = new List<Polynomial<Variable, Expression>>();

      var cloned = newState.DuplicateMe(); // We need to clone because the Join in Karr modifies the state : UniformEnvironments removes variables, including useful slack variables

      var joinedLeft = cloned.Left.Join(other.Left, ref deletedLeft, ref deletedRight) as LinearEqualitiesForSubpolyhedraEnvironment<Variable, Expression>;
      var joinedRight = newState.Right.Widening(other.Right);

      cloned = newState.DuplicateMe();

      var betaList = new Dictionary<Variable, Polynomial<Variable, Expression>>();

      Variable beta;
      Variable existing;
      int k;
      
      foreach (var pol in deletedRight) // only try to put back the equalities dropped from the previous state
      {
        k = IsBetaDefinition(pol, out existing, this.ExpressionManager.Decoder);
        if (k == 0 && pol.Variables.Count > 1)
        { // Equality between program variables, adding a slack variable
          beta = this.ExpressionManager.Encoder.FreshVariable<int>();
          Polynomial<Variable, Expression> polBeta = pol.AddMonomialToTheLeft(new Monomial<Variable>(-1, beta));
          cloned.Left.AddConstraint(cloned.Left.AsVector(polBeta));
          betaList[beta] = polBeta;
          other.Right[beta] = Interval.For(0);
        }
        else if (k == 1)
        { // Definition of a slack variable, finding a bound for the existing slack variable, and renaming it to avoid any possible conflict
          beta = this.ExpressionManager.Encoder.FreshVariable<int>();
          if (cloned.Variables.Contains(existing))
          { // We must avoid conflicts when the definition is not the same
            Polynomial<Variable, Expression> polTwo = pol.Rename(existing, beta);
            cloned.Left.AddConstraint(cloned.Left.AsVector(polTwo));
            betaList[beta] = polTwo;
            other.Right[beta] = other.Right.BoundsFor(existing).AsInterval;
          }
          else
          {
            cloned.Left.AddConstraint(cloned.Left.AsVector(pol));
            betaList[existing] = pol;
          }
        }
      }

      var nDeps = new Dictionary<Variable, Polynomial<Variable, Expression>>();
      if (betaList.Count > 0)
      {
        cloned = cloned.ReduceInternal(cloned.Left, cloned.Right, false);

        foreach (var alpha in betaList)
        {
          if (!cloned.Right.BoundsFor(alpha.Key).IsTop)
          {
            joinedLeft.AddConstraint(joinedLeft.AsVector(alpha.Value));
 
            var disInterval = cloned.Right.BoundsFor(alpha.Key).Widening(other.Right.BoundsFor(alpha.Key));
            joinedRight[alpha.Key] = disInterval.AsInterval;
            nDeps[alpha.Key] = alpha.Value;
          }
        }
      }
      var result = Factory(joinedLeft, joinedRight) as SubPolyhedra<Variable, Expression>;
      foreach (var e in other.betaDep)
      {
        if (result.Variables.Contains(e.Key))
        {
          result.betaDep[e.Key] = e.Value;
        }
      }
      foreach (var e in nDeps)
      {
        result.betaDep.Add(e.Key, e.Value);
      }

      #region Using hints
      betaList = new Dictionary<Variable, Polynomial<Variable, Expression>>();
      var intvResult = new Dictionary<Variable, Interval>();
      cloned = newState.DuplicateMe();
      
      var allHints = other.hints;

      if (SubPolyhedra.Algorithm != SubPolyhedra.ReductionAlgorithm.Fast && other.numberOfWidenings <= 5) // to enforce termination
      {
        allHints.Add(this.hints);
        foreach (var alpha in this.betaDep)
        {
          allHints.Add(alpha.Value);
        }
      }
      var resultVars = result.Variables;
      foreach (var e in other.betaDep)
      {
        if (!resultVars.Contains(e.Key))
        {
          allHints.Add(e.Value);
        }
      }

      CleanHints(ref allHints, this.ExpressionManager);

      foreach (var pol in allHints.Enumerate())
      {
        k = IsBetaDefinition(pol, out existing, this.ExpressionManager.Decoder);

        if (k != 1 || pol.Variables.Count <= 2)
        {
          continue;
        }

        beta = this.ExpressionManager.Encoder.FreshVariable<int>();
        
        var polBeta = pol.Rename(false, existing, beta);
        cloned.Left.AddConstraint(cloned.Left.AsVector(polBeta));
        betaList[beta] = polBeta;
      }

      cloned = cloned.ReduceInternal(cloned.Left, cloned.Right, false);
      foreach (var alpha in betaList.Keys)
      {
        intvResult[alpha] = cloned.Right.BoundsFor(alpha).AsInterval;
      }

      cloned = other.DuplicateMe();

      foreach (var alpha in betaList)
      {
        cloned.Left.AddConstraint(cloned.Left.AsVector(alpha.Value));
      }

      cloned = cloned.ReduceInternal(cloned.Left, cloned.Right, false);
      foreach (var alpha in betaList.Keys)
      {
        intvResult[alpha] = intvResult[alpha].Widening(cloned.Right.BoundsFor(alpha).AsInterval);
      }

      foreach (var alpha in intvResult)
      {
        if (!alpha.Value.IsTop)
        {
          result.Left.AddConstraint(result.Left.AsVector(betaList[alpha.Key]));
          result.Right[alpha.Key] = alpha.Value;
          result.betaDep[alpha.Key] = betaList[alpha.Key];
        }
        else
        {
          if (other.hints.Contains(betaList[alpha.Key]))
            result.hints.Add(betaList[alpha.Key]);
        }
      }
      #endregion

      // Apply widening to the hints: if we have too many, project some to save time
      result.WideningOnHints = CleanHints(ref result.hints, this.ExpressionManager, SubPolyhedra.MaxHintsToRetainInWidening);

      result.Left.PutIntoRowEchelonForm();

      result.CleanUpBetaVariables(true);
      other.CleanUpBetaVariables(true);

      List<Variable> removed;
      CleanDeps(result.betaDep, this.ExpressionManager, out removed);

      foreach (var e in removed)
      {
        result.RemoveVariableWithoutHints(e);
      }

      foreach (var pol in new Set<Polynomial<Variable, Expression>>(result.hints.Enumerate())) // create a new collection since we will modify the hints
      {
        if (!other.hints.Contains(pol))
        {
          result.hints.Remove(pol);
        }
      }

      result.numberOfWidenings = other.numberOfWidenings + 1;

      if (result.hints.Count > SubPolyhedra.MaxHintsToRetainInWidening)
      {
        result.hints = new Hints<Variable, Expression>();
        result.WideningOnHints = true;
      }

      return result;
    }

    public override bool IsBottom
    {
      get
      {
        return base.IsBottom;
      }
    }

    public bool IsBottomWithReduction()
    {
      var dup = this.DuplicateMe();
      var reduced = this.ReduceInternal(dup.Left, dup.Right, true);

      return reduced.IsBottom;
    }
    #endregion

    #region Checks
    public override FlatAbstractDomain<bool> CheckIfHolds(Expression exp)
    {
      return this.checkIfHoldsVisitor.Visit(exp, this);
    }

    public override FlatAbstractDomain<bool> CheckIfGreaterEqualThanZero(Expression exp)
    {
      return this.checkIfHoldsVisitor.Visit(this.ExpressionManager.Encoder.CompoundExpressionFor(ExpressionType.Bool,
        ExpressionOperator.GreaterEqualThan, exp, this.ExpressionManager.Encoder.ConstantFor(0)), this);
    }

    public override FlatAbstractDomain<bool> CheckIfLessThan(Expression e1, Expression e2)
    {
      return this.checkIfHoldsVisitor.Visit(this.ExpressionManager.Encoder.CompoundExpressionFor(ExpressionType.Bool, ExpressionOperator.LessThan, e1, e2), this);
    }

    public override FlatAbstractDomain<bool> CheckIfLessThan_Un(Expression e1, Expression e2)
    {
      return this.CheckIfLessThan(e1, e2);
    }

    public override FlatAbstractDomain<bool> CheckIfLessEqualThan(Expression e1, Expression e2)
    {
      return this.checkIfHoldsVisitor.Visit(this.ExpressionManager.Encoder.CompoundExpressionFor(ExpressionType.Bool, ExpressionOperator.LessEqualThan, e1, e2), this);
    }

    public override FlatAbstractDomain<bool> CheckIfLessEqualThan_Un(Expression e1, Expression e2)
    {
      return this.CheckIfLessEqualThan(e1, e2);
    }
    #endregion

    #region Operations on variables
    public override void AssignInParallel(Dictionary<Variable, FList<Variable>> sourcesToTargets, Converter<Variable, Expression> convert)
    {
      this.ExpressionManager.Log("Subpoly before renaming:\n{0}", this.ToString);

      this.PrintStatistics("@ BEFORE - assign in parallel");

#if TRACEJOIN
      this.DebugCheckLessEq("sv27", "sv9", "before parallel assign");
#endif

      if (sourcesToTargets.IsIdentityMap())
      {
        return;
      }

      Left.AssignInParallel(sourcesToTargets, convert, ref betaDep, ref hints);
      Right.AssignInParallel(sourcesToTargets, convert);

#if TRACEJOIN
      this.DebugCheckLessEq("sv18", "sv9", "after parallel assign");
#endif
      this.ExpressionManager.Log("Subpoly after renaming:\n{0}", this.ToString);

      this.PrintStatistics("@ AFTER - assign in parallel");
    }

#if DEBUG 
    SubPolyhedra<Variable, Expression> cloned;
    
    private void MyScript(Dictionary<Variable, FList<Variable>> s, Converter<Variable, Expression> c)
    {
      this.cloned = this.DuplicateMe();
      var reduced = this.ReduceInternal(cloned.Left, cloned.Right, true);
      reduced.DebugCheckLessEq("sv27", "sv9", "before parallel assign");
      reduced.AssignInParallel(s, c);
      reduced.DebugCheckLessEq("sv18", "sv9", "after parallel assign");
    }
#endif

    public override void ProjectVariable(Variable var)
    {
      base.ProjectVariable(var);
      if (betaDep.ContainsKey(var))
      {
        hints.Add(betaDep[var]);
        betaDep.Remove(var);
      }
    }

    public override void RemoveVariable(Variable var)
    {
      base.RemoveVariable(var);
      if (betaDep.ContainsKey(var))
      {
        hints.Add(betaDep[var]);
        betaDep.Remove(var);
      }
    }

    private void RemoveVariableWithoutHints(Variable var)
    {
      base.RemoveVariable(var);
      if (betaDep.ContainsKey(var))
        betaDep.Remove(var);
    }

    public override void RenameVariable(Variable OldName, Variable NewName)
    {
      if (NewName.Equals(OldName)) return;
      base.RenameVariable(OldName, NewName);
      if (betaDep.ContainsKey(NewName))
        betaDep.Remove(NewName);
      var vars = new List<Variable>(betaDep.Keys);
      foreach (var beta in vars)
      {
        if (beta.Equals(OldName))
        {
          betaDep[NewName] = betaDep[beta].Rename(OldName, NewName);
          betaDep.Remove(OldName);
        }
        else
        {
          betaDep[beta] = betaDep[beta].Rename(OldName, NewName);
        }
      }
    }
    #endregion

    #region Implementation of Reduce and Factory

    private SubPolyhedra<Variable, Expression> ReduceInternal(
      LinearEqualitiesForSubpolyhedraEnvironment<Variable, Expression> left, IntervalEnvironment<Variable, Expression> right, 
      bool forceSimplex)
    {
      var reductionAlg = forceSimplex ? SubPolyhedra.ReductionAlgorithm.Simplex : SubPolyhedra.Algorithm;

      var result = Factory(left, right) as SubPolyhedra<Variable, Expression>;
      if (!result.IsNormal())
      {
        return result;
      }

      left.PutIntoRowEchelonForm();

      var redundancies = left.GetRedundancies();

      foreach (var redundancy in redundancies)
      {
        var e = redundancy.V1;
        var coeff = redundancy.Coeff2;
        var target = redundancy.V2;
        var cst = redundancy.Offset;

        if (coeff.IsNotZero)
        {
          right[target] = right.BoundsFor(target).AsInterval.Meet((cst - right.BoundsFor(e).AsInterval) / coeff);
        }
        else
        {
          if (!cst.IsPlusInfinity && Interval.For(cst).Meet(right.BoundsFor(e).AsInterval).IsBottom)
          {
            return this.Bottom as SubPolyhedra<Variable, Expression>;
          }
        }

        left.RemoveVariable(e);
        right.RemoveVariable(e);
      }

      #region Phase 1 : Constant Propagation from Intervals to Karr

      foreach (var pair in right.Elements)
      {
        var var = pair.Key;
        var value = pair.Value;

        if (value.IsSingleton && left.Variables.Contains(var))
        {
          result.Left.AddConstraintEqual(var, value.LowerBound);
        }
      }

      result.Left.PutIntoRowEchelonForm();

      if (result.IsBottom)
      {
        return result.Bottom as SubPolyhedra<Variable, Expression>;
      }
      #endregion

      #region Phase 2 : Bounds Inference in Karr
      result = Factory(result.Left, result.Left.BoundsInference(result.Right, reductionAlg, SubPolyhedra.MaxVariablesToRunSimplex)) as SubPolyhedra<Variable, Expression>;
      #endregion


      var intvEnv = result.Right;

      foreach (var alpha_pair in redundancies)
      {
        var coeff = alpha_pair.Coeff2;
        var cst = alpha_pair.Offset;

        if (coeff.IsZero)
        {
          if (!cst.IsInfinity)
          {
            var disInterval = DisInterval.For(cst).Meet(intvEnv.BoundsFor(alpha_pair.V1));
            intvEnv[alpha_pair.V1] = disInterval.AsInterval;
          }
        }
        else
        {
          intvEnv[alpha_pair.V1] = cst - intvEnv.BoundsFor(alpha_pair.V2).AsInterval * coeff;
        }
      }

      return Factory(result.Left, intvEnv) as SubPolyhedra<Variable, Expression>;
    }
    
    private SubPolyhedra<Variable, Expression> ReduceComplete()
    {
      if (SubPolyhedra.Algorithm == SubPolyhedra.ReductionAlgorithm.Fast)
      {
        SubPolyhedra.Algorithm = SubPolyhedra.ReductionAlgorithm.Complete;
        var result = this.ReduceInternal(this.Left, this.Right, false);
        SubPolyhedra.Algorithm = SubPolyhedra.ReductionAlgorithm.Fast;

        return result;
      }
      else
      {
        return this.ReduceInternal(this.Left, this.Right, false);
      }
    }

    protected override ReducedCartesianAbstractDomain<LinearEqualitiesForSubpolyhedraEnvironment<Variable, Expression>, IntervalEnvironment<Variable, Expression>> Factory(LinearEqualitiesForSubpolyhedraEnvironment<Variable, Expression> left, IntervalEnvironment<Variable, Expression> right)
    {
      return new SubPolyhedra<Variable, Expression>(left, right, this.ExpressionManager);
    }

    public override ReducedCartesianAbstractDomain<LinearEqualitiesForSubpolyhedraEnvironment<Variable, Expression>, IntervalEnvironment<Variable, Expression>>
  Reduce(LinearEqualitiesForSubpolyhedraEnvironment<Variable, Expression> left, IntervalEnvironment<Variable, Expression> right)
    {
      return this.ReduceInternal(left, right, false);
    }
    #endregion

    #region Helper methods
    /// <summary>
    /// Propagate the renaming to the dependencies map and return the variables that should be deleted
    /// </summary>
    /// <param name="oldExp">The expression to replace</param>
    /// <param name="newExp">The new expression</param>
    /// <returns>A set of variables who depend on previous values of <paramref name="newExp"/></returns>
    internal static Set<Variable> UpdateBetaDeps(Variable oldExp, Variable newExp, ref Dictionary<Variable, Polynomial<Variable, Expression>> betaDep)
    {
      var result = new Set<Variable>();
      var updated = new Dictionary<Variable, Polynomial<Variable, Expression>>(betaDep.Count);
      
      foreach (var e in betaDep.Keys)
      {
        var deps = betaDep[e];
        if (deps.Variables.Contains(newExp))
        {
          result.Add(e);
          updated[e] = betaDep[e]; // we rely on the caller to remove it
        }
        else
        {
          updated[e] = deps.Rename(oldExp, newExp);
        }
      }
      betaDep = updated;

      return result;
    }

    internal static void UpdateHints(Variable oldExp, Variable newExp, ref Hints<Variable, Expression> hints)
    {
      var result = new Hints<Variable, Expression>();
      foreach (var pol in hints.Enumerate())
      {
        if (!pol.Variables.Contains(newExp))
        {
          result.Add(pol.Rename(false, oldExp, newExp));
        }
      }
      hints = result;
    }

    /// <summary>
    /// Remove useless slack variables (either redundant ones or those that are only present in one side of the product)
    /// </summary>
    private bool CleanUpBetaVariables(bool IsWidening = false)
    {
      if (Left.IsBottom)
      {
        return true;
      }

      this.ExpressionManager.Log("SubPoly before cleaning up: {0}", this.ToString);

      var result = false;
      var zero = Rational.For(0);
      var decoder = this.ExpressionManager.Decoder;
      var toRemove = (this.Left as LinearEqualitiesForSubpolyhedraEnvironment<Variable, Expression>).GetRedundancies();

      // Propagate the value for the variable to be removed to its equivalent
      foreach (var redundancy in toRemove)
      {
        var k = redundancy.Coeff2;

        if (k.IsZero)
        {// e is a constant and doesn't have a target
          continue;
        }

        var target = redundancy.V2;
        var c = redundancy.Offset;

        // e + k * target == c or
        // target = (c - e) / k
        var forE = Right.BoundsFor(redundancy.V1).AsInterval;
        var forTarget = (c - forE) / k;
        Right[target] = forTarget.Meet(Right.BoundsFor(target).AsInterval);
      }

      var leftVars = this.Left.Variables;

      foreach (var e in this.Variables)
      {
        if (decoder.IsSlackVariable(e))
        {
          var boundsForE = Right.BoundsFor(e);
          if (!leftVars.Contains(e) || boundsForE.IsTop)
          {
            toRemove.Add(new LinearEqualityRedundancy<Variable>(e, zero, default(Variable), zero));
          }
          else if (boundsForE.IsSingleton)
          {
            toRemove.Add(new LinearEqualityRedundancy<Variable>(e, zero, default(Variable), zero));

            Left.AddConstraintEqual(e, boundsForE.LowerBound);
          }
        }
      }

      foreach (var redundancy in toRemove)
      {
        this.ExpressionManager.Log("Removing redundant variable: {0}", redundancy.V1.ToString);

        result = true;
        this.RemoveVariable(redundancy.V1);
      }

      if (!IsWidening)
      {
        this.ExpressionManager.Log("Hints before injecting beta-deps: {0}", this.hints.Count.ToString);

        // F: Here we add all the betaDeps to the hints! It seems that if we do not do it, then sometimes the precision depends on the variable ordering!!!
        foreach (var pair in this.betaDep)
        {
          this.hints.Add(pair.Value);
        }

        this.ExpressionManager.Log("Hints after: {0}", this.hints.Count.ToString);
      }

      this.ExpressionManager.Log("SubPoly after cleaning up: {0}", this.ToString);

      return result;
    }

#if false
      static int count = 0;
#endif

    /// <summary>
    /// Method used to remove redundant definitions from a dictionary mapping slack variables to their definitions
    /// </summary>
    /// <param name="deps">The dictionary mapping slack variables to their definition in terms of program variables</param>
    private static void CleanDeps(ref Dictionary<Variable, Polynomial<Variable, Expression>> deps, 
      ExpressionManagerWithEncoder<Variable, Expression> expManager)
    {
      Contract.Requires(expManager != null);

#if TRACECLEANDEPS      
      Console.WriteLine("== CleanDeps #{0}, Constraints #{1}", count, deps.Count);

      var originalCount = deps.Count;
            
      foreach (var pair in deps)
      {
        Console.WriteLine("{0} -> {1}", pair.Key, pair.Value);
      }
      var prev = new Dictionary<Variable, Polynomial<Variable, Expression>>(deps);      
#endif

      deps = CleanDepsSyntactical(deps);

#if TRACECLEANDEPS
       Console.WriteLine("==== After syntactic cleaning #{0} (were #{1})", deps.Count, originalCount);

       foreach (var pair in deps)
       {
         Console.WriteLine("{0} -> {1}", pair.Key, pair.Value);
       }
#endif

      if (deps.Count == 0)
      {
#if TRACECLEANDEPS
      Console.WriteLine("No deps -- nothing to do");
#endif

        return;
      }

      var temp = new LinearEqualitiesForSubpolyhedraEnvironment<Variable, Expression>(expManager);

      foreach (var pair in deps)
      {
        temp.AddConstraint(pair.Value);
      }     

      // Hot path!
      temp.PutIntoRowEchelonForm();
 
#if TRACECLEANDEPS
    Console.WriteLine("===== Removed Constraints");
#endif

      var toRemove = temp.GetRedundancies();
      foreach (var redundancy in toRemove)
      {

#if TRACECLEANDEPS
        Console.WriteLine("entry : {0}", redundancy.V1);
#endif
        deps.Remove(redundancy.V1);
      }

#if TRACECLEANDEPS
      Console.WriteLine("===== Cleaned Constraints #{0}", deps.Count);
     
      foreach (var e_pair in deps)
      {
        Console.WriteLine("{0} -> {1}", e_pair.Key, e_pair.Value);
      }

      Console.WriteLine("===============================================");
#endif
#if false
      count++;
#endif
    }

    private static Dictionary<Variable, Polynomial<Variable, Expression>> CleanDepsSyntactical(
      Dictionary<Variable, Polynomial<Variable, Expression>> deps)
    {
      var buffer2 = new Set<Pair<Pair<Rational, Variable>, Pair<Rational, Variable>>>();
      var buffer3 = new Set<Tuple<Pair<Rational, Variable>, Pair<Rational, Variable>, Pair<Rational, Variable>>>();
      var buffer4 = new Set<Tuple<Pair<Rational, Variable>, Pair<Rational, Variable>, Pair<Rational, Variable>, Pair<Rational, Variable>>>();

#if false
      var stringbuff = new Set<string>();
#endif
      var result = new Dictionary<Variable, Polynomial<Variable, Expression>>();

      foreach (var pair in deps)
      {
#if false // this way of removing redundances seems way slower
        var key = pair.Value.ToString(v => !v.Equals(pair.Key));

        if (stringbuff.Contains(key))
        {
          continue;
        }
        else
        {
          stringbuff.Add(key);
        }
#else
        List<Pair<Rational, Variable>> val;

        if (pair.Value.TryToList(out val))
        {

          val.Remove(new Pair<Rational, Variable>(Rational.For(-1), pair.Key)); // Vincent's representation has the slack variable in the polynomial 

          switch (val.Count)
          {
            case 2:
              {
                var key = new Pair<Pair<Rational, Variable>, Pair<Rational, Variable>>(val[0], val[1]);

                if (buffer2.Contains(key))
                {
                  continue;
                }

                buffer2.Add(key);
              }
              break;

            case 3:
              {
                var key = new Tuple<Pair<Rational, Variable>, Pair<Rational, Variable>, Pair<Rational, Variable>>(val[0], val[1], val[2]);
                if (buffer3.Contains(key))
                {
                  continue;
                }

                buffer3.Add(key);
              }
              break;

            case 4:
              {
                var key = new Tuple<Pair<Rational, Variable>, Pair<Rational, Variable>, Pair<Rational, Variable>, Pair<Rational, Variable>>(val[0], val[1], val[2], val[3]);
                if (buffer4.Contains(key))
                {
                  continue;
                }

                buffer4.Add(key);

              }
              break;
          }
        } 
#endif
        result[pair.Key] = pair.Value;
      }

      return result;
    }

    /// <summary>
    /// Method used to remove redundant definitions from a dictionary mapping slack variables to their definitions, returning redundant variables
    /// </summary>
    /// <param name="deps">The dictionary mapping slack variables to their definition in terms of program variables</param>
    private static void CleanDeps(Dictionary<Variable, Polynomial<Variable, Expression>> deps, 
      ExpressionManagerWithEncoder<Variable, Expression> expManager, out List<Variable> removed)
    {
      var temp = new LinearEqualitiesForSubpolyhedraEnvironment<Variable, Expression>(expManager);
      foreach (var pair in deps)
      {
        temp.AddConstraint(temp.AsVector(pair.Value));
      }
      temp.PutIntoRowEchelonForm();
      var toRemove = temp.GetRedundancies();

      removed = new List<Variable>();
      foreach (var e in toRemove)
      {
        deps.Remove(e.V1);
        removed.Add(e.V1);
      }
    }
    
    /// <returns>true iff the threshold has been reached</returns>
    private static bool CleanHints(ref Hints<Variable, Expression> hints,
      ExpressionManagerWithEncoder<Variable, Expression> expManager,
      int ThresholdForHints = Int32.MaxValue)
    {
      var deps = new Dictionary<Variable, Polynomial<Variable, Expression>>(hints.Count);
      foreach (var pol in hints.Enumerate())
      {
        Variable beta;
        int k = IsBetaDefinition(pol, out beta, expManager.Decoder);

        if (k == 0)
        { //shouldn't be reached for now, but it may come handy in the future
          beta = expManager.Encoder.FreshVariable<int>();
          deps.Add(beta, pol.AddMonomialToTheLeft(new Monomial<Variable>(-1, beta)));
        }
        else if (k == 1)
        {
          if (deps.ContainsKey(beta))
          {
            var betaTwo = expManager.Encoder.FreshVariable<int>();
            deps.Add(betaTwo, pol.Rename(beta, betaTwo));
          }
          else
          {
            deps.Add(beta, pol);
          }
        }
        else
        {
          // do nothing ; this case should not be reached, 
          // but if it becomes so, we may want to treat it better by extracting the underlying relation on program variables
        }
      }

      CleanDeps(ref deps, expManager);

      // Turn all the dependencies into hints
      hints = new Hints<Variable, Expression>(deps.Values);
      return false;
    }

    private Rational Separate(Polynomial<Variable, Expression> pol, out Polynomial<Variable, Expression> programVariables, out Polynomial<Variable, Expression> slackVariables)
    {
      Debug.Assert(pol.IsLinear);

      var pV = new List<Monomial<Variable>>();
      var sV = new List<Monomial<Variable>>();
      var result = Rational.For(0);

      foreach (var mono in pol.Left)
      {
        if (mono.IsConstant)
        {
          result -= mono.K;
        }
        else if (this.ExpressionManager.Decoder.IsSlackVariable(mono.VariableAt(0)))
        {
          sV.Add(mono);
        }
        else
        {
          pV.Add(mono);
        }
      }
      
      Debug.Assert(pol.Relation == ExpressionOperator.Equal || pol.Relation == ExpressionOperator.Equal_Obj);
      
      result += pol.Right[0].K;
      
      if (!Polynomial<Variable, Expression>.TryToPolynomialForm(pV.ToArray(), out programVariables))
      {
        throw new AbstractInterpretationException();
      }

      if (!Polynomial<Variable, Expression>.TryToPolynomialForm(sV.ToArray(), out slackVariables))
      {
        throw new AbstractInterpretationException();
      }

      return result;
    }

    /// <summary>
    /// beta = result * coeff + k
    /// </summary>
    private bool TryGetEquivalent(Variable beta, Polynomial<Variable, Expression> betaDef, out Variable result, out Rational coeff, out Rational k)
    {
      result = default(Variable);
      coeff = Rational.For(0);

      k = Rational.For(0);

        if (betaDep.ContainsKey(beta))
        {
          Polynomial<Variable, Expression> pol;
          if (LinearEqualitiesEnvironment<Variable, Expression>.TryCombine(betaDep[beta], betaDef, beta, out pol) && pol.IsTautology)
          {
            result = beta;
            coeff = Rational.For(1);
            k = Rational.For(0);
          
            return true;
          }
        }
       
      try 
        {
        var otherVar = default(Variable);
        
        foreach (var m in betaDef.Left)
        {
          if (m.VariableAt(0).Equals(beta)) 
            continue;
          
          otherVar = m.VariableAt(0);
          break;
        }

        Contract.Assert(!otherVar.Equals(default(Expression)));
        
        foreach (var e in this.betaDep.Keys)
        {
          var other = betaDep[e];
          if (!other.Variables.Contains(otherVar)) 
            continue;

          Polynomial<Variable, Expression> combined;
          if (!LinearEqualitiesEnvironment<Variable, Expression>.TryCombine(betaDef, other, otherVar, out combined))
          {
            continue;
          }
          if (combined.Variables.Count == 2) // both variables should be slack variables
          {
            var k1 = Rational.For(0);
            var k2 = Rational.For(0);
            foreach (var m in combined.Left)
            {
              if (m.VariableAt(0).Equals(beta))
              {
                k1 = m.K;
              }
              else
              {
                k2 = m.K;
                result = m.VariableAt(0);
              }
            }
            coeff = -k2 / k1;
            k = combined.Right[0].K / k1;

            return true;
          }
        }

        return false;
      }
      catch (ArithmeticExceptionRational)
      {
        return false;
      }
    }

    /// <summary>
    /// Method to put equalities in Karr for variables whose interval is a singleton. Used for keeping equalities at the join
    /// </summary>
    private void PutConstantsInKarr()
    {
      foreach (var e in Right.Variables)
      {
        var intv = Right.BoundsFor(e);
        if (intv.IsSingleton)
        {
          Left.AddConstraintEqual(e, intv.LowerBound);
        } 
      }
    }


    /// <summary>
    /// Method for Checking whether a polynomial corresponds to the definition of a slack variable. The result is the number of occurrences of slack variables, the out parameter one of the encountered slack variables if any.
    /// </summary>
    /// <param name="pol">The polynomial to check</param>
    /// <param name="beta">The last slack variable encountered, if any</param>
    /// <returns>The number of occurences of slack variables</returns>
    internal static int IsBetaDefinition(Polynomial<Variable, Expression> pol, out Variable beta, 
      IExpressionDecoder<Variable, Expression> decoder)
    {
      Contract.Requires(pol.IsLinear);

      beta = default(Variable);
      int result = 0;
      
      foreach (var m in pol.Left)
      {
        if (m.IsConstant)
        {
          continue;
        }

        if (decoder.IsSlackVariable(m.VariableAt(0)))
        {
          result++;
          beta = m.VariableAt(0);
        }
      }
      return result;
    }

    internal static int IsBetaDefinition(Polynomial<Variable, Expression> pol, out Variable beta,
      IExpressionDecoder<Variable, Expression> decoder, out bool result)
    {
      result = false;
      int k = IsBetaDefinition(pol, out beta, decoder);
      if (k == 1 && pol.Variables.Count > 2)
        result = true;
      return k;
    }
    #endregion

    #region Print statistics

    [Conditional("TRACE_PERFORMANCE")]
    public void PrintStatistics(string msg = "")
    {
      Console.WriteLine("Subpolyhedra {0} (mem: {1}Mbytes)", msg, System.GC.GetTotalMemory(false) / (1024 * 1024));
      Console.WriteLine("Linear equalities: {0}", this.Left.Statistics());
      Console.WriteLine("Intervals: {0}", this.Right.Statistics());
    }
    
    #endregion

    #region overriden : ToString

    private StringClosure Print(Hints<Variable, Expression> allHints)
    {
      return () =>
        {
          var s = new StringBuilder();
          foreach (var p in allHints.Enumerate())
          {
            s.AppendFormat("{0} ", p.ToString());
          }

          return s.ToString();
        };
    }

    public override string ToString()
    {
#if LOG
      StringBuilder deps = new StringBuilder();
      deps.AppendLine();
      foreach (var beta in betaDep.Keys)
      {
        deps.AppendLine(beta.ToString + " : " + betaDep[beta].ToString);
      }
      deps.AppendLine("hints :");
      foreach (var p in hints)
      {
        deps.AppendLine(p.ToString());
      }
      deps.AppendLine("number of widenings : " + numberOfWidenings);
      return "betaDeps :" + deps.ToString() + base.ToString();
#else 

#if LOG
      var isBottom = this.IsBottomWithReduction();
      return isBottom ? "bottom after reduction! (_|_)" : base.ToString();
#else
      var hintsCount = this.hints.Count.ToString();
      hintsCount += "\nHints:\n" + ToString(this.hints);
      var depsCount = this.betaDep.Count;
      return string.Format("#Hints:{0}\n#Deps:{1}\n{2}", hintsCount, depsCount, base.ToString());
#endif

#endif
    }

    private string ToString(Hints<Variable, Expression> lists)
    {
      if (lists == null || lists.Count == 0)
        return "";

      string result = "";

      foreach (var p in lists.Enumerate())
      {
        result += p.ToString() + " ";
      }

      return result.ToString();
    }

    #endregion

    #region Visitors

    private class SPTestTrueVisitor 
      : TestTrueVisitor<SubPolyhedra<Variable, Expression>, Variable, Expression>
    {
      private readonly ExpressionManagerWithEncoder<Variable, Expression> ExpressionManager;

      public SPTestTrueVisitor(ExpressionManagerWithEncoder<Variable, Expression> expManager)
        : base(expManager.Decoder)
      {
        Contract.Requires(expManager != null);

        this.ExpressionManager = expManager;
      }

      protected override SubPolyhedra<Variable, Expression> DispatchCompare(
        GenericExpressionVisitor<SubPolyhedra<Variable, Expression>, 
        SubPolyhedra<Variable, Expression>, Variable, Expression>.CompareVisitor cmp, Expression left, Expression right, Expression original, 
        SubPolyhedra<Variable, Expression> data)
      {
        return base.DispatchCompare(cmp, left, right, original, data);
      }

      public override SubPolyhedra<Variable, Expression> VisitEqual(Expression left, Expression right, Expression original, SubPolyhedra<Variable, Expression> data)
      {
        Polynomial<Variable, Expression> pol;
        if (!Polynomial<Variable, Expression>.TryToPolynomialForm(ExpressionOperator.Equal, left, right, this.ExpressionManager.Decoder, out pol) || !pol.IsLinear)
        {
          return data;
        }

        if (pol.IsInconsistent)
        {
          return data.Bottom as SubPolyhedra<Variable, Expression>;
        }

        data.Left.AddConstraint(data.Left.AsVector(pol));
        var newIntv = data.Right.TestTrueEqual(left, right);

        var result = new SubPolyhedra<Variable,Expression>(data.Left, data.Right, data.ExpressionManager);

        result.betaDep = data.betaDep;
        result.hints = data.hints;

        return result;
      }

      public override SubPolyhedra<Variable, Expression> VisitLessEqualThan(Expression left, Expression right, Expression original, SubPolyhedra<Variable, Expression> data)
      {
        return HelperForVisitLessEqualSignedOrUnsigned(true, left, right, data);
      }

      public override SubPolyhedra<Variable, Expression> VisitLessEqualThan_Un(Expression left, Expression right, Expression original, SubPolyhedra<Variable, Expression> data)
      {
        return HelperForVisitLessEqualSignedOrUnsigned(false, left, right, data);
      }

      public override SubPolyhedra<Variable, Expression> VisitLessThan(Expression left, Expression right, Expression original, SubPolyhedra<Variable, Expression> data)
      {
        return HelperForVisitLessThanSignedOrUnsigned(true, left, right, data);
      }

      public override SubPolyhedra<Variable, Expression> VisitLessThan_Un(Expression left, Expression right, Expression original, SubPolyhedra<Variable, Expression> data)
      {
        return HelperForVisitLessThanSignedOrUnsigned(false, left, right, data);
      }

      public override SubPolyhedra<Variable, Expression> VisitAddition(Expression left, Expression right, Expression original, SubPolyhedra<Variable, Expression> data)
      {
        var encoder = this.ExpressionManager.Encoder;
        var l1 = encoder.CompoundExpressionFor(ExpressionType.Int32, ExpressionOperator.Addition, left, right);
        var zero = Rational.For(0).ToExpression(encoder);
        var exp = encoder.CompoundExpressionFor(ExpressionType.Bool, ExpressionOperator.NotEqual, l1, zero);

        return VisitNotEqual(l1, zero, exp, data);
      }

      public override SubPolyhedra<Variable, Expression> VisitSubtraction(Expression left, Expression right, Expression original, SubPolyhedra<Variable, Expression> data)
      {
        var encoder = this.ExpressionManager.Encoder;
        var l1 = encoder.CompoundExpressionFor(ExpressionType.Int32, ExpressionOperator.Subtraction, left, right);
        var zero = Rational.For(0).ToExpression(encoder);
        var exp = encoder.CompoundExpressionFor(ExpressionType.Bool, ExpressionOperator.NotEqual, l1, zero);

        return VisitNotEqual(l1, zero, exp, data);
      }

      public override SubPolyhedra<Variable, Expression> VisitUnaryMinus(Expression left, Expression original, SubPolyhedra<Variable, Expression> data)
      {
        var encoder = this.ExpressionManager.Encoder;
        var l1 = encoder.CompoundExpressionFor(ExpressionType.Int32, ExpressionOperator.UnaryMinus, left);
        var zero = Rational.For(0).ToExpression(encoder);
        var exp = encoder.CompoundExpressionFor(ExpressionType.Bool, ExpressionOperator.NotEqual, l1, zero); 

        return VisitNotEqual(l1, zero, exp, data);
      }

      public override SubPolyhedra<Variable, Expression> VisitMultiplication(Expression left, Expression right, Expression original, SubPolyhedra<Variable, Expression> data)
      {
        var encoder = this.ExpressionManager.Encoder;
        var l1 = encoder.CompoundExpressionFor(ExpressionType.Int32, ExpressionOperator.Multiplication, left, right);
        var zero = Rational.For(0).ToExpression(encoder);
        var exp = encoder.CompoundExpressionFor(ExpressionType.Bool, ExpressionOperator.NotEqual, l1, zero); 

        return VisitNotEqual(l1, zero, exp, data);
      }

      public override SubPolyhedra<Variable, Expression> VisitNotEqual(Expression left, Expression right, Expression original, SubPolyhedra<Variable, Expression> data)
      {
        var leftVar = this.ExpressionManager.Decoder.UnderlyingVariable(left);
        var rightVar = this.ExpressionManager.Decoder.UnderlyingVariable(right);

        // left != null || right != null
        // We do not care, so we return
        if (this.ExpressionManager.Decoder.IsNull(right) || this.ExpressionManager.Decoder.IsNull(left) )
        {
          return data;
        }

        // If we have x != const (or const != x) then we have intervals to handle it
        int value;
        if (this.ExpressionManager.Decoder.IsConstantInt(left, out value) && this.ExpressionManager.Decoder.IsVariable(right))
        {
          return TestTrueNotEqualOnlyOnIntervals(left, right, data);
        }

        if (this.ExpressionManager.Decoder.IsConstantInt(right, out value) && this.ExpressionManager.Decoder.IsVariable(left))
        {
          return TestTrueNotEqualOnlyOnIntervals(left, right, data);
        }

        var subpoly1 = data.DuplicateMe();
        var subpoly2 = data.DuplicateMe();

        subpoly1 = subpoly1.TestTrueLessThan(left, right) as SubPolyhedra<Variable, Expression>;
        subpoly2 = subpoly2.TestTrueLessThan(right, left) as SubPolyhedra<Variable, Expression>;

        return subpoly1.Join(subpoly2) as SubPolyhedra<Variable, Expression>;
      }

      public override SubPolyhedra<Variable, Expression> VisitVariable(Variable variable, Expression original, SubPolyhedra<Variable, Expression> data)
      {
        var result = data.Factory(data.Left, data.Right.TestTrue(original)) as SubPolyhedra<Variable, Expression>;
        result.betaDep = new Dictionary<Variable, Polynomial<Variable, Expression>>(data.betaDep);
        //result.betaDep = new Dictionary<Variable, Polynomial<Variable, Expression>>(data.betaDep);
        result.hints = new Hints<Variable, Expression>(data.hints);

        return result;
      }

      private static SubPolyhedra<Variable, Expression> TestTrueNotEqualOnlyOnIntervals(Expression left, Expression right, SubPolyhedra<Variable, Expression> data)
      {
        var newIntv = data.Right.TestNotEqual(left, right);

        return data.Factory(data.Left, newIntv) as SubPolyhedra<Variable, Expression>;
      }

      private SubPolyhedra<Variable, Expression> HelperForVisitLessEqualSignedOrUnsigned(
        bool isSignedComparison, Expression left, Expression right, SubPolyhedra<Variable, Expression> data)
      {
        Variable beta;
        Polynomial<Variable, Expression> pol;

        var encoder = this.ExpressionManager.Encoder;

        // We are not allowed to infer it with an unsigned comparison
        if (isSignedComparison && !Decoder.IsConstant(left) && !Decoder.IsConstant(right))
        {
          beta = encoder.FreshVariable<int>();
          var monos = new Monomial<Variable>[]
          { 
            new Monomial<Variable>(Decoder.UnderlyingVariable(Decoder.Stripped(left))), 
            new Monomial<Variable>(-1, Decoder.UnderlyingVariable(Decoder.Stripped(right))),
            new Monomial<Variable>(-1, beta)
          };

          Polynomial<Variable, Expression>.TryToPolynomialForm(monos, out pol);

          data.Left.AddConstraint(data.Left.AsVector(pol));
          data.betaDep[beta] = pol;
          data.Right[beta] = Interval.NegativeInterval;
        }
        else
        {
          var newIntv = isSignedComparison ?
            data.Right.TestTrueLessEqualThan(left, right) :
            data.Right.TestTrue(encoder.CompoundExpressionFor(ExpressionType.Bool, ExpressionOperator.LessEqualThan_Un, left, right));

          data = new SubPolyhedra<Variable, Expression>(data, newIntv);
        }

        var bop = isSignedComparison ? ExpressionOperator.LessEqualThan : ExpressionOperator.LessEqualThan_Un;

        if (!Polynomial<Variable, Expression>.TryToPolynomialForm(bop, left, right, this.ExpressionManager.Decoder, out pol) 
          || !pol.IsLinear)
        {
          return data;
        }

        if (pol.IsInconsistent)
        {
          return data.Bottom as SubPolyhedra<Variable, Expression>;
        }

        if (pol.Left.Length == 1)
        {
          var newIntv = data.Right.TestTrue(pol.ToPureExpression(encoder));
          return new SubPolyhedra<Variable, Expression>(data, newIntv);
        }

        beta = encoder.FreshVariable<int>();
        var mono = new Monomial<Variable>(beta);
        mono.K = -mono.K;
        var newPolLeft = new List<Monomial<Variable>>(pol.Left);
        newPolLeft.Add(mono);

        Polynomial<Variable, Expression> eq;

        if (Polynomial<Variable, Expression>.TryToPolynomialForm(newPolLeft.ToArray(), out eq))
        {
          data.Left.AddConstraint(data.Left.AsVector(eq));
          data.betaDep[beta] = eq;
          data.hints.Add(eq);
        }
        data.Right[beta] = Interval.For(Rational.MinusInfinity, pol.Right[0].K);

        return data;
      }

      private SubPolyhedra<Variable, Expression> HelperForVisitLessThanSignedOrUnsigned(
        bool isSignedComparison, Expression left, Expression right, SubPolyhedra<Variable, Expression> data)
      {
        Variable beta;
        var encoder = this.ExpressionManager.Encoder;

        // We are not allowed to infer it with an unsigned comparison
        Polynomial<Variable, Expression> pol;
        if (isSignedComparison && !Decoder.IsConstant(left) && !Decoder.IsConstant(right))
        {
          beta = encoder.FreshVariable<int>();
          var monos = new Monomial<Variable>[]
          {
            new Monomial<Variable>(Decoder.UnderlyingVariable(Decoder.Stripped(left))),
            new Monomial<Variable>(-1, Decoder.UnderlyingVariable(Decoder.Stripped(right))),
            new Monomial<Variable>(-1, beta)
          };

          Polynomial<Variable, Expression>.TryToPolynomialForm(monos, out pol);
          data.Left.AddConstraint(data.Left.AsVector(pol));
          data.betaDep[beta] = pol;
          data.Right[beta] = Interval.For(Rational.MinusInfinity, -1);
        }
        else
        {
          var newIntv = isSignedComparison ?
            data.Right.TestTrueLessThan(left, right) :
            data.Right.TestTrueLessThan_Un(left, right);

          data = new SubPolyhedra<Variable, Expression>(data, newIntv);
        }

        var bop = isSignedComparison ? ExpressionOperator.LessThan : ExpressionOperator.LessThan_Un;

        if (!Polynomial<Variable, Expression>.TryToPolynomialForm(bop, left, right, this.ExpressionManager.Decoder, out pol) ||  !pol.IsLinear)
        {
          return data;
        }

        if (pol.IsInconsistent)
        {
          return data.Bottom as SubPolyhedra<Variable, Expression>;
        }

        if (pol.Left.Length == 1)
        {
          var newIntv = data.Right.TestTrue(pol.ToPureExpression(encoder));
          return new SubPolyhedra<Variable, Expression>(data, newIntv);
        }

        beta = encoder.FreshVariable<int>();
        var mono = new Monomial<Variable>(beta);
        mono.K = -mono.K;
        
        var newPolLeft = new List<Monomial<Variable>>(pol.Left);
        newPolLeft.Add(mono);

        Polynomial<Variable, Expression> eq;

        if (Polynomial<Variable, Expression>.TryToPolynomialForm(newPolLeft.ToArray(), out eq))
        {
          data.Left.AddConstraint(data.Left.AsVector(eq));
          data.betaDep[beta] = eq;
          data.hints.Add(eq);
        }
        data.Right[beta] = Interval.For(Rational.MinusInfinity, pol.Right[0].K.IsInteger ? pol.Right[0].K - 1 : pol.Right[0].K);

        return data;
      }

    }

    private class SPTestFalseVisitor
      : TestFalseVisitor<SubPolyhedra<Variable, Expression>, Variable, Expression>
    {
      public SPTestFalseVisitor(IExpressionDecoder<Variable, Expression> decoder)
        : base(decoder)
      {
      }

      public override SubPolyhedra<Variable, Expression> VisitVariable(Variable variable, Expression original, SubPolyhedra<Variable, Expression> data)
      {
        var result = data.Factory(data.Left, data.Right.TestFalse(original)) as SubPolyhedra<Variable, Expression>;

        result.betaDep = new Dictionary<Variable, Polynomial<Variable, Expression>>(data.betaDep);
        result.hints = new Hints<Variable, Expression>(data.hints);
        
        return result;
      }
    }

    private class SPCheckIfHoldsVisitor
      : CheckIfHoldsVisitor<SubPolyhedra<Variable, Expression>, Variable, Expression>
    {
      private readonly ExpressionManagerWithEncoder<Variable, Expression> ExpressionManager;

      public SPCheckIfHoldsVisitor(ExpressionManagerWithEncoder<Variable, Expression> expManager)
        : base(expManager.Decoder)
      {
        Contract.Requires(expManager != null);

        this.ExpressionManager = expManager;
      }

      public override FlatAbstractDomain<bool> VisitEqual(Expression left, Expression right, Expression original, FlatAbstractDomain<bool> data)
      {
        if (Domain.IsBottom)
        {
          return data.Bottom;
        }

        var encoder = this.ExpressionManager.Encoder;
        var decoder = this.ExpressionManager.Decoder;

        if ((decoder.VariablesIn(left).IsSingleton && decoder.IsConstant(right))
          || (decoder.VariablesIn(right).IsSingleton && decoder.IsConstant(left)))
        {
          return Domain.Left.BoundsInference(Domain.Right, SubPolyhedra.Algorithm, SubPolyhedra.MaxVariablesToRunSimplex).CheckIfHolds(encoder.CompoundExpressionFor(ExpressionType.Bool, ExpressionOperator.Equal, left, right));
        }
        
        var beta = Domain.ExpressionManager.Encoder.FreshVariable<int>();

        Polynomial<Variable, Expression> polBeta;
        if (!Polynomial<Variable, Expression>.TryToPolynomialForm(encoder.CompoundExpressionFor(ExpressionType.Int32,
          ExpressionOperator.Subtraction,
            encoder.CompoundExpressionFor(ExpressionType.Int32, ExpressionOperator.Subtraction, left, right),
            encoder.VariableFor(beta)),
            Domain.ExpressionManager.Decoder, out polBeta))
        {
          return CheckOutcome.Top;
        }

        if (!polBeta.IsLinear)
        {
          return CheckOutcome.Top;
        }

        var enlarged = Domain.DuplicateMe();
        enlarged.Left.AddConstraint(enlarged.Left.AsVector(polBeta));
        enlarged = enlarged.ReduceInternal(enlarged.Left, enlarged.Right, false);

        return enlarged.Right.BoundsFor(beta).IsSingleton && enlarged.Right.BoundsFor(beta).LowerBound.IsZero? 
          CheckOutcome.True : 
          CheckOutcome.Top;
      }

      public override FlatAbstractDomain<bool> VisitEqual_Obj(Expression left, Expression right, Expression original, FlatAbstractDomain<bool> data)
      {
        return VisitEqual(left, right, original, data);
      }

      public override FlatAbstractDomain<bool> VisitLessEqualThan_Un(Expression left, Expression right, Expression original, FlatAbstractDomain<bool> data)
      {
        // We check if one of the two operands is a constant because of the polymorphic behavior of constants with *_un
        if (Domain.ExpressionManager.Decoder.IsConstant(Decoder.Stripped(left)) || Domain.ExpressionManager.Decoder.IsConstant(Decoder.Stripped(right)))
        {
          var c1 = Domain.Left.CheckIfLessEqualThan_Un(left, right);
          var c2 = Domain.Right.CheckIfLessEqualThan_Un(left, right);

          return c1.Meet(c2);
        }
        else
        {
          return VisitLessEqualThan(left, right, original, data);
        }
      }

      public override FlatAbstractDomain<bool> VisitLessThan_Un(Expression left, Expression right, Expression original, FlatAbstractDomain<bool> data)
      {
        // We check if one of the two operands is a constant because of the polymorphic behavior of constants with *_un
        if (Domain.ExpressionManager.Decoder.IsConstant(Decoder.Stripped(left)) || Domain.ExpressionManager.Decoder.IsConstant(Decoder.Stripped(right)))
        {
          var c1 = Domain.Left.CheckIfLessThan_Un(left, right);
          var c2 = Domain.Right.CheckIfLessThan_Un(left, right);

          return c1.Meet(c2);
        }
        else
        {
          return VisitLessThan(left, right, original, data);
        }
      }

      public override FlatAbstractDomain<bool> VisitLessEqualThan(Expression left, Expression right, Expression original, FlatAbstractDomain<bool> data)
      {
        if (Domain.IsBottom)
        {
          return data.Bottom;
        }

        // First try: discharge constants
        var intervalsOutcome = this.Domain.Right.CheckIfLessEqualThan(left, right);
        if (!intervalsOutcome.IsTop)
        {
          return intervalsOutcome;
        }

        var decoder = this.ExpressionManager.Decoder;

        if ((decoder.VariablesIn(left).IsSingleton && decoder.IsConstant(right))
          || (decoder.VariablesIn(right).IsSingleton && decoder.IsConstant(left)))
        {
          return Domain.Left.BoundsInference(Domain.Right, SubPolyhedra.Algorithm, SubPolyhedra.MaxVariablesToRunSimplex).CheckIfLessEqualThan(left, right);
        }

        var encoder = this.ExpressionManager.Encoder;

        var beta = Domain.ExpressionManager.Encoder.FreshVariable<int>();

        Polynomial<Variable, Expression> polBeta;
        if (!Polynomial<Variable, Expression>.TryToPolynomialForm(encoder.CompoundExpressionFor(ExpressionType.Int32,
          ExpressionOperator.Subtraction,
            encoder.CompoundExpressionFor(ExpressionType.Int32, ExpressionOperator.Subtraction, left, right), 
            encoder.VariableFor(beta)),
            Domain.ExpressionManager.Decoder, out polBeta))
        {
          // F: we are not closing applying the reduction here
          var intvLeft = this.Domain.BoundsFor(left);
          var intvRight = this.Domain.BoundsFor(right);

          if (intvLeft.IsNormal && intvRight.IsNormal)
          {
            return intvLeft.UpperBound <= intvRight.LowerBound ? CheckOutcome.True : CheckOutcome.Top;
          }

          return CheckOutcome.Top;
        }

        if (!polBeta.IsLinear)
        {
          return CheckOutcome.Top;
        }

        var enlarged = Domain.DuplicateMe();
        enlarged.Left.AddConstraint(enlarged.Left.AsVector(polBeta));
        enlarged = enlarged.ReduceInternal(enlarged.Left, enlarged.Right, false);
        
        var intv = enlarged.Right.BoundsFor(beta);
        return intv.IsNormal && intv.UpperBound <= 0 ? CheckOutcome.True : CheckOutcome.Top;
      }

      public override FlatAbstractDomain<bool> VisitLessThan(Expression left, Expression right, Expression original, FlatAbstractDomain<bool> data)
      {
        if (Domain.IsBottom)
        {
          return data.Bottom;
        }

        // First try: discharge constants
        var intervalsOutcome = this.Domain.Right.CheckIfLessThan(left, right);
        if (!intervalsOutcome.IsTop)
        {
          return intervalsOutcome;
        }

        var decoder = this.ExpressionManager.Decoder;

        if ((decoder.VariablesIn(left).IsSingleton && decoder.IsConstant(right))
          || (decoder.VariablesIn(right).IsSingleton && decoder.IsConstant(left)))
        {
          return Domain.Left.BoundsInference(Domain.Right, SubPolyhedra.Algorithm, SubPolyhedra.MaxVariablesToRunSimplex).CheckIfLessThan(left, right);
        }

        var beta = Domain.ExpressionManager.Encoder.FreshVariable<int>();

        var encoder = this.ExpressionManager.Encoder;

        Polynomial<Variable, Expression> polBeta;
        
        if (!Polynomial<Variable, Expression>.TryToPolynomialForm(encoder.CompoundExpressionFor(ExpressionType.Int32,
          ExpressionOperator.Subtraction,
            encoder.CompoundExpressionFor(ExpressionType.Int32, ExpressionOperator.Subtraction, left, right), 
            encoder.VariableFor(beta)),
            Domain.ExpressionManager.Decoder, out polBeta))
        {
          // F: we are not closing applying the reduction here
          var intvLeft = this.Domain.BoundsFor(left);
          var intvRight = this.Domain.BoundsFor(right);

          if (intvLeft.IsNormal && intvRight.IsNormal)
          {
            return intvLeft.UpperBound < intvRight.LowerBound ? CheckOutcome.True : CheckOutcome.Top;
          }

          return CheckOutcome.Top;
        }

        if (!polBeta.IsLinear)
        {
          return CheckOutcome.Top;
        }

        var enlarged = Domain.DuplicateMe();
        enlarged.Left.AddConstraint(enlarged.Left.AsVector(polBeta));
        enlarged = enlarged.ReduceInternal(enlarged.Left, enlarged.Right, false);

        var intv = enlarged.Right.BoundsFor(beta);

        return intv.IsNormal && intv.UpperBound < 0 ? CheckOutcome.True: CheckOutcome.Top;
      }

      public override FlatAbstractDomain<bool> VisitConstant(Expression left, FlatAbstractDomain<bool> data)
      {
        if (Domain.IsBottom)
        {
          return CheckOutcome.Bottom;
        }

        return Domain.Right.CheckIfHolds(left);
      }
    }

    #endregion
  }

  internal class Hints<Variable, Expression>
  {
    private readonly List<Polynomial<Variable, Expression>> hints;

    public Hints()
    {
      this.hints = new List<Polynomial<Variable, Expression>>();
    }

    public Hints(Hints<Variable, Expression> other)
    {
      Contract.Requires(other != null);
      this.hints = new List<Polynomial<Variable, Expression>>(other.hints);
    }

    public Hints(IEnumerable<Polynomial<Variable, Expression>> others)
    {
      Contract.Requires(others != null);
      this.hints = new List<Polynomial<Variable, Expression>>(others);
    }

    public int Count { get { return this.hints.Count; } }

    public bool Contains(Polynomial<Variable, Expression> pol)
    {
      return this.hints.Contains(pol);
    }

    public IEnumerable<Polynomial<Variable, Expression>> Enumerate(int max = -1)
    {
      max = max < 0 ? SubPolyhedra.MaxHintsToRetainInWidening : max;

      return this.hints.Take(max);
    }

    public void Add(Polynomial<Variable, Expression> hint)
    {
      this.hints.Add(hint);
    }

    public void Add(IEnumerable<Polynomial<Variable, Expression>> hints)
    {
      Contract.Requires(hints != null);

      this.hints.AddRange(hints);
    }

    public void Add(Hints<Variable, Expression> other)
    {
      Contract.Requires(other != null);

      this.hints.AddRange(other.hints);
    }

    public void Remove(Polynomial<Variable, Expression> p)
    {
      this.hints.Remove(p);
    }

    public void Clear()
    {
      this.hints.Clear();
    }
 
    public override string ToString()
    {
      return this.hints.ToString();
    }

  }

}