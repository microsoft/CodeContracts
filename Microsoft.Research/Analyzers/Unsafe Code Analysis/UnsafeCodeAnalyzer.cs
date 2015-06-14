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
using Microsoft.Research.AbstractDomains;

namespace Microsoft.Research.CodeAnalysis
{
  public interface IOverallUnsafeStatistics
  {
    void AddUncheckableAccesses(int value);
  }

  static partial class Analyzers {

    public enum DomainKind
    {
      Intervals,
      Leq, Karr,
      Pentagons, PentagonsKarr, PentagonsKarrLeq, PentagonsKarrLeqOctagons,
      SubPolyhedra,
#if POLYHEDRA
      Polyhedra,
#endif
      Octagons,
      Stripes, StripesIntervals, StripesIntervalsKarr,
      WeakUpperBounds,
      Top
    };

#if INCLUDE_UNSAFE_ANALYSIS
    public class Unsafe : IMethodAnalysis, IOverallUnsafeStatistics
    {
      public class UnsafeOptions 
        : ValueAnalysisOptions<UnsafeOptions> 
      {
        public UnsafeOptions(ILogOptions logoptions)
          : base(logoptions)
        {
          this.type = DomainKind.StripesIntervalsKarr;
        }

      }

      List<UnsafeOptions> options = new List<UnsafeOptions>();

      /// <summary>
      /// Total number of accesses that were apriori not checkable due to lack of information
      /// </summary>
      int UncheckableObligationsTotal;

      public void AddUncheckableAccesses(int value)
      {
        this.UncheckableObligationsTotal += value;
      }

      #region IMethodAnalysis Members

      public string Name { get { return "Unsafe memory"; } }

      public bool Initialize(ILogOptions logoptions, string[] args)
      {
        var newoptions = new UnsafeOptions(logoptions);
        newoptions.Parse(args);

        this.options.Add(newoptions);
        
        return !newoptions.HasErrors;
      }

      /// <summary>
      ///  Run the analysis for the unsafe code
      /// </summary>
      public IMethodResult<Variable> 
        Analyze<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly, Expression, Variable>(
          string fullMethodName, 
          IMethodDriver<APC, Local, Parameter, Method, Field, Property, Type, Attribute, Assembly, Expression, Variable, ILogOptions> mdriver
        )
        where Variable : IEquatable<Variable>
        where Expression : IEquatable<Expression>
        where Type : IEquatable<Type>
      {
        return AnalysisWrapper.RunTheUnsafeAnalysis(fullMethodName, mdriver, options, this);
      }

      public IMethodResult<Variable>
        Analyze<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly, Expression, Variable>(
          string fullMethodName,
          IMethodDriver<APC, Local, Parameter, Method, Field, Property, Type, Attribute, Assembly, Expression, Variable, ILogOptions> mdriver,
          IFactQuery<BoxedExpression, Variable> factQuery
        )
        where Variable : IEquatable<Variable>
        where Expression : IEquatable<Expression>
        where Type : IEquatable<Type>
      {
        return Analyze(fullMethodName, mdriver);
      }

      public void PrintOptions(string indent)
      {
        UnsafeOptions defaultOptions = new UnsafeOptions(null);
        defaultOptions.PrintOptions(indent);
        defaultOptions.PrintDerivedOptions(indent);
      }

      public void PrintAnalysisSpecificStatistics(IOutput output)
      {
        // output.WriteLine("Total uncheckable unsafe accesses: {0}", UncheckableObligationsTotal.ToString());
      }

      #endregion
    }
#endif
  }

}
