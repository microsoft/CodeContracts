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
using Microsoft.Research.DataStructures;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Diagnostics;
using Microsoft.Research.AbstractDomains.Numerical;

namespace Microsoft.Research.CodeAnalysis
{
  using Postconditions = List<BoxedExpression>;
  using Preconditions = List<BoxedExpression>;
  using Microsoft.Research.AbstractDomains.Expressions;

  public static class BackwardsPostconditionPropagation
  {
    /// <summary>
    /// Preconditions inference by symbolic backwards propagation, as Sect.9 of [CCL-VMCAI11]
    /// </summary>
    public class PreconditionsInferenceBackwardSymbolic<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions>
      where Variable : IEquatable<Variable>
      where Expression : IEquatable<Expression>
      where Type : IEquatable<Type>
      where LogOptions : IFrameworkLogOptions
    {

      #region Constants

      private const int TIMEOUT = 2;

      #endregion

      #region Object Invariant

      [ContractInvariantMethod]
      private void ObjectInvariant()
      {
        Contract.Invariant(this.Facts != null);
        Contract.Invariant(this.MDriver != null);
      }

      #endregion

      #region State

      readonly private IFactQuery<BoxedExpression, Variable> Facts;
      readonly private IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions> MDriver;
      readonly TimeOutChecker timeout;

      #endregion

      #region Constructor

      public PreconditionsInferenceBackwardSymbolic(
        IFactQuery<BoxedExpression, Variable> facts,
        IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions> mdriver
      )
      {
        Contract.Requires(facts != null);
        Contract.Requires(mdriver != null);

        this.Facts = facts;
        this.MDriver = mdriver;
        this.timeout = new TimeOutChecker(TIMEOUT, false); // we do not start the timeout, because we want to do it only for effective computations
      }

      #endregion

      #region Public API

      public void SuggestContractsForExtractedMethod(Preconditions postConditionsAsBE, IOutput output)
      {
        IEnumerable<BoxedExpression> newPreconditions;
        if (this.TryInferPreconditionsFromPostconditions(postConditionsAsBE.ToList(), out newPreconditions))
        {
          var decompiler = new AnalysisWrapper.TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>.BooleanExpressionsDecompiler<LogOptions>(this.MDriver);

          var alreadyShown = new Set<string>();

          foreach (var p in newPreconditions)
          {
            var inPreState = PreconditionSuggestion.ExpressionInPreState(p, this.MDriver.Context, this.MDriver.MetaDataDecoder, this.MDriver.CFG.EntryAfterRequires, allowedKinds: ExpressionInPreStateKind.MethodPrecondition);
            BoxedExpression decompiled;
            if (inPreState != null && decompiler.FixIt(this.MDriver.CFG.EntryAfterRequires, inPreState.expr, out decompiled))
            {
              var outStr = decompiled.ToString();
              if (this.Facts.IsTrue(this.MDriver.CFG.EntryAfterRequires, decompiled) == ProofOutcome.Top && !alreadyShown.Contains(outStr))
              {
                output.Suggestion(ClousotSuggestion.Kind.Requires, ClousotSuggestion.Kind.Requires.Message(), 
                  this.MDriver.CFG.Entry, string.Format("Extract method suggestion: Contract.Requires({0});", outStr), null, ClousotSuggestion.ExtraSuggestionInfo.None);
                alreadyShown.Add(outStr);
              }
            }
          }
        }
      }

      public bool TryInferPreconditionsFromPostconditions(List<BoxedExpression> existingPost, out IEnumerable<BoxedExpression> preconditions)
      {
        Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out preconditions) != null);

        if (this.timeout.HasAlreadyTimeOut)
        {
          preconditions = null;
          return false;
        }

        var pc = this.MDriver.CFG.NormalExit;

        var backwardInference = new BackwardsPropagation(pc, this.Facts, this.MDriver, this.timeout);

