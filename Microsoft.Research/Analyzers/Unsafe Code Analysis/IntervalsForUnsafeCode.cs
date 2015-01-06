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

      /// <summary>
      /// This class improved the precision of the base class in order to manage some special cases of the unsafe code analysis
      /// </summary>
      public class IntervalsForUnsafeCode 
        : IntervalEnvironment<BoxedExpression>
      {
        //It traces the varibles whose value has been modified during a TestTrue or TestFalse
        //We need also to override the indexer in order to trace it
        private Set<BoxedExpression> newvalues = new Set<BoxedExpression>();
        public Set<BoxedExpression> NewValues
        {
          get
          {
            return newvalues;
          }
          set
          {
            newvalues = value;
          }
        }
        public override Interval this[BoxedExpression index]
        {
          set
          {
            if (value.IsBottom)
            {
              base.State = AbstractState.Bottom;
            }
            if (!value.IsTop)
              newvalues.Add(index);
            base[index] = value;
          }

          get
          {
            return base[index];
          }
        }

         override public IntervalEnvironment<BoxedExpression>/*!*/ TestTrue(BoxedExpression/*!*/ guard)
        {
          newvalues = new Set<BoxedExpression>();

          return base.TestTrue(guard);
         }

         override public IntervalEnvironment<BoxedExpression>/*!*/ TestFalse(BoxedExpression/*!*/ guard)
         {
           newvalues = new Set<BoxedExpression>();

           return base.TestFalse(guard);
         }
        
        protected IExpressionContext<APC, Local, Parameter, Method, Field, Type, ExternalExpression, Variable> context;

        BoxedExpressionDecoder decoder;

        public IntervalsForUnsafeCode(BoxedExpressionDecoder decoder, IExpressionEncoder<BoxedExpression> encoder, IExpressionContext<APC, Local, Parameter, Method, Field, Type, ExternalExpression, Variable> context)
          : base(decoder, encoder)
        {
          this.decoder = decoder;
          this.context = context;
        }

        public IntervalsForUnsafeCode(IntervalsForUnsafeCode original, IExpressionContext<APC, Local, Parameter, Method, Field, Type, ExternalExpression, Variable> context)
          : base(original)
        {
          this.context = context;
          this.decoder = original.decoder;
        }

        public IntervalsForUnsafeCode(IntervalEnvironment<BoxedExpression> original, IExpressionContext<APC, Local, Parameter, Method, Field, Type, ExternalExpression, Variable> context)
          : base(original)
        {
          this.context = context;
          this.decoder = (BoxedExpressionDecoder<Type,ExternalExpression>)original.Decoder;
        }

        public override void AssignInParallel(Dictionary<BoxedExpression, FList<BoxedExpression>>/*!*/ sourcesToTargets)
        {
          base.AssignInParallel(sourcesToTargets);
        }

        /// <summary>
        /// The join operator improved in order to keep information about pointers that are null in a branch and != null in the other
        /// In this case we trace the length of the pointer != null ignoring the other branch
        /// </summary>
        /// <param name="pc">is the target of the join.</param>
        public IAbstractDomain Join(IAbstractDomain a, Set<BoxedExpression> bottomPointers, APC pc)
        {
          var joined = base.Join((IntervalEnvironment<BoxedExpression>) a);
          
          IntervalsForUnsafeCode prev = new IntervalsForUnsafeCode((IntervalEnvironment<BoxedExpression>) a, this.context);
          IntervalsForUnsafeCode result = new IntervalsForUnsafeCode((IntervalEnvironment<BoxedExpression>)joined, this.context);

          //It traces all the pointers that in a branch are assigned to null and in the other are assigned to an allocated area of memory
          //This information is used in order to refine the Join, as otherwise we would lose the size of the allocated memory
          foreach (BoxedExpression ptr in bottomPointers)
          {
            BoxedExpression len;
            // TODO: if this doesn't work, we need to pass in the pc to GetAssociatedInfo !
            if (ptr.TryGetAssociatedInfo(pc, AssociatedInfo.WritableBytes, out len))
            {
              RefineNullVariable(pc, len, prev, result);
            }
          }
          return result;
        }

        /// <summary>
        /// TODO: figure out the subtle uses of PC that Pietro introduced.
        /// </summary>
        private void RefineNullVariable(APC pc, BoxedExpression length, IntervalsForUnsafeCode prev, IntervalsForUnsafeCode result)
        {
          if (this.ContainsKey(length) && !this[length].IsTop)
          {
            if (result.ContainsKey(length) == false)
            {
              // result.elements.Add(length, this[length]);
              result[length] = this[length];
            }
          }
          if (prev.ContainsKey(length) && !prev[length].IsTop)
          {
            if (result.ContainsKey(length))
            {
             // result.Add(tempExp, prev[tempExp]);
              result[length] = prev[length];
            }
          }
        }

        protected override IntervalEnvironment<BoxedExpression> Factory()
        {
          return new IntervalsForUnsafeCode((BoxedExpressionDecoder<Type,ExternalExpression>) decoder, this.Encoder, context);
        }

        protected override IntervalEnvironment<BoxedExpression> DuplicateMe()
        {
          return new IntervalsForUnsafeCode(base.DuplicateMe(), this.context);
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