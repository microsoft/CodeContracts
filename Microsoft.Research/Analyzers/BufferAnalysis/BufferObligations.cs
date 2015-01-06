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
using System.Text;
using System.Collections.Generic;
using Microsoft.Research.AbstractDomains.Numerical;
using Microsoft.Research.AbstractDomains;
using ADomainKind = Microsoft.Research.CodeAnalysis.Analyzers.DomainKind;
using Microsoft.Research.AbstractDomains.Expressions;
using System.Diagnostics;
using System.Diagnostics.Contracts;

namespace Microsoft.Research.CodeAnalysis
{
  public static partial class AnalysisWrapper
  {
    #region Common Extension Method
    /// <summary>
    /// Because of expression reconstruction, ptr is refined to exp[base]
    /// We generate the two proof obligations:
    /// offset >= 0 (lower bound)
    /// offset + sizeof(type) \leq WB(base)
    /// </summary>
    /// <returns>false if no proof obligation could be inferred</returns>
    public static bool TryInferSafeBufferAccessConstraints<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions>
    (
      this IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions> mdriver, APC pc, Type type,
      Variable ptr, 
      out BoxedExpression lowerBound, out BoxedExpression upperBound
    )
      where Type : IEquatable<Type>
      where Expression : IEquatable<Expression>
      where Variable : IEquatable<Variable>
      where LogOptions : IFrameworkLogOptions
    {
      var ptrType = mdriver.Context.ValueContext.GetType(pc, ptr);

      // If we do not have a type, or it is a managed pointer, we are done
      if (!ptrType.IsNormal || mdriver.MetaDataDecoder.IsManagedPointer(ptrType.Value))
      {
        return FailedToInferObligation(out lowerBound, out upperBound);
      }

      // F: need to consider when sizeof is there?

      Polynomial<BoxedVariable<Variable>, BoxedExpression> pol;
      if (Polynomial<BoxedVariable<Variable>, BoxedExpression>.TryToPolynomialForm(BoxedExpression.For(mdriver.Context.ExpressionContext.Refine(pc, ptr), mdriver.ExpressionDecoder), mdriver.BoxedDecoder(), out pol))
      {
        Contract.Assume(!object.ReferenceEquals(pol, null));

        BoxedExpression basePtr, wbPtr, offset;

        if (!mdriver.TrySplitBaseWBAndOffset(pc, pol, out basePtr, out wbPtr, out offset))
        {
          return FailedToInferObligation(out lowerBound, out upperBound);
        }

        // 0 <= offset
        lowerBound = BoxedExpression.Binary(BinaryOperator.Cle, BoxedExpression.Const(0, mdriver.MetaDataDecoder.System_Int32, mdriver.MetaDataDecoder), offset);

        // offset + sizeof(T) <= WB
        var size = mdriver.MetaDataDecoder.TypeSize(type);

        if (size >= 0)
        {
          //var neededbytes = BoxedExpression.Binary(BinaryOperator.Add, offset, BoxedExpression.SizeOf(type, size));
          var neededbytes = BoxedExpression.Binary(BinaryOperator.Add,
            offset, BoxedExpression.Const(size, mdriver.MetaDataDecoder.System_Int32, mdriver.MetaDataDecoder));

          upperBound = BoxedExpression.Binary(BinaryOperator.Cle, neededbytes, wbPtr);
        }
        else // We cannot get the size statically, and we create an expression with the size expressed symbolically
        {
          var neededbytes = BoxedExpression.Binary(BinaryOperator.Add, offset, BoxedExpression.SizeOf(type, size));

          upperBound = BoxedExpression.Binary(BinaryOperator.Cle, neededbytes, wbPtr);
        }

        return true;
      }
      else
      {
        // TODO: Consider the non-polynomial case
        // F: for instance "*(p + a/b)" we do not infer any proof obligation.
        return FailedToInferObligation(out lowerBound, out upperBound);
      }
    }

