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
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;

using Microsoft.Research.DataStructures;
using Microsoft.Research.Graphs;

namespace Microsoft.Research.CodeAnalysis
{

  using Tag = System.String;
  using SubroutineContext = FList<Tuple<CFGBlock, CFGBlock, string>>;

  /// <summary>
  /// Represents a filter that determines what handlers apply to a given context.
  /// </summary>
  /// <typeparam name="Label">Underlying program point representation</typeparam>
  /// <typeparam name="Data">Client data type for auxiliary client information</typeparam>
  /// <typeparam name="Type">Type for exception filter</typeparam>
  public interface IHandlerFilter<Label, Type, Data>
  {
    /// <summary>Given handler is a catch handler with given exception filter.</summary>
    /// <param name="data">Client specific data that helps client determine match</param>
    /// <param name="exception">Catch type</param>
    /// <param name="stopPropagation">Client should set this to true if no outer enclosing handlers are possible</param>
    /// <returns>true if handler applies, false, if handler does not apply</returns>
    bool Catch(Data data, Type exception, out bool stopPropagation);

    /// <summary>Given handler is a filter handler with filter code.</summary>
    /// <param name="data">Client specific data that helps client determine match</param>
    /// <param name="filterCode">Code determining applicability of filter</param>
    /// <param name="stopPropagation">Client should set this to true if no outer enclosing handlers are possible</param>
    /// <returns>true if handler applies, false, if handler does not apply</returns>
    bool Filter(Data data, Label filterCode, out bool stopPropagation);
  }
  /// <summary>
  /// An abstract view of a control flow graph, generic over program points (and underlying code).
  /// </summary>
  /// <typeparam name="Label">The type of labels for specifying program points in the code.</typeparam>
  /// <typeparam name="Handler">The type of exception handler information of the underlying code.</typeparam>
  /// <typeparam name="AbstractPC">Client code should be generic over this type. It encapsulates
  ///                              program points due to finally expansion that are not 1-1 with Labels.</typeparam>
  public interface ICFG<Type, AbstractPC>
  {
    /// <summary>
    /// The entry point of the method
    /// </summary>
    AbstractPC Entry { get; }

    /// <summary>
    /// The normal exit point of the method
    /// </summary>
    AbstractPC NormalExit { get; }

    /// <summary>
    /// The exceptional exit point of the method
    /// </summary>
    AbstractPC ExceptionExit { get; }

    /// <summary>
    /// Returns the immediate normal successor program point if there is a single one.
    /// Otherwise, must use Successors which returns all (or 0).
    /// </summary>
    bool HasSingleSuccessor(AbstractPC ppoint, out AbstractPC singleSuccessor);

    /// <summary>
    /// Provides the immediate normal successor program points of the given ppoint
    /// </summary>
    IEnumerable<AbstractPC> Successors(AbstractPC ppoint);

    /// <summary>
    /// Returns the immediate normal predecessor program point if there is a single one.
    /// Otherwise, must use Predecessor which returns all (or 0).
    /// </summary>
    bool HasSinglePredecessor(AbstractPC ppoint, out AbstractPC singlePredecessor);

    /// <summary>
    /// Provides the immediate normal predecessor program points of the given ppoint
    /// </summary>
    IEnumerable<AbstractPC> Predecessors(AbstractPC ppoint);


    /// <summary>
    /// If ppoint is the beginning of a call instruction, then this provides the first PC prior to 
    /// ppoint and prior to any requires of the call
    /// </summary>
    AbstractPC PredecessorPCPriorToRequires(AbstractPC ppoint);

    /// <summary>
    /// Returns true if the given program point is the target of multiple forward control
    /// transfers.
    /// </summary>
    bool IsJoinPoint(AbstractPC ppoint);

    /// <summary>
    /// Returns true if the given program point is the target of multiple backward control
    /// transfers.
    /// </summary>
    bool IsSplitPoint(AbstractPC ppoint);

    /// <summary>
    /// Returns true if the given program point is a back edge target in a forward traversal
    /// </summary>
    bool IsForwardBackEdgeTarget(AbstractPC ppoint);

    /// <summary>
    /// Returns true if the given program point is a back edge target in a backward traversal
    /// </summary>
    bool IsBackwardBackEdgeTarget(AbstractPC ppoint);

