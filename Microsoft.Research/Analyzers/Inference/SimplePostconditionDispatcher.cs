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
using System.Diagnostics.Contracts;
using Microsoft.Research.DataStructures;

namespace Microsoft.Research.CodeAnalysis
{
  using Provenance = IEnumerable<ProofObligation>;
  using Microsoft.Research.CodeAnalysis.Inference;
  using System.Diagnostics.CodeAnalysis;

  public class SimplePostconditionDispatcher
  {
    public static List<Tuple<Method, BoxedExpression.AssertExpression, Provenance>>
      GetCandidatePostconditionsForAutoProperties<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>
      (IEnumerable<BoxedExpression> boxedExpressions, IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdd,
      FieldsDB<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> fieldsDB)
    {
      var result = new List<Tuple<Method, BoxedExpression.AssertExpression, Provenance>>();
      if (boxedExpressions != null)
      {
        foreach (var post in boxedExpressions.Where(exp => { return exp.IsBinary; }))
        {
          Contract.Assume(post != null);

          BoxedExpression exp;
          if (post.IsCheckExpNotNotNull(out exp))
          {
            var path = exp.AccessPath;
            if (path != null)
            {
              Method m;
              if (path[0].ToString() == "this" && path[path.Length - 1].IsMethodCall && path[path.Length - 1].TryMethod(out m) && mdd.IsAutoPropertyMember(m))
              {
                Method setter;
                if (mdd.TryGetSetterFromGetter(m, out setter) && fieldsDB.IsCalledInANonConstructor(setter))
                {
#if DEBUG
                  Console.WriteLine("[INFERENCE] Skipping the inference of the postcondition '{0}' to an autoproperty because found it is set in a non-constructor", post);
#endif
                  continue;
                }

                var t = mdd.ReturnType(m);
                var neqNull = BoxedExpression.Binary(BinaryOperator.Cne_Un, BoxedExpression.Result(t), BoxedExpression.Const(null, t, mdd));
#if DEBUG
                Console.WriteLine("[INFERENCE] Propagating the postcondition '{0}' to the autoproperty", post);
#endif

                var p = new BoxedExpression.AssertExpression(neqNull, "ensures", APC.Dummy, null, null);

                result.Add(new Tuple<Method, BoxedExpression.AssertExpression, Provenance>(m, p, null));
              }
            }
          }
        }
      }
      return result;
    }
  }

  [ContractVerification(true)]
  public class SimplePostconditionDispatcher<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions>
    : IPostconditionDispatcher
    where LogOptions : IFrameworkLogOptions
    where Variable : IEquatable<Variable>
    where Expression : IEquatable<Expression>
    where Type : IEquatable<Type>
  {
    #region Statics

    private const string ContractPostconditionTemplate = "Contract.Ensures({0});";

    #endregion

    #region Object invariant

    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(this.output != null);
      Contract.Invariant(this.mDriver != null);
      Contract.Invariant(this.globalInfo != null);
      Contract.Invariant(this.inferredPostconditions != null);
    }

    #endregion

    #region Private state

    readonly private IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions> mDriver;
    readonly private SharedPostConditionManagerInfo<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> globalInfo;
    readonly private IOutputResults output;
    readonly private ClousotSuggestion.Ensures PostconditionSuggestionFilter;
    readonly private List<BoxedExpression> inferredPostconditions;
    readonly private bool warnAndFilterIfExternallyVisible;
    readonly private bool aggressiveInference;
    private List<BoxedExpression> filteredPostconditions;

    #endregion

    #region Constructor

    public SimplePostconditionDispatcher(
      SharedPostConditionManagerInfo<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> globalInfo,
      IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions> mDriver, IOutputResults output, ClousotSuggestion.Ensures suggestionFilter, bool warnAndFilterIfExternallyVisible, bool aggressiveInference)
    {
      Contract.Requires(globalInfo != null);
      Contract.Requires(output != null);
      Contract.Requires(mDriver != null);

      this.mDriver = mDriver;
      this.output = output;
      this.inferredPostconditions = new List<BoxedExpression>();
      this.filteredPostconditions = null;
      this.globalInfo = globalInfo;
      this.PostconditionSuggestionFilter = suggestionFilter;
      this.warnAndFilterIfExternallyVisible = warnAndFilterIfExternallyVisible;
      this.aggressiveInference = aggressiveInference;
    }

