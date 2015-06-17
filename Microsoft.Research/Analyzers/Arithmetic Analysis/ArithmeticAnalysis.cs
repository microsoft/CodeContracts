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
//using System.Diagnostics.Contracts;
using Microsoft.Research.AbstractDomains;
using Microsoft.Research.AbstractDomains.Expressions;
using Microsoft.Research.AbstractDomains.Numerical;
using Microsoft.Research.DataStructures;
using ADomainKind = Microsoft.Research.CodeAnalysis.Analyzers.DomainKind;
using Generics = System.Collections.Generic;

namespace Microsoft.Research.CodeAnalysis
{
  using Generics;
  using System.Diagnostics;

  public static partial class AnalysisWrapper
  {
    /// <summary>
    /// The entry point for running the arithmetic analysis
    /// </summary>
    public static IMethodResult<Variable> RunTheArithmeticAnalysis<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>
    (
      string methodName,
      IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> driver,
      ADomainKind adomain, Analyzers.Arithmetic.ArithmeticOptions options,
      Predicate<APC> cachePCs
     )
      where Variable : IEquatable<Variable>
      where Expression : IEquatable<Expression>
      where Type : IEquatable<Type>
    {
      return TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>.HelperForRunTheArithmeticAnalysis(methodName, adomain, driver, options, cachePCs);
    }


