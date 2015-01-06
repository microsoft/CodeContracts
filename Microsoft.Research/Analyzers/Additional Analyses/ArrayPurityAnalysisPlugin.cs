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
using Microsoft.Research.AbstractDomains;
using System.Collections.Generic;
using Microsoft.Research.DataStructures;
using Microsoft.Research.AbstractDomains.Numerical;

using System.Diagnostics.Contracts;

using Microsoft.Research.CodeAnalysis;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Research.CodeAnalysis
{
  public static partial class AnalysisWrapper
  {
    public static partial class TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>
      where Variable : IEquatable<Variable>
      where Expression : IEquatable<Expression>
      where Type : IEquatable<Type>
    {
      /// <summary>
      /// Analysis that determines which parts of an array (at the moment only parameter) are not written by the method body
      /// </summary>
      [ContractVerification(true)]
      public class ArrayPurityAnalysisPlugIn :
        GenericPlugInAnalysisForComposedAnalysis
      {
        #region Constants
        
        public readonly TwoValuesLattice<BoxedVariable<Variable>> UNMODIFIED 
          = new TwoValuesLattice<BoxedVariable<Variable>>(Reachability.State.Bottom, "unchanged");
        
        public readonly TwoValuesLattice<BoxedVariable<Variable>> MAYBEMODIFIED 
          = new TwoValuesLattice<BoxedVariable<Variable>>(Reachability.State.Top, "changed?");
        
        #endregion

        #region Invariant
        [ContractInvariantMethod]
        void ObjectInvariant()
        {
          Contract.Invariant(this.Id >= 0);
          Contract.Invariant(this.MAYBEMODIFIED != null);
          Contract.Invariant(this.UNMODIFIED != null);
        }

        #endregion

        #region Constructor

        public ArrayPurityAnalysisPlugIn(
          int id, string methodName,
          IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> mdriver,
          ILogOptions options,
          Predicate<APC> cachePCs
        )
          : base(id, methodName, mdriver, new PlugInAnalysisOptions(options), cachePCs)
        {
          Contract.Requires(id >= 0);
        }

        #endregion

        #region This abstract state selector
        public ArraySegmentationEnvironment<TwoValuesLattice<BoxedVariable<Variable>>, BoxedVariable<Variable>, BoxedExpression>
          Select(ArrayState state)
        {
          Contract.Requires(state != null);

          Contract.Ensures(Contract.Result<ArraySegmentationEnvironment<TwoValuesLattice<BoxedVariable<Variable>>, BoxedVariable<Variable>, BoxedExpression>>() != null);

          Contract.Assume(this.Id < state.PluginsCount, "Assuming the global invariant");

          var untyped = state.PluginAbstractStateAt(this.Id);
          var selected = untyped as ArraySegmentationEnvironment<TwoValuesLattice<BoxedVariable<Variable>>, BoxedVariable<Variable>, BoxedExpression>;

          Contract.Assume(selected != null);

          return selected;
        }
        #endregion

        #region Inherited

        public override ArrayState.AdditionalStates Kind
        {
          get { return ArrayState.AdditionalStates.ArrayPurity; }
        }

        public override IAbstractDomainForEnvironments<BoxedVariable<Variable>, BoxedExpression> InitialState
        {
          get
          {
            return new ArraySegmentationEnvironment<TwoValuesLattice<BoxedVariable<Variable>>, BoxedVariable<Variable>, BoxedExpression>(this.ExpressionManager);
          }
        }

        public override IAbstractDomainForEnvironments<BoxedVariable<Variable>, BoxedExpression> AssignInParallel(Dictionary<BoxedVariable<Variable>, FList<BoxedVariable<Variable>>> refinedMap, Converter<BoxedVariable<Variable>, BoxedExpression> convert, List<Pair<NormalizedExpression<BoxedVariable<Variable>>, NormalizedExpression<BoxedVariable<Variable>>>> equalities, ArrayState state)
        {
          Contract.Assume(state != null);
          Contract.Assume(refinedMap != null);
          Contract.Assume(convert != null);

          var result = Select(state);

          result.AssignInParallel(refinedMap, convert);          

          foreach(var pair in equalities)
          {
            Contract.Assume(pair.One != null);
            Contract.Assume(pair.Two != null);

            var tmp = result.TestTrueEqualAsymmetric(pair.One, pair.Two);
            result = result.TestTrueEqualAsymmetric(pair.One.PlusOne(), pair.Two.PlusOne());
          }

          return result;
        }

        public override IFactQuery<BoxedExpression, Variable> FactQuery(IFixpointInfo<APC, ArrayState> fixpoint)
        {
          return new ArrayPurityFactQuery(this, fixpoint);
        }

        #endregion

        #region Transfer functions

        #region Entry
        public override ArrayState Entry(APC pc, Method method, ArrayState data)
        {
          Contract.Assume(data != null);
          var resultState = data;

          var arrayParamCount = 0;

          // We materialize all the arrays in the parameters...
          foreach (var param in this.DecoderForMetaData.Parameters(method).Enumerate())
          {
            Variable symb;
            var postPC = Context.MethodContext.CFG.Post(pc);
            if (this.Context.ValueContext.TryParameterValue(postPC, param, out symb))
             {
               var paramType = this.DecoderForMetaData.ParameterType(param);
               if (this.DecoderForMetaData.IsArray(paramType))
               {
                 arrayParamCount++;
                 var dummy = MaterializeArray(postPC, Select(resultState), resultState.Numerical.CheckIfNonZero, symb, UNMODIFIED, UNMODIFIED);
               }
               else
               {
                 foreach (var intf in this.DecoderForMetaData.Interfaces(paramType))
                 {
                   if (this.DecoderForMetaData.Name(intf).AssumeNotNull().Contains("IEnumerable"))
                   {
                     var dummy = MaterializeEnumerable(postPC, Select(resultState), symb, UNMODIFIED);
                   }
                 }
               }
            }
          }

#if false && DEBUG
          if (arrayParamCount > 0)
          {
            Console.WriteLine("Method contains {0} array params", arrayParamCount);
          }
#endif
          return resultState;
        }
        #endregion

        #region Stelem
        public override ArrayState Stelem(
          APC pc, Type type, Variable array, Variable index, Variable value, ArrayState data)
        {
          Contract.Assume(data != null);

          var state = Select(data);
          var bArray = new BoxedVariable<Variable>(array);

          ArraySegmentation<TwoValuesLattice<BoxedVariable<Variable>>, BoxedVariable<Variable>, BoxedExpression> segment, newSegment;

          if (state.TryGetValue(bArray, out segment))
          {
            var indexExp = ToBoxedExpression(pc, index);
            var indexNormExp = indexExp.ToNormalizedExpression<Variable>();

            if (indexNormExp == null)
            {
              // Try to find a numerical bounds
              var indexValue = data.Numerical.BoundsFor(indexExp).AsInterval;
              if (indexValue.IsSingleton && indexValue.LowerBound.IsInteger)
              {
                indexNormExp = NormalizedExpression<BoxedVariable<Variable>>.For((Int32)indexValue.LowerBound);

                Contract.Assert(indexNormExp != null);
              }
              else
              {
                // We have a range of addresses
                Int32 low, upp;
                if (indexValue.IsFiniteAndInt32(out low, out upp))
                {
                  var lowExp = NormalizedExpression<BoxedVariable<Variable>>.For(low);
                  var uppExp = NormalizedExpression<BoxedVariable<Variable>>.For(upp);

                  if (segment.TrySetAbstractValue(lowExp, uppExp,
                    MAYBEMODIFIED,
                    data.Numerical, out newSegment))
                  {
                    state.Update(new BoxedVariable<Variable>(array), newSegment);

                    return data.UpdatePluginAt(this.Id, state);
                  }
                }
                state.RemoveElement(new BoxedVariable<Variable>(array));

                return data.UpdatePluginAt(this.Id, state);
              }
            }

            if (segment.TrySetAbstractValue(indexNormExp, MAYBEMODIFIED, data.Numerical, out newSegment))
            {
              // We explicty add the constant for indexNormExp, if we have one
              Rational val;
              if (data.Numerical.BoundsFor(indexExp).TryGetSingletonValue(out val) && val.IsInteger)
              {
                var k = (Int32)val;
                // Add k == indexNormExp
                newSegment = newSegment.TestTrueEqualAsymmetric(NormalizedExpression<BoxedVariable<Variable>>.For(k), indexNormExp);
              }

              // Add the sv = constants informations
              newSegment = newSegment.TestTrueWithIntConstants(data.Numerical.IntConstants.ToNormalizedExpressions());

              state.Update(new BoxedVariable<Variable>(array), newSegment);
            }
            else // For some reason, we were unable to set the abstract value, so we smash all the array to one
            {
              state.RemoveElement(new BoxedVariable<Variable>(array));
            }
          }

          return data.UpdatePluginAt(this.Id, state);
        }
        #endregion

        #region Assume
        public override ArrayState Assume(APC pc, string tag, Variable source, object provenance, ArrayState data)
        {
          Contract.Assume(data != null);

          if (tag != "false")
          {
            Variable v1, v2;
            if (this.ToBoxedExpression(pc, source).IsCheckExp1EqExp2(out v1, out v2))
            {
              data = data.UpdatePluginAt(this.Id, Select(data).TestTrueEqual(new BoxedVariable<Variable>(v1), new BoxedVariable<Variable>(v2)));
            }
          }
          return data.UpdatePluginAt(this.Id, Select(data).ReduceWith(data.Numerical));
        }
        #endregion

        #region Call
        public override ArrayState Call<TypeList, ArgList>(APC pc, Method method, bool tail, bool virt, TypeList extraVarargs, Variable dest, ArgList args, ArrayState data)
        {
          Contract.Assume(data != null);

          var postPC = this.Context.MethodContext.CFG.Post(pc);

          if (!this.MethodDriver.ExpressionLayer.ContractDecoder.IsPure(method))
          {
            var mySubState = Select(data);
            // Havoc all the arrays in the arguments that are not marked as pure
            for (var pos = this.DecoderForMetaData.IsStatic(method) ? 0 : 1; pos < args.Count; pos++)
            {
              Variable arrayValue, index;
              // An array is passed. Havoc the content
              if (mySubState.ContainsKey(new BoxedVariable<Variable>(args[pos])) && !this.DecoderForMetaData.IsPure(method, pos))
              {
                mySubState.RemoveVariable(new BoxedVariable<Variable>(args[pos]));
              }
              // an array is passed by ref: Havoc the array
              else if (this.Context.ValueContext.TryGetArrayFromElementAddress(pc, args[pos], out arrayValue, out index))
              {
                mySubState.RemoveVariable(new BoxedVariable<Variable>(arrayValue));
              }
            }

            data = data.UpdatePluginAt(this.Id, mySubState);
          }

          // Materialize the array if any
          data = MaterializeUnmodifiedIfArray(postPC, dest, data);

          return data;
        }
        #endregion

        #region ldsfield

        public override ArrayState Ldsfld(APC pc, Field field, bool @volatile, Variable dest, ArrayState data)
        {
          Contract.Assume(data != null);
          return MaterializeUnmodifiedIfArray(this.Context.MethodContext.CFG.Post(pc), dest, data);
        }

        #endregion

        #region ldfield

        public override ArrayState Ldfld(APC pc, Field field, bool @volatile, Variable dest, Variable obj, ArrayState data)
        {
          Contract.Assume(data != null);
          return MaterializeUnmodifiedIfArray(this.Context.MethodContext.CFG.Post(pc), dest, data);
        }

        #endregion

        #endregion

        #region Precondition suggestion

        public override void SuggestPrecondition(ContractInferenceManager inferenceManager, IFixpointInfo<APC, ArrayState> fixpointinfo)
        {
          Contract.Assume(inferenceManager!= null, "Assume for inheritance");

          if (
            !this.Options.LogOptions.SuggestRequiresPurityForArrays
            && !this.Options.LogOptions.PropagateRequiresPurityForArrays)
          {
            return;
          }

          ArrayState returnState;
          if (PreState(this.Context.MethodContext.CFG.NormalExit, fixpointinfo, out returnState))
          {
            Contract.Assume(returnState != null);
            var mySubState = Select(returnState);

#pragma warning disable 0219
            var first = true; // used in the commented part below
#pragma warning restore

            foreach (var element in mySubState.Elements)
            {
#if false // Make it true to debug the content of the result state
              if (ContainsUnModifiedSegment(element.Value))
              {
                if (first)
                {
                  Console.WriteLine("Method: {0}", this.Context.MethodContext.CurrentMethod);
                  first = false;
                }

                var arrayName = this.VariablesToPrettyNames(this.Context.MethodContext.CFG.EntryAfterRequires, element.Key);
                if (arrayName != null)
                {
                  var preconditions = element.Value.PrettyPrint(
                      arrayName,
                      (var => this.VariablesToPrettyNames(this.Context.MethodContext.CFG.EntryAfterRequires, var)),
                      (ael => this.PrettyPrint(arrayName, ael))
                      );
                  foreach (var str in preconditions)
                  {
                    output.WriteLine("Effect on the input array segment: {0}", str);
                  }
                }
              }
#endif
              var exp = ToBoxedExpression(this.Context.MethodContext.CFG.Entry, element.Key);
              var pre = PreconditionSuggestion.ExpressionInPreState(exp, this.Context,
                this.DecoderForMetaData, this.Context.MethodContext.CFG.EntryAfterRequires, 
                allowedKinds: ExpressionInPreStateKind.MethodPrecondition);

              Contract.Assume(element.Value != null);

              if (pre != null &&
                AllSegmentsUnmodified(element.Value) &&
                !this.DecoderForContracts.IsPure(this.Context.MethodContext.CurrentMethod)
                )
              {
                Parameter p;
                int pos;
                Contract.Assume(element.Key != null, "Assuming the precondition");
                if (TryParameterFor(element.Key, out p, out pos) && !this.DecoderForMetaData.IsPure(this.Context.MethodContext.CurrentMethod, p))
                {
                  // TODO: wire those suggestions to the contract inference manager
                  if (this.Options.LogOptions.SuggestRequiresPurityForArrays)
                  {
                    inferenceManager.Output.Suggestion(ClousotSuggestion.Kind.Requires, ClousotSuggestion.Kind.Requires.Message(), this.Context.MethodContext.CFG.EntryAfterRequires,
                        string.Format("Consider adding the [Pure] attribute to the parameter {0}", pre.ToString()), null, ClousotSuggestion.ExtraSuggestionInfo.None);
                  }

                  if (this.Options.LogOptions.PropagateRequiresPurityForArrays && pos <= 64)
                  {
                    this.DecoderForMetaData.AddPure(this.Context.MethodContext.CurrentMethod, pos);
                  }
                   
                }
              }
            }
          }

        }

        private bool TryParameterFor(BoxedVariable<Variable> var, out Parameter p, out int pos)
        {
          Contract.Requires(var != null);

          Variable v;
          if (var.TryUnpackVariable(out v) && TryParameterFor(v, out p, out pos))
          {
            return true;
          }

          pos = -1;
          p = default(Parameter);
          return false;
        }

        private bool TryParameterFor(Variable var, out Parameter p, out int pos)
        {
          pos = this.DecoderForMetaData.IsStatic(this.Context.MethodContext.CurrentMethod) ? 0 : 1;

          foreach (var param in this.DecoderForMetaData.Parameters(this.Context.MethodContext.CurrentMethod).Enumerate())
          {
            Variable symb;
            var postPC = Context.MethodContext.CFG.Post(this.Context.MethodContext.CFG.Entry);
            if (this.Context.ValueContext.TryParameterValue(postPC, param, out symb) && symb.Equals(var))
            {
              p = param;
              return true;
            }
            pos++;
          }

          pos = -1;
          p = default(Parameter);
          return false;
        }

        private bool ContainsUnModifiedSegment(
          ArraySegmentation<TwoValuesLattice<BoxedVariable<Variable>>, BoxedVariable<Variable>, BoxedExpression> arraySegmentation)
        {
          Contract.Requires(arraySegmentation != null);

          if (arraySegmentation.IsEmptyArray || !arraySegmentation.IsNormal)
          {
            return false;
          }

          foreach (var segment in arraySegmentation.Elements)
          {
            if (segment.IsBottom)
              return true;
          }

          return false;
        }

        private bool AllSegmentsUnmodified(
          ArraySegmentation<TwoValuesLattice<BoxedVariable<Variable>>, BoxedVariable<Variable>, BoxedExpression> arraySegmentation)
        {
          Contract.Requires(arraySegmentation != null);
          if (arraySegmentation.IsEmptyArray || !arraySegmentation.IsNormal)
          {
            return false;
          }

          foreach (var segment in arraySegmentation.Elements)
          {
            if (segment.IsTop)
              return false;
          }

          return true;
        }

        private string PrettyPrint(string arrayName, TwoValuesLattice<BoxedVariable<Variable>> abstractElement)
        {
          Contract.Requires(arrayName != null);
          Contract.Requires(abstractElement != null);

          if (abstractElement.IsBottom)
          {
            return string.Format("Contract.Old({0}[i]) == {0}[i]", arrayName);
          }
          else
          {
            return "changed?";
          }
        }
        #endregion

        #region Materialize array
        
        private ArrayState MaterializeUnmodifiedIfArray(APC postPC, Variable dest, ArrayState data)
        {
          Contract.Requires(data != null);

          var destType = this.Context.ValueContext.GetType(postPC, dest);

          if (destType.IsNormal && this.DecoderForMetaData.IsArray(destType.Value))
          {
            var mySubState = Select(data);
            if (this.MaterializeArray(
              postPC, mySubState, data.Numerical.CheckIfNonZero, dest, UNMODIFIED, UNMODIFIED) != null)
            {
              data = data.UpdatePluginAt(this.Id, mySubState);
            }
          }

          return data;
        }

        #endregion
      }

      #region FactQuery

      public class ArrayPurityFactQuery
        : IFactQuery<BoxedExpression, Variable>
      {
        #region Invariant

        [ContractInvariantMethod]
        void ObjectInvariant()
        {
          Contract.Invariant(this.analysis != null);
          Contract.Invariant(this.fixpoint != null);
        }

        #endregion

        #region State
        
        private readonly ArrayPurityAnalysisPlugIn analysis;
        private readonly IFixpointInfo<APC, ArrayState> fixpoint;
        
        #endregion

        #region Constructor

        public ArrayPurityFactQuery(ArrayPurityAnalysisPlugIn analysis, IFixpointInfo<APC,ArrayState> fixpoint)
        {
          Contract.Requires(analysis != null);
          Contract.Requires(fixpoint != null);

          this.fixpoint = fixpoint;
          this.analysis = analysis;
        }
        
        #endregion

        #region IFactQuery<BoxedExpression,Variable> Members

        public ProofOutcome IsNull(APC pc, BoxedExpression expr)
        {
          return ProofOutcome.Top;
        }

        public ProofOutcome IsNonNull(APC pc, BoxedExpression expr)
        {
          return ProofOutcome.Top;
        }

        public ProofOutcome IsTrue(APC pc, BoxedExpression condition)
        {
          return ProofOutcome.Top;
        }

        public ProofOutcome IsTrue(APC pc, Question question)
        {
          ArrayState preState;
          if (this.fixpoint.PreState(pc, out  preState))
          {
            return new IsArrayPure(this.analysis.Select(preState)).Visit(question);
          }

          return ProofOutcome.Bottom;
        }

        public ProofOutcome IsTrueImply(APC pc, FList<BoxedExpression> posAssumptions, FList<BoxedExpression> negAssumptions, BoxedExpression goal)
        {
          return ProofOutcome.Top;
        }

        public ProofOutcome IsGreaterEqualToZero(APC pc, BoxedExpression expr)
        {
          return ProofOutcome.Top;
        }

        public ProofOutcome IsLessThan(APC pc, BoxedExpression left, BoxedExpression right)
        {
          return ProofOutcome.Top;
        }

        public ProofOutcome IsNonZero(APC pc, BoxedExpression condition)
        {
          return ProofOutcome.Top;
        }

        public ProofOutcome HaveSameFloatType(APC pc, BoxedExpression left, BoxedExpression right)
        {
          return ProofOutcome.Top;
        }

        public bool TryGetFloatType(APC pc, BoxedExpression exp, out ConcreteFloat type)
        {
          type = default(ConcreteFloat);
          return false;
        }

        public IEnumerable<Variable> LowerBounds(APC pc, BoxedExpression exp, bool strict)
        {
          yield break; 
        }

        public IEnumerable<Variable> UpperBounds(APC pc, BoxedExpression exp, bool strict)
        {
          yield break;
        }

        public IEnumerable<BoxedExpression> LowerBoundsAsExpressions(APC pc, BoxedExpression exp, bool strict)
        {
          yield break;
        }

        public IEnumerable<BoxedExpression> UpperBoundsAsExpressions(APC pc, BoxedExpression exp, bool strict)
        {
          yield break;
        }

        public Pair<long, long> BoundsFor(APC pc, BoxedExpression exp)
        {
          return new Pair<long, long>(Int64.MinValue, Int64.MaxValue);
        }

        public ProofOutcome IsVariableDefinedForType(APC pc, Variable v, object type)
        {
          return ProofOutcome.Top;
        }

        public FList<BoxedExpression> InvariantAt(APC pc, FList<Variable> filterVariables, bool replaceVarsWithAccessPaths = true)
        {
          return FList<BoxedExpression>.Empty;
        }

        #endregion

        #region IFactBase<Variable> Members

        public ProofOutcome IsNull(APC pc, Variable value)
        {
          return ProofOutcome.Top;
        }

        public ProofOutcome IsNonNull(APC pc, Variable value)
        {
          return ProofOutcome.Top;
        }

        public bool IsUnreachable(APC pc)
        {
          return false;
        }
        #endregion

        class IsArrayPure : QuestionVisitor<BoxedVariable<Variable>, BoxedExpression>
        {
          [ContractInvariantMethod]
          void ObjectInvariant()
          {
            Contract.Invariant(this.state != null);
          }

          readonly private ArraySegmentationEnvironment<TwoValuesLattice<BoxedVariable<Variable>>, BoxedVariable<Variable>, BoxedExpression> state;

          public IsArrayPure(ArraySegmentationEnvironment<TwoValuesLattice<BoxedVariable<Variable>>, BoxedVariable<Variable>, BoxedExpression> state)
          {
            Contract.Requires(state != null);

            this.state = state;
          }

          protected override ProofOutcome VisitIsPureArray(BoxedVariable<Variable> array)
          {
            ArraySegmentation<TwoValuesLattice<BoxedVariable<Variable>>, BoxedVariable<Variable>, BoxedExpression> arrayAbstraction;
            if (this.state.TryGetValue(array, out arrayAbstraction))
            {
              if (arrayAbstraction.IsEmptyArray)
              {
                return ProofOutcome.Top;
              }

              foreach (var el in arrayAbstraction.Elements)
              {
                if (!el.IsBottom)
                {
                  return ProofOutcome.Top;
                }
              }

              return ProofOutcome.True;
            }

            return ProofOutcome.Top;
          }
        }
      }
      
      #endregion
    }
  }
}