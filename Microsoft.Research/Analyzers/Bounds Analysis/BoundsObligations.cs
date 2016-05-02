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
using Microsoft.Research.AbstractDomains;
using System.Diagnostics.Contracts;

namespace Microsoft.Research.CodeAnalysis
{
  public static partial class AnalysisWrapper
  {
    public static partial class TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>
      where Variable : IEquatable<Variable>
      where Expression : IEquatable<Expression>
      where Type : IEquatable<Type>
    {

      internal class BoundsObligations
        : ProofObligations<Local, Parameter, Method, Field, Property, Event, Type, Variable, Attribute, Assembly, BoxedExpression, ProofObligationBase<BoxedExpression, Variable>>
      {
        BoxedExpressionDecoder<Type, Variable, Expression> expressionDecoder;
        readonly IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> mdriver;
        readonly bool noObl;

        new IExpressionContext<Local, Parameter, Method, Field, Type, Expression, Variable> Context
        {
          get
          {
            return this.mdriver.Context;
          }
        }

        IExpressionContextData<Local, Parameter, Method, Field, Type, Expression, Variable> ContextData
        {
          get
          {
            return this.mdriver.Context.ExpressionContext;
          }
        }

        BoxedExpressionDecoder<Type, Variable, Expression> DecoderForExpressions
        {
          get
          {
            if (this.expressionDecoder == null)
            {
              this.expressionDecoder = BoxedExpressionDecoder<Variable>.Decoder(new ValueExpDecoder(Context, this.MetaDataDecoder));
            }
            return this.expressionDecoder;
          }
        }

        public BoundsObligations(
          IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> mdriver,
          bool noObligations
          )
        {
          this.mdriver = mdriver;
          this.noObl = noObligations;

          if (!this.noObl)
          {
            this.Run(mdriver.ValueLayer, null);
          }
        }

        public override string Name
        {
          get { return "Bounds"; }
        }

        private void HandleAllocations(APC pc, Variable dest, Variable len, int dim, bool isMultidimensional)
        {
          if (!this.IgnoreProofObligationAtPC(pc))
          {
            var lenAsExp = BoxedExpression.Convert(this.ContextData.Refine(pc, len), this.DecoderForExpressions.Outdecoder);

            if (lenAsExp != null)
            {
              this.Add(new ArrayCreation(lenAsExp, dim, isMultidimensional, this.DecoderForExpressions, pc, this.mdriver));
            }
          }
        }

        private void HandleArrayAccesses(string name, APC pc, Type type, Variable array, Variable index)
        {
          if (!IgnoreProofObligationAtPC(pc))
          {
            Variable arrayLength;
            if (this.Context.ValueContext.TryGetArrayLength(pc, array, out arrayLength))
            {
              var expDecoder = this.DecoderForExpressions;
              // We refine the variables to expressions
              var expForIndex = BoxedExpression.Convert(this.ContextData.Refine(pc, index), expDecoder.Outdecoder);

              if (expForIndex != null)
              {
                this.Add(new ArrayLowerBoundAccess(expForIndex, expDecoder, pc, this.mdriver));

                var expForArrayLength = BoxedExpression.Convert(this.ContextData.Refine(pc, arrayLength), expDecoder.Outdecoder);

                if (expForArrayLength != null)
                {
                  this.Add(new ArrayUpperBoundAccess(expForIndex, expForArrayLength, expDecoder, pc, this.mdriver));
                }
              }
            }
          }
        }

        public override bool Ldelem(APC pc, Type type, Variable dest, Variable array, Variable index, bool data)
        {
          HandleArrayAccesses("Ldelem", pc, type, array, index);
          return data;
        }

        public override bool Ldelema(APC pc, Type type, bool @readonly, Variable dest, Variable array, Variable index, bool data)
        {
          HandleArrayAccesses("Ldelema", pc, type, array, index);
          return data;
        }

        public override bool Stelem(APC pc, Type type, Variable array, Variable index, Variable value, bool data)
        {
          HandleArrayAccesses("Stelem", pc, type, array, index);
          return data;
        }

        public override bool Newarray<ArgList>(APC pc, Type type, Variable dest, ArgList lengths, bool data)
        {
          for (int i = 0; i < lengths.Count; i++)
          {
            HandleAllocations(pc, dest, lengths[i], i, 1 < lengths.Count);
          }
          return data;
        }
      }

      [ContractVerification(true)]
      internal class ArrayCreation : ProofObligationWithDecoder
      {
        #region Messages

        static private readonly string[] fixedMessages = {
            "The length of the array may be negative",
            "This array creation is never executed",
            "Array creation : ok",
            "Cannot create an array of negative length"
          };

        #endregion

        #region Invariant

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
          Contract.Invariant(size != null);
          Contract.Invariant(fixedMessages != null);
          Contract.Invariant(Contract.ForAll(fixedMessages, msg => msg != null));
        }

        #endregion

        #region State

        readonly BoxedExpression size;
        readonly int dimension;
        readonly bool isMultidimensional;

        #endregion

        #region Constructor
        public ArrayCreation(BoxedExpression size, int dimension, bool isMultidimensional, BoxedExpressionDecoder<Variable> decoder, APC pc,
          IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> mdriver
        )
          : base(pc, decoder, mdriver, null)
        {
          Contract.Requires(size != null);
          Contract.Requires(mdriver != null);

          this.size = size;
          this.dimension = dimension;
          this.isMultidimensional = isMultidimensional;
        }
        #endregion

        #region Overridden

        public override BoxedExpression Condition
        {
          get
          {
            return   
              BoxedExpression.Binary(BinaryOperator.Cle, 
              BoxedExpression.Const(0, this.DecoderForMetaData.System_Int32, this.DecoderForMetaData), 
              this.size);
          }
        }

        protected override ProofOutcome ValidateInternalSpecific(IFactQuery<BoxedExpression, Variable> query, ContractInferenceManager inferenceManager, IOutputResults output)
        {
          return query.IsGreaterEqualToZero(this.PC, this.size);
        }

        public override void EmitOutcome(ProofOutcome outcome, IOutputResults output)
        {
          var dim = isMultidimensional ? string.Format(" (dimension {0})", dimension) : string.Empty;
          output.EmitOutcome(GetWitness(outcome), AddHintsForTheUser(outcome, "{0}{1}"), fixedMessages[(int)outcome], dim);
        }

        protected override void PopulateWarningContextInternal(ProofOutcome outcome)
        {
          if (outcome == ProofOutcome.Top)
          {
            this.AdditionalInformationOnTheWarning.AddRange(WarningContextFetcher.InferContext(this.PC, this.size, this.Context, this.DecoderForMetaData.IsBoolean));
          }
        }

        public override Witness GetWitness(ProofOutcome outcome)
        {
          this.PopulateWarningContext(outcome);

          return new Witness(this.ID, WarningType.ArrayCreation, outcome, this.PC, this.AdditionalInformationOnTheWarning);
        }

        public override string ObligationName
        {
          get { return "ArrayCreation"; }
        }


        #endregion
      }

