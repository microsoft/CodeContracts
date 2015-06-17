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
    public class PlugInAnalysisOptions
      : Analyzers.ValueAnalysisOptions<PlugInAnalysisOptions>
    {
      public PlugInAnalysisOptions(ILogOptions options)
        : base(options)
      {
      }
    }

    public static partial class TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>
      where Variable : IEquatable<Variable>
      where Expression : IEquatable<Expression>
      where Type : IEquatable<Type>
    {
      /// <summary>
      /// This is the class to inherit from if you want to add a new plugin to the Array Analysis
      /// </summary>
      /// <typeparam name="AbstractDomain">The abstract domain of the analysis</typeparam>
      //[ContractClass(typeof(GenericPlugInAnalysisForComposedAnalysisContracts))]
      public abstract class GenericPlugInAnalysisForComposedAnalysis :
        GenericValueAnalysis<ArrayState, PlugInAnalysisOptions>
      {

        #region State
        public readonly int Id;
        #endregion

        #region Constructor

        protected GenericPlugInAnalysisForComposedAnalysis(
          int id,
          string methodName,
          IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> mdriver,
          PlugInAnalysisOptions options,
          Predicate<APC> cachePCs
        )
          : base(methodName, mdriver, options, cachePCs)
        {
          Contract.Ensures(this.Id == id);

          this.Id = id;
        }

        #endregion

        #region Abstract methods

        abstract public IAbstractDomainForEnvironments<BoxedVariable<Variable>, BoxedExpression> InitialState { get; }
        //abstract override public IFactQuery<BoxedExpression, Variable> FactQuery();
        //abstract override public IFactQuery<BoxedExpression, Variable> FactQuery(IFixpointInfo<APC,ArrayState> fixpoint);
        abstract public ArrayState.AdditionalStates Kind { get; }

        /// <returns>
        /// It should return the renamed *substate* not the whole one
        /// </returns>
        abstract public IAbstractDomainForEnvironments<BoxedVariable<Variable>, BoxedExpression> AssignInParallel
          (Dictionary<BoxedVariable<Variable>, FList<BoxedVariable<Variable>>> refinedMap,
          Converter<BoxedVariable<Variable>, BoxedExpression> convert,
          List<Pair<NormalizedExpression<BoxedVariable<Variable>>, NormalizedExpression<BoxedVariable<Variable>>>> equalities,
          ArrayState state);

        #endregion

        #region Default implementations of overridden

        public override bool SuggestAnalysisSpecificPostconditions(
          ContractInferenceManager inferenceManager, 
          IFixpointInfo<APC, ArrayState> fixpointInfo, 
          List<BoxedExpression> postconditions)
        {
          return false;
        }

        public override bool TrySuggestPostconditionForOutParameters(
          IFixpointInfo<APC, ArrayState> fixpointInfo, List<BoxedExpression> postconditions, Variable p, FList<PathElement> path)
        {
          return false;
        }
        
        #endregion

        #region Materialize Enumerble

        protected ArraySegmentation<Elements, BoxedVariable<Variable>, BoxedExpression> MaterializeEnumerable<Elements>(
          APC postPC, ArraySegmentationEnvironment<Elements, BoxedVariable<Variable>, BoxedExpression> preState,
          Variable enumerable, Elements initialValue)
                 where Elements : class, IAbstractDomainForArraySegmentationAbstraction<Elements, BoxedVariable<Variable>>
        {
          Variable modelArray;
          if (this.Context.ValueContext.TryGetModelArray(postPC, enumerable, out modelArray))
          {
            MaterializeArray(postPC, preState, (BoxedExpression be) => CheckOutcome.True, modelArray, initialValue, initialValue);
          }

          return null;
        }
   
        #endregion

        #region Materialize Array
        protected  ArraySegmentation<Elements, BoxedVariable<Variable>, BoxedExpression> MaterializeArray<Elements>(
          APC postPC, ArraySegmentationEnvironment<Elements, BoxedVariable<Variable>, BoxedExpression> preState, 
          Func<BoxedExpression, FlatAbstractDomain<bool>> CheckIfNonZero, Variable arrayValue, Elements initialValue, Elements bottom)
                 where Elements : class, IAbstractDomainForArraySegmentationAbstraction<Elements, BoxedVariable<Variable>>
        {
          Contract.Requires(preState != null);
          Contract.Requires(initialValue != null);

          var boxSym = new BoxedVariable<Variable>(arrayValue);

          ArraySegmentation<Elements, BoxedVariable<Variable>, BoxedExpression> arraySegment;
          if (preState.TryGetValue(boxSym, out arraySegment))
          {
            return arraySegment; // already materialized
          }

          Variable array_Length;
          if (this.Context.ValueContext.TryGetArrayLength(postPC, arrayValue, out array_Length))
          {
            var isNonEmpty = CheckIfNonZero(this.ToBoxedExpression(postPC, array_Length)).IsTrue();

            var limits = new NonNullList<SegmentLimit<BoxedVariable<Variable>>>() 
                { 
                  new SegmentLimit<BoxedVariable<Variable>>(NormalizedExpression<BoxedVariable<Variable>>.For(0), false),                     // { 0 }
                  new SegmentLimit<BoxedVariable<Variable>>(NormalizedExpression<BoxedVariable<Variable>>.For(new BoxedVariable<Variable>(array_Length)), !isNonEmpty) // { symb.Length }
                };

            var elements = new NonNullList<Elements>() { initialValue };

            var newSegment = new ArraySegmentation<Elements, BoxedVariable<Variable>, BoxedExpression>(
              limits, elements,
              bottom,
              this.ExpressionManager);

            preState.AddElement(new BoxedVariable<Variable>(arrayValue), newSegment);

            return newSegment;
          }

          return null;
        }
 
        #endregion

        #region Array loop counter initialization

        protected ArraySegmentationEnvironment<AbstractDomain, BoxedVariable<Variable>, BoxedExpression> 
          ArrayCounterInitialization<AbstractDomain>(APC pc, Local local, 
          Variable source, ArrayState resultState,  ArraySegmentationEnvironment<AbstractDomain, BoxedVariable<Variable>, BoxedExpression> mySubState)
          where AbstractDomain : class, IAbstractDomainForArraySegmentationAbstraction<AbstractDomain, BoxedVariable<Variable>>
        {
          Contract.Ensures(Contract.Result<ArraySegmentationEnvironment<AbstractDomain, BoxedVariable<Variable>, BoxedExpression>>() != null);

          var sourceExp = ToBoxedExpression(pc, source);

          // we look for loop initializations i == var
          Variable localValue;
          if (sourceExp.IsVariable
            &&
            this.Context.ValueContext.TryLocalValue(this.Context.MethodContext.CFG.Post(pc), local, out localValue))
          {
            // If source >= 0, we check if source <= arr.Length, for some array 'arr'
            // If this is the case, then we can try to refine the segmentation including source and locValue
            if (resultState.Numerical.CheckIfGreaterEqualThanZero(sourceExp).IsTrue())
            {
              var sourceNorm = ToNormalizedExpression(source);

              var toUpdate = new Dictionary<BoxedVariable<Variable>, ArraySegmentation<AbstractDomain, BoxedVariable<Variable>, BoxedExpression>>();

              foreach (var pair in mySubState.Elements)
              {
                if (!pair.Value.IsEmptyArray && pair.Value.IsNormal
                  &&
                  // We do the trick only for arrays {0} val {Len} as otherwise we should be more careful where we refine the segmentation, as for instance the update below may be too rough. However, I haven't find any interesting non-artificial example for it
                   pair.Value.Elements.Count() == 1)
                {
                  foreach (var limit in pair.Value.LastLimit)
                  {
                    if (resultState.Numerical.CheckIfLessEqualThan(sourceExp, limit.Convert(this.Encoder)).IsTrue())
                    {
                      IAbstractDomain abstractValue;
                      if (pair.Value.TryGetAbstractValue(
                        NormalizedExpression<BoxedVariable<Variable>>.For(0), sourceNorm,
                        resultState.Numerical,
                        out abstractValue)
                        &&
                        abstractValue is AbstractDomain)
                      {
                        ArraySegmentation<AbstractDomain, BoxedVariable<Variable>, BoxedExpression> newSegment;
                        if (pair.Value.TrySetAbstractValue(sourceNorm, (AbstractDomain) abstractValue, resultState.Numerical,
                          out newSegment))
                        {
                          toUpdate.Add(pair.Key, newSegment);
                          break; // We've already updated this segmentation
                        }
                      }
                    }
                  }
                }
              }
              foreach (var pair in toUpdate)
              {
                mySubState[pair.Key] = pair.Value;
              }
            }
          }
          return mySubState;
        }
  
        #endregion

        #region Visitors
        public override sealed ArrayState GetTopValue()
        {
          throw new NotSupportedException();
        }

        public override ArrayState Arglist(APC pc, Variable dest, ArrayState data)
        {
          return data;
        }

        public override ArrayState Assert(APC pc, string tag, Variable condition, object provenance, ArrayState data)
        {
          return Assume(pc, "assert", condition, provenance, data);
        }

        public override ArrayState Assume(APC pc, string tag, Variable source, object provenance, ArrayState data)
        {
          return data;
        }

        public override ArrayState BeginOld(APC pc, APC matchingEnd, ArrayState data)
        {
          return data;
        }

        public override ArrayState Binary(APC pc, BinaryOperator op, Variable dest, Variable s1, Variable s2, ArrayState data)
        {
          return data;
        }

        public override ArrayState Box(APC pc, Type type, Variable dest, Variable source, ArrayState data)
        {
          return data;
        }

        public override ArrayState Branch(APC pc, APC target, bool leave, ArrayState data)
        {
          return data;
        }

        public override ArrayState BranchCond(APC pc, APC target, BranchOperator bop, Variable value1, Variable value2, ArrayState data)
        {
          return data;
        }

        public override ArrayState BranchFalse(APC pc, APC target, Variable cond, ArrayState data)
        {
          return data;
        }

        public override ArrayState BranchTrue(APC pc, APC target, Variable cond, ArrayState data)
        {
          return data;
        }

        public override ArrayState Break(APC pc, ArrayState data)
        {
          return data;
        }

        public override ArrayState Call<TypeList, ArgList>(APC pc, Method method, bool tail, bool virt, TypeList extraVarargs, Variable dest, ArgList args, ArrayState data)
        {
          return data;
        }

        public override ArrayState Calli<TypeList, ArgList>(APC pc, Type returnType, TypeList argTypes, bool tail, bool isInstance, Variable dest, Variable fp, ArgList args, ArrayState data)
        {
          return data;
        }

        public override ArrayState Castclass(APC pc, Type type, Variable dest, Variable obj, ArrayState data)
        {
          return data;
        }

        public override ArrayState Ckfinite(APC pc, Variable dest, Variable source, ArrayState data)
        {
          return data;
        }

        public override ArrayState ConstrainedCallvirt<TypeList, ArgList>(APC pc, Method method, bool tail, Type constraint, TypeList extraVarargs, Variable dest, ArgList args, ArrayState data)
        {
          return data;
        }

        public override ArrayState Cpblk(APC pc, bool @volatile, Variable destaddr, Variable srcaddr, Variable len, ArrayState data)
        {
          return data;
        }

        public override ArrayState Cpobj(APC pc, Type type, Variable destptr, Variable srcptr, ArrayState data)
        {
          return data;
        }

        public override ArrayState Endfilter(APC pc, Variable decision, ArrayState data)
        {
          return data;
        }

        public override ArrayState Endfinally(APC pc, ArrayState data)
        {
          return data;
        }

        public override ArrayState EndOld(APC pc, APC matchingBegin, Type type, Variable dest, Variable source, ArrayState data)
        {
          return data;
        }

        public override ArrayState Entry(APC pc, Method method, ArrayState data)
        {
          return data;
        }

        internal override ArrayState HelperForJoin(ArrayState newState, ArrayState prevState, Pair<APC, APC> edge)
        {
          return base.HelperForJoin(newState, prevState, edge);
        }

        internal override ArrayState HelperForWidening(ArrayState newState, ArrayState prevState, Pair<APC, APC> edge)
        {
          return base.HelperForWidening(newState, prevState, edge);
        }

        public override ArrayState Initblk(APC pc, bool @volatile, Variable destaddr, Variable value, Variable len, ArrayState data)
        {
          return data;
        }

        public override ArrayState Initobj(APC pc, Type type, Variable ptr, ArrayState data)
        {
          return data;
        }

        public override ArrayState Isinst(APC pc, Type type, Variable dest, Variable obj, ArrayState data)
        {
          return data;
        }

        public override ArrayState Jmp(APC pc, Method method, ArrayState data)
        {
          return data;
        }

        public override ArrayState Join(Pair<APC, APC> edge, ArrayState newState, ArrayState prevState, out bool changed, bool widen)
        {
          return base.Join(edge, newState, prevState, out changed, widen) ;
        }

        public override ArrayState Ldarg(APC pc, Parameter argument, bool isOld, Variable dest, ArrayState data)
        {
          return data;
        }

        public override ArrayState Ldarga(APC pc, Parameter argument, bool isOld, Variable dest, ArrayState data)
        {
          return data;
        }

        public override ArrayState Ldconst(APC pc, object constant, Type type, Variable dest, ArrayState data)
        {
          return data;
        }

        public override ArrayState Ldelem(APC pc, Type type, Variable dest, Variable array, Variable index, ArrayState data)
        {
          return data;
        }

        public override ArrayState Ldelema(APC pc, Type type, bool @readonly, Variable dest, Variable array, Variable index, ArrayState data)
        {
          return data;
        }

        public override ArrayState Ldfieldtoken(APC pc, Field type, Variable dest, ArrayState data)
        {
          return data;
        }

        public override ArrayState Ldfld(APC pc, Field field, bool @volatile, Variable dest, Variable obj, ArrayState data)
        {
          return data;
        }

        public override ArrayState Ldflda(APC pc, Field field, Variable dest, Variable obj, ArrayState data)
        {
          return data;
        }

        public override ArrayState Ldftn(APC pc, Method method, Variable dest, ArrayState data)
        {
          return data;
        }

        public override ArrayState Ldind(APC pc, Type type, bool @volatile, Variable dest, Variable ptr, ArrayState data)
        {
          return data;
        }

        public override ArrayState Ldlen(APC pc, Variable dest, Variable array, ArrayState data)
        {
          return data;
        }

        public override ArrayState Ldloc(APC pc, Local local, Variable dest, ArrayState data)
        {
          return data;
        }

        public override ArrayState Ldloca(APC pc, Local local, Variable dest, ArrayState data)
        {
          return data;
        }

        public override ArrayState Ldmethodtoken(APC pc, Method type, Variable dest, ArrayState data)
        {
          return data;
        }

        public override ArrayState Ldnull(APC pc, Variable dest, ArrayState data)
        {
          return data;
        }

        public override ArrayState Ldresult(APC pc, Type type, Variable dest, Variable source, ArrayState data)
        {
          return data;
        }

        public override ArrayState Ldsfld(APC pc, Field field, bool @volatile, Variable dest, ArrayState data)
        {
          return data;
        }

        public override ArrayState Ldsflda(APC pc, Field field, Variable dest, ArrayState data)
        {
          return data;
        }

        public override ArrayState Ldstack(APC pc, int offset, Variable dest, Variable source, bool isOld, ArrayState data)
        {
          return data;
        }

        public override ArrayState Ldstacka(APC pc, int offset, Variable dest, Variable source, Type type, bool isOld, ArrayState data)
        {
          return data;
        }

        public override ArrayState Ldtypetoken(APC pc, Type type, Variable dest, ArrayState data)
        {
          return data;
        }

        public override ArrayState Ldvirtftn(APC pc, Method method, Variable dest, Variable obj, ArrayState data)
        {
          return data;
        }

        public override ArrayState Localloc(APC pc, Variable dest, Variable size, ArrayState data)
        {
          return data;
        }

        public override ArrayState Mkrefany(APC pc, Type type, Variable dest, Variable obj, ArrayState data)
        {
          return data;
        }

        public override ArrayState MutableVersion(ArrayState state)
        {
          return base.MutableVersion(state);
        }

        public override ArrayState Newarray<ArgList>(APC pc, Type type, Variable dest, ArgList lengths, ArrayState data)
        {
          return data;
        }

        public override ArrayState Newobj<ArgList>(APC pc, Method ctor, Variable dest, ArgList args, ArrayState data)
        {
          return data;
        }

        public override ArrayState Nop(APC pc, ArrayState data)
        {
          return data;
        }

        public override ArrayState Pop(APC pc, Variable source, ArrayState data)
        {
          return data;
        }

        public override ArrayState Refanytype(APC pc, Variable dest, Variable source, ArrayState data)
        {
          return data;
        }

        public override ArrayState Refanyval(APC pc, Type type, Variable dest, Variable source, ArrayState data)
        {
          return data;
        }

        public override ArrayState Rethrow(APC pc, ArrayState data)
        {
          return data;
        }

        public override ArrayState Return(APC pc, Variable source, ArrayState data)
        {
          return data;
        }

        public override ArrayState Sizeof(APC pc, Type type, Variable dest, ArrayState data)
        {
          return data;
        }

        public override ArrayState Starg(APC pc, Parameter argument, Variable source, ArrayState data)
        {
          return data;
        }

        public override ArrayState Stelem(APC pc, Type type, Variable array, Variable index, Variable value, ArrayState data)
        {
          return data;
        }

        public override ArrayState Stfld(APC pc, Field field, bool @volatile, Variable obj, Variable value, ArrayState data)
        {
          return data;
        }

        public override ArrayState Stind(APC pc, Type type, bool @volatile, Variable ptr, Variable value, ArrayState data)
        {
          return data;
        }

        public override ArrayState Stloc(APC pc, Local local, Variable source, ArrayState data)
        {
          return data;
        }

        public override ArrayState Stsfld(APC pc, Field field, bool @volatile, Variable value, ArrayState data)
        {
          return data;
        }

        public override ArrayState Switch(APC pc, Type type, IEnumerable<Pair<object, APC>> cases, Variable value, ArrayState data)
        {
          return data;
        }

        public override ArrayState Throw(APC pc, Variable exn, ArrayState data)
        {
          return data;
        }

        public override ArrayState Unary(APC pc, UnaryOperator op, bool overflow, bool unsigned, Variable dest, Variable source, ArrayState data)
        {
          return data;
        }

        public override ArrayState Unbox(APC pc, Type type, Variable dest, Variable obj, ArrayState data)
        {
          return data;
        }

        public override ArrayState Unboxany(APC pc, Type type, Variable dest, Variable obj, ArrayState data)
        {
          return data;
        }

        #endregion

        #region Normalized Expressions
        static public NormalizedExpression<BoxedVariable<Variable>> ToNormalizedExpression(Variable v)
        {
          Contract.Ensures(Contract.Result<NormalizedExpression<BoxedVariable<Variable>>>() != null);

          return NormalizedExpression<BoxedVariable<Variable>>.For(new BoxedVariable<Variable>(v));
        }
        #endregion

        #region Postcondition suggestion

        /// <summary>
        /// Does nothing, as a plugin as no fixpoint
        /// </summary>
        public override void SuggestPrecondition(ContractInferenceManager inferenceManager)
        {
          return;
        }

        virtual public void SuggestPrecondition(ContractInferenceManager inferenceManager, IFixpointInfo<APC, ArrayState> fixpointInfo)
        {
          // does nothing by default
        }
        #endregion
      }

#if false // F: C# compiler is not happy for some reason which is unclear to me...
      [ContractClassFor(typeof(GenericPlugInAnalysisForComposedAnalysis))]
      abstract class GenericPlugInAnalysisForComposedAnalysisContracts :
        GenericPlugInAnalysisForComposedAnalysis
      {
        public override IAbstractDomainForEnvironments<BoxedVariable<Variable>, BoxedExpression> InitialState
        {
          get { 
            Contract.Ensures(Contract.Result<IAbstractDomainForEnvironments<BoxedVariable<Variable>, BoxedExpression>>() != null); 
            throw new NotImplementedException(); }
        }

        public override IFactQuery<BoxedExpression, Variable> FactQuery
        {
          get { throw new NotImplementedException(); }
        }

        public override ArrayState.AdditionalStates Kind
        {
          get { throw new NotImplementedException(); }
        }

        public override IAbstractDomainForEnvironments<BoxedVariable<Variable>, BoxedExpression> AssignInParallel(
          Dictionary<BoxedVariable<Variable>, FList<BoxedVariable<Variable>>> refinedMap, 
          Converter<BoxedVariable<Variable>, BoxedExpression> convert, 
          List<Pair<NormalizedExpression<BoxedVariable<Variable>>, NormalizedExpression<BoxedVariable<Variable>>>> equalities, 
          ArrayState state)
        {
          Contract.Requires(refinedMap != null);
          Contract.Requires(convert != null);
          Contract.Requires(equalities != null);
          Contract.Requires(state != null);

          Contract.Ensures(Contract.Result<IAbstractDomainForEnvironments<BoxedVariable<Variable>, BoxedExpression>>() != null);

          return default(IAbstractDomainForEnvironments<BoxedVariable<Variable>, BoxedExpression>);
        }
      }
#endif

    }
  }
}