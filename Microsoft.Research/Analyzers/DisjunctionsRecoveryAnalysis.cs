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
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using Microsoft.Research.AbstractDomains;
using System.Diagnostics.Contracts;
using Microsoft.Research.DataStructures;
using Microsoft.Research.AbstractDomains.Expressions;
using Microsoft.Research.AbstractDomains.Numerical;

namespace Microsoft.Research.CodeAnalysis
{
  public static partial class AnalysisWrapper
  {
    public static SyntacticInformation<Method, Field, Variable> 
      RunDisjunctionRecoveryAnalysis<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>
    (
      string methodName,
      IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> driver,
      Predicate<APC> cachePCs
    )
      where Variable : IEquatable<Variable>
      where Expression : IEquatable<Expression>
      where Type : IEquatable<Type>
    {
      Contract.Requires(driver != null);

      var analysis =
       new TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>.DisjunctionsRecoveryAnalysis
         (methodName, driver, cachePCs);

      var closure = driver.HybridLayer.CreateForward<TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>.DisjunctiveRefinement>(
          analysis, 
          new DFAOptions { Trace = driver.Options.TraceDFA, Timeout = driver.Options.Timeout, SymbolicTimeout = driver.Options.SymbolicTimeout, EnforceFairJoin = driver.Options.EnforceFairJoin, IterationsBeforeWidening = driver.Options.IterationsBeforeWidening, TraceTimePerInstruction = driver.Options.TraceTimings, TraceMemoryPerInstruction = driver.Options.TraceMemoryConsumption }, null);

      closure(analysis.GetTopValue());   // Do the analysis 

      analysis.ClearCaches();

      return new SyntacticInformation<Method,Field, Variable>(analysis, analysis.Tests, analysis.RightExpressions, analysis.FirstViewAt, analysis.MayUpdateFields, analysis.MethodCalls, analysis.liveVariablesInRenamings, analysis.RenamingsLength, analysis.HasThrow, analysis.HasExceptionHandlers);
    }

    #region Void Options

    public class VoidOptions
      : IValueAnalysisOptions
    {
      readonly private ILogOptions options;

      public VoidOptions(ILogOptions options)
      {
        Contract.Requires(options != null);

        this.options = options;
      }

      public ILogOptions LogOptions
      {
        get { return this.options; }
      }

      public bool NoProofObligations
      {
        get { return false; }
      }

      public int Steps
      {
        get { return 0; }
      }

      public ReductionAlgorithm Algorithm
      {
        get { return ReductionAlgorithm.None; }
      }

      public bool Use2DConvexHull
      {
        get { return false; }
      }

      public bool InferOctagonConstraints
      {
        get { return false; }
      }

      public bool UseMorePreciseWidening
      {
        get { return false; }
      }

      public bool UseTracePartitioning
      {
        get { return false; }
      }

      public bool TrackDisequalities
      {
        get { return false; }
      }

      public bool TracePartitionAnalysis
      {
        get { return false; }
      }

      public bool TraceNumericalAnalysis
      {
        get { return false; }
      }
    }
    
    #endregion

