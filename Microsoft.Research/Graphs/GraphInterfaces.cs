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
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.Research.DataStructures;
using System.Diagnostics.Contracts;

namespace Microsoft.Research.Graphs
{

  [ContractClass(typeof(IGraphContracts<,>))]
  public interface IGraph<Node, Info>
  {
    [Pure]
    bool Contains(Node node);
    IEnumerable<Node>/*!*/ Nodes { get; }
    IEnumerable<Pair<Info,Node>> Successors(Node node);
  }

  [ContractClassFor(typeof(IGraph<,>))]
  abstract class IGraphContracts<Node, Info> : IGraph<Node, Info>
  {
    #region IGraph<Node,Info> Members

    public bool Contains(Node node)
    {
      return default(bool);
    }

    public IEnumerable<Node> Nodes
    {
      get { 
        Contract.Ensures(Contract.Result<IEnumerable<Node>>() != null); 
        return null; 
      }
    }

    public IEnumerable<Pair<Info, Node>> Successors(Node node)
    {
      Contract.Ensures(Contract.Result<IEnumerable<Pair<Info, Node>>>() != null);

      return null;
    }

    #endregion
  }

  public interface IGraphVisitor<Node, Info>
  {
    /// <summary>
    /// Should return true to continue visit, false to stop.
    /// </summary>
    bool VisitNode(Node node);
    void VisitEdge(Node source, Info info, Node target);
  }

  public interface IGraphBuilder<Node, Info> : IGraph<Node, Info>
  {
    void AddNode(Node n);
    void AddEdge(Node source, Info info, Node target);
  }

  public delegate Target DComposer<Original, Target>(Original node, Target accumulator);

  public class GraphWrapper<Node, Info> : IGraph<Node, Info>
  {
    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(nodes != null);
    }

    IEnumerable<Node>/*!*/ nodes;

    Converter<Node, IEnumerable<Pair<Info,Node>>> successors;

    public GraphWrapper(IEnumerable<Node>/*!*/ nodes,
                        Converter<Node, IEnumerable<Pair<Info,Node>>> successors
    )
    {
      Contract.Requires(nodes != null);// Clousot Suggestion

      this.nodes = nodes;
      this.successors = successors;
    }

    public IEnumerable<Node>/*!*/ Nodes { get { return this.nodes; } }

    public bool Contains(Node node) { return this.nodes.Contains(node); } 

    [ContractVerification(false)]
    public IEnumerable<Pair<Info,Node>> Successors(Node node) { return this.successors(node); }

  }
}