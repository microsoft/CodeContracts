// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

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
            private readonly Node _source;
            private readonly Node _target;
            private readonly Info _info;

            public Edge(Node source, Info info, Node target)
            {
                _source = source;
                _target = target;
                _info = info;
            }

            public Node Source
            {
                get { return _source; }
            }
            public Node Target
            {
                get { return _target; }
            }
            public Info Info { get { return _info; } }
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(_edgemap != null);
        }


        private readonly Dictionary<Node, Dictionary<Node, Edge>/*!*/>/*!*/ _edgemap;
        private readonly DComposer<Info, Info>/*!*/ _composer;
        private readonly IEqualityComparer<Node> _nodeComparer;

        private void Ignore(object ignore) { }

        public SingleEdgeGraph(DComposer<Info, Info> edgeComposer, IEqualityComparer<Node> nodeComparer = null)
        {
            _edgemap = new Dictionary<Node, Dictionary<Node, Edge>/*!*/>(nodeComparer);
            _composer = edgeComposer;
            _nodeComparer = nodeComparer;
        }

        public void AddNode(Node node)
        {
            // just materialize the node and edge list as a side
            // effect of calling Edges
            Ignore(Edges(node));
        }

        public void AddEdge(Node source, Info info, Node target)
        {
            Dictionary<Node, Edge> edges = Edges(source);
            Edge existing;
            if (edges.TryGetValue(target, out existing))
            {
                // compose info or ignore new edge
                if (_composer != null)
                {
                    Info composed = _composer(info, existing.Info);
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

            var result = new SingleEdgeGraph<Node, Info>(_composer, _nodeComparer);
            foreach (var pair in _edgemap)
            {
                Contract.Assume(pair.Value != null);
                foreach (var pair2 in pair.Value)
                {
                    result.AddEdge(pair2.Key, pair2.Value.Info, pair.Key);
                }
            }

            return result;
        }

        private Dictionary<Node, Edge>/*!*/ Edges(Node node)
        {
            Contract.Ensures(Contract.Result<Dictionary<Node, Edge>>() != null);

            Dictionary<Node, Edge> result;
            if (!_edgemap.TryGetValue(node, out result))
            {
                result = new Dictionary<Node, Edge>(_nodeComparer);
                _edgemap[node] = result;
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
            get { return _edgemap.Keys; }
        }

        public bool Contains(Node node)
        {
            return _edgemap.ContainsKey(node);
        }

#if false
        public IEnumerable<IEdge<Node, EdgeInfo>/*!*/>/*!*/ Successors(Node node)
        {
            Dictionary<Node, Edge> edges = null;
            this.edgemap.TryGetValue(node, out edges);
            if (edges != null)
            {
                Dictionary<Node, Edge>/*!*/ nnedges = edges;
                foreach (Node target in nnedges.Keys)
                {
                    yield return nnedges[target];
                }
            }
            else
            {
                throw new ArgumentException("Node is not in graph");
            }
            yield break;
        }
#endif
        #endregion


        #region IGraph<Node,Edge> Members


        public IEnumerable<Pair<Info, Node>> Successors(Node node)
        {
            Dictionary<Node, Edge> targets;
            if (_edgemap.TryGetValue(node, out targets))
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
