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
using System.Diagnostics.Contracts;
using Microsoft.Research.DataStructures;

namespace Microsoft.Research.CodeAnalysis
{
  public static class MarkerFetcher
  {

    public static Data<Variable> FindMethodExtractMarkers<Local, Parameter, Method, Field, Property, Event, Typ, Attribute, Assembly, Expression, Variable>(IMethodDriver<Local, Parameter, Method, Field, Property, Event, Typ, Attribute, Assembly, Expression, Variable, ILogOptions> mdriver)
      where Typ : IEquatable<Typ>
      where Expression : IEquatable<Expression>
      where Variable : IEquatable<Variable>
    {
      return TypeBindings<Local, Parameter, Method, Field, Property, Event, Typ, Attribute, Assembly, Expression, Variable>.RunTheAnalysis(mdriver);
    }

    public struct Data<Variable>
    {
      #region Object invariant

      [ContractInvariantMethod]
      private void ObjectInvariant()
      {
        Contract.Invariant(this.Version >= 0);
      }

      #endregion

      #region State

      public int Version { get; private set; }
      public Info Precondition { get; private set; }
      public Info Postcondition { get; private set; }
      public Info Invariant { get; private set; }
      #endregion

      #region Constructors

      private Data(int version, Info Precondition, Info Postcondition, Info Invariant)
        : this()
      {
        Contract.Requires(version >= 0);

        this.Version = version;
        this.Precondition = Precondition;
        this.Postcondition = Postcondition;
        this.Invariant = Invariant;
      }

      #endregion

      public Data<Variable> UpdatePrecondition(APC pc, FList<Variable> Variables)
      {
        return new Data<Variable>(this.Version + 1, new Info() { PC = pc, Variables = Variables }, this.Postcondition, this.Invariant);
      }

      public Data<Variable> UpdatePostcondition(APC pc, Variable retVar, FList<Variable> Variables)
      {
        return new Data<Variable>(this.Version + 1, this.Precondition, new Info() { PC = pc, ReturnVariable = retVar, Variables = Variables }, this.Invariant);
      }

      public Data<Variable> UpdatePostcondition(APC pc, FList<Variable> Variables)
      {
        return new Data<Variable>(this.Version + 1, this.Precondition, new Info() { PC = pc, Variables = Variables }, this.Invariant);
      }

      public Data<Variable> UpdateInvariant(APC pc, FList<Variable> Variables)
      {
        return new Data<Variable>(this.Version + 1, this.Precondition, this.Postcondition, new Info() { PC = pc, Variables = Variables });
      }

      public struct Info
      {

        public bool HasReturnVariable { get; private set; }

        private Variable returnVariable;
        public Variable ReturnVariable 
        {
          get
          {
            return this.returnVariable;
          }
          internal set
          {
            HasReturnVariable = true;
            this.returnVariable = value;
          }
        }


        public APC PC { get; internal set; }
        public FList<Variable> Variables { get; internal set; }
      }

    }

