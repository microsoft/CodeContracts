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
using System.Diagnostics;
using Microsoft.Research.AbstractDomains;
using Microsoft.Research.AbstractDomains.Expressions;
using Microsoft.Research.AbstractDomains.Numerical;
using Microsoft.Research.DataStructures;
using ADomainKind = Microsoft.Research.CodeAnalysis.Analyzers.DomainKind;
using Generics = System.Collections.Generic;

namespace Microsoft.Research.CodeAnalysis
{
  using Generics;

  public static partial class AnalysisWrapper
  {
    /// <summary>
    /// The entry point for running the unsafe analysis
    /// </summary>
    public static IMethodResult<Variable> RunTheUnsafeAnalysis<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly, ExternalExpression, Variable>
    (
      string methodName,
      IMethodDriver<APC, Local, Parameter, Method, Field, Property, Type, Attribute, Assembly, ExternalExpression, Variable, ILogOptions> driver,  
      List<Analyzers.Unsafe.UnsafeOptions> options, 
      IOverallUnsafeStatistics overallStats
     )
      where Variable : IEquatable<Variable>
      where ExternalExpression : IEquatable<ExternalExpression>
      where Type : IEquatable<Type>
    {
      return TypeBindings<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly, ExternalExpression, Variable>.HelperForRunTheUnsafeAnalysis(methodName, driver, options, overallStats);
    }

