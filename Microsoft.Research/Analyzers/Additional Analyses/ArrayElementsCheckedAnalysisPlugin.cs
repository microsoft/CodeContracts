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
      /// Analysis that determines which array elements have tested (or not tested).
      /// It is used to infer preconditions for array contents
      /// </summary>
      [ContractVerification(true)]
      public class ArrayElementsCheckedAnalysisPlugin :
        GenericPlugInAnalysisForComposedAnalysis
      {
        #region Invariant
        [ContractInvariantMethod]
        void ObjectInvariant()
        {
          Contract.Invariant(this.Id >= 0);
        }

        #endregion

        #region Constants

        static public readonly FlatAbstractDomainOfBoolsWithRenaming<BoxedVariable<Variable>> CHECKED
          = new FlatAbstractDomainOfBoolsWithRenaming<BoxedVariable<Variable>>(true, "checked"); // Thread-safe

        static public readonly FlatAbstractDomainOfBoolsWithRenaming<BoxedVariable<Variable>> UNCHECKED
          = new FlatAbstractDomainOfBoolsWithRenaming<BoxedVariable<Variable>>(false, "not-checked"); // Thread-safe

        #endregion

        #region Constructor

        public ArrayElementsCheckedAnalysisPlugin(
          int id, string methodName,
          IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> mdriver,
          ILogOptions options,
          Predicate<APC> cachePCs
          )
          : base(id, methodName, mdriver, new PlugInAnalysisOptions(options), cachePCs)
        {
        }
        #endregion

        #region Override
        public override IFactQuery<BoxedExpression, Variable> FactQuery(IFixpointInfo<APC, ArrayState> fixpoint)
        {
          return null;
        }

        public override ArrayState.AdditionalStates Kind
        {
          get { return ArrayState.AdditionalStates.ArrayChecked; }
        }

        public override IAbstractDomainForEnvironments<BoxedVariable<Variable>, BoxedExpression> InitialState
        {
          get
          {
            return new ArraySegmentationEnvironment<FlatAbstractDomainOfBoolsWithRenaming<BoxedVariable<Variable>>, BoxedVariable<Variable>, BoxedExpression>(
              this.ExpressionManager);
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

        #endregion

        #region Select

        public ArraySegmentationEnvironment<FlatAbstractDomainOfBoolsWithRenaming<BoxedVariable<Variable>>, BoxedVariable<Variable>, BoxedExpression>
          Select(ArrayState state)
        {
          Contract.Requires(state != null);
          Contract.Requires(this.Id < state.PluginsCount);
          Contract.Ensures(
            Contract.Result<
            ArraySegmentationEnvironment<
            FlatAbstractDomainOfBoolsWithRenaming<BoxedVariable<Variable>>, BoxedVariable<Variable>, BoxedExpression>>() != null);

          var subState = state.PluginAbstractStateAt(this.Id)
            as ArraySegmentationEnvironment<FlatAbstractDomainOfBoolsWithRenaming<BoxedVariable<Variable>>, BoxedVariable<Variable>, BoxedExpression>;

          Contract.Assume(subState != null);

          return subState;
        }

        #endregion

        #region Transfer functions

        #region Entry

        public override ArrayState Entry(APC pc, Method method, ArrayState data)
        {
          Contract.Assume(data != null);

          var mySubState = Select(data);

          // We materialize all the arrays in the parameters...
          foreach (var param in this.DecoderForMetaData.Parameters(method).Enumerate())
          {
            Variable symb;
            var postPC = Context.MethodContext.CFG.Post(pc);
            if (
              this.Context.ValueContext.TryParameterValue(postPC, param, out symb))
            {
              if (this.DecoderForMetaData.IsArray(DecoderForMetaData.ParameterType(param)))
              {
                var dummy = MaterializeArray(
                  postPC, mySubState,
                  data.Numerical.CheckIfNonZero, symb, UNCHECKED,
                  new FlatAbstractDomainOfBoolsWithRenaming<BoxedVariable<Variable>>(FlatAbstractDomain<bool>.State.Bottom));
              }
              else
              {
                var paramType = this.DecoderForMetaData.ParameterType(param);
                foreach (var intf in this.DecoderForMetaData.Interfaces(paramType))
                {
                  if (this.DecoderForMetaData.Name(intf).AssumeNotNull().Contains("IEnumerable"))
                  {
                    var dummy = MaterializeEnumerable(postPC, mySubState, symb, UNCHECKED);
                  }
                }
              }
            }
          }

          return data.UpdatePluginAt(this.Id, mySubState);
        }

        #endregion

        #region Assert

        public override ArrayState Assert(APC pc, string tag, Variable condition, object provenance, ArrayState data)
        {
          Contract.Ensures(Contract.Result<ArrayState>() != null);

          Contract.Assume(data != null);

          var conditionExp = ToBoxedExpression(pc, condition);

          if(conditionExp == null)
          {
            return data;
          }
          
          // We first check (x == nul) == 0
          BoxedExpression checkedExp;
          Variable var1, var2;
          if (conditionExp.IsCheckExpNotNotNull(out checkedExp) && checkedExp.TryGetFrameworkVariable(out var1))
          {
            return AssumeCheckOfAnArrayElement(pc, var1, data);
          }
          else if (conditionExp.IsCheckExp1EqExp2(out var1, out var2))
          {
            var refined = Select(data).TestTrueEqual(new BoxedVariable<Variable>(var1), new BoxedVariable<Variable>(var2));

            return data.UpdatePluginAt(this.Id, refined);
          }
          else
          {
            var t1 = Select(data);

            // Otherwise see if "assert refined.One[refined.Two]
            return AssumeCheckOfAnArrayElement(pc, condition, data);
          }
        }

        private ArrayState AssumeCheckOfAnArrayElement(APC pc, Variable var, ArrayState data)
        {
          Contract.Requires(data != null);
          Contract.Ensures(Contract.Result<ArrayState>() != null);

          Pair<BoxedVariable<Variable>, BoxedVariable<Variable>> refined;
          if (data.CanRefineToArrayLoad(new BoxedVariable<Variable>(var), out refined))
          {
            var mySubState = Select(data);
            ArraySegmentation<
              FlatAbstractDomainOfBoolsWithRenaming<BoxedVariable<Variable>>, BoxedVariable<Variable>, BoxedExpression> segmentation;

            Contract.Assume(refined.One != null);
            Contract.Assume(refined.Two != null);

            // Do we have a segmentation?
            foreach (var arr in GetEquals(pc, refined.One, data))
            {
              if (mySubState.TryGetValue(arr, out segmentation))
              {
                // Is it unmodified? 
                if (data.IsUnmodifiedArrayElementFromEntry(arr, ToBoxedExpression(pc, refined.Two)))
                {
                  var norm = ToBoxedExpression(pc, refined.Two).ToNormalizedExpression<Variable>();
                  if (norm != null && segmentation.TrySetAbstractValue(norm, CHECKED, data.Numerical, out segmentation))
                  {
                    mySubState.Update(arr, segmentation);
                  }
                  else
                  {
                    mySubState.RemoveElement(arr);
                  }

                  data = data.UpdatePluginAt(this.Id, mySubState);
                }
              }
            }
          }

          return data;
        }

        IEnumerable<BoxedVariable<Variable>> GetEquals(APC pc, BoxedVariable<Variable> arr, ArrayState state)
        {
          yield return arr;

          foreach (var x in new List<BoxedVariable<Variable>>(Select(state).Keys))
          {
            if (state.Numerical.CheckIfEqual(ToBoxedExpression(pc, arr), ToBoxedExpression(pc, x)).IsTrue())
            {
              yield return x;
            }
          }
        }

        #endregion

        #region Assume
        public override ArrayState Assume(APC pc, string tag, Variable source, object provenance, ArrayState data)
        {
          Contract.Assume(data != null);

          if (tag != "false")
          {
            data = this.Assert(pc, "assume", source, provenance, data);

            Contract.Assert(data != null);

            var convertedExp = ToBoxedExpression(pc, source);

            if(convertedExp == null)
            {
              // abort
              return data;
            }

            Variable v1, v2;
            BoxedExpression bLeft, bRight, x;
            int k;
            if (convertedExp.IsCheckExp1EqExp2(out bLeft, out bRight)  // bLeft == bRight
              && bRight.IsCheckExp1EqConst(out x, out k)                                // bRight == x + 1
              && bLeft.TryGetFrameworkVariable(out v1)
              && x.TryGetFrameworkVariable(out v2)
              )
            {
              var vNormExp = NormalizedExpression<BoxedVariable<Variable>>.For(new BoxedVariable<Variable>(v1));
              var sumNormExp =  NormalizedExpression<BoxedVariable<Variable>>.For(new BoxedVariable<Variable>(v2), k);
              
              var mySubState = Select(data);
              mySubState = mySubState.TestTrueEqualAsymmetric(vNormExp, sumNormExp);
              mySubState = mySubState.TestTrueEqualAsymmetric(sumNormExp, vNormExp);

              data = data.UpdatePluginAt(this.Id, mySubState);
            }
          }

          return data.UpdatePluginAt(this.Id, Select(data).ReduceWith(data.Numerical));
        }
        #endregion

        #region Call
        public override ArrayState Call<TypeList, ArgList>(APC pc, Method method, bool tail, bool virt, TypeList extraVarargs, Variable dest, ArgList args, ArrayState data)
        {
          Contract.Assume(data != null);

          if (!this.DecoderForContracts.IsPure(method))
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

          if (!this.DecoderForMetaData.IsStatic(method))
          {
            Contract.Assume(args.Count > 0);
            return AssumeCheckOfAnArrayElement(pc, args[0], data);
          }
          else
          {
            return data;
          }
        }
        #endregion

        #region Binary 
        public override ArrayState Binary(APC pc, BinaryOperator op, Variable dest, Variable s1, Variable s2, ArrayState data)
        {
          Variable var;
          int k;

          if (TryMatchVariableConstant(pc, op, Strip(pc, s1), Strip(pc, s2), data.Numerical, out var, out k) && k != Int32.MinValue)
          {
            var mySubState = Select(data);

            var normExpression = NormalizedExpression<BoxedVariable<Variable>>.For(new BoxedVariable<Variable>(var), k);
            var normVariable = NormalizedExpression<BoxedVariable<Variable>>.For(new BoxedVariable<Variable>(dest));

            var array = mySubState.TestTrueEqualAsymmetric(normVariable, normExpression);

            normExpression = NormalizedExpression<BoxedVariable<Variable>>.For(new BoxedVariable<Variable>(dest), -k);
            normVariable = NormalizedExpression<BoxedVariable<Variable>>.For(new BoxedVariable<Variable>(var));

            return data.UpdatePluginAt(this.Id, mySubState.TestTrueEqualAsymmetric(normExpression, normVariable));
          }

          return data;
        }
        #endregion

        #region StLoc

        public override ArrayState Stloc(APC pc, Local local, Variable source, ArrayState data)
        {
          var resultState = base.Stloc(pc, local, source, data);

          Contract.Assume(resultState != null);

          return resultState.UpdatePluginAt(this.Id, ArrayCounterInitialization(pc, local, source, resultState, Select(resultState)));
        }

        #endregion

        #region Collect not-null assertions

        public override ArrayState Ldelem(APC pc, Type type, Variable dest, Variable array, Variable index, ArrayState data)
        {
          Contract.Assume(data != null);

          return AssumeCheckOfAnArrayElement(pc, array, data);
        }

        public override ArrayState Ldelema(APC pc, Type type, bool @readonly, Variable dest, Variable array, Variable index, ArrayState data)
        {
          Contract.Assume(data != null);
          
          return AssumeCheckOfAnArrayElement(pc, array, data);
        }

        public override ArrayState Ldfld(APC pc, Field field, bool @volatile, Variable dest, Variable obj, ArrayState data)
        {
          Contract.Assume(data != null);
          return AssumeCheckOfAnArrayElement(pc, obj, data);
        }

        #endregion

        #endregion

        #region Precondition suggestion

        public override void SuggestPrecondition(ContractInferenceManager inferenceManager, IFixpointInfo<APC, ArrayState> fixpointInfo)
        {
          Contract.Assume(inferenceManager != null);
          ArrayState postState;
          var pc = this.Context.MethodContext.CFG.NormalExit;
          if (PreState(pc, fixpointInfo, out postState))
          {
            Contract.Assume(postState != null);

            var mySubState = Select(postState);

            if (mySubState.IsNormal())
            {
              foreach (var segmentation in mySubState.Elements)
              {
                Contract.Assume(segmentation.Key != null);
                Contract.Assume(segmentation.Value != null);

                if (segmentation.Value.IsNormal())
                {
                  foreach (var pre in GetForAllVisibleOutsideOfTheMethod(segmentation.Key, segmentation.Value))
                  {
                    var pcAfterRequires = this.Context.MethodContext.CFG.EntryAfterRequires;
                    inferenceManager.AddPreconditionOrAssume(new EmptyProofObligation(pcAfterRequires), new List<BoxedExpression>() { pre }, ProofOutcome.Top);
                  }
                }
              }
            }
          }
        }

        private List<BoxedExpression> GetForAllVisibleOutsideOfTheMethod(
          BoxedVariable<Variable> array,
          ArraySegmentation<FlatAbstractDomainOfBoolsWithRenaming<BoxedVariable<Variable>>, BoxedVariable<Variable>, BoxedExpression> segmentation)
        {
          Contract.Requires(array != null);
          Contract.Requires(segmentation != null);

          var result = new List<BoxedExpression>();

          var arrayBE = ReadVariableInPreStateForRequires(this.Context.MethodContext.CFG.EntryAfterRequires, array);

          if (arrayBE != null)
          {
            var boundVar = BoxedExpression.Var("i");
            var arrayindex = new BoxedExpression.ArrayIndexExpression<Type>(arrayBE, boundVar, this.DecoderForMetaData.System_Object);
            var factory = new BoxedExpressionFactory<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Variable>(              
              boundVar, arrayindex, 
              (v => ReadVariableInPreStateForRequires(this.Context.MethodContext.CFG.EntryAfterRequires, v)),
              this.DecoderForMetaData, null);
            var formula = segmentation.To(factory);
            if (formula != null)
            {
              result.AddRange(factory.SplitAnd(formula));
            }
          }

          return result;        
        }


        public string PrettyPrint(string arrayName, FlatAbstractDomainOfBoolsWithRenaming<BoxedVariable<Variable>> aState)
        {
          Contract.Requires(arrayName != null);
          Contract.Requires(aState != null);

          if (aState.IsBottom)
          {
            return "false";
          }
#if false // Verbose
          if (aState.IsTop) 
          {
            return "maybe checked";
          }
          if (!aState.BoxedElement)
          {
            return "never checked";
          }
#else
          if (aState.IsTop /*|| !aState.BoxedElement*/)
          {
            return null;
          }
#endif
          if (aState.BoxedElement)
          {
            return string.Format("{0}[i] != null", arrayName);
          }

          // Should be unreachable
          return null; 
        }
        #endregion
      }
    }
  }
}