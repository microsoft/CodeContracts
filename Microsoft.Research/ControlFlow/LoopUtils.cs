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

      var graph = cfg.AsBackwardGraph(includeExceptionEdges:false, skipContracts:true);
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

    static List<CFGBlock> MaterializeContainingLoop(Dictionary<CFGBlock, List<CFGBlock>> map, CFGBlock block)
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
