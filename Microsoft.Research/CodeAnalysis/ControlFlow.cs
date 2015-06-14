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

// #define ENABLE_ASSERTIONS
//#define CHECK_RECURSIVE_APCS

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.IO;

using Tag = System.String;
using UnitSource = Microsoft.Research.DataStructures.Unit;
using UnitDest = Microsoft.Research.DataStructures.Unit;
using Microsoft.Research.DataStructures;
using Microsoft.Research.Graphs;


namespace Microsoft.Research.CodeAnalysis
{
  using SubroutineEdge = Tuple<CFGBlock, CFGBlock, string>;
  using SubroutineContext = FList<Tuple<CFGBlock, CFGBlock, string>>;

  [ContextAdapter]
  internal interface IStackInfo
  {
    /// <summary>
    /// Return true if the pc is a call site and the call is on the current "this" object of the method
    /// </summary>
    bool IsCallOnThis(APC pc);
  }

  public static class APCExtensions
  {
    public static string ExtractAssertionCondition(this APC pc)
    {
      return pc.Block.SourceAssertionCondition(pc);
    }

    public static APC PrimaryMethodLocation(this APC candidate)
    {
      if (candidate.Block.Subroutine.IsMethod || candidate.Block.Subroutine.IsFaultFinally) return candidate;
      bool isRequiresContext = candidate.Block.Subroutine.IsRequires;

      for (var context = candidate.SubroutineContext; context != null; context = context.Tail)
      {
        if (context.Head.One.Subroutine.IsMethod || context.Head.One.Subroutine.IsFaultFinally)
        {
          APC methodContext;
          if (isRequiresContext)
          {
            // use target of edge in case of subroutine context
            CFGBlock contextBlock = context.Head.Two;
            methodContext = contextBlock.First;
          }
          else
          {
            CFGBlock contextBlock = context.Head.One;
            methodContext = contextBlock.PriorLast;
          }
          return methodContext;
        }
      }
      return candidate; // don't know better. There should always be a method
    }

    public static IEnumerable<APC> NonPrimaryRelatedLocations(this APC candidate)
    {
      bool skippedMethodPrimary = false;
      if (candidate.Block.Subroutine.IsContract)
      {
        // that is the first related one
        yield return candidate;
      }
      else
      {
        Debug.Assert(candidate.Block.Subroutine.IsMethod || candidate.Block.Subroutine.IsFaultFinally);
        skippedMethodPrimary = true;
      }

      for (var subroutineContext = candidate.SubroutineContext; subroutineContext != null; subroutineContext=subroutineContext.Tail)
      {
        if (!skippedMethodPrimary && (subroutineContext.Head.One.Subroutine.IsMethod || subroutineContext.Head.One.Subroutine.IsFaultFinally))
        {
          skippedMethodPrimary = true;
          continue;
        }

        CFGBlock contextBlock = subroutineContext.Head.One;
        yield return contextBlock.First;
      }
    }

  }


  /// <summary>
  /// An abstract program point representing a particular control point including possible finally continuations
  ///
  /// The point is an index into a CFGBlock. If the block has a label at that index, the point represents the program
  /// point just PRIOR to the execution of the code at that label. 
  /// If the point is past the sequence of labels in that CFGBlock, then the program point represents the excecution
  /// point just after the last instruction in the block (or predecessor blocks).
  /// </summary>
  public class APC : IEquatable<APC> {

    public static readonly APC Dummy = new APC(null, 0, null);
    /// <summary>
    /// The logical block containing this abstract PC
    /// </summary>
    public readonly CFGBlock Block;

    /// <summary>
    /// The index within the block of this abstract PC
    /// </summary>
    public readonly int Index;

    /// <summary>
    /// Fault/Finally edges. (from,to) that are currently being traversed.
    /// The current block/index is executing on some handler of the first edge in the
    /// finally edges. If the current handler ends(endfinally), the next handler on the
    /// edge is retrieved and made the current program point. If no more handlers are on the
    /// edge, the edge is popped and the target of the edge becomes the new current point.
    /// NOTE: if the edge targets a catch/filter handler entry point, then fault handlers
    /// are visited, otherwise only finally handlers.
    /// </summary>
    public readonly SubroutineContext/*?*/ SubroutineContext;

    /// <summary>
    /// Construct APC
    /// </summary>
    public APC(CFGBlock block, int index, 
               SubroutineContext/*?*/ subroutineContext) {
      this.Block = block;
      this.Index = index;
      this.SubroutineContext = subroutineContext;
#if CHECK_RECURSIVE_APCS
      for (; subroutineContext != null; subroutineContext = subroutineContext.Tail)
      {
        Debug.Assert(block.Subroutine != subroutineContext.Head.One.Subroutine);
      }
#endif
    }


    public APC Post()
    {
      if (this.Index < this.Block.Count)
      {
        return new APC(this.Block, this.Index + 1, this.SubroutineContext);
      }
      return this;
    }

    public static APC ForEnd(CFGBlock block,
                             SubroutineContext subroutineContext) {
      return new APC(block, block.Count, subroutineContext);
    }

    public string AlternateSourceContext()
    {
      return String.Format("{0}[0x{1:x}]", this.Block.Subroutine.Name, this.Block.ILOffset(this));
    }

    public bool HasRealSourceContext
    {
      get
      {
        string realSC = this.Block.SourceContext(this);
        return realSC != null;
      }
    }
    public string PrimarySourceContext()
    {
      string realSC = this.Block.SourceContext(this);
      if (realSC != null) return realSC;
      return AlternateSourceContext();
    }


    public int ILOffset
    {
      get
      {
        return this.Block.ILOffset(this);
      }
    }
    /// <summary>
    /// Copy constructor that removes subroutine context
    /// </summary>
    private APC(APC from)
    {
      this.Index = from.Index;
      this.Block = from.Block;
      this.SubroutineContext = null;
    }

    public APC WithoutContext
    {
      get
      {
        return new APC(this);
      }
    }

    public static void ToString(StringBuilder sb, SubroutineContext finallyContext)
    {
      bool seenFirst = false;
      while (finallyContext != null)
      {
        if (!seenFirst) { sb.Append(" {"); seenFirst = true; } else { sb.Append(", "); }
        sb.AppendFormat("(SR{2} {0},{1}) [{3}]", finallyContext.Head.One.Index, finallyContext.Head.Two.Index, finallyContext.Head.One.Subroutine.Id, finallyContext.Head.Three);
        finallyContext = finallyContext.Tail;
      }
      if (seenFirst)
      {
        sb.Append("}");
      }
    }

    public override string ToString()
    {
      StringBuilder sb = new StringBuilder();
      sb.Append("[");
      sb.AppendFormat("SR{2} {0},{1}", this.Block.Index, this.Index, this.Block.Subroutine.Id);
      ToString(sb, this.SubroutineContext);
      sb.Append("]");
      return sb.ToString();
    }


    #region IEquatable<APC> Members

    //^ [StateIndependent]
    public bool Equals(APC that)
    {
      if (this.Block != that.Block) return false;
      if (this.Index != that.Index) return false;
      if (this.SubroutineContext != that.SubroutineContext) return false;
      return true;
    }

    #endregion

    public bool InsideRequiresAtCallInsideContract
    {
      get
      {
        if (this.Block.Subroutine.IsRequires)
        {
          if (this.SubroutineContext != null)
          {
            for (SubroutineContext context = this.SubroutineContext; context != null; context = context.Tail)
            {
              if (context.Head.Three == "entry") { return false; }
              if (context.Head.Three.StartsWith("before"))
              {
                // find if we are inside another contract
                Subroutine sub = context.Head.One.Subroutine;
                return sub.IsEnsuresOrOld || sub.IsRequires || sub.IsInvariant;
              }
            }
            throw new NotImplementedException();
          }
        }
        return false;
      }
    }

    public bool InsideRequiresAtCall
    {
      get {
        if (this.Block.Subroutine.IsRequires)
        {
          if (this.SubroutineContext != null)
          {
            for (SubroutineContext context = this.SubroutineContext; context != null; context = context.Tail)
            {
              if (context.Head.Three == "entry") { return false; }
              if (context.Head.Three.StartsWith("before")) { return true; }
            }
            throw new NotImplementedException();
          }
        }
        return false;
      }
    }

    public bool InsideEnsuresAtCall
    {
      get {
        if (this.Block.Subroutine.IsEnsuresOrOld)
        {
          if (this.SubroutineContext != null)
          {
            for (SubroutineContext context = this.SubroutineContext; context != null; context = context.Tail)
            {
              if (context.Head.Three == "exit") { return false; }
              if (context.Head.Three == "oldmanifest") { return false; }
              if (context.Head.Three.StartsWith("after")) { return true; }
            }
            throw new NotImplementedException();
          }
        }
        return false;
      }
    }

    public bool InsideInvariantAtCall
    {
      get
      {
        if (this.Block.Subroutine.IsInvariant)
        {
          if (this.SubroutineContext != null)
          {
            for (SubroutineContext context = this.SubroutineContext; context != null; context = context.Tail)
            {
              if (context.Head.Three == "exit") { return false; }
              if (context.Head.Three == "entry") { return false; }
              if (context.Head.Three.StartsWith("after")) { return true; }
            }
            throw new NotImplementedException();
          }
        }
        return false;
      }
    }

    public bool InsideInvariantInMethod
    {
      get
      {
        if (this.Block.Subroutine.IsInvariant)
        {
          if (this.SubroutineContext != null)
          {
            for (SubroutineContext context = this.SubroutineContext; context != null; context = context.Tail)
            {
              if (context.Head.Three == "exit") { return true; }
              if (context.Head.Three == "entry") { return true; }
              if (context.Head.Three.StartsWith("after")) { return true; }
            }
            return false;
          }
        }
        return false;
      }
    }

    internal bool InsideInvariantOnExit
    {
      get { 
        if (this.Block.Subroutine.IsInvariant)
        {
          if (this.SubroutineContext != null)
          {
            for (SubroutineContext context = this.SubroutineContext; context != null; context = context.Tail)
            {
              if (context.Head.Three == "exit") { return true; }
              if (context.Head.Three == "entry") { return false; }
              if (context.Head.Three.StartsWith("after")) { return false; }
            }
            throw new NotImplementedException();
          }
        }
        return false;
}
    }

    public bool InsideContract
    {
      get
      {
        Subroutine subr = this.Block.Subroutine;
        if (subr.IsContract) return true;
        return false;
      }
    }

    public bool InsideOldManifestation
    {
      get
      {
        Subroutine subr = this.Block.Subroutine;
        if (subr.IsOldValue)
        {
          for (SubroutineContext context = this.SubroutineContext; context != null; context = context.Tail)
          {
            if (context.Head.Three == "oldmanifest") return true;
          }
        }
        return false;
      }
    }

    public bool IsCompilerGenerated
    {
      get
      {
        if (this.Block.Subroutine.IsCompilerGenerated)
        {
          return true;
        }
        else
        {
          if (this.SubroutineContext != null)
          {
            for (SubroutineContext context = this.SubroutineContext; context != null; context = context.Tail)
            {
              if (context.Head.One.Subroutine.IsCompilerGenerated) return true;
            }
          }
        }
        return false;
      }
    }

    public override bool Equals(object obj)
    {
      APC that = obj as APC;
      if (that == null) return false;

      return this.Index == that.Index && this.Block == that.Block && this.SubroutineContext == that.SubroutineContext;
    }

    public override int GetHashCode()
    {
      return this.Index + this.Block.Index;
    }

  }

  public interface IMethodInfo<Method2> {
    Method2 Method { get; }
  }

  /// <summary>
  /// Represents a region of code that has a single entry and exit, and control flow enters
  /// only at the given entry and exits only at the given exit. Exceptions can escape from any
  /// point however.
  /// We use subroutines to model entire methods, finally and fault regions within methods,
  /// and injected code, such as pre-condition and post-condition evaluations around method calls.
  ///
  /// The ITypedProperties interface provdes a type safe property map on subroutines so that analyses
  /// (e.g., stack depth) can store info on each subroutine
  /// </summary>
  public abstract class Subroutine : ITypedProperties, IEquatable<Subroutine> {
    private static int subroutineIdGen;
    protected readonly int subroutineId = subroutineIdGen++;

    public int Id { get { return this.subroutineId; } }
    public abstract string Kind { get; }
    public abstract CFGBlock Entry { get; }
    public abstract CFGBlock EntryAfterRequires { get; }
    public abstract CFGBlock Exit { get; }
    public abstract CFGBlock ExceptionExit { get; }
    public abstract string Name { get; }
    public abstract bool HasReturnValue { get; }
    /// <summary>
    /// true for pre/post condition subroutines and methods (if they can be inlined), but not for 
    /// fault/finally, as these always have 0 initial stack depth
    /// </summary>
    public abstract bool HasContextDependentStackDepth { get; }

    /// <summary>
    /// Block level normal successors
    /// </summary>
    public abstract IEnumerable<CFGBlock> SuccessorBlocks(CFGBlock block);

    /// <summary>
    /// Block level normal predecessors
    /// </summary>
    public abstract IEnumerable<CFGBlock> PredecessorBlocks(CFGBlock block);

    /// <summary>
    /// Blocks are ordered from 0 - n, so arrays can be used for lookup maps
    /// </summary>
    public abstract int BlockCount { get; }

    /// <summary>
    /// Block level view of CFG
    /// </summary>
    public abstract IEnumerable<CFGBlock> Blocks { get; }

    public abstract IEnumerable<Subroutine> UsedSubroutines { get; }

    public abstract void Print(TextWriter tw, ILPrinter<APC> ilPrinter, BlockInfoPrinter<APC>/*?*/ blockInfoPrinter, Func<CFGBlock, IEnumerable<SubroutineContext>> contextLookup, SubroutineContext context, ISet<Pair<Subroutine, SubroutineContext>> printed);


    /// <summary>
    /// Returns true, if the block starts an exception or filter handler, false otherwise.
    /// NOTE: blocks relating to fault or finally handlers never return true, as these are expanded out in the CFG abstraction
    /// </summary>
    public virtual bool IsCatchFilterHeader(CFGBlock block) { return false; }
    internal virtual bool IsRequires { get { return false; } }
    internal virtual bool IsEnsures { get { return false; } }
    internal bool IsEnsuresOrOld { get { return IsEnsures || IsOldValue; } }
    internal virtual bool IsOldValue { get { return false; } }
    public virtual bool IsMethod { get { return false; } }
    internal virtual bool IsInvariant { get { return false; } }
    internal virtual bool IsFaultFinally { get { return false; } }
    internal virtual bool IsContract { get { return false; } }
    internal abstract bool IsSubroutineStart(CFGBlock block);
    internal abstract bool IsSubroutineEnd(CFGBlock block);
    internal abstract bool IsJoinPoint(CFGBlock block);
    internal abstract bool IsSplitPoint(CFGBlock block);
    internal virtual bool IsCompilerGenerated { get { return false; } }

    /// <summary>
    /// Returns the delta of the evaluation stack after evaluating this subroutine.
    /// Usually 0, but subroutines such as old subroutines push 1 value onto the stack.
    /// </summary>
    internal abstract int StackDelta { get; }

    internal abstract APC ComputeTargetFinallyContext(APC ppoint, CFGBlock succ, DConsCache consCache);

    internal abstract EdgeMap<Tag> SuccessorEdges { get; }
    internal abstract EdgeMap<Tag> PredecessorEdges { get; }
    internal abstract bool HasSingleSuccessor(APC ppoint, out APC next, DConsCache consCache);
    internal abstract bool HasSinglePredecessor(APC ppoint, out APC next, DConsCache consCache);
    internal abstract APC PredecessorPCPriorToRequires(APC ppoint, DConsCache consCache);
    internal abstract IEnumerable<APC> Predecessors(APC ppoint, DConsCache consCache);
    internal abstract IEnumerable<APC> Successors(APC ppoint, DConsCache consCache);
    internal abstract DepthFirst.Visitor<CFGBlock, Unit>/*?*/ EdgeInfo { get; }
    /// <summary>
    /// Enumerates the local exception handler start blocks protecting the given ppoint and surrounding fault/finally subroutines also protecting ppoint. If the exception can escape the current subroutine
    /// then the last enumerated block is the subroutine exception exit block.
    /// </summary>
    /// <typeparam name="Data">Handler predicate specific type</typeparam>
    /// <typeparam name="Type">Type of exceptions</typeparam>
    /// <param name="ppoint">Current point at which exception is thrown</param>
    /// <param name="currentSubroutine">if non-null, a subroutine already executing as a transfer from ppoint. Reduces the set of
    /// handlers to those surrounding the execution of the given subroutine from ppoint.</param>
    /// <param name="data">Handler filter specific data</param>
    /// <param name="handlerPredicate">Called with each potential handler. Can determine if handler is chosen and if search continues</param>
    /// <returns>All selected handlers within the subroutine ppoint.Block.Container.</returns>
    internal abstract IEnumerable<CFGBlock> ExceptionHandlers<Data, Type>(CFGBlock ppoint, Subroutine innerSubroutine, Data data, IHandlerFilter<APC, Type, Data> handlerPredicate);
    public abstract void AddEdgeSubroutine(CFGBlock from, CFGBlock to, Subroutine subroutine, string callTag);

    /// <param name="context">APC where edge is being considered. Used for filtering recursive calls.</param>
    internal abstract FList<Pair<string, Subroutine>> EdgeSubroutinesOuterToInner(CFGBlock current, CFGBlock succ, out bool isExceptionHandlerEdge, SubroutineContext context);

    /// <summary>
    /// The constructor only builds the subroutine, but does not really traverse all the code to avoid 
    /// infinite recursions in the cache lookup.
    /// This method is called by the cache constructor after the subroutine is entered into the cache.
    /// </summary>
    internal abstract void Initialize();

    #region ITypedProperties Members

    private TypedProperties propertyMap = new TypedProperties();

    public bool Contains<T>(TypedKey<T> key)
    {
      return propertyMap.Contains(key);
    }

    public void Add<T>(TypedKey<T> key, T value)
    {
      propertyMap.Add(key, value);
    }

    public bool TryGetValue<T>(TypedKey<T> key, out T value)
    {
      return propertyMap.TryGetValue(key, out value);
    }

    #endregion

    internal static int GetKey(Subroutine s) { return s.Id; }


    public void EmitTransferEquations(TextWriter where, InvariantQuery<APC> invariantDB, AssumptionFinder<APC> assumptionsFinder, CrossBlockRenamings<APC> renamings, RenamedVariables<APC> renamedVariables)
    {
      // Create the list to hold the equations representing the equations associated with each node
      List<EquationBlock> equations = new List<EquationBlock>();
      IFunctionalMap<CFGBlock, EquationBlock> cache = FunctionalMap<CFGBlock, EquationBlock>.Empty;

      EquationBlock entryPoint = null;

      foreach (CFGBlock block in this.Blocks) {
        // Console.WriteLine("Block {0}", block.Index);

        EquationBlock currentEquationBlock;

        if (cache.Contains(block)) {
          currentEquationBlock = cache[block];
        }
        else {
          currentEquationBlock = new EquationBlock(block);
          cache = cache.Add(block, currentEquationBlock);
        }

        if (block.Equals(this.Entry)) {
          entryPoint = currentEquationBlock;
        }

#if false // Ignore join points information etc. 
          // revisit this. How should we print CFGs with subroutines?
          if (this.EdgeInfo.DepthFirstInfo(block).TargetOfBackEdge)
          {
            tw.WriteLine(" (target of backedge)");
            Debug.Assert(this.IsJoinPoint(block));
          }
          else if (this.IsJoinPoint(block))
          {
            tw.WriteLine(" (join point)");
          }
          else
          {
            tw.WriteLine();
          }
#endif
#if false // Ignore the Predecessors
          tw.Write("  Predecessors: ");
          foreach (Pair<string, CFGBlock<Label>> taggedPred in block.Subroutine.PredecessorEdges[block])
          {
            tw.Write("({0}, {1}) ", taggedPred.One, taggedPred.Two.Index);
          }
#endif
#if false // Ignore (for the moment) the exception handlers
          tw.WriteLine();
          tw.Write("  Handlers: ");
          foreach (Handler handler in this.GetProtectingHandlers(block))
          {
            if (this.MethodInfo.IsFaultOrFinally(handler))
            {
              FaultFinallySubroutineBase sb = this.FaultFinallySubroutines[handler];
              tw.Write("SR{0} ", sb.Id);
            }
            else
            {
              CFGBlock<Label> handlerStart = this.CatchFilterHeaders[handler];
              tw.Write("{0} ", handlerStart.Index);
            }
          }
          // print out method wide handler (implicitly added in the appropriate query on CFGs)
          if (block != this.exceptionExit)
          {
            tw.Write("{0} ", this.exceptionExit.Index);
          }
          tw.WriteLine();
          tw.WriteLine("  Code:");
#endif
        string condition = null;
        // Search for the condition, to see if it is a conditional transition
        foreach (APC apc in block.APCs()) {
          condition += assumptionsFinder(apc);
        }

        currentEquationBlock.Condition = condition;

        // Successors
        foreach (Pair<Tag, CFGBlock> succ in this.SuccessorEdges[block]) {
          // tw.Write("({0},{1}", taggedSucc.One, taggedSucc.Two.Index);

          // Get the renamings associated with the transition from "block" to "succ"
          IFunctionalMap<string, string> assignments = renamings(block.Last, succ.Two.First);

          // Get the variables that are renamed, that is the parameter
          Set<string> renamed = renamedVariables(block.Last, succ.Two.First);

          EquationBlock successorEquationBlock;
          if (cache.Contains(succ.Two)) {
            successorEquationBlock = cache[succ.Two];
          }
          else {
            successorEquationBlock = new EquationBlock(succ.Two);
            cache = cache.Add(succ.Two, successorEquationBlock);
          }

          currentEquationBlock.AddParameters(renamed);
          currentEquationBlock.AddBody(new EquationBody(currentEquationBlock, successorEquationBlock, assignments));

#if false // Ignore (for the momentd) the subroutines
             if (this.EdgeInfo.IsBackEdge(new Pair<CFGBlock<Label>, CFGBlock<Label>>(block, taggedSucc.Two)))
            {
              tw.Write(" BE");
            }           
            FList<Subroutine<Label>> edgesubs = this.GetOrdinaryEdgeSubOroutines(block, taggedSucc.Two);
            while (edgesubs != null)
            {
              tw.Write(" SR{0}", edgesubs.Head.Id);
              edgesubs = edgesubs.Tail;
            }
#endif
        }
        equations.Add(currentEquationBlock);
      }

#if false // Ignore (for the moment) the subroutines
        // print subroutines
        foreach (FaultFinallySubroutineBase sb in this.FaultFinallySubroutines.Values)
        {
          tw.WriteLine("Subroutine SR{0}", sb.Id);
          tw.WriteLine("-----------------");
          sb.Print(tw, ilPrinter, blockInfoPrinter);
          tw.WriteLine();
        }
        foreach (FList<Subroutine<Label>> sblist in this.edgeSubroutines.Values)
        {
          FList<Subroutine<Label>> slist = sblist;
          while (slist != null)
          {
            Subroutine<Label> sb = slist.Head;
            slist = slist.Tail;
            tw.WriteLine("Subroutine SR{0}", sb.Id);
            tw.WriteLine("-----------------");
            sb.Print(tw, ilPrinter, blockInfoPrinter);
            tw.WriteLine();
          }
        }
#endif
      // Do the output
      BenigniMain.EmitTheEquations(entryPoint, equations, where, invariantDB);
    }


    #region IEquatable<Subroutine> Members

    public bool Equals(Subroutine that)
    {
      return this == that;
    }

    #endregion
  }


  internal delegate SubroutineContext DConsCache(SubroutineEdge edge, SubroutineContext/*?*/ tail);

  public abstract class CFGBlock : IEquatable<CFGBlock> {

    #region Private/Internal
    private int id;
    private int reversePostOrder;
    private Subroutine subroutine;

    internal void SetReversePostOrderIndex(int index)
    {
      this.reversePostOrder = index;
    }

    protected CFGBlock(Subroutine container, ref int idGen)
    {
      id = idGen++;
      this.subroutine = container;
    }

    internal void Renumber(ref int idGen)
    {
      this.id = idGen++;
    }


    #endregion

    internal int Index { get { return this.id; } }

    /// <summary>
    /// Index in reverse post order. This is the preferred order for forward data flow analysis
    /// </summary>
    public int ReversePostOrderIndex { get { return this.reversePostOrder; } }

    public abstract int Count
    {
      get;
    }

    /// <summary>
    /// Returns the list of APCs represented by this block (without finally contexts)
    /// </summary>
    public IEnumerable<APC> APCs()
    {
      return APCs(null);
    }

    /// <summary>
    /// Returns the list of APCs represented by this block (without finally contexts)
    /// </summary>
    public IEnumerable<APC> APCs(SubroutineContext context)
    {
      for (int i = 0; i < this.Count; i++)
      {
        yield return new APC(this, i, context);
      }
    }


    public APC First
    {
      get { return new APC(this, 0, null); }
    }

    /// <summary>
    /// Returns program point after last statement in this block
    /// </summary>
    public APC Last
    {
      get
      {
        return APC.ForEnd(this, null);
      }
    }

    /// <summary>
    /// Returns program point prior to last statement in this block
    /// </summary>
    public APC PriorLast
    {
      get
      {
        int priorToLast = this.Count - 1;
        if (priorToLast < 0) { priorToLast = 0; }
        return new APC(this, priorToLast, null);
      }
    }

    public abstract int ILOffset(APC pc);

    public abstract string SourceContext(APC pc);
    public abstract string SourceDocument(APC pc);
    public abstract int SourceStartLine(APC pc);
    public abstract int SourceEndLine(APC pc);
    public abstract int SourceStartColumn(APC pc);
    public abstract int SourceEndColumn(APC pc);
    public abstract string SourceAssertionCondition(APC pc);

    internal abstract Result ForwardDecode<Local, Parameter, Method2, Field, Type2, Data, Result, Visitor>(APC pc, Visitor visitor, Data data)
      where Visitor : IVisitMSIL<APC, Local, Parameter, Method2, Field, Type2, UnitSource, UnitDest, Data, Result>;

    public Subroutine Subroutine { get { return this.subroutine; } }

    /// <summary>
    /// Returns true if the block is a method call block or newObj call block
    /// </summary>
    /// <param name="parameterCount">on success holds the parameter count of the called method (not including "this")</param>
    internal virtual bool IsMethodCallBlock<Method>(out Method calledMethod, out bool isNewObj, out bool isVirtual) {
      calledMethod = default(Method);
      isNewObj = false;
      isVirtual = false;
      return false;
    } 

    #region IEquatable<CFGBlock> Members

    public bool Equals(CFGBlock other)
    {
      return this.id == other.id;
    }

    #endregion

  }