    private static bool TrySplitBaseWBAndOffset<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions>
    (
      this IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions> mdriver, 
      APC pc, Polynomial<BoxedVariable<Variable>, BoxedExpression> pol, out BoxedExpression basePtr, out BoxedExpression wbPtr, out BoxedExpression offset
    )
      where LogOptions : IFrameworkLogOptions
      where Type : IEquatable<Type>
      where Variable : IEquatable<Variable>
      where Expression : IEquatable<Expression>
    {
      var foundAPointer = false;
      var offsets = new List<Monomial<BoxedVariable<Variable>>>(pol.Left.Length);
      var basePtrVar = default(Variable);

      // 1. Fetch the pointer
      foreach (var m in pol.Left)
      {
        BoxedVariable<Variable> tryVar;
        Variable v;
        if (m.IsVariable(out tryVar) && tryVar.TryUnpackVariable(out v))
        {
          var type = mdriver.Context.ValueContext.GetType(mdriver.Context.MethodContext.CFG.Post(pc), v);

          if (type.IsNormal && (mdriver.MetaDataDecoder.IsUnmanagedPointer(type.Value) || mdriver.MetaDataDecoder.IsReferenceType(type.Value)))
          {
            basePtrVar = v;

            Contract.Assume(foundAPointer == false);
            foundAPointer = true;

            continue;
          }
        }
        offsets.Add(m);
      }

      if (!foundAPointer)
      {
        basePtr = offset = wbPtr = default(BoxedExpression);

        return false;
      }

      // 2. Get the WB

      Variable varForWB;
      if (!mdriver.Context.ValueContext.TryGetWritableBytes(mdriver.Context.MethodContext.CFG.Post(pc), basePtrVar, out varForWB))
      {
        basePtr = offset = wbPtr = default(BoxedExpression);

        return false;
      }

      // 3. Construct the offset
      Polynomial<BoxedVariable<Variable>, BoxedExpression> tmpPol;
      if (!Polynomial<BoxedVariable<Variable>, BoxedExpression>.TryToPolynomialForm(offsets.ToArray(), out tmpPol))
      {
        throw new AbstractInterpretationException("Impossible case?");
      }

      basePtr = BoxedExpression.Var(basePtrVar);
      wbPtr = BoxedExpression.Var(varForWB);
      offset = tmpPol.ToPureExpression(mdriver.BoxedEncoder());

      return true;
    }

    [ThreadStatic] // because construction depends on driver
    private static object decoderCache;

    internal static BoxedExpressionDecoder<Type, Variable, Expression> BoxedDecoder<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions>
    (
      this IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions> mdriver
    )
      where Type : IEquatable<Type>
      where Expression : IEquatable<Expression>
      where Variable : IEquatable<Variable>
      where LogOptions : IFrameworkLogOptions
    {
      Contract.Ensures(Contract.Result<BoxedExpressionDecoder<Type, Variable, Expression>>() != null);

      var result = decoderCache as BoxedExpressionDecoder<Type, Variable, Expression>;
      if (result == null)
      {
        decoderCache = result = BoxedExpressionDecoder<Variable>.Decoder(new AnalysisWrapper.TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>.ValueExpDecoder(mdriver.Context, mdriver.MetaDataDecoder), 
          (obj) => mdriver.TypeFor(obj));
      }

      return result;
    }

    private static ExpressionType TypeFor<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions>(
      this IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions> mdriver,
      object exp
    )
      where Type : IEquatable<Type>
      where Expression : IEquatable<Expression>
      where Variable : IEquatable<Variable>
      where LogOptions : IFrameworkLogOptions
    {
      var be = exp as BoxedExpression;
      if (be != null && be.IsVariable)
      {
        object o;
        if (be.TryGetType(out o) && o is Type)
        {
          Type type = (Type)o;
          if (type.Equals(mdriver.MetaDataDecoder.System_Int8))
          {
            return ExpressionType.Int8;
          }
          if (type.Equals(mdriver.MetaDataDecoder.System_Int16))
          {
            return ExpressionType.Int16;
          }
          if (type.Equals(mdriver.MetaDataDecoder.System_Int32))
          {
            return ExpressionType.Int32;
          }
          if (type.Equals(mdriver.MetaDataDecoder.System_Int64))
          {
            return ExpressionType.Int64;
          }
          if (type.Equals(mdriver.MetaDataDecoder.System_UInt8))
          {
            return ExpressionType.UInt8;
          }
          if (type.Equals(mdriver.MetaDataDecoder.System_Char))
          {
            return ExpressionType.UInt16;
          }

          if (type.Equals(mdriver.MetaDataDecoder.System_UInt16))
          {
            return ExpressionType.UInt16;
          }
          if (type.Equals(mdriver.MetaDataDecoder.System_UInt32))
          {
            return ExpressionType.UInt32;
          }
          if (type.Equals(mdriver.MetaDataDecoder.System_UInt64))
          {
            return ExpressionType.UInt64;
          }
          if (type.Equals(mdriver.MetaDataDecoder.System_Single))
          {
            return ExpressionType.Float32;
          }
          if (type.Equals(mdriver.MetaDataDecoder.System_Double))
          {
            return ExpressionType.Float64;
          }

        }
      }

      return ExpressionType.Unknown;
    }

