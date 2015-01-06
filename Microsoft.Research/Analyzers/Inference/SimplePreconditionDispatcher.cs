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
using System.Text;
using System.Diagnostics.Contracts;
using Microsoft.Research.CodeAnalysis.Inference;
using System.Linq;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Research.DataStructures;

namespace Microsoft.Research.CodeAnalysis
{
  /// <summary>
  /// Base class to share functionalities among different analyses
  /// </summary>
  abstract public class SimpleDispatcherBase<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions>
    where LogOptions : IFrameworkLogOptions
    where Variable : IEquatable<Variable>
    where Expression : IEquatable<Expression>
    where Type : IEquatable<Type>
  {
    #region Invariant

    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(this.mdriver != null);
      Contract.Invariant(this.output != null);
    }
    #endregion

    #region State
    readonly protected IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions> mdriver;
    readonly protected IOutputResults output;
    readonly protected bool allowDisjunctivePreconditions;
    #endregion

    #region Constructor
    protected SimpleDispatcherBase(IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions> mdriver,
      IOutputResults output, bool allowDisjunctivePreconditions)
    {
      Contract.Requires(mdriver != null);
      Contract.Requires(output != null);

      this.mdriver = mdriver;
      this.output = output;
      this.allowDisjunctivePreconditions = allowDisjunctivePreconditions;
    }
    #endregion

    #region abstract

    abstract protected string ContractTemplate { get; }

    #endregion

    #region Shared methods

    protected string MakeconditionString(string condition)
    {
      Contract.Ensures(Contract.Result<string>() != null);

      return string.Format(ContractTemplate, condition);
    }

    protected delegate bool PreconditionChecker(BoxedExpression simplified, ProofOutcome originalOutcome, out ProofOutcome outcome);

    protected bool CheckPrecondition(BoxedExpression simplified, ProofOutcome originalOutcome, out ProofOutcome outcome)
    {
      Contract.Requires(simplified != null);

      outcome = originalOutcome;

      if (this.output.LogOptions.PropagateInferredRequires(mdriver.MetaDataDecoder.IsPropertyGetterOrSetter(mdriver.CurrentMethod)))
      {
        if (this.output.LogOptions.PropagatedRequiresAreSufficient)
          outcome = ProofOutcome.True;
      }

      return true;
    }

    protected ProofOutcome GenericAddPreconditions(ProofObligation obl, IEnumerable<BoxedExpression> preconditions, ProofOutcome originalOutcome,
      InferenceDB preconditionDB, PreconditionChecker checkPrecondition)
    {
      var outcome = originalOutcome;

      foreach (var pre in preconditions)
      {
        if (pre == null)
          continue;

        // 1. Simplify the expression
        var simplified = SimplifyAndFix(obl.PCForValidation, pre);

        if (simplified == null)
        {
          continue;
        }

        if (DisjunctivePreconditionsAreDisallowed(simplified))
        {
          continue;
        }

        // 2. Have we already seen this precondition?
        if (preconditionDB.TryLookUp(obl, simplified, out outcome))
        {
          continue;
        }
        else
        {
          outcome = originalOutcome;
        }

        // 3. See if it is valid
        if (checkPrecondition(simplified, outcome, out outcome))
        {
          preconditionDB.Add(obl, simplified, outcome);
        }
      }

      return outcome;
    }

    [Pure]
    protected bool DisjunctivePreconditionsAreDisallowed(BoxedExpression exp)
    {
      Contract.Requires(exp != null);

      return !this.allowDisjunctivePreconditions && exp.IsBinary && exp.BinaryOp == BinaryOperator.LogicalOr;
    }