    #endregion

    #region Implementation

    public void AddPostconditions(IEnumerable<BoxedExpression> postconditions)
    {
      if (this.filteredPostconditions != null)
      {
        throw new InvalidOperationException("The object is sealed: cannot add postconditions anymore");
      }

      this.inferredPostconditions.AddRange(postconditions);
    }

    public void AddNonNullFields(object method, IEnumerable<object> fields)
    {
      var m = (Method)method;
      var f = fields;

      this.globalInfo.AddNonNullFields(m, f); 
    }

    public IEnumerable<object> GetNonNullFields(object method)
    {
      var m = (Method)method;
      IEnumerable<Tuple<Field, BoxedExpression>> result;
      if(this.globalInfo.TryNonNullFields(m, out result))
      {
        return result;
      }
      else
      {
        return Enumerable.Empty<object>();
      }
    }

    public List<BoxedExpression> GeneratePostconditions()
    {
      if (this.filteredPostconditions == null)
      {
        this.filteredPostconditions = new List<BoxedExpression>();
        var db = new Set<string>();
        foreach (var p in this.inferredPostconditions)
        {
          if (p != null)
          {
            var str = p.ToString();
            if (!db.Contains(str))
            {
              this.filteredPostconditions.Add(p);
              db.Add(str);
            }
          }
        }
      }
      return filteredPostconditions;
    }

    public int SuggestPostconditions()
    {
      var md = this.mDriver;
      var mdd = md.MetaDataDecoder;
      var pc = md.CFG.GetPCForMethodEntry();

      var count = 0;
      // No suggestions on struct constructors
      if(mdd.IsConstructor(md.CurrentMethod) && mdd.IsStruct(mdd.DeclaringType(md.CurrentMethod)))
      {
        return count;
      }

      foreach (var p in this.GeneratePostconditions().Where(p => p != null && !p.IsConstantFalse()))
      {
        Contract.Assume(p != null);

        bool containsResult;
        if (this.IsAdmissible(p, out containsResult))
        {
          if (this.warnAndFilterIfExternallyVisible)
          {
            if(mdd.IsVisibleOutsideAssembly(md.CurrentMethod) && (aggressiveInference || containsResult))
            {
              var suggestion = string.Format("Consider adding the postcondition {0} to provide extra-documentation to the library clients", MakePostconditionString(p.ToString()));
              this.output.Suggestion(ClousotSuggestion.Kind.Ensures, "contract for public surface member", pc, suggestion, null, ClousotSuggestion.ExtraSuggestionInfo.None);
            }
#if DEBUG
            else
            {
              this.output.WriteLine("[INFERENCE] Postcondition {0} has been filted because is not considered admissible (the method is not visible or it does not contain a result and the inference is not aggressive)- we are only showing it in debug mode", p.ToString());
            }
#endif
          }
          else 
          {
            this.output.Suggestion(ClousotSuggestion.Kind.Ensures, ClousotSuggestion.Kind.Ensures.Message(), pc, MakePostconditionString(p.ToString()), null, ClousotSuggestion.ExtraSuggestionInfo.None);
            count++;
          }
        }
#if DEBUG
        else
        {
          this.output.WriteLine("[INFERENCE] Postcondition {0} has been filted because is not considered admissible for printing - we are only showing it in debug mode", p.ToString());
        }
#endif
      }

      return count;
    }

    public int SuggestNonNullFieldsForConstructors()
    {
      var md = this.mDriver;
      var t = md.MetaDataDecoder.DeclaringType(md.CurrentMethod);
      if (this.globalInfo.CanSuggestInvariantFor(t))
      {
        return this.globalInfo.SuggestNonNullFieldsForConstructors(md.CFG.GetPCForMethodEntry(), t, this.output);
      }
      return 0;
    }

    public IEnumerable<BoxedExpression> SuggestNonNullObjectInvariantsFromConstructorsForward(bool doNotRecord)
    {
      var md = this.mDriver;
      var mdd = md.MetaDataDecoder;

      var type = mdd.DeclaringType(md.CurrentMethod);

      return this.globalInfo.SuggestNonNullObjectInvariantsFromConstructorsForward(type, doNotRecord);
    }

