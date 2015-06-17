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
using System.Diagnostics;
using System.Text;
using System.IO;

using Microsoft.Research.DataStructures;

namespace Microsoft.Research.CodeAnalysis
{
  using SubroutineContext = FList<Microsoft.Research.DataStructures.STuple<CFGBlock, CFGBlock, string>>;
  using System.Diagnostics.Contracts;
  using System.Threading;

  public class DFAOptions
  {
    public bool Trace { get; set; }
    public bool TraceTimePerInstruction { get; set; }
    public bool TraceMemoryPerInstruction { get; set; }

    /// <summary>
    /// How many exact joins we do before applying the widening operator?
    /// </summary>
    public int IterationsBeforeWidening { get; set; }
    public bool EnforceFairJoin { get; set; }
    /// <summary>
    /// Timeout expressed in minutes for the fixpoint computation
    /// </summary>
    public int Timeout { get; set; }

    public DFAOptions()
    {
      Timeout = Int32.MaxValue;
    }

    [ContractInvariantMethod]
    void ObjectInvariant()
    {
      Contract.Invariant(Timeout >= 0);
    }

  }

  [ContractVerification(true)]
  public abstract class DFARoot
  {

    #region Constants
    
    public const int DefaultTimeOut = 180;

    #endregion

    #region TimeOut
    static public TimeOutChecker StartTimeOut(int time, CancellationToken cancellationToken)
    {
      Contract.Requires(time > 0);

      Contract.Ensures(Contract.Result<TimeOutChecker>() != null);

      timeout = new TimeOutChecker(time, cancellationToken);

      return timeout;
    }

    [ThreadStatic]
    static private TimeOutChecker timeout;

    static public TimeOutChecker TimeOut
    {
      get
      {
        Contract.Ensures(Contract.Result<TimeOutChecker>() != null);

        // just to make the code more robust
        if (timeout == null)
        {
          timeout = new TimeOutChecker(DefaultTimeOut, new CancellationToken());
        }

        return timeout;
      }
    }
    #endregion

    #region Object invariant

    [ContractInvariantMethod]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Required for code contracts.")]
    private void ObjectInvariant()
    {
      Contract.Invariant(this.timeCounter != null);
    }


    #endregion

    #region Time Counter

    protected readonly TimeCounter timeCounter;

    #endregion

    #region Constructor

    protected DFARoot()
    {
      this.timeCounter = new TimeCounter();
    }

    #endregion

  }

  #region DFARoot<AState, Type> contract binding
  [ContractClass(typeof(DFARootContract<, >))]
  public abstract partial class DFARoot<AState, Type>
  {

  }

  [ContractClassFor(typeof(DFARoot<,>))]
  abstract class DFARootContract <AState, Type> : DFARoot<AState, Type>
  {
    protected DFARootContract(ICFG cfg) : base(cfg) {}

    protected override int Comparer(APC o1, APC o2)
    {
      throw new NotImplementedException();
    }

    protected override bool RequiresJoining(APC apc)
    {
      throw new NotImplementedException();
    }

    protected override bool IsBackEdge(APC from, APC to)
    {
      throw new NotImplementedException();
    }

    protected override bool HasSingleSuccessor(APC current, out APC next)
    {
      throw new NotImplementedException();
    }

    protected override IEnumerable<APC> Successors(APC current)
    {
      throw new NotImplementedException();
    }

    protected override AState ImmutableVersion(AState state, APC at)
    {
      throw new NotImplementedException();
    }

    protected override AState MutableVersion(AState state, APC at)
    {
      throw new NotImplementedException();
    }

    protected override bool Join(Pair<APC, APC> edge, AState newState, AState existingState, out AState joinedState, bool widen)
    {
      throw new NotImplementedException();
    }

    protected override AState Transfer(APC pc, AState state)
    {
      Contract.Ensures(Contract.Result<AState>() != null);

      throw new NotImplementedException();
    }

    protected override bool IsBottom(APC pc, AState state)
    {
      throw new NotImplementedException();
    }

    protected override bool IsTop(APC pc, AState state)
    {
      throw new NotImplementedException();
    }
  }
  #endregion

  [ContractVerification(true)]
  public abstract partial class DFARoot<AState, Type> : DFARoot, IEqualityComparer<APC>
  {
    #region Object invariant

    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(this.pending != null);
      Contract.Invariant(this.joinState != null);
    }

    #endregion

    private DFAOptions options;
    public DFAOptions Options
    {
      get
      {
        if (options == null)
        {
          this.options = new DFAOptions();
        }
        return this.options;
      }
      set
      {
        this.options = value;
      }
    }
    private IWidenStrategy widenStrategy;

    /// <summary>
    /// Predicate indicating to fixpoint computation where to cache intermediate state.
    /// </summary>
    private Predicate<APC> cachePolicy;

    public Predicate<APC> CachePolicy {
      set
      {
        this.cachePolicy = value;
      }
    }

    protected bool CacheThisPC(APC pc)
    {
      if (cachePolicy != null) return cachePolicy(pc);
      return false;
    }

