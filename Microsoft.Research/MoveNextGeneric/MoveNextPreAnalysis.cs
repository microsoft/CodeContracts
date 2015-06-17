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
using System.Diagnostics;
using Microsoft.Research.CodeAnalysis;
using Microsoft.Research.AbstractDomains;

namespace Microsoft.Research.MoveNextGeneric {
  /// <summary>
  /// The analyzer that collects the state machine information of an iterator. It does so by using an forward analysis
  /// over the move next method of concern. The main interface of the analyzer is AnalyzeMoveNext, which returns information
  /// of the statemachine.
  /// </summary>
  public class MoveNextStateAnalyzer {
    public MoveNextStateAnalyzer() {
    }

    public const int DefaultCase = -999;

    static class TypeBindings<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly, Expression, Variable, ILogOptions>
      where Variable : IEquatable<Variable>
      where Type : IEquatable<Type>
      where Expression: IEquatable<Expression>
      where ILogOptions: IFrameworkLogOptions
    {

      /// <summary>
      /// The main analysis, which most importantly is a visitor. When the visitor visits an assume statement, we try 
      /// to determine whether this corresponds to a case statement in the global switch this.__state. 
      /// 
      /// </summary>
      public class Analysis : MSILVisitor<APC, Local, Parameter, Method, Field, Type, Variable, Variable, INumericalAbstractDomain<BoxedExpression>, INumericalAbstractDomain<BoxedExpression>>,
       IValueAnalysis<APC, INumericalAbstractDomain<BoxedExpression>, IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Variable, Variable, INumericalAbstractDomain<BoxedExpression>, INumericalAbstractDomain<BoxedExpression>>,
                      Variable,
                      IExpressionContext<APC, Local, Parameter, Method, Field, Type, Expression, Variable>> {

        #region Private fields
        /// <summary>
        /// Context we got from the upcall to Visitor. This lets us find out things about Variables and Expressions
        /// </summary>
        internal IExpressionContext<APC, Local, Parameter, Method, Field, Type, Expression, Variable> context;
        /// <summary>
        /// An analysis likely needs access to a meta data decoder to make sense of types etc.
        /// </summary>
        internal IMethodDriver<APC, Local, Parameter, Method, Field, Property, Type, Attribute, Assembly, Expression, Variable, ILogOptions> mdriver;
        internal MoveNextStateAnalyzer parent;
        private Dictionary<int, Set<APC>> stateInfo = new Dictionary<int,Set<APC>>();
        private Set<string> returnValues = Set<string>.Empty;
        private BoxedExpressionDecoder<Type, Expression> decoderForExpressions;
        private Microsoft.Research.AbstractDomains.Expressions.IExpressionEncoder<BoxedExpression> encoderForExpressions;
        #endregion Private fields

        public Analysis(IMethodDriver<APC, Local, Parameter, Method, Field, Property, Type, Attribute, Assembly, Expression, Variable, ILogOptions> mdriver, MoveNextStateAnalyzer parent) {
          this.parent = parent;
          this.mdriver = mdriver;
          this.decoderForExpressions = BoxedExpressionDecoder.Decoder(new
          AnalysisWrapper.TypeBindings<Local, Parameter, Method, Field, Property,
          Type, Attribute, Assembly, Expression, Variable>.ValueExpDecoder(this.mdriver.Context, this.mdriver.MetaDataDecoder));
          this.encoderForExpressions = BoxedExpressionEncoder.Encoder(this.mdriver.MetaDataDecoder, this.mdriver.Context);
        }

        public StateMachineInformation<Local> MoveNextPreAnalysisResult {
          get {
            return new StateMachineInformation<Local>(this.stateInfo, this.returnValues); 
          }
        }

        public INumericalAbstractDomain<BoxedExpression> InitialValue() {
          INumericalAbstractDomain<BoxedExpression> result = new AbstractDomains.Numerical.IntervalEnvironment<BoxedExpression>(decoderForExpressions, encoderForExpressions);
          return result;
        }

        #region IValueAnalysis<APC,INumericalAbstractDomain<Expression>,IVisitMSIL<APC,Local,Parameter,Method,Field,Type,Expression,Variable,INumericalAbstractDomain<Expression>,INumericalAbstractDomain<Expression>>,Variable,Expression,IExpressionContext<APC,Method,Type,Expression,Variable>> Members

        /// <summary>
        /// Here, we return the transfer function. Since we implement this via MSILVisitor, we just return this.
        /// </summary>
        /// <param name="context">The expression context is an interface we can use to find out more about expressions, such as their type etc.</param>
        /// <returns>The transfer function.</returns>
        public IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Variable, Variable, INumericalAbstractDomain<BoxedExpression>, INumericalAbstractDomain<BoxedExpression>> Visitor(IExpressionContext<APC, Local, Parameter, Method, Field, Type, Expression, Variable> context) {
          // store away the context for future reference
          this.context = context;
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
        public INumericalAbstractDomain<BoxedExpression> Join(Pair<APC, APC> edge, INumericalAbstractDomain<BoxedExpression> newState, INumericalAbstractDomain<BoxedExpression> prevState, out bool weaker, bool widen) {
          INumericalAbstractDomain<BoxedExpression> result = (INumericalAbstractDomain<BoxedExpression>)prevState.Join(newState);
          weaker = !prevState.LessEqual(newState);
          return result;
        }

        public bool IsBottom(APC pc, INumericalAbstractDomain<BoxedExpression> state) {
          return state.IsBottom;
        }

        public INumericalAbstractDomain<BoxedExpression> ImmutableVersion(INumericalAbstractDomain<BoxedExpression> state) {
          return state;
        }

        public INumericalAbstractDomain<BoxedExpression> MutableVersion(INumericalAbstractDomain<BoxedExpression> state) {
          return (INumericalAbstractDomain<BoxedExpression>)state.Clone();
        }

        public void Dump(Pair<INumericalAbstractDomain<BoxedExpression>, System.IO.TextWriter> stateAndWriter) {
          var tw = stateAndWriter.Two;
          tw.Write("numerical INumericalAbstractDomain<Expression>: {0} ", stateAndWriter.One);
        }

        /// <summary>
        /// This method is called by the underlying driver of the fixpoint computation. It provides delegates for future lookup
        /// of the abstract state at given pcs.
        /// </summary>
        /// <returns>Return true only if you want the fixpoint computation to eagerly cache each pc state.</returns>
        public Predicate<APC> CacheStates(IFixpointInfo<APC, INumericalAbstractDomain<BoxedExpression>> fixpointInfo) {
          return PCsAtBranches;
        }

        private bool PCsAtBranches(APC pc) {
          return false;
        }

        public INumericalAbstractDomain<BoxedExpression> ParallelAssign(Pair<APC, APC> edge, IFunctionalMap<Variable, FList<Variable>> sourceTargetMap, INumericalAbstractDomain<BoxedExpression> state) {
          var refinedMap = RefineMapToExpressions(edge.One, sourceTargetMap);
          state.AssignInParallel(refinedMap); 
          return state;
        }

        private Dictionary<BoxedExpression, FList<BoxedExpression>> RefineMapToExpressions(APC pc, IFunctionalMap<Variable, FList<Variable>> sourceTargetMap) {
          var result = new Dictionary<BoxedExpression, FList<BoxedExpression>>(sourceTargetMap.Count);

          foreach (Variable v in sourceTargetMap.Keys) {
            BoxedExpression vRefined = ToBoxedExpression(pc, v);
            result[vRefined] = FList<Variable>.Map<BoxedExpression>(delegate(Variable target) { 
              return BoxedExpression.For(this.mdriver.Context.For(target), this.decoderForExpressions.Outdecoder); }, 
              sourceTargetMap[v]);
          }
          return result;
        }
        protected BoxedExpression ToBoxedExpression(APC pc, Variable var) {
          return BoxedExpression.For(this.mdriver.Context.Refine(pc, var), this.decoderForExpressions.Outdecoder);
        }

        protected override INumericalAbstractDomain<BoxedExpression> Default(APC pc, INumericalAbstractDomain<BoxedExpression> data) {
          return data;
        }

        bool IsEnumeratorStatePredicate(APC pc, Variable condition, out int stateValue, out bool OpIsEq) {
          stateValue = -100;
          OpIsEq = false;
          IFullExpressionDecoder<Type, Expression> decoder = this.mdriver.ExpressionDecoder;
          Expression exp = this.context.Refine(pc, condition);
          BinaryOperator op;
          Expression left, right;
          if (decoder.IsBinaryOperator(exp, out op, out left, out right)) {
            if (op == BinaryOperator.Ceq || op == BinaryOperator.Cne_Un || op == BinaryOperator.Cobjeq) {
              if (op == BinaryOperator.Cne_Un) OpIsEq = false;
              else OpIsEq = true;
              object o;
              if (decoder.IsVariable(left, out o)) {
                string path = this.context.AccessPath(pc, (Variable)o);
                if (path != null) {
                  if (path.Contains(".<>") && path.EndsWith("state")) {
                    object value; Type type;
                    if (decoder.IsConstant(right, out value, out type)) {
                      if (type.Equals(this.mdriver.MetaDataDecoder.System_UInt32) || type.Equals(this.mdriver.MetaDataDecoder.System_Int32)) {
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

        public override INumericalAbstractDomain<BoxedExpression> Assume(APC pc, string tag, Variable condition, INumericalAbstractDomain<BoxedExpression> data) {
          BoxedExpression boxedCondition = BoxedExpression.For(this.context.Refine(pc, condition), this.decoderForExpressions.Outdecoder);
          if (tag == "false") {
            data.TestFalse(boxedCondition);
          } else if (tag == "true") {
            data.TestTrue(boxedCondition);
          }
          Variable varForThisDotState = ThisDotState(pc);
          BoxedExpression boxedThisDotState = BoxedExpression.For(this.context.Refine(pc, varForThisDotState), this.decoderForExpressions.Outdecoder);
          AbstractDomains.Numerical.Interval interval = data.BoundsFor(boxedThisDotState);

          AbstractDomains.Numerical.Rational rat;
          if (interval.TryGetSingletonValue(out rat)) {
            long lv;
            if (rat.TryInt64(out lv)) {
              int state = (int)lv;
              if (this.stateInfo.ContainsKey(state)) {
                this.stateInfo[state].Add(pc);
              } else {
                this.stateInfo[state] = new Set<APC>(pc);
              }
            }
          }
          return base.Assume(pc, tag, condition, data);
        }

        private Variable ThisDotState(APC pc) {
          Parameter pThis = this.mdriver.MetaDataDecoder.This(this.mdriver.CurrentMethod);
          Variable thisValue;
          if (this.context.TryParameterValue(pc, pThis, out thisValue)) {
            Variable addrThisDotState;
            if (this.context.TryFieldAddress(pc, thisValue, this.StateField, out addrThisDotState)) {
              Variable result;
              if (this.context.TryLoadIndirect(pc, addrThisDotState, out result)) {
                return result;
              }
            }
          }
          return default(Variable);
        }

        private Field stateField = default(Field);
        private Field StateField {
          get {
            if (stateField == null) {
              Type closureType = this.mdriver.MetaDataDecoder.DeclaringType(this.mdriver.CurrentMethod);
              foreach (Field f in this.mdriver.MetaDataDecoder.Fields(closureType)) {
                string fname = this.mdriver.MetaDataDecoder.Name(f);
                if (fname.Contains("<>") && fname.EndsWith("state")) {
                  stateField = f;
                  break;
                }
              }
            }
            return stateField;
          }
        }
        #endregion 
      }
    }

    /// <summary>
    /// Main interface method of the analyzer. It returns state machine information, given a method driver. 
    /// </summary>
    /// <param name="fullMethodName">The name of the method, should always be "MoveNext".</param>
    /// <param name="driver">A method analysis driver.</param>
    /// <returns></returns>
    public StateMachineInformation<Local> AnalyzeMoveNext<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly, Expression, Variable, ILogOptions>(
      string fullMethodName,
      IMethodDriver<APC, Local, Parameter, Method, Field, Property, Type, Attribute, Assembly, Expression, Variable, ILogOptions> driver
      )
      where Variable : IEquatable<Variable>
      where Expression : IEquatable<Expression>
      where Type : IEquatable<Type> 
      where ILogOptions : IFrameworkLogOptions
    {
      TypeBindings<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly, Expression, Variable, ILogOptions>.Analysis analysis =
        new TypeBindings<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly, Expression, Variable, ILogOptions>.Analysis(driver, this);
      driver.CreateForward(analysis)(analysis.InitialValue());
      return analysis.MoveNextPreAnalysisResult;
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
  public class StateMachineInformation<Local> {
    Dictionary<int, Set<APC>> stateInfo;

    public Dictionary<int, Set<APC>> StateInfo {
      get { return stateInfo; }
    }
    IEnumerable<string> returnValues;
    public StateMachineInformation(Dictionary<int, Set<APC>> stateInfo, IEnumerable<string> returnValues) {
      this.stateInfo = stateInfo;
      this.returnValues = returnValues;
    }

    /// <summary>
    /// Orderred "visible" state. A state is visible if this.__state can take this value when the movenext
    /// method is not running. This excludes internal states such as -1. Order the integer values of 
    /// the states since the smallest (0) is the initial state. 
    /// 
    /// Not every state that has an entry is a visible state, namely, DefaultCase is not. It has a case
    /// statement entry, which should never be visited. 
    /// </summary>
    public IEnumerable<int> OrderedVisibleStates {
      get {
        foreach (int state in stateInfo.Keys.OrderBy(delegate(int key) { return key; })) {
          if (state == MoveNextStateAnalyzer.DefaultCase) continue;
          yield return state;
        }
      }
    }

    public Set<APC> EntryPointForState(int state) {
      if (this.stateInfo.ContainsKey(state))
        return this.stateInfo[state];
      return Set<APC>.Empty;
    }
  }
}
