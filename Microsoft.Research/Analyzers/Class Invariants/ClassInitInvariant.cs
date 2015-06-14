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
using System.IO;

namespace Microsoft.Research.CodeAnalysis
{
  public static partial class Analyzers
  {
    public class ClassInit : IClassAnalysis
    {
      public class ClassInitOptions : OptionParsing
      {
        readonly private ILogOptions mLogOptions; 
        public ClassInitOptions(ILogOptions options)
        {
          mLogOptions = options;
        }
      }

      public virtual ClassInitOptions CreateOptions(ILogOptions options)
      {
        return new ClassInitOptions(options);
      }

      public string Name { get { return "ClassInit"; } }

      public bool Initialize(ILogOptions options, string[] args)
      {
        return true;
      }

      public bool ShouldBeCalledAfterConstructors()
      {
        return true;
      }

      public IClassResult<Variable> Analyze
        <Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions>
      (
        string fullClassName,
        IClassDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions, IMethodResult<Variable>> cdriver,
        IList<IMethodAnalysis> methodAnalyses
      )
        where Variable : IEquatable<Variable>
        where Expression : IEquatable<Expression>
        where Type : IEquatable<Type>
        where LogOptions : IFrameworkLogOptions
      {
        // Mic: TODO: Fix. Ugly cast, but Label is not propagated properly in some other code, so easier for now to use APC instead
        // The Label generic should be also propagated everywhere, but it doesn't appear in TypeBindings, so it's too much of an effort
        // to get it totally right now.
        //var cdriver = cdriver_generic as IClassDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions, IMethodResult<Variable>>;

#if false
        Console.WriteLine("ClassInit.Analyze begins...");

        List<BoxedExpression> result_exprs = new List<BoxedExpression>();
        // For each analysis, compute the invariants
        foreach (var analysis in methodAnalyses)
        {
          string analysis_name = analysis.Name;

          var results = new List<Pair<IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions>, IMethodResult<Variable>>>();

          // For each method, create a pair <MethodDriver, MethodResult> for the given analysis (analysis_name)
          foreach (var constr in cdriver.ConstructorsStatus.AnalysesResults)
            results.Add(new Pair<IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions>, IMethodResult<Variable>>(constr.Value.Driver, constr.Value.Results[analysis_name]));

          // create the functor that will actually run the class analysis
          var ComputeFunctor = new ComputeReadonlyInvariants<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions>();
          List<BoxedExpression> analysis_result_exprs;

          // Get the new invariants
          if (analysis.ExecuteAbstractDomainFunctor(cdriver, ComputeFunctor, results, out analysis_result_exprs))
          {
            if (analysis_result_exprs.Count > 0)
            {
              Console.WriteLine("Analysis {0} successfully inferred some type invariants.", analysis_name);
              // Add them to all the invariants
              result_exprs.AddRange(analysis_result_exprs);
            }
            else
              Console.WriteLine("Analysis {0} tried to infer some type invariants but didn't actually discover anything.", analysis_name);
          }
          else
            Console.WriteLine("Analysis {0} tried to infer some type invariants but FAILED.", analysis_name);

        }

        if (result_exprs.Count > 0)
        {
          Console.WriteLine("ClassInit inferred invariant(s) for class {0}:", OutputPrettyCS.TypeHelper.TypeFullName(cdriver.MetaDataDecoder, cdriver.ClassType));
          foreach (var res in result_exprs)
          {
            Console.WriteLine(res.ToString());
            cdriver.ParentDriver.ContractsHandlerManager.RegisterClassInvariant(cdriver.ClassType, res);
          }
        }
#endif
        return null;
      }

      virtual public void PrintOptions(string indent, TextWriter output)
      {
        var defaultOptions = this.CreateOptions(null);
        defaultOptions.PrintOptions(indent, output);
        defaultOptions.PrintDerivedOptions(indent, output);
      }

      public IEnumerable<OptionParsing> Options { get { return Enumerable.Empty<OptionParsing>(); } }

      virtual public void PrintAnalysisSpecificStatistics(IOutput output)
      {
        // Mic: TODO
      }
    }
  }
}
