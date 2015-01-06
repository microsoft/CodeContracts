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
using System.Diagnostics.Contracts;

namespace Microsoft.Research.CodeAnalysis
{
  using Provenance = IEnumerable<ProofObligation>;

  public static partial class AnalysisWrapper
  {
    public static partial class TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>
      where Variable : IEquatable<Variable>
      where Expression : IEquatable<Expression>
      where Type : IEquatable<Type>
    {
      // f: I am getting a warning from the C# compiler that I do not understand
      //[ContractClass(typeof(ProofObligationWithDecoderContracts))]
      abstract internal class ProofObligationWithDecoder
        : ProofObligationBase<BoxedExpression, Variable>
      {
        private BoxedExpressionDecoder<Variable> decoder;
        protected readonly IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> MethodDriver;

        abstract protected ProofOutcome ValidateInternalSpecific(IFactQuery<BoxedExpression, Variable> query, ContractInferenceManager contractManager, IOutputResults output);

        protected ProofObligationWithDecoder(APC pc, BoxedExpressionDecoder<Variable> decoder,
          IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> mdriver
          , Provenance provenance)
          : base(pc, mdriver.CurrentMethodName(), provenance)
        {
          Contract.Requires(mdriver != null);

          this.decoder = decoder;
          this.MethodDriver = mdriver;
        }

        protected BoxedExpressionDecoder<Variable> DecoderForExpressions
        {
          get
          {
            return this.decoder;
          }
        }

        protected IExpressionContext<Local, Parameter, Method, Field, Type, Expression, Variable> Context
        {
          get
          {
            return this.MethodDriver.Context;
          }
        }

        protected IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> DecoderForMetaData
        {
          get
          {
            return this.MethodDriver.MetaDataDecoder;
          }
        }

        protected override sealed ProofOutcome ValidateInternal(IFactQuery<BoxedExpression, Variable> query, ContractInferenceManager inferenceManager, IOutputResults output)
        {
          if (this.MethodDriver.Options.TraceChecks) 
          {
            output.WriteLine("Validating proof obligation: {0}", this.Condition != null ? this.Condition.ToString() : "<null?>");
          }

          var result = ValidateInternalSpecific(query, inferenceManager, output);
          if(result != ProofOutcome.Top)            
          {
            return result;
          }

          var condition = this.Condition;

          if (condition != null)
          {
            WeakestPreconditionProver.AdditionalInfo why;
            if (TryDischargeProofObligationWithWeakestPreconditions(condition, query, inferenceManager, output, out why))
            {
              return ProofOutcome.True;
            }
            else
            {
              this.AdditionalInformationOnTheWarning.AddRange(WarningContextFetcher.InferContext(this.PC, condition, this.Context, this.DecoderForMetaData.IsBoolean));
              this.AdditionalInformationOnTheWarning.AddRange(why.GetWarningContexts());
            }
          }
          return ProofOutcome.Top;
        }
        
        private bool TryDischargeProofObligationWithWeakestPreconditions(BoxedExpression condition, IFactQuery<BoxedExpression, Variable> query, ContractInferenceManager inferenceManager, IOutputResults output, out WeakestPreconditionProver.AdditionalInfo why)
        {
          var options = output.LogOptions;

          // Try to validate the assertion by WP inference
          if (options.UseWeakestPreconditions)
          {
            var driver = this.MethodDriver;
            bool messageAlreadyPrinted;
            if (driver.SyntacticComplexity.ShouldAvoidWPComputation(out messageAlreadyPrinted))
            {
              if (!messageAlreadyPrinted)
              {
                output.WriteLine("Skipping backwards computation for this method ({0}) as cccheck thinks it will cause a timeout", driver.MetaDataDecoder.Name(driver.CurrentMethod));
              }
              this.AdditionalInformationOnTheWarning.Add(new WarningContext(WarningContext.ContextType.WPSkippedBecauseAdaptiveAnalysis));
            }
            else
            {
              var path = WeakestPreconditionProver.Discharge(this.PC, condition, options.MaxPathSize, this.MethodDriver, query, inferenceManager, out why);
              return path == null;
            }
          }
          why = WeakestPreconditionProver.AdditionalInfo.None;
          return false;
        }

      }

#if false
      /*
      //[ContractClassFor(typeof(ProofObligationWithDecoder))]
      abstract internal class ProofObligationWithDecoderContracts
        : ProofObligationWithDecoder
      {
        public ProofObligationWithDecoderContracts()
          : base(default(APC), null, null)
        {
        }

        protected override ProofOutcome ValidateInternalSpecific(IFactQuery<BoxedExpression, Variable> query, ContractInferenceManager inferenceManager, IOutputResults output)
        {
          Contract.Requires(query != null);
          Contract.Requires(output != null);

          throw new NotImplementedException();
        }

        public override BoxedExpression Condition
        {
          get { throw new NotImplementedException(); }
        }

        public override void EmitOutcome(ProofOutcome outcome, IOutputResults output)
        {
          Contract.Requires(output != null);

          throw new NotImplementedException();
        }

        public override Witness GetWitness(ProofOutcome outcome)
        {
          throw new NotImplementedException();
        }

        protected override void PopulateWarningContext(ProofOutcome outcome)
        {
          throw new NotImplementedException();
        }
      }
       */
#endif
      }
  }
}