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
using System.Linq;
using Microsoft.Research.DataStructures;
using System.Text;
using System.Diagnostics.Contracts;

namespace Microsoft.Research.CodeAnalysis
{
  using Provenance = IEnumerable<ProofObligation>;

  /// <summary>
  /// The base class for proof obligations
  /// </summary>
  [ContractClass(typeof(ProofObligationBaseContracts<,>))]
  public abstract class ProofObligationBase<Expression, Variable> : ProofObligation
  {
    #region Invariant
    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(this.AdditionalInformationOnTheWarning != null);
    }

    #endregion

    #region State

    // By default we try to infer a precondition
    protected virtual bool TryInferPrecondition { get { return true; } }

    // Some additional piece of information that can improve the error reporting    
    protected Set<WarningContext> AdditionalInformationOnTheWarning { get; private set; }
    
    private ProofOutcome? outcome;
    virtual public ProofOutcome? Outcome
    {
      get
      {
        return this.outcome;
      }
      protected set
      {
        Contract.Requires(value.HasValue);

        this.outcome = value;
      }
    }

    #endregion

    #region Constructor
    public ProofObligationBase(APC pc, string definingMethod, Provenance provenance)
      : base(pc, definingMethod, provenance)
    {
      this.AdditionalInformationOnTheWarning = new Set<WarningContext>();
    }
    #endregion

    #region Main Validate loop

    public ProofOutcome Validate(IFactQuery<Expression, Variable> query, ContractInferenceManager inferenceManager, IOutputResults output)
    {
      ProofOutcome outcome;
      if (query.IsUnreachable(this.PCForValidation))
      {
        outcome = ProofOutcome.Bottom;
      }
      else
      {
        outcome = ValidateInternal(query, inferenceManager, output);

        if (outcome == ProofOutcome.Top || outcome == ProofOutcome.False)
        {
          if (this.Condition != null && this.TryInferPrecondition && !output.IsMasked(this.GetWitness(outcome)))
          {
            InferredConditions inferredPreConditions;

            var inferencer = inferenceManager.PreCondition.Inference;
            if (inferencer.TryInferConditions(this, inferenceManager.CodeFixesManager, out inferredPreConditions))
            {
              var context = inferredPreConditions.PushToContractManager(inferenceManager, inferencer.ShouldAddAssumeFalse,  this, ref outcome, output.LogOptions);

              this.HasASufficientAndNecessaryCondition = inferredPreConditions.HasASufficientCondition;
              this.AdditionalInformationOnTheWarning.AddRange(context);
            }
            else if (inferencer.ShouldAddAssumeFalse)
            {
              inferenceManager.Assumptions.AddEntryAssumes(this, new BoxedExpression[] { BoxedExpression.ConstFalse });
            }
          }
        }
      }

      this.Outcome = outcome;

      return outcome;
    }

    #endregion

    #region Abstract methods

    /// <summary>
    /// Returns an expression standing for the condition encoded by this proof obligation.
    /// Can be null if the condition is not expressible as a BoxedExpression
    /// </summary>
    public override abstract BoxedExpression Condition { get; }
    
    /// <summary>
    /// Try to validate the embodied proof obligation and return the outcome.
    /// The IFactQuery interface is for future logic based implementation of fact querying.
    /// The validation can make queries or use internal state to answer the outcome.
    /// </summary>
    protected abstract ProofOutcome ValidateInternal(IFactQuery<Expression,Variable> query, ContractInferenceManager inferenceManager , IOutputResults output);

    /// <summary>
    /// Prints out the outcome of the embodied proof obligation on the given output.
    /// Note, the code should not make decisions about when to print, as this is done
    /// by the context.
    /// </summary>
    public abstract void EmitOutcome(ProofOutcome outcome, IOutputResults output);

    public abstract Witness GetWitness(ProofOutcome outcome);

    /// <summary>
    /// Sometimes we may need to know which kind of proof obligation it is 
    /// </summary>
    public override abstract string ObligationName { get; }
    /// <summary>
    /// Called from EmitOutcome.
    /// A subclass should implement it to add new facts to the warning context
    /// </summary>
    abstract protected void PopulateWarningContextInternal(ProofOutcome outcome);

    #endregion

    #region Populate warning contexts

