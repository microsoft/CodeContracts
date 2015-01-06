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

namespace Microsoft.Research.CodeAnalysis
{

  /// <summary>
  /// Every particular analysis is a nested class within this class. 
  /// We use reflection to find which analysis to run given command line options, as well as 
  /// which constructor to call
  /// </summary>
  public static partial class Analyzers 
  {
    public enum DomainKind
    {
      Intervals,
      Disintervals,
      Leq, Karr,
      Pentagons, PentagonsKarr, PentagonsKarrLeq, PentagonsKarrLeqOctagons,
      SubPolyhedra,
#if POLYHEDRA
      Polyhedra,
#endif
      Octagons,
      WeakUpperBounds,
      Top
    };


    private static readonly Dictionary<string, Func<IMethodAnalysis>> MethodAnalyzerConstructors; // Thread-safe
    private static readonly Dictionary<string, Func<IClassAnalysis>> ClassAnalyzerConstructors; // Thread-safe

    #region Initializer

    static Analyzers()
    {
      MethodAnalyzerConstructors = GatherAnalyzers<IMethodAnalysis>();
      ClassAnalyzerConstructors = GatherAnalyzers<IClassAnalysis>();
    }

    private static Dictionary<string, Func<TAnalysis>> GatherAnalyzers<TAnalysis>()
    {
      var argTypes = new System.Type[] { }; // looking for constructors with zero parameters

      var result = new Dictionary<string, Func<TAnalysis>>();
      var analyzers = typeof(Analyzers);
      var candidates = analyzers.GetNestedTypes();
      foreach (var t in candidates)
      {
        if (!typeof(TAnalysis).IsAssignableFrom(t))
          continue; // only interested in TAnalysis analyses

        var cInfo = t.GetConstructor(argTypes);
        if (cInfo == null)
          continue;
        result.Add(t.Name.ToLower(), () => (TAnalysis)cInfo.Invoke(null));
      }
      return result;
    }

    public static Dictionary<string, IMethodAnalysis> CreateMethodAnalyzers()
    {
      return MethodAnalyzerConstructors.Select(ctor => ctor()).ToDictionary();
    }
    public static Dictionary<string, IClassAnalysis> CreateClassAnalyzers()
    {
      return ClassAnalyzerConstructors.Select(ctor => ctor()).ToDictionary();
    }

    #endregion
  }
}