        if (backwardInference.TryInferPreconditions(pc, existingPost.SyntacticReductionRemoval(removeChecksWithMinValue: true).ToList()))
        {
          preconditions = backwardInference.InferredPreconditions(this.Facts);
        }
        else
        {
          preconditions = null;
        }

        return preconditions != null;
      }

      #endregion

      #region Backwards Visit with fixpoint computation

      class BackwardsPropagation
        : IEdgeVisit<APC, Local, Parameter, Method, Field, Type, Variable, Preconditions>
      {
        #region object invariant

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
          Contract.Invariant(this.Mdriver != null);
          Contract.Invariant(this.CFG != null);
          Contract.Invariant(this.timeout != null);
          Contract.Invariant(this.joinPoints != null);
        }

        #endregion

        #region State

        readonly private APC pcCondition;
        readonly private IFactQuery<BoxedExpression, Variable> facts;
        readonly private IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions> Mdriver;
        readonly private ICFG CFG;
        readonly private TimeOutChecker timeout;
        private Preconditions invariants;

        readonly private Dictionary<APC, Preconditions> joinPoints;

        #endregion

        #region Constructor

        public BackwardsPropagation(
          APC pcCondition,
          IFactQuery<BoxedExpression, Variable> facts,
          IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions> mdriver,
          TimeOutChecker timeout)
        {
          Contract.Requires(mdriver != null);

          this.pcCondition = pcCondition;
          this.facts = facts;
          this.Mdriver = mdriver;
          this.CFG = this.Mdriver.StackLayer.Decoder.Context.MethodContext.CFG;
          this.timeout = timeout;
          this.joinPoints = new Dictionary<APC, Preconditions>();
        }

        #endregion

        #region Properties

        private bool TraceInference { get { return this.Mdriver.Options.TraceInference; } }

#if false
        private BoxedExpression Converter(APC pc, BoxedExpression exp)
        {
          var inPreState = PreconditionSuggestion.ExpressionInPreState(exp, this.Mdriver.Context, this.Mdriver.MetaDataDecoder, pc, allowedKinds: ExpressionInPreStateKind.All, isSufficient:false);
          return inPreState == null || !inPreState.hasVariables ? null : inPreState.expr;
        }
#endif
        public IEnumerable<BoxedExpression> InferredPreconditions(IFactQuery<BoxedExpression, Variable> facts)
        {
          var result = new List<BoxedExpression>();
          foreach (var pre in this.invariants)
          {
            switch (facts.IsTrue(this.Mdriver.CFG.EntryAfterRequires, pre))
            {
              case ProofOutcome.Bottom:
              case ProofOutcome.False:
                return null;

              case ProofOutcome.True:
                continue;

              case ProofOutcome.Top:
                result.Add(pre);
                break;
            }
          }

          return result.SyntacticReductionRemoval(removeChecksWithMinValue: true);
        }

        #endregion

        #region TryInferPreconditions

        public bool TryInferPreconditions(APC pc, Postconditions postconditions)
        {
          Trace("Starting backwards postcondition propagation", postconditions);

          if (this.timeout.HasAlreadyTimeOut)
          {
            return false;
          }

          try
          {
            this.timeout.Start();

            FixpointComputation(pc, postconditions);

            return this.invariants != null && this.invariants.Count != 0;
          }
          catch (TimeoutExceptionFixpointComputation)
          {
            return false;
          }
          finally
          {
            this.timeout.Stop();
          }
        }