    public static class TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>
      where Variable : IEquatable<Variable>
      where Expression : IEquatable<Expression>
      where Type : IEquatable<Type>
    {

      public static Data<Variable> RunTheAnalysis(IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> mdriver)
      {
        var analysis = new MarkersFetcherAnalysis(mdriver);
        var closure = mdriver.HybridLayer.CreateForward(analysis, new DFAOptions { Trace = mdriver.Options.TraceDFA, Timeout = mdriver.Options.Timeout, SymbolicTimeout = mdriver.Options.SymbolicTimeout, EnforceFairJoin = mdriver.Options.EnforceFairJoin, IterationsBeforeWidening = mdriver.Options.IterationsBeforeWidening, TraceTimePerInstruction = mdriver.Options.TraceTimings, TraceMemoryPerInstruction = mdriver.Options.TraceMemoryConsumption }, null);

        closure(analysis.GetTopValue());

        return analysis.Result;
      }

      [ContractVerification(true)]
      class MarkersFetcherAnalysis
        : MSILVisitor<APC, Local, Parameter, Method, Field, Type, Variable, Variable, Data<Variable>, Data<Variable>>
        , IAbstractAnalysis<Local, Parameter, Method, Field, Property, Type, Expression, Attribute, Assembly, Data<Variable>, Variable>
      {
        #region Constants

        const string PRECONDITIONMARKER = "__PreconditionMarker";
        const string POSTCONDITIONMARKER = "__PostconditionMarker";
        const string INVARIANTMARKER = "___ClousotInvariantAt";

        #endregion

        #region Object invariant

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
          Contract.Invariant(this.mDriver != null);
        }

        #endregion

        #region  State

        public Data<Variable> Result { get; private set; }

        IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> mDriver;


        #endregion

        #region Constructor

        public MarkersFetcherAnalysis(
          IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> mdriver)
        {
          Contract.Requires(mdriver != null);

          this.mDriver = mdriver;
        }

        #endregion

        #region overridden

        public override Data<Variable> Call<TypeList, ArgList>(APC pc, Method method, bool tail, bool virt, TypeList extraVarargs, Variable dest, ArgList args, Data<Variable> data)
        // where TypeList : IIndexable<Type> 
        // where ArgList : IIndexable<Variable>
        {

          var mdd = this.mDriver.MetaDataDecoder;
          Contract.Assert(mdd != null); // was an assume

          var methodName = mdd.Name(method);
          var skipFirstArgument = !mdd.IsStatic(method); // We want to skip the first parameter when it is this

          /*
          if (methodName == null)
          {
            return data;
          }
          */

          Contract.Assert(methodName != null);

          if (methodName.Contains(PRECONDITIONMARKER))
          {
            // For preconditions, to get Ps, the only variables we are intested in ref values
            // As we have problems with the symbolic values, we assume the contract that the Roslyn integration passes us only the variables that will be used byRef in the call
            var variablesForPs = FList<Variable>.Empty;

            var i = mdd.IsStatic(method) ? 0 : 1;
            foreach (var p in mdd.Parameters(method).Enumerate())
            {
              Contract.Assume(i < args.Count, "Assuming the global invariant");
              variablesForPs = variablesForPs.Cons(args[i]);
              i++;
            }

            return data.UpdatePrecondition(pc, variablesForPs);
          }
          
          if (methodName.Contains(POSTCONDITIONMARKER))
          {
            // We use the protocol that if the last parameter is set to true, then the but last actual parameter is the sv of the value
            // returned from the extract method

            if (args.Count > 0) // sanity check to make the code more robust
            {
              Type t;
              object value;
              if (this.mDriver.Context.ValueContext.IsConstant(pc, args[args.Count - 1], out t, out value)
                && mdd.System_Int32.Equals(t) && value.Equals(1) && args.Count > 1)
              {
                var retVar = args[args.Count - 2];

                return data.UpdatePostcondition(pc, retVar, AddVariablesForsArrayLengths(pc, ToFList(args, args.Count - 2, skipFirstArgument)));
              }
              return data.UpdatePostcondition(pc, AddVariablesForsArrayLengths(pc, ToFList(args, skipThis: skipFirstArgument)));
            }
          }
          if (methodName.Contains(INVARIANTMARKER))
          {
            return data.UpdateInvariant(pc, AddVariablesForsArrayLengths(pc, ToFList(args, skipThis: skipFirstArgument)));
          }

          return data;
        }

        public override Data<Variable> Return(APC pc, Variable source, Data<Variable> data)
        {
          this.Result = data;

          return data;
        }

        #endregion

        #region Default: Do nothing

        protected override Data<Variable> Default(APC pc, Data<Variable> data)
        {
          return data;
        }

        #endregion

        #region Utils

        private FList<Variable> AddVariablesForsArrayLengths(APC pc, FList<Variable> variables)
        {
          Contract.Ensures(variables == null || Contract.Result<FList<Variable>>() != null);

          if(variables == FList<Variable>.Empty)
          {
            return variables;
          }

          var postPC = pc.Post();
          var arrayLenghts = FList<Variable>.Empty;
          foreach (var v in variables.GetEnumerable())
          {
            Variable vLength;
            if (this.mDriver.Context.ValueContext.TryGetArrayLength(postPC, v, out vLength))
            {
              arrayLenghts = arrayLenghts.Cons(vLength);
            }
          }

          if (!arrayLenghts.IsEmpty())
          {
            variables = variables.Append(arrayLenghts);
            Contract.Assert(variables != null);
          }

          return variables;
        }

        [Pure]
        public static FList<Variable> ToFList<IIndexable>(IIndexable args, int length = -1, bool skipThis=true)
          where IIndexable : IIndexable<Variable>
        {
          var head = FList<Variable>.Empty;

          if (args == null)
            return head;

          var count = 0;

          if (length == -1)
            length = args.Count;

          foreach (var x in args.Enumerate())
          {
            if (count++ == length)
              break;

            // skip this?
            if (skipThis && count == 1)
              continue;

            head.Append(head);
            head = head.Cons(x);
          }

          return head;
        }

        #endregion

        public Data<Variable> GetTopValue()
        {
          return new Data<Variable>();
        }

        public Data<Variable> GetBottomValue()
        {
          return new Data<Variable>();
        }

        public IFactQuery<BoxedExpression, Variable> FactQuery(IFixpointInfo<APC, Data<Variable>> fixpoint)
        {
          return null;
        }

        public Data<Variable> EdgeConversion(APC from, APC next, bool joinPoint, IFunctionalMap<Variable, FList<Variable>> edgeData, Data<Variable> newState)
        {
          // We do not want to convert the variables!
          return newState;
        }

        public Data<Variable> Join(Pair<APC, APC> edge, Data<Variable> newState, Data<Variable> prevState, out bool weaker, bool widen)
        {
          if(newState.Version >= prevState.Version)
          {
            if(newState.Precondition.PC.Equals(prevState.Precondition.PC) &&
              newState.Postcondition.PC.Equals(prevState.Postcondition.PC) &&
              newState.Invariant.PC.Equals(prevState.Invariant.PC))
            {
              weaker = false;
              return prevState;
            }

            weaker = true;
            return newState;
          }
          else
          {
            weaker = false;
            return prevState;
          }
        }

        public Data<Variable> MutableVersion(Data<Variable> state)
        {
          return state;
        }

        public Data<Variable> ImmutableVersion(Data<Variable> state)
        {
          return state;
        }

        public void Dump(Pair<Data<Variable>, System.IO.TextWriter> pair)
        {
          pair.Two.WriteLine(pair.One.Version);
        }

        public bool IsBottom(APC pc, Data<Variable> state)
        {
          return false;
        }

        public bool IsTop(APC pc, Data<Variable> state)
        {
          return false;
        }

        public IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Variable, Variable, Data<Variable>, Data<Variable>> Visitor()
        {
          return this;
        }

        public Predicate<APC> CacheStates(IFixpointInfo<APC, Data<Variable>> fixpointInfo)
        {
          return apc => false;
        }
      }
    }
  }
}