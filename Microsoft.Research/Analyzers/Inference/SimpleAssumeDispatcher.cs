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
using Microsoft.Research.CodeAnalysis.Inference;

namespace Microsoft.Research.CodeAnalysis
{
  [ContractVerification(true)]
  public class SimpleAssumeDispatcher<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions>
    : SimpleDispatcherBase<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions>
    , IAssumeDispatcher
    where LogOptions : IFrameworkLogOptions
    where Variable : IEquatable<Variable>
    where Expression : IEquatable<Expression>
    where Type : IEquatable<Type>
  {
    #region Invariant

    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(this.entryAssumptions != null);
      Contract.Invariant(this.calleeAssumptions != null);
      Contract.Invariant(this.calleeAssumptionsPCs != null);
      Contract.Invariant(this.calleeAssumptions.Count == this.calleeAssumptionsPCs.Count);
    }

    #endregion

    #region State

    protected override string ContractTemplate { get { return "Contract.Assume({0});"; } }

    readonly private InferenceDB entryAssumptions;
    readonly private Dictionary<Method, InferenceDB> calleeAssumptions;
    readonly private Dictionary<Method, List<APC>> calleeAssumptionsPCs;
    readonly private bool AggressiveInferece;
    private IEnumerable<InferredCalleeNecessaryConditionCandidate<Method>> necessaryPostconditionCandidates;

    #endregion

    #region Constructor

    public SimpleAssumeDispatcher(
      IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions> mdriver,
      IOutputResults output, bool allowDisjunctivePreconditions, bool aggressiveInference)
      : base(mdriver, output, allowDisjunctivePreconditions)
    {
      Contract.Requires(mdriver != null);
      Contract.Requires(output != null);

      this.entryAssumptions = new InferenceDB(exp => exp.Simplify(mdriver.MetaDataDecoder), _ => true);
      this.calleeAssumptions = new Dictionary<Method, InferenceDB>();
      this.calleeAssumptionsPCs = new Dictionary<Method, List<APC>>();
      this.AggressiveInferece = aggressiveInference;
    }

    #endregion

    #region Implementation

    public void AddEntryAssumes(ProofObligation obl, IEnumerable<BoxedExpression> assumes)
    {
      base.GenericAddPreconditions(obl, assumes, ProofOutcome.Top, this.entryAssumptions, this.True);
    }

    [ContractVerification(false)]
    public IEnumerable<WarningContext> AddCalleeAssumes(ProofObligation obl, IEnumerable<IInferredCondition> calleeAssumes)
    {
      var md = this.mdriver;
      var result = new List<WarningContext>();

      foreach (var calleeAssume in calleeAssumes.OfType<InferredCalleeCondition<Method>>())
      {
        InferenceDB db;
        if (!this.calleeAssumptions.TryGetValue(calleeAssume.Callee, out db))
        {
          var calleeWarningContext = GetContextForCallee(calleeAssume.Callee);
          db = new InferenceDB(exp => exp.Simplify(md.MetaDataDecoder), _=> true, calleeWarningContext);
          this.calleeAssumptions.Add(calleeAssume.Callee, db);
          this.calleeAssumptionsPCs.Add(calleeAssume.Callee, new List<APC>()); // add the entry to the map calleAssumptions --> PC

          // Get the witnesses for the scoring
          result.Add(new WarningContext(WarningContext.ContextType.InferredCalleeAssume, (int)calleeWarningContext));

          if (calleeAssume.Expr.IsBinary && calleeAssume.Expr.BinaryOp == BinaryOperator.LogicalOr)
          {
            result.Add(new WarningContext(WarningContext.ContextType.InferredCalleeAssumeContainsDisjunction, /*unused?*/ 1));
          }

          if (md.AnalysisDriver.MethodCache.GetWitnessForMayReturnNull(calleeAssume.Callee))
          {
            result.Add(new WarningContext(WarningContext.ContextType.ViaMethodCallThatMayReturnNull, /*unused?*/ 1));
          }
        }
        base.GenericAddPreconditions(obl, new[] { calleeAssume.Expr }, ProofOutcome.Top, db, this.True);
        this.calleeAssumptionsPCs[calleeAssume.Callee].Add(calleeAssume.CalleePC);
      }

      this.necessaryPostconditionCandidates = calleeAssumes.OfType<InferredCalleeNecessaryConditionCandidate<Method>>();

      return result.Distinct();
    }