  /// <summary>
  /// Caches method CFGs, pre and post condition subroutines
  /// </summary>
  public class MethodCache<Local, Parameter, Type, Method, Field, Property, Attribute, Assembly>
   : IMethodCodeConsumer<Local, Parameter, Method, Field, Type, Unit, Subroutine>
  {
    #region ------------ Fields -------------

    public readonly IDecodeMetaData<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly> MetadataDecoder;
    public readonly IDecodeContracts<Local, Parameter, Method, Field, Type> ContractDecoder;

    readonly Dictionary<Method, ControlFlow<Type, Method>> methodCache = new Dictionary<Method, ControlFlow<Type, Method>>();
    readonly Dictionary<Method, Set<Field>> methodModifies = new Dictionary<Method, Set<Field>>();
    readonly InvariantCache invariantCache;
    readonly RequiresCache requiresCache;
    readonly EnsuresCache ensuresCache;
    
    #endregion

    public MethodCache(IDecodeMetaData<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly> mdDecoder,
                       IDecodeContracts<Local, Parameter, Method, Field, Type> contractDecoder,
                       IOutput output
                      )
    {
      this.MetadataDecoder = mdDecoder;
      this.ContractDecoder = contractDecoder;
      this.requiresCache = new RequiresCache(this, output);
      this.ensuresCache = new EnsuresCache(this);
      this.invariantCache = new InvariantCache(this);
    }

    public ICFG<Type, APC> GetCFG(Method method)
    {
      if (methodCache.ContainsKey(method)) {
        return methodCache[method];
      }
      ControlFlow<Type, Method> result;
      if (MetadataDecoder.HasBody(method)) {
        Subroutine sb = MetadataDecoder.AccessMethodBody(method, this, Unit.Value);
        result = new ControlFlow<Type, Method>(method, sb, this);
      }
      else {
        throw new InvalidOperationException("Method has no body");
      }
      //methodCache.Add(method, result);
      return result;
    }

    /// <summary>
    /// Returns null if method has no requires (and no inherited requires)
    /// </summary>
    internal Subroutine GetRequires(Method method)
    {
      // Experimental
      //   To get contracts on generi/specialized method instances, let's grab the unspecialized method
      //   here.
      method = this.MetadataDecoder.Unspecialized(method);
      // End Experimental
      return requiresCache.Get(method);
    }


    /// <summary>
    /// Returns null if method has no ensures (and no inherited ensures)
    /// </summary>
    internal Subroutine GetEnsures(Method method)
    {
      // Experimental
      //   To get contracts on generi/specialized method instances, let's grab the unspecialized method
      //   here.
      method = this.MetadataDecoder.Unspecialized(method);
      // End Experimental
      return ensuresCache.Get(method);
    }

    /// <summary>
    /// Returns null if no invariant is specified
    /// </summary>
    public Subroutine GetInvariant(Type type)
    {
      // we have to make sure we are not confused by the particular instantiation of the type
      type = this.MetadataDecoder.Unspecialized(type);
      return invariantCache.Get(type);
    }


    #region IMethodCodeConsumer<Local,Parameter,Method,Field,Type,Unit,Unit> Members

    Subroutine IMethodCodeConsumer<Local, Parameter, Method, Field, Type, Unit, Subroutine>.Accept<Label, Handler>(
      IMethodCodeProvider<Label, Method, Type, Handler> codeProvider, 
      IDecodeMSIL<Label, Local, Parameter, Method, Field, Type, Unit, Unit, Unit> decoder,
      Label entryPoint,
      Method method,
      Unit data)
    {
      SubroutineWithHandlersBuilder<Label, Handler> sb = new SubroutineWithHandlersBuilder<Label, Handler>(codeProvider, decoder, this, method, entryPoint);
      return new MethodSubroutine<Label, Handler>(this, method, sb, entryPoint);
    }

    #endregion

    #region ----------- Nested Types -------------

    abstract class SubroutineFactory<Key, Data> : ICodeConsumer<Local, Parameter, Method, Field, Type, Data, Subroutine> {
      readonly Dictionary<Key, Subroutine> cache = new Dictionary<Key,Subroutine>();
      protected readonly MethodCache<Local, Parameter, Type, Method, Field, Property, Attribute, Assembly> MethodCache;
      protected IDecodeContracts<Local, Parameter, Method, Field, Type> ContractDecoder { get { return this.MethodCache.ContractDecoder; } }
      protected IDecodeMetaData<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly> MetadataDecoder { get { return this.MethodCache.MetadataDecoder; } }

      public SubroutineFactory(
        MethodCache<Local,Parameter,Type,Method,Field,Property,Attribute,Assembly> methodCache
      )
      {
        this.MethodCache = methodCache;
      }

      public Subroutine Get(Key key) {
        if (cache.ContainsKey(key)) {
          return cache[key];
        }
        Subroutine result = this.BuildNewSubroutine(key);
        cache.Add(key, result);
        // Perform the initialization after adding it to the cache due to recursion
        if (result != null) result.Initialize();
        return result;
      }

      public void Install(Key key, Subroutine sr)
      {
        if (cache.ContainsKey(key))
        {
          Debug.Assert(cache[key] == null);
        }
        cache[key] = sr;
      }

      protected abstract Subroutine BuildNewSubroutine(Key key);

      protected abstract Subroutine Factory<Label>(SimpleSubroutineBuilder<Label> sb, Label entry, Data data);

      #region ICodeConsumer<Local,Parameter,Method,Field,Type,Unit,Subroutine> Members


      public Subroutine Accept<Label>(ICodeProvider<Label, Method, Type> codeProvider,
                                      IDecodeMSIL<Label, Local, Parameter, Method, Field, Type, Unit, Unit, Unit> decoder,
                                      Label entryPoint, Data data)
      {
        SimpleSubroutineBuilder<Label> sb = new SimpleSubroutineBuilder<Label>(codeProvider, decoder, this.MethodCache, entryPoint);

        Subroutine subr = Factory(sb, entryPoint, data);

        return subr;
      }

      #endregion
    }

    class EnsuresCache : SubroutineFactory<Method, Pair<Method,IFunctionalSet<Subroutine>>> {
      Method lastMethodWeAddedInferredEnsures;
      Set<string> lastMethodInferredEnsures;
      
      public EnsuresCache(MethodCache<Local, Parameter, Type, Method, Field, Property, Attribute, Assembly> methodCache)
        : base(methodCache)
      { }

      public bool AlreadyInferred(Method method, string postCondition)
      {
        if (!MetadataDecoder.Equal(method, lastMethodWeAddedInferredEnsures))
        {
          this.lastMethodWeAddedInferredEnsures = method;
          this.lastMethodInferredEnsures = new Set<string>();
        }

        return !this.lastMethodInferredEnsures.AddQ(postCondition);
      }

      protected override Subroutine BuildNewSubroutine(Method method)
      {
        IFunctionalSet<Subroutine> inheritedEnsures = this.GetInheritedEnsures(method);
        if (this.ContractDecoder.HasEnsures(method)) {
          return this.ContractDecoder.AccessEnsures(method, this, new Pair<Method,IFunctionalSet<Subroutine>>(method,inheritedEnsures));
        }
        else if (inheritedEnsures.Count > 0) {
          if (inheritedEnsures.Count > 1) {
            // make up a label
            return new EnsuresSubroutine<Unit>(this.MethodCache, method, inheritedEnsures);
          }
          else {
            return inheritedEnsures.Any;
          }
        }
        else {
          return null;
        }
      }

      protected override Subroutine Factory<Label>(SimpleSubroutineBuilder<Label> sb, Label entry, Pair<Method,IFunctionalSet<Subroutine>> data)
      {
        return new EnsuresSubroutine<Label>(this.MethodCache, data.One, sb, entry, data.Two);
      }

      private IFunctionalSet<Subroutine> GetInheritedEnsures(Method method)
      {
        IFunctionalSet<Subroutine> result = FunctionalSet<Subroutine>.Empty(Subroutine.GetKey);
        if (MetadataDecoder.IsVirtual(method)) {
          foreach (Method baseMethod in MetadataDecoder.OverriddenAndImplementedMethods(method)) {
            Subroutine rs = this.Get(MetadataDecoder.Unspecialized(baseMethod));
            if (rs != null) {
              result = result.Add(rs);
            }
          }
        }
        return result;
      }


    }

    class RequiresCache : SubroutineFactory<Method, Pair<Method,IFunctionalSet<Subroutine>>> {

      // for issuing warnings about inheritance of multiple requires
      IOutput output;

      public RequiresCache(
        MethodCache<Local, Parameter, Type, Method, Field, Property, Attribute, Assembly> methodCache,
        IOutput output
      )
        : base(methodCache)
      {
        this.output = output;
      }


      protected override Subroutine Factory<Label>(SimpleSubroutineBuilder<Label> sb, Label entry, Pair<Method,IFunctionalSet<Subroutine>> data)
      {
        return new RequiresSubroutine<Label>(this.MethodCache, data.One, sb, entry, data.Two);
      }

      protected override Subroutine BuildNewSubroutine(Method method)
      {
        IFunctionalSet<Subroutine> inheritedRequires = this.GetInheritedRequires(method);
        if (this.ContractDecoder.HasRequires(method)) {
          if (inheritedRequires.Count > 0) {
            if (!output.LogOptions.IsSilentOutput) {
              output.WriteLine("Error: method {0} has a pre-condition and inherits other pre-conditions.", this.MetadataDecoder.FullName(method));
            }
          }
          return this.ContractDecoder.AccessRequires(method, this, new Pair<Method,IFunctionalSet<Subroutine>>(method,inheritedRequires));
        }
        else if (inheritedRequires.Count > 0) {
          if (inheritedRequires.Count > 1) {
            if (!output.LogOptions.IsSilentOutput) {
              output.WriteLine("Error: method {0} inherits multiple pre-conditions.", this.MetadataDecoder.FullName(method));
            }
            // we can make up a label here.
            return new RequiresSubroutine<Unit>(this.MethodCache, method, inheritedRequires);
          }
          else {
            return inheritedRequires.Any;
          }
        }
        else {
          return null;
        }
      }

      private IFunctionalSet<Subroutine> GetInheritedRequires(Method method)
      {
        IFunctionalSet<Subroutine> result = FunctionalSet<Subroutine>.Empty(Subroutine.GetKey);
        if (this.MetadataDecoder.IsVirtual(method)) {
          foreach (Method baseMethod in this.MetadataDecoder.OverriddenAndImplementedMethods(method)) {
            Subroutine rs = this.Get(this.MetadataDecoder.Unspecialized(baseMethod));
            if (rs != null) {
              result = result.Add(rs);
            }
          }
        }
        return result;
      }

    }

    class InvariantCache : SubroutineFactory<Type, Subroutine> {

      public InvariantCache(MethodCache<Local, Parameter, Type, Method, Field, Property, Attribute, Assembly> methodCache)
        : base(methodCache)
      {
      }

      protected override Subroutine BuildNewSubroutine(Type type)
      {
        Subroutine baseInv = GetInheritedInvariant(type);

        if (this.ContractDecoder.HasInvariant(type)) {
          return this.ContractDecoder.AccessInvariant<Subroutine, Subroutine>(type, this, baseInv);
        }
        else {
          return baseInv;
        }
      }

      protected override Subroutine Factory<Label>(SimpleSubroutineBuilder<Label> sb, Label entry, Subroutine baseInv)
      {
        return new InvariantSubroutine<Label>(MethodCache, sb, entry, baseInv);
      }


      private Subroutine GetInheritedInvariant(Type type)
      {
        if (this.MetadataDecoder.HasBaseClass(type)) {
          Type baseClass = this.MetadataDecoder.BaseClass(MetadataDecoder.Unspecialized(type));
          Subroutine baseInv = this.MethodCache.GetInvariant(baseClass);
          return baseInv;
        }
        return null;
      }

    }


    /// <summary>
    /// This data structure contains data used during the construction of a Subroutine. For methods, this structure
    /// is used to hold handler information and is shared among multiple subroutine, namely the method subroutine and
    /// all the fault/finally subroutine within it.
    /// </summary>
    internal abstract class SubroutineBuilder<Label> {

      #region Data
      internal readonly MethodCache<Local, Parameter, Type, Method, Field, Property, Attribute, Assembly> MethodCache;
      private readonly Set<Label> labelsStartingBlocks = new Set<Label>();
      private readonly Set<Label> targetLabels = new Set<Label>();
      private OnDemandMap<Label, Pair<Method,bool>> labelsForCallSites;
      private OnDemandMap<Label, Method> labelsForNewObjSites;

      internal readonly ICodeProvider<Label, Method, Type> CodeProvider;
      internal readonly IDecodeMSIL<Label, Local, Parameter, Method, Field, Type, Unit, Unit, Unit> MSILDecoder;
      internal IDecodeMetaData<Local, Parameter, Method, Field, Property, Type, Attribute, Assembly> MetadataDecoder { get { return this.MethodCache.MetadataDecoder; } }
      internal IDecodeContracts<Local, Parameter, Method, Field, Type> ContractDecoder { get { return this.MethodCache.ContractDecoder; } }
      #endregion

      protected SubroutineBuilder(
        ICodeProvider<Label, Method, Type> codeProvider,
        IDecodeMSIL<Label, Local, Parameter, Method, Field, Type, Unit, Unit, Unit> msilDecoder,
        MethodCache<Local, Parameter, Type, Method, Field, Property, Attribute, Assembly> methodCache,
        Label entry
      )
      {
        this.CodeProvider = codeProvider;
        this.MethodCache = methodCache;
        this.MSILDecoder = msilDecoder;
        this.AddTargetLabel(entry);
      }

      protected void AddTargetLabel(Label target)
      {
        this.AddBlockStart(target);
        this.targetLabels.Add(target);
      }

      protected void AddBlockStart(Label target)
      {
        this.labelsStartingBlocks.Add(target);
      }


      protected void Initialize(Label entry)
      {
        new BlockStartGatherer(this).TraceAggregateSequentially(entry);
      }

      protected abstract SubroutineBase<Label> CurrentSubroutine { get; }

      public bool IsBlockStart(Label label)
      {
        return labelsStartingBlocks.Contains(label);
      }

      internal virtual void RecordBlockInfoSameAsOtherBlock(BlockWithLabels<Label> ab, BlockWithLabels<Label> otherblock)
      {
      }

      internal bool IsMethodCallSite(Label label, out Pair<Method,bool> methodVirtPair)
      {
        return this.labelsForCallSites.TryGetValue(label, out methodVirtPair);
      }

      internal bool IsNewObjSite(Label label, out Method constructor)
      {
        return this.labelsForNewObjSites.TryGetValue(label, out constructor);
      }

      /// <summary>
      /// Returns the last block
      /// </summary>
      protected BlockWithLabels<Label> BuildBlocks(Label start)
      {
        return BlockBuilder.BuildBlocks(start, this);
      }

      protected virtual BlockWithLabels<Label> RecordInformationForNewBlock(Label currentLabel, BlockWithLabels<Label>/*?*/ previousBlock)
      {
        BlockWithLabels<Label> newBlock = CurrentSubroutine.GetBlock(currentLabel);

        if (previousBlock != null) {
          var postConditionTarget = newBlock;
          var preConditionSource = previousBlock;
          // Special hack for ensures subroutines so we can insert end_old where needed.
          // - if we have 2 call blocks back to back, we insert a dummy block in the middle.
          if (this.CurrentSubroutine.IsEnsures && newBlock is MethodCallBlock<Label> && previousBlock is MethodCallBlock<Label>)
          {
            var intermediateBlock = this.CurrentSubroutine.NewBlock();
            postConditionTarget = intermediateBlock;
            preConditionSource = intermediateBlock;
            CurrentSubroutine.AddSuccessor(previousBlock, "fallthrough", intermediateBlock);
            CurrentSubroutine.AddSuccessor(intermediateBlock, "fallthrough", newBlock);
          }
          else
          {
            CurrentSubroutine.AddSuccessor(previousBlock, "fallthrough", newBlock);
          }
          // insert pre/post

          // make sure to insert post condition first, as there may be a post from a previous call, followed by a pre from the subsequent call.
          //
          InsertPostConditionEdges(previousBlock, postConditionTarget);
          InsertPreConditionEdges(preConditionSource, newBlock);
        }
        return newBlock;
      }

      private void InsertPreConditionEdges(BlockWithLabels<Label> previousBlock, BlockWithLabels<Label> newBlock)
      {
        MethodCallBlock<Label> newCallBlock = newBlock as MethodCallBlock<Label>;
        if (newCallBlock != null && !this.CurrentSubroutine.IsContract)
        {
          // insert pre-condition
          string tag = newCallBlock.IsNewObj ? "beforeNewObj" : "beforeCall";
          CurrentSubroutine.AddEdgeSubroutine(previousBlock, newBlock, this.MethodCache.GetRequires(newCallBlock.CalledMethod), tag);
        }
      }

      private void InsertPostConditionEdges(BlockWithLabels<Label> previousBlock, BlockWithLabels<Label> newBlock)
      {
        MethodCallBlock<Label> previousCallBlock = previousBlock as MethodCallBlock<Label>;
        if (previousCallBlock != null)
        {
          string tag = previousCallBlock.IsNewObj ? "afterNewObj" : "afterCall";
          // insert post-condition
          Subroutine post = this.MethodCache.GetEnsures(previousCallBlock.CalledMethod);
          CurrentSubroutine.AddEdgeSubroutine(previousBlock, newBlock, post, tag);
          // insert invariant on exit (but not if we are ourselves within an invariant) or for getters
          Property p;
          if (!this.CurrentSubroutine.IsInvariant && !this.MetadataDecoder.IsPropertyGetter(previousCallBlock.CalledMethod, out p) && !this.MetadataDecoder.IsStatic(previousCallBlock.CalledMethod))
          {
            Subroutine invariant = this.MethodCache.GetInvariant(this.MetadataDecoder.DeclaringType(previousCallBlock.CalledMethod));
            CurrentSubroutine.AddEdgeSubroutine(previousBlock, newBlock, invariant, tag);
          }
        }
      }

      class BlockStartGatherer : ICodeQuery<Label, Type, Method, Unit, bool>
      {
        SubroutineBuilder<Label> parent;

        public BlockStartGatherer(SubroutineBuilder<Label> parent)
        {
          this.parent = parent;
        }

        /// <returns>true, if next label starts a new block</returns>
        public bool TraceAggregateSequentially(Label current)
        {
          bool nextInstructionStartsNewBlock = false;
          bool more = true;
          do {
            nextInstructionStartsNewBlock = parent.CodeProvider.Decode<Unit, bool>(Unit.Value, current, this);

            more = parent.CodeProvider.Next(current, out current);
            if (more && nextInstructionStartsNewBlock) {
              this.AddBlockStart(current);
            }
          }
          while (more);
          return nextInstructionStartsNewBlock;
        }

        void AddBlockStart(Label target) { this.parent.AddBlockStart(target); }
        void AddTargetLabel(Label target) { this.parent.AddTargetLabel(target); }


        bool ICodeQuery<Label, Type, Method, Unit, bool>.Branch(Unit unit, Label current, Label target)
        {
          this.AddTargetLabel(target);
          return true; // next instruction starts a new block
        }

        bool ICodeQuery<Label, Type, Method, Unit, bool>.BranchCond(Unit unit, Label current, Label target, bool trueBranch, bool fallthrough, Label elseLabel)
        {
          this.AddTargetLabel(target);

          if (!fallthrough) {
            this.AddTargetLabel(elseLabel);
          }

          return true; // next instruction starts a new block
        }

        bool ICodeQuery<Label, Type, Method, Unit, bool>.BranchSwitch(Unit unit, Label current, Type type, IEnumerable<Pair<object, Label>> targets, bool hasDefault, bool fallthrough, Label defaultLabel)
        {
          foreach (Pair<object, Label> target in targets) {
            this.AddTargetLabel(target.Two);
          }
          if (hasDefault) {
            if (fallthrough) {
              return true; // next instruction starts new block
            }
            this.AddTargetLabel(defaultLabel);
          }
          return false;
        }

        bool ICodeQuery<Label, Type, Method, Unit, bool>.Throw(Unit unit, Label current)
        {
          return true;
        }

        bool ICodeQuery<Label, Type, Method, Unit, bool>.EndFinally(Unit data, Label current)
        {
          return true;
        }

        bool ICodeQuery<Label, Type, Method, Unit, bool>.Return(Unit data, Label current)
        {
          return true;
        }

        bool ICodeQuery<Label, Type, Method, Unit, bool>.Aggregate(Unit unit, Label current, Label aggregateStart, bool canBeBranchTarget)
        {
          // recurse
          return TraceAggregateSequentially(aggregateStart);
        }

        bool ICodeQuery<Label, Type, Method, Unit, bool>.Atomic(Unit unit, Label current)
        {
          return false;
        }

        bool ICodeQuery<Label, Type, Method, Unit, bool>.Nop(Unit unit, Label current)
        {
          return false;
        }

        bool ICodeQuery<Label, Type, Method, Unit, bool>.Call(Unit unit, Label current, Method method, bool isVirtual)
        {
          return CallHelper(unit, current, method, false, isVirtual);
        }

        bool CallHelper(Unit unit, Label current, Method method, bool newObj, bool isVirtual)
        {
          // if we insert pre/post conditions, we want separate blocks between the call and the preceding and succeeding instructions
          // so we can add edge subroutines.

          this.AddBlockStart(current);
          if (newObj)
          {
            this.parent.labelsForNewObjSites[current] = method; // okay to double add
          }
          else
          {
            this.parent.labelsForCallSites[current] = new Pair<Method,bool>(method, isVirtual); // okay to double add
          }
 

          return true; // next instruction starts a new block
        }

        bool ICodeQuery<Label, Type, Method, Unit, bool>.NewObj(Unit unit, Label current, Method constructor)
        {
          return CallHelper(unit, current, constructor, true, false);
        }

        bool ICodeQuery<Label, Type, Method, Unit, bool>.BeginOld(Unit unit, Label current)
        {
          // start a new block
          this.AddTargetLabel(current); // It's a target block as it starts a new subroutine
          this.parent.BeginOldHook(current);
          return false;
        }

        bool ICodeQuery<Label, Type, Method, Unit, bool>.EndOld(Unit unit, Label current)
        {
          this.parent.EndOldHook(current);
          return true; // ends a block
        }


      }

      class BlockBuilder : ICodeQuery<Label, Type, Method, BlockWithLabels<Label>, bool>,
        IVisitMSIL<Label,Local,Parameter,Method,Field,Type,Unit, Unit, Unit, Unit>
      {

        SubroutineBuilder<Label> parent;
        BlockWithLabels<Label>/*?*/ currentBlock = null;

        SubroutineBase<Label> CurrentSubroutine { get { return parent.CurrentSubroutine; } }

        private BlockBuilder(SubroutineBuilder<Label> builder)
        {
          this.parent = builder;
        }

        internal static BlockWithLabels<Label> BuildBlocks(Label start, SubroutineBuilder<Label> builder)
        {
          BlockBuilder b = new BlockBuilder(builder);
          b.TraceAggregateSequentially(start);

          // check if we are falling off the end
          if (b.currentBlock != null) {
            // add edge to exit
            b.CurrentSubroutine.AddSuccessor(b.currentBlock, "fallthrough-return", b.CurrentSubroutine.Exit);
            // Callback
            b.CurrentSubroutine.AddReturnBlock(b.currentBlock);

            return b.currentBlock;
          }
          return null;
        }

        /// <summary>
        /// Builds
        ///   - Protecting handler map
        ///   - Successor relation
        ///   - CFG blocks
        /// Temporary structures
        ///   - current protecting handlers
        ///   - currentBlock
        /// </summary>
        /// <param name="currentLabel"></param>
        private void TraceAggregateSequentially(Label currentLabel)
        {
          bool more = true;
          do {
            if (this.parent.IsBlockStart(currentLabel)) {
              // starts a block
              this.currentBlock = this.parent.RecordInformationForNewBlock(currentLabel, this.currentBlock);
            }
            System.Diagnostics.Debug.Assert(this.currentBlock != null);

            // we add the currentLabel to the block in the decoded to Methods
            // this allows optimizing intermediate labels away

            // NOTE: this decode can recurse and cause this.currentBlock to change.
            bool noFallThrough = parent.CodeProvider.Decode<BlockWithLabels<Label>, bool>(this.currentBlock, currentLabel, this);
            if (noFallThrough) {
              this.currentBlock = null;
            }

            more = parent.CodeProvider.Next(currentLabel, out currentLabel);
          }
          while (more);
        }


        bool ICodeQuery<Label, Type, Method, BlockWithLabels<Label>, bool>.Branch(BlockWithLabels<Label> currentBlock, Label current, Label target)
        {
          currentBlock.Add(current);
          CurrentSubroutine.AddSuccessor(currentBlock, "branch", CurrentSubroutine.GetTargetBlock(target));
          return true; // no fall through
        }

        bool ICodeQuery<Label, Type, Method, BlockWithLabels<Label>, bool>.BranchCond(BlockWithLabels<Label> currentBlock, Label current, Label target, bool trueBranch, bool fallthrough, Label elseLabel)
        {
          currentBlock.Add(current);

          string takenLabel = trueBranch ? "true" : "false";
          string notTakenLabel = trueBranch ? "false" : "true";

          // insert a special assume block for taken branch
          AssumeBlock<Label> abTaken = CurrentSubroutine.NewAssumeBlock(current, takenLabel);
          parent.RecordBlockInfoSameAsOtherBlock(abTaken, this.currentBlock);
          CurrentSubroutine.AddSuccessor(currentBlock, takenLabel, abTaken);
          CurrentSubroutine.AddSuccessor(abTaken, "fallthrough", CurrentSubroutine.GetTargetBlock(target));

          // insert a special assume block for untaken branch
          AssumeBlock<Label> abElse = CurrentSubroutine.NewAssumeBlock(current, notTakenLabel);
          parent.RecordBlockInfoSameAsOtherBlock(abElse, this.currentBlock);
          CurrentSubroutine.AddSuccessor(currentBlock, notTakenLabel, abElse);
          if (fallthrough) {
            this.currentBlock = abElse;
            return false; // has fall through
          }
          else {
            CurrentSubroutine.AddSuccessor(abElse, "fallthrough", CurrentSubroutine.GetTargetBlock(elseLabel));
            return true; // no fall through
          }
        }

        bool ICodeQuery<Label, Type, Method, BlockWithLabels<Label>, bool>.BranchSwitch(BlockWithLabels<Label> currentBlock, Label current, Type type, IEnumerable<Pair<object, Label>> targets, bool hasDefault, bool fallthrough, Label defaultLabel)
        {
          List<object> patterns = new List<object>();
          currentBlock.Add(current);
          foreach (Pair<object, Label> target in targets) {
            patterns.Add(target.One);
            // insert a special switch case assume block            
            SwitchCaseAssumeBlock<Label> ab = CurrentSubroutine.NewSwitchCaseAssumeBlock(current, target.One, type);
            parent.RecordBlockInfoSameAsOtherBlock(ab, this.currentBlock);
            CurrentSubroutine.AddSuccessor(currentBlock, "switch", ab);
            CurrentSubroutine.AddSuccessor(ab, "fallthrough", CurrentSubroutine.GetTargetBlock(target.Two));
          }
          if (hasDefault) {
            // insert special default assume block
            SwitchDefaultAssumeBlock<Label> ab = CurrentSubroutine.NewSwitchDefaultAssumeBlock(current, patterns, type);
            parent.RecordBlockInfoSameAsOtherBlock(ab, this.currentBlock);
            CurrentSubroutine.AddSuccessor(currentBlock, "default", ab);

            if (fallthrough) {
              this.currentBlock = ab;
              return false; // has fall through
            }
            else {
              CurrentSubroutine.AddSuccessor(ab, "fallthrough", CurrentSubroutine.GetTargetBlock(defaultLabel));
            }
          }
          return true; // no fall through
        }

        bool ICodeQuery<Label, Type, Method, BlockWithLabels<Label>, bool>.Throw(BlockWithLabels<Label> currentBlock, Label current)
        {
          currentBlock.Add(current);
          return true;
        }

        bool ICodeQuery<Label, Type, Method, BlockWithLabels<Label>, bool>.EndFinally(BlockWithLabels<Label> currentBlock, Label currentLabel)
        {
          currentBlock.Add(currentLabel);
          CurrentSubroutine.AddSuccessor(currentBlock, "endsub", CurrentSubroutine.Exit);
          return true;
        }

        bool ICodeQuery<Label, Type, Method, BlockWithLabels<Label>, bool>.Return(BlockWithLabels<Label> currentBlock, Label currentLabel)
        {
          currentBlock.Add(currentLabel);
          CurrentSubroutine.AddSuccessor(currentBlock, "return", CurrentSubroutine.Exit);
          // Callback
          CurrentSubroutine.AddReturnBlock(currentBlock);
          return true; // no fall through
        }

        bool ICodeQuery<Label, Type, Method, BlockWithLabels<Label>, bool>.Aggregate(BlockWithLabels<Label> currentBlock, Label current, Label aggregateStart, bool canBeBranchTarget)
        {
          // recurse
          this.TraceAggregateSequentially(aggregateStart);
          return false; // recursion would have already set current block to null if there is no fall through.
        }

        bool ICodeQuery<Label, Type, Method, BlockWithLabels<Label>, bool>.Atomic(BlockWithLabels<Label> currentBlock, Label current)
        {
          if (this.CurrentSubroutine.IsMethod)
          {
            IMethodInfo<Method> mi = (IMethodInfo<Method>)this.CurrentSubroutine;
            Property prop;
            if (this.parent.MetadataDecoder.IsPropertySetter(mi.Method, out prop))
            {
              this.parent.MSILDecoder.ForwardDecode<Unit, Unit, BlockBuilder>(current, this, Unit.Value);
            }
          }
          currentBlock.Add(current);
          return false;
        }

        bool ICodeQuery<Label, Type, Method, BlockWithLabels<Label>, bool>.Nop(BlockWithLabels<Label> currentBlock, Label current)
        {
          // ignore this label
          return false;
        }

        /// <summary>
        /// Pre/post insertion is done when new blocks are linked in fall-through edges
        /// </summary>
        bool ICodeQuery<Label, Type, Method, BlockWithLabels<Label>, bool>.Call(BlockWithLabels<Label> currentBlock, Label current, Method method, bool isVirtual)
        {
          this.currentBlock.Add(current);
          return false;
        }

        /// <summary>
        /// Pre/post insertion is done when new blocks are linked in fall-through edges
        /// </summary>
        bool ICodeQuery<Label, Type, Method, BlockWithLabels<Label>, bool>.NewObj(BlockWithLabels<Label> currentBlock, Label current, Method constructor)
        {
          this.currentBlock.Add(current);
          return false;
        }

        /// <summary>
        /// Give current subroutine a chance to record information about matching begin/old old labels
        /// </summary>
        bool ICodeQuery<Label, Type, Method, BlockWithLabels<Label>, bool>.BeginOld(BlockWithLabels<Label> currentBlock, Label current)
        {
          this.currentBlock.Add(current);
          return false;
        }

        /// <summary>
        /// Give current subroutine a chance to record information about matching begin/old old labels
        /// </summary>
        bool ICodeQuery<Label, Type, Method, BlockWithLabels<Label>, bool>.EndOld(BlockWithLabels<Label> currentBlock, Label current)
        {
          this.currentBlock.Add(current);
          CurrentSubroutine.AddSuccessor(currentBlock, "endold", CurrentSubroutine.Exit);

          return false;
        }



        #region IVisitMSIL<Label,Local,Parameter,Method,Field,Type,Unit,Unit,Unit,Unit> Members

        Unit IVisitMSIL<Label, Local, Parameter, Method, Field, Type, Unit, Unit, Unit, Unit>.Arglist(Label pc, Unit dest, Unit data)
        {
          return data;
        }

        Unit IVisitMSIL<Label, Local, Parameter, Method, Field, Type, Unit, Unit, Unit, Unit>.BranchCond(Label pc, Label target, BranchOperator bop, Unit value1, Unit value2, Unit data)
        {
          return data;
        }

        Unit IVisitMSIL<Label, Local, Parameter, Method, Field, Type, Unit, Unit, Unit, Unit>.BranchTrue(Label pc, Label target, Unit cond, Unit data)
        {
          return data;
        }

        Unit IVisitMSIL<Label, Local, Parameter, Method, Field, Type, Unit, Unit, Unit, Unit>.BranchFalse(Label pc, Label target, Unit cond, Unit data)
        {
          return data;
        }

        Unit IVisitMSIL<Label, Local, Parameter, Method, Field, Type, Unit, Unit, Unit, Unit>.Branch(Label pc, Label target, bool leave, Unit data)
        {
          return data;
        }

        Unit IVisitMSIL<Label, Local, Parameter, Method, Field, Type, Unit, Unit, Unit, Unit>.Break(Label pc, Unit data)
        {
          return data;
        }

        Unit IVisitMSIL<Label, Local, Parameter, Method, Field, Type, Unit, Unit, Unit, Unit>.Call<TypeList, ArgList>(Label pc, Method method, bool tail, bool virt, TypeList extraVarargs, Unit dest, ArgList args, Unit data)
        {
          return data;
        }

        Unit IVisitMSIL<Label, Local, Parameter, Method, Field, Type, Unit, Unit, Unit, Unit>.Calli<TypeList, ArgList>(Label pc, Type returnType, TypeList argTypes, bool tail, Unit dest, Unit fp, ArgList args, Unit data)
        {
          return data;
        }

        Unit IVisitMSIL<Label, Local, Parameter, Method, Field, Type, Unit, Unit, Unit, Unit>.Ckfinite(Label pc, Unit dest, Unit source, Unit data)
        {
          return data;
        }

        Unit IVisitMSIL<Label, Local, Parameter, Method, Field, Type, Unit, Unit, Unit, Unit>.Cpblk(Label pc, bool @volatile, Unit destaddr, Unit srcaddr, Unit len, Unit data)
        {
          return data;
        }

        Unit IVisitMSIL<Label, Local, Parameter, Method, Field, Type, Unit, Unit, Unit, Unit>.Endfilter(Label pc, Unit decision, Unit data)
        {
          return data;
        }

        Unit IVisitMSIL<Label, Local, Parameter, Method, Field, Type, Unit, Unit, Unit, Unit>.Endfinally(Label pc, Unit data)
        {
          return data;
        }

        Unit IVisitMSIL<Label, Local, Parameter, Method, Field, Type, Unit, Unit, Unit, Unit>.Initblk(Label pc, bool @volatile, Unit destaddr, Unit value, Unit len, Unit data)
        {
          return data;
        }

        Unit IVisitMSIL<Label, Local, Parameter, Method, Field, Type, Unit, Unit, Unit, Unit>.Jmp(Label pc, Method method, Unit data)
        {
          return data;
        }

        Unit IVisitMSIL<Label, Local, Parameter, Method, Field, Type, Unit, Unit, Unit, Unit>.Ldarg(Label pc, Parameter argument, bool isOld, Unit dest, Unit data)
        {
          return data;
        }

        Unit IVisitMSIL<Label, Local, Parameter, Method, Field, Type, Unit, Unit, Unit, Unit>.Ldarga(Label pc, Parameter argument, bool isOld, Unit dest, Unit data)
        {
          return data;
        }

        Unit IVisitMSIL<Label, Local, Parameter, Method, Field, Type, Unit, Unit, Unit, Unit>.Ldftn(Label pc, Method method, Unit dest, Unit data)
        {
          return data;
        }

        Unit IVisitMSIL<Label, Local, Parameter, Method, Field, Type, Unit, Unit, Unit, Unit>.Ldind(Label pc, Type type, bool @volatile, Unit dest, Unit ptr, Unit data)
        {
          return data;
        }

        Unit IVisitMSIL<Label, Local, Parameter, Method, Field, Type, Unit, Unit, Unit, Unit>.Ldloc(Label pc, Local local, Unit dest, Unit data)
        {
          return data;
        }

        Unit IVisitMSIL<Label, Local, Parameter, Method, Field, Type, Unit, Unit, Unit, Unit>.Ldloca(Label pc, Local local, Unit dest, Unit data)
        {
          return data;
        }

        Unit IVisitMSIL<Label, Local, Parameter, Method, Field, Type, Unit, Unit, Unit, Unit>.Localloc(Label pc, Unit dest, Unit size, Unit data)
        {
          return data;
        }

        Unit IVisitMSIL<Label, Local, Parameter, Method, Field, Type, Unit, Unit, Unit, Unit>.Nop(Label pc, Unit data)
        {
          return data;
        }

        Unit IVisitMSIL<Label, Local, Parameter, Method, Field, Type, Unit, Unit, Unit, Unit>.Pop(Label pc, Unit source, Unit data)
        {
          return data;
        }

        Unit IVisitMSIL<Label, Local, Parameter, Method, Field, Type, Unit, Unit, Unit, Unit>.Return(Label pc, Unit source, Unit data)
        {
          return data;
        }

        Unit IVisitMSIL<Label, Local, Parameter, Method, Field, Type, Unit, Unit, Unit, Unit>.Starg(Label pc, Parameter argument, Unit source, Unit data)
        {
          return data;
        }

        Unit IVisitMSIL<Label, Local, Parameter, Method, Field, Type, Unit, Unit, Unit, Unit>.Stind(Label pc, Type type, bool @volatile, Unit ptr, Unit value, Unit data)
        {
          return data;
        }

        Unit IVisitMSIL<Label, Local, Parameter, Method, Field, Type, Unit, Unit, Unit, Unit>.Stloc(Label pc, Local local, Unit source, Unit data)
        {
          return data;
        }

        Unit IVisitMSIL<Label, Local, Parameter, Method, Field, Type, Unit, Unit, Unit, Unit>.Switch(Label pc, Type type, IEnumerable<Label> cases, Unit value, Unit data)
        {
          return data;
        }

        Unit IVisitMSIL<Label, Local, Parameter, Method, Field, Type, Unit, Unit, Unit, Unit>.Box(Label pc, Type type, Unit dest, Unit source, Unit data)
        {
          return data;
        }

        Unit IVisitMSIL<Label, Local, Parameter, Method, Field, Type, Unit, Unit, Unit, Unit>.ConstrainedCallvirt<TypeList, ArgList>(Label pc, Method method, bool tail, Type constraint, TypeList extraVarargs, Unit dest, ArgList args, Unit data)
        {
          return data;
        }

        Unit IVisitMSIL<Label, Local, Parameter, Method, Field, Type, Unit, Unit, Unit, Unit>.Castclass(Label pc, Type type, Unit dest, Unit obj, Unit data)
        {
          return data;
        }

        Unit IVisitMSIL<Label, Local, Parameter, Method, Field, Type, Unit, Unit, Unit, Unit>.Cpobj(Label pc, Type type, Unit destptr, Unit srcptr, Unit data)
        {
          return data;
        }

        Unit IVisitMSIL<Label, Local, Parameter, Method, Field, Type, Unit, Unit, Unit, Unit>.Initobj(Label pc, Type type, Unit ptr, Unit data)
        {
          return data;
        }

        Unit IVisitMSIL<Label, Local, Parameter, Method, Field, Type, Unit, Unit, Unit, Unit>.Ldelem(Label pc, Type type, Unit dest, Unit array, Unit index, Unit data)
        {
          return data;
        }

        Unit IVisitMSIL<Label, Local, Parameter, Method, Field, Type, Unit, Unit, Unit, Unit>.Ldelema(Label pc, Type type, bool @readonly, Unit dest, Unit array, Unit index, Unit data)
        {
          return data;
        }

        Unit IVisitMSIL<Label, Local, Parameter, Method, Field, Type, Unit, Unit, Unit, Unit>.Ldfld(Label pc, Field field, bool @volatile, Unit dest, Unit obj, Unit data)
        {
          return data;
        }

        Unit IVisitMSIL<Label, Local, Parameter, Method, Field, Type, Unit, Unit, Unit, Unit>.Ldflda(Label pc, Field field, Unit dest, Unit obj, Unit data)
        {
          return data;
        }

        Unit IVisitMSIL<Label, Local, Parameter, Method, Field, Type, Unit, Unit, Unit, Unit>.Ldlen(Label pc, Unit dest, Unit array, Unit data)
        {
          return data;
        }

        Unit IVisitMSIL<Label, Local, Parameter, Method, Field, Type, Unit, Unit, Unit, Unit>.Ldsfld(Label pc, Field field, bool @volatile, Unit dest, Unit data)
        {
          return data;
        }

        Unit IVisitMSIL<Label, Local, Parameter, Method, Field, Type, Unit, Unit, Unit, Unit>.Ldsflda(Label pc, Field field, Unit dest, Unit data)
        {
          return data;
        }

        Unit IVisitMSIL<Label, Local, Parameter, Method, Field, Type, Unit, Unit, Unit, Unit>.Ldtypetoken(Label pc, Type type, Unit dest, Unit data)
        {
          return data;
        }

        Unit IVisitMSIL<Label, Local, Parameter, Method, Field, Type, Unit, Unit, Unit, Unit>.Ldfieldtoken(Label pc, Field field, Unit dest, Unit data)
        {
          return data;
        }

        Unit IVisitMSIL<Label, Local, Parameter, Method, Field, Type, Unit, Unit, Unit, Unit>.Ldmethodtoken(Label pc, Method method, Unit dest, Unit data)
        {
          return data;
        }

        Unit IVisitMSIL<Label, Local, Parameter, Method, Field, Type, Unit, Unit, Unit, Unit>.Ldvirtftn(Label pc, Method method, Unit dest, Unit obj, Unit data)
        {
          return data;
        }

        Unit IVisitMSIL<Label, Local, Parameter, Method, Field, Type, Unit, Unit, Unit, Unit>.Mkrefany(Label pc, Type type, Unit dest, Unit obj, Unit data)
        {
          return data;
        }

        Unit IVisitMSIL<Label, Local, Parameter, Method, Field, Type, Unit, Unit, Unit, Unit>.Newarray<ArgList>(Label pc, Type type, Unit dest, ArgList len, Unit data)
        {
          return data;
        }

        Unit IVisitMSIL<Label, Local, Parameter, Method, Field, Type, Unit, Unit, Unit, Unit>.Newobj<ArgList>(Label pc, Method ctor, Unit dest, ArgList args, Unit data)
        {
          return data;
        }

        Unit IVisitMSIL<Label, Local, Parameter, Method, Field, Type, Unit, Unit, Unit, Unit>.Refanytype(Label pc, Unit dest, Unit source, Unit data)
        {
          return data;
        }

        Unit IVisitMSIL<Label, Local, Parameter, Method, Field, Type, Unit, Unit, Unit, Unit>.Refanyval(Label pc, Type type, Unit dest, Unit source, Unit data)
        {
          return data;
        }

        Unit IVisitMSIL<Label, Local, Parameter, Method, Field, Type, Unit, Unit, Unit, Unit>.Rethrow(Label pc, Unit data)
        {
          return data;
        }

        Unit IVisitMSIL<Label, Local, Parameter, Method, Field, Type, Unit, Unit, Unit, Unit>.Stelem(Label pc, Type type, Unit array, Unit index, Unit value, Unit data)
        {
          return data;
        }

        Unit IVisitMSIL<Label, Local, Parameter, Method, Field, Type, Unit, Unit, Unit, Unit>.Stfld(Label pc, Field field, bool @volatile, Unit obj, Unit value, Unit data)
        {
          if (this.CurrentSubroutine.IsMethod) {
            IMethodInfo<Method> mi = (IMethodInfo<Method>)this.CurrentSubroutine;
            this.parent.MethodCache.AddModifies(mi.Method, field);
          }
          return data;
        }

        Unit IVisitMSIL<Label, Local, Parameter, Method, Field, Type, Unit, Unit, Unit, Unit>.Stsfld(Label pc, Field field, bool @volatile, Unit value, Unit data)
        {
          return data;
        }

        Unit IVisitMSIL<Label, Local, Parameter, Method, Field, Type, Unit, Unit, Unit, Unit>.Throw(Label pc, Unit exn, Unit data)
        {
          return data;
        }

        Unit IVisitMSIL<Label, Local, Parameter, Method, Field, Type, Unit, Unit, Unit, Unit>.Unbox(Label pc, Type type, Unit dest, Unit obj, Unit data)
        {
          return data;
        }

        Unit IVisitMSIL<Label, Local, Parameter, Method, Field, Type, Unit, Unit, Unit, Unit>.Unboxany(Label pc, Type type, Unit dest, Unit obj, Unit data)
        {
          return data;
        }

        #endregion

        #region IVisitSynthIL<Label,Method,Type,Unit,Unit,Unit,Unit> Members

        Unit IVisitSynthIL<Label, Method, Type, Unit, Unit, Unit, Unit>.Entry(Label pc, Method method, Unit data)
        {
          return data;
        }

        Unit IVisitSynthIL<Label, Method, Type, Unit, Unit, Unit, Unit>.Assume(Label pc, string tag, Unit condition, Unit data)
        {
          return data;
        }

        Unit IVisitSynthIL<Label, Method, Type, Unit, Unit, Unit, Unit>.Assert(Label pc, string tag, Unit condition, Unit data)
        {
          return data;
        }

        Unit IVisitSynthIL<Label, Method, Type, Unit, Unit, Unit, Unit>.Ldstack(Label pc, int offset, Unit dest, Unit source, bool isOld, Unit data)
        {
          return data;
        }

        Unit IVisitSynthIL<Label, Method, Type, Unit, Unit, Unit, Unit>.Ldstacka(Label pc, int offset, Unit dest, Unit source, Type type, bool isOld, Unit data)
        {
          return data;
        }

        Unit IVisitSynthIL<Label, Method, Type, Unit, Unit, Unit, Unit>.Ldresult(Label pc, Unit dest, Unit source, Unit data)
        {
          return data;
        }

        Unit IVisitSynthIL<Label, Method, Type, Unit, Unit, Unit, Unit>.BeginOld(Label pc, Label matchingEnd, Unit data)
        {
          return data;
        }

        Unit IVisitSynthIL<Label, Method, Type, Unit, Unit, Unit, Unit>.EndOld(Label pc, Label matchingBegin, Type type, Unit dest, Unit source, Unit data)
        {
          return data;
        }

        #endregion

        #region IVisitExprIL<Label,Type,Unit,Unit,Unit,Unit> Members

        Unit IVisitExprIL<Label, Type, Unit, Unit, Unit, Unit>.Binary(Label pc, BinaryOperator op, Unit dest, Unit s1, Unit s2, Unit data)
        {
          return data;
        }

        Unit IVisitExprIL<Label, Type, Unit, Unit, Unit, Unit>.Isinst(Label pc, Type type, Unit dest, Unit obj, Unit data)
        {
          return data;
        }

        Unit IVisitExprIL<Label, Type, Unit, Unit, Unit, Unit>.Ldconst(Label pc, object constant, Type type, Unit dest, Unit data)
        {
          return data;
        }

        Unit IVisitExprIL<Label, Type, Unit, Unit, Unit, Unit>.Ldnull(Label pc, Unit dest, Unit data)
        {
          return data;
        }

        Unit IVisitExprIL<Label, Type, Unit, Unit, Unit, Unit>.Sizeof(Label pc, Type type, Unit dest, Unit data)
        {
          return data;
        }

        Unit IVisitExprIL<Label, Type, Unit, Unit, Unit, Unit>.Unary(Label pc, UnaryOperator op, bool overflow, bool unsigned, Unit dest, Unit source, Unit data)
        {
          return data;
        }

        #endregion
      }


      internal virtual void BeginOldHook(Label current)
      {
      }

      internal virtual void EndOldHook(Label current)
      {
      }

      internal bool IsTargetLabel(Label label)
      {
        return this.targetLabels.Contains(label);
      }
    }

    internal class SubroutineWithHandlersBuilder<Label, Handler> : SubroutineBuilder<Label> {

      protected override SubroutineBase<Label> CurrentSubroutine { get { return this.subroutineStack.Head; } }

      private FList<SubroutineWithHandlers<Label,Handler>> subroutineStack;

      protected SubroutineWithHandlers<Label, Handler> CurrentSubroutineWithHandler { get { return subroutineStack.Head; } }

      private OnDemandMap<Label, Stack<Handler>> tryStartList;
      private OnDemandMap<Label, Queue<Handler>> tryEndList;
      private OnDemandMap<Label, Queue<Handler>> subroutineHandlerEndList;
      private OnDemandMap<Label, Handler> handlerStartingAt;
      protected readonly Method method;

      internal new readonly IMethodCodeProvider<Label, Method, Type, Handler> CodeProvider;


      public SubroutineWithHandlersBuilder(
        IMethodCodeProvider<Label, Method, Type, Handler> codeProvider,
        IDecodeMSIL<Label, Local, Parameter, Method, Field, Type, Unit, Unit, Unit> msilDecoder,
        MethodCache<Local, Parameter, Type, Method, Field, Property, Attribute, Assembly> methodCache,
        Method method,
        Label entry
      )
        : base(codeProvider, msilDecoder, methodCache, entry)
      {
        this.CodeProvider = codeProvider;
        this.method = method;
        ComputeTryBlockStartAndEndInfo(method);
        base.Initialize(entry);
      }

      public CFGBlock BuildBlocks(Label entry, SubroutineWithHandlers<Label, Handler> subroutine)
      {
        this.subroutineStack = FList<SubroutineWithHandlers<Label, Handler>>.Cons(subroutine, null);
        return base.BuildBlocks(entry);
      }

      internal override void RecordBlockInfoSameAsOtherBlock(BlockWithLabels<Label> ab, BlockWithLabels<Label> otherblock)
      {
        FList<Handler>/*?*/ protHandlers;
        if (this.CurrentSubroutineWithHandler.ProtectingHandlers.TryGetValue(otherblock, out protHandlers)) {
          this.CurrentSubroutineWithHandler.ProtectingHandlers.Add(ab, protHandlers);
        }
      }

      private FList<Handler> CurrentProtectingHandlers
      {
        get { return this.CurrentSubroutineWithHandler.CurrentProtectingHandlers; }
        set { this.CurrentSubroutineWithHandler.CurrentProtectingHandlers = value; }
      }

      protected override BlockWithLabels<Label> RecordInformationForNewBlock(Label currentLabel, BlockWithLabels<Label>/*?*/ previousBlock)
      {
        BlockWithLabels<Label>/*?*/ newBlock = null;

        #region Pop subroutine handlers off stack whose scope ends here (must be first)
        Queue<Handler> handlerEnds = this.GetHandlerEnd(currentLabel);
        if (handlerEnds != null) {
          foreach (Handler eh in handlerEnds) {
            // TODO: not enough info to make sure we are popping the correct subroutine
            SubroutineBase<Label> endingSubroutine = this.subroutineStack.Head;
            endingSubroutine.Commit();

            this.subroutineStack = this.subroutineStack.Tail;
            // can't fall from one subroutine into another
            previousBlock = null;
          }
        }
        #endregion

        #region Pop protecting handlers off stack whose scope ends here

        Queue<Handler> tryEnds = this.GetTryEnd(currentLabel);
        if (tryEnds != null) {
          foreach (Handler eh in tryEnds) {
            if (Object.Equals(eh, CurrentProtectingHandlers.Head)) {
              CurrentProtectingHandlers = CurrentProtectingHandlers.Tail; // Pop handler
            }
            else {
              throw new ApplicationException("wrong handler");
            }
          }
        }
        #endregion

        #region Check if this is a fault/finally subroutine start or an exception handler
        Handler handlerStarting;
        if (this.IsHandlerStart(currentLabel, out handlerStarting)) {
          if (this.IsFaultOrFinally(handlerStarting)) {
            SubroutineWithHandlers<Label,Handler> subroutine;
            if (this.CodeProvider.IsFaultHandler(handlerStarting)) {
              subroutine = new FaultSubroutine<Label, Handler>(this.MethodCache, this, currentLabel);
            }
            else {
              subroutine = new FinallySubroutine<Label, Handler>(this.MethodCache, this, currentLabel);
            }
            this.CurrentSubroutineWithHandler.FaultFinallySubroutines.Add(handlerStarting, subroutine);

            // push new current subroutine
            this.subroutineStack = FList<SubroutineWithHandlers<Label,Handler>>.Cons(subroutine, this.subroutineStack);

            previousBlock = null; // can't fall into here.
          }
          else {
            // This is an exception handler start. Make sure we use a special entry block for it.
            newBlock = CurrentSubroutineWithHandler.CreateCatchFilterHeader(handlerStarting, currentLabel);
          }
        }
        #endregion


        #region Get new block and record fall-through edge if necessary
        if (newBlock == null) {
          newBlock = base.RecordInformationForNewBlock(currentLabel, previousBlock);
        }

        #endregion

        #region Push protecting handlers on stack whose scope starts here
        Stack<Handler> starts = this.GetTryStart(currentLabel);
        if (starts != null) {
          foreach (Handler eh in starts) {
            // push this handler on top of current block enclosing handlers
            CurrentProtectingHandlers = FList<Handler>.Cons(eh, CurrentProtectingHandlers); // Push handler
          }
        }
        #endregion


        #region Record handlers for new block
        CurrentSubroutineWithHandler.ProtectingHandlers.Add(newBlock, this.CurrentProtectingHandlers);
        #endregion

        return newBlock;
      }


      private void ComputeTryBlockStartAndEndInfo(Method method)
      {

        foreach (Handler eh in this.CodeProvider.TryBlocks(method)) {

          if (this.CodeProvider.IsFilterHandler(eh)) {
            this.AddTargetLabel(CodeProvider.FilterDecisionStart(eh));
          }
          this.AddTargetLabel(CodeProvider.HandlerStart(eh));
          this.AddTargetLabel(CodeProvider.HandlerEnd(eh));

          AddTryStart(eh);
          AddTryEnd(eh);
          AddHandlerEnd(eh);
          this.handlerStartingAt.Add(CodeProvider.HandlerStart(eh), eh);
        }

      }

      private void AddTryStart(Handler eh)
      {
        Label tryStart = this.CodeProvider.TryStart(eh);
        Stack<Handler>/*?*/ starts;
        this.tryStartList.TryGetValue(tryStart, out starts);
        if (starts == null) {
          starts = new Stack<Handler>();
          tryStartList[tryStart] = starts;
        }
        starts.Push(eh);

        // also update block start
        this.AddTargetLabel(tryStart);
      }

      private void AddTryEnd(Handler eh)
      {
        Label tryEnd = this.CodeProvider.TryEnd(eh);
        Queue<Handler>/*?*/ ends;
        this.tryEndList.TryGetValue(tryEnd, out ends);
        if (ends == null) {
          ends = new Queue<Handler>();
          tryEndList[tryEnd] = ends;
        }
        ends.Enqueue(eh);

        // also update block start
        this.AddTargetLabel(tryEnd);
      }

      private void AddHandlerEnd(Handler eh)
      {
        if (!this.IsFaultOrFinally(eh)) return;
        // only record subroutine handlers
        Label ehEnd = this.CodeProvider.HandlerEnd(eh);
        Queue<Handler>/*?*/ ends;
        this.subroutineHandlerEndList.TryGetValue(ehEnd, out ends);
        if (ends == null) {
          ends = new Queue<Handler>();
          this.subroutineHandlerEndList[ehEnd] = ends;
        }
        ends.Enqueue(eh);

        // also update block start
        this.AddTargetLabel(ehEnd);
      }

      public bool IsHandlerStart(Label label, out Handler handler)
      {
        return (this.handlerStartingAt.TryGetValue(label, out handler));
      }

      public bool IsFaultOrFinally(Handler handler)
      {
        return this.CodeProvider.IsFaultHandler(handler) || this.CodeProvider.IsFinallyHandler(handler);
      }

      public Queue<Handler> GetHandlerEnd(Label label)
      {
        Queue<Handler>/*?*/ result;
        this.subroutineHandlerEndList.TryGetValue(label, out result);
        return result;
      }

      public Queue<Handler> GetTryEnd(Label label)
      {
        Queue<Handler>/*?*/ result;
        this.tryEndList.TryGetValue(label, out result);
        return result;
      }

      public Stack<Handler> GetTryStart(Label label)
      {
        Stack<Handler>/*?*/ result;
        this.tryStartList.TryGetValue(label, out result);
        return result;
      }


    }

    internal class SimpleSubroutineBuilder<Label> : SubroutineBuilder<Label> {

      SubroutineBase<Label> currentSubroutine;

      public SimpleSubroutineBuilder(ICodeProvider<Label,Method,Type> codeProvider,
                                     IDecodeMSIL<Label,Local,Parameter,Method,Field,Type,Unit,Unit,Unit> msilDecoder,
                                     MethodCache<Local,Parameter,Type,Method,Field,Property,Attribute,Assembly> methodCache,
                                     Label entry
      )
        : base(codeProvider, msilDecoder, methodCache, entry)
      {
        base.Initialize(entry);
      }

      public BlockWithLabels<Label> BuildBlocks(Label entry, SubroutineBase<Label> subroutine)
      {
        this.currentSubroutine = subroutine;
        return base.BuildBlocks(entry);
      }

      protected override SubroutineBase<Label> CurrentSubroutine
      {
        get
        {
          if (this.currentOldSubroutine != null) return this.currentOldSubroutine;
          return this.currentSubroutine;
        }
      }

      protected OldValueSubroutine<Label> currentOldSubroutine;
      protected BlockWithLabels<Label> blockPriorToOld;

      protected override BlockWithLabels<Label> RecordInformationForNewBlock(Label currentLabel, BlockWithLabels<Label> previousBlock)
      {
        Label lastLabel;
        BlockWithLabels<Label> newBlock;
        if (previousBlock != null && previousBlock.HasLastLabel(out lastLabel) && endOldStart.Contains(lastLabel))
        {
          // end the old subroutine here.
          var endingOldSubroutine = this.currentOldSubroutine;
          endingOldSubroutine.Commit(previousBlock);

          // now change the context to previous subroutine (ensures)
          this.currentOldSubroutine = null;
          Debug.Assert(this.CurrentSubroutine.IsEnsures);
          // We need to fall into the new block from the block prior to the old subroutine
          Debug.Assert(blockPriorToOld.Subroutine == this.CurrentSubroutine);
          newBlock = base.RecordInformationForNewBlock(currentLabel, blockPriorToOld);
          Debug.Assert(newBlock.Subroutine == this.CurrentSubroutine);
          this.CurrentSubroutine.AddEdgeSubroutine(blockPriorToOld, newBlock, endingOldSubroutine, "old");
          return newBlock;
        }
        if (beginOldStart.Contains(currentLabel))
        {
          // start of an old subroutine
          Debug.Assert(this.currentOldSubroutine == null);
          this.currentOldSubroutine = new OldValueSubroutine<Label>(this.MethodCache, ((CallingContractSubroutine<Label>)this.currentSubroutine).Method, this, currentLabel);
          this.blockPriorToOld = previousBlock;

          // don't fall into this block
          newBlock = base.RecordInformationForNewBlock(currentLabel, null);
          this.currentOldSubroutine.RegisterBeginBlock(newBlock);
          return newBlock;
        }
        newBlock = base.RecordInformationForNewBlock(currentLabel, previousBlock);

        return newBlock;
      }

      ISet<Label> beginOldStart = new Set<Label>();
      ISet<Label> endOldStart = new Set<Label>();

      internal override void BeginOldHook(Label label)
      {
        beginOldStart.Add(label);
      }

      internal override void EndOldHook(Label label)
      {
        endOldStart.Add(label);
      }
    }

    
    /// <summary>
    /// CFG blocks group consecutive instructions/code fragments to reduce the number of edges
    /// in the graph. A Block has a single entry point, meaning all control transfers go to the head
    /// never to an interior label. The only control transfer out of the block is at the last instruction.
    /// </summary>
    /// <typeparam name="Label"></typeparam>
    public class BlockWithLabels<Label> : CFGBlock, IEquatable<BlockWithLabels<Label>> {

      #region Private/Internal
      private static Label[] EmptyLabels = new Label[0];

      readonly List<Label> Labels;


      internal BlockWithLabels(SubroutineBase<Label> container, ref int idGen)
        : base(container, ref idGen)
      {
        Labels = new List<Label>();
        this.subroutine = container;
      }

      internal void Add(Label label)
      {
        Labels.Add(label);
      }

      #endregion

      internal readonly SubroutineBase<Label> subroutine;

      #region public API
      public override int Count
      {
        get { return this.Labels.Count; }
      }

      internal bool HasLastLabel(out Label label)
      {
        if (this.Labels.Count > 0)
        {
          label = this.Labels[this.Labels.Count - 1];
          return true;
        }
        label = default(Label);
        return false;
      }

      internal virtual bool UnderlyingLabelForward(int index, out Label/*?*/ label)
      {
        if (index < this.Labels.Count) {
          label = this.Labels[index];
          return true;
        }
        else {
          label = default(Label);
          return false;
        }
      }

      public override string SourceAssertionCondition(APC pc)
      {
        Label label;
        if (this.UnderlyingLabelForward(pc.Index, out label))
        {
          return this.subroutine.SourceAssertionCondition(label);
        }
        return null;
      }

      public override string SourceContext(APC pc)
      {
        Label label;
        if (this.UnderlyingLabelForward(pc.Index, out label)) {
          return this.subroutine.SourceContext(label);
        }
        return null;
      }

      public override string SourceDocument(APC pc)
      {
        Label label;
        if (this.UnderlyingLabelForward(pc.Index, out label)) {
          return this.subroutine.SourceDocument(label);
        }
        return null;
      }

      public override int SourceStartLine(APC pc)
      {
        Label label;
        if (this.UnderlyingLabelForward(pc.Index, out label)) {
          return this.subroutine.SourceStartLine(label);
        }
        return 0;
      }

      public override int SourceEndLine(APC pc)
      {
        Label label;
        if (this.UnderlyingLabelForward(pc.Index, out label)) {
          return this.subroutine.SourceEndLine(label);
        }
        return 0;
      }

      public override int SourceStartColumn(APC pc)
      {
        Label label;
        if (this.UnderlyingLabelForward(pc.Index, out label)) {
          return this.subroutine.SourceStartColumn(label);
        }
        return 0;
      }

      public override int SourceEndColumn(APC pc)
      {
        Label label;
        if (this.UnderlyingLabelForward(pc.Index, out label)) {
          return this.subroutine.SourceEndColumn(label);
        }
        return 0;
      }

      public override int ILOffset(APC pc)
      {
        Label label;
        if (this.UnderlyingLabelForward(pc.Index, out label)) {
          return this.subroutine.ILOffset(label);
        }
        return 0;
      }

      internal override Result ForwardDecode<Local2, Parameter2, Method2, Field2, Type2, Data, Result, Visitor>(APC pc, Visitor visitor, Data data)
      {
        Label/*?*/ lab;
        if (this.UnderlyingLabelForward(pc.Index, out lab)) {
          IDecodeMSIL<Label, Local2, Parameter2, Method2, Field2, Type2, Unit, Unit, Unit> decoder = (IDecodeMSIL<Label, Local2, Parameter2, Method2, Field2, Type2, Unit, Unit, Unit>)this.subroutine.Decoder;
          return decoder.ForwardDecode<Data, Result, LabelAdapter<Local2, Parameter2, Method2, Field2, Type2, Data, Result, Visitor>>(lab, new LabelAdapter<Local2, Parameter2, Method2, Field2, Type2, Data, Result, Visitor>(visitor, pc), data);
        }
        else {
          return visitor.Nop(pc, data);
        }
      }

      #region APC -> Label adapter visitor

      protected struct LabelAdapter<Local2, Parameter2, Method2, Field2, Type2, Data, Result, Visitor> :
        IVisitMSIL<Label, Local2, Parameter2, Method2, Field2, Type2, UnitSource, UnitDest, Data, Result>
        where Visitor : IVisitMSIL<APC, Local2, Parameter2, Method2, Field2, Type2, UnitSource, UnitDest, Data, Result> {
        APC originalPC;
        Visitor visitor;

        public LabelAdapter(
          Visitor visitor,
          APC origPC
        )
        {
          this.visitor = visitor;
          this.originalPC = origPC;
        }

        APC ConvertLabel(Label pc)
        {
          // we return the stored pc here, as the underlying decoder only dealt with a label
          // this works because we never translate branch target labels.
          return this.originalPC;
        }

        /// <summary>
        /// Map a label occurring in a begin_old/end_old to an apc.
        /// </summary>
        private APC ConvertMatchingBeginLabel(Label underlying)
        {
          OldValueSubroutine<Label> subroutine = (OldValueSubroutine<Label>)this.originalPC.Block.Subroutine;

          return subroutine.BeginOldAPC(this.originalPC.SubroutineContext);
        }
        private APC ConvertMatchingEndLabel(Label underlying)
        {
          OldValueSubroutine<Label> subroutine = (OldValueSubroutine<Label>)this.originalPC.Block.Subroutine;

          return subroutine.EndOldAPC(this.originalPC.SubroutineContext);
        }

        public Result Assume(Label pc, string tag, UnitSource source, Data data)
        {
          return visitor.Assume(ConvertLabel(pc), tag, source, data);
        }

        public Result Assert(Label pc, string tag, UnitSource source, Data data)
        {
          return visitor.Assert(ConvertLabel(pc), tag, source, data);
        }

        public Result Arglist(Label pc, UnitDest dest, Data data)
        {
          return visitor.Arglist(ConvertLabel(pc), dest, data);
        }

        public Result Binary(Label pc, BinaryOperator op, UnitDest dest, UnitSource s1, UnitSource s2, Data data)
        {
          return visitor.Binary(ConvertLabel(pc), op, dest, s1, s2, data);
        }

        public Result BranchCond(Label pc, Label target, BranchOperator bop, UnitSource value1, UnitSource value2, Data data)
        {
          return visitor.BranchCond(ConvertLabel(pc), ConvertLabel(target), bop, value1, value2, data);
        }

        public Result BranchTrue(Label pc, Label target, UnitSource cond, Data data)
        {
          return visitor.BranchFalse(ConvertLabel(pc), ConvertLabel(target), cond, data);
        }

        public Result BranchFalse(Label pc, Label target, UnitSource cond, Data data)
        {
          return visitor.BranchFalse(ConvertLabel(pc), ConvertLabel(target), cond, data);
        }

        public Result Branch(Label pc, Label target, bool leave, Data data)
        {
          return visitor.Branch(ConvertLabel(pc), ConvertLabel(target), leave, data);
        }

        public Result Break(Label pc, Data data)
        {
          return visitor.Break(ConvertLabel(pc), data);
        }

        public Result Call<TypeList, ArgList>(Label pc, Method2 method, bool tail, bool virt, TypeList extraVarargs, UnitDest dest, ArgList args, Data data)
          where TypeList : IIndexable<Type2>
          where ArgList : IIndexable<UnitSource>
        {
          return visitor.Call(ConvertLabel(pc), method, tail, virt, extraVarargs, dest, args, data);
        }

        public Result Calli<TypeList, ArgList>(Label pc, Type2 returnType, TypeList argTypes, bool tail, UnitDest dest, UnitSource fp, ArgList args, Data data)
          where TypeList : IIndexable<Type2>
          where ArgList : IIndexable<UnitSource>
        {
          return visitor.Calli(ConvertLabel(pc), returnType, argTypes, tail, dest, fp, args, data);
        }

        public Result Ckfinite(Label pc, UnitDest dest, UnitSource source, Data data)
        {
          return visitor.Ckfinite(ConvertLabel(pc), dest, source, data);
        }

        public Result Cpblk(Label pc, bool @volatile, UnitSource destaddr, UnitSource srcaddr, UnitSource len, Data data)
        {
          return visitor.Cpblk(ConvertLabel(pc), @volatile, destaddr, srcaddr, len, data);
        }

        public Result Endfilter(Label pc, UnitSource decision, Data data)
        {
          return visitor.Endfilter(ConvertLabel(pc), decision, data);
        }

        public Result Endfinally(Label pc, Data data)
        {
          return visitor.Endfinally(ConvertLabel(pc), data);
        }

        public Result Entry(Label pc, Method2 method, Data data)
        {
          return visitor.Entry(ConvertLabel(pc), method, data);
        }

        public Result Initblk(Label pc, bool @volatile, UnitSource destaddr, UnitSource value, UnitSource len, Data data)
        {
          return visitor.Initblk(ConvertLabel(pc), @volatile, destaddr, value, len, data);
        }

        public Result Jmp(Label pc, Method2 method, Data data)
        {
          return visitor.Jmp(ConvertLabel(pc), method, data);
        }

        /// <summary>
        /// Could be access to "this" in an invariant code sequence. Map to "this" in surrounding using method.
        /// </summary>
        public Result Ldarg(Label pc, Parameter2 argument, bool isOld, UnitDest dest, Data data)
        {
          return visitor.Ldarg(ConvertLabel(pc), argument, isOld, dest, data);
        }

        public Result Ldarga(Label pc, Parameter2 argument, bool isOld, UnitDest dest, Data data)
        {
          return visitor.Ldarga(ConvertLabel(pc), argument, isOld, dest, data);
        }

        public Result Ldconst(Label pc, object constant, Type2 type, UnitDest dest, Data data)
        {
          return visitor.Ldconst(ConvertLabel(pc), constant, type, dest, data);
        }

        public Result Ldnull(Label pc, UnitDest dest, Data data)
        {
          return visitor.Ldnull(ConvertLabel(pc), dest, data);
        }

        public Result Ldftn(Label pc, Method2 method, UnitDest dest, Data data)
        {
          return visitor.Ldftn(ConvertLabel(pc), method, dest, data);
        }

        public Result Ldind(Label pc, Type2 type, bool @volatile, UnitDest dest, UnitSource ptr, Data data)
        {
          return visitor.Ldind(ConvertLabel(pc), type, @volatile, dest, ptr, data);
        }

        public Result Ldloc(Label pc, Local2 local, UnitDest dest, Data data)
        {
          return visitor.Ldloc(ConvertLabel(pc), local, dest, data);
        }

        public Result Ldloca(Label pc, Local2 local, UnitDest dest, Data data)
        {
          return visitor.Ldloca(ConvertLabel(pc), local, dest, data);
        }

        public Result Ldstack(Label pc, int offset, UnitDest dest, UnitSource source, bool isOld, Data data)
        {
          return visitor.Ldstack(ConvertLabel(pc), offset, dest, source, isOld, data);
        }

        public Result Ldstacka(Label pc, int offset, UnitDest dest, UnitSource source, Type2 type, bool isOld, Data data)
        {
          return visitor.Ldstacka(ConvertLabel(pc), offset, dest, source, type, isOld, data);
        }

        public Result Localloc(Label pc, UnitDest dest, UnitSource size, Data data)
        {
          return visitor.Localloc(ConvertLabel(pc), dest, size, data);
        }

        public Result Nop(Label pc, Data data)
        {
          return visitor.Nop(ConvertLabel(pc), data);
        }

        public Result Pop(Label pc, UnitSource source, Data data)
        {
          return visitor.Pop(ConvertLabel(pc), source, data);
        }

        public Result Return(Label pc, UnitSource source, Data data)
        {
          return visitor.Return(ConvertLabel(pc), source, data);
        }

        public Result Starg(Label pc, Parameter2 argument, UnitSource source, Data data)
        {
          return visitor.Starg(ConvertLabel(pc), argument, source, data);
        }

        public Result Stind(Label pc, Type2 type, bool @volatile, UnitSource ptr, UnitSource value, Data data)
        {
          return visitor.Stind(ConvertLabel(pc), type, @volatile, ptr, value, data);
        }

        public Result Stloc(Label pc, Local2 local, UnitSource source, Data data)
        {
          return visitor.Stloc(ConvertLabel(pc), local, source, data);
        }

        public Result Switch(Label pc, Type type, IEnumerable<Label> cases, UnitSource value, Data data)
        {
          return visitor.Nop(this.originalPC, data);
        }

        public Result Unary(Label pc, UnaryOperator op, bool overflow, bool unsigned, UnitDest dest, UnitSource source, Data data)
        {
          return visitor.Unary(ConvertLabel(pc), op, overflow, unsigned, dest, source, data);
        }

        public Result Box(Label pc, Type2 type, UnitDest dest, UnitSource source, Data data)
        {
          return visitor.Box(ConvertLabel(pc), type, dest, source, data);
        }

        public Result ConstrainedCallvirt<TypeList, ArgList>(Label pc, Method2 method, bool tail, Type2 constraint, TypeList extraVarargs, UnitDest dest, ArgList args, Data data)
          where TypeList : IIndexable<Type2>
          where ArgList : IIndexable<UnitSource>
        {
          return visitor.ConstrainedCallvirt(ConvertLabel(pc), method, tail, constraint, extraVarargs, dest, args, data);
        }

        public Result Castclass(Label pc, Type2 type, UnitDest dest, UnitSource obj, Data data)
        {
          return visitor.Castclass(ConvertLabel(pc), type, dest, obj, data);
        }

        public Result Cpobj(Label pc, Type2 type, UnitSource destptr, UnitSource srcptr, Data data)
        {
          return visitor.Cpobj(ConvertLabel(pc), type, destptr, srcptr, data);
        }

        public Result Initobj(Label pc, Type2 type, UnitSource ptr, Data data)
        {
          return visitor.Initobj(ConvertLabel(pc), type, ptr, data);
        }

        public Result Isinst(Label pc, Type2 type, UnitDest dest, UnitSource obj, Data data)
        {
          return visitor.Isinst(ConvertLabel(pc), type, dest, obj, data);
        }

        public Result Ldelem(Label pc, Type2 type, UnitDest dest, UnitSource array, UnitSource index, Data data)
        {
          return visitor.Ldelem(ConvertLabel(pc), type, dest, array, index, data);
        }

        public Result Ldelema(Label pc, Type2 type, bool @readonly, UnitDest dest, UnitSource array, UnitSource index, Data data)
        {
          return visitor.Ldelema(ConvertLabel(pc), type, @readonly, dest, array, index, data);
        }

        public Result Ldfld(Label pc, Field2 field, bool @volatile, UnitDest dest, UnitSource obj, Data data)
        {
          return visitor.Ldfld(ConvertLabel(pc), field, @volatile, dest, obj, data);
        }

        public Result Ldflda(Label pc, Field2 field, UnitDest dest, UnitSource obj, Data data)
        {
          return visitor.Ldflda(ConvertLabel(pc), field, dest, obj, data);
        }

        public Result Ldlen(Label pc, UnitDest dest, UnitSource array, Data data)
        {
          return visitor.Ldlen(ConvertLabel(pc), dest, array, data);
        }

        public Result Ldsfld(Label pc, Field2 field, bool @volatile, UnitDest dest, Data data)
        {
          return visitor.Ldsfld(ConvertLabel(pc), field, @volatile, dest, data);
        }

        public Result Ldsflda(Label pc, Field2 field, UnitDest dest, Data data)
        {
          return visitor.Ldsflda(ConvertLabel(pc), field, dest, data);
        }

        public Result Ldtypetoken(Label pc, Type2 type, UnitDest dest, Data data)
        {
          return visitor.Ldtypetoken(ConvertLabel(pc), type, dest, data);
        }

        public Result Ldfieldtoken(Label pc, Field2 field, UnitDest dest, Data data)
        {
          return visitor.Ldfieldtoken(ConvertLabel(pc), field, dest, data);
        }

        public Result Ldmethodtoken(Label pc, Method2 method, UnitDest dest, Data data)
        {
          return visitor.Ldmethodtoken(ConvertLabel(pc), method, dest, data);
        }

        public Result Ldvirtftn(Label pc, Method2 method, UnitDest dest, UnitSource obj, Data data)
        {
          return visitor.Ldvirtftn(ConvertLabel(pc), method, dest, obj, data);
        }

        public Result Mkrefany(Label pc, Type2 type, UnitDest dest, UnitSource obj, Data data)
        {
          return visitor.Mkrefany(ConvertLabel(pc), type, dest, obj, data);
        }

        public Result Newarray<ArgList>(Label pc, Type2 type, UnitDest dest, ArgList lengths, Data data)
          where ArgList : IIndexable<UnitSource>
        {
          return visitor.Newarray(ConvertLabel(pc), type, dest, lengths, data);
        }

        public Result Newobj<ArgList>(Label pc, Method2 ctor, UnitDest dest, ArgList args, Data data)
          where ArgList : IIndexable<UnitSource>
        {
          return visitor.Newobj(ConvertLabel(pc), ctor, dest, args, data);
        }

        public Result Refanytype(Label pc, UnitDest dest, UnitSource source, Data data)
        {
          return visitor.Refanytype(ConvertLabel(pc), dest, source, data);
        }

        public Result Refanyval(Label pc, Type2 type, UnitDest dest, UnitSource source, Data data)
        {
          return visitor.Refanyval(ConvertLabel(pc), type, dest, source, data);
        }

        public Result Rethrow(Label pc, Data data)
        {
          return visitor.Rethrow(ConvertLabel(pc), data);
        }

        public Result Sizeof(Label pc, Type2 type, UnitDest dest, Data data)
        {
          return visitor.Sizeof(ConvertLabel(pc), type, dest, data);
        }

        public Result Stelem(Label pc, Type2 type, UnitSource array, UnitSource index, UnitSource value, Data data)
        {
          return visitor.Stelem(ConvertLabel(pc), type, array, index, value, data);
        }

        public Result Stfld(Label pc, Field2 field, bool @volatile, UnitSource obj, UnitSource value, Data data)
        {
          return visitor.Stfld(ConvertLabel(pc), field, @volatile, obj, value, data);
        }

        public Result Stsfld(Label pc, Field2 field, bool @volatile, UnitSource value, Data data)
        {
          return visitor.Stsfld(ConvertLabel(pc), field, @volatile, value, data);
        }

        public Result Throw(Label pc, UnitSource exn, Data data)
        {
          return visitor.Throw(ConvertLabel(pc), exn, data);
        }

        public Result Unbox(Label pc, Type2 type, UnitDest dest, UnitSource obj, Data data)
        {
          return visitor.Unbox(ConvertLabel(pc), type, dest, obj, data);
        }

        public Result Unboxany(Label pc, Type2 type, UnitDest dest, UnitSource obj, Data data)
        {
          return visitor.Unboxany(ConvertLabel(pc), type, dest, obj, data);
        }


        #region IVisitSynthIL<Label,Method2,Source,Dest,Data,Result> Members


        public Result BeginOld(Label pc, Label matchingEnd, Data data)
        {
          // special treatment of beginold decoding in the context of an old manifestation.
          if (this.originalPC.InsideOldManifestation)
          {
            return visitor.Nop(ConvertLabel(pc), data);
          }

          return visitor.BeginOld(ConvertLabel(pc), ConvertMatchingEndLabel(matchingEnd), data);
        }

        public Result EndOld(Label pc, Label matchingBegin, Type2 type, UnitDest dest, UnitSource source, Data data)
        {
          // special treatment of beginold decoding is done in the stack decoder, as it 
          // needs to know the specialness of endOld to generate the appropriate copy
          return visitor.EndOld(ConvertLabel(pc), ConvertMatchingBeginLabel(matchingBegin), type, dest, source, data);
        }

        public Result Ldresult(Label pc, UnitDest dest, UnitSource source, Data data)
        {
          return visitor.Ldresult(ConvertLabel(pc), dest, source, data);
        }

        #endregion
      }


      #endregion

      #endregion


      /// <summary>
      /// For debugging
      /// </summary>
      //^ [Confined]
      public override string/*!*/ ToString()
      {
        return this.Index.ToString();
      }

      #region IEquatable<CFGBlock<Label>> Members

      //^ [StateIndependent]
      public bool Equals(BlockWithLabels<Label> other)
      {
        return this == other;
      }

      #endregion

    }

    internal class EnsuresBlock<Label> : BlockWithLabels<Label>
    {
      const int BeginOldMask = unchecked((int)0x80000000);
      const int EndOldMask = 0x40000000;
      const int Mask = unchecked((int)0xC0000000);

      /// <summary>
      /// The integers are used as follows:
      /// - if no Mask bits are set, then it's the index of a label in the underlying
      ///   label list
      /// - otherwise, the mask bits indicate if it is a begin old or an end old and
      ///   the remaining bits provide the index of the corresponding end/begin within
      ///   the corresponding block (which is stored on the subroutine).
      /// </summary>
      List<int> overridingLabels;

      public EnsuresBlock(SubroutineBase<Label> container, ref int idgen)
        : base(container, ref idgen)
      {
      }

      internal bool UsesOverriding { get { return this.overridingLabels != null; } }

      new EnsuresSubroutine<Label> Subroutine { get { return (EnsuresSubroutine<Label>)base.Subroutine; } }

      public override int Count
      {
        get
        {
          if (overridingLabels != null) return overridingLabels.Count;
          return base.Count;
        }
      }

      bool IsOriginal(int index, out int originalOffset)
      {
        if (overridingLabels == null)
        {
          originalOffset = index;
          return true;
        }
        if (index < this.overridingLabels.Count && (this.overridingLabels[index] & Mask) == 0)
        {
          originalOffset = this.overridingLabels[index] & ~Mask;
          return true;
        }
        originalOffset = 0;
        return false;
      }

      bool IsBeginOld(int index, out int endOldIndex)
      {
        if (overridingLabels == null || index >= overridingLabels.Count)
        {
          endOldIndex = 0;
          return false;
        }
        if ((this.overridingLabels[index] & BeginOldMask) != 0)
        {
          endOldIndex = this.overridingLabels[index] & ~Mask;
          return true;
        }
        endOldIndex = 0;
        return false;
      }

      bool IsEndOld(int index, out int beginOldIndex)
      {
        if (overridingLabels == null || index >= overridingLabels.Count)
        {
          beginOldIndex = 0;
          return false;
        }
        if ((this.overridingLabels[index] & EndOldMask) != 0)
        {
          beginOldIndex = this.overridingLabels[index] & ~Mask;
          return true;
        }
        beginOldIndex = 0;
        return false;
      }

      internal override bool UnderlyingLabelForward(int index, out Label label)
      {
        int originalOffset;
        if (IsOriginal(index, out originalOffset))
        {
          return base.UnderlyingLabelForward(originalOffset, out label);
        }
        label = default(Label);
        return false;
      }


      internal Result OriginalForwardDecode<Data, Result, Visitor>(int index, Visitor visitor, Data data)
        where Visitor : IVisitMSIL<Label, Local, Parameter, Method, Field, Type, Unit, Unit, Data, Result>
      {
        Label/*?*/ lab;
        if (base.UnderlyingLabelForward(index, out lab)) {
          return this.subroutine.Decoder.ForwardDecode<Data,Result,Visitor>(lab, visitor, data);
        }
        throw new NotImplementedException();
      }

      internal override Result ForwardDecode<Local2, Parameter2, Method2, Field2, Type2, Data, Result, Visitor>(APC pc, Visitor visitor, Data data)
      {
        Label/*?*/ lab;
        if (this.UnderlyingLabelForward(pc.Index, out lab)) {
          IDecodeMSIL<Label, Local2, Parameter2, Method2, Field2, Type2, Unit, Unit, Unit> decoder = (IDecodeMSIL<Label, Local2, Parameter2, Method2, Field2, Type2, Unit, Unit, Unit>)this.subroutine.Decoder;
          return decoder.ForwardDecode<Data, Result, LabelAdapter<Local2, Parameter2, Method2, Field2, Type2, Data, Result, Visitor>>(lab, new LabelAdapter<Local2, Parameter2, Method2, Field2, Type2, Data, Result, Visitor>(visitor, pc), data);
        }
        int endOldIndex;
        if (IsBeginOld(pc.Index, out endOldIndex))
        {
          var endBlock = this.Subroutine.InferredBeginEndBijection(pc);
          return visitor.BeginOld(pc, new APC(endBlock, endOldIndex, pc.SubroutineContext), data);
        }
        int beginOldIndex;
        if (IsEndOld(pc.Index, out beginOldIndex))
        {
          Type2 endOldType;
          var beginBlock = this.Subroutine.InferredBeginEndBijection<Type2>(pc, out endOldType);
          return visitor.EndOld(pc, new APC(beginBlock, beginOldIndex, pc.SubroutineContext), endOldType, Unit.Value, Unit.Value, data);
        }
        return visitor.Nop(pc, data);
      }


      internal void StartOverridingLabels()
      {
        Debug.Assert(this.overridingLabels == null);
        this.overridingLabels = new List<int>();
      }

      internal void BeginOld(int index)
      {
        if (this.overridingLabels == null)
        {
          StartOverridingLabels();
          for (int i=0; i<index; i++) {
            this.overridingLabels.Add(i); // original instructions up to but not including index
          }
        }
        // temporary begin_old without corresponding end old index
        this.overridingLabels.Add(BeginOldMask);
      }

      internal void AddInstruction(int index)
      {
        // add original instruction
        this.overridingLabels.Add(index);
      }


      internal void EndOld(int index, Type nextEndOldType)
      {
        AddInstruction(index);
        EndOldWithoutInstruction(nextEndOldType);
      }

      internal void EndOldWithoutInstruction(Type nextEndOldType)
      {
        int endOldIndex = this.overridingLabels.Count;
        CFGBlock beginBlock;
        int correspondingBeginOldIndex = PatchPriorBeginOld(this, endOldIndex, out beginBlock);
        this.overridingLabels.Add(EndOldMask | correspondingBeginOldIndex);
        this.Subroutine.AddInferredOldMap(this.Index, endOldIndex, beginBlock, nextEndOldType);
      }

      private int PatchPriorBeginOld(CFGBlock endBlock, int endOldIndex, out CFGBlock beginBlock)
      {
        var startIndex = (this == endBlock) ? endOldIndex - 2 : this.Count - 1;
        for (int i = startIndex; i >= 0; i--)
        {
          int dummy;
          if (IsBeginOld(i, out dummy))
          {
            Debug.Assert(dummy == 0);
            overridingLabels[i] = BeginOldMask | endOldIndex;
            beginBlock = this;
            this.Subroutine.AddInferredOldMap(this.Index, i, endBlock, default(Type));
            return i;
          }
        }
        // if we get here the begin/end spans multiple blocks:
        var preds = this.subroutine.PredecessorBlocks(this).GetEnumerator();
        if (preds.MoveNext()) {
          var beginOldIndex = PatchPriorBeginOld(endBlock, endOldIndex, preds.Current, out beginBlock);
          var next = preds.MoveNext();
          Debug.Assert(!next);
          return beginOldIndex;
        }
        throw new InvalidOperationException("missing begin_old");
      }

      private static int PatchPriorBeginOld(CFGBlock endBlock, int endOldIndex, CFGBlock current, out CFGBlock beginBlock)
      {
        EnsuresBlock<Label> pred = current as EnsuresBlock<Label>;
        if (pred == null)
        {
          // skip this block
          // if we get here the begin/end spans multiple blocks:

          var preds = current.Subroutine.PredecessorBlocks(current).GetEnumerator();
          if (preds.MoveNext()) {
            var beginOldIndex = PatchPriorBeginOld(endBlock, endOldIndex, preds.Current, out beginBlock);
            bool next = preds.MoveNext();
            Debug.Assert(!next);
            return beginOldIndex;
          }
          throw new InvalidOperationException("missing begin_old");
        }
        return pred.PatchPriorBeginOld(endBlock, endOldIndex, out beginBlock);
      }
    }


    /// <summary>
    /// Used for separate blocks that call methods for which we have pre/post conditions
    /// </summary>
    internal class MethodCallBlock<Label> : BlockWithLabels<Label> {
      public readonly Method CalledMethod;
      public readonly bool Virtual;
      readonly int parameterCount;

      public MethodCallBlock(Method method, bool virtcall, IDecodeMetaData<Local,Parameter,Method,Field,Property,Type,Attribute,Assembly> mdDecoder, SubroutineBase<Label> container, ref int idgen)
        : base(container, ref idgen)
      {
        this.CalledMethod = method;
        this.Virtual = virtcall;
        this.parameterCount = mdDecoder.Parameters(method).Count;
      }

      internal override bool IsMethodCallBlock<Method2>(out Method2 calledMethod, out bool isNewObj, out bool isVirtual)
      {
        if (this.CalledMethod is Method2)
        {
          calledMethod = (Method2)(object)this.CalledMethod;
          isNewObj = this.IsNewObj;
          isVirtual = this.Virtual;
          return true;
        }
        calledMethod = default(Method2);
        isNewObj = false;
        isVirtual = false;
        return false;
      }

      internal virtual bool IsNewObj
      {
        get { return false; }
      }
    }


    internal class NewObjCallBlock<Label> : MethodCallBlock<Label>
    {
      public NewObjCallBlock(Method method, IDecodeMetaData<Local,Parameter,Method,Field,Property,Type,Attribute,Assembly> mdDecoder, SubroutineBase<Label> container, ref int idgen)
        : base(method, false, mdDecoder, container, ref idgen)
      {
      }

      internal override bool IsNewObj
      {
        get
        {
          return true;
        }
      }
    }

    internal class AssumeBlock<Label> : BlockWithLabels<Label>
    {
      public readonly Tag Tag;
      public readonly Label BranchLabel;
      public AssumeBlock(SubroutineBase<Label> container, Label label, string tag, ref int idgen)
        : base(container, ref idgen)
      {
        this.Tag = tag;
        this.BranchLabel = label;
      }

      public override int Count { get { return 1; } } // synthetic instruction



      #region ISelfDecode<Label,Method,Type> Members

      internal override Result ForwardDecode<Local2, Parameter2, Method2, Field2, Type2, Data, Result, Visitor>(APC pc, Visitor visitor, Data data)
      {
        if (pc.Index == 0) {
          return visitor.Assume(pc, this.Tag, Unit.Value, data);
        }
        else {
          return visitor.Nop(pc, data);
        }
      }

      #endregion
    }


    /// <summary>
    /// Encodes 3 synthetic instructions
    ///
    ///   ldc c
    ///   ceq
    ///   assume
    ///
    /// </summary>
    internal class SwitchCaseAssumeBlock<Label> : BlockWithLabels<Label>
    {
      readonly Label SwitchLabel;
      readonly object Pattern;
      readonly Type Type;

      public SwitchCaseAssumeBlock(SubroutineBase<Label> container, Label switchLabel, object pattern, Type type, ref int idgen)
        : base(container, ref idgen)
      {
        this.SwitchLabel = switchLabel;
        this.Pattern = pattern;
        this.Type = type;
      }

      public override int Count { get { return 3; } } // synthetic instructions


      internal override Result ForwardDecode<Local2, Parameter2, Method2, Field2, Type2, Data, Result, Visitor>(APC pc, Visitor visitor, Data data)
      {
        if (pc.Index == 0) {
          return visitor.Ldconst(pc, this.Pattern, (Type2)(object)this.Type, Unit.Value, data);
        }
        else if (pc.Index == 1) {
          return visitor.Binary(pc, BinaryOperator.Ceq, Unit.Value, Unit.Value, Unit.Value, data);
        }
        else if (pc.Index == 2) {
          return visitor.Assume(pc, "true", Unit.Value, data);
        }
        else {
          return visitor.Nop(pc, data);
        }

      }
    }

    /// <summary>
    /// Compares switched value against all constants c_i suing the following structure
    /// </summary>
    internal class SwitchDefaultAssumeBlock<Label> : BlockWithLabels<Label>
    {
      readonly Label SwitchLabel;
      readonly List<object> Patterns;
      readonly Type Type;

      public SwitchDefaultAssumeBlock(SubroutineBase<Label> container, Label label, List<object> patterns, Type type, ref int idgen)
        : base(container, ref idgen) {
        this.SwitchLabel = label;
        this.Patterns = patterns;
        this.Type = type;
      }

      public override int Count { get { return this.Patterns.Count*4 + 1; } } // synthetic instruction

      internal override Result ForwardDecode<Local2, Parameter2, Method2, Field2, Type2, Data, Result, Visitor>(APC pc, Visitor visitor, Data data)
      {
        int caseNum = pc.Index / 4;
        if (caseNum < this.Patterns.Count) {
          int withinInstr = pc.Index % 4;
          switch (withinInstr) {
            case 0:
              return visitor.Ldstack(pc, 0, Unit.Value, Unit.Value, false, data);
            case 1:
              return visitor.Ldconst(pc, this.Patterns[caseNum], (Type2)(object)this.Type, Unit.Value, data);
            case 2:
              return visitor.Binary(pc, BinaryOperator.Cne_Un, Unit.Value, Unit.Value, Unit.Value, data);
            case 3:
              return visitor.Assume(pc, "true", Unit.Value, data);
          }
        }
        else if (caseNum == this.Patterns.Count && pc.Index % 4 == 0) {
          // finally pop it.
          return visitor.Pop(pc, Unit.Value, data);
        }
        return visitor.Nop(pc, data);

      }

    }

    /// <summary>
    /// Special entry and exit block that has one program point for Entry synthetic instruction or Ret
    /// </summary>
    internal class EntryExitBlock<Label> : BlockWithLabels<Label>
    {
      public EntryExitBlock(SubroutineBase<Label> container, ref int idGen) : base(container, ref idGen) { }

      public override int Count { get { return 1; } } /// synthetic instruction
    }

    /// <summary>
    /// Special entry block that self decodes first instruction to Entry
    /// </summary>
    internal class EntryBlock<Label> : EntryExitBlock<Label>
    {
      public EntryBlock(SubroutineBase<Label> container, ref int idGen)
        : base(container, ref idGen) {
      }

      internal override Result ForwardDecode<Local2, Parameter2, Method2, Field2, Type2, Data, Result, Visitor>(APC pc, Visitor visitor, Data data)
      {
        if (pc.Index == 0 && pc.SubroutineContext == null && this.Subroutine.IsMethod) {
          IMethodInfo<Method2> ms = (IMethodInfo<Method2>)this.Subroutine;
          return visitor.Entry(pc, ms.Method, data);
        }
        return visitor.Nop(pc, data);
      }

    }

    /// <summary>
    /// Used to mark catch filter header blocks.
    /// </summary>
    internal class CatchFilterEntryBlock<Label> : BlockWithLabels<Label>
    {
      public CatchFilterEntryBlock(SubroutineBase<Label> container, ref int idgen)
        : base(container, ref idgen)
      {
      }
    }



    internal abstract class SubroutineBase<Label> : Subroutine, IGraph<CFGBlock, Unit>, IStackInfo
    {
      #region -------------- Fields ------------------------

      private readonly BlockWithLabels<Label> entry;
      private readonly BlockWithLabels<Label> exit;
      private readonly CatchFilterEntryBlock<Label> exceptionExit;
      protected readonly Label startLabel;
      private BlockWithLabels<Label> entryAfterRequires; // possibly null

      protected int blockIdGenerator = 0;

      /// <summary>
      /// Holds the map from labels that start a block to their block
      /// </summary>
      protected Dictionary<Label, BlockWithLabels<Label>> BlockStart = new Dictionary<Label, BlockWithLabels<Label>>();

      protected readonly MethodCache<Local, Parameter, Type, Method, Field, Property, Attribute, Assembly> MethodCache;
      protected SubroutineBuilder<Label> Builder;

      private readonly List<Pair<CFGBlock, Pair<string, CFGBlock>>> successors = new List<Pair<CFGBlock,Pair<string,CFGBlock>>>();

      protected readonly ICodeProvider<Label, Method, Type> CodeProvider;

      protected CFGBlock[] blocks;

      /// <summary>
      /// Stored in last to first order (thus easy to append)
      ///
      /// The string is a tag for what kind of call-edge it is.
      /// </summary>
      protected OnDemandMap<Pair<CFGBlock, CFGBlock>, FList<Pair<string, Subroutine>>> edgeSubroutines;

      #endregion // fields

      #region -------------- Private helpers --------------


      internal AssumeBlock<Label> NewAssumeBlock(Label current, Tag tag)
      {
        return new AssumeBlock<Label>(this, current, tag, ref this.blockIdGenerator);
      }

      internal SwitchCaseAssumeBlock<Label> NewSwitchCaseAssumeBlock(Label current, object pattern, Type type)
      {
        return new SwitchCaseAssumeBlock<Label>(this, current, pattern, type, ref this.blockIdGenerator);
      }

      internal SwitchDefaultAssumeBlock<Label> NewSwitchDefaultAssumeBlock(Label current, List<object> patterns, Type type)
      {
        return new SwitchDefaultAssumeBlock<Label>(this, current, patterns, type, ref this.blockIdGenerator);
      }

      #endregion -------------------------------------------

      #region ------------ Protected Helpers -------------

      internal virtual BlockWithLabels<Label> NewBlock()
      {
        return new BlockWithLabels<Label>(this, ref this.blockIdGenerator);
      }

      internal IDecodeMSIL<Label, Local, Parameter, Method, Field, Type, Unit, Unit, Unit> Decoder;

      protected SubroutineBase(
        MethodCache<Local, Parameter, Type, Method, Field, Property, Attribute, Assembly> methodCache
      )
      {
        this.MethodCache = methodCache;
        this.entry = new EntryBlock<Label>(this, ref this.blockIdGenerator);
        this.exit = new EntryExitBlock<Label>(this, ref this.blockIdGenerator);
        this.exceptionExit = new CatchFilterEntryBlock<Label>(this, ref this.blockIdGenerator);
      }
      protected SubroutineBase(MethodCache<Local, Parameter, Type, Method, Field, Property, Attribute, Assembly> methodCache,
                               Label startLabel,
                               SubroutineBuilder<Label> builder)
        : this(methodCache)
      {
        this.startLabel = startLabel;
        this.Builder = builder;
        this.CodeProvider = builder.CodeProvider;
        this.Decoder = builder.MSILDecoder;

        // link entry block to start label
        this.entryAfterRequires = this.GetTargetBlock(startLabel);
        AddSuccessor(this.entry, "entry", this.entryAfterRequires);
      }

      internal abstract override void Initialize();

      internal virtual void Commit() {
        Debug.Assert(blocks == null); // make sure we don't double commit

        // cleanup
        PostProcessBlocks();
      }

      internal void AddSuccessor(CFGBlock from, Tag tag, CFGBlock target)
      {
        Debug.Assert(from.Subroutine == target.Subroutine);
        this.AddNormalControlFlowEdge(this.successors, from, tag, target);
      }

      internal BlockWithLabels<Label> GetTargetBlock(Label label)
      {
        Debug.Assert(this.Builder.IsTargetLabel(label));
        return GetBlock(label);
      }

      internal BlockWithLabels<Label> GetBlock(Label label)
      {
        BlockWithLabels<Label> newBlock;
        if (!this.BlockStart.TryGetValue(label, out newBlock))
        {
#if ENABLE_ASSERTIONS
          Debug.Assert(this.MethodInfo.IsBlockStart(label));
#endif
          Pair<Method,bool> calledMethodVirtPair;
          Method calledMethod;
          if (this.Builder.IsMethodCallSite(label, out calledMethodVirtPair))
          {
            newBlock = new MethodCallBlock<Label>(calledMethodVirtPair.One, calledMethodVirtPair.Two, this.MethodCache.MetadataDecoder, this, ref this.blockIdGenerator);
          }
          else if (this.Builder.IsNewObjSite(label, out calledMethod))
          {
            newBlock = new NewObjCallBlock<Label>(calledMethod, this.MethodCache.MetadataDecoder, this, ref this.blockIdGenerator);
          }
          else
          {
            newBlock = this.NewBlock();
          }
          // only add branch targets to this map
          if (this.Builder.IsTargetLabel(label))
          {
            this.BlockStart.Add(label, newBlock);
          }
        }
        return newBlock;
      }


      /// <summary>
      /// Note that subroutine can be null, in which case we add nothing.
      /// </summary>
      sealed public override void AddEdgeSubroutine(CFGBlock from, CFGBlock to, Subroutine subroutine, string callTag)
      {
        if (subroutine == null) return;
        FList<Pair<string,Subroutine>> sofar;
        Pair<CFGBlock,CFGBlock> edge = new Pair<CFGBlock,CFGBlock>(from, to);
        this.edgeSubroutines.TryGetValue(edge, out sofar);
        this.edgeSubroutines[edge] = FList<Pair<string,Subroutine>>.Cons(new Pair<string,Subroutine>(callTag,subroutine), sofar);
      }

      /// <summary>
      /// Returns registered edge subroutines in last to first order.
      ///
      /// The context is used to filter subroutines on this list as follows:
      /// - Any contract subroutines returned are distinct from the current subroutine and any subroutines in the context
      /// </summary>
      FList<Pair<string, Subroutine>> GetOrdinaryEdgeSubroutinesInternal(CFGBlock from, CFGBlock to, SubroutineContext context)
      {
        FList<Pair<string,Subroutine>> sofar;
        Pair<CFGBlock, CFGBlock> edge = new Pair<CFGBlock, CFGBlock>(from, to);
        this.edgeSubroutines.TryGetValue(edge, out sofar);
        if (sofar != null && context != null)
        {
          sofar = sofar.Filter(FilterRecursiveContracts(to, context));
        }
        return sofar;
      }

      /// <summary>
      /// Used to specialize ensures subroutines based on calling context.
      /// We are looking for a stack of contract calls (ensures/requires) where each call is on "this" except maybe 
      /// the outermost one. Since the calls are on "this", we can use the declaring type of the called method as
      /// a possible improvement of the dynamic type other than the final declaring type. E.g., the ensures about to be
      /// inserted might be the ensures of ICollection.Count, called from within ICollection.CopyTo's requires.
      /// But the call to CopyTo happens to be a call to Array.CopyTo (inheriting ICollection.CopyTo's requires). Thus,
      /// we can determine that the Count ensures to be used is the one from Array, which specializes ICollection.Count's 
      /// ensures to relate the Count to the array length.
      /// </summary>
      internal FList<Pair<string, Subroutine>> GetOrdinaryEdgeSubroutines(CFGBlock from, CFGBlock to, FList<Tuple<CFGBlock, CFGBlock, string>> context)
      {
        CallAdaption.Push(this);
        try
        {
          var result = GetOrdinaryEdgeSubroutinesInternal(from, to, context);

          if (result != null && context != null)
          {
            var mdDecoder = this.MethodCache.MetadataDecoder;
            Method calledMethod;
            bool isNewObj;
            bool isVirtualCall;
            if (from.IsMethodCallBlock(out calledMethod, out isNewObj, out isVirtualCall) && isVirtualCall)
            {
              // try to specialize any ensures based on calling context

              // find out if call is on "this" (using Stack provider information)
              if (CallAdaption.Dispatch<IStackInfo>(this).IsCallOnThis(new APC(from, 0, null)))
              {
                Type bestType = mdDecoder.DeclaringType(calledMethod); // so far
                // find best declaring type on outer calls and use it to specialize ensures.
                do
                {
                  // this is really a do loop (as we tested for context != null on the outside
                  if (context.Head.Three == "extra") { context = context.Tail; continue; }
                  Method outerCalledMethod;
                  bool dummy;
                  bool dummy2;

                  if (context.Head.Three.StartsWith("after") && context.Head.One.IsMethodCallBlock(out outerCalledMethod, out dummy, out dummy2))
                  {
                    Type candidateType = mdDecoder.DeclaringType(outerCalledMethod);
                    if (mdDecoder.DerivesFrom(candidateType, bestType))
                    {
                      bestType = candidateType;
                    }
                    // we can try for even better types if the outer call is also on this
                    if (!CallAdaption.Dispatch<IStackInfo>(this).IsCallOnThis(new APC(context.Head.One, 0, null)))
                    {
                      break; // get out of do loop
                    }
                  }
                  else if (context.Head.Three.StartsWith("before") && context.Head.Two.IsMethodCallBlock(out outerCalledMethod, out dummy, out dummy2))
                  {
                    Type candidateType = mdDecoder.DeclaringType(outerCalledMethod);
                    if (mdDecoder.DerivesFrom(candidateType, bestType))
                    {
                      bestType = candidateType;
                    }
                    // we can try for even better types if the outer call is also on this
                    if (!CallAdaption.Dispatch<IStackInfo>(this).IsCallOnThis(new APC(context.Head.Two, 0, null)))
                    {
                      break; // get out of do loop
                    }
                  }
                  else
                  {
                    // no specialization possible
                    return result;
                  }
                  // loop around
                  context = context.Tail;
                } while (context != null);
                // if we get here, we may have found a better type:
                if (!mdDecoder.Equal(bestType, mdDecoder.DeclaringType(calledMethod)))
                {
                  result = SpecializeEnsures(result, bestType);
                }
              }
            }
          }
          return result;
        }
        finally
        {
          CallAdaption.Pop(this);
        }
      }

      FList<Pair<string, Subroutine>> SpecializeEnsures(FList<Pair<string, Subroutine>> subs, Type minimalType)
      {
        return subs.Map(pair => new Pair<string, Subroutine>(pair.One, SpecializeEnsures(pair.Two, minimalType)));
      }

      Subroutine SpecializeEnsures(Subroutine sub, Type minimalType)
      {
        var mdDecoder = this.MethodCache.MetadataDecoder;
        if (!sub.IsEnsures) return sub;
        IMethodInfo<Method> im = sub as IMethodInfo<Method>;
        if (im != null)
        {
          Type ensuresDeclaringType = mdDecoder.DeclaringType(im.Method);
          if (!mdDecoder.Equal(minimalType, ensuresDeclaringType) && mdDecoder.DerivesFrom(minimalType, mdDecoder.DeclaringType(im.Method)))
          {
            // strictly better type
            // find corresponding method in this type and its ensures
            Method specializedMethod;
            if (mdDecoder.TryGetImplementingMethod(minimalType, im.Method, out specializedMethod))
            {
              // now find it's ensures
              return this.MethodCache.GetEnsures(specializedMethod);
            }
          }
        }
        return sub;
      }

      bool IStackInfo.IsCallOnThis(APC pc)
      {
        // default implementation does not try to refine
        return false;
      }

      static Predicate<Pair<string, Subroutine>> FilterRecursiveContracts(CFGBlock from, SubroutineContext context)
      {
        return delegate(Pair<string, Subroutine> candidatePair)
        {
          var candidate = candidatePair.Two;
          if (!candidate.IsContract) return true;
          if (candidate == from.Subroutine) return false; // direct recursion
          for (var filter = context; filter != null; filter = filter.Tail)
          {
            if (candidate == filter.Head.One.Subroutine) return false;
          }
          return true;
        };
      }

      /// <summary>
      /// Call back when a return statement is found. Usually ignored but MethodSubroutine wants to know about it
      /// </summary>
      internal virtual void AddReturnBlock(BlockWithLabels<Label> block) { }

      #endregion ------------------------------------


      public override CFGBlock Entry
      {
        get { return this.entry; }
      }

      public override CFGBlock EntryAfterRequires
      {
        get { if (this.entryAfterRequires != null) { return this.entryAfterRequires; } else { return this.Entry; } }
      }

      public override CFGBlock Exit
      {
        get { return this.exit; }
      }

      public override CFGBlock ExceptionExit
      {
        get { return this.exceptionExit; }
      }

      public override bool HasReturnValue
      {
        get { return false; } // default
      }

      public override bool HasContextDependentStackDepth
      {
        get { return true; } // default
      }

      public override IEnumerable<CFGBlock> SuccessorBlocks(CFGBlock block)
      {
        foreach (Pair<Tag, CFGBlock> edge in this.SuccessorEdges[block])
        {
          yield return edge.Two;
        }
      }

      public override IEnumerable<CFGBlock> PredecessorBlocks(CFGBlock block)
      {
        foreach (Pair<Tag, CFGBlock> edge in this.PredecessorEdges[block])
        {
          yield return edge.Two;
        }
      }

      public override bool IsCatchFilterHeader(CFGBlock block)
      {
        return block is CatchFilterEntryBlock<Label>;
      }

      /// <summary>
      /// Number of distinct syntactic blocks.
      /// </summary>
      public override int BlockCount { get { return this.blocks.Length; } }

      /// <summary>
      /// The blocks are numbered from 0-n. For syntactic analysis (ignoring finally context)
      /// an array can be used to map from block to information by using the block.index as key.
      /// </summary>
      public override IEnumerable<CFGBlock> Blocks { get { return this.blocks; } }

      public override string Name
      {
        get { return "SR" + this.Id.ToString(); }
      }

      #region ------------ Internal overrides ------------------


      internal override bool IsSubroutineStart(CFGBlock block)
      {
        return block == this.entry;
      }

      internal override bool IsSubroutineEnd(CFGBlock block)
      {
        return block == this.exit || block == this.exceptionExit;
      }

      internal override bool IsJoinPoint(CFGBlock block)
      {
        // catch and filter handler starts are join points
        if (this.IsCatchFilterHeader(block)) return true;
        // prevents us from stepping in/out of straight line code across subroutines
        if (this.IsSubroutineStart(block)) return true;
        if (this.IsSubroutineEnd(block)) return true;
        return (this.PredecessorEdges[block].Count > 1);
      }

      internal override bool IsSplitPoint(CFGBlock block)
      {
        // prevents us from stepping in/out of straight line code across subroutines
        if (this.IsSubroutineStart(block)) return true;
        if (this.IsSubroutineEnd(block)) return true;
        return (SuccessorEdges[block].Count > 1);
      }

      internal override DepthFirst.Visitor<CFGBlock, Unit>/*?*/ EdgeInfo
      {
        get { return this.edgeInfo; }
      }

      internal string SourceContext(Label label)
      {
        if (this.CodeProvider.HasSourceContext(label))
        {
          return String.Format("{0}({1},{2})", this.CodeProvider.SourceDocument(label), this.CodeProvider.SourceStartLine(label), this.CodeProvider.SourceStartColumn(label));
        }
        return null;
      }
      internal string SourceDocument(Label label)
      {
        if (this.CodeProvider.HasSourceContext(label))
        {
          return this.CodeProvider.SourceDocument(label);
        }
        return null;
      }
      internal int SourceStartLine(Label label)
      {
        if (this.CodeProvider.HasSourceContext(label))
        {
          return this.CodeProvider.SourceStartLine(label);
        }
        return 0;
      }
      internal int SourceEndLine(Label label)
      {
        if (this.CodeProvider.HasSourceContext(label))
        {
          return this.CodeProvider.SourceEndLine(label);
        }
        return 0;
      }
      internal int SourceStartColumn(Label label)
      {
        if (this.CodeProvider.HasSourceContext(label))
        {
          return this.CodeProvider.SourceStartColumn(label);
        }
        return 0;
      }
      internal int SourceEndColumn(Label label)
      {
        if (this.CodeProvider.HasSourceContext(label))
        {
          return this.CodeProvider.SourceEndColumn(label);
        }
        return 0;
      }

      internal int ILOffset(Label label)
      {
        return this.CodeProvider.ILOffset(label);
      }

      internal override int StackDelta
      {
        get { return 0; }
      }

      #endregion --------------------------------------

      #region Code scanning and block building



      private void AddNormalControlFlowEdge(List<Pair<CFGBlock, Pair<string, CFGBlock>>> succs,
                                            CFGBlock from, string tag, CFGBlock to)
      {
        Debug.Assert(from != this.Exit);
        succs.Add(new Pair<CFGBlock, Pair<string, CFGBlock>>(from, new Pair<string, CFGBlock>(tag, to)));
      }


      #endregion

      #region Subroutine computations
      internal override bool HasSingleSuccessor(APC ppoint, out APC next, DConsCache consCache)
      {
        //
        // This works as follows:
        //  1) determine if we are at the end of a block, if not, just provide the successor
        //  2) if this point ends a finally handler, find the next handler or pop the stack
        //  3) determine syntactic successor labels
        //     a) for each such label, determine finally nesting difference with current finally scope
        //     b) push executed finallies onto finally stack and make top be the new current PC
        // If this is an endfinally, there must be at least one label

        // We allow stepping past the last instruction in this block!
        if (ppoint.Index < ppoint.Block.Count)
        {
          next = new APC(ppoint.Block, ppoint.Index + 1, ppoint.SubroutineContext);
          return true;
        }

        if (IsSubroutineEnd(ppoint.Block))
        {
          // Could be end of entire CFG
          if (ppoint.SubroutineContext == null)
          {
            next = APC.Dummy;
            return false;
          }
          next = ComputeSubroutineContinuation(ppoint, consCache);
          return true;
        }

        BlockWithLabels<Label> singleSucc = null;
        foreach (BlockWithLabels<Label> succ in ppoint.Block.Subroutine.SuccessorBlocks(ppoint.Block))
        {
          if (singleSucc == null)
          {
            singleSucc = succ;
          }
          else
          {
            // more than one
            next = APC.Dummy;
            return false;
          }
        }
        if (singleSucc != null)
        {
          next = ComputeTargetFinallyContext(ppoint, singleSucc, consCache);
          return true;
        }

        // zero
        next = APC.Dummy;
        return false;
      }

      internal override bool HasSinglePredecessor(APC ppoint, out APC singlePredecessor, DConsCache consCache)
      {
        //
        // This works as follows:
        //  1) determine if we are at the beginning of a block, if not, just provide the predecessor
        //  2) if this point starts a finally handler, find the previous handler or pop the stack
        //     NOTE: if the popping is necessary and the target of the edge is a catch handler, then
        //           the predecessors are all indices in the block where the exception is thrown.
        //  3) determine syntactic predecessors
        //     a) for each such label, determine finally nesting difference with current finally scope
        //     b) push edge onto finally stack and make top be the new current PC
        if (ppoint.Index > 0)
        {
          singlePredecessor = new APC(ppoint.Block, ppoint.Index - 1, ppoint.SubroutineContext);
          return true;
        }

        // If this is the beginning of the subroutine, get the next one
        if (IsSubroutineStart(ppoint.Block))
        {
          // could be beginning of entire CFG
          if (ppoint.SubroutineContext == null)
          {
            singlePredecessor = APC.Dummy;
            return false;
          }
          bool hasSinglePred;
          singlePredecessor = this.ComputeSubroutinePreContinuation(ppoint, out hasSinglePred, consCache);
          return hasSinglePred;
        }

        CFGBlock/*?*/ singlePred = null;
        foreach (CFGBlock pred in ppoint.Block.Subroutine.PredecessorBlocks(ppoint.Block))
        {
          if (singlePred != null)
          {
            singlePredecessor = APC.Dummy;
            return false;
          }
          singlePred = pred;
        }
        if (singlePred == null)
        {
          // 0
          singlePredecessor = APC.Dummy;
          return false;
        }

        FList<Pair<string,Subroutine>> diffs = EdgeSubroutinesOuterToInner(singlePred, ppoint.Block, ppoint.SubroutineContext);

        if (diffs == null)
        {
          singlePredecessor = APC.ForEnd(singlePred, ppoint.SubroutineContext);
          return true;
        }

        // now diff is in outermost to inner most order of fault/finallies we are traversing.
        // 1) push edge onto fault/finally context stack
        // 2) for each endfinally in innermost handler, yield a program point
        // IMPORTANT: the Finally context must be hash consed so that APC's have Equal working properly!
        //
        SubroutineContext newFinallyContext = consCache(new SubroutineEdge(singlePred, ppoint.Block, diffs.Head.One), ppoint.SubroutineContext);

        // 3) find outermost handler (Head) and its subroutine
        // 4) transfer control to end of first (outermost) fault/finally
        //     (NOTE: diffs only contains finallies unless ppoint.Block is a catch/filter header)

        Subroutine subroutine = diffs.Head.Two;

        singlePredecessor = APC.ForEnd(subroutine.Exit, newFinallyContext);
        return true;
      }

      internal override APC PredecessorPCPriorToRequires(APC pc, DConsCache consCache)
      {
        Debug.Assert(pc.Index == 0); // must be at head of block
        var currBlock = pc.Block;
        Method dummy;
        bool dummyBool;
        bool dummyIsVirtual;
        Debug.Assert(currBlock.IsMethodCallBlock(out dummy, out dummyBool, out dummyIsVirtual));
        var predBlock = this.PredecessorBlocks(currBlock).AsIndexable(1)[0];

        var subs = EdgeSubroutinesOuterToInner(predBlock, currBlock, pc.SubroutineContext);

        for (;  subs != null; subs = subs.Tail)
        {
          var sub = subs.Head.Two;
          if (sub.IsRequires) continue;
          // non requires sub could be ensures/invariant, etc. Need to use this as last pc
          SubroutineContext newFinallyContext = consCache(new SubroutineEdge(predBlock, currBlock, subs.Head.One), pc.SubroutineContext);
          return APC.ForEnd(sub.Exit, newFinallyContext);
        }
        return APC.ForEnd(predBlock, pc.SubroutineContext);
      }

      /// <summary>
      /// Returns all normal control flow successors
      /// </summary>
      internal override IEnumerable<APC>/*!*/ Successors(APC ppoint, DConsCache consCache)
      {

        APC singleNext;
        if (HasSingleSuccessor(ppoint, out singleNext, consCache))
        {
          yield return singleNext;
          yield break;
        }
        //
        //  determine syntactic successor labels
        //     a) for each such label, determine finally nesting difference with current finally scope
        //     b) push executed finallies onto finally stack and make top be the new current PC

        int successors = 0;
        foreach (BlockWithLabels<Label> succ in ppoint.Block.Subroutine.SuccessorBlocks(ppoint.Block))
        {
          successors++;
          yield return ppoint.Block.Subroutine.ComputeTargetFinallyContext(ppoint, succ, consCache);
        }

        if (successors == 0)
        {
          // The following is too strong, as throw in a finally block aborts the finally continuation
          // System.Diagnostics.Debug.Assert(ppoint.FaultFinallyContext == null);

          // the following is too strong, as the current pc could also be a throw.
          // However, we don't know how to compute this.
          // System.Diagnostics.Debug.Assert(ppoint.Block == this.normalExit || ppoint.Block == this.exceptionExit);
        }
      }

      internal override IEnumerable<APC> Predecessors(APC ppoint, DConsCache consCache)
      {
        //
        // This works as follows:
        //  1) determine if we are at the beginning of a block, if not, just provide the predecessor
        //  2) if this point starts a finally handler, find the previous handler or pop the stack
        //  3) determine syntactic predecessors
        //     a) for each such label, determine finally nesting difference with current finally scope
        //     b) push edge onto finally stack and make top be the new current PC
        if (ppoint.Index > 0)
        {
          yield return new APC(ppoint.Block, ppoint.Index - 1, ppoint.SubroutineContext);
          yield break;
        }

        if (IsSubroutineStart(ppoint.Block))
        {
          // Could be start of entire CFG
          if (ppoint.SubroutineContext == null)
          {
            yield break;
          }
          // 
          foreach (APC nestedpred in this.ComputeSubroutinePreContinuation(ppoint, consCache))
          {
            yield return nestedpred;
          }
          yield break;
        }

        foreach (CFGBlock pred in ppoint.Block.Subroutine.PredecessorBlocks(ppoint.Block))
        {
          FList<Pair<string,Subroutine>> diffs = EdgeSubroutinesOuterToInner(pred, ppoint.Block, ppoint.SubroutineContext);

          if (diffs == null)
          {
            yield return APC.ForEnd(pred, ppoint.SubroutineContext);
          }
          else
          {
            // now diff is in outermost to inner most order of fault/finallies we are traversing.
            // 1) push edge onto fault/finally context stack
            // 2) yield exit of subroutine as program point
            // IMPORTANT: the Finally context must be hash consed so that APC's have Equal working properly!
            //
            SubroutineContext newFinallyContext = consCache(new SubroutineEdge(pred, ppoint.Block, diffs.Head.One), ppoint.SubroutineContext);

            // 2) find outermost handler (Head)
            // 3) transfer control to end of first (outermost) fault/finally
            Subroutine subroutine = diffs.Head.Two;
            yield return APC.ForEnd(subroutine.Exit, newFinallyContext);
          }
        }
      }

      /// <summary>
      /// Called when ppoint is beginning of a subroutine on backwards walk.
      ///
      /// Finds next subroutine on current edge, or pops edge
      ///
      /// Note: we distinguish between edges that target an exception handler and thus need
      /// to execute fault and finally handlers and other edges that only execute finally handlers
      /// by the target block. In the former case, the target block is a SpecialCatchFilterHeader
      /// block. These blocks are needed, as the start of a catch handler could also be the normal
      /// goto/leave target of some try finally within the catch handler itself. Thus, the special
      /// block disinguishes between a throw target where faults must be traversed, and a normal
      /// leave target to the beginning of a catch handler.
      /// </summary>
      private IEnumerable<APC> ComputeSubroutinePreContinuation(APC ppoint, DConsCache consCache)
      // ^ requires ppoint.FinallyEdges != null;
      {
        SubroutineEdge edge = ppoint.SubroutineContext.Head;
        bool isHandlerEdge;
        FList<Pair<string,Subroutine>> diffs = EdgeSubroutinesOuterToInner(edge.One, edge.Two, out isHandlerEdge, ppoint.SubroutineContext);
        Debug.Assert(diffs != null);
        while (diffs.Head.Two != this)
        {
          diffs = diffs.Tail;
        }
        // diffs.Tail.Head is new handler or none
        if (diffs.Tail == null)
        {
          // If isHandlerEdge, then each label in the source block of the edge is a target
          if (isHandlerEdge)
          {
            for (int index = 0; index == 0 || index < edge.One.Count; index++)
            {
              yield return new APC(edge.One, index, ppoint.SubroutineContext.Tail);
            }
          }
          else
          {
            yield return APC.ForEnd(edge.One, ppoint.SubroutineContext.Tail);
          }
          yield break;
        }
        // predecessor is end of the next inner handler
        Subroutine nextSubroutine = diffs.Tail.Head.Two;
        yield return APC.ForEnd(nextSubroutine.Exit, consCache(new SubroutineEdge(edge.One, edge.Two, diffs.Tail.Head.One), ppoint.SubroutineContext.Tail));
      }

      /// <summary>
      /// Called when ppoint is beginning of a finally on backwards walk.
      ///
      /// Finds next handler on current edge, or pops edge
      ///
      /// Note: we distinguish between edges that target an exception handler and thus need
      /// to execute fault and finally handlers and other edges that only execute finally handlers
      /// by the target block. In the former case, the target block is a SpecialCatchFilterHeader
      /// block. These blocks are needed, as the start of a catch handler could also be the normal
      /// goto/leave target of some try finally within the catch handler itself. Thus, the special
      /// block disinguishes between a throw target where faults must be traversed, and a normal
      /// leave target to the beginning of a catch handler.
      /// </summary>
      private APC ComputeSubroutinePreContinuation(APC ppoint, out bool hasSinglePred, DConsCache consCache)
      // ^ requires ppoint.FinallyEdges != null;
      {
        SubroutineEdge edge = ppoint.SubroutineContext.Head;
        bool isHandlerEdge;
        FList<Pair<string,Subroutine>> diffs = EdgeSubroutinesOuterToInner(edge.One, edge.Two, out isHandlerEdge, ppoint.SubroutineContext);
        Debug.Assert(diffs != null);
        while (diffs.Head.Two != this)
        {
          diffs = diffs.Tail;
        }
        // diffs.Tail.Head is new handler or none
        if (diffs.Tail == null)
        {
          // If isHandlerEdge, then each label in the source block of the edge is a target
          if (isHandlerEdge && edge.One.Count > 1)
          {
            // no single pred
            hasSinglePred = false;
            return APC.Dummy;
          }
          hasSinglePred = true;
          return APC.ForEnd(edge.One, ppoint.SubroutineContext.Tail);
        }
        // predecessor is end of the next inner handler
        Subroutine nextSubroutine = diffs.Tail.Head.Two;
        // only one end
        hasSinglePred = true;
        return APC.ForEnd(nextSubroutine.Exit, consCache(new SubroutineEdge(edge.One, edge.Two, diffs.Tail.Head.One), ppoint.SubroutineContext.Tail));
      }

      /// <summary>
      /// Called when ppoint is an endfinally.
      ///
      /// Finds next handler on current edge, or pops edge
      ///
      /// Note: we distinguish between edges that target an exception handler and thus need
      /// to execute fault and finally handlers and other edges that only execute finally handlers
      /// by the target block. In the former case, the target block is a SpecialCatchFilterHeader
      /// block. These blocks are needed, as the start of a catch handler could also be the normal
      /// goto/leave target of some try finally within the catch handler itself. Thus, the special
      /// block disinguishes between a throw target where faults must be traversed, and a normal
      /// leave target to the beginning of a catch handler.
      /// </summary>
      private APC ComputeSubroutineContinuation(APC ppoint, DConsCache consCache)
      // ^ requires ppoint.SubroutineContext != null;
      {
        SubroutineEdge edge = ppoint.SubroutineContext.Head;

        FList<Pair<string,Subroutine>> diffs = EdgeSubroutinesOuterToInner(edge.One, edge.Two, ppoint.SubroutineContext);
        Debug.Assert(diffs != null);
        if (diffs.Head.Two == this)
        {
          // last handler executed.
          // pop finally edge
          return new APC(edge.Two, 0, ppoint.SubroutineContext.Tail);
        }
        while (diffs.Tail.Head.Two != this)
        {
          diffs = diffs.Tail;
        }
        // diffs.Head is new handler
        Subroutine nextSubroutine = diffs.Head.Two;
        return new APC(nextSubroutine.Entry, 0, consCache(new SubroutineEdge(edge.One,edge.Two,diffs.Head.One), ppoint.SubroutineContext.Tail));
      }

      /// <param name="context">APC where edge is being considered. Used for filtering recursive calls.</param>
      private FList<Pair<string, Subroutine>> EdgeSubroutinesOuterToInner(CFGBlock current, CFGBlock succ, SubroutineContext context)
      {
        bool isExnHandler;
        return EdgeSubroutinesOuterToInner(current, succ, out isExnHandler, context);
      }

      /// <summary>
      /// Returns the difference in fault/finally handlers between the given program point and the
      /// given successor. Normally, the list only includes finally handlers, except in the case 
      /// where succ is a special catch/filter header block, indicating that this edge represents
      /// the execution on a throw, and thus fault handlers are included
      /// </summary>
      /// <param name="isExceptionHandlerEdge">is true, if the successor is a catch/filter handler</param>
      /// <param name="context">APC where edge is being considered. Used for filtering recursive calls.</param>
      internal override FList<Pair<string,Subroutine>> EdgeSubroutinesOuterToInner(CFGBlock current, CFGBlock succ, out bool isExceptionHandlerEdge, SubroutineContext context)
      {
        if (current.Subroutine != this)
        {
          // dispatch
          return current.Subroutine.EdgeSubroutinesOuterToInner(current, succ, out isExceptionHandlerEdge, context);
        }

        bool includeFaults = IsCatchFilterHeader(succ);
        isExceptionHandlerEdge = includeFaults;

        return this.GetOrdinaryEdgeSubroutines(current, succ, context);
      }

      internal override APC ComputeTargetFinallyContext(APC ppoint, CFGBlock succ, DConsCache consCache)
      {
        FList<Pair<string,Subroutine>> diffs = EdgeSubroutinesOuterToInner(ppoint.Block, succ, ppoint.SubroutineContext);

        if (diffs == null)
        {
          return new APC(succ, 0, ppoint.SubroutineContext);
        }
        // now diff is in outermost to inner most order of finallies we are traversing.
        // 1) push edge onto fault/finally context stack
        // 2) make innermost handler start current program point
        // IMPORTANT: the Finally context must be hash consed so that APC's have Equal working properly!
        //

        // 2) find innermost handler
        while (diffs.Length() != 1)
        {
          diffs = diffs.Tail;
        }

        Subroutine innerMost = diffs.Head.Two;
        SubroutineContext newFinallyContext = consCache(new SubroutineEdge(ppoint.Block, succ, diffs.Head.One), ppoint.SubroutineContext);

        // 3) transfer control to last (innermost) finally
        return new APC(innerMost.Entry, 0, newFinallyContext);
      }

      /// <summary>
      /// Returns a set of program points that are possible execution continuations if 
      /// an exception is raised at the given ppoint.
      ///
      /// The supplied predicate allows the client to determine which handlers are possible
      /// candidates. The predicate is called from closest enclosing to outermost.
      /// </summary>
      /// <param name="data">Arbitrary client data passed to the handler predicate</param>
      /// <returns>A set of continuation program points that represent how execution continues to a chosen handler.
      /// The continuation includes Finally and Fault executions that intervene between the given ppoint and a
      /// chosen handler.
      ///
      /// Hack. This only works if Type2 is Type. We do this because we don't want to expose Type in Subroutine and thus
      /// CFGBlock.
      /// </returns>
      internal override IEnumerable<CFGBlock> ExceptionHandlers<Data, Type2>(CFGBlock ppoint, Subroutine innerSubroutine, Data data, IHandlerFilter<APC, Type2, Data> handlerPredicateArg)
      {
        // This kind of subroutine has no handlers except the exceptional exit point
        yield return this.exceptionExit;
      }


      #endregion

      #region Post Cleanup

      private DepthFirst.Visitor<CFGBlock, Unit>/*?*/ edgeInfo = null;

      private bool IsEdgeUsed(Pair<CFGBlock, Pair<string, CFGBlock>> edge)
      {
        return (edge.One.Index != UnusedBlockIndex);
      }

      private EdgeMap<string> successorEdges;
      private EdgeMap<string> predecessorEdges; // cache

      internal override EdgeMap<string> SuccessorEdges
      {
        get { return this.successorEdges; }
      }

      internal override EdgeMap<string> PredecessorEdges
      {
        get
        {
          if (predecessorEdges == null)
          {
            //  compute predecessor map on demand
            this.predecessorEdges = this.SuccessorEdges.ReversedEdges();
          }
          return this.predecessorEdges;
        }
      }

      const int UnusedBlockIndex = Int16.MaxValue - 10;


      /// <summary>
      /// Removes unreachable blocks and reorders them in reverse post order for easier reading
      /// and for analysis.
      /// </summary>
      protected void PostProcessBlocks()
      {
        Stack<CFGBlock> blockStack = new Stack<CFGBlock>();

        this.successorEdges = new EdgeMap<string>(this.successors);

        // choose only reachable blocks
        edgeInfo = new DepthFirst.Visitor<CFGBlock, Unit>(
           this,
           null,
          // we use the post visit for the post order
           delegate(CFGBlock block)
           {
             // we use a stack to obtain reverse post order
             blockStack.Push(block);

             // we approximate the backwards reverse post order here as the forward pre-order
             // block.SetBackwardReversePostOrderIndex(ref backwardIndexGen);
           },
           null);

        // Make sure that the exception exit block appears last and the normal exit block 2nd to last
        // This also makes sure that these nodes appear in our list.
        edgeInfo.VisitSubGraphNonRecursive(this.exceptionExit);
        edgeInfo.VisitSubGraphNonRecursive(this.exit);
        edgeInfo.VisitSubGraphNonRecursive(this.entry);

        //
        //  Now, we have unreachable blocks (and unused edges)
        //  1) unnumber all the blocks so we can recognize which ones are dead
        foreach (Pair<CFGBlock,Pair<Tag, CFGBlock>> edge in this.successorEdges)
        {
          int unused = UnusedBlockIndex;
          edge.One.Renumber(ref unused);
        }
        //  2) renumber the blocks in the reverse post order
        int blockRenumber = 0;
        foreach (BlockWithLabels<Label> block in blockStack)
        {
          block.Renumber(ref blockRenumber);
        }

        //  3) remove edges that are of unused blocks
        this.SuccessorEdges.Filter(IsEdgeUsed);

        // 3a) compute reversed graph
        this.predecessorEdges = this.SuccessorEdges.ReversedEdges();

        int finishTime = 0;
        // 3b) renumber according to reverse post order 
        var predvisit = new DepthFirst.Visitor<CFGBlock, string>(
           this.predecessorEdges, 
           null,
           delegate(CFGBlock block)
           {
             block.SetReversePostOrderIndex(finishTime++);
           },
           null);

        predvisit.VisitSubGraphNonRecursive(this.exit);

        foreach (var block in blockStack)
        {
          predvisit.VisitSubGraphNonRecursive(block);
        }


        //  4) resort the edges according to the new numbering
        //
        this.SuccessorEdges.ReSort();

        //  5) persist blocks as array
        this.blocks = blockStack.ToArray();

        //  6) cleanup
        this.BlockStart = null;
        this.Builder = null;

        // for testing:
        // Dominators.Compute(this, this.entry);


      }
      #endregion

      #region IGraph<CFGBlock<Label>,Pair<CFGBlock<Label>,CFGBlock<Label>>> Members

      IEnumerable<CFGBlock>/*!*/ IGraph<CFGBlock, Unit>.Nodes
      {
        get { return this.blocks; }
      }


      /// <summary>
      /// Provide all successors, normal and exceptional
      /// </summary>
      public virtual IEnumerable<Pair<Unit, CFGBlock>>/*!*/ Successors(CFGBlock node)
      {

        foreach (Pair<Tag, CFGBlock> normalSucc in this.SuccessorEdges[node])
        {
          yield return new Pair<Unit, CFGBlock>(Unit.Value, normalSucc.Two);
        }

        if (node != this.exceptionExit)
        {
          yield return new Pair<Unit, CFGBlock>(Unit.Value, this.exceptionExit);
        }
      }


      #endregion

      public override void Print(TextWriter tw, ILPrinter<APC> ilPrinter, BlockInfoPrinter<APC>/*?*/ blockInfoPrinter, Func<CFGBlock,IEnumerable<SubroutineContext>> contextLookup, SubroutineContext context, ISet<Pair<Subroutine,SubroutineContext>> printed)
      {
        var identifier = new Pair<Subroutine, SubroutineContext>(this, context);
        if (printed.Contains(identifier)) return; // already printed
        printed.Add(identifier);
        ISet<Subroutine> subs = new Set<Subroutine>();

        IMethodInfo<Method> mi = this as IMethodInfo<Method>;
        string extraSRIdentification = (mi != null) ? String.Format("({0})", this.MethodCache.MetadataDecoder.FullName(mi.Method)) : null;
        if (context == null)
        {
          tw.WriteLine("Subroutine SR{0} {1} {2}", this.Id, this.Kind, extraSRIdentification);
        }
        else
        {
          tw.WriteLine("Subroutine SR{0} {1} {2} {3}", this.Id, this.Kind, new APC(this.Entry, 0, context), extraSRIdentification);
        }
        tw.WriteLine("-----------------");
        
        foreach (BlockWithLabels<Label> block in this.Blocks)
        {
          tw.Write("Block {0} ({1})", block.Index, block.ReversePostOrderIndex);
          // revisit this. How should we print CFGs with subroutines?
          if (this.EdgeInfo.DepthFirstInfo(block).TargetOfBackEdge)
          {
            tw.WriteLine(" (target of backedge)");
            Debug.Assert(this.IsJoinPoint(block));
          }
          else if (this.IsJoinPoint(block))
          {
            tw.WriteLine(" (join point)");
          }
          else
          {
            tw.WriteLine();
          }
          tw.Write("  Predecessors: ");
          foreach (Pair<string, CFGBlock> taggedPred in block.Subroutine.PredecessorEdges[block])
          {
            tw.Write("({0}, {1}) ", taggedPred.One, taggedPred.Two.Index);
          }
          tw.WriteLine();
          PrintHandlers(tw, block);
          tw.WriteLine("  Code:");
          foreach (APC apc in block.APCs(context))
          {
            string sc = apc.PrimarySourceContext();
            if (sc != null) tw.WriteLine(sc);
            ilPrinter(apc, "    ", tw);
          }
          tw.Write("  Successors: ");
          foreach (Pair<Tag, CFGBlock> taggedSucc in this.SuccessorEdges[block])
          {
            tw.Write("({0},{1}", taggedSucc.One, taggedSucc.Two.Index);
            if (this.EdgeInfo.IsBackEdge(block, Unit.Value, taggedSucc.Two))
            {
              tw.Write(" BE");
            }
            FList<Pair<string,Subroutine>> edgesubs = this.GetOrdinaryEdgeSubroutines(block, taggedSucc.Two, context);
            while (edgesubs != null)
            {
              subs.Add(edgesubs.Head.Two);
              tw.Write(" SR{0}({1})", edgesubs.Head.Two.Id, edgesubs.Head.One);
              edgesubs = edgesubs.Tail;
            }
            tw.Write(") ");

          }
          tw.WriteLine();
          if (blockInfoPrinter != null)
          {
            blockInfoPrinter(new APC(block, block.Last.Index, context), "  ", tw);
          }
          tw.WriteLine();
        }

        PrintReferencedSubroutines(tw, subs, ilPrinter, blockInfoPrinter, contextLookup, context, printed);

      }

      protected virtual void PrintReferencedSubroutines(TextWriter tw, ISet<Subroutine> subs, ILPrinter<APC> ilPrinter, BlockInfoPrinter<APC> blockInfoPrinter, Func<CFGBlock, IEnumerable<SubroutineContext>> contextLookup, SubroutineContext context, ISet<Pair<Subroutine, SubroutineContext>> printed)
      {
        // print edge subroutines
        foreach (Subroutine sb in subs)
        {
          if (contextLookup == null)
          {
            sb.Print(tw, ilPrinter, blockInfoPrinter, contextLookup, null, printed);
          }
          else
          {
            foreach (SubroutineContext newContext in contextLookup(sb.Entry))
            {
              sb.Print(tw, ilPrinter, blockInfoPrinter, contextLookup, newContext, printed);
            }
          }
        }
      }

      protected virtual void PrintHandlers(TextWriter tw, BlockWithLabels<Label> block)
      {
        tw.Write("  Handlers: ");
        // print out method wide handler (implicitly added in the appropriate query on CFGs)
        if (block != this.exceptionExit)
        {
          tw.Write("{0} ", this.exceptionExit.Index);
        }
        tw.WriteLine();
      }


      public override IEnumerable<Subroutine> UsedSubroutines
      {
        get
        {
          ISet<int> alreadyFound = new Set<int>();
          foreach (var sublist in this.edgeSubroutines.Values)
          {
            var list = sublist;
            for (; list != null; list = list.Tail)
            {
              var sub = list.Head.Two;
              if (alreadyFound.Contains(sub.Id)) continue;
              alreadyFound.Add(sub.Id);
              yield return sub;
            }
          }
        }
      }

      internal virtual string SourceAssertionCondition(Label label)
      {
        return this.CodeProvider.SourceAssertionCondition(label);
      }
    }

    internal abstract class SubroutineWithHandlers<Label, Handler> : SubroutineBase<Label> {

      /// <summary>
      /// Used during scanning to compute the protecting handlers in this subroutine only
      /// </summary>
      internal FList<Handler> CurrentProtectingHandlers = FList<Handler>.Empty; // protecting the current block
      /// <summary>
      /// Holds the block starting the filter code for filter handlers.
      /// Materialized on demand.
      /// </summary>
      protected OnDemandMap<Handler, BlockWithLabels<Label>> FilterCodeBlocks;
      protected OnDemandMap<Handler, BlockWithLabels<Label>> CatchFilterHeaders;
      internal OnDemandMap<Handler, Subroutine> FaultFinallySubroutines;
      /// <summary>
      /// For each block, this provides the enclosing handlers (finally,fault, and exceptions) targeted by an exception raised in this block.
      /// </summary>
      internal OnDemandMap<CFGBlock, FList<Handler>> ProtectingHandlers;


      new protected IMethodCodeProvider<Label, Method, Type, Handler> CodeProvider
      {
        get { return (IMethodCodeProvider<Label, Method, Type, Handler>)base.CodeProvider; }
      }

      internal SubroutineWithHandlers(MethodCache<Local, Parameter, Type, Method, Field, Property, Attribute, Assembly> methodCache)
        : base(methodCache)
      {
      }

      internal SubroutineWithHandlers(MethodCache<Local,Parameter,Type,Method,Field,Property,Attribute,Assembly> methodCache,
                                      SubroutineWithHandlersBuilder<Label, Handler> builder,
                                      Label entry)
        : base(methodCache, entry, builder)
      {
      }


      bool IsFault(Handler handler)
      {
        return this.CodeProvider.IsFaultHandler(handler);
      }

      private FList<Handler> ProtectingHandlersList(CFGBlock block)
      {
        FList<Handler>/*?*/ result;
        ProtectingHandlers.TryGetValue(block, out result);
        return result;
      }

      private IEnumerable<Handler> GetProtectingHandlers(CFGBlock block)
      {
        return ProtectingHandlersList(block).GetEnumerable();
      }


      internal BlockWithLabels<Label> CreateCatchFilterHeader(Handler handler, Label label)
      {
        BlockWithLabels<Label> newBlock;
        if (!this.BlockStart.TryGetValue(label, out newBlock)) {
#if ENABLE_ASSERTIONS
          Debug.Assert(this.MethodInfo.IsBlockStart(label));
#endif
          newBlock = new CatchFilterEntryBlock<Label>(this, ref this.blockIdGenerator);
          this.CatchFilterHeaders.Add(handler, newBlock);
          this.BlockStart.Add(label, newBlock);
          if (this.CodeProvider.IsFilterHandler(handler))
          {
            // cache the filter code block which we need to lookup during exception paths
            var filterCodeBlock = GetTargetBlock(this.CodeProvider.FilterDecisionStart(handler));
            this.FilterCodeBlocks.Add(handler, filterCodeBlock);
          }
        }
        return newBlock;
      }

      public override IEnumerable<Subroutine> UsedSubroutines
      {
        get
        {
          foreach (Subroutine ffsr in this.FaultFinallySubroutines.Values) {
            yield return ffsr;
          }
          foreach (var sub in BaseUsedSubroutines())
          {
            yield return sub;
          }
        }
      }

      private IEnumerable<Subroutine> BaseUsedSubroutines()
      {
        return base.UsedSubroutines;
      }

      /// <param name="context">APC where edge is being considered. Used for filtering recursive calls.</param>
      internal override FList<Pair<string, Subroutine>> EdgeSubroutinesOuterToInner(CFGBlock current, CFGBlock succ, out bool isExceptionHandlerEdge, SubroutineContext context)
      {
        if (current.Subroutine != this)
        {
          // dispatch
          return current.Subroutine.EdgeSubroutinesOuterToInner(current, succ, out isExceptionHandlerEdge, context);
        }
        // determine finally nesting difference
        FList<Handler> currentHandlers = this.ProtectingHandlersList(current);
        FList<Handler> newHandlers = this.ProtectingHandlersList(succ);

        bool includeFaults = IsCatchFilterHeader(succ);
        isExceptionHandlerEdge = includeFaults;

        // now we could pop some, push some, or both (pop then push)
        FList<Pair<string, Subroutine>>/*?*/ subroutines = this.GetOrdinaryEdgeSubroutines(current, succ, context);
        while (currentHandlers != newHandlers)
        {
          if (currentHandlers.Length() >= newHandlers.Length())
          {
            // definite pop
            Handler eh = currentHandlers.Head;
            if (this.IsFaultOrFinally(eh))
            {
              if (!IsFault(eh) || includeFaults)
              {
                subroutines = FList<Pair<string, Subroutine>>.Cons(new Pair<string, Subroutine>("finally", this.FaultFinallySubroutines[eh]), subroutines);
              }
            }
            currentHandlers = currentHandlers.Tail;
          }
          else
          {
            // definite push
            newHandlers = newHandlers.Tail;
          }
        }
        return subroutines;
      }

      public bool IsFaultOrFinally(Handler handler)
      {
        return this.CodeProvider.IsFaultHandler(handler) || this.CodeProvider.IsFinallyHandler(handler);
      }

      internal override IEnumerable<CFGBlock> ExceptionHandlers<Data, Type2>(CFGBlock ppoint, Subroutine innerSubroutine, Data data, IHandlerFilter<APC, Type2, Data> handlerPredicateArg)
      {
        // Hack: cast to expected Type.
        IHandlerFilter<APC, Type, Data> handlerPredicate = (IHandlerFilter<APC, Type, Data>)handlerPredicateArg;

        // First, find handlers that protect ppoint, while being further out than innerSubroutine
        FList<Handler> protectingHandlers = this.ProtectingHandlersList(ppoint);

        if (innerSubroutine != null && innerSubroutine.IsFaultFinally) {
          while (protectingHandlers != null) {
            if (this.IsFaultOrFinally(protectingHandlers.Head) && this.FaultFinallySubroutines[protectingHandlers.Head] == innerSubroutine) {
              protectingHandlers = protectingHandlers.Tail;
              // found remaining handlers
              break;
            }
            protectingHandlers = protectingHandlers.Tail;
          }
        }

        // now we have the remaining handlers that protect the program point

        while (protectingHandlers != null) {
          Handler handler = protectingHandlers.Head;
          if (!this.IsFaultOrFinally(handler)) {
            if (handlerPredicate != null) {
              bool stopPropagation = false;
              if (this.CodeProvider.IsCatchHandler(handler)) {
                if (handlerPredicate.Catch(data, this.CodeProvider.CatchType(handler), out stopPropagation)) {
                  yield return this.CatchFilterHeaders[handler];
                }
              }
              else {
                Debug.Assert(this.CodeProvider.IsFilterHandler(handler));
                if (handlerPredicate.Filter(data, new APC(this.FilterCodeBlocks[handler], 0, null), out stopPropagation)) {
                  yield return this.CatchFilterHeaders[handler];
                }
              }
              if (stopPropagation) yield break; // no further handlers
            }
            else {
              // no predicate, add all handlers
              yield return this.CatchFilterHeaders[handler];
            }
            if (this.CodeProvider.IsCatchAllHandler(handler)) {
              yield break; // no further handlers
            }
          }
          protectingHandlers = protectingHandlers.Tail;
        }

        // if we get here, the exception can escape the subroutine
        yield return this.ExceptionExit;
      }

      /// <summary>
      /// Provide all successors, normal and exceptional
      /// </summary>
      public override IEnumerable<Pair<Unit, CFGBlock>>/*!*/ Successors(CFGBlock node)
      {
        foreach (Pair<Tag, CFGBlock> normalSucc in this.SuccessorEdges[node]) {
          yield return new Pair<Unit, CFGBlock>(Unit.Value, normalSucc.Two);
        }

        foreach (Handler handler in this.GetProtectingHandlers(node)) {
          // Careful: for fault finally the block belongs to a different subroutine, so we ignore it here
          //          for filter/catch, it is the special header block
          if (this.IsFaultOrFinally(handler)) {
            continue;
          }
          else {
            yield return new Pair<Unit, CFGBlock>(Unit.Value, this.CatchFilterHeaders[handler]);
          }
        }
        if (node != this.ExceptionExit) {
          yield return new Pair<Unit, CFGBlock>(Unit.Value, this.ExceptionExit);
        }
      }

      protected override void PrintReferencedSubroutines(TextWriter tw, ISet<Subroutine> subs, ILPrinter<APC> ilPrinter, BlockInfoPrinter<APC> blockInfoPrinter, Func<CFGBlock, IEnumerable<FList<Tuple<CFGBlock, CFGBlock, string>>>> contextLookup, SubroutineContext context, ISet<Pair<Subroutine, SubroutineContext>> printed)
      {
        foreach (Subroutine sb in this.FaultFinallySubroutines.Values)
        {
          if (contextLookup == null)
          {
            sb.Print(tw, ilPrinter, blockInfoPrinter, contextLookup, context, printed);
          }
          else
          {
            foreach (SubroutineContext newContext in contextLookup(sb.Entry))
            {
              sb.Print(tw, ilPrinter, blockInfoPrinter, contextLookup, newContext, printed);
            }
          }
        }
        base.PrintReferencedSubroutines(tw, subs, ilPrinter, blockInfoPrinter, contextLookup, context, printed);
      }

      protected override void PrintHandlers(TextWriter tw, BlockWithLabels<Label> block)
      {
        tw.Write("  Handlers: ");
        foreach (Handler handler in this.GetProtectingHandlers(block))
        {
          if (this.IsFaultOrFinally(handler))
          {
            Subroutine sb = this.FaultFinallySubroutines[handler];
            tw.Write("SR{0} ", sb.Id);
          }
          else
          {
            BlockWithLabels<Label> handlerStart = this.CatchFilterHeaders[handler];
            tw.Write("{0} ", handlerStart.Index);
          }
        }
        // print out method wide handler (implicitly added in the appropriate query on CFGs)
        if (block != this.ExceptionExit)
        {
          tw.Write("{0} ", this.ExceptionExit.Index);
        }
        tw.WriteLine();
      }

    }

    /// <summary>
    /// Represents an entire method
    /// </summary>
    internal class MethodSubroutine<Label, Handler> : SubroutineWithHandlers<Label, Handler>, IMethodInfo<Method>
    {
      Set<BlockWithLabels<Label>> blocksEndingInReturn;

      /// <summary>
      /// Dummy one without a body
      /// </summary>
      internal MethodSubroutine(MethodCache<Local, Parameter, Type, Method, Field, Property, Attribute, Assembly> methodCache, Method method)
        : base(methodCache)
      {
        this.method = method;
      }

      Method method;

      public MethodSubroutine(MethodCache<Local, Parameter, Type, Method, Field, Property, Attribute, Assembly> methodCache,
                              Method method,
                              SubroutineWithHandlersBuilder<Label, Handler> builder,
                              Label startLabel)
        : base(methodCache, builder, startLabel)
      {
        this.method = method;

        builder.BuildBlocks(startLabel, this);

        BlockWithLabels<Label> startLabelBlock = this.GetTargetBlock(startLabel);

        this.Commit();
        Type declaringType = MethodCache.MetadataDecoder.DeclaringType(method);

        this.AddEdgeSubroutine(this.Entry, startLabelBlock, MethodCache.GetRequires(method), "entry");
        Subroutine invariant = MethodCache.GetInvariant(declaringType);

        if (!MethodCache.MetadataDecoder.IsConstructor(method) && !MethodCache.MetadataDecoder.IsStatic(method))
        {
          this.AddEdgeSubroutine(this.Entry, startLabelBlock, invariant, "entry");
        }

        if (blocksEndingInReturn != null)
        {
          var ensuresSub = MethodCache.GetEnsures(method);

          foreach (BlockWithLabels<Label> retBlock in blocksEndingInReturn)
          {
            this.AddEdgeSubroutine(retBlock, this.Exit, ensuresSub, "exit");
            if (!MethodCache.MetadataDecoder.IsStatic(method) &&
                !MethodCache.MetadataDecoder.IsFinalizer(method) && 
                !MethodCache.MetadataDecoder.IsDispose(method))
            {
              this.AddEdgeSubroutine(retBlock, this.Exit, invariant, "exit");
            }
          }

          if (ensuresSub != null)
          {
            foreach (var nestedEnsuresSub in ensuresSub.UsedSubroutines)
            {
              if (nestedEnsuresSub.IsOldValue)
              {
                this.AddEdgeSubroutine(this.Entry, startLabelBlock, Microsoft.Research.CodeAnalysis.ILCodeProvider.Predefined.OldEvalPopSubroutine(MethodCache, nestedEnsuresSub), "oldmanifest");
              }
            }
          }
          // cleanup
          blocksEndingInReturn = null;
        }
      }

      public override string Kind
      {
        get { return "method"; }
      }
      /// <summary>
      /// Method subroutines are eagerly initialized
      /// </summary>
      internal override void Initialize()
      {
      }

      /// <summary>
      /// Record which blocks end in returns
      /// </summary>
      internal override void AddReturnBlock(BlockWithLabels<Label> block)
      {
        if (blocksEndingInReturn == null)
        {
          this.blocksEndingInReturn = new Set<BlockWithLabels<Label>>();
        }
        this.blocksEndingInReturn.Add(block);
        base.AddReturnBlock(block);
      }

      public override bool HasReturnValue
      {
        get
        {
          return !this.MethodCache.MetadataDecoder.IsVoidMethod(this.method);
        }
      }

      public override bool IsMethod
      {
        get
        {
          return true;
        }
      }

      internal override bool IsCompilerGenerated
      {
        get
        {
          return this.MethodCache.MetadataDecoder.IsCompilerGenerated(this.method);
        }
      }

      public override string Name
      {
        get
        {
          return this.MethodCache.MetadataDecoder.FullName(this.method);
        }
      }

      public Method Method { get { return this.method; } }
    }

    internal abstract class FaultFinallySubroutineBase<Label, Handler> : SubroutineWithHandlers<Label, Handler>
    {
      protected FaultFinallySubroutineBase(MethodCache<Local, Parameter, Type, Method, Field, Property, Attribute, Assembly> methodCache,
                                           SubroutineWithHandlersBuilder<Label, Handler> builder,
                                           Label startLabel)
        : base(methodCache, builder, startLabel)
      {
      }

      internal override void Initialize()
      {
        // nothing to do. MethodSubroutine finalizes the fault finally subroutines.
      }

      public override bool HasContextDependentStackDepth
      {
        get
        {
          return false; // always starts at 0
        }
      }

      internal override bool IsFaultFinally
      {
        get
        {
          return true;
        }
      }
    }

    /// <summary>
    /// Represents a fault region within a method
    /// </summary>
    internal class FaultSubroutine<Label, Handler> : FaultFinallySubroutineBase<Label, Handler>
    {
      public FaultSubroutine(MethodCache<Local, Parameter, Type, Method, Field, Property, Attribute, Assembly> methodCache, 
                             SubroutineWithHandlersBuilder<Label, Handler> builder,
                             Label startLabel)
        : base(methodCache, builder, startLabel)
      {
      }

      public override string Kind
      {
        get { return "fault"; }
      }

    }


    /// <summary>
    /// Represents a finally region within a method
    /// </summary>
    internal class FinallySubroutine<Label, Handler> : FaultFinallySubroutineBase<Label, Handler>
    {
      public FinallySubroutine(MethodCache<Local, Parameter, Type, Method, Field, Property, Attribute, Assembly> methodCache,
                               SubroutineWithHandlersBuilder<Label, Handler> builder,
                               Label startLabel)
        : base(methodCache, builder, startLabel)
      {
      }

      public override string Kind
      {
        get { return "finally"; }
      }
    }

    internal abstract class CallingContractSubroutine<Label> : SubroutineBase<Label>, IMethodInfo<Method>
    {
      Method methodWithThisContract;
      new protected SimpleSubroutineBuilder<Label> Builder;

      public CallingContractSubroutine(MethodCache<Local, Parameter, Type, Method, Field, Property, Attribute, Assembly> methodCache,
                                       Method method)
        : base(methodCache)
      {
        this.methodWithThisContract = method;
      }

      public CallingContractSubroutine(MethodCache<Local, Parameter, Type, Method, Field, Property, Attribute, Assembly> methodCache,
                                       Method method,
                                       SimpleSubroutineBuilder<Label> builder,
                                       Label startLabel)
        : base(methodCache, startLabel, builder)
      {
        this.Builder = builder;
        this.methodWithThisContract = method;
      }

      #region IMethodInfo<Method> Members

      public Method Method
      {
        get { return this.methodWithThisContract; }
      }

      #endregion

    }

    internal class RequiresSubroutine<Label> : CallingContractSubroutine<Label>, IEquatable<RequiresSubroutine<Label>>
    {
      public RequiresSubroutine(MethodCache<Local, Parameter, Type, Method, Field, Property, Attribute, Assembly> methodCache,
                                Method method,
                                SimpleSubroutineBuilder<Label> builder, Label startLabel, IFunctionalSet<Subroutine> inherited)
        : base(methodCache, method, builder, startLabel)
      {
        this.AddBaseRequires(this.GetTargetBlock(startLabel), inherited);
      }

      public override string Kind
      {
        get { return "requires"; }
      }

      internal override void Initialize()
      {
        if (Builder == null) return;

        Builder.BuildBlocks(startLabel, this);
        this.Commit();
        Builder = null;
      }

      public RequiresSubroutine(MethodCache<Local, Parameter, Type, Method, Field, Property, Attribute, Assembly> methodCache, Method method,
                                IFunctionalSet<Subroutine> inherited)
        : base(methodCache, method)
      {
        this.AddSuccessor(this.Entry, "entry", this.Exit);
        this.AddBaseRequires(this.Exit, inherited);
        this.Commit();
      }

      /// <summary>
      /// Add subroutine calls to
      ///  - non-abstract virtual methods
      ///  - abstract methods with HasRequires
      /// </summary>
      private void AddBaseRequires(CFGBlock targetOfEntry, IFunctionalSet<Subroutine> inherited) {
        foreach (Subroutine rs in inherited.Elements)
        {
          this.AddEdgeSubroutine(this.Entry, targetOfEntry, rs, "inherited");
        }
      }

      internal override bool IsRequires
      {
        get { return true; }
      }

      internal override bool IsContract
      {
        get { return true; }
      }

      public bool Equals(RequiresSubroutine<Label> that)
      {
        return this.Id == that.Id;
      }
    }

    internal class SimpleSubroutine<Label> : SubroutineBase<Label>
    {
      public SimpleSubroutine(
        int stackDelta,
        MethodCache<Local,Parameter,Type,Method,Field,Property,Attribute,Assembly> methodCache,
        Label startLabel,
        SimpleSubroutineBuilder<Label> builder
      )
        : base(methodCache, startLabel, builder)
      {
        this.stackDelta = stackDelta;
        builder.BuildBlocks(startLabel, this);

        this.Commit();
      }

      int stackDelta;

      internal override int StackDelta
      {
        get
        {
          return this.stackDelta;
        }
      }

      internal override void Initialize()
      {
      }

      public override string Kind
      {
        get { return "simple"; }
      }
    }

    internal class OldValueSubroutine<Label> : CallingContractSubroutine<Label>
    {
      BlockWithLabels<Label> beginOldBlock;
      BlockWithLabels<Label> endOldBlock;

      public OldValueSubroutine(MethodCache<Local, Parameter, Type, Method, Field, Property, Attribute, Assembly> methodCache,
                                Method method,
                                SimpleSubroutineBuilder<Label> builder, Label startLabel)
        : base(methodCache, method, builder, startLabel)
      {
      }

      public override string Kind
      {
        get { return "old"; }
      }

      internal override int StackDelta
      {
        get
        {
          return 1;
        }
      }

      internal override bool IsOldValue
      {
        get { return true; }
      }


      internal override void Initialize()
      {
        // nothing to do. EnsuresSubroutine finalizes this subroutines.
      }


      internal void Commit(BlockWithLabels<Label> endOldBlock)
      {
        this.endOldBlock = endOldBlock;
        this.Commit();
      }

      internal void RegisterBeginBlock(BlockWithLabels<Label> newBlock)
      {
        this.beginOldBlock = newBlock;
      }

      internal APC BeginOldAPC(SubroutineContext context)
      {
        return new APC(beginOldBlock, 0, context);
      }

      internal APC EndOldAPC(SubroutineContext context)
      {
        return new APC(endOldBlock, endOldBlock.Count - 1, context);
      }
    }

    internal class EnsuresSubroutine<Label> : CallingContractSubroutine<Label>, IEquatable<EnsuresSubroutine<Label>>
    {
      public EnsuresSubroutine(MethodCache<Local, Parameter, Type, Method, Field, Property, Attribute, Assembly> methodCache,
                               Method method,
                               SimpleSubroutineBuilder<Label> builder, Label startLabel, IFunctionalSet<Subroutine> inherited)
        : base(methodCache, method, builder, startLabel)
      {
        CFGBlock startBlock = this.GetTargetBlock(startLabel);
        this.AddBaseEnsures(this.Entry, startBlock, inherited);
      }

      public override string Kind
      {
        get { return "ensures"; }
      }

      internal override void Initialize()
      {
        if (Builder == null) return;

        Builder.BuildBlocks(startLabel, this);

        this.Commit();
        Builder = null;
      }

      public EnsuresSubroutine(MethodCache<Local, Parameter, Type, Method, Field, Property, Attribute, Assembly> methodCache, Method method, IFunctionalSet<Subroutine> inherited)
        : base(methodCache, method)
      {
        this.AddSuccessor(this.Entry, "entry", this.Exit);
        this.AddBaseEnsures(this.Entry, this.Exit, inherited);
        this.Commit();
      }


      internal override BlockWithLabels<Label> NewBlock()
      {
        return new EnsuresBlock<Label>(this, ref this.blockIdGenerator);
      }

      /// <summary>
      /// Add subroutine calls to
      ///  - non-abstract virtual methods
      ///  - abstract methods with HasRequires
      /// </summary>
      private void AddBaseEnsures(CFGBlock fromBlock, CFGBlock toBlock, IFunctionalSet<Subroutine> inherited)
      {
        foreach (Subroutine es in inherited.Elements)
        {
          this.AddEdgeSubroutine(fromBlock, toBlock, es, "inherited");
        }
      }


      /// <summary>
      /// This map is used by inferred old regions to map (index lsh 16) + block_id to the corresponding 
      /// block and type (for end old)
      /// </summary>
      OnDemandMap<int, Pair<CFGBlock, Type>> inferredOldLabelReverseMap;

      internal static int GetKey(EnsuresSubroutine<Label> rs) { return rs.Id; }

      internal override bool IsEnsures
      {
        get { return true; }
      }

      internal override bool IsContract
      {
        get { return true; }
      }

      public bool Equals(EnsuresSubroutine<Label> that)
      {
        return this.Id == that.Id;
      }

      #region Committing

      enum ScanState { OutsideOld, InsideOld, InsertingOld, InsertingOldAfterCall }

      /// <summary>
      /// Fixes up sequences of ldarga ...  ... to wrap them with begin_old end_old
      /// Visitor returns true if we are adding instructions to underlying block. Argument is original block index.
      ///
      /// We have to be very careful when the old sequence ends in a method call. If the method has arguments
      /// other than the old address, then we can't evaluate the method call in the old scope, since we wouldn't find
      /// all the arguments to it.
      /// So we have to prematurely end the old scope at such method calls.
      /// </summary>
      private class CommitScanState : MSILVisitor<Label,Local,Parameter,Method,Field,Type, Unit, Unit, int, bool>
      {
        ScanState state;
        Type nextEndOldType;
        EnsuresBlock<Label> currentBlock;
        EnsuresSubroutine<Label> parent;

        OnDemandMap<CFGBlock, ScanState> blockStartState;

        public CommitScanState(EnsuresSubroutine<Label> parent)
        {
          this.parent = parent;
          this.state = ScanState.OutsideOld;
        }

        public void StartBlock(EnsuresBlock<Label> block)
        {
          if (!this.blockStartState.TryGetValue(block, out this.state))
          {
            this.state = ScanState.OutsideOld; // default
            this.blockStartState.Add(block, this.state); // remember in case of bad control flow
          }
          if (this.state == ScanState.InsertingOld)
          {
            block.StartOverridingLabels();
          }
          if (this.state == ScanState.InsertingOldAfterCall)
          {
            // insert end old at head of block
            block.StartOverridingLabels();
            block.EndOldWithoutInstruction(this.nextEndOldType);
            this.state = ScanState.OutsideOld;
          }
          this.currentBlock = block;
        }

        public void SetStartState(CFGBlock succ)
        {
          // Debug.Assert(this.state != ScanState.InsertingOld); // can't insert old spanning blocks.
          ScanState start;
          if (!blockStartState.TryGetValue(succ, out start))
          {
            blockStartState.Add(succ, this.state);
          }
          else
          {
            Debug.Assert(start == this.state);
          }
        }

        #region Visitor
        protected override bool Default(Label pc, int index)
        {
          if (this.state == ScanState.InsertingOld)
          {
            // something's odd: need to end old scope here. Could be loading only the address of
            // a parameter and never dereferencing it, or passing it to a method among other parameters,
            // in which case, we can't do much (mix of old and new in a method use).

            // the end old type is actually a pointer
            this.state = ScanState.OutsideOld;
            var oldType = this.parent.MethodCache.MetadataDecoder.ManagedPointer(this.nextEndOldType);
            this.currentBlock.EndOldWithoutInstruction(oldType);
            
            // still insert the current instruction (now after the end old)
            return true;
          }
          return this.currentBlock.UsesOverriding;
        }

        public override bool Nop(Label pc, int data)
        {
          return this.currentBlock.UsesOverriding;
        }

        public override bool BeginOld(Label pc, Label matchingEnd, int index)
        {
          Debug.Assert(this.state == ScanState.OutsideOld);
          this.state = ScanState.InsideOld;
          return this.currentBlock.UsesOverriding;
        }

        public override bool EndOld(Label pc, Label matchingBegin, Type type, Unit dest, Unit source, int index)
        {
          Debug.Assert(this.state == ScanState.InsideOld);
          this.state = ScanState.OutsideOld;
          return this.currentBlock.UsesOverriding;
        }

        public override bool Ldarga(Label pc, Parameter argument, bool dummyIsOld, Unit dest, int index)
        {
          if (this.state == ScanState.OutsideOld)
          {
            this.state = ScanState.InsertingOld;
            this.currentBlock.BeginOld(index);
            this.nextEndOldType = parent.MethodCache.MetadataDecoder.ParameterType(argument);
          }
          return this.currentBlock.UsesOverriding;
        }

        public override bool Ldind(Label pc, Type type, bool @volatile, Unit dest, Unit ptr, int index)
        {
          if (this.state == ScanState.InsertingOld)
          {
            // ends the old scope
            this.state = ScanState.OutsideOld;
            this.currentBlock.EndOld(index, nextEndOldType);
            return false; // we already had to add this instruction before the end_old.
          }
          return this.currentBlock.UsesOverriding;
        }

        public override bool Ldfld(Label pc, Field field, bool @volatile, Unit dest, Unit obj, int index)
        {
          if (this.state == ScanState.InsertingOld)
          {
            // ends the old scope
            this.state = ScanState.OutsideOld;
            this.currentBlock.EndOld(index, this.parent.MethodCache.MetadataDecoder.FieldType(field));
            return false; // we already had to add this instruction before the end_old.
          }
          return this.currentBlock.UsesOverriding;
        }

        public override bool Ldflda(Label pc, Field field, Unit dest, Unit obj, int data)
        {
          if (this.state == ScanState.InsertingOld)
          {
            // keep track of eventual type in old
            this.nextEndOldType = this.parent.MethodCache.MetadataDecoder.FieldType(field);
          }
          return this.currentBlock.UsesOverriding;
        }

        /// <summary>
        /// Ends sequence of inserted old. Call is on struct address passed by value, thus in the old state
        /// Trickyness:
        ///  - Because Call statements appear in their own block, and post-conditions of the call need to be
        ///    evaluated also in the old state, we need to insert the EndOld statement in the next block
        ///  - The CFG construction is making sure that there are no back-to-back call blocks in ensures.
        /// </summary>
        public override bool Call<TypeList, ArgList>(Label pc, Method method, bool tail, bool virt, TypeList extraVarargs, Unit dest, ArgList args, int index)
        {
          // we are not decoding method call blocks.
          Debug.Assert(false);
          return false;
        }
        #endregion


        /// <summary>
        /// If we are inserting old, and this method has more than 1 parameter (including this), then
        /// we are out of luck. We cannot evaluate it in the oldstate, as the other parameters were pushed in 
        /// the new state.
        /// </summary>
        /// <param name="block"></param>
        internal void HandlePotentialCallBlock(MethodCallBlock<Label> block, EnsuresBlock<Label> priorBlock)
        {
          if (block == null) return;

          if (this.state == ScanState.InsertingOld)
          {
            // ends scope. Figure out if before call, or after call.
            int paramCount = parent.MethodCache.MetadataDecoder.Parameters(block.CalledMethod).Count;
            if (!parent.MethodCache.MetadataDecoder.IsStatic(block.CalledMethod))
            {
              paramCount++;
            }
            if (paramCount > 1)
            {
              // end old in prior block (block before call block).
              this.state = ScanState.OutsideOld;

              // the end old type is actually a pointer
              var oldType = parent.MethodCache.MetadataDecoder.ManagedPointer(this.nextEndOldType);
              priorBlock.EndOldWithoutInstruction(oldType);
            }
            else
            {
              this.state = ScanState.InsertingOldAfterCall;
              this.nextEndOldType = parent.MethodCache.MetadataDecoder.ReturnType(block.CalledMethod);
            }
          }
        }
      }

      internal override void Commit()
      {
        base.Commit();
        var scan = new CommitScanState(this);

        EnsuresBlock<Label> priorBlock = null;

        foreach (var block in this.Blocks)
        {
          // skip blocks that are not our special blocks.
          EnsuresBlock<Label> b = block as EnsuresBlock<Label>;
          if (b != null)
          {
            // save block in case next block is a call block. 
            // (we assume that block order is sequential for straight line code)
            priorBlock = b;
            // scan block, keeping track if we are in old, inserting old, or outside old
            // decode each instruction
            // if ldarg or ldarga and we are not in old, add a begin_old bubble and start inserting_old
            //   if inserting_old and current instruction is a memory deref (ldfld, ldind), add end_old after instruction 
            //   and state is now outside old
            // if reaching end of block while inserting old, insert an end_old (could be passing address by ref to a method)
            int originalCount = b.Count; // do this before scan.StartBlock
            scan.StartBlock(b);

            for (int i = 0; i < originalCount; i++)
            {
              bool addInstruction = b.OriginalForwardDecode<int, bool, CommitScanState>(i, scan, i);
              if (addInstruction)
              {
                b.AddInstruction(i);
              }
            }
          }
          else // might be a method call block
          {
            // Method call blocks end the old scope if we are in one
            scan.HandlePotentialCallBlock(block as MethodCallBlock<Label>, priorBlock);
          }
          foreach (var succ in this.SuccessorBlocks(block))
          {
            scan.SetStartState(succ);
          }
        }
      }
      #endregion


      internal void AddInferredOldMap(int blockIndex, int instructionIndex, CFGBlock otherBlock, Type endOldType)
      {
        this.inferredOldLabelReverseMap.Add(OverlayInstructionKey(blockIndex, instructionIndex), new Pair<CFGBlock, Type>(otherBlock, endOldType));
      }

      private static int OverlayInstructionKey(int blockIndex, int instruction)
      {
        var result = (instruction << 16) + blockIndex;
        return result;
      }

      internal CFGBlock InferredBeginEndBijection(APC pc)
      {
        Type dummy;
        return InferredBeginEndBijection<Type>(pc, out dummy);
      }

      internal CFGBlock InferredBeginEndBijection<Type2>(APC pc, out Type2 endOldType)
      {
        var key = OverlayInstructionKey(pc.Block.Index, pc.Index);
        Pair<CFGBlock, Type> data;
        if (!this.inferredOldLabelReverseMap.TryGetValue(key, out data))
        {
          throw new ApplicationException("Fatal bug in ensures CFG begin/end old map");
        }
        endOldType = (Type2)(object)data.Two;
        return data.One;
      }
    }


    internal class InvariantSubroutine<Label> : SubroutineBase<Label>
    {
      new SimpleSubroutineBuilder<Label> Builder;

      public InvariantSubroutine(MethodCache<Local, Parameter, Type, Method, Field, Property, Attribute, Assembly> methodCache,
                                 SimpleSubroutineBuilder<Label> builder, Label startLabel,
                                 Subroutine baseInv
      )
        : base(methodCache, startLabel, builder)
      {
        this.Builder = builder;
        CFGBlock startBlock = this.GetTargetBlock(startLabel);
        this.AddBaseInvariant(this.Entry, startBlock, baseInv);

      }

      public override string Kind
      {
        get { return "invariant"; }
      }

      internal override void Initialize()
      {
        if (Builder == null) return;

        Builder.BuildBlocks(startLabel, this);
        this.Commit();
        Builder = null;
      }

      /// <summary>
      /// Add subroutine calls to base invariant if non-null
      /// </summary>
      private void AddBaseInvariant(CFGBlock fromBlock, CFGBlock toBlock, Subroutine inherited)
      {
        this.AddEdgeSubroutine(fromBlock, toBlock, inherited, "inherited");
      }

      internal override bool IsInvariant
      {
        get { return true; }
      }
      internal override bool IsContract
      {
        get { return true; }
      }
    }

    #endregion // ------------- Nested Types --------------------


    /// <summary>
    /// Try to add a requires to a method. If the method inherits a requires, then this is not possible.
    /// </summary>
    /// <param name="identifying">Identifiying string (e.g. of pre-condition). Used to eliminate dups</param>
    /// <returns>true if added or already present</returns>
    internal bool AddPreCondition(Method method, BoxedExpression boxedExpression, APC apc)
    {
      // TODO: figure out whether we already analyzed a caller to this method. In that case, we can't strengthen the pre-condition.

      Subroutine original = GetRequires(method);

      if (original != null)
      {
        IMethodInfo<Method> mi = original as IMethodInfo<Method>;
        if (mi != null && !this.MetadataDecoder.Equal(mi.Method, method))
        {
          // inherited, can't add
          return false;
        }
      }
      BoxedExpression assertion = new BoxedExpression.AssumeExpression(boxedExpression, "requires", apc);
      SimpleSubroutineBuilder<BoxedExpression.PC> builder = new SimpleSubroutineBuilder<BoxedExpression.PC>(ClousotExpressionCodeProvider<Local, Parameter, Method, Field, Type>.Decoder, ClousotExpressionCodeProvider<Local, Parameter, Method, Field, Type>.Decoder, this, new BoxedExpression.PC(assertion, 0));

      Subroutine newPre = new RequiresSubroutine<BoxedExpression.PC>(this, method, builder, new BoxedExpression.PC(assertion, 0), FunctionalSet<Subroutine>.Empty());
      newPre.Initialize(); // not going through cache and lazy initialized
      if (original == null)
      {
        this.requiresCache.Install(method, newPre);
        return true;
      }
      else
      {
        foreach (CFGBlock pred in original.PredecessorBlocks(original.Exit))
        {
          original.AddEdgeSubroutine(pred, original.Exit, newPre, "extra");
        }
        return true;
      }
    }

    internal bool AddPostCondition(Method method, BoxedExpression boxedExpression, string identifying, APC pc)
    {
      // We do not want to add a postcondition to virtual methods, so we check if method is virtual or not
      if (this.MetadataDecoder.IsVirtual(method))
      {
        return false;
      }

      BoxedExpression post = new BoxedExpression.AssertExpression(boxedExpression, "ensures", pc);

      Subroutine original = GetEnsures(method);
      SimpleSubroutineBuilder<BoxedExpression.PC> builder = new SimpleSubroutineBuilder<BoxedExpression.PC>(ClousotExpressionCodeProvider<Local, Parameter, Method, Field, Type>.Decoder, ClousotExpressionCodeProvider<Local, Parameter, Method, Field, Type>.Decoder, this, new BoxedExpression.PC(post, 0));

      Subroutine newPost = new EnsuresSubroutine<BoxedExpression.PC>(this, method, builder, new BoxedExpression.PC(post, 0), FunctionalSet<Subroutine>.Empty());
      newPost.Initialize();

      if (original == null)
      {
        this.ensuresCache.Install(method, newPost);
        return true;
      }
      else
      {
        foreach (CFGBlock pred in original.PredecessorBlocks(original.Exit))
        {
          original.AddEdgeSubroutine(pred, original.Exit, newPost, "extra");
        }
        return true;
      }

    }

    internal Subroutine BuildSubroutine<Label>(
      int stackDelta,
      ICodeProvider<Label, Method, Type> codeProvider,
      IDecodeMSIL<Label, Local, Parameter, Method, Field, Type, Unit, Unit, Unit> decoder,
      Label entryPoint)
    {
      var sb = new SimpleSubroutineBuilder<Label>(codeProvider, decoder, this, entryPoint);
      return new SimpleSubroutine<Label>(stackDelta, this, entryPoint, sb);
    }

    static IEnumerable<Field> emptyFields = new Field[0];

    internal IEnumerable<Field> GetModifies(Method method)
    {
      Set<Field> result;
      if (methodModifies.TryGetValue(method, out result))
      {
        return result;
      }
      return emptyFields;
    }

    private Set<Field> ModifiesSet(Method method)
    {
      Set<Field> result;
      if (!methodModifies.TryGetValue(method, out result))
      {
        result = new Set<Field>();
        methodModifies.Add(method, result);
      }
      return result;
    }

    internal void AddModifies(Method method, Field field)
    {
      ModifiesSet(method).Add(field);
    }
  }


