// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Research.DataStructures;
using System.Diagnostics.Contracts;


namespace Microsoft.Research.Graphs
{
    public delegate void EdgeVisitor<Node, Info>(Node source, Info info, Node target);

    public static class DepthFirst
    {
        public static void Visit<Node, EdgeInfo>
            (
             IGraph<Node, EdgeInfo>/*!*/ graph,
             Predicate<Node> nodeStartVisitor,
             EdgeVisitor<Node, EdgeInfo> edgeVisitor
             )
        {
            Contract.Requires(graph != null);// Clousot Suggestion

            Visitor<Node, EdgeInfo> dfs = new Visitor<Node, EdgeInfo>(graph, nodeStartVisitor, null, edgeVisitor);
            dfs.VisitAll();
        }

        public static void Visit<Node, EdgeInfo>
          (
           IGraph<Node, EdgeInfo>/*!*/ graph,
           Node startNode,
           Predicate<Node> nodeStartVisitor,
           EdgeVisitor<Node, EdgeInfo> edgeVisitor
           )
        {
            Contract.Requires(graph != null);// Clousot Suggestion
            Visitor<Node, EdgeInfo> dfs = new Visitor<Node, EdgeInfo>(graph, nodeStartVisitor, null, edgeVisitor);
            dfs.VisitSubGraphNonRecursive(startNode);
        }

        public class Info<Node>
        {
            internal Info(Node parent, int starttime)
            {
                this.Parent = parent;
                this.StartTime = starttime;
            }

            public readonly Node Parent;
            public readonly int StartTime;
            public int FinishTime;
            public bool TargetOfBackEdge;
            public bool SourceOfBackEdge;
        }

        public class Visitor<Node, Edge>
        {
            private struct SearchFrame
            {
                public readonly Node Node;
                public readonly IEnumerator<Pair<Edge, Node>> Edges;
                public readonly Info<Node> Info;

                public SearchFrame(Node node, IEnumerator<Pair<Edge, Node>> edges, Info<Node> info)
                {
                    this.Node = node;
                    this.Edges = edges;
                    this.Info = info;
                }
            }

            [ContractInvariantMethod]
            private void ObjectInvariant()
            {
                Contract.Invariant(_history != null);
                Contract.Invariant(_graph != null);
                Contract.Invariant(_backEdges != null);
                Contract.Invariant(_todo != null);
            }


            private int _time = 0;

            private readonly IGraph<Node, Edge>/*!*/ _graph;
            private readonly Predicate<Node>/*?*/ _nodeStartVisitor;
            private readonly Action<Node>/*?*/ _nodeFinishVisitor;
            private readonly EdgeVisitor<Node, Edge>/*?*/ _edgeVisitor;

            private readonly Dictionary<Node, Info<Node>/*!*/>/*!*/ _history = new Dictionary<Node, Info<Node>/*!*/>();
            private readonly Set<STuple<Node, Edge, Node>>/*!*/ _backEdges = new Set<STuple<Node, Edge, Node>>();
            private readonly Stack<SearchFrame> _todo = new Stack<SearchFrame>();

            public Visitor(IGraph<Node, Edge>/*!*/ graph)
              : this(graph, null, null, null)
            {
                Contract.Requires(graph != null);// Clousot Suggestion
            }

            public Visitor(IGraph<Node, Edge>/*!*/ graph,
                           //^ [Delayed]
                           Predicate<Node> nodeStartVisitor)
              : this(graph, nodeStartVisitor, null, null)
            {
                Contract.Requires(graph != null);// Clousot Suggestion
            }

            public Visitor(IGraph<Node, Edge>/*!*/ graph,
                           //^ [Delayed]
                           Predicate<Node>/*?*/ nodeStartVisitor,
                           //^ [Delayed]
                           Action<Node>/*?*/ nodeFinishVisitor,
                           //^ [Delayed]
                           EdgeVisitor<Node, Edge>/*?*/ edgeVisitor)
            {
                Contract.Requires(graph != null);// Clousot Suggestion

                _graph = graph;
                _nodeStartVisitor = nodeStartVisitor;
                _nodeFinishVisitor = nodeFinishVisitor;
                _edgeVisitor = edgeVisitor;
            }

            public Visitor(IGraph<Node, Edge>/*!*/ graph,
                           //^ [Delayed]
                           IGraphVisitor<Node, Edge>/*!*/ visitor)
              : this(graph, visitor.VisitNode, null, visitor.VisitEdge)
            {
                Contract.Requires(graph != null); // Clousot Suggestion
                Contract.Requires(visitor != null);// Clousot Suggestion
            }

            public virtual void VisitAll()
            {
                foreach (Node node in _graph.Nodes)
                {
                    VisitSubGraphNonRecursive(node);
                }
            }