    /// <summary>
    /// This class is just for binding types for the internal clases
    /// </summary>
    public static partial class TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>
      where Variable : IEquatable<Variable>
      where Expression : IEquatable<Expression>
      where Type : IEquatable<Type>
    {
      public class DisjunctionsRecoveryAnalysis :
        GenericValueAnalysis<DisjunctiveRefinement, VoidOptions>,
        IDisjunctiveExpressionRefiner<Variable, BoxedExpression>
      {

        #region Consts

        const int MAXDEPTH = 15;
        
        #endregion

        #region Object invariant

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
          Contract.Invariant(this.Tests != null);
          Contract.Invariant(this.liveVariablesInRenamings != null);
        }

        #endregion

        #region State

        readonly public List<SyntacticTest> Tests;
        readonly public List<RightExpression> RightExpressions;
        readonly public Dictionary<Variable, APC> FirstViewAt;
        readonly public List<Field> MayUpdateFields;
        readonly public List<MethodCallInfo<Method, Variable>> MethodCalls;
        readonly public Set<string> StringsForPostconditions;
        readonly public List<Tuple<Pair<APC, APC>, Dictionary<Variable, HashSet<Variable>>>> liveVariablesInRenamings;
        readonly public List<Tuple<Pair<APC, APC>, int>> RenamingsLength;

        public bool HasThrow { get; private set; }
        public bool HasExceptionHandlers { get; private set; }

        #endregion

        #region Constructor

        public DisjunctionsRecoveryAnalysis(
          string methodName, 
          IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> mdriver,
          Predicate<APC> cachePCs)
        : base(methodName, mdriver, new VoidOptions(mdriver.Options), cachePCs)
        {
          this.Tests = new List<SyntacticTest>();
          this.RightExpressions = new List<RightExpression>();
          this.FirstViewAt = new Dictionary<Variable, APC>();
          this.MayUpdateFields = new List<Field>();
          this.MethodCalls = new List<MethodCallInfo<Method, Variable>>();
          this.StringsForPostconditions = new Set<string>();
          this.liveVariablesInRenamings = new List<Tuple<Pair<APC, APC>, Dictionary<Variable, HashSet<Variable>>>>();
          this.RenamingsLength = new List<Tuple<Pair<APC, APC>, int>>();
          this.HasThrow = false;
        }
        
        #endregion

        #region Transfer functions

        public override DisjunctiveRefinement Assume(APC pc, string tag, Variable source, object provenance, DisjunctiveRefinement data)
        {
          Contract.Assume(data != null);
          this.InferExceptionHandlers(pc);

          var refinedExp = new LazyEval<BoxedExpression>(
            () => BoxedExpression.Convert(this.Context.ExpressionContext.Refine(pc, source), this.MethodDriver.ExpressionDecoder, MAXDEPTH));

          if (tag != "false")
          {
            var toRefine = source;

            // in clousot1 we refine "assume refinedExp". 
            // in clousot2 we refinedExp may be exp != 0 and in this case we refine "assume exp"
            if (!this.MethodDriver.SyntacticComplexity.TooManyJoinsForBackwardsChecking &&
              refinedExp.Value != null && CanRefineAVariableTruthValue(refinedExp.Value, ref toRefine))
            {
              Log("Trying to refine the variable {0} to one containing logical connectives", refinedExp.Value.UnderlyingVariable.ToString);

              BoxedExpression refinedExpWithConnectives;
              if (TryToBoxedExpressionWithBooleanConnectives(pc, tag, toRefine, false, 
                TopNumericalDomain<BoxedVariable<Variable>, BoxedExpression>.Singleton, out refinedExpWithConnectives))
              {
                Log("Succeeded. Got {0}", refinedExpWithConnectives.ToString);

                data[new BoxedVariable<Variable>(source)] = new SetOfConstraints<BoxedExpression>(refinedExpWithConnectives);
              }
            }
          }

          APC pcForExpression;
          if (!this.FirstViewAt.TryGetValue(source, out pcForExpression))
          {
            pcForExpression = pc;
          }

          this.Tests.Add(new SyntacticTest(SyntacticTest.Polarity.Assume, pcForExpression, tag, refinedExp)); 

          // We do not call the base Assume as it performs too many things not needed here
          return data;
        }

        private bool CanRefineAVariableTruthValue(BoxedExpression exp, ref Variable v)
        {
          Contract.Requires(exp != null);

          if (exp.IsVariable)
          {
            return true;
          }

          BinaryOperator bop;
          int k;
          return exp.IsCheckExpOpConst(out bop, out v, out k) && (
            (bop == BinaryOperator.Ceq && k != 0) || (bop == BinaryOperator.Cne_Un && k == 0));
        }

        public override DisjunctiveRefinement Assert(APC pc, string tag, Variable condition, object provenance, DisjunctiveRefinement data)
        {
          this.InferExceptionHandlers(pc);

          var md = this.MethodDriver;

          Func<BoxedExpression> closure = () =>
            {
              BoxedExpression newPost;
              if (
                (newPost = md.AsExistsIndexed(pc, condition)) != null
                ||
                (newPost = md.AsForAllIndexed(pc, condition)) != null)
              {
                return newPost;
              }
              else
              {
                return BoxedExpression.Convert(this.Context.ExpressionContext.Refine(pc, condition), md.ExpressionDecoder, MAXDEPTH, false);
              }
            };

          this.Tests.Add(new SyntacticTest(SyntacticTest.Polarity.Assert, pc, tag, new LazyEval<BoxedExpression>(closure)));

          return this.Assume(pc, tag, condition, provenance, data);
        }

        public override DisjunctiveRefinement Binary(APC pc, BinaryOperator op, Variable dest, Variable s1, Variable s2, DisjunctiveRefinement data)
        {
          this.InferExceptionHandlers(pc);

          // We want to save the PC where "dest" first appeared. 
          // We will need this information in the code fixes, because we need the expression source context, and not the statement where it appears
          if (!this.FirstViewAt.ContainsKey(dest))
          {
            this.FirstViewAt[dest] = pc;
          }

          if (!op.IsComparisonBinaryOperator())
          {
            var rValueExp = new LazyEval<BoxedExpression>(
             () =>
             {
               var left = BoxedExpression.Convert(this.Context.ExpressionContext.Refine(pc, s1), this.MethodDriver.ExpressionDecoder, MAXDEPTH);
               var right = BoxedExpression.Convert(this.Context.ExpressionContext.Refine(pc, s2), this.MethodDriver.ExpressionDecoder, MAXDEPTH);
               // conversion may fail, so let us check it
               return (left != null && right != null)? 
                 BoxedExpression.Binary(op, left, right, dest) : 
                 null;
             });

            this.RightExpressions.Add(new RightExpression(pc, rValueExp));
          }
          return data;
        }

        public override DisjunctiveRefinement Throw(APC pc, Variable exn, DisjunctiveRefinement data)
        {
          this.HasThrow = true;
          this.InferExceptionHandlers(pc);

          return base.Throw(pc, exn, data);
        }

        public override DisjunctiveRefinement Ldelem(APC pc, Type type, Variable dest, Variable array, Variable index, DisjunctiveRefinement data)
        {
          this.InferExceptionHandlers(pc);

          data[new BoxedVariable<Variable>(dest)] = new SetOfConstraints<BoxedExpression>(new BoxedExpression.ArrayIndexExpression<Type>(ToBoxedExpression(pc, array), ToBoxedExpression(pc, index), type));

          return data;
        }

        public override DisjunctiveRefinement Stfld(APC pc, Field field, bool @volatile, Variable obj, Variable value, DisjunctiveRefinement data)
        {
          this.InferExceptionHandlers(pc);

          this.MayUpdateFields.Add(field);
          return data;
        }

        public override DisjunctiveRefinement Call<TypeList, ArgList>(APC pc, Method method, bool tail, bool virt, TypeList extraVarargs, Variable dest, ArgList args, DisjunctiveRefinement data)
        {
          this.InferExceptionHandlers(pc);

          var asForAll = this.MethodDriver.AsForAllIndexed(pc.Post(), dest);
          if (asForAll != null)
          {
            data[new BoxedVariable<Variable>(dest)] = new SetOfConstraints<BoxedExpression>(asForAll);
          }
          else
          {
            this.MethodCalls.Add(new MethodCallInfo<Method, Variable>(pc, method, args.Enumerate().ToList()));
          }

          return data;
        }

        public override DisjunctiveRefinement Return(APC pc, Variable source, DisjunctiveRefinement data)
        {
          this.InferExceptionHandlers(pc);

          var md = this.MethodDriver;
          var mdd = md.MetaDataDecoder;
          BoxedExpression refinedExpWithConnectives;
          if (!mdd.System_Void.Equals(mdd.ReturnType(md.CurrentMethod)) && 
            TryToBoxedExpressionWithBooleanConnectives(pc, "ret", source, true, TopNumericalDomain<BoxedVariable<Variable>, BoxedExpression>.Singleton, out refinedExpWithConnectives))
          {
            Log("Succeeded. Got {0}", refinedExpWithConnectives.ToString);

            data[new BoxedVariable<Variable>(source)] = new SetOfConstraints<BoxedExpression>(refinedExpWithConnectives);
          }

          return data;
        }

        public override DisjunctiveRefinement HelperForAssignInParallel(DisjunctiveRefinement state, Pair<APC, APC> edge, 
          Dictionary<BoxedVariable<Variable>, FList<BoxedVariable<Variable>>> refinedMap, 
          Converter<BoxedVariable<Variable>, BoxedExpression> convert)
        {
          if(refinedMap != null)
          {
            this.RenamingsLength.Add(new Tuple<Pair<APC, APC>, int>(edge, refinedMap.Count));
          }

          return state.AssignInParallelFunctional(refinedMap, convert);
        }

        public override DisjunctiveRefinement Join(Pair<APC, APC> edge, DisjunctiveRefinement newState, DisjunctiveRefinement prevState, out bool changed, bool widen)
        {

          // this.SaveRenamings(edge, prevState.varsInAssignment, newState.varsInAssignment);

          return base.Join(edge, newState, prevState, out changed, widen);
        }

        protected override DisjunctiveRefinement Default(APC pc, DisjunctiveRefinement data)
        {
          this.InferExceptionHandlers(pc);
          return base.Default(pc, data);
        }

        private void SaveRenamings(Pair<APC, APC> edge, Dictionary<Variable, HashSet<Variable>> dictionary1, Dictionary<Variable, HashSet<Variable>> dictionary2)
        {
          this.liveVariablesInRenamings.Add(new Tuple<Pair<APC, APC>, Dictionary<Variable, HashSet<Variable>>>(edge, dictionary1));
          this.liveVariablesInRenamings.Add(new Tuple<Pair<APC, APC>, Dictionary<Variable, HashSet<Variable>>>(edge, dictionary2));
        }

        #endregion

        #region Overridden

        public override bool SuggestAnalysisSpecificPostconditions(ContractInferenceManager inferenceManager, IFixpointInfo<APC, DisjunctiveRefinement> fixpointInfo, List<BoxedExpression> postconditions)
        {
          // does nothing
          return false;
        }

        public override bool TrySuggestPostconditionForOutParameters(IFixpointInfo<APC, DisjunctiveRefinement> fixpointInfo, List<BoxedExpression> postconditions, Variable p, FList<PathElement> path)
        {
          // does nothing
          return false;
        }

        public override DisjunctiveRefinement GetTopValue()
        {
          return new DisjunctiveRefinement(this.ExpressionManager);
        }

        public override IFactQuery<BoxedExpression, Variable> FactQuery(IFixpointInfo<APC, DisjunctiveRefinement> fixpoint)
        {
          return null;
        }

        #endregion

        #region IDisjunctiveExpressionRefined
        
        public bool TryRefineExpression(APC pc, Variable toRefine, out BoxedExpression refined)
        {
          DisjunctiveRefinement state;
          if (PreStateLookup(pc, out state))
          {
            SetOfConstraints<BoxedExpression> candidates;
            if (state.TryGetValue(new BoxedVariable<Variable>(toRefine), out candidates) && candidates.IsNormal() && candidates.Count == 1)
            {
              refined = candidates.Values.First();
              return refined != null;
            }
          }

          refined = null;
          return false;
        }

        public bool TryApplyModusPonens(APC pc, BoxedExpression premise, Predicate<BoxedExpression> IsTrue, out List<BoxedExpression> consequences)
        {
          DisjunctiveRefinement state;
          if (PreStateLookup(pc, out state))
          {
            return state.TryApplyModusPonens(premise, IsTrue, out consequences);
          }

          consequences = null;
          return false;
        }

        #endregion

        #region Info
        private void InferExceptionHandlers(APC pc)
        {
          if (this.HasExceptionHandlers)
          {
            return;
          }

          var handlers = this.Context.MethodContext.CFG.ExceptionHandlers<int, int>(pc, 0, null);
          foreach (var h in handlers)
          {
            if (!pc.Block.Subroutine.ExceptionExit.Equals(h.Block))
            {
              this.HasExceptionHandlers = true;
              return;
            }
          }

        }

        #endregion
      }

