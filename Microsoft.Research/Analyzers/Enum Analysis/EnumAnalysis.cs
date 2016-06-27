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
using Microsoft.Research.AbstractDomains;
using System.Diagnostics.Contracts;
using Microsoft.Research.DataStructures;

namespace Microsoft.Research.CodeAnalysis
{
  public static partial class AnalysisWrapper
  {
    /// <summary>
    /// Entry point to run the Enum analysis
    /// </summary>
    public static IMethodResult<Variable> RunEnumAnalysis<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>
    (
      string methodName,
      IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> driver,
      List<Analyzers.Enum.Options> options,
      Predicate<APC> cachePCs, DFAController controller
    )
      where Variable : IEquatable<Variable>
      where Expression : IEquatable<Expression>
      where Type : IEquatable<Type>
    {
      Contract.Requires(driver != null);
      Contract.Requires(options != null);
      Contract.Requires(options.Count > 0);

      var analysis =
       new TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>.EnumAnalysis
         (methodName, driver, options[0], cachePCs);

      return TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>.RunTheAnalysis(methodName, driver, analysis, controller);
    }

    /// <summary>
    /// This class is just for binding types for the internal clases
    /// </summary>
    public static partial class TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>
      where Variable : IEquatable<Variable>
      where Expression : IEquatable<Expression>
      where Type : IEquatable<Type>
    {
      public class EnumAnalysis :
        GenericValueAnalysis<EnumDefined<BoxedVariable<Variable>, Type, BoxedExpression>, IValueAnalysisOptions>
      {

        #region Private state
        [ContractInvariantMethod]
        void ObjectInvariant()
        {
          Contract.Invariant(this.lastTypeForGetTypeFromHandle != null);
        }


        private Dictionary<Variable, Type> lastTypeForGetTypeFromHandle = new Dictionary<Variable,Type>();
        protected void RenameTypes(Dictionary<BoxedVariable<Variable>, FList<BoxedVariable<Variable>>> sourceTargetMap)
        {
          Contract.Requires(sourceTargetMap != null);

          Contract.Ensures((this.lastTypeForGetTypeFromHandle != null) == (Contract.OldValue(this.lastTypeForGetTypeFromHandle) != null));

          if (this.lastTypeForGetTypeFromHandle != null)
          {
            var newMap = new Dictionary<Variable, Type>();

            foreach (var pair in this.lastTypeForGetTypeFromHandle)
            {
              var bKey = new BoxedVariable<Variable>(pair.Key);
              FList<BoxedVariable<Variable>> renamings;
              if (sourceTargetMap.TryGetValue(bKey, out renamings))
              {
                foreach (var newName in renamings.GetEnumerable())
                {
                  Variable v;
                  if (newName.TryUnpackVariable(out v))
                  {
                    newMap[v] = pair.Value;
                  }
                }
              }
            }

            this.lastTypeForGetTypeFromHandle = newMap;
          }
        }

        #endregion

        public EnumAnalysis(
          string methodName, 
          IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> driver,
          Analyzers.Enum.Options options,
          Predicate<APC> cachePCs
        )
          : base(methodName, driver, options, cachePCs)
        {
        }

        public override IFactQuery<BoxedExpression, Variable> FactQuery(IFixpointInfo<APC, EnumDefined<BoxedVariable<Variable>, Type, BoxedExpression>> fixpoint)
        {
          return new EnumFactQuery(this, fixpoint);
        }


        #region Transfer functions

        public EnumDefined<BoxedVariable<Variable>, Type, BoxedExpression> Rename(EnumDefined<BoxedVariable<Variable>, Type, BoxedExpression> state, Dictionary<BoxedVariable<Variable>, FList<BoxedVariable<Variable>>> refinedMap)
        {
          RenameTypes(refinedMap);

          return state.Rename(refinedMap);
        }

        public override EnumDefined<BoxedVariable<Variable>, Type, BoxedExpression> HelperForAssignInParallel(
          EnumDefined<BoxedVariable<Variable>, Type, BoxedExpression> state, Pair<APC, APC> edge, 
          Dictionary<BoxedVariable<Variable>, FList<BoxedVariable<Variable>>> refinedMap, Converter<BoxedVariable<Variable>, BoxedExpression> convert)
        {
          return Rename(state, refinedMap);
        }

        public override EnumDefined<BoxedVariable<Variable>, Type, BoxedExpression> Entry(APC pc, Method method, EnumDefined<BoxedVariable<Variable>, Type, BoxedExpression> data)
        {
          foreach (var param in this.DecoderForMetaData.Parameters(method).Enumerate())
          {
            Variable symb;
            if (this.Context.ValueContext.TryParameterValue(Context.MethodContext.CFG.Post(pc), param, out symb))
            {
              var typeForParam = this.Context.ValueContext.GetType(Context.MethodContext.CFG.Post(pc), symb);
              if (typeForParam.IsNormal && this.DecoderForMetaData.IsEnumWithoutFlagAttribute(typeForParam.Value))
              {
                data = data.AssumeCondition(typeForParam.Value, ToBoxedVariable(symb));
              }
            }
          }

          return data;
        }

        public override EnumDefined<BoxedVariable<Variable>, Type, BoxedExpression> 
          Assume(APC pc, string tag, Variable source, object provenance, EnumDefined<BoxedVariable<Variable>, Type, BoxedExpression> data)
        {          
          if (tag != "false")
          {
            return data.AssumeCondition(ToBoxedVariable(source));
          }
          else
          {
            // try to see if it is sv == 0
            var bExp = ToBoxedExpression(pc, source);
            BoxedExpression left;
            int k;
            Variable underlying;
            if (bExp.IsCheckExp1EqConst(out left, out k) && k == 0 && left.TryGetFrameworkVariable(out underlying))
            {
              return data.AssumeCondition(ToBoxedVariable(underlying));
            }

            return data;
          }
        }

        public override EnumDefined<BoxedVariable<Variable>, Type, BoxedExpression> 
          Assert(APC pc, string tag, Variable condition, object provenance, EnumDefined<BoxedVariable<Variable>, Type, BoxedExpression> data)
        {
          return data.AssumeCondition(ToBoxedVariable(condition));
        }

        public override EnumDefined<BoxedVariable<Variable>, Type, BoxedExpression> Call<TypeList, ArgList>(APC pc, Method method, 
          bool tail, bool virt, TypeList extraVarargs, Variable dest, ArgList args, EnumDefined<BoxedVariable<Variable>, Type, BoxedExpression> data)
        {
          var methodname = this.DecoderForMetaData.Name(method);
          var nsName = this.DecoderForMetaData.Namespace(this.DecoderForMetaData.DeclaringType(method));
          Type type;

          if (nsName != null && methodname != null
            && nsName.Equals("System") && methodname.Equals("GetTypeFromHandle"))
          {
            Type runtimeType;
            if (Context.ValueContext.IsRuntimeType(this.Context.MethodContext.CFG.Post(pc), dest, out runtimeType))  // Get the type
            {
              this.lastTypeForGetTypeFromHandle[dest] = runtimeType;
            }

            return data;
          }
          
          if (args.Count == 2 && methodname == "IsDefined" && this.lastTypeForGetTypeFromHandle.TryGetValue(args[0], out type))
          {
            data = data.AssumeTypeIff(ToBoxedVariable(dest), type, ToBoxedVariable(args[1]));

            Variable unboxed;
            if (this.Context.ValueContext.TryUnbox(pc, args[1], out unboxed))
            {
              data = data.AssumeTypeIff(ToBoxedVariable(dest), type, ToBoxedVariable(unboxed));
            }

            return data;
          }
          
          var destType = this.Context.ValueContext.GetType(this.MethodDriver.CFG.Post(pc), dest);
          if (destType.IsNormal && this.DecoderForMetaData.IsEnumWithoutFlagAttribute(destType.Value))
          {
            data = data.AssumeCondition(destType.Value, ToBoxedVariable(dest));
          }
          return data;
        }

        public override EnumDefined<BoxedVariable<Variable>, Type, BoxedExpression> Ldelem(APC pc, Type type, Variable dest, Variable array, Variable index, EnumDefined<BoxedVariable<Variable>, Type, BoxedExpression> data)
        {
          var arrayType = this.Context.ValueContext.GetType(pc, array);
          if (arrayType.IsNormal && this.DecoderForMetaData.IsArray(arrayType.Value))
          {
            return AssumeIfEnumType(this.DecoderForMetaData.ElementType(arrayType.Value), dest, data);
          }

          return data;
        }

        public override EnumDefined<BoxedVariable<Variable>, Type, BoxedExpression> Ldarg(APC pc, Parameter argument, bool isOld, Variable dest, EnumDefined<BoxedVariable<Variable>, Type, BoxedExpression> data)
        {
          return AssumeIfEnumType(pc, dest, data);
        }

        public override EnumDefined<BoxedVariable<Variable>, Type, BoxedExpression> Ldind(APC pc, Type type, bool @volatile, Variable dest, Variable ptr, EnumDefined<BoxedVariable<Variable>, Type, BoxedExpression> data)
        {
          return AssumeIfEnumType(pc, dest, data);
        }

        public override EnumDefined<BoxedVariable<Variable>, Type, BoxedExpression> Ldloc(APC pc, Local local, Variable dest, EnumDefined<BoxedVariable<Variable>, Type, BoxedExpression> data)
        {
          return AssumeIfEnumType(pc, dest, data);
        }

        public override EnumDefined<BoxedVariable<Variable>, Type, BoxedExpression> Ldfld(APC pc, Field field, bool @volatile, Variable dest, Variable obj, EnumDefined<BoxedVariable<Variable>, Type, BoxedExpression> data)
        {
          return AssumeIfEnumType(pc, dest, data);
        }

        #endregion

        #region Helper methods

         private EnumDefined<BoxedVariable<Variable>, Type, BoxedExpression> AssumeIfEnumType(APC pc, Variable dest, EnumDefined<BoxedVariable<Variable>, Type, BoxedExpression> data)
        {
          var type = this.Context.ValueContext.GetType(pc.Post(), dest);
          if (type.IsNormal)
          {
            return this.AssumeIfEnumType(type.Value, dest, data);
          }

          return data;
        }

        private EnumDefined<BoxedVariable<Variable>, Type, BoxedExpression> AssumeIfEnumType(Type type, Variable v, EnumDefined<BoxedVariable<Variable>, Type, BoxedExpression> data)
        {
          if (this.DecoderForMetaData.IsEnumWithoutFlagAttribute(type))
          {
            return data.AssumeCondition(type, ToBoxedVariable(v));
          }
          return data;
        }
        
        #endregion

        #region Implementation of the abstract methods
        public override EnumDefined<BoxedVariable<Variable>, Type, BoxedExpression> GetTopValue()
        {
          return EnumDefined<BoxedVariable<Variable>, Type, BoxedExpression>.Unknown;
        }

        public override bool SuggestAnalysisSpecificPostconditions(
          ContractInferenceManager inferenceManager, 
          IFixpointInfo<APC, EnumDefined<BoxedVariable<Variable>, Type, BoxedExpression>> fixpointInfo, 
          List<BoxedExpression> postconditions)
        {
          return false;
        }

        public override bool TrySuggestPostconditionForOutParameters(IFixpointInfo<APC, EnumDefined<BoxedVariable<Variable>, Type, BoxedExpression>> fixpointInfo, List<BoxedExpression> postconditions, Variable p, FList<PathElement> path)
        {
          return false;
        }
        #endregion

        #region EnumFactQuery

        [ContractVerification(true)]
        class EnumFactQuery : IFactQuery<BoxedExpression, Variable>
        {
          [ContractInvariantMethod]
          void ObjectInvariant()
          {
            Contract.Invariant(this.analysis != null);
          }

          private readonly EnumAnalysis analysis;
          private IFixpointInfo<APC, EnumDefined<BoxedVariable<Variable>, Type, BoxedExpression>> fixpoint;

          public EnumFactQuery(EnumAnalysis analysis, IFixpointInfo<APC, EnumDefined<BoxedVariable<Variable>, Type, BoxedExpression>> fixpoint)
          {
            Contract.Requires(analysis != null);

            this.analysis = analysis;
            this.fixpoint = fixpoint;
          }

          #region IFactQuery<Expression,Variable> Members

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
            EnumDefined<BoxedVariable<Variable>, Type, BoxedExpression> preState;
            if (this.fixpoint.PreState(pc, out preState) &&
              ((outcome = preState.CheckIfVariableIsDefined(new BoxedVariable<Variable>(v), type).ToProofOutcome()) != ProofOutcome.Top))
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

          public FList<BoxedExpression> InvariantAt(APC pc, FList<Variable> filterVariables, bool replaceVarsWithAccessPaths=true)
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
        }

        #endregion
      }

    }
  }
}