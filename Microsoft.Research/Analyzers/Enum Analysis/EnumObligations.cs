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
using System.Diagnostics;
using System.Diagnostics.Contracts;

namespace Microsoft.Research.CodeAnalysis
{
  public static partial class AnalysisWrapper
  {
    /// <summary>
    /// This class is just for binding types for the internal clases
    /// </summary>
    public static partial class TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>
      where Variable : IEquatable<Variable>
      where Expression : IEquatable<Expression>
      where Type : IEquatable<Type>
    {
      internal class EnumObligations
        : ProofObligations<Local, Parameter, Method, Field, Property, Event, Type, Variable, Attribute, Assembly, BoxedExpression, ProofObligationBase<BoxedExpression, Variable>>
      {
        readonly private IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> methodDriver;

        public EnumObligations(
          IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> mdriver,
          bool noProofObligations)
        {
          Contract.Requires(mdriver != null);

          this.methodDriver = mdriver;

          if (noProofObligations)
          {
            return;
          }

          this.Run(mdriver.ValueLayer);
        }

        public override string Name
        {
          get { return "Enum"; }
        }

        public override bool Call<TypeList, ArgList>(APC pc, Method method, bool tail, bool virt, TypeList extraVarargs, Variable dest, ArgList args, bool data)
        {
          var mdDecoder = this.MetaDataDecoder;
          var formalParameters = this.MetaDataDecoder.Parameters(method);

          var isStaticCall = mdDecoder.IsStatic(method);

          Contract.Assume(isStaticCall? formalParameters.Count == args.Count : formalParameters.Count + 1 == args.Count, "sanity check, just for debugging");


          for (var i = 0; i < formalParameters.Count; i++)
          {
            var formalType = mdDecoder.ParameterType(formalParameters[i]);
            if (mdDecoder.IsEnumWithoutFlagAttribute(formalType))
            {
              var argIndex = isStaticCall ? i : i + 1;
              this.Add(new EnumIsDefinedProofObligation(EnumIsDefinedProofObligation.Kind.Parameter, pc, this.methodDriver.CurrentMethodName(), formalType, args[argIndex]));
            }
          }

          return data;
        }

        public override bool Starg(APC pc, Parameter argument, Variable source, bool data)
        {
          // TODO: wire it to the MetaDataDecoder
          return false;
        }

        public override bool Stelem(APC pc, Type arraytype, Variable array, Variable index, Variable value, bool data)
        {
          var type = this.Context.ValueContext.GetType(pc, array);
          if(type.IsNormal && this.MetaDataDecoder.IsEnumWithoutFlagAttribute(this.MetaDataDecoder.ElementType(type.Value)))
          {
            this.Add(new EnumIsDefinedProofObligation(EnumIsDefinedProofObligation.Kind.Assignment, pc, this.methodDriver.CurrentMethodName(), this.MetaDataDecoder.ElementType(type.Value), value));
          }
          return data;
        }

        public override bool Stfld(APC pc, Field field, bool @volatile, Variable obj, Variable value, bool data)
        {
          var type = this.MetaDataDecoder.FieldType(field);
          if (this.MetaDataDecoder.IsEnumWithoutFlagAttribute(type))
          {
            this.Add(new EnumIsDefinedProofObligation(EnumIsDefinedProofObligation.Kind.Assignment, pc, this.methodDriver.CurrentMethodName(), type, value));
          }
          return data;
        }

        public override bool Stind(APC pc, Type type, bool @volatile, Variable ptr, Variable value, bool data)
        {
          if (this.MetaDataDecoder.IsEnumWithoutFlagAttribute(type))
          {
            this.Add(new EnumIsDefinedProofObligation(EnumIsDefinedProofObligation.Kind.Assignment, pc, this.methodDriver.CurrentMethodName(), type, value));
          }
          return data;        
        }

        public override bool Stloc(APC pc, Local local, Variable source, bool data)
        {
          var type = this.MetaDataDecoder.LocalType(local);
          if (this.MetaDataDecoder.IsEnumWithoutFlagAttribute(type))
          {
            this.Add(new EnumIsDefinedProofObligation(EnumIsDefinedProofObligation.Kind.Assignment, pc, this.methodDriver.CurrentMethodName(), type, source));
          }
          return data;        
        }

        public override bool Stsfld(APC pc, Field field, bool @volatile, Variable value, bool data)
        {
          var type = this.MetaDataDecoder.FieldType(field);
          if (this.MetaDataDecoder.IsEnumWithoutFlagAttribute(type))
          {
            this.Add(new EnumIsDefinedProofObligation(EnumIsDefinedProofObligation.Kind.Assignment, pc, this.methodDriver.CurrentMethodName(), type, value));
          }
          return data;        
        }

        public override bool Return(APC pc, Variable source, bool data)
        {
          var mdDecoder = this.MetaDataDecoder;
          var type = mdDecoder.ReturnType(this.methodDriver.CurrentMethod);
          if (mdDecoder.IsEnumWithoutFlagAttribute(type))
          {
            var pcForObligation = this.methodDriver.ContractFreeCFG.NormalExit;

            this.Add(new EnumIsDefinedProofObligation(EnumIsDefinedProofObligation.Kind.ReturnValue, pcForObligation, this.methodDriver.CurrentMethodName(), type, source)); 
          }

          return data;
        }

        #region ProofObligation for Enum
        class EnumIsDefinedProofObligation 
          : ProofObligationBase<BoxedExpression, Variable>
        {
          #region 
          public enum Kind { Assignment, ReturnValue, Parameter}
          #endregion

          #region Messages
          public static readonly string[,] fixedMessages;


          static EnumIsDefinedProofObligation()
          {
            fixedMessages = new string[,]
              {{"The assigned value may not be in the range defined for this enum value", "This assignment is unreached", "Assignment to an enum value ok", "The assigned value is not one of those defined for this enum. Forgotten [Flag] in the enum definition?" },
              {"The returned value may not be in the range defined for this enum value", "This return point is unreached", "Returned value is in the enum range", "The returned value is not one of those defined for this enum. Forgotten [Flag] in the enum definition?" },
              {"The actual value may not be in the range defined for this enum value",  "This method call is unreached", "Actual parameter is in the enum range", "The actual value is not one of those defined for this enum. Forgotten [Flag] in the enum definition?" }};
          }

          #endregion

          #region Private state
          
          private readonly Type type;
          private readonly Variable value;
          private readonly Kind kind;

          #endregion

          /// <summary>
          /// The proof obligation roughly standing for Enum.IsDefined(type, value)
          /// </summary>
          public EnumIsDefinedProofObligation(Kind kind, APC pc, string definingMethod, Type type, Variable value)
            : base(pc, definingMethod, null)
          {
            this.type = type;
            this.value = value;
            this.kind = kind;
          }

          public override void EmitOutcome(ProofOutcome outcome, IOutputResults output)
          {
            var witness = GetWitness(outcome);

            output.EmitOutcome(witness, AddHintsForTheUser(outcome, "{0}"), fixedMessages[(int)this.kind, (int)outcome]);
          }

          public override Witness GetWitness(ProofOutcome outcome)
          {
            return new Witness(this.ID, WarningType.EnumRange, outcome, this.PC, this.AdditionalInformationOnTheWarning);
          }

          protected override void PopulateWarningContextInternal(ProofOutcome outcome)
          {
            // do nothing
          }

          public override BoxedExpression Condition
          {
            get { return null; }
          }

          protected override ProofOutcome ValidateInternal(IFactQuery<BoxedExpression, Variable> query, ContractInferenceManager inferenceManager, IOutputResults output)
          {
            ProofOutcome outcome;
            if ((outcome = query.IsVariableDefinedForType(this.PC, this.value, this.type)) != ProofOutcome.Top)
            {
              return outcome;
            }

            return ProofOutcome.Top;
          }

          public override string ObligationName
          {
            get { return "EnumIsDefinedProofObligation"; }
          }
        }
        #endregion
      }
    }
  }
}