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
  using SubroutineContext = FList<STuple<CFGBlock, CFGBlock, string>>;
  using System.Diagnostics.Contracts;

  /// <summary>
  /// Used to pass a handler to emit warnings
  /// </summary>
  public delegate void ErrorHandler(string format, params string[] args);

  /// <summary>
  /// Represents a filter that determines what handlers apply to a given context.
  /// </summary>
  /// <typeparam name="Label">Underlying program point representation</typeparam>
  /// <typeparam name="Data">Client data type for auxiliary client information</typeparam>
  /// <typeparam name="Type">Type for exception filter</typeparam>
  public interface IHandlerFilter<Type, Data>
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
    bool Filter(Data data, APC filterCode, out bool stopPropagation);
  }
  /// <summary>
  /// An abstract view of a control flow graph, generic over underlying code.
  /// </summary>
  public partial interface ICFG
  {
    /// <summary>
    /// The entry point of the method
    /// </summary>
    APC Entry { get; }

    /// <summary>
    /// The entry point of the method after all requires contracts
    /// </summary>
    APC EntryAfterRequires { get; }

    /// <summary>
    /// The normal exit point of the method
    /// </summary>
    APC NormalExit { get; }

    /// <summary>
    /// The exceptional exit point of the method
    /// </summary>
    APC ExceptionExit { get; }

    /// <summary>
    /// If the label has a single successor, then this returns the unique post pc.
    /// Otherwise the result is equal to label.
    /// </summary>
    APC Post(APC label);

    /// <summary>
    /// Returns the immediate normal successor program point if there is a single one.
    /// Otherwise, must use Successors which returns all (or 0).
    /// </summary>
    [Pure]
    bool HasSingleSuccessor(APC ppoint, out APC singleSuccessor);

    /// <summary>
    /// Provides the immediate normal successor program points of the given ppoint
    /// </summary>
    [Pure]
    IEnumerable<APC> Successors(APC ppoint);

    /// <summary>
    /// Returns the immediate normal predecessor program point if there is a single one.
    /// Otherwise, must use Predecessor which returns all (or 0).
    /// </summary>
    [Pure]
    bool HasSinglePredecessor(APC ppoint, out APC singlePredecessor, bool skipContracts = false);

    /// <summary>
    /// Provides the immediate normal predecessor program points of the given ppoint
    /// </summary>
    [Pure]
    IEnumerable<APC> Predecessors(APC ppoint, bool skipContracts = false);

    /// <summary>
    /// If ppoint is the beginning of a call instruction, then this provides the first PC prior to 
    /// ppoint and prior to any requires of the call
    /// </summary>
    [Pure]
    APC PredecessorPCPriorToRequires(APC ppoint);

    /// <summary>
    /// Returns true if the given program point is the target of multiple forward control
    /// transfers.
    /// </summary>
    [Pure]
    bool IsJoinPoint(APC ppoint);

    /// <summary>
    /// Returns true if the given program point is the target of multiple backward control
    /// transfers.
    /// </summary>
    [Pure]
    bool IsSplitPoint(APC ppoint);

    /// <summary>
    /// Returns true if the given program point is a back edge target in a forward traversal
    /// </summary>
    [Pure]
    bool IsForwardBackEdgeTarget(APC ppoint);

    /// <summary>
    /// Returns true if the given program point is a back edge target in a backward traversal
    /// </summary>
    [Pure]
    bool IsBackwardBackEdgeTarget(APC ppoint);

    /// <summary>
    /// Returns true if the edge described by the two program points is a back edge in a forward
    /// traversal of the CFG.
    /// NOTE: the edge must be an edge related by the Successors relation above, not
    /// an arbitrary pair.
    /// </summary>
    [Pure]
    bool IsForwardBackEdge(APC from, APC to);

    /// <summary>
    /// Returns true if the edge described by the two program points is a back edge in a backward
    /// traversal of the CFG.
    /// NOTE: the edge must be an edge related by the Predecessor relation above, not
    /// an arbitrary pair.
    /// </summary>
    [Pure]
    bool IsBackwardBackEdge(APC from, APC to);

    /// <summary>
    /// Returns the APC corresponding to loop heads.
    /// </summary>
    IEnumerable<CFGBlock> LoopHeads { get; }

    /// <summary>
    /// Returns true if the program point is the first point in a logical block
    /// </summary>
    [Pure]
    bool IsBlockStart(APC ppoint);

    /// <summary>
    /// Returns true if the program point is the last point in a logical block
    /// </summary>
    [Pure]
    bool IsBlockEnd(APC ppoint);

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
    [Pure]
    IEnumerable<APC> ExceptionHandlers<Type, Data>(APC ppoint, Data data, IHandlerFilter<Type, Data> handlerPredicate);

    /// <summary>
    /// Returns a graph wrapper for the cfg that can be used with standard graph algorithms.
    /// There's only one APC used per block (the first in the block), and the set of nodes is not
    /// explicitly given.
    /// </summary>
    [Pure]
    IGraph<APC, Unit> AsForwardGraph(bool includeExceptionEdges);

    /// <summary>
    /// Returns a graph wrapper for the cfg that can be used with standard graph algorithms.
    /// There's only one APC used per block (the first in the block), and the set of nodes is not
    /// explicitly given.
    /// </summary>
    [Pure]
    IGraph<APC, Unit> AsBackwardGraph(bool includeExceptionEdges, bool skipContracts);

    [Pure]
    IDecodeMSIL<APC, Local, Parameter, Method, Field, Type, Unit, Unit, IMethodContext<Field, Method>, Unit>
      GetDecoder<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder);


    /// <summary>
    /// Get a subroutine view of the CFG for block level operations
    /// </summary>
    Subroutine Subroutine { get; }

    /// <summary>
    /// Get the subroutines on the edge from ---> to
    /// </summary>
    FList<Pair<string, Subroutine>> GetOrdinaryEdgeSubroutines(CFGBlock from, CFGBlock to, SubroutineContext context);

    /// <summary>
    /// Returns a textual representation of an abstract PC
    /// </summary>
    [Pure]
    string ToString(APC pc);

    /// <summary>
    /// Prints out a textual representation of the CFG
    ///
    /// Also decodes the synthetic IL from the CFG generation
    /// eg. Assumes
    /// </summary>
    /// <param name="tw">Target for output</param>
    /// <param name="edgePrinter">Optional info printer for successor edges</param>
    void Print(TextWriter tw, ILPrinter<APC> ilPrinter, BlockInfoPrinter<APC>/*?*/ edgePrinter, Func<CFGBlock, IEnumerable<SubroutineContext>> contextLookup, SubroutineContext context);

  }

  #region ICFG contract binding
  [ContractClass(typeof(ICFGContract))]
  public partial interface ICFG
  {

  }

  [ContractClassFor(typeof(ICFG))]
  abstract class ICFGContract : ICFG
  {
    #region ICFG Members

    public APC Entry
    {
      get {
        throw new NotImplementedException();
      }
    }

    public APC EntryAfterRequires
    {
      get {
        throw new NotImplementedException();
      }
    }

    public APC NormalExit
    {
      get {
        throw new NotImplementedException();
      }
    }

    public APC ExceptionExit
    {
      get {
        throw new NotImplementedException();
      }
    }

    public APC Post(APC label)
    {
      throw new NotImplementedException();
    }

    public bool HasSingleSuccessor(APC ppoint, out APC singleSuccessor)
    {
      throw new NotImplementedException();
    }

    public IEnumerable<APC> Successors(APC ppoint)
    {
      Contract.Ensures(Contract.Result<IEnumerable<APC>>() != null);
      throw new NotImplementedException();
    }

    public bool HasSinglePredecessor(APC ppoint, out APC singlePredecessor, bool skipContracts)
    {
      throw new NotImplementedException();
    }

    public IEnumerable<APC> Predecessors(APC ppoint, bool skipContracts)
    {
      Contract.Ensures(Contract.Result<IEnumerable<APC>>() != null);
      throw new NotImplementedException();
    }

    public APC PredecessorPCPriorToRequires(APC ppoint)
    {
      Contract.Requires(ppoint.Index == 0);

      throw new NotImplementedException();
    }

    public bool IsJoinPoint(APC ppoint)
    {
      throw new NotImplementedException();
    }

    public bool IsSplitPoint(APC ppoint)
    {
      throw new NotImplementedException();
    }

    public bool IsForwardBackEdgeTarget(APC ppoint)
    {
      throw new NotImplementedException();
    }

    public bool IsBackwardBackEdgeTarget(APC ppoint)
    {
      throw new NotImplementedException();
    }

    public bool IsForwardBackEdge(APC from, APC to)
    {
      throw new NotImplementedException();
    }

    public bool IsBackwardBackEdge(APC from, APC to)
    {
      throw new NotImplementedException();
    }

    public IEnumerable<CFGBlock> LoopHeads
    {
      get {
        Contract.Ensures(Contract.Result<IEnumerable<CFGBlock>>() != null);
        throw new NotImplementedException();
      }
    }

    public bool IsBlockStart(APC ppoint)
    {
      throw new NotImplementedException();
    }

    public bool IsBlockEnd(APC ppoint)
    {
      throw new NotImplementedException();
    }

    public IEnumerable<APC> ExceptionHandlers<Type, Data>(APC ppoint, Data data, IHandlerFilter<Type, Data> handlerPredicate)
    {
      Contract.Ensures(Contract.Result<IEnumerable<APC>>() != null);
      throw new NotImplementedException();
    }

    public IGraph<APC, Unit> AsForwardGraph(bool includeExceptionEdges)
    {
      Contract.Ensures(Contract.Result<IGraph<APC, Unit>>() != null);
      throw new NotImplementedException();
    }

    public IGraph<APC, Unit> AsBackwardGraph(bool includeExceptionEdges, bool skipContracts)
    {
      Contract.Ensures(Contract.Result<IGraph<APC, Unit>>() != null);
      throw new NotImplementedException();
    }

    public IDecodeMSIL<APC, Local, Parameter, Method, Field, Type, Unit, Unit, IMethodContext<Field, Method>, Unit> GetDecoder<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder)
    {
      Contract.Ensures(Contract.Result<IDecodeMSIL<APC, Local, Parameter, Method, Field, Type, Unit, Unit, IMethodContext<Field, Method>, Unit>>() != null);
      throw new NotImplementedException();
    }

    public Subroutine Subroutine
    {
      get {
        Contract.Ensures(Contract.Result<Subroutine>() != null);
        throw new NotImplementedException();
      }
    }

    public Tag ToString(APC pc)
    {
      Contract.Ensures(Contract.Result<string>() != null);
      throw new NotImplementedException();
    }

    public void Print(TextWriter tw, ILPrinter<APC> ilPrinter, BlockInfoPrinter<APC> edgePrinter, Func<CFGBlock, IEnumerable<SubroutineContext>> contextLookup, SubroutineContext context)
    {
      throw new NotImplementedException();
    }

    #endregion


    public FList<Pair<Tag, Subroutine>> GetOrdinaryEdgeSubroutines(CFGBlock from, CFGBlock to, SubroutineContext context)
    {
      Contract.Requires(from != null);
      Contract.Requires(to != null);
      throw new NotImplementedException();
    }
  }
  #endregion

  /// <summary>
  /// Code query 
  /// </summary>
  public interface ICodeQuery<Label, Local, Parameter, Method, Field, Type, Data, Result> : IVisitMSIL<Label, Local, Parameter, Method, Field, Type, Unit,Unit, Data, Result>
  {
    /// <summary>
    /// Called if Code represents a nested code aggregate
    /// </summary>
    /// <param name="aggregateStart">Start label of aggregate</param>
    /// <param name="data">Client specific data threaded through the dispatch</param>
    /// <param name="current">Label where instruction is decoded</param>
    /// <param name="canBeTargetOfBranch">true if current can be a branch target. If not, the CFG can remove unnecessary program points </param>
    Result Aggregate(Label current, Label aggregateStart, bool canBeTargetOfBranch, Data data);

  }

  public interface ICodeProvider<Label, Local, Parameter, Method, Field, Type> 
  {
    /// <summary>
    /// Dispatches to the appropriate method in query based on the code at the given label
    /// </summary>
    R Decode<Visitor, T, R>(Label label, Visitor query, T data)
      where Visitor : ICodeQuery<Label, Local, Parameter, Method, Field, Type, T, R>;

    //    Code Lookup(Label label);
    /// <summary>
    /// Returns the label of the code textually following the code at the given location (if sensible)
    /// </summary>
    /// <returns>true if next label is meaningful.</returns>
    bool Next(Label current, out Label nextLabel);

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
    /// The character index of the first character of the source context of this pc,
    /// when treating the source document as a single string.
    /// </summary>
    int SourceStartIndex(Label pc);

    /// <summary>
    /// The number of characters in the source context of this pc.
    /// </summary>
    int SourceLength(Label pc);

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

  public interface IMethodCodeProvider<Label, Local, Parameter, Method, Field, Type, Handler> : ICodeProvider<Label, Local, Parameter, Method, Field, Type> {
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
    Result Accept<Label>(ICodeProvider<Label, Local, Parameter, Method, Field, Type> codeProvider, Label entryPoint, Data data);
  }

  public interface IMethodCodeConsumer<Local, Parameter, Method, Field, Type, Data, Result> {
    Result Accept<Label, Handler>(IMethodCodeProvider<Label, Local, Parameter, Method, Field, Type, Handler> codeProvider, Label entryPoint, Method method, Data data);
  }
}