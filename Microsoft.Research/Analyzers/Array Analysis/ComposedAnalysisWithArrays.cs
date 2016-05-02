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

#define TRACEPERFORMANCE
// The array analysis

using System;
using Microsoft.Research.AbstractDomains;
using System.Collections.Generic;
using Microsoft.Research.DataStructures;
using Microsoft.Research.AbstractDomains.Numerical;

using System.Diagnostics.Contracts;

using Microsoft.Research.CodeAnalysis;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Microsoft.Research.CodeAnalysis
{
  public static partial class AnalysisWrapper
  {

    /// <summary>
    /// Entry point to run the Arrays analysis
    /// </summary>
    public static IMethodResult<Variable> RunArraysAnalysis<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, NumericalOptions>
    (
      string methodName,
      IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> driver,
      Analyzers.Arrays.ArrayOptions options,
      List<NumericalOptions> numericaloptions,
      Analyzers.NonNull nonnull,
      bool isEnumAnalysisSelected,
      Predicate<APC> cachePCs, DFAController controller
    )
      where Variable : IEquatable<Variable>
      where Expression : IEquatable<Expression>
      where Type : IEquatable<Type>
      where NumericalOptions : Analyzers.ValueAnalysisOptions<NumericalOptions>
    {
      var numericalAnalysis =
        new TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>.
          NumericalAnalysis<NumericalOptions>(methodName, driver, numericaloptions, cachePCs, controller);

      var nonnullAnalysis =
        nonnull != null ?
          new Analyzers.NonNull.TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>.
            AnalysisForArrays(driver, nonnull, cachePCs)
        : null;

      var arrayAnalysis = 
        new TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>.
          ArrayAnalysisPlugIn(methodName, driver, options.LogOptions, cachePCs);

      var analysis =
       new TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>.
         ArrayAnalysis<Analyzers.Arrays.ArrayOptions, NumericalOptions>(methodName, arrayAnalysis, numericalAnalysis, nonnullAnalysis, isEnumAnalysisSelected, driver, options, cachePCs);

      return TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>.RunTheAnalysis(methodName, driver, analysis, controller);
    }

    public static partial class TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>
      where Variable : IEquatable<Variable>
      where Expression : IEquatable<Expression>
      where Type : IEquatable<Type>
    {
      [ContractVerification(false)]
      public class ArrayAnalysis<AnalysisOptions, NumericalOptions> :
        GenericValueAnalysis<ArrayState, AnalysisOptions>
        where AnalysisOptions : Analyzers.Arrays.ArrayOptions
        where NumericalOptions : Analyzers.ValueAnalysisOptions<NumericalOptions>
      {
        #region Object Invariant
        [ContractInvariantMethod]
        void ObjectInvariant()
        {
          Contract.Invariant(AnalysisDependencies != null);
          Contract.Invariant(this.numericalAnalysis != null);
          Contract.Invariant(this.arrayAnalysis != null);
          Contract.Invariant(this.analysisMapping != null);
          Contract.Invariant(this.additionalAnalyses != null);
          Contract.Invariant(this.pluginCount >= 0);
        }

        #endregion

        #region State
        readonly private int pluginCount;

        readonly private /*Dictionary<ArrayState.AdditionalStates, int>*/ int[] analysisMapping;

        readonly private ArrayAnalysisPlugIn arrayAnalysis;

        readonly private GenericNumericalAnalysis<NumericalOptions>
          numericalAnalysis;

        readonly private Analyzers.NonNull.TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>.AnalysisForArrays
          nonnullAnalysis;

        readonly private GenericPlugInAnalysisForComposedAnalysis[]  // Made an array for performance
          additionalAnalyses;

        #endregion

        #region Analysis dependencies

        // For each plugin-analysis, the list of the analyses it depends on
        // F: prefer to make it a global table, instead of an analysis property to have a nicer understanding of the dependencies
        private static readonly Dictionary<System.Type, System.Type[]> AnalysisDependencies; // Thread-safe

        // Set up the dependencies
        static ArrayAnalysis()
        {
          // TODO: Instead of declaring them statically, let each plugin register its dependencies

          AnalysisDependencies = new Dictionary<System.Type, System.Type[]>();

          AnalysisDependencies[typeof(EnumAnalysisWrapperPlugIn)] = new System.Type[] { };
          AnalysisDependencies[typeof(RuntimeTypesPlugIn)] = new System.Type[] { };
          AnalysisDependencies[typeof(ArrayExpressionRefinementPlugIn)] = new System.Type[] { };
          AnalysisDependencies[typeof(ArrayPurityAnalysisPlugIn)] = new System.Type[] { };
          AnalysisDependencies[typeof(ExistentialAnalysisPlugIn)] = new System.Type[] { typeof(ArrayPurityAnalysisPlugIn) };
          AnalysisDependencies[typeof(ArrayElementsCheckedAnalysisPlugin)] = new System.Type[] { typeof(ArrayPurityAnalysisPlugIn), typeof(ArrayExpressionRefinementPlugIn) };
        }

        #endregion

        #region Constructors
        public ArrayAnalysis(
          string methodname,
           ArrayAnalysisPlugIn arrayAnalysis,
          GenericNumericalAnalysis<NumericalOptions> numericalAnalysis,
          IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> mdriver,
          AnalysisOptions options,
          Predicate<APC> cachePCs
        )
          : this(methodname, arrayAnalysis, numericalAnalysis, null, false, mdriver, options, cachePCs)
        {
          Contract.Requires(options != null);
        }

        public ArrayAnalysis(
          string methodname,
         ArrayAnalysisPlugIn arrayAnalysis,
        GenericNumericalAnalysis<NumericalOptions> numericalAnalysis,
         Analyzers.NonNull.TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>.AnalysisForArrays nonnullAnalysis,
          bool isEnumAnalysisSelected,
          IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> mdriver,
          AnalysisOptions options,
          Predicate<APC> cachePCs
        )
          : base(methodname, mdriver, options, cachePCs)
        {
          Contract.Requires(options != null);

          this.arrayAnalysis = arrayAnalysis;
          this.numericalAnalysis = numericalAnalysis;
          this.nonnullAnalysis = nonnullAnalysis;


          this.analysisMapping = new int[ArrayState.AdditionalStatesCount];
          this.additionalAnalyses = SetUpAdditionalAnalyses(out this.pluginCount, isEnumAnalysisSelected, cachePCs).ToArray();

          // The analyses should share the cache, to avoid a memory blowup
          var cacheManager = this.cacheManager;
          Contract.Assume(cacheManager != null);
          this.numericalAnalysis.ShareCachesWith(cacheManager);
          this.arrayAnalysis.ShareCachesWith(cacheManager);

          //this.additionalAnalyses.ForEach(analysis => analysis.ShareCachesWith(cacheManager));
          Array.ForEach(this.additionalAnalyses, analysis => analysis.ShareCachesWith(cacheManager));
        }

        // Made pluginCount an out parameter so that we can be sure that we assign it only in the constructor
        private List<GenericPlugInAnalysisForComposedAnalysis> SetUpAdditionalAnalyses(out int pluginCount, bool isEnumAnalysisSelected, Predicate<APC> cachePCs)
        {
          Contract.Ensures(Contract.Result<List<GenericPlugInAnalysisForComposedAnalysis>>() != null);

          pluginCount = 0;
          var result = new List<GenericPlugInAnalysisForComposedAnalysis>();

          // Here we add all the selected plugin analyses

          this.AddAnalysisAndDependencies(
            new RuntimeTypesPlugIn(pluginCount, this.MethodName, this.MethodDriver, this.Options.LogOptions, cachePCs),
            result, cachePCs, ref pluginCount);

          if (isEnumAnalysisSelected)
          {
            var enumAnalysis = new EnumAnalysis(this.MethodName, this.MethodDriver, new Analyzers.Enum.Options(this.Options.LogOptions), cachePCs);

            this.AddAnalysisAndDependencies(
              new EnumAnalysisWrapperPlugIn(enumAnalysis, pluginCount, this.MethodName, this.MethodDriver, this.Options.LogOptions, cachePCs),
              result, cachePCs, ref pluginCount);
          }

          if (this.Options.RefineArrays)
          {
            this.AddAnalysisAndDependencies(
              new ArrayExpressionRefinementPlugIn(pluginCount, this.MethodName, this.MethodDriver, this.Options.LogOptions, cachePCs),
              result, cachePCs, ref pluginCount);
          }

          if (this.Options.InferArrayPurity)
          {
            this.AddAnalysisAndDependencies(
              new ArrayPurityAnalysisPlugIn(pluginCount, this.MethodName, this.MethodDriver, this.Options.LogOptions, cachePCs),
              result, cachePCs, ref pluginCount);
          }

          if (this.Options.LogOptions.CheckExistentials)
          {
            this.AddAnalysisAndDependencies(
              new ExistentialAnalysisPlugIn(pluginCount, this.MethodName, this.MethodDriver, this.Options.LogOptions, cachePCs),
              result, cachePCs, ref pluginCount);
          }

          if (this.Options.LogOptions.SuggestRequiresForArrays)
          {
            this.AddAnalysisAndDependencies(
              new ArrayElementsCheckedAnalysisPlugin(pluginCount, this.MethodName, MethodDriver, this.Options.LogOptions, cachePCs),
              result, cachePCs, ref pluginCount);
          }

          return result;
        }

        private void AddAnalysisAndDependencies
        (
          GenericPlugInAnalysisForComposedAnalysis analysis,
          List<GenericPlugInAnalysisForComposedAnalysis> analyses,
          Predicate<APC> cachePCs,
          ref int id
        )
        {
          Contract.Requires(analysis != null);
          Contract.Requires(analyses != null);
          Contract.Requires(id >= 0);

          Contract.Ensures(id >= Contract.OldValue(id));


          // Add the analysis - we do the explicit check for robustness 
          if (!analyses.Exists(prev => prev.GetType().Equals(analysis.GetType())))
          {
            analyses.Add(analysis);
            this.analysisMapping[(int)analysis.Kind] = id++;
          }
          else
          {
            return;
          }
          // Now add the dependencies if not present
          // This uses reflection and can be very slow. 
          // So the best thing is to specify the dependencies directly on the command line
          foreach (var dependencies in AnalysisDependencies[analysis.GetType()])
          {
            if (!analyses.Exists(prev => prev.GetType().Equals(dependencies)))
            {
              try
              {
                Contract.Assume(dependencies != null, "Assumption from the environment");
                var newInstance = (GenericPlugInAnalysisForComposedAnalysis)
                  Activator.CreateInstance(dependencies, id, this.MethodName, this.MethodDriver, this.Options.LogOptions, cachePCs);
                AddAnalysisAndDependencies(newInstance, analyses, cachePCs, ref id);
              }
              catch (System.MissingMethodException e)
              {
                Console.WriteLine("A problem occurred with the initialization of the plugin analysis: {0}", e.Message);
                return;
              }
            }
          }
        }
        #endregion

        #region Transfer functions

        public override ArrayState
          Arglist(APC pc, Variable dest, ArrayState data)
        {
          var numerical = this.numericalAnalysis.Arglist(pc, dest, data.Numerical); Contract.Assume(numerical != null);
          var resultState = this.arrayAnalysis.Arglist(pc, dest, data.UpdateNumerical(numerical));

          Contract.Assume(numerical != null);

          if (this.nonnullAnalysis != null)
          {
            var nonnull = this.nonnullAnalysis.Arglist(pc, dest, resultState.NonNull);

            resultState = resultState.UpdateNonNull(nonnull);
          }

          foreach (var additionalAnalysis in this.additionalAnalyses)
          {
            resultState = additionalAnalysis.Arglist(pc, dest, resultState);
          }

          return resultState;
        }

        public override ArrayState
          Assert(APC pc, string tag, Variable condition, object provenance, ArrayState data)
        {
          var numerical = this.numericalAnalysis.Assert(pc, tag, condition, provenance, data.Numerical);
          Contract.Assume(numerical != null);

          var resultState = data.UpdateNumerical(numerical);

#if DEBUG
          data = null; // F: just for debugging - data should not be used from here
#endif

          if (this.nonnullAnalysis != null)
          {
            var nonnull = this.nonnullAnalysis.Assert(pc, tag, condition, provenance, resultState.NonNull);

            if (this.nonnullAnalysis.IsBottom(pc, nonnull))
            {
              return this.GetBottomValue();
            }

            resultState = resultState.UpdateNonNull(nonnull);
          }

          resultState = this.arrayAnalysis.Assert(pc, tag, condition, provenance, resultState);

          foreach (var additionalAnalysis in this.additionalAnalyses)
          {
            resultState = additionalAnalysis.Assert(pc, tag, condition, provenance, resultState);
          }

          return resultState;
        }

        public override ArrayState
          Assume(APC pc, string tag, Variable source, object provenance, ArrayState data)
        {
          // We first perform numerical and nonnull to get more information 

          var numerical = this.numericalAnalysis.Assume(pc, tag, source, provenance, data.Numerical);
          Contract.Assume(numerical != null);

          var resultState = data.UpdateNumerical(numerical);

          if (this.nonnullAnalysis != null)
          {
            var nonnull = this.nonnullAnalysis.Assume(pc, tag, source, provenance, resultState.NonNull);

            if (this.nonnullAnalysis.IsBottom(pc, nonnull))
            {
              return this.GetBottomValue();
            }

            resultState = resultState.UpdateNonNull(nonnull);
          }

          resultState = this.arrayAnalysis.Assume(pc, tag, source, provenance, resultState);

          foreach (var additionalAnalysis in this.additionalAnalyses)
          {
            resultState = additionalAnalysis.Assume(pc, tag, source, provenance, resultState);

            if (resultState.IsBottom)
              return resultState;
          }

          return RefineDisjunctionForQuantifier(pc, tag, source, data, resultState);
        }

        private ArrayState RefineDisjunctionForQuantifier(APC pc, string tag, Variable source, ArrayState data, ArrayState resultState)
        {
          if (this.MethodDriver.DisjunctiveExpressionRefiner != null)
          {
            Log("Trying to use modus ponens to push information to the other abstract domains");

            List<BoxedExpression> consequences;

            var convertedExp = BoxedExpression.Convert(this.Context.ExpressionContext.Refine(pc, source), this.MethodDriver.ExpressionDecoder);
            if (convertedExp != null)
            {
              if (tag != "false")
              {
                convertedExp = BoxedExpression.UnaryLogicalNot(convertedExp);
              }

              if (this.MethodDriver.DisjunctiveExpressionRefiner.TryApplyModusPonens(this.Context.MethodContext.CFG.Post(pc),
                convertedExp, GetSimpleDecisionProcedure(data.Numerical), out consequences))
              {
                Log("Discovered {0} new consequences. Pushing quantifiers to the array domain, if any", consequences.Count.ToString);

                foreach (var consequence in consequences)
                {
                  Contract.Assume(consequence != null, "Weakness of Clousot in handling of enumerators");

                  if (consequence.IsQuantified)
                  {
                    var newArrayState = this.arrayAnalysis.AssumeForAll(pc, consequence as ForAllIndexedExpression, resultState);
                    resultState = newArrayState;
                  }
                }
              }
            }
          }

          return resultState;
        }

        public override ArrayState BeginOld(APC pc, APC matchingEnd, ArrayState data)
        {
          var numerical = this.numericalAnalysis.BeginOld(pc, matchingEnd, data.Numerical); Contract.Assume(numerical != null);
          var resultState = this.arrayAnalysis.BeginOld(pc, matchingEnd, data.UpdateNumerical(numerical));

          if (this.nonnullAnalysis != null)
          {
            var nonnull = this.nonnullAnalysis.BeginOld(pc, matchingEnd, resultState.NonNull);

            resultState = resultState.UpdateNonNull(nonnull);
          }

          foreach (var additionalAnalysis in this.additionalAnalyses)
          {
            resultState = additionalAnalysis.BeginOld(pc, matchingEnd, resultState);
          }

          return resultState;
        }

        public override ArrayState Binary(APC pc, BinaryOperator op, Variable dest, Variable s1, Variable s2, ArrayState data)
        {
          var numerical = this.numericalAnalysis.Binary(pc, op, dest, s1, s2, data.Numerical); Contract.Assume(numerical != null);
          var resultState = this.arrayAnalysis.Binary(pc, op, dest, s1, s2, data.UpdateNumerical(numerical));

          if (this.nonnullAnalysis != null)
          {
            var nonnull = this.nonnullAnalysis.Binary(pc, op, dest, s1, s2, resultState.NonNull);

            resultState = resultState.UpdateNonNull(nonnull);
          }

          foreach (var additionalAnalysis in this.additionalAnalyses)
          {
            resultState = additionalAnalysis.Binary(pc, op, dest, s1, s2, resultState);
          }

          return resultState;
        }

        public override ArrayState Box(APC pc, Type type, Variable dest, Variable source, ArrayState data)
        {
          var numerical = this.numericalAnalysis.Box(pc, type, dest, source, data.Numerical); Contract.Assume(numerical != null);
          var resultState = this.arrayAnalysis.Box(pc, type, dest, source, data.UpdateNumerical(numerical));

          if (this.nonnullAnalysis != null)
          {
            var nonnull = this.nonnullAnalysis.Box(pc, type, dest, source, resultState.NonNull);
            resultState = resultState.UpdateNonNull(nonnull);
          }

          foreach (var additionalAnalysis in this.additionalAnalyses)
          {
            resultState = additionalAnalysis.Box(pc, type, dest, source, resultState);
          }

          return resultState;
        }

        public override ArrayState Branch(APC pc, APC target, bool leave, ArrayState data)
        {
          var numerical = this.numericalAnalysis.Branch(pc, target, leave, data.Numerical); Contract.Assume(numerical != null);
          var resultState = this.arrayAnalysis.Branch(pc, target, leave, data.UpdateNumerical(numerical));

          if (this.nonnullAnalysis != null)
          {
            var nonnull = this.nonnullAnalysis.Branch(pc, target, leave, resultState.NonNull);
            resultState = resultState.UpdateNonNull(nonnull);
          }

          foreach (var additionalAnalysis in this.additionalAnalyses)
          {
            resultState = additionalAnalysis.Branch(pc, target, leave, resultState);
          }

          return resultState;
        }

        public override ArrayState BranchCond
          (APC pc, APC target, BranchOperator bop, Variable value1, Variable value2, ArrayState data)
        {
          var numerical = this.numericalAnalysis.BranchCond(pc, target, bop, value1, value2, data.Numerical); Contract.Assume(numerical != null);
          var resultState = this.arrayAnalysis.BranchCond(pc, target, bop, value1, value2, data.UpdateNumerical(numerical));

          if (this.nonnullAnalysis != null)
          {
            var nonnull = this.nonnullAnalysis.BranchCond(pc, target, bop, value1, value2, resultState.NonNull);
            resultState = resultState.UpdateNonNull(nonnull);
          }

          foreach (var additionalAnalysis in this.additionalAnalyses)
          {
            resultState = additionalAnalysis.BranchCond(pc, target, bop, value1, value2, resultState);
          }

          return resultState;
        }

        public override ArrayState BranchFalse
          (APC pc, APC target, Variable cond, ArrayState data)
        {
          var numerical = this.numericalAnalysis.BranchFalse(pc, target, cond, data.Numerical); Contract.Assume(numerical != null);
          var resultState = this.arrayAnalysis.BranchFalse(pc, target, cond, data.UpdateNumerical(numerical));

          if (this.nonnullAnalysis != null)
          {
            var nonnull = this.nonnullAnalysis.BranchFalse(pc, target, cond, resultState.NonNull);
            resultState = resultState.UpdateNonNull(nonnull);
          }

          foreach (var additionalAnalysis in this.additionalAnalyses)
          {
            resultState = additionalAnalysis.BranchFalse(pc, target, cond, resultState);
          }

          return resultState;
        }

        public override ArrayState BranchTrue(
          APC pc, APC target, Variable cond, ArrayState data)
        {
          var numerical = this.numericalAnalysis.BranchTrue(pc, target, cond, data.Numerical); Contract.Assume(numerical != null);
          var resultState = this.arrayAnalysis.BranchTrue(pc, target, cond, data.UpdateNumerical(numerical));

          if (this.nonnullAnalysis != null)
          {
            var nonnull = this.nonnullAnalysis.BranchTrue(pc, target, cond, resultState.NonNull);
            resultState = resultState.UpdateNonNull(nonnull);
          }


          foreach (var additionalAnalysis in this.additionalAnalyses)
          {
            resultState = additionalAnalysis.BranchTrue(pc, target, cond, resultState);
          }


          return resultState;
        }

        public override ArrayState Break(APC pc, ArrayState data)
        {
          var numerical = this.numericalAnalysis.Break(pc, data.Numerical); Contract.Assume(numerical != null);
          var resultState = this.arrayAnalysis.Break(pc, data.UpdateNumerical(numerical));

          if (this.nonnullAnalysis != null)
          {
            var nonnull = this.nonnullAnalysis.Break(pc, resultState.NonNull);
            resultState = resultState.UpdateNonNull(nonnull);
          }


          foreach (var additionalAnalysis in this.additionalAnalyses)
          {
            resultState = additionalAnalysis.Break(pc, resultState);
          }

          return resultState;
        }

        public override ArrayState
          Call<TypeList, ArgList>(APC pc, Method method, bool tail, bool virt, TypeList extraVarargs, Variable dest, ArgList args, ArrayState data)
        //where TypeList : IIndexable<Type>
        // where ArgList : IIndexable<Variable>
        {
          var numerical = this.numericalAnalysis.Call(pc, method, tail, virt, extraVarargs, dest, args, data.Numerical); Contract.Assume(numerical != null);
          var resultState = this.arrayAnalysis.Call(pc, method, tail, virt, extraVarargs, dest, args, data.UpdateNumerical(numerical));

          if (this.nonnullAnalysis != null)
          {
            var nonnull = this.nonnullAnalysis.Call(pc, method, tail, virt, extraVarargs, dest, args, resultState.NonNull);
            resultState = resultState.UpdateNonNull(nonnull);
          }

          foreach (var additionalAnalysis in this.additionalAnalyses)
          {
            resultState = additionalAnalysis.Call(pc, method, tail, virt, extraVarargs, dest, args, resultState);
          }

          return resultState;
        }

        public override ArrayState Calli<TypeList, ArgList>
          (APC pc, Type returnType, TypeList argTypes, bool tail, bool isInstance,
          Variable dest, Variable fp, ArgList args, ArrayState data)
        // where TypeList : IIndexable<Type>
        {
          var numerical = this.numericalAnalysis.Calli(pc, returnType, argTypes, tail, isInstance, dest, fp, args, data.Numerical); Contract.Assume(numerical != null);
          var resultState = this.arrayAnalysis.Calli(pc, returnType, argTypes, tail, isInstance, dest, fp, args, data.UpdateNumerical(numerical));

          if (this.nonnullAnalysis != null)
          {
            var nonnull = this.nonnullAnalysis.Calli(pc, returnType, argTypes, tail, isInstance, dest, fp, args, resultState.NonNull);

            resultState = resultState.UpdateNonNull(nonnull);
          }

          foreach (var additionalAnalysis in this.additionalAnalyses)
          {
            resultState = additionalAnalysis.Calli(pc, returnType, argTypes, tail, isInstance, dest, fp, args, resultState);
          }

          // TODO: we want to add a string analysis, and then propagate the information to this analysis to the others

          return resultState;
        }

        public override ArrayState Castclass
          (APC pc, Type type, Variable dest, Variable obj, ArrayState data)
        {
          var numerical = this.numericalAnalysis.Castclass(pc, type, dest, obj, data.Numerical); Contract.Assume(numerical != null);
          var resultState = this.arrayAnalysis.Castclass(pc, type, dest, obj, data.UpdateNumerical(numerical));

          if (this.nonnullAnalysis != null)
          {
            var nonnull = this.nonnullAnalysis.Castclass(pc, type, dest, obj, resultState.NonNull);
            resultState = resultState.UpdateNonNull(nonnull);
          }
          foreach (var additionalAnalysis in this.additionalAnalyses)
          {
            resultState = additionalAnalysis.Castclass(pc, type, dest, obj, resultState);
          }

          return resultState;
        }

        public override ArrayState Ckfinite
          (APC pc, Variable dest, Variable source, ArrayState data)
        {
          var numerical = this.numericalAnalysis.Ckfinite(pc, dest, source, data.Numerical); Contract.Assume(numerical != null);
          var resultState = this.arrayAnalysis.Ckfinite(pc, dest, source, data.UpdateNumerical(numerical));

          if (this.nonnullAnalysis != null)
          {
            var nonnull = this.nonnullAnalysis.Ckfinite(pc, dest, source, resultState.NonNull);
            resultState = resultState.UpdateNonNull(nonnull);
          }

          foreach (var additionalAnalysis in this.additionalAnalyses)
          {
            resultState = additionalAnalysis.Ckfinite(pc, dest, source, resultState);
          }

          return resultState;
        }

        public override ArrayState ConstrainedCallvirt<TypeList, ArgList>
          (APC pc, Method method, bool tail, Type constraint, TypeList extraVarargs, Variable dest, ArgList args, ArrayState data)
        {
          var numerical = this.numericalAnalysis.ConstrainedCallvirt(pc, method, tail, constraint, extraVarargs, dest, args, data.Numerical); Contract.Assume(numerical != null);
          var resultState = this.arrayAnalysis.ConstrainedCallvirt(pc, method, tail, constraint, extraVarargs, dest, args, data.UpdateNumerical(numerical));

          if (this.nonnullAnalysis != null)
          {
            var nonnull = this.nonnullAnalysis.ConstrainedCallvirt(pc, method, tail, constraint, extraVarargs, dest, args, resultState.NonNull);
            resultState = resultState.UpdateNonNull(nonnull);
          }

          foreach (var additionalAnalysis in this.additionalAnalyses)
          {
            resultState = additionalAnalysis.ConstrainedCallvirt(pc, method, tail, constraint, extraVarargs, dest, args, resultState);
          }

          return resultState;
        }

        public override ArrayState Cpblk(APC pc, bool @volatile, Variable destaddr, Variable srcaddr, Variable len, ArrayState data)
        {
          var numerical = this.numericalAnalysis.Cpblk(pc, @volatile, destaddr, srcaddr, len, data.Numerical); Contract.Assume(numerical != null);
          var resultState = this.arrayAnalysis.Cpblk(pc, @volatile, destaddr, srcaddr, len, data.UpdateNumerical(numerical));

          if (this.nonnullAnalysis != null)
          {
            var nonnull = this.nonnullAnalysis.Cpblk(pc, @volatile, destaddr, srcaddr, len, resultState.NonNull);
            resultState = resultState.UpdateNonNull(nonnull);
          }
          foreach (var additionalAnalysis in this.additionalAnalyses)
          {
            resultState = additionalAnalysis.Cpblk(pc, @volatile, destaddr, srcaddr, len, resultState);
          }

          return resultState;
        }
        public override ArrayState Cpobj(APC pc, Type type, Variable destptr, Variable srcptr, ArrayState data)
        {
          var numerical = this.numericalAnalysis.Cpobj(pc, type, destptr, srcptr, data.Numerical); Contract.Assume(numerical != null);
          var resultState = this.arrayAnalysis.Cpobj(pc, type, destptr, srcptr, data.UpdateNumerical(numerical));

          if (this.nonnullAnalysis != null)
          {
            var nonnull = this.nonnullAnalysis.Castclass(pc, type, destptr, srcptr, resultState.NonNull);
            resultState = resultState.UpdateNonNull(nonnull);
          }

          foreach (var additionalAnalysis in this.additionalAnalyses)
          {
            resultState = additionalAnalysis.Castclass(pc, type, destptr, srcptr, resultState);
          }

          return resultState;
        }

        public override ArrayState Endfilter(APC pc, Variable decision, ArrayState data)
        {
          var numerical = this.numericalAnalysis.Endfilter(pc, decision, data.Numerical); Contract.Assume(numerical != null);
          var resultState = this.arrayAnalysis.Endfilter(pc, decision, data.UpdateNumerical(numerical));

          if (this.nonnullAnalysis != null)
          {
            var nonnull = this.nonnullAnalysis.Endfilter(pc, decision, resultState.NonNull);
            resultState = resultState.UpdateNonNull(nonnull);
          }

          foreach (var additionalAnalysis in this.additionalAnalyses)
          {
            resultState = additionalAnalysis.Endfilter(pc, decision, resultState);
          }

          return resultState;
        }

        public override ArrayState Endfinally(APC pc, ArrayState data)
        {
          var numerical = this.numericalAnalysis.Endfinally(pc, data.Numerical); Contract.Assume(numerical != null);
          var resultState = this.arrayAnalysis.Endfinally(pc, data.UpdateNumerical(numerical));

          if (this.nonnullAnalysis != null)
          {
            var nonnull = this.nonnullAnalysis.Endfinally(pc, resultState.NonNull);
            resultState = resultState.UpdateNonNull(nonnull);
          }

          foreach (var additionalAnalysis in this.additionalAnalyses)
          {
            resultState = additionalAnalysis.Endfinally(pc, resultState);
          }

          return resultState;
        }

        public override ArrayState EndOld
          (APC pc, APC matchingBegin, Type type, Variable dest, Variable source, ArrayState data)
        {
          var numerical = this.numericalAnalysis.EndOld(pc, matchingBegin, type, dest, source, data.Numerical); Contract.Assume(numerical != null);
          var resultState = this.arrayAnalysis.EndOld(pc, matchingBegin, type, dest, source, data.UpdateNumerical(numerical));

          if (this.nonnullAnalysis != null)
          {
            var nonnull = this.nonnullAnalysis.EndOld(pc, matchingBegin, type, dest, source, resultState.NonNull);
            resultState = resultState.UpdateNonNull(nonnull);
          }

          foreach (var additionalAnalysis in this.additionalAnalyses)
          {
            resultState = additionalAnalysis.EndOld(pc, matchingBegin, type, dest, source, resultState);
          }

          return resultState;
        }

        public override ArrayState
          Entry(APC pc, Method method, ArrayState data)
        {
          var numerical = this.numericalAnalysis.Entry(pc, method, data.Numerical); Contract.Assume(numerical != null);
          var resultState = this.arrayAnalysis.Entry(pc, method, data.UpdateNumerical(numerical));

          if (this.nonnullAnalysis != null)
          {
            var nonnull = this.nonnullAnalysis.Entry(pc, method, resultState.NonNull);
            resultState = resultState.UpdateNonNull(nonnull);
          }

          foreach (var additionalAnalysis in this.additionalAnalyses)
          {
            resultState = additionalAnalysis.Entry(pc, method, resultState);
          }

          return resultState;
        }

        public override ArrayState
          EdgeConversion(APC from, APC to, bool isJoin, IFunctionalMap<Variable, FList<Variable>> sourceTargetMap, ArrayState state)
        {
          if (sourceTargetMap == null) return state;


#if true 
          // The parallel version seems slower
          ArrayState renamed;

          // Causes the invocation of HelperForAssignInParallel, also for the plugin analyses
          renamed = base.EdgeConversion(from, to, isJoin, sourceTargetMap, state);

          if (this.nonnullAnalysis != null)
          {
            Contract.Assume(state.HasNonNullInfo);

            var nonnull = this.nonnullAnalysis.EdgeConversion(from, to, isJoin, sourceTargetMap, state.NonNull);

            renamed = renamed.UpdateNonNull(nonnull);
          }

          return renamed;
#else

          if (this.nonnullAnalysis != null)
          {
            Contract.Assume(state.HasNonNullInfo);
            ArrayState renamed = default(ArrayState);

            // Causes the invocation of HelperForAssignInParallel, also for the plugin analyses
            var numericalState = Task.Run(() => renamed = base.EdgeConversion(from, to, isJoin, sourceTargetMap, state));

            Analyzers.NonNull.TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>.Domain nonnull = default(Analyzers.NonNull.TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>.Domain);

            var nnState = Task.Run(() => nonnull = this.nonnullAnalysis.EdgeConversion(from, to, isJoin, sourceTargetMap, state.NonNull));

            Task.WaitAll(numericalState, nnState);

            renamed = renamed.UpdateNonNull(nonnull);

            return renamed;
          }
          else
          {
            return base.EdgeConversion(from, to, isJoin, sourceTargetMap, state);
          }
#endif
        }

        [ThreadStatic]
        private static int count = 0;

        public override ArrayState
          HelperForAssignInParallel(
          ArrayState state,
          Pair<APC, APC> edge, Dictionary<BoxedVariable<Variable>, FList<BoxedVariable<Variable>>> refinedMap,
          Converter<BoxedVariable<Variable>, BoxedExpression> convert)
        {
          if (ArrayOptions.Trace)
          {
            Console.WriteLine("Assign in parallel #{0}", count++);
          }

          return PerformanceMeasure.Measure(PerformanceMeasure.ActionTags.ArraysAssignInParallel, () => HelperForAssignInParallelInternal(state, ref edge, refinedMap, convert), true);
        }

        private ArrayState HelperForAssignInParallelInternal(ArrayState state, ref Pair<APC, APC> edge, Dictionary<BoxedVariable<Variable>, FList<BoxedVariable<Variable>>> refinedMap, Converter<BoxedVariable<Variable>, BoxedExpression> convert)
        {
          // F: the duplication below is because the Renaming for numerical states works with side effects
          // this is pretty ugly and I should change it 

           var refinedMapForNumerical = SimplifyRefinedMap(ref edge, refinedMap);

          //var refinedMapForNumerical = refinedMap;

          var numerical = this.numericalAnalysis.HelperForAssignInParallel(
            state.Numerical.Clone() as INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression>,
            edge, refinedMapForNumerical, convert);

          Contract.Assume(numerical != null);

          // ** Get the constants in the *post* state
          var intConstants = numerical.IntConstants;

          var pairs = new List<Pair<NormalizedExpression<BoxedVariable<Variable>>, NormalizedExpression<BoxedVariable<Variable>>>>();
          foreach (var pair in intConstants)
          {
            var v = NormalizedExpression<BoxedVariable<Variable>>.For(pair.One);
            var k = NormalizedExpression<BoxedVariable<Variable>>.For(pair.Two);

            pairs.Add(v, k);
          }
          // ** end

          var array = this.arrayAnalysis.AssignInParallel(refinedMap, convert, pairs, state) as ArraySegmentationEnvironment<NonRelationalValueAbstraction<BoxedVariable<Variable>, BoxedExpression>, BoxedVariable<Variable>, BoxedExpression>;

          Contract.Assume(array != null);

          var renamedPlugins = new IAbstractDomainForEnvironments<BoxedVariable<Variable>, BoxedExpression>[state.PluginsCount];

          for (var i = 0; i < state.PluginsCount; i++)
          {
            var renamed = this.additionalAnalyses[i].AssignInParallel(refinedMap, convert, pairs, state);

            // this should be made a postcondition of AssignInParallel, but at the moment the extractor has problems with all the generics envolved
            Contract.Assume(renamed.GetType() != typeof(ArrayState), "Runtime checking that the plugin analysis do not return the whole state");

            renamedPlugins[i] = renamed;
          }

          if (state.HasNonNullInfo)
          {
            return new ArrayState(array, numerical, state.NonNull, renamedPlugins, this.analysisMapping);
          }
          else
          {
            return new ArrayState(array, numerical, renamedPlugins, this.analysisMapping);
          }
        }

        public override ArrayState Join(Pair<APC, APC> edge, ArrayState newState, ArrayState prevState, out bool changed, bool widen)
        {
#if DEBUG && false
          Console.WriteLine("{0} -> {1} : {2}", edge.One, edge.Two, widen ? "widening" : "join");
#endif

#if TRACEPERFORMANCE
          bool changedToMakeHappyTheCompiler = false;

          var result = PerformanceMeasure.Measure<ArrayState>(PerformanceMeasure.ActionTags.ArraysJoin, () => JoinInternal(ref edge, newState, prevState, widen, out changedToMakeHappyTheCompiler));

          changed = changedToMakeHappyTheCompiler;
#else
          var result =  JoinInternal(ref edge, newState, prevState, widen, out changed);
#endif
          return result;
        }

        private ArrayState JoinInternal(ref Pair<APC, APC> edge, ArrayState newState, ArrayState prevState, bool widen, out bool changed)
        {
          /*
          Set<Variable> liveVars, deadVars;
          if(this.MethodDriver.AdditionalSyntacticInformation.Renamings.TryLiveVariablesAtJoinPoint(edge.Two, out liveVars, out deadVars))
          {
            Console.WriteLine("[JOIN] Have {0} live and {1} dead variables at join point {2} ", liveVars.Count, deadVars.Count, edge.Two);
          }
          */
          if (!widen)
          {
            var resultState = newState.Join(prevState);
            changed = true;

            if (this.nonnullAnalysis != null)
            {
              Contract.Assume(newState.HasNonNullInfo);
              Contract.Assume(prevState.HasNonNullInfo);

              bool changedNN;
              var nonnull = this.nonnullAnalysis.Join(edge, newState.NonNull, prevState.NonNull, out changedNN, widen);

              changed = changed || changedNN;

              resultState = resultState.UpdateNonNull(nonnull);
            }

            return resultState;
          }
          else
          {

            var resultState = newState.Widening(prevState);

            changed = false;

            if (this.nonnullAnalysis != null)
            {
              Contract.Assume(newState.HasNonNullInfo);
              Contract.Assume(prevState.HasNonNullInfo);

              var nonnull = this.nonnullAnalysis.Join(edge, newState.NonNull, prevState.NonNull, out changed, widen);

              resultState = resultState.UpdateNonNull(nonnull);
            }

            changed = changed || !resultState.LessEqual(prevState);

            return resultState;
          }
        }

        public override ArrayState Initblk(
          APC pc, bool @volatile, Variable destaddr, Variable value, Variable len, ArrayState data)
        {
          var numerical = this.numericalAnalysis.Initblk(pc, @volatile, destaddr, value, len, data.Numerical); Contract.Assume(numerical != null);
          var resultState = this.arrayAnalysis.Initblk(pc, @volatile, destaddr, value, len, data.UpdateNumerical(numerical));

          if (this.nonnullAnalysis != null)
          {
            var nonnull = this.nonnullAnalysis.Initblk(pc, @volatile, destaddr, value, len, resultState.NonNull);
            resultState = resultState.UpdateNonNull(nonnull);
          }

          foreach (var additionalAnalysis in this.additionalAnalyses)
          {
            resultState = additionalAnalysis.Initblk(pc, @volatile, destaddr, value, len, resultState);
          }

          return resultState;
        }

        public override ArrayState Initobj(APC pc, Type type, Variable ptr, ArrayState data)
        {
          var numerical = this.numericalAnalysis.Initobj(pc, type, ptr, data.Numerical); Contract.Assume(numerical != null);
          var resultState = this.arrayAnalysis.Initobj(pc, type, ptr, data.UpdateNumerical(numerical));

          if (this.nonnullAnalysis != null)
          {
            var nonnull = this.nonnullAnalysis.Initobj(pc, type, ptr, resultState.NonNull);
            resultState = resultState.UpdateNonNull(nonnull);
          }
          foreach (var additionalAnalysis in this.additionalAnalyses)
          {
            resultState = additionalAnalysis.Initobj(pc, type, ptr, resultState);
          }

          return resultState;
        }

        public override ArrayState Isinst(APC pc, Type type, Variable dest, Variable obj, ArrayState data)
        {
          var numerical = this.numericalAnalysis.Isinst(pc, type, dest, obj, data.Numerical); Contract.Assume(numerical != null);
          var resultState = this.arrayAnalysis.Isinst(pc, type, dest, obj, data.UpdateNumerical(numerical));

          if (this.nonnullAnalysis != null)
          {
            var nonnull = this.nonnullAnalysis.Isinst(pc, type, dest, obj, resultState.NonNull);
            resultState = resultState.UpdateNonNull(nonnull);
          }
          foreach (var additionalAnalysis in this.additionalAnalyses)
          {
            resultState = additionalAnalysis.Isinst(pc, type, dest, obj, resultState);
          }

          return resultState;
        }

        public override ArrayState Jmp(APC pc, Method method, ArrayState data)
        {
          var numerical = this.numericalAnalysis.Jmp(pc, method, data.Numerical); Contract.Assume(numerical != null);
          var resultState = this.arrayAnalysis.Jmp(pc, method, data.UpdateNumerical(numerical));

          if (this.nonnullAnalysis != null)
          {
            var nonnull = this.nonnullAnalysis.Jmp(pc, method, resultState.NonNull);
            resultState = resultState.UpdateNonNull(nonnull);
          }
          foreach (var additionalAnalysis in this.additionalAnalyses)
          {
            resultState = additionalAnalysis.Jmp(pc, method, resultState);
          }

          return resultState;
        }

        public override ArrayState Ldarg(APC pc, Parameter argument, bool isOld, Variable dest, ArrayState data)
        {
          var numerical = this.numericalAnalysis.Ldarg(pc, argument, isOld, dest, data.Numerical); Contract.Assume(numerical != null);
          var resultState = this.arrayAnalysis.Ldarg(pc, argument, isOld, dest, data.UpdateNumerical(numerical));

          if (this.nonnullAnalysis != null)
          {
            var nonnull = this.nonnullAnalysis.Ldarg(pc, argument, isOld, dest, resultState.NonNull);
            resultState = resultState.UpdateNonNull(nonnull);
          }
          foreach (var additionalAnalysis in this.additionalAnalyses)
          {
            resultState = additionalAnalysis.Ldarg(pc, argument, isOld, dest, resultState);
          }

          return resultState;
        }

        public override ArrayState Ldarga(APC pc, Parameter argument, bool isOld, Variable dest, ArrayState data)
        {
          var numerical = this.numericalAnalysis.Ldarga(pc, argument, isOld, dest, data.Numerical); Contract.Assume(numerical != null);
          var resultState = this.arrayAnalysis.Ldarga(pc, argument, isOld, dest, data.UpdateNumerical(numerical));

          if (this.nonnullAnalysis != null)
          {
            var nonnull = this.nonnullAnalysis.Ldarga(pc, argument, isOld, dest, resultState.NonNull);
            resultState = resultState.UpdateNonNull(nonnull);
          }
          foreach (var additionalAnalysis in this.additionalAnalyses)
          {
            resultState = additionalAnalysis.Ldarga(pc, argument, isOld, dest, resultState);
          }

          return resultState;
        }

        public override ArrayState
          Ldconst(APC pc, object constant, Type type, Variable dest,
          ArrayState data)
        {
          var numerical = this.numericalAnalysis.Ldconst(pc, constant, type, dest, data.Numerical); Contract.Assume(numerical != null);
          var resultState = this.arrayAnalysis.Ldconst(pc, constant, type, dest, data.UpdateNumerical(numerical));

          if (this.nonnullAnalysis != null)
          {
            var nonnull = this.nonnullAnalysis.Ldconst(pc, constant, type, dest, resultState.NonNull);
            resultState = resultState.UpdateNonNull(nonnull);
          }
          foreach (var additionalAnalysis in this.additionalAnalyses)
          {
            resultState = additionalAnalysis.Ldconst(pc, constant, type, dest, resultState);
          }

          return resultState;
        }

        public override ArrayState
          Ldelem(APC pc, Type type, Variable dest, Variable array, Variable index, ArrayState data)
        {
          var numerical = this.numericalAnalysis.Ldelem(pc, type, dest, array, index, data.Numerical); Contract.Assume(numerical != null);
          var resultState = this.arrayAnalysis.Ldelem(pc, type, dest, array, index, data.UpdateNumerical(numerical));

          if (this.nonnullAnalysis != null)
          {
            var nonnull = this.nonnullAnalysis.Ldelem(pc, type, dest, array, index, resultState.NonNull);
            resultState = resultState.UpdateNonNull(nonnull);
          }
          foreach (var additionalAnalysis in this.additionalAnalyses)
          {
            resultState = additionalAnalysis.Ldelem(pc, type, dest, array, index, resultState);
          }

          return resultState;
        }

        public override ArrayState Ldelema(APC pc, Type type, bool @readonly, Variable dest, Variable array, Variable index, ArrayState data)
        {
          var numerical = this.numericalAnalysis.Ldelema(pc, type, @readonly, dest, array, index, data.Numerical); Contract.Assume(numerical != null);
          var resultState = this.arrayAnalysis.Ldelema(pc, type, @readonly, dest, array, index, data.UpdateNumerical(numerical));

          if (this.nonnullAnalysis != null)
          {
            var nonnull = this.nonnullAnalysis.Ldelema(pc, type, @readonly, dest, array, index, resultState.NonNull);
            resultState = resultState.UpdateNonNull(nonnull);
          }
          foreach (var additionalAnalysis in this.additionalAnalyses)
          {
            resultState = additionalAnalysis.Ldelema(pc, type, @readonly, dest, array, index, resultState);
          }

          return resultState;
        }

        public override ArrayState Ldfieldtoken(APC pc, Field type, Variable dest, ArrayState data)
        {
          var numerical = this.numericalAnalysis.Ldfieldtoken(pc, type, dest, data.Numerical); Contract.Assume(numerical != null);
          var resultState = this.arrayAnalysis.Ldfieldtoken(pc, type, dest, data.UpdateNumerical(numerical));

          if (this.nonnullAnalysis != null)
          {
            var nonnull = this.nonnullAnalysis.Ldfieldtoken(pc, type, dest, resultState.NonNull);
            resultState = resultState.UpdateNonNull(nonnull);
          }
          foreach (var additionalAnalysis in this.additionalAnalyses)
          {
            resultState = additionalAnalysis.Ldfieldtoken(pc, type, dest, resultState);
          }

          return resultState;
        }

        public override ArrayState Ldfld(APC pc, Field field, bool @volatile, Variable dest, Variable obj, ArrayState data)
        {
          var numerical = this.numericalAnalysis.Ldfld(pc, field, @volatile, dest, obj, data.Numerical); Contract.Assume(numerical != null);
          var resultState = this.arrayAnalysis.Ldfld(pc, field, @volatile, dest, obj, data.UpdateNumerical(numerical));

          if (this.nonnullAnalysis != null)
          {
            var nonnull = this.nonnullAnalysis.Ldfld(pc, field, @volatile, dest, obj, resultState.NonNull);
            resultState = resultState.UpdateNonNull(nonnull);
          }
          foreach (var additionalAnalysis in this.additionalAnalyses)
          {
            resultState = additionalAnalysis.Ldfld(pc, field, @volatile, dest, obj, resultState);
          }

          return resultState;
        }

        public override ArrayState Ldflda(APC pc, Field field, Variable dest, Variable obj, ArrayState data)
        {
          var numerical = this.numericalAnalysis.Ldflda(pc, field, dest, obj, data.Numerical); Contract.Assume(numerical != null);
          var resultState = this.arrayAnalysis.Ldflda(pc, field, dest, obj, data.UpdateNumerical(numerical));

          if (this.nonnullAnalysis != null)
          {
            var nonnull = this.nonnullAnalysis.Ldflda(pc, field, dest, obj, resultState.NonNull);
            resultState = resultState.UpdateNonNull(nonnull);
          }
          foreach (var additionalAnalysis in this.additionalAnalyses)
          {
            resultState = additionalAnalysis.Ldflda(pc, field, dest, obj, resultState);
          }

          return resultState;
        }

        public override ArrayState Ldftn(APC pc, Method method, Variable dest, ArrayState data)
        {
          var numerical = this.numericalAnalysis.Ldftn(pc, method, dest, data.Numerical);
          Contract.Assume(numerical != null);
          var resultState = this.arrayAnalysis.Ldftn(pc, method, dest, data.UpdateNumerical(numerical));

          if (this.nonnullAnalysis != null)
          {
            var nonnull = this.nonnullAnalysis.Ldftn(pc, method, dest, resultState.NonNull);
            resultState = resultState.UpdateNonNull(nonnull);
          }
          foreach (var additionalAnalysis in this.additionalAnalyses)
          {
            resultState = additionalAnalysis.Ldftn(pc, method, dest, data.UpdateNumerical(numerical));
          }

          return resultState;
        }

        public override ArrayState Ldind(
          APC pc, Type type, bool @volatile, Variable dest, Variable ptr, ArrayState data)
        {
          var numerical = this.numericalAnalysis.Ldind(pc, type, @volatile, dest, ptr, data.Numerical); Contract.Assume(numerical != null);
          var resultState = this.arrayAnalysis.Ldind(pc, type, @volatile, dest, ptr, data.UpdateNumerical(numerical));

          if (this.nonnullAnalysis != null)
          {
            var nonnull = this.nonnullAnalysis.Ldind(pc, type, @volatile, dest, ptr, resultState.NonNull);
            resultState = resultState.UpdateNonNull(nonnull);
          }
          foreach (var additionalAnalysis in this.additionalAnalyses)
          {
            resultState = additionalAnalysis.Ldind(pc, type, @volatile, dest, ptr, resultState);
          }

          return resultState;
        }

        public override ArrayState Ldlen(
          APC pc, Variable dest, Variable array, ArrayState data)
        {
          var numerical = this.numericalAnalysis.Ldlen(pc, dest, array, data.Numerical); Contract.Assume(numerical != null);
          var resultState = this.arrayAnalysis.Ldlen(pc, dest, array, data.UpdateNumerical(numerical));

          if (this.nonnullAnalysis != null)
          {
            var nonnull = this.nonnullAnalysis.Ldlen(pc, dest, array, resultState.NonNull);
            resultState = resultState.UpdateNonNull(nonnull);
          }
          foreach (var additionalAnalysis in this.additionalAnalyses)
          {
            resultState = additionalAnalysis.Ldlen(pc, dest, array, resultState);
          }

          return resultState;
        }

        public override ArrayState
          Ldloc(APC pc, Local local, Variable dest, ArrayState data)
        {
          var numerical = this.numericalAnalysis.Ldloc(pc, local, dest, data.Numerical); Contract.Assume(numerical != null);
          var resultState = this.arrayAnalysis.Ldloc(pc, local, dest, data.UpdateNumerical(numerical));

          if (this.nonnullAnalysis != null)
          {
            var nonnull = this.nonnullAnalysis.Ldloc(pc, local, dest, resultState.NonNull);
            resultState = resultState.UpdateNonNull(nonnull);
          }
          foreach (var additionalAnalysis in this.additionalAnalyses)
          {
            resultState = additionalAnalysis.Ldloc(pc, local, dest, resultState);
          }

          return resultState;
        }

        public override ArrayState Ldloca(APC pc, Local local, Variable dest, ArrayState data)
        {
          var numerical = this.numericalAnalysis.Ldloca(pc, local, dest, data.Numerical); Contract.Assume(numerical != null);
          var resultState = this.arrayAnalysis.Ldloca(pc, local, dest, data.UpdateNumerical(numerical));

          if (this.nonnullAnalysis != null)
          {
            var nonnull = this.nonnullAnalysis.Ldloca(pc, local, dest, resultState.NonNull);
            resultState = resultState.UpdateNonNull(nonnull);
          }
          foreach (var additionalAnalysis in this.additionalAnalyses)
          {
            resultState = additionalAnalysis.Ldloca(pc, local, dest, resultState);
          }

          return resultState;
        }

        public override ArrayState Ldmethodtoken(APC pc, Method type, Variable dest, ArrayState data)
        {
          var numerical = this.numericalAnalysis.Ldmethodtoken(pc, type, dest, data.Numerical); Contract.Assume(numerical != null);
          var resultState = this.arrayAnalysis.Ldmethodtoken(pc, type, dest, data.UpdateNumerical(numerical));

          if (this.nonnullAnalysis != null)
          {
            var nonnull = this.nonnullAnalysis.Ldmethodtoken(pc, type, dest, resultState.NonNull);
            resultState = resultState.UpdateNonNull(nonnull);
          }
          foreach (var additionalAnalysis in this.additionalAnalyses)
          {
            resultState = additionalAnalysis.Ldmethodtoken(pc, type, dest, resultState);
          }

          return resultState;
        }

        public override ArrayState Ldnull(APC pc, Variable dest, ArrayState data)
        {
          var numerical = this.numericalAnalysis.Ldnull(pc, dest, data.Numerical); Contract.Assume(numerical != null);
          var resultState = this.arrayAnalysis.Ldnull(pc, dest, data.UpdateNumerical(numerical));

          if (this.nonnullAnalysis != null)
          {
            var nonnull = this.nonnullAnalysis.Ldnull(pc, dest, resultState.NonNull);
            resultState = resultState.UpdateNonNull(nonnull);
          }
          foreach (var additionalAnalysis in this.additionalAnalyses)
          {
            resultState = additionalAnalysis.Ldnull(pc, dest, resultState);
          }

          return resultState;
        }

        public override ArrayState Ldresult(APC pc, Type type, Variable dest, Variable source, ArrayState data)
        {
          var numerical = this.numericalAnalysis.Ldresult(pc, type, dest, source, data.Numerical); Contract.Assume(numerical != null);
          var resultState = this.arrayAnalysis.Ldresult(pc, type, dest, source, data.UpdateNumerical(numerical));

          if (this.nonnullAnalysis != null)
          {
            var nonnull = this.nonnullAnalysis.Ldresult(pc, type, dest, source, resultState.NonNull);
            resultState = resultState.UpdateNonNull(nonnull);
          }
          foreach (var additionalAnalysis in this.additionalAnalyses)
          {
            resultState = additionalAnalysis.Ldresult(pc, type, dest, source, resultState);
          }

          return resultState;
        }

        public override ArrayState Ldsfld(APC pc, Field field, bool @volatile, Variable dest, ArrayState data)
        {
          var numerical = this.numericalAnalysis.Ldsfld(pc, field, @volatile, dest, data.Numerical); Contract.Assume(numerical != null);
          var resultState = this.arrayAnalysis.Ldsfld(pc, field, @volatile, dest, data.UpdateNumerical(numerical));

          if (this.nonnullAnalysis != null)
          {
            var nonnull = this.nonnullAnalysis.Ldsfld(pc, field, @volatile, dest, resultState.NonNull);
            resultState = resultState.UpdateNonNull(nonnull);
          }
          foreach (var additionalAnalysis in this.additionalAnalyses)
          {
            resultState = additionalAnalysis.Ldsfld(pc, field, @volatile, dest, resultState);
          }

          return resultState;
        }

        public override ArrayState Ldsflda(APC pc, Field field, Variable dest, ArrayState data)
        {
          var numerical = this.numericalAnalysis.Ldsflda(pc, field, dest, data.Numerical); Contract.Assume(numerical != null);
          var resultState = this.arrayAnalysis.Ldsflda(pc, field, dest, data.UpdateNumerical(numerical));

          if (this.nonnullAnalysis != null)
          {
            var nonnull = this.nonnullAnalysis.Ldsflda(pc, field, dest, resultState.NonNull);
            resultState = resultState.UpdateNonNull(nonnull);
          }
          foreach (var additionalAnalysis in this.additionalAnalyses)
          {
            resultState = additionalAnalysis.Ldsflda(pc, field, dest, resultState);
          }

          return resultState;
        }

        public override ArrayState Ldstack(APC pc, int offset, Variable dest, Variable source, bool isOld, ArrayState data)
        {
          var numerical = this.numericalAnalysis.Ldstack(pc, offset, dest, source, isOld, data.Numerical); Contract.Assume(numerical != null);
          var resultState = this.arrayAnalysis.Ldstack(pc, offset, dest, source, isOld, data.UpdateNumerical(numerical));

          if (this.nonnullAnalysis != null)
          {
            var nonnull = this.nonnullAnalysis.Ldstack(pc, offset, dest, source, isOld, resultState.NonNull);
            resultState = resultState.UpdateNonNull(nonnull);
          }
          foreach (var additionalAnalysis in this.additionalAnalyses)
          {
            resultState = additionalAnalysis.Ldstack(pc, offset, dest, source, isOld, resultState);
          }

          return resultState;
        }

        public override ArrayState Ldstacka(APC pc, int offset, Variable dest, Variable source, Type type, bool isOld, ArrayState data)
        {
          var numerical = this.numericalAnalysis.Ldstacka(pc, offset, dest, source, type, isOld, data.Numerical); Contract.Assume(numerical != null);
          var resultState = this.arrayAnalysis.Ldstacka(pc, offset, dest, source, type, isOld, data.UpdateNumerical(numerical));

          if (this.nonnullAnalysis != null)
          {
            var nonnull = this.nonnullAnalysis.Ldstacka(pc, offset, dest, source, type, isOld, resultState.NonNull);
            resultState = resultState.UpdateNonNull(nonnull);
          }
          foreach (var additionalAnalysis in this.additionalAnalyses)
          {
            resultState = additionalAnalysis.Ldstacka(pc, offset, dest, source, type, isOld, resultState);
          }

          return resultState;
        }

        public override ArrayState Ldtypetoken(APC pc, Type type, Variable dest, ArrayState data)
        {
          var numerical = this.numericalAnalysis.Ldtypetoken(pc, type, dest, data.Numerical); Contract.Assume(numerical != null);
          var resultState = this.arrayAnalysis.Ldtypetoken(pc, type, dest, data.UpdateNumerical(numerical));

          if (this.nonnullAnalysis != null)
          {
            var nonnull = this.nonnullAnalysis.Ldtypetoken(pc, type, dest, resultState.NonNull);
            resultState = resultState.UpdateNonNull(nonnull);
          }
          foreach (var additionalAnalysis in this.additionalAnalyses)
          {
            resultState = additionalAnalysis.Ldtypetoken(pc, type, dest, resultState);
          }

          return resultState;
        }
        public override ArrayState Ldvirtftn(
          APC pc, Method method, Variable dest, Variable obj, ArrayState data)
        {
          var numerical = this.numericalAnalysis.Ldvirtftn(pc, method, dest, obj, data.Numerical); Contract.Assume(numerical != null);
          var resultState = this.arrayAnalysis.Ldvirtftn(pc, method, dest, obj, data.UpdateNumerical(numerical));

          if (this.nonnullAnalysis != null)
          {
            var nonnull = this.nonnullAnalysis.Ldvirtftn(pc, method, dest, obj, resultState.NonNull);
            resultState = resultState.UpdateNonNull(nonnull);
          }
          foreach (var additionalAnalysis in this.additionalAnalyses)
          {
            resultState = additionalAnalysis.Ldvirtftn(pc, method, dest, obj, resultState);
          }

          return base.Ldvirtftn(pc, method, dest, obj, data);
        }

        public override ArrayState Localloc(APC pc, Variable dest, Variable size, ArrayState data)
        {
          var numerical = this.numericalAnalysis.Localloc(pc, dest, size, data.Numerical); Contract.Assume(numerical != null);
          var resultState = this.arrayAnalysis.Localloc(pc, dest, size, data.UpdateNumerical(numerical));

          if (this.nonnullAnalysis != null)
          {
            var nonnull = this.nonnullAnalysis.Localloc(pc, dest, size, resultState.NonNull);
            resultState = resultState.UpdateNonNull(nonnull);
          }
          foreach (var additionalAnalysis in this.additionalAnalyses)
          {
            resultState = additionalAnalysis.Localloc(pc, dest, size, resultState);
          }

          return resultState;
        }

        public override ArrayState Mkrefany(APC pc, Type type, Variable dest, Variable obj, ArrayState data)
        {
          var numerical = this.numericalAnalysis.Mkrefany(pc, type, dest, obj, data.Numerical); Contract.Assume(numerical != null);
          var resultState = this.arrayAnalysis.Mkrefany(pc, type, dest, obj, data.UpdateNumerical(numerical));

          if (this.nonnullAnalysis != null)
          {
            var nonnull = this.nonnullAnalysis.Mkrefany(pc, type, dest, obj, resultState.NonNull);
            resultState = resultState.UpdateNonNull(nonnull);
          }
          foreach (var additionalAnalysis in this.additionalAnalyses)
          {
            resultState = additionalAnalysis.Mkrefany(pc, type, dest, obj, resultState);
          }

          return resultState;
        }

        public override ArrayState Newarray<ArgList>(APC pc, Type type, Variable dest, ArgList lengths, ArrayState data)
        {
          var numerical = this.numericalAnalysis.Newarray(pc, type, dest, lengths, data.Numerical); Contract.Assume(numerical != null);
          var resultState = this.arrayAnalysis.Newarray(pc, type, dest, lengths, data.UpdateNumerical(numerical));

          if (this.nonnullAnalysis != null)
          {
            var nonnull = this.nonnullAnalysis.Newarray(pc, type, dest, lengths, resultState.NonNull);
            resultState = resultState.UpdateNonNull(nonnull);
          }
          foreach (var additionalAnalysis in this.additionalAnalyses)
          {
            resultState = additionalAnalysis.Newarray(pc, type, dest, lengths, resultState);
          }

          return resultState;
        }

        public override ArrayState Newobj<ArgList>(APC pc, Method ctor, Variable dest, ArgList args, ArrayState data)
        {
          var numerical = this.numericalAnalysis.Newobj(pc, ctor, dest, args, data.Numerical); Contract.Assume(numerical != null);
          var resultState = this.arrayAnalysis.Newobj(pc, ctor, dest, args, data.UpdateNumerical(numerical));

          if (this.nonnullAnalysis != null)
          {
            var nonnull = this.nonnullAnalysis.Newobj(pc, ctor, dest, args, resultState.NonNull);
            resultState = resultState.UpdateNonNull(nonnull);
          }

          foreach (var additionalAnalysis in this.additionalAnalyses)
          {
            resultState = additionalAnalysis.Newobj(pc, ctor, dest, args, resultState);
          }

          return resultState;
        }

        public override ArrayState Nop(APC pc, ArrayState data)
        {
          var numerical = this.numericalAnalysis.Nop(pc, data.Numerical);
          Contract.Assume(numerical != null);
          var resultState = this.arrayAnalysis.Nop(pc, data.UpdateNumerical(numerical));

          if (this.nonnullAnalysis != null)
          {
            var nonnull = this.nonnullAnalysis.Nop(pc, resultState.NonNull);
            resultState = resultState.UpdateNonNull(nonnull);
          }
          foreach (var additionalAnalysis in this.additionalAnalyses)
          {
            resultState = additionalAnalysis.Nop(pc, resultState);
          }

          return resultState;
        }

        public override ArrayState Pop(APC pc, Variable source, ArrayState data)
        {
          var numerical = this.numericalAnalysis.Pop(pc, source, data.Numerical);
          Contract.Assume(numerical != null);
          var resultState = this.arrayAnalysis.Pop(pc, source, data.UpdateNumerical(numerical));

          if (this.nonnullAnalysis != null)
          {
            var nonnull = this.nonnullAnalysis.Pop(pc, source, resultState.NonNull);
            resultState = resultState.UpdateNonNull(nonnull);
          }
          foreach (var additionalAnalysis in this.additionalAnalyses)
          {
            resultState = additionalAnalysis.Pop(pc, source, resultState);
          }

          return resultState;
        }

        public override ArrayState Refanytype
          (APC pc, Variable dest, Variable source, ArrayState data)
        {
          var numerical = this.numericalAnalysis.Refanytype(pc, dest, source, data.Numerical); Contract.Assume(numerical != null);
          var resultState = this.arrayAnalysis.Refanytype(pc, dest, source, data.UpdateNumerical(numerical));

          if (this.nonnullAnalysis != null)
          {
            var nonnull = this.nonnullAnalysis.Refanytype(pc, dest, source, resultState.NonNull);
            resultState = resultState.UpdateNonNull(nonnull);
          }
          foreach (var additionalAnalysis in this.additionalAnalyses)
          {
            resultState = additionalAnalysis.Refanytype(pc, dest, source, resultState);
          }

          return resultState;
        }

        public override ArrayState Refanyval
          (APC pc, Type type, Variable dest, Variable source, ArrayState data)
        {
          var numerical = this.numericalAnalysis.Refanyval(pc, type, dest, source, data.Numerical); Contract.Assume(numerical != null);
          var resultState = this.arrayAnalysis.Refanyval(pc, type, dest, source, data.UpdateNumerical(numerical));

          if (this.nonnullAnalysis != null)
          {
            var nonnull = this.nonnullAnalysis.Refanyval(pc, type, dest, source, resultState.NonNull);
            resultState = resultState.UpdateNonNull(nonnull);
          }
          foreach (var additionalAnalysis in this.additionalAnalyses)
          {
            resultState = additionalAnalysis.Refanyval(pc, type, dest, source, resultState);
          }

          return resultState;
        }

        public override ArrayState Rethrow(APC pc, ArrayState data)
        {
          var numerical = this.numericalAnalysis.Rethrow(pc, data.Numerical); Contract.Assume(numerical != null);
          var resultState = this.arrayAnalysis.Rethrow(pc, data.UpdateNumerical(numerical));

          if (this.nonnullAnalysis != null)
          {
            var nonnull = this.nonnullAnalysis.Rethrow(pc, resultState.NonNull);
            resultState = resultState.UpdateNonNull(nonnull);
          }
          foreach (var additionalAnalysis in this.additionalAnalyses)
          {
            resultState = additionalAnalysis.Rethrow(pc, resultState);
          }

          return resultState;
        }

        public override ArrayState Return(APC pc, Variable source, ArrayState data)
        {
          var numerical = this.numericalAnalysis.Return(pc, source, data.Numerical); Contract.Assume(numerical != null);
          var resultState = this.arrayAnalysis.Return(pc, source, data.UpdateNumerical(numerical));

          if (this.nonnullAnalysis != null)
          {
            var nonnull = this.nonnullAnalysis.Return(pc, source, resultState.NonNull);
            resultState = resultState.UpdateNonNull(nonnull);
          }
          foreach (var additionalAnalysis in this.additionalAnalyses)
          {
            resultState = additionalAnalysis.Return(pc, source, resultState);
          }

          return resultState;
        }

        public override ArrayState Sizeof(APC pc, Type type, Variable dest, ArrayState data)
        {
          var numerical = this.numericalAnalysis.Sizeof(pc, type, dest, data.Numerical); Contract.Assume(numerical != null);
          var resultState = this.arrayAnalysis.Sizeof(pc, type, dest, data.UpdateNumerical(numerical));

          if (this.nonnullAnalysis != null)
          {
            var nonnull = this.nonnullAnalysis.Sizeof(pc, type, dest, resultState.NonNull);
            resultState = resultState.UpdateNonNull(nonnull);
          }
          foreach (var additionalAnalysis in this.additionalAnalyses)
          {
            resultState = additionalAnalysis.Sizeof(pc, type, dest, resultState);
          }

          return resultState;
        }

        public override ArrayState Starg(APC pc, Parameter argument, Variable source, ArrayState data)
        {
          var numerical = this.numericalAnalysis.Starg(pc, argument, source, data.Numerical); Contract.Assume(numerical != null);
          var resultState = this.arrayAnalysis.Starg(pc, argument, source, data.UpdateNumerical(numerical));

          if (this.nonnullAnalysis != null)
          {
            var nonnull = this.nonnullAnalysis.Starg(pc, argument, source, resultState.NonNull);
            resultState = resultState.UpdateNonNull(nonnull);
          }
          foreach (var additionalAnalysis in this.additionalAnalyses)
          {
            resultState = additionalAnalysis.Starg(pc, argument, source, resultState);
          }

          return resultState;
        }

        public override ArrayState Stelem
          (APC pc, Type type, Variable array, Variable index, Variable value, ArrayState data)
        {
          var numerical = this.numericalAnalysis.Stelem(pc, type, array, index, value, data.Numerical); Contract.Assume(numerical != null);
          var resultState = this.arrayAnalysis.Stelem(pc, type, array, index, value, data.UpdateNumerical(numerical));

          if (this.nonnullAnalysis != null)
          {
            var nonnull = this.nonnullAnalysis.Stelem(pc, type, array, index, value, resultState.NonNull);
            resultState = resultState.UpdateNonNull(nonnull);
          }
          foreach (var additionalAnalysis in this.additionalAnalyses)
          {
            resultState = additionalAnalysis.Stelem(pc, type, array, index, value, resultState);
          }

          return resultState;
        }

        public override ArrayState Stfld(APC pc, Field field, bool @volatile, Variable obj, Variable value, ArrayState data)
        {
          var numerical = this.numericalAnalysis.Stfld(pc, field, @volatile, obj, value, data.Numerical); Contract.Assume(numerical != null);
          var resultState = this.arrayAnalysis.Stfld(pc, field, @volatile, obj, value, data.UpdateNumerical(numerical));

          if (this.nonnullAnalysis != null)
          {
            var nonnull = this.nonnullAnalysis.Stfld(pc, field, @volatile, obj, value, resultState.NonNull);
            resultState = resultState.UpdateNonNull(nonnull);
          }
          foreach (var additionalAnalysis in this.additionalAnalyses)
          {
            resultState = additionalAnalysis.Stfld(pc, field, @volatile, obj, value, resultState);
          }

          return resultState;
        }

        public override ArrayState Stind(APC pc, Type type, bool @volatile, Variable ptr, Variable value, ArrayState data)
        {
          var numerical = this.numericalAnalysis.Stind(pc, type, @volatile, ptr, value, data.Numerical); Contract.Assume(numerical != null);
          var resultState = this.arrayAnalysis.Stind(pc, type, @volatile, ptr, value, data.UpdateNumerical(numerical));

          if (this.nonnullAnalysis != null)
          {
            var nonnull = this.nonnullAnalysis.Stind(pc, type, @volatile, ptr, value, resultState.NonNull);
            resultState = resultState.UpdateNonNull(nonnull);
          }
          foreach (var additionalAnalysis in this.additionalAnalyses)
          {
            resultState = additionalAnalysis.Stind(pc, type, @volatile, ptr, value, resultState);
          }

          return resultState;
        }

        public override ArrayState
          Stloc(APC pc, Local local, Variable source, ArrayState data)
        {
          var numerical = this.numericalAnalysis.Stloc(pc, local, source, data.Numerical); Contract.Assume(numerical != null);
          var resultState = this.arrayAnalysis.Stloc(pc, local, source, data.UpdateNumerical(numerical));

          if (this.nonnullAnalysis != null)
          {
            var nonnull = this.nonnullAnalysis.Stloc(pc, local, source, resultState.NonNull);
            resultState = resultState.UpdateNonNull(nonnull);
          }
          foreach (var additionalAnalysis in this.additionalAnalyses)
          {
            resultState = additionalAnalysis.Stloc(pc, local, source, resultState);
          }

          return resultState;
        }

        public override ArrayState Stsfld(APC pc, Field field, bool @volatile, Variable value, ArrayState data)
        {
          var numerical = this.numericalAnalysis.Stsfld(pc, field, @volatile, value, data.Numerical); Contract.Assume(numerical != null);
          var resultState = this.arrayAnalysis.Stsfld(pc, field, @volatile, value, data.UpdateNumerical(numerical));

          if (this.nonnullAnalysis != null)
          {
            var nonnull = this.nonnullAnalysis.Stsfld(pc, field, @volatile, value, resultState.NonNull);
            resultState = resultState.UpdateNonNull(nonnull);
          }
          foreach (var additionalAnalysis in this.additionalAnalyses)
          {
            resultState = additionalAnalysis.Stsfld(pc, field, @volatile, value, resultState);
          }

          return resultState;
        }

        public override ArrayState Switch(APC pc, Type type, IEnumerable<Pair<object, APC>> cases, Variable value, ArrayState data)
        {
          var numerical = this.numericalAnalysis.Switch(pc, type, cases, value, data.Numerical); Contract.Assume(numerical != null);
          var resultState = this.arrayAnalysis.Switch(pc, type, cases, value, data.UpdateNumerical(numerical));

          if (this.nonnullAnalysis != null)
          {
            var nonnull = this.nonnullAnalysis.Switch(pc, type, cases, value, resultState.NonNull);
            resultState = resultState.UpdateNonNull(nonnull);
          }
          foreach (var additionalAnalysis in this.additionalAnalyses)
          {
            resultState = additionalAnalysis.Switch(pc, type, cases, value, resultState);
          }

          return resultState;
        }

        public override ArrayState Throw(APC pc, Variable exn, ArrayState data)
        {
          var numerical = this.numericalAnalysis.Throw(pc, exn, data.Numerical); Contract.Assume(numerical != null);
          var resultState = this.arrayAnalysis.Throw(pc, exn, data.UpdateNumerical(numerical));

          if (this.nonnullAnalysis != null)
          {
            var nonnull = this.nonnullAnalysis.Throw(pc, exn, resultState.NonNull);
            resultState = resultState.UpdateNonNull(nonnull);
          }
          foreach (var additionalAnalysis in this.additionalAnalyses)
          {
            resultState = additionalAnalysis.Throw(pc, exn, resultState);
          }

          return resultState;
        }

        public override ArrayState Unary(
          APC pc, UnaryOperator op, bool overflow, bool unsigned, Variable dest, Variable source,
          ArrayState data)
        {
          var numerical = this.numericalAnalysis.Unary(pc, op, overflow, unsigned, dest, source, data.Numerical); Contract.Assume(numerical != null);
          var resultState = this.arrayAnalysis.Unary(pc, op, overflow, unsigned, dest, source, data.UpdateNumerical(numerical));

          if (this.nonnullAnalysis != null)
          {
            var nonnull = this.nonnullAnalysis.Unary(pc, op, overflow, unsigned, dest, source, resultState.NonNull);
            resultState.UpdateNonNull(nonnull);
          }
          foreach (var additionalAnalysis in this.additionalAnalyses)
          {
            resultState = additionalAnalysis.Unary(pc, op, overflow, unsigned, dest, source, resultState);
          }

          return resultState;
        }

        public override ArrayState Unbox(
          APC pc, Type type, Variable dest, Variable obj,
          ArrayState data)
        {
          var numerical = this.numericalAnalysis.Unbox(pc, type, dest, obj, data.Numerical); Contract.Assume(numerical != null);
          var resultState = this.arrayAnalysis.Unbox(pc, type, dest, obj, data.UpdateNumerical(numerical));

          if (this.nonnullAnalysis != null)
          {
            var nonnull = this.nonnullAnalysis.Unbox(pc, type, dest, obj, resultState.NonNull);
            resultState = resultState.UpdateNonNull(nonnull);
          }
          foreach (var additionalAnalysis in this.additionalAnalyses)
          {
            resultState = additionalAnalysis.Unbox(pc, type, dest, obj, resultState);
          }

          return resultState;
        }

        public override ArrayState Unboxany(APC pc, Type type, Variable dest, Variable obj, ArrayState data)
        {
          var numerical = this.numericalAnalysis.Unboxany(pc, type, dest, obj, data.Numerical); Contract.Assume(numerical != null);
          var resultState = this.arrayAnalysis.Unboxany(pc, type, dest, obj, data.UpdateNumerical(numerical));

          if (this.nonnullAnalysis != null)
          {
            var nonnull = this.nonnullAnalysis.Unboxany(pc, type, dest, obj, resultState.NonNull);
            resultState = resultState.UpdateNonNull(nonnull);
          }
          foreach (var additionalAnalysis in this.additionalAnalyses)
          {
            resultState = additionalAnalysis.Unboxany(pc, type, dest, obj, resultState);
          }

          return resultState;
        }

        #endregion

        #region Implementation of abstract methods

        public override Predicate<APC> CacheStates(IFixpointInfo<APC, ArrayState> fixpointInfo)
        {
          if (this.fixpointInfo != null)
          {
            // distribute the fixpoint info here
            if (this.nonnullAnalysis != null)
            {
              this.nonnullAnalysis.CacheStates(new FixpointInfoProjectionOnNonnullState(this, fixpointInfo));
            }

            this.numericalAnalysis.CacheStates(new FixPointInfoProjectionOnNumericalState(this, fixpointInfo));

            this.arrayAnalysis.CacheStates(fixpointInfo);

            foreach (var analysis in this.additionalAnalyses)
            {
              analysis.CacheStates(fixpointInfo);
            }
          }
          return base.CacheStates(fixpointInfo);
        }

        public override ArrayState GetTopValue()
        {
          Contract.Ensures(Contract.Result<ArrayState>() != null);

          var numerical = this.numericalAnalysis.GetTopValue();
          Contract.Assume(numerical != null);

          var array = this.arrayAnalysis.InitialState as ArraySegmentationEnvironment<NonRelationalValueAbstraction<BoxedVariable<Variable>, BoxedExpression>, BoxedVariable<Variable>, BoxedExpression>;

          Contract.Assume(array != null);

          var resultState = new ArrayState(array, numerical);

          if (this.nonnullAnalysis != null)
          {
            var nonnull = this.nonnullAnalysis.GetTopValue();

            resultState = resultState.UpdateNonNull(nonnull);
          }

          if (pluginCount > 0)
          {
            var plugins = new IAbstractDomainForEnvironments<BoxedVariable<Variable>, BoxedExpression>[this.pluginCount];

            var i = 0;
            foreach (var analysis in this.additionalAnalyses)
            {
              plugins[i] = analysis.InitialState;
              i++;
            }

            if (resultState.HasNonNullInfo)
            {
              resultState = new ArrayState(resultState.Array, resultState.Numerical, resultState.NonNull, plugins, this.analysisMapping);
            }
            else
            {
              resultState = new ArrayState(resultState.Array, resultState.Numerical, null, plugins, this.analysisMapping);
            }
          }

          return resultState;
        }
#if false
        internal override ProofObligations<Local, Parameter, Method, Field, Property, Event, Type, Variable, Attribute, Assembly, BoxedExpression, ProofObligationBase<BoxedExpression, Variable>> Obligations
        {
          get
          {
            // Note: the non-null analysis collects the obligations internally

            return this.numericalAnalysis.Obligations;
          }
        }

        public override AnalysisStatistics Statistics()
        {
          var result = this.numericalAnalysis.Obligations.Statistics;
          if (this.nonnullAnalysis != null)
          {
            result.Add(this.nonnullAnalysis.Statistics());
          }

          foreach (var analysis in this.additionalAnalyses)
          {
            result.Add(analysis.Statistics());
          }

          return result;
        }

        public override void ValidateImplicitAssertions(IFactQuery<BoxedExpression, Variable> facts, IOutputResults output)
        {
          this.numericalAnalysis.ValidateImplicitAssertionsWithAlternativeFixpointInfo(output, new FixPointInfoProjectionOnNumericalState(this));

          if (this.nonnullAnalysis != null)
          {
            this.nonnullAnalysis.ValidateImplicitAssertions(facts, output, new FixpointInfoProjectionOnNonnullState(this));
          }

          foreach (var analysis in this.additionalAnalyses)
          {
            analysis.ValidateImplicitAssertions(facts, output);
          }
        }

#endif
        public override IFactQuery<BoxedExpression, Variable> FactQuery(IFixpointInfo<APC, ArrayState> fixpoint)
        {
          var composedFactQuery = new ComposedFactQuery<Variable>(this.MethodDriver.BasicFacts.IsUnreachable);

          if (this.nonnullAnalysis != null)
          {
            composedFactQuery.Add(this.nonnullAnalysis.FactQuery(new FixpointInfoProjectionOnNonnullState(this, fixpoint)));
          }

          composedFactQuery.Add(this.numericalAnalysis.FactQuery(new FixPointInfoProjectionOnNumericalState(this, fixpoint)));

          if (fixpoint != null)
          {
            composedFactQuery.Add(this.arrayAnalysis.FactQuery(fixpoint));
          }

          foreach (var analysis in this.additionalAnalyses)
          {
            var q = analysis.FactQuery(fixpoint);
            if (q != null)
            {
              composedFactQuery.Add(q);
            }
          }

          return composedFactQuery;
        }

        public override bool SuggestAnalysisSpecificPostconditions(
          ContractInferenceManager inferenceManager,
          IFixpointInfo<APC, ArrayState> fixpointInfo,
          List<BoxedExpression> postconditions)
        {
          var result = false;

          result |= this.numericalAnalysis.SuggestAnalysisSpecificPostconditions(inferenceManager,
            new FixPointInfoProjectionOnNumericalState(this, fixpointInfo), postconditions);

          if (this.nonnullAnalysis != null)
          {
            result |= this.nonnullAnalysis.SuggestPostcondition(inferenceManager, new FixpointInfoProjectionOnNonnullState(this, fixpointInfo));
          }

          result |= this.arrayAnalysis.SuggestAnalysisSpecificPostconditions(inferenceManager, fixpointInfo, postconditions);

          //this.additionalAnalyses.ForEach(analysis => analysis.SuggestAnalysisSpecificPostconditions(inferenceManager, fixpointInfo, postconditions));
          Array.ForEach(this.additionalAnalyses, analysis => result |= analysis.SuggestAnalysisSpecificPostconditions(inferenceManager, fixpointInfo, postconditions));

          return result;
        }

        public override bool TrySuggestPostconditionForOutParameters(
          IFixpointInfo<APC, ArrayState> fixpointInfo,
          List<BoxedExpression> postconditions, Variable p, FList<PathElement> path)
        {
          var result = this.numericalAnalysis.TrySuggestPostconditionForOutParameters(new FixPointInfoProjectionOnNumericalState(this, fixpointInfo), postconditions, p, path);
          result = result | this.arrayAnalysis.TrySuggestPostconditionForOutParameters(fixpointInfo, postconditions, p, path);

          // NOTE: We do not infer postconditions for out parameters for nonnull

          foreach (var additionalAnalysis in this.additionalAnalyses)
          {
            result = result | additionalAnalysis.TrySuggestPostconditionForOutParameters(fixpointInfo, postconditions, p, path);
          }

          return result;
        }

        #endregion

        #region Utility methods

        static public NormalizedExpression<BoxedVariable<Variable>> ToNormalizedExpression(Variable v)
        {
          Contract.Ensures(Contract.Result<NormalizedExpression<BoxedVariable<Variable>>>() != null);

          return NormalizedExpression<BoxedVariable<Variable>>.For(new BoxedVariable<Variable>(v));
        }

        static public Predicate<BoxedExpression> GetSimpleDecisionProcedure<AbstractDomain>(AbstractDomain data)
        {
          if (data == null || !(data is INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression>))
          {
            return exp => false;
          }

          var numDom = data as INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression>;

          return exp => numDom.CheckIfHolds(exp).IsTrue();
        }

        #endregion

        #region FixpointInfoProjection class



        #region Fixpoint Projections
        public class FixPointInfoProjectionOnNumericalState
          : FixPointInfoProjection<ArrayState, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression>>
        {
          readonly ArrayAnalysis<AnalysisOptions, NumericalOptions> analysis;

          public FixPointInfoProjectionOnNumericalState(ArrayAnalysis<AnalysisOptions, NumericalOptions> arrayAnalysis, IFixpointInfo<APC, ArrayState> fixpoint)
            : base(fixpoint)
          {
            Contract.Requires(arrayAnalysis != null);
            Contract.Requires(fixpoint != null);

            this.analysis = arrayAnalysis;
          }

          protected override ArrayState InitialValue
          {
            get
            {
              var result = this.analysis.GetTopValue();
              Contract.Assume(result != null);

              return result;
            }
          }

          protected override ArrayState
            MakeProductState(
            INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> init,
            ArrayState top)
          {
            Contract.Assume(init != null);

            return new ArrayState(top.Array, init);
          }

          protected override INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> Project(ArrayState productAD)
          {
            return productAD.Numerical;
          }
        }

        public class FixpointInfoProjectionOnNonnullState
          : FixPointInfoProjection<ArrayState, Analyzers.NonNull.TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>.Domain>
        {
          readonly ArrayAnalysis<AnalysisOptions, NumericalOptions> analysis;

          public FixpointInfoProjectionOnNonnullState(ArrayAnalysis<AnalysisOptions, NumericalOptions> arrayAnalysis, IFixpointInfo<APC, ArrayState> fixpoint)
            : base(fixpoint)
          {
            Contract.Requires(arrayAnalysis != null);
            Contract.Requires(fixpoint != null);

            this.analysis = arrayAnalysis;
          }

          protected override ArrayState
            InitialValue
          {
            get
            {
              var result = this.analysis.GetTopValue();
              // Contract.Assume(result != null);

              return result;
            }
          }

          protected override ArrayState
            MakeProductState(
            Analyzers.NonNull.TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>.Domain init,
            ArrayState top)
          {
            return top.UpdateNonNull(init);
          }

          protected override Analyzers.NonNull.TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>.Domain
            Project(ArrayState productAD)
          {
            if (productAD.HasNonNullInfo)
            {
              return productAD.NonNull;
            }
            else
            {
              return default(Analyzers.NonNull.TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>.Domain);
            }
          }
        }
        #endregion
        #endregion

        #region Precondition suggestion

        public override void SuggestPrecondition(ContractInferenceManager inferenceManager)
        {
          this.numericalAnalysis.SuggestPrecondition(inferenceManager);
          this.arrayAnalysis.SuggestPrecondition(inferenceManager);
          foreach (var analysis in this.additionalAnalyses)
          {
            analysis.SuggestPrecondition(inferenceManager, this.fixpointInfo);
          }
        }

        #endregion       
      }

      // F: TODO - Once the rewriter goes to CCI2, uncomment it
      //[ContractClass(typeof(FixPointInfoProjectionContracts<,,>))]
      abstract public class FixPointInfoProjection<ProductAbstractDomain, AbstractDomain>
        : IFixpointInfo<APC, AbstractDomain>
      {
        readonly protected IFixpointInfo<APC, ProductAbstractDomain> fixpointinfo;

        public FixPointInfoProjection(IFixpointInfo<APC, ProductAbstractDomain> fixpoint)
        {
          Contract.Requires(fixpoint != null);

          this.fixpointinfo = fixpoint;
        }

        #region Abstract methods

        abstract protected AbstractDomain Project(ProductAbstractDomain productAD);

        abstract protected ProductAbstractDomain InitialValue { get; }

        abstract protected ProductAbstractDomain MakeProductState(AbstractDomain init, ProductAbstractDomain top);

        #endregion

        #region IFixpointInfo<APC, AbstractDomain> Members

        public bool PreState(APC label, out AbstractDomain ifFound)
        {
          ProductAbstractDomain prestate;

          if (this.fixpointinfo.PreState(label, out prestate))
          {
            Contract.Assume(prestate != null);

            ifFound = Project(prestate);
            return true;
          }

          ifFound = default(AbstractDomain);
          return false;
        }

        public bool PostState(APC label, out AbstractDomain ifFound)
        {
          ProductAbstractDomain poststate;

          if (this.fixpointinfo.PostState(label, out poststate))
          {
            Contract.Assume(poststate != null);

            ifFound = Project(poststate);
            return true;
          }

          ifFound = default(AbstractDomain);
          return false;
        }

        public bool TryAStateForPostCondition(AbstractDomain initialState, out AbstractDomain exitState)
        {
          if (initialState == null)
          {
            exitState = default(AbstractDomain);

            return false;
          }

          var topState = this.InitialValue;
          var initState = this.MakeProductState(initialState, topState); // this.arrayAnalysis.MakeArrayState(initialState, topState.Array);

          ProductAbstractDomain poststate;

          if (this.fixpointinfo.TryAStateForPostCondition(initState, out poststate))
          {
            Contract.Assume(poststate != null);

            exitState = Project(poststate);

            return true;
          }

          exitState = default(AbstractDomain);
          return false;
        }

        public IEnumerable<FList<STuple<CFGBlock, CFGBlock, string>>> CachedContexts(CFGBlock block)
        {
          return this.fixpointinfo.CachedContexts(block);
        }

        public void PushExceptionState(APC atThrow, AbstractDomain exceptionState)
        {
          // Silently continue...
          if (exceptionState == null)
          {
            return;
          }

          var topState = this.InitialValue;
          var liftedExceptionState = this.MakeProductState(exceptionState, topState);

          this.fixpointinfo.PushExceptionState(atThrow, liftedExceptionState);
        }

        #endregion
      }

      #region Contracts -- Commented by the time being as they cause the CCI1-based rewriter to generate a bad binary
#if false
        //[ContractClassFor(typeof(FixPointInfoProjection<,,>))]
        abstract class FixPointInfoProjectionContracts<Analysis, ProductAbstractDomain, AbstractDomain>
          : FixPointInfoProjection<Analysis, ProductAbstractDomain, AbstractDomain>
        {
          protected FixPointInfoProjectionContracts() : base(default(Analysis)) { }

          protected override IFixpointInfo<APC, ProductAbstractDomain> FixpointInfo(Analysis analysis)
          {
            Contract.Requires(analysis != null);
            Contract.Ensures(Contract.Result<IFixpointInfo<APC, ProductAbstractDomain>>() != null);

            return default(IFixpointInfo<APC, ProductAbstractDomain>);
          }

          protected override AbstractDomain Project(ProductAbstractDomain productAD)
          {
            Contract.Requires(productAD != null);
            Contract.Ensures(Contract.Result<AbstractDomain>() != null);

            return default(AbstractDomain);
          }

          protected override ProductAbstractDomain InitialValue
          {
            get 
            {
              Contract.Ensures(Contract.Result<ProductAbstractDomain>() != null);

              return default(ProductAbstractDomain);
            }
          }

          protected override ProductAbstractDomain MakeProductState(AbstractDomain init, ProductAbstractDomain top)
          {
            Contract.Requires(init != null);
            Contract.Requires(top != null);

            Contract.Ensures(Contract.Result<ProductAbstractDomain>() != null);

            return default(ProductAbstractDomain);
          }
        }
#endif
      #endregion
    }
  }
}