      #region Abstract domain

      public class DisjunctiveRefinement :
        FunctionalAbstractDomainEnvironment<DisjunctiveRefinement, BoxedVariable<Variable>, SetOfConstraints<BoxedExpression>, BoxedVariable<Variable>, BoxedExpression>
      {

        #region ExtraState

        public readonly Dictionary<Variable, HashSet<Variable>> varsInAssignment = null;
        
        #endregion

        #region Constructor

        public DisjunctiveRefinement(ExpressionManager<BoxedVariable<Variable>, BoxedExpression> expManager)
          : base(expManager)
        {
          Contract.Requires(expManager != null);
        }

        private DisjunctiveRefinement(DisjunctiveRefinement other, Dictionary<Variable, HashSet<Variable>> vars)
          : this(other)
        {
          Contract.Requires(other != null);

          this.varsInAssignment = vars;
        }
        private DisjunctiveRefinement(DisjunctiveRefinement other)
          : base(other)
        {
        }

        #endregion

        #region Abstract Domain specific

        public IEnumerable<BoxedExpression> Disjunctions
        {
          get
          {
            if (this.IsNormal())
            {
              foreach (var pair in this.Elements)
              {
                if (pair.Value.IsNormal())
                {
                  foreach (var exp in pair.Value.Values)
                  {
                    yield return exp;
                  }
                }
              }
            }
          }
        }

        public bool TryApplyModusPonens(BoxedExpression premise, Predicate<BoxedExpression> oracle, out List<BoxedExpression> consequences)
        {
          Contract.Requires(premise != null);
          Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out consequences) != null);

