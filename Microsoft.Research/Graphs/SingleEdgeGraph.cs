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
using System.Text;
using Microsoft.Research.DataStructures;
using System.Diagnostics.Contracts;

namespace Microsoft.Research.Graphs
{
  /// <summary>
  /// A specialized graph based on a successor adjacency list.
  ///
  /// This graph maintains only a single distinct edges between a pair of nodes.
  /// It uses a composer provided on construction to compose edge information if 
  /// multiple edges are added between the same node.
  /// </summary>
  public class SingleEdgeGraph<Node, Info> : IGraphBuilder<Node, Info>
  {
    // invariant: if there exists an edge n1 -> n2, then n2 is in the domain of the edgemap
    // this is important as we use that domain to characterize the set of nodes in our graph.
    // and some nodes may not have outgoing edges.

    private struct Edge
    {
      private readonly Node source;
      private readonly Node target;
      private readonly Info info;

      public Edge(Node source, Info info, Node target)
      {
        this.source = source;
        this.target = target;
        this.info = info;
      }

      public Node Source
      {
        get { return this.source; }
      }
      public Node Target
      {
        get { return this.target; }
      }
      public Info Info { get { return this.info; } }
    }

    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(this.edgemap != null);
    }


    private readonly Dictionary<Node, Dictionary<Node, Edge>/*!*/>/*!*/ edgemap;
    private readonly DComposer<Info, Info>/*!*/ composer;
    private readonly IEqualityComparer<Node> nodeComparer;

    private void Ignore(object ignore) { }

    public SingleEdgeGraph(DComposer<Info, Info> edgeComposer, IEqualityComparer<Node> nodeComparer = null)
    {
      this.edgemap = new Dictionary<Node, Dictionary<Node, Edge>/*!*/>(nodeComparer);
      this.composer = edgeComposer;
      this.nodeComparer = nodeComparer;
    }

    public void AddNode(Node node)
    {
      // just materialize the node and edge list as a side
      // effect of calling Edges
      Ignore(Edges(node));
    }

    [ContractVerification(false)]
    public void AddEdge(Node source, Info info, Node target)
    {
      Dictionary<Node, Edge> edges = Edges(source);
      Edge existing;
      if (edges.TryGetValue(target, out existing))
      {
        // compose info or ignore new edge
        if (this.composer != null)
        {
          Info composed = this.composer(info, existing.Info);
          edges[target] = new Edge(source, composed, target);
        }
      }
      else
      {
        Edge edge = new Edge(source, info, target);
        edges[target] = edge;
        // Make sure target node is in graph
        this.AddNode(target);
      }
    }

    public SingleEdgeGraph<Node, Info> Reverse()
    {
      Contract.Ensures(Contract.Result<SingleEdgeGraph<Node, Info>>() != null);

      var result = new SingleEdgeGraph<Node, Info>(this.composer, this.nodeComparer);
      foreach (var pair in this.edgemap)
      {
        Contract.Assume(pair.Value != null);
        foreach (var pair2 in pair.Value)
        {
          result.AddEdge(pair2.Key, pair2.Value.Info, pair.Key);
        }
      }

      return result;
    }

    [ContractVerification(false)]
    private Dictionary<Node, Edge>/*!*/ Edges(Node node)
    {
      Contract.Ensures(Contract.Result<Dictionary<Node, Edge>>() != null);

      Dictionary<Node, Edge> result;
      if (!this.edgemap.TryGetValue(node, out result))
      {
        result = new Dictionary<Node, Edge>(nodeComparer);
        this.edgemap[node] = result;
      }
      else
      {
        Contract.Assume(result != null);
      }

      //^ assert result != null;
      return result;
    }

    #region IGraph<Node,Edge> Members

    public IEnumerable<Node>/*!*/ Nodes
    {
      get { return this.edgemap.Keys; }
    }

    [ContractVerification(false)]
    public bool Contains(Node node)
    {
      return this.edgemap.ContainsKey(node);
    }
    #endregion


    #region IGraph<Node,Edge> Members


    public IEnumerable<Pair<Info, Node>> Successors(Node node)
    {
      Dictionary<Node, Edge> targets;
      if (this.edgemap.TryGetValue(node, out targets))
      {
        foreach (Edge edge in targets.Values)
        {
          yield return new Pair<Info, Node>(edge.Info, edge.Target);
        }
      }
      else
      {
        var n = node;
      }
    }

    #endregion
  }

}