    /// <summary>
    /// Returns true if the edge described by the two program points is a back edge in a forward
    /// traversal of the CFG.
    /// NOTE: the edge must be an edge related by the Successors relation above, not
    /// an arbitrary pair.
    /// </summary>
    bool IsForwardBackEdge(AbstractPC from, AbstractPC to);

    /// <summary>
    /// Returns true if the edge described by the two program points is a back edge in a backward
    /// traversal of the CFG.
    /// NOTE: the edge must be an edge related by the Predecessor relation above, not
    /// an arbitrary pair.
    /// </summary>
    bool IsBackwardBackEdge(AbstractPC from, AbstractPC to);

    /// <summary>
    /// Returns true if the program point is the first point in a logical block
    /// </summary>
    bool IsBlockStart(AbstractPC ppoint);

    /// <summary>
    /// Returns true if the program point is the last point in a logical block
    /// </summary>
    bool IsBlockEnd(AbstractPC ppoint);

    /// <summary>
    /// Returns a set of program points that are possible execution continuations if 
    /// an exception is raised at the given ppoint.
    ///
    /// The supplied predicate allows the client to determine which handlers are possible
    /// candidates. The predicate is called from closest enclosing to outermost.
    /// </summary>
    /// <param name="data">Arbitrary client data passed to the handler predicate</param>
    /// <returns>A set of continuation program points that represent how execution continues to a chosen handler.
    /// The continuation includes Finally and Fault executions that intervene between the given ppoint and a
    /// chosen handler.
    /// </returns>
    IEnumerable<AbstractPC> ExceptionHandlers<Data>(AbstractPC ppoint, Data data, IHandlerFilter<AbstractPC, Type, Data> handlerPredicate);

    /// <summary>
    /// Returns a graph wrapper for the cfg that can be used with standard graph algorithms
    /// </summary>
    IGraph<AbstractPC, Unit> AsGraph { get; }


    IDecodeMSIL<AbstractPC, Local, Parameter, Method, Field, Type, Unit, Unit, IMethodContext<AbstractPC, Field, Method>>
      GetDecoder<Local, Parameter, Method, Field, Property, Attribute, Assembly>(IDecodeMetaData<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly> mdDecoder);


    /// <summary>
    /// Get a subroutine view of the CFG for block level operations
    /// </summary>
    Subroutine Subroutine { get; }

    /// <summary>
    /// Returns a textual representation of an abstract PC
    /// </summary>
    string ToString(AbstractPC pc);

    /// <summary>
    /// Prints out a textual representation of the CFG
    ///
    /// Also decodes the synthetic IL from the CFG generation
    /// eg. Assumes
    /// </summary>
    /// <param name="tw">Target for output</param>
    /// <param name="edgePrinter">Optional info printer for successor edges</param>
    void Print(TextWriter tw, ILPrinter<AbstractPC> ilPrinter, BlockInfoPrinter<AbstractPC>/*?*/ edgePrinter, Func<CFGBlock, IEnumerable<SubroutineContext>> contextLookup, SubroutineContext context);

    /// <summary>
    /// Emits the transfer equations that represent this CFG
    /// </summary>
    void EmitTransferEquations(TextWriter tw, InvariantQuery<AbstractPC> invariantDB, AssumptionFinder<AbstractPC> assumptionFinder, CrossBlockRenamings<AbstractPC> renamings, RenamedVariables<AbstractPC> renamed);
  }


  /// <summary>
  /// Code query 
  /// </summary>
  public interface ICodeQuery<Label, Type, Method, T, R>
  {

    /// <summary>
    /// Called if code represents an unconditional branch to target.
    /// </summary>
    /// <param name="target">target of transfer</param>
    /// <param name="data">Client specific data threaded through the dispatch</param>
    /// <param name="current">Label where instruction is decoded</param>
    R Branch(T data, Label current, Label target);

    /// <summary>
    /// Called if code represents a conditional branch to target.
    /// </summary>
    /// <param name="data">Client specific data threaded through dispatch</param>
    /// <param name="current">Label where instruction is decoded</param>
    /// <param name="target">Label to which control is transferred by the branch</param>
    /// <param name="branchTrue">If true, control transfers to target on "true" condition. If false, control transfer on "false" condition.</param>
    /// <param name="fallthrough">If true, alternate control transfer to successor label. If false, elseTarget is provided and used as the alternate target</param>
    /// <param name="elseTarget">Only valid if !fallthrough.</param>
    R BranchCond(T data, Label current, Label target, bool branchTrue, bool fallthrough, Label elseTarget);

