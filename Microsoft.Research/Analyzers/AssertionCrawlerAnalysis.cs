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

// #define EXPERIMENTAL

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Microsoft.Research.DataStructures;

namespace Microsoft.Research.CodeAnalysis
{
  using Provenance = IEnumerable<ProofObligation>;

  public static class AssertionFinder
  {
    public static AnalysisStatistics ValidateAssertions<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>(
      AssertionObligations<Variable> assertions,
      IFactQuery<BoxedExpression, Variable> facts,
      ContractInferenceManager inferenceManager,
      IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> driver,
      IOutputResults output
    )
      where Expression : IEquatable<Expression>
      where Variable : IEquatable<Variable>
      where Type : IEquatable<Type>
    {
      Contract.Requires(assertions != null);

      return TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>
        .ValidateAssertions(assertions, facts, inferenceManager,driver, output);
    }

    public static AssertionObligations<Variable> 
      GatherAssertions<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>(
      IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> driver,
      IOutputResults output,
      out AssertionStatistics assertStats)
      where Expression : IEquatable<Expression>
      where Variable : IEquatable<Variable>
      where Type : IEquatable<Type>
    {
      return TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>.GatherAssertions
        (driver, output, out assertStats);
    }

    public static class TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>
      where Expression : IEquatable<Expression>
      where Variable : IEquatable<Variable>
      where Type : IEquatable<Type>
    {
      public static AssertionObligations<Variable> GatherAssertions(
        IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> mdriver,
        IOutputResults output, out AssertionStatistics assertStats)
      {
        Contract.Requires(mdriver != null);
        return GetAssertions(mdriver, out assertStats);
      }

      public static AnalysisStatistics ValidateAssertions(
        AssertionObligations<Variable> assertions,
        IFactQuery<BoxedExpression, Variable> facts,
        ContractInferenceManager inferenceManager,
        IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> mdriver,
        IOutputResults output
      )
      {
        #region Contracts

        Contract.Requires(assertions != null);
        Contract.Requires(facts != null);
        Contract.Requires(mdriver != null);
        Contract.Requires(output != null);
        Contract.Requires(inferenceManager!= null);

        #endregion

        var options = output.LogOptions;
        var stats = new AnalysisStatistics();

        #region Check if requires are unsatisfiable
        APC afterRequires = mdriver.Context.MethodContext.CFG.EntryAfterRequires;
        if (facts.IsUnreachable(afterRequires) && options.CheckEntryContradictions)
        {
          var toReport = mdriver.CFG.GetPCForMethodEntry();

          var isVirtual = mdriver.MetaDataDecoder.IsVirtual(mdriver.CurrentMethod);
          var witness = new Witness(null /* no proog obligation causes it*/, WarningType.UnreachedCodeAfterPrecondition,  ProofOutcome.Top, toReport);

          output.EmitOutcome(witness, String.Format("method Requires (including{1} invariants) are unsatisfiable: {0}",
                                         mdriver.MetaDataDecoder.FullName(mdriver.CurrentMethod),
                                         isVirtual ? " inherited requires and" : ""));

          return stats;
        }
        #endregion

        #region Check assertions in method body

        foreach (var obl in assertions)
        {
          string extraMessage = null;

          if (IsTracing(output))
          {
            output.WriteLine("Checking explicit assertion: {0}", (obl.Condition != null? obl.Condition.ToString() : "??"));
          }

          var outcome = obl.Validate(facts, inferenceManager, output);

          if (IsTracing(output))
          {
            output.WriteLine("Assertion discharged from the facts in the abstract domains");
          }

          WeakestPreconditionProver.Path path = null;
          if (outcome == ProofOutcome.Top)
          {
            #region Use backwards analysis
            // Try to validate the assertion by WP inference 
            // We are not pushing it inside obl.Validate essentially because this requires a refacroting where the method driver
            // is a field of AssertionObligation and hence many more type parameters should be added to it
            if (options.UseWeakestPreconditions)
            {
              bool messageAlreadyPrinted;
              if (mdriver.SyntacticComplexity.ShouldAvoidWPComputation(out messageAlreadyPrinted))
              {
                if (!messageAlreadyPrinted)
                {
                  output.WriteLine("Skipping backwards computation for this method ({0}) as cccheck thinks it will cause a timeout", mdriver.MetaDataDecoder.Name(mdriver.CurrentMethod));
                }
                obl.GetWarningContext.Add(new WarningContext(WarningContext.ContextType.WPSkippedBecauseAdaptiveAnalysis));
              }
              else
              {
                WeakestPreconditionProver.AdditionalInfo why;
                path = WeakestPreconditionProver.Discharge(obl.PC, obl.Condition, output.LogOptions.MaxPathSize, mdriver, facts, inferenceManager, out why);
                if (path == null)
                {
                  outcome = ProofOutcome.True;
                  if (IsTracing(output))
                  {
                    output.WriteLine("Assertion discharged via WPs");
                  }
                }
                else
                {
                  if (!why.IsNone)
                  {
                    obl.GetWarningContext.AddRange(why.GetWarningContexts());
                  }
                }
              }
            }
            #endregion
          }
          
          // If it is an assumption, and we were able to prove it, then we suggest it
          if (obl.IsAssumeOrConditionCheck && (options.CheckAssumptions || options.CheckConditions))
          {
            #region Turn assume into asserts
            if (!obl.PC.HasRealSourceContext)
            {
              continue;
            }

            if (outcome.IsNormal())
            {
              if (obl.IsAssume)
              {
                if (outcome == ProofOutcome.True)
                {
                  switch (obl.Tag)
                  {
                    case "assume":
                      {
                        output.Suggestion(ClousotSuggestion.Kind.AssumptionCanBeProven, ClousotSuggestion.Kind.AssumptionCanBeProven.Message(),
                          obl.PC, "Assumption can be proven: Consider changing it into an assert.", null, ClousotSuggestion.ExtraSuggestionInfo.None);
                        break;
                      }

                    case "requires":
                      {
                        // We filter the preconditions containing maxValues
                        if (obl.Condition.Constants().Any(be => be.IsConstantLimitValue()))
                        {
#if DEBUG
                          output.WriteLine("[DEBUG] We skip the reporting a redundant precondition, as it contains a {Min,Max}Value ");
#endif
                          continue;
                        }

                        var msg = "This precondition is redundant: Consider removing it";
                        var extraMsg = GetExtraMessageForRedundantPreconditionIfPossible(obl, mdriver);

                        if (extraMsg != null)
                        {
                          msg = string.Format("{0}. {1}", msg, extraMsg);
                        }

                        output.Suggestion(ClousotSuggestion.Kind.RequiresCanBeProven, ClousotSuggestion.Kind.RequiresCanBeProven.Message(),
                          obl.PC, msg, null, ClousotSuggestion.ExtraSuggestionInfo.None);
                        break;
                      }

                    default:
                      {
                        break;
                      }
                  }
                }
                else if (options.CheckAssumptionsAndContradictions)
                {
                  Contract.Assume(outcome == ProofOutcome.False);
                  output.Suggestion(ClousotSuggestion.Kind.AssumptionCanBeProven, ClousotSuggestion.Kind.AssumptionCanBeProven.Message(),
                    obl.PC, "Assumption is false", null, ClousotSuggestion.ExtraSuggestionInfo.None);
                  // F: we should not reach this point with a requires, as it would have been detected earlier
                }
              }
              else if (obl.IsConditionCheck)
              {
                switch (outcome)
                {
                  case ProofOutcome.True:
                  case ProofOutcome.False:
                    {
                      obl.EmitOutcome(ProofOutcome.Bottom, output);

                      break;
                    }

                  default:
                    {
                      break;
                    }
                }
              }
            }

            #endregion
            
            continue;
          }

          // Try to infer a precondition as weel assumes, code fixes, etc.
          if (outcome == ProofOutcome.Top || outcome == ProofOutcome.False)
          {
            #region Necessary conditions
            InferredConditions inferredConditions;
            outcome = TryDischargeViaInference(outcome, mdriver, inferenceManager, output, obl, obl.GetWarningContext, out inferredConditions);
            if (outcome == ProofOutcome.True)
            {
              if (IsTracing(output))
              {
                output.WriteLine("Assertion discharged via precondition inference");
              }
            }

            // Can we turn the assert into a requires or an assume?
            if (options.SuggestAssertToContracts && obl.Tag == "assert")
            {
              string message = null;

              if (obl.HasASufficientAndNecessaryCondition && inferredConditions != null && inferredConditions.Count > 0)
              {
                #region Entry state
                var candidateCondition = inferredConditions[0].Expr;
                var conditionsIsUnchanged = obl.Condition != null && obl.Condition.Equals(candidateCondition);

                if (conditionsIsUnchanged)
                {
                  if (outcome == ProofOutcome.True)
                  {
                    Field f;
                    var anyField = candidateCondition.Variables().Any(exp => exp.AccessPath.Any(el => el.TryField(out f) && mdriver.MetaDataDecoder.IsReadonly(f)));

                    if (anyField)
                    {
                      message = "Consider turning this Assert into an Assume";
                    }
                    else
                    {
                      message = "This Assert should be made explicit to the callers. Consider turning it into a precondition (or an assumption)";
                    }
                  }
                  else if(outcome == ProofOutcome.Top)
                  {
                    message = "This Assert cannot be statically proven because some information on the entry state is missing. Consider turning it into an Assume (or adding the missing contracts, e.g., object invariants)";
                  }
                  else
                  {
                    // We do nothing
                  }
                }
                #endregion
              }
              else if(obl.HasCodeFix)
              {
                #region Value returned by a method call
                var tracedBackToACall = obl.CodeFixes.OfType<CodeFix.MethodCallResult>().Where(fix => fix.RawFix != null && fix.RawFix.Equals(obl.Condition));
                if (tracedBackToACall.Any())
                {
                  var firstFix = tracedBackToACall.First();
                  Contract.Assume(firstFix != null);

                  if (IsABooleanVariableCondition(obl.PC, obl.Condition, mdriver))
                  {
                    Contract.Assert(obl.Tag == "assert");
                    if (firstFix.CalleeName != null && firstFix.CalleeName.StartsWith("Try"))
                    {
                      message = "This Assert cannot be statically proven because it depends on the return value of a Try* method. Consider turning it into an Assume";
                    }
                    else
                    {
                      message = "Consider turning this Assert into an Assume, or adding extra information so that the static checker can prove it";
                    }
                  }
                  else
                  {
                    message = string.Format(
                      "The validity of this assertion can be traced back to the return value of {0}. Consider adding a postcondition to the method or turning the assert into an assume",
                      (firstFix.CalleeName != null ? firstFix.CalleeName : "a method"));
                  }
                }
                #endregion
              }

              // If we got something let's print it out
              if (message != null)
              {
                output.Suggestion(ClousotSuggestion.Kind.AssertToContract, ClousotSuggestion.Kind.AssertToContract.Message(), obl.PCForValidation, message, null, ClousotSuggestion.ExtraSuggestionInfo.None);
                obl.GetWarningContext.Add(new WarningContext(WarningContext.ContextType.AssertMayBeTurnedIntoAssume, 1));
              }
            }
            #endregion
          }

          // Add the warning contexts
          if (outcome == ProofOutcome.Top)
          {
            obl.GetWarningContext.AddRange(WarningContextFetcher.InferContext(obl.PC, obl.Condition, mdriver.Context, mdriver.MetaDataDecoder.IsBoolean, field => mdriver.MetaDataDecoder.IsReadonly(field)));
            obl.AddWarningContextsFromCodeFixes();
            obl.AddWarningContextsFromMessage();  
          }

          // Avoid reporting an assert(false) is unreachable, as usually that's what we want
          if (outcome == ProofOutcome.Bottom && obl.Tag == "assert" && obl.Condition.IsConstantFalse())
          {
            outcome = ProofOutcome.True;
          }

          // Avoid reporting any warning on a throw instruction that can be reachable
          if(obl.IsFromThrowInstruction)
          {
            outcome = ProofOutcome.True;
          }

         
          // Add extra information to the error messages and contexts (aka features) to rank the warning
          if(outcome == ProofOutcome.Top)
          {
            BoxedExpression offByOne;
            if(TryGetOffByOne(obl, facts, mdriver, out offByOne))
            {
              extraMessage = string.Format("Is it an off-by-one? The static checker can prove {0} instead", offByOne);

              obl.GetWarningContext.Add(new WarningContext(WarningContext.ContextType.OffByOne));
            }

            if(obl.Tag == "ensures")
            {
              var md = mdriver;
              var mdd = md.MetaDataDecoder;

              if(mdd.IsImplicitImplementation(md.CurrentMethod) || mdd.IsOverride(md.CurrentMethod) || mdd.IsVirtual(md.CurrentMethod))
              {
                obl.GetWarningContext.Add(new WarningContext(WarningContext.ContextType.InheritedEnsures));
              }

              Field field;
              if(TryGetAnObjectInvariant(obl.Condition, out field))
              {
                extraMessage = (extraMessage != null ? extraMessage + ". " : String.Empty)
                  + string.Format("Are you missing an object invariant on field {0}?", mdd.Name(field));

                obl.GetWarningContext.Add(new WarningContext(WarningContext.ContextType.ObjectInvariantNeededForEnsures));
              }
            }
          }

          var witness = obl.GetWitness(outcome);

          switch (outcome)
          {
            #region Report outcome
            case ProofOutcome.Top:
              if (options.PrintOutcome(outcome))
              {
                var lastPC = obl.PC;

                var conditionString = obl.PC.ExtractAssertionCondition();
                if (conditionString != null)
                {
                  conditionString = ": " + conditionString;
                }

                var msg = obl.GetMessageString(outcome, "{0} unproven{1}");

                if(extraMessage != null)
                {
                  msg = string.Format("{0}. {1}", msg, extraMessage);
                }

                output.EmitOutcomeAndRelated(witness, msg, obl.Tag, conditionString);

                if (path != null)
                {
                  if (options.ShowPaths)
                  {
                    path.PrintPathInMethod(output, lastPC.PrimarySourceContext(), mdriver);
                  }
                  if (options.ShowUnprovenObligations)
                  {
                    var pathPC = path.FirstUsablePC;
                    output.WriteLine("{0}: unproven condition: {1}", pathPC.PrimarySourceContext(), path.ObligationStringAtPC(pathPC, mdriver.Context, mdriver.MetaDataDecoder));
                  }
                }
              }
              break;

            case ProofOutcome.Bottom:
              if (options.PrintOutcome(outcome))
              {
                var msg = obl.AddHintsForTheUser(outcome, "{0} unreachable", obl.GetWarningContext);
                output.EmitOutcomeAndRelated(witness, msg, obl.Tag);
              }
              break;

            case ProofOutcome.False:
              if (options.PrintOutcome(outcome))
              {
                var conditionString = obl.PC.ExtractAssertionCondition();
                if (conditionString != null) conditionString = ": " + conditionString;

                string msg;
                if (obl.InferredConditionContainsOnlyEnumValues)
                {
                  // hack hack to remove the string ": false"
                  if (conditionString != null)
                  {
                    const string false_const = "false";
                    var index = conditionString.IndexOf(false_const);
                    if (index != -1)
                    {
                      conditionString = conditionString.Substring(index + false_const.Length);
                    }
                  }
                  msg = "This {0}, always leading to an error, may be reachable. Are you missing an enum case?{1}";
                }
                else
                {
                  msg = obl.GetMessageString(outcome, "{0} is false{1}");
                }

                output.EmitOutcomeAndRelated(witness, msg, obl.Tag, conditionString);
              }
              break;

            case ProofOutcome.True:
              if (options.PrintOutcome(outcome))
              {
                output.EmitOutcomeAndRelated(witness, "{0} is valid", obl.Tag);
              }
              break;
            #endregion
          }
          stats.Add(outcome);
        }
        #endregion

        return stats;
      }

      private static string GetExtraMessageForRedundantPreconditionIfPossible(AssertionObligation<Variable> obl, IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> mdriver)
      {
        // It may be the case we are comparing a struct that redefines opeator==, and then this is used for strc != null  
        if(obl.Condition.IsConstantTrue())
        {
          var mdd = mdriver.MetaDataDecoder;
          foreach(var param in mdd.Parameters(mdriver.CurrentMethod).Enumerate())
          {
            if(mdd.IsStruct(mdd.ParameterType(param)))
            {
              return "Are you comparing a struct value to null?";
            }
          }

        }

        return null;
      }


      private static ProofOutcome TryDischargeViaInference(
        ProofOutcome outcome,
        IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> mdriver,
        ContractInferenceManager inferenceManager,
        IOutputResults output, AssertionObligation<Variable> obl, Set<WarningContext> warningContext, out InferredConditions inferredConditions)
      {
        Contract.Requires(outcome == ProofOutcome.False || outcome == ProofOutcome.Top);
        Contract.Ensures(warningContext.Count >= Contract.OldValue(warningContext.Count)); // can only add contexts;

        var entryOutcome = outcome;

        inferredConditions = null;
        if (!output.IsMasked(obl.GetWitness(ProofOutcome.Top)))
        {
          if (inferenceManager.PreCondition.Inference.TryInferConditions(obl, inferenceManager.CodeFixesManager, out inferredConditions))
          {
            var additionalContext = inferredConditions.PushToContractManager(inferenceManager, inferenceManager.PreCondition.Inference.ShouldAddAssumeFalse, obl, ref outcome, output.LogOptions);

            obl.HasASufficientAndNecessaryCondition = inferredConditions.HasASufficientCondition;

            // We want to override the new outcome under some conditions
            if(entryOutcome != ProofOutcome.Bottom && obl.Condition.IsConstantFalse() && obl.InferredConditionContainsOnlyEnumValues)
            {
              outcome = ProofOutcome.False;
            }

            warningContext.AddRange(additionalContext);
          }
          else if (inferenceManager.PreCondition.Inference.ShouldAddAssumeFalse)
          {
            inferenceManager.Assumptions.AddEntryAssumes(obl, new BoxedExpression[] { BoxedExpression.ConstFalse });
          }
        }
        return outcome;
      }

      private static bool TryGetOffByOne(AssertionObligation<Variable> obl,
        IFactQuery<BoxedExpression, Variable> facts,
        IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> mdriver, 
        out BoxedExpression offByOneExp)
      {
        Contract.Requires(obl != null);
        Contract.Requires(facts != null);
        Contract.Requires(mdriver != null);
        Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out offByOneExp) != null);