    public IEnumerable<KeyValuePair<BoxedExpression, IEnumerable<MinimalProofObligation>>> GenerateAssumptions()
    {
      Contract.Ensures(Contract.Result<IEnumerable<KeyValuePair<BoxedExpression, IEnumerable<MinimalProofObligation>>>>() != null);

      return this.entryAssumptions.GenerateConditions();
    }

    public int SuggestEntryAssumes()
    {
      var pc = this.mdriver.CFG.GetPCForMethodEntry();

      var i = 0;
      foreach (var passume in this.GenerateAssumptions())
      {
        var assume = passume.Key;
        Contract.Assume(assume != null);
        output.Suggestion(ClousotSuggestion.Kind.AssumeOnEntry, ClousotSuggestion.Kind.AssumeOnEntry.Message(),
          pc, MakeconditionString(assume.ToString()), entryAssumptions.CausesFor(assume), ClousotSuggestion.ExtraSuggestionInfo.None);
        i++;
      }
      return i;
    }

    public int PropagateAssumes()
    {
      var count = 0;
      foreach (var passume in this.GenerateAssumptions())
      {
        var assume = passume.Key;
        var provenance = passume.Value;
        count++;
        this.mdriver.AddEntryAssume(assume, provenance);
      }

      var dummyPC = this.mdriver.Context.MethodContext.CFG.EntryAfterRequires;
      foreach (var calleePair in this.calleeAssumptions)
      {
        var method = calleePair.Key;
        var db = calleePair.Value;
        foreach (var pair in db.GenerateConditions())
        {
          var assume = pair.Key;
          var provenance = pair.Value;
          this.mdriver.AddCalleeAssume(assume, method, dummyPC, provenance);
        }
      }
      return count;
    }

    [ContractVerification(false)]
    public int SuggestCalleeAssumes(bool suggestCalleeAssumes, bool suggestNecessaryPostconditions, bool includeDisjunctions)
    {
      var entryPC = this.mdriver.Context.MethodContext.CFG.Entry;

      var count = 0;
      var emptyList = new List<uint>();
      var md = this.mdriver.MetaDataDecoder;

      foreach (var pair in this.calleeAssumptions)
      {
        var method = pair.Key;
        var db = pair.Value;
        var PCs = this.calleeAssumptionsPCs[method];
        Contract.Assume(PCs != null);
        Contract.Assume(PCs.Count > 0);

        var pc = PCs.First();

        foreach (var condition in db.GenerateConditions())
        {
          var assume = condition.Key;

          Contract.Assume(assume != null);

          if(!includeDisjunctions && ContainsDisjunctions(assume))
          {
            continue;
          }

          var obl = condition.Value.FirstOrDefault();
          var sourceCondition = "";
          var oblCondition = obl != null && MakesSense(obl.PC, obl.Condition, ref sourceCondition) ? " (obligation " + sourceCondition + ")" : null;

          if (suggestCalleeAssumes)
          {
            var message = string.Format("This condition involving the return value of {0}{1} should hold to avoid an error later{2}: {3}", md.Name(method), PCs.Count > 1 ? " (in this or in another invocation)" : null, oblCondition /* can be null */, assume);
            this.output.Suggestion(ClousotSuggestion.Kind.AssumeOnCallee, ClousotSuggestion.Kind.AssumeOnCallee.Message(), pc, message, emptyList, ClousotSuggestion.ExtraSuggestionInfo.None);
          }

          if(suggestNecessaryPostconditions)
          {
            ClousotSuggestion.Kind kind;
            ClousotSuggestion.ExtraSuggestionInfo extraInfo;
            string result;
            if(TrySuggestNecessaryPostconditions(method, assume, out kind, out result, out extraInfo))
            {
              this.output.Suggestion(kind, kind.Message(md.Name(method)), pc, result, null, extraInfo);
            }
          }
          count++;
        }
      }

      if(suggestNecessaryPostconditions && this.necessaryPostconditionCandidates != null)
      {
        ClousotSuggestion.ExtraSuggestionInfo extraInfo;
        ClousotSuggestion.Kind kind;
        string result;
        foreach (var candidate in this.necessaryPostconditionCandidates)
        {
          if (TrySuggestNecessaryPostconditions(candidate.Callee, candidate.Expr, out kind, out result, out extraInfo))
          {
            this.output.Suggestion(kind, kind.Message(md.Name(candidate.Callee)), candidate.CalleePC, result, null, extraInfo);
          }
        }
      }

      return count;
    }

