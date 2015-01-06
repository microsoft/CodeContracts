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
using System.Diagnostics.Contracts;
using System.Collections.Generic;
using Microsoft.Research.DataStructures;
using System.Diagnostics;
using System.Text;
using System.Linq;

namespace Microsoft.Research.CodeAnalysis
{

  /// <summary>
  /// Precondition inference by symbolic backwards propagation, as Sect.9 of [CCL-VMCAI11]
  /// </summary>
  public class PreconditionInferenceBackwardSymbolic<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions>
    : IPreconditionInference
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
    public bool ShouldAddAssumeFalse { private set; get; }

    #endregion

    #region Constructor

    public PreconditionInferenceBackwardSymbolic(
      IFactQuery<BoxedExpression, Variable> facts,
      IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions> mdriver
    )
    {
      Contract.Requires(facts != null);
      Contract.Requires(mdriver != null);

      this.Facts = facts;
      this.MDriver = mdriver;
      this.ShouldAddAssumeFalse = false;
      this.timeout = new TimeOutChecker(TIMEOUT, false); // we do not start the timeout, because we want to do it only for effective computations
    }

    #endregion

    #region Entry point to try to infer a precondition

    public bool TryInferConditions(ProofObligation obl, ICodeFixesManager codefixesManager, out InferredConditions preConditions)
    {
      preConditions = null;

      if (this.timeout.HasAlreadyTimeOut)
      {
        return false;
      }

      var backwardInference = new BackwardsPropagation(obl, obl.PC, codefixesManager, this.Facts, this.MDriver, this.timeout);

      // Do not try to suggest a fix for a requires
      if (obl.ObligationName != "requires")
      {
        var context = new ParametersSuggestOffByOneFix<Variable>(obl, obl.PC, pc => pc.GetFirstPredecessorWithSourceContext(this.MDriver.CFG),
          obl.ObligationName == "ArrayUpperBoundAccess", // special treatement for array upper bounds, as we do not want to introduce an underflow
          obl.Condition,
         (Variable var) => this.MDriver.Context.ValueContext.AccessPathList(obl.PC, var, true, true),
         expr => expr.IsArrayLength(obl.PC, this.MDriver.Context, this.MDriver.MetaDataDecoder), this.Facts);

        codefixesManager.TrySuggestOffByOneFix(ref context);
      }

      // always do it.
      backwardInference.TryInferPrecondition(obl.PC, new Precondition(null as BoxedExpression, obl.ConditionForPreconditionInference));

      var preconditions = backwardInference.InferredConditions(this.Facts, !backwardInference.LoopHit);

      // record if we found a condition with all enums
      obl.InferredConditionContainsOnlyEnumValues = backwardInference.InferredConditionContainsEnum;

      if (preconditions == null)
      {
        preConditions = null;
      }
      else
      {
        //var vContext = this.MDriver.Context.ValueContext;
        //var preconditionsWithKind = preconditions.Where(be => be != null).Select(be => new SimpleInferredPrecondition(be, be.GuessExpressionInPreStateKind(obl.PC, vContext, path => this.MDriver.MetaDataDecoder.IsReadOnly(path)), !backwardInference.LoopHit));
        preConditions = new InferredConditions(preconditions);
      }

      // Suggest test strenghtening only if we did not inferred any precondition
      // It may be the case that the test fix is better than the precondition (eg. precondition: 0 < a.Length but test i < a.Length). 
      // Our catch is to give priority to preconditions in this case: if the programmer fix the program with the precodnition, and then run Clousot, then it will get the test fix anyway
      if (preConditions == null || !preConditions.Any() || (backwardInference.TestFix != null && backwardInference.TestFix.StrengthenNullCheck))
      {
        if (this.MDriver.Options.SufficientConditions)
        {
          this.ShouldAddAssumeFalse = true;
        }

        if (!backwardInference.AlreadyFoundAFix && backwardInference.TestFix != null)
        {
          var dummy = codefixesManager.TrySuggestTestStrengthening(obl, backwardInference.TestFix.pc, backwardInference.TestFix.Fix, backwardInference.TestFix.StrengthenNullCheck);
        }
      }

      return preConditions != null && preConditions.Any();
    }

    #endregion

    #region Backwards Visit with fixpoint computation