        BinaryOperator bop;
        BoxedExpression left, right;
        if(obl.Condition.IsBinaryExpression(out bop, out left, out right) && bop.IsComparisonBinaryOperator())
        {
          var mdd = mdriver.MetaDataDecoder;
          var intType = mdd.System_Int32;
          ProofOutcome outcome;
          int value;
          if(left.IsConstantInt(out value) && right.CanPrintAsSourceCode())
          {
            // left + 1 bop right
            var plusOne = BoxedExpression.Binary(bop,
              BoxedExpression.Binary(BinaryOperator.Add, left, BoxedExpression.Const(1, intType, mdd)),
              right);

            outcome = facts.IsTrue(obl.PC, plusOne);

            if(outcome == ProofOutcome.True)
            {
              offByOneExp = plusOne;
              return true;
            }

            if (value != 0 || !IsLengthOrCount(right))
            {
              // left - 1 bop right
              var minusOne = BoxedExpression.Binary(bop,
                BoxedExpression.Binary(BinaryOperator.Sub, left, BoxedExpression.Const(1, intType, mdd)),
                right);

              outcome = facts.IsTrue(obl.PC, minusOne);

              if (outcome == ProofOutcome.True)
              {
                offByOneExp = minusOne;
                return true;
              }
            }
          }
          if(right.IsConstantInt(out value) && left.CanPrintAsSourceCode())
          {
            // left bop right + 1
            var plusOne = BoxedExpression.Binary(bop,
              left,
              BoxedExpression.Binary(BinaryOperator.Add, right, BoxedExpression.Const(1, intType, mdd)));

            outcome = facts.IsTrue(obl.PC, plusOne);

            if (outcome == ProofOutcome.True)
            {
              offByOneExp = plusOne;
              return true;
            }

            if (value != 0 || !IsLengthOrCount(left))
            {
              // left bop right - 1
              var minusOne = BoxedExpression.Binary(bop,
                left,
                BoxedExpression.Binary(BinaryOperator.Sub, right, BoxedExpression.Const(1, intType, mdd)));

              outcome = facts.IsTrue(obl.PC, minusOne);

              if (outcome == ProofOutcome.True)
              {
                offByOneExp = minusOne;
                return true;
              }
            }
          }
        }

