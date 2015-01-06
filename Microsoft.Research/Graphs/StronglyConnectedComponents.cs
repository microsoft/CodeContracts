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
using System.Diagnostics.Contracts;

namespace Microsoft.Research.Graphs
{
    public class StronglyConnectedComponents
    {
        public static List<List<Node>/*!*/>/*!*/ Compute<Node,EdgeInfo>(IGraph<Node, EdgeInfo>/*!*/ graph)
        {
          Contract.Requires(graph != null);// Clousot Suggestion

            // Step 1:  Call DFS(G) to compute finishing times f[u] for each vertex u
            //
            DepthFirst.Visitor<Node, EdgeInfo>/*!*/ dfs = 
                new DepthFirst.Visitor<Node, EdgeInfo>(graph);

            dfs.VisitAll();

            List<Node> nodesSortedByDecreasingFinishingTime =
                new List<Node>(graph.Nodes);
            // Sort in decreasing order of finish time
            nodesSortedByDecreasingFinishingTime.Sort(
                delegate(Node lhs, Node rhs)
                {
                    return dfs.DepthFirstInfo(rhs).FinishTime -
                           dfs.DepthFirstInfo(lhs).FinishTime;
                }
            );

            // Step 2:  Compute G^T
            // 
            IGraph<Node, EdgeInfo> transposed = Transpose.Build<Node, EdgeInfo>(graph);


            // Step 3:  Call DFS(G^T), but in the main loop of DFS, consider the vertices
            //          in order of decreasing f[u] (as computed in Step 1)
            //
            bool startNewGroup = true;
            List<List<Node>/*!*/>/*!*/ groups = new List<List<Node>/*!*/>();

            DepthFirst.Visitor<Node, EdgeInfo> transposedDFS = new DepthFirst.Visitor<Node, EdgeInfo>(transposed,
                delegate(Node n)
                {
                   List<Node> group;

                   if (startNewGroup) {
                       startNewGroup = false;
                       group = new List<Node>();
                       groups.Add(group);
                   }
                   else {
                       group = groups[groups.Count-1];
                   }
                   group.Add(n);
                   return true;
                });
            
            foreach (Node n in nodesSortedByDecreasingFinishingTime)
            {
                // debug
#if false
                Console.WriteLine("Node {0}, finish time {1}", n, dfs.DepthFirstInfo(n).FinishTime);
#endif
                transposedDFS.VisitSubGraph(n);
                startNewGroup = true;
            }

            return groups;
        }

        public static IDictionary<Node, Component> Compute<Node, EdgeInfo, Component>(
            IGraph<Node, EdgeInfo>/*!*/ graph,
            Converter<List<Node>, Component>/*!*/ createComponent)
        {
          Contract.Requires(graph != null);// Clousot Suggestion
          Contract.Requires(createComponent != null);

            List<List<Node>/*!*/> groups = Compute(graph);

            Dictionary<Node, Component> map = new Dictionary<Node, Component>();
            foreach (List<Node> group in groups)
            {
              Contract.Assume(group != null);
                Component c = createComponent(group);
                foreach (Node n in group)
                {
                    map[n] = c;
                }
            }
            return map;
        }
    }
}
