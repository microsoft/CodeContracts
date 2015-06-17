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

#if INCLUDE_UNSAFE_ANALYSIS

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Research.AbstractDomains.Numerical;
using Microsoft.Research.AbstractDomains.Expressions;
using Microsoft.Research.AbstractDomains;
using System.Diagnostics;
using Microsoft.Research.DataStructures;

namespace Microsoft.Research.CodeAnalysis
{

  public static partial class AnalysisWrapper
  {
    public static partial class TypeBindings<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly, ExternalExpression, Variable>
      where Variable : IEquatable<Variable>
      where ExternalExpression : IEquatable<ExternalExpression>
      where Type : IEquatable<Type>
    {

      public class StripeForUnsafeCode : Stripe<BoxedExpression, IDecodeMetaData<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly>>
      {
        protected IExpressionContext<APC, Local, Parameter, Method, Field, Type, ExternalExpression, Variable> context;
        new BoxedExpressionDecoder decoder;

        #region Constructors

        public StripeForUnsafeCode(
          BoxedExpressionDecoder decoder,
          IExpressionEncoder<BoxedExpression> encoder,
          IExpressionContext<APC, Local, Parameter, Method, Field, Type, ExternalExpression, Variable> context,
          IDecodeMetaData<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly> mdDecoder)
          : base(decoder, mdDecoder, encoder)
        {
          this.context = context;
          this.decoder = decoder;
        }

        public StripeForUnsafeCode(StripeForUnsafeCode original, IExpressionContext<APC, Local, Parameter, Method, Field, Type, ExternalExpression, Variable> context)
          : base(original)
        {
          this.context = context;
          this.decoder = original.decoder;
        }

        public StripeForUnsafeCode(Stripe<BoxedExpression, IDecodeMetaData<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly>> original, IExpressionContext<APC, Local, Parameter, Method, Field, Type, ExternalExpression, Variable> context)
          : base(original)
        {
          this.context = context;
          this.decoder = (BoxedExpressionDecoder<Type, ExternalExpression>)original.decoder;
        }

        #endregion

        /// <summary>
        /// The version of the join operator refined with the information about null pointers
        /// </summary>
        /// <param name="pc">PC at join point</param>
        public IAbstractDomain Join(IAbstractDomain a, Set<BoxedExpression> bottomPointers, IntervalsForUnsafeCode intervalsThis, IntervalsForUnsafeCode intervalsPrev, APC pc)
        {
          StripeForUnsafeCode thiscloned=(StripeForUnsafeCode) this.Clone();
          StripeForUnsafeCode othercloned=(StripeForUnsafeCode) a.Clone();

          StripeForUnsafeCode joined = (StripeForUnsafeCode)((IAbstractDomain)this.Clone()).Join((IAbstractDomain)a.Clone());


          StripeForUnsafeCode prev = new StripeForUnsafeCode((Stripe<BoxedExpression, IDecodeMetaData<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly>>)a, this.context);
          StripeForUnsafeCode result = joined;

          //For each pointer such that in a branch it is null and in the other is assigned to an allocated area of memory, we trace information only from
          //this branch, ignoring the other one
          foreach (BoxedExpression ptr in bottomPointers)
          {
            BoxedExpression tempExp;
            if (ptr.TryGetAssociatedInfo(pc, AssociatedInfo.WritableBytes, out tempExp) ||
                ptr.TryGetAssociatedInfo(pc, AssociatedInfo.ArrayLength, out tempExp))
              // TODO: if this doesn't work, we need to pass in the PC to the getassociated info method to refine it at a particular place
            {
              Stripe<BoxedExpression, IDecodeMetaData<Local,Parameter,Method,Field,Property,Type,Attribute,Assembly>> thisConstr = this.GetConstraintsOfAVariable(tempExp);
              Stripe<BoxedExpression, IDecodeMetaData<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly>> prevConstr = prev.GetConstraintsOfAVariable(tempExp);
              if (thisConstr.IsTop && !prevConstr.IsTop)
                result = new StripeForUnsafeCode((Stripe<BoxedExpression, IDecodeMetaData<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly>>)result.Meet(prevConstr), this.context);
              if (prevConstr.IsTop && !thisConstr.IsTop)
                result = new StripeForUnsafeCode((Stripe<BoxedExpression, IDecodeMetaData<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly>>)result.Meet(thisConstr), this.context);
            }
          }

          //It checks for each dropped constraints if it can be validated by the intervals domain state
          addConstraints(intervalsPrev, thiscloned, result);
          addConstraints(intervalsThis, othercloned, result);

          return result;
        }

        /// <summary>
        /// It checks for each dropped constraints if it can be validated by the intervals domain state
        /// </summary>
        private void addConstraints(IntervalsForUnsafeCode intervalsPrev, StripeForUnsafeCode thiscloned, StripeForUnsafeCode result)
        {
          StripeForUnsafeCode dropped = (StripeForUnsafeCode)this.Factory();
          ExtractDroppedConstraints(thiscloned, result, dropped);

          foreach (BoxedExpression key in dropped.Keys)
            foreach (AtMostTwoExpressions<BoxedExpression> constr in dropped[key].EmbeddedValues_Unsafe)
            {
              if (!constr.IsTop && !constr.IsBottom)
              {
                BoxedExpression toBeChecked = StripeWithIntervalsForUnsafeCode.MakeCondition(key, constr, this.mdDecoder);
                FlatAbstractDomain<bool> test = intervalsPrev.CheckIfHolds(toBeChecked);
                if (!test.IsBottom && !test.IsTop && test.BoxedElement == true)
                  result.AddConstraint(key, constr);
              }
            }
        }

        /// <summary>
        /// Given the previous state of the domain and the result of a join operations, add to the third parameter all the constraints that have been dropped
        /// during the join
        /// </summary>
        /// <param name="thiscloned">The old state</param>
        /// <param name="result">The result of the join</param>
        /// <param name="dropped">The variable on which the constraints must be added</param>
        private static void ExtractDroppedConstraints(StripeForUnsafeCode thiscloned, StripeForUnsafeCode result, StripeForUnsafeCode dropped)
        {
          foreach (BoxedExpression key in thiscloned.Keys)
            foreach (AtMostTwoExpressions<BoxedExpression> constr in thiscloned[key].EmbeddedValues_Unsafe)
            {
              if (result.ContainsKey(key) == false || result[key].Contains(constr) == false)
                dropped.AddConstraint(key, constr);
            }
        }

        protected override Stripe<BoxedExpression, IDecodeMetaData<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly>> Factory()
        {
          return new StripeForUnsafeCode(this.decoder, this.encoder, this.context, this.mdDecoder);
        }

        public override object Clone()
        {
          StripeForUnsafeCode result = new StripeForUnsafeCode((Stripe<BoxedExpression, IDecodeMetaData<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly>>)base.Clone(), this.context);
          return result;
        }

        public override string ToString()
        {
          return base.ToString();
        }
      }

    }
  }
}
#endif