    private bool ContainsDisjunctions(BoxedExpression assume)
    {
      BinaryOperator bop;
      BoxedExpression left, right;

      return assume.IsBinaryExpression(out bop, out left, out right) && bop == BinaryOperator.LogicalOr;
    }

    #endregion

    #region Private

    private WarningContext.CalleeInfo GetContextForCallee(Method m)
    {
      var result = WarningContext.CalleeInfo.Nothing;
      var mdDecoder = this.mdriver.MetaDataDecoder;

      IIndexable<Type> dummyTypes;

      if (mdDecoder.IsAbstract(m))
      {
        result |= WarningContext.CalleeInfo.IsAbstract;
      }
      if (mdDecoder.IsAutoPropertyMember(m))
      {
        result |= WarningContext.CalleeInfo.IsAutoProperty;
      }
      if (mdDecoder.IsCompilerGenerated(m))
      {
        result |= WarningContext.CalleeInfo.IsCompilerGenerated;
      }
      if (mdDecoder.IsConstructor(m))
      {
        result |= WarningContext.CalleeInfo.IsConstructor;
      }
      if (mdDecoder.IsDispose(m))
      {
        result |= WarningContext.CalleeInfo.IsDispose;
      }
      if (mdDecoder.IsExtern(m))
      {
        result |= WarningContext.CalleeInfo.IsExtern;
      }
      if (mdDecoder.IsFinalizer(m))
      {
        result |= WarningContext.CalleeInfo.IsFinalizer;
      }
      if (mdDecoder.IsGeneric(m, out dummyTypes))
      {
        result |= WarningContext.CalleeInfo.IsGeneric;
      }
      if (mdDecoder.IsImplicitImplementation(m))
      {
        result |= WarningContext.CalleeInfo.IsImplicitImplementation;
      }
      if (mdDecoder.IsInternal(m))
      {
        result |= WarningContext.CalleeInfo.IsInternal;
      }
      if (mdDecoder.IsNewSlot(m))
      {
        result |= WarningContext.CalleeInfo.IsNewSlot;
      }
      if (mdDecoder.IsOverride(m))
      {
        result |= WarningContext.CalleeInfo.IsOverride;
      }
      if (mdDecoder.IsPrivate(m))
      {
        result |= WarningContext.CalleeInfo.IsPrivate;
      }
      if (mdDecoder.IsPropertyGetterOrSetter(m))
      {
        result |= WarningContext.CalleeInfo.IsPropertyGetterOrSetter;
      }
      if (mdDecoder.IsProtected(m))
      {
        result |= WarningContext.CalleeInfo.IsProtected;
      }
      if (mdDecoder.IsPublic(m))
      {
        result |= WarningContext.CalleeInfo.IsPublic;
      }
      if (mdDecoder.IsSealed(m))
      {
        result |= WarningContext.CalleeInfo.IsSealed;
      }
      if (mdDecoder.IsStatic(m))
      {
        result |= WarningContext.CalleeInfo.IsStatic;
      }
      if (mdDecoder.IsVirtual(m))
      {
        result |= WarningContext.CalleeInfo.IsVirtual;
      }
      if (mdDecoder.IsVoidMethod(m))
      {
        result |= WarningContext.CalleeInfo.IsVoidMethod;
      }
      else
      {
        var returnType = mdDecoder.ReturnType(m);
        if (mdDecoder.IsPrimitive(returnType))
        {
          result |= WarningContext.CalleeInfo.ReturnPrimitiveValue;
        }
        if (mdDecoder.IsReferenceType(returnType))
        {
          result |= WarningContext.CalleeInfo.ReturnReferenceType;
        }
        else
        {
          result |= WarningContext.CalleeInfo.ReturnStructValue;
        }
      }

      var currentMethod = mdriver.CurrentMethod;
      if(object.Equals(mdDecoder.DeclaringType(m), mdDecoder.DeclaringType(currentMethod)))
      {
        result |= WarningContext.CalleeInfo.DeclaredInTheSameType;
      }

      var declaringAssembly = mdDecoder.DeclaringAssembly(m);
      if (!mdDecoder.DeclaringAssembly(currentMethod).Equals(declaringAssembly))
      {

        if (mdDecoder.IsNetFrameworkAssembly(declaringAssembly))
        {
          result |= WarningContext.CalleeInfo.DeclaredInAFrameworkAssembly;
        }
        else
        {
          result |= WarningContext.CalleeInfo.DeclaredInADifferentAssembly;
        }
      }

      return result;
    }

