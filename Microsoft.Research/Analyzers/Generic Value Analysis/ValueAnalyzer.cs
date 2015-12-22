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
using Microsoft.Research.AbstractDomains;
using System.Diagnostics.Contracts;
using System.IO;

namespace Microsoft.Research.CodeAnalysis
{
  public static partial class Analyzers
  {
    abstract public class ValueAnalyzer<Analyzer, Options>
      : IMethodAnalysis
      where Options : ValueAnalysisOptions<Options>
    {     

      // The different options to be used to run the bounds analysis
      readonly protected List<Options> options;

      // We keep an hash of the options we've already seen 
      readonly protected List<Int64> hashes;

      [ContractInvariantMethod]
      void ObjectInvariant()
      {
        Contract.Invariant(options != null);
      }

      abstract public string Name { get; }

      public Func<object, int> FailingObligations { get; set; }

      public bool ObligationsEnabled
      {
        get
        {
          if (options != null)
          {
            for (int i = 0; i < options.Count; i++)
            {
              if (!options[i].noObl) return true;
            }
          }
          return false;
        }
      }
      abstract protected Options CreateOptions(ILogOptions options);

      abstract public T Instantiate<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, T>(
        string fullMethodName,
        IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> driver,
        Predicate<APC> cachePCs,
        IMethodAnalysisClientFactory<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, T> factory
      )
        where Variable : IEquatable<Variable>
        where Expression : IEquatable<Expression>
        where Type : IEquatable<Type>;

      abstract public IProofObligations<Variable, BoxedExpression> GetProofObligations<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>
        (
          string fullMethodName,
          IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> mdriver
        )
        where Type : IEquatable<Type>
        where Expression : IEquatable<Expression>
        where Variable : IEquatable<Variable>;

      abstract public IMethodResult<Variable> Analyze<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>(
        string fullMethodName,
        IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> driver,
        Predicate<APC> cachePCs
      )
        where Type : IEquatable<Type>
        where Expression : IEquatable<Expression>
        where Variable : IEquatable<Variable>;

      virtual public IMethodResult<Variable> Analyze<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>(
        string fullMethodName,
        IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> driver,
        Predicate<APC> cachePCs,
        IFactQuery<BoxedExpression, Variable> factQuery
      )
        where Type : IEquatable<Type>
        where Expression : IEquatable<Expression>
        where Variable : IEquatable<Variable>
      {
        return Analyze(fullMethodName, driver, cachePCs);
      }

      protected ValueAnalyzer()
      {
        this.options = new List<Options>();
        this.hashes = new List<Int64>();
      }

      public bool Initialize(ILogOptions logoptions, string[] args)
      {
        var newOptions = this.CreateOptions(logoptions);
        newOptions.Parse(args);

        newOptions.ForceValueOfOverriddenArguments(this.options);

        var optionshash = newOptions.GetCheckCode();
        if (!this.hashes.Contains(optionshash))
        {
          if (this.options.Count > 0)
          {
            // Disable obligation collecting
            newOptions.noObl = true;

            // Disable disequalities
            newOptions.diseq = false;
          }
          this.options.Add(newOptions);
          this.hashes.Add(optionshash);
        }
        return !newOptions.HasErrors;
      }

      public void PrintOptions(string indent, TextWriter output)
      {
        var defaultOptions = this.CreateOptions(null);
        defaultOptions.PrintOptions(indent, output);
        defaultOptions.PrintDerivedOptions(indent, output);
      }

      virtual public void PrintAnalysisSpecificStatistics(IOutput output)
      {
        // does nothing
      }

      virtual public void SetDependency(IMethodAnalysis analysis)
      {
        // do nothing
      }

      public abstract bool ExecuteAbstractDomainFunctor<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, Opts, Result, Data>(
        IClassDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, Opts, IMethodResult<Variable>> cdriver,
        IResultsFunctor<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, Result, Data> functor,
        Data data,
        out Result result)
        where Variable : IEquatable<Variable>
        where Expression : IEquatable<Expression>
        where Type : IEquatable<Type>
        where Opts : IFrameworkLogOptions;

      IEnumerable<OptionParsing> IMethodAnalysis.Options { get { return this.options.AsEnumerable().Cast<OptionParsing>(); } }

    }
  }
}