    private BoxedExpression SimplifyAndFix(APC pc, BoxedExpression precondition)
    {
      Contract.Requires(precondition != null);

      var decompiler = new AnalysisWrapper.TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>.BooleanExpressionsDecompiler<LogOptions>(mdriver);

      BoxedExpression result;
      if (decompiler.FixIt(pc, precondition, out result))
      {
        return result.Simplify(this.mdriver.MetaDataDecoder);
      }
      else
      {
        return null; // doesn't type check and is not fixable, ignore
      }
    }
    #endregion
  }


  [ContractVerification(true)]
  public class SimplePreconditionDispatcher<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions>
    : SimpleDispatcherBase<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions>
    , IPreconditionDispatcher
    where LogOptions : IFrameworkLogOptions
    where Variable : IEquatable<Variable>
    where Expression : IEquatable<Expression>
    where Type : IEquatable<Type>
  {
    #region Invariant

    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(this.preconditions != null);
      Contract.Invariant(this.numberOfWarnings >= 0);
    }

    #endregion

    #region State

    protected override string ContractTemplate { get { return "Contract.Requires({0});"; } }

    readonly private InferenceDB preconditions;
    readonly private Func<BoxedExpression, bool> shouldFilter;

    private int numberOfWarnings = 0;

    #endregion

    #region Constructor

    /// <param name="extraFilter"> extraFilter(e) == true if e should not be suggested</param>
    public SimplePreconditionDispatcher(
      IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions> mdriver,
      IOutputResults output, bool allowDisjunctivePreconditions,
      Func<BoxedExpression, bool> extraFilter = null)
      : base(mdriver, output, allowDisjunctivePreconditions)
    {
      Contract.Requires(mdriver != null);
      Contract.Requires(output != null);

      this.preconditions = new InferenceDB(exp => exp.Simplify(mdriver.MetaDataDecoder), this.IsSuitableInRequires);
      this.shouldFilter = extraFilter;
    }

    #endregion

    #region IPreconditionDispatcher Members

    public ProofOutcome AddPreconditions(ProofObligation obl, IEnumerable<BoxedExpression> preconditions, ProofOutcome originalOutcome)
    {
      return this.GenericAddPreconditions(obl, preconditions, originalOutcome, this.preconditions, this.CheckPreconditionAndCanAddRequires);
    }

    public IEnumerable<KeyValuePair<BoxedExpression, IEnumerable<MinimalProofObligation>>> GeneratePreconditions()
    {
      return preconditions.GenerateConditions();
    }

    [SuppressMessage("Microsoft.Contracts", "TestAlwaysEvaluatingToAConstant")]
    public int SuggestPreconditions(bool treatSuggestionsAsWarnings, bool forceSuggestionOutput)
    {
      var md = this.mdriver;
      var mdd = md.MetaDataDecoder;
      var pc = md.CFG.GetPCForMethodEntry(50);

      var count = 0;

      foreach (var pair in this.GeneratePreconditions())
      {
        var precondition = pair.Key;
        var provenance = pair.Value;

        if (this.shouldFilter != null && this.shouldFilter(precondition))
        {
          // Skip the precondition, because the external filter decided so
#if DEBUG
          Console.WriteLine("Skipping the suggestion of the precondition {0}, as the external filter decided so", precondition);
#endif
          continue;
        }

        if (precondition.IsConstantFalse())
        {
          if (mdriver.AdditionalSyntacticInformation.HasThrow)
          {
            // do nothing...
            // We are very conservative, and if we inferred false, and the method has at least one throw, then we do not say anything...
          }
          else
          {
            var witness = MakeUpAWitness(pc, WarningType.FalseRequires, precondition, pair);
            var isConstructor = mdd.IsConstructor(md.CurrentMethod);

            string msg;

            if (IsFromInvariant(provenance))
            {
              msg = String.Format("Invoking {0} '{1}' will always lead to a violation of an (inferred) object invariant",
               isConstructor ? "constructor" : "method",
               isConstructor ? mdd.FullName(md.CurrentMethod) : mdd.Name(md.CurrentMethod));
            }
            else
            {
              msg = String.Format("Invoking {0} '{1}' will always lead to an error. If this is wanted, consider adding Contract.Requires(false) to document it",
               isConstructor ? "constructor" : "method",
               isConstructor ? mdd.FullName(md.CurrentMethod) : mdd.Name(md.CurrentMethod));
            }
            output.EmitOutcome(witness, msg);

            numberOfWarnings++;
          }
        }
        else
        {
          // When sugegstion preconditions, we want to avoid showing to the user those originated by treating e.g. "throw new ArgumentException" as "assert false".
          // But we want to propagate them!!!!
          if (provenance.Any(obl => obl.IsFromThrowInstruction))
          {
            continue;
          }

          var conditionString = MakeconditionString(precondition.ToString());
          var overrideToAssume = false;
          if (treatSuggestionsAsWarnings)
          {
            var pcForWarning = pc;
            if (!pcForWarning.HasRealSourceContext)
            {
              pcForWarning = this.mdriver.CFG.Entry;
            }

            Witness witness;
            string msg;
            BoxedExpression readonlyField;

            if (this.ContainsAReadonlyField(precondition, out readonlyField))
            {
              witness = MakeUpAWitness(pcForWarning, WarningType.MissingPreconditionInvolvingReadonly, precondition, pair);
              conditionString = conditionString.Replace("Requires", "Assume");
              var msg0 = string.Format("You are making an assumption on the value of {0} in an externally visible method. ", 
                readonlyField != null && readonlyField.CanPrintAsSourceCode()? 
                ("the readonly field " + readonlyField.ToString()) : "a readonly field");
              msg = msg0 + "Consider adding {0} to document it, or turning the field into a (auto)property";

              overrideToAssume = true;
            }
            else
            {
              witness = MakeUpAWitness(pcForWarning, WarningType.MissingPrecondition, precondition, pair);
              msg = "Missing precondition in an externally visible method. Consider adding {0} for parameter validation";
            }
            // Provide better error messages
            var obligationsFromInferredInvariants = provenance.Where(obl => obl.ObligationName == "invariant" && obl.Provenance != null && obl.Provenance.Any()).ToArray();
            var obligationsFromContracts = provenance.Where(obl => obl.ObligationName == "requires" && obl.Provenance != null && obl.Provenance.Any()).ToArray();
            if (obligationsFromInferredInvariants.Any())
            {
              var provenanceOfTheFirstOne = obligationsFromInferredInvariants.First().Provenance;
              if (provenanceOfTheFirstOne != null && provenanceOfTheFirstOne.Any())
              {
                var defininingMethod = provenanceOfTheFirstOne.First().definingMethod;
                if (!string.IsNullOrEmpty(defininingMethod))
                {
                  msg += string.Format(". Otherwise invoking {0} (in the same type) may cause an error", defininingMethod);
                  witness.AddWarningContext(new WarningContext(WarningContext.ContextType.InterproceduralPathToError, 1));
                }
                else
                {
                  // TODO?
                }
              }
            }
            else if (obligationsFromContracts.Any())
            {
              var pathLen = 0;
              var sequenceOfMethodCalls = new List<string>();
              var nextObligation = obligationsFromContracts.First(); 
              
              while(nextObligation != null && nextObligation.definingMethod != null) 
              {
                sequenceOfMethodCalls.Add(nextObligation.definingMethod);
                pathLen++;


                // compute the next path to visit in the tree
                if(nextObligation.Provenance != null)
                {
                  // As aan heuristic, we prefer following the requires chain
                  var candidateNext = nextObligation.Provenance.Where(next => next.ObligationName == "requires" && next.Provenance != null && next.Provenance.Any()).Select(next => next.MinimalProofObligation).FirstOrDefault();
                  if(candidateNext != null)
                  {
                    nextObligation = candidateNext;
                  }
                  else
                  {
                    nextObligation = nextObligation.Provenance.FirstOrDefault().MinimalProofObligation;
                  }
                }
                else
                {
                  nextObligation = null; // kill the loop;
                }

              }
              msg += string.Format(". Otherwise the following sequence of method calls may cause an error. Sequence: {0}", sequenceOfMethodCalls.ToString(" -> "));
              witness.AddWarningContext(new WarningContext(WarningContext.ContextType.InterproceduralPathToError, pathLen));
            }
            output.EmitOutcome(witness, msg, conditionString);

            numberOfWarnings++;
          }

          if (!treatSuggestionsAsWarnings || forceSuggestionOutput)
          {
            // If we decided that we want not a Requires, but an Assume
            if (overrideToAssume)
            {
              output.Suggestion(ClousotSuggestion.Kind.AssumeOnEntry, ClousotSuggestion.Kind.AssumeOnEntry.Message(),
                pc, conditionString, this.preconditions.CausesFor(precondition), ClousotSuggestion.ExtraSuggestionInfo.None);
            }
            else
            {
              output.Suggestion(ClousotSuggestion.Kind.Requires, ClousotSuggestion.Kind.Requires.Message(),
                pc, conditionString, this.preconditions.CausesFor(precondition), ClousotSuggestion.ExtraSuggestionInfo.None);
            }
          }
        }
        count++;
      }

      return count;
    }

    public int NumberOfWarnings
    {
      get { return this.numberOfWarnings; }
    }

    private Witness MakeUpAWitness(APC pc, WarningType type, BoxedExpression precondition, KeyValuePair<BoxedExpression, IEnumerable<MinimalProofObligation>> pair)
    {
      uint? id;
      var provenance = pair.Value;
      if (provenance != null && provenance.Any())
      {
        id = provenance.First().ID;
      }
      else
      {
        id = null;
      }

      var md = this.mdriver;
      var warningContext = WarningContextFetcher.InferContext(pc, precondition, md.Context, md.MetaDataDecoder.IsBoolean)
        .Where(wc => wc.Type != WarningContext.ContextType.ViaParameterUnmodified); // very likely we will have unmodified parameters, and we do not want them to count in the score

      return new Witness(id, type, ProofOutcome.Top, pc, warningContext);
    }

    private bool ContainsAReadonlyField(BoxedExpression exp, out BoxedExpression readonlyField)
    {
      foreach(var v in exp.Variables())
      {
        var accessPath = v.AccessPath;
        if(accessPath != null)
        {
          for(var i = accessPath.Length-1; 0 <= i; i--)
          {
            var element = accessPath[i];

            // Should be impossible
            if(element == null)
            {
              readonlyField = null;
              return false;
            }

            if (element.IsDeref)
            {
              continue;
            }
            Field f;
            if(element.TryField(out f) && this.mdriver.MetaDataDecoder.IsReadonly(f))
            {
              readonlyField = v;
              return true;
            }
            break;
          }
        }
      }
      readonlyField = null;
      return false;
    }

    public int PropagatePreconditions(bool asPrecondition)
    {
      var md = this.mdriver;
      var pc = md.CFG.GetPCForMethodEntry();
      var count = 0;

      foreach (var pair in this.GeneratePreconditions())
      {
        var precondition = pair.Key;
        var provenanceUntyped = pair.Value as IEnumerable<ProofObligation>;
        Contract.Assume(provenanceUntyped != null);

        // Filter the preconditions "false" in constructors, when the preconditions derives from an object invariant
        if (precondition.IsConstantFalse() && provenanceUntyped.Any(obligation => obligation.ObligationName == "invariant") && md.MetaDataDecoder.IsConstructor(md.CurrentMethod))
        {
          continue;
        }

        // We want the minimal proof obligation to avoid carrying around pointers to the heap graph, context, etc.
        var provenanceWithMinimalProofObligations = new List<MinimalProofObligation>();
        foreach (var obl in provenanceUntyped)
        {
          provenanceWithMinimalProofObligations.Add(obl.MinimalProofObligation);
        }

        if (asPrecondition)
        {
          md.AddPreCondition(precondition, pc, provenanceWithMinimalProofObligations);
          count++;
        }
        else
        {
          md.AddEntryAssume(precondition, provenanceWithMinimalProofObligations);
        }
      }

      return count;
    }

    #endregion

    #region Private

    public bool CheckPreconditionAndCanAddRequires(BoxedExpression condition, ProofOutcome pre, out ProofOutcome outcome)
    {
      Contract.Requires(condition != null);

      if (!this.mdriver.CanAddRequires())
      {
        outcome = pre;
        return false;
      }

      return base.CheckPrecondition(condition, pre, out outcome);
    }

    [Pure]
    static private bool IsFromInvariant(IEnumerable<MinimalProofObligation> provenance)
    {
      if (provenance == null)
      {
        return false;
      }
      var p = provenance.FirstOrDefault();
      if (p == null)
      {
        return false;
      }

      return p.ObligationName == "invariant";
    }

    [Pure]
    private bool IsSuitableInRequires(BoxedExpression exp)
    {
      if(exp == null)
      {
        return false;
      }

      var md = this.mdriver;
      var context = md.Context.ValueContext;
      var entry = md.CFG.EntryAfterRequires;

      foreach(var variable in exp.Variables())
      {
        if(variable != null && variable.AccessPath != null)
        {
          var accessPath = variable.AccessPath.ToFList();
           
          if(!context.IsRootedInParameter(accessPath))
          {
            return false;
          }
        }
      }

      return true;
    }    

    #endregion
  }
}