    [Pure]
    private bool MakesSense(APC pc, BoxedExpression exp, ref string sourceExpression)
    {      
      if (exp == null)
        return false;

      var renamed = exp.ReadAt(pc, this.mdriver.Context, true);
      if (renamed != null)
      {
        sourceExpression = renamed.ToString();
        return true;
      }

      return false;
    }

    private bool TrySuggestNecessaryPostconditions(Method method, BoxedExpression exp, out ClousotSuggestion.Kind kind, out string result, out ClousotSuggestion.ExtraSuggestionInfo extraInfo)
    {
      result = null;
      kind = default(ClousotSuggestion.Kind);
      extraInfo = new ClousotSuggestion.ExtraSuggestionInfo();

      var md = this.mdriver.MetaDataDecoder;

      if (ShouldInferNecessaryPostconditionFor(method) && IsSuitableNecessaryPostcondition(exp))
      {
        #region Interface methods
        // Is it an interface method?
        if (md.IsInterface(md.DeclaringType(method)))
        {
          kind = ClousotSuggestion.Kind.EnsuresNecessary;
          this.SetExtraInfo(method, StringConstants.XML.Interface, exp, extraInfo);
          result = string.Format("The caller expects the postcondition {0} to hold for the interface member {1}. Consider adding such postcondition to enforce all implementations to guarantee it", extraInfo.SuggestedCode, md.Name(method));

          return true;
        }
        // Is it an implementation of an interface method?
        if (md.IsImplicitImplementation(method))
        {
          var implemented = md.ImplementedMethods(method).ToArray();
          Contract.Assume(implemented.Length > 0);
          kind = ClousotSuggestion.Kind.EnsuresNecessary;
          this.SetExtraInfo(implemented[0], StringConstants.XML.Interface, exp, extraInfo); // we just pick the first one
          if (implemented.Length == 1)
          {
            result = string.Format("The caller expects the postcondition {0} to hold for the interface member {1}. Consider adding the postcondition to enforce all implementations to guarantee it", extraInfo.SuggestedCode, md.Name(method));
          }
          else
          {
            extraInfo.CalleeDocumentId =  String.Join(" or ", implemented.Select(m => md.DocumentationId(m)).ToArray());
            result = string.Format("Add a postcondition {0} to one of the interface members {1}", extraInfo.SuggestedCode, md.Name(method));
          }

          return true;
        }
        #endregion

        #region Abstract methods

        if (md.IsAbstract(method))
        {
          kind = ClousotSuggestion.Kind.EnsuresNecessary;
          this.SetExtraInfo(method, StringConstants.XML.Abstract, exp, extraInfo);
          result = string.Format("The caller expects the postcondition {0} to hold for the abstract member {1}. Consider adding the postcondition to enforce all overrides to guarantee it", extraInfo.SuggestedCode, md.Name(method));
          
          return true;
        }
        #endregion

        #region External methods

        if (md.IsExtern(method))
        {
          kind = ClousotSuggestion.Kind.EnsuresNecessary;
          this.SetExtraInfo(method, StringConstants.XML.Extern, exp, extraInfo);
          result = string.Format("The caller expects the postcondition {0} to hold for the external member {1}. Consider adding a postcondition or an assume to document it", extraInfo.SuggestedCode, md.Name(method));
          
          return true;
        }
        #endregion

        #region Proper methods

        kind = ClousotSuggestion.Kind.EnsuresNecessary;
        this.SetExtraInfo(method, StringConstants.XML.ProperMember, exp, extraInfo);
        result = string.Format("The caller expects the postcondition {0} to hold for the member {1}. Consider adding such postcondition to make sure all implementations satisfy it", extraInfo.SuggestedCode, md.Name(method));

        #endregion
      }

      return false;
    }


