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
      internal class ArithmeticObligations
        : ProofObligations<Local, Parameter, Method, Field, Property, Event, Type, Variable, Attribute, Assembly, BoxedExpression, ProofObligationBase<BoxedExpression, Variable>>
      {
        BoxedExpressionDecoder<Type, Variable, Expression> expressionDecoder;

        new IExpressionContext<Local, Parameter, Method, Field, Type, Expression, Variable> Context
        {
          get
          {
            return this.MethodDriver.Context;
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

        IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> MethodDriver
        {
          get;
          set;
        }

        public readonly Analyzers.Arithmetic.ArithmeticOptions myOptions;

        public ArithmeticObligations
        (
          IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> mdriver,
          Analyzers.Arithmetic.ArithmeticOptions myOptions
        )
        {
          this.MethodDriver = mdriver;
          this.myOptions = myOptions;
          this.Run(this.MethodDriver.ValueLayer);
        }

        public override string Name
        {
          get { return "Arithmetic"; }
        }

        public override bool Binary(APC pc, BinaryOperator op, Variable dest, Variable s1, Variable s2, bool data)
        {
          if (this.IgnoreProofObligationAtPC(pc))
          {
            return data;
          }

          var mdDecoder = this.MetaDataDecoder;
          var valueContext = this.Context.ValueContext;
          var methodContext = this.Context.MethodContext;
          var expressionContext = this.Context.ExpressionContext;
          var expressionDecoder = this.DecoderForExpressions;

          if (this.myOptions.Div0Obligations)
          {
            var type = valueContext.GetType(methodContext.CFG.Post(pc), s1);
            if (type.IsNormal && mdDecoder.IsIntegerType(type.Value))
            {
              if (op == BinaryOperator.Div || op == BinaryOperator.Div_Un || op == BinaryOperator.Rem || op == BinaryOperator.Rem_Un)
              {
                var s2AsBoxedExpression = BoxedExpression.Convert(expressionContext.Refine(pc, s2), expressionDecoder.Outdecoder);
                if (s2AsBoxedExpression != null)
                {
                  this.Add(new DivisionByZeroObligation(s2AsBoxedExpression, expressionDecoder, pc, this.MethodDriver));
                }
              }
            }
          }
          
          if (this.myOptions.DivOverflowObligations
              && (op == BinaryOperator.Div || op == BinaryOperator.Rem))
          {
            var type = valueContext.GetType(methodContext.CFG.Post(pc), s1);
            if (type.IsNormal && mdDecoder.IsIntegerType(type.Value))
            {
              var op1 = BoxedExpression.Convert(expressionContext.Refine(pc, s1), expressionDecoder.Outdecoder);
              var op2 = BoxedExpression.Convert(expressionContext.Refine(pc, s2), expressionDecoder.Outdecoder);
              if (op1 != null && op2 != null)
              {
                this.Add(new DivisionOverflowObligation(op1, op2, type.Value, expressionDecoder, pc, this.MethodDriver));
              }
            }
          }

          var emitProofObligationsForInts = this.myOptions.ArithmeticOverflow && op.IsOverflowChecked();
          var emitProofObligationsForFloats = this.myOptions.FloatingPointOverflow; 
          
          if (emitProofObligationsForInts || emitProofObligationsForFloats)
          {
            var type = valueContext.GetType(methodContext.CFG.Post(pc), dest);
            if (type.IsNormal)
            {
              var left = BoxedExpression.Convert(expressionContext.Refine(pc, s1), expressionDecoder.Outdecoder);
              var right = BoxedExpression.Convert(expressionContext.Refine(pc, s2), expressionDecoder.Outdecoder);

              if (left != null && right != null)
              {
                var exp = BoxedExpression.Binary(op, left, right);

                // According to the type we generate different proof obligations
                if (emitProofObligationsForInts && (mdDecoder.IsIntegerType(type.Value) || mdDecoder.IsUnsignedIntegerType(type.Value)))
                {
                  this.Add(new ArithmeticUnderflow(exp, type.Value, pc, expressionDecoder, this.MethodDriver));
                  this.Add(new ArithmeticOverflow(exp, type.Value, pc, expressionDecoder, this.MethodDriver));
                }
                else if (emitProofObligationsForFloats && !op.IsComparisonBinaryOperator() && mdDecoder.IsFloatType(type.Value))
                {
                  this.Add(new ArithmeticUnderflowForFloats(exp, type.Value, pc, expressionDecoder, this.MethodDriver));
                  this.Add(new ArithmeticOverflowForFloats(exp, type.Value, pc, expressionDecoder, this.MethodDriver));
                }
              }
            }
          }

          if (this.myOptions.FloatingPointIsNaN)
          {
            var type = valueContext.GetType(methodContext.CFG.Post(pc), dest);
            if (IsFloat(type))
            {
            }
          }

          if (op == BinaryOperator.Ceq && this.myOptions.FloatEqualityObligations)
          {
            var t1 = valueContext.GetType(pc, s1);
            var t2 = valueContext.GetType(pc, s2);

            if (!t1.IsBottom && !t2.IsBottom && (IsFloat(t1) || IsFloat(t2)))
            {
              var s1Exp = BoxedExpression.Convert(expressionContext.Refine(pc, s1), expressionDecoder.Outdecoder);
              var s2Exp = BoxedExpression.Convert(expressionContext.Refine(pc, s2), expressionDecoder.Outdecoder);
              if (s1Exp != null && s2Exp != null)
              {
                this.Add(new FloatEqualityObligation(pc, dest, s1Exp, s2Exp, expressionDecoder, this.MethodDriver));
              }
            }
          }

          return data;
        }

        public override bool Newarray<ArgList>(APC pc, Type type, Variable dest, ArgList lengths, bool data)
        {
          if (this.IgnoreProofObligationAtPC(pc))
          {
            return data;
          }

          if (this.myOptions.ArithmeticOverflow && lengths.Count == 1)
          {
            var condition = BoxedExpression.Convert(this.Context.ExpressionContext.Refine(pc, lengths[0]), this.DecoderForExpressions.Outdecoder);
            if (condition != null)
            {
              this.Add(new NewArrayArithmeticOverflow(condition, pc, this.DecoderForExpressions, this.MethodDriver));
            }
          }

          return data;
        }

        public override bool Unary(APC pc, UnaryOperator op, bool overflow, bool unsigned, Variable dest, Variable source, bool data)
        {
          if (this.IgnoreProofObligationAtPC(pc))
          {
            return data;
          }

          var mdDecoder = this.MetaDataDecoder;

          if (this.myOptions.NegMinObligations && op == UnaryOperator.Neg)
          {
            var type = this.Context.ValueContext.GetType(pc, source);
            if (type.IsNormal &&
                (mdDecoder.Equal(type.Value, mdDecoder.System_Int32) ||
                 mdDecoder.Equal(type.Value, mdDecoder.System_Int64) ||
                 mdDecoder.Equal(type.Value, mdDecoder.System_Int16) ||
                 mdDecoder.Equal(type.Value, mdDecoder.System_Int8)
                ))
            {
              var argAsBoxedExpression = BoxedExpression.Convert(this.Context.ExpressionContext.Refine(pc, source), this.DecoderForExpressions.Outdecoder);
              if (argAsBoxedExpression != null)
              {
                this.Add(new NoMinValueObligation(type.Value, mdDecoder.Name(type.Value), argAsBoxedExpression, this.DecoderForExpressions, pc, this.MethodDriver));
              }
            }
          }

          if (overflow && this.myOptions.ArithmeticOverflow && op.IsConversionOperator())
          {
            // this.Context.ValueContext.GetType fails sometimes. This is why we infer the type from the operator
            Type type;
            if (mdDecoder.TryInferTypeForOperator(op, out type))
            {
              var exp = BoxedExpression.Convert(this.Context.ExpressionContext.Refine(pc, source), this.DecoderForExpressions.Outdecoder);

              if (exp != null)
              {
                this.Add(new ArithmeticUnderflow(exp, type, pc, this.DecoderForExpressions, this.MethodDriver));
                this.Add(new ArithmeticOverflow(exp, type, pc, this.DecoderForExpressions, this.MethodDriver));
              }
            }
          }

          return data;
        }

        private bool IsFloat(FlatDomain<Type> t)
        {
          var mdDecoder = this.MetaDataDecoder;
          return t.IsNormal && (t.Value.Equals(mdDecoder.System_Single) || t.Value.Equals(mdDecoder.System_Double));
        }
      }

      #region Proof obligations

      class DivisionByZeroObligation
        : ProofObligationWithDecoder
      {
        #region Statics
        public static readonly string[] fixedMessages = 
          {
            "Possible division by zero",
            "This arithmetic operation is unreached",
            "Division by zero ok",
            "Division by zero detected"
          };
        #endregion

        #region Object invariant

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
          Contract.Invariant(this.denominator != null);
        }

        #endregion

        #region State

        readonly BoxedExpression denominator;

        #endregion

        #region Constructor
        public DivisionByZeroObligation
        (
          BoxedExpression denominator, BoxedExpressionDecoder<Variable> decoder, APC pc,
          IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> mdriver
        )
          : base(pc, decoder, mdriver, null)
        {
          Contract.Requires(denominator != null);

          this.denominator = denominator;
        }

        #endregion

        #region Overridden

        public override BoxedExpression Condition
        {
          get
          {
            return
            BoxedExpression.Binary(BinaryOperator.Cne_Un, 
            this.denominator, 
            BoxedExpression.Const(0, this.DecoderForMetaData.System_Int32, this.DecoderForMetaData));
          }
        }

        protected override void PopulateWarningContextInternal(ProofOutcome outcome)
        {
          if (outcome == ProofOutcome.Top)
          {
            this.AdditionalInformationOnTheWarning.AddRange(WarningContextFetcher.InferContext(this.PC, this.denominator, this.Context, this.DecoderForMetaData.IsBoolean));
          }
        }

        public override void EmitOutcome(ProofOutcome outcome, IOutputResults output)
        {
          output.EmitOutcome(GetWitness(outcome), this.AddHintsForTheUser(outcome, "{0}"), fixedMessages[(int)outcome]);
        }

        public override Witness GetWitness(ProofOutcome outcome)
        {
          this.PopulateWarningContext(outcome);

          return new Witness(this.ID, WarningType.ArithmeticDivisionByZero, outcome, this.PC, this.AdditionalInformationOnTheWarning);
        }

        protected override ProofOutcome ValidateInternalSpecific(IFactQuery<BoxedExpression, Variable> query, ContractInferenceManager inferenceManager, IOutputResults output)
        {
          return query.IsNonZero(this.PC, this.denominator);
        }

        public override string ObligationName
        {
          get { return "DivisionByZeroObligation"; }
        }

        #endregion

      }

      class DivisionOverflowObligation
        : ProofObligationWithDecoder
      {
        #region Statics
        public static readonly string[] fixedMessages = 
          {
            "Possible overflow in division (MinValue / -1)",
            "This arithmetic operation is unreached",
            "No overflow",
            "Overflow in division"
          };
        #endregion

        #region Invariant
        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
          Contract.Invariant(this.Op1 != null);
          Contract.Invariant(this.Op2 != null);
        }
        #endregion

        #region State
        readonly private BoxedExpression Op1;
        readonly private BoxedExpression Op2;
        readonly private Type TypeOp1;
        #endregion

        #region Constructor
        public DivisionOverflowObligation(BoxedExpression op1, BoxedExpression op2,
          Type typeOp1,
          BoxedExpressionDecoder<Variable> decoder, APC pc,
          IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> mdriver)
          : base(pc, decoder, mdriver, null)
        {
          Contract.Requires(op1 != null);
          Contract.Requires(op2 != null);

          this.Op1 = op1;
          this.Op2 = op2;
          this.TypeOp1 = typeOp1;
        }
        #endregion

        #region Overridden

        public override BoxedExpression Condition
        {
          get
          {
            object minValue;
            if (!this.DecoderForMetaData.TryGetMinValueForType(this.TypeOp1, out minValue))
            {
              return null;
            }
            var condition1 = BoxedExpression.Binary(BinaryOperator.Cne_Un, this.Op1, BoxedExpression.Const(minValue, this.TypeOp1, this.DecoderForMetaData));
            var condition2 = BoxedExpression.Binary(BinaryOperator.Cne_Un, this.Op2, BoxedExpression.Const(-1, this.DecoderForMetaData.System_Int32, this.DecoderForMetaData));

            return BoxedExpression.Binary(BinaryOperator.LogicalOr, condition1, condition2);
          }
        }

        protected override ProofOutcome ValidateInternalSpecific(IFactQuery<BoxedExpression, Variable> query, ContractInferenceManager inferenceManager, IOutputResults output)
        {
          object minValue;
          if (!this.DecoderForMetaData.TryGetMinValueForType(this.TypeOp1, out minValue))
          {
            return ProofOutcome.Top;
          }

          // first, we check that Op1 != MinValue
          var condition1 = BoxedExpression.Binary(BinaryOperator.Cne_Un, this.Op1, BoxedExpression.Const(minValue, this.TypeOp1, this.DecoderForMetaData));
          var resultOp1 = query.IsTrue(this.PC, condition1);

          // second, we check Op2 != -1
          var condition2 = BoxedExpression.Binary(BinaryOperator.Cne_Un, this.Op2, BoxedExpression.Const(-1, this.DecoderForMetaData.System_Int32, this.DecoderForMetaData));
          var resultOp2 = query.IsTrue(this.PC, condition2);

          // One of the two conditions is true, so it is ok!
          if (resultOp1 == ProofOutcome.True || resultOp2 == ProofOutcome.True)
          {
            return ProofOutcome.True;
          }

          // Both conditions are false, so division is definitely an overflow
          if (resultOp1 == ProofOutcome.False && resultOp2 == ProofOutcome.False)
          {
            return ProofOutcome.False;
          }

          return ProofOutcome.Top;
        }

        protected override void PopulateWarningContextInternal(ProofOutcome outcome)
        {
          if (outcome == ProofOutcome.Top)
          {
            this.AdditionalInformationOnTheWarning.AddRange(WarningContextFetcher.InferContext(this.PC, this.Op1, this.Context, this.DecoderForMetaData.IsBoolean));
          }
        }

        public override void EmitOutcome(ProofOutcome outcome, IOutputResults output)
        {
          output.EmitOutcome(GetWitness(outcome), AddHintsForTheUser(outcome, "{0}"), fixedMessages[(int)outcome]);
        }

        public override Witness GetWitness(ProofOutcome outcome)
        {
          this.PopulateWarningContext(outcome);

          return new Witness(this.ID, WarningType.ArithmeticDivisionOverflow, outcome, this.PC, this.AdditionalInformationOnTheWarning);
        }

        public override string ObligationName
        {
          get { return "DivisionOverflowObligation"; }
        }
      }

      /// <summary>
      /// The proof obligation for Arithmetic Overflow
      /// </summary>
      abstract class ArithmeticUnderflowOrOverflowObligation
        : ProofObligationWithDecoder
      {
        #region Object Invariant

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
          Contract.Invariant(this.exp != null);
        }

        #endregion

        #region State

        readonly protected BoxedExpression exp;
        readonly protected Type type;

        #endregion

        #region To be Overridden
        //protected virtual abstract BoxedExpression Condition { get; }
        protected abstract string What { get; }
        protected abstract string what { get; }
        #endregion

        #region Constructor
        protected ArithmeticUnderflowOrOverflowObligation(BoxedExpression exp, Type type, APC pc,
          BoxedExpressionDecoder<Variable> decoder,
          IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> mdriver)
          : base(pc, decoder, mdriver, null)
        {
          Contract.Requires(exp != null);

          this.exp = exp;
          this.type = type;
        }

        #endregion

        #region overridden
        protected override ProofOutcome ValidateInternalSpecific(IFactQuery<BoxedExpression, Variable> query, ContractInferenceManager inferenceManager, IOutputResults output)
        {
          // We check the condition
          var condition = this.Condition;

          if (condition == null)
          {
            return ProofOutcome.Top;
          }

          return query.IsTrue(this.PC, condition);
        }

        protected override void PopulateWarningContextInternal(ProofOutcome outcome)
        {
          if (outcome == ProofOutcome.Top)
          {
            this.AdditionalInformationOnTheWarning.AddRange(WarningContextFetcher.InferContext(this.PC, this.exp, this.Context, this.DecoderForMetaData.IsBoolean));
          }
        }

        public override void EmitOutcome(ProofOutcome outcome, IOutputResults output)
        {
          output.EmitOutcome(GetWitness(outcome), AddHintsForTheUser(outcome, "{0}"), FixedMessages(outcome));
        }

        public override Witness GetWitness(ProofOutcome outcome)
        {
          this.PopulateWarningContext(outcome);

          return new Witness(this.ID, WarningType.ArithmeticDivisionOverflow, outcome, this.PC, this.AdditionalInformationOnTheWarning);
        }

        protected string FixedMessages(ProofOutcome outcome)
        {
          switch (outcome)
          {
            case ProofOutcome.Bottom:
              return "This arithmetic operation is unreached";

            case ProofOutcome.False:
              return What + " in the arithmetic operation";

            case ProofOutcome.Top:
              return "Possible " + what + " in the arithmetic operation";

            case ProofOutcome.True:
              return "No " + what;

            default:
              return "unknown outcome";
          }
        }

        public override string ObligationName
        {
          get { return "ArithmeticUnderflowOrOverflowObligation"; }
        }
        #endregion
      }

      class ArithmeticUnderflow
        : ArithmeticUnderflowOrOverflowObligation
      {
        private BoxedExpression condition;

        public ArithmeticUnderflow(BoxedExpression exp, Type type, APC pc, BoxedExpressionDecoder<Variable> decoder,
                   IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> mdriver)
          : base(exp, type, pc, decoder, mdriver)
        { }

        public override BoxedExpression Condition
        {
          get
          {
            if (condition == null)
            {
              object minvalue;
              if (this.DecoderForMetaData.TryGetMinValueForType(type, out minvalue))
              {
                condition = BoxedExpression.Binary(BinaryOperator.Cle, BoxedExpression.Const(minvalue, this.type, this.DecoderForMetaData), this.exp);
              }
            }

            return condition;
          }
        }

        protected override string What
        {
          get { return "Underflow"; }
        }

        protected override string what
        {
          get { return "underflow"; }
        }

        public override string ObligationName
        {
          get { return "ArithemeticUnderflow"; }
        }

      }

      class ArithmeticOverflow
        : ArithmeticUnderflowOrOverflowObligation
      {
        private BoxedExpression condition;

        public ArithmeticOverflow(BoxedExpression exp, Type type, APC pc, BoxedExpressionDecoder<Variable> decoder,
                   IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> mdriver)
          : base(exp, type, pc, decoder, mdriver)
        { }

        public override BoxedExpression Condition
        {
          get
          {
            if (condition == null)
            {
              object maxvalue;
              if (this.DecoderForMetaData.TryGetMaxValueForType(type, out maxvalue))
              {
                condition = BoxedExpression.Binary(BinaryOperator.Cle, this.exp, BoxedExpression.Const(maxvalue, this.type, this.DecoderForMetaData));
              }
            }

            return condition;
          }
        }

        protected override string What
        {
          get { return "Overflow"; }
        }

        protected override string what
        {
          get { return "overflow"; }
        }

        public override string ObligationName
        {
          get { return "ArithmeticOverflow"; }
        }
      }

      class ArithmeticUnderflowForFloats
        : ArithmeticUnderflowOrOverflowObligation
      {

        private BoxedExpression condition;

        public ArithmeticUnderflowForFloats(BoxedExpression exp, Type type, APC pc, BoxedExpressionDecoder<Variable> decoder,
                   IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> mdriver)
          : base(exp, type, pc, decoder, mdriver)
        { }

        protected override string What
        {
          get { return "Too small floating point result"; }
        }

        protected override string what
        {
          get { return "too small floating point result"; }
        }

        public override BoxedExpression Condition
        {
          get
          {
            if (condition == null)
            {
              object minvalue;
              if (this.DecoderForMetaData.TryGetMinValueForType(type, out minvalue))
              {
                condition = BoxedExpression.Binary(BinaryOperator.Cle, BoxedExpression.Const(minvalue, this.type, this.DecoderForMetaData), this.exp);
              }
            }

            return condition;
          }
        }
      }

      class ArithmeticOverflowForFloats
        : ArithmeticUnderflowOrOverflowObligation
      {
        private BoxedExpression condition;

        public ArithmeticOverflowForFloats(BoxedExpression exp, Type type, APC pc, BoxedExpressionDecoder<Variable> decoder,
                   IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> mdriver)
          : base(exp, type, pc, decoder, mdriver)
        { }

        protected override string What
        {
          get { return "Too large floating point result"; }
        }

        protected override string what
        {
          get { return "too large floating point result"; }
        }

        public override BoxedExpression Condition
        {
          get
          {
            if (condition == null)
            {
              object maxvalue;
              if (this.DecoderForMetaData.TryGetMaxValueForType(type, out maxvalue))
              {
                condition = BoxedExpression.Binary(BinaryOperator.Cle, this.exp, BoxedExpression.Const(maxvalue, this.type, this.DecoderForMetaData));
              }
            }

            return condition;
          }
        }
      }

      class ArithmeticIsNaN
        : ArithmeticUnderflowOrOverflowObligation
      {
        #region state

        private BoxedExpression condition;

        #endregion

        public ArithmeticIsNaN(BoxedExpression exp, Type type, APC pc, BoxedExpressionDecoder<Variable> decoder,
                   IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> mdriver,
                  object provenance = null)
          : base(exp, type, pc, decoder, mdriver)
        {
          Contract.Requires(exp != null);
          Contract.Requires(decoder != null);
          Contract.Requires(mdriver != null);

          this.condition = null;
        }

        protected override string What
        {
          get { return "Is not NaN"; }
        }

        protected override string what
        {
          get { return "is not NaN"; }
        }

        public override BoxedExpression Condition
        {
          get 
          {
            if (this.condition == null)
            {
              // MinusInfinity <= exp
              var geqMinValue = BoxedExpression.Binary(
                BinaryOperator.Cge_Un,
                BoxedExpression.Const(this.MinusInfinity, this.type, this.DecoderForMetaData),
                this.exp);

              // exp <= PlusInfinity
              var leqPlusInfinity = BoxedExpression.Binary(
                BinaryOperator.Cge_Un,
                this.exp,
                BoxedExpression.Const(this.PlusInfinity, this.type, this.DecoderForMetaData));

              condition = BoxedExpression.Binary(BinaryOperator.LogicalOr, geqMinValue, leqPlusInfinity);
            }

            return this.condition;
          }
        }

        private object MinusInfinity
        {
          get
          {
            if (this.MethodDriver.MetaDataDecoder.System_Single.Equals(this.type))
            {
              return Single.NegativeInfinity;
            }
            else
            {
              return Double.NegativeInfinity;
            }
          }
        }
        private object PlusInfinity
        {
          get
          {
            if (this.MethodDriver.MetaDataDecoder.System_Single.Equals(this.type))
            {
              return Single.PositiveInfinity;
            }
            else
            {
              return Double.PositiveInfinity;
            }
          }
        }
      }

      class NewArrayArithmeticOverflow
        : ArithmeticUnderflowOrOverflowObligation
      {
        private BoxedExpression condition;

        public NewArrayArithmeticOverflow(BoxedExpression exp, APC pc, BoxedExpressionDecoder<Variable> decoder,
                   IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> mdriver)
          : base(exp, mdriver.MetaDataDecoder.System_Int32, pc, decoder, mdriver)
        { }

        public override BoxedExpression Condition
        {
          get
          {
            if (condition == null)
            {
              condition = BoxedExpression.Binary(BinaryOperator.Cle, this.exp, BoxedExpression.Const((Int64)Int32.MaxValue, this.DecoderForMetaData.System_Int64, this.DecoderForMetaData));
            }
            return condition;
          }
        }

        /// <summary>
        /// We return 0 leq exp
        /// </summary>
        public override BoxedExpression ConditionForPreconditionInference
        {
          get
          {
            return BoxedExpression.Binary(BinaryOperator.Cle, BoxedExpression.Const(0, this.DecoderForMetaData.System_Int32, this.DecoderForMetaData), this.exp); 
          }
        }

        protected override ProofOutcome ValidateInternalSpecific(IFactQuery<BoxedExpression, Variable> query, ContractInferenceManager inferenceManager, IOutputResults output)
        {
          // If it is a variable or a constant, then there is nothing to do, and we return true.

          if (this.exp.IsConstant || this.exp.IsVariable)
          {
            return ProofOutcome.True;
          }

          return base.ValidateInternalSpecific(query, inferenceManager, output);
        }

        protected override string What
        {
          get { return "Overflow (caused by a negative array size)"; }
        }

        protected override string what
        {
          get { return "overflow (caused by a negative array size)"; }
        }

        public override string ObligationName
        {
          get { return "NewArrayArithmeticOverflow"; }
        }

      }

      /// <summary>
      /// The proof obligation for the unary negation
      /// </summary>
      class NoMinValueObligation
        : ProofObligationWithDecoder
      {
        #region Statics
        public static readonly string[] fixedMessages = 
          {
            "Possible negation of MinValue",
            "This arithmetic operation is unreached",
            "Negation ok (no MinValue)",
            "Negation of MinValue"
          };
        #endregion

        #region Object invariant

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
          Contract.Invariant(this.arg != null);
        }

        #endregion

        #region State

        readonly BoxedExpression arg;
        readonly Type typeOfArg; // either int8, int16, int32, or int64
        string nameOfTypeOfArg;

        #endregion

        #region Constructor
        public NoMinValueObligation
        (
          Type typeOfArg,
          string nameOfTypeOfArg,
          BoxedExpression arg, BoxedExpressionDecoder<Variable> decoder, APC pc,
          IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> mdriver
        )
          : base(pc, decoder, mdriver, null)
        {
          Contract.Requires(arg != null);

          this.arg = arg;
          this.typeOfArg = typeOfArg;
          this.nameOfTypeOfArg = nameOfTypeOfArg;
        }
        #endregion

        #region Overridden

        public override BoxedExpression Condition
        {
          get
          {
            object value;
            if (!this.DecoderForMetaData.TryGetMinValueForType(this.typeOfArg, out value))
            {
              return null;
            }
            return BoxedExpression.Binary(BinaryOperator.Cne_Un, this.arg, BoxedExpression.Const(value, this.typeOfArg, this.DecoderForMetaData));
          }
        }

        public override void EmitOutcome(ProofOutcome outcome, IOutputResults output)
        {
          output.EmitOutcome(GetWitness(outcome), AddHintsForTheUser(outcome, "{0} of type {1}"), fixedMessages[(int)outcome], nameOfTypeOfArg);
        }

        public override Witness GetWitness(ProofOutcome outcome)
        {
          this.PopulateWarningContext(outcome);

          return new Witness(this.ID, WarningType.ArithmeticMinValueNegation, outcome, this.PC, this.AdditionalInformationOnTheWarning);
        }

        protected override void PopulateWarningContextInternal(ProofOutcome outcome)
        {
          if (outcome == ProofOutcome.Top)
          {
            this.AdditionalInformationOnTheWarning.AddRange(WarningContextFetcher.InferContext(this.PC, this.arg, this.Context, this.DecoderForMetaData.IsBoolean));
          }
        }

        protected override ProofOutcome ValidateInternalSpecific(IFactQuery<BoxedExpression, Variable> query, ContractInferenceManager inferenceManager, IOutputResults output)
        {
          // First, we check if arg != MinValue

          object value;
          if (!this.DecoderForMetaData.TryGetMinValueForType(this.typeOfArg, out value))
          {
            return ProofOutcome.Top;
          }

          var condition = this.Condition;
          if (condition != null) // this.Condition may return null
          {
            var result = query.IsTrue(this.PC, condition);

            if (result != ProofOutcome.Top)
            {
              return result;
            }
          }
          // We check a sufficient condition: if arg is >= 0, then it is not the negation of a negative "extreme"
          var greaterThanZero = query.IsGreaterEqualToZero(this.PC, this.arg);

          return (greaterThanZero == ProofOutcome.True || greaterThanZero == ProofOutcome.Bottom)
            ? greaterThanZero : ProofOutcome.Top;
        }

        private string TypeMinValueAsString()
        {
          string prefix;
          var MetaDataDecoder = this.MethodDriver.MetaDataDecoder;

          if (MetaDataDecoder.Equal(this.typeOfArg, MetaDataDecoder.System_Int32))
            prefix = "Int32";
          else if (MetaDataDecoder.Equal(this.typeOfArg, MetaDataDecoder.System_Int64))
            prefix = "Int64";
          else if (MetaDataDecoder.Equal(this.typeOfArg, MetaDataDecoder.System_Int16))
            prefix = "Int16";
          else if (MetaDataDecoder.Equal(this.typeOfArg, MetaDataDecoder.System_Int8))
            prefix = "Int8";
          else
            prefix = "UnknownType";

          return string.Format("{0}.MinValue", prefix);
        }

        public override string ObligationName
        {
          get { return "NoMinValueObligation"; }
        }

        #endregion
      }

      class FloatEqualityObligation : ProofObligationWithDecoder
      {
        #region Statics
        public static readonly string[] fixedMessages = 
          {
            "Possible precision mismatch for the arguments of ==",
            "This comparison is unreached",
            "The arguments of the comparison have a compatible precision",
            "The arguments of the comparisons have an INCOMPATIBLE precision"
          };
        #endregion

        #region Invariant
        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
          Contract.Invariant(this.left != null);
          Contract.Invariant(this.right != null);
        }

        #endregion

        #region State
        private readonly Variable var;
        private readonly BoxedExpression left, right;

        public FloatEqualityObligation(APC pc, Variable var, BoxedExpression left, BoxedExpression right, BoxedExpressionDecoder<Variable> decoder, IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> mdriver)
          : base(pc, decoder, mdriver, null)
        {
          this.var = var;
          this.left = left;
          this.right = right;
        }
        #endregion

        #region Overridden

        public override BoxedExpression Condition
        {
          get { return null; }
        }

        /// <summary>
        /// If we cannot validate the precision of the operands, we try to suggest an explicit cast
        /// </summary>
        protected override ProofOutcome ValidateInternalSpecific(IFactQuery<BoxedExpression, Variable> query, ContractInferenceManager inferenceManager, IOutputResults output)
        {
          var outcome = query.HaveSameFloatType(PC, this.left, this.right);

          switch (outcome)
          {
            case ProofOutcome.Top:
              {
                ConcreteFloat leftType, rightType;

                if (query.TryGetFloatType(this.PC, this.left, out leftType) && query.TryGetFloatType(this.PC, this.right, out rightType))
                {
                  APC conditionPC;
                  if (!this.MethodDriver.AdditionalSyntacticInformation.VariableDefinitions.TryGetValue(this.var, out conditionPC))
                  {
                    conditionPC = this.PC;
                  }

                  if(inferenceManager.CodeFixesManager.TrySuggestFloatingPointComparisonFix(this, conditionPC, this.left, this.right, leftType, rightType))
                  {
                    // do something? Returning true seems a bad idea
                  }
                }

                return outcome;
              }

            default:
              {
                return outcome;
              }
          }
        }

        protected override void PopulateWarningContextInternal(ProofOutcome outcome)
        {
          // does nothing
        }

        public override void EmitOutcome(ProofOutcome outcome, IOutputResults output)
        {
          output.EmitOutcome(GetWitness(outcome), fixedMessages[(int)outcome]);
        }

        public override Witness GetWitness(ProofOutcome outcome)
        {
          this.PopulateWarningContext(outcome);

          return new Witness(this.ID, WarningType.ArithmeticFloatEqualityPrecisionMismatch, outcome, this.PC, this.AdditionalInformationOnTheWarning);
        }

        public override string ObligationName
        {
          get { return "EnumIsDefinedProofObligation"; }
        }
        #endregion
      }

    }
  }
        #endregion
      #endregion
}