        private void FixpointComputation(APC entry, Postconditions initialState)
        {
          var todo = new List<Tuple<APC, Preconditions>>() { new Tuple<APC, Preconditions>(entry, initialState) };
          var stable = new Set<APC>();

          while (todo.Count != 0)
          {
            this.timeout.CheckTimeOut();

            var next = todo.ExtractFirst();

            var nextPC = next.Item1;
            var nextState = next.Item2;

            var newPre = nextState;

            // do the block
            {
              APC pred;
              while (CFG.HasSinglePredecessor(nextPC, out pred) && !CFG.IsJoinPoint(nextPC))
              {
                this.timeout.CheckTimeOut();

                //var post = CFG.Predecessors(nextPC).First();
                newPre = this.Mdriver.BackwardTransfer(nextPC, pred, newPre, this);

                // no pre, killing the path
                if (newPre == null)
                {
                  break;
                }

                // TODO: check for contraddictions

                nextPC = pred;
              }
            }
            if (nextPC.Equals(CFG.EntryAfterRequires) || nextPC.Equals(CFG.Entry))
            {
              this.invariants = newPre;

              continue;
            }

            var fixpointReached = false;

            if (CFG.IsJoinPoint(nextPC))
            {
              Preconditions prev;
              if (this.joinPoints.TryGetValue(nextPC, out prev))
              {
                newPre = Join(prev, newPre, CFG.IsForwardBackEdgeTarget(nextPC.Block.First), out fixpointReached);

                if (fixpointReached)
                {
                  stable.Add(nextPC.Block.First);
                }
              }
              joinPoints[nextPC] = newPre;
            }

            // Split in the backwards analysis
            foreach (var pred in CFG.Predecessors(nextPC))
            {
              if (fixpointReached && CFG.IsForwardBackEdge(pred, nextPC))
              {
                continue;
              }
              if (this.Mdriver.IsUnreachable(nextPC)) continue;
              var transf = this.Mdriver.BackwardTransfer(nextPC, pred, newPre, this);
              if (transf != null)
              {
                todo.Add(new Tuple<APC, Preconditions>(pred, transf));
              }
            }
          }
        }

        #endregion

        #region Join

        /// <summary>
        /// Very simple join: We keep the stable constraints (syntactically, and we project on intervals)
        /// </summary>
        public Preconditions Join(Preconditions prev, Preconditions next, bool widen, out bool fixpointReached)
        {
          fixpointReached = false;

          if (prev == null)
            return next;

          if (next == null)
            return prev;

          var intersection = prev.Intersect(next).ToList();

          var factsFromJoin = JoinOnIntervals(prev, next, widen);

          if (factsFromJoin != null)
          {
            intersection.AddRange(factsFromJoin);
            return intersection;
          }
          else if (widen)
          {
            fixpointReached = true;
            return prev;
          }

          return intersection;
        }

        private Preconditions JoinOnIntervals(Preconditions prev, Preconditions next, bool widen)
        {
          Contract.Requires(prev != null);
          Contract.Requires(next != null);

          var expManager = this.GetExpressionManager();
          var intvLeft = new IntervalEnvironment<BoxedVariable<Variable>, BoxedExpression>(expManager);
          var intvRight = new IntervalEnvironment<BoxedVariable<Variable>, BoxedExpression>(expManager);

          intvLeft = Assume(intvLeft, prev);
          intvRight = Assume(intvRight, next);

          var result = widen ? intvRight.Widening(intvLeft) : intvRight.Join(intvLeft);

          if (widen && result.LessEqual(intvLeft))
            return null;

          return result.To(new BoxedExpressionFactory<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Variable>(this.Mdriver.MetaDataDecoder)).SplitConjunctions();
        }

        private ExpressionManager<BoxedVariable<Variable>, BoxedExpression> GetExpressionManager()
        {
          var valuedDecoder = new AnalysisWrapper.TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>.ValueExpDecoder(this.Mdriver.Context, this.Mdriver.MetaDataDecoder);
          var expDecoder = BoxedExpressionDecoder<Variable>.Decoder(valuedDecoder, (object o) => ExpressionType.Unknown);
          var expEncoder = BoxedExpressionEncoder<Variable>.Encoder(this.Mdriver.MetaDataDecoder, this.Mdriver.Context);
          var expManager = new ExpressionManagerWithEncoder<BoxedVariable<Variable>, BoxedExpression>(this.timeout, expDecoder, expEncoder);

          return expManager;
        }