        offByOneExp = null;
        return false;
      }

      private static bool TryGetAnObjectInvariant(BoxedExpression exp, out Field field)
      {
        var variables = exp.Variables();
        if(variables.Any())
        {
          if(variables.All(be => be.AccessPath != null && be.AccessPath[0].IsParameter && be.AccessPath[0].ToString() == "this"))
          {
            // just pick the first one
            var firstOne = variables.PickAnElement().AccessPath;
            for(var i = firstOne.Length-1; i >= 0; i--)
            {
              if(firstOne[i].TryField(out field))
              {
                return true;
              }
            }
          }
        }
        field = default(Field);
        return false;
      }

      private static bool IsABooleanVariableCondition(APC pc, BoxedExpression obl, IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> mdriver)
      {
        Contract.Requires(mdriver != null);
        
        if(obl == null || !obl.IsVariable || !(obl.UnderlyingVariable is Variable))
        {
          return false;
        }

        var type = mdriver.Context.ValueContext.GetType(pc.Post(), (Variable)obl.UnderlyingVariable);
        
        return type.IsNormal && mdriver.MetaDataDecoder.System_Boolean.Equals(type.Value);
      }
      
      private static bool IsLengthOrCount(BoxedExpression exp)
      {
        BinaryOperator bop;
        BoxedExpression left, right;
        if(exp.IsBinaryExpression(out bop, out left, out right))
        {
          return IsLengthOrCount(left) || IsLengthOrCount(right);
        }

        var accessPath = exp.AccessPath;
        int len;
        string name;
        return accessPath != null && (len = accessPath.Length) >= 3 && ((name = accessPath[len - 1].ToString()) == "Length" || name == "Count");
      }