  public class ControlFlow<Type, Method> : ICFG<Type, APC>
  {

    #region Private

    private readonly Method method;
    private readonly Subroutine methodSubroutine;
    private readonly object methodCache;

    private CFGBlock entry { get { return this.methodSubroutine.Entry; } }
    private CFGBlock normalExit { get { return this.methodSubroutine.Exit; } }
    private CFGBlock exceptionExit { get { return this.methodSubroutine.ExceptionExit; } }

    
    #endregion

    /// <summary>
    /// The control flow class provides an overlay of logical control flow over a syntactic
    /// code structure by making control flow explicit, including block entry/exit, and 
    /// finally block execution.
    /// </summary>
    /// <param name="start">Entry point for method</param>
    //^ [NotDelayed]
    internal ControlFlow(Method method, Subroutine subroutine, object methodCache) {
      this.method = method;
      this.methodSubroutine = subroutine;
      this.consCache = this.contextCache.Cons;
      this.methodCache = methodCache;
    }


    #region ICFG<Code,Label,Handler,APC> Members

    public APC Entry {
      get { return new APC(this.entry, 0, null); }
    }

    public APC EntryAfterRequires
    {
      get { return new APC(this.methodSubroutine.EntryAfterRequires, 0, null); }
    }

    public APC NormalExit
    {
      get { return new APC(this.normalExit, 0, null); }
    }