      [ContractVerification(true)]
      internal class ArrayLowerBoundAccess 
        : ProofObligationWithDecoder
      {
        #region Object invariant

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
          Contract.Invariant(this.index != null);
          Contract.Invariant(Contract.ForAll(fixedMessages, el => el != null));
        }

        #endregion 

        #region Statics
        private static readonly string[] fixedMessages = {
            "Array access might be below the lower bound",
            "This array access is unreached",
            "Lower bound access ok",
            "Array access IS below the lower bound"
          };
        #endregion

        #region Private state

        readonly BoxedExpression index;

        #endregion

        #region Constructor

        public ArrayLowerBoundAccess(BoxedExpression index, BoxedExpressionDecoder<Variable> decoder, APC pc,
          IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> mdriver
        )
          : base(pc, decoder, mdriver, null)
        {
          Contract.Requires(index != null);
          Contract.Requires(mdriver != null);

          this.index = index;
        }

        #endregion

        #region Overridden

        public override BoxedExpression Condition
        {
          get
          {
            return BoxedExpression.Binary(BinaryOperator.Cle, BoxedExpression.Const(0, this.DecoderForMetaData.System_Int32, this.DecoderForMetaData), this.index);
          }
        }

        protected override ProofOutcome ValidateInternalSpecific(IFactQuery<BoxedExpression, Variable> query, ContractInferenceManager inferenceManager, IOutputResults output)
        {
          // Check if expForIndex >= 0
        return query.IsGreaterEqualToZero(this.PC, index);
#if false
          // Try to use weakest preconditions
          if (result == ProofOutcome.Top)
          {


            if (CanAssumeLowerBoundPrecondition(output, this.index))
            {
              result = ProofOutcome.True;
            }
            else
            {
              // if we have lb <= i, then we can suggest 0 <= lb
              //
              foreach (Variable lb in query.LowerBounds(this.PC, index, false))
              {
                BoxedExpression lbbox = BoxedExpression.Var(lb);
                if (CanAssumeLowerBoundPrecondition(output, lbbox))
                {
                  result = ProofOutcome.True;
                }
              }
            }
          }

          return result;

#endif
        }

        //This code should be moved in the inference helper
#if false
        
        private bool CanAssumeLowerBoundPrecondition(IOutputResults output, BoxedExpression index)
        {
          bool hasVariables, hasAccessPath;
          BoxedExpression indexInPreState = PreconditionSuggestion.ExpressionInPreState(index, this.Context, this.DecoderForMetaData, out hasVariables, out hasAccessPath, this.PC);
          if (indexInPreState == null || !hasVariables) return false;

          this.warningContext.Add(new WarningContext(WarningContext.ContextType.PreconditionCanDischarge));

          BoxedExpression suggestedPre = BoxedExpression.Binary(
            BinaryOperator.Cle, BoxedExpression.Const(0, this.DecoderForMetaData.System_Int32, this.DecoderForMetaData), indexInPreState);

          return InferenceHelper.TryAssumePreCondition(this.PC, suggestedPre, MethodDriver, SuggestedCodeFixes.WARNING_ARRAY_BOUND, "array lower bound", output);
        }
#endif