    class BackwardsPropagation
      : GenericNecessaryConditionsGenerator<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions, Precondition>
    {

    #endregion

      #region State

      readonly private ICodeFixesManager CodeFixes;
      readonly private ProofObligation obl;
      readonly private List<BoxedExpression> conditionsWithFixes;
      readonly private InvariantDB invariants;

      internal AdditionalTest TestFix { private set; get; }
      internal bool AlreadyFoundAFix { private set; get; }  // True if we found a fix different from adding a conjunct to the inner-most test
      internal bool InferredConditionContainsEnum { private set; get; } // True if we found a condition containing only enums

      #endregion

      #region Constructor

      public BackwardsPropagation(
        ProofObligation obl,
        APC pcCondition,
        ICodeFixesManager codefixesManager,
        IFactQuery<BoxedExpression, Variable> facts,
        IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions> mdriver,
        TimeOutChecker timeout)
        : base(pcCondition, facts, mdriver, timeout)
      {
        Contract.Requires(obl != null);
        Contract.Requires(mdriver != null);

        this.obl = obl;
        this.invariants = new InvariantDB(CFG.Entry);
        this.CodeFixes = codefixesManager;
        this.TestFix = null; // for readability
        this.AlreadyFoundAFix = false;
        this.conditionsWithFixes = new List<BoxedExpression>();
      }

      #endregion

      #region Properties

      public IEnumerable<IInferredCondition> InferredConditions(IFactQuery<BoxedExpression, Variable> facts, bool isSufficient)
      {
        var conditions = new List<IInferredCondition>();

        var mdd = this.Mdriver.MetaDataDecoder;
        var context = this.Mdriver.Context.ValueContext;

        foreach (var candidate in this.invariants.PreconditionAtEntryPoint)
        {
          if (!candidate.IsNone)
          {
            ExpressionInPreStateKind kind;
            // TODO: why are we converting again. Seems to do the work all over again, and does not know whether we allow objectinvariants.
            var candidate1 = candidate.Convert(exp => Converter(CFG.EntryAfterRequires, exp), out kind);

            Predicate<BoxedExpression> IsEnum = exp =>
              {
                Variable sv;
                if (exp.TryGetFrameworkVariable(out sv))
                {
                  var typeAtEntry = context.GetType(CFG.EntryAfterRequires, sv);
                  return typeAtEntry.IsNormal && mdd.IsEnumWithoutFlagAttribute(typeAtEntry.Value);
                }

                return false;
              };

            Func<BoxedExpression, FlatDomain<Type>> GetType = exp =>
              {
                Variable sv;
                if (exp.TryGetFrameworkVariable(out sv))
                {
                  return context.GetType(CFG.EntryAfterRequires, sv);
                }

                return FlatDomain<Type>.TopValue;
              };

            Func<BoxedExpression, ProofOutcome> checker = exp => facts.IsTrue(CFG.EntryAfterRequires, exp);
            //Func<BoxedExpression, ProofOutcome> checker = exp => facts.IsTrue(CFG.EntryAfterRequires, exp);

            if (!candidate1.IsNone)
            {
              if (conditions.AddIfNotNull(
                candidate1.SimplifyPremises(checker, IsEnumType, MakeInt).SimplifyCondition(checker, this.SatisfyProcedure, IsEnumType, MakeInt).ToInferredCondition(mdd, kind, isSufficient, GetType)))
              {
                continue;
              }
            }

            var candidate2 = candidate.Convert(exp => Converter(this.pcCondition, exp), out kind);

            if (!candidate2.IsNone)
            {
              if (conditions.AddIfNotNull(
                candidate2.SimplifyPremises(checker, IsEnumType, MakeInt).SimplifyCondition(checker, this.SatisfyProcedure, IsEnumType, MakeInt).ToInferredCondition(mdd, kind, isSufficient, GetType)))
              {
                continue;
              }
            }

            // Let's see if we have some precondition implied by the current one

//            Variable underlyingVariable;
            BinaryOperator bop;
            BoxedExpression l, r;
            if (candidate.Condition.IsBinaryExpression(out bop, out l, out r))
            {
              foreach (var fact in candidate.KnownFacts)
              {
                if (fact.IsBinary && fact.BinaryOp == BinaryOperator.Ceq)
                {
                  AddANewConditionIfPossible(l, fact.BinaryLeft, fact.BinaryRight, isSufficient, conditions, candidate, kind, IsEnum, GetType, checker);
                  AddANewConditionIfPossible(l, fact.BinaryRight, fact.BinaryLeft, isSufficient, conditions, candidate, kind, IsEnum, GetType, checker);
                  AddANewConditionIfPossible(r, fact.BinaryLeft, fact.BinaryRight, isSufficient, conditions, candidate, kind, IsEnum, GetType, checker);
                  AddANewConditionIfPossible(r, fact.BinaryRight, fact.BinaryLeft, isSufficient, conditions, candidate, kind, IsEnum, GetType, checker);
                }
              }
            }
            // this hack is definitely wrong. So we delete it
              /*
            else if(candidate.Condition.TryGetFrameworkVariable(out underlyingVariable) && candidate.Premises != null && candidate.Premises.Any())
            {
              // HACK HACK: If we find that a ==> v, and v is a variable, then we want to emit the precondition "a"

              //              var type = context.GetType(CFG.EntryAfterRequires, underlyingVariable);
              //              if (type.IsNormal && mdd.System_Boolean.Equals(type.Value))
              {
                var newCandidate = new Precondition(candidate.Premises, BoxedExpression.ConstFalse, candidate.KnownFacts);
                conditions.AddIfNotNull(
                  newCandidate
                  .SimplifyPremises(checker, IsEnumType, MakeInt)
                  .SimplifyCondition(checker, this.SatisfyProcedure, IsEnumType, MakeInt)
                  .ToInferredCondition(mdd, kind, isSufficient, GetType)
                  );
              }
            }*/
          }
        }

        ProduceCalleeAssumes(conditions);
        return conditions.Where(
          cond => cond.Expr != null /* should always be true, but double checking is not bad after all ... */
            && !cond.Expr.IsConstantTrue() /* avoid suggesting true */
            && cond.Expr.CanPrintAsSourceCode()
            );
      }

      private void AddANewConditionIfPossible(BoxedExpression original, BoxedExpression fact, BoxedExpression other, bool isSufficient, List<IInferredCondition> conditions, Precondition candidate, ExpressionInPreStateKind kind, Predicate<BoxedExpression> IsEnum, Func<BoxedExpression, FlatDomain<Type>> GetType, Func<BoxedExpression, ProofOutcome> checker)
      {
        if (original.UnderlyingVariable != null && original.UnderlyingVariable.Equals(fact.UnderlyingVariable))
        {
          var newCandidate = new Precondition(candidate.Premises, candidate.Condition.Substitute(original, other), candidate.KnownFacts);
          conditions.AddIfNotNull(newCandidate.SimplifyPremises(checker, IsEnumType, MakeInt).SimplifyCondition(checker, this.SatisfyProcedure, IsEnumType, MakeInt).ToInferredCondition(this.Mdriver.MetaDataDecoder, kind, isSufficient, GetType));
        }
      }

      #endregion

      #region TryInferPrecondition

      public bool TryInferPrecondition(APC pc, Precondition pre)
      {
        Trace("\nStarting backwards precondition propagation", pre);

        if (this.timeout.HasAlreadyTimeOut)
        {
          return false;
        }

        try
        {
          this.timeout.Start();

          Visit(pc, pre, 0);

          return this.invariants.PreconditionAtEntryPoint.Count != 0;
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

      protected override bool ShoudStopTheVisit(APC pc, Precondition pre, int depth, out APC nextPC, out Precondition nextCondition)
      {
        nextPC = default(APC);
        nextCondition = default(Precondition);

        if (Checks(pc, pre, depth))
        {
          return true;
        }

        // Otherwise we should propagate the precondition backwards, and compute the fixpoint

        nextPC = VisitBlock(pc, pre, out nextCondition);

        if (nextCondition.IsNone || Checks(nextPC, nextCondition, depth))
        {
          // we are done
          return true;
        }

        return false;
      }

      private bool Checks(APC pc, Precondition pre, int depth)
      {
        Contract.Requires(depth >= 0);

        if (WentTooFar(depth, pre))
        {
          return true;
        }

        // we do not have any candidate precondition, so we simply stop here
        if (pre.IsNone)
        {
          Trace("Found empty precondition. Killing the path", pre);
          return true;
        }

        // If we reached the entry point, then we are done
        if (CFG.Entry.Equals(pc))
        {
          Trace("Reached method entry with candidate precondition", pre);

          if (pre.Premises != null && pre.Premises.Contains(this.False))
          {
            Trace("  The premises of the candidate precondition contain the constant false, so we discard it", pre);

            return false;
          }


          Trace("  Adding the candidate precondition to the set of candidates");
          // we are done
          this.invariants.Add(pc, pre);

          if (CanBeAnEnum(pc, pre))
          {
            Trace("  Found a necessary condition involving only enum values");
            this.InferredConditionContainsEnum = true;
          }
          return true;
        }

        return false;
      }

      override protected Precondition NoCondition { get { return Precondition.None; } }

      protected override bool IsNoCondition(Precondition el) { return el.IsNone; }

      #endregion

      #region Transfer functions
      override public Precondition Rename(APC from, APC to, Precondition pre, IFunctionalMap<Variable, Variable> renaming)
      {
        Contract.Ensures(Contract.Result<Precondition>().IsNone || Contract.Result<Precondition>().Condition != null);
        BreakHere(to, pre, "rename");

        Func<Variable, BoxedExpression> converter = ((Variable v) => BoxedExpression.Convert(this.Mdriver.Context.ExpressionContext.Refine(to, v), this.Mdriver.ExpressionDecoder));

        var newPremise = pre.HasEmptyPremises ? null : pre.Premises.ConvertAllDroppingNulls(exp => exp.Rename(renaming, converter));

        // This check is because the renaming may have killed some premise, and hence the precondition inference is no longer valid
        if (newPremise != null && newPremise.Count < pre.Premises.Count)
        {
          Trace("We lost a premise, killing the path", new Precondition(newPremise, pre.Condition));

          return Precondition.None;
        }

        var newCondition = pre.Condition.Rename(renaming, converter);
        var newKnownFacts = pre.KnownFacts != null ? pre.KnownFacts.ConvertAllDroppingNulls(exp => exp.Rename(renaming, converter)) : null;

        Func<Variable, BoxedExpression> VariableName =
          (Variable v) =>
          {
            var path = this.Mdriver.Context.ValueContext.AccessPathList(from, v, true, false);
            if (path != null)
            {
              return BoxedExpression.Var(v, path);
            }

            return null;
          };

        if (newPremise != null && newCondition != null)
        {
          bool contradiction = false;
          newPremise = newPremise.ConvertAllDroppingNulls(exp => RemoveVacuousPremiseAndCheckForContradiction(to, exp, ref contradiction));
          if (contradiction)
          {
            // Just a shortcut, to avoid calling the fact query that can be expensive later
            Trace("Premises are false, and we try to get a code fix", new Precondition(newPremise, pre.Condition));

            // Try to get a code fix
            this.AlreadyFoundAFix = this.AlreadyFoundAFix || this.CodeFixes.TrySuggestConstantInitializationFix(this.obl, () => to.GetFirstPredecessorWithSourceContext(this.Mdriver.CFG), pre.Condition, newCondition, this.Mdriver.MetaDataDecoder, (Variable v) => converter(renaming[v]), VariableName);

            return Precondition.None;
          }
        }

        if (newCondition != null)
        {
          // We are propagating false
          if (newCondition.IsConstantFalse())
          {
            var result = new Precondition(newPremise, newCondition, newKnownFacts);

            Trace("The new condition, after renaming is the constant false", result);

            // Try to get a code fix
            this.AlreadyFoundAFix = this.AlreadyFoundAFix || this.CodeFixes.TrySuggestConstantInitializationFix(this.obl, () => to.GetFirstPredecessorWithSourceContext(this.Mdriver.CFG), pre.Condition, newCondition, this.Mdriver.MetaDataDecoder, (Variable v) => converter(renaming[v]), VariableName);

            return result;
          }

          var outcome = facts.IsTrue(to, newCondition);
          switch (outcome)
          {
            case ProofOutcome.Top:
              {
                return new Precondition(newPremise, newCondition, newKnownFacts);
              }

            case ProofOutcome.False:
              {
                var falseCond = BoxedExpression.ConstBool(0, this.Mdriver.MetaDataDecoder);

                // Try to get a code fix
                this.AlreadyFoundAFix = this.AlreadyFoundAFix || this.CodeFixes.TrySuggestConstantInitializationFix(this.obl, () => to.GetFirstPredecessorWithSourceContext(this.Mdriver.CFG), pre.Condition, newCondition, this.Mdriver.MetaDataDecoder, (Variable v) => converter(renaming[v]), VariableName);

                // Only constants
                if (!newCondition.Variables<Variable>().Any())
                {
                  var result = new Precondition(newPremise, falseCond, newKnownFacts);

                  Trace("The condition is false after renaming (" + newCondition.ToString() + "), and it is made only of constants", result);

                  return result;
                }
                else
                {
                  Trace("The new condition is false after renaming", new Precondition(newPremise, newCondition));
                }
                return new Precondition(newPremise, falseCond, newKnownFacts);
              }

            case ProofOutcome.True:
              {
                // We do an induction step (widening?) here. 
                // 1. We know we are in a rename, and 
                // 2. if we have "i {<, <=} b == > i < c", then (very likely) we are at a for loop head, and the loop test is i {<, <= } b
                // Then, it should be that b <= c (otherwise if c > b we would never execute the loop too many times)
                // TODO: The fact that "b <= c" is an invariant, it should be proven by a further iteration, but we are not doing it now
                BinaryOperator bopNewCondition, bopNewPremise;
                BoxedExpression newConditionLeft, newConditionRight, newPremiseLeft, newPremiseRight;
                if (newPremise != null && newPremise.Count == 1
                  && newCondition.IsBinaryExpression(out bopNewCondition, out newConditionLeft, out newConditionRight)
                  && newPremise[0].IsBinaryExpression(out bopNewPremise, out newPremiseLeft, out newPremiseRight))
                {
                  Normalize(ref bopNewCondition, ref newConditionLeft, ref newConditionRight);
                  Normalize(ref bopNewPremise, ref newPremiseLeft, ref newPremiseRight);

                  if (bopNewCondition == BinaryOperator.Clt)
                  {
                    if ((bopNewPremise == BinaryOperator.Clt || bopNewPremise == BinaryOperator.Cle)
                      && newConditionLeft.Equals(newPremiseLeft))
                    {
                      return new Precondition(new List<BoxedExpression>(), BoxedExpression.Binary(BinaryOperator.Cle, newPremiseRight, newConditionRight), pre.KnownFacts);
                    }
                  }
                }

                Trace("The new condition is true after renaming", new Precondition(newPremise, newCondition));

                return Precondition.None;
              }

            case ProofOutcome.Bottom:
              {
                Trace("The new condition is bottom after renaming", new Precondition(newPremise, newCondition));
                return Precondition.None;
              }

            default:
              {
                Contract.Assert(false, "Impossible case");
                break;
              }
          }
        }
        Trace("The new condition is null after renaming", Precondition.None);
        return Precondition.None;
      }

      override public Precondition Call<TypeList, ArgList>(APC pc, Method method, bool tail, bool virt, TypeList extraVarargs, Variable dest, ArgList args, Precondition pre)
      //where TypeList : IIndexable<Type>
      // where ArgList : IIndexable<Variable>
      {
        BreakHere(pc, pre);

        Trace(string.Format("  Hit a call of {0} with {1}", Mdriver.MetaDataDecoder.Name(method), pre));

        var mdDecoder = this.Mdriver.MetaDataDecoder;

        this.RecordCalleeAssumptionCandidate(pc, method, pre);

        // F: We are forgetting the path condition here!!! So the inferred code fix in general is only is sufficient
        if (this.CodeFixes.IsEnabled && !this.AlreadyFoundAFix && pre.Condition != null)
        {
          var valueContext = this.Mdriver.Context.ValueContext;

          var failingCondition = pre.Condition.Simplify(mdDecoder, ExtraSimplification: exp => new SimpleSatisfyProcedure<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(mdDecoder).ApplySimpleArithmeticRules(exp));

          Func<APC> pcWithSourceContext = () => pc.GetFirstPredecessorWithSourceContext(this.CFG);

          var context = new ParametersFixMethodCallReturnValue<Variable, ArgList, Method>(this.obl, pc, pcWithSourceContext, dest, args, failingCondition, pre.Premises,
            method,
            mdDecoder,
            Mdriver.CFG,
            (APC at, BoxedExpression b, bool allowReturnValue) => this.mutator.ReadAt(at, b, true, allowReturnValue ? this.Mdriver.MetaDataDecoder.ReturnType(method) : default(Type)),
            (APC where, Variable var) => valueContext.AccessPathList(where, var, true, true),
            valueContext.IsRootedInParameter,
            (Variable var, FList<PathElement> accessPath) => MakeMethodCallExpression(method, var, accessPath),
            (Variable var) => MakeZeroExp(pc, var));

          if (this.CodeFixes.TrySuggestFixForMethodCallReturnValue(ref context))
          {
            this.AlreadyFoundAFix = true;
          }

#if false // It does not really works for out parameters
          else
          {
            var formalParams = this.Mdriver.MetaDataDecoder.Parameters(method);
            var conditionVars = pre.Condition.Variables<Variable>();
            for (var i = 0; i < formalParams.Count; i++)
            {
              if (this.Mdriver.MetaDataDecoder.IsOut(formalParams[i]))
              {
                Variable tmp;
                if (this.Mdriver.Context.ValueContext.TryLoadIndirect(pc, args[i], out tmp) && conditionVars.Contains(tmp))
                {
                }
              }
            }
          }
#endif
        }

        if (CanBeAnEnum(pc, dest, pre))
        {
          Trace("  Found a necessary condition involving only enum values");
          this.InferredConditionContainsEnum = true;
        }

        if (dest.Equals(pre.Condition.UnderlyingVariable))
        {
          switch (mdDecoder.Name(method))
          {
            case "op_Inequality":
              {
                var newLeft = BoxedExpression.Convert(this.Mdriver.Context.ExpressionContext.Refine(pc, args[0]), this.Mdriver.ExpressionDecoder);
                var newRight = BoxedExpression.Convert(this.Mdriver.Context.ExpressionContext.Refine(pc, args[1]), this.Mdriver.ExpressionDecoder);
                if (newLeft != null && newRight != null)
                {
                  Trace(string.Format("  Rebuilding the !=({0}, {1})", newLeft, newRight));
                  var newPremise = BoxedExpression.Binary(BinaryOperator.Cne_Un, newLeft, newRight, dest);
                  return new Precondition(pre.Premises, newPremise, pre.KnownFacts);
                }
                else
                {
                  return Precondition.None;
                }
              }

            case "op_Equality":
              {
                var newLeft = BoxedExpression.Convert(this.Mdriver.Context.ExpressionContext.Refine(pc, args[0]), this.Mdriver.ExpressionDecoder);
                var newRight = BoxedExpression.Convert(this.Mdriver.Context.ExpressionContext.Refine(pc, args[1]), this.Mdriver.ExpressionDecoder);
                if (newLeft != null && newRight != null)
                {
                  Trace(string.Format("  Rebuilding the ==({0}, {1})", newLeft, newRight));
                  var newPremise = BoxedExpression.Binary(BinaryOperator.Ceq, newLeft, newRight, dest);
                  return new Precondition(pre.Premises, newPremise, pre.KnownFacts);
                }
                else
                {
                  return Precondition.None;
                }
              }
          }
        }

        return pre;
      }


      internal void ProduceCalleeAssumes(List<IInferredCondition> conditions)
      {
        // see if we can express current condition after call
        var mdDecoder = this.Mdriver.MetaDataDecoder;
        var context = this.Mdriver.Context;
        var valueContext = this.Mdriver.Context.ValueContext;

        foreach (var pair in this.invariants.CalleeCandidates())
        {
          var method = pair.Key;
          var returnType = this.Mdriver.MetaDataDecoder.ReturnType(method);

          foreach (var pc in pair.Value)
          {
            foreach (var pre in this.invariants[pc])
            {
              var assumption = pre.ToBoxedExpression(mdDecoder);
              var postPC = this.CFG.Post(pc);

              var postExp = assumption.ReadAt(postPC, context, failIfCannotReplaceVarsWithAccessPaths: true, allowReturnValue: returnType);
              if (postExp == null)
              {
                // Let's shot for a || b, i.e. we guess that b contains the original proof obligation
                BinaryOperator bop;
                BoxedExpression left, right;
                if (assumption.IsBinaryExpression(out bop, out left, out right)
                  && bop == BinaryOperator.LogicalOr &&
                  (postExp = right.ReadAt(postPC, context, failIfCannotReplaceVarsWithAccessPaths: true, allowReturnValue: returnType)) != null)
                {
                  // Make sure it contains a postcondition...
                  if (postExp.ToString().Contains("Contract.Result"))
                  {
                    conditions.Add(new InferredCalleeNecessaryConditionCandidate<Method>(postExp, pc, method));
                  }
                }
                continue;
              }

              var preExp = assumption.ReadAt(pc, context, failIfCannotReplaceVarsWithAccessPaths: true, allowReturnValue: returnType);
              if (preExp != null)  // can still read prior to call
              {
                continue;
              }

              // we have a candidate
              conditions.Add(new InferredCalleeCondition<Method>(postExp, pc, method));
            }
          }
        }
      }

      private void RecordCalleeAssumptionCandidate(APC pc, Method method, Precondition pre)
      {
        // candidate for callee assumption
        if (pre.IsNone) return;
        if (pc.InsideContract) return;

#if false // MAF record everything and filter at the end
        var mdDecoder = this.Mdriver.MetaDataDecoder;
        var valueContext = this.Mdriver.Context.ValueContext;

        var assumption = pre.ToBoxedExpression(mdDecoder);

        var postPC = this.CFG.Post(pc);

        var postExp = assumption.ReadAt(postPC, this.Mdriver.Context, true,  allowReturnValue: this.Mdriver.MetaDataDecoder.ReturnType(method));
        if (postExp == null) return;

        var preExp = assumption.ReadAt(pc, this.Mdriver.Context, true, allowReturnValue: this.Mdriver.MetaDataDecoder.ReturnType(method));
        if (preExp != null) return; // can still read prior to call
#endif

#if DEBUG
        if (this.TraceInference)
        {
          Console.WriteLine("    Adding the calleee assumption candidate {0} for method {1}", pre, this.Mdriver.MetaDataDecoder.Name(method));
        }
#endif
        // record
        this.invariants.AddCalleeAssumeCandidate(pc, method, pre);
      }


      override public Precondition ConstrainedCallvirt<TypeList, ArgList>(APC pc, Method method, bool tail, Type constraint, TypeList extraVarargs, Variable dest, ArgList args, Precondition pre)
      // where TypeList : IIndexable<Type>
      // where ArgList : IIndexable<Variable>
      {
        BreakHere(pc, pre);

        this.RecordCalleeAssumptionCandidate(pc, method, pre);
        return pre;
      }

      override public Precondition Ldlen(APC pc, Variable dest, Variable array, Precondition pre)
      {
        BreakHere(pc, pre);

        if (!pre.IsNone && this.CodeFixes.IsEnabled)
        {
          // if pre == left < right, and right == dest, so we can suggest a different initialization for dest
          BinaryOperator bop;
          BoxedExpression left, right;
          if (pre.Condition.IsBinaryExpression(out bop, out left, out right) && bop == BinaryOperator.Clt && AreTheSame(pc, dest, right))
          {
            // We cannot refine the dest directly, because the heap analysis is too smart, and it will give us back the exp for source
            //var destAsBE = BoxedExpression.Convert(this.Mdriver.Context.ExpressionContext.Refine(this.Mdriver.CFG.Post(pc), dest), this.Mdriver.ExpressionDecoder);

            // HACK HACK HACK HACK HACK HACK HACK HACK HACK HACK HACK HACK !!!! We try to get the name two instructions downsteam, i.e. dest = ldlen array; stloc accessPath dest
            var accessPath = this.Mdriver.Context.ValueContext.AccessPathList(this.Mdriver.CFG.Post(this.Mdriver.CFG.Post(pc)), dest, true, true);
            if (accessPath != null && accessPath.Last().ToString() != "Length")
            {
              var destAsBE = BoxedExpression.Var(dest, accessPath);
              var rightMinusOne = BoxedExpression.Binary(BinaryOperator.Sub, right, BoxedExpression.Const(1, this.Mdriver.MetaDataDecoder.System_Int32, this.Mdriver.MetaDataDecoder));
              var converted = BoxedExpression.Convert(this.Mdriver.Context.ExpressionContext.Refine(pc, dest), this.Mdriver.ExpressionDecoder);

              if (converted != null)
              {
                this.AlreadyFoundAFix = this.AlreadyFoundAFix |
                  this.CodeFixes.TrySuggestConstantInititalizationFix(this.obl, pc, destAsBE, converted, rightMinusOne, pre.Condition.Simplify(this.Mdriver.MetaDataDecoder, replaceIntConstantsByBooleans: true));
              }
            }
          }
        }

        return pre;
      }

      override public Precondition Newarray<ArgList>(APC pc, Type type, Variable dest, ArgList len, Precondition pre)
      //where ArgList : IIndexable<Variable>
      {
        BreakHere(pc, pre);

        if (this.CodeFixes.IsEnabled)
        {
          var info = new ReaderInfo<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>(pc, this.Mdriver.Context, this.Mdriver.MetaDataDecoder);

          // we do not rewrite the precondition, as it may be null if some of its part cannot be given a name.
          // downstream, we are only interested it is in the form a < len[0]

          //var PreConditionRewritten = this.ExpressionReader.Visit(pre.Condition, info);

          Func<Variable, BoxedExpression> ToExp = (Variable v) =>
          {
            return BoxedExpression.Convert(this.Mdriver.Context.ExpressionContext.Refine(pc.Post(), v), this.Mdriver.ExpressionDecoder);
          };

          Func<APC> ConditionPC = () =>
            {
              APC condPC;
              if (!this.Mdriver.AdditionalSyntacticInformation.VariableDefinitions.TryGetValue(len[0], out condPC))
              {
                condPC = pc.GetFirstPredecessorWithSourceContext(this.Mdriver.CFG);
              }

              return condPC;
            };

          if (pre.Condition != null &&
           this.CodeFixes.TrySuggestLargerAllocation(this.obl,
            ConditionPC, pc,
            pre.Condition, dest, len[0], ToExp, this.facts))
          {
            this.AlreadyFoundAFix = true;
          }
        }
        return pre;
      }

      override public Precondition Stelem(APC pc, Type type, Variable array, Variable index, Variable value, Precondition pre)
      {
        BreakHere(pc, pre);

        // We check if a ForAll assertion is violated because of this array aupdate.
        // If it is, then we propagate the condition on value, instead of the whole quantified fact
        bool isForAll;
        BoxedExpression boundVar, low, upp, body;
        if (pre.Condition.IsQuantifiedExpression(out isForAll, out boundVar, out low, out upp, out body))
        {
          if (isForAll)
          {
            BoxedExpression.ArrayIndexExpression<Type> arrayIndexExp;
            if (body.TryFindArrayExp(boundVar, out arrayIndexExp) && array.Equals(arrayIndexExp.Array.UnderlyingVariable))
            {
              var newUpperBound = BoxedExpression.Convert(this.Mdriver.Context.ExpressionContext.Refine(pc, index), this.Mdriver.ExpressionDecoder);
              if (newUpperBound != null)
              {
                var newForAll = new ForAllIndexedExpression(null, boundVar, low, newUpperBound, body);
                if (this.facts.IsTrue(pc, newForAll) == ProofOutcome.True)
                {
                  var converted = BoxedExpression.Convert(this.Mdriver.Context.ExpressionContext.Refine(pc, value), this.Mdriver.ExpressionDecoder);
                  if (converted != null)
                  {
                    var newCondition = body.Substitute(arrayIndexExp, converted);
                    if (newCondition != null)
                    {
                      return new Precondition(pre.Premises, newCondition);
                    }
                  }
                }
              }
            }
          }
        }

        return pre;
      }

      [ContractVerification(true)]
      override public Precondition Assume(APC pc, string tag, Variable condition, object provenance, Precondition pre)
      {
        BreakHere(pc, pre, "assume " + tag);

        if (tag == "true" || tag == "false")
        {
          var premise = BoxedExpression.Convert(this.Mdriver.Context.ExpressionContext.Refine(pc, condition), this.Mdriver.ExpressionDecoder);

          if (premise == null)
          {
            Trace("      Condition to assume (after conversion) is <null>. We give up");

            // we give up
            return Precondition.None;
          }

          var type = this.Mdriver.Context.ValueContext.GetType(pc, condition);

          // F: TODO this check is because in CCI2 we obtain variables b to be Booleans instead of int. I should investigate more where MakeNotEqualToZero is used, and if it better to split it
          if (!(type.IsNormal && type.Value.Equals(this.Mdriver.MetaDataDecoder.System_Boolean)))
          {
            premise = premise.MakeNotEqualToZero(type, this.Mdriver.MetaDataDecoder);
          }

          if (tag == "false")
          {
            premise = premise.Negate();
            // since we have some issues with types (boolean exps marked as int), normalizing the expression now may generate some "bad" precondition
            // premise = premise.MakeNotEqualToZero(type, this.Mdriver.MetaDataDecoder);
          }

          Trace(string.Format("      Condition for the assumption is {0}", premise));

          // Try to suggest a simple test fix

          Func<APC> ExpressionPC = () =>
            {
              APC expPC;
              if (this.Mdriver.AdditionalSyntacticInformation.VariableDefinitions.TryGetValue(condition, out expPC))
              {
                return expPC;
              }
              else
              {
                return pc.GetFirstPredecessorWithSourceContext(this.Mdriver.CFG);
              }
            };

          var info = new ReaderInfo<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>(pc, this.Mdriver.Context, this.Mdriver.MetaDataDecoder);
          var PreConditionRewritten = this.ExpressionReader.Visit(pre.Condition, info);
          if (PreConditionRewritten != null)
          {
            if (this.CodeFixes.TrySuggestTestFix(this.obl, ExpressionPC,
              premise, PreConditionRewritten, this.Mdriver.MetaDataDecoder,
              be => be.IsArrayLength(pc, this.Mdriver.Context, this.Mdriver.MetaDataDecoder)))
            {
              // We found a fix, let's remember it
              this.AlreadyFoundAFix = true;
            }
            else if (this.TestFix == null)
            {
              bool disjointVars = false, nullCheck = false;

              if ((disjointVars = PreConditionRewritten.Variables().Intersect(premise.Variables()).Any()) || (nullCheck = TestArrayForNullButRequiresMore(pc, PreConditionRewritten, premise)))
              {
                APC pcForVariable;
                if (!this.Mdriver.AdditionalSyntacticInformation.VariableDefinitions.TryGetValue(condition, out pcForVariable))
                {
                  pcForVariable = pc.GetFirstPredecessorWithSourceContext(this.Mdriver.CFG);
                }

                Trace(string.Format("      Found a test strengthening by adding {0} to {1}", PreConditionRewritten, condition));

                this.TestFix = new AdditionalTest(pcForVariable, PreConditionRewritten, disjointVars, nullCheck);
              }
            }
          }

          switch (facts.IsTrue(pc, premise))
          {
            #region All the cases
            case ProofOutcome.True:
              {
                Trace(string.Format("    Assume {0}, outcome true : Adding known fact {1}", tag, premise));

                return pre.AddKnownFact(premise);
              }

            case ProofOutcome.False:
            case ProofOutcome.Bottom:
              {
                return Precondition.None;
              }

            case ProofOutcome.Top:
              {

                if (pre.Condition.IsConstantFalse())
                {
                  Trace(string.Format("    The condition is the constant false. We replace it with the current assumption", premise.Negate()));
                  return new Precondition(pre.Premises, premise.Negate());                
                }

                if (pre.KnownFacts != null)
                {
                  foreach (var exp in pre.KnownFacts)
                  {
                    if (exp != null)
                    {
                      var newfact = FList<BoxedExpression>.Cons(exp, FList<BoxedExpression>.Empty);
                      if (facts.IsTrueImply(pc, newfact, FList<BoxedExpression>.Empty, premise) == ProofOutcome.True)
                      {
                        Trace(string.Format("    Assume {0}, outcome top : Adding known fact {1}", tag, premise));

                        return pre.AddKnownFact(premise);
                      }
                    }
                  }
                }

                Trace(string.Format("      Adding the condition {0} as premise to the current necessary condition", premise));

                return pre.AddPremise(premise).SimplifyImplication();
              }

            default:
              Contract.Assert(false);
              return pre;
            #endregion
          }
        }
        // We want to try to replace a part of the premise
        else if (tag == "requires")
        {
          // nothing to do
        }
        // we try to replace a part of the candidate precondition with a more visibile piece, if possible
        else if (tag == "ensures")
        {
          var premise = BoxedExpression.Convert(this.Mdriver.Context.ExpressionContext.Refine(pc, condition), this.Mdriver.ExpressionDecoder);

          if (premise == null)
          {
            return pre;
          }

          BinaryOperator bop;
          BoxedExpression left, right;
          // matches left == right
          if (premise.IsBinaryExpression(out bop, out left, out right) && bop == BinaryOperator.Ceq)
          {
            // is left suitable in a precondition? if so, let's replace right by left 

            // TODO: the symmetric case
            Variable leftVar;
            FList<PathElement> leftPath;
            if (left.TryGetFrameworkVariable(out leftVar) &&
              (leftPath = this.Mdriver.Context.ValueContext.AccessPathList(pc, leftVar, false, false)) != null &&
              this.Mdriver.Context.ValueContext.PathSuitableInRequires(pc, leftPath))
            {
              var newCondition = pre.Condition.Substitute(right, left);
              var newPremises = pre.Premises != null ? pre.Premises.ApplyToAll<BoxedExpression, BoxedExpression>(p => p.Substitute(right, left)).ToList() : pre.Premises;

              return new Precondition(newPremises, newCondition, pre.KnownFacts);
            }

            Trace(string.Format("    Assume ensures: Adding known fact {0}", premise));

            return pre.AddKnownFact(premise);
          }
        }
        return pre;
      }


      override public Precondition Binary(APC pc, BinaryOperator op, Variable dest, Variable s1, Variable s2, Precondition pre)
      {
        BreakHere(pc, pre);

        if (this.CodeFixes.IsEnabled && !op.IsComparisonBinaryOperator() &&
          !this.obl.ConditionForPreconditionInference.Equals(pre.Condition) &&
          !this.conditionsWithFixes.Contains(pre.Condition) &&
          pre.Condition.VariablesInnerNodes<Variable>().Contains(dest))
        {
          if (AtLeastOnePremisIsNotFalse(pc, pre.Premises))
          {
            var parameters = new InitializationFix<Variable>(this.obl, pc, dest, pre.Condition, pre.Condition.VariablesInnerNodes<Variable>());
            if (this.CodeFixes.TrySuggestInitializationFix<Variable>(ref parameters))
            {
              this.conditionsWithFixes.Add(pre.Condition);
            }
          }
        }

        return pre;
      }


      override public Precondition Unary(APC pc, UnaryOperator op, bool overflow, bool unsigned, Variable dest, Variable source, Precondition pre)
      {
        BreakHere(pc, pre);

        if (!pre.IsNone && this.CodeFixes.IsEnabled)
        {
          // if pre == left < right, and right == dest, so we can suggest a different initialization for dest
          BinaryOperator bop;
          BoxedExpression left, right;
          if (pre.Condition.IsBinaryExpression(out bop, out left, out right) && bop == BinaryOperator.Clt && AreTheSame(pc, dest, right))
          {
            // We cannot refine the dest directly, because the heap analysis is too smart, and it will give us back the exp for source
            //var destAsBE = BoxedExpression.Convert(this.Mdriver.Context.ExpressionContext.Refine(this.Mdriver.CFG.Post(pc), dest), this.Mdriver.ExpressionDecoder);

            // HACK HACK HACK: We are looking for a pattern of instructions: "Unary dest ...; Stelem  accessPath dest"
            var accessPath = this.Mdriver.Context.ValueContext.AccessPathList(this.Mdriver.CFG.Post(this.Mdriver.CFG.Post(pc)), dest, true, true);
            if (accessPath != null)
            {
              var destAsBE = BoxedExpression.Var(dest, accessPath);
              var rightMinusOne = BoxedExpression.Binary(BinaryOperator.Sub, right, BoxedExpression.Const(1, this.Mdriver.MetaDataDecoder.System_Int32, this.Mdriver.MetaDataDecoder));

              var converted = BoxedExpression.Convert(this.Mdriver.Context.ExpressionContext.Refine(pc, source), this.Mdriver.ExpressionDecoder);

              if (converted != null)
              {
                this.AlreadyFoundAFix = this.AlreadyFoundAFix |
                  this.CodeFixes.TrySuggestConstantInititalizationFix(this.obl, pc, destAsBE, converted, rightMinusOne, pre.Condition.Simplify(this.Mdriver.MetaDataDecoder, replaceIntConstantsByBooleans: true));
              }
            }
          }
        }

        return pre;
      }

      #region Private helpers

      [Pure]
      private bool CanBeAnEnum(APC pc, Variable dest, Precondition pre)
      {
        var postPC = pc.Post();
        var type = this.Mdriver.Context.ValueContext.GetType(postPC, dest);
        // Is dest an enum?
        var md = this.Mdriver.MetaDataDecoder;
        if (type.IsNormal && md.IsEnumWithoutFlagAttribute(type.Value) && pre.Condition != null)
        {
          var varsInPre = pre.Condition.Variables();
          // Check that the condition is on the returned value
          if (varsInPre.Count == 1 && dest.Equals(varsInPre.First().UnderlyingVariable))
          {
            var vars = pre.ToBoxedExpression(md).Variables();
            return vars.All(boxedVar =>
            {
              Variable v;
              if (boxedVar.TryGetFrameworkVariable(out v))
              {
                var t = this.Mdriver.Context.ValueContext.GetType(postPC, v);
                return t.IsNormal && md.IsEnumWithoutFlagAttribute(t.Value);
              }

              return false;
            }
              );
          }
        }

        return false;
      }

      [Pure]
      private bool CanBeAnEnum(APC pc, Precondition pre)
      {
        var md = this.Mdriver.MetaDataDecoder;
        var postPC = pc.Post();
        var vars = pre.ToBoxedExpression(md).Variables();

        return vars.Any() // At least one var...
          && vars.All(boxedVar =>
          {
            Variable v;
            if (boxedVar.TryGetFrameworkVariable(out v))
            {
              var t = this.Mdriver.Context.ValueContext.GetType(postPC, v);
              return t.IsNormal && md.IsEnumWithoutFlagAttribute(t.Value);
            }

            return false;
          }
              );
      }

      private bool TestArrayForNullButRequiresMore(APC pc, BoxedExpression currentCondition, BoxedExpression test)
      {
        if (currentCondition == null || test == null)
        {
          return false;
        }

        BinaryOperator bop1, bop2;
        BoxedExpression left1, left2, right1, right2;
        if (currentCondition.IsBinaryExpression(out bop1, out left1, out right1) && test.IsBinaryExpression(out bop2, out left2, out right2))
        {
          var found = false;
          Variable arrayVar;
          // search for a test in the form arrayVar {==, !=, ...} null
          if (left2.IsNull && right2.TryGetFrameworkVariable(out arrayVar))
          {
            found = true;
          }
          else if (right2.IsNull && left2.TryGetFrameworkVariable(out arrayVar))
          {
            found = true;
          }
          else
          {
            arrayVar = default(Variable); // just to make the compiler happy 
          }

          if (found)
          {
            // See if it is an array type
            FlatDomain<Type> type;
            if ((type = this.Mdriver.Context.ValueContext.GetType(pc, arrayVar)).IsNormal && this.Mdriver.MetaDataDecoder.IsArray(type.Value))
            {
              // See if we are propagating an array length check
              Variable arrayLength;
              if (this.Mdriver.Context.ValueContext.TryGetArrayLength(pc, arrayVar, out arrayLength))
              {
                return arrayLength.Equals(left1.UnderlyingVariable) || arrayLength.Equals(right1.UnderlyingVariable);
              }
            }
          }
        }

        return false;
      }

      #endregion

      #endregion

      #region Additional Test definition

      internal class AdditionalTest
      {
        public readonly APC pc;
        public readonly BoxedExpression Fix;
        public readonly bool StrengthenSameVariable;
        public readonly bool StrengthenNullCheck;

        public AdditionalTest(APC pc, BoxedExpression Fix, bool strengthenSameVariable, bool strengthenNullCheck)
        {
          Contract.Requires(Fix != null);

          this.pc = pc;
          this.Fix = Fix;
          this.StrengthenSameVariable = strengthenSameVariable;
          this.StrengthenNullCheck = strengthenNullCheck;
        }

        public override string ToString()
        {
          return string.Format("{0}:{1}", pc, Fix);
        }
      }

      #endregion

      #region Privates
      private bool IsEnumType(Variable v)
      {
        var mdd = this.Mdriver.MetaDataDecoder;
        var type = this.Mdriver.Context.ValueContext.GetType(this.Mdriver.CFG.EntryAfterRequires, v);
        return type.IsNormal && mdd.IsEnum(type.Value);
      }

      private BoxedExpression MakeInt(int v)
      {
        var mdd = this.Mdriver.MetaDataDecoder;
        return BoxedExpression.Const(v, mdd.System_Int32, mdd);
      }
      #endregion
    }

    #region Precondition abstractElement

    struct Precondition
    {
      #region State

      enum State { Normal, None }

      readonly public List<BoxedExpression> Premises;
      readonly public BoxedExpression Condition;
      readonly public List<BoxedExpression> KnownFacts;

      readonly private State state;

      #endregion

      #region Properties

      static public Precondition None { get { return new Precondition(true); } }

      public bool IsNone { get { return this.state == State.None; } }

      public bool HasEmptyPremises { get { return !this.IsNone && (this.Premises == null || this.Premises.Count == 0); } }

      public bool HasFalsePremise { get { return this.Premises != null && this.Premises.Count == 1 && this.Premises[0].IsConstantFalse(); } }

      #endregion

      #region Constructors
      public Precondition(BoxedExpression Premise, BoxedExpression Condition, List<BoxedExpression> knownFacts = null)
        : this(Premise != null ? new List<BoxedExpression>() { Premise } : null, Condition, knownFacts)
      {
      }

      public Precondition(List<BoxedExpression> Premises, BoxedExpression Condition, List<BoxedExpression> knownFacts = null)
      {
        Contract.Requires(Condition != null);

        this.Premises = Premises;
        this.Condition = Condition;
        this.KnownFacts = knownFacts == null ? new List<BoxedExpression>() : knownFacts;
        this.state = State.Normal;
      }

      public Precondition(bool dummy)
      {
        this.Premises = null;
        this.Condition = null;
        this.KnownFacts = null;
        this.state = State.None;
      }

      #endregion

      #region Operators
      static public bool operator ==(Precondition left, Precondition right)
      {
        if (left.state == right.state)
        {
          switch (left.state)
          {
            case State.Normal:
              return left.Condition == right.Condition && ListEqual(left.Premises, right.Premises);

            case State.None:
              return true;

            default:
              Contract.Assert(false);
              break;
          }
        }

        return false;
      }

      static public bool operator !=(Precondition left, Precondition right)
      {
        return !(left == right);
      }

      static private bool ListEqual(List<BoxedExpression> left, List<BoxedExpression> right)
      {
        if ((left == null) && (right == null))
        {
          return true;
        }
        if ((left == null) != (right == null))
        {
          return false;
        }
        foreach (var l in left)
        {
          if (!right.Contains(l))
            return false;
        }
        foreach (var r in right)
        {
          if (!left.Contains(r))
            return false;
        }

        return true;
      }

      #endregion

      #region public Methods

      public Precondition AddPremise(BoxedExpression premise)
      {
        Contract.Requires(premise != null);

        if (this.Premises == null)
        {
          return new Precondition(premise, this.Condition, this.KnownFacts);
        }
        else
        {
          if (this.Premises.Contains(premise))
          {
            return this;
          }

          UnaryOperator uop;
          BoxedExpression inner;
          if (premise.IsUnaryExpression(out uop, out inner) && uop == UnaryOperator.Not)
          {
            // Cut precondition with false premises (hence trivially true preconditions)
            var innerVar = inner.UnderlyingVariable;
            if (innerVar != null &&
              this.Premises.Exists(
              exp =>
              {
                Variable frameworkVar;
                return exp != null && exp.TryGetFrameworkVariable(out frameworkVar) && frameworkVar.Equals(innerVar);
              }
                )
              )
            {
              return Precondition.None;
            }
          }

          return new Precondition(new List<BoxedExpression>(this.Premises) { premise }, this.Condition, this.KnownFacts);
        }
      }

      public Precondition AddKnownFact(BoxedExpression knownFact)
      {
        Contract.Requires(knownFact != null);

        // Avoid dummy expressions
        bool dummy;
        if (knownFact.IsTrivialCondition(out dummy))
        {
          return this;
        }

        return new Precondition(this.Premises, this.Condition,
          this.KnownFacts != null ? new List<BoxedExpression>(this.KnownFacts) { knownFact } : null);
      }

      public IInferredCondition ToInferredCondition(IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder, ExpressionInPreStateKind kind, bool isSufficient, Func<BoxedExpression, FlatDomain<Type>> GetType)
      {
        var be = this.ToBoxedExpression(mdDecoder);

        be = ReduceDisjunctions(be, mdDecoder, GetType);

        return new SimpleInferredConditionAtEntry(be, kind, isSufficient);
      }

      public BoxedExpression ToBoxedExpression(IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder)
      {
        switch (this.state)
        {
          case State.None:
            {
              return BoxedExpression.ConstBool(true, mdDecoder);
            }

          case State.Normal:
            {
              var result = Condition;
              if (Premises != null && Premises.Count > 0)
              {
                result =
                  BoxedExpression.Binary(BinaryOperator.LogicalOr, Premises.ConvertAll(exp => exp.Negate()).Concatenate(BinaryOperator.LogicalOr), result);
              }
              return result;
            }

          default:
            {
              Contract.Assert(false);
              break;
            }
        }

        return null;
      }

      public Precondition SimplifyPremises(Func<BoxedExpression, ProofOutcome> IsImpliedByPreconditions, Predicate<Variable> IsEnumType, Func<int, BoxedExpression> MakeInt)
      {
        Contract.Requires(IsImpliedByPreconditions != null);
        Contract.Requires(IsEnumType != null);
        Contract.Requires(MakeInt != null);

        if (this.IsNone || this.HasEmptyPremises)
        {
          return this;
        }

        var newPremises = new List<BoxedExpression>(this.Premises.Count);

        foreach (var premise in this.Premises)
        {
          // Remember: we do not force this.Premises to contain only != null premises
          if (premise != null && IsImpliedByPreconditions(premise) == ProofOutcome.Top)
          {
            var premiseSimplified = SimplifyEnums(premise, IsEnumType, MakeInt);

            newPremises.Add(premiseSimplified);
          }
        }

        // reverse the premises as we collect them from the end to the top of the method, but in the precondition (because of shortcuts) they should appear
        // in the reverse order
        newPremises.Reverse();

        return new Precondition(newPremises, this.Condition);
      }

      /// <summary>
      /// Hack for simplifying enums!!!
      /// </summary>
      private BoxedExpression SimplifyEnums(BoxedExpression premise, Predicate<Variable> IsEnumType, Func<int, BoxedExpression> MakeInt)
      {
        Contract.Requires(premise != null);
        Contract.Requires(IsEnumType != null);

        // Decompiles things like d-1 == 3, where d is an enum var generated by the C# compiler
        BinaryOperator bop, sub; BoxedExpression left, rightConstant, enumVar, enumConstant;
        if (premise.IsBinaryExpression(out bop, out left, out rightConstant) && (bop == BinaryOperator.Ceq || bop == BinaryOperator.Cne_Un) &&
          left.IsBinaryExpression(out sub, out enumVar, out enumConstant) && sub == BinaryOperator.Sub)
        {
          left = null; // to make sure we are not using it!!!
          Variable v;
          int k1, k2;
          if (enumVar.TryGetFrameworkVariable(out v) && IsEnumType(v) &&
            rightConstant.IsConstantInt(out k1) && enumConstant.IsConstantInt(out k2))
          {
            return BoxedExpression.Binary(bop, enumVar, MakeInt(k2 + k1));
          }
        }
        return premise;
      }

      private BoxedExpression ReduceDisjunctions(BoxedExpression be, IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder, Func<BoxedExpression, FlatDomain<Type>> GetType)
      {
        // Simplify enums
        var disjunctions = be.SplitDisjunctions();
        if (disjunctions.Count > 1)
        {
          var type = default(Type);
          var v = default(BoxedExpression);
          var constants = new Set<int>();
          foreach (var d in disjunctions)
          {
            FlatDomain<Type> t;
            // try to match l == r
            BinaryOperator bop; BoxedExpression l, r; int k;
            if (d.IsBinaryExpression(out bop, out l, out r) && bop == BinaryOperator.Ceq &&
              (v == null || v.Equals(l)) &&
              ((t = GetType(l)).IsNormal) && mdDecoder.IsEnumWithoutFlagAttribute(t.Value) && r.IsConstantInt(out k))
            {
              if (Object.Equals(type, default(Type)))
              {
                type = t.Value;
              }
              else
              {
                if (!type.Equals(t.Value))
                {
                  goto done;
                }
              }
              v = l;
              constants.Add(k);
            }
            else
            {
              goto done; // we are done
            }
          }
          Contract.Assume(constants.Count >= 2);

          List<int> allValuesForEnum;
          if (mdDecoder.TryGetEnumValues(type, out allValuesForEnum))
          {
            var difference = allValuesForEnum.Except(constants);
            if (difference.Count() < disjunctions.Count)
            {
              BoxedExpression result = null;
              foreach (var notThere in difference)
              {
                var tmp = BoxedExpression.Binary(BinaryOperator.Cne_Un, v, BoxedExpression.Const(notThere, type, mdDecoder));
                if (result == null)
                {
                  result = tmp;
                }
                else
                {
                  result = BoxedExpression.Binary(BinaryOperator.LogicalAnd, result, tmp);
                }
              }

              return result;
            }
          }
        }

      done:
        return be;
      }

      public Precondition SimplifyCondition(Func<BoxedExpression, ProofOutcome> Checker, SimpleSatisfyProcedure<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> satisfy, Predicate<Variable> IsEnumType, Func<int, BoxedExpression> MakeInt)
      {
        Contract.Requires(Checker != null);
        Contract.Requires(satisfy != null);

        if (this.IsNone || this.HasEmptyPremises)
        {
          return this;
        }

        var condition = this.Condition;

        // Simplify trivialities that we do not want in a precondition, e.g., (0 <= 3) == 0
        if (!condition.Variables().Any())
        {
          switch (Checker(this.Condition))
          {
            case ProofOutcome.True:
              condition = satisfy.True;
              break;

            case ProofOutcome.False:
              condition = satisfy.False;
              break;

            default:
              break;
          }
        }

        condition = SimplifyEnums(condition, IsEnumType, MakeInt);

        var newCondition = satisfy.ApplySimpleArithmeticRules(condition);

        return newCondition != this.Condition ? new Precondition(this.Premises, newCondition) : this;
      }

      public Precondition Convert(Converter<BoxedExpression, ExpressionInPreState> converter, out ExpressionInPreStateKind kind)
      {
        Contract.Requires(converter != null);

        if (this.IsNone)
        {
          kind = ExpressionInPreStateKind.Any;
          return this;
        }

        var newCondition = converter(this.Condition);
        if (newCondition == null)
        {
          kind = ExpressionInPreStateKind.Any;
          return Precondition.None;
        }

        var resultKind = newCondition.kind;
        if (this.Premises == null)
        {
          kind = resultKind;
          return new Precondition(/*only to resolve overload*/ this.Premises, newCondition.expr, null);
        }

        var newPremises = new List<BoxedExpression>(this.Premises.Count);

        foreach (var premise in this.Premises)
        {
          var converted = converter(premise);
          if (converted == null)
          {
            kind = ExpressionInPreStateKind.Any;
            return Precondition.None;
          }
          resultKind = resultKind.Join(converted.kind);
          newPremises.Add(converted.expr);
        }
        kind = resultKind;
        return new Precondition(newPremises, newCondition.expr);
      }

      #endregion

      #region Overridden

      public override bool Equals(object obj)
      {
        if (obj == null)
          return false;
        if (obj is Precondition)
        {
          var that = (Precondition)obj;
          return this == that;
        }
        return false;
      }

      public override int GetHashCode()
      {
        if (this.state == State.None)
        {
          return 0;
        }
        else
        {
          return (this.Premises != null ? this.Premises.GetHashCode() : 0) + (this.Condition != null ? this.Condition.GetHashCode() : 0);
        }
      }

      public override string ToString()
      {
        return string.Format("{0} ==> {1} {2}",
          ToString(this.Premises),
          this.Condition != null ? this.Condition.ToString() : String.Empty,
          this.KnownFacts != null && this.KnownFacts.Count > 0 ? "( known facts: " + ToString(this.KnownFacts) + " )" : String.Empty);
      }

      private string ToString(List<BoxedExpression> exps)
      {
        if (exps == null || exps.Count == 0)
          return "true";

        var buffer = new StringBuilder();
        foreach (var exp in exps)
        {
          if (exp != null)
          {
            buffer.Append(" " + exp.ToString());
          }
        }

        return buffer.ToString();
      }
      #endregion

      internal Precondition SimplifyImplication()
      {
        if (this.IsNone)
        {
          return this;
        }

        if (this.Condition != null && this.Premises != null && this.Premises.Count == 1)
        {
          BinaryOperator bop1, bop2;
          BoxedExpression condition1, condition2;
          BoxedExpression premise1, premise2;
          //  Simplifies "i < rank ==> i < C.Length" to " rank <= C.Length
          if (this.Condition.IsBinaryExpression(out bop1, out condition1, out condition2) && bop1 == BinaryOperator.Clt &&
            this.Premises[0].IsBinaryExpression(out bop2, out premise1, out premise2) && bop2 == BinaryOperator.Clt &&
            condition1.Equals(premise1))
          {
            var newCondition = BoxedExpression.Binary(BinaryOperator.Cle, premise2, condition2);
            return new Precondition(this.Premises, newCondition, this.KnownFacts);
          }
        }

        return this;
      }
    }

    #endregion

    #region Invariant DB

    struct InvariantDB
    {
      readonly private Dictionary<APC, Set<Precondition>> Invariants;
      readonly private APC entry;
      readonly private Dictionary<Method, Set<APC>> calleeAssumeCandidates;

      public InvariantDB(APC entry)
      {
        this.entry = entry;
        this.Invariants = new Dictionary<APC, Set<Precondition>>();
        this.calleeAssumeCandidates = new Dictionary<Method, Set<APC>>();
      }

      /// <summary>
      /// Essentially performs the join of the preconditions at the program point PC.
      /// Right now the join is pretty straightforward
      /// </summary>
      public void Add(APC pc, Precondition pre)
      {

        bool preUsed = false;
        Set<Precondition> preconditions;
        if (this.Invariants.TryGetValue(pc, out preconditions))
        {
          var result = new Set<Precondition>();
          foreach (var candidate in preconditions)
          {
            // If they have the same condition
            if (candidate.Condition.Equals(pre.Condition))
            {
              preUsed = true;

              // If one of the two has no premises then we add them
              if (candidate.HasEmptyPremises || pre.HasEmptyPremises)
              {
                result.Add(new Precondition(null as BoxedExpression, pre.Condition));
              }
              else // let's try to find if one is weaker than the other, or we try getting a normal form
              { 
                // if we have a && b ==> c and b ==> c, we want to only keep b ==> c
                var premisesForPreAsSet = pre.Premises.ToSet();
                var premisesForCandidatedAsSet = candidate.Premises.ToSet();
                if (premisesForCandidatedAsSet.IsSubset(premisesForPreAsSet))
                {
                  result.Add(candidate);
                  continue;
                }
                else if (premisesForPreAsSet.IsSubset(premisesForCandidatedAsSet))
                {
                  result.Add(pre);
                  continue;
                }
                // else we do not have a 

                // Let us try to simplify the contraddictory premises (essentially Robinson's resolution)

                var newPremises = new List<BoxedExpression>(); // The new premises for the result

                var negatedPremises = pre.Premises.ConvertAll(exp => exp != null ? exp.Negate() : null);

                // first remove the contraddictions
                foreach (var premise in candidate.Premises)
                {
                  if (negatedPremises.Contains(premise))
                  {
                    continue;
                  }
                  newPremises.Add(premise);
                }

                // now see if the new premises are weaker of what we had before
                bool isSubset = true;
                foreach (var newPremise in newPremises)
                {
                  if (!candidate.Premises.Contains(newPremise))
                  {
                    isSubset = false;
                    break;
                  }
                }

                // we simplified the candidate, let's add it
                if (isSubset)
                {
                  result.Add(new Precondition(newPremises, pre.Condition));
                }
                else
                {
                  result.Add(candidate);
                }

                // now we have to chose if we want to add the "pre" of the input. We do so only if we have different length of premises
                if (pre.Premises.Count != candidate.Premises.Count)
                {
                  result.Add(pre);
                }
              }
            }
            else
            {
              result.Add(candidate);
            }
          }

          if (!preUsed)
          {
            result.Add(pre);
          }

          this.Invariants[pc] = result;
        }
        else
        {
          this.Invariants[pc] = new Set<Precondition>() { pre };
        }
      }

      public Set<Precondition> PreconditionAtEntryPoint
      {
        get
        {
          Contract.Ensures(Contract.Result<Set<Precondition>>() != null);

          return this[entry];
        }
      }

      public Set<Precondition> this[APC pc]
      {
        get
        {
          Contract.Ensures(Contract.Result<Set<Precondition>>() != null);

          Set<Precondition> result;
          if (this.Invariants.TryGetValue(pc, out result))
          {
            Contract.Assume(result != null);
            return result;
          }
          else
          {
            return new Set<Precondition>();
          }
        }
      }

      public override string ToString()
      {
        return this.Invariants.ToString();
      }

      private Set<APC> GetOrCreateCalleeAssumeCandidates(Method callee)
      {
        Set<APC> result;
        if (!this.calleeAssumeCandidates.TryGetValue(callee, out result))
        {
          result = new Set<APC>();
          this.calleeAssumeCandidates.Add(callee, result);
        }
        return result;
      }

      public void AddCalleeAssumeCandidate(APC pc, Method callee, Precondition pre)
      {
        this.Add(pc, pre);
        this.GetOrCreateCalleeAssumeCandidates(callee).Add(pc);
      }

      public IEnumerable<KeyValuePair<Method, Set<APC>>> CalleeCandidates()
      {
        return this.calleeAssumeCandidates;
      }

    }

    #endregion
  }
}