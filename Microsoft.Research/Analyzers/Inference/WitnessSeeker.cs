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
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Microsoft.Research.CodeAnalysis.Inference
{
  public class PostconditionWitnessSeaker<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions>
    where Variable : IEquatable<Variable>
    where Expression : IEquatable<Expression>
    where Type : IEquatable<Type>
    where LogOptions : IFrameworkLogOptions

  {
    private readonly IFactQuery<BoxedExpression, Variable> facts;
    private readonly IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions> mdriver;

    public PostconditionWitnessSeaker(IFactQuery<BoxedExpression, Variable> facts,
      IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions> mdriver)
    {
      this.facts = facts;
      this.mdriver = mdriver;
    }

    public bool TryGetAWitness(APC pc, TimeOutChecker timeout, out bool mayReturnNull /* TODO make this more expressive*/)
    {
      mayReturnNull = false;
      if(timeout.HasAlreadyTimeOut)
      {
        return false;
      }

      var searchForAWitness = new SearchWitnesses(this.facts, this.mdriver, timeout);
      return searchForAWitness.TryCollectWitnesses(out mayReturnNull);
    }

    internal class SearchWitnesses
      : GenericNecessaryConditionsGenerator<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions, Witness>
    {
      public bool Found { get; private set; }
      public Witness Result { get; private set; }

      public SearchWitnesses
             (
      IFactQuery<BoxedExpression, Variable> facts,
      IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, LogOptions> mdriver,
      TimeOutChecker timeout)
        : base(mdriver.CFG.NormalExit, facts, mdriver, timeout)
      {
        this.Found = false;
        this.Result = Witness.None;
      }

      public bool TryCollectWitnesses(out bool mayReturnNull)
      {
        Trace("Try to collect a witness for the return value");

        mayReturnNull = false;
        if (this.timeout.HasAlreadyTimeOut)
        {
          return false;
        }

        try
        {
          this.timeout.Start();
          Variable retVar;

          if (this.Mdriver.Context.ValueContext.TryResultValue(this.pcCondition, out retVar))
          {
            var initialState = new Witness(BoxedExpression.Var(retVar));
            Visit(this.pcCondition, initialState, 0);
            if (this.Found)
            {
              mayReturnNull = this.Result.State == Witness.Kind.ReturnNull || this.Result.State == Witness.Kind.ReturnNullIndirectly;
              return true;
            }
          }
        }
        catch (TimeoutExceptionFixpointComputation)
        {
          return false;
        }
        catch (AccessViolationException)
        {
          Console.WriteLine("Internal error in CLR: memory is corrupted? -- Continuing as we where simply searching for a witness");
        }
        finally
        {
          this.timeout.Stop();
        }

        return false;
      }

      protected override Witness NoCondition
      {
        get { return Witness.None; }
      }

      protected override bool IsNoCondition(Witness el)
      {
        return el.State == Witness.Kind.None;
      }

      protected override bool ShoudStopTheVisit(APC pc, Witness pre, int depth, out APC nextPC, out Witness nextCondition)
      {
        nextPC = pc;
        nextCondition = pre;
        if (pre.CurrentExpression == null || pre.State != Witness.Kind.None)
        {
          this.Found = pre.State != Witness.Kind.None;
          this.Result = pre;

          return true;
        }
        return base.WentTooFar(depth, pre);
      }


      public override Witness Rename(APC from, APC to, Witness pre, DataStructures.IFunctionalMap<Variable, Variable> renaming)
      {
        if (pre.CurrentExpression == null)
        {
          return pre;
        }

        Func<Variable, BoxedExpression> converter = ((Variable v) => BoxedExpression.Convert(this.Mdriver.Context.ExpressionContext.Refine(to, v), this.Mdriver.ExpressionDecoder));
        var newCondition = pre.CurrentExpression.Rename(renaming, converter);

        int value;
        var foundNull = newCondition != null && newCondition.IsConstantIntOrNull(out value) && value == 0;

        return new Witness(newCondition, foundNull ? Witness.Kind.ReturnNull : pre.State);
      }

      public override Witness Call<TypeList, ArgList>(APC pc, Method method, bool tail, bool virt, TypeList extraVarargs, Variable dest, ArgList args, Witness pre)
      {
        Variable v;
        if (pre.CurrentExpression != null && pre.CurrentExpression.TryGetFrameworkVariable(out v))
        {
          if (v.Equals(dest))
          {
            if (Mdriver.AnalysisDriver.MethodCache.GetWitnessForMayReturnNull(method))
            {
              return new Witness(pre.CurrentExpression, Witness.Kind.ReturnNullIndirectly);
            }
            else
            {
              return new Witness(null, Witness.Kind.None);
            }
          }
        }

        return base.Call<TypeList, ArgList>(pc, method, tail, virt, extraVarargs, dest, args, pre);
      }
    }

    public struct Witness
    {
      static public Witness None { get { return new Witness(Kind.None); } }

      public enum Kind { None, ReturnNull, ReturnNullIndirectly }

      public Kind State { get; private set; }
      public BoxedExpression CurrentExpression { get; private set; }

      public Witness(Kind state) 
        : this()
      {
        this.State = state;
        this.CurrentExpression = null;
      }

      public Witness(BoxedExpression currExp, Kind state = Kind.None)
        : this()
      {
        this.State = state;
        this.CurrentExpression = currExp;
      }

      public override string ToString()
      {
        return string.Format("{0} ({1})", this.CurrentExpression, this.State);
      }

    }

  }
}
