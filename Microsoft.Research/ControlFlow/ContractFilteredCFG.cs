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
using Microsoft.Research.Graphs;
using System.Diagnostics.Contracts;

namespace Microsoft.Research.CodeAnalysis
{

  using SubroutineContext = FList<STuple<CFGBlock, CFGBlock, string>>;

  public class ContractFilteredCFG : ICFG, IEdgeSubroutineAdaptor
  {
    readonly ICFG underlying;

    [ContractInvariantMethod]
    void ObjectInvariant()
    {
      Contract.Invariant(underlying != null);
    }

    public ContractFilteredCFG(ICFG underlying)
    {
      Contract.Requires(underlying != null);
      this.underlying = underlying;
    }

    #region ICFG Members

    public APC Entry
    {
      get { return underlying.Entry; }
    }

    public APC EntryAfterRequires
    {
      get { return underlying.EntryAfterRequires; }
    }


    public APC NormalExit
    {
      get { return underlying.NormalExit; }
    }

    public APC ExceptionExit
    {
      get { return underlying.ExceptionExit; }
    }

    public APC Post(APC pc)
    {
      return underlying.Post(pc);
    }

    public bool HasSingleSuccessor(APC ppoint, out APC singleSuccessor)
    {
      CallAdaption.Push<IEdgeSubroutineAdaptor>(this);
      try
      {
        return underlying.HasSingleSuccessor(ppoint, out singleSuccessor);
      }
      finally {
        CallAdaption.Pop(this);
      }
    }

    public IEnumerable<APC> Successors(APC ppoint)
    {
      CallAdaption.Push<IEdgeSubroutineAdaptor>(this);
      try
      {
        return underlying.Successors(ppoint);
      }
      finally {
        CallAdaption.Pop(this);
      }
    }

    public bool HasSinglePredecessor(APC ppoint, out APC singlePredecessor, bool skipContracts)
    {
      CallAdaption.Push<IEdgeSubroutineAdaptor>(this);
      try
      {
        return underlying.HasSinglePredecessor(ppoint, out singlePredecessor, skipContracts);
      }
      finally {
        CallAdaption.Pop(this);
      }
    }

    public IEnumerable<APC> Predecessors(APC ppoint, bool skipContracts)
    {
      CallAdaption.Push<IEdgeSubroutineAdaptor>(this);
      try
      {
        return underlying.Predecessors(ppoint, skipContracts);
      }
      finally
      {
        CallAdaption.Pop(this);
      }
    }

    public APC PredecessorPCPriorToRequires(APC ppoint)
    {
      CallAdaption.Push<IEdgeSubroutineAdaptor>(this);
      try
      {
        return underlying.PredecessorPCPriorToRequires(ppoint);
      }
      finally
      {
        CallAdaption.Pop(this);
      }
    }

    public bool IsJoinPoint(APC ppoint)
    {
      return underlying.IsJoinPoint(ppoint);
    }

    public bool IsSplitPoint(APC ppoint)
    {
      return underlying.IsSplitPoint(ppoint);
    }

    public bool IsForwardBackEdgeTarget(APC ppoint)
    {
      return underlying.IsForwardBackEdgeTarget(ppoint);
    }

    public bool IsBackwardBackEdgeTarget(APC ppoint)
    {
      return underlying.IsBackwardBackEdgeTarget(ppoint);
    }

    public bool IsForwardBackEdge(APC from, APC to)
    {
      return underlying.IsForwardBackEdge(from, to);
    }

    public bool IsBackwardBackEdge(APC from, APC to)
    {
      return underlying.IsBackwardBackEdge(from, to);
    }

    public bool IsBlockStart(APC ppoint)
    {
      return underlying.IsBlockStart(ppoint);
    }

    public bool IsBlockEnd(APC ppoint)
    {
      return underlying.IsBlockEnd(ppoint);
    }