    private void SetExtraInfo(Method m, string kind, BoxedExpression exp, ClousotSuggestion.ExtraSuggestionInfo extraInfo)
    {
      Contract.Requires(kind != null);

      var md = mdriver.MetaDataDecoder;
      extraInfo.CalleeDocumentId = md.DocumentationId(m);
      extraInfo.CalleeMemberKind = kind;
      extraInfo.TypeDocumentId = md.DocumentationId(md.DeclaringType(m));
      extraInfo.SuggestedCode = GetPostconditionString(exp);
      extraInfo.CalleeIsDeclaredInTheSameAssembly = md.DeclaringAssembly(m).Equals(md.DeclaringAssembly(mdriver.CurrentMethod)).ToString();
    }


    [Pure]
    private bool ShouldInferNecessaryPostconditionFor(Method method)
    {
      var mdd = this.mdriver.MetaDataDecoder;
      return this.AggressiveInferece || mdd.DeclaringAssembly(this.mdriver.CurrentMethod).Equals(mdd.DeclaringAssembly(method));
    }

    [Pure]
    static private bool IsSuitableNecessaryPostcondition(BoxedExpression exp)
    {
      BinaryOperator bop;
      BoxedExpression left, right;
      if (exp.IsBinaryExpression(out bop, out left, out right) )
      {
        var variablesInExp = exp.Variables();

        if (variablesInExp.Count() == 1 && variablesInExp.First().ToString().Contains(StringConstants.Contract.Result)) // Very bad way to check that we have: (i) unary predicates on (ii) the result value 
        {
          // Check for Contract.Result<..>() != null or Contract.Result<..>() == null
          if (bop.IsEqualityOrDisequality() && (left.IsNull != right.IsNull))
          {
            return true;
          }

          // Check for Contract.Result<..>() > k and similar
          if (bop.IsComparisonBinaryOperator() && (left.IsConstant != right.IsConstant))
          {
            return true;
          }
        }
      }

      return false;
    }

    [Pure]
    static private string GetPostconditionString(BoxedExpression exp)
    {
      return string.Format(StringConstants.Contract.EnsuresWithHole, exp);
    }

    [Pure]
    bool True(BoxedExpression exp, ProofOutcome originalOutcome, out ProofOutcome outcome)
    {
      outcome = originalOutcome;
      return true;
    }
    
    #endregion
  }
}
