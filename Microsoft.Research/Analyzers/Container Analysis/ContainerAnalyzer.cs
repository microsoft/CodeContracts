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

#if CONTRACTS_EXPERIMENTAL
using System;
using System.Collections.Generic;
using Microsoft.Research.AbstractDomains;
using System.Linq;
using System.Text;

namespace Microsoft.Research.CodeAnalysis
{
  public static partial class Analyzers
  {
    public class Containers
      : ValueAnalyzer<Containers, Containers.ContainerOptions>
    {

      #region Properties (Name)
      
      /// <summary>
      /// The analysis is named "Containers"
      /// </summary>
      override public string Name { get { return "Containers"; } }

      #endregion

      # region Options

      /// <summary>
      /// Class for options specific to containers analysis
      /// </summary>
      public class ContainerOptions
        : ValueAnalysisOptions<ContainerOptions>
      {
        internal ContainerOptions(ILogOptions logoptions)
          : base(logoptions)
        {
        }
      }
      
      /// <summary>
      /// create and initialize the options of the container analysis
      /// </summary>
      override protected Containers.ContainerOptions CreateOptions(ILogOptions options)
      {
        return new ContainerOptions(options);
      }

      #endregion

      public override IMethodResult<Variable> Analyze<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly, Expression, Variable>(
          string fullMethodName,
          IMethodDriver<APC, Local, Parameter, Method, Field, Property, Type, Attribute, Assembly, Expression, Variable, ILogOptions> mdriver)
      {
        throw new AbstractInterpretationException("Containers analysis need a FactQuery: call the overload offering this parameter");
      }
      
      /// <summary>
      /// method calling the analysis of containers, wich is preceded by a partition analysis of containers
      /// </summary>
      public override IMethodResult<Variable> Analyze<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly, Expression, Variable>(
          string fullMethodName,
          IMethodDriver<APC, Local, Parameter, Method, Field, Property, Type, Attribute, Assembly, Expression, Variable, ILogOptions> mdriver,
          IFactQuery<BoxedExpression, Variable> factQuery)
      // where Variable : IEquatable<Variable>
      // where Expression : IEquatable<Expression>
      // where Type : IEquatable<Type>
      {
        //IMethodResult<Variable> partitions = AnalysisWrapper.RunPartitionAnalysis(fullMethodName, mdriver, this.options); //t-maper@54: do I need a different set of options for the partition analyis or I mixed it with the options of Container Analysis?
        
        //return AnalysisWrapper.RunContainerAnalysis(fullMethodName, mdriver, partitions, this.options); //t-maper@54: this.options[0] ?
        
        var partitionsResult = AnalysisWrapper.RunPartitionAnalysis(fullMethodName, mdriver, this.options, factQuery);
        
        var containersResult = AnalysisWrapper.RunContainerAnalysis(fullMethodName, mdriver, partitionsResult, this.options);

        containersResult.MethodAnalysis = this; // Mic: keep track of the analysis that gave the result
        return containersResult;
        //return partitionsResult;
      }

      override public bool ExecuteAbstractDomainFunctor<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly, Expression, Variable, Options, Result, Data>(
        IClassDriver<APC, Local, Parameter, Method, Field, Property, Type, Attribute, Assembly, Expression, Variable, Options, IMethodResult<Variable>> cdriver,
        IResultsFunctor<APC, Local, Parameter, Method, Field, Property, Type, Attribute, Assembly, Expression, Variable, Result, Data> functor,
        Data data,
        out Result result)
          //where Variable : IEquatable<Variable>
          //where Expression : IEquatable<Expression>
          //where Type : IEquatable<Type>
          //where Options : IFrameworkLogOptions
      {
        var aoi = new AnalysisWrapper.TypeBindings<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly, Expression, Variable>.AbstractOperationsImplementationContainer();
        return functor.Execute(aoi, data, out result);
      }
    }
  }
}
#endif