          if (this.IsNormal())
          {
            consequences = new List<BoxedExpression>();

            foreach (var exp in this.Disjunctions)
            {
              BinaryOperator bop;
              BoxedExpression left, right;
              if (exp.IsBinaryExpression(out bop, out left, out right) && bop == BinaryOperator.LogicalOr)
              {
                left = RemoveKnownFacts(left, oracle);
                right = RemoveKnownFacts(right, oracle);

                if (left != null && this.ExtendedExpressionEquals(left, premise))
                {
                  consequences.AddIfNotNull(RemoveShortCutExpression(right, premise));
                }
                else if (right != null && this.ExtendedExpressionEquals(right, premise))
                {
                  consequences.AddIfNotNull(RemoveShortCutExpression(left, premise));
                }
              }
            }

            return consequences.Count != 0;
          }

          consequences = null;
          return false;
        }

        private BoxedExpression RemoveKnownFacts(BoxedExpression original, Predicate<BoxedExpression> isKnown)
        {
          if (isKnown == null)
          {
            return original;
          }

          var splitted = original.SplitConjunctions();
          BoxedExpression result = null;

          foreach (var exp in splitted)
          {
            if (exp == null || isKnown(exp))
            {
              continue;
            }

            result = result == null ? exp : BoxedExpression.Binary(BinaryOperator.LogicalAnd, result, exp);
          }

          return result;
        }