    public APC ExceptionExit {
      get { return new APC(this.methodSubroutine.ExceptionExit, 0, null); }
    }

    private static IEnumerable<APC> EmptySucc = new APC[0];

    public bool HasSingleSuccessor(APC ppoint, out APC next) {
      return ppoint.Block.Subroutine.HasSingleSuccessor(ppoint, out next, this.consCache);
    }

    public bool HasSinglePredecessor(APC ppoint, out APC next)
    {
      return ppoint.Block.Subroutine.HasSinglePredecessor(ppoint, out next, this.consCache);
    }

    public APC PredecessorPCPriorToRequires(APC ppoint)
    {
      return ppoint.Block.Subroutine.PredecessorPCPriorToRequires(ppoint, this.consCache);
    }

    /// <summary>
    /// Returns all normal control flow successors
    /// </summary>
    public IEnumerable<APC>/*!*/ Successors(APC ppoint) {
      return ppoint.Block.Subroutine.Successors(ppoint, this.consCache);
    }


    public IEnumerable<APC> Predecessors(APC ppoint) {
      return ppoint.Block.Subroutine.Predecessors(ppoint, this.consCache);
    }

    /// <summary>
    /// Used to hash cons subroutine context edge lists
    /// </summary>
    private readonly SubroutineContextCache contextCache = new SubroutineContextCache();
    private readonly DConsCache consCache;