    public bool HasAnInferredCondition
    {
      get
      {
        return this.HasASufficientAndNecessaryCondition || this.AdditionalInformationOnTheWarning.Any(wc => wc.Type == WarningContext.ContextType.InferredCalleeAssume || wc.Type == WarningContext.ContextType.InferredEntryAssume || wc.Type == WarningContext.ContextType.InferredObjectInvariant || wc.Type == WarningContext.ContextType.InferredPrecondition);
      }
    }

    protected void PopulateWarningContext(ProofOutcome outcome)
    {
      this.AddWarningContextFromCodeFixesAndInference(this.AdditionalInformationOnTheWarning);
      this.PopulateWarningContextInternal(outcome);
      this.AdditionalInformationOnTheWarning = Summarize(this.AdditionalInformationOnTheWarning);
    }
    
    protected void AddWarningContextFromCodeFixesAndInference(Set<WarningContext> info)
    {
      if (this.HasCodeFix)
      {
        foreach (var fix in this.codeFixes)
        {
          switch (fix.Kind)
          {
            case CodeFixKind.ConstantInitialization:
            case CodeFixKind.ExpressionInitialization:
                {
                  info.Add(new WarningContext(WarningContext.ContextType.CodeRepairThinksWrongInitialization));
                }
              break;
            case CodeFixKind.Test:
            case CodeFixKind.ArrayOffByOne:
              {
                info.Add(new WarningContext(WarningContext.ContextType.CodeRepairLikelyFixingABug));
              }
              break;

            case CodeFixKind.AssumeMethodResult:
            case CodeFixKind.MethodCallResult:
            case CodeFixKind.MethodCallResultNoCode:
            case CodeFixKind.BaselineAssume:
              {
                info.Add(new WarningContext(WarningContext.ContextType.ViaMethodCall));
              }
              break;

            case CodeFixKind.AssumeLocalInitialization:
              {
                info.Add(new WarningContext(WarningContext.ContextType.CodeRepairLikelyFoundAWeakenessInClousot));               
              }
              break;

            default:
              {
                // do nothing?
                break;
              }
          }
        }
      }
      if (this.HasASufficientAndNecessaryCondition)
      {
        this.AdditionalInformationOnTheWarning.Add(new WarningContext(WarningContext.ContextType.InferredConditionIsSufficient));
      }
    }

    #endregion

    #region Instance Methods
    /// <summary>
    /// We cache the information if some analysis was already able to validate the proof obligation
    /// </summary>
    public bool IsAlreadyValidated
    {
      get
      {
        return this.Outcome.HasValue;
      }
    }


    #endregion

    #region Static methods
    // We have this static as we want to share some code with the AssertionCrawler
    public string AddHintsForTheUser(ProofOutcome outcome, string format, Set<WarningContext> context)
    {
      Contract.Requires(format != null);
      Contract.Ensures(Contract.Result<string>() != null);

      return outcome != ProofOutcome.True ? SuggestAddingPrecondition(format, context) : format;
    }

    protected string AddHintsForTheUser(ProofOutcome outcome, string format)
    {
      Contract.Requires(format != null);
      Contract.Ensures(Contract.Result<string>() != null);

      return AddHintsForTheUser(outcome, format, this.AdditionalInformationOnTheWarning);
    }

    /// <summary>
    /// If we found evidence that this proof obligation can be discharged with a precondition, then we suggest it to the user
    /// </summary>
    private string SuggestAddingPrecondition(string format, Set<WarningContext> context)
    {
      Contract.Requires(format != null);

      Contract.Ensures(Contract.Result<string>() != null);

#if SHOULDFIXREGRESSION
      return context.Exists(x => x.Type == WarningContext.ContextType.PreconditionCanDischarge) 
        ? (format + " (Consider adding a precondition?)") : format;
#else
      var msg = this.GetMessageStringFromCodeFixesOrSufficientButNotNecessaryConditions();

      return string.IsNullOrEmpty(msg) ? format : format + ". " + msg;
#endif
    }
    #endregion

    #region Overridden
    /// <summary>
    /// Two proof obligations are equal if they are of the same dynamic type and talk 
    /// about the same PC. If some analyses generate multiple similar proof obligations about the same PC
    /// they need to override Equals.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object obj)
    {
      if (this.GetType() != obj.GetType()) return false;

      var that = (ProofObligationBase<Expression, Variable>)obj;

      if (this.PC.Equals(that.PC))
      {
        return true;
      }
      return false;
    }

