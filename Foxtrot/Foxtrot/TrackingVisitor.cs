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
using System.Collections.ObjectModel;
using System.Text;
using System.Compiler;
using System.Threading;

namespace Microsoft.Contracts.Foxtrot
{
    /// <summary>
    /// Keeps a history (stack) of each node visited during visitation, even across TrackingVisitors.
    /// </summary>
    public class TrackingVisitor : StandardVisitor
    {
        private ListStack<Node> visitedNodes;
        private ListStack<SourceContext> visitedContexts;

        public override Node Visit(Node node)
        {
            if (node == null)
                return null;

            LocalDataStoreSlot slot = Thread.GetNamedDataSlot("TrackingVisitor");
            TrackingVisitor parent = Thread.GetData(slot) as TrackingVisitor;
            if (parent == null)
            {
                Thread.SetData(slot, this);
                this.visitedNodes = new ListStack<Node>();
                this.visitedContexts = new ListStack<SourceContext>();
            }
            else if (parent != this)
            {
                this.visitedNodes = parent.visitedNodes;
                this.visitedContexts = parent.visitedContexts;
            }

            this.PushNode(node);

            node = base.Visit(node);

            this.PopNode();

            if (parent == null)
                Thread.SetData(slot, null);

            return node;
        }

        public IList<Node> VisitedNodes
        {
            get
            {
                return this.visitedNodes;
            }
        }

        public IList<SourceContext> VisitedContexts
        {
            get
            {
                return this.visitedContexts;
            }
        }

        public virtual SourceContext GetLastContext()
        {
            if (this.visitedContexts.Count > 0)
                return this.visitedContexts[0];

            Document document = new Document();
            document.Text = new DocumentText("");
            foreach (Node node in this.visitedNodes)
            {
                document.Name = node.ToString();
                if (!(document.Name == node.GetType().ToString()))
                    break;
            }
            document.Name = "[" + document.Name + "]";

            SourceContext context = new SourceContext(document);
            return context;
        }

        public T GetLastNode<T>() where T : Node
        {
            foreach (Node node in this.visitedNodes)
            {
                T t = node as T;
                if (t != null)
                    return t;
            }

            return null;
        }

        protected void PushNode(Node node)
        {
            this.visitedNodes.Push(node);
            if (node.SourceContext.Document != null)
                this.visitedContexts.Push(node.SourceContext);
        }

        protected Node PopNode()
        {
            Node node = this.visitedNodes.Pop();
            if (node.SourceContext.Document != null)
                this.visitedContexts.Pop();
            return node;
        }

        private class ListStack<T> : Stack<T>, IList<T>
        {
            public int IndexOf(T item)
            {
                int index = 0;
                foreach (T t in this)
                    if (Object.Equals(t, item))
                        return index++;
                return -1;
            }

            public void Insert(int index, T item)
            {
                throw new NotSupportedException("Collection is read-only.");
            }

            public void RemoveAt(int index)
            {
                throw new NotSupportedException("Collection is read-only.");
            }

            public T this[int index]
            {
                get
                {
                    foreach (T t in this)
                        if (index-- == 0)
                            return t;

                    throw new IndexOutOfRangeException();
                }
                set
                {
                    throw new NotSupportedException("Collection is read-only.");
                }
            }

            public void Add(T item)
            {
                throw new NotSupportedException("Collection is read-only.");
            }

            public bool IsReadOnly
            {
                get
                {
                    return true;
                }
            }

            public bool Remove(T item)
            {
                throw new NotSupportedException("Collection is read-only.");
            }
        }
    }
}