        private IntervalEnvironment<BoxedVariable<Variable>, BoxedExpression> Assume(IntervalEnvironment<BoxedVariable<Variable>, BoxedExpression> intv, Preconditions pre)
        {
          Contract.Requires(intv != null);
          Contract.Requires(pre != null);

          foreach (var p in pre)
          {
            intv = intv.TestTrue(p);
          }

          return intv;
        }

        #endregion

        #region Transfer functions
        public Preconditions Rename(APC from, APC to, Preconditions pre, IFunctionalMap<Variable, Variable> renaming)
        {
          BreakHere(to, pre, "rename");

          Func<Variable, BoxedExpression> converter = ((Variable v) => BoxedExpression.Convert(this.Mdriver.Context.ExpressionContext.Refine(to, v), this.Mdriver.ExpressionDecoder));

          var result = new List<BoxedExpression>();

          foreach (var p in pre)
          {
            var newCondition = p.Rename(renaming, converter);

            if (newCondition != null)
            {
              var truth = facts.IsTrue(to, newCondition);
              switch (truth)
              {
                case ProofOutcome.Top:
                  result.Add(newCondition);
                  break;

                case ProofOutcome.True:
                  continue;

                case ProofOutcome.Bottom:
                case ProofOutcome.False:
                  return null;

              }
            }
          }

          return result.Count == 0 ? null : result;
        }

        public Preconditions Arglist(APC pc, Variable dest, Preconditions pre)
        {
          BreakHere(pc, pre); return pre;
        }

        public Preconditions BranchCond(APC pc, APC target, BranchOperator bop, Variable value1, Variable value2, Preconditions pre)
        {
          BreakHere(pc, pre); return pre;
        }

        public Preconditions BranchTrue(APC pc, APC target, Variable cond, Preconditions pre)
        {
          BreakHere(pc, pre); return pre;
        }

        public Preconditions BranchFalse(APC pc, APC target, Variable cond, Preconditions pre)
        {
          BreakHere(pc, pre); return pre;
        }

        public Preconditions Branch(APC pc, APC target, bool leave, Preconditions pre)
        {
          BreakHere(pc, pre); return pre;
        }

        public Preconditions Break(APC pc, Preconditions pre)
        {
          BreakHere(pc, pre); return pre;
        }

        public Preconditions Call<TypeList, ArgList>(APC pc, Method method, bool tail, bool virt, TypeList extraVarargs, Variable dest, ArgList args, Preconditions pre)
          where TypeList : IIndexable<Type>
          where ArgList : IIndexable<Variable>
        {
          BreakHere(pc, pre); return pre;
        }

        public Preconditions Calli<TypeList, ArgList>(APC pc, Type returnType, TypeList argTypes, bool tail, bool instance, Variable dest, Variable fp, ArgList args, Preconditions pre)
          where TypeList : IIndexable<Type>
          where ArgList : IIndexable<Variable>
        {
          BreakHere(pc, pre); return pre;
        }

        public Preconditions Ckfinite(APC pc, Variable dest, Variable source, Preconditions pre)
        {
          BreakHere(pc, pre); return pre;
        }

        public Preconditions Cpblk(APC pc, bool @volatile, Variable destaddr, Variable srcaddr, Variable len, Preconditions pre)
        {
          BreakHere(pc, pre); return pre;
        }

        public Preconditions Endfilter(APC pc, Variable decision, Preconditions pre)
        {
          BreakHere(pc, pre); return pre;
        }

        public Preconditions Endfinally(APC pc, Preconditions pre)
        {
          BreakHere(pc, pre); return pre;
        }

        public Preconditions Initblk(APC pc, bool @volatile, Variable destaddr, Variable value, Variable len, Preconditions pre)
        {
          BreakHere(pc, pre); return pre;
        }

        public Preconditions Jmp(APC pc, Method method, Preconditions pre)
        {
          BreakHere(pc, pre); return pre;
        }

        public Preconditions Ldarg(APC pc, Parameter argument, bool isOld, Variable dest, Preconditions pre)
        {
          BreakHere(pc, pre); return pre;
        }

