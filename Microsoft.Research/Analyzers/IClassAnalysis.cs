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
using System.Text;
using Microsoft.Research.DataStructures;
using System.IO;

namespace Microsoft.Research.CodeAnalysis
{
  public interface IClassAnalysis
  {
    string Name { get; }

    /// <summary>
    /// Return false if options or args where wrong and initialization failed
    /// </summary>
    bool Initialize(ILogOptions options, string[] args);

    /// <summary>
    /// Return true if the analysis should be run on a class just after all its constructors
    /// have been analyzed (and after that, all the information can be discarded).
    /// </summary>
    bool ShouldBeCalledAfterConstructors();

    IClassResult<Variable>
      Analyze<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions>
      (
        string fullClassName,
        IClassDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions, IMethodResult<Variable>> cdriver,
        IList<IMethodAnalysis> methodAnalyses
      )
      where Variable : IEquatable<Variable>
      where Expression : IEquatable<Expression>
      where Type : IEquatable<Type>
      where LogOptions : IFrameworkLogOptions;

    IEnumerable<OptionParsing> Options { get; }

    void PrintOptions(string indent, TextWriter output);
    void PrintAnalysisSpecificStatistics(IOutput output);
  }

  public interface IClassResult<Variable>
  {
    void ValidateImplicitAssertions(IOutputResults output);
    AnalysisStatistics Statistics();
    /// <summary>
    /// Get the postcondition for the class.
    /// </summary>    
    void SuggestInvariant (IOutputResults output);
  }
}