    /// <summary>
    /// Called if dispatched code represents a switch/case transfer.
    /// </summary>
    /// <param name="data">Client specific data threaded through dispatch</param>
    /// <param name="current">Label where instruction is decoded</param>
    /// <param name="type">Type of constants being matched</param>
    /// <param name="targets">List of pairs of matched constant and label if match succeeds</param>
    /// <param name="hasDefault">true if there is a default branch if no match in list</param>
    /// <param name="fallthrough">Only valid if <paramref name="hasDefault"/>. True if on default case, control transfers to successor label</param>
    /// <param name="defaultTarget">Only valid if <paramref name="hasDefault"/> and not <paramref name="fallthrough"/>. In that case, it indicates where control continues in default case.</param>
    R BranchSwitch(T data, Label current, Type type, IEnumerable<Pair<object, Label>> targets, bool hasDefault, bool fallthrough, Label defaultTarget);

    /// <summary>
    /// Called if Code represents an definite throw
    /// </summary>
    /// <param name="data">Client specific data threaded through the dispatch</param>
    /// <param name="current">Label where instruction is decoded</param>
    R Throw(T data, Label current);

    /// <summary>
    /// Called if Code represents an endfinally
    /// Some code representations (such as block structured code) will not use this instruction
    /// and do not need to synthesize it. The CFG construction will use endfinally in combination
    /// with handler extent information.
    /// </summary>
    /// <param name="data">Client specific data</param>
    /// <param name="current">Label marking this code</param>
    R EndFinally(T data, Label current);


    /// <summary>
    /// Called if Code represents a normal return statement from the current method
    ///
    /// It is allowed for code to fall off the end of the method body without an explicit return
    /// </summary>
    /// <param name="data">Client provided data</param>
    /// <param name="current">Label marking this code</param>
    /// <returns></returns>
    R Return(T data, Label current);

    /// <summary>
    /// Called if Code represents a nested code aggregate
    /// </summary>
    /// <param name="aggregateStart">Start label of aggregate</param>
    /// <param name="data">Client specific data threaded through the dispatch</param>
    /// <param name="current">Label where instruction is decoded</param>
    /// <param name="canBeTargetOfBranch">true if current can be a branch target. If not, the CFG can remove unnecessary program points </param>
    R Aggregate(T data, Label current, Label aggregateStart, bool canBeTargetOfBranch);

    /// <summary>
    /// Called if Code represents the call instruction to a particular method. Arguments are already evaluated at this point.
    /// </summary>
    /// <param name="data">Client specific data threaded through the dispatch</param>
    /// <param name="current">Label where instruction is decoded</param>
    R Call(T data, Label current, Method method, bool isVirtual);

    /// <summary>
    /// Called if Code represents a newObj instruction to a particular construtcor method. Arguments are already evaluated at this point.
    /// </summary>
    /// <param name="data">Client specific data threaded through the dispatch</param>
    /// <param name="current">Label where instruction is decoded</param>
    R NewObj(T data, Label current, Method constructor);

    /// <summary>
    /// Called if Code has no further structure for CFG purpose
    /// </summary>
    /// <param name="data">Client specific data threaded through the dispatch</param>
    /// <param name="current">Label where instruction is decoded</param>
    R Atomic(T data, Label current);

    /// <summary>
    /// Called if Code is a nop. This allows filtering out such code positions early
    /// </summary>
    R Nop(T data, Label current);

    /// <summary>
    /// Called if Code represents a BeginOld pc
    /// </summary>
    R BeginOld(T data, Label current);

    /// <summary>
    /// Called if Code represents an EndOld pc
    /// </summary>
    R EndOld(T data, Label current);
  }

  public interface ICodeProvider<Label, Method, Type> {
    /// <summary>
    /// Dispatches to the appropriate method in query based on the code at the given label
    /// </summary>
    R Decode<T, R>(T data, Label label, ICodeQuery<Label, Type, Method, T, R> query);