        /// <summary>
        /// Because of shortcut we may have P && !premise
        /// </summary>
        private BoxedExpression RemoveShortCutExpression(BoxedExpression exp, BoxedExpression premise)
        {
          if (exp == null)
          {
            return null;
          }

          BinaryOperator bop;
          BoxedExpression left, right;

          if (exp.IsBinaryExpression(out bop, out left, out right) && bop == BinaryOperator.LogicalAnd)
          {
            var negatedPremise = premise.Negate();

            if (left.Equals(negatedPremise))
            {
              return right;
            }
            else if (right.Equals(negatedPremise))
            {
              return left;
            }
          }

          return exp;
        }

        private bool ExtendedExpressionEquals(BoxedExpression e1, BoxedExpression e2)
        {
          if (e1 == null || e2 == null)
          {
            return false;
          }

          if (e1.Equals(e2))
          {
            return true;
          }

          UnaryOperator uop1, uop2;
          BoxedExpression inner1, inner2;
          if (e1.IsUnaryExpression(out uop1, out inner1) && e2.IsUnaryExpression(out uop2, out inner2) && uop1 == UnaryOperator.Not && uop1 == uop2)
          {
            BoxedExpression innerExp;
            if (inner1.IsVariable)
            {
              return inner2.IsExpressionNotEqualToNull(out innerExp) && ExtendedExpressionEquals(inner1, innerExp);
            }
            if (inner2.IsVariable)
            {
              return inner1.IsExpressionNotEqualToNull(out innerExp) && ExtendedExpressionEquals(inner2, innerExp);
            }
          }

          return false;
        }

        #endregion

        #region Overridden
        public override object Clone()
        {
          return new DisjunctiveRefinement(this);
        }

        protected override DisjunctiveRefinement Factory()
        {
          return new DisjunctiveRefinement(this.ExpressionManager);
        }

        public override List<BoxedVariable<Variable>> Variables
        {
          get
          {
            var result = new List<BoxedVariable<Variable>>();

            if (this.IsNormal())
            {
              foreach (var pair in this.Elements)
              {
                result.Add(pair.Key);
                if (pair.Value.IsNormal())
                {
                  foreach (var el in pair.Value.Values)
                  {
                    result.AddRange(el.Variables<Variable>().ConvertAll(v => new BoxedVariable<Variable>(v)));
                  }
                }
              }

            }
            return result;
          }
        }

        public override void Assign(BoxedExpression x, BoxedExpression exp)
        {
          // do nothing
        }

