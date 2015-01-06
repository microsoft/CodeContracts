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

using System.Diagnostics.Contracts;
using Microsoft.Research.AbstractDomains;
using System.Collections.Generic;
using Microsoft.Research.DataStructures;
using Microsoft.Research.AbstractDomains.Expressions;
using Microsoft.Research.AbstractDomains.Numerical;


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
      /// Analysis that propagates existential facts
      /// </summary>
      [ContractVerification(true)]
      public class ExistentialAnalysisPlugIn :
        ArrayAnalysisBasePlugIn<NonRelationalValueAbstraction<BoxedVariable<Variable>, BoxedExpression>>
      {

        #region Constructor

        public ExistentialAnalysisPlugIn(
          int pluginCount, 
          string name,
          IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> driver,
          ILogOptions options,
          Predicate<APC> cachePCs
        )
          : base(pluginCount, name, driver, options, cachePCs)
        {
        }

        #endregion

        #region Implementation of overridden

        public override IAbstractDomainForEnvironments<BoxedVariable<Variable>, BoxedExpression> InitialState
        {
          get
          {
            return
              new ArraySegmentationEnvironment<NonRelationalValueAbstraction<BoxedVariable<Variable>, BoxedExpression>, BoxedVariable<Variable>, BoxedExpression>(this.ExpressionManager);
          }
        }

        public override IFactQuery<BoxedExpression, Variable> FactQuery(IFixpointInfo<APC, ArrayState> fixpoint)
        {
          return null;
        }

        public override ArrayState.AdditionalStates Kind
        {
          get { return ArrayState.AdditionalStates.Existential; }
        }

        public override IAbstractDomainForEnvironments<BoxedVariable<Variable>, BoxedExpression>
          AssignInParallel(Dictionary<BoxedVariable<Variable>, FList<BoxedVariable<Variable>>> refinedMap,
          Converter<BoxedVariable<Variable>, BoxedExpression> convert, List<Pair<NormalizedExpression<BoxedVariable<Variable>>, NormalizedExpression<BoxedVariable<Variable>>>> equalities,
          ArrayState state)
        {
          Contract.Assume(refinedMap != null);
          Contract.Assume(convert != null);

          var renamed = Select(state);
          renamed.AssignInParallel(refinedMap, convert);

          return renamed;
        }

        #endregion

        #region Transfer functions

        #region Assume

        public override ArrayState Assume(APC pc, string tag, Variable source, object provenance, ArrayState data)
        {
          Contract.Assume(data != null, "Assume for inheritance");

          #region Assume the existential, if any
          
          if (tag != "false")
          {
            var exist = this.MethodDriver.AsExistsIndexed(pc, source);
            if (exist != null)
            {
              var updated = AssumeExistsAll(pc, exist, data);

              if (updated.IsBottom)
              {
                return data.Bottom;
              }

              data = data.UpdatePluginAt(this.Id, updated);
            }
          }

          #endregion

          #region Look for contraddictions forall/exists
          var bottom = NonRelationalValueAbstraction<BoxedVariable<Variable>, BoxedExpression>.Unreached;

          foreach (var pair in Select(data).Elements)
          {
            if (pair.Value.IsNormal)
            {
              ArraySegmentation<NonRelationalValueAbstraction<BoxedVariable<Variable>, BoxedExpression>, BoxedVariable<Variable>, BoxedExpression> universal;
              if (data.Array.TryGetValue(pair.Key, out universal))
              {
                if (pair.Value.JoinAll(bottom).AreInContraddiction(universal.JoinAll(bottom)))
                {
                  // Found a contraddiction!!!
                  return data.Bottom;
                }
              }
            }
          }
          #endregion

          return data;
        }

        #region Handling of Assume Contract.Exists

        public ArraySegmentationEnvironment<NonRelationalValueAbstraction<BoxedVariable<Variable>, BoxedExpression>, BoxedVariable<Variable>, BoxedExpression>
          AssumeExistsAll(APC pc, ExistsIndexedExpression exists, ArrayState data)
        {
          Contract.Requires(exists != null);
          Contract.Requires(data != null);

          // F: those are readonly-fields to which we cannot attach postcondition
          Contract.Assume(exists.LowerBound != null);
          Contract.Assume(exists.UpperBound != null);

          var mySubState = Select(data);

          // Shortcut for Exists(false => ...)
          if (exists.LowerBound.Equals(exists.UpperBound))
          {
            return mySubState.Bottom
              as ArraySegmentationEnvironment<NonRelationalValueAbstraction<BoxedVariable<Variable>, BoxedExpression>, BoxedVariable<Variable>, BoxedExpression>;
          }

          NonRelationalValueAbstraction<BoxedVariable<Variable>, BoxedExpression> intv;
          BoxedExpression array;

          if (this.TryInferNonRelationalProperty(exists.BoundVariable, exists.Body, data.Numerical, out array, out intv))
          {
            var arrayVar = ((Variable)array.UnderlyingVariable);
            Variable arrayLen;
            if ((array.UnderlyingVariable is Variable)
              && this.Context.ValueContext.TryGetArrayLength(this.Context.MethodContext.CFG.Post(pc), arrayVar, out arrayLen))
            {
              Contract.Assume(arrayLen != null); // Clousot does not know arrayLen is a struct here

              ArraySegmentation<NonRelationalValueAbstraction<BoxedVariable<Variable>, BoxedExpression>, BoxedVariable<Variable>, BoxedExpression> 
                arraySegmentation;

              if (this.TryCreateArraySegment(exists.LowerBound, exists.UpperBound, arrayLen, intv, data.Numerical, out arraySegmentation))
              {
                ArraySegmentation<NonRelationalValueAbstraction<BoxedVariable<Variable>, BoxedExpression>, BoxedVariable<Variable>, BoxedExpression> prevVal;
                if (mySubState.TryGetValue(ToBoxedVariable(arrayVar), out prevVal))
                {
                  var meet = prevVal.Meet(arraySegmentation);
                  mySubState.Update(ToBoxedVariable(arrayVar), meet);
                }
                else
                {
                  mySubState.AddElement(ToBoxedVariable(arrayVar), arraySegmentation);
                }
              }
            }
          }

          return mySubState;
        }
        #endregion

        #endregion

        #region Assert

        public override ArrayState Assert(APC pc, string tag, Variable condition, object provenance, ArrayState data)
        {
          return this.Assume(pc, tag, condition, provenance, data);
        }

        #endregion

        #region Stelem

        public override ArrayState Stelem(APC pc, Type type, Variable array, Variable index, Variable value, ArrayState data)
        {
          var mySubState = Select(data);
          var bArray = new BoxedVariable<Variable>(array);
          if (mySubState.ContainsKey(bArray))
          {
            mySubState.RemoveElement(bArray);

            return data.UpdatePluginAt(this.Id, mySubState);
          }

          return data;
        }

        #endregion

        #region Ldelem

        [ContractVerification(false)]
        public override ArrayState Ldelem(APC pc, Type type, Variable dest, Variable array, Variable index, ArrayState data)
        {
          var mySubState = Select(data);
          var bArray = new BoxedVariable<Variable>(array);
          ArraySegmentation<NonRelationalValueAbstraction<BoxedVariable<Variable>, BoxedExpression>, BoxedVariable<Variable>, BoxedExpression>
            existentialSegmentation;

          var noConstraints = SetOfConstraints<BoxedVariable<Variable>>.Unknown;
          var newElement = new NonRelationalValueAbstraction<BoxedVariable<Variable>, BoxedExpression>(DisInterval.UnknownInterval, SymbolicExpressionTracker<BoxedVariable<Variable>, BoxedExpression>.Unknown,
            new SetOfConstraints<BoxedVariable<Variable>>(new BoxedVariable<Variable>(dest)),
            noConstraints, noConstraints, noConstraints, noConstraints);


          if (mySubState.TryGetValue(bArray, out existentialSegmentation))
          {
            if (existentialSegmentation.IsNormal())
            {
              var newSegmentation = new ArraySegmentation<NonRelationalValueAbstraction<BoxedVariable<Variable>, BoxedExpression>, BoxedVariable<Variable>, BoxedExpression>
              (new NonNullList<SegmentLimit<BoxedVariable<Variable>>>() { existentialSegmentation.Limits.AsIndexable()[0], existentialSegmentation.LastLimit },
              new NonNullList<NonRelationalValueAbstraction<BoxedVariable<Variable>, BoxedExpression>>() { newElement },
              existentialSegmentation.Elements.AsIndexable()[0].Bottom, this.ExpressionManager);

              var meet = existentialSegmentation.Meet(newSegmentation);

              mySubState.Update(bArray, meet);

              return data.UpdatePluginAt(this.Id, mySubState);
            }
          }
          else // materialize
          {
            existentialSegmentation = MaterializeArray(this.Context.MethodContext.CFG.Post(pc),
              mySubState, data.Numerical.CheckIfNonZero, array, newElement, newElement.Bottom);

            // Segmentation may fail
            if (existentialSegmentation != null)
            {
              mySubState.Update(bArray, existentialSegmentation);
             
              return data.UpdatePluginAt(this.Id, mySubState);
            }
          }

          return data;
        }
        
        #endregion

        #endregion

      }
    }
  }
}