    //    Code Lookup(Label label);
    /// <summary>
    /// Returns the label of the code textually following the code at the given location (if sensible)
    /// </summary>
    /// <returns>true if next label is meaningful.</returns>
    bool Next(Label current, out Label nextLabel);

#if false
    /// <summary>
    /// For debugging
    /// </summary>
    void PrintCodeAt(Label pc, string indent, TextWriter tw);
#endif

    /// <summary>
    /// Returns true if pc has source context associated with it
    /// </summary>
    bool HasSourceContext(Label pc);

    /// <summary>
    /// Returns the name of the associated document of the source context for this pc
    /// </summary>
    string SourceDocument(Label pc);

    /// <summary>
    /// Returns the first line number in the source context of this pc.
    /// </summary>
    int SourceStartLine(Label pc);

    /// <summary>
    /// Returns the last line number in the source context of this pc.
    /// </summary>
    int SourceEndLine(Label pc);

    /// <summary>
    /// Returns the first column number in the source context of this pc.
    /// </summary>
    int SourceStartColumn(Label pc);

    /// <summary>
    /// Returns the first column number in the source context of this pc.
    /// </summary>
    int SourceEndColumn(Label pc);
    /// <summary>
    /// Returns the IL offset within the method of the given instruction or 0
    /// </summary>
    int ILOffset(Label pc);

    /// <summary>
    /// If the label corresponds to a contract (assert/requires, etc), this 
    /// may provide the user or tool provided condition.
    /// </summary>
    string SourceAssertionCondition(Label label);
  }

  public interface IMethodCodeProvider<Label, Method, Type, Handler> : ICodeProvider<Label, Method, Type> {
    /// <summary>
    /// Returns the handlers associated with the provided method
    /// </summary>
    IEnumerable<Handler> TryBlocks(Method method);

    /// <summary>
    /// Decodes ExceptionInfo for the kind of handler
    /// </summary>
    bool IsFaultHandler(Handler info);
    /// <summary>
    /// Decodes ExceptionInfo for the kind of handler
    /// </summary>
    bool IsFinallyHandler(Handler info);
    /// <summary>
    /// Decodes ExceptionInfo for the kind of handler
    /// </summary>
    bool IsCatchHandler(Handler info);
    /// <summary>
    /// Decodes ExceptionInfo for the kind of handler
    /// </summary>
    bool IsFilterHandler(Handler info);

    /// <summary>
    /// If the handler is a CatchHandler, this method returns the type of exception caught.
    /// Fails if !IsCatchHandler
    /// </summary>
    Type CatchType(Handler info);

    /// <summary>
    /// Should return true if this handler catches all Exceptions
    /// </summary>
    bool IsCatchAllHandler(Handler info);

    /// <summary>
    /// Decodes ExceptionInfo for the Label corresponding to the start of try block
    /// </summary>
    Label TryStart(Handler info);

    /// <summary>
    /// Decodes ExceptionInfo for the Label corresponding to the end of try block
    /// </summary>
    Label TryEnd(Handler info);

    /// <summary>
    /// Decodes ExceptionInfo for the Label corresponding to the start of filter decision block (if any)
    /// </summary>
    Label FilterDecisionStart(Handler info);

    /// <summary>
    /// Decodes ExceptionInfo for the Label corresponding to the start of handler block
    /// </summary>
    Label HandlerStart(Handler info);

    /// <summary>
    /// Decodes ExceptionInfo for the Label corresponding to the end of handler block
    /// </summary>
    Label HandlerEnd(Handler info);
  }

  public interface ICodeConsumer<Local, Parameter, Method, Field, Type, Data, Result> {
    Result Accept<Label>(ICodeProvider<Label, Method, Type> codeProvider, IDecodeMSIL<Label, Local, Parameter, Method, Field, Type, Unit, Unit, Unit> decoder, Label entryPoint, Data data);
  }

  public interface IMethodCodeConsumer<Local, Parameter, Method, Field, Type, Data, Result> {
    Result Accept<Label, Handler>(IMethodCodeProvider<Label, Method, Type, Handler> codeProvider, IDecodeMSIL<Label, Local, Parameter, Method, Field, Type, Unit, Unit, Unit> decoder, Label entryPoint, Method method, Data data);
  }
}