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
      /// Analysis that determines how an expressions refines to an array
      /// </summary>
      public class EnumAnalysisWrapperPlugIn :
        GenericPlugInAnalysisForComposedAnalysis
      {
        #region State
        private readonly EnumAnalysis enumAnalysis;
        #endregion

        #region Constructor
        public EnumAnalysisWrapperPlugIn(EnumAnalysis enumAnalysis, int id, string methodName,
          IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> mdriver,
          ILogOptions options,  Predicate<APC> cachePCs)
          : base(id, methodName, mdriver, new PlugInAnalysisOptions(options), cachePCs)
        {
          Contract.Requires(enumAnalysis != null);

          this.enumAnalysis = enumAnalysis;
        }
        #endregion

        #region Select and MakeState
        [Pure]
        public EnumDefined<BoxedVariable<Variable>, Type, BoxedExpression>  Select(ArrayState state)
        {
          Contract.Requires(state != null);
          Contract.Requires(this.Id < state.PluginsCount);

          Contract.Ensures(Contract.Result<EnumDefined<BoxedVariable<Variable>, Type, BoxedExpression>>() != null);

          var untyped = state.PluginAbstractStateAt(this.Id);
          var selected = untyped as EnumDefined<BoxedVariable<Variable>, Type, BoxedExpression>;

          Contract.Assume(selected != null);

          return selected;
        }

        public ArrayState MakeState(EnumDefined<BoxedVariable<Variable>, Type, BoxedExpression> newSubState, ArrayState oldState)
        {
          return oldState.UpdatePluginAt(this.Id, newSubState);
        }

        #endregion

        #region Transfer functions --- just wrappers for the standalone enum analysis

        public override ArrayState Entry(APC pc, Method method, ArrayState data)
        {
          return MakeState(this.enumAnalysis.Entry(pc, method, Select(data)), data);
        }

        public override ArrayState Assume(APC pc, string tag, Variable source, object provenance, ArrayState data)
        {
          var newData = data;
          var newSubState = this.enumAnalysis.Assume(pc, tag, source, provenance, Select(data));

          // At this point, we may know more on enums, so let's push them to the numerical domain

          if (newSubState.IsNormal())
          {
            var mdDecoder = this.DecoderForMetaData;
            var numericalDomain = data.Numerical;
            foreach (var pair in newSubState.DefinedVariables)
            {
              List<int> typeValues;
              // TODO: We may be smarted, and avoid recomputing the enum values if we already saw the type
              var type = pair.One;
              if (mdDecoder.IsEnumWithoutFlagAttribute(type) && mdDecoder.TryGetEnumValues(type, out typeValues))
              {
                // Assume the variable is in the enum
                numericalDomain.AssumeInDisInterval(pair.Two, DisInterval.For(typeValues)); 
              }
            }

            newData = newData.UpdateNumerical(numericalDomain);
          }

          return MakeState(newSubState, newData);
        }

        public override ArrayState Assert(APC pc, string tag, Variable condition, object provenance, ArrayState data)
        {
          return MakeState(this.enumAnalysis.Assert(pc, tag, condition, provenance, Select(data)), data);
        }

        public override ArrayState Call<TypeList, ArgList>(APC pc, Method method, bool tail, bool virt, TypeList extraVarargs, Variable dest, ArgList args, ArrayState data)
        {
          return MakeState(this.enumAnalysis.Call<TypeList, ArgList>(pc, method, tail, virt, extraVarargs, dest, args, Select(data)), data);
        }

        public override ArrayState Stelem(APC pc, Type type, Variable array, Variable index, Variable value, ArrayState data)
        {
          return MakeState(this.enumAnalysis.Stelem(pc, type, array, index, value, Select(data)), data);
        }

        public override ArrayState Ldelem(APC pc, Type type, Variable dest, Variable array, Variable index, ArrayState data)
        {
          return MakeState(this.enumAnalysis.Ldelem(pc, type, dest, array, index, Select(data)), data);
        }

        public override ArrayState Starg(APC pc, Parameter argument, Variable source, ArrayState data)
        {
          return MakeState(this.enumAnalysis.Starg(pc, argument, source, Select(data)), data);
        }

        public override ArrayState Ldarg(APC pc, Parameter argument, bool isOld, Variable dest, ArrayState data)
        {
          return MakeState(this.enumAnalysis.Ldarg(pc, argument, isOld, dest, Select(data)), data);
        }

        public override ArrayState Stind(APC pc, Type type, bool @volatile, Variable ptr, Variable value, ArrayState data)
        {
          return MakeState(this.enumAnalysis.Stind(pc, type, @volatile, ptr, value, Select(data)), data);
        }

        public override ArrayState Ldind(APC pc, Type type, bool @volatile, Variable dest, Variable ptr, ArrayState data)
        {
          return MakeState(this.enumAnalysis.Ldind(pc, type, @volatile, dest, ptr, Select(data)), data);
        }

        public override ArrayState Stloc(APC pc, Local local, Variable source, ArrayState data)
        {
          return MakeState(this.enumAnalysis.Stloc(pc, local, source, Select(data)), data);
        }

        public override ArrayState Ldloc(APC pc, Local local, Variable dest, ArrayState data)
        {
          return MakeState(this.enumAnalysis.Ldloc(pc, local, dest, Select(data)), data);
        }

        public override ArrayState Stfld(APC pc, Field field, bool @volatile, Variable obj, Variable value, ArrayState data)
        {
          return MakeState(this.enumAnalysis.Stfld(pc, field, @volatile, obj, value, Select(data)), data);
        }

        public override ArrayState Ldfld(APC pc, Field field, bool @volatile, Variable dest, Variable obj, ArrayState data)
        {
          return MakeState(this.enumAnalysis.Ldfld(pc, field, @volatile, dest, obj, Select(data)), data);
        }

        #endregion

        #region Overridden
        public override IAbstractDomainForEnvironments<BoxedVariable<Variable>, BoxedExpression> InitialState
        {
          get { return this.enumAnalysis.GetTopValue(); }
        }

        public override ArrayState.AdditionalStates Kind
        {
          get { return ArrayState.AdditionalStates.Enum; }
        }

        public override IAbstractDomainForEnvironments<BoxedVariable<Variable>, BoxedExpression> AssignInParallel(Dictionary<BoxedVariable<Variable>, FList<BoxedVariable<Variable>>> refinedMap, Converter<BoxedVariable<Variable>, BoxedExpression> convert, List<Pair<NormalizedExpression<BoxedVariable<Variable>>, NormalizedExpression<BoxedVariable<Variable>>>> equalities, ArrayState state)
        {
          Contract.Assume(state != null);
          Contract.Assume(refinedMap != null, "missing preconditions in base class");
          Contract.Assume(convert != null, "missing preconditions in base class");

          return  this.enumAnalysis.Rename(Select(state), refinedMap);
        }

        public override IFactQuery<BoxedExpression, Variable> FactQuery(IFixpointInfo<APC, ArrayState> fixpoint)
        {
          return new EnumPlugingFactQuery(this, fixpoint);
        }

        #endregion

        #region FactQuery

        public class EnumPlugingFactQuery : IFactQuery<BoxedExpression, Variable>
        {

          #region State

          private readonly EnumAnalysisWrapperPlugIn analysis;
          private readonly IFixpointInfo<APC, ArrayState> fixpoint;

          #endregion

          #region Constructor

          public EnumPlugingFactQuery(EnumAnalysisWrapperPlugIn analysis, IFixpointInfo<APC, ArrayState> fixpoint)
          {
            Contract.Requires(analysis != null);
            Contract.Requires(fixpoint != null);

            this.analysis = analysis;
            this.fixpoint = fixpoint;
          }

          #endregion

          #region implementation of the interface
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
            return ProofOutcome.Top; 

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

          public ProofOutcome IsVariableDefinedForType(APC pc, Variable v, object typeAsObject)
          {
            if (!(typeAsObject is Type))
            {
              return ProofOutcome.Top;
            }

            var type = (Type)typeAsObject;

            ProofOutcome outcome;
            ArrayState preState;
            if (this.fixpoint.PreState(pc, out preState) &&
              ((outcome = this.analysis.Select(preState).CheckIfVariableIsDefined(new BoxedVariable<Variable>(v), type).ToProofOutcome()) != ProofOutcome.Top))
            {
              return outcome;
            }

            var varType = this.analysis.Context.ValueContext.GetType(this.analysis.MethodDriver.CFG.Post(pc), v);
            if (varType.IsNormal && varType.Value.Equals(type))
            {
              return ProofOutcome.True;
            }

            return ProofOutcome.Top;
          
          }

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

          public FList<BoxedExpression> InvariantAt(APC pc, FList<Variable> variables = null, bool replaceVarsWithAccessPaths = true)
          {
            return FList<BoxedExpression>.Empty;
          }

          #endregion
        }
        
        #endregion

      }
    }
  }
}