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
using Microsoft.Research.DataStructures;
using DomainKind = Microsoft.Research.CodeAnalysis.Analyzers.DomainKind;
using System.Diagnostics.Contracts;

namespace Microsoft.Research.CodeAnalysis
{
  public static partial class Analyzers
  {
    public class Arrays
      : ValueAnalyzer<Arrays, Arrays.ArrayOptions>
    {
      #region Private state

      private List<Bounds.BoundsOptions> boundsOptions;      
      private Analyzers.NonNull nonnullAnalysis;
      private bool IsEnumAnalysisSelected = false;
      
      #endregion

      public override string Name
      {
        get
        {
          return "arrays";
        }
      }

      protected override Arrays.ArrayOptions CreateOptions(ILogOptions options)
      {
        return new ArrayOptions(options);
      }

      public override void SetDependency(IMethodAnalysis analysis)
      {
        if (analysis.Name == "Bounds")
        {
          var boundsAnalysis = analysis as Analyzers.Bounds;
          Contract.Assert(boundsAnalysis != null);

          this.boundsOptions = boundsAnalysis.Options;

          // We do not need to save the numerical analysis, as we will create it aftwards.
        }

        if (analysis.Name == "Non-null")
        {
          this.nonnullAnalysis = analysis as Analyzers.NonNull;
        }

        // TODO: instrad of keeping the boolean, just keep the instance of the analysus
        if (analysis.Name == "Enum")
        {
          this.IsEnumAnalysisSelected = true;
        }
      }

      public override IProofObligations<Variable, BoxedExpression> GetProofObligations<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>
      (
        string fullMethodName, 
        IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> mdriver
      )
      {
        var obl = new ProofObligationComposition<Variable, BoxedExpression>();
        if (!this.boundsOptions[0].NoProofObligations)
        {
          var numObl = new AnalysisWrapper.TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>.BoundsObligations(mdriver, this.boundsOptions[0].NoProofObligations);
          obl.Add(numObl);
        }
        if (this.nonnullAnalysis != null)
        {
          var nnObl = this.nonnullAnalysis.GetProofObligations(fullMethodName, mdriver);
          if (nnObl != null)
          {
            obl.Add(nnObl);
          }
        }
        if (this.IsEnumAnalysisSelected)
        {
          obl.Add(new AnalysisWrapper.TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>.EnumObligations(mdriver, false));
        }

        return obl;
      }

      public override T Instantiate<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, T>
      (
        string methodName,
        IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> mdriver,
        Predicate<APC> cachePCs,
        IMethodAnalysisClientFactory<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, T> factory
      )
      {
        var numericalAnalysis =
          new AnalysisWrapper.TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>.
            NumericalAnalysis<Bounds.BoundsOptions>(methodName, mdriver, this.boundsOptions, cachePCs);

        var nonnullAnalysis =
          this.nonnullAnalysis != null ?
            new Analyzers.NonNull.TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>.
              AnalysisForArrays(mdriver, this.nonnullAnalysis, cachePCs)
          : null;

        var arrayAnalysis =
          new AnalysisWrapper.TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>.
            ArrayAnalysisPlugIn(methodName, mdriver, this.options[0].LogOptions, cachePCs);

        var analysis =
         new AnalysisWrapper.TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>.
           ArrayAnalysis<Analyzers.Arrays.ArrayOptions, Bounds.BoundsOptions>(methodName, arrayAnalysis, numericalAnalysis, nonnullAnalysis, this.IsEnumAnalysisSelected, mdriver, this.options[0], cachePCs);

        return factory.Create(analysis);
      }

      public override IMethodResult<Variable> Analyze<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>
      (
        string fullMethodName, 
        IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> mdriver,
        Predicate<APC> cachePCs
      )
      //where Type : IEquatable<Type>
      //where Expression : IEquatable<Expression>
      //where Variable : IEquatable<Variable>
      {
        return AnalysisWrapper.RunArraysAnalysis(fullMethodName, mdriver, this.options[0], this.boundsOptions, this.nonnullAnalysis, this.IsEnumAnalysisSelected, cachePCs);
      }

      override public bool ExecuteAbstractDomainFunctor<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, Options, Result, Data>(
        IClassDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, Options, IMethodResult<Variable>> cdriver,
        IResultsFunctor<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, Result, Data> functor,
        Data data,
        out Result result)
      //where Variable : IEquatable<Variable>
      //where Expression : IEquatable<Expression>
      //where Type : IEquatable<Type>
      //where Options : IFrameworkLogOptions
      {
        // TODO !!!
        result = default(Result);

        return false;
      }

      public class ArrayOptions
        : ValueAnalysisOptions<ArrayOptions>
      {
        public ArrayOptions(ILogOptions options)
          : base(options)
        {
        }

        public bool InferArrayPurity
        {
          get
          {
            Contract.Ensures(Contract.Result<bool>() == this.arrayPurity || this.LogOptions.SuggestRequiresForArrays);

            return this.arrayPurity || this.LogOptions.SuggestRequiresForArrays || this.LogOptions.SuggestRequiresPurityForArrays;
          }
        }

        public bool RefineArrays
        {
          get
          {
            Contract.Ensures(Contract.Result<bool>() == this.refineArrays || this.LogOptions.SuggestRequiresForArrays);

            return this.refineArrays || this.LogOptions.SuggestRequiresForArrays;
          }
        }

        [OptionDescription("Refine symbolic expressions to array")]
        public bool refineArrays = true;

        [OptionDescription("Infer array segments not written by the method")]
        public bool arrayPurity = false;
      }

    }
  }
}