    protected ICFG cfg;

    /// <summary>
    /// Maintains the summmary abstract state at a join point, which is the join/widening of all incoming edges
    /// NOTE: states may also be cached at APCs other than join points.
    /// </summary>
    readonly private Dictionary<APC, AState> joinState;

    public IEnumerable<KeyValuePair<APC, AState>> JoinStates() {
      return joinState;
    }

    public bool TryJoinState(APC apc, out AState value)
    {
      return this.joinState.TryGetValue(apc, out value);
    }

    /// <summary>
    /// An abstract pc is in this set if it needs to be (re)analyzed.
    /// </summary>
    readonly protected PriorityQueue<APC> pending;

    protected DFARoot(ICFG cfg) {
      this.cfg = cfg;
      this.pending = new PriorityQueue<APC>(this.Comparer);

      this.widenStrategy = null; // Will be set later
      this.joinState = new Dictionary<APC, AState>(this);
    }

    /// <summary>
    /// State state must be immutable.
    /// </summary>
    protected void Seed(APC startPC, AState startState) {
      joinState.Add(startPC, startState);
      pending.Add(startPC);
    }

    /// <summary>
    /// Called whenever we have a new state at the beginning of a block
    /// This method decides whether
    /// a) it is a join point and requires a join
    /// b) whether to just cache the state
    /// </summary>
    public virtual void PushState(APC current, APC next, AState state)
    {
      // since we store this away, we need to get an immutable version
      state = ImmutableVersion(state, next);

      if (RequiresJoining(next)) {
        Pair<APC, APC> edge = new Pair<APC, APC>(current, next);
        if (JoinStateAtBlock(edge, state)) {
          this.pending.Add(next);
        }
      }
      else {
        // don't join if no join is required, just store pending state and add to pending list
        this.joinState[next] = state;
        this.pending.Add(next);
      }
    }

    /// <summary>
    /// Called by the TryGetAStateForPostcondition to rename a state
    /// </summary>
    protected virtual bool TryRename(APC lastPC, APC aPC, AState currState, out AState renamed)
    {
      Contract.Ensures(Contract.ValueAtReturn(out renamed) != null || !Contract.Result<bool>());

      renamed = currState;
      return false;
    }


    #region Edge state not needed for now
#if false
    bool PushStateOntoEdge(Pair<APC, APC> edge, AState state)
    {
      AState existing;
      if (this.edgeState.TryGetValue(edge, out existing)) {
        AState joined;
        bool changed = Join(edge, state, existing, out joined, false);
        if (changed) {
          this.edgeState[edge] = joined;
        }
        return changed;
      }
      else {
        this.edgeState.Add(edge, state);
        return true;
      }
    }
#endif
    #endregion

    protected virtual bool JoinStateAtBlock(Pair<APC, APC> edge, AState state) 
    {
      AState/*?*/ existing;

      if (this.joinState.TryGetValue(edge.Two, out existing)) 
      {
        AState joined;

        Contract.Assume(this.widenStrategy != null, "At this point, the widening strategy has already been set");
        bool widen = this.widenStrategy.WantToWiden(edge.One, edge.Two, this.IsBackEdge(edge.One, edge.Two));

        bool changed = Join(edge, state, existing, out joined, widen);
        
        if (changed)
        {
          this.joinState[edge.Two] = this.ImmutableVersion(joined, edge.Two);
        }
        return changed;
      }
      else {
        this.joinState.Add(edge.Two, state);
        return true;
      }
    }

    /// <summary>
    /// Print the states at join points
    /// </summary>
    public void PrintStatesAtJoinPoints(TextWriter output)
    {
      // It's just logging
      if(output == null)
      {
        return;
      }

      foreach (APC apc in this.joinState.Keys)
      {
        string stateAsStr = this.joinState[apc].ToString().Replace(Environment.NewLine, Environment.NewLine + "  ");

        output.WriteLine("Block {0}, PC {1}: {2}", apc.Block.ToString(), apc.Index.ToString(), stateAsStr);
      }
    }

    private AState Cache(APC pc, AState state)
    {
      state = ImmutableVersion(state, pc);
      if (this.Options.Trace)
      {
        Console.WriteLine("Caching at {0}", pc);
        this.Dump(state);
      }
      this.joinState[pc] = state;
      return MutableVersion(state, pc);
    }