    private class SubroutineContextCache
    {
      private DoubleTable<SubroutineEdge, SubroutineContext, SubroutineContext> subroutineContextHashMap = new DoubleTable<Tuple<CFGBlock, CFGBlock, string>, FList<Tuple<CFGBlock, CFGBlock, string>>, FList<Tuple<CFGBlock, CFGBlock, string>>>();
      private Dictionary<SubroutineEdge, SubroutineContext> subroutineContextHashMapSingleton = new Dictionary<Tuple<CFGBlock, CFGBlock, string>, FList<Tuple<CFGBlock, CFGBlock, string>>>();

      public SubroutineContext Cons(SubroutineEdge edge, SubroutineContext tail)
      {
        SubroutineContext/*?*/ result;
        if (tail != null)
        {
          Debug.Assert(tail.Head.One.Subroutine != edge.One.Subroutine);
          if (!this.subroutineContextHashMap.TryGetValue(edge, tail, out result))
          {
            result = SubroutineContext.Cons(edge, tail);
            this.subroutineContextHashMap.Add(edge, tail, result);
          }
        }
        else
        {
          if (!this.subroutineContextHashMapSingleton.TryGetValue(edge, out result))
          {
            result = SubroutineContext.Cons(edge, null);
            this.subroutineContextHashMapSingleton[edge] = result;
          }
        }
        return result;
      }
    }