    public IEnumerable<APC> ExceptionHandlers<Type, Data>(APC ppoint, Data data, IHandlerFilter<Type, Data> handlerPredicate)
    {
      return underlying.ExceptionHandlers(ppoint, data, handlerPredicate);
    }

    public Graphs.IGraph<APC, DataStructures.Unit> AsForwardGraph(bool includeExceptionEdges)
    {
      var underlyingGraph = underlying.AsForwardGraph(includeExceptionEdges);
      return new GraphWrapper<APC, Unit>(
        underlyingGraph.Nodes,
        node => SuccessorEdges(node, underlyingGraph));
    }

    IEnumerable<Pair<Unit, APC>> SuccessorEdges(APC pc, IGraph<APC,Unit> underlyingGraph)
    {
      CallAdaption.Push(this);
      try
      {
        foreach (var succ in underlyingGraph.Successors(pc))
        {
          yield return succ;
        }
      }
      finally
      {
        CallAdaption.Pop(this);
      }
    }

    public FList<Pair<string, Subroutine>> GetOrdinaryEdgeSubroutines(CFGBlock from, CFGBlock to, SubroutineContext context)
    {
      CallAdaption.Push(this);

      try
      {
        return this.Subroutine.GetOrdinaryEdgeSubroutines(from, to, context);
      }
      finally
      {
        CallAdaption.Pop(this);
      }
    }


    public Graphs.IGraph<APC, DataStructures.Unit> AsBackwardGraph(bool includeExceptionEdges, bool skipContracts)
    {
      var underlyingGraph = underlying.AsBackwardGraph(includeExceptionEdges, skipContracts);
      return new GraphWrapper<APC, Unit>(
        underlyingGraph.Nodes,
        node => SuccessorEdges(node, underlyingGraph));
    }

    public IEnumerable<CFGBlock> LoopHeads { get { return underlying.LoopHeads; } }

    public IDecodeMSIL<APC, Local, Parameter, Method, Field, Type, Unit, Unit, IMethodContext<Field, Method>, Unit> GetDecoder<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder)
    {
      return underlying.GetDecoder(mdDecoder);
    }

    /// <summary>
    /// Does not filter edge in this subroutine!. If we want filtering, we have to wrap the subroutine itself too.
    /// </summary>
    public Subroutine Subroutine
    {
      get { 
        return underlying.Subroutine; 
      }
    }

    public string ToString(APC pc)
    {
      CallAdaption.Push<IEdgeSubroutineAdaptor>(this);
      try
      {
        return underlying.ToString(pc);
      }
      finally
      {
        CallAdaption.Pop(this);
      }
    }

    public void Print(System.IO.TextWriter tw, ILPrinter<APC> ilPrinter, BlockInfoPrinter<APC> edgePrinter, Func<CFGBlock, IEnumerable<DataStructures.FList<DataStructures.STuple<CFGBlock, CFGBlock, string>>>> contextLookup, DataStructures.FList<DataStructures.STuple<CFGBlock, CFGBlock, string>> context)
    {
      CallAdaption.Push<IEdgeSubroutineAdaptor>(this);
      try
      {
        underlying.Print(tw, ilPrinter, edgePrinter, contextLookup, context);
      }
      finally
      {
        CallAdaption.Pop(this);
      }
    }

    #endregion

    #region IEdgeSubroutineAdaptor Members

    public DataStructures.FList<DataStructures.Pair<string, Subroutine>> GetOrdinaryEdgeSubroutinesInternal(CFGBlock from, CFGBlock to, DataStructures.FList<DataStructures.STuple<CFGBlock, CFGBlock, string>> context)
    {
      var result = CallAdaption.Inner<IEdgeSubroutineAdaptor>(this).GetOrdinaryEdgeSubroutinesInternal(from, to, context);
      return result.Filter(pair => !(pair.Two.IsContract || pair.Two.IsOldValue));
    }

    #endregion
  }
}
