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

#define PARALLEL
#define PARALLELWIDENING
#define TRACEPERFORMANCE

using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Microsoft.Research.AbstractDomains;
using Microsoft.Research.AbstractDomains.Numerical;
using Microsoft.Research.CodeAnalysis;
using System.Diagnostics;
using Microsoft.Research.DataStructures;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Microsoft.Research.CodeAnalysis
{

  public static partial class AnalysisWrapper
  {
    public static partial class TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>
      where Variable : IEquatable<Variable>
      where Expression : IEquatable<Expression>
      where Type : IEquatable<Type>
    {
      /// <summary>
      /// The product abstract state for the array analysis.
      /// It contains:
      ///   An abstract state for the array
      ///   An abstract state for the numerical environment
      /// It may contain:
      ///   An abstract state for the nonnull environement
      ///   A list of abstract states, plugins of the analysis
      /// </summary>
      /// <typeparam name="BoxedVariable<Variable>iable"></typeparam>
      [ContractVerification(false)]
      public class ArrayState
        : IAbstractDomainForEnvironments<BoxedVariable<Variable>, BoxedExpression>, ICloneable
      {
        [ContractInvariantMethod]
        void ObjectInvariant()
        {
          Contract.Invariant(this.arrayState != null);
          Contract.Invariant(this.numericalState != null);
          Contract.Invariant(this.otherStates != null);
          Contract.Invariant(this.mappings != null);
        }

        [ThreadStatic]
        private static int? cachedAdditionalStatesCount = null;
        public static int AdditionalStatesCount
        {
          get
          {
            if (!cachedAdditionalStatesCount.HasValue)
            {
              cachedAdditionalStatesCount = Enum.GetNames(typeof(AdditionalStates)).Length;
            }
            return cachedAdditionalStatesCount.Value;
          }
        }

        // Invariant: the enums value below should be >= 0 and consecutive. We use this representation for performance downstream, to avoid dictionaries
        public enum AdditionalStates { ArrayChecked = 0, ArrayRefinement, ArrayPurity, ArrayValues, Existential, RuntimeTypes, Enum }

        private readonly ArraySegmentationEnvironment<NonRelationalValueAbstraction<BoxedVariable<Variable>, BoxedExpression>, BoxedVariable<Variable>, BoxedExpression>
          arrayState;
        private readonly INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression>
          numericalState;
        private readonly Analyzers.NonNull.TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>.Domain?
          nonnullState;

        private readonly IAbstractDomainForEnvironments<BoxedVariable<Variable>, BoxedExpression>[] otherStates;

        private readonly /*Dictionary<AdditionalStates, int>*/ int[] mappings;

        #region Constructor
        public ArrayState(
          ArraySegmentationEnvironment<NonRelationalValueAbstraction<BoxedVariable<Variable>, BoxedExpression>, BoxedVariable<Variable>, BoxedExpression> array,
          INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> numerical,
          Analyzers.NonNull.TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>.Domain? nonnull = null
          )
          : this(array, numerical, nonnull, new IAbstractDomainForEnvironments<BoxedVariable<Variable>, BoxedExpression>[0], new int[0])
        {
          Contract.Requires(array != null);
          Contract.Requires(numerical!= null);
        }

        [SuppressMessage("Microsoft.Contracts", "RequiresAtCall-Contract.ForAll(otherStates, x => x != null)")]
        public ArrayState(
          ArraySegmentationEnvironment<NonRelationalValueAbstraction<BoxedVariable<Variable>, BoxedExpression>, BoxedVariable<Variable>, BoxedExpression> array,
          INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> numerical,
          IAbstractDomainForEnvironments<BoxedVariable<Variable>, BoxedExpression>[] otherStates,
          int[] mappings
          )
          : this(array, numerical, null, otherStates, mappings)
        {
          Contract.Requires(array != null);
          Contract.Requires(numerical!= null);
          Contract.Requires(otherStates != null);
          Contract.Requires(mappings != null);
          Contract.Requires(Contract.ForAll(otherStates, x => x != null));
        }

        [SuppressMessage("Microsoft.Contracts", "RequiresAtCall-Contract.ForAll(otherStates, x => x != null)")]
        public ArrayState(
          ArraySegmentationEnvironment<NonRelationalValueAbstraction<BoxedVariable<Variable>, BoxedExpression>, BoxedVariable<Variable>, BoxedExpression> array,
          INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> numerical,
          Analyzers.NonNull.TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>.Domain? nonnull,
          IAbstractDomainForEnvironments<BoxedVariable<Variable>, BoxedExpression>[] otherStates,
          int[] mappings)
        {
          Contract.Requires(array != null);
          Contract.Requires(numerical != null);
          Contract.Requires(otherStates != null);
          Contract.Requires(mappings != null);
          Contract.Requires(Contract.ForAll(otherStates, x => x != null));

          Contract.Ensures(this.arrayState == array);
          Contract.Ensures(this.numericalState == numerical);
          Contract.Ensures(this.nonnullState.Equals(nonnull));
          Contract.Ensures(this.otherStates == otherStates);
          Contract.Ensures(this.mappings == mappings);

          this.arrayState = array;
          this.numericalState = numerical;
          this.nonnullState = nonnull;

          this.otherStates = otherStates;
          this.mappings = mappings;
        }
        #endregion

        #region Getters

        public ArraySegmentationEnvironment<NonRelationalValueAbstraction<BoxedVariable<Variable>, BoxedExpression>, BoxedVariable<Variable>, BoxedExpression> Array
        {
          get
          {
            Contract.Ensures(Contract.Result
              <ArraySegmentationEnvironment<NonRelationalValueAbstraction<BoxedVariable<Variable>, BoxedExpression>, BoxedVariable<Variable>, BoxedExpression>>() != null);

            return this.arrayState;
          }
        }

        public INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> Numerical
        {
          get
          {
            Contract.Ensures(Contract.Result<INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression>>() != null);

            return this.numericalState;
          }
        }

        public Analyzers.NonNull.TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>.Domain NonNull
        {
          get
          {
            Contract.Requires(this.HasNonNullInfo);

            if (this.nonnullState.HasValue)
            {
              return this.nonnullState.Value;
            }

            throw new AbstractInterpretationException("Non null state is not set!");
          }
        }

        public bool HasNonNullInfo
        {
          get
          {
            Contract.Ensures(Contract.Result<bool>() == this.nonnullState.HasValue);

            return this.nonnullState.HasValue;
          }
        }

        public int PluginsCount
        {
          get
          {
            Contract.Ensures(Contract.Result<int>() == this.otherStates.Length);

            return this.otherStates.Length;
          }
        }

        /// <summary>
        /// Convention: index == -1 is the Array state
        /// </summary>
        [Pure]
        [DebuggerStepThrough]
        public IAbstractDomainForEnvironments<BoxedVariable<Variable>, BoxedExpression> PluginAbstractStateAt(int index)
        {
          Contract.Requires(index >= -1);
          Contract.Requires(index < PluginsCount);

          Contract.Ensures(Contract.Result<IAbstractDomainForEnvironments<BoxedVariable<Variable>, BoxedExpression>>() != null);

          if (index == -1)
          {
            return this.arrayState;
          }
          return this.otherStates[index];
        }

        #endregion

        #region Questions

        private bool TryGetIdForState(AdditionalStates state, out int id)
        {
          id = this.mappings[(int)state];

          return id > 0;
        }

        [Pure]
        public bool IsUnmodifiedArrayElementFromEntry(BoxedVariable<Variable> array, BoxedExpression index)
        {
          int id;
          if (TryGetIdForState(AdditionalStates.ArrayPurity, out id))
          {
            IAbstractDomain element;
            ArraySegmentation<TwoValuesLattice<BoxedVariable<Variable>>, BoxedVariable<Variable>, BoxedExpression> segmentation;
            var state = this.PluginAbstractStateAt(id) as ArraySegmentationEnvironment<TwoValuesLattice<BoxedVariable<Variable>>, BoxedVariable<Variable>, BoxedExpression>;
            if (state != null && state.TryGetValue(array, out segmentation))
            {
              // The common case the array is not modified
              TwoValuesLattice<BoxedVariable<Variable>> value;
              if (segmentation.Elements.Single(out value) && value.IsBottom)
              {
                return true;
              }

              var indexNormalized = index.ToNormalizedExpression<Variable>();
              // indexNormalized may be null, so we should check it
              if (indexNormalized != null && segmentation.TryGetAbstractValue(indexNormalized, this.Numerical, out element))
              {
                var casted = element as TwoValuesLattice<BoxedVariable<Variable>>;
                if (casted != null && casted.IsBottom)
                {
                  return true;
                }
              }
            }
          }

          return false;
        }

        [Pure]
        public bool CanRefineToArrayLoad(BoxedVariable<Variable> variable, out Pair<BoxedVariable<Variable>, BoxedVariable<Variable>> refined)
        {
          int id;
          refined = default(Pair<BoxedVariable<Variable>, BoxedVariable<Variable>>);
          if (TryGetIdForState(AdditionalStates.ArrayRefinement, out id))
          {
            var state = this.PluginAbstractStateAt(id) as ArrayTracking;
            if (state != null)
            {
              ScalarFromArrayTracking value;

              if (state.TryGetValue(variable, out value) && value.IsNormal())
              {
                if (value.Left.IsNormal() && value.Right.IsNormal() 
                  && value.Left.Count == 1 && value.Right.Count == 1)
                {
                  var arr = value.Left.Values.First();
                  var index = value.Right.Values.First();

                  refined = new Pair<BoxedVariable<Variable>, BoxedVariable<Variable>>(arr, index);
                  return true;
                }
              }
            }
          }

          return false;
        }

        [Pure]
        public bool CanRefineToArrayLoad(Variable variable, out Pair<Variable, Variable> refined)
        {
          Pair<BoxedVariable<Variable>, BoxedVariable<Variable>> bRefined;
          Variable v1, v2;

          if(CanRefineToArrayLoad(new BoxedVariable<Variable>(variable), out bRefined)
            && bRefined.One.TryUnpackVariable(out v1) && bRefined.Two.TryUnpackVariable(out v2))
          {
            refined = new Pair<Variable, Variable>(v1, v2);
            return true;
          }

          refined = default(Pair<Variable, Variable>);
          return false;
        }

        [Pure]
        // F: TODO: Lift it to sets (in general we may have several of them)
        public bool IsVariableValueFlowFromArray(BoxedVariable<Variable> variable, out BoxedVariable<Variable> array, out bool unmodifiedFromMethodEntry)
        {
          // First look into the array refinement substate
          int id;
          if (TryGetIdForState(AdditionalStates.ArrayRefinement, out id))
          {
            var state = this.PluginAbstractStateAt(id) as ArrayTracking;
            if (state != null)
            {
              ScalarFromArrayTracking value;

              if (state.TryGetValue(variable, out value) && value.IsNormal())
              {
                if (value.Left.IsNormal() && value.Left.Count == 1)
                {
                  array = value.Left.Values.First();
                  unmodifiedFromMethodEntry = value.IsUnmodifiedFromEntry.IsTrue();
                  return true;
                }
              }
            }
          }
          // Now look into the array existential substate
          if (TryGetIdForState(AdditionalStates.Existential, out id))
          {
            var state = this.PluginAbstractStateAt(id) as ArraySegmentationEnvironment<NonRelationalValueAbstraction<BoxedVariable<Variable>, BoxedExpression>, BoxedVariable<Variable>, BoxedExpression>;
            if (state != null)
            {
              foreach (var segmentation in state.Elements)
              {
                if (segmentation.Value.IsNormal())
                {
                  foreach (var segment in segmentation.Value.Elements)
                  {
                    if (segment.Equalities.Contains(variable))
                    {
                      unmodifiedFromMethodEntry = false; // we do not know...
                      array = segmentation.Key;
                      return true;
                    }
                  }
                }
              }
            }
          }
          array = default(BoxedVariable<Variable>);
          unmodifiedFromMethodEntry = default(bool);
          return false;
        }

        [Pure]
        public bool IsVariableValueFlowFromArray(Variable variable, out Variable array, out bool unmodifiedFromMethodEntry)
        {
          BoxedVariable<Variable> boxArray;
          if (this.IsVariableValueFlowFromArray(new BoxedVariable<Variable>(variable), out boxArray, out unmodifiedFromMethodEntry)
            && boxArray.TryUnpackVariable(out array))
          {
            return true;
          }

          array = default(Variable);
          return false;
        }

        [Pure]
        public SymbolicExpressionTracker<BoxedVariable<Variable>, BoxedExpression> SymbolicConditions(BoxedVariable<Variable> variable)
        {
          Contract.Requires(variable != null);
          Contract.Ensures(Contract.Result<SymbolicExpressionTracker<BoxedVariable<Variable>, BoxedExpression>>() != null);

          int id;
          if (TryGetIdForState(AdditionalStates.ArrayRefinement, out id))
          {
            var state = this.PluginAbstractStateAt(id) as ArrayTracking;
            ScalarFromArrayTracking arrayTracking;
            if (state != null && state.TryGetValue(variable, out arrayTracking))
            {
              Contract.Assume(arrayTracking != null);

              return arrayTracking.Conditions;
            }
          }

          return SymbolicExpressionTracker<BoxedVariable<Variable>, BoxedExpression>.Unknown;
        }

        [Pure]
        public DisInterval IntervalIfNotNull(Variable value, BoxedExpression asBox = null)
        {
          Contract.Ensures(Contract.Result<DisInterval>() != null);

          if (!this.HasNonNullInfo)
          {
            return DisInterval.UnknownInterval;
          }
          var nnState = this.NonNull;
          if (nnState.IsNull(value))
          {
            return DisInterval.For(0);
          }
          else if (nnState.IsNonNull(value))
          {
            return DisInterval.NotZero;
          }
          else
          {
            // For some reason, sometimes the non-null analysis does not record in the nnState when a variable is the constant "null". 
            // We do the explicit check here

            if (asBox != null && asBox.IsNull)
            {
              return DisInterval.For(0);
            }
          }
          return DisInterval.UnknownInterval;
        }
        #endregion

        #region Factory methods

        [DebuggerStepThrough]
        [Pure]
        public ArrayState UpdateArray(ArraySegmentationEnvironment<NonRelationalValueAbstraction<BoxedVariable<Variable>, BoxedExpression>, BoxedVariable<Variable>, BoxedExpression> newArrayState)
        {
          Contract.Requires(newArrayState != null);

          Contract.Ensures(Contract.Result<ArrayState>() != null);

          return new ArrayState(newArrayState, this.numericalState, this.nonnullState, this.otherStates, this.mappings);
        }

        [DebuggerStepThrough]
        [Pure]
        public ArrayState UpdateNumerical(INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> newNumerical)
        {
          Contract.Requires(newNumerical != null);

          Contract.Ensures(Contract.Result<ArrayState>() != null);

          return new ArrayState(this.arrayState, newNumerical, this.nonnullState, this.otherStates, this.mappings);
        }

        [DebuggerStepThrough]
        [Pure]
        public ArrayState UpdateNonNull(Analyzers.NonNull.TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>.Domain newNonNull)
        {
          Contract.Ensures(Contract.Result<ArrayState>() != null);

          return new ArrayState(this.arrayState, this.numericalState, newNonNull, this.otherStates, this.mappings);
        }

        [DebuggerStepThrough]
        [Pure]
        public ArrayState UpdatePlugins(IAbstractDomainForEnvironments<BoxedVariable<Variable>, BoxedExpression>[] newPlugins)
        {
          Contract.Requires(newPlugins != null);
          Contract.Requires(newPlugins.Length == this.PluginsCount);
          Contract.Requires(Contract.ForAll(newPlugins, x => x != null));

          Contract.Ensures(Contract.Result<ArrayState>() != null);

          return new ArrayState(this.arrayState, this.numericalState, this.nonnullState, newPlugins, this.mappings);
        }

        [DebuggerStepThrough]
        [Pure]
        public ArrayState UpdatePluginAt(int index, IAbstractDomainForEnvironments<BoxedVariable<Variable>, BoxedExpression> newPlugin)
        {
          Contract.Requires(index >= 0);
          Contract.Requires(index < this.PluginsCount);
          Contract.Requires(newPlugin != null);

          Contract.Ensures(Contract.Result<ArrayState>() != null);

          Contract.Assume(newPlugin.GetType().Equals(this.otherStates[index].GetType()), "Runtime check to ensure that we replace the same abstract domain type");

          var newArray = new IAbstractDomainForEnvironments<BoxedVariable<Variable>, BoxedExpression>[this.otherStates.Length];

          System.Array.Copy(this.otherStates, newArray, newArray.Length);
          newArray[index] = newPlugin;

          return this.UpdatePlugins(newArray);
        }

        #endregion

        #region ToString
        public override string ToString()
        {
          var pluginStates = string.Format("plugin states: ({0} in total)", this.otherStates.Length);

          if (this.otherStates.Length > 0)
          {
            foreach (var state in this.otherStates)
            {
              var str = state.ToString();

              pluginStates += Environment.NewLine + str;
            }
          }

          return String.Format("num:{0}{1}{0}nonnull:{0}{2}{0}arr:{0}{3}{0}others:{0}{4}{0}",
            Environment.NewLine,
            this.numericalState.ToString(),
            this.nonnullState.HasValue ? this.nonnullState.Value.ToString() : "<>",
            this.arrayState.ToString(),
            pluginStates
            );
        }
        #endregion

        #region Domain operations
        public bool IsBottom
        {
          get 
          {
            if (this.arrayState.IsBottom || this.numericalState.IsBottom)
              return true;

            if (this.nonnullState.HasValue)
            {
              if (this.nonnullState.Value.NonNulls.IsBottom || this.nonnullState.Value.Nulls.IsBottom)
              {
                return true;
              }
            }

            foreach (var plugin in this.otherStates)
            {
              if (plugin.IsBottom)
                return true;
            }

            return false;
          }
        }
        
        public ArrayState Bottom
        {
          get
          {
            return new ArrayState(this.arrayState.Bottom, this.numericalState.Bottom as INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression>);
          }
        }

        public ArrayState Top
        {
          get
          {
            var tops = new IAbstractDomainForEnvironments<BoxedVariable<Variable>, BoxedExpression>[this.otherStates.Length];
            var i = 0;
            foreach (var other in this.otherStates)
            {
              tops[i] = other.Top as IAbstractDomainForEnvironments<BoxedVariable<Variable>, BoxedExpression>;
              i++;
            }

            return new ArrayState(
              this.arrayState.Top,
              this.numericalState.Top as INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression>,
              null, // F: hack - should add the Top getter to the NonNull abstract state
              tops,
              this.mappings
              );
          }
        }

        public bool IsTop
        {
          get 
          {
            if (!this.arrayState.IsTop || !this.numericalState.IsTop)
            {
              return false;
            }
            if (this.nonnullState.HasValue)
            {
              if (!this.nonnullState.Value.NonNulls.IsTop || !this.nonnullState.Value.Nulls.IsTop)
              {
                return false;
              }
            }

            foreach (var plugin in this.otherStates)
            {
              if (!plugin.IsTop)
              {
                return false;
              }
            }

            return true;
          }
        }

        #endregion

        #region Abstract operations

        // From measurements it seems we spend very little time in this method
        public bool LessEqual(ArrayState a)
        {
          Contract.Requires(this.PluginsCount == a.PluginsCount);

          bool result;
          if (AbstractDomainsHelper.TryTrivialLessEqual(this, a, out result))
          {
            return result;
          }

          if (!this.Array.LessEqual(a.Array))
          {
            return false;
          }

          if (!this.Numerical.LessEqual(a.Numerical))
          {
            return false;
          }

          for (var i = 0; i < this.PluginsCount; i++)
          {
            if (!this.PluginAbstractStateAt(i).LessEqual(a.PluginAbstractStateAt(i)))
            {
              return false;
            }
          }

          return true;
        }

        public ArrayState Join(ArrayState a)
        {
          Contract.Requires(this.PluginsCount == a.PluginsCount);

          Contract.Ensures(Contract.Result<ArrayState>() != null);
          Contract.Ensures(Contract.Result<ArrayState>().PluginsCount == this.PluginsCount);

          ArrayState result;
          if (AbstractDomainsHelper.TryTrivialJoin(this, a, out result))
          {
            return result;
          }

          ArraySegmentationEnvironment<NonRelationalValueAbstraction<BoxedVariable<Variable>, BoxedExpression>, BoxedVariable<Variable>, BoxedExpression> array = null;
          INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> numerical = null;

#if PARALLEL
          var arrayTask = Task.Run(() => array = this.Array.Join(a.Array));
          var numericalTask = Task.Run(() => numerical = this.Numerical.Join(a.Numerical) as INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression>);
#else
          array = this.Array.Join(a.Array);
          numerical = this.Numerical.Join(a.Numerical) as INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression>;
#endif
          var plugins = new IAbstractDomainForEnvironments<BoxedVariable<Variable>, BoxedExpression>[this.PluginsCount];

          for (var i = 0; i < this.PluginsCount; i++)
          {
            plugins[i] = this.PluginAbstractStateAt(i).Join(a.PluginAbstractStateAt(i)) as IAbstractDomainForEnvironments<BoxedVariable<Variable>, BoxedExpression>;
          }

#if PARALLEL
          Task.WaitAll(arrayTask, numericalTask);
#endif
          return new ArrayState(array, numerical, plugins, this.mappings);
        }

        public ArrayState Meet(ArrayState a)
        {
          Contract.Requires(this.PluginsCount == a.PluginsCount);

          Contract.Ensures(Contract.Result<ArrayState>() != null);
          Contract.Ensures(Contract.Result<ArrayState>().PluginsCount == this.PluginsCount);

          ArrayState result;
          if (AbstractDomainsHelper.TryTrivialMeet(this, a, out result))
          {
            return result;
          }

#if DEBUG
          // F: for debugging
          result = null;
#endif
          var array = this.Array.Meet(a.Array);
          var numerical = this.Numerical.Meet(a.Numerical) as INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression>;

          var plugins = new IAbstractDomainForEnvironments<BoxedVariable<Variable>, BoxedExpression>[this.PluginsCount];

          for (var i = 0; i < this.PluginsCount; i++)
          {
            plugins[i] = this.PluginAbstractStateAt(i).Meet(a.PluginAbstractStateAt(i)) as IAbstractDomainForEnvironments<BoxedVariable<Variable>, BoxedExpression>;
          }

          return new ArrayState(array, numerical, plugins, this.mappings);
        }

        public ArrayState Widening(ArrayState a)
        {
          Contract.Requires(this.PluginsCount == a.PluginsCount);

          Contract.Ensures(Contract.Result<ArrayState>() != null);
          Contract.Ensures(Contract.Result<ArrayState>().PluginsCount == this.PluginsCount);

          ArrayState result;
          if (AbstractDomainsHelper.TryTrivialJoin(this, a, out result))
          {
            return result;
          }

#if DEBUG
          // F: for debugging
          result = null;
#endif
          ArraySegmentationEnvironment<NonRelationalValueAbstraction<BoxedVariable<Variable>, BoxedExpression>, BoxedVariable<Variable>, BoxedExpression> array = null;
          INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> numerical = null;

#if PARALLELWIDENING
          var arrayTask = Task.Run(() => array = this.Array.Widening(a.Array));
          var numericalTask = Task.Run(() => numerical = this.Numerical.Widening(a.Numerical) as INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression>);
#else
          array = this.Array.Widening(a.Array);
          numerical = this.Numerical.Widening(a.Numerical) as INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression>;
#endif

          var plugins = new IAbstractDomainForEnvironments<BoxedVariable<Variable>, BoxedExpression>[this.PluginsCount];

          for (var i = 0; i < this.PluginsCount; i++)
          {
            plugins[i] = this.PluginAbstractStateAt(i).Widening(a.PluginAbstractStateAt(i)) as IAbstractDomainForEnvironments<BoxedVariable<Variable>, BoxedExpression>;
          }

#if PARALLELWIDENING
          Task.WaitAll(arrayTask, numericalTask);
#endif
          return new ArrayState(array, numerical, plugins, this.mappings);
        }

        
        #endregion

        #region DomainSpecific facts

        public void AssumeDomainSpecificFact(DomainSpecificFact fact)
        {
          this.Numerical.AssumeDomainSpecificFact(fact);
          this.Array.AssumeDomainSpecificFact(fact);
          foreach (var ad in this.otherStates)
          {
            ad.AssumeDomainSpecificFact(fact);
          }
        }

        #endregion

        #region Duplicate

        internal ArrayState Duplicate()
        {
          Contract.Ensures(Contract.Result<ArrayState>() != null);

          var dupNumerical = this.numericalState.Clone() as INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression>;
          
          // We know all the other domains are functional

          return new ArrayState(this.arrayState, dupNumerical, this.nonnullState, this.otherStates, this.mappings);
        }

        #endregion

        #region Only to make the type system happy
        #region IAbstractDomainForEnvironments<BoxedVariable<Variable>,BoxedVariable<Variable>> Members

        string IAbstractDomainForEnvironments<BoxedVariable<Variable>, BoxedExpression>.ToString(BoxedExpression exp)
        {
          throw new NotImplementedException();
        }

        #endregion

        #region IAbstractDomain Members

        bool IAbstractDomain.LessEqual(IAbstractDomain a)
        {
          return this.LessEqual(a as ArrayState);
        }

        IAbstractDomain IAbstractDomain.Bottom
        {
          get { return this.Bottom; }
        }

        IAbstractDomain IAbstractDomain.Top
        {
          get { return this.Top; }
        }

        IAbstractDomain IAbstractDomain.Join(IAbstractDomain a)
        {
          return this.Join(a as ArrayState);
        }

        IAbstractDomain IAbstractDomain.Meet(IAbstractDomain a)
        {
          return this.Meet(a as ArrayState);
        }

        IAbstractDomain IAbstractDomain.Widening(IAbstractDomain prev)
        {
          return this.Widening(prev as ArrayState);
        }

        // At the moment we do the To<> only of the numerical state 
        T IAbstractDomain.To<T>(IFactory<T> factory)
        {
          var result = this.Numerical.To(factory);

          return result;
        }

        #endregion

        #region ICloneable Members

        object ICloneable.Clone()
        {
          var cnumerical = this.numericalState.Clone() as INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression>;
          var carray = this.arrayState.DuplicateMe();
          var cnonnull = this.nonnullState;
          var cRest = new IAbstractDomainForEnvironments<BoxedVariable<Variable>, BoxedExpression>[this.otherStates.Length];
          var i = 0;
          foreach (var other in this.otherStates)
          {
            var cloned = other.Clone() as IAbstractDomainForEnvironments<BoxedVariable<Variable>, BoxedExpression>;
            Contract.Assume(cloned != null);
            cRest[i] = cloned;
            i++;
          }

          return new ArrayState(carray, cnumerical, cnonnull, cRest, this.mappings);
        }
        
        #endregion

        #region IPureExpressionAssignments<BoxedVariable<Variable>,BoxedVariable<Variable>> Members

        List<BoxedVariable<Variable>> IPureExpressionAssignments<BoxedVariable<Variable>, BoxedExpression>.Variables
        {
          get { throw new NotImplementedException(); }
        }

        void IPureExpressionAssignments<BoxedVariable<Variable>, BoxedExpression>.AddVariable(BoxedVariable<Variable> var)
        {
          throw new NotImplementedException();
        }

        void IPureExpressionAssignments<BoxedVariable<Variable>, BoxedExpression>.Assign(BoxedExpression x, BoxedExpression exp)
        {
          throw new NotImplementedException();
        }

        void IPureExpressionAssignments<BoxedVariable<Variable>, BoxedExpression>.ProjectVariable(BoxedVariable<Variable> var)
        {
          throw new NotImplementedException();
        }

        void IPureExpressionAssignments<BoxedVariable<Variable>, BoxedExpression>.RemoveVariable(BoxedVariable<Variable> var)
        {
          throw new NotImplementedException();
        }

        void IPureExpressionAssignments<BoxedVariable<Variable>, BoxedExpression>.RenameVariable(BoxedVariable<Variable> OldName, BoxedVariable<Variable> NewName)
        {
          throw new NotImplementedException();
        }

        #endregion

        #region IPureExpressionTest<BoxedVariable<Variable>,BoxedVariable<Variable>> Members

        IAbstractDomainForEnvironments<BoxedVariable<Variable>, BoxedExpression> IPureExpressionTest<BoxedVariable<Variable>, BoxedExpression>.TestTrue(BoxedExpression guard)
        {
          // This method is only invoked when filtering the inferred boxed expressions,

          // TODO, forward the exp to the other domains
          var numerical = this.Numerical.TestTrue(guard) as INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression>;

          Contract.Assume(numerical != null);

          return this.UpdateNumerical(numerical);
        }

        IAbstractDomainForEnvironments<BoxedVariable<Variable>, BoxedExpression> IPureExpressionTest<BoxedVariable<Variable>, BoxedExpression>.TestFalse(BoxedExpression guard)
        {
          return this;
        }

        FlatAbstractDomain<bool> IPureExpressionTest<BoxedVariable<Variable>, BoxedExpression>.CheckIfHolds(BoxedExpression exp)
        {
          return this.Numerical.CheckIfHolds(exp);
        }

        #endregion

        #region IAssignInParallel<BoxedVariable<Variable>,BoxedVariable<Variable>> Members

        void IAssignInParallel<BoxedVariable<Variable>, BoxedExpression>.AssignInParallel(
          Dictionary<BoxedVariable<Variable>, DataStructures.FList<BoxedVariable<Variable>>> sourcesToTargets, 
          Converter<BoxedVariable<Variable>, BoxedExpression> convert)
        {
          throw new NotImplementedException();
        }

        #endregion

        #endregion

      }
    }
  }
}