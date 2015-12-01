// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Research.DataStructures;

namespace Microsoft.Research.CodeAnalysis
{
    using StackTemp = System.Int32;
    public static class CodeLayerFactory
    {
        public static ICodeLayer<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ContextData, EdgeData>
          Create<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ContextData, EdgeData>(
            IDecodeMSIL<APC, Local, Parameter, Method, Field, Type, Expression, Variable, ContextData, EdgeData> ildecoder,
            IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder,
            IDecodeContracts<Local, Parameter, Method, Field, Type> ctDecoder,
            Converter<Expression, string> expression2String,
            Converter<Variable, string> variable2String,
            Func<Variable, Variable, bool> newerThan
          )
          where Type : IEquatable<Type>
          where ContextData : IMethodContext<Field, Method>
        {
            return new CodeLayer<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ContextData, EdgeData>(
              ildecoder, mdDecoder, ctDecoder,
              expression2String, variable2String,
              newerThan
              );
        }

        public static ICodeLayer<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ContextData, EdgeData>
        Create<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ContextData, EdgeData>(
          IDecodeMSIL<APC, Local, Parameter, Method, Field, Type, Expression, Variable, ContextData, EdgeData> ildecoder,
          IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder,
          IDecodeContracts<Local, Parameter, Method, Field, Type> ctDecoder,
          Converter<Expression, string> expression2String,
          Converter<Variable, string> variable2String,
          ILPrinter<APC> printer,
          Func<Variable, Variable, bool> newerThan
        )
          where Type : IEquatable<Type>
          where ContextData : IMethodContext<Field, Method>
        {
            return new CodeLayer<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ContextData, EdgeData>(
              ildecoder, mdDecoder, ctDecoder,
              expression2String, variable2String,
              printer, newerThan
              );
        }
    }

    internal class CodeLayer<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ContextData, EdgeData>
          : ICodeLayer<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ContextData, EdgeData>
      where Type : IEquatable<Type>
      where ContextData : IMethodContext<Field, Method>
    {
        private IDecodeMSIL<APC, Local, Parameter, Method, Field, Type, Expression, Variable, ContextData, EdgeData> ildecoder;
        private IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder;
        private IDecodeContracts<Local, Parameter, Method, Field, Type> ctDecoder;
        private Converter<Expression, string> expression2String;
        private Converter<Variable, string> variable2String;
        private ILPrinter<APC> printer;
        private Func<Variable, Variable, bool> newerThan;

        public CodeLayer(IDecodeMSIL<APC, Local, Parameter, Method, Field, Type, Expression, Variable, ContextData, EdgeData> ildecoder, IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder, IDecodeContracts<Local, Parameter, Method, Field, Type> ctDecoder,
          Converter<Expression, string> expression2String, Converter<Variable, string> variable2String,
          Func<Variable, Variable, bool> newerThan)
        {
            this.ildecoder = ildecoder;
            this.mdDecoder = mdDecoder;
            this.ctDecoder = ctDecoder;
            this.expression2String = expression2String;
            this.variable2String = variable2String;
            this.newerThan = newerThan;
        }

        public CodeLayer(IDecodeMSIL<APC, Local, Parameter, Method, Field, Type, Expression, Variable, ContextData, EdgeData> ildecoder, IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder, IDecodeContracts<Local, Parameter, Method, Field, Type> ctDecoder,
          Converter<Expression, string> expression2String, Converter<Variable, string> variable2String,
          ILPrinter<APC> printer, Func<Variable, Variable, bool> newerThan)
          : this(ildecoder, mdDecoder, ctDecoder, expression2String, variable2String, newerThan)
        {
            this.printer = printer;
        }


        #region ICodeLayer<Local,Parameter,Method,Field,Property,Event,Type,Attribute,Assembly,LogOptions,Expression,Variable,ContextData> Members

        public IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> MetaDataDecoder
        {
            get { return mdDecoder; }
        }

        public IDecodeContracts<Local, Parameter, Method, Field, Type> ContractDecoder
        {
            get { return ctDecoder; }
        }

        public IDecodeMSIL<APC, Local, Parameter, Method, Field, Type, Expression, Variable, ContextData, EdgeData> Decoder
        {
            get { return ildecoder; }
        }

        public Converter<Variable, string> VariableToString
        {
            get { return variable2String; }
        }

        public Converter<Expression, string> ExpressionToString
        {
            get { return expression2String; }
        }

        public ILPrinter<APC> Printer
        {
            get
            {
                if (printer == null)
                {
                    printer = PrinterFactory.Create(ildecoder, mdDecoder, expression2String, variable2String);
                }
                return printer;
            }
        }

        public bool NewerThan(Variable v1, Variable v2) { return newerThan(v1, v2); }

        public Func<AnalysisState, IFixpointInfo<APC, AnalysisState>> CreateForward<AnalysisState>(
          IAnalysis<APC, AnalysisState, IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Expression, Variable, AnalysisState, AnalysisState>, EdgeData> analysis,
          DFAOptions options)
        {
            var solver = ForwardAnalysisSolver<AnalysisState, Type, EdgeData>.Make(ildecoder, analysis, this.Printer);
            solver.Options = options;
            return (initialState) => { solver.Run(initialState, analysis); return solver; };
        }

        #endregion
    }
}