        public Preconditions Ldarga(APC pc, Parameter argument, bool isOld, Variable dest, Preconditions pre)
        {
          BreakHere(pc, pre); return pre;
        }

        public Preconditions Ldftn(APC pc, Method method, Variable dest, Preconditions pre)
        {
          BreakHere(pc, pre); return pre;
        }

        public Preconditions Ldind(APC pc, Type type, bool @volatile, Variable dest, Variable ptr, Preconditions pre)
        {
          BreakHere(pc, pre); return pre;
        }

        public Preconditions Ldloc(APC pc, Local local, Variable dest, Preconditions pre)
        {
          BreakHere(pc, pre); return pre;
        }

        public Preconditions Ldloca(APC pc, Local local, Variable dest, Preconditions pre)
        {
          BreakHere(pc, pre); return pre;
        }

        public Preconditions Localloc(APC pc, Variable dest, Variable size, Preconditions pre)
        {
          BreakHere(pc, pre); return pre;
        }

        public Preconditions Nop(APC pc, Preconditions pre)
        {
          BreakHere(pc, pre); return pre;
        }

        public Preconditions Pop(APC pc, Variable source, Preconditions pre)
        {
          BreakHere(pc, pre); return pre;
        }

        public Preconditions Return(APC pc, Variable source, Preconditions pre)
        {
          BreakHere(pc, pre); return pre;
        }

        public Preconditions Starg(APC pc, Parameter argument, Variable source, Preconditions pre)
        {
          // When we write in an argument, then, in the backwards analysis its value is lost, so we havoc it
          BreakHere(pc, pre);

          if (pre != null)
          {
            pre = pre.Map(exp => exp.Variables<Variable>().Contains(source) ? null : exp).GetEnumerable().ToList();
            pre.RemoveAll(exp => exp == null);
            if (pre.Count == 0)
              return null;
          }
          return pre;
        }

        public Preconditions Stind(APC pc, Type type, bool @volatile, Variable ptr, Variable value, Preconditions pre)
        {
          BreakHere(pc, pre); return pre;
        }

        public Preconditions Stloc(APC pc, Local local, Variable source, Preconditions pre)
        {
          BreakHere(pc, pre); return pre;
        }

        public Preconditions Switch(APC pc, Type type, IEnumerable<Pair<object, APC>> cases, Variable value, Preconditions pre)
        {
          BreakHere(pc, pre); return pre;
        }

        public Preconditions Box(APC pc, Type type, Variable dest, Variable source, Preconditions pre)
        {
          BreakHere(pc, pre); return pre;
        }

        public Preconditions ConstrainedCallvirt<TypeList, ArgList>(APC pc, Method method, bool tail, Type constraint, TypeList extraVarargs, Variable dest, ArgList args, Preconditions pre)
          where TypeList : IIndexable<Type>
          where ArgList : IIndexable<Variable>
        {
          BreakHere(pc, pre); return pre;
        }

        public Preconditions Castclass(APC pc, Type type, Variable dest, Variable obj, Preconditions pre)
        {
          BreakHere(pc, pre); return pre;
        }

        public Preconditions Cpobj(APC pc, Type type, Variable destptr, Variable srcptr, Preconditions pre)
        {
          BreakHere(pc, pre); return pre;
        }

        public Preconditions Initobj(APC pc, Type type, Variable ptr, Preconditions pre)
        {
          BreakHere(pc, pre); return pre;
        }

        public Preconditions Ldelem(APC pc, Type type, Variable dest, Variable array, Variable index, Preconditions pre)
        {
          BreakHere(pc, pre);

          if (pre != null)
          {
            var result = new Preconditions();

            var ldElemExp = new BoxedExpression.ArrayIndexExpression<Type>(BoxedExpression.Var(array), BoxedExpression.Var(index), type);
            var destExp = BoxedExpression.Var(dest);

            foreach (var p in pre)
            {
              if (p != null)
              {
                result.AddIfNotNull(p.Substitute(destExp, ldElemExp));
              }
            }
            return result;
          }
          else
          {
            return pre;
          }
        }