    public override int GetHashCode()
    {
      return this.PC.GetHashCode();
    }

    public override string ToString()
    {
      var condition = this.Condition;
      return condition != null? condition.ToString() : "???";
    }
    #endregion

    #region Privates

    /// <summary>
    /// A simple abstraction to count the occurrences of each ContextType weighted by the associate info
    /// </summary>
    protected static Set<WarningContext> Summarize(IEnumerable<WarningContext> collected)
    {
      Contract.Ensures(Contract.Result<Set<WarningContext>>() != null);

      var counts = new Dictionary<WarningContext.ContextType, int>();
      foreach (var c in collected)
      {
        if (!counts.ContainsKey(c.Type))
        {
          counts.Add(c.Type, 0);
        }
        if (c.AssociatedInfo <= 1)
        {
          counts[c.Type]++;
        }
        else
        {
          counts[c.Type] = c.AssociatedInfo;
        }
      }

      var result = new Set<WarningContext>(counts.Count);
      foreach (var c in counts)
      {
        result.Add(new WarningContext(c.Key, c.Value));
      }

      return result;
    }
    #endregion

    #region Better warning messages
    protected string GetMessageStringFromCodeFixesOrSufficientButNotNecessaryConditions()
    {
      var result = new StringBuilder();
      var msgSeen = new Set<string>();
      foreach (var fix in this.codeFixes)
      {
        var msg = fix.GetMessageForSourceObligation();
        if (!string.IsNullOrEmpty(msg) && !msgSeen.Contains(msg))
        {
          string dot = Char.IsPunctuation(msg[msg.Length - 1]) ? null : ".";
          if (msgSeen.Count > 0)
          {
            result.AppendFormat("Or, {0}{1} ", msg, dot);
          }
          else
          {
            result.AppendFormat("{0}{1} ", msg, dot);
          }

          msgSeen.Add(msg); // Avoid repeating twice the same message. Even if code fixes are different, they may generate the same English text
        }
      }

      // No message from code fixes, see if we have a sufficient yet not necessary condition
      if(msgSeen.Count == 0 && this.SufficientPreconditions != null)
      {
        foreach(var sufficientPrecondition in this.SufficientPreconditions)
        {
          if(sufficientPrecondition.Expr.CanPrintAsSourceCode())
          {
            string msg;
            switch(sufficientPrecondition.Kind)
            {
              case ConditionKind.CalleeAssume:
              case ConditionKind.EntryAssume:
                {
                  msg = string.Format("an explicit assumption at entry to document it: Contract.Assume({0});", sufficientPrecondition.Expr);

                  break;
                }
              case ConditionKind.ObjectInvariant:
                {
                  msg = string.Format("an object invariant or an assumption at entry to document it: Contract.Invariant({0});", sufficientPrecondition.Expr);

                  break;
                }
              case ConditionKind.Requires:
                {
                  msg = string.Format("a precondition to document it: Contract.Requires({0});", sufficientPrecondition.Expr);

                  break;
                }

              default:
                continue;
            }

            result.AppendFormat("The static checker determined that the condition '{0}' should hold on entry. Nevertheless, the condition may be too strong for the callers. If you think it is ok, add {1}", sufficientPrecondition.Expr, msg);
          }
        }
      }

      return result.ToString();    
    }
    