    public int PropagatePostconditions()
    {
      if (!this.mDriver.CanAddEnsures())
      {
        return 0;
      }

      var pc = this.mDriver.CFG.Entry;

      var count = 0;
      // TODO: We do not propagate quantified expressions
      foreach (var post in this.GeneratePostconditions().Where(exp => !exp.IsQuantified))
      {
        Contract.Assume(post != null);

        Provenance provenance = null; // TODO: encode where this contract comes from.

        if (CanPropagate(post))
        {
          this.mDriver.AddPostCondition(post, pc, provenance);
          count++;
        }
      }

#if DEBUG
      if(count != this.GeneratePostconditions().Count)
      {
        Console.WriteLine("[INFERENCE] We did not propagate all the generated postconditions. This is probably because either we had some Forall(...), and CC does not know how to handle inferred ForAll postconditions");
      }
#endif

      return count;
    }

    public int PropagatePostconditionsForProperties()
    {
      // Only for constructors
      if (!this.mDriver.MetaDataDecoder.IsConstructor(this.mDriver.CurrentMethod))
      {
        return 0;
      }


      this.mDriver.AddPostConditionsForAutoProperties(SimplePostconditionDispatcher.GetCandidatePostconditionsForAutoProperties(this.GeneratePostconditions(), this.mDriver.MetaDataDecoder, this.globalInfo.FieldDB));

      return 0;
    }
    
    [SuppressMessage("Microsoft.Contracts", "TestAlwaysEvaluatingToAConstant")]
    public bool MayReturnNull(IFact facts, TimeOutChecker timeout)
    {
      var moreRefinedFacts = facts as IFactQuery<BoxedExpression, Variable>;
      if (facts != null)
      {
        bool mayReturnNull;
        var search = new PostconditionWitnessSeaker<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions>(moreRefinedFacts, this.mDriver);

        return search.TryGetAWitness(this.mDriver.CFG.NormalExit, timeout, out mayReturnNull) && mayReturnNull;
      }

      return false;
    }

    public bool EmitWarningIfFalseIsInferred()
    {
      var md = this.mDriver;
      var cd = md.AnalysisDriver.ContractDecoder;
      var currMethod = md.CurrentMethod;
      if (this.GeneratePostconditions().Any(exp => exp.IsConstantFalse()))
      {

        if (md.AdditionalSyntacticInformation.AssertedPostconditions.Any(exp => exp.IsConstantFalse())
          ||
            (md.AdditionalSyntacticInformation.HasThrow && md.BasicFacts.IsUnreachable(md.CFG.NormalExit))
          )
        {
          // If the method has already an ensures "false", then we do nothing.
          // HACK: If the method normal exit is unreachable, and the method has a throw instruction, then we do nothing. 
          //       Of course, this does not imply that the throw instruction dominates the normal exit, we only use it as an heuristic

          return false;
        }
        else
        {
          var witness = new Witness(null /* no proof obligation causes it*/, WarningType.FalseEnsures, ProofOutcome.Top, md.CFG.GetPCForMethodEntry());
          var isConstructor = md.MetaDataDecoder.IsConstructor(currMethod);

          string outputMessage;

          if (cd.HasEnsures(currMethod) || cd.HasInvariant(currMethod))
          {
            outputMessage = String.Format("{0} ensures (and invariants) are in contradiction with the detected code behavior. If this is wanted, consider adding Contract.Ensures(false) to document that it never returns normally", isConstructor ? "Constructor" : "Method");
          }
          else
          {
            outputMessage = String.Format("The normal exit of the method is unreachable. If this is wanted, consider adding Contract.Ensures(false) to document it");
          }

          this.output.EmitOutcome(witness, outputMessage);
        }

        return true;
      }
      return false;
    }

    #endregion

    #region private

