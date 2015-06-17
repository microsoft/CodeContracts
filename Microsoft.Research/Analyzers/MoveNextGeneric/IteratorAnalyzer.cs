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
using Microsoft.Research.AbstractDomains.Expressions;

namespace Microsoft.Research.CodeAnalysis
{
  public interface IIteratorClient<Variable>
  {
    IMethodResult<Variable> Analyze();
  }


  class IteratorMethodResult<Variable> : IMethodResult<Variable>
  {
    private IFactQuery<BoxedExpression, Variable> factQuery;

    public IteratorMethodResult(IFactQuery<BoxedExpression, Variable> factQuery)
    {
      this.factQuery = factQuery;
    }

    #region IMethodResult<Variable> Members

    public IFactQuery<BoxedExpression, Variable> FactQuery
    {
      get { return this.factQuery; }
    }

    public bool SuggestPostcondition(ContractInferenceManager inferenceManager)
    {
      return false;
    }

    public void SuggestPrecondition(ContractInferenceManager inferenceManager)
    {
    }

    public IEnumerable<BoxedExpression> GetPostconditionAsExpression()
    {
      yield break;
    }

    #endregion
  }

  public static class IteratorAnalyzer
  {
    public static IMethodAnalysisClientFactory<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, IIteratorClient<Variable>>
      Create<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>
    (
      IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> mdriver,
      int startState
    )
      where Type : IEquatable<Type>
      where Expression : IEquatable<Expression>
      where Variable : IEquatable<Variable>
    {
      var factory = new TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>.IteratorClientFactory(mdriver, startState);

      return factory;
    }