    #endregion
  }

  #region Contracts

  [ContractClassFor(typeof(ProofObligationBase<,>))]
  abstract class ProofObligationBaseContracts<Exp, Var> : ProofObligationBase<Exp, Var>
  {
    public override Witness GetWitness(ProofOutcome outcome)
    {
      Contract.Ensures(Contract.Result<Witness>() != null);

      return null;
    }

    #region To please the compiler

    ProofObligationBaseContracts() : base(default(APC), null, null) { }

    public override BoxedExpression Condition
    {
      get
      {
        // it may return null
        return null;
      }
    }

    protected override ProofOutcome ValidateInternal(IFactQuery<Exp, Var> query, ContractInferenceManager inferenceManager, IOutputResults output)
    {
      Contract.Requires(query != null);
      Contract.Requires(inferenceManager != null);

      throw new NotImplementedException();
    }

    public override void EmitOutcome(ProofOutcome outcome, IOutputResults output)
    {
      throw new NotImplementedException();
    }

    protected override void PopulateWarningContextInternal(ProofOutcome outcome)
    {
      throw new NotImplementedException();
    }
    #endregion
  }

  #endregion

  public class AssertionObligation<Variable>
    : ProofObligationBase<BoxedExpression, Variable>
  {
    #region State

    public readonly bool IsAssume;
    public readonly bool IsConditionCheck;

    public bool IsAssumeOrConditionCheck { get { return this.IsAssume || this.IsConditionCheck; } }
    
    public readonly string Tag;
    private readonly bool ShowInferenceTrace;

    private readonly BoxedExpression condition;
    private readonly bool? MethodMayContainUnsafeCode, MayBeAStringNullComparisonInASwitchStatement;

    #endregion

    #region Constructor

    public AssertionObligation(APC pc, string definingMethod, string tag, BoxedExpression cond, Provenance provenance, bool ShowInferenceTrace, bool isAssume, bool isConditionCheck, bool isFromThrowInstruction, bool? methodMayContainUnsafeCode, bool? mayBeAStringNullComparisonInASwitchStatement)
      : base(pc, definingMethod, provenance)
    {
      Contract.Requires(cond != null);
      Contract.Requires(tag != "Assume" || isAssume);
      Contract.Requires(!(isAssume || isConditionCheck) || (isAssume == !isConditionCheck));

      this.Tag = tag;
      this.condition = cond;
      this.IsAssume = isAssume;
      this.IsConditionCheck = isConditionCheck;
      this.ShowInferenceTrace = ShowInferenceTrace;
      this.IsFromThrowInstruction = isFromThrowInstruction;
      this.MethodMayContainUnsafeCode = methodMayContainUnsafeCode;
      this.MayBeAStringNullComparisonInASwitchStatement = mayBeAStringNullComparisonInASwitchStatement;
    }

    #endregion

    #region Overridden

    public override BoxedExpression Condition
    {
      get { return this.condition; }
    }

    public override string ObligationName
    {
      get { return this.Tag; }
    }

    protected override bool TryInferPrecondition
    {
      get
      {
        // The precondition is inferred externally for assertions
        // the main reason is the large generic types refactoring required to bring the IMethodDriver as a field in AssertionObligation
        // and then use it for the WP prover
        return false;
      }
    }

    protected override ProofOutcome ValidateInternal(IFactQuery<BoxedExpression, Variable> query, ContractInferenceManager inferenceManager, IOutputResults output)
    {
      return query.IsTrue(this.PC, this.condition);
    }

    public override void EmitOutcome(ProofOutcome outcome, IOutputResults output)
    {
      // For some reason the EmitOutcome for the other assertions is done externally
      if (this.IsConditionCheck)
      {
        this.PopulateWarningContextInternal(outcome);
        var witness = this.GetWitness(outcome);
       
        // new Witness(this.ID, WarningType.TestAlwaysEvaluatingToAConstant, ProofOutcome.Bottom, this.PC)
        output.EmitOutcome(witness, MessageString(), this.condition.ToString());
      }
    }

    private string MessageString()
    {
      var message = "warning: The Boolean condition {0} always evaluates to a constant value. If it (or its negation) appear in the source code, you may have some dead code or redundant check";

      if(this.MethodMayContainUnsafeCode.HasValue && (this.MethodMayContainUnsafeCode.Value == true))
      {
        message += ". It may also be a byproduct of the compilation of, e.g., fixed. In this case add a SuppressMessage to shut down the warning";
      }

      if (this.MayBeAStringNullComparisonInASwitchStatement.HasValue && this.MayBeAStringNullComparisonInASwitchStatement.Value)
      {
        message += ". It may also been introduced by the C# compiler for a switch statement over strings. In this case add a SuppressMessage to shut down the warning";
      }

      return message;
    }

    protected override void PopulateWarningContextInternal(ProofOutcome outcome)
    {
      if(this.MethodMayContainUnsafeCode.HasValue && (this.MethodMayContainUnsafeCode.Value == true))
      {
        this.AdditionalInformationOnTheWarning.Add(new WarningContext(WarningContext.ContextType.InsideUnsafeMethod));
      }

      if(this.MayBeAStringNullComparisonInASwitchStatement.HasValue && this.MayBeAStringNullComparisonInASwitchStatement.Value)
      {
        this.AdditionalInformationOnTheWarning.Add(new WarningContext(WarningContext.ContextType.StringComparedAgainstNullInSwitchStatement));
      }
    }

    public override Witness GetWitness(ProofOutcome outcome)
    {
      var warningType = this.IsConditionCheck? WarningType.TestAlwaysEvaluatingToAConstant: TagToWarningType(this.Tag);

      var witness = new Witness(this.ID, warningType, outcome, this.PC, this.AdditionalInformationOnTheWarning);

      if (this.ShowInferenceTrace)
      {
        WalkProvenance(this, 0, witness);
      }

      return witness;
    }

    public override string ToString()
    {
      return this.condition != null ? this.condition.ToString() : "null";
    }
    #endregion

    #region Publics

    /// <summary>
    /// Returns a pointer to the current warning context 
    /// </summary>
    public Set<WarningContext> GetWarningContext
    {
      get
      {
        return this.AdditionalInformationOnTheWarning;
      }
    }

    #endregion

    #region Privates

    static WarningType TagToWarningType(string tag)
    {
      switch (tag)
      {
        case "requires": return WarningType.ContractRequires;
        case "ensures": return WarningType.ContractEnsures;
        case "invariant": return WarningType.ContractInvariant;
        case "assert":
        default:
          return WarningType.ContractAssert;
      }
    }
    
    #endregion

    #region GetMessageString
    internal string GetMessageString(ProofOutcome outcome, string p)
    {
      // var msg = ProofObligationBase<BoxedExpression, Variable>.AddHintsForTheUser(outcome, p, this.GetWarningContext);

      // Add some information from the provenance, i.e., a sequence of calls
      var msg = ImproveMessageForInferredConditions(p, outcome);

      // Get information from the entry state
      var msgFromCodeFixes = GetMessageStringFromCodeFixesOrSufficientButNotNecessaryConditions();

      // Get information from the output message
      var msgViaOutParam = GetMessageFromWarningContexts();

      // Now let's build the string
      char c;
      var sep = (msg != null && ((c = msg[msg.Length - 1]) != '}' && Char.IsPunctuation(c) )) ? " " : ". ";

      var tempString1 = !string.IsNullOrEmpty(msgFromCodeFixes) 
        ? msg + sep + msgFromCodeFixes 
        : msg;

      return msgViaOutParam != null
        ? string.Format("{0}{1}{2}", tempString1, sep, msgViaOutParam)
        : tempString1;
    }

    private string GetMessageFromWarningContexts()
    {
      var outParams = this.GetWarningContext.Where(wc => wc.Type == WarningContext.ContextType.ViaOutParameter);
      var howMany = outParams.Count();
      if (howMany > 0)
      {
        if(howMany == 1)
        {
          var nameForVariable = outParams.First().Name;
          if(nameForVariable != null)
          {
            return string.Format("The variable '{0}' flows from an out parameter. Consider adding a postconditon to the callee or an assumption after the call to document it", nameForVariable);
          }
          else
          {
            return "A variable flows from an out parameter. Consider adding a postconditon or an assumption";
          }
        }
        return "One or more variables flow from an out parameter. Consider adding a postconditon or an assumption";
      }

      return null;
    }

    private string ImproveMessageForInferredConditions(string msg, ProofOutcome outcome)
    {
      if (this.Provenance != null && this.Provenance.Any())
      {
        switch (this.ObligationName)
        {
          case "requires":
          case "ensures":
            if(outcome == ProofOutcome.Top || outcome == ProofOutcome.False)
            {
              var str = new StringBuilder();
              var obligation = string.Empty;
              var chainLength = 0;

              str.AppendFormat("{0}", this.definingMethod);
              for (var p = this.Provenance.FirstOrDefault(); p != null; p = p.Provenance != null ? p.Provenance.FirstOrDefault() : null)
              {
                str.AppendFormat(" -> ");
                if (!string.IsNullOrEmpty(p.definingMethod))
                {
                  chainLength++;
                  str.AppendFormat("{0}", p.definingMethod);
                }

                if (p.Provenance == null && p.Condition != null && p.Condition.CanPrintAsSourceCode())
                {
                  obligation = p.Condition.ToString();
                }
              }
              if (chainLength > 0)
              {
                  msg += string.Format(". This sequence of invocations will bring to an error {0}{1}",
                    str.ToString(),
                    string.IsNullOrEmpty(obligation) ? null : (", condition " + obligation)
                    );
              }
            }
            break;

          case "invariant":
            if(outcome == ProofOutcome.False)
            {
              var p = this.Provenance.FirstOrDefault();
              if (p != null) // should always be true...
              {
                  string what;
                  if (!string.IsNullOrEmpty(p.definingMethod))
                  {
                      what = string.Format("the method {0} (in the same type)", p.definingMethod);
                  }
                  else
                  {
                      what = "a method in the same type";
                  }

                  var extraMsg = string.Format(". This object invariant was inferred, and it should hold in order to avoid an error if {0} is invoked", what);
                  msg += extraMsg;
              }
            }
            break;
        }
      }

      return msg;
    }

    static private void WalkProvenance(ProofObligation thisObl, int depth, Witness witness)
    {
      var obligationList = thisObl.Provenance;
      if (obligationList != null && obligationList.Any())
      {
        foreach (var obl in obligationList)
        {
          Contract.Assume(obl != null);
          if (obl.Condition != null)
          {
            var depthString = new String('-', depth + 1);

            witness.AddTrace(obl.PC, string.Format("{0} Cause {1} obligation: {2}", depthString, obl.ObligationName, obl.Condition.ToString()));

            WalkProvenance(obl, depth + 1, witness);
          }

        }
      }
    }
    #endregion

    #region Add warning contexts
    public void AddWarningContextsFromCodeFixes()
    {
      var wcsFromCodeFixes = new Set<WarningContext>();
      base.AddWarningContextFromCodeFixesAndInference(wcsFromCodeFixes);
      this.AdditionalInformationOnTheWarning.AddRange(Summarize(wcsFromCodeFixes));
    }
    
    public void AddWarningContextsFromMessage()
    {
      var conditionString = this.PC.ExtractAssertionCondition();
      if (conditionString != null && conditionString.Contains("Contract.ValueAtReturn"))
      {
        this.AdditionalInformationOnTheWarning.Add(new WarningContext(WarningContext.ContextType.ConditionContainsValueAtReturn));
      }
    }

    #endregion
  }

  public class AssertionForAllObligation<Variable>
    : AssertionObligation<Variable>
  {
    public readonly BoxedExpression ForAllCondition;

    public AssertionForAllObligation(APC pc, string definingMethod, string tag, BoxedExpression cond, Provenance provenance, bool ShowInferenceTrace, BoxedExpression forAllCondition, bool isAssume, bool isConditionCheck)
      : base(pc, definingMethod, tag, cond, provenance, ShowInferenceTrace, isAssume, isConditionCheck, false, null, null)
    {
      Contract.Requires(tag != null);
      Contract.Requires(cond != null);
      Contract.Requires(forAllCondition != null);

      this.ForAllCondition = forAllCondition;
    }

    #region Overridden

    public override BoxedExpression ConditionForPreconditionInference
    {
      get
      {
        Contract.Assert(this.ForAllCondition != null);
        return this.ForAllCondition;
      }
    }

    public override string ToString()
    {
      return string.Format("{0} ({1})", this.ForAllCondition.ToString(), base.ToString());
    }
    
    #endregion
  }

  /// <summary>
  /// This crawler servers as the base for analysis specific gatherers of proof obligations.
  ///
  /// Subclasses should override the MSIL instructions at which they want to generate a proof obligation.
  /// NOTE: Asserts should not be handled by specific analyses, as they are done in an analysis independent way.
  /// The obligation list, expression context and metadata decoder are available as fields.
  /// </summary>
  internal abstract class ProofObligations<Local, Parameter, Method, Field, Property, Event, Type, Variable, Attribute, Assembly, HighLevelExpression, ProofObligation>
    : ValueCodeVisitor<Local, Parameter, Method, Field, Property, Event, Type, Variable, Attribute, Assembly>
    , IProofObligations<Variable,HighLevelExpression>
    where ProofObligation : ProofObligationBase<HighLevelExpression,Variable>
    where Type : IEquatable<Type>
  {

    #region State
    private AnalysisStatistics stats;
    protected readonly List<ProofObligation> obligations = new List<ProofObligation>();
    /// <summary>
    /// To inform DFA where to cache
    /// </summary>
    readonly Set<APC> labels = new Set<APC>();
    #endregion

    #region Public access

    public abstract string Name { get; }

    public int Count { get { return this.obligations.Count; } }

    public void Add(ProofObligation obl)
    {
      this.obligations.Add(obl);
      this.labels.Add(obl.PC);
    }

    protected bool IgnoreProofObligationAtPC(APC pc)
    {
#if EXPERIMENTAL
      if (pc.InsideContract)
      {
        return true;
      }
#endif

      return pc.InsideNecessaryAssumption || pc.InsideContractAtCall || pc.InsideInvariantInMethod || pc.InsideOldManifestation;
    }

    public AnalysisStatistics Statistics { get { return this.stats; } }

    public double Validate(IOutputResults output, ContractInferenceManager inferenceManager, IFactQuery<HighLevelExpression, Variable> query)
    {
      var options = output.LogOptions;
      var total_obligations = 0;
      var validated = 0;

      var outcomes = new List<Pair<ProofObligation, ProofOutcome>>();

      foreach (var obl in obligations) 
      {
        if (obl.IsAlreadyValidated)
        {
          continue;
        }

        var outcome = obl.Validate(query, inferenceManager, output);

        // Discard unreached implicit obligations in contracts
        if (outcome == ProofOutcome.Bottom && obl.PC.InsideContract)
        {
          continue;
        }

        total_obligations++;

        if (outcome == ProofOutcome.True)
        {
          validated++;
        }

        this.stats.Add(outcome);

        outcomes.Add(new Pair<ProofObligation, ProofOutcome>(obl, outcome)); 
        
      }

      foreach (var pair in outcomes)
      {
        if (options.PrintOutcome(pair.Two))
        {
          pair.One.EmitOutcome(pair.Two, output);
        }
      }

      return total_obligations != 0 ? ((double)validated) / total_obligations : 1.0;
    }

    public void Print()
    {
      foreach (var p in this.obligations)
      {
        Console.WriteLine(p.ToString());
      }
    }
    #endregion

    #region For DFA caching
    public bool PCWithProofObligation(APC pc)
    {
      if (this.labels.Contains(pc)) 
      {
        return true;
      }
      return false;
    }
    #endregion

    #region IProofObligations<Variable,HighLevelExpression> Members


    public bool PCWithProofObligations(APC pc, List<ProofObligationBase<HighLevelExpression, Variable>> conditions)
    {
      var added = 0;
      foreach (var obl in this.obligations)
      {
        if (obl.PC.Equals(pc))
        {
          conditions.Add(obl);
          added++;
        }
      }

      return added != 0;
    }

    #endregion
  }

  internal class ProofObligationComposition<Variable, HighLevelExpression> : IProofObligations<Variable, HighLevelExpression>
  {
    List<IProofObligations<Variable, HighLevelExpression>> underlying = new List<IProofObligations<Variable, HighLevelExpression>>();

    public void Add(IProofObligations<Variable, HighLevelExpression> obl)
    {
      this.underlying.Add(obl);
    }

    #region IProofObligations<Variable,HighLevelExpression> Members

    string name;

    public string Name
    {
      get {
        if (name == null)
        {
          var sb = new StringBuilder();
          sb.Append("Combined ");
          foreach (var obl in this.underlying)
          {
            sb.Append(obl.Name);
            sb.Append(' ');
          }
          name = sb.ToString();
        }
        return name;
      }
    }

    public double Validate(IOutputResults output, ContractInferenceManager inferenceManager, IFactQuery<HighLevelExpression, Variable> query)
    {
      var result = 0.0; // is this used? 
      foreach (var obl in this.underlying)
      {
        obl.Validate(output, inferenceManager, query);
      }
      return result;
    }

    public bool PCWithProofObligation(APC pc)
    {
      foreach (var obl in this.underlying)
      {
        if (obl.PCWithProofObligation(pc)) return true;
      }
      return false;
    }

    public bool PCWithProofObligations(APC pc, List<ProofObligationBase<HighLevelExpression, Variable>> conditions)
    {
      var added = 0;
      foreach (var obl in this.underlying)
      {
        if (obl.PCWithProofObligations(pc, conditions))
        {
          added++;
        }
      }

      return added != 0;
    }

    public AnalysisStatistics Statistics
    {
      get {
        var result = new AnalysisStatistics();
        foreach (var obl in this.underlying)
        {
          result.Add(obl.Statistics);
        }
        return result;
      }
    }
    #endregion

  }

}

