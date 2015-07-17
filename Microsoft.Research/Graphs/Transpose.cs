// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

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
            private IGraph<Node, OldEdgeInfo>/*!*/ _orig;

            public struct Edge
            {
                private readonly EdgeInfo _edgeInfo;
                private readonly Node _source;
                private readonly Node _target;

                public Edge(Node source, EdgeInfo info, Node target)
                {
                    _source = source;
                    _edgeInfo = info;
                    _target = target;
                }

                #region IEdge<Node, EdgeInfo> Members

                public EdgeInfo Info
                {
                    get { return _edgeInfo; }
                }
                public Node Source { get { return _source; } }
                public Node Target
                {
                    get { return _target; }
                }
                #endregion
            }

            [ContractInvariantMethod]
            private void ObjectInvariant()
            {
                Contract.Invariant(_orig != null);
                Contract.Invariant(_reverseEdges != null);
                Contract.Invariant(_edgeinfomap != null);
            }


            private Dictionary<Node, List<Edge>/*!*/>/*!*/ _reverseEdges = new Dictionary<Node, List<Edge>/*!*/>();
            private Converter<OldEdgeInfo, EdgeInfo>/*!*/ _edgeinfomap;

            //^ [NotDelayed]
            public TransposedGraph(IGraph<Node, OldEdgeInfo>/*!*/ orig, Converter<OldEdgeInfo, EdgeInfo>/*!*/ edgeinfomap)
            {
                Contract.Requires(orig != null);// Clousot Suggestion
                Contract.Requires(edgeinfomap != null);// Clousot Suggestion


                _orig = orig;
                _edgeinfomap = edgeinfomap;
                //^ base();
                DepthFirst.Visitor<Node, OldEdgeInfo> v = new DepthFirst.Visitor<Node, OldEdgeInfo>(orig, this);
                // Visit all edges to compute the transpose
                v.VisitAll();

                // debug
#if false
                foreach (Node n in this.orig.Nodes)
                {
                    DepthFirst.Info info = v.DepthFirstInfo(n);
                    Console.WriteLine("Depthfirst info: {0}: Start:{1}, Finish:{2}", n, info.StartTime, info.FinishTime);
                }
#endif
            }


            public IEnumerable<Node>/*!*/ Nodes
            {
                get { return _orig.Nodes; }
            }

            public bool Contains(Node node) { return _orig.Contains(node); }

            public IEnumerable<Pair<EdgeInfo, Node>> Successors(Node node)
            {
                List<Edge> edges = null;
                _reverseEdges.TryGetValue(node, out edges);
                if (edges != null)
                {
                    foreach (Edge edge in edges)
                    {
                        yield return new Pair<EdgeInfo, Node>(edge.Info, edge.Target);
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

            private List<Edge>/*!*/ Edges(Node node)
            {
                Contract.Ensures(Contract.Result<List<Edge>>() != null);

                List<Edge> edges;
                if (!_reverseEdges.TryGetValue(node, out edges))
                {
                    edges = new List<Edge>();
                    _reverseEdges[node] = edges;
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
                edges.Add(new Edge(target, _edgeinfomap(info), source));
            }

            #endregion
        }

        public static IGraph<Node, EdgeInfo>/*!*/ Build<Node, EdgeInfo>
            (
             IGraph<Node, EdgeInfo>/*!*/ graph
             )
        {
            Contract.Requires(graph != null);// Clousot Suggestion

            return new TransposedGraph<Node, EdgeInfo, EdgeInfo>(graph, delegate (EdgeInfo info) { return info; });
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