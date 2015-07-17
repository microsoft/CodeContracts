// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

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
        IEnumerable<Pair<Info, Node>> Successors(Node node);
    }

    [ContractClassFor(typeof(IGraph<,>))]
    internal abstract class IGraphContracts<Node, Info> : IGraph<Node, Info>
    {
        #region IGraph<Node,Info> Members

        public bool Contains(Node node)
        {
            return default(bool);
        }

        public IEnumerable<Node> Nodes
        {
            get
            {
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
            Contract.Invariant(_nodes != null);
        }

        private IEnumerable<Node>/*!*/ _nodes;

        private Converter<Node, IEnumerable<Pair<Info, Node>>> _successors;

        public GraphWrapper(IEnumerable<Node>/*!*/ nodes,
                            Converter<Node, IEnumerable<Pair<Info, Node>>> successors
        )
        {
            Contract.Requires(nodes != null);// Clousot Suggestion

            _nodes = nodes;
            _successors = successors;
        }

        public IEnumerable<Node>/*!*/ Nodes { get { return _nodes; } }

        public bool Contains(Node node) { return _nodes.Contains(node); }

        [ContractVerification(false)]
        public IEnumerable<Pair<Info, Node>> Successors(Node node) { return _successors(node); }
    }
}