    [Pure]
    private bool CanPropagate(BoxedExpression be)
    {
      Contract.Requires(be != null);

      Contract.Assert(this.mDriver.MetaDataDecoder != null);

      var options = this.output.LogOptions;
      var md = this.mDriver;
      var currMethod = md.CurrentMethod;

      if (options.PropagateInferredEnsures(md.MetaDataDecoder.IsPropertyGetterOrSetter(currMethod)))
      {
        return true;
      }

      if (options.PropagateInferredNonNullReturn 
        && !this.mDriver.AdditionalSyntacticInformation.HasExceptionHandlers) // Avoid inferring a postcondition if we have an exception handler, has Clousot ignores those
      {
        BinaryOperator bop;
        BoxedExpression left, right;
        if (be.IsBinaryExpression(out bop, out left, out right) && (bop == BinaryOperator.Ceq || bop == BinaryOperator.Cne_Un)
          && (left.IsNull || right.IsNull))
        {
          // TODO check the other is the return value
          return true;
        }
      }

      if (options.PropagateInferredSymbolicReturn && md.AnalysisDriver.ContractDecoder.IsPure(currMethod))
      {
        BinaryOperator bop;
        BoxedExpression left, right;
        if (be.IsBinaryExpression(out bop, out left, out right) && bop == BinaryOperator.Ceq && (left.IsResult || right.IsResult))
        {
          return true;
        }
      }

      return false;
    }

    [Pure]
    private static string MakePostconditionString(string condition)
    {
      return string.Format(ContractPostconditionTemplate, condition);
    }

    [Pure]
    private bool IsAdmissible(BoxedExpression p, out bool containsResult)
    {
      Contract.Requires(p != null);

      containsResult = false;
      switch (this.PostconditionSuggestionFilter)
      {
        case ClousotSuggestion.Ensures.NonNull:
        case ClousotSuggestion.Ensures.NonNullReturnOnly: // Never used?????
          {
            BinaryOperator bop;
            BoxedExpression left, right;
            var isExpBopNull = p.IsBinaryExpression(out bop, out left, out right) && bop.IsEqualityOrDisequality() && (right.IsNull || left.IsNull);

            if(!isExpBopNull)
            {
              return false;
            }

            Contract.Assert(isExpBopNull);

            containsResult = left.IsResult || right.IsResult;

            if(this.PostconditionSuggestionFilter == ClousotSuggestion.Ensures.NonNullReturnOnly)
            {
              return left.IsResult;
            }

            return isExpBopNull; // which should be true
          }

        default:
          {
            containsResult = p.ContainsReturnValue();
            return true;
          }
      }
    }