    [ThreadStatic] // because construction depends on mdriver
    private static object encoderCache;

    internal static IExpressionEncoder<BoxedVariable<Variable>, BoxedExpression> BoxedEncoder<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions>
    (
      this IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions> mdriver
    )
      where Type : IEquatable<Type>
      where Expression : IEquatable<Expression>
      where Variable : IEquatable<Variable>
      where LogOptions : IFrameworkLogOptions
    {
      Contract.Ensures(Contract.Result<IExpressionEncoder<BoxedVariable<Variable>, BoxedExpression>>() != null);

      var result = encoderCache as IExpressionEncoder<BoxedVariable<Variable>, BoxedExpression>;
      if (result == null)
      {
        encoderCache = result = BoxedExpressionEncoder<Variable>.Encoder(mdriver.MetaDataDecoder, mdriver.Context);
      }

      return result;
    }

    private static bool FailedToInferObligation(out BoxedExpression a, out BoxedExpression b)
    {
      a = b = default(BoxedExpression);
      return false;
    }
    #endregion

    /// <summary>
    /// This class is just for binding types for the internal clases
    /// </summary>
    public static partial class TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>
      where Variable : IEquatable<Variable>
      where Expression : IEquatable<Expression>
      where Type : IEquatable<Type>
    {
      internal class BufferObligations
        : ProofObligations<Local, Parameter, Method, Field, Property, Event, Type, Variable, Attribute, Assembly, BoxedExpression, ProofObligationBase<BoxedExpression, Variable>>
      {
        readonly private IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> methodDriver;
        private BoxedExpressionDecoder<Type, Variable, Expression> expressionDecoder;
        private int uncheckableAccesses;

        #region Getters
        BoxedExpressionDecoder<Type, Variable, Expression> DecoderForExpressions
        {
          get
          {
            if (this.expressionDecoder == null)
            {
              this.expressionDecoder = BoxedExpressionDecoder<Variable>.Decoder(new ValueExpDecoder(this.methodDriver.ExpressionLayer.Decoder.Context, this.MetaDataDecoder));
            }

            return this.expressionDecoder;
          }
        }

        IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> MethodDriver
        {
          get
          {
            return this.methodDriver;
          }
        }

        public int UncheckableAccesses
        {
          get
          {
            return this.uncheckableAccesses;
          }
        }
        
        #endregion

        public BufferObligations(
          IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> mdriver,
          bool noObligations)
        {
          Contract.Requires(mdriver != null);

          this.methodDriver = mdriver;
          this.uncheckableAccesses = 0;

          if (noObligations)
          {
            return;
          }

          this.Run(mdriver.ValueLayer);
        }

        public override string Name
        {
          get { return "Unsafe"; }
        }

        /// <summary>
        /// Generates the proof obligation for the memory access at program point <code>pc</code>
        /// </summary>
        private void AddProofObligationsForMemoryAccess(APC pc, Type type, bool @volatile, Variable ptr, Variable value, bool isWrite)
        {
          if (this.IgnoreProofObligationAtPC(pc))
          {
            return;
          }

          var ptrType = this.Context.ValueContext.GetType(pc, ptr);
          if (ptrType.IsBottom)
          {
            // null pointer, ignore here
            return;
          }

          if (ptrType.IsTop)
          {
            // We do not know the type, so we cannot check...
            AddUncheckableAccess(pc);

            return;
          }

          if (this.MetaDataDecoder.IsManagedPointer(ptrType.Value))
          {
            // nothing to check
            return;
          }

          BoxedExpression lowerBound, upperBound;
          if (this.methodDriver.TryInferSafeBufferAccessConstraints(pc, type, ptr, out lowerBound, out upperBound))
          {
            this.Add(new LowerBoundBufferAccess(pc, lowerBound, isWrite, this.DecoderForExpressions, this.MethodDriver));
            this.Add(new UpperBoundsBufferAccess(pc, upperBound, isWrite, this.DecoderForExpressions, this.MethodDriver));
          }
          else
          {
            AddUncheckableAccess(pc);
          }           
        }

        public override bool Localloc(APC pc, Variable dest, Variable size, bool data)
        {
          if (!this.IgnoreProofObligationAtPC(pc))
          {
            // sizeAsExp >= 0
            var sizeAsExp = BoxedExpression.For(this.methodDriver.ExpressionLayer.Decoder.Context.ExpressionContext.Refine(pc, size), this.DecoderForExpressions.Outdecoder);
            this.Add(new LocallocObligation(pc, sizeAsExp, this.DecoderForExpressions, this.MethodDriver));
          }

          return data;
        }

        public override bool Ldflda(APC pc, Field field, Variable dest, Variable obj, bool data)
        {
          if (!this.IgnoreProofObligationAtPC(pc))
          {
            var type = this.Context.ValueContext.GetType(pc, obj);
            if (type.IsNormal && this.MetaDataDecoder.IsUnmanagedPointer(type.Value))
            {
              AddProofObligationsForMemoryAccess(pc, this.MetaDataDecoder.ElementType(type.Value), false, obj, dest, false);
            }
          }
          
          return data;
        }

        public override bool Stind(APC pc, Type type, bool @volatile, Variable ptr, Variable value, bool data)
        {
          if (!this.IgnoreProofObligationAtPC(pc))
          {
            AddProofObligationsForMemoryAccess(pc, type, @volatile, ptr, value, true);
          }
          
          return data;
        }

        public override bool Ldind(APC pc, Type type, bool @volatile, Variable dest, Variable ptr, bool data)
        {
          if (!this.IgnoreProofObligationAtPC(pc))
          {
            AddProofObligationsForMemoryAccess(pc, type, @volatile, ptr, dest, false);
          }

          return data;
        }

        public override bool Stfld(APC pc, Field field, bool @volatile, Variable obj, Variable value, bool data)
        {
          if (!this.IgnoreProofObligationAtPC(pc))
          {
            var type = this.Context.ValueContext.GetType(pc, obj);
            if (type.IsNormal && this.MetaDataDecoder.IsUnmanagedPointer(type.Value))
            {
              AddProofObligationsForMemoryAccess(pc, type.Value, @volatile, obj, value, true);
            }
          }
         
          return data;
        }

        public override bool Ldfld(APC pc, Field field, bool @volatile, Variable dest, Variable obj, bool data)
        {
          if (!this.IgnoreProofObligationAtPC(pc))
          {
            var type = this.Context.ValueContext.GetType(pc, obj);
            if (type.IsNormal && this.MetaDataDecoder.IsUnmanagedPointer(type.Value))
            {
              AddProofObligationsForMemoryAccess(pc, type.Value, @volatile, obj, dest, false);
            }
          }

          return data;
        }

        public override bool Call<TypeList, ArgList>(APC pc, Method method, bool tail, bool virt, TypeList extraVarargs, Variable dest, ArgList args, bool data)
        {
          if (!this.IgnoreProofObligationAtPC(pc))
          {
            // Check for instance methods f(IntPtr, ...)
            var mdDecoder = this.MetaDataDecoder;
            if (!mdDecoder.IsStatic(method) )
            {
              var type = mdDecoder.ParameterType(mdDecoder.This(method));
              if (mdDecoder.IsManagedPointer(type))
              {
                AddProofObligationsForMemoryAccess(pc, type, false, args[0], default(Variable), false);
              }
            }
          }

          return data;
        }

        #region Private methods

        private void AddUncheckableAccess(APC pc)
        {
          this.Add(new LowerBoundBufferAccess(pc, this.DecoderForExpressions, this.MethodDriver));
          this.Add(new UpperBoundsBufferAccess(pc, this.DecoderForExpressions, this.MethodDriver));

          this.uncheckableAccesses++;
        }

        #endregion


        internal class LocallocObligation
          : ProofObligationWithDecoder
        {
          #region Messages
          public static readonly string[] fixedMessages = 
          {
            "The size of this localloc may be negative",
            "Localloc instruction never reached",
            "Localloc size ok",
            "Localloc instruction with a negative size"
          };
          #endregion

          [ContractInvariantMethod]
          private void ObjectInvariant()
          {
            Contract.Invariant(size != null);
          }

          readonly BoxedExpression size;

          public LocallocObligation(APC pc, BoxedExpression size, BoxedExpressionDecoder<Variable> decoder, 
            IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> mdriver
          )
            : base(pc, decoder, mdriver, null)
          {
            Contract.Requires(size != null);

            this.size = size;
          }

          protected override void PopulateWarningContextInternal(ProofOutcome outcome)
          {
            if (outcome == ProofOutcome.Top)
            {
              this.AdditionalInformationOnTheWarning.AddRange(WarningContextFetcher.InferContext(this.PC, this.size,  this.Context, this.DecoderForMetaData.IsBoolean));
            }
          }

          public override BoxedExpression Condition
          {
            get
            {
              return BoxedExpression.Binary(BinaryOperator.Cle, BoxedExpression.Const(0, this.MethodDriver.MetaDataDecoder.System_Int32, this.MethodDriver.MetaDataDecoder), this.size);
              ;
            }
          }

          protected override ProofOutcome ValidateInternalSpecific(IFactQuery<BoxedExpression, Variable> query, ContractInferenceManager inferenceManager, IOutputResults output)
          {
            // Check if size >= 0
            return query.IsGreaterEqualToZero(this.PC, this.size);
          }

          public override void EmitOutcome(ProofOutcome outcome, IOutputResults output)
          {
            output.EmitOutcome(GetWitness(outcome), AddHintsForTheUser(outcome, "{0}"), fixedMessages[(int)outcome]);
          }

          public override Witness GetWitness(ProofOutcome outcome)
          {
            return new Witness(this.ID, WarningType.UnsafeCreation, outcome, this.PC, this.AdditionalInformationOnTheWarning);
          }

          public override string ObligationName
          {
            get { return "LocallocObligation"; }
          }

          public override string ToString()
          {
            return ExpressionPrinter.ToGeqZero(this.size, this.DecoderForExpressions);
          }
        }

        /// <summary>
        /// Proof obligation for underflows
        /// </summary>
        [ContractVerification(true)]
        internal class LowerBoundBufferAccess
          : ProofObligationWithDecoder
        {
          #region Messages
          private static readonly string[] fixedMessagesWrite = 
          {
            "Unsafe memory store might be below the lower bound",
            "Unsafe store never reached",
            "The lower bound of the unsafe store is correct",
            "Unsafe memory store IS below the lower bound"
          };

          private static readonly string[] fixedMessagesRead =
          {
            "Unsafe memory load might be below the lower bound",
            "Unsafe load never reached",
            "The lower bound of the unsafe load is correct",
            "Unsafe memory load IS below the lower bound"
          };
          #endregion

          [ContractInvariantMethod]
          private void ObjectInvariant()
          {
            Contract.Invariant(this.uncheckable || this.condition != null);
            Contract.Invariant(fixedMessagesWrite.Length == fixedMessagesRead.Length);
          }

          readonly BoxedExpression condition;
          readonly bool isWrite;
          readonly bool uncheckable;

          public LowerBoundBufferAccess(APC pc, BoxedExpression condition, bool isWrite, BoxedExpressionDecoder<Variable> decoder, 
            IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> mdriver)
            : base(pc, decoder, mdriver, null)
          {
            Contract.Requires(condition != null);
            Contract.Requires(mdriver != null);

            this.condition = condition;
            this.uncheckable = false;
            this.isWrite = isWrite;
          }

          public LowerBoundBufferAccess(APC pc, BoxedExpressionDecoder<Variable> decoder, 
            IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> mdriver)
            : base(pc, decoder, mdriver, null)
          {
            Contract.Requires(mdriver != null);

            this.condition = default(BoxedExpression);
            this.uncheckable = true;
            this.isWrite = default(bool);
          }

          public override BoxedExpression Condition
          {
            get { return this.condition; }
          }

          public override string ObligationName
          {
            get { return "LowerBoundBufferAccess"; }
          }


          protected override ProofOutcome ValidateInternalSpecific(IFactQuery<BoxedExpression, Variable> query, ContractInferenceManager inferenceManager, IOutputResults output)
          {
            Contract.Assume(query != null, "Should be a precondition");

            if (uncheckable)
            {
              return ProofOutcome.Top;
            }

            return query.IsTrue(this.PC, this.condition);
          }

          public override void EmitOutcome(ProofOutcome outcome, IOutputResults output)
          {
            Contract.Assume(output != null, "Should be a precondition");

            var index = (int)outcome;

            if (index < 0 || index >= fixedMessagesWrite.Length)
              return;

            if (this.isWrite)
            {
              output.EmitOutcome(GetWitness(outcome), AddHintsForTheUser(outcome, "{0}"), fixedMessagesWrite[index]);
            }
            else
            {
              output.EmitOutcome(GetWitness(outcome), AddHintsForTheUser(outcome, "{0}"), fixedMessagesRead[index]);
            }
          }

          public override Witness GetWitness(ProofOutcome outcome)
          {
            this.PopulateWarningContext(outcome);
            return new Witness(this.ID, WarningType.UnsafeLowerBound, outcome, this.PC, this.AdditionalInformationOnTheWarning);
          }

          protected override void PopulateWarningContextInternal(ProofOutcome outcome)
          {
            if (outcome == ProofOutcome.Top)
            {
              this.AdditionalInformationOnTheWarning.AddRange(WarningContextFetcher.InferContext(this.PC, this.condition, this.Context, this.DecoderForMetaData.IsBoolean));
            }
          }

          [ContractVerification(false)]
          public override string ToString()
          {
            if (this.condition != null)
              return ExpressionPrinter.ToString(this.condition, this.DecoderForExpressions);
            else
              return "";
          }
        }

        /// <summary>
        /// Proof obligation for overflows
        /// </summary>
        [ContractVerification(true)]
        internal class UpperBoundsBufferAccess
          : ProofObligationWithDecoder
        {
          #region Messages
          private static readonly string[] fixedMessagesRead = 
          {
            "Unsafe memory load might be above the upper bound",
            "Unsafe load never reached",
            "The upper bound of the unsafe load is correct",
            "Unsafe memory load IS above the upper bound"
          };
          private static readonly string[] fixedMessagesWrite = 
          {
            "Unsafe memory store might be above the upper bound",
            "Unsafe store never reached",
            "The upper bound of the unsafe store is correct",
            "Unsafe memory store IS above the upper bound"
          };
          #endregion

          [ContractInvariantMethod]
          private void ObjectInvariant()
          {
            Contract.Invariant(this.uncheckable || this.condition != null);
            Contract.Invariant(fixedMessagesRead.Length == fixedMessagesWrite.Length);
          }


          readonly BoxedExpression condition;
          readonly bool isWrite;
          readonly bool uncheckable;

          public UpperBoundsBufferAccess(APC pc, BoxedExpression condition, bool isWrite, BoxedExpressionDecoder<Variable> decoder,
            IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> mdriver)
            : base(pc, decoder, mdriver, null)
          {
            Contract.Requires(mdriver != null);
            Contract.Requires(condition != null);

            this.condition = condition;
            this.uncheckable = false;
            this.isWrite = isWrite;
          }

          public UpperBoundsBufferAccess(APC pc, BoxedExpressionDecoder<Variable> decoder,
            IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> mdriver)
            : base(pc, decoder, mdriver, null)
          {
            Contract.Requires(mdriver != null);

            this.condition = default(BoxedExpression);
            this.uncheckable = true;
            this.isWrite = default(bool);
          }

          public override BoxedExpression Condition
          {
            get { return this.condition; }
          }

          protected override ProofOutcome ValidateInternalSpecific(IFactQuery<BoxedExpression, Variable> query, ContractInferenceManager inferenceManager, IOutputResults output)
          {
            Contract.Assume(query != null, "should be a precondition");

            if (this.uncheckable)
            {
              return ProofOutcome.Top;
            }

            return query.IsTrue(this.PC, condition);
          }

          public override void EmitOutcome(ProofOutcome outcome, IOutputResults output)
          {
            Contract.Assume(output != null, "Should be a precondition");

            var index = (int)outcome;

            if(index < 0 || index >= fixedMessagesWrite.Length)
              return;

            var witness = GetWitness(outcome);

            if (this.isWrite)
            {
              output.EmitOutcome(witness, AddHintsForTheUser(outcome, "{0}"), fixedMessagesWrite[index]);
            }
            else
            {
              output.EmitOutcome(witness, AddHintsForTheUser(outcome, "{0}"), fixedMessagesRead[index]);
            }
          }

          public override Witness GetWitness(ProofOutcome outcome)
          {
            this.PopulateWarningContext(outcome);
            return new Witness(this.ID, WarningType.UnsafeUpperBound, outcome, this.PC, this.AdditionalInformationOnTheWarning);
          }

          protected override void PopulateWarningContextInternal(ProofOutcome outcome)
          {
            if (outcome == ProofOutcome.Top)
            {
              this.AdditionalInformationOnTheWarning.AddRange(WarningContextFetcher.InferContext(this.PC, this.condition, this.Context, this.DecoderForMetaData.IsBoolean));
            }
          }

          public override string ObligationName
          {
            get { return "UpperBoundsBufferAccess"; }
          }

          [ContractVerification(false)]
          public override string ToString()
          {
            if (this.condition != null)
              return ExpressionPrinter.ToString(this.condition, this.DecoderForExpressions);
            else
              return "";
          }
        }
      }

    }
  }
}
#endif