    public static partial class TypeBindings<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly, ExternalExpression, Variable>
      where Variable : IEquatable<Variable>
      where ExternalExpression : IEquatable<ExternalExpression>
      where Type : IEquatable<Type>
    {

      public static IMethodResult<Variable> HelperForRunTheUnsafeAnalysis
        (string/*!*/ methodName, 
        IMethodDriver<APC, Local, Parameter, Method, Field, Property, Type, Attribute, Assembly, ExternalExpression, Variable, ILogOptions>/*!*/ driver,
        List<Analyzers.Unsafe.UnsafeOptions> options, IOverallUnsafeStatistics overallStats
        )
      {
        var adomain = options[0].type;

        switch (adomain)
        {
          case ADomainKind.Stripes:
            return RunTheAnalysis(methodName, driver, new StripeAnalysis(methodName, driver, options, overallStats));

          case ADomainKind.StripesIntervals:
            return RunTheAnalysis(methodName, driver, new StripeWithIntervalsAnalysis(methodName, driver, options, overallStats));

          case ADomainKind.StripesIntervalsKarr:
            return RunTheAnalysis(methodName, driver, new StripeIntervalsKarrAnalysis(methodName, driver, options, overallStats));

          case ADomainKind.Pentagons:
            return RunTheAnalysis(methodName, driver, new PentagonUnsafeCodeAnalysis(methodName, driver, options, overallStats));

#if POLYHEDRA
          case ADomainKind.Polyhedra:
              return RunTheAnalysis(methodName, driver, new PolyhedraUnsafeAnalysis(methodName, driver, options, overallStats));
#endif
          case ADomainKind.Intervals:
            return RunTheAnalysis(methodName, driver, new IntervalUnsafeCodeAnalysis(methodName, driver, options, overallStats));

          case ADomainKind.Karr:
            return RunTheAnalysis(methodName, driver, new KarrAnalysis(methodName, driver, options, overallStats));

          case ADomainKind.SubPolyhedra:
            return RunTheAnalysis(methodName, driver, new SubPolyhedraAnalysisForUnsafeCode(methodName, driver, options, overallStats));

          case ADomainKind.Top:
            return RunTheAnalysis(methodName, driver, new TopAnalysis(methodName, driver, options, overallStats));

          default:
            throw new NotImplementedException("The analysis " + adomain + " has not been implemented yet");
        }
      }

      abstract internal partial class UnsafeCodeAnalysis :
        GenericNumericalAnalysis<Analyzers.Unsafe.UnsafeOptions>
      {

        //It contains the results produced by the function System.Runtime.CompilerServices.RuntimeHelpers.get_OffsetToStringData
        //It is used by the visitor of the Call statement
        private Set<BoxedExpression> zero = new Set<BoxedExpression>();

        UnsafeObligations obligations;
        IOverallUnsafeStatistics overallStats;

        IFactQuery<BoxedExpression, Variable> factQuery;

        List<Analyzers.Unsafe.UnsafeOptions> optionsList;

        //new List<IFixpointInfo<APC, INumericalAbstractDomain<BoxedExpression>>> fixpointInfo_List;
        
        #region Constructor
        protected UnsafeCodeAnalysis(
          string methodName,
          IMethodDriver<APC, Local, Parameter, Method, Field, Property, Type, Attribute, Assembly, ExternalExpression, Variable,ILogOptions> mdriver,
          List<Analyzers.Unsafe.UnsafeOptions> options,
          IOverallUnsafeStatistics totalStats
          )
          : base(methodName, mdriver, options[0])
        {
          this.optionsList = options;
          this.obligations = new UnsafeObligations(this);
          this.factQuery = null;

          totalStats.AddUncheckableAccesses(obligations.UncheckableAccesses);
          this.overallStats = totalStats;
        }

        #endregion

        public override IFactQuery<BoxedExpression, Variable> FactQuery
        {
          get
          {
            if (this.factQuery == null)
            {
              this.factQuery =  new AILogicInferenceWithRefinements<INumericalAbstractDomain<BoxedExpression>>(this.Decoder,
                this.Options, (IFixpointInfo<APC, INumericalAbstractDomain<BoxedExpression>>) this.fixpointInfo, 
                this.Context, this.DecoderForMetaData, FixpointInfo);
            }

            return this.factQuery;
          }
        }

        private IEnumerable<IFixpointInfo<APC, INumericalAbstractDomain<BoxedExpression>>> FixpointInfo()
        {
          if (this.fixpointInfo_List == null)
          {
            this.fixpointInfo_List = new List<IFixpointInfo<APC, INumericalAbstractDomain<BoxedExpression>>>();
          }

          int cached_Count = fixpointInfo_List.Count;

          for (int i = 1; i < this.optionsList.Count; i++)
          {
            IFixpointInfo<APC, INumericalAbstractDomain<BoxedExpression>> result = null;
            if (cached_Count > 0 && i <= cached_Count)
            {
              result = fixpointInfo_List[i - 1];
            }
            else
            {
              var opt = this.optionsList[i];

              try
              {
                var run = (UnsafeCodeAnalysis)HelperForRunTheUnsafeAnalysis(this.MethodName,
                                this.MethodDriver, new List<Analyzers.Unsafe.UnsafeOptions>() { opt }, this.overallStats);
                result = run.fixpointInfo;
              }
              catch (TimeoutExceptionFixpointComputation)
              {
              }

              // Cache the fixpoint
              this.fixpointInfo_List.Add(result);
            }
            if (result != null) { yield return result; }
          }
        }

        #region Checker and Proof obligations

        protected override ProofObligations<Local, Parameter, Method, Field, Property, Type, Variable, ExternalExpression, Attribute, Assembly, BoxedExpression, ProofObligationBase<BoxedExpression, Variable>> Obligations
        {
          get { return obligations; }
        }

        //public override void ValidateImplicitAssertions(IOutputResults output)
        //{
        //  if (this.options.NoProofObligations)
        //  {
        //    return;
        //  }
          
        //  var inf = this.FactQuery;
        //  this.Obligations.Validate(output, inf);
        //}

        /// <summary>
        /// The checker for the size of a localloc instruction
        /// </summary>
        internal class LocallocProof : ProofObligationWithDecoder
        {
          #region Statics
          public static readonly string[] fixedMessages = {
            "The size of this localloc may be negative",
            "Localloc instruction never reached",
            "Localloc size ok",
            "Localloc instruction with a negative size"
          };
          #endregion

          BoxedExpression size;
          public LocallocProof(BoxedExpression size, BoxedExpressionDecoder decoder, APC pc,
            IMethodDriver<APC,Local,Parameter,Method,Field,Property,Type,Attribute,Assembly,ExternalExpression,Variable, ILogOptions> mdriver
          )
            : base(pc, decoder, mdriver)
          {
            this.size = size;
          }

          override public void PopulateWarningContext(ProofOutcome outcome)
          {
            if (outcome == ProofOutcome.Top)
            {
              this.warningContext.AddRange(WarningContextFetcher.InferContext(this.PC, this.size, this.Context));
            }
          }

          public override ProofOutcome Validate(IFactQuery<BoxedExpression, Variable> query, IOutputResults output)
          {
            ALog.BeginCheckingLowerBound(StringClosure.For(this.PC));

            // Check if expForIndex >= 0
            ProofOutcome result = query.IsGreaterEqualToZero(this.PC, this.size);

            if (result == ProofOutcome.Top)
            {
              bool hasVariables, dummy;
              
              var sizeInPreState = PreconditionSuggestion.ExpressionInPreState(this.size, this.Context, this.DecoderForMetaData, out hasVariables, out dummy, this.PC);

              if (sizeInPreState != null && hasVariables)
              {
                PreconditionsDiscovered++;
                this.warningContext.Add(new WarningContext(WarningContext.ContextType.PreconditionCanDischarge));

                BoxedExpression suggestedPre = BoxedExpression.Binary(BinaryOperator.Cle, sizeInPreState, BoxedExpression.Const(0, this.DecoderForMetaData.System_Int32, this.DecoderForMetaData));

                if (InferenceHelper.TryAssumePreCondition(this.PC, suggestedPre, MethodDriver, SuggestedCodeFixes.WARNING_UNSAFE_CREATION, "localloc size", output))
                {
                  result = ProofOutcome.True; // set to success
                }
              }
            }

            ALog.EndCheckingLowerBound();
            return result;
          }

          public override void EmitOutcome(ProofOutcome outcome, IOutputResults output)
          {
            var conditionAsString = ExpressionPrinter.ToString(this.size, this.DecoderForExpressions);

            var witness = new Witness(Witness.WarningType.UnsafeCreation, outcome, this.PC, this.warningContext);

            output.EmitOutcome(witness, AddHintsForTheUser(outcome, "{0} {1}"), fixedMessages[(int)outcome], conditionAsString);
          }

        }
        /// <summary>
        /// The checker for the lower bound of an access through a pointer
        /// </summary>
        internal class LowerBoundsUnsafeAccess : ProofObligationWithDecoder
        {
          #region Statics
          private static readonly string[] fixedMessagesWrite = {
            "Unsafe memory store might be below the lower bound",
            "Unsafe store never reached",
            "The lower bound of the unsafe store is correct",
            "Unsafe memory store IS below the lower bound"
          };
          private static readonly string[] fixedMessagesRead = {
            "Unsafe memory load might be below the lower bound",
            "Unsafe load never reached",
            "The lower bound of the unsafe load is correct",
            "Unsafe memory load IS below the lower bound"
          };
          #endregion

          readonly BoxedExpression condition;
          readonly bool isWrite;

          bool uncheckable;

          public LowerBoundsUnsafeAccess(BoxedExpression condition, BoxedExpressionDecoder decoder, APC pc,
            IMethodDriver<APC,Local,Parameter,Method,Field,Property,Type,Attribute,Assembly,ExternalExpression,Variable,ILogOptions> mdriver,
            bool isWrite
          )
            : base(pc, decoder, mdriver)
          {
            Debug.Assert(mdriver != null);

            this.condition = condition;
            this.uncheckable = false;
            this.isWrite = isWrite;
          }

          public LowerBoundsUnsafeAccess(APC pc)
            : base(pc, null, null)
          {
            this.uncheckable = true;
          }

          public override ProofOutcome Validate(IFactQuery<BoxedExpression, Variable> query, IOutputResults output)
          {
            ALog.BeginCheckingLowerBound(StringClosure.For(this.PC));

            ProofOutcome result;

            if (uncheckable)
            {
              result = ProofOutcome.Top;
            }
            else
            {
              result = query.IsTrue(this.PC, this.condition);

              bool conditionHasVariables, dummy;
              if (result == ProofOutcome.Top)
              {
                var conditionInPreState = PreconditionSuggestion.ExpressionInPreState(this.condition, this.Context, this.DecoderForMetaData, out conditionHasVariables, out dummy, this.PC);
                if (conditionInPreState != null && conditionHasVariables)
                {
                  PreconditionsDiscovered++;
                  this.warningContext.Add(new WarningContext(WarningContext.ContextType.PreconditionCanDischarge));

                  if (InferenceHelper.TryAssumePreCondition(this.PC, conditionInPreState, MethodDriver, SuggestedCodeFixes.WARNING_UNSAFE_BOUND, "unsafe lower bound", output))
                  {
                    result = ProofOutcome.True; // set to success
                  }
                }
              }
            }

            ALog.EndCheckingLowerBound();

            return result;
          }

          public override void EmitOutcome(ProofOutcome outcome, IOutputResults output)
          {
            this.PopulateWarningContext(outcome);

            // F: What is this isWrite? Maybe some legacy code?
            if (isWrite)
            {
              var witness = new Witness(Witness.WarningType.UnsafeLowerBound, outcome, this.PC, this.warningContext);
              output.EmitOutcome(witness, AddHintsForTheUser(outcome, "{0}"), fixedMessagesWrite[(int)outcome]);
            }
            else
            {
              var witness = new Witness(Witness.WarningType.UnsafeLowerBound, outcome, this.PC, this.warningContext);
              output.EmitOutcome(witness, AddHintsForTheUser(outcome, "{0}"), fixedMessagesRead[(int)outcome]);
            }
          }

          public override void PopulateWarningContext(ProofOutcome outcome)
          {
            if (outcome == ProofOutcome.Top)
            {
              this.warningContext.AddRange(WarningContextFetcher.InferContext(this.PC, this.condition, this.Context));
            } 
          }
        }

        /// <summary>
        /// The checker for the upper bound of an access through a pointer
        /// </summary>
        internal class UpperBoundsUnsafeAccess : ProofObligationWithDecoder
        {
          #region Statics
          private static readonly string[] fixedMessagesRead = {
            "Unsafe memory load might be above the upper bound",
            "Unsafe load never reached",
            "The upper bound of the unsafe load is correct",
            "Unsafe memory load IS above the upper bound"
          };
          private static readonly string[] fixedMessagesWrite = {
            "Unsafe memory store might be above the upper bound",
            "Unsafe store never reached",
            "The upper bound of the unsafe store is correct",
            "Unsafe memory store IS above the upper bound"
          };
          #endregion

          BoxedExpression condition;
          bool uncheckable;
          bool isWrite;

          public UpperBoundsUnsafeAccess(BoxedExpression condition, BoxedExpressionDecoder decoder, APC pc,
            IMethodDriver<APC,Local,Parameter,Method,Field,Property,Type,Attribute,Assembly,ExternalExpression,Variable,ILogOptions> mdriver,
            bool isWrite
          )
            : base(pc, decoder, mdriver)
          {
            this.condition = condition;
            this.uncheckable = false;
            this.isWrite = isWrite;
          }

          public UpperBoundsUnsafeAccess(APC pc)
            : base(pc, null, null)
          {
            this.uncheckable = true;
          }

          public override ProofOutcome Validate(IFactQuery<BoxedExpression, Variable> query, IOutputResults output)
          {
            ALog.BeginCheckingUpperBound(StringClosure.For(this.PC));

            ProofOutcome result;

            if (uncheckable)
            {
              result = ProofOutcome.Top;
            }
            else
            {
              result = query.IsTrue(this.PC, condition);


              if (result == ProofOutcome.Top)
              {
                bool hasVariables, dummy;
                var conditionInPreState = PreconditionSuggestion.ExpressionInPreState(this.condition, this.Context, this.DecoderForMetaData, out hasVariables, out dummy, this.PC);

                if (conditionInPreState != null && hasVariables)
                {
                  PreconditionsDiscovered++;
                  this.warningContext.Add(new WarningContext(WarningContext.ContextType.PreconditionCanDischarge));

                  if (InferenceHelper.TryAssumePreCondition(this.PC, conditionInPreState, MethodDriver, SuggestedCodeFixes.WARNING_UNSAFE_BOUND, "unsafe upper bound", output))
                  {
                    result = ProofOutcome.True; // set to success
                  }
                }
              }
            }

            ALog.EndCheckingUpperBound();

            return result;
          }

          public override void EmitOutcome(ProofOutcome outcome, IOutputResults output)
          {
            this.PopulateWarningContext(outcome);

            if (isWrite)
            {
              var witness = new Witness(Witness.WarningType.UnsafeUpperBound, outcome, this.PC, this.warningContext);
              output.EmitOutcome(witness, AddHintsForTheUser(outcome, "{0}"), fixedMessagesWrite[(int)outcome]);
            }
            else
            {
              var witness = new Witness(Witness.WarningType.UnsafeUpperBound, outcome, this.PC, this.warningContext);
              output.EmitOutcome(witness, AddHintsForTheUser(outcome, "{0}"), fixedMessagesRead[(int)outcome]);
            }
          }

          public override void PopulateWarningContext(ProofOutcome outcome)
          {
            if (outcome == ProofOutcome.Top)
            {
              this.warningContext.AddRange(WarningContextFetcher.InferContext(this.PC, this.condition, this.Context));
            }
          }

          private bool IsNegativeOne(BoxedExpression exp)
          {
            if (exp.IsConstant)
            {
              object value = exp.Constant;
              if (value is int)
              {
                int i = (int)value;
                return i == -1;
              }
            }
            return false;
          }
          private bool IsNegativeConstant(BoxedExpression exp, out int negated)
          {
            if (exp.IsConstant)
            {
              object value = exp.Constant;
              if (value is int)
              {
                int i = (int)value;
                negated = -i;
                return i <= 0;
              }
            }
            negated = 0;
            return false;
          }

          // Is this dead code???
          private BinaryOperator ExpressLessThanAsSizeOfConstraint(ref BoxedExpression index, ref BoxedExpression size) {
            BinaryOperator bop = BinaryOperator.Clt;
            // Transform  "-1 * index < -size" into "index > size"
            int csize;
            if (index.IsBinary && index.BinaryOp == BinaryOperator.Mul && IsNegativeOne(index.BinaryLeft) && IsNegativeConstant(size, out csize))
            {
              index = index.BinaryRight;
              bop = BinaryOperator.Cgt;

              // Transform constant size into sizeof(T)
              if (csize == sizeof(byte) - 1)
              {
                size = BoxedExpression.SizeOf(this.DecoderForMetaData.System_Int8, csize + 1);
                bop = BinaryOperator.Cge;
              }
              else if (csize == sizeof(Int16) - 1)
              {
                size = BoxedExpression.SizeOf(this.DecoderForMetaData.System_Int16, csize + 1);
                bop = BinaryOperator.Cge;
              }
              else if (csize == sizeof(Int32) - 1)
              {
                size = BoxedExpression.SizeOf(this.DecoderForMetaData.System_Int32, csize + 1);
                bop = BinaryOperator.Cge;
              }
              else if (csize == sizeof(Int64) - 1)
              {
                size = BoxedExpression.SizeOf(this.DecoderForMetaData.System_Int64, csize + 1);
                bop = BinaryOperator.Cge;
              }
            }
            return bop;
          }
        }

        #endregion

        #region Visitors

        public override INumericalAbstractDomain<BoxedExpression> Binary(APC pc, BinaryOperator op, Variable dest, Variable s1, Variable s2, INumericalAbstractDomain<BoxedExpression> data)
        {
          BoxedExpression x = ToBoxedExpression(pc, dest);
          BoxedExpression leftExp = ToBoxedExpression(pc, s1);
          BoxedExpression rightExp = ToBoxedExpression(pc, s2);

          ALog.BeginTransferFunction(StringClosure.For("Assign"),
             StringClosure.For("{0} := {1}({2}, {3})",
             ExpressionPrinter.ToStringClosure(x, this.Decoder), StringClosure.For(op),
             ExpressionPrinter.ToStringClosure(leftExp, this.Decoder), ExpressionPrinter.ToStringClosure(rightExp, this.Decoder)),
             PrettyPrintPC(pc), StringClosure.For(data));

          INumericalAbstractDomain<BoxedExpression> result = base.Binary(pc, op, dest, s1, s2, data);

          switch (op)
          {            
            case BinaryOperator.Add:
            case BinaryOperator.Add_Ovf:
            case BinaryOperator.Add_Ovf_Un:
            case BinaryOperator.Sub:
            case BinaryOperator.Sub_Ovf:
            case BinaryOperator.Sub_Ovf_Un:

              FlatDomain<Type> typeForDest = this.Context.GetType(this.Context.Post(pc), dest);
              FlatDomain<Type> typeForS1 = this.Context.GetType(this.Context.Post(pc), s1);
              FlatDomain<Type> typeForS2 = this.Context.GetType(this.Context.Post(pc), s2);

              if (typeForDest.IsNormal && typeForS1.IsNormal && typeForS2.IsNormal)
              {
                // Is it some pointer arithmetic?  
                if (this.DecoderForMetaData.IsUnmanagedPointer(typeForDest.Value))
                {
                  if (this.DecoderForMetaData.IsUnmanagedPointer(typeForS1.Value) && this.DecoderForMetaData.IsPrimitive(typeForS2.Value))
                  {
                    result = PropagateWritableBytesInPointerArithmetics(pc, dest, s1, s2, FlipOperator(op), data);
                  }
                  else if (this.DecoderForMetaData.IsUnmanagedPointer(typeForS2.Value) && this.DecoderForMetaData.IsPrimitive(typeForS1.Value))
                  {
                    result = PropagateWritableBytesInPointerArithmetics(pc, dest, s2, s1, FlipOperator(op), data);
                  }
                }
                  // We want just simple assignments as x = y + k or x = y - k
                else if (AreAllInt32(typeForDest.Value, typeForS1.Value, typeForS2.Value))
                {
                  BoxedExpression repacked = BoxedExpression.Binary(op, leftExp, rightExp);
                  result.Assign(x, repacked, (INumericalAbstractDomain<BoxedExpression>) result.Clone()) ;
                }
              }
              break;
          }

          ALog.EndTransferFunction(StringClosure.For(result));

          return result;
        }

        /// <param name="op">Should be an addition or a subtraction</param>
        /// <returns>
        /// Sub if it is an addition, Add if it is a subtraction
        /// </returns>
        private BinaryOperator FlipOperator(BinaryOperator op)
        {
          switch (op)
          {
            case BinaryOperator.Add:
            case BinaryOperator.Add_Ovf:
            case BinaryOperator.Add_Ovf_Un:
              return BinaryOperator.Sub;

            case BinaryOperator.Sub:
            case BinaryOperator.Sub_Ovf:
            case BinaryOperator.Sub_Ovf_Un:
              return BinaryOperator.Add;

            default:
              throw new AbstractInterpretationException("You cannot flip this operator (precondition violation) " + op);
          }
        }

        /// <returns>
        /// <code>true</code> iff <code>t1</code>, <code>t2</code> and <code>t3</code> are Int32
        /// </returns>
        private bool AreAllInt32(Type t1, Type t2, Type t3)
        {
          return this.DecoderForMetaData.System_Int32.Equals(t1)
            && this.DecoderForMetaData.System_Int32.Equals(t2)
            && this.DecoderForMetaData.System_Int32.Equals(t3); 
        }

        /// <summary>
        /// We have a special case for conv_i to relate the lenght of a string with the WritableBytes of a pointer
        /// </summary>
        public override INumericalAbstractDomain<BoxedExpression> Unary(APC pc, UnaryOperator op, bool overflow, bool unsigned, Variable dest, Variable source, INumericalAbstractDomain<BoxedExpression> data)
        {
          BoxedExpression x = ToBoxedExpression(pc, dest);
          BoxedExpression sourceExp = ToBoxedExpression(pc, source);

          ALog.BeginTransferFunction(StringClosure.For("Unary"),
             StringClosure.For("{0} := {1}({2})", ExpressionPrinter.ToStringClosure(x, this.Decoder), StringClosure.For(op),
              ExpressionPrinter.ToStringClosure(sourceExp, this.Decoder)),
             PrettyPrintPC(pc), StringClosure.For(data)); 

          INumericalAbstractDomain<BoxedExpression> result = base.Unary(pc, op, overflow, unsigned, dest, source, data);

          FlatDomain<Type> typeForDest = this.Context.GetType(this.Context.Post(pc), dest);

          if (typeForDest.IsNormal)
          {
            FlatDomain<Type> typeForSource = this.Context.GetType(this.Context.Post(pc), source);
            switch (op)
            {
              // Handle strings
              case UnaryOperator.Conv_i:
                if (this.DecoderForMetaData.IsUnmanagedPointer(typeForDest.Value)
                  && typeForSource.IsNormal && typeForSource.Value.Equals(this.DecoderForMetaData.System_String))
                {
                  ALog.Message(StringClosure.For("Propagating the length of the string {0} as Writablebytes for {1}", 
                    StringClosure.For(sourceExp), StringClosure.For(x)));

                  result = PropagateStringLength(pc, dest, source, data);
                }
                break;

                // Handle some pointer arithmetics
              case UnaryOperator.WritableBytes:
                if (typeForSource.IsNormal 
                  && this.DecoderForMetaData.IsUnmanagedPointer(typeForSource.Value)
                  && this.Decoder.IsBinaryExpression(sourceExp))    // We want to propagate WritableBytest just for pointer arithmetics
                {
                  Pair<BoxedExpression, bool> exp = 
                    VisitorToObtainWriteExtent.Transform(pc, this.Context.Refine(pc, source), this.Context, this.Decoder.Outdecoder, this.SizeOfElementType, this.DecoderForMetaData);

                  if (exp.Two) // successful transformation
                  {
                    ALog.Message(StringClosure.For("Adding the constraint {0} == {1}", StringClosure.For(x), StringClosure.For(exp.One)));
                    BoxedExpression newConstraint = BoxedExpression.Binary(BinaryOperator.Ceq, x, exp.One);

                    result = (INumericalAbstractDomain<BoxedExpression>) result.TestTrue(newConstraint);
                  }
                }

                break;
            }
          }
          ALog.EndTransferFunction(StringClosure.For(result));

          return result;
        }

        public override INumericalAbstractDomain<BoxedExpression> Ldsflda(APC pc, Field field, Variable dest, INumericalAbstractDomain<BoxedExpression> data)
        {
          return this.LoadAddress(pc, dest, this.DecoderForMetaData.FieldType(field), data);
        }

        public override INumericalAbstractDomain<BoxedExpression> Ldflda(APC pc, Field field, Variable dest, Variable obj, INumericalAbstractDomain<BoxedExpression> data)
        {
          FlatDomain<Type> type = this.Context.GetType(pc, obj);
          if (type.IsNormal && this.DecoderForMetaData.IsUnmanagedPointer(type.Value))
          {
            data = AssumeTheCondition(pc, this.DecoderForMetaData.ElementType(type.Value), false, obj, data);
            return data;
          }
          else
          {
            return this.LoadAddress(pc, dest, this.DecoderForMetaData.FieldType(field), data);
          }
        }

        public override INumericalAbstractDomain<BoxedExpression> Ldloca(APC pc, Local local, Variable dest, INumericalAbstractDomain<BoxedExpression> data)
        {
          return this.LoadAddress(pc, dest, this.DecoderForMetaData.LocalType(local), data);
        }

        public override INumericalAbstractDomain<BoxedExpression> Ldarga(APC pc, Parameter argument, bool isOld, Variable dest, INumericalAbstractDomain<BoxedExpression> data)
        {
          return this.LoadAddress(pc, dest, this.DecoderForMetaData.ParameterType(argument), data);
        }

        public override INumericalAbstractDomain<BoxedExpression> Stind(APC pc, Type type, bool @volatile, Variable ptr, Variable value, INumericalAbstractDomain<BoxedExpression> data)
        {
          ALog.BeginTransferFunction(StringClosure.For("Stind"),
            StringClosure.For("{0} := {1}",
              StringClosure.For(data.ToString(ToBoxedExpression(pc, ptr))),
              StringClosure.For(data.ToString(ToBoxedExpression(pc, value)))),
            PrettyPrintPC(pc), StringClosure.For(data));

          // Pietro's
          //if (strong)
          if(true)
          {
            BoxedExpression ToBeChecked = BoxedExpression.Binary(BinaryOperator.Ceq, BoxedExpression.Convert(this.Context.Refine(this.Context.Post(pc), value), this.Decoder.Outdecoder), BoxedExpression.Convert(this.Context.Refine(pc, ptr), this.Decoder.Outdecoder));
            data = (INumericalAbstractDomain<BoxedExpression>)data.TestTrue(ToBeChecked);
          }

          data = AssumeTheCondition(pc, type, @volatile, ptr, data);

          ALog.EndTransferFunction(StringClosure.For(data));

          return data;
        }

        public override INumericalAbstractDomain<BoxedExpression> Ldind(APC pc, Type type, bool @volatile, Variable dest, Variable ptr, INumericalAbstractDomain<BoxedExpression> data)
        {
          ALog.BeginTransferFunction(StringClosure.For("LdInd"), StringClosure.For(""), PrettyPrintPC(pc), StringClosure.For(data));

          //Pietro's
          //if (strong)
          if(true)
          {
            BoxedExpression ToBeChecked = BoxedExpression.Binary(BinaryOperator.Ceq, BoxedExpression.Convert(this.Context.Refine(this.Context.Post(pc), dest), this.Decoder.Outdecoder), BoxedExpression.Convert(this.Context.Refine(pc, ptr), this.Decoder.Outdecoder));
            data = (INumericalAbstractDomain<BoxedExpression>)data.TestTrue(ToBeChecked);
          }

          data = AssumeTheCondition(pc, type, @volatile, ptr, data);

          ALog.EndTransferFunction(StringClosure.For(data));

          return data;
        }

        public override INumericalAbstractDomain<BoxedExpression> Stfld(APC pc, Field field, bool @volatile, Variable obj, Variable value, INumericalAbstractDomain<BoxedExpression> data)
        {
          FlatDomain<Type> type = this.Context.GetType(pc, obj);
          if (type.IsNormal && this.DecoderForMetaData.IsUnmanagedPointer(type.Value))
          {
            data = AssumeTheCondition(pc, type.Value, @volatile, obj, data); 
          }
          
          return data;
        }

        public override INumericalAbstractDomain<BoxedExpression> Ldfld(APC pc, Field field, bool @volatile, Variable dest, Variable obj, INumericalAbstractDomain<BoxedExpression> data)
        {
          FlatDomain<Type> type = this.Context.GetType(pc, obj);
          if (type.IsNormal && this.DecoderForMetaData.IsUnmanagedPointer(type.Value))
          {
            data = AssumeTheCondition(pc, type.Value, @volatile, obj, data);
          }

          return data;
        }

        #region Code to infer the new assumptions to add - It should be made nicer, and merged with some other

        private INumericalAbstractDomain<BoxedExpression> AssumeTheCondition(APC pc, Type type, bool @volatile, Variable ptr, INumericalAbstractDomain<BoxedExpression> data)
        {
          BoxedExpression lowerBound, upperBound;
          if (ComputePointerOffsetConstraints(pc, type, @volatile, ptr, out lowerBound, out upperBound))
          {
            ALog.Message(StringClosure.For("LowerBound Condition : {0}", ExpressionPrinter.ToStringClosure(lowerBound, this.Decoder)));
            ALog.Message(StringClosure.For("UpperBound Condition : {0}", ExpressionPrinter.ToStringClosure(upperBound, this.Decoder)));

            data = (INumericalAbstractDomain<BoxedExpression>)data.TestTrue(lowerBound);
            data = (INumericalAbstractDomain<BoxedExpression>)data.TestTrue(upperBound);
          }
          return data;
        }

        
        /// <summary>
        /// Given a pointer expression <para>ptr</para> used to access memory at type <para>type</para>, this code decomposes
        /// <para>ptr</para> into basePtr + offset and generates the constraints required to make the access safe.
        /// </summary>
        /// <param name="pc">PC at which access is done</param>
        /// <param name="type">Type describing the amount of memory loaded/stored by access</param>
        /// <param name="ptr">The pointer expression used to access memory</param>
        /// <param name="lowerBound">The condition: offset >= 0</param>
        /// <param name="upperBound">The condition: offset + sizeof(T) &lte; writableBytes(basePtr)</param>
        /// <returns>false if no bounds could be inferred</returns>
        private bool ComputePointerOffsetConstraints(APC pc, Type type, bool @volatile, Variable ptr, out BoxedExpression lowerBound, out BoxedExpression upperBound)
        {
          IExpressionContext<APC, Local, Parameter, Method, Field, Type, ExternalExpression, Variable> context = this.Context;
          FlatDomain<Type> ptrType = context.GetType(pc, ptr);
          if (!ptrType.IsNormal || this.DecoderForMetaData.IsManagedPointer(ptrType.Value))
          {
            lowerBound = default(BoxedExpression);
            upperBound = default(BoxedExpression);
            
            return false;
          }

          Variable basePtr, writableBytes;
          ExternalExpression ptrAsExp = this.Context.Refine(pc, ptr);

          if (!ExtractPointer(pc, ptrAsExp, out basePtr, out writableBytes)) 
          {
            lowerBound = default(BoxedExpression);
            upperBound = default(BoxedExpression);
            
            return false;
          }

          BoxedExpression offset;
          ExternalExpression basePtrAsExpr = this.Context.Refine(pc, basePtr);

          //Extract the expression in order to check if the index accessed is less or equal than the upper bound of the allocated stack
          if (!ExtractOffset(pc, ptrAsExp, basePtrAsExpr, out offset))
          {
            lowerBound = default(BoxedExpression);
            upperBound = default(BoxedExpression);
            
            return false;
          }

          BoxedExpression writableBytesExp = ToBoxedExpression(pc, writableBytes);

          int size = SizeOfType(type, this.DecoderForMetaData);
          if (size == -1)
          {
            lowerBound = default(BoxedExpression);
            upperBound = default(BoxedExpression);
            return false;
          }

#if UseCONSTANTForSizeOf
          BoxedExpression offsetPlusSize = BoxedExpression.Binary(BinaryOperator.Add, offset, BoxedExpression.Const(size, this.DecoderForMetaData.System_Int32));
#else
          BoxedExpression offsetPlusSize = BoxedExpression.Binary(BinaryOperator.Add, offset, BoxedExpression.SizeOf(type, size));
#endif
          // offset + sizeof(T) <= writableBytes
          upperBound = BoxedExpression.Binary(BinaryOperator.Cle, offsetPlusSize, writableBytesExp);

          // offset >= 0
          lowerBound = BoxedExpression.Binary(BinaryOperator.Cge, offset, BoxedExpression.Const(0, this.DecoderForMetaData.System_Int32, this.DecoderForMetaData));

          return true;
        }

        struct VisitorForExtractingPointer : IVisitValueExprIL<ExternalExpression, Type, ExternalExpression, Variable, APC, Pair<Variable,bool>>
        {
          IExpressionContext<APC, Local, Parameter, Method, Field, Type, ExternalExpression, Variable> context;

          public VisitorForExtractingPointer(IExpressionContext<APC, Local, Parameter, Method, Field, Type, ExternalExpression, Variable> context)
          {
            this.context = context;
          }

          #region IVisitValueExprIL<ExternalExpression,Type,ExternalExpression,Variable,APC,Pair<Variable,bool>> Members

          public Pair<Variable, bool> SymbolicConstant(ExternalExpression exp, Variable symbol, APC pc)
          {
            Variable wb;
            if (context.TryGetWritableBytes(pc, symbol, out wb))
            {
              return new Pair<Variable, bool>(symbol, true);
            }
            return default(Pair<Variable, bool>);
          }

        #endregion

          #region IVisitExprIL<ExternalExpression,Type,ExternalExpression,Variable,APC,Pair<Variable,bool>> Members

          Pair<Variable, bool> Recurse(ExternalExpression exp, APC pc)
          {
            return this.context.Decode<APC, Pair<Variable, bool>, VisitorForExtractingPointer>(exp, this, pc);
          }

          public Pair<Variable, bool> Binary(ExternalExpression pc, BinaryOperator op, Variable dest, ExternalExpression s1, ExternalExpression s2, APC data)
          {
            // recurse
            Pair<Variable, bool> result = Recurse(s1, data);
            if (result.Two) return result;
            return Recurse(s2, data);
          }

          public Pair<Variable, bool> Isinst(ExternalExpression pc, Type type, Variable dest, ExternalExpression obj, APC data)
          {
            return default(Pair<Variable, bool>);
          }

          public Pair<Variable, bool> Ldconst(ExternalExpression pc, object constant, Type type, Variable dest, APC data)
          {
            return default(Pair<Variable, bool>);
          }

          public Pair<Variable, bool> Ldnull(ExternalExpression pc, Variable dest, APC data)
          {
            return default(Pair<Variable, bool>);
          }

          public Pair<Variable, bool> Sizeof(ExternalExpression pc, Type type, Variable dest, APC data)
          {
            return default(Pair<Variable, bool>);
          }

          public Pair<Variable, bool> Unary(ExternalExpression pc, UnaryOperator op, bool overflow, bool unsigned, Variable dest, ExternalExpression source, APC data)
          {
            // recurse
            return Recurse(source, data);
          }

          #endregion
        }

        /// <summary>
        /// Extracts from the pointer expression the variable that points the space of memory allocated
        /// </summary>
        /// <returns>Guarantees that it contains the ArrayLength parameter if success==true</returns>
        private bool ExtractPointer(APC pc, ExternalExpression e, out Variable basePtr, out Variable writableBytes)
        {
          Pair<Variable, bool> result = this.Context.Decode<APC, Pair<Variable, bool>, VisitorForExtractingPointer>(e, new VisitorForExtractingPointer(this.Context), pc);
          basePtr = result.One;
          bool success = result.Two;
          if (success)
          {
            this.Context.TryGetWritableBytes(pc, basePtr, out writableBytes);
          }
          else
          {
            writableBytes = default(Variable);
          }
          return success;
        }

        /// <summary>
        /// Returns the "offset" expression of a pointer expression containing basePtr.
        /// </summary>
        /// <param name="ptrExp">The pointer expression</param>
        /// <param name="basePtr">The base pointer of the allocated region</param>
        private bool ExtractOffset(APC pc, ExternalExpression ptrExp, ExternalExpression basePtr, out BoxedExpression offset)
        {
          BoxedExpression withoutSizeOf = SubstituteSizeOf(pc, ptrExp);

          Polynomial<BoxedExpression> polynomial;

          if (!Polynomial<BoxedExpression>.TryToPolynomialForm(withoutSizeOf, this.Decoder, out polynomial))
          {
            offset = null;
            return false;
          }

          // polynomial is now basePtr + offsetExpressions*
          // We want to compute just hte offset, so we subtract basePtr from it.
         
          // Original :
          // polynomial.Left.Add(new Monomial<BoxedExpression>(-1, BoxedExpression.For(basePtr, this.Decoder.Outdecoder)));
         // polynomial = polynomial.ToCanonicalForm(); // now we should have subtracted it

          polynomial = polynomial.AddMonomialToTheLeft(new Monomial<BoxedExpression>(-1, BoxedExpression.For(basePtr, this.Decoder.Outdecoder)));

          // Original :
          // offset = new Polynomial<BoxedExpression>(polynomial.Left).ToPureExpression(BoxedExpressionEncoder.Encoder(this.DecoderForMetaData, this.Context));

          // F.: I do not understand why it takes polynomial.Left above ...

          offset = polynomial.ToPureExpression(BoxedExpressionEncoder.Encoder(this.DecoderForMetaData, this.Context));
          
          return true;
        }

        #endregion



        /// <summary>
        /// We capture information for instructions like char * ptr = &array[index]
        /// </summary>
        public override INumericalAbstractDomain<BoxedExpression> Ldelema(APC pc, Type type, bool @readonly, Variable dest, Variable array, Variable index, INumericalAbstractDomain<BoxedExpression> data)
        {
          ALog.BeginTransferFunction(StringClosure.For("Ldelema"), 
            StringClosure.For("{0} := {1}[{2}]", 
              StringClosure.For(data.ToString(ToBoxedExpression(pc, dest))), 
              StringClosure.For(data.ToString(ToBoxedExpression(pc, array))), 
              StringClosure.For(data.ToString(ToBoxedExpression(pc, index)))),
            PrettyPrintPC(pc), StringClosure.For(data));

          //We extract the length of the array and the index accessed, as well as the post writable bytes of the destination.
          BoxedExpression ptr = ToBoxedExpression(Context.Post(pc), dest);
          BoxedExpression ptrLength;
          ptr.TryGetAssociatedInfo(AssociatedInfo.WritableBytes, out ptrLength);
          Variable arrayLength;

          if (!this.Context.TryGetArrayLength(pc, array, out arrayLength) || ptrLength == null)
          {
            goto top;
          }
          
          BoxedExpression arrayLengthExpr = ToBoxedExpression(pc, arrayLength);
          ExternalExpression index2 = this.Context.Refine(pc, index);

          //As the length of an array represents the number of allocated elements (and not of bytes), we have to multiply it for the size of the type of elements
          int size = this.SizeOfElements(pc, array);
          if (size == -1) 
            goto top;

          arrayLengthExpr = BoxedExpression.Binary(BinaryOperator.Mul, arrayLengthExpr, BoxedExpression.Const(size, this.DecoderForMetaData.System_Int32, this.DecoderForMetaData));
          BoxedExpression indexExpr = BoxedExpression.Binary(BinaryOperator.Mul, SubstituteSizeOf(pc, index2), BoxedExpression.Const(size, this.DecoderForMetaData.System_Int32, this.DecoderForMetaData));

          // We build up and simplify the expression ptr.WritableBytes+a*index=a*arrayLength where a is the size of the type of the array elements
          Polynomial<BoxedExpression> ptrPol;
          if (!Polynomial<BoxedExpression>.TryToPolynomialForm(ptrLength, this.Decoder, out ptrPol))
          {
            goto top;
          }

          Polynomial<BoxedExpression> arrPol;
          if (!Polynomial<BoxedExpression>.TryToPolynomialForm(arrayLengthExpr, this.Decoder, out arrPol))
          {
            goto top;
          }
          
          Polynomial<BoxedExpression> indPol;
          if (!Polynomial<BoxedExpression>.TryToPolynomialForm(indexExpr, this.Decoder, out indPol))
          {
            goto top;
          }

          Polynomial<BoxedExpression> comprehensive;

          // comphrensive = "indPol + prtPol == arrPol"

          // New : 
          List<Monomial<BoxedExpression>> leftHandSide = new List<Monomial<BoxedExpression>>();
          leftHandSide.AddRange(indPol.Left);
          leftHandSide.AddRange(ptrPol.Left);

          List<Monomial<BoxedExpression>> rightHandSide = new List<Monomial<BoxedExpression>>();
          rightHandSide.AddRange(arrPol.Left);

          if (!Polynomial<BoxedExpression>.TryToPolynomialForm(ExpressionOperator.Equal, leftHandSide,  rightHandSide, out comprehensive))
          {
            goto top;
          }     

          BoxedExpression tobechecked = comprehensive.ToPureExpression(BoxedExpressionEncoder.Encoder(this.DecoderForMetaData, this.Context));

          // We add the condition
          ALog.Message(StringClosure.For("Adding the constraint {0}", StringClosure.For(data.ToString(tobechecked))));
          INumericalAbstractDomain<BoxedExpression> result = (INumericalAbstractDomain<BoxedExpression>)data.TestTrue(tobechecked);

          //We refine the condition in order to try to capure more precise information 
          BoxedExpression right = this.Decoder.RightExpressionFor(tobechecked);
          BoxedExpression left = this.Decoder.LeftExpressionFor(tobechecked);
          
          // If it is in the form a*x-b*y==0 we transform it into a*x==b*y
          if (MatchAXMinusBXEqZero(right, left))
          {
            BoxedExpression temp = this.Decoder.LeftExpressionFor(left);
            right = BoxedExpression.Binary(BinaryOperator.Mul, left.BinaryRight, BoxedExpression.Const(-1, this.DecoderForMetaData.System_Int32, this.DecoderForMetaData)); //-1*b*y
            left = temp;
          }

          // if it is in the form of "a*x == exp" we transform it into "x == exp/a" and we try to test to true it
          if (this.Decoder.IsBinaryExpression(left) && this.Decoder.OperatorFor(left) == ExpressionOperator.Multiplication &&
                  (this.Decoder.IsConstant(this.Decoder.LeftExpressionFor(left)) || this.Decoder.IsConstant(this.Decoder.LeftExpressionFor(left))))
          {
            BoxedExpression constant, other;
            //Extract the constant and the other part of the expression
            if (this.Decoder.IsConstant(this.Decoder.LeftExpressionFor(left)))
            {
              constant = this.Decoder.LeftExpressionFor(left);
              other = this.Decoder.RightExpressionFor(left);
            }
            else
            {
              constant = this.Decoder.RightExpressionFor(left);
              other = this.Decoder.LeftExpressionFor(left);
            }

            BoxedExpression rightToBeChecked = BoxedExpression.Binary(BinaryOperator.Div, right, constant); // -> exp/a
            tobechecked = BoxedExpression.Binary(BinaryOperator.Ceq, other, rightToBeChecked);
          }


          result = (INumericalAbstractDomain<BoxedExpression>)result.TestTrue(tobechecked);

          ALog.EndTransferFunction(StringClosure.For(result));
          return result;

          // We jump here if we ignore this assertion
        top:
          ALog.EndTransferFunction(StringClosure.For(data));
          return data;
        }

        /// <summary>
        /// <bold>Strictly</bold> matches the expression "right == left" with "a*X - b*Y == 0" 
        /// </summary>
        private bool MatchAXMinusBXEqZero(BoxedExpression right, BoxedExpression left)
        {
          Int32 value;

          // 1. Check that "right" == "0"
          if(this.Decoder.IsConstant(right) 
            && this.Decoder.TryValueOf<int>(right, ExpressionType.Int32, out value) && value == 0)
          {
            // 2. Check that "left" == "+(e1, e2)"
            if(this.Decoder.IsBinaryExpression(left) && this.Decoder.OperatorFor(left) == ExpressionOperator.Addition)
            { 
              BoxedExpression e1 = this.Decoder.LeftExpressionFor(left);
              BoxedExpression e2 = this.Decoder.RightExpressionFor(left);
              
              // 3. Check that "e1" is an expression in the form of "a * x" or a variable 
              if(this.Decoder.IsBinaryExpression(e1) && this.Decoder.OperatorFor(e1) == ExpressionOperator.Multiplication)
              {
                if(!this.Decoder.IsConstant(this.Decoder.LeftExpressionFor(e1)))
                {
                  return false;
                }
              }
              else if(!this.Decoder.IsVariable(e1))
              {
                return false;
              }

              // 4. Check that "e2" is an expression in the form of "b * y" or a variable 
              if (this.Decoder.IsBinaryExpression(e2) && this.Decoder.OperatorFor(e2) == ExpressionOperator.Multiplication)
              {
                if (!this.Decoder.IsConstant(this.Decoder.LeftExpressionFor(e2)))
                {
                  return false;
                }
              }
              else if (!this.Decoder.IsVariable(e2))
              {
                return false;
              }

              // If we reach this point, we succeeded all the checks
              return true;
            }
            else
            {
              return false;
            }
          }
          else
          {
            return false;
          }
        }

        /// <summary>
        /// Add a checkpoint in order to analyze if the size of the allocated memory is greater than or equal to zero
        /// </summary>
        public override INumericalAbstractDomain<BoxedExpression> Localloc(APC pc, Variable dest, Variable size, INumericalAbstractDomain<BoxedExpression> data)
        {
          return TestSize(pc, dest, size, data);
        }

        private INumericalAbstractDomain<BoxedExpression> TestSize(APC pc, Variable dest, Variable size, INumericalAbstractDomain<BoxedExpression> data)
        {
          ExternalExpression destAtPost = this.Context.Refine(this.Context.Post(pc), dest);
          ExternalExpression length;

          //Test to true the equivalence between the writable bytes of dest and the size of the localloc
          //It is interesting when using the Karr domain and size is not a constant or interval value
          if (this.Context.TryGetWritableBytes(destAtPost, out length))
          {

            BoxedExpression toBeChecked = BoxedExpression.Convert(this.Context.Refine(pc, size), this.Decoder.Outdecoder);
            toBeChecked = BoxedExpression.Binary(BinaryOperator.Ceq, BoxedExpression.For(length, this.Decoder.Outdecoder), toBeChecked);

            INumericalAbstractDomain<BoxedExpression> result = (INumericalAbstractDomain<BoxedExpression>)data.TestTrue(toBeChecked);
            return result;
          }
          else
          {
            return data;
          }
        }

        /// <summary>
        /// We assume that the Length of a String or of a collection is greater or equal than zero
        /// </summary>
        public override INumericalAbstractDomain<BoxedExpression> Call<TypeList, ArgList>(APC pc, Method method, bool tail, bool virt, TypeList extraVarargs, Variable dest, ArgList args, INumericalAbstractDomain<BoxedExpression> data)
        {
          string methodName = this.DecoderForMetaData.FullName(method);

          ALog.BeginTransferFunction(StringClosure.For("Call"), StringClosure.For(methodName), 
            PrettyPrintPC(pc), StringClosure.For(data));

          INumericalAbstractDomain<BoxedExpression> result = base.Call(pc, method, tail, virt, extraVarargs, dest, args, data);

          if (IsLengthMethodCall(methodName)) //Manage the case in which we are reading the length of a string
          {
            BoxedExpression destExpr = ToBoxedExpression(pc, dest);
            Variable str = ((IIndexable<Variable>)args)[0];
            Variable strLength;
            if (this.Context.TryGetArrayLength(pc, str, out strLength))
            {
              BoxedExpression strLengthExpr = ToBoxedExpression(pc, strLength);
              BoxedExpression tobechecked = BoxedExpressionEncoder.Encoder(this.DecoderForMetaData,this.Context).CompoundExpressionFor(ExpressionType.Bool, ExpressionOperator.Equal, destExpr, strLengthExpr);
              result = (INumericalAbstractDomain<BoxedExpression>)result.TestTrue(tobechecked);
            }
          }
          else if (methodName.Equals("System.Runtime.CompilerServices.RuntimeHelpers.get_OffsetToStringData")) //A special case to manage assignments like char * = string
          {
            zero.Add(ToBoxedExpression(this.Context.Post(pc), dest));
          }
          else if (methodName.Contains("HeapAlloc(System.Int32,System.Int32,System.Int32)")) //When we use the external primitive HeapAlloc in order to alloc memory on the heap
          {
            Variable size = args[2]; //The size is the third argument of the HeapAlloc function
            result = this.TestSize(pc, dest, size, data);
          }

          ALog.EndTransferFunction(StringClosure.For(result));

          return result;
        }

        /// <summary>
        /// It traces the equivalences between the length of pointers when there is an assignemnt between an unmanaged and a managed pointer
        /// </summary>
        public override INumericalAbstractDomain<BoxedExpression> Stloc(APC pc, Local local, Variable source, INumericalAbstractDomain<BoxedExpression> data)
        {
          Type typeDest = this.DecoderForMetaData.LocalType(local);
          ExternalExpression sourceExpr = this.Context.Refine(pc, source);

          FlatDomain<Type> typeSource = this.Context.GetType(sourceExpr);
          if (/* !light*/
            true
            && typeSource.IsNormal && this.DecoderForMetaData.IsUnmanagedPointer(typeDest) && (!this.DecoderForMetaData.IsUnmanagedPointer(typeSource.Value) || this.ContainsPointer(pc, sourceExpr)))
          {
            APC postPC = this.Context.Post(pc);
            Variable localValue;
            if (!this.Context.TryLocalValue(postPC, local, out localValue)) { return data; }
            ExternalExpression ptr = this.Context.Refine(postPC, localValue);
            ExternalExpression ptrLength;
            if (!this.Context.TryGetWritableBytes(ptr, out ptrLength)) return data;

            Pair<BoxedExpression, bool> exp = VisitorToObtainWriteExtent.Transform(pc, sourceExpr, this.Context, this.Decoder.Outdecoder, this.SizeOfElementType, this.DecoderForMetaData);

            if (exp.Two) // successful transformation
            {
              BoxedExpression condition = BoxedExpression.Binary(BinaryOperator.Ceq, exp.One, BoxedExpression.For(ptrLength, this.Decoder.Outdecoder));
              INumericalAbstractDomain<BoxedExpression> result = (INumericalAbstractDomain<BoxedExpression>)data.TestTrue(condition);
              return result;
            }
          }

          return data;

        }

        #endregion

        #region To be implemented by subclasses
        /// <summary>
        /// Assume that the expression <code>exp</code> is greater than zero
        /// </summary>
        /// <param name="exp">The expression</param>
        /// <returns>An abstract domain where <code>exp >= 0</code></returns>
        abstract protected INumericalAbstractDomain<BoxedExpression> AssumeGreaterEqualThanZero(BoxedExpression exp, INumericalAbstractDomain<BoxedExpression> data);

        //abstract public String PostStatesToString();

        #endregion

        #region Private methods

        /// <summary>
        /// Returns true iff exp contains a pointer
        /// </summary>
        private bool ContainsPointer(APC pc, ExternalExpression exp)
        {
          BinaryOperator bop;
          UnaryOperator uop;
          object value, variable;
          Type ctype;

          ExternalExpression arg, left, right;
          if (this.Decoder.Outdecoder.IsBinaryOperator(exp, out bop, out left, out right))
            return ContainsPointer(pc, left) || ContainsPointer(pc, right);
          if (this.Decoder.Outdecoder.IsUnaryOperator(exp, out uop, out arg))
            return ContainsPointer(pc, arg);
          if (this.Decoder.Outdecoder.IsConstant(exp, out value, out ctype)) return false;
          if (this.Decoder.Outdecoder.IsVariable(exp, out variable))
          {
            FlatDomain<Type> type = this.Context.GetType(exp);
          if (type.IsNormal)
              return this.DecoderForMetaData.IsUnmanagedPointer(type.Value);
            else 
              return false;
          }
          if (this.Decoder.Outdecoder.IsNull(exp)) { return false; }
          Type sizeOfType;
          if (this.Decoder.Outdecoder.IsSizeOf(exp, out sizeOfType)) { return false; }
          Debug.Assert(false, "Type of expressions unknown");
          return false;
        }

        /// <summary>
        /// Add the relation that the WBs of the dest are equal to the length of source variable
        /// </summary>
        /// <param name="dest">Should be an unmanaged pointer</param>
        /// <param name="source">Should be a string </param>
        /// <param name="length">is the symbol representing the array length of source.</param>
        private INumericalAbstractDomain<BoxedExpression> PropagateStringLength(APC pc, Variable dest, Variable source, INumericalAbstractDomain<BoxedExpression> data)
        {          
          Variable destWritableBytesVar;
          if (this.Context.TryGetWritableBytes(this.Context.Post(pc), dest, out destWritableBytesVar))
          {
            // 1. Get the WritableBytes associated with dest
            // We convert it into an external expression
            ExternalExpression destWritableBytes = this.Context.Refine(pc, destWritableBytesVar);

            // 2. Get the length associated with source
            ExternalExpression sourceExp = this.Context.Refine(pc, source); 

            ExternalExpression sourceLenght;
            if (!this.Context.TryGetArrayLength(sourceExp, out sourceLenght))
            {
              return data;
            }

            // 3. Build the equality dest.WB = source.Length * 2
            BoxedExpression sourceLengthInBytes = BoxedExpression.Binary(BinaryOperator.Mul, BoxedExpression.For(sourceLenght, this.Decoder.Outdecoder), BoxedExpression.Const(2, this.DecoderForMetaData.System_Int32, this.DecoderForMetaData));
            BoxedExpression toBeChecked = BoxedExpression.Binary(BinaryOperator.Ceq, BoxedExpression.For(destWritableBytes, this.Decoder.Outdecoder), sourceLengthInBytes);

            data = (INumericalAbstractDomain<BoxedExpression>)data.TestTrue(toBeChecked);
          }

          return data;
        }

        /// <summary>
        /// Add  a constraint WB(dest) == WB(source) op length
        /// </summary>
        /// <param name="dest">Should be an unmanaged pointer</param>
        /// <param name="source">Should be an unmanaged pointer</param>
        /// <param name="op">Which operation (Add or Sub)</param>
        private INumericalAbstractDomain<BoxedExpression> PropagateWritableBytesInPointerArithmetics(APC pc, Variable dest, Variable source, Variable length, BinaryOperator op, INumericalAbstractDomain<BoxedExpression> adom)
        {
          ALog.Message(StringClosure.For("Propagating WritableBytes in Pointer arithmetic"));

          // 1. See if it is incremented by a constant
          BoxedExpression lengthExp = ToBoxedExpression(pc, length);
          int offset;
          if (this.zero.Contains(lengthExp))
          {
            offset = 0;
          }
          else
          {
            Interval value = adom.BoundsFor(lengthExp);
          
          // If value is not a constant, we have nothing to do, and we return
          if (value.IsTop || value.IsBottom || !value.IsSingleton)
          { 
            return adom;
          }

          // 2. Build the constraint WB(dest) == WB(source) op value 
          Rational v = value.LowerBound;    // == value.UpperBound, as we have checked above that value is a singleton
            offset = (Int32)v;
          }
          Variable wbForDestVar, wbForSourceVar;
          if (this.Context.TryGetWritableBytes(this.Context.Post(pc), dest, out wbForDestVar) 
            && this.Context.TryGetWritableBytes(this.Context.Post(pc), source, out wbForSourceVar))
          {
            BoxedExpression wbForDest = ToBoxedExpression(pc, wbForDestVar);
            BoxedExpression wbForSource = ToBoxedExpression(pc, wbForSourceVar);

            BoxedExpression increment = BoxedExpression.Const(offset, this.DecoderForMetaData.System_Int32, this.DecoderForMetaData);

            // WB(dest) == WB(source) op increment
            BoxedExpression newConstraint = BoxedExpression.Binary(BinaryOperator.Ceq, wbForDest, BoxedExpression.Binary(op, wbForSource, increment));

            adom.TestTrue(newConstraint);
          }

          return adom;
        }

        /// <summary>
        /// The generic method used to load the address of a variable and to assign to the length of the pointer the size of the type pointed by this address
        /// </summary>
        private INumericalAbstractDomain<BoxedExpression> LoadAddress(APC pc, Variable dest, Type type, INumericalAbstractDomain<BoxedExpression> data)
        {
          BoxedExpression x = ToBoxedExpression(pc, dest);

          ALog.BeginTransferFunction(StringClosure.For("Generic LoadAddress"),
             StringClosure.For("{0} := {1}", 
             ExpressionPrinter.ToStringClosure(x, this.Decoder), StringClosure.For(type.ToString())),
             PrettyPrintPC(pc), StringClosure.For(data));

          INumericalAbstractDomain<BoxedExpression> result = data;

          if (SizeOfType(type, this.DecoderForMetaData) != -1)
          {
            //We extract the length of the array and the index accessed
            BoxedExpression ptr = ToBoxedExpression(this.Context.Post(pc), dest);
            BoxedExpression ptrLength;
            if (ptr.TryGetAssociatedInfo(AssociatedInfo.WritableBytes, out ptrLength))
            {
              BoxedExpression toBeChecked = BoxedExpression.Binary(BinaryOperator.Ceq, ptrLength, BoxedExpression.Const(SizeOfType(type, this.DecoderForMetaData), this.DecoderForMetaData.System_Int32, this.DecoderForMetaData));
              result = (INumericalAbstractDomain<BoxedExpression>)data.TestTrue(toBeChecked);
            }
          }

          ALog.EndTransferFunction(StringClosure.For(result));
          return result;
        }

        class VisitorForReplacingSizeOf : IVisitValueExprIL<ExternalExpression, Type, ExternalExpression, Variable, IDecodeMetaData<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly>, BoxedExpression>
        {
          IExpressionContext<APC, Local, Parameter, Method, Field, Type, ExternalExpression, Variable> context;
          IFullExpressionDecoder<Type, ExternalExpression> decoder;

          public VisitorForReplacingSizeOf(
            IExpressionContext<APC, Local, Parameter, Method, Field, Type, ExternalExpression, Variable> context,
            IFullExpressionDecoder<Type, ExternalExpression> decoder
          )
          {
            this.context = context;
            this.decoder = decoder;
          }

          BoxedExpression Recurse(ExternalExpression exp, IDecodeMetaData<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly> mdDecoder)
          {
            return context.Decode<IDecodeMetaData<Local,Parameter,Method,Field,Property,Type,Attribute,Assembly>, BoxedExpression, VisitorForReplacingSizeOf>(exp, this, mdDecoder);
          }

          #region IVisitValueExprIL<ExternalExpression,Type,ExternalExpression,Variable,Unit,BoxedExpression> Members

          public BoxedExpression SymbolicConstant(ExternalExpression pc, Variable symbol, IDecodeMetaData<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly> mdDecoder)
          {
            return BoxedExpression.For(pc, decoder);
          }

          #endregion

          #region IVisitExprIL<ExternalExpression,Type,ExternalExpression,Variable,Unit,BoxedExpression> Members

          public BoxedExpression Binary(ExternalExpression pc, BinaryOperator op, Variable dest, ExternalExpression s1, ExternalExpression s2, IDecodeMetaData<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly> mdDecoder)
          {
            return BoxedExpression.Binary(op, Recurse(s1, mdDecoder), Recurse(s2, mdDecoder));
          }

          public BoxedExpression Isinst(ExternalExpression pc, Type type, Variable dest, ExternalExpression obj, IDecodeMetaData<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly> mdDecoder)
          {
            return BoxedExpression.For(pc, decoder);
          }

          public BoxedExpression Ldconst(ExternalExpression pc, object constant, Type type, Variable dest, IDecodeMetaData<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly> mdDecoder)
          {
            return BoxedExpression.For(pc, decoder);
          }

          public BoxedExpression Ldnull(ExternalExpression pc, Variable dest, IDecodeMetaData<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly> mdDecoder)
          {
            return BoxedExpression.For(pc, decoder);
          }

          public BoxedExpression Sizeof(ExternalExpression pc, Type type, Variable dest, IDecodeMetaData<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly> mdDecoder)
          {
            int size = mdDecoder.TypeSize(type);
            if (size != -1)
            {
              return BoxedExpression.Const(size, mdDecoder.System_Int32, mdDecoder);
          }
            return BoxedExpression.For(pc, decoder);
          }

          public BoxedExpression Unary(ExternalExpression pc, UnaryOperator op, bool overflow, bool unsigned, Variable dest, ExternalExpression source, IDecodeMetaData<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly> mdDecoder)
          {
            return BoxedExpression.Unary(op, Recurse(source, mdDecoder));
          }

          #endregion
          }

        private BoxedExpression SubstituteSizeOf(APC pc, ExternalExpression exp)
        {
          return SubstituteSizeOf(exp, this.Decoder.Outdecoder, this.Context, this.DecoderForMetaData);
          // return this.context.Decode<Converter<Type,int>, BoxedExpression, VisitorForReplacingSizeOf>(exp, new VisitorForReplacingSizeOf(this.context, this.Decoder.Outdecoder), this.SizeOfType);

          // return SubstituteSizeOf(pc, exp, this.Decoder, this.context, this.DecoderForMetaData);
        }

        /// <summary>
        /// It removes all the occurences of the unary operator WritableBytes with the size of the type
        /// </summary>
        /// <param name="exp">The expression to be simplified</param>
        private static BoxedExpression SubstituteSizeOf(
          ExternalExpression exp,
          IFullExpressionDecoder<Type, ExternalExpression> decoder,
          IExpressionContext<APC, Local, Parameter, Method, Field, Type, ExternalExpression, Variable> context,
          IDecodeMetaData<Local,Parameter,Method,Field,Property,Type,Attribute,Assembly> mdDecoder
          )
        {
          return context.Decode<IDecodeMetaData<Local,Parameter,Method,Field,Property,Type,Attribute,Assembly>, BoxedExpression, VisitorForReplacingSizeOf>(exp, new VisitorForReplacingSizeOf(context, decoder), mdDecoder);
        }

        /// <summary>
        /// This visitor transforms an ExternalExpression representing a pointer computation into a BoxedExpression representing
        /// the writable extent of the pointer expression.
        ///
        /// We compute not just the writable extent, but also whether the returned number is a writable extent or an ordinary number.
        ///
        /// There are some special cases we handle:
        ///   x + y  : if x is transformed into a writable extent and y is not, then WE(x + y) = WE(x) - y
        ///   x - y  : if both x and y transform to writable extents , then they are both pointers and the difference is a number. The result
        ///            must thus be (x-y), not WE(x) - WE(y)
        /// </summary>
        class VisitorToObtainWriteExtent : IVisitValueExprIL<ExternalExpression, Type, ExternalExpression, Variable, IExpressionContext<APC, Local, Parameter, Method, Field, Type, ExternalExpression, Variable>, Pair<BoxedExpression, bool>>
        {
          private APC pc;
          IFullExpressionDecoder<Type, ExternalExpression> decoder;
          Converter<Type, int> sizeOfElement;
          IDecodeMetaData<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly> mdDecoder;

          /// <param name="sizeOfElement">A delegate that returns the size of the element types of an array type or -1 if this is not possible</param>
          public VisitorToObtainWriteExtent(APC pc, IFullExpressionDecoder<Type, ExternalExpression> decoder, Converter<Type,int> sizeOfElement,
            IDecodeMetaData<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly> mdDecoder
          )
          {
            this.pc = pc;
            this.decoder = decoder;
            this.sizeOfElement = sizeOfElement;
            this.mdDecoder = mdDecoder;
          }

          public static Pair<BoxedExpression,bool> Transform(
            APC pc, 
            ExternalExpression exp, IExpressionContext<APC, Local, Parameter, Method, Field, Type, ExternalExpression, Variable> context,
            IFullExpressionDecoder<Type, ExternalExpression> decoder,
            Converter<Type, int> sizeofElement,
            IDecodeMetaData<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly> mdDecoder
          )
          {
            return context.Decode<IExpressionContext<APC, Local, Parameter, Method, Field, Type, ExternalExpression, Variable>, Pair<BoxedExpression, bool>, VisitorToObtainWriteExtent>(exp, new VisitorToObtainWriteExtent(pc, decoder, sizeofElement, mdDecoder), context);
          }

          #region IVisitValueExprIL<ExternalExpression,Type,ExternalExpression,Variable,IExpressionContext<APC,Local,Parameter,Method,Type,ExternalExpression,Variable>,BoxedExpression> Members

          public Pair<BoxedExpression, bool> SymbolicConstant(ExternalExpression exp, Variable symbol, IExpressionContext<APC, Local, Parameter, Method, Field, Type, ExternalExpression, Variable> data)
          {
            Variable length;
            if (data.TryGetArrayLength(pc, symbol, out length))
            {
              // to obtain the writable extent, we need to multiply the length by the size of the elements
              FlatDomain<Type> arraytype = data.GetType(exp);
              if (arraytype.IsNormal)
              {
                int size = sizeOfElement(arraytype.Value);
                if (size != -1)
                {
                  // multiply by the size
                  return new Pair<BoxedExpression, bool>(BoxedExpression.Binary(BinaryOperator.Mul, BoxedExpression.Const(size, mdDecoder.System_Int32, mdDecoder), 
                    BoxedExpression.For(data.Refine(pc, length), decoder)), true); // substitute length
                }
              }
            }
            if (data.TryGetWritableBytes(pc, symbol, out length))
            {
              return new Pair<BoxedExpression, bool>(BoxedExpression.For(data.Refine(pc, length), decoder), true);
            }
            return new Pair<BoxedExpression, bool>(BoxedExpression.For(exp, decoder), false); // leave original symbol
          }

          #endregion

          #region IVisitExprIL<ExternalExpression,Type,ExternalExpression,Variable,IExpressionContext<APC,Local,Parameter,Method,Type,ExternalExpression,Variable>,Pair<BoxedExpression,bool>> Members

          Pair<BoxedExpression, bool> Recurse(ExternalExpression exp, IExpressionContext<APC, Local, Parameter, Method, Field, Type, ExternalExpression, Variable> context)
          {
            return context.Decode<IExpressionContext<APC, Local, Parameter, Method, Field, Type, ExternalExpression, Variable>, Pair<BoxedExpression, bool>, VisitorToObtainWriteExtent>(exp, this, context);
          }

          /// <summary>
          /// Here's the interesting cases: x op y
          ///   If both sides are pointers (and thus translated to writable extents, we keep the original x op y.
          ///   If x is a pointer (and thus WE), and y is an offset, and op==add, then we subtract the offset WE(x) - y.
          /// </summary>
          public Pair<BoxedExpression, bool> Binary(ExternalExpression orig, BinaryOperator op, Variable dest, ExternalExpression s1, ExternalExpression s2, IExpressionContext<APC, Local, Parameter, Method, Field, Type, ExternalExpression, Variable> data)
          {
            Pair<BoxedExpression, bool> r1 = Recurse(s1, data);
            Pair<BoxedExpression, bool> r2 = Recurse(s2, data);
            if (r1.Two && r2.Two)
            {
              // both are extents: leave original expression
              return new Pair<BoxedExpression, bool>(BoxedExpression.For(orig, decoder), false);
            }
            if (r1.Two)
            {
              switch (op)
              {
                case BinaryOperator.Add:
                case BinaryOperator.Add_Ovf:
                case BinaryOperator.Add_Ovf_Un:
                  // turn x + offset into WE(x) - offset
                  return new Pair<BoxedExpression, bool>(BoxedExpression.Binary(BinaryOperator.Sub, r1.One, r2.One), true);
                default:
                  // other operations on pointers don't make much sense, leave original
                  return new Pair<BoxedExpression, bool>(BoxedExpression.For(orig, decoder), false);
              }
            }
            else if (r2.Two)
            {
              switch (op)
              {
                case BinaryOperator.Add:
                case BinaryOperator.Add_Ovf:
                case BinaryOperator.Add_Ovf_Un:
                  // turn offset + x into WE(x) - offset
                  return new Pair<BoxedExpression, bool>(BoxedExpression.Binary(BinaryOperator.Sub, r2.One, r1.One), true);
                default:
                  // other operations on pointers don't make much sense, leave original
                  return new Pair<BoxedExpression, bool>(BoxedExpression.For(orig, decoder), false);
              }
            }
            // none form the operation
            return new Pair<BoxedExpression,bool>(BoxedExpression.Binary(op, r1.One, r2.One), false);
          }

          public Pair<BoxedExpression, bool> Isinst(ExternalExpression exp, Type type, Variable dest, ExternalExpression obj, IExpressionContext<APC, Local, Parameter, Method, Field, Type, ExternalExpression, Variable> data)
          {
            return new Pair<BoxedExpression, bool>(BoxedExpression.For(exp, decoder), false);
          }

          public Pair<BoxedExpression, bool> Ldconst(ExternalExpression exp, object constant, Type type, Variable dest, IExpressionContext<APC, Local, Parameter, Method, Field, Type, ExternalExpression, Variable> data)
          {
            return new Pair<BoxedExpression, bool>(BoxedExpression.For(exp, decoder), false);
          }

          public Pair<BoxedExpression, bool> Ldnull(ExternalExpression exp, Variable dest, IExpressionContext<APC, Local, Parameter, Method, Field, Type, ExternalExpression, Variable> data)
          {
            return new Pair<BoxedExpression, bool>(BoxedExpression.For(exp, decoder), false);
          }

          public Pair<BoxedExpression, bool> Sizeof(ExternalExpression exp, Type type, Variable dest, IExpressionContext<APC, Local, Parameter, Method, Field, Type, ExternalExpression, Variable> data)
          {
            return new Pair<BoxedExpression, bool>(BoxedExpression.For(exp, decoder), false);
          }

          /// <summary>
          /// Just pass it through, but filter out conversions
          /// </summary>
          public Pair<BoxedExpression, bool> Unary(ExternalExpression pc, UnaryOperator op, bool overflow, bool unsigned, Variable dest, ExternalExpression source, IExpressionContext<APC, Local, Parameter, Method, Field, Type, ExternalExpression, Variable> data)
          {
            Pair<BoxedExpression, bool> r = Recurse(source, data);
            switch (op)
            {
              case UnaryOperator.Neg:
              case UnaryOperator.Not:
              case UnaryOperator.WritableBytes:
                return new Pair<BoxedExpression, bool>(BoxedExpression.Unary(op, r.One), r.Two);

              default: // conversions
                return r;
            }
          }

          #endregion
        }

        /// <summary>
        /// returns the size of the elements of the array type, or -1 if not an array type or size not determinable
        /// </summary>
        private int SizeOfElementType(Type type)
        {
          if (this.DecoderForMetaData.IsArray(type))
          {
            return this.DecoderForMetaData.TypeSize(this.DecoderForMetaData.ElementType(type));
          }
          return -1;
        }

        private int SizeOfType(Type type)
        {
          return SizeOfType(type, this.DecoderForMetaData);
        }

        /// <summary>
        /// Given a type, returns an integer containing its sizeof
        /// </summary>
        private static int SizeOfType(Type type, IDecodeMetaData<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly> decoderForMetadata)
        {
          return decoderForMetadata.TypeSize(type);
        }

        private int SizeOfElements(APC pc, Variable array)
        {
          Type arrayType = this.Context.GetType(pc, array).Value;
          Type pointer = this.DecoderForMetaData.ElementType(arrayType);
          return SizeOfType(pointer);
        }

        /// <summary>
        /// A built-in contract that specifies that the getter Count returns a value >= 0
        /// </summary>
        private bool IsLengthMethodCall(string methodName)
        {
          bool isCountInCollection = (methodName.StartsWith("System.Collections.Generic.ICollection") || methodName.StartsWith("System.Collections.ICollection")) && methodName.EndsWith("get_Count");
          bool isLengthInString = methodName.Equals("System.String.get_Length");
         
          return isCountInCollection || isLengthInString;
        }

        #endregion

        public override AnalysisStatistics Statistics()
        {
          return base.Statistics();
        }

        class UnsafeObligations 
          : ProofObligations<Local, Parameter, Method, Field, Property, Type, Variable, ExternalExpression, Attribute, Assembly, BoxedExpression, ProofObligationBase<BoxedExpression, Variable>>
        {
          BoxedExpressionDecoder<Type, ExternalExpression> expressionDecoder;

          BoxedExpressionDecoder<Type, ExternalExpression> Decoder
          {
            get
            {
              if (this.expressionDecoder == null)
              {
                this.expressionDecoder = BoxedExpressionDecoder.Decoder(new ValueExpDecoder(Context, this.MetaDataDecoder));
              }
              return this.expressionDecoder;
            }
          }

          IMethodDriver<APC, Local, Parameter, Method, Field, Property, Type, Attribute, Assembly, ExternalExpression, Variable,ILogOptions> MethodDriver 
          { 
            get 
            { 
              return this.parent.MethodDriver; 
            } 
          }
          readonly UnsafeCodeAnalysis parent;

          public UnsafeObligations(
            UnsafeCodeAnalysis parent
            )
          {
            this.parent = parent;
            if (parent.Options.NoProofObligations) return;
            this.Run(parent.MethodDriver);
          }

          public int UncheckableAccesses = 0;

          void AddUncheckableObligations(APC pc)
          {
            this.Add(new LowerBoundsUnsafeAccess(pc));
            this.Add(new UpperBoundsUnsafeAccess(pc));
            this.UncheckableAccesses++;
          }

          /// <summary>
          /// Given an expression representing an access to the memory, this method compares it in order to check if it respects the lower and
          /// upper bound of the allocated memory
          /// </summary>
          /// <param name="ptr">The expression representing the access to the memory</param>
          private void CheckPointerAccess(APC pc, Type type, bool @volatile, Variable ptr, Variable value, bool isWrite)
          {
            if (IgnoreProofObligationAtPC(pc)) return;

            FlatDomain<Type> ptrType = this.Context.GetType(pc, ptr);
            if (ptrType.IsBottom)
            {
              // null pointer, ignore here
              return;
            }

            if (ptrType.IsNormal && this.MetaDataDecoder.IsManagedPointer(ptrType.Value))
            {
              // nothing to check
              return;
            }

            BoxedExpression lowerBound, upperBound;
            if (!parent.ComputePointerOffsetConstraints(pc, type, @volatile, ptr, out lowerBound, out upperBound))
            {
              AddUncheckableObligations(pc);
              return;
            }

            //Set the checkpoints

            this.Add(new LowerBoundsUnsafeAccess(lowerBound, this.Decoder, pc, this.MethodDriver, isWrite));
            this.Add(new UpperBoundsUnsafeAccess(upperBound, this.Decoder, pc, this.MethodDriver, isWrite));
          }

          public override bool Localloc(APC pc, Variable dest, Variable size, bool data)
          {
            if (this.IgnoreProofObligationAtPC(pc)) return data;

            BoxedExpression lenAsExp = BoxedExpression.For(this.Context.Refine(pc, size), this.Decoder.Outdecoder);
            this.Add(new LocallocProof(lenAsExp, this.Decoder, pc, this.MethodDriver));
            return data;
          }

          public override bool Ldflda(APC pc, Field field, Variable dest, Variable obj, bool data)
          {
            if (this.IgnoreProofObligationAtPC(pc)) return data;

            FlatDomain<Type> type = this.Context.GetType(pc, obj);
            if (type.IsNormal && this.MetaDataDecoder.IsUnmanagedPointer(type.Value))
              CheckPointerAccess(pc, this.MetaDataDecoder.ElementType(type.Value), false, obj, dest, false);

            return data;
          }

          public override bool Stind(APC pc, Type type, bool @volatile, Variable ptr, Variable value, bool data)
          {
            if (this.IgnoreProofObligationAtPC(pc)) return data;

            CheckPointerAccess(pc, type, @volatile, ptr, value, true);
            return data;
          }

          public override bool Ldind(APC pc, Type type, bool @volatile, Variable dest, Variable ptr, bool data)
          {
            if (this.IgnoreProofObligationAtPC(pc)) return data;

            CheckPointerAccess(pc, type, @volatile, ptr, dest, false);
            return data;
          }

          public override bool Stfld(APC pc, Field field, bool @volatile, Variable obj, Variable value, bool data)
          {
            if (this.IgnoreProofObligationAtPC(pc)) return data;

            FlatDomain<Type> type = this.Context.GetType(pc, obj);
            if (type.IsNormal && this.MetaDataDecoder.IsUnmanagedPointer(type.Value))
              CheckPointerAccess(pc, type.Value, @volatile, obj, value, true);
            return data;
          }

          public override bool Ldfld(APC pc, Field field, bool @volatile, Variable dest, Variable obj, bool data)
          {
            if (this.IgnoreProofObligationAtPC(pc)) return data;

            FlatDomain<Type> type = this.Context.GetType(pc, obj);
            if (type.IsNormal && this.MetaDataDecoder.IsUnmanagedPointer(type.Value))
              CheckPointerAccess(pc, type.Value, @volatile, obj, dest, false);

            return data;
          }

        }
      }
    }
  }
}

#endif