    #endregion
  }

  public class SharedPostConditionManagerInfo<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>
    where Type : IEquatable<Type>
  {
    #region State

    readonly private IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> metadataDecoder;
    public FieldsDB<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> FieldDB { get; private set; }
    readonly private Dictionary<Method, IEnumerable<Tuple<Field, BoxedExpression>>> nnFieldsAtMethodExitPoint;
    readonly private Dictionary<Type, Set<Method>> analyzedConstructors;
    readonly private Set<Type> completeTypes;
    readonly private Set<Type> typesWeSuggestedNonNullFields;
    readonly private Set<Type> typesWeSuggestedObjectInvariants;

    #endregion

    public SharedPostConditionManagerInfo(IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> metadataDecoder, FieldsDB<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> fieldDB)
    {
      Contract.Requires(metadataDecoder != null);
      Contract.Requires(fieldDB != null);

      this.metadataDecoder = metadataDecoder;
      this.FieldDB = fieldDB;
      this.nnFieldsAtMethodExitPoint = new Dictionary<Method, IEnumerable<Tuple<Field, BoxedExpression>>>();
      this.analyzedConstructors = new Dictionary<Type, Set<Method>>();
      this.completeTypes = new Set<Type>();
      this.typesWeSuggestedNonNullFields = new Set<Type>();
      this.typesWeSuggestedObjectInvariants = new Set<Type>();
    }

    public void AddNonNullFields(Method method, IEnumerable<object> fields)
    {
      this.nnFieldsAtMethodExitPoint[method] = fields.OfType<Tuple<Field, BoxedExpression>>();
      RecordAnalysisOf(method);
    }

    public bool TryNonNullFields(Method m, out IEnumerable<Tuple<Field, BoxedExpression>> fields)
    {
      return this.nnFieldsAtMethodExitPoint.TryGetValue(m, out fields);
    }

    internal bool CanSuggestInvariantFor(Type t)
    {
      return this.completeTypes.Contains(t) && !this.typesWeSuggestedNonNullFields.Contains(t);
    }

    internal int SuggestNonNullFieldsForConstructors(APC pc, Type t, IOutputResults output)
    {
      Contract.Ensures(Contract.Result<int>() >= 0);

      var md = this.metadataDecoder;
      var count = 0;

      // No suggestions for structs
      if(md.IsStruct(t))
      {
        return count;
      }

      var result = GenerateObjectInvariantsForType(t);

      if (result.Any())
      {
        foreach (var f in result)
        {
          var contract = string.Format("Contract.Invariant({0} != null);", md.Name(f.Item1));
          var extraInfo = new ClousotSuggestion.ExtraSuggestionInfo() { SuggestedCode = contract, TypeDocumentId = md.DocumentationId(t) };
          var str = String.Format("Consider adding an object invariant {0} to the type {1}", contract, md.Name(t));
          output.Suggestion(ClousotSuggestion.Kind.ObjectInvariant, ClousotSuggestion.Kind.ObjectInvariant.ToString(), pc, str, null, extraInfo);
          count++;
        }
      }

      this.typesWeSuggestedNonNullFields.Add(t);
      return count;
    }

    /// <summary>
    /// ** INVARIANT ** We only suggest the invariants once for each typem unless doNotRecord is set
    /// </summary>
    public IEnumerable<BoxedExpression> SuggestNonNullObjectInvariantsFromConstructorsForward(Type type, bool doNotRecord)
    {
      Contract.Ensures(Contract.Result<IEnumerable<BoxedExpression>>() != null);

      var mdd = this.metadataDecoder;

      if (mdd.IsStruct(type))
      {
        return Enumerable.Empty<BoxedExpression>();
      }

      if(doNotRecord)
      {
        return this.GenerateObjectInvariantsForType(type).Select(pair => pair.Item2);
      }

      if (!this.typesWeSuggestedObjectInvariants.Contains(type))
      {
        return this.GenerateObjectInvariantsForType(type).Select(pair => pair.Item2);
      }
      else
      {
        return Enumerable.Empty<BoxedExpression>();
      }
    }

    #region Private

    private IEnumerable<Tuple<Field, BoxedExpression>> GenerateObjectInvariantsForType(Type t)
    {
      Contract.Requires(!this.typesWeSuggestedObjectInvariants.Contains(t));

      if(!HaveSeenAllConstructorsOf(t))
      {
        return Enumerable.Empty<Tuple<Field, BoxedExpression>>();
      }

      // We are going to suggest the object invariant
      this.typesWeSuggestedObjectInvariants.Add(t);

      var md = this.metadataDecoder;
      var isFirst = true;
      IEnumerable<Tuple<Field, BoxedExpression>> result = /* to please the C# compiler */ new Set<Tuple<Field, BoxedExpression>>();
      foreach (var fields in this.nnFieldsAtMethodExitPoint.Where(pair => md.IsConstructor(pair.Key) && md.DeclaringType(pair.Key).Equals(t)).Select(x => x.Value))
      {
        if (isFirst)
        {
          result = fields;
          isFirst = false;
        }
        else
        {
          // Just Intersect seems not to work. So we do the work in the ugly way
          var f = fields.Select(pair => pair.Item1).ToSet();
          result = result.Where(pair => f.Contains(pair.Item1));
        }

        if (!result.Any())
        {
          break;
        }
      }

      return result.Where(pair => (md.IsReadonly(pair.Item1) || this.FieldDB.IsACandidateReadonly(pair.Item1)));
    }

    private bool HaveSeenAllConstructorsOf(Type t)
    {
      var md = this.metadataDecoder;
      return md.ConstructorsCount(t) == this.nnFieldsAtMethodExitPoint.Where(pair => md.IsConstructor(pair.Key) && md.DeclaringType(pair.Key).Equals(t)).Count();
    }
    
    private void RecordAnalysisOf(Method m)
    {
      var md = this.metadataDecoder;
      if (md.IsConstructor(m))
      {
        var type = md.DeclaringType(m);
        Set<Method> constructorsAnalyzedForAType;
        if (!this.analyzedConstructors.TryGetValue(type, out constructorsAnalyzedForAType))
        {
          constructorsAnalyzedForAType = new Set<Method>();
          this.analyzedConstructors[type] = constructorsAnalyzedForAType;
        }
        constructorsAnalyzedForAType.Add(m);

        // We saw all the constructors for this type
        if (constructorsAnalyzedForAType.Count == md.ConstructorsCount(type))
        {
          this.completeTypes.Add(type);
        }
      }
    }
    #endregion
  }
}