    protected virtual void ComputeFixpoint()
    {
      var timeout = TimeOut;

      if (this.Options.EnforceFairJoin)
      {
        this.widenStrategy = new EdgeBasedWidening(this.Options.IterationsBeforeWidening);
      }
      else
      {
        this.widenStrategy = new BlockBasedWidening(this.Options.IterationsBeforeWidening);
      }

      while (pending.Count > 0)
      {
        var next = pending.Pull();
        var state = MutableVersion(joinState[next], next);
        var alreadyCached = true;
        APC current;

        if (Options.Trace)
        {
          Console.WriteLine("State before {0}", next);
          Dump(state);
        }

        if (this.Options.TraceTimePerInstruction)
        {
          this.timeCounter.Start();
        }

        do
        {
          current = next;

          if (IsBottom(current, state))
          {
            goto nextPending;
          }

          if (!alreadyCached && this.CacheThisPC(current))
          {
            state = this.Cache(current, state);
          }

          state = Transfer(current, state);

          if (Options.Trace)
          {
            Console.WriteLine("State after {0}", current);
            Dump(state);
          }

          if (this.Options.TraceTimePerInstruction)
          {
            this.timeCounter.Stop();
            Console.WriteLine("Elapsed time for {0} : {1}", current, timeCounter);
          }

          this.TraceMemoryUsageIfEnabled("after instruction", current);

          // The transfer function of some abstract domains can take *really* a lot of time, this is the reason why we check the timeout here
          timeout.CheckTimeOut("fixpoint computation");

          alreadyCached = false;
        } while (this.HasSingleSuccessor(current, out next) && !RequiresJoining(next));


        foreach (APC succ in this.Successors(current).AssumeNotNull())
        {
          if (IsBottom(succ, state)) continue;

          PushState(current, succ, state);
        }
        timeout.CheckTimeOut("fixpoint computation");

      nextPending:
        ;
      }

      if (Options.Trace)
      {
        Console.WriteLine("DFA done");
      }
    }

    #region Directional abstractions

    protected abstract int Comparer(APC o1, APC o2);

    protected abstract bool RequiresJoining(APC apc);

    protected abstract bool IsBackEdge(APC from, APC to);

    protected abstract bool HasSingleSuccessor(APC current, out APC next);

    protected abstract IEnumerable<APC> Successors(APC current);

    #endregion

    #region Client abstractions

    /// <summary>
    /// Called by framework prior to storing a state as a temporary fixpoint.
    /// This is useful for a client to know in that it can no longer modify that 
    /// state, or the analysis is meaningless.
    /// </summary>
    protected abstract AState ImmutableVersion(AState state, APC at);

    /// <summary>
    /// Called by the framework whenever it picks up interpretation from a temporary
    /// fixpoint. The result of this call is passed to the transfer function, which
    /// is free to modify it in place (if it is mutable at all).
    /// </summary>
    protected abstract AState MutableVersion(AState state, APC at);

    protected abstract bool Join(Pair<APC, APC> edge, AState newState, AState existingState, out AState joinedState, bool widen);

    /// <summary>
    /// Apply instruction at the given pc to the abstract state.
    /// Should be side effect free as transfer is called during recomputation of intermediate states.
    /// </summary>
    /// <param name="pc"></param>
    /// <param name="state"></param>
    /// <returns></returns>
    protected abstract AState Transfer(APC pc, AState state);

    /// <summary>
    /// Must be asked in the pre-state
    /// </summary>
    protected abstract bool IsBottom(APC pc, AState state);

    /// <summary>
    /// Must be asked in the pre-state
    /// </summary>
    protected abstract bool IsTop(APC pc, AState state);

    protected virtual void Dump(AState state) { }

    #endregion

    #region IEqualityComparer<APC> Members

    bool IEqualityComparer<APC>.Equals(APC x, APC y)
    {
      return x.Equals(y);
    }

    int IEqualityComparer<APC>.GetHashCode(APC obj)
    {
      return obj.GetHashCode();
    }

    #endregion

    #region Printing helpers

    public void TraceMemoryUsageIfEnabled(string where, APC? apc)
    {
#if DEBUG && false
      var currmem = System.GC.GetTotalMemory(false) / (1024 * 1024);
      if (currmem > 4000)
      {
        //System.Diagnostics.Debugger.Break();
        Console.WriteLine("Used more than 4GBytes of mem");
      }
#endif
      if (this.Options.TraceMemoryPerInstruction)
      {
        var mem = System.GC.GetTotalMemory(false) / (1024 * 1024);
        Console.WriteLine("mem: {0}Mbytes @ {1} {2}", mem, where, apc.HasValue? apc.Value.ToString() : "");
      }
    }
    