    public static class TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>
      where Type : IEquatable<Type>
      where Expression : IEquatable<Expression>
      where Variable : IEquatable<Variable>
    {

      public class IteratorClientFactory
        : IMethodAnalysisClientFactory<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, IIteratorClient<Variable>>
      {
        private IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> mdriver;
        private int startState;

        public IteratorClientFactory(IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> mdriver, int startState)
        {
          this.mdriver = mdriver;
          this.startState = startState;
        }

        #region IMethodAnalysisClientFactory<Local,Parameter,Method,Field,Property,Event,Type,Attribute,Assembly,Expression,Variable,IteratorClient> Members

        public IIteratorClient<Variable> Create<AState>(IAbstractAnalysis<Local, Parameter, Method, Field, Property, Type, Expression, Attribute, Assembly, AState, Variable> analysis)
        {
          return new IteratorClient<AState>(this.mdriver, this.startState, analysis);
        }

        #endregion
      }

      class IteratorClient<AState>
        : IIteratorClient<Variable>
      {
        private IAbstractAnalysis<Local, Parameter, Method, Field, Property, Type, Expression, Attribute, Assembly, AState, Variable> analysis;
        private IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> mdriver;
        StateMachineInformation stateInfo;
        private int startState;

        public IteratorClient(
          IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> iMethodDriver,
          int startState,
          IAbstractAnalysis<Local, Parameter, Method, Field, Property, Type, Expression, Attribute, Assembly, AState, Variable> analysis
        )
        {
          this.mdriver = iMethodDriver;
          this.startState = startState;
          this.analysis = analysis;

          var stateInfo = new StateMachineInformation();
          var queue = new Queue<int>();
          queue.Enqueue(startState);
          while (queue.Count > 0)
          {
            var currentState = queue.Dequeue();
            if (stateInfo.Contains(currentState)) continue;

            if (mdriver.Options.TraceMoveNext)
            {
              Console.WriteLine("[MoveNext state analysis] current state {0}", currentState);
            }

            var preAnalyzer = new IteratorStateAnalysis(mdriver, startState, currentState);
            var fixpoint = mdriver.HybridLayer.CreateForward(preAnalyzer, new DFAOptions { Trace = mdriver.Options.TraceMoveNext })(preAnalyzer.GetTopValue());
            stateInfo.Add(currentState, apc =>
            {
              INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> d;
              return fixpoint.PreState(apc, out d) && !d.IsBottom;
            },
            preAnalyzer.ExitStates);
            foreach (var succ in preAnalyzer.ExitStates.Keys)
            {
              if (mdriver.Options.TraceMoveNext)
              {
                Console.WriteLine("[MoveNext state analysis] next state {0}", succ);
              }
              if (!stateInfo.Contains(succ)) { queue.Enqueue(succ); }
            }
          }

          this.stateInfo = stateInfo;
        }

        public IMethodResult<Variable> Analyze()
        {
          // Fix point computation: fixpoint of the iterator analysis is a mapping: answers. 
          if (stateInfo == null)
          {
            return null;
          }
          WorkList<int> todo = new WorkList<int>();
          todo.Add(this.startState);
          var answers = (IFunctionalMap<int, IFixpointInfo<APC, AState>>)FunctionalMap<int, IFixpointInfo<APC, AState>>.Empty;
          Dictionary<int, AState> invariantCandidate = new Dictionary<int, AState>();

          int pass = 0;
          while (!todo.IsEmpty)
          {
            var state = todo.Pull();
            // The initial state is only analyzed once. 
            if (state < this.startState) continue;
            if (state == startState && pass > 0)
            {
              continue;
            }
            if (mdriver.Options.TraceMoveNext)
            {
              Console.WriteLine("[MoveNext final analysis] running state {0}", state);
            }

            // Initial value for one pass, either TOP, if we analyze it for the first time
            // or the invariantCandidate.
            if (!invariantCandidate.ContainsKey(state))
            {
              if (state == this.startState)
              {
                invariantCandidate[state] = analysis.GetTopValue();
              }
              else
              {
                throw new Exception("Can't get here");
              }
            }
            AState d = analysis.MutableVersion(invariantCandidate[state]);
            if (mdriver.Options.TraceMoveNext)
            {
              Console.WriteLine("[MoveNext final analysis starting abstract state]");
              analysis.Dump(new Pair<AState, System.IO.TextWriter>(d, Console.Out));
            }
            

            // Call the underlying analysis for one pass
            Predicate<APC> reachable = stateInfo.GetReachable(state);

            #region We create our DFA from scratch here
            // we use cutoffPCs to return bottom in the analysis when we stray from our current state
            var visitor = analysis.Visitor();
            var decoder = this.mdriver.HybridLayer.Decoder;

            var fixpoint = new ForwardAnalysisSolver<AState, Type, IFunctionalMap<Variable, FList<Variable>>>(
              decoder.Context.MethodContext.CFG,
              delegate(APC pc, AState astate) { return decoder.ForwardDecode<AState, AState, IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Variable, Variable, AState, AState>>(pc, visitor, astate); },
              analysis.EdgeConversion,
              analysis.Join,
              analysis.ImmutableVersion,
              analysis.MutableVersion,
              analysis.Dump,
              delegate(APC pc, AState astate) { return (!reachable(pc) || decoder.IsUnreachable(pc) || analysis.IsBottom(pc, astate)); },
              delegate(APC pc, AState astate) { return (analysis.IsTop(pc, astate)); },
              this.mdriver.ValueLayer.Printer,
              decoder.Display,
              decoder.EdgeData
            );
            fixpoint.Options = new DFAOptions() { Trace = this.mdriver.Options.TraceMoveNext };
            fixpoint.CachePolicy = analysis.CacheStates(fixpoint);
            fixpoint.Run(d);

            #endregion

            // Fill the result table with the most recent fixpoint information. 
            answers = answers.Add(state, fixpoint);

            // Getting the state from the return point of the most recent pass, map it
            foreach (int possibleNextState in stateInfo.NextStates(state))
            {
              foreach (var exitPC in stateInfo.ExitPoints(state, possibleNextState))
              {
                bool isBottom;
                AState newInvariantState = GetReturnState(exitPC, fixpoint, analysis, out isBottom);

                if (mdriver.Options.TraceMoveNext)
                {
                  Console.WriteLine("[MoveNext final analysis exit abstract state of state {0} at {1}]", state, exitPC);
                  analysis.Dump(new Pair<AState, System.IO.TextWriter>(newInvariantState, Console.Out));
                }

                if (invariantCandidate.ContainsKey(possibleNextState))
                {
                  AState oldd = invariantCandidate[possibleNextState];
                  // TODO: use a more sophisticated widening strategy. 
                  bool toWiden = true;
                  bool changed = false;
                  if (isBottom)
                  {
                    d = oldd;
                  }
                  else
                  {
                    var fakeEdge = new Pair<APC, APC>(this.mdriver.CFG.NormalExit, this.mdriver.CFG.Entry);
                    d = analysis.Join(fakeEdge, newInvariantState, oldd, out changed, toWiden);
                  }
                  if (changed)
                  {
                    todo.Add(possibleNextState);
                    invariantCandidate[possibleNextState] = d;
                    if (mdriver.Options.TraceMoveNext)
                    {
                      Console.WriteLine("[MoveNext final analysis new abstract state for state {0}]", possibleNextState);
                      analysis.Dump(new Pair<AState, System.IO.TextWriter>(d, Console.Out));
                    }
                  }
                }
                else
                {
                  if (!isBottom)
                  {
                    invariantCandidate[possibleNextState] = newInvariantState;
                    todo.Add(possibleNextState);
                    if (mdriver.Options.TraceMoveNext)
                    {
                      Console.WriteLine("[MoveNext final analysis new abstract state for state {0}]", possibleNextState);
                      analysis.Dump(new Pair<AState, System.IO.TextWriter>(newInvariantState, Console.Out));
                    }
                  }
                }

              }
            }

            pass++;
          }
          var result = new DisjunctionFactQuery<Variable>(this.mdriver.ValueLayer.Decoder.IsUnreachable);
          answers.Visit((statekey, fixpoint) => { result.Add(analysis.FactQuery(fixpoint)); return VisitStatus.ContinueVisit; });
          return new IteratorMethodResult<Variable>(result);
        }


        AState GetReturnState(APC returnPoint, IFixpointInfo<APC, AState> fixpoint, IAbstractAnalysis<Local, Parameter, Method, Field, Property, Type, Expression, Attribute, Assembly, AState, Variable> analysis, out bool isBottom)
        {
          var fakeEdge = new Pair<APC, APC>(this.mdriver.CFG.NormalExit, this.mdriver.CFG.Entry);
          AState tmp;
          if (fixpoint.PostState(returnPoint, out tmp) && !analysis.IsBottom(returnPoint, tmp))
          {
             isBottom = false;
             var edge = new Pair<APC, APC>(returnPoint, this.mdriver.CFG.Entry.Post());
             IFunctionalMap<Variable, FList<Variable>> mapping = GetFieldMapping(returnPoint, this.mdriver.CFG.Entry.Post());
             AState newState = analysis.EdgeConversion(fakeEdge.One, fakeEdge.Two, true, mapping, tmp);
             return newState;
          }
          isBottom = true;
          return default(AState);
        }

        /// <summary>
        /// Given a method driver, compute a parallel renaming relation between the (symbolic) values at the return point
        /// of a method and those at the entry of the same method. This renaming relation will be used to map an abstract
        /// state at the end of one pass of analysis to the starting point of the next pass.
        /// 
        /// Implementation is to use SymbolicValuesAt method to find symbolic values for "important" access pathes and
        /// match the symbolic values that represent the same access path. 
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        IFunctionalMap<Variable, FList<Variable>> GetFieldMapping(APC pc1, APC pc2)
        {
          FunctionalMap<Variable, FList<Variable>> fieldMapping = null;
          fieldMapping = FunctionalMap<Variable, FList<Variable>>.Empty;
          foreach (Pair<string, Variable> pair in SymbolicValuesAt(pc1))
          {
            if (!fieldMapping.Contains(pair.Two))
            {
              fieldMapping = (FunctionalMap<Variable, FList<Variable>>)fieldMapping.Add(pair.Two, FList<Variable>.Empty);
            }
            foreach (Pair<string, Variable> pair2 in SymbolicValuesAt(pc2))
            {
              if (pair.One == pair2.One)
              {
                FList<Variable> list = fieldMapping[pair.Two];
                list = list.Cons(pair2.Two);
                fieldMapping = (FunctionalMap<Variable, FList<Variable>>)fieldMapping.Add(pair.Two, list);
              }
            }
          }
          return fieldMapping;
        }

        /// <summary>
        /// Computes a table of (access path, symbolic value) tuples at the given program point. 
        /// 
        /// The method hard codes our choice of which access pathes are considered "important". 
        /// Currently, fields of the iterator class and the length of the array, if the field is
        /// an array. TODO: consult Francesco to see what we can do to improve the accuracy. 
        /// </summary>
        /// <param name="pc"></param>
        /// <param name="driver"></param>
        /// <returns></returns>
        public IEnumerable<Pair<string, Variable>> SymbolicValuesAt(APC pc)
        {
          Variable thisV;
          var context = this.mdriver.Context;
          var mdDecoder = this.mdriver.MetaDataDecoder;
          if (!context.ValueContext.TryParameterValue(pc, mdDecoder.This(context.MethodContext.CurrentMethod), out thisV)) { yield break; }
          Type currentType = mdDecoder.DeclaringType(context.MethodContext.CurrentMethod);
          foreach (Field f in mdDecoder.Fields(currentType))
          {
            Variable fieldAddr;
            if (context.ValueContext.TryFieldAddress(pc, thisV, f, out fieldAddr))
            {
              Variable v;
              if (context.ValueContext.TryLoadIndirect(pc, fieldAddr, out v))
              {
                Type fieldType = mdDecoder.FieldType(f);
                if (fieldType != null && mdDecoder.IsArray(fieldType))
                {
                  Variable arrayLength;
                  if (context.ValueContext.TryGetArrayLength(pc, v, out arrayLength))
                  {
                    yield return new Pair<string, Variable>(mdDecoder.Name(f) + ".Length", arrayLength);
                  }
                }
                yield return new Pair<string, Variable>(mdDecoder.Name(f), v);
              }
            }
          }
        }

      }

      /// <summary>
      /// Encapsulates information regarding the iterator state machine: what the states are, what their entry point(s)
      /// are and the access path of the return value, which is typically a local variable. 
      /// 
      /// An entry point for state i is the program point of the case statement of the global switch in a movenext method:
      /// switch (this.__state)... L: case 1: ...., where L is the entry point for state 1. 
      /// </summary>
      /// <typeparam name="Local"></typeparam>
      public class StateMachineInformation
      {
        /// <summary>
        /// Maps entry state to reachable states and possible exit states
        /// </summary>
        readonly Dictionary<int, Tuple<Predicate<APC>, Dictionary<int, Set<APC>>>> sliceInfo = new Dictionary<int, Tuple<Predicate<APC>, Dictionary<int, Set<APC>>>>();

        public void Add(int state, Predicate<APC> isReachable, Dictionary<int, Set<APC>> successors)
        {
          this.sliceInfo.Add(state, new Tuple<Predicate<APC>, Dictionary<int, Set<APC>>>(isReachable, successors));
        }

        internal bool Contains(int state)
        {
          return this.sliceInfo.ContainsKey(state);
        }

        internal Predicate<APC> GetReachable(int state)
        {
          Tuple<Predicate<APC>, Dictionary<int, Set<APC>>> result;
          if (!this.sliceInfo.TryGetValue(state, out result))
          {
            return apc => false;
          }
          return result.Item1;
        }
      
        internal IEnumerable<int> NextStates(int state)
        {
 	        return this.sliceInfo[state].Item2.Keys;
        }

        internal Set<APC> ExitPoints(int from, int to) {
          return this.sliceInfo[from].Item2[to];
        }
      }


      class IteratorStateAnalysis : AnalysisWrapper.TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>.NumericalAnalysis<Analyzers.Bounds.BoundsOptions> 
      {
        private IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> mdriver;
        private int startState;
        private int currentState;
        private Dictionary<int, Set<APC>> stateReturnPoints = new Dictionary<int, Set<APC>>();

        public IteratorStateAnalysis(IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> mdriver, int startState, int currentState)
          : base(mdriver.MetaDataDecoder.Name(mdriver.CurrentMethod),mdriver, 
                 new List<Analyzers.Bounds.BoundsOptions>() { new Analyzers.Bounds.BoundsOptions(mdriver.Options){ type= Analyzers.DomainKind.SubPolyhedra} },
                 apc => true)
        {
          // TODO: Complete member initialization
          this.mdriver = mdriver;
          this.startState = startState;
          this.currentState = currentState;
        }

        /// <summary>
        /// Context we got from the upcall to Visitor. This lets us find out things about Variables and Expressions
        /// </summary>
        internal IExpressionContext<Local, Parameter, Method, Field, Type, Expression, Variable> context { get { return this.mdriver.ExpressionLayer.Decoder.Context; } }

        private Field stateField = default(Field);
        private Field StateField
        {
          get
          {
            if (stateField == null)
            {
              Type closureType = this.mdriver.MetaDataDecoder.DeclaringType(this.mdriver.CurrentMethod);
              foreach (Field f in this.mdriver.MetaDataDecoder.Fields(closureType))
              {
                string fname = this.mdriver.MetaDataDecoder.Name(f);
                if (fname.Contains("<>") && fname.EndsWith("state"))
                {
                  stateField = f;
                  break;
                }
              }
            }
            return stateField;
          }
        }
        bool IsEnumeratorStatePredicate(APC pc, Variable condition, out int stateValue, out bool OpIsEq)
        {
          stateValue = -100;
          OpIsEq = false;
          IFullExpressionDecoder<Type, Variable, Expression> decoder = this.mdriver.ExpressionDecoder;
          Expression exp = this.context.ExpressionContext.Refine(pc, condition);
          BinaryOperator op;
          Expression left, right;
          if (decoder.IsBinaryOperator(exp, out op, out left, out right))
          {
            if (op == BinaryOperator.Ceq || op == BinaryOperator.Cne_Un || op == BinaryOperator.Cobjeq)
            {
              if (op == BinaryOperator.Cne_Un) OpIsEq = false;
              else OpIsEq = true;
              object o;
              if (decoder.IsVariable(left, out o))
              {
                string path = this.context.ValueContext.AccessPath(pc, (Variable)o);
                if (path != null)
                {
                  if (path.Contains(".<>") && path.EndsWith("state"))
                  {
                    object value; Type type;
                    if (decoder.IsConstant(right, out value, out type))
                    {
                      if (type.Equals(this.mdriver.MetaDataDecoder.System_UInt32) || type.Equals(this.mdriver.MetaDataDecoder.System_Int32))
                      {
                        stateValue = (int)value;
                        return true;
                      }
                    }
                  }
                }
              }
            }
          }
          return false;
        }

        private Variable ThisDotState(APC pc)
        {
          Parameter pThis = this.mdriver.MetaDataDecoder.This(this.mdriver.CurrentMethod);
          Variable thisValue;
          if (this.context.ValueContext.TryParameterValue(pc, pThis, out thisValue))
          {
            Variable addrThisDotState;
            if (this.context.ValueContext.TryFieldAddress(pc, thisValue, this.StateField, out addrThisDotState))
            {
              Variable result;
              if (this.context.ValueContext.TryLoadIndirect(pc, addrThisDotState, out result))
              {
                return result;
              }
            }
          }
          return default(Variable);
        }

        public override INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> Entry(APC pc, Method method, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          var postPC = this.mdriver.CFG.Post(pc);
          var thisDotState = ThisDotState(postPC);
          BoxedExpression boxedThisDotState = BoxedExpression.For(this.context.ExpressionContext.Refine(pc, thisDotState), base.Decoder.Outdecoder);
          var initialStateCond = BoxedExpression.Binary(BinaryOperator.Ceq, boxedThisDotState, BoxedExpression.Const(this.currentState, this.mdriver.MetaDataDecoder.System_Int32, this.mdriver.MetaDataDecoder));
          data.TestTrue(initialStateCond);

          return base.Entry(pc, method, data);
        }

        public override INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> Stfld(APC pc, Field field, bool @volatile, Variable obj, Variable value, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          Variable possibleThisV;
          string fieldName = this.mdriver.MetaDataDecoder.Name(field);
          if (this.context.ValueContext.TryParameterValue(pc, this.mdriver.MetaDataDecoder.This(this.context.MethodContext.CurrentMethod), out possibleThisV))
          {
            if (possibleThisV.Equals(obj))
            {
              if (fieldName.EndsWith("_state"))
              {
                object v; Type type;
                if (this.context.ValueContext.IsConstant(pc, value, out type, out v))
                {
                  int stateValue = (int)v;
                  if (stateValue > this.startState)
                  {
                    var postPC = this.MethodDriver.CFG.Post(pc);
                    if (stateReturnPoints.ContainsKey(stateValue))
                    {
                      stateReturnPoints[stateValue].Add(postPC);
                    }
                    else
                      stateReturnPoints[stateValue] = new Set<APC>(postPC);
                  }
                }
              }
            }
          }
          return base.Stfld(pc, field, @volatile, obj, value, data);
        }

        public Dictionary<int, Set<APC>> ExitStates { get { return this.stateReturnPoints; } }   
      }

      /// <summary>
      /// The main analysis, which most importantly is a visitor. When the visitor visits an assume statement, we try 
      /// to determine whether this corresponds to a case statement in the global switch this.__state. 
      /// 
      /// </summary>
      public class OldIteratorStateAnalysis : MSILVisitor<APC, Local, Parameter, Method, Field, Type, Variable, Variable, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression>, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression>>,
       IAnalysis<APC, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression>, IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Variable, Variable, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression>, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression>>,
                      IFunctionalMap<Variable, FList<Variable>>>
      {
        public const int DefaultCase = -999;

        #region Private fields
        /// <summary>
        /// Context we got from the upcall to Visitor. This lets us find out things about Variables and Expressions
        /// </summary>
        internal IExpressionContext<Local, Parameter, Method, Field, Type, Expression, Variable> context { get { return this.mdriver.ExpressionLayer.Decoder.Context; } }
        /// <summary>
        /// An analysis likely needs access to a meta data decoder to make sense of types etc.
        /// </summary>
        internal IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> mdriver;
        private Dictionary<int, Set<APC>> stateReturnPoints = new Dictionary<int, Set<APC>>();
        private BoxedExpressionDecoder<Type, Variable, Expression> decoderForExpressions;
        private Microsoft.Research.AbstractDomains.Expressions.IExpressionEncoder<BoxedVariable<Variable>, BoxedExpression> encoderForExpressions;
        #endregion Private fields

        public OldIteratorStateAnalysis(IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> mdriver,
          int startState,
          int currentState)
        {
          this.mdriver = mdriver;
          this.startState = startState;
          this.currentState = currentState;
          this.decoderForExpressions = BoxedExpressionDecoder<Variable>.Decoder(new
          AnalysisWrapper.TypeBindings<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable>.ValueExpDecoder(this.mdriver.Context, this.mdriver.MetaDataDecoder));
          this.encoderForExpressions = BoxedExpressionEncoder<Variable>.Encoder(this.mdriver.MetaDataDecoder, this.mdriver.Context);
        }


        public INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> InitialValue()
        {
          var result = new AbstractDomains.Numerical.IntervalEnvironment<BoxedVariable<Variable>, BoxedExpression>(
            new ExpressionManager<BoxedVariable<Variable>, BoxedExpression>(DFARoot.TimeOut, decoderForExpressions, encoderForExpressions));
          return result;
        }

        #region IValueAnalysis<APC,INumericalAbstractDomain<Expression>,IVisitMSIL<APC,Local,Parameter,Method,Field,Type,Expression,Variable,INumericalAbstractDomain<Expression>,INumericalAbstractDomain<Expression>>,Variable,Expression,IExpressionContext<APC,Method,Type,Expression,Variable>> Members

        /// <summary>
        /// Here, we return the transfer function. Since we implement this via MSILVisitor, we just return this.
        /// </summary>
        /// <param name="context">The expression context is an interface we can use to find out more about expressions, such as their type etc.</param>
        /// <returns>The transfer function.</returns>
        public IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Variable, Variable, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression>, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression>> Visitor()
        {
          return this;
        }

        /// <summary>
        /// Must implement the join/widen operation
        /// </summary>
        /// <param name="edge"></param>
        /// <param name="newState"></param>
        /// <param name="prevState"></param>
        /// <param name="weaker">should return false if result is less than or equal prevState.</param>
        /// <param name="widen">true if this is a widen operation. For our INumericalAbstractDomain<Expression>, this makes no difference</param>
        public INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> Join(Pair<APC, APC> edge, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> newState, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> prevState, out bool weaker, bool widen)
        {
          INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> result = (INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression>)prevState.Join(newState);
          weaker = !prevState.LessEqual(newState);
          return result;
        }

        public bool IsBottom(APC pc, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> state)
        {
          return state.IsBottom;
        }

        public bool IsTop(APC pc, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> state)
        {
          return state.IsTop;
        }

        public INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> ImmutableVersion(INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> state)
        {
          return state;
        }

        public INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> MutableVersion(INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> state)
        {
          return (INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression>)state.Clone();
        }

        public void Dump(Pair<INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression>, System.IO.TextWriter> stateAndWriter)
        {
          var tw = stateAndWriter.Two;
          tw.Write("MoveNext state: {0} ", stateAndWriter.One);
        }

        /// <summary>
        /// This method is called by the underlying driver of the fixpoint computation. It provides delegates for future lookup
        /// of the abstract state at given pcs.
        /// </summary>
        /// <returns>Return true only if you want the fixpoint computation to eagerly cache each pc state.</returns>
        public Predicate<APC> CacheStates(IFixpointInfo<APC, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression>> fixpointInfo)
        {
          return apc => true; // cache everything
        }

        public INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> EdgeConversion(APC from, APC to, bool isJoin, IFunctionalMap<Variable, FList<Variable>> sourceTargetMap, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> state)
        {
          if (sourceTargetMap == null) return state;

          var edge = new Pair<APC, APC>(from, to);
          var refinedMap = RefineMapToExpressions(edge.One, sourceTargetMap);
          state.AssignInParallel(refinedMap,
                        delegate(BoxedVariable<Variable> v) {
                          Variable tmp;
                          if (v.TryUnpackVariable(out tmp))
                          {
                            return ToBoxedExpression(edge.One, tmp);
                          }
                          else
                          {
                            return default(BoxedExpression);
                          }
                        });
          return state;
        }

        private Dictionary<BoxedVariable<Variable>, FList<BoxedVariable<Variable>>> RefineMapToExpressions(APC pc, IFunctionalMap<Variable, FList<Variable>> sourceTargetMap)
        {
          var result = new Dictionary<BoxedVariable<Variable>, FList<BoxedVariable<Variable>>>(sourceTargetMap.Count);

          foreach (var v in sourceTargetMap.Keys)
          {
            var vRefined = new BoxedVariable<Variable>(v);
            result[vRefined] = sourceTargetMap[v].Map(target => new BoxedVariable<Variable>(target));
          }
          return result;
        }
        protected BoxedExpression ToBoxedExpression(APC pc, Variable var)
        {
          return BoxedExpression.For(this.mdriver.Context.ExpressionContext.Refine(pc, var), this.decoderForExpressions.Outdecoder);
        }

        protected override INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> Default(APC pc, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          return data;
        }

        bool IsEnumeratorStatePredicate(APC pc, Variable condition, out int stateValue, out bool OpIsEq)
        {
          stateValue = -100;
          OpIsEq = false;
          IFullExpressionDecoder<Type, Variable, Expression> decoder = this.mdriver.ExpressionDecoder;
          Expression exp = this.context.ExpressionContext.Refine(pc, condition);
          BinaryOperator op;
          Expression left, right;
          if (decoder.IsBinaryOperator(exp, out op, out left, out right))
          {
            if (op == BinaryOperator.Ceq || op == BinaryOperator.Cne_Un || op == BinaryOperator.Cobjeq)
            {
              if (op == BinaryOperator.Cne_Un) OpIsEq = false;
              else OpIsEq = true;
              object o;
              if (decoder.IsVariable(left, out o))
              {
                string path = this.context.ValueContext.AccessPath(pc, (Variable)o);
                if (path != null)
                {
                  if (path.Contains(".<>") && path.EndsWith("state"))
                  {
                    object value; Type type;
                    if (decoder.IsConstant(right, out value, out type))
                    {
                      if (type.Equals(this.mdriver.MetaDataDecoder.System_UInt32) || type.Equals(this.mdriver.MetaDataDecoder.System_Int32))
                      {
                        stateValue = (int)value;
                        return true;
                      }
                    }
                  }
                }
              }
            }
          }
          return false;
        }

        public override INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> Entry(APC pc, Method method, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          var postPC = this.mdriver.CFG.Post(pc);
          var thisDotState = ThisDotState(postPC);
          BoxedExpression boxedThisDotState = BoxedExpression.For(this.context.ExpressionContext.Refine(pc, thisDotState), this.decoderForExpressions.Outdecoder);
          var initialStateCond = BoxedExpression.Binary(BinaryOperator.Ceq, boxedThisDotState, BoxedExpression.Const(this.currentState, this.mdriver.MetaDataDecoder.System_Int32, this.mdriver.MetaDataDecoder));
          data.TestTrue(initialStateCond);

          return base.Entry(pc, method, data);
        }

        public override INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> Assume(APC pc, string tag, Variable condition, object provenance, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          BoxedExpression boxedCondition = BoxedExpression.For(this.context.ExpressionContext.Refine(pc, condition), this.decoderForExpressions.Outdecoder);
          if (tag == "false")
          {
            data.TestFalse(boxedCondition);
          }
          else // if (tag == "true")
          {
            data.TestTrue(boxedCondition);
          }
          return base.Assume(pc, tag, condition, provenance, data);
        }

        private Variable ThisDotState(APC pc)
        {
          Parameter pThis = this.mdriver.MetaDataDecoder.This(this.mdriver.CurrentMethod);
          Variable thisValue;
          if (this.context.ValueContext.TryParameterValue(pc, pThis, out thisValue))
          {
            Variable addrThisDotState;
            if (this.context.ValueContext.TryFieldAddress(pc, thisValue, this.StateField, out addrThisDotState))
            {
              Variable result;
              if (this.context.ValueContext.TryLoadIndirect(pc, addrThisDotState, out result))
              {
                return result;
              }
            }
          }
          return default(Variable);
        }

        private Field stateField = default(Field);
        private int currentState;
        private int startState;
        private Field StateField
        {
          get
          {
            if (stateField == null)
            {
              Type closureType = this.mdriver.MetaDataDecoder.DeclaringType(this.mdriver.CurrentMethod);
              foreach (Field f in this.mdriver.MetaDataDecoder.Fields(closureType))
              {
                string fname = this.mdriver.MetaDataDecoder.Name(f);
                if (fname.Contains("<>") && fname.EndsWith("state"))
                {
                  stateField = f;
                  break;
                }
              }
            }
            return stateField;
          }
        }

        public override INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> Stfld(APC pc, Field field, bool @volatile, Variable obj, Variable value, INumericalAbstractDomain<BoxedVariable<Variable>, BoxedExpression> data)
        {
          Variable possibleThisV;
          string fieldName = this.mdriver.MetaDataDecoder.Name(field);
          if (this.context.ValueContext.TryParameterValue(pc, this.mdriver.MetaDataDecoder.This(this.context.MethodContext.CurrentMethod), out possibleThisV))
          {
            if (possibleThisV.Equals(obj))
            {
              if (fieldName.EndsWith("_state"))
              {
                object v; Type type;
                if (this.context.ValueContext.IsConstant(pc, value, out type, out v))
                {
                  int stateValue = (int)v;
                  if (stateValue > this.startState)
                  {
                    if (stateReturnPoints.ContainsKey(stateValue))
                    {
                      stateReturnPoints[stateValue].Add(pc);
                    }
                    else
                      stateReturnPoints[stateValue] = new Set<APC>(pc);
                    // stop exploration here.
                    var result = new AbstractDomains.Numerical.IntervalEnvironment<BoxedVariable<Variable>, BoxedExpression>(
                      new ExpressionManager<BoxedVariable<Variable>, BoxedExpression>(DFARoot.TimeOut, decoderForExpressions, encoderForExpressions));
                    return result.Bottom;
                  }
                }
              }
            }
          }
          return base.Stfld(pc, field, @volatile, obj, value, data);
        }

        #endregion

        public Dictionary<int, Set<APC>> ExitStates { get { return this.stateReturnPoints; } }
      }

    }
  }
}