        public Preconditions Ldelema(APC pc, Type type, bool @readonly, Variable dest, Variable array, Variable index, Preconditions pre)
        {
          BreakHere(pc, pre); return pre;
        }

        public Preconditions Ldfld(APC pc, Field field, bool @volatile, Variable dest, Variable obj, Preconditions pre)
        {
          BreakHere(pc, pre); return pre;
        }

        public Preconditions Ldflda(APC pc, Field field, Variable dest, Variable obj, Preconditions pre)
        {
          BreakHere(pc, pre); return pre;
        }

        public Preconditions Ldlen(APC pc, Variable dest, Variable array, Preconditions pre)
        {
          BreakHere(pc, pre); return pre;
        }

        public Preconditions Ldsfld(APC pc, Field field, bool @volatile, Variable dest, Preconditions pre)
        {
          BreakHere(pc, pre); return pre;
        }

        public Preconditions Ldsflda(APC pc, Field field, Variable dest, Preconditions pre)
        {
          BreakHere(pc, pre); return pre;
        }

        public Preconditions Ldtypetoken(APC pc, Type type, Variable dest, Preconditions pre)
        {
          BreakHere(pc, pre); return pre;
        }

        public Preconditions Ldfieldtoken(APC pc, Field field, Variable dest, Preconditions pre)
        {
          BreakHere(pc, pre); return pre;
        }

        public Preconditions Ldmethodtoken(APC pc, Method method, Variable dest, Preconditions pre)
        {
          BreakHere(pc, pre); return pre;
        }

        public Preconditions Ldvirtftn(APC pc, Method method, Variable dest, Variable obj, Preconditions pre)
        {
          BreakHere(pc, pre); return pre;
        }

        public Preconditions Mkrefany(APC pc, Type type, Variable dest, Variable obj, Preconditions pre)
        {
          BreakHere(pc, pre); return pre;
        }

        public Preconditions Newarray<ArgList>(APC pc, Type type, Variable dest, ArgList len, Preconditions pre) where ArgList : IIndexable<Variable>
        {
          BreakHere(pc, pre); return pre;
        }

        public Preconditions Newobj<ArgList>(APC pc, Method ctor, Variable dest, ArgList args, Preconditions pre) where ArgList : IIndexable<Variable>
        {
          BreakHere(pc, pre); return pre;
        }

        public Preconditions Refanytype(APC pc, Variable dest, Variable source, Preconditions pre)
        {
          BreakHere(pc, pre); return pre;
        }

        public Preconditions Refanyval(APC pc, Type type, Variable dest, Variable source, Preconditions pre)
        {
          BreakHere(pc, pre); return pre;
        }

        public Preconditions Rethrow(APC pc, Preconditions pre)
        {
          BreakHere(pc, pre); return pre;
        }

        public Preconditions Stelem(APC pc, Type type, Variable array, Variable index, Variable value, Preconditions pre)
        {
          BreakHere(pc, pre); return pre;
        }

        public Preconditions Stfld(APC pc, Field field, bool @volatile, Variable obj, Variable value, Preconditions pre)
        {
          BreakHere(pc, pre); return pre;
        }

        public Preconditions Stsfld(APC pc, Field field, bool @volatile, Variable value, Preconditions pre)
        {
          BreakHere(pc, pre); return pre;
        }

        public Preconditions Throw(APC pc, Variable exn, Preconditions pre)
        {
          BreakHere(pc, pre); return pre;
        }

        public Preconditions Unbox(APC pc, Type type, Variable dest, Variable obj, Preconditions pre)
        {
          BreakHere(pc, pre); return pre;
        }

        public Preconditions Unboxany(APC pc, Type type, Variable dest, Variable obj, Preconditions pre)
        {
          BreakHere(pc, pre); return pre;
        }

        public Preconditions Entry(APC pc, Method method, Preconditions pre)
        {
          BreakHere(pc, pre); return pre;
        }