        public override void EmitOutcome(ProofOutcome outcome, IOutputResults output)
        {
          output.EmitOutcome(GetWitness(outcome), AddHintsForTheUser(outcome, "{0}"), fixedMessages[(int)outcome]);
        }

        public override Witness GetWitness(ProofOutcome outcome)
        {
          this.PopulateWarningContext(outcome);

          return new Witness(this.ID, WarningType.ArrayLowerBound, outcome, this.PC, this.AdditionalInformationOnTheWarning);
        }

        protected override void PopulateWarningContextInternal(ProofOutcome outcome)
        {
          if (outcome == ProofOutcome.Top)
          {
            this.AdditionalInformationOnTheWarning.AddRange(WarningContextFetcher.InferContext(this.PC, this.index, this.Context, this.DecoderForMetaData.IsBoolean));
          }
        }

        public override string ObligationName
        {
          get { return "ArrayLowerBoundAccess"; }
        }
        #endregion
      }


      [ContractVerification(true)]
      internal class ArrayUpperBoundAccess 
        : ProofObligationWithDecoder
      {

        [ContractInvariantMethod]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Required for code contracts.")]
        private void ObjectInvariant()
        {
          Contract.Invariant(this.left != null);
          Contract.Invariant(this.right != null);
          Contract.Invariant(fixedMessages.Length == 4);
          Contract.Invariant(Contract.ForAll(fixedMessages, msg => msg != null));
        }

        #region Statics
        public static readonly string[] fixedMessages = {
            "Array access might be above the upper bound",
            "This array access is unreached",
            "Upper bound access ok",
            "Array access IS above the upper bound"
          };
        #endregion

        #region State
        
        BoxedExpression left, right;
        
        #endregion

        #region Constructor
        public ArrayUpperBoundAccess(BoxedExpression left, BoxedExpression right, BoxedExpressionDecoder<Variable> decoder, APC pc,
                      IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> mdriver
        )
          : base(pc, decoder, mdriver, null)
        {
          Contract.Requires(left != null);
          Contract.Requires(right != null);
          Contract.Requires(mdriver != null);

          this.left = left;
          this.right = right;

          Contract.Assume(fixedMessages.Length == 4, "assuming the invariant on the static field");
        }
        #endregion

        #region Overridden

        public override BoxedExpression Condition
        {
          get { return BoxedExpression.Binary(BinaryOperator.Clt, this.left, this.right); }
        }

        protected override ProofOutcome ValidateInternalSpecific(IFactQuery<BoxedExpression, Variable> query, ContractInferenceManager inferenceManager, IOutputResults output)
        {
          var outcome = query.IsLessThan(this.PC, this.left, this.right);

          switch (outcome)
          {
            case ProofOutcome.Top:
              {
                // Let us see if there is a possible off-by-one
                var offByOne = query.IsTrue(this.PC, BoxedExpression.Binary(BinaryOperator.Cle, this.left, this.right));
                if (offByOne == ProofOutcome.True)
                {
                  this.AdditionalInformationOnTheWarning.Add(new WarningContext(WarningContext.ContextType.OffByOne));
                }

                return outcome;
              }

            case ProofOutcome.Bottom:
            case ProofOutcome.False:
            case ProofOutcome.True:
              {
                return outcome;
              }
            default:
              {
                Contract.Assert(false);
                return outcome; // unreached
              }
          }
        }

        public override void EmitOutcome(ProofOutcome outcome, IOutputResults output)
        {
          output.EmitOutcome(GetWitness(outcome), AddHintsForTheUser(outcome, "{0}"), fixedMessages[(int)outcome]);
        }

        public override Witness GetWitness(ProofOutcome outcome)
        {
          this.PopulateWarningContext(outcome);

          return new Witness(this.ID, WarningType.ArrayUpperBound, outcome, this.PC, this.AdditionalInformationOnTheWarning);
        }

        protected override void PopulateWarningContextInternal(ProofOutcome outcome)
        {
          if (outcome == ProofOutcome.Top)
          {
            this.AdditionalInformationOnTheWarning.AddRange(WarningContextFetcher.InferContext(this.PC, this.left, this.Context, this.DecoderForMetaData.IsBoolean));
            this.AdditionalInformationOnTheWarning.AddRange(WarningContextFetcher.InferContext(this.PC, this.right, this.Context, this.DecoderForMetaData.IsBoolean));
          }
        }

        public override string ObligationName
        {
          get { return "ArrayUpperBoundAccess"; }
        }
      #endregion
      }
    }
  }
}
