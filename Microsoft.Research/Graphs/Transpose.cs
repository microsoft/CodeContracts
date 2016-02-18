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
  public class Transpose
  {

    private class TransposedGraph<Node, EdgeInfo, OldEdgeInfo> : IGraph<Node, EdgeInfo>, IGraphVisitor<Node, OldEdgeInfo>
    {
      IGraph<Node, OldEdgeInfo>/*!*/ orig;

      public struct Edge
      {
        private readonly EdgeInfo edgeInfo;
        private readonly Node source;
        private readonly Node target;

        public Edge(Node source, EdgeInfo info, Node target)
        {
          this.source = source;
          this.edgeInfo = info;
          this.target = target;
        }

        #region IEdge<Node, EdgeInfo> Members

        public EdgeInfo Info
        {
          get { return edgeInfo; }
        }
        public Node Source { get { return this.source; } }
        public Node Target
        {
          get { return this.target; }
        }
        #endregion
      }

      [ContractInvariantMethod]
      private void ObjectInvariant()
      {
        Contract.Invariant(this.orig != null);
        Contract.Invariant(this.reverseEdges != null);
        Contract.Invariant(this.edgeinfomap != null);
      }


      Dictionary<Node, List<Edge>/*!*/>/*!*/ reverseEdges = new Dictionary<Node, List<Edge>/*!*/>();
      Converter<OldEdgeInfo, EdgeInfo>/*!*/ edgeinfomap;

      //^ [NotDelayed]
      public TransposedGraph(IGraph<Node, OldEdgeInfo>/*!*/ orig, Converter<OldEdgeInfo, EdgeInfo>/*!*/ edgeinfomap)
      {
        Contract.Requires(orig != null);// Clousot Suggestion
        Contract.Requires(edgeinfomap != null);// Clousot Suggestion


        this.orig = orig;
        this.edgeinfomap = edgeinfomap;
        //^ base();
        DepthFirst.Visitor<Node, OldEdgeInfo> v = new DepthFirst.Visitor<Node, OldEdgeInfo>(orig, this);
        // Visit all edges to compute the transpose
        v.VisitAll();
      }


      public IEnumerable<Node>/*!*/ Nodes
      {
        get { return orig.Nodes; }
      }

      public bool Contains(Node node) { return orig.Contains(node); }

      public IEnumerable<Pair<EdgeInfo, Node>> Successors(Node node)
      {
        List<Edge> edges = null;
        this.reverseEdges.TryGetValue(node, out edges);
        if (edges != null)
        {
          foreach (Edge edge in edges)
          {
            yield return new Pair<EdgeInfo,Node>(edge.Info, edge.Target);
          }
        }
        else
        {
          throw new ArgumentException("Node is not in graph");
        }
        yield break;
      }


      #region IGraphVisitor<Node,EdgeInfo> Members

      private void AddNode(Node node)
      {
        // just materialize it
        Ignore(Edges(node));
      }
      bool IGraphVisitor<Node, OldEdgeInfo>.VisitNode(Node node)
      {
        this.AddNode(node);
        return true;
      }

      private void Ignore(object o) { }

      [ContractVerification(false)]
      private List<Edge>/*!*/ Edges(Node node)
      {
        Contract.Ensures(Contract.Result<List<Edge>>() != null);

        List<Edge> edges;
        if (!this.reverseEdges.TryGetValue(node, out edges))
        {
          edges = new List<Edge>();
          this.reverseEdges[node] = edges;
        }
        else
        {
          Contract.Assume(edges != null);
        }
        //^ assert edges != null;
        return edges;
      }

      void IGraphVisitor<Node, OldEdgeInfo>.VisitEdge(Node source, OldEdgeInfo info, Node target)
      {
        // add reverse edge

        // make sure that original source is materialized (new target)
        this.AddNode(source);
        List<Edge> edges = Edges(target);
        edges.Add(new Edge(target, this.edgeinfomap(info), source));
      }

      #endregion
    }

    public static IGraph<Node, EdgeInfo>/*!*/ Build<Node, EdgeInfo>
        (
         IGraph<Node, EdgeInfo>/*!*/ graph
         )
    {
      Contract.Requires(graph != null);// Clousot Suggestion

      return new TransposedGraph<Node, EdgeInfo, EdgeInfo>(graph, delegate(EdgeInfo info) { return info; });
    }

    public static IGraph<Node, EdgeInfo>/*!*/ Build<Node, OldEdgeInfo, EdgeInfo>
        (
         IGraph<Node, OldEdgeInfo>/*!*/ graph,
         Converter<OldEdgeInfo, EdgeInfo>/*!*/ infomap
         )
    {
      Contract.Requires(graph != null);// Clousot Suggestion
      Contract.Requires(infomap != null);// Clousot Suggestion
      return new TransposedGraph<Node, EdgeInfo, OldEdgeInfo>(graph, infomap);
    }
  }
}