    public static partial class TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>
      where Variable : IEquatable<Variable>
      where Expression : IEquatable<Expression>
      where Type:IEquatable<Type>
    {
      public static IMethodResult<Variable> HelperForRunTheArithmeticAnalysis
      (
        string methodName, ADomainKind adomain,
        IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions>/*!*/ driver,
        Analyzers.Arithmetic.ArithmeticOptions options,
        Predicate<APC> cachePCs
      )
      {
        return RunTheAnalysis(methodName, driver, new ArithmeticAnalysis(methodName, driver, options, adomain, cachePCs));
      }

      #region Facility to forward operations on the abstract domain (implementation of IAbstractDomainOperations)
      public class AbstractOperationsImplementationArithmetic : IAbstractDomainOperations<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression>>
      {
        public bool LookupState(IMethodResult<Variable> mr, APC pc, out INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> astate)
        {
          astate = null;
          ArithmeticAnalysis an = mr as ArithmeticAnalysis;
          if (an == null)
            return false;

          return an.PreStateLookup(pc, out astate);
        }

        public INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> Join(IMethodResult<Variable> mr, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> astate1, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> astate2)
        {
          ArithmeticAnalysis an = mr as ArithmeticAnalysis;
          if (an == null)
            return null;

          bool bWeaker;
          return an.Join(new Pair<APC, APC>(), astate1, astate2, out bWeaker, false);
        }

        public List<BoxedExpression> ExtractAssertions(
          IMethodResult<Variable> mr,
          INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> astate,
          IExpressionContext<Local, Parameter, Method, Field, Type, Expression, Variable> context,
          IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> metaDataDecoder)
        {
          ArithmeticAnalysis an = mr as ArithmeticAnalysis;
          if (an == null)
            return null;

          BoxedExpressionReader<Local, Parameter, Method, Field, Property, Event, Type, Variable, Expression, Attribute, Assembly> br = new BoxedExpressionReader<Local, Parameter, Method, Field, Property, Event, Type, Variable, Expression, Attribute, Assembly>(context, metaDataDecoder);

          return an.ToListOfBoxedExpressions(astate, br);
        }

        public bool AssignInParallel(IMethodResult<Variable> mr, ref INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> astate, Dictionary<BoxedVariable<Variable>, FList<BoxedVariable<Variable>>> mapping, Converter<BoxedVariable<Variable>, BoxedExpression> convert)
        {
          ArithmeticAnalysis an = mr as ArithmeticAnalysis;
          if (an == null)
            return false;

          astate.AssignInParallel(mapping, convert);
          return true;
        }
      }
      #endregion


      public class ArithmeticAnalysis
          : NumericalAnalysis<Analyzers.Arithmetic.ArithmeticOptions>
      { 
        public readonly Analyzers.Arithmetic.ArithmeticOptions myOptions;
        
        #region Constructor
        public ArithmeticAnalysis
        (
          string methodName,
          IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> mdriver,
          Analyzers.Arithmetic.ArithmeticOptions options,
          ADomainKind abstractDomain,
          Predicate<APC> cachePCs
        )
          : base(methodName, abstractDomain, mdriver, options, cachePCs)
        {
          this.myOptions = options;
        }

        #endregion

        #region Overridden
        public override INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> GetTopValue()
        {
          INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> result;

          result = TopNumericalDomain<BoxedVariable<Variable>, BoxedExpression>.Singleton;

          if (this.myOptions.AnalyzeFloats)
          {

            var fp = new IntervalEnvironment_IEEE754<BoxedVariable<Variable>, BoxedExpression>(this.ExpressionManager);
            var fpTypes = new FloatTypes<BoxedVariable<Variable>, BoxedExpression>(this.ExpressionManager);

            var refined = new RefinedWithIntervalsIEEE<INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression>, BoxedVariable<Variable>, BoxedExpression>(fp, result, this.ExpressionManager);

            result = new RefinedWithFloatTypes<RefinedWithIntervalsIEEE<INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression>, BoxedVariable<Variable>, BoxedExpression>, BoxedVariable<Variable>, BoxedExpression>(fpTypes, refined, this.ExpressionManager);
          }

          return result;
        }

        #endregion 

        #region Transfer functions

        #region Unary
        public override INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> Unary(APC pc, UnaryOperator op, bool overflow, bool unsigned, Variable dest, Variable source, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          data = FloatTypesUnary(pc, op, dest, source, data); 

          // Handle the conversion from an int
          if (op == UnaryOperator.Conv_r4 || op == UnaryOperator.Conv_r8)
          {
            var type = this.Context.ValueContext.GetType(this.Context.MethodContext.CFG.Post(pc), source);
            if (type.IsNormal && this.DecoderForMetaData.IsIntegerType(type.Value))
            {
              var intv = this.DecoderForMetaData.GetDisIntervalForType(type.Value);

              long low, upp;
              if (intv.IsNormal() && intv.LowerBound.TryInt64(out low) && intv.UpperBound.TryInt64(out upp))
              {
                var floatIntv =
                  op == UnaryOperator.Conv_r8 ?
                  Interval_IEEE754.For((double)low, (double)upp) :
                  Interval_IEEE754.For((float)low, (float)upp);

                data.AssumeDomainSpecificFact(new DomainSpecificFact.AssumeInFloatInterval(new BoxedVariable<Variable>(dest), floatIntv));
              }
            }
          }
          
          return base.Unary(pc, op, overflow, unsigned, dest, source, data);
        }

        #endregion

        public override INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> Binary(APC pc, BinaryOperator op, Variable dest, Variable s1, Variable s2, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          data = FloatTypesBinary(pc, op, dest, s1, s2, data);

          return base.Binary(pc, op, dest, s1, s2, data);
        }

        public override INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> Ldconst(APC pc, object constant, Type type, Variable dest, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {

          data = FloatTypesLdConst(pc, dest, type, constant ,data);

          return base.Ldconst(pc, constant, type, dest, data);
        }

        public override INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> Ldfld(APC pc, Field field, bool @volatile, Variable dest, Variable obj, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          data = FloatTypesLdfld(pc, dest, obj, data);

          return base.Ldfld(pc, field, @volatile, dest, obj, data);
        }

        public override INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> Ldelem(APC pc, Type type, Variable dest, Variable array, Variable index, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          data = FloatTypesLdElem(pc, dest, data);

          return base.Ldelem(pc, type, dest, array, index, data);
        }

        public override INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> Stfld(APC pc, Field field, bool @volatile, Variable obj, Variable value, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          data = FloatTypesStfld(pc, obj, value, data);

          return base.Stfld(pc, field, @volatile, obj, value, data);
        }

        public override INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> Ldarg(APC pc, Parameter argument, bool isOld, Variable dest, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          data = FloatTypesLdarg(pc, dest, data);

          return base.Ldarg(pc, argument, isOld, dest, data);
        }

        public override INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> Call<TypeList, ArgList>(APC pc, Method method, bool tail, bool virt, TypeList extraVarargs, Variable dest, ArgList args, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          data = base.Call<TypeList, ArgList>(pc, method, tail, virt, extraVarargs, dest, args, data);

          var containingType = this.DecoderForMetaData.DeclaringType(method);

          // Set the float types
          data = FloatTypesCall(pc, method, dest, data);

          return data;
        }

        #endregion

        #region Inference of Float types

        private INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> FloatTypesCall(APC pc, Method method, Variable dest, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          // Optimistic hypothesis: from getters we return a 32/64 bit floating point

          var type = this.Context.ValueContext.GetType(this.Context.MethodContext.CFG.Post(pc), dest);
          if (type.IsNormal &&
            this.DecoderForMetaData.IsPropertyGetter(method))
          {
            if (this.DecoderForMetaData.System_Double.Equals(type.Value))
            {
              data.SetFloatType(new BoxedVariable<Variable>(dest), ConcreteFloat.Float64);
            }
            else if(this.DecoderForMetaData.System_Single.Equals(type.Value)) 
            {
              data.SetFloatType(new BoxedVariable<Variable>(dest), ConcreteFloat.Float32);
            }
          }

          return data;
        }

        private INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> FloatTypesBinary(APC pc, BinaryOperator op, Variable dest, Variable s1, Variable s2, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          ConcreteFloat t1, t2;

          if (TryFloatType(pc, s1, out t1) && TryFloatType(pc, s2, out t2))
          {
            var prevType = data.GetFloatType(ToBoxedVariable(dest));

            if (prevType.IsTop)
            {
              data.SetFloatType(ToBoxedVariable(dest), ConcreteFloat.Float80);
            }
            else
            {
              data.SetFloatType(ToBoxedVariable(dest), ConcreteFloat.Uncompatible);
            }
          }

          return data;
        }

        private INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> FloatTypesStfld(APC pc, Variable obj, Variable value, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          ConcreteFloat type;
          if (TryFloatType(this.Context.MethodContext.CFG.Post(pc), value, out type))
          {
            data.SetFloatType(ToBoxedVariable(value), type);
          }

          return data;
        }

        private INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> FloatTypesLdfld(APC pc, Variable dest, Variable obj, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          ConcreteFloat type;
          if (TryFloatType(this.Context.MethodContext.CFG.Post(pc), dest, out type))
          {
            data.SetFloatType(ToBoxedVariable(dest), type); 
          }

          return data;
        }


        private INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> FloatTypesLdElem(APC pc, Variable dest, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          ConcreteFloat type;
          if (TryFloatType(this.Context.MethodContext.CFG.Post(pc), dest, out type))
          {
            data.SetFloatType(ToBoxedVariable(dest), type);
          }

          return data;
        }

        private INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> FloatTypesLdarg(APC pc, Variable dest, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          ConcreteFloat type;
          if (TryFloatType(this.Context.MethodContext.CFG.Post(pc), dest, out type) && !data.GetFloatType(new BoxedVariable<Variable>(dest)).IsNormal())
          {
            data.SetFloatType(ToBoxedVariable(dest), ConcreteFloat.Float80);
          }

          return data;
        }

        private INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> FloatTypesLdConst(APC pc, Variable dest, Type type, object constant, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          if (type.Equals(this.DecoderForMetaData.System_Double))
          {
            data.SetFloatType(ToBoxedVariable(dest), ConcreteFloat.Float64);
          }

          if (type.Equals(this.DecoderForMetaData.System_Single))
          {
            data.SetFloatType(ToBoxedVariable(dest), ConcreteFloat.Float32);
          }

          return data;
        }

        private INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> FloatTypesUnary(APC pc, UnaryOperator op, Variable dest, Variable source, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          switch (op)
          {
            case UnaryOperator.Conv_r4:
              data.SetFloatType(ToBoxedVariable(dest), ConcreteFloat.Float32);
              break;

            case UnaryOperator.Conv_r8:
              data.SetFloatType(ToBoxedVariable(dest), ConcreteFloat.Float64);
              break;

            default:
            // do nothing;
              break;
          }

          return data;
        }

        private bool TryFloatType(APC pc, Variable var, out ConcreteFloat type)
        {
          var flattype = this.Context.ValueContext.GetType(pc, var);

          if(flattype.IsNormal)
          {
            var emb = flattype.Value;

            if(emb.Equals(this.DecoderForMetaData.System_Single))
            {
              type = ConcreteFloat.Float32;
              return true;
            }

            if(emb.Equals(this.DecoderForMetaData.System_Double))
            {
              type = ConcreteFloat.Float64;
              return true;
            }
          }

          type = default(ConcreteFloat);
          return false;
        }

        #endregion

      }
    }
  }
}