            internal virtual void VisitEdge(Info<Node> sourceInfo, Node source, Edge info, Node targetNode)
            {
                Contract.Requires(sourceInfo != null);

                if (_edgeVisitor != null)
                {
                    _edgeVisitor(source, info, targetNode);
                }

                Info<Node>/*?*/ targetInfo;
                if (_history.TryGetValue(targetNode, out targetInfo))
                {
                    Contract.Assume(targetInfo != null);
                    //^ assume targetInfo != null;
                    if (targetInfo.FinishTime == 0)
                    {
                        // on stack, means it's a back edge
                        targetInfo.TargetOfBackEdge = true;
                        sourceInfo.SourceOfBackEdge = true;
                        _backEdges.Add(new STuple<Node, Edge, Node>(source, info, targetNode));
                    }
                    // no need to visit target node
                    return;
                }
                VisitSubGraph(targetNode, source);
            }

            /// <summary>
            /// Non-recursive version
            /// </summary>
            internal virtual void VisitEdgeNonRecursive(Info<Node> sourceInfo, Node source, Edge info, Node targetNode)
            {
                Contract.Requires(sourceInfo != null);

                if (_edgeVisitor != null)
                {
                    _edgeVisitor(source, info, targetNode);
                }

                Info<Node>/*?*/ targetInfo;
                if (_history.TryGetValue(targetNode, out targetInfo))
                {
                    Contract.Assume(targetInfo != null);
                    //^ assume targetInfo != null;
                    if (targetInfo.FinishTime == 0)
                    {
                        // on stack, means it's a back edge
                        targetInfo.TargetOfBackEdge = true;
                        sourceInfo.SourceOfBackEdge = true;
                        _backEdges.Add(new STuple<Node, Edge, Node>(source, info, targetNode));
                    }
                    // no need to visit target node
                    return;
                }
                ScheduleNode(targetNode, source);
            }

            public virtual void VisitSubGraph(Node node)
            {
                VisitSubGraph(node, default(Node));
            }

            public virtual void VisitSubGraph(Node node, Node parent)
            {
                if (_history.ContainsKey(node)) return;

                var info = new Info<Node>(parent, ++_time);
                _history[node] = info;

                if (_nodeStartVisitor != null)
                {
                    var cont = _nodeStartVisitor(node);
                    if (!cont) return;
                }

                this.VisitSuccessors(info, node);

                if (_nodeFinishVisitor != null) _nodeFinishVisitor(node);

                info.FinishTime = ++_time;
            }

            public virtual void VisitSubGraphNonRecursive(Node node, Node parent)
            {
                ScheduleNode(node, parent);
                IterativeDFS();
            }

            public virtual void VisitSubGraphNonRecursive(Node node)
            {
                ScheduleNode(node, default(Node));
                IterativeDFS();
            }

            /// <summary>
            /// Non-recursive version
            /// </summary>
            public virtual void ScheduleNode(Node node, Node parent)
            {
                if (_history.ContainsKey(node)) return;

                var info = new Info<Node>(parent, ++_time);
                _history[node] = info;

                if (_nodeStartVisitor != null)
                {
                    var cont = _nodeStartVisitor(node);
                    if (!cont) return;
                }

                _todo.Push(new SearchFrame(node, _graph.Successors(node).GetEnumerator(), info));
            }

            internal virtual void VisitSuccessors(Info<Node> info, Node node)
            {
                Contract.Requires(info != null);

                foreach (Pair<Edge, Node> edge in _graph.Successors(node))
                {
                    this.VisitEdge(info, node, edge.One, edge.Two);
                }
            }

            /// <summary>
            /// Drains the todo list
            /// </summary>
            private void IterativeDFS()
            {
                while (_todo.Count > 0)
                {
                    SearchFrame sf = _todo.Peek();
                    Contract.Assume(sf.Edges != null);

                    if (sf.Edges.MoveNext())
                    {
                        var edgeTarget = sf.Edges.Current;

                        Contract.Assume(sf.Info != null);

                        this.VisitEdgeNonRecursive(sf.Info, sf.Node, edgeTarget.One, edgeTarget.Two);
                        continue; // make sure to visit new edges first.
                    }
                    // done with this frame.
                    if (_nodeFinishVisitor != null) _nodeFinishVisitor(sf.Node);

                    Contract.Assume(sf.Info != null);
                    sf.Info.FinishTime = ++_time;
                    _todo.Pop();
                }
            }

            public IEnumerable<Node> Visited
            {
                get
                {
                    Contract.Ensures(Contract.Result<IEnumerable<Node>>() != null);

                    return _history.Keys;
                }
            }

            public bool IsVisited(Node node)
            {
                return _history.ContainsKey(node);
            }

            public Info<Node>/*!*/ DepthFirstInfo(Node node)
            {
                Contract.Ensures(Contract.Result<Info<Node>>() != null);

                return _history[node].AssumeNotNull();
            }

            public bool IsBackEdge(Node source, Edge info, Node target)
            {
                return _backEdges.Contains(new STuple<Node, Edge, Node>(source, info, target));
            }
        }
    }
}