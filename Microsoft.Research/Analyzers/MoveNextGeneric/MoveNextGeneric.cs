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


namespace Microsoft.Research.MoveNextGeneric {
  /// <summary>
  /// The driver class to analyze a movenext method of an iterator. The compiler-generated movenext method in C# is a state-machine, 
  /// which, when called from a different state, executes a different slice of the method body. Our analysis of the movenext thus 
  /// needs to establish dataflow facts of the fields to pass information from the analysis of one slice to another. We call such 
  /// facts invariant candidate, in that, when our analysis finishes, the final candidate is indeed an object invariant of the iterator 
  /// class.  
  /// 
  /// The main entry point of the driver class is the Analyze method, which, when supplied with an analysis for one pass of one slice, 
  /// (an IMoveNextOnePassAnalysis), will manage the state machine to analyze all the slices corresponding to different states. This
  /// one pass analysis will be refered to as underlying analysis.
  /// </summary>
  /// <typeparam name="Local">Local variables</typeparam>
  /// <typeparam name="Parameter">Parameters</typeparam>
  /// <typeparam name="Method">Methods</typeparam>
  /// <typeparam name="Field">Fields</typeparam>
  /// <typeparam name="Property">Properties</typeparam>
  /// <typeparam name="Type">Types</typeparam>
  /// <typeparam name="Attribute">Attributes</typeparam>
  /// <typeparam name="Expression">Expressions used by the underlying analysis</typeparam>
  /// <typeparam name="Assembly">Assembly types</typeparam>
  /// <typeparam name="ILogOptions">ILogOptions</typeparam>
  /// <typeparam name="Variable">Symbolic variable types</typeparam>
  public class MoveNextDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions>
    : IMoveNextDriver<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly, Expression, Variable>
    where Variable : IEquatable<Variable>
    where Expression : IEquatable<Expression>
    where Type : IEquatable<Type>
    where ILogOptions : IFrameworkLogOptions
    {
    IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> driver;
    StateMachineInformation<Local> stateInfo;
    public MoveNextDriver(IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions>
      driver) {
      this.driver = driver;
      MoveNextStateAnalyzer preAnalyzer = new MoveNextStateAnalyzer();
      stateInfo = preAnalyzer.AnalyzeMoveNext("MoveNext", driver);
      // If we are not able 
      if (stateInfo.OrderedVisibleStates == null || stateInfo.OrderedVisibleStates.Count() == 0) {
        stateInfo = null;
      }
    }

    /// <summary>
    /// Given an underlying analysis and generator for a bottom value, this method computes a mapping between a state of
    /// the iterator and fixpoint information computed by the underlying analysis for the slice that corresponds to that
    /// state. 
    /// 
    /// The analysis runs the underlying analyzer on state 0, the initial state, first. The state from the return point
    /// of this state will be the current value of the invariant candidate. 
    /// Then for every continuing state (the state from which a new item is generated for the ienumerable result), we map
    /// the current invariant candidate and map it to the entry of the method, and run the underlying analysis for that 
    /// state. Running the underlying analysis for a particular state requires a set of program points, which are the entry
    /// points of other slices. The underlying analysis will mark those program points as unreachable. The exit abstract
    /// state will become the current invariant candidate. This process finishes when a fixedpoint is reached. 
    /// </summary>
    /// <typeparam name="MyDomain">The domain of the underlying analysis.</typeparam>
    /// <param name="analysis">The underlying analysis.</param>
    /// <param name="bv">A delegate that generates a bottom value for MyDomain.</param>
    /// <returns></returns>
    public IFactQuery<BoxedExpression, Variable> Analyze<MyDomain>(
      IAbstractAnalysis<Local, Parameter, Method, Field, Property, Type, Expression, Attribute, Assembly, MyDomain, Variable> analysis
    )
    {
      // Fix point computation: fixpoint of the iterator analysis is a mapping: answers. 
      if (stateInfo == null) {
        return null;
      }
      bool fixedpointReached = false;
      MyDomain d = default(MyDomain), oldd = default(MyDomain);
      var answers = (IFunctionalMap<int, IFixpointInfo<APC,MyDomain>>)FunctionalMap<int, IFixpointInfo<APC,MyDomain>>.Empty;
      Dictionary<int, MyDomain> invariantCandidate = new Dictionary<int, MyDomain>();

      int pass = 0;
      while (!fixedpointReached) {
        fixedpointReached = true;
        // Going through every state of the state machine, if the final states changes at least once
        // then fixpoint is not reached. 
        foreach (int state in stateInfo.OrderedVisibleStates) {
          // The initial state is only analyzed once. 
          if (state < 0) continue;
          if (state == 0 && pass > 0) {
            continue;
          }
          // Initial value for one pass, either TOP, if we analyze it for the first time
          // or the invariantCandidate.
          if (!invariantCandidate.ContainsKey(state)) {
            invariantCandidate[state] = d = analysis.GetTopValue();
          } else {
            d = analysis.MutableVersion(invariantCandidate[state]);
          }

          // Call the underlying analysis for one pass
          Set<APC> cutOffPCs = GetStateEntriesOtherThan(stateInfo, state);
          var fixpoint = driver.CreateForwardForIterator(analysis, () => analysis.GetBottomValue(), cutOffPCs)(d);
          // Fill the result table with the most recent fixpoint information. 
          answers = answers.Add(state, fixpoint);

          // Getting the state from the return point of the most recent pass, map it
          // to the entry point, and join it with the current invariant candidate
          bool changed = false;
          var fakeEdge = new Pair<APC, APC>(driver.CFG.NormalExit, driver.CFG.Entry);
          foreach (int possibleNextState in stateInfo.OrderedVisibleStates) {
            bool isBottom; 
            MyDomain newInvariantState = GetReturnState(fixpoint, analysis, possibleNextState, out isBottom, analysis.GetTopValue());
            if (invariantCandidate.ContainsKey(possibleNextState)) {
              oldd = invariantCandidate[possibleNextState];
              // TODO: use a more sophisticated widenning strategy. 
              bool toWiden = (pass > 2) ? true : false;
              if (isBottom) {
                d = oldd;
              } else {
                d = analysis.Join(fakeEdge, newInvariantState, oldd, out changed, toWiden);
              }
              if (changed) {
                invariantCandidate[possibleNextState] = d;
              }
            } else {
              if (!isBottom) {
                changed = true;
                invariantCandidate[possibleNextState] = newInvariantState;
              }
            }
            if (changed) fixedpointReached = false;
          }
        }
        pass++;
      }
      var result = new DisjunctionFactQuery<Variable>(this.driver.ValueLayer.Decoder.IsUnreachable);
      answers.Visit((statekey, fixpoint) => { result.Add(analysis.FactQuery(fixpoint)); return VisitStatus.ContinueVisit; });
      return result;
    }

    MyDomain GetReturnState<MyDomain>(IFixpointInfo<APC,MyDomain> fixpoint, IAbstractAnalysis<Local, Parameter, Method, Field, Property, Type, Expression, Attribute, Assembly, MyDomain, Variable> analysis, int state, out bool isBottom, MyDomain defaultValue) {
      MyDomain returnState= defaultValue;
      var fakeEdge = new Pair<APC, APC>(driver.CFG.NormalExit, driver.CFG.Entry);
      if (stateInfo.StateReturnPoints == null || !stateInfo.StateReturnPoints.ContainsKey(state)) {
        isBottom = true;
      } else {
        bool first = true;
        isBottom = true;
        foreach (APC returnPoint in stateInfo.StateReturnPoints[state]) {
          MyDomain tmp;
          if (fixpoint.PostState(returnPoint, out tmp) && !analysis.IsBottom(returnPoint, tmp)) {
            isBottom = false;
            var edge = new Pair<APC, APC>(returnPoint, driver.CFG.Entry.Post());
            IFunctionalMap<Variable, FList<Variable>> mapping = GetFieldMapping(driver, returnPoint, driver.CFG.Entry.Post());
            MyDomain newState = analysis.EdgeConversion(fakeEdge.One, fakeEdge.Two, true, mapping, tmp);
            bool ignore;
            if (first) {
              returnState = newState;
              first = false;
            } else {
              returnState = analysis.Join(edge, newState, returnState, out ignore, false);
            }
          }
        }
      }
      return returnState;
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
    IFunctionalMap<Variable, FList<Variable>> GetFieldMapping(IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions>
      driver, APC pc1, APC pc2) {
      FunctionalMap<Variable, FList<Variable>> fieldMapping = null;
      fieldMapping = FunctionalMap<Variable, FList<Variable>>.Empty;
      foreach (Pair<string, Variable> pair in SymbolicValuesAt(pc1,driver)) {
        if (!fieldMapping.Contains(pair.Two)) {
          fieldMapping = (FunctionalMap<Variable, FList<Variable>>)fieldMapping.Add(pair.Two, FList<Variable>.Empty);
        }
        foreach (Pair<string, Variable> pair2 in SymbolicValuesAt(pc2,driver)) {
          if (pair.One == pair2.One) {
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
    public IEnumerable<Pair<string, Variable>> SymbolicValuesAt
      (APC pc, IMethodDriver<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly, Expression, Variable, ILogOptions> driver) 
    {
      Variable thisV;
      var context = driver.Context;
      var mdDecoder = driver.MetaDataDecoder;
      if (!context.ValueContext.TryParameterValue(pc, mdDecoder.This(context.MethodContext.CurrentMethod), out thisV)) { yield break; }
      Type currentType = mdDecoder.DeclaringType(context.MethodContext.CurrentMethod);
      foreach (Field f in mdDecoder.Fields(currentType)) {
        Variable fieldAddr;
        if (context.ValueContext.TryFieldAddress(pc, thisV, f, out fieldAddr)) {
          Variable v;
          if (context.ValueContext.TryLoadIndirect(pc, fieldAddr, out v)) {
            Type fieldType = mdDecoder.FieldType(f);
            if (fieldType != null && mdDecoder.IsArray(fieldType)) {
              Variable arrayLength;
              if (context.ValueContext.TryGetArrayLength(pc, v, out arrayLength)) {
                yield return new Pair<string, Variable>(mdDecoder.Name(f) + ".Length", arrayLength);
              }
            }
            yield return new Pair<string, Variable>(mdDecoder.Name(f), v);
          }
        }
      }
    }

    /// <summary>
    /// Given the state machine information, return the set of entry points for the slices that 
    /// do not correspond to "state". The entry point of a slice refers to program point for the case
    /// statements in the switch (this.__state) statement in a MoveNext method. 
    /// </summary>
    /// <param name="info"></param>
    /// <param name="state"></param>
    /// <returns></returns>
    Set<APC> GetStateEntriesOtherThan(StateMachineInformation<Local> info, int state) {
      Set<APC> result = new Set<APC>();
      foreach (int s in info.StateSliceEntries.Keys) {
        if (s == state) continue;
        var set = info.StateSliceEntries[s];
        result = result | set;
      }
      return result;
    }
  }
}