    /// <summary>
    /// Returns a set of program points that are possible execution continuations if 
    /// an exception is raised at the given ppoint.
    ///
    /// The supplied predicate allows the client to determine which handlers are possible
    /// candidates. The predicate is called from closest enclosing to outermost.
    /// </summary>
    /// <param name="data">Arbitrary client data passed to the handler predicate</param>
    /// <returns>A set of continuation program points that represent how execution continues to a chosen handler.
    /// The continuation includes Finally and Fault executions that intervene between the given ppoint and a
    /// chosen handler.
    /// </returns>
    public IEnumerable<APC> ExceptionHandlers<Data>(APC ppoint, Data data, IHandlerFilter<APC, Type, Data> handlerPredicate)
    {

      // This is a cross-subroutine operation. We need to ask for the handlers guarding the current block,
      // followed by the handlers guarding the source of each edge on the subroutine stack.

      // Ask the innermost subroutine for its handler continuations
      bool escapesFromSubroutine = false;
      foreach (CFGBlock handlerBlock in ppoint.Block.Subroutine.ExceptionHandlers(ppoint.Block, null, data, handlerPredicate))
      {
        if (handlerBlock != ppoint.Block.Subroutine.ExceptionExit)
        {
          yield return ppoint.Block.Subroutine.ComputeTargetFinallyContext(ppoint, handlerBlock, this.consCache);
        }
        else
        {
          escapesFromSubroutine = true;
        }
      }

      if (!escapesFromSubroutine)
      {
        yield break; // no more outer handlers needed
      }


      // Now find handlers in surrounding subroutines, from inner to outer.
      //
      SubroutineContext outerContext = ppoint.SubroutineContext;
      FList<Pair<string,CFGBlock>> innerAbortingContext = null;
      APC innermostAbortingAPC = ppoint.Block.Subroutine.ComputeTargetFinallyContext(ppoint.WithoutContext, ppoint.Block.Subroutine.ExceptionExit, this.consCache);
      SubroutineContext innerMostReversedContext = SubroutineContext.Reverse(innermostAbortingAPC.SubroutineContext);
      Subroutine innerSubroutine = ppoint.Block.Subroutine;
      while (outerContext != null)
      {
        escapesFromSubroutine = false;

        CFGBlock from = outerContext.Head.One;
        foreach (CFGBlock handlerBlock in from.Subroutine.ExceptionHandlers(from, innerSubroutine, data, handlerPredicate))
        {
          if (handlerBlock == from.Subroutine.ExceptionExit)
          {
            escapesFromSubroutine = true;
          }
          else
          {
            yield return this.ComputeExceptionTarget(innermostAbortingAPC.Block, innermostAbortingAPC.Index, innerMostReversedContext,
                                      innerAbortingContext,
                                      this.consCache(new SubroutineEdge(from, handlerBlock, "exception"), outerContext.Tail),
                                      this.consCache);
          }
        }
        if (!escapesFromSubroutine)
        {
          yield break; // no more outer handlers needed
        }
        innerAbortingContext = FList<Pair<string,CFGBlock>>.Cons(new Pair<string,CFGBlock>(outerContext.Head.Three, from), innerAbortingContext);
        outerContext = outerContext.Tail;
      }
      // if we get here, we are throwing out of the outermost context
      yield return this.ComputeExceptionTarget(innermostAbortingAPC.Block, innermostAbortingAPC.Index, innerMostReversedContext,
                                innerAbortingContext,
                                null,
                                this.consCache);
    }