    #endregion
  }

  
  public abstract class ForwardDFA<AState, Type> : DFARoot<AState, Type>
  {
    /// <summary>
    /// Assumes that exception edges always target join points, so we never need to reach
    /// backwards across a throw edges.
    /// </summary>
    public bool GetPreState(APC apc, out AState/*?*/ ifFound)
    {      
      bool noInfo;
      ifFound = GetPreState(apc, default(AState), out noInfo);
      return !noInfo;
    }


    public AState/*?*/ GetPreStateWithDefault(APC apc, AState/*?*/ ifMissing)
    {
      bool noInfo;
      AState ifFound = GetPreState(apc, default(AState), out noInfo);
      if (noInfo) return ifMissing;
      return ifFound;
    }

    OnDemandCache<APC, AState> preStateCache = new OnDemandCache<APC, AState> { Capacity = 1000 };

    virtual protected bool TryPreStateCache(APC apc, out AState value)
    {
      if (this.TryJoinState(apc, out value)) return true;
      return preStateCache.TryGetValue(apc, out value);
    }

    virtual protected void CachePreState(APC apc, AState value)
    {
      this.preStateCache.Add(apc, value);
    }

    /// <summary>
    /// Assumes that exception edges always target join points, so we never need to reach
    /// backwards across a throw edges.
    /// </summary>
    /// <param name="apc"></param>
    /// <param name="ifMissing"></param>
    /// <returns></returns>
    public AState/*?*/ GetPreState(APC apc, AState/*?*/ ifMissing, out bool noInfo) {
      // Find closest preceeding cached point
      // Keep a list of program points we visit backward so we can take the
      // same path forward.
      FList<APC>/*?*/ path = null;
      APC current = apc;
      APC pred;
      AState/*?*/ state;
      bool found;
      while (!(found = this.TryPreStateCache(current, out state)) && !RequiresJoining(current) && !this.CacheThisPC(current) && this.cfg.HasSinglePredecessor(current, out pred))
      {
        current = pred;
        path = path.Cons(current);
      }
      if (!found) {
        noInfo = true;
        return ifMissing; // unreachable
      }
      bool nonTrivialPath = path != null;
      // Now forward simulate and cache
      while (path != null) {
        if (IsBottom(path.Head, state))
        {
          noInfo = true; // equate no info with bottom since we cannot manufacture bottom here
          return state;
        }
        // must get a mutable version if we are going to change it.
        state = MutableVersion(state, path.Head);
        state = Transfer(path.Head, state);
        if (IsBottom(path.Head, state))
        {
          noInfo = false;
          return state;
        }
        path = path.Tail;
        if (path != null)
        {
          // cache on next
          this.CachePreState(path.Head, ImmutableVersion(state, path.Head));
          // this.joinState.Add(path.Head, state);
        }
      }
      // cache on lookedup up apc
      if (nonTrivialPath) {
        this.CachePreState(apc, ImmutableVersion(state, apc));
        // this.joinState.Add(apc, state);
      }
      noInfo = false;
      return state;
    }

#if false
    public bool TryAStateForPostCondition(AState initialState, out AState exitState)
    {
      APC currPC = default(APC);  // just to please the compiler
      int paths = 0;

      if (this.Options.Trace)
      {
        Console.WriteLine("Looking for a state for the postcondition filtering");
      }

      var currState = initialState;

      foreach (var prevBlock in this.cfg.NormalExit.Block.Subroutine.PredecessorBlocks(this.cfg.NormalExit.Block))
      {
        if(this.Options.Trace)
        {
          Console.WriteLine("Starting analysis of predecessor #{0} (Block {1})", paths, prevBlock.ToString());        
        }

        APC prevPC, lastPC; 
        prevPC = lastPC = currPC = prevBlock.Last;
        currState = initialState;

        // F: this is a rough strategy. A subroutine may contain loops. In this case we just gave up 
        // (I do not want to compute a fixpoint here to avoid slowing down the inference of posts)
        while (currPC.Block != this.cfg.NormalExit.Block && this.cfg.HasSingleSuccessor(currPC, out currPC))
        {
          if (currPC.Block.First.Equals(currPC))
          {
            int preds = 0;
            foreach (var p in this.cfg.Predecessors(currPC))
            {
              preds++;
            }

            if (preds > 1 && currPC.Block != this.cfg.NormalExit.Block)
            {
              exitState = initialState;
              return false;
            }
          }

          currState = Transfer(currPC, currState);

          if (IsBottom(currPC, currState))
          {
            if (this.Options.Trace)
            {
              Console.WriteLine("Found bottom, giving up");
            }

            exitState = initialState;
            return false;
          }

          prevPC = lastPC;
          lastPC = currPC;
        }

        if (this.Options.Trace)
        {
          Console.WriteLine("Trying to apply the renaming: {0} --> {1}", prevPC, lastPC.Block.First);
        }


        AState renamed;
        if (!this.IsTop(prevPC, currState) // Inferring good postconditions is based on a heuristic. We do not want the renaming to add more information 
          && this.TryRename(prevPC, lastPC.Block.First, currState, out renamed))
        {
          currState = renamed;
        }
        
        if (this.Options.Trace)
        {
          Console.WriteLine("Found state\n{0}", currState);
        }

        paths++;
        if (paths == 1)
        {
          break;
        }
      }

      if (paths == 0)
      {
        exitState = initialState;
        return false;
      }

      exitState = currState;
      return true;
    }
#endif

    /// <summary>
    /// Maintains the abstract post state for given apc's that have join successors. Computed on demand
    /// </summary>
    protected OnDemandMap<APC, AState> postState;
    
    public bool GetPostState(APC apc, out AState/*?*/ result)
    {
      if (postState.TryGetValue(apc, out result)) {
        return true;
      }
      APC succ;
      if (apc.Block.Count <= apc.Index)
      {
        // this is already the post program point in this block, so pre==post state here
        return GetPreState(apc, out result);
      }
      if (cfg.HasSingleSuccessor(apc, out succ) && !RequiresJoining(succ)) {
        return GetPreState(succ, out result);
      }
      else {
        // recompute from pre-state
        AState preState;
        if (!GetPreState(apc, out preState)) {
          return false;
        }
        result = MutableVersion(preState, apc);
        result = this.Transfer(apc, result);
      }
      // cache
      postState.Add(apc, result);
      return true;
    }

    protected ForwardDFA(ICFG cfg)
      : base(cfg) 
    {
    }

    /// <summary>
    /// State state must be immutable.
    /// </summary>
    public void Run(AState startState) {
      Seed(cfg.Entry, startState);

      ComputeFixpoint();
    }

    #region Forward specific overrides

    protected override int Comparer(APC o1, APC o2) {
      return o2.Block.ReversePostOrderIndex - o1.Block.ReversePostOrderIndex;
    }

    protected override bool RequiresJoining(APC apc) {
      return this.cfg.IsJoinPoint(apc);
    }

    protected override bool IsBackEdge(APC from, APC to) {
      return cfg.IsForwardBackEdge(from, to);
    }
    protected override bool HasSingleSuccessor(APC current, out APC next) {
      return this.cfg.HasSingleSuccessor(current, out next);
    }

    protected override IEnumerable<APC> Successors(APC current) {
      return this.cfg.Successors(current);
    }

    #endregion

  }

  public abstract class BackwardDFA<AState, Type> : DFARoot<AState, Type>
  {
    private bool skipContracts;
    public AState GetPostState(APC apc, AState ifMissing) {
      if (cfg.IsSplitPoint(apc)) {
        AState/*?*/ result;
        if (this.TryJoinState(apc, out result)) {
          return result;
        }
        else {
          return ifMissing;
        }
      }
      throw new NotImplementedException("non split point recomputation not implemented yet");
    }

    protected BackwardDFA(ICFG cfg, bool skipContracts)
      : base(cfg) {

        this.skipContracts = skipContracts;
    }

    /// <summary>
    /// State state must be immutable.
    /// </summary>
    public void Run(AState normalExit, AState exceptionExit) {
      base.Seed(cfg.NormalExit, normalExit);
      base.Seed(cfg.ExceptionExit, exceptionExit);

      ComputeFixpoint();
    }

    #region Backward specific overrides

    protected override int Comparer(APC o1, APC o2) {
      return o1.Block.Index - o2.Block.Index;
    }

    protected override bool RequiresJoining(APC apc) {
      return this.cfg.IsSplitPoint(apc);
    }

    protected override bool IsBackEdge(APC from, APC to) {
      return cfg.IsBackwardBackEdge(from, to);
    }
    protected override bool HasSingleSuccessor(APC current, out APC next) {
      return this.cfg.HasSinglePredecessor(current, out next, this.skipContracts);
    }

    protected override IEnumerable<APC> Successors(APC current) {
      return this.cfg.Predecessors(current, this.skipContracts);
    }

    #endregion

  }

  public class ForwardAnalysisSolver<AState, Type, EdgeData> : ForwardDFA<AState, Type>, IFixpointInfo<APC, AState>
  {
    #region Privates

    private int joinCount = 0;      // For debugging

    readonly Transformer<APC, AState, AState> transfer;
    readonly EdgeConverter<APC, AState, EdgeData> edgeConverter;
    readonly Joiner<APC, AState> joiner;
    readonly Converter<AState, AState> immutableVersion;
    readonly Converter<AState, AState> mutableVersion;
    readonly Action<Pair<AState, TextWriter>> dumper;
    readonly Func<APC, AState, bool> isBottom;
    readonly Func<APC, AState, bool> isTop;
    readonly ILPrinter<APC>/*?*/ printer;
    readonly Printer<EdgeData> edgeDataPrinter;
    readonly Func<APC, APC, EdgeData> edgeDataGetter;

    public ForwardAnalysisSolver(
      ICFG cfg,
      Transformer<APC, AState, AState> transfer,
      EdgeConverter<APC, AState, EdgeData> edgeConverter,
      Joiner<APC, AState> joiner,
      Converter<AState, AState> immutableVersion,
      Converter<AState, AState> mutableVersion,
      Action<Pair<AState,TextWriter>> dumper,
      Func<APC,AState,bool> isBottom,
      Func<APC, AState, bool> isTop,
      ILPrinter<APC> printer,
      Printer<EdgeData> edgeDataPrinter,
      Func<APC, APC, EdgeData> edgeDataGetter
    )
      : base(cfg) {
        Contract.Requires(transfer != null);
        Contract.Requires(immutableVersion != null);
        Contract.Requires(isBottom != null);
        Contract.Requires(isTop != null);
        Contract.Requires(joiner != null);
        Contract.Requires(mutableVersion != null);
        Contract.Requires(edgeConverter != null);
        Contract.Requires(edgeDataGetter != null);

      this.transfer = transfer;
      this.edgeConverter = edgeConverter;
      this.joiner = joiner;
      this.immutableVersion = immutableVersion;
      this.mutableVersion = mutableVersion;
      this.dumper = dumper;
      this.isBottom = isBottom;
      this.isTop = isTop;
      this.printer = printer;
      this.edgeDataPrinter = edgeDataPrinter;
      this.edgeDataGetter = edgeDataGetter;
    }

    protected override void Dump(AState state)
    {
      if (this.dumper != null)
      {
        this.dumper(new Pair<AState, TextWriter>(state, Console.Out));
      }
    }

    #endregion

    public static ForwardAnalysisSolver<AState, Type, EdgeData> Make<Local, Parameter, Method, Field, Source, Dest, Context>(
      IDecodeMSIL<APC, Local, Parameter, Method, Field, Type, Source, Dest, Context, EdgeData> decoder,
      IAnalysis<APC, AState, IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Source, Dest, AState, AState>, EdgeData> driver,
      ILPrinter<APC> printer
    )
      where Context : IMethodContext<Field, Method>
    {
      Contract.Requires(decoder != null);
      Contract.Requires(driver != null);

      IVisitMSIL<APC,Local,Parameter,Method,Field,Type,Source,Dest,AState,AState> visitor = driver.Visitor();
      var result = new ForwardAnalysisSolver<AState, Type, EdgeData>(
        decoder.Context.MethodContext.CFG,
        delegate(APC pc, AState state) { return decoder.ForwardDecode<AState, AState, IVisitMSIL<APC, Local, Parameter, Method, Field, Type, Source, Dest, AState, AState>>(pc, visitor, state); },
        driver.EdgeConversion,
        driver.Join,
        driver.ImmutableVersion,
        driver.MutableVersion,
        driver.Dump,
        delegate(APC pc, AState state) { return (decoder.IsUnreachable(pc) || driver.IsBottom(pc, state)); },
        delegate(APC pc, AState state) { return (driver.IsTop(pc, state)); },
        printer,
        decoder.Display,
        decoder.EdgeData
      );
      result.CachePolicy = driver.CacheStates(result);
      return result;
    }

    #region Overrides

    /// <summary>
    /// Gives us a chance to let client rename variables prior to storing away the state
    /// </summary>
    /// <param name="edge"></param>
    /// <param name="state"></param>
    /// <returns></returns>
    public override void PushState(APC pc, APC next, AState state) {
      var edgeData = this.edgeDataGetter(pc, next);
      if (this.Options.Trace)
      {
        Console.WriteLine("------EdgeConversion from {0} to {1}", pc, next);
        this.edgeDataPrinter(Console.Out, "  ", edgeData);
        Console.WriteLine("------EdgeConversion data end-----------");
      }
      if (this.Options.TraceTimePerInstruction)
      {
        timeCounter.Start();
      }
      this.TraceMemoryUsageIfEnabled("before renaming", pc);

      var transformed = edgeConverter(pc, next, this.RequiresJoining(next), edgeData, state);
      
      
      if (this.Options.TraceTimePerInstruction)
      {
        timeCounter.Stop();
        Console.WriteLine("Time elapsed to perform EdgeConversion: {0}", timeCounter);
      }
      if (this.Options.Trace)
      {
        Console.WriteLine("Edge converted state:");
        this.Dump(transformed);
      }
      this.TraceMemoryUsageIfEnabled("after renaming", null);

      base.PushState(pc, next, transformed);
    }

    protected override bool TryRename(APC prev, APC next, AState currState, out AState renamed)
    {
      var edgeData = this.edgeDataGetter(prev, next);

      if (this.Options.Trace)
      {
        if (edgeData != null)
          Console.WriteLine("Found a remaing");
        else
          Console.WriteLine("No renaming found");
      }

      renamed = this.edgeConverter(prev, next, false, edgeData, currState);
      Contract.Assume(renamed != null);

      return true;
    }



    protected override AState ImmutableVersion(AState state, APC at) {
      return this.immutableVersion(state);
    }

    protected override AState MutableVersion(AState state, APC at) {
      return this.mutableVersion(state);
    }

    protected override bool Join(Pair<APC, APC> edge, AState newState, AState existingState, out AState joinedState, bool widen) {
      bool weaker;
      if (this.Options.Trace) {
        Console.WriteLine("---: --Joining #{2} on {0} (widen={1})", edge.ToString(), widen, joinCount++);
        Console.WriteLine("-------Existing state");
        Dump(existingState);
        Console.WriteLine("-------New state");
        Dump(newState);
      }

      if (this.Options.TraceTimePerInstruction)
      {
        this.timeCounter.Start();
      }

      this.TraceMemoryUsageIfEnabled("before join", edge.One);

      joinedState = joiner(edge, newState, existingState, out weaker, widen);
      if (Options.Trace) {
        Console.WriteLine("-------Result state: changed {0} (widen = {1})", weaker, widen);
        Dump(joinedState);
      }

      if (this.Options.TraceTimePerInstruction)
      {
        this.timeCounter.Stop();
        Console.WriteLine("Elapsed time for the join {0} : {1}", edge.ToString(), timeCounter);
      }

      this.TraceMemoryUsageIfEnabled("after join", null);

      return weaker;
    }

    protected override AState Transfer(APC pc, AState state) {
      if (Options.Trace) {
        Console.WriteLine("State before {0}", pc);
        Dump(state);
        if (printer != null) printer(pc, "---: ", Console.Out);
      }

      if (this.Options.TraceTimePerInstruction)
      {
        this.timeCounter.Start();
      }

      this.TraceMemoryUsageIfEnabled("before instruction", pc);

      var postState = this.transfer(pc, state);
      Contract.Assume(postState != null);

      if (this.Options.TraceTimePerInstruction)
      {
        this.timeCounter.Stop();

        Console.WriteLine("Elapsed time for the transfer function of {0} : {1}", pc, this.timeCounter);
      }

      this.TraceMemoryUsageIfEnabled("after instruction", pc);

      return postState;
    }

    protected override bool IsBottom(APC pc, AState state) {
      return this.isBottom(pc, state);
    }

    protected override bool IsTop(APC pc, AState state)
    {
      return this.isTop(pc, state);
    }

    #endregion


    #region IFixpointInfo<APC,AState> Members

    [ContractVerification(false)]
    public bool PreState(APC label, out AState ifFound)
    {
      return this.GetPreState(label, out ifFound);
    }

    [ContractVerification(false)]
    public bool PostState(APC label, out AState ifFound)
    {
      return this.GetPostState(label, out ifFound);
    }

    public IEnumerable<SubroutineContext> CachedContexts(CFGBlock block)
    {
      foreach (KeyValuePair<APC, AState> kv in this.JoinStates())
      {
        if (kv.Key.Index == 0 && kv.Key.Block == block) yield return kv.Key.SubroutineContext;
      }
    }

    public void PushExceptionState(APC atThrow, AState state) {
      foreach (APC target in this.cfg.ExceptionHandlers<Type, object>(atThrow, null, null))
      {
        PushState(atThrow, target, state);
      }
    }

    public bool TryAStateForPostCondition(AState initialState, out AState exitState)
    {
      APC currPC = default(APC);  // just to please the compiler
      int paths = 0;

      if (this.Options.Trace)
      {
        Console.WriteLine("Looking for a state for the postcondition filtering");
      }

      var currState = initialState;

      var normalExit = this.cfg.NormalExit.Block;
      Contract.Assume(normalExit != null);
      foreach (var prevBlock in normalExit.Subroutine.PredecessorBlocks(this.cfg.NormalExit.Block))
      {
        if (this.Options.Trace)
        {
          Console.WriteLine("Starting analysis of predecessor #{0} (Block {1})", paths, prevBlock.ToString());
        }

        APC prevPC, lastPC;
        prevPC = lastPC = currPC = prevBlock.Last;
        currState = initialState;

        // F: this is a rough strategy. A subroutine may contain loops. In this case we just gave up 
        // (I do not want to compute a fixpoint here to avoid slowing down the inference of posts)
        while (currPC.Block != this.cfg.NormalExit.Block && this.cfg.HasSingleSuccessor(currPC, out currPC))
        {
          if (currPC.Block.First.Equals(currPC))
          {
            int preds = 0;
            foreach (var p in this.cfg.Predecessors(currPC))
            {
              preds++;
            }

            if (preds > 1 && currPC.Block != this.cfg.NormalExit.Block)
            {
              exitState = initialState;
              return false;
            }
          }

          currState = Transfer(currPC, currState);

          if (IsBottom(currPC, currState))
          {
            if (this.Options.Trace)
            {
              Console.WriteLine("Found bottom, giving up");
            }

            exitState = initialState;
            return false;
          }

          prevPC = lastPC;
          lastPC = currPC;
        }

        if (this.Options.Trace)
        {
          Console.WriteLine("Trying to apply the renaming: {0} --> {1}", prevPC, lastPC.Block.First);
        }


        AState renamed;
        if (!this.IsTop(prevPC, currState) // Inferring good postconditions is based on a heuristic. We do not want the renaming to add more information 
          && this.TryRename(prevPC, lastPC.Block.First, currState, out renamed))
        {
          currState = renamed;
        }

        if (this.Options.Trace)
        {
          Console.WriteLine("Found state\n{0}", currState);
        }

        paths++;
        if (paths == 1)
        {
          break;
        }
      }

      if (paths == 0)
      {
        exitState = initialState;
        return false;
      }

      exitState = currState;
      return true;
    }

    #endregion

  }

    /// <summary>
  /// A small class to check timeouts
  /// </summary>
  [ContractVerification(true)]
  public class TimeOutChecker
  {
    [ContractInvariantMethod]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Required for code contracts.")]
    private void ObjectInvariant()
    {
      Contract.Invariant(this.stopWatch != null);
    }


    #region Private state

    enum State { Stopped, Running }

private readonly Stopwatch stopWatch;
  private TimeSpan totalElapsed;
    readonly CancellationToken cancellationToken;
    readonly private int timeout;                                    // The seconds for the timeout
    private TimeoutExceptionFixpointComputation exception;  // We want to throw one exception per TimeOutChecker instance
    private State state;

    #endregion

    public TimeOutChecker(int seconds, bool start = true)
      : this(seconds, new CancellationToken(), start)
    {
      Contract.Requires(seconds >= 0);
    }

    /// <param name="seconds"> The timeout in seconds</param>    
    public TimeOutChecker(int seconds, CancellationToken cancellationToken, bool start = true)
    {
      Contract.Requires(seconds >= 0);

      this.stopWatch = new Stopwatch();
      this.totalElapsed = new TimeSpan();
      if (start)
      {
        this.stopWatch.Start();
        this.state = State.Running;
      }
      else
      {
        this.state = State.Stopped;
      }
      this.timeout = seconds;
      this.cancellationToken = cancellationToken;
      this.exception = null;
    }

    public void Start()
    {
      switch (this.state)
      {
        case State.Stopped:
          {
            this.state = State.Running;
            this.stopWatch.Start();
          }
          break;

        case State.Running:
          {
          }
          break;

        default:
          {
            Contract.Assert(false);
            break;
          }
      }
    }

    public void Stop()
    {
      switch (this.state)
      {
        case State.Running:
          {
            this.state = State.Stopped;

            this.stopWatch.Stop();
            totalElapsed += this.stopWatch.Elapsed;
            this.stopWatch.Reset();
          }
          break;

        case State.Stopped:
          {
          }
          break;

        default:
          {
            Contract.Assert(false);
            break;
          }
      }
    }

    public bool HasAlreadyTimeOut
    {
      get
      {
        return this.exception != null;
      }
    }

    /// <summary>
    /// Check that we did not timed out.
    /// If the timeout was not started, starts it
    /// </summary>
    public void CheckTimeOut(string reason = "")
    {
      this.Start();

      this.stopWatch.Stop();

      this.totalElapsed += this.stopWatch.Elapsed;

      this.stopWatch.Reset();
      this.stopWatch.Start();

      // If we've reached a timeout, we throw an exception, and we abort the fixpoint computation
      if (this.totalElapsed.TotalSeconds >= this.timeout
#if DEBUG
 && !System.Diagnostics.Debugger.IsAttached
#endif
)
      {
#if DEBUG
        if (!String.IsNullOrEmpty(reason))
        {
          Console.WriteLine("Timeout hit: Reason {0}", reason);
        }
#endif
        if (this.exception == null)
        {
          this.exception = new TimeoutExceptionFixpointComputation();
        }
        throw this.exception;
      }

      cancellationToken.ThrowIfCancellationRequested();
    }
  }

  /// <summary>
  /// Exception raised if the fixpoint computation is taking too long
  /// </summary>
  public class TimeoutExceptionFixpointComputation : TimeoutException
  {
    [ThreadStatic]
    private static uint count;

    static public uint ThrownExceptions
    {
      get
      {
        return count;
      }
    }

    public TimeoutExceptionFixpointComputation()
    {
      count++;
    }
  }

  public interface IWidenStrategy
  {    
    bool WantToWiden(APC from, APC to, bool isBackEdge);
  }

  public abstract class StepWidening<Index> : IWidenStrategy
  {
    [ContractInvariantMethod]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Required for code contracts.")]
    private void ObjectInvariant()
    {
      Contract.Invariant(this.widenCounter != null);
    }


    /// <summary>
    /// Mantains a count of the times we've seen each widen point, so that we can decide if we perform widening or join
    /// </summary>
    readonly private Dictionary<Index, int> widenCounter;
    readonly private int N;

    protected StepWidening(int n)
    {
      this.widenCounter = new Dictionary<Index, int>();
      this.N = n;
    }

    abstract protected Index MakeIndex(APC from, APC to);

    #region IWidenStrategy Members

    public bool WantToWiden(APC from, APC to, bool IsBackEdge)
    {
      if (!IsBackEdge)
      {
        return false;
      }
      else
      {
        var index = MakeIndex(from, to);
        if (this.widenCounter.ContainsKey(index))
        {
          this.widenCounter[index]++;
        }
        else
        {
          this.widenCounter[index] = 1;
        }

        return this.N < this.widenCounter[index];
      }
    }

    #endregion
  }

  public class BlockBasedWidening : StepWidening<APC>
  {
    public BlockBasedWidening(int n)
      : base(n)
    { }

    protected override APC MakeIndex(APC from, APC to)
    {
      return to;
    }
  }

  public class EdgeBasedWidening : StepWidening<Pair<APC, APC>>
  {
    public EdgeBasedWidening(int n)
      : base(n)
    { }

    protected override Pair<APC, APC> MakeIndex(APC from, APC to)
    {
      return new Pair<APC, APC>(from, to);
    }
  }
}
