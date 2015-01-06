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
using Microsoft.Research.DataStructures;
using System.Diagnostics.Contracts;
using Microsoft.Research.AbstractDomains;
namespace Microsoft.Research.CodeAnalysis
{
  public static partial class AnalysisWrapper
  {
    public static partial class TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>
      where Variable : IEquatable<Variable>
      where Expression : IEquatable<Expression>
      where Type : IEquatable<Type>
    {
      // packs together all the caches we keep during the analysiss
      [ContractVerification(true)]
      public class GenericValueAnalysisCacher
      {
        private ToBoxedExpressionConverter var2be;
        private Dictionary<Pair<APC, Variable>, BoxedExpression> refinedDisjunctions;
        private DoubleTable<APC, APC, Dictionary<BoxedVariable<Variable>, FList<BoxedVariable<Variable>>>> refinedMappingCache;

        readonly private ExpressionCacheMode ExpCachingMode;
        readonly private IExpressionContext<Local, Parameter, Method, Field, Type, Expression, Variable> Context;
        readonly private IFullExpressionDecoder<Type, Variable, Expression> Decoder;

        public GenericValueAnalysisCacher(
          ExpressionCacheMode expCachingMode, 
          IExpressionContext<Local, Parameter, Method, Field, Type, Expression, Variable> context,
          IFullExpressionDecoder<Type, Variable, Expression> decoder
          )
        {
          this.ExpCachingMode = expCachingMode;
          this.Context = context;
          this.Decoder = decoder;
        }

        public void ClearCaches()
        {
          this.var2be = null;
          this.refinedMappingCache = null;
          this.refinedDisjunctions = null;
        }
                
        public Dictionary<Pair<APC, Variable>, BoxedExpression> RefinedDisjunctions
        {
          get
          {
            if (this.refinedDisjunctions == null)
            {
              this.refinedDisjunctions = new Dictionary<Pair<APC, Variable>, BoxedExpression>();
            }

            return this.refinedDisjunctions;
          }
        }

        public DoubleTable<APC, APC, Dictionary<BoxedVariable<Variable>, FList<BoxedVariable<Variable>>>> RefinedMappingCache
        {
          get
          {
            if (this.refinedMappingCache == null)
            {
              this.refinedMappingCache = new DoubleTable<APC, APC, Dictionary<BoxedVariable<Variable>, FList<BoxedVariable<Variable>>>>();
            }
            return this.refinedMappingCache;
          }
        }

        public Dictionary<BoxedVariable<Variable>, FList<BoxedVariable<Variable>>> GetRefinedMap(
          APC from, APC to, IFunctionalMap<Variable, FList<Variable>> sourceTargetMap, Renamings<Variable> renamings, int maxVarsInOneRenaming = Int32.MaxValue)
        {
          Contract.Requires(sourceTargetMap != null);

          Dictionary<BoxedVariable<Variable>, FList<BoxedVariable<Variable>>> result;

          if (!this.RefinedMappingCache.TryGetValue(from, to, out result))
          {
            result = sourceTargetMap.RefineMapToBoxedVariables(renamings, maxVarsInOneRenaming);
            this.RefinedMappingCache.Add(from, to, result);
          }
  
          return result;
        }

        public BoxedExpression ToBoxedExpression(APC pc, Variable var, Func<BoxedExpression, BoxedExpression> extraTransformation)
        {
          if (this.var2be == null)
          {
            this.var2be = new ToBoxedExpressionConverter(ExpCachingMode, this.Context, this.Decoder);
          }

          return this.var2be.ToBoxedExpression(pc, var, extraTransformation);
        }

        public override string ToString()
        {
          return "cache manager";
        }

        #region Caching of Variable -> BoxedExpression conversion

        [ContractVerification(true)]
        class ToBoxedExpressionConverter
        {
          [ContractInvariantMethod]
          void ObjectInvariant()
          {
            Contract.Invariant(this.context != null);
            Contract.Invariant(cachingMode == ExpressionCacheMode.None || (this.weakrefMap != null) == (this.fullMap == null));
            Contract.Invariant(cachingMode != ExpressionCacheMode.Mem || this.weakrefMap != null);
            Contract.Invariant(cachingMode != ExpressionCacheMode.Time || this.fullMap != null);
          }

          readonly private IExpressionContextData<Local, Parameter, Method, Field, Type, Expression, Variable> context;
          readonly private IFullExpressionDecoder<Type, Variable, Expression> outdecoder;

          readonly private ExpressionCacheMode cachingMode;
          readonly private Dictionary<Pair<APC, Variable>, WeakReference> weakrefMap;
          readonly private Dictionary<Pair<APC, Variable>, BoxedExpression> fullMap;

          public ToBoxedExpressionConverter(
            ExpressionCacheMode mode,
            IExpressionContext<Local, Parameter, Method, Field, Type, Expression, Variable> context,
            IFullExpressionDecoder<Type, Variable, Expression> outdecoder)
          {
            Contract.Requires(context != null);

            this.cachingMode = mode;
            this.context = context.ExpressionContext;
            this.outdecoder = outdecoder;

            if (mode == ExpressionCacheMode.Mem)
            {
              this.weakrefMap = new Dictionary<Pair<APC, Variable>, WeakReference>();
              Contract.Assert((this.weakrefMap != null) == (this.fullMap == null), "Help the WP of Clousot prove the invariant");
            }
            else if (mode == ExpressionCacheMode.Time)
            {
              this.fullMap = new Dictionary<Pair<APC, Variable>, BoxedExpression>();
            }
          }

          public BoxedExpression/*?*/ ToBoxedExpression(APC pc, Variable var, Func<BoxedExpression, BoxedExpression> extraTransformation)
          {
            // Contract.Ensures(Contract.Result<BoxedExpression>() != null);

            BoxedExpression result;

            switch (this.cachingMode)
            {
              case ExpressionCacheMode.Mem:
                {
                  result = ToBoxedExpressionMem(pc, var);
                  break;
                }
              case ExpressionCacheMode.Time:
                {
                  result = ToBoxedExpressionTime(pc, var);
                  break;
                }
              case ExpressionCacheMode.None:
                {
                  result = BoxedExpression.For(this.context.Refine(pc, var), this.outdecoder);
                  break;
                }
              default:
                Contract.Assert(false);
                throw new AbstractInterpretationException("Impossible case");
            }

            var next =  extraTransformation != null? extraTransformation(result) : result;

            return next;
          }

          private BoxedExpression ToBoxedExpressionMem(APC pc, Variable var)
          {
            Contract.Requires(this.weakrefMap != null);
            Contract.Ensures(Contract.Result<BoxedExpression>() != null);

            BoxedExpression result;
            WeakReference wr;

            var key = new Pair<APC, Variable>(pc, var);
            if (this.weakrefMap.TryGetValue(key, out wr) && wr != null && wr.IsAlive)
            {
              result = wr.Target as BoxedExpression;
              Contract.Assume(result != null);
            }
            else
            {
              result = BoxedExpression.For(this.context.Refine(pc, var), this.outdecoder);
              this.weakrefMap[key] = new WeakReference(result);
            }

            return result;
          }

          private BoxedExpression ToBoxedExpressionTime(APC pc, Variable var)
          {
            Contract.Requires(this.fullMap != null);

            Contract.Ensures(Contract.Result<BoxedExpression>() != null);

            BoxedExpression result;

            var key = new Pair<APC, Variable>(pc, var);
            if (this.fullMap.TryGetValue(key, out result))
            {
              Contract.Assume(result != null);
              return result;
            }
            else
            {
              result = BoxedExpression.For(this.context.Refine(pc, var), this.outdecoder);
              this.fullMap[key] = result;
            }
            
            return result;
          }
        }

        #endregion
      }

    }
  }
}