        public Preconditions Assume(APC pc, string tag, Variable condition, object provenance, Preconditions pre)
        {
          BreakHere(pc, pre, "assume " + tag);

          return pre;
        }

        public Preconditions Assert(APC pc, string tag, Variable condition, object provenance, Preconditions pre)
        {
          BreakHere(pc, pre, "assert");

          return pre;
        }

        public Preconditions Ldstack(APC pc, int offset, Variable dest, Variable source, bool isOld, Preconditions pre)
        {
          BreakHere(pc, pre); return pre;
        }

        public Preconditions Ldstacka(APC pc, int offset, Variable dest, Variable source, Type origParamType, bool isOld, Preconditions pre)
        {
          BreakHere(pc, pre); return pre;
        }

        public Preconditions Ldresult(APC pc, Type type, Variable dest, Variable source, Preconditions pre)
        {
          BreakHere(pc, pre); return pre;
        }

        public Preconditions BeginOld(APC pc, APC matchingEnd, Preconditions pre)
        {
          BreakHere(pc, pre); return pre;
        }

        public Preconditions EndOld(APC pc, APC matchingBegin, Type type, Variable dest, Variable source, Preconditions pre)
        {
          BreakHere(pc, pre); return pre;
        }

        public Preconditions Binary(APC pc, BinaryOperator op, Variable dest, Variable s1, Variable s2, Preconditions pre)
        {
          BreakHere(pc, pre); return pre;
        }

        public Preconditions Isinst(APC pc, Type type, Variable dest, Variable obj, Preconditions pre)
        {
          BreakHere(pc, pre); return pre;
        }

        public Preconditions Ldconst(APC pc, object constant, Type type, Variable dest, Preconditions pre)
        {
          BreakHere(pc, pre); return pre;
        }

        public Preconditions Ldnull(APC pc, Variable dest, Preconditions pre)
        {
          BreakHere(pc, pre); return pre;
        }

        public Preconditions Sizeof(APC pc, Type type, Variable dest, Preconditions pre)
        {
          BreakHere(pc, pre); return pre;
        }

        public Preconditions Unary(APC pc, UnaryOperator op, bool overflow, bool unsigned, Variable dest, Variable source, Preconditions pre)
        {
          BreakHere(pc, pre); return pre;
        }

        #region Private helpers

        [Conditional("DEBUG")]
        protected void BreakHere(APC pc, List<BoxedExpression> pre, string instr = null)
        {
          if (this.TraceInference)
          {
            try
            {
              Console.WriteLine("Visiting: {0}-{1} with {2}", pc.ToString(), instr, pre != null ? ToString(pre).ToString() : "-- no Preconditions --");
            }
            catch(FormatException)
            {
              Console.WriteLine("< Hit a format exception in Console.Writeline >");
            }
          }
        }

        [Conditional("DEBUG")]
        protected void Trace(string s, List<BoxedExpression> pre)
        {
          if (this.TraceInference)
          {
            try
            {
              if (pre != null)
              {
                Console.WriteLine(s + ": " + ToString(pre).ToString());
              }
              else
              {
                Console.WriteLine(s + " -- no Preconditions --");
              }
            }
            catch (FormatException)
            {
              Console.WriteLine("< Hit a format exception in Console.Writeline >");
            }
          }
        }

        [Pure]
        private StringBuilder ToString(List<BoxedExpression> pre)
        {
          var buff = new StringBuilder();

          if (pre == null)
            return buff;

          foreach (var exp in pre)
          {
            buff.AppendFormat("{0}; ", exp.ToString());
          }

          return buff;
        }

        #endregion
        #endregion

        #region Tracing

        [Conditional("DEBUG")]
        private void Trace(APC pc, string s, List<BoxedExpression> state)
        {
          if (this.Mdriver.Options.TraceDFA)
          {
            Console.WriteLine("{0}: {1} ==> {2}", pc, s, state);
          }
        }

        #endregion
      }

      #endregion
    }
  }
}
