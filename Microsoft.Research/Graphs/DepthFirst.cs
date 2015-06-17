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
      struct SearchFrame
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
        Contract.Invariant(this.history != null);
        Contract.Invariant(this.graph != null);
        Contract.Invariant(this.backEdges != null);
        Contract.Invariant(this.todo != null);
      }


      private int time = 0;

      private readonly IGraph<Node, Edge>/*!*/ graph;
      private readonly Predicate<Node>/*?*/ nodeStartVisitor;
      private readonly Action<Node>/*?*/ nodeFinishVisitor;
      private readonly EdgeVisitor<Node, Edge>/*?*/ edgeVisitor;

      private readonly Dictionary<Node, Info<Node>/*!*/>/*!*/ history = new Dictionary<Node, Info<Node>/*!*/>();
      private readonly Set<STuple<Node, Edge, Node>>/*!*/ backEdges = new Set<STuple<Node, Edge, Node>>();
      private readonly Stack<SearchFrame> todo = new Stack<SearchFrame>();

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

        this.graph = graph;
        this.nodeStartVisitor = nodeStartVisitor;
        this.nodeFinishVisitor = nodeFinishVisitor;
        this.edgeVisitor = edgeVisitor;
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
        foreach (Node node in graph.Nodes)
        {
          VisitSubGraphNonRecursive(node);
        }
      }

      internal virtual void VisitEdge(Info<Node> sourceInfo, Node source, Edge info, Node targetNode)
      {
        Contract.Requires(sourceInfo != null);

        if (this.edgeVisitor != null)
        {
          this.edgeVisitor(source, info, targetNode);
        }

        Info<Node>/*?*/ targetInfo;
        if (history.TryGetValue(targetNode, out targetInfo))
        {
          Contract.Assume(targetInfo != null);
          //^ assume targetInfo != null;
          if (targetInfo.FinishTime == 0)
          {
            // on stack, means it's a back edge
            targetInfo.TargetOfBackEdge = true;
            sourceInfo.SourceOfBackEdge = true;
            this.backEdges.Add(new STuple<Node, Edge, Node>(source, info, targetNode));
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

        if (this.edgeVisitor != null)
        {
          this.edgeVisitor(source, info, targetNode);
        }

        Info<Node>/*?*/ targetInfo;
        if (history.TryGetValue(targetNode, out targetInfo))
        {
          Contract.Assume(targetInfo != null);
          //^ assume targetInfo != null;
          if (targetInfo.FinishTime == 0)
          {
            // on stack, means it's a back edge
            targetInfo.TargetOfBackEdge = true;
            sourceInfo.SourceOfBackEdge = true;
            this.backEdges.Add(new STuple<Node, Edge, Node>(source, info, targetNode));
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
        if (history.ContainsKey(node)) return;

        var info = new Info<Node>(parent, ++time);
        history[node] = info;

        if (nodeStartVisitor != null)
        {
          var cont = nodeStartVisitor(node);
          if (!cont) return;
        }

        this.VisitSuccessors(info, node);

        if (nodeFinishVisitor != null) nodeFinishVisitor(node);

        info.FinishTime = ++time;
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
        if (history.ContainsKey(node)) return;

        var info = new Info<Node>(parent, ++time);
        history[node] = info;

        if (nodeStartVisitor != null)
        {
          var cont = nodeStartVisitor(node);
          if (!cont) return;
        }

        todo.Push(new SearchFrame(node, this.graph.Successors(node).GetEnumerator(), info));
      }

      internal virtual void VisitSuccessors(Info<Node> info, Node node)
      {
        Contract.Requires(info != null);

        foreach (Pair<Edge, Node> edge in graph.Successors(node))
        {
          this.VisitEdge(info, node, edge.One, edge.Two);
        }
      }

      /// <summary>
      /// Drains the todo list
      /// </summary>
      private void IterativeDFS()
      {
        while (todo.Count > 0)
        {
          SearchFrame sf = todo.Peek();
          Contract.Assume(sf.Edges != null);

          if (sf.Edges.MoveNext())
          {
            var edgeTarget = sf.Edges.Current;

            Contract.Assume(sf.Info != null);

            this.VisitEdgeNonRecursive(sf.Info, sf.Node, edgeTarget.One, edgeTarget.Two);
            continue; // make sure to visit new edges first.
          }
          // done with this frame.
          if (nodeFinishVisitor != null) nodeFinishVisitor(sf.Node);

          Contract.Assume(sf.Info != null);
          sf.Info.FinishTime = ++time;
          todo.Pop();
        }
      }

      public IEnumerable<Node> Visited
      {
        get {
          Contract.Ensures(Contract.Result<IEnumerable<Node>>() != null);

          return this.history.Keys; 
        }
      }

      public bool IsVisited(Node node)
      {
        return this.history.ContainsKey(node);
      }

      public Info<Node>/*!*/ DepthFirstInfo(Node node)
      {
        Contract.Ensures(Contract.Result<Info<Node>>() != null);

        return this.history[node].AssumeNotNull();
      }

      public bool IsBackEdge(Node source, Edge info, Node target)
      {
        return this.backEdges.Contains(new STuple<Node, Edge, Node>(source, info, target));
      }
    }

  }
}