      private static bool IsTracing(IOutputResults output)
      {
        var options = output.LogOptions;
        return options.TraceDFA || options.TraceChecks;
      }

      private static AssertionObligations<Variable> GetAssertions
        (IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> driver,
         out AssertionStatistics stats)
      {
        var analysis = new AssertionCrawlerAnalysis();
        var result = analysis.Gather(driver);

        stats = analysis.Stats;

        return result;
      }

      public struct Data
      {
        public readonly bool IsReached;
        private readonly List<Variable> symbols;
        public bool InsideMonitor /* = false*/;

        public static Data Top
        {
          get
          {
            return new Data(true, false);
          }
        }

        public IEnumerable<Variable> Symbols
        {
          get
          {
            return symbols;
          }
        }

        public Data(bool value, bool insideMonitor)
        {
          this.IsReached = value;
          this.InsideMonitor = insideMonitor;
          this.symbols = new List<Variable>();
        }

        public Data(bool value, bool insideMonitor, List<Variable> renamed)
        {
          Contract.Requires(renamed != null);

          this.IsReached = value;
          this.InsideMonitor = insideMonitor;
          this.symbols = renamed;
        }

        private Data(Data state, Variable v)
        {
          this.IsReached = state.IsReached;
          this.InsideMonitor = state.InsideMonitor;
          this.symbols = new List<Variable>(state.symbols) { v };
          this.InsideMonitor = false;
        }