    /// <summary>
    /// Computes new APC corresponding to a concatenation of exectuion from the current abortPoint (and its subroutine execution, provided in reversed from as innerMostReversedContext)
    /// with the innerAbortingContext and the outerAbortingContext.
    /// The innerAbortingContext is a set of blocks from outer to inner surrounding the abort point.
    /// The resulting APC starts with the abort point and the innermost context (in proper order), followed by (x,exit_x) edges for all points in the innerAbortingContext (from inner to outer)
    /// This entire context is pre-pended to the outer subroutine context.
    /// </summary>
    private APC ComputeExceptionTarget(CFGBlock abortPoint, int abortIndex, 
                                       SubroutineContext innerMostReversedContext, 
                                       FList<Pair<string,CFGBlock>> innerAbortingContext,
                                       SubroutineContext outerSubroutineContext,
                                       DConsCache consCache)
    {
      while (innerAbortingContext != null) {
        outerSubroutineContext = this.consCache(new SubroutineEdge(innerAbortingContext.Head.Two, innerAbortingContext.Head.Two.Subroutine.ExceptionExit, innerAbortingContext.Head.One), outerSubroutineContext);
        innerAbortingContext = innerAbortingContext.Tail;
      }
      while (innerMostReversedContext != null)
      {
        outerSubroutineContext = this.consCache(innerMostReversedContext.Head, outerSubroutineContext);
        innerMostReversedContext = innerMostReversedContext.Tail;
      }
      return new APC(abortPoint, abortIndex, outerSubroutineContext);
    }



    public void Print(TextWriter tw, ILPrinter<APC> ilPrinter, BlockInfoPrinter<APC>/*?*/ blockInfoPrinter, Func<CFGBlock, IEnumerable<SubroutineContext>> contextLookup, SubroutineContext context) {
      var printed = new Set<Pair<Subroutine, SubroutineContext>>();
      this.methodSubroutine.Print(tw, ilPrinter, blockInfoPrinter, contextLookup, context, printed);
    }

    public void EmitTransferEquations(TextWriter tw, InvariantQuery<APC> invariantDB, AssumptionFinder<APC> assumptionfinder, 
      CrossBlockRenamings<APC> renamings, RenamedVariables<APC> renamed)
    {
      this.methodSubroutine.EmitTransferEquations(tw, invariantDB, assumptionfinder, renamings, renamed);
    }

    private string FormatSourceContext(string doc, int line, int col) {
      return String.Format("{0}({1},{2})", doc, line, col);
    }


    public Subroutine Subroutine { get { return this.methodSubroutine; } }

    public bool IsJoinPoint(CFGBlock block) {
      return block.Subroutine.IsJoinPoint(block);
    }

    public bool IsJoinPoint(APC ppoint) {
      if (ppoint.Index != 0) return false;
      return IsJoinPoint(ppoint.Block);
    }

    public bool IsSplitPoint(CFGBlock block) {
      return block.Subroutine.IsSplitPoint(block);
    }

    public bool IsSplitPoint(APC ppoint)
    {
      if (ppoint.Index != ppoint.Block.Count) return false;

      return IsSplitPoint(ppoint.Block);
    }

    public bool IsForwardBackEdgeTarget(APC ppoint) {
      CFGBlock block = ppoint.Block;
      if (ppoint.Index != 0) return false; // not at beginning.
      return ppoint.Block.Subroutine.EdgeInfo.DepthFirstInfo(block).TargetOfBackEdge;
    }

    public bool IsBackwardBackEdgeTarget(APC ppoint) {
      CFGBlock block = ppoint.Block;
      if (ppoint.Index != block.Count) return false; // not at end.
      return ppoint.Block.Subroutine.EdgeInfo.DepthFirstInfo(block).SourceOfBackEdge;
    }

    public bool IsForwardBackEdge(APC from, APC to) {
      if (to.Index != 0) return false;
      return IsForwardBackEdgeHelper(from, to);
    }

    public bool IsBackwardBackEdge(APC from, APC to) {
      if (from.Index != 0) return false;
      return IsBackwardBackEdgeHelper(from, to);
    }

    private bool IsForwardBackEdgeHelper(APC from, APC to) {
      if (to.Block.Subroutine.EdgeInfo.IsBackEdge(from.Block, Unit.Value, to.Block)) return true;

      // in case of an end finally we consult the original edge
      // If this is an endfinally, there must be at least one label
      if (from.SubroutineContext != null && from.SubroutineContext.Tail == to.SubroutineContext) {
        // it was a pop
        SubroutineEdge edge = from.SubroutineContext.Head;
        return edge.Two.Subroutine.EdgeInfo.IsBackEdge(edge.One, Unit.Value, edge.Two);
      }
      return false;
    }

    private bool IsBackwardBackEdgeHelper(APC from, APC to) {
      // use symmetry
      if (from.Block.Subroutine.EdgeInfo.IsBackEdge(to.Block, Unit.Value, from.Block)) return true;

      // in case of an end finally we consult the original edge
      // If this is an endfinally, there must be at least one label
      if (to.SubroutineContext != null && to.SubroutineContext.Tail == from.SubroutineContext) {
        // it was a pop
        SubroutineEdge reversedEdge = to.SubroutineContext.Head;
        // ask reversed question
        return reversedEdge.One.Subroutine.EdgeInfo.IsBackEdge(reversedEdge.Two, Unit.Value, reversedEdge.One);
      }
      return false;
    }


    public bool IsBlockStart(APC ppoint) {
      return (ppoint.Index == 0);
    }

    public bool IsBlockEnd(APC ppoint) {
      return (ppoint.Index == ppoint.Block.Count);
    }

    #endregion


    #region IGraph of APCs (forward only)

    private IGraph<APC, Unit>/*?*/ forwardGraphCache = null;

    public IGraph<APC, Unit> AsGraph {
      get {
        if (forwardGraphCache == null) {
          forwardGraphCache = new GraphWrapper<APC, Unit>(
            new APC[0], // we don't provide the set of all apcs
            SuccessorEdges);
        }
        return forwardGraphCache;
      }
    }

    IEnumerable<Pair<Unit, APC>> SuccessorEdges(APC pc) {
      foreach (APC succ in this.Successors(pc)) {
        yield return new Pair<Unit, APC>(Unit.Value, succ);
      }

      foreach (APC succ in this.ExceptionHandlers(pc, 0, null)) {
        yield return new Pair<Unit, APC>(Unit.Value, succ);
      }

    }

    #endregion


    #region ICFG<Label,Handler,APC> Members

    public IDecodeMSIL<APC, Local, Parameter, Method2, Field, Type, Unit, Unit, IMethodContext<APC,Field,Method2>>
      GetDecoder<Local, Parameter, Method2, Field, Property, Attribute, Assembly>(IDecodeMetaData<Local, Parameter, Method2, Field, Property, Type, Attribute, Assembly> mdDecoder)
    {
      MethodCache<Local, Parameter, Type, Method2, Field, Property, Attribute, Assembly> mcache =
        this.methodCache as MethodCache<Local, Parameter, Type, Method2, Field, Property, Attribute, Assembly>;
      return new APCDecoder<Local, Parameter, Method2, Field, Property, Attribute, Assembly>(this, mdDecoder, mcache);
    }

    /// <summary>
    /// We know that Source and Dest are Unit
    /// </summary>
    private class APCDecoder<Local, Parameter, Method2, Field, Property, Attribute, Assembly> :
      IDecodeMSIL<APC, Local, Parameter, Method2, Field, Type, UnitSource, UnitDest, IMethodContext<APC,Field,Method2>>,
      IMethodContext<APC, Field, Method2>
    {
      ControlFlow<Type, Method> parent;
      IDecodeMetaData<Local, Parameter, Method2, Field, Property, Type, Attribute, Assembly> mdDecoder;
      MethodCache<Local, Parameter, Type, Method2, Field, Property, Attribute, Assembly> methodCache;

      public APCDecoder(
        ControlFlow<Type, Method> parent,
        IDecodeMetaData<Local, Parameter, Method2, Field, Property, Type, Attribute, Assembly> mdDecoder,
        MethodCache<Local, Parameter, Type, Method2, Field, Property, Attribute, Assembly> methodCache
      )
      {
        this.parent = parent;
        this.mdDecoder = mdDecoder;
        this.methodCache = methodCache;
      }


      #region IMSILDecoder<APC,Local,Parameter,Method2,Field,Type,Source,Dest> Members

      /// <summary>
      /// Removes unconditional branches and branch true, branch false.
      /// Turns binary conditional branches into their condition evaluation
      /// </summary>
      struct RemoveBranchDelegator<Data, Result,Visitor> :
        IVisitMSIL<APC, Local, Parameter, Method2, Field, Type, UnitSource, UnitDest, Data, Result>
        where Visitor : IVisitMSIL<APC, Local, Parameter, Method2, Field, Type, UnitSource, UnitDest, Data, Result>
      {
        Visitor visitor;
        IDecodeMetaData<Local, Parameter, Method2, Field, Property, Type, Attribute, Assembly> mdDecoder;

        public RemoveBranchDelegator(
          Visitor visitor,
          IDecodeMetaData<Local, Parameter, Method2, Field, Property, Type, Attribute, Assembly> mdDecoder
        )
        {
          this.visitor = visitor;
          this.mdDecoder = mdDecoder;
        }

        private Method2 GetExecutingMethod(APC apc)
        {
          SubroutineContext context = apc.SubroutineContext;
          while (context != null)
          {
            if (context.Head.One.Subroutine.IsMethod) {
              IMethodInfo<Method2> msr = (IMethodInfo<Method2>)context.Head.One.Subroutine;
              return msr.Method;
            }
            context = context.Tail;
          }
          throw new ApplicationException("No executing method found in apc");
        }


        /// <summary>
        /// If original block is a method contract block, then invert assume/assert
        /// </summary>
        public Result Assume(APC pc, string tag, UnitSource source, Data data)
        {
          if ((tag == "requires" && pc.InsideRequiresAtCall) || (tag == "invariant" && pc.InsideInvariantOnExit))
          {
            return visitor.Assert(pc, tag, source, data);
          }
          return visitor.Assume(pc, tag, source, data);
        }


        /// <summary>
        /// If original block is a method contract block, then invert assume/assert
        /// </summary>
        public Result Assert(APC pc, string tag, UnitSource source, Data data)
        {
          if (pc.InsideEnsuresAtCall)
          {
            return visitor.Assume(pc, tag, source, data);
          }
          return visitor.Assert(pc, tag, source, data);
        }

        public Result Arglist(APC pc, UnitDest dest, Data data)
        {
          return visitor.Arglist(pc, dest, data);
        }

        public Result Binary(APC pc, BinaryOperator op, UnitDest dest, UnitSource s1, UnitSource s2, Data data)
        {
          return visitor.Binary(pc, op, dest, s1, s2, data);
        }

        public Result BranchCond(APC pc, APC target, BranchOperator bop, UnitSource value1, UnitSource value2, Data data)
        {
          UnitDest dest = UnitDest.Value;
          switch (bop) {
            case BranchOperator.Beq:
              // equal to Ceq followed by br.true
              return this.visitor.Binary(pc, BinaryOperator.Ceq, dest, value1, value2, data);

            case BranchOperator.Bge:
              // equal to Cge followed by br.true
              return this.visitor.Binary(pc, BinaryOperator.Cge, dest, value1, value2, data);

            case BranchOperator.Bge_un:
              // equal to Cge_un followed by br.true
              return this.visitor.Binary(pc, BinaryOperator.Cge_Un, dest, value1, value2, data);

            case BranchOperator.Bgt:
              // equal to Cgt followed by br.true
              return this.visitor.Binary(pc, BinaryOperator.Cgt, dest, value1, value2, data);

            case BranchOperator.Bgt_un:
              // equal to Cgt_un followed by br.true
              return this.visitor.Binary(pc, BinaryOperator.Cgt_Un, dest, value1, value2, data);

            case BranchOperator.Ble:
              // equal to Cle followed by br.true
              return this.visitor.Binary(pc, BinaryOperator.Cle, dest, value1, value2, data);

            case BranchOperator.Ble_un:
              // equal to Cle_un followed by br.true
              return this.visitor.Binary(pc, BinaryOperator.Cle_Un, dest, value1, value2, data);

            case BranchOperator.Blt:
              // equal to Clt followed by br.true
              return this.visitor.Binary(pc, BinaryOperator.Clt, dest, value1, value2, data);

            case BranchOperator.Blt_un:
              // equal to Clt_un followed by br.true
              return this.visitor.Binary(pc, BinaryOperator.Clt_Un, dest, value1, value2, data);

            case BranchOperator.Bne_un:
              // equal to Cne_un followed by br.true
              return this.visitor.Binary(pc, BinaryOperator.Cne_Un, dest, value1, value2, data);

              
              
          }
          return this.visitor.Nop(pc, data);
        }

        public Result BranchTrue(APC pc, APC target, UnitSource cond, Data data) {
          return this.visitor.Nop(pc, data);
        }

        public Result BranchFalse(APC pc, APC target, UnitSource cond, Data data) {
          return this.visitor.Nop(pc, data);
        }

        public Result Branch(APC pc, APC target, bool leave, Data data)
        {
          return this.visitor.Nop(pc, data);
        }

        public Result Break(APC pc, Data data)
        {
          return visitor.Break(pc, data);
        }

        public Result Call<TypeList,ArgList>(APC pc, Method2 method, bool tail, bool virt, TypeList extraVarargs, UnitDest dest, ArgList args, Data data)
          where TypeList : IIndexable<Type>
          where ArgList : IIndexable<UnitSource>
        {
          Type declaringType = mdDecoder.DeclaringType(method);
          if (args.Count == 2 &&
              mdDecoder.IsStatic(method) &&
              mdDecoder.Equal(declaringType, mdDecoder.System_Object) &&
              mdDecoder.Name(method) == "ReferenceEquals"
              )
          {
            // special case equal here
            return visitor.Binary(pc, BinaryOperator.Ceq, dest, args[0], args[1], data);
          }

          return visitor.Call(pc, method, tail, virt, extraVarargs, dest, args, data);
        }

        public Result Calli<TypeList,ArgList>(APC pc, Type returnType, TypeList argTypes, bool tail, UnitDest dest, UnitSource fp, ArgList args, Data data)
          where TypeList : IIndexable<Type>
          where ArgList : IIndexable<UnitSource>
        {
          return visitor.Calli(pc, returnType, argTypes, tail, dest, fp, args, data);
        }

        public Result Ckfinite(APC pc, UnitDest dest, UnitSource source, Data data)
        {
          return visitor.Ckfinite(pc, dest, source, data);
        }

        public Result Cpblk(APC pc, bool @volatile, UnitSource destaddr, UnitSource srcaddr, UnitSource len, Data data)
        {
          return visitor.Cpblk(pc, @volatile, destaddr, srcaddr, len, data);
        }

        public Result Endfilter(APC pc, UnitSource decision, Data data)
        {
          return visitor.Endfilter(pc, decision, data);
        }

        public Result Endfinally(APC pc, Data data)
        {
          return visitor.Endfinally(pc, data);
        }

        public Result Entry(APC pc, Method2 method, Data data)
        {
          return visitor.Entry(pc, method, data);
        }

        public Result Initblk(APC pc, bool @volatile, UnitSource destaddr, UnitSource value, UnitSource len, Data data)
        {
          return visitor.Initblk(pc, @volatile, destaddr, value, len, data);
        }

        public Result Jmp(APC pc, Method2 method, Data data)
        {
          return visitor.Jmp(pc, method, data);
        }

        public Result Ldarg(APC pc, Parameter argument, bool isOld, UnitDest dest, Data data)
        {
          return visitor.Ldarg(pc, argument, isOld, dest, data);
        }

        public Result Ldarga(APC pc, Parameter argument, bool isOld, UnitDest dest, Data data)
        {
          return visitor.Ldarga(pc, argument, isOld, dest, data);
        }

        public Result Ldconst(APC pc, object constant, Type type, UnitDest dest, Data data)
        {
          return visitor.Ldconst(pc, constant, type, dest, data);
        }

        public Result Ldnull(APC pc, UnitDest dest, Data data)
        {
          return visitor.Ldnull(pc, dest, data);
        }

        public Result Ldftn(APC pc, Method2 method, UnitDest dest, Data data)
        {
          return visitor.Ldftn(pc, method, dest, data);
        }

        public Result Ldind(APC pc, Type type, bool @volatile, UnitDest dest, UnitSource ptr, Data data)
        {
          return visitor.Ldind(pc, type, @volatile, dest, ptr, data);
        }

        public Result Ldloc(APC pc, Local local, UnitDest dest, Data data)
        {
          return visitor.Ldloc(pc, local, dest, data);
        }

        public Result Ldloca(APC pc, Local local, UnitDest dest, Data data)
        {
          return visitor.Ldloca(pc, local, dest, data);
        }

        public Result Ldstack(APC pc, int offset, UnitDest dest, UnitSource source, bool isOld, Data data)
        {
          return visitor.Ldstack(pc, offset, dest, source, isOld, data);
        }

        public Result Ldstacka(APC pc, int offset, UnitDest dest, UnitSource source, Type type, bool isOld, Data data)
        {
          return visitor.Ldstacka(pc, offset, dest, source, type, isOld, data);
        }

        public Result Localloc(APC pc, UnitDest dest, UnitSource size, Data data)
        {
          return visitor.Localloc(pc, dest, size, data);
        }

        public Result Nop(APC pc, Data data)
        {
          return visitor.Nop(pc, data);
        }

        public Result Pop(APC pc, UnitSource source, Data data)
        {
          return visitor.Pop(pc, source, data);
        }

        public Result Return(APC pc, UnitSource source, Data data)
        {
          return visitor.Return(pc, source, data);
        }

        public Result Starg(APC pc, Parameter argument, UnitSource source, Data data)
        {
          return visitor.Starg(pc, argument, source, data);
        }

        public Result Stind(APC pc, Type type, bool @volatile, UnitSource ptr, UnitSource value, Data data)
        {
          return visitor.Stind(pc, type, @volatile, ptr, value, data);
        }

        public Result Stloc(APC pc, Local local, UnitSource source, Data data)
        {
          return visitor.Stloc(pc, local, source, data);
        }

        public Result Switch(APC pc, Type type, IEnumerable<APC> cases, UnitSource value, Data data)
        {
          return visitor.Nop(pc, data);
        }

        public Result Unary(APC pc, UnaryOperator op, bool overflow, bool unsigned, UnitDest dest, UnitSource source, Data data)
        {
          return visitor.Unary(pc, op, overflow, unsigned, dest, source, data);
        }

        public Result Box(APC pc, Type type, UnitDest dest, UnitSource source, Data data)
        {
          return visitor.Box(pc, type, dest, source, data);
        }

        public Result ConstrainedCallvirt<TypeList,ArgList>(APC pc, Method2 method, bool tail, Type constraint, TypeList extraVarargs, UnitDest dest, ArgList args, Data data)
          where TypeList : IIndexable<Type>
          where ArgList : IIndexable<UnitSource>
        {
          return visitor.ConstrainedCallvirt(pc, method, tail, constraint, extraVarargs, dest, args, data);
        }

        public Result Castclass(APC pc, Type type, UnitDest dest, UnitSource obj, Data data)
        {
          return visitor.Castclass(pc, type, dest, obj, data);
        }

        public Result Cpobj(APC pc, Type type, UnitSource destptr, UnitSource srcptr, Data data)
        {
          return visitor.Cpobj(pc, type, destptr, srcptr, data);
        }

        public Result Initobj(APC pc, Type type, UnitSource ptr, Data data)
        {
          return visitor.Initobj(pc, type, ptr, data);
        }

        public Result Isinst(APC pc, Type type, UnitDest dest, UnitSource obj, Data data)
        {
          return visitor.Isinst(pc, type, dest, obj, data);
        }

        public Result Ldelem(APC pc, Type type, UnitDest dest, UnitSource array, UnitSource index, Data data)
        {
          return visitor.Ldelem(pc, type, dest, array, index, data);
        }

        public Result Ldelema(APC pc, Type type, bool @readonly, UnitDest dest, UnitSource array, UnitSource index, Data data)
        {
          return visitor.Ldelema(pc, type, @readonly, dest, array, index, data);
        }

        public Result Ldfld(APC pc, Field field, bool @volatile, UnitDest dest, UnitSource obj, Data data)
        {
          return visitor.Ldfld(pc, field, @volatile, dest, obj, data);
        }

        public Result Ldflda(APC pc, Field field, UnitDest dest, UnitSource obj, Data data)
        {
          return visitor.Ldflda(pc, field, dest, obj, data);
        }

        public Result Ldlen(APC pc, UnitDest dest, UnitSource array, Data data)
        {
          return visitor.Ldlen(pc, dest, array, data);
        }

        public Result Ldsfld(APC pc, Field field, bool @volatile, UnitDest dest, Data data)
        {
          return visitor.Ldsfld(pc, field, @volatile, dest, data);
        }

        public Result Ldsflda(APC pc, Field field, UnitDest dest, Data data)
        {
          return visitor.Ldsflda(pc, field, dest, data);
        }

        public Result Ldtypetoken(APC pc, Type type, UnitDest dest, Data data)
        {
          return visitor.Ldtypetoken(pc, type, dest, data);
        }

        public Result Ldfieldtoken(APC pc, Field field, UnitDest dest, Data data)
        {
          return visitor.Ldfieldtoken(pc, field, dest, data);
        }

        public Result Ldmethodtoken(APC pc, Method2 method, UnitDest dest, Data data)
        {
          return visitor.Ldmethodtoken(pc, method, dest, data);
        }

        public Result Ldvirtftn(APC pc, Method2 method, UnitDest dest, UnitSource obj, Data data)
        {
          return visitor.Ldvirtftn(pc, method, dest, obj, data);
        }

        public Result Mkrefany(APC pc, Type type, UnitDest dest, UnitSource obj, Data data)
        {
          return visitor.Mkrefany(pc, type, dest, obj, data);
        }

        public Result Newarray<ArgList>(APC pc, Type type, UnitDest dest, ArgList lengths, Data data)
          where ArgList : IIndexable<UnitSource>
        {
          return visitor.Newarray(pc, type, dest, lengths, data);
        }

        public Result Newobj<ArgList>(APC pc, Method2 ctor, UnitDest dest, ArgList args, Data data)
          where ArgList : IIndexable<UnitSource>
        {
          return visitor.Newobj(pc, ctor, dest, args, data);
        }

        public Result Refanytype(APC pc, UnitDest dest, UnitSource source, Data data)
        {
          return visitor.Refanytype(pc, dest, source, data);
        }

        public Result Refanyval(APC pc, Type type, UnitDest dest, UnitSource source, Data data)
        {
          return visitor.Refanyval(pc, type, dest, source, data);
        }

        public Result Rethrow(APC pc, Data data)
        {
          return visitor.Rethrow(pc, data);
        }

        public Result Sizeof(APC pc, Type type, UnitDest dest, Data data)
        {
          return visitor.Sizeof(pc, type, dest, data);
        }

        public Result Stelem(APC pc, Type type, UnitSource array, UnitSource index, UnitSource value, Data data)
        {
          return visitor.Stelem(pc, type, array, index, value, data);
        }

        public Result Stfld(APC pc, Field field, bool @volatile, UnitSource obj, UnitSource value, Data data)
        {
          return visitor.Stfld(pc, field, @volatile, obj, value, data);
        }

        public Result Stsfld(APC pc, Field field, bool @volatile, UnitSource value, Data data)
        {
          return visitor.Stsfld(pc, field, @volatile, value, data);
        }

        public Result Throw(APC pc, UnitSource exn, Data data)
        {
          return visitor.Throw(pc, exn, data);
        }

        public Result Unbox(APC pc, Type type, UnitDest dest, UnitSource obj, Data data)
        {
          return visitor.Unbox(pc, type, dest, obj, data);
        }

        public Result Unboxany(APC pc, Type type, UnitDest dest, UnitSource obj, Data data)
        {
          return visitor.Unboxany(pc, type, dest, obj, data);
        }


        #region IVisitSynthIL<Label,Method2,Source,Dest,Data,Result> Members


        public Result BeginOld(APC pc, APC matchingEnd, Data data)
        {
          return visitor.BeginOld(pc, matchingEnd, data);
        }

        public Result EndOld(APC pc, APC matchingBegin, Type type, UnitDest dest, UnitSource source, Data data)
        {
          return visitor.EndOld(pc, matchingBegin, type, dest, source, data);
        }

        public Result Ldresult(APC pc, UnitDest dest, UnitSource source, Data data)
        {
          return visitor.Ldresult(pc, dest, source, data);
        }

        #endregion
      }

#if false
      struct AssumeDelegator<Data, Result, Visitor> :
        IVisitMSIL<Label, Local, Parameter, Method2, Field, Type, UnitSource, UnitDest, Data, Result>
        where Visitor : IVisitMSIL<APC, Local, Parameter, Method2, Field, Type, UnitSource, UnitDest, Data, Result>
      {
        Visitor visitor;
        Tag assumeLabel;
        APC originalPC;

        public AssumeDelegator(
          Visitor visitor,
          Tag assumeLabel,
          APC origPC)
        {
          this.visitor = visitor;
          this.assumeLabel = assumeLabel;
          this.originalPC = origPC;
        }

        APC ConvertLabel(Label pc)
        {
          // we return the stored pc here, as the underlying decoder only dealt with a label
          // this works because we never translate branch target labels.
          return this.originalPC;
        }

        /// <summary>
        /// Map a label occurring in a begin_old/end_old to an apc.
        /// </summary>
        private APC ConvertMatchingLabel(Label underlying)
        {
          MethodCache<Local, Parameter, Type, Method, Field, Label, Handler>.EnsuresSubroutine subroutine = (MethodCache<Local, Parameter, Type, Method, Field, Label, Handler>.EnsuresSubroutine)this.originalPC.Block.Subroutine;

          return subroutine.MapLabelToAPC(underlying, this.originalPC.SubroutineContext);
        }


        public Result Assert(Label pc, string tag, UnitSource condition, Data data)
        {
          return visitor.Assert(ConvertLabel(pc), tag, condition, data);
        }

        public Result Assume(Label pc, string tag, UnitSource source, Data data)
        {
          return visitor.Assume(ConvertLabel(pc), tag, source, data);
        }

        public Result Arglist(Label pc, UnitDest dest, Data data)
        {
          return visitor.Arglist(ConvertLabel(pc), dest, data);
        }

        public Result Binary(Label pc, BinaryOperator op, UnitDest dest, UnitSource s1, UnitSource s2, Data data)
        {
          return visitor.Binary(ConvertLabel(pc), op, dest, s1, s2, data);
        }

        public Result BranchCond(Label pc, Label target, BranchOperator bop, UnitSource value1, UnitSource value2, Data data)
        {
          // note: all binary branch conditions are evaluated to yield a value in value2 and use br.true
          return this.visitor.Assume(this.originalPC, this.assumeLabel, value2, data);
        }

        public Result BranchTrue(Label pc, Label target, UnitSource cond, Data data) {
          return this.visitor.Assume(this.originalPC, this.assumeLabel, cond, data);
        }

        public Result BranchFalse(Label pc, Label target, UnitSource cond, Data data) {
          return this.visitor.Assume(this.originalPC, this.assumeLabel, cond, data);
        }

        public Result Branch(Label pc, Label target, bool leave, Data data)
        {
          return this.visitor.Nop(this.originalPC, data);
        }

        public Result Break(Label pc, Data data)
        {
          return visitor.Break(ConvertLabel(pc), data);
        }

        public Result Call<TypeList,ArgList>(Label pc, Method2 method, bool tail, bool virt, TypeList extraVarargs, UnitDest dest, ArgList args, Data data)
          where TypeList : IIndexable<Type>
          where ArgList : IIndexable<UnitSource>
        {
          return visitor.Call(ConvertLabel(pc), method, tail, virt, extraVarargs, dest, args, data);
        }

        public Result Calli<TypeList,ArgList>(Label pc, Type returnType, TypeList argTypes, bool tail, UnitDest dest, UnitSource fp, ArgList args, Data data)
          where TypeList : IIndexable<Type>
          where ArgList : IIndexable<UnitSource>
        {
          return visitor.Calli(ConvertLabel(pc), returnType, argTypes, tail, dest, fp, args, data);
        }

        public Result Ckfinite(Label pc, UnitDest dest, UnitSource source, Data data)
        {
          return visitor.Ckfinite(ConvertLabel(pc), dest, source, data);
        }

        public Result Cpblk(Label pc, bool @volatile, UnitSource destaddr, UnitSource srcaddr, UnitSource len, Data data)
        {
          return visitor.Cpblk(ConvertLabel(pc), @volatile, destaddr, srcaddr, len, data);
        }

        public Result Endfilter(Label pc, UnitSource decision, Data data)
        {
          return visitor.Endfilter(ConvertLabel(pc), decision, data);
        }

        public Result Endfinally(Label pc, Data data)
        {
          return visitor.Endfinally(ConvertLabel(pc), data);
        }

        public Result Entry(Label pc, Method2 method, Data data)
        {
          return visitor.Entry(ConvertLabel(pc), method, data);
        }

        public Result Initblk(Label pc, bool @volatile, UnitSource destaddr, UnitSource value, UnitSource len, Data data)
        {
          return visitor.Initblk(ConvertLabel(pc), @volatile, destaddr, value, len, data);
        }

        public Result Jmp(Label pc, Method2 method, Data data)
        {
          return visitor.Jmp(ConvertLabel(pc), method, data);
        }

        public Result Ldarg(Label pc, Parameter argument, UnitDest dest, Data data)
        {
          return visitor.Ldarg(ConvertLabel(pc), argument, dest, data);
        }

        public Result Ldarga(Label pc, Parameter argument, UnitDest dest, Data data)
        {
          return visitor.Ldarga(ConvertLabel(pc), argument, dest, data);
        }

        public Result Ldconst(Label pc, object constant, Type type, UnitDest dest, Data data)
        {
          return visitor.Ldconst(ConvertLabel(pc), constant, type, dest, data);
        }

        public Result Ldnull(Label pc, UnitDest dest, Data data)
        {
          return visitor.Ldnull(ConvertLabel(pc), dest, data);
        }

        public Result Ldftn(Label pc, Method2 method, UnitDest dest, Data data)
        {
          return visitor.Ldftn(ConvertLabel(pc), method, dest, data);
        }

        public Result Ldind(Label pc, Type type, bool @volatile, UnitDest dest, UnitSource ptr, Data data)
        {
          return visitor.Ldind(ConvertLabel(pc), type, @volatile, dest, ptr, data);
        }

        public Result Ldloc(Label pc, Local local, UnitDest dest, Data data)
        {
          return visitor.Ldloc(ConvertLabel(pc), local, dest, data);
        }

        public Result Ldloca(Label pc, Local local, UnitDest dest, Data data)
        {
          return visitor.Ldloca(ConvertLabel(pc), local, dest, data);
        }

        public Result Ldstack(Label pc, int offset, UnitDest dest, UnitSource source, Data data)
        {
          return visitor.Ldstack(ConvertLabel(pc), offset, dest, source, data);
        }

        public Result Localloc(Label pc, UnitDest dest, UnitSource size, Data data)
        {
          return visitor.Localloc(ConvertLabel(pc), dest, size, data);
        }

        public Result Nop(Label pc, Data data)
        {
          return visitor.Nop(ConvertLabel(pc), data);
        }

        public Result Pop(Label pc, UnitSource source, Data data)
        {
          return visitor.Pop(ConvertLabel(pc), source, data);
        }

        public Result Return(Label pc, UnitSource source, Data data)
        {
          return visitor.Return(ConvertLabel(pc), source, data);
        }

        public Result Starg(Label pc, Parameter argument, UnitSource source, Data data)
        {
          return visitor.Starg(ConvertLabel(pc), argument, source, data);
        }

        public Result Stind(Label pc, Type type, bool @volatile, UnitSource ptr, UnitSource value, Data data)
        {
          return visitor.Stind(ConvertLabel(pc), type, @volatile, ptr, value, data);
        }

        public Result Stloc(Label pc, Local local, UnitSource source, Data data)
        {
          return visitor.Stloc(ConvertLabel(pc), local, source, data);
        }

        public Result Switch(Label pc, IEnumerable<Label> cases, UnitSource value, Data data)
        {
          string tag = this.assumeLabel == "false" ? "default" : this.assumeLabel;
          return visitor.Assume(this.originalPC, tag, value, data);
        }

        public Result Unary(Label pc, UnaryOperator op, bool overflow, bool unsigned, UnitDest dest, UnitSource source, Data data)
        {
          return visitor.Unary(ConvertLabel(pc), op, overflow, unsigned, dest, source, data);
        }

        public Result Box(Label pc, Type type, UnitDest dest, UnitSource source, Data data)
        {
          return visitor.Box(ConvertLabel(pc), type, dest, source, data);
        }

        public Result ConstrainedCallvirt<TypeList,ArgList>(Label pc, Method2 method, bool tail, Type constraint, TypeList extraVarargs, UnitDest dest, ArgList args, Data data)
          where TypeList : IIndexable<Type>
          where ArgList : IIndexable<UnitSource>
        {
          return visitor.ConstrainedCallvirt(ConvertLabel(pc), method, tail, constraint, extraVarargs, dest, args, data);
        }

        public Result Castclass(Label pc, Type type, UnitDest dest, UnitSource obj, Data data)
        {
          return visitor.Castclass(ConvertLabel(pc), type, dest, obj, data);
        }

        public Result Cpobj(Label pc, Type type, UnitSource destptr, UnitSource srcptr, Data data)
        {
          return visitor.Cpobj(ConvertLabel(pc), type, destptr, srcptr, data);
        }

        public Result Initobj(Label pc, Type type, UnitSource ptr, Data data)
        {
          return visitor.Initobj(ConvertLabel(pc), type, ptr, data);
        }

        public Result Isinst(Label pc, Type type, UnitDest dest, UnitSource obj, Data data)
        {
          return visitor.Isinst(ConvertLabel(pc), type, dest, obj, data);
        }

        public Result Ldelem(Label pc, Type type, UnitDest dest, UnitSource array, UnitSource index, Data data)
        {
          return visitor.Ldelem(ConvertLabel(pc), type, dest, array, index, data);
        }

        public Result Ldelema(Label pc, Type type, bool @readonly, UnitDest dest, UnitSource array, UnitSource index, Data data)
        {
          return visitor.Ldelema(ConvertLabel(pc), type, @readonly, dest, array, index, data);
        }

        public Result Ldfld(Label pc, Field field, bool @volatile, UnitDest dest, UnitSource obj, Data data)
        {
          return visitor.Ldfld(ConvertLabel(pc), field, @volatile, dest, obj, data);
        }

        public Result Ldflda(Label pc, Field field, UnitDest dest, UnitSource obj, Data data)
        {
          return visitor.Ldflda(ConvertLabel(pc), field, dest, obj, data);
        }

        public Result Ldlen(Label pc, UnitDest dest, UnitSource array, Data data)
        {
          return visitor.Ldlen(ConvertLabel(pc), dest, array, data);
        }

        public Result Ldsfld(Label pc, Field field, bool @volatile, UnitDest dest, Data data)
        {
          return visitor.Ldsfld(ConvertLabel(pc), field, @volatile, dest, data);
        }

        public Result Ldsflda(Label pc, Field field, UnitDest dest, Data data)
        {
          return visitor.Ldsflda(ConvertLabel(pc), field, dest, data);
        }

        public Result Ldtypetoken(Label pc, Type type, UnitDest dest, Data data)
        {
          return visitor.Ldtypetoken(ConvertLabel(pc), type, dest, data);
        }

        public Result Ldfieldtoken(Label pc, Field field, UnitDest dest, Data data)
        {
          return visitor.Ldfieldtoken(ConvertLabel(pc), field, dest, data);
        }

        public Result Ldmethodtoken(Label pc, Method2 method, UnitDest dest, Data data)
        {
          return visitor.Ldmethodtoken(ConvertLabel(pc), method, dest, data);
        }

        public Result Ldvirtftn(Label pc, Method2 method, UnitDest dest, UnitSource obj, Data data)
        {
          return visitor.Ldvirtftn(ConvertLabel(pc), method, dest, obj, data);
        }

        public Result Mkrefany(Label pc, Type type, UnitDest dest, UnitSource obj, Data data)
        {
          return visitor.Mkrefany(ConvertLabel(pc), type, dest, obj, data);
        }

        public Result Newarray(Label pc, Type type, UnitDest dest, UnitSource len, Data data)
        {
          return visitor.Newarray(ConvertLabel(pc), type, dest, len, data);
        }

        public Result Newobj<ArgList>(Label pc, Method2 ctor, UnitDest dest, ArgList args, Data data)
          where ArgList : IIndexable<UnitSource>
        {
          return visitor.Newobj(ConvertLabel(pc), ctor, dest, args, data);
        }

        public Result Refanytype(Label pc, UnitDest dest, UnitSource source, Data data)
        {
          return visitor.Refanytype(ConvertLabel(pc), dest, source, data);
        }

        public Result Refanyval(Label pc, Type type, UnitDest dest, UnitSource source, Data data)
        {
          return visitor.Refanyval(ConvertLabel(pc), type, dest, source, data);
        }

        public Result Rethrow(Label pc, Data data)
        {
          return visitor.Rethrow(ConvertLabel(pc), data);
        }

        public Result Sizeof(Label pc, Type type, UnitDest dest, Data data)
        {
          return visitor.Sizeof(ConvertLabel(pc), type, dest, data);
        }

        public Result Stelem(Label pc, Type type, UnitSource array, UnitSource index, UnitSource value, Data data)
        {
          return visitor.Stelem(ConvertLabel(pc), type, array, index, value, data);
        }

        public Result Stfld(Label pc, Field field, bool @volatile, UnitSource obj, UnitSource value, Data data)
        {
          return visitor.Stfld(ConvertLabel(pc), field, @volatile, obj, value, data);
        }

        public Result Stsfld(Label pc, Field field, bool @volatile, UnitSource value, Data data)
        {
          return visitor.Stsfld(ConvertLabel(pc), field, @volatile, value, data);
        }

        public Result Throw(Label pc, UnitSource exn, Data data)
        {
          return visitor.Throw(ConvertLabel(pc), exn, data);
        }

        public Result Unbox(Label pc, Type type, UnitDest dest, UnitSource obj, Data data)
        {
          return visitor.Unbox(ConvertLabel(pc), type, dest, obj, data);
        }

        public Result Unboxany(Label pc, Type type, UnitDest dest, UnitSource obj, Data data)
        {
          return visitor.Unboxany(ConvertLabel(pc), type, dest, obj, data);
        }


        #region IVisitSynthIL<Label,Method2,Source,Dest,Data,Result> Members


        public Result BeginOld(Label pc, Label matchingEnd, Data data)
        {
          return visitor.BeginOld(ConvertLabel(pc), ConvertMatchingLabel(matchingEnd), data);
        }

        public Result EndOld(Label pc, Label matchingBegin, Type type, UnitDest dest, UnitSource source, Data data)
        {
          return visitor.EndOld(ConvertLabel(pc), ConvertMatchingLabel(matchingBegin), type, dest, source, data);
        }

        public Result Ldresult(Label pc, UnitDest dest, UnitSource source, Data data)
        {
          return visitor.Ldresult(ConvertLabel(pc), dest, source, data);
        }

        #endregion
      }

#endif

      public Result ForwardDecode<Data, Result, Visitor>(APC pc, Visitor visitor, Data data)
        where Visitor : IVisitMSIL<APC, Local, Parameter, Method2, Field, Type, UnitSource, UnitDest, Data, Result>
      {
        return pc.Block.ForwardDecode<Local, Parameter, Method2, Field, Type, Data, Result, RemoveBranchDelegator<Data, Result, Visitor>>(pc, new RemoveBranchDelegator<Data, Result, Visitor>(visitor, this.mdDecoder), data);
      }

      public Transformer<APC, Data, Result> CacheForwardDecoder<Data, Result>(IVisitMSIL<APC, Local, Parameter, Method2, Field, Type, UnitSource, UnitDest, Data, Result> visitor)
      {
        // can't cache further down, as we depend on the pc
        return delegate(APC lab, Data data) { return this.ForwardDecode<Data, Result, IVisitMSIL<APC, Local, Parameter, Method2, Field, Type, UnitSource, UnitDest, Data, Result>>(lab, visitor, data); };
      }


      public IMethodContext<APC,Field,Method2> GetContext
      {
        get { return this; }
      }

      #endregion

      #region IMethodContext<Method2> Members

      public Method2 CurrentMethod
      {
        get { return (Method2)(object)this.parent.method; }
      }

      public APC Entry { get { return parent.Entry; } }

      public APC EntryAfterRequires { get { return parent.EntryAfterRequires; } }

      public APC NormalExit { get { return parent.NormalExit; } }

      public APC ExceptionExit { get { return parent.ExceptionExit; } }

      public APC Post(APC label)
      {
        APC next;
        if (this.parent.HasSingleSuccessor(label, out next)) return next;
        return label;
      }

      public string SourceContext(APC pc) {
        return pc.PrimarySourceContext();
      }

      public string SourceDocument(APC pc)
      {
        return pc.Block.SourceDocument(pc);
      }

      public int SourceStartLine(APC pc)
      {
        return pc.Block.SourceStartLine(pc);
      }

      public int SourceEndLine(APC pc)
      {
        return pc.Block.SourceEndLine(pc);
      }

      public int SourceStartColumn(APC pc)
      {
        return pc.Block.SourceStartColumn(pc);
      }

      public int SourceEndColumn(APC pc)
      {
        return pc.Block.SourceEndColumn(pc);
      }

      public IEnumerable<Field> Modifies(Method2 method)
      {
        return this.methodCache.GetModifies(method);
      }
      #endregion
    }


#if false
    public IDecodeMSIL<APC, Local, Parameter, Method2, Field, Type, Source, Dest> 
    GetDecoder<Local, Parameter, Method2, Field, Type, Source, Dest>(
      IDecodeMSIL<Label, Local, Parameter, Method2, Field, Type, Source, Dest> underlying)
    {
      return new AssumeDecoder<Local, Parameter, Method2, Field, Type, Source, Dest>(this, underlying);
    }