        public override void ProjectVariable(BoxedVariable<Variable> var)
        {
          this.RemoveVariable(var);
        }

        public override void RemoveVariable(BoxedVariable<Variable> var)
        {
          if (this.IsNormal())
          {
            if (this.ContainsKey(var))
            {
              this.RemoveElement(var);
            }
            else
            {
              var toUpdate = new List<Pair<BoxedVariable<Variable>, SetOfConstraints<BoxedExpression>>>();

              foreach (var pair in this.Elements)
              {
                if (pair.Value.IsNormal())
                {
                  var toRemove = new List<BoxedExpression>();

                  foreach (var el in pair.Value.Values)
                  {
                    if (el.Variables<Variable>().ConvertAll(v => new BoxedVariable<Variable>(v)).Contains(var))
                    {
                      toRemove.Add(el);
                    }
                  }

                  if (toRemove.Count > 0)
                  {
                    toUpdate.Add(pair.Key, new SetOfConstraints<BoxedExpression>(pair.Value.Values.Except(toRemove)));
                  }
                }
              }

              if (toUpdate.Count > 0)
              {
                foreach (var pair in toUpdate)
                {
                  this[pair.One] = pair.Two;
                }
              }

            }
          }
        }

        public override void RenameVariable(BoxedVariable<Variable> OldName, BoxedVariable<Variable> NewName)
        {
          throw new AbstractInterpretationTODOException();
        }

        public override DisjunctiveRefinement TestTrue(BoxedExpression guard)
        {
          return this;
        }

        public override DisjunctiveRefinement TestFalse(BoxedExpression guard)
        {
          return this;
        }

        public override FlatAbstractDomain<bool> CheckIfHolds(BoxedExpression exp)
        {
          return CheckOutcome.Top;
        }


        public override void AssignInParallel(
          Dictionary<BoxedVariable<Variable>, FList<BoxedVariable<Variable>>> sourcesToTargets,
          Converter<BoxedVariable<Variable>, BoxedExpression> convert)
        {
          throw new NotImplementedException();
        }

        public DisjunctiveRefinement AssignInParallelFunctional(
          Dictionary<BoxedVariable<Variable>, FList<BoxedVariable<Variable>>> sourcesToTargets,
          Converter<BoxedVariable<Variable>, BoxedExpression> convert)
        {
          // var varsInRenaming = ComputeVarsInRenaming(sourcesToTargets, convert);

          Dictionary<Variable, HashSet<Variable>> varsInRenaming = null; // We do not use them now, and as the cost of the call is too high, we just gave up

          if (this.IsNormal())
          {
            var result = this.Factory();

            foreach (var pair in this.Elements)
            {
              if (pair.Value.IsNormal())
              {
                var renamed = new List<BoxedExpression>();
                FList<BoxedVariable<Variable>> newNames;
                if (sourcesToTargets.TryGetValue(pair.Key, out newNames))
                {
                  foreach (var exp in pair.Value.Values)
                  {
                    renamed.AddIfNotNull(exp.Rename(sourcesToTargets));
                  }

                  if (renamed.Count > 0)
                  {
                    var newConstraints = new SetOfConstraints<BoxedExpression>(renamed);
                    foreach (var newName in newNames.GetEnumerable())
                    {
                      result[newName] = newConstraints;
                    }
                  }
                }
                else
                {
                  // do nothing -> the variable goes away
                }
              }
            }
            return new DisjunctiveRefinement(result, varsInRenaming);
          }
          else
          {
            return new DisjunctiveRefinement(this, varsInRenaming);
          }
        }

        private Dictionary<Variable, HashSet<Variable>> ComputeVarsInRenaming(Dictionary<BoxedVariable<Variable>, FList<BoxedVariable<Variable>>> sourcesToTargets, Converter<BoxedVariable<Variable>, BoxedExpression> convert)
        {
          var result = new Dictionary<Variable, HashSet<Variable>>();

          foreach(var pair in sourcesToTargets)
          {
            Variable source;
            if (pair.Key.TryUnpackVariable(out source))
            {

              var set = new HashSet<Variable>();
              foreach (var v in pair.Value.GetEnumerable())
              {
                set.UnionWith(convert(v).Variables<Variable>());
              }

              result[source] = set;
            }
          }

          return result;
        }

        #endregion
      }


      #endregion

    }
  }
}