        public Data AddSymbol(Variable v)
        {
          return new Data(this, v);
        }

        public override string ToString()
        {
          return string.Format("IsReached = {0}, IsInsideMonitor = {1}, <symbols not shown>", this.IsReached, this.InsideMonitor);
        }
      }

      /// <summary>
      /// This crawler's abstract domain is just the boolean lattice interpreted as reachability. As a side effect it 
      /// computes a single list of assertions (no set needed as we just visit each point once)
      /// </summary>
      private class AssertionCrawlerAnalysis
        : MSILVisitor<APC, Local, Parameter, Method, Field, Type, Variable, Variable, Data, Data>,
        IAnalysis<APC, Data, IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Variable, Variable, Data, Data>, IFunctionalMap<Variable, FList<Variable>>>
      {
        #region State

        readonly AssertionObligations<Variable> Obligations = new AssertionObligations<Variable>();

        private bool checkAssumptions;
        private bool checkConditions;

        private bool CheckAssumptionsOrConditions { get { return this.checkAssumptions || this.checkConditions; } }

        private IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> mdriver;

        public AssertionStatistics Stats;

        #endregion

        public AssertionObligations<Variable> Gather(
          IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> mdriver)
        {
          this.mdriver = mdriver;
          this.checkAssumptions = mdriver.Options.CheckAssumptions;
          this.checkConditions = mdriver.Options.CheckConditions;

          var codeLayer = mdriver.ValueLayer;
          var closure = codeLayer.CreateForward<Data>(this, new DFAOptions() { Trace = mdriver.Options.TraceDFA });
          closure(Data.Top);   // Do the analysis 

          this.mdriver = null;

          return Obligations;
        }

        public override Data Assert(APC pc, string tag, Variable condition, object objProvenance, Data data)
        {
          // only add the assert if the pc is not within another contract
          if (pc.InsideNecessaryAssumption || pc.InsideRequiresAtCallInsideContract) return data;

          var methodDriver = this.mdriver;
          if (methodDriver != null)
          {
            var cond = BoxedExpression.Convert(methodDriver.Context.ExpressionContext.Refine(pc, condition), methodDriver.ExpressionDecoder).Simplify(methodDriver.MetaDataDecoder);
            if (cond != null)
            {
              var provenance = objProvenance as Provenance;
              var isFromThrowInstruction = objProvenance != null && objProvenance.Equals("throw instruction");
              var forall = methodDriver.AsForAllIndexed(pc, condition);
              var currMethodName = methodDriver.MetaDataDecoder.Name(methodDriver.CurrentMethod);
              if (forall != null)
              {
                this.Obligations.Add(new AssertionForAllObligation<Variable>(pc, currMethodName, tag, cond, provenance, methodDriver.Options.ShowInferenceTrace, forall, false, false));
              }
              else
              {
                var containsAPinnedVariable = methodDriver.MetaDataDecoder.ContainsAPinnedLocal(methodDriver.CurrentMethod);
                var mayBeASwitchStatement = this.IsComparisonStringToNull(pc, cond) && IsCFGWitAComplexExit();
                // TODO: add proof obligation object for exists
                this.Obligations.Add(new AssertionObligation<Variable>(pc, currMethodName, tag, cond, provenance, methodDriver.Options.ShowInferenceTrace, false, false, isFromThrowInstruction, containsAPinnedVariable, mayBeASwitchStatement));
              }

              this.Stats.AddAssert(pc, tag);
            }
          }
          return data;
        }

        public override Data Assume(APC pc, string tag, Variable condition, object objProvenance, Data data)
        {
          // Just skip some assumptions
          if (data.Symbols.Contains(condition))
          {
            return data;
          }

          var provenance = objProvenance as Provenance;
          var isSourceCodeAssume = (tag == "assume" || tag == "requires" );
          var isConditionCheck = !isSourceCodeAssume && (tag == "true" || tag == "false");

          if (!pc.InsideRequiresAtCallInsideContract && 
            ((this.checkAssumptions && isSourceCodeAssume && !(pc.InsideEnsuresAtCall || pc.InsideInvariantAtCall)) || 
            (this.checkConditions && !pc.InsideContract && tag == "true")))
          {
            var methodDriver = this.mdriver;
            var metadata = methodDriver.MetaDataDecoder;
            var currMethodName = metadata.Name(methodDriver.CurrentMethod);
            if (methodDriver != null)
            {
              var externalCond = methodDriver.Context.ExpressionContext.Refine(pc, condition);
              var cond = BoxedExpression.Convert(externalCond, methodDriver.ExpressionDecoder);
              
              if (cond != null)
              {
                // Hack to get rid of the condition checking for some compiler generated branches but better miss them that give strange warnings
                if (!isSourceCodeAssume && cond.IsConstant && cond.Constant == null)
                {
                  // Sometimes the analysis is too smart, and it replaces a binary exp for null. In this case we want to keep going
                  var condAsSeenFromTheFramework = BoxedExpression.Convert(externalCond, methodDriver.ExpressionDecoder, replaceNull: false);

                  if (condAsSeenFromTheFramework != null && condAsSeenFromTheFramework.IsBinary)
                  {
                    cond = condAsSeenFromTheFramework;
                  }
                  else
                  {
                    // skip it
                    return data;
                  }
                }

                var pcWithSourceContext = pc.HasRealSourceContext ? pc : pc.GetFirstPredecessorWithSourceContext(methodDriver.CFG);

                // Hack to get rid of the condition checking for "using " statements. Probably this is too aggressive, but better miss them that give strange warnings
                var sr = pcWithSourceContext.SubroutineContext;
                if (sr != null && sr.Head.Three == "finally")
                {
                  // skip it
                  return data;
                }

                cond = cond.Simplify(methodDriver.MetaDataDecoder);

                // We want to filter false warnings
                if(isConditionCheck)
                {
                  // Skip warnings inside Dispose methods
                  if (currMethodName == "Dispose"  // Fast check...
                    && metadata.ImplementedMethods(mdriver.CurrentMethod).Any(m => metadata.FullName(m) == "System.IDisposable.Dispose()") // More costly check
                    )
                  {
                    goto skip;
                  }

                  // skip checks of enum values
                  Predicate<Variable> pred = v => IsVariableOfEnumType(pc.Post(), v);
                  if(cond.IsCheckOfEnumValue(pred))
                  {
                    goto skip;
                  }

                  // Skip trivial conditionals "true" and "false"
                  bool booleanConst;
                  if(cond.IsTrivialCondition(out booleanConst))
                  {
                    goto skip;
                  }

                  // Skip compiler generated Booleans
                  // TODO: Should we check all the variables for more complex expressions?
                  if(cond.IsVariable && cond.AccessPath == null)
                  {
                    goto skip;
                  }

                  var condVariables = cond.Variables();

                  // If we can't read one of the vars, just skip it
                  if(condVariables.Count == 0 || condVariables.Any(exp => exp.AccessPath == null) || cond.Variables<Variable>().Count != condVariables.Count)
                  {
                    goto skip;
                  }

                  // Skip conditionals involving static variables, as we know in Clousot are set to != null by default
                  if(condVariables.Any(exp => exp.AccessPath.ContainsStaticFieldAccess<Field>()))
                  {
                    goto skip;
                  }

                  // Skip instance fields if inside a monitor
                  if(data.InsideMonitor && condVariables.Any(exp => exp.AccessPath == null || exp.AccessPath.Length >= 3) 
                    /*&& condVariables.Any(exp => exp.HasVariableRootedInThis())*/)
                  {
                    goto skip;
                  }

                  // If we reached this point, we are positive it's something we may want to show to the user.
                  // We are only carfull to show "x != null"  instead of "x"
                  Variable varForCond;
                  if (cond.IsVariable && cond.TryGetFrameworkVariable(out varForCond))
                  {
                    var t = this.mdriver.Context.ValueContext.GetType(pc.Post(), varForCond);
                    if (t.IsNormal)
                    {
                      if (this.mdriver.MetaDataDecoder.IsReferenceType(t.Value))
                      {
                        cond = BoxedExpression.Binary(BinaryOperator.Cne_Un, cond, BoxedExpression.Const(null, t.Value, this.mdriver.MetaDataDecoder));
                      }
                      else
                      {
                        cond = BoxedExpression.Binary(BinaryOperator.Cne_Un, cond, BoxedExpression.Const(this.mdriver.MetaDataDecoder.System_Boolean.Equals(t.Value) ? (object)false : (object)0, t.Value, this.mdriver.MetaDataDecoder));
                      }
                    }
                    else
                    {
                      goto skip;
                    }
                  }

                  cond = cond.Negate().Simplify(this.mdriver.MetaDataDecoder);
                }

                var forall = methodDriver.AsForAllIndexed(pc, condition);
                if (forall != null)
                {
                  this.Obligations.Add(new AssertionForAllObligation<Variable>(pcWithSourceContext, currMethodName, tag, cond, provenance, methodDriver.Options.ShowInferenceTrace, forall, isSourceCodeAssume, isConditionCheck));
                }
                else
                {
                  var containsAPinnedVariable = methodDriver.MetaDataDecoder.ContainsAPinnedLocal(methodDriver.CurrentMethod);
                  var mayBeASwitchStatement = IsComparisonStringToNull(pc, cond) && IsCFGWitAComplexExit();

                  // TODO: add proof obligation object for exists
                  this.Obligations.Add(new AssertionObligation<Variable>(pcWithSourceContext, currMethodName, tag, cond, provenance, methodDriver.Options.ShowInferenceTrace, isSourceCodeAssume, isConditionCheck, false, containsAPinnedVariable, mayBeASwitchStatement));
                }
              }
            }
          }

skip:

          this.Stats.AddAssume(pc, tag);

          return data;
        }

        public override Data Call<TypeList, ArgList>(APC pc, Method method, bool tail, bool virt, TypeList extraVarargs, Variable dest, ArgList args, Data data)
        {
          var md = this.mdriver.MetaDataDecoder;
          var name = md.Name(method);

          switch (name)
          {
            case "IsDefined":
              {
                var typeName = md.Name(md.DeclaringType(method));
                if (typeName == "Enum")
                {
                  return data.AddSymbol(dest);
                }
              }
              break;

            case "Enter":
              {
                data.InsideMonitor = md.Name(md.DeclaringType(method)) == "Monitor";
              }
              break;

            case "Exit":
              {
                if (data.InsideMonitor)
                {
                  data.InsideMonitor = md.Name(md.DeclaringType(method)) == "Monitor";
                }
              }
              break;
          }
          return data;
        }

        protected override Data Default(APC pc, Data data)
        {
          return data;
        }

        public Data EdgeConversion(APC from, APC next, bool joinPoint, IFunctionalMap<Variable, FList<Variable>> edgeData, Data newState)
        {
          if (edgeData == null)
          {
            // it's the identity
            return newState;
          }

          var result = new List<Variable>();
          foreach (var sv in newState.Symbols)
          {
            if (edgeData.Contains(sv))
            {
              result.AddRange(edgeData[sv].GetEnumerable());
            }
          }

          return new Data(newState.IsReached, newState.InsideMonitor, result);
        }

        public Data Join(Pair<APC, APC> edge, Data newState, Data prevState, out bool weaker, bool widen)
        {
          // TODO: the join of the states, and then the check
          // If we do it, then we have also to be carefull when we add a new proof obligation, because we may add it many times
          weaker = false;
          return prevState;
        }

        public Data MutableVersion(Data state)
        {
          return state;
        }

        public Data ImmutableVersion(Data state)
        {
          return state;
        }

        public void Dump(Pair<Data, System.IO.TextWriter> pair)
        {
          Console.WriteLine(pair.One);
        }

        public bool IsBottom(APC pc, Data state)
        {
          return !state.IsReached;
        }

        public bool IsTop(APC pc, Data state)
        {
          return false;
        }

        public IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Variable, Variable, Data, Data> Visitor()
        {
          return this;
        }

        public Predicate<APC> CacheStates(IFixpointInfo<APC, Data> fixpointInfo)
        {
          return null;
        }


        #region Private

        private bool IsVariableOfEnumType(APC pc, Variable v)
        {
          var md = this.mdriver;
          FlatDomain<Type> t = md.Context.ValueContext.GetType(pc.Post(), v);
          return t.IsNormal && md.MetaDataDecoder.IsEnumWithoutFlagAttribute(t.Value);
        }

        private bool IsComparisonStringToNull(APC pc, BoxedExpression condition)
        {
          BinaryOperator bop;
          BoxedExpression left, right;
          if(condition.IsBinaryExpression(out bop, out left, out right))          
          {
            var md = this.mdriver;
            FlatDomain<Type> t; 
            // using pc.Post seems not to work with Switch statements!!!
            if(left.IsNull)
            {
              return right.UnderlyingVariable is Variable && (t = md.Context.ValueContext.GetType(pc, (Variable)right.UnderlyingVariable)).IsNormal && md.MetaDataDecoder.System_String.Equals(t.Value);
            }
            if(right.IsNull)
            {
              return left.UnderlyingVariable is Variable && (t = md.Context.ValueContext.GetType(pc, (Variable)left.UnderlyingVariable)).IsNormal && md.MetaDataDecoder.System_String.Equals(t.Value);
            }
          }

          return false;
        }

        private bool IsCFGWitAComplexExit()
        {
          var cfg = this.mdriver.Context.MethodContext.CFG;

          return cfg.Predecessors(cfg.NormalExit).Count() >= 3;
        }

        #endregion
      }
    }

    public class AssertionObligations<Variable>
       : IProofObligations<Variable, BoxedExpression>,
        IEnumerable<AssertionObligation<Variable>>
    {
      #region Invariant

      [ContractInvariantMethod]
      private void ObjectInvariant()
      {
        Contract.Invariant(this.assertions != null);
      }

      #endregion

      #region State

      readonly private List<AssertionObligation<Variable>> assertions;

      #endregion

      #region Constructor
      public AssertionObligations()
      {
        this.assertions = new List<AssertionObligation<Variable>>();
      }

      #endregion

      #region Add

      public void Add(AssertionObligation<Variable> obl)
      {
        this.assertions.Add(obl);
      }

      #endregion

      #region IProofObligations<Variable,Expression> Members

      public string Name
      {
        get { return "Assertion obligation"; }
      }

      public double Validate(IOutputResults output, ContractInferenceManager preManager, IFactQuery<BoxedExpression, Variable> query)
      {
        foreach (var obl in this.assertions)
        {
          obl.Validate(query, preManager, output);
        }
        return 0.0;
      }

      public bool PCWithProofObligation(APC pc)
      {
        foreach (var obl in this.assertions)
        {
          if (pc.Equals(obl.PC))
            return true;
        }

        return false;
      }

      public bool PCWithProofObligations(APC pc, List<ProofObligationBase<BoxedExpression, Variable>> conditions)
      {
        var count = 0;
        foreach (var obl in this.assertions)
        {
          if (pc.Equals(obl.PC))
          {
            conditions.Add(obl);
            count++;
          }
        }

        return count != 0;
      }

      public AnalysisStatistics Statistics
      {
        get { return default(AnalysisStatistics); }
      }

      #endregion

      #region IEnumerable<ProofObligationBase<BoxedExpression,Variable>> Members

      public IEnumerator<AssertionObligation<Variable>> GetEnumerator()
      {
        return this.assertions.GetEnumerator();
      }

      #endregion

      #region IEnumerable Members

      System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
      {
        return this.GetEnumerator();
      }

      #endregion
    } 
  }


  public struct AssertionStatistics
  {
    private struct Values
    {
      public int Requires;
      public int Ensures;
      public int Asserts;
      public int Invariants;

      public void Add(APC pc, string tag)
      {
        switch (tag)
        {
          case "requires": Requires++; break;
          case "ensures": Ensures++; break;
          case "assert": Asserts++; break;
          case "invariant": Invariants++; break;
          default:
            break;
        }
      }
      public void Add(Values that)
      {
        this.Requires += that.Requires;
        this.Ensures += that.Ensures;
        this.Asserts += that.Asserts;
        this.Invariants += that.Invariants;
      }
    }

    Values Asserts;
    Values Assumes;

    public void AddAssert(APC pc, string tag)
    {
      this.Asserts.Add(pc, tag);
    }

    public void AddAssume(APC pc, string tag)
    {
      this.Assumes.Add(pc, tag);
    }

    public void Add(AssertionStatistics that)
    {
      this.Asserts.Add(that.Asserts);
      this.Assumes.Add(that.Assumes);
    }

    public void Show(IOutput output)
    {
      output.WriteLine("Assertions");
      if (Asserts.Requires > 0) output.WriteLine("  {0} requires", Asserts.Requires.ToString());
      if (Asserts.Ensures > 0) output.WriteLine("  {0} ensures", Asserts.Ensures.ToString());
      if (Asserts.Asserts > 0) output.WriteLine("  {0} asserts", Asserts.Asserts.ToString());
      if (Asserts.Invariants > 0) output.WriteLine("  {0} invariants", Asserts.Invariants.ToString());
      output.WriteLine("Assumptions");
      if (Assumes.Requires > 0) output.WriteLine("  {0} requires", Assumes.Requires.ToString());
      if (Assumes.Ensures > 0) output.WriteLine("  {0} ensures", Assumes.Ensures.ToString());
      if (Assumes.Invariants > 0) output.WriteLine("  {0} invariants", Assumes.Invariants.ToString());
    }

    public void ShowAverage(IOutput output, int methodCount)
    {
      output.WriteLine("Assertions");
      output.WriteLine("  {0} requires", Average(Asserts.Requires, methodCount));
      output.WriteLine("  {0} ensures", Average(Asserts.Ensures, methodCount));
      output.WriteLine("  {0} asserts", Average(Asserts.Asserts, methodCount));
      output.WriteLine("  {0} invariants", Average(Asserts.Invariants, methodCount));
      output.WriteLine("Assumptions");
      output.WriteLine("  {0} requires", Average(Assumes.Requires, methodCount));
      output.WriteLine("  {0} ensures", Average(Assumes.Ensures, methodCount));
      output.WriteLine("  {0} invariants", Average(Assumes.Invariants, methodCount));
    }

    string Average(int a, int total)
    {
      if (total == 0) return "0";
      float result = ((float)a) / total;
      return result.ToString();
    }
  }
}