    public ITransformer<APC, Local, Parameter, Method2, Field, Type, Source, Dest>
    GetTransformer<Local, Parameter, Method2, Field, Type, Source, Dest>(
      ITransformer<Label, Local, Parameter, Method2, Field, Type, Source, Dest> underlying) {
      return new AssumeTransformer<Local, Parameter, Method2, Field, Type, Source, Dest>(this, underlying);
    }

    private class AssumeDecoder<Local, Parameter, Method2, Field, Type, Source, Dest> : 
      IDecodeMSIL<APC, Local, Parameter, Method2, Field, Type, Source, Dest>
    {
      ControlFlow<Method, Label, Handler> parent;
      IDecodeMSIL<Label, Local, Parameter, Method2, Field, Type, Source, Dest> underlying;

      public AssumeDecoder(
        ControlFlow<Method, Label, Handler> parent,
        IDecodeMSIL<Label, Local, Parameter, Method2, Field, Type, Source, Dest> underlying
      ) {
        this.parent = parent;
        this.underlying = underlying;
      }


      #region IDecodeMSIL<AbstractPC,Local,Parameter,Method,Field,Type,Source,Dest> Members

      private class NoBranchDelegator<Data, Result> : 
        MSILVisitDelegator<Label, Local, Parameter, Method2, Field, Type, Source, Dest, Pair<Data,IVisitMSIL<APC, Local, Parameter, Method2, Field, Type, Source, Dest, Data, Result>>, Result, APC, Source, Dest, Data> 
      {

        protected override Data Convert(Pair<Data, IVisitMSIL<APC, Local, Parameter, Method2, Field, Type, Source, Dest, Data, Result>> data) {
          return data.One;
        }
        protected override Dest ConvertDest(Label pc, Pair<Data, IVisitMSIL<APC, Local, Parameter, Method2, Field, Type, Source, Dest, Data, Result>> data, Dest dest) {
          return dest;
        }
        protected override IIndexable<Source> Convert(Label pc, Pair<Data, IVisitMSIL<APC, Local, Parameter, Method2, Field, Type, Source, Dest, Data, Result>> data, IIndexable<Source> sources) {
          return sources;
        }
        protected override Source ConvertSource(Label pc, Pair<Data, IVisitMSIL<APC, Local, Parameter, Method2, Field, Type, Source, Dest, Data, Result>> data, Source source) {
          return source;
        }
        protected override IVisitMSIL<APC, Local, Parameter, Method2, Field, Type, Source, Dest, Data, Result> Delegatee(Pair<Data, IVisitMSIL<APC, Local, Parameter, Method2, Field, Type, Source, Dest, Data, Result>> data) {
          return data.Two;
        }
        protected override APC ConvertLabel(Pair<Data, IVisitMSIL<APC, Local, Parameter, Method2, Field, Type, Source, Dest, Data, Result>> data, Label label) {
          throw new Exception("Should not get here");
        }
        protected override IEnumerable<APC> Convert(Pair<Data, IVisitMSIL<APC, Local, Parameter, Method2, Field, Type, Source, Dest, Data, Result>> data, IEnumerable<Label> label) {
          throw new Exception("Should not get here");
        }
        public NoBranchDelegator() {
        }

        public override Result Branch(Label pc, Label target, bool leave, Pair<Data, IVisitMSIL<APC, Local, Parameter, Method2, Field, Type, Source, Dest, Data, Result>> data) {
          // branches are no-ops in the CFG IL
          return Delegatee(data).Nop(ConvertLabel(data, pc), Convert(data));
        }

        public override Result BranchCond(Label pc, Label target, Source cond, Pair<Data, IVisitMSIL<APC, Local, Parameter, Method2, Field, Type, Source, Dest, Data, Result>> data) {
          // branches are no-ops in the CFG IL
          return Delegatee(data).Nop(ConvertLabel(data, pc), Convert(data));
        }

        public override Result Switch(Label pc, IEnumerable<Label> cases, Source value, Pair<Data, IVisitMSIL<APC, Local, Parameter, Method2, Field, Type, Source, Dest, Data, Result>> data) {
          // branches are no-ops in the CFG IL
          return Delegatee(data).Nop(ConvertLabel(data,pc), Convert(data));
        }
      }

      private class AssumeDelegator<Data, Result> :
        MSILVisitDelegator<Label, Local, Parameter, Method2, Field, Type, Source, Dest, Pair<Pair<Tag, Data>, IVisitMSIL<APC, Local, Parameter, Method2, Field, Type, Source, Dest, Data, Result>>, Result, APC, Source, Dest, Data> {

        #region Overrides
        protected override Data Convert(Pair<Pair<string, Data>, IVisitMSIL<APC, Local, Parameter, Method2, Field, Type, Source, Dest, Data, Result>> data) {
          return data.One.Two;
        }

        protected override Source ConvertSource(Label pc, Pair<Pair<string, Data>, IVisitMSIL<APC, Local, Parameter, Method2, Field, Type, Source, Dest, Data, Result>> data, Source source) {
          return source;
        }

        protected override Dest ConvertDest(Label pc, Pair<Pair<string, Data>, IVisitMSIL<APC, Local, Parameter, Method2, Field, Type, Source, Dest, Data, Result>> data, Dest dest) {
          return dest;
        }

        protected override IIndexable<Source> Convert(Label pc, Pair<Pair<string, Data>, IVisitMSIL<APC, Local, Parameter, Method2, Field, Type, Source, Dest, Data, Result>> data, IIndexable<Source> sources) {
          return sources;
        }

        protected override IVisitMSIL<APC, Local, Parameter, Method2, Field, Type, Source, Dest, Data, Result> Delegatee(Pair<Pair<string, Data>, IVisitMSIL<APC, Local, Parameter, Method2, Field, Type, Source, Dest, Data, Result>> data) {
          return data.Two;
        }

        protected override APC ConvertLabel(Pair<Pair<string, Data>, IVisitMSIL<APC, Local, Parameter, Method2, Field, Type, Source, Dest, Data, Result>> data, Label label) {
          throw new NotImplementedException("Should not be called");
        }

        protected override IEnumerable<APC> Convert(Pair<Pair<string, Data>, IVisitMSIL<APC, Local, Parameter, Method2, Field, Type, Source, Dest, Data, Result>> data, IEnumerable<Label> label) {
          throw new NotImplementedException("Should not be called");
        }

        public override Result Branch(Label pc, Label target, bool leave, Pair<Pair<string, Data>, IVisitMSIL<APC, Local, Parameter, Method2, Field, Type, Source, Dest, Data, Result>> data) {
          // unconditional branch has no assume, thus is a nop
          return Delegatee(data).Nop(ConvertLabel(data,pc), Convert(data));
        }

        public override Result BranchCond(Label pc, Label target, Source cond, Pair<Pair<string, Data>, IVisitMSIL<APC, Local, Parameter, Method2, Field, Type, Source, Dest, Data, Result>> data) {
          return Delegatee(data).Assume(ConvertLabel(data, pc), data.One.One, cond, Convert(data));
        }

        public override Result Switch(Label pc, IEnumerable<Label> cases, Source value, Pair<Pair<string, Data>, IVisitMSIL<APC, Local, Parameter, Method2, Field, Type, Source, Dest, Data, Result>> data) {
          return Delegatee(data).Assume(ConvertLabel(data,pc), data.One.One, value, Convert(data));
        }

        #endregion
      }
        
      public ILDecoder<APC, Data, Result, IVisitMSIL<APC, Local, Parameter, Method2, Field, Type, Source, Dest, Data, Result>> GetDecoder<Data, Result>() {
        ILDecoder<Label, Pair<Data, IVisitMSIL<APC, Local, Parameter, Method2, Field, Type, Source, Dest, Data, Result>>, Result, IVisitMSIL<Label, Local, Parameter, Method2, Field, Type, Source, Dest,Pair<Data,IVisitMSIL<APC, Local, Parameter, Method2, Field, Type, Source, Dest, Data, Result>>, Result>> decoder1 = underlying.GetDecoder<Pair<Data,IVisitMSIL<APC, Local, Parameter, Method2, Field, Type, Source,Dest, Data, Result>>, Result>();
        ILDecoder<Label, Pair<Pair<Tag,Data>, IVisitMSIL<APC, Local, Parameter, Method2, Field, Type, Source, Dest, Data, Result>>, Result, IVisitMSIL<Label, Local, Parameter, Method2, Field, Type, Source, Dest,Pair<Pair<Tag,Data>,IVisitMSIL<APC, Local, Parameter, Method2, Field, Type, Source, Dest, Data, Result>>, Result>> decoder2 = underlying.GetDecoder<Pair<Pair<Tag,Data>,IVisitMSIL<APC, Local, Parameter, Method2, Field, Type, Source,Dest, Data, Result>>, Result>();

        NoBranchDelegator<Data, Result> delegator = new NoBranchDelegator<Data, Result>();
        AssumeDelegator<Data, Result> assumeDelegator = new AssumeDelegator<Data, Result>();

        return delegate(APC pc, Data data, IVisitMSIL<APC, Local, Parameter, Method2, Field, Type, Source, Dest, Data, Result> visitor) 
        {
          Label/*?*/ lab;
          if (parent.UnderlyingLabel(pc, out lab)) {
            return decoder1(lab, new Pair<Data, IVisitMSIL<APC, Local, Parameter, Method2, Field, Type, Source, Dest, Data, Result>>(data, visitor), delegator);
          }
          AssumeBlock/*?*/ ab = pc.Block as AssumeBlock;
          if (ab != null) {
            return decoder2(ab.Label, new Pair<Pair<string, Data>, IVisitMSIL<APC, Local, Parameter, Method2, Field, Type, Source, Dest, Data, Result>>(new Pair<Tag, Data>(ab.Tag, data), visitor), assumeDelegator);
          }
          return visitor.Nop(pc, data);
        };
      }
      
      #endregion
    }

    class AssumeTransformer<Local, Parameter, Method2, Field, Type, Source, Dest> : ITransformer<APC, Local, Parameter, Method2, Field, Type, Source, Dest> 
    {
      ControlFlow<Method, Label, Handler> parent;
      ITransformer<Label, Local, Parameter, Method2, Field, Type, Source, Dest> underlying;

      public AssumeTransformer(
        ControlFlow<Method, Label, Handler> parent,
        ITransformer<Label, Local, Parameter, Method2, Field, Type, Source, Dest> underlying
      ) {
        this.parent = parent;
        this.underlying = underlying;
      }


      #region ITransformer<APC,Local,Parameter,Method2,Field,Type,Source,Dest> Members

      private class NoBranchDelegator<Data, Result> :
        MSILVisitDelegator<Label, Local, Parameter, Method2, Field, Type, Source, Dest, Pair<APC,Data>, Result, APC, Source, Dest, Data>
      {
        // We need to funnel the original apc through, although the underlying decoder will call us with a label only.
        // Thus, we pass the original APC in the data part and take it out in the label converter.
        // We just need to check that the underlying label actually agrees.

        IVisitMSIL<APC, Local, Parameter, Method2, Field, Type, Source, Dest, Data, Result> delegatee;

        public NoBranchDelegator(IVisitMSIL<APC, Local, Parameter, Method2, Field, Type, Source, Dest, Data, Result> delegatee)
        {
          this.delegatee = delegatee;
        }

        protected override Data Convert(Pair<APC,Data> data)
        {
          return data.Two;
        }
        protected override Dest ConvertDest(Label pc, Pair<APC,Data> data, Dest dest)
        {
          return dest;
        }
        protected override IIndexable<Source> Convert(Label pc, Pair<APC,Data> data, IIndexable<Source> sources)
        {
          return sources;
        }
        protected override Source ConvertSource(Label pc, Pair<APC,Data> data, Source source)
        {
          return source;
        }
        protected override IVisitMSIL<APC, Local, Parameter, Method2, Field, Type, Source, Dest, Data, Result> Delegatee(Pair<APC,Data> data)
        {
          return this.delegatee;
        }
        protected override APC ConvertLabel(Pair<APC,Data> data, Label label)
        {
          // check that the underlying label in the data part is indeed the current label
          Label/*?*/ underlying;
          if (data.One.UnderlyingLabel(out underlying)) {
            if (underlying.Equals(label)) {
              return data.One;
            }
          }
          throw new Exception("Should not get here");
        }
        protected override IEnumerable<APC> Convert(Pair<APC,Data> data, IEnumerable<Label> label)
        {
          throw new Exception("Should not get here");
        }


        public override Result Branch(Label pc, Label target, bool leave, Pair<APC,Data> data)
        {
          // branches are no-ops in the CFG IL
          return Delegatee(data).Nop(ConvertLabel(data, pc), Convert(data));
        }

        public override Result BranchCond(Label pc, Label target, Source cond, Pair<APC,Data> data)
        {
          // branches are no-ops in the CFG IL
          return Delegatee(data).Nop(ConvertLabel(data, pc), Convert(data));
        }

        public override Result Switch(Label pc, IEnumerable<Label> cases, Source value, Pair<APC,Data> data)
        {
          // branches are no-ops in the CFG IL
          return Delegatee(data).Nop(ConvertLabel(data, pc), Convert(data));
        }
      }

      /// <summary>
      /// We need to funnel the original APC through with the data as well as the assume label.
      /// </summary>
      private class AssumeDelegator<Data, Result> :
        MSILVisitDelegator<Label, Local, Parameter, Method2, Field, Type, Source, Dest, Pair<Pair<APC,Tag>, Data>, Result, APC, Source, Dest, Data>
      {
        IVisitMSIL<APC, Local, Parameter, Method2, Field, Type, Source, Dest, Data, Result> delegatee;

        public AssumeDelegator(IVisitMSIL<APC, Local, Parameter, Method2, Field, Type, Source, Dest, Data, Result> delegatee) {
          this.delegatee = delegatee;
        }

        #region Overrides
        protected override Data Convert(Pair<Pair<APC,Tag>, Data> data)
        {
          return data.Two;
        }

        protected override Source ConvertSource(Label pc, Pair<Pair<APC,Tag>, Data> data, Source source)
        {
          return source;
        }

        protected override Dest ConvertDest(Label pc, Pair<Pair<APC,Tag>, Data> data, Dest dest)
        {
          return dest;
        }

        protected override IIndexable<Source> Convert(Label pc, Pair<Pair<APC,Tag>, Data> data, IIndexable<Source> sources)
        {
          return sources;
        }

        protected override IVisitMSIL<APC, Local, Parameter, Method2, Field, Type, Source, Dest, Data, Result> Delegatee(Pair<Pair<APC,Tag>, Data> data)
        {
          return this.delegatee;
        }

        protected override APC ConvertLabel(Pair<Pair<APC, Tag>, Data> data, Label label) 
        {
          // check that the underlying label in the data part is indeed the current label
          Label/*?*/ underlying;
          if (data.One.One.UnderlyingLabel(out underlying)) {
            if (underlying.Equals(label)) {
              return data.One.One;
            }
          }
          throw new NotImplementedException("Should not be called");
        }

        protected override IEnumerable<APC> Convert(Pair<Pair<APC,Tag>, Data> data, IEnumerable<Label> label)
        {
          throw new NotImplementedException("Should not be called");
        }

        public override Result Branch(Label pc, Label target, bool leave, Pair<Pair<APC,Tag>, Data> data)
        {
          // unconditional branch has no assume, thus is a nop

          // NOTE: pick the apc out of the data part, as we are decoding the branch that gave rise to the Assume,
          // but the assume is at a different apc.
          return Delegatee(data).Nop(data.One.One, Convert(data));
        }

        public override Result BranchCond(Label pc, Label target, Source cond, Pair<Pair<APC,Tag>, Data> data)
        {
          // NOTE: pick the apc out of the data part, as we are decoding the branch that gave rise to the Assume,
          // but the assume is at a different apc.
          return Delegatee(data).Assume(data.One.One, data.One.Two, cond, Convert(data));
        }

        public override Result Switch(Label pc, IEnumerable<Label> cases, Source value, Pair<Pair<APC,Tag>, Data> data)
        {
          // NOTE: pick the apc out of the data part, as we are decoding the branch that gave rise to the Assume,
          // but the assume is at a different apc.
          return Delegatee(data).Assume(data.One.One, data.One.Two, value, Convert(data));
        }

        #endregion
      }

      public Transformer<APC, Data, Result> GetTransformer<Data, Result>(IVisitMSIL<APC, Local, Parameter, Method2, Field, Type, Source, Dest, Data, Result> visitor) {
#if SPECSHARP
        NoBranchDelegator<Data, Result> noBranchDelegator = null; // new NoBranchDelegator<Data, Result>(visitor);
        AssumeDelegator<Data, Result> assumeDelegator = null; // new AssumeDelegator<Data, Result>(visitor);

        Transformer<Label, Pair<APC, Data>, Result> decoder1 = null; // underlying.GetTransformer(noBranchDelegator);
        Transformer<Label, Pair<Pair<APC, Tag>, Data>, Result> decoder2 = null; // underlying.GetTransformer(assumeDelegator);
#else
        NoBranchDelegator<Data, Result> noBranchDelegator = new NoBranchDelegator<Data, Result>(visitor);
        AssumeDelegator<Data, Result> assumeDelegator = new AssumeDelegator<Data, Result>(visitor);

        Transformer<Label, Pair<APC, Data>, Result> decoder1 = underlying.GetTransformer(noBranchDelegator);
        Transformer<Label, Pair<Pair<APC, Tag>, Data>, Result> decoder2 = underlying.GetTransformer(assumeDelegator);
#endif

        return delegate(APC pc, Data data)
        {
          Label/*?*/ lab;
          if (parent.UnderlyingLabel(pc, out lab)) {
            return decoder1(lab, new Pair<APC,Data>(pc, data));
          }
          AssumeBlock/*?*/ ab = pc.Block as AssumeBlock;
          if (ab != null) {
            return decoder2(ab.Label, new Pair<Pair<APC,Tag>, Data>(new Pair<APC,Tag>(pc,ab.Tag), data));
          }
          return visitor.Nop(pc, data);
        };
      }
      
      #endregion
    }
#endif

    #endregion

    #region ICFG<Type,APC> Members


    public string ToString(APC pc)
    {
      return pc.ToString();
    }

    #endregion
  }

  /// <summary>
  /// Represents edges as just an array ordered by the index of the cfg block where the edge starts.
  /// </summary>
  internal class EdgeMap<Tag> : IEnumerable<Pair<CFGBlock,Pair<Tag, CFGBlock>>>, IGraph<CFGBlock, Tag>
  {
    List<Pair<CFGBlock, Pair<Tag,CFGBlock>>> edges;

    public EdgeMap(List<Pair<CFGBlock, Pair<Tag, CFGBlock>>> edges) {
      this.edges = edges;
      edges.Sort(CompareFirstBlockIndex);
      CheckSort();
    }
    private void CheckSort() {
      int lastSeen = 0;
      for (int i = 0; i < edges.Count; i++) {
        int currentIndex = edges[i].One.Index;
        Debug.Assert(currentIndex >= lastSeen);
        lastSeen = currentIndex;
      }
    }
    private static int CompareFirstBlockIndex(Pair<CFGBlock, Pair<Tag, CFGBlock>> e1, Pair<CFGBlock, Pair<Tag, CFGBlock>> e2) {
      if (e1.One.Index == e2.One.Index) {
        return e1.Two.Two.Index - e2.Two.Two.Index;
      }
      return e1.One.Index - e2.One.Index;
    }

    private struct Successors : ICollection<Pair<Tag,CFGBlock>>
    {
      EdgeMap<Tag> underlying;
      int startIndex;

      public Successors(EdgeMap<Tag> underlying, int startIndex) {
        this.underlying = underlying;
        this.startIndex = startIndex;
      }

      #region ICollection<Pair<Tag,CFGBlock>> Members

      public void Add(Pair<Tag,CFGBlock> item) {
        throw new Exception("The method or operation is not implemented.");
      }

      public void Clear() {
        throw new Exception("The method or operation is not implemented.");
      }

      //^ [Confined]
      public bool Contains(Pair<Tag,CFGBlock> item) {
        throw new Exception("The method or operation is not implemented.");
      }

      public void CopyTo(Pair<Tag,CFGBlock>[] array, int arrayIndex) {
        throw new Exception("The method or operation is not implemented.");
      }

      public int Count {
        get {
          int edgeIndex = startIndex;
          if (edgeIndex < underlying.edges.Count) {
            // count equal first
            int blockIndex = underlying.edges[edgeIndex].One.Index;
            int count = 0;
            do {
              count++;
              edgeIndex++;
            } while (edgeIndex < underlying.edges.Count && underlying.edges[edgeIndex].One.Index == blockIndex);
            return count;
          }
          return 0;
        }
      }

      public bool IsReadOnly {
        get { return true; }
      }

      public bool Remove(Pair<Tag,CFGBlock> item) {
        throw new Exception("The method or operation is not implemented.");
      }

      #endregion

      #region IEnumerable<Pair<Tag,CFGBlock>> Members

      //^ [Pure]
      public IEnumerator<Pair<Tag,CFGBlock>> GetEnumerator() {
        if (startIndex >= underlying.edges.Count) { yield break; }
        int edgeIndex = startIndex;
        int blockIndex = underlying.edges[edgeIndex].One.Index;
        do {
          yield return underlying.edges[edgeIndex].Two;
          edgeIndex++;
        }
        while (edgeIndex < underlying.edges.Count && underlying.edges[edgeIndex].One.Index == blockIndex);
      }

      #endregion

      #region IEnumerable Members

      //^ [Pure]
      System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
        throw new Exception("The method or operation is not implemented.");
      }

      #endregion
    }

    public ICollection<Pair<Tag,CFGBlock>> this[CFGBlock from] {
      get {
        return new Successors(this, FindStartIndex(from));
      }
    }

    private int FindStartIndex(CFGBlock from) {
      // binary search
      int lo = 0;
      int hi = edges.Count;
      while (lo < hi) {
        int mid = lo + (hi - lo) / 2;

        int indexAtMid = edges[mid].One.Index;
        if (indexAtMid == from.Index) {
          // now find beginning of sequence
          while (mid > 0 && edges[mid - 1].One.Index == indexAtMid) {
            mid--;
          }
          return mid;
        }
        if (indexAtMid < from.Index) {
          lo = mid + 1;
          continue;
        }
        Debug.Assert(indexAtMid > from.Index);
        hi = mid;
      }
      // if we fall out, we didn't find any. Return index past last
      return edges.Count;
    }


    public EdgeMap<Tag> ReversedEdges() {
      List<Pair<CFGBlock, Pair<Tag,CFGBlock>>> reversed = new List<Pair<CFGBlock, Pair<Tag, CFGBlock>>>(edges.Count);

      foreach (Pair<CFGBlock, Pair<Tag, CFGBlock>> edge in edges) {
        reversed.Add(new Pair<CFGBlock, Pair<Tag,CFGBlock>>(edge.Two.Two, new Pair<Tag,CFGBlock>(edge.Two.One, edge.One)));
      }
      return new EdgeMap<Tag>(reversed);
    }

    public void ReSort() {
      edges.Sort(CompareFirstBlockIndex);
    }

    /// <summary>
    /// Removes the unwanted edges
    /// </summary>
    public void Filter(Predicate<Pair<CFGBlock, Pair<Tag, CFGBlock>>> keep) {
      // must separately build a list of indices to remove, as we can't change the list while traversing it or we'll skip
      // elements
      FList<int> toRemove = null;
      for (int i = 0; i < edges.Count; i++) {
        if (!keep(edges[i])) {
          toRemove = FList<int>.Cons(i, toRemove);
        }
      }
      toRemove.Apply(delegate(int i) { edges.RemoveAt(i); });
    }

    #region IEnumerable<Pair<CFGBlock,Pair<Tag>,CFGBlock>> Members

    //^ [Pure]
    public IEnumerator<Pair<CFGBlock, Pair<Tag, CFGBlock>>> GetEnumerator() {
      return edges.GetEnumerator();
    }

    #endregion

    #region IEnumerable Members

    //^ [Pure]
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
      throw new Exception("The method or operation is not implemented.");
    }

    #endregion

    #region IGraph<CFGBlock,Tag> Members

    IEnumerable<CFGBlock> IGraph<CFGBlock, Tag>.Nodes
    {
      get { throw new NotImplementedException(); }
    }

    IEnumerable<Pair<Tag, CFGBlock>> IGraph<CFGBlock, Tag>.Successors(CFGBlock node)
    {
      return this[node];
    }

    #endregion
  }
}