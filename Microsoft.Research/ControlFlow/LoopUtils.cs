// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using Microsoft.Research.CodeAnalysis;
using Microsoft.Research.DataStructures;
using Microsoft.Research.Graphs;
using System.Diagnostics.Contracts;

namespace Microsoft.Research.CodeAnalysis
{
    public static class LoopAnalysis
    {
        public static Dictionary<CFGBlock, List<CFGBlock>> ComputeContainingLoopMap(ICFG cfg)
        {
            Contract.Requires(cfg != null);
            Contract.Ensures(Contract.Result<Dictionary<CFGBlock, List<CFGBlock>>>() != null);

            var result = new Dictionary<CFGBlock, List<CFGBlock>>();
            var visitedSubroutines = new Set<int>();
            var pendingSubroutines = new Stack<Subroutine>();
            var pendingAPCs = new Stack<APC>();

            var graph = cfg.AsBackwardGraph(includeExceptionEdges: false, skipContracts: true);
            foreach (var loophead in cfg.LoopHeads)
            {
                // push back-edge sources
                var loopPC = new APC(loophead, 0, null);
                foreach (var pred in cfg.Predecessors(loopPC))
                {
                    if (cfg.IsForwardBackEdge(pred, loopPC))
                    {
                        var normalizedPred = new APC(pred.Block, 0, null);
                        pendingAPCs.Push(normalizedPred);
                    }
                }
                var visit = new DepthFirst.Visitor<APC, Unit>(graph,
                  (APC pc) =>
                  {
                      if (pc.SubroutineContext != null)
                      {
                          // push continuation PC
                          pendingAPCs.Push(new APC(pc.SubroutineContext.Head.One, 0, null));
                          if (visitedSubroutines.AddQ(pc.Block.Subroutine.Id))
                          {
                              pendingSubroutines.Push(pc.Block.Subroutine);
                          }
                          return false; // stop exploration
                      }
                      return !pc.Equals(loopPC);
                  });

                while (pendingAPCs.Count > 0)
                {
                    var root = pendingAPCs.Pop();
                    visit.VisitSubGraphNonRecursive(root);

                    while (pendingSubroutines.Count > 0)
                    {
                        var sub = pendingSubroutines.Pop();
                        pendingAPCs.Push(new APC(sub.Exit, 0, null));
                    }
                }

                foreach (var visited in visit.Visited)
                {
                    if (visited.SubroutineContext != null) continue; // ignore non-primary pcs
                    MaterializeContainingLoop(result, visited.Block).AssumeNotNull().Add(loophead);
                }
            }

            return result;
        }

        private static List<CFGBlock> MaterializeContainingLoop(Dictionary<CFGBlock, List<CFGBlock>> map, CFGBlock block)
        {
            Contract.Requires(map != null);// F: Added as of Clousot suggestion

            List<CFGBlock> result;
            if (!map.TryGetValue(block, out result))
            {
                result = new List<CFGBlock>();
                map.Add(block, result);
            }
            return result;
        }
    }
}
